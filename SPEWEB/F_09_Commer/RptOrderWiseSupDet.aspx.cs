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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_09_Commer
{
    public partial class RptOrderWiseSupDet : System.Web.UI.Page
    {
        ProcessAccess ComData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "ORDER WISE SUPPLY DETAILS";

                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = "01" + (this.txtFDate.Text.Trim().Substring(2));
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetOrderName();


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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetOrderName()
        {
            string SrchTxt = this.txtSrcOrder.Text.Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = ComData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GETORDER", SrchTxt, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlOrderList.DataTextField = "actdesc1";
            this.ddlOrderList.DataValueField = "actcode";
            this.ddlOrderList.DataSource = ds1.Tables[0];
            this.ddlOrderList.DataBind();


        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tbOrdWiseSupp");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMMM-yyyy");
            string Orderno = this.ddlOrderList.SelectedValue.ToString();

            DataSet ds1 = ComData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "RPTORWISESUPDET", fromdate, todate, Orderno, "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvOrderWiseSupdet.DataSource = null;
                this.gvOrderWiseSupdet.DataBind();
                return;

            }

            Session["tbOrdWiseSupp"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Jobno = dt1.Rows[0]["jobno"].ToString();
            string Orderno = dt1.Rows[0]["mlccod"].ToString();
            string ssircode = dt1.Rows[0]["ssircode"].ToString();
            string bblccod = dt1.Rows[0]["bblccod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["jobno"].ToString() == Jobno || dt1.Rows[j]["mlccod"].ToString() == Orderno)
                {
                    Jobno = dt1.Rows[j]["jobno"].ToString();
                    Orderno = dt1.Rows[j]["mlccod"].ToString();
                    dt1.Rows[j]["jobno"] = "";
                    dt1.Rows[j]["Orderno"] = "";
                    dt1.Rows[j]["lcval"] = 0.00;

                }

                else
                {
                    Jobno = dt1.Rows[j]["jobno"].ToString();
                    Orderno = dt1.Rows[j]["mlccod"].ToString();
                }
                ////////--------------/////////////

                if (dt1.Rows[j]["bblccod"].ToString() == bblccod)
                {
                    bblccod = dt1.Rows[j]["bblccod"].ToString();
                    dt1.Rows[j]["bblcdesc"] = "";


                }

                else
                {
                    bblccod = dt1.Rows[j]["bblccod"].ToString();
                }
                /////-------------------------/////////////
                if (dt1.Rows[j]["ssircode"].ToString() == ssircode)
                {
                    ssircode = dt1.Rows[j]["ssircode"].ToString();
                    dt1.Rows[j]["ssirdesc"] = "";


                }

                else
                {
                    ssircode = dt1.Rows[j]["ssircode"].ToString();
                }


            }

            return dt1;
        }
        private void Data_Bind()
        {

            this.gvOrderWiseSupdet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvOrderWiseSupdet.Columns[2].Visible = (this.ddlOrderList.SelectedValue.ToString() == "000000000000") ? true : false;
            this.gvOrderWiseSupdet.DataSource = (DataTable)Session["tbOrdWiseSupp"];
            this.gvOrderWiseSupdet.DataBind();
            //this.FooterCalculation();
        }
        //private void FooterCalculation()
        //{


        //    DataTable dt = (DataTable)Session["tbOrdWiseSupp"];
        //    if (dt.Rows.Count == 0)
        //        return;

        //    ((Label)this.gvOrderWiseSupdet.FooterRow.FindControl("lblFAmtFc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bblcamt)", "")) ?
        //                    0 : dt.Compute("sum(bblcamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((Label)this.gvOrderWiseSupdet.FooterRow.FindControl("lblFAmtTk")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalamt)", "")) ?
        //                            0 : dt.Compute("sum(totalamt)", ""))).ToString("#,##0;(#,##0); ");


        //}
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string HeaderTitle = "Order Wise Supply Details";
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            ReportDocument Orderstatus = new RMGiRPT.R_09_Commer.RptOrderstWiseSupDet();
            TextObject rptCname = Orderstatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rpttxtHeaderTitle = Orderstatus.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            rpttxtHeaderTitle.Text = HeaderTitle;
            TextObject txtFDate1 = Orderstatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "From " + fromdate + " To " + todate;

            TextObject txtuserinfo = Orderstatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            Orderstatus.SetDataSource((DataTable)Session["tbOrdWiseSupp"]);
            Session["Report1"] = Orderstatus;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void imgbtnFindOrder_Click(object sender, EventArgs e)
        {
            this.GetOrderName();
        }
        protected void gvOrderWiseSupdet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrderWiseSupdet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvOrderWiseSupdet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label matdesc = (Label)e.Row.FindControl("lgMatDesc");
                Label ToamtFc = (Label)e.Row.FindControl("lgvbblcamt");
                Label AmtTk = (Label)e.Row.FindControl("lgvbblcamtTk");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "bblccod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "ZZZZ")
                {

                    matdesc.Font.Bold = true;
                    ToamtFc.Font.Bold = true;
                    AmtTk.Font.Bold = true;
                    matdesc.Style.Add("text-align", "right");
                }

            }
        }
    }
}