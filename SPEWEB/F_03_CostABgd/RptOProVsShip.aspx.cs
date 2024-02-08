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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_03_CostABgd
{
    public partial class RptOProVsShip : System.Web.UI.Page
    {
        ProcessAccess ProData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "OrdProVsShip") ? "Order, Production Vs Shipment" : "Production Vs Consumption";

                this.GetSesson();

                this.GetOrderNo();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


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
            string txtsrch = this.txtOrdsrch.Text.Trim()+"%";
            string season = this.DdlSeason.SelectedValue == "00000" ? "%" : this.DdlSeason.SelectedValue + "%";
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", txtsrch, "%", season, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlOrder.DataTextField = "mlcdesc";
            this.ddlOrder.DataValueField = "mlccod";
            this.ddlOrder.DataSource = ds1.Tables[1];
            this.ddlOrder.DataBind();
        }
        protected void imgbtnFindOrd_Click(object sender, EventArgs e)
        {
            this.GetOrderNo();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblopvss");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.ShowView();

        }

        private void ShowView()
        {
            Session.Remove("tblopvss");
            string comcod = this.GetComCode();
            string OrdrNo = this.ddlOrder.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = new DataSet();
            if (this.Request.QueryString["Type"].ToString() == "OrdProVsShip")
            {
                this.MultiView1.ActiveViewIndex = 0;
                ds2 = ProData.GetTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "RPTORDPROVSSHIP", OrdrNo, date, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                Session["tblopvss"] = HiddenSameData(ds2.Tables[0]);



            }
            else
            {

                this.MultiView1.ActiveViewIndex = 2;
                ds2 = ProData.GetTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "RPTPROVSCONS", OrdrNo, date, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                Session["tblopvss"] = ds2.Tables[0];



            }

            this.LoadGrid();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string styleid = dt1.Rows[0]["styleid"].ToString();
            string colorid = dt1.Rows[0]["colorid"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["styleid"].ToString() == styleid && dt1.Rows[j]["colorid"].ToString() == colorid)
                {
                    styleid = dt1.Rows[j]["styleid"].ToString();
                    colorid = dt1.Rows[j]["colorid"].ToString();
                    dt1.Rows[j]["styledesc"] = "";
                    dt1.Rows[j]["colordesc"] = "";


                }

                else
                {
                    if (dt1.Rows[j]["styleid"].ToString() == styleid)
                        dt1.Rows[j]["styledesc"] = "";
                    if (dt1.Rows[j]["colorid"].ToString() == colorid)
                        dt1.Rows[j]["colordesc"] = "";

                    styleid = dt1.Rows[j]["styleid"].ToString();
                    colorid = dt1.Rows[j]["colorid"].ToString();

                }

            }


            return dt1;


        }


        private void LoadGrid()
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "OrdProVsShip":
                    this.gvAnalysis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvAnalysis.DataSource = (DataTable)Session["tblopvss"];
                    this.gvAnalysis.DataBind();
                    this.FooterCal();
                    break;

                case "ProVsCons":

                    this.gvRes.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    DataTable dt = (DataTable)Session["tblopvss"];
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("rescode  like '03%'");
                    this.gvStyle.DataSource = dv.ToTable();
                    this.gvStyle.DataBind();
                    dv.RowFilter = ("rescode not like '03%'");
                    this.gvRes.DataSource = dv.ToTable();
                    this.gvRes.DataBind();
                    this.FooterCal();
                    break;

            }


        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblopvss"];
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "OrdProVsShip":
                    ((Label)this.gvAnalysis.FooterRow.FindControl("lblgvFOrdrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ?
                               0 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAnalysis.FooterRow.FindControl("lblgvFProqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proqty)", "")) ?
                                       0 : dt.Compute("sum(proqty)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAnalysis.FooterRow.FindControl("lblgvFShpqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shipqty)", "")) ?
                                       0 : dt.Compute("sum(shipqty)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAnalysis.FooterRow.FindControl("lblgvFBProQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balpro)", "")) ?
                                       0 : dt.Compute("sum(balpro)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAnalysis.FooterRow.FindControl("lblgvFBShpqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balship)", "")) ?
                                       0 : dt.Compute("sum(balship)", ""))).ToString("#,##0;(#,##0); ");

                    break;


                case "ProVsCons":

                    DataTable dt1 = new DataTable();
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("rescode  like '03%'");
                    dt1 = dv.ToTable();
                    ((Label)this.gvStyle.FooterRow.FindControl("lblgvFOrdrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordrqty)", "")) ?
                            0 : dt1.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvStyle.FooterRow.FindControl("lblgvFProqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(proqty)", "")) ?
                                       0 : dt1.Compute("sum(proqty)", ""))).ToString("#,##0;(#,##0); ");

                    dv.RowFilter = ("rescode not like '03%'");
                    dt1 = dv.ToTable();
                    if (dt1.Rows.Count == 0)
                        return;
                    ((Label)this.gvRes.FooterRow.FindControl("lblgvFOrdrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordrqty)", "")) ?
                             0 : dt1.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvRes.FooterRow.FindControl("lblgvFProqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(proqty)", "")) ?
                                       0 : dt1.Compute("sum(proqty)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvRes.FooterRow.FindControl("lblgvFIsuepqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(isuqty)", "")) ?
                                       0 : dt1.Compute("sum(isuqty)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvRes.FooterRow.FindControl("lblgvFVarqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(varqty)", "")) ?
                                       0 : dt1.Compute("sum(varqty)", ""))).ToString("#,##0;(#,##0); ");



                    break;

            }




        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblopvss"];

            if (this.Request.QueryString["Type"].ToString() == "OrdProVsShip")
            {
                ReportDocument rptOpVSS = new RMGiRPT.R_03_CostABgd.RptOrdProVsShip();
                TextObject rpttxtcname = rptOpVSS.ReportDefinition.ReportObjects["txtCname"] as TextObject;
                rpttxtcname.Text = comnam;
                TextObject rpttxtOrderDesc = rptOpVSS.ReportDefinition.ReportObjects["txtOrderDesc"] as TextObject;
                rpttxtOrderDesc.Text = "Order No: " + this.ddlOrder.SelectedItem.Text;
                TextObject rpttxtDate = rptOpVSS.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                rpttxtDate.Text = "Date: " + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rptOpVSS.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rptOpVSS.SetDataSource(dt);
                Session["Report1"] = rptOpVSS;

            }
            else
            {

                ReportDocument rptOpVSS = new RMGiRPT.R_03_CostABgd.RptProVsCons();
                TextObject rpttxtcname = rptOpVSS.ReportDefinition.ReportObjects["txtCname"] as TextObject;
                rpttxtcname.Text = comnam;
                TextObject rpttxtOrderDesc = rptOpVSS.ReportDefinition.ReportObjects["txtOrderDesc"] as TextObject;
                rpttxtOrderDesc.Text = "Order No: " + this.ddlOrder.SelectedItem.Text;
                TextObject rpttxtDate = rptOpVSS.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                rpttxtDate.Text = "Date: " + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rptOpVSS.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rptOpVSS.SetDataSource(dt);
                Session["Report1"] = rptOpVSS;

            }
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void gvAnalysis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAnalysis.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvRes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRes.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }



        private void GetSesson()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            this.DdlSeason.DataTextField = "gdesc";
            this.DdlSeason.DataValueField = "gcod";
            this.DdlSeason.DataSource = ds1.Tables[0];
            this.DdlSeason.DataBind();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();

            this.DdlSeason.SelectedValue = season;

            this.DdlSeason_SelectedIndexChanged(null, null);

        }




        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetOrderNo();
        }
    }
}