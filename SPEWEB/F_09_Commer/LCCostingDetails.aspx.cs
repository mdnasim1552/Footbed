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
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Xml.Linq;
using SPELIB;

namespace SPEWEB.F_09_Commer
{
    public partial class LCCostingDetails : System.Web.UI.Page
    {
        ProcessAccess proc1 = new ProcessAccess();
        static string prevPage = String.Empty;
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


                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "LC Costing";//
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
                this.imgbtnLcsearch_Click(null, null);
                ddlLcCode_SelectedIndexChanged(null, null);

                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
           
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnupload_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkTotalLcCost_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            //((CheckBox)this.Master.FindControl("chkBoxN")).CheckedChanged += new EventHandler(chkPayment_CheckedChanged);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void lnkOk_Click(object sender, EventArgs e)
        {
            this.pnlPro.Visible = true;
            this.pnlCosting.Visible = true;
            string comcod = this.ComCod();
            var lccode = ddlLcCode.SelectedValue.ToString();
            var grrno = ddlgenno.SelectedValue.ToString();
            string source = (this.RadioButtonList2.SelectedIndex == 1) ? "ACC" : "PROPOSED";
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "ORDERRECEIVE3", lccode, grrno, source, "", "", "", "", "", "");
            this.dgvReceive.DataSource = ds1.Tables[0];
            this.dgvReceive.DataBind();
            if (ds1.Tables[0].Rows.Count == 0)
                return;
            ((Label)this.dgvReceive.FooterRow.FindControl("lblgvFordqty")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(rcvqty)", "")) ?
                           0 : ds1.Tables[0].Compute("sum(rcvqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ViewState["tbllccost"] = ds1.Tables[1];
            CostData_Bind();
        }
        protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.lnkOk_Click(null,null);

        }
        private void CostData_Bind()
        {
            DataTable tbl6 = (DataTable)ViewState["tbllccost"];
            this.gvlccost.DataSource = tbl6;
            this.gvlccost.DataBind();
            this.LCFooterCal();
        }

        private void LCFooterCal()
        {
            DataTable dt = (DataTable)ViewState["tbllccost"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFtolcCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tolccost)", "")) ?
                           0 : dt.Compute("sum(tolccost)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFprelcCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(utorecamt)", "")) ?
                            0 : dt.Compute("sum(utorecamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFcurlcCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recamt)", "")) ?
                            0 : dt.Compute("sum(recamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvlccost.FooterRow.FindControl("lblgrvFlcbalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ?
                            0 : dt.Compute("sum(balamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }


        protected void imgbtnLcsearch_Click(object sender, EventArgs e)
        {
            string comcod = this.ComCod();
            string SlcNO = "%%";
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "RETRIVE_LC_VALUE", SlcNO, "14", "ACINF", "", "", "", "", "", ""); // table Desc 3
            this.ddlLcCode.DataTextField = "actdesc";
            this.ddlLcCode.DataValueField = "actcode";
            this.ddlLcCode.DataSource = ds1.Tables[0];
            this.ddlLcCode.DataBind();
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.ddlLcCode.SelectedValue = this.Request.QueryString["actcode"].ToString();
            }
        }

        protected string ComCod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        protected void ddlLcCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.ComCod();
            var lccode = ddlLcCode.SelectedValue.ToString();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_LC_INFO", "LCSTORES", lccode, "", "", "", "", "", "", "", "");
            ViewState["LCStores"] = ds1;
            this.ddlStorid.DataTextField = "storedtls";
            this.ddlStorid.DataValueField = "storid";
            this.ddlStorid.DataSource = ds1.Tables[1];
            this.ddlStorid.DataBind();
            ddlStorid_SelectedIndexChanged(null, null);
        }
        protected void ddlStorid_SelectedIndexChanged(object sender, EventArgs e)
        {
            var storeid = ddlStorid.SelectedValue.ToString();
            DataSet ds1 = (DataSet)ViewState["LCStores"];
            DataTable dtResult = new DataTable();
            if (ds1.Tables[0].Rows.Count != 0)
                dtResult = ds1.Tables[0].Select("storid LIKE '%" + storeid + "%'").CopyToDataTable();
            else
                dtResult = ds1.Tables[0];

            this.ddlgenno.DataTextField = "grrno1";
            this.ddlgenno.DataValueField = "grrno";
            this.ddlgenno.DataSource = dtResult;
            this.ddlgenno.DataBind();
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.ComCod();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            var lccode = ddlLcCode.SelectedValue.ToString();
            var grnno = ddlgenno.SelectedValue.ToString();
            var storeid = ddlStorid.SelectedValue.ToString();
            DataTable dt = ((DataTable)ViewState["tbllccost"]).Copy();
            DataSet ds1 = new DataSet("ds1");

            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl2";

            DataSet ds112 = proc1.GetTransInfoNew(comcod, "SP_ENTRY_LC_INFO", "UPDATLCCOSTING", ds1, null, null, lccode, grnno, storeid, userid, Terminal, Sessionid, Posteddat, "", "", "", "", "", "", "", "");
            if (ds112 != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }
            this.lnkOk_Click(null,null);
            // this.RateUpdate();
        }


        protected void lnkTotalLcCost_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbllccost"];
            int RowIndex = 0;

            for (int i = 0; i < this.gvlccost.Rows.Count; i++)
            {
                double tolccost = Convert.ToDouble("0" + ((Label)this.gvlccost.Rows[i].FindControl("lblgvtolcCost")).Text.Trim());
                double utorecamt = Convert.ToDouble("0" + ((Label)this.gvlccost.Rows[i].FindControl("lblgvprelcCost")).Text.Trim());
                double recamt = ASTUtility.StrPosOrNagative((((TextBox)this.gvlccost.Rows[i].FindControl("txtgvcurlcCostt")).Text.Trim()));

                // RowIndex = this.dgvOrder.PageIndex * this.dgvOrder.PageSize + i;
                dt.Rows[i]["recamt"] = recamt;
                dt.Rows[i]["balamt"] = tolccost - utorecamt - recamt;
            }

            ViewState["tbllccost"] = dt;
            this.CostData_Bind();
        }

        protected void ddlgenno_SelectedIndexChanged(object sender, EventArgs e)
        {
            lnkOk_Click(null, null);
        }
    }
}