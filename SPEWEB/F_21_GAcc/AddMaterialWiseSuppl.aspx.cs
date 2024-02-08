using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using SPELIB;
using AjaxControlToolkit;
using SPERDLC;
using Microsoft.Reporting.WinForms;
using SPEENTITY.C_22_Sal;

namespace SPEWEB.F_21_GAcc
{
    public partial class AddMaterialWiseSuppl : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        static string prevPage = String.Empty;
        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //prevPage = Request.UrlReferrer.ToString();
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                ////DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblTitle")).Text = "Material Wise Supplier";

                this.CurrencyInf();
                this.SelectView();
                this.CommonButton();
                ImgbtnFindRes1_Click(null, null);
                ddlCurrency_SelectedIndexChanged(null, null);

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnSuplUpdate_Click);


            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private void lbtnSuplUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.Session_tblResSupl_Update();
            DataTable tbl1 = (DataTable)Session["ResSupl"];

            DataRow[] dr2 = tbl1.Select("active = 'true'");
            if (dr2.Count() > 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Sorry !!!!! Only one item should be Recommended');", true);
                return;
            }
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string mRMRKS = tbl1.Rows[i]["rmrks"].ToString();
                string mDelsys = "0";
                string mPaysys = "0";
                string mRate = tbl1.Rows[i]["rate"].ToString();
                string mPaylimit = "0";
                string mCurcode = tbl1.Rows[i]["curcode"].ToString();
                string mConrate = tbl1.Rows[i]["conrate"].ToString();
                string mActive = tbl1.Rows[i]["active"].ToString();


                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATESUPLRES",
                              mSSIRCODE, mRSIRCODE, mSPCFCOD, mRMRKS, mDelsys, mPaysys, mRate, mPaylimit, mCurcode, mConrate, mActive, "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ purData.ErrorObject["Msg"].ToString() + "');", true);

                    return;
                }
                else
                {
                   
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                }
            }

        }

        private void lnkTotal_Click(object sender, EventArgs e)
        {

        }

        private void CommonButton()
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);

           
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;

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
        }
        protected void btnCurr_Click(object sender, EventArgs e)
        {

        }
        private void SelectView()
        {

            this.ddlSurveyType.Items[0].Enabled = false;
            this.ddlSurveyType.Items[1].Enabled = true;
            this.MultiView1.ActiveViewIndex = 0;
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //ReportClass rptstk = null;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "1")
            //{
            //    DataTable dt = (DataTable)Session["tblMSR"];
            //    RMGiRPT.R_07_Inv.RptPurMktSurveyBR rptstk1 = new MFGRPT.R_07_Inv.RptPurMktSurveyBR();

            //    TextObject txtmsrdate = rptstk1.ReportDefinition.ReportObjects["txtmsrdate"] as TextObject;
            //    txtmsrdate.Text = this.txtCurMSRDate.Text.ToString().Trim();
            //    TextObject txtadate = rptstk1.ReportDefinition.ReportObjects["txtExpiredDate"] as TextObject;
            //    txtadate.Text = this.txtCurMSRDate.Text.ToString().Trim();

            //    //txtadate.Text =Convert.ToDateTime(this.txtCurMSRDate.Text).AddMonths(30).ToString("dd.MM.yyyy").Trim();

            //    rptstk1.SetDataSource((DataTable)Session["tblMSR"]);
            //    Session["Report1"] = rptstk1;
            //    rptstk = rptstk1;
            //}
            //else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "2")
            //{
            //    MFGRPT.R_07_Inv.RptMktSurveyMatWiseSupList rptstk2 = new MFGRPT.R_07_Inv.RptMktSurveyMatWiseSupList();
            //    rptstk2.SetDataSource((DataTable)Session["ResSupl"]);
            //    Session["Report1"] = rptstk2;
            //    rptstk = rptstk2;
            //}
            //else if (this.ddlSurveyType.SelectedValue.ToString().Trim() == "3")
            //{
            //    MFGRPT.R_07_Inv.RptMktSurveySupWiseMatList rptstk3 = new MFGRPT.R_07_Inv.RptMktSurveySupWiseMatList();
            //    rptstk3.SetDataSource((DataTable)Session["SuplRes"]);
            //    Session["Report1"] = rptstk3;
            //    rptstk = rptstk3;
            //}


            //TextObject txtCompanyName = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;
            ////TextObject txtCompanyAddress = rptstk.ReportDefinition.ReportObjects["companyaddress"] as TextObject;
            ////txtCompanyAddress.Text = ConstantInfo.ComAdd;
            //TextObject txtsurveynoname = rptstk.ReportDefinition.ReportObjects["surveynoname"] as TextObject;
            //txtsurveynoname.Text = this.lblCurMSRNo1.Text.Trim() + this.txtCurMSRNo2.Text.ToString().Trim();
            ////TextObject txtadate = rptstk.ReportDefinition.ReportObjects["adate"] as TextObject;
            ////txtadate.Text = this.txtApprovalDate.Text.ToString().Trim();
            //TextObject txtnarrationname = rptstk.ReportDefinition.ReportObjects["narrationname"] as TextObject;
            //txtnarrationname.Text = this.txtMSRNarr.Text.ToString().Trim();
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = this.Label1.Text;
            //    string eventdesc = "Print Report Survey";
            //    string eventdesc2 = this.lblCurMSRNo1.Text.Trim() + this.txtCurMSRNo2.Text.ToString().Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            int indeex = this.ddlSurveyType.SelectedIndex;
            this.MultiView1.ActiveViewIndex = this.ddlSurveyType.SelectedIndex;
            this.Session_tblResSupl_Update();
            this.gvSuplInfo_DataBind();

        }

        private void gvSuplInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["ResSupl"];
            this.gvSuplInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvSuplInfo.DataSource = tbl1;
            this.gvSuplInfo.DataBind();
            if (tbl1.Rows.Count == 0)
                return;

        }

        private void Session_tblResSupl_Update()
        {
            DataTable tbl1 = (DataTable)Session["ResSupl"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvSuplInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvSuplInfo.Rows[j].FindControl("txtgvSuplRemarks")).Text.Trim();
                double gvrate1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvSuplInfo.Rows[j].FindControl("txtgvRate1")).Text.Trim()));
                bool gvActive =Convert.ToBoolean(((DropDownList)this.gvSuplInfo.Rows[j].FindControl("ddlactive")).SelectedValue);
                string gvActive1 = Convert.ToBoolean(((DropDownList)this.gvSuplInfo.Rows[j].FindControl("ddlactive")).SelectedValue.ToString()).ToString();


                TblRowIndex2 = (this.gvSuplInfo.PageIndex) * this.gvSuplInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["rmrks"] = dgvRemarks;
                tbl1.Rows[TblRowIndex2]["rate"] = gvrate1;
                tbl1.Rows[TblRowIndex2]["active"] = gvActive;
            }
            Session["ResSupl"] = tbl1;
        }


        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string ssircode = dt1.Rows[0]["ssircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["ssircode"].ToString() == ssircode)
                    dt1.Rows[j]["ssirdesc1"] = "";
                ssircode = dt1.Rows[j]["ssircode"].ToString();
            }

            return dt1;

        }

        protected void Resource_List(string MatCode)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRRESLIST", "", MatCode, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            Session["tblMat"] = ds1.Tables[0];
            Session["tblSpcf"] = ds1.Tables[1];
        }

        protected void ImgbtnFindRes1_Click(object sender, EventArgs e)
        {
            string mSrchTxt =  "%";
            this.Resource_List(mSrchTxt);
            this.ddlResList1.DataTextField = "rsirdesc1";
            this.ddlResList1.DataValueField = "rsircode";
            this.ddlResList1.DataSource = (DataTable)Session["tblMat"];
            this.ddlResList1.SelectedValue = this.Request.QueryString["sircode"].ToString();
            this.ddlResList1.DataBind();
            this.GetSpecification01();
            this.ddlResList1.Enabled = false;
            lbtnSelectRes1_Click(null, null);

        }


        private void GetSpecification01()
        {
            string specode = this.Request.QueryString["spcfcod"].ToString();
            DataTable dt = ((DataTable)Session["tblSpcf"]).Copy();
            string Resource01 = this.ddlResList1.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rsircode='" + Resource01 + "' or spcfcod='000000000000'");
            //dv.Sort = ("wrkcode, rsircode");
            dt = dv.ToTable();
            if (dt.Rows.Count > 1)
            {
                dt.Rows[0].Delete();
            }

            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = dt;
            this.ddlSpecification.DataBind();
            this.ddlSpecification.SelectedValue = specode;
            this.ddlSpecification.Enabled = false;
            ImgbtnFindSupl1_Click(null, null);

        }

        protected void ImgbtnFindSpecification_Click(object sender, EventArgs e)
        {
            this.GetSpecification01();

        }
        protected void ddlResList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification01();
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fcode = "001";
            string tcode = this.ddlCurrency.SelectedValue.ToString();
            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;


            this.lblConRate.Text = Convert.ToDouble(method).ToString("#,##0.000000;-#,##0.000000; ");

        }



        protected void ImgbtnFindSupl1_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt =  "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            this.ddlSupl1.DataTextField = "ssirdesc1";
            this.ddlSupl1.DataValueField = "ssircode";
            this.ddlSupl1.DataSource = ds1.Tables[0];
            this.ddlSupl1.DataBind();
        }


        protected void lbtnSelectRes1_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mResCode = this.Request.QueryString["sircode"].ToString();
            string mResspcfcod = this.Request.QueryString["spcfcod"].ToString();
            //string mSpcfCod = "000000000000";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETSUPLRRESLISTMATWISE", mResCode, mResspcfcod, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                return;
            }
            Session["ResSupl"] = this.HiddenSameData(ds1.Tables[0]);

            this.gvSuplInfo_DataBind();
        }
        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            this.Session_tblResSupl_Update();
            string mSuplCode = this.ddlSupl1.SelectedValue.ToString();

            string mResCode = this.Request.QueryString["sircode"].ToString();
            string mSpcfCod = this.Request.QueryString["spcfcod"].ToString();

            string fcode = "001";
            string tcode = this.ddlCurrency.SelectedValue.ToString();
            double conrate = 1.00;

            conrate = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;
            DataTable tbl1 = (DataTable)Session["ResSupl"];


            //a.rsircode, a.ssircode, ssirdesc1 = a.ssircode + ' - ' + b.sirdesc, a.rmrks, a.spcfcod, spcfdesc = '',rsirunit = '',propqty = 0,rate = 0,conrate = 0,curdesc = '',rmrks = ''


            DataRow[] dr2 = tbl1.Select("ssircode = '" + mSuplCode + "' and  rsircode='" + mResCode + "' and  spcfcod='" + mSpcfCod + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["rsircode"] = this.ddlResList1.SelectedValue.ToString();
                dr1["ssircode"] = this.ddlSupl1.SelectedValue.ToString();
                dr1["ssirdesc1"] = this.ddlSupl1.SelectedItem.Text.Trim();
                dr1["spcfcod"] = this.ddlSpecification.SelectedValue.ToString();
                dr1["spcfdesc"] = this.ddlSpecification.SelectedItem.Text.Trim();
                dr1["rmrks"] = "";
                dr1["active"] = "false";
                double rate = Convert.ToDouble((((DataTable)Session["tblSpcf"]).Select("rsircode='" + mResCode + "' and  spcfcod='" + mSpcfCod + "'"))[0]["stdrat"]);
                double rate1 = Convert.ToDouble((((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["stdrat"]);
                double ratefinal = (rate == 0) ? rate1 : rate;
                dr1["rate"] = ratefinal;

                //dr1["rate"] = ((Convert.ToDouble((((DataTable)Session["tblSpcf"]).Select("rsircode='" + mResCode + "' and  spcfcod='" + mSpcfCod + "'"))[0]["stdrat"]))==0)?
                //    Convert.ToDouble((((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode  + "'"))[0]["stdrat"]):
                //    Convert.ToDouble((((DataTable)Session["tblSpcf"]).Select("rsircode='" + mResCode + "' and  spcfcod='" + mSpcfCod + "'"))[0]["stdrat"]);


                dr1["curcode"] = this.ddlCurrency.SelectedValue.ToString();
                dr1["curdesc"] = this.ddlCurrency.SelectedItem.Text;
                dr1["conrate"] = conrate;
                dr1["rsirunit"] = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["rsirunit"].ToString();
                dr1["propqty"] = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["propqty"];

                tbl1.Rows.Add(dr1);
            }


            DataView dv = tbl1.DefaultView;
            dv.Sort = ("ssircode, spcfcod");
            tbl1 = this.HiddenSameData(dv.ToTable());
            Session["SuplRes"] = tbl1;
            this.gvSuplInfo_DataBind();
        }

        private void Session_tblSuplRes_Update()
        {
            //DataTable tbl1 = (DataTable)Session["SuplRes"];
            //int TblRowIndex2;
            //for (int j = 0; j < this.gvResInfo.Rows.Count; j++)
            //{
            //    string dgvRemarks = ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvResRemarks1")).Text.Trim();
            //    double gvrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvRate")).Text.Trim()));


            //    TblRowIndex2 = (this.gvResInfo.PageIndex) * this.gvResInfo.PageSize + j;
            //    tbl1.Rows[TblRowIndex2]["rmrks"] = dgvRemarks;
            //    tbl1.Rows[TblRowIndex2]["rate"] = gvrate;
            //}
            //Session["SuplRes"] = tbl1;
        }



        protected void lbtnResUpdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            //this.Session_tblSuplRes_Update();
            DataTable tbl1 = (DataTable)Session["SuplRes"];


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString(); ;
                string mRMRKS = tbl1.Rows[i]["rmrks"].ToString();
                string mDelsys = tbl1.Rows[i]["delsys"].ToString();
                string mPayss = tbl1.Rows[i]["paysys"].ToString();
                string mRate = tbl1.Rows[i]["rate"].ToString();
                string mPaylimit = tbl1.Rows[i]["paylimit"].ToString();
                string mCurcode = tbl1.Rows[i]["curcode"].ToString();
                string mConrate = tbl1.Rows[i]["conrate"].ToString();
                string mActive = tbl1.Rows[i]["active"].ToString();


                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATESUPLRES",
                              mSSIRCODE, mRSIRCODE, mSPCFCOD, mRMRKS, mDelsys, mPayss, mRate, mPaylimit, mCurcode, mConrate, mActive, "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

                }
            }
        }




        protected void gvSuplInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["ResSupl"];
            string Supcode = ((Label)this.gvSuplInfo.Rows[e.RowIndex].FindControl("lblgvSuplCod0")).Text.Trim();
            string Rescode = ((Label)this.gvSuplInfo.Rows[e.RowIndex].FindControl("lblgvResCod0")).Text.Trim();
            string Spcfcod = ((Label)this.gvSuplInfo.Rows[e.RowIndex].FindControl("lblgvspcfcod")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETESUPLRES",
                             Supcode, Rescode, Spcfcod, "", "", "", "", "", "", "", "", "", "", "", "");


            if (result == true)
            {
                int rowindex = (this.gvSuplInfo.PageSize) * (this.gvSuplInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("ResSupl");
            Session["ResSupl"] = dv.ToTable();
            this.gvSuplInfo_DataBind();

        }

        protected void gvSuplInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_tblResSupl_Update();
            this.gvSuplInfo.PageIndex = e.NewPageIndex;

            this.gvSuplInfo_DataBind();
        }

        protected void gvSuplInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlactive = (DropDownList)e.Row.FindControl("ddlactive");
                bool Code =Convert.ToBoolean(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "active")));
                if (Code == true)
                {
                    ddlactive.SelectedIndex = 1;
                }

            }
        }

        protected void lbtnCopy_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mResCode = this.Request.QueryString["sircode"].ToString();
            string mSpcfCod = this.Request.QueryString["spcfcod"].ToString();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "COPYSUPRES",
                              mResCode, mSpcfCod);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Copy Successfully');", true);
              

            }
        }
    }
}