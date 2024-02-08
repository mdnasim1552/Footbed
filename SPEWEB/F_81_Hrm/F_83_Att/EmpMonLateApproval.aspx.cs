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
using SPERDLC;
using Microsoft.Reporting.WinForms;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class EmpMonLateApproval : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtfrmDate.Text = "01" + this.txtfrmDate.Text.Trim().Substring(2);
                this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetWorkStation();
                this.GetAllOrganogramList();
                //this.GetCompName();
                this.GetDesignation();
                //this.GetDepartment();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "MLateAppDay") ? "Monthly Late  Approval Information"
                    : (this.Request.QueryString["Type"].ToString() == "MPunchAppDay") ? "Monthly One Time Punch Approval Information"
                    : (this.Request.QueryString["Type"].ToString() == "MnthabsentApp") ? "Attendance Approval" : "Monthly Absent Approval";
                this.ViewSaction();
                this.CommonButton();
                this.GetJobLocation();
            }

        }
        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtn_FiUpdate);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(btn_Recalculate);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void btn_Recalculate(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":
                    this.lbtnTotalDay_Click(null, null);
                    break;
                case "MPunchAppDay":
                    this.lbtnTotalP_Click(null, null);
                    break;

                case "MabsentApp":
                    this.lbtnTotalabs_Click(null, null);
                    break;

                case "MnthabsentApp":
                    
                    break;

            }


        }

        protected void lbtn_FiUpdate(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":
                    this.btnUpdateDayAdj_Click(null,null);
                    break;
                case "MPunchAppDay":
                    this.btnUpdatePunch_Click(null, null);
                    break;

                case "MabsentApp":
                    this.btnUpdateAbsent_Click(null, null);
                    break;

                case "MnthabsentApp":
                    this.btnUpdateAbsentx_Click(null, null);
                    break;

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

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
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
            this.ddlDivision.SelectedValue = "000000000000";
            this.ddlDivision_SelectedIndexChanged(null, null);

        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
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
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
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
            //lst1.Add()

            //this.ddlSection.DataTextField = "actdesc";
            //this.ddlSection.DataValueField = "actcode";
            //this.ddlSection.DataSource = lst1;
            //this.ddlSection.DataBind();

            //this.ddlSection.SelectedValue = "000000000000";

            this.DropCheck1.DataTextField = "actdesc";
            this.DropCheck1.DataValueField = "actcode";
            this.DropCheck1.DataSource = lst1;
            this.DropCheck1.DataBind();



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

        private void ViewSaction()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "MPunchAppDay":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "MabsentApp":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "MnthabsentApp":
                    //this.dpt.Visible = false;
                    this.MultiView1.ActiveViewIndex = 3;
                    break;





            }


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        


        private void GetDesignation()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDESIGNATION", "", "", "", "", "", "", "", "", "");
            //if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
            //    ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
            //    return;
            //}

            Session["tbldesig"] = ds1.Tables[0];

            this.ddlfrmDesig.DataTextField = "designation";
            this.ddlfrmDesig.DataValueField = "desigcod";
            this.ddlfrmDesig.DataSource = ds1.Tables[0];
            this.ddlfrmDesig.DataBind();
            this.ddlfrmDesig.SelectedValue = "0357999";
            this.GetDessignationTo();
        }

        private void GetDessignationTo()
        {

            DataTable dt = (DataTable)Session["tbldesig"];

            this.ddlToDesig.DataTextField = "designation";
            this.ddlToDesig.DataValueField = "desigcod";
            this.ddlToDesig.DataSource = dt;
            this.ddlToDesig.DataBind();

        }

        protected void ddlfrmDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDessignationTo();
        }


        //protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.GetDepartment();

        //}
        //protected void ibtnFindCompany_Click(object sender, EventArgs e)
        //{
        //    this.GetCompName();
        //}

        //protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        //{
        //    this.GetDepartment();
        //}

        //protected void imgbtnSection_Click(object sender, EventArgs e)
        //{
        //    this.GetSection();
        //}

        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.txtfrmDate.Enabled = false;
                this.txttoDate.Enabled = false;
                this.ddlWstation.Enabled = false;
                this.ddlDivision.Enabled = false;
                this.ddlDept.Enabled = false;
                this.DropCheck1.Enabled = false;
                //this.lblDeptDesc.Enabled = false;


                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.lnkbtnShow.Text = "New";

                this.ShowData();
                return;
            }

            this.txtfrmDate.Enabled = true;
            this.txttoDate.Enabled = true;
            this.ddlWstation.Enabled = true;
            this.ddlDivision.Enabled = true;
            this.ddlDept.Enabled = true;
            this.DropCheck1.Enabled = true;


            //this.ddlCompanyName.Visible = true;
            //this.ddlDept.Visible = true;
            //this.lblCompanyName.Visible = false;
            //this.lblDeptDesc.Visible = false;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.grvAdjDay.DataSource = null;
            this.grvAdjDay.DataBind();
            this.gvOPunch.DataSource = null;
            this.gvOPunch.DataBind();
            this.gvmapsapp.DataSource = null;
            this.gvmapsapp.DataBind();
            this.lnkbtnShow.Text = "Ok";
            //this.lblCompanyName.Text = "";
            
        }

        private void ShowData()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":
                    this.ShowMonthlyLate();
                    break;
                case "MPunchAppDay":
                    this.ShowMPunchAppDay();
                    break;
                case "MabsentApp":
                    this.ShowMabsentApp();
                    break;
                case "MnthabsentApp":
                    this.ShowMonthabsentApp();
                    break;


            }



        }


        private void ShowMonthlyLate()
        {
            Session.Remove("tblover"); 
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string compname = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%"; ;
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.DropCheck1.SelectedValue.ToString() == "000000000000") ? "%" : this.DropCheck1.SelectedValue.ToString();
            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPDAYADJUSTMENT", compname, frmdate, todate, deptname, section, Empcode, joblocation, userid, div);
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                this.grvAdjDay.DataSource = null;
                this.grvAdjDay.DataBind();
                return;
            }

            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        private void ShowMPunchAppDay()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string compname = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%"; ;
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.DropCheck1.SelectedValue.ToString() == "000000000000") ? "%" : this.DropCheck1.SelectedValue.ToString() + "%";
            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMOPUNCHAPPROVAL", compname, frmdate, todate, deptname, section, Empcode, "", "", div);
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                this.grvAdjDay.DataSource = null;
                this.grvAdjDay.DataBind();
                return;
            }

            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        private void ShowMabsentApp()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string compname = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%"; ;
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.DropCheck1.SelectedValue.ToString() == "000000000000") ? "%" : this.DropCheck1.SelectedValue.ToString() + "%";
            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMONABSADJUSTMENT", compname, frmdate, todate, deptname, section, Empcode, "", "", div);
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                this.grvAdjDay.DataSource = null;
                this.grvAdjDay.DataBind();
                return;
            }

            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }


        private void ShowMonthabsentApp()
        {
            Session.Remove("tbloverabsnt");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 7) + "%";
            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "GETEMPABSENTLIST", frmdate, todate, deptname, Empcode, joblocation, userid);
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                this.gvMonthAbsent.DataSource = null;
                this.gvMonthAbsent.DataBind();
                return;
            }

            Session["tbloverabsnt"] = ds2.Tables[0];
            this.Data_Bind();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string company = dt1.Rows[0]["company"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["secid"].ToString() == secid)
                {

                    dt1.Rows[j]["companyname"] = "";
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["company"].ToString() == company)
                        dt1.Rows[j]["companyname"] = "";
                    if (dt1.Rows[j]["secid"].ToString() == secid)
                        dt1.Rows[j]["section"] = "";
                }

                company = dt1.Rows[j]["company"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();
            }
            return dt1;

        }

        private void SaveValue()
        {


            DataTable dt = (DataTable)Session["tblover"];
            DataTable dt2 = (DataTable)Session["tbloverabsnt"];


            int rowindex;



            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":
                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double dedday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtAdj")).Text.Trim());
                        string remarks = Convert.ToString(((TextBox)this.grvAdjDay.Rows[i].FindControl("txtRemrks")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        dt.Rows[rowindex]["dedday"] = dedday;
                        dt.Rows[rowindex]["remarks"] = remarks;

                    }
                    break;
                case "MPunchAppDay":
                    for (int i = 0; i < this.gvOPunch.Rows.Count; i++)
                    {
                        double dedday = Convert.ToDouble("0" + ((TextBox)this.gvOPunch.Rows[i].FindControl("txtpAdj")).Text.Trim());
                        rowindex = (this.gvOPunch.PageSize) * (this.gvOPunch.PageIndex) + i;
                        dt.Rows[rowindex]["pdedday"] = dedday;

                    }
                    break;
                case "MabsentApp":
                    string comcod = this.GetCompCode();


                    for (int i = 0; i < this.gvmapsapp.Rows.Count; i++)
                    {

                        double absday = Convert.ToDouble(((Label)this.gvmapsapp.Rows[i].FindControl("lblgvabsday")).Text.Trim());

                        double aprday = Convert.ToDouble("0" + ((TextBox)this.gvmapsapp.Rows[i].FindControl("txtabsaprday")).Text.Trim());
                        string remarks = Convert.ToString(((TextBox)this.gvmapsapp.Rows[i].FindControl("abtxtRemrks")).Text.Trim());

                        rowindex = (this.gvmapsapp.PageSize) * (this.gvmapsapp.PageIndex) + i;
                        dt.Rows[rowindex]["aprday"] = aprday;
                        dt.Rows[rowindex]["dedday"] = (absday - aprday);
                        dt.Rows[rowindex]["remarks"] = remarks;

                    }

                    break;

                case "MnthabsentApp":


                    for (int i = 0; i < this.gvMonthAbsent.Rows.Count; i++)
                    {
                        string chkabs = (((CheckBox)gvMonthAbsent.Rows[i].FindControl("chkabs")).Checked) ? "True" : "False";
                        string remarks = ((TextBox)gvMonthAbsent.Rows[i].FindControl("mtxtRemrks")).Text.ToString();
                        rowindex = (this.gvMonthAbsent.PageSize) * (this.gvMonthAbsent.PageIndex) + i;
                        dt2.Rows[rowindex]["chkabs"] = chkabs;
                        dt2.Rows[rowindex]["remarks"] = remarks;
                    }
                    break;


            }
            Session["tblover"] = dt;
            Session["tbloverabsnt"] = dt2;

        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblover"];
            DataTable dt2 = (DataTable)Session["tbloverabsnt"];



            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":
                    this.grvAdjDay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvAdjDay.DataSource = dt;
                    this.grvAdjDay.DataBind();

                    Session["Report1"] = grvAdjDay;
                    if (dt.Rows.Count > 0)
                        ((HyperLink)this.grvAdjDay.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
                case "MPunchAppDay":
                    this.gvOPunch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvOPunch.DataSource = dt;
                    this.gvOPunch.DataBind();
                    break;

                case "MabsentApp":
                    this.gvmapsapp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvmapsapp.DataSource = dt;
                    this.gvmapsapp.DataBind();
                    break;

                case "MnthabsentApp":
                    this.gvMonthAbsent.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());

                    this.gvMonthAbsent.DataSource = dt2;
                    this.gvMonthAbsent.DataBind();
                    break;

            }
        }

        protected void btnUpdateDayAdj_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = this.GetCompCode();
            string monthid = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("yyyyMM");
            bool result = false;
            ///--------------------------------------------------////////////

            //////----------------------------------------------------------/////////////
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string delday = Convert.ToDouble("0" + dt.Rows[i]["delday"]).ToString();
                string aprday = Convert.ToDouble("0" + dt.Rows[i]["aprday"]).ToString();
                double dedday = Convert.ToDouble("0" + dt.Rows[i]["dedday"]);
                string remarks = dt.Rows[i]["remarks"].ToString();
                //if (dedday > 0)
                //{
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPSALADJST", monthid, empid, dedday.ToString(), delday, aprday, remarks, postDat, trmid, sessionid, userid, "", "", "", "", "");
                if (!result)
                    return;
                //  }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
        }
        protected void lbtnTotalDay_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnCalCulationSadj_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;

            switch (comcod)
            {
                case "4101":
                case "4315":
                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((Label)this.grvAdjDay.Rows[i].FindControl("lblgvDelday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                        string remarks = Convert.ToString(((TextBox)this.grvAdjDay.Rows[i].FindControl("txtRemrks")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = redelay / 2;
                        dt.Rows[rowindex]["remarks"] = remarks;

                    }
                    break;

                case "4305":             
                case "5301":

                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((Label)this.grvAdjDay.Rows[i].FindControl("lblgvDelday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = Convert.ToInt32(Convert.ToInt32(redelay) / 3);
                    }

                    break;
            }
            Session["tblover"] = dt;
            this.Data_Bind();
        }



        //protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.GetSection();
        //}
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }
        protected void grvAdjDay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvAdjDay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":
                    this.RptMonLateApp();
                    break;
                case "MPunchAppDay":

                    break;

                case "MabsentApp":

                    break;

                case "MnthabsentApp":

                    break;

            }
        }

        private void RptMonLateApp()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_83_Att.RptMonthlyLateAttn02();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtMonth"] as TextObject;
            rptftdate.Text = "( From " + fromdate + " To " + todate + " )";
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tblover"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptTransList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptTransList();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = "Date: " + fromdate + " To " + todate;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tblEmpstatus"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lbtnTotalP_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void btnUpdatePunch_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetCompCode();
            string monthid = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("yyyyMM");
            bool result = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string opunday = Convert.ToDouble("0" + dt.Rows[i]["opunchday"]).ToString();
                string paprday = Convert.ToDouble("0" + dt.Rows[i]["paprday"]).ToString();
                string pdedday = Convert.ToDouble("0" + dt.Rows[i]["pdedday"]).ToString();
                //if (dedday > 0)
                //{
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPOPUNCH", monthid, empid, opunday, paprday, pdedday, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;
                //  }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }
        protected void lbtnCalCulationPday_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;
            for (int i = 0; i < this.gvOPunch.Rows.Count; i++)
            {
                double Opunday = Convert.ToDouble("0" + ((Label)this.gvOPunch.Rows[i].FindControl("lblgvPunchDay")).Text.Trim());
                double PAprvday = Convert.ToDouble("0" + ((TextBox)this.gvOPunch.Rows[i].FindControl("txtpaprday")).Text.Trim());
                rowindex = (this.gvOPunch.PageSize) * (this.gvOPunch.PageIndex) + i;
                double redelay = Opunday - PAprvday;
                dt.Rows[rowindex]["paprday"] = PAprvday;
                dt.Rows[rowindex]["pdedday"] = Convert.ToInt32(Convert.ToInt32(redelay) / 2);

            }

            Session["tblover"] = dt;
            this.Data_Bind();
        }
        protected void lbtnTotalabs_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            this.Data_Bind();
        }
        protected void btnUpdateAbsent_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            
            Hashtable hst = (Hashtable)Session["tblLogin"];
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetCompCode();
            string monthid = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("yyyyMM");
            bool result = false;
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            foreach (DataRow dr1 in dt.Rows)
            {
                string empid = dr1["empid"].ToString();
                string absday = Convert.ToDouble(dr1["absday"]).ToString();
                string aprday = Convert.ToDouble(dr1["aprday"]).ToString();
                string dedday = Convert.ToDouble(dr1["dedday"]).ToString();
                string remarks = Convert.ToString(dr1["remarks"]);
                //if (dedday > 0)
                //{
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPABSENTADJ", monthid, empid, absday, aprday, dedday, remarks, postDat, trmid, sessionid, userid, "", "", "", "", "");
                if (!result)
                    return;
                //  }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
        }
        //protected void ddlDept_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    this.GetSection();
        //}



        protected void btnUpdateAbsentx_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            
            DataTable dt = (DataTable)Session["tbloverabsnt"];
            string comcod = this.GetCompCode();
            string monthid = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("yyyyMM");
            bool result = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();

            foreach (DataRow dr1 in dt.Rows)
            {

                string app = dr1["chkabs"].ToString().Trim();
                string remarks = dr1["remarks"].ToString().Trim();

                if ((app == "True"))
                {
                    string dayid = Convert.ToDateTime(dr1["cdate"].ToString()).ToString("yyyyMMdd");

                    string empid = dr1["empid"].ToString();
                    string machid = "01";
                    string idcardno = dr1["idcardno"].ToString();
                    string intime = dr1["intime"].ToString();
                    string outtime = dr1["outtime"].ToString();
                    string dedout = "0";
                    string addhour = "0";
                    string addoffhour = "0";
                    string offintime = dr1["offintime"].ToString();
                    string offoutime = dr1["offouttime"].ToString();
                    string lnintime = dr1["lnchintime"].ToString();
                    string lnoutime = dr1["lnchouttime"].ToString();
                    result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOFFTIME", dayid, empid, machid, idcardno, intime, outtime, "", "", dedout, addhour, addoffhour, offintime, offoutime, lnintime, lnoutime, "", remarks, "Ok", userid, postDat, trmid, sessionid);
                }

                else
                {
                    string empid = dr1["empid"].ToString();
                    string frmdate = Convert.ToDateTime(dr1["intime"]).ToString("dd-MMM-yyyy");
                    string absfl = "1";
                    string month = Convert.ToDateTime(dr1["intime"]).ToString("ddMMyyyy").Substring(2, 2);
                    //tring month1 = month.PadLeft(2, '0');
                    string year = ASTUtility.Right(Convert.ToDateTime(dr1["intime"]).ToString("dd-MMM-yyyy"), 4);
                    string monyr = month + year;

                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, frmdate, absfl, monyr, postDat, trmid, sessionid, userid, remarks, "", "", "", "", "", "");

                }

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            }

        }
        protected void gvMonthAbsent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvMonthAbsent.PageIndex = e.NewPageIndex;

            this.Data_Bind();
        }

        protected void LbtnBreakdown_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            this.lbmodalheading.Text = "Monthly Late Approval Details Information. Date :" + this.txtfrmDate.Text.ToString() + " To: " + this.txttoDate.Text.ToString();
            string compname = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";

            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.DropCheck1.SelectedValue.ToString() == "000000000000") ? "%" : this.DropCheck1.SelectedValue.ToString() + "%";

            //string section = "";
            //if ((this.ddlDept.SelectedValue.ToString() != "000000000000"))
            //{
            //    string[] sec = this.DropCheck1.Text.Trim().Split(',');

            //    if (sec[0].Substring(0, 3) == "000")
            //        section = "";
            //    else
            //        foreach (string s1 in sec)
            //            section = section + this.ddlDept.SelectedValue.ToString().Substring(0, 9) + s1.Substring(0, 3);

            //}


            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
            string todesig = this.ddlToDesig.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "EMPLATEATTENDETAILS", compname, frmdate, todate, deptname, section, Empcode, todesig, frmdesig, div);
            if (ds2 == null)
            {
                this.mgvbreakdown.DataSource = null;
                this.mgvbreakdown.DataBind();
                return;
            }
            this.mgvbreakdown.DataSource = ds2.Tables[0];
            this.mgvbreakdown.DataBind();
            Session["Report1"] = mgvbreakdown;
            if (ds2.Tables[0].Rows.Count > 0)
                ((HyperLink)this.mgvbreakdown.HeaderRow.FindControl("mhlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {

            int i;
            if (((CheckBox)this.mgvbreakdown.HeaderRow.FindControl("chkall")).Checked)
            {
                for (i = 0; i < this.mgvbreakdown.Rows.Count; i++)
                {

                    ((CheckBox)this.mgvbreakdown.Rows[i].FindControl("chkack")).Checked = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
                    //this.lblgvdeptandemployeeemp_Click(null, null);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
                }
            }
            else
            {
                for (i = 0; i < this.mgvbreakdown.Rows.Count; i++)
                {

                    if (((CheckBox)this.mgvbreakdown.Rows[i].FindControl("chkack")).Enabled == true)
                    {
                        ((CheckBox)this.mgvbreakdown.Rows[i].FindControl("chkack")).Checked = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseMOdal();", true);

                        //this.lblgvdeptandemployeeemp_Click(null, null);
                    }
                }
            }

        }
        protected void ModalUpdateBtn_Click(object sender, EventArgs e)
        {
            
            string comcod = this.GetCompCode();
            bool result = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();

            for (int j = 0; j < mgvbreakdown.Rows.Count; j++)
            {


                if (((CheckBox)this.mgvbreakdown.Rows[j].FindControl("chkack")).Checked == true)
                {
                    string lateappsta = (((CheckBox)this.mgvbreakdown.Rows[j].FindControl("chkack")).Checked == true) ? "1" : "0";
                    string empid = Convert.ToString(((Label)this.mgvbreakdown.Rows[j].FindControl("mlgvEmpIdAdj")).Text.Trim());

                    string remarks = Convert.ToString(((TextBox)this.mgvbreakdown.Rows[j].FindControl("mTxtremarks")).Text.Trim());
                    string dayid = Convert.ToDateTime(((Label)this.mgvbreakdown.Rows[j].FindControl("mlblgvlateday")).Text).ToString("yyyyMMdd");
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPDATEADJUSTB", dayid, empid, postDat, userid, remarks, lateappsta, "", "", "", "", "");
                }
                // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseMOdal();", true);


                if (!result)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseMOdal();", true);


            }

        }

        protected void lblgvdeptandemployeeemp_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            this.lbmodalheading.Text = "Individual Monthly Late Approval Details Information. Date :" + this.txtfrmDate.Text.ToString() + " To: " + this.txttoDate.Text.ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = ((Label)this.grvAdjDay.Rows[index].FindControl("lgvEmpIdAdj")).Text.ToString(); // "%" + this.txtSrcEmployee.Text.Trim() + "%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "EMPLATEATTENDETAILSINDIVIDUAL", frmdate, todate, Empcode);
            if (ds2 == null)
            {
                this.mgvbreakdown.DataSource = null;
                this.mgvbreakdown.DataBind();
                return;
            }
            this.mgvbreakdown.DataSource = ds2.Tables[0];
            this.mgvbreakdown.DataBind();
            Session["Report1"] = mgvbreakdown;
            if (ds2.Tables[0].Rows.Count > 0)
                ((HyperLink)this.mgvbreakdown.HeaderRow.FindControl("mhlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }





    }
}