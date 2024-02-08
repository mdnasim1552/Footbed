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
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class RptEmpDailyAttendance : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "DailyAtten") ? "EMPLOYEE DAILY ATTENDANCE INMROMATION"
                    : (this.Request.QueryString["Type"].ToString() == "MgtDailyAtten") ? "DAILY TEA MEETING ATTENDANCE"
                    : (this.Request.QueryString["Type"].ToString() == "DailyOverTime") ? "EMPLOYEE DAILY OVERTIME INMROMATION"
                    : (this.Request.QueryString["Type"].ToString() == "AttendanceSummary") ? "EMPLOYEE ATTENDANCE SUMMARY (DAYWISE)"
                    : "EMPLOYEE MONTHLY LATE ATTENDANCE INMROMATION";

                GetWorkStation();
                GetAllOrganogramList();
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

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string userid = hst["usrid"].ToString();
            //string txtCompany = "%"+this.txtSrcCompany.Text.Trim() + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETCOMPANYNAMEIALL", txtCompany, userid, "", "", "", "", "", "", "");
            //this.ddlWstation.DataTextField = "actdesc";
            //this.ddlWstation.DataValueField = "actcode";
            //this.ddlWstation.DataSource = ds1.Tables[0];
            //this.ddlWstation.DataBind();
            //this.GetDeptName();

        }

        private void GetDeptName()
        {
            //string comcod = this.GetCompCode();
            //string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 2)) + "%";
            //string txtSProject = "%" + this.txtSrcDept.Text.Trim() + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            //this.ddlDeptName.DataTextField = "actdesc";
            //this.ddlDeptName.DataValueField = "actcode";
            //this.ddlDeptName.DataSource = ds1.Tables[0];
            //this.ddlDeptName.DataBind();
            //this.GetSectionName();

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

                case "AttendanceSummary":
                    this.lblfrmdate.Text = "From:";
                    this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtDate.Text = "01" + this.txtDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 3;
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

                case "AttendanceSummary":
                    this.showAttendancesummary();
                    break;

            }




        }


        private void ShowDailyAtten()
        {

            this.pnlGroup.Visible = true;
            Session.Remove("tbldailyattn");
            string comcod = this.GetCompCode();

            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";


            //string section = "";
            //foreach (ListItem item in listProject.Items)
            //{
            //    if (item.Selected)
            //    {
            //        section += item.Value;
            //    }
            //}






            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPDAILYATTN02", Company, "", "", date, section, section, divison, "", "");
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

            this.pnlGroup.Visible = true;
            Session.Remove("tbldailyattn");
            string comcod = this.GetCompCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string DeptCode = "";
            string section = "";
            //nahid 20181017
            //foreach (ListItem item in listProject.Items)
            //{
            //    if (item.Selected)
            //    {
            //        section += item.Value;
            //    }
            //}

            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTDAILYOVERTIME", Company, "", "", date, DeptCode, section, "", "", "");
            if (ds1 == null)
            {
                this.gvDailyOvr.DataSource = null;
                this.gvDailyOvr.DataBind();
                return;

            }
            Session["tbldailyattn"] = this.HiddenSameData(ds1.Tables[0]);

            this.Data_Bind();
        }

        private void ShowMonthlyLateAtten()
        {


            Session.Remove("tbldailyattn");
            string comcod = this.GetCompCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";

            string DeptCode = "";
            string section = "";
            //nahid 20181017
            //
            //foreach (ListItem item in listProject.Items)
            //{
            //    if (item.Selected)
            //    {
            //        section += item.Value;
            //    }
            //}

            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTMONTHLYLATEATTN02", Company, "", "", frmdate, todate, DeptCode, section, "", "");
            if (ds1 == null)
            {
                this.gvMoLateAttn.DataSource = null;
                this.gvMoLateAttn.DataBind();
                return;

            }
            Session["tbldailyattn"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }

        private void showAttendancesummary()
        {
            this.pnlGroup.Visible = true;
            Session.Remove("tbldailyattn");
            string comcod = this.GetCompCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "940000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";

            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";



            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPDAILYATTNSUMMARY", Company, "", "", date, deptname, section, todate, divison, "");
            if (ds1 == null)
            {
                this.gvattendsum.DataSource = null;
                this.gvattendsum.DataBind();
                return;

            }
            Session["tbldailyattn"] = ds1.Tables[0];
            this.Data_Bind();




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

                case "AttendanceSummary":
                    this.gvattendsum.DataSource = (DataTable)Session["tbldailyattn"];
                    this.gvattendsum.DataBind();
                    this.FoooterCalculation();
                    break;



            }





        }
        private void FoooterCalculation()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            DataTable dt = (DataTable)Session["tbldailyattn"];
            switch (Type)
            {
                case "DailyAtten":
                case "MgtDailyAtten":

                    break;


                case "DailyOverTime":

                    break;

                case "AttendanceSummary":
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFtoemployee")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(nofemployee)", "")) ? 0.00 : dt.Compute("Sum(nofemployee)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFabsent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(absnt)", "")) ? 0.00 : dt.Compute("Sum(absnt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFleave")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(leave)", "")) ? 0.00 : dt.Compute("Sum(leave)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFpresent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(present)", "")) ? 0.00 : dt.Compute("Sum(present)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFlwi5min")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lw5min)", "")) ? 0.00 : dt.Compute("Sum(lw5min)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFlwi6to10min")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lw6to10min)", "")) ? 0.00 : dt.Compute("Sum(lw6to10min)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFlwi11to30min")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lw11to30min)", "")) ? 0.00 : dt.Compute("Sum(lw11to30min)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFla10am")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(la10am)", "")) ? 0.00 : dt.Compute("Sum(la10am)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFtolate")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tolate)", "")) ? 0.00 : dt.Compute("Sum(tolate)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFeleavebofot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(eleavebofot)", "")) ? 0.00 : dt.Compute("Sum(eleavebofot)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFleaveaofot")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(leaveaofot)", "")) ? 0.00 : dt.Compute("Sum(leaveaofot)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvattendsum.FooterRow.FindControl("lblgvFtoelaafleave")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(toelaafleave)", "")) ? 0.00 : dt.Compute("Sum(toelaafleave)", ""))).ToString("#,##0;(#,##0); ");

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
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    return;
                }

                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                }


            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

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
                case "AttendanceSummary":
                    this.PrintAttenSummary();

                    break;



            }






        }

        private void PrintDailyAtten()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptcb1.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptcb1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
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
           // ReportDocument rptcb1 = new RMGiRPT.R_81_Hrm.R_83_Att.RptDailyMgtAttn();

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
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



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
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        private void PrintAttenSummary()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbldailyattn"];
            //ReportDocument rptcb1 = new RMGiRPT.R_81_Hrm.R_83_Att.RptAttendanceSummary();

            //TextObject txtCompName = rptcb1.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //txtCompName.Text = this.ddlWstation.SelectedItem.Text;
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptdate.Text = "( From " + Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy") + ")";
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(dt);

            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
        protected void gvattendsum_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;

                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 1;
                cell03.Font.Bold = true;

                TableCell cell04 = new TableCell();
                cell04.Text = "";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 1;
                cell04.Font.Bold = true;

                TableCell cell05 = new TableCell();
                cell05.Text = "";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 1;
                cell05.Font.Bold = true;

                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;
                cell06.Font.Bold = true;

                TableCell cell07 = new TableCell();
                cell07.Text = "Late";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 5;
                cell07.Font.Bold = true;

                TableCell cell08 = new TableCell();
                cell08.Text = "Office Left";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 3;
                cell08.Font.Bold = true;




                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);

                gvattendsum.Controls[0].Controls.AddAt(0, gvrow);
            }

        }


        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            string Type = this.Request.QueryString["Type"].ToString();

            if (Type == "AttendanceSummary")
            {
                //var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1 all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1 { actcode = "940000000000", actdesc = "All Type", hrcomname="", hrcomadd="", hrcomnameb="", hrcomaddb="" };
                lst.Add(all);
            }




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
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
            lst1.Add(all);






            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst1;
            this.ddlDivision.DataBind();

            this.ddlDivision_SelectedIndexChanged(null, null);

        }

        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);

            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept_SelectedIndexChanged(null, null);

        }

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();

        }


        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
    }
}