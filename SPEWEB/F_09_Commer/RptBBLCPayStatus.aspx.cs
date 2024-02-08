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

namespace SPEWEB.F_09_Commer
{
    public partial class RptBBLCPayStatus : System.Web.UI.Page
    {
        ProcessAccess Data = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                this.rbtnList1.SelectedIndex = 0;
                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.GetOrderName();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "BBLC Payment Status";
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }





        private void GetOrderName()
        {
            string SrchTxt = this.txtSrcOrder.Text.Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CallType = (this.rbtnList1.SelectedIndex == 0) ? "MLCFORBBLCL" : "GETSUPPLIER";
            DataSet ds1 = Data.GetTransInfo(comcod, "SP_ENTRY_BACK2BACKLC", CallType, SrchTxt, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlOrderList.DataTextField = "actdesc1";
            this.ddlOrderList.DataValueField = "actcode";
            this.ddlOrderList.DataSource = ds1.Tables[0];
            this.ddlOrderList.DataBind();


        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            this.RptBBLCPay();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Export Plan Vs Achivement";
                string eventdesc = "Show Report: ";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        private void RptBBLCPay()
        {
            this.LoadDetailsData();

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }


        private void LoadDetailsData()
        {
            Session.Remove("tbBBLCPay");
            string comcod = this.GetCompCode();
            string ordercode = this.ddlOrderList.SelectedValue.ToString();
            string Type = (this.rbtnList1.SelectedIndex == 0 ? "1" : "");
            string Date = this.txtDatefrom.Text;
            DataSet ds1 = Data.GetTransInfo(comcod, "SP_ENTRY_BACK2BACKLC", "BBLBPAYSTATUS", ordercode, "1", Date, "", "", "", "", "", "");

            if (ds1.Tables[0] == null)
            {
                this.gvRptBBLCPay.DataSource = null;
                this.gvRptBBLCPay.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tbBBLCPay"] = dt1;

            this.LoadGrid();
            this.FooterCalculation();

        }
        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tbBBLCPay"];

            this.gvRptBBLCPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptBBLCPay.DataSource = dt;
            this.gvRptBBLCPay.DataBind();



        }
        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string mlccod = dt1.Rows[0]["mlccod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mlccod"].ToString() == mlccod)
                {
                    mlccod = dt1.Rows[j]["mlccod"].ToString();
                    dt1.Rows[j]["mlcdesc"] = "";
                }

                else
                {
                    mlccod = dt1.Rows[j]["mlccod"].ToString();
                }

            }

            return dt1;
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbBBLCPay"];
            if (dt.Rows.Count == 0)
                return;

            double amt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(grossamt)", "")) ?
                               0 : dt.Compute("sum(grossamt)", "")));
            double dueamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueamt)", "")) ?
                               0 : dt.Compute("sum(dueamt)", "")));
            double paidamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
                               0 : dt.Compute("sum(paidamt)", "")));


            ((Label)this.gvRptBBLCPay.FooterRow.FindControl("lgvAmt")).Text = amt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvRptBBLCPay.FooterRow.FindControl("lgvFDueAmt")).Text = dueamt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvRptBBLCPay.FooterRow.FindControl("lgvFPaidAmt")).Text = paidamt.ToString("#,##0;(#,##0); ");
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            this.RptBBLCPaySta();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Export Plan Vs Achivement";
                string eventdesc = "Print Report: ";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void RptBBLCPaySta()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //   string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //   string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //  string ToFrDate = "(From :" + fromdate + " To " + todate + ")";

            DataTable dt = (DataTable)Session["tbBBLCPay"];

            var lst = dt.DataTableToList<SPEENTITY.C_09_Commer.BBLCPayStatus>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptBBLCPayStatus", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("ToFrDate", " From " + this.txtDatefrom.Text.Trim()));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "BBLC PAYMENT STATUS"));
            rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            rpt1.SetParameters(new ReportParameter("Order", "Order No: " + this.ddlOrderList.SelectedItem.Text.Substring(14).ToString()));
            rpt1.SetParameters(new ReportParameter("Type", this.rbtnList1.SelectedItem.Text));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            // rpt1.SetParameters(new ReportParameter("todate", DateTime.Today.ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
 



        //private void RptBBLCPayStatus()
        //{

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


        //    DataTable dt1 = (DataTable)Session["tbBBLCPay"];
        //    //DataTable dt2 = (DataTable)Session["tbBBLCPayDat"];txtOrder
        //    ReportDocument rrs1 = new RMGiRPT.R_09_Commer.rptBBLCPayStatus();
        //    TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    rptCname.Text = comnam;

        //    TextObject rptOrder = rrs1.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
        //    rptOrder.Text = this.ddlOrderList.SelectedItem.Text.Substring(14).ToString();

        //    TextObject txtHead = rrs1.ReportDefinition.ReportObjects["txtHead"] as TextObject;
        //    txtHead.Text = this.rbtnList1.SelectedItem.Text;

        //    //TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
        //    //txtTitle.Text = "Work Order Status( " + basis + " )";
        //    TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rrs1.SetDataSource(dt1);

        //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    //rrs1.SetParameterValue("ComLogo", ComLogo);

        //    Session["Report1"] = rrs1;
        //    lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.LoadGrid();


        }




        protected void imgbtnFindOrder_Click(object sender, EventArgs e)
        {
            this.GetOrderName();
        }

        protected void gvRptBBLCPay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRptBBLCPay.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblName.Text = (this.rbtnList1.SelectedIndex == 0) ? "Order No" : "Supplier";
            this.GetOrderName();
        }
    }
}