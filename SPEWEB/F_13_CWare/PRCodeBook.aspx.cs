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

namespace SPEWEB.F_13_CWare
{
    public partial class PRCodeBook : System.Web.UI.Page
    {

        ProcessAccess da = new ProcessAccess();
        // static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            ((Label)this.Master.FindControl("lblTitle")).Text = "Project Information Code";

            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();
            
        }

        protected void Load_CodeBooList()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTPRCODE", "",
                                "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "prgdesc";
                this.ddlOthersBook.DataValueField = "prgcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Information Code";
                string eventdesc = "Edit Code Book";
                string eventdesc2 = this.ddlOthersBook.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPPRINF", tgcod,
                           gdesc, Gtype, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

                return;
            }
            this.ShowInformation();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Information Code";
                string eventdesc = "Update Code Book";
                string eventdesc2 = this.ddlOthersBook.SelectedItem.ToString() + ", " + tgcod + "-" + gdesc;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];

                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;
                double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                for (int i = 1; i <= TotalPage; i++)
                    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                if (TotalPage > 1)
                    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = true;
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;



            }
            catch (Exception ex)
            {
            }

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                this.grvacc.PageIndex = ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
                this.grvacc.EditIndex = -1;

                this.grvacc_DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //if (this.lnkok.Visible)
            //    this.lnkok_Click(null, null);

            //string CodeDesc = this.ddlOthersBook.SelectedItem.ToString().Trim().Substring(3)
            //            + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable Dtable = (DataTable)Session["storedata"];
            //tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            //ReportDocument rptAccCode = new RptHRCodeBookInfo();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = CodeDesc;
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAccCode.SetDataSource(Dtable);
            //Session["Report1"] = rptAccCode;
            //this.lbljavascript.Text = "<script>window.open('RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "Ok")
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("storedata");
                    this.lnkok.Text = "New";

                    this.ddlOthersBook.Enabled = false;
                    this.ddlOthersBookSegment.Enabled = false;
                    //this.LblBookName1.Visible = false;
                    //this.lbalterofddl.Visible = true;
                    //this.lbalterofddl.Text = "Project Code Book: " + this.ddlOthersBook.SelectedItem.ToString().Trim()
                    //             + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    this.ShowInformation();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Information not found!!!!');", true);
                }
            }
            else
            {
                this.lnkok.Text = "Ok";

                this.ddlOthersBook.Enabled = true;
                this.ddlOthersBookSegment.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
            }
        }

        private void ShowInformation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            tempddl1 = ((tempddl1 == "00" ? "" : tempddl1));
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTPRCODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];

            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();


        }



    }
}