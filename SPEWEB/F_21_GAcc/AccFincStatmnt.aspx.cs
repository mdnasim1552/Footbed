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

namespace SPEWEB.F_21_GAcc
{
    public partial class AccFincStatmnt : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDatefrom.Text = Convert.ToDateTime("01-Jan-" + curdate.Substring(7)).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtOpeningDate.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddYears(-1).ToString("dd-MMM-yyyy");

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            string reptype = this.txtflag.Text.Trim();

            switch (reptype)
            {
                case "IS":
                    this.GetIncomeStatementForPrint();
                    break;
                case "BS":
                    this.GetBalanceSheetForPrint();
                    break;
                case "SHEQUITY":
                    this.PrintShareQty();
                    break;
                case "CSHFLW":
                    this.RptCashFlow02();
                    break;
                case "CSHFLW2":
                    this.RptCashFlow02();
                    break;



            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string reptype = this.txtflag.Text.Trim();

            switch (reptype)
            {
                case "IS":
                    this.GetIncomeStatement();
                    break;
                case "BS":
                    this.GetBalanceSheet();
                    break;
                case "SHEQUITY":
                    this.SHOWSHAREEQUIT();
                    break;
                case "CSHFLW":
                    this.ShowCashFlow();
                    break;
                case "CSHFLW2":
                    this.ShowCashFlow02();
                    break;



            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "activetab();", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report: ";// + mRepID;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void GetBalanceSheet()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string CallType = this.Company();
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", CallType +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, "", "", "", "", "", "");


            this.dgvBS.Columns[2].HeaderText = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBS.Columns[3].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBS.DataSource = ds1.Tables[0];
            this.dgvBS.DataBind();
            ViewState["tblAcc"] = ds1.Tables[0];

            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvBS.HeaderRow.FindControl("hlbtnDetailsbs")).NavigateUrl = "LinkAccount.aspx?Type=Details&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

        }
        protected void dgvBS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            this.txtOpeningDate.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddYears(-1).ToString("dd-MMM-yyyy");

            string opendat = this.txtOpeningDate.Text;
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBSDesc");
            Label lblcode = (Label)e.Row.FindControl("lblgvcode");
            Label clobal = (Label)e.Row.FindControl("lblgvclobal");
            Label opnamt = (Label)e.Row.FindControl("lblgvopnamt");
            Label cuamt = (Label)e.Row.FindControl("lblgvcuamt");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string mACTDESC = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString().Trim();

