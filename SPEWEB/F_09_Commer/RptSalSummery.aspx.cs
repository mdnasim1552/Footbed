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
using Microsoft.Reporting.WinForms;
using System.Drawing;

namespace SPEWEB.F_09_Commer
{
    public partial class RptSalSummery : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        public static double OpenBal, Clsbal;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01" + Date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(txtfromdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Type = this.Request.QueryString["Type"].ToString();
                //this.lblHeader.Text = (Type == "QtyBasis") ? "SALES  SUMMARY REPORT(QTY BASIS)" :(Type == "SalesRegister")? "SALES REGISTER"
                //    : (Type == "dSaleVsColl") ? "DAILY SALES & COLLECTION STATUS" : (Type == "CollectStatus") ? "REAL COLLECTION STATUS"
                //    : (Type == "BankRecon") ? "Bank Reconcillation Summary" : "SALES  SUMMARY REPORT (AMOUNT BASIS)";
                Session.Remove("tblsalsum");

                this.ViewSection();
                this.GetLcType();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "QtyBasis") ? "SALES  SUMMARY REPORT(QTY BASIS)" : (Type == "SalesRegister") ? "SALES REGISTER"
                    : (Type == "dSaleVsColl") ? "DAILY SALES & COLLECTION STATUS" : (Type == "CollectStatus") ? "REAL COLLECTION STATUS"
                    : (Type == "BankRecon") ? "Bank Reconcillation Summary" : (Type == "SerCost") ? "Service Center Cost"
                    : (Type == "ProAgaing") ? "Products Againg Reports" : (Type == "LcCost") ? "All L/C Cost" : (Type == "LcReceive") ? "L/C Receive Report" : "";

                //this.GetCirTeri();
            }

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

            Hashtable hst = (Hashtable)Session["tblLogin"];

            //this.lblCir.Text = hst["circle"].ToString();
            //this.lblReg.Text = hst["area"].ToString();
            //this.lblArea.Text = hst["region"].ToString();
            //this.lbltrri.Text = hst["territory"].ToString();


            //this.gvSalcolDues.Columns[4].HeaderText = hst["territory"].ToString();
            //this.gvSalcolDues.Columns[5].HeaderText = hst["region"].ToString();
            //this.gvSalcolDues.Columns[6].HeaderText = hst["area"].ToString();
            //this.gvSalcolDues.Columns[7].HeaderText = hst["circle"].ToString();

        }

        protected void ViewSection()
        {
            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "QtyBasis":
                    this.MultiView1.ActiveViewIndex = 0;

                    break;
                case "AmtBasis":
                    this.MultiView1.ActiveViewIndex = 1;

                    break;

                case "dSaleVsColl":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "SalesRegister":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "CollectStatus":
                    this.chkwithoutrep.Visible = true;
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "BankRecon":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;
                case "SalComm":
                    this.MultiView1.ActiveViewIndex = 6;
                    break;
                case "SerCost":
                    this.MultiView1.ActiveViewIndex = 7;
                    break;
                case "ProAgaing":
                    this.MultiView1.ActiveViewIndex = 8;
                    break;
                case "LcCost":
                    this.CirPanel.Visible = false;
                    this.lctypePanel.Visible = true;
                    this.MultiView1.ActiveViewIndex = 9;
                    break;
                case "LcReceive":
                    this.CirPanel.Visible = false;
                    this.lctypePanel.Visible = true;
                    this.MultiView1.ActiveViewIndex = 11;
                    break;

                case "SalColDu":
                    this.lblCir.Visible = true;
                    this.lbltrri.Visible = true;
                    this.ddlcircle.Visible = true;
                    this.ddlterri.Visible = true;
                    this.GetCirTeri();

                    this.MultiView1.ActiveViewIndex = 10;
                    break;

            }

        }

        private void GetLcType()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORTS_LC_INFO", "GETLCTYPE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                this.ddllctypelist.DataSource = ds2.Tables[0];
                this.ddllctypelist.DataTextField = "actdesc";
                this.ddllctypelist.DataValueField = "actcode";
                this.ddllctypelist.SelectedValue = "000000000000";
                this.ddllctypelist.DataBind();

                ds2.Dispose();

            }
        }
        private void GetCirTeri()
        {
            DataTable dt1 = new DataTable();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_sales.SP_REPORT_SALES_INFO_02", "GETPOINTPROCAT", "", "", "", "", "", "", "", "", "");

            ViewState["tblOrdSer"] = ds1.Tables[0];
            ViewState["tblOrdSer_2"] = ds1.Tables[1];
            dt1 = ds1.Tables[0].Copy();

            dt1 = dt1.DefaultView.ToTable(true, "circode", "circle");
            dt1.Rows.Add("00000", "ALL");

            this.ddlcircle.DataTextField = "circle";
            this.ddlcircle.DataValueField = "circode";
            this.ddlcircle.DataSource = dt1;
            this.ddlcircle.DataBind();
            this.ddlcircle.SelectedValue = "00000";
            //this.ddlcircle_SelectedIndexChanged(null, null);
            this.ddlcircle_SelectedIndexChanged(null, null);

        }
        protected void ddlcircle_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = ((DataTable)ViewState["tblOrdSer"]).Copy();
            string Div1 = this.ddlcircle.SelectedValue.ToString();
            if (Div1 != "00000")
            {
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("circode='" + Div1 + "'");
                dt1 = dv.ToTable();
            }
            dt1 = dt1.Copy();

            //DataView dv1 = dt1.DefaultView;
            //dv1.RowFilter = ("tericode like '45%' and circode='" + Region + "'");
            //dt1 = dv1.ToTable();

            dt1 = dt1.DefaultView.ToTable(true, "regcode", "region");
            dt1.Rows.Add("00000", "ALL");
            this.ddlRegi.DataTextField = "region";
            this.ddlRegi.DataValueField = "regcode";
            this.ddlRegi.DataSource = dt1;
            this.ddlRegi.DataBind();
            this.ddlRegi.SelectedValue = "00000";
            this.ddlRegi_SelectedIndexChanged(null, null);

        }
        protected void ddlRegi_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = ((DataTable)ViewState["tblOrdSer"]).Copy();
            string circode = this.ddlcircle.SelectedValue.ToString();
            if (circode != "00000")
            {
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("circode='" + circode + "'");
                dt1 = dv.ToTable();

                string Regi = this.ddlRegi.SelectedValue.ToString();
                if (Regi != "00000")
                {
                    DataView dv1 = dt1.DefaultView;
                    dv1.RowFilter = ("regcode='" + Regi + "'");
                    dt1 = dv1.ToTable();
                }
            }
            dt1 = dt1.Copy();
            //DataView dv1 = dt1.DefaultView;
            //dv1.RowFilter = ("tericode like '45%' and circode='" + Region + "'");
            //dt1 = dv1.ToTable();
            dt1 = dt1.DefaultView.ToTable(true, "areacode", "area");
            dt1.Rows.Add("00000", "ALL");
            this.ddlArea.DataTextField = "area";
            this.ddlArea.DataValueField = "areacode";
            this.ddlArea.DataSource = dt1;
            this.ddlArea.DataBind();
            this.ddlArea.SelectedValue = "00000";
            this.ddlArea_SelectedIndexChanged(null, null);
        }
        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = ((DataTable)ViewState["tblOrdSer"]).Copy();
            string circle = this.ddlcircle.SelectedValue.ToString();
            if (circle != "00000")
            {
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("circode='" + circle + "'");
                dt1 = dv.ToTable();

                string Area = this.ddlArea.SelectedValue.ToString();
                if (Area != "00000")
                {
                    DataView dv1 = dt1.DefaultView;
                    dv1.RowFilter = ("areacode='" + Area + "'");
                    dt1 = dv1.ToTable();
                }
            }

            dt1 = dt1.Copy();
            //DataView dv1 = dt1.DefaultView;
            //dv1.RowFilter = ("tericode like '46%' and circode='" + Area + "'");
            ////dv1.RowFilter = ("tericode like '46%'");
            //dt1 = dv1.ToTable();
            dt1 = dt1.DefaultView.ToTable(true, "tericode", "territory");
            dt1.Rows.Add("00000", "ALL");
            this.ddlterri.DataTextField = "territory";
            this.ddlterri.DataValueField = "tericode";
            this.ddlterri.DataSource = dt1;
            this.ddlterri.DataBind();
            this.ddlterri.SelectedValue = "00000";

            this.ddlterri_SelectedIndexChanged(null, null);
        }
        protected void ddlterri_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Data_Bind()
        {
            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;

            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "QtyBasis":

                    break;
                case "AmtBasis":

                    break;

                case "dSaleVsColl":

                    break;

                case "SalesRegister":

                    break;
                case "CollectStatus":

                    break;
                case "BankRecon":

                    break;
                case "SalComm":

                    break;
                case "SerCost":

                    break;
                case "ProAgaing":
                    var lst = (List<SPEENTITY.C_22_Sal.Sales_BO.ProAgaing>)Session["tblsalsum"];
                    this.gvProAging.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvProAging.DataSource = lst;
                    this.gvProAging.DataBind();
                    break;
                case "LcCost":

                    break;
                case "LcReceive":

                    break;

                case "SalColDu":
                    var lstsdu = (List<SPEENTITY.C_22_Sal.Sales_BO.SalColDues>)ViewState["tblsalcoldues"];

                    this.gvSalcolDues.DataSource = lstsdu;
                    this.gvSalcolDues.DataBind();
                    this.FooterCalculation(null, "SalColDu");


                    for (int i = 0; i < gvSalcolDues.Rows.Count; i++)
                    {
                        string grp = ((Label)gvSalcolDues.Rows[i].FindControl("lgvgrp")).Text.Trim();

                        if (grp == "B")
                        {
                            this.gvSalcolDues.Rows[i].BackColor = Color.SkyBlue;
                            this.gvSalcolDues.Rows[i].ForeColor = Color.Black;
                        }
                        else if (grp == "C")
                        {
                            this.gvSalcolDues.Rows[i].BackColor = Color.YellowGreen; //Chartreuse
                            this.gvSalcolDues.Rows[i].ForeColor = Color.Black;
                        }
                        else if (grp == "D")
                        {
                            this.gvSalcolDues.Rows[i].BackColor = Color.Coral;  ///BurlyWood
                            this.gvSalcolDues.Rows[i].ForeColor = Color.Black;
                        }

                    }

                    amt1 = (lstsdu.Select(p => p.d2).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d2).Sum();
                    amt2 = (lstsdu.Select(p => p.d2).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d2).Sum();
                    amt3 = (lstsdu.Select(p => p.d3).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d3).Sum();
                    amt4 = (lstsdu.Select(p => p.d4).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d4).Sum();
                    amt5 = (lstsdu.Select(p => p.d5).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d5).Sum();
                    amt6 = (lstsdu.Select(p => p.d6).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d6).Sum();
                    amt7 = (lstsdu.Select(p => p.d7).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d7).Sum();
                    amt8 = (lstsdu.Select(p => p.d8).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d8).Sum();
                    amt9 = (lstsdu.Select(p => p.d9).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d9).Sum();
                    amt10 = (lstsdu.Select(p => p.d10).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d10).Sum();
                    amt11 = (lstsdu.Select(p => p.d11).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d11).Sum();
                    amt12 = (lstsdu.Select(p => p.d12).Sum() == 0.00) ? 0.00 : lstsdu.Select(p => p.d12).Sum();




                    //this.gvSalcolDues.Columns[4].Visible = (amt1 != 0);
                    this.gvSalcolDues.Columns[13].Visible = (amt1 != 0);
                    this.gvSalcolDues.Columns[14].Visible = (amt1 != 0);
                    this.gvSalcolDues.Columns[15].Visible = (amt1 != 0);
                    this.gvSalcolDues.Columns[16].Visible = (amt1 != 0);

                    this.gvSalcolDues.Columns[17].Visible = (amt2 != 0);
                    this.gvSalcolDues.Columns[18].Visible = (amt2 != 0);
                    this.gvSalcolDues.Columns[19].Visible = (amt2 != 0);
                    this.gvSalcolDues.Columns[20].Visible = (amt2 != 0);
                    //this.gvSalcolDues.Columns[21].Visible = (amt3 != 0);

                    this.gvSalcolDues.Columns[22].Visible = (amt3 != 0);
                    this.gvSalcolDues.Columns[23].Visible = (amt3 != 0);
                    this.gvSalcolDues.Columns[24].Visible = (amt3 != 0);
                    this.gvSalcolDues.Columns[25].Visible = (amt3 != 0);
                    //this.gvSalcolDues.Columns[26].Visible = (amt4 != 0);

                    this.gvSalcolDues.Columns[27].Visible = (amt4 != 0);
                    this.gvSalcolDues.Columns[28].Visible = (amt4 != 0);
                    this.gvSalcolDues.Columns[29].Visible = (amt4 != 0);
                    this.gvSalcolDues.Columns[30].Visible = (amt4 != 0);
                    //this.gvSalcolDues.Columns[31].Visible = (amt5 != 0);

                    this.gvSalcolDues.Columns[32].Visible = (amt5 != 0);
                    this.gvSalcolDues.Columns[33].Visible = (amt5 != 0);
                    this.gvSalcolDues.Columns[34].Visible = (amt5 != 0);
                    this.gvSalcolDues.Columns[35].Visible = (amt5 != 0);
                    //this.gvSalcolDues.Columns[36].Visible = (amt6 != 0);

                    this.gvSalcolDues.Columns[37].Visible = (amt6 != 0);
                    this.gvSalcolDues.Columns[38].Visible = (amt6 != 0);
                    this.gvSalcolDues.Columns[39].Visible = (amt6 != 0);
                    this.gvSalcolDues.Columns[40].Visible = (amt6 != 0);
                    //this.gvSalcolDues.Columns[41].Visible = (amt7 != 0);

                    this.gvSalcolDues.Columns[42].Visible = (amt7 != 0);
                    this.gvSalcolDues.Columns[43].Visible = (amt7 != 0);
                    this.gvSalcolDues.Columns[44].Visible = (amt7 != 0);
                    this.gvSalcolDues.Columns[45].Visible = (amt7 != 0);
                    //this.gvSalcolDues.Columns[46].Visible = (amt8 != 0);

                    this.gvSalcolDues.Columns[47].Visible = (amt8 != 0);
                    this.gvSalcolDues.Columns[48].Visible = (amt8 != 0);
                    this.gvSalcolDues.Columns[49].Visible = (amt8 != 0);
                    this.gvSalcolDues.Columns[50].Visible = (amt8 != 0);
                    //this.gvSalcolDues.Columns[51].Visible = (amt9 != 0);

                    this.gvSalcolDues.Columns[52].Visible = (amt9 != 0);
                    this.gvSalcolDues.Columns[53].Visible = (amt9 != 0);
                    this.gvSalcolDues.Columns[54].Visible = (amt9 != 0);
                    this.gvSalcolDues.Columns[55].Visible = (amt9 != 0);
                    //this.gvSalcolDues.Columns[56].Visible = (amt10 != 0);

                    this.gvSalcolDues.Columns[57].Visible = (amt10 != 0);
                    this.gvSalcolDues.Columns[58].Visible = (amt10 != 0);
                    this.gvSalcolDues.Columns[59].Visible = (amt10 != 0);
                    this.gvSalcolDues.Columns[60].Visible = (amt10 != 0);
                    //this.gvSalcolDues.Columns[61].Visible = (amt11 != 0);

                    this.gvSalcolDues.Columns[62].Visible = (amt11 != 0);
                    this.gvSalcolDues.Columns[63].Visible = (amt11 != 0);
                    this.gvSalcolDues.Columns[64].Visible = (amt11 != 0);
                    this.gvSalcolDues.Columns[65].Visible = (amt11 != 0);
                    //this.gvSalcolDues.Columns[66].Visible = (amt11 != 0);

                    this.gvSalcolDues.Columns[67].Visible = (amt12 != 0);
                    this.gvSalcolDues.Columns[68].Visible = (amt12 != 0);
                    this.gvSalcolDues.Columns[69].Visible = (amt12 != 0);
                    this.gvSalcolDues.Columns[70].Visible = (amt12 != 0);
                    //this.gvSalcolDues.Columns[71].Visible = (amt11 != 0);



                    if (lstsdu.Count == 0)
                        return;

                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                    ((HyperLink)this.gvSalcolDues.HeaderRow.FindControl("hlbtnSalExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                    Session["Report1"] = gvSalcolDues;
                    if (dr1.Length > 0)
                        ((HyperLink)this.gvSalcolDues.HeaderRow.FindControl("hlbtnSalExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



                    break;

            }

        }

        private void FooterCalculation(DataTable dt, string GvName)
        {


            switch (GvName)
            {
                case "gvSalSummery":
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtSh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(thead1)", "")) ?
                //        0 : dt.Compute("sum(thead1)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtCs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(thead2)", "")) ?
                //      0 : dt.Compute("sum(thead2)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtApt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(thead3)", "")) ?
                //        0 : dt.Compute("sum(thead3)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtOt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(thead4)", "")) ?
                //      0 : dt.Compute("sum(thead4)", ""))).ToString("#,##0;(#,##0); ");

                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFBSh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bhead1)", "")) ?
                //        0 : dt.Compute("sum(bhead1)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFBCs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bhead2)", "")) ?
                //      0 : dt.Compute("sum(bhead2)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFBApt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bhead3)", "")) ?
                //        0 : dt.Compute("sum(bhead3)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFBOt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bhead4)", "")) ?
                //      0 : dt.Compute("sum(bhead4)", ""))).ToString("#,##0;(#,##0); ");


                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFCSh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chead1)", "")) ?
                //        0 : dt.Compute("sum(chead1)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFCCs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chead2)", "")) ?
                //      0 : dt.Compute("sum(chead2)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFCApt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chead3)", "")) ?
                //        0 : dt.Compute("sum(chead3)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFCOt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chead4)", "")) ?
                //      0 : dt.Compute("sum(chead4)", ""))).ToString("#,##0;(#,##0); ");

                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtSSh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shead1)", "")) ?
                //        0 : dt.Compute("sum(shead1)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtSCs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shead2)", "")) ?
                //      0 : dt.Compute("sum(shead2)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtSApt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shead3)", "")) ?
                //        0 : dt.Compute("sum(shead3)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFtSOt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shead4)", "")) ?
                //      0 : dt.Compute("sum(shead4)", ""))).ToString("#,##0;(#,##0); ");


                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFASh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ahead1)", "")) ?
                //        0 : dt.Compute("sum(ahead1)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFACS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ahead2)", "")) ?
                //      0 : dt.Compute("sum(ahead2)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFAApt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ahead3)", "")) ?
                //        0 : dt.Compute("sum(ahead3)", ""))).ToString("#,##0;(#,##0); ");
                //((Label)this.gvSalSummery.FooterRow.FindControl("lgvFAot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ahead4)", "")) ?
                //      0 : dt.Compute("sum(ahead4)", ""))).ToString("#,##0;(#,##0); ");
                //break;


                case "gvSalAmt":
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtShA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salval1)", "")) ?
                    //        0 : dt.Compute("sum(salval1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtCsA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salval2)", "")) ?
                    //      0 : dt.Compute("sum(salval2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtAptA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salval3)", "")) ?
                    //        0 : dt.Compute("sum(salval3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtOtA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salval4)", "")) ?
                    //      0 : dt.Compute("sum(salval4)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFBShA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(brecvam1)", "")) ?
                    //        0 : dt.Compute("sum(brecvam1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFBCsA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(brecvam2)", "")) ?
                    //      0 : dt.Compute("sum(brecvam2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFBAptA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(brecvam3)", "")) ?
                    //        0 : dt.Compute("sum(brecvam3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFBOtA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(brecvam4)", "")) ?
                    //      0 : dt.Compute("sum(brecvam4)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFCShA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crecvam1)", "")) ?
                    //        0 : dt.Compute("sum(crecvam1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFCCsA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crecvam2)", "")) ?
                    //      0 : dt.Compute("sum(crecvam2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFCAptA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crecvam3)", "")) ?
                    //        0 : dt.Compute("sum(crecvam3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFCOtA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(crecvam4)", "")) ?
                    //      0 : dt.Compute("sum(crecvam4)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtRShA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trecvam1)", "")) ?
                    //        0 : dt.Compute("sum(trecvam1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtRCsA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trecvam2)", "")) ?
                    //      0 : dt.Compute("sum(trecvam2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtRAptA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trecvam3)", "")) ?
                    //        0 : dt.Compute("sum(trecvam3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFtROtA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trecvam4)", "")) ?
                    //      0 : dt.Compute("sum(trecvam4)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFRShA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvbal1)", "")) ?
                    //        0 : dt.Compute("sum(recvbal1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFRCSA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvbal2)", "")) ?
                    //      0 : dt.Compute("sum(recvbal2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFRAptA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvbal3)", "")) ?
                    //        0 : dt.Compute("sum(recvbal3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalAmt.FooterRow.FindControl("lgvFRotA")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recvbal4)", "")) ?
                    //      0 : dt.Compute("sum(recvbal4)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    break;
                case "gvSalComm":
                    //((Label)this.grvSalesComm.FooterRow.FindControl("lgvFmonsalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(msaleamt)", "")) ?
                    //        0 : dt.Compute("sum(msaleamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.grvSalesComm.FooterRow.FindControl("lgvFtsalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tasaleamt)", "")) ?
                    //      0 : dt.Compute("sum(tasaleamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.grvSalesComm.FooterRow.FindControl("lgvFtatsaleamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(commamt)", "")) ?
                    //        0 : dt.Compute("sum(commamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.grvSalesComm.FooterRow.FindControl("lgvFmoncollamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mcollamt)", "")) ?
                    //      0 : dt.Compute("sum(mcollamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.grvSalesComm.FooterRow.FindControl("lgvFtcollamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tacollamt)", "")) ?
                    //      0 : dt.Compute("sum(tacollamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "SerCost":

                    break;
                case "ProAgaing":
                    break;
                case "gvLCReceived":
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rqty)", "")) ?
                           0 : dt.Compute("sum(rqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFfob")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fob)", "")) ?
                           0 : dt.Compute("sum(fob)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFINV")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(invvalu)", "")) ?
                           0 : dt.Compute("sum(invvalu)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFADVANCE")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(advance)", "")) ?
                           0 : dt.Compute("sum(advance)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFcDUTY")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cduty)", "")) ?
                           0 : dt.Compute("sum(cduty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFVAT")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(vat)", "")) ?
                           0 : dt.Compute("sum(vat)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFAIT")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ait)", "")) ?
                           0 : dt.Compute("sum(ait)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFsSCM")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(scm)", "")) ?
                           0 : dt.Compute("sum(scm)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFfreight")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(freight)", "")) ?
                           0 : dt.Compute("sum(freight)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFinsurance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(insurance)", "")) ?
                           0 : dt.Compute("sum(insurance)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFothers")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(others)", "")) ?
                           0 : dt.Compute("sum(others)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.GridLCReceived.FooterRow.FindControl("lgvFtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(total)", "")) ?
                           0 : dt.Compute("sum(total)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "SalColDu":

                    //var lstsdu = (List<SPEENTITY.C_22_Sal.Sales_BO.SalColDues>)ViewState["tblsalcoldues"];


                    //var lstsdu1 = lstsdu.FindAll(p => p.grp == "A");

                    //if (lstsdu1.Count == 0)
                    //        return;

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFopamt")).Text = (lstsdu1.Select(p => p.opamt).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.opamt).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs1")).Text = (lstsdu1.Select(p => p.s1).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s1).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr1")).Text = (lstsdu1.Select(p => p.r1).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r1).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc1")).Text = (lstsdu1.Select(p => p.c1).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c1).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd1")).Text = (lstsdu1.Select(p => p.d1).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d1).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs2")).Text = (lstsdu1.Select(p => p.s2).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s2).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr2")).Text = (lstsdu1.Select(p => p.r2).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r2).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc3")).Text = (lstsdu1.Select(p => p.c3).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c3).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd4")).Text = (lstsdu1.Select(p => p.d4).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d4).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs3")).Text = (lstsdu1.Select(p => p.s3).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s3).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr3")).Text = (lstsdu1.Select(p => p.r3).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r3).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc3")).Text = (lstsdu1.Select(p => p.c3).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c3).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd3")).Text = (lstsdu1.Select(p => p.d3).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d3).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs4")).Text = (lstsdu1.Select(p => p.s4).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s4).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr4")).Text = (lstsdu1.Select(p => p.r4).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r4).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc4")).Text = (lstsdu1.Select(p => p.c4).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c4).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd4")).Text = (lstsdu1.Select(p => p.d4).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d4).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs5")).Text = (lstsdu1.Select(p => p.s5).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s5).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr5")).Text = (lstsdu1.Select(p => p.r5).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r5).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc5")).Text = (lstsdu1.Select(p => p.c5).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c5).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd5")).Text = (lstsdu1.Select(p => p.d5).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d5).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs6")).Text = (lstsdu1.Select(p => p.s6).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s6).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr6")).Text = (lstsdu1.Select(p => p.r6).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r6).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc6")).Text = (lstsdu1.Select(p => p.c6).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c6).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd6")).Text = (lstsdu1.Select(p => p.d6).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d6).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs7")).Text = (lstsdu1.Select(p => p.s7).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s7).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr7")).Text = (lstsdu1.Select(p => p.r7).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r7).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc7")).Text = (lstsdu1.Select(p => p.c7).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c7).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd7")).Text = (lstsdu1.Select(p => p.d7).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d7).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs8")).Text = (lstsdu1.Select(p => p.s8).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s8).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr8")).Text = (lstsdu1.Select(p => p.r8).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r8).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc8")).Text = (lstsdu1.Select(p => p.c8).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c8).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd8")).Text = (lstsdu1.Select(p => p.d8).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d8).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs9")).Text = (lstsdu1.Select(p => p.s9).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s9).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr9")).Text = (lstsdu1.Select(p => p.r9).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r9).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc9")).Text = (lstsdu1.Select(p => p.c9).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c9).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd9")).Text = (lstsdu1.Select(p => p.d9).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d9).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs10")).Text = (lstsdu1.Select(p => p.s10).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s10).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr10")).Text = (lstsdu1.Select(p => p.r10).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r10).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc10")).Text = (lstsdu1.Select(p => p.c10).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c10).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd10")).Text = (lstsdu1.Select(p => p.d10).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d10).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs11")).Text = (lstsdu1.Select(p => p.s11).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s11).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr11")).Text = (lstsdu1.Select(p => p.r11).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r11).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc11")).Text = (lstsdu1.Select(p => p.c11).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c11).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd11")).Text = (lstsdu1.Select(p => p.d11).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d11).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFs12")).Text = (lstsdu1.Select(p => p.s12).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.s12).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFr12")).Text = (lstsdu1.Select(p => p.r12).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.r12).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFc12")).Text = (lstsdu1.Select(p => p.c12).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.c12).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFd12")).Text = (lstsdu1.Select(p => p.d12).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.d12).Sum().ToString("#,##0.00;(#,##0.00); ");

                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFsalamt")).Text = (lstsdu1.Select(p => p.salamt).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.salamt).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFtcolamt")).Text = (lstsdu1.Select(p => p.collamt).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.collamt).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgFtretamt")).Text = (lstsdu1.Select(p => p.retamt).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.retamt).Sum().ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvSalcolDues.FooterRow.FindControl("lgvFtduesamt")).Text = (lstsdu1.Select(p => p.tduesamt).Sum() == 0.00) ? "0" : lstsdu1.Select(p => p.tduesamt).Sum().ToString("#,##0.00;(#,##0.00); ");




                    break;

            }


        }





        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "QtyBasis":
                    this.ShowSummeryQbasis();
                    break;
                case "AmtBasis":
                    this.SalSummeryAbasis();
                    break;

                case "dSaleVsColl":
                    this.ShowDailSalVsColl(); ;
                    break;

                case "SalesRegister":
                    this.ShowSaleRegister();
                    break;
                case "CollectStatus":
                    this.ShowRealCollection();
                    break;
                case "BankRecon":

                    this.ShowBankReconcillation();
                    break;
                case "SalComm":
                    this.ShowSalVsCollComm();
                    break;

                case "SerCost":
                    this.ShowSerCentCost();
                    break;
                case "ProAgaing":
                    this.ShowProAgaing();
                    break;
                case "LcCost":
                    this.ShowAllLcCost();
                    break;
                case "LcReceive":
                    this.ShowAllLcReceive();
                    break;

                case "SalColDu":
                    this.ShowSaleColDues();
                    break;






            }

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }

        private void ShowSummeryQbasis()
        {
            try
            {

                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "SALESSUM01", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvSalSummery.DataSource = null;
                    this.gvSalSummery.DataBind();
                    return;
                }

                Session["tblsalsum"] = ds1.Tables[0];

                this.gvSalSummery.Columns[2].HeaderText = (comcod.Substring(0, 1) == "2") ? "Plot" : "Shop";
                this.gvSalSummery.Columns[6].HeaderText = (comcod.Substring(0, 1) == "2") ? "B.Plot" : "B.Shop";
                this.gvSalSummery.Columns[10].HeaderText = (comcod.Substring(0, 1) == "2") ? "C.Plot" : "C.Shop";
                this.gvSalSummery.Columns[14].HeaderText = (comcod.Substring(0, 1) == "2") ? "TS.Plot" : "TS.Shop";
                this.gvSalSummery.Columns[18].HeaderText = (comcod.Substring(0, 1) == "2") ? "A.Plot" : "A.Shop";
                this.gvSalSummery.DataSource = ds1.Tables[0];
                this.gvSalSummery.DataBind();
                this.FooterCalculation(ds1.Tables[0], "gvSalSummery");


            }

            catch (Exception ex)
            {


            }
        }

        private void SalSummeryAbasis()
        {

            try
            {
                Session.Remove("tblsalsum");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "SALESSUM02", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvSalAmt.DataSource = null;
                    this.gvSalAmt.DataBind();
                    return;
                }

                Session["tblsalsum"] = ds1.Tables[0];
                this.gvSalAmt.Columns[2].HeaderText = (comcod.Substring(0, 1) == "2") ? "Plot" : "Shop";
                this.gvSalAmt.Columns[6].HeaderText = (comcod.Substring(0, 1) == "2") ? "B.Plot" : "B.Shop";
                this.gvSalAmt.Columns[10].HeaderText = (comcod.Substring(0, 1) == "2") ? "C.Plot" : "C.Shop";
                this.gvSalAmt.Columns[14].HeaderText = (comcod.Substring(0, 1) == "2") ? "TR.Plot" : "TR.Shop";
                this.gvSalAmt.Columns[18].HeaderText = (comcod.Substring(0, 1) == "2") ? "R.Plot" : "R.Shop";
                this.gvSalAmt.DataSource = ds1.Tables[0];
                this.gvSalAmt.DataBind();
                this.FooterCalculation(ds1.Tables[0], "gvSalAmt");


            }

            catch (Exception ex)
            {


            }

        }

        private void ShowDailSalVsColl()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWISESALVSCOLTAR", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvSalVsColl.DataSource = null;
                    this.gvSalVsColl.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.gvSalVsColl.DataSource = this.HiddenSameData(ds1.Tables[0]);
                this.gvSalVsColl.DataBind();




            }

            catch (Exception ex)
            {


            }



        }

        private void ShowSalVsCollComm()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_03", "RPTCUSTCOMMSION", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.grvSalesComm.DataSource = null;
                    this.grvSalesComm.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.grvSalesComm.DataSource = this.HiddenSameData(ds1.Tables[0]);
                this.grvSalesComm.DataBind();
                this.FooterCalculation(ds1.Tables[0], "gvSalComm");



            }

            catch (Exception ex)
            {


            }



        }
        private void ShowSerCentCost()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORTS_LC_INFO", "RPTSERCENTCOST", frmdate, todate, "", "", "", "", "", "", "");

                var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.SerCentrCost>();

                if (ds1 == null)
                {
                    this.gvSerCost.DataSource = null;
                    this.gvSerCost.DataBind();
                    return;
                }
                Session["tblsalsum"] = lst;

                //this.FooterCalculation(ds1.Tables[0], "gvSalComm");
                double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12, amt13, amt14, amt15, amt16, amt17, amt18, amt19, amt20, amt21, amt22, amt23,
                    amt24, amt25, amt26, amt27, amt28, amt29, amt30, amt31, amt32, amt33, amt34, amt35;


                amt1 = (lst.Select(p => p.r1).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r1).Sum();
                amt2 = (lst.Select(p => p.r2).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r2).Sum();
                amt3 = (lst.Select(p => p.r3).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r3).Sum();
                amt4 = (lst.Select(p => p.r4).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r4).Sum();
                amt5 = (lst.Select(p => p.r5).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r5).Sum();
                amt6 = (lst.Select(p => p.r6).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r6).Sum();
                amt7 = (lst.Select(p => p.r7).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r7).Sum();
                amt8 = (lst.Select(p => p.r8).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r8).Sum();
                amt9 = (lst.Select(p => p.r9).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r9).Sum();
                amt10 = (lst.Select(p => p.r10).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r10).Sum();
                amt11 = (lst.Select(p => p.r11).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r11).Sum();
                amt12 = (lst.Select(p => p.r12).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r12).Sum();
                amt13 = (lst.Select(p => p.r13).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r13).Sum();
                amt14 = (lst.Select(p => p.r14).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r14).Sum();
                amt15 = (lst.Select(p => p.r15).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r15).Sum();
                amt16 = (lst.Select(p => p.r16).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r16).Sum();
                amt17 = (lst.Select(p => p.r17).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r17).Sum();
                amt18 = (lst.Select(p => p.r18).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r18).Sum();
                amt19 = (lst.Select(p => p.r19).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r19).Sum();
                amt20 = (lst.Select(p => p.r20).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r20).Sum();
                amt21 = (lst.Select(p => p.r21).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r21).Sum();
                amt22 = (lst.Select(p => p.r22).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r22).Sum();
                amt23 = (lst.Select(p => p.r23).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r23).Sum();
                amt24 = (lst.Select(p => p.r24).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r24).Sum();
                amt25 = (lst.Select(p => p.r25).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r25).Sum();
                amt26 = (lst.Select(p => p.r26).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r26).Sum();
                amt27 = (lst.Select(p => p.r27).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r27).Sum();
                amt28 = (lst.Select(p => p.r28).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r28).Sum();
                amt29 = (lst.Select(p => p.r29).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r29).Sum();
                amt30 = (lst.Select(p => p.r30).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r30).Sum();
                amt31 = (lst.Select(p => p.r31).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r31).Sum();
                amt32 = (lst.Select(p => p.r32).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r32).Sum();
                amt33 = (lst.Select(p => p.r33).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r33).Sum();
                amt34 = (lst.Select(p => p.r34).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r34).Sum();
                amt35 = (lst.Select(p => p.r35).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r35).Sum();



                this.gvSerCost.Columns[4].Visible = (amt1 != 0);
                this.gvSerCost.Columns[5].Visible = (amt2 != 0);
                this.gvSerCost.Columns[6].Visible = (amt3 != 0);
                this.gvSerCost.Columns[7].Visible = (amt4 != 0);
                this.gvSerCost.Columns[8].Visible = (amt5 != 0);
                this.gvSerCost.Columns[9].Visible = (amt6 != 0);
                this.gvSerCost.Columns[10].Visible = (amt7 != 0);
                this.gvSerCost.Columns[11].Visible = (amt8 != 0);
                this.gvSerCost.Columns[12].Visible = (amt9 != 0);
                this.gvSerCost.Columns[13].Visible = (amt10 != 0);
                this.gvSerCost.Columns[14].Visible = (amt11 != 0);
                this.gvSerCost.Columns[15].Visible = (amt12 != 0);
                this.gvSerCost.Columns[16].Visible = (amt13 != 0);
                this.gvSerCost.Columns[17].Visible = (amt14 != 0);
                this.gvSerCost.Columns[18].Visible = (amt15 != 0);
                this.gvSerCost.Columns[19].Visible = (amt16 != 0);
                this.gvSerCost.Columns[20].Visible = (amt17 != 0);
                this.gvSerCost.Columns[21].Visible = (amt18 != 0);
                this.gvSerCost.Columns[22].Visible = (amt19 != 0);
                this.gvSerCost.Columns[23].Visible = (amt20 != 0);
                this.gvSerCost.Columns[24].Visible = (amt21 != 0);
                this.gvSerCost.Columns[25].Visible = (amt22 != 0);
                this.gvSerCost.Columns[26].Visible = (amt23 != 0);
                this.gvSerCost.Columns[27].Visible = (amt24 != 0);
                this.gvSerCost.Columns[28].Visible = (amt25 != 0);
                this.gvSerCost.Columns[29].Visible = (amt26 != 0);
                this.gvSerCost.Columns[30].Visible = (amt27 != 0);
                this.gvSerCost.Columns[31].Visible = (amt28 != 0);
                this.gvSerCost.Columns[31].Visible = (amt29 != 0);
                this.gvSerCost.Columns[33].Visible = (amt30 != 0);
                this.gvSerCost.Columns[34].Visible = (amt31 != 0);
                this.gvSerCost.Columns[35].Visible = (amt32 != 0);
                this.gvSerCost.Columns[36].Visible = (amt33 != 0);
                this.gvSerCost.Columns[37].Visible = (amt34 != 0);
                this.gvSerCost.Columns[38].Visible = (amt35 != 0);


                int rowcount = ds1.Tables[1].Rows.Count;
                int j = 1;


                for (int i = 4; i < 39; i++)
                {
                    if (rowcount < j)
                        break;
                    //if (datefrm > dateto)
                    //    break;
                    this.gvSerCost.Columns[i].HeaderText = ds1.Tables[1].Rows[i - 4]["resdesc"].ToString();


                    j++;
                    //datefrm = datefrm.AddMonths(1);
                }
                this.gvSerCost.DataSource = lst;
                this.gvSerCost.DataBind();



                if (lst.Count == 0)
                    return;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((HyperLink)this.gvSerCost.HeaderRow.FindControl("hlbtnCBdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                Session["Report1"] = gvSerCost;
                if (dr1.Length > 0)
                    ((HyperLink)this.gvSerCost.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

            catch (Exception ex)
            {


            }
        }

        private void ShowProAgaing()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORTS_LC_INFO", "RPTAGINGSTATUS", frmdate, todate, "", "", "", "", "", "", "");

                var lst1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ProAgaing>();

                if (ds1 == null)
                {
                    this.gvProAging.DataSource = null;
                    this.gvProAging.DataBind();
                    return;
                }
                Session["tblsalsum"] = lst1;
                this.Data_Bind();


                if (lst1.Count == 0)
                    return;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((HyperLink)this.gvProAging.HeaderRow.FindControl("hlbtnSalExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                Session["Report1"] = gvProAging;
                if (dr1.Length > 0)
                    ((HyperLink)this.gvProAging.HeaderRow.FindControl("hlbtnSalExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                //this.FooterCalculation(ds1.Tables[0], "gvSalComm");



            }
            catch (Exception ex)
            {

            }

        }


        private void ShowAllLcReceive()
        {
            Session.Remove("tblsalsum");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string actcode = (this.ddllctypelist.SelectedValue == "000000000000") ? "%" : this.ddllctypelist.SelectedValue.ToString() + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORTS_LC_INFO", "RPTALLLCCOST02", frmdate, todate, actcode, "", "", "", "", "", "");

            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_09_Commer.LCReceived01>();

            if (ds1 == null)
                return;

            this.GridLCReceived.DataSource = lst;
            this.GridLCReceived.DataBind();

            Session["tblsalsum"] = lst;
            this.FooterCalculation(ds1.Tables[0], "gvLCReceived");

            if (lst.Count == 0)
                return;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

            ((HyperLink)this.GridLCReceived.HeaderRow.FindControl("hlbtnSalExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            Session["Report1"] = GridLCReceived;
            if (dr1.Length > 0)
                ((HyperLink)this.GridLCReceived.HeaderRow.FindControl("hlbtnSalExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }



        private void ShowAllLcCost()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string actcode = (this.ddllctypelist.SelectedValue == "000000000000") ? "%" : this.ddllctypelist.SelectedValue.ToString() + "%";
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORTS_LC_INFO", "RPTALLLCCOST", frmdate, todate, actcode, "", "", "", "", "", "");

                var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.AlllcCost>();

                if (ds1 == null)
                {
                    this.gvAllLccost.DataSource = null;
                    this.gvAllLccost.DataBind();
                    return;
                }
                Session["tblsalsum"] = lst;

                //this.FooterCalculation(ds1.Tables[0], "gvSalComm");
                double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12, amt13, amt14, amt15, amt16, amt17, amt18, amt19, amt20, amt21, amt22, amt23,
                    amt24, amt25, amt26, amt27, amt28, amt29, amt30, amt31, amt32, amt33, amt34, amt35;


                amt1 = (lst.Select(p => p.r1).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r1).Sum();
                amt2 = (lst.Select(p => p.r2).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r2).Sum();
                amt3 = (lst.Select(p => p.r3).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r3).Sum();
                amt4 = (lst.Select(p => p.r4).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r4).Sum();
                amt5 = (lst.Select(p => p.r5).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r5).Sum();
                amt6 = (lst.Select(p => p.r6).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r6).Sum();
                amt7 = (lst.Select(p => p.r7).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r7).Sum();
                amt8 = (lst.Select(p => p.r8).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r8).Sum();
                amt9 = (lst.Select(p => p.r9).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r9).Sum();
                amt10 = (lst.Select(p => p.r10).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r10).Sum();
                amt11 = (lst.Select(p => p.r11).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r11).Sum();
                amt12 = (lst.Select(p => p.r12).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r12).Sum();
                amt13 = (lst.Select(p => p.r13).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r13).Sum();
                amt14 = (lst.Select(p => p.r14).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r14).Sum();
                amt15 = (lst.Select(p => p.r15).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r15).Sum();
                amt16 = (lst.Select(p => p.r16).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r16).Sum();
                amt17 = (lst.Select(p => p.r17).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r17).Sum();
                amt18 = (lst.Select(p => p.r18).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r18).Sum();
                amt19 = (lst.Select(p => p.r19).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r19).Sum();
                amt20 = (lst.Select(p => p.r20).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r20).Sum();
                amt21 = (lst.Select(p => p.r21).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r21).Sum();
                amt22 = (lst.Select(p => p.r22).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r22).Sum();
                amt23 = (lst.Select(p => p.r23).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r23).Sum();
                amt24 = (lst.Select(p => p.r24).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r24).Sum();
                amt25 = (lst.Select(p => p.r25).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r25).Sum();
                amt26 = (lst.Select(p => p.r26).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r26).Sum();
                amt27 = (lst.Select(p => p.r27).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r27).Sum();
                amt28 = (lst.Select(p => p.r28).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r28).Sum();
                amt29 = (lst.Select(p => p.r29).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r29).Sum();
                amt30 = (lst.Select(p => p.r30).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r30).Sum();
                amt31 = (lst.Select(p => p.r31).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r31).Sum();
                amt32 = (lst.Select(p => p.r32).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r32).Sum();
                amt33 = (lst.Select(p => p.r33).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r33).Sum();
                amt34 = (lst.Select(p => p.r34).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r34).Sum();
                amt35 = (lst.Select(p => p.r35).Sum() == 0.00) ? 0.00 : lst.Select(p => p.r35).Sum();



                this.gvAllLccost.Columns[4].Visible = (amt1 != 0);
                this.gvAllLccost.Columns[5].Visible = (amt2 != 0);
                this.gvAllLccost.Columns[6].Visible = (amt3 != 0);
                this.gvAllLccost.Columns[7].Visible = (amt4 != 0);
                this.gvAllLccost.Columns[8].Visible = (amt5 != 0);
                this.gvAllLccost.Columns[9].Visible = (amt6 != 0);
                this.gvAllLccost.Columns[10].Visible = (amt7 != 0);
                this.gvAllLccost.Columns[11].Visible = (amt8 != 0);
                this.gvAllLccost.Columns[12].Visible = (amt9 != 0);
                this.gvAllLccost.Columns[13].Visible = (amt10 != 0);
                this.gvAllLccost.Columns[14].Visible = (amt11 != 0);
                this.gvAllLccost.Columns[15].Visible = (amt12 != 0);
                this.gvAllLccost.Columns[16].Visible = (amt13 != 0);
                this.gvAllLccost.Columns[17].Visible = (amt14 != 0);
                this.gvAllLccost.Columns[18].Visible = (amt15 != 0);
                this.gvAllLccost.Columns[19].Visible = (amt16 != 0);
                this.gvAllLccost.Columns[20].Visible = (amt17 != 0);
                this.gvAllLccost.Columns[21].Visible = (amt18 != 0);
                this.gvAllLccost.Columns[22].Visible = (amt19 != 0);
                this.gvAllLccost.Columns[23].Visible = (amt20 != 0);
                this.gvAllLccost.Columns[24].Visible = (amt21 != 0);
                this.gvAllLccost.Columns[25].Visible = (amt22 != 0);
                this.gvAllLccost.Columns[26].Visible = (amt23 != 0);
                this.gvAllLccost.Columns[27].Visible = (amt24 != 0);
                this.gvAllLccost.Columns[28].Visible = (amt25 != 0);
                this.gvAllLccost.Columns[29].Visible = (amt26 != 0);
                this.gvAllLccost.Columns[30].Visible = (amt27 != 0);
                this.gvAllLccost.Columns[31].Visible = (amt28 != 0);
                this.gvAllLccost.Columns[31].Visible = (amt29 != 0);
                this.gvAllLccost.Columns[33].Visible = (amt30 != 0);
                this.gvAllLccost.Columns[34].Visible = (amt31 != 0);
                this.gvAllLccost.Columns[35].Visible = (amt32 != 0);
                this.gvAllLccost.Columns[36].Visible = (amt33 != 0);
                this.gvAllLccost.Columns[37].Visible = (amt34 != 0);
                this.gvAllLccost.Columns[38].Visible = (amt35 != 0);


                int rowcount = ds1.Tables[1].Rows.Count;
                int j = 1;


                for (int i = 4; i < 39; i++)
                {
                    if (rowcount < j)
                        break;
                    //if (datefrm > dateto)
                    //    break;
                    this.gvAllLccost.Columns[i].HeaderText = ds1.Tables[1].Rows[i - 4]["resdesc"].ToString();
                    j++;
                    //datefrm = datefrm.AddMonths(1);
                }
                this.gvAllLccost.DataSource = lst;
                this.gvAllLccost.DataBind();
                if (lst.Count == 0)
                    return;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((HyperLink)this.gvAllLccost.HeaderRow.FindControl("hlbtnCBdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                Session["Report1"] = gvAllLccost;
                if (dr1.Length > 0)
                    ((HyperLink)this.gvAllLccost.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

            catch (Exception ex)
            {


            }
        }



        private void ShowSaleColDues()
        {
            try
            {
                ViewState.Remove("tblsalcoldues");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string circode = (this.ddlcircle.SelectedValue.Trim().ToString() == "00000") ? "%" : this.ddlcircle.SelectedValue.Trim().ToString() + "%";
                string region = (this.ddlRegi.SelectedValue.Trim().ToString() == "00000") ? "%" : this.ddlRegi.SelectedValue.Trim().ToString() + "%";
                string area = (this.ddlArea.SelectedValue.Trim().ToString() == "00000") ? "%" : this.ddlArea.SelectedValue.Trim().ToString() + "%";

                string territory = (this.ddlterri.SelectedValue.Trim().ToString() == "00000") ? "%" : this.ddlterri.SelectedValue.Trim().ToString() + "%";

                string EmpType = this.rbtnSelect.SelectedIndex.ToString();

                DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO_02", "RPTSALCOLDUES", frmdate, todate, circode, region, area, territory, EmpType, "");

                var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.SalColDues>();

                if (ds1 == null)
                {
                    this.gvSalcolDues.DataSource = null;
                    this.gvSalcolDues.DataBind();
                    return;
                }

                ViewState["tblsalcoldues"] = lst;
                this.Data_Bind();
                //this.FooterCalculation(ds1.Tables[0], "gvSalcolDues");

            }
            catch (Exception ex)
            {

            }


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string mdeptcode = dt1.Rows[0]["mdeptcode"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mdeptcode"].ToString() == mdeptcode)
                    dt1.Rows[j]["mdeptname"] = "";

                mdeptcode = dt1.Rows[j]["mdeptcode"].ToString();

            }
            return dt1;

        }



        private void ShowSaleRegister()
        {

            try
            {
                Session.Remove("tblsalsum");
                Session.Remove("tblSalereg");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWISESALEREGISTER", frmdate, todate, "", "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvSalReg.DataSource = null;
                    this.gvSalReg.DataBind();
                    this.gvTransSum.DataSource = null;
                    this.gvTransSum.DataBind();
                    return;


                }
                Session["tblsalsum"] = ds1.Tables[0];
                Session["tblSalereg"] = ds1.Tables[1];
                this.gvSalReg.DataSource = ds1.Tables[0];
                this.gvSalReg.DataBind();

                ((Label)this.gvSalReg.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(saleamt)", "")) ?
                         0 : ds1.Tables[0].Compute("sum(saleamt)", ""))).ToString("#,##0;(#,##0); ");


                DataTable dt = ds1.Tables[2];
                for (int i = 0; i < dt.Rows.Count; i++)
                    this.gvTransSum.Columns[i].HeaderText = dt.Rows[i]["deptname"].ToString();

                this.gvTransSum.DataSource = ds1.Tables[1];
                this.gvTransSum.DataBind();



            }

            catch (Exception ex)
            {


            }



        }


        private void ShowRealCollection()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string withrep = (this.chkwithoutrep.Checked ? "without" : "");

                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWCOLLECTSTATUS", frmdate, todate, withrep, "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvrcoll.DataSource = null;
                    this.gvrcoll.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.gvrcoll.DataSource = ds1.Tables[0];
                this.gvrcoll.DataBind();


            }

            catch (Exception ex)
            {


            }


        }

        private void ShowCollectionStatus()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWTARVSCOLLECTION", frmdate, todate, "", "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvrcoll.DataSource = null;
                    this.gvrcoll.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.gvrcoll.DataSource = ds1.Tables[0];
                this.gvrcoll.DataBind();


            }

            catch (Exception ex)
            {


            }




        }
        private void ShowBankReconcillation()
        {
            try
            {
                Session.Remove("tblsalsum");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWRECCOLLSTATUS", frmdate, todate, "", "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvbrecon.DataSource = null;
                    this.gvbrecon.DataBind();
                    return;
                }
                Session["tblsalsum"] = ds1.Tables[0];
                this.gvbrecon.DataSource = ds1.Tables[0];
                this.gvbrecon.DataBind();


            }
            catch (Exception ex)
            {

            }



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "QtyBasis":
                    //this.MultiView1.ActiveViewIndex = 0;
                    this.PrintSaleSummeryQbasis();
                    break;
                case "AmtBasis":
                    this.PrintSaleSummeryAmtbasis();
                    break;

                case "dSaleVsColl":
                    this.PrintDailSalVsColl(); ;
                    break;

                case "SalesRegister":
                    this.PrintSaleRegister();
                    break;
                case "CollectStatus":
                    this.PrintCollection();
                    break;
                case "SalComm":

                    break;
                case "SerCost":

                    break;
                case "ProAgaing":
                    this.PrintProdAgaing();
                    break;
                case "LcReceive":
                    this.LcReceivePrint();
                    break;


            }


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales Sumarry";
                string eventdesc = "Print Report";
                string eventdesc2 = mRepID;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        private void PrintSaleSummeryQbasis()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblsalsum"];
            //ReportDocument rptsale = new RMGiRPT.R_23_SaM.rptSalSumary();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject rptbDate = rptsale.ReportDefinition.ReportObjects["bdate"] as TextObject;
            //rptbDate.Text = "(As On " + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy") + ")";
            //TextObject rptbetDate = rptsale.ReportDefinition.ReportObjects["betdate"] as TextObject;
            //rptbetDate.Text = "(" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
            //TextObject rpatDate = rptsale.ReportDefinition.ReportObjects["adate"] as TextObject;
            //rpatDate.Text = "(As On " + Convert.ToDateTime(this.txttodate.Text).AddDays(1).ToString("dd-MMM-yyyy") + ")";

            //string shoporplot = (comcod.Substring(0, 1) == "2") ? "Plot" : "Shop";
            //TextObject tshop = rptsale.ReportDefinition.ReportObjects["tshop"] as TextObject;
            //tshop.Text = shoporplot;
            //TextObject bshop = rptsale.ReportDefinition.ReportObjects["bshop"] as TextObject;
            //bshop.Text = shoporplot;
            //TextObject cshop = rptsale.ReportDefinition.ReportObjects["cshop"] as TextObject;
            //cshop.Text = shoporplot;
            //TextObject tsshop = rptsale.ReportDefinition.ReportObjects["tsshop"] as TextObject;
            //tsshop.Text = shoporplot;
            //TextObject ashop = rptsale.ReportDefinition.ReportObjects["ashop"] as TextObject;
            //ashop.Text = shoporplot;


            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintSaleSummeryAmtbasis()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblsalsum"];
            //ReportDocument rptsale = new RMGiRPT.R_23_SaM.rptSalSumAmtBasis();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //TextObject rptbDate = rptsale.ReportDefinition.ReportObjects["bdate"] as TextObject;
            //rptbDate.Text = "(As On " + Convert.ToDateTime(this.txtfromdate.Text).AddDays(-1).ToString("dd-MMM-yyyy") + ")";
            //TextObject rptbetDate = rptsale.ReportDefinition.ReportObjects["betdate"] as TextObject;
            //rptbetDate.Text = "(" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
            //TextObject rpatDate = rptsale.ReportDefinition.ReportObjects["adate"] as TextObject;
            //rpatDate.Text = "(As On " + Convert.ToDateTime(this.txttodate.Text).AddDays(1).ToString("dd-MMM-yyyy") + ")";

            //string shoporplot = (comcod.Substring(0, 1) == "2") ? "Plot" : "Shop";
            //TextObject tshop = rptsale.ReportDefinition.ReportObjects["tshop"] as TextObject;
            //tshop.Text = shoporplot;
            //TextObject bshop = rptsale.ReportDefinition.ReportObjects["bshop"] as TextObject;
            //bshop.Text = shoporplot;
            //TextObject cshop = rptsale.ReportDefinition.ReportObjects["cshop"] as TextObject;
            //cshop.Text = shoporplot;
            //TextObject tsshop = rptsale.ReportDefinition.ReportObjects["tsshop"] as TextObject;
            //tsshop.Text = shoporplot;
            //TextObject ashop = rptsale.ReportDefinition.ReportObjects["rshop"] as TextObject;
            //ashop.Text = shoporplot;



            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintProdAgaing()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();  //company name
            //string comadd = hst["comadd1"].ToString();  //address
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string CurDate1 = "Date: From: " + frmdate + "  To: " + todate;

            //var lst = (List<SPEENTITY.C_22_Sal.Sales_BO.ProAgaing>)Session["tblsalsum"];

            //LocalReport rpt1 = new LocalReport();

            //rpt1 = RMGiRPTRDLC.RptSetupClass1.GetLocalReport("RD_23_SaM.RptSalSummProdAgaing", lst, null, null);
            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "PRODUCTS AGAING REPORTS"));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //Session["Report1"] = rpt1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void LcReceivePrint()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();  //company name
            //string comadd = hst["comadd1"].ToString();  //address
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string CurDate1 = "Date: From: " + frmdate + "  To: " + todate;

            //// var lst = Session["tblsalsum"];

            //var lst = (List<SPEENTITY.C_09_Commer.LCReceived01>)Session["tblsalsum"];

            //LocalReport rpt1 = new LocalReport();
            //rpt1 = RMGiRPTRDLC.RptSetupClass1.GetLocalReport("RD_09_LCM.RptLCReceived", lst, null, null);
            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("CurDate1", CurDate1));
            //rpt1.SetParameters(new ReportParameter("RptTitle", "L/C RECEIVE REPORT"));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintDailSalVsColl()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblsalsum"];
            //ReportDocument rptsale = new RMGiRPT.R_23_SaM.rptDailySaleVsCollTarget();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptDate.Text = "( From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";

            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintSaleRegister()
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comcod = hst["comcod"].ToString();
            // string comnam = hst["comnam"].ToString();
            // string comadd = hst["comadd1"].ToString();
            // string compname = hst["compname"].ToString();
            // string username = hst["username"].ToString();
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // DataTable dt = (DataTable)Session["tblsalsum"];
            // DataTable dt1 = (DataTable)Session["tblSalereg"];

            // ReportDocument rptsale = new RMGiRPT.R_23_SaM.rptSaleRegisterSummary();




            // TextObject rptCname = rptsale.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            // rptCname.Text = comnam;
            // TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            // rptDate.Text = "( From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";



            //for (int i = 0; i < this.gvTransSum.Columns.Count ; i++)
            //{
            //    TextObject rpttxth = rptsale.Subreports["RptSubSaleRegister.rpt"].ReportDefinition.ReportObjects["txtamt" + (i + 1).ToString()] as TextObject;
            //    rpttxth.Text = this.gvTransSum.Columns[i].HeaderText.ToString().Trim();


            //    //TextObject rpttxth = rptsubsale.ReportDefinition.ReportObjects["txtamt" + (i + 1).ToString()] as TextObject;
            //    //rpttxth.Text = this.gvTransSum.Columns[i].HeaderText.ToString().Trim();
            //}


            // TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            // txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            // rptsale.Subreports["RptSubSaleRegister.rpt"].SetDataSource(dt1);
            // rptsale.SetDataSource(dt);
            // Session["Report1"] = rptsale;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintCollection()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblsalsum"];
            //ReportDocument rptsale = new RMGiRPT.R_23_SaM.rptDWiseRealCollection();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptDate.Text = "( From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";

            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        protected void gvSalVsColl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartment");
                Label lgvmonsalamt = (Label)e.Row.FindControl("lgvmonsalamt");
                Label lgvmoncollamt = (Label)e.Row.FindControl("lgvmoncollamt");
                Label lgvtsalamt = (Label)e.Row.FindControl("lgvtsalamt");
                Label lgvtcollamt = (Label)e.Row.FindControl("lgvtcollamt");
                Label lgvuatsalamt = (Label)e.Row.FindControl("lgvuatsalamt");
                Label lgvtatsaleamt = (Label)e.Row.FindControl("lgvtatsaleamt");
                Label lgvuatcollamt = (Label)e.Row.FindControl("lgvuatcollamt");
                Label lgvtatcollamt = (Label)e.Row.FindControl("lgvtatcollamt");
                Label lgvpmonsalamt = (Label)e.Row.FindControl("lgvpmonsalamt");
                Label lgvpmoncollamt = (Label)e.Row.FindControl("lgvpmoncollamt");
                Label lgvperontsale = (Label)e.Row.FindControl("lgvperontsale");
                Label lgvperontcoll = (Label)e.Row.FindControl("lgvperontcoll");
                Label lgvsalsfall = (Label)e.Row.FindControl("lgvsalsfall");
                Label lgvcollsfall = (Label)e.Row.FindControl("lgvcollsfall");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {

                    Deptdesc.Font.Bold = true;
                    lgvmonsalamt.Font.Bold = true;
                    lgvmoncollamt.Font.Bold = true;
                    lgvtsalamt.Font.Bold = true;
                    lgvtcollamt.Font.Bold = true;
                    lgvtatsaleamt.Font.Bold = true;
                    lgvtatcollamt.Font.Bold = true;
                    lgvuatsalamt.Font.Bold = true;
                    lgvuatcollamt.Font.Bold = true;
                    lgvpmonsalamt.Font.Bold = true;
                    lgvpmoncollamt.Font.Bold = true;
                    lgvperontsale.Font.Bold = true;
                    lgvperontcoll.Font.Bold = true;
                    lgvsalsfall.Font.Bold = true;
                    lgvcollsfall.Font.Bold = true;

                    Deptdesc.Style.Add("text-align", "right");


                }

            }
        }
        protected void gvrcoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartmentrc");
                Label lgvtocollection = (Label)e.Row.FindControl("lgvtocollection");
                Label lgvinhfchq = (Label)e.Row.FindControl("lgvinhfchq");
                Label lgvinhrchq = (Label)e.Row.FindControl("lgvinhrchq");
                Label lgvchqdep = (Label)e.Row.FindControl("lgvchqdep");
                Label lgvreconamt = (Label)e.Row.FindControl("lgvreconamt");
                Label lgvinhpchq = (Label)e.Row.FindControl("lgvinhpchq");
                Label lgvrepchq = (Label)e.Row.FindControl("lgvrepchq");
                Label lgvncollamt = (Label)e.Row.FindControl("lgvncollamt");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {

                    Deptdesc.Font.Bold = true;
                    lgvtocollection.Font.Bold = true;
                    lgvinhfchq.Font.Bold = true;
                    lgvinhrchq.Font.Bold = true;
                    lgvchqdep.Font.Bold = true;
                    lgvreconamt.Font.Bold = true;
                    lgvinhpchq.Font.Bold = true;
                    lgvrepchq.Font.Bold = true;
                    lgvncollamt.Font.Bold = true;

                    Deptdesc.Style.Add("text-align", "right");


                }

            }
        }
        protected void gvCollSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvbrecon_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartmentbrec");
                Label lgvreconamt = (Label)e.Row.FindControl("lgvreconamtbrec");
                Label lgvadjsutment = (Label)e.Row.FindControl("lgvadjsutment");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {

                    Deptdesc.Font.Bold = true;
                    lgvreconamt.Font.Bold = true;
                    lgvadjsutment.Font.Bold = true;
                    Deptdesc.Style.Add("text-align", "right");


                }

            }
        }
        protected void grvSalesComm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartment");
                Label lgvmonsalamt = (Label)e.Row.FindControl("lgvmonsalamt");
                Label lgvtsalamt = (Label)e.Row.FindControl("lgvtsalamt");
                Label lgvperontsale = (Label)e.Row.FindControl("lgvperontsale");
                Label lgvtatsaleamt = (Label)e.Row.FindControl("lgvtatsaleamt");
                Label lgvseprationsale = (Label)e.Row.FindControl("lgvseprationsale");
                Label lgvmoncollamt = (Label)e.Row.FindControl("lgvmoncollamt");
                Label lgvperontcoll = (Label)e.Row.FindControl("lgvperontcoll");
                Label lgvcommp = (Label)e.Row.FindControl("lgvcommp");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {

                    Deptdesc.Font.Bold = true;
                    lgvmonsalamt.Font.Bold = true;
                    lgvtsalamt.Font.Bold = true;
                    lgvperontsale.Font.Bold = true;
                    lgvperontsale.Font.Bold = true;
                    lgvtatsaleamt.Font.Bold = true;
                    lgvseprationsale.Font.Bold = true;
                    lgvmoncollamt.Font.Bold = true;
                    lgvperontcoll.Font.Bold = true;
                    lgvcommp.Font.Bold = true;
                    Deptdesc.Style.Add("text-align", "right");


                }

            }
        }
        protected void gvProAging_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProAging.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvSalcolDues_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                //string pstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pstatus")).ToString();
                Label code = (Label)e.Row.FindControl("lblgvcode");
                Label staff = (Label)e.Row.FindControl("lblgvstaff");
                Label headquater = (Label)e.Row.FindControl("lgvheadquater");
                Label desig = (Label)e.Row.FindControl("lgvdesig");
                Label joindate = (Label)e.Row.FindControl("lgvjoindate");
                //Label s1 = (Label)e.Row.FindControl("lgvs1");
                //Label c1 = (Label)e.Row.FindControl("lgvc1");
                //Label d1 = (Label)e.Row.FindControl("lgvd1");


                if (grp == "B")
                {
                    code.Font.Bold = true;
                    code.Style.Add("color", "blue");
                    staff.Font.Bold = true;
                    staff.Style.Add("color", "blue");
                    headquater.Font.Bold = true;
                    headquater.Style.Add("color", "blue");
                    desig.Font.Bold = true;
                    desig.Style.Add("color", "blue");
                    joindate.Font.Bold = true;
                    joindate.Style.Add("color", "blue");

                }


            }
        }
    }
}