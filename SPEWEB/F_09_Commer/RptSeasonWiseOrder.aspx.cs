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

namespace SPEWEB.F_09_Commer
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ProcessAccess _processAccess = new ProcessAccess();
        Common _common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "SeasonBySeason") ? "Season by Season Supplier's Summary" :
                                                                    (type == "SeasonOverview") ? "Season Overview Of Materials" :
                                                                    (type == "MasterPOR") ? "Master PO Report" : "Materials Price Variance Report";
                //this.lblHeaderTitle.Text = (this.Request.QueryString["Type"] == "OrdVsSup") ? "ORDER VS SUPPLY" : "PURCHASE TRACKING INFORMATION";
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetSeason();
                this.GetCodeBookList();
                this.MaxPriceButton();

                if (Request.QueryString["Type"] == "SeasonBySeason")
                {
                    this.GetSupplier();
                    divSubGroup.Visible = false;
                    divMatGroup.Visible = false;
                    divddlSupplierName.Visible = true;
                    lblAggrgt.Visible = true;
                    ddlAggrgt.Visible = true;
                }
                else if (Request.QueryString["Type"] == "MasterPOR")
                {
                    this.GetSupplier();
                    divSubGroup.Visible = false;
                    divMatGroup.Visible = false;
                    divddlSupplierName.Visible = true;
                }

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Text = "Delete Selected";
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lnkbtnTranList_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetSeason()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            //ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();

        }

        private void MaxPriceButton()
        {
            string type = Request.QueryString["Type"];

            switch (type)
            {
                case "PriceVariance":
                    this.ChkMaxPrice.Visible = true;
                    break;
                case "SeasonBySeason":
                case "SeasonOverview":
                case "MasterPOR":
                    this.ChkMaxPrice.Visible = false;
                    break;
            }
        }

        private void GetSupplier()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string mProjCode = this.ddlProject.SelectedValue.ToString();
            string mSrchTxt = "%";
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                return;
            }
            Session["tblSup"] = ds1.Tables[0];
            this.ddlSupplierName.DataTextField = "ssirdesc1";
            this.ddlSupplierName.DataValueField = "ssircode";
            this.ddlSupplierName.DataSource = ds1.Tables[0];
            this.ddlSupplierName.DataBind();
        }

        protected void GetCodeBookList()
        {
            try
            {
                string Querytype = this.Request.QueryString["Type"];
                string coderange = "04%";

                string comcod = _common.GetCompCode();
                DataSet dsone = _processAccess.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                Session["tblmatsubhead"] = dsone.Tables[1];
                this.ddlCodeBook.DataTextField = "sircode";
                this.ddlCodeBook.DataValueField = "sircode1";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
                this.ddlCodeBook_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
            }
        }

        protected void ddlCodeBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mathead = this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            DataTable dt = (DataTable)Session["tblmatsubhead"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode1 like '" + mathead + "'";
            this.ddlGroup.DataTextField = "sircode";
            this.ddlGroup.DataValueField = "sircode1";
            this.ddlGroup.DataSource = dv.ToTable();
            this.ddlGroup.DataBind();
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {

            this.LoadData();
            this.LoadGv();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGv();
            this.Show_Season_By_Season_GV();
            this.Get_Master_PO_Rpt();
        }

        private void LoadData()
        {
            string type = Request.QueryString["Type"];

            switch (type)
            {
                case "SeasonBySeason":
                    this.MV1.ActiveViewIndex = 0;
                    this.Get_Season_By_Season_Data();
                    break;
                case "SeasonOverview":
                    this.MV1.ActiveViewIndex = 1;
                    this.Get_Season_Overview_Data();
                    break;
                case "PriceVariance":
                    this.MV1.ActiveViewIndex = 2;
                    this.GetMaterialPriceVariance();
                    break;
                case "MasterPOR":
                    this.MV1.ActiveViewIndex = 3;
                    this.Get_Master_PO_Rpt();
                    break;
            }
        }

        private void Get_Season_By_Season_Data()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string season = "";
            string aggregate = this.ddlAggrgt.SelectedValue.ToString();
            string shipperwise = (this.ChckShipper1.Checked == true) ? "TRUE" : "FALSE";
            string supCod = this.ddlSupplierName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSupplierName.SelectedValue.ToString() + "%";
            int i = 0;
            foreach (ListItem item in DdlSeason.Items)
            {
                if (item.Selected)
                {
                    season += item.Value;
                    i++;
                }

                if (i > 9)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Less  Then 10 Sesion');", true);

                    return;
                }

            }
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SEASON_WISE_ORDER_SUMMARY", todate, season, supCod, shipperwise, aggregate, "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvOrderStatus.DataSource = null;
                this.gvOrderStatus.DataBind();
                return;
            }

            DataTable dt1 = ds1.Tables[0];
            Session["tblstatus"] = dt1;
            ViewState["tblseasonsum"] = ds1.Tables[1];
            ViewState["tblpricevar"] = ds1.Tables[2];
            ViewState["tblSpcfpricevar"] = ds1.Tables[3];

        }

        private void Get_Master_PO_Rpt()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string season = "";
            string asShipper = (this.ChckShipper1.Checked == true) ? "TRUE" : "FALSE";
            string supCod = this.ddlSupplierName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSupplierName.SelectedValue.ToString() + "%";
            int i = 0;
            foreach (ListItem item in DdlSeason.Items)
            {
                if (item.Selected)
                {
                    season += item.Value;
                    i++;
                }

                if (i > 9)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Less  Then 10 Sesion');", true);

                    return;
                }

            }
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "MASTER_PO_REPORT", season, supCod, asShipper, "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvMasterPOR.DataSource = null;
                this.gvMasterPOR.DataBind();
                return;
            }

            ViewState["tblMasterPOR"] = ds1.Tables[0];
            this.gvMasterPOR.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMasterPOR.DataSource = (DataTable)ViewState["tblMasterPOR"];
            this.gvMasterPOR.DataBind();
            this.FooterCalculation3();

        }

        private void GetMaterialPriceVariance()
        {

            string comcod = this.GetCompCode();

            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string season = "";
            string group = this.ddlGroup.SelectedValue == "040000000000" ? "%%" : this.ddlGroup.SelectedValue + "%";

            int i = 0;
            foreach (ListItem item in DdlSeason.Items)
            {
                if (item.Selected)
                {
                    season += item.Value;
                    i++;
                }

                if (i > 9)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Less  Then 10 Sesion');", true);

                    return;
                }

            }

            string maxPrice = (ChkMaxPrice.Checked == true) ? "maxPrice" : "";

            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "MATERIAL_PRICE_VARIANCE", todate, season, group, maxPrice, "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvVariance.DataSource = null;
                this.gvVariance.DataBind();
                return;
            }

            DataTable dt1 = ds1.Tables[0];
            ViewState["PriceVariance"] = dt1;

        }

        private void Get_Season_Overview_Data()
        {
            Session.Remove("tblSeasonOverview");
            string comcod = this.GetCompCode();

            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string season = "";
            int i = 0;

            foreach (ListItem item in DdlSeason.Items)
            {
                if (item.Selected)
                {
                    season += item.Value;
                    i++;
                }

                //if (i > 9)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Less  Then 10 Sesion');", true);
                //    return;
                //}
            }

            DataSet ds = _processAccess.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "SEASON_WISE_OVERVIEW_OF_MATERIALS", todate, season, "", "", "", "", "", "", "");

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }

            Session["tblSeasonOverview"] = ds.Tables[0];
        }

        private void LoadGv()
        {
            string type = Request.QueryString["Type"];

            switch (type)
            {
                case "SeasonBySeason":
                    Show_Season_By_Season_GV();
                    break;
                case "SeasonOverview":
                    Show_Season_Overview_GV();
                    break;
                case "PriceVariance":
                    DataTable dt = (DataTable)ViewState["PriceVariance"];

                    if (dt.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);

                        return;
                    }

                    this.gvVariance.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvVariance.DataSource = dt;
                    this.gvVariance.DataBind();

                    if (ChkMaxPrice.Checked == true)
                    {
                        this.gvVariance.Columns[2].Visible = false;
                    }
                    else
                    {
                        this.gvVariance.Columns[2].Visible = true;
                    }

                    this.PrepareExcelDownload(dt);
                    break;
            }
        }

        private void Show_Season_By_Season_GV()
        {
            DataTable dt = (DataTable)Session["tblstatus"];
            DataTable tblseasonsum = (DataTable)ViewState["tblseasonsum"];

            if (dt == null || tblseasonsum == null)
            {
                return;
            }

            int i = 0;
            foreach (ListItem item in DdlSeason.Items)
            {
                if (item.Selected)
                {
                    if (i > 10)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select 10 Sesion Or Less');", true);
                        return;
                    }

                    int col = 2 + i;
                    this.gvOrderStatus.Columns[col].HeaderText = item.Text;

                    i++;
                }
            }

            this.gvOrderStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvOrderStatus.DataSource = dt;
            this.gvOrderStatus.DataBind();
            this.FooterCalculation(dt);

            this.gvPriceVar.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvPriceVar.DataSource = (DataTable)ViewState["tblpricevar"];
            this.gvPriceVar.DataBind();
            this.FooterCalculation1();


            this.gvSpcfPriceVar.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSpcfPriceVar.DataSource = (DataTable)ViewState["tblSpcfpricevar"];
            this.gvSpcfPriceVar.DataBind();
            this.FooterCalculation2();


            var seasonsum = tblseasonsum.DataTableToList<SPEENTITY.C_09_Commer.SeasonSum>();
            var jsonSerialiser = new JavaScriptSerializer();
            var seasonsum_json = jsonSerialiser.Serialize(seasonsum);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + seasonsum_json + "')", true);
        }

        private void Show_Season_Overview_GV()
        {
            DataTable dt = (DataTable)Session["tblSeasonOverview"];

            if (dt == null)
            {
                return;
            }

            gvSeasonOverview.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            gvSeasonOverview.DataSource = dt;
            gvSeasonOverview.DataBind();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvs1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s1)", "")) ? 0.00 : dt.Compute("sum(s1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvs2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s2)", "")) ? 0.00 : dt.Compute("sum(s2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvs3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s3)", "")) ? 0.00 : dt.Compute("sum(s3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvs4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s4)", "")) ? 0.00 : dt.Compute("sum(s4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvs5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s5)", "")) ? 0.00 : dt.Compute("sum(s5)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvs6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s6)", "")) ? 0.00 : dt.Compute("sum(s6)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvs7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s7)", "")) ? 0.00 : dt.Compute("sum(s7)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvs8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s8)", "")) ? 0.00 : dt.Compute("sum(s8)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvs9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s9)", "")) ? 0.00 : dt.Compute("sum(s9)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvs10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(s10)", "")) ? 0.00 : dt.Compute("sum(s10)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }

        private void FooterCalculation1()
        {
            DataTable dt = (DataTable)ViewState["tblpricevar"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvo1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(o1)", "")) ? 0.00 : dt.Compute("sum(o1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvo2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(o2)", "")) ? 0.00 : dt.Compute("sum(o2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvo3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(o3)", "")) ? 0.00 : dt.Compute("sum(o3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvo4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(o4)", "")) ? 0.00 : dt.Compute("sum(o4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvo5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(o5)", "")) ? 0.00 : dt.Compute("sum(o5)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvr1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r1)", "")) ? 0.00 : dt.Compute("sum(r1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvr2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r2)", "")) ? 0.00 : dt.Compute("sum(r2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvr3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r3)", "")) ? 0.00 : dt.Compute("sum(r3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvr4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r4)", "")) ? 0.00 : dt.Compute("sum(r4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvr5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r5)", "")) ? 0.00 : dt.Compute("sum(r5)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvPriceVar.FooterRow.FindControl("lgvamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }

        private void FooterCalculation2()
        {
            DataTable dt = (DataTable)ViewState["tblSpcfpricevar"];
            if (dt.Rows.Count == 0)
                return;

            DataView dv = new DataView(dt);
            dv.RowFilter = "grp = 'A'";
            dt = dv.ToTable();

            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvo1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(o1)", "")) ? 0.00 : dt.Compute("sum(o1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvo2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(o2)", "")) ? 0.00 : dt.Compute("sum(o2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvo3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(o3)", "")) ? 0.00 : dt.Compute("sum(o3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvo4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(o4)", "")) ? 0.00 : dt.Compute("sum(o4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvo5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(o5)", "")) ? 0.00 : dt.Compute("sum(o5)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvr1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r1)", "")) ? 0.00 : dt.Compute("sum(r1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvr2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r2)", "")) ? 0.00 : dt.Compute("sum(r2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvr3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r3)", "")) ? 0.00 : dt.Compute("sum(r3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvr4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r4)", "")) ? 0.00 : dt.Compute("sum(r4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvr5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r5)", "")) ? 0.00 : dt.Compute("sum(r5)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSpcfPriceVar.FooterRow.FindControl("lgrvamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }

        private void FooterCalculation3()
        {
            DataTable dt = (DataTable)ViewState["tblMasterPOR"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvMasterPOR.FooterRow.FindControl("ttlLcVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lcvalue)", "")) ? 0.00 : dt.Compute("sum(lcvalue)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMasterPOR.FooterRow.FindControl("ttlpoqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ? 0.00 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMasterPOR.FooterRow.FindControl("ttlpoVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordamt)", "")) ? 0.00 : dt.Compute("sum(ordamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMasterPOR.FooterRow.FindControl("ttlinvqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ttlshipqty)", "")) ? 0.00 : dt.Compute("sum(ttlshipqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMasterPOR.FooterRow.FindControl("ttlLcamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lcpayamt)", "")) ? 0.00 : dt.Compute("sum(lcpayamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMasterPOR.FooterRow.FindControl("ttlLclink")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lclinkqty)", "")) ? 0.00 : dt.Compute("sum(lclinkqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMasterPOR.FooterRow.FindControl("ttlrcvQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rcvqty)", "")) ? 0.00 : dt.Compute("sum(rcvqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMasterPOR.FooterRow.FindControl("ttlqcqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qcqty)", "")) ? 0.00 : dt.Compute("sum(qcqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvMasterPOR.FooterRow.FindControl("ttlcstbal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(costbalqty)", "")) ? 0.00 : dt.Compute("sum(costbalqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void gvOrderStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrderStatus.PageIndex = e.NewPageIndex;
            this.LoadGv();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];

            switch (type)
            {
                case "SeasonBySeason":
                    this.Print_Season_By_Season_Summary();
                    break;
                case "SeasonOverview":
                    this.Print_Season_Overview_Data();
                    break;
                case "MasterPOR":
                    this.Print_MasterPORpt();
                    break;
            }

        }

        private void Print_MasterPORpt()
        {
            DataTable dt = (DataTable)ViewState["tblMasterPOR"];
            var lst = dt.DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.MasterOReport>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string asonDate = "As On Date: " + Convert.ToDateTime(txttodate.Text).ToString("dd-MMM-yyyy");
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_10_Procur.RptMasterPOReport", lst, "", "");
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Master PO Report"));
            rpt1.SetParameters(new ReportParameter("asondate", asonDate));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void Print_Season_By_Season_Summary()
        {
            DataTable dt = (DataTable)Session["tblstatus"];
            DataTable dt2 = (DataTable)ViewState["tblseasonsum"];

            var lst = dt.DataTableToList<SPEENTITY.C_09_Commer.RptSeasonBySeasonSupplierSummary>();
            var lst2 = dt2.DataTableToList<SPEENTITY.C_09_Commer.SeasonSum>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string hostname = hst["hostname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string asondate = "As On Date: " + Convert.ToDateTime(txttodate.Text).ToString("dd-MMM-yyyy");
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptSeasonBySeasonSupplySummary", lst, lst2, "");
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Season By Season Suppliers' Summary"));
            rpt1.SetParameters(new ReportParameter("asondate", asondate));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            int i = 1;
            foreach (ListItem item in DdlSeason.Items)
            {
                if (item.Selected)
                {
                    if (i > 10)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Less  Then 10 Sesion');", true);
                        return;
                    }
                    rpt1.SetParameters(new ReportParameter("s" + i, item.Text));

                    i++;
                }
            }

            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void Print_Season_Overview_Data()
        {
            DataTable dt = (DataTable)Session["tblSeasonOverview"];

            var lst = dt.DataTableToList<SPEENTITY.C_09_Commer.RptSeasonOverviewOfMaterials>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string hostname = hst["hostname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string asondate = "As On Date: " + Convert.ToDateTime(txttodate.Text).ToString("dd-MMM-yyyy");
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptSeasonOverviewOfMaterials", lst, "", "");
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Season Overview Of Materials"));
            rpt1.SetParameters(new ReportParameter("asondate", asondate));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvSeasonOverview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSeasonOverview.PageIndex = e.NewPageIndex;
            this.Show_Season_Overview_GV();
        }

        protected void gvVariance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvVariance.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["PriceVariance"];
            this.gvVariance.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvVariance.DataSource = dt;
            this.gvVariance.DataBind();
        }

        protected void PrepareExcelDownload(DataTable dt)
        {
            try
            {
                if (dt == null) return;

                GridView gvExcel = new GridView();
                gvExcel.AllowPaging = false;
                gvExcel.DataSource = dt;
                gvExcel.DataBind();

                Session["Report1"] = gvExcel;
                lnkbtnExptExcel.Visible = true;
                lnkbtnExptExcel.NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception Ex)
            {
            }
        }

        protected void gvPriceVar_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridView gv = sender as GridView;

            DataTable dtseason = (DataTable)ViewState["tblseasonsum"];

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow extraHeader1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell1 = new TableCell();
                cell1.ColumnSpan = 3;
                cell1.Text = "";
                extraHeader1.Cells.Add(cell1);


                int i = 1;
                foreach (ListItem item in DdlSeason.Items)
                {
                    if (item.Selected)
                    {
                        if (i > 10)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select 10 Sesion Or Less');", true);
                            return;
                        }


                        TableCell cell2 = new TableCell();
                        cell2.ColumnSpan = 3;
                        cell2.HorizontalAlign = HorizontalAlign.Center;
                        cell2.Text = item.Text.ToString();
                        cell2.ForeColor = System.Drawing.Color.DarkRed;
                        extraHeader1.Cells.Add(cell2);
                        i++;
                    }
                }
                //TableCell cell3 = new TableCell();
                //cell3.Font.Bold = true;
                //cell3.ColumnSpan = 4;
                //cell3.Text = "Total: " + total.ToString("#,##0");
                //cell3.BackColor = System.Drawing.Color.LightSkyBlue;
                //extraHeader1.Cells.Add(cell3);

                gv.Controls[0].Controls.AddAt(0, extraHeader1);
            }
        }

        protected void gvSpcfPriceVar_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridView gv = sender as GridView;

            DataTable dtseason = (DataTable)ViewState["tblseasonsum"];

            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridViewRow extraHeader1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell1 = new TableCell();
                cell1.ColumnSpan = 4;
                cell1.Text = "";
                extraHeader1.Cells.Add(cell1);


                int i = 1;
                foreach (ListItem item in DdlSeason.Items)
                {
                    if (item.Selected)
                    {
                        if (i > 10)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select 10 Sesion Or Less');", true);
                            return;
                        }


                        TableCell cell2 = new TableCell();
                        cell2.ColumnSpan = 3;
                        cell2.HorizontalAlign = HorizontalAlign.Center;
                        cell2.Text = item.Text.ToString();
                        cell2.ForeColor = System.Drawing.Color.DarkRed;
                        extraHeader1.Cells.Add(cell2);
                        i++;
                    }
                }


                gv.Controls[0].Controls.AddAt(0, extraHeader1);
            }
        }

        protected void imgbtnprint1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblpricevar"];

            var lst1 = dt.DataTableToList<SPEENTITY.C_09_Commer.RptMatPriceVariance>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string asondate = "As On Date: " + Convert.ToDateTime(txttodate.Text).ToString("dd-MMM-yyyy");
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptMatPriceVariance", lst1, "", "");
            rpt1.EnableExternalImages = true;



            int i = 1;
            foreach (ListItem item in DdlSeason.Items)
            {
                if (item.Selected)
                {
                    if (i <= 5)
                    {
                        rpt1.SetParameters(new ReportParameter("Season" + i.ToString(), item.Text));
                    }
                    i++;
                }
            }

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Material Price Variance"));
            rpt1.SetParameters(new ReportParameter("asondate", asondate));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;

            string pType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "ExecutePrint('" + pType + "');", true);

        }

        protected void imgbtnprint2_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblSpcfpricevar"];
            var lst2 = dt.DataTableToList<SPEENTITY.C_09_Commer.RptMatPriceVariance>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string asondate = "As On Date: " + Convert.ToDateTime(txttodate.Text).ToString("dd-MMM-yyyy");
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptMatSpcfPriceVariance", lst2, "", "");
            rpt1.EnableExternalImages = true;

            int i = 1;
            foreach (ListItem item in DdlSeason.Items)
            {
                if (item.Selected)
                {
                    if (i <= 5)
                    {
                        rpt1.SetParameters(new ReportParameter("Season" + i.ToString(), item.Text));
                    }
                    i++;
                }
            }

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Material Specification Price Variance"));
            rpt1.SetParameters(new ReportParameter("asondate", asondate));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;

            string pType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "ExecutePrint('" + pType + "');", true);
        }

        protected void gvPriceVar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPriceVar.PageIndex = e.NewPageIndex;
            this.Show_Season_By_Season_GV();
        }

        protected void gvSpcfPriceVar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSpcfPriceVar.PageIndex = e.NewPageIndex;
            this.Show_Season_By_Season_GV();
        }

        protected void gvMasterPOR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMasterPOR.PageIndex = e.NewPageIndex;
            this.Get_Master_PO_Rpt();
        }

        protected void gvSpcfPriceVar_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSub = (Label)e.Row.FindControl("lblSpcf2");
                Label lblQty1 = (Label)e.Row.FindControl("lblgrvo1");
                Label lblQty2 = (Label)e.Row.FindControl("lblgrvo2");
                Label lblQty3 = (Label)e.Row.FindControl("lblgrvo3");
                Label lblQty4 = (Label)e.Row.FindControl("lblgrvo4");
                Label lblQty5 = (Label)e.Row.FindControl("lblgrvo5");
                Label lblRate1 = (Label)e.Row.FindControl("lblgrvr1");
                Label lblRate2 = (Label)e.Row.FindControl("lblgrvr2");
                Label lblRate3 = (Label)e.Row.FindControl("lblgrvr3");
                Label lblRate4 = (Label)e.Row.FindControl("lblgrvr4");
                Label lblRate5 = (Label)e.Row.FindControl("lblgrvr5");
                Label lblAmt1 = (Label)e.Row.FindControl("lblgrvamt1");
                Label lblAmt2 = (Label)e.Row.FindControl("lblgrvamt2");
                Label lblAmt3 = (Label)e.Row.FindControl("lblgrvamt3");
                Label lblAmt4 = (Label)e.Row.FindControl("lblgrvamt4");
                Label lblAmt5 = (Label)e.Row.FindControl("lblgrvamt5");

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (grp == "B")
                {
                    lblSub.Font.Bold = true;
                    lblQty1.Font.Bold = true;
                    lblQty2.Font.Bold = true;
                    lblQty3.Font.Bold = true;
                    lblQty4.Font.Bold = true;
                    lblQty5.Font.Bold = true;
                    lblRate1.Font.Bold = true;
                    lblRate2.Font.Bold = true;
                    lblRate3.Font.Bold = true;
                    lblRate4.Font.Bold = true;
                    lblRate5.Font.Bold = true;
                    lblAmt1.Font.Bold = true;
                    lblAmt2.Font.Bold = true;
                    lblAmt3.Font.Bold = true;
                    lblAmt4.Font.Bold = true;
                    lblAmt5.Font.Bold = true;
                }
                else
                {
                    lblSub.Font.Bold = false;
                    lblQty1.Font.Bold = false;
                    lblQty2.Font.Bold = false;
                    lblQty3.Font.Bold = false;
                    lblQty4.Font.Bold = false;
                    lblQty5.Font.Bold = false;
                    lblRate1.Font.Bold = false;
                    lblRate2.Font.Bold = false;
                    lblRate3.Font.Bold = false;
                    lblRate4.Font.Bold = false;
                    lblRate5.Font.Bold = false;
                    lblAmt1.Font.Bold = false;
                    lblAmt2.Font.Bold = false;
                    lblAmt3.Font.Bold = false;
                    lblAmt4.Font.Bold = false;
                    lblAmt5.Font.Bold = false;
                }
            }
        }

    }
}