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
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_15_Pro
{
    public partial class EntryDailyProduction : System.Web.UI.Page
    {
        ProcessAccess ProData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetOrderName();
                this.GetLineFloor();
                ((Label)this.Master.FindControl("lblTitle")).Text = "HOURLY PRODUCTION ENTRY";




            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            this.RptProduction();

        }
        private void RptProduction()
        {


            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string DayID = Convert.ToDateTime(this.txtCurDate.Text).ToString("yyyyMMdd");
            string Date1 = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = ProData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "HOURLYPRODREPORT", DayID, Date1, "", "", "", "", "", "", "");


            ReportDocument rptChallan = new RMGiRPT.R_15_Pro.RptHourlyProd();
            TextObject txtrptcomp = rptChallan.ReportDefinition.ReportObjects["Company"] as TextObject;
            txtrptcomp.Text = comnam.ToUpper();
            //TextObject txtCompAdd = rptChallan.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            //txtCompAdd.Text = comadd;
            TextObject txtrptHeader = rptChallan.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtrptHeader.Text = "HOURLY PRODUCTION REPORT";


            TextObject txtDate = rptChallan.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");




            string day = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy 8:00:00");
            DateTime datefrm, dateto;
            datefrm = Convert.ToDateTime(day);
            dateto = Convert.ToDateTime(this.txtCurDate.Text).AddHours(24);

            for (int i = 1; i < 13; i++)
            {
                if (datefrm > dateto)
                    break;
                string dayadd = datefrm.ToString("HH:mm");
                if (dayadd == "14:00")
                {
                    datefrm = datefrm.AddMinutes(30);
                }
                TextObject rpttxth = rptChallan.ReportDefinition.ReportObjects["t" + i.ToString()] as TextObject;
                rpttxth.Text = datefrm.ToString("HH:mm");
                datefrm = datefrm.AddMinutes(60);
            }









            TextObject txtuserinfo = rptChallan.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptChallan.SetDataSource(ds2.Tables[0]);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptChallan.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptChallan;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetOrderName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds2 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETMLCOLORSIZECCODE", "%", "", "", "", "", "", "", "");

            ViewState["tblmlcorder"] = ds2.Tables[0];

            DataTable dt1 = ds2.Tables[0].Copy();
            dt1 = dt1.DefaultView.ToTable(true, "actcode", "actdesc");
            this.ddlOrderno.DataSource = dt1;
            this.ddlOrderno.DataTextField = "actdesc";
            this.ddlOrderno.DataValueField = "actcode";
            this.ddlOrderno.DataBind();
            ds2.Dispose();
            this.ddlOrderno_SelectedIndexChanged(null, null);



        }
        protected void ddlOrderno_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = ((DataTable)ViewState["tblmlcorder"]).Copy();
            string mlccode = this.ddlOrderno.SelectedValue.ToString();
            dt1 = dt1.DefaultView.ToTable(true, "actcode", "rescode", "resdesc", "resunit", "qty", "rate", "exqty", "balqty");
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("actcode='" + mlccode + "'");
            dt1 = dv.ToTable();

            this.ddlProduct.DataSource = dt1;
            this.ddlProduct.DataTextField = "resdesc";
            this.ddlProduct.DataValueField = "rescode";
            this.ddlProduct.DataBind();
            ViewState["tblItmCod"] = dt1;
            this.ddlProduct_SelectedIndexChanged(null, null);
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ItmCod = this.ddlProduct.SelectedValue.ToString().Trim();
            DataRow[] dr1 = ((DataTable)ViewState["tblItmCod"]).Select("rescode='" + ItmCod + "'");
            this.lblQty.Text = "Order Qty :" + Convert.ToDouble(dr1[0]["qty"]).ToString("#,##0.");
            this.lblUnit.Text = "Unit : " + dr1[0]["resunit"].ToString().Trim();
            this.lblexeqty.Text = "Production Qty :" + Convert.ToDouble(dr1[0]["exqty"]).ToString("#,##0.") + "  ";
            this.lblBalQty.Text = "Bal. Qty :" + Convert.ToDouble(dr1[0]["balqty"]).ToString("#,##0.") + "  ";


        }
        //private void GetProduct()
        //{
        //    string comcod = this.GetCompCode();
        //    DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETPRODUCT", "%", "", "", "", "", "", "", "", "");
        //    ddlProduct.DataTextField = "prodesc";
        //    ddlProduct.DataValueField = "procod";
        //    ddlProduct.DataSource = ds1.Tables[0];
        //    ddlProduct.DataBind();
        //}
        private void GetLineFloor()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "GETLINEFLOOR", "%", "", "", "", "", "", "", "", "");
            ddlLine.DataTextField = "linedesc";
            ddlLine.DataValueField = "linecode";
            ddlLine.DataSource = ds1.Tables[0];
            ddlLine.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "New")
            {
                this.txtCurDate.Enabled = true;
                //this.txtrefno.Text = "";
                this.gvprotar.DataSource = null;
                this.gvprotar.DataBind();
                this.lbtnOk.Text = "Ok";
                this.lblmsg01.Text = "";
                this.lblmsg01.Visible = false;
                this.txtCurDate.Enabled = true;
                this.ddlProduct.Enabled = true;
                this.ddlLine.Enabled = true;
                this.ddlOrderno.Enabled = true;

                return;
            }
            this.lbtnOk.Text = "New";
            this.txtCurDate.Enabled = false;
            this.ddlLine.Enabled = false;
            this.ddlProduct.Enabled = false;
            this.ddlOrderno.Enabled = false;
            this.Get_Plan_Info();
        }
        private void Get_Plan_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd.MM.yyyy");
            string CurDate2 = Convert.ToDateTime(this.txtCurDate.Text).AddDays(7).ToString("dd.MM.yyyy");
            string orderno = this.ddlOrderno.SelectedValue.ToString();
            string Line = this.ddlLine.SelectedValue.ToString();

            string Date1 = ASTUtility.Right(CurDate1, 4) + CurDate1.Substring(3, 2) + ASTUtility.Left(CurDate1, 2);
            string Date2 = ASTUtility.Right(CurDate2, 4) + CurDate2.Substring(3, 2) + ASTUtility.Left(CurDate2, 2);
            DataSet ds1 = new DataSet();

            string DayID = Convert.ToDateTime(this.txtCurDate.Text).ToString("yyyyMMdd");
            string Product = ASTUtility.Left(this.ddlProduct.SelectedValue.ToString(), 12);
            string colorid = this.ddlProduct.SelectedValue.ToString().Substring(12, 12);
            string sizeid = ASTUtility.Right(this.ddlProduct.SelectedValue.ToString(), 12);

            ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "LINEWISEPRODENTRY", Line, DayID, Product, Date1, orderno, colorid, sizeid, "", "");
            if (ds1 == null)
                return;
            //Session["tblprotar"] =ds1.Tables[0];
            Session["tblprotar"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string proscode = dt1.Rows[0]["proscode"].ToString();
            string empid = dt1.Rows[0]["empid"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["proscode"].ToString() == proscode && dt1.Rows[j]["empid"].ToString() == empid)
                {

                    dt1.Rows[j]["prosdesc"] = "";
                    dt1.Rows[j]["empname"] = "";


                }

                else
                {

                    if (dt1.Rows[j]["proscode"].ToString() == proscode)
                        dt1.Rows[j]["prosdesc"] = "";

                    if (dt1.Rows[j]["empid"].ToString() == empid)
                        dt1.Rows[j]["empname"] = "";
                }





                proscode = dt1.Rows[j]["proscode"].ToString();
                empid = dt1.Rows[j]["empid"].ToString();


            }



            return dt1;

        }
        private void Data_Bind()
        {
            string day = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy 8:00:00");
            DateTime datefrm, dateto;
            datefrm = Convert.ToDateTime(day);
            dateto = Convert.ToDateTime(this.txtCurDate.Text).AddHours(24);

            for (int i = 5; i < 17; i++)
            {
                if (datefrm > dateto)
                    break;
                string dayadd = datefrm.ToString("HH:mm");
                if (dayadd == "14:00")
                {
                    datefrm = datefrm.AddMinutes(30);
                }
                this.gvprotar.Columns[i].HeaderText = datefrm.ToString("HH:mm");
                datefrm = datefrm.AddMinutes(60);
            }


            this.gvprotar.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvprotar.DataSource = (DataTable)Session["tblprotar"];




            this.gvprotar.DataBind();
            //this.FooterCalculation();


        }

        //private void FooterCalculation()
        //{
        //    DataTable dt = ((DataTable)Session["tblprotar"]).Copy();
        //    if (dt.Rows.Count == 0)
        //        return;


        //    ((Label)this.gvprotar.FooterRow.FindControl("lblgvFcapacity")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(capacity)", "")) ?
        //        0.00 : dt.Compute("Sum(capacity)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprotar.FooterRow.FindControl("lblgvFTarqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tqty)", "")) ?
        //        0.00 : dt.Compute("Sum(tqty)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty1)", "")) ?
        //       0.00 : dt.Compute("Sum(qty1)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty2)", "")) ?
        //      0.00 : dt.Compute("Sum(qty2)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty3)", "")) ?
        //      0.00 : dt.Compute("Sum(qty3)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty4)", "")) ?
        //      0.00 : dt.Compute("Sum(qty4)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty5)", "")) ?
        //      0.00 : dt.Compute("Sum(qty5)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty6)", "")) ?
        //      0.00 : dt.Compute("Sum(qty6)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvprotar.FooterRow.FindControl("lgvFQty7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty7)", "")) ?
        //      0.00 : dt.Compute("Sum(qty7)", ""))).ToString("#,##0;(#,##0); ");

        //    ((Label)this.gvprotar.FooterRow.FindControl("lgvFmachine")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(macno)", "")) ?
        //        0.00 : dt.Compute("Sum(macno)", ""))).ToString("#,##0;(#,##0); ");


        //}



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }



        protected void gvprotar_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvprotar.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblprotar"];



            for (int i = 0; i < this.gvprotar.Rows.Count; i++)
            {

                dt.Rows[i]["p1"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp1")).Text.Trim()).ToString();
                dt.Rows[i]["p2"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp2")).Text.Trim()).ToString();
                dt.Rows[i]["p3"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp3")).Text.Trim()).ToString();
                dt.Rows[i]["p4"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp4")).Text.Trim()).ToString();
                dt.Rows[i]["p5"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp5")).Text.Trim()).ToString();
                dt.Rows[i]["p6"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp6")).Text.Trim()).ToString();
                dt.Rows[i]["p7"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp7")).Text.Trim()).ToString();
                dt.Rows[i]["p8"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp8")).Text.Trim()).ToString();
                dt.Rows[i]["p9"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp9")).Text.Trim()).ToString();
                dt.Rows[i]["p10"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp10")).Text.Trim()).ToString();
                dt.Rows[i]["p11"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp11")).Text.Trim()).ToString();
                dt.Rows[i]["p12"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvp12")).Text.Trim()).ToString();

                dt.Rows[i]["tqty"] = Convert.ToDouble("0" + ((TextBox)this.gvprotar.Rows[i].FindControl("txtgvTqty")).Text.Trim()).ToString();
                dt.Rows[i]["totalp"] = Convert.ToDouble(dt.Rows[i]["p1"]) + Convert.ToDouble(dt.Rows[i]["p2"]) + Convert.ToDouble(dt.Rows[i]["p3"])
              + Convert.ToDouble(dt.Rows[i]["p4"]) + Convert.ToDouble(dt.Rows[i]["p5"]) + Convert.ToDouble(dt.Rows[i]["p6"]) + Convert.ToDouble(dt.Rows[i]["p7"])
              + Convert.ToDouble(dt.Rows[i]["p8"]) + Convert.ToDouble(dt.Rows[i]["p9"]) + Convert.ToDouble(dt.Rows[i]["p10"]) + Convert.ToDouble(dt.Rows[i]["p11"])
              + Convert.ToDouble(dt.Rows[i]["p12"]);

                dt.Rows[i]["expercent"] = (Convert.ToDouble(dt.Rows[i]["tqty"]) == 0.00) ? 0.00 : ((Convert.ToDouble(dt.Rows[i]["p1"]) + Convert.ToDouble(dt.Rows[i]["p2"]) + Convert.ToDouble(dt.Rows[i]["p3"])
              + Convert.ToDouble(dt.Rows[i]["p4"]) + Convert.ToDouble(dt.Rows[i]["p5"]) + Convert.ToDouble(dt.Rows[i]["p6"]) + Convert.ToDouble(dt.Rows[i]["p7"])
              + Convert.ToDouble(dt.Rows[i]["p8"]) + Convert.ToDouble(dt.Rows[i]["p9"]) + Convert.ToDouble(dt.Rows[i]["p10"]) + Convert.ToDouble(dt.Rows[i]["p11"])
              + Convert.ToDouble(dt.Rows[i]["p12"])) * 100) / Convert.ToDouble(dt.Rows[i]["tqty"]);
            }



            Session["tblprotar"] = dt;


        }
        protected void ibtnSrchorder_Click(object sender, EventArgs e)
        {

        }




        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            this.lblmsg01.Visible = true;

            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)Session["tblprotar"];

                string Orderno = this.ddlOrderno.SelectedValue.ToString();

                string ProdCode = ASTUtility.Left(this.ddlProduct.SelectedValue.ToString(), 12);
                string colorid = this.ddlProduct.SelectedValue.ToString().Substring(12, 12);
                string sizeid = ASTUtility.Right(this.ddlProduct.SelectedValue.ToString(), 12);

                string linecode = this.ddlLine.SelectedValue.ToString();

                string mPRODAT = this.txtCurDate.Text.Trim();
                // string billref = this.txtrefno.Text.Trim();
                bool result = true;

                for (int i = 0; i < dt1.Rows.Count; i++)
                {

                    string proscode = dt1.Rows[i]["proscode"].ToString();
                    string empid = dt1.Rows[i]["empid"].ToString();
                    double tQty = Convert.ToDouble(dt1.Rows[i]["tqty"].ToString());
                    int j;
                    string Date1 = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy");
                    string day = Convert.ToDateTime(this.txtCurDate.Text).ToString("dd-MMM-yyyy 8:00:00");
                    DateTime date;
                    date = Convert.ToDateTime(day);
                    string dayid = date.ToString("yyyyMMdd");

                    for (j = 1; j <= 12; j++)
                    {

                        string dayadd = date.ToString("HH:mm");
                        if (dayadd == "14:00")
                        {
                            date = date.AddMinutes(30);
                        }


                        double qty = Convert.ToDouble(dt1.Rows[i]["p" + j.ToString()].ToString());
                        if (qty != 0)
                        {
                            result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "INSERTUPDATEPRODUCTION", dayid, linecode, ProdCode, proscode, empid, date.ToString("dd-MMM-yyyy HH:mm"), qty.ToString(), Orderno, colorid, sizeid, "", "", "", "", "PROEXINFA");
                        }

                        if (result == false)
                        {
                            this.lblmsg01.Text = "Updated Failed";
                            return;
                        }

                        date = date.AddMinutes(60);
                    }

                    if (tQty != 0)
                    {
                        result = ProData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_INFO", "INSERTUPDATEPRODUCTION", dayid, linecode, ProdCode, proscode, empid, Date1, tQty.ToString(), Orderno, colorid, sizeid, "", "", "", "", "PROEXINFB");
                    }
                    if (result == false)
                    {
                        this.lblmsg01.Text = "Updated Failed";
                        return;
                    }



                }

                this.lblmsg01.Text = "Updated Successfully";






            }
            catch (Exception ex)
            {
                this.lblmsg01.Text = "Errp:" + ex.Message;
            }
        }

        protected void gvprotar_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 5;

                TableCell cell1 = new TableCell();
                cell1.Text = "HOURLY ACTUAL PRODUCTION";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 12;
                cell1.Font.Bold = true;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell1);
                gvprotar.Controls[0].Controls.AddAt(0, gvrow);
            }

        }


    }
}