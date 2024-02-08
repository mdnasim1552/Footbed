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
using SPEENTITY;
using SPELIB;
using SPEENTITY.C_22_Sal;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_21_GAcc
{
    public partial class Print : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "PostDatVou":
                    PostVouPrint();
                    break;
                case "accVou":
                    printVoucher();
                    break;
                case "Cheque":
                    PrintCheque();
                    break;
                case "PosdatCheque":
                    RptPostDatChq();
                    break;
                case "Spdetledger":
                    PrintSpDetailsLedger();
                    break;
                case "OreqPrint":
                    Other_Req_Print();
                    break;
                case "IssueChallan":
                    this.issueDelvPrintBR();
                    break;




            }


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void PostVouPrint()
        {
            try
            {
                //if (this.ddlPrivousVou.Items.Count > 0 && this.lnkOk.Text == "Ok")
                //    this.lnkOk_Click(null, null);

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                //string curvoudat = this.txtEntryDate.Text.Substring(0, 11);
                //string pvnum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + this.txtcurrentvou.Text.Trim().Substring(2, 2) + "-" + this.txtCurrntlast6.Text.Trim();
                //string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                //this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();
                //string VouType = this.Request.QueryString["tname"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString();
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt = _ReportDataSet.Tables[0];
                if (dt.Rows.Count == 0)
                    return;
                double dramt, cramt;
                dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



                if (dramt > 0 && cramt > 0)
                {
                    TAmount = cramt;

                }
                else if (dramt > 0 && cramt <= 0)
                {
                    TAmount = dramt;
                }
                else
                {
                    TAmount = cramt;
                }

                string Type = "";// this.CompanyPrintVou();
                                 //ReportDocument rptinfo = new ReportDocument();
                                 ////if (Type == "VocherPrint")
                                 ////{
                                 //rptinfo = new RMGiRPT.R_21_GAcc.rptBankVoucher();
                                 //// }


                ////-----------------------------
                //DataTable dt1 = _ReportDataSet.Tables[1];
                //string voutype = dt1.Rows[0]["voutyp"].ToString();
                //string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                //string venar = dt1.Rows[0]["venar"].ToString();
                ////ReportDocument rptinfo = new RMGiRPT.R_15_Acc.rptBankVoucher();
                //rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcAdd.Text = comadd;

                ////TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                ////txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                //TextObject rpttxtVoutype = rptinfo.ReportDefinition.ReportObjects["txtVoutype"] as TextObject;
                //rpttxtVoutype.Text = voutype;
                //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                //vounum1.Text = "Voucher No: " + vounum;
                //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                //date.Text = " Date:" + voudat;
                //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                //naration.Text = ((comcod == "7307") ? "On Account of: " : "Narration: ") + venar;
                //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);
                //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                //if (ConstantInfo.LogStatus == true)
                //{
                //    string eventtype = "Post Dated Cheque";
                //    string eventdesc = "Print Voucher";
                //    string eventdesc2 = vounum;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                //}
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptinfo.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptinfo;
                //this.lblprintstkl.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //               "PDF" + "', target='_self');</script>";
            }
            catch (Exception ex)
            {
                this.lmsg.Text = "Error:" + ex.Message;
            }
        }
        private void printVoucher()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string vounum = this.Request.QueryString["vounum"].ToString();



            string Unitname = "";// filtrcom[0].comname.ToString();


            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");

            var lst = ds.Tables[0].DataTableToList<SPEENTITY.C_21_Acc.EClassAccVoucher.EclassPrintVoucherBr>();
            //DataTable dt = ds.Tables[1];
            string paymode = ((ASTUtility.Left(vounum, 2) == "BD") || (ASTUtility.Left(vounum, 2) == "BC")) ? "BANK"
                        : ((ASTUtility.Left(vounum, 2) == "CD") || (ASTUtility.Left(vounum, 2) == "CC")) ? "CASH" : "";//ds.Tables[1].Rows[0]["refnum"].ToString();
            string refno = ds.Tables[1].Rows[0]["refnum"].ToString();
            string srinfo = ds.Tables[1].Rows[0]["srinfo"].ToString();
            string payto = (ASTUtility.Left(vounum, 2) == "BC") ? "Recieved From:" : (ASTUtility.Left(vounum, 2) == "CC") ? "Recieved From:" : "Pay To: " + ds.Tables[1].Rows[0]["payto"].ToString();

            string venar = ds.Tables[1].Rows[0]["venar"].ToString();

            double dramt, cramt;
            dramt = (lst.Select(p => p.Dr).Sum() == 0.00) ? 0.00 : lst.Select(p => p.Dr).Sum();
            cramt = (lst.Select(p => p.Cr).Sum() == 0.00) ? 0.00 : lst.Select(p => p.Cr).Sum();
            string voutype = ds.Tables[1].Rows[0]["voutyp"].ToString();



            string vounum1 = ASTUtility.Left((ds.Tables[0].Rows[0]["vounum"].ToString()), 2);
            string nVoutype = (vounum1 == "BD") ? "Bank Payment Voucher" : (vounum1 == "CD") ? "Cash Payment Voucher" :
                  (vounum1 == "BC") ? "Bank Deposit Voucher" : (vounum1 == "CC") ? "Cash Deposit Voucher" : (vounum1 == "JV") ? "Journal Voucher" :
                  (vounum1 == "CT") ? "Contra Voucher" : voutype;


            string type = vounum1;


            string voudate = Convert.ToDateTime(ds.Tables[1].Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            string bankname = ds.Tables[1].Rows[0]["cactdesc"].ToString();
            string usrNam = ds.Tables[1].Rows[0]["username"].ToString();

            //string voudate = ds.Tables[1].Rows["voudat"].ToString("dd-MM-yyyy");
            LocalReport Rpt1 = new LocalReport();

            switch (comcod)
            {
                case "5305" :
                case "5306" :
                    Rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptPrintVoucherSPFB", lst, null, null); //this

                    break;

                default:
                    Rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptPrintVoucherSP", lst, null, null);

                    break;
            }

            Rpt1.SetParameters(new ReportParameter("vounum", "Voucher No:" + vounum));
            Rpt1.SetParameters(new ReportParameter("voudate", "Voucher Date:" + voudate));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("header2", Unitname));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("type", type));
            Rpt1.SetParameters(new ReportParameter("usrNam", usrNam));
            Rpt1.SetParameters(new ReportParameter("paymode", paymode));
            Rpt1.SetParameters(new ReportParameter("refno", refno));
            Rpt1.SetParameters(new ReportParameter("srinfo", srinfo));
            Rpt1.SetParameters(new ReportParameter("payto", payto));
            Rpt1.SetParameters(new ReportParameter("RptTitle", nVoutype));
            Rpt1.SetParameters(new ReportParameter("appuname", ds.Tables[1].Rows[0]["appuname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("dramt", dramt.ToString("#,##0.00; (#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("cramt", cramt.ToString("#,##0.00; (#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(cramt), 2)));
            Rpt1.SetParameters(new ReportParameter("varNar", venar));

            //rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(NetAmt), 2)));



            //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //naration.Text = ((comcod == "7307") ? "On Account of: " : "Narration: ") + venar;


            Session["Report1"] = Rpt1;
            this.lblprintstkl.Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=PDF', target='_self');</script>";
        }
        private void PrintCheque()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString();
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MM-yyyy");
                string voudat2 = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MM-yyyy");

                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split(' ', ' ');
                // string[] amtdivide = amtWrd1[1].Split(' ');

                //string value = this.ChboxPayee.Checked ? "A/C Payee" : " ";
                string value = this.Request.QueryString["payee"].ToString();

                for (int i = 0; i <= amtWrd1.Length - 1; i++)
                {
                    if (i == amtWrd1.Length)
                    {
                        return;
                    }
                    else if (i > 9)
                    {
                        wam1 += " " + amtWrd1[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtWrd1[i].ToString();
                    }
                }

                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["Payee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["date2"] = voudat2;
                hshtbl["amtWord"] = (wam2).Replace("Taka", "").ToUpper();
                hshtbl["amtWord1"] = (wam1).Replace("Taka", "").ToUpper();
                hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";

                LocalReport rpt1 = new LocalReport();

                //if (comcod == "7305")
                //{
                //    rpt1 = RptSetupClass.GetLocalReport("RD_15_Acc.RptChequelinnex", hshtbl, null, null);
                //}
                //else
                //{
                rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptCheque2", hshtbl, null, null);
                // }

                Session["Report1"] = rpt1;
                this.lblprintstkl.Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=PDF', target='_Self');</script>";

            }
            catch (Exception ex)
            {
                this.lmsg.Text = "Error:" + ex.Message;
            }



        }

        private void RptPostDatChq()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString();// this.ddlPostDatedCheque.SelectedValue.Substring(0, 14);
                string chqno = this.Request.QueryString["chequeno"].ToString();// this.ddlPostDatedCheque.SelectedValue.Substring(14);
                DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                //this.txtfromdatec1.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                //this.txttodatec1.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd-MM-yyyy");
                string voudat2 = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd-MM-yyyy");

                // voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split(' ', ' ');
                // string[] amtdivide = amtWrd1[1].Split(' ');
                string value = this.Request.QueryString["payee"].ToString();
                for (int i = 0; i <= amtWrd1.Length - 1; i++)
                {
                    if (i == amtWrd1.Length)
                    {
                        return;
                    }
                    else if (i > 8)
                    {
                        wam1 += " " + amtWrd1[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtWrd1[i].ToString();
                    }
                }

                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["Payee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["date2"] = voudat2;
                hshtbl["amtWord"] = wam2.ToUpper();
                hshtbl["amtWord1"] = wam1.ToUpper();
                hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";

                LocalReport rpt1 = new LocalReport();

                //if (comcod == "7305")
                //{
                //    rpt1 = RptSetupClass.GetLocalReport("RD_15_Acc.RptChequelinnex", hshtbl, null, null);
                //}
                //else
                // {
                //rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptCheque", hshtbl, null, null);
                // }
                rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptCheque2", hshtbl, null, null);
                Session["Report1"] = rpt1;
                this.lblprintstkl.Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=PDF', target='_Self');</script>";
            }
            catch (Exception ex)
            {
                this.lmsg.Text = "Error:" + ex.Message;
            }
        }


        //private void RptOthReq(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
        //    string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

        //    DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
        //         "", "", "", "", "", "", "");
        //    ReportDocument rptstk = new RMGiRPT.R_34_Mgt.rptOtherReqStatus();

        //    DataTable dt1 = new DataTable();
        //    dt1 = (DataTable)Session["tblReq"];
        //    DataTable dt2 = new DataTable();
        //    dt2 = ((DataTable)Session["tblUserReq"]).Copy();
        //    DataTable dtsign = ds1.Tables[2];//(DataTable)Session["tblUsersign"];



        //    TextObject txtcompanyame = rptstk.ReportDefinition.ReportObjects["txtcompanyame"] as TextObject;
        //    txtcompanyame.Text = comnam;
        //    TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
        //    rpttxtnaration.Text = this.txtReqNarr.Text.Trim();

        //    TextObject txtrefno = rptstk.ReportDefinition.ReportObjects["txtrefno"] as TextObject;
        //    txtrefno.Text = "Ref No : " + this.txtMRFNo.Text.ToString().Trim();
        //    TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
        //    txtcrdate.Text = "Date : " + this.txtCurReqDate.Text.ToString().Trim();
        //    TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
        //    txtcrno.Text = "Requisition No : " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
        //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

        //    TextObject txtPtype = rptstk.ReportDefinition.ReportObjects["txtPtype"] as TextObject;
        //    txtPtype.Text = "Pay Type: " + this.rblpaytype.SelectedItem.Text.ToString();//"Pay Type: " + dt2.Rows[0]["paytype"].ToString(); 

        //    TextObject tctPayto = rptstk.ReportDefinition.ReportObjects["tctPayto"] as TextObject;
        //    tctPayto.Text = "Pay To: " + this.txtPayto.Text.ToString();  //"Pay To: " + dt2.Rows[0]["payto"].ToString(); 





        //    TextObject rpttxtReq = rptstk.ReportDefinition.ReportObjects["txtReq"] as TextObject;
        //    rpttxtReq.Text = dtsign.Rows[0]["reqnam"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();



        //    if (ConstantInfo.LogStatus == true)
        //    {

        //        string eventtype = "Other Req Entry";
        //        string eventdesc = "Print Report";
        //        string eventdesc2 = "Requisition No: " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }


        //    rptstk.SetDataSource(dt1);

        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rptstk.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptstk;

        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //}
        private void PrintSpDetailsLedger()
        {
            string comcod = this.GetCompCode();
            string resource = this.Request.QueryString["genno"].ToString();
            string frmdate = this.Request.QueryString["date1"].ToString();
            string todate = this.Request.QueryString["date2"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESOURCELG", resource, frmdate, todate, "", "", "", "", "", "");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptsl = new RMGiRPT.R_21_GAcc.RPTSpecialLedger();
            //string session = hst["session"].ToString();
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //DataTable dt = (DataTable)Session["tblspledger"];

            //var rptlist = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.AccSpLedger>();

            //LocalReport Rpt1a = new LocalReport();

            //Rpt1a = RMGiRDLC.RptSetupClass.GetLocalReport("R_21_GAcc.RptSpLedger", rptlist, null, null);

            //Rpt1a.EnableExternalImages = true;
            //Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            //Rpt1a.SetParameters(new ReportParameter("date", frmdate + " To " + todate));
            //Rpt1a.SetParameters(new ReportParameter("footer", printFooter));
            //Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));

            //Session["Report1"] = Rpt1a;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        private void Other_Req_Print()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CurDate1 = this.Request.QueryString["date1"].ToString();
            string mReqNo = this.Request.QueryString["genno"].ToString();


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
                 "", "", "", "", "", "", "");

            string PurOrderno = ds1.Tables[1].Rows[0]["orderno"].ToString();
            string Orderdate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["orderdate"]).ToString("dd-MMM-yyyy");
            string supliern = ds1.Tables[1].Rows[0]["supdesc"].ToString();
            string mrfno = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            DataTable dt1 = new DataTable();
            //dt1 = (DataTable)ViewState["tblReq"];

            DataView dv = ds1.Tables[0].Copy().DefaultView;//((DataTable)ViewState["tblReq"])
            dv.RowFilter = ("pactcode not like '2398%'");
            dt1 = dv.ToTable();


            DataView dv1 = ds1.Tables[0].Copy().DefaultView;
            dv1.RowFilter = ("pactcode like '23980001%'");
            DataTable dt3 = dv1.ToTable();

            DataView dv2 = ds1.Tables[0].Copy().DefaultView;
            dv2.RowFilter = ("pactcode like '23980002%'");
            DataTable dt4 = dv2.ToTable();

            double AIT = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(proamt)", "")) ?
                0.00 : dt3.Compute("Sum(proamt)", "")));
            double VAT = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(proamt)", "")) ?
                0.00 : dt4.Compute("Sum(proamt)", "")));
            double pAmt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(proamt)", "")) ?
                0.00 : dt1.Compute("Sum(proamt)", "")));

            double nAmt = pAmt + AIT + VAT;

            double tvat = (AIT + VAT) * -1;

            DataTable dt2 = new DataTable();
            dt2 = ds1.Tables[2].Copy();
            DataTable dtsign = ds1.Tables[2];//(DataTable)Session["tblUsersign"];

            string usrName = dtsign.Rows[0]["reqnam"].ToString();
            string checkuser = dtsign.Rows[0]["reqanam"].ToString();
            string Appusrname = dtsign.Rows[0]["faprovnam"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;



            var lst = dt1.DataTableToList<SPEENTITY.C_34_Mgt.OthReqStatus>();
            LocalReport rpt1 = new LocalReport();

            if (comcod == "5301")
            {


                rpt1 = RptSetupClass.GetLocalReport("R_34_Mgt.RptBillSticker", lst, null, null);
                rpt1.EnableExternalImages = true;

                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("Rpttitle", "BILL STICKER"));
                rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

                rpt1.SetParameters(new ReportParameter("Cdate", "Date: " + CurDate1));
                rpt1.SetParameters(new ReportParameter("VenName", supliern));
                rpt1.SetParameters(new ReportParameter("VenBillno", ""));
                rpt1.SetParameters(new ReportParameter("VDate", CurDate1));
                rpt1.SetParameters(new ReportParameter("BillAmount", pAmt.ToString("#,##0;(#,##0); ")));

                rpt1.SetParameters(new ReportParameter("PurOrderno", PurOrderno));
                rpt1.SetParameters(new ReportParameter("PurDate", Orderdate));
                rpt1.SetParameters(new ReportParameter("MRNo", ""));
                rpt1.SetParameters(new ReportParameter("RefNo", "Ref No : P&SCM: " + mrfno));

                rpt1.SetParameters(new ReportParameter("PerparedBy", usrName));
                rpt1.SetParameters(new ReportParameter("CheckedBy", checkuser));
                rpt1.SetParameters(new ReportParameter("ApprovededBy", Appusrname));


                rpt1.SetParameters(new ReportParameter("LessAIT", (AIT * -1).ToString("#,##0;(#,##0); ")));
                rpt1.SetParameters(new ReportParameter("AIT", (VAT * -1).ToString("#,##0;(#,##0); ")));
                rpt1.SetParameters(new ReportParameter("Advance", ""));
                rpt1.SetParameters(new ReportParameter("NetPayable", nAmt.ToString("#,##0;(#,##0); ")));

                rpt1.SetParameters(new ReportParameter("Totalamt", tvat.ToString("#,##0;(#,##0); ")));


                
            }
            else
            {


                rpt1 = RptSetupClass.GetLocalReport("R_15_DPayReg.RptOtherReq", lst, null, null);
                rpt1.EnableExternalImages = true;

                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("Rpttitle", "Other Requisition"));
                rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

                rpt1.SetParameters(new ReportParameter("Cdate", "Date: " + CurDate1));
                rpt1.SetParameters(new ReportParameter("VenName", supliern));
                rpt1.SetParameters(new ReportParameter("VenBillno", ""));
                rpt1.SetParameters(new ReportParameter("VDate", CurDate1));
                rpt1.SetParameters(new ReportParameter("BillAmount", pAmt.ToString("#,##0;(#,##0); ")));

                rpt1.SetParameters(new ReportParameter("PurOrderno", PurOrderno));
                rpt1.SetParameters(new ReportParameter("PurDate", Orderdate));
                rpt1.SetParameters(new ReportParameter("MRNo", ""));
                rpt1.SetParameters(new ReportParameter("RefNo", "Ref No : P&SCM: " + mrfno));

                rpt1.SetParameters(new ReportParameter("PerparedBy", usrName));
                rpt1.SetParameters(new ReportParameter("CheckedBy", checkuser));
                rpt1.SetParameters(new ReportParameter("ApprovededBy", Appusrname));


                rpt1.SetParameters(new ReportParameter("LessAIT", (AIT * -1).ToString("#,##0;(#,##0); ")));
                rpt1.SetParameters(new ReportParameter("AIT", (VAT * -1).ToString("#,##0;(#,##0); ")));
                rpt1.SetParameters(new ReportParameter("Advance", AIT.ToString("#,##0;(#,##0); ")));
                rpt1.SetParameters(new ReportParameter("NetPayable", nAmt.ToString("#,##0;(#,##0); ")));

                rpt1.SetParameters(new ReportParameter("Totalamt", tvat.ToString("#,##0;(#,##0); ")));
                
            }

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



        }

        private void issueDelvPrintBR()
        {
            string mISUNo = this.Request.QueryString["issueno"].ToString();
            string CurDate1 = this.Request.QueryString["issuedat"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Request.QueryString["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            //    string username = hst["username"].ToString();
            //    string empname = hst["username"].ToString();
            //string custadd = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GETISSUEINFO", mISUNo, CurDate1, "", "", "", "", "", "", "");
            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];

            string issudate = Convert.ToDateTime(dt1.Rows[0]["issuedat"]).ToString("dd-MMMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var rptlist = dt.DataTableToList<Sales_BO.EissueDelvPrint>();
            //var rptlist = dt.DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.EissueDelvPrint>();


            //string store = list[0].actdesc;
            LocalReport rpt1 = new LocalReport();

            rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_23_SaM.RptissueDelvBR", rptlist, null, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("empname", dt1.Rows[0]["empname"].ToString()));
            rpt1.SetParameters(new ReportParameter("custadd", dt1.Rows[0]["custadd"].ToString()));
            rpt1.SetParameters(new ReportParameter("phone", dt1.Rows[0]["phone"].ToString()));
            rpt1.SetParameters(new ReportParameter("issueno1", dt1.Rows[0]["issueno1"].ToString()));
            rpt1.SetParameters(new ReportParameter("issuedat", issudate));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            //  rpt1.SetParameters(new ReportParameter("rptTitle", "INDENT ISSUE"));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }
    }

}