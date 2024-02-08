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
using SPEENTITY;

namespace SPEWEB.F_21_GAcc
{
    public partial class RptAccVouher : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "JV")
                {
                    this.lblBankDescription.Visible = false;
                    this.lblValBankDescription.Visible = false;

                }

                this.lblPayto.Text = (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "BD") ? "Pay To :"
                    : (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "CD") ? "Pay To :"
                    : (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "BC") ? "Received From :"
                    : (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "CC") ? "Received From :" : "Pay To :";

                if (this.Request.QueryString["vounum"].ToString().Substring(0, 2) == "JV")
                {
                    this.lblBankDescription.Visible = false;
                    this.lblValBankDescription.Visible = false;

                }
                 ((Label)this.Master.FindControl("lblTitle")).Text = "General Voucher";
                //((Label)this.Master.FindControl("lblTitle")).Text = "Sales, Received, Receivable & Cost";
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.ShowVoucher();



            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected string GetCompCode()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //return(hst["comcod"].ToString());
            return (this.Request.QueryString["comcod"].ToString());

        }


        private void ShowVoucher()
        {
            string comcod = this.GetCompCode();
            string vounum = this.Request.QueryString["vounum"].ToString();

            DataSet _EditDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", vounum, "", "", "", "", "", "", "", "");
            DataTable dt = this.HiddenSameData(_EditDataSet.Tables[0]);

            if (dt.Rows.Count == 0)
                return;

            DataTable dtedit = _EditDataSet.Tables[1];
            this.lblvalVoucherDate.Text = Convert.ToDateTime(dtedit.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            this.lblvalVoucherNo.Text = dtedit.Rows[0]["vounum"].ToString().Substring(0, 2) + "-" + dtedit.Rows[0]["vounum"].ToString().Substring(6);
            this.lblValBankDescription.Text = dtedit.Rows[0]["cactdesc"].ToString();

            this.lblisunum.Text = dtedit.Rows[0]["isunum"].ToString();
            this.lblvalRefNum.Text = dtedit.Rows[0]["refnum"].ToString();
            this.lblvalSirinfo.Text = dtedit.Rows[0]["srinfo"].ToString();


            this.lblvalpayto.Text = dtedit.Rows[0]["payto"].ToString();
            this.lblvalNarration.Text = dtedit.Rows[0]["venar"].ToString();
            this.dgv1.DataSource = dt;
            this.dgv1.DataBind();

            ((Label)this.dgv1.FooterRow.FindControl("lblFgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trndram)", "")) ?
                           0 : dt.Compute("sum(trndram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("txtFgvCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trncram)", "")) ?
                  0 : dt.Compute("sum(trncram)", ""))).ToString("#,##0;(#,##0); ");

            //-------------------------------------------------//
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string actcode = dt1.Rows[0]["actcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();

                }

            }
            return dt1;

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            this.printVoucherSP();


        }


        private void printVoucherSP()
        {
            string curvoudat = this.lblvalVoucherDate.Text.Substring(0, 11);
            string vounum = this.lblvalVoucherNo.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
                    this.lblvalVoucherNo.Text.Trim().Substring(3);
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('Print.aspx?Type=accVou&vounum=" + vounum + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();

            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            ////string vounum = this.lstVouname.SelectedValue.ToString();





            //string curvoudat = this.lblvalVoucherDate.Text.Substring(0, 11);
            //string vounum = this.lblvalVoucherNo.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
            //        this.lblvalVoucherNo.Text.Trim().Substring(3);


            //DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");


            //var lst = ds.Tables[0].DataTableToList<RMGiEntity.C_21_Acc.EClassAccVoucher.EclassPrintVoucherBr>();
            ////DataTable dt = ds.Tables[1];
            //string paymode = ((ASTUtility.Left(vounum, 2) == "BD") || (ASTUtility.Left(vounum, 2) == "BC")) ? "BANK"
            //            : ((ASTUtility.Left(vounum, 2) == "CD") || (ASTUtility.Left(vounum, 2) == "CC")) ? "CASH" : "";//ds.Tables[1].Rows[0]["refnum"].ToString();
            //string refno = ds.Tables[1].Rows[0]["refnum"].ToString();
            //string payto = (ASTUtility.Left(vounum, 2) == "BC") ? "Recieved From:" : (ASTUtility.Left(vounum, 2) == "CC") ? "Recieved From:" : "Pay To: " + ds.Tables[1].Rows[0]["payto"].ToString();

            //string venar = ds.Tables[1].Rows[0]["venar"].ToString();

            //double dramt, cramt;
            //dramt = (lst.Select(p => p.Dr).Sum() == 0.00) ? 0.00 : lst.Select(p => p.Dr).Sum();
            //cramt = (lst.Select(p => p.Cr).Sum() == 0.00) ? 0.00 : lst.Select(p => p.Cr).Sum();
            //string voutype = ds.Tables[1].Rows[0]["voutyp"].ToString();



            //string vounum1 = ASTUtility.Left((ds.Tables[0].Rows[0]["vounum"].ToString()), 2);
            //string nVoutype = (vounum1 == "BD") ? "Bank Payment Voucher" : (vounum1 == "CD") ? "Cash Payment Voucher" :
            //      (vounum1 == "BC") ? "Bank Deposit Voucher" : (vounum1 == "CC") ? "Cash Deposit Voucher" : (vounum1 == "JV") ? "Journal Voucher" :
            //      (vounum1 == "CT") ? "Contra Voucher" : voutype;


            //string type = vounum1;


            //string voudate = Convert.ToDateTime(ds.Tables[1].Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            //string bankname = ds.Tables[1].Rows[0]["cactdesc"].ToString();
            //string usrNam = ds.Tables[1].Rows[0]["username"].ToString();

            ////string voudate = ds.Tables[1].Rows["voudat"].ToString("dd-MM-yyyy");
            //LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RptSetupClass.GetLocalReport("RD_15_Acc.RptPrintVoucherSP", lst, null, null);



            //Rpt1.SetParameters(new ReportParameter("vounum", "Voucher No:" + vounum));
            //Rpt1.SetParameters(new ReportParameter("voudate", "Voucher Date:" + voudate));
            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            //Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //Rpt1.SetParameters(new ReportParameter("header2", ""));
            //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("type", type));
            //Rpt1.SetParameters(new ReportParameter("usrNam", usrNam));
            //Rpt1.SetParameters(new ReportParameter("paymode", paymode));
            //Rpt1.SetParameters(new ReportParameter("refno", refno));
            //Rpt1.SetParameters(new ReportParameter("payto", payto));
            //Rpt1.SetParameters(new ReportParameter("RptTitle", nVoutype));
            //Rpt1.SetParameters(new ReportParameter("dramt", dramt.ToString("#,##0.00; (#,##0.00); ")));
            //Rpt1.SetParameters(new ReportParameter("cramt", cramt.ToString("#,##0.00; (#,##0.00); ")));
            //Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(cramt), 2)));
            //Rpt1.SetParameters(new ReportParameter("varNar", venar));

            ////rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(NetAmt), 2)));



            ////TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            ////naration.Text = ((comcod == "7307") ? "On Account of: " : "Narration: ") + venar;


            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }






    }
}