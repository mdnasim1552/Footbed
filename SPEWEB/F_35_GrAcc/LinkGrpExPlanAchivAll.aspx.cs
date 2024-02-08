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

namespace SPEWEB.F_35_GrAcc
{
    public partial class LinkGrpExPlanAchivAll : System.Web.UI.Page
    {
        ProcessAccess Data = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.lblDateRange.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                this.LoadDetailsData();

            }
        }





        private void RptExPlnVsAchiv()
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.LoadDetailsData();

        }
        private string GetCompCode()
        {
            return (this.Request.QueryString["comcod"].ToString());
        }


        private void LoadDetailsData()
        {
            Session.Remove("tbExPlan");
            string comcod = this.GetCompCode();
            string fdate = this.Request.QueryString["Date1"].ToString(); ;
            string tdate = this.Request.QueryString["Date2"].ToString();

            DataSet ds1 = Data.GetTransInfo(comcod, "SP_REPORT_EXPORT_PLAN", "RPTEXPLNVSACHIVALL", fdate, tdate, "", "", "", "", "", "", "");


            if (ds1.Tables[0] == null)
            {
                this.gvRptExPlnAch.DataSource = null;
                this.gvRptExPlnAch.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tbExPlan"] = dt1;

            this.LoadGrid();
            //this.FooterCalculation();

        }
        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tbExPlan"];

            this.gvRptExPlnAch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptExPlnAch.DataSource = dt;
            this.gvRptExPlnAch.DataBind();


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
                    dt1.Rows[j]["orderdes"] = "";
                }

                else
                {
                    mlccod = dt1.Rows[j]["mlccod"].ToString();
                }

            }

            return dt1;
        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            this.PrintExPlnAchiv();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Export Plan Vs Achivement";
                string eventdesc = "Print Report: ";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }





        private void PrintExPlnAchiv()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            //DataTable dt1 = (DataTable)Session["tbExPlan"];
            ////DataTable dt2 = (DataTable)Session["tbExPlanDat"];txtOrder
            //ReportDocument rrs1 = new RMGiRPT.R_03_CostABgd.RptExPlnAchivAll();
            //TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtTitle.Text = "From : " + this.txtFdate.Text + " To : " + this.txtTdate.Text;
            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource(dt1);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rrs1.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rrs1;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.LoadGrid();


        }



        protected void gvRptExPlnAch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRptExPlnAch.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void gvRptExPlnAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Date = (Label)e.Row.FindControl("lgvOrdDat");
                Label ShipPQty = (Label)e.Row.FindControl("lgvShiPlqty");
                Label ProPlanqty = (Label)e.Row.FindControl("lgvProPlqty");
                Label ProQty = (Label)e.Row.FindControl("lgvProqty");
                Label ShipQty = (Label)e.Row.FindControl("lgvShiQty");
                Label Proper = (Label)e.Row.FindControl("lgvProPer");
                Label ShiPer = (Label)e.Row.FindControl("lgvShiPer");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "plandate")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "Total" || code == "Grand Total")
                {

                    Date.Font.Bold = true;
                    ShipPQty.Font.Bold = true;
                    ProPlanqty.Font.Bold = true;
                    ProQty.Font.Bold = true;
                    ShipQty.Font.Bold = true;
                    Proper.Font.Bold = true;
                    ShiPer.Font.Bold = true;
                    Date.Style.Add("text-align", "right");
                }

            }
        }
    }
}