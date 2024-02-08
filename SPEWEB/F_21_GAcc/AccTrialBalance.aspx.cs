using System;
using System.Collections;
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
using CrystalDecisions.CrystalReports.Engine;
using SPELIB;

namespace SPEWEB.F_21_GAcc
{
    public partial class AccTrialBalance : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Mains") ? "Accounts Trial Balance"
                : (this.Request.QueryString["Type"].ToString().Trim() == "Details") ? "Notes to the Financial  Statement"
                : (this.Request.QueryString["Type"].ToString().Trim() == "TBDetails") ? "Details of Trial Balance"
                 : (this.Request.QueryString["Type"].ToString().Trim() == "INDetails") ? "Notes to the Financial  Statement"

                : (this.Request.QueryString["Type"].ToString().Trim() == "HOTB") ? "Head Office Trial Balance"
                : (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "Accounts Trial Balance (Consolidated)"
                : "Bank Position Report";
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.ViewSection();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void ViewSection()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            double day;
            switch (Type)
            {
                case "Mains":
                    day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                    this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                    this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "Details":
                case "TBDetails":
                case "INDetails":
                    string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtDatefromd.Text = Convert.ToDateTime("01-Jan-" + curdate.Substring(7)).ToString("dd-MMM-yyyy");
                    this.txtDatetod.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "BankPosition":
                    day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                    this.txtDatefrombank.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                    this.txtDatetobank.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                //  case "HOTB":
                //day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                //this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                //this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.lblreportlevel.Visible = false;
                //this.ddlReportLevel.Visible = false;
                //this.MultiView1.ActiveViewIndex = 0;
                //    break;
                case "HOTB":
                case "TBConsolidated":
                    this.txtAsDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 3;

                    break;

                case "BankPosition02":
                    this.txtAsDateb.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 4;
                    break;


                case "BalConfirmation":

                    day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                    this.txtDatefrombankcb.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                    this.txtDatetobankcb.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).ToString();
                string eventdesc = "Show Report: " + Type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private DataSet GetDataForReport()
        {


            string Type = this.Request.QueryString["Type"].ToString().Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = new DataSet();
            string date1, date2;

            switch (Type)
            {
                case "Mains":
                    date1 = this.txtDatefrom.Text.Substring(0, 11).ToString();
                    date2 = this.txtDateto.Text.Substring(0, 11).ToString();
                    string level = this.ddlReportLevel.SelectedValue.ToString();
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "TB_COMPANY_0" + level, date1, date2, "", "", "", "", "", "", "");
                    break;

                case "Details":
                case "TBDetails":
                case "INDetails":

                    date1 = this.txtDatefromd.Text.Substring(0, 11).ToString();
                    date2 = this.txtDatetod.Text.Substring(0, 11).ToString();
                    string levelmain = this.ddlacclevel.SelectedValue.ToString();
                    string leveldetails = this.ddlReportLevelDetails.SelectedValue.ToString();
                    string status = this.Request.QueryString["Type"];
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTDETAILSTB", date1, date2, levelmain, leveldetails, status, "", "", "", "");
                    break;

                case "BankPosition":
                    date1 = this.txtDatefrombank.Text.Substring(0, 11).ToString();
                    date2 = this.txtDatetobank.Text.Substring(0, 11).ToString();
                    string levelbank = this.ddlReportLevelBank.SelectedValue.ToString();
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTBANKPOSITION", date1, date2, levelbank, "", "", "", "", "", "");
                    break;
                // case "HOTB":
                //date1 = this.txtDatefrom.Text.Substring(0, 11).ToString();
                //date2 = this.txtDateto.Text.Substring(0, 11).ToString();
                //ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "TBHEADOFFICE", date1, date2, "", "", "", "", "", "", "");
                // break;

                case "HOTB":
                case "TBConsolidated":
                    string date = this.txtAsDate.Text.Substring(0, 11).ToString();
                    string Level = this.ddlReportLevelcon.SelectedValue.ToString();
                    string hotb = (this.Request.QueryString["Type"].ToString().Trim() == "HOTB") ? "HOTB" : "";
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTCONTRIALBALANCE", date, Level, hotb, "", "", "", "", "", "");
                    break;


                case "BankPosition02":
                    date = this.txtAsDateb.Text.Substring(0, 11).ToString();
                    string levelbank02 = this.ddlReportLevelbk02.SelectedValue.ToString();
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTBANKPOSITION02", date, levelbank02, "", "", "", "", "", "", "");
                    break;


                case "BalConfirmation":
                    date1 = this.txtDatefrombankcb.Text.Substring(0, 11).ToString();
                    date2 = this.txtDatetobankcb.Text.Substring(0, 11).ToString();
                    string levelcandbank = this.ddlReportLevelBankcb.SelectedValue.ToString();
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTCASHANDBANKBAL", date1, date2, levelcandbank, "", "", "", "", "", "");
                    break;




            }

            return ds1;
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.dgv1.DataSource = null;
                this.dgv1.DataBind();
                return;

            }

