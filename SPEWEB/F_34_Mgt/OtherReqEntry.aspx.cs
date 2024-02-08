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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using AjaxControlToolkit;
using System.IO;

namespace SPEWEB.F_34_Mgt
{
    public partial class OtherReqEntry : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();
        UserService userSer = new UserService();
        AutoCompleted AutoData = new AutoCompleted();
        Common CommonClass = new Common();
        public static string Url = "";

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
                //    Response.Redirect("../AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                // this.GetGroup();

                this.txtCurReqDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtBillDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtOrdDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.TxtMrrDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetSupplier();
                this.GroupBind();
                this.ViewComponent();
                this.Bankcode();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Others Requisition";
                this.lblmsg1.Visible = false;


                if ((Request.QueryString["Type"].ToString() == "Entry"))
                {
                    string comcod = this.GetCompCode();

                    DataSet ds4 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "LASTNARRATION", "", "", "", "", "", "", "", "", "");
                    if (ds4.Tables[0].Rows.Count == 0)
                        this.txtReqNarr.Text = "";
                    else
                        this.txtReqNarr.Text = ds4.Tables[0].Rows[0]["vernar"].ToString();

                }


                if ((Request.QueryString["Type"].ToString() == "OreqPrint") || (Request.QueryString["Type"].ToString() == "OreqEdit") || (Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "FinalAppr"))
                {
                    this.lbtnPrevReqList_Click(null, null);
                    this.lbtnOk_Click(null, null);

                }




                //if ((Request.QueryString["Type"].ToString() == "OreqPrint") || (Request.QueryString["Type"].ToString() == "OreqEdit"))
                //{
                //    //this.lbtnUpdateResReq.Visible = false;
                //    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;


                //}
                this.GetRecAndPayto();

                this.CommonButton();
                //  
            }
        }
        private void Bankcode()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string UserId = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);

                string ttsrch = "%" + "%";
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "GETCONACCHEAD", ttsrch, UserId, "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;

                if (ds2.Tables[0].Rows.Count == 0)
                    return;

                // ds2.Tables[0].Rows.Add(comcod, "000000000000", "None", "None");

                ViewState["tblBank"] = ds2.Tables[0];
                this.rblpaytype_SelectedIndexChanged(null, null);
                //this.ddlBankName.DataTextField = "cactdesc";
                //this.ddlBankName.DataValueField = "cactcode";
                //this.ddlBankName.DataSource = ds2.Tables[0];
                //this.ddlBankName.DataBind();
                //this.ddlBankName.SelectedValue = "000000000000";


            }
            catch (Exception ex)
            {
                //this.lblmsg.Text = "Error:" + ex.Message;
            }
        }
        private void Load_Bank()
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblBank"];
            if (this.rblpaytype.SelectedItem.ToString() == "Payable")
            {
                dt.Rows.Add(comcod, "000000000000", "None", "None");

            }

            this.ddlBankName.DataTextField = "cactdesc";
            this.ddlBankName.DataValueField = "cactcode";
            this.ddlBankName.DataSource = dt;
            this.ddlBankName.DataBind();
            if (this.rblpaytype.SelectedItem.ToString() == "Payable")
            {
                this.ddlBankName.SelectedValue = "000000000000";
                this.ddlBankName.Enabled = false;
                this.ddlSupplier.Enabled = true;
            }
            else
            {
                this.ddlBankName.Enabled = true;
                this.ddlSupplier.Enabled = false;
            }
        }
        protected void rblpaytype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Load_Bank();
        }
        private void CommonButton()
        {
            if (this.Request.QueryString["Type"] == "OreqApproved")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Approval";
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).OnClientClick = "return confirm('Do You want to Approve?')";
            }

            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Send Mail";
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdateResReq_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);


            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        public void GetRecAndPayto()
        {
            ViewState.Remove("tblrecandPayto");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            AutoData.GetRecAndPayto(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "GETPAYRECCOD", "", "", "", "", "", "", "", "", "");

        }
        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    //if ((Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "OreqAcc"))

        //    //{
        //    //    ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = false;
        //    //}

        //    // Create an event handler for the master page's contentCallEvent event
        //    ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        //    //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        //}

        private string GetCompCode()
        {
            if (Request.QueryString.AllKeys.Contains("comcod"))
            {
                if (this.Request.QueryString["comcod"].Length == 0)
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    return (hst["comcod"].ToString());
                }
                else
                {
                    return (this.Request.QueryString["comcod"].ToString());
                }
            }
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                return (hst["comcod"].ToString());
            }
             
        }
        private void ViewComponent()
        {
            if ((Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "FinalAppr") || (Request.QueryString["Type"].ToString() == "OreqPrint"))
            {
                //this.lblMatGroup.Visible = false;
                //this.ddlMatGrp.Visible = false;
                this.lblCurNo.Visible = false;
                this.lblCurReqNo1.Visible = false;
                this.txtCurReqNo2.Visible = false;
                this.lblmrfno.Visible = false;
                this.txtMRFNo.Visible = false;
                this.gvOtherReq.Columns[1].Visible = false;
                this.pnlnew.Visible = true;
            }
            if ((Request.QueryString["Type"].ToString() == "OreqAcc"))
            {

                this.pnlnew.Visible = false;
            }
            if ((Request.QueryString["Type"].ToString() == "OreqEntry") || (Request.QueryString["Type"].ToString() == "OreqEdit"))
            {
                this.pnlnew.Visible = true;

                this.gvOtherReq.Columns[12].Visible = false;
                this.rblpaytype.SelectedIndex = 0;

            }

            //if (Request.QueryString["Type"].ToString() == "OreqEdit")
            //{

            //    this.lblMatGroup.Visible = true;
            //    this.ddlMatGrp.Visible = true;
            //    this.lblCurNo.Visible = true;
            //    this.lblCurReqNo1.Visible = true;
            //    this.txtCurReqNo2.Visible = true;
            //    this.lblmrfno.Visible = true;
            //    this.txtMRFNo.Visible = true;

            //}

        }
        protected void lnkAcccode_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        private void GetProjectName()
        {

            ViewState.Remove("tblproject");
            string type = this.Request.QueryString["Type"].ToString();
            string fac = "%%";// ((this.Request.QueryString["prjcode"]).Length == 0) ? "%%" : this.Request.QueryString["prjcode"].ToString() + "%";
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "PROJECTNAME", fac, type, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ViewState["tblproject"] = ds1.Tables[0];
            ds1.Dispose();
            this.ddlProjectName_SelectedIndexChanged(null, null);

        }

        private void GetSupplier()
        {
            string comcod = this.GetCompCode();
            string serch1 = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETSUPPLIERNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSupplier.DataTextField = "sirdesc";
            this.ddlSupplier.DataValueField = "sircode";
            this.ddlSupplier.DataSource = ds1.Tables[0];
            this.ddlSupplier.DataBind();

        }
        private void GetGroup()
        {
            ViewState.Remove("tblgroup");
            string comcod = this.GetCompCode();
            string Calltype = (comcod.Substring(0, 1) == "2") ? "LANDOTHERGRP" : "OTHERGRP";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", Calltype, "%", "", "", "", "", "", "", "", "");
            ViewState["tblgroup"] = ds1.Tables[0];
        }
        private void GroupBind()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string actcode = this.ddlProjectName.SelectedValue.ToString();
                string filter1 = "%%";
                //string oldRescode = (this.ddlresuorcecode.Items.Count == 0) ? "" : this.ddlresuorcecode.SelectedValue.ToString();
                string SearchInfo = "";

                string search1 = this.ddlProjectName.SelectedValue.ToString().Trim();
                DataTable dt = (DataTable)ViewState["tblproject"];
                DataRow[] dr1 = dt.Select("actcode='" + search1 + "'");


                string type = dr1[0]["acttype"].ToString().Trim();
                if (type.Length > 0)
                {
                    int len;
                    string[] ar = type.Split('/');
                    foreach (string ar1 in ar)
                    {
                        if (ar1.Contains("-"))
                        {
                            len = ar1.IndexOf("-");
                            SearchInfo = SearchInfo + "left(rescode," + len + ") between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(rescode," + len + ")" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = new List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHeadREQ(actcode, filter1, SearchInfo);


                var lst1 = lst.OrderBy(x => x.rescode);
                this.ddlMatGrp.DataSource = lst1;
                this.ddlMatGrp.DataTextField = "resdesc1";
                this.ddlMatGrp.DataValueField = "rescode";
                this.ddlMatGrp.DataBind();
            }


            catch (Exception ex)
            {
                this.lblmsg1.Text = ex.Message;
            }
            //DataTable dt = (DataTable)Session["tblgroup"];
            //this.ddlMatGrp.DataTextField = "sirdesc";
            //this.ddlMatGrp.DataValueField = "sircode";
            //this.ddlMatGrp.DataSource = dt;
            //this.ddlMatGrp.DataBind();
            // this.ddlMatGrp_SelectedIndexChanged(null, null);
        }
        protected void ddlMatGrp_SelectedIndexChanged(object sender, EventArgs e)
        {

            string pactcode = this.ddlProjectName.SelectedValue.ToString().Substring(0, 2);
            if (pactcode == "16")
                this.ProjectData();
            // this.Get_Requisition_Info();
            //this.GetMaterial();
            //add new

        }

        private void GetMaterial()
        {
            string comcod = this.GetCompCode();
            string project = this.ddlProjectName.SelectedValue.ToString();
            string group = this.ddlMatGrp.SelectedValue.Substring(0, 4);
            string txtfindMat = this.txtResSearch.Text.Trim() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETGRPMATERIAL", project, group, CurDate1, txtfindMat, "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["getMat"] = ds1.Tables[0];
            this.ddlResList.DataTextField = "sirdesc";
            this.ddlResList.DataValueField = "rsircode";
            this.ddlResList.DataSource = ds1.Tables[0];
            this.ddlResList.DataBind();


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                Session.Remove("tblAttDocs");
                this.pnlAttacDeocx.Visible = false;
                ListViewEmpAll.DataSource = null;
                ListViewEmpAll.DataBind();
                //this.txtSrchMrfNo.Visible = true;
                this.lbtnPrevReqList.Visible = true;
                this.ddlPrevReqList.Visible = true;
                this.ddlPrevReqList.Items.Clear();
                // this.ddlProjectName.Enabled = true;
                this.lblCurReqNo1.Text = "GBL" + DateTime.Today.ToString("MM") + "-";
                //this.lblCurReqNo1.Text = "REQ" + DateTime.Today.ToString("MM") + "-";
                this.txtCurReqDate.Enabled = true;
                this.txtMRFNo.Text = "";
                this.lblmsg1.Text = "";
                this.txtResSearch.Text = "";
                this.ddlResList.Items.Clear();
                this.txtReqNarr.Text = "";
                this.gvOtherReq.DataSource = null;
                this.gvOtherReq.DataBind();
                this.Panel1.Visible = false;
                this.lbtnOk.Text = "Ok";
                if ((Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "FinalAppr") || (Request.QueryString["Type"].ToString() == "OreqPrint") || (Request.QueryString["Type"].ToString() == "OreqAcc"))
                {
                    this.lblMatGroup.Visible = false;
                    this.ddlMatGrp.Visible = false;
                    this.lblCurNo.Visible = false;
                    this.lblCurReqNo1.Visible = false;
                    this.txtCurReqNo2.Visible = false;
                    this.lblmrfno.Visible = false;
                    this.txtMRFNo.Visible = false;
                }

                this.lbtnGroupSelect.Visible = false;
                return;
            }


            if ((Request.QueryString["Type"].ToString() == "OreqApproved") || (Request.QueryString["Type"].ToString() == "FinalAppr") || (Request.QueryString["Type"].ToString() == "OreqPrint") || (Request.QueryString["Type"].ToString() == "OreqAcc"))
            {

                //this.Panel1.Visible=(Request.QueryString["Type"].ToString() =="OreqAcc")?false:true;

                this.lblCurNo.Visible = false;
                this.lblCurReqNo1.Visible = false;
                this.txtCurReqNo2.Visible = false;
                this.lblmrfno.Visible = false;
                this.txtMRFNo.Visible = false;
            }

            this.lblmrfno.Visible = true;
            this.txtMRFNo.Visible = true;
            //this.ddlProjectName.Enabled = false;
            this.lblCurNo.Visible = true;
            this.lblCurReqNo1.Visible = true;
            this.txtCurReqNo2.Visible = true;
            this.txtSrchMrfNo.Visible = false;
            this.lbtnPrevReqList.Visible = false;
            this.ddlPrevReqList.Visible = false;
            this.txtCurReqNo2.ReadOnly = true;
            this.Panel1.Visible = true;
            this.lbtnOk.Text = "New";

            this.ddlMatGrp_SelectedIndexChanged(null, null);
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            this.lbtnGroupSelect.Visible = (pactcode.Substring(0, 2) == "16") ? false : true;
            this.Get_Requisition_Info();
            this.pnlAttacDeocx.Visible = true;
            btnShowimg_OnClick(null, null);
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
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETLASTREQINFO", mREQDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mREQNO = ds2.Tables[0].Rows[0]["maxreqno"].ToString();
                    this.lblCurReqNo1.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(0, 6);
                    this.txtCurReqNo2.Text = ds2.Tables[0].Rows[0]["maxreqno1"].ToString().Substring(6, 5);

                    this.ddlPrevReqList.DataTextField = "maxreqno1";
                    this.ddlPrevReqList.DataValueField = "maxreqno";
                    this.ddlPrevReqList.DataSource = ds2.Tables[0];
                    this.ddlPrevReqList.DataBind();
                }
            }

        }

        protected void lbtnPrevReqList_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
             
            string CurDate1 = (this.Request.QueryString["Type"] == "OreqEdit") ? this.Request.QueryString["date1"].ToString() : this.GetStdDate(this.txtCurReqDate.Text.Trim());
            // string prjcode = this.ddlProjectName.SelectedValue.ToString() + "%";
            string mrfno = ((this.Request.QueryString["genno"]).Length == 0) ? "%%" : this.Request.QueryString["genno"].ToString() + "%";
            string type = this.Request.QueryString["Type"].ToString();
            string Module = (Request.QueryString["Type"].ToString() == "OreqAcc") ? "Acc" : "";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPREVREQLIST", CurDate1,
                          "", Module, mrfno, type, "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevReqList.Items.Clear();
            this.ddlPrevReqList.DataTextField = "reqno1";
            this.ddlPrevReqList.DataValueField = "reqno";
            this.ddlPrevReqList.DataSource = ds1.Tables[0];
            this.ddlPrevReqList.DataBind();

        }
        protected void Get_Requisition_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());


            DataTable dtp = (DataTable)ViewState["tblproject"];
            string actcode = this.ddlProjectName.SelectedValue.ToString();


            string mReqNo = "NEWREQ";
            if (this.ddlPrevReqList.Items.Count > 0)
            {
                this.txtCurReqDate.Enabled = false;
                mReqNo = this.ddlPrevReqList.SelectedValue.ToString();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
                      "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblReq"] = this.HiddenSameData(ds1.Tables[0]);
            ViewState["tblUserReq"] = ds1.Tables[1];
            ViewState["tblUsersign"] = ds1.Tables[2];


            DataTable dt = ds1.Tables[1];
            //if (dt.Rows.Count == 0)
            //    return;
            if (dt.Rows.Count > 0)
            {
                this.txtPayto.Text = dt.Rows[0]["payto"].ToString();
                string paytype = dt.Rows[0]["paytype"].ToString();
                //this.ddlSupplier.SelectedValue = dt.Rows[0]["supdesc"].ToString();

                //this.ddlSupplier.SelectedItem.Text = dt.Rows[0]["supdesc"].ToString();


                this.rblpaytype.SelectedIndex = (paytype == "Cheque") ? 1 : 0;



                //this.rblpaytype.SelectedItem.Text = dt.Rows[0]["paytype"].ToString();
            }


            if (Request.QueryString["Type"].ToString() == "OreqApproved" || Request.QueryString["Type"].ToString() == "FinalAppr" || Request.QueryString["Type"].ToString() == "OreqPrint")
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.GetApprQty();


                }
            }




            if (mReqNo == "NEWREQ")
            {

                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETLASTREQINFO", CurDate1, "", "", "", "", "", "", "", "");
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

            this.txtOrdNo.Text = ds1.Tables[1].Rows[0]["orderno"].ToString();
            this.txtOrdDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["orderdate"]).ToString("dd-MMM-yyyy");

            this.txtBillno.Text = ds1.Tables[1].Rows[0]["billno"].ToString();
            this.txtBillDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdate"]).ToString("dd-MMM-yyyy");

            this.txtMrrno.Text = ds1.Tables[1].Rows[0]["mrrno"].ToString();
            this.TxtMrrDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mrrdate"]).ToString("dd-MMM-yyyy");
            this.txtAdvamt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00); ");

            this.ddlBankName.SelectedValue = ds1.Tables[1].Rows[0]["cactcode"].ToString();
            this.ddlSupplier.SelectedValue = ds1.Tables[1].Rows[0]["supcode"].ToString();


            this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();

            //this.txtReqNarr.Text = ds1.Tables[1].Rows[0]["reqnar"].ToString();
            //actelev = dtp.Select("actcode='" + ds1.Tables[1].Rows[0]["pactcode"].ToString() + "'")[0]["actelev"].ToString();
            //if (actelev == "")
            //{
            //    this.ddlMatGrp.Items.Clear();
            //    this.ddlMatGrp.Visible = false;
            //}
            //else
            //{
            //    this.ddlMatGrp.Visible = true;

            //}

            this.gvOtherReq_DataBind();
        }

        private void GetApprQty()
        {
            DataTable dt = (DataTable)ViewState["tblReq"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double qty = Convert.ToDouble(dt.Rows[i]["qty"]);
                double ppdamt = Convert.ToDouble(dt.Rows[i]["ppdamt"]);

                double proamt = Convert.ToDouble(dt.Rows[i]["proamt"]);
                double appamt = Convert.ToDouble(dt.Rows[i]["appamt"]);
                if (appamt == 0)
                    dt.Rows[i]["appamt"] = proamt;
                dt.Rows[i]["qty"] = qty;
                dt.Rows[i]["ppdamt"] = ppdamt;




            }
            ViewState["tblReq"] = dt;


        }

        private void GetBudgetAndBal()
        {
            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string pactcode = this.ddlProjectName.SelectedValue.ToString();

            string rescode = (this.ddlMatGrp.Items.Count == 0) ? "000000000000" : this.ddlMatGrp.SelectedValue.ToString();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETREQBGDBALHAOTHERS", pactcode, rescode, CurDate1, "", "", "", "", "", "");
            ViewState["tblprobudbal"] = ds2.Tables[0];

        }


        protected void lbtnGroupSelect_Click(object sender, EventArgs e)
        {

            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            DataTable dt = (DataTable)ViewState["tblproject"];

            string actcode = this.ddlProjectName.SelectedValue.ToString();
            string actelev = dt.Select("actcode='" + actcode + "'")[0]["actelev"].ToString();

            if (actelev == "")
            {

                this.GetBudgetAndBal();

                DataRow dr1 = tbl1.NewRow();
                dr1["pactcode"] = actcode;
                dr1["rsircode"] = "000000000000";
                dr1["pactdesc"] = this.ddlProjectName.SelectedItem.Text;
                dr1["sirdesc"] = "";
                dr1["bgdamt"] = ((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "'"))[0]["bgdamt"]).ToString();
                dr1["trnamt"] = 0.00;
                dr1["balamt"] = ((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "'"))[0]["balamt"]).ToString();
                dr1["proamt"] = 0;
                dr1["appamt"] = 0;
                dr1["qty"] = 0;
                dr1["rate"] = 0;
                dr1["ppdamt"] = ((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "'"))[0]["ppdamt"]).ToString();
                dr1["billno"] = "";
                tbl1.Rows.Add(dr1);

            }

            else
            {
                string rescode = this.ddlMatGrp.SelectedValue.ToString();
                DataRow[] dr2 = tbl1.Select("pactcode='" + actcode + "' and rsircode = '" + rescode + "'");
                if (dr2.Length == 0)
                {

                    //string billno = dr2[0]["billno"].ToString();

                    this.GetBudgetAndBal();
                    DataRow dr1 = tbl1.NewRow();
                    dr1["pactcode"] = actcode;
                    dr1["rsircode"] = rescode;
                    dr1["pactdesc"] = this.ddlProjectName.SelectedItem.Text;
                    dr1["sirdesc"] = this.ddlMatGrp.SelectedItem.Text.Trim().Substring(14);
                    dr1["bgdamt"] = ((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'"))[0]["bgdamt"]).ToString();
                    dr1["trnamt"] = 0.00;
                    dr1["balamt"] = ((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'"))[0]["balamt"]).ToString();
                    dr1["proamt"] = 0;

                    dr1["appamt"] = 0;
                    dr1["qty"] = 0;
                    dr1["rate"] = 0;
                    dr1["ppdamt"] = ((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'"))[0]["ppdamt"]).ToString();
                    dr1["billno"] = "";
                    tbl1.Rows.Add(dr1);
                }
                else
                {
                    DataTable dtbill = tbl1.Copy();
                    DataView dv = dtbill.DefaultView;
                    dv.RowFilter = ("rsircode = '" + rescode + "'");
                    dtbill = dv.ToTable();

                    //for (int i = 0; i < dtbill.Rows.Count; i++)
                    //{
                    //    string Billno = dtbill.Rows[i]["billno"].ToString();
                    //    if (Billno.Length == 0)
                    //        return; 
                    //}
                    DataRow dr1 = tbl1.NewRow();

                    dr1["pactcode"] = actcode;
                    dr1["rsircode"] = rescode;
                    dr1["pactdesc"] = this.ddlProjectName.SelectedItem.Text;
                    dr1["sirdesc"] = this.ddlMatGrp.SelectedItem.Text.Trim().Substring(14);
                    dr1["bgdamt"] = ((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'"))[0]["bgdamt"]).ToString();
                    dr1["trnamt"] = 0.00;
                    dr1["balamt"] = ((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'"))[0]["balamt"]).ToString();
                    dr1["proamt"] = 0;

                    dr1["appamt"] = 0;
                    dr1["qty"] = 0;
                    dr1["rate"] = 0;
                    dr1["ppdamt"] = ((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'")).Length == 0) ? "0.00" : Convert.ToDouble((((DataTable)ViewState["tblprobudbal"]).Select("pactcode='" + actcode + "' and  rsircode='" + rescode + "'"))[0]["ppdamt"]).ToString();
                    dr1["billno"] = "";

                    tbl1.Rows.Add(dr1);
                }
            }
            ViewState["tblReq"] = this.HiddenSameData(tbl1);
            this.gvOtherReq_DataBind();



            //this.Session_tblReq_Update();         
            // DataTable tbl1 = (DataTable)Session["tblReq"];
            //string rescode = this.ddlResList.SelectedValue.ToString().Substring(0,4);
            //DataRow[] dr2 = tbl1.Select("rsircode1 = '" + rescode + "'");
            //if (dr2.Length == 0)
            //{

            //    string comcod = this.GetCompCode();  
            //    string pactcode = this.ddlProjectName.SelectedValue.ToString();
            //    string GroupCode = this.ddlMatGrp.SelectedValue.ToString().Substring(0,4);
            //    string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            //    DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETREQBGDBAL", pactcode, GroupCode, CurDate1, "", "", "", "", "", "");
            //    DataTable dt=ds2.Tables[0];

            //    if(dt.Rows.Count==0)
            //        return;

            //    for (int i = 0; i < dt.Rows.Count; i++) 
            //    {
            //        DataRow dr1 = tbl1.NewRow();
            //        dr1["rsircode"] =dt.Rows[i]["rsircode"].ToString();
            //        dr1["rsircode1"] =ASTUtility.Left(dt.Rows[i]["rsircode"].ToString(),4);
            //        dr1["sirdesc"] = dt.Rows[i]["sirdesc"].ToString();
            //        dr1["bgdamt"] = Convert.ToDouble(dt.Rows[i]["bgdamt"]);
            //        dr1["trnamt"] = Convert.ToDouble(dt.Rows[i]["trnamt"]);
            //        dr1["balamt"] = Convert.ToDouble(dt.Rows[i]["balamt"]);
            //        dr1["proamt"] = 0;
            //        dr1["appamt"] = 0;
            //        dr1["qty"] = 0;
            //        dr1["ppdamt"] = Convert.ToDouble(dt.Rows[i]["ppdamt"]);

            //        tbl1.Rows.Add(dr1);
            //    }


            //}
            //Session["tblReq"] = tbl1;
            //this.gvOtherReq_DataBind();
        }


        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            string mResCode = this.ddlResList.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["rsircode"] = this.ddlResList.SelectedValue.ToString();
                dr1["sirdesc"] = this.ddlResList.SelectedItem.Text.Trim();
                DataTable tbl2 = (DataTable)ViewState["getMat"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");
                dr1["bgdamt"] = dr3[0]["bgdamt"];
                dr1["trnamt"] = dr3[0]["trnamt"];
                dr1["balamt"] = dr3[0]["balamt"];
                dr1["proamt"] = 0;
                dr1["appamt"] = 0;
                dr1["rate"] = 0;
                dr1["qty"] = 0;
                dr1["ppdamt"] = dr3[0]["ppdamt"];
                dr1["billno"] = "";
                tbl1.Rows.Add(dr1);
            }
            ViewState["tblReq"] = tbl1;
            this.gvOtherReq_DataBind();
        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string supliern = ddlSupplier.SelectedItem.Text.Trim().ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
                 "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string narration = ds1.Tables[1].Rows[0]["reqnar"].ToString();
            string aprvuser = ds1.Tables[1].Rows[0]["aprvusrname"].ToString();

            string PurOrderno = ds1.Tables[1].Rows[0]["orderno"].ToString();
            string Orderdate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["orderdate"]).ToString("dd-MMM-yyyy");
            string mrrno = ds1.Tables[1].Rows[0]["mrrno"].ToString();
            string billno = ds1.Tables[1].Rows[0]["billno"].ToString();
            string billdate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdate"]).ToString("dd-MMM-yyyy");
            double advanceamt = Convert.ToDouble(ds1.Tables[1].Rows[0]["advamt"]);
            DataTable dt1 = new DataTable();
            //dt1 = (DataTable)ViewState["tblReq"];

            DataView dv = ((DataTable)ViewState["tblReq"]).Copy().DefaultView;
            dv.RowFilter = ("pactcode not like '2398%'");
            dt1 = dv.ToTable();


            DataView dv1 = ((DataTable)ViewState["tblReq"]).Copy().DefaultView;
            dv1.RowFilter = ("pactcode like '23980001%'");
            DataTable dt3 = dv1.ToTable();

            DataView dv2 = ((DataTable)ViewState["tblReq"]).Copy().DefaultView;
            dv2.RowFilter = ("pactcode like '23980002%'");
            DataTable dt4 = dv2.ToTable();

            double AIT = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("Sum(proamt)", "")) ?
                0.00 : dt3.Compute("Sum(proamt)", "")));
            double VAT = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(proamt)", "")) ?
                0.00 : dt4.Compute("Sum(proamt)", "")));
            double pAmt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(proamt)", "")) ?
                0.00 : dt1.Compute("Sum(proamt)", "")));

            double nAmt = ((pAmt + AIT + VAT) - advanceamt);

            double tvat = ((AIT + VAT) * -1) + advanceamt;

            DataTable dt2 = new DataTable();
            dt2 = ((DataTable)ViewState["tblUserReq"]).Copy();
            DataTable dtsign = ds1.Tables[2];//(DataTable)Session["tblUsersign"];

            string usrName = dtsign.Rows[0]["reqnam"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //DataTable dt = (DataTable)ViewState["tbladdbgdt"];
            //if (dt == null)
            //    return;


            var lst = dt1.DataTableToList<SPEENTITY.C_34_Mgt.OthReqStatus>();


            //  string year = lstsum[0].bgdyear.ToString();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_34_Mgt.RptBillSticker", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("Rpttitle", "BILL STICKER"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            rpt1.SetParameters(new ReportParameter("Cdate", "Date: " + CurDate1));
            rpt1.SetParameters(new ReportParameter("VenName", supliern));
            rpt1.SetParameters(new ReportParameter("VenBillno", billno));
            rpt1.SetParameters(new ReportParameter("VDate", billdate));
            rpt1.SetParameters(new ReportParameter("BillAmount", pAmt.ToString("#,##0;(#,##0); ")));

            rpt1.SetParameters(new ReportParameter("PurOrderno", PurOrderno));
            rpt1.SetParameters(new ReportParameter("PurDate", Orderdate));
            rpt1.SetParameters(new ReportParameter("MRNo", mrrno));
            rpt1.SetParameters(new ReportParameter("RefNo", "Ref No : P&SCM: " + this.txtMRFNo.Text.ToString().Trim()));

            rpt1.SetParameters(new ReportParameter("PerparedBy", usrName));
            rpt1.SetParameters(new ReportParameter("CheckedBy", ""));
            rpt1.SetParameters(new ReportParameter("ApprovededBy", aprvuser));
            rpt1.SetParameters(new ReportParameter("narration", narration));
            rpt1.SetParameters(new ReportParameter("LessAIT", (AIT * -1).ToString("#,##0;(#,##0); ")));
            rpt1.SetParameters(new ReportParameter("AIT", (VAT * -1).ToString("#,##0;(#,##0); ")));
            rpt1.SetParameters(new ReportParameter("Advance", advanceamt.ToString("#,##0;(#,##0); ")));
            rpt1.SetParameters(new ReportParameter("NetPayable", nAmt.ToString("#,##0;(#,##0); ")));

            rpt1.SetParameters(new ReportParameter("Totalamt", tvat.ToString("#,##0;(#,##0); ")));




            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        //protected void lnkPrint_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
        //    string mReqNo = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

        //    DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETPURREQINFO", mReqNo, CurDate1,
        //         "", "", "", "", "", "", "");
        //    ReportDocument rptstk = new RMGiRPT.R_34_Mgt.rptOtherReqStatus();

        //    DataTable dt1 = new DataTable();
        //    dt1 = (DataTable)ViewState["tblReq"];
        //    DataTable dt2 = new DataTable();
        //    dt2 = ((DataTable)ViewState["tblUserReq"]).Copy();
        //    DataTable dtsign = ds1.Tables[2];//(DataTable)Session["tblUsersign"];



        //    TextObject txtcompanyame = rptstk.ReportDefinition.ReportObjects["txtcompanyame"] as TextObject;
        //    txtcompanyame.Text = comnam;
        //    TextObject rpttxtnaration = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
        //    rpttxtnaration.Text = this.txtReqNarr.Text.Trim();

        //    TextObject txtrefno = rptstk.ReportDefinition.ReportObjects["txtrefno"] as TextObject;
        //    txtrefno.Text = "Ref No : " + this.txtMRFNo.Text.ToString().Trim();
        //    TextObject txtcrdate = rptstk.ReportDefinition.ReportObjects["crdate"] as TextObject;
        //    txtcrdate.Text = "Date : " + this.txtCurReqDate.Text.ToString().Trim();
        //    TextObject txtcrno = rptstk.ReportDefinition.ReportObjects["crno"] as TextObject;
        //    txtcrno.Text = "Requisition No : " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
        //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

        //    TextObject txtPtype = rptstk.ReportDefinition.ReportObjects["txtPtype"] as TextObject;
        //    txtPtype.Text = "Pay Type: " + this.rblpaytype.SelectedItem.Text.ToString();//"Pay Type: " + dt2.Rows[0]["paytype"].ToString(); 

        //    TextObject tctPayto = rptstk.ReportDefinition.ReportObjects["tctPayto"] as TextObject;
        //    tctPayto.Text = "Pay To: " + this.txtPayto.Text.ToString();  //"Pay To: " + dt2.Rows[0]["payto"].ToString(); 





        //    TextObject rpttxtReq = rptstk.ReportDefinition.ReportObjects["txtReq"] as TextObject;
        //    rpttxtReq.Text = dtsign.Rows[0]["reqnam"].ToString() + "\n" + dtsign.Rows[0]["reqdat"].ToString();



        //    if (ConstantInfo.LogStatus == true)
        //    {

        //        string eventtype = "Other Req Entry";
        //        string eventdesc = "Print Report";
        //        string eventdesc2 = "Requisition No: " + this.lblCurReqNo1.Text + this.txtCurReqNo2.Text.ToString().Trim();
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }


        //    rptstk.SetDataSource(dt1);

        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rptstk.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptstk;

        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //}

        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {


            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //string usrid = hst["usrid"].ToString();
            //string sessionid = hst["session"].ToString();
            //string trmid = hst["compname"].ToString();
            this.Session_tblReq_Update();
            string comcod = this.GetCompCode();
            string mMRFNO = this.txtMRFNo.Text.Trim();
            if (this.ddlPrevReqList.Items.Count == 0)
                this.GetReqNo();
            string mREQDAT = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();


            string type = this.Request.QueryString["Type"].ToString();

            //log Report
            DataTable dtuser = (DataTable)ViewState["tblUserReq"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPostedDate = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
            string tblApprovByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvbyid"].ToString();
            string tblApprovDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["aprvdat"]).ToString("dd-MMM-yyyy");
            string tblApprovtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvtrmid"].ToString();
            string tblApprovSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["aprvseson"].ToString();
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (this.Request.QueryString["Type"] == "OreqEntry") ? userid : (tblPostedByid == "") ? userid : tblPostedByid;

            string Posttrmid = (this.Request.QueryString["Type"] == "OreqEntry") ? Terminal : (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;

            string PostSession = (this.Request.QueryString["Type"] == "OreqEntry") ? Sessionid : (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string posteddat = (this.Request.QueryString["Type"] == "OreqEntry") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : (tblPostedDate == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblPostedDate;
            string ApprovByid = (this.Request.QueryString["Type"] == "OreqEntry")  ? "" : ((this.Request.QueryString["Type"] == "OreqApproved") || (this.Request.QueryString["Type"] == "OreqEdit") ? userid : ((tblApprovByid == "") ? userid : tblApprovByid));
            string approvdat = (this.Request.QueryString["Type"] == "OreqEntry") ? "01-Jan-1900" : ((this.Request.QueryString["Type"] == "OreqApproved") || (this.Request.QueryString["Type"] == "OreqEdit") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : (tblApprovDat == "01-Jan-1900") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : tblApprovDat);
            string Approvtrmid = (this.Request.QueryString["Type"] == "OreqEntry") ? "" : ((this.Request.QueryString["Type"] == "OreqApproved") || (this.Request.QueryString["Type"] == "OreqEdit") ? Terminal : (tblApprovtrmid == "") ? Terminal : tblApprovtrmid);
            string ApprovSession = (this.Request.QueryString["Type"] == "OreqEntry")  ? "" : ((this.Request.QueryString["Type"] == "OreqApproved") || (this.Request.QueryString["Type"] == "OreqEdit") ? Sessionid : (tblApprovSession == "") ? Sessionid : tblApprovSession);


            /////log end

            string OrderNo = this.txtOrdNo.Text;
            string OrderDat = this.txtOrdDate.Text;

            string Billno = this.txtBillno.Text;
            string BillDate = Convert.ToDateTime(this.txtBillDate.Text).ToString("dd-MMM-yyyy");
            string mrrno = this.txtMrrno.Text;
            string mrrdate = Convert.ToDateTime(this.TxtMrrDate.Text).ToString("dd-MMM-yyyy");

            string advamt = Convert.ToDouble("0" + (this.txtAdvamt.Text)).ToString();

            string refno = this.txtRefno.Text;
            string nARRATION = txtReqNarr.Text;
            string paytype = this.rblpaytype.SelectedValue.ToString();
            string payto = this.txtPayto.Text.Trim().ToString();
            string cactcode = ddlBankName.SelectedValue.ToString();
            string supllcode = ddlSupplier.SelectedValue.ToString();
            string supcode = this.ddlSupplier.SelectedValue.ToString();//(type == "OreqApproved" || type == "FinalAppr" ? dtuser.Rows[0]["supcode"].ToString() : 
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            bool result = true;
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mPACTCODE = tbl1.Rows[i]["pactcode"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mbillno = tbl1.Rows[i]["billno"].ToString();
                double mProAMT = Convert.ToDouble(tbl1.Rows[i]["proamt"]);
                double mAPPAMT = Convert.ToDouble(tbl1.Rows[i]["appamt"]);
                double qty = Convert.ToDouble(tbl1.Rows[i]["qty"]);
                double rate = Convert.ToDouble(tbl1.Rows[i]["rate"]);
                double ppdamt = Convert.ToDouble(tbl1.Rows[i]["ppdamt"]);
                if (mProAMT != 0)
                    result = purData.UpdateTransInfo1(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTOTHERREQ",
                             mREQNO, mPACTCODE, mRSIRCODE, mREQDAT, mMRFNO, mProAMT.ToString(), mAPPAMT.ToString(), nARRATION,
                             PostedByid, PostSession, Posttrmid, ApprovByid, approvdat, Approvtrmid, ApprovSession, qty.ToString(), paytype, payto, ppdamt.ToString(), posteddat, supcode, mbillno, type, OrderNo, OrderDat, "", "", "", "", "OTHERREQ");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

                    return;
                }
            }

            result = purData.UpdateTransInfo1(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSERTOTHERREQ", mREQNO, Billno, BillDate, mrrno, mrrdate, advamt, cactcode, refno, "", "", "", "", "", "", "", "", "", "", "", "", "", "", type, "", "", "", "", "", "OTHERREQB");

            ///Doc Upload
            DataTable dtd = (DataTable)Session["tblAttDocs"];
            DataSet ds1d = new DataSet("ds1");
            ds1d.Merge(dtd);
            ds1d.Tables[0].TableName = "tbl1";

            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "REQOTHERATTACHEDDOCUMENT", ds1d, null, null, mREQNO);
            if (!resulta)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }
            if (this.Request.QueryString["Type"] == "OreqApproved")
            {
                if (this.rblpaytype.SelectedItem.ToString() == "Payable")
                {
                    DataSet dsruaauser = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "UPDATEGENBILLJV", mREQNO, mREQDAT, ApprovByid, Approvtrmid, ApprovSession, approvdat, supcode);

                }


                if (cactcode != "000000000000")
                {
                    DataSet dsbankVou = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "UPDATEGENBILLBANK", mREQNO, mREQDAT, ApprovByid, Approvtrmid, ApprovSession, approvdat, cactcode);
                }


            }

            //this.lblmsg1.Text = "Data Updated successfully";


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);



            if (this.Request.QueryString["Type"] == "OreqEntry")
            {
                string esubject = "Other Requistion Create Notifications";
                string url = "http://202.0.94.49/F_34_Mgt/OtherReqEntry.aspx?Type=OreqEntry&prjcode=&genno=&comcod=" + comcod;
                string bodyContent = "Dear Sir, </br>A New Other Requisition Create Successfully, Please click  <b> <a href='" + url +
                                "' target='_blank'>" + mREQNO + " </a> </b> on the link to see";

                if (CommonClass.ConfimMail("2102022", esubject, url, bodyContent) == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Forward Successfully');", true);
                }


            }




            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Other Req Entry";
                string eventdesc = "Update Req";
                string eventdesc2 = mREQNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void gvOtherReq_DataBind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            this.gvOtherReq.DataSource = tbl1;
            this.gvOtherReq.DataBind();
            this.FooterVallue();
        }


        protected void FooterVallue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            if (tbl1.Rows.Count == 0)
                return;


            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFBgdamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(bgdamt)", "")) ?
                0.00 : tbl1.Compute("Sum(bgdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFPaidAmtxx")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(ppdamt)", "")) ?
                0.00 : tbl1.Compute("Sum(ppdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");



            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFPaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(trnamt)", "")) ?
                0.00 : tbl1.Compute("Sum(trnamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFBalamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(balamt)", "")) ?
                0.00 : tbl1.Compute("Sum(balamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFProposedamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(appamt)", "")) ?
                0.00 : tbl1.Compute("Sum(proamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvOtherReq.FooterRow.FindControl("lgvFAppamt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(appamt)", "")) ?
             0.00 : tbl1.Compute("Sum(appamt)", ""))).ToString("#,##0.00;(#,##0.00); ");





        }


        private void Session_tblReq_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblReq"];
            // int TblRowIndex2;
            string type = this.Request.QueryString["Type"].ToString();
            this.lblmsg1.Text = "";
            for (int i = 0; i < this.gvOtherReq.Rows.Count; i++)
            {
                //TblRowIndex2 = (this.gvOtherReq.PageSize) * (this.gvOtherReq.PageIndex) + i;
                //Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.dgv1.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                double qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvQtamt")).Text.Trim()));
                //Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvQtamt")).Text.Trim());
                double rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvRate")).Text.Trim()));
                //Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvRate")).Text.Trim());

                double Proamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvProposedamt")).Text.Trim()));
                //Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvProposedamt")).Text.Trim());
                double appamt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvApamt")).Text.Trim()));

                string Billno = ((TextBox)this.gvOtherReq.Rows[i].FindControl("lblgvBillno")).Text.Trim();

                //Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[i].FindControl("txtgvApamt")).Text.Trim());
                //OreqApproved  =OreqEntry
                //    FinalAppr
                rate = rate > 0 ? rate : (qty > 0 ? (Proamt / qty) : 0.00);
                Proamt = (type == "OreqEntry") ? (rate > 0 ? qty * rate : Proamt) : Proamt;
                appamt = (type == "OreqEntry") ? 0.00 : (rate > 0 ? qty * rate : appamt);
                tbl1.Rows[i]["proamt"] = Proamt;
                tbl1.Rows[i]["appamt"] = appamt;
                tbl1.Rows[i]["qty"] = qty;
                tbl1.Rows[i]["rate"] = rate;
                tbl1.Rows[i]["billno"] = Billno;




            }
            ViewState["tblReq"] = tbl1;
        }


        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            this.GetMaterial();

        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblReq_Update();
            this.gvOtherReq_DataBind();

        }

        private void GeResVisibility()
        {
            DataTable dtp = (DataTable)ViewState["tblproject"];
            string actcode = this.ddlProjectName.SelectedValue.ToString();
            string actelev = dtp.Select("actcode='" + actcode + "'")[0]["actelev"].ToString().Trim();

            if (actelev == "2")
            {

                this.GroupBind();
                this.lblMatGroup.Visible = true;
                this.ddlMatGrp.Visible = true;


            }
            else
            {

                this.ddlMatGrp.Items.Clear();
                this.lblMatGroup.Visible = false;
                this.ddlMatGrp.Visible = false;



            }
        }

        private void ProjectData()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurReqDate.Text.Trim());
            string pactcode = this.ddlProjectName.SelectedValue.ToString();

            if (pactcode.Substring(0, 2) == "16")
            {
                string GroupCode = this.ddlMatGrp.SelectedValue.ToString().Substring(0, 4);
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETREQBGDBAL", pactcode, GroupCode, CurDate1, "", "", "", "", "", "");
                ViewState["tblReq"] = this.HiddenSameData(ds2.Tables[0]);
                if (ds2 == null)
                    return;
                ds2.Dispose();
                this.gvOtherReq_DataBind();


            }

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {


            this.GeResVisibility();
            this.ProjectData();





            //string Type = Request.QueryString["Type"].ToString();
            //if (Type == "OreqEntry" )
            //{
            //    this.ddlPrevReqList.Items.Clear();

            //}


            //else
            //{
            //    this.lbtnPrevReqList_Click(null, null);

            //}
        }

        protected void gvOtherReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblReq"];
            string comcod = this.GetCompCode();
            int rowindex = (this.gvOtherReq.PageSize) * (this.gvOtherReq.PageIndex) + e.RowIndex;
            string pactcode = dt.Rows[rowindex]["pactcode"].ToString();
            // string reqno = "REQ" + this.txtCurReqDate.Text.Substring(6, 4) + this.lblCurReqNo1.Text.Substring(3, 2) + this.txtCurReqNo2.Text.ToString();//((Label)this.gvOtherReq.Rows[e.RowIndex].FindControl("lblgvreqf")).Text.Trim();

            string mREQDAT = this.txtCurReqDate.Text.Trim();
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            string rsircode = ((Label)this.gvOtherReq.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "DELETEREQRES", mREQNO, pactcode, rsircode, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                //this.Get_Requisition_Info();
                //int rowindex = (this.gvOtherReq.PageSize) * (this.gvOtherReq.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }
            DataView dv = dt.DefaultView;
            ViewState.Remove("tblReq");
            ViewState["tblReq"] = dv.ToTable();
            this.gvOtherReq_DataBind();
        }


        protected void ImgbtnFindGroup_Click(object sender, EventArgs e)
        {
            this.GetGroup();
        }
        protected void gvOtherReq_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvOtherReq.EditIndex = -1;
            this.gvOtherReq_DataBind();

        }
        protected void gvOtherReq_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvOtherReq.EditIndex = e.NewEditIndex;
            this.gvOtherReq_DataBind();


            string comcod = this.GetCompCode();
            int rowindex = (gvOtherReq.PageSize) * (this.gvOtherReq.PageIndex) + e.NewEditIndex;

            string actcode = ((DataTable)ViewState["tblReq"]).Rows[rowindex]["pactcode"].ToString();
            string subcode = ((DataTable)ViewState["tblReq"]).Rows[rowindex]["rsircode"].ToString();
            //double txtgvQty = Convert.ToDouble("0"+((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvQty")).Text.Trim());


            //double txtgvRate =Convert.ToDouble("0"+ ((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvRate")).Text.Trim());
            //double txtgvCrAmt = Convert.ToDouble("0" + ((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvCrAmt")).Text.Trim());

            //double txtgvDrAmt = Convert.ToDouble("0" + ((TextBox)dgv1.Rows[e.NewEditIndex].FindControl("txtgvDrAmt")).Text.Trim());

            DropDownList ddlgrdacccode = (DropDownList)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("ddlgrdacccode");
            ViewState["gindex"] = e.NewEditIndex;




            DataTable dt2 = (DataTable)ViewState["tblproject"];


            ddlgrdacccode.DataTextField = "actdesc1";
            ddlgrdacccode.DataValueField = "actcode";
            ddlgrdacccode.DataSource = dt2;
            ddlgrdacccode.DataBind();
            ddlgrdacccode.SelectedValue = actcode;




            //ddlgrdresouce.SelectedValue = actcode; 
            DataTable dt01 = (DataTable)ViewState["tblproject"];
            string search1 = ddlgrdacccode.SelectedValue.ToString().Trim();
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            DropDownList ddlgrdresouce = (DropDownList)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode");

            if (dr1[0]["actelev"].ToString() == "2")
            {


                ((Label)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("lblgvreshead")).Visible = true;
                //((LinkButton)this.dgv1.Rows[e.NewEditIndex].FindControl("ibtngrdFindResource")).Visible = true;
                ((DropDownList)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Visible = true;

                string filter1 = "%%";
                string SearchInfo = "";


                string type = dr1[0]["acttype"].ToString().Trim();
                if (type.Length > 0)
                {
                    int len;
                    string[] ar = type.Split('/');
                    foreach (string ar1 in ar)
                    {
                        if (ar1.Contains("-"))
                        {
                            len = ar1.IndexOf("-");
                            SearchInfo = SearchInfo + "left(rescode," + len + ") between " + ar1.Trim().Replace("-", " and ") + " ";
                        }
                        else
                        {
                            len = ar1.Length;

                            SearchInfo = SearchInfo + "left(rescode," + len + ")" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = new List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHeadREQ(actcode, filter1, SearchInfo);


                var lst1 = lst.OrderBy(x => x.rescode);
                ddlgrdresouce.DataSource = lst1;
                ddlgrdresouce.DataTextField = "resdesc1";
                ddlgrdresouce.DataValueField = "rescode";
                ddlgrdresouce.DataBind();
                ddlgrdresouce.SelectedValue = subcode;
                ddlgrdresouce.Focus();





                //SP_ENTRY_ACCOUNTS_BUDGET", "OTHERGRP",





            }
            else
            {

                ((Label)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("lblgvreshead")).Visible = false;
                //((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserresource")).Visible = false;
                //((LinkButton)this.dgv1.Rows[e.NewEditIndex].FindControl("ibtngrdFindResource")).Visible = false;
                ((DropDownList)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Visible = false;


            }
            //---------------------------------------------//
            //((TextBox)this.dgv1.Rows[e.NewEditIndex].FindControl("txtgrdserceacc")).Text = "";
            ((DropDownList)this.gvOtherReq.Rows[e.NewEditIndex].FindControl("ddlrgrdesuorcecode")).Focus();
        }
        protected void gvOtherReq_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblReq"];
            int rowindex = (int)ViewState["gindex"];
            string pactcode = ((DataTable)ViewState["tblReq"]).Rows[rowindex]["pactcode"].ToString();
            string rsircode = ((DataTable)ViewState["tblReq"]).Rows[rowindex]["rsircode"].ToString();



            string type = this.Request.QueryString["Type"].ToString();
            DataRow[] dr2 = dt.Select("pactcode = '" + pactcode + "' and rsircode='" + rsircode + "'");
            string ResCode = "";
            if (dr2.Length > 0)
            {

                double qty = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[rowindex].FindControl("txtgvQtamt")).Text.Trim());
                double rate = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[rowindex].FindControl("txtgvRate")).Text.Trim());
                double Proamt = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[rowindex].FindControl("txtgvProposedamt")).Text.Trim());
                double appamt = Convert.ToDouble('0' + ((TextBox)this.gvOtherReq.Rows[rowindex].FindControl("txtgvApamt")).Text.Trim());

                rate = rate > 0 ? rate : (qty > 0 ? (Proamt / qty) : 0.00);
                Proamt = (type == "OreqEntry") ? (rate > 0 ? qty * rate : Proamt) : Proamt;
                appamt = (type == "OreqEntry") ? 0.00 : (rate > 0 ? qty * rate : appamt);


                dr2[0]["pactcode"] = ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedValue.ToString();
                ResCode = ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).SelectedValue.ToString();
                ResCode = (ResCode.Length < 12 ? "000000000000" : ResCode);
                dr2[0]["rsircode"] = ResCode;
                dr2[0]["pactdesc"] = ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedItem.Text;
                dr2[0]["sirdesc"] = ResCode == "000000000000" ? "" : ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).SelectedItem.Text;
                dr2[0]["proamt"] = Proamt;// qty* rate; proamt
                dr2[0]["appamt"] = appamt; //qty * rate;//appamt;
                dr2[0]["qty"] = qty;
                dr2[0]["rate"] = rate;


            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string Terminal = hst["compname"].ToString();
            string userdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string actcodeold = pactcode;
            string rescodeold = rsircode;
            string mREQDAT = this.txtCurReqDate.Text.Trim();
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();
            string mMRFNO = this.txtMRFNo.Text.Trim();
            string actcode1 = ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlgrdacccode")).SelectedValue.ToString();
            string subcode1 = ResCode;

            string mqty = dr2[0]["qty"].ToString();
            string mProAMT = dr2[0]["proamt"].ToString();
            string mAPPAMT = dr2[0]["appamt"].ToString();
            string nARRATION = txtReqNarr.Text;

            string paytype = this.rblpaytype.SelectedValue.ToString();
            string payto = this.txtPayto.Text.Trim().ToString();
            string supllcode = ddlSupplier.SelectedValue.ToString();

            bool result = purData.UpdateTransInfo3(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "EDITOTHERREQSITION",
                        mREQNO, mMRFNO, actcodeold, rescodeold, actcode1, subcode1, mqty, mProAMT, mAPPAMT, paytype, payto, supllcode, nARRATION, userid, Terminal, userdate, "", "", "", "", "", "", "");










            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);




            this.gvOtherReq.EditIndex = -1;


            ViewState["tblReq"] = HiddenSameData(dt);
            DataView dv = dt.DefaultView;
            dv.Sort = "pactcode,rsircode";
            dt = dv.ToTable();

            this.Get_Requisition_Info();
            // this.gvOtherReq_DataBind();
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            dv.Sort = "pactcode,rsircode";
            dt1 = dv.ToTable();
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {

                    dt1.Rows[j]["pactdesc"] = "";
                }



                pactcode = dt1.Rows[j]["pactcode"].ToString();
            }

            return dt1;
        }

        protected void ddlgrdacccode_SelectedIndexChanged(object sender, EventArgs e)
        {

            int rowindex = (int)ViewState["gindex"];
            DataTable dt01 = (DataTable)ViewState["tblproject"];
            string search1 = ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlgrdacccode")).Text;
            DropDownList ddlgrdresouce = (DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode");
            DataRow[] dr1 = dt01.Select("actcode='" + search1 + "'");
            if (dr1.Length == 0)
                return;

            if (dr1[0]["actelev"].ToString() == "2")
            {
                ((Label)this.gvOtherReq.Rows[rowindex].FindControl("lblgvreshead")).Visible = true;
                ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Visible = true;
                this.GetgrdResource();

            }

            else
            {
                ((Label)this.gvOtherReq.Rows[rowindex].FindControl("lblgvreshead")).Visible = false;
                ((DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode")).Visible = false;
                ddlgrdresouce.Items.Clear();


            }

        }

        private void GetgrdResource()
        {




            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();



                int rowindex = (int)ViewState["gindex"];
                DropDownList ddlactcode = (DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlgrdacccode");
                DropDownList ddlgrdresouce = (DropDownList)this.gvOtherReq.Rows[rowindex].FindControl("ddlrgrdesuorcecode");

                string actcode = ddlactcode.SelectedValue.ToString();
                string filter1 = "%";

                string oldRescode = (ddlgrdresouce.Items.Count == 0) ? "" : ddlgrdresouce.SelectedValue.ToString();


                string SearchInfo = "";


                DataTable dt = (DataTable)ViewState["tblproject"];
                DataRow[] dr1 = dt.Select("actcode='" + actcode + "'");
                string type = dr1[0]["acttype"].ToString().Trim();
                if (type.Length > 0)
                {

                    int len;
                    string[] ar = type.Split('/');
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

                            SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                        }
                        SearchInfo = SearchInfo + " or ";

                    }
                    if (SearchInfo.Length > 0)
                        SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
                }

                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst = new List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead>();
                lst = userSer.GetResHead(actcode, filter1, SearchInfo);
                ViewState["HeadRsc1"] = lst;
                ddlgrdresouce.DataTextField = "resdesc1";
                ddlgrdresouce.DataValueField = "rescode";
                ddlgrdresouce.DataSource = lst;
                ddlgrdresouce.DataBind();
                ddlgrdresouce.Focus();



                List<SPEENTITY.C_21_Acc.EClassAccVoucher.EClassResHead> lst1 = lst.FindAll((p => p.rescode == oldRescode));
                if (lst1.Count > 0)
                {
                    ddlgrdresouce.SelectedValue = oldRescode;


                }




            }


            catch (Exception ex)
            {


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }



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
                int next = r.Next(0, 99999999);
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
                AsyncFileUpload1.Dispose();

            }
            Session["tblAttDocs"] = dt;

        }

        protected void btnShowimg_OnClick(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETIATTACHEDDOCSOTHERS", mREQNO, "", "", "", "", "", "", "", "");
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
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblAttDocs"];

            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                //string reqno = ((Label)this.ListViewEmpAll.Items[j].FindControl("reqno")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();



                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    string filePath = Server.MapPath("~/Upload/ReqDoc");
                    System.IO.File.Delete(filePath + filesname);

                    dt.Rows[j].Delete();

                }

            }
            DataSet ds1d = new DataSet("ds1");
            ds1d.Merge(dt);
            ds1d.Tables[0].TableName = "tbl1";
            string mREQNO = this.lblCurReqNo1.Text.Trim().Substring(0, 3) + this.txtCurReqDate.Text.Trim().Substring(6, 4) + this.lblCurReqNo1.Text.Trim().Substring(3, 2) + this.txtCurReqNo2.Text.Trim();

            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "REQOTHERATTACHEDDOCUMENT", ds1d, null, null, mREQNO);
            if (!resulta)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

                return;
            }

            this.btnShowimg_OnClick(null, null);


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

       
    }
}