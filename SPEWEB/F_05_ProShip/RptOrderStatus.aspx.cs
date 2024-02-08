using Microsoft.Reporting.WinForms;
using SPEENTITY;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_05_ProShip
{
    public partial class RptOrderStatus : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        UserManagerSampling objUserMan = new UserManagerSampling();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();

                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "MatMaster") ? "Material Master Report" : "Order Status Report";

                this.GetSeason();
                this.GetBuyers();
                this.GetAgents();
                this.GetCompanyName();

                if (type == "MatMaster")
                {
                    GetMatGroup();
                    this.Pipeline.Visible = true;
                    this.DdlMatGroup.Visible = true;
                    this.lblmatgrp.Visible = true;
                    this.plnDateF.Visible = false;
                    this.BomType.Visible = true;

                }
                else
                {
                    this.lblBom.Visible = false;
                    this.ddlBomList.Visible = false;
                }

                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDatefrom.Text = "01" + this.txtDatefrom.Text.Trim().Substring(2);
                this.txtdateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }

        }

        private void GetCompanyName()
        {
            UserLogin ulog = new UserLogin();
            DataSet ds1 = ulog.GetNameAdd();
            this.ddlCompanyName.DataTextField = "comsnam";
            this.ddlCompanyName.DataValueField = "comcod";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            this.ddlCompanyName.Items.Add(new ListItem { Text = "All", Value = "0000" });
            this.ddlCompanyName.SelectedValue = this.GetComCode();

            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "OrdStatus":
                    FieldCompNameList.Visible = true;
                    break;
                case "MatMaster":
                    FieldCompNameList.Visible = false;
                    break;
                default:
                    FieldCompNameList.Visible = false;
                    break;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "Req":
                    //this.ReqVSRecPrint();
                    break;
                case "MatMaster":
                    this.MaterialMasterReportPrint();
                    break;
                case "OrdStatus":
                    this.PrintOrderStatReport();
                    break;
            }
        }

        private void MaterialMasterReportPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Fdate = this.txtDatefrom.Text;
            string Tdate = this.txtdateto.Text;
            string date = Convert.ToString((Tdate));
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            DataTable dt = (DataTable)ViewState["tblMatMasterDetails"];
            var list = dt.DataTableToList<SPEENTITY.C_05_ProShip.EClassPlanning.RptMaterialMaster>();

            DataTable dt2 = (DataTable)ViewState["tblBomList"];
            var list2 = dt2.DataTableToList<SPEENTITY.C_05_ProShip.EClassPlanning.BOM>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_05_ProShip.RptMatMaster", list, list2, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Material Master Report"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintOrderStatReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string rptTitle = "Order Status Report";
            string fromtodate = "Date Range:  " + this.txtDatefrom.Text + "  to  " + this.txtdateto.Text;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            string season = this.DdlSeason.SelectedItem.Text;

            DataSet ds = (DataSet)ViewState["dsOrderStatus"];
            var list1 = ds.Tables[0].DataTableToList<SPEENTITY.C_05_ProShip.EClassPlanning.OrderStatusRpt>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("R_05_ProShip.RptOrderStatus", list1, null, null);

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

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetMatGroup()
        {
            string comcod = this.GetComCode();
            DataSet tblmatgrp = feaData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_MATERIAL_GROUP_NAME", "", "", "", "", "", "", "");
            ViewState["tblmatgrp"] = tblmatgrp.Tables[0];
            DdlMatGroup.DataTextField = "gdesc";
            DdlMatGroup.DataValueField = "gcod";
            DdlMatGroup.DataSource = tblmatgrp.Tables[0];
            DdlMatGroup.DataBind();
        }

        private void GetSeason()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
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


        private void GetAgents()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetComCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;

            var lstagent = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "32");
            lstagent.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            ddlAgent.DataTextField = "gdesc";
            ddlAgent.DataValueField = "gcod";
            ddlAgent.DataSource = lstagent;
            ddlAgent.DataBind();
            ddlAgent.SelectedValue = "00000";
        }


        private void GetBuyers()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            ddlBuyer.DataTextField = "sirdesc";
            ddlBuyer.DataValueField = "sircode";
            ddlBuyer.DataSource = ds2.Tables[0];
            ddlBuyer.DataBind();
            ddlBuyer.SelectedValue = "000000000000";
        }


        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBOMList();
        }


        protected void GetBOMList()
        {
            string comcod = this.GetComCode();
            string season = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string agent = (this.ddlAgent.SelectedValue.ToString() == "00000") ? "%" : this.ddlAgent.SelectedValue.ToString() + "%";
            string buyer = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";

            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GETBOMLIST", season, "", "", "", "", "", "", "", "");

            ViewState["tblBomList"] = ds1.Tables[0];

            if (ds1 == null)
                return;
            DataView dv = ds1.Tables[0].Copy().DefaultView;
            dv.RowFilter = "pipeline = false";

            DataView dv1 = ds1.Tables[0].Copy().DefaultView;
            dv1.RowFilter = "pipeline = true and planarchive=false";

            DataView dv2 = ds1.Tables[0].Copy().DefaultView;
            dv2.RowFilter = "pipeline = true and planarchive=true";

            this.gvBomlistPan.DataSource = dv.ToTable();
            this.gvBomlistPan.DataBind();
            this.PendingCounter.InnerHtml = dv.ToTable().Rows.Count.ToString();

            this.gvBomlist.DataSource = dv1.ToTable();
            this.gvBomlist.DataBind();
            this.SelectedCounter.InnerHtml = dv1.ToTable().Rows.Count.ToString();


            this.gvArchived.DataSource = dv2.ToTable();
            this.gvArchived.DataBind();
            this.ArchiveCounter.InnerHtml = dv2.ToTable().Rows.Count.ToString();

            DataTable dt = dv1.ToTable();
            dt.Rows.Add(comcod, "0000000000", "All", "000000000000");



            ddlBomList.DataTextField = "bomdesc";
            ddlBomList.DataValueField = "bomid";
            ddlBomList.DataSource = dt;
            ddlBomList.DataBind();
            //this.ddlBomList.SelectedValue = "0000000000";


            ds1.Dispose();
            // this.ddlOrder_SelectedIndexChanged(null, null);
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {

                case "OrdStatus":
                    this.Multiview.ActiveViewIndex = 0;
                    this.GetOrderStatus();
                    this.LoadOrderStatusGridView();
                    break;

                case "MatMaster":
                    this.Multiview.ActiveViewIndex = 1;
                    this.OrderMaterialMaster();
                    break;

                case "SMVsheet":
                    this.Multiview.ActiveViewIndex = 2;
                    this.GetSMVSheet();
                    break;

            }
        }

        private void GetSMVSheet()
        {
            string comcod = this.ddlCompanyName.SelectedValue == "0000" ? "%%" : this.ddlCompanyName.SelectedValue;
            string tdate = this.txtdateto.Text.ToString();
            string fdate = this.txtDatefrom.Text.ToString();
            string season = this.DdlSeason.SelectedValue.ToString() == "00000" ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string agent = (this.ddlAgent.SelectedValue.ToString() == "00000") ? "%" : this.ddlAgent.SelectedValue.ToString() + "%";
            string buyer = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";
            string orderno = this.txtSearch.Text.ToString().Length > 0 ? this.txtSearch.Text.ToString() + "%" : "%";


            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "RPTODRSTATUS", fdate, tdate, season, agent, buyer, orderno, "", "");

            if (this.txtSearch.Text.ToString().Length > 0)
            {
                if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                {
                    this.gvSMVsheet.DataSource = null;
                    this.gvSMVsheet.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data not found');", true);
                    return;
                }
            }
            if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
            {
                this.gvSMVsheet.DataSource = null;
                this.gvSMVsheet.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data not found');", true);
                return;
            }

            ViewState["dsOrderStatus"] = ds2;

            EnableExcelDownload(ds2.Tables[0]);

            this.gvSMVsheet.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            this.gvSMVsheet.DataSource = ds2.Tables[0];
            this.gvSMVsheet.DataBind();
        }

        private void GetOrderStatus()
        {
            string comcod = this.ddlCompanyName.SelectedValue == "0000" ? "%%" : this.ddlCompanyName.SelectedValue;
            string tdate = this.txtdateto.Text.ToString();
            string fdate = this.txtDatefrom.Text.ToString();
            string season = this.DdlSeason.SelectedValue.ToString() == "00000" ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string agent = (this.ddlAgent.SelectedValue.ToString() == "00000") ? "%" : this.ddlAgent.SelectedValue.ToString() + "%";
            string buyer = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";
            string orderno = this.txtSearch.Text.ToString().Length > 0 ? "%" + this.txtSearch.Text.ToString() + "%" : "%";


            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "RPTODRSTATUS", fdate, tdate, season, agent, buyer, orderno, "", "");

            if (this.txtSearch.Text.ToString().Length > 0)
            {
                if (ds2 == null)
                    return;

                else if (ds2.Tables[0].Rows.Count == 0)
                {
                    this.gvOrderstatus.DataSource = null;
                    this.gvOrderstatus.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data not found');", true);
                    return;
                }
                else
                {
                    ViewState["dsOrderStatus"] = ds2;
                    EnableExcelDownload(ds2.Tables[0]);
                    return;
                }
            }
            if (ds2 == null)
            {
                this.gvOrderstatus.DataSource = null;
                this.gvOrderstatus.DataBind();
                return;
            }
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvOrderstatus.DataSource = null;
                this.gvOrderstatus.DataBind();
                return;
            }

            ViewState["dsOrderStatus"] = ds2;

            EnableExcelDownload(ds2.Tables[0]);
        }

        private void LoadOrderStatusGridView()
        {

            DataSet ds = (DataSet)ViewState["dsOrderStatus"];

            if (ds == null)
            {
                this.gvOrderstatus.DataSource = null;
                this.gvOrderstatus.DataBind();
                
                return;

            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                this.gvOrderstatus.DataSource = null;
                this.gvOrderstatus.DataBind();
             
                return;

            }

            this.gvOrderstatus.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            this.gvOrderstatus.DataSource = ds.Tables[0];
            this.gvOrderstatus.DataBind();
            this.CalculateTotalFooter();

        }

        private void CalculateTotalFooter()
        {
            DataTable dt = ((DataSet)ViewState["dsOrderStatus"]).Tables[0];

            if (dt.Rows.Count > 0)
            {
                double OrdrQty = Convert.ToDouble(dt.Compute("SUM(ordrqty)", string.Empty));
                double CutQty = Convert.ToDouble(dt.Compute("SUM(cutdone)", string.Empty));
                double SngQty = Convert.ToDouble(dt.Compute("SUM(sewdone)", string.Empty));
                double FitQty = Convert.ToDouble(dt.Compute("SUM(fitdone)", string.Empty));
                double LastQty = Convert.ToDouble(dt.Compute("SUM(lasdone)", string.Empty));
                double ShipQty = Convert.ToDouble(dt.Compute("SUM(shipedqty)", string.Empty));
                double CutBal = Convert.ToDouble(dt.Compute("SUM(ordrqty)", string.Empty)) - Convert.ToDouble(dt.Compute("SUM(cutdone)", string.Empty));
                double SngBal = Convert.ToDouble(dt.Compute("SUM(ordrqty)", string.Empty)) - Convert.ToDouble(dt.Compute("SUM(sewdone)", string.Empty));
                double FitBal = Convert.ToDouble(dt.Compute("SUM(ordrqty)", string.Empty)) - Convert.ToDouble(dt.Compute("SUM(fitdone)", string.Empty));
                double LastBal = Convert.ToDouble(dt.Compute("SUM(ordrqty)", string.Empty)) - Convert.ToDouble(dt.Compute("SUM(lasdone)", string.Empty));

                ((Label)(this.gvOrderstatus.FooterRow.FindControl("gvLblTtlOrdrQty"))).Text = OrdrQty.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvOrderstatus.FooterRow.FindControl("gvLblTtlCutQty"))).Text = CutQty.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvOrderstatus.FooterRow.FindControl("gvLblTtlSngQty"))).Text = SngQty.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvOrderstatus.FooterRow.FindControl("gvLblTtlFitQty"))).Text = FitQty.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvOrderstatus.FooterRow.FindControl("gvLblTtlLastQty"))).Text = LastQty.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvOrderstatus.FooterRow.FindControl("gvLblTtlShipQty"))).Text = ShipQty.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvOrderstatus.FooterRow.FindControl("gvLblTtlCutBal"))).Text = CutBal.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvOrderstatus.FooterRow.FindControl("gvLblTtlSngBal"))).Text = SngBal.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvOrderstatus.FooterRow.FindControl("gvLblTtlFitBal"))).Text = FitBal.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvOrderstatus.FooterRow.FindControl("gvLblTtlLastBal"))).Text = LastBal.ToString("#,##0.00;(#,##0.00); ");
            }
        }

        private void EnableExcelDownload(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    this.lnkbtnExcl.Visible = true;
                }
                else
                {
                    return;
                }

                GridView gvTemp = new GridView();
                gvTemp.AllowPaging = false;
                gvTemp.DataSource = dt;
                gvTemp.DataBind();
                Session["Report1"] = gvTemp;
                lnkbtnExcl.NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception ex)
            {
            }

        }

        private void OrderMaterialMaster()
        {
            string comcod = this.GetComCode();

            string tdate = this.txtdateto.Text.ToString();
            string season = this.DdlSeason.SelectedValue.ToString() + "%";
            string bomList = this.ddlBomList.SelectedValue.ToString() == "0000000000" ? "%" : this.ddlBomList.SelectedValue.ToString() + "%";
            string matgrp = (this.DdlMatGroup.SelectedValue.ToString() == "00000") ? "%" : this.DdlMatGroup.SelectedValue.ToString() + "%";
            string archbomtype = DdlBomType.SelectedValue.ToString();

            DataTable dtBom = new DataTable();

            dtBom.Columns.Add("bomid");

            foreach (ListItem item in ddlBomList.Items)
            {
                if (item.Selected)
                {
                    dtBom.Rows.Add(item.Value);
                }
            }

            DataSet dsBom = new DataSet("ds");
            dsBom.Tables.Add(dtBom);
            dsBom.Tables[0].TableName = "tbl1";

            string dsxml = dsBom.GetXml();
            DataSet ds2 = feaData.GetTransInfoNew(comcod, "SP_REPORT_ORDER_STATUS", "RPT_MATERIAL_MASTER", dsBom, null, null, tdate, season, (bomList != "%" ? "%" : matgrp), archbomtype, bomList, "", "");

            if (ds2 == null)
            {
                this.gvMatMasterDetails.DataSource = null;
                this.gvMatMasterDetails.DataBind();

                this.gvMatMasterSummary.DataSource = null;
                this.gvMatMasterSummary.DataBind();
                return;
            }

            ViewState["tblMatMasterDetails"] = ds2.Tables[0];
            ViewState["tblMatMasterSummary"] = ds2.Tables[1];
            this.LoadMaterialMasterGridView();

        }

        private void LoadMaterialMasterGridView()
        {
            DataTable dt1 = (DataTable)ViewState["tblMatMasterDetails"];
            DataTable dt2 = (DataTable)ViewState["tblMatMasterSummary"];

            if (dt1 == null || dt2 == null) return;
            if (dt1.Rows.Count == 0 || dt2.Rows.Count == 0) return;

            this.gvMatMasterDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.gvMatMasterDetails.DataSource = dt1;
            this.gvMatMasterDetails.DataBind();

            this.gvMatMasterSummary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.gvMatMasterSummary.DataSource = dt2;
            this.gvMatMasterSummary.DataBind();

            double ttlBomQtyD = Convert.ToDouble(dt1.Compute("Sum(itmqty)", String.Empty));
            double ttlIsuBalD = Convert.ToDouble(dt1.Compute("Sum(bomisubal)", String.Empty));
            ((Label)this.gvMatMasterDetails.FooterRow.FindControl("lblgvFtrBOmqtyD")).Text = ttlBomQtyD.ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMatMasterDetails.FooterRow.FindControl("lblgvFtrIsuBalD")).Text = ttlIsuBalD.ToString("#,#0.00;(#,##0.00); ");

            double ttlBomQtyS = Convert.ToDouble(dt2.Compute("Sum(itmqty)", String.Empty));
            double ttlIsuBalS = Convert.ToDouble(dt2.Compute("Sum(bomisubal)", String.Empty));
            ((Label)this.gvMatMasterSummary.FooterRow.FindControl("lblgvFtrBOmqtyS")).Text = ttlBomQtyS.ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMatMasterSummary.FooterRow.FindControl("lblgvFtrIsuBalS")).Text = ttlIsuBalS.ToString("#,#0.00;(#,##0.00); ");
        }

        protected void lblgvOrdQty_Click1(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            ViewState.Remove("tblOrderQty");
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string mlccod = ((Label)this.gvOrderstatus.Rows[index].FindControl("lblmlccod")).Text.ToString();
            string style = ((Label)this.gvOrderstatus.Rows[index].FindControl("lblstyle")).Text.ToString();
            string dayid = ((Label)this.gvOrderstatus.Rows[index].FindControl("lbldayid")).Text.ToString();
            string type = (dayid != "00000000") ? "Reorder" : "";
            string date = (dayid == "00000000") ? "01-Jan-1900" : Convert.ToDateTime(dayid.Substring(4, 2) + "/" + dayid.Substring(6, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ORDER_DETAILS", mlccod, type, date, style, "", "", "", ""); ;

            for (int i = 5; i < 45; i++)
                this.gv1.Columns[i].Visible = false;

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gv1.Columns[columid + 4].Visible = true;
                this.gv1.Columns[columid + 4].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            ViewState["tblOrderQty"] = lst;
            this.gv1.DataSource = lst;
            this.gv1.DataBind();

            this.gv1pack.DataSource = null;
            this.gv1pack.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);
        }

        protected void lgvLeatherpercnt_Click(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string bomid = ((HyperLink)this.gvOrderstatus.Rows[index].FindControl("gvmshlnkBomid")).Text.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GET_BOM_WISE_REC_LIST", bomid, "46001%", "", "", "");
            this.gvRecDetails.DataSource = HiddenSameData(ds1.Tables[0]);
            this.gvRecDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenRecModal();", true);
        }

        protected void lgvSynthetic_Click(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string bomid = ((HyperLink)this.gvOrderstatus.Rows[index].FindControl("gvmshlnkBomid")).Text.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GET_BOM_WISE_REC_LIST", bomid, "46005%", "", "", "");
            this.gvRecDetails.DataSource = HiddenSameData(ds1.Tables[0]);
            this.gvRecDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenRecModal();", true);

        }

        protected void lgvORNAMENT_Click(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string bomid = ((HyperLink)this.gvOrderstatus.Rows[index].FindControl("gvmshlnkBomid")).Text.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GET_BOM_WISE_REC_LIST", bomid, "46010%", "", "", "");
            this.gvRecDetails.DataSource = HiddenSameData(ds1.Tables[0]);
            this.gvRecDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenRecModal();", true);

        }

        protected void lgvTHREAD_Click(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string bomid = ((HyperLink)this.gvOrderstatus.Rows[index].FindControl("gvmshlnkBomid")).Text.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GET_BOM_WISE_REC_LIST", bomid, "46015%", "", "", "");
            this.gvRecDetails.DataSource = HiddenSameData(ds1.Tables[0]);
            this.gvRecDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenRecModal();", true);
        }

        protected void lgvOUTSOLE_Click(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string bomid = ((HyperLink)this.gvOrderstatus.Rows[index].FindControl("gvmshlnkBomid")).Text.ToString();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GET_BOM_WISE_REC_LIST", bomid, "46020%", "", "", "");
            this.gvRecDetails.DataSource = HiddenSameData(ds1.Tables[0]);
            this.gvRecDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenRecModal();", true);
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string bomid = dt1.Rows[0]["bomid"].ToString();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            string spcfcod = dt1.Rows[0]["spcfcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["bomid"].ToString() == bomid && dt1.Rows[j]["rsircode"].ToString() == rsircode && dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                {
                    bomid = dt1.Rows[j]["bomid"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    dt1.Rows[j]["budget"] = "0.00";
                }
                else
                {
                    bomid = dt1.Rows[j]["bomid"].ToString();
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                }


            }
            return dt1;

        }

        protected void LbtnUpdatePipeline_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComCode();
            for (int i = 0; i < gvBomlistPan.Rows.Count; i++)
            {
                string pipeline = (((CheckBox)gvBomlistPan.Rows[i].FindControl("gvPenChkCol")).Checked == true) ? "true" : "false";

                string bomid = ((Label)gvBomlistPan.Rows[i].FindControl("LblgvPenBOmid")).Text.ToString();
                bool result = feaData.UpdateTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "UPDATE_BOM_TO_PIPELINE", bomid, pipeline, "false");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + feaData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
                else
                {
                    if (result == true && ConstantInfo.LogStatus == true && ((CheckBox)gvBomlistPan.Rows[i].FindControl("gvPenChkCol")).Checked == true)
                    {

                        string eventtype = "BOM Pipe Line Push to Selected";
                        string eventdesc = "Planning BOM Pipe Line Selected" + bomid;
                        string eventdesc2 = "BOM ID- " + bomid;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Pipeline Updated');", true);
                }
            }

            for (int i = 0; i < gvBomlist.Rows.Count; i++)
            {
                string archive = (((CheckBox)gvBomlist.Rows[i].FindControl("gvArcChkCol")).Checked == true) ? "true" : "false";

                string bomid = ((Label)gvBomlist.Rows[i].FindControl("LblBOmid")).Text.ToString();
                bool result = feaData.UpdateTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "UPDATE_BOM_TO_PIPELINE", bomid, "true", archive);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + feaData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
                else
                {
                    if (result == true && ConstantInfo.LogStatus == true && ((CheckBox)gvBomlist.Rows[i].FindControl("gvArcChkCol")).Checked == true)
                    {

                        string eventtype = "BOM Pipe line  Achived";
                        string eventdesc = "Planning BOM Pipe Line archived" + bomid;
                        string eventdesc2 = "BOM ID- " + bomid;
                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Pipeline Updated');", true);

                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "closeModal( );", true);
            
            this.GetBOMList();
        }

        protected void ddlBomList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DdlMatGroup.SelectedValue = "00000";
        }

        protected void gvMatMasterDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblMatMasterDetails"];
            if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "stockqty")) < 0)
            {
                Label lblActualStockQty = (Label)e.Row.FindControl("lblgvActualStockQty");

                lblActualStockQty.ForeColor = System.Drawing.Color.Red;
            }

        }


        protected void DdlBomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlBomList.SelectedValue = "0000000000";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "MatMaster":
                    this.LoadMaterialMasterGridView();
                    break;

                case "OrdStatus":
                    this.LoadOrderStatusGridView();
                    break;

                default:
                    break;
            }

        }

        protected void gvOrderstatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrderstatus.PageIndex = e.NewPageIndex;
            this.LoadOrderStatusGridView();
        }

        protected void gvMatMasterDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatMasterDetails.PageIndex = e.NewPageIndex;
            this.LoadMaterialMasterGridView();
        }

        protected void LbtnDetailsExfactdate_Click(object sender, EventArgs e)
        {
            this.hyprModalPrint.Visible = true;
            string comcod = GetComCode();
            ViewState.Remove("tblPackqty");
            HyperLink printbtn = (HyperLink)this.hyprModalPrint;

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string mlccod = ((Label)this.gvOrderstatus.Rows[index].FindControl("lblmlccod")).Text.ToString();
            string dayid = ((Label)this.gvOrderstatus.Rows[index].FindControl("lbldayid")).Text.ToString();
            string styleid = ((Label)this.gvOrderstatus.Rows[index].FindControl("lblstyle")).Text.ToString();
            string colorid = ((Label)this.gvOrderstatus.Rows[index].FindControl("lblcolorid")).Text.ToString();

            printbtn.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod=" + mlccod + "&Ptype=import&dayid=" + dayid + "&sircode=" + styleid + colorid + "&format=PDF&Dept=Planning&info=packinginfo";

            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "GET_ORDER_PACKING_DETAILS", mlccod, dayid, styleid, "", "", "", ""); ;

            if (ds1 == null)
                return;
            ViewState["tblsizedesc"] = ds1.Tables[1];
            string mStyleID = "xxxxxxx";
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                if (ds1.Tables[0].Rows[i]["styleid"].ToString() == mStyleID)
                    ds1.Tables[0].Rows[i]["StyleDesc"] = " >> ";
                mStyleID = ds1.Tables[0].Rows[i]["styleid"].ToString();
            }

            ViewState["tblPackqty"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>().OrderBy(p => p.styleid).OrderBy(p => p.colorid).ToList();


            for (int i = 10; i < 47; i++)
                this.gv1pack.Columns[i].Visible = false;

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gv1pack.Columns[columid + 9].Visible = true;
                this.gv1pack.Columns[columid + 9].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }
            this.gv1.DataSource = null;
            this.gv1.DataBind();
            this.gv1pack.EditIndex = -1;
            this.Bind_Pack_Allocation();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);

        }
        private void Bind_Pack_Allocation()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst = list;
            this.gv1pack.DataSource = lst;
            this.gv1pack.DataBind();
            this.FooterCalPackList();
            //this.OrderINput_Selection();
        }
        private void FooterCalPackList()
        {
            var list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPackqty"];
            if (list == null || list.Count == 0)
            {
                return;
            }
         ((Label)this.gv1pack.FooterRow.FindControl("flblgvF1")).Text = ((list.Sum(p => p.p1) == 0) ? 0 : list.Sum(p => p.p1)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF2")).Text = ((list.Sum(p => p.p2) == 0) ? 0 : list.Sum(p => p.p2)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF3")).Text = ((list.Sum(p => p.p3) == 0) ? 0 : list.Sum(p => p.p3)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF4")).Text = ((list.Sum(p => p.p4) == 0) ? 0 : list.Sum(p => p.p4)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF5")).Text = ((list.Sum(p => p.p5) == 0) ? 0 : list.Sum(p => p.p5)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF6")).Text = ((list.Sum(p => p.p6) == 0) ? 0 : list.Sum(p => p.p6)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF7")).Text = ((list.Sum(p => p.p7) == 0) ? 0 : list.Sum(p => p.p7)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF8")).Text = ((list.Sum(p => p.p8) == 0) ? 0 : list.Sum(p => p.p8)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF9")).Text = ((list.Sum(p => p.p9) == 0) ? 0 : list.Sum(p => p.p9)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF10")).Text = ((list.Sum(p => p.p10) == 0) ? 0 : list.Sum(p => p.p10)).ToString("#,##0;(#,##0); ");

            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF11")).Text = ((list.Sum(p => p.p11) == 0) ? 0 : list.Sum(p => p.p11)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF12")).Text = ((list.Sum(p => p.p12) == 0) ? 0 : list.Sum(p => p.p12)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF13")).Text = ((list.Sum(p => p.p13) == 0) ? 0 : list.Sum(p => p.p13)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF14")).Text = ((list.Sum(p => p.p14) == 0) ? 0 : list.Sum(p => p.p14)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF15")).Text = ((list.Sum(p => p.p15) == 0) ? 0 : list.Sum(p => p.p15)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF16")).Text = ((list.Sum(p => p.p16) == 0) ? 0 : list.Sum(p => p.p16)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF17")).Text = ((list.Sum(p => p.p17) == 0) ? 0 : list.Sum(p => p.p17)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF18")).Text = ((list.Sum(p => p.p18) == 0) ? 0 : list.Sum(p => p.p18)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF19")).Text = ((list.Sum(p => p.p19) == 0) ? 0 : list.Sum(p => p.p19)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF20")).Text = ((list.Sum(p => p.p20) == 0) ? 0 : list.Sum(p => p.p20)).ToString("#,##0;(#,##0); ");

            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF21")).Text = ((list.Sum(p => p.p21) == 0) ? 0 : list.Sum(p => p.p21)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF22")).Text = ((list.Sum(p => p.p22) == 0) ? 0 : list.Sum(p => p.p22)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF23")).Text = ((list.Sum(p => p.p23) == 0) ? 0 : list.Sum(p => p.p23)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF24")).Text = ((list.Sum(p => p.p24) == 0) ? 0 : list.Sum(p => p.p24)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF25")).Text = ((list.Sum(p => p.p25) == 0) ? 0 : list.Sum(p => p.p25)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF26")).Text = ((list.Sum(p => p.p26) == 0) ? 0 : list.Sum(p => p.p26)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF27")).Text = ((list.Sum(p => p.p27) == 0) ? 0 : list.Sum(p => p.p27)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF28")).Text = ((list.Sum(p => p.p28) == 0) ? 0 : list.Sum(p => p.p28)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF29")).Text = ((list.Sum(p => p.p29) == 0) ? 0 : list.Sum(p => p.p29)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF30")).Text = ((list.Sum(p => p.p30) == 0) ? 0 : list.Sum(p => p.p30)).ToString("#,##0;(#,##0); ");

            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF31")).Text = ((list.Sum(p => p.p31) == 0) ? 0 : list.Sum(p => p.p31)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF32")).Text = ((list.Sum(p => p.p32) == 0) ? 0 : list.Sum(p => p.p32)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF33")).Text = ((list.Sum(p => p.p33) == 0) ? 0 : list.Sum(p => p.p33)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF34")).Text = ((list.Sum(p => p.p34) == 0) ? 0 : list.Sum(p => p.p34)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF35")).Text = ((list.Sum(p => p.p35) == 0) ? 0 : list.Sum(p => p.p35)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF36")).Text = ((list.Sum(p => p.p36) == 0) ? 0 : list.Sum(p => p.p36)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF37")).Text = ((list.Sum(p => p.p37) == 0) ? 0 : list.Sum(p => p.p37)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF38")).Text = ((list.Sum(p => p.p38) == 0) ? 0 : list.Sum(p => p.p38)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF39")).Text = ((list.Sum(p => p.p39) == 0) ? 0 : list.Sum(p => p.p39)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1pack.FooterRow.FindControl("flblgvF40")).Text = ((list.Sum(p => p.p40) == 0) ? 0 : list.Sum(p => p.p30)).ToString("#,##0;(#,##0); ");

            ((Label)this.gv1pack.FooterRow.FindControl("PFLblgvTotal")).Text = ((list.Sum(p => p.totalqty) == 0) ? 0 : list.Sum(p => p.totalqty)).ToString("#,##0;(#,##0); ") + " CTN";
            ((Label)this.gv1pack.FooterRow.FindControl("PFLblgvTotalPair")).Text = ((list.Sum(p => p.psum) == 0) ? 0 : list.Sum(p => p.psum)).ToString("#,##0;(#,##0); ") + " PRS";


        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            this.GetOrderStatus();
            this.txtSearch.Text = "";
            this.LoadOrderStatusGridView();
        }


        //protected void ddlAgent_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.GetBOMList();
        //}

        //protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.GetBOMList();
        //}



        //protected void gvMatStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.gvMatStatus.PageIndex = e.NewPageIndex;
        //    LoadOrderStatusGridView();
        //}

        //protected void gvProdRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.gvProdRpt.PageIndex = e.NewPageIndex;
        //    LoadOrderStatusGridView();
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

        protected void gvMatMasterDetails_Sorting(object sender, GridViewSortEventArgs e)
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

            DataTable dt = (DataTable)ViewState["tblMatMasterDetails"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            gvMatMasterDetails.DataSource = sortedView;
            gvMatMasterDetails.DataBind();
        }

        protected void gvMatMasterSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMatMasterSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvMatMasterSummary_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}