            string level = this.DDListLevels.SelectedValue.ToString();
            if (code == "")
            {
                return;
            }
            if (code == "01DAAAAA" || code == "02IAAAAA")
            {
                hlink1.Style.Add("color", "green");
                lblcode.Style.Add("color", "green");
                clobal.Style.Add("color", "green");
                opnamt.Style.Add("color", "green");
                cuamt.Style.Add("color", "green");
                hlink1.Style.Add("font-weight", "bolder");
                lblcode.Style.Add("font-weight", "bolder");
                clobal.Style.Add("font-weight", "bolder");
                opnamt.Style.Add("font-weight", "bolder");
                cuamt.Style.Add("font-weight", "bolder");
            }
            else if (code == "01010000" || code == "01020000" || code == "02010000" || code == "02020000" || code == "02030000")
            {
                hlink1.Style.Add("color", "blue");
                lblcode.Style.Add("color", "blue");
                clobal.Style.Add("color", "blue");
                opnamt.Style.Add("color", "blue");
                cuamt.Style.Add("color", "blue");


            }
            else if (level == "4" && code.Length == 8 && (ASTUtility.Right(code, 2) != "00" && ASTUtility.Right(code, 2) != "AA"))
            {
                hlink1.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblcode.Attributes["style"] = "color:maroon; font-weight:bold;";
                clobal.Attributes["style"] = "color:maroon; font-weight:bold;";
                opnamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                cuamt.Attributes["style"] = "color:maroon; font-weight:bold;";

            }
            else if (level == "2")
            {
                if (code == "02010700" || code == "02010300")   //F_17_Acc/AccFinalReports.aspx?RepType=IS // code == "02010200" 
                {
                    hlink1.NavigateUrl = "LinkAccFinalReports.aspx?RepType=" + "IS" + "&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");

                }
                else
                {
                    hlink1.NavigateUrl = "LinkAccount.aspx?Type=BalanceDet&acgcode=" + code.Substring(0, 6) + "&Date1=" + this.txtDatefrom.Text.Trim() + "&Date2=" + this.txtDateto.Text.Trim() + "&mdesc=" + mACTDESC;

                }


            }
            else if (level == "4")
            {
                if (code.Substring(0, 4) == "2109" || code == "02010701")
                {
                    hlink1.NavigateUrl = "LinkAccFinalReports.aspx?RepType=" + "IS" + "&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
                }
                else
                {

                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + code +
                            "&Date1=" + this.txtDatefrom.Text.Trim() + "&Date2=" + this.txtDateto.Text.Trim();
                }
            }




        }
        private string Company()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Calltype = "";
            switch (comcod)
            {
                case "2305":
                case "3306":
                    Calltype = "BSR_WIP_COMPANY_0";
                    break;
                default:
                    Calltype = "BSR_COMPANY_0";
                    break;
            }
            return Calltype;
        }
        protected void GetIncomeStatement()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string Opndate = this.txtOpeningDate.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "ISR_COMPANY_0" +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, Opndate, "", "", "", "", "");

            this.dgvIS.Columns[2].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            this.dgvIS.Columns[3].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.dgvIS.DataSource = ds1.Tables[0];
            this.dgvIS.DataBind();
            ds1.Dispose();
            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvIS.HeaderRow.FindControl("hlbtnDetails")).NavigateUrl = "LinkAccount.aspx?Type=INDetails&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy"); ;


        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            int j;
            string grpcode;
            string RptType = this.txtflag.Text.Trim();
            switch (RptType)
            {

                case "IS":
                    grpcode = dt1.Rows[0]["grpcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                            dt1.Rows[j]["grpcode"] = "";



                        }

                        else
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                        }

                    }
                    break;



                case "SPBS":
                    grpcode = dt1.Rows[0]["grpcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }

                        else
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                        }

                    }
                    break;
                case "CSHFLW":
                case "CSHFLW2":

                    grpcode = dt1.Rows[0]["grp"].ToString();

                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpcode)
                            dt1.Rows[j]["grpdesc"] = "";
                        grpcode = dt1.Rows[j]["grp"].ToString();




                    }



                    break;


                default:
                    grpcode = dt1.Rows[0]["grp"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }

                        else
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                        }

                    }

                    break;

            }


            return dt1;


        }

        protected void dgvIS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvISDesc");
            Label lblgvcuamt = (Label)e.Row.FindControl("lblgvcuamt");
            Label lblgvopnamt = (Label)e.Row.FindControl("lblgvopnamt");
            Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobal");


            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (code == "3AAAAAAAAAAA")
            {
                hlink1.Style.Add("color", "blue");
                hlink1.NavigateUrl = "LinkAccount.aspx?Type=SalDetails&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }




            if (code == "030102AA" || code == "030201AA" || code == "030201CA" || code == "030202AA" || code == "030301AA" || code == "030302AA" || code == "030501AA" || code == "030502AA" || code == "03BAAAAA" || code == "03CAAAAA")
            {

                hlink1.Attributes["style"] = "color:blue; font-weight:bold;";
                lblgvcuamt.Attributes["style"] = "color:blue; font-weight:bold;";
                lblgvopnamt.Attributes["style"] = "color:blue; font-weight:bold;";
                lblgvclobal.Attributes["style"] = "color:blue; font-weight:bold;";

            }

            else if (ASTUtility.Right(code, 2) == "00")
            {
                hlink1.Attributes["style"] = "color:green; font-weight:bold;";
                lblgvcuamt.Attributes["style"] = "color:green; font-weight:bold;";
                lblgvopnamt.Attributes["style"] = "color:green; font-weight:bold;";
                lblgvclobal.Attributes["style"] = "color:green; font-weight:bold;";

            }

            else
            {

                hlink1.Attributes["style"] = "color:black;";
                lblgvcuamt.Attributes["style"] = "color:black;";
                lblgvopnamt.Attributes["style"] = "color:black; ";
                lblgvclobal.Attributes["style"] = "color:black;";
            }


        }


        private void SHOWSHAREEQUIT()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            Session.Remove("tblfinst");
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string Level = this.DDListLevels.SelectedValue.ToString();


            string levelval = (Level == "1") ? "2" : (Level == "2") ? "4" : (Level == "3") ? "8" : "12";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", "RPTSHAREHOLDEREQUITY", date1, date2, levelval, "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvsequ.DataSource = null;
                this.gvsequ.DataBind();

            }

            DataTable dt = ds2.Tables[0];
            Session["tblfinst"] = dt;

            this.gvsequ.Columns[2].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd MMMM yyyy");
            this.gvsequ.Columns[5].HeaderText = "Balance as at " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd MMMM yyyy");

            this.gvsequ.DataSource = dt;
            this.gvsequ.DataBind();

            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFopnamse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFcramtse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cram)", "")) ?
                        0.00 : dt.Compute("Sum(cram)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFdramtse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dram)", "")) ?
                        0.00 : dt.Compute("Sum(dram)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFclosamse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closam)", "")) ?
                        0.00 : dt.Compute("Sum(closam)", ""))).ToString("#,##0;(#,##0); - ");



        }

        private void ShowCashFlow()
        {

            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string Opendate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            string group = this.DDListLevels.SelectedValue.ToString();
            switch (group)
            {
                case "1":
                    group = "2";
                    break;
                case "2":
                    group = "4";
                    break;
                case "3":
                    group = "8";
                    break;
                case "4":
                    group = "12";
                    break;
            }
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTCASHFLOW", frmdate, todate, Opendate, group, "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvCashFlow.DataSource = null;
                this.grvCashFlow.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            // ViewState["recandpayNote"] = ds1.Tables[1];
            // this.grvCashFlow.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvCashFlow.Columns[3].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            this.grvCashFlow.Columns[4].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            this.grvCashFlow.DataSource = this.HiddenSameData(ds1.Tables[0]);
            this.grvCashFlow.DataBind();
            ds1.Dispose();

            for (int i = 0; i < grvCashFlow.Rows.Count; i++)
            {
                string actcode = ((Label)grvCashFlow.Rows[i].FindControl("lgvactcode")).Text.Trim();
                LinkButton lactDesc = (LinkButton)grvCashFlow.Rows[i].FindControl("lbtnactDesc");
                if (ASTUtility.Right(actcode, 4) != "AAAA")
                    lactDesc.CommandArgument = actcode;

            }



        }
        protected void lbtnactDesc_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string actcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["tblbdeposit"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "actcode like('" + actcode + "')";
            dt = dv1.ToTable();
            if (dt.Rows.Count == 0)
                return;

            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;
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
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=PrjReportRP&actcode=" +
                                mACTCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }
            else if (lebel2 == "")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=RPschedule&comcod=" + mCOMCOD + "&actcode=" +
                            mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=detailsTBRP&comcod=" + mCOMCOD + "&actcode=" +
                            mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
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
                    //  description.Style.Add("text-align", "right");


                }





                if (code == "FFFFAAAAAAAA")
                {

                    cuamt.Style.Add("color", "blue");
                    cuamt.NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtDatefrom.Text + "&Date2=" + this.txtDateto.Text;

                }
                else
                {

                    cuamt.Style.Add("color", "black");


                }



            }
        }

        private void ShowCashFlow02()
        {

            Session.Remove("tblbdeposit");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string Opendate = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            string Procedure = "SP_REPORT_ACCOUNTS_RP"; //(this.Request.QueryString["Type"] == "CashFlow02") ? "SP_REPORT_ACCOUNTS_RP" : "SP_REPORT_ACCOUNTS_RP_02";
            DataSet ds1 = accData.GetTransInfo(comcod, Procedure, "RPTCASHFLOW02", frmdate, todate, Opendate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvCashFlow02.DataSource = null;
                this.grvCashFlow02.DataBind();
                return;
            }
            Session["tblbdeposit"] = this.HiddenSameData(ds1.Tables[0]);
            this.grvCashFlow02.Columns[3].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "<br />" + " To " + "<br / >" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            this.grvCashFlow02.Columns[4].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + " To " + "<br / >" + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            //    this.grvCashFlow02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvCashFlow02.DataSource = this.HiddenSameData(ds1.Tables[0]);
            this.grvCashFlow02.DataBind();
            ds1.Dispose();
            // this.RPNote02();
            //for (int i = 0; i < grvCashFlow.Rows.Count; i++)
            //{
            //    string actcode = ((Label)grvCashFlow.Rows[i].FindControl("lgvactcode")).Text.Trim();
            //    LinkButton lactDesc = (LinkButton)grvCashFlow.Rows[i].FindControl("lbtnactDesc");
            //    if (ASTUtility.Right(actcode, 4) != "AAAA")
            //        lactDesc.CommandArgument = actcode;

            //}



        }
        protected void grvCashFlow02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label Desc = (Label)e.Row.FindControl("lblgvDesccf02");
                Label closam = (Label)e.Row.FindControl("lblgvclosamcf02");
                Label opnam = (Label)e.Row.FindControl("lblgvopnamcf02");
                HyperLink cuamt = (HyperLink)e.Row.FindControl("hlnkgvcuamtcf02");
                // Label ffamt = (Label)e.Row.FindControl("lgvffamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    Desc.Font.Bold = true;
                    closam.Font.Bold = true;
                    opnam.Font.Bold = true;
                    cuamt.Font.Bold = true;

                    // ffamt.Font.Bold = true;
                    Desc.Style.Add("text-align", "right");


                }
                if (code == "28BBBBAAAAAA")
                {

                    cuamt.Style.Add("color", "blue");
                    cuamt.NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtDatefrom.Text + "&Date2=" + this.txtDateto.Text;

                }




            }
        }
        protected void GetIncomeStatementForPrint()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            //string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            //string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            ////string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            ////DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "ISR_COMPANY_0" +
            ////        ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, "", "", "", "", "", "");

            ////if (ds1 == null)
            ////    return;

            ////mTRNDAT1 = this.txtDatefrom.Text;
            ////mTRNDAT2 = this.txtDateto.Text;
            ////mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();

            ////ReportDocument RptInSt = new RMGiRPT.R_15_Acc.RptAccIncomeSt();
            ////RptInSt.SetDataSource((DataTable)ViewState["tblAcc"]); 








            //DataTable dt1 = (DataTable)ViewState["tblAcc"];
            //var list1 = dt1.DataTableToList<MFGOBJ.C_15_Acc.EClassAccounts.NoteIncoStatement>();
            //LocalReport RptInSt = new LocalReport();
            //RptInSt = RptSetupClass1.GetLocalReport("RD_15_Acc.RptAccIncomeSt", list1, null, null);
            //RptInSt.SetParameters(new ReportParameter("comnam", comnam));
            //RptInSt.SetParameters(new ReportParameter("mTRNDAT2", Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy")));
            //RptInSt.SetParameters(new ReportParameter("mTRNDAT1", Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy")));

            ////TextObject TxtCompName = RptInSt.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            ////TxtCompName.Text = comnam;

            //////TextObject TxtRptTitle = RptInSt.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
            //////TxtRptTitle.Text = mLEVEL1;

            ////TextObject TxtRptPeriod = RptInSt.ReportDefinition.ReportObjects["TxtRptPeriod"] as TextObject;
            ////TxtRptPeriod.Text = "For the year ended " + Convert.ToDateTime(this.txtDateto.Text.Substring(0, 11)).ToString("dd MMMM yyyy");
            ////// TxtRptPeriod.Text = "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";

            ////TextObject txtdate = RptInSt.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            ////txtdate.Text = System.DateTime.Today.ToString("MMMM dd, yyyy");



            ////TextObject txtCuramt = RptInSt.ReportDefinition.ReportObjects["txtCuramt"] as TextObject; //"\n"+
            ////txtCuramt.Text = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            ////TextObject txtPreamt = RptInSt.ReportDefinition.ReportObjects["txtPreamt"] as TextObject;
            ////txtPreamt.Text = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");


            ////TextObject txtuserinfo = RptInSt.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            ////txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            ////RptInSt.SetParameterValue("ComLogo", ComLogo);
            ////--------------------Export to PDF--------------------------------------------------
            //Session["Report1"] = RptInSt;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void GetBalanceSheetForPrint()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string mTRNDAT1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            //string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            //string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();

            ////string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            //////DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "BSR_COMPANY_0" +
            //////        ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, "", "", "", "", "", "");

            //////if (ds1 == null)
            //////    return;

            //DataTable dt = (DataTable)ViewState["tblAcc"];
            //var list1 = dt.DataTableToList<MFGOBJ.C_15_Acc.EClassAccounts.Rptspbalancesheet>();
            //LocalReport RptBalanceSheet = new LocalReport();
            //RptBalanceSheet = RptSetupClass1.GetLocalReport("RD_15_Acc.RptBalanceSheet2", list1, null, null);
            //RptBalanceSheet.SetParameters(new ReportParameter("comnam", comnam));
            //RptBalanceSheet.SetParameters(new ReportParameter("mTRNDAT2", mTRNDAT2));
            //RptBalanceSheet.SetParameters(new ReportParameter("mTRNDAT1", mTRNDAT1));
            ////rpt1.SetParameters(new ReportParameter("RptTitle", "Purchase Requisition"));


            ////ReportDocument RptBalanceSheet = new RMGiRPT.R_15_Acc.RptBalanceSheetAlli();
            ////RptBalanceSheet.SetDataSource((DataTable)ViewState["tblAcc"]);

            ////DataTable dt = (DataTable)ViewState["tblAcc"];

            ////TextObject TxtCompName = RptBalanceSheet.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            ////TxtCompName.Text = comnam;

            //////TextObject TxtRptTitle = RptBalanceSheet.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
            //////TxtRptTitle.Text =  mLEVEL1;

            ////TextObject TxtRptPeriod = RptBalanceSheet.ReportDefinition.ReportObjects["TxtRptPeriod"] as TextObject;
            ////TxtRptPeriod.Text = "As at " + Convert.ToDateTime(mTRNDAT2).ToString("dd MMMM, yyyy");


            ////TextObject TxtOpening = RptBalanceSheet.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            ////TxtOpening.Text = Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy");

            ////TextObject TxtClosing = RptBalanceSheet.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            ////TxtClosing.Text = Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy");


            //////TextObject txtrptsysdate = RptBalanceSheet.ReportDefinition.ReportObjects["txtrptsysdate"] as TextObject;
            //////txtrptsysdate.Text = System.DateTime.Today.ToString("MMMM dd, yyyy");

            //////TextObject txtuserinfo = RptBalanceSheet.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //////txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////RptBalanceSheet.SetParameterValue("ComLogo", ComLogo);

            ////--------------------Export to PDF--------------------------------------------------
            ////Session["Report1"] = RptBalanceSheet;
            ////((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ////             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Session["Report1"] = RptBalanceSheet;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


        }
        private void PrintShareQty()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblfinst"];
            //if (dt1.Rows.Count == 0)
            //    return;

            //ReportDocument RptShareQty = new RMGiRPT.R_15_Acc.RptShareQty();

            //TextObject TxtCompName = RptShareQty.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //TxtCompName.Text = comnam;

            //TextObject txtdate = RptShareQty.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "For the year ended " + Convert.ToDateTime(this.txtDateto.Text.Substring(0, 11)).ToString("dd MMMM yyyy");
            //TextObject rpttxtopening = RptShareQty.ReportDefinition.ReportObjects["rpttxtopening"] as TextObject;
            //rpttxtopening.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd MMMM yyyy");

            //TextObject rpttxtclosing = RptShareQty.ReportDefinition.ReportObjects["rpttxtclosing"] as TextObject;
            //rpttxtclosing.Text = "Balance as at " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd MMMM yyyy");

            //// txtdate.Text = "(Form " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";




            //RptShareQty.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //RptShareQty.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = RptShareQty;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void RptCashFlow02()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            //DataTable dt1 = (DataTable)ViewState["recandpayNote"];





            //ReportDocument rptstate = new RMGiRPT.R_15_Acc.RptCashFlow02();

            //// rptstate.Subreports["RptBankBalance02.rpt"].SetDataSource(dt1);

            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = comnam;
            //string reptype = this.txtflag.Text.Trim().ToString();
            //TextObject TxtHeader = rptstate.ReportDefinition.ReportObjects["TxtHeader"] as TextObject;
            //TxtHeader.Text = (reptype == "CSHFLW") ? "Statement of Cash Flow" : "Statement of Cash Flow -Indirect";

            //TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            //rptftdate.Text = "For the year ended " + Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd MMMM yyyy");
            ////rptftdate.Text = "Date: " + fromdate + " To " + todate;




            //TextObject txtCuramt = rptstate.ReportDefinition.ReportObjects["txtCuramt"] as TextObject;
            //txtCuramt.Text = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            //TextObject txtPreamt = rptstate.ReportDefinition.ReportObjects["txtPreamt"] as TextObject;
            //txtPreamt.Text = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");





            ////TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            ////txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;


            //rptstate.SetDataSource((DataTable)Session["tblbdeposit"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Cash Flow";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}


            //Session["Report1"] = rptstate;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
    }
}