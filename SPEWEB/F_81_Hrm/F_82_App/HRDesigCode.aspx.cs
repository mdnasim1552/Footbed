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
    public partial class HRDesigCode : System.Web.UI.Page

    {
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
            ((Label)this.Master.FindControl("lblTitle")).Text = "Designation Code Book";
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

                string comcod = this.GetCompCode();
                DataSet dsone = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "OACCOUNTHRDESCODE", "", "", "", "", "", "", "", "", "");
                this.ddlOthersBook.DataTextField = "hrgdesc";
                this.ddlOthersBook.DataValueField = "hrgcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + " Already Added');", true);
                
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

            string comcod = this.GetCompCode();
            int rowindex = (this.grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;
            string code = ((DataTable)Session["storedata"]).Rows[rowindex]["droll"].ToString();
            DropDownList ddl3 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlRoll");
            //ViewState["gindex"] = e.NewEditIndex;        
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "DEGROLL", "", "", "", "", "", "", "", "", "");
            ddl3.DataTextField = "hrgdesc";
            ddl3.DataValueField = "hrgcod";
            ddl3.DataSource = ds1;
            ddl3.DataBind();
            ddl3.SelectedValue = code;

        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            if (gcode2.Length != 5)
                return;
            string Descb = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDescb")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string srtdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvsrtdesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();
            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            string Roll = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlRoll")).Text.Trim();

            bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "INSERTUPHRINF", tgcod,
                           gdesc, Gtype, "0", "", "0", Descb, Roll, srtdesc, "", "", "", "", "", "");

            if (result == true)
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Updated');", true);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Failed');", true);

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
                this.grvacc.PageSize = Convert.ToInt16(this.ddlPageNo.SelectedValue);
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

                Session["Report1"] = grvacc;
                if (tbl1.Rows.Count > 0)
                {
                    ((HyperLink)this.grvacc.HeaderRow.FindControl("hlbtnexportexcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + ex.Message.ToString() + "');", true);
            }

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
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
                string comcod = this.GetCompCode();
                this.lnkok.Text = "New";

                this.ddlOthersBook.Visible = false;
                this.ddlOthersBookSegment.Visible = false;
                this.lbalterofddl.Visible = true;
                this.lbalterofddlSegment.Visible = true;
                //this.ibtnSrch.Visible = true;
                //this.PanelSearch.Visible = true;
                this.lbalterofddl.Text = this.ddlOthersBook.SelectedItem.ToString().Trim();
                this.lbalterofddlSegment.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                this.ShowInformation();
            }

            else
            {

                this.lnkok.Text = "Ok";

                this.lbalterofddl.Visible = false;
                this.lbalterofddlSegment.Visible = false;
                this.ddlOthersBook.Visible = true;
                this.ddlOthersBookSegment.Visible = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();

            }


        }

        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()) == "0300000" ? "03%" : this.ddlOthersBook.SelectedValue.ToString().Substring(0, 4) + "%";
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            string txtSearchItem = "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "HRDESIGDETAIL", tempddl1, tempddl2, txtSearchItem, "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }
        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }

        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
    }
}