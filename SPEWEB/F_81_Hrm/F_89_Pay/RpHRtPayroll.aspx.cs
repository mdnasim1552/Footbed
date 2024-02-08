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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class RpHRtPayroll : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        //Tuple<string, string> CompInfoBn = ASTUtility.CompInfoBn();
        int curd;
        int frdate;
        int mon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = 
                      (type == "Salary") ? "Salary Sheet"
                    : (type == "SalaryHold") ? "Hold Salary Sheet"
                    : (type == "Bonus") ? "Festival Bonus Sheet"
                    : (type == "CashPay") ? "EMPLOYEE CASH PAYMENT INFORMATION"
                    : (type == "OvertimeSal") ? "Over Time Salary"
                    : (type == "SalaryReg") ? "Resign Salary Sheet"
                    : (type == "SalaryOT") ? "Salary OT Sheet"
                    : "EMPLOYEE PAY SLIP INFORMATION";
                this.txtpayment.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                GetWorkStation();
                GetAllOrganogramList();
                GetJobLocation();
                this.CommonButton();
                this.GetLineddl();
                this.SelectType();
                this.GetBonusType();
                this.GetSepType();
                //this.GetEmployeeGrade();
                //this.ddlGrade_SelectedIndexChanged(null, null);

            }
           

        }

        public void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = CheckBoxMaternity.Checked== true?false:true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = this.Request.QueryString["Type"].ToString() == "Bonus" ? true : false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).ToolTip = "Final Update";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).ToolTip = "Total Calculation";

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
        }
        private void GetSepType()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GET_SEAPRATION_TYPE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            this.ddlSepType.DataTextField = "hrgdesc";
            this.ddlSepType.DataValueField = "hrgcod";
            this.ddlSepType.DataSource = ds1.Tables[0];
            this.ddlSepType.DataBind();
        }
        private void GetEmployeeGrade()
        {
            string comcod = this.GetCompCode();
            string emptype = ddlWstation.SelectedValue;
            emptype = emptype == "000000000000" ? "%" : emptype.Substring(0,4);
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPT_EMPLOYEE_GRADE", emptype,"", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            DataTable dt = ds1.Tables[0];
            if (emptype == "9403")
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "[dhrgcod] not in ('0333','0338','0340','0350')";
                dt = dv.ToTable();
            }

            this.empGradeDropDownList.DataTextField = "hrgdesc";
            this.empGradeDropDownList.DataValueField = "dhrgcod";
            this.empGradeDropDownList.DataSource = dt;
            this.empGradeDropDownList.DataBind();
            Session["EmployeeGrade"] = dt;
        }
        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            //string dhrgcod = this.empGradeDropDownList.SelectedValue;
           // dhrgcod = dhrgcod == "0000" ? "03%" : dhrgcod;
            string emptype = ddlWstation.SelectedValue;
            emptype = emptype == "000000000000" ? "%" : emptype.Substring(0, 4);

            List<string> dhrgcodList = new List<string>();
            foreach (ListItem litem in empGradeDropDownList.Items)
            {

                if (litem.Selected)
                {
                    dhrgcodList.Add(litem.Value);

                }

            }
            string dhrgcod = "03%";
            if (dhrgcodList.Count != 0)
            {
                dhrgcod = string.Join(", ", dhrgcodList);
            }
            

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPT_EMPLOYEE_DESIGNATION", dhrgcod, emptype, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            
            this.empDesignationDropDownList.DataTextField = "hrgdesc";
            this.empDesignationDropDownList.DataValueField = "hrgcod";
            this.empDesignationDropDownList.DataSource = ds1.Tables[0];
            this.empDesignationDropDownList.DataBind();
        }
       
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetLineddl()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;

            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";

        }
        private void GetBonusType ()
        {
            string comcod = GetCompCode();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GET_BONUS_TYPE", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
                return;

            this.ddlBonusType.DataTextField = "hrgdesc";
            this.ddlBonusType.DataValueField = "hrgcod";
            this.ddlBonusType.DataSource = ds4.Tables[0];
            this.ddlBonusType.DataBind();
        }
        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "Salary":
                case "SalaryOT":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.divPayType.Visible = true;
                    this.rbtPaymentType.SelectedIndex = 2;
                    this.divResign.Visible = false;
                    this.CompanySalary();
                    this.txtpayment.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                case "SalaryHold":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.divPayType.Visible = true;
                    this.rbtPaymentType.SelectedIndex = 2;
                    this.divResign.Visible = false;
                    this.CompanySalary();
                    this.txtpayment.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                case "SalaryReg":
                    this.divPayType.Visible = false;
                    this.divResign.Visible = true;
                    this.ChckResign.Checked = true;
                    this.ChckResign.Enabled = false;
                    this.divSepType.Visible = true;
                    this.rbtPaymentType.SelectedIndex = 2;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.CompanySalary();
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                case "Bonus":
                    this.rbtlBonSheet.Visible = false;
                    this.rbtPaymentType.SelectedIndex = 2;
                    this.MultiView1.ActiveViewIndex = 1;
                    this.CompanyBonus();
                    this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.lblfrmdate.Text = "Date";
                    this.divToDate.Visible = false;
                    this.lbltAtype2.Visible = true;
                    this.divPayType.Visible = true;
                    this.lbltAtype2.Visible = true;
                    this.lblline.Visible = true;
                    this.ddlempline.Visible = true;
                    break;

                case "Payslip":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.lblline.Visible = true;
                    this.ddlempline.Visible = true;
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                case "Signature":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                case "CashPay":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                case "OvertimeSalary":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                case "OvertimeSal":
                    this.divPayType.Visible = true;
                    this.rbtPaymentType.SelectedIndex = 2;
                    this.MultiView1.ActiveViewIndex = 6;
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.ddlWstation.SelectedValue = "940300000000";
                    this.ddlWstation.Enabled = false;
                    ddlWstation_SelectedIndexChanged(null, null);
                    break;

            }

        }

        private void Get_Bank_name()
        {
            string comcod = this.GetCompCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETGENINFO", "%", "", "", "", "", "", "", "", "");
            DataTable dt = ds5.Tables[8].DefaultView.ToTable(true, "actcode", "actdesc");

            DataRow dr = dt.NewRow();
            dr["actcode"] = "000000000000";
            dr["actdesc"] = "All Bank";
            dt.Rows.Add(dr);
            this.ddlBank.DataTextField = "actdesc";
            this.ddlBank.DataValueField = "actcode";
            this.ddlBank.DataSource = dt;
            this.ddlBank.DataBind();
            this.ddlBank.SelectedValue = "000000000000";
        }

        private void CompanySalary()
        {
            // this.rbtSalSheet.Visible = false;
            this.rbtSalSheet.Visible = false;
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "5301"://Edison
                            //  this.rbtSalSheet.Visible = true;
                    this.rbtSalSheet.SelectedIndex = 0;
                    break;
                case "5305"://FB Footwear
                case "5306"://Footbed Footwear
                            //  this.rbtSalSheet.Visible = true;
                    this.rbtSalSheet.SelectedIndex = 1;
                    break;

                default:
                    //  this.rbtSalSheet.Visible = true;
                    this.rbtSalSheet.SelectedIndex = 0;
                    break;


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

        private void CompanyBonus()
        {

            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "5301": //Edison
                    this.rbtlBonSheet.SelectedIndex = 0;
                    break;

                case "5305": //FB
                case "5306": //FB
                    this.rbtlBonSheet.SelectedIndex = 1;
                    break;

                default: //Edison
                    this.rbtlBonSheet.SelectedIndex = 0;
                    break;

            }
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "Salary":
                case "SalaryReg":
                    if (this.lnkbtnShow.Text == "New")
                    {
                        this.lnkbtnShow.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.chkAddEmp.Checked = false;
                        this.divAddEmp.Visible = false;
                        this.gvpayroll.DataSource = null;
                        this.gvpayroll.DataBind();
                        return;

                    }

                    this.lnkbtnShow.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.ShowSal();
                    break;

                case "SalaryOT":
                    if (this.lnkbtnShow.Text == "New")
                    {
                        this.lnkbtnShow.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.divAddEmp.Visible = false;
                        this.gvpayroll.DataSource = null;
                        this.gvpayroll.DataBind();
                        return;

                    }

                    this.lnkbtnShow.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.ShowSalOT();
                    break;

                case "SalaryHold":
                    if (this.lnkbtnShow.Text == "New")
                    {
                        this.lnkbtnShow.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.divAddEmp.Visible = false;
                        this.gvpayroll.DataSource = null;
                        this.gvpayroll.DataBind();
                        return;

                    }

                    this.lnkbtnShow.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.ShowHoldSal();
                    break;

                case "Bonus":
                    this.ShowBonus();
                    this.divChkEmp.Visible = true;
                    break;

                case "Payslip":
                    this.divChkEmp.Visible = true;
                    this.ShowPaySlip();
                    break;


                case "Signature":
                    this.ShowSignature();
                    break;


                case "CashPay":
                    this.EmpCashPay();
                    break;

                case "OvertimeSalary":
                    this.ShowEmpOvertimeSalary();
                    break;

                case "OvertimeSal":
                    this.ShowEmpOvertimeSalary02();
                    break;

            }

        }

        private void ShowSal()
        {
            Session.Remove("tblpay");
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string sepType = (type == "SalaryReg" ? ((this.ddlSepType.SelectedValue == "00000" ? "75" : this.ddlSepType.SelectedValue.ToString()) + "%") : "");
            string saltyp = (type == "SalaryReg" ? "R" : type == "SalaryHold" ? "H" : "");
            //string grade = this.empGradeDropDownList.SelectedValue;
            //grade = grade == "0000" ? "%" : grade;
            //string designation = this.empDesignationDropDownList.SelectedValue;
            //designation = designation == "0000000" ? "%" : designation;

            List<string> gradeList = new List<string>();
            foreach (ListItem litem in empGradeDropDownList.Items)
            {

                if (litem.Selected)
                {
                    gradeList.Add(litem.Value);

                }

            }
            List<string> designationList = new List<string>();
            foreach (ListItem litem in empDesignationDropDownList.Items)
            {

                if (litem.Selected)
                {
                    designationList.Add(litem.Value);

                }

            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string CompanyName = (this.ddlWstation.SelectedValue.ToString().Substring(0, 4)=="0000"?"94": this.ddlWstation.SelectedValue.ToString().Substring(0, 4))+"%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string projectcode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string bank = (this.ddlBank.SelectedValue.ToString() == "000000000000" ? "19%" : this.ddlBank.SelectedValue.ToString()) + "%";
            string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            if (payType == "")
            {
                bank = "%";
            }
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
            mon = this.Datediffday1(Convert.ToDateTime(curdate), Convert.ToDateTime(dt1));
            DataSet ds3;
            string EmpType = this.ddlWstation.SelectedValue.ToString();
            
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SALLOCK", monthid, EmpType, saltyp, "", "", "", "", "", "");
            Session["UserLog"] = ds1.Tables[0];
            this.lblComSalLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();

            // PAYROLL_DETAIL06 for edison
            string CallType = (this.rbtSalSheet.SelectedIndex == 0) ? "PAYROLL_DETAIL06" : (this.rbtSalSheet.SelectedIndex == 1) ? "PAYROLL_DETAILFB" : "PAYROLL_DETAIL";           
            string ProName = (this.rbtSalSheet.SelectedIndex == 0 || this.rbtSalSheet.SelectedIndex == 1) ? "dbo_hrm.SP_REPORT_PAYROLL03" : "dbo_hrm.SP_REPORT_PAYROLL";   

            //Lock Salary
            if (this.lblComSalLock.Text == "True")
            {
                ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPT_BACSALARY", null, null, null, monthid, projectcode, section, CompanyName, saltyp, payType, division, line, bank, joblocation, userid);

                if (ds3.Tables[0].Rows.Count == 0)
                    ds3 = HRData.GetTransInfoNew(comcod, ProName, CallType, null, null, null, frmdate, todate, projectcode, section, CompanyName, saltyp, division, payType, line, bank, joblocation);
            }           
            //Actual Salary
            else
            {

                ds3 = HRData.GetTransInfoNew(comcod, ProName, CallType, null, null, null, frmdate, todate, projectcode, section, CompanyName, saltyp, division, payType, line, bank, joblocation, sepType, userid);
            }    
            if (ds3 == null)
            {
                this.gvpayroll.DataSource = null;
                this.gvpayroll.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            EnumerableRowCollection<DataRow> filteredRows= from DataRow row in dt.AsEnumerable()                                        
                                                           select row; ;
            if (gradeList.Count != 0)
            {
                filteredRows = from DataRow row in dt.AsEnumerable()
                                                    where gradeList.Contains(row.Field<string>("desigid").Substring(0, 4))
                                                    select row;
            }
            if (designationList.Count != 0)
            {
                filteredRows = from DataRow row in dt.AsEnumerable()
                                                    where designationList.Contains(row.Field<string>("desigid"))
                                                    select row;
            }
            //if (grade != "%")
            //{
            //    filteredRows = from DataRow row in dt.AsEnumerable()
            //                   where row.Field<string>("desigid").Substring(0, 4) == grade
            //                   select row;
            //    //from DataRow row in dt.AsEnumerable()
            //    //where new[] { "123", "456", "789" }.Contains(row.Field<string>("desigid"))
            //    //select row;
            //    //List<string> desigidList = new List<string> { "123", "456", "789" };

            //    //IEnumerable<DataRow> selectedRows = from DataRow row in dt.AsEnumerable()
            //    //                                    where desigidList.Contains(row.Field<string>("desigid"))
            //    //                                    select row;
            //}
            //if (designation != "%")
            //{
            //    filteredRows = from DataRow row in dt.AsEnumerable()
            //                   where  row.Field<string>("desigid") == designation
            //                   select row;
            //}
            //filteredRows = from DataRow row in dt.AsEnumerable()
            //                   where row.Field<string>("desigid").Substring(0, 4) == grade && row.Field<string>("desigid")== designation
            //                   select row;
            if(filteredRows.Any() == false)
            {
                return;
            }
            DataTable filteredDataTable = filteredRows.CopyToDataTable();
           
            Session["tblpay"] = filteredDataTable;
            this.LoadGrid();

        }
        private void ShowSalOT()
        {
            Session.Remove("tblpay");
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string saltyp = (type == "SalaryReg" ? "R" : type == "SalaryHold" ? "H" : "");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string CompanyName = (this.ddlWstation.SelectedValue.ToString().Substring(0, 4) == "0000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string projectcode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string bank = (this.ddlBank.SelectedValue.ToString() == "000000000000" ? "19%" : this.ddlBank.SelectedValue.ToString()) + "%";
            string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            if (payType == "")
            {
                bank = "%";
            }
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
            mon = this.Datediffday1(Convert.ToDateTime(curdate), Convert.ToDateTime(dt1));
            DataSet ds3;
            string EmpType = this.ddlWstation.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SALLOCK", monthid, EmpType, saltyp, "", "", "", "", "", "");
            Session["UserLog"] = ds1.Tables[0];
            this.lblComSalLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();

            //Lock Salary
            if (this.lblComSalLock.Text == "True")
            {
                ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPT_BACSALARY", null, null, null, monthid, projectcode, section, CompanyName, saltyp, payType, division, line, bank, joblocation);

                if (ds3.Tables[0].Rows.Count == 0)
                    ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "PAYROLL_DETAILFB_OT", null, null, null, frmdate, todate, projectcode, section, CompanyName, saltyp, division, payType, line, bank, joblocation);
            }
            //Actual Salary
            else
            {

                ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "PAYROLL_DETAILFB_OT", null, null, null, frmdate, todate, projectcode, section, CompanyName, saltyp, division, payType, line, bank, joblocation);
            }
            if (ds3 == null)
            {
                this.gvpayroll.DataSource = null;
                this.gvpayroll.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;
            this.LoadGrid();

        }
        private void ShowHoldSal()
        {
            Session.Remove("tblpay");
            string saltyp = (this.ChckResign.Checked == true) ? "R" : "";
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string CompanyName = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string CompanyName = (this.ddlWstation.SelectedValue.ToString().Substring(0, 4) == "0000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string projectcode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string bank = (this.ddlBank.SelectedValue.ToString() == "000000000000" ? "19%" : this.ddlBank.SelectedValue.ToString()) + "%";
            string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            if (payType == "")
            {
                bank = "%";
            }
            string EmpType = this.ddlWstation.SelectedValue.ToString();
           
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SALLOCK", monthid, EmpType, "H", "", "", "", "", "", "");
            Session["UserLog"] = ds1.Tables[0];
            this.lblComSalLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();
            DataSet ds3;
            if (this.lblComSalLock.Text == "True")
            {
                ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPT_BACSALARY", null, null, null, monthid, projectcode, section, CompanyName, "H", payType, division, line, bank, joblocation);
            }
            else
            {
                ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "PAYROLL_HOLD_DETAILFB", null, null, null, frmdate, todate, projectcode, section, CompanyName, saltyp,
                    division, payType, line, bank, joblocation);
            }
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvpayroll.DataSource = null;
                this.gvpayroll.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;
            this.LoadGrid();

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
                    ComGross = ""; ;
                    break;



            }

            return ComGross;
        }


        private void ShowBonus()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string CompanyName = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string projectcode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            //string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "Cash" : (this.rbtPaymentType.SelectedIndex == 1) ? "Bank" : (this.rbtPaymentType.SelectedIndex == 2) ? "" : "";

            string monthid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMM").ToString();
            DataSet ds3;
            string workstation = this.ddlWstation.SelectedValue.ToString();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "BONLOCK", monthid, workstation, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                return;
            }

            this.lblComBonLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();
            string Calltype = "";
            switch (comcod)
            {
                case "5301"://Edison
                    if (CompanyName == "9404" || CompanyName == "9403" || CompanyName == "9402")
                    {
                        Calltype = (this.rbtlBonSheet.SelectedIndex == 0) ? "EMPBONUSEDISONWORKER" : "EMPBONUS2";

                    }
                    else
                    {
                        Calltype = (this.rbtlBonSheet.SelectedIndex == 0) ? "EMPBONUSEDISONEXCUTIVE" : "EMPBONUS2";
                    }
                    break;

                case "5305"://FB
                case "5306"://FB
                    if (CompanyName == "9403" || CompanyName == "9408")
                    {
                        Calltype = (this.rbtlBonSheet.SelectedIndex == 1) ? "EMPBONUSFBWORKER" : "EMPBONUS2";

                    }
                    else
                    {
                        Calltype = (this.rbtlBonSheet.SelectedIndex == 1) ? "EMPBONUSFBEXCUTIVE" : "EMPBONUS2";

                    }
                    break;

            }

            string afterdays = Convert.ToDouble("0" + this.txtafterdays.Text.Trim()).ToString();
            string comgross = this.Companygross();

            if (mon > 3 || (this.lblComBonLock.Text == "True"))
            {
                ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "BONSALARY", monthid, projectcode, section, CompanyName + "%", payType, line, "", "", "");
            }
            else
            {
                ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", Calltype, date, projectcode, section, afterdays, CompanyName + "%", comgross, division, payType, line);
            }

            if (ds3 == null)
            {
                this.gvBonus.DataSource = null;
                this.gvBonus.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblpay"] = dt;
            this.LoadGrid();

        }
      
        private void ShowPaySlip()
        {
            Session.Remove("tblpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string CompanyName = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string projectcode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%%" : this.ddlSection.SelectedValue.ToString());
            string Empid = "%";          
            string ChckResign = (this.ChckResign.Checked == true) ? "RESIGN" : "ALL";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_PAYSLIP", "RPTPAYSLIP",null,null,null, frmdate, todate, CompanyName, projectcode, division, Empid, section, ChckResign, line, joblocation, userid);
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvpayslip.DataSource = null;
                this.gvpayslip.DataBind();
                return;
            }

            DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;
            this.TakaInWord();
            this.LoadGrid();

        }

        private void ShowSignature()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string projectcode = this.ddlDept.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SIGNATURESHEET", frmdate, todate, projectcode, section, "", "", "", "", "");
            if (ds3 == null)
            {

                return;
            }
            Session["tblpay"] = ds3.Tables[0];
            ds3.Dispose();


        }

        private void EmpCashPay()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string CompanyName = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string projectcode = this.ddlDept.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString();

            string CallType = this.CashCallType();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", CallType, frmdate, todate, projectcode, section, CompanyName, "", "", "", "");
            if (ds3 == null)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                //((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");

                this.gvpayroll.DataSource = null;
                this.gvpayroll.DataBind();
                return;

            }

            //DataView dv = ds3.Tables[0].DefaultView;
            //dv.RowFilter = "othded>0";
            // DataTable dt = HiddenSameData(dv.ToTable());
            DataTable dt = HiddenSameData(ds3.Tables[0]);
            Session["tblpay"] = dt;
            this.LoadGrid();


        }

        private void ShowEmpOvertimeSalary()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();

            string CompanyName = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 7)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_OVRTIMESALARY", "RPTOVRTIMESALARY", frmdate, todate, CompanyName, department, section, "", "", "", "");

            if (ds3 == null)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                //((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");

                this.gvOvertime.DataSource = null;
                this.gvOvertime.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;

            this.LoadGrid();
        }


        private void ShowEmpOvertimeSalary02()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string EmpType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string Division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            //string monthid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMM").ToString();
            string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
            mon = this.Datediffday1(Convert.ToDateTime(curdate), Convert.ToDateTime(dt1));

            string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";

            DataSet ds3;
            string empwkStation = this.ddlWstation.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SALOVLOCK", monthid, empwkStation, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                return;
            }

            Session["UserLog"] = ds1.Tables[0];

            this.lblComSalovLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();

            if ((this.lblComSalovLock.Text == "True"))
            {
                ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "OVERTIMESALSHEET", monthid, Department, section, EmpType, payType, "", "", "", "");

                if (ds3.Tables[0].Rows.Count == 0)
                    ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL03", "OVERTIMESALARY", frmdate, todate, EmpType, Division, Department, section, payType, "", "");
            }
            else
            {

                ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL03", "OVERTIMESALARY", frmdate, todate, EmpType, Division, Department, section, payType, "", "");
                //            ds3 = HRData.GetTransInfo(comcod, ProName, CallType, frmdate, todate, projectcode, section, CompanyName, saltyp, division, payType, "");

            }
            if (ds3 == null)
            {
                this.gvpayroll.DataSource = null;
                this.gvpayroll.DataBind();
                return;

            }

            DataTable dt = HiddenSameData(ds3.Tables[0]);
            Session["tblpay"] = dt;
            ViewState["tblOtDetails"] = ds3.Tables[1];

            this.LoadGrid();

        }

        private string CashCallType()
        {
            string compcod = this.GetCompCode();
            string CallType = "";
            switch (compcod)
            {
                case "4101":

                    break;

                case "4301":
                    CallType = "RPTCASHSALARY";
                    break;
            }

            return CallType;


        }

        private void TakaInWord()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblpay"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double netpay = Convert.ToDouble(dt.Rows[i]["netpay"]);
                    dt.Rows[i]["aminword"] = ASTUtility.Trans(netpay, 2);

                }
                Session["tblpay"] = dt;
            }

            catch (Exception ex)
            {

            }


        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string refno = dt1.Rows[0]["refno"].ToString().Trim();
            string section = dt1.Rows[0]["section"].ToString().Trim();

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "Salary":
                case "SalaryHold":
                case "SalaryOT":
                case "Bonus":
                case "Payslip":
                case "Signature":
                case "CashPay":

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["refno"].ToString() == refno && dt1.Rows[j]["section"].ToString() == section)
                        {
                            refno = dt1.Rows[j]["refno"].ToString();
                            section = dt1.Rows[j]["section"].ToString();
                            dt1.Rows[j]["refdesc"] = "";
                            dt1.Rows[j]["sectionname"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["refno"].ToString() == refno)
                            {
                                dt1.Rows[j]["refdesc"] = "";
                            }

                            if (dt1.Rows[j]["section"].ToString() == section)
                            {
                                dt1.Rows[j]["sectionname"] = "";

                            }
                            refno = dt1.Rows[j]["refno"].ToString();
                            section = dt1.Rows[j]["section"].ToString();
                        }
                    }
                    break;

                case "SalaryReg":


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["refno"].ToString() == refno && dt1.Rows[j]["section"].ToString() == section)
                        {
                            refno = dt1.Rows[j]["refno"].ToString();
                            section = dt1.Rows[j]["section"].ToString();
                            dt1.Rows[j]["refdesc"] = "";
                            dt1.Rows[j]["sectionname"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["refno"].ToString() == refno)
                            {
                                dt1.Rows[j]["refdesc"] = "";
                            }

                            if (dt1.Rows[j]["section"].ToString() == section)
                            {
                                dt1.Rows[j]["sectionname"] = "";

                            }
                            refno = dt1.Rows[j]["refno"].ToString();
                            section = dt1.Rows[j]["section"].ToString();
                        }
                    }

                    break;


                case "OvertimeSalary":
                    string company = dt1.Rows[0]["company"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company)
                            dt1.Rows[j]["companyname"] = "";
                        company = dt1.Rows[j]["company"].ToString();
                    }
                    break;



            }


            return dt1;

        }



        private void LoadGrid()
        {
            DataTable dt= (DataTable)Session["tblpay"];
            string EmpType = this.ddlWstation.SelectedValue.ToString();
            try
            {                
                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {
                    case "Salary":
                    case "SalaryOT":
                        this.gvpayroll.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            this.gvpayroll.DataSource = null;
                            this.gvpayroll.DataBind();
                            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                            return;
                        }
                        if (ddlWstation.SelectedValue.ToString().Substring(0, 4) == "9403")
                        {
                            dt.DefaultView.Sort = "idcard";
                        }

                        this.gvpayroll.DataSource = dt;
                        this.gvpayroll.DataBind();

                        //NAHID COMMENCTS OFF 
                        this.gvpayroll.Columns[1].Visible = (this.ddlDept.SelectedValue == "000000000000") ? true : false;
                        this.gvpayroll.Columns[3].Visible = false;

                        ((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked = (this.lblComSalLock.Text == "True") ? true : false;

                        for (int i = 0; i < gvpayroll.Rows.Count; i++)
                        {
                            string presal = ((Label)gvpayroll.Rows[i].FindControl("lblpresal")).Text.Trim();

                            if (presal == "New")
                            {
                                this.gvpayroll.Rows[i].BackColor = Color.SkyBlue;
                                this.gvpayroll.Rows[i].ForeColor = Color.Black;
                            }

                        }

                        /// END NAHID COMMENTS 
                        if (Request.QueryString["Entry"].ToString() == "Payroll")
                        {
                            //this.gvpayroll.Columns[7].Visible = false;
                            ((LinkButton)this.gvpayroll.FooterRow.FindControl("lnkDeduction")).Visible = false;
                            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked) ? false : true;
                            ((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Enabled = false;
                            ((TextBox)this.gvpayroll.FooterRow.FindControl("txtFVeID")).Enabled = false;
                        }
                        else
                        {
                            ((LinkButton)this.gvpayroll.FooterRow.FindControl("lnkDeduction")).Visible = (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked) ? false : true;
                            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                        }


                        this.FooterCalculation();
                        if (dt.Rows.Count == 0)
                            return;
                        DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                        ((HyperLink)this.gvpayroll.HeaderRow.FindControl("hlbtnCBdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                        Session["Report1"] = gvpayroll;
                        if (dr1.Length > 0)
                            ((HyperLink)this.gvpayroll.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        
                        ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = EmpType == "000000000000" ? false : true;
                        break;

                    case "SalaryHold":
                        this.gvpayroll.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            this.gvpayroll.DataSource = null;
                            this.gvpayroll.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                            return;
                        }
                        if (ddlWstation.SelectedValue.ToString().Substring(0, 4) == "9403")
                        {
                            dt.DefaultView.Sort = "idcard";
                        }

                        this.gvpayroll.DataSource = dt;
                        this.gvpayroll.DataBind();

                        //NAHID COMMENCTS OFF 
                        this.gvpayroll.Columns[1].Visible = (this.ddlDept.SelectedValue == "000000000000") ? true : false;
                        this.gvpayroll.Columns[3].Visible = false;

                        ((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked = (this.lblComSalLock.Text == "True") ? true : false;
                        //((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked = (this.lblComSalLock.Text == "True") ? true : false;

                        for (int i = 0; i < gvpayroll.Rows.Count; i++)
                        {
                            string presal = ((Label)gvpayroll.Rows[i].FindControl("lblpresal")).Text.Trim();

                            if (presal == "New")
                            {
                                this.gvpayroll.Rows[i].BackColor = Color.SkyBlue;
                                this.gvpayroll.Rows[i].ForeColor = Color.Black;
                            }

                        }

                        /// END NAHID COMMENTS 
                        if (Request.QueryString["Entry"].ToString() == "Payroll")
                        {
                            this.gvpayroll.Columns[7].Visible = false;
                            ((LinkButton)this.gvpayroll.FooterRow.FindControl("lnkDeduction")).Visible = false;
                            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked) ? false : true;
                            ((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Visible = false;
                            ((TextBox)this.gvpayroll.FooterRow.FindControl("txtFVeID")).Visible = false;
                        }
                        else
                        {
                            ((LinkButton)this.gvpayroll.FooterRow.FindControl("lnkDeduction")).Visible = false;
                            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked) ? false : true;
                        }


                        this.FooterCalculation();
                        if (dt.Rows.Count == 0)
                            return;
                        DataRow[] dr2 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                        ((HyperLink)this.gvpayroll.HeaderRow.FindControl("hlbtnCBdataExel")).Enabled = (Convert.ToBoolean(dr2[0]["printable"]));
                        Session["Report1"] = gvpayroll;
                        if (dr2.Length > 0)
                            ((HyperLink)this.gvpayroll.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = EmpType == "000000000000" ? false : true;
                        break;

                    case "SalaryReg":
                        this.gvpayroll.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            this.gvpayroll.DataSource = null;
                            this.gvpayroll.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                            return;
                        }
                        this.gvpayroll.DataSource = dt;
                        this.gvpayroll.DataBind();

                        //NAHID COMMENCTS OFF 
                        this.gvpayroll.Columns[1].Visible = (this.ddlDept.SelectedValue == "000000000000") ? true : false;
                        this.gvpayroll.Columns[3].Visible = false;

                        ((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked = (this.lblComSalLock.Text == "True") ? true : false;

                        /// END NAHID COMMENTS 

                        if (Request.QueryString["Entry"].ToString() == "Payroll")
                        {
                            ((LinkButton)this.gvpayroll.FooterRow.FindControl("lnkDeduction")).Visible = false;
                            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked) ? false : true;
                            ((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Enabled = false;
                            ((TextBox)this.gvpayroll.FooterRow.FindControl("txtFVeID")).Enabled = false;
                        }
                        else
                        {
                            ((LinkButton)this.gvpayroll.FooterRow.FindControl("lnkDeduction")).Visible = (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked) ? false : true;
                            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                        }                       
                        this.FooterCalculation();
                        Session["Report1"] = gvpayroll;
                        if (dt.Rows.Count > 0)
                            ((HyperLink)this.gvpayroll.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = EmpType == "000000000000" ? false : true;
                        break;

                    case "Bonus":
                        this.gvBonus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            this.gvBonus.DataSource = null;
                            this.gvBonus.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                            return;
                        }

                        this.gvBonus.DataSource = dt;
                        this.gvBonus.DataBind();
                        ((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Checked = (this.lblComBonLock.Text == "True") ? true : false;
                        if (Request.QueryString["Entry"].ToString() == "Payroll")
                        {
                            ((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Visible = (((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Checked) ? false : true;
                            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = (((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Checked) ? false : true;
                            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = (((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Checked) ? false : true;
                            ((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Enabled = false;
                        }
                       if(dt.Rows.Count>0)
                        {
                            Session["Report1"] = gvBonus;
                            ((HyperLink)this.gvBonus.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                            this.FooterCalculation();
                        }
                        break;



                    case "CashPay":
                        this.gvcashpay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            this.gvcashpay.DataSource = null;
                            this.gvcashpay.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                            return;
                        }
                        this.gvcashpay.DataSource = dt;
                        this.gvcashpay.DataBind();
                        this.FooterCalculation();
                        break;

                    case "OvertimeSalary":
                        this.gvOvertime.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            this.gvOvertime.DataSource = null;
                            this.gvOvertime.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                            return;
                        }
                        this.gvOvertime.DataSource = dt;
                        this.gvOvertime.DataBind();
                        this.FooterCalculation();
                        break;

                    case "OvertimeSal":
                        this.gvovsal02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            this.gvovsal02.DataSource = null;
                            this.gvovsal02.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                            return;
                        }
                        this.gvovsal02.DataSource = dt;
                        this.gvovsal02.DataBind();

                        ((CheckBox)this.gvovsal02.FooterRow.FindControl("chkSalaryovLock")).Checked = (this.lblComSalovLock.Text == "True") ? true : false;
                        if (Request.QueryString["Entry"].ToString() == "Payroll")
                        {
                            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = (((CheckBox)this.gvovsal02.FooterRow.FindControl("chkSalaryovLock")).Checked) ? false : true;
                            ((CheckBox)this.gvovsal02.FooterRow.FindControl("chkSalaryovLock")).Enabled = false;
                        }
                        this.FooterCalculation();
                        break;

                    case "Payslip":
                        this.gvpayslip.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvpayslip.DataSource = dt;
                        this.gvpayslip.DataBind();
                        this.FooterCalculation();


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
            DataTable dt = (DataTable)Session["tblpay"];

            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "Salary":
                case "SalaryHold":
                case "SalaryOT":
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFbSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFhrent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ? 0.00 : dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFCon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ? 0.00 : dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFmallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ? 0.00 : dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFodallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fallow)", "")) ? 0.00 : dt.Compute("sum(fallow)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFarier")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arsal)", "")) ? 0.00 : dt.Compute("sum(arsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFoth")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oth)", "")) ? 0.00 : dt.Compute("sum(oth)", ""))).ToString("#,##0;(#,##00); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFtallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tallow)", "")) ? 0.00 : dt.Compute("sum(tallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFgssal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFgspay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gspay)", "")) ? 0.00 : dt.Compute("sum(gspay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFpfund")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFitax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adv)", "")) ? 0.00 : dt.Compute("sum(adv)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFothded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFtded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tdeduc)", "")) ? 0.00 : dt.Compute("sum(tdeduc)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFbonusamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonusamt)", "")) ? 0.00 : dt.Compute("sum(bonusamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFgspay3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gspay3)", "")) ? 0.00 : dt.Compute("sum(gspay3)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFnetSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFcashamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFabsded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(absded)", "")) ? 0.00 : dt.Compute("sum(absded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFbankamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFSpcded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(spcded)", "")) ? 0.00 : dt.Compute("sum(spcded)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFgssal1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal1)", "")) ? 0.00 : dt.Compute("sum(gssal1)", ""))).ToString("#,##0;(#,##0); ");

                    break;
                case "SalaryReg":
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFbSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFhrent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ? 0.00 : dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFCon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ? 0.00 : dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFmallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ? 0.00 : dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFarier")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arsal)", "")) ? 0.00 : dt.Compute("sum(arsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFoth")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oth)", "")) ? 0.00 : dt.Compute("sum(oth)", ""))).ToString("#,##0;(#,##00); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFtallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tallow)", "")) ? 0.00 : dt.Compute("sum(tallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFgssal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFgspay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gspay)", "")) ? 0.00 : dt.Compute("sum(gspay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFpfund")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFitax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adv)", "")) ? 0.00 : dt.Compute("sum(adv)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFothded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFtded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tdeduc)", "")) ? 0.00 : dt.Compute("sum(tdeduc)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFnetSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFcashamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFabsded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(absded)", "")) ? 0.00 : dt.Compute("sum(absded)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFgssal1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal1)", "")) ? 0.00 : dt.Compute("sum(gssal1)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "Bonus":
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFbSalb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFgssalb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFBonusAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "CashPay":
                    ((Label)this.gvcashpay.FooterRow.FindControl("lgvFToCahamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "OvertimeSalary":
                    ((Label)this.gvOvertime.FooterRow.FindControl("lgvFoallows")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "OvertimeSal":
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFotofdayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(otoffamount)", "")) ? 0.00 : dt.Compute("sum(otoffamount)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFosamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(osamount)", "")) ? 0.00 : dt.Compute("sum(osamount)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFnetamtos")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ? 0.00 : dt.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFBasic")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFGross")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal1)", "")) ? 0.00 : dt.Compute("sum(gssal1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFnetamtoeotcas")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvovsal02.FooterRow.FindControl("lgvFnetamtosbnk")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;



                case "Payslip":
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFbSalpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFhrentpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ? 0.00 : dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFConpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ? 0.00 : dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFmallowpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ? 0.00 : dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFodallowpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fallow)", "")) ? 0.00 : dt.Compute("sum(fallow)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFarierpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arsal)", "")) ? 0.00 : dt.Compute("sum(arsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFothpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oth)", "")) ? 0.00 : dt.Compute("sum(oth)", ""))).ToString("#,##0;(#,##00); ");

                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFgssalpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFgspaypslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gspay)", "")) ? 0.00 : dt.Compute("sum(gspay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFpfundpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFitaxpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFothdedpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFtdedpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tdeduc)", "")) ? 0.00 : dt.Compute("sum(tdeduc)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFbonusamtpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonusamt)", "")) ? 0.00 : dt.Compute("sum(bonusamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFnetSalpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFcashamtpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFabsdedpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(absded)", "")) ? 0.00 : dt.Compute("sum(absded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFbankamtpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayslip.FooterRow.FindControl("lgvFSpcdedpslip")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(spcded)", "")) ? 0.00 : dt.Compute("sum(spcded)", ""))).ToString("#,##0;(#,##0); ");



                    break;

            }



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Salary":
                case "SalaryHold":
                    this.PrintSal();
                    // this.PrintRDLCSalary();
                    break;
                case "SalaryReg":
                    this.PrintSal();
                    //this.PrintRDLCSalary();
                    break;

                case "SalaryOT":
                    this.PrintSalaryOT();
                    break;

                case "Bonus":
                    this.PrintEmpBonus();
                    break;
                case "Payslip":
                    this.PrintPaySlip();
                    break;

                case "Signature":
                    this.PrintSignature();
                    break;

                case "CashPay":
                    this.PrintCashPay();
                    break;

                case "OvertimeSalary":
                    this.PrintOvertimeSalary();
                    break;

                case "OvertimeSal":
                    this.PrintOvertimeSalary02();
                    break;

            }

        }

        private void PrintSalaryOT()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " +
                                 username + " ,Time: " + printdate;

            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string empType = this.ddlWstation.SelectedItem.Text.ToString();
            string empType1 = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string department = this.ddlDept.SelectedItem.Text.ToString();
            string section = this.ddlSection.SelectedItem.Text.ToString();
            string line = this.ddlempline.SelectedItem.Text.ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> list1 = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>)Session["hrcompnameadd"];

            var lst2 = list1.FindAll(l => l.actcode.Substring(0, 4) == empType1);
            string compName = lst2[0].actcode == "000000000000" ? comname : lst2[0].hrcomname.ToString();
            string compAdd = lst2[0].actcode == "000000000000" ? list1[1].hrcomadd.ToString() : lst2[0].hrcomadd.ToString();

            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.OverTimeSal>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSalaryOTFB", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", compName));
            Rpt1.SetParameters(new ReportParameter("comadd", compAdd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary OT Sheet"));
            Rpt1.SetParameters(new ReportParameter("empType", empType));
            Rpt1.SetParameters(new ReportParameter("department", department));
            Rpt1.SetParameters(new ReportParameter("section", section));
            Rpt1.SetParameters(new ReportParameter("line", line));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("todate", "For the Month of " + todate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintSal()
        {

            if (this.rbtSalSheet.SelectedIndex == 0)
            {
                //Edison Footwear
                this.PrintSalaryEdisonFootwear();

            }
            else if (this.rbtSalSheet.SelectedIndex == 1)
            {
                //FB Footwear
                this.PrintSalaryFBFootwear();
            }


        }

        private void PrintSalaryFBFootwear()
        {
            string qtype = this.Request.QueryString["Type"].ToString();
            string Depart = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            switch (Depart)
            {
                //case "9401": //EXECUTIVE EMPLOYEES(FB)
                //case "9402": //EXECUTIVE EMPLOYEES(Footbed)
                //case "9411": //Supporting Staff- FB(HO)
                //case "9412": //Supporting Staff- Foodbed(HO)
                //    this.PrintExecutiveSalaryFB();
                //    break;


                //case "9413": //FACTORY STAFF (FB-Non OT Based)
                //case "9415": //FACTORY STAFF (Footbed-Non OT Based)
                //this.PrintStaffSalaryNONOTBased();
                //break;

                case "9403": //FACTORY WORKER(FB)
                case "9404": //FACTORY WORKER
                case "9405": //FACTORY WORKER
                case "9406": //FACTORY WORKER
                case "9407": //FACTORY WORKER
                case "9408": //FACTORY WORKER(Footbed)
                case "9409": //FACTORY WORKER
                case "9410": //FACTORY WORKER
                case "9413": //FACTORY STAFF (FB-Non OT Based)
                case "9414": //FACTORY STAFF (FB-OT Based)   
                case "9415": //FACTORY STAFF (Footbed-Non OT Based)
                case "9416": //FACTORY STAFF (Foodbed-OT Based)
                    this.PrintWorkerSalary();
                    break;

                default:
                    this.PrintExecutiveSalaryFB();
                    break;
            }
        }

        private void PrintSalaryEdisonFootwear()
        {
            string qtype = this.Request.QueryString["Type"].ToString();
            //string Depart = this.ddlProjectName.SelectedValue.ToString();
            string Depart = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            switch (Depart)
            {
                case "9401":
                    this.PrintExecutiveSalary();
                    // this.PrintRDLCSalary();
                    break;
                case "9402":
                    this.PrintExecutiveSalary();
                    // this.PrintFactorySalary(); this code commented by safi instructed by Mahadi (Edision)
                    break;
                case "9403":

                    if (qtype == "SalaryReg")
                    {
                        this.PrintResignWorkerSalary();
                    }
                    else
                    {
                        this.PrintWorkerSalary();

                    }
                    //this.PrintRDLCSalary();
                    break;
                case "9404":
                    this.PrintExecutiveSalary();
                    // this.PrintRDLCSalary();
                    break;
                case "9409":
                    this.PrintExecutiveSalary();
                    // this.PrintRDLCSalary();
                    break;
            }
        }
        private void PrintExecutiveSalaryFB()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string month = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string section = "";
            string Depart = this.ddlWstation.SelectedValue.ToString().Substring(0, 4).Trim();
            string jobLocation = this.ddlJob.SelectedItem.Text;
            string dep = "";
            string rptTitle = (Depart.ToString() == "9401" ? "Executive Salary Sheet(FB) For The Month of-" : Depart.ToString() == "9402" ? "Executive Salary Sheet(Footbed) For The Month of-" :
                    Depart.ToString() == "9411" ? "Supporting Staff-FB(HO) Salary Sheet For The Month of-" : "Supporting Staff-Foodbed(HO) Salary Sheet For The Month of-");

            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptExecutiveSalaryFB", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("comLogo", compLogo));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Sheet"));
            rpt1.SetParameters(new ReportParameter("TkInWord", ASTUtility.Trans(Math.Round(netpay), 2)));
            rpt1.SetParameters(new ReportParameter("txtMonth", rptTitle + month));
            rpt1.SetParameters(new ReportParameter("jobLocation", "Job Location: " + jobLocation));
            rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintExecutiveSalary()
        {

            // Session.Remove("tblpay");
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string month = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string section = "";

            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>();
            LocalReport rpt1 = new LocalReport();
            string Depart = this.ddlWstation.SelectedValue.ToString().Substring(0, 4).Trim();
            string dep = "";
            string rptittel = (Depart.ToString() == "9401" ? "Executive Salary Sheet For The Month of-"
                    : Depart.ToString() == "9409" ? "Monthly Allowance Information of Intern For the Month of- "
                    : (Depart.ToString() == "9402") ? "Factory Staff Salary Sheet For The Month of-" : "Messenger Salary Sheet For The Month of-");

            switch (comcod)
            {
                case "5305":
                case "5306":
                    rptittel = "মাসিক মজুরি শীট";
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptWorkerSalFB", lst, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("comnam", "এফ বি ফুটওয়্যার লি."));
                    rpt1.SetParameters(new ReportParameter("comadd", "উলুসারা,কালিয়াকৈর,গাজীপুর"));
                    rpt1.SetParameters(new ReportParameter("section", section));
                    rpt1.SetParameters(new ReportParameter("rpttitle", rptittel));
                    rpt1.SetParameters(new ReportParameter("month", month));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    break;
                default:
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptExecutiveSal", lst, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("section", section));
                    rpt1.SetParameters(new ReportParameter("rpttitle", rptittel + month));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    break;
            }
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintStaffSalaryNONOTBased()
        {
            // Session.Remove("tblpay");
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comaddf = hst["comaddf"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string month = GetMonthName((Convert.ToDateTime(this.txttodate.Text).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txttodate.Text).ToString("yyyy")));

            string section = this.ddlSection.SelectedValue == "000000000000" ? "" : "Section: " + this.ddlSection.SelectedItem.ToString();
            string ddlDept = this.ddlDept.SelectedValue == "000000000000" ? "" : "Section: " + this.ddlDept.SelectedItem.ToString();
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));


            string wrkstattion = this.ddlWstation.SelectedValue.ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> list1 = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>)Session["hrcompnameadd"];
            string comnambn = list1.FindAll(l => l.actcode == wrkstattion)[0].hrcomnameb;
            string comaddbn = list1.FindAll(l => l.actcode == wrkstattion)[0].hrcomaddb;

            string paytype = "";
            string paydate = Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? System.DateTime.Now.ToString("dd-MMM-yyyy") : Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy");
            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>();
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSalaryNONOTFB", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnambn));
            rpt1.SetParameters(new ReportParameter("comadd", comaddbn));
            rpt1.SetParameters(new ReportParameter("section", section));
            rpt1.SetParameters(new ReportParameter("Dept", ddlDept));
            rpt1.SetParameters(new ReportParameter("rpttitle", "মাসিক মজুরি শীট"));
            rpt1.SetParameters(new ReportParameter("month", month));
            rpt1.SetParameters(new ReportParameter("paydate", paydate));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintWorkerSalary()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comaddf = hst["comaddf"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string comaddbn = hst["comaddbn"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string month = GetMonthName((Convert.ToDateTime(this.txttodate.Text).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txttodate.Text).ToString("yyyy")));
            string section = this.ddlSection.SelectedValue == "000000000000" ? "" : "Section: " + this.ddlSection.SelectedItem.ToString();
            string ddlDept = this.ddlDept.SelectedValue == "000000000000" ? "" : "Section: " + this.ddlDept.SelectedItem.ToString();
            string paydate = this.txtpayment.Text.Trim() == "" ? "" :
                Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? System.DateTime.Now.ToString("dd-MMM-yyyy")
                : Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy");
            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>();
            string paytype = "";
            LocalReport rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5305":
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptWorkerSalFB", lst, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("comnam", userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি." : comnambn)); //Audit user
                    rpt1.SetParameters(new ReportParameter("comadd", comaddbn));
                    rpt1.SetParameters(new ReportParameter("section", section));
                    rpt1.SetParameters(new ReportParameter("Dept", ddlDept));
                    rpt1.SetParameters(new ReportParameter("rpttitle", "মাসিক মজুরি শীট"));
                    rpt1.SetParameters(new ReportParameter("month", month));
                    rpt1.SetParameters(new ReportParameter("paydate", paydate == "" ? "" : "Print Date: " + paydate));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    break;

                case "5306":
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptWorkerSalFootbed", lst, null, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("comnam", comnambn.Replace("লি.", "লিমিটেড")));
                    rpt1.SetParameters(new ReportParameter("comadd", comaddbn));
                    rpt1.SetParameters(new ReportParameter("section", section));
                    rpt1.SetParameters(new ReportParameter("Dept", ddlDept));
                    rpt1.SetParameters(new ReportParameter("rpttitle", "মাসিক মজুরি শীট"));
                    rpt1.SetParameters(new ReportParameter("month", month));
                    rpt1.SetParameters(new ReportParameter("paydate", paydate == "" ? "" : "Print Date: " + paydate));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    break;

                default:
                    if (rbtPaymentType.SelectedIndex == 0)
                    {
                        rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptFactorySalaryCash", lst, null, null);
                        paytype = "Cash Salary";
                    }
                    else
                    {
                        rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSalarySheet", lst, null, null);
                        paytype = "Bank Salary";
                    }
                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comaddf)); 
                    rpt1.SetParameters(new ReportParameter("section", section));
                    rpt1.SetParameters(new ReportParameter("paydate", paydate == "" ? "" : "Payment Date: " + paydate));
                    rpt1.SetParameters(new ReportParameter("rpttitle", "Worker Salary Sheet For The Month of- " + month + " (" + paytype + ")"));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
                    break;
            }

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        } 

        private void PrintResignWorkerSalary()
        {
            // Session.Remove("tblpay");
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comaddf = hst["comaddf"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string month = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string section = this.ddlSection.SelectedValue == "000000000000" ? "" : "Section: " + this.ddlSection.SelectedItem.ToString();
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            string paytype = "";
            string paydate = Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? System.DateTime.Now.ToString("dd-MMM-yyyy") : Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy");

            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>();
            LocalReport rpt1 = new LocalReport();
            // string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            //if (rbtPaymentType.SelectedIndex == 0)
            //{
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptFactResignSalCash", lst, null, null);
            paytype = "Cash Salary";
            //}
            //else
            //{
            //    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptResignSalSheet", lst, null, null);
            //    paytype = "Bank Salary";
            //}
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comaddf)); //"JL NO:5, Mirzapur, Gazipur Sadar"));
            rpt1.SetParameters(new ReportParameter("section", section));
            //rpt1.SetParameters(new ReportParameter("paytype", paytype));
            rpt1.SetParameters(new ReportParameter("paydate", "Payment Date:" + paydate));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Worker Resign Salary Sheet For The Month of- " + month + " (" + paytype + ")"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintFactorySalary()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comaddf = hst["comaddf"].ToString();

            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string month = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string section = "";
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptStaffSalary", lst, null, null);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comaddf)); //"JL NO:5, Mirzapur, Gazipur Sadar"));
            rpt1.SetParameters(new ReportParameter("section", section));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Factory Staff Salary Sheet For The Month of- " + month));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private void PrintEmpBonus()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "5301"://Edison
                    this.EmpBonusInfo();
                    break;

                case "5305"://FB
                case "5306"://FB
                    this.RptEmpBonusFB();
                    break;

                default:
                    this.EmpBonusInfo();
                    break;

            }
        }

        private void EmpBonusInfo()
        {

            string qtype = this.Request.QueryString["Type"].ToString();
            //string Depart = this.ddlProjectName.SelectedValue.ToString();
            string Depart = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            switch (Depart)
            {
                case "9401":
                case "9404":
                    this.PrintExecutiveBonus();
                    // this.PrintRDLCSalary();
                    break;
                case "9402":
                    this.PrintFactoryBonus();
                    break;

                case "9403":

                    this.PrintWorkerbonous();
                    break;

                case "9409":
                    this.PrintInternBonus();
                    break;

            }

        }

        private void RptEmpBonusFB()
        {
            string empType = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            switch (empType)
            {
                case "9403"://Worker Bonus FB
                case "9408"://Worker Bonus Footbed
                    this.PrintWorkerBonusFB();
                    break;

                default:
                    this.PrintWorkerBonusFB();
                    break;

            }

        }
        private void PrintWorkerbonous()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comaddf = hst["comaddf"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMMM-yyyy");
            string todate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string eidname = ddlBonusType.SelectedValue == "08001" ? "EID UL-FITR" : "EID UL-ADHA";
            string section = this.ddlSection.SelectedValue == "000000000000" ? "" : "Section: " + this.ddlSection.SelectedItem.ToString();
            string paydate = Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? System.DateTime.Now.ToString("dd-MMM-yyyy") : Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy");
            string paytype = "";
            DataTable dt = (DataTable)Session["tblpay"];
            string eidyear = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyy");

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpBonusheet>();


            LocalReport rpt1 = new LocalReport();
            // string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            if (rbtPaymentType.SelectedIndex == 0)
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusCash", lst, null, null);
                paytype = "Cash Bonus";
            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusInfo", lst, null, null);
                paytype = "Bank Bonus";

            }
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comaddf)); //"JL NO:5, Mirzapur, Gazipur Sadar"));
            rpt1.SetParameters(new ReportParameter("section", section));
            //rpt1.SetParameters(new ReportParameter("paytype", paytype));
            rpt1.SetParameters(new ReportParameter("paydate", "Payment Date: " + paydate));
            rpt1.SetParameters(new ReportParameter("Date", "Cut off Date: " + frmdate));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Worker Bonus Sheet For the " + eidname + " - " + eidyear + " (" + paytype + ")"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        } // end
        private void PrintWorkerBonusFB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string comaddbn = hst["comaddbn"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string txtEidName = ddlBonusType.SelectedValue == "08001" ?  "ঈদ উল-ফিতর" : "ঈদ উল-আযহা";
            string txtDate = GetMonthName((Convert.ToDateTime(this.txtpayment.Text).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txtpayment.Text).ToString("yyyy")));
            string paytype = "";
            string paymntdate = GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txtpayment.Text).ToString("dd"))) + "-" + GetMonthName((Convert.ToDateTime(this.txtpayment.Text).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txtpayment.Text).ToString("yyyy")));

            DataTable dt = (DataTable)Session["tblpay"];
            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptFestivalBonus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptWorkerFestBonusFB", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnambn));
            Rpt1.SetParameters(new ReportParameter("compAdd", comaddbn));
            Rpt1.SetParameters(new ReportParameter("rptTitle", txtEidName + " বোনাস"));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));
            Rpt1.SetParameters(new ReportParameter("sign2", comcod == "5305" ? "HR" : "Deputy  Manager-HR"));
            Rpt1.SetParameters(new ReportParameter("sign3", comcod == "5305" ? "Accounts" : "Deputy Manager-Acc"));
            Rpt1.SetParameters(new ReportParameter("sign4", comcod == "5305" ? "Recommended By" : "Manager Admin, HR & Compliance"));
            Rpt1.SetParameters(new ReportParameter("paymntdate", paymntdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintExecutiveBonus()
        {
            //Nayan d
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comaddf = hst["comaddf"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMMM-yyyy");
            string todate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string eidname = ddlBonusType.SelectedValue == "08001" ? "EID UL-FITR" : "EID UL-ADHA";
            string section = this.ddlSection.SelectedValue == "000000000000" ? "" : "Section: " + this.ddlSection.SelectedItem.ToString();
            string paydate = Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? System.DateTime.Now.ToString("dd-MMM-yyyy") : Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy");
            string paytype = "";
            DataTable dt = (DataTable)Session["tblpay"];
            string eidyear = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyy");

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpBonusheet>();


            LocalReport rpt1 = new LocalReport();
            // string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            if (rbtPaymentType.SelectedIndex == 0)
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusCash", lst, null, null);
                paytype = " ( Cash Bonus ) ";
            }
            else if (rbtPaymentType.SelectedIndex == 1)
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusInfo", lst, null, null);
                paytype = " ( Bank Bonus ) ";

            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusInfo", lst, null, null);
                paytype = "";
            }
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comaddf)); //"JL NO:5, Mirzapur, Gazipur Sadar"));
            rpt1.SetParameters(new ReportParameter("section", section));
            //rpt1.SetParameters(new ReportParameter("paytype", paytype));
            rpt1.SetParameters(new ReportParameter("paydate", "Payment Date: " + paydate));
            rpt1.SetParameters(new ReportParameter("Date", "Cut off Date: " + frmdate));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Executive Bonus Sheet For the " + eidname + " - " + eidyear + paytype));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintFactoryBonus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comaddf = hst["comaddf"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMMM-yyyy");
            string todate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string eidname = ddlBonusType.SelectedValue == "08001" ? "EID UL-FITR" : "EID UL-ADHA";
            string section = this.ddlSection.SelectedValue == "000000000000" ? "" : "Section: " + this.ddlSection.SelectedItem.ToString();
            string paydate = Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? System.DateTime.Now.ToString("dd-MMM-yyyy") : Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy");
            string paytype = "";
            string eidyear = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyy");
            DataTable dt = (DataTable)Session["tblpay"];

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpBonusheet>();


            LocalReport rpt1 = new LocalReport();
            // string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            if (rbtPaymentType.SelectedIndex == 0)
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusInfo", lst, null, null);
                paytype = " (Cash Bonus ) ";
            }
            else if (rbtPaymentType.SelectedIndex == 1)
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusInfo", lst, null, null);
                paytype = " ( Bank Bonus )";

            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusInfo", lst, null, null);
                paytype = "";

            }
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comaddf)); //"JL NO:5, Mirzapur, Gazipur Sadar"));
            rpt1.SetParameters(new ReportParameter("section", section));
            //rpt1.SetParameters(new ReportParameter("paytype", paytype));
            rpt1.SetParameters(new ReportParameter("paydate", "Payment Date: " + paydate));
            rpt1.SetParameters(new ReportParameter("Date", "Cut off Date: " + frmdate));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Factory Staff Bonus Sheet For the " + eidname + " - " + eidyear + paytype));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintInternBonus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comaddf = hst["comaddf"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMMM-yyyy");
            string todate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string eidname = ddlBonusType.SelectedValue == "08001" ? "EID UL-FITR" : "EID UL-ADHA";
            string section = this.ddlSection.SelectedValue == "000000000000" ? "" : "Section: " + this.ddlSection.SelectedItem.ToString();
            string paydate = Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? System.DateTime.Now.ToString("dd-MMM-yyyy") : Convert.ToDateTime(this.txtpayment.Text.Trim()).ToString("dd-MMM-yyyy");
            string paytype = "";
            DataTable dt = (DataTable)Session["tblpay"];
            string eidyear = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyy");

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpBonusheet>();


            LocalReport rpt1 = new LocalReport();
            // string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            if (rbtPaymentType.SelectedIndex == 0)
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusInfo", lst, null, null);
                paytype = " ( Cash Bonus ) ";
            }
            else if (rbtPaymentType.SelectedIndex == 1)
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusInfo", lst, null, null);
                paytype = " ( Bank Bonus ) ";

            }

            else
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusInfo", lst, null, null);
                paytype = "";

            }

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comaddf)); //"JL NO:5, Mirzapur, Gazipur Sadar"));
            rpt1.SetParameters(new ReportParameter("section", section));
            //rpt1.SetParameters(new ReportParameter("paytype", paytype));
            rpt1.SetParameters(new ReportParameter("paydate", "Payment Date: " + paydate));
            rpt1.SetParameters(new ReportParameter("Date", "Cut off Date: " + frmdate));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Intern Bonus Sheet For the " + eidname + " - " + eidyear + paytype));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintPaySlip()
        {
            string comcod = GetCompCode();
            switch (comcod)
            {
                case "5305": //FB Footwear.
                case "5306": //Footbed Footwear.
                    this.PrintPaySlipFB();
                    break;

                default: //Pay Slip General.
                    this.PrintPaySlipGen();
                    break;
            }
        }
        private void PrintPaySlipFB()
        {
            string empType = this.ddlWstation.SelectedValue.Substring(0, 4);

            switch (empType)
            {
               
                case "9403": //FACTORY WORKER(FB)
                case "9404": //FACTORY WORKER
                case "9405": //FACTORY WORKER
                case "9406": //FACTORY WORKER
                case "9407": //FACTORY WORKER
                case "9408": //FACTORY WORKER(Footbed)
                case "9409": //FACTORY WORKER
                case "9410": //FACTORY WORKER
                case "9413": //FACTORY STAFF (FB-Non OT Based)
                case "9414": //FACTORY STAFF (FB-OT Based)   
                case "9415": //FACTORY STAFF (Footbed-Non OT Based)
                case "9416": //FACTORY STAFF (Foodbed-OT Based)
                    this.RptPaySlipWorkerFB();
                    break;

                default:
                    this.printExecPaySlip();
                    break;
            }



          
        }
        private void PrintPaySlipGen()
        {
            string print = this.ddlWstation.SelectedValue.Substring(0, 4);
            switch (print)
            {
                case "9401":
                    this.printExecPaySlip();
                    break;

                //case "9401":
                case "9402":
                case "9403":
                    this.RptFactWrkPaySlip();
                    break;
            }
        }
        private void RptPaySlipWorkerFB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comnambn = hst["comnambn"].ToString();
            string comaddbn = hst["comaddbn"].ToString();
            string month = GetMonthName((Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM"))) + "-" + 
                GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyy")));
            string paydate = GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txtpayment.Text).ToString("dd"))) + "-" + 
                GetMonthName((Convert.ToDateTime(this.txtpayment.Text).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txtpayment.Text).ToString("yyyy")));
            DataTable dt = (DataTable)Session["tblpay"];
            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptWorkerPaySlipFB", list, null, null);
            Rpt1.SetParameters(new ReportParameter("comnam", userid == "5305139" ? "ফুটবেড ফুটওয়্যার লি." : comnambn)); //Audit user
            Rpt1.SetParameters(new ReportParameter("comadd", comaddbn));
            Rpt1.SetParameters(new ReportParameter("Creator", ""));
            Rpt1.SetParameters(new ReportParameter("authority", ""));
            Rpt1.SetParameters(new ReportParameter("prdate", paydate));
            Rpt1.SetParameters(new ReportParameter("rpttitle", month));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void printExecPaySlip()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Inwords = "";
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM-yyyy");
            DataTable dt = (DataTable)Session["tblpay"];
            double NetAmt = Convert.ToDouble(dt.Rows[0]["exnetpay"]);
            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>();
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptPaySlipExe", lst, null, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Pay Slip for the month of " + month));
            rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(NetAmt), 2)));

            //rpt1.SetParameters (new ReportParameter ("footer", ASTUtility.Concat ("", username, printdate)));
            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        public string GetBanglaNumber(int number)
        {
            return string.Concat(number.ToString().Select(c => (char)('\u09E6' + c - '0')));
        }
        public string GetMonthName(string name)
        {
            return name.Replace("Jan", "জানুয়ারী").Replace("Feb", "ফেব্রুয়ারী").Replace("Mar", "মার্চ").
                Replace("Apr", "এপ্রিল").Replace("May", "মে").Replace("Jun", "জুন").Replace("Jul", "জুলাই").
                Replace("Aug", "আগস্ট").Replace("Sep", "সেপ্টেম্বর").Replace("Oct", "অক্টোবর").Replace("Nov", "নভেম্বর").
                Replace("Dec", "ডিসেম্বর");

        }

        private void RptFactWrkPaySlip()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Inwords = "";
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            var CompInfoBn = ASTUtility.CompInfoBn();

            string month = GetMonthName((Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyy")));
            DataTable dt = (DataTable)Session["tblpay"];


            string fline = (dt.Rows.Count > 0 ? Convert.ToString(dt.Rows[0]["fline"]).ToString() : "");

            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>();
            LocalReport rpt1 = new LocalReport();


            //  rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptPaymSlipBangla", lst, null, null);
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptWorkerPaySlip", lst, null, null);
            string paydate = GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txtpayment.Text).ToString("dd"))) + "-" + GetMonthName((Convert.ToDateTime(this.txtpayment.Text).ToString("MMM"))) + "-" + GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(this.txtpayment.Text).ToString("yyyy")));

            //rpt1.SetParameters(new ReportParameter("comnam", "এডিসন ফুটওয়্যার লিঃ "));
            //rpt1.SetParameters(new ReportParameter("comadd", "তালতলী , মির্জাপুর ,হোতাপাড়া, গাজীপুর। "));
            //var CompInfoBn = ASTUtility.CompInfoBn();
            rpt1.SetParameters(new ReportParameter("comnam", CompInfoBn.Item1));
            rpt1.SetParameters(new ReportParameter("comadd", CompInfoBn.Item2));
            rpt1.SetParameters(new ReportParameter("Creator", ""));
            rpt1.SetParameters(new ReportParameter("authority", ""));
            rpt1.SetParameters(new ReportParameter("prdate", paydate));
            rpt1.SetParameters(new ReportParameter("rpttitle", month));
            rpt1.SetParameters(new ReportParameter("fline", fline));



            //rpt1.SetParameters (new ReportParameter ("footer", ASTUtility.Concat ("", username, printdate)));
            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintSignature()
        {
            this.ShowSignature();
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");

            ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_89_Pay.RptSignatureSheet();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Month: " + frmdate;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintCashPay()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", "")));
            ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_89_Pay.RptCashPay();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Month: " + frmdate; ;
            TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2);
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);

            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintOvertimeSalary()
        {



            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string month = "";
            string section = "";
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.SalarySheet>();


            LocalReport rpt1 = new LocalReport();


            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSalarySheet", lst, null, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comadd", section));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Salary Sheet For The Month of" + month));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));





            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.OverTimeSal>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptOverTimeSal", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comname));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("todate", todate));

            Session["Report1"] = Rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvpayroll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.SaveValue();
            this.gvpayroll.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (type == "OvertimeSal")
            {
                this.ShowEmpOvertimeSalary02();

            }

            this.SaveValue();
            this.LoadGrid();
        }

        private void SaveValue()
        {

            int rowindex;
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblpay"];

            switch (comcod)
            {
                case "5301"://Edison

                    for (int i = 0; i < this.gvBonus.Rows.Count; i++)
                    {
                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double bsal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvBasicb")).Text.Trim());
                        double bonamt = bsal * 0.01 * perbonus;
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                    }

                    break;

                case "5305"://FB
                case "5306"://FB
                    for (int i = 0; i < this.gvBonus.Rows.Count; i++)
                    {

                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double gssal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvGsalb")).Text.Trim());
                        //double bonamt = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvBonusAmt")).Text.Trim());
                        //double bonamt1 = gssal * 0.01 * perbonus;
                        //double a = Math.Ceiling(5456.50);
                        //double tmonamt2 = Math.Round(bonamt1);
                        double bonamt =gssal * 0.01 * perbonus;
                        int bonamtint = (int)(gssal * 0.01 * perbonus);
                        double adpart = (bonamt - bonamtint) >= .5 ? 1.00 : 0.00;
                        double fbonamt = bonamtint + adpart;
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = fbonamt;
                    }
                    break;

                default:
                    for (int i = 0; i < this.gvBonus.Rows.Count; i++)
                    {
                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double bsal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvBasicb")).Text.Trim());
                        double bonamt = bsal * 0.01 * perbonus;
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                    }

                    break;
            }

            Session["tblpay"] = dt;

        }
        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string usrSession = hst["session"].ToString().Trim();
                if (CheckBoxMaternity.Checked)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please uncheck Maternity status.');", true);
                    return;
                }
                DataRow[] dr6 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr6[0]["entry"]))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

                    return;
                }

                string saltyp = "";
                string type = this.Request.QueryString["Type"].ToString().Trim();
                saltyp = (type == "SalaryReg" ? "R" : type == "SalaryHold" ? "H" : "");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                //string Sessionid = hst["session"].ToString();


                string Sessionid = (ViewState["Vid"] == null) ? "0" : ((string)ViewState["Vid"]).ToString();

                if (this.Request.QueryString["Entry"] == "Mgt")
                {

                    string VfId = ((TextBox)this.gvpayroll.FooterRow.FindControl("txtFVeID")).Text.Trim();

                    if (Sessionid != VfId)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Verified');", true);
                        return;
                    }

                }


                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                DataTable dt = (DataTable)Session["tblpay"];
                string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM");

                bool result = false;

                string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
                string division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
                string Department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
                string Section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "DELETESALSHEET", monthid, Company, Department, Section, saltyp, frmdate, todate, division, "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    double dedday = Convert.ToDouble(dt.Rows[i]["dedday"].ToString());

                    //if (dedday > 0)
                    //{
                    //    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "INSERTORUPEMPSALADJST", monthid, empid, dedday.ToString(), "", "", "", "", "", "", "", "", "", "", "", "");

                    //}


                    string idcard = dt.Rows[i]["idcard"].ToString();
                    // string joindate = dt.Rows[i]["joindate"].ToString();
                    string section = dt.Rows[i]["section"].ToString();
                    string desigid = dt.Rows[i]["desigid"].ToString();
                    string desig = dt.Rows[i]["desig"].ToString();
                    string empname = dt.Rows[i]["empname"].ToString();
                    string refno = dt.Rows[i]["refno"].ToString();
                    string wd = dt.Rows[i]["wd"].ToString();
                    string absday = dt.Rows[i]["absday"].ToString();
                    string wld = dt.Rows[i]["wld"].ToString();
                    string acat = dt.Rows[i]["acat"].ToString();
                    string bsal = dt.Rows[i]["bsal"].ToString();
                    string hrent = dt.Rows[i]["hrent"].ToString();
                    string cven = dt.Rows[i]["cven"].ToString();
                    string mallow = dt.Rows[i]["mallow"].ToString();
                    string arsal = dt.Rows[i]["arsal"].ToString();
                    string pickup = dt.Rows[i]["pickup"].ToString();
                    string fuel = dt.Rows[i]["fuel"].ToString();
                    string entaint = dt.Rows[i]["entaint"].ToString();
                    string mcell = dt.Rows[i]["mcell"].ToString();
                    string incent = dt.Rows[i]["incent"].ToString();
                    string oth = dt.Rows[i]["oth"].ToString();
                    string pfund = dt.Rows[i]["pfund"].ToString();
                    string itax = dt.Rows[i]["itax"].ToString();
                    string adv = dt.Rows[i]["adv"].ToString();
                    string loanins = dt.Rows[i]["loanins"].ToString();
                    string othded = dt.Rows[i]["othded"].ToString();
                    string spcded = dt.Rows[i]["spcded"].ToString();
                    string dallow = dt.Rows[i]["dallow"].ToString();
                    string oallow = dt.Rows[i]["oallow"].ToString();
                    string ohour = dt.Rows[i]["ohour"].ToString();
                    string hallow = dt.Rows[i]["hallow"].ToString();
                    string elallow = dt.Rows[i]["elallow"].ToString();
                    string mbill = dt.Rows[i]["mbill"].ToString();
                    string lwided = dt.Rows[i]["lwided"].ToString();
                    string gssal = dt.Rows[i]["gssal"].ToString();
                    string salpday = dt.Rows[i]["salpday"].ToString();
                    string gspay = dt.Rows[i]["gspay"].ToString();
                    string absded = dt.Rows[i]["absded"].ToString();
                    string tallow = dt.Rows[i]["tallow"].ToString();
                    string tdeduc = dt.Rows[i]["tdeduc"].ToString();
                    string mcadj = dt.Rows[i]["mcadj"].ToString();
                    string sdedamt = dt.Rows[i]["sdedamt"].ToString();
                    string netpay = dt.Rows[i]["netpay"].ToString();
                    string refdesc = dt.Rows[i]["refdesc"].ToString();
                    string sectionname = dt.Rows[i]["sectionname"].ToString();
                    string othallow = dt.Rows[i]["othallow"].ToString();
                    string othearn = dt.Rows[i]["othearn"].ToString();
                    string mcallow = dt.Rows[i]["mcallow"].ToString();
                    string teallow = dt.Rows[i]["teallow"].ToString();
                    string thday = dt.Rows[i]["thday"].ToString();
                    string lwpday = dt.Rows[i]["lwpday"].ToString();
                    string arded = dt.Rows[i]["arded"].ToString();
                    string cashamt = dt.Rows[i]["cashamt"].ToString();
                    string bankamt = dt.Rows[i]["bankamt"].ToString();
                    string wjd = dt.Rows[i]["wjd"].ToString();
                    string empcont = dt.Rows[i]["empcont"].ToString();
                    string elftam = dt.Rows[i]["elftam"].ToString();
                    string elfthour = dt.Rows[i]["ellfthour"].ToString();
                    string dalday = dt.Rows[i]["dalday"].ToString();
                    string ddaya10 = dt.Rows[i]["ddaya10"].ToString();
                    string dday10amt = dt.Rows[i]["dday10amt"].ToString();
                    string fallded = dt.Rows[i]["fallded"].ToString();
                    string mbillded = dt.Rows[i]["mbillded"].ToString();
                    //string asloanins = dt.Rows[i]["asloanins"].ToString();
                    string gssal1 = dt.Rows[i]["gssal1"].ToString();
                    string emptype = dt.Rows[i]["emptype"].ToString();
                    string gspay2 = dt.Rows[i]["gspay2"].ToString();
                    string bonusamt = dt.Rows[i]["bonusamt"].ToString();
                    string linecode = dt.Rows[i]["linecode"].ToString()==""?"00000": dt.Rows[i]["linecode"].ToString();

                    string whlv = dt.Rows[i]["whlv"].ToString();
                    string flv = dt.Rows[i]["flv"].ToString();
                    string clv = dt.Rows[i]["clv"].ToString();
                    string slv = dt.Rows[i]["slv"].ToString();
                    string mlv = dt.Rows[i]["mlv"].ToString();
                    string elv = dt.Rows[i]["elv"].ToString();
                    string arpfund = dt.Rows[i]["arpfund"].ToString();
                    string aritax = dt.Rows[i]["aritax"].ToString();

                    string fallow = dt.Rows[i]["fallow"].ToString();
                    string stamp = dt.Rows[i]["stamp"].ToString();
                    string splv = dt.Rows[i]["splv"].ToString();
                    string orate = dt.Rows[i]["orate"].ToString();
                    string splvFrmOffday = dt.Rows[i]["splvFrmOffday"].ToString();
                    string adlv = dt.Rows[i]["adlv"].ToString();

                    result = HRData.UpdateTransInfoHRSal(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INUPSALSHEET", monthid, refno, empid, wd,
                        absday, wld, acat, bsal, hrent, cven, mallow, arsal, pickup, fuel, entaint, mcell, incent, oth, pfund, itax, adv,
                        othded, dallow, oallow, ohour, hallow, elallow, mbill, lwided, loanins, gssal, salpday, gspay, absded, tallow,
                        tdeduc, dedday.ToString(), sdedamt, netpay, section, desigid, mcadj, othallow, othearn, mcallow, teallow, thday,
                        lwpday, arded, cashamt, bankamt, wjd, empcont, elftam, elfthour, dalday, ddaya10, dday10amt, fallded, mbillded,
                        emptype, bonusamt, gspay2, gssal1, whlv, flv, clv, slv, mlv, elv, arpfund, aritax, spcded, fallow, stamp, splv, orate, splvFrmOffday, adlv, linecode);
                    
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }

                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                string Depart = this.ddlWstation.SelectedValue.ToString();
                string Salarylock = (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked) ? "1" : "0";


                DataTable dtuser = (DataTable)Session["UserLog"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();

                string PostedByid = (this.Request.QueryString["Entry"] == "Payroll") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (this.Request.QueryString["Entry"] == "Payroll") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (this.Request.QueryString["Entry"] == "Payroll") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (this.Request.QueryString["Entry"] == "Payroll") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                string EditByid = (this.Request.QueryString["Entry"] == "Payroll") ? "" : userid;
                string Editdat = (this.Request.QueryString["Entry"] == "Payroll") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
                string EditTrnid = (this.Request.QueryString["Entry"] == "Payroll") ? "" : Terminal;

                //
                string sdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string edate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string paydate = Convert.ToDateTime(this.txtpayment.Text).ToString("dd-MMM-yyyy");
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INORUPSALLOCK", monthid, Depart, Salarylock, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, Editdat, EditTrnid, saltyp, sdate, edate, paydate, "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail');", true);

                    return;
                }

                if (ConstantInfo.LogStatus == true)
                {
                    todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    string eventtype = "SALARY SHEET Updated";
                    string eventdesc = "Month ID: " + todate + ", Employee Type: " + this.ddlWstation.SelectedValue.ToString(); ;
                    string eventdesc2 = "Employe Status-" + (this.ChckResign.Checked == true ? "Resign Employe" : "Active Employee");
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

                }
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " +ex.Message+ "');", true);


            }

        }


        protected void chkBonus_CheckedChanged(object sender, EventArgs e)
        {
            this.divGenBonus.Visible = this.chkBonus.Checked;
        }
        protected void gvBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvBonus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }


        protected void lbtnTotalBonus_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        protected void lnkbtnGenBonus_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblpay"];
            double perbonus = Convert.ToDouble("0" + this.txtBonusPer.Text);
            switch (comcod)
            {
                case "4305":
                case "4101":
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double gssal = Convert.ToDouble(dt.Rows[i]["gssal"]);
                        double bonamt = gssal * 0.01 * perbonus;
                        dt.Rows[i]["perbon"] = perbonus;
                        dt.Rows[i]["bonamt"] = bonamt;
                    }

                    break;
                default:
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double bsal = Convert.ToDouble(dt.Rows[i]["bsal"]);
                        double bonamt = bsal * 0.01 * perbonus;
                        dt.Rows[i]["perbon"] = perbonus;
                        dt.Rows[i]["bonamt"] = bonamt;
                    }

                    break;

            }



            Session["tblpay"] = dt;
            this.LoadGrid();
            this.chkBonus.Checked = false;
            this.chkBonus_CheckedChanged(null, null);

        }
        protected void lnkUpBonus_Click(object sender, EventArgs e)
        {
            if (CheckBoxMaternity.Checked)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please uncheck Maternity status.');", true);
                return;
            }
            DataRow[] dr6 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr6[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblpay"];
            string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string monthid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMM");
            string bondate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string bonusname = this.ddlBonusType.SelectedValue.ToString();
            bool result = false;
            string divsion = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 7)) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string Section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "DELETEBONSHEET", monthid, Company, Department, Section, divsion, "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string perbon = Convert.ToDouble(dt.Rows[i]["perbon"]).ToString();
                string bsal = Convert.ToDouble(dt.Rows[i]["bsal"]).ToString();
                string gssal = Convert.ToDouble(dt.Rows[i]["gssal"]).ToString();
                string section = dt.Rows[i]["section"].ToString();
                string desigid = dt.Rows[i]["desigid"].ToString();
                string durationday = Convert.ToDouble(dt.Rows[i]["durationday"]).ToString();
                string bonamt = Convert.ToDouble(dt.Rows[i]["bonamt"]).ToString("###0;(###0); ");
                string bankamt = Convert.ToDouble(dt.Rows[i]["bankamt"]).ToString();

                string cashamt = Convert.ToDouble(dt.Rows[i]["cashamt"]).ToString();
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "INSERTORUPHRBONINF", monthid, empid, perbon, bsal, gssal, bondate, section, desigid, durationday, bonamt, bankamt, cashamt, bonusname, "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);

                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Festival Bonus Updated Successfully');", true);

            string Bonlock = (((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Checked) ? "1" : "0";
            string deptcode = this.ddlWstation.SelectedValue.ToString();
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INORUPBONLOCK", monthid, deptcode, Bonlock, "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
        }

        protected void gvcashpay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvcashpay.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void gvOvertime_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }


        //protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.ibtnEmpList_Click(null, null);
        //}

        protected void lnkFiUpdateoSalary_Click(object sender, EventArgs e)
        {
            if (CheckBoxMaternity.Checked)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please uncheck Maternity status.');", true);
                return;
            }

            DataRow[] dr6 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr6[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblpay"];
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM");

            bool result = false;

            string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string Department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 8)) + "%";
            string Section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "DELETEOVERSALSHEET", monthid, Company, Department, Section, "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Fail');", true);
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();


                //if (dedday > 0)
                //{
                //    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "INSERTORUPEMPSALADJST", monthid, empid, dedday.ToString(), "", "", "", "", "", "", "", "", "", "", "", "");

                //}
                // string joindate = dt.Rows[i]["joindate"].ToString();
                string section = dt.Rows[i]["secid"].ToString();
                string desigid = dt.Rows[i]["desigid"].ToString();
                string refno = dt.Rows[i]["refno"].ToString();
                //otoffrate   otoffday   otoffamount

                string otrate = dt.Rows[i]["otoffrate"].ToString();
                string otoffday = dt.Rows[i]["otoffday"].ToString();
                string otoffamount = dt.Rows[i]["otoffamount"].ToString();
                //string overhour = dt.Rows[i]["overhour"].ToString();
                //string osday = dt.Rows[i]["osday"].ToString();
                //string offday = dt.Rows[i]["offday"].ToString();

                //string otamount = dt.Rows[i]["otamount"].ToString();
                //string offamount = dt.Rows[i]["offamount"].ToString();
                //string osamount = dt.Rows[i]["osamount"].ToString();
                string netamt = dt.Rows[i]["netamt"].ToString();
                string bsal = dt.Rows[i]["bsal"].ToString();
                string gssal1 = dt.Rows[i]["gssal1"].ToString();



                result = HRData.UpdateTransInfoHRSal(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INUPSALSHEETOV", monthid, refno, section, empid, desigid, otrate, otoffday, otoffamount, netamt, bsal, gssal1);
                if (!result)
                    return;//refdesc

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            string Depart = this.ddlDept.SelectedValue.ToString();
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


            //

            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INORUPSALOVLOCK", monthid, Depart, Salarylock, PostedByid, PostSession, Posttrmid, Posteddat, EditByid, Editdat, EditTrnid, "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Fail');", true);
                return;
            }




        }
        protected void lnkDeduction_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            string Monthid = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("yyyyMM");
            //System.DateTime.Today.ToString("yyyyMM");



            for (int i = 0; i < this.gvpayroll.Rows.Count; i++)
            {
                string empid = ((Label)this.gvpayroll.Rows[i].FindControl("lblempid")).Text;

                double dedamt = Convert.ToDouble("0" + ((TextBox)this.gvpayroll.Rows[i].FindControl("txtSpcDed")).Text.Trim());
                string otherded = dedamt.ToString();
                if (dedamt > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERT_SPECIAL_DEDUCTION_ONLY", Monthid, empid, otherded, "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;
                }


            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            this.lnkbtnShow_Click(null, null);
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
            Session["hrcompnameadd"] = lst;
            this.ddlWstation_SelectedIndexChanged(null, null);

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
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
            this.VisibilitiesLineorJob();
            this.GetEmployeeGrade();
            this.ddlGrade_SelectedIndexChanged(null, null);
        }
        private void VisibilitiesLineorJob()

        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    //string emptype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
                    this.lblline.Visible = true;
                    this.ddlempline.Visible = true;
                    this.lblJob.Visible = true;
                    this.ddlJob.Visible = true;
                    //switch (emptype)
                    //{
                    //    case "9401": //EXECUTIVE EMPLOYEES(FB)
                    //    case "9402"://EXECUTIVE EMPLOYEES(Footbed)
                    //    case "9411"://Supporting Staff- FB(HO)
                    //    case "9412"://Supporting Staff- Foodbed(HO)
                    //        this.lblline.Visible = false;
                    //        this.ddlempline.Visible = false;
                    //        this.lblJob.Visible = true;
                    //        this.ddlJob.Visible = true;
                    //        break;


                    //    //default:

                    //    //    this.lblline.Visible = true;
                    //    //    this.ddlempline.Visible = true;
                    //    //    this.lblJob.Visible = false;
                    //    //    this.ddlJob.Visible = false;
                    //    //    break;
                    //}
                    break;


                default:
                    this.lblline.Visible = true;
                    this.ddlempline.Visible = true;
                    break;



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



        protected void searchbutton_Click(object sender, EventArgs e)
        {
            // DataTable dt = (DataTable)ViewState["NewReq"];
            DataTable dt = (DataTable)Session["tblpay"];

            DataView view = new DataView();
            view.Table = dt;

            view.RowFilter = "idcard LIKE '%" + inputtextbox.Text.Trim().ToString() + "%' OR " +
                    "empname LIKE '%" + inputtextbox.Text.Trim().ToString() + "%'";// +
                                                                                   //"orderid1 ='" + inputtextbox.Text.Trim().ToString() + "' OR " +
                                                                                   //"orderdetailsid = '" + inputtextbox.Text.Trim().ToString() + "' OR " +
                                                                                   //"unitcost = '" + inputtextbox.Text.Trim().ToString() + "' OR " +
                                                                                   //  "phone like '%" + inputtextbox.Text.Trim().ToString() + "%' OR " +
                                                                                   //"itemcount = '" + inputtextbox.Text.Trim().ToString() + "' OR " +
                                                                                   //  "compname LIKE '%" + inputtextbox.Text.Trim().ToString() + "%' ";

            this.gvovsal02.DataSource = view.ToTable();
            this.gvovsal02.DataBind();

        }

        protected void lblgvdeptandemployeeemp_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            string comcod = this.GetCompCode();
            this.lbmodalheading.Text = "Individual Monthly Over Time Details Information. Date :" + this.txtfromdate.Text.ToString() + " To: " + this.txttodate.Text.ToString();

            int index = row.RowIndex;

            string frmdate = this.txtfromdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string Empcode = ((Label)this.gvovsal02.Rows[index].FindControl("lblEmpidOT")).Text.ToString(); // "%" + this.txtSrcEmployee.Text.Trim() + "%";

            DataTable dt = (DataTable)ViewState["tblOtDetails"];


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

        protected void gvovsal02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvpayroll.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Bonus":
                    this.lbtnTotalBonus_Click(null, null);
                    break;

                default:
                    break;

            }
        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Salary":
                case "SalaryHold":
                case "SalaryReg":
                case "SalaryOT":
                    this.lnkFiUpdate_Click(null, null);
                    break;

                case "Bonus":
                    this.lnkUpBonus_Click(null, null);
                    break;

                case "OvertimeSal":
                    this.lnkFiUpdateoSalary_Click(null, null);
                    break;

                default:
                    break;


            }
        }
        protected void gvpayroll_Sorting(object sender, GridViewSortEventArgs e)
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
            DataTable dt = (DataTable)Session["tblpay"];

            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            //Session["SortedView"] = sortedView;
            gvpayroll.DataSource = sortedView;
            gvpayroll.DataBind();

            Session["tblpay"] = sortedView.ToTable();
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

        protected void rbtPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.divBank.Visible = true;
            this.Get_Bank_name();
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
        protected void CheckBoxMaternity_CheckedChanged(object sender, EventArgs e)
        {
            bool maternitystatus = this.CheckBoxMaternity.Checked;

            if (maternitystatus)
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                DataTable dt1 = (DataTable)Session["tblpay"];
                DataView dv = dt1.DefaultView;
                dv.RowFilter = "maternitystatus =1";
                Session["tblpay"] = dv.ToTable();
                this.LoadGrid();
            }
            else
            {
                this.CheckBoxMaternity.Checked = false;
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }

        }
        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAddEmp.Checked)
            {
                DataTable dt1 = (DataTable)Session["tblpay"];
                this.divAddEmp.Visible = true;
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
                        dra["idcard"] = dr1["idcard"].ToString();
                        dra["empname"] = dr1["idcard"].ToString() + "-" + dr1["empname"].ToString();
                        dt.Rows.Add(dra);
                    }


                    //dt1.AddDat

                }
                Session.Remove("tblpay");
                Session.Remove("tbladdemppay");
                DataTable dt2 = dt1.Copy();
                Session["tbladdemppay"] = dt2;
                DataTable dt3 = dt1.Clone();
                Session["tblpay"] = dt3;

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
        protected void lbtnAddEmployee_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblpay"];
            DataTable dtadd = (DataTable)Session["tbladdemppay"];
            string empid = this.ddlEmployee.SelectedValue.ToString();
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");
            if (dr1.Length == 0)
            {
                DataRow[] dra = dtadd.Select("empid='" + empid + "'");
                dt.ImportRow(dra[0]);
            }
            else
            {
                string existempdet = "Employee : " + dr1[0]["idcard"].ToString() + " - " + dr1[0]["empname"].ToString() + " already existed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + existempdet + "');", true);
            }

            DataView dv = dt.DefaultView;
            dv.Sort = ("section,idcard");
            Session["tblpay"] = dv.ToTable();
            this.LoadGrid();


        }

        protected void gvpayslip_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvpayslip.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
    }
}