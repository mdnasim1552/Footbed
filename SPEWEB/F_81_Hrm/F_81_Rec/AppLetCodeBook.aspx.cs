using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WinForms;
using System.Drawing;
using SPELIB;
using SPERDLC;

namespace SPEWEB.F_81_Hrm.F_81_Rec
{
    public partial class AppLetCodeBook : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Appiontment Letter Code";

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //this.lblTitle.Text = (this.Request.QueryString["Type"] == "MktTeam" ? "MARKETING TEAM CODE BOOK INFORMATION" : "LETTER CREATION INFORMATION");
                this.ViewSection();

            }

            this.lmsg.Text = "";

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void ViewSection()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "AppLetter":
                    this.ShowSalLetterInfo();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
            }


        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable tbl1 = (DataTable)Session["LetterInfo"];

            switch (Type)
            {

                case "AppLetter":
                    this.grvAppLetterinfo.DataSource = tbl1;
                    this.grvAppLetterinfo.DataBind();
                    break;

            }

        }
        private void ShowSalLetterInfo()
        {

            string comcod = this.GetComeCode();
            DataSet ds1 = this.accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "APPLETTERINFO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvAppLetterinfo.DataSource = null;
                this.grvAppLetterinfo.DataBind();
                return;

            }

            Session["LetterInfo"] = ds1.Tables[0];
            this.Data_Bind();


        }


        protected void grvAppLetterinfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {


                string comcod = this.GetComeCode();
                string letcod1 = ((Label)this.grvAppLetterinfo.Rows[e.RowIndex].FindControl("lbgrletcodesal")).Text.Trim().Replace("-", "");
                string letcode3 = ((TextBox)this.grvAppLetterinfo.Rows[e.RowIndex].FindControl("txtgrletcodesal")).Text.Trim().Replace("-", "");
                string Desc = ((TextBox)this.grvAppLetterinfo.Rows[e.RowIndex].FindControl("txtgvDesclettersal")).Text.Trim();
                string Code = letcod1 + letcode3;
                if (Code.Length != 7)
                {
                    this.lmsg.Text = "Code Length Must Be 7 Digit";
                    return;
                }

                bool result = this.accData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_APPOINTMENT_LETTER", "INORUPAPPLETTER", Code, Desc, "", "", "", "", "", "", "", "", "", "", "", "", "");
                this.grvAppLetterinfo.EditIndex = -1;

                if (result)
                {

                    this.lmsg.Text = "Update Successfully";
                    this.ShowSalLetterInfo();
                }
                else
                {
                    this.lmsg.Text = "Update Failed";
                }

            }
            catch (Exception ex)
            {
                this.lmsg.Text = "Error:" + ex.Message;
            }
        }
        protected void grvAppLetterinfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvAppLetterinfo.EditIndex = -1;
            this.Data_Bind();
        }
        protected void grvAppLetterinfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvAppLetterinfo.EditIndex = e.NewEditIndex;
            this.Data_Bind();
        }
        protected void grvAppLetterinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvAppLetterinfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}