using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class DailyAttenSummary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetEmpLine();
                this.GetJobLocation();
                string type = this.Request.QueryString["Type"].ToString() == "AttnCatSum" ? " ( Category Wise ) " : "";
                ((Label)this.Master.FindControl("lblTitle")).Text = "Daily Attendance Summary" + type;
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
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
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1 all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1("000000000000","ALL","","","","");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation.SelectedValue = "000000000000";

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
            this.ddlDivision.SelectedValue = "000000000000";

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
            this.ddlDept.SelectedValue = "000000000000";

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

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";

        }
        private void GetEmpLine()
        {
            string comcod = GetCompCode();
            string CompanyName = (this.ddlSection.SelectedValue.ToString()=="000000000000" ? "94" : this.ddlSection.SelectedValue.ToString().Substring(0, 4))+"%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";

            var lst = getlist.GETLine(comcod, CompanyName, division, deptname, section);

            this.ddlEmpLine.DataTextField = "linedesc";
            this.ddlEmpLine.DataValueField = "linecode";
            this.ddlEmpLine.DataSource = lst;
            this.ddlEmpLine.DataBind();

        }
        private void GetJobLocation()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }

        protected void lnkbtn_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "AttnSum":
                    DailyAttenSummaryinfo();
                    break;
                case "AttnSum2":
                    DailyAttenSummary2();
                    break;
                case "AttnSum3":
                    DailyAttenSummary3();
                    break;
                case "AttnCatSum":
                    DailyAttenSummaryCategory();
                    break;

            }

        }

        private void DailyAttenSummaryinfo()
        {
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "EMPDAILYATTSUMMERY", date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDailyAttnSummary.DataSource = null;
                this.gvDailyAttnSummary.DataBind();
                return;
            }
            this.gvDailyAttnSummary.DataSource = ds1;
            this.gvDailyAttnSummary.DataBind();
            DataTable dt = ds1.Tables[0];
            // ViewState["tbldatsum02"] = dt.Copy();
            ViewState["tbldatsum"] = this.HiddenMisSameData(dt);
            this.Data_bind();
        }
        private void DailyAttenSummary2()
        {
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "DAILY_WORKER_ATTEN_SUMMURY", date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDailyAttnSummary2.DataSource = null;
                this.gvDailyAttnSummary2.DataBind();
                return;
            }

            this.gvDailyAttnSummary2.DataSource = ds1;
            this.gvDailyAttnSummary2.DataBind();
            ViewState["tbldatsum"] = this.HiddenMisSameData(ds1.Tables[0]);
            this.Data_bind();
        }
        private void DailyAttenSummary3()
        {
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE_SUMMARY", "DAILY_WORKER_ATTEN_SUMMURY_DIR_INDIRECT", date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDailyAttnSummary2.DataSource = null;
                this.gvDailyAttnSummary2.DataBind();
                return;
            }

            this.gvDailyAttnSummary2.DataSource = ds1;
            this.gvDailyAttnSummary2.DataBind();
            ViewState["tbldatsum"] = this.HiddenMisSameData(ds1.Tables[0]);
            this.Data_bind();
        }

        private void DailyAttenSummaryCategory()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                string company = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
                string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
                string line = (this.ddlEmpLine.SelectedValue.ToString()=="00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString())+"%";
                string jobLocation = this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "%" : this.ddlJobLocation.SelectedValue.ToString() + "%";
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "EMPDAILYATTSUMMERYCATEGORY", date, company, div, dept, section, line, jobLocation, userid, "");
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    this.gvAttnCatSummary.DataSource = null;
                    this.gvAttnCatSummary.DataBind();
                    return;
                }
                this.gvAttnCatSummary.DataSource = ds1;
                this.gvAttnCatSummary.DataBind();
                DataTable dt = ds1.Tables[0];
                ViewState["tbldatsum"] = this.HiddenMisSameData(dt);
                this.Data_bind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);

            }
        }

        private void Data_bind()
        {
            DataTable dt = (DataTable)ViewState["tbldatsum"];
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "AttnSum":
                    this.MultiView.ActiveViewIndex = 0;
                    this.gvDailyAttnSummary.DataSource = dt;
                    this.gvDailyAttnSummary.DataBind();
                    break;

                case "AttnCatSum":
                    this.MultiView.ActiveViewIndex = 2;
                    this.gvAttnCatSummary.DataSource = dt;
                    this.gvAttnCatSummary.DataBind();
                    this.FooterCalculationCat();
                    break;

                default:
                    this.MultiView.ActiveViewIndex = 1;
                    this.gvDailyAttnSummary2.DataSource = dt;
                    this.gvDailyAttnSummary2.DataBind();
                    this.FooterCalculation();
                    break;
            }            
        }
        public void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tbldatsum"];
            var lst2 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttenSummarySkillWise>();
            //var lst2 = lst1.FindAll(l => l.linecode.Substring(8) == "BBBB");
            
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvBSupervisorF")).Text = (lst2.Sum(p => p.budsup) == 0) ? "" : lst2.Sum(p => p.budsup).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvBLinemanF")).Text = (lst2.Sum(p => p.budline) == 0) ? "" : lst2.Sum(p => p.budline).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvBOpreatoridF")).Text = (lst2.Sum(p => p.budop) == 0) ? "" : lst2.Sum(p => p.budop).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvBTotalidF")).Text = (lst2.Sum(p => p.btotal) == 0) ? "" : lst2.Sum(p => p.btotal).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvPSuperF")).Text = (lst2.Sum(p => p.psup) == 0) ? "" : lst2.Sum(p => p.psup).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvPLinemanF")).Text = (lst2.Sum(p => p.pline) == 0) ? "" : lst2.Sum(p => p.pline).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvPOw1F")).Text = (lst2.Sum(p => p.pw1) == 0) ? "" : lst2.Sum(p => p.pw1).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvPOw2F")).Text = (lst2.Sum(p => p.pw2) == 0) ? "" : lst2.Sum(p => p.pw2).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvPOw3F")).Text = (lst2.Sum(p => p.pw3) == 0) ? "" : lst2.Sum(p => p.pw3).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvPTotalF")).Text = (lst2.Sum(p => p.ptotal) == 0) ? "" : lst2.Sum(p => p.ptotal).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvASuperF")).Text = (lst2.Sum(p => p.asup) == 0) ? "" : lst2.Sum(p => p.asup).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvALinemanF")).Text = (lst2.Sum(p => p.aline) == 0) ? "" : lst2.Sum(p => p.aline).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvAOw1F")).Text = (lst2.Sum(p => p.aw1) == 0) ? "" : lst2.Sum(p => p.aw1).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvAOw2F")).Text = (lst2.Sum(p => p.aw2) == 0) ? "" : lst2.Sum(p => p.aw2).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvAOw3F")).Text = (lst2.Sum(p => p.aw3) == 0) ? "" : lst2.Sum(p => p.aw3).ToString("#,##;(#,##)");
            ((Label)this.gvDailyAttnSummary2.FooterRow.FindControl("lblgvATotalF")).Text = (lst2.Sum(p => p.atotal) == 0) ? "" : lst2.Sum(p => p.atotal).ToString("#,##;(#,##)");
        }
        //Nayan
        private DataTable HiddenMisSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string seccod = dt1.Rows[0]["seccod"].ToString();
           // string emptype = dt1.Rows[0]["empcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["seccod"].ToString() == seccod)
                {
                    dt1.Rows[j]["secdesc"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["seccod"].ToString() == seccod)
                        dt1.Rows[j]["secdesc"] = "";
                }
                //if (dt1.Rows[j]["empcode"].ToString() == emptype)
                //{
                //    dt1.Rows[j]["emptype"] = "";
                //}
                //else
                //{
                //    if (dt1.Rows[j]["empcode"].ToString() == emptype)
                //        dt1.Rows[j]["emptype"] = "";
                //}


                seccod = dt1.Rows[j]["seccod"].ToString();
              //  emptype = dt1.Rows[j]["empcode"].ToString();
            }
            return dt1;
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "AttnSum":
                    PrintAttSumm();
                    break;

                case "AttnSum2":
                    printAttSumm2();
                    break;

                case "AttnSum3":
                    printAttSumm2();
                    break;

                case "AttnCatSum":
                    PrintAttnCatSum();
                    break;

            }
        }

        private void PrintAttnCatSum()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string txtDate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string comcod = GetCompCode();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            this.DailyAttenSummaryCategory();

            DataTable dt = (DataTable)ViewState["tbldatsum"];
            string empType = this.ddlWstation.SelectedItem.Text.Trim();          
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttnSummaryCatWise>();
            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5306"://Footbed
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAttnSumCatFootbed", list, null, null);
                    break;

                default:
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAttnSumCatFB", list, null, null);
                    break;          
            
            
            }
           
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Attendance Summary Report(Category Wise)"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + txtDate));
            Rpt1.SetParameters(new ReportParameter("empType", empType));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintAttSumm()
        {

            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    PrintAttSummFB();
                    break;
                default:
                    PrintAttSummGen();
                    break;
            }



        }

        private void PrintAttSummGen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)ViewState["tbldatsum"];

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttenSummary>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAttenSummary", lst, null, null);
            rpt1.EnableExternalImages = true;

            string foot = ASTUtility.Concat(compname, username, printdate);
            rpt1.SetParameters(new ReportParameter("date", "Date: " + date));
            rpt1.SetParameters(new ReportParameter("RptTitle", "DAILY ATTENDANCE SUMMARY"));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("footer", foot));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintAttSummFB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "EMPDAILYATTSUMMERYFB", date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt1 = ds1.Tables[0];
            var lst = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttenSummary>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAttenSummaryFB", lst, null, null);
            rpt1.EnableExternalImages = true;

            string foot = ASTUtility.Concat(compname, username, printdate);
            rpt1.SetParameters(new ReportParameter("date", "Date: " + date));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Attendance Summery Report" + " - " + "Date: " + date));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("footer", foot));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void printAttSumm2()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)ViewState["tbldatsum"];

            var lst2 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttenSummarySkillWise>();

            LocalReport rpt2 = new LocalReport();
            rpt2 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAttenSummarySkillWise", lst2, null, null);
            rpt2.EnableExternalImages = true;

            string foot = ASTUtility.Concat(compname, username, printdate);

            rpt2.SetParameters(new ReportParameter("comnam", comnam));
            rpt2.SetParameters(new ReportParameter("comadd", comadd));
            rpt2.SetParameters(new ReportParameter("date", "Date: " + date));
            rpt2.SetParameters(new ReportParameter("RptTitle", "SKILLWISE DAILY ATTENDANCE SUMMARY"));
            rpt2.SetParameters(new ReportParameter("footer", foot));

            Session["Report1"] = rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void gvDailyAttnSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string linecode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "linecode"));

                if (linecode == "AAAA")
                {
                    e.Row.BackColor = Color.Beige;
                }
                else
                {
                    e.Row.BackColor = Color.Azure;
                }
            }
        }

        protected void gvDailyAttnSummary2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 4;
                gvrow.Cells.Add(cell0);



                TableCell cell1 = new TableCell();
                cell1.Text = "Budget";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 4;
                gvrow.Cells.Add(cell1);


                TableCell cell2 = new TableCell();
                cell2.Text = "Present";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 6;
                gvrow.Cells.Add(cell2);


                TableCell cell3 = new TableCell();
                cell3.Text = "Absent";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 6;
                gvrow.Cells.Add(cell3);



                gvDailyAttnSummary2.Controls[0].Controls.AddAt(0, gvrow);


            }
        }

        protected void gvAttnCatSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string linecode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "linecode")).Trim();
                string depcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "depcode"));
                string tempdep = depcode.Substring(0, 2);
                
                TextBox txtgatePass = (TextBox)e.Row.FindControl("txtgatePass");
                Label lblgvlinecsum = (Label)e.Row.FindControl("lblgvlinecsum");


                if (linecode == "AAAA" || linecode == "BBBB")
                {
                    e.Row.BackColor = Color.Beige;
                    txtgatePass.ReadOnly = true;
                    lblgvlinecsum.Attributes["style"] = "font-weight:bold";
                }
                else
                {
                    e.Row.BackColor = Color.Azure;
                    txtgatePass.ReadOnly = false;

                }

                
            }
        }



        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            this.Session_Update();            

        }

        private void Session_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tbldatsum"];

            int TblRowIndex2;
            for (int i = 0; i < this.gvAttnCatSummary.Rows.Count; i++)
            {

                TblRowIndex2 = (this.gvAttnCatSummary.PageSize) * (this.gvAttnCatSummary.PageIndex) + i;

                double gatepass = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvAttnCatSummary.Rows[i].FindControl("txtgatePass")).Text.Trim()));
                double psent = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvAttnCatSummary.Rows[i].FindControl("lblCatPresent")).Text.Trim()));
                double heldmpower = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvAttnCatSummary.Rows[i].FindControl("lblCatheldmpower")).Text.Trim()));


                double acpresent = gatepass + psent;
                double actman = gatepass + heldmpower;

                ((Label)this.gvAttnCatSummary.Rows[i].FindControl("lblCatAcpresent")).Text = acpresent.ToString("#,##0.000;(#,##0.000); ");
                ((Label)this.gvAttnCatSummary.Rows[i].FindControl("lblCatActMan")).Text = actman.ToString("#,##0.000;(#,##0.000); ");

                tbl1.Rows[TblRowIndex2]["acpresent"] = acpresent;
                tbl1.Rows[TblRowIndex2]["gatepass"] = gatepass;
                tbl1.Rows[TblRowIndex2]["actman"] = actman;
            }


           // DataTable dt1 = (DataTable)ViewState["tbldatsum"];

            DataView dv1 = tbl1.Copy().DefaultView;
            dv1.RowFilter = ("linecode not  like 'AAAA%' and linecode not  like 'BBBB%'");
            DataTable dt = dv1.ToTable();
            double tgatepass= Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(gatepass)", "")) ? 0.00 : dt.Compute("Sum(gatepass)", "")));
            double tactpsent= Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(acpresent)", "")) ? 0.00 : dt.Compute("Sum(acpresent)", "")));
            double tactman= Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(actman)", "")) ? 0.00 : dt.Compute("Sum(actman)", "")));
            DataRow[] dr1 = tbl1.Select("linecode like 'BBBB%'");
            dr1[0]["gatepass"]=tgatepass;
            dr1[0]["acpresent"]=tactpsent;
            dr1[0]["actman"]=tactman;

            ViewState["tbldatsum"] = tbl1;
            this.Data_bind();

        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string POSTEDBYID = hst["usrid"].ToString();
            string POSTRMID = hst["compname"].ToString();
            string POSTSESON = hst["session"].ToString();
            string POSTEDDAT = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string dayid = Convert.ToDateTime(this.txtDate.Text).ToString("yyyyMMdd");

            this.Session_Update();

            DataTable dt1 = (DataTable)ViewState["tbldatsum"];

            DataView dv1 = dt1.Copy().DefaultView;
            dv1.RowFilter = ("depcode like '94%'");
            DataTable tbl1 = dv1.ToTable();

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string seccod = tbl1.Rows[i]["seccod"].ToString();
                string linecode = tbl1.Rows[i]["linecode"].ToString();
                string gatepass = tbl1.Rows[i]["gatepass"].ToString();
                string psent = tbl1.Rows[i]["psent"].ToString();

                bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPDATEMANATTN", seccod, linecode, dayid, gatepass, psent, POSTRMID, POSTEDBYID, POSTSESON, POSTEDDAT, "", "", "");


                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);

                    
                    return;
                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);

            
        }


        private void FooterCalculationCat()
        {
            DataTable dt1 = (DataTable)ViewState["tbldatsum"];

            DataView dv1 = dt1.Copy().DefaultView;
            dv1.RowFilter = ("linecode not  like 'AAAA%' and linecode not  like 'BBBB%'");
            DataTable dt = dv1.ToTable();

            if (dt.Rows.Count == 0)
                return;
            //((Label)this.gvAttnCatSummary.FooterRow.FindControl("lblftacpresent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(acpresent)", "")) ? 0.00
            //    : dt.Compute("Sum(acpresent)", ""))).ToString("#,##0.00;(#,##0.00);  ");
            //((Label)this.gvAttnCatSummary.FooterRow.FindControl("lblftactman")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(actman)", "")) ? 0.00
            //    : dt.Compute("Sum(actman)", ""))).ToString("#,##0.00;(#,##0.00);  ");


            //((Label)this.gvAttnCatSummary.FooterRow.FindControl("lblftgatepass")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(gatepass)", "")) ? 0.00
            //    : dt.Compute("Sum(gatepass)", ""))).ToString("#,##0.00;(#,##0.00);  ");

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvAttnCatSummary.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            //((HyperLink)this.gvAttnCatSummary.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = true;

            Session["Report1"] = gvAttnCatSummary;
            ((HyperLink)this.gvAttnCatSummary.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

    }
}