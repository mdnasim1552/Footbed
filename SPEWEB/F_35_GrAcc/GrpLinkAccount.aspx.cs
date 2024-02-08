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
    public partial class GrpLinkAccount : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "BalConfirmation") ? "Balance Confirmation Information"
                  : (this.Request.QueryString["Type"].ToString() == "Details") ? "Details of Balance Sheet"
                  : (this.Request.QueryString["Type"].ToString() == "INDetails") ? "Details of Income  Statement"
                  : (this.Request.QueryString["Type"].ToString() == "CashFlow") ? "Statement Of Cash Flow- Direct"
                  : (this.Request.QueryString["Type"].ToString() == "ReqStatus") ? "Business  Status"
                  : (this.Request.QueryString["Type"].ToString() == "PenRequisiton") ? "Pending Requisition" : "Date Wise Sales";
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Statement Of Cash Flow- Direct";

                this.SelectView();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private string GetCompCode()
        {

            return (this.Request.QueryString["comcod"]);

        }
        protected void ImgbtnFindCompany_Click(object sender, EventArgs e)
        {
            this.GetCompanyName();
        }
        private void GetCompanyName()
        {


            try
            {
                string comcod = this.GetCompCode();
                string SrchCompany = this.txtCompanySearch.Text + "%";
                DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETCOMPANY", SrchCompany, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                this.ddlCompany.DataTextField = "actdesc";
                this.ddlCompany.DataValueField = "actcode";
                this.ddlCompany.DataSource = ds1.Tables[0];
                this.ddlCompany.DataBind();
            }

            catch (Exception ex)
            {



            }
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowreqStatus();
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

                case "ReqStatus":
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.MultiView1.ActiveViewIndex = 5;
                    this.GetCompanyName();
                    this.ShowreqStatus();
                    break;


                case "PenRequisiton":
                    this.lblAsDate.Text = "As on " + this.Request.QueryString["Date1"].ToString();
                    this.MultiView1.ActiveViewIndex = 5;
                    this.GetCompanyName();
                    this.ShowreqStatus();
                    break;

                case "CashFlow":
                    this.MultiView1.ActiveViewIndex = 6;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                    this.ShowCashFlow();
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
        private void ShowreqStatus()
        {


            ViewState.Remove("tbAcc");
            string comcod = this.GetCompCode();
            string frmdate = this.Request.QueryString["Date1"];
            string trmdate = this.Request.QueryString["Date2"];
            string Company = ((this.ddlCompany.SelectedValue == "000000000000") ? "" : this.ddlCompany.SelectedValue.Substring(0, 8)) + "%";
            string Pending = (this.Request.QueryString["Type"].ToString() == "PenRequisiton") ? "Pending" : "";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "RPTREQSTATUS", frmdate, trmdate, Company, Pending, "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
                return;

            }

            ViewState["tbAcc"] = ds1.Tables[0];
            this.Data_Bind();







        }
        private void ShowCashFlow()
        {

            ViewState.Remove("tbAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy");
            string Opendate = Convert.ToDateTime(this.Request.QueryString["Date1"]).AddMonths(-1).ToString("dd-MMM-yyyy");

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTCASHFLOW", frmdate, todate, Opendate, "4", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvCashFlow.DataSource = null;
                this.grvCashFlow.DataBind();
                return;
            }
            ViewState["tbAcc"] = this.HiddenSameData(ds1.Tables[0]);
            ViewState["recandpayNote"] = ds1.Tables[1];
            this.Data_Bind();
            ds1.Dispose();
            this.RPNote();
            for (int i = 0; i < grvCashFlow.Rows.Count; i++)
            {
                string actcode = ((Label)grvCashFlow.Rows[i].FindControl("lgvactcode")).Text.Trim();
                LinkButton lactDesc = (LinkButton)grvCashFlow.Rows[i].FindControl("lbtnactDesc");
                if (ASTUtility.Right(actcode, 4) != "AAAA")
                    lactDesc.CommandArgument = actcode;

            }



        }
        private void RPNote()
        {
            this.PanelNote.Visible = true;
            DataTable dt = (DataTable)ViewState["recandpayNote"];
            this.gvbankbal.DataSource = dt;
            this.gvbankbal.DataBind();
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
                case "CashFlow":


                    grp = dt1.Rows[0]["grp"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";
                        grp = dt1.Rows[j]["grp"].ToString();




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

                case "ReqStatus":
                case "PenRequisiton":
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFreqamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reqamt)", "")) ? 0.00 : dt.Compute("sum(reqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFsalordamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salordamt)", "")) ? 0.00 : dt.Compute("sum(salordamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFordramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordramt)", "")) ? 0.00 : dt.Compute("sum(ordramt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFrcvmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rcvamt)", "")) ? 0.00 : dt.Compute("sum(rcvamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFdelamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(delamt)", "")) ? 0.00 : dt.Compute("sum(delamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFinvamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(invamt)", "")) ? 0.00 : dt.Compute("sum(invamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFstoreamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(storeamt)", "")) ? 0.00 : dt.Compute("sum(storeamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFcollamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(collamt)", "")) ? 0.00 : dt.Compute("sum(collamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFpayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(acpaidamt)", "")) ? 0.00 : dt.Compute("sum(acpaidamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFlfhoff")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lfhoff)", "")) ? 0.00 : dt.Compute("sum(lfhoff)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFltohoff")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ltohoff)", "")) ? 0.00 : dt.Compute("sum(ltohoff)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFoheadarisk")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oheadarisk)", "")) ? 0.00 : dt.Compute("sum(oheadarisk)", ""))).ToString("#,##0.00;(#,##0.00); ");


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
                case "ReqStatus":
                case "PenRequisiton":

                    this.gvReqStatus.DataSource = dt;
                    this.gvReqStatus.DataBind();
                    this.FooterCalculation();
                    break;
                case "CashFlow":
                    this.grvCashFlow.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvCashFlow.Columns[3].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");
                    this.grvCashFlow.Columns[4].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddMonths(-1).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.grvCashFlow.DataSource = dt;
                    this.grvCashFlow.DataBind();
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
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




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
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




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
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




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
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             //((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




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
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




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
        protected void gvReqStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)

            {
                HyperLink lnkreqno = (HyperLink)e.Row.FindControl("hlnkgvreqno");
                Label lblgvstoreamt = (Label)e.Row.FindControl("lblgvstoreamt");
                Label lblgvacsupdat = (Label)e.Row.FindControl("lblgvacsupdat");
                Label lblgvperodelamt = (Label)e.Row.FindControl("lblgvperodelamt");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();



                if (reqno == "")
                {
                    return;
                }

                double storeamt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "storeamt"));
                double perooheadar = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "perooheadar"));
                DateTime Shipsupdat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "shipsupdat"));
                DateTime deldat = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "deldat1"));
                if (Shipsupdat < deldat)
                {
                    lblgvacsupdat.Style.Add("color", "red");

                }

                if (storeamt > 0.00)
                {
                    lblgvstoreamt.Style.Add("color", "red");
                }
                if (perooheadar < 0.00)
                {
                    lblgvperodelamt.Style.Add("color", "red");
                }


                lnkreqno.Style.Add("color", "blue");
                lnkreqno.NavigateUrl = "~/F_35_GrAcc/LinkReqStatus.aspx?Rpt=Purchasetrk&comcod=" + comcod + "&reqno=" + reqno;


            }







        }

        protected void grvCashFlow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton description = (LinkButton)e.Row.FindControl("lbtnactDesc");
                Label closam = (Label)e.Row.FindControl("lblgvclosamcf");
                Label opnam = (Label)e.Row.FindControl("lblgvopnamcf");
                HyperLink cuamt = (HyperLink)e.Row.FindControl("hlnkgvcuamtcf");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    description.Font.Bold = true;
                    closam.Font.Bold = true;

                    opnam.Font.Bold = true;
                    cuamt.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }





                if (code == "FFFFAAAAAAAA")
                {

                    cuamt.Style.Add("color", "blue");
                    cuamt.NavigateUrl = "~/F_35_GrAcc/GrpLinkAccount.aspx?Type=BalConfirmation&comcod=" + comcod + "&Date1=" + this.Request.QueryString["Date1"].ToString() + "&Date2=" + this.Request.QueryString["Date2"].ToString();

                }



            }
        }
        protected void gvbankbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label ActDesc = (Label)e.Row.FindControl("lgcActDescbb");
                Label opnam = (Label)e.Row.FindControl("lgvopnambb");
                Label closam = (Label)e.Row.FindControl("lgvclosambb");
                Label balam = (Label)e.Row.FindControl("lgbalambb");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "code")).ToString();


                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "AAA")
                {


                    ActDesc.Font.Bold = true;
                    opnam.Font.Bold = true;
                    closam.Font.Bold = true;
                    balam.Font.Bold = true;
                    ActDesc.Style.Add("text-align", "right");

                }




            }
        }
        protected void lbtnactDesc_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string actcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "actcode like('" + actcode + "')";
            dt = dv1.ToTable();
            if (dt.Rows.Count == 0)
                return;

            string mCOMCOD = comcod;
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString();
            string mACTCODE = dt.Rows[0]["actcode"].ToString();
            string mACTDESC = dt.Rows[0]["actdesc"].ToString();
            string lebel2 = dt.Rows[0]["rleb2"].ToString();
            if (mACTCODE == "")
            {
                return;
            }

            ///---------------------------------//// 
            if (ASTUtility.Left(mACTCODE, 1) == "4")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }
            else if (lebel2 == "")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }
        }


    }
}