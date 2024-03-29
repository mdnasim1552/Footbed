﻿using System;
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

namespace SPEWEB.F_31_Mis
{
    public partial class MisFinNoteCodeBook : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.ShowLetterInfo();

            }

            this.lmsg.Text = "";
            //

        }



        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }






        protected void grvacc_DataBind()
        {

            DataTable tbl1 = (DataTable)Session["storedata"];
            this.gvnoteinfo.DataSource = tbl1;
            this.gvnoteinfo.DataBind();
        }






        private void ShowLetterInfo()
        {

            string comcod = this.GetComeCode();
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "FINNOTEINFO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvnoteinfo.DataSource = null;
                this.gvnoteinfo.DataBind();
                return;

            }

            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();


        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }
        protected void gvnoteinfo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvnoteinfo.EditIndex = -1;
            this.grvacc_DataBind();
        }
        protected void gvnoteinfo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvnoteinfo.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
        }

        protected void gvnoteinfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            try
            {
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //if (!Convert.ToBoolean(dr1[0]["entry"]))
                //{
                //    this.lmsg.Text = "You have no permission";
                //    return;
                //}

                string comcod = this.GetComeCode();
                string letcod1 = ((Label)gvnoteinfo.Rows[e.RowIndex].FindControl("lbgvcode")).Text.Trim().Replace("-", "");
                string letcode3 = ((TextBox)gvnoteinfo.Rows[e.RowIndex].FindControl("txtgvcode3")).Text.Trim().Replace("-", "");
                string Desc = ((TextBox)gvnoteinfo.Rows[e.RowIndex].FindControl("txtgvCodeDesc")).Text.Trim();
                string Code = letcod1 + letcode3;
                if (Code.Length != 7)
                {
                    this.lmsg.Text = "Code Length Must Be 7 Digit";
                    return;
                }
                bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "INORUPFNOTEINF", Code, Desc, "", "", "", "", "", "", "", "", "", "", "", "", "");
                this.gvnoteinfo.EditIndex = -1;

                if (result)
                {

                    this.lmsg.Text = "Update Successfully";
                    this.ShowLetterInfo();
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


        protected void gvnoteinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvnoteinfo.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();

        }

    }

}