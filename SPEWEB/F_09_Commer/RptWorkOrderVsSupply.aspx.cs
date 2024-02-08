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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Web.Services;
using System.Web.Script.Services;

namespace SPEWEB.F_09_Commer
{
    public partial class RptWorkOrderVsSupply : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "OrdVsSup") ?
                    "ORDER VS SUPPLY" : (this.Request.QueryString["Type"] == "SeasonSummary") ? " Season Wise Supply Summary" : (this.Request.QueryString["Type"] == "LeadTime") ? "Raw Materials Supply Lead Time" : "PURCHASE TRACKING INFORMATION";
                //   this.lblHeaderTitle.Text = (this.Request.QueryString["Type"] == "OrdVsSup") ? "ORDER VS SUPPLY" : "PURCHASE TRACKING INFORMATION";
                this.ViewSection();
                this.GetSesson();
            }
        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();




            DdlSeason2.DataTextField = "gdesc";
            DdlSeason2.DataValueField = "gcod";
            DdlSeason2.DataSource = ds1.Tables[0];
            DdlSeason2.DataBind();

            ddlSeason3.DataTextField = "gdesc";
            ddlSeason3.DataValueField = "gcod";
            ddlSeason3.DataSource = ds1.Tables[0];
            ddlSeason3.DataBind();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
                this.DdlSeason2.SelectedValue = season;
                this.ddlSeason3.SelectedValue = season;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void ViewSection()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "OrdVsSup":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ChkBalance.Checked = false;
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    //this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.GetOrderName();
                    this.imgbtnFindSupplier_Click(null, null);
                    break;

                case "OrderTk":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.txtorddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.GetOrderList();
                    break;

                case "SeasonSummary":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.GetCountry();
                    this.TxtAsOnDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    break;

                case "LeadTime":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.GetSesson();
                    this.txtAsOnDate2.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    break;

            }
        }

        private void GetOrderName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAMEFORREQ", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlOrderName.DataTextField = "pactdesc";
            this.ddlOrderName.DataValueField = "pactcode";
            this.ddlOrderName.DataSource = ds1.Tables[0];
            this.ddlOrderName.DataBind();
        }

        private void GetOrderList()
        {

            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtorddate.Text.Trim()).ToString("dd-MMM-yyyy");
            string orderlist = this.txtSrcOrder.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETORDERNO", date, orderlist, "", "", "", "", "", "", "");
            this.ddlOrderList.DataTextField = "orderno1";
            this.ddlOrderList.DataValueField = "orderno";
            this.ddlOrderList.DataSource = ds1.Tables[0];
            this.ddlOrderList.DataBind();
            ds1.Dispose();
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            //if (this.lnkbtnOk.Text == "Ok")
            //{


            //this.lblPage.Visible = true;
            //this.ddlpagesize.Visible = true;
            //this.LoadData();

            //}
            //else
            //{
            //    this.lnkbtnOk.Text = "Ok";
            //    this.ddlProjectName.Visible = true;
            //    this.lblProjectdesc.Visible = false;
            //    this.ddlSupplierName.Visible = true;
            //    this.lblSupplierDesc.Visible = false;
            //    this.lblPage.Visible = false;
            //    this.ddlpagesize.Visible = false;
            //    this.gvReqStatus.DataSource = null;
            //    this.gvReqStatus.DataBind();
            //}

            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "LeadTime":
                    this.LoadRawMatSupLeadTime();
                    break;

                default:
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.LoadData();
                    break;
            }
        }

        private void LoadRawMatSupLeadTime()
        {
            string comcod = this.GetCompCode();
            string season = this.ddlSeason3.SelectedValue;
            string date = Convert.ToDateTime(this.txtAsOnDate2.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "MATERIAL_SUPPLY_LEAD_TIME", date, season, "", "", "", "");
            ViewState["dsLeadTime"] = ds1;
            this.LoadGv();
        }

        private void LoadData()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            //string basis = this.rbtnList1.SelectedItem.Text;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlOrderName.SelectedValue.ToString();
            string ddltype = this.DDLType.SelectedValue.ToString();
            string supCod = this.ddlSupplierName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSupplierName.SelectedValue.ToString() + "%"; ;
            string balance = (this.ChkBalance.Checked) ? "woz" : "";
            string season = DdlSeason.SelectedItem.Value == "00000" ? "%" : DdlSeason.SelectedItem.Value + "%";
            string pricesum = (this.ChckPriceSum.Checked == true) ? "TRUE" : "FALSE";
            string shipperwise = (this.ChckShipper.Checked == true) ? "TRUE" : "FALSE";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "WORKORDER_VS_SUPPLY", supCod, "", todate, pactcode, balance, season, pricesum, shipperwise, ddltype);
            if (ds1 == null)
            {
                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
                return;
            }
            if (this.ChckPriceSum.Checked == true)
            {
                switch (ddltype)
                {
                    case "0":
                        this.gvReqStatus.Columns[1].Visible = true;
                        this.gvReqStatus.Columns[2].Visible = true;
                        this.gvReqStatus.Columns[3].Visible = false;
                        this.gvReqStatus.Columns[4].Visible = false;
                        this.gvReqStatus.Columns[5].Visible = false;
                        this.gvReqStatus.Columns[6].Visible = false;
                        this.gvReqStatus.Columns[7].Visible = false;
                        this.gvReqStatus.Columns[8].Visible = true;
                        this.gvReqStatus.Columns[9].Visible = true;
                        this.gvReqStatus.Columns[10].Visible = false;
                        this.gvReqStatus.Columns[11].Visible = true;
                        this.gvReqStatus.Columns[12].Visible = false;
                        this.gvReqStatus.Columns[13].Visible = false;
                        this.gvReqStatus.Columns[14].Visible = false;
                        this.gvReqStatus.Columns[15].Visible = true;
                        this.gvReqStatus.Columns[16].Visible = true;
                        this.gvReqStatus.Columns[17].Visible = true;
                        this.gvReqStatus.Columns[18].Visible = true;
                        this.gvReqStatus.Columns[19].Visible = false;
                        this.gvReqStatus.Columns[20].Visible = false;
                        this.gvReqStatus.Columns[21].Visible = false;
                        this.gvReqStatus.Columns[22].Visible = false;
                        this.gvReqStatus.Columns[23].Visible = false;
                        break;

                    case "1":
                        this.gvReqStatus.Columns[1].Visible = true;
                        this.gvReqStatus.Columns[2].Visible = false;
                        this.gvReqStatus.Columns[3].Visible = false;
                        this.gvReqStatus.Columns[4].Visible = false;
                        this.gvReqStatus.Columns[5].Visible = false;
                        this.gvReqStatus.Columns[6].Visible = false;
                        this.gvReqStatus.Columns[7].Visible = false;
                        this.gvReqStatus.Columns[8].Visible = true;
                        this.gvReqStatus.Columns[9].Visible = false;
                        this.gvReqStatus.Columns[10].Visible = false;
                        this.gvReqStatus.Columns[11].Visible = true;
                        this.gvReqStatus.Columns[12].Visible = true;
                        this.gvReqStatus.Columns[13].Visible = false;
                        this.gvReqStatus.Columns[14].Visible = false;
                        this.gvReqStatus.Columns[15].Visible = true;
                        this.gvReqStatus.Columns[16].Visible = false;
                        this.gvReqStatus.Columns[17].Visible = false;
                        this.gvReqStatus.Columns[18].Visible = false;
                        this.gvReqStatus.Columns[19].Visible = true;
                        this.gvReqStatus.Columns[20].Visible = false;
                        this.gvReqStatus.Columns[21].Visible = false;
                        this.gvReqStatus.Columns[22].Visible = false;
                        this.gvReqStatus.Columns[23].Visible = false;
                        break;

                    case "2":
                        this.gvReqStatus.Columns[1].Visible = true;
                        this.gvReqStatus.Columns[2].Visible = false;
                        this.gvReqStatus.Columns[3].Visible = false;
                        this.gvReqStatus.Columns[4].Visible = false;
                        this.gvReqStatus.Columns[5].Visible = false;
                        this.gvReqStatus.Columns[6].Visible = false;
                        this.gvReqStatus.Columns[7].Visible = false;
                        this.gvReqStatus.Columns[8].Visible = true;
                        this.gvReqStatus.Columns[9].Visible = true;
                        this.gvReqStatus.Columns[10].Visible = true;
                        this.gvReqStatus.Columns[11].Visible = true;
                        this.gvReqStatus.Columns[12].Visible = true;
                        this.gvReqStatus.Columns[13].Visible = false;
                        this.gvReqStatus.Columns[14].Visible = false;
                        this.gvReqStatus.Columns[15].Visible = true;
                        this.gvReqStatus.Columns[16].Visible = false;
                        this.gvReqStatus.Columns[17].Visible = false;
                        this.gvReqStatus.Columns[18].Visible = false;
                        this.gvReqStatus.Columns[19].Visible = true;
                        this.gvReqStatus.Columns[20].Visible = false;
                        this.gvReqStatus.Columns[21].Visible = false;
                        this.gvReqStatus.Columns[22].Visible = false;
                        this.gvReqStatus.Columns[23].Visible = false;
                        break;

                }
            }
            else
            {
                this.gvReqStatus.Columns[1].Visible = true;
                this.gvReqStatus.Columns[2].Visible = true;
                this.gvReqStatus.Columns[3].Visible = true;
                this.gvReqStatus.Columns[4].Visible = true;
                this.gvReqStatus.Columns[5].Visible = true;
                this.gvReqStatus.Columns[6].Visible = true;
                this.gvReqStatus.Columns[7].Visible = true;
                this.gvReqStatus.Columns[8].Visible = true;
                this.gvReqStatus.Columns[9].Visible = true;

                this.gvReqStatus.Columns[10].Visible = false;

                this.gvReqStatus.Columns[11].Visible = true;
                this.gvReqStatus.Columns[12].Visible = true;
                this.gvReqStatus.Columns[13].Visible = true;
                this.gvReqStatus.Columns[14].Visible = true;
                this.gvReqStatus.Columns[15].Visible = true;

                this.gvReqStatus.Columns[16].Visible = false;
                this.gvReqStatus.Columns[17].Visible = false;
                this.gvReqStatus.Columns[18].Visible = false;

                this.gvReqStatus.Columns[19].Visible = true;
                this.gvReqStatus.Columns[20].Visible = true;
                this.gvReqStatus.Columns[21].Visible = true;
                this.gvReqStatus.Columns[22].Visible = true;
                this.gvReqStatus.Columns[23].Visible = true;

            }


            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            Session["tblstatus"] = dt1;

            this.LoadGv();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Work Order Status";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void LoadGv()
        {

            DataTable dt = (DataTable)Session["tblstatus"];

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "OrdVsSup":
                    this.gvReqStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvReqStatus.DataSource = dt;
                    this.gvReqStatus.DataBind();

                    DataView dv = new DataView(dt);
                    dv.RowFilter = "grp = 'A'";
                    dt = dv.ToTable();

                    if (dt.Rows.Count > 0)
                    {
                        Session["Report1"] = gvReqStatus;
                        ((HyperLink)this.gvReqStatus.HeaderRow.FindControl("gvReqStatus_hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvTotalOrderQty")).Text = Convert.ToDouble(dt.Compute("Sum(ordrqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvTotalOrdrAmt")).Text = Convert.ToDouble(dt.Compute("Sum(ordamt)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvTotalReceived")).Text = Convert.ToDouble(dt.Compute("Sum(mrrqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvTotalBillQty")).Text = Convert.ToDouble(dt.Compute("Sum(billqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvTotalBalQty")).Text = Convert.ToDouble(dt.Compute("Sum(balqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvTotalShippedQty")).Text = Convert.ToDouble(dt.Compute("Sum(shippedqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvTotalShipBalQty")).Text = Convert.ToDouble(dt.Compute("Sum(shipbalqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");

                    }
                    break;

                case "OrderTk":
                    this.gvOrdertk.DataSource = dt;
                    this.gvOrdertk.DataBind();
                    break;

                case "LeadTime":
                    DataSet ds1 = (DataSet)ViewState["dsLeadTime"];

                    this.gvLeadTime.PageSize = Convert.ToInt32(this.ddlPageSize3.SelectedValue);
                    this.gvLeadTime.DataSource = ds1.Tables[0];
                    this.gvLeadTime.DataBind();

                    this.gvLeadTimeSumry.DataSource = ds1.Tables[1];
                    this.gvLeadTimeSumry.DataBind();

                    var leadtimesum = ds1.Tables[1].DataTableToList<SPEENTITY.C_09_Commer.RptSupLeadTimeSummary>();

                    var jsonSerialiser = new JavaScriptSerializer();
                    var leadtimesum_json = jsonSerialiser.Serialize(leadtimesum);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteLeadTimeGraph('" + leadtimesum_json + "')", true);

                    break;

            }

        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            string pactcode;
            switch (rpt)
            {
                case "OrdVsSup":
                    pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string reqno = dt1.Rows[0]["orderno"].ToString();
                    string supcode = dt1.Rows[0]["ssircode"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["ssircode"].ToString() == supcode && dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["orderno"].ToString() == reqno)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            reqno = dt1.Rows[j]["orderno"].ToString();
                            //supcode = dt1.Rows[j]["ssircode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            //dt1.Rows[j]["orderno"] = "";
                           // dt1.Rows[j]["orderdat"] = "";
                            supcode = dt1.Rows[j]["ssircode"].ToString();
                            dt1.Rows[j]["ssirdesc"] = "";
                            dt1.Rows[j]["refno"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["ssircode"].ToString() == supcode)
                            {
                                dt1.Rows[j]["pactdesc"] = "";

                            }
                            if (dt1.Rows[j]["ssircode"].ToString() == supcode)
                            {

                                dt1.Rows[j]["ssirdesc"] = "";

                            }

                            if (dt1.Rows[j]["orderno"].ToString() == reqno)
                            {
                                dt1.Rows[j]["orderno"] = "";
                                dt1.Rows[j]["orderdat"] = "";


                            }
                            supcode = dt1.Rows[j]["ssircode"].ToString();
                            reqno = dt1.Rows[j]["orderno"].ToString();
                            pactcode = dt1.Rows[j]["pactcode"].ToString();

                        }

                        //if ( dt1.Rows[j]["ssircode"].ToString() == supcode)
                        //{

                        //    supcode = dt1.Rows[j]["ssircode"].ToString();
                        //    dt1.Rows[j]["ssirdesc"] = "";
                        //}

                    }

                    break;

                case "OrderTk":
                    pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string rsircode = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {

                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            rsircode = dt1.Rows[j]["rsircode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["rsirdesc"] = "";
                            dt1.Rows[j]["rsirunit"] = "";
                            dt1.Rows[j]["spcfdesc"] = "";
                            dt1.Rows[j]["ordrqty"] = 0.0000000;

                        }

                        else
                        {

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                            {
                                pactcode = dt1.Rows[j]["pactcode"].ToString();
                                rsircode = dt1.Rows[j]["rsircode"].ToString();
                                dt1.Rows[j]["pactdesc"] = "";

                            }
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            rsircode = dt1.Rows[j]["rsircode"].ToString();

                        }
                    }

                    break;
            }

            return dt1;

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string ddltype = this.DDLType.SelectedValue.ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "OrdVsSup":

                    if (this.ChckPriceSum.Checked == true)
                    {
                        switch (ddltype)
                        {
                            case "1":
                                this.PriceSumVariance();
                                break;
                            case "0":
                            case "2":
                                this.PriceSpcfVariance();
                                break;
                        }
                    }
                    else
                    {
                        this.ProjectBasisStatus();
                    }
                    break;

                case "OrderTk":
                    this.RptOrderTrakcing();
                    break;

                case "SeasonSummary":

                    if(currentTabNow.Text == "summaryTab")
                    {
                        this.PrintSeasonSummary();
                    }
                    else if(currentTabNow.Text == "detailTab")
                    {
                        DataTable dtt1 = (DataTable)ViewState["DetailReprt"];

                        GridView gv = new GridView();
                        gv.AllowPaging = false;
                        gv.DataSource = dtt1;
                        gv.DataBind();

                        Session["Report1"] = gv;

                        Response.Redirect("../RptViewer.aspx?PrintOpt=GRIDTOEXCEL");
                    }
                    else
                    {
                        return;
                    }
                    
                    break;

                case "LeadTime":
                    this.PrintRawMatSupLeadTime();
                    break;
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Work Order Status";
                string eventdesc = "Print Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void PrintRawMatSupLeadTime()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string asOnDate = this.txtAsOnDate2.Text;
            string date = "As On Date: " + Convert.ToString(asOnDate);
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataSet dsLeadTime = (DataSet)ViewState["dsLeadTime"];

            var lst1 = dsLeadTime.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.RptRawMatSupLeadTime>();
            var lst2 = dsLeadTime.Tables[1].DataTableToList<SPEENTITY.C_09_Commer.RptSupLeadTimeSummary>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptRawMatSupLeadTime", lst1, lst2, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Raw Materials Supply Lead Time"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        
        }

        private void PrintSeasonSummary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string asOnDate = this.TxtAsOnDate.Text;
            string date = "As On Date: " + Convert.ToString(asOnDate);
            string season = ((DropDownList)DdlSeason2).SelectedItem.Text;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dtSeasonSum = (DataTable)ViewState["tblSeasoSum"];
            DataTable dtCntrySum = (DataTable)ViewState["tblCountrySum"];

            var listSeasonSum = dtSeasonSum.DataTableToList<SPEENTITY.C_09_Commer.RptSeasonSummary>();
            var listCntrySum = dtCntrySum.DataTableToList<SPEENTITY.C_09_Commer.RptCountrySummary>();

            var top20List = listSeasonSum.OrderByDescending(x => x.ordamt).Take(20).ToList();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptSeasonWiseSupplySummary", listSeasonSum, listCntrySum, top20List);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("Season", season));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Season Wise Supply Summary"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        
        }

        private void ProjectBasisStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string asOnDate = this.txttodate.Text;
            string date = "As On Date: " + Convert.ToString(asOnDate);
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string withoutprice = this.ChkPrice.Checked ? "true" : "false";

            DataTable dt = ((DataTable)Session["tblstatus"]).Copy();
            List<SPEENTITY.C_09_Commer.WorkOrderVsSupply> list = dt.DataTableToList<SPEENTITY.C_09_Commer.WorkOrderVsSupply>();

            if (ChkShipBal.Checked)
            {

                list = list.FindAll(x => x.shipbalqty > 0).ToList();
            }

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptWorkOrderVsSupply", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Work Order Supplier Wise"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("withoutprice", withoutprice));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ////string basis = this.rbtnList1.SelectedItem.Text;
            ////string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //DataTable dt1 = (DataTable)Session["tblstatus"];
            ////ReportDocument rrs1 = new RMGiRPT.R_03_Pro.RptWorkOrderStatus2();
            //ReportDocument rptDoc = new RMGiRPT.R_09_Commer.RptWorkOrderVsSupply();
            //TextObject rptCname = rptDoc.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;


            //TextObject txtFDate1 = rptDoc.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "(As on Date :" + todate + ")";

            //TextObject txtsupplier = rptDoc.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            //txtsupplier.Text = "Supplier: " + this.ddlSupplierName.SelectedItem.Text.Trim().Substring(14);

            //TextObject txtuserinfo = rptDoc.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptDoc.SetDataSource(dt1);
            //Session["Report1"] = rptDoc;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //this.ChkBalance.Checked = false;
        }

        private void PriceSumVariance()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string asOnDate = this.txttodate.Text;
            string date = "As On Date: " + Convert.ToString(asOnDate);
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string withoutprice = this.ChkPrice.Checked ? "true" : "false";

            DataTable dt = ((DataTable)Session["tblstatus"]).Copy();
            DataView dv = new DataView(dt);
            dv.RowFilter = "grp = 'A'";
            dt = dv.ToTable();
            List<SPEENTITY.C_09_Commer.WorkOrderVsSupply> list = dt.DataTableToList<SPEENTITY.C_09_Commer.WorkOrderVsSupply>();

            if (ChkShipBal.Checked)
            {
                list = list.FindAll(x => x.shipbalqty > 0).ToList();
            }

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptWorkOrderVsSupply2", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Material Summary Price Variance"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            //rpt1.SetParameters(new ReportParameter("withoutprice", withoutprice));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PriceSpcfVariance()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string asOnDate = this.txttodate.Text;
            string date = "As On Date: " + Convert.ToString(asOnDate);
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string withoutprice = this.ChkPrice.Checked ? "true" : "false";

            DataTable dt = ((DataTable)Session["tblstatus"]).Copy();

            List<SPEENTITY.C_09_Commer.WorkOrderVsSupply> list = dt.DataTableToList<SPEENTITY.C_09_Commer.WorkOrderVsSupply>();

            if (ChkShipBal.Checked)
            {

                list = list.FindAll(x => x.shipbalqty > 0).ToList();
            }

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptWorkOrderVsSupply3", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Material Specification Price Variance"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            //rpt1.SetParameters(new ReportParameter("withoutprice", withoutprice));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptOrderTrakcing()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblstatus"];


            //ReportDocument rptDoc = new RMGiRPT.R_09_Commer.RptOrderTracking();
            //TextObject rptCname = rptDoc.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rpttxtsupplier = rptDoc.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            //rpttxtsupplier.Text = dt1.Rows[0]["ssirdesc"].ToString();

            //TextObject rpttxtorderno = rptDoc.ReportDefinition.ReportObjects["txtorderno"] as TextObject;
            //rpttxtorderno.Text = "Order No: " + dt1.Rows[0]["orderno"].ToString();

            //TextObject rpttxtFDate = rptDoc.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtFDate.Text = "Date: " + Convert.ToDateTime(dt1.Rows[0]["orderdat"]).ToString("dd-MMM-yyyy");

            //TextObject txtuserinfo = rptDoc.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptDoc.SetDataSource(dt1);
            //Session["Report1"] = rptDoc;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void gvReqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvReqStatus.PageIndex = e.NewPageIndex;
            this.LoadGv();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGv();
        }

        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string mProjCode = this.ddlProject.SelectedValue.ToString();
            string mSrchTxt = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
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

        protected void lbtnOrderTk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string orderno = this.ddlOrderList.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTORDERTRACK", orderno, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvOrdertk.DataSource = null;
                this.gvOrdertk.DataBind();
                return;
            }

            Session["tblstatus"] = this.HiddenSameData(ds1.Tables[0]);
            ds1.Dispose();
            this.LoadGv();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Work Order Status";
                string eventdesc = "Show Order Tracking";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void imgbtnFindOrder_Click(object sender, EventArgs e)
        {
            this.GetOrderList();
        }

        protected void imgbtnFindOr_Click(object sender, EventArgs e)
        {
            this.GetOrderName();
        }

        protected void lbtngvOrderModel_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var rowIndex = gvr.RowIndex;
            var orderNo = ((HyperLink)this.gvReqStatus.Rows[rowIndex].FindControl("lbtngvOrderModel")).Text.ToString().Trim();

            DataTable dt = (DataTable)Session["tblstatus"];
            DataView dv = dt.AsDataView();
            dv.RowFilter = ("orderno1='" + orderNo + "'");

            DataSet ds = new DataSet();
            ds.Tables.Add(dv.ToTable());

            //Session["tblMatDesc"] = ds;

            this.GV_MatDesc.DataSource = ds.Tables[0];
            this.GV_MatDesc.DataBind();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "showMatDescriptionModal();", true);
        }

        protected void gvSHLbtnClick_Click(object sender, EventArgs e)
        {
            //int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //string challanno = ((LinkButton)this.gvShipmentHistory.Rows[index].FindControl("gvSHLblchallanno")).Text.ToString();

            //DataTable mainlist = (DataTable)ViewState["tblShipmentHistory"];
            //DataTable viewlist = (DataTable)ViewState["tblShipmentHistoryView"];

            //string flag = ((Label)gvShipmentHistory.Rows[index].FindControl("gvSHlblFlag")).Text.ToString();

            //DataView dv = new DataView();
            //dv = mainlist.DefaultView;
            //dv.RowFilter = ("challanno ='" + challanno + "'");
            //mainlist = dv.ToTable();
            //if (flag == "")
            //    return;
            //if (Convert.ToBoolean(flag) == false)
            //{
            //    foreach (DataRow dr2 in viewlist.Rows)
            //    {
            //        if (dr2["challanno"].ToString() != challanno)
            //        {
            //            dr2["flag"] = false;

            //        }
            //        else
            //        {
            //            dr2["flag"] = true;
            //        }
            //    }

            //    foreach (DataRow dr in mainlist.Rows)
            //    {
            //        DataRow drs = viewlist.NewRow();
            //        drs["comcod"] = dr["comcod"].ToString();
            //        drs["challanno"] = challanno;
            //        drs["logno"] = dr["logno"].ToString();
            //        drs["syspon"] = dr["syspon"].ToString();
            //        drs["challandate"] = dr["challandate"].ToString();
            //        drs["expecteddeldate"] = dr["expecteddeldate"].ToString();
            //        drs["rsircode"] = dr["rsircode"].ToString();
            //        drs["sirdesc"] = dr["sirdesc"].ToString();
            //        drs["spcfdesc"] = dr["spcfdesc"].ToString();
            //        drs["spcfcod"] = dr["spcfcod"].ToString();
            //        drs["sirunit"] = dr["sirunit"].ToString();
            //        drs["shipqty"] = dr["shipqty"].ToString();
            //        drs["flag"] = dr["flag"];
            //        viewlist.Rows.Add(drs);

            //    }
            //}
            //else
            //{
            //    mainlist.Clear();
            //    mainlist = (DataTable)ViewState["tblShipmentHistory"];
            //    if (mainlist.Rows.Count > 0)
            //    {

            //        var newDt = mainlist.AsEnumerable()
            //                      .GroupBy(r => r.Field<string>("challanno"))
            //                      .Select(g =>
            //                      {
            //                          var row = mainlist.NewRow();

            //                          row["challanno"] = g.Key;
            //                          row["syspon"] = "";
            //                          row["comcod"] = "";
            //                          row["challandate"] = "01-JAN-1900";
            //                          row["expecteddeldate"] = "01-JAN-1900";
            //                          row["logno"] = "";
            //                          row["rsircode"] = "";
            //                          row["sirdesc"] = "";
            //                          row["spcfcod"] = "";
            //                          row["spcfdesc"] = "";
            //                          row["shipqty"] = Convert.ToDecimal("0.00");
            //                          row["flag"] = g.Where(r => r["challanno"] == g.Key.ToString()).First()["flag"];

            //                          return row;
            //                      }).CopyToDataTable();

            //        ViewState["tblShipmentHistoryView"] = newDt;
            //        this.Gv_Data_Bind("gvShipmentHistory", (DataTable)ViewState["tblShipmentHistoryView"]);
            //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "showMatDescriptionModal();", true);
            //        return;
            //    }
            //}


            //DataTable dt = viewlist.AsEnumerable()
            //         .OrderBy(r => r.Field<string>("challanno"))
            //         .CopyToDataTable();
            //this.Gv_Data_Bind("gvShipmentHistory", dt);
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "showMatDescriptionModal();", true);
        }

        private void Gv_Data_Bind(string gvname, DataTable dt)
        {
            //switch (gvname)
            //{
            //    case "gvShipmentHistory":
            //        this.gvShipmentHistory.DataSource = dt;
            //        this.gvShipmentHistory.DataBind();

            //        bool flag = false;
            //        if (dt != null && dt.Rows.Count > 0)
            //            flag = dt.AsEnumerable().Any(row => "True" == row.Field<String>("flag"));

            //        if (flag)
            //        {
            //            this.gvShipmentHistory.Columns[2].Visible = true;
            //            this.gvShipmentHistory.Columns[3].Visible = true;
            //            this.gvShipmentHistory.Columns[4].Visible = true;
            //            this.gvShipmentHistory.Columns[5].Visible = true;
            //            this.gvShipmentHistory.Columns[6].Visible = true;
            //            this.gvShipmentHistory.Columns[7].Visible = true;
            //        }
            //        else
            //        {
            //            this.gvShipmentHistory.Columns[2].Visible = false;
            //            this.gvShipmentHistory.Columns[3].Visible = false;
            //            this.gvShipmentHistory.Columns[4].Visible = false;
            //            this.gvShipmentHistory.Columns[5].Visible = false;
            //            this.gvShipmentHistory.Columns[6].Visible = false;
            //            this.gvShipmentHistory.Columns[7].Visible = false;
            //        }
            //        break;
            //}
        }

        protected void btnSaveMatDesc_Click(object sender, EventArgs e)
        {
            //var comcod = this.GetCompCode();
            //string challanNo = this.txtChallanNo.Text.Trim();
            //string challanDate = this.txtChallanDate.Text.Trim();
            //string expectedDelDate = this.txtExpDeliveryDate.Text.Trim();
            //string note = this.txtNote.Text.Trim();

            //var ds = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GET_LOG_NO");
            //string maxlogno = ds.Tables[0].Rows[0]["maxlogno"].ToString();

            //if (expectedDelDate == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Faild to save. Expected date is required.');", true);
            //    return;
            //}

            //foreach (GridViewRow row in GV_MatDesc.Rows)
            //{
            //    string orderno = ((Label)row.Cells[1].FindControl("lbtngvOrderNo")).Text.Trim();
            //    string matcode = ((Label)row.Cells[4].FindControl("lblgvMatCode")).Text.Trim();
            //    string spcfcode = ((Label)row.Cells[6].FindControl("lblgvSpcfCode")).Text.Trim();
            //    string shipQty = ((TextBox)row.Cells[13].FindControl("txtShipQty")).Text.Trim();


            //    var isSuccessful = MktData.UpdateTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SAVE_PO_SHIP_LOG", orderno, matcode, spcfcode, expectedDelDate, challanNo, challanDate, note, shipQty, maxlogno);
            //    //var isSuccessful = MktData.UpdateTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SAVE_ORDER_WISE_MATERIAL_LIST", orderno, matcode, spcfcode, expectedDelDate, challanNo, challanDate, expectedDelDate, note, shipQty);

            //    if (!isSuccessful)
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);
            //        return;
            //    }

            //}

            //this.LoadData();
            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Updated');", true);

            var comcod = this.GetCompCode();

            foreach (GridViewRow row in GV_MatDesc.Rows)
            {
                string orderno = ((Label)row.Cells[1].FindControl("lbtngvOrderNo")).Text.Trim();
                string matcode = ((Label)row.Cells[4].FindControl("lblgvMatCode")).Text.Trim();
                string spcfcode = ((Label)row.Cells[6].FindControl("lblgvSpcfCode")).Text.Trim();
                string expecteddate = ((TextBox)row.Cells[12].FindControl("txtExpDelDat")).Text.Trim();

                if (expecteddate != "")
                {
                    var isSuccessful = MktData.UpdateTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SAVE_ORDER_WISE_MATERIAL_LIST", orderno, matcode, spcfcode, expecteddate);

                    if (!isSuccessful)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);
                        return;
                    }
                }
            }

            this.LoadData();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Updated');", true);

        }

        private void GetCountry()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds11 = MktData.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_REGION_INFORMATION", "", "", "", "", "", "", ""); ;
            ViewState["tblorigin"] = ds11.Tables[0];
            this.DdlCountry.DataTextField = "gdesc";
            this.DdlCountry.DataValueField = "gcod";
            this.DdlCountry.DataSource = ds11.Tables[0];
            this.DdlCountry.DataBind();
            this.DdlCountry.SelectedValue = "00000";

        }

        protected void LbtnSeasonSummary_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            //string basis = this.rbtnList1.SelectedItem.Text;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.TxtAsOnDate.Text).ToString("dd-MMM-yyyy");
            string season = DdlSeason2.SelectedItem.Value == "00000" ? "%" : DdlSeason2.SelectedItem.Value + "%";
            string country = DdlCountry.SelectedItem.Value == "00000" ? "%" : DdlCountry.SelectedItem.Value + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "SEASON_WISE_SUPPLY", todate, season, country, "", "");
            if (ds1.Tables[0] == null)
            {
                this.GvSeasonSum.DataSource = null;
                this.GvSeasonSum.DataBind();
                return;
            }
            else
            {
                this.GvSeasonSum.PageSize = Convert.ToInt32(this.DdlPSize.SelectedValue.ToString());

                this.GvSeasonSum.DataSource = ds1.Tables[0];
                this.GvSeasonSum.DataBind();
                ((Label)this.GvSeasonSum.FooterRow.FindControl("lblgvfOrderQty")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(ordrqty)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(ordrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.GvSeasonSum.FooterRow.FindControl("lblgvfOrderamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(ordamt)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(ordamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.GvSeasonSum.FooterRow.FindControl("lblgvfOrderamtbdt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(ordamtbdt)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(ordamtbdt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.GvSeasonSum.FooterRow.FindControl("lblgvfOrderamtEuro")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(oramteuro)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(oramteuro)", ""))).ToString("#,##0.00;(#,##0.00); ");

                //Session["Report1"] = GvSeasonSum;
                //((HyperLink)this.GvSeasonSum.FooterRow.FindControl("GvSeasonSum_hlbtntbCdataExel")).NavigateUrl =
                //   "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                this.GvCountrySum.DataSource = ds1.Tables[1];
                this.GvCountrySum.DataBind();
                ((Label)this.GvCountrySum.FooterRow.FindControl("lblgvfCOrderQty")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(ordrqty)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(ordrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.GvCountrySum.FooterRow.FindControl("lblgvfCOrderAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(ordamt)", "")) ? 0.00 : ds1.Tables[0].Compute("sum(ordamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                this.gvSeasonSumDetail.DataSource = ds1.Tables[0];
                this.gvSeasonSumDetail.DataBind();

                this.EnableExcelDownload(ds1.Tables[0]);

                //Session["Report1"] = GvCountrySum;
                //((HyperLink)this.GvCountrySum.FooterRow.FindControl("GvCountrySum_hlbtntbCdataExel")).NavigateUrl =
                //   "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }
            ViewState["tblSeasoSum"] = ds1.Tables[0];
            ViewState["tblCountrySum"] = ds1.Tables[1];

            var countrysum = ds1.Tables[1].DataTableToList<CountrySum>();
            var suppliersum = ds1.Tables[0].DataTableToList<SupplierSum>().Take(20);
            var jsonSerialiser = new JavaScriptSerializer();
            var countrysum_json = jsonSerialiser.Serialize(countrysum);
            var suppliersum_json = jsonSerialiser.Serialize(suppliersum);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + countrysum_json + "','" + suppliersum_json + "')", true);

        }

        private void EnableExcelDownload(DataTable dataTable)
        {
            try
            {
                if (dataTable == null || dataTable.Rows.Count == 0) return;


                DataView dv1 = new DataView(dataTable);

                DataTable dtt1 = dv1.ToTable();

                dtt1.Columns.Remove("comcod");
                dtt1.Columns.Remove("ssircode");
                dtt1.Columns.Remove("ordamt1");
                dtt1.Columns.Remove("ordrqty");
                dtt1.Columns.Remove("curcode");
                dtt1.Columns.Remove("curcodedesc");
                dtt1.Columns.Remove("cursymbol");
                dtt1.Columns.Remove("countrycode");
                dtt1.Columns.Remove("oramteuro");
                dtt1.Columns.Remove("ordamtbdt");
                    
                dtt1.Columns["ssirdesc"].ColumnName = "Supplier Name";
                dtt1.Columns["ordamt"].ColumnName = "Business Value";
                dtt1.Columns["country"].ColumnName = "Country Of Origin";
                dtt1.Columns["paymentrms"].ColumnName = "Payment Terms";
                dtt1.Columns["shipterms"].ColumnName = "Shipping Terms";
                dtt1.Columns["supaddress"].ColumnName = "Address";
                dtt1.Columns["avgleadtime"].ColumnName = "Average Lead Time";
                dtt1.Columns["itemslist"].ColumnName = "Items List";

                GridView gv = new GridView();
                gv.AllowPaging = false;
                gv.DataSource = dtt1;
                gv.DataBind();

                ViewState["DetailReprt"] = dtt1;

                Session["Report1"] = gv;
                this.gvssdHlinkExcel.NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception ex)
            {
            }
        }

        protected void GvSeasonSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblSeasoSum"];
            this.GvSeasonSum.PageIndex = e.NewPageIndex;
            this.GvSeasonSum.PageSize = Convert.ToInt32(this.DdlPSize.SelectedValue.ToString());
            this.GvSeasonSum.DataSource = dt;
            this.GvSeasonSum.DataBind();
            this.home.Attributes["class"] = "tab-pane fade active show";
            this.details.Attributes["class"] = "tab-pane fad";
            this.hometab.Attributes["class"] = "nav-link active show";
            this.detailstab.Attributes["class"] = "nav-link";

            var countrysum = ((DataTable)ViewState["tblCountrySum"]).DataTableToList<CountrySum>();
            var suppliersum = ((DataTable)ViewState["tblSeasoSum"]).DataTableToList<SupplierSum>().Take(20);
            var jsonSerialiser = new JavaScriptSerializer();
            var countrysum_json = jsonSerialiser.Serialize(countrysum);
            var suppliersum_json = jsonSerialiser.Serialize(suppliersum);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + countrysum_json + "','" + suppliersum_json + "')", true);
        }

        protected void gvSeasonSumDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblSeasoSum"];
            this.gvSeasonSumDetail.PageIndex = e.NewPageIndex;
            this.gvSeasonSumDetail.PageSize = Convert.ToInt32(this.DdlPSize.SelectedValue.ToString());
            this.gvSeasonSumDetail.DataSource = dt;
            this.gvSeasonSumDetail.DataBind();
            this.home.Attributes["class"] = "tab-pane fade";
            this.details.Attributes["class"] = "tab-pane fade active show";
            this.hometab.Attributes["class"] = "nav-link";
            this.detailstab.Attributes["class"] = "nav-link active show";

            var countrysum = ((DataTable)ViewState["tblCountrySum"]).DataTableToList<CountrySum>();
            var suppliersum = ((DataTable)ViewState["tblSeasoSum"]).DataTableToList<SupplierSum>().Take(20);
            var jsonSerialiser = new JavaScriptSerializer();
            var countrysum_json = jsonSerialiser.Serialize(countrysum);
            var suppliersum_json = jsonSerialiser.Serialize(suppliersum);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + countrysum_json + "','" + suppliersum_json + "')", true);

        }

        protected void lkbtnPOSummary_Click(object sender, EventArgs e)
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlOrderName.SelectedValue.ToString();
            string supCod = this.ddlSupplierName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSupplierName.SelectedValue.ToString() + "%";
            string balance = (this.ChkBalance.Checked) ? "woz" : "";
            string season = DdlSeason.SelectedItem.Value == "00000" ? "%" : DdlSeason.SelectedItem.Value + "%";
            string pricesum = (this.ChckPriceSum.Checked == true) ? "TRUE" : "FALSE";
            string shipperwise = (this.ChckShipper.Checked == true) ? "TRUE" : "FALSE";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GET_SUPPLIER_WISE_PO_SUMMARY", supCod, "", todate, pactcode, balance, season, pricesum, shipperwise);

            if (ds1 == null)
                return;

            ds1.Tables[0].Columns["ssirdesc"].ColumnName = "Supplier Name";
            ds1.Tables[0].Columns["pono"].ColumnName = "Order. No.";
            ds1.Tables[0].Columns["orderdat"].ColumnName = "Date";
            ds1.Tables[0].Columns["refno"].ColumnName = "Ref. No.";
            ds1.Tables[0].Columns["actdesc"].ColumnName = "LC";
            ds1.Tables[0].Columns["ordrqty"].ColumnName = "Order Qty";
            ds1.Tables[0].Columns["ordamt"].ColumnName = "Order Amt.";
            ds1.Tables[0].Columns["curdesc"].ColumnName = "Currency";
            ds1.Tables[0].AcceptChanges();

            GridView gvPOSummay = new GridView();
            gvPOSummay.DataSource = ds1.Tables[0];
            gvPOSummay.DataBind();


            Session["Report1"] = gvPOSummay;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('../RptViewer.aspx?PrintOpt=GRIDTOEXCEL' ,'_blank');", true);
            //"../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }

        protected void ChkShipBal_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkShipBal.Checked)
            {
                DataTable dt = ((DataTable)Session["tblstatus"]).Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = "shipbalqty >0";

                this.gvReqStatus.DataSource = dv.ToTable();
                this.gvReqStatus.DataBind();

            }
            else
            {
                this.gvReqStatus.DataSource = (DataTable)Session["tblstatus"];
                this.gvReqStatus.DataBind();
            }
        }

        protected void ChckPriceSum_CheckedChanged(object sender, EventArgs e)
        {
            if (ChckPriceSum.Checked == true)
            {
                this.DDLType.Visible = true;
            }
            else
            {
                this.DDLType.Visible = false;
            }
        }

        protected void gvReqStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSub = (Label)e.Row.FindControl("lblgvResUnit");
                Label lblOrdQty = (Label)e.Row.FindControl("lblgvApprQty");
                Label lblOrdAmt = (Label)e.Row.FindControl("lblgvOrderAmt");

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (grp == "B")
                {
                    lblSub.Font.Bold = true;
                    lblOrdQty.Font.Bold = true;
                    lblOrdAmt.Font.Bold = true;
                }
                else
                {
                    lblSub.Font.Bold = false;
                    lblOrdQty.Font.Bold = false;
                    lblOrdAmt.Font.Bold = false;
                }
            }  
        }

        protected void gvReqStatus_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataTable dt = (DataTable)Session["tblstatus"];
            if (ChkShipBal.Checked)
            {
               
                DataView dv = dt.DefaultView;
                dv.RowFilter = "shipbalqty >0";
                dt = dv.ToTable();             

            }
            

          
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            gvReqStatus.DataSource = sortedView;
            gvReqStatus.DataBind();

            Session["tblstatus"] = sortedView.ToTable();
        }

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
    }


    public class CountrySum
    {
        public string countrycode { get; set; }
        public string country { get; set; }
        public double ordrqty { get; set; }
        public double ordamt { get; set; }
    }

    public class SupplierSum
    {
        public string ssircode { get; set; }
        public string ssirdesc { get; set; }
        public double ordrqty { get; set; }
        public double ordamt { get; set; }
    }

}