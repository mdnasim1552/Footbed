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
using SPERDLC;
using Microsoft.Reporting.WinForms;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_21_GAcc
{
    public partial class AccControlSchedule : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            ((Label)this.Master.FindControl("lblTitle")).Text = "ACCOUNT CONTROL SCHEDULE";
            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            if (this.txtFromdat.Text.Trim().Length == 0)
            {
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtFromdat.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtTodat.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtFromdat.Text = DateTime.Today.AddDays(-90).ToString("dd-MMM-yyyy ddd");
                //this.txtTodat.Text = DateTime.Today.ToString("dd-MMM-yyyy ddd");
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private DataSet GetDataForReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string date1 = this.txtFromdat.Text.Substring(0, 11);
            string date2 = this.txtTodat.Text.Substring(0, 11);
            string level = this.ddlRptlbl.SelectedItem.Text.Substring(5);
            string TopHead = "dfdsf";//(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlAccHeads.SelectedValue.ToString();
            string WzeroBal = (this.chkWiZeroBal.Checked) ? "WZero" : "";
            //string calltype1=this
            string CallType = (this.ddlAccHeads.SelectedValue.ToString().Substring(0, 2) == "26") ? "PROJECTLIABILITIES" : (((this.Request.QueryString["Type"] == "Type01") ? "CSCH_REPORT_LEVEL01_0" : "CSCH_REPORT_LEVEL02_0") + level);
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_CSCH", CallType, date1, date2, TopHead, actcode, "", WzeroBal, "", "", "");


            return ds2;
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.txtFromdat.Text == "" && this.txtTodat.Text == "")
            {
                this.lblmsg.Text = "Please select from date and to date.";
                return;
            }// End If
            if (this.ddlAccHeads.SelectedValue.ToString() == "")
            {
                this.lblmsg.Text = "Please select Accounts Code.";
                return;
            }// End If
            try
            {

                DataSet ds2 = GetDataForReport();
                if (ds2 == null)
                {
                    this.dgv2.DataSource = null;
                    this.dgv2.DataBind();
                    return;
                }
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    this.lblmsg.Text = "There is no Transaction in this Accounts Code.";
                    this.lblmsg.ForeColor = System.Drawing.Color.Blue;
                    return;
                }



                this.dgv2.DataSource = ds2.Tables[0];
                this.dgv2.DataBind();
                ((Label)this.dgv2.FooterRow.FindControl("lblfopnDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(opndram)", "")) ?
                                0 : ds2.Tables[0].Compute("sum(opndram)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfopnCramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(opncram)", "")) ?
                               0 : ds2.Tables[0].Compute("sum(opncram)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(dram)", "")) ?
                               0 : ds2.Tables[0].Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(cram)", "")) ?
                               0 : ds2.Tables[0].Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
                double closdramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(closdram)", "")) ? 0 : ds2.Tables[0].Compute("sum(closdram)", "")));
                double closcramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(closcram)", "")) ? 0 : ds2.Tables[0].Compute("sum(closcram)", "")));
                ((Label)this.dgv2.FooterRow.FindControl("lblfcloDramt")).Text = closdramt.ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfcloCramt")).Text = closcramt.ToString("#,##0;(#,##0); ");
                ((Label)this.dgv2.FooterRow.FindControl("lblfcloNetamt")).Text = (closdramt - closcramt).ToString("#,##0;(#,##0); ");
                Session["Report1"] = dgv2;
                ((HyperLink)this.dgv2.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }
        }

        protected void imgsearch_Click(object sender, EventArgs e)
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string filter = this.txtSearch.Text.Trim() + "%";
                // string calltype=asi
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_CSCH", "GETCONACCHEAD02", filter, "", "", "", "", "", "", "", "");
                DataTable dt1 = ds1.Tables[0];
                this.ddlAccHeads.DataSource = dt1;
                this.ddlAccHeads.DataTextField = "actdesc1";
                this.ddlAccHeads.DataValueField = "actcode";
                this.ddlAccHeads.DataBind();

            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string title = "Account Control Schedule Report - " + ASTUtility.Right((this.ddlRptlbl.SelectedValue.ToString().Trim()), 1);
            string daterange = "(From " + Convert.ToDateTime(this.txtFromdat.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtTodat.Text.Trim()).ToString("dd-MMM-yyyy") + ")";
            DataSet ds2 = GetDataForReport();
            if (ds2 == null)
                return;
            var list = ds2.Tables[0].DataTableToList<SPEENTITY.C_21_Acc.EClassAccVoucher.EclassAccControlSchdule>();
            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptAccControlSchule", list, null, null);
            //rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("rpttitle", title));
            rpt1.SetParameters(new ReportParameter("daterange", daterange));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptAccControlSchedule();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            txtTitle.Text = "Account Control Schedule Report - " + ASTUtility.Right((this.ddlRptlbl.SelectedValue.ToString().Trim()), 1);
            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "(From " + Convert.ToDateTime(this.txtFromdat.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtTodat.Text.Trim()).ToString("dd-MMM-yyyy") + ")";



            //TextObject txtopeingname1 = rptstk.ReportDefinition.ReportObjects["opeingname1"] as TextObject;
            //txtopeingname1.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");
            //TextObject txtopeingname2 = rptstk.ReportDefinition.ReportObjects["opeingname2"] as TextObject;
            //txtopeingname2.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); "); ;

            //TextObject txtdramount = rptstk.ReportDefinition.ReportObjects["dramount"] as TextObject;
            //txtdramount.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtcramount = rptstk.ReportDefinition.ReportObjects["cramount"] as TextObject;
            //txtcramount.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); "); ;

            //TextObject txtclosingamount1 = rptstk.ReportDefinition.ReportObjects["closingamount1"] as TextObject;
            //txtclosingamount1.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");

            //TextObject txtclosingamount2 = rptstk.ReportDefinition.ReportObjects["closingamount2"] as TextObject;
            //txtclosingamount2.Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); "); ;
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Control Schedule";
                string eventdesc = "Print Schedule";
                string eventdesc2 = this.ddlAccHeads.SelectedItem.ToString() + "  (From " + Convert.ToDateTime(this.txtFromdat.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtTodat.Text.Trim()).ToString("dd-MMM-yyyy") + ")"; ;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            rptstk.SetDataSource(ds2.Tables[0]);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvCode")).Text;
            string mACTDesc = ((Label)e.Row.FindControl("lblgvDesc")).Text;
            string mTRNDAT1 = this.txtFromdat.Text;
            string mTRNDAT2 = this.txtTodat.Text;

            if (ASTUtility.Right(mACTCODE, 4) == "0000")
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&actdesc=" + mACTDesc +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            else
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&actdesc=" + mACTDesc +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        }

    }
}