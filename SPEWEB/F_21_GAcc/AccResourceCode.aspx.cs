using Microsoft.Reporting.WinForms;
using SPEENTITY.C_22_Sal;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_21_GAcc
{
    public partial class AccResourceCode : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();
        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        CommonHelperClass helpercl = new CommonHelperClass();
      
        Common Common = new Common();
        GridView obj = new GridView();
        SalesInvoice_BL lst = new SalesInvoice_BL();



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = this.Request.QueryString["Type"].ToString() == "Matcode" ?  "Materials Code Book"
                                                                    : this.Request.QueryString["Type"].ToString() == "MatPriceSumm" ? "Material Price Summary" : "";
                if (this.ddlOthersBook.Items.Count == 0)
                    this.Load_CodeBooList();


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
                // lnkok_Click(null,null);
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
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "Matcode":
                    this.mvMaterial.ActiveViewIndex = 0;
                    this.ShowInformation();
                    break;

                case "MatPriceSumm":
                    this.mvMaterial.ActiveViewIndex = 1;
                    this.ShowInformation();

                    break;
            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "Matcode":
                    //this.grvacc.PageIndex = this.ddlpagesize.SelectedIndex;
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();
                    break;

                case "MatPriceSumm":
                    this.Data_Bind();
                    break;
            }

            

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
                string Querytype = this.Request.QueryString["Type"];
                string coderange = "04%";

                string comcod = this.GetComeCode();
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                Session["tblmatsubhead"] = dsone.Tables[1];
                this.ddlOthersBook.DataTextField = "sircode";
                this.ddlOthersBook.DataValueField = "sircode1";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
                this.ddlOthersBook_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
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
            try
            {
                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvacc.EditIndex = e.NewEditIndex;
                this.grvacc_DataBind();
                string comcod = this.GetComCode();
                DropDownList ddlgvinco = (DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlgvinco");

                DataRow dr1;

                DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO", "17%", "", "", "", "", "");
                dr1 = ds1.Tables[0].NewRow();
                dr1["gcod"] = "00000";
                dr1["gdesc"] = "NONE";
                dr1["comcod"] = comcod;
                ds1.Tables[0].Rows.Add(dr1);
                ddlgvinco.DataValueField = "gcod";
                ddlgvinco.DataTextField = "gdesc";
                ddlgvinco.DataSource = ds1.Tables[0];

                string incoterms= tbl1.Rows[e.NewEditIndex]["incoterms"].ToString().Trim();
                if (incoterms.Length > 0 || incoterms != "")
                {
                    ddlgvinco.SelectedValue = tbl1.Rows[e.NewEditIndex]["incoterms"].ToString();

                }
                ddlgvinco.DataBind();

                string resdesc = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgvDesc")).Text.Trim();
                DropDownList DdlSizelble=(DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("DdlSizelble");
                if (resdesc == "")
                {
                    ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgvDesc")).Enabled = false;
                    ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgridsirtype")).Enabled = false;
                    ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgvsirunit")).Enabled = false;
                    //((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgvsirval")).Enabled = false;
                    ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgvsirtdesc")).Enabled = false;
                    DdlSizelble.Enabled = false;

                }
                string rescode1 = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lbgrcode")).Text.Trim().Substring(0, 2);
                string rescode2 = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
                string rescodespc = ((Label)grvacc.Rows[e.NewEditIndex].FindControl("lblspcfcode")).Text.Trim().Replace("-", "");
                int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;
                string actcode = ((DataTable)Session["storedata"]).Rows[rowindex]["actcode"].ToString();

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
                    //this.grvacc.Columns[7].Visible = true;          
                    DataTable unitble = (DataTable)Session["units"];
                    DropDownList ddlgval = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlunit");
                    ddlgval.DataTextField = "gdesc";
                    ddlgval.DataValueField = "gcod";
                    ddlgval.DataSource = unitble;
                    ddlgval.DataBind();
                    string unitname = ((DataTable)Session["storedata"]).Rows[rowindex]["untcod"].ToString();
                    ddlgval.SelectedValue = (unitname == "") ? "00000" : unitname;
                    DataTable tblcolor = (DataTable)Session["tblcolor"];
                    DropDownList color = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("txtgvColor");
                    color.DataTextField = "gdesc";
                    color.DataValueField = "gcod";
                    color.DataSource = tblcolor;
                    color.DataBind();
                    string rescode = rescode1 + rescode2;

                    DataTable dt = (DataTable)Session["storedata"];
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "sircode = '" + rescode + "' and spcfcod ='" + rescodespc+ "'";
                    dt=dv.ToTable();
                    string colorname = dt.Rows[0]["colorid"].ToString();
                    string sizeble = dt.Rows[0]["sizeble"].ToString();
                    color.SelectedValue = colorname;
                    DdlSizelble.SelectedValue = sizeble == "True" ? "1": "0";
                }
                string rescode3 = ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgrcode")).Text.Trim();

                if (ASTUtility.Right(rescode3, 3) == "000")
                {
                    ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgvAllowance")).Enabled = true;
                }
                else
                {
                    ((TextBox)grvacc.Rows[e.NewEditIndex].FindControl("txtgvAllowance")).Enabled = false;
                }
            }
            catch (Exception ex)
            {
                return;
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
                DataTable dt = (DataTable)Session["tblcolor"];
                DataView dv = dt.DefaultView;
                string comcod = this.GetComeCode();
                string sircode = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblsircode1")).Text.Trim();
                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string txtsirtype = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridsirtype")).Text.Trim();
                string txtsirtdesc = (comcod == "5305" || comcod == "5306") ? ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("ddlgvinco")).SelectedItem.ToString() : ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirtdesc")).Text.Trim();
                string txtsirunit = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirunit")).Text.Trim();
                string spcfcod = ((Label)grvacc.Rows[e.RowIndex].FindControl("LblSpcfcod")).Text.Trim();
                string spcfdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsize")).Text.Trim();
                string thickness = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvThickness")).Text.Trim();
                string color = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("txtgvColor")).SelectedItem.ToString();
                string inco = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("ddlgvinco")).SelectedValue.ToString();
                string colorid = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("txtgvColor")).SelectedValue.ToString();
                string brand = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvBrand")).Text.Trim();
                string allowance = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvAllowance")).Text.Trim();
                string sizeble = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("DdlSizelble")).SelectedValue.Trim();
                string convertible = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("DdlConvertible")).SelectedValue.Trim();

                //  string colorcode = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvColorCode")).Text.Trim();
                dv.RowFilter = "gcod = '" + colorid + "'";

                string colorcode = dv.ToTable().Rows[0]["colcode"].ToString();// ((TextBox)gvSpcfinfo.Rows[i].FindControl("TxtProdCode")).Text.ToString();

                string other = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvOther")).Text.Trim();

                string txtsirval = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirval")).Text.Trim()).ToString();
                string actcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlAccCode")).Text.Trim();
                string unitcode = "";
                string txtcfprnct = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvCfPercnt")).Text.Trim()).ToString();

                if (sircode.Substring(0, 2) == "04")
                {
                    txtsirunit = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlunit")).SelectedItem.ToString();
                    unitcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlunit")).SelectedValue.ToString();
                }

                DataTable tbl1 = (DataTable)Session["storedata"];//check whether it is needed or not

                string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();

                bool result = this.da.UpdateTransInfo1(comcod, "SP_ENTRY_CODEBOOK02", "UPDATE_MATERIALS_CODE_BOOK", sircode,
                    Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, actcode, unitcode, spcfcod, spcfdesc,
                    thickness, color, brand, other, colorcode, sizeble, txtcfprnct, allowance, inco, convertible);
                //bool result = true;
                this.ShowInformation();
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Invalid!!!');", true);
                }
                this.grvacc.EditIndex = -1;
                this.grvacc_DataBind();


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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error" + ex.Message + "');", true);


            }
        }

        protected void grvacc_DataBind()
        {
            try
            {
                //DataTable tbl1 = (DataTable)Session["storedata"];
                if (this.GetComCode() != "5301")
                {
                    this.grvacc.Columns[13].HeaderText = "Origin";
                    this.grvacc.Columns[19].Visible = false;
                    this.grvacc.Columns[20].Visible = true;
                }
                else
                {
                    this.grvacc.Columns[19].Visible = true;
                    this.grvacc.Columns[20].Visible = false;

                }
                string materialCode = this.ddlmaterials.SelectedValue.Trim() == "000000000000" ? "%" : "%" + this.ddlmaterials.SelectedValue.Trim() + "%";
                DataTable dt = (DataTable)Session["storedata"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "sircode3 like '" + materialCode + "' and sircode3 <> '000000000000'";

                Session["storedata"] = dv.ToTable();
                this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvacc.DataSource = HiddenSaveValue(dv.ToTable());
                this.grvacc.DataBind();

            }
            catch (Exception ex)
            {
            }
        }
       
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "Matcode":
                    this.Print_Mat_Code_Opening_Report();
                    break;
                case "MatPriceSumm":
                    this.Print_Mat_Price_Summary_Report();
                    break;
            }

            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExcelPrint();", true);
        }

        private void Print_Mat_Price_Summary_Report()
        {
            DataTable dt = (DataTable) ViewState["tblPriceSummary"];

            if (dt == null || dt.Rows.Count == 0) return;

            var lst = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.RptMatPriceSummary>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string hostname = hst["hostname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptMatPriceSummary", lst, "", "");
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Material Price Summary"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void Print_Mat_Code_Opening_Report()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["storedata"];
            DataTable dt1 = (DataTable)Session["units"];

            var lst = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.ResCodeBook>();
            var lst1 = dt1.DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.Unit>();

            LocalReport rpt1 = new LocalReport();
            switch (this.GetComCode())
            {
                case "5305":
                case "5306":
                    rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptMaterialsCodeBookFB", lst, lst1, null);

                    break;
                default:
                    rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptMaterialsCodeBook", lst, lst1, null);
                    break;
            }

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));


            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "Matcode":
                    this.mvMaterial.ActiveViewIndex = 0;
                    this.ExcecuteMaterialCodeOpeningPart();
                    break;

                case "MatPriceSumm":
                    this.mvMaterial.ActiveViewIndex = 1;
                    this.GetMaterialPriceSummary();
                    this.Data_Bind();
                    break;
            }

        }

        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString();

            DataTable dt = new DataTable();

            switch (type)
            {
                case "MatPriceSumm":
                    dt = (DataTable)ViewState["tblPriceSummary"];

                    if (dt == null || dt.Rows.Count == 0) return;
                    else {
                        this.lblPage.Visible = true;
                        this.ddlpagesize.Visible = true;
                    } 
                    
                    this.gvMatPriceSumm.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
                    this.gvMatPriceSumm.DataSource = dt;
                    this.gvMatPriceSumm.DataBind();
                    break;
            }
        }

        private void GetMaterialPriceSummary()
        {
            //string codebook = this.ddlOthersBook.SelectedValue.Substring(0, 4) + "%";
            //string group = this.ddlOthersBookSegment.SelectedValue.Substring(0, 7) + "%";

            //[SP_ENTRY_CODEBOOK02] '5305','MATERIAL_PRICE_SUMMARY',null,null,null,'04%','0401001%',''
            //DataSet dsPriceSumm = purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "MATERIAL_PRICE_SUMMARY", codebook, group, "");


            string comcod = Common.GetCompCode();
            string srchoptionmain = this.txtsrch.Text.Trim();
            string testdata = "%" + this.testdata.Text.Trim() + "%";
            string srchoption1 = "";
            string srchoption = (srchoptionmain.Contains("-")) ? srchoptionmain : "%" + srchoptionmain + "%";
            if (srchoption.Contains("-"))
            {
                int index = srchoption.IndexOf("-");
                srchoption = srchoptionmain.Substring(0, index);
                srchoption1 = srchoptionmain.Substring(index + 1).Replace("%", "");
            }

            string tempddl1 = (this.ddlOthersBookSegment.SelectedValue.ToString().Substring(4, 8) == "00000000") ? this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) + "%" : (this.ddlOthersBookSegment.SelectedValue.ToString()).Substring(0, 7) + "%";

            DataSet dsPriceSumm = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "MATERIAL_PRICE_SUMMARY", tempddl1, srchoption, srchoption1, testdata, "", "", "", "");

            if(dsPriceSumm == null || dsPriceSumm.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            ViewState["tblPriceSummary"] = dsPriceSumm.Tables[0];
        }

        private void ExcecuteMaterialCodeOpeningPart()
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
                    this.ddlmaterials.Enabled = false;
                    this.lbalterofddl.Visible = true;
                    this.lbalterofddl0.Visible = true;
                    this.ibtnSrch.Visible = true;
                    this.lbalterofddl.Text = this.ddlOthersBook.SelectedItem.ToString().Trim();
                    this.lbalterofddl0.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    //tempddl1 = ASTUtility.Left(this.ddlOthersBook.SelectedItem.ToString(), 2);


                    if (ASTUtility.Left(ddlOthersBook.SelectedValue.ToString(), 2) == "04")
                    {
                        grvacc.Columns[5].HeaderText = "Material code";
                        grvacc.Columns[6].HeaderText = "Description";

                        grvacc.Columns[10].Visible = true;

                    }

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

                    this.LblBookName1.Text = "Select Code Book:";
                    this.ibtnSrch.Visible = false;
                    this.lbalterofddl.Visible = false;
                    this.lbalterofddl0.Visible = false;
                    this.ddlOthersBook.Visible = true;
                    this.ddlOthersBookSegment.Visible = true;
                    this.ddlmaterials.Enabled = true;
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
            Session.Remove("DetailsCode");
            string srchoptionmain = this.txtsrch.Text.Trim();
            string testdata = "%" + this.testdata.Text.Trim() + "%";
            string srchoption1 = "";
            string srchoption = (srchoptionmain.Contains("-")) ? srchoptionmain : "%" + srchoptionmain + "%";
            if (srchoption.Contains("-"))
            {
                int index = srchoption.IndexOf("-");
                srchoption = srchoptionmain.Substring(0, index);
                srchoption1 = srchoptionmain.Substring(index + 1).Replace("%", "");
            }
            string tempddl1 = (this.ddlOthersBookSegment.SelectedValue.ToString().Substring(4, 8) == "00000000") ? this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) + "%" : (this.ddlOthersBookSegment.SelectedValue.ToString()).Substring(0, 7) + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_RESOURCE_CODEBOOK", tempddl1, srchoption, srchoption1, testdata, "", "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }

            DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_NAME", "", "", "", "", "", "", "");
            Session["units"] = dsone.Tables[0];
            DataSet colorinfo = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_COLOR_CODE", "", "", "", "", "", "", "");
            Session["tblcolor"] = colorinfo.Tables[0];
            Session["storedata"] = ds1.Tables[0];

            this.grvacc_DataBind();

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string Code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                    string materialCode = this.ddlmaterials.SelectedValue.Trim();
                    string groupcod = this.ddlOthersBookSegment.SelectedValue.Trim();
                    if (Code == "")
                        return;
                    int index = ((this.grvacc.PageSize * this.grvacc.PageIndex) + e.Row.RowIndex)+1;
                    // int pagesize = (this.grvacc.PageSize * this.grvacc.PageIndex);
                    int gindex = e.Row.RowIndex;
                    materialCode = this.ddlmaterials.SelectedValue.Trim() == "000000000000" ? "%" : "%" + this.ddlmaterials.SelectedValue.Trim() + "%";
                    DataTable dt = (DataTable)Session["storedata"];
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "sircode3 like '" + materialCode + "' and sircode3 <> '000000000000'";
                    if (dv.Count == index)
                    {
                        ((LinkButton)e.Row.FindControl("lbtnAdd")).Visible = true;

                    }
                    //if (ASTUtility.Right(Code, 3) == "000" && ASTUtility.Right(Code, 5) != "00000" && index != 0)
                    //{
                    //    int newindex = (gindex - 1);
                    //    if (newindex > 0)
                    //    {
                    //        //string sircode1 = ((Label)grvacc.Rows[newindex].FindControl("lblgvsircode")).Text.Trim();
                    //        string sircode1 = tbl1.Rows[index - 1]["sircode"].ToString();

                    //        if (ASTUtility.Right(sircode1, 3) != "000" && ASTUtility.Right(sircode1, 5) != "00000")
                    //        {

                    //            ((LinkButton)this.grvacc.Rows[newindex].FindControl("lbtnAdd")).Visible = true;
                    //        }
                    //    }
                    //}
                    //if (materialCode!="000000000000" || groupcod !="040000000000")
                    //{
                    //    //int newindex = (gindex - 1 );
                    //     materialCode = this.ddlmaterials.SelectedValue.Trim() == "000000000000" ? "%" : "%" + this.ddlmaterials.SelectedValue.Trim() + "%";
                    //    DataTable dt = (DataTable)Session["storedata"];
                    //    DataView dv = dt.DefaultView;
                    //    dv.RowFilter = "sircode3 like '" + materialCode + "' and sircode3 <> '000000000000'";
                    //    if (index == dv.Count-1)
                    //    {


                    //            ((LinkButton)this.grvacc.Rows[gindex].FindControl("lbtnAdd")).Visible = true;
                            
                    //    }
                    //}

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
                    HyperLink hlnkSpcf = (HyperLink)e.Row.FindControl("lnkSpcf");
                    HyperLink hlnkSerialbtn = (HyperLink)e.Row.FindControl("hlnkSerialbtn1");
                    string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();
                    string sirdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sirdesc")).ToString();

                    string spcfcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfcod")).ToString();

                    if (ASTUtility.Left(sircode, 2) == "04")
                    {

                        hlnkSpcf.Style.Add("color", "red");
                        hlnkSpcf.NavigateUrl = "LinkSpecificCodeBook.aspx?sircode=" + sircode + "&sirdesc=" + sirdesc;
                    }
                    else
                    {
                        hlnkSpcf.Text = "";
                        hlnkSpcf.Style.Add("color", "black");
                    }

                    if (ASTUtility.Right(sircode, 3) != "000")
                    {

                        hlnkSerialbtn.Style.Add("color", "blue");
                        hlnkSerialbtn.NavigateUrl = "AddMaterialWiseSuppl.aspx?Type=Entry&sircode=" + sircode + "&spcfcod=" + spcfcod;
                    }
                    else
                    {

                        hlnkSerialbtn.Style.Add("color", "black");
                    }

                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "survey"))==false && ASTUtility.Right(sircode, 3) != "000")
                    {
                        //e.Row.CssClass = "bg-";
                        e.Row.ToolTip="Default Supplier Survey missing";
                        e.Row.BackColor = System.Drawing.Color.LightSkyBlue;

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        private DataTable HiddenSaveValue(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return dt;
            string sircode = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (sircode == dt.Rows[i]["sircode"].ToString())
                {
                    dt.Rows[i]["sirdesc"] = "";

                    //dt.Rows[i]["sirunit"] = "";
                    //dt.Rows[i]["sirval"] = 0.00;
                    dt.Rows[i]["sirtdes"] = "";
                    //dt.Rows[i]["sircode2"] = "";
                    //dt.Rows[i]["sircode4"] = "";

                }
                sircode = dt.Rows[i]["sircode"].ToString();
            }
            return dt;
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission to add');", true);

                return;
            }
            try
            {
                Session.Remove("tblspcfinfo");
                string comcod = this.GetComCode();
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string sircode = ((Label)this.grvacc.Rows[index].FindControl("lblgvsircode")).Text.ToString();
                this.lblSircode.Text = sircode;
                this.lalplug.Text = "resource";
                this.Recourcepanel.Visible = true;
                DataTable unitble = (DataTable)Session["units"];
                this.ddlUnit.DataTextField = "gdesc";
                this.ddlUnit.DataValueField = "gcod";
                this.ddlUnit.DataSource = unitble;
                this.ddlUnit.DataBind();
                List<SPEENTITY.C_21_Acc.EClassSpecification> lst = new List<SPEENTITY.C_21_Acc.EClassSpecification>();
                lst.Add(new SPEENTITY.C_21_Acc.EClassSpecification(sircode, "", "", 0, "", "None", "", "", ""));
                Session["tblspcfinfo"] = lst;
                this.SpecificationBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
            }
            catch (Exception ex)
            {

            }
        }
        
        protected void gvSpcfinfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex > 0)
                {
                    e.Row.FindControl("lbAddMore").Visible = false;
                }
                DataTable tblcolor = (DataTable)Session["tblcolor"];
                DropDownList color = (DropDownList)e.Row.FindControl("txtgvMColor");
                color.DataTextField = "gdesc";
                color.DataValueField = "gcod";
                color.DataSource = tblcolor;
                color.DataBind();
                color.SelectedIndex = e.Row.RowIndex;

            }

        }

        protected void gvSpcfinfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<SPEENTITY.C_21_Acc.EClassSpecification> lst = (List<SPEENTITY.C_21_Acc.EClassSpecification>)Session["tblspcfinfo"];

            int index = (this.gvSpcfinfo.PageIndex) * this.gvSpcfinfo.PageSize + e.RowIndex;
            lst.RemoveAt(index);
            Session["tblspcfinfo"] = lst;
            this.SpecificationBind();
        }
        
        private void SpecificationBind()
        {
            List<SPEENTITY.C_21_Acc.EClassSpecification> lst = (List<SPEENTITY.C_21_Acc.EClassSpecification>)Session["tblspcfinfo"];
            if (this.GetComCode() != "5301")
            {
                this.gvSpcfinfo.Columns[6].HeaderText = "Origin";
            }
            this.gvSpcfinfo.DataSource = lst;
            this.gvSpcfinfo.DataBind();
        }
        
        private void Save_Value()
        {

            var spcfinfo = (List<SPEENTITY.C_21_Acc.EClassSpecification>)Session["tblspcfinfo"];
            DataTable dt = (DataTable)Session["tblcolor"];
            DataView dv = dt.DefaultView;
            if (spcfinfo == null || spcfinfo.Count == 0)
                return;
            for (int i = 0; i < this.gvSpcfinfo.Rows.Count; i++)
            {
                string sizedesc = ((TextBox)gvSpcfinfo.Rows[i].FindControl("txtgvMSize")).Text.ToString();
                string thickness = ((TextBox)gvSpcfinfo.Rows[i].FindControl("txtgMThikness")).Text.ToString();
                string color = ((DropDownList)gvSpcfinfo.Rows[i].FindControl("txtgvMColor")).SelectedItem.ToString();
                string colorcode = ((DropDownList)gvSpcfinfo.Rows[i].FindControl("txtgvMColor")).SelectedValue.ToString();
                string brand = ((TextBox)gvSpcfinfo.Rows[i].FindControl("txtMBrand")).Text.ToString();
                string other = ((TextBox)gvSpcfinfo.Rows[i].FindControl("txtgMOther")).Text.ToString();
                dv.RowFilter = "gcod = '" + colorcode + "'";

                string proCode = dv.ToTable().Rows[0]["colcode"].ToString();// ((TextBox)gvSpcfinfo.Rows[i].FindControl("TxtProdCode")).Text.ToString();

                spcfinfo[i].spcfdesc = sizedesc;
                spcfinfo[i].desc1 = thickness;
                spcfinfo[i].desc2 = color;
                spcfinfo[i].desc3 = brand;
                spcfinfo[i].desc4 = other;
                spcfinfo[i].desc5 = proCode;

            }
            Session["tblspcfinfo"] = spcfinfo;
        }

        protected void lblbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Save_Value();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string comcod = this.GetComCode();
                string sircode = this.lblSircode.Text.Trim().ToString();
                string matname = this.TxtMatName.Text.Trim().ToString();
                string description = this.txtDescription.Text.Trim().ToString();
                string value = Convert.ToDouble("0" + this.TxtValue.Text.Trim()).ToString();
                string bondname = this.txtBondName.Text.Trim().ToString();
                string txtCF = Convert.ToDouble("0"+ this.txtCF.Text.Trim().ToString()).ToString();


                var spcfinfo = (List<SPEENTITY.C_21_Acc.EClassSpecification>)Session["tblspcfinfo"];
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(ASITUtility03.ListToDataTable(spcfinfo));
                ds.Tables[0].TableName = "tblspcf";
                bool result = false;
                if (this.lalplug.Text == "resource")
                {
                    string unit = this.ddlUnit.SelectedValue.Trim().ToString();
                    string unitname = this.ddlUnit.SelectedItem.ToString();

                    if (description.Length == 0)
                        return;
                    //string x = ds.GetXml();
                    result = this.da.UpdateXmlTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SAVE_MATERIALCODE", ds, null, null, sircode, matname, description, unit, value, bondname, userid, unitname, txtCF);

                }
                else
                {
                    result = this.da.UpdateXmlTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "SAVE_SPECIFICATION_CODE", ds, null, null, sircode);

                }
                if (result)
                {
                    this.ShowInformation();
                    Common.LogStatus("Materials Code book Opening", "Update Code Material Code book", "Update/Modification Code book from Material COde book Opening", "");
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Save Successfully');", true);

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Error" + ex.Message + "');", true);


            }
        }

        protected void lbtnAddSpcf_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("tblspcfinfo");
                string comcod = this.GetComCode();
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string sircode = ((Label)this.grvacc.Rows[index].FindControl("lblgvsircode")).Text.ToString();
                string size = ((Label)this.grvacc.Rows[index].FindControl("lblsize")).Text.ToString();
                double price = Convert.ToDouble(((Label)this.grvacc.Rows[index].FindControl("lblsirval")).Text.Trim() == "" ? "0" : ((Label)this.grvacc.Rows[index].FindControl("lblsirval")).Text);
                string thickness = ((Label)this.grvacc.Rows[index].FindControl("lblthickness")).Text.ToString();
                string brand = ((Label)this.grvacc.Rows[index].FindControl("lblBrand")).Text.ToString();
                string color = ((Label)this.grvacc.Rows[index].FindControl("lblcolorid")).Text.ToString();

                this.lblSircode.Text = sircode;
                this.lalplug.Text = "specification";
                this.Recourcepanel.Visible = false;
                List<SPEENTITY.C_21_Acc.EClassSpecification> lst = new List<SPEENTITY.C_21_Acc.EClassSpecification>();
                lst.Add(new SPEENTITY.C_21_Acc.EClassSpecification(sircode, "", size, price, thickness, "", brand, "", ""));
                Session["tblspcfinfo"] = lst;
                this.SpecificationBind();
                DropDownList ddlcolor = ((DropDownList)this.gvSpcfinfo.Rows[0].FindControl("txtgvMColor"));

                ddlcolor.SelectedValue = color;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlOthersBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mathead = this.ddlOthersBook.SelectedValue.ToString().Substring(0, 4) + "%";
            DataTable dt = (DataTable)Session["tblmatsubhead"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode1 like '" + mathead + "'";
            this.ddlOthersBookSegment.DataTextField = "sircode";
            this.ddlOthersBookSegment.DataValueField = "sircode1";
            this.ddlOthersBookSegment.DataSource = dv.ToTable();
            this.ddlOthersBookSegment.DataBind();

            if(mathead != "0400%")
                ddlOthersBookSegment_SelectedIndexChanged(null, null);
        }

        protected void LbtnGenerat_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sircode = ((Label)this.grvacc.Rows[index].FindControl("lblgvsircode")).Text.ToString();
            if (ASTUtility.Right(sircode, 3) != "000")
            {
                DataTable tbl1 = (DataTable)Session["storedata"];
                DataView dv1 = tbl1.DefaultView;
                dv1.RowFilter = "sircode ='" + sircode + "'";

                string matname = dv1.ToTable().Rows[0]["sirdesc"].ToString();// ((HyperLink)this.grvacc.Rows[index].FindControl("hlnkgvdesc")).Text.ToString();


                string spcfcod = ((Label)this.grvacc.Rows[index].FindControl("lblspcf")).Text.ToString();
                string descript = ((Label)this.grvacc.Rows[index].FindControl("lblsize")).Text.ToString();
                string widn = ((Label)this.grvacc.Rows[index].FindControl("lblthickness")).Text.ToString();
                string color = ((Label)this.grvacc.Rows[index].FindControl("lblcolorCode")).Text.ToString();
                //  string colorcode = ((HyperLink)this.grvacc.Rows[index].FindControl("hlnkgvdesc")).Text.ToString();

                DataTable dt = (DataTable)Session["tblmatsubhead"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "sircode1 like '" + sircode.Substring(0, 7) + "%'";
                DataTable dt2 = dv.ToTable();
                //  Array sirdesc = new Array[10];
                var sirdesc = matname.Split(' ');
                var tempname = "";
                descript = Regex.Replace(descript, "[^0-9.]", "");
                widn = Regex.Replace(widn, "[^0-9.]", "");
                foreach (var sirdesc1 in sirdesc)
                {
                    tempname = (tempname.Length == 0) ? sirdesc1.ToString().Substring(0, 2) : (sirdesc1.ToString().Length > 1) ? tempname + sirdesc1.ToString().Substring(0, 2) : "";
                }

                string gencode = sircode.Substring(5, 2) + "-" + dt2.Rows[0]["sircode"].ToString().Substring(8, 3) + "-" + tempname + ((descript == "") ? "" : "-" + descript) + ((widn == "") ? "" : "-" + widn) + ((color == "") ? "" : "-" + color);
                string comcod = this.GetComCode();

                bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "UPDATE_MATERIAL_CODE", sircode, spcfcod, gencode);
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Save Successfully');", true);

                }
            }
        }

        protected void ddlOthersBookSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string materialCode = (this.ddlmaterials.SelectedValue.Trim().ToString()) != "" ? this.ddlmaterials.SelectedValue.Trim().ToString() : "%";


            string comcod = this.GetComeCode();
            Session.Remove("DetailsCode");
            string srchoptionmain = this.txtsrch.Text.Trim();
            string testdata = "%" + this.testdata.Text.Trim() + "%";
            string srchoption1 = "";
            string srchoption = (srchoptionmain.Contains("-")) ? srchoptionmain : "%" + srchoptionmain + "%";
            if (srchoption.Contains("-"))
            {
                int index = srchoption.IndexOf("-");
                srchoption = srchoptionmain.Substring(0, index);
                srchoption1 = srchoptionmain.Substring(index + 1).Replace("%", "");
            }
            string tempddl1 = (this.ddlOthersBookSegment.SelectedValue.ToString().Substring(4, 8) == "00000000") ? this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) + "%" : (this.ddlOthersBookSegment.SelectedValue.ToString()).Substring(0, 7) + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_RESOURCE_CODEBOOK", tempddl1, srchoption, srchoption1, testdata, "", "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }

            Session["storedata"] = ds1.Tables[0];
            DataTable dt = (ds1.Tables[0].Copy());
            

            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode3 not like '%000'";
            

            DataTable dt1 = dv.ToTable();
            DataRow dr1 = dt1.NewRow();
            dr1["sircode3"] = "000000000000";
            dr1["sirdesc"] = "----Select All---";
            dt1.Rows.Add(dr1);
            DataView dv1 = dt1.DefaultView;


            this.ddlmaterials.DataTextField = "sirdesc";
            this.ddlmaterials.DataValueField = "sircode3";
            this.ddlmaterials.DataSource = dv1.ToTable(true, "sircode3", "sirdesc");
            this.ddlmaterials.DataBind();
            this.ddlmaterials.SelectedValue = "000000000000";

        }

        protected void lbtnallinone_Click(object sender, EventArgs e)
        {
            bool result = false;


            for (int index = 0; index < this.grvacc.Rows.Count; index++)
            {

                string sircode = ((Label)this.grvacc.Rows[index].FindControl("lblgvsircode")).Text.ToString();
                if (ASTUtility.Right(sircode, 3) != "000")
                {
                    string spcfcod = ((Label)this.grvacc.Rows[index].FindControl("lblspcf")).Text.ToString();
                    DataTable tbl1 = (DataTable)Session["storedata"];
                    DataView dv1 = tbl1.DefaultView;
                    dv1.RowFilter = "sircode ='" + sircode + "'";

                    string matname = dv1.ToTable().Rows[0]["sirdesc"].ToString();// ((HyperLink)this.grvacc.Rows[index].FindControl("hlnkgvdesc")).Text.ToString();

                    string descript = ((Label)this.grvacc.Rows[index].FindControl("lblsize")).Text.ToString();
                    string widn = ((Label)this.grvacc.Rows[index].FindControl("lblthickness")).Text.ToString();
                    // string color = ((Label)this.grvacc.Rows[index].FindControl("lblcolor")).Text.ToString();
                    string color = ((Label)this.grvacc.Rows[index].FindControl("lblcolorCode")).Text.ToString();
                    //    string descript = ((HyperLink)this.grvacc.Rows[index].FindControl("hlnkgvdesc")).Text.ToString();

                    DataTable dt = (DataTable)Session["tblmatsubhead"];
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "sircode1 like '" + sircode.Substring(0, 7) + "%'";
                    DataTable dt2 = dv.ToTable();
                    //  Array sirdesc = new Array[10];
                    var sirdesc = matname.Split(' ');
                    var tempname = "";
                    descript = Regex.Replace(descript, "[^0-9.]", "");
                    widn = Regex.Replace(widn, "[^0-9.]", "");
                    foreach (var sirdesc1 in sirdesc)
                    {
                        tempname = (tempname.Length == 0) ? sirdesc1.ToString().Substring(0, 2) : (sirdesc1.ToString().Length > 1) ? tempname + sirdesc1.ToString().Substring(0, 2) : tempname;
                    }

                    string gencode = sircode.Substring(5, 2) + "-" + dt2.Rows[0]["sircode"].ToString().Substring(8, 3) + "-" + tempname + ((descript == "") ? "" : "-" + descript) + ((widn == "") ? "" : "-" + widn) + ((color == "") ? "" : "-" + color);
                    string comcod = this.GetComCode();

                    result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "UPDATE_MATERIAL_CODE", sircode, spcfcod, gencode);

                }
            }
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Save Successfully');", true);


            }
        }

        protected void gvMatPriceSumm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatPriceSumm.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}