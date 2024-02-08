using SPELIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_11_RawInv
{
    public partial class MiniStockInput : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess _processAccess = new ProcessAccess();
        Common _common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //=======================
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                {
                    Response.Redirect("~/AcceessError.aspx");
                }

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //=======================

                string type = Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "Entry") ? "Standard Setup - Materials" : "";

                this.CommonButton();
                this.GetAllStores();
                this.GetCodeBookList();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnUpdate_Click);
        }

        private void CommonButton()
        {
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text= "Calculation";

        }

        private void GetAllStores()
        {   
            string comcod = _common.GetCompCode();
            string filter = "%%";
            DataSet ds = _processAccess.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETEXPRJNAME", filter, "", "", "", "", "", "", "", "");
            this.ddlStore.DataTextField = "actdesc";
            this.ddlStore.DataValueField = "actcode";
            this.ddlStore.DataSource = ds.Tables[0];
            this.ddlStore.DataBind();
        }

        protected void GetCodeBookList()
        {
            try
            {
                string Querytype = this.Request.QueryString["Type"];
                string coderange = "04%";

                string comcod = _common.GetCompCode();
                DataSet dsone = _processAccess.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                Session["tblmatsubhead"] = dsone.Tables[1];
                this.ddlCodeBook.DataTextField = "sircode";
                this.ddlCodeBook.DataValueField = "sircode1";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
                this.ddlCodeBook_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
            }
        }

        protected void ddlCodeBook_SelectedIndexChanged(object sender, EventArgs e)
        {

            string mathead = this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            DataTable dt = (DataTable)Session["tblmatsubhead"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode1 like '" + mathead + "'";
            this.ddlGroup.DataTextField = "sircode";
            this.ddlGroup.DataValueField = "sircode1";
            this.ddlGroup.DataSource = dv.ToTable();
            this.ddlGroup.DataBind();
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "Entry":
                    stkLvlMultiview.ActiveViewIndex = 0;
                    this.Get_Mat_Stock();
                    Populate_GridView_MatStock();
                    break;
            }
        }

        private void Get_Mat_Stock()
        {
            string comcod = _common.GetCompCode();
            string store = ddlStore.SelectedValue;
            string sircode = (this.ddlGroup.SelectedValue.ToString().Substring(4, 8) == "00000000") ? this.ddlCodeBook.SelectedValue.ToString().Substring(0, 2) + "%" : (this.ddlGroup.SelectedValue.ToString()).Substring(0, 7) + "%";

            string codebook = ddlCodeBook.SelectedValue;
            string group = ddlGroup.SelectedValue;

            DataSet ds = _processAccess.GetTransInfo(comcod, "SP_INV_STDANA", "GATEMATSTOCK", store, sircode, "", "", "", "");

            if (ds == null || ds.Tables[0].Rows.Count == 0) return;

            ViewState["tblMatStock"] = ds.Tables[0];
        }

        private void Populate_GridView_MatStock()
        {
            DataTable dt = (DataTable) ViewState["tblMatStock"];

            if (dt == null) return;

            gvMiniStock.DataSource = dt;
            gvMiniStock.DataBind();


            //Prepare Excel
            dt.Columns.RemoveAt(1);
            dt.Columns.RemoveAt(1);
            dt.Columns.RemoveAt(1);

            GridView gvExcel = new GridView();
            gvExcel.DataSource = dt;
            gvExcel.DataBind();
            Session["Report1"] = gvExcel;
            ((HyperLink)this.gvMiniStock.HeaderRow.FindControl("lnkExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }


        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            string comcod = _common.GetCompCode();
            string store = this.ddlStore.SelectedValue.ToString();


            for (int i = 0; i < this.gvMiniStock.Rows.Count; i++)
            {
                string rsircode = ((Label)this.gvMiniStock.Rows[i].FindControl("lblgvitemcode")).Text;
                string spcfcod = ((Label)this.gvMiniStock.Rows[i].FindControl("lblgvSpcfCod")).Text;

                bool chkmr = ((CheckBox)this.gvMiniStock.Rows[i].FindControl("chkvmrno")).Checked;
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvMiniStock.Rows[i].FindControl("txtgvqty")).Text.Replace(",", "").Trim());
                double restkQty = Convert.ToDouble("0" + ((TextBox)this.gvMiniStock.Rows[i].FindControl("txtgvrestkqty")).Text.Replace(",", "").Trim());
                double maxQty = Convert.ToDouble("0" + ((TextBox)this.gvMiniStock.Rows[i].FindControl("txtgvmaxqty")).Text.Replace(",", "").Trim());
                double eoq = Convert.ToDouble("0" + ((TextBox)this.gvMiniStock.Rows[i].FindControl("txtgveoq")).Text.Replace(",", "").Trim());
                double delper = Convert.ToDouble("0" + ((TextBox)this.gvMiniStock.Rows[i].FindControl("txtgvdelper")).Text.Replace(",", "").Trim());

                if (qty > 0 || chkmr == true)
                {
                    bool result = _processAccess.UpdateTransInfo(comcod, "SP_INV_STDANA", "INSERTMSTKINF", store, rsircode, qty.ToString(), restkQty.ToString(), maxQty.ToString(),
                        eoq.ToString(), delper.ToString(), spcfcod, "", "", "", "", "", "", "");
                    
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + _processAccess.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Record Updated Successfully');", true);
                    }
                }
            }
        }

        protected void gvMiniStock_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = _common.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblMatStock"];
            string store = this.ddlStore.SelectedValue.ToString();
            string rescod = ((Label)this.gvMiniStock.Rows[e.RowIndex].FindControl("lblgvitemcode")).Text.Trim();
            string spcfcod = ((Label)this.gvMiniStock.Rows[e.RowIndex].FindControl("lblgvSpcfCod")).Text;
            
            double qty = 0;
            double restkQty = 0;
            double maxQty = 0;
            double eoq = 0;
            double delper = 0;

            //string proscode = "000000000000";
            bool result = _processAccess.UpdateTransInfo(comcod, "SP_INV_STDANA", "INSERTMSTKINF", store, rescod, qty.ToString(), restkQty.ToString(), maxQty.ToString(),
                        eoq.ToString(), delper.ToString(), spcfcod, "", "", "", "", "", "", "");

            //bool result = _processAccess.UpdateTransInfo(comcod, "SP_INV_STDANA", "DELETMINSTO", store, rescod, spcfcod, "", "", "", "", "", "", "", "", "", "", "", "");
            
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + _processAccess.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            else
            {
                ((TextBox)this.gvMiniStock.Rows[e.RowIndex].FindControl("txtgvqty")).Text = "";
                ((TextBox)this.gvMiniStock.Rows[e.RowIndex].FindControl("txtgvrestkqty")).Text = "";
                ((TextBox)this.gvMiniStock.Rows[e.RowIndex].FindControl("txtgvmaxqty")).Text = "";
                ((TextBox)this.gvMiniStock.Rows[e.RowIndex].FindControl("txtgveoq")).Text = "";
                ((TextBox)this.gvMiniStock.Rows[e.RowIndex].FindControl("txtgvdelper")).Text = "";

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Record Updated Successfully');", true);
            }

            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "rescode not in('" + rescod + "')";
            //ViewState.Remove("tblMatStock");
            //ViewState["tblMatStock"] = dv.ToTable();
            //this.Populate_GridView_MatStock();
        }





    }
}