﻿using System;
using System.Collections;
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
using SPERDLC;

namespace SPEWEB.F_15_DPayReg
{
    public partial class AccOnlinePaymentRa : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.CommonButton();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtpaymentdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtPayDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "ChequeReady") ? "Payment Checked" : "Payment Forward";

                this.lbtnOk_Click(null, null);
            }

        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
           
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            string Type = this.Request.QueryString["Type"].ToString();
            if (Type == "ChequeReady")
            {
                
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Checked";
            }
            else
            {
                
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Audit Checked";
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string strScript = "window.close();";
            ScriptManager.RegisterStartupScript(this, typeof(string), "key", strScript, true);
        }
        private string GetCompCode()
        {
            if (this.Request.QueryString["comcod"].Length == 0)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }
            else
            {
                return (this.Request.QueryString["comcod"].ToString());
            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.PanelNote.Visible = true;
            // this.lbtnBankPos.Visible = true;
            this.ShowChequeReady();
            //this.Note();
            this.ShowNote();
        }

        private void ShowChequeReady()
        {
            ViewState.Remove("tblpayment");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            // string refno = "%" + this.txtSearch.Text.Trim() + "%";
            string date = Convert.ToDateTime(this.txtpaymentdate.Text).ToString("dd-MMM-yyyy");//System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Chkdate = Convert.ToDateTime(this.txtPayDate.Text).ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();
            string approved = (this.Request.QueryString["Type"].ToString().Trim() == "ChequeApproval") ? "approved" : "";
            string refno = (this.Request.QueryString["genno"].ToString()).Length == 0 ? this.txtSearch.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";
            //string refno = "%" + this.txtSearch.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "CHECKEDALLPAYMENTID", refno, date, "12", Chkdate, userid, approved, "", "", "");
            if (ds1 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                return;

            }

            DataTable dt2 = HiddenSameData(ds1.Tables[0]);
            ViewState["tblpayment"] = dt2;
            //ViewState["tblNote"] = ds1.Tables[1];
            //this.Note();

            this.Data_Bind();
            string reqnar = "";
            foreach (DataRow row in dt2.Rows)
            {
                reqnar += row["remarks"].ToString() + " , ";
            }
            this.txtNarration.Text = reqnar;

            ds1.Dispose();

        }
        //private void ShowChequeReady()
        //{
        //    ViewState.Remove("tblpayment");
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = this.GetCompCode();
        //    // string refno = "%" + this.txtSearch.Text.Trim() + "%";
        //    string date = Convert.ToDateTime(this.txtpaymentdate.Text).ToString("dd-MMM-yyyy");//System.DateTime.Today.ToString("dd-MMM-yyyy");
        //    string Chkdate = Convert.ToDateTime(this.txtPayDate.Text).ToString("dd-MMM-yyyy");
        //    string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
        //    string approved = (this.Request.QueryString["Type"].ToString().Trim() == "ChequeApproval") ? "approved" : "";
        //    string refno = (this.Request.QueryString["genno"].ToString()).Length == 0 ? this.txtSearch.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";
        //    //string refno = "%" + this.txtSearch.Text.Trim() + "%";

        //    DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCHEQUEREADY", refno, date, "12", Chkdate, userid, approved, "", "", "");
        //    if (ds1 == null)
        //    {
        //        this.gvPayment.DataSource = null;
        //        this.gvPayment.DataBind();
        //        return;

        //    }


        //    ViewState["tblpayment"] = HiddenSameData(ds1.Tables[0]);
        //    ViewState["tblNote"] = ds1.Tables[1];
        //    this.Data_Bind();
        //    ds1.Dispose();

        //}
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChequeApproval":
                case "ChequeReady":
                    string slnum = dt1.Rows[0]["slnum"].ToString();
                    string actcode = dt1.Rows[0]["actcode"].ToString();
                    string apppaydate = dt1.Rows[0]["apppaydate1"].ToString();
                    string refno = dt1.Rows[0]["refno"].ToString();


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["slnum"].ToString() == slnum && dt1.Rows[j]["actcode"].ToString() == actcode && dt1.Rows[j]["apppaydate1"].ToString() == apppaydate)
                        {

                            dt1.Rows[j]["actdesc"] = "";
                            dt1.Rows[j]["slnum1"] = "";
                            dt1.Rows[j]["apppaydate1"] = "";
                            dt1.Rows[j]["refno"] = "";

                        }
                        if (dt1.Rows[j]["slnum"].ToString() == slnum)
                        {
                            // actcode = dt1.Rows[j]["actcode"].ToString();

                            dt1.Rows[j]["apppaydate1"] = "";
                            dt1.Rows[j]["slnum1"] = "";
                            dt1.Rows[j]["actdesc"] = "";
                            dt1.Rows[j]["refno"] = "";

                        }

                        if (dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            // actcode = dt1.Rows[j]["actcode"].ToString();


                            dt1.Rows[j]["actdesc"] = "";

                        }

                        slnum = dt1.Rows[j]["slnum"].ToString();
                        actcode = dt1.Rows[j]["actcode"].ToString();
                        apppaydate = dt1.Rows[j]["apppaydate1"].ToString();
                        refno = dt1.Rows[j]["refno"].ToString();


                    }

                    break;

            }


            return dt1;
        }

        private void ShowNote()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            // string refno = "%" + this.txtSearch.Text.Trim() + "%";
            string date = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();//  Convert.ToDateTime(this.txtpaymentdate.Text).ToString("dd-MMM-yyyy");//System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Chkdate = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString(); // Convert.ToDateTime(this.txtpaymentdate.Text).ToString("dd-MMM-yyyy");
            string userid = hst["usrid"].ToString();
            string approved = (this.Request.QueryString["Type"].ToString().Trim() == "ChequeApproval") ? "approved" : "";
            string refno = (this.Request.QueryString["genno"].ToString()).Length == 0 ? this.txtSearch.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";
            //string refno = "%" + this.txtSearch.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCHEQUEREADY", refno, date, "12", Chkdate, userid, approved, "", "", "");
            if (ds1 == null)
            {
                return;

            }


            //  ViewState["tblpayment"] = HiddenSameData(ds1.Tables[0]);
            ViewState["tblNote"] = ds1.Tables[1];
            this.Note();


        }
        private void Note()
        {
            DataTable dt = (DataTable)ViewState["tblNote"];
            if (dt.Rows.Count > 0)
            {
                this.Hyplnk.Text = Convert.ToDouble(dt.Rows[0]["closdram"]).ToString("#,##0;(#,##0) ;");

                //this.lblClAmt.Text =  Convert.ToDouble(dt.Rows[0]["closdram"]).ToString("#,##0;(#,##0) ;");
                this.lbllssueAmt.Text = Convert.ToDouble(dt.Rows[0]["isuamt"]).ToString("#,##0;(#,##0) ;");
                this.lblCollAmt.Text = Convert.ToDouble(dt.Rows[0]["collamt"]).ToString("#,##0;(#,##0) ;");
                this.lblnetBal.Text = Convert.ToDouble(dt.Rows[0]["bankbal"]).ToString("#,##0;(#,##0) ;");

            }
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblpayment"];
            this.gvPayment.DataSource = dt;
            this.gvPayment.DataBind();
            this.FooterCalculation();
        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblpayment"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvPayment.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ? 0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");
            ((Label)this.gvPayment.FooterRow.FindControl("txtFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(balamt)", "")) ? 0.00 : dt.Compute("Sum(balamt)", ""))).ToString("#,##0;(#,##0); -");
            ((Label)this.gvPayment.FooterRow.FindControl("txtFToday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(apamt)", "")) ? 0.00 : dt.Compute("Sum(apamt)", ""))).ToString("#,##0;(#,##0); -");


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }



        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblpayment"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {

                //tbl1.Rows[i]["refno"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvref")).Text.Trim();
                tbl1.Rows[i]["nochq"] = Convert.ToDouble("0" + ((Label)this.gvPayment.Rows[i].FindControl("lblgvNochq")).Text.Trim()).ToString();
                tbl1.Rows[i]["apamt"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvPayment.Rows[i].FindControl("lblgvAppramt")).Text.Trim()));
                //Convert.ToDouble("0" + ((Label)this.gvPayment.Rows[i].FindControl("lblgvAppramt")).Text.Trim()).ToString();
                //tbl1.Rows[i]["apppaydate"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvpaymentdate")).Text.Trim();
                tbl1.Rows[i]["remarks"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                //tbl1.Rows[i]["chqready"] = (((CheckBox)this.gvPayment.Rows[i].FindControl("chkready")).Checked) ? "True" : "False";



            }
            ViewState["tblpayment"] = tbl1;

        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ChequeReady":

                    this.UpdateChequeReady();
                    break;



                case "ChequeApproval":
                    this.UpdateChequeApproval();
                    break;

            }





        }

        private void UpdateChequeReady()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                this.SaveValue();

                DataTable dt1 = (DataTable)ViewState["tblpayment"];
                bool result = true;



                //  string CallType = (this.Request.QueryString["Type"].ToString().Trim() == "ChequeApproval") ? "UPDATECHEQUEREAPPROVAL" : "UPDATECHEQUEREADY";


                foreach (DataRow dr in dt1.Rows)
                {
                    string slnum = dr["slnum"].ToString().Trim();
                    string actcode = dr["actcode"].ToString().Trim();
                    string rescode = dr["rescode"].ToString().Trim();
                    string billno = dr["billno1"].ToString().Trim();
                    string refno = dr["refno"].ToString().Trim();

                    double amt = Convert.ToDouble(ASTUtility.ExprToValue("0" + dr["apamt"].ToString()));
                    //Convert.ToDouble("0" + dr["apamt"].ToString());
                    double nochq = Convert.ToDouble("0" + dr["nochq"].ToString());
                    string apppaydate = ASTUtility.DateFormat(dr["apppaydate"].ToString());
                    string chqreadydate = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();
                    string Remarks = dr["remarks"].ToString();
                    if (Convert.ToDouble(dr["balamt"]) < Convert.ToDouble(dr["apamt"]))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Amount Equal or Below Balance  Amount');", true);
                        return;
                    }


                    if (amt != 0)
                    {
                        result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "UPDATECHEQUEREADY", slnum, chqreadydate, amt.ToString(),
                                                                   apppaydate, nochq.ToString(), Remarks, userid, Terminal, Sessionid, Date, actcode, rescode, billno, "", "");
                    }
                    if (result == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                    }


                }







            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }


        }


        private void UpdateChequeApproval()
        {


            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                this.SaveValue();

                DataTable dt1 = (DataTable)ViewState["tblpayment"];
                bool result = true;





                foreach (DataRow dr in dt1.Rows)
                {
                    string slnum = dr["slnum"].ToString().Trim();
                    string actcode = dr["actcode"].ToString().Trim();
                    string rescode = dr["rescode"].ToString().Trim();
                    string billno = dr["billno1"].ToString().Trim();
                    string refno = dr["refno"].ToString().Trim();

                    double amt = Convert.ToDouble(ASTUtility.ExprToValue("0" + dr["apamt"].ToString()));//Convert.ToDouble("0" + dr["apamt"].ToString());
                    double nochq = Convert.ToDouble("0" + dr["nochq"].ToString());
                    string apppaydate = ASTUtility.DateFormat(dr["apppaydate"].ToString());
                    string chqreadydate = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();
                    string Remarks = dr["remarks"].ToString();


                    if (amt != 0)
                    {
                        result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "UPDATECHEQUEREAPPROVAL", slnum, chqreadydate, amt.ToString(),
                                                                   apppaydate, nochq.ToString(), Remarks, userid, Terminal, Sessionid, Date, actcode, rescode, billno, "", "");
                    }
                    if (result == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                    }


                }

                if (hst["compsms"].ToString() == "True")
                {


                    if (this.Request.QueryString["Type"] == "ChequeApproval")
                    {

                        SendSmsProcess sms = new SendSmsProcess();
                        string comnam = hst["comnam"].ToString();
                        string compname = hst["compname"].ToString();
                        string frmname = "AccOnlinePaymentApp.aspx?Type=ChequePayment";
                        string SMSHead = "Ready for  Bill Payment Approval, ";
                        string issueno = this.Request.QueryString["payid"].ToString();


                        string SMSText = comnam + ":\n" + SMSHead + "\n" + "\n" + "Issue No: " + issueno;
                        bool resultsms = sms.SendSmms(SMSText, userid, frmname);


                    }



                }








            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.FooterCalculation();
        }
        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string slnum = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lbgvslnum")).Text.Trim();

            //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETEPAYPRO", slnum, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //    return;

            //this.lmsg.Text = "Updated Successfully";




            DataTable dt = (DataTable)ViewState["tblpayment"];
            string slnum = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lbgvslnum")).Text.Trim();
            //int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.RowIndex;;  

            // dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("slnum<>'" + slnum + "'");
            ViewState.Remove("tblpayment");
            ViewState["tblpayment"] = dv.ToTable();
            this.Data_Bind();


        }


        protected void lbtnBankPos_Load(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string Date = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();

            this.lbtnBankPos.NavigateUrl = "LinkPayAcc.aspx?Type=BankPosition02&comcod=" + comcod + "&Date1=" + Date;
        }
        protected void lbtnOk0_Click(object sender, EventArgs e)
        {

        }

    }
}