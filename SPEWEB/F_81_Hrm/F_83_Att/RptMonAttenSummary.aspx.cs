using Microsoft.Reporting.WinForms;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class RptMonAttenSummary : System.Web.UI.Page
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

                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "DayWise") ? "Monthly Attn. Count Summary (Day Wise)" :
                                                                    (type == "LineWise") ? "Monthly Attn. Count Summary (Line Wise)" : 
                                                                    "Monthly Attendance Summary";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetWorkStation();
                this.GetDivision();
                this.GetDeptList();
                this.GetSectionList();
                this.GetJobLocation();
                this.SelectType();
                this.GetLineddl();
                this.GetJobLocation();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
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
                case "DayWise":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "LineWise":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                default:
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

            this.ShowData();

        }

        private void ShowData()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "DayWise":
                    if (this.lnkbtnShow.Text == "New")
                    {
                        this.lnkbtnShow.Text = "Ok";
                        this.gvMonAttnCountSum.DataSource = null;
                        this.gvMonAttnCountSum.DataBind();
                        return;

                    }

                    this.lnkbtnShow.Text = "New";
                    this.ShowMonAttnCountSumDayWise();
                    break;

                case "LineWise":
                    if (this.lnkbtnShow.Text == "New")
                    {
                        this.lnkbtnShow.Text = "Ok";
                        this.gvMonAttnSumLineWise.DataSource = null;
                        this.gvMonAttnSumLineWise.DataBind();
                        return;

                    }

                    this.lnkbtnShow.Text = "New";
                    this.ShowMonAttnCountSumLineWise();
                    break;

                default:
                    break;


            }


        }

        private void ShowMonAttnCountSumLineWise()
        {
            Session.Remove("attnsummary");
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
            DataSet ds4 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE_SUMMARY", "MONTHLY_ATTN_COUNTSUM_LINEWISE", null, null, null, frmdate, todate, EmpType,
                    Division, Department, section, line, joblocation, userid, "", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvMonAttnSumLineWise.DataSource = null;
                this.gvMonAttnSumLineWise.DataBind();
                return;

            }

            Session["attnsummary"] = ds4.Tables[0];
            Session["tblsecname"] = ds4.Tables[1];
            this.LoadGrid();
        }

        private void ShowMonAttnCountSumDayWise()
        {
            Session.Remove("attnsummary");
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
            DataSet ds4 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE_SUMMARY", "MONTHLY_ATTN_COUNTSUM_DAYWISE", null, null, null, frmdate, todate, EmpType,
                    Division, Department, section, line, joblocation, userid, "", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvMonAttnCountSum.DataSource = null;
                this.gvMonAttnCountSum.DataBind();
                return;

            }

            Session["attnsummary"] = ds4.Tables[0];
            this.LoadGrid();
        }
        private void LoadGrid()
        {
            try
            {
                DataTable dt = (DataTable)Session["attnsummary"];

                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {

                    case "DayWise":
                        this.MultiView1.ActiveViewIndex = 0;
                        this.gvMonAttnCountSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvMonAttnCountSum.DataSource = dt;
                        this.gvMonAttnCountSum.DataBind();
                        this.FooterCalculation();

                        if (dt.Rows.Count == 0)
                            return;
                        Session["Report1"] = gvMonAttnCountSum;
                        ((HyperLink)this.gvMonAttnCountSum.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;

                    case "LineWise":
                        DataTable dt1 = (DataTable)Session["tblsecname"];
                        this.MultiView1.ActiveViewIndex = 1;
                        this.gvMonAttnSumLineWise.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        int i = 0;
                        for (i = 2; i < this.gvMonAttnSumLineWise.Columns.Count; i++)
                        {
                            if (i == 64)
                                break;
                            this.gvMonAttnSumLineWise.Columns[i].Visible = false;
                        }

                        int j = 2;
                        for (i = 0; i < dt1.Rows.Count; i++)
                        {
                            this.gvMonAttnSumLineWise.Columns[j].Visible = true;
                            this.gvMonAttnSumLineWise.Columns[j].HeaderText = dt1.Rows[i]["linedesc"].ToString();
                            j++;
                            if (j > 64)
                                break;
                        }

                        this.gvMonAttnSumLineWise.DataSource = dt;
                        this.gvMonAttnSumLineWise.DataBind();
                        this.FooterCalculation();

                        if (dt.Rows.Count == 0)
                            return;
                        Session["Report1"] = gvMonAttnSumLineWise;
                        ((HyperLink)this.gvMonAttnSumLineWise.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
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
                DataTable dt = (DataTable)Session["attnsummary"];

                if (dt.Rows.Count == 0)
                    return;
                string type = this.Request.QueryString["Type"].ToString().Trim();

                switch (type)
                {
                    case "DayWise":
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p1)", "")) ? 0 : dt.Compute("sum(p1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p2)", "")) ? 0 : dt.Compute("sum(p2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p3)", "")) ? 0 : dt.Compute("sum(p3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p4)", "")) ? 0 : dt.Compute("sum(p4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p5)", "")) ? 0 : dt.Compute("sum(p5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p6)", "")) ? 0 : dt.Compute("sum(p6)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p7)", "")) ? 0 : dt.Compute("sum(p7)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p8)", "")) ? 0 : dt.Compute("sum(p8)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p9)", "")) ? 0 : dt.Compute("sum(p9)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p10)", "")) ? 0 : dt.Compute("sum(p10)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p11)", "")) ? 0 : dt.Compute("sum(p11)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p12)", "")) ? 0 : dt.Compute("sum(p12)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p13)", "")) ? 0 : dt.Compute("sum(p13)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p14)", "")) ? 0 : dt.Compute("sum(p14)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p15)", "")) ? 0 : dt.Compute("sum(p15)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p16)", "")) ? 0 : dt.Compute("sum(p16)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p17)", "")) ? 0 : dt.Compute("sum(p17)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p18)", "")) ? 0 : dt.Compute("sum(p18)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p19)", "")) ? 0 : dt.Compute("sum(p19)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p20)", "")) ? 0 : dt.Compute("sum(p20)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP21")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p21)", "")) ? 0 : dt.Compute("sum(p21)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP22")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p22)", "")) ? 0 : dt.Compute("sum(p22)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP23")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p23)", "")) ? 0 : dt.Compute("sum(p23)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP24")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p24)", "")) ? 0 : dt.Compute("sum(p24)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP25")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p25)", "")) ? 0 : dt.Compute("sum(p25)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP26")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p26)", "")) ? 0 : dt.Compute("sum(p26)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP27")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p27)", "")) ? 0 : dt.Compute("sum(p27)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP28")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p28)", "")) ? 0 : dt.Compute("sum(p28)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP29")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p29)", "")) ? 0 : dt.Compute("sum(p29)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p30)", "")) ? 0 : dt.Compute("sum(p30)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFP31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p31)", "")) ? 0 : dt.Compute("sum(p31)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvTotalFPresent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalprsnt)", "")) ? 0 : dt.Compute("sum(totalprsnt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a1)", "")) ? 0 : dt.Compute("sum(a1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a2)", "")) ? 0 : dt.Compute("sum(a2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a3)", "")) ? 0 : dt.Compute("sum(a3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a4)", "")) ? 0 : dt.Compute("sum(a4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a5)", "")) ? 0 : dt.Compute("sum(a5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a6)", "")) ? 0 : dt.Compute("sum(a6)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a7)", "")) ? 0 : dt.Compute("sum(a7)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a8)", "")) ? 0 : dt.Compute("sum(a8)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a9)", "")) ? 0 : dt.Compute("sum(a9)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a10)", "")) ? 0 : dt.Compute("sum(a10)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a11)", "")) ? 0 : dt.Compute("sum(a11)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a12)", "")) ? 0 : dt.Compute("sum(a12)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a13)", "")) ? 0 : dt.Compute("sum(a13)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a14)", "")) ? 0 : dt.Compute("sum(a14)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a15)", "")) ? 0 : dt.Compute("sum(a15)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a16)", "")) ? 0 : dt.Compute("sum(a16)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a17)", "")) ? 0 : dt.Compute("sum(a17)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a18)", "")) ? 0 : dt.Compute("sum(a18)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a19)", "")) ? 0 : dt.Compute("sum(a19)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a20)", "")) ? 0 : dt.Compute("sum(a20)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA21")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a21)", "")) ? 0 : dt.Compute("sum(a21)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA22")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a22)", "")) ? 0 : dt.Compute("sum(a22)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA23")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a23)", "")) ? 0 : dt.Compute("sum(a23)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA24")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a24)", "")) ? 0 : dt.Compute("sum(a24)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA25")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a25)", "")) ? 0 : dt.Compute("sum(a25)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA26")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a26)", "")) ? 0 : dt.Compute("sum(a26)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA27")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a27)", "")) ? 0 : dt.Compute("sum(a27)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA28")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a28)", "")) ? 0 : dt.Compute("sum(a28)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA29")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a29)", "")) ? 0 : dt.Compute("sum(a29)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a30)", "")) ? 0 : dt.Compute("sum(a30)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFA31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a31)", "")) ? 0 : dt.Compute("sum(a31)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvTotalFAbsent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalabsnt)", "")) ? 0 : dt.Compute("sum(totalabsnt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l1)", "")) ? 0 : dt.Compute("sum(l1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l2)", "")) ? 0 : dt.Compute("sum(l2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l3)", "")) ? 0 : dt.Compute("sum(l3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l4)", "")) ? 0 : dt.Compute("sum(l4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l5)", "")) ? 0 : dt.Compute("sum(l5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l6)", "")) ? 0 : dt.Compute("sum(l6)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l7)", "")) ? 0 : dt.Compute("sum(l7)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l8)", "")) ? 0 : dt.Compute("sum(l8)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l9)", "")) ? 0 : dt.Compute("sum(l9)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l10)", "")) ? 0 : dt.Compute("sum(l10)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l11)", "")) ? 0 : dt.Compute("sum(l11)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l12)", "")) ? 0 : dt.Compute("sum(l12)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l13)", "")) ? 0 : dt.Compute("sum(l13)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l14)", "")) ? 0 : dt.Compute("sum(l14)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l15)", "")) ? 0 : dt.Compute("sum(l15)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l16)", "")) ? 0 : dt.Compute("sum(l16)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l17)", "")) ? 0 : dt.Compute("sum(l17)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l18)", "")) ? 0 : dt.Compute("sum(l18)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l19)", "")) ? 0 : dt.Compute("sum(l19)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l20)", "")) ? 0 : dt.Compute("sum(l20)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL21")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l21)", "")) ? 0 : dt.Compute("sum(l21)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL22")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l22)", "")) ? 0 : dt.Compute("sum(l22)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL23")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l23)", "")) ? 0 : dt.Compute("sum(l23)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL24")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l24)", "")) ? 0 : dt.Compute("sum(l24)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL25")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l25)", "")) ? 0 : dt.Compute("sum(l25)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL26")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l26)", "")) ? 0 : dt.Compute("sum(l26)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL27")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l27)", "")) ? 0 : dt.Compute("sum(l27)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL28")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l28)", "")) ? 0 : dt.Compute("sum(l28)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL29")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l29)", "")) ? 0 : dt.Compute("sum(l29)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l30)", "")) ? 0 : dt.Compute("sum(l30)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvFL31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l31)", "")) ? 0 : dt.Compute("sum(l31)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnCountSum.FooterRow.FindControl("lgvTotalFLeave")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalleave)", "")) ? 0 : dt.Compute("sum(totalleave)", ""))).ToString("#,##0;(#,##0); ");
                        break;


                    case "LineWise":
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p1)", "")) ? 0 : dt.Compute("sum(p1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p2)", "")) ? 0 : dt.Compute("sum(p2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p3)", "")) ? 0 : dt.Compute("sum(p3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p4)", "")) ? 0 : dt.Compute("sum(p4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p5)", "")) ? 0 : dt.Compute("sum(p5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p6)", "")) ? 0 : dt.Compute("sum(p6)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p7)", "")) ? 0 : dt.Compute("sum(p7)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p8)", "")) ? 0 : dt.Compute("sum(p8)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p9)", "")) ? 0 : dt.Compute("sum(p9)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p10)", "")) ? 0 : dt.Compute("sum(p10)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p11)", "")) ? 0 : dt.Compute("sum(p11)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p12)", "")) ? 0 : dt.Compute("sum(p12)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p13)", "")) ? 0 : dt.Compute("sum(p13)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p14)", "")) ? 0 : dt.Compute("sum(p14)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p15)", "")) ? 0 : dt.Compute("sum(p15)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p16)", "")) ? 0 : dt.Compute("sum(p16)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p17)", "")) ? 0 : dt.Compute("sum(p17)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p18)", "")) ? 0 : dt.Compute("sum(p18)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p19)", "")) ? 0 : dt.Compute("sum(p19)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p20)", "")) ? 0 : dt.Compute("sum(p20)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP21")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p21)", "")) ? 0 : dt.Compute("sum(p21)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP22")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p22)", "")) ? 0 : dt.Compute("sum(p22)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP23")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p23)", "")) ? 0 : dt.Compute("sum(p23)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP24")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p24)", "")) ? 0 : dt.Compute("sum(p24)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP25")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p25)", "")) ? 0 : dt.Compute("sum(p25)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP26")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p26)", "")) ? 0 : dt.Compute("sum(p26)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP27")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p27)", "")) ? 0 : dt.Compute("sum(p27)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP28")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p28)", "")) ? 0 : dt.Compute("sum(p28)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP29")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p29)", "")) ? 0 : dt.Compute("sum(p29)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p30)", "")) ? 0 : dt.Compute("sum(p30)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p31)", "")) ? 0 : dt.Compute("sum(p31)", ""))).ToString("#,##0;(#,##0); ");
                        
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP32")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p32)", "")) ? 0 : dt.Compute("sum(p32)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP33")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p33)", "")) ? 0 : dt.Compute("sum(p33)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP34")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p34)", "")) ? 0 : dt.Compute("sum(p34)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP35")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p35)", "")) ? 0 : dt.Compute("sum(p35)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP36")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p36)", "")) ? 0 : dt.Compute("sum(p36)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP37")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p37)", "")) ? 0 : dt.Compute("sum(p37)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP38")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p38)", "")) ? 0 : dt.Compute("sum(p38)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP39")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p39)", "")) ? 0 : dt.Compute("sum(p39)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP40")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p40)", "")) ? 0 : dt.Compute("sum(p40)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP41")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p41)", "")) ? 0 : dt.Compute("sum(p41)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP42")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p42)", "")) ? 0 : dt.Compute("sum(p42)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP43")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p43)", "")) ? 0 : dt.Compute("sum(p43)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP44")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p44)", "")) ? 0 : dt.Compute("sum(p44)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP45")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p45)", "")) ? 0 : dt.Compute("sum(p45)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP46")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p46)", "")) ? 0 : dt.Compute("sum(p46)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP47")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p47)", "")) ? 0 : dt.Compute("sum(p47)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP48")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p48)", "")) ? 0 : dt.Compute("sum(p48)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP49")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p49)", "")) ? 0 : dt.Compute("sum(p49)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP50")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p50)", "")) ? 0 : dt.Compute("sum(p50)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP51")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p51)", "")) ? 0 : dt.Compute("sum(p51)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP52")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p52)", "")) ? 0 : dt.Compute("sum(p52)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP53")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p53)", "")) ? 0 : dt.Compute("sum(p53)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP54")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p54)", "")) ? 0 : dt.Compute("sum(p54)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP55")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p55)", "")) ? 0 : dt.Compute("sum(p55)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP56")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p56)", "")) ? 0 : dt.Compute("sum(p56)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP57")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p57)", "")) ? 0 : dt.Compute("sum(p57)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP58")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p58)", "")) ? 0 : dt.Compute("sum(p58)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP59")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p59)", "")) ? 0 : dt.Compute("sum(p59)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP60")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p60)", "")) ? 0 : dt.Compute("sum(p60)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP61")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p61)", "")) ? 0 : dt.Compute("sum(p61)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFP62")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(p62)", "")) ? 0 : dt.Compute("sum(p62)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvTotalFPresent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalprsnt)", "")) ? 0 : dt.Compute("sum(totalprsnt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a1)", "")) ? 0 : dt.Compute("sum(a1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a2)", "")) ? 0 : dt.Compute("sum(a2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a3)", "")) ? 0 : dt.Compute("sum(a3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a4)", "")) ? 0 : dt.Compute("sum(a4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a5)", "")) ? 0 : dt.Compute("sum(a5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a6)", "")) ? 0 : dt.Compute("sum(a6)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a7)", "")) ? 0 : dt.Compute("sum(a7)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a8)", "")) ? 0 : dt.Compute("sum(a8)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a9)", "")) ? 0 : dt.Compute("sum(a9)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a10)", "")) ? 0 : dt.Compute("sum(a10)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a11)", "")) ? 0 : dt.Compute("sum(a11)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a12)", "")) ? 0 : dt.Compute("sum(a12)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a13)", "")) ? 0 : dt.Compute("sum(a13)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a14)", "")) ? 0 : dt.Compute("sum(a14)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a15)", "")) ? 0 : dt.Compute("sum(a15)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a16)", "")) ? 0 : dt.Compute("sum(a16)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a17)", "")) ? 0 : dt.Compute("sum(a17)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a18)", "")) ? 0 : dt.Compute("sum(a18)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a19)", "")) ? 0 : dt.Compute("sum(a19)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a20)", "")) ? 0 : dt.Compute("sum(a20)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA21")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a21)", "")) ? 0 : dt.Compute("sum(a21)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA22")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a22)", "")) ? 0 : dt.Compute("sum(a22)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA23")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a23)", "")) ? 0 : dt.Compute("sum(a23)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA24")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a24)", "")) ? 0 : dt.Compute("sum(a24)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA25")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a25)", "")) ? 0 : dt.Compute("sum(a25)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA26")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a26)", "")) ? 0 : dt.Compute("sum(a26)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA27")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a27)", "")) ? 0 : dt.Compute("sum(a27)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA28")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a28)", "")) ? 0 : dt.Compute("sum(a28)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA29")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a29)", "")) ? 0 : dt.Compute("sum(a29)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a30)", "")) ? 0 : dt.Compute("sum(a30)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a31)", "")) ? 0 : dt.Compute("sum(a31)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA32")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a32)", "")) ? 0 : dt.Compute("sum(a32)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA33")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a33)", "")) ? 0 : dt.Compute("sum(a33)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA34")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a34)", "")) ? 0 : dt.Compute("sum(a34)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA35")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a35)", "")) ? 0 : dt.Compute("sum(a35)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA36")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a36)", "")) ? 0 : dt.Compute("sum(a36)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA37")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a37)", "")) ? 0 : dt.Compute("sum(a37)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA38")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a38)", "")) ? 0 : dt.Compute("sum(a38)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA39")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a39)", "")) ? 0 : dt.Compute("sum(a39)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA40")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a40)", "")) ? 0 : dt.Compute("sum(a40)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA41")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a41)", "")) ? 0 : dt.Compute("sum(a41)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA42")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a42)", "")) ? 0 : dt.Compute("sum(a42)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA43")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a43)", "")) ? 0 : dt.Compute("sum(a43)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA44")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a44)", "")) ? 0 : dt.Compute("sum(a44)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA45")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a45)", "")) ? 0 : dt.Compute("sum(a45)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA46")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a46)", "")) ? 0 : dt.Compute("sum(a46)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA47")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a47)", "")) ? 0 : dt.Compute("sum(a47)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA48")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a48)", "")) ? 0 : dt.Compute("sum(a48)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA49")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a49)", "")) ? 0 : dt.Compute("sum(a49)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA50")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a50)", "")) ? 0 : dt.Compute("sum(a50)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA51")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a51)", "")) ? 0 : dt.Compute("sum(a51)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA52")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a52)", "")) ? 0 : dt.Compute("sum(a52)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA53")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a53)", "")) ? 0 : dt.Compute("sum(a53)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA54")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a54)", "")) ? 0 : dt.Compute("sum(a54)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA55")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a55)", "")) ? 0 : dt.Compute("sum(a55)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA56")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a56)", "")) ? 0 : dt.Compute("sum(a56)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA57")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a57)", "")) ? 0 : dt.Compute("sum(a57)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA58")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a58)", "")) ? 0 : dt.Compute("sum(a58)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA59")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a59)", "")) ? 0 : dt.Compute("sum(a59)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA60")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a60)", "")) ? 0 : dt.Compute("sum(a60)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA61")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a61)", "")) ? 0 : dt.Compute("sum(a61)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFA62")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(a62)", "")) ? 0 : dt.Compute("sum(a62)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvTotalFAbsent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalabsnt)", "")) ? 0 : dt.Compute("sum(totalabsnt)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l1)", "")) ? 0 : dt.Compute("sum(l1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l2)", "")) ? 0 : dt.Compute("sum(l2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l3)", "")) ? 0 : dt.Compute("sum(l3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l4)", "")) ? 0 : dt.Compute("sum(l4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l5)", "")) ? 0 : dt.Compute("sum(l5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l6)", "")) ? 0 : dt.Compute("sum(l6)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l7)", "")) ? 0 : dt.Compute("sum(l7)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l8)", "")) ? 0 : dt.Compute("sum(l8)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l9)", "")) ? 0 : dt.Compute("sum(l9)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l10)", "")) ? 0 : dt.Compute("sum(l10)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l11)", "")) ? 0 : dt.Compute("sum(l11)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l12)", "")) ? 0 : dt.Compute("sum(l12)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l13)", "")) ? 0 : dt.Compute("sum(l13)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l14)", "")) ? 0 : dt.Compute("sum(l14)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l15)", "")) ? 0 : dt.Compute("sum(l15)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l16)", "")) ? 0 : dt.Compute("sum(l16)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l17)", "")) ? 0 : dt.Compute("sum(l17)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l18)", "")) ? 0 : dt.Compute("sum(l18)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l19)", "")) ? 0 : dt.Compute("sum(l19)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l20)", "")) ? 0 : dt.Compute("sum(l20)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL21")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l21)", "")) ? 0 : dt.Compute("sum(l21)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL22")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l22)", "")) ? 0 : dt.Compute("sum(l22)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL23")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l23)", "")) ? 0 : dt.Compute("sum(l23)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL24")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l24)", "")) ? 0 : dt.Compute("sum(l24)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL25")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l25)", "")) ? 0 : dt.Compute("sum(l25)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL26")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l26)", "")) ? 0 : dt.Compute("sum(l26)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL27")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l27)", "")) ? 0 : dt.Compute("sum(l27)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL28")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l28)", "")) ? 0 : dt.Compute("sum(l28)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL29")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l29)", "")) ? 0 : dt.Compute("sum(l29)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l30)", "")) ? 0 : dt.Compute("sum(l30)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l31)", "")) ? 0 : dt.Compute("sum(l31)", ""))).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL32")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l32)", "")) ? 0 : dt.Compute("sum(l32)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL33")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l33)", "")) ? 0 : dt.Compute("sum(l33)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL34")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l34)", "")) ? 0 : dt.Compute("sum(l34)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL35")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l35)", "")) ? 0 : dt.Compute("sum(l35)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL36")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l36)", "")) ? 0 : dt.Compute("sum(l36)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL37")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l37)", "")) ? 0 : dt.Compute("sum(l37)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL38")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l38)", "")) ? 0 : dt.Compute("sum(l38)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL39")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l39)", "")) ? 0 : dt.Compute("sum(l39)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL40")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l40)", "")) ? 0 : dt.Compute("sum(l40)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL41")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l41)", "")) ? 0 : dt.Compute("sum(l41)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL42")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l42)", "")) ? 0 : dt.Compute("sum(l42)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL43")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l43)", "")) ? 0 : dt.Compute("sum(l43)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL44")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l44)", "")) ? 0 : dt.Compute("sum(l44)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL45")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l45)", "")) ? 0 : dt.Compute("sum(l45)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL46")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l46)", "")) ? 0 : dt.Compute("sum(l46)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL47")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l47)", "")) ? 0 : dt.Compute("sum(l47)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL48")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l48)", "")) ? 0 : dt.Compute("sum(l48)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL49")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l49)", "")) ? 0 : dt.Compute("sum(l49)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL50")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l50)", "")) ? 0 : dt.Compute("sum(l50)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL51")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l51)", "")) ? 0 : dt.Compute("sum(l51)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL52")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l52)", "")) ? 0 : dt.Compute("sum(l52)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL53")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l53)", "")) ? 0 : dt.Compute("sum(l53)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL54")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l54)", "")) ? 0 : dt.Compute("sum(l54)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL55")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l55)", "")) ? 0 : dt.Compute("sum(l55)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL56")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l56)", "")) ? 0 : dt.Compute("sum(l56)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL57")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l57)", "")) ? 0 : dt.Compute("sum(l57)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL58")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l58)", "")) ? 0 : dt.Compute("sum(l58)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL59")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l59)", "")) ? 0 : dt.Compute("sum(l59)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL60")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l60)", "")) ? 0 : dt.Compute("sum(l60)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL61")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l61)", "")) ? 0 : dt.Compute("sum(l61)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvFL62")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(l62)", "")) ? 0 : dt.Compute("sum(l62)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvMonAttnSumLineWise.FooterRow.FindControl("lgvTotalFLeave")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalleave)", "")) ? 0 : dt.Compute("sum(totalleave)", ""))).ToString("#,##0;(#,##0); ");
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
                case "DayWise":
                    this.PrintMonAttnSumDayWise();
                    break;

                case "LineWise":
                    this.PrintMonAttnSumLineWise();
                    break;


            }
        }

        private void PrintMonAttnSumLineWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~/Image/LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["attnsummary"];

            var lst1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptMonAttnCountSum>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMonAttnCountSumSecWise", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("date", "Date: " + date));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Present, Absent & Leave Summary (Section Wise)"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintMonAttnSumDayWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~/Image/LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["attnsummary"];

            var lst1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptMonAttnCountSum>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMonAttnCountSumSecWise", lst1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("date", "Date: " + date));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Present, Absent & Leave Summary (Section Wise)"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.LoadGrid();
        }
        private void GetWorkStation()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
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
        protected void gvMonAttnCountSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvMonAttnCountSum.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvMonAttnSumLineWise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvMonAttnSumLineWise.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
    }
}