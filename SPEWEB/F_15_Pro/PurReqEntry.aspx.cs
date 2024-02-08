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

namespace SPEWEB.F_15_Pro
{
    public partial class PurReqEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                // for Mettroo
                this.txtReqText.Enabled = false;
                // this.ImgbtnReqse.Enabled = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];


                this.Load_Project_Combo();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["InputType"].ToString() == "Entry") ? "Materials Requisition Information Input/Edit Screen"
                    : (Request.QueryString["InputType"].ToString() == "Approval") ? "Materials Requisition Approval Screen"
                    : (Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "Fixed Assets Requisition Information Input/Edit Screen"
                     : (Request.QueryString["InputType"].ToString() == "ReqEdit") ? "Materials Requisition Information Input/Edit Screen" : "Fixed Assets Requisition Approval Screen";

                this.CommonButton();

            }




        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnResFooterTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnResFooterTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Click += new EventHandler(lbtnResFooterDelete_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdateResReq_Click);



            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void CommonButton()
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = true;

            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;




        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void Load_Project_Combo()
        {

            string comcod = this.GetCompCode();
            string fxtast = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "FxtAst" : (this.Request.QueryString["InputType"].ToString() == "ReqEdit") ? "ReqEdit" : "";

            string Aproval = (this.Request.QueryString["InputType"].ToString() == "Approval") ? "Aproval" : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "Aproval" : "";
            string actcode = (this.Request.QueryString["actcode"].ToString() == "") ? "" : this.Request.QueryString["actcode"].ToString();

            string FindProject = this.txtProjectSearch.Text.Trim() + "%";
            //this.txtCurReqDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy ddd");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "PRJCODELIST", FindProject, fxtast, Aproval, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            if (actcode.Length > 0)
            {
                this.ddlProject.SelectedValue = actcode;
                this.ddlProject.Enabled = false;
            }


            this.ddlFloor.DataTextField = "flrdes";
            this.ddlFloor.DataValueField = "flrcod";
            this.ddlFloor.DataSource = ds1.Tables[1];
            this.ddlFloor.DataBind();
            this.lbtnOk.Text = "New";
            this.lbtnOk_Click(null, null);
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.txtSrchMrfNo.Visible = true;
                this.lblpreReq.Visible = true;
                //this.ImgbtnFindReq.Visible = true;
                this.ddlPrevReqList.Visible = true;
                this.ddlPrevReqList.Items.Clear();
                this.ddlProject.Visible = true;
                this.lblddlProject.Visible = false;
                this.ddlFloor.Visible = false;
                this.lblddlFloor.Visible = false;
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
                this.lbtnOk.Text = "Ok";
                if (Request.QueryString["InputType"].ToString() == "Approval" || Request.QueryString["InputType"].ToString() == "FxtAstApproval" || Request.QueryString["InputType"].ToString() == "ReqEdit")
                {

                    this.ddlFloor.Visible = false;
                    this.lblddlFloor.Visible = false;
                    this.lblResList.Visible = false;

                    this.ddlResList.Visible = false;
                    this.ddlResSpcf.Visible = false;
                    this.lbtnSelectRes.Visible = false;
                    this.lblfloor.Visible = false;
                    this.lblddlFloor.Visible = false;
                    this.lblmrfno.Visible = false;
                    this.txtMRFNo.Visible = false;
                    this.lblCurNo.Visible = false;
                    this.lblCurReqNo1.Visible = false;
                    this.txtCurReqNo2.Visible = false;
                    this.txtReqText.Visible = false;
                    this.ImgbtnFindReq_Click(null, null);

                }
                else
                {
                    this.gvReqInfo.Columns[9].Visible = false;
                    this.gvReqInfo.Columns[14].Visible = false;
                }

                return;
            }

            if (Request.QueryString["InputType"].ToString() == "Approval" || Request.QueryString["InputType"].ToString() == "FxtAstApproval" || Request.QueryString["InputType"].ToString() == "ReqEdit")
            {
                this.lblfloor.Visible = false;
                this.lblddlFloor.Visible = false;
                this.lblmrfno.Visible = true;
                this.txtMRFNo.Visible = true;
                this.lblCurNo.Visible = true;
                this.lblCurReqNo1.Visible = true;
                this.txtCurReqNo2.Visible = true;
                this.txtReqText.Visible = true;
            }


            this.txtSrchMrfNo.Visible = false;
            this.lblpreReq.Visible = false;
            this.ddlPrevReqList.Visible = false;
            this.lblddlProject.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.ddlProject.Visible = false;
            this.lblddlProject.Visible = true;
            this.lblddlFloor.Text = this.ddlFloor.SelectedItem.Text.Trim();
            this.ddlFloor.Visible = false;
            this.lblddlFloor.Visible = false;
            this.txtCurReqNo2.ReadOnly = true;
            this.Panel1.Visible = true;
            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.ImgbtnFindRes_Click(null, null);
            this.Get_Requisition_Info();


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
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURREQINFO", mReqNo, CurDate1,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblReq"] = ds1.Tables[0];
            Session["tblUserReq"] = ds1.Tables[1];
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
                return;
            }
            this.txtMRFNo.Text = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            this.lblCurReqNo1.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(0, 6);
            this.txtCurReqNo2.Text = ds1.Tables[1].Rows[0]["reqno1"].ToString().Substring(6, 5);
            this.txtCurReqDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["reqdat"]).ToString("dd.MM.yyyy");


            this.ddlProject.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.ddlFloor.SelectedValue = ds1.Tables[1].Rows[0]["flrcod"].ToString();
            this.lblddlProject.Text = this.ddlProject.SelectedItem.Text.Trim();
            this.lblddlFloor.Text = (this.ddlFloor.Items.Count == 0 ? "YYY" : this.ddlFloor.SelectedItem.Text.Trim());
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["reqbydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtExpDeliveryDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["eddat"]).ToString("dd.MM.yyyy");
            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();
            this.gvResInfo_DataBind();
        }


        private void GetApprQty()
        {
            DataTable dt = (DataTable)Session["tblReq"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double aprqty = Convert.ToDouble(dt.Rows[i]["preqty"]);
                dt.Rows[i]["areqty"] = aprqty;

            }
            Session["tblReq"] = dt;
        }


        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {

            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)Session["tblReq"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                dr1["rsirdesc1"] = this.ddlResList.SelectedItem.Text.Trim().Substring(14);
                dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                DataTable tbl2 = (DataTable)Session["tblMat"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["bbgdqty"] = dr3[0]["bbgdqty"];
                dr1["preqty"] = dr3[0]["bbgdqty"];
                dr1["areqty"] = 0;
                dr1["fcrate"] = dr3[0]["fcrate"];
                dr1["conrate"] = dr3[0]["conrate"]; ;
                dr1["reqrat"] = 0;
                dr1["preqamt"] = 0;
                dr1["areqamt"] = 0;
                dr1["pstkqty"] = 0;
                dr1["expusedt"] = "";
                dr1["reqnote"] = "";
                tbl1.Rows.Add(dr1);
            }
            else
            {
                dr2[0]["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                dr2[0]["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
            }
            Session["tblReq"] = tbl1;
            this.gvResInfo_DataBind();

        }

        protected void lnkSelectAll_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            string mResCode = this.ddlResList.SelectedValue.ToString();

            DataTable tbl1 = (DataTable)Session["tblReq"];
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "'");
            DataTable tbl2 = (DataTable)Session["tblMat"];
            DataRow[] dr3 = tbl2.Select("rsircode='" + mResCode + "'");
            DataView dv = tbl2.DefaultView;
            tbl2 = dv.ToTable();

            if (dr2.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();

                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                    dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                    dr1["rsirdesc1"] = tbl2.Rows[i]["rsirdesc1"].ToString();
                    dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["bbgdqty"] = tbl2.Rows[i]["bbgdqty"].ToString();
                    dr1["preqty"] = tbl2.Rows[i]["bbgdqty"].ToString();
                    dr1["areqty"] = 0.00;
                    dr1["fcrate"] = tbl2.Rows[i]["fcrate"].ToString();
                    dr1["conrate"] = tbl2.Rows[i]["conrate"].ToString();
                    dr1["reqrat"] = 0.00;
                    dr1["preqamt"] = 0;
                    dr1["areqamt"] = 0;
                    dr1["pstkqty"] = 0;
                    dr1["expusedt"] = "";// DateTime.Today;
                    dr1["reqnote"] = "";
                    tbl1.Rows.Add(dr1);
                }

                Session["tblReq"] = (tbl1);
                this.gvResInfo_DataBind();
            }

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_15_Pro.RptReqEntry();
            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject rpttxtexdeldate = rptstk.ReportDefinition.ReportObjects["eddate"] as TextObject;
            //rpttxtexdeldate.Text = this.txtExpDeliveryDate.Text.Trim();
            //TextObject rpttxtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
            //rpttxtadate.Text = this.txtApprovalDate.Text.Trim();
            //TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //rpttxtnaration.Text = this.txtReqNarr.Text.Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = "Project Name: " + this.ddlProject.SelectedItem.Text.Trim().Substring(14);
            //TextObject txtfloornoText = rptstk.ReportDefinition.ReportObjects["floornotext"] as TextObject;
            //TextObject txtfloorno = rptstk.ReportDefinition.ReportObjects["floorno"] as TextObject;
            //if (ddlFloor.SelectedValue.ToString().Trim() != "000")
            //{

            //    txtfloornoText.Text = "Floor No:";
            //    txtfloorno.Text = this.ddlFloor.SelectedValue.ToString().Trim();
            //}
            //else
            //{
            //    txtfloornoText.Text = "";
            //    txtfloorno.Text = " ";
            //}

            //TextObject txtmrfno = rptstk.ReportDefinition.ReportObjects["mrfno"] as TextObject;
            //txtmrfno.Text = this.txtMRFNo.Text.ToString().Trim();
            //TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
            //txtcrdate.Text = this.txtCurReqDate.Text.ToString().Trim();
            //TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
            //txtcrno.Text = this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{

            //    string eventtype = "Material Requisition";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = "Project Name " + this.ddlProject.SelectedItem.ToString() + "Req No- " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //DataTable dt1 = new DataTable();
            //dt1 = (DataTable)Session["tblReq"];

            //rptstk.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.lbtnResFooterTotal_Click(null, null);
            string mMRFNO = this.txtMRFNo.Text.Trim();
            if (this.ddlPrevReqList.Items.Count == 0)
                this.GetReqNo();
            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            //Log Entry

            DataTable dtuser = (DataTable)Session["tblUserReq"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPostedDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            string tblApprovByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
            string tblApprovDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["aprvdat"]).ToString("dd-MMM-yyyy");
            string tblApprovtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
            string tblApprovSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string PostedByid = (this.Request.QueryString["InputType"] == "Entry") ? userid : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? userid
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (this.Request.QueryString["InputType"] == "Entry") ? Terminal : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Terminal
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (this.Request.QueryString["InputType"] == "Entry") ? Sessionid : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Sessionid
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string PostedDat = (this.Request.QueryString["InputType"] == "Entry") ? Date : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? Date
                : (this.Request.QueryString["InputType"] == "ReqEdit") ? Date : (tblPostedSession == "") ? Date : tblPostedDat;
            string ApprovByid = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" :
                    (tblApprovByid == "") ? userid : tblApprovByid;
            string approvdat = (this.Request.QueryString["InputType"] == "Entry") ? "01-Jan-1900" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "01-Jan-1900"
                : (tblApprovDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblApprovDat;
            string Approvtrmid = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? ""
                    : (tblApprovtrmid == "") ? Terminal : tblApprovtrmid;
            string ApprovSession = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? ""
                : (tblApprovSession == "") ? Sessionid : tblApprovSession;


            string Approved = (this.Request.QueryString["InputType"] == "Entry") ? "" : (this.Request.QueryString["InputType"] == "FxtAstEntry") ? "" :
                    (tblApprovByid == "") ? "Ok" : "";
            //////


            string mPACTCODE = this.ddlProject.SelectedValue.ToString().Trim();
            string mFLRCOD = this.ddlFloor.SelectedValue.ToString().Trim();
            string mREQUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mEDDAT = this.GetStdDate(this.txtExpDeliveryDate.Text.Trim()); // DateTime.Today.ToString("dd-MMM-yyyy");
            string mREQBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();

            string mREQNAR = this.txtReqNarr.Text.Trim();
            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQB", mREQNO, mREQDAT, mPACTCODE, mFLRCOD, mREQUSRID, mAPPRUSRID, mAPPRDAT, mEDDAT,

                             mREQBYDES, mAPPBYDES, mMRFNO, mREQNAR, PostedByid, Posttrmid, PostSession, ApprovByid, approvdat, Approvtrmid, ApprovSession, PostedDat, Approved, "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }
            DataTable tbl1 = (DataTable)Session["tblReq"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();

                double mPREQTY = Convert.ToDouble(tbl1.Rows[i]["preqty"]);
                double mAREQTY = Convert.ToDouble(tbl1.Rows[i]["areqty"]);
                double mBgdBalQty = Convert.ToDouble(tbl1.Rows[i]["bbgdqty"]);
                string Fcrate = tbl1.Rows[i]["fcrate"].ToString();
                string Conrate = tbl1.Rows[i]["conrate"].ToString();
                string mREQRAT = tbl1.Rows[i]["reqrat"].ToString();
                string mPSTKQTY = tbl1.Rows[i]["pstkqty"].ToString();
                string mEXPUSEDT = tbl1.Rows[i]["expusedt"].ToString();
                string mREQNOTE = tbl1.Rows[i]["reqnote"].ToString();

                if (mPREQTY >= mAREQTY)
                {

                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURREQINFO", "PURREQA",
                                mREQNO, mRSIRCODE, mSPCFCOD, mPREQTY.ToString(), mAREQTY.ToString(), Fcrate, Conrate, mREQRAT, mPSTKQTY, mEXPUSEDT, mREQNOTE,
                                "", "0", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                        return;
                    }
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Aprove Qty Must be Less Or Equal  Req. Qty";
                    return;

                }

            }

            this.txtCurReqDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

            if (ConstantInfo.LogStatus == true)
            {

                string eventtype = "Material Requisition";
                string eventdesc = "Update Reqisition";
                string eventdesc2 = "Req No- " + mREQNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }





        protected void gvResInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblReq"];
            this.gvReqInfo.DataSource = tbl1;
            this.gvReqInfo.DataBind();
            if (Request.QueryString["InputType"].ToString() == "Approval") // "Entry"
            {
                for (int i = 0; i < this.gvReqInfo.Rows.Count; i++)
                {
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvReqQty")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvUseDat")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvStokQty")).ReadOnly = true;
                    ((TextBox)this.gvReqInfo.Rows[i].FindControl("txtgvReqNote")).ReadOnly = true;
                }
            }

            ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvReqInfo.PageSize);
            ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
            {
                ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).Visible = true;
                ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvReqInfo.PageIndex;
            }
            this.lbtnResFooterTotal_Click(null, null);
        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            this.gvReqInfo.PageIndex = ((DropDownList)this.gvReqInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvResInfo_DataBind();
        }
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)Session["tblReq"];
            ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTReqAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(preqamt)", "")) ?
                    0.00 : tbl1.Compute("Sum(preqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvReqInfo.FooterRow.FindControl("lblgvFooterTAprAmt")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(areqamt)", "")) ?
                0.00 : tbl1.Compute("Sum(areqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }
        private void Session_tblReq_Update()
        {
            DataTable tbl1 = (DataTable)Session["tblReq"];
            int TblRowIndex2;

            for (int j = 0; j < this.gvReqInfo.Rows.Count; j++)
            {

                TblRowIndex2 = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + j;
                double dgvBgdQty = Convert.ToDouble(tbl1.Rows[TblRowIndex2]["bbgdqty"]);
                double dgvReqQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text.Trim()));




                double dgvApprQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvappQty")).Text.Trim()));
                double fcrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvFCRate")).Text.Trim()));
                double conrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvConRate")).Text.Trim()));
                double rate = fcrate * conrate;

                //double dgvReqRat = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvResRat")).Text.Trim()));
                double dgvStokQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvStokQty")).Text.Trim()));
                string dgvUseDat = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvUseDat")).Text.Trim();
                string dgvReqNote = ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqNote")).Text.Trim();
                double dgvReqAmt = dgvReqQty * rate;
                double dgvApprAmt = dgvApprQty * rate;
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvReqQty")).Text = dgvReqQty.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvappQty")).Text = dgvApprQty.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvFCRate")).Text = fcrate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvConRate")).Text = conrate.ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvReqInfo.Rows[j].FindControl("lblgvResRat")).Text = rate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvReqInfo.Rows[j].FindControl("txtgvStokQty")).Text = dgvStokQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTResAmt")).Text = dgvReqAmt.ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvReqInfo.Rows[j].FindControl("lblgvTAprAmt")).Text = dgvApprAmt.ToString("#,##0.00;(#,##0.00); ");
                tbl1.Rows[TblRowIndex2]["preqty"] = dgvReqQty;
                tbl1.Rows[TblRowIndex2]["areqty"] = dgvApprQty;
                tbl1.Rows[TblRowIndex2]["fcrate"] = fcrate;
                tbl1.Rows[TblRowIndex2]["conrate"] = conrate;
                tbl1.Rows[TblRowIndex2]["reqrat"] = rate;
                tbl1.Rows[TblRowIndex2]["preqamt"] = dgvReqAmt;
                tbl1.Rows[TblRowIndex2]["areqamt"] = dgvApprAmt;
                tbl1.Rows[TblRowIndex2]["pstkqty"] = dgvStokQty;
                tbl1.Rows[TblRowIndex2]["expusedt"] = dgvUseDat;
                tbl1.Rows[TblRowIndex2]["reqnote"] = dgvReqNote;
            }
            Session["tblReq"] = tbl1;
        }

        protected void lbtnResSpcf_Click(object sender, EventArgs e)
        {
            string mResCode = this.ddlResList.SelectedValue.ToString();
            string spcfcod1 = this.ddlResSpcf.SelectedValue.ToString();
            this.ddlResSpcf.Items.Clear();
            DataTable tbl1 = (DataTable)Session["tblSpcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "mspcfcod = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();
            this.ddlResSpcf.DataTextField = "spcfdesc";
            this.ddlResSpcf.DataValueField = "spcfcod";
            this.ddlResSpcf.DataSource = dt;
            this.ddlResSpcf.DataBind();
            DataRow[] dr = dt.Select("spcfcod='" + spcfcod1 + "'");
            if (dr.Length > 0)
            {
                this.ddlResSpcf.SelectedValue = spcfcod1;
            }

        }

        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string mProject = this.ddlProject.SelectedValue.ToString();
            string mSrchTxt = this.txtResSearch.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "MATCODELIST", mSrchTxt, mProject, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Materials are not available for budget');", true);
                return;
            }

            Session["tblMat"] = ds1.Tables[0];
            Session["tblSpcf"] = ds1.Tables[1];

            this.ddlResList.DataTextField = "rsirdesc1";
            this.ddlResList.DataValueField = "rsircode";
            this.ddlResList.DataSource = ds1.Tables[0];
            this.ddlResList.DataBind();

            this.lbtnResSpcf_Click(null, null);
        }
        protected void ddlResList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnResSpcf_Click(null, null);
        }
        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {


            string Type = Request.QueryString["InputType"].ToString();
            if (Type == "Entry" || Type == "FxtAstEntry")
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
            DataTable dt = (DataTable)Session["tblReq"];
            string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string rescode = ((Label)this.gvReqInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQFORSPCRES",
                        mREQNO, rescode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvReqInfo.PageSize) * (this.gvReqInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("rsircode<>''");
                Session["tblReq"] = dv.ToTable();
                this.gvResInfo_DataBind();
            }

        }


        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            string genno = (this.Request.QueryString["genno"].ToString() == "") ? "" : this.Request.QueryString["genno"].ToString();

            string comcod = this.GetCompCode();
            string fxtast = (this.Request.QueryString["InputType"].ToString() == "FxtAstEntry") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? "FxtAst"
                : (this.Request.QueryString["InputType"].ToString() == "ReqEdit") ? "ReqEdit" : "";

            string prjcode = ((Request.QueryString["InputType"].ToString() == "Approval") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "FxtAstApproval") ? this.ddlProject.SelectedValue.ToString()
                : (Request.QueryString["InputType"].ToString() == "ReqEdit") ? this.ddlProject.SelectedValue.ToString() : "") + "%";
            string mrfno = this.txtSrchMrfNo.Text.Trim() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPREVREQLIST", CurDate1,
                          prjcode, fxtast, mrfno, "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevReqList.Items.Clear();
            this.ddlPrevReqList.DataTextField = "reqno1";
            this.ddlPrevReqList.DataValueField = "reqno";
            this.ddlPrevReqList.DataSource = ds1.Tables[0];
            this.ddlPrevReqList.DataBind();
            if (genno.Length > 0)
            {
                this.ddlPrevReqList.SelectedValue = genno;
                this.ddlPrevReqList.Enabled = false;
            }

        }
        protected void lbtnResFooterDelete_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblReq"];
            string mREQNO = ASTUtility.Left(this.lblCurReqNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurReqDate.Text.Trim(), 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETEREQNO", mREQNO, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Failed";
                return;

            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Successfully";

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
        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)Session["tblReq"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "'");

            DataTable tbl2 = (DataTable)Session["tblMat"];

            if (dr2.Length == 0)
            {
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                    dr1["spcfcod"] = this.ddlResSpcf.SelectedValue.ToString();
                    dr1["rsirdesc1"] = tbl2.Rows[i]["rsirdesc1"].ToString().Substring(14);
                    dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();

                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["bbgdqty"] = Convert.ToDouble(tbl2.Rows[i]["bbgdqty"]).ToString(); //tbl2.Rows[i]["bbgdqty"].ToString();
                    dr1["preqty"] = Convert.ToDouble(tbl2.Rows[i]["bbgdqty"]).ToString(); //tbl2.Rows[i]["bbgdqty"].ToString(); 
                    dr1["areqty"] = 0;
                    dr1["fcrate"] = tbl2.Rows[i]["fcrate"];
                    dr1["conrate"] = tbl2.Rows[i]["conrate"];
                    dr1["reqrat"] = Convert.ToDouble(tbl2.Rows[i]["fcrate"]) * Convert.ToDouble(tbl2.Rows[i]["conrate"]);
                    dr1["preqamt"] = 0;
                    dr1["areqamt"] = 0;
                    dr1["pstkqty"] = 0;
                    dr1["expusedt"] = "";// DateTime.Today;
                    dr1["reqnote"] = "";

                    tbl1.Rows.Add(dr1);

                }

                Session["tblReq"] = tbl1;
            }
            int RowNo = 1;
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                if (tbl1.Rows[i]["rsircode"].ToString() == mResCode)
                {
                    RowNo = i + 1;
                    break;
                }
            }
            double PageNo = Math.Ceiling(RowNo * 1.00 / this.gvReqInfo.PageSize);
            this.gvReqInfo.PageIndex = Convert.ToInt32(PageNo - 1);
            this.gvResInfo_DataBind();


        }

    }
}