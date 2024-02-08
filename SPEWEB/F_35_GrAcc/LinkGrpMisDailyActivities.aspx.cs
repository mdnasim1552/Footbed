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


namespace SPEWEB.F_35_GrAcc
{
    public partial class LinkGrpMisDailyActivities : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        Common ObjCommon = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Order, Production & Shipment";

                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "RptOrdProVsShip") ? "Order, Production & Shipment"
                    : (type == "ChequeInHand") ? "Cheque In Hand" : (type == "RecPay") ? "Receipt &  payment Information"
                    : (type == "BankPosition") ? "Bank Position Information"
                    : (type == "MasPVsMonPVsExAllPro") ? "MASTER PLAN, MONTHLY PLAN Vs. ACHIEVEMENT- ALL PROJECT"

                    : (type == "RptOrdExparlz") ? "Order, Export & Realization Summary"
                    : (type == "SoldUnsold") ? "Sold  & Unsold Infromation"
                    : (type == "RptBBLPaySt") ? "BBLC Payment Status"
                    : (type == "FeaAllProject") ? "Feasibility Report-All Project(Summary)"
                    : (type == "GPNPALLPRO") ? "GP & NP All Projeect "
                    : (type == "LCStatus") ? "LC Status"
                    : (type == "MProStatus") ? "Month Wise Project Status"
                    : (type == "PDCSummary") ? "PDC Summary Status"
                    : (type == "RptDayWSale") ? "Day Wise Sales" : (type == "LcSummary") ? "Day Wise Sales" : "Month Wise Payment-Summary";
                this.SelectView();


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChequeInHand":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lblAsDate.Text = "As On " + this.Request.QueryString["Date2"].ToString();
                    this.ShowCollDetails();
                    break;

                case "RecPay":

                    this.MultiView1.ActiveViewIndex = 1;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ReceiptAndPayment();
                    break;

                case "IssuedVsCollect":

                    this.MultiView1.ActiveViewIndex = 6;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowIssuedVsColl();
                    break;

                case "BankPosition":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowBankPosition();
                    break;

                case "MasPVsMonPVsExAllPro":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowMMonPlnVsAchAllPro();
                    break;


                case "RptOrdExparlz":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowOrdExpAndRelized();
                    break;

                case "SoldUnsold":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowSoldUnsold();
                    break;

                case "RptBBLPaySt":
                    this.MultiView1.ActiveViewIndex = 7;
                    this.lblAsDate.Text = "As On " + this.Request.QueryString["Date2"].ToString();
                    this.ShowBBLCPaymentst();
                    break;

                case "FeaAllProject":
                    this.lblAsDate.Text = " As On " + this.Request.QueryString["date"].ToString();
                    this.MultiView1.ActiveViewIndex = 8;
                    this.ShowFeaAllProject();
                    break;

                case "GPNPALLPRO":
                    this.lblAsDate.Text = " As On " + this.Request.QueryString["date"].ToString();
                    this.MultiView1.ActiveViewIndex = 9;
                    this.ShowAllProGPNP();
                    break;

                case "LCStatus":
                    this.lblAsDate.Text = " As On " + this.Request.QueryString["date"].ToString();
                    this.MultiView1.ActiveViewIndex = 10;
                    this.ShowLCStatus();
                    break;

                case "MProStatus":
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.MultiView1.ActiveViewIndex = 11;
                    this.ShowMonProStatus();
                    break;

                case "PDCSummary":
                    this.MultiView1.ActiveViewIndex = 12;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.lblDateRange.Visible = false;
                    this.lblAsDate.Visible = false;
                    this.lblAsDate.Text = "As On " + this.Request.QueryString["Date2"].ToString();
                    this.ShowPDCSummary();
                    break;


                case "RptOrdProVsShip":
                    this.MultiView1.ActiveViewIndex = 13;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.OrderProVsShip();
                    break;

                case "LcDetails":
                    this.MultiView1.ActiveViewIndex = 14;
                    this.lblAsDate.Text = "As On " + this.Request.QueryString["Date"].ToString();
                    this.ShowLcDetails();
                    break;

                case "LcSummary":
                    this.MultiView1.ActiveViewIndex = 15;
                    //  this.lblAsDate.Text =  "As On " + this.Request.QueryString["Date"].ToString();
                    this.ShowLcSummary();
                    break;


                case "BgdLCStatus":
                    this.MultiView1.ActiveViewIndex = 16;
                    this.lblAsDate.Text = "As On " + this.Request.QueryString["Date"].ToString();
                    this.ShowBGdLCStatus();
                    break;









            }
        }


        private void ShowCollDetails()
        {
            Session.Remove("tblcollvscl");

            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "RPTCHEQUEINHAND", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvCollDet.DataSource = null;
                this.gvCollDet.DataBind();
                return;
            }
            Session["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();


        }

        private void ReceiptAndPayment()
        {
            Session.Remove("tblcollvscl");

            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string rp = "RP";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RP_COMPANY_04", frmdate, todate, rp, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblcollvscl"] = ds1.Tables[0];
            Session["recandpayFo"] = ds1.Tables[1];
            Session["recandpayNote"] = ds1.Tables[2];

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

            this.FooterCalculation();
            ds1.Dispose();
            Session["Report1"] = gvrecandpay;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((HyperLink)this.gvrecandpay.HeaderRow.FindControl("hlbtnRcvPayCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                ((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).NavigateUrl = "~/F_45_GrAcc/LinkGrpAccount.aspx?Type=BalConfirmation&comcod=" + comcod + "&Date1=" + frmdate + "&Date2=" + todate;

            }
        }


        private void ShowIssuedVsColl()
        {

            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "ISSUEDVSCOLLECTION", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblcollvscl"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();




        }
        private void ShowOrdExpAndRelized()
        {
            try
            {
                Session.Remove("tblcollvscl");
                string comcod = this.Request.QueryString["comcod"].ToString();
                string frmdate = this.Request.QueryString["Date1"].ToString();
                string todate = this.Request.QueryString["Date2"].ToString();
                DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "RPTORDEXPARlZSTATUS", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvOrdexarlz.DataSource = null;
                    this.gvOrdexarlz.DataBind();
                    return;
                }
                Session["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
                this.Data_Bind();
                ds1.Dispose();




            }

            catch (Exception ex)
            {


            }




        }

        private void RPNote()
        {
            this.PanelNote.Visible = true;
            DataTable dt = (DataTable)Session["recandpayNote"];

            this.lblopnliabilitiesval.Text = Convert.ToDouble(dt.Rows[0]["opnliaam"]).ToString("#,##0;(#,##0) ;");
            this.lblclsliabilitiesval.Text = Convert.ToDouble(dt.Rows[0]["clsliaam"]).ToString("#,##0;(#,##0) ;");
            this.lblnetLiabilitiesval.Text = Convert.ToDouble(dt.Rows[0]["netliaam"]).ToString("#,##0;(#,##0) ;");

            //this.lblPaid.Text = Convert.ToDouble(dt.Rows[0]["payamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblInPaid.Text = Convert.ToDouble(dt.Rows[0]["ipayamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblSodPaid.Text = Convert.ToDouble(dt.Rows[0]["sodpayamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblTPaid.Text = Convert.ToDouble(dt.Rows[0]["tpayamt"]).ToString("#,##0;(#,##0) ;");

            //this.lblNet.Text = Convert.ToDouble(dt.Rows[0]["netamt"]).ToString("#,##0;(#,##0) ;");

        }

        private void ShowBankPosition()
        {

            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTBANKPOSITION", frmdate, todate, "12", "", "", "", "", "", "");
            Session["tblcollvscl"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();




        }


        private void ShowMMonPlnVsAchAllPro()
        {
            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTMASPVSMONPVSEX", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMMPlanVsAch.DataSource = null;
                this.gvMMPlanVsAch.DataBind();
                return;
            }
            Session["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }

        private void ShowSoldUnsold()
        {
            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT02", "RPTDATEWALLPROINSDUES", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.dgvAccRec03.DataSource = null;
                this.dgvAccRec03.DataBind();
                return;
            }
            Session["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }

        private void ShowBBLCPaymentst()
        {

            try
            {
                string comcod = this.Request.QueryString["comcod"].ToString();
                string date = this.Request.QueryString["Date2"].ToString();
                DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "RPTBBLCPAYSTATUS", "", date, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvRptBBLCPay.DataSource = null;
                    this.gvRptBBLCPay.DataBind();
                    return;
                }
                Session["tblcollvscl"] = ds1.Tables[0];
                this.Data_Bind();



            }
            catch (Exception ex)
            {

            }




        }

        private void ShowFeaAllProject()
        {

            string comcod = this.Request.QueryString["comcod"].ToString();
            string date = this.Request.QueryString["date"].ToString();
            DataSet ds2 = AccData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "INSTATALLPRJSUM", date, "consolidate", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaAllPro.DataSource = null;
                this.gvFeaAllPro.DataBind();

                return;
            }
            Session["tblcollvscl"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }


        private void ShowAllProGPNP()
        {


            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string date = this.Request.QueryString["date"].ToString();
            DataSet ds2 = AccData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_04", "RPTALLPROCOSTASALE", date, "consolidate", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvgpnp.DataSource = null;
                this.gvgpnp.DataBind();
                return;
            }
            Session["tblcollvscl"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void ShowLCStatus()
        {



            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string date = this.Request.QueryString["date"].ToString();
            string dd2value = "12";
            string FcAmt = "FcAmt";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_LC_STATUS", "RPTMONLCSTATUS", date, dd2value, FcAmt, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvMonPorStatus.DataSource = null;
                this.gvMonPorStatus.DataBind();
                return;

            }

            Session["tblcollvscl"] = ds1.Tables[0];
            ViewState["tblresdesc"] = ds1.Tables[1];
            ViewState["tblresdesc1"] = ds1.Tables[2];
            ds1.Dispose();
            this.Data_Bind();



        }


        private void ShowMonProStatus()
        {
            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTMONPROSTATUS", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvMonPorStatus.DataSource = null;
                this.gvMonPorStatus.DataBind();
                return;

            }

            Session["tblcollvscl"] = ds1.Tables[0];
            ViewState["tblresdesc"] = ds1.Tables[1];
            ds1.Dispose();
            this.Data_Bind();
        }

        private void ShowPDCSummary()
        {

            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_MIS02", "RPPDCSUMMARY", "", todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvpdc.DataSource = null;
                this.gvpdc.DataBind();
                return;

            }

            Session["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            ds1.Dispose();
            this.Data_Bind();



        }

        private void OrderProVsShip()
        {
            Session.Remove("tblcollvscl");


            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();


            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "RPTORDPROVSSHIP", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvOrderrec.DataSource = null;
                this.gvOrderrec.DataBind();
                return;
            }



            Session["tblcollvscl"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }



        private void ShowLcDetails()
        {
            Session.Remove("tblcollvscl");


            string comcod = this.Request.QueryString["comcod"].ToString();

            string date = this.Request.QueryString["Date"].ToString();
            string buyerid = this.Request.QueryString["buyerid"].ToString();
            string mlccod = this.Request.QueryString["mlccod"].ToString();


            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "RPTLCPOSITION", date, mlccod, buyerid, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLcDetails.DataSource = null;
                this.gvLcDetails.DataBind();
                return;
            }



            Session["tblcollvscl"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();



        }



        private void ShowLcSummary()
        {
            Session.Remove("tblcollvscl");

            string comcod = this.Request.QueryString["comcod"].ToString();
            string date = this.Request.QueryString["Date"].ToString();

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "RPTLCSUMMARY", date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLcSummary.DataSource = null;
                this.gvLcSummary.DataBind();
                return;
            }



            Session["tblcollvscl"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();


        }

        private void ShowBGdLCStatus()
        {

            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string date = this.Request.QueryString["date"].ToString();
            string dd2value = "12";
            string FcAmt = "FcAmt";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_LC_STATUS", "RPTBGDLCSTATUS", date, dd2value, FcAmt, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvMonPorStatus.DataSource = null;
                this.gvMonPorStatus.DataBind();
                return;

            }

            Session["tblcollvscl"] = ds1.Tables[0];
            ViewState["tblresdesc"] = ds1.Tables[1];
            ViewState["tblresdesc1"] = ds1.Tables[2];
            ds1.Dispose();
            this.Data_Bind();


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string mlccod, buyerid, mmlccod; ;

            switch (type)
            {



                case "ChequeInHand":
                case "MasPVsMonPVsExAllPro":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        else
                            grp = dt1.Rows[j]["grp"].ToString();
                    }

                    break;





                case "SoldUnsold":
                    string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpdesc"].ToString() == grpdesc)
                        {
                            grpdesc = dt1.Rows[j]["grpdesc"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        else
                        {
                            grpdesc = dt1.Rows[j]["grpdesc"].ToString();
                        }

                    }

                    break;


                case "FeaAllProject":
                    string company = dt1.Rows[0]["company"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company)
                        {
                            company = dt1.Rows[j]["company"].ToString();
                            dt1.Rows[j]["companydesc"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["company"].ToString() == company)
                                dt1.Rows[j]["companydesc"] = "";
                            company = dt1.Rows[j]["company"].ToString();
                        }
                    }
                    break;


                case "GPNPALLPRO":
                    string pactcode1 = dt1.Rows[0]["pactcode1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode1"].ToString() == pactcode1)
                        {
                            dt1.Rows[j]["pactdesc1"] = "";
                        }


                        pactcode1 = dt1.Rows[j]["pactcode1"].ToString();
                    }
                    break;


                case "PrjStatus":

                    string grpps = dt1.Rows[0]["grp"].ToString();
                    pactcode1 = dt1.Rows[0]["pactcode1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpps && dt1.Rows[j]["pactcode1"].ToString() == pactcode1)
                        {

                            dt1.Rows[j]["grpdesc"] = "";
                            dt1.Rows[j]["pactdesc1"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["grp"].ToString() == grpps)
                                dt1.Rows[j]["grpdesc"] = "";

                            if (dt1.Rows[j]["pactcode1"].ToString() == pactcode1)
                                dt1.Rows[j]["pactdesc1"] = "";
                        }

                        grpps = dt1.Rows[j]["grp"].ToString();
                        pactcode1 = dt1.Rows[j]["pactcode1"].ToString();

                    }
                    break;

                case "PDCSummary":

                    string pgrp = dt1.Rows[0]["pgrp"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pgrp"].ToString() == pgrp)
                            dt1.Rows[j]["pgrpdesc"] = "";
                        pgrp = dt1.Rows[j]["pgrp"].ToString();
                    }
                    break;

                case "RptDayWSale":
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
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

                    }



                    break;

                case "RptOrdExparlz":

                    mlccod = dt1.Rows[0]["mlccod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["mlccod"].ToString() == mlccod)
                        {
                            dt1.Rows[j]["lcname"] = "";
                            dt1.Rows[j]["buyer"] = "";
                            dt1.Rows[j]["job"] = "";
                            dt1.Rows[j]["orderno"] = "";
                            dt1.Rows[j]["ordrqty"] = 0;
                            dt1.Rows[j]["ordrrat"] = 0;
                            dt1.Rows[j]["ordramt"] = 0;

                        }





                        mlccod = dt1.Rows[j]["mlccod"].ToString();
                    }




                    break;


                case "LcDetails":

                    buyerid = dt1.Rows[0]["buyerid"].ToString();
                    mmlccod = dt1.Rows[0]["mmlccod"].ToString();
                    mlccod = dt1.Rows[0]["mlccod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["buyerid"].ToString() == buyerid && dt1.Rows[j]["mmlccod"].ToString() == mmlccod && dt1.Rows[j]["mlccod"].ToString() == mlccod)
                        {
                            dt1.Rows[j]["buyer"] = "";
                            dt1.Rows[j]["lcname"] = "";
                            dt1.Rows[j]["job"] = "";
                            dt1.Rows[j]["lcdate"] = "";
                            dt1.Rows[j]["shipdate"] = "";
                            dt1.Rows[j]["orderno"] = "";
                            dt1.Rows[j]["ordrqty"] = 0;
                            dt1.Rows[j]["ordrrat"] = 0;
                            dt1.Rows[j]["ordramt"] = 0;

                        }
                        else
                        {

                            if (dt1.Rows[j]["buyerid"].ToString() == buyerid)
                                dt1.Rows[j]["buyer"] = "";


                            if (dt1.Rows[j]["mmlccod"].ToString() == mmlccod)
                            {
                                dt1.Rows[j]["lcname"] = "";
                                dt1.Rows[j]["lcdate"] = "";
                            }




                            if (dt1.Rows[j]["mlccod"].ToString() == mlccod)
                            {

                                dt1.Rows[j]["lcname"] = "";
                                dt1.Rows[j]["job"] = "";
                                dt1.Rows[j]["lcdate"] = "";
                                dt1.Rows[j]["shipdate"] = "";
                                dt1.Rows[j]["orderno"] = "";
                                dt1.Rows[j]["ordrqty"] = 0;
                                dt1.Rows[j]["ordrrat"] = 0;
                                dt1.Rows[j]["ordramt"] = 0;

                            }


                        }




                        buyerid = dt1.Rows[j]["buyerid"].ToString();
                        mmlccod = dt1.Rows[j]["mmlccod"].ToString();
                        mlccod = dt1.Rows[j]["mlccod"].ToString();
                    }
                    break;


                case "LcSummary":
                    buyerid = dt1.Rows[0]["buyerid"].ToString();
                    mmlccod = dt1.Rows[0]["mmlccod"].ToString();
                    mlccod = dt1.Rows[0]["mlccod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["buyerid"].ToString() == buyerid && dt1.Rows[j]["mmlccod"].ToString() == mmlccod && dt1.Rows[j]["mlccod"].ToString() == mlccod)
                        {
                            dt1.Rows[j]["buyer"] = "";
                            dt1.Rows[j]["lcname"] = "";
                            dt1.Rows[j]["mlcstatus"] = "";

                            dt1.Rows[j]["job"] = "";
                            dt1.Rows[j]["lcdate"] = "";
                            dt1.Rows[j]["shipdate"] = "";
                            dt1.Rows[j]["orderno"] = "";


                        }
                        else
                        {

                            if (dt1.Rows[j]["buyerid"].ToString() == buyerid)
                                dt1.Rows[j]["buyer"] = "";


                            if (dt1.Rows[j]["mmlccod"].ToString() == mmlccod)
                            {
                                dt1.Rows[j]["lcname"] = "";
                                dt1.Rows[j]["mlcstatus"] = "";
                                dt1.Rows[j]["lcdate"] = "";
                            }




                            if (dt1.Rows[j]["mlccod"].ToString() == mlccod)
                            {

                                dt1.Rows[j]["lcname"] = "";
                                dt1.Rows[j]["mlcstatus"] = "";
                                dt1.Rows[j]["job"] = "";
                                dt1.Rows[j]["lcdate"] = "";
                                dt1.Rows[j]["shipdate"] = "";
                                dt1.Rows[j]["orderno"] = "";


                            }


                        }




                        buyerid = dt1.Rows[j]["buyerid"].ToString();
                        mmlccod = dt1.Rows[j]["mmlccod"].ToString();
                        mlccod = dt1.Rows[j]["mlccod"].ToString();
                    }
                    break;


            }


            return dt1;

        }

        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dtpname, dtpname1;

            int j, k;
            switch (type)
            {

                case "ChequeInHand":
                    this.gvCollDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCollDet.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvCollDet.DataBind();
                    this.FooterCalculation();
                    break;

                case "BankPosition":
                    this.gvBankPosition.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvBankPosition.DataBind();
                    Session["Report1"] = gvBankPosition;
                    ((HyperLink)this.gvBankPosition.HeaderRow.FindControl("hlbtnbnkpdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;


                case "MasPVsMonPVsExAllPro":

                    this.gvMMPlanVsAch.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvMMPlanVsAch.DataBind();
                    this.FooterCalculation();
                    break;
                case "RptOrdExparlz":
                    this.gvOrdexarlz.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvOrdexarlz.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvOrdexarlz.DataBind();
                    this.FooterCalculation();
                    break;

                case "SoldUnsold":
                    this.dgvAccRec03.Columns[17].HeaderText = "Dues Up to " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("MMM- yyyy");
                    this.dgvAccRec03.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.dgvAccRec03.DataSource = (DataTable)Session["tblcollvscl"];
                    this.dgvAccRec03.DataBind();
                    this.FooterCalculation();
                    break;

                case "IssuedVsCollect":
                    this.gvarecandpay.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvarecandpay.DataBind();
                    this.FooterCalculation();
                    Session["Report1"] = this.gvarecandpay;
                    if (((DataTable)Session["tblcollvscl"]).Rows.Count > 0)
                        ((HyperLink)this.gvarecandpay.HeaderRow.FindControl("hlbtnacRcvPayCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;


                case "RptBBLPaySt":
                    this.gvRptBBLCPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvRptBBLCPay.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvRptBBLCPay.DataBind();
                    this.FooterCalculation();
                    break;


                case "FeaAllProject":

                    this.gvFeaAllPro.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvFeaAllPro.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvFeaAllPro.DataBind();
                    break;


                case "GPNPALLPRO":
                    this.gvgpnp.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvgpnp.DataBind();
                    break;

                case "LCStatus":
                    dtpname = (DataTable)ViewState["tblresdesc"];
                    j = 9;
                    for (int i = 0; i < dtpname.Rows.Count; i++)
                    {

                        this.gvLcStatus.Columns[j].HeaderText = dtpname.Rows[i]["resdesc"].ToString();
                        j++;
                        if (j == 15)
                            break;


                    }
                    dtpname1 = (DataTable)ViewState["tblresdesc1"];
                    k = 19;
                    for (int i = 0; i < dtpname1.Rows.Count; i++)
                    {

                        this.gvLcStatus.Columns[k].HeaderText = dtpname1.Rows[i]["resdesc"].ToString();
                        k++;
                        if (k == 26)
                            break;


                    }



                    this.gvLcStatus.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvLcStatus.DataBind();
                    Session["Report1"] = gvLcStatus;
                    if (((DataTable)Session["tblcollvscl"]).Rows.Count > 0)
                        ((HyperLink)this.gvLcStatus.HeaderRow.FindControl("hlbtnCdataExells")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    this.FooterCalculation();
                    break;


                case "MProStatus":
                    dtpname = (DataTable)ViewState["tblresdesc"];
                    j = 2;
                    for (int i = 0; i < dtpname.Rows.Count; i++)
                    {

                        this.gvMonPorStatus.Columns[j].HeaderText = dtpname.Rows[i]["resdesc"].ToString();
                        j++;
                        if (j == 22)
                            break;


                    }

                    this.gvMonPorStatus.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvMonPorStatus.DataBind();
                    Session["Report1"] = gvMonPorStatus;
                    if (((DataTable)Session["tblcollvscl"]).Rows.Count > 0)
                        ((HyperLink)this.gvMonPorStatus.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    this.FooterCalculation();
                    break;


                case "PDCSummary":

                    this.gvpdc.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvpdc.DataBind();
                    break;

                case "RptOrdProVsShip":
                    this.gvOrderrec.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvOrderrec.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvOrderrec.DataBind();
                    this.FooterCalculation();
                    break;

                case "LcDetails":
                    this.gvLcDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvLcDetails.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvLcDetails.DataBind();
                    this.FooterCalculation();
                    break;

                case "LcSummary":
                    this.gvLcSummary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvLcSummary.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvLcSummary.DataBind();
                    this.FooterCalculation();
                    break;


                case "BgdLCStatus":
                    dtpname = (DataTable)ViewState["tblresdesc"];
                    j = 9;
                    for (int i = 0; i < dtpname.Rows.Count; i++)
                    {

                        this.gvbgdLcStatus.Columns[j].HeaderText = dtpname.Rows[i]["resdesc"].ToString();
                        j++;
                        if (j == 15)
                            break;


                    }

                    dtpname1 = (DataTable)ViewState["tblresdesc1"];
                    k = 19;
                    for (int i = 0; i < dtpname1.Rows.Count; i++)
                    {

                        this.gvbgdLcStatus.Columns[k].HeaderText = dtpname1.Rows[i]["resdesc"].ToString();
                        k++;
                        if (k == 26)
                            break;


                    }



                    this.gvbgdLcStatus.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvbgdLcStatus.DataBind();
                    Session["Report1"] = gvLcStatus;
                    if (((DataTable)Session["tblcollvscl"]).Rows.Count > 0)
                        ((HyperLink)this.gvbgdLcStatus.HeaderRow.FindControl("hlbtnCdataExellsbgd")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    this.FooterCalculation();
                    break;



            }

        }



        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["tblcollvscl"];
            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt4;
            DataView dv1;
            switch (type)
            {
                case "ChequeInHand":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("grp='C' and usircode='BBBBAAAAAAAA' ");
                    dt4 = dv1.ToTable();
                    double cashamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(cashamt)", "")) ? 0 : dt4.Compute("sum(cashamt)", "")));
                    double chqamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(chqamt)", "")) ? 0 : dt4.Compute("sum(chqamt)", "")));


                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvFCashamt")).Text = cashamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvFChqamt")).Text = chqamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvCDNetTotal")).Text = (cashamt + chqamt).ToString("#,##0;(#,##0); ");
                    break;



                case "RecPay":
                    dt1 = (DataTable)Session["recandpayFo"];

                    //dv1=dt.Copy().DefaultView;
                    //dv1.RowFilter = ("recpcode like '%00000000%' or paycode like '%00000000%'");
                    //dt1 = dv1.ToTable();
                    double frecamt = 0, fpayamt1 = 0, netbal;

                    frecamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recpam)", "")) ?
                           0 : dt1.Compute("sum(recpam)", "")));
                    ((Label)this.gvrecandpay.FooterRow.FindControl("lblgvFrecpam")).Text = frecamt.ToString("#,##0;(#,##0) ;");
                    fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                         0 : dt1.Compute("sum(payam)", "")));

                    ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFpayam1")).Text = fpayamt1.ToString("#,##0;(#,##0) ;");
                    netbal = frecamt - fpayamt1;

                    ((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text = (frecamt - fpayamt1).ToString("#,##0;(#,##0) ;");
                    break;


                case "RptOrdExparlz":

                    double orderamt, shipamt, orderbal, peronord, rlzamt, shipbal, peronship;


                    orderamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordramt)", "")) ? 0.00 : dt1.Compute("sum(ordramt)", "")));
                    shipamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(shipamt)", "")) ? 0.00 : dt1.Compute("sum(shipamt)", "")));
                    orderbal = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordrbal)", "")) ? 0.00 : dt1.Compute("sum(ordrbal)", "")));
                    rlzamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(rlzamt)", "")) ? 0.00 : dt1.Compute("sum(rlzamt)", "")));
                    shipbal = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(shipbal)", "")) ? 0.00 : dt1.Compute("sum(shipbal)", "")));
                    peronord = (orderamt == 0) ? 0 : (shipamt * 100) / orderamt;
                    peronship = (shipamt == 0) ? 0 : (rlzamt * 100) / shipamt;

                    ((Label)this.gvOrdexarlz.FooterRow.FindControl("lgvFordramtoer")).Text = orderamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOrdexarlz.FooterRow.FindControl("lgvFexpamtoer")).Text = shipamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOrdexarlz.FooterRow.FindControl("lgvForderbaloer")).Text = (orderamt - shipamt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOrdexarlz.FooterRow.FindControl("lgvFperoordoer")).Text = peronord.ToString("#,##0.00;(#,##0.00); ") + "%";
                    ((Label)this.gvOrdexarlz.FooterRow.FindControl("lgvFrealizeamtoer")).Text = rlzamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOrdexarlz.FooterRow.FindControl("lgvFperoshipoer")).Text = peronship.ToString("#,##0.00;(#,##0.00); ") + "%";
                    ((Label)this.gvOrdexarlz.FooterRow.FindControl("lgvFshortoexcessoer")).Text = shipbal.ToString("#,##0;(#,##0); ");

                    break;


                case "MasPVsMonPVsExAllPro":

                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFmasPlan")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(masplan)", "")) ? 0.00 :
                        dt1.Compute("sum(masplan)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFmonPlan")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(monplan)", "")) ? 0.00 :
                        dt1.Compute("sum(monplan)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFExecutionpAC")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(excution)", "")) ? 0.00 :
                       dt1.Compute("sum(excution)", ""))).ToString("#,##0;(#,##0); ");
                    break;




                case "SoldUnsold":
                    dt4 = dt1.Copy();
                    DataView dv = dt1.DefaultView;
                    dv.RowFilter = ("pactcode not like '%AAAA%'");
                    dt4 = dv.ToTable();

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtstkamal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(tstkam)", "")) ?
                     0.00 : dt4.Compute("Sum(tstkam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFususizeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(ususize)", "")) ?
                       0.00 : dt4.Compute("Sum(ususize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFusuamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(usamt)", "")) ?
                       0.00 : dt4.Compute("Sum(usamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFusizeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(usize)", "")) ?
                          0.00 : dt4.Compute("Sum(usize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFaptcostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(aptcost)", "")) ?
                     0.00 : dt4.Compute("Sum(aptcost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFcpaocostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(cpaocost)", "")) ?
                    0.00 : dt4.Compute("Sum(cpaocost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtocostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(tocost)", "")) ?
                    0.00 : dt4.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFatoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(atodues)", "")) ?
                     0.00 : dt4.Compute("Sum(atodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtotalduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(todues)", "")) ?
                    0.00 : dt4.Compute("Sum(todues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgFEncashal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(reconamt)", "")) ?
                    0.00 : dt4.Compute("Sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtretamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(retcheque)", "")) ?
                    0.00 : dt4.Compute("Sum(retcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtframtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(fcheque)", "")) ?
                    0.00 : dt4.Compute("Sum(fcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtpdamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(pcheque)", "")) ?
                    0.00 : dt4.Compute("Sum(pcheque)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoreceivedal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(ramt)", "")) ?
                    0.00 : dt4.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(bamt)", "")) ?
                    0.00 : dt4.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFpbookingal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(pbookam)", "")) ?
                    0.00 : dt4.Compute("Sum(pbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFpinstallmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(pinsam)", "")) ?
                0.00 : dt4.Compute("Sum(pinsam)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFCbookingal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(cbookam)", "")) ?
                    0.00 : dt4.Compute("Sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFCinstallmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(cinsam)", "")) ?
                    0.00 : dt4.Compute("Sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoCInstalmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(ctodues)", "")) ?
                0.00 : dt4.Compute("Sum(ctodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFvbaamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(vbamt)", "")) ?
                            0.00 : dt4.Compute("Sum(vbamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFdelchargeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(cdelay)", "")) ?
                0.00 : dt4.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFdischargeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(discharge)", "")) ?
                0.00 : dt4.Compute("Sum(discharge)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFnettoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(ntodues)", "")) ?
               0.00 : dt4.Compute("Sum(ntodues)", ""))).ToString("#,##0;(#,##0); ");


                    break;


                case "IssuedVsCollect":
                    frecamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recpam)", "")) ?
                           0 : dt1.Compute("sum(recpam)", "")));
                    ((Label)this.gvarecandpay.FooterRow.FindControl("lblgvFrecpamac")).Text = frecamt.ToString("#,##0;(#,##0) ;");
                    fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                         0 : dt1.Compute("sum(payam)", "")));

                    ((Label)this.gvarecandpay.FooterRow.FindControl("lgvFpayamac")).Text = fpayamt1.ToString("#,##0;(#,##0) ;");
                    netbal = frecamt - fpayamt1;

                    ((Label)this.gvarecandpay.FooterRow.FindControl("lgvFNetBalanceac")).Text = (frecamt - fpayamt1).ToString("#,##0;(#,##0) ;");
                    break;

                case "RptBBLPaySt":

                    ((Label)this.gvRptBBLCPay.FooterRow.FindControl("lgvFbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(billamt)", "")) ?
                    0.00 : dt1.Compute("Sum(billamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvRptBBLCPay.FooterRow.FindControl("lgvFdueamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(dueam)", "")) ?
                    0.00 : dt1.Compute("Sum(dueam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvRptBBLCPay.FooterRow.FindControl("lgvFnydueamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(nydueamt)", "")) ?
                   0.00 : dt1.Compute("Sum(nydueamt)", ""))).ToString("#,##0;(#,##0); ");
                    // dt4 = dt1.Copy();
                    //dv1 = dt4.DefaultView;
                    //dv1.RowFilter = ("typesum='TTTT'");
                    //dt4 = dv1.ToTable();
                    //((Label)this.gvgrpchqissued.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(payam)", "")) ? 0 : dt4.Compute("sum(payam)", ""))).ToString("#,##0;-#,##0; ");
                    break;


                case "LCStatus":

                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFtQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordrqty)", "")) ? 0.00 : dt1.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFordramtlc")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordramt)", "")) ? 0.00 : dt1.Compute("sum(ordramt)", ""))).ToString("#,##0;(#,##0); ");



                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFshiqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(shiqty)", "")) ? 0.00 : dt1.Compute("sum(shiqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFseqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(seqty)", "")) ? 0.00 : dt1.Compute("sum(seqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFtramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tramt)", "")) ? 0.00 : dt1.Compute("sum(tramt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r1)", "")) ? 0.00 : dt1.Compute("sum(r1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r2)", "")) ? 0.00 : dt1.Compute("sum(r2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r3)", "")) ? 0.00 : dt1.Compute("sum(r3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFtcostls")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tcost)", "")) ? 0.00 : dt1.Compute("sum(tcost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r4)", "")) ? 0.00 : dt1.Compute("sum(r4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r5)", "")) ? 0.00 : dt1.Compute("sum(r5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r6)", "")) ? 0.00 : dt1.Compute("sum(r6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls7")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r7)", "")) ? 0.00 : dt1.Compute("sum(r7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls8")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r8)", "")) ? 0.00 : dt1.Compute("sum(r8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls9")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r9)", "")) ? 0.00 : dt1.Compute("sum(r9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls10")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r10)", "")) ? 0.00 : dt1.Compute("sum(r10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls11")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r1)", "")) ? 0.00 : dt1.Compute("sum(r11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls12")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r12)", "")) ? 0.00 : dt1.Compute("sum(r12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFRls13")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r13)", "")) ? 0.00 : dt1.Compute("sum(r13)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFtoCostls")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(toramt)", "")) ? 0.00 : dt1.Compute("sum(toramt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvLcStatus.FooterRow.FindControl("lgvFnetpositionls")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netamt)", "")) ? 0.00 : dt1.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "MProStatus":
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r1)", "")) ? 0.00 : dt1.Compute("sum(r1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r2)", "")) ? 0.00 : dt1.Compute("sum(r2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r3)", "")) ? 0.00 : dt1.Compute("sum(r3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r4)", "")) ? 0.00 : dt1.Compute("sum(r4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r5)", "")) ? 0.00 : dt1.Compute("sum(r5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r6)", "")) ? 0.00 : dt1.Compute("sum(r6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR7")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r7)", "")) ? 0.00 : dt1.Compute("sum(r7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR8")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r8)", "")) ? 0.00 : dt1.Compute("sum(r8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR9")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r9)", "")) ? 0.00 : dt1.Compute("sum(r9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR10")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r10)", "")) ? 0.00 : dt1.Compute("sum(r10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR11")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r1)", "")) ? 0.00 : dt1.Compute("sum(r11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR12")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r12)", "")) ? 0.00 : dt1.Compute("sum(r12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR13")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r13)", "")) ? 0.00 : dt1.Compute("sum(r13)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR14")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r14)", "")) ? 0.00 : dt1.Compute("sum(r14)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR15")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r15)", "")) ? 0.00 : dt1.Compute("sum(r15)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR16")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r16)", "")) ? 0.00 : dt1.Compute("sum(r16)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR17")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r17)", "")) ? 0.00 : dt1.Compute("sum(r17)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR18")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r18)", "")) ? 0.00 : dt1.Compute("sum(r18)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR19")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r19)", "")) ? 0.00 : dt1.Compute("sum(r19)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR20")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r20)", "")) ? 0.00 : dt1.Compute("sum(r20)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtoCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(toramt)", "")) ? 0.00 : dt1.Compute("sum(toramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtoCollection")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tocollamt)", "")) ? 0.00 : dt1.Compute("sum(tocollamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFnetposition")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netamt)", "")) ? 0.00 : dt1.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "RptOrdProVsShip":


                    //dt4 = dt1.Copy();
                    //     DataView dv = dt1.DefaultView;
                    //    dv.RowFilter = ("pactcode not like '%AAAA%'");
                    //    dt4 = dv.ToTable();

                    ((Label)this.gvOrderrec.FooterRow.FindControl("lgvForderamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordramt)", "")) ?
                            0 : dt1.Compute("sum(ordramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOrderrec.FooterRow.FindControl("lgvFproamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(proamt)", "")) ?
                                    0 : dt1.Compute("sum(proamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOrderrec.FooterRow.FindControl("lgvFshipamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(shipamt)", "")) ?
                                    0 : dt1.Compute("sum(shipamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvOrderrec.FooterRow.FindControl("lgvForderrecqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordrqty)", "")) ?
                            0 : dt1.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOrderrec.FooterRow.FindControl("lgvFproqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(proqty)", "")) ?
                                    0 : dt1.Compute("sum(proqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOrderrec.FooterRow.FindControl("lgvFshipqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(shipqty)", "")) ?
                                    0 : dt1.Compute("sum(shipqty)", ""))).ToString("#,##0;(#,##0); ");


                    break;

                case "LcDetails":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("buyerid='AAAAAAAAAAAA' ");
                    dt4 = dv1.ToTable();
                    ((Label)this.gvLcDetails.FooterRow.FindControl("lgvFordramtlc")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(ordramt)", "")) ?
                                   0 : dt4.Compute("sum(ordramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcDetails.FooterRow.FindControl("lgvFbblcTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(bblcamt)", "")) ?
                                   0 : dt4.Compute("sum(bblcamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvLcDetails.FooterRow.FindControl("lgvFlcpayable")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(payable)", "")) ?
                                   0 : dt4.Compute("sum(payable)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcDetails.FooterRow.FindControl("lgvFexpamtlc")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(shipamt)", "")) ?
                                   0 : dt4.Compute("sum(shipamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;



                case "LcSummary":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("buyerid='AAAAAAAAAAAA' ");
                    dt4 = dv1.ToTable();
                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFordramtlcsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(lcval)", "")) ?
                                   0 : dt4.Compute("sum(lcval)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFexpamtlcsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(shipamt)", "")) ?
                                    0 : dt4.Compute("sum(shipamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFexbalsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(ordrbal)", "")) ?
                                     0 : dt4.Compute("sum(ordrbal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFrealizesum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(rlzamt)", "")) ?
                                     0 : dt4.Compute("sum(rlzamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFsrealizesum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(shipbal)", "")) ?
                                     0 : dt4.Compute("sum(shipbal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFfcheldsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(fcheld)", "")) ?
                                     0 : dt4.Compute("sum(fcheld)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFbblcTotalsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(ordrval)", "")) ?
                                   0 : dt4.Compute("sum(ordrval)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFbblcrcvsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(mrramt)", "")) ?
                                   0 : dt4.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFbblcpaymentsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(payment)", "")) ?
                                  0 : dt4.Compute("sum(payment)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFbblcpaymentsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(payable)", "")) ?
                                   0 : dt4.Compute("sum(payable)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvLcSummary.FooterRow.FindControl("lgvFeosfcheldsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(eosfcheld)", "")) ?
                                   0 : dt4.Compute("sum(eosfcheld)", ""))).ToString("#,##0;(#,##0); ");



                    break;


                case "BgdLCStatus":

                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFtQtybgd")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordrqty)", "")) ? 0.00 : dt1.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFordramtlcbgd")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordramt)", "")) ? 0.00 : dt1.Compute("sum(ordramt)", ""))).ToString("#,##0;(#,##0); ");



                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFshiqtybgd")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(shiqty)", "")) ? 0.00 : dt1.Compute("sum(shiqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFseqtybgd")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(seqty)", "")) ? 0.00 : dt1.Compute("sum(seqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFtramtbgd")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tramt)", "")) ? 0.00 : dt1.Compute("sum(tramt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r1)", "")) ? 0.00 : dt1.Compute("sum(r1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r2)", "")) ? 0.00 : dt1.Compute("sum(r2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r3)", "")) ? 0.00 : dt1.Compute("sum(r3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFtcostlsbgd")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tcost)", "")) ? 0.00 : dt1.Compute("sum(tcost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r4)", "")) ? 0.00 : dt1.Compute("sum(r4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r5)", "")) ? 0.00 : dt1.Compute("sum(r5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r6)", "")) ? 0.00 : dt1.Compute("sum(r6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd7")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r7)", "")) ? 0.00 : dt1.Compute("sum(r7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd8")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r8)", "")) ? 0.00 : dt1.Compute("sum(r8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd9")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r9)", "")) ? 0.00 : dt1.Compute("sum(r9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd10")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r10)", "")) ? 0.00 : dt1.Compute("sum(r10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd11")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r1)", "")) ? 0.00 : dt1.Compute("sum(r11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd12")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r12)", "")) ? 0.00 : dt1.Compute("sum(r12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFRlsbgd13")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r13)", "")) ? 0.00 : dt1.Compute("sum(r13)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFtoCostlsbgd")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(toramt)", "")) ? 0.00 : dt1.Compute("sum(toramt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvbgdLcStatus.FooterRow.FindControl("lgvFnetpositionlsbgd")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netamt)", "")) ? 0.00 : dt1.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;



            }



        }



        protected void gvCollDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvCollDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }


        protected void gvCollDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label udesc = (Label)e.Row.FindControl("lgvudesc");
                Label cashamt = (Label)e.Row.FindControl("lgvcashamt");
                Label chqamt = (Label)e.Row.FindControl("lgvchqamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    udesc.Font.Bold = true;
                    cashamt.Font.Bold = true;
                    chqamt.Font.Bold = true;
                    udesc.Style.Add("text-align", "right");


                }

            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void btnRecDesc_Click(object sender, EventArgs e)
        {


            string recpcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["tblcollvscl"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "recpcode like('" + recpcode + "')";
            dt = dv1.ToTable();


            string mCOMCOD = this.Request.QueryString["comcod"].ToString();
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString();
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
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            else if (lebel2 == "")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }


        }
        protected void btnPayDesc_Click(object sender, EventArgs e)
        {
            string paycode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["tblcollvscl"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "paycode like('" + paycode + "')";
            dt = dv1.ToTable();

            string mCOMCOD = this.Request.QueryString["comcod"].ToString();
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString();
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
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            else if (lebel2 == "")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
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

                if (ASTUtility.Right(code1, 8) == "00000000")
                {

                    HyRecDesc.Font.Bold = true;
                    lgvRecAmt.Font.Bold = true;
                }
                if (ASTUtility.Right(code2, 8) == "00000000")
                {
                    HyPayDesc.Font.Bold = true;
                    lgvPayAmt.Font.Bold = true;
                }

            }
        }
        protected void gvBankPosition_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label AccDesc = (Label)e.Row.FindControl("lblgvDescbank");
                Label opnbal = (Label)e.Row.FindControl("lblgvopnbal");
                Label opnliabilities = (Label)e.Row.FindControl("lblgvopnliabilities");
                Label Dramtbank = (Label)e.Row.FindControl("lblgvDramtbank");
                Label Cramtbank = (Label)e.Row.FindControl("lblgvCramtbank");
                Label clobalbank = (Label)e.Row.FindControl("lblgvclobalbank");
                Label cloliabilities = (Label)e.Row.FindControl("lblgvcloliabilities");
                Label bankLim = (Label)e.Row.FindControl("lblgvbankLim");
                Label bankBal = (Label)e.Row.FindControl("lblgvbankBal");




                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();




                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    AccDesc.Font.Bold = true;
                    opnbal.Font.Bold = true;
                    opnliabilities.Font.Bold = true;
                    Dramtbank.Font.Bold = true;
                    Cramtbank.Font.Bold = true;
                    clobalbank.Font.Bold = true;
                    cloliabilities.Font.Bold = true;
                    bankLim.Font.Bold = true;
                    bankBal.Font.Bold = true;
                    //lgvRecAmt.Font.Bold = true;
                    AccDesc.Style.Add("text-align", "right");
                }


            }


        }
        protected void gvMMPlanVsAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvSalVsColl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartment");
                Label lgvmonsalamt = (Label)e.Row.FindControl("lgvmonsalamt");
                Label lgvmoncollamt = (Label)e.Row.FindControl("lgvmoncollamt");
                Label lgvtsalamt = (Label)e.Row.FindControl("lgvtsalamt");
                Label lgvtcollamt = (Label)e.Row.FindControl("lgvtcollamt");
                Label lgvtatsaleamt = (Label)e.Row.FindControl("lgvtatsaleamt");
                Label lgvtaothsaleamt = (Label)e.Row.FindControl("lgvtaothsaleamt");
                Label lgvtatosaleamt = (Label)e.Row.FindControl("lgvtatosaleamt");
                Label lgvtatcollamt = (Label)e.Row.FindControl("lgvtatcollamt");
                Label lgvtaothcollamt = (Label)e.Row.FindControl("lgvtaothcollamt");
                Label lgvtatocollamt = (Label)e.Row.FindControl("lgvtatocollamt");
                Label lgvuatsalamt = (Label)e.Row.FindControl("lgvuatsalamt");
                Label lgvuaothsalamt = (Label)e.Row.FindControl("lgvuaothsalamt");
                Label lgvuatosaleamt = (Label)e.Row.FindControl("lgvuatosaleamt");
                Label lgvuatcollamt = (Label)e.Row.FindControl("lgvuatcollamt");
                Label lgvuaothcollamt = (Label)e.Row.FindControl("lgvuaothcollamt");
                Label lgvuatocollamt = (Label)e.Row.FindControl("lgvuatocollamt");
                Label lgvpmonsalamt = (Label)e.Row.FindControl("lgvpmonsalamt");
                Label lgvpmoncollamt = (Label)e.Row.FindControl("lgvpmoncollamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {

                    Deptdesc.Font.Bold = true;
                    lgvmonsalamt.Font.Bold = true;
                    lgvmoncollamt.Font.Bold = true;
                    lgvtsalamt.Font.Bold = true;
                    lgvtcollamt.Font.Bold = true;
                    lgvtatsaleamt.Font.Bold = true;
                    lgvtaothsaleamt.Font.Bold = true;
                    lgvtatosaleamt.Font.Bold = true;
                    lgvtatcollamt.Font.Bold = true;
                    lgvtaothcollamt.Font.Bold = true;
                    lgvtatocollamt.Font.Bold = true;
                    lgvuatsalamt.Font.Bold = true;
                    lgvuaothsalamt.Font.Bold = true;
                    lgvuatosaleamt.Font.Bold = true;
                    lgvuatcollamt.Font.Bold = true;
                    lgvuaothcollamt.Font.Bold = true;
                    lgvuatocollamt.Font.Bold = true;
                    lgvpmonsalamt.Font.Bold = true;
                    lgvpmoncollamt.Font.Bold = true;
                    Deptdesc.Style.Add("text-align", "right");


                }

            }
        }

        protected void gvgrpchqissued_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label resdesc = (Label)e.Row.FindControl("lgvresdescgp");
                Label amt = (Label)e.Row.FindControl("lgvpayam");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (code == "TTTT")
                {
                    resdesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    resdesc.Style.Add("text-align", "right");

                }


            }
        }
        protected void dgvAccRec03_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HLgvDesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label lgvtstkamal = (Label)e.Row.FindControl("lgvtstkamal");
                Label lgvusunitsizeal = (Label)e.Row.FindControl("lgvusunitsizeal");
                Label lgvusuamtal = (Label)e.Row.FindControl("lgvusuamtal");
                Label lgvunitsizeal = (Label)e.Row.FindControl("lgvunitsizeal");
                Label lgvaptcostal = (Label)e.Row.FindControl("lgvaptcostal");
                Label lgvcpaocostal = (Label)e.Row.FindControl("lgvcpaocostal");
                Label lgvtocsotal = (Label)e.Row.FindControl("lgvtocsotal");
                Label lgvEncashal = (Label)e.Row.FindControl("lgvEncashal");
                Label lgvtretamtal = (Label)e.Row.FindControl("lgvtretamtal");
                Label lgvtframtal = (Label)e.Row.FindControl("lgvtframtal");
                Label lgvtpdamtal = (Label)e.Row.FindControl("lgvtpdamtal");
                Label lgvtotreceivedal = (Label)e.Row.FindControl("lgvtotreceivedal");
                Label lgvtatoduesall = (Label)e.Row.FindControl("lgvtatoduesall");
                Label lgvtotalduesal = (Label)e.Row.FindControl("lgvtotalduesal");
                Label lgvtoduesal = (Label)e.Row.FindControl("lgvtoduesal");
                Label lgvpbduesal = (Label)e.Row.FindControl("lgvpbduesal");
                Label lgvpinsduesall = (Label)e.Row.FindControl("lgvpinsduesall");
                Label lgvCbookingal = (Label)e.Row.FindControl("lgvCbookingal");
                Label lgvCinstallmental = (Label)e.Row.FindControl("lgvCinstallmental");
                Label lgvCoCInstalmental = (Label)e.Row.FindControl("lgvCoCInstalmental");
                Label lgvvbaamtal = (Label)e.Row.FindControl("lgvvbaamtal");
                Label lgvdelchargeal = (Label)e.Row.FindControl("lgvdelchargeal");
                Label lgvdischargeal = (Label)e.Row.FindControl("lgvdischargeal");
                Label lgvnettoduesal = (Label)e.Row.FindControl("lgvnettoduesal");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HLgvDesc.Font.Bold = true;
                    lgvtstkamal.Font.Bold = true;
                    lgvusunitsizeal.Font.Bold = true;
                    lgvusuamtal.Font.Bold = true;
                    lgvunitsizeal.Font.Bold = true;
                    lgvaptcostal.Font.Bold = true;
                    lgvcpaocostal.Font.Bold = true;
                    lgvtocsotal.Font.Bold = true;
                    lgvEncashal.Font.Bold = true;
                    lgvtretamtal.Font.Bold = true;
                    lgvtframtal.Font.Bold = true;
                    lgvtpdamtal.Font.Bold = true;
                    lgvtotreceivedal.Font.Bold = true;
                    lgvtatoduesall.Font.Bold = true;
                    lgvtotalduesal.Font.Bold = true;
                    lgvtoduesal.Font.Bold = true;
                    lgvpbduesal.Font.Bold = true;
                    lgvpinsduesall.Font.Bold = true;
                    lgvCbookingal.Font.Bold = true;
                    lgvCinstallmental.Font.Bold = true;
                    lgvCoCInstalmental.Font.Bold = true;
                    lgvCbookingal.Font.Bold = true;
                    lgvvbaamtal.Font.Bold = true;
                    lgvdelchargeal.Font.Bold = true;
                    lgvdischargeal.Font.Bold = true;
                    lgvnettoduesal.Font.Bold = true;
                    // actdesc.Style.Add("text-align", "right");


                }

                else
                {
                    string comcod = this.Request.QueryString["comcod"].ToString();
                    string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                    string frmdate = this.Request.QueryString["Date1"].ToString();
                    string todate = this.Request.QueryString["Date2"].ToString();
                    HLgvDesc.NavigateUrl = "~/F_45_GrAcc/LinkGrpRptSaleDues.aspx?Type=DuesCollect&comcod=" + comcod + "&pactcode=" + code + "&pactdesc=" + pactdesc + "&Date1=" + frmdate + "&Date2=" + todate;


                }

            }


        }
        protected void dgvAccRec03_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvAccRec03.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvFeaAllPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDescfea");
                Label lgvoRev = (Label)e.Row.FindControl("lgvoRev");
                Label logvCost = (Label)e.Row.FindControl("logvCost");
                Label logvmargin = (Label)e.Row.FindControl("logvmargin");
                Label lgvperorCost = (Label)e.Row.FindControl("lgvperorCost");
                Label lgvRev = (Label)e.Row.FindControl("lgvRev");
                Label lgvCost = (Label)e.Row.FindControl("lgvCost");
                Label lgvProfit = (Label)e.Row.FindControl("lgvProfit");
                Label lgvperCost = (Label)e.Row.FindControl("lgvperCost");
                Label lgvPerRev = (Label)e.Row.FindControl("lgvPerRev");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    lgvoRev.Font.Bold = true;
                    logvCost.Font.Bold = true;
                    logvmargin.Font.Bold = true;
                    lgvperorCost.Font.Bold = true;

                    lgvRev.Font.Bold = true;
                    lgvCost.Font.Bold = true;
                    lgvProfit.Font.Bold = true;
                    lgvperCost.Font.Bold = true;
                    lgvPerRev.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");


                }

            }




            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string comcod = this.Request.QueryString["comcod"].ToString();
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescfea");
            string Actcode = ((Label)e.Row.FindControl("lgvInfoCode")).Text;
            string ActDesc = ((Label)e.Row.FindControl("lgvInfodesc")).Text;
            if (ASTUtility.Right(Actcode, 4) == "AAAA")
                return;
            hlink1.NavigateUrl = "LinkGrpFeaIncomeSt.aspx?comcod=" + comcod + "&actcode=" + Actcode + "&actdesc=" + ActDesc;

        }
        protected void gvgpnp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblgvProjectgn");
                Label conarea = (Label)e.Row.FindControl("lblgvconarea");
                Label salamt = (Label)e.Row.FindControl("lblgvsaleamt");
                Label conscost = (Label)e.Row.FindControl("lblgvconsct");
                Label binterest = (Label)e.Row.FindControl("lblgvbinterest");
                Label lblgvtconabin = (Label)e.Row.FindControl("lblgvtconabin");
                Label lcost = (Label)e.Row.FindControl("lblgvlcost");
                Label lblgvcoffund = (Label)e.Row.FindControl("lblgvcoffund");
                Label lblgvtocfaland = (Label)e.Row.FindControl("lblgvtocfaland");
                Label adcost = (Label)e.Row.FindControl("lblgvadcost");

                Label tprcost = (Label)e.Row.FindControl("lblgvtprcost");
                Label gprofit = (Label)e.Row.FindControl("lblgvgp");

                Label ovrhead = (Label)e.Row.FindControl("lblgvovrhead");
                Label rfund = (Label)e.Row.FindControl("lblgvrfund");
                Label topcost = (Label)e.Row.FindControl("lblgvtopcost");
                Label tocost = (Label)e.Row.FindControl("lblgvtocost");
                Label nprofit = (Label)e.Row.FindControl("lblgvnp");
                Label peroncost = (Label)e.Row.FindControl("lblgvperoncost");
                Label peronsl = (Label)e.Row.FindControl("lblgvperonsl");
                Label npperoncost = (Label)e.Row.FindControl("lblgvnpperoncost");
                Label vnpperonsl = (Label)e.Row.FindControl("lblgvnpperonsl");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    conarea.Font.Bold = true;
                    salamt.Font.Bold = true;
                    conscost.Font.Bold = true;
                    binterest.Font.Bold = true;
                    lblgvtconabin.Font.Bold = true;
                    lcost.Font.Bold = true;
                    lblgvcoffund.Font.Bold = true;
                    lblgvtocfaland.Font.Bold = true;
                    adcost.Font.Bold = true;
                    tprcost.Font.Bold = true;
                    gprofit.Font.Bold = true;

                    ovrhead.Font.Bold = true;
                    rfund.Font.Bold = true;
                    topcost.Font.Bold = true;
                    nprofit.Font.Bold = true;
                    peroncost.Font.Bold = true;
                    peronsl.Font.Bold = true;
                    npperoncost.Font.Bold = true;
                    vnpperonsl.Font.Bold = true;

                    actdesc.Style.Add("text-align", "right");


                }

            }


        }
        protected void grvPrjStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescps");
                Label lgvTSVal = (Label)e.Row.FindControl("lgvTSVal");
                Label lgvTmonSVal = (Label)e.Row.FindControl("lgvTmonSVal");
                Label lgvTReSVal = (Label)e.Row.FindControl("lgvTReSVal");
                Label lgvNOI = (Label)e.Row.FindControl("lgvNOI");
                Label lgvRecamt = (Label)e.Row.FindControl("lgvRecamt");
                Label lgvBRecSalamt = (Label)e.Row.FindControl("lgvBRecSalamt");
                Label lgvExpAmt = (Label)e.Row.FindControl("lgvExpAmt");
                Label lgvPAdvAmt = (Label)e.Row.FindControl("lgvPAdvAmt");
                Label lgvLCNFAmt = (Label)e.Row.FindControl("lgvLCNFAmt");
                Label lgvOvmt = (Label)e.Row.FindControl("lgvOvmt");
                Label lgvIAmt = (Label)e.Row.FindControl("lgvIAmt");
                Label lgvtExp = (Label)e.Row.FindControl("lgvtExp");
                Label lgvLibAmt = (Label)e.Row.FindControl("lgvLibAmt");
                Label lgvLframt = (Label)e.Row.FindControl("lgvLframt");
                Label lgvLtoamt = (Label)e.Row.FindControl("lgvLtoamt");
                Label lgvRLamt = (Label)e.Row.FindControl("lgvRLamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string mCOMCOD = this.Request.QueryString["comcod"].ToString();
                string mPACTCODE = ((Label)e.Row.FindControl("lblActcode")).Text;
                string mTRNDAT1 = this.Request.QueryString["date"].ToString();
                //------------------------------//////
                Label actcode = (Label)e.Row.FindControl("lblgvcode");
                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                hlink1.NavigateUrl = "LinkProjectCollBrkDown.aspx?Type=IndPrjStDet&comcod=" + mCOMCOD + "&pactcode=" + mPACTCODE + "&Date1=" + mTRNDAT1;

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    hlink1.Font.Bold = true;
                    lgvTSVal.Font.Bold = true;
                    lgvTmonSVal.Font.Bold = true;
                    lgvTReSVal.Font.Bold = true;
                    lgvNOI.Font.Bold = true;
                    lgvRecamt.Font.Bold = true;
                    lgvBRecSalamt.Font.Bold = true;
                    lgvExpAmt.Font.Bold = true;
                    lgvPAdvAmt.Font.Bold = true;
                    lgvLCNFAmt.Font.Bold = true;
                    lgvOvmt.Font.Bold = true;
                    lgvIAmt.Font.Bold = true;
                    lgvtExp.Font.Bold = true;
                    lgvLibAmt.Font.Bold = true;
                    lgvLframt.Font.Bold = true;
                    lgvLtoamt.Font.Bold = true;
                    lgvRLamt.Font.Bold = true;

                    hlink1.Style.Add("text-align", "right");
                }

            }


        }

        protected void gvRptBBLCPay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRptBBLCPay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvpdc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HLgvDescpaysum = (HyperLink)e.Row.FindControl("HLgvDescpaysum");
                Label toamtpdc = (Label)e.Row.FindControl("lgvtoamtpdc");
                Label dueam = (Label)e.Row.FindControl("lgvdueam");
                Label pdcam = (Label)e.Row.FindControl("lgvpdc");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pgrp")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HLgvDescpaysum.Font.Bold = true;
                    toamtpdc.Font.Bold = true;
                    dueam.Font.Bold = true;
                    pdcam.Font.Bold = true;
                    HLgvDescpaysum.Style.Add("text-align", "right");
                }


                else
                {
                    HLgvDescpaysum.Style.Add("color", "blue");
                    string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                    HLgvDescpaysum.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PDCStatus&comcod=" + comcod + "&actcode=" + code + "&grp=" + grp + "&Date2=" + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");

                }




            }
        }

        protected void gvOrdexarlz_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrdexarlz.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvDayWSale_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblgvDPactdesc");
                Label bgdamt = (Label)e.Row.FindControl("lgvDTAmt");
                Label salamt = (Label)e.Row.FindControl("lgvDSAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    bgdamt.Font.Bold = true;
                    salamt.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");
                }

            }
        }





        protected void gvLcStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink actdesc = (HyperLink)e.Row.FindControl("hlnkgvActDescls");

                string comcod = this.Request.QueryString["comcod"].ToString();

                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string mlcdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc")).ToString(); ;
                string date = this.Request.QueryString["date"].ToString();
                actdesc.Style.Add("color", "blue");

                actdesc.NavigateUrl = "~/F_35_GrAcc/LinkLCIncomeStatement.aspx?Type=LCStatus&comcod=" + comcod + "&mlccod=" + mlccod + "&mlcdesc=" + mlcdesc + "&date=" + date;



            }
        }

        protected void gvOrderrec_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrderrec.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvLcDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLcDetails.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvLcDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lgvlcnameoerlc");
                Label ordramt = (Label)e.Row.FindControl("lgvordramtoerlc");
                Label bblctotal = (Label)e.Row.FindControl("lgvbblctotallc");
                Label lcpayable = (Label)e.Row.FindControl("lgvlcpayable");
                Label expamtlc = (Label)e.Row.FindControl("lgvexpamtoerlc");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mmlccod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    ordramt.Font.Bold = true;
                    bblctotal.Font.Bold = true;
                    lcpayable.Font.Bold = true;
                    expamtlc.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");
                }

            }
        }
        protected void gvLcSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLcSummary.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvLcSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink buyer = (HyperLink)e.Row.FindControl("hlnkgvbuyeroerlcsum");
                Label actdesc = (Label)e.Row.FindControl("lgvlcnameoerlcsum");
                Label lcval = (Label)e.Row.FindControl("lgvordramtoerlcsum");
                Label examtlc = (Label)e.Row.FindControl("lgvexpamtoerlcsum");
                Label exbal = (Label)e.Row.FindControl("lgvexbalsum");
                Label realize = (Label)e.Row.FindControl("lgvrealizesum");
                Label srealize = (Label)e.Row.FindControl("lgvsrealizesum");
                Label fcheldsum = (Label)e.Row.FindControl("lgvfcheldsum");
                Label bblctotal = (Label)e.Row.FindControl("lgvbblctotallcsum");
                Label bblcpayment = (Label)e.Row.FindControl("lgvbblcpaymentsum");
                Label bblcrcv = (Label)e.Row.FindControl("lgvbblcrcvsum");
                Label lcpayable = (Label)e.Row.FindControl("lgvlcpayablesum");
                Label eosfcheld = (Label)e.Row.FindControl("lgveosfcheldsum");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mmlccod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    lcval.Font.Bold = true;
                    examtlc.Font.Bold = true;
                    exbal.Font.Bold = true;
                    realize.Font.Bold = true;
                    srealize.Font.Bold = true;
                    fcheldsum.Font.Bold = true;
                    bblctotal.Font.Bold = true;
                    bblcpayment.Font.Bold = true;
                    bblcrcv.Font.Bold = true;
                    lcpayable.Font.Bold = true;
                    eosfcheld.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");
                }
                else
                {
                    string comcod = this.Request.QueryString["comcod"].ToString();
                    string buyerid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "buyerid")).ToString();
                    string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mmlccod")).ToString();
                    string Date = this.Request.QueryString["Date"].ToString();
                    buyer.NavigateUrl = "~/F_35_GrAcc/LinkGrpMisDailyActivities.aspx?Type=LcDetails&comcod=" + comcod + "&Date=" + Date + "&buyerid=" + buyerid + "&mlccod=" + mlccod;



                }

            }
        }
        protected void gvbgdLcStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink actdesc = (HyperLink)e.Row.FindControl("hlnkgvActDesclsbgd");

                string comcod = this.Request.QueryString["comcod"].ToString();

                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string mlcdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc")).ToString(); ;
                string date = this.Request.QueryString["date"].ToString();
                actdesc.Style.Add("color", "blue");

                actdesc.NavigateUrl = "~/F_35_GrAcc/LinkLCIncomeStatement.aspx?Type=LCStatus&comcod=" + comcod + "&mlccod=" + mlccod + "&mlcdesc=" + mlcdesc + "&date=" + date;



            }

        }
    }
}