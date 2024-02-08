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
namespace SPEWEB.F_81_Hrm.F_90_PF
{
    public partial class RptAccDTransaction : System.Web.UI.Page
    {

        ProcessAccess MktData = new ProcessAccess();
        public static double OpenBal, Clsbal, Dtdram, Dtcram;

        //double OpenBal = 0, Clsbal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
            //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            ((Label)this.Master.FindControl("lblTitle")).Text = "MONEY RECEIPT INFORMATION VIEW/EDIT";




            if (this.txtfromdate.Text.Trim().Length == 0)
            {
                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void TransactionList()
        {
            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string txtSProject = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTTRANSACTIONS", fromdate, todate, txtSProject, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            DataTable dtr1 = HiddenSameDate(dtr);
            this.gvtranlsit.DataSource = dtr1;
            this.gvtranlsit.DataBind();
            Session["tranlist"] = dtr1;
            this.FooterCalculation(dtr1, "gvtranlsit");

        }

        private void ShowCashBook()
        {
            Session.Remove("cashbank");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTCASHBOOK", fromdate, todate, txtSProject, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            Session["cashbank"] = dtr;


            DataView dvr = new DataView();
            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1 = 'A'");
            DataTable dtr1 = HiddenSameDate(dvr.ToTable());
            this.gvcashbook.DataSource = dtr1;
            this.gvcashbook.DataBind();
            this.FooterCalculation(dtr1, "gvcashbook");



            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='B'");
            DataTable dtr2 = HiddenSameDate(dvr.ToTable());
            this.gvcashbookp.DataSource = dtr2;
            this.gvcashbookp.DataBind();
            this.FooterCalculation(dtr2, "gvcashbookp");


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

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RP_COMPANY_04", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            Session["recandpay"] = dtr;

            DataView dvr = new DataView();
            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='B'");
            DataTable dtr2 = dvr.ToTable();
            this.gvrecapaybal.DataSource = dtr2;
            this.gvrecapaybal.DataBind();
            this.FooterCalculation(dtr2, "gvrecapaybal");


            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1 = 'A'");
            DataTable dtr1 = dvr.ToTable();
            this.gvrecandpay.DataSource = dtr1;
            this.gvrecandpay.DataBind();
            this.FooterCalculation(dtr1, "gvrecandpay");




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
                            0 : dt.Compute("sum(casham)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvFBankAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                          0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;


                case "gvcashbookp":
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvCashAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                             0 : dt.Compute("sum(casham)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvFBankAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                           0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;

                case "gvcashbookDB":
                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                             0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lblgvFrecam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(depam)", "")) ?
                                   0 : dt.Compute("sum(depam)", ""))).ToString("#,##0;(#,##0) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                             0 : dt.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFClAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                           0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;

                case "gvtranlsit":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acrescode not in('" + "  Total Amt:" + "')");
                    dt = dv.ToTable();
                    ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0) ;");
                    Dtdram = Convert.ToDouble("0" + ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text);
                    Dtcram = Convert.ToDouble("0" + ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text);
                    break;




                case "gvrecandpay":
                    ((Label)this.gvrecandpay.FooterRow.FindControl("lblgvFrecpam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recpam)", "")) ?
                            0 : dt.Compute("sum(recpam)", ""))).ToString("#,##0;(#,##0) ;");
                    double topenbal = 0, tclsbal = 0;
                    topenbal = OpenBal + Convert.ToDouble("0" + ((Label)this.gvrecandpay.FooterRow.FindControl("lblgvFrecpam")).Text.Trim());

                    ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFObal")).Text = OpenBal.ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFTRAmt")).Text = topenbal.ToString("#,##0;(#,##0) ;");

                    ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFpayam1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                          0 : dt.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0) ;");
                    tclsbal = Clsbal + Convert.ToDouble("0" + ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFpayam1")).Text.Trim());
                    ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFCbal")).Text = Clsbal.ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFTPAmt")).Text = tclsbal.ToString("#,##0;(#,##0) ;");
                    break;


                case "gvrecapaybal":
                    OpenBal = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recpam)", "")) ? 0 : dt.Compute("sum(recpam)", "")));
                    ((Label)this.gvrecapaybal.FooterRow.FindControl("lgvFOpening1")).Text = OpenBal.ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvrecapaybal.FooterRow.FindControl("lblgvdepam1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(depam)", "")) ?
                                   0 : dt.Compute("sum(depam)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvrecapaybal.FooterRow.FindControl("lgvFwitam1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(witham)", "")) ?
                             0 : dt.Compute("sum(witham)", ""))).ToString("#,##0;(#,##0) ;");
                    Clsbal = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ? 0 : dt.Compute("sum(payam)", "")));
                    ((Label)this.gvrecapaybal.FooterRow.FindControl("lgvFClAmt1")).Text = Clsbal.ToString("#,##0;(#,##0) ;");
                    break;






            }


            {


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


            if (this.rbtnList1.SelectedIndex == 0)
            {
                this.PrintCashBook();

            }
            else if (this.rbtnList1.SelectedIndex == 1)
            {
                this.PrinTransaction();

            }

            else if (this.rbtnList1.SelectedIndex == 2)
            {
                this.PrintReceiveAndPayment();

            }
        }

        private void PrintCashBook()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = HiddenSameDate((DataTable)Session["cashbank"]);
            //ReportDocument rptcb1 = new RMGiRPT.R_21_GAcc.RptAccCashbook1();
            //// ReportDocument rptcb1 = new RMGiRPT.R_21_GAcc.RptAccCashbook1();
            //TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(dt);
            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }


        private void PrinTransaction()
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
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintReceiveAndPayment()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["recandpay"];
            //ReportDocument rptrandpay = new RMGiRPT.R_21_GAcc.RptRecAndPayment();
            //TextObject rptCname = rptrandpay.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptdate = rptrandpay.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
            //TextObject rptopenbal = rptrandpay.ReportDefinition.ReportObjects["Openbal"] as TextObject;
            //rptopenbal.Text = ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFObal")).Text.Trim();
            //TextObject rpttopebal = rptrandpay.ReportDefinition.ReportObjects["topenbal"] as TextObject;
            //rpttopebal.Text = ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFTRAmt")).Text.Trim();
            //TextObject rptclsbal = rptrandpay.ReportDefinition.ReportObjects["clsbal"] as TextObject;
            //rptclsbal.Text = ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFCbal")).Text.Trim();
            //TextObject rpttclsbal = rptrandpay.ReportDefinition.ReportObjects["tclsbal"] as TextObject;
            //rpttclsbal.Text = ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFTPAmt")).Text.Trim();
            //TextObject txtuserinfo = rptrandpay.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptrandpay.SetDataSource(dt);
            //Session["Report1"] = rptrandpay;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }



        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
                    this.ShowCashBook();
                    break;
                case 1:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
                    this.TransactionList();
                    break;
                case 2:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
                    this.ReceiptAndPayment();
                    break;
                case 3:

                    break;
                case 4:

                    break;
            }
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

                }

            }
        }
    }

}