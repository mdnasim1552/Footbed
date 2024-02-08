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

using System.IO;

namespace SPEWEB.F_21_GAcc
{
    public partial class AccSubCodeBook2 : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        CommonHelperClass helpercl = new CommonHelperClass();
        static string[] CarArray = new string[3] { "Sub Code-1", "Sub Code-2", "Details Code" };
        static string tempddl1 = "", tempddl2 = "";
        Common Common = new Common();
        GridView obj = new GridView();



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["InputType"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "Dept") ? "Department Code" : (type == "Appointment") ? "Employees Code" : "";

                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "Dept") ? "Department Code" : "Employees Code";

                //((Label)this.Master.FindControl("lblTitle")).Text = "Department Code";

                if (this.ddlOthersBook.Items.Count == 0)
                    this.Load_CodeBooList();
                this.ConfirmMessage.Text = "";

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
                ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
                this.helpercl.GetSisterConcernInf();

                if (this.Request.QueryString["InputType"] == "Dept")
                {
                    this.grvacc.Columns[14].Visible = true;
                }

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);


        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
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
        protected void Load_CodeBooList()
        {
            obj = this.grvacc;
            try
            {
                string Querytype = this.Request.QueryString["InputType"];
                string coderange = (Querytype == "ResourceCode") ? "sircode like '0[1-9]%'   or  sircode like '1[0-7]%' " : (Querytype == "Overhead") ? "sircode like '0[89]%'  or  sircode like '1[0-9]%' or sircode like '20%'"
                    : (Querytype == "LC") ? "sircode like '2[5-6]%'" : (Querytype == "BiPro") ? "sircode like '42%'"
                    : (Querytype == "FAssets") ? "sircode like '31%'" : (Querytype == "MProduct") ? "sircode like '4[1-2]%'" : (Querytype == "Process") ? "sircode like '49%'"
                    : (Querytype == "ResCodePrint") ? "sircode like '%'" : (Querytype == "SupplierCode") ? " sircode like '99%'" : (Querytype == "Appointment") ? " sircode like '93%'" : (Querytype == "Dept") ? " sircode like '94%'" : (Querytype == "BBLCCode") ? "sircode like '05%'" : "";  // 99 supplaier

                // Res,LC,FAssets,MPpoduct,Process,Supplier
                string AllRes = (Querytype == "ResCodePrint") ? "ALL" : "";
                string comcod = this.GetComeCode();
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTCODE", coderange, AllRes, "", "", "", "", "", "", "");
                Session["LoadDataForDropDownList"] = dsone.Tables[0];
                DataTable dt1 = (DataTable)Session["firsttable"];
                this.ddlOthersBook.DataSource = dt1;

                this.ddlOthersBook.DataTextField = "sircode";
                this.ddlOthersBook.DataValueField = "sircode1";
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
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
            string comcod = this.GetComCode();

            string rescode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim().Substring(0, 2);
            string rescode2 = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;
            string actcode = ((DataTable)ViewState["storedata"]).Rows[rowindex]["actcode"].ToString();

            if (this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) == "05")
            {

                DataSet dss = Rprss.UpdateCode("SP_ENTRY_CODEBOOK", comcod, "GETACTCODE", "", "", "", "");
                DropDownList ddl1 = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlAccCode");
                ddl1.DataTextField = "actdesc";
                ddl1.DataValueField = "actcode";
                ddl1.DataSource = dss.Tables[0];
                ddl1.DataBind();


                string rescode = rescode1 + rescode2;

                ddl1.Visible = (rescode.Trim().Substring(9) == "000") ? false : true;
                if (rescode.Trim().Substring(9) == "000")
                {
                    ddl1.Items.Clear();


                }
                else
                {

                    ddl1.SelectedValue = actcode;

                }

            }
            if (this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) == "04")
            {
                this.grvacc.Columns[7].Visible = true;
                this.grvacc.Columns[6].Visible = false;
                DataTable unitble = (DataTable)ViewState["units"];
                DropDownList ddlgval = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlunit");
                ddlgval.DataTextField = "gdesc";
                ddlgval.DataValueField = "gcod";
                ddlgval.DataSource = unitble;
                ddlgval.DataBind();
            }

        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.ConfirmMessage.Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetComeCode();
                string sircode1 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
                string sircode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();
                string sircode = "";
                bool updateallow = true;//01-001-01-001

                if (sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    sircode = sircode2.Substring(0, 2) + sircode1.Substring(0, 2) + sircode1.Substring(3, 3) + sircode1.Substring(7, 2) + sircode1.Substring(10, 3);
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid code!";
                    updateallow = false;
                }







                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string Sirdescb = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirdescb")).Text.Trim();
                string txtsirtype = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridsirtype")).Text.Trim();
                string txtsirtdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirtdesc")).Text.Trim();
                string txtsirunit = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirunit")).Text.Trim();
                string txtsirval = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirval")).Text.Trim()).ToString();
                string psircode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();
                string actcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlAccCode")).Text.Trim();
                string unitcode = "";
                if (sircode2.Substring(0, 2) == "04")
                {
                    txtsirunit = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlunit")).SelectedItem.ToString();
                    unitcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlunit")).Text.Trim();
                }

                DataTable tbl1 = (DataTable)ViewState["storedata"];//check whether it is needed or not

                string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

                if (tempddl2 == "4" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) != psircode1.Substring(2, 3))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }
                else if (tempddl2 == "7" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }
                else if (tempddl2 == "9" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {

                    if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3) || sircode1.Substring(3, 3) == "000")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }

                }
                else if (tempddl2 == "12" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) == "000" && sircode1.Substring(7, 2) != "00" || sircode1.Substring(7, 2) == "00" && sircode1.Substring(10, 3) != "000")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Not Allowed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        updateallow = false;
                    }
                }


                if (updateallow)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string userid = hst["usrid"].ToString();

                    int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;


                    //txtsirval = "0" + txtsirval;
                    //tbl1.Rows[Index]["SIRCODE"] = sircode;
                    //tbl1.Rows[Index]["SIRDESC"] = Desc;
                    //tbl1.Rows[Index]["SIRTYPE"] = txtsirtype;
                    //tbl1.Rows[Index]["SIRTDES"] = txtsirtdesc;
                    //tbl1.Rows[Index]["SIRUNIT"] = txtsirunit;
                    //tbl1.Rows[Index]["SIRVAL"] = Convert.ToDecimal(txtsirval);
                    //tbl1.Rows[Index]["actcode"] = actcode;
                    //tbl1.Rows[Index]["actdesc"] = actdesc;


                    bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", sircode2.Substring(0, 2), sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, actcode, unitcode,
                        Sirdescb, "", "", "", "");
                    this.ShowInformation();
                    if (result)
                    {
                        this.ConfirmMessage.Text = "Update Successfully";
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }
                    else
                    {
                        this.ConfirmMessage.Text = "Parent Code Not Found!!!";
                        //((Label)this.Master.FindControl("lblmsg")).Text = "Parent Code Not Found!!!";
                    }
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();

                }
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Account Sub-CodeBook";
                    string eventdesc = "Update Sub-CodeBook";
                    string eventdesc2 = sircode;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }

        protected void grvacc_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)ViewState["storedata"];
                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
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
                this.grvacc.PageIndex = ((DropDownList)this.grvacc.HeaderRow.FindControl("ddlPageNo")).SelectedIndex;
                this.grvacc.EditIndex = -1;
                this.grvacc_DataBind();
            }
            catch
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

            DataTable Dtable = (DataTable)ViewState["storedata"];
            //ReportDocument rptAccCode = new RMGiRPT.R_21_GAcc.rptOthersAccCode();
            //TextObject txtCompany = rptAccCode.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rpttxtNameR = rptAccCode.ReportDefinition.ReportObjects["txtNameRpt"] as TextObject;
            //rpttxtNameR.Text = CodeDesc;
            //TextObject txtuserinfo = rptAccCode.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Account Sub-CodeBook";
            //    string eventdesc = "Print Sub-CodeBook";
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptAccCode.SetDataSource(Dtable);
            //Session["Report1"] = rptAccCode;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";
                    this.LblBookName1.Text = "Code Book:";
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ddlOthersBook.Visible = false;
                    this.ddlOthersBookSegment.Visible = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.ibtnSrch.Visible = true;
                    this.lbalterofddl.Text = this.ddlOthersBook.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    //tempddl1 = ASTUtility.Left(this.ddlOthersBook.SelectedItem.ToString(), 2);

                    if (ASTUtility.Left(ddlOthersBook.SelectedValue.ToString(), 2) == "05")
                    {
                        //grvacc.Columns[3].HeaderText = "Code";
                        //grvacc.Columns[4].HeaderText = "Description of Code";
                        grvacc.Columns[9].Visible = true;
                        //grvacc.Columns[6].Visible = true;
                        //grvacc.Columns[5].HeaderText = "Unit";
                        //grvacc.Columns[6].HeaderText = "Std.Rate";
                        //grvacc.Columns[7].HeaderText = "Type";
                        //grvacc.Columns[8].HeaderText = "Type Desc";
                    }
                    //else if (tempddl1 == "02")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Code";
                    //    grvacc.Columns[4].HeaderText = "Description of Code";
                    //    grvacc.Columns[5].Visible = true;
                    //    grvacc.Columns[6].Visible = true;
                    //    grvacc.Columns[5].HeaderText = "Unit";
                    //    grvacc.Columns[6].HeaderText = "Std.Rate";
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[8].HeaderText = "Type Desc";
                    //}
                    //else if (tempddl1 == "03")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Code";
                    //    grvacc.Columns[4].HeaderText = "Description of Code";
                    //    grvacc.Columns[5].Visible = true;
                    //    grvacc.Columns[6].Visible = true;
                    //    grvacc.Columns[5].HeaderText = "Unit";
                    //    grvacc.Columns[6].HeaderText = "Std.Rate";
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[8].HeaderText = "Type Desc";
                    //}
                    else if (ASTUtility.Left(ddlOthersBook.SelectedValue.ToString(), 2) == "04")
                    {
                        grvacc.Columns[4].HeaderText = "Material Name";
                        grvacc.Columns[5].HeaderText = "Description";
                        //grvacc.Columns[5].Visible = true;
                        grvacc.Columns[8].Visible = false;
                        //grvacc.Columns[5].HeaderText = "Unit";
                        //grvacc.Columns[6].HeaderText = "Std.Rate";
                        //grvacc.Columns[7].HeaderText = "Type";
                        //grvacc.Columns[8].HeaderText = "Type Desc";
                        grvacc.Columns[9].Visible = true;

                    }
                    else if (ASTUtility.Left(ddlOthersBook.SelectedValue.ToString(), 2) == "94")
                    {

                        grvacc.Columns[6].HeaderText = "ID Card";


                    }

                    //else if (tempddl1 == "41")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Item Code";
                    //    grvacc.Columns[4].HeaderText = "Description of Code";
                    //    grvacc.Columns[5].Visible = true;
                    //    grvacc.Columns[6].Visible = true;
                    //    grvacc.Columns[15].Visible = true;
                    //    grvacc.Columns[16].Visible = true;
                    //    grvacc.Columns[5].HeaderText = "Unit";
                    //    grvacc.Columns[6].HeaderText = "Std.Qty";
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[16].HeaderText = "Type Desc";
                    //}
                    //else if (tempddl1 == "81")
                    //{
                    //    grvacc.Columns[3].HeaderText = "Code";
                    //    grvacc.Columns[4].HeaderText = "Contractor Name";
                    //    grvacc.Columns[5].Visible = false;
                    //    grvacc.Columns[6].Visible = false;
                    //    grvacc.Columns[7].HeaderText = "Type";
                    //    grvacc.Columns[8].HeaderText = "Type Desc";
                    //}
                    else
                    {
                        grvacc.Columns[4].HeaderText = "Item Code";
                        grvacc.Columns[5].HeaderText = "Description of Code";
                    }
                    this.ShowInformation();
                }
                else
                {

                    this.lnkok.Text = "Ok";
                    this.txtsrch.Text = "";
                    this.ConfirmMessage.Visible = false;
                    this.LblBookName1.Text = "Select Code Book:";
                    this.ibtnSrch.Visible = false;
                    this.lbalterofddl.Visible = false;
                    this.lbalterofddl0.Visible = false;
                    this.ddlOthersBook.Visible = true;
                    this.ddlOthersBookSegment.Visible = true;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();

                }

            }
            catch (Exception ex)
            {
                this.ConfirmMessage.Text = "Information not found!!!!";
            }
        }

        private void ShowInformation()
        {
            string comcod = this.GetComeCode();
            ViewState.Remove("DetailsCode");
            string srchoptionmain = this.txtsrch.Text.Trim();
            string srchoption1 = "";
            string srchoption = (srchoptionmain.Contains("-")) ? srchoptionmain : "%" + srchoptionmain + "%";
            if (srchoption.Contains("-"))
            {
                int index = srchoption.IndexOf("-");
                srchoption = srchoptionmain.Substring(0, index);
                srchoption1 = srchoptionmain.Substring(index + 1).Replace("%", "");
            }

            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

            tempddl1 = (tempddl1 == "00") ? "" : tempddl1;

            string Calltype = (srchoptionmain.Contains("-")) ? "OACCOUNTBTWNCINFO" : "OACCOUNTINFO";

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", Calltype, tempddl1, tempddl2, srchoption, srchoption1, "", "", "", "", "");
            //---------------
            //string comcod = this.GetComeCode();
            //Session.Remove("DetailsCode");
            //tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            //tempddl1 = (tempddl1 == "00" ? "" : tempddl1);
            //tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            //string srchoption = this.txtsrch.Text.Trim() + "%";
            //DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTINFO", tempddl1, tempddl2, srchoption, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }
            DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_NAME", "", "", "", "", "", "", "");
            ViewState["units"] = dsone.Tables[0];
            ViewState["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();


                if (Code == "")
                    return;

                if (ASTUtility.Right(Code, 3) == "000" && ASTUtility.Right(Code, 5) != "00000")
                {
                    e.Row.Attributes["style"] = "background:#2EFEC8;";
                }
                else if (ASTUtility.Right(Code, 10) == "0000000000")
                {
                    e.Row.Attributes["style"] = "background:#14BCF5;";
                }

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("lbgrcode");
                //HyperLink hlnkDetails = (HyperLink)e.Row.FindControl("lnkDetails");
                HyperLink hlnkSpcf = (HyperLink)e.Row.FindControl("lnkSpcf");
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                string sirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sirdesc")).ToString();



                if (ASTUtility.Left(sircode, 2) == "41" && ASTUtility.Right(sircode, 3) != "000" || ASTUtility.Left(sircode, 7) == "0101001" && ASTUtility.Right(sircode, 3) != "000")
                {
                    //hlink1.Font.Bold = true;
                    //hlink1.Style.Add("color", "blue");

                    //hlink1.NavigateUrl = "LinkProductinfo.aspx?Type=ProdInfo&comcod=" + comcod + "&sircode=" + sircode;
                }
                if (ASTUtility.Left(sircode, 2) == "04" || ASTUtility.Left(sircode, 2) == "21" || ASTUtility.Left(sircode, 2) == "22")
                {

                    hlnkSpcf.Style.Add("color", "red");
                    hlnkSpcf.NavigateUrl = "LinkSpecificCodeBook.aspx?sircode=" + sircode + "&sirdesc=" + sirdesc;
                }
                else
                {
                    hlnkSpcf.Text = "";
                    hlnkSpcf.Style.Add("color", "black");
                }



                if ((ASTUtility.Left(sircode, 2) == "41" || ASTUtility.Left(sircode, 2) == "21" || ASTUtility.Left(sircode, 2) == "22" || ASTUtility.Left(sircode, 2) == "01") && ASTUtility.Right(sircode, 3) != "000")
                {
                    //hlnkDetails.Font.Bold = true;
                    //hlnkDetails.Style.Add("color", "blue");
                    //hlnkDetails.NavigateUrl = "LinkMAProGeninfo.aspx?Type=GenInfo&sircode=" + sircode;

                    // hlnkSpcf.Style.Add("color", "red");
                    //  hlnkSpcf.NavigateUrl = "LinkSpecificCodeBook.aspx?sircode=" + sircode + "&sirdesc=" + sirdesc;

                }
                else
                {
                    //hlnkDetails.Font.Bold = true;
                    //hlnkDetails.Text = "";
                    //hlnkDetails.Style.Add("color", "black");

                    //hlnkSpcf.Text = "";
                    //hlnkSpcf.Style.Add("color", "black");

                }


            }

        }
    }
}