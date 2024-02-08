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
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class RptMonAttendanceRND : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                //this.SectionName();
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.rbtnAtten.SelectedIndex = 0;
                this.BtnChckResign.Visible = (this.rbtnAtten.SelectedIndex == 0);
                GetJobLocation();
                GetWorkStation();
                GetAllOrganogramList();
                this.GetLineddl();

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

        private void GetLineddl()
        {
            string comcod = GetComCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            //this.ddlempline.SelectedIndex = 1;
            ViewState["tbllineddl"] = ds3.Tables[0];
        }

        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            //
        }
        protected void imgbtnEmpName_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            //
        }


        protected void rbtnAtten_SelectedIndexChanged(object sender, EventArgs e)
        {                        
            this.BtnChckResign.Visible = true;
            this.lblfrmdate.Text = "From:";
            this.lbltodate.Visible = true;
            this.txttodate.Visible = true;
            this.chkWithImage.Visible =  false;
            string rbtnindex = this.rbtnAtten.SelectedIndex.ToString();
       
            switch (rbtnindex)
            {

                case "0":
                    MultiView1.ActiveViewIndex = 0;
                    break;              
            }


        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string rbtnindex = this.rbtnAtten.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":
                    MultiView1.ActiveViewIndex = 0;
                    this.GetMonthlyAttendence();
                    break;

            }
        }

        private void GetMonthlyAttendence()
        {
            Session.Remove("tblattendane");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetComCode();
            string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%";
            string empline = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";

            string joblocation = this.ddlJobLocation.SelectedValue.ToString();
            string resign = (this.BtnChckResign.Checked == true) ? "RESIGN" : "ALL";
            string callType = "RPTEMPMONTHLYATTN02";
            switch (comcod)
            {
                // case "5301":
                case "5305": // Fb footwear
                case "5306": // Fb footwear
                    callType = "RPTEMPMONTHLYATTN03";
                    break;
                case "5301": // Edison Footwear
                    callType = "RPTEMPMONTHLYATTN02";
                    break;
            }


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", callType, frmdate, todate, Company, division, deptCode, section, joblocation, resign, "");



            if (ds1 == null)
                return;

            Session["tblattendane"] = ds1.Tables[0];
            ds1.Dispose();
            this.gridData_bind();


        }

        private void gridData_bind()
        {

            string rbtnindex = this.rbtnAtten.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":
                    this.gvmonthlyattndc.DataSource = (DataTable)Session["tblattendane"];
                    this.gvmonthlyattndc.DataBind();
                    break;

            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string rbtnindex = this.rbtnAtten.SelectedIndex.ToString();
            switch (rbtnindex)
            {

                case "0":
                    this.PrintMonthlyAttn();
                    break;
            
            }
        }

        private void PrintMonthlyAttn()
        {

            string comcod = this.GetComCode();
            switch (comcod)
            {                 // Rupayan Group

                case "5301": // Edison Footwear
                    this.PrintMonAttendance01();
                    break;

                case "5305": // Fb Footwear
                case "5306": // Fb Footwear
                    this.PrintMonAttendance03();
                    break;

                default:
                    //this.PrintMonAttendance02();
                    this.PrintMonAttendance01();
                    break;
            }
        }

        private void PrintMonAttendance01()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            var topTitle = this.ddlWstation.SelectedItem.ToString();
            DataTable dt = (DataTable)Session["tblattendane"];
            LocalReport rpt1 = new LocalReport();
            var list1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendence>();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMonAttendance", list1, null, null);
            rpt1.SetParameters(new ReportParameter("empname", "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy") + " ( " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM") + " to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM") + ")"));
            rpt1.SetParameters(new ReportParameter("section", topTitle));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Attendance Sheet"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));



            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";










        }

        private void PrintMonAttendance03()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetComCode();
            string Emptype = this.ddlWstation.SelectedItem.ToString();
            var topTitle = this.ddlWstation.SelectedItem.ToString();
            DataTable dt = (DataTable)Session["tblattendane"];
            LocalReport rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendenceFB>();
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMonAttendanceFB", list, null, null);
                    rpt1.SetParameters(new ReportParameter("txtDate", "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy") + " ( " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM") + " to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM") + ")"));
                    rpt1.SetParameters(new ReportParameter("compName", comnam));
                    rpt1.SetParameters(new ReportParameter("Emptype", Emptype));
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Attendance Sheet"));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
                    break;

                default:
                    var list1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendence>();
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMonAttendance", list1, null, null);
                    rpt1.SetParameters(new ReportParameter("empname", "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy") + " ( " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM") + " to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM") + ")"));
                    rpt1.SetParameters(new ReportParameter("section", topTitle));
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Attendance Sheet"));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
                    break;
            }

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsent> HiddenSameData(List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsent> lst)
        {
            if (lst.Count == 0)
                return lst;

            int i = 0;
            string pactcode = "";
            foreach (SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsent c1 in lst)
            {
                if (i == 0)
                {
                    pactcode = c1.pactcode;
                    i++;
                    continue;
                }
                else if (c1.pactcode == pactcode)
                {
                    c1.department = "";
                    //c1.designation = "";

                }
                pactcode = c1.pactcode;

            }
            return lst;
        }
 
        public void GetAllOrganogramList()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
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

            this.ddlWstation_SelectedIndexChanged(null, null);

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
        protected void ddlJobLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
   
        protected void gvmonthlyattndc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvmonthlyattndc.PageIndex = e.NewPageIndex;
            this.gridData_bind();
        }

        protected void ddlEmpName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void BtnChckResign_CheckedChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
    }
}