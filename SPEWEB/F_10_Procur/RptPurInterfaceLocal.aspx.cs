using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using SPEENTITY;

namespace SPEWEB.F_10_Procur
{
    public partial class RptPurInterfaceLocal : System.Web.UI.Page
    {
        // public static string recvno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus = "";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        UserManagerSampling objUserMan = new UserManagerSampling();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PURCHASE Smartface";//


                // string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //string today = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfromdate.Attributes.Add("data-default-date", today);
                //  this.txttodate.Text = Convert.ToDateTime(this.t//xtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                //RadioButtonList1.Items.Remove("Daily Transaction");
                //this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();
                HyperLink HyPBom = (HyperLink)this.HyPBom as HyperLink;
                HyPBom.NavigateUrl = "~/F_01_Mer/RptOrdAppSheet?Type=PendBom";

                this.ShowBOMCount();
                this.GetSeason();
                this.GetSupplierName();

                lbtnOk_Click(null, null);
                //this.txtIme_TextChanged(null, null);
                //this.RadioButtonList1_SelectedIndexChanged(null, null);
            }
        }
        private void GetSeason()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");
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

        private void GetSupplierName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_MARKETSERVEY", "GETPSNAME", txtSProject, "", "", "", "", "", "", "", "");

            DataRow dr = ds1.Tables[0].NewRow();
            dr["sirdesc"] = "All";
            dr["sircode"] = "000000000000";
            ds1.Tables[0].Rows.Add(dr);

            this.DdlSupplier.DataTextField = "sirdesc";
            this.DdlSupplier.DataValueField = "sircode";
            this.DdlSupplier.DataSource = ds1.Tables[0];
            this.DdlSupplier.SelectedValue = "000000000000";
            this.DdlSupplier.DataBind();

        }
        private void ShowBOMCount()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "BOM_COUNTER", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.HyPBom.Text = "0";

            }
            else
            {
                this.HyPBom.Text = ds1.Tables[0].Rows[0]["bomcount"].ToString();

            }

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblspledger"];
            //if (dt == null)
            lbtnOk_Click(null, null);


        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.PurchaseInfoRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }

        protected void btnSetup_Click(object sender, EventArgs e)
        {
            this.PnlSalesSetup.Visible = true;
            this.pnlInterf.Visible = false;
            this.pnlPurchase.Visible = false;

            //this.lblVal.Visible = false;

        }
        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            this.pnlInterf.Visible = true;
            this.pnlPurchase.Visible = false;
            this.PnlSalesSetup.Visible = false;
            this.RadioButtonList1.SelectedIndex = 0;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            this.pnlInterf.Visible = false;
            this.pnlPurchase.Visible = true;
            this.PnlSalesSetup.Visible = false;
        }
        private void PurchaseInfoRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string temp = this.txtfrmdate.Text;
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string season = DdlSeason.SelectedValue == "00000" ? "%" : DdlSeason.SelectedValue;
            string supplier = DdlSupplier.SelectedValue == "000000000000" ? "%" : DdlSupplier.SelectedValue + "%";


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_LOCAL_INTERFACE", "LOCALPURCHASEDASHBOARD", frmdate, todate, season, supplier, "", "", "", "", "");
            ViewState["dsLocalPurchase"] = ds1;

            //if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
            //    ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
            //    return;
            //}


            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[7].Rows[0]["ttlreq"].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> Request</div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + ds1.Tables[7].Rows[0]["reqchk"].ToString() + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'> Req Checked</div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading mix counter'>" + ds1.Tables[7].Rows[0]["reqauth"].ToString() + "</i></div></a><div class='circle-tile-content mix'><div class='circle-tile-description text-faded'> Req Review</div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[7].Rows[0]["reqapp"].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'> Req Approval</div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[7].Rows[0]["reqcs"].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> CS Create</div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[7].Rows[0]["reqcschk"].ToString() + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'> CS Check</div></div></div>";
            
            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[7].Rows[0]["reqcsapp"].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'> CS Approval</div></div></div>";
            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-yal counter'>" + ds1.Tables[7].Rows[0]["reqpurord"].ToString() + "</i></div></a><div class='circle-tile-content dark-yal'><div class='circle-tile-description text-faded'>Purchase Order</div></div></div>";
            this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-yal counter'>" + ds1.Tables[7].Rows[0]["poapp"].ToString() + "</i></div></a><div class='circle-tile-content dark-yal'><div class='circle-tile-description text-faded'>PO Approval</div></div></div>";
            this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + ds1.Tables[7].Rows[0]["reqmr"].ToString() + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'>Incoming Area</div></div></div>";
            this.RadioButtonList1.Items[10].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow counter'>" + ds1.Tables[7].Rows[0]["mrqc"].ToString() + "</i></div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'>QC Check</div></div></div>";
            this.RadioButtonList1.Items[11].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[7].Rows[0]["storcv"].ToString() + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'>Store Receive</div></div></div>";
            this.RadioButtonList1.Items[12].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + ds1.Tables[7].Rows[0]["readybill"].ToString() + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'>Costing</div></div></div>";

            ////
            //this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["ttlreq"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class='lbldata2'>" + "Req Status" + "</span>";
            //this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["reqchk"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "Req Checked" + "</span>";
            //this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["reqauth"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "Req Review" + "</span>";
            //this.RadioButtonList1.Items[3].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["reqapp"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "Req Approval" + "</span>";
            //this.RadioButtonList1.Items[4].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["reqcs"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "CS Preparation" + "</span>";
            //this.RadioButtonList1.Items[5].Text = "<span class='fa fa-pencil-square-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["reqcschk"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "CS Checked" + "</span>";

            //this.RadioButtonList1.Items[6].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["reqcsapp"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "CS Approval" + "</span>";
            //this.RadioButtonList1.Items[7].Text = "<span class='fa fa-credit-card fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["reqpur"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "Purchase Confirmation" + "</span>";
            //this.RadioButtonList1.Items[8].Text = ((ASTUtility.Left(comcod, 1) == "7") ? "Goods" : "") + "<span class='fa fa-life-ring fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["reqpurord"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "Purchase Order" + "</span>";
            //this.RadioButtonList1.Items[9].Text = "<span class='fa fa-line-chart  fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["reqmr"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "Incoming Area" + "</span>";
            //this.RadioButtonList1.Items[10].Text = "<span class='fa fa-file-text-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["mrqc"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "QC Check" + "</span>";
            //this.RadioButtonList1.Items[11].Text = "<span class='fa fa-file-text-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["storcv"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "Store Receive" + "</span>";
            //this.RadioButtonList1.Items[12].Text = "<span class='fa fa-file-text-o fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ((Convert.ToDouble(ds1.Tables[7].Rows[0][""]) == 0) ? "0" : Convert.ToDouble(ds1.Tables[7].Rows[0]["readybill"]).ToString("#,##0;(#,##0); ")) + "</span>" + "<span class=lbldata2>" + "Costing" + "</span>";


            RadioButtonList1_SelectedIndexChanged(null,null);
            // All Recive
         






        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds1 =(DataSet)ViewState["dsLocalPurchase"];
            // ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = new DataTable();

            DataView dv = new DataView();

            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    this.Data_Bind("gvReqInfo", dv.ToTable());
                    //Status
                    this.pnlReqInfo.Visible = true;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.PnlReqApproval.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelComp.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelBill.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["style"] = "lblactive blink_me";
                    break;
                case "1":
                    //Checked             
                    //Req checked
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("checked <> 'Ok' ");
                    this.Data_Bind("gvReqCheck", dv.ToTable());

                    this.pnlReqInfo.Visible = false;
                    this.pnlReqCheck.Visible = true;
                    this.PnlReqAuth.Visible = false;
                    this.PnlReqApproval.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[1].Attributes["style"] = "lblactive blink_me";
                    break;
                case "2":
                    //Auth             
                    //Req Auth
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("checked <> '' and authid='' ");
                    this.Data_Bind("gvReqAuth", dv.ToTable());

                    this.pnlReqInfo.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = true;
                    this.PnlReqApproval.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[2].Attributes["style"] = "lblactive blink_me";
                    break;
                case "3":
                    //Approval              
                    //Req Approval
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("authid= 'Ok' and approved <> 'Ok' ");
                    this.Data_Bind("gvReqApproval", dv.ToTable());

                    this.pnlReqInfo.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.PnlReqApproval.Visible = true;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[3].Attributes["style"] = "lblactive blink_me";
                    break;

                case "4":
                    //CS Create
                    //CS Preparation
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("approved = 'Ok'  and mktserv <>  'Ok' ");
                    this.Data_Bind("gvCSCreate", dv.ToTable());

                    this.pnlReqInfo.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.PnlReqApproval.Visible = false;
                    this.PnlCSCreate.Visible = true;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[4].Attributes["style"] = "lblactive blink_me";
                    break;
                case "5":
                    //CS Check


                    //CS Check
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("fwrd = ''  and mktserv <>  '' ");
                    this.Data_Bind("gvCsCheck", dv.ToTable());

                    this.pnlReqInfo.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.PnlReqApproval.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = true;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[5].Attributes["style"] = "lblactive blink_me";
                    break;
                case "6":
                    //CS Approval
                    //Rate Proposal
                    dt = ((DataTable)ds1.Tables[1]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("fwrd <> '' and csapp <>'Ok' ");
                    this.Data_Bind("gvRatePro", dv.ToTable());


                    ////Rate Approval comment out by safi for unused gridview
                    //dt = ((DataTable)ds1.Tables[1]).Copy();
                    //dv = dt.DefaultView;

                    //dv.RowFilter = ("cstatus='CS Approval'  ");
                    ////dv.RowFilter = ("empid ='" + usrid + "'");
                    //this.Data_Bind("gvRateApp", dv.ToTable());


                    this.PnlReqApproval.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlReqInfo.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = true;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[6].Attributes["style"] = "lblactive blink_me";


                    break;
                case "7":
                    //Work Order


                    //Work Order
                    dt = (DataTable)ds1.Tables[3];
                    //dv = dt.DefaultView;
                    //dv.RowFilter = ("bblccode <> ''");
                    this.Data_Bind("gvWrkOrd", dt);

                    this.PnlReqApproval.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlReqInfo.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = true;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[7].Attributes["style"] = "lblactive blink_me";

                    break;

                case "8":
                    //Order Process
                    //PO App.   Order Process

                    dt = (DataTable)ds1.Tables[4].Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("appstatus='' ");
                    this.Data_Bind("gvOrdeProc", dt);

                    this.PnlReqApproval.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlReqInfo.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = true;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelComp.Visible = false;
                    // this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[8].Attributes["style"] = "lblactive blink_me";

                    break;
                

                case "9":
                    //MRR
                    ////MRr
                    dt = (DataTable)ds1.Tables[4];
                    dv = dt.DefaultView;
                    dv.RowFilter = ("appstatus<> '' ");
                    this.Data_Bind("grvMRec", dt);
                    ViewState["tblincomeArea"] = dt;


                    this.PnlReqApproval.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlReqInfo.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = true;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelComp.Visible = false;
                    this.PanelBill.Visible = false;
                    this.RadioButtonList1.Items[9].Attributes["style"] = "lblactive blink_me";

                    break;
                case "10":
                    //QC
                                     
                    dt = (DataTable)ds1.Tables[5];
                    this.Data_Bind("grvQC", dt);

                    this.PnlReqApproval.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlReqInfo.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = true;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelBill.Visible = false;
                    this.PanelComp.Visible = false;

                    this.RadioButtonList1.Items[10].Attributes["style"] = "lblactive blink_me";

                    break;
                case "11":
                    //Store Rec

                    //Store Recv

                    dt = ((DataTable)ds1.Tables[6]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("approved=''  ");
                    this.Data_Bind("gvStorRcv", dv.ToTable());

                    this.PnlReqApproval.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlReqInfo.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = true;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = false;

                    this.RadioButtonList1.Items[11].Attributes["style"] = "lblactive blink_me";

                    break;
                case "12":
                    //Bill

                    //Bill
                    dt = ((DataTable)ds1.Tables[6]).Copy();
                    dv = dt.DefaultView;
                    dv.RowFilter = ("approved<>''  ");
                    this.Data_Bind("gvPurBill", dv.ToTable());

                    this.PnlReqApproval.Visible = false;
                    this.pnlReqCheck.Visible = false;
                    this.PnlReqAuth.Visible = false;
                    this.pnlReqInfo.Visible = false;
                    this.PnlCSCreate.Visible = false;
                    this.PnlCSChk.Visible = false;
                    this.pnlRatePro.Visible = false;
                    this.PaneWorder.Visible = false;
                    this.pnlRateApp.Visible = false;
                    this.PanelPurchaseType.Visible = false;
                    this.PanelRecv.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.PanelComp.Visible = false;
                    //this.PanelQC.Visible = false;
                    this.PanelBill.Visible = true;

                    this.RadioButtonList1.Items[12].Attributes["style"] = "lblactive blink_me";

                    break;





            }
        }
        protected void gvReqInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");

                HyperLink hlnkgvgvmrfno = (HyperLink)e.Row.FindControl("hlnkgvgvmrfno");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();



                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string mrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrfno")).ToString();

                TableCell cell = e.Row.Cells[9];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Bill Confirm")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4BCF9E");
                }
                if (cstatus == "Purchase Invoice")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00CBF3");
                }
                if (cstatus == "Rate Proposal")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5EB75B");
                }
                if (cstatus == "Order Process")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D95350");
                }
                if (cstatus == "Rate Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#EFAD4D");
                }
                if (cstatus == "Materials Receved")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#769BF4");
                }
                if (cstatus == "QC Check")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#769BF4");
                }
                if (cstatus == "Purchase Order")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#76C9B5");
                }



                hlnkgvgvmrfno.NavigateUrl = "~/F_10_Procur/RptPurchasetracking?Type=Purchasetrk&reqno=" + reqno;
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Local&AppType=YES";





            }
        }
        protected void gvReqCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("gvHyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnUpdate");

                HyperLink hlnkgvapmrfno = (HyperLink)e.Row.FindControl("hlnkgvapmrfno");
                HyperLink HypApproval = (HyperLink)e.Row.FindControl("HypApproval");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();


                string prjCode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string mrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrfno")).ToString();



                hlnkgvapmrfno.NavigateUrl = "~/F_10_Procur/RptPurchasetracking?Type=Purchasetrk&reqno=" + reqno;
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Local&AppType=YES";
                hlink2.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=ReqEdit&comcod=" + comcod + "&actcode=" + prjCode + "&genno=" + reqno;
                
                HypApproval.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=FxtAstAuth&actcode=" + prjCode + "&genno=" + reqno;

            }
        }
        protected void gvReqAuth_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("gvHyInprPrint");

                HyperLink hlnkgvapmrfno = (HyperLink)e.Row.FindControl("hlnkgvapmrfno");
                HyperLink HypApproval = (HyperLink)e.Row.FindControl("HypApproval");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();


                string prjCode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string mrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrfno")).ToString();



                hlnkgvapmrfno.NavigateUrl = "~/F_10_Procur/RptPurchasetracking?Type=Purchasetrk&reqno=" + reqno;
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Local&AppType=YES";

                HypApproval.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=ReqReview&actcode=" + prjCode + "&genno=" + reqno;







            }
        }
        protected void gvReqApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("gvHyInprPrint");

                HyperLink hlnkgvapmrfno = (HyperLink)e.Row.FindControl("hlnkgvapmrfno");
                HyperLink HypApproval = (HyperLink)e.Row.FindControl("HypApproval");



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();



                string prjCode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string mrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrfno")).ToString();

                TableCell cell = e.Row.Cells[9];
                string cstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();
                if (cstatus == "Bill Confirm")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4BCF9E");
                }
                if (cstatus == "Purchase Invoice")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00CBF3");
                }
                if (cstatus == "Rate Proposal")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5EB75B");
                }
                if (cstatus == "Order Process")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#D95350");
                }
                if (cstatus == "Rate Approval")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#EFAD4D");
                }
                if (cstatus == "Materials Receved")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#769BF4");
                }
                if (cstatus == "QC Check")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#769BF4");
                }
                if (cstatus == "Purchase Order")
                {
                    cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#76C9B5");
                }



                //string fDate = Convert.ToDateTime(this.txFdate.Text).ToString("dd-MMM-yyyy");
                //string tDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                //hlink2.NavigateUrl = "~/F_20_Service/RecProductEntry.aspx?Type=Entry";
                //hlink2.ToolTip = "Create New";



                hlnkgvapmrfno.NavigateUrl = "~/F_10_Procur/RptPurchasetracking?Type=Purchasetrk&reqno=" + reqno;
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Local&AppType=YES";
                if (ASTUtility.Left(prjCode, 2) == "16")
                {
                    HypApproval.NavigateUrl = "~/F_15_Pro/PurReqEntry?InputType=Approval&actcode=" + prjCode + "&genno=" + reqno;
                }
                else
                {
                    HypApproval.NavigateUrl = "~/F_13_CWare/PurReqEntry02?InputType=FxtAstApproval&actcode=" + prjCode + "&genno=" + reqno;
                }






            }
        }
        protected void gvCSCreate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");




                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string status = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();

                string ReqType = "Local";
                //hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&RptTitel=Purchase Requisition Approval";
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Local&AppType=NO";

                // hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;

                hlink2.NavigateUrl = "~/F_10_Procur/PurMktSurvey02?Type=Create&genno=" + reqno + "&ReqType=" + ReqType;

                //if (status == "CS Preparation")
                //{
                //    hlink3.NavigateUrl = "~/F_07_Inv/PurReqEntry02.aspx?InputType=ReqEdit&actcode=" + pactcode + "&genno=" + reqno;

                //}



            }
        }
        protected void gvCsCheck_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                //HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");




                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string status = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cstatus")).ToString();

                string ReqType = "Local";
                //hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&RptTitel=Purchase Requisition Approval";
                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Local&AppType=NO";

                // hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;

                hlink2.NavigateUrl = "~/F_10_Procur/PurMktSurvey02?Type=Check&genno=" + reqno + "&ReqType=" + ReqType;

                //if (status == "CS Preparation")
                //{
                //    hlink3.NavigateUrl = "~/F_07_Inv/PurReqEntry02.aspx?InputType=ReqEdit&actcode=" + pactcode + "&genno=" + reqno;

                //}



            }
        }
        protected void gvRatePro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;

                hlink2.NavigateUrl = "~/F_10_Procur/PurMktSurvey02?Type=Approved&genno=" + reqno + "&ReqType=Local";


            }
        }
        protected void gvRateApp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();

                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno;

                hlink2.NavigateUrl = "~/F_10_Procur/PurMktSurvey02?Type=Approved&genno=" + reqno;

            }
        }
        protected void gvOrdeProc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
                HyperLink hlink4 = (HyperLink)e.Row.FindControl("HyInReqPrint");
                HyperLink hlink5 = (HyperLink)e.Row.FindControl("HyCSPrint");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString().Trim();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();




                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=OrderPrint&comcod=" + comcod + "&orderno=" + orderno;//
                hlink2.NavigateUrl = "~/F_10_Procur/PurWrkOrderEntryL?InputType=OrderApprove&genno=" + orderno + "&actcode=" + sircode;
                hlink3.NavigateUrl = "~/F_10_Procur/PurWrkOrderEntryL?InputType=OrderEdit&genno=" + orderno + "&actcode=" + sircode;
                hlink4.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Local&AppType=YES";
                hlink5.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;





                //string purtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purtype")).ToString();
                //LC
                //if (purtype == "25001")
                //{

                //    hlink2.NavigateUrl = "~/F_09_Commer/LCOpening?Type=Open&genno=" + reqno;
                //}
                ////BBLC

                //else if (purtype == "25002")
                //{

                //    hlink2.NavigateUrl = "~/F_09_Commer/BBLCInfo?InputType=Entry&actcode=" + pactcode + "&genno=" + reqno;

                //}
                ////Cash

                //else if (purtype == "25003")
                //{

                //}
                ////Local

                //else
                //{

                //}

                //hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;



            }
        }
        protected void gvWrkOrd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hnlinkprint = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("HypLnkPrintReq");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string bblccode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bblccode")).ToString();
                string ssircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();
                string purtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purtype")).ToString();

                switch (purtype)
                {
                    case "25004":
                        hlink1.NavigateUrl = "~/F_10_Procur/PurWrkOrderEntryL?InputType=OrderEntry&genno=" + reqno + "&actcode=" + ssircode;
                        break;
                    case "25002":
                        //hlink1.NavigateUrl = "~/F_09_Commer/PurWrkOrderEntry.aspx?InputType=OrderEntry&genno=" + reqno + "&actcode=" + pactcode + "&bblccode=" + bblccode;
                        break;

                }

                hnlinkprint.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + reqno;
                hlink3.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Local&AppType=YES";




            }
        }
        protected void grvMRec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
                LinkButton hlink4 = (LinkButton)e.Row.FindControl("btnSupplier");
                LinkButton lnkCheck = (LinkButton)e.Row.FindControl("lnkCheck");





                LinkButton hlinkdelrec = (LinkButton)e.Row.FindControl("btnDelRec");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString().Trim();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();
                string purtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purtype")).ToString();
                string authenbyid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "authenbyid")).ToString();

                if (purtype == "25003" || purtype == "25001")
                {
                    hlink3.Visible = false;
                    hlinkdelrec.Visible = false;

                }
                // 25003 cash
                if ((purtype == "25003" || purtype == "25004") && (sircode == "000000000000"))
                {
                    hlink4.Visible = true;
                    hlink4.Enabled = true;

                }


                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=OrderPrint&comcod=" + comcod + "&orderno=" + orderno;// +"&recvno=" + recvno + "&imesimeno=" + imesimeno;
                if (orderno.Substring(0, 3) == "POR")
                {
                    if (sircode == "000000000000")
                    {
                        hlink2.ToolTip = "Please Update Supplier Name";
                    }
                    else
                    {
                        hlink2.NavigateUrl = "~/F_15_Pro/PurMRREntry?Type=Entry&actcode=" + pactcode + "&genno=" + orderno + "&sircode=" + sircode;
                    }


                }
                else
                {
                    hlink2.NavigateUrl = "~/F_09_Commer/LcReceive?Type=Entry&comcod=" + comcod + "&actcode=" + orderno + "&centrid=&genno=";

                }
                //hlink3.NavigateUrl = "~/F_11_Pro/PurWrkOrderEntry.aspx?InputType=OrderEdit&genno=" + aprovno;

                //local purchase 
                if (purtype == "25004" || purtype == "25002")
                {
                    hlink3.NavigateUrl = "~/F_10_Procur/PurWrkOrderEntryL?InputType=OrderEdit&genno=" + orderno + "&actcode=" + sircode;

                }

                else
                {
                    //hlink3.NavigateUrl = "~/F_10_Procur/PurWrkOrderEntry.aspx?InputType=OrderEdit&genno=" + orderno + "&actcode=" + sircode;

                }

                if (authenbyid.Length != 0)
                {
                    //lnkCheck.Visible = false;
                }


            }
        }
        protected void grvQC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string mrrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrrno")).ToString();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).Trim().ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();

                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=MRRPrint&comcod=" + comcod + "&mrrno=" + mrrno;
                if (mrrno.Substring(0, 3) == "MRR")
                {
                    hlink2.NavigateUrl = "~/F_10_Procur/PurMRQCEntry?Type=Entry&actcode=" + pactcode + "&genno=" + reqno + "&mrrno=" + mrrno;

                }
                else
                {
                    hlink2.NavigateUrl = "~/F_09_Commer/LcQcRecv?Type=Entry&comcod=" + comcod + "&actcode=" + orderno + "&centrid=" + pactcode + "&genno=" + mrrno;

                }
                if (mrrno.Substring(0, 3) == "MRR")
                {
                    // hlink3.NavigateUrl = "~/F_15_Pro/PurMRREntry.aspx?Type=Mgt&genno=" + mrrno;
                    hlink3.NavigateUrl = "~/F_15_Pro/PurMRREntry?Type=Mgt&actcode=" + pactcode + "&genno=" + mrrno + "&sircode=" + sircode;

                }
                else
                {
                    hlink3.NavigateUrl = "~/F_09_Commer/LcReceive?Type=Edit&comcod=" + comcod + "&actcode=" + orderno + "&centrid=" + pactcode + "&genno=" + mrrno;
                }



            }
        }
        protected void gvPurBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlinkqcedit = (HyperLink)e.Row.FindControl("lnkbtnEditIN");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string purqcno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purqcno")).ToString().Trim();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString().Trim();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString().Trim();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString().Trim();
                string mrrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrrno")).ToString().Trim();

                if (purqcno.Substring(0, 2) == "QC")
                {
                    hlink2.NavigateUrl = "~/F_09_Commer/PurBillEntry?Type=BillEntry&genno=" + purqcno + "&sircode=" + sircode;

                    hlinkqcedit.NavigateUrl = "~/F_10_Procur/PurMRQCEntry?Type=Edit&actcode=" + pactcode + "&genno=" + purqcno + "&mrrno=" + mrrno;

                }
                else
                {
                    hlink2.NavigateUrl = "~/F_09_Commer/LCCostingDetails?Type=Entry&comcod=" + comcod + "&actcode=" + pactcode;
                    hlinkqcedit.NavigateUrl = "~/F_09_Commer/LcQcRecv?Type=Edit&comcod=" + comcod + "&actcode=" + orderno + "&centrid=" + pactcode + "&genno=" + purqcno;

                    // hlink2.NavigateUrl = "~/F_09_Commer/PurBillEntry.aspx?Type=BillEntry&genno=" + purqcno + "&sircode=" + sircode;
                }




            }
        }
        protected void grvComp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print.aspx?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                //hlink2.NavigateUrl = "~/F_07_Inv/PurBillEntry.aspx?Type=BillEntry";
                //hlink1.NavigateUrl = "~/F_99_Allinterface/PuchasePrint.aspx?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;
                //hlink2.NavigateUrl = "~/F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=Purchasetrk";

                hlink1.NavigateUrl = "~/F_11_Pro/PuchasePrint?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;
                hlink2.NavigateUrl = "~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk";

            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
            }

            return dt1;
        }
        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvReqInfo":
                    this.gvReqInfo.DataSource = HiddenSameData(dt);
                    this.gvReqInfo.DataBind();
                    break;

                case "gvReqCheck":
                    this.gvReqCheck.DataSource = HiddenSameData(dt);
                    this.gvReqCheck.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvReqCheck.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvReqCheck.Rows[i].FindControl("lblgvapreqno")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvReqCheck.Rows[i].FindControl("gvreqcheckDelete");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }
                    break;
                case "gvReqAuth":
                    this.gvReqAuth.DataSource = HiddenSameData(dt);
                    this.gvReqAuth.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvReqAuth.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvReqAuth.Rows[i].FindControl("lblgvapreqno")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvReqAuth.Rows[i].FindControl("gvaprbtnDelReqChk");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }

                    break;
                case "gvReqApproval":
                    this.gvReqApproval.DataSource = HiddenSameData(dt);
                    this.gvReqApproval.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvReqApproval.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvReqApproval.Rows[i].FindControl("lblgvapreqno")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvReqApproval.Rows[i].FindControl("btnDelReqRev");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }
                    break;

                case "gvCSCreate":
                    this.gvCSCreate.DataSource = HiddenSameData(dt);
                    this.gvCSCreate.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvCSCreate.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvCSCreate.Rows[i].FindControl("lblgvreqno")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvCSCreate.Rows[i].FindControl("btnDelReqApp");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno;
                    }


                    break;
                case "gvCsCheck":
                    this.gvCsCheck.DataSource = HiddenSameData(dt);
                    this.gvCsCheck.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvCsCheck.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvCsCheck.Rows[i].FindControl("lblgvreqno")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvCsCheck.Rows[i].FindControl("btnCSRev");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + "Next";
                    }


                    break;

                case "gvRatePro":

                    this.gvRatePro.DataSource = HiddenSameData(dt);
                    this.gvRatePro.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvRatePro.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvRatePro.Rows[i].FindControl("lblgvreqno")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvRatePro.Rows[i].FindControl("btnDelCSNext");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + "Check";
                    }


                    break;
                case "gvRateApp":  ///Un used

                    this.gvRateApp.DataSource = HiddenSameData(dt);
                    this.gvRateApp.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvRateApp.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvRateApp.Rows[i].FindControl("lblgvreqno")).Text.Trim();
                        LinkButton lbtn1 = (LinkButton)gvRateApp.Rows[i].FindControl("btnCSchk");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + "Check";
                    }
                    break;


                case "gvOrdeProc":

                    this.gvOrdeProc.DataSource = HiddenSameData(dt);
                    this.gvOrdeProc.DataBind();

                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvOrdeProc.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvOrdeProc.Rows[i].FindControl("lblgvreqno")).Text.Trim();
                        string orderno = ((Label)gvOrdeProc.Rows[i].FindControl("lblgvorderno")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)gvOrdeProc.Rows[i].FindControl("btnCSApp");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + "Approved";

                        LinkButton lbtnDelPoApp = (LinkButton)gvOrdeProc.Rows[i].FindControl("btnDelPoApp");
                        if (lbtnDelPoApp != null)
                            if (lbtnDelPoApp.Text.Trim().Length > 0)
                                lbtnDelPoApp.CommandArgument = orderno;
                    }


                    break;
                case "gvWrkOrd":

                    this.gvWrkOrd.DataSource = HiddenSameData(dt);
                    this.gvWrkOrd.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvWrkOrd.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvWrkOrd.Rows[i].FindControl("lgvPoreqno")).Text.Trim();
                        string aprovno = ((Label)gvWrkOrd.Rows[i].FindControl("lgvbblccode")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)gvWrkOrd.Rows[i].FindControl("btnDelOrProc");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + aprovno;
                    }
                    break;

                case "grvMRec":

                    this.grvMRec.DataSource = HiddenSameData(dt);
                    this.grvMRec.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < grvMRec.Rows.Count; i++)
                    {
                        string Reqno = ((Label)grvMRec.Rows[i].FindControl("lblgvreqno")).Text.Trim();
                        string orderno = ((Label)grvMRec.Rows[i].FindControl("lblgvorderno")).Text.Trim();
                        string purtype = ((Label)grvMRec.Rows[i].FindControl("lblpurtype")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)grvMRec.Rows[i].FindControl("btnDelRec");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + orderno;
                    }
                    //if (dt.Rows.Count == 0)
                    //    return;


                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispAmtTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmamt)", "")) ?
                    //0 : dt.Compute("sum(itmamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispPTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
                    //0 : dt.Compute("sum(paidamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    //((Label)this.gvDispatch.FooterRow.FindControl("lblDispQTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmqty)", "")) ?
                    //0 : dt.Compute("sum(itmqty)", ""))).ToString("#,##0;(#,##0);");


                    break;
                case "grvQC":

                    this.grvQC.DataSource = HiddenSameData(dt);
                    this.grvQC.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < grvQC.Rows.Count; i++)
                    {
                        string Reqno = ((Label)grvQC.Rows[i].FindControl("lblQCgvreqno")).Text.Trim();
                        string mrrno = ((Label)grvQC.Rows[i].FindControl("lblqcgvorderno")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)grvQC.Rows[i].FindControl("btnDelexitsRecv");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + mrrno;
                    }

                    break;
                case "gvStorRcv":

                    this.gvStorRcv.DataSource = HiddenSameData(dt);
                    this.gvStorRcv.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvStorRcv.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvStorRcv.Rows[i].FindControl("lblgvreqnoqc")).Text.Trim();
                        string qcno = ((Label)gvStorRcv.Rows[i].FindControl("lblgbPrqcno")).Text.Trim();
                        string pactcode = ((Label)gvStorRcv.Rows[i].FindControl("lblgvpactcodeqc")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)gvStorRcv.Rows[i].FindControl("btnDelQC");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + pactcode + qcno;
                    }
                    break;

                case "gvPurBill":

                    this.gvPurBill.DataSource = HiddenSameData(dt);
                    this.gvPurBill.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    for (int i = 0; i < gvPurBill.Rows.Count; i++)
                    {
                        string Reqno = ((Label)gvPurBill.Rows[i].FindControl("lblgvreqnoqc")).Text.Trim();
                        string qcno = ((Label)gvPurBill.Rows[i].FindControl("lblgbPrqcno")).Text.Trim();
                        string pactcode = ((Label)gvPurBill.Rows[i].FindControl("lblgvpactcodeqc")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)gvPurBill.Rows[i].FindControl("btnDelSRcv");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = Reqno + pactcode + qcno;
                    }
                    break;
                case "grvComp":

                    this.grvComp.DataSource = HiddenSameData(dt);
                    this.grvComp.DataBind();

                    break;


            }


            this.FooterCalculation(gv, dt);




        }
        private void FooterCalculation(string gv, DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
            switch (gv)
            {
                case "gvReqInfo":
                    ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFApamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(apamt)", "")) ?
                    0 : dt.Compute("sum(apamt)", ""))).ToString("#,##0;(#,##0);");

                    break;
                case "gvCSCreate":
                    break;
                case "gvRatePro":
                    break;
                case "gvRateApp":
                    break;
                case "gvOrdeProc":
                    //((Label)this.gvOrdeProc.FooterRow.FindControl("lblgvFOrProAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amount)", "")) ?
                    //0 : dt.Compute("sum(amount)", ""))).ToString("#,##0;(#,##0);");
                    break;
                case "gvWrkOrd":
                    //((Label)this.gvWrkOrd.FooterRow.FindControl("lblgvFWoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(woamt)", "")) ?
                    //0 : dt.Compute("sum(woamt)", ""))).ToString("#,##0;(#,##0);");                
                    break;
                case "grvMRec":
                    //((Label)this.grvMRec.FooterRow.FindControl("lblgvFWoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvamt)", "")) ?
                    //0 : dt.Compute("sum(recvamt)", ""))).ToString("#,##0;(#,##0);");
                    break;
                case "grvQC":
                    //((Label)this.grvMRec.FooterRow.FindControl("lblgvQCFWoamtda")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rqcamt)", "")) ?
                    //0 : dt.Compute("sum(rqcamt)", ""))).ToString("#,##0;(#,##0);");
                    break;
                case "gvPurBill":
                    ((Label)this.gvPurBill.FooterRow.FindControl("lblgvFmrramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(purqcamt)", "")) ?
                    0 : dt.Compute("sum(purqcamt)", ""))).ToString("#,##0;(#,##0);");
                    break;
                case "grvComp":
                    break;
            }
        }
        private string XmlDataInsert(string Reqno, string Apprno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");

            ds.Tables[1].Columns.Add("delbyid", typeof(string));
            ds.Tables[1].Columns.Add("delseson", typeof(string));
            ds.Tables[1].Columns.Add("deldate", typeof(DateTime));

            ds.Tables[1].Rows[0]["delbyid"] = usrid;
            ds.Tables[1].Rows[0]["delseson"] = session;
            ds.Tables[1].Rows[0]["deldate"] = Date;


            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";

            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno, Apprno);

            if (!resulta)
            {

                //return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Deleted";
                ((Label)this.Master.FindControl("lblmsg")).Attributes["style"] = "background:Green;";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

            return "";
        }
        /// delete methods start
        protected void btnDelReq_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();

            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();



            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQNO", Reqno, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "CS Next Delete", "Order No: ", Reqno);
        }
        protected void btnDelCSNext_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string Type = Code.Substring(14).ToString();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();



            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "REVCSPART", Reqno, Type, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CS Check Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "CS Next Delete", "Order No: ", Reqno);
        }
        protected void btnCSchk_Click(object sender, EventArgs e)
        {
            // ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string Type = Code.Substring(14).ToString();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();



            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE_01", "REVCSPART", Reqno, Type, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CS Audit Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "CS Audit Delete", "Order No: ", Reqno);
        }
        protected void btnCSRev_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string Type = Code.Substring(14).ToString();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }


            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "REVCSPART", Reqno, Type, userid, Date, "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('CS Check Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "CS Approval Delete", "Order No: ", Reqno);
        }
        protected void btnDelOrProc_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string Apprno = Code.Substring(14).ToString();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "REVORDERPROCESS", Reqno, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Process Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Order Process Delete", "Order No: ", Reqno + "-" + Apprno);
        }

        //DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "GETAPPINFO", Reqno);
        //this.XmlDataInsert(Reqno, Apprno, ds);

        protected void btnDelPoApp_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string Apprno = Code.Substring(14).ToString();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "REVPURORDERRECEIVE", Reqno, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Approved Order Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Approved Order Delete", "Order No: ", Reqno + "-" + Apprno);
        }

        protected void btnDelOrder_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string Orderno = Code.Substring(14).ToString();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE_01", "GETORDERINFO", Orderno);
            this.XmlDataInsert(Reqno, Orderno, ds);
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE_01", "REVWORKORDER", Orderno, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Work Order Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Work Order Delete", "Order No: ", Reqno + "-" + Orderno);
        }

        protected void btnDelRec_Click(object sender, EventArgs e)
        {
            // ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string recvno = Code.Substring(14, 14).ToString();

            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }
            string calltype = "GETPURORDERINFO";
            //if (purtype == "25002")
            //{
            //    calltype = "GETMRRINFO";
            //}
            //else if (purtype == "25002")
            //{
            //    calltype = "";

            //}
            //else if (purtype == "25003")
            //{
            //    calltype = "";

            //}
            //else
            //{
            //    calltype = "";

            //}


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", calltype, recvno);
            //this.XmlDataInsert(Reqno, recvno, ds);
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "REVPURORDERRECEIVE", recvno, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Receive Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Receive Delete", "Order No: ", Reqno + "-" + recvno);
        }
        protected void btnDelexitsRecv_Click(object sender, EventArgs e)
        {
            // ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string mrrno = ASTUtility.Right(Code, 14).ToString();

            if (mrrno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE_01", "GETMRRINFO", qcno);
            //this.XmlDataInsert(Reqno, qcno, ds);
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "REVMATRECEIVE", Reqno, mrrno, "Local", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('MR Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "MR Delete", "MR No: ", Reqno + "-" + mrrno);
        }
        protected void btnDelQC_Click(object sender, EventArgs e)
        {
            // ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string pactcode = Code.Substring(14, 12).ToString();
            string qcno = Code.Substring(26).ToString();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE_01", "GETMRRINFO", qcno);
            //this.XmlDataInsert(Reqno, qcno, ds);
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "PUR_REVERSE", "DELQC", qcno, pactcode, Reqno, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('QC Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "QC Delete", "Order No: ", Reqno + "-" + qcno);
        }
        protected void btnDelSRcv_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = ASTUtility.Left(Code, 14).ToString();
            string pactcode = Code.Substring(14, 12).ToString();
            string qcno = Code.Substring(26).ToString();
            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE_01", "GETMRRINFO", qcno);
            //this.XmlDataInsert(Reqno, qcno, ds);
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "PUR_REVERSE", "DELSRCV", qcno, pactcode, Reqno, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Store Receive Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Store Receive Delete", "Order No: ", Reqno + "-" + qcno);
        }
        protected void gvaprbtnDelReqChk_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = Code;//ASTUtility.Left(Code, 14).ToString();

            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "DELETEREQCHK", Reqno, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Checked Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Requisition Checked Delete", "Order No: ", Reqno);
        }
        protected void btnDelReqApp_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = Code;//ASTUtility.Left(Code, 14).ToString();

            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string userid = hst["usrid"].ToString();



            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "DELETEREQAPPROVAl", Reqno, userid, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Approval Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Requisition Approval Delete", "Order No: ", Reqno);
        }
        protected void btnDelReqRev_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = Code;//ASTUtility.Left(Code, 14).ToString();

            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string userid = hst["usrid"].ToString();



            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "DELETEREQAUTH", Reqno, userid, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Review Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Requisition Approval Delete", "Order No: ", Reqno);
        }
        protected void gvStorRcv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                //HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlinkqcedit = (HyperLink)e.Row.FindControl("lnkbtnEditIN");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string purqcno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "purqcno")).ToString().Trim();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString().Trim();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString().Trim();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString().Trim();
                string mrrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrrno")).ToString().Trim();

                if (purqcno.Substring(0, 2) == "QC")
                {
                    //hlink2.NavigateUrl = "~/F_09_Commer/PurBillEntry.aspx?Type=BillEntry&genno=" + purqcno + "&sircode=" + sircode;

                    hlinkqcedit.NavigateUrl = "~/F_10_Procur/PurMRQCEntry?Type=Approve&actcode=" + pactcode + "&genno=" + purqcno + "&mrrno=" + mrrno;

                }
                else
                {
                    //hlink2.NavigateUrl = "~/F_09_Commer/LCCostingDetails.aspx?Type=Entry&comcod=" + comcod + "&actcode=" + pactcode;
                    hlinkqcedit.NavigateUrl = "~/F_09_Commer/LcQcRecv?Type=Approve&comcod=" + comcod + "&actcode=" + orderno + "&centrid=" + pactcode + "&genno=" + purqcno;

                    // hlink2.NavigateUrl = "~/F_09_Commer/PurBillEntry.aspx?Type=BillEntry&genno=" + purqcno + "&sircode=" + sircode;
                }




            }
        }
        protected void btnSupplier_Click(object sender, EventArgs e)
        {
            var row = ((LinkButton)sender).NamingContainer;
            Label reqno = (Label)row.FindControl("lblgvreqno");
            Label purcode = (Label)row.FindControl("lblpurtype");
            DataTable dt = (DataTable)ViewState["tblincomeArea"];

            this.lblReqno.Text = reqno.Text.Trim();
            this.lblpurcode.Text = purcode.Text.Trim();


            GetSupplier();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalSuppl();", true);

        }
        protected void lnkSupllerupdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string reqno = this.lblReqno.Text.Trim();
            string purcode = this.lblpurcode.Text.Trim();
            string supcode = this.ddlSup.SelectedValue.Trim().ToString();
            bool result = accData.UpdateTransInfo1(comcod, "SP_ENTRY_PURCHASE_01", "UPDATECASHSUPPLIER", reqno, supcode, purcode);
            if (result != true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Save Fail');", true);

            }
            else
            {

                // ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#modalSulierList').modal('hide')", true);

                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal();", true);
                Response.Redirect(Request.RawUrl);

            }

        }
        private void GetSupplier()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                return;
            }

            DataView dv = new DataView();
            DataTable dt = ds1.Tables[0];
            dv = dt.DefaultView;
            dv.RowFilter = ("ssircode <> '000000000000' ");


            this.ddlSup.DataTextField = "ssirdesc1";
            this.ddlSup.DataValueField = "ssircode";
            this.ddlSup.DataSource = dv.ToTable();
            this.ddlSup.DataBind();
        }
        protected void lnkCheck_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string orderno = ((Label)grvMRec.Rows[index].FindControl("lblgvorderno")).Text.ToString();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_LOCAL_INTERFACE", "POACHARRI", orderno, userid, Terminal, Sessionid, Posteddat);


            if (result == true)
            {
                this.PurchaseInfoRpt();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Arrival Complete Successfully');", true);
            }
        }

        protected void gvreqcheckDelete_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Reqno = Code;//ASTUtility.Left(Code, 14).ToString();

            if (Reqno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "DELETE_REQUISITION", Reqno, "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Requisition Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "Requisition Delete", "Order No: ", Reqno);
        }

        protected void lnkBtngvOrdeProcSendMail_Click(object sender, EventArgs e)
        {
            //DataSet ds = (DataSet)ViewState["dsLocalPurchase"];
            //DataTable dt = ds.Tables[4];

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            //string comcod = this.GetCompCode();
            //string mORDERNO = dt.Rows[index]["orderno"].ToString();

            string comcod = this.GetCompCode();
            string mORDERNO = ((Label)gvOrdeProc.Rows[index].FindControl("lblgvorderno")).Text;

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "LoadRdlcVIewer('" + comcod + "', '" + mORDERNO + "');", true);
        }
    }
}