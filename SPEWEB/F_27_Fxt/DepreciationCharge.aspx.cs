﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
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

namespace SPEWEB.F_27_Fxt
{
    public partial class DepreciationCharge : System.Web.UI.Page
    {


        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        GridView obj = new GridView();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Depreciation Schedule ";
                this.ShowInformation();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
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
                this.grvDeChg.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue.ToString());
                this.grvDeChg.DataSource = tbl1;
                this.grvDeChg.DataBind();

                if (tbl1.Rows.Count == 0)
                    return;
                Session["Report1"] = grvDeChg;
                ((HyperLink)this.grvDeChg.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception ex)
            {
            }
        }
        private void ShowInformation()
        {
            Session.Remove("storedata");
            string comcod = this.GetCompCode();
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "DEPCHARGE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvDeChg.DataSource = null;
                this.grvDeChg.DataBind();
                return;
            }
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.grvacc_DataBind();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["storedata"];
            int rowindex;
            for (int i = 0; i < this.grvDeChg.Rows.Count; i++)
            {
                string actcode = ((Label)this.grvDeChg.Rows[i].FindControl("lgCode")).Text.Trim();
                string dcharge = Convert.ToDouble('0' + ((TextBox)this.grvDeChg.Rows[i].FindControl("txtgvchg")).Text.Trim()).ToString();
                rowindex = (this.grvDeChg.PageSize) * (this.grvDeChg.PageIndex) + i;
                dt.Rows[rowindex]["actcode"] = actcode;
                dt.Rows[rowindex]["charge"] = dcharge;
            }
            Session["storedata"] = dt;
        }


        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                this.lmsg.Visible = true;
                this.lmsg.Text = "You have no permission";
                return;
            }

            this.SaveValue();
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["storedata"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string actcode = dt.Rows[i]["actcode"].ToString();
                double dcharge = Convert.ToDouble(dt.Rows[i]["charge"].ToString());
                bool resultb = da.UpdateTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO", "INSERTDCHARGE", actcode, dcharge.ToString(), "", "", "",
                    "", "", "", "", "", "", "", "", "", "");

                if (!resultb)
                {
                    this.lmsg.Visible = true;
                    this.lmsg.Text = da.ErrorObject["Msg"].ToString();
                    return;
                }


            }
            this.lmsg.Visible = true;
            this.lmsg.Text = "Updated Successfully";
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
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