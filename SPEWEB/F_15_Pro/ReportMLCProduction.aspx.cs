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
    public partial class ReportMLCProduction : System.Web.UI.Page
    {
        ProcessAccess Production = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "ProStatus") ? "Production Status" : "Production Vs Stock";
            // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            if (this.ddlMLc.Items.Count == 0)
            {
                this.GetMasterLC();
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
            string comcod = hst["comcod"].ToString();
            return comcod;

        }


        private void GetMasterLC()
        {
            string comcod = GetComCode();
            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "RptExisLCOrder", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlMLc.DataTextField = "actdesc1";
            this.ddlMLc.DataValueField = "actcode";
            this.ddlMLc.DataSource = ds1.Tables[0];
            this.ddlMLc.DataBind();

            Session["TblOrder"] = ds1.Tables[1];

            ddlMLc_SelectedIndexChanged(null, null);

        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            if (this.Request.QueryString["Type"].ToString().Trim() == "ProStatus")
            {
                this.ProductionReport();
            }
            else
            {

            }

        }


        private void ProductionReport()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string comcod = GetComCode();
            //string mlccod = this.ddlMLc.SelectedValue.ToString().Trim();
            //string mlctex = this.ddlMLc.SelectedItem.Text.Substring(14);
            //string ordtex = this.ddlOrder.SelectedItem.Text.Trim();
            //string ordrid = this.ddlOrder.SelectedValue.ToString().Trim().Substring(12).Trim();

            //if (!this.chkAllOrder.Checked)
            //{
            //    //DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "RptGetActualProductionDetails",ordrid, "","", "", "", "", "", "", "");

            //    DataTable dt1 = (DataTable)Session["ProBalance"];
            //    DataTable dt2 = (DataTable)Session["Size"];
            //    ReportDocument rptmlcp = new RMGiRPT.R_15_Pro.RptMLCProduction();
            //    TextObject rpttxtCname = rptmlcp.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            //    rpttxtCname.Text = comnam;
            //    TextObject rptmasterlc = rptmlcp.ReportDefinition.ReportObjects["masterlc"] as TextObject;
            //    rptmasterlc.Text = mlctex;
            //    TextObject rptorderno = rptmlcp.ReportDefinition.ReportObjects["orderno"] as TextObject;
            //    rptorderno.Text = ordtex;
            //    int i = 0;
            //    for (i = 0; i < dt2.Rows.Count; i++)
            //    {
            //        string header = dt2.Rows[i]["StyleDes"].ToString().Trim();
            //        TextObject rpttxth = rptmlcp.ReportDefinition.ReportObjects["s" + (i + 1).ToString()] as TextObject;
            //        rpttxth.Text = header;
            //    }

            //    //TextObject rpttxtTotal = rptmlcp.ReportDefinition.ReportObjects["s" + (i + 1).ToString()] as TextObject;
            //    //rpttxtTotal.Text = "Total";
            //    TextObject txtuserinfo = rptmlcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //    rptmlcp.SetDataSource(dt1);
            //    Session["Report1"] = rptmlcp;


            //}
            //else
            //{
            //    DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "RptGetTotalActualProductionDetails", mlccod, "", "", "", "", "", "", "", "");
            //    DataTable dt1 = ds1.Tables[0];
            //    DataTable dt2 = ds1.Tables[1];
            //    ReportDocument rptmlcp = new RMGiRPT.R_15_Pro.RptAllMLCProduction();
            //    TextObject rpttxtCname = rptmlcp.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            //    rpttxtCname.Text = comnam;
            //    TextObject rptmasterlc = rptmlcp.ReportDefinition.ReportObjects["masterlc"] as TextObject;
            //    rptmasterlc.Text = mlctex;

            //    int i = 0, j = 0;
            //    string tempstr = "";
            //    for (j = 0, i = 0; i < dt2.Rows.Count; i++, j++)
            //    {
            //        string header = dt2.Rows[i]["StyleDes"].ToString().Trim();
            //        if (i != 0 && tempstr == header)
            //        {
            //            j--;
            //        }
            //        else
            //        {

            //            TextObject rpttxth = rptmlcp.ReportDefinition.ReportObjects["s" + (j + 1).ToString()] as TextObject;
            //            rpttxth.Text = header;
            //        }
            //        tempstr = header;

            //    }

            //    TextObject rpttxtTotal = rptmlcp.ReportDefinition.ReportObjects["s" + (j + 1).ToString()] as TextObject;
            //    rpttxtTotal.Text = "Total";
            //    TextObject txtuserinfo = rptmlcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //    rptmlcp.SetDataSource(ds1.Tables[0]);
            //    Session["Report1"] = rptmlcp;
            //}
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlMLc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccod = this.ddlMLc.SelectedValue.ToString().Trim();

            DataView dv1 = ((DataTable)Session["TblOrder"]).DefaultView;
            dv1.RowFilter = "mlccod = '" + mlccod + "'";
            this.ddlOrder.DataTextField = "GenData";
            this.ddlOrder.DataValueField = "gencod1";
            this.ddlOrder.DataSource = dv1;
            this.ddlOrder.DataBind();
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            if (this.Request.QueryString["Type"].ToString().Trim() == "ProStatus")
            {
                this.MultiView1.ActiveViewIndex = 0;
                this.ShowProStatus();
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
            }
            else
            {
                this.MultiView1.ActiveViewIndex = 1;
                this.ShowProVsStock();
            }
        }
        private void ShowProStatus()
        {
            string comcod = GetComCode();
            string mlccod = this.ddlOrder.SelectedValue.ToString().Trim().Substring(12).Trim();
            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "RptGetActualProductionDetails", mlccod, "", "", "", "", "", "", "", "");

            Session["ProBalance"] = HiddenSameData(ds1.Tables[0]);
            Session["Size"] = (ds1.Tables[1]);
            if (ds1 == null)
            {
                gvProStatus.DataSource = null;
                gvProStatus.DataBind();
                return;
            }
            this.Data_Bind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string prodno = dt1.Rows[0]["prodno"].ToString();
            string prodate = dt1.Rows[0]["PRODUCDAT"].ToString();
            string StyleID = dt1.Rows[0]["StyleID"].ToString();
            string ColorID = dt1.Rows[0]["ColorID"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["prodno"].ToString() == prodno && dt1.Rows[j]["PRODUCDAT"].ToString() == prodate)
                {
                    prodno = dt1.Rows[j]["prodno"].ToString();
                    prodate = dt1.Rows[j]["PRODUCDAT"].ToString();
                    dt1.Rows[j]["prodno"] = "";
                    dt1.Rows[j]["PRODUCDAT"] = "";
                }

                else
                {
                    prodno = dt1.Rows[j]["prodno"].ToString();
                    prodate = dt1.Rows[j]["PRODUCDAT"].ToString();
                }

                /////------------------------------////////////
                if (dt1.Rows[j]["StyleID"].ToString() == StyleID)
                {
                    StyleID = dt1.Rows[j]["StyleID"].ToString();
                    dt1.Rows[j]["StyleDes"] = "";
                }

                else
                {
                    StyleID = dt1.Rows[j]["StyleID"].ToString();
                }
            }

            return dt1;


        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["ProBalance"];
            DataTable dt1 = (DataTable)Session["Size"];
            this.gvProStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    this.gvProStatus.Columns[i + 5].HeaderText = "";
                    this.gvProStatus.Columns[i + 5].Visible = false;
                }

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string ColHeadDesc = dt1.Rows[i]["StyleDes"].ToString();
                    this.gvProStatus.Columns[i + 5].HeaderText = ColHeadDesc;
                    this.gvProStatus.Columns[i + 5].Visible = true;
                }
                this.gvProStatus.DataSource = dt;
                this.gvProStatus.DataBind();
                this.FooterCal();
            }
        }
        protected void FooterCal()
        {
            DataTable dt = (DataTable)Session["ProBalance"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvProStatus.FooterRow.FindControl("lblgvFProqty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(F7201001)", "")) ?
                            0 : dt.Compute("sum(F7201001)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProStatus.FooterRow.FindControl("lblgvFProqty2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(F7201002)", "")) ?
                                    0 : dt.Compute("sum(F7201002)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProStatus.FooterRow.FindControl("lblgvFProqty3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(F7201003)", "")) ?
                           0 : dt.Compute("sum(F7201003)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProStatus.FooterRow.FindControl("lblgvFProqty4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(F7201004)", "")) ?
                                    0 : dt.Compute("sum(F7201004)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProStatus.FooterRow.FindControl("lblgvFProqty5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(F7201005)", "")) ?
                           0 : dt.Compute("sum(F7201005)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProStatus.FooterRow.FindControl("lblgvFTptal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(total)", "")) ?
                                    0 : dt.Compute("sum(total)", ""))).ToString("#,##0;(#,##0); ");

        }
        private void ShowProVsStock()
        {

        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvProStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }

}