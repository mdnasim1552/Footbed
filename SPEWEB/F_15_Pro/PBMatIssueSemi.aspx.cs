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
using SPEENTITY;
using SPERDLC;

namespace SPEWEB.F_15_Pro
{
    public partial class PBMatIssueSemi : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess PurData = new ProcessAccess();
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
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                string Date = this.Request.QueryString["genno"].ToString();
                this.txtCurDate.Text = (Date.Length == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Date;
                this.GetStoretName();
                this.GetUnitName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "MATERIAL ISSUE INFORMATION-Semi";

                this.CommonButton();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
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
           

           
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Calculation";


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

        private void GetStoretName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string serch1 = "%" + this.txtSrcPro.Text.Trim() + "%";
            string Type = (this.Request.QueryString["Type"] == "Entry") ? "" : "Semi";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETSTORENAME", serch1, Type, usrid, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlStoreName.DataTextField = "actdesc";
            this.ddlStoreName.DataValueField = "actcode";
            this.ddlStoreName.DataSource = ds1.Tables[0];
            this.ddlStoreName.DataBind();
            ViewState["tblStoreType"] = ds1.Tables[0];
        }


        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetStoretName();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblbill"];
            //DataView dv1 = dt.DefaultView;
            //dv1.RowFilter = "billqty>0";
            //ReportDocument rptstk = new MFGRPT.R_02_Pur.RptConBill();
            //TextObject rpttxtComName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtComName.Text = comnam;
            //TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            //TextObject txtContractor = rptstk.ReportDefinition.ReportObjects["txtContractor"] as TextObject;
            //txtContractor.Text = this.ddlSubName.SelectedItem.Text.Trim().Substring(13);
            //TextObject rpttxtbillno = rptstk.ReportDefinition.ReportObjects["billno"] as TextObject;
            //rpttxtbillno.Text = "Bill No: " + this.lblCurNo1.Text.Trim() + this.lblCurNo2.Text.Trim();
            //TextObject rpttxtbillRefno = rptstk.ReportDefinition.ReportObjects["billRefno"] as TextObject;
            //rpttxtbillRefno.Text = (this.txtCBillRefNo.Text.Trim().Length > 0) ? "Bill Ref. No: " + this.txtCBillRefNo.Text.Trim() : "";
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = "Date: " + this.txtCurDate.Text.Trim();
            //TextObject txtremarks = rptstk.ReportDefinition.ReportObjects["txtremarks"] as TextObject;
            //txtremarks.Text = this.txtRemarks.Text.Trim();

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dv1.ToTable());
            //string comcod = hst["comcod"].ToString();

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                
                this.ddlStoreName.Enabled = false;
                this.PnlProRemarks.Visible = true;
                //this.lblPreList.Visible = false;
                //this.txtSrcIssueNo.Visible = false;
                this.ibtnPreIssueList.Visible = false;
                this.ddlPrevIssueList.Visible = false;
                this.ddlpagesize.Visible = true;
                this.lblPage.Visible = true;
                this.txtCurDate.Enabled = true;
                if (ddlPrevIssueList.Items.Count == 0)
                {
                    this.panel11.Visible = true;
                    this.ImgbtnFindReqList_Click(null, null);
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
                return;
            }
            this.lbtnOk.Text = "Ok";
            this.panel11.Visible = false;
            this.txtRemarks.Text = "";
            this.txtlSuRefNo.Text = "";
            this.lblCurNo1.Text = "";
            this.lblCurNo2.Text = "";
            this.ddlPrevIssueList.Items.Clear();
            this.ddlStoreName.Enabled = true;
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
        protected void GetUnitName()
        {
            string comcod = this.GetComeCode();
            DataSet ds = PurData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_UNIT_CONVRT_INF", "", "", "", "", "", "", "");
            ViewState["UnitsRate"] = ds.Tables[0];
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
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURISUINFO", mReqNo, CurDate1,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblMatIssue"] = HiddenSameData(ds1.Tables[0]);
            ViewState["tblUserReq"] = ds1.Tables[1];

            //if (Request.QueryString["InputType"].ToString() == "Approval" || Request.QueryString["InputType"].ToString() == "FxtAstApproval")
            //{
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        this.GetApprQty();
            //    }
            //}

            if (mReqNo == "NEWREQ")
            {
                ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTISUINFO", CurDate1, "", "", "", "", "", "", "", "");
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
            //this.lblStoredesc.Text = this.ddlStoreName.SelectedItem.Text.Trim();
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

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["preqno"].ToString() == preqno || dt1.Rows[j]["bactcode"].ToString() == bactcode || dt1.Rows[j]["procode"].ToString() == procode)
                {
                    preqno = dt1.Rows[j]["preqno"].ToString();
                    bactcode = dt1.Rows[j]["bactcode"].ToString();
                    procode = dt1.Rows[j]["procode"].ToString();
                    dt1.Rows[j]["preqno1"] = "";
                    dt1.Rows[j]["bactdesc"] = "";
                    dt1.Rows[j]["prodesc"] = "";
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
            DataTable dt = (DataTable)ViewState["tblMatIssue"];
            int rowindex;
            for (int i = 0; i < this.gvMatIssue.Rows.Count; i++)
            {
                string untcod = dt.Rows[i]["untcod"].ToString();
                string conunt = dt.Rows[i]["conunt"].ToString();
                DataTable tblunit = (DataTable)ViewState["UnitsRate"];
                DataRow[] unitrow = tblunit.Select("bcod = '" + untcod + "' and ccod='" + conunt + "'");
                double conqty = Convert.ToDouble("0" + ((TextBox)this.gvMatIssue.Rows[i].FindControl("lgvConQty")).Text.Trim());
                double isuqty = Convert.ToDouble("0" + ((TextBox)this.gvMatIssue.Rows[i].FindControl("lgvIsuQty")).Text.Trim());
                double isurate = Convert.ToDouble("0" + ((TextBox)this.gvMatIssue.Rows[i].FindControl("lgvIsuRate")).Text.Trim());
                double isueqty = (unitrow.Count() == 0) ? isuqty : (Convert.ToDouble(conqty) / Convert.ToDouble(unitrow[0]["conrat"]));

                rowindex = (this.gvMatIssue.PageIndex) * this.gvMatIssue.PageSize + i;
                dt.Rows[rowindex]["isuqty"] = isueqty;
                dt.Rows[rowindex]["conqty"] = conqty;
                dt.Rows[rowindex]["isurate"] = isurate;
            }
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
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVMISSUELIST", CurDate1, "Normal", "", "", "", "", "", "", "");
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
            string matcod = ASTUtility.Left(this.ddlMatList.SelectedValue.ToString(), 12);
            string spcfcod = ASTUtility.Right(this.ddlMatList.SelectedValue.ToString(), 12);
            string conunit = this.ddlunit.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)ViewState["tblMatIssue"];
            DataTable dt2 = (DataTable)ViewState["tblMatISu"];
            DataRow[] dr2 = dt2.Select("preqno='" + mReqno + "'");
            DataView dv = dt2.DefaultView;
            dt2 = dv.ToTable();
            DataRow[] dr3 = dt2.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'");

            DataRow[] dr4 = tbl1.Select("rsircode='" + matcod + "' and spcfcod='" + spcfcod + "'");
            if (dr4.Length > 0)
            {
                if (dr4[0]["rsircode"].ToString() == matcod)
                {
                    return;
                }
            }
            else
            {
                DataTable tblunit = (DataTable)ViewState["UnitsRate"];
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
                dr1["conuntdesc"] = this.ddlunit.SelectedItem.ToString();
                dr1["balqty"] = dr3[0]["balqty"].ToString();
                dr1["isuqty"] = dr3[0]["isuqty"].ToString();
                dr1["conqty"] = (unitrow.Count() == 0) ? dr3[0]["isuqty"].ToString() : (Convert.ToDouble(dr3[0]["isuqty"]) * Convert.ToDouble(unitrow[0]["conrat"])).ToString();
                dr1["isurate"] = dr3[0]["isurate"].ToString();
                dr1["stockqty"] = dr3[0]["stockqty"].ToString();
                dr1["balstkqty"] = dr3[0]["balstkqty"].ToString();

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

            DataTable dt = (DataTable)ViewState["tblStoreType"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("actcode='" + iStore + "'");
            dt = dv.ToTable();
            string Codetype = dt.Rows[0]["acttype"].ToString();
            string SearchInfo = "";
            if (Codetype.Length > 0)
            {

                int len;
                string[] ar = Codetype.Split('/');
                foreach (string ar1 in ar)
                {


                    if (ar1.Contains("-"))
                    {
                        len = ar1.IndexOf("-");
                        SearchInfo = SearchInfo + "left(rsircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                    }
                    else
                    {
                        len = ar1.Length;

                        SearchInfo = SearchInfo + "left(rsircode,'" + len + "')" + " = " + ar1 + " ";
                    }
                    SearchInfo = SearchInfo + " or ";

                }
                if (SearchInfo.Length > 0)
                    SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            }
            string Dprno = (this.Request.QueryString["actcode"].ToString().Length == 0) ? "%" : this.Request.QueryString["actcode"].ToString();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "MATCODELIST", iStore, iDate, SearchInfo, Dprno, "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblMatISu"] = ds1.Tables[0];
            //ViewState["tblSpcf"] = ds1.Tables[2];
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
                DataSet ds2 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTISUINFO", mISUDAT,
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

                string Remarks = this.txtRemarks.Text.Trim();
                string misuref = this.txtlSuRefNo.Text.Trim();




                ////////////////////////--------------------------------Check Stock Qty////////////////////////////////////
                //if (ASTUtility.Left(comcod, 2) != "87")
                //{
                //    int index;

                //    for (int j = 0; j < this.gvMatIssue.Rows.Count; j++)
                //    {
                //        index = (this.gvMatIssue.PageSize) * (this.gvMatIssue.PageIndex) + j;

                //        double stockqty = Convert.ToDouble(dt.Rows[index]["stockqty"]);
                //        double IsuQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMatIssue.Rows[j].FindControl("lgvIsuQty")).Text.Trim()));

                //        string rsircode = ((Label)this.gvMatIssue.Rows[j].FindControl("lblgvrsircode")).Text.Trim();

                //        if (ASTUtility.Left(rsircode, 2) != "06")
                //        {
                //            if (stockqty < IsuQty)
                //            {

                //                this.lblmsg.Text = "Stock Not Available";
                //                return;

                //            }
                //        }


                //    }

                //}


                bool result;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                DataTable dtuser = (DataTable)ViewState["tblUserReq"];
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


                result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "MATISSUEB", misuno, actcode, misudate,
                      Remarks, misuref, PostedByid, Posttrmid, PostSession, Posteddat, EditByid, Editdat, "Normal", "", "");

                //result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "MATISSUEB", misuno, actcode, misudate,
                //      Remarks, misuref, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + PurData.ErrorObject["Msg"] + "');", true);

                    return;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string preqno = dt.Rows[i]["preqno"].ToString();
                    decimal isuqty = Convert.ToDecimal(dt.Rows[i]["isuqty"]);
                    decimal isurate = Convert.ToDecimal(dt.Rows[i]["isurate"]);
                    string bactcode = dt.Rows[i]["bactcode"].ToString();
                    string rsircode = dt.Rows[i]["rsircode"].ToString();
                    string spcfcod = dt.Rows[i]["spcfcod"].ToString();
                    string procode = dt.Rows[i]["procode"].ToString();
                    string conunt = dt.Rows[i]["conunt"].ToString();
                    decimal conqty = Convert.ToDecimal(dt.Rows[i]["conqty"]);
                    if (isuqty > 0)
                    {
                        result = PurData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURMISSUEINFO", "MATISSUEA", misuno, bactcode, rsircode, procode,
                            isuqty.ToString(), preqno, isurate.ToString(), spcfcod, conunt, conqty.ToString(), "", "", "", "");
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
            DataTable dt1 = ((DataTable)ViewState["tblMatISu"]).Copy();
            //DataRow[] dr2 = tbl1.Select("preqno='" + mReqno + "'");
            DataTable dt2 = new DataTable();
            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("preqno='" + mReqno + "'");
            dt2 = dv.ToTable();


            this.ddlMatList.DataTextField = "rsirdesc1";
            this.ddlMatList.DataValueField = "rsircode1";
            this.ddlMatList.DataSource = dt2; // ds1.Tables[1];
            this.ddlMatList.DataBind();
            this.ddlMatList_SelectedIndexChanged(null, null);

        }
        protected void ddlMatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable matlist = (DataTable)ViewState["tblMatISu"];
            string matcod = ASTUtility.Left(this.ddlMatList.SelectedValue.ToString(), 12);
            string spcfcod = ASTUtility.Right(this.ddlMatList.SelectedValue.ToString(), 12);

            DataView dv1 = matlist.DefaultView;
            dv1.RowFilter = "rsircode = '" + matcod + "' and spcfcod= '" + spcfcod + "' ";
            DataTable dt = dv1.ToTable();
            if (dt.Rows.Count > 0 && dt.Rows[0]["untcode"].ToString() != "")
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
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
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Not set Material Unit";
            }
        }
        protected void ImgbtnFindMatList_Click(object sender, EventArgs e)
        {

        }
        protected void lnkSelectAll_Click(object sender, EventArgs e)
        {

            string mReqno = this.ddlReqList.SelectedValue.ToString();
            string matcod = ASTUtility.Left(this.ddlMatList.SelectedValue.ToString(), 12);
            string spcfcod = ASTUtility.Right(this.ddlMatList.SelectedValue.ToString(), 12);
            string conunit = this.ddlunit.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)ViewState["tblMatIssue"];
            DataRow[] dr2 = tbl1.Select("preqno='" + mReqno + "' and rsircode = '" + matcod + "' and spcfcod = '" + spcfcod + "'");

            DataTable tbl2 = (DataTable)ViewState["tblMatISu"];
            //DataRow[] dr3 = tbl2.Select("preqno='" + mReqno + "'");
            DataView dv = tbl2.DefaultView;
            dv.RowFilter = ("preqno='" + mReqno + "'");
            tbl2 = dv.ToTable();

            if (dr2.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    DataTable tblunit = (DataTable)ViewState["UnitsRate"];
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
                    dr1["untcod"] = tbl2.Rows[i]["untcode"].ToString();
                    dr1["conunt"] = conunit;
                    dr1["conuntdesc"] = this.ddlunit.SelectedItem.ToString();
                    dr1["conqty"] = (unitrow.Count() == 0) ? tbl2.Rows[i]["isuqty"].ToString() : (Convert.ToDouble(tbl2.Rows[i]["isuqty"]) * Convert.ToDouble(unitrow[0]["conrat"])).ToString();
                    dr1["balqty"] = tbl2.Rows[i]["balqty"].ToString();
                    dr1["isuqty"] = tbl2.Rows[i]["isuqty"].ToString();
                    dr1["isurate"] = tbl2.Rows[i]["isurate"].ToString();
                    dr1["stockqty"] = tbl2.Rows[i]["stockqty"].ToString();
                    dr1["balstkqty"] = tbl2.Rows[i]["balstkqty"].ToString();

                    tbl1.Rows.Add(dr1);

                }

                ViewState["tblReq"] = tbl1;
                this.Data_DataBind();
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

    }
}