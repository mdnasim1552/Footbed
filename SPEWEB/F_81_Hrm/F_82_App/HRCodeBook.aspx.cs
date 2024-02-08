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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class HRCodeBook : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
            ((Label)this.Master.FindControl("lblTitle")).Text = "Information Code";
            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();
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
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                string comcod = this.GetCompCode();
                DataSet dsone = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "OACCOUNTHRCODE", "", "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "hrgdesc";
                this.ddlOthersBook.DataValueField = "hrgcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                string msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }

        }
        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();

        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string comcod = this.GetCompCode();
            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();

            string gcode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lblgrcode")).Text.Trim();

            if (gcode1.Substring(0, 2) == "39")
            {
                ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).MaxLength = 6;
            }

            DropDownList grvaccdll = ((DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlLine"));
            DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPRODSETUPINFO", "", "", "", "", "", "", "", "", "");

            grvaccdll.DataTextField = "sirdesc";
            grvaccdll.DataValueField = "sircode";
            grvaccdll.DataSource = ds1.Tables[1];
            grvaccdll.DataBind();

        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();

            if (gcode1.Substring(0, 2) == "39")
            {
                if (gcode2[2] == '-')
                {
                    gcode2 = gcode2.Substring(0, 2) + gcode2.Substring(3, 3);
                }

                if (gcode2.Length != 5)
                    return;

            }
            else
            {
                if (gcode2.Length != 3)
                    return;
            }

            //if (gcode2.Length != 3)
            //    return;

            string Descb = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDescb")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            string percent = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvtxtpercnt")).Text.Trim()).ToString();
            string unit = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvUnit")).Text.Trim().ToString();
            string rate = Convert.ToDouble("0" + ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvRate")).Text.Trim()).ToString();
            string line = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("ddlLine")).SelectedValue.ToString();

            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPHRINF", tgcod,
                           gdesc, Gtype, percent, unit, rate, Descb, line, "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Updated');", true);
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
                this.grvacc.Columns[10].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "04") ? true : false;
                this.grvacc.Columns[11].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "70") ? true : false;

                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "Ok")
            {
                this.lnkok.Text = "New";
                this.ddlOthersBook.Enabled = false;
                this.ddlOthersBookSegment.Enabled = false;
                this.ShowInformation();
                this.grvacc_DataBind();
                return;
            }

            this.lnkok.Text = "Ok";
            this.ddlOthersBook.Enabled = true;
            this.ddlOthersBookSegment.Enabled = true;
            this.grvacc.DataSource = null;
            this.grvacc.DataBind();
        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            tempddl1 = ((tempddl1 == "00" ? "" : tempddl1));
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "OACCOUNTHRCODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");

            Session["storedata"] = ds1.Tables[0];
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
    }
}