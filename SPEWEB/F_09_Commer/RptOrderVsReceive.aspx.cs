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
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_22_Sal;
using System.Web.Services;

namespace SPEWEB.F_09_Commer
{
    public partial class RptOrderVsReceive : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "OrderVsRec") ? "BOM Vs Received"
                                                                                            : (Type == "BomMatSummary") ? "BOM Wise Material Summary"
                                                                                            : (Type == "BuyerWiseSamp") ? "Buyer Wise Article List"
                                                                                            : (Type == "SesonWiseBom") ? "Season Wise BOM Status"
                                                                                            : "Materials Order Vs Received";


                this.txtFDate.Text = DateTime.Today.AddDays(-15).ToString("dd-MMM-yyyy");
                this.txtdate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCustomer();
                this.SelectView();
                GetSupplierName();
                GetStore();

                this.CurrencyInf();
                GetPurType();
                GetDeparment();
                this.GetSesson();

                this.GetCodeBookList();
            }
        }


        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");


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


            this.DdlSeason_SelectedIndexChanged(null, null);

        }

        protected void GetCodeBookList()
        {
            try
            {
                string Querytype = this.Request.QueryString["Type"];
                string coderange = "04%";
                string comcod = GetCompCode();
                DataSet dsone = purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                Session["tblmatsubhead"] = dsone.Tables[1];
                this.ddlCodeBook.DataTextField = "sirdesc";
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
            this.ddlGroup.DataTextField = "sirdesc";
            this.ddlGroup.DataValueField = "sircode1";
            this.ddlGroup.DataSource = dv.ToTable();
            this.ddlGroup.DataBind();


            //if (mathead != "0400%")
            //{
            //    this.ddlGroup.Items.Add(new ListItem { Value = mathead, Text = "All", Selected = true });
            //}
        }


        private void GetPurType()
        {
            string comcod = this.GetCompCode();

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURTYPE", "");
            if (ds == null)
                return;



            ddlPurType.DataSource = ds.Tables[0];
            ddlPurType.DataTextField = "gdesc";
            ddlPurType.DataValueField = "gcod";
            ddlPurType.DataBind();

            this.ddlPurType.SelectedItem.Text = "----Select----";

            //if (comcod == "5305")
            //{
            //    this.ddlPurType.SelectedValue = "25001";
            //}

        }


        private void SelectView()
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "Req":

                    this.lblplntype.Visible = false;
                    this.lblplnSeason.Visible = false;
                    this.RadioButtonList1.SelectedIndex = 0;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.divddlCodeBook.Visible = false;
                    this.pnlSubGroup.Visible = false;
                    this.divddlSummary.Visible = false;
                    this.CheckPObal.Visible = false;
                    break;

                case "OrderVsRec":
                    this.lblType.InnerText = "BOM";
                    this.ReqBtn.Visible = true;
                    //this.ReqBalance.Visible = true;
                    this.GetBOMList();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "BomMatSummary":
                    this.lblType.InnerText = "BOM";
                    this.divfDate.Visible = false;
                    this.divtDate.Visible = false;
                    this.divddlSummary.Visible = false;
                    this.CheckPObal.Visible = false;
                    this.divddlCodeBook.Visible = true;
                    //this.lblSubGroup.Visible = false;
                    //this.ddlGroup.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "SesonWiseBom":

                    this.divfDate.Visible = false;
                    this.pnlBuyer.Visible = false;
                    this.divddlCodeBook.Visible = false;
                    this.pnlSubGroup.Visible = false;
                    this.divtDate.Visible = false;
                    this.divddlSummary.Visible = false;
                    this.CheckPObal.Visible = false;
                    this.pnlDays.Visible = true;

                    this.MultiView1.ActiveViewIndex = 3;
                    break;


            }

        }
        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");
            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds2.Tables[0];
            this.ddlBuyer.DataBind();


        }

        private void GetStoreList()
        {
            string comcod = this.GetCompCode();
            string HeaderCode = "15%";
            string filter = "%";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", HeaderCode, filter, "", "", "", "", "", "", "");

            this.ddlBuyer.DataTextField = "actdesc1";
            this.ddlBuyer.DataValueField = "actcode";
            this.ddlBuyer.DataSource = ds1.Tables[0];
            this.ddlBuyer.DataBind();
        }

        private void GetCustomer()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");
            this.DdlCustomer.DataTextField = "sirdesc";
            this.DdlCustomer.DataValueField = "sircode";
            this.DdlCustomer.DataSource = ds2.Tables[0];
            this.DdlCustomer.DataBind();
            this.DdlCustomer.SelectedValue = "000000000000";

            this.DdlCustomer2.DataTextField = "sirdesc";
            this.DdlCustomer2.DataValueField = "sircode";
            this.DdlCustomer2.DataSource = ds2.Tables[0];
            this.DdlCustomer2.DataBind();

        }
        protected void GetOrderName()
        {
            string comcod = this.GetCompCode();
            string FindOrder = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GETBOMLIST", FindOrder, "", "", "", "", "", "", "", "");

            DataView view = new DataView(ds1.Tables[0]);
            DataTable distinctValues = view.ToTable(true, "mlccod", "mlcdesc", "dayid");

            if (ds1 == null)
                return;
            this.ddlBuyer.DataTextField = "mlcdesc";
            this.ddlBuyer.DataValueField = "mlccod";
            this.ddlBuyer.DataSource = distinctValues;
            this.ddlBuyer.DataBind();
            ds1.Dispose();
            // this.ddlOrder_SelectedIndexChanged(null, null);
        }
        protected void GetBOMList()
        {
            string comcod = this.GetCompCode();
            string FindOrder = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string customer = (this.DdlCustomer.SelectedValue.ToString() == "000000000000") ? "%" : this.DdlCustomer.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GETBOMLIST", FindOrder, customer, "", "", "", "", "", "", "");

            ViewState["BomList"] = ds1;

            if (ds1 == null)
                return;
            this.ddlBuyer.DataTextField = "bomdesc";
            this.ddlBuyer.DataValueField = "bomid";
            this.ddlBuyer.DataSource = ds1.Tables[0];
            this.ddlBuyer.DataBind();
            ds1.Dispose();
            // this.ddlOrder_SelectedIndexChanged(null, null);
            // this.ddlOrder_SelectedIndexChanged(null, null);week
            DdlCustomer2.ClearSelection();
        }
        protected void Load_CodeBooList()
        {

            try
            {
                string coderange = "04%";
                string comcod = this.GetCompCode();
                DataSet dsone = this.purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                this.ddlBuyer.DataTextField = "sircode";
                this.ddlBuyer.DataValueField = "sircode1";
                this.ddlBuyer.DataSource = dsone.Tables[1];
                this.ddlBuyer.DataBind();
            }
            catch (Exception ex)
            {
                //this.ConfirmMessage.Text = "Error:" + ex.Message;
            }
        }

        protected void Load_ReqTypeList()
        {

            try
            {

                var dataTablesummary = new DataTable();
                dataTablesummary.Columns.Add("type", typeof(string));
                dataTablesummary.Columns.Add("typedesc", typeof(string));
                dataTablesummary.Rows.Add("Local", "LO");
                dataTablesummary.Rows.Add("LC", "LC");

                //dataTablesummary.Rows.Add("LO", "Local");
                //dataTablesummary.Rows.Add("LC", "LC");

                this.ddlBuyer.DataTextField = "type";
                this.ddlBuyer.DataValueField = "typedesc";
                this.ddlBuyer.DataSource = dataTablesummary;
                this.ddlBuyer.DataBind();

            }
            catch (Exception ex)
            {
                //this.ConfirmMessage.Text = "Error:" + ex.Message;
            }
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();

            switch (value)
            {
                case "0": ///Date Wise
                    this.lblplntype.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1": ///Bom wise
                    this.lblplntype.Visible = true;
                    this.lblType.InnerText = "BOM No";
                    this.GetBOMList();
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2": ///Order wise
                    this.lblplntype.Visible = true;
                    this.lblType.InnerText = "Order No";
                    this.GetOrderName();
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "3": ///Buyer Wise
                    this.lblplntype.Visible = true;
                    this.lblType.InnerText = "Buyer";
                    this.GetBuyer();
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;
                case "4": ///Req type wise
                    this.Load_ReqTypeList();
                    this.lblplntype.Visible = true;
                    this.lblType.InnerText = "Req. Type";
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    break;
                case "5": ///Material Type
                    this.Load_CodeBooList();
                    this.lblplntype.Visible = true;
                    this.lblType.InnerText = "Material Type";
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;
                case "6": ///Strore Type
                    this.lblplntype.Visible = true;
                    this.lblType.InnerText = "Store";
                    this.GetStoreList();
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    break;




            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "Req":
                    this.ShowReqVsRecStatus();
                    break;
                case "OrderVsRec":
                    this.ShowBOMVsRecStatus();
                    break;

                case "BomMatSummary":
                    this.GetBomWiseMatSummary();

                    break;

                case "SesonWiseBom":
                    this.GetSesonWiseBomInfo();
                    break;

            }
        }

        public class Bomlist
        {
            public string bomid { get; set; }
            public Bomlist()
            {
            }
            public Bomlist(string bomid)
            {
                this.bomid = bomid;
            }
        }


        private void GetSesonWiseBomInfo()
        {
            string comcod = this.GetCompCode();
            string season = this.DdlSeason.SelectedItem.Value;
            string day = this.ddlDays.SelectedItem.Value;
            string customer = this.DdlCustomer.SelectedItem.Value == "000000000000" ? "%" : this.DdlCustomer.SelectedItem.Value + "%";

            DataSet ds1 = purData.GetTransInfoNew(comcod, "SP_REPORT_PURCHASE_INTERFACE", "SEASON_WISE_BOM_PURCHASE_STATUS", null, null, null, season, day, customer, "");

            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ViewState["tblSesonWiseBom"] = ds1.Tables[0];
                    this.Data_Bind();
                    return;
                }
            }

            ViewState["tblSesonWiseBom"] = null;
            this.Data_Bind();

        }


        private void GetBomWiseMatSummary()
        {
            string comcod = this.GetCompCode();
            string matGorup = (this.ddlCodeBook.SelectedValue == "040000000000") ? "04%" : this.ddlCodeBook.SelectedValue.Substring(0, 4) + "%";
            string season = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            DataSet ds = new DataSet("ds");
            DataTable dt = new DataTable();
            ds.Merge(dt);

            ds.Tables[0].Columns.Add("bomid", typeof(string));

            ds.Tables[0].TableName = "tbl1";

            int index = 0;

            string bomlist = "";

            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    DataRow dr = ds.Tables["tbl1"].NewRow();
                    dr["bomid"] = item.Value;
                    ds.Tables["tbl1"].Rows.Add(dr);

                    bomlist += item.Value;
                }
                index++;
            }

            string matcode = "";

            foreach (ListItem item in ddlGroup.Items)
            {
                if (item.Selected)
                {
                    matcode += item.Value;
                }
            }

            var temp = ds.GetXml();
            DataSet ds1 = purData.GetTransInfoNew(comcod, "SP_REPORT_ORDER_STATUS", "GET_BOM_WISE_MATERIAL_SUMMARY", ds, null, null, matGorup, matcode, season);
            string a = ds.GetXml();
            if (ds1 == null || ds1.Tables[0].Rows.Count <= 0)
            {
                Session["tblBomWiseMat"] = null;
                this.gvBomWise.DataSource = null;
                this.gvBomWise.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data not found');", true);
                return;
            }
            Session["tblBomWiseMat"] = ds1.Tables[0];

            this.Data_Bind();


            DataSet ds2 = purData.GetTransInfoNew(comcod, "SP_REPORT_ORDER_STATUS", "BOM_WISE_PRODUCT_FG_STATUS", ds, null, null, "");

            if (ds2 != null)
            {
                this.lblFGQty.Visible = true;
                this.lblFGQty.Text = "Total Order Qty : " + Convert.ToDouble(ds2.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");
            }
        }

        private void FooterCalculation()
        {
            DataTable dtFT = (DataTable)Session["tblBomWiseMat"];

            if (dtFT.Rows.Count > 0)
            {
                double TtlBomQty = Convert.ToDouble(dtFT.Compute("SUM(itmqty)", string.Empty));
                double TtlStkQty = Convert.ToDouble(dtFT.Compute("SUM(stockqty)", string.Empty));
                double TtlOrdQty = Convert.ToDouble(dtFT.Compute("SUM(ordqty)", string.Empty));
                double TtlProjOrd = Convert.ToDouble(dtFT.Compute("SUM(prjectionord)", string.Empty));
                double TtlOrd = Convert.ToDouble(dtFT.Compute("SUM(ttlorder)", string.Empty));
                double TtlShpQty = Convert.ToDouble(dtFT.Compute("SUM(shipqty)", string.Empty));
                double TtlSBalQty = Convert.ToDouble(dtFT.Compute("SUM(shipbalqty)", string.Empty));
                double TtlRcvQty = Convert.ToDouble(dtFT.Compute("SUM(rcvqty)", string.Empty));
                double TtlIssQty = Convert.ToDouble(dtFT.Compute("SUM(isuqty)", string.Empty));
                double TtlBomRcv = Convert.ToDouble(dtFT.Compute("SUM(bombalrcv)", string.Empty));
                double TtlBomPO = Convert.ToDouble(dtFT.Compute("SUM(bombalpo)", string.Empty));
                double TtlRcvBal = Convert.ToDouble(dtFT.Compute("SUM(bombalissue)", string.Empty));

                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlBomQty"))).Text = TtlBomQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlStkQty"))).Text = TtlStkQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlOrdQty"))).Text = TtlOrdQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlProjOrd"))).Text = TtlProjOrd.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlOrd"))).Text = TtlOrd.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlShpQty"))).Text = TtlShpQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlSBalQty"))).Text = TtlSBalQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlRcvQty"))).Text = TtlRcvQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlIssQty"))).Text = TtlIssQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlBomRcv"))).Text = TtlBomRcv.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlBomPO"))).Text = TtlBomPO.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvBomWise.FooterRow.FindControl("gvTtlRcvBal"))).Text = TtlRcvBal.ToString("#,##0.00;(#,##0.00); ");
            }
        }

        private void ShowReqVsRecStatus()
        {
            string comcod = this.GetCompCode();
            string Fdate = this.txtFDate.Text;
            string Tdate = this.txtdate.Text;
            string DescType = "";
            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    DescType += item.Value;
                }
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "RPTORDVSRECEIVED", Fdate, Tdate, DescType, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblOrderRcv"] = ds1.Tables[0];  //HiddenSameData(ds1.Tables[0]);//
            this.Data_Bind();
        }

        private void GetSupplierName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MARKETSERVEY", "GETPSNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlSupplier.DataTextField = "sirdesc";
            this.ddlSupplier.DataValueField = "sircode";
            this.ddlSupplier.DataSource = ds1.Tables[0];
            this.ddlSupplier.DataBind();

        }


        private void CurrencyInf()
        {
            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;
            this.ddlCurrency.DataValueField = "curcode";
            this.ddlCurrency.DataTextField = "curdesc";
            this.ddlCurrency.DataSource = lstCurryDesc;
            this.ddlCurrency.DataBind();

            if (this.Request.QueryString["InputType"] == "FxtAstEntry")
            {
                this.ddlCurrency.SelectedValue = "001";
                this.ddlCurrency.Enabled = false;
            }



            this.ddlCurrency_SelectedIndexChanged(null, null);
        }
        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fcode = "001";
            string tcode = this.ddlCurrency.SelectedValue.ToString();
            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;


            this.lblConRate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
            //double txtpeople = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBudgeted.Rows[j].FindControl("txtpeople")).Text.Trim()));
            //  ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ShowModal();", true);


        }

        [WebMethod(EnableSession = false)]
        public static string GetCurRate(string curcode)
        {
            try
            {
                string fcode = "001";
                string tcode = curcode;
                SalesInvoice_BL lst1 = new SalesInvoice_BL();
                DataSet ds = lst1.Curreny();
                var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();


                double method = lstConv.FindAll(p => p.fcode == fcode && p.tcode == tcode).ToList()[0].conrate;


                // this.lblConRate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");

                return Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        protected void GetStore()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string fxtast = "FxtAst";
            string Aproval = "";
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string ReFindProject = "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", ReFindProject, fxtast, Aproval, userid, "", "", "", "", "");
            if (ds2 == null)
                return;

            this.DDlStore.DataTextField = "actdesc1";
            this.DDlStore.DataValueField = "actcode";
            this.DDlStore.DataSource = ds2.Tables[0];
            this.DDlStore.DataBind();
            ViewState["tblStoreType"] = ds2.Tables[0];


        }

        private void ShowBOMVsRecStatus()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ResetSelectedItems();", true);
            string comcod = this.GetCompCode();
            string Fdate = this.txtFDate.Text;
            string Tdate = this.txtdate.Text;
            DataSet ds = new DataSet("ds");
            DataTable dt = new DataTable();
            ds.Merge(dt);

            ds.Tables[0].Columns.Add("bomid", typeof(string));

            ds.Tables[0].TableName = "tbl1";

            int index = 0;
            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    DataRow dr = ds.Tables["tbl1"].NewRow();
                    dr["bomid"] = item.Value;
                    ds.Tables["tbl1"].Rows.Add(dr);
                }
                index++;
            }
            //string matcode = (this.ddlGroup.SelectedValue.ToString().Substring(2,10)=="0000000000")?"%": this.ddlGroup.SelectedValue.ToString().Substring(0,7)+"%";
            string matcode = "";

            foreach (ListItem item in ddlGroup.Items)
            {
                if (item.Selected)
                {
                    matcode += item.Value;
                }
            }
            string a = ds.GetXml();
            string season = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            DataSet ds1 = purData.GetTransInfoNew(comcod, "SP_REPORT_ORDER_STATUS", "RPTBOMVSRECEIVED", ds, null, null, Fdate, Tdate, "", matcode, season, "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblOrderRcv"] = ds1.Tables[0];
            this.ddlSummary_SelectedIndexChanged(null, null);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string bblccode = dt1.Rows[0]["bblccode"].ToString();
            string ssircode = dt1.Rows[0]["ssircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["bblccode"].ToString() == bblccode)
                {
                    bblccode = dt1.Rows[j]["bblccode"].ToString();
                    dt1.Rows[j]["bblcdesc"] = "";
                }

                else
                {
                    bblccode = dt1.Rows[j]["bblccode"].ToString();
                }
                if (dt1.Rows[j]["ssircode"].ToString() == ssircode)
                {
                    ssircode = dt1.Rows[j]["ssircode"].ToString();
                    dt1.Rows[j]["supplier"] = "";
                }

                else
                {
                    ssircode = dt1.Rows[j]["ssircode"].ToString();
                }

            }
            return dt1;

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblOrderRcv"];

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "Req":
                    if (dt == null) return;
                    this.gvOrderStatus.Columns[1].Visible = false;
                    this.gvOrderStatus.Columns[12].Visible = false;
                    this.gvOrderStatus.Columns[13].Visible = false;

                    this.gvOrderStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvOrderStatus.DataSource = dt;
                    this.gvOrderStatus.DataBind();

                    string value = this.RadioButtonList1.SelectedValue.ToString();

                    switch (value)
                    {
                        case "2":
                            this.gvOrderStatus.Columns[1].Visible = true;
                            break;
                        case "3":
                            this.gvOrderStatus.Columns[12].Visible = true;
                            break;
                        case "4":
                            this.gvOrderStatus.Columns[11].Visible = true;
                            break;

                    }
                    ((Label)this.gvOrderStatus.FooterRow.FindControl("Flgvreqqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reqqty)", "")) ?
                            0.00 : dt.Compute("Sum(reqqty)", ""))).ToString("#,##0;-#,##0; ");
                    ((Label)this.gvOrderStatus.FooterRow.FindControl("Flgvrecqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(mrrqty)", "")) ?
                            0.00 : dt.Compute("Sum(mrrqty)", ""))).ToString("#,##0;-#,##0; ");
                    ((Label)this.gvOrderStatus.FooterRow.FindControl("Flgvremqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(remainqty)", "")) ?
                            0.00 : dt.Compute("Sum(remainqty)", ""))).ToString("#,##0;-#,##0; ");
                    break;

                case "OrderVsRec":
                    if (dt == null) return;
                    if (ddlSummary.SelectedIndex == 0)
                    {
                        if (this.CheckReqbal.Checked)
                        {
                            DataView dv = dt.Copy().DefaultView;
                            dv.RowFilter = "bomqty>reqqty";
                            dt = dv.ToTable();

                        }

                        if (this.CheckPObal.Checked)
                        {
                            DataView dv = dt.Copy().DefaultView;
                            //dv.RowFilter = "bomqty>reqqty";
                            dv.RowFilter = "reqqty-purordqty>0";
                            dt = dv.ToTable();
                        }

                        gvBMvRev.Columns[2].Visible = true;
                        gvBMvRev.Columns[3].Visible = true;
                        gvBMvRev.Columns[15].Visible = true;
                        this.gvBMvRev.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvBMvRev.DataSource = dt;
                        this.gvBMvRev.DataBind();

                        if (dt.Rows.Count > 0)
                        {
                            ((Label)this.gvBMvRev.FooterRow.FindControl("Flgvbomqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bomqty)", "")) ?
                                    0.00 : dt.Compute("Sum(bomqty)", ""))).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvBMvRev.FooterRow.FindControl("FlgvPoqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(purordqty)", "")) ?
                                    0.00 : dt.Compute("Sum(purordqty)", ""))).ToString("#,##0;-#,##0; ");

                            ((Label)this.gvBMvRev.FooterRow.FindControl("FlgvOrdreqqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reqqty)", "")) ?
                                    0.00 : dt.Compute("Sum(reqqty)", ""))).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvBMvRev.FooterRow.FindControl("FlgvOrdrecqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(mrrqty)", "")) ?
                                    0.00 : dt.Compute("Sum(mrrqty)", ""))).ToString("#,##0;-#,##0; ");
                            ((Label)this.gvBMvRev.FooterRow.FindControl("FlgvOrdremqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(remainqty)", "")) ?
                                    0.00 : dt.Compute("Sum(remainqty)", ""))).ToString("#,##0;-#,##0; ");
                        }
                    }
                    else
                    {
                        DataTable summaryDt = (DataTable)ViewState["tblSummaryBOMVsPO"];

                        gvBMvRev.Columns[2].Visible = false;
                        gvBMvRev.Columns[3].Visible = false;
                        gvBMvRev.Columns[15].Visible = false;

                        this.gvBMvRev.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvBMvRev.DataSource = summaryDt;
                        this.gvBMvRev.DataBind();
                    }

                    break;

                case "BomMatSummary":
                    this.gvBomWise.DataSource = (DataTable)Session["tblBomWiseMat"];
                    this.gvBomWise.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBomWise.DataBind();
                    this.FooterCalculation();
                    break;

                case "SesonWiseBom":

                    DataTable dtSesonWiseBom = (DataTable)ViewState["tblSesonWiseBom"];

                    if (dtSesonWiseBom == null)
                    {
                        this.gvSesonWiseBom.DataSource = null;
                        this.gvSesonWiseBom.DataBind();

                        return;
                    }
                    this.gvSesonWiseBom.Columns[6].HeaderText = "PO (%) within " + this.ddlDays.SelectedValue.ToString() + " Days";
                    this.gvSesonWiseBom.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSesonWiseBom.DataSource = dtSesonWiseBom;
                    this.gvSesonWiseBom.DataBind();
                    break;

            }

            //DataTable dttotal = (DataTable)Session["tblOrderRcv"];
            //((Label)this.gvOrderStatus.FooterRow.FindControl("lgvfamt")).Text = Convert.ToDouble((Convert.IsDBNull(dttotal.Compute("Sum(ordram)", "")) ?
            //0.00 : dttotal.Compute("Sum(ordram)", ""))).ToString("#,##0;-#,##0; ");


        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }


        protected void gvOrderStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrderStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void gvBMvRev_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBMvRev.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "Req":
                    this.ReqVSRecPrint();
                    break;
                case "OrderVsRec":
                    this.BomVsRecPrint();
                    break;
                case "BomMatSummary":
                    this.BomWiseMatPrint();
                    break;
            }

        }

        private void BomWiseMatPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblBomWiseMat"];

            var list = dt.DataTableToList<SPEENTITY.C_09_Commer.BomWiseMatSummary>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptBomWiseMatSummary", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("rptTitle", "BOM Wise Material Summary"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void ReqVSRecPrint()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Fdate = this.txtFDate.Text;
            string Tdate = this.txtdate.Text;
            string DescType = "";

            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    DescType += item.Value;
                }
            }
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToString((Fdate) + " To " + (Tdate));

            DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "RPTORDVSRECEIVED", Fdate, Tdate, DescType, "", "", "", "", "", "");
            var list = ds.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.BO_Meterails>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptRequisitionVsReceived", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Materials Report"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void BomVsRecPrint()
        {

            string summarytype = ddlSummary.SelectedItem.Value.Trim();
            DataTable dt = new DataTable();
            if (summarytype == "0")
            {
                dt = (DataTable)ViewState["tblOrderRcv"];
            }
            else
            {
                dt = (DataTable)ViewState["tblSummaryBOMVsPO"];
            }

            if (dt == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No data in table');", true);
                return;
            }

            var list = dt.DataTableToList<SPEENTITY.C_09_Commer.BOMvsReceidved>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Fdate = this.txtFDate.Text;
            string Tdate = this.txtdate.Text;
            string date = Convert.ToString((Fdate) + " To " + (Tdate));
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            LocalReport rpt1 = new LocalReport();

            if (summarytype == "2")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptBOMVsReceivedSpecification", list, null, null);
            }
            else if (summarytype == "1")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptBOMVsReceivedMaterials", list, null, null);
            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptBOMVsReceived", list, null, null);
            }

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("rptTitle", "BOM Vs PO"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("summarytype", summarytype));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void chkheadl_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvBMvRev.Rows.Count; i++)
            {
                if (((CheckBox)this.gvBMvRev.HeaderRow.FindControl("chkhead")).Checked)
                {
                    ((CheckBox)this.gvBMvRev.Rows[i].FindControl("chkCol")).Checked = true;
                }
                else
                {
                    ((CheckBox)this.gvBMvRev.Rows[i].FindControl("chkCol")).Checked = false;
                }
            }

        }
        protected void GetDeparment()
        {
            string comcod = this.GetCompCode();
            //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETDEPARTMENT", "%%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].Rows.Add(comcod, "000000000000", "Department");
            ds1.Tables[0].Rows.Add(comcod, "AAAAAAAAAAAA", "-------Select-----------");


            this.ddlDeptCode.DataTextField = "fxtgdesc";
            this.ddlDeptCode.DataValueField = "fxtgcod";
            this.ddlDeptCode.DataSource = ds1.Tables[0];
            this.ddlDeptCode.DataBind();
            this.ddlDeptCode.SelectedValue = "AAAAAAAAAAAA";


        }

        protected string GetReqNo()
        {
            string comcod = this.GetCompCode();
            string mREQNO = "NEWREQ";


            string mREQDAT = this.GetStdDate(System.DateTime.Today.ToString("dd-MM-yyyy"));
            if (mREQNO == "NEWREQ")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTREQINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                {
                    return mREQNO;
                }
                    ;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    return ds2.Tables[0].Rows[0]["maxreqno"].ToString();
                }

            }
            return mREQNO;

        }
        protected void LbtnCreateReq_Click(object sender, EventArgs e)
        {
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

                return;
            }



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();




            string mMRFNO = this.TxtMrfno.Text.Trim();

            string mREQDAT = this.GetStdDate(System.DateTime.Today.ToString("dd-MM-yyyy"));
            string mREQNO = GetReqNo();

            //Log Entry


            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;
            string PostedDat = Date;
            // FxtAstChecked




            string ChkByid = "";
            string Chkdat = "01-Jan-1900";
            string chked = "";

            string RevByid = "";
            string CRevdat = "01-Jan-1900";
            string revTrmid = "";
            string revSeson = "";

            // FxtAstApproval

            string ApprovByid = "";
            string approvdat = "01-Jan-1900";
            string Approvtrmid = "";
            string ApprovSession = "";


            string approved = "";

            //////


            string mPACTCODE = this.DDlStore.SelectedValue.ToString().Trim();
            //string mFLRCOD = this.ddlFloor.SelectedValue.ToString().Trim();
            string mREQUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(System.DateTime.Today.ToString("dd-MM-yyyy"));  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mEDDAT = this.GetStdDate(System.DateTime.Today.ToString("dd-MM-yyyy")); // DateTime.Today.ToString("dd-MMM-yyyy");
            string mREQBYDES = "";
            string mAPPBYDES = "";
            string Deptcode = this.ddlDeptCode.SelectedValue.ToString();
            string Deptcode2 = "000000000000";// this.ddlDeptCode2.SelectedValue.ToString();

            //string orderno = this.ddlOrder.SelectedValue.ToString();

            string mslccod = "";// (orderno.Length==0)?"":this.ddlOrder.SelectedValue.ToString().Substring(0, 12);
            string rOrder = "";// (orderno.Length == 0) ? "" : this.ddlOrder.SelectedValue.ToString().Substring(12);
            string mREQNAR = this.TxtRemarks.Text.Trim();
            string Curcode = this.ddlCurrency.SelectedValue.ToString();

            string pType = this.ddlPurType.SelectedValue.ToString();
            string headsupp = this.ddlSupplier.SelectedValue.ToString() ?? "";
            string nSupcode = this.ddlSupplier.SelectedValue.ToString();
            string Currency = this.ddlCurrency.SelectedValue.ToString();
            string season = this.DdlSeason.SelectedValue.ToString();
            string rType = (pType == "25003") ? "" : (pType == "25004") ? "" : "LC";

            bool result = purData.UpdateTransInfo01(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQB", mREQNO, mREQDAT, mPACTCODE, rType,
                mREQUSRID, mAPPRUSRID, mAPPRDAT, mEDDAT, mREQBYDES, mAPPBYDES, mMRFNO, mREQNAR, PostedByid, Posttrmid, PostSession, ApprovByid, approvdat,
                Approvtrmid, ApprovSession, PostedDat, approved, Deptcode, mslccod, Curcode, Deptcode2, ChkByid, Chkdat, chked, pType, nSupcode, RevByid, CRevdat,
                revTrmid, revSeson, rOrder, season);  ///////////////////, , , 
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }



            for (int i = 0; i < gvBMvRev.Rows.Count; i++)
            {
                if (((CheckBox)gvBMvRev.Rows[i].FindControl("chkCol")).Checked == true)
                {
                    string mRSIRCODE = ((Label)gvBMvRev.Rows[i].FindControl("LblRsircode")).Text.ToString();
                    string mSPCFCOD = ((Label)gvBMvRev.Rows[i].FindControl("LblSpcfCod")).Text.ToString();
                    double atprqty = Convert.ToDouble(((LinkButton)gvBMvRev.Rows[i].FindControl("lgvBomqty")).Text.ToString()) - Convert.ToDouble("0" + ((LinkButton)gvBMvRev.Rows[i].FindControl("lgvOrdreqqty")).Text.ToString());
                    double mPREQTY = Convert.ToDouble(((LinkButton)gvBMvRev.Rows[i].FindControl("lgvBomqty")).Text.ToString()) - Convert.ToDouble("0" + ((LinkButton)gvBMvRev.Rows[i].FindControl("lgvOrdreqqty")).Text.ToString());
                    double mAREQTY = Convert.ToDouble(((LinkButton)gvBMvRev.Rows[i].FindControl("lgvBomqty")).Text.ToString()) - Convert.ToDouble("0" + ((LinkButton)gvBMvRev.Rows[i].FindControl("lgvOrdreqqty")).Text.ToString());
                    double mConRate = Convert.ToDouble(this.lblConRate.Text.Trim());
                    string mREQRAT = Convert.ToDouble(((Label)gvBMvRev.Rows[i].FindControl("LblRate")).Text.ToString()).ToString();
                    string mPSTKQTY = "0.00";
                    string mEXPUSEDT = "";
                    string mREQNOTE = "";
                    string PursDate = "01-Jan-1900";
                    string Lpurrate = "0.00";
                    string storecode = "";
                    string ptype = "Others";// tbl1.Rows[i]["ptype"].ToString();
                    string budget = "48001";// tbl1.Rows[i]["budget"].ToString();
                    string pkgsize = "";
                    string bomid = ((Label)gvBMvRev.Rows[i].FindControl("lgvOrdbomid")).Text.ToString();
                    string rfgqty = "0.00"; ;

                    result = purData.UpdateTransInfo01(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQA",
                                mREQNO, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mConRate.ToString(), mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                                PursDate, Lpurrate, storecode, ptype, budget, pkgsize, Currency, atprqty.ToString(), bomid, rfgqty);
                    //result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQA",
                    //            mREQNO, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                    //            PursDate, Lpurrate, storecode, "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
                    }

                }


            }



            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "closeModal( );", true);


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
            ((CheckBox)this.gvBMvRev.HeaderRow.FindControl("chkhead")).Checked = false;
            this.chkheadl_CheckedChanged(null, null);



        }

        protected void CheckReqbal_CheckedChanged(object sender, EventArgs e)
        {
            Data_Bind();
        }

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBOMList();
        }

        protected void lgvPOqty_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string bomid = ((Label)this.gvBMvRev.Rows[index].FindControl("lgvOrdbomid")).Text.ToString();
            string rsircode = ((Label)this.gvBMvRev.Rows[index].FindControl("LblRsircode")).Text.ToString();
            string spcfcod = ((Label)this.gvBMvRev.Rows[index].FindControl("LblSpcfCod")).Text.ToString();
            string DescType = "";
            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    DescType += item.Value;
                }
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_LC_INTERFACE", "GET_BOM_WISE_PO_LIST", DescType, rsircode, spcfcod, "", "", "");

            this.gvPoDetails.DataSource = ds1.Tables[0];
            this.gvPoDetails.DataBind();

            this.gvPoDetails.Visible = true;
            this.gvMaterialDetails.Visible = false;
            this.MaterialSuma.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);
        }
        protected void lgvOrdreqqty_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string bomid = ((Label)this.gvBMvRev.Rows[index].FindControl("lgvOrdbomid")).Text.ToString();
            string rsircode = ((Label)this.gvBMvRev.Rows[index].FindControl("LblRsircode")).Text.ToString();
            string spcfcod = ((Label)this.gvBMvRev.Rows[index].FindControl("LblSpcfCod")).Text.ToString();

            DataSet ds = new DataSet("ds");
            DataTable dt = new DataTable();
            ds.Merge(dt);

            ds.Tables[0].Columns.Add("bomid", typeof(string));

            ds.Tables[0].TableName = "tbl1";

            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    DataRow dr = ds.Tables["tbl1"].NewRow();
                    dr["bomid"] = item.Value;
                    ds.Tables["tbl1"].Rows.Add(dr);
                }
            }
            DataSet ds1 = purData.GetTransInfoNew(comcod, "SP_LC_INTERFACE", "GET_BOM_WISE_REQ_LIST", ds, null, null, "", rsircode, spcfcod, "", "", "");

            this.gvREQDetails.DataSource = ds1.Tables[0];
            this.gvREQDetails.DataBind();

            //this.gvREQDetails.Visible = true;
            //this.gvMaterialDetails.Visible = false;
            //this.MaterialSuma.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModalReq();", true);
        }

        protected void ddlSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.lbtnOk_Click(sender, e);

            DataTable dt = (DataTable)ViewState["tblOrderRcv"];

            DataTable summaryDt = null;

            if (ddlSummary.SelectedIndex == 2)
            {
                this.ReqBtn.Visible = false;

                gvBMvRev.Columns[5].Visible = true;
                gvBMvRev.Columns[17].Visible = false;
                summaryDt = dt.AsEnumerable()
                                    .GroupBy(row => new { rsircode = row.Field<string>("rsircode"), spcfcod = row.Field<string>("spcfcod") })
                                    .Select(g =>
                                    {
                                        var row = dt.NewRow();

                                        row["rsircode"] = g.Key.rsircode;
                                        row["spcfcod"] = g.Key.spcfcod;
                                        row["comcod"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["comcod"];
                                        //row["bomid"] = g.Where(r => r["rsircode"] == g.Key.rsircode.ToString() && r["spcfcod"] == g.Key.spcfcod.ToString()).First()["bomid"];
                                        row["bomid"] = "";
                                        row["rsircode"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["rsircode"];
                                        row["rsirdesc"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["rsirdesc"];
                                        row["spcfcod"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["spcfcod"];
                                        row["spcfdesc"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["spcfdesc"];
                                        row["unit"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["unit"];
                                        //row["reqdat"] = g.Where(r => r["rsircode"] == g.Key.rsircode.ToString() && r["spcfcod"] == g.Key.spcfcod.ToString()).First()["reqdat"];
                                        row["reqdat"] = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", null);
                                        row["mlccod"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["mlccod"];
                                        row["mlcdesc"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["mlcdesc"];
                                        row["dayid"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["dayid"];
                                        row["buyerid"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["buyerid"];
                                        //row["buyername"] = g.Where(r => r["rsircode"] == g.Key.rsircode.ToString() && r["spcfcod"] == g.Key.spcfcod.ToString()).First()["buyername"];
                                        row["buyername"] = "";
                                        row["stkqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).Sum(r => r.Field<decimal>("stkqty"));
                                        row["bomqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).Sum(r => r.Field<decimal>("bomqty"));
                                        row["bomamt"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).Sum(r => r.Field<decimal>("bomamt"));
                                        row["reqqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).Sum(r => r.Field<decimal>("reqqty"));
                                        row["reqamt"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).Sum(r => r.Field<decimal>("reqamt"));
                                        row["rate"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).Sum(r => r.Field<decimal>("rate"));
                                        //row["reqbal"] = "";
                                        row["purordqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).Sum(r => r.Field<decimal>("purordqty"));
                                        //row["purordbal"] = g.Sum(r => r.Field<decimal>("rsqty"));
                                        row["mrrqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).Sum(r => r.Field<decimal>("mrrqty"));
                                        row["mrramt"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).Sum(r => r.Field<decimal>("mrramt"));
                                        row["remainqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).Sum(r => r.Field<decimal>("remainqty"));
                                        row["defaultsupname"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["defaultsupname"];
                                        row["defaultsup"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["defaultsup"];
                                        row["projorder"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["projorder"];
                                        row["ttlordqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString() && r["spcfcod"].ToString() == g.Key.spcfcod.ToString()).First()["ttlordqty"];

                                        return row;
                                    })
                                    .OrderBy(row => row.Field<string>("rsircode"))
                                    .CopyToDataTable();

                ViewState["tblSummaryBOMVsPO"] = summaryDt;
            }
            else if (ddlSummary.SelectedIndex == 1)
            {
                gvBMvRev.Columns[5].Visible = false;
                gvBMvRev.Columns[17].Visible = false;

                this.ReqBtn.Visible = false;
                summaryDt = dt.AsEnumerable()
                                    .GroupBy(row => new { rsircode = row.Field<string>("rsircode") })
                                    .Select(g =>
                                    {
                                        var row = dt.NewRow();

                                        row["rsircode"] = g.Key.rsircode;
                                        row["comcod"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["comcod"];
                                        //row["bomid"] = g.Where(r => r["rsircode"] == g.Key.rsircode.ToString() && r["spcfcod"] == g.Key.spcfcod.ToString()).First()["bomid"];
                                        row["bomid"] = "";
                                        row["rsircode"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["rsircode"];
                                        row["rsirdesc"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["rsirdesc"];
                                        //row["spcfcod"] = g.Where(r => r["rsircode"] == g.Key.rsircode.ToString() ).First()["spcfcod"];
                                        //row["spcfdesc"] = g.Where(r => r["rsircode"] == g.Key.rsircode.ToString() ).First()["spcfdesc"];
                                        row["unit"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["unit"];
                                        //row["reqdat"] = g.Where(r => r["rsircode"] == g.Key.rsircode.ToString() ).First()["reqdat"];
                                        row["reqdat"] = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", null);
                                        row["mlccod"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["mlccod"];
                                        row["mlcdesc"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["mlcdesc"];
                                        row["dayid"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["dayid"];
                                        row["buyerid"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["buyerid"];
                                        //row["buyername"] = g.Where(r => r["rsircode"] == g.Key.rsircode.ToString() ).First()["buyername"];
                                        row["buyername"] = "";
                                        row["stkqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).Sum(r => r.Field<decimal>("stkqty"));
                                        row["bomqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).Sum(r => r.Field<decimal>("bomqty"));
                                        row["bomamt"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).Sum(r => r.Field<decimal>("bomamt"));
                                        row["reqqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).Sum(r => r.Field<decimal>("reqqty"));
                                        row["reqamt"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).Sum(r => r.Field<decimal>("reqamt"));
                                        row["rate"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).Sum(r => r.Field<decimal>("rate"));
                                        //row["reqbal"] = "";
                                        row["purordqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).Sum(r => r.Field<decimal>("purordqty"));
                                        //row["purordbal"] = g.Sum(r => r.Field<decimal>("rsqty"));
                                        row["mrrqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).Sum(r => r.Field<decimal>("mrrqty"));
                                        row["mrramt"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).Sum(r => r.Field<decimal>("mrramt"));
                                        row["remainqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).Sum(r => r.Field<decimal>("remainqty"));
                                        row["defaultsupname"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["defaultsupname"];
                                        row["defaultsup"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["defaultsup"];
                                        row["projorder"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["projorder"];
                                        row["ttlordqty"] = g.Where(r => r["rsircode"].ToString() == g.Key.rsircode.ToString()).First()["ttlordqty"];

                                        return row;
                                    })
                                    .OrderBy(row => row.Field<string>("rsircode"))
                                    .CopyToDataTable();

                ViewState["tblSummaryBOMVsPO"] = summaryDt;
            }
            else
            {
                gvBMvRev.Columns[5].Visible = true;
                gvBMvRev.Columns[17].Visible = true;
                this.ReqBtn.Visible = true;
                ViewState["tblSummaryBOMVsPO"] = null;
            }

            this.Data_Bind();
        }

        protected void lgvBomqty_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string rsircode = ((Label)this.gvBMvRev.Rows[index].FindControl("LblRsircode")).Text.ToString();
            string spcfcod = ((Label)this.gvBMvRev.Rows[index].FindControl("LblSpcfCod")).Text.ToString();
            double stockqty = Convert.ToDouble(((Label)this.gvBMvRev.Rows[index].FindControl("lgvStockqty")).Text.Trim() == "" ? "0" : ((Label)this.gvBMvRev.Rows[index].FindControl("lgvStockqty")).Text.Trim());
            DataSet ds = new DataSet("ds");
            DataTable dt = new DataTable();
            ds.Merge(dt);

            ds.Tables[0].Columns.Add("bomid", typeof(string));

            ds.Tables[0].TableName = "tbl1";

            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    DataRow dr = ds.Tables["tbl1"].NewRow();
                    dr["bomid"] = item.Value;
                    ds.Tables["tbl1"].Rows.Add(dr);
                }
            }
            string season = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfoNew(comcod, "SP_REPORT_ORDER_STATUS", "GET_BOM_WISE_MATERIAL_DETAILS", ds, null, null, rsircode, spcfcod, "", season);

            this.gvMaterialDetails.DataSource = ds1.Tables[0];
            this.gvMaterialDetails.DataBind();

            ((Label)this.gvMaterialDetails.FooterRow.FindControl("lblgvFQty")).Text =
             Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("Sum(itmqty)", "")) ?
             0.00 : ds1.Tables[0].Compute("Sum(itmqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            this.gvPoDetails.Visible = false;
            this.gvMaterialDetails.Visible = true;
            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.MaterialSuma.Visible = true;
                this.TotalReq.InnerHtml = Convert.ToDouble(ds1.Tables[1].Rows[0]["totalreq"]).ToString("#,##0.00");
                this.Prjtionreq.InnerHtml = Convert.ToDouble(ds1.Tables[1].Rows[0]["projectionreq"]).ToString("#,##0.00");
                this.ReqWithBom.InnerHtml = Convert.ToDouble(ds1.Tables[1].Rows[0]["bomwisreq"]).ToString("#,##0.00");
                this.TotalPO.InnerHtml = Convert.ToDouble(ds1.Tables[1].Rows[0]["totalpo"]).ToString("#,##0.00");
                this.PrjctionORder.InnerHtml = Convert.ToDouble(ds1.Tables[1].Rows[0]["projectionpo"]).ToString("#,##0.00");
                this.OrderWithBOm.InnerHtml = Convert.ToDouble(ds1.Tables[1].Rows[0]["bomwispo"]).ToString("#,##0.00");
            }
            else
            {
                this.MaterialSuma.Visible = false;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal(" + stockqty + ");", true);

            this.EnableExcelDownload(ds1.Tables[0]);

        }


        private void EnableExcelDownload(DataTable dt)
        {
            try
            {
                GridView gvTemp = new GridView();
                gvTemp.AllowPaging = false;
                gvTemp.DataSource = dt;
                gvTemp.DataBind();
                Session["Report1"] = gvTemp;
                lnkbtnExclDnld.NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception ex)
            {
            }

        }

        protected void CheckPObal_CheckedChanged(object sender, EventArgs e)
        {
            Data_Bind();
        }

        protected void gvSesonWiseBom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSesonWiseBom.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvSesonWiseBom_Sorting(object sender, GridViewSortEventArgs e)
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

            DataTable dt = (DataTable)ViewState["tblSesonWiseBom"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            gvSesonWiseBom.DataSource = sortedView;
            gvSesonWiseBom.DataBind();

            ViewState["tblSesonWiseBom"] = sortedView.ToTable();
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

        protected void LowerValue_TextChanged(object sender, EventArgs e)
        {
            string lower = this.LowerValue.Text.ToString();
            string uper = this.UperValue.Text.ToString();
            DataTable dt = (DataTable)ViewState["tblSesonWiseBom"];
            if (dt == null)
                return;
            DataView dv = dt.DefaultView;
            if (lower != "" || uper != "")
            {
                if (Convert.ToDouble(lower) > 1 || Convert.ToDouble(uper) < 500000)
                {
                    dv.RowFilter = "uptopercnt >=" + lower + " and uptopercnt <= " + uper + "";
                }
            }

            dt = dv.ToTable();

            this.ExitLower.Text = lower;
            gvSesonWiseBom.DataSource = dt;
            gvSesonWiseBom.DataBind();
        }

        protected void UperValue_TextChanged(object sender, EventArgs e)
        {
            string lower = this.LowerValue.Text.ToString();
            string uper = this.UperValue.Text.ToString();
            DataTable dt = (DataTable)ViewState["tblSesonWiseBom"];
            if (dt == null)
                return;

            DataView dv = dt.DefaultView;
            if (lower != "" || uper != "")
            {
                if (Convert.ToDouble(lower) > 1 || Convert.ToDouble(uper) < 500000)
                {
                    dv.RowFilter = "uptopercnt >=" + lower + " and uptopercnt <= " + uper + "";
                }
            }

            dt = dv.ToTable();
            this.ExitUper.Text = uper;
            gvSesonWiseBom.DataSource = dt;
            gvSesonWiseBom.DataBind();
        }

        protected void gvBomWise_Sorting(object sender, GridViewSortEventArgs e)
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

            DataTable dt = (DataTable)Session["tblBomWiseMat"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            gvBomWise.DataSource = sortedView;
            gvBomWise.DataBind();

            Session["tblBomWiseMat"] = sortedView.ToTable();
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string FindOrder = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            string customer = "000000000000%";

            DataTable dtAll = new DataTable();
            ViewState["BomList1"] = null;

            foreach (ListItem item in DdlCustomer2.Items)
            {
                if (item.Selected)
                {
                    customer = (item.Value == "000000000000") ? "%" : item.Value + "%";

                    DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GETBOMLIST", FindOrder, customer, "", "", "", "", "", "", "");

                    if (ViewState["BomList1"] != null)
                    {
                        DataTable dt1 = (DataTable)ViewState["BomList1"];
                        if (ds1 == null)
                            return;

                        dtAll = dt1.Copy();
                        dtAll.Merge(ds1.Tables[0]);
                        ViewState["BomList1"] = dtAll;
                    }
                    else
                    {
                        ViewState["BomList1"] = null;
                        ViewState["BomList1"] = ds1.Tables[0];
                        dtAll = ds1.Tables[0];
                    }
                }
            }

            this.DdlCustomer.SelectedValue = "000000000000";

            this.ddlBuyer.DataTextField = "bomdesc";
            this.ddlBuyer.DataValueField = "bomid";
            this.ddlBuyer.DataSource = dtAll;
            this.ddlBuyer.DataBind();

            DataTable dt = dtAll.Copy();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            ViewState["BomList"] = ds;

        }
    }
}