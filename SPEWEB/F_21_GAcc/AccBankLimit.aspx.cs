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
    public partial class AccBankLimit : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess _process = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblTitle")).Text = "Bank Limit Information";


                this.ShowData();
            }
        }


        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void ShowData()
        {
            
            string comcod = this.GetCompCode();
            string banksearch = "%" + this.txtSrchBankName.Text + "%";
            DataSet ds1 = _process.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBANKLOANLIMIT", banksearch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBankLimit.DataSource = null;
                this.gvBankLimit.DataBind();
                return;
            }
            ViewState["tblbank"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblbank"];
            this.gvBankLimit.DataSource = tbl1;
            this.gvBankLimit.DataBind();
            this.FooterCalculation(tbl1);
        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvBankLimit.FooterRow.FindControl("lgvFamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bankamt)", "")) ? 0.00
            : dt.Compute("Sum(bankamt)", ""))).ToString("#,##0;(#,##0);  ");
        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblbank"];
            for (int i = 0; i < this.gvBankLimit.Rows.Count; i++)
            {
                tbl1.Rows[i]["bankamt"] = Convert.ToDouble("0" + ((TextBox)this.gvBankLimit.Rows[i].FindControl("txtgvbankamt")).Text.Trim()).ToString();

            }

            ViewState["tblbank"] = tbl1;

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lblmsg.Text = "You have no permission";
            //    return;
            //}
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblbank"];

                bool result = true;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {


                    string bankcode = dt1.Rows[i]["bankcode"].ToString();
                    double bankam = Convert.ToDouble(dt1.Rows[i]["bankamt"].ToString());
                    if (bankam > 0)

                        result = _process.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INORUPBANKLIMIT", bankcode, bankam.ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "");


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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ex.Message + "');", true);
                return;
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        protected void gvBankLimit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string bankcode = ((Label)this.gvBankLimit.Rows[e.RowIndex].FindControl("lblgvBankCod")).Text.Trim();

            bool result = _process.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELETEBANKLIMIT", bankcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            this.ShowData();
        }
    }
}