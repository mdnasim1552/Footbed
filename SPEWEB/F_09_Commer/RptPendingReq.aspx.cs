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
    public partial class RptPendingReq : System.Web.UI.Page
    {
        ProcessAccess ComData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                this.GetOrderName();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PANDING REQUSITION";


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
            DataSet ds1 = ComData.GetTransInfo(comcod, "SP_ENTRY_BACK2BACKLC", "MLCFORBBLCL", SrchTxt, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlOrderList.DataTextField = "actdesc1";
            this.ddlOrderList.DataValueField = "actcode";
            this.ddlOrderList.DataSource = ds1.Tables[0];
            this.ddlOrderList.DataBind();


        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tbPenReq");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = this.GetCompCode();
            string Orderno = this.ddlOrderList.SelectedValue.ToString();

            DataSet ds1 = ComData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPENDINGREQ", Orderno, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvPenReq.DataSource = null;
                this.gvPenReq.DataBind();
                return;

            }

            Session["tbPenReq"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string Orderno = dt1.Rows[0]["mlccod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mlccod"].ToString() == Orderno)
                {
                    Orderno = dt1.Rows[j]["mlccod"].ToString();
                    dt1.Rows[j]["mlcdesc"] = "";
                }

                else
                {
                    Orderno = dt1.Rows[j]["mlccod"].ToString();
                }

            }

            return dt1;
        }
        private void Data_Bind()
        {

            this.gvPenReq.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvPenReq.Columns[1].Visible = (this.ddlOrderList.SelectedValue.ToString() == "000000000000") ? true : false;
            this.gvPenReq.DataSource = (DataTable)Session["tbPenReq"];
            this.gvPenReq.DataBind();
            //this.FooterCalculation();
        }
        //private void FooterCalculation()
        //{


        //    DataTable dt = (DataTable)Session["tbPenReq"];
        //    if (dt.Rows.Count == 0)
        //        return;

        //    //((Label)this.gvPenReq.FooterRow.FindControl("lblFAmtFc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bblcamt)", "")) ?
        //    //                0 : dt.Compute("sum(bblcamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    //((Label)this.gvPenReq.FooterRow.FindControl("lblFAmtTk")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalamt)", "")) ?
        //    //                        0 : dt.Compute("sum(totalamt)", ""))).ToString("#,##0;(#,##0); ");


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

            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            ReportDocument Orderstatus = new RMGiRPT.R_09_Commer.RptPendingReq();
            TextObject rptCname = Orderstatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            //TextObject rpttxtHeaderTitle = Orderstatus.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = HeaderTitle;
            //TextObject txtFDate1 = Orderstatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;

            TextObject txtuserinfo = Orderstatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            Orderstatus.SetDataSource((DataTable)Session["tbPenReq"]);
            Session["Report1"] = Orderstatus;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void imgbtnFindOrder_Click(object sender, EventArgs e)
        {
            this.GetOrderName();
        }

        protected void gvPenReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPenReq.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}