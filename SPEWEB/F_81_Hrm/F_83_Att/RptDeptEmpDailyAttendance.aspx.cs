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

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class RptDeptEmpDailyAttendance : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "DailyAtten") ? "EMPLOYEE DAILY ATTENDANCE INFORMATION"
                    : (this.Request.QueryString["Type"].ToString() == "MgtDailyAtten") ? "DAILY TEA MEETING ATTENDANCE"
                    : (this.Request.QueryString["Type"].ToString() == "DailyOverTime") ? "EMPLOYEE DAILY OVERTIME INFORMATION"

                    : "EMPLOYEE MONTHLY LATE ATTENDANCE INFORMATION";

                this.GetCompName();
                this.GetDesignation();
                this.ViewSection();
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


        private void GetCompName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETCOMPANYNAMEIALL", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            this.GetDeptName();

        }

        private void GetDeptName()
        {
            string comcod = this.GetCompCode();
            string Company = ((this.ddlCompanyName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2)) + "%";
            string txtSProject = "%" + this.txtSrcDept.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.DropCheck1.DataTextField = "actdesc";
            this.DropCheck1.DataValueField = "actdesc";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();
            ds1.Dispose();

        }

        private void GetSectionName()
        {

            //string comcod = this.GetCompCode();
            //string Company = ((this.ddlCompanyName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2)) + "%";
            //string Department = "%";
            //string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
            //DataSet ds2 = HRData.GetTransInfo(comcod, "SP_REPORT_HR_ATTENDENCE", "SECTIONNAMEDP", Company, Department, txtSSec, "", "", "", "", "", "");
            //this.DropCheck2.DataTextField = "sectionname";
            //this.DropCheck2.DataValueField = "sectionname";
            //this.DropCheck2.DataSource = ds2.Tables[0];
            //this.DropCheck2.DataBind();

        }






        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptName();
        }


        protected void ibtnFindCompany_Click(object sender, EventArgs e)
        {
            this.GetCompName();
        }
        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDeptName();
        }

        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.GetSectionName();
        }

        private void GetDesignation()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDESIGNATION", "", "", "", "", "", "", "", "", "");
            Session["tbldesig"] = ds1.Tables[0];
            if (ds1 == null)
                return;
            this.ddlfrmDesig.DataTextField = "designation";
            this.ddlfrmDesig.DataValueField = "desigcod";
            this.ddlfrmDesig.DataSource = ds1.Tables[0];
            this.ddlfrmDesig.DataBind();
            this.ddlfrmDesig.SelectedValue = "0345005";
            this.GetDessignationTo();
        }

        private void GetDessignationTo()
        {

            DataTable dt = (DataTable)Session["tbldesig"];
            //string desigcod = this.ddlfrmDesig.SelectedValue.ToString().Trim();
            //DataView dv1 = dt.DefaultView;
            //dv1.RowFilter = "desigcod not in ('" + desigcod + "')";
            this.ddlToDesig.DataTextField = "designation";
            this.ddlToDesig.DataValueField = "desigcod";
            this.ddlToDesig.DataSource = dt;
            this.ddlToDesig.DataBind();

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        private void ViewSection()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "DailyAtten":
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lbltodate.Visible = false;
                    this.txttoDate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "MonthlyLateAtten":
                    this.lblfrmdate.Text = "From:";
                    this.txtDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtDate.Text = "26" + this.txtDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "DailyOverTime":
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lbltodate.Visible = false;
                    this.txttoDate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "MgtDailyAtten":
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lbltodate.Visible = false;
                    this.txttoDate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;


            }
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "DailyAtten":
                    this.ShowDailyAtten();
                    break;


                case "DailyOverTime":
                    this.ShowDailyOverTime();
                    break;


                case "MgtDailyAtten":
                    this.showMgtAttendance();
                    break;

            }




        }


        private void ShowDailyAtten()
        {

            this.pnlGroup.Visible = true;
            Session.Remove("tbldailyattn");
            string comcod = this.GetCompCode();
            string Company = ((this.ddlCompanyName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2)) + "%";

            string DeptCode = "";

            string[] dept = this.DropCheck1.Text.Trim().Split(',');

            if (dept[0].Substring(0, 12) == "000000000000")
                DeptCode = "";
            else
                foreach (string d1 in dept)
                    DeptCode = DeptCode + d1.Substring(0, 12);



            string FrmDesignation = this.ddlfrmDesig.SelectedValue.ToString();
            string ToDesignation = this.ddlToDesig.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_HR_ATTENDENCE02", "RPTEMPDAILYATTN02", Company, ToDesignation, FrmDesignation, date, DeptCode, "", "", "", "");
            if (ds1 == null)
            {
                this.gvDailyAttn.DataSource = null;
                this.gvDailyAttn.DataBind();
                return;

            }
            Session["tbldailyattn"] = this.HiddenSameData(ds1.Tables[0]);
            this.lblvalTotalemployee.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["temployee"]).ToString("#,##0;(#,##0); ");
            this.lblvalAbsent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["absemp"]).ToString("#,##0;(#,##0); ");
            this.lblvalLeave.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["leaveemp"]).ToString("#,##0;(#,##0); ");
            this.lblvalPresent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["presntemp"]).ToString("#,##0;(#,##0); ");
            this.lblvalResign.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["resignemp"]).ToString("#,##0;(#,##0); ");

            this.lblvallatew5mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["lw5min"]).ToString("#,##0;(#,##0); ");
            this.lblVallatewi6to10mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["l6to10"]).ToString("#,##0;(#,##0); ");
            this.lblVallate11onwards.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["l11toup"]).ToString("#,##0;(#,##0); ");

            this.lblValEarlyLate.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["eleft"]).ToString("#,##0;(#,##0); ");
            this.lblValoutw30mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw30mi"]).ToString("#,##0;(#,##0); ");
            this.lblValoutw31to60mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw31to60mi"]).ToString("#,##0;(#,##0); ");
            this.lblValoutw61to90mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw61to90mi"]).ToString("#,##0;(#,##0); ");
            this.lblValoutw91toabove.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw91toabove"]).ToString("#,##0;(#,##0); ");
            this.Data_Bind();


        }


        private void showMgtAttendance()
        {


            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGTATTENDENCE", "RPTMGTDAILYATTN", date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDailyAttn.DataSource = null;
                this.gvDailyAttn.DataBind();
                return;

            }
            Session["tbldailyattn"] = this.HiddenSameData(ds1.Tables[0]);
            this.lblvalTotalemployee.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["temployee"]).ToString("#,##0;(#,##0); ");
            this.lblvalAbsent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["absemp"]).ToString("#,##0;(#,##0); ");
            this.lblvalLeave.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["leaveemp"]).ToString("#,##0;(#,##0); ");
            this.lblvalPresent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["presntemp"]).ToString("#,##0;(#,##0); ");
            this.lblvalResign.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["resignemp"]).ToString("#,##0;(#,##0); ");

            this.lblvallatew5mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["lw5min"]).ToString("#,##0;(#,##0); ");
            this.lblVallatewi6to10mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["l6to10"]).ToString("#,##0;(#,##0); ");
            this.lblVallate11onwards.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["l11toup"]).ToString("#,##0;(#,##0); ");

            this.lblValEarlyLate.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["eleft"]).ToString("#,##0;(#,##0); ");
            this.lblValoutw30mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw30mi"]).ToString("#,##0;(#,##0); ");
            this.lblValoutw31to60mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw31to60mi"]).ToString("#,##0;(#,##0); ");
            this.lblValoutw61to90mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw61to90mi"]).ToString("#,##0;(#,##0); ");
            this.lblValoutw91toabove.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw91toabove"]).ToString("#,##0;(#,##0); ");
            this.Data_Bind();


        }

        private void ShowDailyOverTime()
        {

            //this.pnlGroup.Visible = true;
            //Session.Remove("tbldailyattn");
            //string comcod = this.GetCompCode();
            //string Company = ((this.ddlCompanyName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2)) + "%";
            //string DeptCode = (this.ddlDeptName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDeptName.SelectedValue.ToString().Substring(0, 8) + "%";
            //string section = "";
            //if ((this.ddlDeptName.SelectedValue.ToString() != "000000000000"))
            //{
            //    string[] sec = this.DropCheck1.Text.Trim().Split(',');

            //    if (sec[0].Substring(0, 4) == "0000")
            //        section = "";
            //    else
            //        foreach (string s1 in sec)
            //            section = section + this.ddlDeptName.SelectedValue.ToString().Substring(0, 8) + s1.Substring(0, 4);

            //}

            //string FrmDesignation = this.ddlfrmDesig.SelectedValue.ToString();
            //string ToDesignation = this.ddlToDesig.SelectedValue.ToString();
            //string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_HR_ATTENDENCE", "RPTDAILYOVERTIME", Company, ToDesignation, FrmDesignation, date, DeptCode, section, "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvDailyOvr.DataSource = null;
            //    this.gvDailyOvr.DataBind();
            //    return;

            //}
            //Session["tbldailyattn"] = this.HiddenSameData(ds1.Tables[0]);

            //this.Data_Bind();
        }

        private void ShowMonthlyLateAtten()
        {


            //Session.Remove("tbldailyattn");
            //string comcod = this.GetCompCode();
            //string Company = ((this.ddlCompanyName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2)) + "%";

            //string DeptCode = (this.ddlDeptName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDeptName.SelectedValue.ToString().Substring(0, 8) + "%";
            //string section = "";
            //if ((this.ddlDeptName.SelectedValue.ToString() != "000000000000"))
            //{
            //    string[] sec = this.DropCheck1.Text.Trim().Split(',');

            //    if (sec[0].Substring(0, 4) == "0000")
            //        section = "";
            //    else
            //        foreach (string s1 in sec)
            //            section = section + this.ddlDeptName.SelectedValue.ToString().Substring(0, 8) + s1.Substring(0, 4);

            //}


            //string FrmDesignation = this.ddlfrmDesig.SelectedValue.ToString();
            //string ToDesignation = this.ddlToDesig.SelectedValue.ToString();
            //string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            //DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_HR_ATTENDENCE", "RPTMONTHLYLATEATTN02", Company, ToDesignation, FrmDesignation, frmdate, todate, DeptCode, section, "", "");
            //if (ds1 == null)
            //{
            //    this.gvMoLateAttn.DataSource = null;
            //    this.gvMoLateAttn.DataBind();
            //    return;

            //}
            //Session["tbldailyattn"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();


        }

        private void Data_Bind()
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "DailyAtten":
                case "MgtDailyAtten":
                    this.gvDailyAttn.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue);
                    this.gvDailyAttn.DataSource = (DataTable)Session["tbldailyattn"];
                    this.gvDailyAttn.DataBind();
                    break;


                case "DailyOverTime":
                    this.gvDailyOvr.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue);
                    this.gvDailyOvr.DataSource = (DataTable)Session["tbldailyattn"];
                    this.gvDailyOvr.DataBind();
                    break;



            }





        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string company = dt1.Rows[0]["company"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company)
                {
                    company = dt1.Rows[j]["company"].ToString();
                    dt1.Rows[j]["companyname"] = "";
                }

                else
                    company = dt1.Rows[j]["company"].ToString();


            }

            return dt1;

        }


        protected void ddlfrmDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDessignationTo();
        }
        protected void gvDailyAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvDailyAttn.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();

            DataTable dt = (DataTable)Session["tbldailyattn"];
            string comcod = this.GetCompCode();
            string dayid = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("yyyyMMdd");
            string date = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string rmrks = dt.Rows[i]["rmrks"].ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSORUPEMPARMRKS", dayid, empid, rmrks, "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;

            }

            this.lblmsg.Text = "Updated Successfully";

        }


        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tbldailyattn"];
            int rowindex;
            for (int i = 0; i < this.gvDailyAttn.Rows.Count; i++)
            {

                string rmrks = ((TextBox)this.gvDailyAttn.Rows[i].FindControl("txtgvrmrks")).Text.Trim();
                rowindex = (this.gvDailyAttn.PageSize) * (this.gvDailyAttn.PageIndex) + i;
                dt.Rows[rowindex]["rmrks"] = rmrks;

            }

            this.Data_Bind();


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "DailyAtten":
                    this.PrintDailyAtten();
                    break;


                case "DailyOverTime":
                    this.PrintDailyOverTime();
                    break;

                case "MgtDailyAtten":
                    this.PrintMgtDailyAtten();
                    break;



            }






        }

        private void PrintDailyAtten()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbldailyattn"];
            ReportDocument rptcb1 = new RMGiRPT.R_81_Hrm.R_83_Att.RptDailyAllEmpAttn02();

            TextObject txttotalemp = rptcb1.ReportDefinition.ReportObjects["txttotalemp"] as TextObject;
            txttotalemp.Text = this.lblvalTotalemployee.Text.Trim();
            TextObject txtabsent = rptcb1.ReportDefinition.ReportObjects["txtabsent"] as TextObject;
            txtabsent.Text = this.lblvalAbsent.Text.Trim();
            TextObject txtleave = rptcb1.ReportDefinition.ReportObjects["txtleave"] as TextObject;
            txtleave.Text = this.lblvalLeave.Text.Trim();
            TextObject txtpresent = rptcb1.ReportDefinition.ReportObjects["txtpresent"] as TextObject;
            txtpresent.Text = this.lblvalPresent.Text.Trim();
            TextObject txtresign = rptcb1.ReportDefinition.ReportObjects["txtresign"] as TextObject;
            txtresign.Text = this.lblvalResign.Text.Trim();

            TextObject txtlate5min = rptcb1.ReportDefinition.ReportObjects["txtlate5min"] as TextObject;
            txtlate5min.Text = this.lblvallatew5mi.Text.Trim();
            TextObject txtlate6to10min = rptcb1.ReportDefinition.ReportObjects["txtlate6to10min"] as TextObject;
            txtlate6to10min.Text = this.lblVallatewi6to10mi.Text.Trim();
            TextObject txtlate11minon = rptcb1.ReportDefinition.ReportObjects["txtlate11minon"] as TextObject;
            txtlate11minon.Text = this.lblVallate11onwards.Text;

            TextObject txteleft = rptcb1.ReportDefinition.ReportObjects["txteleft"] as TextObject;
            txteleft.Text = this.lblValEarlyLate.Text.Trim();
            TextObject txtoutw30mi = rptcb1.ReportDefinition.ReportObjects["txtoutw30mi"] as TextObject;
            txtoutw30mi.Text = this.lblValoutw30mi.Text.Trim();
            TextObject txtoutw31to60mi = rptcb1.ReportDefinition.ReportObjects["txtoutw31to60mi"] as TextObject;
            txtoutw31to60mi.Text = this.lblValoutw31to60mi.Text.Trim();
            TextObject txtoutw61to90mi = rptcb1.ReportDefinition.ReportObjects["txtoutw61to90mi"] as TextObject;
            txtoutw61to90mi.Text = this.lblValoutw61to90mi.Text.Trim();
            TextObject txtoutw91toabove = rptcb1.ReportDefinition.ReportObjects["txtoutw91toabove"] as TextObject;
            txtoutw91toabove.Text = this.lblValoutw91toabove.Text.Trim();
            TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptdate.Text = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd MMMM,yyyy");
            TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptcb1.SetDataSource(dt);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptcb1.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptcb1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintMgtDailyAtten()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbldailyattn"];
            //ReportDocument rptcb1 = new RMGiRPT.R_81_Hrm.R_83_Att.RptDailyMgtAttn();

            //TextObject txttotalemp = rptcb1.ReportDefinition.ReportObjects["txttotalemp"] as TextObject;
            //txttotalemp.Text = this.lblvalTotalemployee.Text.Trim();
            //TextObject txtabsent = rptcb1.ReportDefinition.ReportObjects["txtabsent"] as TextObject;
            //txtabsent.Text = this.lblvalAbsent.Text.Trim();
            //TextObject txtleave = rptcb1.ReportDefinition.ReportObjects["txtleave"] as TextObject;
            //txtleave.Text = this.lblvalLeave.Text.Trim();
            //TextObject txtpresent = rptcb1.ReportDefinition.ReportObjects["txtpresent"] as TextObject;
            //txtpresent.Text = this.lblvalPresent.Text.Trim();
            //TextObject txtresign = rptcb1.ReportDefinition.ReportObjects["txtresign"] as TextObject;
            //txtresign.Text = this.lblvalResign.Text.Trim();

            //TextObject txtlate5min = rptcb1.ReportDefinition.ReportObjects["txtlate5min"] as TextObject;
            //txtlate5min.Text = this.lblvallatew5mi.Text.Trim();
            //TextObject txtlate6to10min = rptcb1.ReportDefinition.ReportObjects["txtlate6to10min"] as TextObject;
            //txtlate6to10min.Text = this.lblVallatewi6to10mi.Text.Trim();
            //TextObject txtlate11minon = rptcb1.ReportDefinition.ReportObjects["txtlate11minon"] as TextObject;
            //txtlate11minon.Text = this.lblVallate11onwards.Text;

            //TextObject txteleft = rptcb1.ReportDefinition.ReportObjects["txteleft"] as TextObject;
            //txteleft.Text = this.lblValEarlyLate.Text.Trim();
            //TextObject txtoutw30mi = rptcb1.ReportDefinition.ReportObjects["txtoutw30mi"] as TextObject;
            //txtoutw30mi.Text = this.lblValoutw30mi.Text.Trim();
            //TextObject txtoutw31to60mi = rptcb1.ReportDefinition.ReportObjects["txtoutw31to60mi"] as TextObject;
            //txtoutw31to60mi.Text = this.lblValoutw31to60mi.Text.Trim();
            //TextObject txtoutw61to90mi = rptcb1.ReportDefinition.ReportObjects["txtoutw61to90mi"] as TextObject;
            //txtoutw61to90mi.Text = this.lblValoutw61to90mi.Text.Trim();
            //TextObject txtoutw91toabove = rptcb1.ReportDefinition.ReportObjects["txtoutw91toabove"] as TextObject;
            //txtoutw91toabove.Text = this.lblValoutw91toabove.Text.Trim();
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptdate.Text = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd MMMM,yyyy");
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(dt);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintDailyOverTime()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbldailyattn"];
            //ReportDocument rptcb1 = new RMGiRPT.R_81_Hrm.R_83_Att.RptDailyOvertime();
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptdate.Text = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd MMMM,yyyy");
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(dt);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        protected void gvMoLateAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMoLateAttn.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }



        protected void gvDailyOvr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDailyOvr.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }



    }
}