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
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace SPEWEB.F_01_Mer
{
    public partial class RptOrdSum : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //if ((!ASTUtility.PagePermission (HttpContext.Current.Request.Url.AbsoluteUri.ToString ().Substring (0, indexofamp), (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean (hst["permission"]))
                //    Response.Redirect("~/AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0,indexofamp),
                //    (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled =dr1.Length==0?false: (Convert.ToBoolean(dr1[0]["printable"]));


                ////if (hst["comcod"]=="3101" || hst["comcod"]=="")

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "Ord") ? "Order Summary"
                    : (this.Request.QueryString["Type"] == "Shipment") ? "Shipment Summary"
                    : (this.Request.QueryString["Type"] == "Relz") ? "Realization Summary"
                    : (this.Request.QueryString["Type"] == "ProTar") ? "Production Target Summary" : "Production Execution Summary";


                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtFDate.Text = (this.Request.QueryString["Date1"].Length > 0) ? this.Request.QueryString["Date1"].ToString() : Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = (this.Request.QueryString["Date2"].Length > 0) ? this.Request.QueryString["Date2"].ToString() : Convert.ToDateTime(txtFDate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                //this.txttodate.Text = date;
                //this.txtFDate.Text = "01" + date.Substring (2);

                if (this.Request.QueryString["Type"] == "Relz")
                {
                    this.gvOrdSumm.Columns[2].Visible = false;
                    this.gvOrdSumm.Columns[3].Visible = false;
                    this.gvOrdSumm.Columns[4].Visible = false;

                }

            }
        }


        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;

        }

        private void ShowAll()
        {
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string CallType = (this.Request.QueryString["Type"] == "Ord") ? "ORDERGRAPH" : (this.Request.QueryString["Type"] == "Shipment") ? "EXPORTGRAPH"
                : (this.Request.QueryString["Type"] == "Relz") ? "RELZGRAPH" : (this.Request.QueryString["Type"] == "ProTar") ? "" : "";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_DASHBOARDMAIN", CallType, fromdate, todate, "", "", "", "", "", "", "");
            // DataSet ds = MktData.GetTransInfo (comcod, "SP_REPORT_ACCOUNTS_BGD", "GETYSALBGDBREAK", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvOrdSumm.DataSource = null;
                this.gvOrdSumm.DataBind();
                return;
            }

            ViewState["tblmainhead"] = ds1.Tables[0];
            ViewState["tblbreakd"] = ds1.Tables[1];
            ViewState["tblbmainhead"] = ds1.Tables[0];
            //ViewState["tblabpamt"] = ds1.Tables[2];
            this.LoadGrid();
        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            this.ShowAll();
        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)ViewState["tblmainhead"];
            //DataTable dt1 = (DataTable)ViewState["tblabpamt"];
            this.gvOrdSumm.DataSource = dt;
            this.gvOrdSumm.DataBind();
            this.FooterCal();
            this.ShowGraph();
            //this.abppanel.Visible = true;
            //this.abpamt.Text =(dt1.Rows.Count==0)?"0.00": Convert.ToDouble (dt1.Rows[0]["topamt"]).ToString ("#,##0.00;(#,##0.00); ");
        }

        private void ShowGraph()
        {

            DataTable dt = ((DataTable)ViewState["tblmainhead"]);
            DataTable dt2 = ((DataTable)ViewState["tblbreakd"]);
            DataTable dt3 = ((DataTable)ViewState["tblbmainhead"]);

            var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.OrderSummary>();
            var lst2 = dt2.DataTableToList<SPEENTITY.C_01_Mer.OrderSummary>();
            var lst3 = dt3.DataTableToList<SPEENTITY.C_01_Mer.OrderSummary>();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);
            var json2 = jsonSerialiser.Serialize(lst2);
            var json3 = jsonSerialiser.Serialize(lst3);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + json + "','" + json2 + "', '" + json3 + "')", true);
            //,'" + json2 + "'


        }



        protected void lnkgvWDescgp_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string mlccod1 = ((DataTable)ViewState["tblmainhead"]).Rows[index]["mlccod1"].ToString();
            string mlccod = ((DataTable)ViewState["tblmainhead"]).Rows[index]["mlccod"].ToString();
            string colst = ((DataTable)ViewState["tblmainhead"]).Rows[index]["colst"].ToString();
            DataTable dt = ((DataTable)ViewState["tblmainhead"]);
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = ("mlccod like '%000'");
            dt = dv.ToTable();

            DataRow[] dr1 = dt.Select("mlccod1='" + mlccod1 + "'");
            dr1[0]["colst"] = (colst == "0") ? "1" : "0";


            // For Status 0
            foreach (DataRow dr2 in dt.Rows)
            {
                if (dr2["mlccod1"] != mlccod1)
                {
                    dr2["colst"] = "0";

                }
            }

            colst = (dt.Select("mlccod1='" + mlccod1 + "'"))[0]["colst"].ToString();
            if (colst == "1")
            {
                DataTable dtb = ((DataTable)ViewState["tblbreakd"]).Copy();
                dv = dtb.DefaultView;
                dv.RowFilter = ("mlccod1='" + mlccod1 + "'");
                dtb = dv.ToTable();
                dt.Merge(dtb);

            }

            dv = dt.DefaultView;
            dv.Sort = ("mlccod1, mlccod");
            ViewState["tblmainhead"] = dv.ToTable();
            this.LoadGrid();


        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)ViewState["tblmainhead"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("mlccod like '%000'");
            dt = dv.ToTable();
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvOrdSumm.FooterRow.FindControl("lgvFtamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt)", "")) ?
                             0 : dt.Compute("sum(tamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrdSumm.FooterRow.FindControl("lgvFfcamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fcamt)", "")) ?
                             0 : dt.Compute("sum(fcamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void gvOrdSumm_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                LinkButton lnkgvWDescgp = (LinkButton)e.Row.FindControl("lnkgvWDescgp");
                //HyperLink HypLInk = (HyperLink)e.Row.FindControl("HypLInk");
                //Label lblBgdamt = (Label)e.Row.FindControl("lgvBgdamtsp");
                Label lgvqty = (Label)e.Row.FindControl("lgvqty");
                Label lgvrate = (Label)e.Row.FindControl("lgvrate");
                Label lgvAmt = (Label)e.Row.FindControl("lgvfcamt");
                Label lgvunit = (Label)e.Row.FindControl("lgvunit");
                Label lgvtamt = (Label)e.Row.FindControl("lgvtamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                var colst = DataBinder.Eval(e.Row.DataItem, "colst");
                if (Convert.ToInt32(colst) == 1)
                {
                    lnkgvWDescgp.Enabled = false;
                    //lnkgvWDescgp.Visible = false;
                    //HypLInk.Visible = true;
                    //HypLInk.Enabled = true;
                    //HypLInk.NavigateUrl = "www.facebook.com";
                    //HypLInk.Target = "blank";
                }
                else
                {
                    lnkgvWDescgp.Enabled = true;
                    // lnkgvWDescgp.Visible = true;
                    //HypLInk.Visible = false;
                    //HypLInk.Enabled = false;
                }

                if (code == "")
                {
                    return;
                }

                if (ASTUtility.Left(code, 2) == "00")
                {

                    lnkgvWDescgp.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvqty.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvrate.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvAmt.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvunit.Attributes["style"] = "color:maroon;font-weight:bold;";
                    lgvtamt.Attributes["style"] = "color:maroon;font-weight:bold;";
                }

            }
        }
    }
}