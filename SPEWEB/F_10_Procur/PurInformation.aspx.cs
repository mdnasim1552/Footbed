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
using SPEENTITY.C_10_Procur;

namespace SPEWEB.F_10_Procur
{
    public partial class PurInformation : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        UserManPur objUserService = new UserManPur();
        ProcessAccess _DataEntry = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase SMARTBOARD";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.lbtnOk_Click(null, null);


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
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

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

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblYear.Visible = true;
            this.lblWeek.Visible = true;
            this.lblMon.Visible = true;
            //this.lblGrp.Visible = true;
            this.lblDetails.Visible = true;
            this.lblPayDet.Visible = true;
            this.GetPurYearly();
            this.GetPurMonth();
            this.GetPurWeekly();
            this.GetDayWisePur();
            this.GetDayWisePay();
            this.GetComparePurcYear();
        }

        private void GetPurYearly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_10_Procur.EClassPur.EClassYear> list = objUserService.ShowPurYearly(comcod, CurDate);

                if (list == null)
                    return;

                this.grvYearlyPur.DataSource = list;
                this.grvYearlyPur.DataBind();

                ViewState["tblYearly"] = list;
                this.BindChartYear();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindChartYear()
        {
            List<SPEENTITY.C_10_Procur.EClassPur.EClassYear> lst = (List<SPEENTITY.C_10_Procur.EClassPur.EClassYear>)ViewState["tblYearly"];

            //int i = 0;
            //foreach (SPEENTITY.C_10_Procur.EClassPur.EClassYear c1 in list)
            //{
            //    Chart2.Series[i].Points.Add(new DataPoint(0, (c1.ttlamt / 10000000)));
            //    Chart2.Series[i].Points.Add(new DataPoint(1, (c1.purpay / 10000000)));
            //    i++;
            //}

            //Chart2.DataSource = list;
            //Chart2.DataBind();


            this.yc1.Text = Convert.ToInt32(lst[0].purpay).ToString();//.ToString("#,##0.00;(#,##0.00);");
            this.ys1.Text = Convert.ToInt32(lst[0].ttlamt).ToString();//.ToString("#,##0.00;(#,##0.00);");                    
            this.xaxis0.Text = Convert.ToInt32(lst[0].yearid).ToString();

            this.yc2.Text = ((lst.Count == 1) ? 0.00 : Convert.ToInt32(lst[1].purpay)).ToString();//.ToString("#,##0.00;(#,##0.00);");
            this.ys2.Text = ((lst.Count == 1) ? 0.00 : Convert.ToInt32(lst[1].ttlamt)).ToString();//.ToString("#,##0.00;(#,##0.00);");                    
            this.xaxis1.Text = Convert.ToInt32(lst[1].yearid).ToString();

            this.yc3.Text = Convert.ToInt32(lst[2].purpay).ToString();//.ToString("#,##0.00;(#,##0.00);");
            this.ys3.Text = Convert.ToInt32(lst[2].ttlamt).ToString();//.ToString("#,##0.00;(#,##0.00);");                    
            this.xaxis2.Text = Convert.ToInt32(lst[2].yearid).ToString();



        }
        private void GetPurMonth()
        {
            try
            {
                string comcod = this.GetCompCode();
                string curDate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly> list2 = objUserService.ShowPurMonth(comcod, curDate);// objUserService.ShowPurMonth(comcod, curDate);
                ///List<SPEENTITY.C_10_Procur.EClassPur.EClassYear> list = objUserService.ShowPurYearly(comcod, CurDate);
                if (list2 == null)
                    return;

                this.grvMonthlyPur.DataSource = list2;
                this.grvMonthlyPur.DataBind();
                ViewState["tblMonthly"] = list2;

                ((Label)this.grvMonthlyPur.FooterRow.FindControl("lblyFAmt")).Text = list2.Select(p => p.ttlsalamt).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvMonthlyPur.FooterRow.FindControl("lblyFtpayamt")).Text = list2.Select(p => p.tpayamt).Sum().ToString("#,##0;(#,##0); ");


                this.BindChart();
            }
            catch (Exception ex)
            {
            }
        }
        private void BindChart()
        {
            List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly> lst = (List<SPEENTITY.C_10_Procur.EClassPur.EClassMonthly>)ViewState["tblMonthly"];

            //Chart1.Series["Series1"].XValueMember = "yearmon1";
            //Chart1.Series["Series1"].YValueMembers = "ttlsalamt";
            //Chart1.Series["Series1"].LegendText = "Purchase";


            //Chart1.Series["Series2"].XValueMember = "yearmon1";
            //Chart1.Series["Series2"].YValueMembers = "tpayamt";
            //Chart1.Series["Series2"].LegendText = "Payment";

            //Chart1.DataSource = lst;
            //Chart1.DataBind();

            this.c1.Text = Convert.ToInt32(lst[0].tpayamt).ToString();//.ToString("#,##0.00;(#,##0.00);");
            this.s1.Text = Convert.ToInt32(lst[0].ttlsalamt).ToString();//.ToString("#,##0.00;(#,##0.00);");

            this.c2.Text = Convert.ToInt32(lst[1].tpayamt).ToString();
            this.s2.Text = Convert.ToInt32(lst[1].ttlsalamt).ToString();

            this.c3.Text = Convert.ToInt32(lst[2].tpayamt).ToString();
            this.s3.Text = Convert.ToInt32(lst[2].ttlsalamt).ToString();

            this.c4.Text = Convert.ToInt32(lst[3].tpayamt).ToString();
            this.s4.Text = Convert.ToInt32(lst[3].ttlsalamt).ToString();

            this.c5.Text = Convert.ToInt32(lst[4].tpayamt).ToString();
            this.s5.Text = Convert.ToInt32(lst[4].ttlsalamt).ToString();

            this.c6.Text = Convert.ToInt32(lst[5].tpayamt).ToString();
            this.s6.Text = Convert.ToInt32(lst[5].ttlsalamt).ToString();

            this.c7.Text = Convert.ToInt32(lst[6].tpayamt).ToString();
            this.s7.Text = Convert.ToInt32(lst[6].ttlsalamt).ToString();

            this.c8.Text = Convert.ToInt32(lst[7].tpayamt).ToString();
            this.s8.Text = Convert.ToInt32(lst[7].ttlsalamt).ToString();

            this.c9.Text = Convert.ToInt32(lst[8].tpayamt).ToString();
            this.s9.Text = Convert.ToInt32(lst[8].ttlsalamt).ToString();

            this.c10.Text = Convert.ToInt32(lst[9].tpayamt).ToString();
            this.s10.Text = Convert.ToInt32(lst[9].ttlsalamt).ToString();

            this.c11.Text = Convert.ToInt32(lst[10].tpayamt).ToString();
            this.s11.Text = Convert.ToInt32(lst[10].ttlsalamt).ToString();

            this.c12.Text = Convert.ToInt32(lst[11].tpayamt).ToString();
            this.s12.Text = Convert.ToInt32(lst[11].ttlsalamt).ToString();




        }

        private void GetPurWeekly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_10_Procur.EClassPur.EClassWeekly> lst1 = objUserService.ShowPurWeekly(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvWeekPur.DataSource = lst1;
                this.grvWeekPur.DataBind();

                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt1")).Text = (lst1.Select(p => p.wpamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt2")).Text = (lst1.Select(p => p.wpamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt3")).Text = (lst1.Select(p => p.wpamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt4")).Text = (lst1.Select(p => p.wpamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt4).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt2T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt3T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2 + p.wpamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt4T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2 + p.wpamt3 + p.wpamt4)).Sum().ToString("#,##0;(#,##0); ");


                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt1")).Text = (lst1.Select(p => p.wpayamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt2")).Text = (lst1.Select(p => p.wpayamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt3")).Text = (lst1.Select(p => p.wpayamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt4")).Text = (lst1.Select(p => p.wpayamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt4).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt2T")).Text = lst1.Select(p => (p.wpayamt1 + p.wpayamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt3T")).Text = lst1.Select(p => (p.wpayamt1 + p.wpayamt2 + p.wpayamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt4T")).Text = lst1.Select(p => (p.wpayamt1 + p.wpayamt2 + p.wpayamt3 + p.wpayamt4)).Sum().ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {

            }
        }

        private void GetDayWisePur()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur> lst = objUserService.ShowPurDayWise(comcod, CurDate1);
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
        private List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur> HiddenSameData(List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string billno = "";
            foreach (SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur c1 in lst3)
            {
                if (i == 0)
                {
                    billno = c1.billno;
                    i++;
                    continue;

                }
                else if (c1.billno == billno)
                {
                    c1.billno1 = "";
                    c1.billdate1 = "";
                    c1.pactdesc = "";
                    c1.vounum1 = "";
                    c1.ssirdesc = "";
                }
                billno = c1.billno;

            }

            return lst3;

        }
        private void FooterCalculation()
        {


            List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur> lst3 = (List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePur>)ViewState["tblDayWise"];
            if (lst3.Count == 0)
                return;

            ((Label)this.GvDayWise.FooterRow.FindControl("lblFitmamt")).Text = Convert.ToDouble(lst3.Select(p => p.billamt).Sum()).ToString("#,##0;(#,##0); ");

        }
        private void GetDayWisePay()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePay> lst = objUserService.ShowPayDayWise(comcod, CurDate1);
                if (lst == null)
                    return;

                this.gvDayWisePay.DataSource = HiddenSameData2(lst);
                this.gvDayWisePay.DataBind();
                if (lst.Count < 0)
                    return;
                ((Label)this.gvDayWisePay.FooterRow.FindControl("lblFamount")).Text = lst.Select(p => p.payamt).Sum().ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {

            }
        }
        private List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePay> HiddenSameData2(List<SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePay> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string vounum = "";
            foreach (SPEENTITY.C_10_Procur.EClassPur.EClassDayWisePay c1 in lst3)
            {
                if (i == 0)
                {
                    vounum = c1.vounum;
                    i++;
                    continue;

                }
                else if (c1.vounum == vounum)
                {
                    c1.vounum1 = "";
                    c1.voudat = "";
                    c1.pactdesc = "";
                    c1.billno1 = "";
                    c1.cactdesc = "";
                }
                vounum = c1.vounum;

            }

            return lst3;

        }


        protected void grvWeekPur_RowDataBound(object sender, GridViewRowEventArgs e)
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
                grvWeekPur.Controls[0].Controls.AddAt(0, gvrow);
            }
        }


        private void GetComparePurcYear()
        {
            string comcod = this.GetCompCode();

            string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");

            //this.lblModDayCol.Text = "Receipt";
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO", "PURYEARSUMM", CurDate1, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            //Purchase 	 
            this.gvPurDet.DataSource = ds2;
            this.gvPurDet.DataBind();
            double prevamt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(prvpur)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(prvpur)", "")));

            //((Label)this.gvPurDet.FooterRow.FindControl("lblFPrevAmtPurdet")).Text = (prevamt == 0.00) ? "0.00" : prevamt.ToString("#,##0;(#,##0);  ");


            double curamt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(curpur)", "")) ?
                   0.00 : ds2.Tables[0].Compute("Sum(curpur)", "")));
            //((Label)this.gvPurDet.FooterRow.FindControl("lblypryCollamtPurdet")).Text = curamt.ToString("#,##0;(#,##0); - ");


            //Recipt
            this.gvPaymDet.DataSource = ds2;
            this.gvPaymDet.DataBind();
            double prevamt1 = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(prvpay)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(prvpay)", "")));
            //((Label)this.gvPaymDet.FooterRow.FindControl("lblpayamtprev")).Text = (prevamt1 == 0.00) ? "0.00" : prevamt1.ToString("#,##0;(#,##0);  ");


            double curamt1 = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(curpay)", "")) ?
                   0.00 : ds2.Tables[0].Compute("Sum(curpay)", "")));
            //  ((Label)this.gvPaymDet.FooterRow.FindControl("lblpayamtcur")).Text = curamt1.ToString("#,##0;(#,##0); - ");





            return;

        }
    }
}