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
using SPEENTITY;

namespace SPEWEB.F_19_EXP
{
    public partial class ExportReturn : System.Web.UI.Page
    {

        UserManager objUserService = new UserManager();

        ProcessAccess purData = new ProcessAccess();
        static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Replace("%20", " "), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((LinkButton)this.Master.FindControl("lnkbtnSave")).Enabled = (Convert.ToBoolean(dr1[0]["entry"]));
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                this.txtCurMRRDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Export Return";
                this.GetLCCode();
                //   GetInvoiceNo();
                CreateTable();

                this.CommonButton();



            }
        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(btnForward_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);

            // ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }
        private void GetLCCode()
        {

            string comcod = this.GetComeCode();
            string filter = "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_INV_STDANA", "GETORDERMLCCOD", filter, "", "", "", "", "", "", "");
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();

            this.ddlmlccode.DataSource = lst.Select(m => new { m.mlccod, m.mlcdesc }).Distinct().ToList();
            this.ddlmlccode.DataTextField = "mlcdesc";
            this.ddlmlccode.DataValueField = "mlccod";
            this.ddlmlccode.DataBind();
            if (Request.QueryString["actcode"].ToString() != "")
            {
                this.ddlmlccode.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.ddlmlccode.Enabled = false;
            }
            ds2.Dispose();
            this.ddlmlccode_SelectedIndexChanged(null, null);
        }
        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInvoiceNo();
        }

        private void PreviousList()
        {
            //string Centrid = this.ddlmlccode.SelectedValue.ToString();
            //string CurDate1 = this.txtCurMRRDate.Text.ToString().Trim();
            //string CurDate2 = this.txtCurMRRDate.Text.ToString().Trim();


            //List<SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet> lst = objUserService.GetPreReturn(Centrid, CurDate1, CurDate2);
            //if (lst == null)
            //    return;
            //this.ddlPrevList.DataTextField = "retmemo1";
            //this.ddlPrevList.DataValueField = "retmemo";
            //this.ddlPrevList.DataSource = lst;
            //this.ddlPrevList.DataBind();


            //ViewState["tblPreList"] = lst;

            //this.ddlInvoiceList.DataTextField = "invno1";
            //this.ddlInvoiceList.DataValueField = "invno";
            //this.ddlInvoiceList.DataSource = lst;
            //this.ddlInvoiceList.DataBind();

            //if (this.Request.QueryString["genno"].Length > 0)
            //{
            //    this.ddlPrevList.SelectedValue = this.Request.QueryString["genno"].ToString();
            //    this.ddlPrevList_SelectedIndexChanged(null, null);
            //    this.lbtnOk_Click(null, null);
            //}



            //this.GetClientName();


        }
        private void GetInvoiceNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.txtCurMRRDate.Text.Trim();
            string MlcNo = this.ddlmlccode.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETILCINVNO", date, MlcNo, "", "", "", "", "", "", "");
            this.ddlInvoiceList.Items.Clear();
            this.ddlInvoiceList.DataTextField = "textfield";
            this.ddlInvoiceList.DataValueField = "invno";
            this.ddlInvoiceList.DataSource = ds1.Tables[0];
            this.ddlInvoiceList.DataBind();
        }
        protected void ddlInvoiceList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {

        }

        private void CreateTable()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("mlccod", Type.GetType("System.String"));
            tblt01.Columns.Add("sizeid", Type.GetType("System.String"));
            tblt01.Columns.Add("styleid", Type.GetType("System.String"));
            tblt01.Columns.Add("colorid", Type.GetType("System.String"));
            tblt01.Columns.Add("actdesc", Type.GetType("System.String"));
            tblt01.Columns.Add("styledesc", Type.GetType("System.String"));
            tblt01.Columns.Add("colordesc", Type.GetType("System.String"));
            tblt01.Columns.Add("sizedesc", Type.GetType("System.String"));
            tblt01.Columns.Add("invqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("retqty", Type.GetType("System.Double"));
            tblt01.Columns.Add("trnrmrk", Type.GetType("System.String"));
            tblt01.Columns.Add("invno", Type.GetType("System.String"));
            Session["tblt01"] = tblt01;
        }
        protected void lbtnSelectInv_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string invno = this.ddlInvoiceList.SelectedValue.ToString();
            string mlcno = this.ddlmlccode.SelectedValue.ToString();
            string mReturn = "NEWSRET";
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.txtCurMRRDate.Enabled = false;
                mReturn = this.ddlPrevList.SelectedValue.ToString();

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GETINVOICEQTYINFO", invno, mlcno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);

            DataTable tblt01 = (DataTable)Session["tblt01"];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string invno1 = dt1.Rows[i]["invno"].ToString();
                string mlccod = dt1.Rows[i]["mlccod"].ToString();
                string sizeid = dt1.Rows[i]["sizeid"].ToString();
                DataRow[] dr = tblt01.Select("invno='" + invno1 + "' and mlccod='" + mlccod + "' and sizeid='" + sizeid + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = tblt01.NewRow();
                    dr1["mlccod"] = dt1.Rows[i]["mlccod"].ToString();
                    dr1["styleid"] = dt1.Rows[i]["styleid"].ToString();
                    dr1["colorid"] = dt1.Rows[i]["colorid"].ToString();
                    dr1["sizeid"] = dt1.Rows[i]["sizeid"].ToString();
                    dr1["actdesc"] = dt1.Rows[i]["actdesc"].ToString();

                    dr1["styledesc"] = dt1.Rows[i]["styledesc"].ToString();
                    dr1["colordesc"] = dt1.Rows[i]["colordesc"].ToString();
                    dr1["sizedesc"] = dt1.Rows[i]["sizedesc"].ToString();
                    dr1["invqty"] = Convert.ToDouble(dt1.Rows[i]["invqty"]);
                    dr1["retqty"] = Convert.ToDouble(dt1.Rows[i]["retqty"]);

                    dr1["trnrmrk"] = dt1.Rows[i]["trnrmrk"].ToString();
                    dr1["invno"] = dt1.Rows[i]["invno"].ToString();
                    tblt01.Rows.Add(dr1);
                }
            }
            if (tblt01.Rows.Count == 0)
                return;
            Session["tblt01"] = tblt01;
            gvExpRet.DataSource = tblt01;
            gvExpRet.DataBind();

            //calculation();
            //this.lnkFinalUpdate.Visible = true;

            //this.txtCurrntlast6.ReadOnly = false;
            //this.Panel1.Visible = true;


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            string actcode = dt1.Rows[0]["mlccod"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mlccod"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["mlccod"].ToString();
                    dt1.Rows[j]["actdesc"] = "";

                }

                else
                {
                    actcode = dt1.Rows[j]["mlccod"].ToString();

                }

            }
            return dt1;

        }

        protected void SaveValue()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblt01"];


                int index;

                for (int j = 0; j < this.gvExpRet.Rows.Count; j++)
                {

                    double Retqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvExpRet.Rows[j].FindControl("retQty")).Text.Trim()));
                    string remarks = ((TextBox)this.gvExpRet.Rows[j].FindControl("lblgvRemarks")).Text.Trim().ToString();
                    index = (this.gvExpRet.PageIndex) * this.gvExpRet.PageSize + j;

                    dt.Rows[index]["retqty"] = Retqty.ToString();
                    dt.Rows[index]["trnrmrk"] = remarks.ToString();

                }


                Session["tblt01"] = dt;
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }


        }

        protected void GetSaRetNo()
        {

            string Centrid = this.ddlmlccode.SelectedValue.ToString();
            string CurDate1 = this.txtCurMRRDate.Text.Trim().ToString();
            string mInvoiceNo = "NEWSRET";
            if (this.ddlPrevList.Items.Count > 0)
                mInvoiceNo = this.ddlPrevList.SelectedValue.ToString();
            if (mInvoiceNo == "NEWSRET")
            {

                List<SPEENTITY.C_22_Sal.EClassReturn.EClassLastRet> lst = objUserService.GetLastRetNo(CurDate1, Centrid);
                if (lst == null)
                    return;
                this.lblCurNo1.Text = lst[0].maxno1.ToString().Substring(0, 6);
                this.txtCurNo2.Text = lst[0].maxno1.ToString().Substring(6, 5);
                this.ddlPrevList.DataTextField = "maxno1";
                this.ddlPrevList.DataValueField = "maxno";
                this.ddlPrevList.DataSource = lst;
                this.ddlPrevList.DataBind();

            }
        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {

            //    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            //    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            //    if (!Convert.ToBoolean(dr1[0]["entry"]))
            //    {
            //        ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //        return;
            //    }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComeCode();
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblt01"];

            DataSet ds = new DataSet("ds");
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "tbl1";


            if (this.ddlPrevList.Items.Count == 0)
                this.GetSaRetNo();
            string mRETNO = this.lblCurNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtCurMRRDate.Text.Trim(), 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();


            string mRETDAT = this.txtCurMRRDate.Text.Trim().ToString();
            string Invoice = this.ddlInvoiceList.SelectedValue.ToString();
            string mlcode = this.ddlmlccode.SelectedValue.ToString();
            string refno = "";
            string mNAR = "";
            string postedbyid = "";
            string postrmid = "";
            string postseson = "";
            string posteddat = "01-Jan-1900";
            string aprvbyid = "";
            string aprvdat = "";
            string aprvtrmid = "";
            string aprvseson = "";
            string approved = "";
            string vounum = "00000000000";
            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_EXPORT", "INSERTEXPORTRETURNDATA", ds, null, null, mRETNO, mlcode, mRETDAT, mNAR, postedbyid, 
                                                                            postrmid, postseson, posteddat, aprvbyid, aprvdat, aprvtrmid, aprvseson, approved, vounum);

            if (!resulta)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully','" + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string comnam = hst["comnam"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //    string vounum = this.txtcurrentvou.Text.Trim() + this.txtCurrntlast6.Text.Trim();
            //    DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHERPUR",
            //                             vounum, "", "", "", "", "", "", "", "");

            //    ReportDocument rptinfo = new RMGiRPT.R_21_GAcc.rptPrintVoucher();
            //    rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
            //    TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //    txtCompanyName.Text = comnam;
            //    TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //    Session["Report1"] = rptinfo;
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //}
            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            //}
        }

        protected void imgPreVious_Click(object sender, EventArgs e)
        {
            this.GetSaRetNo();
        }

        protected void ddlPrevList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSaRetNo();
        }
    }
}