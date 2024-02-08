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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_85_Lon
{
    public partial class RptSalarySummary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess("ASITHRM");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.GetCompany();
                this.SelectType();
            }
        }


        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Salary":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
            }

        }

        private void GetCompany()
        {

            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo("", "SP_REPORT_PAYROLL01", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            //----------- Date
            this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
            this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            //----------
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

                case "SalSum":
                    //this.EmpCashPay();
                    this.loadSalSum();
                    this.LoadGrid();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
            }
        }
        protected void loadSalSum()
        {
            //  load data into salsum

            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string curmonhtid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMM");
            string prmonthid = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("yyyyMM");
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            DataSet ds3 = HRData.GetTransInfo("", "SP_REPORT_PAYROLL01", "RPT_SALSUM", CompanyName, curmonhtid, prmonthid, "", "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvSalSum.DataSource = null;
                this.gvSalSum.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            Session["tblSalSum"] = dt;

        }

        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tblSalSum"];

            gvSalSum.Columns[1].HeaderText = (this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "Company Name" : "Department Name";

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSum":
                    //this.gvcashpay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());


                    this.gvSalSum.DataSource = dt;
                    this.gvSalSum.DataBind();
                    this.FooterCalculation();
                    break;
            }

        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSum":
                    // this.gvSalSum.HeaderRow.FindControl()
                    //lgvHTCurMamt
                    //lgvHTPreMamt
                    //lgvHTPreMEmp
                    //lgvHTCurMEmp
                    string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM-yy");
                    string premonth = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("MMM-yy");
                    ((Label)this.gvSalSum.HeaderRow.FindControl("lgvHTCurMEmp")).Text = frmdate + "</br>" + "Emp.";
                    ((Label)this.gvSalSum.HeaderRow.FindControl("lgvHTCurMamt")).Text = frmdate + "</br>" + "Amount";
                    ((Label)this.gvSalSum.HeaderRow.FindControl("lgvHTPreMEmp")).Text = premonth + "</br>" + "Emp.";
                    ((Label)this.gvSalSum.HeaderRow.FindControl("lgvHTPreMamt")).Text = premonth + "</br>" + "Amount";
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFTCurEmp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curempno)", "")) ? 0.00 : dt.Compute("sum(curempno)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFTCurMamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curpay)", "")) ? 0.00 : dt.Compute("sum(curpay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFTPreEmp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(preempno)", "")) ? 0.00 : dt.Compute("sum(preempno)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFTPreMamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(prepay)", "")) ? 0.00 : dt.Compute("sum(prepay)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "SalSum":
                    this.PrintSalSum();
                    break;
            }

        }

        protected void imgbtnCompany_Click(object sender, ImageClickEventArgs e)
        {
            this.GetCompany();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.SaveValue();
            this.LoadGrid();
        }

        protected void gvcashpay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvSalSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalSum.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }
        protected void PrintSalSum()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string namedep = (this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "Company Name" : "Department Name";
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM-yy");
            string premonth = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("MMM-yy");
            ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_89_Pay.RptSalSumm();

            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
            TextObject comnamedep = rpcp.ReportDefinition.ReportObjects["namedep"] as TextObject;
            comnamedep.Text = namedep;
            TextObject txtCurMonth = rpcp.ReportDefinition.ReportObjects["txtCurMonth"] as TextObject;
            txtCurMonth.Text = frmdate;
            TextObject txtPreMonth = rpcp.ReportDefinition.ReportObjects["txtPreMonth"] as TextObject;
            txtPreMonth.Text = premonth;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rpcp.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            this.lblprint.Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                             this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}