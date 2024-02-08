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

using System.IO;
using SPELIB;
using SPEENTITY.C_19_Exp;
using Microsoft.Reporting.WinForms;
using SPERDLC;


namespace SPEWEB.F_19_EXP
{
    public partial class SalesRealCertificate : System.Web.UI.Page
    {
        SalesInvoice_BL lst = new SalesInvoice_BL();
        Common _common = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.TextFdate.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.TextTodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");



            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            this.GetDataItem();
        }

        private void GetDataItem()
        {

            string fdate = Convert.ToDateTime(this.TextFdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string tdate = Convert.ToDateTime(this.TextTodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string rptype = this.ddlRptType.SelectedValue.ToString();


            List<SPEENTITY.C_19_Exp.Sales_BO.SalesRealCertificate> lst1 = lst.GetProcRealSheet(fdate, tdate, rptype);


            this.gvProceedRelSheet.DataSource = lst1;
            this.gvProceedRelSheet.DataBind();

            this.gvProcdRelCert.DataSource = lst1;
            this.gvProcdRelCert.DataBind();


            ViewState["SalesRealizeCert"] = lst1;

        }

        protected void ddlpagesize_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod1 = _common.GetCompCode();

            if(comcod1 == "5305" || comcod1 == "5306")
            {
                PrintNewSalesRealizationReport();
            }
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                //string hostname = hst["hostname"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string session = hst["session"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string frmdate = Convert.ToDateTime(this.TextFdate.Text).ToString("dd-MMMM-yyyy");
                string todate = Convert.ToDateTime(this.TextTodate.Text).ToString("dd-MMMM-yyyy");
                string dateft = "Date: (From " + frmdate + " To " + todate + ")";

                var lst = (List<SPEENTITY.C_19_Exp.Sales_BO.SalesRealCertificate>)ViewState["SalesRealizeCert"];
                LocalReport rpt1 = new LocalReport();

                rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptSalesRealizeCer", lst, null, null);

                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("rpttitle", "PROCEED REALIZATION SHEET"));
                rpt1.SetParameters(new ReportParameter("DateFT", dateft));

                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


                Session["Report1"] = rpt1;
                //BDAccSession.Current.RdlcReport1 = Rpt1;

                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

        }

        private void PrintNewSalesRealizationReport()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.TextFdate.Text).ToString("dd-MMMM-yyyy");
            string todate = Convert.ToDateTime(this.TextTodate.Text).ToString("dd-MMMM-yyyy");
            string dateft = "Date: (From " + frmdate + " To " + todate + ")";

            var lst = (List<SPEENTITY.C_19_Exp.Sales_BO.SalesRealCertificate>)ViewState["SalesRealizeCert"];
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptSalesRealizeCert2", lst, null, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Export Proceeds Realisation Certificate Against Direct Export"));
            rpt1.SetParameters(new ReportParameter("DateFT", dateft));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}