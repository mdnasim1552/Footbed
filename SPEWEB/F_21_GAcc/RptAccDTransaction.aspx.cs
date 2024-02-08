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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_21_GAcc
{
    public partial class RptAccDTransaction : System.Web.UI.Page
    {

        ProcessAccess MktData = new ProcessAccess();
        public static double OpenBal, Clsbal, Dtdram, Dtcram;

        //double OpenBal = 0, Clsbal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string TrMod = Request.QueryString["TrMod"].Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (TrMod == "DTran" ? "DAILY TRANSACTION" : (TrMod == "RecPay" ? "RECEIPTS & PAYMENT"
                  : (TrMod == "DelTran" ? "DELETED TRANSACTION" : (TrMod == "IssuedVsCollect" ? "Issued Vs. Collection"
                  : (TrMod == "ProTrans" ? "DAILY TRANSACTION" : "FUND FLOW")))));
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));




                this.RbtnVisibility();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void RbtnVisibility()
        {
            string TrMode = this.Request.QueryString["TrMod"].ToString();
            switch (TrMode)
            {
                case "DTran":
                    this.rbtnList1.SelectedIndex = 0;
                    rbtnList1.Items.Remove("Daily Transaction");
                    rbtnList1.Items.Remove("Deleted Transaction");
                    rbtnList1.Items.Remove("Fund Flow");
                    rbtnList1.Items.Remove("Receipts & Payment");
                    rbtnList1.Items.Remove("Issued Vs. Collection");
                    rbtnList1.Items.Remove("Project Transaction");
                    break;

                case "DelTran":
                    this.rbtnList1.SelectedIndex = 3;
                    this.rbtnList1.Visible = false;
                    break;
                case "RecPay":
                    this.rbtnList1.SelectedIndex = 4;
                    this.rbtnList1.Visible = false;
                    break;

                case "Fflow":
                    this.rbtnList1.SelectedIndex = 5;
                    this.rbtnList1.Visible = false;
                    break;

                case "IssuedVsCollect":
                    this.rbtnList1.SelectedIndex = 6;
                    this.rbtnList1.Visible = false;
                    break;

                case "ProTrans":
                    this.rbtnList1.SelectedIndex = 7;
                    this.rbtnList1.Visible = false;
                    break;
            }

            this.rbtnList1_SelectedIndexChanged(null, null);

        }


        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.ShowCashBook();
                    break;

                case 1:
                    this.TransactionList();
                    break;

                case 2:
                    this.ShowDailyProposal();
                    break;

                case 3:
                    this.ShowDeletedtransaction();
                    break;

                case 4:
                case 5:
                    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                    this.ReceiptAndPayment();
                    break;

                case 6:
                    this.ShowIssuedVsColl();
                    break;

                case 7:
                    this.ShowpProTransaction();
                    break;



            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblprintstk")).Text;
                string eventdesc = "Show Report";
                string eventdesc2 = rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetAccCode()
        {


            string comcod = this.GetCompCode();
            string filter = "%" + this.txtAccSearch.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "GETACCHEAD", filter, "", "", "", "", "", "", "", "");
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataTextField = "actdesc1";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataBind();
            ds1.Dispose();
        }


        private void TransactionList()
        {
            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.Paneltovoucherno.Visible = true;
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtVouType = this.ddlVouchar.SelectedItem.ToString().Trim();
            string txtSVoucher = (txtVouType == "ALL Voucher" ? "" : txtVouType) + "%";


            string searchinfo = "";

            if (this.ddlSrch.SelectedValue != "")
            {

                if (this.ddlSrch.SelectedValue == "between")
                {
                    searchinfo = "dram between " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmount2.Text.Trim()).ToString();
                }
                else
                {
                    searchinfo = "dram " + this.ddlSrch.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString();
                }
            }

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTTRANSACTIONS", fromdate, todate, txtSVoucher, searchinfo, "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            DataTable dtr1 = HiddenSameDate(dtr);
            Session["tranlist"] = dtr1;
            DataTable tblt03 = (DataTable)Session["tranlist"];
            // this.gvtranlsit.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvtranlsit.DataSource = dtr1;
            this.gvtranlsit.DataBind();
            Session["tranlist"] = dtr1;
            this.FooterCalculation(dtr1, "gvtranlsit");

            this.lbltoCashVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[0]["tonum"]).ToString("#,##0; (#,##0); ");
            this.lbltoBankVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[1]["tonum"]).ToString("#,##0; (#,##0); ");
            this.lbltoContraVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[2]["tonum"]).ToString("#,##0; (#,##0); ");
            this.lbltoJournalVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[3]["tonum"]).ToString("#,##0; (#,##0); ");
            Session["Report1"] = gvtranlsit;
            ((HyperLink)this.gvtranlsit.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }

        private void ShowCashBook()
        {
            Session.Remove("cashbank");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comcod = hst["comcod"].ToString();
            string txtVouType = this.ddlVoucharCash.SelectedValue.ToString().Trim();
            string txtSVoucher = (txtVouType == "ALL Voucher" ? "" : txtVouType);
            string searchinfo = "";

            if (this.ddlSrchCash.SelectedValue != "")
            {

                if (this.ddlSrchCash.SelectedValue == "between")
                {
                    searchinfo = "(srcham between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

                }

                else
                {
                    searchinfo = "( srcham " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

                }
            }

            //string txtSProject =  "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTCASHBOOK", fromdate, todate, txtSVoucher, searchinfo, "", "", "", "", "");
            if (ds1 == null)
                return;

            //For Grouping
            DataTable dtr = new DataTable();
            string RptGroup = this.rbtnGroup.SelectedItem.Text.Trim();
            DataView dv1 = new DataView();
            switch (RptGroup)
            {
                case "Receipt":
                    dv1 = ds1.Tables[0].DefaultView;
                    dv1.RowFilter = ("grp1 = 'A'");
                    dtr = dv1.ToTable();
                    this.lblReceiptCash.Visible = true;
                    this.lblPaymentCash.Visible = false;
                    this.lblDetailsCash.Visible = false;

                    break;
                case "Payment":
                    dv1 = ds1.Tables[0].DefaultView;
                    dv1.RowFilter = ("grp1 = 'B'");
                    dtr = dv1.ToTable();
                    this.lblReceiptCash.Visible = false;
                    this.lblPaymentCash.Visible = true;
                    this.lblDetailsCash.Visible = false;
                    break;

                case "Both":
                    dv1 = ds1.Tables[0].DefaultView;
                    dv1.RowFilter = (this.ddlVoucharCash.SelectedValue == "ALL Voucher") ? ("grp1 = 'A' or grp1 = 'B'  or grp1 = 'C'  ") : ("grp1 = 'A' or grp1 = 'B' ");
                    dtr = dv1.ToTable();
                    this.lblReceiptCash.Visible = true;
                    this.lblPaymentCash.Visible = true;
                    this.lblDetailsCash.Visible = (this.ddlVoucharCash.SelectedValue == "ALL Voucher");
                    break;
            }

            /////////

            Session["cashbank"] = dtr;




            DataView dvr = new DataView();
            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1 = 'A'");
            DataTable dtr1 = HiddenSameDate(dvr.ToTable());
            this.gvcashbook.DataSource = dtr1;
            this.gvcashbook.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (RptGroup == "Receipt")
            {
                ((HyperLink)this.gvcashbook.HeaderRow.FindControl("hlbtnCBdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
            this.FooterCalculation(dtr1, "gvcashbook");
            Session["Report1"] = gvcashbook;
            if (dtr1.Rows.Count > 0)
                ((HyperLink)this.gvcashbook.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='B'");
            DataTable dtr2 = HiddenSameDate(dvr.ToTable());
            this.gvcashbookp.DataSource = dtr2;
            this.gvcashbookp.DataBind();

            if (RptGroup == "Payment")
            {
                ((HyperLink)this.gvcashbookp.HeaderRow.FindControl("hlbtnCBPdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }


            this.FooterCalculation(dtr2, "gvcashbookp");
            Session["Report1"] = gvcashbookp;
            if (dtr2.Rows.Count > 0)
                ((HyperLink)this.gvcashbookp.HeaderRow.FindControl("hlbtnCBPdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='C'");
            DataTable dtr3 = dvr.ToTable();
            this.gvcashbookDB.DataSource = dvr.ToTable(); ;
            this.gvcashbookDB.DataBind();
            this.FooterCalculation(dtr3, "gvcashbookDB");
        }

        private void ReceiptAndPayment()
        {
            Session.Remove("recandpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comcod = hst["comcod"].ToString();
            string rp = (this.Request.QueryString["TrMod"] == "RecPay") ? "RP" : "";
            string CBorBoth = (this.rbtnGroupRP.SelectedIndex == 0) ? "C" : (this.rbtnGroupRP.SelectedIndex == 1) ? "B" : "";
            string net = this.chknet.Checked ? "Net" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RP_COMPANY_04", fromdate, todate, rp, CBorBoth, net, "", "", "", "");
            if (ds1 == null)
                return;

            Session["recandpay"] = this.HiddenSameDate(ds1.Tables[0]);
            Session["recandpayFo"] = ds1.Tables[1];
            ViewState["recandpayNote"] = ds1.Tables[2];

            this.gvrecandpay.DataSource = ds1.Tables[0];
            this.gvrecandpay.DataBind();
            this.RPNote();

            for (int i = 0; i < gvrecandpay.Rows.Count; i++)
            {
                string recpcode = ((Label)gvrecandpay.Rows[i].FindControl("lblgvrecpcode")).Text.Trim();
                string paycode = ((Label)gvrecandpay.Rows[i].FindControl("lblgvpaycode")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvrecandpay.Rows[i].FindControl("btnRecDesc");
                LinkButton lbtn2 = (LinkButton)gvrecandpay.Rows[i].FindControl("btnPayDesc");
                if (lbtn1 != null)
                {
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = recpcode;
                }
                if (lbtn2 != null)
                {
                    if (lbtn2.Text.Trim().Length > 0)
                        lbtn2.CommandArgument = paycode;
                }
            }


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.gvrecandpay.HeaderRow.FindControl("hlbtnRcvPayCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            this.FooterCalculation(ds1.Tables[0], "gvrecandpay");
            ds1.Dispose();
            Session["Report1"] = gvrecandpay;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((HyperLink)this.gvrecandpay.HeaderRow.FindControl("hlbtnRcvPayCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                //((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;

            }
        }
        private void RPNote()
        {
            this.PanelNote.Visible = true;
            DataTable dt = (DataTable)ViewState["recandpayNote"];
            this.gvbankbal.DataSource = dt;
            this.gvbankbal.DataBind();

            //this.lblPaid.Text = Convert.ToDouble(dt.Rows[0]["payamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblInPaid.Text = Convert.ToDouble(dt.Rows[0]["ipayamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblSodPaid.Text = Convert.ToDouble(dt.Rows[0]["sodpayamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblTPaid.Text = Convert.ToDouble(dt.Rows[0]["tpayamt"]).ToString("#,##0;(#,##0) ;");

            //this.lblNet.Text = Convert.ToDouble(dt.Rows[0]["netamt"]).ToString("#,##0;(#,##0) ;");

        }

        private void ShowIssuedVsColl()
        {

            Session.Remove("recandpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "ISSUEDVSCOLLECTION", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["recandpay"] = ds1.Tables[0]; ;
            this.gvarecandpay.DataSource = ds1.Tables[0];
            this.gvarecandpay.DataBind();
            this.FooterCalculation(ds1.Tables[0], "gvarecandpay");
            ds1.Dispose();
            Session["Report1"] = this.gvarecandpay;
            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.gvarecandpay.HeaderRow.FindControl("hlbtnacRcvPayCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";




        }

        private void ShowDailyProposal()
        {

            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTTRANSPROPOSAL", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            DataTable dtr1 = HiddenSameDate(dtr);
            this.gvDailPayPro.DataSource = dtr1;
            this.gvDailPayPro.DataBind();
            Session["tranlist"] = dtr1;
            this.FooterCalculation(dtr1, "gvDailPayPro");


        }

        private void ShowDeletedtransaction()
        {
            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtSVoucher = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTDELTRANSACTIONS", fromdate, todate, txtSVoucher, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            DataTable dtr1 = HiddenSameDate(dtr);
            this.gvdtranlsit.DataSource = dtr1;
            this.gvdtranlsit.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //((HyperLink)this.gvdtranlsit.HeaderRow.FindControl("hlbtnbtbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["tranlist"] = dtr1;
            this.FooterCalculation(dtr1, "gvdtranlsit");



        }


        private void ShowpProTransaction()
        {
            Session.Remove("tranlist");

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlAccHead.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "RPTPROTRANSACTION", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dtr = this.HiddenSameDate(ds1.Tables[0]);
            Session["tranlist"] = dtr;
            this.gvPtotranlsit.DataSource = dtr;
            this.gvPtotranlsit.DataBind();
            this.FooterCalculation(dtr, "gvPtotranlsit");
            Session["Report1"] = gvPtotranlsit;
            if (dtr.Rows.Count > 0)
                ((HyperLink)this.gvPtotranlsit.HeaderRow.FindControl("hlbtnbtbCdataExelp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }

        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();
            double frecamt = 0, fpayamt1 = 0, netbal;
            DataView dv1; DataTable dt1;

            switch (GvName)
            {
                case "gvcashbook":
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvCashAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                            0 : dt.Compute("sum(casham)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvFBankAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                          0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    break;


                case "gvcashbookp":
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvCashAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                             0 : dt.Compute("sum(casham)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvFBankAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                           0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    break;

                case "gvcashbookDB":
                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                             0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0.00;(#,##0.00) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lblgvFrecam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(depam)", "")) ?
                                   0 : dt.Compute("sum(depam)", ""))).ToString("#,##0.00;(#,##0.00) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                             0 : dt.Compute("sum(payam)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFClAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                           0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    break;

                case "gvtranlsit":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acrescode not in('" + "  Total Amt:" + "')");
                    dt = dv.ToTable();
                    ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    Dtdram = Convert.ToDouble("0" + ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text);
                    Dtcram = Convert.ToDouble("0" + ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text);
                    break;


                case "gvdtranlsit":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acrescode not in('" + "  Total Amt:" + "')");
                    dt = dv.ToTable();
                    ((Label)this.gvdtranlsit.FooterRow.FindControl("lgvdFDram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    ((Label)this.gvdtranlsit.FooterRow.FindControl("txtgvdFCram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    Dtdram = Convert.ToDouble("0" + ((Label)this.gvdtranlsit.FooterRow.FindControl("lgvdFDram")).Text);
                    Dtcram = Convert.ToDouble("0" + ((Label)this.gvdtranlsit.FooterRow.FindControl("txtgvdFCram")).Text);
                    break;



                case "gvrecandpay":

                    dv1 = dt.Copy().DefaultView;
                    dv1.RowFilter = ("recpcode like '%BBBBAAAAAAAA%' or paycode like '%BBBBAAAAAAAA%'");
                    dt1 = dv1.ToTable();
                    //dt1 = (DataTable)Session["recandpayFo"];

                    frecamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recpam)", "")) ?
                           0 : dt1.Compute("sum(recpam)", "")));
                    //((Label)this.gvrecandpay.FooterRow.FindControl("lblgvFrecpam")).Text = frecamt.ToString("#,##0;(#,##0) ;");
                    fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                         0 : dt1.Compute("sum(payam)", "")));

                    //((Label)this.gvrecandpay.FooterRow.FindControl("lgvFpayam1")).Text = fpayamt1.ToString("#,##0;(#,##0) ;");
                    netbal = frecamt - fpayamt1;

                    //((Label)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text = netbal.ToString("#,##0;(#,##0) ;");

                    ((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text = netbal.ToString("#,##0.00;(#,##0.00) ;");




                    break;

                case "gvDailPayPro":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acrescode not in('" + "  Total Amt:" + "')");
                    dt = dv.ToTable();
                    ((Label)this.gvDailPayPro.FooterRow.FindControl("lgvFDramPPro")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0.00;(#,##0.00) ;");


                    Dtdram = Convert.ToDouble("0" + ((Label)this.gvDailPayPro.FooterRow.FindControl("lgvFDramPPro")).Text);
                    break;



                case "gvarecandpay":
                    frecamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recpam)", "")) ?
                           0 : dt.Compute("sum(recpam)", "")));
                    ((Label)this.gvarecandpay.FooterRow.FindControl("lblgvFrecpamac")).Text = frecamt.ToString("#,##0.00;(#,##0.00) ;");
                    fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                         0 : dt.Compute("sum(payam)", "")));

                    ((Label)this.gvarecandpay.FooterRow.FindControl("lgvFpayamac")).Text = fpayamt1.ToString("#,##0.00;(#,##0.00) ;");
                    netbal = frecamt - fpayamt1;

                    ((Label)this.gvarecandpay.FooterRow.FindControl("lgvFNetBalanceac")).Text = (frecamt - fpayamt1).ToString("#,##0.00;(#,##0.00) ;");
                    break;


                case "gvPtotranlsit":

                    ((Label)this.gvPtotranlsit.FooterRow.FindControl("lgvFDramp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                           0 : dt.Compute("sum(dram)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    ((Label)this.gvPtotranlsit.FooterRow.FindControl("lgvFCramp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0.00;(#,##0.00) ;");

                    break;




            }


        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Date1, vounum;
            int j;
            if (this.rbtnList1.SelectedIndex == 0)
            {

                Date1 = dt1.Rows[0]["voudat1"].ToString();
                vounum = dt1.Rows[0]["vounum1"].ToString();
                for (j = 1; j < dt1.Rows.Count; j++)
                {
                    if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                    {
                        vounum = dt1.Rows[j]["vounum1"].ToString();
                        dt1.Rows[j]["vounum1"] = "";
                        dt1.Rows[j]["voudat1"] = "";
                        dt1.Rows[j]["vounar"] = "";


                    }

                    else
                    {
                        vounum = dt1.Rows[j]["vounum1"].ToString();
                    }

                }


            }


            else if (this.rbtnList1.SelectedIndex == 7)
            {

                Date1 = dt1.Rows[0]["voudat"].ToString();
                vounum = dt1.Rows[0]["vounum"].ToString();
                for (j = 1; j < dt1.Rows.Count; j++)
                {
                    if (dt1.Rows[j]["voudat"].ToString() == Date1 && dt1.Rows[j]["vounum"].ToString() == vounum)
                    {
                        dt1.Rows[j]["vounum1"] = "";
                        dt1.Rows[j]["voudat1"] = "";
                    }

                    else
                    {
                        if (dt1.Rows[j]["vounum"].ToString() == vounum)
                            dt1.Rows[j]["vounum1"] = "";

                        if (dt1.Rows[j]["voudat"].ToString() == Date1)
                            dt1.Rows[j]["voudat1"] = "";
                    }

                    Date1 = dt1.Rows[j]["voudat"].ToString();
                    vounum = dt1.Rows[j]["vounum"].ToString();


                }




            }


            else if (this.rbtnList1.SelectedIndex == 4)
            {
                string grp1 = dt1.Rows[0]["grp1"].ToString();
                for (j = 1; j < dt1.Rows.Count; j++)
                {
                    if (dt1.Rows[j]["grp1"].ToString() == grp1)
                    {
                        dt1.Rows[j]["grprpdesc"] = "";
                        dt1.Rows[j]["grppaydesc"] = "";
                    }


                    grp1 = dt1.Rows[j]["grp1"].ToString();


                }




            }




            else
            {
                Date1 = dt1.Rows[0]["voudat1"].ToString();
                vounum = dt1.Rows[0]["vounum1"].ToString();
                for (j = 1; j < dt1.Rows.Count; j++)
                {
                    if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                    {
                        vounum = dt1.Rows[j]["vounum1"].ToString();
                        dt1.Rows[j]["vounum1"] = "";
                        dt1.Rows[j]["voudat1"] = "";


                    }

                    else
                    {
                        vounum = dt1.Rows[j]["vounum1"].ToString();
                    }

                }
            }

            return dt1;

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.PrintCashBook();
                    break;

                case 1:
                    this.PrintTransaction();
                    break;

                case 2:
                    this.PrintDailyProposal();
                    break;

                case 3:
                    this.PrintDelTransaction();
                    break;

                case 4:
                case 5:
                    this.PrintReceiveAndPayment();
                    break;

                case 6:
                    this.PrintIssuedVsCollection();
                    break;

                case 7:
                    this.PrintProTransaction();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblprintstk")).ToString();
                string eventdesc = "Print Report";
                string eventdesc2 = rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        private void PrintCashBook()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable dt = HiddenSameDate((DataTable)Session["cashbank"]);
            //ReportDocument rptcb1 = new RMGiRPT.R_21_GAcc.RptAccCashbook1();
            ////TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            ////rptCname.Text = comnam;
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintTransaction()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            DataTable dt = (DataTable)Session["tranlist"];
            //ReportDocument rptdtlist = new RMGiRPT.R_21_GAcc.RptDailyTransaction();
            //TextObject rptCname = rptdtlist.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptdate = rptdtlist.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

            //TextObject rptdram = rptdtlist.ReportDefinition.ReportObjects["txtdram"] as TextObject;
            //rptdram.Text = Dtdram.ToString("#,##0;(#,##0); ");
            //TextObject rptcram = rptdtlist.ReportDefinition.ReportObjects["txtcram"] as TextObject;
            //rptcram.Text = Dtcram.ToString("#,##0;(#,##0); ");

            //TextObject txtuserinfo = rptdtlist.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptdtlist.SetDataSource(dt);
            //Session["Report1"] = rptdtlist;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        private void PrintReceiveAndPayment()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["recandpay"];

            ReportDocument rptrandpay = new RMGiRPT.R_21_GAcc.RptRecAndPayment();
            TextObject rptCname = rptrandpay.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rpttxtHeaderTitle = rptrandpay.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            rpttxtHeaderTitle.Text = (this.Request.QueryString["TrMod"] == "RecPay") ? "RECEIPTS & PAYMENTS" : "FUND FLOW";
            TextObject rptdate = rptrandpay.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";


            DataTable dt1 = (DataTable)ViewState["recandpayNote"];



            TextObject rptxtRec = rptrandpay.ReportDefinition.ReportObjects["txtRec"] as TextObject;
            //rptxtRec.Text = ((Label)(this.gvrecandpay.FooterRow.FindControl("lblgvFrecpamac"))).Text.Trim();
            TextObject rptxtPay = rptrandpay.ReportDefinition.ReportObjects["txtPay"] as TextObject;
            rptxtPay.Text = ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFpayam1")).Text.Trim();

            TextObject rptopenbal = rptrandpay.ReportDefinition.ReportObjects["txtNetBalance"] as TextObject;
            rptopenbal.Text = ((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text.Trim();


            TextObject txtuserinfo = rptrandpay.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptrandpay.Subreports["RptBankBalance02.rpt"].SetDataSource(dt1);
            rptrandpay.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptrandpay.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptrandpay;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        private void PrintIssuedVsCollection()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable dt = (DataTable)Session["recandpay"];
            //ReportDocument rptrandpay = new RMGiRPT.R_21_GAcc.RptRecAndPaymentActual();
            //TextObject rptCname = rptrandpay.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            ////TextObject rpttxtHeaderTitle = rptrandpay.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            ////rpttxtHeaderTitle.Text = "Issued Vs. Collection";
            //TextObject rptdate = rptrandpay.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

            //TextObject rptopenbal = rptrandpay.ReportDefinition.ReportObjects["txtNetBalance"] as TextObject;
            //rptopenbal.Text = ((Label)this.gvarecandpay.FooterRow.FindControl("lgvFNetBalanceac")).Text.Trim();
            //TextObject txtuserinfo = rptrandpay.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptrandpay.SetDataSource(dt);
            //Session["Report1"] = rptrandpay;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintDailyProposal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            DataTable dt = (DataTable)Session["tranlist"];
            //ReportDocument rptdtlist = new RMGiRPT.R_21_GAcc.RptDailyPayProposal();
            //TextObject rptCname = rptdtlist.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptdate = rptdtlist.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

            //TextObject rptdram = rptdtlist.ReportDefinition.ReportObjects["txtdram"] as TextObject;
            //rptdram.Text = Dtdram.ToString("#,##0;(#,##0); ");


            //TextObject txtuserinfo = rptdtlist.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptdtlist.SetDataSource(dt);
            //Session["Report1"] = rptdtlist;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }

        private void PrintDelTransaction()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            DataTable dt = (DataTable)Session["tranlist"];
            //ReportDocument rptdtlist = new RMGiRPT.R_21_GAcc.RptDelDailyTransaction();
            //TextObject rptCname = rptdtlist.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptdate = rptdtlist.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

            //TextObject rptdram = rptdtlist.ReportDefinition.ReportObjects["txtdram"] as TextObject;
            //rptdram.Text = Dtdram.ToString("#,##0;(#,##0); ");
            //TextObject rptcram = rptdtlist.ReportDefinition.ReportObjects["txtcram"] as TextObject;
            //rptcram.Text = Dtcram.ToString("#,##0;(#,##0); ");

            //TextObject txtuserinfo = rptdtlist.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptdtlist.SetDataSource(dt);
            //Session["Report1"] = rptdtlist;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintProTransactionold()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tranlist"];
            //ReportDocument rptdtlist = new RMGiRPT.R_21_GAcc.RptProTransaction();
            //TextObject rptCname = rptdtlist.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtProjectName = rptdtlist.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = "Date Wise Transaction - " + this.ddlAccHead.SelectedItem.Text.Trim().Substring(13);

            //TextObject rptdate = rptdtlist.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";



            //TextObject txtuserinfo = rptdtlist.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptdtlist.SetDataSource(dt);
            //Session["Report1"] = rptdtlist;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintProTransaction()
        {

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Acchead = "Accounts Head:- " + this.ddlAccHead.SelectedItem.Text.Trim().Substring(13);
            string dateft = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string LCname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            //string fdate = this.txtDatefrom.Text.ToString();
            //string tdate = this.txtdateto.Text.ToString();
            //string ToFrDate = "(From :" + fdate + " To " + tdate + ")";

            DataTable dt = (DataTable)Session["tranlist"];


            //var lst = ds.Tables[0].DataTableToList<SPEENTITY.C_09_C>();
            var lst = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccVoucher.AccDTransaction>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptDaliyTrans", lst, null, null);
            //rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("dateft", dateft));
            rpt1.SetParameters(new ReportParameter("Acchead", Acchead));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Daily Transaction"));
            //rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void gvtranlsit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label acrescode = (Label)e.Row.FindControl("lblgvAcRsCode");
                Label acresdesc = (Label)e.Row.FindControl("lblgvAcRsDesc");
                Label lbldram = (Label)e.Row.FindControl("lgvDram");
                Label lblcramt = (Label)e.Row.FindControl("txtgvCram");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acrescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "  Total Amt:")
                {

                    acrescode.Font.Bold = true;
                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    lblcramt.Font.Bold = true;
                    //lblcramt.Style.Add("text-transform", "initcap");



                }

            }
        }
        protected void gvDailPayPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label acrescode = (Label)e.Row.FindControl("lblgvAcRsCodePPro");
                Label acresdesc = (Label)e.Row.FindControl("lblgvAcRsDescPPro");
                Label lbldram = (Label)e.Row.FindControl("lgvDramPPro");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acrescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "  Total Amt:")
                {

                    acrescode.Font.Bold = true;
                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;


                }

            }



        }
        protected void gvdtranlsit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label acrescode = (Label)e.Row.FindControl("lblgvdAcRsCode");
                Label acresdesc = (Label)e.Row.FindControl("lblgvdAcRsDesc");
                Label lbldram = (Label)e.Row.FindControl("lgvdDram");
                Label lblcramt = (Label)e.Row.FindControl("txtgvdCram");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acrescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "  Total Amt:")
                {

                    acrescode.Font.Bold = true;
                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    lblcramt.Font.Bold = true;

                }

            }
        }
        protected void gvtranlsit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.SessionUpdate2();
            this.gvtranlsit.PageIndex = e.NewPageIndex;
            this.TransactionList();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TransactionList();
            //this.gvtranlsit_DataBind();
        }
        //protected void dgv3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.SessionUpdate2();
        //    this.gvtranlsit.PageIndex = e.NewPageIndex;
        //    this.gvtranlsit_DataBind();
        //}
        protected void gvdtranlsit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void imgbtnSearchVoucher_Click(object sender, EventArgs e)
        {
            this.TransactionList();
        }

        protected void imgbtnSearchVoucherCash_Click(object sender, EventArgs e)
        {
            this.ShowCashBook();
        }

        protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.lblTo.Visible = (this.ddlSrch.SelectedValue == "between");
            this.txtAmount2.Visible = (this.ddlSrch.SelectedValue == "between");
        }
        protected void ddlSrchCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblToCash.Visible = (this.ddlSrchCash.SelectedValue == "between");
            this.txtAmountC2.Visible = (this.ddlSrchCash.SelectedValue == "between");
        }
        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;

                    break;
                case 1:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;

                    this.lblVoucher.Visible = true;
                    this.ddlVouchar.Visible = true;
                    break;

                case 2:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
                    break;
                case 3:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
                    break;
                case 4:
                case 5:
                    this.MultiView1.ActiveViewIndex = 4;
                    break;


                case 6:
                    this.MultiView1.ActiveViewIndex = 5;
                    break;
                case 7:
                    this.MultiView1.ActiveViewIndex = 6;
                    break;
            }
        }

        protected void btnRecDesc_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string recpcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["recandpay"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "recpcode like('" + recpcode + "')";
            dt = dv1.ToTable();

            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtfromdate.Text;
            string mTRNDAT2 = this.txttodate.Text;
            string mACTCODE = dt.Rows[0]["recpcode"].ToString();
            string mACTDESC = dt.Rows[0]["recpdesc"].ToString();
            string lebel2 = dt.Rows[0]["rleb2"].ToString();
            if (mACTCODE == "")
            {
                return;
            }

            ///---------------------------------//// 
            if (ASTUtility.Left(mACTCODE, 1) == "4")
            {


                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=PrjReportRP&actcode=" +
                                mACTCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }
            else if (lebel2 == "")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=RPschedule&comcod=" + mCOMCOD + "&actcode=" +
                            mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=detailsTBRP&comcod=" + mCOMCOD + "&actcode=" +
                            mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }



        }
        protected void btnPayDesc_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string paycode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["recandpay"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "paycode like('" + paycode + "')";
            dt = dv1.ToTable();

            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtfromdate.Text;
            string mTRNDAT2 = this.txttodate.Text;
            string mACTCODE = dt.Rows[0]["paycode"].ToString();
            string mACTDESC = dt.Rows[0]["paydesc"].ToString();
            string lebel2 = dt.Rows[0]["pleb2"].ToString();
            if (mACTCODE == "")
            {
                return;
            }

            ///---------------------------------//// 

            if (ASTUtility.Left(mACTCODE, 1) == "4")
            {


                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=PrjReportRP&actcode=" +
                                mACTCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=P" + "', target='_blank');</script>";
            }
            else if (lebel2 == "")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=RPschedule&comcod=" + mCOMCOD + "&actcode=" +
                                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=P" + "', target='_blank');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=detailsTBRP&comcod=" + mCOMCOD + "&actcode=" +
                                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=P" + "', target='_blank');</script>";
            }
        }
        protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        {
            this.GetAccCode();

        }
        protected void gvrecandpay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton HyRecDesc = (LinkButton)e.Row.FindControl("btnRecDesc");
                Label lgvRecAmt = (Label)e.Row.FindControl("lblgvrecpam");

                LinkButton HyPayDesc = (LinkButton)e.Row.FindControl("btnPayDesc");
                Label lgvPayAmt = (Label)e.Row.FindControl("lgvpayam1");


                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recpcode")).ToString();
                string code2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paycode")).ToString();

                if (code1 == "" && code2 == "")
                {
                    return;
                }

                if (ASTUtility.Right(code1, 8) == "00000000" || ASTUtility.Right(code1, 8) == "AAAAAAAA")
                {

                    HyRecDesc.Font.Bold = true;
                    lgvRecAmt.Font.Bold = true;
                }
                if (ASTUtility.Right(code2, 8) == "00000000" || ASTUtility.Right(code1, 8) == "AAAAAAAA")
                {
                    HyPayDesc.Font.Bold = true;
                    lgvPayAmt.Font.Bold = true;
                }

            }


        }
        protected void gvbankbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label ActDesc = (Label)e.Row.FindControl("lgcActDescbb");
                Label opnam = (Label)e.Row.FindControl("lgvopnambb");
                Label closam = (Label)e.Row.FindControl("lgvclosambb");
                HyperLink balam = (HyperLink)e.Row.FindControl("hlnkgvbalambb");

                //((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "code")).ToString();
                double netbal = Convert.ToDouble((DataBinder.Eval(e.Row.DataItem, "netbal")).ToString());


                if (code == "")
                {
                    return;
                }


                if (ASTUtility.Right(code, 3) == "AAA")
                {


                    ActDesc.Font.Bold = true;
                    opnam.Font.Bold = true;
                    closam.Font.Bold = true;
                    balam.Font.Bold = true;
                    ActDesc.Style.Add("text-align", "right");

                }
                if (code == "003AAA" && netbal != 0.00)
                {
                    balam.Font.Bold = true;
                    balam.Style.Add("color", "blue");
                    balam.NavigateUrl = "~/F_21_GAcc/LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;


                }





            }
        }
    }
}