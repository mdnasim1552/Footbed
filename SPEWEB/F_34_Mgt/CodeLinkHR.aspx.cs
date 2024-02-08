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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using SPELIB;
//using RealERPRPT;
namespace RealERPWEB.F_34_Mgt
{
    public partial class CodeLinkCoReBa : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Link(Cost Resource Basis)";
                this.GetEmpType();

                //this.ShowInformation();
                this.GetACGCode();

            }
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private void GetACGCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string srchoption = "47%";
            DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETSALCODE", srchoption, "", "", "", "", "", "", "", "");
            ViewState["tblgencode"] = dsone.Tables[0];
            dsone.Dispose();
        }
        private void GetEmpType()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETEMPTYPECODE", "", "", "", "", "", "", "", "", "");


            this.ddlEmpType.DataTextField = "sirdesc";
            this.ddlEmpType.DataValueField = "sircode";
            this.ddlEmpType.DataSource= dsone.Tables[0];
            this.ddlEmpType.DataBind();

        }
        
        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();
        }

        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {


            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
            DataTable tbl1 = (DataTable)Session["storedata"];
            int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;
            string sircode = ((DataTable)Session["storedata"]).Rows[rowindex]["sircode"].ToString();
            string selected  = ((DataTable)Session["storedata"]).Rows[rowindex]["hrsection"].ToString();
            //string actcode = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lblgvactcode")).Text.Trim().Replace("-", "");
            string agccode = ((DataTable)Session["storedata"]).Rows[rowindex]["hrsection"].ToString();
            DropDownList ddlteam = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlteam");
            DataTable dt = (DataTable)ViewState["tblgencode"];
            DataView dv = dt.DefaultView;
            if (sircode.Substring(7,5) == "00000")
            {
                dv.RowFilter = "hrsection like '470%'";
            }
            else if (sircode.Substring(9, 3) == "000")
            {
                dv.RowFilter = "hrsection like '471%'";
            }
            else
            {
                dv.RowFilter = "hrsection like '472%'";
            }
            DataTable dt2 = dv.ToTable();
            ddlteam.DataTextField = "hrsectiondesc";
            ddlteam.DataValueField = "hrsection";
            ddlteam.DataSource = dt2;
            ddlteam.DataBind();
            ddlteam.SelectedValue = selected; //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();



        }

        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }



            string actcode = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgvactcode")).Text.Trim().Replace("-", "");
            string acgcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlteam")).SelectedValue.ToString();
            string acgdesc = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlteam")).SelectedItem.ToString();
            DataTable tbl1 = (DataTable)Session["storedata"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
            tbl1.Rows[Index]["hrsection"] = acgcode;
            tbl1.Rows[Index]["hrsectiondesc"] = acgdesc;
            Session["storedata"] = tbl1;
            this.grvacc.EditIndex = -1;
            bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "UPDATEHRCODE", actcode, acgcode, "", "", "", "", "", "", "", "",
                "", "", "", "", "");


            if (result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Accounts CodeBook";
                    string eventdesc = "Update CodeBook";
                    string eventdesc2 = actcode;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.grvacc_DataBind();
        }



        protected void grvacc_DataBind()
        {
            try
            {
                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvacc.DataSource = (DataTable)Session["storedata"]; 
                this.grvacc.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('"+ ex.Message +"');", true);
            }          
          

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }


        private void ShowInformation()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string empType=this.ddlEmpType.SelectedValue.ToString();
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETHRCODEINFO", empType.Substring(0, 4) + "%", "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();
        }

        protected void OkBtn_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }
        protected void ibtnSrchProject_Click(object sender, ImageClickEventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];

            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[rowindex].FindControl("ddlProCode");
            string SearchProject = "%" + ((TextBox)grvacc.Rows[rowindex].FindControl("txtSerachProject")).Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc1";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();

        }



        protected void ibtnSrchProject_Click1(object sender, EventArgs e)
        {

        }

        protected void ibtnSrchteam_Click(object sender, EventArgs e)
        {

        }

        //protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblactcode = (Label)e.Row.FindControl("lblgvactcode");

        //        HyperLink hlnkgvapmrfno = (HyperLink)e.Row.FindControl("hlnkgvapmrfno");
        //        HyperLink HypApproval = (HyperLink)e.Row.FindControl("HypApproval");


        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string comcod = hst["comcod"].ToString();


        //        string actcode = lblactcode.Text.Substring(5).ToString();
        //        string reqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
        //        string mrfno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrfno")).ToString();
        //        TableCell cell = e.Row.Cells[2];
        //        if (actcode == "0000000")
        //        {
        //            cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4BCF9E");
        //        }
        //        if (actcode == "0000000")
        //        {
        //            cell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#00CBF3");
        //        }





        //    }
            
        //}
    }
}
