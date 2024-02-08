using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Globalization;
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
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_11_RawInv
{
    public partial class RptWareHouseInterface : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Warehouse Smartface";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                
                this.txtFDate.Text = System.DateTime.Today.AddDays(-15).ToString("dd-MMM-yyyy");
                this.TxtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");   

                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();     
                this.PnlInt.Visible = true;
                this.GetSesson();
                this.GetLCName();
                //this.GetLCCode();
                this.ImportInterFace();
                this.PanelVisible();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

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
            DdlSeason_SelectedIndexChanged(null, null);
        }

        private void GetLCName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string txtSProject = "1601%";
            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string customer = "%";
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETORDERNO", txtSProject, findseason, "", "", "", "", "", "", "");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GETBOMLIST", findseason, customer, "", "", "", "", "", "", "");
            // BOM List appear as per WH Request by Safi-05-Nov-23
            if (ds1 == null)
                return;

            DataRow dr = ds1.Tables[0].NewRow();
            dr["bomdesc"] = "All";
            dr["mlccod"] = "000000000000";
            ds1.Tables[0].Rows.Add(dr);

            this.ddlLCName.DataTextField = "bomdesc";
            this.ddlLCName.DataValueField = "mlccod";
            this.ddlLCName.DataSource = ds1.Tables[0];
            this.ddlLCName.DataBind();
            this.ddlLCName.SelectedValue = "000000000000";
        }

        private void GetLCCode()
        {

            string comcod = this.GetCompCode();
            string SlcNO = "%%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "RETRIVE_LC_VALUE", SlcNO, "14", "", "", "", "", "", "", "");

            this.ddlLCName.DataTextField = "actdesc";
            this.ddlLCName.DataValueField = "actcode";
            this.ddlLCName.DataSource = ds1.Tables[0];
            this.ddlLCName.DataBind();

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

            this.ddlLCName.DataTextField = "sirdesc";
            this.ddlLCName.DataValueField = "sircode";
            this.ddlLCName.DataSource = ds1.Tables[0];
            this.ddlLCName.DataBind();

        }

        //private void ColoumVisiable()
        //    {
        //        this.gvAchList.Columns[4].Visible = true;
        //        //this.grvProReq.Columns[4].Visible = true;
        //        ////this.grvProIssue.Columns[4].Visible = true;
        //        //this.grvProdtion.Columns[4].Visible = true;
        //        //this.grvQCEntry.Columns[4].Visible = true;
        //        ////this.gvstorec.Columns[4].Visible = true;
        //        //this.grvComp.Columns[4].Visible = true;
        //        //this.gvProdInfo.Columns[4].Visible = true;
        //    }

        private void PanelVisible()
        {


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string orderno = dt1.Rows[0]["orderno"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

                if (dt1.Rows[j]["orderno"].ToString() == orderno)
                {
                    orderno = dt1.Rows[j]["orderno"].ToString();
                    dt1.Rows[j]["ssirdesc"] = "";
                    dt1.Rows[j]["orderno1"] = "";
                    // dt1.Rows[j]["ssirdesc"] = "";
                }

                else
                {
                    orderno = dt1.Rows[j]["orderno"].ToString();
                }

            }

            return dt1;
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            DateTime fromDate = Convert.ToDateTime(this.txtFDate.Text);
            DateTime toDate = Convert.ToDateTime(this.TxtToDate.Text);

            if ((toDate - fromDate).TotalDays >= 15 && this.ddlLCName.SelectedValue.ToString() == "000000000000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('To select All Order, the date must be less than 15 days');", true);
                return;
            }

            this.ImportInterFace();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //int day = ASTUtility.Datediffday(Convert.ToDateTime(this.txtFDate.Text), Convert.ToDateTime(this.txtdate.Text));
            //if (day != 0)
            //    return;
            //txtdate_TextChanged(null, null);
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.ImportInterFace();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PanelVisible();
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();
            DataSet ds = (DataSet)ViewState["tbldata1"];
            DataTable dt = ds.Tables[0];

            DataTable Tempdt = new DataTable();
            DataView Tempdv = new DataView();
            switch (value)
            {
                case "0": ///AchList
                    this.pnlAchList.Visible = true;
                    this.pnlArvList.Visible = false;
                    this.PnlIncom.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.pnlMatIssue.Visible = false;
                    this.pnlIndentIssue.Visible = false;
                    this.PnlgStrec.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("authenbyid ='' ");
                    this.Data_Bind("gvAchList", Tempdv.ToTable());
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1": ///ArrivList
                    this.pnlAchList.Visible = false;
                    this.pnlArvList.Visible = true;
                    this.PnlIncom.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.pnlMatIssue.Visible = false;
                    this.pnlIndentIssue.Visible = false;
                    this.PnlgStrec.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = false;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("authenbyid <>''  and aprvbyid=''");
                    this.Data_Bind("gvArrivList", Tempdv.ToTable());
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2": ///Incoming
                    this.pnlAchList.Visible = false;
                    this.pnlArvList.Visible = false;
                    this.PnlIncom.Visible = true;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.pnlMatIssue.Visible = false;
                    this.pnlIndentIssue.Visible = false;
                    this.PnlgStrec.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = false;
                    this.Data_Bind("gvIncomList", ds.Tables[1]);
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "3": ///QC
                    this.pnlAchList.Visible = false;
                    this.pnlArvList.Visible = false;
                    this.PnlIncom.Visible = false;
                    this.PanelQc.Visible = true;
                    this.PanelStoreRcv.Visible = false;
                    this.pnlMatIssue.Visible = false;
                    this.pnlIndentIssue.Visible = false;
                    this.PnlgStrec.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.Data_Bind("grvQC", ds.Tables[2]);
                    this.PnlFGChln.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;
                case "4": ///Store Recv
                    this.pnlAchList.Visible = false;
                    this.pnlArvList.Visible = false;
                    this.PnlIncom.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = true;
                    this.pnlMatIssue.Visible = false;
                    this.pnlIndentIssue.Visible = false;
                    this.PnlgStrec.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = false;
                    this.Data_Bind("gvStorRcv", ds.Tables[3]);
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;
                case "5": ///Mat Issue
                    this.pnlAchList.Visible = false;
                    this.pnlArvList.Visible = false;
                    this.PnlIncom.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.pnlMatIssue.Visible = true;
                    this.pnlIndentIssue.Visible = false;
                    this.PnlgStrec.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = false;
                    this.Data_Bind("grvProIssue", ds.Tables[4]);
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;
                case "6": ///Indent Issue
                    this.pnlAchList.Visible = false;
                    this.pnlArvList.Visible = false;
                    this.PnlIncom.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.pnlMatIssue.Visible = false;
                    this.pnlIndentIssue.Visible = true;
                    this.PnlgStrec.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = false;
                    this.Data_Bind("gvPromData", ds.Tables[5]);
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    break;
                case "7": ///Goods Recv
                    this.pnlAchList.Visible = false;
                    this.pnlArvList.Visible = false;
                    this.PnlIncom.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.pnlMatIssue.Visible = false;
                    this.pnlIndentIssue.Visible = false;
                    this.PnlgStrec.Visible = true;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = false;
                    this.Data_Bind("gvgstorec", ds.Tables[6]);
                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
                    break;
                case "8": ///Goods Delivery
                    this.pnlAchList.Visible = false;
                    this.pnlArvList.Visible = false;
                    this.PnlIncom.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.pnlMatIssue.Visible = false;
                    this.pnlIndentIssue.Visible = false;
                    this.PnlgStrec.Visible = false;
                    this.pnlFGDeliv.Visible = true;
                    this.PnlFGChln.Visible = false;
                    this.Data_Bind("gvFGDeliv", ds.Tables[7]);
                    this.RadioButtonList1.Items[8].Attributes["class"] = "lblactive blink_me";
                    break;
                case "9": /// Delivery Challan
                    this.pnlAchList.Visible = false;
                    this.pnlArvList.Visible = false;
                    this.PnlIncom.Visible = false;
                    this.PanelQc.Visible = false;
                    this.PanelStoreRcv.Visible = false;
                    this.pnlMatIssue.Visible = false;
                    this.pnlIndentIssue.Visible = false;
                    this.PnlgStrec.Visible = false;
                    this.pnlFGDeliv.Visible = false;
                    this.PnlFGChln.Visible = true;
                    this.Data_Bind("gvFGChln", ds.Tables[8]);
                    this.RadioButtonList1.Items[9].Attributes["class"] = "lblactive blink_me";
                    break;
            }
        }

        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string value = this.RadioButtonList2.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.GetLCName();
                    break;
                case "1":
                    this.GetLCCode();
                    break;
                case "2":
                    this.GetSupplierName();
                    break;
            }

        }

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList2.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.GetLCName();
                    break;
                case "1":
                    this.GetLCCode();
                    break;
                case "2":
                    this.GetSupplierName();
                    break;
            }
        }

        private void ImportInterFace()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();

            string Date1 = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Date2 = Convert.ToDateTime(this.TxtToDate.Text).ToString("dd-MMM-yyyy");
            string season = this.DdlSeason.SelectedItem.Value == "00000" ? "%" : this.DdlSeason.SelectedItem.Value + "%";
            string LCcode = (this.ddlLCName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlLCName.SelectedValue.ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_WAREHOUSE_INTERFACE", "WAREHOUSEDASHBOARD", Date1, usrid, Date2, season, LCcode, "", "", "", "");

            if (ds1 == null)
                return;

            this.gvAchList.DataSource = null;
            this.gvAchList.DataBind();

            this.gvArrivList.DataSource = null;
            this.gvArrivList.DataBind();

            this.gvIncomList.DataSource = null;
            this.gvIncomList.DataBind();

            this.grvQC.DataSource = null;
            this.grvQC.DataBind();

            this.gvStorRcv.DataSource = null;
            this.gvStorRcv.DataBind();

            this.grvProIssue.DataSource = null;
            this.grvProIssue.DataBind();

            this.gvPromData.DataSource = null;
            this.gvPromData.DataBind();

            this.gvgstorec.DataSource = null;
            this.gvgstorec.DataBind();

            this.gvFGDeliv.DataSource = null;
            this.gvFGDeliv.DataBind();

            this.gvFGChln.DataSource = null;
            this.gvFGChln.DataBind();


            ViewState["tbldata1"] = ds1;

            this.RadioButtonList1.Items[0].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[9].Rows[0]["achqty"].ToString() + "</div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> P.O Archive </div></div></div>";
            this.RadioButtonList1.Items[1].Text = "<div class='circle-tile'><a><div class='circle-tile-heading red counter'>" + ds1.Tables[9].Rows[0]["arvqty"].ToString() + "</i></div></a><div class='circle-tile-content red'><div class='circle-tile-description text-faded'> P.O Arrival </div></div></div>";
            this.RadioButtonList1.Items[2].Text = "<div class='circle-tile'><a><div class='circle-tile-heading purple counter'>" + ds1.Tables[9].Rows[0]["inqty"].ToString() + "</i></div></a><div class='circle-tile-content purple'><div class='circle-tile-description text-faded'> Incoming Area </div></div></div>";
            this.RadioButtonList1.Items[3].Text = "<div class='circle-tile'><a><div class='circle-tile-heading orange counter'>" + ds1.Tables[9].Rows[0]["iqcqty"].ToString() + "</i></div></a><div class='circle-tile-content orange'><div class='circle-tile-description text-faded'> IQC Check </div></div></div>";
            this.RadioButtonList1.Items[4].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-gray counter'>" + ds1.Tables[9].Rows[0]["srqty"].ToString() + "</i></div></a><div class='circle-tile-content dark-gray'><div class='circle-tile-description text-faded'> Store Receive </div></div></div>";
            this.RadioButtonList1.Items[5].Text = "<div class='circle-tile'><a><div class='circle-tile-heading yellow counter'>" + ds1.Tables[9].Rows[0]["isuqty"].ToString() + "</i></div></a><div class='circle-tile-content yellow'><div class='circle-tile-description text-faded'> Mat. Issue </div></div></div>";
            this.RadioButtonList1.Items[6].Text = "<div class='circle-tile'><a><div class='circle-tile-heading blue counter'>" + ds1.Tables[9].Rows[0]["indisuqty"].ToString() + "</i></div></a><div class='circle-tile-content blue'><div class='circle-tile-description text-faded'> Indent Issue </div></div></div>";
            this.RadioButtonList1.Items[7].Text = "<div class='circle-tile'><a><div class='circle-tile-heading green counter'>" + ds1.Tables[9].Rows[0]["fgrecqty"].ToString() + "</i></div></a><div class='circle-tile-content green'><div class='circle-tile-description text-faded'> FG Receive </div></div></div>";
            this.RadioButtonList1.Items[8].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[9].Rows[0]["fgdeliv"].ToString() + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> FG Delivery </div></div></div>";
            this.RadioButtonList1.Items[9].Text = "<div class='circle-tile'><a><div class='circle-tile-heading dark-blue counter'>" + ds1.Tables[9].Rows[0]["fgchln"].ToString() + "</i></div></a><div class='circle-tile-content dark-blue'><div class='circle-tile-description text-faded'> FG Challan </div></div></div>";

            RadioButtonList1_SelectedIndexChanged(null, null);


        }

        private void Data_Bind(string gv, DataTable dt)
        {
            switch (gv)
            {
                case "gvAchList":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvAchList.DataSource = HiddenSameData(dt);
                    this.gvAchList.DataBind();
                    break;
                case "gvArrivList":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvArrivList.DataSource = HiddenSameData(dt);
                    this.gvArrivList.DataBind();
                    break;
                case "gvIncomList":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvIncomList.DataSource = HiddenSameData(dt);
                    this.gvIncomList.DataBind();
                    break;
                case "grvQC":

                    if (dt.Rows.Count == 0)
                        return;
                    this.grvQC.DataSource = HiddenSameData(dt);
                    this.grvQC.DataBind();
                    for (int i = 0; i < grvQC.Rows.Count; i++)
                    {
                        string storid = ((Label)grvQC.Rows[i].FindControl("lblQCgvreqno")).Text.Trim();
                        string mrrno = ((Label)grvQC.Rows[i].FindControl("lblqcgvmrrno")).Text.Trim();
                        string orderno = ((Label)grvQC.Rows[i].FindControl("lblgvorderno")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)grvQC.Rows[i].FindControl("btnDelexitsRecv");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = storid + mrrno + orderno;
                    }
                    break;
                case "gvStorRcv":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvStorRcv.DataSource = HiddenSameData(dt);
                    this.gvStorRcv.DataBind();


                    for (int i = 0; i < gvStorRcv.Rows.Count; i++)
                    {
                        string storid = ((Label)gvStorRcv.Rows[i].FindControl("lblgvreqnoqc")).Text.Trim();
                        string qcno = ((Label)gvStorRcv.Rows[i].FindControl("lblgbPrqcno")).Text.Trim();
                        string pactcode = ((Label)gvStorRcv.Rows[i].FindControl("lblgvpactcodeqc")).Text.Trim();

                        LinkButton lbtn1 = (LinkButton)gvStorRcv.Rows[i].FindControl("btnDelQC");
                        if (lbtn1 != null)
                            if (lbtn1.Text.Trim().Length > 0)
                                lbtn1.CommandArgument = storid + pactcode + qcno;
                    }

                    break;
                case "grvProIssue":

                    if (dt.Rows.Count == 0)
                        return;
                    this.grvProIssue.DataSource = (dt);
                    this.grvProIssue.DataBind();
                    break;
                case "gvPromData":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvPromData.DataSource = (dt);
                    this.gvPromData.DataBind();
                    break;
                case "gvgstorec":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvgstorec.DataSource = (dt);
                    this.gvgstorec.DataBind();
                    break;
                case "gvFGDeliv":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvFGDeliv.DataSource = (dt);
                    this.gvFGDeliv.DataBind();
                    break;
                case "gvFGChln":

                    if (dt.Rows.Count == 0)
                        return;
                    this.gvFGChln.DataSource = (dt);
                    this.gvFGChln.DataBind();
                    break;


            }
        }

        protected void gvAchList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyReqPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("HypPOPrint");
                LinkButton lbtn = (LinkButton)e.Row.FindControl("lnkCheck");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString().Trim();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string ssircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();
                string reqtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqtype")).ToString();
                if (ssircode == "000000000000")
                {
                    lbtn.Visible = false;

                    lbtn.ToolTip = "Supplier Not Selected";
                }
                reqtype = (reqtype == "LC") ? "Import" : "Local";

                hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=" + reqtype + "&AppType=YES";


                hlink2.NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=OrderPrint&comcod=" + comcod + "&orderno=" + orderno;
            }
        }

        protected void gvIncomList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkCheck");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString().Trim();
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();



                //hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=OrderPrint&comcod=" + comcod + "&orderno=" + orderno;// +"&recvno=" + recvno + "&imesimeno=" + 

                if (orderno.Substring(0, 3) == "POR")
                {
                    hlink2.NavigateUrl = "~/F_15_Pro/PurMRREntry?Type=Entry&actcode=" + pactcode + "&genno=" + orderno + "&sircode=" + sircode;

                }
                else
                {
                    hlink2.NavigateUrl = "~/F_09_Commer/LcReceive?Type=Entry&comcod=" + comcod + "&actcode=" + orderno + "&centrid=&genno=";

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

        protected void grvProIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("HyInprPrint");
                Label reqno = (Label)e.Row.FindControl("lgpreqno1");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string reqtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqtype")).ToString();
                string reqdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "reqdate")).ToString("dd-MMM-yyyy");
                string Type = "FG"; //this.Request.QueryString["Type"].ToString();
                string procode = DataBinder.Eval(e.Row.DataItem, "procode").ToString();

                //if (this.GetCompCode() == "5305")
                //{
                //    hlink1.NavigateUrl = "~/F_11_RawInv/MaterialIssueSuplierWise?Type=Entry&genno=" + preqno + "&actcode=" + actcode + "&reptype=" + reqtype;

                //}
                //else
                //{
                string reqtest = (reqno.Text).ToString().Substring(0, 3);
                if (reqtest != "SDI")
                {
                    hlink1.NavigateUrl = "~/F_11_RawInv/PBMatIssueSingle?Type=Entry&genno=" + preqno + "&actcode=" + actcode + "&reptype=" + reqtype;

                }
                else
                {
                    hlink1.NavigateUrl = "~/F_11_RawInv/SamplingMatIssue?Type=Entry&genno=" + preqno + "&actcode=" + actcode + "&reptype=" + reqtype;

                }

                hlink2.NavigateUrl = "~/F_15_Pro/Print?Type=proreq&dprno=" + preqno + "&pbmno=" + actcode + "&tardate=" + "" + "&slnum=" + "" + "&plnno=" + "" + "&pbdate=" + reqdate;

                //hlink2.NavigateUrl = "~/F_15_Pro/Print?Type=reqapp&actcode=" + actcode + "&genno=" + preqno + "&process=" + procode + "&reqdate=" + reqdate + "&reqtype=" + reqtype;

                // }
                if (comcod != "5305" || comcod != "5306")
                {
                    ((TextBox)this.grvProIssue.HeaderRow.FindControl("gvfbordno")).Attributes.Add("placeholder", "Order No");
                    this.grvProIssue.Columns[7].Visible = true;
                }


            }
        }

        protected void gvPromData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("HypRDDoPrint");
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("BtnEdit");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string issueno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issueno")).ToString();
                string issuedat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "issuedat")).ToString("dd-MMM-yyyy");
                //string apstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "apstatus"));


                hlink3.NavigateUrl = "~/F_23_SaM/Print?Type=IssueChallan&comcod=" + comcod + "&issueno=" + issueno + "&issuedat=" + issuedat;

                hlink1.NavigateUrl = "~/F_11_RawInv/Material_Issue?Type=Approve&genno=" + issueno;
                hlink1.Target = "blank";


            }
        }

        protected void gvgstorec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string preqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "preqno")).ToString();


                hlink2.NavigateUrl = "~/F_15_Pro/ProdEntry?Type=Entry&actcode=" + actcode + "&genno=" + preqno;

            }
        }

        protected void gvFGDeliv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HPrint = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("HypDelivery");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string invno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "invno")).ToString();

                hlink2.NavigateUrl = "~/F_19_EXP/ExportMgt?Type=Approve&actcode=" + actcode + "&genno=" + invno;
                HPrint.NavigateUrl = "~/F_19_EXP/ExpPrint?Type=DelChallan&actcode=" + actcode + "&genno=" + invno;
                hlink3.NavigateUrl = "~/F_19_EXP/DelvChallan?Type=Entry&actcode=" + actcode + "&genno=" + invno + "&sircode=";

                hlink3.NavigateUrl = "~/F_19_EXP/DelvChallan?Type=Entry&actcode=" + actcode + "&genno=" + invno;

            }
        }

        protected void gvFGChln_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HPrint = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                string invno = "";// Convert.ToString(DataBinder.Eval(e.Row.DataItem, "invno")).ToString();
                string dchno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dchno")).ToString();
                hlink2.NavigateUrl = "~/F_19_EXP/DelvChallan?Type=Approve&actcode=" + actcode + "&genno=" + invno + "&sircode=" + dchno;
                HPrint.NavigateUrl = "~/F_19_EXP/ExpPrint?Type=DelChallan&actcode=" + actcode + "&genno=" + dchno;
                //hlink2.NavigateUrl = "~/F_19_EXP/DelvChallan.aspx?Type=Approve&actcode=" + actcode + "&genno=" + dchno;
                //HPrint.NavigateUrl = "~/F_19_EXP/ExpPrint.aspx?Type=DelChallan&actcode=" + actcode + "&genno=" + dchno;

            }
        }

        protected void btnSetup_Click(object sender, EventArgs e)
        {
            this.PnlSalesSetup.Visible = true;
            this.PnlInt.Visible = false;
            this.pnlReprots.Visible = true;
            this.plnMgf.Visible = false;
            //this.lblVal.Visible = false;

        }

        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = true;
            this.pnlReprots.Visible = false;
            //this.plnMgf.Visible = false;
            //this.lblVal.Visible = true;
            this.PnlSalesSetup.Visible = false;
        }

        protected void lnkRept_Click(object sender, EventArgs e)
        {
            this.PnlInt.Visible = false;
            this.pnlReprots.Visible = true;
            this.plnMgf.Visible = true;
            //this.lblVal.Visible = false;
            this.PnlSalesSetup.Visible = false;
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
            string Reqno = ASTUtility.Left(Code, 14).ToString(); // warehouse qc delete
            string pactcode = Code.Substring(14, 14).ToString();
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

        protected void btnDelexitsRecv_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            string Code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string order = ASTUtility.Right(Code, 12).ToString();

            string delType = (ASTUtility.Left(order, 2) == "14") ? "LC" : "Local";
            string Reqno, mrrno, pactcode = "";
            if ((ASTUtility.Left(order, 2) != "14"))
            {
                Reqno = ASTUtility.Left(Code, 12).ToString();
                mrrno = Code.Substring(12, 14).ToString();
                pactcode = ASTUtility.Right(Code, 14).ToString();
            }
            else
            {
                mrrno = Code.Substring(12, 15).ToString();
                Reqno = ASTUtility.Left(Code, 12).ToString();
                pactcode = order;

            }

            if (mrrno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');", true);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE_01", "GETMRRINFO", qcno);
            //this.XmlDataInsert(Reqno, qcno, ds);
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_PURCHASE_INTERFACE", "REVMATRECEIVE", Reqno, mrrno, delType, pactcode, "", "");

            if (true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('MR Delete Successfully');", true);
            }
            Common.LogStatus("Purchase Interface", "MR Delete", "MR No: ", Reqno + "-" + mrrno);
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
            string orderno = ((Label)gvAchList.Rows[index].FindControl("lblorderno")).Text.ToString();
            string reqno = ((Label)gvAchList.Rows[index].FindControl("lblreqno")).Text.ToString();
            string reqtype = (reqno.Trim().Length > 0) ? ((Label)gvAchList.Rows[index].FindControl("lblReqType")).Text.ToString() : "LC";
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_WAREHOUSE_INTERFACE", "PURCHASE_ORDER_AUTHENTICATION_APPROVAL", orderno, reqno, reqtype, "AUTH", userid, Terminal, Sessionid, Posteddat);

            if (result == true)
            {
                this.ImportInterFace();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Arrival Complete Successfully');", true);
            }
        }

        //protected void lbtnApprve_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string userid = hst["usrid"].ToString();
        //    string Terminal = hst["compname"].ToString();
        //    string Sessionid = hst["session"].ToString();
        //    string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
        //    string comcod = this.GetCompCode();
        //    int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        //    string orderno = ((Label)gvArrivList.Rows[index].FindControl("lblorderno")).Text.ToString();
        //    string reqno = ((Label)gvArrivList.Rows[index].FindControl("lblreqno")).Text.ToString();
        //    string reqtype = ((Label)gvArrivList.Rows[index].FindControl("lblReqType")).Text.ToString();
        //    bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_WAREHOUSE_INTERFACE", "PURCHASE_ORDER_AUTHENTICATION_APPROVAL", orderno, reqno, reqtype, "APP", userid, Terminal, Sessionid, Posteddat);

        //    if (result == true)
        //    {
        //        this.ImportInterFace();
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Approved Successfully');", true);
        //    }
        //}

        protected void LbtnArivUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string orderno = ((Label)this.gvArrivList.Rows[index].FindControl("lblorderno")).Text.ToString();


            DataSet result = accData.GetTransInfo(comcod, "SP_REPORT_WAREHOUSE_INTERFACE", "GETORDERLISTFORARRI", orderno);
            if (result == null)
            {
                return;
            }
            ViewState["tblAridata"] = result.Tables[0];
            this.gvArvDetails.DataSource = result.Tables[0];
            this.gvArvDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenArvmodal();", true);
        }
        protected void ModalUpdateBtn_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            bool result = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            for (int j = 0; j < gvArvDetails.Rows.Count; j++)
            {
                if (((CheckBox)this.gvArvDetails.Rows[j].FindControl("chkack")).Checked == true && ((CheckBox)this.gvArvDetails.Rows[j].FindControl("chkack")).Enabled == true)
                {
                    string lateappsta = (((CheckBox)this.gvArvDetails.Rows[j].FindControl("chkack")).Checked == true) ? "1" : "0";
                    string orderno = Convert.ToString(((Label)this.gvArvDetails.Rows[j].FindControl("lblgvorderno")).Text.Trim());
                    string rsircode = Convert.ToString(((Label)this.gvArvDetails.Rows[j].FindControl("lblrsircode")).Text.Trim());
                    string spcfcod = Convert.ToString(((Label)this.gvArvDetails.Rows[j].FindControl("lblspcfcod")).Text.Trim());
                    string oType = Convert.ToString(((Label)this.gvArvDetails.Rows[j].FindControl("lblgp")).Text.Trim());


                    userid = (lateappsta == "1") ? userid : "";
                    Terminal = (lateappsta == "1") ? Terminal : "";
                    Sessionid = (lateappsta == "1") ? Sessionid : "";
                    Posteddat = (lateappsta == "1") ? Posteddat : "01-Jan-1900";



                    result = accData.UpdateTransInfo(comcod, "SP_REPORT_WAREHOUSE_INTERFACE", "UPDATEPOARRIVAL", oType, orderno, rsircode, spcfcod, userid, Terminal, Sessionid, Posteddat);
                }

                if (!result)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";

                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";



            }

        }

        protected void lnkbtngviclpoModal_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string porno = linkButton.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)linkButton.NamingContainer;
            string oldstoreid = ((Label)row.FindControl("lblpactcode")).Text;
            string re = ASTUtility.Left(porno, 3);
            if (ASTUtility.Left(porno, 3) != "POR")
                return;

            this.lblHelper.Text = porno;
            string comcod = this.GetCompCode();
            DataSet ds5 = accData.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "RETRIVELCSTORE1", "actcode like '15%'", "", "", "",
                   "", "", "", "", "");
            this.ddlStorid.DataTextField = "actdesc1";
            this.ddlStorid.DataValueField = "actcode";
            this.ddlStorid.DataSource = ds5.Tables[0];
            this.ddlStorid.DataBind();
            //this.ddlStorid.SelectedValue = oldstoreid;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenStoreModal();", true);
        }

        protected void lnkbtnUpdateStore_Click(object sender, EventArgs e)
        {
            string porno = this.lblHelper.Text.Trim();
            string comcod = this.GetCompCode();
            string storeid = this.ddlStorid.SelectedValue.ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_WAREHOUSE_INTERFACE", "CHANGE_STORE_IN_REQUISITION", porno, storeid);

            if (result == true)
            {
                string eventtype = "STORE CHANGE";
                string eventdesc = "Store change for this PO All Req No" + porno + ", New Store Id: " + storeid;
                string eventdesc2 = "Order No- " + porno;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                this.ImportInterFace();
                this.RadioButtonList1_SelectedIndexChanged(null, null);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Faild to update');", true);
            }
        }

        protected void lnkbtnPrintCombined_Click(object sender, EventArgs e)
        {

            string order = "";
            string reqno = "";
            string reqdat = "";

            for (int i = 0; i < this.grvProIssue.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.grvProIssue.Rows[i].FindControl("chkPrintCombined");

                if (chkbox.Checked)
                {
                    string mlccod = ((Label)this.grvProIssue.Rows[i].FindControl("lblgvcentrid")).Text;
                    string dayid = ((Label)this.grvProIssue.Rows[i].FindControl("lgrvDayId")).Text;
                    string order2 = mlccod + dayid;
                    reqdat = ((Label)this.grvProIssue.Rows[i].FindControl("lgpreqdate")).Text;

                    if (order != order2)
                    {
                        order += order2;
                    }

                    if (order.Length > 20)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have selected more than 1 order');", true);
                        return;
                    }

                    reqno += ((Label)this.grvProIssue.Rows[i].FindControl("lblgvpreqno")).Text;

                }
            }

            string printFormat = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue;
            string url = "";

            if (reqno.Contains("SDI"))
            {
                url = ResolveClientUrl("~//F_15_Pro/Print.aspx?Type=SamProdReqMulti&genno=" + reqno + "&printfrmt=" + printFormat);
            }
            else
            {
                url = ResolveClientUrl("~//F_15_Pro/Print.aspx?Type=PrintReqMulti&mlccod=" + order + "&reqno=" + reqno + "&pbdate=" + reqdat + "&printfrmt=" + printFormat);
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);

        }

        protected void LbtnIssueMulti_Click(object sender, EventArgs e)
        {
            string order = "";
            string reqno = "";

            for (int i = 0; i < this.grvProIssue.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.grvProIssue.Rows[i].FindControl("chkPrintCombined");

                if (chkbox.Checked)
                {
                    string mlccod = ((Label)this.grvProIssue.Rows[i].FindControl("lblgvcentrid")).Text;
                    string dayid = ((Label)this.grvProIssue.Rows[i].FindControl("lgrvDayId")).Text;
                    string order2 = mlccod + dayid;

                    if (order != order2)
                    {
                        order += order2;
                    }

                    if (order.Length > 20)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have selected more than 1 order');", true);
                        return;
                    }

                    reqno += ((Label)this.grvProIssue.Rows[i].FindControl("lblgvpreqno")).Text;

                }
            }

            string url = "";
            
            if(reqno.Contains("SDI") && !reqno.Contains("DPR"))
            {
                url = ResolveClientUrl("~/F_11_RawInv/SamplingMatIssue?Type=Entry&genno=" + reqno + "&actcode=" + order.Substring(0, 12) + "&reptype=SAMPLING");
            }
            else
            {
                url = ResolveClientUrl("~/F_11_RawInv/PBMatIssueSingle?Type=Entry&genno=" + reqno + "&actcode=" + order.Substring(0, 12) + "&reptype=NORMAL");
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);
        }

        protected void lnkCheckAll_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetCompCode();
            bool result = false;
            for (int i = 0; i < this.gvAchList.Rows.Count; i++)
            {
                string orderno = ((Label)gvAchList.Rows[i].FindControl("lblorderno")).Text.ToString();
                string reqno = ((Label)gvAchList.Rows[i].FindControl("lblreqno")).Text.ToString();
                string reqtype = (reqno.Trim().Length > 0) ? ((Label)gvAchList.Rows[i].FindControl("lblReqType")).Text.ToString() : "LC";
                 result = accData.UpdateTransInfo(comcod, "SP_REPORT_WAREHOUSE_INTERFACE", "PURCHASE_ORDER_AUTHENTICATION_APPROVAL", orderno, reqno, reqtype, "AUTH", userid, Terminal, Sessionid, Posteddat);

            }

            if (result == true)
            {
                this.ImportInterFace();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Arrival Complete Successfully');", true);
            }
        }

        protected void LbtnFGMulti_Click(object sender, EventArgs e)
        {
            string order = "";
            string preqno = "";

            for (int i = 0; i < this.gvgstorec.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.gvgstorec.Rows[i].FindControl("chkPrintCombinedFG");

                if (chkbox.Checked)
                {
                    string mlccod = ((Label)this.gvgstorec.Rows[i].FindControl("lblgvwipid")).Text;
                    string odayid = ((Label)this.gvgstorec.Rows[i].FindControl("lgrvoDayId")).Text;
                    string order2 = mlccod + odayid;

                    if (order != order2)
                    {
                        order += order2;
                    }

                    if (order.Length > 20)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have selected more than 1 order');", true);
                        return;
                    }

                    preqno += ((Label)this.gvgstorec.Rows[i].FindControl("lblprqno")).Text;

                }
            }

            string url = ResolveClientUrl("~/F_15_Pro/ProdEntry?Type=Entry&actcode=" + order.Substring(0, 12) + "&genno=" + preqno);

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);
        }

        //protected void lnkBtnReqPrint_Click(object sender, EventArgs e)
        //{

        //}

        //protected void lnkbtnCstmPO_Click(object sender, EventArgs e)
        //{
        //    GridViewRow gridViewRow = ((LinkButton)sender).NamingContainer as GridViewRow;
        //    string customPO = ((LinkButton)gridViewRow.FindControl("lnkbtnCstmPO")).Text.Trim();

        //    string[] arrCustomPO = customPO.Split(',');

        //    foreach (string custpo in arrCustomPO)
        //    {
        //        string po = custpo.Trim();
        //    }
        //}

        //protected void lnkbtnReqNo_Click(object sender, EventArgs e)
        //{

        //}

        //protected void gvArrivList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    string comcod = this.GetCompCode();

        //    for (int i = 0; i < gvArrivList.Rows.Count; i++)
        //    {
        //        string reqno = ((Label)(gvArrivList.Rows[i].FindControl("galLblReqNo"))).Text;
        //        string[] requsitions = reqno.Split(',');
        //        string po = ((Label)(gvArrivList.Rows[i].FindControl("galLblPo"))).Text;

        //        ((HyperLink)(gvArrivList.Rows[i].FindControl("HyReqPrint"))).NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=Local&AppType=YES";
        //        ((HyperLink)(gvArrivList.Rows[i].FindControl("HypPOPrint"))).NavigateUrl = "~/F_10_Procur/PuchasePrint?Type=OrderPrint&comcod=" + comcod + "&orderno=" + po;
        //    }
        //}
    }
}