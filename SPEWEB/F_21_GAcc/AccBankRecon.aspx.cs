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
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_21_GAcc
{
    public partial class AccBankRecon : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                {
                    if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                        Response.Redirect("../AcceessError.aspx");
                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                    ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Bank Reconcilation";
                }
                this.GetBankName();
                if (this.TxtDate1.Text.Trim().Length == 0)
                {
                    this.TxtDate1.Text = DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.TxtDate2.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                }
                this.CommonButton();
                this.lbtnGetData_Click(null, null);
            }
        }

         protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(btnGetDataP_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);



        
        }
        private void CommonButton()
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;


            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = true;
            //((CheckBox)this.Master.FindControl("CheckBox1")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Accounts Code";
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Details Code";
            ((CheckBox)this.Master.FindControl("chkBoxN")).Text = "Cheque Print";
            ((CheckBox)this.Master.FindControl("CheckBox1")).Text = "A/C Payee";


        }
        private void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            string bankcode = this.DDListBank.SelectedValue.ToString();
            this.SaveValue();
            this.PutSameValueVounum();
            string comcod = this.GetCompCode();
            DataTable dt = ((DataTable)Session["tbl01r"]);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string mVOUNUM = dt.Rows[i]["vounum"].ToString();
                string RefNo = dt.Rows[i]["refnum"].ToString();
                string mACTCODE = dt.Rows[i]["actcode"].ToString();
                string mSUBCODE = dt.Rows[i]["rescode"].ToString();
                string mCACTCODE = dt.Rows[i]["cactcode"].ToString();
                string mRECNDT1 = Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");//(ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy")) : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");
                                                                                                   //string mVOUNUM1 = (i == 0) ? dt.Rows[i]["vounum"].ToString() : dt.Rows[i - 1]["vounum"].ToString();
                                                                                                   //mRECNDT1 = (i == 0) ? mRECNDT1 :
                                                                                                   //    ((mVOUNUM == mVOUNUM1) ? ((ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i - 1]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i - 1]["recndt"]).ToString("dd-MMM-yyyy")) : Convert.ToDateTime(dt.Rows[i - 1]["recndt"]).ToString("dd-MMM-yyyy"))
                                                                                                   //    : mRECNDT1);

                DateTime voudat = Convert.ToDateTime(dt.Rows[i]["voudat"]);

                string mUserID = "000000";
                bool dcon = (mRECNDT1 == "01-Jan-1900" || mVOUNUM.Substring(0, 2) == "BC" || mVOUNUM.Substring(0, 2) == "CC") ? true : ASITUtility02.TransReconDate(Convert.ToDateTime(mRECNDT1), voudat);
                if ((!dcon))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Reconcilation Date is equal or greater Voucher Date');", true);
                    return;
                }
                bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPDATEBANKRECON", mRECNDT1, mVOUNUM, mACTCODE, mSUBCODE, mCACTCODE, "", "", "", "", "", "", "", "", "", mUserID);

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }

         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Bank Reconcilaition";
                string eventdesc = "Update Reconcilaition";
                string eventdesc2 = "Bank Name: " + this.DDListBank.SelectedItem.ToString().Substring(13);
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void ibtnFindBankName_Click(object sender, EventArgs e)
        {
            if (this.lbtnGetData.Text == "New")
                return;
            this.GetBankName();
        }
        private void GetBankName()
        {

            string mCOMCOD = this.GetCompCode();
            string mFILTERSTR =  "%";
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBANKNAME", mFILTERSTR, "", "", "", "", "", "", "", "");
            this.DDListBank.DataTextField = "actdesc1";
            this.DDListBank.DataValueField = "actcode";
            this.DDListBank.DataSource = ds1.Tables[0];
            this.DDListBank.DataBind();
        }

        protected void lbtnGetData_Click(object sender, EventArgs e)
        {
            if (this.lbtnGetData.Text == "Ok")
            {
                this.lbtnGetData.Text = "New";
                this.DDListBank.Enabled = false;
                this.TxtDate1.Enabled = false;
                this.TxtDate2.Enabled = false;
                this.LoadData();
            }
            else
            {
                this.lbtnGetData.Text = "Ok";
                this.DDListBank.Enabled = true;
                this.TxtDate1.Enabled = true;
                this.TxtDate2.Enabled = true;
                this.gv1.DataSource = null;
                this.gv1.DataBind();
            }
        }
        private void LoadData()
        {
            Session.Remove("tbl01r");
            string mCOMCOD = this.GetCompCode();
            string mACCODE = this.DDListBank.SelectedValue.ToString();
            string mTRNDAT1 = this.TxtDate1.Text;
            string mTRNDAT2 = this.TxtDate2.Text;
            string chqno = this.txtChqSearch.Text + "%";
            string Type = (this.Request.QueryString["Type"].ToString() == "Mgt") ? "Mgt" : "";
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_ENTRY_ACCOUNTS_VOUCHER", "SHOWBANKRECON", mACCODE, mTRNDAT1, mTRNDAT2, chqno, Type, "", "", "", "");
            if (ds1 == null)
            {
                this.gv1.DataSource = null;
                this.gv1.DataBind();
                return;
            }

            Session["tbl01r"] = ds1.Tables[0];
            this.gv1_DataBind();

        }

        protected void btnGetDataP_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetCompCode();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string mACCODE = this.DDListBank.SelectedValue.ToString();
            string mTRNDAT1 = this.TxtDate1.Text;
            string mTRNDAT2 = this.TxtDate2.Text;
            DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTBANKRECON", mACCODE, mTRNDAT1, mTRNDAT2, "", "", "", "", "", "");

            //DataTable dt = (DataTable) Session["tbl01r"];
            DataTable dt = ds.Tables[0];
            var lst = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.RptBankReconciliation>();

            

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            ////string hostname = hst["hostname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string asondate = "As On Date: " + Convert.ToDateTime(txttodate.Text).ToString("dd-MMM-yyyy");

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptBankReconciliation", lst, "", "");
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("rpttitle", ""));
            //rpt1.SetParameters(new ReportParameter("asondate", asondate));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument RptBankRecon = new RMGiRPT.R_21_GAcc.RptBankReconc();
            ////TextObject TxtCompName = RptBankRecon.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            ////TxtCompName.Text = Convert.ToString(ds1.Tables[1].Rows[0]["comnam"]);

            //TextObject TxtRptTitle = RptBankRecon.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
            //TxtRptTitle.Text = this.DDListBank.SelectedItem.Text.Trim().Substring(13);

            //TextObject TxtRptPeriod = RptBankRecon.ReportDefinition.ReportObjects["TxtRptPeriod"] as TextObject;
            //TxtRptPeriod.Text = "(As on " + mTRNDAT2 + ")";
            //TextObject txtuserinfo = RptBankRecon.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //RptBankRecon.SetDataSource(ds1.Tables[0]);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Bank Reconcilaition";
            //    string eventdesc = "Print Reconcilaition";
            //    string eventdesc2 = "Bank Name: " + this.DDListBank.SelectedItem.ToString().Substring(13);
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}


            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //RptBankRecon.SetParameterValue("ComLogo", ComLogo);

            ////--------------------Export to PDF--------------------------------------------------
            //Session["Report1"] = RptBankRecon;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Response.Redirect("PDFViewer.aspx");
        }

        //private DataTable HiddenSaveValue(DataTable dt)
        //{
        //    if (dt == null || dt.Rows.Count == 0)
        //        return dt;

        //    string voudat = "";
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (voudat == Convert.ToDateTime(dt.Rows[i]["voudat"]).ToString())
        //        {
        //            dt.Rows[i]["voudat"] = "";
        //        }
        //        voudat = dt.Rows[i]["voudat"].ToString();
        //    }
        //    return dt;
        //}


        protected void lbtnGetBankList_Click(object sender, EventArgs e)
        {

        }

        

        protected void gv1_DataBind()
        {

            this.gv1.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gv1.DataSource = (DataTable)Session["tbl01r"];
            this.gv1.DataBind();
        }

        

        private void SaveValue()
        {

            DataTable dt = ((DataTable)Session["tbl01r"]);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int TblRowIndex;
            for (int i = 0; i < this.gv1.Rows.Count; i++)
            {
                string mRECNDT1 = ((TextBox)this.gv1.Rows[i].FindControl("txtgvRECNDT")).Text.Trim() + "01.01.1900";


                //string mVOUNUM1 = (i == 0) ? dt.Rows[i]["vounum"].ToString() : dt.Rows[i-1]["vounum"].ToString(); 
                //mRECNDT1=(i==0)?mRECNDT1:
                //    ((mVOUNUM==mVOUNUM1)?((ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i-1]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i-1]["recndt"]).ToString("dd-MMM-yyyy")):Convert.ToDateTime(dt.Rows[i-1]["recndt"]).ToString("dd-MMM-yyyy"))
                //    : mRECNDT1);

                mRECNDT1 = mRECNDT1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(mRECNDT1.Substring(3, 2))] + "-" + mRECNDT1.Substring(6, 4);
                TblRowIndex = (gv1.PageIndex) * gv1.PageSize + i;
                dt.Rows[TblRowIndex]["recndt"] = mRECNDT1;
            }
            Session["tbl01r"] = dt;

        }

        protected void gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gv1.PageIndex = e.NewPageIndex;
            this.gv1_DataBind();
        }
        private void PutSameValueVounum()
        {
            DataTable dt = ((DataTable)Session["tbl01r"]);
            string bankcode = this.DDListBank.SelectedValue.ToString();
            string mVOUNUM = "", mRECNDT1 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    dt.Rows[i]["recndt"] = (ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy")) : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");
                    mVOUNUM = dt.Rows[i]["vounum"].ToString();
                    mRECNDT1 = Convert.ToDateTime(dt.Rows[i]["recndt"].ToString()).ToString("dd-MMM-yyyy");

                }


                if (mVOUNUM == dt.Rows[i]["vounum"].ToString())
                {
                    dt.Rows[i]["recndt"] = mRECNDT1;
                    mVOUNUM = dt.Rows[i]["vounum"].ToString();
                    mRECNDT1 = (ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy")) : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");

                }
                else
                {
                    dt.Rows[i]["recndt"] = (ASTUtility.Left(bankcode, 4) == "1901") ? ((Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Convert.ToDateTime(dt.Rows[i]["voudat"]).ToString("dd-MMM-yyyy") : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy")) : Convert.ToDateTime(dt.Rows[i]["recndt"]).ToString("dd-MMM-yyyy");
                    mVOUNUM = dt.Rows[i]["vounum"].ToString();
                    mRECNDT1 = Convert.ToDateTime(dt.Rows[i]["recndt"].ToString()).ToString("dd-MMM-yyyy");

                }
            }


            Session["tbl01r"] = dt;

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.gv1_DataBind();
        }
        

        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lblVOUNUM1");
                string comcod = this.GetCompCode();
                Label lblREFNO = (Label)e.Row.FindControl("lblREFNO");
                Label lblgvDetailsHead = (Label)e.Row.FindControl("lblgvDetailsHead");
                string voucher = ((HyperLink)e.Row.FindControl("lblVOUNUM")).Text.ToString();

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                if (grp == "B")
                {
                    lblREFNO.Font.Bold = true;
                    lblgvDetailsHead.Font.Bold = true;
                    e.Row.Attributes["style"] = "background:DarkSeaGreen;";
                    //lblREFNO.Attributes["style"]="color:blue;";
                    //lblgvDetailsHead.Attributes["style"] = "color:blue;";
                }

                hlink.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher + "&comcod=" + comcod;


            }
        }
    }
}