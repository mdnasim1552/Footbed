﻿using System;
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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_81_Rec
{
    public partial class RecHRCodeBook : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
            ((Label)this.Master.FindControl("lblTitle")).Text = "Information Code";

            //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "SubLedger") ? "Accounts Subsidiary Ledger" : "Ledger";

            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();
            this.ConfirmMessage.Text = "";
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void Load_CodeBooList()
        {

            try
            {
                string comcod = this.GetCompCode();
                DataSet dsone = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_RECRUITMENT", "OACCOUNTHRCODE", "", "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "hrgdesc";
                this.ddlOthersBook.DataValueField = "hrgcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                this.ConfirmMessage.Text = "Error:" + ex.Message;
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
        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
            if (gcode2.Length != 3)
                return;

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            string percent = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvtxtpercnt")).Text.Trim()).ToString();
            string unit = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvUnit")).Text.Trim().ToString();
            string rate = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvRate")).Text.Trim()).ToString();
            bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_RECRUITMENT", "INSERTUPHRINF", tgcod,
                           gdesc, Gtype, percent, unit, rate, "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                this.ConfirmMessage.Text = " Successfully Updated ";
            }

            else
            {
                this.ConfirmMessage.Text = "Updated Failed";
            }
            this.grvacc.EditIndex = -1;
            this.ShowInformation();
            this.grvacc_DataBind();

        }

        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvacc.Columns[6].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "07") ? true : false;
                this.grvacc.Columns[7].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "07") ? true : false;
                this.grvacc.Columns[9].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "04") ? true : false;

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
                this.grvacc_DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Visible)
                this.lnkok_Click(null, null);

            string CodeDesc = this.ddlOthersBook.SelectedItem.ToString().Trim().Substring(3)
                        + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable Dtable = (DataTable)Session["storedata"];
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            ReportDocument rptAccCode = new RMGiRPT.R_81_Hrm.R_82_App.RptHRCodeBookInfo();
            TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            rpttxtNameR.Text = CodeDesc;
            TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAccCode.SetDataSource(Dtable);
            Session["Report1"] = rptAccCode;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                Session.Remove("storedata");
                this.lnkok.Visible = false;
                this.lnkcancel.Visible = true;
                this.ddlOthersBook.Visible = false;
                this.ddlOthersBookSegment.Enabled = false;
                this.LblBookName1.Enabled = false;
                this.lbalterofddl.Visible = true;
                this.lbalterofddl.Text = "HR Code Book: " + this.ddlOthersBook.SelectedItem.ToString().Trim()
                             + " " + "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";

                this.ShowInformation();
                this.grvacc_DataBind();

            }
            catch (Exception ex)
            {
                this.ConfirmMessage.Text = "Information not found!!!!";
            }
        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            tempddl1 = ((tempddl1 == "00" ? "" : tempddl1));
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_RECRUITMENT", "OACCOUNTHRCODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
        }
        protected void lnkcancel_Click(object sender, EventArgs e)
        {
            this.lnkok.Visible = true;
            this.lnkcancel.Visible = false;
            this.LblBookName1.Enabled = true;
            this.lbalterofddl.Visible = false;
            this.ddlOthersBook.Visible = true;
            this.ddlOthersBookSegment.Enabled = true;
            this.grvacc.DataSource = null;
            this.grvacc.DataBind();

        }






    }
}