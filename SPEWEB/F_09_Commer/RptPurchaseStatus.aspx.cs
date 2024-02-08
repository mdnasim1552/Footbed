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
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_09_Commer;

namespace SPEWEB.F_09_Commer
{
    public partial class RptPurchaseStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        static int[] intArray = new int[10];
        double qty1, qty2, qty3, qty4, qty5, qty6, qty7, qty8, qty9, qty10, amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
                if (Request.QueryString.AllKeys.Contains("genno"))
                {
                    if (this.Request.QueryString["genno"] != "")
                    {
                        url = url.Replace("&genno=" + this.Request.QueryString["genno"], ""); // code by safi
                    }
                }

                if (!ASTUtility.PagePermission(url, (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                if (this.ddlProjectName.Items.Count == 0)
                {
                    this.GetProjectName();
                }

                string Type = this.Request.QueryString["Rpt"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "DaywPur") ? "DAY WISE PURCHASE STATUS" : (Type == "PurSum") ? "PURCHASE SUMMARY STATUS" : "PURCHASE STATUS REPORT";

                if (Type == "Purchasetrk")
                {
                    string pono = "";

                    if (Request.QueryString.AllKeys.Contains("genno"))
                    {
                        if (this.Request.QueryString["genno"] != "")
                        {
                            this.txtPOQR_TextChanged(null, null);
                        }
                    }
                }

                this.ShowView();
                this.GetSeason();
                if (Type != "DaywPur")
                {
                    imgbtnFindSupplier_Click(null, null);
                }

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");

            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        private void GetSupplier()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSrchSupplier = "%";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETSUPPLIER", pactcode, txtSrchSupplier, "", "", "", "", "", "", "");
            this.ddlSupplier.DataTextField = "ssirdesc";
            this.ddlSupplier.DataValueField = "ssircode";
            this.ddlSupplier.DataSource = ds2.Tables[0];
            this.ddlSupplier.DataBind();
        }

        private void GetSeason()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");
            if (ds1 == null)
                return;

            ddlSeason.DataTextField = "gdesc";
            ddlSeason.DataValueField = "gcod";
            ddlSeason.DataSource = ds1.Tables[0];
            ddlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.ddlSeason.SelectedValue = season;
            }
            else
            {
                this.ddlSeason.SelectedValue = "00000";
            }

        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            this.GetSupplier();
        }
        private void ShowView()
        {
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                    this.search.Visible = true;
                    this.PnlSupplier.Visible = true;

                    this.MultiView1.ActiveViewIndex = 0;

                    break;

                case "PurSum":
                    this.chkDate.Visible = true;
                    this.lblGroup.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "PenBill":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "IndSup":
                    this.chkDate.Visible = true;
                    this.PnlSupplier.Visible = true;
                    this.GetSupplier();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "Purchasetrk":
                    this.divStoreName.Visible = false;
                    this.ToD.Visible = false;
                    this.lblGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 3;
                    break;


                case "Purchasetrk02":
                    this.ddlProjectName.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.ToD.Visible = false;
                    this.lblGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "BgdBal":
                    this.FromD.Visible = false;
                    this.lblGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.GetMaterial();
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "ProwPur":
                    this.search.Visible = false;
                    this.chkDate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 6;

                    break;

            }
        }


        private void GetReqno01()
        {
            Session.Remove("tblreq");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Mrfno = "%";
            string season = this.ddlSeason.SelectedValue;
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", pactcode, date, Mrfno, season, "", "", "", "", "");
            this.ddlReqNo01.DataTextField = "reqno1";
            this.ddlReqNo01.DataValueField = "reqno";
            this.ddlReqNo01.DataSource = ds1.Tables[0];
            this.ddlReqNo01.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();

        }

        private void GetReqno02()
        {
            Session.Remove("tblreq");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Mrfno = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", pactcode, date, Mrfno, "", "", "", "", "", "");
            this.ddlReqNo02.DataTextField = "reqno1";
            this.ddlReqNo02.DataValueField = "reqno";
            this.ddlReqNo02.DataSource = ds1.Tables[0];
            this.ddlReqNo02.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();

        }

        private void GetMaterial()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtfindMat = "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETMATERIAL", pactcode, txtfindMat, "", "", "", "", "", "", "");
            this.ddlMaterial.DataTextField = "rsirdesc";
            this.ddlMaterial.DataValueField = "rsircode";
            this.ddlMaterial.DataSource = ds1.Tables[0];
            this.ddlMaterial.DataBind();

        }

        protected void imgbtnFindMat_Click(object sender, EventArgs e)
        {
            this.GetMaterial();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                    this.RptDayPurchase();
                    break;

                case "PurSum":
                    this.RptPurchaseSum();
                    break;

                case "PenBill":
                    break;

                case "IndSup":
                    this.RptIndSup();
                    break;
                case "Purchasetrk":
                    this.RptPurchaseTrack();
                    break;

                case "BgdBal":
                    this.RptBgdBal();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Print Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        private void RptDayPurchase()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");

            //List<RptPurchaseSummary> list = dt.DataTableToList<RptPurchaseSummary>();
            //LocalReport rpt = new LocalReport();
            //rpt = RptSetupClass.GetLocalReport("R_09_Commer.RptPurchaseSummary", list, null, null);

            //ReportDocument rptpur = new RMGiRPT.R_09_Commer.RptPurchaseStatus1();
            //TextObject rptCname = rptpur.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtFDate1 = rptpur.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;
            //TextObject txtTitle = rptpur.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "PURCHASE REPORT";
            //TextObject txtuserinfo = rptpur.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptpur.SetDataSource(dt);

            var rptlist = dt.DataTableToList<SPEENTITY.C_09_Commer.RptDayWisPrchse>();

            LocalReport Rpt1a = new LocalReport();

            Rpt1a = SPERDLC.RptSetupClass.GetLocalReport("R_09_Commer.RptDayWisPrchse", rptlist, null, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("comlogo", ComLogo));
            Rpt1a.SetParameters(new ReportParameter("rpttitle", "Incoming Material Receive Report Summary"));
            Rpt1a.SetParameters(new ReportParameter("daterange", "From " + fromdate + " To " + todate));
            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptPurchaseSum()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");




            ReportDocument rptpur = new RMGiRPT.R_09_Commer.RptPurchaseSummary();
            TextObject rptCname = rptpur.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtFDate1 = rptpur.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "From " + fromdate + " To " + todate;
            TextObject txtgroup = rptpur.ReportDefinition.ReportObjects["txtgroup"] as TextObject;
            txtgroup.Text = "Group: " + this.ddlRptGroup.SelectedItem.Text;
            TextObject txtuserinfo = rptpur.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptpur.SetDataSource(dt);
            Session["Report1"] = rptpur;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptIndSup()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string compname = hst["compname"].ToString();
            string compadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMM, yyyy");
            string store = ddlProjectName.SelectedItem.Text.Substring(ddlProjectName.SelectedItem.Text.IndexOf("-") + 1);
            string supplier = ddlSupplier.SelectedItem.Text;
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMM, yyyy");

            LocalReport rpt1 = new LocalReport();
            var list = dt.DataTableToList<SPEENTITY.C_09_Commer.IndSupPurchase>();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptIndSupPurchase", list, null, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("fromdate", fromdate));
            rpt1.SetParameters(new ReportParameter("todate", todate));
            rpt1.SetParameters(new ReportParameter("store", store));
            rpt1.SetParameters(new ReportParameter("supplier", supplier));
            rpt1.SetParameters(new ReportParameter("rptTitle", "PURCHASE STATUS REPORT"));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptPurchaseTrack()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string reqno = this.ddlReqNo01.SelectedValue.ToString();

            ReportDocument rptpur = new RMGiRPT.R_09_Commer.RptPurchaseTra();
            TextObject rptCname = rptpur.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtProjectName = rptpur.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            txtProjectName.Text = (((DataTable)Session["tblreq"]).Select("reqno='" + reqno + "'"))[0]["actdesc"].ToString();
            ////TextObject txtreqno = rptpur.ReportDefinition.ReportObjects["txtreqno"] as TextObject;
            ////txtreqno.Text = "Req. No: " + ASTUtility.Left(this.ddlReqNo01.SelectedItem.Text.Trim(), 11);

            TextObject rpttxtMRFno = rptpur.ReportDefinition.ReportObjects["txtMRFno"] as TextObject;
            rpttxtMRFno.Text = "MRF No: " + (((DataTable)Session["tblreq"]).Select("reqno='" + reqno + "'"))[0]["mrfno"].ToString();

            //TextObject txtFDate1 = rptpur.ReportDefinition.ReportObjects["txtreqdate"] as TextObject;
            //txtFDate1.Text = "Req. Date: " + this.ddlReqNo01.SelectedItem.Text.Substring(13, 11);
            TextObject txtuserinfo = rptpur.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptpur.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptpur.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptpur;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptBgdBal()
        {

            DataTable dt = (DataTable)Session["tblpurchase"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptpur = new RMGiRPT.R_09_Commer.RptMaterialBudgetBal();
            //TextObject rptCname = rptpur.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtProjectName = rptpur.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
            TextObject txtMaterial = rptpur.ReportDefinition.ReportObjects["txtMaterial"] as TextObject;
            txtMaterial.Text = this.ddlMaterial.SelectedItem.Text.Trim();

            TextObject txtOpening = rptpur.ReportDefinition.ReportObjects["txtOpening"] as TextObject;
            txtOpening.Text = this.lblvalOpenig.Text.Trim();
            TextObject txttransfer = rptpur.ReportDefinition.ReportObjects["txttransfer"] as TextObject;
            txttransfer.Text = this.lblvaltrans.Text.Trim();

            TextObject txtBudget = rptpur.ReportDefinition.ReportObjects["txtBudget"] as TextObject;
            txtBudget.Text = this.lblvalBudget.Text.Trim();

            TextObject txttotalSupplyQty = rptpur.ReportDefinition.ReportObjects["txttotalSupplyQty"] as TextObject;
            txttotalSupplyQty.Text = this.lblvalTotalSupp.Text.Trim();

            TextObject txtBgdBalance = rptpur.ReportDefinition.ReportObjects["txtBgdBalance"] as TextObject;
            txtBgdBalance.Text = this.lblvalBalance.Text.Trim();
            TextObject txtuserinfo = rptpur.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptpur.SetDataSource(dt);
            Session["Report1"] = rptpur;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowValue();
        }

        private void ShowValue()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ShowDayPur();
                    break;

                case "PurSum":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ShowPurSum();
                    break;

                case "PenBill":
                    break;

                case "IndSup":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.search.Visible = true;
                    this.ShowIndSupplier();
                    break;

                case "Purchasetrk":
                    //this.ShowPurChaseTrk();
                    this.ShowPurChaseTrk01();
                    break;

                case "Purchasetrk02":
                    this.ShowPurChaseTrk02();
                    break;

                case "BgdBal":
                    this.Panelbgdbal.Visible = true;
                    this.ShowBgdBal();
                    break;

                case "ProwPur":
                    this.ShowProWisePur();
                    break;
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Show Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        protected void gvPurStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string comcod = this.GetCompCode();
                HyperLink printlink = (HyperLink)e.Row.FindControl("lgvMrrNor");

                string genno = ((Label)e.Row.FindControl("lblMrrGenno")).Text.ToString();
                string actcode = ((Label)e.Row.FindControl("lblMrractcode")).Text.ToString();
                string reqtype = ASTUtility.Left(genno, 3);

                if (reqtype != "LRC")
                {
                    printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=MRRPrint&comcod=" + comcod + "&mrrno=" + genno + "&ReqType=Local&AppType=YES";
                    printlink.Attributes.Add("class", "text-primary");
                }
                else
                {
                    printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=LCRecPrint&comcod=" + comcod + "&genno=" + genno + "&centrid=&actcode=" + actcode;
                    printlink.Attributes.Add("class", "text-success");

                }

            }
        }

        protected void txtPOQR_TextChanged(object sender, EventArgs e)
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetCompCode();
            string pono = this.txtPOQR.Text.ToString().Trim();
            if (Request.QueryString.AllKeys.Contains("genno"))
            {
                if (this.Request.QueryString["genno"] != "")
                {
                    pono = this.Request.QueryString["genno"].ToString().Trim();
                }
            }
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK01", pono, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk01.DataSource = null;
                this.gvPurstk01.DataBind();

                this.gvPurstk02.DataSource = null;
                this.gvPurstk02.DataBind();

                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            Session["tblpurchaseSum"] = ds1.Tables[1];
            this.LoadGrid();
            this.txtPOQR.Text = String.Empty;
        }

        private void ShowDayPur()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProjectName.SelectedValue == "000000000000") ? "000000000000" : this.ddlProjectName.SelectedValue.ToString();
            string mrfno = this.txtSrcMrfNo.Text.Trim() + "%";
            string sdsd = "";
            string season = this.ddlSeason.SelectedValue == "00000" ? "%%" : this.ddlSeason.SelectedValue + "%";
            string Supplier = this.ddlSupplier.SelectedValue == "000000000000" ? "%%" : this.ddlSupplier.SelectedValue + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQSATIONMRRSTATUS", fromdate, todate, pactcode, mrfno, "%", sdsd, season, Supplier, "");

            if (ds1 == null)
            {
                this.gvPurStatus.DataSource = null;
                this.gvPurStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = dt;
            this.LoadGrid();
        }

        private void ShowPurSum()
        {
            Session.Remove("tblpurchase");

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string uptoDate = (this.chkDate.Checked) ? "OpDate" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURSUMMARY", fromdate, todate, pactcode, mRptGroup, uptoDate, "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurSum.DataSource = null;
                this.gvPurSum.DataBind();
                return;
            }

            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();

        }

        private void ShowIndSupplier()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProjectName.SelectedValue + "%";
            string supplier = this.ddlSupplier.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSupplier.SelectedValue + "%";
            string mrfno = this.txtSrcMrfNo.Text.Trim() + "%";
            string uptoDate = (this.chkDate.Checked) ? "OpDate" : "";
            string season = this.ddlSeason.SelectedValue == "00000" ? "%%" : this.ddlSeason.SelectedValue + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTINDSUPINFO", fromdate, todate, pactcode, supplier, mrfno, uptoDate, season, "", "");

            if (ds1 == null)
            {
                this.gvPurStatus.DataSource = null;
                this.gvPurStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);

            Session["tblpurchase"] = dt;

            this.LoadGrid();

        }

        private void ShowPurChaseTrk01()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetCompCode();
            string reqno = this.ddlReqNo01.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK01", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk01.DataSource = null;
                this.gvPurstk01.DataBind();

                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();
        }

        private void ShowPurChaseTrk02()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetCompCode();
            string reqno = this.ddlReqNo02.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK02", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk.DataSource = null;
                this.gvPurstk.DataBind();
                this.gvPurstk2.DataSource = null;
                this.gvPurstk2.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            Session["tblpurchase1"] = ds1.Tables[1];
            this.LoadGrid();
        }

        private void ShowBgdBal()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string rescode = this.ddlMaterial.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTBUDGETBAL", pactcode, rescode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBgdBal.DataSource = null;
                this.gvBgdBal.DataBind();
                return;
            }

            Session["tblpurchase"] = ds1.Tables[0];

            this.lblvalBudget.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdqty"]).ToString("#,##0;(#,##0); ");

            this.lblvalOpenig.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opqty"]).ToString("#,##0;(#,##0); ");
            this.lblvalRequisition.Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(areqty)", "")) ?
                                         0 : ds1.Tables[0].Compute("sum(areqty)", ""))).ToString("#,##0;(#,##0); ");
            this.lblvaltrans.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["trnqty"]).ToString("#,##0;(#,##0); ");

            this.lblvalTotalSupp.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tosupqty"]).ToString("#,##0;(#,##0); ");
            this.lblvalBalance.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdbal"]).ToString("#,##0;(#,##0); ");
            this.LoadGrid();

        }

        private void ShowProWisePur()
        {

            ViewState.Remove("tblpropurchase");
            string comcod = this.GetCompCode();
            string fdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string tdate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string store = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "RPTPRO_WISE_PUR", fdate, tdate, store, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProPur.DataSource = null;
                this.gvProPur.DataBind();
                return;
            }
            var prolist = ds1.Tables[0].DataTableToList<SPEENTITY.C_10_Procur.EClassProcur.EclassProwisePurchase>();

            ViewState["tblpropurchase"] = prolist;
            ViewState["tblDayhead"] = ds1.Tables[1];
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                case "IndSup":
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string mrrno = dt1.Rows[0]["mrrno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["mrrno"].ToString() == mrrno)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            mrrno = dt1.Rows[j]["mrrno"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["mrrno1"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                            {
                                dt1.Rows[j]["pactdesc"] = "";
                            }

                            if (dt1.Rows[j]["mrrno"].ToString() == mrrno)
                            {
                                dt1.Rows[j]["mrrno1"] = "";
                            }
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            mrrno = dt1.Rows[j]["mrrno"].ToString();

                        }

                    }

                    break;

                case "PurSum":
                    break;

                case "PenBill":
                    break;


                case "Purchasetrk":
                    //string ppactcode = dt1.Rows[0]["pactcode"].ToString();
                    //string matcode = dt1.Rows[0]["rsircode"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["pactcode"].ToString() == ppactcode && dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //    {
                    //        ppactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        matcode = dt1.Rows[j]["rsircode"].ToString();
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";
                    //        dt1.Rows[j]["areqty"] = 0.0000000;
                    //    }

                    //    else
                    //    {
                    //        ppactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        matcode = dt1.Rows[j]["rsircode"].ToString();
                    //    }

                    //}

                    string grp = dt1.Rows[0]["grp"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }

                        else
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                        }

                    }


                    break;


                case "Purchasetrk02":
                    string ppactcode = dt1.Rows[0]["pactcode"].ToString();
                    string matcode = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == ppactcode && dt1.Rows[j]["rsircode"].ToString() == matcode)
                        {
                            ppactcode = dt1.Rows[j]["pactcode"].ToString();
                            matcode = dt1.Rows[j]["rsircode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["rsirdesc"] = "";
                            dt1.Rows[j]["rsirunit"] = "";
                            dt1.Rows[j]["spcfdesc"] = "";
                            dt1.Rows[j]["areqty"] = 0.0000000;
                        }

                        else
                        {
                            ppactcode = dt1.Rows[j]["pactcode"].ToString();
                            matcode = dt1.Rows[j]["rsircode"].ToString();
                        }
                    }

                    break;
            }


            return dt1;

        }


        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];

            if (dt != null)
            {

                if (dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                    //return;
                }

                string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
                switch (rpt)
                {
                    case "DaywPur":
                    case "IndSup":
                        this.gvPurStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPurStatus.DataSource = dt;
                        this.gvPurStatus.DataBind();
                        if (dt.Rows.Count > 0)
                        {
                            ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ?
                                             0 : dt.Compute("sum(qty)", ""))).ToString("#,##0;(#,##0); ");

                            ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                             0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        }
                        break;

                    case "PurSum":
                        this.gvPurSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPurSum.DataSource = dt;
                        this.gvPurSum.DataBind();
                        if (dt.Rows.Count > 0)
                        {
                            ((Label)this.gvPurSum.FooterRow.FindControl("lgvFAmtS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                             0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        }
                        break;

                    case "PenBill":
                        break;

                    case "Purchasetrk":
                        this.gvPurstk01.DataSource = dt;
                        this.gvPurstk01.DataBind();

                        DataTable dt2 = (DataTable)Session["tblpurchaseSum"];
                        this.gvPurstk02.DataSource = dt2;
                        this.gvPurstk02.DataBind();
                        break;

                    case "Purchasetrk02":
                        DataTable dt1 = (DataTable)Session["tblpurchase1"];
                        this.gvPurstk.DataSource = dt;
                        this.gvPurstk.DataBind();

                        this.gvPurstk2.DataSource = dt1;
                        this.gvPurstk2.DataBind();

                        break;


                    case "BgdBal":
                        this.gvBgdBal.DataSource = dt;
                        this.gvBgdBal.DataBind();

                        if (dt.Rows.Count > 0)
                        {
                            ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFareqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(areqty)", "")) ?
                                                0 : dt.Compute("sum(areqty)", ""))).ToString("#,##0;(#,##0); ");
                            ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFprogqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(progqty)", "")) ?
                                                0 : dt.Compute("sum(progqty)", ""))).ToString("#,##0;(#,##0); ");
                            ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFordrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ?
                                                0 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                            ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFmrrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrrqty)", "")) ?
                                                0 : dt.Compute("sum(mrrqty)", ""))).ToString("#,##0;(#,##0); ");
                        }

                        break;

                }

            }
        }

        private void Data_Bind()
        {
            var prolist = (List<SPEENTITY.C_10_Procur.EClassProcur.EclassProwisePurchase>)ViewState["tblpropurchase"];
            if (prolist.Count == 0)
            {
                this.gvProPur.DataSource = null;
                this.gvProPur.DataBind();
                return;
            }


            qty1 = (prolist.Select(p => p.q1).Sum() == 0.00) ? 0.00 : prolist.Select(p => p.q1).Sum();
            qty2 = (prolist.Select(p => p.q2).Sum() == 0.00) ? 0.00 : prolist.Select(p => p.q2).Sum();
            qty3 = (prolist.Select(p => p.q3).Sum() == 0.00) ? 0.00 : prolist.Select(p => p.q3).Sum();
            qty4 = (prolist.Select(p => p.q4).Sum() == 0.00) ? 0.00 : prolist.Select(p => p.q4).Sum();
            qty5 = (prolist.Select(p => p.q5).Sum() == 0.00) ? 0.00 : prolist.Select(p => p.q5).Sum();
            qty6 = (prolist.Select(p => p.q6).Sum() == 0.00) ? 0.00 : prolist.Select(p => p.q6).Sum();
            qty7 = (prolist.Select(p => p.q7).Sum() == 0.00) ? 0.00 : prolist.Select(p => p.q7).Sum();
            qty8 = (prolist.Select(p => p.q8).Sum() == 0.00) ? 0.00 : prolist.Select(p => p.q8).Sum();
            qty9 = (prolist.Select(p => p.q9).Sum() == 0.00) ? 0.00 : prolist.Select(p => p.q9).Sum();
            qty10 = (prolist.Select(p => p.q10).Sum() == 0.00) ? 0.00 : prolist.Select(p => p.q10).Sum();


            intArray[0] = Convert.ToInt32(qty1);
            intArray[1] = Convert.ToInt32(qty2);
            intArray[2] = Convert.ToInt32(qty3);
            intArray[3] = Convert.ToInt32(qty4);
            intArray[4] = Convert.ToInt32(qty5);
            intArray[5] = Convert.ToInt32(qty6);
            intArray[6] = Convert.ToInt32(qty7);
            intArray[7] = Convert.ToInt32(qty8);
            intArray[8] = Convert.ToInt32(qty9);
            intArray[9] = Convert.ToInt32(qty10);


            this.gvProPur.Columns[4].Visible = (qty1 != 0);
            this.gvProPur.Columns[5].Visible = (qty1 != 0);
            this.gvProPur.Columns[6].Visible = (qty1 != 0);
            this.gvProPur.Columns[7].Visible = (qty1 != 0);

            this.gvProPur.Columns[8].Visible = (qty2 != 0);
            this.gvProPur.Columns[9].Visible = (qty2 != 0);
            this.gvProPur.Columns[10].Visible = (qty2 != 0);
            this.gvProPur.Columns[11].Visible = (qty2 != 0);

            this.gvProPur.Columns[12].Visible = (qty3 != 0);
            this.gvProPur.Columns[13].Visible = (qty3 != 0);
            this.gvProPur.Columns[14].Visible = (qty3 != 0);
            this.gvProPur.Columns[15].Visible = (qty3 != 0);

            this.gvProPur.Columns[16].Visible = (qty4 != 0);
            this.gvProPur.Columns[17].Visible = (qty4 != 0);
            this.gvProPur.Columns[18].Visible = (qty4 != 0);
            this.gvProPur.Columns[19].Visible = (qty4 != 0);

            this.gvProPur.Columns[20].Visible = (qty5 != 0);
            this.gvProPur.Columns[21].Visible = (qty5 != 0);
            this.gvProPur.Columns[22].Visible = (qty5 != 0);
            this.gvProPur.Columns[23].Visible = (qty5 != 0);

            this.gvProPur.Columns[24].Visible = (qty6 != 0);
            this.gvProPur.Columns[25].Visible = (qty6 != 0);
            this.gvProPur.Columns[26].Visible = (qty6 != 0);
            this.gvProPur.Columns[27].Visible = (qty6 != 0);

            this.gvProPur.Columns[28].Visible = (qty7 != 0);
            this.gvProPur.Columns[29].Visible = (qty7 != 0);
            this.gvProPur.Columns[30].Visible = (qty7 != 0);
            this.gvProPur.Columns[31].Visible = (qty7 != 0);

            this.gvProPur.Columns[32].Visible = (qty8 != 0);
            this.gvProPur.Columns[33].Visible = (qty8 != 0);
            this.gvProPur.Columns[34].Visible = (qty8 != 0);
            this.gvProPur.Columns[35].Visible = (qty8 != 0);

            this.gvProPur.Columns[36].Visible = (qty9 != 0);
            this.gvProPur.Columns[37].Visible = (qty9 != 0);
            this.gvProPur.Columns[38].Visible = (qty9 != 0);
            this.gvProPur.Columns[39].Visible = (qty9 != 0);

            this.gvProPur.Columns[40].Visible = (qty10 != 0);
            this.gvProPur.Columns[41].Visible = (qty10 != 0);
            this.gvProPur.Columns[42].Visible = (qty10 != 0);
            this.gvProPur.Columns[43].Visible = (qty10 != 0);


            this.gvProPur.DataSource = prolist;
            this.gvProPur.DataBind();
            this.ProWisePurFotterCal();
            Session["Report1"] = gvProPur;
            ((HyperLink)this.gvProPur.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        private void ProWisePurFotterCal()
        {
            var lst = (List<SPEENTITY.C_10_Procur.EClassProcur.EclassProwisePurchase>)ViewState["tblpropurchase"];
            if (lst.Count == 0)
                return;

            ((Label)this.gvProPur.FooterRow.FindControl("Flgvqty01")).Text = (lst.Select(p => p.q1).Sum() == 0.00) ? "0" : lst.Select(p => p.q1).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvqty02")).Text = (lst.Select(p => p.q2).Sum() == 0.00) ? "0" : lst.Select(p => p.q2).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvqty03")).Text = (lst.Select(p => p.q3).Sum() == 0.00) ? "0" : lst.Select(p => p.q3).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvqty04")).Text = (lst.Select(p => p.q4).Sum() == 0.00) ? "0" : lst.Select(p => p.q4).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvqty05")).Text = (lst.Select(p => p.q5).Sum() == 0.00) ? "0" : lst.Select(p => p.q5).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvqty06")).Text = (lst.Select(p => p.q6).Sum() == 0.00) ? "0" : lst.Select(p => p.q6).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvqty07")).Text = (lst.Select(p => p.q7).Sum() == 0.00) ? "0" : lst.Select(p => p.q7).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvqty08")).Text = (lst.Select(p => p.q8).Sum() == 0.00) ? "0" : lst.Select(p => p.q8).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvqty09")).Text = (lst.Select(p => p.q9).Sum() == 0.00) ? "0" : lst.Select(p => p.q9).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvqty10")).Text = (lst.Select(p => p.q10).Sum() == 0.00) ? "0" : lst.Select(p => p.q10).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvamt01")).Text = (lst.Select(p => p.a1).Sum() == 0.00) ? "0" : lst.Select(p => p.a1).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvamt02")).Text = (lst.Select(p => p.q2).Sum() == 0.00) ? "0" : lst.Select(p => p.a2).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvamt03")).Text = (lst.Select(p => p.a3).Sum() == 0.00) ? "0" : lst.Select(p => p.a3).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvamt04")).Text = (lst.Select(p => p.a4).Sum() == 0.00) ? "0" : lst.Select(p => p.a4).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvamt05")).Text = (lst.Select(p => p.a5).Sum() == 0.00) ? "0" : lst.Select(p => p.a5).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvamt06")).Text = (lst.Select(p => p.a6).Sum() == 0.00) ? "0" : lst.Select(p => p.a6).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvamt07")).Text = (lst.Select(p => p.a7).Sum() == 0.00) ? "0" : lst.Select(p => p.a7).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvamt08")).Text = (lst.Select(p => p.a8).Sum() == 0.00) ? "0" : lst.Select(p => p.a8).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvamt09")).Text = (lst.Select(p => p.a9).Sum() == 0.00) ? "0" : lst.Select(p => p.a9).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvamt10")).Text = (lst.Select(p => p.a10).Sum() == 0.00) ? "0" : lst.Select(p => p.a10).Sum().ToString("#,##0;(#,##0); ");

            ((Label)this.gvProPur.FooterRow.FindControl("Flgvtqty")).Text = (lst.Select(p => p.tqty).Sum() == 0.00) ? "0" : lst.Select(p => p.tqty).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvProPur.FooterRow.FindControl("Flgvtamt")).Text = (lst.Select(p => p.tamt).Sum() == 0.00) ? "0" : lst.Select(p => p.tamt).Sum().ToString("#,##0;(#,##0); ");

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void gvPurStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurStatus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void gvPurSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurSum.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void imgbtnFindReqno01_Click(object sender, EventArgs e)
        {
            this.GetReqno01();
        }

        protected void imgbtnFindReqno02_Click(object sender, EventArgs e)
        {
            this.GetReqno02();
        }

        protected void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked)
            {
                this.FromD.Visible = false;
            }
            else
            {
                this.FromD.Visible = true;
            }
        }

        protected void gvProPur_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell0 = new TableCell();
                cell0.Text = "NB: Data Shown on this Table Maximum 10 Days";
                cell0.ForeColor = System.Drawing.Color.Red;
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 4;
                gvrow.Cells.Add(cell0);
                DataTable dt2 = (DataTable)ViewState["tblDayhead"];
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (i > 9)
                        break;
                    if (intArray[i] != 0)
                    {
                        TableCell cell = new TableCell();
                        cell.Text = dt2.Rows[i]["mrrdat"].ToString();
                        cell.HorizontalAlign = HorizontalAlign.Center;
                        cell.ColumnSpan = 4;
                        cell.Font.Bold = true;
                        gvrow.Cells.Add(cell);
                    }
                }
                //this.gvHourlyProd.Columns[5 + i].HeaderText = dt2.Rows[i]["gdesc"].ToString();
                //  i++;
                TableCell cell00 = new TableCell();
                cell00.Text = "Total Purchase (Monthly)";
                cell00.HorizontalAlign = HorizontalAlign.Center;
                cell00.ColumnSpan = 3;
                gvrow.Cells.Add(cell00);

                gvProPur.Controls[0].Controls.AddAt(0, gvrow);
            }
        }

        protected void gvPurstk01_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string reqno = this.ddlReqNo01.SelectedValue.ToString();
                string comcod = this.GetCompCode();
                HyperLink printlink = (HyperLink)e.Row.FindControl("HypLinkPrint");
                string grp = ((Label)e.Row.FindControl("lblGrp")).Text.ToString();
                string genno = ((Label)e.Row.FindControl("lblGenno")).Text.ToString().Trim();
                string actcode = ((Label)e.Row.FindControl("lblactcode")).Text.ToString();
                string ssircode = ((Label)e.Row.FindControl("Labelssircode")).Text.ToString();
                string reqno1 = ((Label)e.Row.FindControl("LabelReqNo")).Text.ToString();
                string msrno = ((Label)e.Row.FindControl("LabelMsrNo")).Text.ToString();

                string reqtype = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqtype")).ToString();
                reqtype = (reqtype != "LC") ? "Local" : "Import";
                switch (grp)
                {
                    case "A":
                    case "C":
                        printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=ReqPrint&comcod=" + comcod + "&reqno=" + reqno + "&ReqType=" + reqtype + "&AppType=YES";
                        break;
                    case "D":
                        printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=SCPrepnation&comcod=" + comcod + "&reqno=" + genno;
                        break;
                    case "E":
                        if (reqtype == "Local")
                        {
                            printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=OrderPrint&comcod=" + comcod + "&orderno=" + genno + "&ReqType=Local&AppType=YES";
                        }
                        else
                        {
                            printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=ImportApp&comcod=" + comcod + "&reqno=" + reqno1 + "&supcode=" + ssircode + "&msrno=" + msrno + "&dayid=" + genno;
                        }
                        break;
                    case "F":
                        if (reqtype == "Local")
                        {
                            printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=MRRPrint&comcod=" + comcod + "&mrrno=" + genno + "&ReqType=Local&AppType=YES";
                        }
                        else
                        {
                            printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=LCRecPrint&comcod=" + comcod + "&genno=" + genno + "&centrid=&actcode=" + actcode;
                        }
                        break;
                    case "G":
                        if (reqtype == "Local")
                        {
                            printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=BillPrint&comcod=" + comcod + "&billno=" + genno + "&ReqType=Local&AppType=YES";
                        }
                        else
                        {
                            printlink.NavigateUrl = "~/F_10_Procur/PuchasePrint.aspx?Type=LCQcPrint&comcod=" + comcod + "&genno=" + genno + "&centrid=" + actcode + "&actcode=";
                        }
                        break;
                }

            }
        }

    }
}