using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;


namespace SPEWEB.F_34_Mgt
{
    public partial class ShareEquity : System.Web.UI.Page
    {


        ProcessAccess da = new ProcessAccess();
        GridView obj = new GridView();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.ShowInformation();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Share Equity";
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void grvacc_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvDeChg.DataSource = tbl1;
                this.grvDeChg.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        private void ShowInformation()
        {
            Session.Remove("storedata");
            string comcod = this.GetCompCode();
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "DEPCHARGE02", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvDeChg.DataSource = null;
                this.grvDeChg.DataBind();
                return;
            }
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["storedata"];
            int rowindex;
            for (int i = 0; i < this.grvDeChg.Rows.Count; i++)
            {
                string actcode = ((Label)this.grvDeChg.Rows[i].FindControl("lgCode")).Text.Trim();
                string equity = Convert.ToDouble('0' + ((TextBox)this.grvDeChg.Rows[i].FindControl("txtgvchg")).Text.Trim()).ToString();
                rowindex = (this.grvDeChg.PageSize) * (this.grvDeChg.PageIndex) + i;
                dt.Rows[rowindex]["actcode"] = actcode;
                dt.Rows[rowindex]["charge"] = equity;
            }
            Session["storedata"] = dt;
        }


        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lmsg.Text = "You have no permission";
            //    return;
            //}

            this.SaveValue();
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["storedata"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string actcode = dt.Rows[i]["actcode"].ToString();
                double equity = Convert.ToDouble(dt.Rows[i]["charge"].ToString());
                bool resultb = da.UpdateTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "INSERTDCHARGE02", actcode, equity.ToString(), "", "", "",
                    "", "", "", "", "", "", "", "", "", "");

                if (!resultb)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:');", true);

                    return;
                }


            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "" ;
                string eventdesc = "Update Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void grvDeChg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvDeChg.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
    }

}