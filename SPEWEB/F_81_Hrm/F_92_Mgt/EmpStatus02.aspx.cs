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
using System.Drawing;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;
using static SPEENTITY.C_81_Hrm.C_92_Mgt.BO_EmpSep;
using System.IO;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class EmpStatus02 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        string empTypeMulti = "";

        public static string userid = "";
        public static string comLogo = "";
        public static Tuple<string, string> companyInfoBn = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                Session.Remove("tblEmpstatus");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFdate.Text = "01" + date.Substring(2);
                this.txtTdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
               
                this.SelectView();
                this.GetWorkStation();
                this.GetDivision();
                this.GetDeptList();
                this.GetSectionList();
                this.GetJobLocation();
                this.GetEmpLine();
                this.GetGrade();
                string Type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "joiningRpt") ? "Joining Report Summary" : (Type == "JoinigdWise") ? "New Joiners List"
                    : (Type == "EmpList") ? "Employee List" : (Type == "TransList") ? "Employee Transfer List"
                    : (Type == "PenEmpCon") ? "Pending Employee Confirmation" : (Type == "SepType") ? "Employee Separation List Report"
                    : (Type == "EmpHold") ? "Employee Hold List" : (Type == "Manpower") ? "Employee Manpower List"
                    : (Type == "EmpGradeADesig") ? "Grade & Designation Wise  Salary Detail"
                    : (Type == "RptAging") ? "Employee Aging Report" : (Type == "EmpListPic") ? "Employee List (With Picture)" : "Employee Confirmation";

                Hashtable hst = (Hashtable)Session["tblLogin"];
                userid = hst["usrid"].ToString();
                companyInfoBn = userid == "5305139" ? ASTUtility.CompInfoBnForFootbed() : ASTUtility.CompInfoBn();
                string comcod = this.GetComeCode();
                comLogo = userid == "5305139" ? new Uri(Server.MapPath(@"~\Image\LOGO" + "5306" + ".jpg")).AbsoluteUri : new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            }

        }


        private void GetJobLocation()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJob.DataTextField = "location";
            this.ddlJob.DataValueField = "loccode";
            this.ddlJob.DataSource = lst;
            this.ddlJob.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "joiningRpt":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "JoinigdWise":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.divRbtnempStatus.Visible = true;
                    this.divGrade.Visible = true;
                    break;

                case "EmpList":
                    this.divFrmDate.Visible = false;
                    this.divToDate.Visible = false;
                    this.divMchnIdMiss.Visible = true;
                    this.divBankAccMiss.Visible = true;
                    this.divGrade.Visible = true;
                    this.divGender.Visible = true;
                    this.divRbtnempStatus.Visible = true;
                    this.divRptTifin.Visible = true;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "EmpListPic":
                    this.divFrmDate.Visible = false;
                    this.divToDate.Visible = false;
                    this.divMchnIdMiss.Visible = true;
                    this.divBankAccMiss.Visible = false;
                    this.divGrade.Visible = true;
                    this.divWithPic.Visible = true;
                    this.divRbtnempStatus.Visible = true;
                    this.MultiView1.ActiveViewIndex = 11;
                    break;

                case "TransList":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "PenEmpCon"://Same GridView As EmpCon(Below)
                    this.divJoinDat.Visible = true;
                    this.chkJoinDate.Checked = true;
                    this.MultiView1.ActiveViewIndex = 4;
                    break;

                case "EmpCon":
                    this.divJoinDat.Visible = false;
                    this.MultiView1.ActiveViewIndex = 4;
                    break;

                case "Manpower":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "SepType":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.divSepFrmDate.Visible = true;
                    this.divSepToDate.Visible = true;
                    this.divRbtnempStatus.Visible = false;
                    this.txtFrmDate.Text = System.DateTime.Today.ToString("dd-MM-yyyy");
                    this.txtToDate.Text = System.DateTime.Today.ToString("dd-MM-yyyy");
                    this.GetSepType();
                    this.divIssuDate.Visible = true;
                    this.divSepType.Visible = true;
                    this.txtIssuDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    break;

                case "EmpHold":
                    this.MultiView1.ActiveViewIndex = 7;
                    break;

                case "EmpGradeADesig":
                    this.divFrmDate.Visible = false;
                    this.divToDate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 8;
                    break;

                case "RptAging":
                    this.chkJoinDate.Visible = false;
                    this.divRbtnempStatus.Visible = true;
                    this.MultiView1.ActiveViewIndex = 10;
                    break;

            }
        }

        private void GetEmpLine()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlEmpLine.DataTextField = "hrgdesc";
            this.ddlEmpLine.DataValueField = "hrgcod";
            this.ddlEmpLine.DataSource = ds3;
            this.ddlEmpLine.DataBind();
            this.ddlEmpLine.SelectedValue = "00000";

            ViewState["tbllineddl"] = ds3.Tables[0];
        }

        private void GetGrade()
        {
            string comcod = GetCompCode();
            var lst = getlist.GetEmpGradelist(comcod);

            this.ddlGrade.DataTextField = "hrgdesc";
            this.ddlGrade.DataValueField = "hrgcod";
            this.ddlGrade.DataSource = lst;
            this.ddlGrade.DataBind();
            this.ddlGrade.SelectedValue = "0399999";

        }

        private void GetSepType()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GET_SEAPRATION_TYPE", "", "", "", "", "", "", "", "", "");
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETSEAPRATIONTYPE", "", "", "", "", "", "", "", "", "");
            this.ddlSepType.DataTextField = "hrgdesc";
            this.ddlSepType.DataValueField = "hrgcod";
            this.ddlSepType.DataSource = ds1.Tables[0];
            this.ddlSepType.DataBind();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  this.GetProjectName();
        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            //  this.GetCompany();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "joiningRpt":
                    this.ShowJoiningSummary();
                    break;

                case "JoinigdWise":
                    this.GetEmpListJoiningDWise();
                    break;

                case "EmpList":
                    if (this.lbtnOk.Text == "New")
                    {
                        this.lbtnOk.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.divAddEmp.Visible = false;
                        this.chkAddEmp.Checked = false;
                        this.chkRptTifin.Checked = false;
                        this.gvEmpList.DataSource = null;
                        this.gvEmpList.DataBind();
                        return;

                    }

                    this.lbtnOk.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.GetEmpListInfo();
                    break;

                case "EmpListPic":
                    if (this.lbtnOk.Text == "New")
                    {
                        this.lbtnOk.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.divAddEmp.Visible = false;
                        this.chkAddEmp.Checked = false;
                        this.gvEmpListPic.DataSource = null;
                        this.gvEmpListPic.DataBind();
                        return;

                    }

                    this.lbtnOk.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.GetEmpListInfo();
                    break;

                case "TransList":
                    this.GetTransList();
                    break;

                case "PenEmpCon":
                    if (this.lbtnOk.Text == "New")
                    {
                        this.lbtnOk.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.divAddEmp.Visible = false;
                        this.chkAddEmp.Checked = false;
                        this.gvEmpCon.DataSource = null;
                        this.gvEmpCon.DataBind();
                        return;
                    }

                    this.lbtnOk.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.GetPenConfirmation();
                    break;

                case "EmpCon":
                    if (this.lbtnOk.Text == "New")
                    {
                        this.lbtnOk.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.divAddEmp.Visible = false;
                        this.chkAddEmp.Checked = false;
                        this.gvEmpCon.DataSource = null;
                        this.gvEmpCon.DataBind();
                        return;
                    }

                    this.lbtnOk.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.GetEmpConfirmation();
                    break;

                case "Manpower":
                    this.GetEmpManPower();
                    break;

                case "SepType":
                    this.GetEmpSPList();
                    break;

                case "EmpHold":
                    this.GetEmpHoldList();
                    break;

                case "EmpGradeADesig":
                    this.GetEmpLowHighSal();
                    break;

                case "RptAging":
                    this.GetEmployeeAging();
                    break;

            }





        }

        private void GetEmployeeAging()
        {
            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmDate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string empactinact = this.ddlEmpStatus.SelectedValue.ToString();
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string empLine = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPT_EMPLOYEE_AGING", Deptid, section, frmDate, toDate, division, empTypeMulti, empactinact, empLine, joblocation, usrid);
            if (ds5 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);

                this.gvEmpAging.DataSource = null;
                this.gvEmpAging.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds5.Tables[0]);
            this.LoadGrid();
        }

        private void ShowJoiningSummary()
        {
            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string empLine = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETJOINSUMMARY", Fdate, Tdate, empTypeMulti, division, Deptid, section, empLine, joblocation, usrid);
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);

                this.grvJoinStat.DataSource = null;
                this.grvJoinStat.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }

        private void GetEmpListJoiningDWise()
        {
            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string DesigFrom = "";
            string DesigTo = "";
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string empLine = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string empactinact = this.ddlEmpStatus.SelectedValue.ToString();
            string empGrade = (this.ddlGrade.SelectedValue.ToString() == "0399999" ? "" : this.ddlGrade.SelectedValue.ToString()) + "%";
            DataSet ds4 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETEMPLISTJDATEWISE", null, null, null, Deptid, section, DesigFrom, DesigTo, Fdate, Tdate, division, empTypeMulti,
                joblocation, usrid, empLine, empactinact, empGrade);
            if (ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvJoinEmp.DataSource = null;
                this.gvJoinEmp.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();
        }


        private void GetEmpListDataSet()
        {

            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();           
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string empactinact = this.ddlEmpStatus.SelectedValue.ToString();
            string empLine = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string mchnIdMiss = this.chkMchnIdMiss.Checked ? "mchnidmiss" : "";
            string bankAccMiss = this.chkBankAccMiss.Checked ? "bankaccmiss" : "";
            string empGrade = (this.ddlGrade.SelectedValue.ToString() == "0399999" ? "" : this.ddlGrade.SelectedValue.ToString()) + "%";
            string withpic = "";
            string qType = this.Request.QueryString["Type"] ?? "";
            if (qType == "EmpListPic")
            {
                withpic = "withpic";
            }
            string empGender = (this.ddlGender.SelectedValue.ToString() == "00000" ? "" : this.ddlGender.SelectedValue.ToString()) + "%";
            DataSet ds4 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTALLEMPLIST", null, null, null, "", section, empTypeMulti, division, Deptid, empactinact,
                usrid, "", empLine, joblocation, mchnIdMiss, bankAccMiss, withpic, empGrade, empGender);
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }

            Session["tblEmpstatus"] = ds4.Tables[0];



        }


        private void GetEmpListInfo()
        {

            this.GetEmpListDataSet();
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            Session["tblEmpstatus"] = HiddenSameData(dt);
            this.LoadGrid();

        }


        private void GetTransList()
        {
            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string divid = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETEMPTRANSFERLIST", Fdate, Tdate, Deptid, section, divid, empTypeMulti, usrid, "", "");
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);

                this.grvTransList.DataSource = null;
                this.grvTransList.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }


        private void GetPenConfirmation()
        {
            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string accjoindate = this.chkJoinDate.Checked ? "JoinDate" : "";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string empLine = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            DataSet ds4 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTPENCONFIRMATION", null, null, null, Deptid, section, empTypeMulti, division, Fdate, Tdate, accjoindate, empLine, joblocation, usrid, "", "", "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvEmpCon.DataSource = null;
                this.gvEmpCon.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }

        private void GetEmpConfirmation()
        {
            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string empLine = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTEMPCONFIRMATION", Deptid, section, empTypeMulti, division, Fdate, Tdate, empLine, joblocation, usrid);
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvEmpCon.DataSource = null;
                this.gvEmpCon.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();


        }
        private void GetEmpManPower()
        {
            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTEMPMANPOWER", empTypeMulti, Fdate, Tdate, usrid, "", "", "", "", "");
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);

                this.grvManPwr.DataSource = null;
                this.grvManPwr.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }
        private void GetEmpSPList()
        {
            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string sptype = ((this.ddlSepType.SelectedValue.ToString() == "00000") ? "75" : this.ddlSepType.SelectedValue.ToString()) + "%";
            string empLine = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "SHOWEMPSPLIST", Fdate, Tdate, section, division, empTypeMulti, Deptid, sptype, empLine, joblocation, usrid);
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);

                this.grvEmpSep.DataSource = null;
                this.grvEmpSep.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }


        private void GetEmpHoldList()
        {
            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string empLine = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTPEMPHOLDLIST", "", section, empTypeMulti, division, Fdate, Tdate, Deptid, empLine, joblocation, usrid);
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvEmpHold.DataSource = null;
                this.gvEmpHold.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();


        }
        private void GetEmpLowHighSal()
        {
            Session.Remove("tblEmpstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    empTypeMulti += items.Value;
                }
            }
            string division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string GradeFrom = "";
            string GradeTo = "";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GRDEGWISESALARYDET", "%", section, GradeFrom, GradeTo, empTypeMulti, division, usrid, "", "");
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.grvEmpLHSal.DataSource = null;
                this.grvEmpLHSal.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            string company, deptid, secid;
            int i = 0;
            switch (type)
            {
                case "joiningRpt":
                    string compcod = dt1.Rows[0]["compcod"].ToString();
                    string deptcod = dt1.Rows[0]["deptcod"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["compcod"].ToString() == compcod)
                        {
                            compcod = dt1.Rows[j]["compcod"].ToString();
                            dt1.Rows[j]["compname"] = "";
                        }
                        else
                        {
                            if (dt1.Rows[j]["deptcod"].ToString() == deptcod)
                            {
                                dt1.Rows[j]["department"] = "";
                            }
                            compcod = dt1.Rows[j]["compcod"].ToString();
                            deptcod = dt1.Rows[j]["deptcod"].ToString();
                        }
                    }
                    break;


                case "EmpList":
                    // company = dt1.Rows[0]["company"].ToString();
                    deptid = dt1.Rows[0]["deptid"].ToString();
                    secid = dt1.Rows[0]["secid"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["deptid"].ToString() == deptid && dt1.Rows[j]["secid"].ToString() == secid)
                        {

                            //dt1.Rows[j]["companyname"] = "";
                            dt1.Rows[j]["deptname"] = "";
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["deptid"].ToString() == deptid)
                                dt1.Rows[j]["deptname"] = "";


                            //if (dt1.Rows[j]["secid"].ToString() == secid)
                            //    dt1.Rows[j]["section"] = "";
                        }



                        deptid = dt1.Rows[j]["deptid"].ToString();
                        secid = dt1.Rows[j]["secid"].ToString();
                    }
                    break;

                case "EmpListPic":
                    // company = dt1.Rows[0]["company"].ToString();
                    deptid = dt1.Rows[0]["deptid"].ToString();
                    secid = dt1.Rows[0]["secid"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["deptid"].ToString() == deptid && dt1.Rows[j]["secid"].ToString() == secid)
                        {

                            //dt1.Rows[j]["companyname"] = "";
                            dt1.Rows[j]["deptname"] = "";
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["deptid"].ToString() == deptid)
                                dt1.Rows[j]["deptname"] = "";


                            //if (dt1.Rows[j]["secid"].ToString() == secid)
                            //    dt1.Rows[j]["section"] = "";
                        }



                        deptid = dt1.Rows[j]["deptid"].ToString();
                        secid = dt1.Rows[j]["secid"].ToString();
                    }
                    break;

                case "JoinigdWise":

                    company = dt1.Rows[0]["company"].ToString();

                    secid = dt1.Rows[0]["secid"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["secid"].ToString() == secid)
                        {

                            dt1.Rows[j]["companyname"] = "";
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["company"].ToString() == company)
                                dt1.Rows[j]["companyname"] = "";

                            if (dt1.Rows[j]["secid"].ToString() == secid)
                                dt1.Rows[j]["secton"] = "";
                        }


                        company = dt1.Rows[j]["company"].ToString();
                        secid = dt1.Rows[j]["secid"].ToString();
                    }

                    deptid = dt1.Rows[0]["deptid"].ToString();
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (i == 0)
                        {


                            deptid = dr1["deptid"].ToString();
                            i++;
                            continue;
                        }

                        if (dr1["deptid"].ToString() == deptid)
                        {

                            dr1["deptname"] = "";

                        }


                        deptid = dr1["deptid"].ToString();
                    }
                    break;

                case "TransList":
                    break;


                case "PenEmpCon":
                case "EmpCon":
                case "EmpHold":
                case "EmpGradeADesig":
                    company = dt1.Rows[0]["company"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company)
                            dt1.Rows[j]["companyname"] = "";

                        company = dt1.Rows[j]["company"].ToString();
                    }

                    break;
                case "Manpower":
                    company = dt1.Rows[0]["compcode"].ToString();
                    secid = dt1.Rows[0]["deptcode"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["compcode"].ToString() == company)
                            dt1.Rows[j]["compname"] = "";

                        if (dt1.Rows[j]["deptcode"].ToString() == secid)
                            dt1.Rows[j]["deptname"] = "";

                        company = dt1.Rows[j]["compcode"].ToString();
                        secid = dt1.Rows[j]["deptcode"].ToString();
                    }

                    break;
                case "SepType":
                    company = dt1.Rows[0]["company"].ToString();
                    secid = dt1.Rows[0]["section"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company)
                            dt1.Rows[j]["compname"] = "";

                        if (dt1.Rows[j]["section"].ToString() == secid)
                            dt1.Rows[j]["secname"] = "";

                        company = dt1.Rows[j]["company"].ToString();
                        secid = dt1.Rows[j]["section"].ToString();
                    }
                    break;

                case "RptAging":

                    company = dt1.Rows[0]["company"].ToString();

                    secid = dt1.Rows[0]["secid"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["secid"].ToString() == secid)
                        {

                            dt1.Rows[j]["companyname"] = "";
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["company"].ToString() == company)
                                dt1.Rows[j]["companyname"] = "";

                            if (dt1.Rows[j]["secid"].ToString() == secid)
                                dt1.Rows[j]["secton"] = "";
                        }


                        company = dt1.Rows[j]["company"].ToString();
                        secid = dt1.Rows[j]["secid"].ToString();
                    }

                    deptid = dt1.Rows[0]["deptid"].ToString();
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (i == 0)
                        {


                            deptid = dr1["deptid"].ToString();
                            i++;
                            continue;
                        }

                        if (dr1["deptid"].ToString() == deptid)
                        {

                            dr1["deptname"] = "";

                        }


                        deptid = dr1["deptid"].ToString();
                    }


                    break;


            }

            return dt1;

        }



        private void LoadGrid()
        {
            try
            {
                string type = this.Request.QueryString["Type"].ToString().Trim();
                DataTable dt = (DataTable)Session["tblEmpstatus"];

                switch (type)
                {
                    case "joiningRpt":
                        this.grvJoinStat.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.grvJoinStat.DataSource = dt;
                        this.grvJoinStat.DataBind();
                        break;

                    case "JoinigdWise":
                        this.gvJoinEmp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvJoinEmp.DataSource = dt;
                        this.gvJoinEmp.DataBind();

                        Session["Report1"] = gvJoinEmp;
                        if (dt.Rows.Count > 0)
                        {
                            ((HyperLink)this.gvJoinEmp.HeaderRow.FindControl("hlbtnexportexcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }
                        break;

                    case "EmpList":
                        this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpList.DataSource = dt;
                        this.gvEmpList.DataBind();

                        Session["Report1"] = gvEmpList;
                        if (dt.Rows.Count > 0)
                        {
                            ((HyperLink)this.gvEmpList.HeaderRow.FindControl("hlbtnexportexcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                            ((Label)this.gvEmpList.FooterRow.FindControl("lblgvFsalaryemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(grossal)", "")) ? 0.00 :
                                dt.Compute("Sum(grossal)", ""))).ToString("#,##0;(#,##0); ");
                        }
                        break;

                    case "EmpListPic":
                        //this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        DataTable dt1 = this.GetDataTableImgURL(dt);
                        //With Image
                        if(chkWithPic.Checked)
                        {
                            DataView dv = dt1.DefaultView;
                            dv.RowFilter = ("empimage<>''");
                            dt1 = dv.ToTable();
                        }                       
                        this.gvEmpListPic.DataSource = dt1;
                        this.gvEmpListPic.DataBind();

                        Session["Report1"] = gvEmpListPic;
                        if (dt.Rows.Count > 0)
                        {
                            ((HyperLink)this.gvEmpListPic.HeaderRow.FindControl("hlbtnexportexcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                            ((Label)this.gvEmpListPic.FooterRow.FindControl("lblgvFsalaryemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(grossal)", "")) ? 0.00 :
                                dt.Compute("Sum(grossal)", ""))).ToString("#,##0;(#,##0); ");
                        }
                        break;

                    case "TransList":
                        this.grvTransList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.grvTransList.DataSource = dt;
                        this.grvTransList.DataBind();
                        break;

                    case "PenEmpCon":
                        this.gvEmpCon.Columns[10].Visible = false; //CheckBox for Confirmation Report
                        this.gvEmpCon.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpCon.DataSource = dt;
                        this.gvEmpCon.DataBind();
                        Session["Report1"] = gvEmpCon;
                        if (dt.Rows.Count > 0)
                        {
                            ((HyperLink)this.gvEmpCon.HeaderRow.FindControl("hlbtnexportexcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                            ((Label)this.gvEmpCon.FooterRow.FindControl("lblgvFPenConGssal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(gssal)", "")) ? 0.00 :
                                dt.Compute("Sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                        }
                        break;

                    case "EmpCon":
                        this.gvEmpCon.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpCon.DataSource = dt;
                        this.gvEmpCon.DataBind();
                        Session["Report1"] = gvEmpCon;
                        if (dt.Rows.Count > 0)
                        {
                            ((HyperLink)this.gvEmpCon.HeaderRow.FindControl("hlbtnexportexcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }
                        break;
                    case "Manpower":
                        this.grvManPwr.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.grvManPwr.DataSource = dt;
                        this.grvManPwr.DataBind();
                        break;
                    case "SepType":
                        this.grvEmpSep.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.grvEmpSep.DataSource = dt;
                        this.grvEmpSep.DataBind();

                        Session["Report1"] = grvEmpSep;
                        if (dt.Rows.Count > 0)
                        {
                            ((HyperLink)this.grvEmpSep.HeaderRow.FindControl("hlbtnexportexcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }
                        break;
                    case "EmpHold":
                        this.gvEmpHold.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpHold.DataSource = dt;
                        this.gvEmpHold.DataBind();
                        break;
                    case "EmpGradeADesig":
                        this.grvEmpLHSal.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.grvEmpLHSal.DataSource = dt;
                        this.grvEmpLHSal.DataBind();
                        break;

                    case "RptAging":
                        this.gvEmpAging.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpAging.DataSource = dt;
                        this.gvEmpAging.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }

        private DataTable GetDataTableImgURL(DataTable dt)
        {
            string comcod = this.GetComeCode();
            foreach (DataRow dr in dt.Rows)
            {
                string idcard = dr["idcardno"].ToString();
                //With Image
                if (chkWithPic.Checked)
                {
                    string empImg = "";
                    switch (comcod)
                    {
                        case "5305"://FB
                            empImg = "~/Upload/HRM/EmpImgFB/" + idcard + ".jpg";
                            break;

                        case "5306"://Footbed
                            empImg = "~/Upload/HRM/EmpImgFBF/" + idcard + ".jpg";
                            break;

                        default:
                            empImg = "~/Upload/HRM/EmpImg/" + idcard + ".jpg";
                            break;
                    }
                    FileInfo ImgFile = new FileInfo(Server.MapPath(empImg));
                    if (ImgFile.Exists)
                    {

                        switch (comcod)
                        {
                            case "5305"://FB
                                dr["empimage"] = "~/Upload/HRM/EmpImgFB/" + idcard + ".jpg";
                                break;

                            case "5306"://Footbed
                                dr["empimage"] = "~/Upload/HRM/EmpImgFBF/" + idcard + ".jpg";
                                break;

                            default:
                                dr["empimage"] = "~/Upload/HRM/EmpImg/" + idcard + ".jpg";
                                break;
                        }
                    }
                }
                else
                {
                    switch (comcod)
                    {
                        case "5305"://FB
                            dr["empimage"] = "~/Upload/HRM/EmpImgFB/" + idcard + ".jpg";
                            break;

                        case "5306"://Footbed
                            dr["empimage"] = "~/Upload/HRM/EmpImgFBF/" + idcard + ".jpg";
                            break;

                        default:
                            dr["empimage"] = "~/Upload/HRM/EmpImg/" + idcard + ".jpg";
                            break;
                    }
                }

            }

            return dt;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "joiningRpt":
                    this.RptJoiningStatus();
                    break;

                case "JoinigdWise":
                    this.PrintEmpListJoiningDWise();
                    break;
                case "EmpList":                   
                    this.PrintEmpList();
                    break;

                case "EmpListPic":
                    this.PrintEmpListWithPic();
                    break;

                case "TransList":
                    this.RptTransList();
                    break;

                case "PenEmpCon":
                    this.RptPenEmpConfirmation();
                    break;

                case "EmpCon":
                    this.RptEmpConfirmation();
                    break;
                case "Manpower":
                    this.RptManPower();
                    break;
                case "SepType":
                    this.RptEmpSPList();
                    break;

                case "EmpHold":
                    this.RptEmpHoldList();
                    break;
                case "EmpGradeADesig":
                    this.RptEmpLowHighSalary();
                    break;
                case "RptAging":
                    this.RptEmployeeAging();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Joining Roport Summary";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void RptEmployeeAging()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");

            string wrkstattion = this.ddlWstation.SelectedValue.ToString();
            var lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>)Session["lstwrkstation"];
            string comnam = lst.FindAll(l => l.actcode == wrkstattion)[0].hrcomname;

            DataTable dt = (DataTable)Session["tblEmpstatus"];
            var list1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeLst>();

            LocalReport rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpAging", list1, null, null);
                    rpt1.EnableExternalImages = true;
                    break;

                default:
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpAging", list1, null, null);
                    rpt1.EnableExternalImages = true;
                    break;


            }

            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Age Report on " + Fdate));
            rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptJoiningStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptJoiningStatus();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = "Date: " + fromdate + " To " + todate;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tblEmpstatus"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintEmpListJoiningDWise()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();

            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var list1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeLst>();
            string daterange = "(From " + Fdate + " To " + Tdate + ")";
            LocalReport rpt1 = new LocalReport();

            switch (comcod)
            {
                case "5305":
                case "5306":

                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpListFB", list1, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("txtdatefrmto", daterange));

                    break;

                default:

                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpList", list1, null, null);
                    rpt1.EnableExternalImages = true;
                    break;


            }


            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "New Joining Employee List"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintEmpList()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string EmpStatus = "";
            string status = this.ddlEmpStatus.SelectedValue;            

            DataTable dt = (DataTable)Session["tblEmpstatus"];
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var list1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeLst>();
            LocalReport rpt1 = new LocalReport();
            string Rpttitle = "";
            //Tifin List Report 
            if (chkRptTifin.Checked)
            {
                string date = Convert.ToDateTime(System.DateTime.Now).ToString("MMMM -yyyy");
                string frmDate = "01-" + Convert.ToDateTime(System.DateTime.Today).ToString("MMM-yyyy");
                string toDate = Convert.ToDateTime(frmDate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_86_All.RptEmpTifinList", list1, null, null);
                rpt1.EnableExternalImages = true;
                rpt1.SetParameters(new ReportParameter("compName", comnam));
                rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                DateTime frmMDate = Convert.ToDateTime(frmDate);
                DateTime toMDate = Convert.ToDateTime(toDate);
                while (frmMDate<=toMDate)
                {
                    string day = Convert.ToDateTime(frmMDate).ToString("dd");
                    rpt1.SetParameters(new ReportParameter("d"+ day, day));
                    frmMDate = frmMDate.AddDays(1);
                    if (frmMDate > toMDate)
                        break;
                }                
                rpt1.SetParameters(new ReportParameter("rptTitle", "TIFIN LIST MONTH OF " + date));
                rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));

                Session["Report1"] = rpt1;
            }
            else
            {
                if (status == "0")
                {

                    EmpStatus = "Active";

                    switch (comcod)
                    {
                        case "5305":
                        case "5306":

                            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptActiveEmpListFB", list1, null, null);
                            rpt1.EnableExternalImages = true;
                            Rpttitle = "Active Employee List";
                            break;

                        default:
                            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpList", list1, null, null);
                            rpt1.EnableExternalImages = true;
                            Rpttitle = "Employee List";
                            break;

                    }

                    rpt1.SetParameters(new ReportParameter("EmpStatus", EmpStatus));
                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("rpttitle", Rpttitle));
                    rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    Session["Report1"] = rpt1;
                }
                else if (status == "1")
                {
                    EmpStatus = "Inactive";

                    switch (comcod)
                    {
                        case "5305":
                        case "5306":

                            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptInActiveEmpListFB", list1, null, null);
                            rpt1.EnableExternalImages = true;
                            Rpttitle = "Inactive Employee List";
                            break;

                        default:
                            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpListInactive", list1, null, null);
                            rpt1.EnableExternalImages = true;
                            Rpttitle = "Employee List";
                            break;


                    }

                    rpt1.SetParameters(new ReportParameter("EmpStatus", EmpStatus));
                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("rpttitle", Rpttitle));
                    rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    Session["Report1"] = rpt1;
                }
            }           

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintEmpListWithPic()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblEmpstatus"];
            //With Image
            if (chkWithPic.Checked)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("empimage<>''");
                dt = dv.ToTable();
            }
            var list1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeLst>();
            //Image from folder with path
            foreach (var item in list1)
            {
                string idcard = item.idcardno.ToString();
                string empImg = item.empimage;
                FileInfo ImgFile = new FileInfo(Server.MapPath(empImg));
                if (ImgFile.Exists)
                {

                    switch (comcod)
                    {
                        case "5305"://FB
                            item.empimage = new Uri(Server.MapPath(@"~\Upload\HRM\EmpImgFB\" + idcard + ".jpg")).AbsoluteUri;
                            break;

                        case "5306"://Footbed
                            item.empimage = new Uri(Server.MapPath(@"~\Upload\HRM\EmpImgFBF\" + idcard + ".jpg")).AbsoluteUri;
                            break;

                        default:
                            item.empimage = new Uri(Server.MapPath(@"~\Upload\HRM\EmpImgFB\" + idcard + ".jpg")).AbsoluteUri;
                            break;
                    }
                }
                else
                {
                    item.empimage = "";
                }
            }
            LocalReport rpt1 = new LocalReport();
            string Rpttitle = "";
            string EmpStatus = "";

            string status = this.ddlEmpStatus.SelectedValue;
            if (status == "0")
            {
                EmpStatus = "Active";
                switch (comcod)
                {
                    case "5305":
                    case "5306":
                        rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptActiveEmpListWPic", list1, null, null);
                        rpt1.EnableExternalImages = true;
                        Rpttitle = "Active Employee List";
                        break;

                    default:
                        rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpList", list1, null, null);
                        rpt1.EnableExternalImages = true;
                        Rpttitle = "Employee List";
                        break;

                }

                rpt1.SetParameters(new ReportParameter("EmpStatus", EmpStatus));
                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("rpttitle", Rpttitle));
                rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                Session["Report1"] = rpt1;
            }
            else if (status == "1")
            {
                EmpStatus = "Inactive";
                switch (comcod)
                {
                    case "5305":
                    case "5306":
                        rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptInActiveEmpListWPic", list1, null, null);
                        rpt1.EnableExternalImages = true;
                        Rpttitle = "Inactive Employee List";
                        break;

                    default:
                        rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpListInactive", list1, null, null);
                        rpt1.EnableExternalImages = true;
                        Rpttitle = "Employee List";
                        break;
                }

                rpt1.SetParameters(new ReportParameter("EmpStatus", EmpStatus));
                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("rpttitle", Rpttitle));
                rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                Session["Report1"] = rpt1;
            }

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptTransList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comAdd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpstatus"];


            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EclassEmployeeTransLst>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptTransList", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comAdd));
            rpt1.SetParameters(new ReportParameter("ComLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("Data", "Date: " + fromdate + " To " + todate));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Mobile List"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptPenEmpConfirmation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comAdd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("MMMM yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpstatus"];


            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.RptEmpConfirmation>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpPendingConfirmation", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comAdd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Month of " + fromdate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Review List (6 Months)"));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptEmpConfirmation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comAdd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("MMMM yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpstatus"];

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("chk='True'");

            var list = dv.ToTable().DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.RptEmpConfirmation>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpPendingConfirmation", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comAdd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Month of " + fromdate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Review List (6 Months)"));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptManPower()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptManpower();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblEmpstatus"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptEmpSPList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpstatus"];

            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.RptEmpSeparation>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpSepList", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Separation List"));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptEmpHoldList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptDateWiseEmpHold();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtfrmdatetodate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblEmpstatus"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptEmpLowHighSalary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptGradeADesgSalary();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtDate = rptstate.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtDept"] as TextObject;
            ////rptftdate.Text = "Department Name: " + this.ddlProjectName.SelectedItem.Text;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblEmpstatus"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }

        protected void grvJoinStat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvJoinStat.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvJoinEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvJoinEmp.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpList.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }
        protected void ddlfrmDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.GetDessignationTo();
        }
        protected void grvTransList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvTransList.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvEmpCon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpCon.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }

        private void SaveValue()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            int i;
            switch (type)
            {
                case "EmpCon":
                    //Looping For Checked Status                    
                    for (i = 0; i < this.gvEmpCon.Rows.Count; i++)
                    {
                        int row = (this.gvEmpCon.PageSize) * this.gvEmpCon.PageIndex + i;
                        bool chkitm = ((CheckBox)this.gvEmpCon.Rows[i].FindControl("chkPrint")).Checked;
                        if (chkitm == true)
                        {
                            dt.Rows[row]["chk"] = "True";

                        }

                        else
                        {
                            dt.Rows[row]["chk"] = "False";
                        }
                    }
                    break;

                case "SepType":
                    for (i = 0; i < this.grvEmpSep.Rows.Count; i++)
                    {
                        int row = (this.grvEmpSep.PageSize) * this.grvEmpSep.PageIndex + i;
                        bool chkitm = ((CheckBox)this.grvEmpSep.Rows[i].FindControl("chkIndv")).Checked;
                        if (chkitm == true)
                        {
                            dt.Rows[row]["chk"] = "True";

                        }

                        else
                        {
                            dt.Rows[row]["chk"] = "False";
                        }
                    }
                    break;
            }

            Session["tblEmpstatus"] = dt;

        }

        protected void grvManPwr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvManPwr.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void grvManPwr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label desg = (Label)e.Row.FindControl("lblgvdesignation");
                Label opening = (Label)e.Row.FindControl("lblgvOpening");
                Label joining = (Label)e.Row.FindControl("lblgvJoining");
                Label tin = (Label)e.Row.FindControl("lblgvnotrIn");
                Label tout = (Label)e.Row.FindControl("lblgvnotrout");
                Label dep = (Label)e.Row.FindControl("lblgvDep");
                Label tqty = (Label)e.Row.FindControl("lblgvTotal");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "desigid")).ToString().Trim();
                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    desg.Font.Bold = true;
                    opening.Font.Bold = true;
                    joining.Font.Bold = true;
                    tin.Font.Bold = true;
                    tout.Font.Bold = true;
                    dep.Font.Bold = true;
                    tqty.Font.Bold = true;
                    desg.Style.Add("text-align", "right");

                }
            }
        }
        protected void imgBtnSpType_Click(object sender, EventArgs e)
        {
            this.GetSepType();
        }

        protected void gvEmpHold_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpHold.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }
        protected void grvEmpLHSal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvEmpLHSal.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvgwemp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetWorkStation()
        {
            Session.Remove("lstwrkstation");
            string comcod = GetCompCode();
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

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            var lst = getlist.GetDivision(comcod, wstation);
            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "00000";

        }

        private void GetDeptList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            var lst = getlist.GetDept(comcod, wstation);
            this.ddlDep.DataTextField = "actdesc";
            this.ddlDep.DataValueField = "actcode";
            this.ddlDep.DataSource = lst;
            this.ddlDep.DataBind();
            this.ddlDep.SelectedValue = "00000";

        }

        private void GetSectionList()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
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
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }


        //IQBAL NAYAN
        protected void LbtnPrint1st_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            switch (comcod)
            {
                case "5305":
                case "5306":
                    string empId = "";
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            empId += ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvempid")).Text.ToString();
                        }
                    }

                    this.PrintFirstLetterMultiFB(empId);
                    break;

                default:

                    List<EmpSep01> listsep = new List<EmpSep01>();
                    EmpSep01 SingleSep = new EmpSep01();
                    string idcardno = ((Label)this.grvEmpSep.Rows[index].FindControl("lblgvcardnocon")).Text.ToString();
                    GetLetterData(idcardno, listsep);
                    PrintFirstLetter(listsep);
                    break;


            }



        }

        protected void LbtnPrint2nd_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            switch (comcod)
            {
                case "5305":
                case "5306":
                    string empId = "";
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            empId += ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvempid")).Text.ToString();
                        }
                    }

                    this.PrintSecondLetterMultiFB(empId);
                    break;

                default:

                    List<EmpSep01> listsep = new List<EmpSep01>();
                    EmpSep01 SingleSep = new EmpSep01();
                    string idcardno = ((Label)this.grvEmpSep.Rows[index].FindControl("lblgvcardnocon")).Text.ToString();
                    GetLetterData(idcardno, listsep);
                    PrintFirstLetter(listsep);
                    break;


            }
        }
        protected void LbtnPrint3th_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            switch (comcod)
            {
                case "5305":
                case "5306":
                    string empId = "";
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            empId += ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvempid")).Text.ToString();
                        }
                    }

                    this.PrintThirdLetterMultiFB(empId);
                    break;

                default:

                    List<EmpSep01> listsep = new List<EmpSep01>();
                    EmpSep01 SingleSep = new EmpSep01();
                    string idcardno = ((Label)this.grvEmpSep.Rows[index].FindControl("lblgvcardnocon")).Text.ToString();
                    GetLetterData(idcardno, listsep);
                    PrintFirstLetter(listsep);
                    break;


            }

        }
        protected void LbtnPrint4th_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            switch (comcod)
            {
                case "5305":
                case "5306":
                    this.PrintAllEnvelope();
                    break;

                default:
                    List<EmpSep01> listsep = new List<EmpSep01>();
                    EmpSep01 SingleSep = new EmpSep01();
                    string idcardno = ((Label)this.grvEmpSep.Rows[index].FindControl("lblgvcardnocon")).Text.ToString();
                    GetLetterData(idcardno, listsep);
                    PrintFirstLetter(listsep);
                    break;


            }

        }

        private void PrintAllEnvelope()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string reportTye = GetReportType();
            LocalReport Rpt1 = new LocalReport();
            this.SaveValue();
            string empidmulti = "";
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("chk='True'");
            dt = dv.ToTable();

            try
            {

                foreach (var row in dt.Rows.OfType<DataRow>())
                {
                    empidmulti += row.Field<string>("empid");
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            string comnamBn = companyInfoBn.Item1;
            string comaddBn = companyInfoBn.Item2;
            if (reportTye == "EnvelopePerBangla")
            {

                var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopeBan", RptListBn, null, null);

                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comaddBn));
            }
            else if (reportTye == "EnvelopePerEnglish")
            {
                var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopeEng", RptListBn, null, null);

                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            }
            else if (reportTye == "EnvelopePresentBangla")
            {
                var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopePresentBan", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comaddBn));
            }
            else if (reportTye == "EnvelopePresentEnglish")
            {
                var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopePresentEng", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            }


            Session["Report1"] = Rpt1;
            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }
        private string GetReportType()
        {
            string Type = "";
            string rptType = this.ddlReportType.SelectedValue.ToString();
            switch (rptType)
            {
                case "0":
                    Type = "EnvelopePerEnglish";
                    break;
                case "1":
                    Type = "EnvelopePerBangla";
                    break;
                case "2":
                    Type = "EnvelopePresentEnglish";
                    break;
                case "3":
                    Type = "EnvelopePresentBangla";
                    break;
                default:
                    Type = "EnvelopePerEnglish";
                    break;
            }
            return Type;
        }

        protected void LbtnPrint6th_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            List<EmpSep01> listsep = new List<EmpSep01>();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    string empId = "";
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            empId += ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvempid")).Text.ToString();
                        }
                    }

                    this.PrintMatSecondLetterMultiFB(empId);
                    break;

                default:
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            string idcardno = ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvcardnocon")).Text.ToString();
                            GetLetterData(idcardno, listsep);
                        }
                    }

                    PrintFirstLetter(listsep);
                    break;
            }
        }
        private void PrintMatSecondLetterMultiFB(string empid)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string comaddbn = hst["comaddbn"].ToString();
            string comaddDetails = hst["comadd"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string auditComLogo = new Uri(Server.MapPath(@"~\Image\LOGO5306.jpg")).AbsoluteUri;
            string curr_date = DateTime.Now.ToString("yyyy");
            string issuDate = Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
            string frmDate = this.txtFrmDate.Text;
            string toDate = this.txtToDate.Text;
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            var lst1 = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
            string empIDCard = lst1[0].idcard.ToString();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpMatSecondLetter", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("frmDate", frmDate));
            Rpt1.SetParameters(new ReportParameter("toDate", toDate));
            Rpt1.SetParameters(new ReportParameter("empIDCard", empIDCard));
            Rpt1.SetParameters(new ReportParameter("curDate", curr_date));
            Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
            Rpt1.SetParameters(new ReportParameter("compName", userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি." : comnambn)); //Audit user
            Rpt1.SetParameters(new ReportParameter("compAdd", comaddbn));
            Rpt1.SetParameters(new ReportParameter("comsnm", (comcod == "5305" && userid == "5305139") ? "এফবিএফ" : comcod == "5305" ? "এফবি" : "এফবিএফ")); //Audit user
            Rpt1.SetParameters(new ReportParameter("comaddDetails", comaddDetails));
            Rpt1.SetParameters(new ReportParameter("sign1", comcod == "5305" ? "উপ-ব্যবস্থাপক" : "ব্যবস্থাপক / উপঃ ব্যবস্থাপক"));
            Rpt1.SetParameters(new ReportParameter("sign2", comcod == "5305" ? "সাস্টেনিবিলিটি এন্ড সিএসআর বিভাগ" : "মানব সম্পদ, প্রশাসন ও কমপ্লায়েন্স বিভাগ"));
            Rpt1.SetParameters(new ReportParameter("comLogo", userid == "5305139" ? auditComLogo : comLogo)); //Audit user
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = Rpt1;

            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }
        protected void LbtnPrint5th_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            List<EmpSep01> listsep = new List<EmpSep01>();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    string empId = "";
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            empId += ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvempid")).Text.ToString();
                        }
                    }

                    this.PrintMatFirstLetterMultiFB(empId);
                    break;

                default:
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            string idcardno = ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvcardnocon")).Text.ToString();
                            GetLetterData(idcardno, listsep);
                        }
                    }

                    PrintFirstLetter(listsep);
                    break;
            }            
        }

        private void PrintMatFirstLetterMultiFB(string empid)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string comaddbn = hst["comaddbn"].ToString();
            string comaddDetails = hst["comadd"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string auditComLogo = new Uri(Server.MapPath(@"~\Image\LOGO5306.jpg")).AbsoluteUri;
            string curr_date = DateTime.Now.ToString("yyyy");
            string issuDate = Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
            string frmDate = this.txtFrmDate.Text;
            string toDate = this.txtToDate.Text;
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            var lst1 = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
            string empIDCard = lst1[0].idcard.ToString();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpMatFirstLetter", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("frmDate", frmDate));
            Rpt1.SetParameters(new ReportParameter("toDate", toDate));
            Rpt1.SetParameters(new ReportParameter("empIDCard", empIDCard));
            Rpt1.SetParameters(new ReportParameter("curDate", curr_date));
            Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
            Rpt1.SetParameters(new ReportParameter("compName", userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি." : comnambn)); //Audit user
            Rpt1.SetParameters(new ReportParameter("compAdd", comaddbn));
            Rpt1.SetParameters(new ReportParameter("comsnm", (comcod == "5305" && userid == "5305139") ? "এফবিএফ" : comcod == "5305" ? "এফবি" : "এফবিএফ")); //Audit user
            Rpt1.SetParameters(new ReportParameter("comaddDetails", comaddDetails));
            Rpt1.SetParameters(new ReportParameter("sign1", comcod == "5305" ? "উপ-ব্যবস্থাপক" : "ব্যবস্থাপক / উপঃ ব্যবস্থাপক"));
            Rpt1.SetParameters(new ReportParameter("sign2", comcod == "5305" ? "সাস্টেনিবিলিটি এন্ড সিএসআর বিভাগ" : "মানব সম্পদ, প্রশাসন ও কমপ্লায়েন্স বিভাগ"));
            Rpt1.SetParameters(new ReportParameter("comLogo", userid == "5305139" ? auditComLogo : comLogo)); //Audit user
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = Rpt1;

            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            int i;
            int row;
            if (((CheckBox)this.grvEmpSep.HeaderRow.FindControl("chkAll")).Checked)
            {

                for (i = 0; i < this.grvEmpSep.Rows.Count; i++)
                {
                    ((CheckBox)this.grvEmpSep.Rows[i].FindControl("chkIndv")).Checked = true;
                    row = (this.grvEmpSep.PageSize * this.grvEmpSep.PageIndex) + i;
                    dt.Rows[row]["chk"] = "True";
                }

            }

            else
            {
                for (i = 0; i < this.grvEmpSep.Rows.Count; i++)
                {
                    ((CheckBox)this.grvEmpSep.Rows[i].FindControl("chkIndv")).Checked = false;
                    row = (this.grvEmpSep.PageSize * this.grvEmpSep.PageIndex) + i;
                    dt.Rows[row]["chk"] = "False";

                }

            }

            Session["tblEmpstatus"] = dt;
        }

        protected void lnkPrintLtr1_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            List<EmpSep01> listsep = new List<EmpSep01>();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    string empId = "";
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            empId += ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvempid")).Text.ToString();
                        }
                    }

                    this.PrintFirstLetterMultiFB(empId);
                    break;

                default:
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            string idcardno = ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvcardnocon")).Text.ToString();
                            GetLetterData(idcardno, listsep);
                        }
                    }

                    PrintFirstLetter(listsep);
                    break;
            }
        }
        public void GetLetterData(string idcardno, List<EmpSep01> listsep)
        {
            string comcod = this.GetComeCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.sp_report_hr_empstatus2", "RPTEMPLATERSTATUS", idcardno, "", "", "", "");
            DataTable dt4 = (DataTable)Session["tblEmpstatus"];
            DataView dv = dt4.DefaultView;
            dv.RowFilter = "idcardno=" + idcardno;
            dt4 = dv.ToTable();
            string empname = (string)ds2.Tables[0].Rows[0]["empname"];
            string Presadd = (string)ds2.Tables[0].Rows[0]["prsentadd"];
            EmpSep01 SingleSep = new EmpSep01();
            string pastoff = (string)ds2.Tables[0].Rows[0]["po"];
            string Thana = (string)ds2.Tables[0].Rows[0]["ps"];
            string Dist = (string)ds2.Tables[0].Rows[0]["dist"];
            string permadd = (string)ds2.Tables[0].Rows[0]["permadd"];
            string empnameeng = (string)ds2.Tables[0].Rows[0]["empnameeng"];
            string SDate = Convert.ToDateTime(dt4.Rows[0]["spdate"]).AddDays(-1).ToString("dd-MMM-yyyy");
            string desg = dt4.Rows[0]["desig"].ToString();
            string dept = dt4.Rows[0]["dept"].ToString();
            string signatory = dt4.Rows[0]["signatory"].ToString();
            string signadesig = dt4.Rows[0]["signadesig"].ToString();
            string duration = dt4.Rows[0]["duration"].ToString();
            string idcard = dt4.Rows[0]["idcardno"].ToString();
            string refno = dt4.Rows[0]["refno"].ToString();
            string joiningdate = Convert.ToDateTime(dt4.Rows[0]["joiningdate"]).ToString("dd-MMM-yyyy");
            string SeparationDate = Convert.ToDateTime(dt4.Rows[0]["spdate"]).ToString("dd-MMM-yyyy");// SDate.Substring(0, 9);
            SingleSep.empname = empname;
            SingleSep.presadd = Presadd;
            SingleSep.sdate = SDate;
            SingleSep.pastoff = pastoff;
            SingleSep.thana = Thana;
            SingleSep.dist = Dist;
            SingleSep.permadd = permadd;
            SingleSep.sepdate = SeparationDate;
            SingleSep.empnameeng = empnameeng;
            SingleSep.desig = desg;
            SingleSep.duration = duration;
            SingleSep.idcard = idcard;
            SingleSep.joiningdate = joiningdate;
            SingleSep.signatory = signatory;
            SingleSep.signadesig = signadesig;
            SingleSep.refno = refno;
            SingleSep.dept = dept;
            listsep.Add(SingleSep);
        }
        public void PrintFirstLetterMultiFB(string empid)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string comaddbn = hst["comaddbn"].ToString();
            string comaddDetails = hst["comadd"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string auditComLogo = new Uri(Server.MapPath(@"~\Image\LOGO5306.jpg")).AbsoluteUri;
            string curr_date = DateTime.Now.ToString("yyyy");
            string issuDate = Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
            string frmDate = this.txtFrmDate.Text;
            string toDate = this.txtToDate.Text;
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            var lst1 = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
            string empIDCard = lst1[0].idcard.ToString();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpLeaveLetterMulti", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("frmDate", frmDate));
            Rpt1.SetParameters(new ReportParameter("toDate", toDate));
            Rpt1.SetParameters(new ReportParameter("empIDCard", empIDCard));
            Rpt1.SetParameters(new ReportParameter("curDate", curr_date));
            Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
            Rpt1.SetParameters(new ReportParameter("compName", userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি." : comnambn)); //Audit user
            Rpt1.SetParameters(new ReportParameter("compAdd", comaddbn));
            Rpt1.SetParameters(new ReportParameter("comsnm", (comcod == "5305" && userid == "5305139") ? "ফুটবেড" : (comcod == "5305" ? "এফবি" : "এফবিএফ"))); //Audit user
            Rpt1.SetParameters(new ReportParameter("comaddDetails", comaddDetails));
            Rpt1.SetParameters(new ReportParameter("sign1", comcod == "5305" ? "" : "ব্যবস্থাপক / উপঃ ব্যবস্থাপক"));
            Rpt1.SetParameters(new ReportParameter("sign2", comcod == "5305" ? "মানব সম্পদ ও কমপ্লায়েন্স বিভাগ" : "মানব সম্পদ, প্রশাসন ও কমপ্লায়েন্স বিভাগ"));
            Rpt1.SetParameters(new ReportParameter("comLogo", userid == "5305139" ? auditComLogo : comLogo)); //Audit user
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = Rpt1;

            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }
        public void PrintFirstLetter(List<EmpSep01> listsep)
        {
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptFirstLetter", listsep, null, null);
            rpt1.EnableExternalImages = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();

            string username = hst["username"].ToString();
            string company = this.ddlWstation.SelectedItem.Text.Trim();
            string Todate = System.DateTime.Now.ToString("dd.MM.yyyy");

            rpt1.SetParameters(new ReportParameter("Todate", Todate));

            Session["Report1"] = rpt1;
            string type = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }
        protected void lnkPrintLtr2_Click(object sender, EventArgs e)
        {
            List<EmpSep01> listsep = new List<EmpSep01>();
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    string empId = "";
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            empId += ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvempid")).Text.ToString();
                        }
                    }

                    this.PrintSecondLetterMultiFB(empId);
                    break;

                default:
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            string idcardno = ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvcardnocon")).Text.ToString();
                            GetLetterData(idcardno, listsep);

                        }
                    }
                    PrintSecondLetter(listsep);
                    break;

            }
        }

        private void PrintSecondLetterMultiFB(string empid)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string comaddbn = hst["comaddbn"].ToString();
            string comaddDetails = hst["comadd"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string auditComLogo = new Uri(Server.MapPath(@"~\Image\LOGO5306.jpg")).AbsoluteUri;
            string curr_date = DateTime.Now.ToString("yyyy");
            string curr_date_full = System.DateTime.Today.ToString("dd/MM/yyyy");
            string frmDate = this.txtFrmDate.Text;
            string toDate = this.txtToDate.Text;
            string issuDate = Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            var lst1 = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
            string empIDCard = lst1[0].idcard.ToString();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpResignLetterMulti", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("frmDate", frmDate));
            Rpt1.SetParameters(new ReportParameter("toDate", toDate));
            Rpt1.SetParameters(new ReportParameter("empIDCard", empIDCard));
            Rpt1.SetParameters(new ReportParameter("curDate", curr_date));
            Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
            Rpt1.SetParameters(new ReportParameter("compName", userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি." : comnambn)); //Audit user
            Rpt1.SetParameters(new ReportParameter("compAdd", comaddbn));
            Rpt1.SetParameters(new ReportParameter("comsnm", (comcod == "5305" && userid == "5305139") ? "এফবিএফ" : comcod == "5305" ? "এফবি" : "এফবিএফ")); //Audit user
            Rpt1.SetParameters(new ReportParameter("comaddDetails", comaddDetails));
            Rpt1.SetParameters(new ReportParameter("sign1", comcod == "5305" ? "" : "ব্যবস্থাপক / উপঃ ব্যবস্থাপক"));
            Rpt1.SetParameters(new ReportParameter("sign2", comcod == "5305" ? "মানব সম্পদ ও কমপ্লায়েন্স বিভাগ" : "মানব সম্পদ, প্রশাসন ও কমপ্লায়েন্স বিভাগ"));
            Rpt1.SetParameters(new ReportParameter("comLogo", userid == "5305139" ? auditComLogo : comLogo)); //Audit user
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = Rpt1;

            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }
        public void PrintSecondLetter(List<EmpSep01> listsep)
        {
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptSecondLetter", listsep, null, null);
            rpt1.EnableExternalImages = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();

            string username = hst["username"].ToString();
            string company = this.ddlWstation.SelectedItem.Text.Trim();
            string Todate = System.DateTime.Now.ToString("dd.MM.yyyy");

            rpt1.SetParameters(new ReportParameter("Todate", Todate));

            Session["Report1"] = rpt1;
            string type = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }
        protected void lnkPrintLtr3_Click(object sender, EventArgs e)
        {
            List<EmpSep01> listsep = new List<EmpSep01>();
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    string empId = "";
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            empId += ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvempid")).Text.ToString();
                        }
                    }

                    this.PrintThirdLetterMultiFB(empId);
                    break;

                default:
                    for (int i = 0; i < grvEmpSep.Rows.Count; i++)
                    {
                        bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                        if (chkper)
                        {
                            string idcardno = ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvcardnocon")).Text.ToString();
                            GetLetterData(idcardno, listsep);

                        }
                    }
                    this.PrintThirdLetter(listsep);
                    break;

            }

        }

        private void PrintThirdLetterMultiFB(string empid)
        {

            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string comaddbn = hst["comaddbn"].ToString();
            string comaddDetails = hst["comadd"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string auditComLogo = new Uri(Server.MapPath(@"~\Image\LOGO5306.jpg")).AbsoluteUri;
            string curr_date = DateTime.Now.ToString("yyyy");
            string curr_date_full = System.DateTime.Today.ToString("dd/MM/yyyy");
            string frmDate = this.txtFrmDate.Text;
            string toDate = this.txtToDate.Text;
            string issuDate = Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            var lst1 = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
            string empIDCard = lst1[0].idcard.ToString();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpSelfSupportMulti", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("frmDate", frmDate));
            Rpt1.SetParameters(new ReportParameter("toDate", toDate));
            Rpt1.SetParameters(new ReportParameter("empIDCard", empIDCard));
            Rpt1.SetParameters(new ReportParameter("curDate", curr_date));
            Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
            Rpt1.SetParameters(new ReportParameter("compName", userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি." : comnambn)); //Audit user
            Rpt1.SetParameters(new ReportParameter("compAdd", comaddbn));
            Rpt1.SetParameters(new ReportParameter("comsnm", (comcod == "5305" && userid == "5305139") ? "ফুটবেড" : comcod == "5305" ? "এফবি" : "এফবিএফ")); //Audit user
            Rpt1.SetParameters(new ReportParameter("comaddDetails", comaddDetails));
            Rpt1.SetParameters(new ReportParameter("sign1", comcod == "5305" ? "" : "ব্যবস্থাপক / উপঃ ব্যবস্থাপক"));
            Rpt1.SetParameters(new ReportParameter("sign2", comcod == "5305" ? "মানব সম্পদ ও কমপ্লায়েন্স বিভাগ" : "মানব সম্পদ, প্রশাসন ও কমপ্লায়েন্স বিভাগ"));
            Rpt1.SetParameters(new ReportParameter("comLogo", userid == "5305139" ? auditComLogo : comLogo)); //Audit user
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = Rpt1;

            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }
        public void PrintThirdLetter(List<EmpSep01> listsep)
        {
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptThirdLetter", listsep, null, null);
            rpt1.EnableExternalImages = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();

            string username = hst["username"].ToString();
            string company = this.ddlWstation.SelectedItem.Text.Trim();
            string Todate = System.DateTime.Now.ToString("dd.MM.yyyy");

            rpt1.SetParameters(new ReportParameter("Todate", Todate));



            Session["Report1"] = rpt1;
            string type = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }
        public void ReportNOC(List<EmpSep01> listsep)
        {
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptNOC", listsep, null, null);
            rpt1.EnableExternalImages = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();

            string username = hst["username"].ToString();
            string company = this.ddlWstation.SelectedItem.Text.Trim();
            string Todate = System.DateTime.Now.ToString("dd.MM.yyyy");

            rpt1.SetParameters(new ReportParameter("Todate", Todate));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));

            Session["Report1"] = rpt1;
            string type = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }

        public void ReportEXCer(List<EmpSep01> listsep)
        {
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEXCer", listsep, null, null);
            rpt1.EnableExternalImages = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();

            string username = hst["username"].ToString();
            string company = this.ddlWstation.SelectedItem.Text.Trim();
            string Todate = System.DateTime.Now.ToString("dd.MM.yyyy");

            rpt1.SetParameters(new ReportParameter("Todate", Todate));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));

            Session["Report1"] = rpt1;
            string type = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }
        public void ReportClearance(List<EmpSep01> listsep)
        {
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptClearance", listsep, null, null);
            rpt1.EnableExternalImages = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();

            string username = hst["username"].ToString();
            string company = this.ddlWstation.SelectedItem.Text.Trim();
            string Todate = System.DateTime.Now.ToString("dd.MM.yyyy");

            rpt1.SetParameters(new ReportParameter("Todate", Todate));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));

            Session["Report1"] = rpt1;
            string type = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }
        public void ReportRessig(List<EmpSep01> listsep)
        {
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptRessig", listsep, null, null);
            rpt1.EnableExternalImages = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();

            string username = hst["username"].ToString();
            string company = this.ddlWstation.SelectedItem.Text.Trim();
            string Todate = System.DateTime.Now.ToString("dd.MM.yyyy");

            rpt1.SetParameters(new ReportParameter("Todate", Todate));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));

            Session["Report1"] = rpt1;
            string type = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);

        }
        protected void lnkPrintLtrClear_Click(object sender, EventArgs e)
        {
            List<EmpSep01> listsep = new List<EmpSep01>();

            for (int i = 0; i < grvEmpSep.Rows.Count; i++)
            {
                bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                if (chkper)
                {
                    string idcardno = ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvcardnocon")).Text.ToString();
                    GetLetterData(idcardno, listsep);

                }
            }
            ReportClearance(listsep);
        }

        protected void lnkPrintLtrRegig_Click(object sender, EventArgs e)
        {
            List<EmpSep01> listsep = new List<EmpSep01>();

            for (int i = 0; i < grvEmpSep.Rows.Count; i++)
            {
                bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                if (chkper)
                {
                    string idcardno = ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvcardnocon")).Text.ToString();
                    GetLetterData(idcardno, listsep);

                }
            }
            ReportRessig(listsep);
        }

        protected void lnkPrintLtrNOC_Click(object sender, EventArgs e)
        {
            List<EmpSep01> listsep = new List<EmpSep01>();

            for (int i = 0; i < grvEmpSep.Rows.Count; i++)
            {
                bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                if (chkper)
                {
                    string idcardno = ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvcardnocon")).Text.ToString();
                    GetLetterData(idcardno, listsep);

                }
            }
            ReportNOC(listsep);
        }

        protected void lnkPrintLtrEXCer_Click(object sender, EventArgs e)
        {
            List<EmpSep01> listsep = new List<EmpSep01>();

            for (int i = 0; i < grvEmpSep.Rows.Count; i++)
            {
                bool chkper = (((CheckBox)grvEmpSep.Rows[i].FindControl("chkIndv")).Checked);
                if (chkper)
                {
                    string idcardno = ((Label)this.grvEmpSep.Rows[i].FindControl("lblgvcardnocon")).Text.ToString();
                    GetLetterData(idcardno, listsep);

                }
            }
            ReportEXCer(listsep);
        }

        protected void chkAllPrint_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblEmpstatus"];

            int i;
            int row;
            if (((CheckBox)this.gvEmpCon.HeaderRow.FindControl("chkAllPrint")).Checked)
            {

                for (i = 0; i < this.gvEmpCon.Rows.Count; i++)
                {
                    ((CheckBox)this.gvEmpCon.Rows[i].FindControl("chkPrint")).Checked = true;
                    row = (this.gvEmpCon.PageSize * this.gvEmpCon.PageIndex) + i;
                    dt.Rows[row]["chk"] = "True";
                }

            }

            else
            {
                for (i = 0; i < this.gvEmpCon.Rows.Count; i++)
                {
                    ((CheckBox)this.gvEmpCon.Rows[i].FindControl("chkPrint")).Checked = false;
                    row = (this.gvEmpCon.PageSize * this.gvEmpCon.PageIndex) + i;
                    dt.Rows[row]["chk"] = "False";

                }

            }


            Session["tblEmpstatus"] = dt;
        }

        protected void gvEmpAging_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpAging.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {

            if (chkAddEmp.Checked)
            {
                this.divAddEmp.Visible = true;
                string quType = this.Request.QueryString["Type"].ToString().Trim();
                if (quType == "EmpList" || quType == "EmpListPic")
                {
                    this.GetEmpListDataSet();
                }
                DataTable dt1 = (DataTable)Session["tblEmpstatus"];
                Session.Remove("tblemp");
                this.CreateDataTable();
                DataTable dt = (DataTable)Session["tblemp"];
                //dt = dt1.Copy();
                this.ddlEmployee.Items.Clear();
                foreach (DataRow dr1 in dt1.Rows)

                {

                    string empid = dr1["empid"].ToString();
                    if (dt.Select("empid='" + empid + "'").Length == 0)
                    {

                        DataRow dra = dt.NewRow();
                        dra["empid"] = dr1["empid"].ToString();
                        dra["idcard"] = dr1["idcardno"].ToString();
                        dra["empname"] = dr1["idcardno"].ToString() + "-" + dr1["empname"].ToString();
                        dt.Rows.Add(dra);
                    }


                    //dt1.AddDat

                }
                Session.Remove("tblEmpstatus");
                Session.Remove("tbladdEmpstatus");
                DataTable dt2 = dt1.Copy();
                Session["tbladdEmpstatus"] = dt2;
                DataTable dt3 = dt1.Clone();
                Session["tblEmpstatus"] = dt3;

                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = dt;
                this.DataBind();
                this.LoadGrid();




            }
            else
            {
                this.divAddEmp.Visible = false;
            }
        }


        private void CreateDataTable()
        {
            if (Session["tblemp"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("empid", Type.GetType("System.String"));
                dt.Columns.Add("empname", Type.GetType("System.String"));
                dt.Columns.Add("idcard", Type.GetType("System.String"));
                Session["tblemp"] = dt;

            }
        }
        protected void lnkbtnAddEmp_Click(object sender, EventArgs e)
        {

            try
            {

                DataTable dt = (DataTable)Session["tblEmpstatus"];
                DataTable dtadd = (DataTable)Session["tbladdEmpstatus"];
                string empid = this.ddlEmployee.SelectedValue.ToString();
                DataRow[] dr1 = dt.Select("empid='" + empid + "'");
                if (dr1.Length == 0)
                {
                    DataRow[] dra = dtadd.Select("empid='" + empid + "'");
                    dt.ImportRow(dra[0]);
                }
                else
                {

                    string existempdet = "Employee : " + dr1[0]["idcardno"].ToString() + " - " + dr1[0]["empname"].ToString() + " already existed!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + existempdet + "');", true);
                }
                DataView dv = dt.DefaultView;
                dv.Sort = ("secid,idcardno");
                Session["tblEmpstatus"] = dv.ToTable();
                this.LoadGrid();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }

        protected void gvEmpList_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }

            DataTable dt = (DataTable)Session["tblEmpstatus"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            gvEmpList.DataSource = sortedView;
            gvEmpList.DataBind();

            Session["tblEmpstatus"] = sortedView.ToTable();
        }
        public System.Web.UI.WebControls.SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (System.Web.UI.WebControls.SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void grvEmpSep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvEmpSep.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void gvEmpList_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpList.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

    }

}