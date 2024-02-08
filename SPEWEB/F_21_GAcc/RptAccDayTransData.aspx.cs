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
    public partial class RptAccDayTransData : System.Web.UI.Page
    {

        ProcessAccess MktData = new ProcessAccess();
        public static double OpenBal, Clsbal, Dtdram, Dtcram;
        public static int PageNumber = 0;

        //double OpenBal = 0, Clsbal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = " Daily transaction";
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //PageNumber = 0;
            //this.lblCurPage.Text = "1";
            //this.lblPage.Visible = true;
            //this.txtPageNo.Visible = true;
            //this.imgbtnSearchPage.Visible = true;

            this.TransactionList();

            DataTable dt = (DataTable)Session["LastTable"];
            //double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            //int pageCount = (int)Math.Ceiling(getPageCount);
            //this.lblCurPage.ToolTip = "Page 1 of " + pageCount;

            if (ConstantInfo.LogStatus == true)
            {
                // string eventtype = this.LblTitle.Text;
                //string eventdesc = "Show Report";
                //string eventdesc2 = "";
                // bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void TransactionList()
        {
            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            // this.Paneltovoucherno.Visible = true;
            //this.GridPage.Visible = true;
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtVouType = this.ddlVouchar.SelectedItem.ToString().Trim();
            string txtSVoucher = (txtVouType == "ALL Voucher" ? "" : txtVouType) + "%";


            string searchinfo = "";

            if (this.ddlSrch.SelectedValue != "")
            {

                if (this.ddlSrch.SelectedValue == "between")
                {
                    // searchinfo = "dram between " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmount2.Text.Trim()).ToString();
                }
                else
                {
                    searchinfo = "dram " + this.ddlSrch.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString();
                }
            }
            //int PageIndex = 0;
            //int pageSize = 100;
            //int startRow =PageIndex * pageSize;
            //  int startRow = PageNumber * 100;
            //int endRow = (PageNumber + 1) * 100;


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "PRINTTRANSACTIONS", fromdate, todate, txtSVoucher, searchinfo, "", "", "", "", "");
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
            // Session["LastTable"] = ds1.Tables[3];
            // this.FooterCalculation(ds1.Tables[3], "gvtranlsit");

            this.lbltoCashVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[0]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            this.lbltoBankVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[1]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            this.lbltoContraVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[2]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            this.lbltoJournalVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[3]["tonum"]).ToString("#, #,#0; (#, #,#0); ");
            Session["Report1"] = gvtranlsit;
            if (dtr1.Rows.Count > 0)
                ((HyperLink)this.gvtranlsit.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }


        private void RptTransactionList()
        {
            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //this.Paneltovoucherno.Visible = true;
            //this.GridPage.Visible = true;
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtVouType = this.ddlVouchar.SelectedItem.ToString().Trim();
            string txtSVoucher = (txtVouType == "ALL Voucher" ? "" : txtVouType) + "%";


            string searchinfo = "";

            if (this.ddlSrch.SelectedValue != "")
            {

                if (this.ddlSrch.SelectedValue == "between")
                {
                    //searchinfo = "dram between " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmount2.Text.Trim()).ToString();
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

            Session["tranlist"] = dtr1;

            this.FooterCalculation(dtr1, "gvtranlsitp");


        }
        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();

            //
            switch (GvName)
            {


                case "gvtranlsit":

                    ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    //Dtdram = Convert.ToDouble("0" + ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text);
                    //Dtcram = Convert.ToDouble("0" + ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text);
                    break;
                case "gvdtranlsitp":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acrescode not in('" + "  Total Amt:" + "')");
                    dt = dv.ToTable();
                    string dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    string cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0.00;(#,##0.00) ;");
                    Dtdram = Convert.ToDouble("0" + dramt);
                    Dtcram = Convert.ToDouble("0" + cramt);
                    break;



            }


        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Date1, vounum;
            int j;

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


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            this.PrintTransaction();

            if (ConstantInfo.LogStatus == true)
            {
                //string eventtype = this.LblTitle.Text;
                // string eventdesc = "Print Report";//
                //string eventdesc2 = "";//
                // bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        private void PrintTransaction()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string cashvou = "Cash Voucher:" + this.lbltoCashVoucher.Text;
            string bankvou = "Bank Voucher:" + this.lbltoBankVoucher.Text;
            string contravou = "Contra Voucher:" + this.lbltoContraVoucher.Text;
            string journvou = "Journal Voucher:" + this.lbltoJournalVoucher.Text;
            string dateft = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string LCname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            //string fdate = this.txtDatefrom.Text.ToString();
            //string tdate = this.txtdateto.Text.ToString();
            //string ToFrDate = "(From :" + fdate + " To " + tdate + ")";

            DataTable dt = (DataTable)Session["tranlist"];


            //var lst = ds.Tables[0].DataTableToList<SPEENTITY.C_09_C>();
            var lst = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccVoucher.AccDTransactrtionAll>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptAllTrans", lst, null, null);
            //rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("dateft", dateft));
            rpt1.SetParameters(new ReportParameter("cashvou", cashvou));
            rpt1.SetParameters(new ReportParameter("bankvou", bankvou));
            rpt1.SetParameters(new ReportParameter("contravou", contravou));
            rpt1.SetParameters(new ReportParameter("journvou", journvou));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Daily Transaction-All"));
            //rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintTransactionOld()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tranlist"];
            //ReportDocument rptdtlist = new RMGiRPT.R_21_GAcc.RptDailyTrans();
            //TextObject rptCname = rptdtlist.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptdate = rptdtlist.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

            //TextObject rptCasVou = rptdtlist.ReportDefinition.ReportObjects["txtCasVou"] as TextObject;
            //rptCasVou.Text = "Cash Voucher:" + this.lbltoCashVoucher.Text;
            //TextObject rptBanVou = rptdtlist.ReportDefinition.ReportObjects["txtbanVou"] as TextObject;
            //rptBanVou.Text = "Bank Voucher:" + this.lbltoBankVoucher.Text;
            //TextObject rptConVou = rptdtlist.ReportDefinition.ReportObjects["txtConVou"] as TextObject;
            //rptConVou.Text = "Contra Voucher:" + this.lbltoContraVoucher.Text;
            //TextObject rptJourVou = rptdtlist.ReportDefinition.ReportObjects["txtJourVou"] as TextObject;
            //rptJourVou.Text = "Journal Voucher:" + this.lbltoJournalVoucher.Text;

            //TextObject txtuserinfo = rptdtlist.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptdtlist.SetDataSource(dt);
            //Session["Report1"] = rptdtlist;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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

                double DrAmt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "dram"));
                double CrAmt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "cram"));
                string Narr = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acrescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (DrAmt == 0.00 & CrAmt == 0.00 && Narr != "  Narration:")
                {

                    acrescode.Font.Bold = true;
                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    lblcramt.Font.Bold = true;
                }
                if (code == "  Total Amt:")
                {

                    acrescode.Style.Add("color", "Green");
                    acresdesc.Style.Add("color", "Green");
                    lbldram.Style.Add("color", "Green");
                    lblcramt.Style.Add("color", "Green");

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

        protected void imgbtnSearchVoucher_Click(object sender, EventArgs e)
        {
            this.TransactionList();
        }

        protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.lblTo.Visible = (this.ddlSrch.SelectedValue == "between");
            //this.txtAmount2.Visible = (this.ddlSrch.SelectedValue == "between");
        }


        protected void imgBtnFirst_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["LastTable"];
            //double getPageCount = (Convert.ToDouble(dt.Rows[0]["tpage"]) / 100);
            //int pageCount = (int)Math.Ceiling(getPageCount);

            //PageNumber = 0;
            this.TransactionList();
            //this.lblCurPage.Text = "1";
            //this.lblCurPage.ToolTip = "Page 1 of " + pageCount;
            //this.imgBtnPerv.Enabled = false;
            //this.imgBtnNext.Enabled = true;
        }
        

    }
}