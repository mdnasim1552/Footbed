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
    public partial class RptExPlVsAchivSumm : System.Web.UI.Page
    {
        ProcessAccess Data = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "EXPORT PLAN VS ACHIVEMENT";


                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.GetOrderName();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private void GetOrderName()
        {
            string SrchTxt = this.txtSrcOrder.Text.Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = Data.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", "GETORDER", SrchTxt, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlOrderList.DataTextField = "actdesc1";
            this.ddlOrderList.DataValueField = "actcode";
            this.ddlOrderList.DataSource = ds1.Tables[0];
            this.ddlOrderList.DataBind();


        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {


            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.PanelExPlan.Visible = true;
            this.lblOrderDetails.Visible = true;
            this.lblpbeforeproduction.Visible = true;
            this.lblacbeforepro.Visible = true;
            this.lblpvsacastoday.Visible = true;
            this.lblpvsac.Visible = true;
            this.lbldocumentation.Visible = true;
            this.lblExportRealization.Visible = true;
            this.lblBBlCduestatus.Visible = true;
            this.lblIncomeSt.Visible = true;
            this.LoadDetailsData();
        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void LoadDetailsData()
        {
            Session.Remove("tbExPlan");
            string comcod = this.GetCompCode();
            string ordercode = this.ddlOrderList.SelectedValue.ToString();
            string date = this.txtDate.Text.Trim();
            DataSet ds1 = Data.GetTransInfo(comcod, "SP_REPORT_EXPORT_PLAN", "RPTEXPLNVSACHIVSUM", ordercode, date, "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvOrDer.DataSource = null;
                this.gvOrDer.DataBind();
                this.gvLcpjInfo.DataSource = null;
                this.gvLcpjInfo.DataBind();
                this.gvachieved.DataSource = null;
                this.gvachieved.DataBind();
                this.gvexpastoday.DataSource = null;
                this.gvexpastoday.DataBind();
                this.gvRptExPlnAch.DataSource = null;
                this.gvRptExPlnAch.DataBind();

                this.gvExport.DataSource = null;
                this.gvExport.DataBind();
                this.gvshipvsrlz.DataSource = null;
                this.gvshipvsrlz.DataBind();
                this.gvduebblc.DataSource = null;
                this.gvduebblc.DataBind();
                this.gvinstment.DataSource = null;
                this.gvinstment.DataBind();

                return;

            }


            if (ds1.Tables[5].Rows.Count > 0)
            {
                this.txtCurStartDate.Text = Convert.ToDateTime(ds1.Tables[9].Rows[0]["startdate"]).ToString("dd-MMM-yyyy");
                this.txtCurEndDate.Text = Convert.ToDateTime(ds1.Tables[9].Rows[0]["enddate"]).ToString("dd-MMM-yyyy");
            }

            Session["tbExPlan"] = ds1;
            this.LoadGrid();
            ds1.Dispose();


        }
        private void LoadGrid()
        {


            DataSet ds1 = ((DataSet)Session["tbExPlan"]).Copy();

            // Order
            this.gvOrDer.DataSource = ds1.Tables[0];
            this.gvOrDer.DataBind();
            this.FooterCalculation("gv_OrDer", ds1.Tables[0]);


            // Planning Before Production
            this.gvLcpjInfo.DataSource = ds1.Tables[1];
            this.gvLcpjInfo.DataBind();
            this.FooterCalculation("gv_LcpjInfo", ds1.Tables[1]);

            // Achieved Before Production
            this.gvachieved.DataSource = this.HiddenSameData(ds1.Tables[2], "gv_achieved");
            this.gvachieved.DataBind();
            this.FooterCalculation("gv_achieved", ds1.Tables[2]);

            // Plan Vs Achievement As of today
            this.gvexpastoday.DataSource = ds1.Tables[3];

            this.gvexpastoday.DataBind();
            this.FooterCalculation("gv_expastoday", ds1.Tables[3]);

            //  Plan Vs Achievement As of today

            this.gvRptExPlnAch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptExPlnAch.DataSource = this.HiddenSameData(ds1.Tables[4], "gv_RptExPlnAch");
            this.gvRptExPlnAch.DataBind();

            //  Doocumentation
            this.gvExport.DataSource = ds1.Tables[5];
            this.gvExport.DataBind();

            //  Realization
            this.gvshipvsrlz.DataSource = ds1.Tables[6];
            this.gvshipvsrlz.DataBind();
            this.FooterCalculation("gv_shipvsrlz", ds1.Tables[6]);

            // BBLC Due Status
            this.gvduebblc.DataSource = ds1.Tables[7];
            this.gvduebblc.DataBind();
            this.FooterCalculation("gv_duebblc", ds1.Tables[7]);
            // Income Statement
            this.gvinstment.DataSource = this.HiddenSameData(ds1.Tables[8], "gv_instment");
            this.gvinstment.DataBind();

        }



        private void FooterCalculation(string GrView, DataTable dt)
        {


            if (dt.Rows.Count == 0)
                return;

            switch (GrView)
            {

                case "gv_OrDer":

                    ((Label)this.gvOrDer.FooterRow.FindControl("lgvFordrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordrqty)", "")) ?
                        0.00 : dt.Compute("Sum(ordrqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvOrDer.FooterRow.FindControl("lgvFordramtord")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordramt)", "")) ?
                        0.00 : dt.Compute("Sum(ordramt)", ""))).ToString("#,##0;(#,##0);  ");







                    break;


                case "gv_LcpjInfo":


                    DateTime plnidate, plnenddate, achidate, achenddate;

                    plnidate = Convert.ToDateTime(dt.Rows[0]["plndate"]);
                    plnenddate = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["plndate"]);
                    achidate = Convert.ToDateTime(dt.Rows[0]["achivedate"]);
                    achenddate = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["achivedate"]);
                    ((Label)this.gvLcpjInfo.FooterRow.FindControl("lgvFtoplntime")).Text = ASTUtility.Datediffday(plnenddate, plnidate).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvLcpjInfo.FooterRow.FindControl("lgvFtoadchtime")).Text = ASTUtility.Datediffday(achenddate, achidate).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvLcpjInfo.FooterRow.FindControl("lgvFdelday")).Text = Math.Abs(ASTUtility.Datediffday(achenddate, achidate) - ASTUtility.Datediffday(plnenddate, plnidate)).ToString("#,##0;(#,##0);  ");


                    break;
                case "gv_expastoday":

                    double proplan, protar, proach, shipplan, shipach, peronpropln, peronshippln;

                    proplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(proplanqty)", "")) ? 0.00 : dt.Compute("Sum(proplanqty)", "")));
                    protar = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tqty)", "")) ? 0.00 : dt.Compute("Sum(tqty)", "")));
                    proach = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(propqty)", "")) ? 0.00 : dt.Compute("Sum(propqty)", "")));
                    shipplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(shipplnqty)", "")) ? 0.00 : dt.Compute("Sum(shipplnqty)", "")));
                    shipach = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(shipqty)", "")) ? 0.00 : dt.Compute("Sum(shipqty)", "")));
                    peronpropln = (proplan == 0) ? 0.00 : (proach * 100) / proplan;
                    peronshippln = (shipplan == 0) ? 0.00 : (shipach * 100) / shipplan;
                    ((Label)this.gvexpastoday.FooterRow.FindControl("lgvFProPlnQty")).Text = proplan.ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvexpastoday.FooterRow.FindControl("lgvFworktarget")).Text = protar.ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvexpastoday.FooterRow.FindControl("lgvFproachieved")).Text = proach.ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvexpastoday.FooterRow.FindControl("lgvFShipPlan")).Text = shipplan.ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvexpastoday.FooterRow.FindControl("lgvFShipAchieved")).Text = shipach.ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvexpastoday.FooterRow.FindControl("lgvFPeroProPlan")).Text = peronpropln.ToString("#,##0.00;(#,##0.00); ") + "%";
                    ((Label)this.gvexpastoday.FooterRow.FindControl("lgvFPeroShipPlan")).Text = peronshippln.ToString("#,##0.00;(#,##0.00);  ") + "%";



                    break;
                case "gv_achieved":
                    ((Label)this.gvachieved.FooterRow.FindControl("lgvFordramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordramt)", "")) ? 0.00 : dt.Compute("Sum(ordramt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvachieved.FooterRow.FindControl("lgvFrcvamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(mrramt)", "")) ? 0.00 : dt.Compute("Sum(mrramt)", ""))).ToString("#,##0;(#,##0);  ");
                    break;


                case "gv_shipvsrlz":


                    double shipamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(shipamt)", "")) ? 0.00 : dt.Compute("Sum(shipamt)", "")));
                    double rlzamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(rlzamt)", "")) ? 0.00 : dt.Compute("Sum(rlzamt)", "")));
                    double varamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(varamt)", "")) ? 0.00 : dt.Compute("Sum(varamt)", "")));
                    double balamt = shipamt - rlzamt;
                    ((Label)this.gvshipvsrlz.FooterRow.FindControl("lgvFexportamt")).Text = shipamt.ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvshipvsrlz.FooterRow.FindControl("lgvFrlzamt")).Text = rlzamt.ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvshipvsrlz.FooterRow.FindControl("lgvFBalamt")).Text = balamt.ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvshipvsrlz.FooterRow.FindControl("lgvFvaramt")).Text = varamt.ToString("#,##0;(#,##0);  ");
                    break;
                case "gv_duebblc":
                    ((Label)this.gvduebblc.FooterRow.FindControl("lgvFbillamtdbblc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(billamt)", "")) ?
                        0.00 : dt.Compute("Sum(billamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvduebblc.FooterRow.FindControl("lgvFdueamtdbblc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam)", "")) ?
                       0.00 : dt.Compute("Sum(dueam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvduebblc.FooterRow.FindControl("lgvFnydueamtdbblc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(nydueamt)", "")) ?
                       0.00 : dt.Compute("Sum(nydueamt)", ""))).ToString("#,##0;(#,##0);  ");

                    break;






            }


        }
        private DataTable HiddenSameData(DataTable dt1, string GrView)
        {

            if (dt1.Rows.Count == 0)
                return dt1;


            switch (GrView)
            {



                case "gv_RptExPlnAch":


                    string shipno = dt1.Rows[0]["shipmentno"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["shipmentno"].ToString() == shipno)
                        {
                            shipno = dt1.Rows[j]["shipmentno"].ToString();
                            dt1.Rows[j]["shipmentno"] = "";
                        }

                        else
                        {
                            shipno = dt1.Rows[j]["shipmentno"].ToString();
                        }

                    }


                    break;
                case "gv_achieved":

                    string bblccode = dt1.Rows[0]["bblccode"].ToString();
                    string orderno = dt1.Rows[0]["orderno"].ToString();
                    string mrrno = dt1.Rows[0]["mrrno"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["bblccode"].ToString() == bblccode && dt1.Rows[j]["orderno"].ToString() == orderno && dt1.Rows[j]["mrrno"].ToString() == mrrno)
                        {

                            dt1.Rows[j]["bblcdesc"] = "";
                            dt1.Rows[j]["orderdat1"] = "";
                            dt1.Rows[j]["mrrdat"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["bblccode"].ToString() == bblccode)
                                dt1.Rows[j]["bblcdesc"] = "";

                            if (dt1.Rows[j]["orderno"].ToString() == orderno)
                                dt1.Rows[j]["orderdat1"] = "";
                            if (dt1.Rows[j]["mrrno"].ToString() == mrrno)
                                dt1.Rows[j]["mrrdat"] = "";



                        }

                        bblccode = dt1.Rows[j]["bblccode"].ToString();
                        orderno = dt1.Rows[j]["orderno"].ToString();
                        mrrno = dt1.Rows[j]["mrrno"].ToString();
                    }

                    break;

                case "gv_instment":
                    string mgrp = dt1.Rows[0]["mgrp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["mgrp"].ToString() == mgrp)
                            dt1.Rows[j]["mgrpdesc"] = "";

                        mgrp = dt1.Rows[j]["mgrp"].ToString();

                    }
                    break;




            }




            return dt1;
        }

        private void FooterCalculation()
        {


            DataTable dt = (DataTable)Session["tbExPlan"];
            if (dt.Rows.Count == 0)
                return;

            double shPlnQty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shipplnqty)", "")) ?
                               0 : dt.Compute("sum(shipplnqty)", "")));
            double proQty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(propqty)", "")) ?
                            0 : dt.Compute("sum(propqty)", "")));
            double shiQty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shipqty)", "")) ?
                            0 : dt.Compute("sum(shipqty)", "")));
            double proPar = ((proQty * 100) / shPlnQty);
            double shiPar = ((shiQty * 100) / shPlnQty);

            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFShiPlqty")).Text = shPlnQty.ToString("#,##0;(#,##0); ");
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFProqty")).Text = proQty.ToString("#,##0;(#,##0); ");
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFShiQty")).Text = shiQty.ToString("#,##0;(#,##0); ");

            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFShiPlPar")).Text = "100%";
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFProPar")).Text = proPar.ToString("#,##0.00;(#,##0.00); ") + "%";
            ((Label)this.gvRptExPlnAch.FooterRow.FindControl("lgvFShiPar")).Text = shiPar.ToString("#,##0.00;(#,##0.00); ") + "%";


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

            string satartdate = this.txtCurStartDate.Text;
            string enddate = this.txtCurEndDate.Text;
            DataSet ds1 = ((DataSet)Session["tbExPlan"]).Copy();
            //DataTable dt2 = (DataTable)Session["tbExPlanDat"];txtOrder
            ReportDocument rrs1 = new RMGiRPT.R_03_CostABgd.RptExPlnAchiv();


            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;

            TextObject rptOrder = rrs1.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
            rptOrder.Text = this.ddlOrderList.SelectedItem.Text.Substring(14).ToString();

            TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "Starting Date: " + satartdate + " Ending Date: " + enddate;

            //TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Work Order Status( " + basis + " )";
            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);






            // rrs1.SetDataSource(ds1.Tables[4]);

            rrs1.Subreports["rptOrderDetails.rpt"].SetDataSource(ds1.Tables[0]);
            rrs1.Subreports["rptPlaningbeforePro.rpt"].SetDataSource(ds1.Tables[1]);
            rrs1.Subreports["rptAchievementbeforePro.rpt"].SetDataSource(this.HiddenSameData(ds1.Tables[2], "gv_achieved")); ;
            rrs1.Subreports["rptProductionSummary.rpt"].SetDataSource(ds1.Tables[3]);

            rrs1.Subreports["RptProductionShipDetails.rpt"].SetDataSource(ds1.Tables[4]);



            //rrs1.Subreports["rptExportDocumentation.rpt"].SetDataSource(ds1.Tables[5]);
            rrs1.Subreports["rptExportRealization.rpt"].SetDataSource(ds1.Tables[6]);

            rrs1.Subreports["rptDueBBLC.rpt"].SetDataSource(ds1.Tables[7]);
            rrs1.Subreports["rptIncomeStatement.rpt"].SetDataSource(ds1.Tables[8]);


            rrs1.SetDataSource(ds1.Tables[5]);
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
        protected void imgbtnFindOrder_Click(object sender, EventArgs e)
        {
            this.GetOrderName();
        }

        protected void gvRptExPlnAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    Label ShipPQty = (Label)e.Row.FindControl("lgvShiPlqty");
            //    Label ProPlanqty = (Label)e.Row.FindControl("lgvProPlqty");
            //    Label ProQty = (Label)e.Row.FindControl("lgvProqty");
            //    Label ShipQty = (Label)e.Row.FindControl("lgvShiQty");
            //    Label Proper = (Label)e.Row.FindControl("lgvProPer");
            //    Label ShiPer = (Label)e.Row.FindControl("lgvShiPer");
            //    Label worktarget = (Label)e.Row.FindControl("lgvworktarget");

            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "plandate")).ToString();

            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (code == "Total" || code == "Grand Total")
            //    {


            //        ShipPQty.Font.Bold = true;
            //        ProPlanqty.Font.Bold = true;
            //        ProQty.Font.Bold = true;
            //        ShipQty.Font.Bold = true;
            //        Proper.Font.Bold = true;
            //        ShiPer.Font.Bold = true;
            //        worktarget.Font.Bold = true;

            //    }

            //}
        }

        protected void gvExport_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink ressdesc = (HyperLink)e.Row.FindControl("hlnkgvressdescexp");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }

                ressdesc.Style.Add("color", "blue");
                ressdesc.NavigateUrl = "~/F_31_Mis/LinkExportDocs.aspx?comcod=" + code;

            }


        }
        protected void gvinstment_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvOrDer_RowDataBound1(object sender, GridViewRowEventArgs e)
        {

        }
    }
}