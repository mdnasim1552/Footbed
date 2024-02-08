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

namespace SPEWEB.F_05_ProShip
{
    public partial class RptExPlanAchivAll : System.Web.UI.Page
    {
        ProcessAccess Data = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "EXPORT PLAN VS ACHIVEMENT";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.lnkbtnOk_Click(null, null);

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }




        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            this.RptExPlnVsAchiv();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Export Plan Vs Achivement";
                string eventdesc = "Show Report: ";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }


        private void LoadDetailsData()
        {
            Session.Remove("tbExPlan");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = Data.GetTransInfo(comcod, "SP_REPORT_EXPORT_PLAN", "RPTEXPLNVSACHIVALL", date, "", "", "", "", "", "", "", "");


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
            this.FooterCalCulation();


        }
        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)Session["tbExPlan"];
            if (dt.Rows.Count == 0) return;

            double proplan, protar, proach, shipplan, shipach, peronpropln, peronshippln;

            proplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(proplanqty)", "")) ? 0.00 : dt.Compute("Sum(proplanqty)", "")));
            protar = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tqty)", "")) ? 0.00 : dt.Compute("Sum(tqty)", "")));
            proach = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(propqty)", "")) ? 0.00 : dt.Compute("Sum(propqty)", "")));
            shipplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(shipplnqty)", "")) ? 0.00 : dt.Compute("Sum(shipplnqty)", "")));
            shipach = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(shipqty)", "")) ? 0.00 : dt.Compute("Sum(shipqty)", "")));
            peronpropln = (proplan == 0) ? 0.00 : (proach * 100) / proplan;
            peronshippln = (shipplan == 0) ? 0.00 : (shipach * 100) / shipplan;
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFProPlnQty")).Text = proplan.ToString("#,##0;(#,##0);  ");
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFworktarget")).Text = protar.ToString("#,##0;(#,##0);  ");
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFproachieved")).Text = proach.ToString("#,##0;(#,##0);  ");
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFShipPlan")).Text = shipplan.ToString("#,##0;(#,##0);  ");
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFShipAchieved")).Text = shipach.ToString("#,##0;(#,##0);  ");
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFPeroProPlan")).Text = peronpropln.ToString("#,##0.00;(#,##0.00); ") + "%";
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFPeroShipPlan")).Text = peronshippln.ToString("#,##0.00;(#,##0.00);  ") + "%";



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
                    dt1.Rows[j]["delday"] = 0;
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

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            DataTable dt1 = (DataTable)Session["tbExPlan"];
            //DataTable dt2 = (DataTable)Session["tbExPlanDat"];txtOrder
            ReportDocument rrs1 = new RMGiRPT.R_03_CostABgd.RptExPlnAchivAll();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtTitle.Text = "As On : " + this.txtdate.Text;

            TextObject txtperonproplan = rrs1.ReportDefinition.ReportObjects["txtperonproplan"] as TextObject;
            txtperonproplan.Text = ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFPeroProPlan")).Text;

            TextObject txtperonshipplan = rrs1.ReportDefinition.ReportObjects["txtperonshipplan"] as TextObject;
            txtperonshipplan.Text = ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFPeroShipPlan")).Text;


            //txtperonshipplan.Text = peronpropln.ToString("#,##0.00;(#,##0.00); ") + "%";

            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rrs1.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rrs1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
        protected void gvRptExPlnAch_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink LcName = (HyperLink)e.Row.FindControl("hlnkgvlcname");
                Label delday = (Label)e.Row.FindControl("lgvdelday");
                string mlccod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccod")).ToString();
                if (mlccod == "")
                {
                    return;
                }
                string mlcdesc = ((HyperLink)e.Row.FindControl("hlnkgvlcname")).Text.Trim();
                LcName.Style.Add("color", "blue");
                LcName.NavigateUrl = "~/F_05_ProShip/LinkRptExPlVsAchivSumm.aspx?mlccod=" + mlccod + "&mlcdesc=" + mlcdesc + "&Date=" + Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");





            }
        }
    }
}