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
using SPELIB;
using SPERDLC;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_11_RawInv
{
    public partial class PBMatIssueSingle : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (prevPage.Length == 0)
                //{
                //    prevPage = Request.UrlReferrer.ToString();
                //}
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                //string Date = this.Request.QueryString["genno"].ToString();
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCodeBookList();
                this.GetOrderNumber();
                this.imgbtnStorid_Click(null, null);
                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIAL ISSUE INFORMATION";

                this.CommonButton();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnFinalProUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Click += new EventHandler(lnkbtnHisprice_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_DataBind();
        }

        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //     ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ////((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Calculation";

            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Text = "Delete Selected Item";
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).OnClientClick = "return confirm('Do you want to Remove Selected Item?')";
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).CssClass = "btn btn-info btn-sm";


        }

        private void lnkbtnHisprice_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gvMatIssue.Rows.Count; i++)
            {
                DataTable dt1 = (DataTable)ViewState["tblMatIssue"];
                DataView dv = dt1.DefaultView;
                string rsircode = ((Label)this.gvMatIssue.Rows[i].FindControl("lblrsircode")).Text.ToString();
                string spcfcod = ((Label)this.gvMatIssue.Rows[i].FindControl("lblspcfcod")).Text.ToString();
                if (((CheckBox)this.gvMatIssue.Rows[i].FindControl("chkCol")).Checked)
                {
                    dv.RowFilter = ("rsircode+spcfcod <>'" + rsircode + spcfcod + "'");
                    ViewState["tblMatIssue"] = dv.ToTable();
                }

            }
            this.Data_DataBind();
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private void GetOrderNumber()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string qactcode = this.Request.QueryString["actcode"] ?? "";
            //string qgenno= this.Request.QueryString["genno"] ?? "";
           // qactcode = (qactcode.Length > 12) ? qactcode.Substring(0, 12) : qactcode;
            string serch1 = (qactcode.Length > 0 ? qactcode : "%" + this.txtSrcPro.Text.Trim()) + "%";




            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETORDNOFORSAMANDORDER", serch1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlStoreName.DataTextField = "actdesc";
            this.ddlStoreName.DataValueField = "actcode";
            this.ddlStoreName.DataSource = ds1.Tables[0];
            this.ddlStoreName.DataBind();
            if (qactcode.Length > 0)
            {
                // this.ddlStoreName.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.lbtnOk_Click(null, null);
            }
            //ViewState["tblStoreType"] = ds1.Tables[0];
        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetOrderNumber();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comaddress = hst["comadd1"].ToString();
            string comcod = hst["comcod"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string issueno = Convert.ToString(dt1.Rows[0]["isueno"]).ToString();
            string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string issueno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();


            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "GET_DATA_MAT_ISSUE_NO_WISE", issueno, "", "", "", "", "");
            ViewState["tblMatissuDetailssingle"] = ds1.Tables[0];
            ViewState["tblMatissusummarysingle"] = ds1.Tables[1];
            DataTable dt = (DataTable)ViewState["tblMatissuDetailssingle"];
            DataTable dt1 = (DataTable)ViewState["tblMatissusummarysingle"];

            string orderno = Convert.ToString(dt1.Rows[0]["orderno"]).ToString();
            string orderdesc = Convert.ToString(dt1.Rows[0]["orderdesc"]).ToString();
            string isuedate = Convert.ToDateTime(dt1.Rows[0]["isuedate"]).ToString("dd-MMM-yyyy");
            string title = "Issue No Wise Material Details";


            var list = dt.DataTableToList<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matsummaryreport>();
            // var list2 = dt1.DataTableToList<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matdetailsreport>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptDateWiseMaterialShowIssuewise", list, null, null);
            Rpt1.SetParameters(new ReportParameter("comname", comname));
            Rpt1.SetParameters(new ReportParameter("comaddress", comaddress));
            Rpt1.SetParameters(new ReportParameter("isuedate", isuedate));
            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("issueno", issueno));
            Rpt1.SetParameters(new ReportParameter("orderno", orderno));
            Rpt1.SetParameters(new ReportParameter("orderdesc", orderdesc));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
           
        }

        //protected void lbtnPrint_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
        //    string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
        //    DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETPURISUINFO", misuno, misudate,
        //            "", "", "", "", "", "", "");
        //    if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
        //        return;
        //    string session = hst["session"].ToString();
        //    string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
        //    DataTable dt = (DataTable)ViewState["tblMatIssue"];
        //    var rptlist = dt.DataTableToList<SPEENTITY.C_11_RawInv.EclassMaterialIssue>();
        //    LocalReport Rpt1a = new LocalReport();

        //    Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_11_RawInv.RptMaterialIssue", rptlist, null, null);
        //    Rpt1a.EnableExternalImages = true;
        //    Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
        //    Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
        //    Rpt1a.SetParameters(new ReportParameter("rptitle", "Material Issue"));
        //    Rpt1a.SetParameters(new ReportParameter("isuno", misuno));
        //    Rpt1a.SetParameters(new ReportParameter("isudat", misudate));
        //    Rpt1a.SetParameters(new ReportParameter("isuref", ds1.Tables[1].Rows[0]["misuref"].ToString()));
        //    Rpt1a.SetParameters(new ReportParameter("rmrks", ds1.Tables[1].Rows[0]["rmrks"].ToString()));
        //    Rpt1a.SetParameters(new ReportParameter("postedbyname", ds1.Tables[1].Rows[0]["postedbyname"].ToString()));
        //    Rpt1a.SetParameters(new ReportParameter("aprvbyname", ds1.Tables[1].Rows[0]["postedbyid"].ToString()));
        //    Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

        //    Session["Report1"] = Rpt1a;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
        //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}


        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                this.lblStoredesc.Text = this.ddlStoreName.SelectedItem.Text.Trim();
                this.ddlStoreName.Visible = false;
                this.lblStoredesc.Visible = true;
                this.PnlProRemarks.Visible = true;
                //this.lblPreList.Visible = false;
                //this.txtSrcIssueNo.Visible = false;
                // this.ddlStore.Enabled = false;
                this.ibtnPreIssueList.Visible = false;
                this.ddlPrevIssueList.Visible = false;
                this.ddlpagesize.Visible = true;
                this.lblPage.Visible = true;
                this.txtCurDate.Enabled = true;
                if (ddlPrevIssueList.Items.Count == 0)
                {
                    this.panel11.Visible = true;
                }
                else
                {
                    this.lblReqList.Visible = false;
                    //this.txtResSearch.Visible = false;
                    //this.ImgbtnFindMatList.Visible = false;
                    this.ddlReqList.Visible = false;
                    this.lbtnSelectReqList.Visible = false;
                    //this.ImgbtnFindReqList.Visible = false;
                    this.panel11.Visible = true;
                }
                //this.lblmsg.Text = "";
                this.ShowIsuInfo();
                this.GetUnitName();
                this.ImgbtnFindReqList_Click(null, null);

                return;
            }


            this.lbtnOk.Text = "Ok";
            this.panel11.Visible = false;
            this.lblStoredesc.Text = "";
            this.txtRemarks.Text = "";

            this.txtlSuRefNo.Text = "";
            this.lblCurNo1.Text = "";
            this.lblCurNo2.Text = "";
            this.ddlPrevIssueList.Items.Clear();
            this.ddlStoreName.Visible = true;
            this.lblStoredesc.Visible = false;
            //this.lblPreList.Visible = true;
            //this.txtSrcIssueNo.Visible = true;
            this.ibtnPreIssueList.Visible = true;
            this.ddlPrevIssueList.Visible = true;
            this.PnlProRemarks.Visible = false;
            this.gvMatIssue.DataSource = null;
            this.ddlpagesize.Visible = false;
            this.lblPage.Visible = false;
            this.txtCurDate.Enabled = true;
            this.gvMatIssue.DataBind();
            this.ddlReqList.Items.Clear();
            this.lblReqList.Visible = true;
            //this.txtResSearch.Visible = true;
            //this.ImgbtnFindMatList.Visible = true;
            this.ddlReqList.Visible = true;
            //this.ImgbtnFindReqList.Visible = true;
            this.lbtnSelectReqList.Visible = true;
            this.ddlMatList.Items.Clear();
            ViewState.Remove("tblMatIssue");
        }

        private void ShowIsuInfo()
        {

            string comcod = this.GetComeCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString();
            string mReqNo = "NEWREQ";
           

            if (this.ddlPrevIssueList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mReqNo = this.ddlPrevIssueList.SelectedValue.ToString();
            }
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETPURISUINFO", mReqNo, CurDate1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblMatIssue"] = HiddenSameData(ds1.Tables[0]);
        
            Session["tblUserReq"] = ds1.Tables[1];

            //if (Request.QueryString["InputType"].ToString() == "Approval" || Request.QueryString["InputType"].ToString() == "FxtAstApproval")
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        this.GetApprQty();
            //    }
            //}

            if (mReqNo == "NEWREQ")
            {
                ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETISSUENO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.txtlSuRefNo.Text = ds1.Tables[1].Rows[0]["misuref"].ToString();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["misuno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["misuno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["misudate"]).ToString("dd-MMM-yyyy");
            this.ddlStoreName.SelectedValue = ds1.Tables[1].Rows[0]["actcode"].ToString();
            // this.ddlStore.SelectedValue = (ds1.Tables[1].Rows[0]["storid"].ToString() != "") ? ds1.Tables[1].Rows[0]["storid"].ToString() : "000000000000";
            this.lblStoredesc.Text = this.ddlStoreName.SelectedItem.Text.Trim();
            this.txtRemarks.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();
            this.Data_DataBind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string preqno = dt1.Rows[0]["preqno"].ToString();
            string bactcode = dt1.Rows[0]["bactcode"].ToString();
            string procode = dt1.Rows[0]["procode"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode && dt1.Rows[j]["preqno"].ToString() == preqno && dt1.Rows[j]["bactcode"].ToString() == bactcode && dt1.Rows[j]["procode"].ToString() == procode)
                {
                    preqno = dt1.Rows[j]["preqno"].ToString();
                    bactcode = dt1.Rows[j]["bactcode"].ToString();
                    procode = dt1.Rows[j]["procode"].ToString();
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["preqno1"] = "";
                    dt1.Rows[j]["bactdesc"] = "";
                    dt1.Rows[j]["prodesc"] = "";
                    dt1.Rows[j]["actdesc"] = "";
                }

                else
                {
                    preqno = dt1.Rows[j]["preqno"].ToString();
                    bactcode = dt1.Rows[j]["bactcode"].ToString();
                    procode = dt1.Rows[j]["procode"].ToString();
                }

            }

            return dt1;
        }

        private void Data_DataBind()
        {
            DataTable dt = (DataTable)ViewState["tblMatIssue"];
            this.gvMatIssue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatIssue.DataSource = dt;
            this.gvMatIssue.DataBind();
            ((Label)this.gvMatIssue.FooterRow.FindControl("gvLblTtlIssuqty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(conqty)", "")) ? 0 : 
                                                                                      dt.Compute("sum(conqty)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvMatIssue.FooterRow.FindControl("gvLblTtlConvqty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuqty)", "")) ? 0 : 
                                                                                      dt.Compute("sum(isuqty)", ""))).ToString("#,##0.0000;(#,##0.0000); ");

        }
        private void Stockout_Bind()
        {

            this.gvStockAlert.DataSource = (DataTable)ViewState["tblstockout"];
            this.gvStockAlert.DataBind();
        }
        protected void lnkselect_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblMatIssue"];
            int rowindex;
            for (int i = 0; i < this.gvMatIssue.Rows.Count; i++)
            {
                string untcod = dt.Rows[i]["untcod"].ToString();
                string conunt = dt.Rows[i]["conunt"].ToString();
                DataTable tblunit = (DataTable)ViewState["UnitsRate"];
                double conrate = 0;
                if (untcod != "21035" || conunt != "conunt")
                {
                    DataRow[] unitrow = tblunit.Select("bcod = '" + untcod + "' and ccod='" + conunt + "'");

                    if (unitrow.Count() == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Make Sure Unit Conversion');", true);


                        //((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = false;
                        return;
                    }
                    conrate = Convert.ToDouble(unitrow[0]["conrat"]);

                }


                double conqty = Convert.ToDouble("0" + ((TextBox)this.gvMatIssue.Rows[i].FindControl("txtgvconqty")).Text.Trim());
                double isuqty = Convert.ToDouble("0" + ((TextBox)this.gvMatIssue.Rows[i].FindControl("lgvIsuQty")).Text.Trim());

               

                //double isurate = Convert.ToDouble("0" + ((Label)this.gvMatIssue.Rows[i].FindControl("LblIsuReate")).Text.Trim());

                string isurate= ((Label)this.gvMatIssue.Rows[i].FindControl("LblIsuReate")).Text.Trim();

                if (!double.TryParse(isurate, out double isurateText))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Sorry!! Issue Rate is negative!');", true);
                    return;
                }

                if (isurateText < 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Sorry!! Issue Rate is negative!');", true);
                    return;
                }


                double isueqty = (conrate == 0) ? conqty : (Convert.ToDouble(conqty) / conrate);

                rowindex = (this.gvMatIssue.PageIndex) * this.gvMatIssue.PageSize + i;
                dt.Rows[rowindex]["conqty"] = conqty;
                // dt.Rows[rowindex]["balqty"] = Convert.ToDouble("0"+dt.Rows[rowindex]["balqty"])-conqty;
                dt.Rows[rowindex]["isuqty"] = isueqty;
                dt.Rows[rowindex]["isurate"] = isurate;
            }
            //  ((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = true;
            ViewState["tblMatIssue"] = dt;
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_DataBind();

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {

        }

        protected void ibtnPreIssueList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string CurDate1 = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETPREVMISSUELIST", CurDate1, "Normal", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevIssueList.Items.Clear();
            this.ddlPrevIssueList.DataTextField = "misuno1";
            this.ddlPrevIssueList.DataValueField = "misuno";
            this.ddlPrevIssueList.DataSource = ds1.Tables[0];
            this.ddlPrevIssueList.DataBind();
        }

        protected void lbtnSelectReqList_Click(object sender, EventArgs e)
        {

            string mReqno = this.ddlReqList.SelectedValue.ToString();
            string matcod = this.ddlMatList.SelectedValue.ToString();
            string spcfcod = this.ddlspcflist.SelectedValue.ToString();
            string conunit = this.ddlunit.SelectedValue.ToString();
            string process = this.ddlprocess.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)ViewState["tblMatIssue"];
            DataTable dt2 = (DataTable)ViewState["tblMatISu"];
            DataTable tblunit = (DataTable)ViewState["UnitsRate"];
            DataRow[] dr2 = dt2.Select("preqno='" + mReqno + "'");
            DataView dv = dt2.DefaultView;
            dt2 = dv.ToTable();
            DataRow[] dr3 = dt2.Select("preqno='" + mReqno + "' and rsircode='" + matcod + "' and spcfcod='" + spcfcod + "' and procode='" + process + "'");

            DataRow[] dr4 = tbl1.Select("preqno='" + mReqno + "' and rsircode='" + matcod + "' and spcfcod='" + spcfcod + "' and procode='" + process + "'");
            if (dr4.Length > 0)
            {
                if ( dr4[0]["rsircode"].ToString() == matcod && dr4[0]["spcfcod"].ToString() == spcfcod)
                {
                    return;
                }
            }
            else
            {

                DataRow[] unitrow = tblunit.Select("bcod = '" + dr3[0]["untcode"].ToString() + "' and ccod='" + conunit + "'");

                DataRow dr1 = tbl1.NewRow();
                dr1["preqno"] = dr3[0]["preqno"].ToString();
                dr1["preqno1"] = dr3[0]["preqno1"].ToString();
                dr1["bactcode"] = dr3[0]["bactcode"].ToString();
                dr1["bactdesc"] = dr3[0]["bactdesc"].ToString();
                dr1["procode"] = dr3[0]["procode"].ToString();
                dr1["prodesc"] = dr3[0]["prodesc"].ToString();
                dr1["rsircode"] = dr3[0]["rsircode"].ToString();
                dr1["rsirdesc"] = dr3[0]["rsirdesc"].ToString();
                dr1["spcfcod"] = dr3[0]["spcfcod"].ToString();
                dr1["spcfdesc"] = dr3[0]["spcfdesc"].ToString();
                dr1["rsirunit"] = dr3[0]["rsirunit"].ToString();
                dr1["untcod"] = dr3[0]["untcode"].ToString();
                dr1["conunt"] = conunit;
                dr1["conqty"] = (unitrow.Count() == 0) ? dr3[0]["isuqty"].ToString() : (Convert.ToDouble(dr3[0]["isuqty"]) * Convert.ToDouble(unitrow[0]["conrat"])).ToString();
                dr1["conuntdesc"] = this.ddlunit.SelectedItem.ToString();
                dr1["consppair"] = dr3[0]["consppair"].ToString();
                dr1["balqty"] = dr3[0]["balqty"].ToString();
                dr1["isuqty"] = dr3[0]["isuqty"].ToString();
                dr1["isurate"] = dr3[0]["isurate"].ToString();
                dr1["stockqty"] = dr3[0]["stockqty"].ToString();
                dr1["balstkqty"] = dr3[0]["balstkqty"].ToString();
                dr1["actcode"] = dr3[0]["actcode"].ToString();
                dr1["actdesc"] = dr3[0]["actdesc"].ToString();
                dr1["reqqty"] = dr3[0]["acqty"].ToString();
                tbl1.Rows.Add(dr1);
            }

            ViewState["tblMatIssue"] = tbl1;
            ViewState["tblMatIssue"] = HiddenSameData((DataTable)ViewState["tblMatIssue"]);
            this.Data_DataBind();

            //Session["tblMatIssue"] = HiddenSameData(tbl1);

            //this.Data_DataBind();
        }
        protected void gvMatIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvMatIssue.PageIndex = e.NewPageIndex;
            this.Data_DataBind();

        }
        protected void ImgbtnFindReqList_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblMatISu");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string iStore = this.ddlStoreName.SelectedValue.ToString();
            string iDate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string mOrderNo = this.ddlOrderList.SelectedValue.ToString();
            string mSrchTxt = "%" + this.txtResSearch.Text.Trim() + "%";
            string mathead = this.ddlCodeBook.SelectedValue == "040000000000" ? "%%" : this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            string group = this.ddlGroup.SelectedValue == "040000000000" ? "%%" : this.ddlGroup.SelectedValue.ToString().Substring(0, 7) + "%";

            //DataTable dt = (DataTable)ViewState["tblStoreType"];
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("actcode='" + iStore+"'");
            //dt = dv.ToTable();
            //string Codetype = dt.Rows[0]["acttype"].ToString();
            string SearchInfo = "";
            //if (Codetype.Length > 0)
            //{

            //    int len;
            //    string[] ar = Codetype.Split('/');
            //    foreach (string ar1 in ar)
            //    {


            //        if (ar1.Contains("-"))
            //        {
            //            len = ar1.IndexOf("-");
            //            SearchInfo = SearchInfo + "left(rsircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
            //        }
            //        else
            //        {
            //            len = ar1.Length;

            //            SearchInfo = SearchInfo + "left(rsircode,'" + len + "')" + " = " + ar1 + " ";
            //        }
            //        SearchInfo = SearchInfo + " or ";

            //    }
            //    if (SearchInfo.Length > 0)
            //        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            //}

            string Dprno = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" :
               ( this.Request.QueryString["genno"].ToString().Length>14)? this.Request.QueryString["genno"].ToString() : this.Request.QueryString["genno"].ToString() + "%";
            string reqtype = this.Request.QueryString["reptype"].ToString() + "%";
            string isunum = "";
            if (this.ddlPrevIssueList.Items.Count > 0)
            {
                isunum = this.ddlPrevIssueList.SelectedValue.ToString();
            }
            string procedure = "SP_ENTRY_LCMATISSUE";
            string calltype = "MATCODELIST";
            if(this.Request.QueryString["genno"].ToString().Length > 14)
            {
                procedure = "SP_ENTRY_PRODUCTION_INFO";
                calltype = "MATCODELIST_FOR_MULTIPLE_REQ_ISSUE";
            }
            //   string CallType = Dprno.Substring(0, 3) == "SDL" ? "MATCODELISTSAMPLING" : "MATCODELIST";
            DataSet ds1 = PurData.GetTransInfo(comcod, procedure, calltype, iStore, iDate, SearchInfo, Dprno, reqtype, isunum, mathead, group, "");
            if (ds1 == null)
                return;

            ViewState["tblMatISu"] = ds1.Tables[0];

            ViewState["tblstockout"] = ds1.Tables[2];
            ViewState["tblTriOrd"] = ds1.Tables[3];

            this.ddlReqList.DataTextField = "preqno1";
            this.ddlReqList.DataValueField = "preqno";
            this.ddlReqList.DataSource = ds1.Tables[1];
            this.ddlReqList.DataBind();
            this.ddlReqList_SelectedIndexChanged(null, null);
        }
        protected void GetPerMatIssu()
        {
            string comcod = this.GetComeCode();
            string mREQNO = "NEWISU";
            if (this.ddlPrevIssueList.Items.Count > 0)
                mREQNO = this.ddlPrevIssueList.SelectedValue.ToString();

            string mISUDAT = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString();
            if (mREQNO == "NEWISU")
            {
                DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETISSUENO", mISUDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxisuno"].ToString();
                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxisuno1"].ToString().Substring(6, 5);
                    this.ddlPrevIssueList.DataTextField = "maxisuno1";
                    this.ddlPrevIssueList.DataValueField = "maxisuno";
                    this.ddlPrevIssueList.DataSource = ds2.Tables[0];
                    this.ddlPrevIssueList.DataBind();
                }
            }
        }

        protected void lbtnFinalProUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                this.SaveValue();
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)ViewState["tblMatIssue"];
                if (ddlPrevIssueList.Items.Count == 0)
                {
                    this.GetPerMatIssu();
                }

                string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string actcode = this.ddlStoreName.SelectedValue.ToString();
                //   string storid = this.ddlStore.SelectedValue.ToString();

                string Remarks = this.txtRemarks.Text.Trim();
                string misuref = this.txtlSuRefNo.Text.Trim();
                bool result;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                DataTable dtuser = (DataTable)Session["tblUserReq"];
                string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
                string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
                string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
                string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["entrydat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
                string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");
                //string tblApprovtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
                //string tblApprovSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();

                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string PostedByid = (this.Request.QueryString["type"] == "Entry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
                string Posttrmid = (this.Request.QueryString["type"] == "Entry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
                string PostSession = (this.Request.QueryString["type"] == "Entry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
                string Posteddat = (this.Request.QueryString["type"] == "Entry") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
                string EditByid = (this.Request.QueryString["type"] == "Entry") ? "" : (tblEditByid == "") ? userid : tblEditByid;
                string Editdat = (this.Request.QueryString["type"] == "Entry") ? "01-Jan-1900" : (tblEditDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblEditDat;
                string reqtype = (this.Request.QueryString["reptype"].ToString().Length > 0) ? this.Request.QueryString["reptype"].ToString() : "Normal";

                result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "UPDATEPURMISSUEINFO", "MATISSUEB", misuno, misudate,
                      Remarks, misuref, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, reqtype, "", "");

                //result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "MATISSUEB", misuno, actcode, misudate,
                //      Remarks, misuref, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + PurData.ErrorObject["Msg"] + "');", true);

                    return;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double isurate = Convert.ToDouble(dt.Rows[i]["isurate"]);
                    string storeid = dt.Rows[i]["actcode"].ToString();
                    string preqno = dt.Rows[i]["preqno"].ToString();
                    double isuqty = Convert.ToDouble(dt.Rows[i]["isuqty"].ToString());
                    decimal isuamt = Convert.ToDecimal(isuqty * isurate);
                    string orderno = dt.Rows[i]["bactcode"].ToString();
                    string rsircode = dt.Rows[i]["rsircode"].ToString();
                    string spcfcod = dt.Rows[i]["spcfcod"].ToString();
                    string conqty = dt.Rows[i]["conqty"].ToString();
                    string conunt = dt.Rows[i]["conunt"].ToString();
                    string procode = dt.Rows[i]["procode"].ToString();
                    if (isuqty > 0)
                    {
                        result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "UPDATEPURMISSUEINFO", "MATISSUEA", misuno, preqno, orderno, rsircode, isuqty.ToString(), spcfcod, conunt, conqty.ToString(), storeid, procode, isuamt.ToString(), "", "");
                        if (!result)
                            return;
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Issue Updated');", true);
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);



            }
        }

        protected void ddlReqList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Session["tblMatISu"] = ds1.Tables[0];
            // Session.Remove("tblMatISu");
            // DataTable tbl1 = (DataTable)Session["tblMatIssue"];
            string mReqno = this.ddlReqList.SelectedValue.ToString();
            DataTable dt1 = (DataTable)ViewState["tblMatISu"];
            //DataRow[] dr2 = tbl1.Select("preqno='" + mReqno + "'");
            DataTable dt2 = new DataTable();
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("preqno='" + mReqno + "'");
            dt2 = dv.ToTable(true, "prodesc", "procode");
            dt2.Rows.Add("ALL PROCESS", "XXXXXXXXXXXX");
            // Session["tblMatIssue"] = dt2;
            this.ddlprocess.DataTextField = "prodesc";
            this.ddlprocess.DataValueField = "procode";
            this.ddlprocess.DataSource = dt2; // ds1.Tables[1];
            this.ddlprocess.DataBind();
            ddlprocess_SelectedIndexChanged(null, null);
        }
        protected void ImgbtnFindMatList_Click(object sender, EventArgs e)
        {

        }
        protected void lnkSelectAll_Click(object sender, EventArgs e)
        {

            string mReqno = this.ddlReqList.SelectedValue.ToString();
            string matcod = this.ddlMatList.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)ViewState["tblMatIssue"];
            string process = (this.ddlprocess.SelectedValue.ToString() == "XXXXXXXXXXXX") ? "%" : this.ddlprocess.SelectedValue.ToString() + "%";
            DataTable tblunit = (DataTable)ViewState["UnitsRate"];
            string conunit = this.ddlunit.SelectedValue.ToString();
            DataTable tbl2 = (DataTable)ViewState["tblMatISu"];
            DataRow[] dr3 = tbl2.Select("preqno='" + mReqno + "'");
            DataView dv = tbl2.DefaultView;
            dv.RowFilter = "procode like '" + process + "' ";
            tbl2 = dv.ToTable();


            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                DataRow[] dr2 = tbl1.Select("preqno='" + mReqno + "' and rsircode = '" + tbl2.Rows[i]["rsircode"].ToString() + "' and spcfcod='" + tbl2.Rows[i]["spcfcod"].ToString() + "' and procode='" + tbl2.Rows[i]["procode"].ToString() + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = tbl1.NewRow();
                    DataRow[] unitrow = tblunit.Select("bcod = '" + tbl2.Rows[i]["untcode"].ToString() + "' and ccod='" + conunit + "'");

                    dr1["preqno"] = tbl2.Rows[i]["preqno"].ToString();
                    dr1["preqno1"] = tbl2.Rows[i]["preqno1"].ToString();
                    dr1["bactcode"] = tbl2.Rows[i]["bactcode"].ToString();
                    dr1["bactdesc"] = tbl2.Rows[i]["bactdesc"].ToString();
                    dr1["procode"] = tbl2.Rows[i]["procode"].ToString();
                    dr1["prodesc"] = tbl2.Rows[i]["prodesc"].ToString();
                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                    dr1["rsirdesc"] = tbl2.Rows[i]["rsirdesc"].ToString();
                    dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();
                    dr1["spcfdesc"] = tbl2.Rows[i]["spcfdesc"].ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["conunt"] = tbl2.Rows[i]["untcode"].ToString();
                    dr1["conqty"] = Convert.ToDouble(tbl2.Rows[i]["isuqty"]).ToString();
                    dr1["consppair"] = tbl2.Rows[i]["consppair"].ToString();
                    dr1["conuntdesc"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["balqty"] = tbl2.Rows[i]["balqty"].ToString();
                    dr1["untcod"] = tbl2.Rows[i]["untcode"].ToString();
                    dr1["isuqty"] = tbl2.Rows[i]["isuqty"].ToString();
                    dr1["isurate"] = tbl2.Rows[i]["isurate"].ToString();
                    dr1["stockqty"] = tbl2.Rows[i]["stockqty"].ToString();
                    dr1["balstkqty"] = tbl2.Rows[i]["balstkqty"].ToString();
                    dr1["actcode"] = tbl2.Rows[i]["actcode"].ToString();
                    dr1["actdesc"] = tbl2.Rows[i]["actdesc"].ToString();
                    dr1["reqqty"] = tbl2.Rows[i]["acqty"].ToString();
                    tbl1.Rows.Add(dr1);

                }
            }
            ViewState["tblReq"] = HiddenSameData(tbl1);
            this.Data_DataBind();
            adjustment();

        }
        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(prevPage);
        //}

        protected void ddlprocess_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Session["tblMatISu"] = ds1.Tables[0];
            // Session.Remove("tblMatISu");

            // DataTable tbl1 = (DataTable)Session["tblMatIssue"];
            string mReqno = this.ddlReqList.SelectedValue.ToString();
            string process = (this.ddlprocess.SelectedValue.ToString() == "XXXXXXXXXXXX") ? "%" : this.ddlprocess.SelectedValue.ToString();
            DataTable dt1 = (DataTable)ViewState["tblMatISu"];
            //DataRow[] dr2 = tbl1.Select("preqno='" + mReqno + "'");
            DataTable dt2 = new DataTable();
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("preqno='" + mReqno + "' and procode like '" + process + "%'");
            //  dt2 = dv.ToTable();
            dt2 = dv.ToTable(true, "rsirdesc1", "rsircode");
            // Session["tblMatIssue"] = dt2;
            this.ddlMatList.DataTextField = "rsirdesc1";
            this.ddlMatList.DataValueField = "rsircode";
            this.ddlMatList.DataSource = dt2; // ds1.Tables[1];
            this.ddlMatList.DataBind();
            ddlMatList_SelectedIndexChanged(null, null);
        }

        protected void ddlMatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable matlist = (DataTable)ViewState["tblMatISu"];
            string mReqno = this.ddlReqList.SelectedValue.ToString();
            string process = (this.ddlprocess.SelectedValue.ToString() == "XXXXXXXXXXXX") ? "%" : this.ddlprocess.SelectedValue.ToString() + "%";
            string mResCode = this.ddlMatList.SelectedValue.ToString();
            //DataRow[] dr2 = tbl1.Select("preqno='" + mReqno + "'");
            DataTable dt2 = new DataTable();
            DataView dv2 = matlist.DefaultView;
            dv2.RowFilter = ("preqno='" + mReqno + "' and procode like '" + process + "' and rsircode='" + mResCode + "'");
            dt2 = dv2.ToTable(true, "spcfcod", "spcfdesc");
            // Session["tblMatIssue"] = dt2;
            this.ddlspcflist.DataTextField = "spcfdesc";
            this.ddlspcflist.DataValueField = "spcfcod";
            this.ddlspcflist.DataSource = dt2; // ds1.Tables[1];
            this.ddlspcflist.DataBind();


            DataView dv1 = matlist.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "' ";
            DataTable dt = dv1.ToTable();
            if (dt.Rows.Count > 0 && dt.Rows[0]["untcode"].ToString() != "")
            {

                DataTable stdunit = (DataTable)ViewState["UnitsRate"];
                DataView dv = stdunit.DefaultView;
                dv.RowFilter = "bcod = '" + dt.Rows[0]["untcode"].ToString() + "' ";
                DataTable newtabl = dv.ToTable();
                if (newtabl.Rows.Count == 0)
                {
                    DataRow dr1 = newtabl.NewRow();

                    dr1["comcod"] = this.GetComeCode(); ;
                    dr1["bcod"] = dt.Rows[0]["untcode"].ToString();
                    dr1["bcodesc"] = dt.Rows[0]["rsirunit"].ToString();
                    dr1["ccod"] = dt.Rows[0]["untcode"].ToString();
                    dr1["ccodesc"] = dt.Rows[0]["rsirunit"].ToString();
                    dr1["conrat"] = 1.00;

                    newtabl.Rows.Add(dr1);
                }
                else
                {
                    dv = newtabl.Copy().DefaultView;
                    dv.RowFilter = "ccod = '" + dt.Rows[0]["untcode"].ToString() + "' ";
                    if (dv.ToTable().Rows.Count == 0)
                    {
                        DataRow dr1 = newtabl.NewRow();
                        dr1["comcod"] = this.GetComeCode(); ;
                        dr1["bcod"] = dt.Rows[0]["untcode"].ToString();
                        dr1["bcodesc"] = dt.Rows[0]["rsirunit"].ToString();
                        dr1["ccod"] = dt.Rows[0]["untcode"].ToString();
                        dr1["ccodesc"] = dt.Rows[0]["rsirunit"].ToString();
                        dr1["conrat"] = 1.00;

                        newtabl.Rows.Add(dr1);
                    }


                }
                this.ddlunit.DataTextField = "ccodesc";
                this.ddlunit.DataValueField = "ccod";
                this.ddlunit.DataSource = newtabl;
                this.ddlunit.DataBind();
                this.ddlunit.SelectedValue = dt.Rows[0]["untcode"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Not set Material Unit or Stock out');", true);


            }
        }
        protected void imgbtnStorid_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter2 = "%" + "%";
            DataTable dt;
            string sType = (this.Request.QueryString["reptype"].Length == 0) ? "%" : this.Request.QueryString["reptype"].ToString();
            //Store Change by Chairman for Date: 15-May-2015
            string EntryType = (this.Request.QueryString["Type"] == "Entry") ? "15%" : "1[57]%";
            DataSet ds5 = PurData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "RETRIVEFGSTOREALL", filter2, EntryType, "", "", "", "", "", "", "");

            DataView dv = (ds5.Tables[0]).Copy().DefaultView;
            dv.RowFilter = "stype like '" + sType + "'";
            dt = dv.ToTable();

            if (dt.Rows.Count == 0)
            {
                dt = ds5.Tables[0];
            }

            dt.Rows.Add(comcod, "000000000000", "", "-------------------- Select Store --------------------", "");

            this.ddlStore.DataTextField = "actdesc1";
            this.ddlStore.DataValueField = "actcode";
            this.ddlStore.DataSource = dt;
            this.ddlStore.DataBind();
            this.ddlStore.SelectedValue = "000000000000";

        }
        protected void GetUnitName()
        {
            string comcod = this.GetComeCode();
            DataSet ds = PurData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_CONVRT_INF", "", "", "", "", "", "", "");
            ViewState["UnitsRate"] = ds.Tables[0];
        }
        protected void gvMatIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink proDesc = (HyperLink)e.Row.FindControl("lgcMatDesc");
                string untcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "untcod")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string conunt = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conunt")).ToString();
                if (actcode == "000000000000")
                {
                    e.Row.BackColor = System.Drawing.Color.Wheat;
                    e.Row.ToolTip = "Stock Empty and Store Not Found";

                }
                if (untcod != conunt)
                {
                    ((Label)e.Row.FindControl("lblgvConunit")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Row.FindControl("lgvlbUnit")).ForeColor = System.Drawing.Color.Red;

                }

                Label rsircode = (Label)e.Row.FindControl("lblrsircode");
                Label spcfcod = (Label)e.Row.FindControl("lblspcfcod");
                string date = this.txtCurDate.Text.ToString();

                proDesc.NavigateUrl = "~/F_11_RawInv/RptIndProStock?Type=MatHis&sircode=" + (rsircode.Text + spcfcod.Text) + "&date=" + date + "&dayid=" + date;


            }
        }

        protected void LbtnBatchUpdate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string ddlStore = this.ddlStore.SelectedValue.ToString();
            string sircode = ((Label)this.gvMatIssue.Rows[index].FindControl("lblrsircode")).Text.ToString();
            string spcfcod = ((Label)this.gvMatIssue.Rows[index].FindControl("lblspcfcod")).Text.ToString();


            DataSet result = PurData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GETSTKBALFIFO", misudate, ddlStore, sircode, spcfcod, misuno);
            if (result == null)
            {
                return;
            }
            ViewState["tblBatchData"] = result.Tables[0];
            this.gvBatchDetails.DataSource = result.Tables[0];
            this.gvBatchDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenBatchModal();", true);
        }

        private void SaveValue_Issue_Pipo()
        {
            DataTable dt = (DataTable)ViewState["tblBatchData"];
            int rowindex;
            for (int i = 0; i < this.gvBatchDetails.Rows.Count; i++)
            {
                string lateappsta = (((CheckBox)this.gvBatchDetails.Rows[i].FindControl("chkack")).Checked == true) ? "True" : "False";
                double qty = Convert.ToDouble("0" + ((TextBox)this.gvBatchDetails.Rows[i].FindControl("txtgvqty")).Text.Trim());

                rowindex = (this.gvBatchDetails.PageIndex) * this.gvBatchDetails.PageSize + i;
                dt.Rows[rowindex]["qty"] = qty;
                dt.Rows[rowindex]["approved"] = lateappsta;

            }
            ViewState["tblBatchData"] = dt;
        }
        protected void ModalUpdateBtn_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            this.SaveValue_Issue_Pipo();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblBatchData"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("approved='True'");
            dt = dv.ToTable();
            string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");

            string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            string rsircode = dt.Rows[0]["rsircode"].ToString();
            string spcfcod = dt.Rows[0]["spcfcod"].ToString();

            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "tbl1";


            bool result = PurData.UpdateXmlTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "UPDATEBATCHNO", ds, null, null, misuno, rsircode, spcfcod, userid, Terminal, Sessionid, Posteddat);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated');", true);






        }


        protected void gvMatIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblMatIssue"];
            int index = (this.gvMatIssue.PageIndex) * this.gvMatIssue.PageSize + e.RowIndex;
            dt.Rows.RemoveAt(index);
            ViewState["tblMatIssue"] = dt;
            this.Data_DataBind();
        }




        protected void CheckBoxStockalert_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBoxStockalert.Checked == true)
            {
                this.IssuePanel.Visible = false;
                this.StockAlertPanel.Visible = true;
                this.Stockout_Bind();
            }
            else
            {
                this.IssuePanel.Visible = true;
                this.StockAlertPanel.Visible = false;

            }
        }

        protected void LbtnModalBreakDown_Click(object sender, EventArgs e)
        {


            this.gvsizes.DataSource = null;
            this.gvsizes.DataBind();
            DataTable dt = (DataTable)ViewState["tblTriOrd"];
            string comcod = this.GetComeCode();
            this.ModalHead.Text = "Size Wise Quantity";
            string orderid = this.Request.QueryString["actcode"].ToString();
            string style = dt.Rows[0]["rsircode"].ToString();
            string colorid = dt.Rows[0]["colorid"].ToString();
            string dayid = dt.Rows[0]["odayid"].ToString();
            string toddat = Convert.ToDateTime(dt.Rows[0]["tardate"]).ToString("dd-MMM-yyyy");
            string slnum = dt.Rows[0]["slnum"].ToString();
            //DataSet result = PurData.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_ORDERWISE_MOLDSIZE_INFORMATION", mlccod, styleid, dayid);
            DataSet result = PurData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERWISE_SIZE_INFORMATION", orderid, style, colorid, dayid, toddat, slnum);
            if (result == null)
            {
                return;
            }
            ViewState["tblmold"] = result.Tables[1];
            for (int i = 0; i < result.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(result.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gvsizes.Columns[columid + 3].Visible = true;
                this.gvsizes.Columns[columid + 3].HeaderText = result.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }
            this.gvsizes.Columns[20].Visible = false;
            this.gvsizes.Columns[2].Visible = false;
            this.gvsizes.EditIndex = -1;

            this.gvsizes.DataSource = result.Tables[0];
            this.gvsizes.DataBind();

            DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ARTICLE_BASIC_INFORMATION", orderid, "", "", "", "", "", "", "", "");
            if (ds2 != null)
            {
                if (ds2.Tables[0].Rows.Count == 0)
                    return;
                this.BuyerName.Text = ds2.Tables[0].Rows[0]["buyername"].ToString();
                this.lblbrand.Text = ds2.Tables[0].Rows[0]["brand"].ToString();
                this.lblcolor.Text = ds2.Tables[0].Rows[0]["colordesc"].ToString();
                // this.lblTrialOrderNo.Text = ds1.Tables[3].Rows[0]["trialordr"].ToString();
                this.lblarticle.Text = ds2.Tables[0].Rows[0]["article"].ToString();
                //this.lblsizernge.Text = ds2.Tables[0].Rows[0]["sizerange"].ToString();
                this.SmpleIMg.ImageUrl = (ds2.Tables[0].Rows[0]["images"].ToString() == "") ? "~/images/no_img_preview.png" : ds2.Tables[0].Rows[0]["images"].ToString();
                //this.TotalOrder.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");
                //this.lblCurrency.Text = ds2.Tables[0].Rows[0]["currency"].ToString();
                //this.lblCurcode.Text = ds2.Tables[0].Rows[0]["curcode"].ToString();
                //this.lblCRate.Text = ds2.Tables[0].Rows[0]["conrate"].ToString();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);
        }

        protected void GetCodeBookList()
        {
            try
            {
                string Querytype = this.Request.QueryString["Type"];
                string coderange = "04%";

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet dsone = PurData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                Session["tblmatsubhead"] = dsone.Tables[1];
                this.ddlCodeBook.DataTextField = "sircode";
                this.ddlCodeBook.DataValueField = "sircode1";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
                this.ddlCodeBook_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
            }
        }

        protected void ddlCodeBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mathead = this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            DataTable dt = (DataTable)Session["tblmatsubhead"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode1 like '" + mathead + "'";
            this.ddlGroup.DataTextField = "sircode";
            this.ddlGroup.DataValueField = "sircode1";
            this.ddlGroup.DataSource = dv.ToTable();
            this.ddlGroup.DataBind();
        }

        protected void LbtnAllDprSingleMat_Click(object sender, EventArgs e)
        {
            string mReqno = this.ddlReqList.SelectedValue.ToString();
            string matcod = this.ddlMatList.SelectedValue.ToString();
            string spcfcod = this.ddlspcflist.SelectedValue.ToString();
            string conunit = this.ddlunit.SelectedValue.ToString();
            string process = this.ddlprocess.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)ViewState["tblMatIssue"];
            DataTable dt2 = (DataTable)ViewState["tblMatISu"];            
            DataTable tblunit = (DataTable)ViewState["UnitsRate"];        
            DataRow[] dr3 = dt2.Select(" rsircode='" + matcod + "' and spcfcod='" + spcfcod + "' and procode='" + process + "'");

            foreach (DataRow item in dr3)
            {
                           

                DataRow[] dr4 = tbl1.Select("preqno='" + item["preqno"] + "' and rsircode='" + matcod + "' and spcfcod='" + spcfcod + "' and procode='" + process + "'");
            if (dr4.Length > 0)
            {
                if (dr4[0]["rsircode"].ToString() == matcod && dr4[0]["spcfcod"].ToString() == spcfcod)
                {
                    return;
                }
            }
            else
            {

                DataRow[] unitrow = tblunit.Select("bcod = '" + item["untcode"].ToString() + "' and ccod='" + conunit + "'");

                DataRow dr1 = tbl1.NewRow();
                dr1["preqno"] = item["preqno"].ToString();
                dr1["preqno1"] = item["preqno1"].ToString();
                dr1["bactcode"] = item["bactcode"].ToString();
                dr1["bactdesc"] = item["bactdesc"].ToString();
                dr1["procode"] = item["procode"].ToString();
                dr1["prodesc"] = item["prodesc"].ToString();
                dr1["rsircode"] = item["rsircode"].ToString();
                dr1["rsirdesc"] = item["rsirdesc"].ToString();
                dr1["spcfcod"] = item["spcfcod"].ToString();
                dr1["spcfdesc"] = item["spcfdesc"].ToString();
                dr1["rsirunit"] = item["rsirunit"].ToString();
                dr1["untcod"] = item["untcode"].ToString();
                dr1["conunt"] = conunit;
                dr1["conqty"] = (unitrow.Count() == 0) ? item["isuqty"].ToString() : (Convert.ToDouble(item["isuqty"]) * Convert.ToDouble(unitrow[0]["conrat"])).ToString();
                dr1["conuntdesc"] = this.ddlunit.SelectedItem.ToString();
                dr1["consppair"] = item["consppair"].ToString();
                dr1["balqty"] = item["balqty"].ToString();
                dr1["isuqty"] = item["isuqty"].ToString();
                dr1["isurate"] = item["isurate"].ToString();
                dr1["stockqty"] = item["stockqty"].ToString();
                dr1["balstkqty"] = item["balstkqty"].ToString();
                dr1["actcode"] = item["actcode"].ToString();
                dr1["actdesc"] = item["actdesc"].ToString();
                dr1["reqqty"] = item["acqty"].ToString();
                tbl1.Rows.Add(dr1);
            }
            }
            ViewState["tblMatIssue"] = tbl1;
            ViewState["tblMatIssue"] = HiddenSameData((DataTable)ViewState["tblMatIssue"]);
            this.Data_DataBind();
            adjustment();
        }

        private void adjustment()
        {
            DataTable dt = (DataTable)ViewState["tblMatIssue"];
            if (dt.Rows.Count > 0)
            {
                var newDt = dt.AsEnumerable()
                                      .GroupBy(r => new {
                                          actcode = r.Field<string>("actcode"),
                                          rsircode = r.Field<string>("rsircode"),
                                          spcfcod = r.Field<string>("spcfcod"),
                                          procode = r.Field<string>("procode")
                                      })
                                      .Select(g =>
                                      {
                                          var row = dt.NewRow();
                                          row["actcode"] = g.Key.actcode;
                                          row["rsircode"] = g.Key.rsircode;
                                          row["spcfcod"] = g.Key.spcfcod;
                                          row["procode"] = g.Key.procode;
                                          row["prodesc"] = g.First()["prodesc"];
                                          row["actdesc"] = g.First()["actdesc"];
                                          row["rsirdesc"] = g.First()["rsirdesc"];
                                          row["spcfdesc"] = g.First()["spcfdesc"];
                                          row["rsirunit"] = g.First()["rsirunit"];
                                          row["stockqty"] = g.First()["stockqty"];
                                          row["isuqty"] = g.Sum(r => r.Field<decimal>("isuqty"));                                        
                                          row["reqqty"] = g.Sum(r => r.Field<decimal>("reqqty"));
                                          row["conqty"] = g.Sum(r => r.Field<decimal>("conqty"));

                                          
                                          return row;
                                      }).CopyToDataTable();
         
            Session["tblissueHelper"] = newDt;

            this.gvtblIsueHelper_DataBind();
            }
        }


        private void gvtblIsueHelper_DataBind()
        {
            DataTable dtisue = (DataTable)Session["tblissueHelper"];
            
            this.gvIsuItem.DataSource = dtisue;
            this.gvIsuItem.DataBind();

            ((Label)this.gvIsuItem.FooterRow.FindControl("gvLblTtlIssuqty2")).Text = Convert.ToDouble((Convert.IsDBNull(dtisue.Compute("sum(isuqty)", "")) ? 0 :
                                                                                      dtisue.Compute("sum(isuqty)", ""))).ToString("#,##0.0000;(#,##0.0000); ");

            //((Label)this.gvIsuItem.FooterRow.FindControl("lgvRSumSMRRQty")).Text = (listsum.Select(p => p.mrrqty).Sum() == 0.00) ? "0" : listsum.Select(p => p.mrrqty).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalBalQty")).Text = (listsum.Select(p => p.orderbal).Sum() == 0.00) ? "0" : listsum.Select(p => p.orderbal).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalReceived")).Text = (listsum.Select(p => p.recup).Sum() == 0.00) ? "0" : listsum.Select(p => p.recup).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalOrdQty")).Text = (listsum.Select(p => p.ordrqty).Sum() == 0.00) ? "0" : listsum.Select(p => p.ordrqty).Sum().ToString("#,##0.00;(#,##0.00); ");

        }
        protected void LbtnRecItemCalculate_Click(object sender, EventArgs e)
        {
            DataTable dtRec = (DataTable)Session["tblissueHelper"];
        //    var listsum = dtRec.DataTableToList<SumClass>();
            var sum = 0.00;

            for (int i = 0; i < this.gvIsuItem.Rows.Count; i++)
            {
                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvIsuItem.Rows[i].FindControl("txtgvSMRRQty")).Text.Trim()));
                sum += Qty;
                dtRec.Rows[i]["conqty"] = Qty;

                if (Qty == 0)
                    continue;
                DataTable dt = (DataTable)ViewState["tblMatIssue"];

                DataRow[] dr3 = dt.Select("actcode='" + dtRec.Rows[i]["actcode"] + "' and rsircode='" + dtRec.Rows[i]["rsircode"] + "' and spcfcod='" + dtRec.Rows[i]["spcfcod"] + "' and procode='" + dtRec.Rows[i]["procode"] + "'");

                foreach (DataRow item in dr3)
                {
                  

                    if (Convert.ToDouble(item["reqqty"]) < Qty)
                    {
                        item["conqty"] = item["reqqty"];
                        Qty = Qty - Convert.ToDouble(item["reqqty"]);
                    }
                    else
                    {
                        item["conqty"] = Qty;
                        Qty = 0;
                        break;
                    }
                }
                ViewState["tblMatIssue"] = dt;

            }
            Session["tblissueHelper"] = dtRec;
            this.Data_DataBind();
            this.gvtblIsueHelper_DataBind();
        }

        protected void LbtnReqItemShow_Click(object sender, EventArgs e)
        {
            if (this.LbtnReqItemShow.Text == "Expand")
            {
                this.gvIsuItem.Visible = true;
                this.LbtnReqItemShow.Text = "Collapse";
            }
            else
            {
                this.gvIsuItem.Visible = false;
                this.LbtnReqItemShow.Text = "Expand";
            }
        }

        protected void LbtnToClear_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblMatIssue"];
            foreach (DataRow item in dt.Rows)
            {
                item["conqty"] = 0;
            }
            ViewState["tblMatIssue"] = dt;
            this.Data_DataBind();
        }
        
        protected void LbtnToClear2_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblissueHelper"];
            foreach (DataRow item in dt.Rows)
            {
                item["conqty"] = 0;
            }
            Session["tblissueHelper"] = dt;
            this.gvtblIsueHelper_DataBind();
        }
    }
}