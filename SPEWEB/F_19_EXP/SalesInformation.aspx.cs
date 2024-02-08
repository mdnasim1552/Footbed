using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using SPEENTITY.C_22_Sal;
using SPEENTITY;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_19_EXP
{
    public partial class SalesInformation : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        UserManSales objUserService = new UserManSales();
        ProcessAccess _DataEntry = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Export & Realization SMARTBOARD";
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                //((Label)this.Master.FindControl("lblANMgsBox")).Visible = false;

                //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
                ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
                this.lbtnOk_Click(null, null);
                //this.GetWeekly();
                //this.GetMonthly();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.RptSalesDeshbordPrint();
        }

        private void RptSalesDeshbordPrint()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //   string comnam = hst["comnam"].ToString();
            //   string compname = "";//hst["compname"].ToString();
            //   string comadd = hst["comadd1"].ToString();
            //   string username = hst["username"].ToString();
            //   string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //   string comcod = this.GetCompCode();
            //   string rptDt = " Date: : " + this.txtDate.Text.ToString();

            //   var lst = (List<SPEENTITY.C_22_Sal.EClassSales_02.EClassYear>)ViewState["tblYearly"];
            //   var lst1 = (List<SPEENTITY.C_22_Sal.EClassSales_02.EClassMonthly>)ViewState["tblMonthly"];
            //   var lst2 = (List<SPEENTITY.C_22_Sal.EClassSales_02.EClassWeekly>)ViewState["tblWeekSales"];
            //   var lst3 = (List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise>)ViewState["tblDayWise"];
            //   var lst4 = (List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWiseColl>)ViewState["tblDayWiseColl"];

            //   var lstsalescollection = new SPEENTITY.C_22_Sal.EClassSales_02.Rptsalescollection();
            //   lstsalescollection.LstEClassYear = lst;
            //   lstsalescollection.LstEClassMonthly = lst1;
            //   lstsalescollection.LstEClassWeekly = lst2;
            //   lstsalescollection.LstEClassDayWise = lst3;
            //   lstsalescollection.LstEClassDayWiseColl = lst4;

            //   LocalReport rpt1 = new LocalReport();
            //   rpt1 = RptSetupClass1.GetLocalReport("RD_23_SaM.RptSalesCollectionDashbord", lstsalescollection, null, null);
            //   rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //   rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //   rpt1.SetParameters(new ReportParameter("RptTitle", "SALES & COLLECTION DASHBOARD"));
            //   rpt1.SetParameters(new ReportParameter("rptdat", rptDt));
            //   rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            //   Session["Report1"] = rpt1;
            //   ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>"; 
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //this.lblYearGrp.Visible = true;
            this.lblYear.Visible = true;
            this.lblWeek.Visible = true;
            this.lblMon.Visible = true;
            //this.lblGrp.Visible = true;
            this.lblDetails.Visible = true;
            this.lblColl.Visible = true;
            //this.BarChart1.Visible = true;
            this.GetYearly();
            this.GetWeekly();
            this.GetMonthly();
            this.GetDayWise();
            this.GetDayWiseColl();
            this.GetCompareSalesYear();
            this.GetCompareSalesDaywise();
            this.GetCompareCollectYear();

            //this.GetCompareColDaywise();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteMyGraph();", true);

        }
        private void GetYearly()
        {
            try
            {
                string comcod = this.GetCompCode();

                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_22_Sal.EClassSales_02.EClassYear> lst = objUserService.ShowYearly(comcod, CurDate1);
                if (lst == null)
                    return;
                this.grvYearlySales.DataSource = lst;
                this.grvYearlySales.DataBind();

                ViewState["tblYearly"] = lst;
                this.BindChartYear();
                //((Label)this.grvYearlySales.FooterRow.FindControl("lblgvFinvAmt")).Text = lst.Select(p => p.samt).Sum().ToString("#,##0;(#,##0); ");
                //((Label)this.grvYearlySales.FooterRow.FindControl("lblgvFinvAmt")).Text = lst.Select(p => p.collamt).Sum().ToString("#,##0;(#,##0); ");
            }
            catch (Exception ex)
            {

            }
        }
        private void GetWeekly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_22_Sal.EClassSales_02.EClassWeekly> lst1 = objUserService.ShowWeekly(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvWeekSales.DataSource = lst1;
                this.grvWeekSales.DataBind();
                ViewState["tblWeekSales"] = lst1;

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1")).Text = (lst1.Select(p => p.wsamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt2")).Text = (lst1.Select(p => p.wsamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt3")).Text = (lst1.Select(p => p.wsamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt4")).Text = (lst1.Select(p => p.wsamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt4).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt1")).Text = (lst1.Select(p => p.wcamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt2")).Text = (lst1.Select(p => p.wcamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt3")).Text = (lst1.Select(p => p.wcamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt4")).Text = (lst1.Select(p => p.wcamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt4).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFbal1")).Text = (lst1.Select(p => p.wbal1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wbal1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFbal2")).Text = (lst1.Select(p => p.wbal2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wbal2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFbal3")).Text = (lst1.Select(p => p.wbal3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wbal3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFbal4")).Text = (lst1.Select(p => p.wbal4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wbal4).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt2T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt3T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2 + p.wsamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt4T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2 + p.wsamt3 + p.wsamt4)).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt1T")).Text = lst1.Select(p => p.wcamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt2T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt3T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2 + p.wcamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt4T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2 + p.wcamt3 + p.wcamt4)).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFbal2T")).Text = lst1.Select(p => (p.wbal1 + p.wbal2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFbal3T")).Text = lst1.Select(p => (p.wbal1 + p.wbal2 + p.wbal3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFbal4T")).Text = lst1.Select(p => (p.wbal1 + p.wbal2 + p.wbal3 + p.wbal4)).Sum().ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {

            }
        }
        private void GetMonthly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_22_Sal.EClassSales_02.EClassMonthly> lst1 = objUserService.ShowMonthly(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvMonthlySales.DataSource = lst1;
                this.grvMonthlySales.DataBind();
                ViewState["tblMonthly"] = lst1;
                this.BindChart();

                ((Label)this.grvMonthlySales.FooterRow.FindControl("lblyFAmt")).Text = lst1.Select(p => p.ttlsalamt).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvMonthlySales.FooterRow.FindControl("lblyFCollamt")).Text = lst1.Select(p => p.collamt).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvMonthlySales.FooterRow.FindControl("lblmFbal")).Text = lst1.Select(p => p.bal).Sum().ToString("#,##0;(#,##0); ");
            }
            catch (Exception ex)
            {

            }
        }
        private void BindChartYear()
        {
            List<SPEENTITY.C_22_Sal.EClassSales_02.EClassYear> lst = (List<SPEENTITY.C_22_Sal.EClassSales_02.EClassYear>)ViewState["tblYearly"];

            if (lst.Count == 0)
                return;

            //int i = 0;
            //foreach (SPEENTITY.C_22_Sal.EClassSales_02.EClassYear c1 in lst)
            //{
            //    Chart2.Series[i].Points.Add(new DataPoint(0, (c1.samt / 10000000)));
            //    Chart2.Series[i].Points.Add(new DataPoint(1, (c1.collamt / 10000000)));
            //    i++;
            //}

            //Chart2.DataSource = lst;
            //Chart2.DataBind();



            this.yc1.Text = Convert.ToInt32(lst[0].collamt).ToString();
            this.ys1.Text = Convert.ToInt32(lst[0].samt).ToString();

            this.yc2.Text = ((lst.Count == 1) ? 0.00 : Convert.ToInt32(lst[1].collamt)).ToString();//.ToString("#,##0.00;(#,##0.00);");

            this.ys2.Text = ((lst.Count == 1) ? 0.00 : Convert.ToInt32(lst[1].samt)).ToString();//.ToString("#,##0.00;(#,##0.00);");                    

            this.xaxis1.Text = Convert.ToInt32(lst[1].yearid).ToString();

            this.xaxis0.Text = Convert.ToInt32(lst[0].yearid).ToString();




        }
        private void BindChart()
        {
            List<SPEENTITY.C_22_Sal.EClassSales_02.EClassMonthly> lst = (List<SPEENTITY.C_22_Sal.EClassSales_02.EClassMonthly>)ViewState["tblMonthly"];

            //Chart1.Series["Series1"].XValueMember = "yearmon1";
            //Chart1.Series["Series1"].YValueMembers = "ttlsalamt";
            //Chart1.Series["Series2"].XValueMember = "yearmon1";
            //Chart1.Series["Series2"].YValueMembers = "collamt";

            //Chart1.Series["Series1"].LegendText = "Sales";
            //Chart1.Series["Series2"].LegendText = "Collection";

            //Chart1.DataSource = lst;
            //Chart1.DataBind();



            this.c1.Text = Convert.ToDouble(lst[0].collamt).ToString();//.ToString("#,##0.00;(#,##0.00);");
            this.s1.Text = Convert.ToDouble(lst[0].ttlsalamt).ToString();//.ToString("#,##0.00;(#,##0.00);");
            this.b1.Text = Convert.ToDouble(lst[0].bal).ToString();//.ToString("#,##0.00;(#,##0.00);");


            this.c2.Text = Convert.ToDouble(lst[1].collamt).ToString();
            this.s2.Text = Convert.ToDouble(lst[1].ttlsalamt).ToString();
            this.b2.Text = Convert.ToDouble(lst[1].bal).ToString();

            this.c3.Text = Convert.ToDouble(lst[2].collamt).ToString();
            this.s3.Text = Convert.ToDouble(lst[2].ttlsalamt).ToString();
            this.b3.Text = Convert.ToDouble(lst[2].bal).ToString();

            this.c4.Text = Convert.ToDouble(lst[3].collamt).ToString();
            this.s4.Text = Convert.ToDouble(lst[3].ttlsalamt).ToString();
            this.b4.Text = Convert.ToDouble(lst[3].bal).ToString();

            this.c5.Text = Convert.ToDouble(lst[4].collamt).ToString();
            this.s5.Text = Convert.ToDouble(lst[4].ttlsalamt).ToString();
            this.b5.Text = Convert.ToDouble(lst[4].bal).ToString();

            this.c6.Text = Convert.ToDouble(lst[5].collamt).ToString();
            this.s6.Text = Convert.ToDouble(lst[5].ttlsalamt).ToString();
            this.b6.Text = Convert.ToDouble(lst[5].bal).ToString();

            this.c7.Text = Convert.ToDouble(lst[6].collamt).ToString();
            this.s7.Text = Convert.ToDouble(lst[6].ttlsalamt).ToString();
            this.b7.Text = Convert.ToDouble(lst[6].bal).ToString();

            this.c8.Text = Convert.ToDouble(lst[7].collamt).ToString();
            this.s8.Text = Convert.ToDouble(lst[7].ttlsalamt).ToString();
            this.b8.Text = Convert.ToDouble(lst[7].bal).ToString();

            this.c9.Text = Convert.ToDouble(lst[8].collamt).ToString();
            this.s9.Text = Convert.ToDouble(lst[8].ttlsalamt).ToString();
            this.b9.Text = Convert.ToDouble(lst[8].bal).ToString();

            this.c10.Text = Convert.ToDouble(lst[9].collamt).ToString();
            this.s10.Text = Convert.ToDouble(lst[9].ttlsalamt).ToString();
            this.b10.Text = Convert.ToDouble(lst[9].bal).ToString();

            this.c11.Text = Convert.ToDouble(lst[10].collamt).ToString();
            this.s11.Text = Convert.ToDouble(lst[10].ttlsalamt).ToString();
            this.b11.Text = Convert.ToDouble(lst[10].bal).ToString();

            this.c12.Text = Convert.ToDouble(lst[11].collamt).ToString();
            this.s12.Text = Convert.ToDouble(lst[11].ttlsalamt).ToString();
            this.b12.Text = Convert.ToDouble(lst[11].bal).ToString();





        }

        private void GetDayWise()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise> lst = objUserService.ShowDayWise(comcod, CurDate1);
                if (lst == null)
                    return;

                ViewState["tblDayWise"] = HiddenSameData(lst);
                this.GvDayWise.DataSource = lst;
                this.GvDayWise.DataBind();
                this.FooterCalculation();

            }
            catch (Exception ex)
            {

            }
        }
        private void GetDayWiseColl()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWiseColl> lst = objUserService.ShowDayWiseColl(comcod, CurDate1);
                if (lst == null)
                    return;

                ViewState["tblDayWiseColl"] = HiddenSameData2(lst);
                this.gvDayWiseColl.DataSource = lst;
                this.gvDayWiseColl.DataBind();

                if (lst.Count < 0)
                    return;
                ((Label)this.gvDayWiseColl.FooterRow.FindControl("lblFamount")).Text = lst.Select(p => p.amount).Sum().ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {

            }
        }
        private List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWiseColl> HiddenSameData2(List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWiseColl> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string centrid = "";
            foreach (SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWiseColl c1 in lst3)
            {
                if (i == 0)
                {
                    centrid = c1.centrid;
                    i++;
                    continue;

                }
                else if (c1.centrid == centrid)
                {
                    c1.centrdesc = "";
                }
                centrid = c1.centrid;

            }

            return lst3;

        }
        private List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise> HiddenSameData(List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string centrid = "";
            foreach (SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise c1 in lst3)
            {
                if (i == 0)
                {
                    centrid = c1.centrid;
                    i++;
                    continue;

                }
                else if (c1.centrid == centrid)
                {
                    c1.centrdesc = "";
                }
                centrid = c1.centrid;

            }

            return lst3;

        }
        private void FooterCalculation()
        {


            List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise> lst3 = (List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise>)ViewState["tblDayWise"];
            if (lst3.Count == 0)
                return;
            ((Label)this.GvDayWise.FooterRow.FindControl("lblFitmamt")).Text = Convert.ToDouble(lst3.Select(p => p.itmamt).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.GvDayWise.FooterRow.FindControl("lblFvat")).Text = Convert.ToDouble(lst3.Select(p => p.vat).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.GvDayWise.FooterRow.FindControl("lblFinvdis")).Text = Convert.ToDouble(lst3.Select(p => p.invdis).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.GvDayWise.FooterRow.FindControl("lblFnetamt")).Text = Convert.ToDouble(lst3.Select(p => p.netamt).Sum()).ToString("#,##0;(#,##0); ");

            ((Label)this.GvDayWise.FooterRow.FindControl("lblFcollamt")).Text = Convert.ToDouble(lst3.Select(p => p.collamt).Sum()).ToString("#,##0;(#,##0); ");

        }
        protected void grvYearlySales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{
            //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //    TableCell cell01 = new TableCell();
            //    cell01.Text = "A. YEARLY";
            //    cell01.HorizontalAlign = HorizontalAlign.Center;
            //    cell01.ColumnSpan = 4;

            //    gvrow.Cells.Add(cell01);
            //    grvYearlySales.Controls[0].Controls.AddAt(0, gvrow);
            //}

        }
        protected void grvMonthlySales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{
            //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //    TableCell cell01 = new TableCell();
            //    cell01.Text = "B. MONTHLY";
            //    cell01.HorizontalAlign = HorizontalAlign.Center;
            //    cell01.ColumnSpan = 4;

            //    gvrow.Cells.Add(cell01);
            //    grvMonthlySales.Controls[0].Controls.AddAt(0, gvrow);
            //}
        }
        protected void grvWeekSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "WEEK-1";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 4;


                TableCell cell02 = new TableCell();
                cell02.Text = "WEEK-2";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 4;


                TableCell cell03 = new TableCell();
                cell03.Text = "WEEK-3";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 4;


                TableCell cell04 = new TableCell();
                cell04.Text = "WEEK-4";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 4;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                grvWeekSales.Controls[0].Controls.AddAt(0, gvrow);
            }
        }

        protected void GvDayWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();

                HyperLink InvNo = (HyperLink)e.Row.FindControl("hypInvoNum");

                string memono1 = ((HyperLink)e.Row.FindControl("hypInvoNum")).Text.ToString();
                string Centrid = ((Label)e.Row.FindControl("lblcenterid")).Text.ToString();
                string Memono = ((Label)e.Row.FindControl("lblmemo")).Text.ToString();

                string Custcode = ((Label)e.Row.FindControl("lblcustid")).Text.ToString();
                string SDate = ((Label)e.Row.FindControl("lblmemodat")).Text.ToString();

                InvNo.Style.Add("color", "blue");
                //InvNo.NavigateUrl = "~/F_23_SaM/LinkInvoice.aspx?Type=InvDetails&comcod=" + comcod + "&Centid=" + Centrid + "&Memo=" + Memono + "&Custid=" + Custcode + "&Date=" + SDate;

            }
        }


        private void GetCompareSalesYear()
        {
            string comcod = this.GetCompCode();

            string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");

            this.lbltitel.Text = "Sales Growth";
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO", "SALEYEARMONTHWISE", CurDate1, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            this.grvCompareYear.DataSource = ds2;
            this.grvCompareYear.DataBind();

            if (ds2.Tables[0].Rows.Count == 0)
                return;
            double prevamt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(presamt)", "")) ?
                     0.00 : ds2.Tables[0].Compute("Sum(presamt)", "")));
            ((Label)this.grvCompareYear.FooterRow.FindControl("lblFPrevAmtcs")).Text = prevamt.ToString("#,##0;(#,##0);  ");


            double curamt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(cursal)", "")) ?
                   0.00 : ds2.Tables[0].Compute("Sum(cursal)", "")));
            ((Label)this.grvCompareYear.FooterRow.FindControl("lblypryCollamtcs")).Text = curamt.ToString("#,##0;(#,##0); - ");


            return;

        }

        private void GetCompareSalesDaywise()
        {
            string comcod = this.GetCompCode();

            string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");

            this.lblModSalDay.Text = "Sales Growth Customer Wise";
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO", "SALEYEARMONTHDAYWISE", CurDate1, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            this.gvCompDayWise.DataSource = ds2;
            this.gvCompDayWise.DataBind();
            if (ds2.Tables[0].Rows.Count == 0)
                return;

            double prevamt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(presamt)", "")) ?
                     0.00 : ds2.Tables[0].Compute("Sum(presamt)", "")));
            ((Label)this.gvCompDayWise.FooterRow.FindControl("lblFPrevAmtcs")).Text = prevamt.ToString("#,##0;(#,##0);  ");


            double curamt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(cursal)", "")) ?
                   0.00 : ds2.Tables[0].Compute("Sum(cursal)", "")));
            ((Label)this.gvCompDayWise.FooterRow.FindControl("lblypryCollamtcs")).Text = curamt.ToString("#,##0;(#,##0); - ");


            return;

        }
        private void GetCompareCollectYear()
        {
            string comcod = this.GetCompCode();

            string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");

            this.lbltitel2.Text = "Collection Growth";
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO", "COLLYEARMONTHWISE", CurDate1, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            this.grvCompareYearColl.DataSource = ds2;
            this.grvCompareYearColl.DataBind();
            if (ds2.Tables[0].Rows.Count == 0)
                return;

            double prevamt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(presamt)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(presamt)", "")));
            ((Label)this.grvCompareYearColl.FooterRow.FindControl("lblFPrevAmtcoll")).Text = (prevamt == 0.00) ? "0.00" : prevamt.ToString("#,##0;(#,##0);  ");


            double curamt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(cursal)", "")) ?
                   0.00 : ds2.Tables[0].Compute("Sum(cursal)", "")));
            ((Label)this.grvCompareYearColl.FooterRow.FindControl("lblypryCollamtcoll")).Text = curamt.ToString("#,##0;(#,##0); - ");



            return;
        }


    }
}