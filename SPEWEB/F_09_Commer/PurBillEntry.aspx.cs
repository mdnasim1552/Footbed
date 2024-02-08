using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_09_Commer
{
    public partial class PurBillEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Invoice";
                this.txtCurBillDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtBillrefDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtChequeDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.TblProject();
                this.txtExpDate.Text = System.DateTime.Today.AddMonths(3).ToString("dd-MMM-yyyy");
                this.lblPreBill.Visible = (this.Request.QueryString["Type"] == "BillEntry") ? false : true;
                this.txtSrchPreBill.Visible = (this.Request.QueryString["Type"] == "BillEntry") ? false : true;
                this.lbtnPrevBillList.Visible = (this.Request.QueryString["Type"] == "BillEntry") ? false : true;
                this.ddlPrevBillList.Visible = (this.Request.QueryString["Type"] == "BillEntry") ? false : true;
                //  this.GridFieldVisible();
                this.GetOrderList();
                if (this.Request.QueryString["genno"].Length != 0)
                {

                    this.lbtnOk_Click(null, null);

                }
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

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void TblProject()
        {
            if (Session["tblproject"] == null)
            {
                DataTable tblproject = new DataTable();
                tblproject.Columns.Add("pactcode", Type.GetType("System.String"));
                tblproject.Columns.Add("pactdesc", Type.GetType("System.String"));
                Session["tblproject"] = tblproject;
            }
        }



        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private string CompanyBill()
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
                    PrintReq = "PrintBill02";
                    break;

                case "3101":
                    PrintReq = "PrintBill03";

                    break;

                default:
                    PrintReq = "PrintBill03";
                    break;
            }

            return PrintReq;

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string printcomreq = this.CompanyBill();


            this.PrintBill01();
        }

        private void PrintBill01()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            //string mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            string mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[1].Rows.Count == 0)
            {
                return;
            }
            DataTable dt = ds1.Tables[0];
            DataTable dt1 = ds1.Tables[1];

            mBILLNo= Convert.ToString(dt1.Rows[0]["billno1"]);


            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var lst = dt.DataTableToList<SPEENTITY.C_09_Commer.EClassBBLMatInfo>();
            mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            string mrrdat =Convert.ToDateTime(dt1.Rows[0]["billdat"]).ToString("dd-MMM-yyyy");
            string orderno = Convert.ToString(dt.Rows[0]["orderno1"]);
            string mrfno = Convert.ToString(dt.Rows[0]["mrfno"]);
            string challno = Convert.ToString(dt.Rows[0]["chlnno"]);
            string acname= Convert.ToString(dt.Rows[0]["pactdesc"]);

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_09_Commer.RptMatBBLInfo", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Software Generated Bill"));
            Rpt1.SetParameters(new ReportParameter("mrrno", lst[0].mrrno1));
            Rpt1.SetParameters(new ReportParameter("mBILLNo", mBILLNo));
            Rpt1.SetParameters(new ReportParameter("mrrdat", mrrdat));
            Rpt1.SetParameters(new ReportParameter("orderno", orderno));
            Rpt1.SetParameters(new ReportParameter("mrfno", mrfno));
            Rpt1.SetParameters(new ReportParameter("challno", challno));
            Rpt1.SetParameters(new ReportParameter("acname", acname));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Software Generated Bill"));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #region Old
            //ReportDocument rptstk = new MFGRPT.R_07_Inv.rptBillIConfirmation();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rpttxtsupplier = rptstk.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            //rpttxtsupplier.Text = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
            //TextObject rpttxtbillno = rptstk.ReportDefinition.ReportObjects["billno"] as TextObject;
            //rpttxtbillno.Text = this.lblCurBillNo1.Text.Trim() + this.txtCurBillNo2.Text.Trim();
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //rpttxtdate.Text = CurDate1;

            //TextObject rpttxReqNo = rptstk.ReportDefinition.ReportObjects["rpttxReqNo"] as TextObject;
            //rpttxReqNo.Text = ds1.Tables[1].Rows[0]["mrfno"].ToString();

            //TextObject rpttxtOrderNo = rptstk.ReportDefinition.ReportObjects["rpttxtOrderNo"] as TextObject;
            //rpttxtOrderNo.Text = ds1.Tables[0].Rows[0]["orderno1"].ToString();
            //TextObject rpttxtMrrNo = rptstk.ReportDefinition.ReportObjects["rpttxtMrrNo"] as TextObject;
            //rpttxtMrrNo.Text = ds1.Tables[0].Rows[0]["mrrref"].ToString();
            ////TextObject rpttxtMrrDate = rptstk.ReportDefinition.ReportObjects["rpttxtMrrDate"] as TextObject;
            ////rpttxtMrrDate.Text = Convert.ToDateTime( ds1.Tables[0].Rows[0]["mrrdate"]).ToString("dd-MMM-yyyy");
            //TextObject rpttxtChalanNo = rptstk.ReportDefinition.ReportObjects["rpttxtChalanNo"] as TextObject;
            //rpttxtChalanNo.Text = ds1.Tables[0].Rows[0]["chlnno"].ToString();

            //TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = "A/C Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString();
            ////TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            ////rpttxtdate.Text = "Date: " + CurDate1;


            ////TextObject rpttxtTotalAmount = rptstk.ReportDefinition.ReportObjects["txtTotalAmount"] as TextObject;
            ////rpttxtTotalAmount.Text = (amt1 - amt2).ToString("#,##0;(#,##0); ");

            //TextObject rpttxtTaka = rptstk.ReportDefinition.ReportObjects["takainword"] as TextObject;
            //rpttxtTaka.Text = "Taka In Word: " + ASTUtility.Trans((amt1 - amt2), 2);
            //TextObject rpttxtNarration = rptstk.ReportDefinition.ReportObjects["txtNarration"] as TextObject;
            //rpttxtNarration.Text = this.txtBillNarr.Text.Trim();

            ////Signing Part

            //TextObject rpttxtReqIns = rptstk.ReportDefinition.ReportObjects["txtReq"] as TextObject;
            //rpttxtReqIns.Text = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            //TextObject rpttxtReqAppIns = rptstk.ReportDefinition.ReportObjects["txtReqA"] as TextObject;
            //rpttxtReqAppIns.Text = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            //TextObject rpttxtOrdIns = rptstk.ReportDefinition.ReportObjects["txtOrd"] as TextObject;
            //rpttxtOrdIns.Text = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            //TextObject rpttxtOrderdIns = rptstk.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
            //rpttxtOrderdIns.Text = ds1.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            //TextObject rpttxtmrrInns = rptstk.ReportDefinition.ReportObjects["txtmrr"] as TextObject;
            //rpttxtmrrInns.Text = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();

            ////TextObject rpttxtcheckIns = rptstk.ReportDefinition.ReportObjects["check"] as TextObject;
            ////rpttxtcheckIns.Text = ds1.Tables[3].Rows[0]["checknam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["checkdat"].ToString();
            //TextObject rpttxtbillIns = rptstk.ReportDefinition.ReportObjects["txtbill"] as TextObject;
            //rpttxtbillIns.Text = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            //rptstk.SetDataSource(this.HiddenSameData(ds1.Tables[0]));
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");


            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion

        }

        protected void lbtnPrevBillList_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETPREVBILLLIST", CurDate1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevBillList.Items.Clear();
            this.ddlPrevBillList.DataTextField = "billno1";
            this.ddlPrevBillList.DataValueField = "billno";
            this.ddlPrevBillList.DataSource = ds1.Tables[0];
            this.ddlPrevBillList.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                Session.Remove("TblProject");
                this.lblPreBill.Visible = (this.Request.QueryString["Type"] == "BillEntry") ? false : true;
                this.txtSrchPreBill.Visible = (this.Request.QueryString["Type"] == "BillEntry") ? false : true;
                this.lbtnPrevBillList.Visible = (this.Request.QueryString["Type"] == "BillEntry") ? false : true;
                this.ddlPrevBillList.Visible = (this.Request.QueryString["Type"] == "BillEntry") ? false : true;
                this.ddlPrevBillList.Items.Clear();
                this.ddlOrderList.Enabled = true;
                this.lblCurBillNo1.Text = "PBL" + DateTime.Today.ToString("MM") + "-";
                this.txtCurBillDate.Enabled = true;
                this.lblSupplier.Text = "";
                this.lblReqno.Text = "";
                this.txtBillRef.Text = "";


                this.ddlOrderList.Items.Clear();
                this.ddlProjectName.Items.Clear();
                this.ddlCharge.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtBillNarr.Text = "";
                this.txtAccVounum.Text = "";
                this.gvBillInfo.DataSource = null;
                this.gvBillInfo.DataBind();

                this.Panel2.Visible = false;
                this.chkCharging.Visible = false;
                this.lbtnOk.Text = "Ok";
                return;
            }
            this.lbtnPrevBillList.Visible = false;
            this.ddlPrevBillList.Visible = false;
            this.ddlOrderList.Enabled = false;
            //this.lbtnSupplierList.Enabled = false;

            this.txtCurBillNo2.ReadOnly = true;

            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.chkCharging.Visible = true;
            this.Get_PurchaseBill_Info();

        }



        protected void Session_tblBill_Update()
        {

            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvBillInfo.Rows.Count; j++)
            {
                double dgvMRRQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvBillInfo.Rows[j].FindControl("lblgvMRRQty")).Text.Trim()));
                double dgvMRRAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBillInfo.Rows[j].FindControl("txtgvMRRAmt")).Text.Trim()));
                double dgvMRRRate = (dgvMRRQty > 0) ? dgvMRRAmt / dgvMRRQty : 00;
                TblRowIndex2 = (this.gvBillInfo.PageIndex) * this.gvBillInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["mrrqty"] = dgvMRRQty;
                tbl1.Rows[TblRowIndex2]["mrrrate"] = dgvMRRRate;
                tbl1.Rows[TblRowIndex2]["mrramt"] = dgvMRRAmt;
            }
            ViewState["tblBill"] = tbl1;
        }

        private void GridColoumnVisible()
        {  //Only For Sanmar
            string comcod = this.GetCompCode();
            if (comcod != "3301")
                return;
            if (this.Request.QueryString["Type"].ToString().Trim() == "BillEntry")
            {


                DataTable tbl1 = (DataTable)ViewState["tblBill"];
                int TblRowIndex2;
                for (int j = 0; j < this.gvBillInfo.Rows.Count; j++)
                {

                    TblRowIndex2 = (this.gvBillInfo.PageIndex) * this.gvBillInfo.PageSize + j;
                    string mRSIRCODE = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();
                    if (ASTUtility.Left(mRSIRCODE, 7) != "0199999")
                        ((TextBox)this.gvBillInfo.Rows[j].FindControl("txtgvMRRAmt")).ReadOnly = true;

                }

            }

        }

        protected void gvBillInfo_DataBind()
        {

            try
            {
                DataTable tbl1 = (DataTable)ViewState["tblBill"];
                this.gvBillInfo.DataSource = tbl1;
                this.gvBillInfo.DataBind();
                this.gvBillInfo.Columns[6].Visible = (this.Request.QueryString["Type"].ToString().Trim() == "BillEdit" && this.lblvalvounum.Text.Trim() == "00000000000000");

                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = (this.lblvalvounum.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");
                //this.lnkFinalUpdate.Visible = (this.lblvalvounum.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");
                //((LinkButton)this.gvBillInfo.FooterRow.FindControl("lbtnUpdateBill")).Visible = (this.lblvalvounum.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");


                ((LinkButton)this.gvBillInfo.FooterRow.FindControl("lbtnDeleteBill")).Visible = (this.Request.QueryString["Type"].ToString().Trim() == "BillEdit" && this.lblvalvounum.Text.Trim() == "00000000000000");

                this.GridColoumnVisible();
                ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).Visible = false;
                double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvBillInfo.PageSize);
                ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                for (int i = 1; i <= TotalPage; i++)
                    ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                if (TotalPage > 1)
                    ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).Visible = true;
                ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvBillInfo.PageIndex;
                this.lbtnResFooterTotal_Click(null, null);
            }

            catch (Exception ex)
            {

            }
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            dv.Sort = "grp1, rsircode";
            dt1 = dv.ToTable();
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



        protected void GetBillNo()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            string mBILLNo = "NEWBILL";
            if (this.ddlPrevBillList.Items.Count > 0)
                mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            if (mBILLNo == "NEWBILL")
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTBILLINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurBillNo1.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(0, 6);
                    this.txtCurBillNo2.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(6, 5);
                    this.ddlPrevBillList.DataTextField = "maxbillno1";
                    this.ddlPrevBillList.DataValueField = "maxbillno";
                    this.ddlPrevBillList.DataSource = ds1.Tables[0];
                    this.ddlPrevBillList.DataBind();
                }

            }
        }


        protected void Get_PurchaseBill_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            string mBILLNo = "NEWBILL";
            if (this.ddlPrevBillList.Items.Count > 0)
            {
                this.txtCurBillDate.Enabled = false;
                mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURQCBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            ViewState["tblBill"] = this.HiddenSameData(ds1.Tables[0]);
            if (mBILLNo == "NEWBILL")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTBILLINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurBillNo1.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(0, 6);
                    this.txtCurBillNo2.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(6, 5);
                }




                string Ordercod = this.ddlOrderList.SelectedValue.ToString().Substring(12, 14);
                DataSet dsmr = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETBILLALLQCLIST", "", Ordercod, "", "", "", "", "", "", "");
                if (dsmr == null)
                    return;
                ViewState["tblMat"] = dsmr.Tables[0];
                ViewState["tblcar"] = dsmr.Tables[1];

                this.GetMatBill();

                return;
            }



            if (ds1.Tables[1].Rows.Count > 0)
            {


                Session["tblorder"] = ds1.Tables[1];
                this.ddlOrderList.DataTextField = "textfield";
                this.ddlOrderList.DataValueField = "valuefield";
                this.ddlOrderList.DataSource = ds1.Tables[1];
                this.ddlOrderList.DataBind();

            }
            this.ddlOrderList.SelectedValue = ds1.Tables[1].Rows[0]["valuefield"].ToString();
            this.GetVesselAReqNo();

            this.txtSrchProjectName.Text = ds1.Tables[0].Rows[0]["pactdesc"].ToString();

            this.txtBillRef.Text = ds1.Tables[1].Rows[0]["billref"].ToString();
            this.lblCurBillNo1.Text = ds1.Tables[1].Rows[0]["billno1"].ToString().Substring(0, 6);
            this.txtCurBillNo2.Text = ds1.Tables[1].Rows[0]["billno1"].ToString().Substring(6, 5);
            this.txtCurBillDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdat"]).ToString("dd.MM.yyyy");
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["billbydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtBillNarr.Text = ds1.Tables[1].Rows[0]["billnar"].ToString();
            this.txtAccVounum.Text = ds1.Tables[1].Rows[0]["vounum"].ToString();
            this.lblvalvounum.Text = ds1.Tables[1].Rows[0]["vounum"].ToString();
            this.txtBillrefDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billrefdat"]).ToString("dd.MM.yyyy");
            this.txtChequeDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["chequedat"]).ToString("dd.MM.yyyy");
            this.ddlPayType.SelectedValue = ds1.Tables[1].Rows[0]["paytype"].ToString();
            this.ddlPayType_SelectedIndexChanged(null, null);
            this.gvBillInfo_DataBind();



        }

        private void GetMatBill()
        {

            this.Session_tblBill_Update();
            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            DataTable tbl2 = (DataTable)ViewState["tblMat"];
            foreach (DataRow dr2 in tbl2.Rows)
            {

                DataRow dr1 = tbl1.NewRow();
                dr1["pactcode"] = dr2["pactcode"].ToString();
                dr1["reqno"] = dr2["reqno"].ToString();
                dr1["reqno1"] = dr2["reqno1"].ToString();
                dr1["orderno"] = ASTUtility.Right(this.ddlOrderList.SelectedValue.ToString().Trim(), 14);
                dr1["orderno1"] = this.ddlOrderList.SelectedItem.Text.Substring(13, 11);
                dr1["mrrno"] = dr2["mrrno"].ToString();
                dr1["mrrno1"] = dr2["mrrno1"].ToString();
                dr1["qcno"] = this.ddlOrderList.SelectedValue.ToString().Substring(12, 14);
                dr1["qcno1"] = this.ddlOrderList.SelectedItem.Text.Substring(0, 11);
                dr1["mrrref"] = dr2["mrrref"].ToString();
                dr1["mrrdat"] = dr2["mrrdat"].ToString();
                dr1["bomid"] = dr2["bomid"].ToString();
                dr1["rsircode"] = dr2["rsircode"].ToString();
                dr1["spcfcod"] = dr2["spcfcod"].ToString();
                dr1["pactdesc"] = dr2["pactdesc"].ToString();
                dr1["rsirdesc1"] = dr2["rsirdesc1"].ToString();
                dr1["spcfdesc"] = dr2["spcfdesc"].ToString();
                dr1["rsirunit"] = dr2["rsirunit"].ToString();
                dr1["ordrqty"] = dr2["ordrqty"].ToString();
                dr1["mrrqty"] = dr2["mrrqty"].ToString();
                dr1["sqty"] = dr2["sqty"].ToString();
                dr1["mrrrate"] = dr2["mrrrate"].ToString();
                dr1["mrramt"] = dr2["mrramt"].ToString();
                dr1["mmrramt"] = dr2["mrramt"].ToString();
                dr1["remrks"] = dr2["mrrnote"].ToString();
                tbl1.Rows.Add(dr1);

            }

            //Carring FirstTime

            DataRow[] drcar = tbl1.Select("mrrno =''");
            if (drcar.Length == 0)
            {
                DataTable dt = (DataTable)ViewState["tblcar"];
                foreach (DataRow drc in dt.Rows)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["pactcode"] = drc["pactcode"].ToString();

                    dr1["reqno"] = "";
                    dr1["mrrno"] = "";
                    dr1["rsircode"] = drc["rsircode"].ToString();
                    dr1["spcfcod"] = drc["spcfcod"].ToString();
                    dr1["reqno1"] = "";
                    dr1["mrrno1"] = "";
                    dr1["mrrref"] = "";
                    dr1["qcno"] = "";
                    dr1["pactdesc"] = drc["pactdesc"].ToString();
                    dr1["rsirdesc1"] = drc["rsirdesc"].ToString();
                    dr1["spcfdesc"] = "";
                    dr1["rsirunit"] = "";
                    dr1["ordrqty"] = 0.00;
                    dr1["sqty"] = 0.00;
                    dr1["mrrqty"] = 0.00;
                    dr1["mrrrate"] = 0.00;
                    dr1["mrramt"] = drc["orderamt"];
                    dr1["mmrramt"] = 0.00;
                    dr1["remrks"] = "";
                    tbl1.Rows.Add(dr1);

                }


            }

            ViewState["tblBill"] = this.HiddenSameData(tbl1);
            this.gvBillInfo_DataBind();

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblBill_Update();
            this.gvBillInfo.PageIndex = ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvBillInfo_DataBind();
        }
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            double amt1 = 0.00, amt2 = 0.00;
            this.Session_tblBill_Update();
            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            DataTable td1 = tbl1.Copy();
            DataTable td2 = tbl1.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like '019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));

            amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            ((Label)this.gvBillInfo.FooterRow.FindControl("lblgvFooterTMRRAmt")).Text = (amt1 - amt2).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvBillInfo.FooterRow.FindControl("lblgvFMMRRAmt")).Text = (amt1 - amt2).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvBillInfo.FooterRow.FindControl("lblgvSumQc")).Text = Convert.ToDouble(td1.Compute("SUM(mrrqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvBillInfo.FooterRow.FindControl("lblgvSumOrdr")).Text = Convert.ToDouble(td1.Compute("SUM(ordrqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");

        }



        protected void imgSearchOrderno_Click(object sender, EventArgs e)
        {
            this.GetOrderList();

        }
        protected void ddlOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetVesselAReqNo();

        }


        private void GetOrderList()
        {

            string comcod = this.GetCompCode();
            string sircode = this.Request.QueryString["sircode"].ToString() + this.Request.QueryString["genno"].ToString();
            string orderSearch = (sircode.Length == 26) ? sircode + "%" : "%" + this.txtSrchOrderrefno.Text + "%";
            string date = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            //GETBILLSUPLIST
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETQCSUPLIST", orderSearch, date, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblorder"] = ds1.Tables[0];
            this.ddlOrderList.DataTextField = "textfield";
            this.ddlOrderList.DataValueField = "valuefield";
            this.ddlOrderList.DataSource = ds1.Tables[0];
            this.ddlOrderList.DataBind();
            ds1.Dispose();
            this.GetVesselAReqNo();

        }
        private void GetVesselAReqNo()
        {
            DataTable dt = (DataTable)Session["tblorder"];
            if (dt.Rows.Count == 0)
            {
                this.lblSupplier.Text = "";
                this.lblReqno.Text = "";
                return;
            }
            string Valuefield = this.ddlOrderList.SelectedValue.ToString();
            DataRow[] dr1 = dt.Select("valuefield ='" + Valuefield + "'");
            if (dr1.Length > 0)
            {
                this.lblSupplier.Text = dr1[0]["pactdesc"].ToString();
                this.lblReqno.Text = dr1[0]["mrfno"].ToString();

            }

        }








        protected void imgSearchProject_Click(object sender, EventArgs e)
        {

            this.TblProject();
            DataTable dt = (DataTable)ViewState["tblBill"];
            DataTable dt1 = (DataTable)Session["tblproject"];
            if (dt.Rows.Count == 0)
            {
                this.ddlProjectName.Items.Clear();
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Pactcode = dt.Rows[i]["pactcode"].ToString();
                DataRow[] dr1 = dt1.Select("pactcode='" + Pactcode + "'");
                if (dr1.Length == 0)
                {
                    DataRow dr2 = dt1.NewRow();
                    dr2["pactcode"] = Pactcode;
                    dr2["pactdesc"] = dt.Rows[i]["pactdesc"].ToString();
                    dt1.Rows.Add(dr2);

                }

            }

            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = dt1;
            this.ddlProjectName.DataBind();

        }
        protected void imgSearchCharge_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABOURCHARGE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlCharge.DataTextField = "sirdesc";
            this.ddlCharge.DataValueField = "sircode";
            this.ddlCharge.DataSource = ds1.Tables[0];
            this.ddlCharge.DataBind();
            ds1.Dispose();
        }




        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            this.Session_tblBill_Update();
            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            string mProjectCode = this.ddlProjectName.SelectedValue.ToString();
            string mResCode = this.ddlCharge.SelectedValue.ToString();
            string mSpcfCode = "000000000000";
            DataRow[] dr2 = tbl1.Select("pactcode ='" + mProjectCode + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["pactcode"] = mProjectCode;
                dr1["reqno"] = "";
                dr1["reqno"] = "";
                dr1["mrrno"] = "";
                dr1["rsircode"] = mResCode;
                dr1["spcfcod"] = mSpcfCode;
                dr1["reqno1"] = "";
                dr1["mrrno1"] = "";
                dr1["mrrref"] = "";
                dr1["pactdesc"] = this.ddlProjectName.SelectedItem.Text.Trim();
                dr1["rsirdesc1"] = this.ddlCharge.SelectedItem.Text.Trim();
                dr1["spcfdesc"] = "";
                dr1["rsirunit"] = "";
                dr1["ordrqty"] = 0.00;
                dr1["mrrqty"] = 0.00;
                dr1["mrrrate"] = 0.00;
                dr1["mrramt"] = 0.00;
                dr1["mmrramt"] = 0.00;
                dr1["sqty"] = 0.00;
                dr1["remrks"] = "";
                tbl1.Rows.Add(dr1);
            }
            ViewState["tblBill"] = this.HiddenSameData(tbl1);
            this.gvBillInfo_DataBind();
        }

        //protected void lbtnUpdateBill_Click(object sender, EventArgs e)
        //{
        //    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
        //    if (!Convert.ToBoolean(dr1[0]["entry"]))
        //    {
        //        ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
        //        return;
        //    }

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string usrid = hst["usrid"].ToString();
        //    string sessionid = hst["session"].ToString();
        //    string trmid = hst["compname"].ToString();
        //    string comcod = this.GetCompCode();
        //    this.lbtnResFooterTotal_Click(null, null);
        //    if (this.ddlPrevBillList.Items.Count == 0)
        //        this.GetBillNo();
        //    string mBILLNO = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
        //    string mBILLDAT = this.GetStdDate(this.txtCurBillDate.Text.Trim());
        //    string billrefdat = this.GetStdDate(this.txtBillrefDate.Text.Trim());
        //    string ChequeDate = this.GetStdDate(this.txtChequeDate.Text.Trim());
        //    string mSSIRCODE =this.ddlOrderList.SelectedValue.ToString().Substring(0,12);
        //    string mBILLUSRID = "";
        //    string mAPPRUSRID = "";
        //    string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
        //    string mBILLBYDES = this.txtPreparedBy.Text.Trim();
        //    string mAPPBYDES = this.txtApprovedBy.Text.Trim();
        //    string mBILLREF = this.txtBillRef.Text.Trim();
        //    string mBILLNAR = this.txtBillNarr.Text.Trim();
        //    string mVOUNUM = this.txtAccVounum.Text.Trim();
        //    string paytype = this.ddlPayType.SelectedValue.ToString();
        //    if (comcod == "3301")
        //    {
        //        if (mBILLREF == "")
        //        {
        //            ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fill Ref. Number";
        //            return;
        //        }
        //    }
        //    DataTable tbl1 = (DataTable)ViewState["tblBill"];




        //    //For Existing MRR

        //    if (this.Request.QueryString["Type"].ToString().Trim() == "BillEntry")
        //    {

        //        for (int i = 0; i < tbl1.Rows.Count; i++)
        //        {
        //            string mMRRNO = tbl1.Rows[i]["mrrno"].ToString();
        //            string Mreqno = tbl1.Rows[i]["reqno"].ToString();
        //            string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
        //            string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
        //            if (mMRRNO != "")
        //            {
        //                DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "EXISTINGMRRNO", mMRRNO, Mreqno, mRSIRCODE, mSPCFCOD, "", "", "", "", "");
        //                if (ds.Tables[0].Rows.Count == 0) continue;
        //                else
        //                {
        //                    ((Label)this.Master.FindControl("lblmsg")).Text = "MRR No already Existing in this Bill No";
        //                    return;
        //                }
        //            }


        //        }


        //    }

        //    mVOUNUM = (mVOUNUM.Length == 0 ? "00000000000000" : mVOUNUM);
        //    bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURBILLINFO", "PURBILLB",
        //                     mBILLNO, mBILLDAT, mSSIRCODE, mBILLUSRID, mAPPRUSRID, mAPPRDAT, mBILLBYDES,
        //                     mAPPBYDES, mBILLREF, mBILLNAR, mVOUNUM, usrid, sessionid, trmid, billrefdat, ChequeDate, paytype, "", "", "");
        //    if (!result)
        //    {
        //        ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
        //        return;
        //    }


        //    for (int i = 0; i < tbl1.Rows.Count; i++)
        //    {



        //        string mPactcode = tbl1.Rows[i]["pactcode"].ToString();
        //        string mORDERNO = tbl1.Rows[i]["orderno"].ToString();
        //        string mMRRNO = tbl1.Rows[i]["mrrno"].ToString();

        //        if (mMRRNO.Length == 14)
        //        {
        //            bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(tbl1.Rows[i]["mrrdat"].ToString()), Convert.ToDateTime(mBILLDAT));
        //            if (!dcon)
        //            {
        //                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bill Date is equal or greater MRR Date');", true);
        //                return;
        //            }
        //        }



        //        string Mreqno = tbl1.Rows[i]["reqno"].ToString();
        //        string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
        //        string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
        //        string mMRRQTY = tbl1.Rows[i]["mrrqty"].ToString();
        //        string mBILLAMT = tbl1.Rows[i]["mrramt"].ToString();
        //        if (mORDERNO != "")
        //            result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURBILLINFO", "PURBILLA", mBILLNO, mORDERNO, mMRRNO, Mreqno, mRSIRCODE, mSPCFCOD, mMRRQTY, mBILLAMT, "", "", "", "", "", "", "", "", "", "", "", "");

        //        else
        //            result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURBILLINFO", "PURBILLC", mBILLNO, mPactcode, mRSIRCODE, "000000000000", mBILLAMT, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");



        //        if (!result)
        //        {
        //            ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
        //            return;
        //        }
        //    }
        //    this.txtCurBillDate.Enabled = false;
        //    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

        //    if (ConstantInfo.LogStatus == true)
        //    {
        //        string eventtype = "Materials Supply Bill Information";
        //        string eventdesc = "Update Bill";
        //        string eventdesc2 = mBILLNO;
        //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
        //    }
        //}
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblBill_Update();
            gvBillInfo_DataBind();
        }

        protected void gvBillInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblBill"];
            string Billno = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            int rowindex = (this.gvBillInfo.PageSize) * (this.gvBillInfo.PageIndex) + e.RowIndex;

            string mORDERNO = dt.Rows[rowindex]["orderno"].ToString();
            string mMRRNO = dt.Rows[rowindex]["mrrno"].ToString();
            string Mreqno = dt.Rows[rowindex]["reqno"].ToString();
            string mRSIRCODE = dt.Rows[rowindex]["rsircode"].ToString();
            string mSPCFCOD = dt.Rows[rowindex]["spcfcod"].ToString();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELBILLMATERIALS", Billno, mORDERNO, mMRRNO, Mreqno, mRSIRCODE, mSPCFCOD, "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState["tblBill"] = dv.ToTable();
            this.gvBillInfo_DataBind();
        }
        protected void lbtnDeleteBill_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string Billno = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEBILL", Billno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted successfully";

            }


        }

        protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblbillrefdate.Visible = (this.ddlPayType.SelectedValue == "003") ? false : true;
            this.txtBillrefDate.Visible = (this.ddlPayType.SelectedValue == "003") ? false : true;
            this.lblchequedate.Visible = (this.ddlPayType.SelectedValue == "003") ? false : true;
            this.txtChequeDate.Visible = (this.ddlPayType.SelectedValue == "003") ? false : true;
        }
        protected void chkCharging_CheckedChanged(object sender, EventArgs e)
        {
            this.Panel1.Visible = (chkCharging.Checked);
        }

        protected void gvBillInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvMRRQty = (Label)e.Row.FindControl("lblgvMRRQty");
                Label lblgvSQty = (Label)e.Row.FindControl("lblgvSQty");

                double ordrqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ordrqty"));
                double mrrqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "mrrqty"));
                double sqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "sqty"));


                if (ordrqty > mrrqty)
                {
                    lblgvMRRQty.Style.Add("color", "red");
                }
                if (sqty != 0.00)
                {
                    lblgvSQty.Style.Add("color", "red");
                }






            }
        }
        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
            //    return;
            //}
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = comcod + ASTUtility.Right(hst["usrid"].ToString(), 3);
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();

            this.lbtnResFooterTotal_Click(null, null);
            if (this.ddlPrevBillList.Items.Count == 0)
                this.GetBillNo();
            string mBILLNO = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            string mBILLDAT = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            string billrefdat = this.GetStdDate(this.txtBillrefDate.Text.Trim());
            string ChequeDate = this.GetStdDate(this.txtChequeDate.Text.Trim());
            string ExpDate = Convert.ToDateTime(this.txtExpDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mSSIRCODE = this.ddlOrderList.SelectedValue.ToString().Substring(0, 12);
            string mBILLUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mBILLBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string mBILLREF = this.txtBillRef.Text.Trim();
            string mBILLNAR = this.txtBillNarr.Text.Trim();
            string mVOUNUM = this.txtAccVounum.Text.Trim();
            string paytype = this.ddlPayType.SelectedValue.ToString();
            string adjuststatus = (this.ChkBoxAdjust.Checked == true) ? "Y" : "N";
            if (comcod == "3301")
            {
                if (mBILLREF == "")
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fill Ref. Number";
                    return;
                }
            }
            DataTable tbl1 = (DataTable)ViewState["tblBill"];


            //if (!dcon)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Voucher Date is Equal or Greater then Transaction Limt');", true);
            //    return;
            //}

            //For Existing MRR

            if (this.Request.QueryString["Type"].ToString().Trim() == "BillEntry")
            {

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mMRRNO = tbl1.Rows[i]["mrrno"].ToString();
                    string qcno = tbl1.Rows[i]["qcno"].ToString();
                    string Mreqno = tbl1.Rows[i]["reqno"].ToString();
                    string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                    string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                    string bomid = tbl1.Rows[i]["bomid"].ToString();
                    if (mMRRNO != "")
                    {
                        DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "EXISTINGMRRNO", mMRRNO, Mreqno, mRSIRCODE, mSPCFCOD, bomid, qcno, "", "", "");
                        if (ds.Tables[0].Rows.Count == 0) continue;
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "MRR No already Existing in this Bill No";
                            return;
                        }
                    }


                }


            }

            mVOUNUM = (mVOUNUM.Length == 0 ? "00000000000000" : mVOUNUM);
            bool result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURBILLINFO", "PURBILLB",
                             mBILLNO, mBILLDAT, mSSIRCODE, mBILLUSRID, mAPPRUSRID, mAPPRDAT, mBILLBYDES,
                             mAPPBYDES, mBILLREF, mBILLNAR, mVOUNUM, usrid, sessionid, trmid, billrefdat, ChequeDate, paytype, ExpDate, adjuststatus, "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                return;
            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {



                string mPactcode = tbl1.Rows[i]["pactcode"].ToString();
                string mORDERNO = tbl1.Rows[i]["orderno"].ToString();
                string mMRRNO = tbl1.Rows[i]["mrrno"].ToString();

                if (mMRRNO.Length == 14)
                {
                    bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(tbl1.Rows[i]["mrrdat"].ToString()), Convert.ToDateTime(mBILLDAT));
                    if (!dcon)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bill Date is equal or greater MRR Date');", true);
                        return;
                    }
                }



                string Mreqno = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string bomid = tbl1.Rows[i]["bomid"].ToString();
                string mMRRQTY = tbl1.Rows[i]["mrrqty"].ToString();
                string mBILLAMT = tbl1.Rows[i]["mrramt"].ToString();
                string mQCNO = tbl1.Rows[i]["qcno"].ToString();
                if (mORDERNO != "")
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURBILLINFO", "PURBILLA", mBILLNO, mORDERNO, mMRRNO, Mreqno, mRSIRCODE, mSPCFCOD, mMRRQTY, mBILLAMT, mQCNO, bomid, "", "", "", "", "", "", "", "", "", "");

                else
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURBILLINFO", "PURBILLC", mBILLNO, mPactcode, mRSIRCODE, "000000000000", mBILLAMT, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");



                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    return;
                }
            }
            string postedDate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            if (this.CheckAccUpdate.Checked == true)
            {
                result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEBILLJV", mBILLNO, mBILLDAT, usrid, trmid, sessionid, postedDate, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            }

            this.txtCurBillDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Supply Bill Information";
                string eventdesc = "Update Bill";
                string eventdesc2 = mBILLNO;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
    }
}