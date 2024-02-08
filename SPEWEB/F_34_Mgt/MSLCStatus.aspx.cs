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
namespace SPEWEB.F_34_Mgt
{
    public partial class MSLCStatus : System.Web.UI.Page
    {
        ProcessAccess MgtData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");


            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblmsg.Text = "";
            ViewState.Remove("tblmlc");
            string comcod = GetCompCode();
            string srchlc = this.txtemlcsrch.Text + "%";
            DataSet ds2 = MgtData.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "SHOWLCSTATUS", srchlc, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvLcStatus.DataSource = null;
                this.gvLcStatus.DataBind();
                return;
            }
            ViewState["tblmlc"] = ds2.Tables[0];
            this.Data_Bind();



        }


        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblmlc"];
            int index;
            for (int i = 0; i < this.gvLcStatus.Rows.Count; i++)
            {
                string chkper = (((CheckBox)gvLcStatus.Rows[i].FindControl("chklcstatus")).Checked) ? "True" : "False";
                index = (this.gvLcStatus.PageSize) * (this.gvLcStatus.PageIndex) + i;
                dt.Rows[index]["mlcstatus"] = chkper;

            }
            ViewState["tblmlc"] = dt;
        }
        protected void gvLcStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvLcStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            this.gvLcStatus.DataSource = (DataTable)ViewState["tblmlc"];
            this.gvLcStatus.DataBind();
        }

        protected void lbtnUpLCStatus_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["tblmlc"];
            foreach (DataRow dr in dt.Rows)
            {
                string mlccode = dr["mlccod"].ToString();
                string mlcstatus = dr["mlcstatus"].ToString();
                if (mlcstatus == "True")
                {

                    bool result = MgtData.UpdateTransInfo(comcod, "SP_ENTRY_MASTERLC", "INSORUPMLCSTATUS", mlccode, mlcstatus, "", "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        this.lblmsg.Text = "Updated Fail";
                        return;
                    }
                }

                this.lblmsg.Text = "Updated Successfully.";


            }



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void gvLcStatus_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblmlc"];
            string mlccod = ((Label)this.gvLcStatus.Rows[e.RowIndex].FindControl("lgvmlccod")).Text.Trim();

            bool result = MgtData.UpdateTransInfo(comcod, "SP_ENTRY_MASTERLC", "DELETEMLCSTATUS", mlccod, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvLcStatus.PageSize) * (this.gvLcStatus.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblmlc");
            ViewState["tblmlc"] = dv.ToTable();
            this.Data_Bind();
        }
    }
}