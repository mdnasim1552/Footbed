using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using Microsoft.Reporting.WinForms;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using SPELIB;
using SPERDLC;
using System.Net;
using System.Net.Mail;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using System.IO;
using System.Text;
using System.Xml;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class EmpOverTimeSalary02 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        int curd;
        int frdate;
        int mon;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text =
                    (this.Request.QueryString["Type"].ToString().Trim() == "OvertimeSal")
                        ? "Employee Over Time Salary"
                        : "EMPLOYEE PAY SLIP INFORMATION";

                GetWorkStation();
                GetAllOrganogramList();
                GetJobLocation();
                //this.CommonButton();
                this.SelectType();
                createTable();
                GetLineddl();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
        }

        private void createTable()
        {
            DataTable mnuTbl1 = new DataTable();
            mnuTbl1.Columns.Add("comcod", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("monthid", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("refno", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("section", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("empid", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("desigid", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("otrate", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("overhour", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("otamount", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("offday", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("offamount", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("eothour", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("eotamount", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("netamt", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("basicsal", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("grosssal", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("emptype", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("detxml", Type.GetType("System.String"));

            ViewState["tblsalot"] = mnuTbl1;
        }

        public void CommonButton()
        {
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;


            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "OvertimeSal":
                    this.divRbtnPayType.Visible = true;
                    this.ddlPayType.SelectedValue = "2";
                    this.MultiView1.ActiveViewIndex = 0;
                    this.divLine.Visible = true;
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    ddlWstation_SelectedIndexChanged(null, null);
                    break;
            }
        }

        private void GetLineddl()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";

            ViewState["tbllineddl"] = ds3.Tables[0];
        }

        private void GetJobLocation()
        {
            string comcod = this.GetCompCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string jobLocCode = "87";
            string wtype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            var lst = getlist.GetCommonHRgcod(comcod, jobLocCode);

            var lst1 = lst;

            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType all =
                new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrgcodType { hrgcod = "87000", hrgdesc = "All Location" };
            lst1.Add(all);



            this.ddlJobLocation.DataTextField = "hrgdesc";
            this.ddlJobLocation.DataValueField = "hrgcod";
            this.ddlJobLocation.DataSource = lst1;
            this.ddlJobLocation.DataBind();
            if (wtype == "9403")
            {
                this.ddlJobLocation.SelectedValue = "87002";

            }
            else
            {
                this.ddlJobLocation.SelectedValue = "87000";

            }
        }




        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            this.SelectIndex();

        }

        private void SelectIndex()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "OvertimeSal":
                    this.ShowEmpOvertimeSalary02();
                    break;


            }


        }

        public int Datediffday1(DateTime dtto, DateTime dtfrm)
        {

            int year, mon, day;
            year = dtto.Year - dtfrm.Year;
            mon = dtto.Month - dtfrm.Month;
            day = dtto.Day - dtfrm.Day;
            if (day < 0)
            {

                day = day + 30;
                mon = mon - 1;
                if (mon < 0)
                {
                    mon = mon + 12;
                    year = year - 1;
                }
            }

            if (mon < 0)
            {

                mon = mon + 12;
                year = year - 1;
            }

            //today = year * 365 + mon * 30 + day;
            return mon;
        }

        private string Companygross()
        {
            string ComGross = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                default:
                    ComGross = "";
                    ;
                    break;
            }

            return ComGross;
        }


        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {


        }


        private string GetComCallType()
        {
            string comcod = this.GetCompCode();
            string CallType = "";
            switch (comcod)
            {
                case "5305":
                case "5306":
                    CallType = "OVERTIMESALARYAFTER1HOURWHOLIDAY";
                    break;

                default:
                    CallType = "OVERTIMESALARY";
                    break;

            }

            return CallType;




        }

        private void ShowEmpOvertimeSalary02()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string empStatus = (this.ddlEmpStatus.SelectedValue == "1") ? "R" : (this.ddlEmpStatus.SelectedValue == "2") ? "H" :  "";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string EmpType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string Division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
            mon = this.Datediffday1(Convert.ToDateTime(curdate), Convert.ToDateTime(dt1));
            string payType = (this.ddlPayType.SelectedValue == "0") ? "" : (this.ddlPayType.SelectedValue == "1") ? "19%" : "%";
            DataSet ds3;
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string empwkStation = this.ddlWstation.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SALOVLOCK", monthid, empwkStation, empStatus,"", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            Session["UserLog"] = ds1.Tables[0];
            this.lblComSalovLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();
            string Calltype = this.GetComCallType();
            if ((this.lblComSalovLock.Text == "True"))
            {
                ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "OVERTIMESALSHEET", monthid, Department,
                    section, EmpType, payType, Division, empStatus, line, "");

                if (ds3.Tables[0].Rows.Count == 0)
                    ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL03", Calltype, frmdate, todate,
                        EmpType, Division, Department, section, payType, empStatus, line);
            }
            else
            {
                ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL03", Calltype, frmdate, todate, EmpType,
                    Division, Department, section, payType, empStatus, line);
            }
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvovsal02.DataSource = null;
                this.gvovsal02.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;
            this.LoadGrid();

        }


        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {


                case "OvertimeSal":
                    this.gvovsal02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    if (dt.Rows.Count == 0)
                    {
                        this.gvovsal02.DataSource = null;
                        this.gvovsal02.DataBind();

                        return;
                    }

                    this.gvovsal02.DataSource = dt;
                    this.gvovsal02.DataBind();

                    ((CheckBox)this.gvovsal02.FooterRow.FindControl("chkSalaryovLock")).Checked = (this.lblComSalovLock.Text == "True") ? true : false;
                    if (Request.QueryString["Entry"].ToString() == "Payroll")
                    {

                        ((LinkButton)this.gvovsal02.FooterRow.FindControl("lnkFiUpdateoSalary")).Visible = (((CheckBox)this.gvovsal02.FooterRow.FindControl("chkSalaryovLock")).Checked) ? false : true;
                        ((CheckBox)this.gvovsal02.FooterRow.FindControl("chkSalaryovLock")).Enabled = false;

                    }
                    else
                    {
                        ((LinkButton)this.gvovsal02.FooterRow.FindControl("lnkFiUpdateoSalary")).Visible = true;
                    }
                   
                    if (dt.Rows.Count > 0)
                    {
                        Session["Report1"] = gvovsal02;
                        ((HyperLink)this.gvovsal02.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        this.FooterCalculation();
                    }
                    break;

            }

        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblpay"];

            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "OvertimeSal":
                    //((Label)this.gvovsal02.FooterRow.FindControl("lgvFotofdayamt")).Text = Convert
                    //    .ToDouble((Convert.IsDBNull(dt.Compute("sum(otoffamount)", ""))
                    //        ? 0.00
                    //        : dt.Compute("sum(otoffamount)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvovsal02.FooterRow.FindControl("lgvFosamt")).Text = Convert
                    //    .ToDouble((Convert.IsDBNull(dt.Compute("sum(osamount)", ""))
                    //        ? 0.00
                    //        : dt.Compute("sum(osamount)", ""))).ToString("#,##0;(#,##0); ");



                    
                    //double tohur = Convert.toi((Convert.IsDBNull(dt.Compute("sum(ohhour)", "")) ?
                    //                   0 : dt.Compute("sum(ohhour)", "")));
                    //int txtHrsFrac = Convert.ToInt32(Minute / 60);
                    //double txtMinFrac = Minute % 60;
                    //double totalHrs = txtHrsFrac + txtMinFrac * 0.01;


                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFotamt")).Text = Convert
                        .ToDouble((Convert.IsDBNull(dt.Compute("sum(otamount)", ""))
                            ? 0.00
                            : dt.Compute("sum(otamount)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFot2amt")).Text = Convert
                        .ToDouble((Convert.IsDBNull(dt.Compute("sum(osamount)", ""))
                            ? 0.00
                            : dt.Compute("sum(osamount)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFoffdayamt")).Text = Convert
                       .ToDouble((Convert.IsDBNull(dt.Compute("sum(offamount)", ""))
                           ? 0.00
                           : dt.Compute("sum(offamount)", ""))).ToString("#,##0;(#,##0); ");




                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFnetamtos")).Text = Convert
                        .ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ? 0.00 : dt.Compute("sum(netamt)", "")))
                        .ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFBasic")).Text = Convert
                        .ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", "")))
                        .ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFGross")).Text = Convert
                        .ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal1)", "")) ? 0.00 : dt.Compute("sum(gssal1)", "")))
                        .ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFnetamtoeotcas")).Text = Convert
                        .ToDouble(
                            (Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", "")))
                        .ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFnetamtosbnk")).Text = Convert
                        .ToDouble(
                            (Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", "")))
                        .ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lblgvFfood")).Text = Convert
                        .ToDouble((Convert.IsDBNull(dt.Compute("sum(foodalw)", "")) ? 0.00 : dt.Compute("sum(foodalw)", "")))
                        .ToString("#,##0;(#,##0); ");

                    break;

            }



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "OvertimeSal":
                    this.PrintOvertimeSalary02();
                    break;
            }
        }

        private void PrintOvertimeSalary02()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " +
                                 username + " ,Time: " + printdate;
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.OverTimeSal>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptOverTimeSal", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comname));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("todate", todate));

            Session["Report1"] = Rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            this.LoadGrid();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ibtnEmpList_Click(null, null);
        }

        protected void lnkFiUpdateoSalary_Click(object sender, EventArgs e)
        {
            string msg = "";
            DataRow[] dr6 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr6[0]["entry"]))
            {
                msg = "You have no permission!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            string empstatus = (this.ddlEmpStatus.SelectedValue == "1") ? "R" : (this.ddlEmpStatus.SelectedValue == "2") ? "H" : "";

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblpay"];
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM");

            bool result = false;

            string Company =  this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string Department = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string sectionS = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";

            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "DELETEOVERSALSHEET", monthid, Company, Department, sectionS, division, empstatus, "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            DataTable tblsalot = ((DataTable)ViewState["tblsalot"]).Clone();
            foreach (DataRow row in dt.Rows)
            {

                DataRow dr2 = tblsalot.NewRow();
                dr2["comcod"] = comcod;
                dr2["monthid"] = monthid;
                dr2["section"] = row["secid"].ToString();
                dr2["refno"] = row["refno"].ToString();
                dr2["empid"] = row["empid"].ToString();
                dr2["desigid"] = row["desigid"].ToString();
                dr2["otrate"] = row["otrate"].ToString();
                dr2["overhour"] = row["ohour"].ToString();
                dr2["otamount"] = row["otamount"].ToString();
                dr2["offday"] = row["offday"].ToString();
                dr2["offamount"] = row["offamount"].ToString();
                dr2["eothour"] = row["osday"].ToString();
                dr2["eotamount"] = row["osamount"].ToString();
                dr2["netamt"] = row["netamt"].ToString();
                dr2["basicsal"] = row["bsal"].ToString();
                dr2["grosssal"] = row["gssal1"].ToString();
                dr2["emptype"] = empstatus;
                dr2["detxml"] = row["xmlcol1"].ToString();
                tblsalot.Rows.Add(dr2);

            }


            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(tblsalot);
            ds1.Tables[0].TableName = "eot";

            string assdd = tblsalot.Rows.Count.ToString();
            string xmldata = ds1.GetXml();

            double ttlemp = Convert.ToDouble(tblsalot.Rows.Count.ToString());
            double bankamt = Convert.ToDouble(dt.Compute("SUM(bankamt)", string.Empty));
            double cashamt = Convert.ToDouble(dt.Compute("SUM(cashamt)", string.Empty));
            double netamt = Convert.ToDouble(dt.Compute("SUM(netamt)", string.Empty));

            DataSet ds = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INUPSALSHEETOV", ds1, null, null, monthid);
            if (ds == null)
            {
                msg = "EOT Salary Sheet Update Failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }

            else
            {
                string Depart = this.ddlWstation.SelectedValue.ToString();
                string Salarylock = (((CheckBox)this.gvovsal02.FooterRow.FindControl("chkSalaryovLock")).Checked) ? "1" : "0";
                DataTable dtuser = (DataTable)Session["UserLog"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string PostedByid = (this.Request.QueryString["Entry"] == "Payroll") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (this.Request.QueryString["Entry"] == "Payroll") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (this.Request.QueryString["Entry"] == "Payroll") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (this.Request.QueryString["Entry"] == "Payroll") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                string EditByid = (this.Request.QueryString["Entry"] == "Payroll") ? "" : userid;
                string Editdat = (this.Request.QueryString["Entry"] == "Payroll") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
                string EditTrnid = (this.Request.QueryString["Entry"] == "Payroll") ? "" : Terminal;

                result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INORUPSALOVLOCK", null, null, null, monthid, Depart,
                    Salarylock, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, Editdat, EditTrnid, empstatus);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }


                double empid = Convert.ToDouble(ds.Tables[0].Rows[0]["empid"]);
                double upneamt = Convert.ToDouble(ds.Tables[0].Rows[0]["netamt"]);
                double balamt = netamt - upneamt;
                double balttlemp = ttlemp - empid;
                this.lblmsg.Text = "EOT Update Sucess/ EOT lock" + Salarylock + "," + (balttlemp == 0 ? "" : "Total Emp:" + empid.ToString()) + (balamt == 0 ? "" : "Total Net Amount:" + upneamt.ToString());
                msg = this.lblmsg.Text;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            }

            if (ConstantInfo.LogStatus == true)
            {
                string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string voutype = "EMPLOYEE EOT  SALARY Updated" + Company;
                string eventdesc = "Month ID: " + todate + "" + " Dated: " + todate;
                string eventdesc2 = "Overtime Allowance," + this.lblmsg.Text + "Employe Status-" + (this.ddlEmpStatus.SelectedValue == "1" ? "Resign Employe" : "Active Employee");
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), voutype, eventdesc, eventdesc2);

            }

        }

        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }

        private void GetWorkStation()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            switch (comcod)
            {
                //FB & Footbed Footwear OT Emp. Only
                case "5305":
                    lst = lst.FindAll(x => (x.actcode.Substring(0, 4) == "0000") || (x.actcode.Substring(0, 4) == "9403") || (x.actcode.Substring(0, 4) == "9414"));
                    break;
                case "5306":
                    lst = lst.FindAll(x => x.actcode.Substring(0, 4) == "0000" || x.actcode.Substring(0, 4) == "9408" || x.actcode.Substring(0, 4) == "9416");
                    break;

                default:
                    lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
                    break;
            }
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

            this.ddlWstation_SelectedIndexChanged(null, null);

        }

        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString(); //940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst =
                (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];


            var lst1 = lst.FindAll(x =>
                x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" &&
                x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all =
                new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf
                { actcode = "000000000000", actdesc = "All Division" };
            lst1.Add(all);

            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst1;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "000000000000";

            this.ddlDivision_SelectedIndexChanged(null, null);

        }

        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString(); //940100000000

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst =
                (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x =>
                x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" &&
                x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all =
                new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf
                { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "000000000000";

            this.ddlDept_SelectedIndexChanged(null, null);

        }

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString(); //940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst =
                (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all =
                new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf
                { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";
        }

        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }

        protected void gvovsal02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvovsal02.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "OvertimeSal":
                    this.lnkFiUpdateoSalary_Click(null, null);
                    break;
                default:
                    break;


            }
        }

        protected void lblgvdeptandemployeeemp_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            string comcod = this.GetCompCode();
            this.lbmodalheading.Text = "Individual Monthly Over Time Details Information. Date :" +
                                       this.txtfromdate.Text.ToString() + " To: " + this.txttodate.Text.ToString();

            int index = row.RowIndex;
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();

            string frmdate = this.txtfromdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string Empcode =
                ((Label)this.gvovsal02.Rows[index].FindControl("lblEmpidOT")).Text
                .ToString(); 
            string xmldata = ((Label)this.gvovsal02.Rows[index].FindControl("lblxmlcol1")).Text.ToString();

            DataSet ds = new DataSet();
            byte[] buffer = Encoding.UTF8.GetBytes(xmldata);
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                XmlReader reader = XmlReader.Create(stream);
                ds.ReadXml(reader);

            }

            DataTable dt = ds.Tables[0];
            this.mgvbreakdown.DataSource = dt;
            this.mgvbreakdown.DataBind();


            ArrayList rows = new ArrayList();

            foreach (DataRow dataRow in dt.Rows)
                rows.Add(string.Join(";", dataRow.ItemArray.Select(ovthour => ovthour.ToString())));

            Session["Report1"] = mgvbreakdown;
            if (dt.Rows.Count > 0)
                ((HyperLink)this.mgvbreakdown.HeaderRow.FindControl("mhlbtntbCdataExel")).NavigateUrl =
                    "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        protected void gvovsal02_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton txtgvname = (LinkButton)e.Row.FindControl("lblgvdeptandemployeeemp");
            }
        }
    }
}