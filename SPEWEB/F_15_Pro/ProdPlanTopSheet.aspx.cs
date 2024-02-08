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
using SPERDLC;


namespace SPEWEB.F_15_Pro
{
    public partial class ProdPlanTopSheet : System.Web.UI.Page
    {
        ProcessAccess ProData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.AddMonths(1).ToString("dd-MMM-yyyy");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "ManProd") ? "Manual Production Top Sheet" :
                   (type == "MatStockAdj") ? "Material Stock Adjustment" : "PRODUCTION TARGET TOP SHEET";
                this.GetBuyer();
                this.selectview();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = ProData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");
            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds2.Tables[0];
            this.ddlBuyer.DataBind();

        }
        private void selectview()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "ManProd":
                    this.sType.Visible = false;
                    this.HyperLink3.Text = "Manual Production";
                    this.HyperLink3.NavigateUrl = "~/F_15_Pro/ProductionManually?Type=Entry&genno=";
                    this.Multivew.ActiveViewIndex = 1;
                    this.gvprodman.Columns[11].Visible = false;
                    break;
                case "ManProdRM":
                    this.sType.Visible = false;
                    this.HyperLink3.Visible = false;
                    this.Multivew.ActiveViewIndex = 1;
                    this.gvprodman.Columns[10].Visible = false;

                    break;

                case "Datewise":
                case "PlanNo":
                case "PlanWise":
                    this.Multivew.ActiveViewIndex = 0;
                    break;
                case "MatStockAdj":
                    this.sType.Visible = false;
                    this.HyperLink3.Visible = false;
                    this.Buyerarea.Visible = false;

                    this.Multivew.ActiveViewIndex = 2;
                    this.gvprodman.Columns[10].Visible = false;

                    break;
            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Datewise":
                case "PlanNo":
                case "PlanWise":
                    this.GETPRODPLAN();
                    break;
                case "ManProd":
                case "ManProdRM":
                    this.GetProductionManually();
                    break;
                case "MatStockAdj":
                    this.GetStockAdjList();
                    break;
            }


        }

        private void GetStockAdjList()
        {
            string comcod = this.GetCompCode();
            string frmDate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_STOCK_MANAGEMENT", "GET_STOCK_ADJS_LIST", frmDate, todate, "");
            if (ds1 == null)
            {
                return;
            }
            var stockadj = ds1.Tables[0].DataTableToList<SPEENTITY.C_11_RawInv.StockAdjList>();
            if (stockadj == null)
            {
                this.gvStockadj.DataSource = null;
                this.gvStockadj.DataBind();
                return;
            }

            ViewState["tblstockadj"] = stockadj;
            this.Data_Bind();
        }
        private void GetProductionManually()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string txtFdate = this.txtfromdate.Text.ToString();
            string txttdate = this.txttodate.Text.ToString();

            string buyerid = ""; // this.ddlAccProject.SelectedValue.ToString();
            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    buyerid += item.Value;
                }
            }
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "GETPRODMANLIST", txtFdate, txttdate, buyerid, " ", "", "", "");

            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdManList>();


            ViewState["ProdManlist"] = list;
            if (list == null)
            {
                return;
            }
            this.Data_Bind();
        }
        private void GETPRODPLAN()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string type = this.ddltype.SelectedValue.ToString();
            string rType = (this.Request.QueryString["Type"] == "PlanNo") ? "PlanWise" : "";
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GET_MONTHLY_PROD_PLAN", fromdate, todate, type, rType, "", "", "", "", "");

            if (ds1 == null)
            {
                return;
            }
            List<SPEENTITY.C_15_Pro.EclassProdPlanSummary> lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.EclassProdPlanSummary>();
            if (lst.Count == 0)
                return;




            ViewState["tblprodplan"] = lst;
            DataTable dt = ds1.Tables[1];
            int j = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 30)
                    break;
                this.gvprodplan.Columns[3 + i].HeaderText = Convert.ToDateTime(dt.Rows[i]["pdate"]).ToString("dd-MMM-yyyy");
                //  i++;
            }
            if (type == "sum")
            {
                this.gvprodplan.Columns[2].Visible = false;
                this.gvprodplan.Columns[1].Visible = false;
            }
            else if (type == "order")
            {
                this.gvprodplan.Columns[1].Visible = true;
                this.gvprodplan.Columns[2].Visible = false;
            }
            else
            {
                this.gvprodplan.Columns[2].Visible = true;
                this.gvprodplan.Columns[1].Visible = true;
            }


            this.Data_Bind();


        }

        private void Data_Bind()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "ManProd":
                case "ManProdRM":

                    if (hst["usrdesig"].ToString() != "Administrator")
                    {
                        this.gvprodman.Columns[9].Visible = false;

                    }
                    List<SPEENTITY.C_15_Pro.BO_Production.EclassProdManList> manlst = (List<SPEENTITY.C_15_Pro.BO_Production.EclassProdManList>)ViewState["ProdManlist"];
                    this.gvprodman.DataSource = manlst;
                    this.gvprodman.DataBind();
                    if (manlst.Count == 0)
                        return;
                    Session["Report1"] = gvprodman;
                    ((HyperLink)this.gvprodman.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    break;
                case "Datewise":
                case "PlanNo":
                case "PlanWise":
                    List<SPEENTITY.C_15_Pro.EclassProdPlanSummary> lst = (List<SPEENTITY.C_15_Pro.EclassProdPlanSummary>)ViewState["tblprodplan"];
                    this.gvprodplan.DataSource = lst;
                    this.gvprodplan.DataBind();
                    this.FooterCalculation();
                    Session["Report1"] = gvprodplan;
                    ((HyperLink)this.gvprodplan.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
                case "MatStockAdj":
                    List<SPEENTITY.C_11_RawInv.StockAdjList> stckadj = (List<SPEENTITY.C_11_RawInv.StockAdjList>)ViewState["tblstockadj"];
                    this.gvStockadj.DataSource = stckadj;
                    this.gvStockadj.DataBind();
                    break;
                    //for (int i = 0; i < gvprotar.Rows.Count; i++)
                    //{
                    //    string linecode = ((Label)gvprotar.Rows[i].FindControl("lgvlinecode")).Text.Trim();
                    //    LinkButton lbtn1 = (LinkButton)gvprotar.Rows[i].FindControl("lbtnadd");
                    //    if (lbtn1 != null)
                    //        if (lbtn1.Text.Trim().Length > 0)
                    //            lbtn1.CommandArgument = linecode;
                    //}
            }

        }

        private void FooterCalculation()
        {
            var lst = (List<SPEENTITY.C_15_Pro.EclassProdPlanSummary>)ViewState["tblprodplan"];

            if (lst.Count == 0)
                return;

            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr01")).Text = ((lst.Select(p => p.qty2).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty2).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr02")).Text = ((lst.Select(p => p.qty2).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty2).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr03")).Text = ((lst.Select(p => p.qty3).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty3).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr04")).Text = ((lst.Select(p => p.qty4).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty4).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr05")).Text = ((lst.Select(p => p.qty5).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty5).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr06")).Text = ((lst.Select(p => p.qty6).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty6).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr07")).Text = ((lst.Select(p => p.qty7).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty7).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr08")).Text = ((lst.Select(p => p.qty8).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty8).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr09")).Text = ((lst.Select(p => p.qty9).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty9).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr10")).Text = ((lst.Select(p => p.qty10).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty10).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr11")).Text = ((lst.Select(p => p.qty11).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty11).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr12")).Text = ((lst.Select(p => p.qty12).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty12).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr13")).Text = ((lst.Select(p => p.qty13).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty13).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr14")).Text = ((lst.Select(p => p.qty14).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty14).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr15")).Text = ((lst.Select(p => p.qty15).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty15).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr16")).Text = ((lst.Select(p => p.qty16).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty16).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr17")).Text = ((lst.Select(p => p.qty17).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty17).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr18")).Text = ((lst.Select(p => p.qty18).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty18).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr19")).Text = ((lst.Select(p => p.qty19).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty19).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr20")).Text = ((lst.Select(p => p.qty20).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty20).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr21")).Text = ((lst.Select(p => p.qty21).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty21).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr22")).Text = ((lst.Select(p => p.qty22).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty22).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr23")).Text = ((lst.Select(p => p.qty23).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty23).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr24")).Text = ((lst.Select(p => p.qty24).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty24).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr25")).Text = ((lst.Select(p => p.qty25).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty25).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr26")).Text = ((lst.Select(p => p.qty26).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty26).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr27")).Text = ((lst.Select(p => p.qty27).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty27).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr28")).Text = ((lst.Select(p => p.qty28).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty28).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr29")).Text = ((lst.Select(p => p.qty29).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty29).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr30")).Text = ((lst.Select(p => p.qty30).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty30).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvprodplan.FooterRow.FindControl("lblrpFr31")).Text = ((lst.Select(p => p.qty31).Sum() == 0.00) ? 0.00 : lst.Select(p => p.qty31).Sum()).ToString("#,##0;(#,##0); ");


        }
        protected void gvStockadj_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("hypPrintbtn");
                //    LinkButton chalnbtn = (LinkButton)e.Row.FindControl("lbtnChalan");
                HyperLink editlink = (HyperLink)e.Row.FindControl("HypEdit");
                HyperLink applink = (HyperLink)e.Row.FindControl("HypApprv");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
                string batchcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "batchcod")).ToString();
                string adjno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adjno")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "adjdate")).ToString("dd-MMM-yyyy");
                string status = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "status")).ToString();



                if (status == "False")
                {
                    //editlink.NavigateUrl = "~/F_23_SaM/StockAdjstmnt.aspx?Type=Entry&centrid=&actcode=&date=";
                    editlink.NavigateUrl = "~/F_11_RawInv/StockAdjstmnt?Type=Entry&centrid=" + centrid + "&genno=" + adjno + "&date=" + date;
                    applink.NavigateUrl = "~/F_11_RawInv/StockAdjstmnt?Type=Approve&centrid=" + centrid + "&genno=" + adjno + "&date=" + date;

                }
                else
                {
                    //chalnbtn.Text = "<span class='glyphicon glyphicon-ban-circle'></span>";
                    //chalnbtn.CssClass = "btn btn-xs btn-warning";
                    //chalnbtn.ToolTip = "Approved";
                    editlink.Text = "<span class='fa-solid fa-lock'></span>";
                    editlink.ToolTip = "Approved";
                    //editlink.CssClass = "btn btn-xs btn-danger";



                    applink.Text = "<span class='fa-solid fa-lock'></span>";
                    applink.ToolTip = "Approved";
                    //fa-solid fa-lockapplink.CssClass = "btn btn-xs btn-danger";

                }


            }
        }
        protected void gvprodman_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink Lbtn = (HyperLink)e.Row.FindControl("LbtnApp");
                string status = ((Label)e.Row.FindControl("LblApstats")).Text.ToString();
                string mgrrno = ((Label)e.Row.FindControl("lvgrrno")).Text.ToString();

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                //-------------RM Part-------------//
                HyperLink HyRMEntry = (HyperLink)e.Row.FindControl("HyRMEntry");
                string lblRMEntry = ((Label)e.Row.FindControl("lblRMEntry")).Text.ToString();
                HyperLink HyRMPrint = (HyperLink)e.Row.FindControl("HyRMPrint");
                HyperLink HtRMApp = (HyperLink)e.Row.FindControl("HtRMApp");
                string LblRMApstats = ((Label)e.Row.FindControl("LblRMApstats")).Text.ToString();
                //-------------RM Part ENd-------------//

                hlink1.NavigateUrl = "~/F_15_Pro/Print?Type=prodman&genno=" + mgrrno;

                //LbtnPrint. = "~/F_15_Pro/Print.aspx?Type=Approve&genno=" + mgrrno;

                string type = this.Request.QueryString["Type"].ToString();
                if (type == "ManProd")
                {
                    if (status == "False")
                    {
                        Lbtn.NavigateUrl = "~/F_15_Pro/ProductionManually?Type=Approve&genno=" + mgrrno;
                        Lbtn.Target = "blank";

                    }
                    else
                    {
                        Lbtn.Text = "<span class='fa fa-lock'></span>";
                        //Lbtn.CssClass = "btn btn-xs btn-danger";
                        Lbtn.ToolTip = "Approved";
                    }
                }
                else
                {
                    if (lblRMEntry == "False")
                    {
                        HyRMEntry.NavigateUrl = "~/F_15_Pro/ProductionManually?Type=EntryRM&genno=" + mgrrno;
                        HyRMEntry.Target = "blank";

                    }
                    else//<i class="fa-solid fa-lock"></i>
                    {
                        HyRMEntry.Text = "<span class='fa fa-lock'></span>";
                        //HyRMEntry.CssClass = "btn btn-xs btn-danger";
                        HyRMEntry.ToolTip = "RM Entry Done";
                    }

                    if (LblRMApstats == "False")
                    {
                        HtRMApp.NavigateUrl = "~/F_15_Pro/ProductionManually?Type=ApproveRM&genno=" + mgrrno;
                        HtRMApp.Target = "blank";

                    }
                    else
                    {
                        HtRMApp.Text = "<span class='fa fa-lock'></span>";
                        //HtRMApp.CssClass = "btn btn-xs btn-danger";
                        HtRMApp.ToolTip = "RM Approved";
                    }
                }


            }
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



        }





        protected void LbtnDelete_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string mgrrno = ((Label)gvprodman.Rows[index].FindControl("lvgrrno")).Text.ToString();
            string comcod = this.GetCompCode();
            bool result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "DELETE_PRODUCTION_MANUALLY_ALL", mgrrno, "", "", "", "", "");
            if (result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Successfully";
                this.GetProductionManually();
            }
        }
    }


}