using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.IO;
using System.Data.OleDb;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_86_All
{
    public partial class EmpOvertime : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        public static string comLogo = "";
        public static string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtIssueDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = 
                       (type == "Overtime") ? "EMPLOYEE  OVERTIME ALLOWANCE "
                     : (type == "BankPayment") ? "BANK PAYMENT INFORMATION"
                     : (type == "Holiday") ? "EMPLOYEE HOLIDAY ALLOWANCE"
                     : (type == "Mobile") ? "EMPLOYEE MOBILE BILL ALLOWANCE"
                     : (type == "Lencashment") ? "LEAVE ENCASHMENT"
                     : (type == "OtherDeduction") ? "EMPLOYEE DEDCUTION"
                     : (type == "loan") ? "EMPLOYEE LOAN INFORMATION"
                     : (type == "dayadj") ? "Salary Adjustment"
                     : (type == "otherearn") ? "Other Earning"
                     : (type == "CarSub") ? "Car & Subsistance Allowance"
                     : (type == "MobLst") ? "Employee Mobile List"
                     : (type == "EarnLeave") ? "Earn Leave Entry"
                     : (type == "Lencashment02") ? "LEAVE ENCASHMENT 02"
                     : "EMPLOYEE ARREAR INFORMATION";

                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.ViewVisibility();
                this.GetYearMonth();
                this.GetGradeList();
                this.GetJobLocation();
                this.GetEmpLine();
                this.CommonButton();
                this.GetEmployee();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                userid = hst["usrid"].ToString();
                string comcod = this.GetComeCode();
                comLogo = userid == "5305139" ? new Uri(Server.MapPath(@"~\Image\LOGO" + "5306" + ".jpg")).AbsoluteUri : new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                ///sdshd
            }

            ///////upload deduction sheet/////
            if (fileuploadExcel.HasFile)
            {
                try
                {
                    Session.Remove("XcelData");
                    //  this.lmsg.Visible = true;
                    string connString = "";
                    string StrFileName = string.Empty;
                    if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                    {
                        StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                        string StrFileType = fileuploadExcel.PostedFile.ContentType;
                        int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                        if (IntFileSize <= 0)
                        {
                            //  this.lmsg.Text = "Uploading Fail";
                            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' file Uploading failed');", true);
                            return;
                        }
                        else
                        {
                            string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
                            string[] filePaths = Directory.GetFiles(savelocation);
                            foreach (string filePath in filePaths)
                                File.Delete(filePath);
                            fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);
                            //   this.lmsg.Text = "Uploading Successfully";
                        }
                    }

                    string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                    //string path = apppath + "ExcelFile\\" + StrFileName;
                    string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);

                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {

                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    }

                    //string query = "SELECT [Product],[Category],[Qty(Pcs)],[Value],[Unit Price],[ERP CODE] FROM [Sheet1$]";
                    string query = "";
                    if (this.Request.QueryString["Type"].ToString() == "OtherDeduction")
                    {
                        query = "SELECT * FROM [Sheet1$]";
                    }
                    if (this.Request.QueryString["Type"].ToString() == "arrear")
                    {
                        query = "SELECT * FROM [Sheet1$]";
                    }
                    OleDbConnection conn = new OleDbConnection(connString);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    Session["XcelData"] = ds.Tables[0];
                    // this.DataInsert();
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();
                    //this.GetExelData();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }

        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).ToolTip = "Final Update Deduction";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).ToolTip = "Calculation Amount";
            string qtype = this.Request.QueryString["Type"].ToString().Trim().Trim();
            if (qtype == "Overtime")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;

            }
            else if (qtype == "MobLst")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;

            }
            else if (qtype == "EarnLeave" || qtype == "Lencashment02")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;

            }
            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lUpdate_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void GetEmployee()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4))) + "%";
            string division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string secname = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string joblocation = ((this.ddlJob.SelectedValue.ToString() == "00000") ? "" : this.ddlJob.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPLOYEENAME", Company, division, Department, secname, joblocation, userid, "", "", "");
            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            Session["tblemplist"] = ds3.Tables[0];

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
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
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
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
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

        private void GetGradeList()
        {

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string qtype = this.Request.QueryString["Type"].ToString();

            string emptype = ASTUtility.Left(this.ddlWstation.SelectedValue.ToString(), 4);
            var lst = getlist.GetEmpGradelist(comcod);

            if (emptype == "9401")
            {
                lst = lst.FindAll(x => Convert.ToInt32(x.hrgcod) < 0336000);

            }
            else if (emptype == "9402")
            {
                lst = lst.FindAll(x => Convert.ToInt32(x.hrgcod) >= 0350000 && Convert.ToInt32(x.hrgcod) < 0360000);
            }
            else
            {
                lst = lst.FindAll(x => Convert.ToInt32(x.hrgcod) >= 0360000 && Convert.ToInt32(x.hrgcod) < 0370000);


            }

            this.ddlGrade.DataTextField = "hrgdesc";
            this.ddlGrade.DataValueField = "hrgcod";
            this.ddlGrade.DataSource = lst;
            this.ddlGrade.DataBind();

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();

            string type = "";
            string quType = this.Request.QueryString["Type"].ToString();
            if (quType == "Lencashment" || quType == "EarnLeave" || quType == "Lencashment02")
            {
                type = "Y";
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", type, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();

            if (quType == "Lencashment" || quType == "EarnLeave" || quType == "Lencashment02")
            {
                this.ddlyearmon.SelectedValue = System.DateTime.Today.AddYears(-1).ToString("yyyy") + "12";
            }
            else
            {
                this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");

            }
            ds1.Dispose();
        }

        private void GetPreYearMonth()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlpreyearmon.DataTextField = "yearmon";
            this.ddlpreyearmon.DataValueField = "ymon";
            this.ddlpreyearmon.DataSource = ds1.Tables[0];
            this.ddlpreyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlpreyearmon.DataBind();

            ds1.Dispose();
        }

        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.SectionView();
        }
        private void ViewVisibility()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Overtime":
                    this.divEmpStatus.Visible = true;
                    break;
                case "BankPayment":
                    break;

                case "Holiday":
                    break;

                case "Mobile":
                    this.lbldate.Text = "Month Id";
                    this.divGrade.Visible = false;
                    break;

                case "Lencashment":
                    this.divGrade.Visible = false;
                    this.lbldate.Text = "Year";
                    break;

                case "OtherDeduction":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id";
                    this.divUpload.Visible = true;
                    break;

                case "loan":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id";
                    break;
                case "arrear":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id";
                    this.divUpload.Visible = true;
                    break;

                case "otherearn":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id";
                    break;
                case "dayadj":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id";
                    break;

                case "CarSub":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id";
                    break;

                case "MobLst":
                    this.divEmpStatus.Visible = true;
                    this.divLnkbtnShow.Visible = true;
                    this.fltrSection1.Visible = false;
                    this.divDate.Visible = false;
                    this.divGrade.Visible = false;
                    this.divCard.Visible = false;
                    break;

                case "EarnLeave":
                    this.divEmpStatus.Visible = true;
                    this.divGrade.Visible = false;
                    this.lbldate.Text = "Year";
                    break;

                case "Lencashment02":
                    this.divEmpStatus.Visible = true;
                    this.divLine.Visible = true;
                    this.divIssueDate.Visible = true;
                    this.divGrade.Visible = false;
                    this.lbldate.Text = "Year";
                    break;
            }


        }

        protected void gvEmpOverTime_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpOverTime.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
      
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string qtype = this.Request.QueryString["Type"].ToString();

            if (this.lnkbtnShow.Text == "Ok")
            {
                this.ddlyearmon.Enabled = false;               
                this.divPageSize.Visible = true;
                if (qtype != "MobLst")
                {
                    this.lnkbtnShow.Text = "New";
                }

                this.SectionView();
                this.GetEmployee();
                return;
            }
            this.MultiView1.ActiveViewIndex = -1;
            this.ddlyearmon.Enabled = true;
            if (qtype == "CarSub")
            {
                this.ddlWstation.Enabled = false;
            }          
          
            this.divPageSize.Visible = false;
            this.gvEmpOverTime.DataSource = null;
            this.gvEmpOverTime.DataBind();
            this.divChkEmp.Visible = false;
            this.chkAddEmp.Checked = false;
            this.divAddEmp.Visible = false;
            this.lnkbtnShow.Text = "Ok";
            this.txtSrcEmployee.Text = "";

        }


        private void SectionView()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Overtime":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowOvertime();
                    this.divDedHour.Visible = true;
                    break;
                case "BankPayment":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ShowBankPay();
                    break;
                case "Holiday":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowEmpHolidayInfo();
                    break;

                case "Mobile":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.divChkEmp.Visible = true;
                    this.EmpMobileBill();
                    break;


                case "Lencashment":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.ShowLeaveEncashment();
                    break;

                case "OtherDeduction":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.divChkEmp.Visible = true;
                    this.ShowOtherDeduction();
                    break;

                case "loan":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.ShowEmpLoan();
                    break;
                case "arrear":
                    this.MultiView1.ActiveViewIndex = 7;
                    this.divChkEmp.Visible = true;
                    this.EmpArrearSalary();
                    break;
                case "otherearn":
                    this.MultiView1.ActiveViewIndex = 8;
                    this.OtherEarning();
                    break;
                case "dayadj":
                    this.MultiView1.ActiveViewIndex = 9;
                    this.SalaryDayAdj();
                    break;
                case "CarSub":
                    this.MultiView1.ActiveViewIndex = 10;
                    this.ShowCarSubAllowan();
                    break;
                case "MobLst":
                    this.MultiView1.ActiveViewIndex = 11;
                    this.ShowMoblst();
                    break;

                case "EarnLeave":
                    this.MultiView1.ActiveViewIndex = 12;
                    this.divChkEmp.Visible = true;
                    this.ShowEmpEarnLeave();
                    break;

                case "Lencashment02":
                    this.MultiView1.ActiveViewIndex = 13;
                    this.divChkEmp.Visible = true;
                    this.ShowLeaveEncashment02();
                    break;

            }

        }
        private void ShowLeaveEncashment02()
        {
            Session.Remove("tblover");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string JobLocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string empLine = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string empStatus = this.ddlEmpType.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "RPT_LEAVE_ENCASHMENT_02", null,null,null, company, divison, deptid, section, MonthId, date,
                Empcode, JobLocation, userid, empLine, empStatus);
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvLvEnCashmnt02.DataSource = null;
                this.gvLvEnCashmnt02.DataBind();
                return;
            }

            Session["tblover"] = ds2.Tables[0];
            this.Data_Bind();
        }
        private void ShowEmpEarnLeave()
        {
            Session.Remove("tblover");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string JobLocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string empStatus = this.ddlEmpType.SelectedValue.Trim().ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMP_EARN_LEAVE", company, divison, deptid, section, MonthId, date, Empcode, JobLocation, userid, empStatus);
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvEarnLvEntry.DataSource = null;
                this.gvEarnLvEntry.DataBind();
                return;
            }

            Session["tblover"] = ds2.Tables[0];
            this.Data_Bind();
        }

        string GetComOverTimeCallType()
        {
            string comcod = this.GetComeCode();
            string CallType = "";
            switch (comcod)
            {
                case "5301":
                case "5401":

                    CallType = "EMPALLOYOVERTIMEEDISON";
                    break;


                case "5305":
                case "5306":
                    CallType = "EMPALLOYOVERTIMEFB";
                    break;


                default:
                    CallType = "EMPALLOYOVERTIME";
                    break;

            }

            return CallType;



        }

        private void ShowOvertime()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string CallType = this.GetComOverTimeCallType();
            string chkresign = this.ddlEmpType.SelectedValue;
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", CallType, company, dayid, txtdate, divison, deptname, section, "", chkresign, Empcode);
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvEmpOverTime.DataSource = null;
                this.gvEmpOverTime.DataBind();
                return;
            }
            Session["tblover"] = ds2.Tables[0];
            Session["tblOtDetails"] = ds2.Tables[1];
            this.Data_Bind();

        }


        private void ShowBankPay()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = "94%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString() + "%";
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.ddlyearmon.Text.Trim()).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "BANKPAYINFO", deptname, todate, comnam, Empcode, "", "", "", "", "");
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvBankPay.DataSource = null;
                this.gvBankPay.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblover"] = dt;
            this.Data_Bind();
        }

        private void ShowEmpHolidayInfo()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = "94%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString() + "%";

            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));

            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "HOLIDAYEMPALLOYEE", deptname, dayid, txtdate, comnam, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvEmpHoliday.DataSource = null;
                this.gvEmpHoliday.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }
        private void EmpMobileBill()
        {
            Session.Remove("tblover");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string comnam = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string JobLocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMOBILEBILLINFO", deptname, MonthId, date, comnam, Empcode, divison, section, JobLocation, userid);
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvEmpMbill.DataSource = null;
                this.gvEmpMbill.DataBind();
                return;
            }

            Session["tblover"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        private void ShowLeaveEncashment()
        {

            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string deptname = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "LEAVEENCASHMENT", deptname, dayid, txtdate, comnam, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpELeave.DataSource = null;
                this.gvEmpELeave.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void ShowCarSubAllowan()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPCARSUBSISTANCEALLOWANCE", section, MonthId, date, comnam, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                this.gvCarSub.DataSource = null;
                this.gvCarSub.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        private void ShowMoblst()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string EmpType = this.ddlEmpType.SelectedValue.ToString();

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "ACTIVEMOBILENO", EmpType, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvMobLst.DataSource = null;
                this.gvMobLst.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        private void ShowOtherDeduction()
        {
            Session.Remove("tblover");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string comnam = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string jobLoc = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPOTHERDEDUCTION", section, MonthId, date, comnam, Empcode, divison, deptname, jobLoc, userid);
            if (ds2 == null)
            {
                this.gvEmpOtherded.DataSource = null;
                this.gvEmpOtherded.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }

        private void ShowEmpLoan()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = "94%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString() + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPLOANEDUCTION", deptname, MonthId, date, comnam, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpOtherded.DataSource = null;
                this.gvEmpOtherded.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void EmpArrearSalary()
        {
            Session.Remove("tblover");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string compname = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string cuDate = System.DateTime.Today.ToString("dd");
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/" + cuDate + "/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string jobLoc = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPARREARSALARY", compname, MonthId, date, section, Empcode, divison, deptname, jobLoc, userid);
            if (ds2 == null)
            {
                this.gvarrear.DataSource = null;
                this.gvarrear.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }

        private void OtherEarning()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string compname = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPOTHEARNING", compname, MonthId, date, deptname, Empcode, section, divison, "", "");
            if (ds2 == null)
            {
                this.gvothearn.DataSource = null;
                this.gvothearn.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        private void SalaryDayAdj()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string compname = "94%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString() + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPDAYADJUSTMENT", compname, MonthId, date, deptname, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                this.grvAdjDay.DataSource = null;
                this.grvAdjDay.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblover"];
                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {
                    case "Overtime":
                        this.gvEmpOverTime.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpOverTime.DataSource = dt;
                        this.gvEmpOverTime.DataBind();
                        this.EnabledOrVissible();
                        this.FooterCalculation();

                        break;

                    case "BankPayment":
                        this.gvBankPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvBankPay.DataSource = dt;
                        this.gvBankPay.DataBind();
                        if (dt.Rows.Count != 0)
                        {
                            this.FooterCalculation();
                        }
                        break;

                    case "Holiday":
                        this.gvEmpHoliday.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpHoliday.DataSource = dt;
                        this.gvEmpHoliday.DataBind();
                        break;

                    case "Mobile":
                        this.gvEmpMbill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpMbill.DataSource = dt;
                        this.gvEmpMbill.DataBind();
                        this.FooterCalculation();
                        break;


                    case "Lencashment":
                        this.gvEmpELeave.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpELeave.DataSource = dt;
                        this.gvEmpELeave.DataBind();

                        Session["Report1"] = gvEmpELeave;
                        if (dt.Rows.Count > 0)
                            ((HyperLink)this.gvEmpELeave.HeaderRow.FindControl("mhlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;

                    case "OtherDeduction":
                        this.gvEmpOtherded.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpOtherded.DataSource = dt;
                        this.gvEmpOtherded.DataBind();
                        this.FooterCalculation();
                        break;
                    case "CarSub":
                        //chkSubBonustype
                        this.gvCarSub.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvCarSub.DataSource = dt;
                        this.gvCarSub.DataBind();
                        this.FooterCalculation();
                        break;

                    case "loan":
                        this.gvEmploan.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmploan.DataSource = dt;
                        this.gvEmploan.DataBind();
                        this.FooterCalculation();
                        break;

                    case "arrear":
                        this.gvarrear.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvarrear.DataSource = dt;
                        this.gvarrear.DataBind();
                        Session["Report1"] = gvarrear;
                        if (dt.Rows.Count != 0)
                        {
                            ((HyperLink)this.gvarrear.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        }
                        this.FooterCalculation();
                        break;

                    case "otherearn":
                        this.gvothearn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvothearn.DataSource = dt;
                        this.gvothearn.DataBind();
                        this.FooterCalculation();
                        break;

                    case "dayadj":
                        this.grvAdjDay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.grvAdjDay.DataSource = dt;
                        this.grvAdjDay.DataBind();
                        this.FooterCalculation();
                        break;
                    case "MobLst":
                        this.gvMobLst.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvMobLst.DataSource = dt;
                        this.gvMobLst.DataBind();
                        break;

                    case "EarnLeave":
                        this.gvEarnLvEntry.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEarnLvEntry.DataSource = dt;
                        this.gvEarnLvEntry.DataBind();
                        Session["Report1"] = gvEarnLvEntry;
                        if (dt.Rows.Count != 0)
                        {
                            ((HyperLink)this.gvEarnLvEntry.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        }
                        break;

                    case "Lencashment02":
                        this.gvLvEnCashmnt02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvLvEnCashmnt02.DataSource = dt;
                        this.gvLvEnCashmnt02.DataBind();
                        this.FooterCalculation();
                        Session["Report1"] = gvLvEnCashmnt02;
                        if (dt.Rows.Count != 0)
                        {
                            ((HyperLink)this.gvLvEnCashmnt02.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }  
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblover"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Overtime":
                    ((Label)this.gvEmpOverTime.FooterRow.FindControl("lgvFhour")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tohour)", "")) ? 0.00
                        : dt.Compute("sum(tohour)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    break;
                case "BankPayment":
                    ((Label)this.gvBankPay.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00
                            : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "Holiday":
                    break;

                case "Mobile":
                    ((Label)this.gvEmpMbill.FooterRow.FindControl("lgvFMbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mbillamt)", "")) ? 0.00
                        : dt.Compute("sum(mbillamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpMbill.FooterRow.FindControl("lgvFpayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payamt)", "")) ? 0.00
                        : dt.Compute("sum(payamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpMbill.FooterRow.FindControl("lgvFactamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(actamt)", "")) ? 0.00
                        : dt.Compute("sum(actamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "OtherDeduction":
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFleaveded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lvded)", "")) ? 0.00
                            : dt.Compute("sum(lvded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFTarrearded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arded)", "")) ? 0.00
                           : dt.Compute("sum(arded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFSaladv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(saladv)", "")) ? 0.00
                            : dt.Compute("sum(saladv)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFotherded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(otherded)", "")) ? 0.00
                            : dt.Compute("sum(otherded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ? 0.00
                            : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFoterded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fallded)", "")) ? 0.00
                          : dt.Compute("sum(fallded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFotermbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mbillded)", "")) ? 0.00
                         : dt.Compute("sum(mbillded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFspecialded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(spcded)", "")) ? 0.00
                      : dt.Compute("sum(spcded)", ""))).ToString("#,##0;(#,##0); ");

                    break;
                case "CarSub":
                    ((Label)this.gvCarSub.FooterRow.FindControl("lblgvFleaveded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gsalary)", "")) ? 0.00
                            : dt.Compute("sum(gsalary)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCarSub.FooterRow.FindControl("lblgvFcarallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(carallow)", "")) ? 0.00
                           : dt.Compute("sum(carallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCarSub.FooterRow.FindControl("lblgvFarcallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arcallow)", "")) ? 0.00
                            : dt.Compute("sum(arcallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCarSub.FooterRow.FindControl("lblgvFsuballowance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hidegs)", "")) ? 0.00
                            : dt.Compute("sum(hidegs)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvCarSub.FooterRow.FindControl("lblgvFasallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(asallow)", "")) ? 0.00
                            : dt.Compute("sum(asallow)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvCarSub.FooterRow.FindControl("lblgvFonetpay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00
                          : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "loan":
                    ((Label)this.gvEmploan.FooterRow.FindControl("lblgvFLToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ? 0.00
                            : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "arrear":
                    ((Label)this.gvarrear.FooterRow.FindControl("lgvFarrearamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(aramt)", "")) ? 0.00
                            : dt.Compute("sum(aramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvarrear.FooterRow.FindControl("lgvPFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfamt)", "")) ? 0.00
                            : dt.Compute("sum(pfamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvarrear.FooterRow.FindControl("lgvFitaxAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00
                          : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvarrear.FooterRow.FindControl("lgvAPFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tapfamt)", "")) ? 0.00
                            : dt.Compute("sum(tapfamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "otherearn":
                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFotherearn")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othearn)", "")) ? 0.00
                            : dt.Compute("sum(othearn)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "dayadj":
                    ((Label)this.grvAdjDay.FooterRow.FindControl("lgvFDelday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dalday)", "")) ? 0.00
                        : dt.Compute("sum(dalday)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvAdjDay.FooterRow.FindControl("lgvFAdj")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dedday)", "")) ? 0.00
                        : dt.Compute("sum(dedday)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "Lencashment02":
                    ((Label)this.gvLvEnCashmnt02.FooterRow.FindControl("lblgvFPayEnLeave")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00
                            : dt.Compute("sum(netpayable)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }
        }
        private void EnabledOrVissible()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {

                case "4301":
                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {
                        double fixhourrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvFixedrate")).Text.Trim());
                        double hourlyrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvhourlyrate")).Text.Trim());
                        double c1rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc1rate")).Text.Trim());
                        double c2rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc2rate")).Text.Trim());
                        double c3rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc3rate")).Text.Trim());
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Visible = fixhourrate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Visible = hourlyrate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Visible = c1rate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Visible = c2rate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Visible = c3rate > 0;
                    }
                    break;
                case "4101":
                case "4305":

                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {

                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Visible = true;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Visible = false;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Visible = false;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Visible = false;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Visible = false;
                    }
                    break;
                case "5301":
                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Visible = false;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).ReadOnly = true;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Visible = false;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Visible = false;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Visible = false;
                    }
                    break;
                default:
                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {
                        double fixhourrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvFixedrate")).Text.Trim());
                        double hourlyrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvhourlyrate")).Text.Trim());
                        double c1rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc1rate")).Text.Trim());
                        double c2rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc2rate")).Text.Trim());
                        double c3rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc3rate")).Text.Trim());
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Visible = fixhourrate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Visible = hourlyrate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Visible = c1rate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Visible = c2rate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Visible = c3rate > 0;
                    }
                    break;
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetJobLocation()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJob.DataTextField = "location";
            this.ddlJob.DataValueField = "loccode";
            this.ddlJob.DataSource = lst;
            this.ddlJob.DataBind();
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
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "BankPayment":
                    this.rptBankPayment();
                    break;
                case "Mobile":
                    this.rptMobileAllowance();
                    break;
                case "arrear":
                    this.rptEmpArrearSalary();
                    break;
                case "CarSub":
                    this.RptCarSubAllownce();
                    break;
                case "Lencashment":
                    this.RptLencashment();
                    break;
                case "MobLst":
                    this.RptMobLst();
                    break;

                case "OtherDeduction":
                    this.PrintOtherDeduction();
                    break;

                case "EarnLeave":
                    this.PrintEarnLeave();
                    break;

                case "Lencashment02":
                    this.RptLvEnCashment02();
                    break;

            }
        }

        private void PrintEarnLeave()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compName = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string year = this.ddlyearmon.SelectedItem.Text.Trim();

            DataTable dt = (DataTable)Session["tblover"];
            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_86_All.EmployeeInformation.RptEarnLeaveEnCashment>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_86_All.RptEarnLeave", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", compName));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Earn Leave for the year of " + year));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptLvEnCashment02()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compName = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string year = this.ddlyearmon.SelectedValue.Trim().Substring(0,4);
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MM-yyyy");
            string monthBan = ASITUtility02.GetMonthNameDigit(date.Substring(3,2));
            string yearBan = ASITUtility02.NumBn(date.Substring(6));

            DataTable dt = (DataTable)Session["tblover"];
            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_86_All.EmployeeInformation.RptEarnLeaveEnCashment>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_86_All.RptEarnLvPaySheet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", compName));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("issueDate", this.txtIssueDate.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("yearBan", yearBan));
            Rpt1.SetParameters(new ReportParameter("monyearBan", monthBan + "-" + yearBan));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Earn Leave Payment Sheet " + year));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintOtherDeduction()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compName = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblover"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("spcded>0");
            dt = dv.ToTable();
            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_86_All.EmployeeInformation.RptOtherDedction>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_86_All.RptOtherDeduction", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", compName));
            rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Punitive Deduction Report on " + this.ddlyearmon.SelectedItem.Text.Trim()));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptLencashment()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compName = hst["comnam"].ToString();
            string comAdd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string empType = this.ddlWstation.SelectedItem.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string grade = this.ddlGrade.SelectedItem.Text.Trim();
            string division = this.ddlDivision.SelectedItem.Text.Trim();
            string dptName = this.ddlDept.SelectedItem.Text.Trim();
            string section = this.ddlSection.SelectedItem.Text.Trim();

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeanChasment>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptLencashment", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", compName));
            rpt1.SetParameters(new ReportParameter("comAdd", comAdd));
            rpt1.SetParameters(new ReportParameter("section", section));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("empType", empType));
            rpt1.SetParameters(new ReportParameter("grade", grade));
            rpt1.SetParameters(new ReportParameter("division", division));
            rpt1.SetParameters(new ReportParameter("dptName", dptName));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Leave Encashment"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptMobLst()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compName = hst["comnam"].ToString();
            string comAdd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpMobLst>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_86_All.RptMobLst", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", compName));
            rpt1.SetParameters(new ReportParameter("comadd", comAdd));
            rpt1.SetParameters(new ReportParameter("ComLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Mobile List"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptCarSubAllownce()
        {
            //Iqbal Nayan
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Todate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string section = "";
            //string month = Convert.ToDateTime(this.ddlyearmon.Text).ToString("MMMM-yyyy");
            string month = this.ddlyearmon.SelectedItem.Text.Trim();
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.ECurSubAllowance>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptCarSubAllowance", lst, null, null);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("section", section));
            rpt1.SetParameters(new ReportParameter("Todate", Todate));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Executive Car & Subsistance Allowance For The Month of " + month));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void rptEmpArrearSalary()
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = hst["comcod"].ToString();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("MMM-yyyy");

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            ///string cominfo = ASTUtility.Cominformation();

            var lst = ((dt.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.EmpArrearSalaryList>()).Where(x => x.tapfamt != 0.00)).ToList();

            string month = "Month of : " + date;
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_86_All.RptEmpArrSalary", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Employees Arrear Salary Report"));
            rpt1.SetParameters(new ReportParameter("month", month));
            rpt1.SetParameters(new ReportParameter("month01", date));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void rptBankPayment()
        {

            //DataTable dt = (DataTable)Session["tbbank"];
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string companyname = ddlCompanyName.SelectedItem.Text.Trim().Substring(13);
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string frmdate = this.ddlyearmon.Text.ToString().Trim();//txtdate

            //    ReportDocument rpcp = new MFGRPT.R_81_Hrm.R_84_Lea.rptBankPayment();
            //    TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //    CompName.Text = companyname;
            //    TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtccaret.Text = "Salary for the Month of " + frmdate;
            //    TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //    rpcp.SetDataSource(dt);
            //    //string comcod = hst["comcod"].ToString();
            //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    //rpcp.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rpcp;

            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void rptMobileAllowance()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string date = ddlyearmon.SelectedItem.Text.Trim();
            string dptName = ddlDept.SelectedItem.Text.Trim().Substring(13);
            string monthid = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblover"];

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_86_All.EmployeeInformation.EmpMobBillInfo>();
            if (this.CheckSum.Checked == true)
            {
                lst = lst.GroupBy(x => x.joblocation).Select(c => new SPEENTITY.C_81_Hrm.C_86_All.EmployeeInformation.EmpMobBillInfo
                {
                    comcod = c.FirstOrDefault().comcod,
                    totaluser = c.Count().ToString(),
                    joblocdesc = c.FirstOrDefault().joblocdesc,
                    mbillamt = c.Sum(a => a.mbillamt),
                    payamt = c.Sum(a => a.payamt)



                }).ToList();

            }
            double Inword = lst.Sum(p => p.payamt);
            LocalReport rpt1 = new LocalReport();
            if (this.CheckSum.Checked == true)
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptEmpMobBillFbSum", lst, null, null);
            }
            else
            {
                if (this.CheckNew.Checked == true)
                {
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptEmpMobBillFbNew", lst, null, null);
                }
                else
                {
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptEmpMobBill", lst, null, null);
                }
            }
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comname));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("date", printdate));
            rpt1.SetParameters(new ReportParameter("Inword", ASTUtility.Trans(Inword, 2)));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Moblie Bill For The Month Of " + date));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lTotal_Click(object sender, EventArgs e)
        {


            this.SaveValue();
            this.Data_Bind();
        }
        private void SaveValue()
        {


            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;
            switch (type)
            {
                case "Overtime":
                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {

                        double fixhour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Text.Trim());
                        double hourly = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Text.Trim());
                        double c1hour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Text.Trim());
                        double c2hour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Text.Trim());
                        double c3hour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Text.Trim());
                        double todedihour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtDeduction")).Text.Trim());
                        double tohour = (fixhour + hourly + c1hour + c2hour + c3hour) - todedihour;
                        rowindex = (this.gvEmpOverTime.PageSize) * (this.gvEmpOverTime.PageIndex) + i;
                        dt.Rows[rowindex]["fixhour"] = fixhour;
                        dt.Rows[rowindex]["hourly"] = hourly;
                        dt.Rows[rowindex]["c1hour"] = c1hour;
                        dt.Rows[rowindex]["c2hour"] = c2hour;
                        dt.Rows[rowindex]["c3hour"] = c3hour;
                        dt.Rows[rowindex]["tohour"] = tohour;
                        dt.Rows[rowindex]["todedihour"] = todedihour;

                    }

                    break;

                case "BankPayment":
                    for (int i = 0; i < this.gvBankPay.Rows.Count; i++)
                    {

                        string bankno = ((TextBox)this.gvBankPay.Rows[i].FindControl("lbgvBankSno")).Text.Trim();
                        string acno = ((TextBox)this.gvBankPay.Rows[i].FindControl("lgvBACNo")).Text.Trim();
                        double amount = Convert.ToDouble("0" + ((TextBox)this.gvBankPay.Rows[i].FindControl("lgvAmt")).Text.Trim());
                        string remarks = ((TextBox)this.gvBankPay.Rows[i].FindControl("lgvRemarks")).Text.Trim();
                        rowindex = (this.gvBankPay.PageSize) * (this.gvBankPay.PageIndex) + i;
                        dt.Rows[rowindex]["bankseno"] = bankno;
                        dt.Rows[rowindex]["bankacno"] = acno;
                        dt.Rows[rowindex]["bankamt"] = amount;
                        dt.Rows[rowindex]["remarks"] = remarks;

                    }

                    break;

                case "Holiday":
                    for (int i = 0; i < this.gvEmpHoliday.Rows.Count; i++)
                    {
                        bool chk = ((CheckBox)this.gvEmpHoliday.Rows[i].FindControl("chkHoliday")).Checked;
                        rowindex = (this.gvEmpHoliday.PageSize) * (this.gvEmpHoliday.PageIndex) + i;
                        dt.Rows[rowindex]["hstatus"] = (chk) ? "True" : "False";
                    }

                    break;

                case "Mobile":
                    for (int i = 0; i < this.gvEmpMbill.Rows.Count; i++)
                    {
                        double mbillamt = Convert.ToDouble("0" + ((TextBox)this.gvEmpMbill.Rows[i].FindControl("txtgvMbill")).Text.Trim());
                        double payamt = Convert.ToDouble("0" + ((TextBox)this.gvEmpMbill.Rows[i].FindControl("txtgvpayamt")).Text.Trim());
                        double accamt = mbillamt - payamt;

                        rowindex = (this.gvEmpMbill.PageSize) * (this.gvEmpMbill.PageIndex) + i;
                        dt.Rows[rowindex]["mbillamt"] = mbillamt;
                        dt.Rows[rowindex]["payamt"] = payamt;
                        dt.Rows[rowindex]["actamt"] = accamt;

                    }
                    break;

                case "Lencashment":
                    for (int i = 0; i < this.gvEmpELeave.Rows.Count; i++)
                    {
                        double enclashleave = Convert.ToDouble("0" + ((TextBox)this.gvEmpELeave.Rows[i].FindControl("txtgvEnCleave")).Text.Trim());
                        rowindex = (this.gvEmpELeave.PageSize) * (this.gvEmpELeave.PageIndex) + i;
                        dt.Rows[rowindex]["ecleave"] = enclashleave;
                    }
                    break;

                case "OtherDeduction":
                    for (int i = 0; i < this.gvEmpOtherded.Rows.Count; i++)
                    {

                        double lvded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtgvleaveded")).Text.Trim());
                        double arded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtgvarairded")).Text.Trim());
                        double saladv = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtgvsaladv")).Text.Trim());
                        double otherded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtlgvotherded")).Text.Trim());
                        double mbillded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("gvtxtmbill")).Text.Trim());
                        double fallded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("gvtxtfallow")).Text.Trim());
                        double spcded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtlgvspecialded")).Text.Trim());

                        double toamt = otherded + lvded + arded + saladv + mbillded + fallded + spcded;
                        rowindex = (this.gvEmpOtherded.PageSize) * (this.gvEmpOtherded.PageIndex) + i;
                        dt.Rows[rowindex]["lvded"] = lvded;
                        dt.Rows[rowindex]["arded"] = arded;
                        dt.Rows[rowindex]["saladv"] = saladv;
                        dt.Rows[rowindex]["otherded"] = otherded;
                        dt.Rows[rowindex]["mbillded"] = mbillded;
                        dt.Rows[rowindex]["fallded"] = fallded;
                        dt.Rows[rowindex]["spcded"] = spcded;
                        dt.Rows[rowindex]["toamt"] = toamt;
                    }
                    break;

                case "CarSub":
                    for (int i = 0; i < this.gvCarSub.Rows.Count; i++)
                    {

                        double gsalary = Convert.ToDouble("0" + ((Label)this.gvCarSub.Rows[i].FindControl("txtgvgsalary")).Text.Trim());
                        double carallow = Convert.ToDouble("0" + ((TextBox)this.gvCarSub.Rows[i].FindControl("txtgvcarallow")).Text.Trim());
                        double arcallow = Convert.ToDouble("0" + ((TextBox)this.gvCarSub.Rows[i].FindControl("txtgvarcallow")).Text.Trim());
                        double suballowance = Convert.ToDouble("0" + ((Label)this.gvCarSub.Rows[i].FindControl("txtgvsuballowance")).Text.Trim());
                        double asallow = Convert.ToDouble("0" + ((TextBox)this.gvCarSub.Rows[i].FindControl("gvasallow")).Text.Trim());

                        // double suballowanceamt= gsalary * 25 / 100;

                        // double subbonus = (this.chkSubBonustype.Checked==true? suballowanceamt * 60 / 100:0);


                        double toamt = carallow + arcallow + suballowance + asallow;

                        rowindex = (this.gvCarSub.PageSize) * (this.gvCarSub.PageIndex) + i;
                        dt.Rows[rowindex]["gsalary"] = gsalary;
                        dt.Rows[rowindex]["carallow"] = carallow;
                        dt.Rows[rowindex]["arcallow"] = arcallow;
                        dt.Rows[rowindex]["suballowance"] = suballowance;
                        dt.Rows[rowindex]["asallow"] = asallow;
                        dt.Rows[rowindex]["netpay"] = toamt;
                        dt.Rows[rowindex]["subbonus"] = 0;

                    }

                    break;

                case "loan":
                    for (int i = 0; i < this.gvEmploan.Rows.Count; i++)
                    {

                        double cloan = Convert.ToDouble("0" + ((TextBox)this.gvEmploan.Rows[i].FindControl("txtgvcloan")).Text.Trim());
                        double pfloan = Convert.ToDouble("0" + ((TextBox)this.gvEmploan.Rows[i].FindControl("txtgvpfloan")).Text.Trim());

                        double toamt = cloan + pfloan;
                        rowindex = (this.gvEmploan.PageSize) * (this.gvEmploan.PageIndex) + i;
                        dt.Rows[rowindex]["cloan"] = cloan;
                        dt.Rows[rowindex]["pfloan"] = pfloan;
                        dt.Rows[rowindex]["toamt"] = toamt;
                    }

                    break;

                case "arrear":
                    for (int i = 0; i < this.gvarrear.Rows.Count; i++)
                    {
                        double pf = 0.00;
                        double bacic = 0.00;
                        double arrear = Convert.ToDouble("0" + ((TextBox)this.gvarrear.Rows[i].FindControl("txtarrear")).Text.Trim());
                        // if (Convert.ToDouble("0" + ((TextBox)this.gvarrear.Rows[i].FindControl("txtarrear")).Text.Trim()) != 0.00)
                        string remarks = Convert.ToString(((TextBox)this.gvarrear.Rows[i].FindControl("txtRemrks")).Text.Trim());

                        //{
                        //    double percent = Convert.ToDouble("0" + (dt.Rows[i]["percnt"]));
                        //    bacic = (arrear * percent) / 100;
                        //    pf = (bacic * 5) / 100;
                        //}
                        rowindex = (this.gvarrear.PageSize) * (this.gvarrear.PageIndex) + i;

                        dt.Rows[rowindex]["aramt"] = arrear;
                        // dt.Rows[rowindex]["pfamt"] = pf;
                        dt.Rows[rowindex]["tapfamt"] = arrear - pf;
                        dt.Rows[rowindex]["remarks"] = remarks;
                    }
                    break;


                case "otherearn":
                    for (int i = 0; i < this.gvothearn.Rows.Count; i++)
                    {
                        double tptallow = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txtgvtpallow")).Text.Trim());
                        double foodallow = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txtgvfoodallow")).Text.Trim());
                        double kpi = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txtgvkpi")).Text.Trim());
                        double perbon = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txgvperbon")).Text.Trim());
                        double otherearn = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txtgvotherearn")).Text.Trim());

                        rowindex = (this.gvothearn.PageSize) * (this.gvothearn.PageIndex) + i;

                        dt.Rows[rowindex]["foodallow"] = foodallow;
                        dt.Rows[rowindex]["tptallow"] = tptallow;
                        dt.Rows[rowindex]["kpi"] = kpi;
                        dt.Rows[rowindex]["perbon"] = perbon;
                        dt.Rows[rowindex]["othearn"] = otherearn;
                        dt.Rows[rowindex]["totalam"] = foodallow + tptallow + kpi + perbon + otherearn;

                    }
                    break;
                case "dayadj":
                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double dedday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtAdj")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        dt.Rows[rowindex]["dedday"] = dedday;

                    }
                    break;

            }
            Session["tblover"] = dt;
        }

        private void Update_Overtime()
        {

            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            bool result = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string gcod = dt.Rows[i]["gcod"].ToString();
                double fixhour = Convert.ToDouble(dt.Rows[i]["fixhour"]);
                double hourly = Convert.ToDouble(dt.Rows[i]["hourly"]);
                double c1hour = Convert.ToDouble(dt.Rows[i]["c1hour"]);
                double c2hour = Convert.ToDouble(dt.Rows[i]["c2hour"]);
                double c3hour = Convert.ToDouble(dt.Rows[i]["c3hour"]);
                double fixrate = Convert.ToDouble(dt.Rows[i]["fixrate"]);
                double hourrate = Convert.ToDouble(dt.Rows[i]["hourrate"]);
                double c1rate = Convert.ToDouble(dt.Rows[i]["c1rate"]);
                double c2rate = Convert.ToDouble(dt.Rows[i]["c2rate"]);
                double c3rate = Convert.ToDouble(dt.Rows[i]["c3rate"]);
                double todedihour = Convert.ToDouble(dt.Rows[i]["todedihour"]);


                string fixamt = (fixhour * fixrate).ToString();
                string c1amt = (c1hour * c1rate).ToString();
                string c2amt = (c2hour * c2rate).ToString();
                string c3amt = (c3hour * c3rate).ToString();
                string dedihour = todedihour.ToString();

                double tohour = Convert.ToDouble(dt.Rows[i]["tohour"]);

                double ohour = Convert.ToDouble((int)(tohour));
                double omin = Convert.ToDouble((tohour - ohour).ToString("#,##0.00;(#,##0.00);")) * 100;
                double dmin = omin > 0 ? (omin / 60) : 0.00;
                tohour = ohour + dmin;
                string houramt = Math.Round((tohour * hourrate), 0).ToString();


                double dedohour = Convert.ToDouble((int)(todedihour));
                double dedomin = Convert.ToDouble((tohour - ohour).ToString("#,##0.00;(#,##0.00);")) * 100;
                double deddmin = omin > 0 ? (omin / 60) : 0.00;
                double dedhour = dedohour + deddmin;
                string deducamt = Math.Round((dedhour * hourrate), 0).ToString();

                //if (tohour > 0)
                //{

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEOVRTIME", dayid, empid, gcod, date, fixhour.ToString(), hourly.ToString(), c1hour.ToString(), c2hour.ToString(), c3hour.ToString(), fixamt, houramt, c1amt, c2amt, c3amt, dedihour, deducamt);
                if (!result)
                    return;



                //}
            }

            if (ConstantInfo.LogStatus == true)
            {
                string ttlemp = dt.Rows.Count.ToString();
                string ttlhour = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tohour)", "")) ? 0.00
                    : dt.Compute("sum(tohour)", ""))).ToString("#,##0.00;(#,##0.00); ");

                string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string voutype = "Overtime Allowance Update";
                string eventdesc = "Month ID: " + dayid + "" + " Dated: " + todate;
                string eventdesc2 = "Overtime Allowance, Total Employe-" + ttlemp + " Total Hour-" + ttlhour + "Employe Status-" + (ddlEmpType.SelectedValue == "2" ? "Resign Employe" : "Active Employee");
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), voutype, eventdesc, eventdesc2);

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Overtime Allowance Updated Successfully');", true);

        }

        protected void lUpdate_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Overtime":
                    this.Update_Overtime();

                    break;

                case "BankPayment":
                    this.lnkFiUpdate_Click(null, null);
                    break;

                case "Holiday":
                    this.lUpdateHoliday_Click(null, null);
                    break;

                case "Mobile":
                    this.lbntUpdateMbill_Click(null, null);
                    break;


                case "Lencashment":
                    this.lbntUpdateEnLeave_Click(null, null);
                    break;

                case "OtherDeduction":
                    this.lbntUpdateOtherDed_Click(null, null);
                    break;
                case "CarSub":
                    this.lbntcsallow_Click(null, null);
                    break;

                case "loan":
                    this.lbntUpdateLoan_Click(null, null);
                    break;

                case "arrear":
                    this.lbntUpdateArrear_Click(null, null);
                    break;

                case "otherearn":
                    this.lbntUpdateOthEarn_Click(null, null);
                    break;

                case "dayadj":
                    this.btnUpdateDayAdj_Click(null, null);
                    break;

                case "EarnLeave":
                    this.lnkbtnUpdateEarnLeave(null,null);
                    break;

                case "Lencashment02":
                    this.lbntUpdateLvEnCash02_Click(null, null);
                    break;

                default:
                    break;
            }



        }

        private void lbntUpdateLvEnCash02_Click(object p1, object p2)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblover"];
                string comcod = this.GetComeCode();
                string ymon = this.ddlyearmon.SelectedValue.ToString();
                string dayid = ymon;
                string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string eleave = dt.Rows[i]["payeleave"].ToString();
                    double encleave = Convert.ToDouble(dt.Rows[i]["eneleave"]);
                    double encleaveamt = Convert.ToDouble(dt.Rows[i]["netpayable"]);
                    if (encleave > 0)
                    {
                        bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEELEAVE", dayid, empid, date, eleave, encleave.ToString(), encleaveamt.ToString(), "", "", "", "", "", "", "", "");
                        if (!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                            return;
                        }
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Leave Encashment Updated Successfully');", true);
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }

        private void lnkbtnUpdateEarnLeave(object p1, object p2)
        {
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string yearid = ymon.Substring(0,4);
            DateTime date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4));
            string yenddate = date.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string ystrtdate = Convert.ToDateTime(yenddate).AddYears(-1).AddDays(1).ToString("dd-MMM-yyyy");
            string gcod = "51001";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                double eleave = Convert.ToDouble(dt.Rows[i]["payeleave"].ToString());
                if (eleave > 0)
                {

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "UPDATE_EMP_EARN_LEAVE", yearid, ystrtdate, yenddate, empid, gcod, eleave.ToString(),  "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ HRData.ErrorObject["Msg"].ToString()+"');", true);
                        return;
                    }
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Earn Leave Updated Successfully');", true);
        }

        protected void gvEmpOverTime_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex = (this.gvEmpOverTime.PageSize) * (this.gvEmpOverTime.PageIndex) + e.RowIndex;
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string empid = dt.Rows[rowindex]["empid"].ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELEMPOVRTIME", dayid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {

                dt.Rows[rowindex].Delete();
            }
            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();


        }


        protected void gvEmpHoliday_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpHoliday.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ChkAllEmp_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblover"];
            int i, index;
            if (((CheckBox)this.gvEmpHoliday.HeaderRow.FindControl("ChkAllEmp")).Checked)
            {

                for (i = 0; i < this.gvEmpHoliday.Rows.Count; i++)
                {
                    ((CheckBox)this.gvEmpHoliday.Rows[i].FindControl("chkHoliday")).Checked = true;
                    index = (this.gvEmpHoliday.PageSize) * (this.gvEmpHoliday.PageIndex) + i;
                    dt.Rows[index]["hstatus"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvEmpHoliday.Rows.Count; i++)
                {
                    ((CheckBox)this.gvEmpHoliday.Rows[i].FindControl("chkHoliday")).Checked = false;
                    index = (this.gvEmpHoliday.PageSize) * (this.gvEmpHoliday.PageIndex) + i;
                    dt.Rows[index]["hstatus"] = "False";

                }

            }

            Session["tblover"] = dt;
        }
        protected void lUpdateHoliday_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();




            string date = Convert.ToDateTime(this.ddlyearmon.Text.Trim()).ToString("dd-MMM-yyyy");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string gcod = dt.Rows[i]["gcod"].ToString();
                string txthstatus = dt.Rows[i]["hstatus"].ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEHOLIDAY", "", empid, gcod, "", txthstatus, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;

            }
            //this.lblmsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }
        protected void gvEmpMbill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpMbill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbntUpdateMbill_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string gcod = dt.Rows[i]["gcod"].ToString();
                double mbill = Convert.ToDouble(dt.Rows[i]["mbillamt"]);
                double payamt = Convert.ToDouble(dt.Rows[i]["payamt"]);
                string mobile = dt.Rows[i]["phone"].ToString();
                if (mbill > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEMBILL", Monthid, empid, gcod, mbill.ToString(), payamt.ToString(), mobile, "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;

                }
            }
            
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Mobile Bill Updated Successfully');", true);
        }
        protected void lbtnTotalmBill_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnTotalEnLeave_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbntUpdateEnLeave_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();


            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                //   string gcod = dt.Rows[i]["gcod"].ToString();
                string eleave = dt.Rows[i]["eleave"].ToString();
                double ecleave = Convert.ToDouble(dt.Rows[i]["ecleave"]);
                double ecleaveamt = Convert.ToDouble(dt.Rows[i]["ecleaveamt"]);
                if (ecleave > 0)
                {

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEELEAVE", dayid, empid, date, eleave, ecleave.ToString(), ecleaveamt.ToString(), "", "", "", "", "", "", "", "");
                    if (!result)
                        return;
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
        }
        protected void lbtnTotalOtherDed_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbntUpdateOtherDed_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string lvded = Convert.ToDouble(dt.Rows[i]["lvded"]).ToString();
                string arded = Convert.ToDouble(dt.Rows[i]["arded"]).ToString();
                string saladv = Convert.ToDouble(dt.Rows[i]["saladv"]).ToString();
                string otherded = Convert.ToDouble(dt.Rows[i]["otherded"]).ToString();
                string mbillded = Convert.ToDouble(dt.Rows[i]["mbillded"]).ToString();
                string fallded = Convert.ToDouble(dt.Rows[i]["fallded"]).ToString();
                string spcded = Convert.ToDouble(dt.Rows[i]["spcded"]).ToString();
                double toamt = Convert.ToDouble(dt.Rows[i]["toamt"]);
                if (toamt > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPOTHERDED", Monthid, empid, lvded, arded, saladv, otherded, mbillded, fallded, spcded, "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }

                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
        }
        protected void gvEmpOtherded_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpOtherded.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvEmploan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmploan.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnTotalLoan_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbntUpdateLoan_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();

                string cloan = Convert.ToDouble(dt.Rows[i]["cloan"]).ToString();
                string pfloan = Convert.ToDouble(dt.Rows[i]["pfloan"]).ToString();

                double toamt = Convert.ToDouble(dt.Rows[i]["toamt"]);
                if (toamt > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPLOANDED", Monthid, empid, cloan, pfloan, "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;

                }
            }
            //this.lblmsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            int j;
            switch (type)
            {

                case "Overtime":
                case "CarSub":
                case "Mobile":
                case "OtherDeduction":
                case "Holiday":
                case "Lencashment":
                case "loan":
                case "arrear":
                case "otherearn":
                case "dayadj":
                    secid = dt1.Rows[0]["secid"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["secid"].ToString() == secid)
                        {
                            secid = dt1.Rows[j]["secid"].ToString();
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {
                            secid = dt1.Rows[j]["secid"].ToString();
                        }

                    }

                    break;


                case "BankPayment":
                    string actcode = dt1.Rows[0]["actcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            dt1.Rows[j]["actdesc"] = "";
                        }

                        else
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                        }

                    }
                    break;




            }



            return dt1;

        }
        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveValue();

            DataTable dt = (DataTable)Session["tblover"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string bankno = dt.Rows[i]["bankseno"].ToString();
                string acno = dt.Rows[i]["bankacno"].ToString();
                double amount = Convert.ToDouble(dt.Rows[i]["bankamt"].ToString());
                string remarks = dt.Rows[i]["remarks"].ToString();

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTBANKPAYINF", empid, acno, bankno, amount.ToString(), remarks, "", "", "", "", "", "", "", "", "", "");

            }
            //this.lblmsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
        }


        protected void gvBankPay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvBankPay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvarrear_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvarrear.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnCalArrear_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }

        protected void lbtnTotalArrear_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;
            for (int i = 0; i < this.gvarrear.Rows.Count; i++)
            {
                double arrear = Convert.ToDouble("0" + ((TextBox)this.gvarrear.Rows[i].FindControl("txtarrear")).Text.Trim());
                double pf = Convert.ToDouble("0" + ((TextBox)this.gvarrear.Rows[i].FindControl("txtPFAmt")).Text.Trim());
                double itax = Convert.ToDouble("0" + ((TextBox)this.gvarrear.Rows[i].FindControl("txtitaxAmt")).Text.Trim());
                string remarks = Convert.ToString(((TextBox)this.gvarrear.Rows[i].FindControl("txtRemrks")).Text.Trim());
                rowindex = (this.gvarrear.PageSize) * (this.gvarrear.PageIndex) + i;
                dt.Rows[rowindex]["aramt"] = arrear;
                dt.Rows[rowindex]["pfamt"] = pf;
                dt.Rows[rowindex]["itax"] = itax;
                dt.Rows[rowindex]["tapfamt"] = arrear - pf - itax;
                dt.Rows[rowindex]["remarks"] = remarks;
            }
            Session["tblover"] = dt;
            this.Data_Bind();
        }

        protected void lbntUpdateArrear_Click(object sender, EventArgs e)
        {
            //this.lblmsg.Visible = true;
            this.lbtnTotalArrear_Click(null, null);
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            foreach (DataRow dr1 in dt.Rows)
            {
                string empid = dr1["empid"].ToString();
                //string gcod = dt.Rows[i]["gcod"].ToString();
                double arrer = Convert.ToDouble("0" + dr1["aramt"]);
                double pfamt = Convert.ToDouble("0" + dr1["pfamt"]);
                double itax = Convert.ToDouble("0" + dr1["itax"]);
                string remarks = Convert.ToString(dr1["remarks"]);
                if (arrer > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTARREAR", Monthid, empid, arrer.ToString(), pfamt.ToString(), remarks, userid, trmid, sessionid, postDat, itax.ToString(), "", "", "", "", "");
                    if (!result)
                        return;
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
        }





        protected void gvEmpMbill_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.gvEmpMbill.Rows[e.RowIndex].FindControl("lgvEmpId")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMBILL", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;
            //this.lblmsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            if (result == true)
            {
                int rowindex = (this.gvEmpMbill.PageSize) * (this.gvEmpMbill.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void gvEmpELeave_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            //string dayid = Convert.ToDateTime(this.ddlyearmon.Text.Trim()).ToString("yyyyMMdd");
            string empid = ((Label)this.gvEmpELeave.Rows[e.RowIndex].FindControl("lgvEmpId")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMLEAVE", dayid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            if (result == true)
            {
                int rowindex = (this.gvEmpELeave.PageSize) * (this.gvEmpELeave.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void gvEmpOtherded_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.gvEmpOtherded.Rows[e.RowIndex].FindControl("lgvEmpId")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMOTDEC", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            if (result == true)
            {
                int rowindex = (this.gvEmpOtherded.PageSize) * (this.gvEmpOtherded.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void gvarrear_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.gvarrear.Rows[e.RowIndex].FindControl("lgvEmpId")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMARSAL", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            if (result == true)
            {
                int rowindex = (this.gvarrear.PageSize) * (this.gvarrear.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void gvothearn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvothearn.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnTotalOthEarn_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbntUpdateOthEarn_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            bool result = false;
            //bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_EMPLOYEE01", "DELETEEMPOTHEARN", Monthid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //    return;

            foreach (DataRow dr in dt.Rows)
            {
                string empid = dr["empid"].ToString();
                string foodallow = Convert.ToDouble(dr["foodallow"]).ToString();
                string tptallow = Convert.ToDouble(dr["tptallow"]).ToString();
                string kpi = Convert.ToDouble(dr["kpi"]).ToString();
                string perbon = Convert.ToDouble(dr["perbon"]).ToString();
                string othearn = Convert.ToDouble(dr["othearn"]).ToString();
                double totalam = Convert.ToDouble(dr["totalam"]);
                if (totalam > 0)
                {
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTOTHEARN", Monthid, empid, tptallow, kpi, perbon, othearn, foodallow, "", "", "", "", "", "", "", "");
                    if (!result)
                        return;
                }
            }
            //this.lblmsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }



        protected void grvAdjDay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvAdjDay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnTotalDay_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void btnUpdateDayAdj_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string monthid = this.ddlyearmon.Text.Trim();
            bool result = false;
            ///--------------------------------------------------////////////
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //////----------------------------------------------------------/////////////
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string delday = Convert.ToDouble("0" + dt.Rows[i]["dalday"]).ToString();
                string aprday = Convert.ToDouble("0" + dt.Rows[i]["aprday"]).ToString();
                double dedday = Convert.ToDouble("0" + dt.Rows[i]["dedday"]);
                //if (dedday > 0)
                //{
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "INSERTORUPEMPSALADJST", monthid, empid, dedday.ToString(), userid, Terminal, Sessionid, Posteddat, delday, aprday, "", "", "", "", "", "");
                if (!result)
                    return;
                //  }
            }
            //this.lblmsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
        }
        protected void grvAdjDay_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.grvAdjDay.Rows[e.RowIndex].FindControl("lgvEmpIdAdj")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "DELETESALADJST", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            if (result == true)
            {
                int rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void lbtnCalCulationSadj_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;
            for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
            {
                double delayday = Convert.ToDouble("0" + ((Label)this.grvAdjDay.Rows[i].FindControl("lblgvDelday")).Text.Trim());
                double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                double redelay = delayday - Aprvday;
                dt.Rows[rowindex]["aprday"] = Aprvday;
                dt.Rows[rowindex]["dedday"] = Convert.ToInt32(Convert.ToInt32(redelay) / 3);

            }

            Session["tblover"] = dt;
            this.Data_Bind();
        }
        protected void gvothearn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.gvothearn.Rows[e.RowIndex].FindControl("lgvEmpIdearn")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMPOTHERN", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            if (result == true)
            {
                int rowindex = (this.gvothearn.PageSize) * (this.gvothearn.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void chkcopy_CheckedChanged(object sender, EventArgs e)
        {

            if (this.chkcopy.Checked)
            {
                this.GetPreYearMonth();
            }
            this.pnlCopy.Visible = (this.chkcopy.Checked);
        }
        protected void lbtnCopy_Click(object sender, EventArgs e)
        {
            Session.Remove("tblover");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string comnam = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string JobLocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlpreyearmon.Text.Trim(), 2) + "/01/" + this.ddlpreyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string MonthId = this.ddlpreyearmon.Text.Trim();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMOBILEBILLINFO", deptname, MonthId, date, comnam, Empcode, divison, section, JobLocation, userid);
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvEmpMbill.DataSource = null;
                this.gvEmpMbill.DataBind();
                return;
            }
            Session["tblover"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
            this.chkcopy.Checked = false;
            this.chkcopy_CheckedChanged(null, null);
        }




        protected void btnexcuplosd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["XcelData"];
                DataTable emp = (DataTable)Session["tblover"];
                if (emp.Rows.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < emp.Rows.Count; i++)
                {
                    DataRow[] rows = dt.Select("Card_no ='" + emp.Rows[i]["idcardno"] + "'");

                    if (rows.Length > 0)
                    {
                        if (this.Request.QueryString["Type"].ToString() == "OtherDeduction")
                        {
                            emp.Rows[i]["lvded"] = Convert.ToDouble("0" + rows[0]["Leave_Ded"]);
                            emp.Rows[i]["arded"] = Convert.ToDouble("0" + rows[0]["PF_Ded"]);
                            emp.Rows[i]["saladv"] = Convert.ToDouble("0" + rows[0]["Advance_Ded"]);
                            emp.Rows[i]["fallded"] = Convert.ToDouble("0" + rows[0]["Food_Ded"]);
                            emp.Rows[i]["mbillded"] = Convert.ToDouble("0" + rows[0]["Mobile_Ded"]);
                            emp.Rows[i]["spcded"] = Convert.ToDouble("0" + rows[0]["Special_Ded"]);
                            emp.Rows[i]["otherded"] = Convert.ToDouble("0" + rows[0]["Other_Ded"]);
                        }
                        if (this.Request.QueryString["Type"].ToString() == "arrear")
                        {
                            emp.Rows[i]["aramt"] = Convert.ToDouble(rows[0]["Arrear_Salary"]);
                        }
                    }

                }

                Session["tblover"] = emp;
                this.Data_Bind();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }

        protected void imgbtnjoblocation_Click(object sender, EventArgs e)
        {

        }

        protected void gvCarSub_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvCarSub.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvCarSub_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void lbtncsAloowance_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            string qtype = this.Request.QueryString["Type"].ToString().Trim().Trim();

            if (qtype == "CarSub")
            {
                lTotal_Click(null, null);

            }
            if (qtype == "OtherDeduction")
            {
                lbtnTotalOtherDed_Click(null, null);

            }
        }





        protected void lbntcsallow_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();

                string gsalary = Convert.ToDouble(dt.Rows[i]["gsalary"]).ToString();
                string carallow = Convert.ToDouble(dt.Rows[i]["carallow"]).ToString();
                string arcallow = Convert.ToDouble(dt.Rows[i]["arcallow"]).ToString();
                string suballowance = Convert.ToDouble(dt.Rows[i]["suballowance"]).ToString();
                string asallow = Convert.ToDouble(dt.Rows[i]["asallow"]).ToString();
                string netpay = Convert.ToDouble(dt.Rows[i]["netpay"]).ToString();
                string subbonus = Convert.ToDouble(dt.Rows[i]["subbonus"]).ToString();



                double toamt = Convert.ToDouble(dt.Rows[i]["netpay"]);
                if (toamt > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTCARALLOWANCE", Monthid, empid, gsalary, carallow, arcallow, suballowance, asallow, netpay, subbonus, "", "", "", "", "", "");
                    if (!result)
                        return;

                }
            }
            //this.lblmsg.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }

        protected void gvOtHourly_Click(object sender, EventArgs e)
        {
            string comcod = GetComeCode();

            this.lbmodalheading.Text = "Individual Monthly Over Time Details Information. Date :" + this.ddlyearmon.SelectedItem.Text.ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;


            string Empcode = ((Label)this.gvEmpOverTime.Rows[index].FindControl("lblEmpidOT")).Text.ToString(); // "%" + this.txtSrcEmployee.Text.Trim() + "%";

            DataTable dt = (DataTable)Session["tblOtDetails"];


            DataView dv = dt.DefaultView;
            dv.RowFilter = "empid=" + Empcode;
            dt = dv.ToTable();

            //DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL03", "OVERTIMESALARY", frmdate, todate, Empcode);
            //if (ds2 == null)
            //{
            //    this.mgvbreakdown.DataSource = null;
            //    this.mgvbreakdown.DataBind();
            //    return;
            //}
            this.mgvbreakdown.DataSource = dt;
            this.mgvbreakdown.DataBind();


            ((Label)this.mgvbreakdown.FooterRow.FindControl("mlgvFDelday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ovthour)", "")) ? 0.00 : dt.Compute("sum(ovthour)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.mgvbreakdown.FooterRow.FindControl("mlgvFovtmin")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ovtmin)", "")) ? 0.00 : dt.Compute("sum(ovtmin)", ""))).ToString("#,##0;(#,##0); ");

            Session["Report1"] = mgvbreakdown;
            if (dt.Rows.Count > 0)
                ((HyperLink)this.mgvbreakdown.HeaderRow.FindControl("mhlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void lnkbtnGenLeave_Click(object sender, EventArgs e)
        {
            string dedhour = Convert.ToDouble("0" + this.txtdedicationHour.Text).ToString();
            DataTable dt = (DataTable)Session["tblover"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string hour = Convert.ToDouble("0" + dt.Rows[i]["hourly"]).ToString();
                dt.Rows[i]["todedihour"] = (Convert.ToDouble(hour) > Convert.ToDouble(dedhour) ? dedhour : hour);
            }
            Session["tblover"] = dt;
            this.Data_Bind();



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

        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string type = this.Request.QueryString["Type"].ToString();
                DataTable dt1 = (DataTable)Session["tblover"];
                DataTable dtemp = (DataTable)Session["tblemplist"];
                this.divAddEmp.Visible = this.chkAddEmp.Checked;
                Session.Remove("tblemp");
                this.CreateDataTable();
                DataTable dt = (DataTable)Session["tblemp"];
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

                }
                Session.Remove("tblover");
                Session.Remove("tbladdempover");
                DataTable dt2 = dt1.Copy();
                Session["tbladdempover"] = dt2;
                DataTable dt3 = dt1.Clone();
                Session["tblover"] = dt3;

                switch (type)
                {
                    case "Mobile":
                        this.ddlEmployee.DataTextField = "empname1";
                        this.ddlEmployee.DataValueField = "empid";
                        this.ddlEmployee.DataSource = dtemp;
                        this.DataBind();
                        break;

                    default:
                        this.ddlEmployee.DataTextField = "empname";
                        this.ddlEmployee.DataValueField = "empid";
                        this.ddlEmployee.DataSource = dt;
                        this.DataBind();
                        break;
                }

                //GridView DataBind
                this.Data_Bind();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + ex.Message + "');", true);
            }
        }
        protected void lbtnAddEmployee_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];
            DataTable dtadd = (DataTable)Session["tbladdempover"];
            DataTable dtemp = (DataTable)Session["tblemplist"];
            string empid = this.ddlEmployee.SelectedValue.ToString();
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Mobile":
                    DataRow[] dr1 = dt.Select("empid='" + empid + "'");
                    if (dr1.Length == 0)
                    {
                        DataRow[] dra = dtemp.Select("empid='" + empid + "'");
                        dt.ImportRow(dra[0]);
                    }
                    else
                    {

                        string existempdet = "Employee : " + dr1[0]["idcardno"].ToString() + " - " + dr1[0]["empname"].ToString() + " already existed!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + existempdet + "');", true);
                    }
                    break;

                default:
                    DataRow[] dr2 = dt.Select("empid='" + empid + "'");
                    if (dr2.Length == 0)
                    {
                        DataRow[] dra = dtadd.Select("empid='" + empid + "'");
                        dt.ImportRow(dra[0]);
                    }
                    else
                    {

                        string existempdet = "Employee : " + dr2[0]["idcardno"].ToString() + " - " + dr2[0]["empname"].ToString() + " already existed!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + existempdet + "');", true);
                    }
                    break;
            }           

            DataView dv = dt.DefaultView;
            dv.Sort = ("secid,idcardno");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void gvEarnLvEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEarnLvEntry.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvLvEnCashmnt02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLvEnCashmnt02.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}