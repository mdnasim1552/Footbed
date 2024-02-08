using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Xml.Linq;
using SPEENTITY;
using SPELIB;
using SPEENTITY.C_09_Commer;
using SPEENTITY.C_22_Sal;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.Text.RegularExpressions;

namespace SPEWEB.F_21_GAcc
{
    public partial class AccResourceCodeOpnStk : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();
        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        CommonHelperClass helpercl = new CommonHelperClass();
        static string[] CarArray = new string[3] { "Sub Code-1", "Sub Code-2", "Details Code" };
        static string tempddl1 = "", tempddl2 = "";
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Material Code book with Opening stock";
                if (this.ddlOthersBook.Items.Count == 0)
                    this.Load_CodeBooList();
                GetProject();

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
                ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


                ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
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
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnSave_Click);


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
                string Querytype = this.Request.QueryString["Type"];
                string coderange = "04%";

                string comcod = this.GetComeCode();
                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                ViewState["tblmatsubhead"] = dsone.Tables[1];
                this.ddlOthersBook.DataTextField = "sircode";
                this.ddlOthersBook.DataValueField = "sircode1";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
                this.ddlOthersBook_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail(" + ex.Message + "');", true);
            }
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                DataTable dt = (DataTable)ViewState["tblcolor"];
                DataView dv = dt.DefaultView;
                string comcod = this.GetComeCode();
                string sircode = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblsircode1")).Text.Trim();
                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string txtsirtype = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgridsirtype")).Text.Trim();
                string txtsirtdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirtdesc")).Text.Trim();
                string txtsirunit = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirunit")).Text.Trim();
                string spcfcod = ((Label)grvacc.Rows[e.RowIndex].FindControl("LblSpcfcod")).Text.Trim();
                string spcfdesc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsize")).Text.Trim();
                string thickness = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvThickness")).Text.Trim();
                string color = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("txtgvColor")).SelectedItem.ToString();
                string colorid = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("txtgvColor")).SelectedValue.ToString();
                string brand = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvBrand")).Text.Trim();
                string sizeble = ((DropDownList)grvacc.Rows[e.RowIndex].FindControl("DdlSizelble")).SelectedValue.Trim();

                //  string colorcode = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvColorCode")).Text.Trim();
                dv.RowFilter = "gcod = '" + colorid + "'";

                string colorcode = dv.ToTable().Rows[0]["colcode"].ToString();// ((TextBox)gvSpcfinfo.Rows[i].FindControl("TxtProdCode")).Text.ToString();

                string other = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvOther")).Text.Trim();

                string txtsirval = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvsirval")).Text.Trim()).ToString();
                string actcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlAccCode")).Text.Trim();
                string unitcode = "";
                if (sircode.Substring(0, 2) == "04")
                {
                    txtsirunit = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlunit")).SelectedItem.ToString();
                    unitcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlunit")).SelectedValue.ToString();
                }

                DataTable tbl1 = (DataTable)ViewState["storedata"];//check whether it is needed or not

                string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                //  int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;


                bool result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "UPDATE_MATERIALS_CODE_BOOK", sircode,
                    Desc, txtsirtype, txtsirtdesc, txtsirunit, txtsirval, userid, actcode, unitcode, spcfcod, spcfdesc,
                    thickness, color, brand, other, colorcode, sizeble);
                this.ShowInformation();
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Invalid!!!');", true);
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
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }



        protected void grvacc_AllRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("sircode");
                dt.Columns.Add("Desc1");
                string comcod = this.GetComeCode();

                foreach (GridViewRow row in grvacc.Rows)
                {

                    TextBox txtName = row.FindControl("spcfcod") as TextBox;
                    TextBox txtCode = row.FindControl("txtgvDesc") as TextBox;

                    string sircode = txtName == null ? "" : txtName.ToString();
                    string Desc = txtCode == null ? "" : txtCode.Text.ToString();
                    DataRow _ravi = dt.NewRow();
                    _ravi["sircode"] = sircode;
                    _ravi["Desc1"] = Desc;
                    dt.Rows.Add(_ravi);
                }
                DataTable tbl1 = (DataTable)ViewState["storedata"];//check whether it is needed or not

                string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                //  int Index = grvacc.PageSize * grvacc.PageIndex + i;


                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                var result = this.da.UpdateXmlTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "UPDATE_MATERIALS_CODE_BOOK02", ds, null, null);
                this.ShowInformation();
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Invalid!!!');", true);
                }
                this.grvacc.EditIndex = -1;
                this.grvacc_DataBind();

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
                this.grvacc.DataSource = HiddenSaveValue(tbl1);
                this.grvacc.DataBind();

                this.PrepareExcelDownload(tbl1);

            }
            catch (Exception ex)
            {
            }
        }


        protected void PrepareExcelDownload(DataTable dt)
        {
            try
            {
                if (dt == null) return;

                GridView gvExcel = new GridView();
                gvExcel.AllowPaging = false;
                gvExcel.DataSource = dt;
                gvExcel.DataBind();

                Session["Report1"] = gvExcel;
                ((HyperLink)this.grvacc.HeaderRow.FindControl("hyplnkExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception Ex)
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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["storedata"];
            DataTable dt1 = (DataTable)ViewState["units"];

            var lst = dt.DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.ResCodeBook>();
            var lst1 = dt1.DataTableToList<SPEENTITY.C_21_Acc.EClassAccounts.Unit>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_21_GAcc.RptMaterialsCodeBook", lst, lst1, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));


            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
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
                    //this.ddlOthersBook.Visible = false;
                    //this.ddlOthersBookSegment.Visible = false;
                    //this.lbalterofddl.Visible = true;
                    //this.lbalterofddl0.Visible = true;
                    this.ibtnSrch.Visible = true;
                    //this.lbalterofddl.Text = this.ddlOthersBook.SelectedItem.ToString().Trim();
                    //this.lbalterofddl0.Text = "(" + this.ddlOthersBookSegment.SelectedItem.ToString().Trim() + ")";
                    //tempddl1 = ASTUtility.Left(this.ddlOthersBook.SelectedItem.ToString(), 2);



                    this.ShowInformation();
                }
                else
                {

                    this.lnkok.Text = "Ok";
                    this.txtsrch.Text = "";
                    this.LblBookName1.Text = "Select Code Book:";
                    this.ibtnSrch.Visible = false;
                    //this.lbalterofddl.Visible = false;
                    //this.lbalterofddl0.Visible = false;
                    //this.ddlOthersBook.Visible = true;
                    //this.ddlOthersBookSegment.Visible = true;
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

            string srchoptionmain = this.txtsrch.Text.Trim();
            string srchoption1 = "";
            string srchoption = (srchoptionmain.Contains("-")) ? srchoptionmain : "%" + srchoptionmain + "%";
            if (srchoption.Contains("-"))
            {
                int index = srchoption.IndexOf("-");
                srchoption = srchoptionmain.Substring(0, index);
                srchoption1 = srchoptionmain.Substring(index + 1).Replace("%", "");
            }
            
            string storid = this.ddlProject.SelectedValue.ToString();
            string tempddl1 = (this.ddlOthersBookSegment.SelectedValue.ToString().Substring(4, 8) == "00000000") ? this.ddlOthersBook.SelectedValue.ToString().Substring(0, 2) + "%" : (this.ddlOthersBookSegment.SelectedValue.ToString()).Substring(0, 7) + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_RESOURCE_CODEBOOK", tempddl1, srchoption, srchoption1, "", storid, "", "", "");
            if (ds1 == null)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }
            DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_NAME", "", "", "", "", "", "", "");
            ViewState["units"] = dsone.Tables[0];
            DataSet colorinfo = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_COLOR_CODE", "", "", "", "", "", "", "");
            ViewState["tblcolor"] = colorinfo.Tables[0];
            DataTable dt1 = ds1.Tables[0];
            //System.Data.DataColumn newColumn = new System.Data.DataColumn("qty", typeof(System.String));
            //newColumn.DefaultValue = "0.00";
            //dt1.Columns.Add(newColumn);
            ViewState["storedata"] = dt1;
            this.grvacc_DataBind();

        }
        private void GetProject()
        {

            // DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_FIXEDASSET_INFO02", "GETPROJECT", Srchname, "", "", "", "", "", "", "", "");


            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", "%15%", "FxtAst", "", userid, "", "", "", "");

            if (ds1 == null)
                return;
            ViewState["tblStoreType"] = ds1.Tables[0];
            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }
        private void Save_Value()
        {

            DataTable dt = (DataTable)ViewState["storedata"];

            DataTable dt2 = dt.Copy();
            dt2.Clear();

            for (int i = 0; i < this.grvacc.Rows.Count; i++)
            {

                string matname = ((TextBox)grvacc.Rows[i].FindControl("txtgvDesc")).Text.Trim();
                string size = ((TextBox)grvacc.Rows[i].FindControl("txtgvsize")).Text.Trim();
                string width = ((TextBox)grvacc.Rows[i].FindControl("txtgvWidth")).Text.Trim();
                string brand = ((TextBox)grvacc.Rows[i].FindControl("txtgvBrand")).Text.Trim();
                string other = ((TextBox)grvacc.Rows[i].FindControl("txtgvOther")).Text.Trim();
                string sirval = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[i].FindControl("txtgvsirval")).Text.Trim()).ToString();
                string bondname = ((TextBox)grvacc.Rows[i].FindControl("txtgvsirtdesc")).Text.Trim();
                string color = ((DropDownList)grvacc.Rows[i].FindControl("txtgvColor")).SelectedItem.ToString() == "None" ? "": ((DropDownList)grvacc.Rows[i].FindControl("txtgvColor")).SelectedItem.ToString();
                string qty = Convert.ToDouble("0" + ((TextBox)grvacc.Rows[i].FindControl("txtgvQty")).Text.Trim()).ToString();

                string sizeble = ((DropDownList)grvacc.Rows[i].FindControl("DdlSizelble")).SelectedValue.Trim();
                string sizeable2 = dt.Rows[i]["sizeble"].ToString() == "False" ? "0" : "1";

                string desc1 = dt.Rows[i]["sirdesc"].ToString().Trim() + dt.Rows[i]["size"].ToString().Trim()
                    + dt.Rows[i]["thickness"].ToString().Trim() + dt.Rows[i]["brand"].ToString().Trim()
                    + dt.Rows[i]["other"].ToString().Trim() + dt.Rows[i]["color"].ToString().Trim()
                    + Convert.ToDouble(dt.Rows[i]["sirval"]).ToString().Trim() + dt.Rows[i]["sirtdes"].ToString().Trim()
                    + Convert.ToDouble(dt.Rows[i]["qty"]) + sizeable2;

                string desc2 = matname + size + width + brand + other + color + Convert.ToDouble(sirval).ToString() + bondname + Convert.ToDouble(qty) + sizeble;

                if (
                    //dt.Rows[i]["sirdesc"].ToString().Trim() != matname || 
                    //dt.Rows[i]["size"].ToString().Trim() != size ||
                    //dt.Rows[i]["thickness"].ToString().Trim() != width ||
                    //dt.Rows[i]["brand"].ToString().Trim() != brand ||
                    //dt.Rows[i]["other"].ToString().Trim() != other ||
                    //dt.Rows[i]["color"].ToString().Trim() != color ||
                    //dt.Rows[i]["sirval"].ToString().Trim() != sirval ||
                    //dt.Rows[i]["sirtdes"].ToString().Trim() != bondname &&
                    //Convert.ToDouble(dt.Rows[i]["qty"]) != Convert.ToDouble(qty) ||
                    //sizeable2 == sizeble
                    desc1 != desc2
                )
                {
                    dt.Rows[i]["sirdesc"] = matname;
                    dt.Rows[i]["size"] = size;//spcdesc
                    dt.Rows[i]["thickness"] = width; //desc1
                    dt.Rows[i]["brand"] = brand; //desc3
                    dt.Rows[i]["other"] = other; //desc4
                    dt.Rows[i]["color"] = color; //desc2
                    dt.Rows[i]["sirval"] = sirval;
                    dt.Rows[i]["sirtdes"] = bondname;
                    dt.Rows[i]["qty"] = qty;
                    dt.Rows[i]["sizeble"] = (sizeble == "0") ? "False" : "True";

                    dt2.Rows.Add(dt.Rows[i].ItemArray);
                }

            }
            ViewState["storedata"] = dt2;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {
                this.Save_Value();
                DataTable dt = (DataTable)ViewState["storedata"];
                string comcod = this.GetComeCode();
                string storid = this.ddlProject.SelectedValue.ToString();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();

                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "tblinfo";
                var dta = ds.GetXml();

                //var dsResult = this.da.GetTransInfoNew(comcod, "SP_ENTRY_CODEBOOK02", "UPDATE_MATERIALS_CODE_BOOK02", ds, null, null, storid, userid);

                bool result = this.da.UpdateXmlTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "UPDATE_MATERIALS_CODE_BOOK02", ds, null, null, storid, userid);
                this.ShowInformation();
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Invalid!!!');", true);
                }
                this.grvacc.EditIndex = -1;
                this.grvacc_DataBind();


                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Account Sub-CodeBook";
                    string eventdesc = "Update Sub-CodeBook with Stock";
                    string eventdesc2 = "Update from Materials Code with Opening Stock";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            }
        }

        protected void grvacc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataTable tblcolor = (DataTable)ViewState["tblcolor"];
                DropDownList color = (DropDownList)e.Row.FindControl("txtgvColor");
                color.DataTextField = "gdesc";
                color.DataValueField = "gcod";
                color.DataSource = tblcolor;
                color.DataBind();
                string colorname = ((DataTable)ViewState["storedata"]).Rows[e.Row.RowIndex]["colorid"].ToString();
                color.SelectedValue = colorname;

                DropDownList sizeble = (DropDownList)e.Row.FindControl("DdlSizelble");
                sizeble.SelectedValue = (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "sizeble")) == true) ? "1" : "0";
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

                    dt.Rows[i]["sirunit"] = "";
                    dt.Rows[i]["sirval"] = 0.00;
                    dt.Rows[i]["sirtdes"] = "";
                    //dt.Rows[i]["sircode2"] = "";
                    //dt.Rows[i]["sircode4"] = "";

                }
                sircode = dt.Rows[i]["sircode"].ToString();
            }
            return dt;
        }



        protected void ddlOthersBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mathead = this.ddlOthersBook.SelectedValue.ToString().Substring(0, 4) + "%";
            DataTable dt = (DataTable)ViewState["tblmatsubhead"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode1 like '" + mathead + "'";
            this.ddlOthersBookSegment.DataTextField = "sircode";
            this.ddlOthersBookSegment.DataValueField = "sircode1";
            this.ddlOthersBookSegment.DataSource = dv.ToTable();
            this.ddlOthersBookSegment.DataBind();
        }




    }
}