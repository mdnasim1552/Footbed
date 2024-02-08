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

namespace SPEWEB.F_21_GAcc
{
    public partial class IncomeReduced : System.Web.UI.Page
    {
        ProcessAccess rptdata = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                //this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetMasterLc();

            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


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
            Session.Remove("tbVarAnaly");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = GetComCode();
            string mlccode = this.ddlMLc.SelectedValue.ToString();
            //string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = rptdata.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LCCVSEXPENSES", "LCCOSTVSEXPREDUCED", mlccode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvcostReduced.DataSource = null;
                this.gvcostReduced.DataBind();
                return;
            }
            Session["tbVarAnaly"] = HiddenSameData(ds2.Tables[0]);
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
            this.gvcostReduced.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvcostReduced.DataSource = (DataTable)Session["tbVarAnaly"];
            this.gvcostReduced.DataBind();
            //this.FooterCal();


        }

        private void FooterCal()
        {
            //DataTable dt = (DataTable)Session["tbVarAnaly"];
            //if (dt.Rows.Count == 0)
            //    return;

            //((Label)this.gvcostReduced.FooterRow.FindControl("lblFBgdQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdqty)", "")) ?
            //                          0 : dt.Compute("sum(bgdqty)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvcostReduced.FooterRow.FindControl("lblFActdQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(actqty)", "")) ?
            //                          0 : dt.Compute("sum(actqty)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvcostReduced.FooterRow.FindControl("lblFBOverQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(overqty)", "")) ?
            //                     0 : dt.Compute("sum(overqty)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvcostReduced.FooterRow.FindControl("lblFQtyVar")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qtyvar)", "")) ?
            //                     0 : dt.Compute("sum(qtyvar)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvcostReduced.FooterRow.FindControl("lblFTotalFc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalamt)", "")) ?
            //                     0 : dt.Compute("sum(totalamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvcostReduced.FooterRow.FindControl("lblFTotalTk")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalamtTK)", "")) ?
            //                     0 : dt.Compute("sum(totalamtTK)", ""))).ToString("#,##0;(#,##0); ");



        }
        protected void imgbtnFindPMlc_Click(object sender, ImageClickEventArgs e)
        {
            this.GetMasterLc();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            //ReportDocument rpcp = new RMGiRPT.R_21_GAcc.RptLCVarAnalysis();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;
            //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //txtPrjName.Text = "LC Name: " + LCname;
            ////TextObject rpttxtdate = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            ////rpttxtdate.Text = "(From " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy") + ")";
            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rpcp.SetDataSource((DataTable)Session["tbVarAnaly"]);
            //Session["Report1"] = rpcp;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvcostReduced_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvcostReduced.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvcostReduced_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label groupdesc = (Label)e.Row.FindControl("lblgvItemDesc");
                Label BgdQty = (Label)e.Row.FindControl("lblgvBudgetedQty");
                Label actQty = (Label)e.Row.FindControl("lblgvActQty");
                Label QtyVar = (Label)e.Row.FindControl("lblgvQtyV");
                Label RateVar = (Label)e.Row.FindControl("lblgvRatVar");
                Label tAmtFc = (Label)e.Row.FindControl("lblgvTotal");
                Label tamtTk = (Label)e.Row.FindControl("lblgvTotalTk");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    groupdesc.Font.Bold = true;
                    BgdQty.Font.Bold = true;
                    actQty.Font.Bold = true;
                    QtyVar.Font.Bold = true;
                    RateVar.Font.Bold = true;

                    tAmtFc.Font.Bold = true;
                    tamtTk.Font.Bold = true;
                    groupdesc.Style.Add("text-align", "right");
                }

            }
        }
    }

}