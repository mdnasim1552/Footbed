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
using SPERDLC;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPEENTITY;
using System.Web.Script.Serialization;

namespace SPEWEB.F_01_Mer
{
    public partial class RptOrdAppSheet : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost, OrdrVal, toqty, ToCostPer, ToCostPerM, totalcmPer;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "OrdPlan") ? "Order Sheet Plan Report" : (type == "costdiff") ? "Pre Costing Vs Post Costing Report" :
                (type == "BuyerWiseSamp") ? "Buyer Wise Article List" :(type == "PendBom") ? "Pending BOM List" : "BOM Approved List";

                
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;
                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

                this.txtDatefrom.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtDatefrom.Text = "01" + this.txtDatefrom.Text.Trim().Substring(2);
                this.txtdateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.lnkbtnSerOk_Click(null, null);
                this.GetSeason();
                this.GetBuyer();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkBtnAdd_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("href", "../F_01_Mer/MerLCAnalysis?Type=Create&actcode=");
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Attributes.Add("target", "_blank");

        }


        public string GetArticle()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst["comcod"].ToString() == "5301")
            {
                return "Edison Article";
            }
            else
            {
                return "Sys. Gen. Article";

            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "OrdPlan":
                case "BomApp":
                    this.MultiView1.ActiveViewIndex = 0;
                    if (GetComCode() == "5305" || GetComCode() == "5306")
                    {
                        this.gvBomAppList.Columns[17].Visible = false;
                        this.gvBomAppList.Columns[21].Visible = true;
                    }
                    else {
                        this.gvBomAppList.Columns[17].Visible = true;
                        this.gvBomAppList.Columns[21].Visible = false;

                    }
                    this.ShowValue();
                    break;
                case "BuyerWiseSamp":
                    this.MultiView1.ActiveViewIndex = 0;
                    if (GetComCode() == "5305" || GetComCode() == "5306")
                    {
                        this.gvBomAppList.Columns[17].Visible = false;
                        this.gvBomAppList.Columns[21].Visible = true;
                    }
                    else
                    {
                        this.gvBomAppList.Columns[17].Visible = true;
                        this.gvBomAppList.Columns[21].Visible = false;

                    }
                    this.ShowValue();
                    break;

                case "costdiff":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetCostDiff();
                    break;
                case "PendBom":
                    this.plnDateF.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowPenBOM();
                    break;
            }

        }

        private void ShowValue()
        {

            string comcod = this.GetComCode();

            string fdate = this.txtDatefrom.Text.ToString();
            string tdate = this.txtdateto.Text.ToString();
            string Season = this.DdlSeason.SelectedValue.ToString() == "00000" ? "%" : this.DdlSeason.SelectedValue.ToString();
            string buyerid = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";

            DataSet ds2 = feaData.GetTransInfoNew(comcod, "SP_REPORT_MERCHAN_01", "BOMAPPLIST", null, null, null, fdate, tdate, Season, buyerid, "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvBomAppList.DataSource = null;
                this.gvBomAppList.DataBind();
                return;
            }

            List<SPEENTITY.C_01_Mer.OrderDetails> list1 = ds2.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.OrderDetails>();
            ViewState["tblBomInfo"] = list1;
            this.Data_Bind();

        }
        private void GetSeason()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetCostDiff()
        {

            string comcod = this.GetComCode();

            string fdate = this.txtDatefrom.Text.ToString();
            string tdate = this.txtdateto.Text.ToString();

            DataSet ds2 = feaData.GetTransInfoNew(comcod, "SP_REPORT_MERCHAN_01", "ORDERWISE_PRECOSTING_VS_POSTCOSTING", null, null, null, fdate, tdate, "%", "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.GvCostDiff.DataSource = null;
                this.GvCostDiff.DataBind();
                return;
            }

            ViewState["tblcostdiff"] = ds2.Tables[0];



            this.Data_Bind();

        }
        private void ShowPenBOM()
        {

            string comcod = this.GetComCode();

            string tdate = this.txtdateto.Text.ToString();

            string buyerid = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";

            DataSet ds2 = feaData.GetTransInfoNew(comcod, "SP_REPORT_ORDER_STATUS", "RPTPENBOMMAT", null, null, null, tdate, buyerid, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvPendBOM.DataSource = null;
                this.gvPendBOM.DataBind();
                return;
            }

            List<SPEENTITY.C_01_Mer.OrderDetails> list1 = ds2.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.OrderDetails>();
            ViewState["tblPenBomInfo"] = list1;



            this.Data_Bind();

        }
        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString();
            var inqtbl = (List<SPEENTITY.C_01_Mer.OrderDetails>)ViewState["tblBomInfo"];

            switch (type)
            {
                case "OrdPlan":
                    if (inqtbl == null)
                        return;

                    gvBomAppList.Columns[3].Visible = false;
                    gvBomAppList.Columns[4].Visible = false;
                    gvBomAppList.Columns[15].Visible = false;
                    gvBomAppList.Columns[16].Visible = false;
                    gvBomAppList.Columns[17].Visible = false;

                    this.gvBomAppList.DataSource = inqtbl;
                    this.gvBomAppList.DataBind();

                    this.FooterCal();

                    break;

                case "BomApp":
                    if (inqtbl == null)
                        return;

                    gvBomAppList.Columns[18].Visible = false;
                    gvBomAppList.Columns[19].Visible = false;
                    gvBomAppList.Columns[20].Visible = false;
                    

                    this.gvBomAppList.DataSource = inqtbl;
                    this.gvBomAppList.DataBind();

                    this.FooterCal();
                    break;
                case "BuyerWiseSamp":
                    if (inqtbl == null)
                        return;
                    gvBomAppList.Columns[3].Visible = false;
                    gvBomAppList.Columns[4].Visible = false;

                    //gvBomAppList.Columns[16].Visible = false;
                    //gvBomAppList.Columns[17].Visible = false;
                    //gvBomAppList.Columns[18].Visible = false;


                    this.gvBomAppList.DataSource = inqtbl;
                    this.gvBomAppList.DataBind();

                    this.FooterCal();
                    break;

                case "costdiff":
                    DataTable dt = (DataTable)ViewState["tblcostdiff"];
                    this.GvCostDiff.DataSource = dt;
                    this.GvCostDiff.DataBind();
                    break;
                
                case "PendBom":
                    var lstbom = (List<SPEENTITY.C_01_Mer.OrderDetails>)ViewState["tblPenBomInfo"];
                    if (lstbom == null)
                        return;
                    
                    this.gvPendBOM.DataSource = lstbom;
                    this.gvPendBOM.DataBind();

                    this.FooterCal();
                    break;
            }
        }

        private void FooterCal()
        {
            string type = this.Request.QueryString["Type"].ToString();


            switch (type)
            {

                case "OrdPlan":
                case "BomApp":

                    var inqtbl = (List<SPEENTITY.C_01_Mer.OrderDetails>)ViewState["tblBomInfo"];
                    if (inqtbl == null || inqtbl.Count == 0)
                        return;

                    ((Label)this.gvBomAppList.FooterRow.FindControl("lblgvFOrdqty")).Text = ((inqtbl.Sum(p => p.ordqty) == 0) ? 0 : inqtbl.Sum(p => p.ordqty)).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBomAppList.FooterRow.FindControl("lblgvFPlanqty")).Text = ((inqtbl.Sum(p => p.proplanqty) == 0) ? 0 : inqtbl.Sum(p => p.proplanqty)).ToString("#,##0;(#,##0); ");
                    break;

                case "PendBom":
                    var lstbom = (List<SPEENTITY.C_01_Mer.OrderDetails>)ViewState["tblPenBomInfo"];
                    if (lstbom == null)
                        return;
                    ((Label)this.gvPendBOM.FooterRow.FindControl("lblgvFOrdqty")).Text = ((lstbom.Sum(p => p.ordqty) == 0) ? 0 : lstbom.Sum(p => p.ordqty)).ToString("#,##0;(#,##0); ");

                    break;
            }


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "BomApp":
                    this.PrintBomAppList();
                    break;
            }

        }

        protected void lbtnSummery_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "BomApp":
                    this.BomAppListSummery();
                    break;
            }

        }
        private void BomAppListSummery()
        {

            string comcod = this.GetComCode();
            string tdate = DateTime.Now.ToString("dd-MMM-yyyy");
           
            DataSet ds2 = feaData.GetTransInfoNew(comcod, "SP_REPORT_MERCHAN_01", "bomapplistsummery", null, null, null, tdate, "",  "" , "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvBomSummery.DataSource = null;
                this.gvBomSummery.DataBind();
                return;
            }
            this.gvBomSummery.DataSource = ds2.Tables[0];
            this.gvBomSummery.DataBind();

            var bomsummary = ds2.Tables[0].DataTableToList<BOMSummaryReport>();

            var jsonSerialiser = new JavaScriptSerializer();
            var bomsummary_json = jsonSerialiser.Serialize(bomsummary);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal('" + bomsummary_json + "');", true);

        }

        private void PrintBomAppList()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string rptTitle = "BOM Approve List";
            string fromtodate = "From: " + this.txtDatefrom.Text + " To: " + this.txtdateto.Text;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            string season = this.DdlSeason.SelectedItem.Text;

            var list1 = (List<SPEENTITY.C_01_Mer.OrderDetails>)ViewState["tblBomInfo"];

            if (list1 == null) return;
            if (list1.Count == 0) return;

            foreach (var item in list1)
            {
                item.images = item.images.Length > 0 ? new Uri(Server.MapPath(item.images.ToString())).AbsoluteUri : new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
            }

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptBOMApprvList", list1, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comlogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("fromtodate", fromtodate));
            Rpt1.SetParameters(new ReportParameter("season", season));
            Rpt1.SetParameters(new ReportParameter("footer", userinfo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rptTitle));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvBomAppList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HyFOrderPrint = (HyperLink)e.Row.FindControl("HyFOrderPrint");
                HyperLink HypShipMark = (HyperLink)e.Row.FindControl("hypShipMark"); //Shipping Mark
                HyperLink HypShipMarkV2 = (HyperLink)e.Row.FindControl("hypShipMarkV2"); //Shipping Mark
                HyperLink HyLOrderPrint = (HyperLink)e.Row.FindControl("HyLOrderPrint");
                HyperLink hypbtnMatReq = (HyperLink)e.Row.FindControl("hypbtnMatReq");
                HyperLink HypExpPlan = (HyperLink)e.Row.FindControl("HypExpPlan");
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();

                string colorid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "colorid")).Trim().ToString();
                string dayid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "category")).Trim().ToString();

                string date = "";
                if (dayid == "00000000")
                {
                    date = "01-Jan-1900"; ;
                }
                else
                {
                    string a = dayid.Substring(4, 2);
                    string b = dayid.Substring(0, 4);
                    string c = ASTUtility.Right(dayid, 2);
                    date = Convert.ToDateTime( a + "/" + c + "/" + b).ToString("dd-MMM-yyyy");
                }

                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();
                string formattype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.ToString();
                HyFOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=import&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=" + formattype;
                //Shipping Mark HyperLink Print
                HypShipMark.NavigateUrl = "~/F_19_EXP/ExpPrint?Type=ShipMark&mlccod=" + mlccod + "&dayid=" + dayid + "&format=" + formattype;
                HypShipMarkV2.NavigateUrl = "~/F_19_EXP/ExpPrint?Type=ShipMarkV2&mlccod=" + mlccod + "&dayid=" + dayid + "&format=" + formattype;
                
                HyLOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=local&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=" + formattype;
                hypbtnMatReq.NavigateUrl = "~/F_03_CostABgd/MlcMatReq?Type=Entry&actcode=" + mlccod + "&genno=" + dayid + "&sircode=" + styleid + colorid;
                HypExpPlan.NavigateUrl = "~/F_05_ProShip/ExportPlanVsAchiv?Type=Entry&actcode=" + mlccod + "&sircode=" + styleid + colorid + dayid;

            }

        }

        protected void gvPendBOM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HyFOrderPrint = (HyperLink)e.Row.FindControl("HyFOrderPrint");

                HyperLink HyLOrderPrint = (HyperLink)e.Row.FindControl("HyLOrderPrint");
                //HyperLink hypbtnMatReq = (HyperLink)e.Row.FindControl("hypbtnMatReq");
                //HyperLink HypExpPlan = (HyperLink)e.Row.FindControl("HypExpPlan");
                string styleid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "styleid")).Trim().ToString();

                string colorid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "colorid")).Trim().ToString();
                string dayid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dayid")).Trim().ToString();

                string date = "";
                //if (dayid == "00000000")
                //{
                //    date = "01-Jan-1900"; ;
                //}
                //else
                //{
                //    date = Convert.ToDateTime(dayid.Substring(4, 2) + "/" + ASTUtility.Right(dayid, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                //}

                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).Trim().ToString();
                string formattype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.ToString();
                HyFOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=import&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=" + formattype;
                HyLOrderPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=local&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=" + formattype;
                //hypbtnMatReq.NavigateUrl = "~/F_03_CostABgd/MlcMatReq.aspx?Type=Entry&actcode=" + mlccod + "&genno=" + dayid + "&sircode=" + styleid + colorid;
                //HypExpPlan.NavigateUrl = "~/F_05_ProShip/ExportPlanVsAchiv.aspx?Type=Entry&actcode=" + mlccod + "&sircode=" + styleid + colorid + dayid;



            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");

            DataView dv21 = ds2.Tables[0].DefaultView;
            DataRowView newRow = dv21.AddNew();
            DataView dv22 = new DataView(ds2.Tables[0]);
            dv22.RowFilter = ("sircode not like '000000000000'");


            newRow = dv22.AddNew();
            newRow["sircode"] = "000000000000";
            newRow["sirdesc"] = "----All----";
            dv22.ToTable().Rows.Add(newRow);


            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = dv22;
            this.ddlBuyer.DataBind();
            this.ddlBuyer.SelectedValue = "000000000000";
        }
    }

    public class BOMSummaryReport
    {
        public string monthnme { get; set; }
        public double ordqty { get; set; }
        public double expqty { get; set; }

    }
}