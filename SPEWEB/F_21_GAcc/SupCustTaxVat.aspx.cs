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
using SPELIB;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_21_GAcc
{
    public partial class SupCustTaxVat : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Tax & Vat";
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;


                this.txfdate.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                LoadAcccombo();
                LoadsupCustList();

                rbttype.SelectedIndex = 0;
                rbttype_SelectedIndexChanged(null, null);

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintSupCusTxVt();
        }
        private void LoadAcccombo()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETTAXVATACCOUNTSHEAD", "%", "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                this.ddlConAccHead.DataSource = dt1;
                this.ddlConAccHead.DataTextField = "actdesc1";
                this.ddlConAccHead.DataValueField = "actcode";
                this.ddlConAccHead.DataBind();
                //this.GetPriviousVoucher();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }
        private void LoadsupCustList()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string mSrchTxt = "%";
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
                if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
                {

                    return;
                }

                this.ddlSupCust.DataTextField = "ssirdesc1";
                this.ddlSupCust.DataValueField = "ssircode";
                this.ddlSupCust.DataSource = ds1.Tables[0];
                this.ddlSupCust.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }

        }


        private void GetData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string fdate = this.txfdate.Text;
            string tdate = this.txtdate.Text;

            string achead = this.ddlConAccHead.SelectedValue.ToString();
            string supcode = this.ddlSupCust.SelectedValue.ToString();



            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "RPTSUPCUSTWITHTAXVAT", fdate, tdate, achead, supcode);
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.dgv2.DataSource = null;
                this.dgv2.DataBind();
                return;
            }

            ViewState["tblsuptxtvat"] = ds1.Tables[0];
            dgv2_DataBind();
        }

        private void PrintSupCusTxVt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            // string achead = hst["achead"].ToString();
            //string supcode = hst["supcode"].ToString();

            string tdate = this.txtdate.Text.Trim();
            string fdate = this.txfdate.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            // DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "RPTSUPCUSTWITHTAXVAT", fdate, tdate, achead, supcode);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)ViewState["tblsuptxtvat"];
            var rptlist = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.SupCustTxVt>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_21_GAcc.RptSupCusTxVt", rptlist, null, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("fdate", fdate));
            rpt1.SetParameters(new ReportParameter("tdate", tdate));

            //rpt1.SetParameters(new ReportParameter("achead", achead));
            //rpt1.SetParameters(new ReportParameter("supcode", supcode));

            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("compname", username, printdate)));




            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void dgv2_DataBind()
        {
            DataTable dt = (DataTable)ViewState["tblsuptxtvat"];

            this.dgv2.DataSource = dt;
            this.dgv2.DataBind();



            ((Label)this.dgv2.FooterRow.FindControl("FotTgvclsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsamt)", "")) ?
                          0 : dt.Compute("sum(clsamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.dgv2.FooterRow.FindControl("FOTgvCrAmtDB")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                  0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.dgv2.FooterRow.FindControl("FOTgvDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.dgv2.FooterRow.FindControl("FOTgvopnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opamt)", "")) ?
               0 : dt.Compute("sum(opamt)", ""))).ToString("#,##0;(#,##0); ");

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (rbttype.SelectedValue == "1")
            {
                GetData();
            }
            else
            {
                GetDataForDetTB();
            }
        }

        protected void rbttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbttype.SelectedValue == "1")
            {
                this.suplsection.Visible = true;
                pnlAll.Visible = false;
                pnlDealis.Visible = true;



            }
            else
            {
                pnlDealis.Visible = false;
                pnlAll.Visible = true;
                this.suplsection.Visible = false;
                GetDataForDetTB();
            }
        }


        protected void GetDataForDetTB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string mACTCODE1 = this.ddlConAccHead.SelectedValue.ToString();

            string mTRNDAT1 = this.txfdate.Text;
            string mTRNDAT2 = this.txtdate.Text;

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTDETAILSTB2",
                          mTRNDAT1, mTRNDAT2, "12", "12", mACTCODE1, "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.grvDTB.DataSource = ds1.Tables[0];
            this.grvDTB.DataBind();
            ((Label)this.grvDTB.FooterRow.FindControl("lblfopnamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(opnam)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvDTB.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(dram)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvDTB.FooterRow.FindControl("lblfCramt")).Text = "<br>" + Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(cram)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvDTB.FooterRow.FindControl("lblfcloamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(closam)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(closam)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void grvDTB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = ((Label)e.Row.FindControl("lblComcod")).Text;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvAccode")).Text;
            string mRESCODE = ((Label)e.Row.FindControl("lblgvcode")).Text;
            string mTRNDAT1 = this.txfdate.Text;
            string mTRNDAT2 = this.txtdate.Text;
            string mACTDESC = ((Label)e.Row.FindControl("gvAcDesc")).Text;
            string mREESDESC = ((Label)e.Row.FindControl("gvResDesc")).Text;

            hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE +
                "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            //if (ASTUtility.Right(mACTCODE, 4) == "0000")
            //    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
            //         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            //else
            //    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
            //"&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        }
    }
}