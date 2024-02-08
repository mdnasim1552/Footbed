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
using System.IO;

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class RptEmpInformation02 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        public List<LocalReport> reportList = new List<LocalReport>();
        public static string userid = "";
        public static string comLogo = "";
        public static Tuple<string, string> companyInfoBn = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE INFORMATION";
                GetWorkStation();
                GetAllOrganogramList();                
                this.SelectView();
                this.GetLineDDL();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                userid = hst["usrid"].ToString();
                companyInfoBn = userid == "5305139" ? ASTUtility.CompInfoBnForFootbed() : ASTUtility.CompInfoBn();
                string comcod = this.GetComeCode();
                comLogo = userid == "5305139" ? new Uri(Server.MapPath(@"~\Image\LOGO" + "5306" + ".jpg")).AbsoluteUri : new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                //this.lnkOk_Click(null, null);
            }
        }

        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"] ?? "";
            if (Type == "AllDoc")
            {
                this.divIssuDate.Visible = true;
                this.txtIssuDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else if (Type == "Datewise")
            {
                //this.Okbtn.Visible = true;
                this.Daterange.Visible = true;
                this.Daterange2.Visible = true;
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.divIssuDate.Visible = true;
                this.txtIssuDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
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
            string empStatus = this.ddlEmpStatus.SelectedValue.ToString();
            string linecode = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPNAMEALLDOC", emptype, division, deptid, section, empStatus, linecode, userid, "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds3.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();

            ViewState["empname"] = ds3.Tables[0];
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "AllDoc":
                    this.PrintAllDocFB();
                    break;

                case "Datewise":
                    if (CheckBox1.Checked)
                    {
                        reportList.Clear();

                        foreach (ListItem item in ddlEmpNameAllInfo.Items)
                        {
                            if (item.Selected)
                            {
                                foreach (ListItem litem in ReportListBox.Items)
                                {

                                    if (litem.Selected)
                                    {
                                        this.PrintAllDocFBDatewiseMerge(GetReportType(litem.Value), item.Value);

                                    }

                                }
                            }
                        }

                        //List<string> reportListBox = new List<string>();

                        if (reportList.Count > 0)
                        {
                            Session["Report1"] = reportList;
                            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=MERGEPDF', target='_blank');</script>";
                        }
                        else
                        {
                            return;
                        }
                        
                    }
                    else
                    {
                        this.PrintAllDocFBDatewise();
                    }
                    break;
            }
        }
        private void PrintAllDocFBDatewiseMerge(string reportTye="", string empidN = "")
        {
            string comcod = this.GetComeCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%"; ;
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString();
            
            string frmdate = this.txtfromdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();
            
            string rptFormat = this.ddlRptFormat.SelectedValue.ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comnamBn = hst["comnambn"].ToString();
            string comaddBn = hst["comaddbn"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ws = this.ddlWstation.SelectedValue.Substring(0, 4);
            string printFooter = "Printed from Computer Address :" + compname + " ,User: " + username + " ,Time: " + printdate;
            string issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
            //string reportTye = GetReportType();

            string signatory = "";
            LocalReport Rpt1 = new LocalReport();
            if (reportTye == "ApplicationForm")
            {
                string compName = companyInfoBn.Item1;
                string compAdd = companyInfoBn.Item2;
                string curDate = System.DateTime.Today.ToString("yyyy");

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                //Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpApplicationForm", RptListBn, null, null);

                Rpt1.LoadReportDefinition(RPTPathClass.GetReportFilePath("RD_81_HRM.RD_81_Rec.RptEmpApplicationForm"));
                Rpt1 = Rpt1.SetRDLCReportDatasets(new Dictionary<string, object> { { "DataSet1", RptListBn } });
                
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", compName));
                Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            }
            else if (reportTye == "AppoinmentLetter")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "RPTALLEMPLISDATEWISE", emptype, div, empidN, deptname, section, frmdate, todate, "", "");
                if (ds3 == null)
                    return;

                ViewState["tblempid"] = ds3.Tables[0];
                DataTable dt1 = (DataTable)ViewState["tblempid"];

                double salamt = 0;
                foreach (DataRow dr1 in dt1.Rows)
                {
                    salamt = Convert.ToDouble(dr1["totalsal"].ToString());

                    string tkinword = ASTUtility.Trans(salamt, 2);
                    dr1["tkinword"] = ASTUtility.Trans(salamt, 2);
                }

                var lst1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.AllEmpInformationGrpwise>();
                switch (rptFormat)
                {
                    //Executive
                    case "0":
                        issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("yyyy-MM");
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAppoinmentLetterExc", lst1, null, null);
                        Rpt1.EnableExternalImages = true;
                        comnam = lst1[0].compnameeng.ToString();
                        comadd = lst1[0].compaddeng.ToString();
                        Rpt1.SetParameters(new ReportParameter("printdate", System.DateTime.Today.ToString("dd-MMM-yyyy")));
                        Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                        break;

                    //Factory Staff
                    case "1":
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAppoinmentLetterNonOT", lst1, null, null);
                        Rpt1.EnableExternalImages = true;
                        comnam = lst1[0].companyname.ToString();
                        comadd = lst1[0].companyaddbn.ToString();
                        break;

                    //Worker
                    default:
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAppoinmentLetterNonExc", lst1, null, null);
                        Rpt1.EnableExternalImages = true;
                        //comnam = lst1[0].companyname.ToString();
                        comnam = userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি." : lst1[0].compnameeng.ToString();
                        comadd = lst1[0].companyaddbn.ToString();
                        Rpt1.SetParameters(new ReportParameter("CSN", comcod == "5305" ? "এফবি" : "এফবিএফ"));
                        break;
                }

                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("workType", ""));
                Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            }
            else if (reportTye == "WorkerConfirmLetter")
            {
                string DGMSign = new Uri(Server.MapPath(@"~\Image\DGMSign.png")).AbsoluteUri;
                string curDate = System.DateTime.Today.ToString("yyyy");
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                if (comcod == "5305")
                {
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptWorkerConfirmLtrMultiFB", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compSName", "এফবি"));
                    Rpt1.SetParameters(new ReportParameter("compName", comnam));
                }
                else
                {
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptWorkerConfirmLtrMulti", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compSName", "ফুটবেড"));
                }

                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("curDate", curDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("DGMSign", DGMSign));
            }
            else if (reportTye == "EvaluationForm")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                switch (rptFormat)
                {
                    //Executive
                    case "0":
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEvaluationFormStaff", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("compName", comnam));
                        break;

                    //Factory Staff
                    case "1":
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEvaluationFormStaff", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("compName", comnam));
                        break;

                    //Worker
                    default:
                        issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("yyyy");
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEvaluationFormWorker", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                        Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                        break;
                }

                string curDate = System.DateTime.Today.ToString("yyyy");
                Rpt1.SetParameters(new ReportParameter("curDate", curDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            }
            else if (reportTye == "CPF" && (rptFormat == "0" || rptFormat == "1"))
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPFMultiEN", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            }
            else if (reportTye == "CPF")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPFMulti", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            }
            else if (reportTye == "CPF2" && (rptFormat == "0" || rptFormat == "1"))
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF2MultiEN", RptListBn, null, null);
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
            }
            else if (reportTye == "CPF2")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF2Multi", RptListBn, null, null);
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
            }
            else if (reportTye == "CPF3" && (rptFormat == "0" || rptFormat == "1"))
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF3MultiEN", RptListBn, null, null);
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
            }
            else if (reportTye == "CPF3")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF3Multi", RptListBn, null, null);
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
            }
            else if (reportTye == "NomineeForm")
            {
                string compName = companyInfoBn.Item1;
                string compAdd = companyInfoBn.Item2;

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptNomineeForm", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", compName));
                Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            }
            else if (reportTye == "AgeForm")
            {
                string compName = companyInfoBn.Item1;
                string compAdd = companyInfoBn.Item2;
                issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAgeForm", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", compName));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            }
            else if (reportTye == "TrainingForm")
            {
                string compName = companyInfoBn.Item1;
                string compAdd = companyInfoBn.Item2;

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptTrainingForm", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", compName));
                Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            }
            else if (reportTye == "JoinLetter")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "RPTALLEMPLISDATEWISE", emptype, div, empidN, deptname, section, frmdate, todate, "", "");
                if (ds3 == null)
                    return;

                ViewState["tblempid"] = ds3.Tables[0];
                DataTable dt1 = (DataTable)ViewState["tblempid"];

                double salamt = 0;
                foreach (DataRow dr1 in dt1.Rows)
                {
                    salamt = Convert.ToDouble(dr1["totalsal"].ToString());

                    string tkinword = ASTUtility.Trans(salamt, 2);
                    dr1["tkinword"] = ASTUtility.Trans(salamt, 2);
                }

                var lst1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.AllEmpInformationGrpwise>();
                switch (ws)
                {
                    case "9403":
                    case "9404":
                    case "9405":
                    case "9406":
                    case "9407":
                    case "9408":
                    case "9409":
                    case "9410":
                    case "9413": // Worker
                    case "9414": // Worker
                    case "9415": // Worker
                    case "9416": // Worker
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptLetterJoiningExc", lst1, null, null);
                        Rpt1.EnableExternalImages = true;
                        comnam = lst1[0].companyname.ToString();
                        comadd = lst1[0].companyaddbn.ToString();

                        break;

                    default:
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptLetterJoiningExc", lst1, null, null);
                        Rpt1.EnableExternalImages = true;
                        comnam = lst1[0].compnameeng.ToString();
                        comadd = lst1[0].compaddeng.ToString();
                        break;

                }

                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            }
            else if (reportTye == "EnvelopePerBangla")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopeBan", RptListBn, null, null);

                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comaddBn));
            }
            else if (reportTye == "EnvelopePerEnglish")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopeEng", RptListBn, null, null);

                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            }
            else if (reportTye == "EnvelopePresentBangla")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopePresentBan", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comaddBn));
            }
            else if (reportTye == "EnvelopePresentEnglish")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopePresentEng", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            }
            else if (reportTye == "OfficeEnvelop")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptOfficeEnvelop", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
            }
            else if (reportTye == "BankOpening")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                switch (comcod)
                {

                    //FB
                    case "5305":
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptBankOpening", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                        Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MMM-yyyy")));
                        break;

                    //Footbed
                    default:
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptBankOpeningFootbed", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                        Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                        Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MMM-yyyy")));
                        break;
                }
            }
            else if (reportTye == "appointmenttopsheet")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptAppsPart", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("rpttitle", "Applicant's Part"));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            }
            else if (reportTye == "salcertificate")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                foreach (var item in RptListBn)
                {
                    item.inword = ASTUtility.Trans((item.grssal) - (item.totaldeduc), 0);
                }

                string signid = this.DdlSignatory.SelectedValue;
                DataTable dt = (DataTable)Session["tblSignatory"];
                string signName = dt.Select("idcard='" + signid + "'")[0]["signamenly"].ToString();
                string signDesig = dt.Select("idcard='" + signid + "'")[0]["signdesig"].ToString();

                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmsalcertificate", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("signName", signName));
                Rpt1.SetParameters(new ReportParameter("signDesig", signDesig));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
            }
            else if (reportTye == "ProbEvaluationForm")
            {
                issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, issuDate, "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                string signid = this.DdlSignatory.SelectedValue;
                DataTable dt = (DataTable)Session["tblSignatory"];
                string signName = dt.Select("idcard='" + signid + "'")[0]["signamenly"].ToString();
                string signDesig = dt.Select("idcard='" + signid + "'")[0]["signdesig"].ToString();

                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptProbEvaluationForm", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("signName", signName));
                Rpt1.SetParameters(new ReportParameter("signDesig", signDesig));
            }
            else if(reportTye == "Suspension")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "EMPATTNIDWISEAUDIT2HRSOT", frmdate, todate, empidN, "Card2Hrs", reportTye, "", "", "", "");
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    return;
                }
                DataTable dt1 = ds1.Tables[0];
                int totalabs = 0;
                foreach (DataRow row in dt1.Rows)
                {
                    string paldate = row["paldate"].ToString();
                    int lenOfpaldate = paldate.Length;
                    string months = ASITUtility02.GetFullMonthName(paldate.Substring(0, lenOfpaldate - 3));
                    string years = ASITUtility02.NumBn(paldate.Substring(lenOfpaldate - 2));
                    row["paldate"] = months + "-" + years;
                    totalabs = totalabs + (int)row["absents"];
                    //row["absents"] = ASITUtility02.NumBn(row["absents"].ToString());
                }
                DateTime startDate = Convert.ToDateTime(this.txtfromdate.Text);
                DateTime endDate = Convert.ToDateTime(this.txttodate.Text);
                int monthsDifference = ((endDate.Year - startDate.Year) * 12) + (endDate.Month - startDate.Month) + 1;
                string sfrmdate = ASITUtility02.GetMonthNameDigit(startDate.ToString("MM")) + "-" + ASITUtility02.NumBn(startDate.ToString("yyyy"));
                string stodate = ASITUtility02.GetMonthNameDigit(endDate.ToString("MM")) + "-" + ASITUtility02.NumBn(endDate.ToString("yyyy"));

                var RptList1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.SuspensionEmpAbsInfo>();
                var RptList2 = ds1.Tables[1].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.SuspensionEmpInfo>();
                //Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpApplicationForm", RptListBn, null, null);issuDate
                string issueDateSuspension = ASITUtility02.NumBn(issuDate.Substring(0, 2)) + "/" + ASITUtility02.NumBn(issuDate.Substring(3, 2)) + "/" + ASITUtility02.NumBn(issuDate.Substring(6, 4)) + " ইং";
                Rpt1.LoadReportDefinition(RPTPathClass.GetReportFilePath("RD_81_HRM.RD_81_Rec.RptSuspension"));
                Rpt1 = Rpt1.SetRDLCReportDatasets(new Dictionary<string, object> { { "DataSet1", RptList1 }, { "DataSet2", RptList2 } });
                Rpt1.SetParameters(new ReportParameter("issueDateSuspension", issueDateSuspension));
                Rpt1.SetParameters(new ReportParameter("sfrmdate", sfrmdate));
                Rpt1.SetParameters(new ReportParameter("stodate", stodate));
                Rpt1.SetParameters(new ReportParameter("totalabs", ASITUtility02.NumBn(totalabs.ToString())));
                Rpt1.SetParameters(new ReportParameter("monthsDifference", ASITUtility02.NumBn(monthsDifference.ToString())));
            }
            reportList.Add(Rpt1);


        }


        private void PrintAllDocFBDatewise()
        {
            string comcod = this.GetComeCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%"; ;
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString();
            string empidN = ""; 
            string frmdate = this.txtfromdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();
            string[] empid = this.ddlEmpNameAllInfo.Text.Trim().Split(',');
            if (empid[0] == "")
            {
                return;
            }

            if (empid[0].Substring(0, 3) == "000")
                empidN = "";
            else
                foreach (ListItem item in ddlEmpNameAllInfo.Items)
                {
                    if (item.Selected)
                    {
                        empidN += item.Value;
                    }
                }
            string rptFormat = this.ddlRptFormat.SelectedValue.ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comnamBn = hst["comnambn"].ToString();
            string comaddBn = hst["comaddbn"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            
            string ws = this.ddlWstation.SelectedValue.Substring(0, 4);
            string printFooter = "Printed from Computer Address :" + compname + " ,User: " + username + " ,Time: " + printdate;
            string issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
            string reportTye = GetReportType();
            string signatory = "";
            LocalReport Rpt1 = new LocalReport();
            if (reportTye == "ApplicationForm")
            {
                string compName = companyInfoBn.Item1;
                string compAdd = companyInfoBn.Item2;
                string curDate = System.DateTime.Today.ToString("yyyy");

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpApplicationForm", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", compName));
                Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            }
            else if (reportTye == "AppoinmentLetter")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "RPTALLEMPLISDATEWISE", emptype, div, empidN, deptname, section, frmdate, todate, "", "");
                if (ds3 == null)
                    return;

                ViewState["tblempid"] = ds3.Tables[0];
                DataTable dt1 = (DataTable)ViewState["tblempid"];

                double salamt = 0;
                foreach (DataRow dr1 in dt1.Rows)
                {
                    salamt = Convert.ToDouble(dr1["totalsal"].ToString());

                    string tkinword = ASTUtility.Trans(salamt, 2);
                    dr1["tkinword"] = ASTUtility.Trans(salamt, 2);
                }

                var lst1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.AllEmpInformationGrpwise>();
                switch (rptFormat)
                {
                    //Executive
                    case "0":
                        issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("yyyy-MM");
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAppoinmentLetterExc", lst1, null, null);
                        Rpt1.EnableExternalImages = true;
                        comnam = userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি.": lst1[0].compnameeng.ToString();
                        comadd = lst1[0].compaddeng.ToString();
                        Rpt1.SetParameters(new ReportParameter("printdate", System.DateTime.Today.ToString("dd-MMM-yyyy")));
                        Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                        break;

                    //Factory Staff
                    case "1":
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAppoinmentLetterNonOT", lst1, null, null);
                        Rpt1.EnableExternalImages = true;
                        comnam = userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি.": lst1[0].companyname.ToString();
                        comadd = lst1[0].companyaddbn.ToString();
                        break;

                    //Worker
                    default:
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAppoinmentLetterNonExc", lst1, null, null);
                        Rpt1.EnableExternalImages = true;
                        comnam = userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি.": lst1[0].companyname.ToString();//userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি." : comnambn
                        comadd = lst1[0].companyaddbn.ToString();
                        Rpt1.SetParameters(new ReportParameter("CSN", comcod == "5305" ? "এফবি" : "এফবিএফ"));
                        break;
                }              

                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("workType", ""));
                Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            }
            else if (reportTye == "WorkerConfirmLetter")
            {
                string DGMSign = new Uri(Server.MapPath(@"~\Image\DGMSign.png")).AbsoluteUri;
                string curDate = System.DateTime.Today.ToString("yyyy");
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                if (comcod == "5305")
                {
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptWorkerConfirmLtrMultiFB", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compSName", "এফবি"));
                    Rpt1.SetParameters(new ReportParameter("compName", comnam));
                }
                else
                {
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptWorkerConfirmLtrMulti", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compSName", "ফুটবেড"));
                }

                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("curDate", curDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("DGMSign", DGMSign));
            }
            else if (reportTye == "WorkerExtensionLetter")//WorkerExtensionLetter
            {
                string DGMSign = new Uri(Server.MapPath(@"~\Image\DGMSign.png")).AbsoluteUri;
                string curDate = System.DateTime.Today.ToString("yyyy");
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1.LoadReportDefinition(RPTPathClass.GetReportFilePath("RD_81_HRM.RD_81_Rec.RptWorkersExtensionLetter"));
                Rpt1 = Rpt1.SetRDLCReportDatasets(new Dictionary<string, object> { { "DataSet1", RptListBn } });
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compSName", "এফবি"));
                Rpt1.SetParameters(new ReportParameter("compName", comnam));

                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("curDate", curDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("DGMSign", DGMSign));
            }
            else if (reportTye == "EvaluationForm")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();               
                switch (rptFormat)
                {
                    //Executive
                    case "0":
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEvaluationFormStaff", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("compName", comnam));
                        break;

                    //Factory Staff
                    case "1":
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEvaluationFormStaff", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("compName", comnam));
                        break;

                    //Worker
                    default:
                        issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("yyyy");
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEvaluationFormWorker", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                        Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                        break;
                }

                string curDate = System.DateTime.Today.ToString("yyyy");
                Rpt1.SetParameters(new ReportParameter("curDate", curDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            }
            else if (reportTye == "CPF" && (rptFormat == "0" || rptFormat == "1"))
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPFMultiEN", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            }
            else if (reportTye == "CPF")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPFMulti", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            }
            else if (reportTye == "CPF2" && (rptFormat == "0" || rptFormat == "1"))
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF2MultiEN", RptListBn, null, null);
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
            }
            else if (reportTye == "CPF2")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF2Multi", RptListBn, null, null);
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
            }
            else if (reportTye == "CPF3" && (rptFormat == "0" || rptFormat == "1"))
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF3MultiEN", RptListBn, null, null);
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
            }
            else if (reportTye == "CPF3")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds3 == null)
                    return;
                var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF3Multi", RptListBn, null, null);
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
            }
            else if (reportTye == "NomineeForm")
            {
                string compName = companyInfoBn.Item1;
                string compAdd = companyInfoBn.Item2;

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptNomineeForm", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", compName));
                Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            }
            else if (reportTye == "AgeForm")
            {
                string compName = companyInfoBn.Item1;
                string compAdd = companyInfoBn.Item2;
                issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAgeForm", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", compName));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            }
            else if (reportTye == "TrainingForm")
            {
                string compName = companyInfoBn.Item1;
                string compAdd = companyInfoBn.Item2;

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptTrainingForm", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", compName));
                Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            }
            else if (reportTye == "JoinLetter")
            {
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "RPTALLEMPLISDATEWISE", emptype, div, empidN, deptname, section, frmdate, todate, "", "");
                if (ds3 == null)
                    return;

                ViewState["tblempid"] = ds3.Tables[0];
                DataTable dt1 = (DataTable)ViewState["tblempid"];

                double salamt = 0;
                foreach (DataRow dr1 in dt1.Rows)
                {
                    salamt = Convert.ToDouble(dr1["totalsal"].ToString());

                    string tkinword = ASTUtility.Trans(salamt, 2);
                    dr1["tkinword"] = ASTUtility.Trans(salamt, 2);
                }

                var lst1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.AllEmpInformationGrpwise>();
                switch (ws)
                {
                    case "9403":
                    case "9404":
                    case "9405":
                    case "9406":
                    case "9407":
                    case "9408":
                    case "9409":
                    case "9410":
                    case "9413": // Worker
                    case "9414": // Worker
                    case "9415": // Worker
                    case "9416": // Worker
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptLetterJoiningExc", lst1, null, null);
                        Rpt1.EnableExternalImages = true;
                        comnam = lst1[0].companyname.ToString();
                        comadd = lst1[0].companyaddbn.ToString();

                        break;

                    default:
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptLetterJoiningExc", lst1, null, null);
                        Rpt1.EnableExternalImages = true;
                        comnam = lst1[0].compnameeng.ToString();
                        comadd = lst1[0].compaddeng.ToString();
                        break;

                }

                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            }
            else if (reportTye == "EnvelopePerBangla")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopeBan", RptListBn, null, null);

                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comaddBn));
            }
            else if (reportTye == "EnvelopePerEnglish")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopeEng", RptListBn, null, null);

                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            }
            else if (reportTye == "EnvelopePresentBangla")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopePresentBan", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comaddBn));
            }
            else if (reportTye == "EnvelopePresentEnglish")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopePresentEng", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            }
            else if (reportTye == "OfficeEnvelop")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptOfficeEnvelop", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
            }
            else if (reportTye == "BankOpening")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                switch (comcod)
                {

                    //FB
                    case "5305":
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptBankOpening", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                        Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MMM-yyyy")));
                        break;
                    
                     //Footbed
                    default:
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptBankOpeningFootbed", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                        Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                        Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MMM-yyyy")));
                        break;
                }             
            }
            else if (reportTye == "appointmenttopsheet")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptAppsPart", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("rpttitle", "Applicant's Part"));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            }
            else if (reportTye == "salcertificate")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidN, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                foreach (var item in RptListBn)
                {
                    item.inword = ASTUtility.Trans((item.grssal) - (item.totaldeduc), 0);
                }

                string signid = this.DdlSignatory.SelectedValue;
                DataTable dt = (DataTable)Session["tblSignatory"];
                string signName = dt.Select("idcard='" + signid + "'")[0]["signamenly"].ToString();
                string signDesig = dt.Select("idcard='" + signid + "'")[0]["signdesig"].ToString();

                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmsalcertificate", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam)); 
                Rpt1.SetParameters(new ReportParameter("signName", signName));
                Rpt1.SetParameters(new ReportParameter("signDesig", signDesig));
                Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
            }
            else if (reportTye == "ProbEvaluationForm")
            {
                issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidN, issuDate, "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                string signid = this.DdlSignatory.SelectedValue;
                DataTable dt = (DataTable)Session["tblSignatory"];
                string signName = dt.Select("idcard='" + signid + "'")[0]["signamenly"].ToString();
                string signDesig = dt.Select("idcard='" + signid + "'")[0]["signdesig"].ToString();

                var RptListBn = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptProbEvaluationForm", RptListBn, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                Rpt1.SetParameters(new ReportParameter("signName", signName));
                Rpt1.SetParameters(new ReportParameter("signDesig", signDesig));
            }
            else if (reportTye == "Suspension")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "EMPATTNIDWISEAUDIT2HRSOT", frmdate, todate, empidN, "Card2Hrs", reportTye, "", "", "", "");
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    return;
                }
                DataTable dt1 = ds1.Tables[0];
                int totalabs = 0;
                foreach (DataRow row in dt1.Rows)
                {
                    string paldate = row["paldate"].ToString();
                    int lenOfpaldate = paldate.Length;
                    string months = ASITUtility02.GetFullMonthName(paldate.Substring(0, lenOfpaldate - 3));
                    string years = ASITUtility02.NumBn(paldate.Substring(lenOfpaldate - 2));
                    row["paldate"] = months + "-" + years;
                    totalabs = totalabs + (int)row["absents"];
                    //row["absents"] = ASITUtility02.NumBn(row["absents"].ToString());
                }
                DateTime startDate = Convert.ToDateTime(this.txtfromdate.Text);
                DateTime endDate = Convert.ToDateTime(this.txttodate.Text);
                int monthsDifference = ((endDate.Year - startDate.Year) * 12) + (endDate.Month - startDate.Month)+1;
                string sfrmdate = ASITUtility02.GetMonthNameDigit(startDate.ToString("MM")) + "-" + ASITUtility02.NumBn(startDate.ToString("yyyy"));
                string stodate = ASITUtility02.GetMonthNameDigit(endDate.ToString("MM")) + "-" + ASITUtility02.NumBn(endDate.ToString("yyyy"));
                
                var RptList1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.SuspensionEmpAbsInfo>();
                var RptList2 = ds1.Tables[1].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.SuspensionEmpInfo>();
                //Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpApplicationForm", RptListBn, null, null);issuDate
                string issueDateSuspension = ASITUtility02.NumBn(issuDate.Substring(0, 2)) + "/" + ASITUtility02.NumBn(issuDate.Substring(3, 2)) + "/" + ASITUtility02.NumBn(issuDate.Substring(6, 4)) + " ইং";
                Rpt1.LoadReportDefinition(RPTPathClass.GetReportFilePath("RD_81_HRM.RD_81_Rec.RptSuspension"));
                Rpt1 = Rpt1.SetRDLCReportDatasets(new Dictionary<string, object> { { "DataSet1", RptList1 }, { "DataSet2", RptList2 } });
                Rpt1.SetParameters(new ReportParameter("issueDateSuspension", issueDateSuspension));
                Rpt1.SetParameters(new ReportParameter("sfrmdate", sfrmdate));
                Rpt1.SetParameters(new ReportParameter("stodate", stodate));
                Rpt1.SetParameters(new ReportParameter("totalabs", ASITUtility02.NumBn(totalabs.ToString())));
                Rpt1.SetParameters(new ReportParameter("monthsDifference", ASITUtility02.NumBn(monthsDifference.ToString())));
            }
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintAllDocFB()
        {

            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComeCode();
                string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
                string empidmulti = "";              
                foreach (ListItem item in ddlEmpNameAllInfo.Items)
                {
                    if (item.Selected)
                    {
                        empidmulti += item.Value;
                    }
                }            

                string username = hst["username"].ToString();
                string comnam = hst["comnam"].ToString();
                string comnamBn = hst["comnambn"].ToString();
                string comaddBn = hst["comaddbn"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string empType = this.ddlWstation.SelectedValue.Substring(0, 4);   
                var empNames = this.ddlEmpNameAllInfo.SelectedItem.Text.Trim().Split('-');
                var empName = empNames[1];
                var empSl = empNames[0];
                string rptFormat = this.ddlRptFormat.SelectedValue.ToString();
                string issuDate = this.txtIssuDate.Text == "" ? "" : (ASITUtility02.NumBn(txtIssuDate.Text.Substring(0, 2)) + "-" + ASITUtility02.GetMonthShortName(txtIssuDate.Text.Substring(3, 3)) + "-" + ASITUtility02.NumBn(txtIssuDate.Text.Substring(6))).ToString();
                string reportTye = GetReportType();
                string comadd = hst["comaddf"].ToString();
                LocalReport Rpt1 = new LocalReport();
                if(reportTye=="ApplicationForm")
                {
                    string compName = companyInfoBn.Item1;
                    string compAdd = companyInfoBn.Item2;
                    issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpApplicationForm", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compName", compName));
                    Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
                    Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                    Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                }
                else if (reportTye == "AppoinmentLetter")
                {                   

                    DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
                    if (ds1 == null)
                        return;
                    DataTable dt = ds1.Tables[0];
                    var listBN = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformationBn>();
                    string deptName = (ds1.Tables[2].Rows.Count == 0) ? "" : ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
                    string netSal = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0.00;(#,##0.00); ");
                    string deptprestring = "";

                    double Bsal = 0.00;
                    double HRent = 0.00;
                    double MedAllow = 0.00;
                    double Conv = 0.00;
                    double foodall = 0.00;
                    foreach (var item in listBN) //For Salary
                    {
                        if (item.gcod == "04001")
                        {
                            Bsal = Convert.ToDouble(item.amt);
                        }
                        else if (item.gcod == "04002")
                        {
                            HRent = Convert.ToDouble(item.amt);
                        }
                        else if (item.gcod == "04003")
                        {
                            MedAllow = Convert.ToDouble(item.amt);
                        }
                        else if (item.gcod == "04004")
                        {
                            Conv = Convert.ToDouble(item.amt);
                        }
                        else if (item.gcod == "04020")
                        {
                            foodall = Convert.ToDouble(item.amt);
                        }

                    }

                    string TotalAllow = Convert.ToDouble(Convert.ToDouble(Bsal) + Convert.ToDouble(HRent) + Convert.ToDouble(MedAllow) +
                                        Convert.ToDouble(Conv) + Convert.ToDouble(foodall)).ToString("#,##0.00;(#,##0.00); ");
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    string jDate = Convert.ToDateTime(ds3.Tables[0].Rows[0]["jointdat"]).ToString("dd-MM-yyyy");
                    var dob = Convert.ToDateTime(ds3.Tables[0].Rows[0]["dob"]).ToString("dd-MMM-yyyy");
                    var dates = jDate.Split('-');
                    string firstnum = ASITUtility02.NumBn(dates[0]);
                    string middlenum = ASITUtility02.NumBn(dates[1]);
                    string lastnum = ASITUtility02.NumBn(dates[2]);
                    string predist = ds3.Tables[0].Rows[0]["predistname"].ToString();
                    string preupzi = ds3.Tables[0].Rows[0]["preupzilname"].ToString();
                    string prepost = ds3.Tables[0].Rows[0]["prepostname"].ToString();
                    string previll = ds3.Tables[0].Rows[0]["previllname"].ToString();
                    string perdist = ds3.Tables[0].Rows[0]["perdistname"].ToString();
                    string perupzi = ds3.Tables[0].Rows[0]["perupzilname"].ToString();
                    string perpost = ds3.Tables[0].Rows[0]["perpostname"].ToString();
                    string pervill = ds3.Tables[0].Rows[0]["pervillname"].ToString();
                    string empGrade = ds3.Tables[0].Rows[0]["grade"].ToString();
                    string desigcod = ds3.Tables[0].Rows[0]["desigcod"].ToString();
                    string empConmfDate = Convert.ToDateTime(ds3.Tables[0].Rows[0]["confrmdat1"]).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : ds3.Tables[0].Rows[0]["confrmdat1"].ToString();
                    string workType = ds3.Tables[0].Rows[0]["worktype"].ToString();
                    var bdates = dob.Split('-');
                    string firstbdates = ASITUtility02.NumBn(bdates[0]);
                    string lastbdates = ASITUtility02.NumBn(bdates[2]);
                    //ds4 not used only for list
                    string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%"; ;
                    string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                    string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                    string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString();
                    string empidN = (empid == "000000000000") ? "%" : empid + "%";

                    empName = ds1.Tables[0].Rows[1]["gdescbn"].ToString();
                    deptName = (ds1.Tables[2].Rows.Count == 0) ? "" : ds1.Tables[2].Rows[0]["empdeptdescbn"].ToString();
                    string empIDCard = ds3.Tables[0].Rows[0]["idcard"].ToString();
                    string empSection = ds3.Tables[0].Rows[0]["sectiondesc"].ToString();
                    string lineDesc = ds3.Tables[0].Rows[0]["linedescb"].ToString();
                    string empDesigDesc = ds3.Tables[0].Rows[0]["desigdesc"].ToString();
                    string promdesig = ds3.Tables[0].Rows[0]["promdesig"].ToString();
                    string fathernam = ds3.Tables[0].Rows[0]["fathername"].ToString();
                    string mothernam = ds3.Tables[0].Rows[0]["mothername"].ToString();
                    string bdate = (firstbdates + "-" + ASITUtility02.GetMonthName(bdates[1]) + "-" + lastbdates).ToString();
                    string jnDate = (firstnum + "/" + middlenum + "/" + lastnum).ToString();
                    switch (rptFormat)
                    {
                        //Executive
                        case "0":
                            //For Same SP Calling
                             issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("yyyy-MM");
                            string frmdate = "01-Jan-2000";
                            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "RPTALLEMPLISDATEWISE", emptype, div, empidmulti, deptname, section, frmdate, todate, "", "");
                            if (ds4 == null)
                                return;

                            DataTable dt1 = ds4.Tables[0];
                            double salamt = 0;
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                salamt = Convert.ToDouble(dr1["totalsal"].ToString());

                                string tkinword = ASTUtility.Trans(salamt, 2);
                                dr1["tkinword"] = ASTUtility.Trans(salamt, 2);
                            }

                            var lst1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.AllEmpInformationGrpwise>();
                            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAppoinmentLetterExc", lst1, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("printdate", System.DateTime.Today.ToString("dd-MMM-yyyy")));
                            Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                            break;

                        //Factory Staff
                        case "1":       
                            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAppoinmentLetterFBNonOT", listBN, null, null);
                            Rpt1.EnableExternalImages = true;
                            break;

                        //Worker
                        default:
                            ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
                            if (ds1 == null)
                                return;

                            listBN = ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformationBn>();
                            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAppoinmentLetterFB", listBN, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("CSN", comcod == "5305" ? "এফবি" : "এফবিএফ"));
                            break;
                    }
                  
                    Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                    Rpt1.SetParameters(new ReportParameter("compAdd", comaddBn));
                    Rpt1.SetParameters(new ReportParameter("empName", empName));
                    Rpt1.SetParameters(new ReportParameter("fname", fathernam));
                    Rpt1.SetParameters(new ReportParameter("mname", mothernam));
                    Rpt1.SetParameters(new ReportParameter("empIDCard", empIDCard));
                    Rpt1.SetParameters(new ReportParameter("joindate", jnDate));
                    Rpt1.SetParameters(new ReportParameter("deptName", deptprestring + deptName));
                    Rpt1.SetParameters(new ReportParameter("empSection", empSection +" ( " + lineDesc + " )"));
                    Rpt1.SetParameters(new ReportParameter("empDesigDesc", empDesigDesc));
                    Rpt1.SetParameters(new ReportParameter("empGrade", empGrade));
                    Rpt1.SetParameters(new ReportParameter("predist", predist));
                    Rpt1.SetParameters(new ReportParameter("preupzi", preupzi));
                    Rpt1.SetParameters(new ReportParameter("prepost", prepost));
                    Rpt1.SetParameters(new ReportParameter("previll", previll));
                    Rpt1.SetParameters(new ReportParameter("perdist", perdist));
                    Rpt1.SetParameters(new ReportParameter("perupzi", perupzi));
                    Rpt1.SetParameters(new ReportParameter("perpost", perpost));
                    Rpt1.SetParameters(new ReportParameter("pervill", pervill));
                    Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                    Rpt1.SetParameters(new ReportParameter("netSal", netSal));
                    Rpt1.SetParameters(new ReportParameter("foodall", foodall.ToString("#,##0.00;(#,##0.00); ")));
                    Rpt1.SetParameters(new ReportParameter("Bsal", Bsal.ToString("#,##0.00;(#,##0.00); ")));
                    Rpt1.SetParameters(new ReportParameter("HRent", HRent.ToString("#,##0.00;(#,##0.00); ")));
                    Rpt1.SetParameters(new ReportParameter("MedAllow", MedAllow.ToString("#,##0.00;(#,##0.00); ")));
                    Rpt1.SetParameters(new ReportParameter("Conv", Conv.ToString("#,##0.00;(#,##0.00); ")));
                    Rpt1.SetParameters(new ReportParameter("TotalAllow", TotalAllow.ToString()));
                    Rpt1.SetParameters(new ReportParameter("workType", workType));
                    Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

                }
                else if (reportTye == "WorkerConfirmLetter")
                {

                    issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                    string DGMSign = new Uri(Server.MapPath(@"~\Image\DGMSign.png")).AbsoluteUri;
                    string curDate = System.DateTime.Today.ToString("yyyy");
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    if (comcod == "5305")
                    {
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptWorkerConfirmLtrMultiFB", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("compSName", "এফবি"));
                        Rpt1.SetParameters(new ReportParameter("compName", comnam));
                    }
                    else
                    {
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptWorkerConfirmLtrMulti", RptListBn, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("compSName", "ফুটবেড"));
                    }

                    Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                    Rpt1.SetParameters(new ReportParameter("curDate", curDate));
                    Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                    Rpt1.SetParameters(new ReportParameter("DGMSign", DGMSign));
                }
                else if (reportTye == "EvaluationForm")
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    switch (rptFormat)
                    {
                        //Executive
                        case "0":
                            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEvaluationFormStaff", RptListBn, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("compName", comnam));
                            break;

                        //Factory Staff
                        case "1":
                            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEvaluationFormStaff", RptListBn, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("compName", comnam));
                            break;

                        //Worker
                        default:
                            issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("yyyy");
                            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEvaluationFormWorker", RptListBn, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                            Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                            break;
                    }

                    string curDate = System.DateTime.Today.ToString("yyyy");
                    Rpt1.SetParameters(new ReportParameter("curDate", curDate));
                    Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                }
                else if (reportTye == "CPF" && (rptFormat == "0" || rptFormat == "1"))
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPFMultiEN", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compName", comnam));
                    Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                    Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                }               
                else if (reportTye == "CPF")
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPFMulti", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                    Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                    Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                }
                else if (reportTye == "CPF2" && (rptFormat == "0" || rptFormat == "1"))
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF2MultiEN", RptListBn, null, null);
                    Rpt1.SetParameters(new ReportParameter("compName", comnam));
                }              
                else if (reportTye == "CPF2")
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF2Multi", RptListBn, null, null);
                    Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                }
                else if (reportTye == "CPF3" && (rptFormat == "0" || rptFormat == "1"))
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF3MultiEN", RptListBn, null, null);
                    Rpt1.SetParameters(new ReportParameter("compName", comnam));
                }
                else if (reportTye == "CPF3")
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptCPF3Multi", RptListBn, null, null);
                    Rpt1.SetParameters(new ReportParameter("compName", comnamBn));
                }
                else if(reportTye== "NomineeForm")
                {
                    string compName = companyInfoBn.Item1;
                    string compAdd = companyInfoBn.Item2;

                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptNomineeForm", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compName", compName));
                    Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
                    Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                    Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                }
                else if (reportTye == "AgeForm")
                {
                    string compName = companyInfoBn.Item1;
                    string compAdd = companyInfoBn.Item2;
                    issuDate = Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");

                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAgeForm", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compName", compName));
                    Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                    Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                }
                else if (reportTye == "TrainingForm")
                {
                    string compName = companyInfoBn.Item1;
                    string compAdd = companyInfoBn.Item2;
                    issuDate = Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");

                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptTrainingForm", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compName", compName));
                    Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
                    Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                    Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                }
                else if (reportTye == "EnvelopePerBangla")
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
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
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
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
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
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
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEnvelopePresentEng", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compName", comnam));
                    Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                    Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                    Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                }
                else if (reportTye == "OfficeEnvelop")
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptOfficeEnvelop", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                    Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                }

                else if (reportTye == "BankOpening")
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    switch (comcod)
                    {
                        //FB
                        case "5305":
                            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptBankOpening", RptListBn, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                            Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MMM-yyyy")));
                            break;

                        //Footbed
                        default:
                            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptBankOpeningFootbed", RptListBn, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                            Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MMM-yyyy")));
                            break;
                    }
                }
                else if (reportTye == "appointmenttopsheet")
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;
                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptAppsPart", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    Rpt1.SetParameters(new ReportParameter("rpttitle", "Applicant's Part"));
                    Rpt1.SetParameters(new ReportParameter("printFooter", ""));
                }
                else if (reportTye == "salcertificate")
                {
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;

                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    foreach (var item in RptListBn)
                    {
                        item.inword = ASTUtility.Trans((item.grssal) - (item.totaldeduc), 0);
                    }
                    string signid = this.DdlSignatory.SelectedValue;
                    DataTable dt = (DataTable)Session["tblSignatory"];
                    string signName = dt.Select("idcard='" + signid + "'")[0]["signamenly"].ToString();
                    string signDesig = dt.Select("idcard='" + signid + "'")[0]["signdesig"].ToString();

                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmsalcertificate", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    Rpt1.SetParameters(new ReportParameter("signName", signName));
                    Rpt1.SetParameters(new ReportParameter("signDesig", signDesig));
                    Rpt1.SetParameters(new ReportParameter("currdate", System.DateTime.Today.ToString("dd-MM-yyyy")));
                }
                else if (reportTye == "ProbEvaluationForm")
                {
                    issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                    DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_SHORT_ADDRESS", empidmulti, "", "", "", "", "", "", "", "");
                    if (ds3 == null)
                        return;

                    string signid = this.DdlSignatory.SelectedValue;
                    DataTable dt = (DataTable)Session["tblSignatory"];
                    string signName = dt.Select("idcard='" + signid + "'")[0]["signamenly"].ToString();
                    string signDesig = dt.Select("idcard='" + signid + "'")[0]["signdesig"].ToString();

                    var RptListBn = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeInfoBangla>();
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptProbEvaluationForm", RptListBn, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
                    Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    Rpt1.SetParameters(new ReportParameter("issuDate", issuDate));
                    Rpt1.SetParameters(new ReportParameter("signName", signName));
                    Rpt1.SetParameters(new ReportParameter("signDesig", signDesig));
                }
                else if (reportTye == "Suspension")
                {
                    reportList.Clear();

                    foreach (ListItem item in ddlEmpNameAllInfo.Items)
                    {
                        if (item.Selected)
                        {
                            Rpt1 = new LocalReport();
                            issuDate = this.txtIssuDate.Text == "" ? "" : Convert.ToDateTime(this.txtIssuDate.Text).ToString("dd/MM/yyyy");
                            string frmdate = this.txtfromdate.Text.ToString();
                            string todate = this.txttodate.Text.ToString();
                            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "EMPATTNIDWISEAUDIT2HRSOT", frmdate, todate, item.Value, "Card2Hrs", reportTye, "", "", "", "");
                            if (ds1 == null)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                                return;
                            }
                            DataTable dt1 = ds1.Tables[0];
                            int totalabs = 0;
                            foreach (DataRow row in dt1.Rows)
                            {
                                string paldate = row["paldate"].ToString();
                                int lenOfpaldate = paldate.Length;
                                string months = ASITUtility02.GetFullMonthName(paldate.Substring(0, lenOfpaldate - 3));
                                string years = ASITUtility02.NumBn(paldate.Substring(lenOfpaldate - 2));
                                row["paldate"] = months + "-" + years;
                                totalabs = totalabs + (int)row["absents"];
                                //row["absents"] = ASITUtility02.NumBn(row["absents"].ToString());
                            }
                            DateTime startDate = Convert.ToDateTime(this.txtfromdate.Text);
                            DateTime endDate = Convert.ToDateTime(this.txttodate.Text);
                            int monthsDifference = ((endDate.Year - startDate.Year) * 12) + (endDate.Month - startDate.Month) + 1;
                            string sfrmdate = ASITUtility02.GetMonthNameDigit(startDate.ToString("MM")) + "-" + ASITUtility02.NumBn(startDate.ToString("yyyy"));
                            string stodate = ASITUtility02.GetMonthNameDigit(endDate.ToString("MM")) + "-" + ASITUtility02.NumBn(endDate.ToString("yyyy"));

                            var RptList1 = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.SuspensionEmpAbsInfo>();
                            var RptList2 = ds1.Tables[1].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.SuspensionEmpInfo>();
                            //Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpApplicationForm", RptListBn, null, null);issuDate
                            string issueDateSuspension = ASITUtility02.NumBn(issuDate.Substring(0, 2)) + "/" + ASITUtility02.NumBn(issuDate.Substring(3, 2)) + "/" + ASITUtility02.NumBn(issuDate.Substring(6, 4)) + " ইং";
                            Rpt1.LoadReportDefinition(RPTPathClass.GetReportFilePath("RD_81_HRM.RD_81_Rec.RptSuspension"));
                            Rpt1 = Rpt1.SetRDLCReportDatasets(new Dictionary<string, object> { { "DataSet1", RptList1 }, { "DataSet2", RptList2 } });
                            Rpt1.SetParameters(new ReportParameter("issueDateSuspension", issueDateSuspension));
                            Rpt1.SetParameters(new ReportParameter("sfrmdate", sfrmdate));
                            Rpt1.SetParameters(new ReportParameter("stodate", stodate));
                            Rpt1.SetParameters(new ReportParameter("totalabs", ASITUtility02.NumBn(totalabs.ToString())));
                            Rpt1.SetParameters(new ReportParameter("monthsDifference", ASITUtility02.NumBn(monthsDifference.ToString())));

                            reportList.Add(Rpt1);
                        }
                    }

                    //List<string> reportListBox = new List<string>();

                    if (reportList.Count > 0)
                    {
                        Session["Report1"] = reportList;
                        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=MERGEPDF', target='_blank');</script>";
                        return;
                    }
                    else
                    {
                        return;
                    }

                   
                }

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception e)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + e.Message + "');", true);
            }


        }

        private string GetReportType(string rptType="")
        {
            string Type = "";
            if (rptType == "")
            {
                rptType = this.ddlReportType.SelectedValue.ToString();
            }
           
            switch (rptType)
            {
                case "0":
                    Type = "ApplicationForm";
                    break;
                case "1":
                    Type = "AppoinmentLetter";
                    break;
                case "2":
                    Type = "WorkerConfirmLetter";
                    break;
                case "3":
                    Type = "EvaluationForm";
                    break;
                case "4":
                    Type = "CPF";
                    break;
                case "5":
                    Type = "CPF2";
                    break;
                case "6":
                    Type = "CPF3";
                    break;
                case "7":
                    Type = "NomineeForm";
                    break;
                case "8":
                    Type = "AgeForm";
                    break;
                case "9":
                    Type = "TrainingForm";
                    break;
                case "10":
                    Type = "EnvelopePerEnglish";
                    break;
                case "11":
                    Type = "EnvelopePerBangla";
                    break;
                case "12":
                    Type = "EnvelopePresentEnglish";
                    break;
                case "13":
                    Type = "EnvelopePresentBangla";
                    break;
                case "14":
                    Type = "OfficeEnvelop";
                    break;
                case "15":
                    Type = "JoinLetter";
                    break;
                case "16":
                    Type = "BankOpening";
                    break;
                case "17":
                    Type = "appointmenttopsheet";
                    break;
                //Salary Certificate Signatory Below based on value
                case "18":
                    Type = "salcertificate";
                    break;
                case "19":
                    Type = "ProbEvaluationForm";
                    break;
                case "20":
                    Type = "Suspension";
                    break;
                case "21":
                    Type = "WorkerExtensionLetter";
                    break;
                default:
                    Type = "AppoinmentLetter";
                    break;
            }
            return Type;
        }

        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"] ?? "";
            if (type == "Datewise")
            {
                this.GetEmpDatewise();
            }
            else
            {
                GetEmpName();
            }
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

      

        public void GetSignatory()
        {
            string comcod = this.GetComeCode();
            DataSet dssign = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPSIGNAME", "80", "%", "%", "%", "", "", "", "", "");
            if (dssign != null)
            {
                DdlSignatory.DataTextField = "signame";
                DdlSignatory.DataValueField = "idcard";
                DdlSignatory.DataSource = dssign.Tables[0];
                DdlSignatory.DataBind();
                Session["tblSignatory"] = dssign.Tables[0];
            }
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

            this.ddlSection_SelectedIndexChanged(null, null);
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


        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"] ?? "";
            if (type == "Datewise")
            {
                this.GetEmpDatewise();
            }
            else
            {
                GetEmpName();
            }

        }
        private void GetLineDDL()
        {
            string comcod = GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";
            ViewState["tbllineddl"] = ds3.Tables[0];
        }
        private void GetEmpDatewise()
        {
            string comcod = this.GetComeCode();            
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dept = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = this.txtfromdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();
            string empStatus = this.ddlEmpStatus.SelectedValue.ToString();
            string linecode = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPNAMEDATEWISE", frmdate, todate, emptype, division, division, dept, section, empStatus, linecode);
            if (ds5 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            DataTable dt1 = ds5.Tables[0].Copy();
            DataView dv1 = dt1.DefaultView;
            dt1 = dv1.ToTable().DefaultView.ToTable(true, "empid", "empname");
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = dt1;
            this.ddlEmpNameAllInfo.DataBind();
            //this.ddlEmpNameAllInfo.SelectedValue = "000000000000";
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"] ?? "";
            if (type == "Datewise")
            {
                this.GetEmpDatewise();
            }
            else
            {
                GetEmpName();
            }
        }
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Salary Certificate Signatory --18
            //Probationary Evaluation Form --19
            this.Daterange.Visible = false;
            this.Daterange2.Visible = false;
            string rptValue = this.ddlReportType.SelectedValue;
            if (rptValue  == "18" || rptValue == "19")
            {
                this.idSignatory.Visible = true;
                this.GetSignatory();
            }
            else if (rptValue == "20")
            {
                this.Daterange.Visible = true;
                this.Daterange2.Visible = true;
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else
            {
                this.idSignatory.Visible = false;
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //ddlEmpNameAllInfo.Items.Add(New ListItem("Item Text 1", "Item Value 1"))
            //ddlEmpNameAllInfo.Items.Add(New ListItem("Item Text 2", "Item Value 2"))


            if (CheckBox1.Checked)
            {
                this.ReportType.Visible = false;
                this.DivListBox.Visible = true;
                List<string> reportListBox = new List<string>();
                foreach (ListItem litem in ReportListBox.Items)
                {

                    if (litem.Selected)
                    {
                        reportListBox.Add(litem.Value);

                    }

                }
            }
            else
            {
                this.ReportType.Visible = true;
                this.DivListBox.Visible = false;
            }
        }
    }
}