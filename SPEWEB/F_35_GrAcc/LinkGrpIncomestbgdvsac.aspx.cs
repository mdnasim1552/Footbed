using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;

namespace SPEWEB.F_35_GrAcc
{
    public partial class LinkGrpIncomestbgdvsac : System.Web.UI.Page
    {
        ProcessAccess rptdata = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Master LC Cost Vs Expenses";
                this.lblDate.Text = this.Request.QueryString["Date"].ToString();

                this.GetMasterLc();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComCode()
        {

            return (this.Request.QueryString["comcod"].ToString());


        }
        private void GetMasterLc()
        {
            string comcod = GetComCode();
            string txtsrch = this.txtpmlcsrch.Text.Trim() + "%";
            DataSet ds1 = rptdata.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LCCVSEXPENSES", "GETMASTERLC", txtsrch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlMLc.DataTextField = "actdesc";
            this.ddlMLc.DataValueField = "actcode";
            this.ddlMLc.DataSource = ds1.Tables[0];
            this.ddlMLc.DataBind();

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tblCostVsEx");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = GetComCode();
            string mlccode = this.ddlMLc.SelectedValue.ToString();
            string todate = this.Request.QueryString["Date"].ToString();
            DataSet ds2 = rptdata.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LCCVSEXPENSES", "LCCOSTVSEXPENSES", mlccode, todate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvcostvsex.DataSource = null;
                this.gvcostvsex.DataBind();
                return;
            }
            Session["tblCostVsEx"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }



        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string mgrp = dt1.Rows[0]["mgrp"].ToString();
            string grp = dt1.Rows[0]["grp"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mgrp"].ToString() == grp && dt1.Rows[j]["grp"].ToString() == grp)
                {
                    mgrp = dt1.Rows[j]["mgrp"].ToString();
                    grp = dt1.Rows[j]["grp"].ToString();

                    dt1.Rows[j]["mgrpdesc"] = "";
                    dt1.Rows[j]["grpdesc"] = "";

                }

                else
                {
                    if (dt1.Rows[j]["mgrp"].ToString() == mgrp)
                    {
                        dt1.Rows[j]["mgrpdesc"] = "";
                    }

                    if (dt1.Rows[j]["grp"].ToString() == grp)
                    {
                        dt1.Rows[j]["grpdesc"] = "";
                    }

                    mgrp = dt1.Rows[j]["mgrp"].ToString();
                    grp = dt1.Rows[j]["grp"].ToString();

                }

            }
            return dt1;

        }

        private void Data_Bind()
        {
            this.gvcostvsex.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvcostvsex.DataSource = (DataTable)Session["tblCostVsEx"];
            this.gvcostvsex.DataBind();
            //this.FooterCal();


        }

        //private void FooterCal() 
        //{
        //    DataTable dt = (DataTable)Session["tblCostVsEx"];
        //    if (dt.Rows.Count == 0)
        //        return;

        //    ((Label)this.gvcostvsex.FooterRow.FindControl("lblgvFCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itmamt)", "")) ?
        //                              0 : dt.Compute("sum(itmamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvcostvsex.FooterRow.FindControl("lblgvFPreamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(preamt)", "")) ?
        //                         0 : dt.Compute("sum(preamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvcostvsex.FooterRow.FindControl("lblgvFCurAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curamt)", "")) ?
        //                         0 : dt.Compute("sum(curamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvcostvsex.FooterRow.FindControl("lblgvFToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ?
        //                         0 : dt.Compute("sum(toamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvcostvsex.FooterRow.FindControl("lblgvFBalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ?
        //                         0 : dt.Compute("sum(balamt)", ""))).ToString("#,##0.00;(#,##0.00); ");



        //}
        protected void imgbtnFindPMlc_Click(object sender, EventArgs e)
        {
            this.GetMasterLc();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvcostvsex_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvcostvsex.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string LCname = this.ddlMLc.SelectedItem.Text.Trim().Substring(14);
            //ReportDocument rpcp = new RMGiRPT.R_21_GAcc.RptLCCostVsEx();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;
            //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //txtPrjName.Text = "LC Name: " + LCname;
            //TextObject rpttxtdate = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = "(As on " + Convert.ToDateTime(this.Request.QueryString["Date"].ToString()).ToString("dd-MMM-yyyy") + ")";
            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rpcp.SetDataSource((DataTable)Session["tblCostVsEx"]);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void gvcostvsex_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lblgvItemDesc");
                Label BudgetedAmtFc = (Label)e.Row.FindControl("lblgvBudgetedFC");

                //Label Preamt = (Label)e.Row.FindControl("lblgvPreamt");
                //Label Curamt = (Label)e.Row.FindControl("lblgvCuramt");
                Label Toamt = (Label)e.Row.FindControl("lblgvtoamt");
                Label BalAmt = (Label)e.Row.FindControl("lblgvBalAmt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    BudgetedAmtFc.Font.Bold = true;

                    //Preamt.Font.Bold = true;
                    //Curamt.Font.Bold = true;
                    Toamt.Font.Bold = true;
                    BalAmt.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");
                }

            }
        }
    }
}