using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_15_Pro
{
    public partial class RptProduction : System.Web.UI.Page
    {

        ProcessAccess _processAccess = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");

                string type = Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "BalSheet") ? "Daily Production Balance Sheet"
                                                                : (type == "SizeBalSheet") ? "Size Production Balance Sheet"
                                                                : (type == "QltyNdPrd") ? "Daily Quality And Productivity Report" 
                                                                : (type == "ProductionReport") ? "Monthly Production Analytical Report" 
                                                                : (type == "DefParChart") ? "Defect Pareto Chart" : "Order-Defect Reject/Repair Report";
                
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                                             
                this.CommonButton();
                if (this.ddlmlccod.Items.Count == 0)
                {
                    this.GetSesson();
                }

                this.GetLotList();
                this.GetProcess();
                this.HideAndShowFields();
            }
        }

        private void HideAndShowFields()
        {

            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "BalSheet":
                    this.FieldDate.Visible = false;
                    this.FieldSeason.Visible = true;
                    this.FieldMasterLC.Visible = true;
                    this.FieldStyle.Visible = true;
                    this.RbPanel1.Visible = false;
                    this.pagesize.Visible = false;
                    break;
                case "SizeBalSheet":
                    this.FieldDate.Visible = false;
                    this.FieldSeason.Visible = true;
                    this.FieldMasterLC.Visible = true;
                    this.FieldStyle.Visible = true;
                    this.divProcess.Visible = false;
                    this.RbPanel1.Visible = false;
                    this.pagesize.Visible = false;
                    break;

                case "QltyNdPrd":
                    this.FieldDate.Visible = true;
                    this.FieldSeason.Visible = false;
                    this.FieldMasterLC.Visible = false;
                    this.FieldStyle.Visible = false;
                    this.RbPanel1.Visible = false;
                    this.pagesize.Visible = false;
                    break;

                case "ProductionReport":
                    this.lbldate.Visible = false;
                    this.lblFromDate.Visible = true;
                    this.FieldtoDate.Visible = true;
                    this.FieldSeason.Visible = false;
                    this.FieldSeason.Visible = false;
                    this.FieldMasterLC.Visible = false;
                    this.FieldStyle.Visible = false;
                    this.divProcess.Visible = false;
                    this.RbPanel1.Visible = false;
                    this.pagesize.Visible = false;
                    break;
                case "DefParChart":
                    this.lbldate.Visible = false;
                    this.lblFromDate.Visible = true;
                    this.FieldtoDate.Visible = true;
                    this.FieldSeason.Visible = false;
                    this.FieldSeason.Visible = false;
                    this.FieldMasterLC.Visible = false;
                    this.FieldStyle.Visible = false;
                    this.divProcess.Visible = true;
                    this.RbPanel1.Visible = true;
                    this.pagesize.Visible = false;
                    break;
                case "OrderDefect":
                    this.lbldate.Visible = false;
                    this.lblFromDate.Visible = true;
                    this.FieldtoDate.Visible = true;
                    this.FieldSeason.Visible = true;
                    this.FieldMasterLC.Visible = true;
                    this.FieldStyle.Visible = true;
                    this.divProcess.Visible = true;
                    this.RbPanel1.Visible = true;
                    this.pagesize.Visible = false;
                    break;
            }
        }

        public void CommonButton()
        {
            //   ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
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
            DdlSeason_SelectedIndexChanged(null, null);
        }

        private void GetLotList()
        {
            string comcod = GetCompCode();
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_SALGINF_INFORMATION", "28%", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            ViewState["tbllotlist"] = ds1.Tables[0];

        }

        private void GetProcess()
        {
            Session.Remove("tblprocess");
            string comcod = GetCompCode();
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPROCESS", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0].Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("procode <>'800100101099'");
            dt = dv.ToTable();
            DataRow dr = dt.NewRow();
            dr["prodesc"] = "All";
            dr["procode"] = "000000000000";
            dt.Rows.Add(dr);
            dt.DefaultView.Sort = "procode";

            this.ddlFromProcess.DataTextField = "prodesc";
            this.ddlFromProcess.DataValueField = "procode";
            this.ddlFromProcess.DataSource = dt.DefaultView.ToTable();
            this.ddlFromProcess.DataBind();
            Session["tblprocess"] = ds1.Tables[0];
            ds1.Dispose();

        }

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMasterLc();
        }

        private void GetMasterLc()
        {

            string comcod = GetCompCode();
            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", "1601%", "%", findseason, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = ds1.Tables[1];
            DataRow dr1 = dt1.NewRow();
            dr1["mlcdesc"] = "All";
            dr1["mlccod"] = "000000000000";
            dt1.Rows.Add(dr1);
            dt1.DefaultView.Sort = "mlccod";

            this.ddlmlccod.DataTextField = "mlcdesc";
            this.ddlmlccod.DataValueField = "mlccod";
            this.ddlmlccod.DataSource = dt1.DefaultView.ToTable();
            this.ddlmlccod.DataBind();
            ViewState["tblordstyle"] = ds1.Tables[0];

            ddlmlccod_SelectedIndexChanged(null, null);
        }

        protected void ddlmlccod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlmlccod.SelectedValue.ToString();
            DataTable dt2 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1 = dt2.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt2 = dv1.ToTable(true, "styledesc2", "stylecode1");
            DataRow dr2 = dt2.NewRow();
            dr2["styledesc2"] = "All";
            dr2["stylecode1"] = "00000000000000000000000000000000";
            dt2.Rows.Add(dr2);

            this.ddlStyle.DataTextField = "styledesc2";
            this.ddlStyle.DataValueField = "stylecode1";
            this.ddlStyle.DataSource = dt2;
            this.ddlStyle.DataBind();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "BalSheet":
                    this.FieldDate.Visible = false;
                    this.FieldSeason.Visible = true;
                    this.FieldMasterLC.Visible = true;
                    this.FieldStyle.Visible = true;
                    this.MVRptProduction.ActiveViewIndex = 0;
                    GetBalSheetData();
                    break;
                case "SizeBalSheet":
                    this.FieldDate.Visible = false;
                    this.FieldSeason.Visible = true;
                    this.FieldMasterLC.Visible = true;
                    this.FieldStyle.Visible = true;
                    this.MVRptProduction.ActiveViewIndex = 1;
                    GetSizeBalSheetData();
                    break;

                case "QltyNdPrd":
                    this.FieldDate.Visible = true;
                    this.FieldSeason.Visible = false;
                    this.FieldMasterLC.Visible = false;
                    this.FieldStyle.Visible = false;
                    this.MVRptProduction.ActiveViewIndex = 2;
                    GetQltyAndProdData();
                    break;

                case "ProductionReport":
                    this.lbldate.Visible = false;
                    this.lblFromDate.Visible = true;
                    this.FieldtoDate.Visible = true;
                    this.FieldSeason.Visible = false;
                    this.FieldSeason.Visible = false;
                    this.FieldMasterLC.Visible = false;
                    this.FieldStyle.Visible = false;
                    this.divProcess.Visible = false;
                    this.MVRptProduction.ActiveViewIndex = 3;
                    this.GetProductionReport();
                    break;
                case "DefParChart":
                    this.lbldate.Visible = false;
                    this.lblFromDate.Visible = true;
                    this.FieldtoDate.Visible = true;
                    this.FieldSeason.Visible = false;
                    this.FieldSeason.Visible = false;
                    this.FieldMasterLC.Visible = false;
                    this.FieldStyle.Visible = false;
                    this.divProcess.Visible = true;
                    this.MVRptProduction.ActiveViewIndex = 4;
                    this.GetDefParChart();
                    this.DefParChartGraph();
                    break;
                case "OrderDefect":
                    this.lbldate.Visible = false;
                    this.lblFromDate.Visible = true;
                    this.FieldtoDate.Visible = true;
                    this.FieldSeason.Visible = true;
                    this.FieldMasterLC.Visible = true;
                    this.FieldStyle.Visible = true;
                    this.divProcess.Visible = true;
                    this.RbPanel1.Visible = true;
                    this.MVRptProduction.ActiveViewIndex = 5;
                    this.GetOrderDefect();
                    break;
            }
        }

        private void GetOrderDefect()
        {
            string comcod = this.GetCompCode();
            string masterLc = this.ddlmlccod.SelectedValue == "000000000000" ? "%" : this.ddlmlccod.SelectedValue + "%";
            string style = this.ddlStyle.SelectedValue == "00000000000000000000000000000000" ? "%" : this.ddlStyle.SelectedValue.ToString().Substring(24, 8) + "%";
            string fromdate = this.txtDate.Text.ToString();
            string todate = this.txttoDate.Text.ToString();
            string state = this.rbtnList1.SelectedValue;
            string process = this.ddlFromProcess.SelectedValue == "000000000000" ? "%" : this.ddlFromProcess.SelectedValue + "%";

            DataSet ds = _processAccess.GetTransInfo(comcod, "SP_REPORT_PRODUCTION_QC ", "GET_ORDER_DEFECT_REJECT_REPAIR", fromdate, todate, state, masterLc, style, process);
            if (ds == null)
                return;

            ViewState["OrderDefect"] = ds.Tables[0];
            this.gvOrderDefect.DataSource = ViewState["OrderDefect"];
            this.gvOrderDefect.DataBind();
            ((Label)this.gvOrderDefect.FooterRow.FindControl("gvLblTtlQty")).Text = Convert.ToDouble((Convert.IsDBNull(ds.Tables[0].Compute("Sum(qty)", "")) ?
                0 : ds.Tables[0].Compute("Sum(qty)", ""))).ToString("#,##0.0;(#,##0.0); ");
        }

        private void GetDefParChart()
        {
            string comcod = this.GetCompCode();
            string fromdate = this.txtDate.Text.ToString();
            string todate = this.txttoDate.Text.ToString();
            string state = this.rbtnList1.SelectedValue;
            string process = this.ddlFromProcess.SelectedValue == "000000000000" ? "%" : this.ddlFromProcess.SelectedValue + "%";

            DataSet ds = _processAccess.GetTransInfo(comcod, "SP_REPORT_PRODUCTION_QC ", "GET_DEFECT_PARETO_CHART", fromdate, todate, state, process);
            if (ds == null)
                return;

            ViewState["DefParChart"] = ds.Tables[0];

            this.gvDefPareto.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDefPareto.DataSource = ViewState["DefParChart"];

            double totalQty = Convert.ToDouble(ds.Tables[0].Compute("Sum(qty)", "")); 
            double temp = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                double current = Convert.ToDouble(ds.Tables[0].Rows[i]["qty"]);
                ds.Tables[0].Rows[i]["cumqty"] = (current + temp).ToString();
                ds.Tables[0].Rows[i]["cumpercnt"] = ((current + temp) / totalQty) * 100;
                temp += current; 
            }

            this.gvDefPareto.DataBind();
            ((Label)this.gvDefPareto.FooterRow.FindControl("gvLblTtlDef")).Text = Convert.ToDouble((Convert.IsDBNull(ds.Tables[0].Compute("Sum(qty)", "")) ?
                0 : ds.Tables[0].Compute("Sum(qty)", ""))).ToString("#,##0;(#,##0); ");
        }

        private void DefParChartGraph()
        {
            DataTable dt = (DataTable)ViewState["DefParChart"];
            var defPar = dt.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.DefectPareto>();

            var jsonSerialiser = new JavaScriptSerializer();
            var rejection_json = jsonSerialiser.Serialize(defPar);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "DefParGraph('" + rejection_json + "')", true);
        }

        private void GetProductionReport()
        {
            ViewState["tblMonthlyAnalyticalRpt"] = null;
            string comcod = this.GetCompCode();
            string fromdate = this.txtDate.Text.ToString();
            string todate = this.txttoDate.Text.ToString();

            DataSet ds = _processAccess.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "MONTHLY_ANALYTICAL_REPORT", fromdate, todate);
            this.gvProductionReport.DataSource = ds;
            this.gvProductionReport.DataBind();
            ViewState["tblMonthlyAnalyticalRpt"] = ds.Tables[0];
        }

        private void GetBalSheetData()
        {
            ViewState["dtBalSheetOrdrQty"] = null;
            ViewState["dtBalSheetSizeDesc"] = null;

            string comcod = this.GetCompCode();

            string season = DdlSeason.SelectedValue == "00000" ? "%" : DdlSeason.SelectedValue;
            string masterLc = ddlmlccod.SelectedValue;
            string style = ddlStyle.SelectedValue;
            string product = style.Substring(0, 12);
            string color = style.Substring(12, 12);
            string dayid = style.Substring(24, 8);
            string process = this.ddlFromProcess.SelectedValue;

            DataSet dsBalSheet = _processAccess.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "DAILY_PRODUCTION_BALANCE_SHEET", masterLc, dayid, product, color, process);

            if(dsBalSheet != null)
            {
                if(dsBalSheet.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);

                    return;
                }

                ViewState["dtBalSheetOrdrQty"] = dsBalSheet.Tables[0];
                ViewState["dtBalSheetSizeDesc"] = dsBalSheet.Tables[1];

                this.LoadBalSheetGV();
            }
        }

        private void GetSizeBalSheetData()
        {
            ViewState["dtSizeBalSheetOrdrQty"] = null;
            ViewState["dtSizeBalSheetSizeDesc"] = null;

            string comcod = this.GetCompCode();

            string season = DdlSeason.SelectedValue == "00000" ? "%" : DdlSeason.SelectedValue;
            string masterLc = ddlmlccod.SelectedValue;
            string style = ddlStyle.SelectedValue;
            string product = style.Substring(0, 12);
            string color = style.Substring(12, 12);
            string dayid = style.Substring(24, 8);
            string process = this.ddlFromProcess.SelectedValue;

            DataSet dsSizeBalSheet = _processAccess.GetTransInfo(comcod, "SP_REPORT_PRODUCTION_INTERFACE", "PROCESS_PRODUCTION_BALANCE_SHEET", masterLc, dayid, product, color);

            if (dsSizeBalSheet != null)
            {
                if (dsSizeBalSheet.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);

                    return;
                }

                ViewState["dtSizeBalSheetOrdrQty"] = dsSizeBalSheet.Tables[0];
                ViewState["dtSizeBalSheetSizeDesc"] = dsSizeBalSheet.Tables[1];

                this.LoadSizeBalSheetGV();


                DataSet ds2 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ARTICLE_BASIC_INFORMATION", masterLc, "", "", "", "", "", "", "", "");
                if (ds2 != null)
                {
                    ViewState["dtSizeBalSheetArticleInfo"] = ds2.Tables[0];
                    if (ds2.Tables[0].Rows.Count == 0)
                        return;
                    this.BuyerName.Text = ds2.Tables[0].Rows[0]["buyername"].ToString();
                    this.lblbrand.Text = ds2.Tables[0].Rows[0]["brand"].ToString();
                    this.lblcolor.Text = ds2.Tables[0].Rows[0]["colordesc"].ToString();
                    //this.lblTrialOrderNo.Text = ds1.Tables[3].Rows[0]["trialordr"].ToString();
                    this.lblarticle.Text = ds2.Tables[0].Rows[0]["article"].ToString();
                    this.lblsizernge.Text = ds2.Tables[0].Rows[0]["sizerange"].ToString();
                    this.SmpleIMg.ImageUrl = (ds2.Tables[0].Rows[0]["images"].ToString() == "") ? "~/images/no_img_preview.png" : ds2.Tables[0].Rows[0]["images"].ToString();
                    this.TotalOrder.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");
                    this.lblCurrency.Text = ds2.Tables[0].Rows[0]["currency"].ToString();
                    this.lblCurcode.Text = ds2.Tables[0].Rows[0]["curcode"].ToString();
                    this.lblOrderNo.Text = dsSizeBalSheet.Tables[1].Rows[0]["orderno"].ToString();
                }
            }
        }

        private void GetQltyAndProdData()
        {
            ViewState["tblQltyNdProd1"] = null;
            ViewState["tblQltyNdProd2"] = null;

            string comcod = this.GetCompCode();

            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string process = this.ddlFromProcess.SelectedValue;

            DataSet dsQltyNdProd = _processAccess.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "DAILY_QUALITY_PRODUCTIVITY_REPORT", process, date, "", "", "", "");

            if(dsQltyNdProd != null)
            {
                if(dsQltyNdProd.Tables[0].Rows.Count == 0)
                {
                    this.LoadQltyNdProdGV();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    return;
                }

                ViewState["tblQltyNdProd1"] = dsQltyNdProd.Tables[0];
                ViewState["tblQltyNdProd2"] = dsQltyNdProd.Tables[1];

                this.LoadQltyNdProdGV();
            }
            
        }

        private void LoadQltyNdProdGV()
        {
            DataTable dtQltyNdProd1 = (DataTable)ViewState["tblQltyNdProd1"];
            DataTable dtQltyNdProd2 = (DataTable)ViewState["tblQltyNdProd2"];

            if(dtQltyNdProd1 == null && dtQltyNdProd2 == null)
            {
                this.gvQltyNdProd.DataSource = null;
                this.gvQltyNdProd.DataBind();
                return;
            }
            else
            {
                this.gvQltyNdProd.DataSource = dtQltyNdProd1;
                this.gvQltyNdProd.DataBind();
                this.CalculateTotalFooter();
            }
            
        }

        private void CalculateTotalFooter()
        {
            DataTable dtQltyNdProd = (DataTable)ViewState["tblQltyNdProd1"];

            if (dtQltyNdProd.Rows.Count > 0)
            {
                double totalMP = Convert.ToDouble(dtQltyNdProd.Compute("SUM(manpower)", string.Empty));
                double totalQC = Convert.ToDouble(dtQltyNdProd.Compute("SUM(qty)", string.Empty));

                ((Label)(this.gvQltyNdProd.FooterRow.FindControl("gvLblTtlMpQty"))).Text = totalMP.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvQltyNdProd.FooterRow.FindControl("gvLblTtlQcQty"))).Text = totalQC.ToString("#,##0;(#,##0); ");
            }
        }

        private void LoadBalSheetGV()
        {
            
            DataTable dtBalSheetSizeDesc = (DataTable) ViewState["dtBalSheetSizeDesc"];
            DataTable dtBalSheetOrdrQty = (DataTable) ViewState["dtBalSheetOrdrQty"];

            for (int i = 2; i < 41; i++)
            {
                gvBalSheet.Columns[i].Visible = false;
            }

            int index = 2;
            for (int i = 0; i < dtBalSheetSizeDesc.Rows.Count; i++)
            {
                gvBalSheet.Columns[index].Visible = true;
                gvBalSheet.Columns[index].HeaderText = dtBalSheetSizeDesc.Rows[i]["sizedesc"].ToString();
                index++;
            }

            gvBalSheet.DataSource = dtBalSheetOrdrQty;
            gvBalSheet.DataBind();

            //string mStyleID = "xxxxxxx";
            //for (int i = 0; i < dtBalSheet.Rows.Count; i++)
            //{
            //    if (dtBalSheet.Rows[i]["styleid"].ToString() == mStyleID)
            //        dtBalSheet.Rows[i]["StyleDesc"] = " >> ";
            //    mStyleID = dtBalSheet.Rows[i]["styleid"].ToString();
            //}
            //ViewState["tblOrderQty"] = dtBalSheet.DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCatLot>().OrderBy(p => p.styleid).OrderBy(p => p.colorid).ToList();
            //ViewState["tblOrderSize"] = ds1.Tables[1];
            //ViewState["tblratio"] = ds1.Tables[2];

            //for (int i = 5; i < 45; i++)
            //    this.gv1.Columns[i].Visible = false;

            //int indexx = 1;
            //for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            //{

            //    //  int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

            //    this.gv1.Columns[indexx + 4].Visible = true;
            //    this.gv1.Columns[indexx + 4].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            //    indexx++;
            //}
            //this.gv1.EditIndex = -1;


            //for (int i = 1; i < 41; i++)
            //    this.gv1ratio.Columns[i].Visible = false;

            //int indexy = 1;
            //for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            //{

            //    // int columid1 = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

            //    this.gv1ratio.Columns[indexy].Visible = true;
            //    this.gv1ratio.Columns[indexy].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            //    indexy++;
            //}
            //this.gv1ratio.EditIndex = -1;

            //this.Bind_Order_Allocation();
        }

        private void LoadSizeBalSheetGV()
        {
            DataTable dtBalSheetSizeDesc = (DataTable) ViewState["dtSizeBalSheetSizeDesc"];
            DataTable dtBalSheetOrdrQty = (DataTable) ViewState["dtSizeBalSheetOrdrQty"];

            for (int i = 2; i < 41; i++)
            {
                gvSizeBalSheet.Columns[i].Visible = false;
            }

            int index = 2;
            for (int i = 0; i < dtBalSheetSizeDesc.Rows.Count; i++)
            {
                gvSizeBalSheet.Columns[index].Visible = true;
                gvSizeBalSheet.Columns[index].HeaderText = dtBalSheetSizeDesc.Rows[i]["sizedesc"].ToString();
                index++;
            }

            gvSizeBalSheet.DataSource = dtBalSheetOrdrQty;
            gvSizeBalSheet.DataBind();
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {

                case "BalSheet":
                    this.PrintBalanceSheet();
                    break;
                case "SizeBalSheet":
                    this.PrintSizeBalanceSheet();
                    break;

                case "QltyNdPrd":
                    this.PrintQltyNdPrdReport();
                    break;
                                        
                case "ProductionReport":
                    this.PrintProductionReport();
                    break;
                case "DefParChart":
                    this.PrintDefectParChart();
                    break;
                case "OrderDefect":
                    this.PrintDefectOrder();
                    break;
            }
        }

        private void PrintProductionReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)ViewState["tblMonthlyAnalyticalRpt"];

            var lstOrderQty = dt.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.RptMonthlyProdAnalyticalReport>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptMntlyProdAnalytical", lstOrderQty, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("comlogo", comlogo));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Production Analytical Report"));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(comnam, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintQltyNdPrdReport()
        {
            DataTable dtQltyNdProd1 = (DataTable)ViewState["tblQltyNdProd1"];
            DataTable dtQltyNdProd2 = (DataTable)ViewState["tblQltyNdProd2"];

            if (dtQltyNdProd1 == null || dtQltyNdProd2 == null) return;

            var lstQltyNdProd1 = dtQltyNdProd1.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.RptQltyNdProd>();
            var lstQltyNdProd2 = dtQltyNdProd2.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.RptQltyNdProd2>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string process = this.ddlFromProcess.SelectedValue;
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptQltyAndProductivity", lstQltyNdProd1, lstQltyNdProd2, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("comlogo", comlogo));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("section", "Section: " + ddlFromProcess.SelectedItem.Text));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Quality And Productivity Report"));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(comnam, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintBalanceSheet()
        {
            DataTable dtSizeDesc = (DataTable)ViewState["dtBalSheetSizeDesc"];
            DataTable dtOrdrQty = (DataTable)ViewState["dtBalSheetOrdrQty"];

            //var lstSizeDesc = dtSizeDesc.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.RptDailyProdBalanceSheet>();
            var lstOrderQty = dtOrdrQty.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.RptDailyProdBalanceSheet>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string masterlc = ddlmlccod.SelectedItem.Text;
            string style = ddlStyle.SelectedItem.Text;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                            //new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptDailyProdBalSheet", lstOrderQty, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("comlogo", comlogo));
            rpt1.SetParameters(new ReportParameter("orderno", dtSizeDesc.Rows[0]["orderno"].ToString()));
            rpt1.SetParameters(new ReportParameter("article", masterlc));
            rpt1.SetParameters(new ReportParameter("section", "Section: "+ddlFromProcess.SelectedItem.Text));
            rpt1.SetParameters(new ReportParameter("customer", ""));
            rpt1.SetParameters(new ReportParameter("color", dtSizeDesc.Rows[0]["colordesc"].ToString()));

            DataView dv = dtOrdrQty.AsDataView();
            dv.RowFilter = "rwtype='ORDER'";
            DataTable dt3 = dv.ToTable();

            rpt1.SetParameters(new ReportParameter("orderqty", Convert.ToDouble(dt3.Rows[0]["totalqty"]).ToString("#,##0;(#,##0); ")));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Production Balance Sheet"));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compadd, username, printdate)));

            for (int i = 0; i < dtSizeDesc.Rows.Count; i++)
            {
                rpt1.SetParameters(new ReportParameter("size" + (i + 1).ToString(), dtSizeDesc.Rows[i]["sizedesc"].ToString()));
            }

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintSizeBalanceSheet()
        {
            DataTable dtSizeDesc = (DataTable)ViewState["dtSizeBalSheetSizeDesc"];
            DataTable dtOrdrQty = (DataTable)ViewState["dtSizeBalSheetOrdrQty"];
            DataTable dtArticleInfo = (DataTable)ViewState["dtSizeBalSheetArticleInfo"];

            //var lstSizeDesc = dtSizeDesc.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.RptDailyProdBalanceSheet>();
            var lstOrderQty = dtOrdrQty.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.RptSizeProdBalanceSheet>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string masterlc = ddlmlccod.SelectedItem.Text;
            string style = ddlStyle.SelectedItem.Text;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptSizeProdBalSheet", lstOrderQty, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("comlogo", comlogo));
            rpt1.SetParameters(new ReportParameter("orderno", dtSizeDesc.Rows[0]["orderno"].ToString()));
            rpt1.SetParameters(new ReportParameter("article", masterlc));
            rpt1.SetParameters(new ReportParameter("section", "Section: " + ddlFromProcess.SelectedItem.Text));
            rpt1.SetParameters(new ReportParameter("customer", dtArticleInfo.Rows[0]["buyername"].ToString()));
            rpt1.SetParameters(new ReportParameter("color", dtSizeDesc.Rows[0]["colordesc"].ToString()));

            rpt1.SetParameters(new ReportParameter("orderqty", Convert.ToDouble(dtArticleInfo.Rows[0]["ordrqty"]).ToString("#,##0;(#,##0); ")));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Size Production Balance Sheet"));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compadd, username, printdate)));

            for (int i = 0; i < dtSizeDesc.Rows.Count; i++)
            {
                rpt1.SetParameters(new ReportParameter("size" + (i + 1).ToString(), dtSizeDesc.Rows[i]["sizedesc"].ToString()));
            }

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintDefectParChart()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");        
            string footer = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rptTile = (this.rbtnList1.SelectedValue == "REJ") ? "Defect Pareto Chart (Reject)" : "Defect Pareto Chart (Repair)";

            DataTable dt1 = (DataTable)ViewState["DefParChart"];
            if (dt1 == null)
                return;

            var list1 = dt1.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassDefectParChart>();
            LocalReport Rpt1a = new LocalReport();

            Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_15_Pro.RptDefectParChart", list1, null, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1a.SetParameters(new ReportParameter("rptTile", rptTile));
            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintDefectOrder()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string footer = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rptTile = (this.rbtnList1.SelectedValue == "REJ") ? "Order Defect (Reject) Report" : "Order Defect (Repair) Report";

            DataTable dt1 = (DataTable)ViewState["OrderDefect"];
            if (dt1 == null)
                return;

            var list1 = dt1.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassDefectOrder>();
            LocalReport Rpt1a = new LocalReport();

            Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_15_Pro.RptDefectOrder", list1, null, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1a.SetParameters(new ReportParameter("rptTile", rptTile));
            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        //protected void gvDefPareto_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    string sortingDirection = string.Empty;
        //    if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
        //    {
        //        direction = System.Web.UI.WebControls.SortDirection.Descending;
        //        sortingDirection = "Desc";
        //    }
        //    else
        //    {
        //        direction = System.Web.UI.WebControls.SortDirection.Ascending;
        //        sortingDirection = "Asc";

        //    }
        //    DataTable dt = (DataTable)ViewState["DefParChart"];
                      
        //    DataView sortedView = new DataView(dt);
        //    sortedView.Sort = e.SortExpression + " " + sortingDirection;

        //    double totalQty = Convert.ToDouble(sortedView.ToTable().Compute("Sum(qty)", ""));
        //    double temp = 0;
        //    for (int i = 0; i < sortedView.ToTable().Rows.Count; i++)
        //    {
        //        double current = Convert.ToDouble(sortedView.ToTable().Rows[i]["qty"]);
        //        sortedView.ToTable().Rows[i]["cumqty"] = (current + temp).ToString();
        //        sortedView.ToTable().Rows[i]["cumpercnt"] = ((current + temp) / totalQty) * 100;
        //        temp += current;
        //    }

        //    gvDefPareto.DataSource = sortedView;
        //    gvDefPareto.DataBind();
        //    ((Label)this.gvDefPareto.FooterRow.FindControl("gvLblTtlDef")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty)", "")) ?
        //        0 : dt.Compute("Sum(qty)", ""))).ToString("#,##0;(#,##0); ");

        //    ViewState["DefParChart"] = sortedView.ToTable();
        //    DefParChartGraph();

        //}

        public System.Web.UI.WebControls.SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (System.Web.UI.WebControls.SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetOrderDefect();
            GetDefParChart();
        }
    }
}