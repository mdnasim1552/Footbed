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

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class RptEmpPromotionLetter : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Increment & Promotion Letter";
                GetWorkStation();
                GetAllOrganogramList();
                this.GetJobLocation();
                this.GetEmpLine();
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        
        private void GetEmpName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string linecode = ((this.ddlEmpLine.SelectedValue.ToString() == "00000") ? "" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string jobloc = ((this.ddlJobLocation.SelectedValue.ToString() == "00000") ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string frmdate = this.txtfromdate.Text;
            string todate = this.txttodate.Text;
            string rpttype = this.ddlRptType.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "INCREMENT_PROMOTED_EMPLOYEE", null,null,null, frmdate, todate, emptype, 
                division, deptid, section, linecode, jobloc, rpttype, userid, "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }

            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds3.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();

        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        private void Data_Bind()
        {
 
        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
             this.GetEmpName();
        }       
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_Bind();
        }
        private void GetJobLocation()
        {

            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }
        private void GetEmpLine()
        {
            string comcod = GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlEmpLine.DataTextField = "hrgdesc";
            this.ddlEmpLine.DataValueField = "hrgcod";
            this.ddlEmpLine.DataSource = ds3;
            this.ddlEmpLine.DataBind();
            this.ddlEmpLine.SelectedValue = "00000";

            ViewState["tbllineddl"] = ds3.Tables[0];
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            Session["hrcompnameadd"] = lst;
            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetComeCode();
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

        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetComeCode();
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

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetComeCode();
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
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string GMSign = new Uri(Server.MapPath(@"~\Image\GMSign.png")).AbsoluteUri;
            string frmdate = this.txtfromdate.Text;
            string todate = this.txttodate.Text;
            string empidMulti = "";
            foreach (ListItem item in ddlEmpNameAllInfo.Items)
            {
                if (item.Selected)
                {
                    empidMulti += item.Value;
                }
            }

            DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "INC_PROM_EMPLOYEE_DETAILS", null, null, null, empidMulti, frmdate, todate, "", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }

            string issueDate = this.txtIssueDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssueDate.Text).ToString("dd/MM/yyyy");
            string effectDate = this.txtEffectDate.Text == "" ? "" : Convert.ToDateTime(this.txtEffectDate.Text).ToString("dd/MM/yyyy");
            var lst1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeePromotionInfo>();     
            LocalReport Rpt1 = new LocalReport();
            string rptFormat = this.ddlRptFormat.SelectedValue.ToString();
            string reportTye = GetReportType();

            if (reportTye == "Increment" && (rptFormat == "01" || rptFormat == "02"))
            {
              
                for (var item = 0; lst1.Count > item; item++)
                {
                    lst1[item].Gross = ASTUtility.Trans(lst1[item].prevgssal, 2);
                    lst1[item].strGross = ASTUtility.Trans(lst1[item].curgssal, 2);
                    lst1[item].strIncAm = ASTUtility.Trans(lst1[item].incamt, 2);
                }

                string currntdate = System.DateTime.Now.ToString("dd/MM/yyyy");
                string currntyear = System.DateTime.Now.ToString("yyyy");
                Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpIncrletter", lst1, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("currntdate", currntdate));
                Rpt1.SetParameters(new ReportParameter("currntyear", currntyear));
                Rpt1.SetParameters(new ReportParameter("refno", ""));
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            }
            else if (reportTye == "Increment" && rptFormat == "03")
            {
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpIncrLetterBan", lst1, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("issueDate", issueDate));
                Rpt1.SetParameters(new ReportParameter("effectDate", effectDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("DGMSign", GMSign));
                Rpt1.SetParameters(new ReportParameter("compSName", comcod == "5305" ? "এফবি" : "এফবিএফ"));
            }
            else if (reportTye == "Promotion" && (rptFormat == "01" || rptFormat == "02"))
            {
                string currntdate = System.DateTime.Now.ToString("dd/MM/yyyy");
                for (var item = 0; lst1.Count > item; item++)
                {
                    lst1[item].Gross = ASTUtility.Trans(lst1[item].prevgssal, 2);
                    lst1[item].strGross = ASTUtility.Trans(lst1[item].curgssal, 2);
                    lst1[item].strIncAm = ASTUtility.Trans(lst1[item].incamt, 2);
                }

                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpPromotionletter", lst1, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("currntdate", currntdate));
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            }
            else if (reportTye == "Promotion" && rptFormat == "03")
            {
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpPromLetterBan", lst1, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("issueDate", issueDate));
                Rpt1.SetParameters(new ReportParameter("effectDate", effectDate));
                Rpt1.SetParameters(new ReportParameter("DGMSign", GMSign));
                Rpt1.SetParameters(new ReportParameter("compSName", comcod == "5305" ? "এফবি" : "এফবিএফ"));

            }
            else if (reportTye == "IncrProm" && rptFormat == "03")
            {
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpIncrPromLetterBan", lst1, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("issueDate", issueDate));
                Rpt1.SetParameters(new ReportParameter("effectDate", effectDate));
                Rpt1.SetParameters(new ReportParameter("DGMSign", GMSign));
                Rpt1.SetParameters(new ReportParameter("compSName", comcod == "5305" ? "এফবি" : "এফবিএফ"));

            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private string GetReportType()
        {
            string Type = "";
            string rptType = this.ddlRptType.SelectedValue.ToString();
            switch (rptType)
            {
                case "01":
                    Type = "Increment";
                    break;
                case "02":
                    Type = "Promotion";
                    break;
                case "03":
                    Type = "IncrProm";
                    break;    
                default:
                    Type = "IncrProm";
                    break;
            }
            return Type;
        }
    }
}