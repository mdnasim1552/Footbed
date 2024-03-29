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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_09_Commer
{
    public partial class RptDateWiseReq : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Periodic Purchase Tracking";


                this.GetProjectName();
                this.ViewSection();
                this.GetSeason();

            }
        }


        private void ViewSection()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            
            switch (Type)
            {
                case "PeriodPurchase":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    break;

                case "PendingStatus":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ToD.Visible = false;
                    this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    break;

            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetSeason()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

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


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            
            switch (Type)
            {
                case "PeriodPurchase":
                    this.PeriodPurchase();
                    break;

                case "PendingStatus":
                    this.PendingStatus();
                    break;

            }
        }

        private void PeriodPurchase()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string MrfNo = this.txtMRFNO.Text.Trim() + "%";
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string season = this.ddlSeason.SelectedValue == "00000" ? "%%" : this.ddlSeason.SelectedValue + "%";

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTDATEWISEREQ", fromdate, todate, MrfNo, pactcode, season, "", "", "", "");
            
            if (ds1 == null)
            {
                this.gvPurStatus.DataSource = null;
                this.gvPurStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = dt;
            this.Data_Bind();



        }

        private void PendingStatus()
        {
            Session.Remove("tblpurchase");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");

            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string MrfNo = this.txtMRFNO.Text.Trim() + "%";


            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPENDINGSTATUS", date, pactcode, MrfNo, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPenStatus.DataSource = null;
                this.gvPenStatus.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblpurchase"] = dt;
            this.Data_Bind();

        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject =  "%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        private void Data_Bind()
        {


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "PeriodPurchase":
                    this.gvPurStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPurStatus.DataSource = (DataTable)Session["tblpurchase"];
                    this.gvPurStatus.DataBind();

                    break;


                case "PendingStatus":
                    this.gvPenStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPenStatus.DataSource = (DataTable)Session["tblpurchase"];
                    this.gvPenStatus.DataBind();
                    this.FooterCalculation();

                    break;


            }



        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string reqno = dt1.Rows[0]["reqno"].ToString();


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "PeriodPurchase":

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["reqno"].ToString() == reqno)
                        {

                            dt1.Rows[j]["reqno1"] = "";
                            dt1.Rows[j]["reqdat"] = "";
                            dt1.Rows[j]["mrfno"] = "";
                            dt1.Rows[j]["pactdesc"] = "";

                        }


                        reqno = dt1.Rows[j]["reqno"].ToString();

                    }

                    break;


                case "PendingStatus":

                    string rsircode = dt1.Rows[0]["rsircode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["reqno"].ToString() == reqno && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        {

                            dt1.Rows[j]["reqno1"] = "";
                            dt1.Rows[j]["reqdat"] = "";
                            dt1.Rows[j]["aprvdat"] = "";
                            dt1.Rows[j]["areqty"] = 0.00;
                            dt1.Rows[j]["reqamt"] = 0.00;

                            dt1.Rows[j]["mrfno"] = "";
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["rsirdesc"] = "";
                            dt1.Rows[j]["rsirunit"] = "";
                        }
                        else
                        {
                            if (dt1.Rows[j]["reqno"].ToString() == reqno)
                            {

                                dt1.Rows[j]["reqno1"] = "";
                                dt1.Rows[j]["reqdat"] = "";
                                dt1.Rows[j]["aprvdat"] = "";
                                dt1.Rows[j]["mrfno"] = "";
                                dt1.Rows[j]["pactdesc"] = "";
                            }

                            //if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                            //{
                            //    dt1.Rows[j]["rsirdesc"] = "";
                            //    dt1.Rows[j]["rsirunit"] = "";
                            //    dt1.Rows[j]["areqty"] = 0.00;
                            //    dt1.Rows[j]["reqamt"] = 0.00;

                            //}


                        }

                        reqno = dt1.Rows[j]["reqno"].ToString();
                        rsircode = dt1.Rows[j]["rsircode"].ToString();

                    }




                    break;


            }


            return dt1;

        }


        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["tblpurchase"];
            if (dt1.Rows.Count == 0)
                return;

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "PeriodPurchase":


                    break;


                case "PendingStatus":


                    double nyordramt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(nyordramt)", "")) ?
                              0 : dt1.Compute("sum(nyordramt)", "")));

                    double nyrcvamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balamt)", "")) ?
                              0 : dt1.Compute("sum(balamt)", "")));

                    ((Label)this.gvPenStatus.FooterRow.FindControl("lgvFnyordramt")).Text = nyordramt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPenStatus.FooterRow.FindControl("lgvFordramt")).Text = (nyrcvamt - nyordramt).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPenStatus.FooterRow.FindControl("lgvFbalamt")).Text = nyrcvamt.ToString("#,##0;(#,##0); ");

                    break;


            }



        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvPurStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "PeriodPurchase":


                    break;


                case "PendingStatus":
                    this.PendingReqStatus();

                    break;
            }



        }


        private void PendingReqStatus()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");

            //DataTable dt1 = (DataTable)Session["tblpurchase"];
            //ReportDocument rrs1 = new RMGiRPT.R_11_Pro.RptReqPendingStatus();
            //TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "As On " + fromdate;
            //TextObject txtorderamt = rrs1.ReportDefinition.ReportObjects["txtorderamt"] as TextObject;
            //// txtorderamt.Text = ((Label)this.gvPenStatus.FooterRow.FindControl("lgvFordramt")).Text;


            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource(dt1);
            //Session["Report1"] = rrs1;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void gvPenStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPenStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}