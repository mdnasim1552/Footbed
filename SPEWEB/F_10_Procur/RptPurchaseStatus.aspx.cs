﻿using System;
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

namespace SPEWEB.F_10_Procur
{
    public partial class RptPurchaseStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Type = this.Request.QueryString["Rpt"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "DaywPur") ? "Day Wise Purchase Report" : (Type == "PurSum") ? "Purchase Summary Report"
                    : (Type == "PenBill") ? "Pending Bill Report" : (Type == "IndSup") ? "Purchase History-Supplier Wise Report"
                    : (Type == "Purchasetrk") ? "Purchase Tracking-01 Report" : (Type == "Purchasetrk02") ? "Purchase Tracking-02 Report"
                    : (Type == "PurBilltk") ? "Purchase Bill Tracking"
                    : (Type == "MatRateVar") ? "Materials Rate Variance" : "Budget Tracking Report";

                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                if (this.ddlProjectName.Items.Count == 0)
                {
                    this.GetProjectName();
                }
                this.ShowView();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {

            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();


        }
        private void GetSupplier()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSrchSupplier = this.txtSrcSupplier.Text.Trim() + "%";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETSUPPLIER", pactcode, txtSrchSupplier, "", "", "", "", "", "", "");
            this.ddlSupplier.DataTextField = "ssirdesc";
            this.ddlSupplier.DataValueField = "ssircode";
            this.ddlSupplier.DataSource = ds2.Tables[0];
            this.ddlSupplier.DataBind();

        }

        private void GetMaterialCode()
        {
            string comcod = this.GetComeCode();

            //string pactcode = this.ddlProjectName.SelectedValue.ToString();
            // string txtSrchSupplier = this.txtSrcSupplier.Text.Trim() + "%";
            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETRESOURCE", "%%", "", "", "", "", "", "", "", "");
            this.ddlMatCode.DataTextField = "sirdesc";
            this.ddlMatCode.DataValueField = "sircode";
            this.ddlMatCode.DataSource = ds3.Tables[0];
            this.ddlMatCode.DataBind();
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
                    this.GetMaterialCode();
                    this.LblReqno.Visible = true;
                    this.txtSrcMrfNo.Visible = true;
                    this.imgbtnFindRequiSition.Visible = true;
                    this.lblMcod.Visible = true;
                    this.ddlMatCode.Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    this.chkDirect.Visible = true;

                    break;

                case "PurSum":
                    this.lblRptGroup.Visible = true;
                    this.ddlRptGroup.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "PenBill":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "IndSup":
                    this.PnlSupplier.Visible = true;
                    this.GetSupplier();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "Purchasetrk":
                    this.lblProjectName.Visible = false;
                    this.txtSrcProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;

                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 3;
                    break;


                case "Purchasetrk02":
                    this.lblProjectName.Visible = false;
                    this.txtSrcProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;

                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "BgdBal":
                    this.lbldatefrm.Visible = false;
                    this.txtFDate.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;

                    this.GetMaterial();
                    this.MultiView1.ActiveViewIndex = 4;
                    break;

                case "PurBilltk":
                    this.lblProjectName.Visible = false;
                    this.txtSrcProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.lbldatefrm.Visible = false;
                    this.txtFDate.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 6;
                    break;

                case "MatRateVar":
                    this.lblProjectName.Visible = false;
                    this.txtSrcProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    //this.lbldatefrm.Text = "";
                    //this.lbldateto.Text = "Present Date";
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    //this.GetReqno();
                    this.MultiView1.ActiveViewIndex = 7;
                    break;




            }



        }


        protected void imgbtnFindReqno01_Click(object sender, EventArgs e)
        {
            this.GetReqno01();
        }


        private void GetReqno01()
        {
            Session.Remove("tblreq");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Mrfno = "%" + this.txtSrcRequisition01.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", pactcode, date, Mrfno, "", "", "", "", "", "");
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
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string Mrfno = this.txtSrcRequisition02.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETREQNO", pactcode, date, Mrfno, "", "", "", "", "", "");
            this.ddlReqNo02.DataTextField = "reqno1";
            this.ddlReqNo02.DataValueField = "reqno";
            this.ddlReqNo02.DataSource = ds1.Tables[0];
            this.ddlReqNo02.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();

        }

        private void GetBillNo()
        {
            Session.Remove("tblreq");
            string comcod = this.GetComeCode();
            string billno = this.txtBillSearch.Text.Trim();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETBILLNO", billno, "", "", "", "", "", "", "", "");
            this.ddlBillno.DataTextField = "billno1";
            this.ddlBillno.DataValueField = "billno";
            this.ddlBillno.DataSource = ds1.Tables[1];
            this.ddlBillno.DataBind();
            Session["tblreq"] = ds1.Tables[0];
            ds1.Dispose();


        }

        private void GetMaterial()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtfindMat = "%" + this.txtSrcMat.Text.Trim() + "%";
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
                //case "DaywPur":
                //    this.RptDayPurchase();
                //    break;

                //case "PurSum":
                //    this.RptPurchaseSum();
                //    break;

                //case "PenBill":               
                //    break;

                //case "IndSup":
                //    this.RptIndSup();
                //    break;
                case "Purchasetrk":
                    this.RptPurchaseTrack();
                    break;

                    //case "BgdBal":
                    //    this.RptBgdBal();
                    //    break;

                    //case "PurBilltk":
                    //    this.PrintBurBillTrack();
                    //    break;

                    //case "MatRateVar":
                    //    PrintMatRateVariance();
                    //    break;


            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Print Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        //private void RptDayPurchase() 
        //{


        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();

        //    switch (comcod)
        //    {

        //        case "3330":
        //        case "3101":
        //            this.RptDayPurchaseBridge();
        //            break;

        //        default :
        //            this.RptDayPurchaseGen();
        //            break;


        //    }





        //}


        private void RptPurchaseTrack()
        {

            //DataTable dt = (DataTable)Session["tblpurchase"];
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string reqno = this.ddlReqNo01.SelectedValue.ToString();

            //ReportDocument rptpur = new RMGiRPT.R_10_Procur.RptPurchaseTra();
            //TextObject rptCname = rptpur.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtProjectName = rptpur.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = (((DataTable)Session["tblreq"]).Select("reqno='" + reqno + "'"))[0]["actdesc"].ToString();
            ////TextObject txtreqno = rptpur.ReportDefinition.ReportObjects["txtreqno"] as TextObject;
            ////txtreqno.Text = "Req. No: " + ASTUtility.Left(this.ddlReqNo01.SelectedItem.Text.Trim(), 11);

            //TextObject rpttxtMRFno = rptpur.ReportDefinition.ReportObjects["txtMRFno"] as TextObject;
            //rpttxtMRFno.Text = "MRF No: " + (((DataTable)Session["tblreq"]).Select("reqno='" + reqno + "'"))[0]["mrfno"].ToString();

            ////TextObject txtFDate1 = rptpur.ReportDefinition.ReportObjects["txtreqdate"] as TextObject;
            ////txtFDate1.Text = "Req. Date: " + this.ddlReqNo01.SelectedItem.Text.Substring(13, 11);

            //TextObject txtNarration = rptpur.ReportDefinition.ReportObjects["txtNarration"] as TextObject;
            //txtNarration.Text = this.txtReqNarr.Text.Trim();

            //TextObject txtuserinfo = rptpur.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptpur.SetDataSource(dt);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptpur.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptpur;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






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
                    this.lblMcod.Visible = true;
                    this.ddlMatCode.Visible = true;
                    this.LblReqno.Visible = true;
                    this.txtSrcMrfNo.Visible = true;
                    this.imgbtnFindRequiSition.Visible = true;



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
                    this.LblReqno.Visible = true;
                    this.txtSrcMrfNo.Visible = true;
                    this.imgbtnFindRequiSition.Visible = true;
                    this.ShowIndSupplier();
                    break;

                case "Purchasetrk":
                    //this.ShowPurChaseTrk();
                    this.pnlnarration.Visible = true;
                    this.ShowPurChaseTrk01();
                    break;

                case "Purchasetrk02":

                    this.ShowPurChaseTrk02();
                    break;

                case "BgdBal":
                    this.Panelbgdbal.Visible = true;
                    this.ShowBgdBal();
                    break;

                case "PurBilltk":
                    this.ShowPurchaseBill();
                    break;

                case "MatRateVar":
                    this.ShowMatRVariacne();
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

        protected void imgbtnFindRequiSition_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                    this.ShowDayPur();
                    break;



                case "IndSup":
                    this.ShowIndSupplier();
                    break;

            }

        }


        private void ShowDayPur()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string mrfno = this.txtSrcMrfNo.Text.Trim() + "%";
            string rescode = ((this.ddlMatCode.SelectedValue.ToString() == "000000000000") ? "" : this.ddlMatCode.SelectedValue.ToString()) + "%";
            string dirorin = (this.chkDirect.Checked) ? "direct" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQSATIONMRRSTATUS", fromdate, todate, pactcode, mrfno, rescode, dirorin, "", "", "");
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

            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURSUMMARY", fromdate, todate, pactcode, mRptGroup, "", "", "", "", "");
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
            string comcod = this.GetComeCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string supplier = this.ddlSupplier.SelectedValue.ToString();
            string mrfno = this.txtSrcMrfNo.Text.Trim() + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTINDSUPINFO", fromdate, todate, pactcode, supplier, mrfno, "", "", "", "");
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
            string comcod = this.GetComeCode();
            string reqno = this.ddlReqNo01.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURCHASETRACK01", reqno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurstk01.DataSource = null;
                this.gvPurstk01.DataBind();

                return;
            }
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            this.txtReqNarr.Text = ds1.Tables[1].Rows.Count == 0 ? "" : ds1.Tables[1].Rows[0]["reqnar"].ToString();
            ///this.lblshipsupdate.Text = ds1.Tables[0].Rows[0]["shipsupdat"].ToString();
            this.LoadGrid();


        }

        private void ShowPurChaseTrk02()
        {


            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
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


        private void ShowPurchaseBill()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string billno = this.ddlBillno.SelectedValue.ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURBILLTRACK", billno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurBilltk.DataSource = null;
                this.gvPurBilltk.DataBind();

                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();

        }

        private string CompBudgetBalance()
        {
            string comcod = this.GetComeCode();
            string reqorapproved = "";
            switch (comcod)
            {
                case "3315":
                case "3316":
                case "3317":
                    reqorapproved = "req";
                    break;

                default:
                    break;



            }
            return reqorapproved;

        }
        private void ShowBgdBal()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string rescode = this.ddlMaterial.SelectedValue.ToString();






            string reqorapproved = this.CompBudgetBalance();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTBUDGETBAL", pactcode, rescode, reqorapproved, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBgdBal.DataSource = null;
                this.gvBgdBal.DataBind();
                return;
            }

            Session["tblpurchase"] = ds1.Tables[0];

            this.lblvalBudget.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdqty"]).ToString("#,##0;(#,##0); ");

            this.lblvalOpenig.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opqty"]).ToString("#,##0;(#,##0); ");
            this.lbltxtvaldqty.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dqty"]).ToString("#,##0;(#,##0); ");

            this.lblvalRequisition.Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(areqty)", "")) ?
                                         0 : ds1.Tables[0].Compute("sum(areqty)", ""))).ToString("#,##0;(#,##0); ");
            this.lblvaltrans.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["trnqty"]).ToString("#,##0;(#,##0); ");

            this.lblvalTotalSupp.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["tosupqty"]).ToString("#,##0;(#,##0); ");
            this.lblvalBalance.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["bgdbal"]).ToString("#,##0;(#,##0); ");
            this.LoadGrid();

        }

        private void ShowMatRVariacne()
        {


            Session.Remove("tblpurchase");
            string comcod = this.GetComeCode();
            string frmdate = Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string ResCode = ((this.ddlMaterialscom.SelectedValue == "000000000000") ? "" : this.ddlMaterialscom.SelectedValue.ToString()) + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTMATRATEVARIANCE", frmdate, todate, ResCode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurMatRVar.DataSource = null;
                this.gvPurMatRVar.DataBind();

                return;
            }

            Session["tblpurchase"] = this.HiddenSameData(ds1.Tables[0]);
            this.gvPurMatRVar.Columns[4].HeaderText = "Price On <br />" + Convert.ToDateTime(this.txtFDate.Text.Trim()).ToString("dd.MM.yyyy");
            this.gvPurMatRVar.Columns[5].HeaderText = "Price On <br />" + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd.MM.yyyy");
            this.LoadGrid();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Rpt"].ToString().Trim();

            string reqno = "", matcode = "", spcfcod = "";
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

                    string grp = dt1.Rows[0]["grp"].ToString();
                    string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";

                        grp = dt1.Rows[j]["grp"].ToString();

                    }



                    //reqno = dt1.Rows[0]["reqno"].ToString();
                    //matcode = dt1.Rows[0]["rsircode"].ToString();
                    // spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == matcode && dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //    {

                    //        dt1.Rows[j]["reqno1"] = "";
                    //        dt1.Rows[j]["mrfno"] = "";
                    //        dt1.Rows[j]["reqdat"] = "";
                    //        dt1.Rows[j]["shipsupdat"] = "";
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";

                    //    }

                    //    else
                    //    {
                    //        if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    //        {
                    //            dt1.Rows[j]["reqno1"] = "";
                    //            dt1.Rows[j]["mrfno"] = "";
                    //            dt1.Rows[j]["reqdat"] = "";
                    //            dt1.Rows[j]["shipsupdat"] = "";
                    //            dt1.Rows[j]["pactdesc"] = "";
                    //        }
                    //         if (dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //             dt1.Rows[j]["rsirdesc"] = "";
                    //         if (dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //             dt1.Rows[j]["spcfdesc"] = "";





                    //    }


                    //    reqno = dt1.Rows[j]["reqno"].ToString();
                    //    matcode = dt1.Rows[j]["rsircode"].ToString();
                    //    spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //}

                    break;




                case "PurBilltk":
                    //reqno = dt1.Rows[0]["reqno"].ToString();
                    //matcode = dt1.Rows[0]["rsircode"].ToString();
                    //spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == matcode && dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //    {

                    //        dt1.Rows[j]["reqno1"] = "";
                    //        dt1.Rows[j]["mrfno"] = "";
                    //        dt1.Rows[j]["reqdat"] = "";
                    //        dt1.Rows[j]["shipsupdat"] = "";
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";

                    //    }

                    //    else
                    //    {
                    //        if (dt1.Rows[j]["reqno"].ToString() == reqno)
                    //        {
                    //            dt1.Rows[j]["reqno1"] = "";
                    //            dt1.Rows[j]["mrfno"] = "";
                    //            dt1.Rows[j]["reqdat"] = "";
                    //            dt1.Rows[j]["shipsupdat"] = "";
                    //            dt1.Rows[j]["pactdesc"] = "";
                    //        }
                    //        if (dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //            dt1.Rows[j]["rsirdesc"] = "";
                    //        if (dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //            dt1.Rows[j]["spcfdesc"] = "";





                    //    }


                    //    reqno = dt1.Rows[j]["reqno"].ToString();
                    //    matcode = dt1.Rows[j]["rsircode"].ToString();
                    //    spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //}

                    break;


                case "Purchasetrk02":
                    //string ppactcode = dt1.Rows[0]["pactcode"].ToString();
                    //string matcode = dt1.Rows[0]["rsircode"].ToString();
                    //string spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["pactcode"].ToString() == ppactcode && dt1.Rows[j]["rsircode"].ToString() == matcode && dt1.Rows[j]["spcfcod"].ToString() ==spcfcod)
                    //    {
                    //        ppactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        matcode = dt1.Rows[j]["rsircode"].ToString();
                    //        spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //        dt1.Rows[j]["rsirdesc"] = "";
                    //        dt1.Rows[j]["rsirunit"] = "";
                    //        dt1.Rows[j]["spcfdesc"] = "";
                    //        dt1.Rows[j]["areqty"] = 0.0000000;
                    //    }

                    //    else
                    //    {
                    //         if (dt1.Rows[j]["pactcode"].ToString() == ppactcode)
                    //            dt1.Rows[j]["pactdesc"] = "";
                    //         if (dt1.Rows[j]["rsircode"].ToString() == matcode)
                    //             dt1.Rows[j]["rsirdesc"] = "";
                    //         if (dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                    //             dt1.Rows[j]["spcfdesc"] = "";




                    //        ppactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        matcode = dt1.Rows[j]["rsircode"].ToString();
                    //        spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                    //    }
                    //}

                    break;



                case "MatRateVar":

                    string rsircode = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        {

                            dt1.Rows[j]["rsirdesc"] = "";
                            dt1.Rows[j]["rsirunit"] = "";
                        }

                        rsircode = dt1.Rows[j]["rsircode"].ToString();

                    }

                    break;


            }


            return dt1;

        }


        private void LoadGrid()
        {

            try
            {
                DataTable dt = ((DataTable)Session["tblpurchase"]).Copy();

                if ((dt.Rows.Count == 0)) //Problem
                    return;

                string rpt = this.Request.QueryString["Rpt"].ToString().Trim();
                switch (rpt)
                {
                    case "DaywPur":
                    case "IndSup":
                        this.gvPurStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPurStatus.DataSource = dt;
                        this.gvPurStatus.DataBind();
                        ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                             0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        if (ddlProjectName.SelectedValue.ToString() != "000000000000")
                        {

                            if (ddlMatCode.SelectedValue.ToString() != "000000000000")
                            {
                                ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ?
                                                0 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                            }

                        }
                        break;

                    case "PurSum":
                        this.gvPurSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPurSum.DataSource = dt;
                        this.gvPurSum.DataBind();
                        ((Label)this.gvPurSum.FooterRow.FindControl("lgvFAmtS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                             0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        break;

                    case "PenBill":



                        break;

                    case "Purchasetrk":
                        this.gvPurstk01.DataSource = dt;
                        this.gvPurstk01.DataBind();

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


                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFareqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(areqty)", "")) ?
                                                0 : dt.Compute("sum(areqty)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFprogqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(progqty)", "")) ?
                                            0 : dt.Compute("sum(progqty)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFordrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ?
                                            0 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvBgdBal.FooterRow.FindControl("lgvFmrrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrrqty)", "")) ?
                                            0 : dt.Compute("sum(mrrqty)", ""))).ToString("#,##0;(#,##0); ");
                        break;


                    case "PurBilltk":
                        this.gvPurBilltk.DataSource = dt;
                        this.gvPurBilltk.DataBind();
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = ("grp='F'");
                        dt = dv.ToTable();

                        ((Label)this.gvPurBilltk.FooterRow.FindControl("lblgvFbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                        break;

                    case "MatRateVar":
                        this.gvPurMatRVar.DataSource = dt;
                        this.gvPurMatRVar.DataBind();
                        break;

                }

            }
            catch (Exception ex)
            {


            }




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
            this.LoadGrid();
        }





        protected void imgbtnFindReqno02_Click(object sender, EventArgs e)
        {
            this.GetReqno02();
        }
        protected void imgbtnFindBill_Click(object sender, EventArgs e)
        {
            this.GetBillNo();
        }

        protected void imgbtnFindMatCom_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string txtfindMat = "%" + this.txtMatcomSearch.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETMATERIALCOM", txtfindMat, "", "", "", "", "", "", "", "");
            this.ddlMaterialscom.DataTextField = "sirdesc";
            this.ddlMaterialscom.DataValueField = "sircode";
            this.ddlMaterialscom.DataSource = ds1.Tables[0];
            this.ddlMaterialscom.DataBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

            if (!(Convert.ToBoolean(dr1[0]["entry"])))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You Have No Permission');", true);
                return;

            }



            string comcod = this.GetComeCode();
            string Billno = this.ddlBillno.SelectedValue.ToString();
            bool result = MktData.UpdateTransInfo2(comcod, "SP_REPORT_REQ_STATUS", "DELETEPURCHASE", Billno, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Fail');", true);
                return;

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);




        }


    }
}