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
using System.Net.Mail;
using SPELIB;
using SPERDLC;
using SPEENTITY.C_22_Sal;
using System.IO;
using AjaxControlToolkit;


namespace SPEWEB.F_13_CWare
{
    public partial class PurReqEntry02 : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        Common CommonClass = new Common();
        //SmsSend SmsApps = new SmsSend();
        public static string Url = "";

        SalesInvoice_BL lst = new SalesInvoice_BL();
        static string prevPage = String.Empty;
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
                DataSet ds = (DataSet)Session["tblusrlog"];
                string frmname = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp);
                frmname = frmname.Substring(frmname.LastIndexOf('/') + 1) + "";
                DataTable dt = ds.Tables[1];
                DataRow[] dr2 = dt.Select("(frmname+qrytype)='" + frmname + "'");


                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                DataTable dt2 = (DataTable)Session["tblpageinfo"];

                string mName = ds.Tables[0].Rows[0]["modulename"].ToString();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.CommonButton();
                // for Mettroo
                this.txtReqText.Enabled = false;
                this.ImgbtnReqse.Enabled = false;
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //if (hst["comcod"].ToString() == "3202")
                //{
                //    this.chkneBudget.Enabled = false;
                //    this.chkneBudget.Checked = true;
                //}
                this.DupMRR();
                this.Load_Project_Combo();
                this.GetDeparment();
                this.CurrencyInf();
                this.GET_Nom_Recomended_info();
                this.GetSesson();
                //this.VisibleGrid();
                this.lbtnOk.Text = "New";
                this.lbtnOk_Click(null, null);
                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["InputType"].ToString() == "Entry") ? "Materials Requisition"
                    : (Request.QueryString["InputType"].ToString() == "Approval") ? "Materials Requisition Approval Screen"
                    : (Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "Requisition Input"
                    : (Request.QueryString["InputType"].ToString() == "LCEntry") ? "Import Requisition Information "
                     : (Request.QueryString["InputType"].ToString() == "ReqEdit") ? "Materials Requisition Information Input/Edit Screen"
                     : (Request.QueryString["InputType"].ToString() == "ReqReview") ? "Material Requisition Review"
                     : (Request.QueryString["InputType"].ToString() == "MoldReqEntry") ? "Mold Requisition Entry"
                     : (Request.QueryString["InputType"].ToString() == "FxtAstAuth") ? "Material Requisition Auth/Audit" : "Requisition Approval Screen";

                this.CreateTblDocs();

            }


             ((Label)this.Master.FindControl("lblprintstk")).Text = "";

        }
        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            if (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval" || this.Request.QueryString["InputType"].ToString() == "FxtAstAuth" || this.Request.QueryString["InputType"].ToString() == "ReqReview")
            {

                GetReqType();
            }
            if (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approve";
            }
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = true;

            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnResFooterTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdateResReq_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(lbtnUpdateApp_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lbtnUpdateAudit_Click);

            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetReqType()
        {
            string comcod = this.GetCompCode();
            string genno = this.Request.QueryString["genno"].ToString();


            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETRPUREQNO", CurDate1,
                          "%", genno, "Approved", "", "", "", "", "");

            if (ds2 == null)
                return;
            string reqTypelc = ds2.Tables[0].Rows.Count == 0 ? "Local" : ds2.Tables[0].Rows[0]["REQNO1"].ToString();
            string reqtype = ASTUtility.Right(reqTypelc.Trim(), 2);
            if (reqtype != "LC")
            {
                this.pnlPurType.Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).ValidationGroup = "FormValiCheck";

                GetPurType();
            }
            else
            {
                if ((this.Request.QueryString["InputType"] == "FxtAstAuth" && reqtype == "LC") || (this.Request.QueryString["InputType"] == "ReqReview" && reqtype == "LC") || (this.Request.QueryString["InputType"] == "FxtAstApproval" && reqtype == "LC"))
                {
                    this.pnlPurType.Visible = true;
                    this.lblpType.Visible = false;
                    this.ddlPurType.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).ValidationGroup = "FormValiCheck";
                }
                else
                {
                    this.pnlPurType.Visible = false;
                }


            }


        }
        private void GET_Nom_Recomended_info()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_NOM_RECM_INF", "", "", "", "", "", "", ""); ;
            ViewState["tblnominated"] = ds1.Tables[0];

        }
        protected void GetDeparment()
        {
            string comcod = this.GetCompCode();
            //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETDEPARTMENT", "%%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].Rows.Add(comcod, "000000000000", "Department");
            ds1.Tables[0].Rows.Add(comcod, "AAAAAAAAAAAA", "-------Select-----------");


            this.ddlDeptCode.DataTextField = "fxtgdesc";
            this.ddlDeptCode.DataValueField = "fxtgcod";
            this.ddlDeptCode.DataSource = ds1.Tables[0];
            this.ddlDeptCode.DataBind();
            this.ddlDeptCode.SelectedValue = "AAAAAAAAAAAA";

            this.ddlDeptCode2.DataTextField = "fxtgdesc";
            this.ddlDeptCode2.DataValueField = "fxtgcod";
            this.ddlDeptCode2.DataSource = ds1.Tables[0];
            this.ddlDeptCode2.DataBind();
        }
        private void DupMRR()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "7107":
                case "8101":
                case "8102":
                case "8103":
                case "8104":
                case "8105":
                case "8106":
                case "8107":
                case "8108":
                case "8109":
                case "8305":
                case "8303":
                    this.chkdupMRF.Enabled = false;
                    this.chkdupMRF.Checked = true;
                    break;
                default:
                    this.chkdupMRF.Enabled = false;
                    this.chkdupMRF.Checked = true;
                    break;







            }




        }
        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");


            ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");


            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";

            }


        }
        private void GetMasterOrder()
        {
            string comcod = this.GetCompCode();
            string season = this.DdlSeason.SelectedValue.ToString() == "00000" ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "PRJCODELIST", "%", "", "", season, "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblMsOrder"] = ds1.Tables[0];
            this.ddlOrder.DataTextField = "actdesc1";
            this.ddlOrder.DataValueField = "bomid";
            this.ddlOrder.DataSource = ds1.Tables[0];
            this.ddlOrder.DataBind();

        }
        private void GetJobWorkOrder()
        {
            string comcod = this.GetCompCode();
            string season = this.DdlSeason.SelectedValue.ToString() == "00000" ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "JOBWORKORDER", "%", "", "", season, "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblMsOrder"] = ds1.Tables[0];
            this.ddlOrder.DataTextField = "actdesc1";
            this.ddlOrder.DataValueField = "bomid";
            this.ddlOrder.DataSource = ds1.Tables[0];
            this.ddlOrder.DataBind();

        }

        private void VisibleGrid()
        {

            string Type = Request.QueryString["InputType"].ToString();
            string comcod = this.GetCompCode();

            //switch (Type)
            //{
            //    case "Approval":
            //    case "FxtAstApproval":
            //    case "ReqEdit":
            //    case "FxtAstAuth":
            //    case "ReqReview":
            //        this.gvReqInfo.Columns[13].Visible = true;
            //        this.gvReqInfo.Columns[14].Visible = true;
            //        this.gvReqInfo.Columns[17].Visible = true;
            //        this.gvReqInfo.Columns[18].Visible = true;
            //        // this.gvReqInfo.Columns[18].Visible = true;
            //        // this.gvReqInfo.Columns[13].Visible = true;
            //        //this.gvReqInfo.Columns[14].Visible = true;
            //        //this.gvReqInfo.Columns[17].Visible = true;
            //        //this.gvReqInfo.Columns[18].Visible = true;
            //        break;
            //}
            switch (comcod)
            {
                case "5301":
                case "5305":
                    this.gvReqInfo.Columns[28].Visible = false;

                    break;
            }





        }

        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            this.gvResInfo_DataBind();

        }
        protected void Load_Project_Combo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string fxtast = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "FxtAst"
                        : (this.Request.QueryString["InputType"].ToString() == "LCEntry") ? "FxtAst"
                        : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "FxtAst"
                        : (this.Request.QueryString["InputType"].ToString() == "ReqEdit") ? "ReqEdit"
                        : (this.Request.QueryString["InputType"].ToString() == "FxtAstAuth") ? "FxtAst"
                        : (this.Request.QueryString["InputType"].ToString() == "ReqReview") ? "FxtAst" : "";

            string Aproval = (this.Request.QueryString["InputType"].ToString() == "Approval") ? "Aproval"
                : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "Aproval"
                : (this.Request.QueryString["InputType"].ToString() == "FxtAstAuth") ? "Aproval"
                : (this.Request.QueryString["InputType"].ToString() == "ReqReview") ? "Aproval" : "";


            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string searchkey = this.txtProjectSearch.Text.Trim();
            if(this.Request.QueryString["InputType"].ToString()== "MoldReqEntry")
            {
                searchkey = "1511";
            }
            string ReFindProject = (this.Request.QueryString["actcode"].ToString()).Length == 0 ? "%" + searchkey + "%" : this.Request.QueryString["actcode"].ToString() + "%";

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "PRJCODELIST", ReFindProject, fxtast, Aproval, userid, "", "", "", "", "");
            if (ds2 == null)
                return;

            if (ds2.Tables[0].Rows.Count < 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
                return;
            }

            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds2.Tables[0];
            this.ddlProject.DataBind();
            ViewState["tblStoreType"] = ds2.Tables[0];
            //this.ddlFloor.DataTextField = "flrdes";
            //this.ddlFloor.DataValueField = "flrcod";
            //this.ddlFloor.DataSource = ds2.Tables[1];
            //this.ddlFloor.DataBind();

        }
        private void GetMrfNO()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "7305":
                    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETREQMRFNO", "", "", "", "", "", "", "", "", "");
                    this.txtMRFNo.Text = ds2.Tables[0].Rows[0]["mrfno"].ToString();
                    break;

                default:

                    break;
            }



        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                Session.Remove("tblAttDocs");
                this.pnlAttacDeocx.Visible = false;

                //this.txtSrchMrfNo.Visible = true;
                //this.lblpreReq.Visible = true;
                this.ImgbtnFindReq.Visible = true;
                this.ddlPrevReqList.Visible = true;
                this.ddlPrevReqList.Items.Clear();
                this.ddlProject.Visible = true;
                //  this.lblddlProject.Visible = false;

                this.ddlDeptCode.Enabled = true;
                this.DdlSeason.Enabled = true;
                this.ddlOrder.Enabled = true;
                this.Chckorder.Visible = true;

                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.lblCurReqNo1.Text = "REQ" + DateTime.Today.ToString("MM") + "-";
                this.txtCurReqDate.Enabled = true;
                this.txtMRFNo.Text = "";

                this.txtResSearch.Text = "";
                this.ddlResSpcf.Items.Clear();
                this.ddlResList.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtExpDeliveryDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtReqNarr.Text = "";
                this.gvReqInfo.DataSource = null;
                this.gvReqInfo.DataBind();
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.PnlDesc.Visible = false;

                ListViewEmpAll.DataSource = null;
                ListViewEmpAll.DataBind();
                this.lbtnOk.Text = "Ok";

                if (Request.QueryString["InputType"].ToString() == "FxtAstApproval" || Request.QueryString["InputType"].ToString() == "ReqEdit"
                    || Request.QueryString["InputType"].ToString() == "FxtAstAuth" || Request.QueryString["InputType"].ToString() == "ReqReview")
                {
                    this.chkdupMRF.Visible = false;
                    this.chkneBudget.Visible = false;

                    this.lblmrfno.Visible = false;
                    this.txtMRFNo.Visible = false;
                    this.lblCurNo.Visible = false;
                    this.lblCurReqNo1.Visible = false;
                    this.txtCurReqNo2.Visible = false;
                    this.txtReqText.Visible = false;
                    this.ImgbtnReqse.Visible = false;
                    //this.lbtnSurVey.Visible = true;+
                    // this.Get_Requisition_Info();
                    this.ImgbtnFindReq_Click(null, null);

                }

                return;
            }

            if (Request.QueryString["InputType"].ToString() == "FxtAstApproval" || Request.QueryString["InputType"].ToString() == "ReqEdit" || Request.QueryString["InputType"].ToString() == "FxtAstAuth" || Request.QueryString["InputType"].ToString() == "ReqReview")
            {
                this.pnlAttacDeocx.Visible = true;

                this.lblmrfno.Visible = true;
                this.txtMRFNo.Visible = true;
                //this.lnkDeleteReqNo.Visible = true;

                this.lblCurNo.Visible = true;
                this.lblCurReqNo1.Visible = true;
                this.txtCurReqNo2.Visible = true;
                this.txtReqText.Visible = true;
                this.ImgbtnReqse.Visible = true;

                //this.Panel1.Visible = false;

            }

            if (this.Request.QueryString["InputType"] == "FxtAstEntry" || this.Request.QueryString["InputType"] == "LCEntry")
            {
                if (this.ddlPrevReqList.Items.Count == 0)
                {

                    if (this.DdlSeason.SelectedValue.ToString() == "00000" && GetCompCode() != "5301")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Season');", true);


                        return;
                    }
                    if (this.ddlDeptCode.SelectedValue.ToString() == "AAAAAAAAAAAA")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Department');", true);


                        return;
                    }
                }

            }


            //this.txtSrchMrfNo.Visible = false;
            //this.lblpreReq.Visible = false;
            this.ImgbtnFindReq.Visible = false;
            this.ddlPrevReqList.Visible = false;
            this.ddlDeptCode.Enabled = false;
            this.DdlSeason.Enabled = false;
            this.ddlOrder.Enabled = true;
            this.Chckorder.Visible = false;
            //this.lblddlProject.Text = this.ddlProject.SelectedItem.Text.Trim();
            //  this.ddlProject.Enabled = false;
            // this.lblddlProject.Visible = true;

            this.txtCurReqNo2.ReadOnly = true;
            this.Panel1.Visible = true;
            this.Panel2.Visible = true;
            this.PnlDesc.Visible = true;
            this.lbtnOk.Text = "New";

            this.Get_Requisition_Info();
            this.LinkMarketSurvey();
            if (this.Chckorder.Checked == true)
            {
                this.ddlOrder_SelectedIndexChanged(null, null);
            }
            else
            {
                if (this.Request.QueryString["InputType"] == "FxtAstEntry" || this.Request.QueryString["InputType"] == "LCEntry")
                {
                    this.ImgbtnFindRes_Click(null, null);
                }


            }

            this.pnlAttacDeocx.Visible = true;
            btnShowimg_OnClick(null, null);

            //this.ddlOrder.Items.Count == 0 ||
            if (ASTUtility.Left(this.ddlProject.SelectedValue.ToString(), 2) != "15")
            {

                this.gvReqInfo.Columns[5].HeaderText = "Specification";
                this.gvReqInfo.Columns[6].HeaderText = "Model";
            }

            VisibleGrid();

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void GetReqNo()
        {
            string comcod = this.GetCompCode();
            string mREQNO = "NEWREQ";
            if (this.ddlPrevReqList.Items.Count > 0)
                mREQNO = this.ddlPrevReqList.SelectedValue.ToString();

            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            if (mREQNO == "NEWREQ")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTREQINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurReqNo1.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);

                    this.ddlPrevReqList.DataTextField = "maxreqno1";
                    this.ddlPrevReqList.DataValueField = "maxreqno";
                    this.ddlPrevReqList.DataSource = ds2.Tables[0];
                    this.ddlPrevReqList.DataBind();
                }
            }

        }


        protected void Get_Requisition_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mReqNo = "NEWREQ";
            if (this.ddlPrevReqList.Items.Count > 0)
            {
                this.txtCurReqDate.Enabled = false;
                mReqNo = this.ddlPrevReqList.SelectedValue.ToString();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETPURREQINFO", mReqNo, CurDate1,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblReq"] = this.HiddenSameData(ds1.Tables[0]);
            Session["tblUserReq"] = ds1.Tables[1];
            ViewState["tblreqdesc"] = ds1.Tables[2];
            this.gvDescrip.DataSource = ds1.Tables[2];
            this.gvDescrip.DataBind();
            if (Request.QueryString["InputType"].ToString() == "Approval" || Request.QueryString["InputType"].ToString() == "FxtAstApproval")
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.GetApprQty();
                }
            }

            if (mReqNo == "NEWREQ")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTREQINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurReqNo1.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds1.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);
                }
                this.GetMrfNO();
                return;
            }
            this.txtMRFNo.Text = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            this.lblCurReqNo1.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(0, 6);
            this.txtCurReqNo2.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(6, 5);
            this.txtCurReqDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["reqdat"]).ToString("dd.MM.yyyy");


            this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.DdlSeason.SelectedValue = ds1.Tables[1].Rows[0]["season"].ToString();
            this.ddlDeptCode.SelectedValue = ds1.Tables[1].Rows[0]["deptcode"].ToString();
            this.ddlDeptCode2.SelectedValue = ds1.Tables[1].Rows[0]["deptcode2"].ToString();
            this.ddlPurType.SelectedValue = ds1.Tables[1].Rows[0]["purtype"].ToString();
            this.ddlPurType_SelectedIndexChanged(null, null);
            this.ddlSup.SelectedValue = ds1.Tables[1].Rows[0]["nsupcode"].ToString();
            //this.lblddlProject.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["reqbydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtExpDeliveryDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["eddat"]).ToString("dd.MM.yyyy");
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();
            this.ShowMarketSurvey(ds1.Tables[1].Rows[0]["msrno"].ToString());
            this.ddlCurrency.SelectedValue = ds1.Tables[0].Rows[0]["curcode"].ToString();
            this.lblConRate.Text = ds1.Tables[0].Rows[0]["conrate"].ToString();

            //this.ddlOrder.DataTextField = "mlcdesc";
            //this.ddlOrder.DataValueField = "mlccod1";
            //this.ddlOrder.DataSource = ds1.Tables[1];
            //this.ddlOrder.DataBind();

            this.gvResInfo_DataBind();

        }

        private void LinkMarketSurvey()
        {


            //string reqno = this.ddlPrevReqList.SelectedValue.ToString();
            //if (reqno == "")
            //    return;

            //string QryStr = "reqno=" + reqno;
            //lbtnSurVey.NavigateUrl = "../F_07_Inv/LinkMktSurvey.aspx?" + QryStr; 



            //--------------------------------Old-----------------------------//
            //string TString = "javascript:window.showModalDialog('../F_12_Inv/LinkMktSurvey.aspx?" + QryStr + "', 'Unit Description', 'dialogHeight:800px;dialogWidth:900px;status:no')";
            //this.lbtnSurVey.Attributes.Add("OnClick", TString);

            //string Comcod = this.Getcomcod();
            //string Proscod = this.ddlClientList.SelectedValue.ToString();
            //string QryStr = "proscod=" + Proscod;
            //string TString = "javascript:window.showModalDialog('MktSalesDescription.aspx?" + QryStr + "', 'Unit Description', 'dialogHeight:600px;dialogWidth:700px;status:no')";
            //this.lbtnSalesCompleted.Attributes.Add("OnClick", TString);


            //string reqno = this.ddlPrevReqList.SelectedValue.ToString();
            //if (reqno == "")
            //    return;
            //string QryStr = "reqno=" + reqno;
            //string TString = "javascript:window.showModalDialog('../F_07_Inv/LinkMktSurvey.aspx?" + QryStr + "', 'Unit Description', 'dialogHeight:800px;dialogWidth:900px;status:no')";
            //this.lbtnSurVey.Attributes.Add("OnClick", TString);




        }
        private void ShowMarketSurvey(string msrno)
        {

            if (msrno == "")
            {
                this.lblMurketSurvey.Visible = false;
                this.lblsurveyby.Text = "";
                this.gvMSRInfo.DataSource = null;
                this.gvMSRInfo.DataBind();
                return;
            }
            this.lblMurketSurvey.Visible = true;
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO", msrno, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt1 = HiddenSameData(ds1.Tables[0]);
            this.gvMSRInfo.DataSource = dt1;
            this.gvMSRInfo.DataBind();
            //if(dt1.Rows.Count==0)
            //    return;

            for (int i = 0; i < this.gvMSRInfo.Rows.Count; i++)
            {

                string rsircode = dt1.Rows[i]["rsircode"].ToString();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("rsircode='" + rsircode + "'");
                dv.Sort = "rsircode, resrate asc";
                DataTable dt2 = dv.ToTable();
                double lsrate = Convert.ToDouble(dt2.Rows[0]["resrate"]);

                double gvlstrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMSRInfo.Rows[i].FindControl("lblgvMSRRate")).Text.Trim()));

                if (lsrate == gvlstrate)
                {

                    Label lblgvMSRSuplDesc = (Label)gvMSRInfo.Rows[i].FindControl("lblgvMSRSuplDesc");
                    Label txtgvRate = (Label)gvMSRInfo.Rows[i].FindControl("lblgvMSRRate");

                    lblgvMSRSuplDesc.Style.Add("color", "blue");
                    txtgvRate.Style.Add("color", "blue");


                }

            }
            this.lblsurveyby.Text = (ds1.Tables[1].Rows.Count == 0) ? "" : "Survey Completed By: " + ds1.Tables[1].Rows[0]["username"].ToString();
            this.txtMSRNarr.Text = (ds1.Tables[1].Rows.Count == 0) ? "" : ds1.Tables[1].Rows[0]["msrnar"].ToString();

        }
        private void GetApprQty()
        {
            DataTable dt = (DataTable)ViewState["tblReq"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double aprqty = Convert.ToDouble(dt.Rows[i]["preqty"]);
                dt.Rows[i]["areqty"] = aprqty;

            }
            ViewState["tblReq"] = dt;


        }



        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {

            this.Panel2.Visible = true;
            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)ViewState["tblReq"];

            double conRate = Convert.ToDouble(this.lblConRate.Text);

            ////-------------------------------/////
            //DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            //DataView dv1 = tbl1.DefaultView;
            //dv1.RowFilter = "rsircode = '" + mResCode + "' or spcfcod = '000000000000'";
            //DataTable dt = dv1.ToTable();

            ///---------------------------------////////
            ///


            foreach (ListItem item in ddlResSpcf.Items)
            {
                if (item.Selected)
                {
                    string bomid = (this.ddlOrder.SelectedValue.ToString() == "") ? "0000000000" : this.ddlOrder.SelectedValue.ToString();
                    string mResCode = this.ddlResList.SelectedValue.ToString();
                    string Specification = item.Value.ToString(); //this.ddlResSpcf.SelectedValue.ToString();
                    if (Specification == "AAAAAAAAAAAA")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Specification');", true);


                        return;
                    }
                    string suppplier = this.ddlSupplier.SelectedValue.ToString();
                    DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and spcfcod='" + Specification + "' and bomid='" + bomid + "'");
                    if (dr2.Length == 0)
                    {
                        DataRow dr1 = tbl1.NewRow();
                        dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                        dr1["spcfcod"] = item.Value.ToString(); //this.ddlResSpcf.SelectedValue.ToString();
                        dr1["rsirdesc1"] = this.ddlResList.SelectedItem.Text.Trim().Substring(7);

                        DataTable tbl3 = (DataTable)ViewState["tblSpcf"];
                        DataRow[] dr4 = tbl3.Select("rsircode = '" + mResCode + "' and spcfcod='" + Specification + "' ");

                        //dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();

                        dr1["spcfdesc"] = (dr4.Count() == 0) ? "" : dr4[0]["spcfdesc1"];
                        dr1["desc1"] = (dr4.Count() == 0) ? "" : dr4[0]["desc1"];
                        dr1["desc2"] = (dr4.Count() == 0) ? "" : dr4[0]["desc2"];
                        dr1["desc3"] = (dr4.Count() == 0) ? "" : dr4[0]["desc3"];
                        dr1["desc4"] = (dr4.Count() == 0) ? "" : dr4[0]["desc4"];
                        dr1["desc5"] = (dr4.Count() == 0) ? "" : dr4[0]["desc5"];
                        //dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();

                        DataTable tbl2 = (DataTable)ViewState["tblMat"];
                        if (this.ddlOrder.Items.Count == 0 || this.ddlOrder.SelectedValue == "00000000000")
                        {
                            DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
                            dr1["rsirunit"] = dr3[0]["rsirunit"];
                            dr1["stkqty"] = dr3[0]["stkqty"];
                            dr1["minstkqty"] = dr3[0]["minstkqty"];
                            dr1["useablestk"] = 0.00;
                            dr1["atprqty"] = dr3[0]["reqqty"];
                            dr1["preqty"] = dr3[0]["reqqty"];
                            dr1["areqty"] = dr3[0]["reqqty"];
                            dr1["lpurrate"] = 0;
                            dr1["reqrat"] = dr3[0]["sirval"];
                            dr1["preqamt"] = Convert.ToDouble(dr3[0]["reqqty"]) * Convert.ToDouble(dr3[0]["sirval"]);
                            dr1["areqamt"] = 0;
                            dr1["bdtamt"] = Convert.ToDouble(dr3[0]["reqqty"]) * Convert.ToDouble(dr3[0]["sirval"]) * Convert.ToDouble(this.lblConRate.Text.Trim());

                            dr1["conrate"] = conRate;
                            dr1["expusedt"] = "";// DateTime.Today;
                            dr1["pursdate"] = "";// DateTime.Today;
                            dr1["reqnote"] = "";
                            dr1["storecode"] = "";
                            dr1["ptype"] = "Others";
                            dr1["budget"] = dr3[0]["budget"];
                            dr1["pkgsize"] = "0";
                            dr1["bomid"] = "0000000000";
                            dr1["rfgqty"] = Convert.ToDouble("0" + TxtReqQty.Text.Trim()).ToString();
                            dr1["overflow"] = dr3[0]["overflow"];
                        }
                        else
                        {
                            DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "' and spcfcod =  '" + Specification + "'");
                            dr1["rsirunit"] = dr3[0]["rsirunit"];
                            dr1["stkqty"] = dr3[0]["stkqty"];
                            dr1["minstkqty"] = dr3[0]["minstkqty"];
                            dr1["useablestk"] = 0.00;
                            dr1["atprqty"] = dr3[0]["reqqty"];
                            dr1["preqty"] = dr3[0]["reqqty"];
                            dr1["areqty"] = dr3[0]["reqqty"];
                            dr1["lpurrate"] = 0;
                            dr1["reqrat"] = dr3[0]["sirval"];
                            dr1["preqamt"] = Convert.ToDouble(dr3[0]["reqqty"]) * Convert.ToDouble(dr3[0]["sirval"]);
                            dr1["areqamt"] = 0;
                            dr1["bdtamt"] = Convert.ToDouble(dr3[0]["reqqty"]) * Convert.ToDouble(dr3[0]["sirval"]) * Convert.ToDouble(this.lblConRate.Text.Trim());

                            dr1["conrate"] = conRate;
                            dr1["expusedt"] = "";// DateTime.Today;
                            dr1["pursdate"] = "";// DateTime.Today;
                            dr1["reqnote"] = "";
                            dr1["storecode"] = "";
                            dr1["ptype"] = "Others";
                            dr1["budget"] = dr3[0]["budget"];
                            dr1["pkgsize"] = "0";
                            dr1["bomid"] = bomid;
                            dr1["rfgqty"] = Convert.ToDouble("0" + TxtReqQty.Text.Trim()).ToString();
                            dr1["overflow"] = dr3[0]["overflow"];

                        }

                        tbl1.Rows.Add(dr1);
                    }

                }
            }
            //else
            //{
            //    dr2[0]["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
            //    dr2[0]["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
            //}
            ViewState["tblReq"] = this.HiddenSameData(tbl1);
            this.gvResInfo_DataBind();

        }


        private string CompanyRequisition()
        {
            string comcod = this.GetCompCode();
            string PrintReq = "";
            switch (comcod)
            {
                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                case "3101":
                    PrintReq = "PrintReque02";

                    break;

                default:
                    PrintReq = "PrintReque02";
                    break;
            }

            return PrintReq;

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            //DataView dv = dt1.DefaultView;
            //dv.Sort = "rsircode";
            //dt1 = dv.ToTable();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.PrintRequisition02();
        }

        private void PrintRequisition02()
        {
            string comcod = this.GetCompCode();
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string PrintType = this.Request.QueryString["InputType"].ToString();
            string reqType = ASTUtility.Left(PrintType, 2);

            string AppType = ASTUtility.Right(this.ddlPrevReqList.SelectedItem.Text, 2);
            string pType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            if (PrintType == "FxtAstEntry")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_10_Procur/PuchasePrint.aspx?Type=ReqPrint&comcod=" + comcod + "&reqno=" + mREQNO + "&ReqType=Local&AppType=YES&pType=" + pType + "" + "', target='_blank');</script>";
            }
            else if (PrintType == "FxtAstApproval")
            {
                if (AppType == "LC")
                {
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + mREQNO + "&ReqType=Import&AppType=YES&pType=" + pType + "" + "', target='_blank');</script>";
                }
                else
                {
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + mREQNO + "&ReqType=Local&AppType=" + AppType + "&pType=" + pType + "', target='_blank');</script>";
                }
            }
            else
            {
                if (reqType == "LC")
                {
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + mREQNO + "&ReqType=Import&AppType=YES&pType=" + pType + "" + "', target='_blank');</script>";
                }
                else
                {
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_10_Procur/PuchasePrint?Type=ReqPrint&comcod=" + comcod + "&reqno=" + mREQNO + "&ReqType=Local&AppType=YES&pType=" + pType + "" + "', target='_blank');</script>";
                }
            }



            //hlink1.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "";


        }


        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

                ;
                return;
            }



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.lbtnResFooterTotal_Click(null, null);
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            if (comcod.Substring(0, 2) == "87")
            {
                DataView dv2 = tbl1.DefaultView;
                dv2.RowFilter = ("reqnote =''");
                DataTable TTable = dv2.ToTable();
                if (TTable.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Fill up Mandatory Field');", true);


                    return;
                }
            }


            string mMRFNO = this.txtMRFNo.Text.Trim();
            if (this.ddlPrevReqList.Items.Count == 0)
                this.GetReqNo();
            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();


            if (this.chkdupMRF.Checked)
            {
                if (mMRFNO.Length == 0)
                {
                    this.txtMRFNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('M.R.F No. Should Not Be Empty');", true);


                    return;
                }

                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "CHECKEDDUPMRRNO", mMRFNO, "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0);


                else
                {

                    DataView dv1 = ds2.Tables[0].DefaultView;
                    dv1.RowFilter = ("reqno <>'" + mREQNO + "'");
                    DataTable dt = dv1.ToTable();
                    if (dt.Rows.Count == 0)
                        ;
                    else
                    {
                        this.txtMRFNo.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Found Duplicate M.R.F No');", true);


                        //this.ddlPrevReqList.Items.Clear();
                        return;
                    }
                }
            }


            //Log Entry

            DataTable dtuser = (DataTable)Session["tblUserReq"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPostedDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string PostedByid = (this.Request.QueryString["InputType"] == "Entry") ? userid : (this.Request.QueryString["InputType"] == "FxtAstEntry" || this.Request.QueryString["InputType"] == "LCEntry") ? userid
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["InputType"] == "Entry") ? Terminal : (this.Request.QueryString["InputType"] == "FxtAstEntry" || this.Request.QueryString["InputType"] == "LCEntry") ? Terminal
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["InputType"] == "Entry") ? Sessionid : (this.Request.QueryString["InputType"] == "FxtAstEntry" || this.Request.QueryString["InputType"] == "LCEntry") ? Sessionid
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string PostedDat = (this.Request.QueryString["InputType"] == "Entry") ? Date : (this.Request.QueryString["InputType"] == "FxtAstEntry" || this.Request.QueryString["InputType"] == "LCEntry") ? Date
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? Date : (tblPostedSession == "") ? Date : tblPostedDat;
            // FxtAstChecked
            string chkPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["reqchkid"].ToString();

            string chkPostedDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : (dtuser.Rows[0]["reqchkdat"] == "") ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["reqchkdat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string chkApproved = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["reqchked"].ToString();



            string ChkByid = (this.Request.QueryString["InputType"] == "FxtAstAuth") ? userid : chkPostedByid;
            string Chkdat = (this.Request.QueryString["InputType"] == "FxtAstAuth") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : chkPostedDat;
            string chked = (this.Request.QueryString["InputType"] == "FxtAstAuth") ? "Ok" : chkApproved;

            ///Review
            string revPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["reviewid"].ToString();

            string revPostedDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : (dtuser.Rows[0]["reviewdat"] == "") ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["reviewdat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string revPTrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["reviewtrm"].ToString();
            string revPSeson = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["reviewses"].ToString();




            string RevByid = (this.Request.QueryString["InputType"] == "ReqReview") ? userid : revPostedByid;
            string CRevdat = (this.Request.QueryString["InputType"] == "ReqReview") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : revPostedDat;
            string revTrmid = (this.Request.QueryString["InputType"] == "ReqReview") ? Terminal : revPTrmid;
            string revSeson = (this.Request.QueryString["InputType"] == "ReqReview") ? Sessionid : revPSeson;

            //FxtAstApproval

            string ApprovByid = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry" || this.Request.QueryString["InputType"] == "LCEntry") ? "" : ((this.Request.QueryString["InputType"] == "FxtAstAuth") || (this.Request.QueryString["InputType"] == "ReqReview")) ? "" : userid;
            string approvdat = (this.Request.QueryString["InputType"] == "Entry") ? "01-Jan-1900" : (this.Request.QueryString["InputType"] == "FxtAstEntry" || this.Request.QueryString["InputType"] == "LCEntry") ? "01-Jan-1900" : ((this.Request.QueryString["InputType"] == "FxtAstAuth") || (this.Request.QueryString["InputType"] == "ReqReview")) ? "01-Jan-1900"
                : System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Approvtrmid = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry" || this.Request.QueryString["InputType"] == "LCEntry") ? "" : ((this.Request.QueryString["InputType"] == "FxtAstAuth") || (this.Request.QueryString["InputType"] == "ReqReview")) ? "" : Terminal;
            string ApprovSession = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry" || this.Request.QueryString["InputType"] == "LCEntry") ? "" : ((this.Request.QueryString["InputType"] == "FxtAstAuth") || (this.Request.QueryString["InputType"] == "ReqReview")) ? "" : Sessionid;






            string approved = (this.Request.QueryString["InputType"] == "Approval") ? "Ok" : (this.Request.QueryString["InputType"] == "FxtAstApproval") ? "Ok" : (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["approved"].ToString();

            string rType = (this.Request.QueryString["InputType"] == "LCEntry") ? "LC" : "";

            //////


            string mPACTCODE = this.ddlProject.SelectedValue.ToString().Trim();
            //string mFLRCOD = this.ddlFloor.SelectedValue.ToString().Trim();
            string mREQUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mEDDAT = this.GetStdDate(this.txtExpDeliveryDate.Text.Trim()); // DateTime.Today.ToString("dd-MMM-yyyy");
            string mREQBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string Deptcode = this.ddlDeptCode.SelectedValue.ToString();
            string Deptcode2 = this.ddlDeptCode2.SelectedValue.ToString();

            //string orderno = this.ddlOrder.SelectedValue.ToString();

            string mslccod = "";// (orderno.Length==0)?"":this.ddlOrder.SelectedValue.ToString().Substring(0, 12);
            string rOrder = "";// (orderno.Length == 0) ? "" : this.ddlOrder.SelectedValue.ToString().Substring(12);
            string mREQNAR = this.txtReqNarr.Text.Trim();
            string Curcode = this.ddlCurrency.SelectedValue.ToString();
            this.ddlOrder_SelectedIndexChanged(null, null);
            string pType = this.ddlPurType.SelectedValue.ToString();
            if (this.ddlPurType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Select a Purchase Type');", true);
                return;
            }
            string headsupp = this.ddlSupplier.SelectedValue.ToString() ?? "000000000000";
            string nSupcode = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry" || this.Request.QueryString["InputType"].ToString() == "LCEntry") ? headsupp == "" ? "000000000000" : headsupp : this.ddlSup.SelectedValue.ToString();
            string Currency = this.ddlCurrency.SelectedValue.ToString();
            string Season = this.DdlSeason.SelectedValue.ToString();
            string ChckJob = this.ChckJob.Checked.ToString();


            bool result = purData.UpdateTransInfo01(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQB", mREQNO, mREQDAT, mPACTCODE, rType, mREQUSRID,
                mAPPRUSRID, mAPPRDAT, mEDDAT, mREQBYDES, mAPPBYDES, mMRFNO, mREQNAR, PostedByid, Posttrmid, PostSession, ApprovByid, approvdat, Approvtrmid,
                ApprovSession, PostedDat, approved, Deptcode, mslccod, Curcode, Deptcode2, ChkByid, Chkdat, chked, pType, nSupcode, RevByid, CRevdat, revTrmid,
                revSeson, rOrder, Season, ChckJob);  ///////////////////, , , 
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }
            ///Doc Upload
            DataTable dtd = (DataTable)Session["tblAttDocs"];
            DataSet ds1d = new DataSet("ds1");
            ds1d.Merge(dtd);
            ds1d.Tables[0].TableName = "tbl1";

            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "REQUISITIONATTACHEDDOCUMENT", ds1d, null, null, mREQNO);
            if (!resulta)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);


                return;
            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                double atprqty = Convert.ToDouble(tbl1.Rows[i]["atprqty"]);
                double mPREQTY = Convert.ToDouble(tbl1.Rows[i]["preqty"]);
                double mAREQTY = Convert.ToDouble(tbl1.Rows[i]["areqty"]);
                double mConRate = Convert.ToDouble(tbl1.Rows[i]["conrate"]);
                string mREQRAT = tbl1.Rows[i]["reqrat"].ToString();
                string mPSTKQTY = tbl1.Rows[i]["stkqty"].ToString();
                string mEXPUSEDT = tbl1.Rows[i]["expusedt"].ToString();
                string mREQNOTE = tbl1.Rows[i]["reqnote"].ToString();
                string PursDate = tbl1.Rows[i]["pursdate"].ToString();
                string Lpurrate = tbl1.Rows[i]["lpurrate"].ToString();
                string storecode = tbl1.Rows[i]["storecode"].ToString();
                string ptype = tbl1.Rows[i]["ptype"].ToString();
                string budget = tbl1.Rows[i]["budget"].ToString();
                string pkgsize = tbl1.Rows[i]["pkgsize"].ToString();
                string bomid = tbl1.Rows[i]["bomid"].ToString();
                string rfgqty = Convert.ToDouble("0" + tbl1.Rows[i]["rfgqty"].ToString()).ToString();
                if (mPREQTY >= mAREQTY)
                {
                    result = purData.UpdateTransInfo01(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQA",
                                mREQNO, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mConRate.ToString(), mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                                PursDate, Lpurrate, storecode, ptype, budget, pkgsize, Currency, atprqty.ToString(), bomid, rfgqty);
                    //result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQA",
                    //            mREQNO, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                    //            PursDate, Lpurrate, storecode, "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

                        return;

                    }
                    else
                    {


                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);




                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Aprove Qty Must be Less Or Equal  Req. Qty');", true);


                    return;

                }

            }

            DataSet ds1x = new DataSet("ds1");
            ds1x.Tables.Add(tbl1);
            this.XmlDataInsert(mREQNO, mPACTCODE, ds1x);

            this.SaveReqDesc();
            DataTable dt1 = (DataTable)ViewState["tblreqdesc"];


            foreach (DataRow dr in dt1.Rows)
            {
                string mTERMSID = dr["termsid"].ToString().Trim();
                string mTERMSSUBJ = dr["termssubj"].ToString().Trim();
                string mTERMSDESC = dr["termsdesc"].ToString().Trim();
                string mTERMSRMRK = dr["termsrmrk"].ToString().Trim();
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQC",
                                mREQNO, mTERMSID, mTERMSSUBJ, mTERMSDESC, mTERMSRMRK, "", "", "", "",
                                "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);


                    return;
                }
            }

            if (this.Request.QueryString["InputType"] == "FxtAstApproval")
            {

                string Supcode = this.ddlSup.SelectedValue.ToString();
                if (this.ddlPurType.SelectedValue == "25003")
                {

                    DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "APPROVALCASHPURCHASE", mREQNO, Supcode, "25003", Date, userid, Terminal, Sessionid, Date);
                }
                else if ((tbl1.Rows[0]["budget"].ToString() == "48001") || (tbl1.Rows[0]["budget"].ToString() == "48003"))
                {
                    //if (Supcode == "000000000000")
                    //    return;
                    string purtyp = (this.ddlPurType.SelectedValue.ToString().Length > 0) ? this.ddlPurType.SelectedValue.ToString() : "25001";
                    DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "APPROVALCASHPURCHASE", mREQNO, Supcode, purtyp, Date, userid, Terminal, Sessionid, Date);
                }

            }

            this.txtCurReqDate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);


            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Material Requisition";
                string eventdesc = "Update Reqisition";
                string eventdesc2 = "Req No- " + mREQNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

                string qtype = this.Request.QueryString["InputType"].ToString();
                //string esubject = String.Empty;
                //string url = String.Empty;
                //string bodyContent = string.Empty;
                if (qtype == "FxtAstEntry")
                {
                    //esubject = "Request for Check Requisition";               
                    //url = "http://202.0.94.49/Login.aspx";
                    //bodyContent = "Dear Sir, </br> A Requisition has Just Arrived, Please click  <b> <a href='" + url +
                    //              "' target='_blank'>" + mREQNO + " </a> </b> on the link to approval";

                    //this.ConfimMail("1002016", esubject, url, bodyContent);

                }
                else if (qtype == "FxtAstAuth")
                {
                    //esubject = "Request for Requisition Approval";
                    //url = "http://202.0.94.49/Login.aspx";
                    //bodyContent = "Dear Sir, </br> A Requisition has Just Arrived, Please click  <b> <a href='" + url +
                    //              "' target='_blank'>" + mREQNO + " </a> </b> on the link to approval";

                    //this.ConfimMail("3402009", esubject, url, bodyContent);


                    string esubject = "Requisition Check Complete! Request to Review";
                    string url = "http://202.0.94.49/F_10_Procur/RptPurInterfaceLocal.aspx";
                    string bodyContent = "Dear Sir, </br>A New Requisition Generate, Please click  <b> <a href='" + url +
                                    "' target='_blank'>" + mREQNO + " </a> </b> on the link to Requisition Review";

                    if (CommonClass.ConfimMail("1002017", esubject, url, bodyContent) == true)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully";
                    }



                }
                else if (qtype == "ReqReview")
                {
                    //esubject = "Request for Requisition Approval";
                    //url = "http://202.0.94.49/Login.aspx";
                    //bodyContent = "Dear Sir, </br> A Requisition has Just Arrived, Please click  <b> <a href='" + url +
                    //              "' target='_blank'>" + mREQNO + " </a> </b> on the link to approval";

                    //this.ConfimMail("3402009", esubject, url, bodyContent);


                    string esubject = "Requisition Review Complete! Request to Requisition Approval";
                    string url = "http://202.0.94.49/F_34_Mgt/RptAdminInterface.aspx";
                    string bodyContent = "Dear Sir, </br>A New Requisition Generate, Please click  <b> <a href='" + url +
                                    "' target='_blank'>" + mREQNO + " </a> </b> on the link to Requisition Approval";

                    if (CommonClass.ConfimMail("3402009", esubject, url, bodyContent) == true)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully";
                    }



                }
                else if (qtype == "FxtAstApproval")
                {
                    // this.ConfimMail("1002016");

                }

                else
                {
                    //
                }

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


            //ds.Tables[0].Columns.Add("delbyid", typeof(string));
            //ds.Tables[0].Columns.Add("delseson", typeof(string));
            //ds.Tables[0].Columns.Add("deldate", typeof(DateTime));

            //ds.Tables[0].Rows[0]["delbyid"] = usrid;
            //ds.Tables[0].Rows[0]["delseson"] = session;
            //ds.Tables[0].Rows[0]["deldate"] = Date;

            //ds1.Merge(ds.Tables[1]);
            //ds1.Tables[1].TableName = "tbl2";

            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, Inqno, Styleid);

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
        protected void lbtnUpdateApp_Click(object sender, EventArgs e)
        {


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);


                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATECHKAUTH", "App", mREQNO, userid, Date, Terminal, Sessionid);


            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated successfully');", true);


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

            }


            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Material Requisition";
                string eventdesc = "Approved Reqisition";
                string eventdesc2 = "Req No- " + mREQNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }
        protected void lbtnUpdateAudit_Click(object sender, EventArgs e)
        {


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATECHKAUTH", "Auth", mREQNO, userid, Date, Terminal, Sessionid);


            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated successfully');", true);


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);


            }

            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Material Requisition";
                string eventdesc = "Auth/Audit Reqisition";
                string eventdesc2 = "Req No- " + mREQNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void SaveReqDesc()
        {

            DataTable dt = (DataTable)ViewState["tblreqdesc"];

            for (int i = 0; i < this.gvDescrip.Rows.Count; i++)
            {
                string trmdesc = ((TextBox)this.gvDescrip.Rows[i].FindControl("txtgvDesc")).Text.Trim();
                dt.Rows[i]["termsdesc"] = trmdesc;
            }


            ViewState["tblreqdesc"] = dt;

        }


        protected void gvResInfo_DataBind()
        {
            //this.gvReqInfo.Columns[16].HeaderText = "Proposed Rate <br> [" + this.ddlCurrency.SelectedItem.ToString() + "]";
            //this.gvReqInfo.Columns[17].HeaderText = "Req. Amount <br> [" + this.ddlCurrency.SelectedItem.ToString() + "]";

            //this.gvReqInfo.Columns[19].HeaderText = "Proposed Rate <br> [" + this.ddlCurrency.SelectedItem.ToString() + "]";
            //this.gvReqInfo.Columns[20].HeaderText = "Total Amount <br> [" + this.ddlCurrency.SelectedItem.ToString() + "]";

            this.gvReqInfo.Columns[20].HeaderText = "Proposed Rate <br> [" + this.ddlCurrency.SelectedItem.ToString() + "]";
            if (this.GetCompCode() == "5305")
            {
                this.gvReqInfo.Columns[28].Visible = false;
            }

            this.gvReqInfo.Columns[21].HeaderText = "Total Amount <br> [" + this.ddlCurrency.SelectedItem.ToString() + "]";

            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            this.gvReqInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvReqInfo.DataSource = tbl1;
            this.gvReqInfo.DataBind();

            if (Request.QueryString["InputType"].ToString() == "Approval") // "Entry"
            {
                for (int i = 0; i < this.gvReqInfo.Rows.Count; i++)
                {
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvReqQty")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvResRat")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvUseDat")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvStokQty")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvReqNote")).ReadOnly = true;
                }
            }

            this.lbtnResFooterTotal_Click(null, null);
        }

        //protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.Session_tblReq_Update();
        //    this.gvReqInfo.PageIndex = ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
        //    this.gvResInfo_DataBind();
        //}
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)ViewState["tblReq"];

            //((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTReqAmt")).Text =
            //    Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(preqamt)", "")) ?
            //        0.00 : tbl1.Compute("Sum(preqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text =
               Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(preqamt)", "")) ?
               0.00 : tbl1.Compute("Sum(preqamt)", ""))).ToString("#,##0.000;(#,##0.000); ");

            ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTbdtAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(bdtamt)", "")) ?
                0.00 : tbl1.Compute("Sum(bdtamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


            ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterReqQty")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(bdtamt)", "")) ?
                0.00 : tbl1.Compute("Sum(preqty)", ""))).ToString("#,##0.0000;(#,##0.0000); ");

        }
        private void Session_tblReq_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            int TblRowIndex2;

            double conRate = Convert.ToDouble(this.lblConRate.Text);
            for (int j = 0; j < this.gvReqInfo.Rows.Count; j++)
            {

                TblRowIndex2 = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + j;
                double dgvBgdQty = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["useablestk"]);
                double dgvReqQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text.Trim()));
                if (this.chkneBudget.Checked)
                {
                    if (dgvBgdQty < dgvReqQty)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Within the Budget');", true);
                        break;

                    }

                }



                double dgvApprQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvappQty")).Text.Trim()));
                double dgvReqRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvResRat")).Text.Trim()));
                //double dgvStokQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvStokQty")).Text.Trim()));
                string dgvUseDat = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvUseDat")).Text.Trim();
                string dgvSupDat = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvpursupDat")).Text.Trim();
                string dgvReqNote = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqNote")).Text.Trim();
                double dgvReqAmt = dgvReqQty * dgvReqRat;
                double dgvApprAmt = dgvApprQty * dgvReqRat;
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text = dgvReqQty.ToString("#,##0.0000;(#,##0.0000); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvappQty")).Text = dgvApprQty.ToString("#,##0.0000;(#,##0.0000); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvResRat")).Text = dgvReqRat.ToString("#,##0.000000;(#,##0.000000); ");
                // ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvStokQty")).Text = dgvStokQty.ToString("#,##0.000;(#,##0.000); ");
                //((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTResAmt")).Text = dgvReqAmt.ToString("#,##0.000;(#,##0.000); ");
                ((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTAprAmt")).Text = dgvReqAmt.ToString("#,##0.000000;(#,##0.000000); ");
                ((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTbdtAmt")).Text = ((dgvReqQty * dgvReqRat) * conRate).ToString("#,##0.00;(#,##0.00); ");

                string ddlType = ((DropDownList)this.gvReqInfo.Rows[j].FindControl("ddlType")).SelectedValue.Trim().ToString();
                string ddlBudget = ((DropDownList)this.gvReqInfo.Rows[j].FindControl("ddlBudget")).SelectedValue.Trim().ToString();

                string pkgsize = ((TextBox)this.gvReqInfo.Rows[j].FindControl("tXTgvPKg")).Text.Trim();


                tbl1.Rows[TblRowIndex2]["preqty"] = dgvReqQty;
                tbl1.Rows[TblRowIndex2]["areqty"] = dgvApprQty;
                tbl1.Rows[TblRowIndex2]["reqrat"] = dgvReqRat;
                tbl1.Rows[TblRowIndex2]["preqamt"] = dgvReqAmt;
                tbl1.Rows[TblRowIndex2]["areqamt"] = dgvApprAmt;
                //tbl1.Rows[TblRowIndex2]["pstkqty"] = dgvStokQty;
                tbl1.Rows[TblRowIndex2]["expusedt"] = dgvUseDat;
                tbl1.Rows[TblRowIndex2]["pursdate"] = dgvSupDat;
                tbl1.Rows[TblRowIndex2]["bdtamt"] = (dgvReqQty * dgvReqRat) * conRate;
                tbl1.Rows[TblRowIndex2]["conrate"] = conRate;

                tbl1.Rows[TblRowIndex2]["reqnote"] = dgvReqNote;
                tbl1.Rows[TblRowIndex2]["ptype"] = ddlType;
                tbl1.Rows[TblRowIndex2]["budget"] = ddlBudget;
                tbl1.Rows[TblRowIndex2]["pkgsize"] = pkgsize;
            }
            ViewState["tblReq"] = tbl1;

        }

        protected void ImgbtnSpecification_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mResCode = this.ddlResList.SelectedValue.ToString();
            if (this.Chckorder.Checked == false )
            {
                 
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "SIRINF_MAT_SPCF_LIST", mResCode, "", "", "", "", "");

                this.ddlResSpcf.DataTextField = "spcfdesc";
                this.ddlResSpcf.DataValueField = "spcfcod";
                this.ddlResSpcf.DataSource = ds1.Tables[0];
                this.ddlResSpcf.DataBind();

                ViewState["tblSpcf"] = ds1.Tables[0];
            }
            else
            {
                DataTable dtspcf = (DataTable)ViewState["tblSpcf"];
                DataView dv = dtspcf.DefaultView;
                dv.RowFilter = "rsircode='" + mResCode + "'";


                this.ddlResSpcf.DataTextField = "spcfdesc";
                this.ddlResSpcf.DataValueField = "spcfcod";
                this.ddlResSpcf.DataSource = dv.ToTable();
                this.ddlResSpcf.DataBind();
            }

        }

        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string mProject = this.ddlProject.SelectedValue.ToString();
            string mSrchTxt = "%" + this.txtResSearch.Text.Trim() + "%";
            string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            if (this.ChckJob.Checked==false)
            {

                DataTable dt = (DataTable)ViewState["tblStoreType"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("actcode='" + mProject + "'");
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
                            SearchInfo = SearchInfo + "left(sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = '" + ar1 + "' ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }



                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "MATCODELIST", mProject, curdate, mSrchTxt, SearchInfo, "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for budget');", true);
                    return;
                }
                ViewState["tblMat"] = ds1.Tables[0];
                //ViewState["tblSpcf"] = ds1.Tables[1];

                DataTable dt1 = ds1.Tables[0].DefaultView.ToTable(true, "rsircode", "rsirdesc1");

                this.ddlResList.DataTextField = "rsirdesc1";
                this.ddlResList.DataValueField = "rsircode";
                this.ddlResList.DataSource = dt1;// ds1.Tables[0];
                this.ddlResList.DataBind();
                this.ImgbtnSpecification_Click(null, null);

                DataTable dt2 = ds1.Tables[0];
                foreach (ListItem item in ddlResList.Items)
                {
                    string rsircode = item.Value;
                    string value1 = (dt2.Select("rsircode='" + rsircode + "'"))[0]["suplink"].ToString().Trim();

                    if (value1 == "000000000000")
                    {
                        item.Attributes.Add("style", "padding:2px;color:red");
                    }

                    else
                    {
                        item.Attributes.Add("style", "padding:2px;color:#000");
                    }
                }
            }
            else
            {

            }


        }



        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnSpecification_Click(null, null);
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {


            string Type = Request.QueryString["InputType"].ToString();
            if (Type == "Entry" || Type == "FxtAstEntry" || Type == "LCEntry" || Type == "MoldReqEntry")
            {
                this.ddlPrevReqList.Items.Clear();
            }
            else
            {
                this.ImgbtnFindReq_Click(null, null);

            }



        }
        protected void gvReqInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblReq"];
            string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string rescode = ((Label)this.gvReqInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            string spcfcod = ((Label)this.gvReqInfo.Rows[e.RowIndex].FindControl("lblgvSpcfCod")).Text.Trim();
            string bomid = ((Label)this.gvReqInfo.Rows[e.RowIndex].FindControl("lblgvBomid")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQFORSPCRES",
                        mREQNO, rescode, spcfcod, bomid, "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {
                if (ConstantInfo.LogStatus == true)
                {

                    string eventtype = "Material Requisition Item Removed";
                    string eventdesc = "Delete Item from Requisition";
                    string eventdesc2 = "Req No- " + mREQNO + ", Item No"+ rescode+spcfcod;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                int rowindex = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("rsircode<>''");
                ViewState["tblReq"] = dv.ToTable();
                this.gvResInfo_DataBind();
            }

        }
        //protected void lnkDeleteReqNo_Click(object sender, EventArgs e)
        //{

        //    string comcod =this.GetCompCode();
        //    DataTable dt = (DataTable)Session["tblReq"];
        //    string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

        //    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQNO",  mREQNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
        //    if (!result) 
        //    {
        //        ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Failed";
        //        return;

        //    }

        //    ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Successfully";
        // }

        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string fxtast = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "LCEntry") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "FxtAstAuth") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "ReqReview") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "ReqEdit") ? "ReqEdit" : "";

            string prjcode = ((Request.QueryString["InputType"].ToString() == "Approval") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "ReqEdit") ? this.ddlProject.SelectedValue.ToString()
                : ((Request.QueryString["InputType"].ToString() == "FxtAstAuth") || (Request.QueryString["InputType"].ToString() == "ReqReview")) ? this.ddlProject.SelectedValue.ToString() : "") + "%";

            string mrfno = (this.Request.QueryString["genno"].ToString().Length == 0) ? "%" : this.Request.QueryString["genno"].ToString() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETPREVREQLIST", CurDate1,
                          prjcode, fxtast, mrfno, "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevReqList.Items.Clear();
            this.ddlPrevReqList.DataTextField = "reqno1";
            this.ddlPrevReqList.DataValueField = "reqno";
            this.ddlPrevReqList.DataSource = ds1.Tables[0];
            this.ddlPrevReqList.DataBind();


            if (this.Request.QueryString["genno"].ToString().Length != 0)
            {
                this.ddlPrevReqList.SelectedValue = this.Request.QueryString["genno"].ToString();
                this.lbtnOk_Click(null, null);
                btnShowimg_OnClick(null, null);
            }

        }
        protected void lbtnResFooterDelete_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblReq"];
            string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQNO", mREQNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Deleted Failed');", true);


                return;

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Deleted Successfully');", true);



            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Material Requisition";
                string eventdesc = "Delete Requisition";
                string eventdesc2 = "Req No- " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void ImgbtnFindProjectName_Click(object sender, EventArgs e)
        {
            this.Load_Project_Combo();
        }
        protected void ImgbtnReqse_Click(object sender, EventArgs e)
        {

        }



        protected void gvReqInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            string Storecode = ((DropDownList)this.gvReqInfo.Rows[e.RowIndex].FindControl("ddlStorename")).SelectedValue.ToString();
            string StoreDesc = ((DropDownList)this.gvReqInfo.Rows[e.RowIndex].FindControl("ddlStorename")).SelectedItem.Text.Trim();
            double dgvReqRat = Convert.ToDouble("0" + ((TextBox)this.gvReqInfo.Rows[e.RowIndex].FindControl("txtgvResRat")).Text.Trim());

            int index = (this.gvReqInfo.PageIndex) * this.gvReqInfo.PageSize + e.RowIndex;
            tbl1.Rows[index]["storecode"] = Storecode;
            tbl1.Rows[index]["storedesc"] = StoreDesc;
            tbl1.Rows[index]["reqrat"] = dgvReqRat;
            ViewState["tblReq"] = tbl1;
            this.gvReqInfo.EditIndex = -1;
            this.gvResInfo_DataBind();
        }
        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            string bomid = this.ddlOrder.SelectedValue.ToString();
            string mResCode = this.ddlResList.SelectedValue.ToString();
            string Specification = this.ddlResSpcf.SelectedValue.ToString();
            string suppplier = this.ddlSupplier.SelectedValue.ToString();
            if (Specification == "AAAAAAAAAAAA")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Specification');", true);


                return;
            }
            double conRate = Convert.ToDouble(this.lblConRate.Text);

            DataTable tbl2 = (DataTable)ViewState["tblMat"];
            DataView dv = tbl2.DefaultView;
            dv.RowFilter = "ssircode like '%" + suppplier + "%'";
            tbl2 = dv.ToTable();
            DataRow[] dr3 = tbl1.Select("rsircode = '" + mResCode + "' and spcfcod='" + Specification + "' and bomid='" + bomid + "'");
            if (dr3.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                    //dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                    //dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.ToString();
                    dr1["bomid"] = (this.Chckorder.Checked == false) ? "0000000000" : tbl2.Rows[i]["bomid"].ToString();
                    dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();
                    DataTable tbl3 = (DataTable)ViewState["tblSpcf"];
                    DataRow[] dr4 = tbl3.Select("rsircode = '" + tbl2.Rows[i]["rsircode"].ToString() + "' and spcfcod='" + tbl2.Rows[i]["spcfcod"].ToString() + "'");

                    //dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();

                    dr1["spcfdesc"] = (dr4.Count() == 0) ? "" : dr4[0]["spcfdesc1"];
                    dr1["desc1"] = (dr4.Count() == 0) ? "" : dr4[0]["desc1"];
                    dr1["desc2"] = (dr4.Count() == 0) ? "" : dr4[0]["desc2"];
                    dr1["desc3"] = (dr4.Count() == 0) ? "" : dr4[0]["desc3"];
                    dr1["desc4"] = (dr4.Count() == 0) ? "" : dr4[0]["desc4"];
                    dr1["desc5"] = (dr4.Count() == 0) ? "" : dr4[0]["desc5"];

                    dr1["rsirdesc1"] = tbl2.Rows[i]["rsirdesc1"].ToString();


                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["stkqty"] = Convert.ToDouble(tbl2.Rows[i]["stkqty"]).ToString();
                    dr1["minstkqty"] = Convert.ToDouble(tbl2.Rows[i]["minstkqty"]).ToString();
                    dr1["useablestk"] = 0.00;
                    dr1["atprqty"] = Convert.ToDouble(tbl2.Rows[i]["reqqty"]).ToString();
                    dr1["preqty"] = Convert.ToDouble(tbl2.Rows[i]["reqqty"]).ToString();
                    dr1["areqty"] = Convert.ToDouble(tbl2.Rows[i]["reqqty"]).ToString();
                    dr1["lpurrate"] = 0;
                    dr1["reqrat"] = Convert.ToDouble(tbl2.Rows[i]["sirval"]).ToString();
                    dr1["preqamt"] = Convert.ToDouble(tbl2.Rows[i]["reqqty"]) * Convert.ToDouble(tbl2.Rows[i]["sirval"]);
                    dr1["areqamt"] = 0;
                    dr1["bdtamt"] = Convert.ToDouble(tbl2.Rows[i]["reqqty"]) * Convert.ToDouble(tbl2.Rows[i]["sirval"]) * Convert.ToDouble(this.lblConRate.Text.Trim());

                    dr1["conrate"] = conRate;
                    //  dr1["pstkqty"] = 0;
                    dr1["expusedt"] = "";// DateTime.Today;
                    dr1["pursdate"] = "";// DateTime.Today;
                    dr1["reqnote"] = "";
                    dr1["storecode"] = "";
                    dr1["ptype"] = "Others";
                    dr1["budget"] = tbl2.Rows[i]["budget"].ToString();// dr3[0]["budget"]; "yes";
                    dr1["pkgsize"] = "0";
                    dr1["rfgqty"] = Convert.ToDouble("0" + TxtReqQty.Text.Trim()).ToString();
                    dr1["overflow"] = tbl2.Rows[i]["overflow"].ToString();
                    tbl1.Rows.Add(dr1);
                }
            }
            //else
            //{
            //    dr2[0]["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
            //    dr2[0]["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
            //}
            ViewState["tblReq"] = this.HiddenSameData(tbl1);
            this.gvResInfo_DataBind();




        }
        protected void gvReqInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                DataTable dtnominated = (DataTable)ViewState["tblnominated"];
                DropDownList ddlBudgetHead = (DropDownList)e.Row.FindControl("ddlBudgetHead");
                ddlBudgetHead.DataTextField = "gdesc";
                ddlBudgetHead.DataValueField = "gcod";
                ddlBudgetHead.DataSource = dtnominated;
                ddlBudgetHead.DataBind();
                ddlBudgetHead.SelectedValue = "00000";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvuseablestkqty = (Label)e.Row.FindControl("lblgvuseablestkqty");


                double usestkqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "useablestk"));

                if (usestkqty < 0.00)
                {
                    lblgvuseablestkqty.Style.Add("color", "red");
                }

                DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
                ddlType.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ptype"));

                bool overflow = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "overflow"));

                if (overflow == true)
                {
                    ((TextBox)e.Row.FindControl("txtgvReqQty")).BorderColor = System.Drawing.Color.Red;
                    ((TextBox)e.Row.FindControl("txtgvReqQty")).BorderWidth = 1;
                    ((TextBox)e.Row.FindControl("txtgvReqQty")).ToolTip = "QTY Overflow as per your Requirment";


                }

                DataTable dtnominated = (DataTable)ViewState["tblnominated"];
                DropDownList ddlBudget = (DropDownList)e.Row.FindControl("ddlBudget");
                ddlBudget.DataTextField = "gdesc";
                ddlBudget.DataValueField = "gcod";
                ddlBudget.DataSource = dtnominated;
                ddlBudget.DataBind();
                ddlBudget.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "budget"));

                //DropDownList ddlnominated = (DropDownList)e.Row.FindControl("ddlnominated");
                //ddlnominated.DataTextField = "gdesc";
                //ddlnominated.DataValueField = "gcod";
                //ddlnominated.DataSource = dtnominated;
                //ddlnominated.DataBind();
                //ddlnominated.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "nrcod"));

            }
        }
        private void AutoSavePDF()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_07_Inv.RptPurReq();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();
            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlProject.SelectedItem.Text.Trim().Substring(14);
            //DataTable dt = (DataTable)ViewState["tblreqdesc"];
            //TextObject txtbuildno = rptstk.ReportDefinition.ReportObjects["txtbuildno"] as TextObject;
            //txtbuildno.Text = dt.Rows[0]["termsdesc"].ToString();
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["txtfloorno"] as TextObject;
            //txtfloorno.Text = dt.Rows[1]["termsdesc"].ToString();

            //TextObject txttoamt = rptstk.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            //txttoamt.Text = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();
            //TextObject txttoamt02 = rptstk.ReportDefinition.ReportObjects["txttoamt02"] as TextObject;
            //txttoamt02.Text = ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text.Trim();

            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)ViewState["tblReq"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            //string apppath = Server.MapPath("~") + "\\EmailDoc" + "\\" + mREQNO + ".pdf"; ;
            //rptstk.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, apppath);

        }
        protected void lblemailsend_Click(object sender, EventArgs e)
        {
            //this.AutoSavePDF();

            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //string comcod = this.GetCompCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString().Trim();
            //string trmid = hst["compname"].ToString().Trim();
            //string usrSession = hst["session"].ToString().Trim();

            //string usrid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3); 
            //DataSet dssmtpandmail = this.purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


            //string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "MATREQEMAILSEND", mREQNO, usrid, "", "", "", "", "", "", "");

            //string subject = "Request Quotation for Requistion";


            ////SMTP
            //string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            //int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            //SmtpClient client = new SmtpClient(hostname, portnumber);

            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            ////client.EnableSsl = true;
            //client.EnableSsl = false;
            //string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            //string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
            //client.UseDefaultCredentials = false;
            //client.Credentials = credentials;

            /////////////////////////

            //MailMessage msg = new MailMessage();
            //msg.From = new MailAddress(frmemail);


            //msg.To.Add(new MailAddress(ds1.Tables[0].Rows[0]["mailid"].ToString()));

            //for (int i = 1; i < ds1.Tables[0].Rows.Count; i++)
            //{
            //    if (ds1.Tables[0].Rows[i]["mailid"].ToString().Trim() != "")
            //        msg.Bcc.Add(new MailAddress(ds1.Tables[0].Rows[i]["mailid"].ToString().Trim()));
            //}
            //msg.Subject = subject;
            //msg.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;

            //string apppath = Server.MapPath("~") + "\\EmailDoc" + "\\" + mREQNO + ".pdf"; ;

            //attachment = new System.Net.Mail.Attachment(apppath);
            //msg.Attachments.Add(attachment);

            ////////////////SMS----------------------
            //string Phone = "";
            //foreach (DataRow dr in ds1.Tables[0].Rows)
            //{
            //    Phone += ";" + dr["phone"].ToString().Trim();
            //}

            ////Phone = ds1.Tables[0].Rows[0]["phone"].ToString().Trim();
            //string usrname = ds1.Tables[0].Rows[0]["usrname"].ToString().Trim();
            //string Note = "Dear Supplier, Requsition No: " + mREQNO + " Send for Offer.";

            //SmsApps.SendSms(comcod, Note, comnam + " , " + usrname, usrid + ", " + trmid + ", " + usrSession, Phone);

            //msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px; font-size:14px; text-align:justify;'>" + "Dear sir," + "<br/>" + "please find the attached file" + "</pre></body></html>");
            //try
            //{
            //    client.Send(msg);
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";



            //}



            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            //}
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void gvReqInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_tblReq_Update();
            this.gvReqInfo.PageIndex = e.NewPageIndex;
            this.gvResInfo_DataBind();
        }
        private void CurrencyInf()
        {
            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;
            this.ddlCurrency.DataValueField = "curcode";
            this.ddlCurrency.DataTextField = "curdesc";
            this.ddlCurrency.DataSource = lstCurryDesc;
            this.ddlCurrency.DataBind();

            if (this.Request.QueryString["InputType"] == "FxtAstEntry")
            {
                this.ddlCurrency.SelectedValue = "001";
                this.ddlCurrency.Enabled = false;
            }
            this.ddlCurrency_SelectedIndexChanged(null, null);
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tcode = "001";
            string fcode = this.ddlCurrency.SelectedValue.ToString();
            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;

            this.lblConRate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
            //double txtpeople = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBudgeted.Rows[j].FindControl("txtpeople")).Text.Trim()));

        }


        protected void Chckorder_CheckedChanged(object sender, EventArgs e)
        {
            if (GetCompCode() == "5301")
            {
                this.ChckJob.Visible = false;
            }
            if (this.Chckorder.Checked == true)
            {
                this.panelorder.Visible = true;
                ChckJob_CheckedChanged(null, null);
            }
            else
            {
                this.ddlOrder.Items.Clear();
                this.panelorder.Visible = false;
            }
        }

        protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState.Remove("tblMat");
            ViewState.Remove("tblSpcf");


            if (this.ChckJob.Checked == false)
            {
                string comcod = this.GetCompCode();
                string mProject1 = this.ddlOrder.SelectedValue.ToString();
                // string mProject = "";//        this.ddlOrder.SelectedValue.ToString().Substring(0,12);
                // string rOrder = "";//        this.ddlOrder.SelectedValue.ToString().Substring(12);

                string mSrchTxt = "%";
                DataTable order = (DataTable)ViewState["tblMsOrder"];
                if (order == null)
                    return;
                DataView dv = order.DefaultView;
                dv.RowFilter = ("bomid='" + mProject1 + "'");
                DataTable dt2 = dv.ToTable();
                //if (dt2.Rows.Count == 0)
                //{

                //    this.panelorder.Visible = false;

                //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No Order Found');", true);
                //    return;
                //}
                string ordrqty = "", reqty = "";
                ordrqty = Convert.ToDouble("0" + dt2.Rows[0]["qty"]).ToString();
                reqty = Convert.ToDouble("0" + TxtReqQty.Text.Trim()).ToString();
                if (dt2.Rows[0]["gendata"].ToString() != "")
                {
                    this.ddlCurrency.SelectedValue = dt2.Rows[0]["gendata"].ToString();
                    this.lblConRate.Text = dt2.Rows[0]["currate"].ToString();
                }
                string purTye = (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "L%" : "%";
                // lc or local requisition show all material when click from order
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "MATCODELIST", mSrchTxt, mProject1, "%", ordrqty, reqty, "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for budget');", true);
                    return;
                }

                ViewState["tblMat"] = ds1.Tables[0];
                ViewState["tblSpcf"] = ds1.Tables[1];

                DataTable dt1 = ds1.Tables[0].DefaultView.ToTable(true, "ssircode", "ssirdesc");
                DataRow dr = dt1.NewRow();
                dr["ssircode"] = "";
                dr["ssirdesc"] = "All";
                dt1.Rows.Add(dr);
                this.ddlSupplier.DataTextField = "ssirdesc";
                this.ddlSupplier.DataValueField = "ssircode";
                this.ddlSupplier.DataSource = dt1;
                this.ddlSupplier.DataBind();
                ddlSupplier_SelectedIndexChanged(null, null);


            }
            else
            {
                string comcod = this.GetCompCode();
                string mProject1 = this.ddlOrder.SelectedValue.ToString();
                // string mProject = "";//        this.ddlOrder.SelectedValue.ToString().Substring(0,12);
                // string rOrder = "";//        this.ddlOrder.SelectedValue.ToString().Substring(12);

                string mSrchTxt = "%";
                DataTable order = (DataTable)ViewState["tblMsOrder"];
                if (order == null)
                    return;
                DataView dv = order.DefaultView;
                dv.RowFilter = ("bomid='" + mProject1 + "'");
                DataTable dt2 = dv.ToTable();
                string ordrqty = "", reqty = "";
                ordrqty = Convert.ToDouble("0" + dt2.Rows[0]["qty"]).ToString();
                reqty = Convert.ToDouble("0" + TxtReqQty.Text.Trim()).ToString();
                if (dt2.Rows[0]["gendata"].ToString() != "")
                {
                    this.ddlCurrency.SelectedValue = dt2.Rows[0]["gendata"].ToString();
                    this.lblConRate.Text = dt2.Rows[0]["currate"].ToString();
                }
                string purTye = (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "L%" : "%";
                // lc or local requisition show all material when click from order
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "MATCODELIST", mSrchTxt, mProject1, "%", ordrqty, reqty, "adf", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for budget');", true);
                    return;
                }

                ViewState["tblMat"] = ds1.Tables[0];
                ViewState["tblSpcf"] = ds1.Tables[1];

                DataTable dt1 = ds1.Tables[0].DefaultView.ToTable(true, "ssircode", "ssirdesc");
                DataRow dr = dt1.NewRow();
                dr["ssircode"] = "";
                dr["ssirdesc"] = "All";
                dt1.Rows.Add(dr);
                this.ddlSupplier.DataTextField = "ssirdesc";
                this.ddlSupplier.DataValueField = "ssircode";
                this.ddlSupplier.DataSource = dt1;
                this.ddlSupplier.DataBind();
                ddlSupplier_SelectedIndexChanged(null, null);
            }
            ///////////bind material and specifications

        }

        public void BindResources()
        {
            string suppplier = this.ddlSupplier.SelectedValue.ToString();
            DataTable dt1 = (DataTable)ViewState["tblMat"];
            DataView dv = dt1.DefaultView;

            dv.RowFilter = "ssircode like '%" + suppplier + "%'";


            dt1 = dv.ToTable(true, "rsircode", "rsirdesc1");



            this.ddlResList.DataTextField = "rsirdesc1";
            this.ddlResList.DataValueField = "rsircode";
            this.ddlResList.DataSource = dt1;
            this.ddlResList.DataBind();

            //  this.lbtnResSpcf_Click(null, null);
            this.ImgbtnSpecification_Click(null, null);
        }
        protected void TxtQuantity_TextChanged(object sender, EventArgs e)
        {
            // DataTable tbl1 = (DataTable)ViewState["tblReq"];

            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;


            TextBox Quntity = (TextBox)this.gvReqInfo.Rows[index].FindControl("txtgvReqQty") as TextBox;
            TextBox lRate = (TextBox)this.gvReqInfo.Rows[index].FindControl("txtgvResRat") as TextBox;
            Label lTotalRate = (Label)this.gvReqInfo.Rows[index].FindControl("lblgvTAprAmt") as Label;
            double qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + Quntity.Text.ToString()));
            double rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + lRate.Text.ToString()));
            double ammount = qty * rate;
            lTotalRate.Text = ammount.ToString();

            this.lbtnResFooterTotal_Click(null, null);

        }

        private void GetPurType()
        {
            string comcod = this.GetCompCode();

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURTYPE", "");
            if (ds == null)
                return;



            ddlPurType.DataSource = ds.Tables[0];
            ddlPurType.DataTextField = "gdesc";
            ddlPurType.DataValueField = "gcod";
            ddlPurType.DataBind();

            this.ddlPurType.SelectedItem.Text = "----Select----";

            if (comcod == "5305")
            {
                this.ddlPurType.SelectedValue = "25004";
            }

        }

        private void GetSupplier()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                return;
            }


            this.ddlSup.DataTextField = "ssirdesc1";
            this.ddlSup.DataValueField = "ssircode";
            this.ddlSup.DataSource = ds1.Tables[0];
            this.ddlSup.DataBind();
        }

        protected void ddlPurType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlSuplist.Visible = true;
            GetSupplier();

        }

        private void CreateTblDocs()
        {
            DataTable mnuTbl1 = new DataTable();
            mnuTbl1.Columns.Add("comcod", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("reqno", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemsurl", Type.GetType("System.String"));
            Session["tblAttDocs"] = mnuTbl1;
        }
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetCompCode();
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            DataTable dt = (DataTable)Session["tblAttDocs"];
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);

            if (AsyncFileUpload1.HasFile)
            {
                Random r = new Random();
                int next = r.Next(0, 9999999);
                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/ReqDoc/") + next + extension);
                Url = next + extension;

                DataRow[] dr2 = dt.Select("itemsurl = '" + Url + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["comcod"] = comcod;
                    dr1["reqno"] = mREQNO;
                    dr1["itemsurl"] = Url;
                    dt.Rows.Add(dr1);
                }


            }
            Session["tblAttDocs"] = dt;

        }

        //private void ConfimMail(string ConId, string esubject, string url, string bodyContent)
        //{
        //    string comcod = this.GetCompCode();
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString().Trim();
        //    string trmid = hst["compname"].ToString().Trim();
        //    string usrSession = hst["session"].ToString().Trim();
        //    string comdd = "ASIT Bangladesh";//hst[""].ToString();
        //    string usrid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);

        //    DataSet ds = this.purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SMSEMAILSETUP", usrid, ConId, "", "", "", "", "", "", "");



        //    string useremail = string.Empty;
        //    string useremail1 = string.Empty;

        //    string compname = string.Empty;


        //    string condate = string.Empty;
        //    //  string endingdate = string.Empty;

        //    //SMTP
        //    string hostname = ds.Tables[0].Rows[0]["smtpid"].ToString();
        //    int portnumber = Convert.ToInt32(ds.Tables[0].Rows[0]["portno"].ToString());
        //    SmtpClient client = new SmtpClient(hostname, portnumber);

        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    //client.EnableSsl = true;
        //    client.EnableSsl = false;
        //    string frmemail = ds.Tables[1].Rows[0]["mailid"].ToString();
        //    string psssword = ds.Tables[1].Rows[0]["mailpass"].ToString();
        //    if (frmemail.Length == 0)
        //        return;

        //    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = credentials;

        //    DataTable dt = ds.Tables[2];

        //    MailMessage msg = new MailMessage();
        //    msg.From = new MailAddress(frmemail);

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        useremail = dt.Rows[i]["mailid"].ToString();
        //        //for (int j = 0; j < dt.Rows.Count; j++)
        //        //{
        //        //    useremail1 = dt.Rows[j]["mailid"].ToString();
        //        //    if (useremail == useremail1)
        //        //    {
        //                msg.To.Add(new MailAddress(useremail));
        //        //    }
        //        //}

        //    }
        //    compname = comnam;


        //    condate = DateTime.Today.ToString("dd.MM.yyyy");
        //    string body = string.Empty;
        //    //string tomail = useremail;

        //    //msg.To.Add(new MailAddress(frmemail));

        //    //msg.Bcc.Add(new MailAddress(tomail));


        //    msg.Subject = esubject;
        //    msg.IsBodyHtml = true;
        //    using (StreamReader reader = new StreamReader(Server.MapPath("~/mail.html")))

        //    {

        //        body = reader.ReadToEnd();

        //    }



        //    body = body.Replace("{@Comp1}", "Dear Recipient");
        //    body = body.Replace("{desc0}", ConId);
        //    body = body.Replace("{msghead}", esubject);
        //    body = body.Replace("{tblHead}", "Contract # ");
        //    body = body.Replace("{TitelDesc1}", "Contract Subject");
        //    body = body.Replace("{Desc1}", esubject);
        //    body = body.Replace("{TitelDesc2}", "Company");
        //    body = body.Replace("{Desc2}", "Test");
        //    body = body.Replace("{TitelDesc3}", "Opening Date");
        //    body = body.Replace("{Desc3}", condate);
        //    //body = body.Replace("{TitelDesc4}", "Ending Date");
        //    // body = body.Replace("{Desc4}", endingdate);



        //    body = body.Replace("{bodyContent}", bodyContent);

        //    // body = body.Replace("{address}", " Rangs Babylonia, Level 6-9, 246 Bir Uttam Mir Shawkat Road, Tejgaon I/A, Dhaka-1208, Bangladesh ");

        //    msg.Body = body;


        //    try
        //    {
        //        client.Send(msg);
        //     //   Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "teste()", true);
        //    //   ScriptManager.RegisterStartupScript(this, GetType(), "alert", "FnSuccess();", true);


        //    }
        //    catch (Exception ex)
        //    {
        //        //
        //    }


        //}


        protected void btnShowimg_OnClick(object sender, EventArgs e)
        {

            //DataTable tbl2 = (DataTable)Session["tblAttDocs"];
            string comcod = this.GetCompCode();
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETIATTACHEDDOCS", mREQNO, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            DataTable tbl1 = ds.Tables[0];
            //string Url = "";
            //for (int j = 0; j < tbl1.Rows.Count; j++)
            //{
            //    Url = "../Upload/ReqDoc/" + tbl1.Rows[j]["itemsurl"].ToString().Trim();
            //    DataRow dr1 = tbl2.NewRow();
            //    dr1["comcod"] = comcod;
            //    dr1["reqno"] = tbl1.Rows[j]["reqno"].ToString().Trim();
            //    dr1["itemsurl"] = Url;
            //    tbl2.Rows.Add(dr1);
            //}
            ListViewEmpAll.DataSource = tbl1;
            ListViewEmpAll.DataBind();
            Session["tblAttDocs"] = tbl1;
        }


        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            //string comcod = this.GetCompCode();
            //for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            //{
            //    string mrsno = ((Label)this.ListViewEmpAll.Items[j].FindControl("msrno")).Text.ToString();
            //    string ssircode = ((Label)this.ListViewEmpAll.Items[j].FindControl("ssircode")).Text.ToString();
            //    string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
            //    if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
            //    {
            //        bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "REMOVESURVEYIMG", mrsno, ssircode, "", "", "", "", "", "", "", "", "", "", "");

            //        if (result == true)
            //        {
            //            string filePath = Server.MapPath("~/MFGWEB/");
            //            System.IO.File.Delete(filePath + filesname);

            //            this.lblMesg.Text = " Files Removed ";
            //        }


            //    }




            //}



        }
        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = "../Upload/ReqDoc/" + imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;


                }

            }

        }


        protected void TxtReqQty_TextChanged(object sender, EventArgs e)
        {
            this.ddlOrder_SelectedIndexChanged(null, null);
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindResources();
        }

        protected void ddlBudgetHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            DropDownList ddlnrcode = (DropDownList)sender;
            string comnrcode = ddlnrcode.SelectedValue;

            foreach (DataRow dr in tbl1.Rows)
            {
                dr["budget"] = comnrcode;
            }


            ViewState["tblReq"] = tbl1;
            this.gvResInfo_DataBind();
        }

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Chckorder.Checked)
            {
                this.GetMasterOrder();

            }
        }

        protected void ChckJob_CheckedChanged(object sender, EventArgs e)
        {

            if (this.ChckJob.Checked == true)
            {
                this.GetJobWorkOrder();
                this.ddlOrder_SelectedIndexChanged(null, null);
                this.ddlSupplier.Visible = false;
                this.Label5.Visible = false;
            }
            else
            {
                this.GetMasterOrder();
                this.panelorder.Visible = true;
                this.ddlSupplier.Visible = true;
                this.Label5.Visible = true;
                this.ddlOrder_SelectedIndexChanged(null, null);
            }
        }

        protected void LbtnWithImage_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string sircode = this.ddlResList.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "SIRINF_MAT_SPCF_LIST", sircode, "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvItemdetails.DataSource = ds1.Tables[0];
            this.gvItemdetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void LbtnPushGrid_Click(object sender, EventArgs e)
        {
            //List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            //string rdayid = this.dllorderType.SelectedValue.ToString();
            //string style = this.ddlprocode.SelectedValue.ToString().Substring(0, 12);
            //string color = this.ddlprocode.SelectedValue.ToString().Substring(12, 12);
            //string sdino = this.ddlprocode.SelectedValue.ToString().Substring(24, 14);

            //DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            //foreach (GridViewRow row in this.gvStockDetails.Rows)
            //{
            //    int index = row.RowIndex;
            //    if (((CheckBox)this.gvStockDetails.Rows[index].FindControl("chkColItem")).Checked)
            //    {
            //        DataTable stocktbl = (DataTable)Session["tblstock"];

            //        DataRow dr = stocktbl.Rows[index];

            //        string mlccode = dr["mlccod"].ToString();
            //        string mlcdesc = dr["actdesc"].ToString();
            //        string styleid = dr["styleid"].ToString();
            //        string styledesc = dr["styldesc"].ToString();
            //        string colorid = dr["colorid"].ToString();
            //        string colordesc = dr["colordesc"].ToString();
            //        string sizeid = dr["sizeid"].ToString();
            //        string sizedesc = dr["sizedesc"].ToString();
            //        double ordrqty = 0.00;
            //        double prdqty = 0.00;
            //        double rate = Convert.ToDouble(dr["rate"]);
            //        string ordrno = dr["ordrno"].ToString();
            //        string hscode = "";
            //        double balqty = Convert.ToDouble(dr["stockqty"]);
            //        string redayid = dr["odayid"].ToString();
            //        double itmamt = 0.00;
            //        string inqno = "00000000000000";
            //        double inprocqty = 0.0;



            //        var checklist = lst.FindAll(p => p.mlccod == mlccode && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.ordrno == ordrno && p.rdayid == redayid && p.ordno == inqno);
            //        if (checklist.Count > 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist');", true);
            //            continue;
            //        }

            //        lst.Add(new SPEENTITY.C_19_Exp.EClassExpBO.EclassExport(mlccode, mlcdesc, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, ordrqty, rate, ordrno, hscode,
            //            0.00, 0.00, 0.00, 0.00, 0.00, "", 0.00, 0.00, prdqty, balqty, redayid, itmamt, inqno, inprocqty));
            //        ViewState["tblexport"] = lst;
            //    }
            //}

            //string mlccod = this.ddlmlccode.SelectedValue.ToString();
            //string comcod = this.GetComeCode();

            //DataTable Tempdt;
            //DataView Tempdv;
            //DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "GETLCDETINFO", mlccod, rdayid, "", "", "", "", "", "", "");
            //Tempdt = ds2.Tables[0].Copy();
            //Tempdv = Tempdt.DefaultView;
            //Tempdv.RowFilter = ("gcod ='010100101009' or gcod ='010100101010'");
            //Tempdt = Tempdv.ToTable();
            //if ((Tempdt.Rows[0]["gdesc1"].ToString() != "") && (Tempdt.Rows[1]["gdesc1"].ToString() != ""))
            //{
            //    this.Data_Bind();
            //    this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + rdayid;
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Add Conversion Rate');", true);
            //    this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + rdayid;
            //}



            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
           
            double conRate = Convert.ToDouble(this.lblConRate.Text);

            DataTable tbl2 = (DataTable)ViewState["tblMat"];

     
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "'");
            if (dr2.Length == 0)
            {
               
                foreach (GridViewRow row in this.gvItemdetails.Rows)
                {
                    int index = row.RowIndex;
                    if (((CheckBox)this.gvItemdetails.Rows[index].FindControl("chkColItem")).Checked)
                    {
                        DataTable tbl3 = (DataTable)ViewState["tblSpcf"];

                        DataRow dr1 = tbl1.NewRow();
                        dr1["rsircode"] = mResCode;
                        dr1["rsirdesc1"] = this.ddlResList.SelectedItem.ToString();
                        dr1["spcfcod"] = tbl3.Rows[index]["spcfcod"].ToString();
                        DataRow[] dr4 = tbl3.Select("rsircode = '" + mResCode + "' and spcfcod='" + tbl3.Rows[index]["spcfcod"].ToString() + "'");

                        //dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();

                        dr1["spcfdesc"] = (dr4.Count() == 0) ? "" : dr4[0]["spcfdesc1"];
                        dr1["desc1"] = (dr4.Count() == 0) ? "" : dr4[0]["desc1"];
                        dr1["desc2"] = (dr4.Count() == 0) ? "" : dr4[0]["desc2"];
                        dr1["desc3"] = (dr4.Count() == 0) ? "" : dr4[0]["desc3"];
                        dr1["desc4"] = (dr4.Count() == 0) ? "" : dr4[0]["desc4"];
                        dr1["desc5"] = (dr4.Count() == 0) ? "" : dr4[0]["desc5"];

                        DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
                        dr1["rsirunit"] = dr3[0]["rsirunit"];
                        dr1["rsirunit"] = dr3[0]["rsirunit"];
                        dr1["stkqty"] = dr3[0]["stkqty"];
                        dr1["minstkqty"] = dr3[0]["minstkqty"];
                        dr1["useablestk"] = 0.00;
                        dr1["atprqty"] = dr3[0]["reqqty"];
                        dr1["preqty"] = dr3[0]["reqqty"];
                        dr1["areqty"] = dr3[0]["reqqty"];
                        dr1["lpurrate"] = 0;
                        dr1["reqrat"] = dr3[0]["sirval"];
                        dr1["preqamt"] = Convert.ToDouble(dr3[0]["reqqty"]) * Convert.ToDouble(dr3[0]["sirval"]);
                        dr1["areqamt"] = 0;
                        dr1["bdtamt"] = Convert.ToDouble(dr3[0]["reqqty"]) * Convert.ToDouble(dr3[0]["sirval"]) * Convert.ToDouble(this.lblConRate.Text.Trim());

                        dr1["conrate"] = conRate;
                        dr1["expusedt"] = "";// DateTime.Today;
                        dr1["pursdate"] = "";// DateTime.Today;
                        dr1["reqnote"] = "";
                        dr1["storecode"] = "";
                        dr1["ptype"] = "Others";
                        dr1["budget"] = dr3[0]["budget"];
                        dr1["pkgsize"] = "0";
                        dr1["bomid"] = "0000000000";
                        dr1["rfgqty"] = Convert.ToDouble("0" + TxtReqQty.Text.Trim()).ToString();
                        dr1["overflow"] = dr3[0]["overflow"];
                        tbl1.Rows.Add(dr1);
                    }

                   
                }
            }
         
            ViewState["tblReq"] = this.HiddenSameData(tbl1);
            this.gvResInfo_DataBind();
        }
    }
}