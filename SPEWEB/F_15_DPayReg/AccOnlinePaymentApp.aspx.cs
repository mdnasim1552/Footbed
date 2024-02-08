using System;
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
using CrystalDecisions.CrystalReports.Engine;
using SPELIB;

namespace SPEWEB.F_15_DPayReg
{
    public partial class AccOnlinePaymentApp : System.Web.UI.Page
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
                this.txtpaymentdate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Payment Approval Information";
                this.lbtnOk_Click(null, null);
            }

        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approved";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
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
            this.ShowChequeApp();
        }


        private void ShowChequeApp()
        {
            ViewState.Remove("tblpayment");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string refno = (this.Request.QueryString["genno"].ToString()).Length == 0 ? this.txtSearch.Text.Trim() + "%" : this.Request.QueryString["genno"].ToString() + "%";
            //string refno = "%" + this.txtSearch.Text.Trim() + "%";
            string date = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCHEQUEPAYMENT", refno, date, userid, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                return;

            }
            ViewState["tblpayment"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ChequePayment":
                    string useridapp = dt1.Rows[0]["useridapp"].ToString();
                    string actcode = dt1.Rows[0]["actcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["useridapp"].ToString() == useridapp && dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            useridapp = dt1.Rows[j]["useridapp"].ToString();
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            dt1.Rows[j]["usrdesig"] = "";
                            dt1.Rows[j]["actdesc"] = "";
                        }
                        if (dt1.Rows[j]["useridapp"].ToString() == useridapp)
                        {
                            useridapp = dt1.Rows[j]["useridapp"].ToString();

                            dt1.Rows[j]["usrdesig"] = "";

                        }
                        //if (dt1.Rows[j]["actcode"].ToString() == actcode)
                        //{
                        //    actcode = dt1.Rows[j]["actcode"].ToString();
                        //    dt1.Rows[j]["actdesc"] = "";
                        //}

                        useridapp = dt1.Rows[j]["useridapp"].ToString();
                        actcode = dt1.Rows[j]["actcode"].ToString();
                    }

                    break;





            }

            return dt1;

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

            ((Label)this.gvPayment.FooterRow.FindControl("lblFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ? 0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");
            ((Label)this.gvPayment.FooterRow.FindControl("lblFApamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(apamt)", "")) ? 0.00 : dt.Compute("Sum(apamt)", ""))).ToString("#,##0;(#,##0); -");
            ((Label)this.gvPayment.FooterRow.FindControl("lblFBalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(balamt)", "")) ? 0.00 : dt.Compute("Sum(balamt)", ""))).ToString("#,##0;(#,##0); -");


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintReadyCheque();
        }

        private void PrintReadyCheque()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RMGiRPT.R_15_DPayReg.RptAccOnlinePay();
            DataTable dt = (DataTable)ViewState["tblpayment"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtdate.Text = "Date: " + ASTUtility.DateInVal(this.txtpaymentdate.Text).ToString();
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void SaveValue()
        {


            DataTable tbl1 = (DataTable)ViewState["tblpayment"];
            for (int i = 0; i < this.gvPayment.Rows.Count; i++)
            {

                tbl1.Rows[i]["apamt"] = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAppramt")).Text.Trim()));
                //Convert.ToDouble("0" + ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvAppramt")).Text.Trim()).ToString();
                tbl1.Rows[i]["nochq"] = Convert.ToDouble("0" + ((Label)this.gvPayment.Rows[i].FindControl("lblgvchq")).Text.Trim()).ToString();
                tbl1.Rows[i]["remarks"] = ((TextBox)this.gvPayment.Rows[i].FindControl("txtgvRemarks")).Text.Trim().ToString();
                tbl1.Rows[i]["chkapp"] = (((CheckBox)this.gvPayment.Rows[i].FindControl("chkapp")).Checked) ? "True" : "False";

            }
            ViewState["tblpayment"] = tbl1;

        }





        protected void lbtnTotal_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            this.Data_Bind();





        }



        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {

                case "ChequePayment":
                    this.UpdateChequeReady();
                    break;


            }





        }

        private void UpdateChequeReady()
        {
            //lmsg.Visible = true;

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();


                this.SaveValue();

                DataTable dt1 = (DataTable)ViewState["tblpayment"];
                bool result = true;



                foreach (DataRow dr in dt1.Rows)
                {
                    string slnum = dr["slnum"].ToString().Trim();
                    string actcode = dr["actcode"].ToString().Trim();
                    string rescode = dr["rescode"].ToString().Trim();
                    string billno = dr["billno1"].ToString().Trim();
                    double amt = Convert.ToDouble(ASTUtility.ExprToValue("0" + dr["apamt"].ToString()));//Convert.ToDouble("0" + dr["apamt"].ToString());
                    double nochq = Convert.ToDouble("0" + dr["nochq"].ToString());
                    string apppaydate = ASTUtility.DateFormat(dr["apppaydate"].ToString());
                    string fiappdate = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();
                    string Approved = "Ok";
                    string remarks = dr["remarks"].ToString().Trim();
                    string chkapp = dr["chkapp"].ToString().Trim();
                    if (chkapp == "True")
                    {
                        result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "UPDATECHEQUEPTPARTY", slnum, actcode, rescode,
                                                                   amt.ToString(), nochq.ToString(), apppaydate, Approved, remarks, fiappdate, userid, Terminal, Sessionid, billno, "", "");
                    }
                    if (result == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        return;
                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

                    }


                }




                //this.ShowChequeApp();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }


        }


        protected void txtpaymentdate_TextChanged(object sender, EventArgs e)
        {
            this.txtpaymentdate.Text = ASTUtility.DateInVal(this.txtpaymentdate.Text);
        }
        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblpayment"];
            string slnum = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lbgvslnum")).Text.Trim();
            //int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.RowIndex;
            // dt.Rows[rowindex].Delete();



            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETEPAYAPP", slnum, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }





            DataView dv = dt.DefaultView;
            dv.RowFilter = ("slnum<>'" + slnum + "'");
            ViewState.Remove("tblpayment");
            ViewState["tblpayment"] = dv.ToTable();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Deleted Row');", true);

            this.Data_Bind();




            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string slnum = ((Label)this.gvPayment.Rows[e.RowIndex].FindControl("lbgvslnum")).Text.Trim();
            //string Date = ASTUtility.DateFormat(this.txtpaymentdate.Text).ToString();
            //bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETEPAYAPP", slnum, Date, "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //    return;

            //this.ShowChequeApp();
        }

        protected void chkAllapp_CheckedChanged(object sender, EventArgs e)
        {

            int i, index;
            DataTable dt = (DataTable)ViewState["tblpayment"];

            if (((CheckBox)this.gvPayment.HeaderRow.FindControl("chkAllapp")).Checked)
            {

                for (i = 0; i < this.gvPayment.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPayment.Rows[i].FindControl("chkapp")).Checked = true;
                    //  ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = true;
                    index = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + i;
                    dt.Rows[index]["chkapp"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPayment.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPayment.Rows[i].FindControl("chkapp")).Checked = false;
                    // ((LinkButton)this.dgv1.Rows[i].FindControl("lbok")).Enabled = true;
                    index = (this.gvPayment.PageSize) * (this.gvPayment.PageIndex) + i;
                    dt.Rows[index]["chkapp"] = "False";


                }

            }

            Session["tblpayment"] = dt;
        }


    }
}