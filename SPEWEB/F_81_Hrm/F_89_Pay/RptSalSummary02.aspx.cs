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
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptSalSummary02 : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                string type = this.Request.QueryString["Type"].ToString().Trim();
                this.GetMonth();
                this.GetWorkStation();
                this.GetDivision();
                this.GetDeptList();
                this.GetSectionList();
                this.SelectType();
                this.GetJobLocation();
                this.CommonButton();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "BonusSummary") ? "Bonus Requisition" : (type == "BonusSummary02") ? "Bonus Summary" :
                    (type == "SalSummaryOV") ? "Monthly All OT Summary Sheet" : (type == "CashBonus") ? "Bonus Sheet (Cash)" :
                    (type == "CashSalary") ? "Salary Statement (Cash)" : (type == "MonSalSum") ? "Monthly Salary Summary" :
                    (type == "SalSummary") ? "Monthly Salary Summary 02" : "Monthly All OT Summary Sheet";

            }
        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).ToolTip = "Total Calculation";


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lFinalTotal_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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

        }
        private void GetDivision()
        {

            string comcod = GetComeCode();
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
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            var lst = getlist.GetDept(comcod, wstation);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "00000";
        }

        private void GetSectionList()
        {
            string comcod = GetComeCode();
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
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
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
        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSummary":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "CashSalary":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "SalLACA":
                    this.divRptType.Visible = true;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "RPTENVELOP":
                    this.lnkbtnShow.Visible = false;
                    break;

                case "CashBonus":
                    this.divBonType.Visible = true;
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "BonusSummary":
                    this.divBonType.Visible = true;
                    this.MultiView1.ActiveViewIndex = 4;
                    break;

                case "BonPaySlip":
                    this.divBonType.Visible = true;
                    this.lnkbtnShow.Visible = false;
                    break;


                case "SalSummary02":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;
                case "SalSummary02Reg":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "SalSummaryOV":
                    this.MultiView1.ActiveViewIndex = 6;
                    break;
                case "BonusSummary02":
                    this.divBonType.Visible = true;
                    this.MultiView1.ActiveViewIndex = 7;
                    break;
                case "MonSalSum":
                    this.divBonSum.Visible = true;
                    this.divFilter1.Visible = false;
                    this.MultiView1.ActiveViewIndex = 8;
                    break;

                case "SalSummaryEot":
                    this.MultiView1.ActiveViewIndex = 9;
                    break;
            }



        }

        private void GetMonth()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlMonth.DataTextField = "yearmon";
            this.ddlMonth.DataValueField = "ymon";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();
        }


        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSummary":
                    this.ShowSalSummary();
                    break;

                case "CashSalary":
                    this.ShowCashSalary();
                    break;
                case "SalLACA":
                    this.ShowSalaLACA();
                    break;
                case "CashBonus":
                    this.ShowCashBonous();
                    break;

                case "BonusSummary":
                    this.ShowBonousSummary();
                    break;

                case "SalSummary02":
                case "SalSummary02Reg":
                    this.ShowSalSummary02();
                    break;

                case "SalSummaryOV":
                    this.ShowSalSummaryOV();
                    break;
                case "BonusSummary02":
                    this.ShowBonousSummary02();
                    break;
                case "MonSalSum":
                    this.MonthlySalarySummery();
                    break;
                case "SalSummaryEot":
                    this.MonthlySalSummaryEot();
                    break;


            }
        }
        private void MonthlySalarySummery()
        {

            string comcod = this.GetComeCode();
            string month = this.ddlMonth.SelectedValue.ToString();
            string calltype = this.ckBonus.Checked == true ? "RPTSUMMARYOFSALRYBONUS" : "RPTSUMMARYOFSALRY";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", calltype, month, "", "", "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvmonthsalsum.DataSource = null;
                this.gvmonthsalsum.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameDataMonthSum(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();

        }

        private void MonthlySalSummaryEot()
        {

            string comcod = this.GetComeCode();
            string month = this.ddlMonth.SelectedValue.ToString();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string divid = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "EXTRAOVERTIMESALSUMMARY", month, Company, deptid, section, "", "", "", "", "");

            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvEotsalsum.DataSource = null;
                this.gvEotsalsum.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();

        }
        private void ShowSalSummary()
        {

            string comcod = this.GetComeCode();
            string month = this.ddlMonth.SelectedValue.ToString();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string divid = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTSALARYSUMMARY", month, Company, deptid, section, "", "", "", "", "");

            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvSalSum.DataSource = null;
                this.gvSalSum.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();

        }

        private void ShowCashSalary()
        {

            string comcod = this.GetComeCode();
            string month = this.ddlMonth.SelectedValue.ToString();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string divid = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTCASHSALARY", month, Company, deptid, section, "", "", "", "", "");

            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvSalSum.DataSource = null;
                this.gvSalSum.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();


        }

        //ShowSalaLACR();
        private void ShowSalaLACA()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string month = this.ddlMonth.SelectedValue.ToString();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string divid = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string loccode = ((this.ddlJobLocation.SelectedValue.ToString() == "00000") ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string rptType = this.ddlRptType.SelectedValue.ToString().Trim();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTSALLACR", month, Company, divid, deptid, section, userid, loccode, rptType, "", "");
            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvLACA.DataSource = null;
                this.gvLACA.DataBind();
                return;
            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();
        }

        private void ShowCashBonous()
        {
            string comcod = this.GetComeCode();
            string month = this.ddlMonth.SelectedValue.ToString();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string divid = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTCASHBONOUS", month, Company, deptid, section, "", "", "", "", "");

            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvSalSum.DataSource = null;
                this.gvSalSum.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();




        }

        private void ShowBonousSummary02()
        {
            string comcod = this.GetComeCode();
            string month = this.ddlMonth.SelectedValue.ToString();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string divid = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "RPTBONSUMMARYSTAR", month, Company, deptid, section, "", "", "", "", "");

            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvbonussam02.DataSource = null;
                this.gvbonussam02.DataBind();
                return;

            }

            Session["tblSalSum"] = ds3.Tables[0];// dt;
            this.Data_Bind();
        }

        private void ShowBonousSummary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string month = this.ddlMonth.SelectedValue.ToString();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string divid = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string loccode = ((this.ddlJobLocation.SelectedValue.ToString() == "00000") ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTBONSUMMARY", month, Company, divid, deptid, section, loccode, userid, "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvBonusSum.DataSource = null;
                this.gvBonusSum.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            Session["tblSalSum"] = dt;
            this.Data_Bind();
        }


        private void ShowSalSummary02()
        {
            string saltyp = "";
            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (type == "SalSummary02Reg")
                saltyp = "R";
            string comcod = this.GetComeCode();
            string month = this.ddlMonth.SelectedValue.ToString();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string divid = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTSALARYSUMMARY02", month, Company, divid, deptid, section, saltyp, "", "", "");

            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvsalsum02.DataSource = null;
                this.gvsalsum02.DataBind();
                return;

            }

            Session["tblSalSum"] = ds3.Tables[0];
            this.Data_Bind();

        }

        private void ShowSalSummaryOV()
        {
            string comcod = this.GetComeCode();
            string month = this.ddlMonth.SelectedValue.ToString();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString()) + "%";
            string divid = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "OVERTIMESALSUMMARY", month, Company, deptid, section, "", "", "", "", "");

            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvSummaryOV.DataSource = null;
                this.gvSummaryOV.DataBind();
                return;

            }

            Session["tblSalSum"] = ds3.Tables[0];
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string refno = dt1.Rows[0]["refno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["refno"].ToString() == refno)
                {
                    refno = dt1.Rows[j]["refno"].ToString();
                    dt1.Rows[j]["refdesc"] = "";
                }
                else
                {
                    refno = dt1.Rows[j]["refno"].ToString();
                }
            }
            return dt1;

        }
        private DataTable HiddenSameDataMonthSum(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string emptype = dt1.Rows[0]["emptype"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["emptype"].ToString() == emptype)
                {
                    emptype = dt1.Rows[j]["emptype"].ToString();
                    dt1.Rows[j]["empcat"] = "";
                }
                else
                {
                    emptype = dt1.Rows[j]["emptype"].ToString();
                }
            }
            return dt1;

        }
        private void lFinalTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }

        private void SaveValue()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblSalSum"];
                int TblRowIndex;
                for (int i = 0; i < this.gvBonusSum.Rows.Count; i++)
                {
                    double bonAmt = Convert.ToDouble("0" + ((Label)this.gvBonusSum.Rows[i].FindControl("lgvBonusAmtbsum")).Text.Trim());
                    double manualAmt = Convert.ToDouble("0" + ((TextBox)this.gvBonusSum.Rows[i].FindControl("txtgvManAmt")).Text.Trim());
                    string remarks = ((TextBox)this.gvBonusSum.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                    TblRowIndex = (gvBonusSum.PageIndex) * gvBonusSum.PageSize + i;

                    double totalbonAmt = bonAmt + manualAmt;
                    dt.Rows[TblRowIndex]["manualamt"] = manualAmt;
                    dt.Rows[TblRowIndex]["totalbonamt"] = totalbonAmt;
                    dt.Rows[TblRowIndex]["remarks"] = remarks;
                }

                Session["tblSalSum"] = dt;
                this.Data_Bind();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)(DataTable)Session["tblSalSum"];
                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {
                    case "SalSummary":
                        this.gvSalSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvSalSum.DataSource = dt;
                        this.gvSalSum.DataBind();
                        this.FooterCalculation(dt);
                        break;

                    case "CashSalary":
                        this.gvcashpay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvcashpay.DataSource = dt;
                        this.gvcashpay.DataBind();
                        this.FooterCalculation(dt);
                        break;

                    case "SalLACA":
                        this.gvLACA.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvLACA.DataSource = dt;
                        this.gvLACA.DataBind();
                        this.FooterCalculation(dt);
                        break;

                    case "CashBonus":
                        this.gvBonus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvBonus.DataSource = dt;
                        this.gvBonus.DataBind();
                        this.FooterCalculation(dt);
                        break;

                    case "BonusSummary":
                        this.gvBonusSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvBonusSum.DataSource = dt;
                        this.gvBonusSum.DataBind();
                        if (dt.Rows.Count > 0)
                        {
                            Session["Report1"] = gvBonusSum;
                            ((HyperLink)this.gvBonusSum.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                            this.FooterCalculation(dt);
                        }
                        break;

                    case "SalSummary02":
                        this.gvsalsum02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvsalsum02.DataSource = dt;
                        this.gvsalsum02.DataBind();
                        this.FooterCalculation(dt);
                        break;

                    case "SalSummary02Reg":
                        this.gvsalsum02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvsalsum02.DataSource = dt;
                        this.gvsalsum02.DataBind();
                        this.FooterCalculation(dt);
                        break;

                    case "SalSummaryOV":
                        this.gvSummaryOV.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvSummaryOV.DataSource = dt;
                        this.gvSummaryOV.DataBind();
                        this.FooterCalculation(dt);
                        break;

                    case "BonusSummary02":
                        this.gvbonussam02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvbonussam02.DataSource = dt;
                        this.gvbonussam02.DataBind();
                        this.FooterCalculation(dt);
                        break;

                    case "MonSalSum":
                        this.gvmonthsalsum.DataSource = dt;
                        this.gvmonthsalsum.DataBind();
                        this.FooterCalculation(dt);
                        break;

                    case "SalSummaryEot":
                        this.gvEotsalsum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEotsalsum.DataSource = dt;
                        this.gvEotsalsum.DataBind();
                        this.FooterCalculation(dt);

                        break;

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSummary":

                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFbSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFhrent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ? 0.00 : dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFCon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ? 0.00 : dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFmallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ? 0.00 : dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFarier")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arsal)", "")) ? 0.00 : dt.Compute("sum(arsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFoth")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oth)", "")) ? 0.00 : dt.Compute("sum(oth)", ""))).ToString("#,##0;(#,##00); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFtallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tallow)", "")) ? 0.00 : dt.Compute("sum(tallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFgssal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFgspay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gspay)", "")) ? 0.00 : dt.Compute("sum(gspay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFpfund")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFitax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adv)", "")) ? 0.00 : dt.Compute("sum(adv)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFothded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFtded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tdeduc)", "")) ? 0.00 : dt.Compute("sum(tdeduc)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFnetSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "CashSalary":
                    ((Label)this.gvcashpay.FooterRow.FindControl("lgvFTNetmtcash")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "SalLACA":
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFothallo")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othallow)", "")) ? 0.00 : dt.Compute("sum(othallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFothearn")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othearn)", "")) ? 0.00 : dt.Compute("sum(othearn)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFtax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFcellbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mcell)", "")) ? 0.00 : dt.Compute("sum(mcell)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFadvance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adv)", "")) ? 0.00 : dt.Compute("sum(adv)", ""))).ToString("#,##0;(#,##00); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFothded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFloan")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(loanins)", "")) ? 0.00 : dt.Compute("sum(loanins)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFarrear")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arsal)", "")) ? 0.00 : dt.Compute("sum(arsal)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "CashBonus":
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFbSalb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFgssalb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFBonusAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "BonusSummary":
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFNoOfEmp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(noofemp)", "")) ? 0.00 : dt.Compute("sum(noofemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFbSalbsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFgssalbsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFBonusAmtbsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFManAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(manualamt)", "")) ? 0.00 : dt.Compute("sum(manualamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFTotalBonAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalbonamt)", "")) ? 0.00 : dt.Compute("sum(totalbonamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;



                case "SalSummary02":

                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFbankemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankemp)", "")) ? 0.00 : dt.Compute("sum(bankemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFcashemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashemp)", "")) ? 0.00 : dt.Compute("sum(cashemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFtotalemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toemp)", "")) ? 0.00 : dt.Compute("sum(toemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFbankamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFcashamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ? 0.00 : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##00); ");
                    break;
                case "SalSummary02Reg":

                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFbankemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankemp)", "")) ? 0.00 : dt.Compute("sum(bankemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFcashemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashemp)", "")) ? 0.00 : dt.Compute("sum(cashemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFtotalemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toemp)", "")) ? 0.00 : dt.Compute("sum(toemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFbankamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFcashamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalsum02.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ? 0.00 : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##00); ");
                    break;

                case "SalSummaryOV":
                    ((Label)this.gvSummaryOV.FooterRow.FindControl("lgvFempname")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(noemp)", "")) ? 0.00 : dt.Compute("sum(noemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSummaryOV.FooterRow.FindControl("lgvFtvamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tovtdamt)", "")) ? 0.00 : dt.Compute("sum(tovtdamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSummaryOV.FooterRow.FindControl("lgvFovamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ovamt)", "")) ? 0.00 : dt.Compute("sum(ovamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSummaryOV.FooterRow.FindControl("lgvFnetamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ? 0.00 : dt.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "BonusSummary02":

                    ((Label)this.gvbonussam02.FooterRow.FindControl("lgvFBonBankemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankemp)", "")) ? 0.00 : dt.Compute("sum(bankemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbonussam02.FooterRow.FindControl("lgvFBonCashemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashemp)", "")) ? 0.00 : dt.Compute("sum(cashemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbonussam02.FooterRow.FindControl("lgvFBontotalemp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toemp)", "")) ? 0.00 : dt.Compute("sum(toemp)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbonussam02.FooterRow.FindControl("lgvFBonBankamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbonussam02.FooterRow.FindControl("lgvFBonCashamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbonussam02.FooterRow.FindControl("lgvFBonamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ? 0.00 : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##00); ");
                    break;
                case "MonSalSum":
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "grp='A'";
                    dt = dv.ToTable();
                    ((Label)this.gvmonthsalsum.FooterRow.FindControl("lgvFBankPay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankpay)", "")) ? 0.00 : dt.Compute("sum(bankpay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvmonthsalsum.FooterRow.FindControl("lblgvFCashPay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(caspay)", "")) ? 0.00 : dt.Compute("sum(caspay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvmonthsalsum.FooterRow.FindControl("lgvFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(total)", "")) ? 0.00 : dt.Compute("sum(total)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvmonthsalsum.FooterRow.FindControl("lgvFManpower")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(manpower)", "")) ? 0.00 : dt.Compute("sum(manpower)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "SalSummaryEot":
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFNoofEmpeot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(noofemployee)", "")) ? 0.00 : dt.Compute("sum(noofemployee)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFtothours")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalothour)", "")) ? 0.00 : dt.Compute("sum(totalothour)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFbsaleot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFhrenteot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ? 0.00 : dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFconvenceeot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ? 0.00 : dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFmedicaleot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ? 0.00 : dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFfoodeot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(foodalw)", "")) ? 0.00 : dt.Compute("sum(foodalw)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFgrosseot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal1)", "")) ? 0.00 : dt.Compute("sum(gssal1)", ""))).ToString("#,##0;(#,##00); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFtotaleoth")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ohour)", "")) ? 0.00 : dt.Compute("sum(ohour)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFtotaleotamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(otamount)", "")) ? 0.00 : dt.Compute("sum(otamount)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFtoffdayott")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(offdhour)", "")) ? 0.00 : dt.Compute("sum(offdhour)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFtoffdayotamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(offdamount)", "")) ? 0.00 : dt.Compute("sum(offdamount)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFeotdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eotdues)", "")) ? 0.00 : dt.Compute("sum(eotdues)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFoffdotdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(offdotdues)", "")) ? 0.00 : dt.Compute("sum(offdotdues)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFothdeduc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othdeduc)", "")) ? 0.00 : dt.Compute("sum(othdeduc)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFteotamout")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalotamt)", "")) ? 0.00 : dt.Compute("sum(totalotamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEotsalsum.FooterRow.FindControl("lgvFpayable")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tnpayable)", "")) ? 0.00 : dt.Compute("sum(tnpayable)", ""))).ToString("#,##0;(#,##0); ");

                    break;

            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSummary":
                    this.PrintSalSummary();
                    break;

                case "CashSalary":
                    this.PrintCashSalary();
                    break;
                case "SalLACA":
                    this.PrintSalLACA();
                    break;

                case "RPTENVELOP":
                    //this.PrintEnvelop();
                    break;

                case "CashBonus":
                    //this.PrintCashBonous();
                    break;
                case "BonusSummary":
                    this.PrintBonSummary();
                    break;

                case "BonPaySlip":
                    //this.PrintBonPaySlip();
                    break;

                case "SalSummary02":
                    this.PrintSalSummary02();
                    break;
                case "SalSummary02Reg":
                    this.PrintSalSummary02();
                    break;

                case "SalSummaryOV":
                    this.PrintSalSummaryOV();
                    break;

                case "BonusSummary02":
                    //this.PrintBonusSummary02();
                    break;
                case "MonSalSum":
                    this.PrintMonSalSum();
                    break;

                case "SalSummaryEot":
                    this.PrintSalSummaryEot();
                    break;


            }



        }

        protected void gvSalSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalSum.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        private void PrintSalSummaryEot()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comadd = hst["comaddf"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string month = ASTUtility.DateFormat("01." + (ddlMonth.SelectedValue).Substring(4, 2) + "." + (ddlMonth.SelectedValue).Substring(0, 4));
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblSalSum"];
            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.MonthlySalSummaryEOT>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSalSumEOT", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            //Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("txtStuffCateg", "Staff Category : " + this.ddlWstation.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly EOT Requsition (" + Convert.ToDateTime(month).ToString("MMMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintSalSummary()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "5305": //FB
                case "5306": //Footbed
                    this.PrintSalSumFB();
                    break;

                default:
                    this.PrintSalSum();
                    break;
            }
        }
        private void PrintSalSumFB()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comadd = hst["comadd1"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string month = ASTUtility.DateFormat("01." + (ddlMonth.SelectedValue).Substring(4, 2) + "." + (ddlMonth.SelectedValue).Substring(0, 4));
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblSalSum"];
            string comtype = this.ddlWstation.SelectedValue.ToString();
            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.MonthlySalSummary>();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> list1 = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>)Session["hrcompnameadd"];


            comnam = list1.FindAll(l => l.actcode == comtype)[0].hrcomname;
            comadd = list1.FindAll(l => l.actcode == comtype)[0].hrcomadd;

            //comnam = list1[0].hrcomname.ToString();
            //comadd = list1[0].hrcomadd.ToString();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSalarySumFB", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("txtStuffCateg", "Staff Category : " + this.ddlWstation.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Salary Summary " + Convert.ToDateTime(month).ToString("MMMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintMonSalSum()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string month = ASTUtility.DateFormat("01." + (ddlMonth.SelectedValue).Substring(4, 2) + "." + (ddlMonth.SelectedValue).Substring(0, 4));
            string rptitle = (this.ckBonus.Checked == true ? "Eid Bonus" : "Salary");
            DataTable dt = (DataTable)Session["tblSalSum"];
            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.EclassMonthSalSummary>();
            var list2 = list.FindAll(p => p.grp == "A");
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptMonthSalSum", list, list2, null);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("RptTitle", rptitle + " Summary for the Month of " + Convert.ToDateTime(month).ToString("MMMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintSalSum()
        {

        }
        private void PrintSalSummary02()
        {

            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();  //company name
            string comadd = hst["comadd1"].ToString();  //address
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            double toamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ? 0.00 : dt.Compute("sum(toamt)", "")));
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var dt1 = (DataTable)Session["tblSalSum"];

            var list = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.SummarySalarySheet>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSalarySummarySheet01", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rtpTitle", "Salary Summary Sheet"));
            rpt1.SetParameters(new ReportParameter("InWards", "In Word: " + ASTUtility.Trans(toamt, 2)));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintSalSummaryOV()
        {

        }
        private void PrintCashSalary()
        {

        }
        private void PrintSalLACA()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string date = this.GetStdDate("01." + ASTUtility.Right(ddlMonth.SelectedValue, 2) + "." + (ddlMonth.SelectedValue).Substring(0, 4));
            string frmdate = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            DataTable dt = (DataTable)Session["tblSalSum"];
            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptMonSalaryDataSheet>();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptMonthlySalDataSheet", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Loan,Adv,Cell,Arrear Data Sheet"));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintBonSummary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString(); ;
            string username = hst["username"].ToString();
            string comadd = hst["comadd"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(ddlMonth.SelectedValue, 2) + "." + (ddlMonth.SelectedValue).Substring(0, 4));
            string frmdate = Convert.ToDateTime(date).ToString("yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string reporttitle = "Festival Bonus For " + (this.chkBonustype.Checked ? "EID UL-ADHA" : "EID UL-FITR") + " " + frmdate;
            DataTable dt = (DataTable)Session["tblSalSum"];
            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.BonusSummary>();
            double toamt = list.Sum(l => l.totalbonamt);

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptEmpBonusSummaryFB", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("RptTitle", reporttitle));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("print_at", "Report Print: " + System.DateTime.Now.ToString("hh:mm:ss tt")));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("InWards", "In Words: " + ASTUtility.Trans(toamt, 2)));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void gvcashpay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvcashpay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvLACA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLACA.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBonus.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvBonusSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvBonusSum.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvbonussam02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvbonussam02.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvEotsalsum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEotsalsum.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
    }
}