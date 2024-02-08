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

namespace SPEWEB.F_21_GAcc
{
    public partial class RptChequeIssuedList : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Cheque Register";

                this.GetBankName();

            }

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string DateFT = "Date: (From " + txtfromdate + " To " + todate + ")";
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string bankname = this.ddlBankName.SelectedItem.Text.Substring(13);
            DataTable dt = (DataTable)Session["tblCheque"];
            var lst = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.ListOfIssuedCheque>();

            // string trnamt = Convert.ToDouble(lst[0].trnamt).ToString("#,##0.00; (#,##0.00); ");

            LocalReport rpt1 = new LocalReport();

            rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_21_GAcc.ListOfIssuedCheque", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("DateFT", DateFT));
            rpt1.SetParameters(new ReportParameter("bankname", bankname));
            rpt1.SetParameters(new ReportParameter("RptTitle", "CHEQUE REGISTER"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            //rpt1.SetParameters(new ReportParameter("Delvp", ASTUtility.Cominformation()));

            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo)); 
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetBankName()
        {
            string comcod = this.GetCompCode();
            string SearchBank = "%" + this.txtSrcBank.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "GETBANKCODE", SearchBank, "", "", "", "", "", "", "", "");
            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1;
            this.ddlBankName.DataBind();
            ds1.Dispose();



        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            this.ShowData();



        }


        private void ShowData()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Bankcode = (this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBankName.SelectedValue.ToString() + "%";

            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "GETCHEQUEISSUED", frmdate, todate, Bankcode, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvChequelist.DataSource = null;
                this.gvChequelist.DataBind();
                return;
            }


            Session["tblCheque"] = ds2.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {


            DataTable dt = (DataTable)Session["tblCheque"];

            this.gvChequelist.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvChequelist.DataSource = dt;
            this.gvChequelist.DataBind();
            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvChequelist.FooterRow.FindControl("lgFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnamt)", "")) ?
                            0.00 : dt.Compute("Sum(trnamt)", ""))).ToString("#,##0;(#,##0); ");
            }




        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }


        protected void gvChequelist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvChequelist.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetBankName();
        }

        public object dt { get; set; }
        protected void gvChequelist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hypBill = (HyperLink)e.Row.FindControl("hypBill");
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (billno.Length != 0)
                {
                    string custbill = ASTUtility.Left(billno, 3).ToString();

                    if (custbill == "PBL")
                    {
                        hypBill.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;
                        hypBill.Style.Add("color", "blue");
                    }


                    else if (custbill == "POR")
                    {
                        hypBill.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=OrderPrint&comcod=" + comcod + "&orderno=" + billno;
                        hypBill.Style.Add("color", "blue");
                    }
                    else
                    {

                    }
                }









            }
        }
    }
}