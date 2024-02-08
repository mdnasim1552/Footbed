using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using Microsoft.Reporting.WinForms;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using SPELIB;
using SPERDLC;
using System.Net;
using System.Net.Mail;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using System.IO;
using System.Text;
using System.Xml;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptMonHolidayAllow : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        int curd;
        int frdate;
        int mon;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text =
                    (this.Request.QueryString["Type"].ToString().Trim() == "MonthlyHolidayAllow")
                        ? "Employee Holiday Allowance"
                        : "Employee Holiday Allowance";
               
                
                string date = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                string txtDate = "01" + date.Substring(2);
                this.txtfromdate.Text = txtDate;
                this.txttodate.Text = Convert.ToDateTime(txtDate).AddMonths(1).AddDays(-1)
                    .ToString("dd-MMM-yyyy");
                GetWorkStation();
                GetAllOrganogramList();
                GetJobLocation();
                GetLineddl();

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
        }

        public void CommonButton()
        {
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;


            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;

            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetLineddl()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";

            ViewState["tbllineddl"] = ds3.Tables[0];
        }

        private void GetJobLocation()
        {
            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "MonthlyHolidayAllow":
                    this.ShowMonHolidayAllow();
                    break;
            }
        }

        private void ShowMonHolidayAllow()
        {
            Session.Remove("tblMonHoAllow");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string EmpType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string Division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%": this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL03", "RPTMONHOLIDAYALLOWANCE", null, null, null, frmdate, todate, EmpType,
                    Division, Department, section, line, joblocation, userid, "", "", "", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvMonHolidayAllow.DataSource = null;
                this.gvMonHolidayAllow.DataBind();
                return;
            }

            DataTable dt = ds1.Tables[0];
            Session["tblMonHoAllow"] = HiddenSameData(dt);
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            string company, secid;
            switch (type)
            {
                case "MonthlyHolidayAllow":

                    company = dt1.Rows[0]["companyid"].ToString();

                    secid = dt1.Rows[0]["sectionid"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["companyid"].ToString() == company && dt1.Rows[j]["sectionid"].ToString() == secid)
                        {

                            dt1.Rows[j]["companyname"] = "";
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["companyid"].ToString() == company)
                                dt1.Rows[j]["companyname"] = "";

                            if (dt1.Rows[j]["sectionid"].ToString() == secid)
                                dt1.Rows[j]["secton"] = "";
                        }


                        company = dt1.Rows[j]["companyid"].ToString();
                        secid = dt1.Rows[j]["sectionid"].ToString();
                    }
                    int i = 0;
                    string deptid = dt1.Rows[0]["deptid"].ToString();
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (i == 0)
                        {
                            deptid = dr1["deptid"].ToString();
                            i++;
                            continue;
                        }

                        if (dr1["deptid"].ToString() == deptid)
                        {

                            dr1["deptname"] = "";

                        }

                        deptid = dr1["deptid"].ToString();
                    }
                    break;
            }

            return dt1;
        }

        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {


        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblMonHoAllow"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "MonthlyHolidayAllow":
                    this.gvMonHolidayAllow.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMonHolidayAllow.DataSource = dt;
                    this.gvMonHolidayAllow.DataBind();
                    break;
            }

            if(dt.Rows.Count>0)
            {
                this.FooterCalculation();
            }

        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblMonHoAllow"];
            ((Label)this.gvMonHolidayAllow.FooterRow.FindControl("lblgvFTotAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hoallowamt)", "")) ? 0.00 : 
                                                                                            dt.Compute("sum(hoallowamt)", ""))).ToString("#,##0;(#,##0); ");
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "MonthlyHolidayAllow":
                    this.PrintMonHolidayAllow();
                    break;
            }
        }

        private void PrintMonHolidayAllow()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compName = hst["comnam"].ToString();
            string compAdd = hst["comaddf"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string txtDate = Convert.ToDateTime(this.txttodate.Text).ToString("MMM-yyyy");           
            
            DataTable dt = (DataTable)Session["tblMonHoAllow"];
            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.MonHolidayAllownace>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptMonHolidayAllowance", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", compName));
            Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Staff Offday Allowance For The Month Of " + txtDate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ibtnEmpList_Click(null, null);
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

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            Session["hrcompnameadd"] = lst;
            this.ddlWstation_SelectedIndexChanged(null, null);

        }

        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString(); //940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst =
                (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];


            var lst1 = lst.FindAll(x =>
                x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" &&
                x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all =
                new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf
                { actcode = "000000000000", actdesc = "All Division" };
            lst1.Add(all);

            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst1;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "000000000000";

            this.ddlDivision_SelectedIndexChanged(null, null);

        }

        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString(); //940100000000

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst =
                (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x =>
                x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" &&
                x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all =
                new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf
                { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "000000000000";

            this.ddlDept_SelectedIndexChanged(null, null);

        }

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString(); //940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst =
                (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all =
                new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf
                { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";
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

        protected void gvMonHolidayAllow_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonHolidayAllow.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}