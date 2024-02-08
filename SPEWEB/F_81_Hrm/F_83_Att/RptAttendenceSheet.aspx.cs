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
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class RptAttendenceSheet : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess HRData = new ProcessAccess();
        string empTypeMulti = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
               
                ((Label)this.Master.FindControl("lblTitle")).Text = "Attendance Information";
                this.GetVisibility();
                this.GetJobLocation();
                this.GetWorkStation();
                this.GetDivision();
                this.GetDeptList();
                this.GetSectionList();
                this.GetLineddl();

            }
        }

        private void GetVisibility()
        {
            this.lblfrmdate.Text = "From";
            this.divToDate.Visible = true;
            this.divResign.Visible = true;
            this.divEmpName.Visible = true;
            this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            //Audit User DDL Visibility
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            switch (userid)
            {                
                case "5305134"://FB
                case "5305135"://FB_admin
                case "5305136"://Administrator
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("0"));
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("2"));
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("3"));
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("4"));
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("5"));
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("6"));
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("8"));
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("12"));
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("13"));
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("14"));
                    this.ddlReport.Items.Remove(ddlReport.Items.FindByValue("17"));
                    break;
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
            string company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string txtSEmployee = "%";
            string resignstatus = (this.BtnChckResign.Checked == true) ? "RESIGN" : "ALL";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPNAME", company, deptCode, txtSEmployee, section, division, resignstatus, "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
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
            this.ddlempline.SelectedValue = "00000";
            ds3.Dispose();
            // ViewState["tbllineddl"] = ds3.Tables[0];
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


        protected void ddlReport_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.divEmpName.Visible = (ddlReport.SelectedValue == "0" || ddlReport.SelectedValue == "2" || ddlReport.SelectedValue == "5" || ddlReport.SelectedValue == "6" || ddlReport.SelectedValue == "8");
            this.divResign.Visible = (ddlReport.SelectedValue == "0" || ddlReport.SelectedValue == "3" || ddlReport.SelectedValue == "11" || ddlReport.SelectedValue == "17");

            this.lblfrmdate.Text = ((ddlReport.SelectedValue == "1" || ddlReport.SelectedValue == "5" || ddlReport.SelectedValue == "9" || ddlReport.SelectedValue == "10" || ddlReport.SelectedValue == "11"
                || ddlReport.SelectedValue == "14" || ddlReport.SelectedValue == "18") ? "Date" : "From");

            this.txtfromdate.Text = ((ddlReport.SelectedValue == "1" || ddlReport.SelectedValue == "9" || ddlReport.SelectedValue == "10" || ddlReport.SelectedValue == "11"
                || ddlReport.SelectedValue == "18") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : this.txtfromdate.Text.Trim());

            this.divToDate.Visible = (ddlReport.SelectedValue == "0") || (ddlReport.SelectedValue == "2") || (ddlReport.SelectedValue == "3") || (ddlReport.SelectedValue == "4")
                || (ddlReport.SelectedValue == "6") || (ddlReport.SelectedValue == "7") || (ddlReport.SelectedValue == "8") || (ddlReport.SelectedValue == "12") || (ddlReport.SelectedValue == "13"
                || (ddlReport.SelectedValue == "17"));

            this.divImage.Visible = (this.ddlReport.SelectedValue == "11") ? true : false;
            this.chkWithoutOT.Visible = (ddlReport.SelectedValue == "3") ? true : false;
            this.divChkInactPunch.Visible = (ddlReport.SelectedValue == "0") ? true : false;

            //Date Time
            string report = this.ddlReport.SelectedValue.ToString();
            switch (report)
            {
                case "2":
                case "3":
                case "4":
                case "12":
                case "17":
                    this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
                default:
                    break;
            }          

            switch (report)
            {

                case "0":
                    MultiView1.ActiveViewIndex = 0;
                    this.divChkPrsntEmp.Visible = false;
                    break;
                case "1":// Daily Attendance
                    MultiView1.ActiveViewIndex = 1;
                    this.divChkPrsntEmp.Visible = true;
                    break;
                case "2":
                    MultiView1.ActiveViewIndex = 2;
                    this.divChkPrsntEmp.Visible = false;
                    break;
                case "3":
                    MultiView1.ActiveViewIndex = 3;
                    this.chkWithoutOT.Visible = true;
                    this.divChkPrsntEmp.Visible = false;
                    break;

                case "4":
                    MultiView1.ActiveViewIndex = 4;
                    this.divChkPrsntEmp.Visible = false;
                    break;

                case "5":
                    MultiView1.ActiveViewIndex = 5;
                    this.divChkPrsntEmp.Visible = false;
                    break;

                case "6":
                    MultiView1.ActiveViewIndex = 6;
                    this.divChkPrsntEmp.Visible = false;
                    break;

                case "8":
                    MultiView1.ActiveViewIndex = 8;
                    this.divChkPrsntEmp.Visible = false;
                    break;
                case "9":
                    MultiView1.ActiveViewIndex = 9;
                    this.divChkPrsntEmp.Visible = false;

                    break;
                case "10":
                    this.divChkPrsntEmp.Visible = false;
                    MultiView1.ActiveViewIndex = 10;

                    break;
                case "11"://Daily Absent
                    this.divChkPrsntEmp.Visible = false;
                    MultiView1.ActiveViewIndex = 11;

                    break;
                case "12":
                    MultiView1.ActiveViewIndex = 12;
                    this.divChkPrsntEmp.Visible = false;
                    break;
                case "13":
                    MultiView1.ActiveViewIndex = 13;
                    this.divChkPrsntEmp.Visible = false;
                    break;
                case "14":
                    MultiView1.ActiveViewIndex = 14;
                    this.divChkPrsntEmp.Visible = false;
                    break;
                case "17"://Monthly Absent
                    MultiView1.ActiveViewIndex = 11;
                    this.divChkPrsntEmp.Visible = false;
                    break;
                case "18"://Daily Present
                    MultiView1.ActiveViewIndex = 17;
                    this.divChkPrsntEmp.Visible = false;
                    break;
            }


        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.LinkButtonExportToExcel.Visible = false;
            string report = this.ddlReport.SelectedValue.ToString();
            switch (report)
            {
                case "0":
                    MultiView1.ActiveViewIndex = 0;
                    this.GetAttendncLogData();

                    break;
                case "1":// Daily Attendance 
                    MultiView1.ActiveViewIndex = 1;
                    this.GetDailyAttendence();
                    break;
                case "2":
                    MultiView1.ActiveViewIndex = 2;
                    this.GetEmployeeStatus();
                    break;
                case "3":
                    MultiView1.ActiveViewIndex = 3;
                    this.GetMonthlyAttendence();
                    break;

                case "4":
                    MultiView1.ActiveViewIndex = 4;
                    this.GetMonthlyLateAtten();
                    break;

                case "5":
                    MultiView1.ActiveViewIndex = 5;
                    this.GetEmpStatusLate();
                    break;

                case "6":
                    MultiView1.ActiveViewIndex = 6;
                    this.GetEmpStatusEarly();
                    break;

                case "7":
                    MultiView1.ActiveViewIndex = 7;
                    this.GetMonthlyOvertime();
                    break;
                case "8":
                    MultiView1.ActiveViewIndex = 8;
                    this.GetShiptingData();
                    break;
                case "9"://Daily Attendance Summary
                    MultiView1.ActiveViewIndex = 9;
                    this.GetDailyAttenSummary();
                    break;
                case "10"://Daily Late
                    MultiView1.ActiveViewIndex = 10;
                    this.GetLateAttData();
                    break;
                case "11"://Daily Absent
                    this.LinkButtonExportToExcel.Visible = true;
                    MultiView1.ActiveViewIndex = 11;
                    this.GetDailyAbsentData();
                    break;
                case "12":
                    MultiView1.ActiveViewIndex = 12;
                    this.GetAttenApprovalData();
                    break;
                case "13":
                    MultiView1.ActiveViewIndex = 13;
                    this.GetShiffingAll();
                    break;
                case "14":
                    MultiView1.ActiveViewIndex = 14;
                    this.GetAllMissingEmp();
                    break;

                case "15":
                    MultiView1.ActiveViewIndex = 15;
                    this.GetJobCard();
                    break;
                case "17"://Monthly Absent
                    MultiView1.ActiveViewIndex = 11;
                    this.GetDailyAbsentData();
                    break;

                case "18"://Daily Present
                    MultiView1.ActiveViewIndex = 17;
                    this.GetDailyPresent();
                    break;
            }
        }
        private void GetMonthlyOvertime()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string joblocation = this.ddlJobLocation.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPMONTHLYATTN02", frmdate, todate, deptCode, Company, section, division, joblocation, "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                return;
            }

            this.gvmonthlyovertime.DataSource = ds1.Tables[0];
            this.gvmonthlyovertime.DataBind();

        }

        private void GetEmpStatusEarly()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds6 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPSTATUSEARLY", frmdate, todate, empid, "", "", "", "", "", "");
            if (ds6 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                return;
            }

            this.gvempstatusearly.DataSource = ds6.Tables[0];
            this.gvempstatusearly.DataBind();
        }
        private void GetEmpStatusLate()
        {
            string comcod = this.GetComCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPLATESTATUS", frmdate, todate, empid, "", "", "", "", "", "");
            if (ds5 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                return;
            }
            this.gvempstatuslate.DataSource = ds5.Tables[0];
            this.gvempstatuslate.DataBind();
        }

        private void GetMonthlyLateAtten()
        {
            Session.Remove("tblattendane");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComCode();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPMONTHLYLATEATTN", frmdate, todate, empTypeMulti, division, deptCode, section, joblocation, userid, "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                return;
            }

            Session["tblattendane"] = ds1.Tables[0];
            ds1.Dispose();
            this.Data_bind();
        }

        private void GetMonthlyAttendence()
        {
            Session.Remove("tblattendane");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComCode();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string resign = (this.BtnChckResign.Checked == true) ? "RESIGN" : "ALL";
            string withoutOT = this.chkWithoutOT.Checked ? "withoutot" : "";
            string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
            string callType = "RPTEMPMONTHLYATTN02";
            switch (comcod)
            {
                // case "5301":
                case "5305": // Fb Footwear
                case "5306": // Footbed Footwear
                    callType = "RPTEMPMONTHLYATTN03";
                    break;
                case "5301": // Edison Footwear
                    callType = "RPTEMPMONTHLYATTN02";
                    break;
            }

            DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", callType, null, null, null, frmdate, todate, empTypeMulti, division, deptCode, section, joblocation, resign, userid, withoutOT, line);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                return;
            }

            Session["tblattendane"] = ds1.Tables[0];
            ds1.Dispose();
            this.Data_bind();

        }


        private void GetDailyAttendence()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetComCode();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "70%" : this.ddlempline.SelectedValue.ToString() + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string pOnly = this.chkpresentOnly.Checked == true ? "true" : "false";
            //Audit User
            string offOutTime = GetOffOutTime();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPDAILYATTN", frmdate, empTypeMulti, division, deptCode, section, line, joblocation, pOnly, usrid, offOutTime);
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                return;
            }

            ViewState["tbldalyatt"] = ds1.Tables[0];
            this.Data_bind();

        }

        private string GetOffOutTime()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string offoutTime = "";
            switch (userid)
            {
                //FB
                case "5305134":
                    offoutTime = "2Hrs";
                    break;
                //FB_admin
                case "5305135":
                    offoutTime = "3Hrs";
                    break;
                //Administrator
                case "5305136":
                    offoutTime = "WithoutFday";
                    break;
                default:
                    offoutTime = "All";
                    break;
            }
            return offoutTime;
        }

        private void Data_bind()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tbldalyatt"];
                DataTable dt1 = (DataTable)ViewState["empMissAttn"];

                var list = (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsent>)ViewState["tbldaiylatedata"];
                var listdp = (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyPresent>)ViewState["tbldailypresent"];

                string report = this.ddlReport.SelectedValue.ToString();
                switch (report)
                {
                    case "0":
                        break;
                    case "1":// Daily Attendance 
                        this.gvdailyattndc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvdailyattndc.DataSource = dt;
                        this.gvdailyattndc.DataBind();
                        break;
                    case "2":
                        break;
                    case "3":
                        this.gvmonthlyattndc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvmonthlyattndc.DataSource = (DataTable)Session["tblattendane"];
                        this.gvmonthlyattndc.DataBind();
                        break;

                    case "4":
                        this.gvmonthlylateattndc.DataSource = (DataTable)Session["tblattendane"];
                        this.gvmonthlylateattndc.DataBind();

                        DateTime datefrm, dateto;
                        int i;
                        for (i = 6; i < this.gvmonthlylateattndc.Columns.Count - 1; i++)
                        {
                            this.gvmonthlylateattndc.Columns[i].Visible = false;
                        }

                        datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                        dateto = Convert.ToDateTime(this.txttodate.Text.Trim());

                        for (i = 6; i < 37; i++)
                        {
                            if (datefrm > dateto)
                                break;
                            this.gvmonthlylateattndc.Columns[i].Visible = true;
                            this.gvmonthlylateattndc.Columns[i].HeaderText = datefrm.ToString("dd");
                            datefrm = datefrm.AddDays(1);

                        }
                        break;

                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                    case "10"://Daily Late
                        this.gvdailylateatt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvdailylateatt.DataSource = list;
                        this.gvdailylateatt.DataBind();
                        break;
                    case "11":
                        break;
                    case "12":
                        break;
                    case "13":
                        break;
                    case "14":
                        this.gvMissAttn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvMissAttn.DataSource = dt1;
                        this.gvMissAttn.DataBind();
                        break;
                    case "15":
                        break;
                    case "17":
                        break;
                    case "18"://Daily Present
                        this.gvDailyPresent.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvDailyPresent.DataSource = listdp;
                        this.gvDailyPresent.DataBind();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }

        }
        private void GetShiffingAll()
        {
            string comcod = this.GetComCode();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "ALLEMPSHIFTING", frmdate, todate, empTypeMulti, division, deptCode, section, "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            this.gvShiftingall.DataSource = ds1.Tables[0];
            this.gvShiftingall.DataBind();

        }

        private void GetAllMissingEmp()
        {
            string comcod = this.GetComCode();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string dayid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMMdd");
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string line = (this.ddlempline.SelectedValue.ToString() == "") ? "%" : this.ddlempline.SelectedValue.ToString();
            string type = "00000";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPMISSATTENDEES", deptCode, dayid, date, empTypeMulti, division, section, type, line, "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvMissAttn.DataSource = null;
                this.gvMissAttn.DataBind();
                return;
            }

            this.gvMissAttn.DataSource = ds1;
            this.gvMissAttn.DataBind();

            ViewState["empMissAttn"] = this.HiddenMisSameData(ds1.Tables[0]); ;
            this.Data_bind();
        }


        private void PrintShiffingAll()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string dateft = "Date: (From " + frmdate + " To " + todate + ")";
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "ALLEMPSHIFTING", frmdate, todate, Company, division, deptCode, section, "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendence>();



            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpShiftAll", lst, null, null);


            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("dateft", dateft));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Employee Shifting All"));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void GetShiptingData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ddlEmpName = this.ddlEmpName.SelectedValue.ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMMdd");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("yyyyMMdd");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPSHIFTING", ddlEmpName, txtfromdate, txttodate, "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }

            this.ShiftingData.DataSource = ds3;
            this.ShiftingData.DataBind();
            ViewState["Attan"] = ds3;
        }


        private void GetDailyPresent()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMMdd");
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string linecode = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
            string offOutTime = GetOffOutTime();
            DataSet ds = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDAILYPRESENT", null, null, null, txtfromdate, offOutTime, deptCode, section, "",
                division, empTypeMulti, joblocation, usrid, linecode);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            var list = ds.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyPresent>();
            ViewState["tbldailypresent"] = list;
            this.Data_bind();

        }



        private void GetLateAttData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMMdd");
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDAILYLATEEMPPERWISE", null, null, null, txtfromdate, "", deptCode, section, "Late", division, empTypeMulti, joblocation, "", usrid);
            if (ds == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }
            var list = ds.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsent>();
            ViewState["tbldaiylatedata"] = list;
            this.Data_bind();

        }
        private string AbsentCallType()
        {
            string CallType = "";
            string comcod = this.GetComCode();
            switch (comcod)
            {

                case "5305":
                case "5306":
                    CallType = this.ddlReport.SelectedValue == "17" ? "GETMONTHLYABSENTFB" : "GETDAILYLATEANDABSENTFB";//17:Monthly Absent
                    break;
                default:
                    CallType = "GETDAILYLATEANDABSENT";
                    break;


            }

            return CallType;


        }
        private void GetDailyAbsentData()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string usrid = hst["usrid"].ToString();
                string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMMdd");
                foreach (ListItem items in ddlWstation.Items)
                {
                    if (items.Selected)
                    {
                        empTypeMulti += items.Value;
                    }
                }
                string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
                string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
                string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
                string todate = this.ddlReport.SelectedValue == "17" ? Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("yyyyMMdd") : "";//17:Monthly Absent
                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                string Calltype = this.AbsentCallType();
                string resgin = (this.BtnChckResign.Checked) ? "R" : "";
                DataSet ds = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", Calltype, null, null, null, txtfromdate, todate, deptCode, section, "Absent",
                    division, empTypeMulti, joblocation, line, "", usrid, resgin);
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    this.gvdailyabsent.DataSource = null;
                    this.gvdailyabsent.DataBind();
                    return;
                }

                var list = ds.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsentFb>();
                //if (this.ddlReport.SelectedValue == "17")
                //{
                //    this.gvdailyabsent.Columns[10].Visible = true;
                //}
                //else
                //{
                //    this.gvdailyabsent.Columns[10].Visible = false;
                //}Z

                this.gvdailyabsent.DataSource = list;
                this.gvdailyabsent.DataBind();
                ViewState["tbldaiyabsentdata"] = ds.Tables[0];//tbldaiyabsentdata
                //this.ExportToExcelFile(ds.Tables[0]);
                //this.PopulateTemplateExcelFile(ds.Tables[1]);
                ViewState["ExportToExcelFile"] = ds.Tables[1];
                if (list.Count>0)
                {
                    Session["Report1"] = gvdailyabsent;
                    ((HyperLink)this.gvdailyabsent.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }
       
        protected void PopulateTemplateExcelFile(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)ViewState["ExportToExcelFile"];
            if (dataTable.Rows.Count == 0)
            {
                return;
            }
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyyMMddHHmmss");
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999); // Generates a number between 10000 and 99999
            string crm_file_name = formattedDateTime + randomNumber.ToString() + ".xlsx";


            string templateFilePath = Server.MapPath("~/Excel_Files/Template.xlsx"); // Path to the template Excel file
            string newFilePath = Server.MapPath("~/Excel_Files/"+ crm_file_name); // Change the file name as needed

            File.Copy(templateFilePath, newFilePath, true);

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(newFilePath, true))
            {
                SheetData sheetData = spreadsheetDocument.WorkbookPart.WorksheetParts.First().Worksheet.GetFirstChild<SheetData>();

                var rowsToDelete = sheetData.Elements<Row>().Skip(4).ToList();
                foreach (var row in rowsToDelete)
                {
                    row.Remove();
                }

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Row row = new Row();

                    foreach (var item in dataRow.ItemArray)
                    {
                        Cell cell = new Cell(new InlineString(new Text(item.ToString()))) { DataType = CellValues.InlineString };
                        row.Append(cell);
                    }

                    sheetData.Append(row);
                }

                spreadsheetDocument.WorkbookPart.WorksheetParts.First().Worksheet.Save();
            }

            string script = "window.open('" + ResolveUrl("~/Excel_Files/"+ crm_file_name) + "','_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "OpenWindow", script, true);

            System.Threading.Tasks.Task.Run(() =>
            {
                System.Threading.Thread.Sleep(5000); // Delay for 5 seconds
                File.Delete(newFilePath); // Delete the file
            });
        }

        private void GetAttenApprovalData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string jobloca = this.ddlJobLocation.SelectedValue.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETATTENDENCEAPPROVAL", txtfromdate, todate, empTypeMulti, division, deptCode, section, jobloca, "", "");
            if(ds == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvattenapproval.DataSource = null;
                this.gvattenapproval.DataBind();
            }

            var list = ds.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EclassAttApp>();
            this.gvattenapproval.DataSource = list;
            this.gvattenapproval.DataBind();
            ViewState["tblattenapproval"] = list;
        }
        private void GetAttendncLogData()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string ddlEmpName =  this.chkInactPunch.Checked ? "%" : this.ddlEmpName.SelectedValue.ToString();
                string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
                string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
                string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
                string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
                string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
                string chkInactPunch = this.chkInactPunch.Checked ? "InactPunch" : "";
                DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTENDENCELOG", txtfromdate, txttodate, ddlEmpName, Company, deptCode, division, section, chkInactPunch, "");
                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    this.Attendencelog.DataSource = null;
                    this.Attendencelog.DataBind();
                }

                this.Attendencelog.DataSource = ds;
                this.Attendencelog.DataBind();
                ViewState["Attanlogdta"] = ds;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }
        private void GetEmployeeStatus()
        {
            string comcod = this.GetComCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, "", "", "", "", "", "");
            if (ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvEmpstatus.DataSource = null;
                this.gvEmpstatus.DataBind();
            }

            ViewState["TblEmpStatus"] = ds4.Tables[0];
            this.gvEmpstatus.DataSource = ds4.Tables[0];
            this.gvEmpstatus.DataBind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string rbtnindex = this.ddlReport.SelectedValue.ToString();
            switch (rbtnindex)
            {

                case "0":
                    this.PritEmpAttndencLog();
                    break;
                case "1":
                    this.PritDailyEmpAttndenc();
                    break;
                case "2":
                    this.PrintEmpAttnIdWise();
                    break;
                case "3":
                    this.PrintMonthlyAttn();
                    break;


                case "4":
                    this.PrintMonthlyLateAtten();
                    break;


                case "5":
                    this.PrintEmpStatusLate();
                    break;

                case "6":
                    this.PrintEmpStatusEarly();
                    break;


                case "7":
                    this.PrintMonthlyOvertime();
                    break;
                case "8":
                    this.PrintShiftingData();
                    break;
                case "9":
                    this.PrintDailyAttsheet();
                    break;
                case "10":
                    this.PrintLateAttsheet();
                    break;
                case "11":
                    this.PrintDailyAbsent();
                    break;
                case "12":
                    this.PrintAttApproval();
                    break;
                case "13":
                    this.PrintShiffingAll();
                    break;
                case "14":
                    this.PrintAllMissingAttn();
                    break;
                case "15":
                    this.PrintJobCard();
                    break;
                case "17":
                    this.PrintMonthlyAbsent();
                    break;
                case "18":
                    this.PrintDailyPresent();
                    break;
            }
        }




        private void PritDailyEmpAttndenc()
        {
            string comcod = this.GetComCode();
            switch (comcod)
            {
                case "4305":// Rupayan

                    this.PrintDailyAttendance02();
                    break;

                case "5305":// FB Footwear
                case "5306":// FB Footwear

                    this.PrintDailyAttendance03();
                    break;

                default:
                    this.PrintDailyAttendance01();
                    // this.PrintDailyAttendance02();
                    break;
            }
        }

        private void GetDailyAttenSummary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string jobLocation = ((this.ddlJobLocation.SelectedValue.ToString() == "00000") ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptCode = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDAILYATTENDENCE", frmdate, deptCode, empTypeMulti, section, division, usrid, jobLocation, "", "");
            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            DataTable dt = ds3.Tables[0];
            ViewState["DailyAttSum"] = dt;
            ViewState["DailyAttSumDet"] = ds3.Tables[1];
            this.gvdailyattsum.DataSource = dt;
            this.gvdailyattsum.DataBind();

            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvdailyattsum.FooterRow.FindControl("gvfotdailsumttlPR")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(present)", "")) ?
                        0 : dt.Compute("sum(present)", ""))).ToString("#,##0;(#,##0);");

                ((Label)this.gvdailyattsum.FooterRow.FindControl("gvfotdailsleave")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(leave)", "")) ?
                        0 : dt.Compute("sum(leave)", ""))).ToString("#,##0;(#,##0);");

                ((Label)this.gvdailyattsum.FooterRow.FindControl("gvfotdabsent1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(absent1)", "")) ?
                        0 : dt.Compute("sum(absent1)", ""))).ToString("#,##0;(#,##0);");


                ((Label)this.gvdailyattsum.FooterRow.FindControl("gvfotholiday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(holiday)", "")) ?
                        0 : dt.Compute("sum(holiday)", ""))).ToString("#,##0;(#,##0);");

                ((Label)this.gvdailyattsum.FooterRow.FindControl("gvfotdailsumttl")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(total)", "")) ?
                        0 : dt.Compute("sum(total)", ""))).ToString("#,##0;(#,##0);");
            }

        }


        private void PrintDailyAttsheet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)ViewState["DailyAttSum"];
            DataTable dt1 = (DataTable)ViewState["DailyAttSumDet"];
            var lstitem = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DayAttnSumry>();
            var lstitem2 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyAttnSummary>();

            LocalReport rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5305":
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.DailyAttenSumryFB", lstitem, lstitem2, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Attendence Summary Report for Dated: " + frmdate));
                    break;

                case "5306":
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.DailyAttenSumryFootbed", lstitem, lstitem2, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Attendence Summary Report for Dated: " + frmdate));
                    break;

                default:
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.DailyAttenSumry", lstitem, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Daily Attendence Summary Sheet"));
                    break;
            }

            rpt1.SetParameters(new ReportParameter("reprtdate", frmdate));
            rpt1.SetParameters(new ReportParameter("DeptNam", ""));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            string comadd = hst["comadd1"].ToString();
            rpt1.SetParameters(new ReportParameter("comadd", comcod == "5305" ? "Uloshara, Kaliakoir, Gazipur." : comcod == "5306" ? "Uloshara, Kaliakoir, Gazipur." : comadd));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintDailyAttendance01()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string comadd = hst["comadd1"].ToString();
            string date = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd MMMM,yyyy");
            string foot = ASTUtility.Concat(compname, username, printdate);
            var topTitle = this.ddlWstation.SelectedItem.ToString();
            DataTable DT = (DataTable)ViewState["tbldalyatt"];
            var lstitem = DT.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpDayAttnSumry>();
            LocalReport rpt1 = new LocalReport();
            foreach (var item in lstitem)
            {
                item.intime = Convert.ToDateTime(item.intime).ToString("hh:mm tt");
                item.outtime = Convert.ToDateTime(item.outtime).ToString("hh:mm tt");
                item.offintime = Convert.ToDateTime(item.offintime).ToString("hh:mm tt");
                item.offouttime = Convert.ToDateTime(item.offouttime).ToString("hh:mm tt");
            }
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpDayAttnSumry", lstitem, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("RptTitle", "Daily Employee Attendance"));
            rpt1.SetParameters(new ReportParameter("compname", compname));
            rpt1.SetParameters(new ReportParameter("topTitle", topTitle));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("footer", foot));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region old
            ReportDocument rptcb1 = new RMGiRPT.R_81_Hrm.R_83_Att.RptDailyAllEmpAttn();    //RptDailyAllEmpAttn();
            TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = this.ddlWstation.SelectedItem.ToString();
            TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptdate.Text = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd MMMM,yyyy");
            TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptcb1.SetDataSource(DT);
            Session["Report1"] = rptcb1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #endregion
        }
        private void PrintDailyAttendance02()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetComCode();
            string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string PCompany = this.ddlWstation.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString() + "%";

            //string section = "";
            //if ((this.ddlDept.SelectedValue.ToString() != "000000000000"))
            //{
            //    string[] sec = this.DropCheck1.Text.Trim().Split(',');

            //    if (sec[0].Substring(0, 4) == "0000")
            //        section = "";
            //    else
            //        foreach (string s1 in sec)
            //            section = section + this.ddlDept.SelectedValue.ToString().Substring(0, 8) + s1.Substring(0, 4);

            //}

            string section = "";

            foreach (ListItem item in ddlSection.Items)
            {
                if (item.Selected)
                {
                    section += item.Value;
                }
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPDAILYATTN02", frmdate, deptid, Company, section, "", "", "", "", "");
            if (ds1 == null)
                return;
            //ReportDocument rptcb1 = new RMGiRPT.R_81_Hrm.R_83_Att.RptDailyAllEmpAttn02();
            //TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = PCompany;
            //TextObject txttotalemp = rptcb1.ReportDefinition.ReportObjects["txttotalemp"] as TextObject;
            //txttotalemp.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["temployee"]).ToString("#,##0;(#,##0); ");
            //TextObject txtabsent = rptcb1.ReportDefinition.ReportObjects["txtabsent"] as TextObject;
            //txtabsent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["absemp"]).ToString("#,##0;(#,##0); ");
            //TextObject txtleave = rptcb1.ReportDefinition.ReportObjects["txtleave"] as TextObject;
            //txtleave.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["leaveemp"]).ToString("#,##0;(#,##0); ");
            //TextObject txtpresent = rptcb1.ReportDefinition.ReportObjects["txtpresent"] as TextObject;
            //txtpresent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["presntemp"]).ToString("#,##0;(#,##0); ");
            //TextObject txtresign = rptcb1.ReportDefinition.ReportObjects["txtresign"] as TextObject;
            //txtresign.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["resignemp"]).ToString("#,##0;(#,##0); ");

            //TextObject txtlate5min = rptcb1.ReportDefinition.ReportObjects["txtlate5min"] as TextObject;
            //txtlate5min.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["lw5min"]).ToString("#,##0;(#,##0); ");
            //TextObject txtlate6to10min = rptcb1.ReportDefinition.ReportObjects["txtlate6to10min"] as TextObject;
            //txtlate6to10min.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["l6to10"]).ToString("#,##0;(#,##0); ");
            //TextObject txtlate11minon = rptcb1.ReportDefinition.ReportObjects["txtlate11minon"] as TextObject;
            //txtlate11minon.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["l11toup"]).ToString("#,##0;(#,##0); ");

            //TextObject txteleft = rptcb1.ReportDefinition.ReportObjects["txteleft"] as TextObject;
            //txteleft.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["eleft"]).ToString("#,##0;(#,##0); ");
            //TextObject txtoutw30mi = rptcb1.ReportDefinition.ReportObjects["txtoutw30mi"] as TextObject;
            //txtoutw30mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw30mi"]).ToString("#,##0;(#,##0); ");
            //TextObject txtoutw31to60mi = rptcb1.ReportDefinition.ReportObjects["txtoutw31to60mi"] as TextObject;
            //txtoutw31to60mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw31to60mi"]).ToString("#,##0;(#,##0); ");
            //TextObject txtoutw61to90mi = rptcb1.ReportDefinition.ReportObjects["txtoutw61to90mi"] as TextObject;
            //txtoutw61to90mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw61to90mi"]).ToString("#,##0;(#,##0); ");
            //TextObject txtoutw91toabove = rptcb1.ReportDefinition.ReportObjects["txtoutw91toabove"] as TextObject;
            //txtoutw91toabove.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw91toabove"]).ToString("#,##0;(#,##0); ");
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptdate.Text = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd MMMM,yyyy");
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(ds1.Tables[0]);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintDailyAttendance03()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string comadd = hst["comaddf"].ToString();
            string date = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd MMMM,yyyy");
            string foot = ASTUtility.Concat(compname, username, printdate);
            var topTitle = this.ddlWstation.SelectedItem.ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable DT = (DataTable)ViewState["tbldalyatt"];
            var lstitem = DT.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpDayAttnSumry03>();
            LocalReport rpt1 = new LocalReport();
            foreach (var item in lstitem)
            {
                if (item.status == "P" || item.status == "W/H")
                {
                    item.intime = Convert.ToDateTime(item.intime).ToString("hh:mm tt");
                    item.outtime = Convert.ToDateTime(item.outtime).ToString("hh:mm tt");
                    item.offintime = Convert.ToDateTime(item.offintime).ToString("hh:mm tt");
                    item.offouttime = Convert.ToDateTime(item.offouttime).ToString("hh:mm tt");
                    item.ovthour = Convert.ToString(item.ovthour);
                    item.late = Convert.ToString(item.late);

                }
                else
                {
                    item.intime = "";
                    item.outtime = "";
                    item.offintime = "";
                    item.offouttime = "";
                    item.ovthour = "";
                    item.late = "";
                }

            }
            int totalPresent = lstitem.Count(l => l.present == "P");
            int totalAbsent = lstitem.Count(l => l.absday == "AB"); ;
            //  int totalLeave = lstitem.Count(l => l.leave "L"); ;
            int totalMleave = lstitem.Count(l => l.mleave == "ML");
            int lineTotal = DT.Rows.Count;
            int othleave = lineTotal - totalPresent - totalAbsent - totalMleave;



            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAttSum", lstitem, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("RptTitle", "Daily Attendance Status"));
            rpt1.SetParameters(new ReportParameter("compname", comnam));
            rpt1.SetParameters(new ReportParameter("topTitle", topTitle));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("footer", foot));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("totalLeave", othleave.ToString()));
            rpt1.SetParameters(new ReportParameter("totalMleave", totalMleave.ToString()));
            rpt1.SetParameters(new ReportParameter("totalAbsent", totalAbsent.ToString()));
            rpt1.SetParameters(new ReportParameter("totalPresent", totalPresent.ToString()));
            rpt1.SetParameters(new ReportParameter("lineTotal", lineTotal.ToString()));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


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

            #region 



            DataTable dt = (DataTable)Session["tblattendane"];

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetComCode();
            string Emptype = this.ddlWstation.SelectedItem.ToString();
            var topTitle = this.ddlWstation.SelectedItem.ToString();



            //rpt1.SetParameters(new ReportParameter("txtDate", "Attendance Sheet for the month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy") + " ( " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM") + " to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM") + ")"));
            //rpt1.SetParameters(new ReportParameter("compName", comnam));
            //rpt1.SetParameters(new ReportParameter("Emptype", Emptype));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Attendance Sheet"));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));


            Int32 Minute = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(totalmin)", "")) ?
                           0 : dt.Compute("sum(totalmin)", "")));
            int txtHrsFrac = Convert.ToInt32(Minute / 60);
            double txtMinFrac = Minute % 60;
            double totalHrs = txtHrsFrac + txtMinFrac * 0.01;


            ReportDocument rptcb1 = new RMGiRPT.R_81_Hrm.R_83_Att.RptMonAttendanceFB();
            TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txttotalothours = rptcb1.ReportDefinition.ReportObjects["txttotalothours"] as TextObject;
            txttotalothours.Text = totalHrs.ToString("#,##0.00;(#,##0.00)");

            TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtMonth"] as TextObject;
            rptdate.Text = "Attendance Sheet for the month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy") + " ( " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM") + " to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM") + ")";

            //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());

            //for (int i = 1; i <= 31; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptcb1.ReportDefinition.ReportObjects["txtdate" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("dd");
            //    datefrm = datefrm.AddDays(1);
            //}


            TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptcb1.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptcb1.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptcb1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string comcod = this.GetComCode();
            //string Emptype = this.ddlWstation.SelectedItem.ToString();
            //var topTitle = this.ddlWstation.SelectedItem.ToString();
            //DataTable dt = (DataTable)Session["tblattendane"];
            //LocalReport rpt1 = new LocalReport();
            //string Reportpath = "";


            //switch (comcod)
            //{
            //    case "5305":
            //    case "5306":
            //        Reportpath = "~/Reports/RptMonAttendanceFB.rdlc";
            //        rpt1.ReportPath = Server.MapPath(Reportpath);
            //        rpt1.DataSources.Clear();
            //        var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendenceFB>();
            //        rpt1.DataSources.Add(new ReportDataSource("DataSet1", dt));
            //        // rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMonAttendanceFB", list, null, null);
            //        rpt1.SetParameters(new ReportParameter("txtDate", "Attendance Sheet for the month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy") + " ( " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM") + " to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM") + ")"));
            //        rpt1.SetParameters(new ReportParameter("compName", comnam));
            //        rpt1.SetParameters(new ReportParameter("Emptype", Emptype));
            //        rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Attendance Sheet"));
            //        rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            //        break;

            //    default:
            //        var list1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendence>();
            //        rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMonAttendance", list1, null, null);
            //        rpt1.SetParameters(new ReportParameter("empname", "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy") + " ( " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM") + " to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM") + ")"));
            //        rpt1.SetParameters(new ReportParameter("section", topTitle));
            //        rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Attendance Sheet"));
            //        rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            //        break;

            #endregion

        }



        private void PrintEmpAttnIdWise()
        {
            // IQBAL NAYAN
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");

            //string ddlEmpName = this.ddlEmpName.SelectedValue.ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMMdd");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("yyyyMMdd");
            string rptDt = "Date: From: " + txtfromdate + " To: " + txttodate;

            string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string PCompany = this.ddlWstation.SelectedItem.Text.Trim();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, "", "", "", "", "", "");


            DataTable dtdailyiemp = ds4.Tables[0];
            int sum = 0;
            string hour, minute;
            //for (int i = 0; i < dtdailyiemp.Rows.Count; i++)
            //{
            //    sum += Convert.ToInt32(dtdailyiemp.Rows[i]["actualattnminute"]);
            //}
            hour = Convert.ToInt32(sum / 60).ToString();
            minute = ASTUtility.Right((Convert.ToString("00" + (sum % 60))), 2);
            string TotalHour = hour + ":" + minute;

            //string empnam = ds.Tables[0].Rows[0]["empnam"].ToString();
            var list = ds4.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttnIdWise>();

            string idcardno = list[0].idcardno.ToString();
            string empdsg = list[0].empdsg.ToString();
            string empdept = list[0].empdept.ToString();
            string empnam = list[0].empnam.ToString();
            string joindate = Convert.ToDateTime(list[0].joindate).ToString("dd-MMM-yyyy");
            string stdtimein = Convert.ToDateTime(list[0].stdtimein).ToString("hh:mm tt");
            string stdtimeout = Convert.ToDateTime(list[0].stdtimeout).ToString("hh:mm tt");

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAttenEmp", list, null, null);

            string wday = Convert.ToDouble(ds4.Tables[1].Rows[0]["twrkday"]).ToString("#,##0;(#,##0); ");
            string laday = Convert.ToDouble(ds4.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");
            string leday = Convert.ToDouble(ds4.Tables[1].Rows[0]["tlvday"]).ToString("#,##0;(#,##0); ");
            string abday = Convert.ToDouble(ds4.Tables[1].Rows[0]["tabsday"]).ToString("#,##0;(#,##0); ");
            string hday = Convert.ToDouble(ds4.Tables[1].Rows[0]["thday"]).ToString("#,##0;(#,##0); ");

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("RptTitle", rptDt));
            rpt1.SetParameters(new ReportParameter("empname", empnam));
            rpt1.SetParameters(new ReportParameter("idcardno", idcardno));
            rpt1.SetParameters(new ReportParameter("empdsg", empdsg));
            rpt1.SetParameters(new ReportParameter("empdept", empdept));
            rpt1.SetParameters(new ReportParameter("joindate", joindate));
            rpt1.SetParameters(new ReportParameter("stdtimein", stdtimein));
            rpt1.SetParameters(new ReportParameter("stdtimeout", stdtimeout));

            rpt1.SetParameters(new ReportParameter("wday", wday));
            rpt1.SetParameters(new ReportParameter("laday", laday));
            rpt1.SetParameters(new ReportParameter("leday", leday));
            rpt1.SetParameters(new ReportParameter("abday", abday));
            rpt1.SetParameters(new ReportParameter("hday", hday));
            //rpt1.SetParameters(new ReportParameter("TotalHour", TotalHour));
            string comadd = hst["comadd1"].ToString();
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Individual Attendance Summary Report"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintMonthlyLateAtten()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comaddf"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblattendane"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.MonthlyLateAttdendace>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptHRMonthlyLateSum", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compName", comnam));

            DateTime datefrm, dateto;
            datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            dateto = Convert.ToDateTime(this.txttodate.Text.Trim());

            for (int i = 1; i <= 31; i++)
            {
                if (datefrm > dateto)
                    break;
                Rpt1.SetParameters(new ReportParameter("Day" + i, datefrm.ToString("dd")));
                datefrm = datefrm.AddDays(1);
            }
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Late Attendance Sheet for the month of " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintEmpStatusLate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComCode();
            string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string PCompany = this.ddlWstation.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPLATESTATUS", frmdate, todate, empid, "", "", "", "", "", "");

            var list = ds5.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpSatausLate>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.rptMonthyLateAttnEmp", list, null, null);
            Rpt1.EnableExternalImages = true;
            string comadd = hst["comadd1"].ToString();
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));

            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("compName", PCompany));
            Rpt1.SetParameters(new ReportParameter("txttolateday", (ds5.Tables[1].Rows.Count == 0) ? "0" : Convert.ToDouble(ds5.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Late Status"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //DataTable dtdailyiemp = ds5.Tables[0];
            ////int sum = 0;
            ////string hour, minute;
            ////for (int i = 0; i < dtdailyiemp.Rows.Count; i++)
            ////{
            ////    sum += Convert.ToInt32(dtdailyiemp.Rows[i]["actualattnminute"]);
            ////}
            ////hour = Convert.ToInt32(sum / 60).ToString();
            ////minute = ASTUtility.Right((Convert.ToString("00" + (sum % 60))), 2);
            ////string TotalHour = hour + ":" + minute;
            //ReportDocument rptTest = new RMGiRPT.R_81_Hrm.R_83_Att.rptMonthyLateAttnEmp();
            //rptTest.SetDataSource(ds5.Tables[0]);
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = PCompany;

            ////TextObject txttowrkday = rptTest.ReportDefinition.ReportObjects["txttowrkday"] as TextObject;
            ////txttowrkday.Text = Convert.ToDouble(ds5.Tables[1].Rows[0]["twrkday"]).ToString("#,##0;(#,##0); ");
            //TextObject txttolateday = rptTest.ReportDefinition.ReportObjects["txttolateday"] as TextObject;
            //txttolateday.Text = Convert.ToDouble(ds5.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");
            ////TextObject txttoleaveday = rptTest.ReportDefinition.ReportObjects["txttoleaveday"] as TextObject;
            ////txttoleaveday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tlvday"]).ToString("#,##0;(#,##0); ");
            ////TextObject txtoabsday = rptTest.ReportDefinition.ReportObjects["txtoabsday"] as TextObject;
            ////txtoabsday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tabsday"]).ToString("#,##0;(#,##0); ");
            ////TextObject txtohday = rptTest.ReportDefinition.ReportObjects["txtohday"] as TextObject;
            ////txtohday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["thday"]).ToString("#,##0;(#,##0); ");


            ////TextObject txtrptTotalHour = rptTest.ReportDefinition.ReportObjects["txtTHour"] as TextObject;
            ////txtrptTotalHour.Text = TotalHour;
            //TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Session["Report1"] = rptTest;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #endregion
        }

        private void PrintEmpStatusEarly()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComCode();
            string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string PCompany = this.ddlWstation.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds6 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPSTATUSEARLY", frmdate, todate, empid, "", "", "", "", "", "");
            var list = ds6.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpSatausLate>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.rptMonthyEarlyLeaveEmp", list, null, null);
            Rpt1.EnableExternalImages = true;
            string comadd = hst["comadd1"].ToString();
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));


            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("compName", PCompany));
            Rpt1.SetParameters(new ReportParameter("txttolateday", (ds6.Tables[1].Rows.Count == 0) ? "0" : Convert.ToDouble(ds6.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Early Leave Status"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //DataTable dtdailyiemp = ds6.Tables[0];
            ////int sum = 0;
            ////string hour, minute;
            ////for (int i = 0; i < dtdailyiemp.Rows.Count; i++)
            ////{
            ////    sum += Convert.ToInt32(dtdailyiemp.Rows[i]["actualattnminute"]);
            ////}
            ////hour = Convert.ToInt32(sum / 60).ToString();
            ////minute = ASTUtility.Right((Convert.ToString("00" + (sum % 60))), 2);
            ////string TotalHour = hour + ":" + minute;
            //ReportDocument rptTest = new RMGiRPT.R_81_Hrm.R_83_Att.rptMonthyEarlyLeaveEmp();
            //rptTest.SetDataSource(ds6.Tables[0]);
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = PCompany;

            ////TextObject txttowrkday = rptTest.ReportDefinition.ReportObjects["txttowrkday"] as TextObject;
            ////txttowrkday.Text = Convert.ToDouble(ds5.Tables[1].Rows[0]["twrkday"]).ToString("#,##0;(#,##0); ");
            //TextObject txttolateday = rptTest.ReportDefinition.ReportObjects["txttolateday"] as TextObject;
            //txttolateday.Text = Convert.ToDouble(ds6.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");
            ////TextObject txttoleaveday = rptTest.ReportDefinition.ReportObjects["txttoleaveday"] as TextObject;
            ////txttoleaveday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tlvday"]).ToString("#,##0;(#,##0); ");
            ////TextObject txtoabsday = rptTest.ReportDefinition.ReportObjects["txtoabsday"] as TextObject;
            ////txtoabsday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tabsday"]).ToString("#,##0;(#,##0); ");
            ////TextObject txtohday = rptTest.ReportDefinition.ReportObjects["txtohday"] as TextObject;
            ////txtohday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["thday"]).ToString("#,##0;(#,##0); ");


            ////TextObject txtrptTotalHour = rptTest.ReportDefinition.ReportObjects["txtTHour"] as TextObject;
            ////txtrptTotalHour.Text = TotalHour;
            //TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Session["Report1"] = rptTest;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }


        private void PrintMonthlyOvertime()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetComCode();
            string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string PCompany = this.ddlWstation.SelectedItem.Text.Trim();
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            string joblocation = this.ddlJobLocation.SelectedValue.ToString();
            var topTitle = this.ddlWstation.SelectedItem.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPMONTHLYATTN02", frmdate, todate, deptCode, Company, section, division, joblocation, "");

            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendence>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMonAttendance", list, null, null);
            Rpt1.SetParameters(new ReportParameter("empname", "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy") + " ( " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM") + " to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM") + ")"));
            Rpt1.SetParameters(new ReportParameter("section", topTitle));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Attendance Sheet"));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintShiftingData()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");

            string ddlEmpName = this.ddlEmpName.SelectedValue.ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMMdd");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("yyyyMMdd");
            string rptDt = "Date: From: " + txtfromdate + " To: " + txttodate;
            string empname = "";
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPSHIFTING", ddlEmpName, txtfromdate, txttodate, "", "", "", "", "", "");


            if (ds.Tables[0].Rows.Count != 0)
            {
                //empname= ds.Tables[0].Rows[0]["empname"].ToString();
            }

            var list = ds.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.Shifting01>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptAttnShifting", list, null, null);


            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            rpt1.SetParameters(new ReportParameter("EmpNam", " Employee Name: " + empname));
            string comadd = hst["comadd1"].ToString();
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Employee Shifting Information"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PritEmpAttndencLog()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            string rptDt = "Date( From: " + txtfromdate + " To: " + txttodate + " )";

            //string empid = this.ddlEmpName.SelectedValue.ToString();

            //DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTENDENCELOG", txtfromdate, txttodate, empid, "", "", "", "", "", "");


            DataSet ds = (DataSet)ViewState["Attanlogdta"];





            string depart = ds.Tables[0].Rows[0]["depname"].ToString();
            string designation = "Designation: " + ds.Tables[0].Rows[0]["desg"].ToString(); ;

            string empname = ds.Tables[0].Rows[0]["empname"].ToString();
            var list = ds.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAttendncLog>();



            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptAttnLog", list, null, null);

            //rpt1.SetParameters(new ReportParameter("Rptusirdesc", usirdesc));

            //rpt1.SetParameters(new ReportParameter("rptDt", rptDt));

            //rpt1.SetParameters(new ReportParameter("comadd", comadd));

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("depart", "Department:" + depart));
            rpt1.SetParameters(new ReportParameter("designation", designation));
            rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            rpt1.SetParameters(new ReportParameter("EmpNam", " Employee Name: " + empname));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Employee Attendance Log Information"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintAllMissingAttn()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            // string rptDt = "Date( From: " + txtfromdate + " To: " + txttodate + " )";



            //string ddlEmpName = this.ddlEmpName.SelectedItem.ToString();
            //string icardnumber = ddlEmpName.Substring(0, 6);
            //  DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTENDENCELOG", txtfromdate, txttodate, icardnumber, "", "", "", "", "", "");
            DataTable dt = (DataTable)ViewState["empMissAttn"];

            string depart = dt.Rows[0]["deptname"].ToString();
            //  string designation = "Designation: " + dt.Rows[0]["desg"].ToString(); ;

            string empname = dt.Rows[0]["empname"].ToString();
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpMissAttn>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpMissAttn", list, null, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("depart", "Department: " + depart));
            rpt1.SetParameters(new ReportParameter("designation", ""));
            rpt1.SetParameters(new ReportParameter("Frmdate", "Date: " + txtfromdate));
            rpt1.SetParameters(new ReportParameter("EmpNam", " Employee Name: " + empname));
            string comadd = hst["comadd1"].ToString();
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Missing Employee Attendance Information"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }




        private void PrintLateAttsheet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comaddf"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string rptDt = "Date: " + this.txtfromdate.Text.Trim().ToString();
            this.GetLateAttData();
            var list = (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsent>)ViewState["tbldaiylatedata"];
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyLateAttFactoryFB", list, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Late Attendence Report on " + this.txtfromdate.Text));
                    break;

                default:
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyLateAtt", list, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Daily Late Attendence Information"));
                    break;
            }

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("print_at", "Report Print: " + System.DateTime.Now.ToString("hh:mm:ss tt")));
            rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string GetPrintType()
        {
            string type = "";
            switch (this.ddlJobLocation.SelectedValue.ToString())
            {
                case "00000":
                case "87001":
                    type = "AllorHeadOff";
                    break;

                case "87002":
                    type = "Factory";
                    break;

                default:
                    type = "AllorHead";
                    break;
            }
            return type;
        }

        private void PrintDailyAbsent()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string rptDt = "Absent List For: " + this.txtfromdate.Text.Trim().ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();
            string rptitle = (this.ddlReport.SelectedValue == "11") ? "Daily Absent Employee List" : "Monthly Absent Employee List";
            DataTable dt = (DataTable)ViewState["tbldaiyabsentdata"];
            switch (comcod)
            {
                case "5305":
                case "5306":

                    var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsentFb>();
                    if (this.chkWithImage.Checked)
                    {
                        rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAbsentImgFb", list, null, null);
                    }
                    else
                    {
                        rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAbsentFb", list, null, null);
                    }
                    break;
                default:
                    var list1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsent>();
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAbsent", list1, null, null);
                    break;
            }

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("print_at", "Report Print: " + System.DateTime.Now.ToString("hh:mm:ss tt")));
            rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            rpt1.SetParameters(new ReportParameter("RptTitle", rptitle));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintMonthlyAbsent()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string rptDt = "Absent List For (" + this.txtfromdate.Text.Trim().ToString()+ " To " + this.txttodate.Text.Trim().ToString() + " )"; 
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();
            string rptitle = "Monthly Absent List";
            DataTable dt = (DataTable)ViewState["tbldaiyabsentdata"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyLateAndAbsentFb>();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMonthlyAbsentFb", list, null, null);
           
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            rpt1.SetParameters(new ReportParameter("RptTitle", rptitle));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintDailyPresent()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            LocalReport Rpt1 = new LocalReport();
            var list = (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.DailyPresent>)ViewState["tbldailypresent"];
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyPresent", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Present Report for : " + txtfromdate));
            Rpt1.SetParameters(new ReportParameter("txtFooter", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintJobCard()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMMdd");
            //string rptDt = "Date: " + this.txtfromdate.Text.Trim().ToString();
            string rptDt = "Date: " + this.txtfromdate.Text.Trim().ToString() + " To " + this.txttodate.Text.Trim().ToString();


            DataTable dt = (DataTable)Session["tblempdatewise"];
            DataTable dt1 = (DataTable)Session["tbljob01"];
            DataTable dt2 = (DataTable)Session["tbljob02"];

            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard01>();
            var list1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard02>();
            var list2 = dt2.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpJobCard03>();

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpJobCard", list, list1, list2);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("print_at", "Report Print: " + System.DateTime.Now.ToString("hh:mm:ss tt")));
            rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Job Card Report"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));


            rpt1.SetParameters(new ReportParameter("empjdate", dt.Rows[0]["joindate"].ToString()));

            rpt1.SetParameters(new ReportParameter("empid", dt.Rows[0]["idcardno"].ToString()));
            rpt1.SetParameters(new ReportParameter("empnam", dt.Rows[0]["empnam"].ToString()));
            rpt1.SetParameters(new ReportParameter("empdsg", dt.Rows[0]["empdsg"].ToString()));
            rpt1.SetParameters(new ReportParameter("empdept", dt.Rows[0]["empdept"].ToString()));
            rpt1.SetParameters(new ReportParameter("ttpsnt", Convert.ToDouble(dt1.Rows[0]["ttpsnt"]).ToString("#,##0;(#,##0)")));
            rpt1.SetParameters(new ReportParameter("tlvday", Convert.ToDouble(dt1.Rows[0]["tlvday"]).ToString("#,##0;(#,##0)")));
            rpt1.SetParameters(new ReportParameter("tabsday", Convert.ToDouble(dt1.Rows[0]["tabsday"]).ToString("#,##0;(#,##0)")));
            rpt1.SetParameters(new ReportParameter("tlday", Convert.ToDouble(dt1.Rows[0]["tlday"]).ToString("#,##0;(#,##0)")));
            rpt1.SetParameters(new ReportParameter("ttot", Convert.ToDouble(dt1.Rows[0]["ttot"]).ToString("#,##0;(#,##0)")));
            rpt1.SetParameters(new ReportParameter("fline", Convert.ToString(dt.Rows[0]["fline"]).ToString()));
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
        private void PrintAttApproval()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMMdd");
            string rptDt = "Date: " + this.txtfromdate.Text.Trim().ToString() + " To " + this.txttodate.Text.Trim().ToString();

            // this.SaveValue();

            var list = (List<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EclassAttApp>)ViewState["tblattenapproval"];
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptAttendanceApp", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Employee Attendance Approval Report"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
            this.ddlWstation.SelectedValue = "000000000000";
        }
        private void GetDivision()
        {
            try
            {
                string comcod = this.GetComCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string userid = hst["usrid"].ToString();
                var lst = getlist.GetDivision(comcod, wstation);
                this.ddlDivision.DataTextField = "actdesc";
                this.ddlDivision.DataValueField = "actcode";
                this.ddlDivision.DataSource = lst;
                this.ddlDivision.DataBind();
                this.ddlDivision.SelectedValue = "00000";

            }
            catch (Exception ex)
            {

            }
        }

        private void GetDeptList()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetDept(comcod, wstation);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "00000";

        }

        private void GetSectionList()
        {
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetSection(comcod, wstation);
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "00000";
        }



        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDivision();
            if (this.ddlReport.SelectedValue == "15")
            {
                this.imgbtnEmpName_Click(null, null);

            }
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
            var lst = getlist.GetJobLocation(comcod, userid);

            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();

        }
        protected void ddlJobLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvdailyattndc_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{
            //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //    TableCell cell0 = new TableCell();
            //    cell0.Text = "";
            //    cell0.HorizontalAlign = HorizontalAlign.Center;
            //    cell0.ColumnSpan = 5;
            //    gvrow.Cells.Add(cell0);

            //    TableCell cell00 = new TableCell();
            //    cell00.Text = "Office Time";
            //    cell00.HorizontalAlign = HorizontalAlign.Center;
            //    cell00.ColumnSpan = 2;
            //    gvrow.Cells.Add(cell00);

            //    TableCell cell03 = new TableCell();
            //    cell03.Text = "Actual Time";
            //    cell03.HorizontalAlign = HorizontalAlign.Center;
            //    cell03.ColumnSpan = 2;
            //    gvrow.Cells.Add(cell03);

            //    TableCell cell04 = new TableCell();
            //    cell04.Text = "";
            //    cell04.HorizontalAlign = HorizontalAlign.Center;
            //    cell04.ColumnSpan = 2;
            //    gvrow.Cells.Add(cell04);




            //    gvdailyattndc.Controls[0].Controls.AddAt(0, gvrow);
            //}
        }
        protected void gvdailyattndc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvdailyattndc.PageIndex = e.NewPageIndex;
            this.Data_bind();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_bind();

        }

        protected void gvmonthlyattndc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvmonthlyattndc.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }

        protected void gvdailyattsum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvdailyattsum.PageIndex = e.NewPageIndex;
            this.GetDailyAttenSummary();
        }

        protected void gvMissAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMissAttn.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }

        private DataTable HiddenMisSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string deptid = dt1.Rows[0]["deptid"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptid"].ToString() == deptid || dt1.Rows[j]["secid"].ToString() == secid)
                {

                    dt1.Rows[j]["deptname"] = "";
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["deptid"].ToString() == deptid)
                        dt1.Rows[j]["deptname"] = "";

                    if (dt1.Rows[j]["secid"].ToString() == secid)
                        dt1.Rows[j]["secton"] = "";
                }


                deptid = dt1.Rows[j]["deptid"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();
            }
            return dt1;

        }

        protected void gvdailylateatt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvdailyattndc.PageIndex = e.NewPageIndex;
            this.Data_bind();

        }

        private void GetJobCard()
        {

            string comcod = this.GetComCode();
            string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            string joblocation = this.ddlJobLocation.SelectedValue.ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, "", "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }

            //this.lblDateOn.Text = " From " + this.Request.QueryString["frmdate"].ToString() + " To " + this.Request.QueryString["todate"].ToString();
            this.lblcompname.Text = (ds1.Tables[2].Rows.Count > 0 ? ds1.Tables[2].Rows[0]["companyname"].ToString() : "");
            this.lblname.Text = (ds1.Tables[0].Rows.Count > 0 ? ds1.Tables[0].Rows[0]["empnam"].ToString() : "");
            this.lbldpt.Text = (ds1.Tables[0].Rows.Count > 0 ? ds1.Tables[0].Rows[0]["empdept"].ToString() : "");
            this.lbldesg.Text = (ds1.Tables[0].Rows.Count > 0 ? ds1.Tables[0].Rows[0]["empdsg"].ToString() : "");
            this.lblcard.Text = (ds1.Tables[0].Rows.Count > 0 ? ds1.Tables[0].Rows[0]["idcardno"].ToString() : "");
            this.lblIntime.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDateTime(ds1.Tables[0].Rows[0]["stdtimein"]).ToString("hh:mm tt") : "");
            this.lblout.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDateTime(ds1.Tables[0].Rows[0]["stdtimeout"]).ToString("hh:mm tt") : "");
            this.lblwork.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["twrkday"]).ToString("#, ##0;(#, ##0);") : "");
            this.lblLate.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["tlday"]).ToString("#, ##0;(#, ##0);") : "");
            this.lblLeave.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["tlvday"]).ToString("#, ##0;(#, ##0);") : "");
            this.lblAbsent.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["tabsday"]).ToString("#, ##0;(#, ##0);") : "");
            this.lblHoliday.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["thday"]).ToString("#, ##0;(#, ##0);") : "");
            this.lblOvtime.Text = (ds1.Tables[0].Rows.Count > 0 ? Convert.ToDouble(ds1.Tables[1].Rows[0]["ttot"]).ToString("#, ##0;(#, ##0);") : "");


            Session["tblempdatewise"] = ds1.Tables[0];
            Session["tbljob01"] = ds1.Tables[1];
            Session["tbljob02"] = ds1.Tables[2];



            this.RptMyAttenView.DataSource = ds1;
            this.RptMyAttenView.DataBind();



        }

        protected void RptMyAttenView_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            //if (e.Item.ItemType == ListItemType.Header)
            //{
            //    HtmlTableCell cell = (HtmlTableCell)e.Item.FindControl("thheader");
            //    cell.Visible = false;
            //}
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {

                //string dpt= ASTUtility.Left(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "empdeptid")).ToString(),4);
                //if (dpt=="9403")
                //{
                //    HtmlTableCell cell = (HtmlTableCell)e.Item.FindControl("lblOt");
                //    cell.Visible = true;
                //}


                string ahleave = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "leav")).ToString();

                DateTime offimein = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimein"));
                DateTime offouttim = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "stdtimeout"));


                DateTime actualin = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "actualin"));
                DateTime actualout = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "actualout"));



                if (ahleave == "A" || ahleave == "H" || ahleave == "Lv")
                {
                    ((Label)e.Item.FindControl("lblactualout")).Visible = false;
                    ((Label)e.Item.FindControl("lblactualin")).Visible = false;
                    ((Label)e.Item.FindControl("lblstatus")).Attributes["style"] = "font-weight:bold;";

                }
                //else if (offimein < actualin || offouttim > actualout)
                //{
                //    ((Label)e.Item.FindControl("lblactualout")).Attributes["style"] = "font-weight:bold; color:red;";
                //    ((Label)e.Item.FindControl("lblactualin")).Attributes["style"] = "font-weight:bold; color:red;";
                //    ((Label)e.Item.FindControl("lbldtimehour")).Attributes["style"] = "font-weight:bold; color:red;";


                //}

            }



            if (e.Item.ItemType == ListItemType.Footer)
            {
                //double AcTime = 0.00;
                //DataTable dt3 = (DataTable)Session["tblempdatewise"];
                //foreach (DataRow dr in dt3.Rows)
                //{
                //    double time = Convert.ToDouble("0" + dr["actTimehour"]);
                //    AcTime = AcTime + time;
                //}
                //((Label)e.Item.FindControl("lblTotalHour")).Text = AcTime.ToString("#,##0.00;(#,##0.00);"); //? 0.00 : dt3.Compute("Sum(actTimehour)", ""))).ToString("#,##0.00;(#,##0.00);");

                //Double actTimehour =Convert.ToDouble(dt3.Rows[0]["actTimehour"]);
                //((Label)e.Item.FindControl("lblTotalHour")).Text = Convert.ToDouble((Convert.IsDnumull(Convert.ToDouble(dt3.Compute("Sum(actTimehour)", ""))))).ToString("#,##0.00;(#,##0.00);"); //? 0.00 : dt3.Compute("Sum(actTimehour)", ""))).ToString("#,##0.00;(#,##0.00);");

            }
        }

        protected void ddlEmpName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void lnkUpdate_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();

            string dayid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMMdd");
            for (int i = 0; i < this.gvMissAttn.Rows.Count; i++)
            {
                string intime = String.Empty;
                string outtime = String.Empty;


                string punstatus = ((Label)this.gvMissAttn.Rows[i].FindControl("lbltstatus")).Text.Trim();
                string empid = ((Label)this.gvMissAttn.Rows[i].FindControl("gvEmpid")).Text.Trim();
                string idcardno = ((Label)this.gvMissAttn.Rows[i].FindControl("gvmissidcard")).Text.Trim();
                if (punstatus == "IM")
                {


                    intime = ((TextBox)this.gvMissAttn.Rows[i].FindControl("txtIntime")).Text.Trim() == ""
                        ? Convert.ToDateTime(this.txtfromdate.Text + "12:00:00 AM").ToString()
                        : Convert.ToDateTime(this.txtfromdate.Text + " " + ((TextBox)this.gvMissAttn.Rows[i].FindControl("txtIntime")).Text.Trim())
                            .ToString();
                    outtime = Convert.ToDateTime(this.txtfromdate.Text + " " + ((Label)this.gvMissAttn.Rows[i].FindControl("gvlateintimdem")).Text
                                                 .Trim()).ToString();

                }
                else
                {
                    intime = Convert.ToDateTime(this.txtfromdate.Text + " " + ((Label)this.gvMissAttn.Rows[i].FindControl("gvlateinout")).Text.Trim())
                        .ToString();
                    outtime = ((TextBox)this.gvMissAttn.Rows[i].FindControl("txtOut")).Text.Trim() == ""
                        ? Convert.ToDateTime(this.txtfromdate.Text + "12:00:00 AM").ToString()
                        : Convert.ToDateTime(this.txtfromdate.Text + " " + ((TextBox)this.gvMissAttn.Rows[i].FindControl("txtOut")).Text.Trim())
                            .ToString();
                }


                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATEEMPMISSINGDATA", dayid,
                    empid, idcardno, intime, outtime);
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                }
            }
        }

        protected void BtnChckResign_CheckedChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        protected void gvDailyPresent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDailyPresent.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }

        protected void chkInactPunch_CheckedChanged(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            this.GetAttendncLogData();
        }
    }
}