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
using SPERDLC;


namespace SPEWEB.F_15_Pro
{
    public partial class RptOrdrStk : System.Web.UI.Page
    {
        ProcessAccess MatStk = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfrmdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetOrderNo();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIALS STOCK ANALYIS";

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetOrderNo()
        {
            string comcod = GetComCode();
            string txtsrch = this.txtOrdsrch.Text.Trim();
            DataSet ds1 = MatStk.GetTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "GETORDERNO", txtsrch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlOrder.DataTextField = "actdesc";
            this.ddlOrder.DataValueField = "actcode";
            this.ddlOrder.DataSource = ds1.Tables[0];
            this.ddlOrder.DataBind();
        }
        protected void imgbtnFindOrd_Click(object sender, EventArgs e)
        {
            this.GetOrderNo();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblstock");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = this.GetComCode();
            string OrdrNo = this.ddlOrder.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = MatStk.GetTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "RPTORDSTOCK", OrdrNo, frmdate, todate, "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblstock"] = ds2.Tables[0];
            this.LoadGrid();
        }

        private void LoadGrid()
        {
            this.gvStk.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvStk.DataSource = (DataTable)Session["tblstock"];
            this.gvStk.DataBind();
            this.FooterCal();

        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblstock"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvStk.FooterRow.FindControl("lblgvFbomqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bomqty)", "")) ?
                               0 : dt.Compute("sum(bomqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvStk.FooterRow.FindControl("lblgvFOpnqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opqty)", "")) ?
                              0 : dt.Compute("sum(opqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvStk.FooterRow.FindControl("lblgvFRcvqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rcvqty)", "")) ?
                              0 : dt.Compute("sum(rcvqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvStk.FooterRow.FindControl("lblgvFTinqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trninqty)", "")) ?
                              0 : dt.Compute("sum(trninqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvStk.FooterRow.FindControl("lblgvFToutQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnoutqty)", "")) ?
                              0 : dt.Compute("sum(trnoutqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvStk.FooterRow.FindControl("lblgvFTRecqtq")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tqty)", "")) ?
                              0 : dt.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvStk.FooterRow.FindControl("lblgvFremaqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(remaqty)", "")) ?
                                0 : dt.Compute("sum(remaqty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvStk.FooterRow.FindControl("lblgvFIsueqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(issueqty)", "")) ?
                              0 : dt.Compute("sum(issueqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvStk.FooterRow.FindControl("lblgvFBalqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(acstock)", "")) ?
                              0 : dt.Compute("sum(acstock)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblstock"];
            //ReportDocument rptOrdStk = new RMGiRPT.R_15_Pro.rptOrdMatStock();
            //TextObject rpttxtcname = rptOrdStk.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            //rpttxtcname.Text = comnam;
            //TextObject rpttxtOrderDesc = rptOrdStk.ReportDefinition.ReportObjects["txtOrderDesc"] as TextObject;
            //rpttxtOrderDesc.Text = "Order No: " + this.ddlOrder.SelectedItem.Text;
            //TextObject rpttxtDate = rptOrdStk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtDate.Text = "From " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptOrdStk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptOrdStk.SetDataSource(dt);
            //Session["Report1"] = rptOrdStk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvStk_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvStk.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
    }

}