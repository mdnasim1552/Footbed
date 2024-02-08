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
using System.IO;
using System.Text;
using System.Xml;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class EmpOverTimeSalary03 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text =
                    (this.Request.QueryString["Type"].ToString().Trim() == "OvertimeSal")
                        ? "Employee Over Time Salary" : (this.Request.QueryString["Type"].ToString().Trim() == "Overtimesheet") ? "Monthly Extra OT Sheet-01" :
                        (this.Request.QueryString["Type"].ToString().Trim() == "Overtimesheet2") ? "Monthly Extra OT Sheet-02" :
                          (this.Request.QueryString["Type"].ToString().Trim() == "Overtimeofsheet") ? "Monthlhy Off Day OT Sheet" :
                         (this.Request.QueryString["Type"].ToString().Trim() == "MonIndOTSum") ? "Month Wise Overtime Summary Report" :
                          (this.Request.QueryString["Type"].ToString().Trim() == "DayTotOTSum") ? "Day Wise Total Overtime Summary Report" :
                           (this.Request.QueryString["Type"].ToString().Trim() == "MonSecOTSum") ? "Monthly OT Summary (Section Wise)" :
                          "EMPLOYEE PAY SLIP INFORMATION";

                DateTime date = System.DateTime.Today.AddMonths(-1);
                this.txtfromdate.Text = "01" + date.ToString("dd-MMM-yyyy").Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                GetWorkStation();
                GetDivision();
                GetDeptList();
                GetSectionList();
                GetJobLocation();
                SelectType();
                createTable();
                GetLineddl();
                GetJobLocation();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
        }

        private void createTable()
        {
            DataTable mnuTbl1 = new DataTable();
            mnuTbl1.Columns.Add("comcod", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("monthid", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("refno", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("section", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("empid", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("desigid", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("otrate", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("overhour", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("otamount", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("netamt", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("basicsal", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("grosssal", Type.GetType("System.Decimal"));
            mnuTbl1.Columns.Add("emptype", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("detxml", Type.GetType("System.String"));

            ViewState["tblsalot"] = mnuTbl1;
        }

        public void CommonButton()
        {
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;


            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Overtimesheet":
                case "Overtimesheet2":
                case "Overtimeofsheet":
                case "Overtimesheetcom":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.divPayType.Visible = true;
                    this.divEmpStatus.Visible = true;
                    this.rbtPaymentType.SelectedIndex = 2;
                    this.ddlWstation.SelectedValue = "940300000000";
                    ddlWstation_SelectedIndexChanged(null, null);
                    break;

                case "MonIndOTSum":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "DayTotOTSum":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "MonSecOTSum":
                    this.divEmpStatus.Visible = false;
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
            }
        }

        private void GetLineddl()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";

            ViewState["tbllineddl"] = ds3.Tables[0];
        }

        private void GetJobLocation()
        {
            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            this.SelectIndex();

        }

        private void SelectIndex()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Overtimesheet":
                case "Overtimesheet2":
                case "Overtimeofsheet":
                case "Overtimesheetcom":
                    if (this.lnkbtnShow.Text == "New")
                    {
                        this.lnkbtnShow.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.divAddEmp.Visible = false;
                        this.chkAddEmp.Checked = false;
                        this.gvovsal02.DataSource = null;
                        this.gvovsal02.DataBind();
                        return;

                    }

                    this.lnkbtnShow.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.ShowEmpOvertimeSalay();
                    break;

                case "MonIndOTSum":
                    this.MonthlyIndividualOTSummary();
                    break;

                case "DayTotOTSum":
                    this.DailyTotalOTSummary();
                    break;

                case "MonSecOTSum":
                    if (this.lnkbtnShow.Text == "New")
                    {
                        this.lnkbtnShow.Text = "Ok";
                        this.gvMonOTSumSecWise.DataSource = null;
                        this.gvMonOTSumSecWise.DataBind();
                        return;
                    }

                    this.lnkbtnShow.Text = "New";
                    this.ShowMonOTSumSectionWise();
                    break;


            }


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

        private void ShowEmpOvertimeSalay()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string empStatus = (this.ddlEmpStatus.SelectedValue == "1") ? "R" : (this.ddlEmpStatus.SelectedValue == "2") ? "H" : "";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string EmpType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string Division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string ottype = (this.Request.QueryString["Type"] == "Overtimesheet") ? "Ot1hour"
                : (this.Request.QueryString["Type"] == "Overtimesheet2") ? "exOthour"
                : (this.Request.QueryString["Type"] == "Overtimesheetcom") ? "comhour"
                : "Offday";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL03", "OVERTIMESALOTEOTAOFF", null, null, null, frmdate, todate, EmpType,
                    Division, Department, section, payType, empStatus, line, ottype, joblocation, userid, "", "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvovsal02.DataSource = null;
                this.gvovsal02.DataBind();
                return;
            }

            DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;
            this.LoadGrid();

        }
        private void MonthlyIndividualOTSummary()
        {
            Session.Remove("tblpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string empStatus = (this.ddlEmpStatus.SelectedValue == "1") ? "R" : (this.ddlEmpStatus.SelectedValue == "2") ? "H" : "";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string EmpType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string Division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string payType = (this.rbtPaymentType.SelectedIndex == 0) ? "" : (this.rbtPaymentType.SelectedIndex == 1) ? "19%" : "%";
            string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "MONTHLY_INDIVIDUAL_OT_SUMMARY", null, null, null, frmdate, todate, EmpType,
                    Division, Department, section, payType, empStatus, line, joblocation, userid, "", "", "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvovsal02.DataSource = null;
                this.gvovsal02.DataBind();
                return;
            }

            DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;
            this.LoadGrid();

        }

        private void DailyTotalOTSummary()
        {
            Session.Remove("tblpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string empStatus = (this.ddlEmpStatus.SelectedValue == "1") ? "R" : (this.ddlEmpStatus.SelectedValue == "2") ? "H" : "";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string EmpType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string Division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "DAILY_TOTAL_OT_SUMMARY", null, null, null, frmdate, todate, EmpType,
                    Division, Department, section, "", empStatus, line, joblocation, userid, "", "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvdailyOTSum.DataSource = null;
                this.gvdailyOTSum.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;
            this.LoadGrid();

        }

        private void ShowMonOTSumSectionWise()
        {
            Session.Remove("tblpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string EmpType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string Division = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds4 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "MONTHLY_OT_SUM_SECTION_WISE", null, null, null, frmdate, todate, EmpType,
                    Division, Department, section, line, joblocation, userid, "", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvMonOTSumSecWise.DataSource = null;
                this.gvMonOTSumSecWise.DataBind();
                return;

            }

            Session["tblpay"] = ds4.Tables[0];
            Session["tblsecname"] = ds4.Tables[1];
            this.LoadGrid();
        }
        private void LoadGrid()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblpay"];

                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {

                    case "Overtimesheet":
                    case "Overtimesheet2":
                    case "Overtimeofsheet":
                    case "Overtimesheetcom":
                        this.gvovsal02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            this.gvovsal02.DataSource = null;
                            this.gvovsal02.DataBind();

                            return;
                        }

                        this.gvovsal02.DataSource = dt;
                        this.gvovsal02.DataBind();

                        if (dt.Rows.Count > 0)
                        {
                            Session["Report1"] = gvovsal02;
                            ((HyperLink)this.gvovsal02.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                            this.FooterCalculation();
                        }
                        break;
                    case "MonIndOTSum":
                        this.MultiView1.ActiveViewIndex = 1;
                        this.grvIndOvrSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            this.grvIndOvrSum.DataSource = null;
                            this.grvIndOvrSum.DataBind();

                            return;
                        }

                        this.grvIndOvrSum.DataSource = dt;
                        this.grvIndOvrSum.DataBind();

                        break;
                    case "DayTotOTSum":
                        this.MultiView1.ActiveViewIndex = 2;
                        //this.gvdailyOTSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            this.gvdailyOTSum.DataSource = null;
                            this.gvdailyOTSum.DataBind();

                            return;
                        }

                        this.gvdailyOTSum.DataSource = dt;
                        this.gvdailyOTSum.DataBind();

                        ((Label)this.gvdailyOTSum.FooterRow.FindControl("lgvWorkertotal")).Text = "T-" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(wkovthr)", "")) ? 0.00 : dt.Compute("sum(wkovthr)", ""))).ToString("#,##0;(#,##0);");
                        ((Label)this.gvdailyOTSum.FooterRow.FindControl("lgvWorkerC")).Text = "C-" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(wkcomplaincehr)", "")) ? 0.00 : dt.Compute("sum(wkcomplaincehr)", ""))).ToString("#,##0;(#,##0);");
                        ((Label)this.gvdailyOTSum.FooterRow.FindControl("lgvWorkerE")).Text = "E-" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(wkextrahr)", "")) ? 0.00 : dt.Compute("sum(wkextrahr)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvdailyOTSum.FooterRow.FindControl("lgvStaffT")).Text = "T-" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(stovthr)", "")) ? 0.00 : dt.Compute("sum(stovthr)", ""))).ToString("#,##0;(#,##0);");
                        ((Label)this.gvdailyOTSum.FooterRow.FindControl("lgvStaffEx")).Text = "E-" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(stextrahr)", "")) ? 0.00 : dt.Compute("sum(stextrahr)", ""))).ToString("#,##0;(#,##0);");
                        ((Label)this.gvdailyOTSum.FooterRow.FindControl("lgvStaffC")).Text = "C-" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(stcomplaincehr)", "")) ? 0.00 : dt.Compute("sum(stcomplaincehr)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvdailyOTSum.FooterRow.FindControl("lgvTTC")).Text = "C-" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ttcomplaincehr)", "")) ? 0.00 : dt.Compute("sum(ttcomplaincehr)", ""))).ToString("#,##0;(#,##0);");
                        ((Label)this.gvdailyOTSum.FooterRow.FindControl("lgvTTE")).Text = "E-" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ttextrahr)", "")) ? 0.00 : dt.Compute("sum(ttextrahr)", ""))).ToString("#,##0;(#,##0);");
                        ((Label)this.gvdailyOTSum.FooterRow.FindControl("lgvftotal")).Text = "T-" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ttovthr)", "")) ? 0.00 : dt.Compute("sum(ttovthr)", ""))).ToString("#,##0;(#,##0); ");

                        break;

                    case "MonSecOTSum":
                        DataTable dt1 = (DataTable)Session["tblsecname"];
                        this.MultiView1.ActiveViewIndex = 3;
                        this.gvMonOTSumSecWise.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        int i = 0;
                        for (i = 2; i < this.gvMonOTSumSecWise.Columns.Count; i++)
                        {
                            if (i == 48)
                                break;
                            this.gvMonOTSumSecWise.Columns[i].Visible = false;
                        }

                        int j = 2;
                        for (i = 0; i < dt1.Rows.Count; i++)
                        {
                            this.gvMonOTSumSecWise.Columns[j].Visible = true;
                            this.gvMonOTSumSecWise.Columns[j].HeaderText = dt1.Rows[i]["section"].ToString();
                            j++;
                        }

                        this.gvMonOTSumSecWise.DataSource = dt;
                        this.gvMonOTSumSecWise.DataBind();
                        this.FooterCalculation();

                        if (dt.Rows.Count == 0)
                            return;
                        Session["Report1"] = gvMonOTSumSecWise;
                            ((HyperLink)this.gvMonOTSumSecWise.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;
                }
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "')", true);

            }

        }


        private void FooterCalculation()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblpay"];

                if (dt.Rows.Count == 0)
                    return;
                string type = this.Request.QueryString["Type"].ToString().Trim();

                switch (type)
                {
                    case "Overtimesheet":
                    case "Overtimesheet2":
                    case "Overtimeofsheet":
                    case "Overtimesheetcom":

                        ((Label)this.gvovsal02.FooterRow.FindControl("lgvFBasic")).Text = Convert
                            .ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", "")))
                            .ToString("#,##0;(#,##0); ");
                        ((Label)this.gvovsal02.FooterRow.FindControl("lgvFGross")).Text = Convert
                            .ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal1)", "")) ? 0.00 : dt.Compute("sum(gssal1)", "")))
                            .ToString("#,##0;(#,##0); ");
                        int tovtmin = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(tovtmin)", ""))
                              ? 0.00 : dt.Compute("sum(tovtmin)", "")));

                        // int txtHrsFrac = Convert.ToInt32(Minute / 60);
                        // double txtMinFrac = Minute % 60;
                        double totalHrs = Convert.ToInt32(tovtmin / 60) + ((tovtmin % 60) * 0.01);
                        ((Label)this.gvovsal02.FooterRow.FindControl("lgvFothour")).Text = totalHrs.ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvovsal02.FooterRow.FindControl("lgvFotamt")).Text = Convert
                          .ToDouble((Convert.IsDBNull(dt.Compute("sum(otamount)", ""))
                              ? 0.00 : dt.Compute("sum(otamount)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvovsal02.FooterRow.FindControl("lgvFnetamtos")).Text = Convert
                            .ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ? 0.00 : dt.Compute("sum(netamt)", "")))
                            .ToString("#,##0;(#,##0); ");
                        break;

                    case "MonSecOTSum":
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s1)", "")) ? 0.00 : dt.Compute("sum(s1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s2)", "")) ? 0.00 : dt.Compute("sum(s2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s3)", "")) ? 0.00 : dt.Compute("sum(s3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s4)", "")) ? 0.00 : dt.Compute("sum(s4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s5)", "")) ? 0.00 : dt.Compute("sum(s5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s6)", "")) ? 0.00 : dt.Compute("sum(s6)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s7)", "")) ? 0.00 : dt.Compute("sum(s7)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s8)", "")) ? 0.00 : dt.Compute("sum(s8)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s9)", "")) ? 0.00 : dt.Compute("sum(s9)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s10)", "")) ? 0.00 : dt.Compute("sum(s10)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s11)", "")) ? 0.00 : dt.Compute("sum(s11)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s12)", "")) ? 0.00 : dt.Compute("sum(s12)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s13)", "")) ? 0.00 : dt.Compute("sum(s13)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s14)", "")) ? 0.00 : dt.Compute("sum(s14)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s15)", "")) ? 0.00 : dt.Compute("sum(s15)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s16)", "")) ? 0.00 : dt.Compute("sum(s16)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s17)", "")) ? 0.00 : dt.Compute("sum(s17)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s18)", "")) ? 0.00 : dt.Compute("sum(s18)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s19)", "")) ? 0.00 : dt.Compute("sum(s19)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s20)", "")) ? 0.00 : dt.Compute("sum(s20)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS21")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s21)", "")) ? 0.00 : dt.Compute("sum(s21)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS22")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s22)", "")) ? 0.00 : dt.Compute("sum(s22)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS23")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s23)", "")) ? 0.00 : dt.Compute("sum(s23)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS24")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s24)", "")) ? 0.00 : dt.Compute("sum(s24)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS25")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s25)", "")) ? 0.00 : dt.Compute("sum(s25)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS26")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s26)", "")) ? 0.00 : dt.Compute("sum(s26)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS27")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s27)", "")) ? 0.00 : dt.Compute("sum(s27)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS28")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s28)", "")) ? 0.00 : dt.Compute("sum(s28)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS29")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s29)", "")) ? 0.00 : dt.Compute("sum(s29)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFS30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s30)", "")) ? 0.00 : dt.Compute("sum(s30)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvTotalFOT")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalovt)", "")) ? 0.00 : dt.Compute("sum(totalovt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM1")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m1)", "")) ? 0 : dt.Compute("sum(m1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM2")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m2)", "")) ? 0 : dt.Compute("sum(m2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM3")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m3)", "")) ? 0 : dt.Compute("sum(m3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM4")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m4)", "")) ? 0 : dt.Compute("sum(m4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM5")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m5)", "")) ? 0 : dt.Compute("sum(m5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM6")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m6)", "")) ? 0 : dt.Compute("sum(m6)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM7")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m7)", "")) ? 0 : dt.Compute("sum(m7)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM8")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m8)", "")) ? 0 : dt.Compute("sum(m8)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM9")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m9)", "")) ? 0 : dt.Compute("sum(m9)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM10")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m10)", "")) ? 0 : dt.Compute("sum(m10)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM11")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m11)", "")) ? 0 : dt.Compute("sum(m11)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM12")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m12)", "")) ? 0 : dt.Compute("sum(m12)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM13")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m13)", "")) ? 0 : dt.Compute("sum(m13)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM14")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m14)", "")) ? 0 : dt.Compute("sum(m14)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM15")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m15)", "")) ? 0 : dt.Compute("sum(m15)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM16")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m16)", "")) ? 0 : dt.Compute("sum(m16)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM17")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m17)", "")) ? 0 : dt.Compute("sum(m17)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM18")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m18)", "")) ? 0 : dt.Compute("sum(m18)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM19")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m19)", "")) ? 0 : dt.Compute("sum(m19)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM20")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m20)", "")) ? 0 : dt.Compute("sum(m20)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM21")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m21)", "")) ? 0 : dt.Compute("sum(m21)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM22")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m22)", "")) ? 0 : dt.Compute("sum(m22)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM23")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m23)", "")) ? 0 : dt.Compute("sum(m23)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM24")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m24)", "")) ? 0 : dt.Compute("sum(m24)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM25")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m25)", "")) ? 0 : dt.Compute("sum(m25)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM26")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m26)", "")) ? 0 : dt.Compute("sum(m26)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM27")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m27)", "")) ? 0 : dt.Compute("sum(m27)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM28")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m28)", "")) ? 0 : dt.Compute("sum(m28)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM29")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m29)", "")) ? 0 : dt.Compute("sum(m29)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvFM30")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(m30)", "")) ? 0 : dt.Compute("sum(m30)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonOTSumSecWise.FooterRow.FindControl("lgvTotalMan")).Text = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(totalmanpower)", "")) ? 0.00 : dt.Compute("sum(totalmanpower)", ""))).ToString("#,##0;(#,##0); ");
                        break;

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "')", true);
            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Overtimesheet":
                case "Overtimesheet2":
                case "Overtimeofsheet":
                case "Overtimesheetcom":

                    this.PrintOvertimeSalary03();
                    break;
                case "MonIndOTSum":
                    this.PrintMonIndOTSum();
                    break;
                case "DayTotOTSum":
                    this.PrintDayTotOTSum();
                    break;
                case "MonSecOTSum":
                    this.PrintMonOTSumSectionWise();
                    break;


            }
        }

        private void PrintOvertimeSalary03()
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
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string empType = this.ddlWstation.SelectedItem.Text.ToString();
            string empType1 = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string department = this.ddlDept.SelectedItem.Text.ToString();
            string section = this.ddlSection.SelectedItem.Text.ToString();
            string line = this.ddlempline.SelectedItem.Text.ToString();
            string title = (Type == "Overtimesheet") ? "Monthly Extra OT Sheet"
                : (Type == "Overtimesheet2") ? "Monthly Extra OT 2 Sheet"
                : (Type == "Overtimesheetcom") ? "Monthly Compliance OT Sheet"
                : "Monthly Off Day OT Sheet";
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> list1 = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>)Session["hrcompnameadd"];

            var lst2 = list1.FindAll(l => l.actcode.Substring(0, 4) == empType1);
            string compName = lst2[0].actcode == "000000000000" ? comname : lst2[0].hrcomname.ToString();
            string compAdd = lst2[0].actcode == "000000000000" ? list1[1].hrcomadd.ToString() : lst2[0].hrcomadd.ToString();

            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.OverTimeSal>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptOverTimeSalFB", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", compName));
            Rpt1.SetParameters(new ReportParameter("comadd", compAdd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", title));
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
        private void PrintMonIndOTSum()
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
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string empType = this.ddlWstation.SelectedItem.Text.ToString();
            string empType1 = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string department = this.ddlDept.SelectedItem.Text.ToString();
            string section = this.ddlSection.SelectedItem.Text.ToString();
            string line = this.ddlempline.SelectedItem.Text.ToString();
            string title = (Type == "Overtimesheet") ? "Monthly Extra OT Sheet"
                : (Type == "Overtimesheet2") ? "Monthly Extra OT 2 Sheet" : (Type == "MonIndOTSum") ? "Month Wise Overtime Summary Report"
                : "Monthly Off Day OT Sheet";

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> list1 = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>)Session["hrcompnameadd"];
            string compName = list1[0].hrcomname.ToString();
            string compAdd = list1[0].hrcomadd.ToString();

            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.OverTimeSal>();
            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5305":
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptMonIndOTSumFB", list, null, null);
                    break;
                default:
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptOverTimeSalFB", list, null, null);
                    break;
            }
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", compName));
            Rpt1.SetParameters(new ReportParameter("comadd", compAdd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", title));
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

        private void PrintDayTotOTSum()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compname = hst["comnam"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " +
                                 username + " ,Time: " + printdate;
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");

            string title = "Day Wise Total Overtime Summary Report";

            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.DayTotOTSum>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptDayTotOTSum", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", compname));
            Rpt1.SetParameters(new ReportParameter("rptTitle", title));
            Rpt1.SetParameters(new ReportParameter("todate", "For the Month of " + todate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMonOTSumSectionWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~/Image/LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblpay"];
            DataTable dt1 = (DataTable)Session["tblsecname"];

            var lst1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptMonthlyOTSumSectionWise>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptMonOTSumSectionWise", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));

            int j = 1;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                Rpt1.SetParameters(new ReportParameter("s"+j, dt1.Rows[i]["section"].ToString()));
                j++;
            }
            Rpt1.SetParameters(new ReportParameter("date", "Date: " + date));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly OT Summary (Hrs) Section Wise"));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.LoadGrid();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GetWorkStation()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            switch (comcod)
            {
                //FB & Footbed Footwear OT Emp. Only
                case "5305":
                    lst = lst.FindAll(x => (x.actcode.Substring(0, 4) == "0000") || (x.actcode.Substring(0, 4) == "9403") || (x.actcode.Substring(0, 4) == "9414"));
                    break;
                case "5306":
                    lst = lst.FindAll(x => x.actcode.Substring(0, 4) == "0000" || x.actcode.Substring(0, 4) == "9408" || x.actcode.Substring(0, 4) == "9416");
                    break;

                default:
                    lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
                    break;
            }
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            Session["hrcompnameadd"] = lst;
            // this.ddlWstation_SelectedIndexChanged(null, null);

        }

        private void GetDivision()
        {
            string comcod = GetCompCode();
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

        private void GetDeptList()
        {
            string comcod = GetCompCode();
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

            string comcod = GetCompCode();
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
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }


        protected void gvovsal02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvovsal02.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }



        protected void lblgvdeptandemployeeemp_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            string comcod = this.GetCompCode();
            this.lbmodalheading.Text = "Individual Monthly Over Time Details Information. Date :" +
                                       this.txtfromdate.Text.ToString() + " To: " + this.txttodate.Text.ToString();

            int index = row.RowIndex;
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();

            string frmdate = this.txtfromdate.Text.Trim();
            string todate = this.txttodate.Text.Trim();
            string Empcode =
                ((Label)this.gvovsal02.Rows[index].FindControl("LblEmpid")).Text
                .ToString();
            string xmldata = ((Label)this.gvovsal02.Rows[index].FindControl("lblxmlcol1")).Text.ToString();
            DataSet ds = new DataSet();
            byte[] buffer = Encoding.UTF8.GetBytes(xmldata);
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                XmlReader reader = XmlReader.Create(stream);
                ds.ReadXml(reader);

            }

            DataTable dt = ds.Tables[0];
            this.mgvbreakdown.DataSource = dt;
            this.mgvbreakdown.DataBind();


            ArrayList rows = new ArrayList();

            foreach (DataRow dataRow in dt.Rows)
                rows.Add(string.Join(";", dataRow.ItemArray.Select(ovthour => ovthour.ToString())));
            Session["Report1"] = mgvbreakdown;
            if (dt.Rows.Count > 0)
                ((HyperLink)this.mgvbreakdown.HeaderRow.FindControl("mhlbtntbCdataExel")).NavigateUrl =
                    "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }



        protected void gvovsal02_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton txtgvname = (LinkButton)e.Row.FindControl("lblgvdeptandemployeeemp");
            }
        }

        protected void ChckResign_OnCheckedChanged(object sender, EventArgs e)
        {
            this.lnkbtnShow.Text = "Ok";
            this.SelectIndex();
        }
        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddEmp.Checked)
            {
                this.divAddEmp.Visible = true;
                Session.Remove("tblemp");
                DataTable dt = (DataTable)Session["tblpay"];
                //Concat ID Card & Name
                this.CreateDataTable();
                DataTable dt1 = (DataTable)Session["tblemp"];
                foreach (DataRow dr1 in dt.Rows)
                {
                    string empid = dr1["empid"].ToString();
                    if (dt1.Select("empid='" + empid + "'").Length == 0)
                    {
                        DataRow dra = dt1.NewRow();
                        dra["empid"] = dr1["empid"].ToString();
                        dra["idcard"] = dr1["idcard"].ToString();
                        dra["empname"] = dr1["idcard"].ToString() + "-" + dr1["empname"].ToString();
                        dt1.Rows.Add(dra);
                    }

                }

                Session.Remove("tblpay");
                Session.Remove("tbladdemppay");
                DataTable dt2 = dt.Clone();
                DataTable dt3 = dt.Copy();
                Session["tblpay"] = dt2;
                Session["tbladdemppay"] = dt3;

                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = dt1;
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
                DataTable dt = (DataTable)Session["tbladdemppay"];
                DataTable dt1 = (DataTable)Session["tblpay"];
                string empId = this.ddlEmployee.SelectedValue.ToString();
                DataRow[] dr = dt1.Select("empid='" + empId + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt1.NewRow();
                    string Type = this.Request.QueryString["Type"].ToString().Trim();
                    switch (Type)
                    {
                        case "Overtimesheet":
                        case "Overtimesheet2":
                        case "Overtimeofsheet":
                        case "Overtimesheetcom":
                            dr1["empid"] = this.ddlEmployee.SelectedValue.ToString();
                            dr1["rowid"] = dt.Select("empid='" + empId + "'")[0]["rowid"];
                            dr1["refno1"] = dt.Select("empid='" + empId + "'")[0]["refno1"];
                            dr1["refno"] = dt.Select("empid='" + empId + "'")[0]["refno"];
                            dr1["secid"] = dt.Select("empid='" + empId + "'")[0]["secid"];
                            dr1["desigid"] = dt.Select("empid='" + empId + "'")[0]["desigid"];
                            dr1["gradeid"] = dt.Select("empid='" + empId + "'")[0]["gradeid"];
                            dr1["grade"] = dt.Select("empid='" + empId + "'")[0]["grade"];
                            dr1["linecode"] = dt.Select("empid='" + empId + "'")[0]["linecode"];
                            dr1["deptname"] = dt.Select("empid='" + empId + "'")[0]["deptname"];
                            dr1["section"] = dt.Select("empid='" + empId + "'")[0]["section"];
                            dr1["fline"] = dt.Select("empid='" + empId + "'")[0]["fline"];
                            dr1["empname"] = dt.Select("empid='" + empId + "'")[0]["empname"];
                            dr1["desig"] = dt.Select("empid='" + empId + "'")[0]["desig"];
                            dr1["idcard"] = dt.Select("empid='" + empId + "'")[0]["idcard"];
                            dr1["joindate"] = dt.Select("empid='" + empId + "'")[0]["joindate"];
                            dr1["bsal"] = dt.Select("empid='" + empId + "'")[0]["bsal"];
                            dr1["gssal1"] = dt.Select("empid='" + empId + "'")[0]["gssal1"];
                            dr1["ohour"] = dt.Select("empid='" + empId + "'")[0]["ohour"];
                            dr1["tovtmin"] = dt.Select("empid='" + empId + "'")[0]["tovtmin"];
                            dr1["otrate"] = dt.Select("empid='" + empId + "'")[0]["otrate"];
                            dr1["otamount"] = dt.Select("empid='" + empId + "'")[0]["otamount"];
                            dr1["netamt"] = dt.Select("empid='" + empId + "'")[0]["netamt"];
                            dr1["bankacno"] = dt.Select("empid='" + empId + "'")[0]["bankacno"];
                            dr1["bankamt"] = dt.Select("empid='" + empId + "'")[0]["bankamt"];
                            dr1["cashamt"] = dt.Select("empid='" + empId + "'")[0]["cashamt"];
                            dr1["foodalw"] = dt.Select("empid='" + empId + "'")[0]["foodalw"];
                            dr1["xmlcol1"] = dt.Select("empid='" + empId + "'")[0]["xmlcol1"];
                            break;


                        default:
                            break;
                    }

                    dt1.Rows.Add(dr1);
                }

                DataView dv = dt1.DefaultView;
                dv.Sort = "refno1, secid";
                Session["tblpay"] = dv.ToTable();
                this.LoadGrid();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }

        protected void gvMonOTSumSecWise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonOTSumSecWise.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void chkHoldEmpOT_CheckedChanged(object sender, EventArgs e)
        {
            this.lnkbtnShow.Text = "Ok";
            this.SelectIndex();
        }
    }
}