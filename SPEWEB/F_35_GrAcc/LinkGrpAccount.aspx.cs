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
    public partial class LinkGrpAccount : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lblHeadtitle.Text = (this.Request.QueryString["Type"].ToString() == "BalConfirmation") ? "Balance Confirmation Information"
                  : (this.Request.QueryString["Type"].ToString() == "Details") ? "Details of Balance Sheet"
                  : (this.Request.QueryString["Type"].ToString() == "INDetails") ? "Details of Income  Statement" : "Date Wise Sales";
                this.SelectView();
            }
        }


        private string GetCompCode()
        {

            return (this.Request.QueryString["comcod"]).ToString();

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "BalConfirmation":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lblfrmdate.Text = this.Request.QueryString["Date1"];
                    this.lbltodate.Text = this.Request.QueryString["Date2"];
                    this.ShowBalConfirmation();
                    break;
                case "SalesProj":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.sfrDate.Text = this.Request.QueryString["Date1"];
                    this.stDate.Text = this.Request.QueryString["Date2"];
                    this.salesStatus();
                    break;
                case "SalDetails":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.lblSFrmDate.Text = this.Request.QueryString["Date1"];
                    this.lblSTrmDate.Text = this.Request.QueryString["Date2"];
                    this.ShowSalesDetails();
                    break;


                case "Details":
                    this.MultiView1.ActiveViewIndex = 3;
                    //this.lblSFrmDate.Text = this.Request.QueryString["Date1"];
                    //this.lblSTrmDate.Text = this.Request.QueryString["Date2"];
                    this.DetailBalance();
                    break;



                case "INDetails":
                    this.MultiView1.ActiveViewIndex = 4;

                    this.IncomeDetails();
                    break;


            }
        }

        private void ShowBalConfirmation()
        {

            ViewState.Remove("tbAcc");
            string comcod = this.GetCompCode();
            string date1 = this.Request.QueryString["Date1"];
            string date2 = this.Request.QueryString["Date2"];

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTCASHANDBANKBAL", date1, date2, "12", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvCABankBal.DataSource = null;
                this.gvCABankBal.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private void salesStatus()
        {
            Session.Remove("tbAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.Request.QueryString["pactcode"];
            string frdate = this.Request.QueryString["Date1"];
            string todate = this.Request.QueryString["Date2"];

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDAYWISHSAL", PactCode, frdate, todate, "12", "%", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDayWSale.DataSource = null;
                this.gvDayWSale.DataBind();
                return;
            }

            this.lblPrijDesc.Text = ds1.Tables[0].Rows[0]["pactdesc"].ToString();
            ViewState["tbAcc"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private void ShowSalesDetails()
        {
            ViewState.Remove("tbAcc");
            string comcod = this.GetCompCode();
            string date1 = this.Request.QueryString["Date1"];
            string date2 = this.Request.QueryString["Date2"];

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT02", "RPTSALESDETAILS", date1, date2, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvSalDet.DataSource = null;
                this.grvSalDet.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = ds1.Tables[0];
            this.Data_Bind();



        }

        private void DetailBalance()
        {

            ViewState.Remove("tbAcc");
            string comcod = this.GetCompCode();
            string date1 = this.Request.QueryString["Date1"];
            string date2 = this.Request.QueryString["Date2"];
            string levelmain = "12";
            string leveldetails = "12";
            string status = this.Request.QueryString["Type"];
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTDETAILSTB", date1, date2, levelmain, leveldetails, status, "", "", "", "");



            if (ds1 == null)
            {
                this.gvDetails.DataSource = null;
                this.gvDetails.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private void IncomeDetails()
        {

            ViewState.Remove("tbAcc");
            string comcod = this.GetCompCode();
            string date1 = this.Request.QueryString["Date1"];
            string date2 = this.Request.QueryString["Date2"];
            string opndate = this.Request.QueryString["opndate"];
            string levelmain = "12";
            string leveldetails = "12";

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTDETAILINST", date1, date2, levelmain, leveldetails, opndate, "", "", "", "");



            if (ds1 == null)
            {
                this.gvInDetails.DataSource = null;
                this.gvInDetails.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();



        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "BalConfirmation":

                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";
                        grp = dt1.Rows[j]["grp"].ToString();
                    }
                    break;

                case "SalesProj":
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                        }

                        else
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                        }

                    }


                    break;

                case "Details":
                case "TBDetails":
                case "INDetails":

                    string actcode4 = dt1.Rows[0]["actcode4"].ToString();

                    if (dt1.Rows[0]["rescode4"].ToString().Substring(0, 4) == "0000")
                        dt1.Rows[0]["rescode4"] = "";

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode4"].ToString() == actcode4)
                        {
                            actcode4 = dt1.Rows[j]["actcode4"].ToString();
                            dt1.Rows[j]["actdesc4"] = "";
                            dt1.Rows[j]["actcode4"] = "";

                        }

                        else
                        {
                            actcode4 = dt1.Rows[j]["actcode4"].ToString();

                        }

                        if (dt1.Rows[j]["rescode4"].ToString().Substring(0, 4) == "0000")
                            dt1.Rows[j]["rescode4"] = "";
                    }

                    break;
            }

            return dt1;
        }
        private void FooterCalculation()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            if (dt.Rows.Count == 0)
                return;

            switch (type)
            {

                case "SalesProj":
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tuamt)", "")) ?
                                    0 : dt.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(suamt)", "")) ?
                                    0 : dt.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDDisAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disamt)", "")) ?
                    //                0 : dt.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;
                case "SalDetails":
                    ((Label)this.grvSalDet.FooterRow.FindControl("lgvFsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salamt)", "")) ?
                                    0 : dt.Compute("sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvSalDet.FooterRow.FindControl("lgvFaAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(agamt)", "")) ?
                                    0 : dt.Compute("sum(agamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvSalDet.FooterRow.FindControl("lgvFNAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ?
                                    0 : dt.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }


        }
        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tbAcc"];

            if ((dt.Rows.Count == 0)) //Problem
                return;

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "BalConfirmation":
                    this.gvCABankBal.DataSource = dt;
                    this.gvCABankBal.DataBind();
                    break;
                case "SalesProj":

                    this.gvDayWSale.DataSource = dt;
                    this.gvDayWSale.DataBind();

                    this.FooterCalculation();
                    break;
                case "SalDetails":
                    this.grvSalDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvSalDet.DataSource = dt;
                    this.grvSalDet.DataBind();
                    this.FooterCalculation();
                    break;

                case "Details":

                    this.gvDetails.Columns[3].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
                    this.gvDetails.Columns[4].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddDays(-1).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
                    this.gvDetails.DataSource = dt;
                    this.gvDetails.DataBind();
                    break;

                case "INDetails":

                    this.gvInDetails.Columns[3].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");
                    this.gvInDetails.Columns[4].HeaderText = Convert.ToDateTime(this.Request.QueryString["opndate"].ToString()).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.gvInDetails.DataSource = dt;
                    this.gvInDetails.DataBind();
                    break;

            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "BalConfirmation":
                    this.PrintBalConfirmation();
                    break;
                case "SalesProj":
                    this.rptDayWSale();
                    break;
                case "SalDetails":
                    this.RptSalesDetails();
                    break;

                case "Details":
                    this.PrintDetBS();
                    break;
                case "INDetails":
                    this.PrintDetIncome();
                    break;

            }
        }

        protected void PrintBalConfirmation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptAccBalConfirmation();
            //DataTable dt = (DataTable)ViewState["tbAcc"];
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.lblfrmdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.lbltodate.Text).ToString("dd-MMM-yyyy") + " )";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void rptDayWSale()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)ViewState["tbAcc"];
            //ReportDocument rptsale = new RMGiRPT.R_22_Sal.rptDayWiseSales();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptCode = rptsale.ReportDefinition.ReportObjects["CodeDesc"] as TextObject;
            //rptCode.Text = "Level: Details";
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "From : " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void RptSalesDetails()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptSalesDetails();
            //DataTable dt = (DataTable)ViewState["tbAcc"];
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["TxtDate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " )";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void PrintDetBS()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptDetAccBalanceSheet();
            //DataTable dt = (DataTable)ViewState["tbAcc"];
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;   //
            //rpttxtcompanyname.Text = comnam;

            //TextObject rpttxtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;   //
            //rpttxtHeader.Text = "Details of Balance Sheet";

            //TextObject TxtOpening = rptstk.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            //TxtOpening.Text = Convert.ToDateTime(this.Request.QueryString["Date1"]).AddDays(-1).ToString("dd-MMM-yyyy") + " Taka";

            //TextObject TxtClosing = rptstk.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            //TxtClosing.Text = Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy") + " Taka";


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void PrintDetIncome()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.RptDetAccBalanceSheet();
            //DataTable dt = (DataTable)ViewState["tbAcc"];
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;   //txtHeader
            //rpttxtcompanyname.Text = comnam;

            //TextObject rpttxtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;   //
            //rpttxtHeader.Text = "Details of Income Statment";

            //TextObject TxtOpening = rptstk.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            //TxtOpening.Text = Convert.ToDateTime(this.Request.QueryString["opndate"]).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date1"]).AddDays(-1).ToString("dd-MMM-yyyy");

            //TextObject TxtClosing = rptstk.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            //TxtClosing.Text = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy");


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void gvCABankBal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink description = (HyperLink)e.Row.FindControl("HLgvDescbankcb");
                Label opnam = (Label)e.Row.FindControl("lblgvopnamcb");
                Label closam = (Label)e.Row.FindControl("lblgvclosamcb");
                Label netbal = (Label)e.Row.FindControl("lblgvnetbalcb");




                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    description.Font.Bold = true;
                    opnam.Font.Bold = true;
                    closam.Font.Bold = true;
                    netbal.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }
                else if (ASTUtility.Right(code, 4) == "0000")
                {

                    description.Font.Bold = true;
                    opnam.Font.Bold = true;
                    closam.Font.Bold = true;
                    netbal.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }


            }
        }

        protected void gvDayWSale_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDayWSale.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvSalDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvSalDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label description = (Label)e.Row.FindControl("lblgvdescriptiond");
                Label lblopnamt = (Label)e.Row.FindControl("lblgvopnamtd");
                Label lblgvcuamt = (Label)e.Row.FindControl("lblgvcuamt");

                Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobald");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "00000000AAAA")
                {

                    description.Font.Bold = true;
                    lblopnamt.Font.Bold = true;
                    lblgvcuamt.Font.Bold = true;

                    lblgvclobal.Font.Bold = true;

                }
            }
        }
        protected void gvInDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label description = (Label)e.Row.FindControl("lblgvdescriptionind");
                Label lblopnamt = (Label)e.Row.FindControl("lblgvopnamtind");
                Label lblgvcuamt = (Label)e.Row.FindControl("lblgvcuamtind");
                Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobalind");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "00000000AAAA")
                {

                    description.Font.Bold = true;
                    lblopnamt.Font.Bold = true;
                    lblgvcuamt.Font.Bold = true;

                    lblgvclobal.Font.Bold = true;

                }
            }
        }
    }
}