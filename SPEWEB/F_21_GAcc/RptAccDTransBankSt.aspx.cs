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
    public partial class RptAccDTransBankSt : System.Web.UI.Page
    {

        ProcessAccess MktData = new ProcessAccess();
        public static double OpenBal, Clsbal, Dtdram, Dtcram;

        //double OpenBal = 0, Clsbal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = " Bank Statement Information";

                this.txtfromdate.Text = System.DateTime.Today.AddDays(-1).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.LoadAcccombo();
            }



        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void LoadAcccombo()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string ttsrch = this.txtSrcProject.Text + "%";
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "GETCONACCHEAD", ttsrch, "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();
            }
            catch (Exception ex)
            {
                //this.lblmsg.Text = "Error:" + ex.Message;
            }

        }


        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowCashBook();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
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
            string RptGroup = ((this.rbtnGroup.SelectedItem.Text.Trim() == "Deposit") ? "R" : (this.rbtnGroup.SelectedItem.Text.Trim() == "Withdraw") ? "P" : "%");
            string searchinfo = this.ddlConAccHead.SelectedValue.ToString().Trim();
            //string txtSProject =  "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "RPTCASHBOOKBANKSTAT", fromdate, todate, txtSVoucher, searchinfo, RptGroup, "", "", "", "");
            if (ds1 == null)
                return;

            string RptGroup1 = this.rbtnGroup.SelectedItem.Text.Trim();
            switch (RptGroup1)
            {
                case "Deposit":
                    this.lblReceiptCash.Visible = true;
                    this.lblPaymentCash.Visible = false;
                    this.lblDetailsCash.Visible = true;
                    this.lblDepUnclr.Visible = true;
                    this.lblWidUnclr.Visible = false;

                    break;
                case "Withdraw":
                    this.lblReceiptCash.Visible = false;
                    this.lblPaymentCash.Visible = true;
                    this.lblDetailsCash.Visible = true;
                    this.lblDepUnclr.Visible = false;
                    this.lblWidUnclr.Visible = true;
                    break;

                case "Both":
                    this.lblReceiptCash.Visible = true;
                    this.lblPaymentCash.Visible = true;
                    this.lblDepUnclr.Visible = true;
                    this.lblWidUnclr.Visible = true;
                    this.lblDetailsCash.Visible = (this.ddlVoucharCash.SelectedValue == "ALL Voucher");
                    break;
            }

            /////////

            Session["cashbank"] = ds1;




            DataView dvr = new DataView();
            dvr = ds1.Tables[0].DefaultView;
            dvr.RowFilter = ("grp1 = 'A'");
            DataTable dtr1 = HiddenSameDate(dvr.ToTable());
            this.gvcashbook.DataSource = dtr1;
            this.gvcashbook.DataBind();
            this.FooterCalculation(dtr1, "gvcashbook");

            dvr = ds1.Tables[0].DefaultView;
            dvr.RowFilter = ("grp1='B'");
            DataTable dtr2 = HiddenSameDate(dvr.ToTable());
            this.gvcashbookp.DataSource = dtr2;
            this.gvcashbookp.DataBind();
            this.FooterCalculation(dtr2, "gvcashbookp");

            dvr = ds1.Tables[0].DefaultView;
            dvr.RowFilter = ("grp1='D'");
            DataTable dtr3 = HiddenSameDate(dvr.ToTable());
            this.gvDepUnclr.DataSource = dtr3;
            this.gvDepUnclr.DataBind();
            this.FooterCalculation(dtr3, "gvcashbookDV");

            dvr = ds1.Tables[0].DefaultView;
            dvr.RowFilter = ("grp1='E'");
            DataTable dtr4 = HiddenSameDate(dvr.ToTable());
            this.gvWidUnclr.DataSource = dtr4;
            this.gvWidUnclr.DataBind();
            this.FooterCalculation(dtr4, "gvcashbookPV");

            this.gvcashbookDB.DataSource = ds1.Tables[1];
            this.gvcashbookDB.DataBind();
            this.FooterCalculation(ds1.Tables[1], "gvcashbookDB");
        }




        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();

            //
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
                case "gvcashbookDV":
                    ((Label)this.gvDepUnclr.FooterRow.FindControl("lgvFCashAmtDV")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                             0 : dt.Compute("sum(casham)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    ((Label)this.gvDepUnclr.FooterRow.FindControl("lgvFBankAmtDV")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                           0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    break;
                case "gvcashbookPV":
                    ((Label)this.gvWidUnclr.FooterRow.FindControl("lgvFCashAmtpayPV")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                             0 : dt.Compute("sum(casham)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    ((Label)this.gvWidUnclr.FooterRow.FindControl("lgvFBankAmtPV")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                           0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    break;

                case "gvcashbookDB":


                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lblgvFrecam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(depam)", "")) ?
                                   0 : dt.Compute("sum(depam)", ""))).ToString("#,##0.00;(#,##0.00) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                             0 : dt.Compute("sum(payam)", ""))).ToString("#,##0.00;(#,##0.00) ;");

                    string RptGroup = this.rbtnGroup.SelectedItem.Text.Trim();
                    switch (RptGroup)
                    {
                        case "Deposit":
                            this.gvcashbookDB.Columns[3].Visible = false;
                            this.gvcashbookDB.Columns[2].Visible = true;
                            break;
                        case "Withdraw":
                            this.gvcashbookDB.Columns[2].Visible = false;
                            this.gvcashbookDB.Columns[3].Visible = true;
                            break;
                        default:
                            this.gvcashbookDB.Columns[2].Visible = true;
                            this.gvcashbookDB.Columns[3].Visible = true;
                            break;


                    }
                    break;
            }
        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
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
            return dt1;
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string RptGroup = this.rbtnGroup.SelectedItem.Text.Trim();
            switch (RptGroup)
            {
                case "Deposit":
                    break;
                case "Withdraw":
                    break;
                default:
                    this.PrintCashBook();
                    break;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void PrintCashBook()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");

            string rptTitle = "Bank Statement";
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string searchinfo = this.ddlConAccHead.SelectedValue.ToString().Trim();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "RPTBOOKBANKSTAT", fromdate, todate, searchinfo, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["cashbank"] = (ds1.Tables[0]);
            DataTable dt = HiddenSameDate(ds1.Tables[0]);

            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_21_Acc.EClassDB_BO.CashBankStatement>();

            LocalReport rpt = new LocalReport();

            rpt = RptSetupClass.GetLocalReport("R_21_GAcc.RptCashBankStatement", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("comnam", comnam));
            rpt.SetParameters(new ReportParameter("compadd", compadd));
            rpt.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt.SetParameters(new ReportParameter("rptTitle", rptTitle));
            rpt.SetParameters(new ReportParameter("fromdate", fromdate));
            rpt.SetParameters(new ReportParameter("todate", todate));
            rpt.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument rptcb1 = new RMGiRPT.R_21_GAcc.RptBankStatement();
            //TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptBname = rptcb1.ReportDefinition.ReportObjects["txtBank"] as TextObject;
            //rptBname.Text = this.ddlConAccHead.SelectedItem.Text.Substring(13);

            //TextObject rptWc = rptcb1.ReportDefinition.ReportObjects["txtWC"] as TextObject;
            //rptWc.Text = ds1.Tables[1].Rows[0]["payamcount"].ToString();

            //TextObject rptDc = rptcb1.ReportDefinition.ReportObjects["txtDC"] as TextObject;
            //rptDc.Text = ds1.Tables[1].Rows[0]["depamcount"].ToString();

            //TextObject rptAllC = rptcb1.ReportDefinition.ReportObjects["txtAllCount"] as TextObject;
            //rptAllC.Text = "Total Tran#: " + ds1.Tables[1].Rows[0]["allcount"].ToString();

            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        protected void imgbtnSearchVoucherCash_Click(object sender, EventArgs e)
        {
            this.LoadAcccombo();
        }


    }

}