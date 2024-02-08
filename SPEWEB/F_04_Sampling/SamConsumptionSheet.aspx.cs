using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY;
using SPEENTITY.C_22_Sal;
using AjaxControlToolkit;
using System.IO;

namespace SPEWEB.F_04_Sampling
{
    public partial class SamConsumptionSheet : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Merdata = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        Common CommonClass = new Common();
        UserManagerSampling objUserMan = new UserManagerSampling();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "PGApp") ? "Modified PD Guide" : (type == "PGEntry") ? "PD Guide Entry" : (type == "PreCosting") ? "CBD Sheet" : "Consumption Sheet";
                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                CommonButton();
                this.GetInqnumber();
                this.getCurList();
                this.GetGroup();
                this.GetProcess();
                GetComponentGrp();
                //this.GetProMaterial();
                Select_View();

                string qgenno = this.Request.QueryString["genno"] ?? "";

                if (qgenno.Length > 0)
                {

                    this.lbtnOk_Click(null, null);
                }

                if (type == "Entry" || type == "ConApp" || type == "PGEntry" || type == "PGApp")
                {
                    //this.DirectCost.Visible = false;
                    //((Label)this.gvCost.FooterRow.FindControl("lbltoalf")).Visible = false;
                    ChckCopy.Visible = true;
                    this.panelCosting.Visible = false;
                    this.txtNotes.Visible = true;
                }
                else if (type == "PreCosting")
                {
                    this.DirectCost.Visible = true;
                    this.NeedImportCheck.Visible = false;
                    this.lnkAddResouctCost.Visible = false;
                    this.lnkComponent.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Complete";
                    this.Resourcepanel.Visible = false;
                    this.RadioButtonList1.Visible = true;
                }
                else if (type == "PreCostingApp")
                {
                    this.DirectCost.Visible = true;
                    this.NeedImportCheck.Visible = false;
                    this.lnkAddResouctCost.Visible = false;
                    this.lnkComponent.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
                    this.Resourcepanel.Visible = false;
                    this.RadioButtonList1.Visible = true;

                }
                else
                {
                    //this.DirectCost.Visible = true;
                    this.panelCosting.Visible = true;
                }
            }

            if (fileuploaddropzone.HasFile)
            {
                List<string> acceptimg = new List<string>() { ".jpeg", ".jpg", ".png", ".gif" };
                string extension = Path.GetExtension(fileuploaddropzone.PostedFile.FileName);
                if (!acceptimg.Contains(extension.ToLower()))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Failed to upload. Only image file is acceptable.');", true);
                    return;
                }
                string comcod = this.GetCompCode();
                var inqinfo = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)ViewState["tblinquery"];
                string Url = "";
                string inqno = this.Request.QueryString["genno"].ToString();

                string random = ASTUtility.RandNumber(1, 99999).ToString();
                fileuploaddropzone.SaveAs(Server.MapPath("~/Upload/SAMPLE/") + random + extension);
                Url = "~/Upload/SAMPLE/" + random + extension;
                this.Uploadedimg.ImageUrl = Url;
                this.Uploadedimg2.ImageUrl = Url;

                inqinfo[0].images = Url;
                ViewState["tblinquery"] = inqinfo;
                bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "UPDATEPROTOSAMPLEIMAGES", inqno, Url);
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                }
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(Con_Cost_Approved);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkbtnHisprice_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click);
        }
        private void Select_View()
        {
            this.ShowConsump();
            this.ShowConsumpsize();
            this.ProcessPanel.Visible = true;
            MultiView1.ActiveViewIndex = 0;
        }
        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            if (this.Request.QueryString["Type"] == "ConApp" || this.Request.QueryString["Type"] == "PGApp" || this.Request.QueryString["Type"] == "PreCostingApp")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approve";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do You want to Approve?')";
            }
            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;

            }
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Delete Selected Item";
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).OnClientClick = "return confirm('Do you want to Remove Selected Item?')";
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).CssClass = "btn btn-info btn-sm";

            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            if (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp" || this.Request.QueryString["Type"].ToString() == "PGEntry")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Forward";
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;
            }


            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void GetProcess()
        {
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString();
            string filter = "%";
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GETPROCESSCODE", filter, "", "", "", "", "", "");
            this.ddlmatdept.DataSource = ds3.Tables[0];
            this.ddlmatdept.DataTextField = "resdesc";
            this.ddlmatdept.DataValueField = "rescode";
            this.ddlmatdept.DataBind();

            //this.imgbtnResourceCost_Click(null, null);
        }

        private void GetInqnumber()
        {
            string comcod = this.GetCompCode();
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string qinqno = this.Request.QueryString["genno"].ToString();
            string srchinqno = (qinqno.Length > 0 ? qinqno : "") + "%";
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETSAMPLEINQNO", todate, srchinqno);
            this.ddlinqno.DataTextField = "inqno1";
            this.ddlinqno.DataValueField = "sampleid";
            this.ddlinqno.DataSource = ds1.Tables[0];
            this.ddlinqno.DataBind();
            ds1.Dispose();
            this.GetInqueryInfo();
        }
        protected void lnkAddSmpSiz_Click(object sender, EventArgs e)
        {

            DataTable tbl2 = (DataTable)ViewState["tblSmpleSizes"];

            this.AddSmpSizSaveValue();

            DataRow dr1 = tbl2.NewRow();
            dr1["typedesc"] = "";
            dr1["s1"] = "";
            dr1["s2"] = "";
            dr1["s3"] = "";
            dr1["s4"] = "";
            dr1["s5"] = "";
            dr1["s6"] = "";
            dr1["s7"] = "";
            dr1["s8"] = "";
            dr1["s9"] = "";
            dr1["s10"] = "";

            tbl2.Rows.Add(dr1);

            ViewState["tblSmpleSizes"] = tbl2;
            this.SmpSize();
        }
        protected void SmpSize()
        {

            DataTable tbl3 = (DataTable)ViewState["tblSmpleSizes"];
            if (tbl3.Rows.Count == 0)
                return;

            this.grvSmpleSizes.ShowHeader = false;
            this.grvSmpleSizes.ShowFooter = false;
            this.grvSmpleSizes.DataSource = tbl3;
            this.grvSmpleSizes.DataBind();

            grvSmpleSizes.Rows[0].FindControl("lnkAddSmpSiz").Visible = true;



        }
        private void ShowConsumpsize()
        {
            ViewState.Remove("tblSmpleSizes");
            string comcod = this.GetCompCode();
            string sampleid = this.ddlinqno.SelectedValue.ToString();


            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_SAM_CONSUMPTION_INFO", sampleid, "", "", "", "");
            if (ds4 == null)
            {
                return;
            }
            ViewState["tblSmpleSizes"] = ds4.Tables[0];

            this.SmpSize();

        }
        protected void AddSmpSizSaveValue()
        {
            DataTable tbl2 = (DataTable)ViewState["tblSmpleSizes"];
            string comcod = this.GetCompCode();
            foreach (GridViewRow row in grvSmpleSizes.Rows)
            {

                tbl2.Rows[row.RowIndex]["typedesc"] = ((TextBox)row.FindControl("lblgvtypedesc")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s1"] = ((TextBox)row.FindControl("lblgvs1")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s2"] = ((TextBox)row.FindControl("lblgvs2")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s3"] = ((TextBox)row.FindControl("lblgvs3")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s4"] = ((TextBox)row.FindControl("lblgvs4")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s5"] = ((TextBox)row.FindControl("lblgvs5")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s6"] = ((TextBox)row.FindControl("lblgvs6")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s7"] = ((TextBox)row.FindControl("lblgvs7")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s8"] = ((TextBox)row.FindControl("lblgvs8")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s9"] = ((TextBox)row.FindControl("lblgvs9")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s10"] = ((TextBox)row.FindControl("lblgvs10")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s11"] = ((TextBox)row.FindControl("lblgvs11")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s12"] = ((TextBox)row.FindControl("lblgvs12")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s13"] = ((TextBox)row.FindControl("lblgvs13")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s14"] = ((TextBox)row.FindControl("lblgvs14")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s15"] = ((TextBox)row.FindControl("lblgvs15")).Text.ToString();

            }
            ViewState["tblSmpleSizes"] = tbl2;
            this.SmpSize();
        }
        private void GetInqueryInfo()
        {
            string comcod = this.GetCompCode();
            string inqno = this.ddlinqno.SelectedValue.ToString();
            ViewState.Remove("tblinquery");
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETSAMPLEINQINFO", inqno, "",
                          "", "", "", "", "", "", "");
            ViewState["tblinquery"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>();
            ds1.Dispose();


            var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)(ViewState["tblinquery"]);
            ViewState["tblproinquery"] = ds1.Tables[1].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>();
            this.txtSampType.Text = lst[0].samtypedesc;
            this.TxtBuyer.Text = lst[0].buyerdesc;
            this.Txtbrand.Text = lst[0].branddesc;
            this.txtConstruction.Text = lst[0].construction;
            this.txtlformaname.Text = lst[0].lformadesc;
            this.txtArtno.Text = lst[0].article.Trim();
            this.txtCategory.Text = lst[0].catagorydesc;
            this.txtshoetype.Text = lst[0].shoetdesc;
            this.txtSeason.Text = lst[0].seasondesc;
            this.txtagent.Text = lst[0].agent;
            this.txtsamplesize.Text = Convert.ToString(lst[0].samsize);
            this.txtsconsize.Text = Convert.ToString(lst[0].comsize);
            this.txtsizernge.Text = lst[0].sizerange;
            this.txtsamqty.Text = Convert.ToDouble(lst[0].samqty).ToString("#,##0;(#,##0); ");
            this.txtRemarks.Text = lst[0].remarks;
            this.Uploadedimg.ImageUrl = (lst[0].images.ToString() == "") ? "~/images/no_img_preview.png" : lst[0].images.ToString();
            this.Uploadedimg2.ImageUrl = (lst[0].images.ToString() == "") ? "~/images/no_img_preview.png" : lst[0].images.ToString();
            this.TxtDelDate.Text = Convert.ToDateTime(lst[0].deldate).ToString("dd-MMM-yyyy");
            this.TxtUnit.Text = lst[0].unitdesc;
            this.TxtColor.Text = lst[0].colordesc;

            this.txtArtclNo.Text = lst[0].article.Trim();
            this.txtByrNm.Text = lst[0].buyerdesc;
            this.txtClr.Text = lst[0].colordesc;

            //this.gvCost.BackImageUrl = lst[0].images.ToString() == "" ? "~/images/no_img_preview.png" : lst[0].images.ToString();

            //  this.GetMaterial();
        }

        private void GetMaterial()
        {
            //var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)(ViewState["tblinquery"]);

            //string mattype = this.ddlMaterial.SelectedValue.ToString();
            //string typename = this.ddlMaterial.SelectedItem.Text;
            //this.lbldddlmaterial.Text = typename;
            //this.txtMaterial.Text = (mattype == "UP") ? lst[0].upmaterial
            //                        : (mattype == "LI") ? lst[0].limaterial 
            //                        : (mattype == "SK") ? lst[0].skmaterial 
            //                        : (mattype == "OS") ? lst[0].osmaterial 
            //                        : (mattype == "AC") ? lst[0].accessories
            //                        : "";
        }

        protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterial();

        }

        protected void ddlinqno_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetInqueryInfo();
        }
        private void getCurList()
        {

            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;
            this.ddlCurList.DataValueField = "curcode";
            this.ddlCurList.DataTextField = "curdesc";
            this.ddlCurList.DataSource = lstCurryDesc;
            this.ddlCurList.DataBind();
            ddlCurrency_SelectedIndexChanged(null, null);

            lstCurryDesc.Add(new SPEENTITY.C_22_Sal.Sales_BO.Currencyinf("000", "--Select--", true));
            this.DdlCurList1.DataValueField = "curcode";
            this.DdlCurList1.DataTextField = "curdesc";
            this.DdlCurList1.DataSource = lstCurryDesc;
            this.DdlCurList1.DataBind();

            DdlCurList1_SelectedIndexChanged(null, null);
            this.DdlCurList2.DataValueField = "curcode";
            this.DdlCurList2.DataTextField = "curdesc";
            this.DdlCurList2.DataSource = lstCurryDesc;
            this.DdlCurList2.DataBind();


            DdlCurList2_SelectedIndexChanged(null, null);
        }
        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tcode = "001";
            string fcode = this.ddlCurList.SelectedValue.ToString();
            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            var List = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode)).ToList();

            double method = (List.Count > 0) ? List[0].conrate : 0;

            this.txtExchngerate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000;-#,##0.000000;");
            double target = Convert.ToDouble("0" + this.txttarprice.Text.Trim());
            double offer = Convert.ToDouble("0" + this.txtoffprice.Text.Trim());
            double confirm = Convert.ToDouble("0" + this.txtconfrmprice.Text.Trim());
            this.txttarprice.Text = Convert.ToDouble("0" + target).ToString("#,##0.000000 ;-#,##0.000000; ");
            this.txtoffprice.Text = Convert.ToDouble("0" + offer).ToString("#,##0.000000 ;-#,##0.000000; ");
            // this.txtconfrmprice.Text = Convert.ToDouble("0" + confirm).ToString("#,##0.000000 ;-#,##0.000000; ");
            //double txtpeople = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBudgeted.Rows[j].FindControl("txtpeople")).Text.Trim()));

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private void lnkbtnHisprice_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string comcod = this.GetCompCode();

            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                if (((CheckBox)this.gvCost.Rows[i].FindControl("chkCol")).Checked)
                {
                    string sampleid = this.ddlinqno.SelectedValue.ToString();
                    string grp = dt.Rows[i]["grp"].ToString();
                    string rescode = dt.Rows[i]["rescode"].ToString();
                    string spcfcode = dt.Rows[i]["spcfcode"].ToString();
                    string inqno = this.Request.QueryString["genno"].ToString();
                    string compgrp = dt.Rows[i]["compcode"].ToString();

                    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                    if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                        return;
                    }
                    bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "DELETECONQUANTITY", sampleid, grp, rescode, spcfcode, inqno, compgrp);
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Merdata.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                    else
                    {
                        dt.Rows[i].Delete();
                        DataView dv = dt.DefaultView;
                        ViewState["tblstdcost"] = dv.ToTable();
                    }
                }
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Successfully');", true);
            this.ShowConsump();
            this.Data_Bind();
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            //this.BindCalcultiveInfo();
            this.Data_Bind();
            string type = Request.QueryString["Type"];
            if (type == "PreCosting" || type == "PreCostingApp")
                BindCalcultiveInfo();
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            this.ChckVIew.Checked = true;
            this.ChckVIew_CheckedChanged(null, null);

            string type = this.Request.QueryString["Type"].ToString();
            if (type == "PreCosting")
            {
                switch (GetCompCode())
                {
                    case "5101":
                    case "5306":
                    case "5305":
                        this.PrintPreCostingSheet();
                        break;
                    default:
                        this.PrintConsumptionSheet();
                        break;
                }
            }
            if (type == "PGEntry" || type == "ConApp" || type == "Entry" || type == "PGApp")
            {
                this.PrintConsumptionSheet();
            }
            else
            {
                this.PrintPreCostingSheet();
            }
        }
        private void PrintPreCostingSheet()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)(ViewState["tblinquery"]);
            //List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = lst.FindAll(s => s.styleid == styleid);
            string url = list[0].images.ToString();
            string image = "";

            if (url.Length > 0)
            {
                image = new Uri(Server.MapPath(url)).AbsoluteUri;
            }
            else
            {
                image = new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
            }


            string artno = this.txtArtno.Text.Trim().ToString();
            string color = this.TxtColor.Text.Trim().ToString();
            string range = this.txtsizernge.Text.Trim().ToString();
            string size = this.txtsamplesize.Text.Trim().ToString();
            string qty = "";
            string date = this.txtDatefrom.Text.Trim().ToString();
            string buyer = this.TxtBuyer.Text.Trim().ToString();
            string mrsendizer = "";
            string catgory = this.txtCategory.Text.Trim().ToString();
            string lstref = "";
            string construction = this.txtConstruction.Text.Trim().ToString();
            string smploptionno = "";
            string season = "";
            string brndname = "";
            string estdate = "";
            string costrang = this.txtsconsize.Text.Trim().ToString();
            string ordrqty = "";
            string technician = "";
            string lastmold = this.txtlformaname.Text.Trim().ToString();
            string style = this.txtCategory.Text.Trim().ToString();
            string exrate = this.txtExchngerate.Text.Trim().ToString();
            string tarrate = this.txttarprice.Text.Trim().ToString();
            string offer = this.txtoffprice.Text.Trim().ToString();
            string confirm = this.txtconfrmprice.Text.Trim().ToString();
            string currency = this.ddlCurList.SelectedItem.Text.Trim().ToString();

            DataTable dt = (DataTable)ViewState["tblstdcost"];

            DataTable dtcommon = (DataTable)ViewState["tblcomncost"];
            DataTable curinfo = (DataTable)ViewState["tblcurinfo"];
            DataView dv = dtcommon.Copy().DefaultView;
            dv.RowFilter = "sircode not like '%999'";

            //string date = (ds4.Tables[0].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds4.Tables[0].Rows[0]["csumpdat"]).ToString("dd-MMM-yyyy");

            // DataTable tbl3 = (DataTable)ViewState["tblSmpleSizes"];
            if (dt == null)
            {
                return;
            }
            var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionFB>();
            var lst1 = dtcommon.DataTableToList<SPEENTITY.C_01_Mer.EclassCommonCostSam>();
            var lst2 = curinfo.DataTableToList<SPEENTITY.C_01_Mer.EclassCurinfo>();
            //double totlcst = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", "")));
            //double tltcommncost = Convert.ToDouble((Convert.IsDBNull(dtcommon.Compute("sum(stdamt)", "")) ? 0.00 : dtcommon.Compute("sum(stdamt)", "")));
            double totlcst = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", "")));
            double tltcommncost = Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("sum(stdamt)", "")) ? 0.00 : dv.ToTable().Compute("sum(stdamt)", "")));
            string totalamt = (totlcst + tltcommncost).ToString("#,##0.0000;(#,##0.0000);");
            string exchnga = lst2.Select(x => x.exratea).SingleOrDefault().ToString();
            string exchngb = lst2.Select(a => a.exrateb).SingleOrDefault().ToString();
            string conrate = lst2.Select(a => a.conrate).SingleOrDefault().ToString();
            string curdesc = lst2.Select(a => a.curdesca).SingleOrDefault().ToString();
            string sdino = dt.Rows[0]["inqno1"].ToString();
            var lstother = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)(ViewState["tblinquery"]);
            string samptype = lstother[0].samtypedesc;
            season = lstother[0].seasondesc;
            string forma = lstother[0].lformadesc;
            LocalReport rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5101":
                case "5306":
                case "5305":
                    rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptPreSampCostingSheetFB", lst, lst1, lst2);
                    rpt1.EnableExternalImages = true;

                    rpt1.SetParameters(new ReportParameter("PlAnalysis", (Convert.ToDouble(lst[0].offprice) - Convert.ToDouble(Convert.ToDouble(totalamt) * Convert.ToDouble(exchnga))).ToString("#,##0.0000;-#,##0.00 ")));
                    rpt1.SetParameters(new ReportParameter("con2rate", (Convert.ToDouble(exchngb)).ToString("#,##0.0000;(#,##0.0000); ")));
                    rpt1.SetParameters(new ReportParameter("totalamt", (Convert.ToDouble(totalamt)).ToString("#,##0.0000;(#,##0.0000); ")));
                    rpt1.SetParameters(new ReportParameter("conrate", conrate));
                    rpt1.SetParameters(new ReportParameter("curdesc", curdesc));
                    rpt1.SetParameters(new ReportParameter("sdino", sdino));
                    //rpt1.SetParameters(new ReportParameter("confirm", confirm));
                    rpt1.SetParameters(new ReportParameter("con1rate", (Convert.ToDouble(exchnga)).ToString("#,##0.0000;(#,##0.0000); ")));
                    break;
                default:
                    rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptPreSampCostingSheet", lst, null, null);
                    break;
            }


            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("artno", artno));
            rpt1.SetParameters(new ReportParameter("color", color));
            rpt1.SetParameters(new ReportParameter("range", range));
            rpt1.SetParameters(new ReportParameter("size", size));
            rpt1.SetParameters(new ReportParameter("qty", qty));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("buyer", buyer));
            rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
            rpt1.SetParameters(new ReportParameter("catgory", catgory));
            rpt1.SetParameters(new ReportParameter("lstref", lstref));
            rpt1.SetParameters(new ReportParameter("construction", construction));
            rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
            rpt1.SetParameters(new ReportParameter("season", season));
            rpt1.SetParameters(new ReportParameter("brndname", brndname));
            rpt1.SetParameters(new ReportParameter("image", image));
            rpt1.SetParameters(new ReportParameter("estdate", estdate));
            rpt1.SetParameters(new ReportParameter("costrang", costrang));
            rpt1.SetParameters(new ReportParameter("ordrqty", ordrqty));
            rpt1.SetParameters(new ReportParameter("technician", technician));
            rpt1.SetParameters(new ReportParameter("lastmold", lastmold));
            rpt1.SetParameters(new ReportParameter("style", style));

            rpt1.SetParameters(new ReportParameter("offprice", lst[0].offprice.ToString("#,##0.00; (#,##0.00); ")));
            rpt1.SetParameters(new ReportParameter("tarprice", lst[0].tarprice.ToString("#,##0.00; (#,##0.00); ")));
            rpt1.SetParameters(new ReportParameter("cnfrmprice", Convert.ToDouble(lst1.Where(x => x.sircode == "049800102999").Select(c => c.stdamt).FirstOrDefault()).ToString("#,##0.00; (#,##0.00); ")));
            rpt1.SetParameters(new ReportParameter("con1", (Convert.ToDouble(totalamt) * Convert.ToDouble(exchnga)).ToString("#,##0.0000;(#,##0.0000); ")));
            rpt1.SetParameters(new ReportParameter("con2", (Convert.ToDouble(totalamt) * Convert.ToDouble(exchngb)).ToString("#,##0.0000;(#,##0.0000); ")));
            rpt1.SetParameters(new ReportParameter("samptype", samptype));

            rpt1.SetParameters(new ReportParameter("offerPrice", txtoffpercnt.Text));
            rpt1.SetParameters(new ReportParameter("pLAnalysis1", string.IsNullOrWhiteSpace(TextBox3.Text) ? " " : TextBox3.Text));
            rpt1.SetParameters(new ReportParameter("cusTarget", txtTarpercnt.Text));

            rpt1.SetParameters(new ReportParameter("forma", forma));
            rpt1.SetParameters(new ReportParameter("RptTitle", "CBD Sheet"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintConsumptionSheet()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)(ViewState["tblinquery"]);
            DataTable dt1 = (DataTable)(ViewState["tblcurinfo"]);
            //List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = lst.FindAll(s => s.styleid == styleid);
            string url = list[0].images.ToString();
            string image = "";

            if (url.Length > 0)
            {
                image = new Uri(Server.MapPath(url)).AbsoluteUri;
            }
            else
            {
                image = new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
            }


            string artno = this.txtArtno.Text.Trim().ToString();
            string color = this.TxtColor.Text.Trim().ToString();
            string range = this.txtsizernge.Text.Trim().ToString();
            string size = this.txtsamplesize.Text.Trim().ToString();
            string qty = "";
            string date = this.txtDatefrom.Text.Trim().ToString();
            string buyer = this.TxtBuyer.Text.Trim().ToString();
            string brand = this.Txtbrand.Text.Trim().ToString();
            string samqty = this.txtsamqty.Text.Trim().ToString();
            string mrsendizer = "";
            string catgory = this.txtCategory.Text.Trim().ToString();
            string lstref = "";
            string construction = "";
            string smploptionno = "";
            string season = "";
            string brndname = "";
            string estdate = "";
            string costrang = this.txtsconsize.Text.Trim().ToString();
            string ordrqty = "";
            string technician = "";
            string lastmold = this.txtlformaname.Text.Trim().ToString();
            string style = this.txtCategory.Text.Trim().ToString();
            string exrate = this.txtExchngerate.Text.Trim().ToString();
            string tarrate = this.txttarprice.Text.Trim().ToString();
            string offer = this.txtoffprice.Text.Trim().ToString();
            string confirm = this.txtconfrmprice.Text.Trim().ToString();
            string currency = this.ddlCurList.SelectedItem.Text.Trim().ToString();


            DataTable dt = (DataTable)ViewState["tblstdcost"];
            DataTable dt5 = (DataTable)ViewState["tblSmpleSizes"];

            DataTable dtcommon = (DataTable)ViewState["CommonCost"];

            // DataTable tbl3 = (DataTable)ViewState["tblSmpleSizes"];
            if (dt == null)
            {
                return;
            }



            var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionFB>();
            var samsize = dt5.DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>();
            //var lst1 = dtcommon.DataTableToList<SPEENTITY.C_01_Mer.EclassCommonCost>();

            LocalReport rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5101":
                case "5306":
                case "5305":
                    rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptConsumptionSheetFB", lst, samsize, null);
                    rpt1.EnableExternalImages = true;
                    rpt1.SetParameters(new ReportParameter("brand", brand));
                    rpt1.SetParameters(new ReportParameter("samqty", samqty));
                    rpt1.SetParameters(new ReportParameter("artno", artno));
                    rpt1.SetParameters(new ReportParameter("createdby", dt1.Rows[0]["createdby"].ToString()));
                    rpt1.SetParameters(new ReportParameter("createdbydesig", dt1.Rows[0]["createdbydesig"].ToString()));
                    rpt1.SetParameters(new ReportParameter("createdbydept", dt1.Rows[0]["createdbydept"].ToString()));

                    rpt1.SetParameters(new ReportParameter("pdappby", dt1.Rows[0]["pdappby"].ToString()));
                    rpt1.SetParameters(new ReportParameter("pdappbydesig", dt1.Rows[0]["pdappbydesig"].ToString()));
                    rpt1.SetParameters(new ReportParameter("pdappbydept", dt1.Rows[0]["pdappbydept"].ToString()));

                    rpt1.SetParameters(new ReportParameter("pdmdappby", dt1.Rows[0]["pdmdappby"].ToString()));
                    rpt1.SetParameters(new ReportParameter("pdmdappbydesig", dt1.Rows[0]["pdmdappbydesig"].ToString()));
                    rpt1.SetParameters(new ReportParameter("pdmdappbydept", dt1.Rows[0]["pdmdappbydept"].ToString()));
                    break;
                default:
                    rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptConsumptionSheet", lst, null, null);
                    break;
            }


            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("artno", artno));
            rpt1.SetParameters(new ReportParameter("color", color));
            rpt1.SetParameters(new ReportParameter("range", range));
            rpt1.SetParameters(new ReportParameter("size", size));
            rpt1.SetParameters(new ReportParameter("qty", qty));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("buyer", buyer));
            rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
            rpt1.SetParameters(new ReportParameter("catgory", catgory));
            rpt1.SetParameters(new ReportParameter("lstref", lstref));
            rpt1.SetParameters(new ReportParameter("construction", construction));
            rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
            rpt1.SetParameters(new ReportParameter("season", season));
            rpt1.SetParameters(new ReportParameter("brndname", brndname));
            rpt1.SetParameters(new ReportParameter("image", image));
            rpt1.SetParameters(new ReportParameter("estdate", estdate));
            rpt1.SetParameters(new ReportParameter("costrang", costrang));
            rpt1.SetParameters(new ReportParameter("ordrqty", ordrqty));
            rpt1.SetParameters(new ReportParameter("technician", technician));
            rpt1.SetParameters(new ReportParameter("lastmold", lastmold));
            rpt1.SetParameters(new ReportParameter("style", style));
            rpt1.SetParameters(new ReportParameter("notes", dt1.Rows[0]["notes"].ToString()));

            rpt1.SetParameters(new ReportParameter("samptype", dt1.Rows[0]["samptypedesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("pddate", Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy")));

            rpt1.SetParameters(new ReportParameter("RptTitle", "PD Guide"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            //Page.Validate();
            //if (!Page.IsValid)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Validation Error!!!";
            //    return;
            //}
            this.UpdateCost();
            //this.lnkbtnAdd_Click(null, null);
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlinqno.Enabled = false;

                this.pnlArticle.Visible = true;
                this.pnlBuyer.Visible = true;
                this.pnlColor.Visible = true;

                return;

            }

            this.pnlArticle.Visible = false;
            this.pnlBuyer.Visible = false;
            this.pnlColor.Visible = false;

            this.lbtnOk.Text = "Ok";
            this.ddlinqno.Enabled = true;
        }
        private void Get_ColorSize()
        {
            //string comcod = this.GetCompCode();
            //string styleid = this.ddlStyle.SelectedValue.ToString();
            //string inqno = this.ddlinqno.SelectedValue.ToString();
            //DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_ACTUAL_COLOR_SIZE", inqno, styleid, "", "", "", "", "", "", "");
            //List<SPEENTITY.C_01_Mer.EclassOrderDetails> color = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            //color.Add(new SPEENTITY.C_01_Mer.EclassOrderDetails("000000000000", "Common", "Y", "", "", ""));
            //List<SPEENTITY.C_01_Mer.EclassOrderDetails> consize = ds1.Tables[3].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            //consize.Add(new SPEENTITY.C_01_Mer.EclassOrderDetails("", "", "", "000000000000", "Common", "Y"));
            //ViewState["tblcolor"] = color;
            //ViewState["tblconsize"] = consize;
            //this.Bind_color_size();
        }


        private void GetGroup()
        {


            ViewState.Remove("lstgroup");
            string comcod = this.GetCompCode();
            var lst = objUserMan.GetGroup(comcod);
            ViewState["lstgroup"] = lst;
            this.ddlGrp.DataSource = lst;
            this.ddlGrp.DataTextField = "grpdesc";
            this.ddlGrp.DataValueField = "grp";
            this.ddlGrp.DataBind();




        }

        //protected void GetProcess()
        //{

        //    string comcod = this.GetCompCode();
        //    string inqno = this.Request.QueryString["genno"].ToString();

        //    string type = this.Request.QueryString["Type"].ToString();
        //    if (type == "PreCosting" || type == "PreCostingApp")
        //    {
        //        type = "Cost";
        //    }
        //    string filter = "%";
        //    DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETPROCESSCODE", filter, inqno, type, "", "", "", "", "");
        //    this.ddlProcess.DataSource = ds3.Tables[0];
        //    this.ddlProcess.DataTextField = "resdesc";
        //    this.ddlProcess.DataValueField = "rescode";
        //    this.ddlProcess.DataBind();
        //    ViewState["tblcodeType"] = ds3.Tables[0];
        //    ds3.Dispose();
        //}

        private void GetProMaterial()
        {
            string comcod = this.GetCompCode();
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ONLY_MATERIAL_LIST ", "04%", "", "", "", "", "", "", "", "");

            DataView view = new DataView(ds3.Tables[0]);
            DataTable mattable = view.ToTable(true, "sircode", "sirdesc");
            this.ddlResourcesCost.DataSource = mattable;
            this.ddlResourcesCost.DataTextField = "sirdesc";
            this.ddlResourcesCost.DataValueField = "sircode";
            this.ddlResourcesCost.DataBind();

            //   ViewState["tblSpcf"] = ds3.Tables[1];
            ds3.Dispose();
            this.ddlResourcesCost_SelectedIndexChanged(null, null);


        }


        protected void imgbtnResourceCost_Click(object sender, EventArgs e)
        {
            GetProMaterial();

        }
        protected void ddlResourcesCost_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mResCode = this.ddlResourcesCost.SelectedValue.ToString() + "%";
            this.DdlSpecres.Items.Clear();
            string comcod = this.GetCompCode();
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_FOR_CONSUMPTION ", mResCode, "", "", "", "", "", "", "", "");

            DataView dv1 = ds3.Tables[0].DefaultView;

            DataTable dt = dv1.ToTable(true, "spcfcod", "spcfdesc");
            dt.Rows.Add("000000000000", "None");
            //if (dt.Rows.Count > 1)
            //{
            //    dt.Rows[0].Delete();
            //}


            ViewState["tblresRes"] = ds3.Tables[0];

            this.DdlSpecres.DataTextField = "spcfdesc";
            this.DdlSpecres.DataValueField = "spcfcod";
            this.DdlSpecres.DataSource = dt;
            this.DdlSpecres.DataBind();
            this.DdlSpecres.SelectedValue = "000000000000";
        }

        private void GetComponentList()
        {
            string comcod = this.GetCompCode();
            string compgrp = this.DDLCompGroup.SelectedValue.ToString();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETCOMPONENTLIST", "", "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;
            DataView dv = ds2.Tables[0].DefaultView;
            dv.RowFilter = "rescode like '" + compgrp.Substring(0, 7) + "%'";
            ddlComponent.DataTextField = "resdesc";
            ddlComponent.DataValueField = "rescode";
            ddlComponent.DataSource = dv.ToTable();
            ddlComponent.DataBind();
            //   string curencyName = this.ddlCurList.SelectedItem.ToString();
            // this.gvCost.Columns[10].HeaderText = "Std. Rate (" + curencyName + ")";
        }
        private void GetComponentGrp()
        {
            string comcod = this.GetCompCode();

            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETCOMPONENTGRP", "", "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            DDLCompGroup.DataTextField = "resdesc";
            DDLCompGroup.DataValueField = "rescode";
            DDLCompGroup.DataSource = ds2.Tables[0];
            DDLCompGroup.DataBind();
            this.DDLCompGroup_SelectedIndexChanged(null, null);
            //string curencyName = this.ddlCurList.SelectedItem.ToString();
            //this.gvCost.Columns[10].HeaderText = "Std. Rate (" + curencyName + ")";
        }
        private void ShowConsump()
        {
            ViewState.Remove("tblstdcost");
            string comcod = this.GetCompCode();
            string inqno = this.ddlinqno.SelectedValue.ToString();
            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETCONSUMPTIONINFO", inqno, "", "", "", "", "", "");
            if (ds4 == null)
            {
                //this.gvRtpcon.DataSource = null;
                //this.gvRtpcon.DataBind();
                return;
            }
            this.txtDatefrom.Text = (ds4.Tables[0].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds4.Tables[0].Rows[0]["csumpdat"]).ToString("dd-MMM-yyyy");

            ViewState["tblstdcost"] = ds4.Tables[0];
            ViewState["tblcurinfo"] = ds4.Tables[1];
            ViewState["tblcomncost"] = ds4.Tables[2];

            if (ds4.Tables[0].Rows.Count == 0)
            {
                this.GetMatfromInquery();
            }

            // ViewState["tblSmpleSizes"] = ds4.Tables[1];
            if (ds4.Tables[0].Rows.Count > 0)
            {
                this.txttarprice.Text = Convert.ToDouble(ds4.Tables[0].Rows[0]["tarprice"]).ToString("#,##0.00;(#,##0.00); ");
                this.txtoffprice.Text = Convert.ToDouble(ds4.Tables[0].Rows[0]["offprice"]).ToString("#,##0.00;(#,##0.00); ");

            }
            if (ds4.Tables[1].Rows.Count > 0)
            {
                this.txtconfrmprice.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["cnfrmprice"]).ToString("#,##0.000 ;-#,##0.000;");
                this.txtNotes.Text = ds4.Tables[1].Rows[0]["notes"].ToString();

            }
            BindCalcultiveInfo();
            this.Data_Bind();
            ds4.Dispose();
        }
        public void BindCalcultiveInfo()
        {
            DataTable curinf = (DataTable)ViewState["tblcurinfo"];
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            DataTable commcost = (DataTable)ViewState["tblcomncost"];
            DataView dv = commcost.Copy().DefaultView;
            dv.RowFilter = "sircode not like '%999'";
            double totlcst = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", "")));
            double tltcommncost = Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("sum(stdamt)", "")) ? 0.00 : dv.ToTable().Compute("sum(stdamt)", "")));
            this.txtEstimated.Text = (totlcst + tltcommncost).ToString("#,##0.0000; ");

            if (curinf.Rows.Count > 0)
            {

                string usd = this.ddlCurList.SelectedValue.ToString();
                this.ddlCurList.SelectedValue = curinf.Rows[0]["curcod"].ToString();
                this.txtExchngerate.Text = Convert.ToDouble(curinf.Rows[0]["conrate"]).ToString("#,##0.000000 ;-#,##0.000000;");
                this.DdlCurList1.SelectedValue = curinf.Rows[0]["curcoda"].ToString();
                this.txtExrate1.Text = Convert.ToDouble(curinf.Rows[0]["exratea"]).ToString("#,##0.000000 ;-#,##0.000000;");
                this.DdlCurList2.SelectedValue = curinf.Rows[0]["curcodb"].ToString();
                this.txtExrate2.Text = Convert.ToDouble(curinf.Rows[0]["exrateb"]).ToString("#,##0.000000 ;-#,##0.000000;");
                DdlCurList1_SelectedIndexChanged(null, null);
                DdlCurList2_SelectedIndexChanged(null, null);
                this.txtConversion1.Text = this.DdlCurList1.SelectedItem.ToString();
                this.txtConversion2.Text = this.DdlCurList2.SelectedItem.ToString();
                double cost = Convert.ToDouble("0" + this.txtEstimated.Text.Trim());
                double method1 = Convert.ToDouble("0" + this.txtExrate1.Text);
                this.TxtCostCur1.Text = (method1 * cost).ToString();

                double method2 = Convert.ToDouble("0" + this.txtExrate2.Text);
                this.TxtCostCur2.Text = (method2 * cost).ToString();

            }
        }
        private void GetMatfromInquery()
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct>)ViewState["tblproinquery"];

            foreach (SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSamProduct lst1 in lst)
            {
                DataRow dr1 = dt.NewRow();
                dr1["grpid"] = lst1.grpid;
                dr1["grp"] = lst1.grp;
                dr1["grpdesc"] = lst1.grpdesc;
                dr1["rescode"] = lst1.msircode;
                dr1["resdesc"] = lst1.msirdesc;
                dr1["resunit"] = lst1.msirunit;
                dr1["conqty"] = 0;
                dr1["issueqty"] = 0;
                dr1["rate"] = 0;
                dr1["amt"] = 0;
                dr1["convrate"] = 0;
                dr1["percnt"] = 0;
                dr1["spcfcode"] = lst1.spcfcod;
                dr1["spcfdesc"] = lst1.msirdesc;
                dr1["cfrate"] = 0.00;
                dr1["fcfrate"] = 0.00;
                dr1["cmpgrpdsc"] = "";

                dt.Rows.Add(dr1);

            }
            ViewState["tblstdcost"] = dt;
        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblstdcost"];
            DataTable tblcomncost = (DataTable)ViewState["tblcomncost"];

            // DataTable dt1 = (DataTable)ViewState["tblSmpleSizes"];
            // DataTable commoncost = (DataTable)ViewState["CommonCost"];


            //if (this.ChckVIew.Checked == false)
            //{
            string Entrype = this.Request.QueryString["Type"].ToString();
            switch (Entrype)
            {
                case "Entry":
                case "ConApp":
                    this.gvCost.Columns[2].Visible = false;
                    //this.gvCost.Columns[3].Visible = false;
                    this.gvCost.Columns[5].Visible = false;
                    this.gvCost.Columns[8].Visible = false;
                    this.gvCost.Columns[9].Visible = false;
                    this.gvCost.Columns[11].Visible = false;
                    this.gvCost.Columns[12].Visible = false;
                    this.gvCost.Columns[13].Visible = false;
                    this.gvCost.Columns[14].Visible = false;
                    this.gvCost.Columns[15].Visible = false;
                    this.gvCost.Columns[16].Visible = false;
                    this.gvCost.Columns[17].Visible = false;
                    this.gvCost.Columns[18].Visible = false;
                    this.gvCost.Columns[21].Visible = false;


                    break;

                case "PGEntry":
                    this.gvCost.Columns[2].Visible = false;
                    this.gvCost.Columns[5].Visible = false;
                    this.gvCost.Columns[8].Visible = false;
                    this.gvCost.Columns[9].Visible = false;
                    this.gvCost.Columns[11].Visible = false;
                    this.gvCost.Columns[12].Visible = false;
                    this.gvCost.Columns[13].Visible = false;
                    this.gvCost.Columns[14].Visible = false;
                    this.gvCost.Columns[15].Visible = false;
                    this.gvCost.Columns[16].Visible = false;
                    this.gvCost.Columns[17].Visible = false;
                    this.gvCost.Columns[18].Visible = false;
                    this.gvCost.Columns[21].Visible = true;
                    break;
                case "PGApp":
                    this.gvCost.Columns[2].Visible = false;
                    this.gvCost.Columns[5].Visible = false;
                    this.gvCost.Columns[8].Visible = true;
                    this.gvCost.Columns[11].Visible = false;
                    this.gvCost.Columns[12].Visible = false;
                    this.gvCost.Columns[13].Visible = false;
                    this.gvCost.Columns[14].Visible = false;
                    this.gvCost.Columns[15].Visible = false;
                    this.gvCost.Columns[16].Visible = false;
                    this.gvCost.Columns[17].Visible = false;
                    this.gvCost.Columns[18].Visible = false;
                    this.gvCost.Columns[21].Visible = true;
                    break;
                case "PreCosting":
                case "PreCostingApp":
                    this.gvCost.Columns[1].Visible = false;
                    this.gvCost.Columns[2].Visible = false;
                    this.gvCost.Columns[7].Visible = true;
                    this.gvCost.Columns[8].Visible = false;
                    this.gvCost.Columns[18].Visible = true;
                    this.gvCost.Columns[21].Visible = true;
                    break;



                //case "PGApp":
                //    this.gvCost.Columns[2].Visible = false;                   
                //    this.gvCost.Columns[4].Visible = false;                    
                //    this.gvCost.Columns[8].Visible = false;
                //    this.gvCost.Columns[9].Visible = false;
                //    this.gvCost.Columns[10].Visible = false;
                //    this.gvCost.Columns[11].Visible = false;
                //    this.gvCost.Columns[12].Visible = false;
                //   // this.gvCost.Columns[13].Visible = false;
                //    //this.gvCost.Columns[14].Visible = false;
                //    //this.gvCost.Columns[15].Visible = false;
                //    break;






                default:
                    this.gvCost.Columns[2].Visible = false;
                    //this.gvCost.Columns[3].Visible = false;
                    this.gvCost.Columns[5].Visible = false;

                    this.gvCost.Columns[18].Visible = false;
                    this.gvCost.Columns[19].Visible = false;
                    this.gvCost.Columns[20].Visible = false;
                    break;

            }

            this.gvCost.DataSource = HiddenSameValue(dt.Copy());
            this.gvCost.DataBind();
            this.gvdircost.DataSource = tblcomncost;
            this.gvdircost.DataBind();
            if (dt.Rows.Count == 0)
                return;
            Session["Report1"] = gvCost;
            ((HyperLink)this.gvCost.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            this.FooterCalculation(dt);
            if (tblcomncost.Rows.Count == 0)
                return;

            DataView dv = tblcomncost.Copy().DefaultView;
            dv.RowFilter = "sircode not like '%999'";
            double totlcst = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", "")));
            double tltcommncost = Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("sum(stdamt)", "")) ? 0.00 : dv.ToTable().Compute("sum(stdamt)", "")));

            ((Label)this.gvdircost.FooterRow.FindControl("lblgvfamtCost")).Text = (totlcst + tltcommncost).ToString("#,##0.0000;(#,##0.0000); ");

            double offerprice = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txtoffprice.Text.Trim()));
            double tarprice = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.txttarprice.Text.Trim()));

            this.txtEstimated.Text = (totlcst + tltcommncost).ToString("#,##0.0000; ");
            this.TxtPlAnalysis.Text = (offerprice - Convert.ToDouble("0" + this.TxtCostCur1.Text)).ToString("#,##0.0000;-#,##0.00 ");

            this.txtoffpercnt.Text = (((offerprice - Convert.ToDouble("0" + this.TxtCostCur1.Text)) / Convert.ToDouble("0" + this.TxtCostCur1.Text)) * 100).ToString("#,##0.00;");
            this.txtTarpercnt.Text = (((tarprice - Convert.ToDouble("0" + this.TxtCostCur1.Text)) / Convert.ToDouble("0" + this.TxtCostCur1.Text)) * 100).ToString("#,##0.00;");

        }

        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            if (this.ChckVIew.Checked == false)
            {
                ((Label)this.gvCost.FooterRow.FindControl("lblgvBdamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(convrate)", "")) ? 0.00 : dt.Compute("sum(convrate)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
                ((Label)this.gvCost.FooterRow.FindControl("lblgvfamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
                ((Label)this.gvCost.FooterRow.FindControl("lblgvfper")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.0000;(#,##0.0000); ") + (Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))) > 0 ? "%" : "");
                //((Label)this.gvCost.FooterRow.FindControl("lblgvttlqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }
            else
            {


                //((Label)this.gvRtpcon.FooterRow.FindControl("lblgvBdamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(convrate)", "")) ? 0.00 : dt.Compute("sum(convrate)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
                //((Label)this.gvRtpcon.FooterRow.FindControl("lblgvfamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
                //((Label)this.gvRtpcon.FooterRow.FindControl("lblgvfper")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.0000;(#,##0.0000); ") + (Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))) > 0 ? "%" : "");
                ////((Label)this.gvRtpcon.FooterRow.FindControl("lblgvttlqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }

            this.TxttoalMatCost.Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
        }



        protected void lnkAddResouctCost_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            DataTable tbl2 = (DataTable)ViewState["tblstdcost"];
            string grp = this.ddlGrp.SelectedValue.ToString();
            string grpdesc = this.ddlGrp.SelectedItem.ToString();
            string rescod = this.ddlResourcesCost.SelectedValue.ToString();
            string Specification = this.DdlSpecres.SelectedValue.ToString();
            string component = this.ddlComponent.SelectedValue.ToString();

            var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGroup>)ViewState["lstgroup"];


            DataRow[] dr = tbl2.Select("compcode='" + component + "' and  rescode='" + rescod + "' and spcfcode='" + Specification + "'");

            if (dr.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Selected Component Already Added');", true);

                return;
            }
            else
            {
                DataRow dr1 = tbl2.NewRow();
                dr1["grpid"] = lst.FindAll(l => l.grp == grp)[0].Id;
                //dr1["grp"] = grp;
                //dr1["grpdesc"] = grpdesc;
                dr1["rescode"] = rescod;
                dr1["resdesc"] = this.ddlResourcesCost.SelectedItem.ToString();
                dr1["resunit"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "'"))[0]["sirunit"].ToString();
                dr1["preqty"] = 0;
                dr1["qty"] = 0;
                dr1["westpc"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "'"))[0]["allowance"].ToString();
                dr1["conqty"] = 0;
                dr1["issueqty"] = 0;
                dr1["rate"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "'"))[0]["rate"].ToString();
                dr1["amt"] = 0;
                dr1["convrate"] = 0;
                dr1["percnt"] = 0;
                dr1["spcfcode"] = Specification;
                dr1["spcfdesc"] = this.ddlResourcesCost.SelectedItem.ToString() + "-" + this.DdlSpecres.SelectedItem.ToString();
                dr1["cfrate"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' "))[0]["cfrate"].ToString();
                dr1["fcfrate"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' "))[0]["rate"].ToString();
                dr1["compcode"] = component;
                dr1["compdesc"] = this.ddlComponent.SelectedItem.ToString();
                dr1["compgrp"] = this.DDLCompGroup.SelectedValue.ToString();
                dr1["cmpgrpdsc"] = this.DDLCompGroup.SelectedItem.ToString();
                tbl2.Rows.Add(dr1);
            }



            DataView dv = tbl2.DefaultView;
            dv.Sort = ("grpid, compgrp, rescode");
            ViewState["tblstdcost"] = dv.ToTable();
            this.Data_Bind();
        }
        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            double qty, rate, amt, convRate, cfrate;
            string Notes = "";

            if (ChckVIew.Checked == false)
            {
                double convrate1 = Convert.ToDouble("0" + ((TextBox)txtExchngerate).Text.Trim());

                for (int i = 0; i < this.gvCost.Rows.Count; i++)
                {
                    //string resdesc = ((Label)this.gvCost.Rows[i].FindControl("lblgvDesc")).Text.Trim();
                    string resunit = ((Label)this.gvCost.Rows[i].FindControl("txtgvunit0")).Text.Trim();
                    double preqty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvPreqty")).Text.Trim());

                    double conqty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvconqty")).Text.Trim());
                    double wper = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvwestpc")).Text.Trim());

                    double netQty = conqty + (conqty * (wper / 100));

                    // ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text = netQty.ToString("#,##0.000000;(#,##0.000000); ");

                    //  qty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text.Trim());
                    rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqrateCost")).Text.Trim()));
                    cfrate = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqcfrate")).Text.Trim());
                    Notes = ((TextBox)this.gvCost.Rows[i].FindControl("txtgvnotes")).Text.Trim();

                    double amtgrid = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvCostamtCost")).Text.Trim());
                    double fcfrate = (cfrate * rate / 100) + rate;
                    amt = netQty * fcfrate;
                    convRate = netQty * convrate1;
                    string type = this.Request.QueryString["Type"].ToString();
                    switch (type)
                    {
                        case "Entry":
                        case "PGEntry":
                        case "ConApp":
                            conqty = Convert.ToDouble("0" + dt.Rows[i]["qty"]);
                            netQty = Convert.ToDouble("0" + dt.Rows[i]["conqty"]);
                            wper = Convert.ToDouble("0" + dt.Rows[i]["westpc"]);
                            rate = Convert.ToDouble("0" + dt.Rows[i]["rate"]);
                            cfrate = Convert.ToDouble("0" + dt.Rows[i]["cfrate"]);
                            amtgrid = Convert.ToDouble("0" + dt.Rows[i]["amt"]);

                            break;
                        case "PGApp":

                            netQty = Convert.ToDouble("0" + dt.Rows[i]["conqty"]);
                            wper = Convert.ToDouble("0" + dt.Rows[i]["westpc"]);
                            rate = Convert.ToDouble("0" + dt.Rows[i]["rate"]);
                            cfrate = Convert.ToDouble("0" + dt.Rows[i]["cfrate"]);
                            amtgrid = Convert.ToDouble("0" + dt.Rows[i]["amt"]);

                            break;
                        case "PreCosting":
                        case "PreCostingApp":
                            preqty = Convert.ToDouble("0" + dt.Rows[i]["preqty"]);

                            break;
                    }
                    dt.Rows[i]["preqty"] = preqty;
                    //dt.Rows[i]["resdesc"] = resdesc;
                    dt.Rows[i]["resunit"] = resunit;
                    dt.Rows[i]["qty"] = conqty;
                    dt.Rows[i]["conqty"] = netQty;
                    dt.Rows[i]["westpc"] = wper;
                    // dt.Rows[i]["qty"] = qty;
                    dt.Rows[i]["rate"] = rate;
                    dt.Rows[i]["convrate"] = convRate;
                    dt.Rows[i]["cfrate"] = cfrate;
                    dt.Rows[i]["fcfrate"] = fcfrate;
                    dt.Rows[i]["notes"] = Notes;
                    dt.Rows[i]["amt"] = (netQty * fcfrate > 0) ? amt : amtgrid;
                }

                double netqty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(conqty)", "")) ? 0.00 : dt.Compute("sum(conqty)", "")));
                //  double netqty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", "")));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["percnt"] = (netqty == 0) ? 0.00 : (Convert.ToDouble(dt.Rows[i]["conqty"].ToString()) * 100) / netqty;

                }
            }


            if (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp")
            {

                DataTable commoncost = (DataTable)ViewState["tblcomncost"];
                // this.lnkbtnAdd_Click(null, null);
                //-------------------------------------- for percent------------------------------
                //DataTable ttlcost = (DataTable)ViewState["tblttlcost"];
                //if (ChckVIew.Checked == true)
                //{
                //    if (ttlcost != null)
                //    {
                //        DataView dv = ttlcost.DefaultView;
                //        dv.RowFilter = "rsircode not like '049800108%' OR rsircode not like '049800105006%'";
                //        ttlcost = dv.ToTable();
                //        double totlfor_percent = Convert.ToDouble((Convert.IsDBNull(ttlcost.Compute("sum(stdamt)", "")) ? 0.00 : ttlcost.Compute("sum(stdamt)", "")));
                //DataTable dt = (DataTable)ViewState["tblstdcost"];
                double totmatcost = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", "")));
                double tltcommncost = 0;
                double totlfor_percent = 0;
                double totalpercnt = 0;
                for (int i = 0; i < this.gvdircost.Rows.Count; i++)
                {


                    double com_amt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvdircost.Rows[i].FindControl("txtgvamtCost")).Text.Trim()));
                    double percnt = Convert.ToDouble("0" + ((TextBox)this.gvdircost.Rows[i].FindControl("txtpercnt")).Text.Trim());
                    string sircode = ((Label)this.gvdircost.Rows[i].FindControl("lblgvcodeCost")).Text.Trim();
                    if (sircode == "049800101999")
                    {

                        com_amt = totmatcost;


                    }
                    else if (sircode == "049800000999")
                    {
                        DataView dv1 = commoncost.Copy().DefaultView;
                        dv1.RowFilter = "deptcod='A' OR deptcod='B'";
                        DataTable locost = dv1.ToTable();
                        com_amt = Convert.ToDouble((Convert.IsDBNull(locost.Compute("sum(stdamt)", "")) ? 0.00 : locost.Compute("sum(stdamt)", "")));


                    }
                    else if (sircode == "040000000999")
                    {
                        DataView dv1 = commoncost.Copy().DefaultView;
                        dv1.RowFilter = "deptcod='D' OR sircode='049800000999'";
                        DataTable locost = dv1.ToTable();
                        com_amt = Convert.ToDouble((Convert.IsDBNull(locost.Compute("sum(stdamt)", "")) ? 0.00 : locost.Compute("sum(stdamt)", "")));


                    }
                    else if (sircode == "04XXXXXXX999")
                    {
                        DataView dv1 = commoncost.Copy().DefaultView;
                        dv1.RowFilter = "deptcod='G' and sircode<>'04XXXXXXX999'";
                        DataTable locost = dv1.ToTable();
                        com_amt = Convert.ToDouble((Convert.IsDBNull(locost.Compute("sum(stdamt)", "")) ? 0.00 : locost.Compute("sum(stdamt)", "")));


                    }
                    else if (sircode == "049800102999")
                    {
                        DataView dv1 = commoncost.Copy().DefaultView;
                        dv1.RowFilter = "sircode like '049800102%' and sircode not like '049800102999%'";
                        DataTable locost = dv1.ToTable();
                        com_amt = Convert.ToDouble((Convert.IsDBNull(locost.Compute("sum(stdamt)", "")) ? 0.00 : locost.Compute("sum(stdamt)", "")));


                    }

                    else if (sircode == "049800104999")
                    {
                        DataView dv2 = commoncost.Copy().DefaultView;
                        dv2.RowFilter = "deptcod='E' OR sircode='049800102999' OR deptcod='G' OR deptcod='H'";

                        // dv2.RowFilter = "sircode not like '049800102999%' and sircode not like '049800104999%' and  sircode not like '049800105%' and  sircode not like '049800108%'";
                        DataTable netcost = dv2.ToTable();
                        com_amt = Convert.ToDouble((Convert.IsDBNull(netcost.Compute("sum(stdamt)", "")) ? 0.00 : netcost.Compute("sum(stdamt)", "")));

                    }
                    DataView dv = commoncost.Copy().DefaultView;
                    dv.RowFilter = "sircode like '049800104999%'";
                    DataTable totalcost = dv.ToTable();
                    totlfor_percent = Convert.ToDouble((Convert.IsDBNull(totalcost.Compute("sum(stdamt)", "")) ? 0.00 : totalcost.Compute("sum(stdamt)", "")));

                    if (percnt > 0)
                    {
                        totalpercnt += percnt;
                        //com_amt = (totlfor_percent * percnt) / 100;
                        //com_amt = com_amt + (com_amt * percnt) / 100;
                    }


                    tltcommncost += com_amt;

                    commoncost.Rows[i]["stdamt"] = com_amt;
                    commoncost.Rows[i]["percnt"] = percnt;
                }
                //    }
                //}

                foreach (DataRow dr in commoncost.Rows)
                {
                    if (dr["sircode"].ToString().Substring(0, 9) == "049800105" || (dr["sircode"].ToString().Substring(0, 9) == "049800108"))
                    {
                        double grandtotalCost = Math.Round(totlfor_percent / ((100 - totalpercnt) / 100), 6);

                        dr["stdamt"] = (grandtotalCost * Convert.ToDouble(dr["percnt"])) / 100;
                    }
                }

                ViewState["tblcomncost"] = commoncost;
            }


            ViewState["tblstdcost"] = dt;



        }
        private void UpdateCost()
        {

            this.Save_Value();
            //this.BindCalcultiveInfo();

            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string condate = this.txtDatefrom.Text.Trim();
            string PostDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            DataTable dtComm = (DataTable)ViewState["tblcomncost"];
            string sampleid = this.ddlinqno.SelectedValue.ToString();
            double convrate1 = Convert.ToDouble("0" + ((TextBox)txtExchngerate).Text.Trim());
            string marchand = this.RefMarName.Text.Trim().ToString();

            string Notes = this.txtNotes.Text.Trim().ToString();
            string curcode = this.ddlCurList.SelectedValue.ToString();
            double tarprice = Convert.ToDouble("0" + ((TextBox)txttarprice).Text.Trim());
            double offprice = Convert.ToDouble("0" + ((TextBox)txtoffprice).Text.Trim());
            double confrmprice = Convert.ToDouble("0" + ((TextBox)txtconfrmprice).Text.Trim());

            string curcodea = this.DdlCurList1.SelectedValue.ToString();
            double exrate1 = Convert.ToDouble("0" + ((TextBox)txtExrate1).Text.Trim());
            string curcodeb = this.DdlCurList2.SelectedValue.ToString();
            double exrate2 = Convert.ToDouble("0" + ((TextBox)txtExrate2).Text.Trim());
            this.AddSmpSizSaveValue();
            DataTable tbl2 = (DataTable)ViewState["tblSmpleSizes"];
            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(tbl2);
            ds.Tables[0].TableName = "tblsizes";

            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "INSERTORUPDATESAMCONSUMINFAB", ds, null, null, "SAMCONINFB", sampleid, marchand,
                curcode, convrate1.ToString(), tarprice.ToString(), offprice.ToString(), confrmprice.ToString(),
               userid, sessionid, trmid, PostDat, Notes, condate, curcodea, exrate1.ToString(), curcodeb, exrate2.ToString());

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + Merdata.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }


            string grp = "";

            //foreach (DataRow dr1 in dt.Rows)
            //{
            //    grp = dr1["grp"].ToString();
            //    string rescod = dr1["rescode"].ToString();
            //    string resdesc = dr1["resdesc"].ToString();
            //    string runit = dr1["resunit"].ToString();
            //    string conqty = dr1["conqty"].ToString();
            //    string rate = dr1["rate"].ToString();
            //    string percnt = dr1["percnt"].ToString();
            //    string resamt = dr1["amt"].ToString();
            //    string spcfcode = dr1["spcfcode"].ToString();
            //    string cfrate = dr1["cfrate"].ToString();
            //    string compcode = dr1["compcode"].ToString();
            //    string pgaprvbyid = dr1["pgaprvbyid"].ToString();
            //    string preqty = dr1["preqty"].ToString();
            //    string qty = dr1["qty"].ToString();
            //    string westpc = dr1["westpc"].ToString();
            //    string notes = dr1["notes"].ToString();

            //}
            DataSet dsitems = new DataSet("ds2");
            dsitems.Tables.Add(dt);
            dsitems.Tables[0].TableName = "tblitem";

            var temp = dsitems.GetXml();
            //result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "INSERTORUPDATESAMCONSUMINFAB", dsitems,null,null, "SAMCONINFA", sampleid, grp,
            //        rescod, spcfcode, conqty, rate, cfrate, resamt, percnt, compcode, qty, westpc, preqty, notes, pgaprvbyid);
            result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "INSERTORUPDATESAMCONSUMINFAB", dsitems, null, null, "SAMCONINFA", sampleid, "",
                   "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + Merdata.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully.');", true);

          

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + Merdata.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }





            if (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp")
            {
                for (int i = 0; i < dtComm.Rows.Count; i++)
                {
                    string compcode = "000000000000";
                    string rescod = dtComm.Rows[i]["sircode"].ToString();
                    string resamt = dtComm.Rows[i]["stdamt"].ToString();
                    string percnt = dtComm.Rows[i]["percnt"].ToString(); ;
                    string conqty = "0.00";
                    string spcfcode = "000000000000";
                    if (rescod.Substring(9) == "999")
                        continue;
                    result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "INSERTORUPDATESAMCONSUMINFAB", "SAMCONINFA", sampleid, grp, rescod, spcfcode, conqty, "0.00", "0.00", resamt, percnt, compcode, "0.00", "0.00", "0.00");

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + Merdata.ErrorObject["Msg"].ToString() + "');", true);

                        return;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully.');", true);

                }
            }

            /// after adding new item in modified pd guide its need to to all item
            /// approved other wise its make a error

            if (this.Request.QueryString["Type"].ToString() == "PGApp")
            {
                bool result1 = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "UPDATED_NEW_ITEM_APPROVED", sampleid, userid, PostDat, sessionid, trmid);

            }

        }
        private string XmlDataInsert(string Inqno, string Styleid, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");

            ds.Tables[0].Columns.Add("delbyid", typeof(string));
            ds.Tables[0].Columns.Add("delseson", typeof(string));
            ds.Tables[0].Columns.Add("deldate", typeof(DateTime));

            ds.Tables[0].Rows[0]["delbyid"] = usrid;
            ds.Tables[0].Rows[0]["delseson"] = session;
            ds.Tables[0].Rows[0]["deldate"] = Date;


            ds1.Merge(ds.Tables[0]);
            //ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            //ds1.Tables[1].TableName = "tbl2";

            bool resulta = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Inqno, Styleid);

            if (!resulta)
            {

                //return;
            }
            else
            {
                //    ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                //    ((Label)this.Master.FindControl("lblANMgsBox")).Text = "Successfully Deleted";
                //       ((Label)this.Master.FindControl("lblANMgsBox")).Attributes["style"] = "background:Green;";

                //  ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

            return "";
        }
        protected void gvCost_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //string comcod = this.GetCompCode();
            //DataTable dt = (DataTable)ViewState["tblstdcost"];
            //string Inqno = this.ddlinqno.SelectedValue.ToString();

            //string prodcode = this.ddlStyle.SelectedValue.ToString();
            //string proscode = this.ddlProcess.SelectedValue.ToString();

            //string rescod = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcodeCost")).Text.Trim();
            //string spcfcode = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvspcfcode")).Text.Trim();
            //string compcode = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcompcode")).Text.Trim();
            //string colorid = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcolor")).Text.Trim();
            //string sizeid = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvsize")).Text.Trim();

            //DataSet ds1 = new DataSet("ds1");
            //ds1.Tables.Add(dt);

            //this.XmlDataInsert(Inqno, prodcode, ds1);


            //bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "DELETE_RESOURCE", prodcode, proscode, rescod, colorid, sizeid, compcode, spcfcode, Inqno, "", "", "", "", "", "", "");
            //if (!result)
            //    return;
            //int index = (this.gvCost.PageIndex) * this.gvCost.PageSize + e.RowIndex;
            //dt.Rows[index].Delete();
            //ViewState["tblstdcost"] = dt;
            //this.Data_Bind();
        }

        protected void ChckVIew_CheckedChanged(object sender, EventArgs e)
        {
            ViewState.Remove("tblstdcost");
            this.gvdircost.DataSource = null;
            this.gvdircost.DataBind();
            this.Select_View();
        }
        protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp")
                {
                    e.Row.Cells[7].Style.Add("display", "none");
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //   HyperLink hlnkMatIssue = (HyperLink)e.Row.FindControl("hlnkMatIssue");
                LinkButton lnkpgMatApproval = (LinkButton)e.Row.FindControl("lnkpgMatApproval");

                if (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp")
                {
                    ((TextBox)e.Row.FindControl("txtgvconqty")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtgvPreqty")).Style.Add("display", "none");
                    e.Row.Cells[7].Style.Add("display", "none");

                    string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).ToString();
                    if ((Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rate"))) == 0)
                    {
                        ((TextBox)e.Row.FindControl("txtgvqrateCost")).CssClass = "bg-flickr";
                    }
                }

                else if (((Label)e.Row.FindControl("lblgvissuedqty")).Text.ToString() == " ")
                {
                    //((TextBox)e.Row.FindControl("txtgvconqty")).Enabled = false;

                }

                else

                {
                    ((TextBox)e.Row.FindControl("txtgvconqty")).Enabled = true;

                }
                if (this.Request.QueryString["Type"].ToString() == "PGEntry" || this.Request.QueryString["Type"].ToString() == "PGApp")
                {
                    string inqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "inqno")).ToString();
                    string pgaprvbyid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pgaprvbyid")).ToString();
                    string actcode = this.Request.QueryString["actcode"] ?? "";



                    if (pgaprvbyid.Length > 0)
                    {
                        lnkpgMatApproval.Enabled = false;
                        lnkpgMatApproval.ToolTip = "Item Already Approved";
                        lnkpgMatApproval.Attributes["style"] = "color:red;";

                    }
                    else
                    {
                        //  hlnkMatIssue.Enabled = false;



                    }



                }

                if (((Label)e.Row.FindControl("lblgvgrp")).Text.ToString() != "")
                {
                    if(this.Request.QueryString["Type"].ToString() != "PreCosting" && this.Request.QueryString["Type"].ToString() != "PreCostingApp")
                    {
                        ((LinkButton)e.Row.FindControl("lnkbtnDltCrs")).Visible = true;
                    }

                }





            }







            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp")
                {
                    ((Label)e.Row.FindControl("lbltoalf")).Visible = true;
                    e.Row.Cells[7].Style.Add("display", "none");
                }
                else
                {
                    ((Label)e.Row.FindControl("lbltoalf")).Visible = false;
                }
            }
        }
        private DataTable HiddenSameValue(DataTable dt)
        {

            string type = Request.QueryString["Type"];
            if (dt.Rows.Count == 0)
                return dt;
            int i = 0;
            string grp = dt.Rows[0]["compgrp"].ToString();

            foreach (DataRow dr1 in dt.Rows)
            {
                if (i == 0)
                {
                    if (type == "PreCosting" || type == "PreCostingApp")
                    {
                        string total = dt.AsEnumerable()
                                            .Where(y => y.Field<string>("compgrp").ToString() == grp)
                                            .Sum(x => x.Field<decimal>("amt"))
                                            .ToString();
                        dr1["cmpgrpdsc"] = dr1["cmpgrpdsc"].ToString() + "<br>" + total;
                    }
                    else
                    {
                        dr1["cmpgrpdsc"] = dr1["cmpgrpdsc"].ToString();
                    }

                    grp = dr1["compgrp"].ToString();

                    i++;
                    continue;
                }

                if (dr1["compgrp"].ToString() == grp)
                {

                    dr1["cmpgrpdsc"] = "";

                }
                else
                {
                    grp = dr1["compgrp"].ToString();
                    if (type == "PreCosting" || type == "PreCostingApp")
                    {
                        string total = dt.AsEnumerable()
                                            .Where(y => y.Field<string>("compgrp").ToString() == grp)
                                            .Sum(x => x.Field<decimal>("amt"))
                                            .ToString();
                        dr1["cmpgrpdsc"] = dr1["cmpgrpdsc"].ToString() + "<br>" + total;
                    }
                    else
                    {
                        dr1["cmpgrpdsc"] = dr1["cmpgrpdsc"].ToString();
                    }
                }



                grp = dr1["compgrp"].ToString();
            }



            DataView dv = dt.DefaultView;
            //dv.Sort = "compcode asc";
            // ViewState["tblstdcost"] = dv.ToTable();
            return dv.ToTable();


        }



        protected void gvdircost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //        HyperLink hlink1 = (HyperLink)e.Row.FindControl("lblgvDesc");

                //        Hashtable hst = (Hashtable)Session["tblLogin"];
                //        string comcod = hst["comcod"].ToString();



                //        string inqno = this.ddlinqno.SelectedValue.ToString();
                //        string styleid = this.ddlStyle.SelectedValue.ToString();

                //        string Type = this.Request.QueryString["Type"].ToString();

                string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sircode")).ToString();

                //        if (ASTUtility.Left(rescode, 9) == "049800101")
                //        {
                //            if (Type == "Entry" || Type == "ConApp")
                //            {
                //                hlink1.Text = "Commmon Material Consumption";
                //            }
                //            hlink1.NavigateUrl = "~/F_01_Mer/LinkConsumptionSheet?Type=" + Type + "&actcode=" + inqno + "&genno=" + styleid;
                //            //hlink1.Attributes()
                //        }
                ((TextBox)e.Row.FindControl("txtpercnt")).Enabled = false;

                if (rescode.Substring(0, 9) == "049800108" || rescode.Substring(0, 9) == "049800105")//049800105006 factoring charge for FB Footwear
                {
                    ((TextBox)e.Row.FindControl("txtpercnt")).Enabled = true;
                }
                if (rescode.Substring(9) == "999")
                {
                    e.Row.Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.LightPink;
                }
                //        //else
                //        //{
                //        //    HypApproval.NavigateUrl = "~/F_13_CWare/PurReqEntry02.aspx?InputType=FxtAstApproval&actcode=" + prjCode + "&genno=" + reqno;
                //        //}






                //    }
                //    if (e.Row.RowType == DataControlRowType.Footer)
                //    {
                //        if (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp")
                //        {
                //            double conAmt = 0;
                //            double otAmt = 0;
                //            int RowIndex = e.Row.RowIndex;
                //            int DataItemIndex = e.Row.DataItemIndex;
                //            int Columnscount = gvdircost.Columns.Count;
                //            GridViewRow row = new GridViewRow(RowIndex, DataItemIndex, DataControlRowType.Footer, DataControlRowState.Normal);
                //            for (int i = 0; i < Columnscount; i++)
                //            {
                //                string mtext = String.Empty;
                //                switch (i.ToString())
                //                {
                //                    case "1":
                //                        mtext = "Profit/Loss with Percent";
                //                        break;
                //                    case "3":
                //                        DataTable dt2 = (DataTable)ViewState["tblstdcost"];
                //                        DataTable commoncost = (DataTable)ViewState["CommonCost"];
                //                        conAmt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(amt)", "")) ? 0.00 : dt2.Compute("sum(amt)", "")));
                //                        otAmt = Convert.ToDouble((Convert.IsDBNull(commoncost.Compute("sum(amt)", "")) ? 0.00 : commoncost.Compute("sum(amt)", "")));
                //                        double confirmprice = Convert.ToDouble("0" + this.txtconfrmprice.Text.Trim());
                //                        mtext = (((confirmprice-(conAmt + otAmt))*100)/ (conAmt + otAmt)).ToString("#,##0.0000;(#,##0.0000); ")+" %";
                //                        break;
                //                    default:
                //                        mtext = "";
                //                        break;
                //                }
                //                TableCell tablecell = new TableCell();
                //                tablecell.Text = mtext;
                //                tablecell.Attributes.CssStyle.Add("text-align", "right");
                //                tablecell.Attributes.CssStyle.Add("color", "blue");
                //                row.Cells.Add(tablecell);
                //            }
                //            this.gvdircost.Controls[0].Controls.Add(row);



                //            ((Label)e.Row.FindControl("lbltoalf")).Visible = true;
                //        }
                //        else
                //        {
                //            ((Label)e.Row.FindControl("lbltoalf")).Visible = false;
                //        }
            }
        }

        protected void Con_Cost_Approved(object sender, EventArgs e)
        {

            this.Consumtion_Approved();
        }

        private void Consumtion_Approved()
        {

            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Enabled = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string AppDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string InqNO = this.ddlinqno.SelectedValue.ToString();
            string stylename = this.txtCategory.Text.ToString();
            string artno = this.txtArtno.Text.ToString();

            string updateType = (this.Request.QueryString["Type"] == "ConApp") ? "Cons"
                : (this.Request.QueryString["Type"] == "PGApp") ? "PGApp"
                : "PreCostingApp";

            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "COMSUMTIONAPPROVED", null, null, null, InqNO, userid, AppDat, trmid, sessionid, updateType);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Consumption Not Approved');", true);

                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Consumption Approved');", true);

            if (this.Request.QueryString["Type"].ToString() == "PreCostingApp")
            {
                string esubject = "Costing Complete! Request to Order Generate";
                string url = "http://202.0.94.49/F_34_Mgt/RptAdminInterface";
                string bodyContent = "Dear Sir, </br>A New Order Generate, Please click  <b> <a href='" + url +
                                "' target='_blank'>" + stylename + "-" + artno + " </a> </b> on the link to Accept or Reject";

                if (CommonClass.ConfimMail("0101010", esubject, url, bodyContent) == true)
                {

                }
            }



            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

            //Common.LogStatus("Diagnosis Complite", "QC Qualified", "Recived No: ", centrid + " - " + wrRecvno);
        }



        private void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();

            string PostDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            // DataTable dtComm = (DataTable)ViewState["CommonCost"];          
            string sampleid = this.ddlinqno.SelectedValue.ToString();

            DataView dv = dt.Copy().DefaultView;
            dv.RowFilter = "pgaprvbyid='' and rescode not like '0498%'";
            //Posting User
            if (dv.ToTable().Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Before Forward Please Approve All Item');", true);
                return;
            }
            string ConType = this.Request.QueryString["Type"];
            bool result = false;

            switch (ConType)
            {
                case "PGEntry":
                case "PreCosting":
                    result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "INSERTORUPCONSUMPUSER", null, null, null, sampleid,
               userid, sessionid, trmid, PostDat, ConType, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                    break;
                default:
                    break;

            }

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Forward Successfully');", true);
            }
        }
        protected void LbtnComponent_Click(object sender, EventArgs e)
        {
            this.GetComponentList();
        }

        protected void txtgvqrateCost_TextChanged(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
            GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
            int index = row.RowIndex + 1;
            if (index <= this.gvCost.Rows.Count - 1)
            {
                ((TextBox)this.gvCost.Rows[index].FindControl("txtgvqrateCost")).Focus();
            }
        }
        protected void ChckCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChckCopy.Checked == true)
            {
                this.ddlPreList.Visible = true;
                this.ibtnPreList.Visible = true;
                ibtnPreList_Click(null, null);
                this.LbtnCopy0.Visible = true;
            }
            else
            {
                this.ddlPreList.Visible = false;
                this.ibtnPreList.Visible = false;
                this.LbtnCopy0.Visible = false;
            }
        }

        protected void LbtnCopy_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string frinqno = this.ddlPreList.SelectedValue.ToString();
            string toinqno = this.Request.QueryString["genno"].ToString();
            string comcod = this.GetCompCode();
            if (frinqno == toinqno)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('You Select Same Style');", true);

                return;
            }

            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETCONSUMPTIONINFO", frinqno, "", "", "", "", "", "");
            if (ds4 == null)
            {

                return;
            }

            foreach (DataRow dr in ds4.Tables[0].Rows)
            {

                DataRow[] dr1 = dt.Select("compcode='" + dr["compcode"] + "' and  rescode='" + dr["rescode"] + "' and spcfcode='" + dr["spcfcode"] + "'");

                if (dr1.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + dr["resdesc"].ToString() + " Already Added');", true);


                    continue;
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr["pgaprvbyid"] = dt.Rows[0]["pgaprvbyid"].ToString();
                        dr["sampleid"] = dt.Rows[0]["sampleid"].ToString();
                        dr["inqno"] = dt.Rows[0]["inqno"].ToString();
                        dr["inqno1"] = dt.Rows[0]["inqno1"].ToString();
                    }
                    dt.ImportRow(dr);

                }

            }

            ViewState["tblstdcost"] = dt;


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Loaded Please Save for Copy');", true);

            this.Data_Bind();



        }




        protected void LbtnImport_Click(object sender, EventArgs e)
        {

            //string process = this.ddlProcess.SelectedValue.ToString();
            //string toinqno = this.Request.QueryString["actcode"].ToString();
            //string tostyleid = this.Request.QueryString["genno"].ToString();
            //string comcod = this.GetCompCode();
            //string buyerid = this.txtbuyerid.Text.Trim().ToString();
            //bool result = Merdata.UpdateTransInfo1(comcod, "SP_ENTRY_CONSUMPTION", "COPY_CONSUMPTION_ANALYSIS", process, toinqno, tostyleid, buyerid);
            //if (result == true)
            //{
            //    this.ShowConsump();
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Import Successfully";
            //}
        }

        protected void lblgvspcfdesc_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string material = ((Label)this.gvCost.Rows[index].FindControl("lblgvcodeCost")).Text.ToString();
            string spcfcode = ((Label)this.gvCost.Rows[index].FindControl("lblgvspcfcode")).Text.ToString();
            string component = ((Label)this.gvCost.Rows[index].FindControl("lblgvCompcode")).Text.ToString();
            this.lblHelper.Text = component + material + spcfcode;
            DataSet result = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_FOR_CONSUMPTION", material);
            if (result.Tables[0].Rows.Count == 0 || result == null)
            {
                return;
            }

            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = result.Tables[0];
            this.ddlSpecification.DataBind();
            this.ddlSpecification.SelectedValue = spcfcode;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void lnkbtnSpecChange_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string comcod = this.GetCompCode();
            string sampleno = this.ddlinqno.SelectedValue.ToString();

            string grp = this.lblHelper.Text.ToString().Substring(0, 12);
            string rescode = this.lblHelper.Text.ToString().Substring(12, 12);
            string spcfcode = this.lblHelper.Text.ToString().Substring(24);
            string tospcfcod = this.ddlSpecification.SelectedValue.ToString();
            string spcfdesc = this.ddlSpecification.SelectedItem.Text.Trim();
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "CHANGESPECFORCONSUMPTION", sampleno, grp, rescode, spcfcode, tospcfcod, "", "", "", "");
            if (result == true)
            {

                DataRow[] dr1 = dt.Select("compcode='" + grp + "' and rescode='" + rescode + "' and spcfcode='" + spcfcode + "'");

                string madetmat = dr1[0]["resdesc"].ToString();
                dr1[0]["spcfcode"] = tospcfcod;
                dr1[0]["spcfdesc"] = madetmat + "-" + spcfdesc;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Specification Change Successfully');", true);


            }
            ViewState["tblstdcost"] = dt;
            this.Data_Bind();

        }




        protected void lnkpgMatApproval_Click(object sender, EventArgs e)
        {

            int index = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;

            string rescode = ((DataTable)ViewState["tblstdcost"]).Rows[index]["rescode"].ToString();
            string spcfcode = ((DataTable)ViewState["tblstdcost"]).Rows[index]["spcfcode"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string AppDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string InqNO = this.ddlinqno.SelectedValue.ToString();


            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "COMSUMTIONMATAPPROVED", null, null, null, InqNO, rescode, spcfcode, userid, AppDat, trmid, sessionid);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Consumption Not Approved');", true);

                return;

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Item Approved');", true);


            ((DataTable)ViewState["tblstdcost"]).Rows[index]["pgaprvbyid"] = userid;
            this.Data_Bind();



        }


        protected void lnkpgAllMatApproval_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["tblstdcost"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string AppDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string InqNO = this.ddlinqno.SelectedValue.ToString();
            string rescode, spcfcode, pgaprvbyid;


            foreach (DataRow dr1 in dt.Rows)
            {
                rescode = dr1["rescode"].ToString();
                spcfcode = dr1["spcfcode"].ToString();
                pgaprvbyid = dr1["pgaprvbyid"].ToString();
                if (pgaprvbyid.Length == 0)
                {
                    bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "COMSUMTIONMATAPPROVED", null, null, null, InqNO, rescode, spcfcode, userid, AppDat, trmid, sessionid);

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Consumption Not Approved');", true);

                        return;

                    }
                    dr1["pgaprvbyid"] = userid;
                }

            }





            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Item Approved');", true);

            this.Data_Bind();

        }

        protected void lnkbtnDelconsum_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;
            string sampleid = this.ddlinqno.SelectedValue.ToString();
            string grp = dt.Rows[index]["grp"].ToString();
            string rescode = dt.Rows[index]["rescode"].ToString();
            string spcfcode = dt.Rows[index]["spcfcode"].ToString();
            string compgrp = dt.Rows[index]["compcode"].ToString();
            string inqno = this.Request.QueryString["genno"].ToString();

            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "DELETECONQUANTITY", sampleid, grp, rescode, spcfcode, inqno, compgrp);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Merdata.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }

            dt.Rows[index].Delete();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Successfully');", true);
            DataView dv = dt.DefaultView;
            ViewState["tblstdcost"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void DDLCompGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComponentList();
        }

        protected void btnMerge_Click(object sender, EventArgs e)
        {
            //ViewState.Remove("tblstdcost");
            string comcod = this.GetCompCode();
            var list = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)(ViewState["tblinquery"]);

            string processcode = this.ddlmatdept.SelectedValue.ToString() + "%";
            string buyerid = "";
            if (this.ChkBrandAnalysis.Checked == true)
            {
                buyerid = this.ddlBrand.SelectedValue.ToString();

            }
            else
            {
                buyerid = list[0].buyer.ToString();

            }

            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_ANALYSIS", processcode, buyerid, "", "", "");
            if (ds4 == null)
            {
                //this.gvCost.DataSource = null;
                //this.gvCost.DataBind();
                return;
            }

            var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGroup>)ViewState["lstgroup"];

            DataTable tbl2 = (DataTable)ViewState["tblstdcost"];
            for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
            {
                DataRow[] dr = tbl2.Select("compcode='" + ds4.Tables[0].Rows[i]["compcode"] + "' and  rescode='" + ds4.Tables[0].Rows[i]["rescode"] + "' and spcfcode='" + ds4.Tables[0].Rows[i]["spcfcode"] + "'");

                if (dr.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ds4.Tables[0].Rows[i]["resdesc"].ToString() + " Already Added');", true);


                    continue;
                }
                else
                {

                    DataRow dr1 = tbl2.NewRow();
                    dr1["grpid"] = 0;
                    dr1["rescode"] = ds4.Tables[0].Rows[i]["rescode"];
                    dr1["resdesc"] = ds4.Tables[0].Rows[i]["resdesc"];
                    dr1["resunit"] = ds4.Tables[0].Rows[i]["resunit"];
                    dr1["preqty"] = ds4.Tables[0].Rows[i]["qty"];
                    dr1["qty"] = ds4.Tables[0].Rows[i]["qty"];
                    dr1["westpc"] = ds4.Tables[0].Rows[i]["westpc"];
                    dr1["conqty"] = ds4.Tables[0].Rows[i]["qty"];
                    dr1["issueqty"] = 0;
                    dr1["rate"] = ds4.Tables[0].Rows[i]["rate"];
                    dr1["amt"] = 0;
                    dr1["convrate"] = 0;
                    dr1["percnt"] = 0;
                    dr1["spcfcode"] = ds4.Tables[0].Rows[i]["spcfcode"];
                    dr1["spcfdesc"] = ds4.Tables[0].Rows[i]["spcfdesc"].ToString();
                    dr1["cfrate"] = ds4.Tables[0].Rows[i]["cfrate"];
                    dr1["fcfrate"] = (Convert.ToDouble(ds4.Tables[0].Rows[i]["cfrate"]) * Convert.ToDouble(ds4.Tables[0].Rows[i]["rate"]) / 100) + Convert.ToDouble(ds4.Tables[0].Rows[i]["rate"]);
                    // (((DataTable)ViewState["tblresRes"]).Select("sircode='" + ds4.Tables[0].Rows[i]["rescode"] + "' "))[0]["rate"].ToString();
                    dr1["compcode"] = ds4.Tables[0].Rows[i]["compcode"];
                    dr1["compdesc"] = ds4.Tables[0].Rows[i]["compname"];
                    dr1["compgrp"] = ds4.Tables[0].Rows[i]["compgrp"];
                    dr1["cmpgrpdsc"] = ds4.Tables[0].Rows[i]["compgrpdesc"];
                    tbl2.Rows.Add(dr1);
                }
            }
            DataView dv = tbl2.DefaultView;
            dv.Sort = ("grpid,compgrp, rescode");
            ViewState["tblstdcost"] = dv.ToTable();
            this.Data_Bind();

        }

        protected void NeedImportCheck_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox needimport = (CheckBox)sender;
            if (needimport.Checked)
            {
                this.ddlmatdept.Visible = true;
                this.btnMerge.Visible = true;
                this.LblImprDept.Visible = true;
                this.ChkBrandAnalysis.Visible = true;
            }
            else
            {
                this.ddlmatdept.Visible = false;
                this.btnMerge.Visible = false;
                this.LblImprDept.Visible = false;
                this.ChkBrandAnalysis.Visible = false;
            }
        }

        protected void DdlCurList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string fcode = this.ddlCurList.SelectedValue.ToString();
                string tcode = this.DdlCurList1.SelectedValue.ToString();

                string type = this.Request.QueryString["Type"].ToString();

                if (type == "PreCosting")
                {
                    DataTable curinf = (DataTable)ViewState["tblcurinfo"];

                    if (curinf.Rows.Count > 0)
                    {
                        curinf.Rows[0]["curcoda"] = this.DdlCurList1.SelectedValue.ToString();
                    }

                    ViewState["tblcurinfo"] = curinf;

                }

                if (tcode == "000" || fcode == "000")
                    return;
                List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];
                double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;
                this.txtExrate1.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
                this.txtConversion1.Text = this.DdlCurList1.SelectedItem.ToString();

                double cost = Convert.ToDouble("0" + this.txtEstimated.Text.Trim());
                this.TxtCostCur1.Text = (method * cost).ToString();

                //UPDATED BY AREFIN 12/2/2023
                

                if (type == "PreCosting")
                {
                    DataTable curinf = (DataTable)ViewState["tblcurinfo"];

                    if (curinf.Rows.Count > 0)
                    {
                        curinf.Rows[0]["exratea"] = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
                    }

                    ViewState["tblcurinfo"] = curinf;

                }
            }
            catch (Exception ex)
            {

            }          
        }

        protected void DdlCurList2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fcode = this.ddlCurList.SelectedValue.ToString();
            string tcode = this.DdlCurList2.SelectedValue.ToString();
            if (tcode == "000" || fcode == "000")
                return;
            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];
            double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;
            this.txtExrate2.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
            this.txtConversion2.Text = this.DdlCurList2.SelectedItem.ToString();

            double cost = Convert.ToDouble("0" + this.txtEstimated.Text.Trim());

            this.TxtCostCur2.Text = (method * cost).ToString();


        }

        protected void ibtnPreList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string srchsam = "";
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETPRE_INQUIRY_NO", curdate, srchsam, "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);


                return;
            }
            this.ddlPreList.DataTextField = "inqno1";
            this.ddlPreList.DataValueField = "sampleid";
            this.ddlPreList.DataSource = ds2.Tables[0];
            this.ddlPreList.DataBind();
            ds2.Dispose();
        }

        protected void LbtnMatGrpImport_Click(object sender, EventArgs e)
        {
            //ViewState.Remove("tblstdcost");
            string comcod = this.GetCompCode();

            string buyerid = this.ddlResourcesCost.SelectedValue.ToString();
            string processcode = this.DdlSpecres.SelectedValue.ToString();

            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_ANALYSIS", processcode, buyerid, "", "", "");
            if (ds4 == null)
            {
                //this.gvCost.DataSource = null;
                //this.gvCost.DataBind();
                return;
            }
            if (ds4.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Pre-analysis Not Found');", true);

                return;
            }

            var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGroup>)ViewState["lstgroup"];

            DataTable tbl2 = (DataTable)ViewState["tblstdcost"];
            for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
            {
                DataRow[] dr = tbl2.Select("compcode='" + ds4.Tables[0].Rows[i]["compcode"] + "' and  rescode='" + ds4.Tables[0].Rows[i]["rescode"] + "' and spcfcode='" + ds4.Tables[0].Rows[i]["spcfcode"] + "'");

                if (dr.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ds4.Tables[0].Rows[i]["resdesc"].ToString() + " Already Added');", true);


                    continue;
                }
                else
                {

                    DataRow dr1 = tbl2.NewRow();
                    dr1["grpid"] = 0;
                    dr1["rescode"] = ds4.Tables[0].Rows[i]["rescode"];
                    dr1["resdesc"] = ds4.Tables[0].Rows[i]["resdesc"];
                    dr1["resunit"] = ds4.Tables[0].Rows[i]["resunit"];// (((DataTable)ViewState["tblresRes"]).Select("sircode='" + ds4.Tables[0].Rows[i]["rescode"] + "'"))[0]["sirunit"].ToString();
                    dr1["preqty"] = ds4.Tables[0].Rows[i]["conqty"];
                    dr1["qty"] = ds4.Tables[0].Rows[i]["conqty"];
                    dr1["westpc"] = ds4.Tables[0].Rows[i]["westpc"];
                    dr1["conqty"] = ds4.Tables[0].Rows[i]["qty"];
                    dr1["issueqty"] = 0;
                    dr1["rate"] = ds4.Tables[0].Rows[i]["rate"]; // (((DataTable)ViewState["tblresRes"]).Select("sircode='" + ds4.Tables[0].Rows[i]["rescode"] + "'"))[0]["rate"].ToString();
                    dr1["amt"] = 0;
                    dr1["convrate"] = 0;
                    dr1["percnt"] = 0;
                    dr1["spcfcode"] = ds4.Tables[0].Rows[i]["spcfcode"];
                    dr1["spcfdesc"] = ds4.Tables[0].Rows[i]["spcfdesc"].ToString();
                    dr1["cfrate"] = ds4.Tables[0].Rows[i]["cfrate"].ToString();// (((DataTable)ViewState["tblresRes"]).Select("sircode='" + ds4.Tables[0].Rows[i]["rescode"] + "' "))[0]["cfrate"].ToString();
                    dr1["fcfrate"] = ds4.Tables[0].Rows[i]["rate"];// (((DataTable)ViewState["tblresRes"]).Select("sircode='" + ds4.Tables[0].Rows[i]["rescode"] + "' "))[0]["rate"].ToString();
                    dr1["compcode"] = ds4.Tables[0].Rows[i]["compcode"];
                    dr1["compdesc"] = ds4.Tables[0].Rows[i]["compname"];
                    dr1["compgrp"] = ds4.Tables[0].Rows[i]["compgrp"];
                    dr1["cmpgrpdsc"] = ds4.Tables[0].Rows[i]["compgrpdesc"];
                    tbl2.Rows.Add(dr1);
                }
            }
            DataView dv = tbl2.DefaultView;
            dv.Sort = ("grpid,compgrp, rescode");
            ViewState["tblstdcost"] = dv.ToTable();
            this.Data_Bind();
        }


        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "1":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                default:
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
            }
        }

        protected void LbtnSyncPrice_Click(object sender, EventArgs e)
        {
            string inqno = this.ddlinqno.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "MATERIAL_PRICE_SYNC", inqno, "", "", "", "", "", "");
            if (result == true)
            {
                ShowConsump();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Material Price Sync Success');", true);


                return;
            }
        }

        protected void chkheadl_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvCost.Rows.Count; i++)
            {
                if (((CheckBox)this.gvCost.HeaderRow.FindControl("chkhead")).Checked)
                {
                    ((CheckBox)this.gvCost.Rows[i].FindControl("chkCol")).Checked = true;
                }
                else
                {
                    ((CheckBox)this.gvCost.Rows[i].FindControl("chkCol")).Checked = false;
                }
            }
        }


        protected void LbtnSyncWestPc_Click(object sender, EventArgs e)
        {
            string inqno = this.ddlinqno.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "MATERIAL_WESTPC_SYNC", inqno, "", "", "", "", "", "");
            if (result == true)
            {
                ShowConsump();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Material Allowance Sync Success');", true);


                return;
            }
        }

        protected void ChkBrandAnalysis_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox needimport = (CheckBox)sender;
            if (needimport.Checked)
            {
                string comcod = this.GetCompCode();
                this.ddlBrand.Visible = true;
                DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BRAND_NAME", "", "", "", "", "", "", "", "", "");

                ddlBrand.DataTextField = "gdesc";
                ddlBrand.DataValueField = "gcod";
                ddlBrand.DataSource = ds2.Tables[0];
                ddlBrand.DataBind();
            }
            else
            {

                this.ddlBrand.Visible = false;
                ddlBrand.DataSource = null;
                ddlBrand.DataBind();
            }
        }

        protected void LbtngvCostfootIssue_Click(object sender, EventArgs e)
        {
            GridView gv = (GridView)this.gvCost;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                ((TextBox)gv.Rows[i].FindControl("txtgvconqty")).Text = ((TextBox)gv.Rows[i].FindControl("txtgvPreqty")).Text.ToString();
            }

        }

        protected void LbtnSyncCnF_Click(object sender, EventArgs e)
        {
            string inqno = this.ddlinqno.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "MATERIAL_CANDF_SYNC", inqno, "", "", "", "", "", "");
            if (result == true)
            {
                ShowConsump();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Material C&F Sync Successful');", true);

                return;
            }
        }

        protected void LbtnSyncAmount_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            var lst = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)(ViewState["tblinquery"]);
            string forma = lst[0].shoetype;
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_FORMA_ANALYSIS", forma, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable tblcomncost = (DataTable)ViewState["tblcomncost"];

            for (int i = 0; tblcomncost.Rows.Count > i; i++)
            {
                DataView dv = ds1.Tables[0].Copy().DefaultView;
                dv.RowFilter = "fcode = '" + tblcomncost.Rows[i]["sircode"].ToString() + "'";
                DataTable dt = dv.ToTable();
                if (dv.ToTable().Rows.Count > 0)
                {
                    tblcomncost.Rows[i]["stdamt"] = dt.Rows[0]["s2"];
                    tblcomncost.Rows[i]["percnt"] = dt.Rows[0]["s1"];
                }

            }

            ViewState["tblcomncost"] = tblcomncost;
            this.Data_Bind();
        }

        protected void lbtngvChangeComponent_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            LinkButton lnkbtncomponent = (LinkButton)sender;
            string compcode = lnkbtncomponent.CommandArgument.ToString();

            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sircode = ((Label)this.gvCost.Rows[index].FindControl("lblgvcodeCost")).Text.ToString();
            string spcfcode = ((Label)this.gvCost.Rows[index].FindControl("lblgvspcfcode")).Text.ToString();
            this.lblHelperComp.Text = sircode + spcfcode + compcode;

            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETCOMPONENTLIST", "", "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;
            mdlDdlComponent.DataTextField = "resdesc";
            mdlDdlComponent.DataValueField = "rescode";
            mdlDdlComponent.DataSource = ds2;
            mdlDdlComponent.DataBind();
            mdlDdlComponent.SelectedValue = compcode;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModalCompChange();", true);
        }

        protected void lnkbtnUpdateComponent_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string comcod = this.GetCompCode();
            string sampid = this.ddlinqno.SelectedValue.ToString();
            string sircode = this.lblHelperComp.Text.ToString().Substring(0, 12);
            string spcfcode = this.lblHelperComp.Text.ToString().Substring(12, 12);
            string oldcompcode = this.lblHelperComp.Text.ToString().Substring(24);
            string newcompcode = this.mdlDdlComponent.SelectedValue.ToString();
            string newcompdesc = this.mdlDdlComponent.SelectedItem.Text.Trim();


            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "CHANGE_COMP_FOR_CONSUMPTION", oldcompcode, newcompcode, sampid, sircode, spcfcode);
            if (result == true)
            {
                DataRow[] dr1 = dt.Select("compcode='" + oldcompcode + "' and rescode='" + sircode + "' and spcfcode='" + spcfcode + "'");
                dr1[0]["compcode"] = newcompcode;
                dr1[0]["compdesc"] = newcompdesc;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Component Change Successfully');", true);

            }
            ViewState["tblstdcost"] = dt;
            this.Data_Bind();
        }

        protected void LbtnCopy0_Click(object sender, EventArgs e)
        {
            string frinqno = this.ddlPreList.SelectedValue.ToString();
            string toinqno = this.Request.QueryString["genno"].ToString();
            string comcod = this.GetCompCode();
            if (frinqno == toinqno)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('You Select Same Style');", true);

                return;
            }
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GET_SDIWISE_COMPONENT_GROUP", frinqno);
            this.gvCompGrp.DataSource = ds2;
            this.gvCompGrp.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModalCompGroup();", true);
        }


        protected void lkbtnCopyComp_Click(object sender, EventArgs e)
        {
            string compgrplist = "";
            string comcod = this.GetCompCode();
            for (int i = 0; i < this.gvCompGrp.Rows.Count; i++)
            {
                if (((CheckBox)this.gvCompGrp.Rows[i].FindControl("chkCol")).Checked)
                {
                    compgrplist += ((Label)this.gvCompGrp.Rows[i].FindControl("lblgvCompcode")).Text.Trim();
                }
            }


            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string frinqno = this.ddlPreList.SelectedValue.ToString();

            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETCONSUMPTIONINFO", frinqno, compgrplist, "", "", "", "", "", "");
            if (ds4 == null)
            {

                return;
            }

            foreach (DataRow dr in ds4.Tables[0].Rows)
            {

                DataRow[] dr1 = dt.Select("compcode='" + dr["compcode"] + "' and  rescode='" + dr["rescode"] + "' and spcfcode='" + dr["spcfcode"] + "'");

                if (dr1.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + dr["resdesc"].ToString() + " Already Added');", true);


                    continue;
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        dr["pgaprvbyid"] = dt.Rows[0]["pgaprvbyid"].ToString();
                        dr["sampleid"] = dt.Rows[0]["sampleid"].ToString();
                        dr["inqno"] = dt.Rows[0]["inqno"].ToString();
                        dr["inqno1"] = dt.Rows[0]["inqno1"].ToString();
                    }
                    dt.ImportRow(dr);

                }

            }

            ViewState["tblstdcost"] = dt;


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Loaded Please Save for Copy');", true);

            this.Data_Bind();



        }

        protected void lnkbtnDltCrs_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string comcod = this.GetCompCode();
            int index = ((GridViewRow)(((LinkButton)sender).NamingContainer)).RowIndex;
            string sampleid = this.ddlinqno.SelectedValue.ToString();
            string inqno = this.ddlinqno.SelectedValue.ToString();

            string main_compgrp = dt.Rows[index]["compgrp"].ToString();

            bool flag = true;

            for (int i=0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["compgrp"].ToString() == main_compgrp)
                {
                    string grp = dt.Rows[i]["grp"].ToString();
                    string rescode = dt.Rows[i]["rescode"].ToString();
                    string spcfcode = dt.Rows[i]["spcfcode"].ToString();
                    string compgrp = dt.Rows[i]["compcode"].ToString();

                    bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "DELETECONQUANTITY", sampleid, grp, rescode, spcfcode, inqno, compgrp);

                    if (!result)
                    {
                        flag = false;
                        return;
                    }
                }
            }


            if (!flag)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Merdata.ErrorObject["Msg"].ToString() + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Successfully');", true);
            }

            ShowConsump();
        }
    }
}