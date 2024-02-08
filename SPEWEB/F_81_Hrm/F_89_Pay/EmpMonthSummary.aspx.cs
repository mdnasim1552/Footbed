using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class EmpMonthSummary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                string type = this.Request.QueryString["Type"].ToString().Trim();

                ((Label)this.Master.FindControl("lblTitle")).Text = type == "salati" ? "AIT purpose salary " : "Monthly Attendance Statement";


                //    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                // this.txtfromdate.Text = "21" + this.txtfromdate.Text.Trim().Substring(2);
                //  this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.SetDate();
                this.GetWorkStation();
                this.GetAllOrganogramList();
                //this.GetSectionType();
                //this.GetEmployeeName();
            }

        }

        private void SetDate()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "8701"://Sanmer
                            //case "4305"://Rupayan
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "21" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                default:

                    // this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    //this.txtfromdate.Text = "26" + this.txtfromdate.Text.Trim().Substring(2);
                    //this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
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
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }
        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);

            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
            lst1.Add(all);


            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst1;
            this.ddlDivision.DataBind();

            this.ddlDivision_SelectedIndexChanged(null, null);

        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);

            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept_SelectedIndexChanged(null, null);

        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
        }
        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);


            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);
            //lst1.Add()

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";
            this.GetEmployeeName();
        }

        private void GetSectionType()
        {
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {

                case "DateWise":
                    this.lbltodate.Visible = true;
                    this.txttodate.Visible = true;
                    // this.lFinalUpdatedwise.Visible = true;

                    string date = "01-" + System.DateTime.Today.ToString("MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(date).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.lnkbtnShow.Visible = false;
                    //case "4305"://Rupayan

                    break;

                default:
                    break;
            }



        }






        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            if (type == "salati")
            {
                this.ShowATI();
            }
            else
            {
                this.ShowSal();
            }

        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ibtnEmpList_Click(null, null);
        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            // this.GetEmployeeName();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            if (dt.Rows.Count == 0)
                return;

            string linkType = Request.QueryString["Type"].ToString().Trim();
            switch (linkType)
            {
                case "salary":
                    this.gvempsumm.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvempsumm.DataSource = dt;
                    this.gvempsumm.DataBind();
                    break;

                case "salati":
                    this.gvati.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvati.DataSource = dt;
                    this.gvati.DataBind();
                    this.FooterCalculation();
                    break;

            }

        }
        private void FooterCalculation()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblpay"];
            if (dt.Rows.Count == 0)
                return;
            switch (type)
            {
                case "salary":

                    break;
                case "salati":

                    ((Label)this.gvati.FooterRow.FindControl("lgvFbsal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ?
                                    0 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFhrent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ?
                                   0 : dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFcven")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ?
                                   0 : dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFmallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ?
                                   0 : dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvati.FooterRow.FindControl("lgvFgsal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gsal)", "")) ?
                                   0 : dt.Compute("sum(gsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFgsal1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gsal1)", "")) ?
                                   0 : dt.Compute("sum(gsal1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFbonamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ?
                                  0 : dt.Compute("sum(bonamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFitax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ?
                                   0 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFcashamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ?
                                   0 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFbankamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ?
                                   0 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;
            }

            //this.gvati.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvati.DataSource = dt;
            //this.gvati.DataBind();
            //this.ddlEmplist.DataTextField = "empname";
            //this.ddlEmplist.DataValueField = "empid";
            //this.ddlEmplist.DataSource = dt;
            //this.ddlEmplist.DataBind();

        }

        private void ShowSal()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string CompanyName = this.ddlWstation.SelectedValue.ToString().Substring(0, 2);
            string projectcode = this.ddlDivision.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString();
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");


            var ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "PAYROLL_DETAIL06", frmdate, todate, projectcode, section, CompanyName, "", "", "", "");

            if (ds3 == null)
            {
                this.gvempsumm.DataSource = null;
                this.gvempsumm.DataBind();
                return;

            }

            //  DataTable dt = HiddenSameData(ds3.Tables[0]);
            Session["tblpay"] = ds3.Tables[0];

            this.Data_Bind();

        }

        private void ShowATI()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string EmpType = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string div = this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string dept = this.ddlDept.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString();

            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");

            string empid = (this.ddlEmplist.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlEmplist.SelectedValue.ToString() + "%";
            string empname = this.ddlEmplist.SelectedItem.ToString();
            var ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETAITPURSALARY", frmdate, todate, EmpType, div, dept, empid, section, "", "");
            if (ds3.Tables[0].Rows.Count == 0)
            {
                this.gvati.DataSource = null;
                this.gvati.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "FnDanger('" + empname + "');", true);
                return;

            }
            Session["tblpay"] = ds3.Tables[0];
            ViewState["taxinf"] = ds3.Tables[1];

            this.Data_Bind();

        }
        private void GetEmployeeName()
        {

            string comcod = this.GetCompCode();
            string pactcode = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string dept = this.ddlDept.SelectedValue.ToString();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", "%", pactcode, "%", dept, "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt1 = new DataTable();
            dt1 = ds1.Tables[0].Copy();

            dt1 = dt1.DefaultView.ToTable(true, "empid", "empname");
            dt1.Rows.Add("000000000000", "ALL");

            this.ddlEmplist.DataTextField = "empname";
            this.ddlEmplist.DataValueField = "empid";
            this.ddlEmplist.DataSource = dt1;
            this.ddlEmplist.DataBind();
            this.ddlEmplist.SelectedValue = "000000000000";

        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            if (type == "salati")
            {
                if (this.CheckAitPrint.Checked == true)
                {
                    this.PrintEMPAITcertificate();
                }
                else
                {
                    this.PrintATI();
                }

            }
            else
            {
                this.PrintSallary();
            }
        }


        protected void PrintSallary()
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


            DataTable dt = (DataTable)Session["tblpay"];
            if (dt == null)
                return;


            var lstsum = dt.DataTableToList<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.EmpMonthSummary>();

            LocalReport rpt1 = new LocalReport();
           
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpMonthSumm", lstsum, null, null);
                   
            
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("Comname", comnam));
            rpt1.SetParameters(new ReportParameter("comaddress", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Monthly Attendance Statement"));
            rpt1.SetParameters(new ReportParameter("date1", ""));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void PrintATI()
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            string dateft = "";

            DataTable dt = (DataTable)Session["tblpay"];
            if (dt == null)
                return;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lstsum = dt.DataTableToList<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.aitpurpose>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RdlcAITPurpose", lstsum, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "AIT Purpose Salary Statement"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("dateft", dateft));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintEMPAITcertificate()
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");
            string empid = this.ddlEmplist.SelectedValue.ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMM-yyyy");
            string daterang = fromdate + " To " + todate;
            DataTable dt = (DataTable)Session["tblpay"];
            DataTable dttax = (DataTable)ViewState["taxinf"];
            if (dt == null)
                return;
            string bankinfo = this.TxBtnInf.Text.ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lstsum = dt.DataTableToList<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.aitpurpose>();
            var lst2 = dttax.DataTableToList<SPERDLC.RD_81_HRM.RD_89_Pay.RpHRtPayroll.aitpurpose>();
            var list = lstsum.FindAll(x => x.empid == empid);
            var taxlist = lst2.FindAll(x => x.empid == empid);
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptAitEmpCertificate", list, taxlist, null);
            rpt1.EnableExternalImages = true;
            double total = Convert.ToDouble(list[0].bsal) + Convert.ToDouble(list[0].mallow) + Convert.ToDouble(list[0].incent) + Convert.ToDouble(list[0].adv) + Convert.ToDouble(list[0].hrent) + Convert.ToDouble(list[0].cven);
            string inwords = "In Words: (" + ASTUtility.Trans(total, 2) + ")";
            double taxamt = Convert.ToDouble(taxlist.Sum(item => item.itax));
            string incometax = "( TK." + ASTUtility.Trans(taxamt, 2) + ")";
            rpt1.SetParameters(new ReportParameter("Comname", comnam));
            rpt1.SetParameters(new ReportParameter("comaddress", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "TO WHOM IT MAY CONCERN"));
            rpt1.SetParameters(new ReportParameter("empname", list[0].empname.ToString()));
            rpt1.SetParameters(new ReportParameter("designation", list[0].desig.ToString()));
            rpt1.SetParameters(new ReportParameter("date", "Date:" + printdate));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("inwords", inwords));
            rpt1.SetParameters(new ReportParameter("taxamt", taxamt.ToString()));
            rpt1.SetParameters(new ReportParameter("incometax", incometax));
            rpt1.SetParameters(new ReportParameter("daterang", daterang));
            rpt1.SetParameters(new ReportParameter("bankinfo", bankinfo));
            rpt1.SetParameters(new ReportParameter("author", "MR. XXX" + System.Environment.NewLine + "AGM (Finance & Accounts)"));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}