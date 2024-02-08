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
using SPEENTITY.C_15_Pro;

namespace SPEWEB.F_15_Pro
{
    public partial class ProductionInfo : System.Web.UI.Page
    {
        BL_Production objUserService = new BL_Production();
        ProcessAccess _DataEntry = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "PRODUCTION TARGET & EXECUTION SMARTBOARD";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.lbtnOk_Click(null, null);
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

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
            this.GetDayWiseProd();
            this.GetDayWiseExe();
        }
        private void GetYearly()
        {
            try
            {
                string comcod = GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_15_Pro.BO_Production.EClassYear> lst = objUserService.ShowYearly(comcod, CurDate1);
                if (lst == null)
                    return;
                this.grvYearlyProd.DataSource = lst;
                this.grvYearlyProd.DataBind();

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
                string comcod = GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_15_Pro.BO_Production.EClassWeekly> lst1 = objUserService.ShowWeekly(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvWeekProd.DataSource = lst1;
                this.grvWeekProd.DataBind();

                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFAmt1")).Text = (lst1.Select(p => p.wsamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFAmt2")).Text = (lst1.Select(p => p.wsamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFAmt3")).Text = (lst1.Select(p => p.wsamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFAmt4")).Text = (lst1.Select(p => p.wsamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt4).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFCollamt1")).Text = (lst1.Select(p => p.wcamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFCollamt2")).Text = (lst1.Select(p => p.wcamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFCollamt3")).Text = (lst1.Select(p => p.wcamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFCollamt4")).Text = (lst1.Select(p => p.wcamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt4).Sum().ToString("#,##0;(#,##0); ");


                //((Label)this.grvWeekProd.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFAmt2T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFAmt3T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2 + p.wsamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFAmt4T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2 + p.wsamt3 + p.wsamt4)).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekProd.FooterRow.FindControl("lblyFCollamt1T")).Text = lst1.Select(p => p.wcamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFCollamt2T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFCollamt3T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2 + p.wcamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekProd.FooterRow.FindControl("lblyFCollamt4T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2 + p.wcamt3 + p.wcamt4)).Sum().ToString("#,##0;(#,##0); ");


            }
            catch (Exception ex)
            {

            }
        }
        private void GetMonthly()
        {
            try
            {
                string comcod = GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_15_Pro.BO_Production.EClassMonthly> lst1 = objUserService.ShowMonthly(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvMonthlyMonth.DataSource = lst1;
                this.grvMonthlyMonth.DataBind();
                ViewState["tblMonthly"] = lst1;

                ((Label)this.grvMonthlyMonth.FooterRow.FindControl("lblyFAmt")).Text = lst1.Select(p => p.bgdamt).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvMonthlyMonth.FooterRow.FindControl("lblyFCollamt")).Text = lst1.Select(p => p.proamt).Sum().ToString("#,##0;(#,##0); ");

                this.BindChart();
            }
            catch (Exception ex)
            {

            }
        }
        private void BindChartYear()
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EClassYear> lst = (List<SPEENTITY.C_15_Pro.BO_Production.EClassYear>)ViewState["tblYearly"];

            if (lst.Count == 0)
                return;

            int i = 0;
            foreach (SPEENTITY.C_15_Pro.BO_Production.EClassYear c1 in lst)
            {
                Chart2.Series[i].Points.Add(new DataPoint(0, (c1.bgdamt / 10000000)));
                Chart2.Series[i].Points.Add(new DataPoint(1, (c1.proamt / 10000000)));
                i++;
            }

            Chart2.DataSource = lst;
            Chart2.DataBind();


        }
        private void BindChart()
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EClassMonthly> lst = (List<SPEENTITY.C_15_Pro.BO_Production.EClassMonthly>)ViewState["tblMonthly"];

            //Chart1.Series["Series1"].XValueMember = "yearmon1";
            //Chart1.Series["Series1"].YValueMembers = "bgdamt";
            //Chart1.Series["Series2"].XValueMember = "yearmon1";
            //Chart1.Series["Series2"].YValueMembers = "proamt";

            //Chart1.Series["Series1"].LegendText = "Production Target";
            //Chart1.Series["Series2"].LegendText = "Actual Production";

            //Chart1.DataSource = lst;
            //Chart1.DataBind();




            this.c1.Text = Convert.ToDouble(lst[0].proamt).ToString();//.ToString("#,##0.00;(#,##0.00);");
            this.s1.Text = Convert.ToDouble(lst[0].bgdamt).ToString();//.ToString("#,##0.00;(#,##0.00);");

            this.c2.Text = Convert.ToDouble(lst[1].proamt).ToString();
            this.s2.Text = Convert.ToDouble(lst[1].bgdamt).ToString();

            this.c3.Text = Convert.ToDouble(lst[2].proamt).ToString();
            this.s3.Text = Convert.ToDouble(lst[2].bgdamt).ToString();

            this.c4.Text = Convert.ToDouble(lst[3].proamt).ToString();
            this.s4.Text = Convert.ToDouble(lst[3].bgdamt).ToString();

            this.c5.Text = Convert.ToDouble(lst[4].proamt).ToString();
            this.s5.Text = Convert.ToDouble(lst[4].bgdamt).ToString();

            this.c6.Text = Convert.ToDouble(lst[5].proamt).ToString();
            this.s6.Text = Convert.ToDouble(lst[5].bgdamt).ToString();

            this.c7.Text = Convert.ToDouble(lst[6].proamt).ToString();
            this.s7.Text = Convert.ToDouble(lst[6].bgdamt).ToString();

            this.c8.Text = Convert.ToDouble(lst[7].proamt).ToString();
            this.s8.Text = Convert.ToDouble(lst[7].bgdamt).ToString();

            this.c9.Text = Convert.ToDouble(lst[8].proamt).ToString();
            this.s9.Text = Convert.ToDouble(lst[8].bgdamt).ToString();

            this.c10.Text = Convert.ToDouble(lst[9].proamt).ToString();
            this.s10.Text = Convert.ToDouble(lst[9].bgdamt).ToString();

            this.c11.Text = Convert.ToDouble(lst[10].proamt).ToString();
            this.s11.Text = Convert.ToDouble(lst[10].bgdamt).ToString();

            this.c12.Text = Convert.ToDouble(lst[11].proamt).ToString();
            this.s12.Text = Convert.ToDouble(lst[11].bgdamt).ToString();

        }

        private void GetDayWiseProd()
        {
            try
            {
                string comcod = GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWise> lst = objUserService.ShowDayWise(comcod, CurDate1);
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
        private void GetDayWiseExe()
        {
            try
            {
                string comcod = GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWiseExe> lst = objUserService.ShowDayWiseExe(comcod, CurDate1);
                if (lst == null)
                    return;

                //ViewState["tblDayWiseColl"] = HiddenSameData2(lst);
                this.gvDayWiseExe.DataSource = HiddenSameData2(lst);
                this.gvDayWiseExe.DataBind();
                if (lst.Count < 0)
                    return;
                ((Label)this.gvDayWiseExe.FooterRow.FindControl("lblFproamt")).Text = lst.Select(p => p.proamt).Sum().ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {

            }
        }
        private List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWiseExe> HiddenSameData2(List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWiseExe> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string batchcode = "";
            foreach (SPEENTITY.C_15_Pro.BO_Production.EClassDayWiseExe c1 in lst3)
            {
                if (i == 0)
                {
                    batchcode = c1.batchcode;
                    i++;
                    continue;

                }
                else if (c1.batchcode == batchcode)
                {
                    c1.batchdesc = "";
                    c1.prodate = "";
                    c1.vounum1 = "";
                    c1.centrdesc = "";
                }
                batchcode = c1.batchcode;

            }

            return lst3;

        }
        private List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWise> HiddenSameData(List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWise> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string batchcode = "";
            foreach (SPEENTITY.C_15_Pro.BO_Production.EClassDayWise c1 in lst3)
            {
                if (i == 0)
                {
                    batchcode = c1.batchcode;
                    i++;
                    continue;

                }
                else if (c1.batchcode == batchcode)
                {
                    c1.batchdesc = "";
                    c1.pbdate = "";
                    c1.preqno = "";
                }
                batchcode = c1.batchcode;

            }

            return lst3;

        }
        private void FooterCalculation()
        {


            List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWise> lst3 = (List<SPEENTITY.C_15_Pro.BO_Production.EClassDayWise>)ViewState["tblDayWise"];
            if (lst3.Count == 0)
                return;

            ((Label)this.GvDayWise.FooterRow.FindControl("lblFpreqamt")).Text = Convert.ToDouble(lst3.Select(p => p.preqamt).Sum()).ToString("#,##0;(#,##0); ");

        }

        protected void grvWeekProd_RowDataBound(object sender, GridViewRowEventArgs e)
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
                cell02.ColumnSpan = 3;


                TableCell cell03 = new TableCell();
                cell03.Text = "WEEK-3";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 3;


                TableCell cell04 = new TableCell();
                cell04.Text = "WEEK-4";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 3;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                grvWeekProd.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}