            this.dgv1.DataSource = ds1.Tables[0];
            this.dgv1.DataBind();
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            if (Type == "HOTB")
            {
                this.dgv1.Columns[11].Visible = false;
            }

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.dgv1.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            ((Label)this.dgv1.FooterRow.FindControl("lblfopndramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");

            ((Label)this.dgv1.FooterRow.FindControl("lblfopncramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");
            //this.dgv1.Columns[2].FooterText = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0.00;(#,##0.00); ") + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfclodramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfclocramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfnetamt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");

            Session["Report1"] = dgv1;
            ((HyperLink)this.dgv1.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "Mains":

                    this.PrintMainTrialBal();
                    break;
                case "Details":
                case "TBDetails":
                case "INDetails":
                    this.PrintDetailsTrialBal();
                    break;
                case "BankPosition":
                    this.RptPrintBankPosition();
                    break;

                case "HOTB":
                case "TBConsolidated":
                    this.PrintConTrialBal();
                    break;

                case "BankPosition02":
                    PrintBankPosition02();
                    break;
                case "BalConfirmation":
                    PrintBalConfirmation();
                    break;





            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).ToString();
                string eventdesc = "Print Report: " + Type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        private void PrintMainTrialBal()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            ReportDocument rptstk = new ReportDocument();

            if (comcod == "9201")
            {
                //rptstk = new RMGiRPT.R_41_GAcc.RptAccTrialBalance1();
            }
            else
            {
                rptstk = new RMGiRPT.R_21_GAcc.RptAccTrialBalance();
            }


            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            //TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtadress.Text = comadd;

            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "TRIAL BALANCE - " + this.ddlReportLevel.SelectedValue.ToString().Trim() : "HEAD OFFICE TRIAL BALANCE";

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy") + ")";

            //TextObject txtopeingname1 = rptstk.ReportDefinition.ReportObjects["opeingname1"] as TextObject;
            //txtopeingname1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtopeingname2 = rptstk.ReportDefinition.ReportObjects["opeingname2"] as TextObject;
            //txtopeingname2.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtdramount = rptstk.ReportDefinition.ReportObjects["dramount"] as TextObject;
            //txtdramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); "); ;

            //TextObject txtcramount = rptstk.ReportDefinition.ReportObjects["cramount"] as TextObject;
            //txtcramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); "); ;

            //TextObject txtclosingamount1 = rptstk.ReportDefinition.ReportObjects["closingamount1"] as TextObject;
            //txtclosingamount1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");

            TextObject txtclosam = rptstk.ReportDefinition.ReportObjects["txtclosam"] as TextObject;
            txtclosam.Text = (comcod == "9201") ? Convert.ToDouble(ds1.Tables[1].Rows[0]["closam"]).ToString("#,##0.00;(#,##0.00); ") : Convert.ToDouble(ds1.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(ds1.Tables[0]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintConTrialBal()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;


            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptAccConTrialBalance();



            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            ////TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtadress.Text = comadd;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (this.Request.QueryString["Type"].ToString().Trim() == "Mains") ? "TRIAL BALANCE - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1)
            //    : (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "TRIAL BALANCE (CONSOLIDATED) - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1) : "HEAD OFFICE TRIAL BALANCE";



            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "As On Date:  " + Convert.ToDateTime(this.txtAsDate.Text.Trim()).ToString("dd-MMM-yyyy");



            //TextObject txtclosdramt = rptstk.ReportDefinition.ReportObjects["txtclosdramt"] as TextObject;
            //txtclosdramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtcloscramt = rptstk.ReportDefinition.ReportObjects["txtcloscramt"] as TextObject;
            //txtcloscramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            //TextObject txtnetdramt = rptstk.ReportDefinition.ReportObjects["txtnetdramt"] as TextObject;
            //txtnetdramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netdram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtnetcramt = rptstk.ReportDefinition.ReportObjects["txtnetcramt"] as TextObject;
            //txtnetcramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netcram"]).ToString("#,##0;(#,##0); ");





            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(ds1.Tables[0]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintDetailsTrialBal()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptAccDetTrialBalance();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            ////TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtadress.Text = comadd;
            //string Type = this.Request.QueryString["Type"].ToString().Trim();
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (Type == "Details") ? "Notes to the Financial  Statement" : (Type == "INDetails") ? "Notes to the Financial  Statement" : "";

            //TextObject txtrptposition = rptstk.ReportDefinition.ReportObjects["txtrptposition"] as TextObject;
            //txtrptposition.Text = (Type == "Details") ? "Financial Position" : (Type == "INDetails") ? "Comprehensive Income" : "Financial Position";

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "As at " + Convert.ToDateTime(this.txtDatetod.Text).ToString("dd MMMM, yyyy");

            //TextObject TxtOpening = rptstk.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            //TxtOpening.Text = Convert.ToDateTime(this.txtDatefromd.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            //TextObject TxtClosing = rptstk.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            //TxtClosing.Text = Convert.ToDateTime(this.txtDatetod.Text).ToString("dd-MMM-yyyy");


            ////TextObject txtopeingname1 = rptstk.ReportDefinition.ReportObjects["opeingname1"] as TextObject;
            ////txtopeingname1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");

            ////TextObject txtopeingname2 = rptstk.ReportDefinition.ReportObjects["opeingname2"] as TextObject;
            ////txtopeingname2.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");

            ////TextObject txtdramount = rptstk.ReportDefinition.ReportObjects["dramount"] as TextObject;
            ////txtdramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); "); ;

            ////TextObject txtcramount = rptstk.ReportDefinition.ReportObjects["cramount"] as TextObject;
            ////txtcramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); "); ;

            ////TextObject txtclosingamount1 = rptstk.ReportDefinition.ReportObjects["closingamount1"] as TextObject;
            ////txtclosingamount1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");

            ////TextObject txtclosingamount2 = rptstk.ReportDefinition.ReportObjects["closingamount2"] as TextObject;
            ////txtclosingamount2.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); "); ;


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(this.HiddenSameData(ds1.Tables[0]));
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void RptPrintBankPosition()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstk = new RMGiRPT.R_21_GAcc.rptBankPosition();
            DataTable dt = (DataTable)Session["tblBankPosition"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDatefrombank.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatetobank.Text).ToString("dd-MMM-yyyy") + " )";
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource((DataTable)Session["tblBankPosition"]);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintBankPosition02()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptAccBankPosition02();
            //DataTable dt = (DataTable)Session["tblBankPosition"];
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "As On Date : " + Convert.ToDateTime(this.txtAsDateb.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["tblBankPosition"]);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintBalConfirmation()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptAccBalConfirmation();
            //DataTable dt = (DataTable)Session["tblBankPosition"];
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDatefrombankcb.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatetobankcb.Text).ToString("dd-MMM-yyyy") + " )";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["tblBankPosition"]);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvcode")).Text;
            string mACTDESC = ((Label)e.Row.FindControl("lblgvAcDesc")).Text;
            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;
            //------------------------------//////
            Label actcode = (Label)e.Row.FindControl("lblgvcode");
            HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();


            if (code == "")
            {
                return;
            }


            if (ASTUtility.Right(code, 4) == "0000")
            {
                actcode.Font.Bold = true;
                actdesc.Font.Bold = true;
                //actdesc.Style.Add("text-align", "right");

            }
            ///---------------------------------//// 

            if (ASTUtility.Left(mACTCODE, 1) == "4" || ASTUtility.Left(mACTCODE, 2) == "17")
            {
                hlink1.NavigateUrl = "AccProjectReports.aspx?actcode=" + mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }
            else if (lebel2 == "")
            {

                if (ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC;
            }
            else
            {
                if (ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            }

        }
        protected void lnkDetailsok_Click(object sender, EventArgs e)
        {
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvDetails.DataSource = null;
                this.gvDetails.DataBind();
                return;
            }

            // this.gvDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDetails.DataSource = HiddenSameData(ds1.Tables[0]);
            this.gvDetails.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvDetails.HeaderRow.FindControl("hlbtnCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["DetTrBal"] = ds1.Tables[0];
            // Session["DetTBal"] = ds1.Tables[1];
            //this.FooterCal();
        }
        private void FooterCal()
        {

            DataTable dt = (DataTable)Session["DetTrBal"];

            DataView dv = dt.Copy().DefaultView;

            dv.RowFilter = ("(rescode like '%AAAA%' or rescode like '%BBBB%') and opnam >'0'");
            DataTable dtopdr = dv.ToTable();
            double opdr = Convert.ToDouble((Convert.IsDBNull(dtopdr.Compute("sum(opnam)", "")) ? 0 : dtopdr.Compute("sum(opnam)", "")));

            dv.RowFilter = ("(rescode like '%AAAA%' or rescode like '%BBBB%') and opnam <'0'");
            DataTable dtopcr = dv.ToTable();
            double opcr = Convert.ToDouble((Convert.IsDBNull(dtopcr.Compute("sum(opnam)", "")) ? 0 : dtopcr.Compute("sum(opnam)", "")));

            DataView dv1 = dt.Copy().DefaultView;

            dv1.RowFilter = ("(rescode like '%AAAA%' or rescode like '%BBBB%') and closam >'0'");
            DataTable dtcldr = dv1.ToTable();
            double cldr = Convert.ToDouble((Convert.IsDBNull(dtcldr.Compute("sum(closam)", "")) ? 0 : dtcldr.Compute("sum(closam)", "")));

            dv1.RowFilter = ("(rescode like '%AAAA%' or rescode like '%BBBB%') and closam <'0'");
            DataTable dtclcr = dv1.ToTable();
            double clcr = Convert.ToDouble((Convert.IsDBNull(dtclcr.Compute("sum(closam)", "")) ? 0 : dtclcr.Compute("sum(closam)", "")));

            ((Label)this.gvDetails.FooterRow.FindControl("lblfopDes")).Text = "Dr. " + "<br>" + "Cr. ";
            ((Label)this.gvDetails.FooterRow.FindControl("lblfopnamtd")).Text = opdr.ToString("#,##0;(#,##0); ") + "<br>" + opcr.ToString("#,##0;(#,##0); ");

            ((Label)this.gvDetails.FooterRow.FindControl("lblfcloamtd")).Text = cldr.ToString("#,##0;(#,##0); ") + "<br>" + clcr.ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvDetails;
            ((HyperLink)this.gvDetails.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
                HyperLink description = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label lblopnamt = (Label)e.Row.FindControl("lblgvopnamtd");
                Label lbldram = (Label)e.Row.FindControl("lblgvDramtd");
                Label lblcramt = (Label)e.Row.FindControl("lblgvCramtd");
                Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobald");
                string mTRNDAT1 = this.txtDatefromd.Text;
                string mTRNDAT2 = this.txtDatetod.Text;

                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString();
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string desc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "00000000AAAA")
                {

                    description.Font.Bold = true;
                    lblopnamt.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    lblcramt.Font.Bold = true;
                    lblgvclobal.Font.Bold = true;

                }
                if (actcode.Length == 12)
                {
                    if (ASTUtility.Left(actcode, 2) == "47")  //F?Type=INDetails&Date1=01-Jan-2018&Date2=24-Mar-2018&opndate=01-Jan-2017
                    {
                        description.Style.Add("color", "blue");
                        hlink1.NavigateUrl = "LinkAccount.aspx?Type=CogsDetails&comcod=" + comcod + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actcode=" + actcode + "&Desc=" + desc;
                    }
                }

            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Mains":

                    break;

                case "Details":
                case "TBDetails":
                case "INDetails":
                    string actcode4 = dt1.Rows[0]["actcode4"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode4"].ToString() == actcode4)
                        {
                            actcode4 = dt1.Rows[j]["actcode4"].ToString();
                            dt1.Rows[j]["actdesc4"] = "";
                            dt1.Rows[j]["actnotes"] = "";
                            dt1.Rows[j]["actcode4"] = "";

                        }

                        else
                        {
                            actcode4 = dt1.Rows[j]["actcode4"].ToString();

                        }

                        if (dt1.Rows[j]["rescode4"].ToString().Substring(0, 4) == "0000")
                            dt1.Rows[j]["rescode4"] = "";
                    }

                    break;

                case "BankPosition":


                    break;

                case "HOTB":
                case "TBConsolidated":
                    break;

                case "BankPosition02":
                    break;


                case "BalConfirmation":
                    string grp = dt1.Rows[0]["grp"].ToString();
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

        protected void lnkbtnBankPosition_Click(object sender, EventArgs e)
        {
            Session.Remove("tblBankPosition");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvBankPosition.DataSource = null;
                this.gvBankPosition.DataBind();
                return;
            }
            Session["tblBankPosition"] = ds1.Tables[0];
            this.gvBankPosition.DataSource = ds1.Tables[0];
            this.gvBankPosition.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvBankPosition.HeaderRow.FindControl("hlbtnbnkpdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            ds1.Dispose();
            Session["Report1"] = gvBankPosition;
            ((HyperLink)this.gvBankPosition.HeaderRow.FindControl("hlbtnbnkpdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }



        //protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //this.GetDataForReport();
        //    this.gvDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
        //    DataTable dt = (DataTable)Session["DetTrBal"];
        //    gvDetails.DataSource = dt;
        //    gvDetails.DataBind();
        //    this.SaveValue();
        //}
        //protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    //this.GetDataForReport();
        //    gvDetails.PageIndex = e.NewPageIndex;
        //    this.gvDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
        //    DataTable dt = (DataTable)Session["DetTrBal"];
        //    gvDetails.DataSource = dt;
        //    gvDetails.DataBind();
        //    this.SaveValue();
        //}
        protected void lbtnCdataExel_Click(object sender, EventArgs e)
        {


        }
        protected void lnkTrialBalCon_Click(object sender, EventArgs e)
        {
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvtbcon.DataSource = null;
                this.gvtbcon.DataBind();
                return;

            }

            this.gvtbcon.DataSource = ds1.Tables[0];
            this.gvtbcon.DataBind();


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvtbcon.HeaderRow.FindControl("hlbtntbCdataExelcon")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            DataTable dt = ds1.Tables[0];

            //((Label)this.gvtbcon.FooterRow.FindControl("lblfClosDramtcon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(closdram)", "")) ?
            //                             0 : dt.Compute("sum(closdram)", ""))).ToString("#,##0;(#,##0); ");

            //((Label)this.gvtbcon.FooterRow.FindControl("lblfClosCramtcon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(closcram)", "")) ?
            //                             0 : dt.Compute("sum(closcram)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvtbcon.FooterRow.FindControl("lblfnetdramtcon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netdram)", "")) ?
            //                             0 : dt.Compute("sum(netdram)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvtbcon.FooterRow.FindControl("lblfnetcramtcon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netcram)", "")) ?
            //                             0 : dt.Compute("sum(netcram)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvtbcon.FooterRow.FindControl("lblfClosDramtcon")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtbcon.FooterRow.FindControl("lblfClosCramtcon")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtbcon.FooterRow.FindControl("lblfnetdramtcon")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netdram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtbcon.FooterRow.FindControl("lblfnetcramtcon")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netcram"]).ToString("#,##0;(#,##0); ");


            Session["Report1"] = gvtbcon;
            ((HyperLink)this.gvtbcon.HeaderRow.FindControl("hlbtntbCdataExelcon")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        protected void gvtbcon_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesccon");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvcodecon")).Text;
            string mACTDESC = ((HyperLink)e.Row.FindControl("HLgvDesccon")).Text;
            string mTRNDAT1 = this.txtAsDate.Text;
            string mTRNDAT2 = this.txtAsDate.Text;
            //------------------------------//////
            Label actcode = (Label)e.Row.FindControl("lblgvcodecon");
            HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesccon");

            string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpcode")).ToString().Trim();
            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();


            if (code == "")
            {
                return;
            }


            if (grp == "A")
            {
                Label closdram = (Label)e.Row.FindControl("lblgClosDramtcon");
                Label closcramt = (Label)e.Row.FindControl("lblgvClosCramtcon");
                Label netdramt = (Label)e.Row.FindControl("lblgvnetdramtcon");
                Label netcramt = (Label)e.Row.FindControl("lblgvnetcramtcon");
                actcode.Font.Bold = true;
                actdesc.Font.Bold = true;
                closdram.Font.Bold = true;
                closcramt.Font.Bold = true;
                netdramt.Font.Bold = true;
                netcramt.Font.Bold = true;
                //actdesc.Style.Add("text-align", "right");

            }
            ///---------------------------------//// 

            if (grp == "B" && ASTUtility.Left(mACTCODE, 1) == "4")
            {
                hlink1.NavigateUrl = "AccProjectReports.aspx?actcode=" + mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }
            else if (grp == "B" && lebel2 == "")
            {

                if (ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC;
            }
            else
            {
                if (grp == "B" && ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            }











            // if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesccon");
            //    Label dramt = (Label)e.Row.FindControl("lblgvDramtcon");
            //    Label cramt = (Label)e.Row.FindControl("lblgvCramtcon");
            //    Label closdramt = (Label)e.Row.FindControl("lblgvclodramtcon");
            //    Label closcramt = (Label)e.Row.FindControl("lblgvclocramtcon");
            //    string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpcode")).ToString();

            //    if (grp == "")
            //    {
            //        return;
            //    }
            //    if (grp == "A")
            //    {

            //        actdesc.Font.Bold = true;
            //        dramt.Font.Bold = true;
            //        cramt.Font.Bold = true;
            //        closdramt.Font.Bold = true;
            //        closcramt.Font.Bold = true;
            //       // actdesc.Style.Add("text-align", "right");


            //    }

            //}
        }
        protected void lnkBankPosition02_Click(object sender, EventArgs e)
        {

            Session.Remove("tblBankPosition");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvBankPosition02.DataSource = null;
                this.gvBankPosition02.DataBind();
                return;
            }
            Session["tblBankPosition"] = ds1.Tables[0];
            this.gvBankPosition02.DataSource = ds1.Tables[0];
            this.gvBankPosition02.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvBankPosition02.HeaderRow.FindControl("hlbtnbnkpdataExel02")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            ds1.Dispose();
            Session["Report1"] = gvBankPosition;
            ((HyperLink)this.gvBankPosition02.HeaderRow.FindControl("hlbtnbnkpdataExel02")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }


        protected void lnkbtnCashBankBal_Click(object sender, EventArgs e)
        {
            Session.Remove("tblBankPosition");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvCABankBal.DataSource = null;
                this.gvCABankBal.DataBind();
                return;
            }
            Session["tblBankPosition"] = this.HiddenSameData(ds1.Tables[0]);
            //Session["tblBankPosition"] = ds1.Tables[0];
            this.gvCABankBal.DataSource = (DataTable)Session["tblBankPosition"];
            this.gvCABankBal.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvCABankBal.HeaderRow.FindControl("hlbtnbnkpdataExelcb")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            ds1.Dispose();
            Session["Report1"] = gvCABankBal;
            ((HyperLink)this.gvCABankBal.HeaderRow.FindControl("hlbtnbnkpdataExelcb")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }


        protected void gvCABankBal_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink description = (HyperLink)e.Row.FindControl("HLgvDescbankcb");
                Label netbal = (Label)e.Row.FindControl("lblgvnetbalcb");
                Label opnbalcb = (Label)e.Row.FindControl("lblgvopnamcb");
                Label closam = (Label)e.Row.FindControl("lblgvclosamcb");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    description.Font.Bold = true;
                    netbal.Font.Bold = true;
                    opnbalcb.Font.Bold = true;
                    closam.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }
                else if (ASTUtility.Right(code, 4) == "0000")
                {

                    description.Font.Bold = true;
                    opnbalcb.Font.Bold = true;
                    closam.Font.Bold = true;
                    description.Style.Add("text-align", "left");


                }


            }

        }
        protected void gvBankPosition_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescbank");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvcodebank")).Text;
            string mTRNDAT1 = this.txtDatefrombank.Text;
            string mTRNDAT2 = this.txtDatetobank.Text;
            //------------------------------//////
            Label actcode = (Label)e.Row.FindControl("lblgvcodebank");
            HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDescbank");

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString().Trim();



            if (code == "")
            {
                return;
            }


            if (ASTUtility.Right(code, 4) != "0000" && ASTUtility.Left(code, 2) == "22")
            {
                actcode.Font.Bold = true;
                actdesc.Font.Bold = true;
                //actdesc.Style.Add("text-align", "right");

                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                 "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }
            ///---------------------------------//// 





        }
    }

}