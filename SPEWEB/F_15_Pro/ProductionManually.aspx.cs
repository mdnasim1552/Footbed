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
using SPEENTITY;
using SPEENTITY.C_22_Sal;
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_15_Pro
{
    public partial class ProductionManually : System.Web.UI.Page
    {

        ProcessAccess purData = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        CommonHelperClass HlpClass = new CommonHelperClass();
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

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Production Entry Manually";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtCurDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.CurrencyInf();
                this.Load_Location();
                this.CenterCustomerInf();
                this.GetSupplier();
                this.GetBatch();
                this.CommonButton();

                if (this.Request.QueryString["Type"] == "Approve" || this.Request.QueryString["Type"] == "EntryRM" || this.Request.QueryString["Type"] == "ApproveRM")
                {
                    PreviousList();

                }
                if (this.Request.QueryString["Type"] == "EntryRM" || this.Request.QueryString["Type"] == "ApproveRM")
                {

                    this.Get_material();
                }


            }
        }
        private void CommonButton()
        {

           // ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

            if (this.Request.QueryString["Type"] == "Approve" || this.Request.QueryString["Type"] == "ApproveRM")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;

                //   ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(lnkbtnNew_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(lnkbtnLedger_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do You Want To Approve This?');";
            if (this.Request.QueryString["Type"] == "Approve" || this.Request.QueryString["Type"] == "ApproveRM")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approve";
            }
        }
        private void Get_material()
        {
            this.MatAddPanel.Visible = true;
            string comcod = this.GetCompCode();
            DataSet ds3 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_FOR_CONSUMPTION", "04%", "", "", "", "", "", "", "", "");
            this.ddlResourcesCost.DataSource = ds3.Tables[0];
            this.ddlResourcesCost.DataTextField = "sirdesc1";
            this.ddlResourcesCost.DataValueField = "sircode1";
            this.ddlResourcesCost.DataBind();
            ViewState["tblresRes"] = ds3.Tables[0];

        }
        private void lnkbtnLedger_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string posteddat = DateTime.Today.ToString("dd-MMM-yyyy");

            string mgrrno = this.Request.QueryString["genno"].ToString();
            string type = "MANPROD";
            if (this.Request.QueryString["Type"] == "ApproveRM")
            {
                type = "PRODISSUE";
            }
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "APPROVEMANPURCHASE", mgrrno, usrid, sessionid, trmid, posteddat, type);
            if (result == true)
            {
                Response.Redirect(prevPage);
                // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Successfully Approved');", true);
            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void CenterCustomerInf()
        {
            string comcod = this.GetCompCode();
            string EntryType = "17%";
            DataSet ds5 = purData.GetTransInfo(comcod, "SP_ENTRY_PRO_QC_INFO", "RETRIVEFGSTOREALL", "%%", EntryType, "", "", "", "", "", "", "");
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataSource = ds5.Tables[0];
            this.ddlProjectName.DataBind();

        }


        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private void GetSupplier()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            this.ddlMSRSupl.DataTextField = "sirdesc";
            this.ddlMSRSupl.DataValueField = "sircode";
            this.ddlMSRSupl.DataSource = ds2.Tables[0];
            this.ddlMSRSupl.DataBind();

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
        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fcode = "001";
            string tcode = this.ddlCurrency.SelectedValue.ToString();

            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;


            this.lblConRate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");


        }

        private void GetBatch()
        {

            this.ddlbatchno.Items.Clear();
            string comcod = this.GetCompCode();
            string FindProject = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETORDERNO", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlbatchno.DataTextField = "mlcdesc";
            this.ddlbatchno.DataValueField = "mlccod";
            this.ddlbatchno.DataSource = ds1.Tables[1];
            this.ddlbatchno.DataBind();
            ViewState["tblordstyle"] = ds1.Tables[0];
            ViewState["tblMasterLc"] = ds1.Tables[1];
            this.ddlbatchno_SelectedIndexChanged(null, null);
        }
        protected void btnview_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlbatchno.SelectedValue.ToString();
            string mlcdesc = this.ddlbatchno.SelectedItem.ToString();
            string stylecolor = this.ddlStyleColor.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction> lst = (List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction>)ViewState["tblOrder"];
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccod + "' and stylecode3='" + stylecolor + "'");
            foreach (DataRow dr in dv1.ToTable().Rows)
            {

                string dayid = dr["dayid"].ToString();
                string styleid = dr["stylecode"].ToString();
                string colorid = dr["colorid"].ToString();
                string sizeid = dr["sizeid"].ToString();
                string styledesc = dr["styledesc1"].ToString();
                string unit = (dr["unit1"].ToString() == "") ? "PAIR" : dr["unit1"].ToString();
                string orderno = dr["artno"].ToString();
                double rate = Convert.ToDouble(dr["rate"]);
                var templst = lst.FindAll(x => x.styleid == styleid && x.colorid == colorid && x.sizeid == sizeid && x.dayid == dayid);
                if (templst.Count > 0)
                    return;
                lst.Add(new SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction(comcod, styleid, colorid, sizeid, mlccod, styledesc, mlcdesc, unit, orderno, 0.00, rate, 0.00, dayid, 0.00,"00000"));
            }
            ViewState["tblOrder"] = lst;
            this.Data_Bind();
        }

        private void PreviousList()
        {
            string CurDate1 = this.GetStdDate(this.txtCurDate.Text.Trim());
            string comcod = GetCompCode();
            string type = "MANPROD";
            if (this.Request.QueryString["Type"] == "ApproveRM" || this.Request.QueryString["Type"] == "EntryRM")
            {
                type = "PRODISSUE";
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "GETPRODMPREVIOUSLIST", CurDate1, type, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevList.DataTextField = "mgrrno1";
            this.ddlPrevList.DataValueField = "mgrrno";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlPrevList.SelectedValue = this.Request.QueryString["genno"].ToString();
                this.lbtnOk_Click(null, null);
            }

        }

        protected void imgPreVious_Click(object sender, EventArgs e)
        {
            this.PreviousList();
        }



        protected void lnkbtnNew_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_13_ProdMon/ProductionManually.aspx?Type=Entry&genno=" + "', target='_self');</script>";
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string prodno = this.lblCurNo1.Text + this.txtCurNo2.Text;
            string orderName = this.ddlbatchno.SelectedItem.Text.Trim();
            string narration = this.txtBillNarr.Text.Trim();
            string date1 = this.GetStdDate(this.txtCurDate.Text.Trim());

            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction> lst = (List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction>)ViewState["tblOrder"];

            LocalReport rpt1 = new LocalReport();

            DataTable dt = (DataTable)ViewState["tblprodcost"];
            List<SPEENTITY.C_15_Pro.BO_Production.EclassManualiProCost> lst1 = dt.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassManualiProCost>();



            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptProductionManually", lst, lst1, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Production Manually Report"));

            rpt1.SetParameters(new ReportParameter("prodno", prodno));
            rpt1.SetParameters(new ReportParameter("orderno", orderName));
            rpt1.SetParameters(new ReportParameter("narration", narration));
            rpt1.SetParameters(new ReportParameter("date", date1));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintCommercialProposal()
        {


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();  //company name
            //string comadd = hst["comadd1"].ToString();  //address
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string CurDate1 = this.GetStdDate(this.txtCurDate.Text.Trim());
            //string refno = this.txtRefno.Text.Trim();

            //// string CustomarN = this.ddlProjectName.SelectedValue.ToString();
            //string supplier = this.ddlMSRSupl.SelectedValue.ToString();
            ////string CustomarN = this.ddlCustomer.SelectedItem.Text.ToString();
            ////string Centrid = this.ddlProjectName.SelectedValue.ToString();
            ////string pactcode = this.ddlProjectName.SelectedValue.ToString();
            ////this.ddlProjectName.SelectedValue = lstord[0].pactcode;

            //List<MFGOBJ.C_22_Sal.EClassComProposal.EClassSalProInfo> lst = (List<MFGOBJ.C_22_Sal.EClassComProposal.EClassSalProInfo>)Session["tblOrder"];


            //List<MFGOBJ.C_22_Sal.EClassComProposal.EClassTermAndCondition> lstterm = (List<MFGOBJ.C_22_Sal.EClassComProposal.EClassTermAndCondition>)ViewState["tblterm"];

            //var lstcust = (List<MFGOBJ.C_22_Sal.Sales_BO.DebtorList>)ViewState["tblCust"];

            //var lstcust1 = lstcust.FindAll(p => p.custid == supplier);
            //string CustomarN = lstcust1[0].custname2;
            //string CustomarAdd = lstcust1[0].custaddr;

            //double tAmt = lst.Select(p => p.proamt).Sum();


            //LocalReport rpt1 = new LocalReport();

            //rpt1 = MFGRPTRDLC.RptSetupClass1.GetLocalReport("RD_23_SaM.RptSalesComProposal", lst, lstterm, null);

            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("refno", "Ref: " + refno));
            //rpt1.SetParameters(new ReportParameter("CurDate1", "Date: " + CurDate1));
            //rpt1.SetParameters(new ReportParameter("CustomarN", CustomarN));
            //rpt1.SetParameters(new ReportParameter("To", "TO"));
            //rpt1.SetParameters(new ReportParameter("MDirector", "Managing Director"));
            //rpt1.SetParameters(new ReportParameter("comadd", "Address: " + CustomarAdd));
            //rpt1.SetParameters(new ReportParameter("subject", "Subject: " + this.txtBillNarr.Text));
            //rpt1.SetParameters(new ReportParameter("DearSir", "Dear Sir,"));
            //rpt1.SetParameters(new ReportParameter("TC", "Terms & Condition: "));
            //rpt1.SetParameters(new ReportParameter("WBR", "With best regards."));

            //rpt1.SetParameters(new ReportParameter("Name1", "Iffat Zarin"));
            //rpt1.SetParameters(new ReportParameter("NmeDes1", "Executive Corporate (Co-Ordinator)"));
            //rpt1.SetParameters(new ReportParameter("Name2", "Md. Manul Hasan"));
            //rpt1.SetParameters(new ReportParameter("NmeDes2", "Manager (Corporate-Sales)"));


            //  //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                Session.Remove("tblOrder");
                this.ddlProjectName.Enabled = true;
                this.ddlMSRSupl.Enabled = true;
                //this.ddlbatchno.Enabled = true;
                //this.lblPrevious.Visible = true;
                //this.txtSrchPrevious.Visible = true;
                this.imgPreVious.Visible = true;
                this.ddlPrevList.Visible = true;
                if (this.Request.QueryString["Type"] == "Entry")
                {
                    this.ddlPrevList.Items.Clear();
                }

                this.lblCurNo1.Text = "GRM" + DateTime.Today.ToString("MM") + "-";
                this.txtCurDate.Enabled = true;
                this.txtRefno.Text = "";
                
                this.txtBillNarr.Text = "";
                this.gvOrdernfo.DataSource = null;
                this.gvOrdernfo.DataBind();
                this.gvCost.DataSource = null;
                this.gvCost.DataBind();
                this.ddlbatchno.Enabled = true;
                this.PnlSub.Visible = false;
                this.Panel2.Visible = false;
               
                this.lbtnOk.Text = "Ok";
                return;
            }
            if (this.ddlPrevList.Items.Count == 0)
            {
                if (this.ddlMSRSupl.SelectedValue == "000000000000")
                {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Buyer');", true);


                    return;
                }
            }
            this.ddlProjectName.Enabled = false;
            this.ddlMSRSupl.Enabled = false;
            this.ddlbatchno.Enabled = false;
            //this.lblPrevious.Visible = false;
            //this.txtSrchPrevious.Visible = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comnam = hst["comnam"].ToString();
            this.imgPreVious.Visible = false;
            this.ddlPrevList.Visible = false;
            this.txtCurNo2.ReadOnly = true;
            this.PnlSub.Visible = true;
            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_Info();

        }






        protected void Data_Bind()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction> lst = (List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction>)ViewState["tblOrder"];
            if (hst["usrdesig"].ToString() != "Administrator")
            {
                this.gvOrdernfo.Columns[6].Visible = false;
                this.gvOrdernfo.Columns[7].Visible = false;
                this.gvOrdernfo.Columns[8].Visible = false;
                this.gvCost.Columns[5].Visible = false;
            }
            if (this.Request.QueryString["Type"] == "EntryRM" || this.Request.QueryString["Type"] == "ApproveRM")
            {
                this.gvOrdernfo.Columns[1].Visible = false;
            }
            this.gvOrdernfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvOrdernfo.DataSource = lst;
            this.gvOrdernfo.DataBind();
            this.FooterCalCulation(lst);
            DataTable dt = (DataTable)ViewState["tblprodcost"];
            if (this.Request.QueryString["Type"] == "Approve")
            {

                this.gvCost.Columns[5].Visible = false;
            }
            this.gvCost.DataSource = dt;
            this.gvCost.DataBind();
            FooterCal_Costing();
        }

        private void FooterCalCulation(List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction> lst)
        {

            if (lst.Count == 0)
                return;
            ((Label)this.gvOrdernfo.FooterRow.FindControl("lblgvFooterTqty")).Text = lst.Select(p => p.qty).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvOrdernfo.FooterRow.FindControl("lblgvFooterFCTAmt")).Text = lst.Select(p => p.fcamt).Sum().ToString("#,##0.0000;(#,##0.0000); ");

            ((Label)this.gvOrdernfo.FooterRow.FindControl("lblgvFooterTAmt")).Text = lst.Select(p => p.proamt).Sum().ToString("#,##0.00;(#,##0.00); ");

        }


        protected void GetSALPRONo()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurDate.Text.Trim());
            string mCPRNo = "NEWCPRNO";
            if (this.ddlPrevList.Items.Count > 0)
                mCPRNo = this.ddlPrevList.SelectedValue.ToString();
            if (mCPRNo == "NEWCPRNO")
            {
                DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "GETLASTPRMNO", CurDate1, "", "", "", "", "", "", "", "");
                this.lblCurNo1.Text = ds.Tables[0].Rows[0]["maxsprono1"].ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds.Tables[0].Rows[0]["maxsprono1"].ToString().Substring(6, 5);

                this.lblCurNo1.Text = ds.Tables[0].Rows[0]["maxsprono1"].ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds.Tables[0].Rows[0]["maxsprono1"].ToString().Substring(6, 5);
                this.ddlPrevList.DataTextField = "maxsprono1";
                this.ddlPrevList.DataValueField = "maxsprono";
                this.ddlPrevList.DataSource = ds.Tables[0];
                this.ddlPrevList.DataBind();

            }
        }


        protected void Get_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurDate.Text.Trim());
            string mCPRNo = "NEWCPRNO";
            if (this.ddlPrevList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mCPRNo = this.ddlPrevList.SelectedValue.ToString();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "GET_PRODUCTION_MANUALLY_INFO", mCPRNo, "", "", "", "", "", "", "", "");

            var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction>();
            if (lst == null)
                return;
            ViewState["tblOrder"] = lst;// this.HiddenSameData(lst);
            ViewState["tblprodcost"] = ds1.Tables[2];
            if (mCPRNo == "NEWCPRNO")
            {

                DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "GETLASTPRMNO", CurDate1, "", "", "", "", "", "", "", "");
                this.lblCurNo1.Text = ds.Tables[0].Rows[0]["maxsprono1"].ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds.Tables[0].Rows[0]["maxsprono1"].ToString().Substring(6, 5);
                return;
            }


            //  this.codDis.Visible = true;
            this.txtRefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["mgrrno1"].ToString().Substring(0, 6);
            this.txtCurNo2.Text = ds1.Tables[1].Rows[0]["mgrrno1"].ToString().Substring(6, 5);
            this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["mgrrdate"]).ToString("dd.MM.yyyy");
            this.ddlProjectName.SelectedValue = ds1.Tables[1].Rows[0]["storid"].ToString();
            ////this.GetCustomer();      
            this.txtBillNarr.Text = ds1.Tables[1].Rows[0]["remarks"].ToString();
            this.ddlMSRSupl.SelectedValue = ds1.Tables[1].Rows[0]["ssircode"].ToString();
            DataView dv = ds1.Tables[0].DefaultView;
            DataTable tempdt = dv.ToTable(true, "mlcdesc", "mlccod");
            this.ddlbatchno.DataTextField = "mlcdesc";
            this.ddlbatchno.DataValueField = "mlccod";
            this.ddlbatchno.DataSource = tempdt;
            this.ddlbatchno.DataBind();
            this.ddlbatchno.SelectedValue = ds1.Tables[0].Rows[0]["mlccod"].ToString();
            this.ddlbatchno_SelectedIndexChanged(null, null);
            this.Data_Bind();
            //   this.FooterCal();


        }




        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.gvOrdernfo.PageIndex = ((DropDownList)this.gvOrdernfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.Data_Bind();
        }




        protected void SaveValue()
        {

            List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction> lst = (List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction>)ViewState["tblOrder"];

            int index;
            for (int j = 0; j < this.gvOrdernfo.Rows.Count; j++)
            {
                double Qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvOrdernfo.Rows[j].FindControl("txtgvQty")).Text.Trim()));
                double Rate = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvOrdernfo.Rows[j].FindControl("txtgvRate")).Text.Trim()));
                double exrate = Convert.ToDouble(ASTUtility.ExprToValue("0" + this.lblConRate.Text.Trim()));
                string loction = ((DropDownList)this.gvOrdernfo.Rows[j].FindControl("ddlval")).SelectedValue.ToString();

                double Amt = Qty * Rate;
                double proamt = Qty * Rate * exrate;
                index = (this.gvOrdernfo.PageIndex) * this.gvOrdernfo.PageSize + j;
                lst[index].qty = Qty;
                lst[index].prorate = Rate;
                lst[index].proamt = proamt;
                lst[index].fcamt = Amt;
                lst[index].location = loction;
            }
            ViewState["tblOrder"] = lst;
        }
        protected void SaveValue_Material()
        {

            DataTable matlist = (DataTable)ViewState["tblprodcost"];

            //int index;
            for (int j = 0; j < this.gvCost.Rows.Count; j++)
            {
                double actQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvCost.Rows[j].FindControl("txtgvitmqty")).Text.Trim()));
                //index = (this.gvCost.PageIndex) * this.gvCost.PageSize + j;
                matlist.Rows[j]["itmqty"] = actQty;
                matlist.Rows[j]["itmamt"] = Convert.ToDouble(matlist.Rows[j]["conrate"]) * actQty;


            }
            ViewState["tblprodcost"] = matlist;
        }


        //private List<MFGOBJ.C_13_ProdMon.BO_Production.EClassSalProInfo> HiddenSameData(List<MFGOBJ.C_13_ProdMon.BO_Production.EClassSalProInfo> lst)
        //{
        //    if (lst.Count == 0)
        //        return lst;

        //    int i = 0;
        //    string procode = lst[0].procode;

        //    //  List<MFGOBJ.C_22_Sal.EClassComProposal.EClassSalProInfo> lst2 = new List<MFGOBJ.C_22_Sal.EClassComProposal.EClassSalProInfo>();
        //    foreach (MFGOBJ.C_13_ProdMon.BO_Production.EClassSalProInfo lst1 in lst)
        //    {



        //        if (i == 0)
        //        {
        //            i++;
        //            continue;
        //        }
        //        else if (lst1.procode == procode)
        //        {
        //            lst[i].prodesc = "";


        //        }
        //        else
        //        {

        //            if (lst1.procode == procode)
        //                lst[i].prodesc = "";


        //        }


        //        procode = lst1.procode;

        //        i++;



        //    }
        //    return lst;

        //}
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            if (this.Request.QueryString["Type"] == "EntryRM" || this.Request.QueryString["Type"] == "ApproveRM")
            {
                UpdateMaterialIssue();
            }
            else
            {
                UpateManualProduction();
            }



        }

        public void UpateManualProduction()
        {       
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);
               
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = this.GetCompCode();
            string posteddat = DateTime.Today.ToString("dd-MMM-yyyy");

            string editbyid = (this.Request.QueryString["Type"] == "Entry") ? "" : usrid;
            string edittrmid = (this.Request.QueryString["Type"] == "Entry") ? "" : trmid;
            string editseson = (this.Request.QueryString["Type"] == "Entry") ? "" : sessionid;
            string editdat = (this.Request.QueryString["Type"] == "Entry") ? "" : posteddat;
           
            string adjstmnt = (this.Request.QueryString["Type"] == "FGAdjst") ? "true" : "false";
            this.SaveValue();
            List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction> lst = (List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction>)ViewState["tblOrder"];
            if (lst == null)
            {
                return;
            }
            if (this.ddlPrevList.Items.Count == 0)
                this.GetSALPRONo();
            string mORDNO = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(6, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string mORDDAT = this.GetStdDate(this.txtCurDate.Text.Trim());
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string buyerid = this.ddlMSRSupl.SelectedValue.ToString();
            string refno = this.txtRefno.Text.Trim();
            string mNAR = this.txtBillNarr.Text.Trim();
            string mlccod = this.ddlbatchno.SelectedValue.ToString();
            string Curcode = this.ddlCurrency.SelectedValue.ToString();
            string conRate = this.lblConRate.Text.ToString();
            //double OvDis = Convert.ToDouble("0" + this.lblCodAmt.Text.Trim());
            //string TchDesc = this.txtTchDesc.Text;
            //string LETDES = this.txtLETDES.Text;
            //string TopHEad = this.txtTopHEad.Text;
            
            var checklist = lst.FindAll(p => (p.qty != 0) && (p.fcamt == 0 || p.proamt == 0)).ToList();
            if (checklist.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Order Sheet Not Updated or Rate Blank');", true);

    
                return;
            }


            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "UPDATE_PROD_MANUALLY_INFO", "PRODMB",
                             mORDNO, mORDDAT, pactcode, refno, mNAR, usrid, sessionid, trmid, posteddat, buyerid,
                             editbyid, edittrmid, editseson, editdat, Curcode, conRate, adjstmnt);
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ purData.ErrorObject["Msg"].ToString() + "');", true);

               
                return;
            }
            foreach (SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction c1 in lst)
            {
                string styleid = c1.styleid;
                string dayid = c1.dayid;
                string colorid = c1.colorid;
                string sizeid = c1.sizeid;
                string batchcode = c1.mlccod;
                string qty = (c1.qty).ToString();
                string orderamt = (c1.proamt).ToString();
                string fcamt = (c1.fcamt).ToString();
                string location = c1.location;
                if(c1.qty!= 0)
                {                                   
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "UPDATE_PROD_MANUALLY_INFO", "PRODMA", mORDNO, styleid, colorid, sizeid, batchcode, qty, orderamt, dayid, fcamt, location, "", "", "", "", "");
                }
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ purData.ErrorObject["Msg"].ToString() + "');", true);

                  
                    return;
                }

            }

            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "UPDATE_PROD_MANUALLY_INFO", "PRODMC",
                            mORDNO, mlccod, mORDDAT, "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + purData.ErrorObject["Msg"].ToString() + "');", true);

               
                return;
            }
            this.Get_Info();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Success');", true);

           
        }

        public void UpdateMaterialIssue()
        {
          
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

              
                return;
            }
            this.SaveValue_Material();

            DataTable matlist = (DataTable)ViewState["tblprodcost"];
            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(matlist);
            ds.Tables[0].TableName = "matlist";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = this.GetCompCode();
            string posteddat = DateTime.Today.ToString("dd-MMM-yyyy");
            if (this.ddlPrevList.Items.Count == 0)
                this.GetSALPRONo();
            string mORDNO = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(6, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string mlccod = this.ddlbatchno.SelectedValue.ToString();
            string mORDDAT = this.GetStdDate(this.txtCurDate.Text.Trim());

            bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "UPDATE_MANUAL_PRODUCTION_MATERIAL_ISSUE", ds, null, null,
                           mORDNO, mlccod, mORDDAT, usrid, sessionid, trmid, posteddat, "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ purData.ErrorObject["Msg"].ToString() +"')", true);


                return;
            }
            this.Get_Info();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Success')", true);


        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            if (this.Request.QueryString["Type"] == "EntryRM" || this.Request.QueryString["Type"] == "ApproveRM")
            {
                this.SaveValue_Material();
            }
            else
            {
                this.SaveValue();

            }

            this.Data_Bind();
            //his.FooterCal();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }



        protected void gvOrdernfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvOrdernfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvOrdernfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction> lst = (List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction>)ViewState["tblOrder"];
            int rowindex = (this.gvOrdernfo.PageSize) * (this.gvOrdernfo.PageIndex) + e.RowIndex;
            string mORDNO = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(6, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string mlccod = lst[rowindex].mlccod.ToString();
            string styleid = lst[rowindex].styleid.ToString();
            string colorid = lst[rowindex].colorid.ToString();
            string sizeid = lst[rowindex].sizeid.ToString();
            string dayid = lst[rowindex].dayid.ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION_MANUALLY", "DELETE_PRODUCTION_MANUALLY", mORDNO, mlccod, styleid, colorid, sizeid, dayid, "");


            lst.RemoveAt(rowindex);

            ViewState["tblOrder"] = lst;
            this.Data_Bind();

        }




        protected void LbtnBatch_Click(object sender, EventArgs e)
        {
            this.GetBatch();
        }

        protected void ddlbatchno_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlbatchno.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataTable masterlc = ((DataTable)ViewState["tblMasterLc"]).Copy();

            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable(true, "styledesc3", "stylecode3");
            this.ddlStyleColor.DataTextField = "styledesc3";
            this.ddlStyleColor.DataValueField = "stylecode3";
            this.ddlStyleColor.DataSource = dt1;
            this.ddlStyleColor.DataBind();
            ddlStyleColor_SelectedIndexChanged(null, null);
            this.ddlProjectName.SelectedValue = "17" + mlccode1.Substring(2, 10);
            DataView dv2;
            dv2 = masterlc.DefaultView;
            dv2.RowFilter = ("mlccod='" + mlccode1 + "'");
            this.ddlMSRSupl.SelectedValue = dv2.ToTable().Rows[0]["buyerid"].ToString();

        }

        protected void lblgArtno_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = GetCompCode();
                ViewState.Remove("tblOrderQty");
                string mMLCCOD = this.ddlbatchno.SelectedValue.ToString();
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string dayid = ((Label)this.gvOrdernfo.Rows[index].FindControl("LblDayid")).Text.ToString();
                string type = (dayid == "00000000") ? "Entry" : "Reorder";
                string styleid = ((Label)this.gvOrdernfo.Rows[index].FindControl("lblgvItmCodc")).Text.ToString();

                string date = (dayid == "00000000") ? "01-Jan-1900" :
                  Convert.ToDateTime(dayid.Substring(4, 2) + "/" + dayid.Substring(6, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ORDER_DETAILS", mMLCCOD, type, date, styleid, "", "", "", ""); ;

                if (ds1 == null)
                    return;
                ViewState["tblsizedesc"] = ds1.Tables[1];
                string mStyleID = "xxxxxxx";
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (ds1.Tables[0].Rows[i]["styleid"].ToString() == mStyleID)
                        ds1.Tables[0].Rows[i]["StyleDesc"] = " >> ";
                    mStyleID = ds1.Tables[0].Rows[i]["styleid"].ToString();
                }


                ViewState["tblOrderQty"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();

                for (int i = 5; i < 20; i++)
                    this.gv1.Columns[i].Visible = false;

                for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
                {

                    int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                    this.gv1.Columns[columid + 4].Visible = true;
                    this.gv1.Columns[columid + 4].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
                }
                this.gv1.EditIndex = -1;
                var list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
                this.gv1.DataSource = list;
                this.gv1.DataBind();
                this.FooterCal();
            }
            catch (Exception ex)
            {

            }
        }
        private void FooterCal()
        {
            var list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
            if (list == null || list.Count == 0)
            {
                return;
            }

        ((Label)this.gv1.FooterRow.FindControl("FLblgvTotal")).Text = ((list.Sum(p => p.totalqty) == 0) ? 0 : list.Sum(p => p.totalqty)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1.FooterRow.FindControl("FLblgvColTotal")).Text = ((list.Sum(p => p.colqty) == 0) ? 0 : list.Sum(p => p.colqty)).ToString("#,##0;(#,##0); ");

            DataTable matlist = (DataTable)ViewState["tblprodcost"];


            ((Label)this.gvCost.FooterRow.FindControl("lblgvttlqty")).Text =
                Convert.ToDouble((Convert.IsDBNull(matlist.Compute("Sum(conqty)", "")) ?
                0.00 : matlist.Compute("Sum(conqty)", ""))).ToString("#,##0.000;(#,##0.000); ");
            ((Label)this.gvCost.FooterRow.FindControl("lblgvitmqty")).Text = Convert.ToDouble((Convert.IsDBNull(matlist.Compute("Sum(itmqty)", "")) ?
                0.00 : matlist.Compute("Sum(itmqty)", ""))).ToString("#,##0.000;(#,##0.000); ");
        }
        private void FooterCal_Costing()
        {
            DataTable matlist = (DataTable)ViewState["tblprodcost"];

            if (matlist == null || matlist.Rows.Count == 0)
                return;
            ((Label)this.gvCost.FooterRow.FindControl("lblgvttlqty")).Text =
                Convert.ToDouble((Convert.IsDBNull(matlist.Compute("Sum(conqty)", "")) ?
                0.00 : matlist.Compute("Sum(conqty)", ""))).ToString("#,##0.000;(#,##0.000); ");
            ((Label)this.gvCost.FooterRow.FindControl("lblgvitmqty")).Text = Convert.ToDouble((Convert.IsDBNull(matlist.Compute("Sum(itmqty)", "")) ?
                0.00 : matlist.Compute("Sum(itmqty)", ""))).ToString("#,##0.000;(#,##0.000); ");
        }

        protected void ddlStyleColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccod = this.ddlbatchno.SelectedValue.ToString();
            string mlcdesc = this.ddlbatchno.SelectedItem.ToString();
            string stylecolor = this.ddlStyleColor.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccod + "' and stylecode3='" + stylecolor + "'");

            if (dv1.ToTable() != null)
            {
                this.ddlCurrency.SelectedValue = dv1.ToTable().Rows[0]["curcode"].ToString();
                this.lblConRate.Text = Convert.ToDouble("0" + dv1.ToTable().Rows[0]["exrate"]).ToString("#,##0.0000;");
            }
        }

        protected void lnkAddResouctCost_Click(object sender, EventArgs e)
        {
            DataTable matlist = (DataTable)ViewState["tblprodcost"];
            string newmaterial = this.ddlResourcesCost.SelectedValue.ToString();

            string mORDNO = this.lblCurNo1.Text.Trim().Substring(0, 3) + this.txtCurDate.Text.Trim().Substring(6, 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string mlccod = this.ddlbatchno.SelectedValue.ToString();

            DataRow[] dr = matlist.Select("itmcode='" + newmaterial.Substring(0, 12) + "' and spcfcod='" + newmaterial.Substring(12, 12) + "'");
            if (dr.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: Selected Component Already Added');", true);



                return;
            }
            else
            {
                string rsirdesc = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + newmaterial.Substring(0, 12) + "' and spcfcod='" + newmaterial.Substring(12, 12) + "'"))[0]["sirdesc2"].ToString();
                string runit = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + newmaterial.Substring(0, 12) + "' and spcfcod='" + newmaterial.Substring(12, 12) + "'"))[0]["sirunit"].ToString();
                //string color = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["desc2"].ToString();

                string spcfdesc = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + newmaterial.Substring(0, 12) + "' and spcfcod='" + newmaterial.Substring(12, 12) + "'"))[0]["spcfdesc"].ToString();

                DataRow dr1 = matlist.NewRow();
                dr1["comcod"] = this.GetCompCode();
                dr1["mgrrno"] = mORDNO;
                dr1["mlccod"] = mlccod;
                dr1["itmcode"] = newmaterial.Substring(0, 12);
                dr1["itmdesc"] = rsirdesc;
                dr1["itmunit"] = runit;
                dr1["spcfcod"] = newmaterial.Substring(12, 12);
                dr1["conqty"] = 0;
                dr1["conamt"] = 0;
                dr1["itmqty"] = 0;
                dr1["itmamt"] = 0;
                dr1["conrate"] = 0;
                dr1["status"] = "NOTUPDATED";
                matlist.Rows.Add(dr1);
            }
            ViewState["tblprodcost"] = matlist;
            this.Data_Bind();
        }
        protected void Load_Location()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMATLOCATION", "", "%", "", "", "", "", "", "", "");

            ViewState["tblLocation"] = ds1.Tables[0];
            if (ds1 == null)
                return;

        }
        protected void gvOrdernfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tblLocation"];

            if (e.Row.RowType == DataControlRowType.Header)
            {
               
                DropDownList ddlLocHead = (DropDownList)e.Row.FindControl("ddlLocHead");
                ddlLocHead.DataTextField = "gdesc";
                ddlLocHead.DataValueField = "gcod";
                ddlLocHead.DataSource = dt;
                ddlLocHead.DataBind();
                ddlLocHead.SelectedValue = "00000";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllocation = (DropDownList)e.Row.FindControl("ddlval");
                string location = DataBinder.Eval(e.Row.DataItem, "location").ToString();
                ddllocation.DataTextField = "gdesc";
                ddllocation.DataValueField = "gcod";
                ddllocation.DataSource = dt;
                ddllocation.DataBind();
                ddllocation.SelectedValue = location;
            }
            }

        protected void ddlLocHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction> lst = (List<SPEENTITY.C_15_Pro.BO_Production.EclassManualProduction>)ViewState["tblOrder"];

           
            DropDownList ddlloca = (DropDownList)sender;
            string loccode = ddlloca.SelectedValue;

            foreach (var item  in lst)
            {
                item.location = loccode;
            }

            ViewState["tblOrder"] = lst;
            this.Data_Bind();
        }
    }

}