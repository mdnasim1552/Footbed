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

namespace SPEWEB.F_09_Commer
{
    public partial class PurMktSurvey : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        static string prevPage = String.Empty;
        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "MktSurvey") ? "Comparative Statement - Purchase " : "Survey Link Information Input/Edit Screen";

                this.txtCurMSRDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.GetRequisition();
                this.CurrencyInf();
                this.CurrencyInf2();
                this.SelectView();
                this.CommonButton();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnSave_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).CssClass = "btn btn-warning btn-sm";
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Archive";
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(btnArchive_Click);
        }

        private void CommonButton()
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);

            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
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

            this.ddlCurrency_SelectedIndexChanged(null, null);
        }


        private void CurrencyInf2()
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

            this.ddlCurList_SelectedIndexChanged(null, null);
        }


        public void GetRequisition()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "GETREQUISITIONFOR_SURVEY", "", "", "", "", "", "", ""); ;
            if (ds1 == null)
                return;
            this.ddlRequision.DataTextField = "reqno1";
            this.ddlRequision.DataValueField = "reqno";
            this.ddlRequision.DataSource = ds1.Tables[0];
            this.ddlRequision.DataBind();
        }

        protected void btnCurr_Click(object sender, EventArgs e)
        {

        }

        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "MktSurvey":
                    this.lblInformation.Visible = false;
                    this.ddlSurveyType.Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lbtnMSROk.Text = "New";
                    this.lbtnMSROk_Click(null, null);

                    break;
                case "SurveyLink":
                    this.ddlSurveyType.Items[0].Enabled = false;
                    this.ddlSurveyType.Items[1].Enabled = true;
                    this.ddlSurveyType.Items[2].Selected = true;
                    this.ddlSurveyType_SelectedIndexChanged(null, null);
                    break;
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compaddf = hst["comaddf"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            if(ViewState["selecteddate"] != null && ViewState["selecteddate"].ToString() != "")
            {
                date = ViewState["selecteddate"].ToString();
            }


            DataTable dsMatWiseSup1 = (DataTable)Session["ResSup2"];
            DataTable dsMatWiseSup2 = (DataTable)Session["ResSup3"];

            var lst1 = dsMatWiseSup1.DataTableToList<SPEENTITY.C_09_Commer.RptPurMktSurvey1>();
            var lst2 = dsMatWiseSup2.DataTableToList<SPEENTITY.C_09_Commer.RptPurMktSurvey2>();

            string rsirdesctxt = lst1.Select(x => x.rsirdesc).SingleOrDefault().ToString();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptPurMktSurvey", lst1, lst2, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("rsirdesctxt", rsirdesctxt));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("fctyAddress", compaddf));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Material Wise Suplier List"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            string bestsupplier = "";
            double rate = 0;
            for (int i = 0; i < dsMatWiseSup2.Rows.Count; i++)
            {

                rpt1.SetParameters(new ReportParameter("supName" + (i + 1).ToString(), dsMatWiseSup2.Rows[i]["ssirdesc"].ToString()));
                rpt1.SetParameters(new ReportParameter("currName" + (i + 1).ToString(), "(" + dsMatWiseSup2.Rows[i]["curdesc"].ToString() + ")"));

                //if (rate < Convert.ToDouble(dsMatWiseSup2.Rows[i]["rate"]))
                //{
                //    rate = Convert.ToDouble(dsMatWiseSup2.Rows[i]["rate"]);
                //    bestsupplier = dsMatWiseSup2.Rows[i]["ssirdesc"].ToString();
                //}
                //if(rate!=0 && rate> Convert.ToDouble(dsMatWiseSup2.Rows[i]["rate"]))
                //{
                //    rate = Convert.ToDouble(dsMatWiseSup2.Rows[i]["rate"]);
                //    bestsupplier = dsMatWiseSup2.Rows[i]["ssirdesc"].ToString();
                //}

                if (rate == 0 || rate > Convert.ToDouble(dsMatWiseSup2.Rows[i]["rate"]))
                {
                    rate = Convert.ToDouble(dsMatWiseSup2.Rows[i]["rate"]);
                    bestsupplier = dsMatWiseSup2.Rows[i]["ssirdesc"].ToString();
                }


            }

            rpt1.SetParameters(new ReportParameter("lowSupName", bestsupplier));



            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlSurveyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.Panel3.Visible = false;
            int indeex = this.ddlSurveyType.SelectedIndex;
            this.MultiView1.ActiveViewIndex = this.ddlSurveyType.SelectedIndex;
            switch (this.ddlSurveyType.SelectedIndex)
            {
                case 0:
                    this.lbtnMSROk.Text = "New";
                    this.lbtnMSROk_Click(null, null);
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "MktSurvey":
                    break;

                case "SurveyLink":

                    int indeex = this.ddlSurveyType.SelectedIndex;
                    this.MultiView1.ActiveViewIndex = this.ddlSurveyType.SelectedIndex;
                    switch (this.ddlSurveyType.SelectedIndex)
                    {
                        case 0:

                            break;
                        case 1:
                            this.Session_tblResSupl_Update();
                            this.gvSuplInfo_DataBind();
                            break;
                        case 2:
                            this.Session_tblSuplRes_Update();
                            this.gvResInfo_DataBind();
                            break;
                    }

                    break;
            }
        }
        protected void lbtnPrevMSRList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPREVMSRLIST", CurDate1,
                          "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
            this.ddlPrevMSRList.Items.Clear();
            this.ddlPrevMSRList.DataTextField = "msrno1";
            this.ddlPrevMSRList.DataValueField = "msrno";
            this.ddlPrevMSRList.DataSource = ds1.Tables[0];
            this.ddlPrevMSRList.DataBind();
        }
        protected void lbtnMSROk_Click(object sender, EventArgs e)
        {
            if (this.lbtnMSROk.Text == "New")
            {
                this.ImgbtnFindPreMR.Visible = true;
                this.ddlPrevMSRList.Visible = true;
                this.lblPreMrList.Visible = true;
                this.txtPreMSRSearch.Visible = true;
                this.ddlPrevMSRList.Items.Clear();

                this.lblCurMSRNo1.Text = "MSR" + DateTime.Today.ToString("MM") + "-";
                this.txtCurMSRDate.Enabled = true;

                this.txtMSRResSearch.Text = "";
                this.ddlMSRRes.Items.Clear();
                this.ddlMSRSupl.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtMSRNarr.Text = "";

                this.gvMSRInfo.DataSource = null;
                this.gvMSRInfo.DataBind();

                this.Panel1.Visible = false;
                this.lbtnMSROk.Text = "Ok";
                return;
            }
            this.ImgbtnFindPreMR.Visible = false;
            this.ddlPrevMSRList.Visible = false;
            this.lblPreMrList.Visible = false;
            this.txtPreMSRSearch.Visible = false;
            this.txtCurMSRDate.ReadOnly = true;
            this.txtCurMSRNo2.ReadOnly = true;
            this.Panel1.Visible = true;
            this.lbtnMSROk.Text = "New";
            this.Get_Survey_Info();

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void Get_Survey_Info()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            string mMSRNo = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
            {
                this.txtCurMSRDate.Enabled = false;
                mMSRNo = this.ddlPrevMSRList.SelectedValue.ToString();
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPURMSRINFO", mMSRNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            Session["tblMSR"] = this.HiddenSameData(ds1.Tables[0]);


            if (mMSRNo == "NEWMSR")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTMSRINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurMSRNo1.Text = ds1.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);
                    this.txtCurMSRNo2.Text = ds1.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(6, 5);
                }
                return;
            }
            this.lblCurMSRNo1.Text = ds1.Tables[1].Rows[0]["msrno1"].ToString().Substring(0, 6);
            this.txtCurMSRNo2.Text = ds1.Tables[1].Rows[0]["msrno1"].ToString().Substring(6, 5);
            this.txtCurMSRDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["msrdat"]).ToString("dd.MM.yyyy");
            //this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["msrbydes"].ToString();
            //this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            //this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtMSRNarr.Text = ds1.Tables[1].Rows[0]["msrnar"].ToString();

            this.gvMSRInfo_DataBind();
        }

        protected void gvMSRInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tblMSR"];
            tbl1 = this.HiddenSameData(tbl1);
            this.gvMSRInfo.DataSource = tbl1;
            this.gvMSRInfo.DataBind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string Type = this.Request.QueryString["Type"].ToString();
            string rescod;
            switch (Type)
            {
                case "MktSurvey":
                    rescod = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rsircode"].ToString() == rescod)
                            dt1.Rows[j]["rsirdesc1"] = "";
                        rescod = dt1.Rows[j]["rsircode"].ToString();
                    }

                    break;

                case "SurveyLink":
                    string index = this.ddlSurveyType.SelectedValue.ToString();
                    switch (index)
                    {
                        case "2":
                            string ssircode = dt1.Rows[0]["ssircode"].ToString();
                            for (int j = 1; j < dt1.Rows.Count; j++)
                            {
                                if (dt1.Rows[j]["ssircode"].ToString() == ssircode)
                                    dt1.Rows[j]["ssirdesc1"] = "";
                                ssircode = dt1.Rows[j]["ssircode"].ToString();
                            }
                            break;
                        case "3":
                            rescod = dt1.Rows[0]["rsircode"].ToString();
                            for (int j = 1; j < dt1.Rows.Count; j++)
                            {
                                if (dt1.Rows[j]["rsircode"].ToString() == rescod)
                                    dt1.Rows[j]["rsirdesc1"] = "";
                                rescod = dt1.Rows[j]["rsircode"].ToString();
                            }
                            break;
                    }

                    break;
            }
            return dt1;
        }




        protected void ddlMSRPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblMSR_Update();
            this.gvMSRInfo_DataBind();
        }

        protected void Session_tblMSR_Update()
        {
            DataTable tbl1 = (DataTable)Session["tblMSR"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvMSRInfo.Rows.Count; j++)
            {
                double dgvLpRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMSRInfo.Rows[j].FindControl("lblgvLRate")).Text.Trim()));
                double dgvMSRRate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRRate")).Text.Trim()));
                double dgvMSRqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRqty")).Text.Trim()));
                string dgvMSRRemarks = ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRRemarks")).Text.Trim();
                double conrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvMSRInfo.Rows[j].FindControl("lblgvconrate")).Text.Trim()));
                double dgvMSRDelivery = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRDel")).Text.Trim()));
                double dgvMSRPay = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRPayment")).Text.Trim()));

                ((Label)this.gvMSRInfo.Rows[j].FindControl("lblgvtotalamt")).Text = (dgvMSRRate * conrate).ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRRate")).Text = dgvMSRRate.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvMSRInfo.Rows[j].FindControl("txtgvMSRqty")).Text = dgvMSRqty.ToString("#,##0.00;(#,##0.00); ");

                TblRowIndex2 = (this.gvMSRInfo.PageIndex) * this.gvMSRInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["maxrate"] = dgvLpRate;
                tbl1.Rows[TblRowIndex2]["resrate"] = dgvMSRRate;
                tbl1.Rows[TblRowIndex2]["totalamt"] = (dgvMSRRate * conrate);
                tbl1.Rows[TblRowIndex2]["reqqty"] = dgvMSRqty;
                tbl1.Rows[TblRowIndex2]["msrrmrk"] = dgvMSRRemarks;
                tbl1.Rows[TblRowIndex2]["delivery"] = dgvMSRDelivery;
                tbl1.Rows[TblRowIndex2]["payment"] = dgvMSRPay;
            }
            Session["tblMSR"] = tbl1;
        }



        protected void lbtnMSRResList_Click(object sender, EventArgs e)
        {
            string mSrchTxt = this.txtMSRResSearch.Text.Trim() + "%";
            this.Resource_List(mSrchTxt);
            this.ddlMSRRes.DataTextField = "rsirdesc1";
            this.ddlMSRRes.DataValueField = "rsircode";
            this.ddlMSRRes.DataSource = (DataTable)Session["tblMat"];
            this.ddlMSRRes.DataBind();
            this.lbtnMSRSupl_Click(null, null);
        }


        protected void lbtnMSRSelect_Click(object sender, EventArgs e)
        {
            this.Session_tblMSR_Update();
            DataTable tbl1 = (DataTable)Session["tblMSR"];
            string mResCode = this.ddlMSRRes.SelectedValue.ToString();
            string mSpcfCod = this.ddlSpecificationms.SelectedValue.ToString();
            string mSuplCode = this.ddlMSRSupl.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and ssircode = '" + mSuplCode + "' and spcfcod = '" + mSpcfCod + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["rsircode"] = this.ddlMSRRes.SelectedValue.ToString();
                dr1["spcfcod"] = mSpcfCod;
                dr1["ssircode"] = this.ddlMSRSupl.SelectedValue.ToString();
                dr1["rsirdesc"] = this.ddlMSRRes.SelectedItem.Text.Trim().Substring(15);
                dr1["rsirdesc1"] = this.ddlMSRRes.SelectedItem.Text.Trim().Substring(15);
                dr1["spcfdesc"] = this.ddlSpecificationms.SelectedItem.Text;
                dr1["ssirdesc1"] = this.ddlMSRSupl.SelectedItem.Text.Trim().Substring(15);

                DataTable tbl2 = (DataTable)Session["tblMat"];
                DataTable Supplier = (DataTable)Session["Supplier"];
                DataRow[] dr3 = tbl2.Select("rsircode = '" + mResCode + "'");

                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["maxrate"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["maxrate"].ToString();
                dr1["resrate"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["rate"].ToString();
                dr1["msrrmrk"] = "";
                dr1["cperson"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["cperson"].ToString();
                dr1["phone"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["phone"].ToString();
                dr1["mobile"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["mobile"].ToString();
                dr1["reqqty"] = 0;
                dr1["brand"] = "";
                dr1["delivery"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["delsys"].ToString();
                dr1["payment"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["paysys"].ToString();
                dr1["paylimit"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["paylimit"].ToString();
                dr1["curcode"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["curcode"].ToString();
                dr1["conrate"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["conrate"].ToString();
                dr1["curdesc"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["curdesc"].ToString();
                dr1["propqty"] = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["propqty"].ToString();
                dr1["totalamt"] = Convert.ToDouble((((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["conrate"]) * Convert.ToDouble((((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["rate"]);
                dr1["trasport"] = (((DataTable)Session["Supplier"]).Select("ssircode='" + mSuplCode + "'"))[0]["trasport"].ToString();

                tbl1.Rows.Add(dr1);
            }

            Session["tblMSR"] = tbl1;
            this.gvMSRInfo_DataBind();
        }

        protected void lbtnMSRUpdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            this.Session_tblMSR_Update();
            string mMSRNO = "NEWMSR";
            if (this.ddlPrevMSRList.Items.Count > 0)
                mMSRNO = this.ddlPrevMSRList.SelectedValue.ToString();
            string mMSRDAT = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            if (mMSRNO == "NEWMSR")
            {
                DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETLASTMSRINFO", mMSRDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    mMSRNO = ds2.Tables[0].Rows[0]["maxmsrno"].ToString();
                    this.lblCurMSRNo1.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(0, 6);
                    this.txtCurMSRNo2.Text = ds2.Tables[0].Rows[0]["maxmsrno1"].ToString().Substring(6, 5);

                    this.ddlPrevMSRList.DataTextField = "maxmsrno1";
                    this.ddlPrevMSRList.DataValueField = "maxmsrno";
                    this.ddlPrevMSRList.DataSource = ds2.Tables[0];
                    this.ddlPrevMSRList.DataBind();
                }
                else
                    return;
            }
            string PostedByid = hst["usrid"].ToString();
            string Postedtrmid = hst["compname"].ToString();
            string PostedSession = hst["session"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            string mMSRNAR = this.txtMSRNarr.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURMSRINFO", "PURMSRB",
                             mMSRNO, mMSRDAT, PostedByid, Postedtrmid, PostedSession, PostedDat, mMSRNAR, "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }

            DataTable tbl1 = (DataTable)Session["tblMSR"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mRESRATE = tbl1.Rows[i]["resrate"].ToString();
                string mMSRRMRK = tbl1.Rows[i]["msrrmrk"].ToString();
                string mMSRRQty = tbl1.Rows[i]["reqqty"].ToString();
                string mMSRRBrand = tbl1.Rows[i]["brand"].ToString();
                string mMSRRDelivery = tbl1.Rows[i]["delivery"].ToString();
                string mMSRRPay = tbl1.Rows[i]["payment"].ToString();
                string mMaxrate = tbl1.Rows[i]["maxrate"].ToString();
                string mPaylimit = tbl1.Rows[i]["paylimit"].ToString();
                string mCurcode = tbl1.Rows[i]["curcode"].ToString();
                string mConrate = tbl1.Rows[i]["conrate"].ToString();

                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATEPURMSRINFO", "PURMSRA",
                         mMSRNO, mRSIRCODE, mSPCFCOD, mSSIRCODE, mRESRATE, mMSRRMRK, mMSRRQty, mMSRRBrand, mMSRRDelivery, mMSRRPay, mMaxrate, mPaylimit, mCurcode, mConrate);
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated successfully";
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = this.Label1.Text;
                string eventdesc = "Update Survey";
                string eventdesc2 = mMSRNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        protected void ddlMSRResSpcf_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbtnMSRSupl_Click(null, null);
        }


        protected void lbtnMSRSupl_Click(object sender, EventArgs e)
        {
            Session.Remove("Supplier");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = this.txtMSRSupSearch.Text.Trim() + "%";
            string mResCode = this.ddlMSRRes.SelectedValue.ToString();
            string mSpcfCod = "000000000000";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, mResCode, mSpcfCod, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
            Session["Supplier"] = ds1.Tables[0];
            this.ddlMSRSupl.DataTextField = "ssirdesc1";
            this.ddlMSRSupl.DataValueField = "ssircode";
            this.ddlMSRSupl.DataSource = ds1.Tables[0];
            this.ddlMSRSupl.DataBind();
        }


        protected void Resource_List(string pmSrchTxt)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string group = this.ddlGroup.SelectedValue.ToString() == "0000" ? "%" : this.ddlGroup.SelectedValue.ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRRESLIST", pmSrchTxt, group, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            Session["tblMat"] = ds1.Tables[0];
            Session["tblSpcf"] = ds1.Tables[1];

            this.ddlResList1.DataTextField = "rsirdesc1";
            this.ddlResList1.DataValueField = "rsircode";
            this.ddlResList1.DataSource = (DataTable)Session["tblMat"];
            this.ddlResList1.DataBind();
            this.GetSpecification01();
        }

        protected void ImgbtnFindRes1_Click(object sender, EventArgs e)
        {
            string mSrchTxt = this.txtResSearch1.Text.Trim() + "%";
            this.Resource_List(mSrchTxt);
            this.ddlResList1.DataTextField = "rsirdesc1";
            this.ddlResList1.DataValueField = "rsircode";
            this.ddlResList1.DataSource = (DataTable)Session["tblMat"];
            this.ddlResList1.DataBind();
            this.GetSpecification01();
        }


        private void GetSpecification01()
        {

            DataTable dt = ((DataTable)Session["tblSpcf"]).Copy();
            string Resource01 = this.ddlResList1.SelectedValue.ToString();
            DataView dv = dt.DefaultView;

            dv.RowFilter = ("rsircode='" + Resource01 + "' or spcfcod='000000000000'");
            dt = dv.ToTable();

            if (dt.Rows.Count > 1)
            {
                dt.Rows[0].Delete();
            }

            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = dt;
            this.ddlSpecification.DataBind();
        }



        private void GetSpecification02()
        {
            DataTable dt = ((DataTable)Session["tblSpcf"]).Copy();
            string Resource02 = this.ddlResList2.SelectedValue.ToString();
            DataView dv = dt.DefaultView;

            dv.RowFilter = ("rsircode='" + Resource02 + "' or spcfcod='000000000000'");
            dt = dv.ToTable();

            if (dt.Rows.Count > 1)
            {
                dt.Rows[0].Delete();
            }

            this.ddlSpecification02.DataTextField = "spcfdesc";
            this.ddlSpecification02.DataValueField = "spcfcod";
            this.ddlSpecification02.DataSource = dt;
            this.ddlSpecification02.DataBind();
        }

        protected void ImgbtnFindSpecification_Click(object sender, EventArgs e)
        {
            this.GetSpecification01();
        }

        protected void ddlResList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification01();
        }

        protected void ImgbtnFindSpecification2_Click(object sender, EventArgs e)
        {
            this.GetSpecification02();
        }

        protected void ddlResList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecification02();
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tcode = "001";
            string fcode = this.ddlCurrency.SelectedValue.ToString();

            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            var List = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode)).ToList();

            double method = (List.Count > 0) ? List[0].conrate : 0;

            this.lblConRate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
        }

        protected void ImgbtnFindRes2_Click(object sender, EventArgs e)
        {
            string mSrchTxt = "%";
            this.Resource_List(mSrchTxt);
            this.ddlResList2.DataTextField = "rsirdesc1";
            this.ddlResList2.DataValueField = "rsircode";
            this.ddlResList2.DataSource = (DataTable)Session["tblMat"];
            this.ddlResList2.DataBind();
            this.GetSpecification02();
        }

        protected void ImgbtnFindSupl1_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = this.txtSuplSearch1.Text.Trim() + "%";
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
            string mResCode = this.ddlResList1.SelectedValue.ToString();
            string spcfcod = this.ddlSpecification.SelectedValue.ToString();
            //string mSpcfCod = "000000000000";

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETRESSUPLRLIST", mResCode, spcfcod, "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            Session["ResSupl"] = this.HiddenSameData(ds1.Tables[0]);
            Session["ResSup2"] = this.HiddenSameData(ds1.Tables[1]);
            Session["ResSup3"] = this.HiddenSameData(ds1.Tables[2]);
            Session["ArchiveDate"] = this.HiddenSameData(ds1.Tables[3]);

            this.Panel2.Visible = true;
            this.ImgbtnFindSupl1.Visible = true;
            this.ddlSupl1.Visible = true;
            this.lbtnSelectSupl1.Visible = true;
            this.LinkButtonCrcy.Visible = true;
            this.HyperLinkCrcy.Visible = true;
            this.ddlCurList.Visible = true;
            this.LabelCRate.Visible = true;
            this.TextBoxCRate.Visible = true;
            this.gvSuplInfo_DataBind();
            ViewState["selecteddate"] = "";
        }

        protected void gvSuplInfo_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["ResSupl"];
                this.gvSuplInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvSuplInfo.DataSource = tbl1;
                this.gvSuplInfo.DataBind();
                this.grvSurveArchDate.DataSource = null;
                this.grvSurveArchDate.DataBind();
                if (tbl1.Rows.Count == 0)
                    return;

                DataTable tbl2 = (DataTable)Session["ArchiveDate"];
                if (tbl2.Rows.Count > 0)
                {
                    this.grvSurveArchDate.DataSource = tbl2;
                    this.grvSurveArchDate.DataBind();
                }
               
            }
            catch(Exception ex)
            {

            }
        }


        private void Session_tblResSupl_Update()
        {
            DataTable tbl1 = (DataTable)Session["ResSupl"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvSuplInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvSuplInfo.Rows[j].FindControl("txtgvSuplRemarks")).Text.Trim();
                double gvrate1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvSuplInfo.Rows[j].FindControl("txtgvRate1")).Text.Trim()));

                TblRowIndex2 = (this.gvSuplInfo.PageIndex) * this.gvSuplInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["rmrks"] = dgvRemarks;
                tbl1.Rows[TblRowIndex2]["rate"] = gvrate1;
            }
            Session["ResSupl"] = tbl1;
        }

        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Session_tblResSupl_Update();
                DataTable tbl1 = (DataTable)Session["ResSupl"];
                string mSuplCode = this.ddlSupl1.SelectedValue.ToString();
                string mResCode = this.ddlResList1.SelectedValue.ToString();
                string mSpcfCod = this.ddlSpecification.SelectedValue.ToString();

                string tcode = "001";
                string fcode = this.ddlCurrency.SelectedValue.ToString();
                double conrate = 1.00;

                conrate = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;

                DataRow[] dr2 = tbl1.Select("ssircode = '" + mSuplCode + "' and  spcfcod='" + mSpcfCod + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["rsircode"] = this.ddlResList1.SelectedValue.ToString();
                    dr1["ssircode"] = this.ddlSupl1.SelectedValue.ToString();
                    dr1["ssirdesc1"] = this.ddlSupl1.SelectedItem.Text.Trim();
                    dr1["spcfcod"] = this.ddlSpecification.SelectedValue.ToString();
                    dr1["spcfdesc"] = this.ddlSpecification.SelectedItem.Text.Trim();
                    dr1["rmrks"] = "";
                    dr1["delsys"] = 0.00;
                    dr1["paysys"] = 0.00;
                    dr1["paylimit"] = 0.00;
                    dr1["rate"] = Convert.ToDouble((((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["stdrat"]) / conrate;
                    dr1["curcode"] = this.ddlCurList.SelectedValue.ToString();
                    dr1["curdesc"] = this.ddlCurList.SelectedItem.Text;
                    dr1["conrate"] = conrate;
                    dr1["rsirunit"] = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["rsirunit"].ToString();
                    dr1["propqty"] = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["propqty"];

                    tbl1.Rows.InsertAt(dr1, 0);
                }

                DataView dv = tbl1.DefaultView;
                //dv.Sort = ("ssircode, spcfcod");
                tbl1 = this.HiddenSameData(dv.ToTable());
                Session["SuplRes"] = tbl1;
                this.gvSuplInfo_DataBind();
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Supplier');", true);
            }         
        }

        protected void lbtnSuplUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.Session_tblResSupl_Update();
            DataTable tbl1 = (DataTable)Session["ResSupl"];

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string mRMRKS = tbl1.Rows[i]["rmrks"].ToString();
                string mDelsys = tbl1.Rows[i]["delsys"].ToString();
                string mPaysys = tbl1.Rows[i]["paysys"].ToString();
                string mRate = tbl1.Rows[i]["rate"].ToString();
                string mPaylimit = tbl1.Rows[i]["paylimit"].ToString();
                string mCurcode = tbl1.Rows[i]["curcode"].ToString();
                string mConrate = tbl1.Rows[i]["conrate"].ToString();

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATESUPLRES",
                              mSSIRCODE, mRSIRCODE, mSPCFCOD, mRMRKS, mDelsys, mPaysys, mRate, mPaylimit, mCurcode, mConrate, "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update failed');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
                }
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = this.Label1.Text;
                string eventdesc = "Update Supplier Info";
                string eventdesc2 = "Update Supplier";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void ImgbtnFindSupl2_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            this.ddlSupl2.DataTextField = "ssirdesc1";
            this.ddlSupl2.DataValueField = "ssircode";
            this.ddlSupl2.DataSource = ds1.Tables[0];
            this.ddlSupl2.DataBind();
        }

        protected void lbtnSelectSupl2_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSuplCode = this.ddlSupl2.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETSUPLRRESLIST", mSuplCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["SuplRes"] = this.HiddenSameData(ds1.Tables[0]);
            this.Panel3.Visible = true;
            this.gvResInfo_DataBind();
        }

        protected void gvResInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["SuplRes"];
            this.gvResInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvResInfo.DataSource = tbl1;
            this.gvResInfo.DataBind();
            if (tbl1.Rows.Count == 0)
                return;
        }

        private void Session_tblSuplRes_Update()
        {
            DataTable tbl1 = (DataTable)Session["SuplRes"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvResInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvResRemarks1")).Text.Trim();
                double gvrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvResInfo.Rows[j].FindControl("txtgvRate")).Text.Trim()));


                TblRowIndex2 = (this.gvResInfo.PageIndex) * this.gvResInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["rmrks"] = dgvRemarks;
                tbl1.Rows[TblRowIndex2]["rate"] = gvrate;
            }

            Session["SuplRes"] = tbl1;
        }

        protected void lbtnSelectRes2_Click(object sender, EventArgs e)
        {

            this.Session_tblSuplRes_Update();
            string fcode = "001";
            string tcode = this.ddlCurrency.SelectedValue.ToString();
            double conrate = 1.00;
            conrate = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;

            DataTable tbl1 = (DataTable)Session["SuplRes"];


            string mResCode = this.ddlResList2.SelectedValue.ToString();
            string mSpcfCod = this.ddlSpecification02.SelectedValue.ToString();
            string rsirunit = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["rsirunit"].ToString();

            DataRow[] dr2 = tbl1.Select("rsircode = '" + mResCode + "' and  spcfcod='" + mSpcfCod + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["ssircode"] = this.ddlSupl2.SelectedValue.ToString();
                dr1["rsircode"] = this.ddlResList2.SelectedValue.ToString();
                dr1["rsirdesc1"] = this.ddlResList2.SelectedItem.Text.Trim();
                dr1["spcfcod"] = this.ddlSpecification02.SelectedValue.ToString();
                dr1["spcfdesc"] = this.ddlSpecification02.SelectedItem.Text.Trim();
                dr1["rmrks"] = "";
                dr1["delsys"] = 0.00;
                dr1["paysys"] = 0.00;
                dr1["rate"] = Convert.ToDouble((((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["stdrat"]) / conrate;
                dr1["paylimit"] = 0.00;
                dr1["curcode"] = this.ddlCurrency.SelectedValue.ToString();
                dr1["curdesc"] = this.ddlCurrency.SelectedItem.Text;
                dr1["conrate"] = conrate;
                dr1["rsirunit"] = rsirunit;
                dr1["propqty"] = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["propqty"];
                tbl1.Rows.Add(dr1);
            }

            DataView dv = tbl1.DefaultView;
            dv.Sort = ("rsircode, spcfcod");
            tbl1 = this.HiddenSameData(dv.ToTable());
            Session["SuplRes"] = tbl1;

            this.gvResInfo_DataBind();
        }

        protected void lbtnSelectResAll_Click(object sender, EventArgs e)
        {
            string fcode = "001";
            string tcode = this.ddlCurrency.SelectedValue.ToString();
            double conrate = 1.00;
            conrate = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;

            DataTable dt = (DataTable)Session["SuplRes"];
            string mResCode = this.ddlResList2.SelectedValue.ToString();
            string rsirunit = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["rsirunit"].ToString();
            for (int i = 0; i < this.ddlSpecification02.Items.Count; i++)
            {
                string mSpcfCod = this.ddlSpecification02.Items[i].Value.ToString();
                DataRow[] dr2 = dt.Select("rsircode = '" + mResCode + "' and  spcfcod='" + mSpcfCod + "'");
                if (dr2.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["ssircode"] = this.ddlSupl2.SelectedValue.ToString();
                    dr1["rsircode"] = this.ddlResList2.SelectedValue.ToString(); ;
                    dr1["rsirdesc1"] = this.ddlResList2.SelectedItem.Text.Trim();
                    dr1["spcfcod"] = this.ddlSpecification02.Items[i].Value.ToString();
                    dr1["spcfdesc"] = this.ddlSpecification02.Items[i].Text.Trim();
                    dr1["rmrks"] = "";
                    dr1["delsys"] = 0.00;
                    dr1["paysys"] = 0.00;
                    dr1["rate"] = Convert.ToDouble((((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["stdrat"]) / conrate;
                    dr1["paylimit"] = 0.00;
                    dr1["curcode"] = this.ddlCurrency.SelectedValue.ToString();
                    dr1["curdesc"] = this.ddlCurrency.SelectedItem.Text;
                    dr1["conrate"] = conrate;
                    dr1["rsirunit"] = rsirunit;
                    dr1["propqty"] = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["propqty"];

                    dt.Rows.Add(dr1);
                }
            }
            DataView dv = dt.DefaultView;
            dv.Sort = ("rsircode, spcfcod");
            dt = this.HiddenSameData(dv.ToTable());
            Session["SuplRes"] = dt;
            this.gvResInfo_DataBind();

        }

        protected void lbtnSelectReaSpesAll_Click(object sender, EventArgs e)
        {
            string fcode = "001";
            string tcode = this.ddlCurrency.SelectedValue.ToString();
            double conrate = 1.00;
            conrate = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;

            DataTable dt = (DataTable)Session["SuplRes"];
            DataTable dt2 = ((DataTable)Session["tblSpcf"]).Copy();

            for (int i = 0; i < this.ddlResList2.Items.Count; i++)
            {

                string mResCode = this.ddlResList2.Items[i].Value.ToString();
                string msmcfcod = this.ddlResList2.Items[i].Value.ToString().Substring(0, 9);
                string rsirunit = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["rsirunit"].ToString();
                DataView dv = dt2.DefaultView;



                dv.RowFilter = ("rsircode='" + mResCode + "' or spcfcod='000000000000'");
                DataTable dt3 = dv.ToTable();
                for (int j = 0; j < dt3.Rows.Count; j++)
                {

                    string mSpcfCod = dt3.Rows[j]["spcfcod"].ToString();
                    DataRow[] dr2 = dt.Select("rsircode = '" + mResCode + "' and  spcfcod='" + mSpcfCod + "'");
                    if (dr2.Length == 0)
                    {
                        DataRow dr1 = dt.NewRow();
                        dr1["ssircode"] = this.ddlSupl2.SelectedValue.ToString();
                        dr1["rsircode"] = mResCode;
                        dr1["rsirdesc1"] = this.ddlResList2.Items[i].Text.ToString();
                        dr1["spcfcod"] = mSpcfCod;
                        dr1["spcfdesc"] = dt3.Rows[j]["spcfdesc"].ToString();
                        dr1["rmrks"] = "";
                        dr1["delsys"] = 0.00;
                        dr1["paysys"] = 0.00;
                        dr1["rate"] = Convert.ToDouble((((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["stdrat"]) / conrate;
                        dr1["paylimit"] = 0.00;
                        dr1["curcode"] = this.ddlCurrency.SelectedValue.ToString();
                        dr1["curdesc"] = this.ddlCurrency.SelectedItem.Text;
                        dr1["conrate"] = conrate;
                        dr1["rsirunit"] = rsirunit;
                        dr1["propqty"] = (((DataTable)Session["tblMat"]).Select("rsircode='" + mResCode + "'"))[0]["propqty"];
                        dt.Rows.Add(dr1);
                    }
                }
            }

            DataView dv1 = dt.DefaultView;
            dv1.Sort = ("rsircode, spcfcod");
            dt = this.HiddenSameData(dv1.ToTable());
            Session["SuplRes"] = dt;
            this.gvResInfo_DataBind();
        }

        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            if (ddlSurveyType.SelectedIndex == 1)
            {
                this.Session_tblResSupl_Update();
                this.gvSuplInfo_DataBind();
            }
            else if (ddlSurveyType.SelectedIndex == 2)
            {
                this.Session_tblSuplRes_Update();
                this.gvResInfo_DataBind();
            }
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

            this.Session_tblSuplRes_Update();
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

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATESUPLRES",
                              mSSIRCODE, mRSIRCODE, mSPCFCOD, mRMRKS, mDelsys, mPayss, mRate, mPaylimit, mCurcode, mConrate, "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update failed');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
                }
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = this.Label1.Text;
                string eventdesc = "Update Resource Info";
                string eventdesc2 = "Update Material";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        protected void gvResInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["SuplRes"];
            string Supcode = this.ddlSupl2.SelectedValue.ToString();
            string Rescode = ((Label)this.gvResInfo.Rows[e.RowIndex].FindControl("lblgvResCod1")).Text.Trim();
            string Spcfcod = ((Label)this.gvResInfo.Rows[e.RowIndex].FindControl("lblgvspcfcod")).Text.Trim();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "DELETESUPLRES",
                             Supcode, Rescode, Spcfcod, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvResInfo.PageSize) * (this.gvResInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("SuplRes");
            Session["SuplRes"] = dv.ToTable();
            this.gvResInfo_DataBind();
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

        protected void ImgbtnFindMat_Click(object sender, EventArgs e)
        {
            string mSrchTxt = this.txtMSRResSearch.Text.Trim() + "%";
            this.Resource_List(mSrchTxt);
            this.ddlMSRRes.DataTextField = "rsirdesc1";
            this.ddlMSRRes.DataValueField = "rsircode";
            this.ddlMSRRes.DataSource = (DataTable)Session["tblMat"];
            this.ddlMSRRes.DataBind();
            this.GetSpecificationms();
        }

        private void GetSpecificationms()
        {

            DataTable dt = ((DataTable)Session["tblSpcf"]).Copy();
            string Resource01 = this.ddlMSRRes.SelectedValue.ToString().Substring(0, 9);

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("mspcfcod='" + Resource01 + "' or mspcfcod='000000000'");
            dt = dv.ToTable();

            this.ddlSpecificationms.DataTextField = "spcfdesc";
            this.ddlSpecificationms.DataValueField = "spcfcod";
            this.ddlSpecificationms.DataSource = dt;
            this.ddlSpecificationms.DataBind();
            this.ImgbtnFindSup_Click(null, null);

        }


        protected void ddlMSRRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSpecificationms();
        }

        protected void ImgbtnFindSpecificationms_Click(object sender, EventArgs e)
        {
            this.GetSpecificationms();
        }

        protected void ddlSpecificationms_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnFindSup_Click(null, null);
        }

        protected void ImgbtnFindSup_Click(object sender, EventArgs e)
        {
            Session.Remove("Supplier");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = this.txtMSRSupSearch.Text.Trim() + "%";
            string mResCode = this.ddlMSRRes.SelectedValue.ToString();
            string mSpcfCod = this.ddlSpecificationms.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", mSrchTxt, mResCode, mSpcfCod, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
            Session["Supplier"] = ds1.Tables[0];
            this.ddlMSRSupl.DataTextField = "ssirdesc1";
            this.ddlMSRSupl.DataValueField = "ssircode";
            this.ddlMSRSupl.DataSource = ds1.Tables[0];
            this.ddlMSRSupl.DataBind();
        }
        protected void ImgbtnFindPreMR_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string premrlist = "%" + this.txtPreMSRSearch.Text + "%";
            string CurDate1 = this.GetStdDate(this.txtCurMSRDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETPREVMSRLIST", CurDate1,
                          premrlist, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
            this.ddlPrevMSRList.Items.Clear();
            this.ddlPrevMSRList.DataTextField = "msrno1";
            this.ddlPrevMSRList.DataValueField = "msrno";
            this.ddlPrevMSRList.DataSource = ds1.Tables[0];
            this.ddlPrevMSRList.DataBind();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void lbtnMSRTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblMSR_Update();
        }
        protected void gvResInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_tblSuplRes_Update();
            this.gvResInfo.PageIndex = e.NewPageIndex;

            this.gvResInfo_DataBind();
        }

        protected void gvSuplInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_tblResSupl_Update();
            this.gvSuplInfo.PageIndex = e.NewPageIndex;

            this.gvSuplInfo_DataBind();
        }



        protected void lbtnreqsurv_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string requisition = this.ddlRequision.SelectedValue.ToString();
            string supplier = this.ddlSupl2.SelectedValue.ToString();
            if (supplier == "" || supplier.Substring(0, 2) != "99")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please select supplier";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_REQ_CENSTORE", "SURVEYLINK_FROM_REQUISITION", requisition, supplier, "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;
            Session["SuplRes"] = ds1.Tables[0];
            this.Panel3.Visible = true;
            this.gvResInfo_DataBind();
        }

        private void GetGroupList()
        {
            Session.Remove("tblresleb2");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            DataSet ds1 = this.purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_RESCODE_LEVEL2_ISSUE", "", "", userid, "", "", "", "", "", "");

            DataTable dt1 = new DataTable();
            DataView dvgrp = ds1.Tables[0].DefaultView;
            dvgrp.RowFilter = ("sircode not like '045%'");
            dt1 = dvgrp.ToTable();

            Session["tblresleb2"] = dt1;
            ds1.Dispose();
        }

        private void SelectGroupList()
        {
            DataTable dt = ((DataTable)Session["tblresleb2"]).Copy();

            this.ddlGroup.DataTextField = "sirdesc";
            this.ddlGroup.DataValueField = "sircode";
            this.ddlGroup.DataSource = dt;
            this.ddlGroup.DataBind();
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gSrchTxt = this.txtGrpSrch.Text.Trim() + "%";
            this.Resource_List(gSrchTxt);
        }

        protected void LinkGrpButton_Click(object sender, EventArgs e)
        {
            this.GetGroupList();
            this.SelectGroupList();

            string gSrchTxt = this.txtGrpSrch.Text.Trim() + "%";
            this.Resource_List(gSrchTxt);
        }

        protected void ddlCurList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tcode = "001";
            string fcode = this.ddlCurList.SelectedValue.ToString();

            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            var List = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode)).ToList();

            double method = (List.Count > 0) ? List[0].conrate : 0;

            this.TextBoxCRate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int index = this.ddlSurveyType.SelectedIndex;

            switch (index)
            {
                case 1:
                    this.lbtnSuplUpdate_Click(sender, e);
                    break;

                case 2:
                    this.lbtnResUpdate_Click(sender, e);
                    break;

                default:
                    break;
            }
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            try
            {
                int index = this.ddlSurveyType.SelectedIndex;

                switch (index)
                {
                    case 1:
                        SaveArchive();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SaveArchive()
        {
            try
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr1[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    return;
                }

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                this.Session_tblResSupl_Update();
                DataTable tbl1 = (DataTable)Session["ResSupl"];

                string dayid = DateTime.Now.ToString("yyyyMMdd");

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mSSIRCODE = tbl1.Rows[i]["ssircode"].ToString();
                    string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                    string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                    string mRMRKS = tbl1.Rows[i]["rmrks"].ToString();
                    string mDelsys = tbl1.Rows[i]["delsys"].ToString();
                    string mPaysys = tbl1.Rows[i]["paysys"].ToString();
                    string mRate = tbl1.Rows[i]["rate"].ToString();
                    string mPaylimit = tbl1.Rows[i]["paylimit"].ToString();
                    string mCurcode = tbl1.Rows[i]["curcode"].ToString();
                    string mConrate = tbl1.Rows[i]["conrate"].ToString();

                    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATESUPLRESARCHIVE",
                                  mSSIRCODE, mRSIRCODE, mSPCFCOD, mRMRKS, mDelsys, mPaysys, mRate, mPaylimit, mCurcode, mConrate, "", dayid, "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update failed');", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
                    }
                }

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = this.Label1.Text;
                    string eventdesc = "Update Supplier Info Archive";
                    string eventdesc2 = "Update Supplier Archive";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lblgvDate_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string mResCode = this.ddlResList1.SelectedValue.ToString();
                string spcfcod = this.ddlSpecification.SelectedValue.ToString();
                string dayid = Convert.ToDateTime(((LinkButton)sender).Text).ToString("yyyyMMdd");

                ViewState["selecteddate"] = Convert.ToDateTime(((LinkButton)sender).Text).ToString("dd-MMM-yyyy");

                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETRESSUPLRLISTBYDATE", mResCode, spcfcod, dayid);

                if (ds1 == null)
                    return;

                Session["ResSupl"] = this.HiddenSameData(ds1.Tables[0]);

                this.gvSuplInfo_DataBind();
            }
            catch(Exception ex)
            {

            }
        }
    }
}