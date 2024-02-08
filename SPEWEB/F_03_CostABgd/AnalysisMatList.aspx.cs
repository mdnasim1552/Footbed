using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Web.Security;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using SPEENTITY;

namespace SPEWEB.F_03_CostABgd
{
    public partial class AnalysisMatList : System.Web.UI.Page
    {
        ProcessAccess proData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Concluded Analysis Material List";

                CommonButton();
                this.GetMatGrpList();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Attributes.Add("target", "Blank");
        }

        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;

        }

        protected void GetMatGrpList()
        {

            string coderange = "04%";
            string comcod = GetCompCode();
            DataSet ds1 = proData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");

            DataView dv = new DataView(ds1.Tables[1]);
            dv.Sort = "sircode1 ASC";

            this.ddlSubGroup.DataTextField = "sirdesc";
            this.ddlSubGroup.DataValueField = "sircode1";
            this.ddlSubGroup.DataSource = dv.ToTable();
            this.ddlSubGroup.DataBind();

        }

        //protected void ddlMatGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string matHead = "04%";
        //    DataTable dt1 = (DataTable)Session["tblmatGrp"];
        //    DataView dv = dt1.DefaultView;
        //    //dv.RowFilter = "sircode1 like '" + matHead + "'";
        //    this.ddlSubGroup.DataTextField = "sirdesc";
        //    this.ddlSubGroup.DataValueField = "sircode1";
        //    this.ddlSubGroup.DataSource = dv.ToTable();
        //    this.ddlSubGroup.DataBind();
        //}

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetAnalysisList()
        {
            string comcod = this.GetCompCode();
            string matGroup = this.ddlSubGroup.SelectedValue == "040000000000" ? "%" : (this.ddlSubGroup.SelectedValue.ToString().Substring(0, 7) + "%");
            DataSet ds2 = proData.GetTransInfo(comcod, "SP_ENTRY_BATCH_BUDGET_03", "GET_ANALYSIS_LIST", matGroup, "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            ViewState["tblAnlysis"] = ds2.Tables[0];
            this.Data_Bind();
        }


        private void Data_Bind()
        {
            DataTable dt2 = (DataTable)ViewState["tblAnlysis"];

            this.gvAnlMatList.DataSource = dt2;
            this.gvAnlMatList.DataBind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.GetAnalysisList();
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        //protected void lnkAnalysis_Click(object sender, EventArgs e)
        //{

        //}

        //protected void gvAnlMatList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    DataTable dt3 = (DataTable)ViewState["tblAnlysis"];

        //}
    }
}