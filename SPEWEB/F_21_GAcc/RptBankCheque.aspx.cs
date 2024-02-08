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


namespace SPEWEB.F_21_GAcc
{
    public partial class RptBankCheque : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString().Trim();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = date;




                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "ChquedepPrint") ? "Chequed Deposit Print" : (type == "ToDayIssChq") ? "Cheque Issue  Vs. Clearence"
               : (type == "CliModNotYet") ? "Pending Client Modification" : (type == "CollChqSt") ? "Collection Cheque Status"
               : (type == "PayChqCl") ? "Payment Cheque Status"
               : (type == "CashFlow") ? "Statement of Cash Flow (Direct)"
               : (type == "FundFlow") ? "Statement Fund Flow (Direct)"
               : (type == "CashFlow02") ? "Statement Of Cash Flow (Indirect)"
               : (type == "CashFlow03") ? "Statement Of Cash Flow"

               : (type == "PostChqInHand") ? "List Of Post Dated Cheque"
               : (type == "IsuVsClr") ? "Cheque Issue Vs. Clearance"
               : (type == "FinNote") ? "Explanatory Notes to the Financial Statements" : "Cheque In Hand Report";


                this.SelectView();
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
        private void GetBankName()
        {
            string comcod = this.GetCompCode();
            string SearchBank = "%" + this.txtSerchBank.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "GETBANKCODE", SearchBank, "", "", "", "", "", "", "", "");
            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1;
            this.ddlBankName.DataBind();
            ds1.Dispose();



        }
        private void GetBankName1()
        {
            string comcod = this.GetCompCode();
            string SearchBank = "%" + this.txtSerchBank1.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "GETBANKCODE", SearchBank, "", "", "", "", "", "", "", "");
            this.ddlBankName1.DataTextField = "actdesc";
            this.ddlBankName1.DataValueField = "actcode";
            this.ddlBankName1.DataSource = ds1;
            this.ddlBankName1.DataBind();
            ds1.Dispose();



        }

        protected void ibtnSrchBank_Click(object sender, EventArgs e)
        {
            this.GetBankName();
        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChquedepPrint":
                    this.GetBankName();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "ChqInHand":
                    this.chkDeposit.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "ToDayIssChq":
                    this.GetBankName1();
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "CliModNotYet":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.lblChqNo.Visible = false;
                    this.txtSrchChequeno.Visible = false;
                    this.imgSearchCheque.Visible = false;
                    break;
                case "CollChqSt":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.Panel1.Visible = false;
                    this.ShowCollChqSt();
                    break;
                case "PayChqCl":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.Panel1.Visible = false;
                    this.ShowChequeHistory();
                    //this.CreateTable();
                    break;
                case "CashFlow":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.lblChqNo.Visible = false;
                    this.txtSrchChequeno.Visible = false;
                    this.imgSearchCheque.Visible = false;
                    this.lblOpeningDate.Visible = true;
                    this.txtOpeningDate.Visible = true;
                    this.lblgroup.Visible = true;
                    this.ddlRptGroup.Visible = true;
                    this.txtOpeningDate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("dd-MMM-yyyy");
                    break;

                case "PostChqInHand":
                    this.MultiView1.ActiveViewIndex = 7;
                    this.lblFdate.Visible = false;
                    this.txtfromdate.Visible = false;
                    this.lblTdate.Text = "Date:";
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    break;
                case "IsuVsClr":
                    this.MultiView1.ActiveViewIndex = 8;
                    this.lblChqNo.Visible = false;
                    this.txtSrchChequeno.Visible = false;
                    this.imgSearchCheque.Visible = false;
                    break;

                case "FundFlow":
                    this.MultiView1.ActiveViewIndex = 9;
                    this.lblChqNo.Visible = false;
                    this.txtSrchChequeno.Visible = false;
                    this.imgSearchCheque.Visible = false;
                    break;

                case "CashFlow02":
                case "CashFlow03":
                    this.MultiView1.ActiveViewIndex = 10;
                    this.lblChqNo.Visible = false;
                    this.txtSrchChequeno.Visible = false;
                    this.imgSearchCheque.Visible = false;
                    this.lblOpeningDate.Visible = true;
                    this.txtOpeningDate.Visible = true;
                    this.txtOpeningDate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("dd-MMM-yyyy");
                    break;


                case "FinNote":
                    this.Panel1.Visible = false;
                    this.MultiView1.ActiveViewIndex = 11;
                    this.lblChqNo.Visible = false;
                    this.txtSrchChequeno.Visible = false;
                    this.imgSearchCheque.Visible = false;
                    break;


            }
        }

        private void CreateTable()
        {
            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("slno", Type.GetType("System.Double"));
            dttemp.Columns.Add("voudat", Type.GetType("System.DateTime"));
            dttemp.Columns.Add("vounum", Type.GetType("System.String"));
            dttemp.Columns.Add("isunum", Type.GetType("System.String"));
            dttemp.Columns.Add("payto", Type.GetType("System.String"));
            dttemp.Columns.Add("actdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("resdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("varnar", Type.GetType("System.String"));
            dttemp.Columns.Add("cactdesc", Type.GetType("System.String"));
            dttemp.Columns.Add("chequeno", Type.GetType("System.String"));
            dttemp.Columns.Add("chequedat", Type.GetType("System.DateTime"));
            dttemp.Columns.Add("recndt", Type.GetType("System.String"));
            dttemp.Columns.Add("trnam", Type.GetType("System.Double"));
            Session["tblbdeposit"] = dttemp;

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ChquedepPrint":
                    this.BankDepositReport();
                    break;
                case "ChqInHand":
                    this.ChqInHandReport();
                    break;
                case "ToDayIssChq":
                    this.RtpToDayIssChq();
                    break;
                case "CliModNotYet":
                    this.RptClientModNotYet();
                    break;
                case "CollChqSt":
                    this.RptCollChqSt();
                    break;
                case "PayChqCl":
                    this.PrintPmntChClearance();
                    break;

                case "PostChqInHand":
                    this.ReportPostDatedCheque();
                    break;
                case "IsuVsClr":
                    this.RtpIssVsClr();
                    break;

                case "FundFlow":
                    this.PrintFundFlow();
                    break;
                case "CashFlow":
                case "CashFlow02":
                case "CashFlow03":
                    this.RptCashFlow02();
                    break;

                case "FinNote":
                    this.ShowFinNote();
                    this.RptFinancialNote();
                    break;

            }
        }
        private void BankDepositReport()
        {

            if (this.ddlBankName.SelectedValue == "000000000000")
            {
                this.ReportDepositAllBank();
                return;

            }
            this.ReportDepositBank();




        }

        private void ReportDepositAllBank()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_21_GAcc.RptChequeDepositAllBank();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Transaction Statement";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        private void ReportDepositBank()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_21_GAcc.RptChequeDepositBank02();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;


            //TextObject rpttxtBankName = rptstate.ReportDefinition.ReportObjects["txtBankName"] as TextObject;
            //rpttxtBankName.Text = this.ddlBankName.SelectedItem.Text.Trim();
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Transaction Statement";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void ChqInHandReport()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_21_GAcc.RptChequeInHand();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;


            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Transaction Statement";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void RtpToDayIssChq()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblbdeposit"];
            //ReportDocument rptsale = new RMGiRPT.R_21_GAcc.rptIssueCheque();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptTotal = rptsale.ReportDefinition.ReportObjects["txtTamt"] as TextObject;
            //rptTotal.Text = ((Label)this.grvToDayIssChq.FooterRow.FindControl("lgvCrAmt")).Text;

            //TextObject txttoreconamt = rptsale.ReportDefinition.ReportObjects["txttoreconamt"] as TextObject;
            //txttoreconamt.Text = ((Label)this.grvToDayIssChq.FooterRow.FindControl("lgvFReconAmt")).Text;

            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;




            //rptDate.Text = "From : " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptClientModNotYet()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_21_GAcc.RptPendingCliMod();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;


            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Transaction Statement";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptCollChqSt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_21_GAcc.RptCollChqStatus();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Transaction Statement";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintPmntChClearance()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstate = new RMGiRPT.R_21_GAcc.RptPaymentChqClearance();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptftdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");

            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Transaction Statement";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintFundFlow()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_21_GAcc.RptFundFlow();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;


            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Fund Flow";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void ReportPostDatedCheque()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_21_GAcc.RptPostDatedChqInHand();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;


            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Transaction Statement";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RtpIssVsClr()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblbdeposit"];
            //ReportDocument rptsale = new RMGiRPT.R_21_GAcc.rptIssueVsClr();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptIsu = rptsale.ReportDefinition.ReportObjects["txtIsuamt"] as TextObject;
            //rptIsu.Text = ((Label)this.gvIsuCleared.FooterRow.FindControl("lgvFIsuAmt")).Text;

            //TextObject txttCramt = rptsale.ReportDefinition.ReportObjects["txtCAmt"] as TextObject;
            //txttCramt.Text = ((Label)this.gvIsuCleared.FooterRow.FindControl("lgvFCuamt")).Text;

            //TextObject rptPr = rptsale.ReportDefinition.ReportObjects["txtPrAmt"] as TextObject;
            //rptPr.Text = ((Label)this.gvIsuCleared.FooterRow.FindControl("lgvFPramt")).Text;

            //TextObject txttTamt = rptsale.ReportDefinition.ReportObjects["txtAmt"] as TextObject;
            //txttTamt.Text = ((Label)this.gvIsuCleared.FooterRow.FindControl("lgvFPramt1")).Text;

            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;

            //rptDate.Text = "From : " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptCashFlow()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataTable dt1 = (DataTable)ViewState["recandpayNote"];

            //ReportDocument rptstate = new RMGiRPT.R_21_GAcc.RptCashFlow();

            //rptstate.Subreports["RptBankBalance.rpt"].SetDataSource(dt1);

            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "Date: " + fromdate + " To " + todate;



            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;


            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);


            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Cash Flow";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstate.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void RptCashFlow02()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //DataTable dt1 = (DataTable)ViewState["recandpayNote"];





            //ReportDocument rptstate = new RMGiRPT.R_21_GAcc.RptCashFlow02();

            //rptstate.Subreports["RptBankBalance02.rpt"].SetDataSource(dt1);

            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //   TextObject TxtHeader = rptstate.ReportDefinition.ReportObjects["TxtHeader"] as TextObject;
            //TxtHeader.Text =(this.Request.QueryString["Type"].ToString()=="CashFlow")?"Statement of Cash Flow -Direct":"Statement of Cash Flow -Indirect";

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "For the year ended " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd MMMM yyyy"); 
            ////rptftdate.Text = "Date: " + fromdate + " To " + todate;




            //TextObject txtCuramt = rptstate.ReportDefinition.ReportObjects["txtCuramt"] as TextObject;
            //txtCuramt.Text = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtPreamt = rptstate.ReportDefinition.ReportObjects["txtPreamt"] as TextObject;
            //txtPreamt.Text = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy"); 





            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;


            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Cash Flow";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}


            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string dateft = "From" + " " + frmdate + " " + "To" + " " + todate;
            string datefh = frmdate + " " + "To " + todate;

            string Opendate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            string group = this.ddlRptGroup.SelectedValue.ToString();


            DataTable dt = (DataTable)Session["tblbdeposit"];


            //var lst = ds.Tables[0].DataTableToList<SPEENTITY.C_09_C>();
            var lst = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassCashFlowD>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptCashFlow", lst, null, null);
            //rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("datefh", datefh));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("dateft", dateft));
            rpt1.SetParameters(new ReportParameter("Opendate", "Opening Date: " + Opendate));
            rpt1.SetParameters(new ReportParameter("group", "Group: " + group));

            rpt1.SetParameters(new ReportParameter("dateOt", Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy")));


            rpt1.SetParameters(new ReportParameter("RptTitle", "Statement Of Cash Flow(Direct)"));
            //rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptFinancialNote()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstate = new RMGiRPT.R_32_Mis.RptFinNoteInfo();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptftdate.Text = "Date: " + Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy"); 



            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);
            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChquedepPrint":
                    this.PrintDepositBank();
                    break;
                case "ChqInHand":
                    this.ShowCorCahsINHand();
                    break;
                case "ToDayIssChq":
                    this.ShowToDayIssChq();
                    break;
                case "CliModNotYet":
                    this.ShowClientMod();
                    break;
                case "CollChqSt":

                    break;
                case "CashFlow":
                    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                    this.ShowCashFlow();
                    break;
                case "FundFlow":

                    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                    this.ShowFundFlow();
                    break;
                case "PostChqInHand":
                    this.ShowPostDatedCheque();
                    break;
                case "IsuVsClr":
                    this.ShowIssVsClr();
                    break;


                case "CashFlow02":
                case "CashFlow03":
                    ((Label)this.Master.FindControl("lblprintstk")).Text = "";

                    this.ShowCashFlow02();
                    break;


                case "FinNote":
                    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                    this.ShowFinNote();
                    break;
            }
        }


        private void PrintDepositBank()
        {

            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string chequeno = "%" + this.txtSrchChequeno.Text.Trim() + "%";
            string Bankcode = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlBankName.SelectedValue.ToString()) + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "RPTDEPOSITINFO", frmdate, todate, chequeno, Bankcode, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvChqdep.DataSource = null;
                this.gvChqdep.DataBind();
                return;
            }
            this.gvChqdep.Columns[1].Visible = (this.ddlBankName.SelectedValue == "000000000000") ? true : false;
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();


        }
        private void ShowCorCahsINHand()
        {
            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string chequeno = "%" + this.txtSrchChequeno.Text.Trim() + "%";
            string withdep = (this.chkDeposit.Checked) ? "withdep" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "RPTCASHORCHQINHAND", frmdate, todate, chequeno, withdep, "", "", "", "", "");
            if (ds1 == null)
            {
                this.dgvChqHand.DataSource = null;
                this.dgvChqHand.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private void ShowToDayIssChq()
        {
            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string chequeno = "%" + this.txtSrchChequeno.Text.Trim() + "%";
            string bankcode = (this.ddlBankName1.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBankName1.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTTODAYISSUECHQ", frmdate, todate, chequeno, bankcode, "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvToDayIssChq.DataSource = null;
                this.grvToDayIssChq.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private void ShowClientMod()
        {
            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "PRINTMODNOTUPDATE", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvRptCliMod.DataSource = null;
                this.grvRptCliMod.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }

        private void ShowCashFlow()
        {

            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Opendate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            string group = this.ddlRptGroup.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTCASHFLOW", frmdate, todate, Opendate, group, "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvCashFlow.DataSource = null;
                this.grvCashFlow.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            // ViewState["recandpayNote"] = ds1.Tables[1];
            this.Data_Bind();
            ds1.Dispose();
            //  this.RPNote();
            for (int i = 0; i < grvCashFlow.Rows.Count; i++)
            {
                string actcode = ((Label)grvCashFlow.Rows[i].FindControl("lgvactcode")).Text.Trim();
                LinkButton lactDesc = (LinkButton)grvCashFlow.Rows[i].FindControl("lbtnactDesc");
                if (ASTUtility.Right(actcode, 4) != "AAAA")
                    lactDesc.CommandArgument = actcode;

            }



        }

        private void ShowCashFlow02()
        {

            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Opendate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            string Procedure = (this.Request.QueryString["Type"] == "CashFlow02") ? "SP_REPORT_ACCOUNTS_RP" : "SP_REPORT_ACCOUNTS_RP_02";
            DataSet ds1 = MktData.GetTransInfo(comcod, Procedure, "RPTCASHFLOW02", frmdate, todate, Opendate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvCashFlow02.DataSource = null;
                this.grvCashFlow02.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            ViewState["recandpayNote"] = ds1.Tables[1];

            this.Data_Bind();
            ds1.Dispose();
            this.RPNote02();
            //for (int i = 0; i < grvCashFlow.Rows.Count; i++)
            //{
            //    string actcode = ((Label)grvCashFlow.Rows[i].FindControl("lgvactcode")).Text.Trim();
            //    LinkButton lactDesc = (LinkButton)grvCashFlow.Rows[i].FindControl("lbtnactDesc");
            //    if (ASTUtility.Right(actcode, 4) != "AAAA")
            //        lactDesc.CommandArgument = actcode;

            //}



        }

        private void RPNote()
        {
            //this.PanelNote.Visible = true;
            //DataTable dt = (DataTable)ViewState["recandpayNote"];
            //this.gvbankbal.DataSource = dt;
            //this.gvbankbal.DataBind();

            //this.lblopnliabilitiesval.Text = Convert.ToDouble(dt.Rows[0]["opnliaam"]).ToString("#,##0;(#,##0) ;");
            //this.lblclsliabilitiesval.Text = Convert.ToDouble(dt.Rows[0]["clsliaam"]).ToString("#,##0;(#,##0) ;");
            //this.lblnetLiabilitiesval.Text = Convert.ToDouble(dt.Rows[0]["netliaam"]).ToString("#,##0;(#,##0) ;");
        }


        private void RPNote02()
        {
            this.PanelNote02.Visible = true;
            DataTable dt = (DataTable)ViewState["recandpayNote"];
            this.gvbankbal02.DataSource = dt;
            this.gvbankbal02.DataBind();

            //this.lblopnliabilitiesval.Text = Convert.ToDouble(dt.Rows[0]["opnliaam"]).ToString("#,##0;(#,##0) ;");
            //this.lblclsliabilitiesval.Text = Convert.ToDouble(dt.Rows[0]["clsliaam"]).ToString("#,##0;(#,##0) ;");
            //this.lblnetLiabilitiesval.Text = Convert.ToDouble(dt.Rows[0]["netliaam"]).ToString("#,##0;(#,##0) ;");
        }


        private void ShowFundFlow()
        {
            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTCASHFLOW", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvfundFlow.DataSource = null;
                this.grvfundFlow.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();
            for (int i = 0; i < grvfundFlow.Rows.Count; i++)
            {
                string actcode = ((Label)grvfundFlow.Rows[i].FindControl("lgvactcodeff")).Text.Trim();
                LinkButton lactDesc = (LinkButton)grvfundFlow.Rows[i].FindControl("lbtnactDescff");
                if (ASTUtility.Right(actcode, 4) != "AAAA")
                    lactDesc.CommandArgument = actcode;

            }

        }
        private void ShowPostDatedCheque()
        {

            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string chequeno = "%" + this.txtSrchChequeno.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "RPTPOSTCASHORCHQINHAND", date, chequeno, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.dgvChqHandPost.DataSource = null;
                this.dgvChqHandPost.DataBind();
                return;
            }
            Session["tblbdeposit"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();


        }
        private void ShowIssVsClr()
        {
            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "CHQISSUEVSCLEARANCE", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvIsuCleared.DataSource = null;
                this.gvIsuCleared.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }


        private void ShowCollChqSt()
        {


            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "RPTCOLLCHQSTATUS", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvCollChqStatus.DataSource = null;
                this.grvCollChqStatus.DataBind();
                return;
            }
            Session["tblbdeposit"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();



            //Session.Remove("tblbdeposit");
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string chequeno = "%" + this.txtSrchChequeno.Text.Trim() + "%";
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "RPTCOLLCHQSTATUS", frmdate, todate, chequeno, "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.grvCollChqStatus.DataSource = null;
            //    this.grvCollChqStatus.DataBind();
            //    return;
            //}
            //Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();
            //ds1.Dispose();


        }

        private void ShowChequeHistory()
        {

            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "SHOWCHQHISTORY", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvIsuCleared.DataSource = null;
                this.gvIsuCleared.DataBind();
                return;
            }
            Session["tblbdeposit"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();




        }

        private void ShowFinNote()
        {
            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS03", "RPTFINANCIALNOTE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                return;
            }
            Session["tblbdeposit"] = ds1.Tables[0];
            //this.Data_Bind();
            //ds1.Dispose();


        }
        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblbdeposit"];
            switch (type)
            {

                case "ChquedepPrint":
                    this.gvChqdep.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvChqdep.DataSource = dt;
                    this.gvChqdep.DataBind();
                    this.FooterCalculation();
                    break;

                case "ChqInHand":
                    this.dgvChqHand.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.dgvChqHand.DataSource = dt;
                    this.dgvChqHand.DataBind();
                    this.FooterCalculation();
                    break;
                case "ToDayIssChq":
                    this.grvToDayIssChq.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvToDayIssChq.DataSource = dt;
                    this.grvToDayIssChq.DataBind();
                    this.FooterCalculation();
                    break;
                case "CliModNotYet":
                    this.grvRptCliMod.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvRptCliMod.DataSource = dt;
                    this.grvRptCliMod.DataBind();
                    this.FooterCalculation();
                    break;
                case "CollChqSt":
                    this.grvCollChqStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvCollChqStatus.DataSource = dt;
                    this.grvCollChqStatus.DataBind();
                    this.FooterCalculation();
                    break;
                case "PayChqCl":
                    this.grvPayChqCl.DataSource = dt;
                    this.grvPayChqCl.DataBind();
                    this.FooterCalculation();
                    break;
                case "CashFlow":
                    this.grvCashFlow.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvCashFlow.Columns[3].HeaderText = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    this.grvCashFlow.Columns[4].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.grvCashFlow.DataSource = dt;
                    this.grvCashFlow.DataBind();
                    //if (dt.Rows.Count > 0)
                    //    ((HyperLink)this.grvCashFlow.FooterRow.FindControl("lgvFCashflow")).NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;
                    // this.FooterCalculation();
                    break;
                case "PostChqInHand":
                    this.dgvChqHandPost.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.dgvChqHandPost.DataSource = (DataTable)Session["tblbdeposit"];
                    this.dgvChqHandPost.DataBind();
                    break;
                case "IsuVsClr":
                    this.gvIsuCleared.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvIsuCleared.DataSource = (DataTable)Session["tblbdeposit"];
                    this.gvIsuCleared.DataBind();
                    this.FooterCalculation();
                    break;

                case "FundFlow":
                    this.grvfundFlow.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvfundFlow.DataSource = dt;
                    this.grvfundFlow.DataBind();
                    if (dt.Rows.Count > 0)
                        ((HyperLink)this.grvfundFlow.FooterRow.FindControl("lgvFCashflowff")).NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;
                    this.FooterCalculation();
                    break;


                case "CashFlow02":
                case "CashFlow03":

                    this.grvCashFlow02.Columns[3].HeaderText = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "<br />" + " To " + "<br / >" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    this.grvCashFlow02.Columns[4].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + " To " + "<br / >" + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.grvCashFlow02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvCashFlow02.DataSource = dt;
                    this.grvCashFlow02.DataBind();

                    // this.FooterCalculation();
                    break;

            }

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string grp = "";
            switch (type)
            {



                case "ChqInHand":
                    grp = dt1.Rows[0]["grp"].ToString();
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

                case "ChquedepPrint":
                    string bankcode = dt1.Rows[0]["bankcode"].ToString();
                    grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["bankcode"].ToString() == bankcode && dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            dt1.Rows[j]["depositbank"] = "";
                            dt1.Rows[j]["grpdesc"] = "";
                        }


                        else
                        {
                            if (dt1.Rows[j]["bankcode"].ToString() == bankcode)
                                dt1.Rows[j]["depositbank"] = "";
                            if (dt1.Rows[j]["grp"].ToString() == grp)
                                dt1.Rows[j]["grpdesc"] = "";
                        }

                        bankcode = dt1.Rows[j]["bankcode"].ToString();
                        grp = dt1.Rows[j]["grp"].ToString();

                    }

                    break;




                case "ToDayIssChq":
                    grp = dt1.Rows[0]["grp"].ToString();
                    string pactcode1 = dt1.Rows[0]["actcode"].ToString();
                    string cactcode = dt1.Rows[0]["cactcode"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        else
                        {
                            grp = dt1.Rows[j]["grp"].ToString();

                        }

                    }



                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if ((dt1.Rows[j]["actcode"].ToString() == pactcode1) && (dt1.Rows[j]["cactcode"].ToString() == cactcode))
                        {
                            pactcode1 = dt1.Rows[j]["actcode"].ToString();
                            cactcode = dt1.Rows[j]["cactcode"].ToString();
                            dt1.Rows[j]["actdesc"] = "";
                            dt1.Rows[j]["cactdesc"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["actcode"].ToString() == pactcode1)
                                dt1.Rows[j]["actdesc"] = "";
                            if (dt1.Rows[j]["cactcode"].ToString() == cactcode)
                                dt1.Rows[j]["cactdesc"] = "";
                            pactcode1 = dt1.Rows[j]["actcode"].ToString();
                            cactcode = dt1.Rows[j]["cactcode"].ToString();
                        }

                    }
                    break;
                case "CliModNotYet":

                    break;
                case "CollChqSt":

                    break;
                case "CashFlow":
                case "FundFlow":

                    grp = dt1.Rows[0]["grp"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";
                        grp = dt1.Rows[j]["grp"].ToString();




                    }



                    break;
                case "IsuVsClr":
                    string voudat1 = dt1.Rows[0]["voudat1"].ToString();
                    string cactcode1 = dt1.Rows[0]["cactcode1"].ToString();
                    string cactcode2 = dt1.Rows[0]["cactcode2"].ToString();
                    string actcode1 = dt1.Rows[0]["actcode1"].ToString();
                    string actcode2 = dt1.Rows[0]["actcode2"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["voudat1"].ToString() == voudat1)
                        {
                            voudat1 = dt1.Rows[j]["voudat1"].ToString();
                            dt1.Rows[j]["voudat"] = "";
                        }
                        if (dt1.Rows[j]["cactcode1"].ToString() == cactcode1)
                        {
                            cactcode1 = dt1.Rows[j]["cactcode1"].ToString();
                            dt1.Rows[j]["cactdesc1"] = "";
                        }
                        if (dt1.Rows[j]["cactcode2"].ToString() == cactcode2)
                        {
                            cactcode2 = dt1.Rows[j]["cactcode2"].ToString();
                            dt1.Rows[j]["cactdesc2"] = "";
                        }
                        if (dt1.Rows[j]["actcode1"].ToString() == actcode1)
                        {
                            actcode1 = dt1.Rows[j]["actcode1"].ToString();
                            dt1.Rows[j]["actdesc1"] = "";
                        }
                        if (dt1.Rows[j]["actcode2"].ToString() == actcode2)
                        {
                            actcode2 = dt1.Rows[j]["actcode2"].ToString();
                            dt1.Rows[j]["actdesc2"] = "";
                        }

                        else
                            voudat1 = dt1.Rows[j]["voudat1"].ToString();
                        cactcode1 = dt1.Rows[j]["cactcode1"].ToString();
                        cactcode2 = dt1.Rows[j]["cactcode2"].ToString();
                        actcode1 = dt1.Rows[j]["actcode1"].ToString();
                        actcode2 = dt1.Rows[j]["actcode2"].ToString();
                    }
                    break;



                case "CashFlow02":
                case "CashFlow03":


                    grp = dt1.Rows[0]["grp"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";
                        grp = dt1.Rows[j]["grp"].ToString();


                    }



                    break;
            }

            return dt1;

        }
        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["tblbdeposit"];
            DataView dv; DataTable dt3;
            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ChquedepPrint":
                    //((Label)this.gvChqdep.FooterRow.FindControl("lblFToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(paidamt)", "")) ?
                    //            0 : dt1.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;
                case "ChqInHand":

                    break;
                case "ToDayIssChq":
                    dt3 = dt1.Copy();
                    dv = dt3.DefaultView;
                    dv.RowFilter = ("typesum ='CCAA'");
                    DataTable dtpay = dv.ToTable();
                    dv.RowFilter = ("typesum ='DDAA'");
                    DataTable dtcon = dv.ToTable();


                    double payamt = Convert.ToDouble((Convert.IsDBNull(dtpay.Compute("sum(cramt)", "")) ? 0 : dtpay.Compute("sum(cramt)", "")));
                    double conamt = Convert.ToDouble((Convert.IsDBNull(dtcon.Compute("sum(cramt)", "")) ? 0 : dtcon.Compute("sum(cramt)", "")));
                    ((Label)this.grvToDayIssChq.FooterRow.FindControl("lgvCrAmt")).Text = (payamt - conamt).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvToDayIssChq.FooterRow.FindControl("lgvFReconAmt")).Text = (Convert.ToDouble((Convert.IsDBNull(dtpay.Compute("sum(reconamt)", "")) ? 0 : dtpay.Compute("sum(reconamt)", ""))) - Convert.ToDouble((Convert.IsDBNull(dtcon.Compute("sum(reconamt)", "")) ? 0 : dtcon.Compute("sum(reconamt)", "")))).ToString("#,##0;(#,##0); ");





                    break;
                case "CliModNotYet":
                    ((Label)this.grvRptCliMod.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
                                0 : dt1.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "CollChqSt":
                    ((Label)this.grvCollChqStatus.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(paidamt)", "")) ?
                                0 : dt1.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "PayChqCl":
                    ((Label)this.grvPayChqCl.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(trnam)", "")) ?
                                0 : dt1.Compute("sum(trnam)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "CashFlow":
                    dt3 = dt1.Copy();
                    dv = dt3.DefaultView;
                    dv.RowFilter = ("actcode ='FFFFAAAAAAAA'");
                    dt1 = dv.ToTable();
                    ((HyperLink)this.grvCashFlow.FooterRow.FindControl("lgvFCashflow")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(cfamt)", "")) ?
                               0 : dt1.Compute("sum(cfamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.grvCashFlow.FooterRow.FindControl("lgvFfundflow")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ffamt)", "")) ?
                    //           0 : dt1.Compute("sum(ffamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "PostChqInHand":
                    break;
                case "IsuVsClr":
                    DataTable dt4 = dt1.Copy();
                    DataView dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("actcode1  not like 'AAAAAAAAAAAA' and actcode1  not like 'BBBBAAAAAAAA'");
                    DataTable dtisu = dv1.ToTable();
                    ((Label)this.gvIsuCleared.FooterRow.FindControl("lgvFIsuAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dtisu.Compute("sum(isuamt)", "")) ?
                               0 : dtisu.Compute("sum(isuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvIsuCleared.FooterRow.FindControl("lgvFCuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dtisu.Compute("sum(recamt)", "")) ?
                               0 : dtisu.Compute("sum(recamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvIsuCleared.FooterRow.FindControl("lgvFPramt")).Text = Convert.ToDouble((Convert.IsDBNull(dtisu.Compute("sum(preamt)", "")) ?
                               0 : dtisu.Compute("sum(preamt)", ""))).ToString("#,##0;(#,##0); ");

                    double cuamt = Convert.ToDouble((Convert.IsDBNull(dtisu.Compute("sum(recamt)", "")) ?
                                0 : dtisu.Compute("sum(recamt)", "")));
                    double pramt = Convert.ToDouble((Convert.IsDBNull(dtisu.Compute("sum(preamt)", "")) ?
                                    0 : dtisu.Compute("sum(preamt)", "")));
                    ((Label)this.gvIsuCleared.FooterRow.FindControl("lgvFPramt1")).Text = (cuamt + pramt).ToString("#,##0;(#,##0); ");
                    break;


                case "FundFlow":
                    dt3 = dt1.Copy();
                    dv = dt3.DefaultView;
                    dv.RowFilter = ("actcode ='FFFFAAAAAAAA'");
                    dt1 = dv.ToTable();
                    ((HyperLink)this.grvfundFlow.FooterRow.FindControl("lgvFCashflowff")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(cfamt)", "")) ?
                               0 : dt1.Compute("sum(cfamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvfundFlow.FooterRow.FindControl("lgvFfundflow")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ffamt)", "")) ?
                               0 : dt1.Compute("sum(ffamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;
            }



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvChqdep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvChqdep.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void dgvChqHand_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvChqHand.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void imgSearchCheque_Click(object sender, EventArgs e)
        {
            this.lbtnOk_Click(null, null);
        }
        protected void grvToDayIssChq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvToDayIssChq.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvToDayIssChq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("lgactdesc");
                Label amt = (Label)e.Row.FindControl("lgvcramt");
                Label Reconamt = (Label)e.Row.FindControl("lgvreconamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 4) != "AAAA")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    Reconamt.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");

                }


            }
        }
        protected void grvRptCliMod_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvRptCliMod.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvCollChqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvCollChqStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvChqdep_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            this.gvChqdep.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void ibtnFindChequenocl_Click(object sender, EventArgs e)
        {
            this.GetCollCheque();
        }
        protected void lbtnSelectChequeNocl_Click(object sender, EventArgs e)
        {
            this.AddChequeNoCL();
        }
        protected void chkorcheqnoasccl_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv = new DataView();
            if (this.chkorcheqnoasccl.Checked)
            {

                dv = dt.DefaultView;
                dv.Sort = ("slno asc");

            }
            else
            {
                dv = dt.DefaultView;
                dv.Sort = ("slno desc");


            }
            Session["tblbdeposit"] = dv.ToTable();
            this.Data_Bind();
        }


        private void GetCollCheque()
        {
            Session.Remove("tblisucheque");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtScheqno = "%" + this.txtIssSchcl.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "GETCOLLCHEQNO", txtScheqno, "", "", "", "", "", "", "", "");
            this.ddlChequeNocl.DataTextField = "textfield";
            this.ddlChequeNocl.DataValueField = "valuefield";
            this.ddlChequeNocl.DataSource = ds1.Tables[0];
            this.ddlChequeNocl.DataBind();
            Session["tblisucheque"] = ds1.Tables[0];
            ds1.Dispose();
            if (this.ddlChequeNocl.Items.Count > 0)
                this.AddChequeNoCL();
            this.txtIssSchcl.Focus();

        }

        private void AddChequeNoCL()
        {
            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.Sort = ("slno asc");
            dt = dv.ToTable();
            string mrno = this.ddlChequeNocl.SelectedValue.Trim().Substring(0, 9);
            string chqno = this.ddlChequeNocl.SelectedValue.Trim().Substring(9);


            int tocount = dt.Rows.Count;
            int slno = (tocount == 0) ? 1 : Convert.ToInt32(dt.Rows[tocount - 1]["slno"]) + 1;

            DataRow[] dr = dt.Select("mrno='" + mrno + "' and chqno='" + chqno + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["slno"] = slno;
                dr1["mrno"] = mrno;
                dr1["chqno"] = chqno;

                dr1["mrdate"] = ((DataTable)Session["tblisucheque"]).Select("mrno='" + mrno + "' and chqno='" + chqno + "'")[0]["mrdate"].ToString();
                dr1["paydate"] = ((DataTable)Session["tblisucheque"]).Select("mrno='" + mrno + "' and chqno='" + chqno + "'")[0]["paydate"].ToString();
                dr1["pactdesc"] = ((DataTable)Session["tblisucheque"]).Select("mrno='" + mrno + "' and chqno='" + chqno + "'")[0]["pactdesc"].ToString(); ;
                dr1["udesc"] = ((DataTable)Session["tblisucheque"]).Select("mrno='" + mrno + "' and chqno='" + chqno + "'")[0]["udesc"].ToString();
                dr1["custname"] = ((DataTable)Session["tblisucheque"]).Select("mrno='" + mrno + "' and chqno='" + chqno + "'")[0]["custname"].ToString();
                dr1["banname"] = ((DataTable)Session["tblisucheque"]).Select("mrno='" + mrno + "' and chqno='" + chqno + "'")[0]["banname"].ToString();
                dr1["depositdat"] = ((DataTable)Session["tblisucheque"]).Select("mrno='" + mrno + "' and chqno='" + chqno + "'")[0]["depositdat"].ToString();
                dr1["dhonrdate"] = ((DataTable)Session["tblisucheque"]).Select("mrno='" + mrno + "' and chqno='" + chqno + "'")[0]["dhonrdate"].ToString();
                dr1["recndt"] = ((DataTable)Session["tblisucheque"]).Select("mrno='" + mrno + "' and chqno='" + chqno + "'")[0]["recndt"].ToString();
                dr1["paidamt"] = Convert.ToDouble(((DataTable)Session["tblisucheque"]).Select("mrno='" + mrno + "' and chqno='" + chqno + "'")[0]["paidamt"]).ToString();
                dt.Rows.Add(dr1);
            }

            // Session["tblbdeposit"] = dt;        
            dv = dt.DefaultView;
            dv.Sort = ("slno desc");
            Session["tblbdeposit"] = dv.ToTable();
            this.Data_Bind();


        }

        protected void lbtnUpdatecl_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            string comcod = this.GetCompCode();
            DataTable tbl1 = (DataTable)Session["tblbdeposit"];


            foreach (DataRow dr in tbl1.Rows)
            {
                string mrno = dr["mrno"].ToString();
                string chqno = dr["chqno"].ToString();
                string mrdate = Convert.ToDateTime(dr["mrdate"]).ToString("dd-MMM-yyyy");
                string paydate = Convert.ToDateTime(dr["paydate"]).ToString("dd-MMM-yyyy");
                string pactdesc = dr["pactdesc"].ToString();
                string udesc = dr["udesc"].ToString();
                string custname = dr["custname"].ToString();
                string banname = dr["banname"].ToString();
                string depositdate = dr["depositdat"].ToString();
                string dhonrdate = dr["dhonrdate"].ToString();
                string Reconcilation = dr["recndt"].ToString();
                string Amount = Convert.ToDouble(dr["paidamt"]).ToString();


                bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "INSORUPCOLLHISTORY",
                             mrno, chqno, mrdate, paydate, pactdesc, udesc, custname, banname,
                             depositdate, dhonrdate, Reconcilation, Amount, "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                    return;
                }

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);
        }
        protected void lbtnDeleteAllcl_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            string comcod = this.GetCompCode();
            bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "DELETECOLLHISTORY",
                             "", "", "", "", "", "", "", "",
                             "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Fail');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Deleted successfully');", true);
            this.ShowCollChqSt();
        }
        protected void ibtnFindChequeno_Click(object sender, EventArgs e)
        {
            this.GetPayIsuChq();
        }
        private void GetPayIsuChq()
        {
            Session.Remove("tblisucheque");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtScheqno = "%" + this.txtIssSch.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "RPTPOSTDATESTRANS", txtScheqno, "", "", "", "", "", "", "", "");
            this.ddlChequeNo.DataTextField = "textfield";
            this.ddlChequeNo.DataValueField = "valuefield";
            this.ddlChequeNo.DataSource = ds1.Tables[0];
            this.ddlChequeNo.DataBind();
            Session["tblisucheque"] = ds1.Tables[0];
            ds1.Dispose();
            if (this.ddlChequeNo.Items.Count > 0)
                this.AddChequeNo();
            this.txtIssSch.Focus();




        }
        private void AddChequeNo()
        {
            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.Sort = ("slno asc");
            dt = dv.ToTable();
            string vounum = this.ddlChequeNo.SelectedValue.Trim().Substring(0, 14);
            string isunum = this.ddlChequeNo.SelectedValue.Trim().Substring(14, 9);
            string chequeno = this.ddlChequeNo.SelectedValue.Trim().Substring(23);


            int tocount = dt.Rows.Count;
            int slno = (tocount == 0) ? 1 : Convert.ToInt32(dt.Rows[tocount - 1]["slno"]) + 1;

            DataRow[] dr = dt.Select("vounum='" + vounum + "' and isunum='" + isunum + "' and chequeno='" + chequeno + "'");
            if (dr.Length == 0)
            {

                DataTable dt2 = ((DataTable)Session["tblisucheque"]).Copy();
                DataView dv2 = dt2.DefaultView;
                dv2.RowFilter = ("vounum='" + vounum + "' and isunum='" + isunum + "' and chequeno='" + chequeno + "'");
                dt2 = dv2.ToTable();

                foreach (DataRow dr2 in dt2.Rows)
                {

                    DataRow dr1 = dt.NewRow();
                    dr1["slno"] = slno;
                    dr1["vounum"] = vounum;
                    dr1["voudat"] = dr2["voudat"].ToString();
                    dr1["isunum"] = dr2["isunum"].ToString(); ;
                    dr1["payto"] = dr2["payto"].ToString();
                    dr1["actdesc"] = dr2["actdesc"].ToString();
                    dr1["resdesc"] = dr2["resdesc"].ToString();
                    dr1["varnar"] = dr2["varnar"].ToString();
                    dr1["cactdesc"] = dr2["cactdesc"].ToString();
                    dr1["chequeno"] = chequeno;
                    dr1["chequedat"] = dr2["chequedat"].ToString();
                    dr1["recndt"] = dr2["recndt"].ToString();
                    dr1["trnam"] = dr2["trnam"].ToString();
                    dt.Rows.Add(dr1);
                }
            }

            // Session["tblbdeposit"] = dt;        
            dv = dt.DefaultView;
            dv.Sort = ("slno desc");
            Session["tblbdeposit"] = dv.ToTable();
            this.Data_Bind();


        }




        protected void lbtnSelectChequeNo_Click(object sender, EventArgs e)
        {
            this.AddChequeNo();

        }
        protected void chkorcheqnoasc_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv = new DataView();
            if (this.chkorcheqnoasc.Checked)
            {

                dv = dt.DefaultView;
                dv.Sort = ("slno asc");

            }
            else
            {
                dv = dt.DefaultView;
                dv.Sort = ("slno desc");


            }
            Session["tblbdeposit"] = dv.ToTable();
            this.Data_Bind();

        }

        protected void gvChqdep_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label udesc = (Label)e.Row.FindControl("lgvudescdep");
                Label cashamt = (Label)e.Row.FindControl("lgvcashamtdep");
                Label chqamt = (Label)e.Row.FindControl("lgvchqamtdep");

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

        protected void dgvChqHand_RowDataBound(object sender, GridViewRowEventArgs e)
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



        protected void grvCashFlow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton description = (LinkButton)e.Row.FindControl("lbtnactDesc");
                Label closam = (Label)e.Row.FindControl("lblgvclosamcf");
                Label opnam = (Label)e.Row.FindControl("lblgvopnamcf");
                HyperLink cuamt = (HyperLink)e.Row.FindControl("hlnkgvcuamtcf");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    description.Font.Bold = true;
                    closam.Font.Bold = true;

                    opnam.Font.Bold = true;
                    cuamt.Font.Bold = true;
                    //  description.Style.Add("text-align", "right");


                }





                if (code == "FFFFAAAAAAAA")
                {

                    cuamt.Style.Add("color", "blue");
                    cuamt.NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;

                }
                else
                {

                    cuamt.Style.Add("color", "black");


                }



            }
        }
        protected void dgvChqHandPost_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvChqHandPost.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void dgvChqHandPost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label udesc = (Label)e.Row.FindControl("lgvudescp");
                Label cashamt = (Label)e.Row.FindControl("lgvcashamtp");
                Label chqamt = (Label)e.Row.FindControl("lgvchqamtp");

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
        protected void gvIsuCleared_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvIsuCleared.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvIsuCleared_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label date = (Label)e.Row.FindControl("lgvDate");
                Label chq1 = (Label)e.Row.FindControl("lgcChq1");
                Label isuamt = (Label)e.Row.FindControl("lgvIsuAmt");
                Label chq2 = (Label)e.Row.FindControl("lgcChq2");
                Label cuamt = (Label)e.Row.FindControl("lgvCuamt");
                Label pramt = (Label)e.Row.FindControl("lgvPramt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    //date.Font.Bold = true;
                    chq1.Font.Bold = true;
                    isuamt.Font.Bold = true;
                    chq2.Font.Bold = true;
                    cuamt.Font.Bold = true;
                    pramt.Font.Bold = true;
                    chq1.Style.Add("text-align", "right");
                    chq2.Style.Add("text-align", "right");


                }

            }
        }




        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            string comcod = this.GetCompCode();
            DataTable tbl1 = (DataTable)Session["tblbdeposit"];

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string Vounum = tbl1.Rows[i]["vounum"].ToString();
                string Voudat = Convert.ToDateTime(tbl1.Rows[i]["voudat"]).ToString("dd-MMM-yyyy");
                string IssueNo = tbl1.Rows[i]["isunum"].ToString();
                string Payto = tbl1.Rows[i]["payto"].ToString();
                string Actdesc = tbl1.Rows[i]["actdesc"].ToString();
                string Resdesc = tbl1.Rows[i]["resdesc"].ToString();
                string Narration = tbl1.Rows[i]["varnar"].ToString();
                string ConDesc = tbl1.Rows[i]["cactdesc"].ToString();
                string ChequeNo = tbl1.Rows[i]["chequeno"].ToString();
                string ChequeDate = Convert.ToDateTime(tbl1.Rows[i]["chequedat"]).ToString("dd-MMM-yyyy");
                string Reconcilation = tbl1.Rows[i]["recndt"].ToString();
                string Amount = Convert.ToDouble(tbl1.Rows[i]["trnam"]).ToString();


                bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "INSORUPCHQHISTORY",
                             Vounum, IssueNo, Voudat, Payto, Actdesc, Resdesc, Narration, ConDesc,
                             ChequeNo, ChequeDate, Reconcilation, Amount, "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                    return;
                }

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Updated successfully');", true);
        }
        protected void lbtnDeleteAll_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            string comcod = this.GetCompCode();
            bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "DELETECHQHISTORY",
                             "", "", "", "", "", "", "", "",
                             "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Fail');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Deleted successfully');", true);
            this.ShowChequeHistory();

        }
        protected void grvPayChqCl_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblbdeposit"];

            string vounum = ((Label)this.grvPayChqCl.Rows[e.RowIndex].FindControl("lblgvvounum")).Text.Trim();
            string Issueno = ((Label)this.grvPayChqCl.Rows[e.RowIndex].FindControl("lblgvIssuNo")).Text.Trim();
            bool result = MktData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "DELCHQHISISSUNUM", vounum, Issueno, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvPayChqCl.PageSize) * (this.grvPayChqCl.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblbdeposit");
            Session["tblbdeposit"] = dv.ToTable();
            this.Data_Bind();




        }
        protected void lbtnactDesc_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string actcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "actcode like('" + actcode + "')";
            dt = dv1.ToTable();
            if (dt.Rows.Count == 0)
                return;

            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtfromdate.Text;
            string mTRNDAT2 = this.txttodate.Text;
            string mACTCODE = dt.Rows[0]["actcode"].ToString();
            string mACTDESC = dt.Rows[0]["actdesc"].ToString();
            string lebel2 = dt.Rows[0]["rleb2"].ToString();
            if (mACTCODE == "")
            {
                return;
            }

            ///---------------------------------//// 
            if (ASTUtility.Left(mACTCODE, 1) == "4")

            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=PrjReportRP&actcode=" +
                                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
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
        protected void grvfundFlow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton description = (LinkButton)e.Row.FindControl("lbtnactDescff");
                Label cfamt = (Label)e.Row.FindControl("lgvcfamtff");
                Label ffamt = (Label)e.Row.FindControl("lgvffamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    description.Font.Bold = true;
                    cfamt.Font.Bold = true;
                    ffamt.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }
                else if (ASTUtility.Right(code, 4) == "0000")
                {

                    description.Font.Bold = true;
                    cfamt.Font.Bold = true;
                    ffamt.Font.Bold = true;
                    cfamt.Style.Add("text-align", "left");
                    ffamt.Style.Add("text-align", "left");


                }


            }
        }
        protected void lbtnactDescff_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string actcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "actcode like('" + actcode + "')";
            dt = dv1.ToTable();
            if (dt.Rows.Count == 0)
                return;

            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtfromdate.Text;
            string mTRNDAT2 = this.txttodate.Text;
            string mACTCODE = dt.Rows[0]["actcode"].ToString();
            string mACTDESC = dt.Rows[0]["actdesc"].ToString();
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
        protected void imgBtn_Click(object sender, EventArgs e)
        {
            this.GetBankName1();
        }
        protected void gvbankbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label ActDesc = (Label)e.Row.FindControl("lgcActDescbb");
                Label opnam = (Label)e.Row.FindControl("lgvopnambb");
                Label closam = (Label)e.Row.FindControl("lgvclosambb");
                Label balam = (Label)e.Row.FindControl("lgbalambb");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "code")).ToString();


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




            }
        }
        protected void grvCashFlow02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label Desc = (Label)e.Row.FindControl("lblgvDesccf02");
                Label closam = (Label)e.Row.FindControl("lblgvclosamcf02");
                Label opnam = (Label)e.Row.FindControl("lblgvopnamcf02");
                HyperLink cuamt = (HyperLink)e.Row.FindControl("hlnkgvcuamtcf02");
                // Label ffamt = (Label)e.Row.FindControl("lgvffamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    Desc.Font.Bold = true;
                    closam.Font.Bold = true;
                    opnam.Font.Bold = true;
                    cuamt.Font.Bold = true;

                    // ffamt.Font.Bold = true;
                    Desc.Style.Add("text-align", "right");


                }
                if (code == "28BBBBAAAAAA")
                {

                    cuamt.Style.Add("color", "blue");
                    cuamt.NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;

                }




            }
        }
        protected void gvbankbal02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label ActDesc = (Label)e.Row.FindControl("lgcActDescbb02");
                Label opnam = (Label)e.Row.FindControl("lgvopnambb02");
                Label closam = (Label)e.Row.FindControl("lgvclosambb02");
                Label balam = (Label)e.Row.FindControl("lgbalambb02");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "code")).ToString();


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




            }

        }







        protected void grvPayChqCl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void grvCollChqStatus_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblbdeposit"];

            string mrno = ((Label)this.grvCollChqStatus.Rows[e.RowIndex].FindControl("lblgvMrNo")).Text.Trim();
            string Chequno = ((Label)this.grvCollChqStatus.Rows[e.RowIndex].FindControl("lblgvCNo")).Text.Trim();
            bool result = MktData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "DELETEINDCOLLHISTORY", mrno, Chequno, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.grvCollChqStatus.PageSize) * (this.grvCollChqStatus.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblbdeposit");
            Session["tblbdeposit"] = dv.ToTable();
            this.Data_Bind();

        }
    }
}