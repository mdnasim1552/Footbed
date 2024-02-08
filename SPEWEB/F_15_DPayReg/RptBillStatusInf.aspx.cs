using System;
using System.Collections;
using System.Collections.Generic;
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
using SPERDLC;

namespace SPEWEB.F_15_DPayReg
{
    public partial class RptBillStatusInf : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                this.txtReceiveDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


                ((Label)this.Master.FindControl("lblTitle")).Text = "Bill Status Information";
                // this.lmsg.Visible = false;
                this.btnAllBill_Click(null, null);
                this.rblpaytype.SelectedIndex = 0;
            }

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            if (this.Request.QueryString["comcod"].Length == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }
            else
            {
                return (this.Request.QueryString["comcod"].ToString());
            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RMGiRPT.R_15_DPayReg.RpBillStatusInfo();
            TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompanyName.Text = comnam;
            TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date : " + txtReceiveDate.Text;

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)Session["BillAmt"];

            rptstk.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private string GetSlNum()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETSLNUM", "", "", "", "", "", "", "", "", "");
            return ds2.Tables[0].Rows[0]["slnum"].ToString();
        }
        protected void btnAllBill_Click(object sender, EventArgs e)
        {
            Session.Remove("BillAmt");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string ttsrch = "%";
            string searchbill = (rblpaytype.SelectedIndex == 0 ? "resource" : "");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT", "GETBILLNO", ttsrch, searchbill, "", "", "", "", "", "", "");
            Session["BillAmt"] = ds1.Tables[0];
            this.Data_Bind();

        }


        private void Data_Bind()
        {

            DataTable tbl1 = (DataTable)Session["BillAmt"];
            this.gvPayment.DataSource = tbl1;
            this.gvPayment.DataBind();

            if (tbl1.Rows.Count > 0)
            {
                ((Label)this.gvPayment.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(amt)", "")) ? 0.00 : tbl1.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");

            }



        }




        protected void txtReceiveDate_TextChanged(object sender, EventArgs e)
        {
            //this.txtReceiveDate.Text = ASTUtility.DateInVal(this.txtReceiveDate.Text);
            //this.txtRefno.Focus();
        }

    }
}