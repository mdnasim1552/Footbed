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
using SPEENTITY.C_81_Hrm.C_81_Rec;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.IO;

namespace SPEWEB.F_81_Hrm.F_81_Rec
{
    public partial class JobAdvertisement : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        string ADid = string.Empty;
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                this.txtADVText.Enabled = false;
                //this.ImgbtnReqse.Enabled = false;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                this.CommonButton();


                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "Entry") ? "Manpower Requisition" : "Manpower Requisition App";


                // ((Label)this.Master.FindControl("lblTitle")).Text = "Manpower Requisition"; 03. Manpower Requisition App

                this.imgBtnDept_Click(null, null);

                this.txtCurAdvDate.Text = DateTime.Today.ToString("dd.MM.yyyy");

                GetWorkStation();
                GetAllOrganogramList();
                GetJobSource();

                GetGradeList();
                GetDesignationList();
                this.lbtnOk_Click(null, null);

                string type = this.Request.QueryString["Type"].ToString().Trim();
                if (type == "App")
                {
                    string comcod = this.GetCompCode();

                    ADid = this.Request.QueryString["genno"].ToString().Trim();


                    ImgbtnFindReq_Click(null, null);
                    this.lbtnOk_Click(null, null);


                }
            }

        }

        private void CommonButton()
        {

            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


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
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            string Type = this.Request.QueryString["Type"].ToString();
            if (Type == "App")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Approved";
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = true;
                //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
                //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;

            }
        }

        private void GetDesignationList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string gradcode = ASTUtility.Left(this.ddlGrade.SelectedValue.ToString(), 4) + "%";
            var lst = getlist.GetDisgnation(comcod, gradcode);


            this.ddlPOSTList.DataTextField = "designation";
            this.ddlPOSTList.DataValueField = "desigcod";
            this.ddlPOSTList.DataSource = lst;
            this.ddlPOSTList.DataBind();
        }
        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDesignationList();
        }
        private void GetGradeList()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();


            var lst = getlist.GetEmpGradelist(comcod);
            lst = lst.FindAll(x => x.hrgcod != "00000");


            this.ddlGrade.DataTextField = "hrgdesc";
            this.ddlGrade.DataValueField = "hrgcod";
            this.ddlGrade.DataSource = lst;
            this.ddlGrade.DataBind();
            this.ddlGrade_SelectedIndexChanged(null, null);


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(lnkbtnEdit_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdateResReq_Click);

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
        }

        protected void GetJobSource()
        {

            string comcod = this.GetCompCode();
            string txtCompany = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");


            this.ddlSource.DataTextField = "soudesc";
            this.ddlSource.DataValueField = "soucod";
            this.ddlSource.DataSource = ds1.Tables[1];
            this.ddlSource.DataBind();

            //ds1.Dispose();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.txtSrchPre.Visible = true;
                this.lblpreReq.Visible = true;
                this.ImgbtnFindReq.Visible = true;
                this.ddlPrevAdvList.Visible = true;
                this.ddlPrevAdvList.Items.Clear();

                //this.ddlSection.Enabled = false;
                //this.ddlDept.Enabled = false;
                //this.ddlWstation.Enabled = false;
                //this.ddlDivision.Enabled = false;

                this.ddlSource.Visible = true;
                this.lblJobSource.Visible = false;

                this.lblCurAdvNo1.Text = "ADV" + DateTime.Today.ToString("MM") + "-";
                this.txtCurAdvDate.Enabled = true;
                this.txtMRFNo.Text = "";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";

                this.ddlDept.Items.Clear();
                this.ddlPOSTList.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtExpDeliveryDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtAdvNarr.Text = "";
                this.gvAdvInfo.DataSource = null;
                this.gvAdvInfo.DataBind();
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.txtCurAdvDate.Enabled = true;
                this.lblSource01.Visible = false;
                this.lblSource.Visible = true;


                return;
            }



            this.GetPostList();

            this.txtSrchPre.Visible = false;
            this.lblpreReq.Visible = false;
            this.ImgbtnFindReq.Visible = false;
            this.ddlPrevAdvList.Visible = false;


            //this.ddlSection.Enabled = true;
            //this.ddlDept.Enabled = true;
            //this.ddlWstation.Enabled = true;
            //this.ddlDivision.Enabled = true;


            this.lblddlCompany.Text = this.ddlSection.SelectedItem.Text.Trim();

            this.ddlSource.Visible = false;
            this.lblJobSource.Visible = true;
            this.lblJobSource.Text = this.ddlSource.SelectedItem.Text.Trim();
            this.txtCurAdvDate.Enabled = false;
            this.lblSource01.Visible = true;
            this.lblSource.Visible = false;

            this.txtCurAdvNo2.ReadOnly = true;
            this.Panel1.Visible = true;
            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_Advertisement_Info();




        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void GetAdvNo()
        {
            string comcod = this.GetCompCode();
            string mADVNO = "NEWADV";
            if (this.ddlPrevAdvList.Items.Count > 0)
                mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();

            string mADVDAT = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            if (mADVNO == "NEWADV")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETREFNO", mADVDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurAdvNo1.Text = ds2.Tables[0].Rows[0]["advno1"].ToString().Substring(0, 5);
                    this.txtCurAdvNo2.Text = ds2.Tables[0].Rows[0]["advno1"].ToString().Substring(6, 3);

                    this.ddlPrevAdvList.DataTextField = "advno1";
                    this.ddlPrevAdvList.DataValueField = "advno";
                    this.ddlPrevAdvList.DataSource = ds2.Tables[0];
                    this.ddlPrevAdvList.DataBind();
                }
            }

        }


        protected void Get_Advertisement_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            string mADVNO = "NEWADV";
            if (this.ddlPrevAdvList.Items.Count > 0)
            {
                this.txtCurAdvDate.Enabled = false;
                mADVNO = this.ddlPrevAdvList.SelectedValue.ToString();
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETPREADVDATA", mADVNO, CurDate1,
                      "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            ViewState["tblAdvPost"] = this.HiddenSameData(ds1.Tables[0]);


            if (mADVNO == "NEWADV")
            {
                ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETREFNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurAdvNo1.Text = ds1.Tables[0].Rows[0]["advno1"].ToString().Substring(0, 5);
                    this.txtCurAdvNo2.Text = ds1.Tables[0].Rows[0]["advno1"].ToString().Substring(6, 3);
                }
                return;
            }
            this.txtMRFNo.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.lblCurAdvNo1.Text = ds1.Tables[1].Rows[0]["advno1"].ToString().Substring(0, 5);
            this.txtCurAdvNo2.Text = ds1.Tables[1].Rows[0]["advno1"].ToString().Substring(6, 3);
            this.txtCurAdvDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["advdat"]).ToString("dd.MM.yyyy");

            //string ss = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            //string adad = this.ddlDept.SelectedValue.ToString();
            //this.ddlDept.DataSource = ds1.Tables[1].Rows[0]["pactcode1"].ToString();
            //this.ddlDept.SelectedValue = ds1.Tables[1].Rows[0]["pactcode1"].ToString();


            ////this.ddlDept.DataTextField = "pactdesc1";
            ////this.ddlDept.DataValueField = "pactcode1";
            ////this.ddlDept.DataSource = ds1.Tables[1];
            ////this.ddlDept.DataBind();

            ////this.ddlProjectName.DataTextField = "pactdesc";
            ////this.ddlProjectName.DataValueField = "pactcode";
            ////this.ddlProjectName.DataSource = ds1.Tables[1];
            ////this.ddlProjectName.DataBind();

            //////this.ddlProjectName.SelectedValue = ds1.Tables[1].Rows[0]["pactcode"].ToString();
            this.ddlSource.SelectedValue = ds1.Tables[1].Rows[0]["soucode"].ToString();

            this.lblddlCompany.Text = this.ddlSection.SelectedItem.Text.Trim();
            this.lblJobSource.Text = this.ddlSource.SelectedItem.Text.Trim();
            //this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["advbydes"].ToString();
            //this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            //this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            //this.txtExpDeliveryDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["eddat"]).ToString("dd.MM.yyyy");
            this.txtAdvNarr.Text = ds1.Tables[1].Rows[0]["remarks"].ToString();
            this.Data_Bind();

        }




        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            DataTable tbl1 = (DataTable)ViewState["tblAdvPost"];
            string mPostCode = this.ddlPOSTList.SelectedValue.ToString();
            DataTable tbl2 = (DataTable)ViewState["tblJobPost"];
            DataTable tbl3 = (DataTable)ViewState["tblSpcf"];

            DataRow[] dr3 = tbl1.Select("postcode = '" + mPostCode + "'");
            if (dr3.Length == 0)
            {
                for (int i = 0; i < tbl3.Rows.Count; i++)
                {
                    DataRow dr1 = tbl1.NewRow();
                    //dr1["advno"] = this.ddlDept.SelectedValue.ToString();
                    dr1["deptcode"] = this.ddlSection.SelectedValue.ToString();
                    dr1["deptdesc"] = this.ddlSection.SelectedItem.Text.Trim();
                    dr1["postcode"] = this.ddlPOSTList.SelectedValue.ToString();
                    dr1["postdesc"] = this.ddlPOSTList.SelectedItem.Text.Trim();



                    dr1["gcod"] = tbl3.Rows[i]["gcod"];
                    dr1["gdesc"] = tbl3.Rows[i]["gdesc"];
                    dr1["requir"] = "";

                    tbl1.Rows.Add(dr1);
                }
            }


            ViewState["tblAdvPost"] = this.HiddenSameData(tbl1);
            this.Data_Bind();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string postcode = dt1.Rows[0]["postcode"].ToString();
            string deptcode = dt1.Rows[0]["deptcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                    dt1.Rows[j]["deptdesc"] = "";
                }
                if (dt1.Rows[j]["postcode"].ToString() == postcode)
                {
                    postcode = dt1.Rows[j]["postcode"].ToString();
                    dt1.Rows[j]["postdesc"] = "";

                }


                else
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                postcode = dt1.Rows[j]["postcode"].ToString();

            }



            return dt1;
        }



        protected void lnkPrint_Click(object sender, EventArgs e)
        {



        }

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string mADVNO = this.lblCurAdvNo1.Text.Trim().Substring(0, 3) + this.txtCurAdvDate.Text.Trim().Substring(6, 4) + this.lblCurAdvNo1.Text.Trim().Substring(3, 2) + this.txtCurAdvNo2.Text.Trim();

            ADid = this.Request.QueryString["genno"].ToString().Trim() == "" ? mADVNO : this.Request.QueryString["genno"].ToString().Trim();
            string comcod = this.GetCompCode();
            bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "APPROVEADD", ADid, userid, "", "", "", "", "", "", "");
            if (result == false)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Approved Fail";
            }


            ((Label)this.Master.FindControl("lblmsg")).Text = "Approved Suceess";





        }

        protected void lbtnUpdateResReq_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.Save_Value();
            string mMRFNO = this.txtMRFNo.Text.Trim();
            if (this.ddlPrevAdvList.Items.Count == 0)
                this.GetAdvNo();
            string mADVDAT = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            string mADVNO = this.lblCurAdvNo1.Text.Trim().Substring(0, 3) + this.txtCurAdvDate.Text.Trim().Substring(6, 4) + this.lblCurAdvNo1.Text.Trim().Substring(3, 2) + this.txtCurAdvNo2.Text.Trim();


            DataTable tbl1 = (DataTable)ViewState["tblAdvPost"];


            string mPACTCODE = this.ddlSection.SelectedValue.ToString().Trim();
            string mJOBSOURCE = this.ddlSource.SelectedValue.ToString().Trim();
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());
            string mEDDAT = this.GetStdDate(this.txtExpDeliveryDate.Text.Trim());
            string mREQBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();

            string mREQNAR = this.txtAdvNarr.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "INSERTUPDATEININFO", "INADVINFOB", mADVNO, mPACTCODE, mADVDAT, mMRFNO, mJOBSOURCE, mREQNAR, "", "", "",

                             "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mpostcode = tbl1.Rows[i]["postcode"].ToString();
                string mGCOD = tbl1.Rows[i]["gcod"].ToString();
                string mREQUIR = tbl1.Rows[i]["requir"].ToString();

                result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "INSERTUPDATEININFO", "INADVINFOA",
                            mADVNO, mpostcode, mGCOD, mREQUIR, "", "", "", "", "",
                            "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }


            }



            this.txtCurAdvDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Advertisement Entry";
                string eventdesc = "Update Advertisement";
                string eventdesc2 = "Adv No- " + mADVNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblAdvPost"];
            this.gvAdvInfo.DataSource = tbl1;
            this.gvAdvInfo.DataBind();

        }


        private void Save_Value()
        {
            DataTable tbl1 = (DataTable)ViewState["tblAdvPost"];
            int TblRowIndex2;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            for (int j = 0; j < this.gvAdvInfo.Rows.Count; j++)
            {
                string dgvMSRBrand = ((TextBox)this.gvAdvInfo.Rows[j].FindControl("txtgvRequ")).Text.Trim();

                TblRowIndex2 = (this.gvAdvInfo.PageSize) * (this.gvAdvInfo.PageIndex) + j;

                tbl1.Rows[TblRowIndex2]["requir"] = dgvMSRBrand;

            }
            ViewState["tblAdvPost"] = tbl1;
        }



        protected void GetPostList()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETNEWPOST", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblJobPost"] = ds1.Tables[0];
            ViewState["tblSpcf"] = ds1.Tables[1];

            this.ddlPOSTList.DataTextField = "designame";
            this.ddlPOSTList.DataValueField = "desigid";
            this.ddlPOSTList.DataSource = ds1.Tables[0];
            this.ddlPOSTList.DataBind();



        }


        protected void gvAdvInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblAdvPost"];
            string mADVNO = ASTUtility.Left(this.lblCurAdvNo1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurAdvDate.Text.Trim(), 4) + this.lblCurAdvNo1.Text.Trim().Substring(3, 2) + this.txtCurAdvNo2.Text.Trim();
            string postcode = ((Label)this.gvAdvInfo.Rows[e.RowIndex].FindControl("lblgvResCod")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "DELETEADVINF",
                        mADVNO, postcode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result)
            {

                int rowindex = (this.gvAdvInfo.PageSize) * (this.gvAdvInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("postcode<>''");
                ViewState["tblAdvPost"] = dv.ToTable();
                this.Data_Bind();
            }

        }

        protected void ImgbtnFindReq_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string mrfno = ADid.Length > 0 ? ADid : "%" + this.txtSrchPre.Text.Trim() + "%";
            string CurDate1 = this.GetStdDate(this.txtCurAdvDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ADVERTISEMENT", "GETPREREF", CurDate1,
                          mrfno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevAdvList.Items.Clear();
            this.ddlPrevAdvList.DataTextField = "advno1";
            this.ddlPrevAdvList.DataValueField = "advno";
            this.ddlPrevAdvList.DataSource = ds1.Tables[0];
            this.ddlPrevAdvList.DataBind();


        }


        //protected void ImgbtnFindComp_Click(object sender, EventArgs e)
        //{
        //    this.GetCompany();
        //}



        protected void imgBtnDept_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();

            string Company = "94";
            string mSrchTxt = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, mSrchTxt, userid, "", "", "", "", "", "");
            if (ds1 == null)
                return;


            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = ds1.Tables[0];
            this.ddlDept.DataBind();

        }


        //protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.GetProjectName();
        //}


        protected void gvAdvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Save_Value();
            this.gvAdvInfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvAdvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }




        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);

            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept_SelectedIndexChanged(null, null);

        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }

        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }



        //private void GetEmpType()
        //{
        //    string comcod = GetCompCode();
        //    var lst = getlist.GetEmpTypelist(comcod);
        //    lst = lst.FindAll(x => x.hrgcod != "00000");
        //    this.ddlTypeEmp.DataTextField = "hrgdesc";
        //    this.ddlTypeEmp.DataValueField = "hrgcod";
        //    this.ddlTypeEmp.DataSource = lst;
        //    this.ddlTypeEmp.DataBind();
        //}


        private void GetWorkStation()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        private void GetDivision()
        {
            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];
            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);

            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst1;
            this.ddlDivision.DataBind();

            this.ddlDivision_SelectedIndexChanged(null, null);

        }


    }
}