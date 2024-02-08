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
using Microsoft.Reporting.WinForms;      
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_11_RawInv
{
    public partial class RptMaterialTrans : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetStoreName();
                this.GetSupplier();
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Periodic Material Transfer Report";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        protected void GetStoreName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string HeaderCode = "15%";
            string filter = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", HeaderCode, filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            dt1.Rows.Add(comcod, "000000000000", "All", "All");
            this.DdlStoreAcc.DataSource = dt1;
            this.DdlStoreAcc.DataTextField = "actdesc1";
            this.DdlStoreAcc.DataValueField = "actcode";
            this.DdlStoreAcc.DataBind();

        }

        private void GetSupplier()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string txtSupplier = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_MARKETSERVEY", "GETPSNAME", txtSupplier, "", "", "", "", "", "", "", "");

            DataRow dr = ds1.Tables[0].NewRow();
            dr["sirdesc1"] = "All";
            dr["sircode"] = "000000000000";
            ds1.Tables[0].Rows.Add(dr);

            this.ddlSupplier.DataTextField = "sirdesc1";
            this.ddlSupplier.DataValueField = "sircode";
            this.ddlSupplier.DataSource = ds1.Tables[0];
            this.ddlSupplier.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MatTransfer":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetDataTransferRpt();
                    this.FooterCalculation();
                    break;
            }
        }

        private void GetDataTransferRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string actcode = this.DdlStoreAcc.SelectedValue.ToString() == "000000000000" ? "%" : this.DdlStoreAcc.SelectedValue.ToString() + "%";
            string supplier = this.ddlSupplier.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSupplier.SelectedValue.ToString() + "%";
            string summary = (this.ChbxSummary.Checked == true) ? "summary" : "";

            if(summary == "summary")
            {
                this.cellSummary2.Visible = true;
            }
            else
            {
                this.cellSummary2.Visible = false;
            }

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_TRANSFER_INTERFACE", "GET_DATE_WISE_TRANSFER_INFO", date1, date2, actcode, summary, supplier);
            Session["tblVeiw"] = ds2.Tables[0];

            // UPDATED BY AREFIN 10:07 AM 12/18/2023
            string type = this.ddlType.SelectedValue;

            if (ds2.Tables.Count > 1)
            {
                DataTable dt2 = ds2.Tables[1].Copy();
                DataView dv2 = dt2.DefaultView;
                dv2.RowFilter = "substring(mtreqno,1,3) like '" + type + "'";
                dt2 = dv2.ToTable();

                ViewState["SummaryView2"] = dt2;
            }

            DataTable dt1 = ds2.Tables[0].Copy();
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "substring(mtreqno,1,3) like '"+ type + "'";
            dt1 = dv1.ToTable();

            Session["tblVeiw"] = dt1;

            this.Data_Bind();
        }

        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MatTransfer":

                    this.gvMatTransfer.Columns[0].Visible = false;

                    this.gvMatTransfer.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMatTransfer.DataSource = (DataTable)Session["tblVeiw"]; 
                    this.gvMatTransfer.DataBind();

                    this.gvSummary2.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSummary2.DataSource = (DataTable)ViewState["SummaryView2"];
                    this.gvSummary2.DataBind();

                    this.FooterCalculation();
                    break;
            }

            //this.FooterCalculation((DataTable)Session["tblVeiw"]);
        }

        protected void gvMatTransfer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatTransfer.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void ChbxSummary_CheckedChanged(object sender, EventArgs e)
        {
            
            if (ChbxSummary.Checked)
            {
                this.lbtnOk_Click(null, null);
                this.gvMatTransfer.Columns[3].Visible = false;
                this.gvMatTransfer.Columns[4].Visible = false;
                this.gvMatTransfer.Columns[7].Visible = false;
            }
            else
            {
                this.lbtnOk_Click(null, null);
                this.gvMatTransfer.Columns[3].Visible = true;
                this.gvMatTransfer.Columns[4].Visible = true;
                this.gvMatTransfer.Columns[7].Visible = true;
            }

        }

        protected void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblVeiw"];

            if (dt.Rows.Count > 0)
            {
                double TtlQty = Convert.ToDouble(dt.Compute("SUM(qty)", string.Empty));
                double TtlGPQty = Convert.ToDouble(dt.Compute("SUM(gqty)", string.Empty));
                double TransQty = Convert.ToDouble(dt.Compute("SUM(tqty)", string.Empty));
                double TransBal = Convert.ToDouble(dt.Compute("SUM(trbalqty)", string.Empty));
                double ItmCnt = Convert.ToDouble(dt.Compute("SUM(itmcount)", string.Empty));

                ((Label)(this.gvMatTransfer.FooterRow.FindControl("gvTtlQty"))).Text = TtlQty.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvMatTransfer.FooterRow.FindControl("gvTtlGPQty"))).Text = TtlGPQty.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvMatTransfer.FooterRow.FindControl("gvTransQty"))).Text = TransQty.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvMatTransfer.FooterRow.FindControl("gvTransBal"))).Text = TransBal.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvMatTransfer.FooterRow.FindControl("gvItmCnt"))).Text = ItmCnt.ToString("#,##0;(#,##0); ");   
            }

            if (this.cellSummary2.Visible == true)
            {
                DataTable dt2 = (DataTable)ViewState["SummaryView2"];

                if (dt2.Rows.Count > 0)
                {
                    double TtlQty2 = Convert.ToDouble(dt2.Compute("SUM(qty)", string.Empty));
                    double TtlGPQty2 = Convert.ToDouble(dt2.Compute("SUM(gqty)", string.Empty));
                    double TransQty2 = Convert.ToDouble(dt2.Compute("SUM(tqty)", string.Empty));
                    double TransBal2 = Convert.ToDouble(dt2.Compute("SUM(trbalqty)", string.Empty));

                    ((Label)(this.gvSummary2.FooterRow.FindControl("gvTtlQty2"))).Text = TtlQty2.ToString("#,##0;(#,##0); ");
                    ((Label)(this.gvSummary2.FooterRow.FindControl("gvTtlGPQty2"))).Text = TtlGPQty2.ToString("#,##0;(#,##0); ");
                    ((Label)(this.gvSummary2.FooterRow.FindControl("gvTransQty2"))).Text = TransQty2.ToString("#,##0;(#,##0); ");
                    ((Label)(this.gvSummary2.FooterRow.FindControl("gvTransBal2"))).Text = TransBal2.ToString("#,##0.00;(#,##0.00); ");
                }
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MatTransfer":
                    this.MatTransferRptPrint();
                    break;
            }
        }

        private void MatTransferRptPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadrs = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string fdate = this.txtDatefrom.Text.Substring(0, 11);
            string tdate = this.txtDateto.Text.Substring(0, 11);
            string date = Convert.ToString((fdate) + " To " + (tdate));
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string actcode = this.DdlStoreAcc.SelectedValue.ToString() == "" ? "%" : this.DdlStoreAcc.SelectedValue.ToString() + "%";
            //string summary = (this.ChbxSummary.Checked == true) ? "summary" : "";
            //string DescType = "";

            DataTable dt = (DataTable)Session["tblVeiw"];
            var list = dt.DataTableToList<SPEENTITY.C_11_RawInv.RptMatTransfer>();
            LocalReport rpt1 = new LocalReport();

            if (this.ChbxSummary.Checked == true)
            {
                rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPeriodMatTransfer", list, null, null);

            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptPeriodMatTransDetails", list, null, null);

            }

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadrs));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Periodic Material Transfer Report"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

