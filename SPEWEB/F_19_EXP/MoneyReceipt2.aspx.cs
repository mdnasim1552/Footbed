using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_19_Exp;

namespace SPEWEB.F_19_EXP
{
    public partial class MoneyReceipt2 : System.Web.UI.Page
    {

        ProcessAccess proc1 = new ProcessAccess();
        public static double ToCost, OrdrVal, toqty, ToCostPer, ToCostPerM, totalcmPer;
        UserManReceiptaPayment objUserMan = new UserManReceiptaPayment();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetBuyer();
                this.PayType();

                this.GetBankList();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Collection Entry";
                this.txtEntryDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtChqDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.CommonButton();
                if (this.Request.QueryString["genno"].Length > 0)
                {
                    //string mrno = this.Request.QueryString ["genno"].ToString();
                    //string centrid = this.Request.QueryString ["centrid"].ToString();
                    //GetPrevData(mrno, centrid);
                }
                else
                {
                    // this.GetMrNo();
                    this.CreateTable();

                }
                if (this.Request.QueryString["genno"].Length != 0)
                {


                    this.lbtnOk_Click(null, null);
                }
                CurrencyInf();
                

            }
        }

        private void GetPrevData(string mrno, string centrid)
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_REPORT_EXPORT", "GETPAYMENTINFO", mrno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            DataTable dt = (DataTable)ds1.Tables[1];
            var lst = dt.DataTableToList<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>();

            ViewState["tblacinfo"] = ds1.Tables[0];
            ViewState["tblcollection"] = lst;
            ViewState["dtbankdata"] = ds1.Tables[2];
            if (this.Request.QueryString["genno"] != "")
            {
                ViewState["tblAdv"] = ds1.Tables[3];
            }

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.txtissueno.Text = ds1.Tables[0].Rows[0]["mrslno"].ToString().Substring(6, 2) + "-" + ASTUtility.Right(ds1.Tables[0].Rows[0]["mrslno"].ToString(), 3);
            this.txtBranch.Text = ds1.Tables[0].Rows[0]["bbranch"].ToString();
            this.txtEntryDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            this.txtChqDate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["paydat"]).ToString("dd-MMM-yyyy");
            this.txtrefnum.Text = ds1.Tables[0].Rows[0]["chqno"].ToString();
            this.txtNar2.Text = ds1.Tables[0].Rows[0]["remarks"].ToString();
            this.txtothref.Text = ds1.Tables[0].Rows[0]["refno"].ToString();
            this.ddlBuyer.SelectedValue = ds1.Tables[0].Rows[0]["rescode"].ToString();

            this.ddlCurrency.SelectedValue = ds1.Tables[0].Rows[0]["curcode"].ToString();
            this.ddlCurrency.Enabled = false;
            this.ddlBatchGrp.SelectedValue = ds1.Tables[0].Rows[0]["paytype"].ToString();
            this.ddlBankList.SelectedValue = ds1.Tables[0].Rows[0]["cactcode"].ToString();

            this.ddlBankList.Enabled = false;
            this.txtrefnum.ReadOnly = true;
            this.txtBranch.ReadOnly = true;
            this.txtChqDate.ReadOnly = true;
            this.txtEntryDate.ReadOnly = true;


            this.Data_Bind();
            this.Bank_Data_Bind();



        }
        private string GetMrNo()
        {
            string Saledate = this.txtEntryDate.Text.Substring(0, 11);
            string SactNo = this.ddlmlccode.SelectedValue.ToString();
            string comcod = this.GetComeCode();
            DataSet ds3 = proc1.GetTransInfo(comcod, "SP_REPORT_EXPORT", "GETNEWMRNO", Saledate, "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                return "";
            }
            this.txtissueno.Text = ds3.Tables[0].Rows[0]["mrno"].ToString().Substring(6, 2) + "-" + ASTUtility.Right(ds3.Tables[0].Rows[0]["mrno"].ToString(), 3);

            this.ddlPreMrr.DataTextField = "mrno";
            this.ddlPreMrr.DataValueField = "mrno";
            this.ddlPreMrr.DataSource = ds3.Tables[0];
            this.ddlPreMrr.DataBind();
            ds3.Dispose();


            return (ds3.Tables[0].Rows[0]["mrno"].ToString());

        }
        private void CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("actcode", Type.GetType("System.String"));
            dt.Columns.Add("actdesc", Type.GetType("System.String"));
            dt.Columns.Add("rescode", Type.GetType("System.String"));
            dt.Columns.Add("resdesc1", Type.GetType("System.String"));
            dt.Columns.Add("fcamt", Type.GetType("System.Double"));
            dt.Columns.Add("bdtamt", Type.GetType("System.Double"));
            dt.Columns.Add("trnrmrk", Type.GetType("System.String"));
            dt.Columns.Add("convrate", Type.GetType("System.Double"));
            dt.Columns.Add("lgamt", Type.GetType("System.Double"));
            dt.Columns.Add("tamt", Type.GetType("System.Double"));
            dt.Columns.Add("aitamt", Type.GetType("System.Double"));
            dt.Columns.Add("commamt", Type.GetType("System.Double"));
            dt.Columns.Add("othcharge", Type.GetType("System.Double"));


            ViewState["dtbankdata"] = dt;
        }
        private void GetBuyer()
        {
            string comcod = this.GetComeCode();
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");
            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds2.Tables[0];
            this.ddlBuyer.DataBind();
            if (this.Request.QueryString["sircode"].ToString() != "")
            {
                this.ddlBuyer.SelectedValue = this.Request.QueryString["sircode"].ToString();


            }
            this.ddlBuyer_SelectedIndexChanged(null, null);
        }
        protected void lnkAcccode_Click(object sender, EventArgs e)
        {
            //List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead> lst = (List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassAccHead>)Session["HeadAcc1"];

            DataTable dt = (DataTable)ViewState["tblBank"];


            this.ddlacccode.DataTextField = "actdesc";
            this.ddlacccode.DataValueField = "actcode";
            this.ddlacccode.DataSource = dt;
            this.ddlacccode.DataBind();

            //ddlacccode_SelectedIndexChanged(null, null);
        }

        protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLCCode();
            this.GetInvCode();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);

            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

            //  ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text= "Calculation";


        }

        private void PayType()
        {

            string comcod = this.GetComeCode();
            DataSet ds4 = proc1.GetTransInfo(comcod, "SP_REPORT_EXPORT", "PAYTYPE", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                return;
            }
            this.ddlBatchGrp.DataTextField = "gdesc";
            this.ddlBatchGrp.DataValueField = "gcod";
            this.ddlBatchGrp.DataSource = ds4.Tables[0];
            this.ddlBatchGrp.DataBind();
            ds4.Dispose();
            //if (this.ddlBatchGrp.SelectedItem.Text == "CASH" || this.ddlBatchGrp.SelectedItem.Text == "cash")
            //{
            //    this.NotCash.Visible = false;
            //}
            //else
            //{
            //    this.NotCash.Visible = true;
            //}

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetLCCode()
        {
            string comcod = this.GetComeCode();
            string filter = "%";
            string Buyer = this.ddlBuyer.SelectedValue.ToString();

            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_INV_STDANA", "GETORDERMLCCOD", filter, Buyer, "", "", "", "", "", "");
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            ViewState["tbllcorder"] = lst;
            this.ddlmlccode.DataSource = lst.Select(m => new { m.mlccod, m.mlcdesc }).Distinct().ToList();
            this.ddlmlccode.DataTextField = "mlcdesc";
            this.ddlmlccode.DataValueField = "mlccod";
            this.ddlmlccode.DataBind();

            if (Request.QueryString["centrid"].ToString() != "")
            {
                // this.ddlmlccode.SelectedValue = "16"+this.Request.QueryString["centrid"].ToString().Substring(2,10);            
                //    this.ddlmlccode.Enabled = false;
            }
            ds2.Dispose();
            
        }

        private void GetInvCode()
        {

            //string mlccode = "31" + this.ddlmlccode.SelectedValue.ToString().Substring(2, 10);
            try
            {
                string mrno = this.Request.QueryString["genno"].Length > 0 ? this.Request.QueryString["genno"].ToString() : "00000000000000";

                string comcod = this.GetComeCode();
                string buyerid = this.ddlBuyer.SelectedValue.ToString() + "%";
                DataSet ds2 = proc1.GetTransInfo(comcod, "SP_REPORT_EXPORT", "GETDEBTORBILL", "%",buyerid, "", "", "", "", "");
                if (this.Request.QueryString["genno"] == "")
                {
                    ViewState["tblAdv"] = ds2.Tables[1];
                    this.lblkbal.Text = (ds2.Tables[2].Rows.Count == 0) ? "0.00" : Convert.ToDouble(ds2.Tables[2].Rows[0]["trnam"]).ToString("#,##0 ;-#,##0; ") + " Tk.";

                }


                List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>();


                //List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst = objUserMan.GetDeborBill(mlccode);
                this.ddlInvno.DataTextField = "isunum1";
                this.ddlInvno.DataValueField = "isunum";
                this.ddlInvno.DataSource = lst;
                this.ddlInvno.DataBind();
                ViewState["tbldebbill"] = lst;

                //this.GetInvCode();

            }
            catch (Exception ex)
            {

            }
        }
        protected void LbtnAdvUpdate_Click(object sender, EventArgs e)
        {
            //string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            this.lblinvnomr.Text = ((Label)this.gvTransaction.Rows[index].FindControl("lblgvbillno")).Text.ToString();


            //DataSet result = accData.GetTransInfo(comcod, "SP_REPORT_WAREHOUSE_INTERFACE", "GETORDERLISTFORARRI", orderno);
            //if (result == null)
            //{
            //    return;
            //}

            DataTable dt = (DataTable)ViewState["tblAdv"];
            this.gvAdvDetails.DataSource = dt;
            this.gvAdvDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenAdvmodal();", true);
        }
        protected void ModalUpdateBtn_Click(object sender, EventArgs e)
        {


            DataTable dt = (DataTable)ViewState["tblAdv"];
            if (dt.Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < this.gvAdvDetails.Rows.Count; i++)
            {
                string chk = (((CheckBox)this.gvAdvDetails.Rows[i].FindControl("chkack")).Checked) ? "True" : "False";
                string invno = ((Label)this.gvAdvDetails.Rows[i].FindControl("lblgvinvno")).Text.Trim();
                double adjamt = Convert.ToDouble("0" + ((TextBox)this.gvAdvDetails.Rows[i].FindControl("txtgvadjamt")).Text.Trim());


                dt.Rows[i]["approved"] = chk;
                dt.Rows[i]["invno"] = (chk == "True") ? ((invno == "") ? this.lblinvnomr.Text : invno) : "";
                dt.Rows[i]["adjamt"] = adjamt;

                //((TextBox)this.gvAdvDetails.Rows[i].FindControl("adjamt")).Text = adjamt;
            }


            ViewState["tblAdv"] = dt;


        }
        private void GetBankList()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_REPORT_EXPORT", "GETBANKLIST", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            this.ddlBankList.DataTextField = "actdesc";
            this.ddlBankList.DataValueField = "actcode";
            this.ddlBankList.DataSource = ds1.Tables[0];
            this.ddlBankList.DataBind();
            this.ddlBankList.SelectedValue = "195100010001";
            this.ddlacccode.DataTextField = "actdesc";
            this.ddlacccode.DataValueField = "actcode";
            this.ddlacccode.DataSource = ds1.Tables[0];

            this.ddlacccode.DataBind();

            ViewState["tblBank"] = ds1.Tables[0];


        }
        private void ShowBillValue()
        {

            double toreceiptam = 0.00, receiptam = 0.00, fcbnkcharge = 0.00, fcadjamt = 0.00;
            double convrate = Convert.ToDouble("0" + this.lblConRate.Text);
            string curcode = this.ddlCurrency.SelectedValue.ToString();


            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst = ((List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)ViewState["tblcollection"]);
            if (lst == null)
            {
                return;
            }
            for (int i = 0; i < this.gvTransaction.Rows.Count; i++)
            {
                string chk = (((CheckBox)this.gvTransaction.Rows[i].FindControl("chkbill")).Checked) ? "True" : "False";

                //string tedt = ((TextBox)this.gvTransaction.Rows[i].FindControl("txtgvreceiptam")).Text.Trim();

                double billAmt = Convert.ToDouble("0" + ((Label)this.gvTransaction.Rows[i].FindControl("lblgvinvbdtamt")).Text.Replace("<i class=\"bold text-red\">৳</i>", "").Trim());
                receiptam = Convert.ToDouble("0" + ((TextBox)this.gvTransaction.Rows[i].FindControl("txtgvreceiptam")).Text.Trim());
                fcbnkcharge = Convert.ToDouble("0" + ((TextBox)this.gvTransaction.Rows[i].FindControl("lblgvsvatamt")).Text.Trim());

                fcadjamt = Convert.ToDouble("0" + ((TextBox)this.gvTransaction.Rows[i].FindControl("txtgvfcadjamt")).Text.Trim());


                double aitamt = Convert.ToDouble("0" + ((TextBox)this.gvTransaction.Rows[i].FindControl("txtgvaitamt")).Text.Trim());
                double comamt = Convert.ToDouble("0" + ((TextBox)this.gvTransaction.Rows[i].FindControl("txtgvcomamt")).Text.Trim());
                double othcharge = Convert.ToDouble("0" + ((TextBox)this.gvTransaction.Rows[i].FindControl("txtgvOtherCharges")).Text.Trim());

                double shortfallfc = Convert.ToDouble("0" + ((TextBox)this.gvTransaction.Rows[i].FindControl("lblgvShortFall")).Text.Trim());
                double overdueinter = Convert.ToDouble("0" + ((TextBox)this.gvTransaction.Rows[i].FindControl("txtgvOverDueInter")).Text.Trim());

                if (aitamt == 0)
                {
                    aitamt = (receiptam * convrate) * 0.01;
                }
                if (comamt == 0)
                {
                    comamt = (receiptam * convrate) * 0.0015;
                }


                //if (chk == "True")
                //{


                lst[i].chk = chk;
                double hbalam = lst[i].hbalam;
                // double receiptam = (hbalam > toreceiptam) ? toreceiptam : hbalam;
                double balam = lst[i].billam - (receiptam + fcadjamt);
                lst[i].receiptam = receiptam;
                lst[i].allocamt = receiptam;
                lst[i].fcadjamt = fcadjamt;
                lst[i].balam = balam;
                lst[i].vatamt = fcbnkcharge * convrate;
                lst[i].fcbnkcharge = fcbnkcharge;
                lst[i].bdtamount = receiptam * convrate;
                lst[i].adjamt = fcadjamt * convrate;
                lst[i].aitamt = aitamt;
                lst[i].commamt = comamt;
                lst[i].othcharge = othcharge;
                lst[i].shortfallfc = shortfallfc;
                lst[i].shortfallbdt = shortfallfc * convrate; ;
                lst[i].ovrdueintrst = overdueinter;


                double cslamt = (receiptam == 0.00) ? 0.00 : (billAmt - ((receiptam * convrate) + (fcbnkcharge * convrate)));//cglamt;
                billAmt = Math.Round(billAmt, 6);
                double creceiptam = Math.Round(receiptam * convrate, 6);
                double cfcbnkcharge = Math.Round(fcbnkcharge * convrate, 6);
                double fcadjamtbdt = Math.Round(fcadjamt * convrate, 6);
                lst[i].cglamt = (receiptam == 0.00) ? 0.00 : (billAmt - (creceiptam + cfcbnkcharge + fcadjamtbdt));//cglamt;
                toreceiptam = toreceiptam - receiptam;
                //}

            }


            ViewState["tblcollection"] = lst;
            this.Data_Bind();


        }
        protected void ddlBatchGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.ddlBatchGrp.SelectedValue.ToString() == "82002")
            //{
            //    this.NotCash.Visible = false;

            //}
            //else
            //{
            //    this.NotCash.Visible = true;

            //}
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

            this.ddlCurrency_SelectedIndexChanged(null, null);
        }
        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

            string tcode = "001";
            string fcode = this.ddlCurrency.SelectedValue.ToString();
            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            var List = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode)).ToList();

            double method = (List.Count > 0) ? List[0].conrate : 0;


            if (this.Request.QueryString["genno"].Length > 0)
            {
                DataTable dt = (DataTable)ViewState["tblacinfo"];
                if (dt == null || dt.Rows.Count == 0)
                    return;
                double txtConRate = Convert.ToDouble("0" + dt.Rows[0]["convrate"].ToString());
                this.lblConRate.Text = txtConRate.ToString("#,##0.000000 ;-#,##0.000000; ");
            }
            else
            {
                this.lblConRate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
            }

            //double txtpeople = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBudgeted.Rows[j].FindControl("txtpeople")).Text.Trim()));

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string buyername = this.ddlBuyer.SelectedItem.Text.ToString();
            string refno = this.ddltype.SelectedItem.Text.ToString() + txtissueno.Text.ToString();
            string date = this.txtChqDate.Text.ToString();
            string bankname = this.ddlBankList.SelectedItem.Text.ToString();
            string currency = this.ddlCurrency.SelectedItem.Text.ToString(); ;
            string currate = this.lblConRate.Text.ToString();
            DataTable dt = (DataTable)ViewState["dtbankdata"];

            var lst = (List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)ViewState["tblcollection"];

            var lst1 = dt.DataTableToList<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassBankData>();
            ///   EClassBankData
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptExportRealization", lst, lst1, null);


            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("buyername", "Buyer Name: " + buyername));
            rpt1.SetParameters(new ReportParameter("refno", "Ref. No: " + refno));

            rpt1.SetParameters(new ReportParameter("date", "Date: " + date));
            rpt1.SetParameters(new ReportParameter("bankname", "Bank Name: " + bankname));
            rpt1.SetParameters(new ReportParameter("currency", "Currency Name: " + currency));
            rpt1.SetParameters(new ReportParameter("currate", "Currency Rate: " + currate));


            rpt1.SetParameters(new ReportParameter("RptTitle", "Export Realization"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }



        private void Data_Bind()
        {
            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst = (List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)ViewState["tblcollection"];


            if (lst.Count == 0)
            {
                this.gvTransaction.DataSource = null;
                this.gvTransaction.DataBind();
                return;
            }

            this.gvTransaction.DataSource = HiddenSameData(lst);
            this.gvTransaction.DataBind();
            this.FooterCalculation(lst);
        }



        protected void Add_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst = (List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)ViewState["tblcollection"];

            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> tbl1 = (List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)ViewState["tbldebbill"];

            if (lst == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Press OK Button Before Add');", true);
               
                return;
            }

            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string invno = this.ddlInvno.SelectedValue.ToString();
            //  DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            var newlist = tbl1.FindAll(x => x.isunum == invno);
            foreach (SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill c1 in newlist)
            {

                string actcode = c1.actcode.ToString();
                string rescode = c1.rescode.ToString();
                string voudat = c1.voudat.ToString();
                string isunum = c1.isunum.ToString();
                string isunum1 = c1.isunum1.ToString();
                double billam = Convert.ToDouble(c1.billam);
                double hbalam = Convert.ToDouble(c1.hbalam);
                double balam = Convert.ToDouble(c1.balam);
                double receiptam = Convert.ToDouble(c1.receiptam);
                double allocamt = Convert.ToDouble(c1.allocamt);
                double vatamt = Convert.ToDouble(c1.vatamt);
                string chk = c1.chk.ToString();
                string curcod = c1.curcod.ToString();
                double convrate = Convert.ToDouble(c1.convrate);
                string curdesc = c1.curdesc.ToString();
                double bdtamount = Convert.ToDouble(c1.bdtamount);
                double fcbnkcharge = Convert.ToDouble(c1.fcbnkcharge);
                double invbdtamt = Convert.ToDouble(c1.invbdtamt);
                double cglamt = Convert.ToDouble(c1.cglamt);
                string invrefno = c1.invrefno;
                double colconrate = Convert.ToDouble("0" + this.lblConRate.Text);
                var checklist = lst.FindAll(p => p.isunum == invno && p.actcode == actcode);
                double fcadjamt = Convert.ToDouble(c1.fcadjamt);
                double adjamt = Convert.ToDouble(c1.adjamt);
                if (checklist.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Money Receipt Already Exist');", true);
                   
                    return;
                }

                lst.Add(new SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill(actcode, rescode, Convert.ToDateTime(voudat), isunum, isunum1, billam, hbalam, balam, receiptam, chk, allocamt, vatamt,
                   curcod, convrate, curdesc, bdtamount, fcbnkcharge, invbdtamt, cglamt, invrefno, colconrate, "False", fcadjamt, adjamt,0.00,0.00,0.00));
            }
            ViewState["tblcollection"] = lst;
            this.Data_Bind();
        }

        protected void AddAll_Click(object sender, EventArgs e)
        {
            // DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst = (List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)ViewState["tblcollection"];

            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> tbl1 = (List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)ViewState["tbldebbill"];

            if (lst == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Press OK Button Before Add');", true);

                return;
            }
            foreach (SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill c1 in tbl1)
            {
                string actcode = c1.actcode.ToString();
                string rescode = c1.rescode.ToString();
                string voudat = c1.voudat.ToString();
                string isunum = c1.isunum.ToString();
                string isunum1 = c1.isunum1.ToString();
                double billam = Convert.ToDouble(c1.billam);
                double hbalam = Convert.ToDouble(c1.hbalam);
                double balam = Convert.ToDouble(c1.balam);
                double receiptam = Convert.ToDouble(c1.receiptam);
                double allocamt = Convert.ToDouble(c1.allocamt);
                double vatamt = Convert.ToDouble(c1.vatamt);
                string chk = c1.chk.ToString();
                string curcod = c1.curcod.ToString();
                double convrate = Convert.ToDouble(c1.convrate);
                string curdesc = c1.curdesc.ToString();
                double bdtamount = Convert.ToDouble(c1.bdtamount);
                double fcbnkcharge = Convert.ToDouble(c1.fcbnkcharge);
                double invbdtamt = Convert.ToDouble(c1.invbdtamt);
                double cglamt = Convert.ToDouble(c1.cglamt);
                string invrefno = c1.invrefno;
                double colconrate = Convert.ToDouble("0" + this.lblConRate.Text);
                var checklist = lst.FindAll(p => p.isunum == c1.isunum && p.actcode == actcode);
                double fcadjamt = Convert.ToDouble(c1.fcadjamt);
                double adjamt = Convert.ToDouble(c1.adjamt);
                if (checklist.Count > 0)
                {
                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invoice Already Exist');", true);

                    return;
                }

                lst.Add(new SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill(actcode, rescode, Convert.ToDateTime(voudat), isunum, isunum1, billam, hbalam, balam, receiptam, chk, allocamt, vatamt,
                   curcod, convrate, curdesc, bdtamount, fcbnkcharge, invbdtamt, cglamt, invrefno, colconrate, "False", fcadjamt, adjamt,0.00,0.00,0.00));

            }

            ViewState["tblcollection"] = lst;
            this.Data_Bind();
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.ShowBillValue();
            this.Data_Bind();
            this.SaveData();
            this.Bank_Data_Bind();
        }


        private List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> HiddenSameData(List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst)
        {

            ////string slnum = dt.Rows[0]["slnum"].ToString();
            //string styleid = "";
            //string colorid = "";
            //string sizeid = "";
            ////var list22 = lst.OrderBy(m => m.styleid).ThenBy(m => m.colorid).ThenBy(m => m.sizeid).ToList();
            //var list22 = lst.OrderBy(m => m.mlccod).ThenBy(m => m.styleid).ThenBy(m => m.colorid).ThenBy(m => m.sizeid).ToList();
            //foreach (SPEENTITY.C_19_Exp.EClassExpBO.EclassExport c1 in list22)
            //{
            //    if (styleid == c1.styleid.ToString())
            //    {               
            //      c1.styledesc = "";
            //    }
            //    if (styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString())
            //    {
            //        c1.colordesc = "";
            //    }
            //    if (styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString() && sizeid == c1.sizeid.ToString())
            //    {
            //        c1.sizedesc = "";

            //    }
            //    styleid = c1.styleid.ToString();
            //    colorid = c1.colorid.ToString();
            //    sizeid = c1.sizeid.ToString();

            //}
            //ViewState["tblexport"] = list22;
            return lst;

        }
        protected void lnkadd_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dtbankdata"];
            string actcode = this.ddlacccode.SelectedValue.ToString();
            string rescode = "000000000000";// this.ddlresuorcecode.SelectedValue.ToString();
            string resdesc1 = "";//rescode.Length> 0? this.ddlresuorcecode.SelectedItem.ToString() :"" ;
            DataRow[] dr1 = dt.Select("actcode = '" + actcode + "' and  rescode='" + rescode + "'");
            if (dr1.Length == 0)
            {
                DataRow dr2 = dt.NewRow();
                dr2["actcode"] = actcode;
                dr2["rescode"] = rescode;
                dr2["actdesc"] = this.ddlacccode.SelectedItem.ToString();
                dr2["resdesc1"] = resdesc1;
                dr2["fcamt"] = 0.00;
                dr2["bdtamt"] = 0.00;
                dr2["convrate"] = Convert.ToDouble("0" + this.lblConRate.Text);
                dr2["lgamt"] = 0.00;
                dr2["tamt"] = 0.00;
                dr2["totlexpnse"] = 0.00;
                dt.Rows.Add(dr2);
            }
            ViewState["dtbankdata"] = dt;
            this.Bank_Data_Bind();


        }
        private void Bank_Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["dtbankdata"];
            if (dt == null || dt.Rows.Count == 0)
                return;
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();

            ((Label)this.dgv1.FooterRow.FindControl("gvlablBDTamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bdtamt)", "")) ? 0.00 : dt.Compute("sum(bdtamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgv1.FooterRow.FindControl("gvlablfcamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fcamt)", "")) ? 0.00 : dt.Compute("sum(fcamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgv1.FooterRow.FindControl("txtFtamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt)", "")) ? 0.00 : dt.Compute("sum(tamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ViewState["dtbankdata"] = dt;

        }
        protected void txtgfcamt_OnTextChanged(object sender, EventArgs e)
        {
            //this.SaveData();
            //this.Bank_Data_Bind();
        }
        private void FooterCalculation(List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst)
        {

            if (lst.Count == 0)
                return;
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFinvbdtamt")).Text = ("<i class=\"bold text-red\">&#2547;</i> ").ToString()+ lst.Select(p => p.invbdtamt).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFbillamt")).Text = lst.Select(p => p.billam).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFbalamt")).Text = lst.Select(p => p.balam).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFreceiptam")).Text = lst.Select(p => p.receiptam).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFsgdam")).Text = ("<i class=\"bold text-red\">&#2547;</i> ").ToString() + lst.Select(p => p.bdtamount).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFbdtbankcge")).Text = ("<i class=\"bold text-red\">&#2547;</i> ").ToString()+ lst.Select(p => p.vatamt).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFsvatamt")).Text = lst.Select(p => p.fcbnkcharge).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFcglamt")).Text = lst.Select(p => p.cglamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFfcadjamt")).Text = lst.Select(p => p.fcadjamt).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFadjamt")).Text = ("<i class=\"bold text-red\">&#2547;</i> ").ToString()+  lst.Select(p => p.adjamt).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvAitamt")).Text = ("<i class=\"bold text-red\">&#2547;</i> ").ToString() + lst.Select(p => p.aitamt).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvComamt")).Text = ("<i class=\"bold text-red\">&#2547;</i> ").ToString() + lst.Select(p => p.commamt).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvOthrChargs")).Text = ("<i class=\"bold text-red\">&#2547;</i> ").ToString() + lst.Select(p => p.othcharge).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFsvShortFall")).Text = lst.Select(p => p.shortfallfc).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFbdShortFall")).Text = ("<i class=\"bold text-red\">&#2547;</i> ").ToString() + lst.Select(p => p.shortfallbdt).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFOverDueInterst")).Text = ("<i class=\"bold text-red\">&#2547;</i> ").ToString() + lst.Select(p => p.ovrdueintrst).Sum().ToString("#,##0.00;(#,##0.00); ");


        }

        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkAccdesc1 = (HyperLink)e.Row.FindControl("hlnkAccdesc1");
                //string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                //string subcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subcode")).ToString();
                //string subdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subdesc")).ToString();
            }
        }
        protected void dgv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            DataTable dt = (DataTable)ViewState["dtbankdata"];

            int rowindex = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();


            DataView dv = dt.DefaultView;
            ViewState.Remove("dtbankdata");
            ViewState["dtbankdata"] = dv.ToTable();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Deleted Successfully');", true);

            this.Bank_Data_Bind();




        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            //  this.lblmsg.Visible = true;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lblmsg.Text = "You have no permission";
            //    return;
            //}
            try
            {
                string refnum = this.txtrefnum.Text.Trim();
                string Bank = this.ddlBankList.SelectedValue.ToString();
                //40
                if (this.ddlBatchGrp.SelectedValue.ToString() != "82002")
                {
                    if (refnum.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Realization No');", true);
                        this.txtrefnum.Focus();
                        this.txtrefnum.BorderColor = System.Drawing.Color.Red;
                        return;
                    }
                    else if (Bank.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please Fill Bank Name');", true);
                        this.ddlBankList.Focus();
                        this.ddlBankList.BorderColor = System.Drawing.Color.Red;

                    }
                    else
                    {
                        this.ddlBankList.BorderColor = System.Drawing.ColorTranslator.FromHtml("#000");
                        this.txtrefnum.BorderColor = System.Drawing.ColorTranslator.FromHtml("#000");
                    }
                }



                if (this.gvTransaction.Rows.Count == 0)
                    return;
                string lblgvFsgdaml = ((Label)this.gvTransaction.FooterRow.FindControl("lblgvFsgdam")).Text.Trim();
                //if (lblgvFsgdaml.Length == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Tick Mark The Allocated Voucher, then Click Total Button');", true);

                //    return;
                //}




                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string logmrno = (ddlPreMrr.Items.Count > 0) ? this.ddlPreMrr.SelectedItem.ToString() : "NEW";
                DataSet ds3 = proc1.GetTransInfo(comcod, "SP_REPORT_EXPORT", "GETMRNOLOG", logmrno, "", "", "", "", "", "", "", "");
                Session["UserLog"] = ds3.Tables[0];
                DataTable tbl2 = (DataTable)Session["status"];
                string SchCode = "";
                if (ddlPreMrr.Items.Count == 0)
                    this.GetMrNo();


                string PactCode = this.ddlmlccode.SelectedValue.ToString();




                string mrno = this.Request.QueryString["genno"].Length > 0 ? this.Request.QueryString["genno"].ToString() : this.ddlPreMrr.SelectedValue.ToString();
                if (mrno.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Required MRR NO');", true);
                  
                    return;
                }

                // this.lblcurrentvou.Text = mrno;

                // string SchCode=
                string mrdate = Convert.ToDateTime(this.txtEntryDate.Text).ToString("dd-MMM-yyyy");
                //////////////////////userlog
                DataTable dtuser = (DataTable)Session["UserLog"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : userid;
                string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
                string teamcode = "";//this.ddlteam.SelectedValue.ToString();

                // ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = false;

                DataTable dt1 = (DataTable)Session["tblfincoll"];
                string type = this.ddlBatchGrp.SelectedValue.ToString();
                string chqno = this.txtrefnum.Text;
                string bname = "";
                string branchname = this.txtBranch.Text;
                string refno = this.txtothref.Text;
                string remrks = this.txtNar2.Text;
                string paydate = Convert.ToDateTime(this.txtChqDate.Text).ToString("dd-MMM-yyyy");

                bool resulta = true;
                int i = 1;
                List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst = ((List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)ViewState["tblcollection"]);
                //List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lsts = lst.FindAll(p => p.chk == "True");


                // foreach(EClassReceiptaPayment.EClassDebtorBill c in lst)
                string cactcode = ddlBankList.SelectedValue.ToString();


                foreach (SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill lst1 in lst)
                {
                    string actcode = lst1.actcode;
                    string rescode = lst1.rescode;
                    string invno = lst1.isunum;
                    double trnam = lst1.receiptam; //--Fc Amount
                    string billno = lst1.isunum;
                    double vatamt = lst1.vatamt;
                    string curcod = this.ddlCurrency.SelectedValue.ToString();
                    double convrate = Convert.ToDouble("0" + this.lblConRate.Text);
                    double bdtamount = lst1.bdtamount;
                    double fcbnkcharge = lst1.fcbnkcharge;
                    string sminqcurcode = lst1.curcod;
                    double cglamt = lst1.cglamt;
                    double fcadjamt = lst1.fcadjamt;
                    double adjamt = lst1.adjamt;
                    double aitamt = lst1.aitamt;
                    double comamt = lst1.commamt;
                    double othcharge = lst1.othcharge;
                    double shortfallfc = lst1.shortfallfc;
                    double shortfallbdt = lst1.shortfallbdt;
                    double ovrdueintrst = lst1.ovrdueintrst;

                    if (Math.Round(cglamt, 0) == 0)
                    {
                        cglamt = 0;
                    }

                    if ((trnam + fcadjamt) != 0)
                    {
                        resulta = proc1.UpdateTransInfo01(comcod, "SP_REPORT_EXPORT", "INSERTORUPDATEMRINF",
                            actcode, rescode, mrno, type, billno, mrdate, bdtamount.ToString(), chqno, bname, branchname, paydate, refno, remrks, PostedByid, PostSession,
                            Posttrmid, Posteddat, EditByid, Editdat, teamcode, vatamt.ToString(), cactcode, "00000000000000", "0", curcod, convrate.ToString(), trnam.ToString(),
                            fcbnkcharge.ToString(), sminqcurcode, cglamt.ToString(), fcadjamt.ToString(), adjamt.ToString(), 
                            aitamt.ToString(), comamt.ToString(),othcharge.ToString(), shortfallfc.ToString(), shortfallbdt.ToString(),
                            ovrdueintrst.ToString());

                    }
                    if (!resulta)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + proc1.ErrorObject["Msg"].ToString() + "');", true);

                       

                        return;
                    }
                }

                this.SaveData();

                DataTable dt = (DataTable)ViewState["dtbankdata"];
                DataSet dtbankdata = new DataSet("dtbankdata");
                dtbankdata.Tables.Add(dt);
                dtbankdata.Tables[0].TableName = "tblbdata";

                DataTable dtadv = (DataTable)ViewState["tblAdv"];
                DataSet dsadvdata = new DataSet("dsadvdata");
                dsadvdata.Tables.Add(dtadv);
                dsadvdata.Tables[0].TableName = "tblbadv";

                bool resultb = proc1.UpdateXmlTransInfo(comcod, "SP_REPORT_EXPORT", "INSERTBANKDATAREALSHEET", dtbankdata, dsadvdata, null, mrno);
                if (!resultb)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + proc1.ErrorObject["Msg"].ToString() + "');", true);

                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);



                }


                //Log Report
                string eventtype = "Money Receipt";
                string eventdesc = "Receipt No: " + mrno + " Dated: " + mrdate;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);




            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Errp'"+ ex.Message +");", true);
               

            }
        }

        private void SaveData()
        {
            DataTable dt = (DataTable)ViewState["dtbankdata"];

            int TblRowIndex, i;
            //double convrate = Convert.ToDouble(this.lblConRate.Text.Trim().ToString());
            for (i = 0; i < this.dgv1.Rows.Count; i++)
            {
                double fcamt = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgfcamt")).Text.Trim());
                double convrate = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtcrate")).Text.Trim());
                double bdtamt = Convert.ToDouble("0" + ((Label)this.dgv1.Rows[i].FindControl("txtgvbdtamt")).Text.Trim());
                double lgamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtglamt")).Text.Trim()));
                double totlexpnse = Convert.ToDouble("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtTotalExpense")).Text.Trim());

                string trnrmrk = ((TextBox)this.dgv1.Rows[i].FindControl("lblgvRemarks")).Text.Trim();
                TblRowIndex = (dgv1.PageIndex) * dgv1.PageSize + i;
                dt.Rows[TblRowIndex]["fcamt"] = fcamt;
                dt.Rows[TblRowIndex]["convrate"] = convrate;
                dt.Rows[TblRowIndex]["bdtamt"] = (fcamt * convrate);
                dt.Rows[TblRowIndex]["trnrmrk"] = trnrmrk;
                dt.Rows[TblRowIndex]["lgamt"] = lgamt;
                dt.Rows[TblRowIndex]["tamt"] = ((lgamt + (fcamt * convrate))- totlexpnse);
                dt.Rows[TblRowIndex]["totlexpnse"] = totlexpnse;

            }



            ViewState["dtbankdata"] = dt;
            this.Bank_Data_Bind();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                ViewState.Remove("tblcollection");
                ViewState.Remove("dtbankdata");
                this.ddlmlccode.Enabled = true;
                this.FooterSegment.Visible = false;
                this.gvTransaction.DataSource = null;
                this.gvTransaction.DataBind();
                this.dgv1.DataSource = null;
                this.dgv1.DataBind();
                this.PanelCollection.Visible = false;
                this.lbtnOk.Text = "Ok";

                this.ddlBuyer.Enabled = true;//System.DateTime.Today.ToString("dd-MMM-yyyy");
                return;
            }


            //this.ddlmlccode.Enabled = false;
            this.FooterSegment.Visible = true;
            this.PanelCollection.Visible = true;
            this.lbtnOk.Text = "New";
            this.ddlBuyer.Enabled = false;
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string mrrno = (this.ddlPreMrr.SelectedValue.ToString() == "") ? this.Request.QueryString["genno"].ToString() : this.ddlPreMrr.SelectedValue.ToString();
            this.GetPrevData(mrrno, mlccod);

        }



        protected void gvTransaction_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill> lst = ((List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)ViewState["tblcollection"]);

            int rowindex = (this.dgv1.PageSize) * (this.dgv1.PageIndex) + e.RowIndex;
            string actcode = lst[rowindex].actcode;
            string rescode = lst[rowindex].rescode;
            string invno = lst[rowindex].isunum;
            string mrno = this.Request.QueryString["genno"].Length > 0 ? this.Request.QueryString["genno"].ToString() : this.ddlPreMrr.SelectedValue.ToString();
            string comcod = this.GetComeCode();
            bool result = proc1.UpdateTransInfo01(comcod, "SP_REPORT_EXPORT", "DELETE_INV_FROM_COLLECTION", actcode, mrno, invno, rescode);
            if (result == true)
            {
                lst.RemoveAt(rowindex);
                ViewState["tblcollection"] = lst;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Successfully";

            }

            this.Data_Bind();
        }
        protected void txtcrate_TextChanged(object sender, EventArgs e)
        {
            //this.SaveData();
            // this.Bank_Data_Bind();
        }
        protected void gvTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string sdfd = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adjflag"));
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adjflag")) == "True")
                {
                    e.Row.BackColor = System.Drawing.Color.MediumSeaGreen;
                }

            }
        }
    }
}