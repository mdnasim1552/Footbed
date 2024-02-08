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

namespace SPEWEB.F_15_Pro
{
    public partial class FloorMatIssue : System.Web.UI.Page
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
                this.GetOrderNumber();

                ((Label)this.Master.FindControl("lblTitle")).Text = "FLOOR MATERIAL ISSUE INFORMATION";


            }
            this.CommonButton();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnFinalProUpdate_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_DataBind();
        }

        private void CommonButton()
        {
            
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

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
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETORDNO", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlStoreName.DataTextField = "actdesc";
            this.ddlStoreName.DataValueField = "actcode";
            this.ddlStoreName.DataSource = ds1.Tables[0];
            this.ddlStoreName.DataBind();
            if (this.Request.QueryString["actcode"].ToString().Length > 0)
            {
                this.ddlStoreName.SelectedValue = this.Request.QueryString["actcode"].ToString();
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
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_LCMATISSUE", "GETPURISUINFO", misuno, misudate,
                    "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;
            string session = hst["session"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)ViewState["tblMatIssue"];
            var rptlist = dt.DataTableToList<SPEENTITY.C_11_RawInv.EclassMaterialIssue>();
            LocalReport Rpt1a = new LocalReport();

            Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_11_RawInv.RptMaterialIssue", rptlist, null, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("rptitle", "Material Issue"));
            Rpt1a.SetParameters(new ReportParameter("isuno", misuno));
            Rpt1a.SetParameters(new ReportParameter("isudat", misudate));
            Rpt1a.SetParameters(new ReportParameter("isuref", ds1.Tables[1].Rows[0]["misuref"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("rmrks", ds1.Tables[1].Rows[0]["rmrks"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("postedbyname", ds1.Tables[1].Rows[0]["postedbyname"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("aprvbyname", ds1.Tables[1].Rows[0]["postedbyid"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


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
                this.GetProcess();
                this.ImgbtnFindReqList_Click(null, null);

                return;
            }
            this.lbtnOk.Text = "Ok";
            this.panel11.Visible = false;
            this.lblStoredesc.Text = "";
            this.txtRemarks.Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
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
            string mReqNo = "NEWISU";
            if (this.ddlPrevIssueList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mReqNo = this.ddlPrevIssueList.SelectedValue.ToString();
            }
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GET_FLOOR_ISSUE_INFORMATION", mReqNo, CurDate1,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblMatIssue"] = HiddenSameData(ds1.Tables[0]);
            Session["tblUserReq"] = ds1.Tables[1];

            if (mReqNo == "NEWISU")
            {
                ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GET_FLISSUE_NO", CurDate1, "", "", "", "", "", "", "", "");
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
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["fisueno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["fisueno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["fisuedate"]).ToString("dd-MMM-yyyy");
            this.ddlStoreName.SelectedValue = ds1.Tables[0].Rows[0]["actcode"].ToString();
            this.lblStoredesc.Text = this.ddlStoreName.SelectedItem.Text.Trim();
            this.txtRemarks.Text = ds1.Tables[1].Rows[0]["rmrks"].ToString();
            this.Data_DataBind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string preqno = dt1.Rows[0]["preqno"].ToString();

            string procode = dt1.Rows[0]["procode"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode && dt1.Rows[j]["preqno"].ToString() == preqno && dt1.Rows[j]["procode"].ToString() == procode)
                {
                    preqno = dt1.Rows[j]["preqno"].ToString();
                    procode = dt1.Rows[j]["procode"].ToString();
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["preqno1"] = "";
                    dt1.Rows[j]["prodesc"] = "";
                    dt1.Rows[j]["actdesc"] = "";
                }

                else
                {
                    preqno = dt1.Rows[j]["preqno"].ToString();
                    procode = dt1.Rows[j]["procode"].ToString();
                    actcode = dt1.Rows[j]["actcode"].ToString();
                }

            }

            return dt1;
        }

        private void Data_DataBind()
        {
            this.gvMatIssue.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatIssue.DataSource = (DataTable)ViewState["tblMatIssue"];
            this.gvMatIssue.DataBind();

        }

        protected void lnkselect_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }

        private void SaveValue()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
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
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Please Make Sure Unit Conversion";
                        //((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = false;
                        return;
                    }
                    conrate = Convert.ToDouble(unitrow[0]["conrat"]);

                }


                double conqty = Convert.ToDouble("0" + ((TextBox)this.gvMatIssue.Rows[i].FindControl("txtgvconqty")).Text.Trim());
                double isuqty = Convert.ToDouble("0" + ((TextBox)this.gvMatIssue.Rows[i].FindControl("lgvIsuQty")).Text.Trim());

                double isurate = Convert.ToDouble("0" + ((TextBox)this.gvMatIssue.Rows[i].FindControl("lgvIsuRate")).Text.Trim());
                double itmqty = (conrate == 0) ? isuqty : (Convert.ToDouble(conqty) / conrate);

                rowindex = (this.gvMatIssue.PageIndex) * this.gvMatIssue.PageSize + i;
                dt.Rows[rowindex]["conqty"] = conqty;
                // dt.Rows[rowindex]["balqty"] = Convert.ToDouble("0"+dt.Rows[rowindex]["balqty"])-conqty;
                dt.Rows[rowindex]["itmqty"] = itmqty;
                dt.Rows[rowindex]["itmrate"] = isurate;
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
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GET_PREV_FLOOR_ISSUE_LIST", CurDate1, "Normal", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevIssueList.Items.Clear();
            this.ddlPrevIssueList.DataTextField = "fisueno1";
            this.ddlPrevIssueList.DataValueField = "fisueno";
            this.ddlPrevIssueList.DataSource = ds1.Tables[0];
            this.ddlPrevIssueList.DataBind();
        }
        private void GetProcess()
        {
            Session.Remove("tblprocess");
            string comcod = GetComeCode();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPROCESS", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0].Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("procode <>'800100101099'");
            dt = dv.ToTable();

            this.ddlprodProcess.DataTextField = "prodesc";
            this.ddlprodProcess.DataValueField = "procode";
            this.ddlprodProcess.DataSource = dt;
            this.ddlprodProcess.DataBind();
            Session["tblprocess"] = ds1.Tables[0];
            ds1.Dispose();


        }
        protected void lbtnSelectReqList_Click(object sender, EventArgs e)
        {

            string mReqno = this.ddlReqList.SelectedValue.ToString();
            string matcod = this.ddlMatList.SelectedValue.ToString();
            string spcfcod = this.ddlspcflist.SelectedValue.ToString();
            string conunit = this.ddlunit.SelectedValue.ToString();
            string process = this.ddlprocess.SelectedValue.ToString();
            string prodprocess = this.ddlprodProcess.SelectedValue.ToString();
            string prodprocdesc = this.ddlprodProcess.SelectedItem.ToString();
            string ordername = this.ddlStoreName.SelectedItem.ToString();

            DataTable tbl1 = (DataTable)ViewState["tblMatIssue"];
            DataTable dt2 = (DataTable)ViewState["tblMatISu"];
            DataTable tblunit = (DataTable)ViewState["UnitsRate"];
            DataRow[] dr2 = dt2.Select("preqno='" + mReqno + "'");


            DataRow[] dr3 = dt2.Select(" preqno ='" + mReqno + "' and itmcode='" + matcod + "' and spcfcod='" + spcfcod + "' and procode='" + process + "'");

            DataRow[] dr4 = tbl1.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "' and procode='" + process + "'");
            if (dr4.Length > 0)
            {

            }
            else
            {

                DataRow[] unitrow = tblunit.Select("bcod = '" + dr3[0]["conunt"].ToString() + "' and ccod='" + conunit + "'");

                DataRow dr1 = tbl1.NewRow();
                dr1["preqno"] = dr3[0]["preqno"].ToString();
                dr1["preqno1"] = dr3[0]["preqno1"].ToString();
                dr1["actcode"] = dr3[0]["orderno"].ToString();
                dr1["actdesc"] = ordername;
                dr1["procode"] = dr3[0]["procode"].ToString();
                dr1["prodesc"] = dr3[0]["prodesc"].ToString();
                dr1["rsircode"] = dr3[0]["itmcode"].ToString();
                dr1["rsirdesc"] = dr3[0]["itmdesc"].ToString();
                dr1["spcfcod"] = dr3[0]["spcfcod"].ToString();
                dr1["spcfdesc"] = dr3[0]["spcfdesc"].ToString();
                dr1["prodproc"] = prodprocess;
                dr1["prodprocdesc"] = prodprocdesc;
                dr1["sirunit"] = dr3[0]["conuntdesc"].ToString();
                dr1["untcod"] = dr3[0]["conunt"].ToString();
                dr1["conunt"] = conunit;
                dr1["conqty"] = (unitrow.Count() == 0) ? dr3[0]["floorbal"].ToString() : (Convert.ToDouble(dr3[0]["floorbal"]) * Convert.ToDouble(unitrow[0]["conrat"])).ToString();
                dr1["conuntdesc"] = this.ddlunit.SelectedItem.ToString();
                //dr1["consppair"] = dr3[0]["consppair"].ToString();
                dr1["itmqty"] = dr3[0]["floorbal"].ToString();
                dr1["balqty"] = dr3[0]["floorbal"].ToString();
                dr1["whisuqty"] = dr3[0]["itmqty"].ToString();
                dr1["itmrate"] = dr3[0]["rate"].ToString();
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
            string mlccod = this.ddlStoreName.SelectedValue.ToString();
            string mSrchTxt = "%" + this.txtResSearch.Text.Trim() + "%";


            string Dprno = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GET_REQ_WISE_ISSUE_LIST", mlccod, Dprno, mSrchTxt, "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblMatISu"] = ds1.Tables[0];

            DataTable dt = ds1.Tables[0].DefaultView.ToTable(true, "preqno", "preqno1");

            this.ddlReqList.DataTextField = "preqno1";
            this.ddlReqList.DataValueField = "preqno";
            this.ddlReqList.DataSource = dt;
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
                DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "GET_FLISSUE_NO", mISUDAT,
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

                result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "UPDATE_FLOOR_MATERIAL_ISSUE", "FMATISSUEB", misuno, misudate,
                      Remarks, misuref, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, "", "");

                //result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "MATISSUEB", misuno, actcode, misudate,
                //      Remarks, misuref, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + PurData.ErrorObject["Msg"] + "');", true);

                    return;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string mlccod = dt.Rows[i]["actcode"].ToString();
                    string preqno = dt.Rows[i]["preqno"].ToString();
                    double itmqty = Convert.ToDouble(dt.Rows[i]["itmqty"].ToString());
                    double isuamt = itmqty * Convert.ToDouble(dt.Rows[i]["itmrate"].ToString());
                    string procode = dt.Rows[i]["procode"].ToString();
                    string rsircode = dt.Rows[i]["rsircode"].ToString();
                    string spcfcod = dt.Rows[i]["spcfcod"].ToString();
                    string conqty = dt.Rows[i]["conqty"].ToString();
                    string conunt = dt.Rows[i]["conunt"].ToString();
                    string prodproc = dt.Rows[i]["prodproc"].ToString();
                    if (itmqty > 0)
                    {
                        result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_MATERIAL_ISSUE", "UPDATE_FLOOR_MATERIAL_ISSUE", "FMATISSUEA", misuno, preqno, mlccod, rsircode, spcfcod, procode, prodproc, conunt, itmqty.ToString(), conqty.ToString(), isuamt.ToString(), "", "");
                        if (!result)
                            return;
                    }

                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Issue Updated');", true);


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);


            }
        }

        protected void ddlReqList_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        protected void lnkSelectAll_Click(object sender, EventArgs e)
        {
            string orderdesc = this.ddlStoreName.SelectedItem.ToString();

            string mReqno = this.ddlReqList.SelectedValue.ToString();
            string matcod = this.ddlMatList.SelectedValue.ToString();
            string prodproc = this.ddlprodProcess.SelectedValue.ToString();
            string prodprocdesc = this.ddlprodProcess.SelectedItem.ToString();

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
                DataRow[] dr2 = tbl1.Select("preqno='" + mReqno + "' and rsircode = '" + tbl2.Rows[i]["itmcode"].ToString() + "' and spcfcod='" + tbl2.Rows[i]["spcfcod"].ToString() + "' and procode='" + tbl2.Rows[i]["procode"].ToString() + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = tbl1.NewRow();
                    DataRow[] unitrow = tblunit.Select("bcod = '" + tbl2.Rows[i]["conunt"].ToString() + "' and ccod='" + conunit + "'");

                    dr1["preqno"] = tbl2.Rows[i]["preqno"].ToString();
                    dr1["preqno1"] = tbl2.Rows[i]["preqno1"].ToString();
                    dr1["prodproc"] = prodproc;
                    dr1["prodprocdesc"] = prodprocdesc;
                    dr1["procode"] = tbl2.Rows[i]["procode"].ToString();
                    dr1["prodesc"] = tbl2.Rows[i]["prodesc"].ToString();
                    dr1["rsircode"] = tbl2.Rows[i]["itmcode"].ToString();
                    dr1["rsirdesc"] = tbl2.Rows[i]["itmdesc"].ToString();
                    dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();
                    dr1["spcfdesc"] = tbl2.Rows[i]["spcfdesc"].ToString();
                    dr1["sirunit"] = tbl2.Rows[i]["conuntdesc"].ToString();
                    dr1["conunt"] = tbl2.Rows[i]["conunt"].ToString();
                    dr1["conqty"] = Convert.ToDouble(tbl2.Rows[i]["itmqty"]).ToString();

                    dr1["conuntdesc"] = tbl2.Rows[i]["conuntdesc"].ToString();
                    dr1["balqty"] = tbl2.Rows[i]["floorbal"].ToString();
                    dr1["untcod"] = tbl2.Rows[i]["conunt"].ToString();
                    dr1["itmqty"] = tbl2.Rows[i]["floorbal"].ToString();
                    dr1["itmrate"] = tbl2.Rows[i]["rate"].ToString();
                    dr1["actcode"] = tbl2.Rows[i]["orderno"].ToString();
                    dr1["actdesc"] = orderdesc;
                    dr1["whisuqty"] = tbl2.Rows[i]["itmqty"].ToString();
                    tbl1.Rows.Add(dr1);

                }
            }
            ViewState["tblMatIssue"] = HiddenSameData(tbl1);
            this.Data_DataBind();

        }
        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(prevPage);
        //}

        protected void ddlprocess_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mReqno = this.ddlReqList.SelectedValue.ToString();
            string process = (this.ddlprocess.SelectedValue.ToString() == "XXXXXXXXXXXX") ? "%" : this.ddlprocess.SelectedValue.ToString();
            DataTable dt1 = (DataTable)ViewState["tblMatISu"];

            DataTable dt2 = new DataTable();
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("preqno='" + mReqno + "' and procode like '" + process + "%'");
            //  dt2 = dv.ToTable();
            dt2 = dv.ToTable(true, "itmcode", "itmdesc");
            // Session["tblMatIssue"] = dt2;
            this.ddlMatList.DataTextField = "itmdesc";
            this.ddlMatList.DataValueField = "itmcode";
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
            dv2.RowFilter = ("preqno='" + mReqno + "' and procode like '" + process + "' and itmcode='" + mResCode + "'");
            dt2 = dv2.ToTable(true, "spcfcod", "spcfdesc");
            // Session["tblMatIssue"] = dt2;
            this.ddlspcflist.DataTextField = "spcfdesc";
            this.ddlspcflist.DataValueField = "spcfcod";
            this.ddlspcflist.DataSource = dt2; // ds1.Tables[1];
            this.ddlspcflist.DataBind();


            DataView dv1 = matlist.DefaultView;
            dv1.RowFilter = "itmcode = '" + mResCode + "' ";
            DataTable dt = dv1.ToTable();
            if (dt.Rows.Count > 0 && dt.Rows[0]["conunt"].ToString() != "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                DataTable stdunit = (DataTable)ViewState["UnitsRate"];
                DataView dv = stdunit.DefaultView;
                dv.RowFilter = "bcod = '" + dt.Rows[0]["conunt"].ToString() + "' ";
                DataTable newtabl = dv.ToTable();
                if (newtabl.Rows.Count == 0)
                {
                    DataRow dr1 = newtabl.NewRow();

                    dr1["comcod"] = this.GetComeCode(); ;
                    dr1["bcod"] = dt.Rows[0]["conunt"].ToString();
                    dr1["bcodesc"] = dt.Rows[0]["conuntdesc"].ToString();
                    dr1["ccod"] = dt.Rows[0]["conunt"].ToString();
                    dr1["ccodesc"] = dt.Rows[0]["conuntdesc"].ToString();
                    dr1["conrat"] = 1.00;

                    newtabl.Rows.Add(dr1);
                }
                else
                {
                    dv = newtabl.Copy().DefaultView;
                    dv.RowFilter = "ccod = '" + dt.Rows[0]["conunt"].ToString() + "' ";
                    if (dv.ToTable().Rows.Count == 0)
                    {
                        DataRow dr1 = newtabl.NewRow();
                        dr1["comcod"] = this.GetComeCode(); ;
                        dr1["bcod"] = dt.Rows[0]["conunt"].ToString();
                        dr1["bcodesc"] = dt.Rows[0]["conuntdesc"].ToString();
                        dr1["ccod"] = dt.Rows[0]["conunt"].ToString();
                        dr1["ccodesc"] = dt.Rows[0]["conuntdesc"].ToString();
                        dr1["conrat"] = 1.00;

                        newtabl.Rows.Add(dr1);
                    }


                }
                this.ddlunit.DataTextField = "ccodesc";
                this.ddlunit.DataValueField = "ccod";
                this.ddlunit.DataSource = newtabl;
                this.ddlunit.DataBind();
                this.ddlunit.SelectedValue = dt.Rows[0]["conunt"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Not set Material Unit or Stock out');", true);

            }
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
                //string untcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "untcod")).ToString();
                //string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                //string conunt = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "conunt")).ToString();
                //if (actcode == "000000000000")
                //{
                //    e.Row.BackColor = System.Drawing.Color.Wheat;
                //    e.Row.ToolTip = "Stock Empty";

                //}
                //if (untcod != conunt)
                //{
                //    ((Label)e.Row.FindControl("lblgvConunit")).ForeColor = System.Drawing.Color.Red;
                //    ((Label)e.Row.FindControl("lgvlbUnit")).ForeColor = System.Drawing.Color.Red;

                //}

            }
        }







        protected void gvMatIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblMatIssue"];
            int index = (this.gvMatIssue.PageIndex) * this.gvMatIssue.PageSize + e.RowIndex;
            dt.Rows.RemoveAt(index);
            ViewState["tblMatIssue"] = dt;
            this.Data_DataBind();
        }







    }
}