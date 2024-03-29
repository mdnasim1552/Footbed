﻿using System;
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


namespace SPEWEB.F_81_Hrm.F_90_PF
{

    public partial class AccTrialBalance : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
            //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            ((Label)this.Master.FindControl("lblTitle")).Text = "Accounts Trial Balance";

            if (this.txtDatefrom.Text.Trim().Length == 0)
            {
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private DataSet GetDataForReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11).ToString();
            string date2 = this.txtDateto.Text.Substring(0, 11).ToString();
            string level = this.ddlReportLevel.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "TB_COMPANY_0" + level, date1, date2, "", "", "", "", "", "", "");
            return ds1;
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;
            this.dgv1.DataSource = ds1.Tables[0];
            this.dgv1.DataBind();
            ((Label)this.dgv1.FooterRow.FindControl("lblfopnamt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ") + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");
            //this.dgv1.Columns[2].FooterText = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0.00;(#,##0.00); ") + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfcloamt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ") + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptAccTrialBalance();
            ////ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptAccTrialBalance();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtadress.Text = comadd;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = "TRIAL BALANCE - " + this.ddlReportLevel.SelectedValue.ToString().Trim();
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy") + ")";
            //TextObject txtopeingname1 = rptstk.ReportDefinition.ReportObjects["opeingname1"] as TextObject;
            //txtopeingname1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtopeingname2 = rptstk.ReportDefinition.ReportObjects["opeingname2"] as TextObject;
            //txtopeingname2.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtdramount = rptstk.ReportDefinition.ReportObjects["dramount"] as TextObject;
            //txtdramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); "); ;

            //TextObject txtcramount = rptstk.ReportDefinition.ReportObjects["cramount"] as TextObject;
            //txtcramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); "); ;

            //TextObject txtclosingamount1 = rptstk.ReportDefinition.ReportObjects["closingamount1"] as TextObject;
            //txtclosingamount1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtclosingamount2 = rptstk.ReportDefinition.ReportObjects["closingamount2"] as TextObject;
            //txtclosingamount2.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); "); ;


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(ds1.Tables[0]);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvcode")).Text;
            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;

            if (ASTUtility.Right(mACTCODE, 4) == "0000")
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            else
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        }
    }
}