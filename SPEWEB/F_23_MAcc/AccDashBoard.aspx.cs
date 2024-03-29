﻿using System;
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
using SPEENTITY.C_21_Acc;

namespace SPEWEB.F_23_MAcc
{
    public partial class AccDashBoard : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        UserDB_BL objUserService = new UserDB_BL();
        ProcessAccess _DataEntry = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "ACCOUNTS SMARTBOARD";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.lbtnOk_Click(null, null);

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
            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

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
            // this.lblGrp.Visible = true;
            //this.BarChart1.Visible = true;
            this.GetYearly();
            this.GetWeekly();
            this.GetMonthly();
            this.ShowDetails();
        }
        private void GetYearly()
        {
            try
            {
                string comcod = GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccYearly> lst = objUserService.ShowYearAcc(comcod, CurDate1);
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
                string comcod = GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccWeekly> lst1 = objUserService.ShowWeeklyAcc(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvWeekSales.DataSource = lst1;
                this.grvWeekSales.DataBind();
                if (lst1.Count == 0)
                    return;
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1")).Text = (lst1.Select(p => p.wpamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt2")).Text = (lst1.Select(p => p.wpamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt3")).Text = (lst1.Select(p => p.wpamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt4")).Text = (lst1.Select(p => p.wpamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt4).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt1")).Text = (lst1.Select(p => p.wramt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wramt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt2")).Text = (lst1.Select(p => p.wramt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wramt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt3")).Text = (lst1.Select(p => p.wramt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wramt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt4")).Text = (lst1.Select(p => p.wramt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wramt4).Sum().ToString("#,##0;(#,##0); ");


                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt2T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt3T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2 + p.wpamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt4T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2 + p.wpamt3 + p.wpamt4)).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblFbRec")).Text = (lst1[0].brec == 0.00) ? "0.00" : lst1[0].brec.ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt1T")).Text = lst1.Select(p => p.wcamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt2T")).Text = lst1.Select(p => (p.wramt1 + p.wramt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt3T")).Text = lst1.Select(p => (p.wramt1 + p.wramt2 + p.wramt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt4T")).Text = lst1.Select(p => (p.wramt1 + p.wramt2 + p.wramt3 + p.wramt4)).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblFbPay")).Text = (lst1[0].bpay == 0.00) ? "0.00" : lst1[0].bpay.ToString("#,##0;(#,##0); ");


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
                List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccMonthly> lst1 = objUserService.ShowMonthlyAcc(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvMonthlySales.DataSource = lst1;
                this.grvMonthlySales.DataBind();
                ViewState["tblMonthly"] = lst1;

                ((Label)this.grvMonthlySales.FooterRow.FindControl("lblyFAmt")).Text = lst1.Select(p => p.dram).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvMonthlySales.FooterRow.FindControl("lblyFCollamt")).Text = lst1.Select(p => p.cram).Sum().ToString("#,##0;(#,##0); ");

                this.BindChart();
            }
            catch (Exception ex)
            {

            }
        }
        private void BindChartYear()
        {
            List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccYearly> lst = (List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccYearly>)ViewState["tblYearly"];

            //int i = 0;
            //foreach (SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccYearly c1 in lst)
            //{
            //    Chart2.Series[i].Points.Add(new DataPoint(0, (c1.dram / 10000000)));
            //    Chart2.Series[i].Points.Add(new DataPoint(1, (c1.cram / 10000000)));
            //    i++;
            //}

            //Chart2.DataSource = lst;
            //Chart2.DataBind();


            this.yc1.Text = Convert.ToInt32(lst[0].dram).ToString();
            this.ys1.Text = Convert.ToInt32(lst[0].cram).ToString();

            this.yc2.Text = ((lst.Count == 1) ? 0.00 : Convert.ToInt32(lst[1].dram)).ToString();//.ToString("#,##0.00;(#,##0.00);");

            this.ys2.Text = ((lst.Count == 1) ? 0.00 : Convert.ToInt32(lst[1].cram)).ToString();//.ToString("#,##0.00;(#,##0.00);");                    

            this.xaxis1.Text = Convert.ToInt32(lst[1].yearid).ToString();

            this.xaxis0.Text = Convert.ToInt32(lst[0].yearid).ToString();


        }
        private void BindChart()
        {
            List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccMonthly> lst = (List<SPEENTITY.C_21_Acc.EClassDB_BO.EClassAccMonthly>)ViewState["tblMonthly"];


            this.c1.Text = Convert.ToDouble(lst[0].dram).ToString();//.ToString("#,##0.00;(#,##0.00);");
            this.s1.Text = Convert.ToDouble(lst[0].cram).ToString();//.ToString("#,##0.00;(#,##0.00);");

            this.c2.Text = Convert.ToDouble(lst[1].dram).ToString();
            this.s2.Text = Convert.ToDouble(lst[1].cram).ToString();

            this.c3.Text = Convert.ToDouble(lst[2].dram).ToString();
            this.s3.Text = Convert.ToDouble(lst[2].cram).ToString();

            this.c4.Text = Convert.ToDouble(lst[3].dram).ToString();
            this.s4.Text = Convert.ToDouble(lst[3].cram).ToString();

            this.c5.Text = Convert.ToDouble(lst[4].dram).ToString();
            this.s5.Text = Convert.ToDouble(lst[4].cram).ToString();

            this.c6.Text = Convert.ToDouble(lst[5].dram).ToString();
            this.s6.Text = Convert.ToDouble(lst[5].cram).ToString();

            this.c7.Text = Convert.ToDouble(lst[6].dram).ToString();
            this.s7.Text = Convert.ToDouble(lst[6].cram).ToString();

            this.c8.Text = Convert.ToDouble(lst[7].dram).ToString();
            this.s8.Text = Convert.ToDouble(lst[7].cram).ToString();

            this.c9.Text = Convert.ToDouble(lst[8].dram).ToString();
            this.s9.Text = Convert.ToDouble(lst[8].cram).ToString();

            this.c10.Text = Convert.ToDouble(lst[9].dram).ToString();
            this.s10.Text = Convert.ToDouble(lst[9].cram).ToString();

            this.c11.Text = Convert.ToDouble(lst[10].dram).ToString();
            this.s11.Text = Convert.ToDouble(lst[10].cram).ToString();

            this.c12.Text = Convert.ToDouble(lst[11].dram).ToString();
            this.s12.Text = Convert.ToDouble(lst[11].cram).ToString();




        }
        private void ShowDetails()
        {
            Session.Remove("cashbank");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string fromdate = "01" + Date.Substring(2);
            string todate = Convert.ToDateTime(fromdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");




            string txtSVoucher = "ALL Voucher";
            string searchinfo = "";


            //string txtSProject =  "%";
            DataSet ds1 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTCASHBOOK", fromdate, todate, txtSVoucher, searchinfo, "", "", "", "", "");
            if (ds1 == null)
                return;

            //For Grouping
            DataTable dtr = new DataTable();

            DataView dv1 = new DataView();
            dv1 = ds1.Tables[0].DefaultView;
            dv1.RowFilter = ("grp1 = 'A' or grp1 = 'B'  or grp1 = 'C'  ");
            dtr = dv1.ToTable();
            this.lblReceiptCash.Visible = true;
            this.lblPaymentCash.Visible = true;
            this.lblDetailsCash.Visible = true;
            Session["cashbank"] = dtr;
            DataView dvr = new DataView();
            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1 = 'A'");
            DataTable dtr1 = HiddenSameDate(dvr.ToTable());
            this.gvcashbook.DataSource = dtr1;
            this.gvcashbook.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (dtr1.Rows.Count > 0)
                ((HyperLink)this.gvcashbook.HeaderRow.FindControl("hlbtnCBdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            this.FooterCalculation(dtr1, "gvcashbook");
            Session["Report1"] = gvcashbook;
            if (dtr1.Rows.Count > 0)
                ((HyperLink)this.gvcashbook.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='B'");
            DataTable dtr2 = HiddenSameDate(dvr.ToTable());
            this.gvcashbookp.DataSource = dtr2;
            this.gvcashbookp.DataBind();

            if (dtr2.Rows.Count > 0)
                ((HyperLink)this.gvcashbookp.HeaderRow.FindControl("hlbtnCBPdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            this.FooterCalculation(dtr2, "gvcashbookp");
            Session["Report1"] = gvcashbookp;
            if (dtr2.Rows.Count > 0)
                ((HyperLink)this.gvcashbookp.HeaderRow.FindControl("hlbtnCBPdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='C'");
            DataTable dtr3 = dvr.ToTable();
            this.gvcashbookDB.DataSource = dvr.ToTable(); ;
            this.gvcashbookDB.DataBind();
            this.FooterCalculation(dtr3, "gvcashbookDB");
        }


        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Date1, vounum;
            int j;
            Date1 = dt1.Rows[0]["voudat1"].ToString();
            vounum = dt1.Rows[0]["vounum1"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                {
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                    dt1.Rows[j]["vounar"] = "";


                }

                else
                {
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                }

            }
            return dt1;

        }
        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();
            double frecamt = 0, fpayamt1 = 0, netbal;
            DataView dv1; DataTable dt1;

            switch (GvName)
            {
                case "gvcashbook":
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvCashAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                            0 : dt.Compute("sum(casham)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvFBankAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                          0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;


                case "gvcashbookp":
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvCashAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                             0 : dt.Compute("sum(casham)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvFBankAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                           0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;

                case "gvcashbookDB":
                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                             0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lblgvFrecam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(depam)", "")) ?
                                   0 : dt.Compute("sum(depam)", ""))).ToString("#,##0;(#,##0) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                             0 : dt.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFClAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                           0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;
            }


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
                cell02.ColumnSpan = 3;


                TableCell cell03 = new TableCell();
                cell03.Text = "WEEK-3";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 3;


                TableCell cell04 = new TableCell();
                cell04.Text = "WEEK-3";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 3;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                grvWeekSales.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}