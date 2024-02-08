using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY;
using SPEENTITY.C_19_Exp;
using System.Drawing;

namespace SPEWEB.F_19_EXP
{
    public partial class RptDaywiseShipment : System.Web.UI.Page
    {
        UserManagerSampling objUserMan = new UserManagerSampling();
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetProjectName();
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string type = this.Request.QueryString["Type"].ToString();
                this.lpnl();
                this.GetBuyer();
                this.GetSeason();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "shipment") ? "Day wise Shipment Report"
                                                                    : (type == "unrealization") ? "Export Statement Unrealization Report"
                                                                    : (type == "ShipmentPlan") ? "Day Wise Shipment Plan Summary"
                                                                    : (type == "IncntvDclr") ? "Incentive Declaration" : "Day wise Export Statement";
            }
        }

        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            DataView dv21 = ds2.Tables[0].DefaultView;
            DataRowView newRow = dv21.AddNew();
            DataView dv22 = new DataView(ds2.Tables[0]);
            dv22.RowFilter = ("sircode not like '000000000000'");

            newRow = dv22.AddNew();
            newRow["sircode"] = "000000000000";
            newRow["sirdesc"] = "All";
            dv22.ToTable().Rows.Add(newRow);

            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = dv22;
            this.ddlBuyer.DataBind();
            this.ddlBuyer.SelectedValue = "000000000000";

            ddlBuyer_SelectedIndexChanged(null, null);
        }

        protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSeason_SelectedIndexChanged(null, null);
        }

        private void GetSeason()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            ddlSeason.DataTextField = "gdesc";
            ddlSeason.DataValueField = "gcod";
            ddlSeason.DataSource = ds1.Tables[0];
            ddlSeason.DataBind();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.ddlSeason.SelectedValue = season;
            }
            else
            {
                this.ddlSeason.SelectedValue = "00000";
            }
            ddlSeason_SelectedIndexChanged(null, null);
        }

        protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string season = this.ddlSeason.SelectedValue.ToString() == "0000" ? "%" : this.ddlSeason.SelectedValue.ToString() + "%";
            string buyer = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_INV_STDANA", "GETORDERMLCCOD", "1601%", buyer, season, "", "", "", "", "");
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            ViewState["tbllcorder"] = lst;

            DataView dv21 = ds2.Tables[0].DefaultView;
            DataRowView newRow = dv21.AddNew();
            DataView dv22 = new DataView(ds2.Tables[0]);
            dv22.RowFilter = ("mlccod not like '000000000000'");

            newRow = dv22.AddNew();
            newRow["mlccod"] = "000000000000";
            newRow["mlcdesc"] = "All";
            dv22.ToTable().Rows.Add(newRow);

            this.ddlmlccode.DataSource = dv22;
            this.ddlmlccode.DataTextField = "mlcdesc";
            this.ddlmlccode.DataValueField = "mlccod";
            this.ddlmlccode.DataBind();
            this.ddlmlccode.SelectedValue = "000000000000";

            ds2.Dispose();
            this.ddlmlccode_SelectedIndexChanged(null, null);
        }

        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tbllcorder"];

            string mlccode = this.ddlmlccode.SelectedValue.ToString();
            tbl1 = (mlccode == "000000000000") ? tbl1 : tbl1.FindAll(x => x.mlccod == mlccode);
            tbl1.Add(new SPEENTITY.C_03_CostABgd.EclassSalesContact("", "", "", "", "", "", "", "", "", 0.00, Convert.ToDateTime("01-Jan-1900"), 0.00, 0.00, "", "", "All", "All", "", "", ""));
            this.dllorderType.DataSource = tbl1;
            this.dllorderType.DataTextField = "rdaydesc";
            this.dllorderType.DataValueField = "rdayid";
            this.dllorderType.DataBind();
            this.dllorderType.SelectedValue = "All";

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void CurrencyInf()
        {
            SalesInvoice_BL lst = new SalesInvoice_BL();
            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;
            this.txtUSD.Text = lstConv.Find(p => p.tcode == "019" && p.fcode == "001").conrate.ToString();
            this.txtEuro.Text = lstConv.Find(p => p.tcode == "006" && p.fcode == "001").conrate.ToString();
            this.txtPound.Text = lstConv.Find(p => p.tcode == "007" && p.fcode == "001").conrate.ToString();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "shipment":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetShipmentReport();
                    break;
                case "QuantityB":
                    //this.MultiView1.ActiveViewIndex = 1;
                    //this.GetInvQB();
                    break;
                case "AmountB":
                    //this.MultiView1.ActiveViewIndex = 2;
                    //this.GetAmtInvB();
                    break;
                case "Location":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetDataLocWise();
                    break;
                case "summary":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.GetDataSummary();
                    break;
                case "unrealization":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.calcurate.Visible = true;
                    this.totaltk.Visible = true;
                    this.CurrencyInf();
                    this.GetDataUnrealization();
                    this.lbtnCalculate_Click(null, null);
                    break;
                case "ShipmentPlan":
                    this.divArticleDdl.Visible = false;
                    this.divOrderDdl.Visible = false;
                    this.MultiView1.ActiveViewIndex = 5;
                    this.GetShipmentPlanSummary();
                    break;

                case "IncntvDclr":
                    this.GetIncntvDclarationData();
                    break;
            }

        }

        private void GetIncntvDclarationData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fdate = this.txtDatefrom.Text.ToString();
            string tdate = this.txtDateto.Text.ToString();

            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "GET_INCNTV_DECLARATION", fdate, tdate);

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                this.gvIncntv.DataSource = null;
                this.gvIncntv.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
            }

            ViewState["tblIncntv"] = ds.Tables[0];

            this.Data_Bind();
        }

        public void lpnl()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "shipment":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "QuantityB":
                    //this.MultiView1.ActiveViewIndex = 1;
                    //this.GetInvQB();
                    break;
                case "AmountB":
                    //this.MultiView1.ActiveViewIndex = 2;
                    //this.GetAmtInvB();
                    break;
                case "Location":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "summary":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "unrealization":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.calcurate.Visible = false;
                    this.totaltk.Visible = false;
                    this.divOrderDdl.Visible = false;
                    this.lblmlccod.Visible = false;
                    this.ddlmlccode.Visible = false;
                    this.lblunrealtype.Visible = true;
                    this.DdlUnrealType.Visible = true;
                    //this.txtDatefrom.Visible = false;
                    this.divToDate.Visible = false;
                    break;

                case "ShipmentPlan":
                    this.pnlSeason.Visible = false;
                    this.divArticleDdl.Visible = false;
                    this.divOrderDdl.Visible = false;
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "IncntvDclr":
                    this.pnlBuyer.Visible = false;
                    this.pnlSeason.Visible = false;
                    this.divArticleDdl.Visible = false;
                    this.divOrderDdl.Visible = false;
                    this.MultiView1.ActiveViewIndex = 6;
                    break;

            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetShipmentReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string date2 = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string mlccod = (this.ddlmlccode.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlmlccode.SelectedValue.ToString() + "%";
            string ordtype = (this.dllorderType.SelectedValue.ToString() == "All") ? "%" : this.dllorderType.SelectedValue.ToString() + "%";

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "DAYWISE_SHIPMENT_REPORT", date1, date2, mlccod, ordtype, "", "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            DataTable dt1 = ds2.Tables[0];
            this.HiddenSameDate(dt1);
            this.Data_Bind();

        }

        private void GetInvQB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
                                      //string actcode = this.ddlAccProject.SelectedValue.ToString();

            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GENARALINVRPT", date1, date2, TopHead, "", "", "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void GetAmtInvB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
                                      // string actcode = this.ddlAccProject.SelectedValue.ToString();

            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GENARALINVRPT", date1, date2, TopHead, "", "", "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void GetDataLocWise()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string date1 = this.txtDatefrom.Text.Substring(0, 11);
            //string date2 = this.txtDateto.Text.Substring(0, 11);

            //string Buyer = (this.ddlBuyer.SelectedValue.ToString()=="00000")?"%":this.ddlBuyer.SelectedValue.ToString()+"%";
            //string mlccode = (this.ddlmlccode.SelectedValue.ToString()=="000000000000")?"%":this.ddlmlccode.SelectedValue.ToString()+"%";
            //string orderType = (this.dllorderType.SelectedValue.ToString()== "00000000") ?"%":this.dllorderType.SelectedValue.ToString()+"%";
            //DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_FG_INV_02", "RPT_LOCATION_WISE_STOCK", location, date1, date2, Buyer, mlccode, orderType, "", "", "");
            //Session["tblVeiw"] = ds2.Tables[0];
            //DataTable dt1 = ds2.Tables[0];
            //this.HiddenSameDate(dt1);
            //this.Data_Bind();

        }
        private void GetDataSummary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);

            string Buyer = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";
            string mlccode = (this.ddlmlccode.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlmlccode.SelectedValue.ToString() + "%";
            string orderType = (this.dllorderType.SelectedValue.ToString() == "00000000") ? "%" : this.dllorderType.SelectedValue.ToString() + "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "DAYWISE_EXPORT_STATEMENT_REPORT", date1, date2, Buyer, mlccode, orderType, "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            DataTable dt1 = ds2.Tables[0];
            //this.HiddenSameDate(dt1);
            this.Data_Bind();

        }
        private void GetDataUnrealization()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string Buyer = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";
            string unrealType = this.DdlUnrealType.SelectedValue.ToString();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "DAYWISE_EXPORT_UNREALIZATION_REPORT", date1, Buyer, unrealType, "", "", "");
            Session["tblVeiw"] = ds2.Tables[0];
            DataTable dt1 = ds2.Tables[0];
            //this.HiddenSameDate(dt1);
            this.Data_Bind();

        }

        private void GetShipmentPlanSummary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fdate = this.txtDatefrom.Text.ToString();
            string tdate = this.txtDateto.Text.ToString();
            string buyer = this.ddlBuyer.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "DAYWISE_SHIPMENT_PLAN_REPORT", fdate, tdate, buyer);
            Session["tblShipmentPlanDetails"] = ds2.Tables[0];
            Session["tblShipmentPlanSummary"] = ds2.Tables[1];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "shipment":
                    this.gvshipment.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvshipment.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvshipment.DataBind();

                    this.FooterCalculation((DataTable)Session["tblVeiw"]);
                    break;

                case "QuantityB":
                    this.gvQBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvQBasis.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvQBasis.DataBind();

                    this.FooterCalculation((DataTable)Session["tblVeiw"]);
                    break;

                case "AmountB":
                    this.gvAmtBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvAmtBasis.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvAmtBasis.DataBind();

                    this.FooterCalculation((DataTable)Session["tblVeiw"]);
                    break;

                case "summary":
                    this.gvSummery.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSummery.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvSummery.DataBind();

                    this.FooterCalculation((DataTable)Session["tblVeiw"]);
                    break;

                case "unrealization":
                    this.gvUnrealization.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvUnrealization.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvUnrealization.DataBind();

                    this.FooterCalculation((DataTable)Session["tblVeiw"]);
                    break;

                case "ShipmentPlan":
                    this.gvShipPlanSummary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvShipPlanSummary.DataSource = (DataTable)Session["tblShipmentPlanSummary"];
                    this.gvShipPlanSummary.DataBind();

                    this.gvShipPlanDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvShipPlanDetails.DataSource = (DataTable)Session["tblShipmentPlanDetails"];
                    this.gvShipPlanDetails.DataBind();
                    this.FooterCalculation((DataTable)Session["tblShipmentPlanDetails"]);
                    break;

                case "IncntvDclr":
                    this.gvIncntv.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvIncntv.DataSource = (DataTable)ViewState["tblIncntv"];
                    this.gvIncntv.DataBind();
                    break;
            }

        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["mlccod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mlccod"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["mlccod"].ToString();
                    dt1.Rows[j]["mlcdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["mlccod"].ToString();
                }

            }
            return dt1;

            Session["tblVeiw"] = dt1;
        }

        protected void GetProjectName()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            ////string comcod = GetComCode();
            //string txtsrch = this.txtSearch.Text.Trim() + "%";
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "GETORDERNO", txtsrch, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;

            //this.ddlAccProject.DataTextField = "actdesc";
            //this.ddlAccProject.DataValueField = "actcode";
            //this.ddlAccProject.DataSource = ds1.Tables[0];
            //this.ddlAccProject.DataBind();

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string HeaderCode = "41%";
            //string filter = this.txtSearch.Text.Trim() + "%";

            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", HeaderCode, filter, "", "", "", "", "", "", "");
            //DataTable dt1 = ds1.Tables[0];
            //this.ddlAccProject.DataSource = dt1;
            //this.ddlAccProject.DataTextField = "actdesc1";
            //this.ddlAccProject.DataValueField = "actcode";
            //this.ddlAccProject.DataBind();

        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "shipment":
                    DataTable Tempdt;
                    DataView Tempdv;
                    Tempdt = dt.Copy();
                    Tempdv = Tempdt.DefaultView;
                    Tempdv.RowFilter = ("invno <>'Sub Total'");
                    Tempdt = Tempdv.ToTable();
                    ((Label)this.gvshipment.FooterRow.FindControl("lgvABFOpQty")).Text = Convert.ToDouble((Convert.IsDBNull(Tempdt.Compute("Sum(totlprs)", "")) ?
                        0.00 : Tempdt.Compute("Sum(totlprs)", ""))).ToString("#,##0;(#,##0);  ");
                    break;
                case "QuantityB":

                    break;
                case "AmountB":
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFOpnAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recam)", "")) ?
                        0.00 : dt.Compute("Sum(recam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFtrnsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                        0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFIssAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(matisamt)", "")) ?
                        0.00 : dt.Compute("Sum(matisamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFStkAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stcamt)", "")) ?
                        0.00 : dt.Compute("Sum(stcamt)", ""))).ToString("#,##0;(#,##0);  ");
                    //this.gvAmtBasis.DataBind();
                    break;
                case "summary":
                    ((Label)this.gvSummery.FooterRow.FindControl("lgvtotlprs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totlprs)", "")) ?
                        0.00 : dt.Compute("Sum(totlprs)", ""))).ToString("#,##0;(#,##0);  ");
                    //this.gvAmtBasis.DataBind();
                    break;
                case "unrealization":
                    ((Label)this.gvUnrealization.FooterRow.FindControl("lgvftotlprs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totlprs)", "")) ?
                        0.00 : dt.Compute("Sum(totlprs)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvUnrealization.FooterRow.FindControl("lgvfusdunrealamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(usdunrealamt)", "")) ?
                        0.00 : dt.Compute("Sum(usdunrealamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvUnrealization.FooterRow.FindControl("lgvfeurounrealamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(eurounrealamt)", "")) ?
                        0.00 : dt.Compute("Sum(eurounrealamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvUnrealization.FooterRow.FindControl("lgvfpoununrealamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(poununrealamt)", "")) ?
                        0.00 : dt.Compute("Sum(poununrealamt)", ""))).ToString("#,##0;(#,##0);  ");

                    //this.gvAmtBasis.DataBind();
                    break;
                case "ShipmentPlan":
                    ((Label)this.gvShipPlanDetails.FooterRow.FindControl("lblgvTotalQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty)", "")) ?
                        0.00 : dt.Compute("Sum(qty)", ""))).ToString("#,##0;(#,##0);  ");
                    break;

            }

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "shipment":
                    this.RptShipmentReport();
                    break;
                case "QuantityB":
                    //this.rptCentralStockQB();
                    break;
                case "AmountB":
                    //this.rptCentralStockAB();
                    break;
                case "summary":
                    this.rptSummary();
                    break;

                case "unrealization":
                    this.rptUnrealization();
                    break;

                case "IncntvDclr":
                    this.PrintIncentiveDeclaration();
                    break;

            }

        }

        private void PrintIncentiveDeclaration()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string note1 = "We, F B Footwear Ltd., Manufacturer / Exporter of Complete Leather Footwear having our Head Office at Suite # B-3, Level # 2, House # 06, Road # 109, Gulshan-2, Dhaka, Bangladesh do hereby declared, as required by Bangladesh Bank that we have not been availed and / or will not be avail any Duty Drawback facility and / or Bond facility against the goods, exported under above EXP Form.";
            string note2 = "We, hereby further declared that we are applying only for Cash Subsidy facility against the Export of Complete Gents Leather Footwear on and after realization of Export Proceeds into Bangladesh through normal Banking Channel in terms of  F. E. Circular No. 08/2016, Date: 04-04-2016, 09/2000 date 17.04.2000, 11/2000 date-08.05.2000, 17/2000 date-19.09.2000, 19/2000 date-18.10.2000, 24/2000 date-06.12.2000, 06/2005 date-06.06.2005, 06/2006 date-14.08.2006  &  03/2007 date-16.08.2007  issued by Bangladesh Bank.";

            DataTable dt = (DataTable)ViewState["tblIncntv"];

            var lst = dt.DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptIncntvDeclaration>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptIncntvDeclaration", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "DECLARATION"));
            rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            rpt1.SetParameters(new ReportParameter("note1", note1));
            rpt1.SetParameters(new ReportParameter("note2", note2));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void RptShipmentReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblVeiw"];

            var lst = dt.DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptShipment", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("ToFrDate", "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )"));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Day wise Shipment Report"));
            rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            //  rpt1.SetParameters(new ReportParameter("ProjectName", "Priject Name: " + this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13)));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            // rpt1.SetParameters(new ReportParameter("todate", DateTime.Today.ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void rptSummary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblVeiw"];

            var lst = dt.DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportSummery>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptExportSummery", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Day wise Export Statement"));
            rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void rptUnrealization()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string comadd = hst["comadd1"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                double lblUSD = Convert.ToDouble((this.lblUSD.Text.ToString()) == "  " ? "0.00" : this.lblUSD.Text.ToString());
                double lblEuro = Convert.ToDouble((this.lblEuro.Text.ToString()) == "  " ? "0.00" : this.lblEuro.Text.ToString());
                double lblPound = Convert.ToDouble((this.lblPound.Text.ToString()) == "  " ? "0.00" : this.lblPound.Text.ToString());
                string total = (Convert.ToDouble(lblUSD) + Convert.ToDouble(lblEuro) + Convert.ToDouble(lblPound)).ToString("#,##0.00;(#,##0.00);  ");
                DataTable dt = (DataTable)Session["tblVeiw"];

                var lst = dt.DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportSummery>();

                LocalReport rpt1 = new LocalReport();
                rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptExportUnrealization", lst, null, null);
                rpt1.EnableExternalImages = true;

                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("RptTitle", "Export Statement Unrealization Report"));
                rpt1.SetParameters(new ReportParameter("USD", lblUSD.ToString("#,##0.00;(#,##0.00);  ")));
                rpt1.SetParameters(new ReportParameter("Euro", lblEuro.ToString("#,##0.00;(#,##0.00);  ")));
                rpt1.SetParameters(new ReportParameter("Pound", lblPound.ToString("#,##0.00;(#,##0.00);  ")));
                rpt1.SetParameters(new ReportParameter("total", total));
                rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            catch (Exception)
            {

            }


        }
        protected void rptCentralStockOld()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblVeiw"];
            //ReportDocument rptstk = new RMGiRPT.R_17_FGInv.rptFGInvReport();//  new RMGiRPT.R_11_RawInv.RptCentralStore();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            //txtlevel.Text = comnam;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstk.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void rptCentralStockQB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblVeiw"];
            //ReportDocument rptstk = new RMGiRPT.R_11_RawInv.rptInvQtyBasis();//.RptCentralStore();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            //txtlevel.Text = "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim();
            ////TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            ////txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstk.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void rptCentralStockAB()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblVeiw"];
            //ReportDocument rptstk = new RMGiRPT.R_11_RawInv.rptInvAmtBasis();//.RptCentralStore();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            //txtlevel.Text = "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim();
            ////TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            ////txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstk.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ImgbtnFindProj_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
        }

        protected void gvCenStore_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvshipment.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvQBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvQBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvAmtBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAmtBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnCalculate_Click(object sender, EventArgs e)
        {
            //string  ((Label)this.gvUnrealization.FooterRow.FindControl("lgvftotlprs")).Text.ToString(); ((Label)this.gvUnrealization.FooterRow.FindControl("lgvfeurounrealamt")).Text.ToString() == ""?"0.00":   ((Label)this.gvUnrealization.FooterRow.FindControl("lgvfpoununrealamt")).Text == ""?"0.00":
            string usdamt = Convert.ToDouble(((Label)this.gvUnrealization.FooterRow.FindControl("lgvfusdunrealamt")).Text == " " ? "0.00" : ((Label)this.gvUnrealization.FooterRow.FindControl("lgvfusdunrealamt")).Text).ToString("#,##0.00;(#,##0.00);  ");
            string eroamt = (((Label)this.gvUnrealization.FooterRow.FindControl("lgvfeurounrealamt")).Text.ToString() == " " ? ((Label)this.gvUnrealization.FooterRow.FindControl("lgvfeurounrealamt")).Text.ToString() : "0.00").ToString();
            string pndamt = (((Label)this.gvUnrealization.FooterRow.FindControl("lgvfpoununrealamt")).Text.ToString() == " " ? ((Label)this.gvUnrealization.FooterRow.FindControl("lgvfpoununrealamt")).Text.ToString() : "0.00").ToString();
            //string pndamt = Convert.ToDouble( ((Label)this.gvUnrealization.FooterRow.FindControl("lgvfpoununrealamt")).Text).ToString("#,##0.00;(#,##0.00);  ");

            string usdamtrate = (this.txtUSD.Text.ToString()) == " " ? "0.00" : this.txtUSD.Text.ToString();
            string Euroamtrate = (this.txtEuro.Text.ToString()) == " " ? "0.00" : this.txtEuro.Text.ToString();
            string Poundamtrate = (this.txtPound.Text.ToString()) == " " ? "0.00" : this.txtPound.Text.ToString();


            this.lblUSD.Text = (Convert.ToDouble(usdamtrate) * Convert.ToDouble(usdamt)).ToString("#,##0.00;(#,##0.00);  ");
            this.lblEuro.Text = (Convert.ToDouble(Euroamtrate) * Convert.ToDouble(eroamt)).ToString("#,##0.00;(#,##0.00);  ");
            this.lblPound.Text = (Convert.ToDouble(Poundamtrate) * Convert.ToDouble(pndamt)).ToString("#,##0.00;(#,##0.00);  ");

        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string packpln = linkButton.CommandArgument.ToString();
            string comcod = GetCompCode();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_EXPORT_02", "DELETE_SHIPMENT_PLAN_REPORT", packpln);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Failed to delete.');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Successfully');", true);

            DataTable dt = (DataTable)Session["tblShipmentPlanDetails"];
            int index = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;
            dt.Rows[index].Delete();
            DataView dv = dt.DefaultView;
            Session["tblShipmentPlanDetails"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void gvShipPlanDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvshipment.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvShipPlanSummary_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvShipPlanSummary.EditIndex = e.NewEditIndex;
            this.Data_Bind();


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "selectedCurrentTabe();", true);
        }

        protected void gvShipPlanSummary_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvShipPlanSummary.EditIndex = -1;
            this.Data_Bind();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "selectedCurrentTabe();", true);
        }

        protected void gvShipPlanSummary_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string comcod = this.GetCompCode();
            string booked = ((CheckBox)gvShipPlanSummary.Rows[e.RowIndex].FindControl("chkboxBook")).Checked.ToString();
            string date1 = ((TextBox)gvShipPlanSummary.Rows[e.RowIndex].FindControl("txtDateExFac")).Text.Trim();
            string daet2 = ((TextBox)gvShipPlanSummary.Rows[e.RowIndex].FindControl("txtDatexfactdat")).Text.Trim();
            string packplan = ((Label)gvShipPlanSummary.Rows[e.RowIndex].FindControl("lblgvPackPlanRef2")).Text.Trim();

            booked = booked == "True" ? "1" : "0";

            string bookingDate = ((TextBox)gvShipPlanSummary.Rows[e.RowIndex].FindControl("txtBookingNumber")).Text.Trim();

            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_EXPORT_02", "UPDATE_SHIPMENT_SUMMARY", packplan, booked,
                           date1, daet2, bookingDate);

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated Successfully');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "selectedCurrentTabe();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
            }

            this.gvShipPlanSummary.EditIndex = -1;
            GetShipmentPlanSummary();
            this.Data_Bind();
        }

        protected void gvShipPlanSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && this.gvShipPlanSummary.EditIndex == -1)
            {
                Label lblBooked = (Label)e.Row.FindControl("lblBooked");

                string booked = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "booked")).ToString();

                if (booked == "No")
                {
                    lblBooked.Attributes.Add("class", "badge badge-pill badge-danger");
                }
                else
                {
                    lblBooked.Attributes.Add("class", "badge badge-pill badge-success");
                }

                HyperLink SummaryDetailLInk = (HyperLink)e.Row.FindControl("hlnkDetails");
                HyperLink LinkPackPlan = (HyperLink)e.Row.FindControl("hlnkgvPackPlanRef");

                if (DataBinder.Eval(e.Row.DataItem, "bookingnumber").ToString() == "")
                {
                    LinkPackPlan.NavigateUrl = "~/F_19_EXP/PackingListDetails?packplanref=" + DataBinder.Eval(e.Row.DataItem, "packplanref");
                    SummaryDetailLInk.NavigateUrl = "~/F_19_EXP/PackingListDetails?packplanref=" + DataBinder.Eval(e.Row.DataItem, "bookingnumber");
                    LinkPackPlan.Attributes.Add("class", "text-info");
                    SummaryDetailLInk.Attributes.Add("class", "text-success");
                }
                else
                {
                    LinkPackPlan.NavigateUrl = "~/F_19_EXP/PackingListDetails?packplanref=" + DataBinder.Eval(e.Row.DataItem, "packplanref");
                    SummaryDetailLInk.NavigateUrl = "~/F_19_EXP/PackingListDetails?packplanref=" + DataBinder.Eval(e.Row.DataItem, "bookingnumber");
                    LinkPackPlan.Attributes.Add("class", "text-info");
                    SummaryDetailLInk.Attributes.Add("class", "text-success");
                }

            }
        }

    }
}