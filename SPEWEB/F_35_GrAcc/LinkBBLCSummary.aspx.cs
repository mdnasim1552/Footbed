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
    public partial class LinkBBLCSummary : System.Web.UI.Page
    {
        ProcessAccess ComData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "SupWise") ? "SUPPLIER WISE BBLC POSITION" : "ORDER WISE SUPPLY - SUMMARY";
                this.lblDaterange.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                this.ShowBBLC();



            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void ShowBBLC()
        {

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "OrderWise":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.showBBLCStatus();
                    break;
                case "SupWise":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.showBBLCStatus();

                    break;
            }



        }

        private void showBBLCStatus()
        {



            Session.Remove("tblstatus");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string CallType = (this.Request.QueryString["Type"] == "SupWise") ? "RPTSUPWISESTATUS" : "RPTORDWISESUMMARY";
            DataSet ds1 = ComData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", CallType, frmdate, todate, "", "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvOrderStatus.DataSource = null;
                this.gvOrderStatus.DataBind();
                return;

            }
            Session["tblstatus"] = ds1.Tables[0];
            this.Data_Bind();


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            //mainmlccod, mlccod, ssircode, bblccod,  buyercod, lcval, bblcdat,  bblcamt, mainmlcdesc, orderno, jobno, bblcdesc, ssirdesc, buyerdesc
            //string reqno = "";
            //string mlccod = "";
            //string buyercod = "";
            //string mainmlccod = "";
            //string ssircode = "";
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "OrderWise":
                    string mlccod = dt1.Rows[0]["mlccod"].ToString();
                    string buyercod = dt1.Rows[0]["buyercod"].ToString();
                    string mainmlccod = dt1.Rows[0]["mainmlccod"].ToString();
                    string ssircode1 = dt1.Rows[0]["ssirdesc"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["mlccod"].ToString() == mlccod && dt1.Rows[j]["buyercod"].ToString() == buyercod && dt1.Rows[j]["mainmlccod"].ToString() == mainmlccod)
                        {
                            mlccod = dt1.Rows[j]["mlccod"].ToString();
                            buyercod = dt1.Rows[j]["buyercod"].ToString();
                            mainmlccod = dt1.Rows[j]["mainmlccod"].ToString();

                            dt1.Rows[j]["mainmlcdesc"] = "";
                            dt1.Rows[j]["jobno"] = "";
                            dt1.Rows[j]["buyerdesc"] = "";
                            dt1.Rows[j]["orderno"] = "";
                            dt1.Rows[j]["lcval"] = 0.00;

                        }
                        else
                        {
                            if (dt1.Rows[j]["mainmlccod"].ToString() == mainmlccod)
                            {
                                dt1.Rows[j]["mainmlcdesc"] = "";
                            }

                            if (dt1.Rows[j]["mlccod"].ToString() == mlccod)
                            {
                                dt1.Rows[j]["orderno"] = "";
                                dt1.Rows[j]["jobno"] = "";
                                dt1.Rows[j]["lcval"] = 0.00;
                            }
                            if (dt1.Rows[j]["buyercod"].ToString() == buyercod)
                            {
                                dt1.Rows[j]["buyerdesc"] = "";
                            }

                            mlccod = dt1.Rows[j]["mlccod"].ToString();
                            buyercod = dt1.Rows[j]["buyercod"].ToString();
                            mainmlccod = dt1.Rows[j]["mainmlccod"].ToString();

                        }
                        ////////--------------/////////////

                        if (dt1.Rows[j]["ssirdesc"].ToString() == ssircode1)
                        {
                            ssircode1 = dt1.Rows[j]["ssirdesc"].ToString();
                            dt1.Rows[j]["ssirdesc"] = "";


                        }

                        else
                        {
                            ssircode1 = dt1.Rows[j]["ssirdesc"].ToString();
                        }
                    }

                    break;

                case "SupWise":
                    string ssircode = dt1.Rows[0]["ssircode"].ToString();
                    string mlccod1 = dt1.Rows[0]["mlccod"].ToString();
                    string buyercod1 = dt1.Rows[0]["buyercod"].ToString();
                    string mainmlccod1 = dt1.Rows[0]["mainmlccod"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["ssircode"].ToString() == ssircode && dt1.Rows[j]["mlccod"].ToString() == mlccod1 && dt1.Rows[j]["buyercod"].ToString() == buyercod1 && dt1.Rows[j]["mainmlccod"].ToString() == mainmlccod1)
                        {
                            ssircode = dt1.Rows[0]["ssircode"].ToString();
                            mlccod1 = dt1.Rows[j]["mlccod"].ToString();
                            buyercod1 = dt1.Rows[j]["buyercod"].ToString();
                            mainmlccod1 = dt1.Rows[j]["mainmlccod"].ToString();

                            dt1.Rows[j]["ssirdesc"] = "";
                            dt1.Rows[j]["mainmlcdesc"] = "";
                            dt1.Rows[j]["jobno"] = "";
                            dt1.Rows[j]["buyerdesc"] = "";
                            dt1.Rows[j]["orderno"] = "";
                            dt1.Rows[j]["lcval"] = 0.00;

                        }
                        if (dt1.Rows[j]["ssircode"].ToString() == ssircode && dt1.Rows[j]["mlccod"].ToString() == mlccod1)
                        {
                            ssircode = dt1.Rows[0]["ssircode"].ToString();
                            mlccod1 = dt1.Rows[j]["mlccod"].ToString();
                            buyercod1 = dt1.Rows[j]["buyercod"].ToString();
                            mainmlccod1 = dt1.Rows[j]["mainmlccod"].ToString();

                            dt1.Rows[j]["ssirdesc"] = "";
                            dt1.Rows[j]["jobno"] = "";
                            dt1.Rows[j]["orderno"] = "";
                            dt1.Rows[j]["lcval"] = 0.00;

                        }

                        else
                        {
                            if (dt1.Rows[j]["ssircode"].ToString() == ssircode)
                            {
                                dt1.Rows[j]["ssirdesc"] = "";
                            }
                            if (dt1.Rows[j]["mainmlccod"].ToString() == mainmlccod1)
                            {
                                dt1.Rows[j]["mainmlcdesc"] = "";
                            }

                            if (dt1.Rows[j]["mlccod"].ToString() == mlccod1)
                            {
                                dt1.Rows[j]["orderno"] = "";
                                dt1.Rows[j]["jobno"] = "";
                                dt1.Rows[j]["lcval"] = 0.00;
                            }
                            if (dt1.Rows[j]["buyercod"].ToString() == buyercod1)
                            {
                                dt1.Rows[j]["buyerdesc"] = "";
                            }

                            mlccod1 = dt1.Rows[j]["mlccod"].ToString();
                            buyercod1 = dt1.Rows[j]["buyercod"].ToString();
                            mainmlccod1 = dt1.Rows[j]["mainmlccod"].ToString();
                            ssircode = dt1.Rows[j]["ssircode"].ToString();

                        }
                    }

                    break;

            }


            return dt1;


        }

        private void Data_Bind()
        {

            DataTable dt01 = (DataTable)Session["tblstatus"];
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "OrderWise":
                    this.gvOrderStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvOrderStatus.DataSource = dt01;// (DataTable)Session["tblstatus"];
                    this.gvOrderStatus.DataBind();
                    this.FooterCalculation();
                    break;
                case "SupWise":
                    this.gvSupplierWise.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSupplierWise.DataSource = dt01;// (DataTable)Session["tblstatus"];
                    this.gvSupplierWise.DataBind();
                    this.FooterCalculation();
                    break;
            }



        }

        private void FooterCalculation()
        {

            DataTable dt = ((DataTable)Session["tblstatus"]);
            if (dt.Rows.Count == 0)
                return;


            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "OrderWise":

                    double lcamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lcval)", "")) ? 0 : dt.Compute("sum(lcval)", "")));
                    double bblcamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bblcamt)", "")) ? 0 : dt.Compute("sum(bblcamt)", "")));
                    double peronlc = (lcamt > 0) ? (bblcamt * 100) / lcamt : 0;
                    ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvFlcamt")).Text = lcamt.ToString("#,##0;(#,##0); ");
                    ((HyperLink)this.gvOrderStatus.FooterRow.FindControl("hlnkgvFbblcamt")).Text = bblcamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvFperonlc")).Text = peronlc.ToString("#,##0;(#,##0); ") + "%";
                    if (dt.Rows.Count > 0)
                    {

                        string comcod = this.Request.QueryString["comcod"].ToString();
                        string frmdate = this.Request.QueryString["Date1"].ToString();
                        string todate = this.Request.QueryString["Date2"].ToString();
                        ((HyperLink)this.gvOrderStatus.FooterRow.FindControl("hlnkgvFbblcamt")).NavigateUrl = "~/F_35_GrAcc/LinkBBLCStatus.aspx?Type=OrderWise&comcod=" + comcod + "&Date1=" + frmdate + "&Date2=" + todate;
                    }





                    break;
                case "SupWise":
                    ((Label)this.gvSupplierWise.FooterRow.FindControl("lgvFbblcamts")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bblcamt)", "")) ?
                                       0 : dt.Compute("sum(bblcamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;
            }


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "OrderWise":

                    this.PrintOrdWiseSup();
                    break;
                case "SupWise":

                    this.PrintSupWiseBBLC();
                    break;
            }

        }
        private void PrintOrdWiseSup()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string HeaderTitle = "Order Wise Supply - Summary";

            //ReportDocument Orderstatus = new RMGiRPT.R_09_Commer.RptOrderstSupWise();
            //TextObject rptCname = Orderstatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rpttxtHeaderTitle = Orderstatus.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = HeaderTitle;
            //TextObject txtFDate1 = Orderstatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = this.lblDaterange.Text;

            //TextObject txtuserinfo = Orderstatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Orderstatus.SetDataSource((DataTable)Session["tblstatus"]);
            //Session["Report1"] = Orderstatus;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintSupWiseBBLC()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string HeaderTitle = "Supplier Wise BBLC Position";

            //ReportDocument Orderstatus = new RMGiRPT.R_09_Commer.RptSupWiseBBlCPos();
            //TextObject rptCname = Orderstatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rpttxtHeaderTitle = Orderstatus.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = HeaderTitle;
            //TextObject txtFDate1 = Orderstatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = this.lblDaterange.Text;

            //TextObject txtuserinfo = Orderstatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Orderstatus.SetDataSource((DataTable)Session["tblstatus"]);
            //Session["Report1"] = Orderstatus;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void gvOrderStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrderStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvSupplierWise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSupplierWise.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvSupplierWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label bblcdesc = (Label)e.Row.FindControl("lgvsbblcdesc");
                Label bblcamt = (Label)e.Row.FindControl("lgvsbblcamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircoded")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 1) == "B")
                {

                    bblcdesc.Font.Bold = true;
                    bblcamt.Font.Bold = true;
                    bblcdesc.Style.Add("text-align", "right");
                }

            }
        }
    }
}