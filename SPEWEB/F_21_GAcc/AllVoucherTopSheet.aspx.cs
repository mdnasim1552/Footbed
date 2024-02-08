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
using Microsoft.Reporting.WinForms;
using SPELIB;

namespace SPEWEB.F_21_GAcc
{
    public partial class AllVoucherTopSheet : System.Web.UI.Page
    {

        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Voucher 360 <sup>0";
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                // HyperLink hyp1 = (HyperLink)this.HyperLinkTriBal as HyperLink;
                //if (this.GetCompCode() == "3339")
                //{
                //    hyp1.NavigateUrl = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal3&prjcode=";
                //}
                //else
                //{
                //    hyp1.NavigateUrl = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal&prjcode=";
                //}

                this.lbtnOk_Click(null, null);

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //if (LinkButton1.Text=="Ok")
            //{
            //    LinkButton1.Text = "New";

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string voutype = this.ddlvoucher.SelectedValue.ToString();
            string refnum = "%" + this.txtrefno.Text.Trim() + "%";


            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "RPTACCOUNTTOPSHEET", frmdate, todate, voutype, refnum);
            if (ds1 == null)
            {
                this.gvAccVoucher.DataSource = null;
                this.gvAccVoucher.DataBind();
                return;
            }

            Session["tblunposted"] = ds1.Tables[0];
            this.Data_Bind();
          
           

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblunposted"];
            this.gvAccVoucher.DataSource = dt;
            this.gvAccVoucher.DataBind();
            this.FooterCalCulation();


        }

        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)Session["tblunposted"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvAccVoucher.FooterRow.FindControl("lblgvFvouamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0);");

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {



        }
        protected void gvAccVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkVoucherEdit");
                HyperLink hlnkPrintVoucher = (HyperLink)e.Row.FindControl("hlnkVoucherPrint");
                HyperLink hlnkChequePrint = (HyperLink)e.Row.FindControl("hlnkChequePrint");
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();

                string paytype = this.ChboxPayee.Checked ? "0" : "1";
                hlink1.NavigateUrl = "~/F_21_GAcc/GeneralAccounts?Mod=Management&vounum=" + vounum;

                hlnkPrintVoucher.NavigateUrl = "~/F_21_GAcc/Print?Type=accVou&vounum=" + vounum;

                hlnkChequePrint.NavigateUrl = "~/F_21_GAcc/Print?Type=Cheque&vounum=" + vounum + "&payee=" + paytype;







            }
        }

        protected void lnkbtnPrintRD_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = (DataTable)Session["tblunposted"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string vounum = dt.Rows[index]["vounum"].ToString();

        }




        protected void lbtnDeleteVoucher_Click(object sender, EventArgs e)
        {

            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataTable dt = (DataTable)Session["tblunposted"];
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int index = row.RowIndex;



            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string userid = hst["usrid"].ToString();
            //string Terminal = hst["trmid"].ToString();
            //string Sessionid = hst["session"].ToString();
            //string vounum = dt.Rows[index]["vounum"].ToString();
            //bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELETEVOUCHERUNPOSTED", vounum, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            //if (result == false)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Deleted";
            //    return;

            //}



            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //this.Data_Bind();


        }


    }
}