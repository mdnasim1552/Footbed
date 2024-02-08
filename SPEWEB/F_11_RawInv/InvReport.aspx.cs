using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_11_RawInv
{
    public partial class InvReport : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Final Accounts Reports View/Print Screen
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetProjectName();
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Central Store";
                this.GetCodeBookList();
            }
            string type = this.Request.QueryString["InputType"].ToString();
          
         ((Label)this.Master.FindControl("lblTitle")).Text = (type == "QuantityB") ? "Inventory Report-Quantity Basis" :
                    (type == "AmountB") ? "Inventory Report-Amount Basis":(type == "MatUnused") ? "Periodic Material Unused Report": (type == "OrdwiseStk") ?"Article Wise Material Stock": "Central Store Inventory Report";

            if(type== "MatUnused")
            {
                this.Label13.Visible = false;
                this.divDdlAccProject.Visible = false;
                this.lblRptGroup.Visible = false;
                this.ddlRptGroup.Visible = false;
                this.DaySize.Visible = true;
            }
            else if (type == "OrdwiseStk")
            {
                this.Label13.Text = "Order";
                //this.ddlAccProject.Visible = false;
                this.lblRptGroup.Visible = false;
                this.ddlRptGroup.Visible = false;
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            
            
            string Type = this.Request.QueryString["InputType"].ToString();
            switch (Type)
            {
                case "General":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetDataForProjectReport();
                    break;
                case "QuantityB":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetInvQB();
                    break;
                case "AmountB":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.GetAmtInvB();
                    break;
                case "MatUnused":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.GetMaterialUnusedRpt();
                    break;
                case "OrdwiseStk":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.GetOrderWiseStckRpt();
                    break;
            }

        }

        private void GetDataForProjectReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = "";// this.ddlAccProject.SelectedValue.ToString();
            string mathead = this.ddlCodeBook.SelectedValue == "040000000000" ? "%%" :  this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            string group = this.ddlGroup.SelectedValue == "040000000000" ? "%%" : this.ddlGroup.SelectedValue.ToString().Substring(0, 7) + "%";
            group = group == "0000000%" ? "%%" : group;
            string newzero = this.CbNotZero.Checked == true ? "NOTZERO" : "";
            string type = this.ddlCodeBookSegment.SelectedValue.ToString();

            foreach (ListItem item in ddlAccProject.Items)
            {
                if (item.Selected)
                {

                    actcode += item.Value;
                }
            }
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            //GENARALINVRPT
            //DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GENARALINVRPT", date1, date2, TopHead, actcode, "", mRptGroup, mathead, group, newzero, type);
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void GetInvQB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = "";// this.ddlAccProject.SelectedValue.ToString();
            string mathead = this.ddlCodeBook.SelectedValue == "040000000000" ? "%%" : this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            string group = this.ddlGroup.SelectedValue == "040000000000" ? "%%" : this.ddlGroup.SelectedValue.ToString().Substring(0, 7) + "%";
            foreach (ListItem item in ddlAccProject.Items)
            {
                if (item.Selected)
                {

                    actcode += item.Value;
                }
            }
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GENARALINVRPT", date1, date2, TopHead, actcode, "", mRptGroup, mathead, group, "");
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void GetAmtInvB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = "";// this.ddlAccProject.SelectedValue.ToString();
            string mathead = this.ddlCodeBook.SelectedValue == "040000000000" ? "%%" : this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            string group = this.ddlGroup.SelectedValue == "040000000000" ? "%%" : this.ddlGroup.SelectedValue.ToString().Substring(0, 7) + "%";
            foreach (ListItem item in ddlAccProject.Items)
            {
                if (item.Selected)
                {

                    actcode += item.Value;
                }
            }
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            // DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTCENTRALSTORE", date1, date2, TopHead, actcode, "", mRptGroup, "", "", "");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GENARALINVRPT", date1, date2, TopHead, actcode, "", mRptGroup, mathead, group, "");
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void GetMaterialUnusedRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string mathead = this.ddlCodeBook.SelectedValue == "040000000000" ? "%%" : this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            string group = this.ddlGroup.SelectedValue == "040000000000" ? "%%" : this.ddlGroup.SelectedValue.ToString().Substring(0, 7) + "%";
            string type = this.ddlCodeBookSegment.SelectedValue.ToString();
            string days = this.ddlDaySize.SelectedValue.ToString();

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_STOCK_MANAGEMENT", "MATERIAL_UNUSED_DURATION_REPORT", date1, date2, mathead, group, type, days);
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private void GetOrderWiseStckRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 =Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string date2 = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string mathead = this.ddlCodeBook.SelectedValue == "040000000000" ? "%%" : this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            string group = this.ddlGroup.SelectedValue == "040000000000" ? "%%" : this.ddlGroup.SelectedValue.ToString().Substring(0, 7) + "%";
            string actcode = "";
            foreach (ListItem item in ddlAccProject.Items)
            {
                if (item.Selected)
                {

                    actcode += item.Value;
                }
            }
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_MASTERLC", "GET_ORDER_WISE_STOCK_RPT", date1, date2,  actcode, mathead, group, "");
            Session["tblVeiw"] = ds2.Tables[0];
            this.Data_Bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string subcode = dt1.Rows[0]["subcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["subcode"].ToString() == subcode)
                {
                    subcode = dt1.Rows[j]["subcode"].ToString();
                    dt1.Rows[j]["subdesc1"] = "";
                }
                else
                    subcode = dt1.Rows[j]["subcode"].ToString();
            }
            return dt1;
        }
        private void Data_Bind()
        {
            string Type = this.Request.QueryString["InputType"].ToString();
            switch (Type)
            {
                case "General":
                    this.gvCenStore.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCenStore.DataSource = (DataTable)Session["tblVeiw"];// HiddenSameData((DataTable)Session["tblVeiw"]);
                    this.gvCenStore.DataBind();
                    break;
                case "QuantityB":
                    this.gvQBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvQBasis.DataSource = (DataTable)Session["tblVeiw"];// HiddenSameData((DataTable)Session["tblVeiw"]);
                    this.gvQBasis.DataBind();
                    break;
                case "AmountB":
                    this.gvAmtBasis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvAmtBasis.DataSource = (DataTable)Session["tblVeiw"];// HiddenSameData((DataTable)Session["tblVeiw"]);
                    this.gvAmtBasis.DataBind();
                    break;
                case "MatUnused":
                    this.gvmatunused.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvmatunused.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvmatunused.DataBind();
                    break;
                case "OrdwiseStk":
                    this.gvOrdewiseStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvOrdewiseStock.DataSource = (DataTable)Session["tblVeiw"];
                    this.gvOrdewiseStock.DataBind();
                    break;

            }

            this.FooterCalculation((DataTable)Session["tblVeiw"]);
        }

        //private DataTable HiddenSameDate(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return dt1;
        //    string pactcode = dt1.Rows[0]["actcode"].ToString();
        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["actcode"].ToString() == pactcode)
        //        {
        //            pactcode = dt1.Rows[j]["actcode"].ToString();
        //            dt1.Rows[j]["actdesc"] = "";
        //        }

        //        else
        //        {
        //            pactcode = dt1.Rows[j]["actcode"].ToString();
        //        }

        //    }
        //    return dt1;

        //}


        protected void ddlAccProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetMaterial();
        }



        protected void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string HeaderCode = "1[15]%";
            string filter = "%";
            if(this.Request.QueryString["InputType"]== "OrdwiseStk")
            {
                 HeaderCode = "16%";
            }
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", HeaderCode, filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            
            this.ddlAccProject.DataSource = dt1;
            this.ddlAccProject.DataTextField = "actdesc1";
            this.ddlAccProject.DataValueField = "actcode";
            this.ddlAccProject.DataBind();

        }
        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
            string Type = this.Request.QueryString["InputType"].ToString();
            switch (Type)
            {
                case "General":
                    ((Label)this.gvCenStore.FooterRow.FindControl("lblgvOpQtyttl")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnqty)", "")) ?
                        0.00 : dt.Compute("Sum(opnqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvFOpnAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvFRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recam)", "")) ?
                        0.00 : dt.Compute("Sum(recam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvFtrnsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                        0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvFIssAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(matisamt)", "")) ?
                        0.00 : dt.Compute("Sum(matisamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lgvFStkAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stcamt)", "")) ?
                        0.00 : dt.Compute("Sum(stcamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lblgvIssQtyttl")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(matisqty)", "")) ?
                        0.00 : dt.Compute("Sum(matisqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lblgvTrQtyTTL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnqty)", "")) ?
                        0.00 : dt.Compute("Sum(trnqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lblgvtrninqtyQtyTTL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trninqty)", "")) ?
                        0.00 : dt.Compute("Sum(trninqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lblgvReQtyTTL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recqty)", "")) ?
                        0.00 : dt.Compute("Sum(recqty)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvCenStore.FooterRow.FindControl("lblgvStQtyttl")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stqty)", "")) ?
                        0.00 : dt.Compute("Sum(stqty)", ""))).ToString("#,##0;(#,##0);  ");
                    break;
                case "QuantityB":
                    ((Label)this.gvQBasis.FooterRow.FindControl("lgvFopnqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnqty)", "")) ?
                       0.00 : dt.Compute("Sum(opnqty)", ""))).ToString("#,##0.0000;(#,##0.0000);  ");
                    ((Label)this.gvQBasis.FooterRow.FindControl("lgvFtrninqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trninqty)", "")) ?
                       0.00 : dt.Compute("Sum(trninqty)", ""))).ToString("#,##0.0000;(#,##0.0000);  ");
                    ((Label)this.gvQBasis.FooterRow.FindControl("lgvFrecqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recqty)", "")) ?
                      0.00 : dt.Compute("Sum(recqty)", ""))).ToString("#,##0.0000;(#,##0.0000);  ");
                    ((Label)this.gvQBasis.FooterRow.FindControl("lgvFtrnqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnqty)", "")) ?
                      0.00 : dt.Compute("Sum(trnqty)", ""))).ToString("#,##0.0000;(#,##0.0000);  ");
                    ((Label)this.gvQBasis.FooterRow.FindControl("lgvFmatisqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(matisqty)", "")) ?
                      0.00 : dt.Compute("Sum(matisqty)", ""))).ToString("#,##0.0000;(#,##0.0000);  ");
                    ((Label)this.gvQBasis.FooterRow.FindControl("lgvFstqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stqty)", "")) ?
                      0.00 : dt.Compute("Sum(stqty)", ""))).ToString("#,##0.0000;(#,##0.0000);  ");


                    break;
                case "AmountB":
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFOpnAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0.0000;(#,##.0000);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recam)", "")) ?
                        0.00 : dt.Compute("Sum(recam)", ""))).ToString("#,##0.0000;(#,##0.0000);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFtrnsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                        0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0.0000;(#,##0.0000);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFIssAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(matisamt)", "")) ?
                        0.00 : dt.Compute("Sum(matisamt)", ""))).ToString("#,##0.0000;(#,##0.0000);  ");
                    ((Label)this.gvAmtBasis.FooterRow.FindControl("lgvABFStkAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stcamt)", "")) ?
                        0.00 : dt.Compute("Sum(stcamt)", ""))).ToString("#,##0.0000;(#,##0.0000);  ");
                    //this.gvAmtBasis.DataBind();
                    break;
                case "MatUnused":
                    ((Label)this.gvmatunused.FooterRow.FindControl("lblgvTtlStk")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stqty)", "")) ?
                       0.00 : dt.Compute("Sum(stqty)", ""))).ToString("#,##0.00;(#,##0.00);  ");
                    ((Label)this.gvmatunused.FooterRow.FindControl("lgvABFStkttlAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stcamt)", "")) ?
                       0.00 : dt.Compute("Sum(stcamt)", ""))).ToString("#,##0.00;(#,##0.00);  ");
                    break;
            }

        }
        

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["InputType"].ToString();
            switch (Type)
            {
                case "General":
                    this.rptCentralStock1();
                    break;
                case "QuantityB":
                    this.rptCentralStockQB();
                    break;
                case "AmountB":
                    this.rptCentralStockAB();
                    break;
                case "MatUnused":
                    this.rptMatUnused();
                    break;
            }

        }
        protected void rptMatUnused()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Datefrom = this.txtDatefrom.Text.Trim() ;
            string DateTo = this.txtDateto.Text.Trim();
            DataTable dt = (DataTable)Session["tblVeiw"];
            
            var lst = dt.DataTableToList<SPEENTITY.C_11_RawInv.EClassPurchase.MatUnused>();
            //List<SPEENTITY.C_11_RawInv.EClassPurchase.MatUnused> lst = (List<SPEENTITY.C_11_RawInv.EClassPurchase.MatUnused>)Session["tblVeiw"];
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RpttMatUnused", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "PERIODIC MATERIAL UNUSED REPORT"));
            rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            rpt1.SetParameters(new ReportParameter("date", "Date : "+Datefrom+" To " + DateTo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            // rpt1.SetParameters(new ReportParameter("todate", DateTime.Today.ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        // 

        protected void rptCentralStock1()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string type = this.ddlCodeBookSegment.SelectedValue;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //   string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //   string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //  string ToFrDate = "(From :" + fromdate + " To " + todate + ")";

            DataTable dt = (DataTable)Session["tblVeiw"];

            var lst = dt.DataTableToList<SPEENTITY.C_11_RawInv.CentralStore>();
            LocalReport rpt1 = new LocalReport();

            if (type == "4" || type == "9")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptCentralStoreSub", lst, null, null);
                rpt1.EnableExternalImages = true;
            }
            else
            {
                
                rpt1 = RptSetupClass.GetLocalReport("R_11_RawInv.RptCentralStore", lst, null, null);
                rpt1.EnableExternalImages = true;
            }


            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("ToFrDate", "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )"));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Store wise Stock Report"));
            rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            rpt1.SetParameters(new ReportParameter("ProjectName", "Store Name: " + this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13)));
            rpt1.SetParameters(new ReportParameter("Level", "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim()));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            // rpt1.SetParameters(new ReportParameter("todate", DateTime.Today.ToString("dd-MMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void rptCentralStockOLD()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblVeiw"];
            ReportDocument rptstk = new RMGiRPT.R_11_RawInv.RptCentralStore();
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            txtlevel.Text = "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim();
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstk.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void rptCentralStockQB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblVeiw"];
            ReportDocument rptstk = new RMGiRPT.R_11_RawInv.rptInvQtyBasis(); //RptCentralStore();
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            txtlevel.Text = "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim();
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstk.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void rptCentralStockAB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblVeiw"];
            ReportDocument rptstk = new RMGiRPT.R_11_RawInv.rptInvAmtBasis();//.RptCentralStore();
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            txtlevel.Text = "Level: " + this.ddlRptGroup.SelectedValue.ToString().Trim();
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstk.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        protected void GetCodeBookList()
        {
            try
            {
                string Querytype = this.Request.QueryString["Type"];
                string coderange = "04%";

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet dsone = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                Session["tblmatsubhead"] = dsone.Tables[1];
                this.ddlCodeBook.DataTextField = "sircode";
                this.ddlCodeBook.DataValueField = "sircode1";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
                this.ddlCodeBook_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
            }
        }

        protected void ddlCodeBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mathead = this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            DataTable dt = (DataTable)Session["tblmatsubhead"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode1 like '" + mathead + "'";

            DataTable dt1 = dv.ToTable();

            DataRow dr = dt1.NewRow();
            dr["sircode"] = "All";
            dr["sircode1"] = "000000000000";
            dt1.Rows.Add(dr);
            //ds1.Tables[0].Rows.Add(dr);

            this.ddlGroup.DataTextField = "sircode";
            this.ddlGroup.DataValueField = "sircode1";
            this.ddlGroup.DataSource = dt1;
            this.ddlGroup.DataBind();
        }


        protected void ImgbtnFindProj_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void MatUnused_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvmatunused.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvCenStore_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCenStore.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvQBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvQBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvAmtBasis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAmtBasis.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvOrdewiseStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrdewiseStock.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvCenStore_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int RowIndex = e.Row.RowIndex;
                DataTable dt1 = (DataTable)Session["tblVeiw"];

                HyperLink proDesc = (HyperLink)e.Row.FindControl("lblgvMatdescryption");
                Label subcode = (Label)e.Row.FindControl("lblsubcode");
                Label specfcode = (Label)e.Row.FindControl("lblspecfcode");
                subcode.Visible = false;
                specfcode.Visible = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string fdate = this.txtDatefrom.Text.ToString();
                string tdate = this.txtDateto.Text.ToString();

                proDesc.NavigateUrl = "~/F_11_RawInv/RptIndProStock?Type=MatHis&sircode=" + subcode.Text + specfcode.Text + "&date=" + fdate + "&dayid=" + tdate;

            }
        }

        protected void gvQBasis_RowDataBound2(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int RowIndex = e.Row.RowIndex;
                DataTable dt1 = (DataTable)Session["tblVeiw"];

                HyperLink proDesc = (HyperLink)e.Row.FindControl("lnkgvQMatdescrp");
                Label subcode = (Label)e.Row.FindControl("lblsubjcode");
                Label specfcode = (Label)e.Row.FindControl("lblspcfcode");
                subcode.Visible = false;
                specfcode.Visible = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string fdate = this.txtDatefrom.Text.ToString();
                string tdate = this.txtDateto.Text.ToString();

                proDesc.NavigateUrl = "~/F_11_RawInv/RptIndProStock?Type=MatHis&sircode=" + subcode.Text + specfcode.Text + "&date=" + fdate + "&dayid=" + tdate;

            }
        }

        protected void gvAmtBasis_RowDataBound3(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int RowIndex = e.Row.RowIndex;
                DataTable dt1 = (DataTable)Session["tblVeiw"];

                HyperLink proDesc = (HyperLink)e.Row.FindControl("lnkgvAmBMatdescrp");
                Label subcode = (Label)e.Row.FindControl("lblsbjtcode");
                Label specfcode = (Label)e.Row.FindControl("lblspcfkcode");
                subcode.Visible = false;
                specfcode.Visible = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string fdate = this.txtDatefrom.Text.ToString();
                string tdate = this.txtDateto.Text.ToString();

                proDesc.NavigateUrl = "~/F_11_RawInv/RptIndProStock?Type=MatHis&sircode=" + subcode.Text + specfcode.Text + "&date=" + fdate + "&dayid=" + tdate;

            }
        }


        protected void gvCenStore_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }

            DataTable dt = (DataTable)Session["tblVeiw"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            gvCenStore.DataSource = sortedView;
            gvCenStore.DataBind();

            Session["tblVeiw"] = sortedView.ToTable();
        }

        public System.Web.UI.WebControls.SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (System.Web.UI.WebControls.SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }


    }
}