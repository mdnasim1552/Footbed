using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY;
using CrystalDecisions.CrystalReports.Engine;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class RptEmpPunch : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy"); 
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Punch Missing";
                this.rbtnMissPunch.SelectedIndex = 1;
                GetJobLocation();
                GetWorkStation();
                GetAllOrganogramList();
                this.GetLineDDL();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent eventPFL-000020660
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void rbtnMissPunch_SelectedIndexChanged(object sender, EventArgs e)
        {

            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":
                    MultiView1.ActiveViewIndex = 0;
                    break;

                case "1":
                    MultiView1.ActiveViewIndex = 1;
                    break;

                case "2":
                    MultiView1.ActiveViewIndex = 2;
                    break;
            }
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":
                    MultiView1.ActiveViewIndex = 0;
                    this.GetMissedInPunch();
                    break;

                case "1":
                    MultiView1.ActiveViewIndex = 1;
                    this.GetMissedOutPunch();
                    break;

                case "2":
                    MultiView1.ActiveViewIndex = 2;
                    this.GetDoubtfulPunch();
                    break;
            }        
        }

        private void GetMissedInPunch()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
               
                string usrid = hst["usrid"].ToString();
                string comcod = GetComCode();
                //string ddlEmpName = this.ddlEmpName.SelectedValue.ToString() == "" ? "%" : this.ddlEmpName.SelectedValue.ToString() + "%";
                string ddlEmpName = "%";
                string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
                //string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%";

                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                
                string AllBranch = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "AllGroup" : "";

                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPT_MISSED_IN_PUNCH",null,null,null, txtfromdate, txttodate, ddlEmpName, Company, deptCode, division, section, joblocation, line, AllBranch, usrid);
                if (ds1 == null)
                    return;

                ViewState["tblMissInPunch"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = ex.ToString();
            }
        }
        private void GetMissedOutPunch()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string usrid = hst["usrid"].ToString();
                string comcod = GetComCode();
                //string ddlEmpName = this.ddlEmpName.SelectedValue.ToString() == "" ? "%" : this.ddlEmpName.SelectedValue.ToString() + "%";
                string ddlEmpName = "%";
                string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
               // string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%";
                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line= ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                string AllBranch = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "AllGroup" : "";
                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPT_MISSED_OUT_PUNCH", null, null, null, txtfromdate, txttodate, ddlEmpName, Company, deptCode, division, section, joblocation, line, AllBranch, usrid);
                if (ds1 == null)
                    return;

                ViewState["tblMissOutPunch"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = ex.ToString();
            }
        }
        private void GetDoubtfulPunch()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComCode();
                string usrid = hst["usrid"].ToString();
                //string ddlEmpName = this.ddlEmpName.SelectedValue.ToString() == "" ? "%" : this.ddlEmpName.SelectedValue.ToString() + "%";
                string ddlEmpName = "%";
                string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%";
                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPT_DOUBTFUL_PUNCH", txtfromdate, txttodate, ddlEmpName, Company, deptCode, division, section, joblocation, line);

                string AllBranch = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "AllGroup" : "";
                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPT_DOUBTFUL_PUNCH", null, null, null, txtfromdate, txttodate, ddlEmpName, Company, deptCode, division, section, joblocation, line, AllBranch, usrid);

                if (ds1 == null)
                    return;

                ViewState["tblDoubtfulPunch"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = ex.ToString();
            }
        }
        private void GetLineDDL()
        {
            string comcod = GetComCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue="00000";
            ViewState["tbllineddl"] = ds3.Tables[0];
        }
        private void GetJobLocation()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string jobLocCode = "87";
            var lst = getlist.GetCommonHRgcod(comcod, jobLocCode);

            this.ddlJobLocation.DataTextField = "hrgdesc";
            this.ddlJobLocation.DataValueField = "hrgcod";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
            this.ddlJobLocation.SelectedValue = "87002";

        }
        private void GetWorkStation()
        {

            string comcod = GetComCode();
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
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetDivision()
        {
            try
            {
                string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
                string comcod = GetComCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

                if (lst == null)
                    return;
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
            catch (Exception ex)
            {

            }
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetComCode();
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
        private void GetEmpName()
        {
            string comcod = this.GetComCode();
            string company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string deptcode = (ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string dtcode = (ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (listProject.SelectedValue.ToString() == "000000000000") ? "%" : listProject.SelectedValue.ToString() + "%";
            string txtSEmployee = "%" + this.txtSrcEmpName.Text.Trim() + "%";
            string resignstatus = (this.BtnChckResign.Checked == true) ? "RESIGN" : "ALL";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPNAME", company, dtcode, txtSEmployee, section, deptcode, resignstatus, "", "", "");
            if (ds3 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);

            this.listProject.DataTextField = "actdesc";
            this.listProject.DataValueField = "actcode";
            this.listProject.DataSource = lst1;
            this.listProject.DataBind();
            this.listProject.SelectedValue = "000000000000";

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblMissInPunch"];
            DataTable dt1 = (DataTable)ViewState["tblMissOutPunch"];
            DataTable dt2 = (DataTable)ViewState["tblDoubtfulPunch"];
            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":
                    this.gvMissInPunch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMissInPunch.DataSource = dt;
                    this.gvMissInPunch.DataBind();
                    break;

                case "1":
                    this.gvMissOutPunch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMissOutPunch.DataSource = dt1;
                    this.gvMissOutPunch.DataBind();
                    break;

                case "2":
                    this.gvDoubtfulPunch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvDoubtfulPunch.DataSource = dt2;
                    this.gvDoubtfulPunch.DataBind();
                    break;
            }
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {

                case "0":
                    this.PrintMissInPunch();
                    break;

                case "1":
                    this.PrintMissOutPunch();
                    break;

                case "2":
                    this.PrintDoubtfulPunch();
                    break;
            }
        }

        private void PrintMissInPunch()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string compName = hst["comnam"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comaddf"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rptDt = " For The " + txtfromdate;
            DataTable dt = (DataTable)ViewState["tblMissInPunch"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptEmpMissPunch>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMissInPunch", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", compName));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", rptDt));            
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Missed In Punch"));
            Rpt1.SetParameters(new ReportParameter("txtFooter", ASTUtility.Concat(compName, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintMissOutPunch()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string username = hst["username"].ToString();
            string compName = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rptDt = " For The " + txtfromdate;           
            DataTable dt = (DataTable)ViewState["tblMissOutPunch"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptEmpMissPunch>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMissOutPunch", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", compName));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", rptDt));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Missed Out Punch"));
            Rpt1.SetParameters(new ReportParameter("txtFooter", ASTUtility.Concat(compName, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintDoubtfulPunch()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string compName = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rptDt = " For The " + txtfromdate;
            DataTable dt = (DataTable)ViewState["tblDoubtfulPunch"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptEmpMissPunch>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDoubtfulPunch", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", compName));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", rptDt));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Doubtful Punch"));
            Rpt1.SetParameters(new ReportParameter("txtFooter", ASTUtility.Concat(compName, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }     
        protected void gvMissOutPunch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMissOutPunch.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void ddlJobLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void imgbtnEmpName_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnChckResign_CheckedChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        protected void gvMissInPunch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMissInPunch.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvDoubtfulPunch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDoubtfulPunch.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}