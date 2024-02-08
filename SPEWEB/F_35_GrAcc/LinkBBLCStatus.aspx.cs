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
    public partial class LinkBBLCStatus : System.Web.UI.Page
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
                case "AllPro":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowOrderTrakcing();
                    this.lblDaterange.Visible = false;
                    this.lbltxtDateRange.Visible = false;
                    break;
            }



        }

        private void showBBLCStatus()
        {



            Session.Remove("tblstatus");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string CallType = (this.Request.QueryString["Type"] == "SupWise") ? "RPTSUPWISESTATUS" : "RPTORDWISESTATUS";
            DataSet ds1 = ComData.GetTransInfo(comcod, "SP_REPORT_ORDER_STATUS", CallType, frmdate, todate, "", "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvOrderStatus.DataSource = null;
                this.gvOrderStatus.DataBind();
                return;

            }
            Session["tblstatus"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }

        private void ShowOrderTrakcing()
        {

            Session.Remove("tblstatus");
            string comcod = this.Request.QueryString["comcod"].ToString();
            DataSet ds1 = ComData.GetTransInfo(comcod, "SP_REPORT_PRODPROCESS", "RPTORDERPROCESS", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvOrderTrack.DataSource = null;
                this.gvOrderTrack.DataBind();
                return;

            }
            Session["tblprocess"] = ds1.Tables[1];
            Session["tblstatus"] = this.HiddenSameData(ds1.Tables[0]);
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
                case "AllPro":
                    string orderno = dt1.Rows[0]["orderno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["orderno"].ToString() == orderno)
                        {
                            orderno = dt1.Rows[j]["orderno"].ToString();
                            dt1.Rows[j]["orderdesc"] = "";
                        }

                        else
                            orderno = dt1.Rows[j]["orderno"].ToString();


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
                case "AllPro":
                    DataTable dt = (DataTable)Session["tblstatus"];
                    DataTable dt1 = (DataTable)Session["tblprocess"];

                    for (int i = 2; i < this.gvOrderTrack.Columns.Count - 1; i++)
                        this.gvOrderTrack.Columns[i].Visible = false;
                    int j = 2;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        this.gvOrderTrack.Columns[j].Visible = true;
                        this.gvOrderTrack.Columns[j].HeaderText = dt1.Rows[i]["prodesc"].ToString();
                        if (j == 13)
                            break;
                        j++;
                    }

                    this.gvOrderTrack.DataSource = dt;
                    this.gvOrderTrack.DataBind();
                    this.FooterCalculation();


                    break;
            }



        }

        private void FooterCalculation()
        {
            DataTable dt1 = ((DataTable)Session["tblstatus"]).Copy();
            DataTable dt = new DataTable();

            if (dt1.Rows.Count == 0)
                return;

            DataView dvr = new DataView();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "OrderWise":
                    dvr = dt1.DefaultView;
                    dvr.RowFilter = ("mlccodd like '%B%'");
                    dt = dvr.ToTable();

                    ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvFbblcamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bblcamt)", "")) ?
                                       0 : dt.Compute("sum(bblcamt)", ""))).ToString("#,##0;(#,##0); ");






                    break;
                case "SupWise":
                    dvr = dt1.DefaultView;
                    dvr.RowFilter = ("ssircoded like '%B%'");
                    dt = dvr.ToTable();
                    ((Label)this.gvSupplierWise.FooterRow.FindControl("lgvFbblcamts")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bblcamt)", "")) ?
                                       0 : dt.Compute("sum(bblcamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;
                case "AllPro":
                    DataTable tbl1 = dt1.Copy();
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp1")).Text =
                        Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p1)", "")) ?
                            0.00 : tbl1.Compute("Sum(p1)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp2")).Text =
                        Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p2)", "")) ?
                            0.00 : tbl1.Compute("Sum(p2)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp3")).Text =
                        Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p3)", "")) ?
                            0.00 : tbl1.Compute("Sum(p3)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp4")).Text =
                        Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p4)", "")) ?
                            0.00 : tbl1.Compute("Sum(p4)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp5")).Text =
                        Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p5)", "")) ?
                            0.00 : tbl1.Compute("Sum(p5)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp6")).Text =
                        Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p6)", "")) ?
                            0.00 : tbl1.Compute("Sum(p6)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp7")).Text =
                        Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p7)", "")) ?
                            0.00 : tbl1.Compute("Sum(p7)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp8")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p8)", "")) ?
                            0.00 : tbl1.Compute("Sum(p8)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp9")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p9)", "")) ?
                            0.00 : tbl1.Compute("Sum(p9)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp10")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p10)", "")) ?
                            0.00 : tbl1.Compute("Sum(p10)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp11")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p11)", "")) ?
                            0.00 : tbl1.Compute("Sum(p11)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblp12")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(p12)", "")) ?
                            0.00 : tbl1.Compute("Sum(p12)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvOrderTrack.FooterRow.FindControl("lblbalinhand")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(balqty)", "")) ?
                           0.00 : tbl1.Compute("Sum(balqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
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
                case "AllPro":

                    this.PrintAllpro();
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
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintAllpro()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblstatus"];
            DataTable dt1 = (DataTable)Session["tblprocess"];

            //ReportDocument rptstk = new RMGiRPT.R_15_Pro.RptOrderTracking();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            //txtCompany.Text = comnam;
            //int j = 1;
            //for (int i = 0; i < dt1.Rows.Count; i++)
            //{


            //    string header = dt1.Rows[i]["prodesc"].ToString();
            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
            //    rpttxth.Text = header;
            //    j++;
            //    if (j == 11)
            //        break;
            //}

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);
            ////string comcod = this.GetComeCode();
            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            ////  }
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
        protected void gvOrderStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label Supplier = (Label)e.Row.FindControl("lgvsupplier");
                Label bblcamt = (Label)e.Row.FindControl("lgvbblcamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mlccodd")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 1) == "B" || ASTUtility.Right(code, 1) == "C")
                {

                    Supplier.Font.Bold = true;
                    bblcamt.Font.Bold = true;
                    Supplier.Style.Add("text-align", "right");
                }

            }
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
        protected void gvOrderTrack_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}