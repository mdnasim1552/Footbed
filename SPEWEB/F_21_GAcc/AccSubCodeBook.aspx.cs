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
    public partial class AccSubCodeBook : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "Dept") ? "Department Code" : (type == "Appointment") ? "Employees Code" : "Details Code";

                //((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "Dept") ? "Department Code" : "Employees Code";

                //((Label)this.Master.FindControl("lblTitle")).Text = "Department Code";

                if (this.ddlOthersBook.Items.Count == 0)
                {
                    this.Load_CodeBooList();
                    this.GetResCodeleb2();
                    this.SelectResCodeLeb2();
                }
                    
               

                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
                this.helpercl.GetSisterConcernInf();

                if (this.Request.QueryString["InputType"] == "Dept")
                {
                    this.grvacc.Columns[15].Visible = true;
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
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string AllRes = (Querytype == "ResCodePrint") ? "ALL" : "";
                string comcod = this.GetComeCode();
                string userid = hst["usrid"].ToString();
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTCODE", coderange, AllRes, userid, "", "", "", "", "", "");
                Session["LoadDataForDropDownList"] = dsone.Tables[0];
                DataTable dt1 = (DataTable)Session["firsttable"];
                if (dsone.Tables[0].Rows.Count == 0)
                {
                    dsone.Tables[0].Rows.Add(comcod, "----Have No Code Permission Please Contact Sys Admin----", "XXXXXXXXXXXX");

                }
                this.ddlOthersBook.DataSource = dt1;

                this.ddlOthersBook.DataTextField = "sircode";
                this.ddlOthersBook.DataValueField = "sircode1";
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
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DataTable tblStore = ((DataTable)ViewState["storedata"]);

            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();
            DropDownList grvaccdll = ((DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlPProces"));
            
            string comcod = this.GetComCode();
           
            string rescode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim().Substring(0, 2);
            string rescode2 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;
            string actcode = ((DataTable)ViewState["storedata"]).Rows[rowindex]["actcode"].ToString();
            string sircode = ((DataTable)ViewState["storedata"]).Rows[rowindex]["sircode"].ToString();
            string sircocde = sircode.Substring(4, 8);
            if (sircode.Substring(4, 8) == "00000000" & sircode.Substring(0, 4)!="8198")
            {
                DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPROCESS", "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                DataTable dt = ds1.Tables[0].Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("procode <>'800100101099'");
                dt = dv.ToTable();

                grvaccdll.DataTextField = "prodesc";
                grvaccdll.DataValueField = "procode";
                grvaccdll.DataSource = dt;
                grvaccdll.DataBind();
                ((DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlPProces")).Visible = true;

            }
            else
            {
                ((DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlPProces")).Visible = false;
            }
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
                this.grvacc.Columns[8].Visible = true;
                this.grvacc.Columns[7].Visible = false;

                DataTable unitble = (DataTable)ViewState["units"];
                
                DropDownList ddlgval = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlunit");
                ddlgval.DataTextField = "gdesc";
                ddlgval.DataValueField = "gcod";
                ddlgval.DataSource = unitble;
                ddlgval.DataBind();

                DataTable tblmatgrp = (DataTable)ViewState["tblmatgrp"];
                DropDownList ddlMaterialGorup = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlMaterialGorup");
                ddlMaterialGorup.DataTextField = "gdesc";
                ddlMaterialGorup.DataValueField = "gcod";
                ddlMaterialGorup.DataSource = tblmatgrp;
                ddlMaterialGorup.DataBind();

                if(rescode2.Substring(0, 2)=="50" && sircocde.Substring(5, 3) != "000")
                {
                    ((DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlPProces")).Visible = true;

                    var ddlProcess = (DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlPProces");
                    var processCode = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lblProcessCode")).Text;

                    DataSet ds3 = da.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GETPROCESSCODE", "%", "", "", "", "", "", "");
                    ddlProcess.DataSource = ds3.Tables[0];
                    ddlProcess.DataTextField = "resdesc";
                    ddlProcess.DataValueField = "rescode";
                    ddlProcess.DataBind();
                    ddlProcess.Items.Add(new ListItem("None", "000000000000"));
                    ddlProcess.SelectedValue = processCode;
                }

            }



            if (this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) == "22")
            {
                //this.grvacc.Columns[10].Visible = true;

                DropDownList ddlgvinco = (DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlIncoterms");

                DataRow dr1;

                DataSet ds2 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO", "17%", "", "", "", "", "");
                dr1 = ds2.Tables[0].NewRow();
                dr1["gcod"] = "00000";
                dr1["gdesc"] = "NONE";
                dr1["comcod"] = comcod;
                ds2.Tables[0].Rows.Add(dr1);
                ddlgvinco.DataValueField = "gcod";
                ddlgvinco.DataTextField = "gdesc";
                ddlgvinco.DataSource = ds2.Tables[0];
                ddlgvinco.SelectedValue = tblStore.Rows[e.NewEditIndex]["incoterms"].ToString();
                ddlgvinco.DataBind();
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
            try
            {

                string comcod = this.GetComeCode();
                string sircode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
                string sircode2 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcode")).Text.Trim();
                string sircode = "";
                bool updateallow = true;//01-001-01-001

                if (sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    sircode = sircode2.Substring(0, 2) + sircode1.Substring(0, 2) + sircode1.Substring(3, 3) + sircode1.Substring(7, 2) + sircode1.Substring(10, 3);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid code!');", true);

                    updateallow = false;
                }

                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string Sirdescb = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirdescb")).Text.Trim();
                string txtsirtype = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridsirtype")).Text.Trim();
                string txtsirtdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirtdesc")).Text.Trim();
                string txtsirunit = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirunit")).Text.Trim();
                string txtsirval = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirval")).Text.Trim()).ToString();
                string txtgvsinfqty = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsinfqty")).Text.Trim()).ToString();
                string psircode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lbgrcod1")).Text.Trim();
                string actcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlAccCode")).Text.Trim();
                string PProces = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlPProces")).Text.Trim();
                string unitcode = "";
                string deptcode = "";

                string allowance = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvAllowance")).Text.Trim() == "" ? "0":
                                    ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvAllowance")).Text.Trim();
                
                string cnfmark = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvCnF")).Text.Trim() == "" ? "0" :
                                    ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvCnF")).Text.Trim();

                var ddlIncTrms = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlIncoterms"));
                string incotrms = ddlIncTrms.Items.Count > 0 ? ddlIncTrms.SelectedItem.Value : ((DataTable)ViewState["storedata"]).Rows[e.RowIndex]["incoterms"].ToString();
                //string incotrmdesc = ddlIncTrms.Items.Count > 0 ? ddlIncTrms.SelectedItem.Text : ((DataTable)ViewState["storedata"]).Rows[e.RowIndex]["incotermsdesc"].ToString();

                if (sircode2.Substring(0, 2) == "04")
                {
                    txtsirunit = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlunit")).SelectedItem.ToString();
                    unitcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlunit")).Text.Trim();
                    deptcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlMaterialGorup")).Text.Trim();

                }

                DataTable tbl1 = (DataTable)ViewState["storedata"];//check whether it is needed or not

                string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

                if (tempddl2 == "4" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) != psircode1.Substring(2, 3))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);

                        updateallow = false;
                    }
                    else if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);

                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);

                        updateallow = false;
                    }
                }
                else if (tempddl2 == "7" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(7, 2) != psircode1.Substring(5, 2))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);

                        updateallow = false;
                    }
                    else if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);

                        updateallow = false;
                    }
                }
                else if (tempddl2 == "9" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {

                    if (sircode1.Substring(10, 3) != psircode1.Substring(7, 3) || sircode1.Substring(3, 3) == "000")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);

                        updateallow = false;
                    }

                }
                else if (tempddl2 == "12" && psircode1 != sircode.Substring(2, 10) && sircode1.Length == 13 && sircode1.Substring(2, 1) == "-" && sircode1.Substring(6, 1) == "-" && sircode1.Substring(9, 1) == "-" && !sircode1.Contains(" "))
                {
                    if (sircode1.Substring(3, 3) == "000" && sircode1.Substring(7, 2) != "00" || sircode1.Substring(7, 2) == "00" && sircode1.Substring(10, 3) != "000")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Not Allowed');", true);

                        updateallow = false;
                    }
                }


                if (updateallow)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string userid = hst["usrid"].ToString();

                    int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;


                    bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", sircode2.Substring(0, 2), sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, actcode, unitcode,
                        Sirdescb, PProces, deptcode, txtgvsinfqty, allowance, cnfmark, incotrms, "");
                    this.ShowInformation();
                    if (result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed');", true);

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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
        }

        protected void grvacc_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)ViewState["storedata"];
                this.grvacc.Columns[16].Visible = ((this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2) == "81") ? true : false;

                string rescode = this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2);
                string groupcode = this.ddlcatagory.SelectedValue.ToString().Substring(0, 4);

                switch (rescode)
                {
                    case "22":
                        this.grvacc.Columns[10].Visible = true;
                        this.grvacc.Columns[11].Visible = true;
                        this.grvacc.Columns[12].Visible = true;
                        this.grvacc.Columns[13].HeaderText = "Short Desc.";
                        this.grvacc.Columns[19].HeaderText = "Production Process";
                        break;
                        
                    case "04":
                        this.grvacc.Columns[10].Visible = false;
                        this.grvacc.Columns[11].Visible = false;
                        this.grvacc.Columns[12].Visible = false;
                        this.grvacc.Columns[20].Visible = true;
                        this.grvacc.Columns[13].HeaderText = "Bond Name";
                        
                        if(groupcode == "0450")
                        {
                            this.grvacc.Columns[19].HeaderText = "Department";
                        }
                        else
                        {
                            this.grvacc.Columns[19].HeaderText = "Production Process";
                        }

                        break;

                    default:
                        this.grvacc.Columns[10].Visible = false;
                        this.grvacc.Columns[11].Visible = false;
                        this.grvacc.Columns[12].Visible = false;
                        this.grvacc.Columns[13].HeaderText = "Bond Name";
                        this.grvacc.Columns[19].HeaderText = "Production Process";
                        break;
                }


                //if (this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) == "22")
                //{
                //    //this.grvacc.Columns[6].HeaderText = "Description Of Code";
                //    this.grvacc.Columns[10].Visible = true;
                //    this.grvacc.Columns[11].Visible = true;
                //    this.grvacc.Columns[12].Visible = true;
                //    this.grvacc.Columns[13].HeaderText = "Short Desc.";
                //}
                //else if (this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) == "04" && this.ddlcatagory.SelectedValue.ToString().Substring(0, 4) == "0450")
                //{
                //    this.grvacc.Columns[10].Visible = false;
                //    this.grvacc.Columns[11].Visible = false;
                //    this.grvacc.Columns[12].Visible = false;
                //    this.grvacc.Columns[13].HeaderText = "Bond Name";
                //    this.grvacc.Columns[19].HeaderText = "Department";
                //}
                //else
                //{
                //    //this.grvacc.Columns[6].HeaderText = "Description Of Code";
                //    this.grvacc.Columns[10].Visible = false;
                //    this.grvacc.Columns[11].Visible = false;
                //    this.grvacc.Columns[12].Visible = false;
                //    this.grvacc.Columns[13].HeaderText = "Bond Name";
                //    this.grvacc.Columns[19].HeaderText = "Production Process";

                //}


                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

                GridView gvExcel = new GridView();
                gvExcel.DataSource = tbl1;
                gvExcel.DataBind();
                Session["Report1"] = gvExcel;
                ((HyperLink)this.grvacc.HeaderRow.FindControl("lnkExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
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
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ddlOthersBook.Enabled = false;
                    this.ddlOthersBookSegment.Enabled = false;
                    this.ibtnSrch.Visible = true;

                    if (ASTUtility.Left(ddlOthersBook.SelectedValue.ToString(), 2) == "05")
                    {
                       
                        grvacc.Columns[10].Visible = true;
                    }
                   
                    else if (ASTUtility.Left(ddlOthersBook.SelectedValue.ToString(), 2) == "04")
                    {
                        grvacc.Columns[5].HeaderText = "Material Name";
                        grvacc.Columns[6].HeaderText = "Description";
                        //grvacc.Columns[5].Visible = true;
                        grvacc.Columns[9].Visible = false;
                        //grvacc.Columns[5].HeaderText = "Unit";
                        //grvacc.Columns[6].HeaderText = "Std.Rate";
                        //grvacc.Columns[7].HeaderText = "Type";
                        //grvacc.Columns[8].HeaderText = "Type Desc";
                        grvacc.Columns[10].Visible = true;
                        grvacc.Columns[17].Visible = true;

                    }
                    else if (ASTUtility.Left(ddlOthersBook.SelectedValue.ToString(), 2) == "94")
                    {
                        
                        grvacc.Columns[7].HeaderText = "ID Card";


                    }
                    else if (ASTUtility.Left(ddlOthersBook.SelectedValue.ToString(), 2) == "81")
                    {
                        grvacc.Columns[7].HeaderText = "Manpower";
                        grvacc.Columns[9].HeaderText = "Capacity";


                    }

                    else
                    {
                        grvacc.Columns[5].HeaderText = "Item Code";
                        grvacc.Columns[6].HeaderText = "Description of Code";
                    }
                    this.ShowInformation();
                }
                else
                {

                    this.lnkok.Text = "Ok";
                    this.txtsrch.Text = "";
                    this.ibtnSrch.Visible = false;
                    this.ddlOthersBook.Enabled = true;
                    this.ddlOthersBookSegment.Enabled = true;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Information not found!!!!');", true);

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
            string catgrory = ((this.ddlcatagory.SelectedValue.ToString() == "0000") ? "" : this.ddlcatagory.SelectedValue.ToString()) + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", Calltype, tempddl1, tempddl2, srchoption, srchoption1, catgrory, "", "", "", "");
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

            DataSet tblmatgrp = this.da.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_MATERIAL_GROUP_NAME", "", "", "", "", "", "", "");
            ViewState["tblmatgrp"] = tblmatgrp.Tables[0];
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
                LinkButton lbtnAdd = (LinkButton)e.Row.FindControl("lbtnAdd");
                string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                int additem = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "additem"));


                if (Code == "")
                    return;

                if (ASTUtility.Right(Code, 3) == "000" && ASTUtility.Right(Code, 5) != "00000")
                {
                    e.Row.Attributes["style"] = "color:#3399FF;";
                }
                else if (ASTUtility.Right(Code, 8) == "00000000" && ASTUtility.Right(Code, 10) != "0000000000")
                {
                    e.Row.Attributes["style"] = "color:#EA4335;";
                }
                else if (ASTUtility.Right(Code, 5) == "00000" && ASTUtility.Right(Code, 8) != "00000000")
                {
                    e.Row.Attributes["style"] = "color:#34A853;";
                }
                //else if (ASTUtility.Right(Code, 10) == "0000000000")
                //{
                //    e.Row.Attributes["style"] = "background:#34A853;";
                //}

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("lbgrcode");
                //HyperLink hlnkDetails = (HyperLink)e.Row.FindControl("lnkDetails");
                HyperLink hlnkSpcf = (HyperLink)e.Row.FindControl("lnkSpcf");
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                string sirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sirdesc")).ToString();
               


                if (ASTUtility.Left(sircode, 2) == "41" && ASTUtility.Right(sircode, 3) != "000" || ASTUtility.Left(sircode, 7) == "0101001" && ASTUtility.Right(sircode, 3) != "000")
                {

                }
                if (ASTUtility.Left(sircode, 2) == "04" || ASTUtility.Left(sircode, 2) == "21" || ASTUtility.Left(sircode, 2) == "22")
                {

                    hlnkSpcf.Style.Add("color", "red");
                   
                    hlnkSpcf.NavigateUrl = "LinkSpecificCodeBook.aspx?sircode=" + sircode + "&sirdesc=" + sirdesc;

                }
                else if (ASTUtility.Left(sircode, 2) == "99" && ASTUtility.Right(sircode, 3) != "000")
                {
                    hlnkSpcf.NavigateUrl = "~/F_09_Commer/PurSupplierinfo?Type=SupDetails&sircode=" + sircode;
                    hlnkSpcf.Text = "Details";

                }

                else if (ASTUtility.Left(sircode, 2) == "51" && ASTUtility.Right(sircode, 3) != "000")
                {
                    hlnkSpcf.NavigateUrl = "~/F_01_Mer/PurCustInfo?Type=CusDetails&sircode=" + sircode;
                    hlnkSpcf.Text = "Details";

                }
                else
                {
                    hlnkSpcf.Text = "";
                    hlnkSpcf.Style.Add("color", "black");
                }



                if ((ASTUtility.Left(sircode, 2) == "41" || ASTUtility.Left(sircode, 2) == "21" || ASTUtility.Left(sircode, 2) == "22" || ASTUtility.Left(sircode, 2) == "01") && ASTUtility.Right(sircode, 3) != "000")
                {


                }
                else
                {


                }
                if (additem == 1)
                {
                    lbtnAdd.Visible = true;
                }

            }

        }


        protected void lbtnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;

                int RowIndex = (grvacc.PageSize) * (this.grvacc.PageIndex) + gvr.RowIndex;

               // int RowIndex = gvr.RowIndex;
                string comcod = this.GetComCode();
                string sircode = ((DataTable)ViewState["storedata"]).Rows[RowIndex]["sircode"].ToString();
                string actcode = ((DataTable)ViewState["storedata"]).Rows[RowIndex]["actcode"].ToString();
                this.lblsircode.Text = sircode;
                this.txtresourcecode.Text = sircode.Substring(0, 2) + "-" + sircode.Substring(2, 2) + "-" + sircode.Substring(4, 3) + "-" + sircode.Substring(7, 2) + "-" + ASTUtility.Right(sircode, 3);

                this.Chboxchild.Checked = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");
                this.chkbod.Visible = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");
                this.lblchild.Visible = (ASTUtility.Right(sircode, 8) == "00000000" && ASTUtility.Right(sircode, 10) != "0000000000") || (ASTUtility.Right(sircode, 5) == "00000" && ASTUtility.Right(sircode, 8) != "00000000") || (ASTUtility.Right(sircode, 3) == "000");


                if (this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) == "05")
                {

                    DataSet dss = Rprss.UpdateCode("SP_ENTRY_CODEBOOK", comcod, "GETACTCODE", "", "", "", "");
                    DropDownList ddl1 = (DropDownList)this.grvacc.Rows[RowIndex].FindControl("ddlAccCode");
                    ddl1.DataTextField = "actdesc";
                    ddl1.DataValueField = "actcode";
                    ddl1.DataSource = dss.Tables[0];
                    ddl1.DataBind();


                    string rescode = sircode;// rescode1 + rescode2;

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
                    this.grunit.Visible = false;
                    this.grucode.Visible = true;

                    DataTable unitble = (DataTable)ViewState["units"];
                    ddlunit.DataTextField = "gdesc";
                    ddlunit.DataValueField = "gcod";
                    ddlunit.DataSource = unitble;
                    ddlunit.DataBind();
                }
                else
                {
                    this.grunit.Visible = true;
                    this.grucode.Visible = false;
                }


                if (this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) == "93" || this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) == "94")
                {
                    this.grdescb.Visible = true;
                }
                else
                {
                    this.grdescb.Visible = false;

                }




                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModalAddCode();", true);
            }


            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }
        }

        protected void lbtnAddCode_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid = hst["usrid"].ToString();
                string isircode = this.lblsircode.Text.Trim();
                string tsircode = this.txtresourcecode.Text.Trim().Replace("-", "");
                string sircode = (this.Chboxchild.Checked) ? ((ASTUtility.Right(isircode, 8) == "00000000") ? (ASTUtility.Left(isircode, 4) + "001" + ASTUtility.Right(isircode, 5))
                    : ((ASTUtility.Right(isircode, 5) == "00000" && ASTUtility.Right(isircode, 8) != "00000000") ? (ASTUtility.Left(isircode, 7) + "01" + ASTUtility.Right(isircode, 3)) : ASTUtility.Left(isircode, 9) + "001"))
                    : ((isircode != tsircode) ? tsircode : isircode);
                string mnumber = (isircode == tsircode) ? "" : "manual";

                string Desc = this.txtresourcehead.Text.Trim();
                string Sirdescb = this.txtresourceheadB.Text.Trim();
                string txtsirtype = "";
                string txtsirtdesc = "";
                string txtsirunit = this.txtunit.Text.Trim();
                string txtsirval = Convert.ToDouble("0" + this.txtstdrate.Text.Trim()).ToString();
                string actcode = "";// this.ddlProject.Items.Count == 0 ? "" : this.ddlProject.SelectedValue.ToString();
                string unitcode = "";
                if (sircode.Substring(0, 2) == "04")
                {
                    txtsirunit = this.ddlunit.Items.Count == 0 ? "" : this.ddlunit.SelectedItem.Text.ToString();
                    unitcode = this.ddlunit.Items.Count == 0 ? "" : this.ddlunit.SelectedValue.ToString();
                }

                // return;

                if (Desc.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Resource Head is not empty');", true);
                    return;
                }
                else
                {

                    //bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTUPDATE", sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, actcode, unitcode,
                    //   Sirdescb, "", "", "", "");

                    bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "ADDRESOUCECODE", sircode, Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, actcode, mnumber,
                      unitcode, Sirdescb, "", "", "");

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + da.ErrorObject["Msg"].ToString() + "');", true);

                        return;

                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                    this.ShowInformation();
                    this.Chboxchild.Checked = false;

                }




            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);

            }

        }

        protected void lbtnUpdateDetails_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string rsircode = this.txtrsircode.Text;
            string sdetails = this.txtDetails.Text.Trim();

            bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "UPDATESDETAILS", rsircode, sdetails, "", "", "", "", "", "");

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);



        }
        protected void lbtnDetails_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            string rsircode = ((DataTable)Session["storedata"]).Rows[RowIndex]["sircode"].ToString();
            this.txtrsircode.Text = rsircode;
            this.GetDetailsInfo(rsircode);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);

        }

        protected void ddlOthersBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectResCodeLeb2();
        }
        private void GetResCodeleb2()
        {
            Session.Remove("tblresleb2");
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETRESCODELEVEL2", "", "", userid, "", "", "", "", "", "");
            Session["tblresleb2"] = ds1.Tables[0];
            ds1.Dispose();

        }
        private void SelectResCodeLeb2()
        {
            DataTable dt = ((DataTable)Session["tblresleb2"]).Copy();
            if (this.ddlOthersBook.SelectedValue.ToString().Length == 0)
                return;

            string mrescode = this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2);
            EnumerableRowCollection<DataRow> item = (from r in dt.AsEnumerable()
                                                     where (r.Field<string>("sircode").Substring(0, 2) == mrescode || r.Field<string>("sircode").Substring(0, 2) == "00")
                                                     select r);
            dt = item.AsDataView().ToTable();

            this.ddlcatagory.DataTextField = "sirdesc";
            this.ddlcatagory.DataValueField = "sircode";
            this.ddlcatagory.DataSource = dt;
            this.ddlcatagory.DataBind();
        }
        private void GetDetailsInfo(string rsircode)
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETRESOURCEDETEAILS", rsircode, "", "", "", "", "", "", "", "");
            this.txtDetails.Text = ds1.Tables[0].Rows[0]["sdetails"].ToString();
        }
    }
}