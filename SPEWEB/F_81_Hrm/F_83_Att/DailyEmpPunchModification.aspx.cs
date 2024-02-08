using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY;
using CrystalDecisions.CrystalReports.Engine;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class DailyEmpPunchModification : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Daily Employee Punch Missing Update";
                this.rbtnMissPunch.SelectedIndex = 1;
                CommonButton();
                GetJobLocation();
                GetWorkStation();
                GetAllOrganogramList();
                this.GetLineDDL();
                this.GetYearMonth();
                ddlyearmon_SelectedIndexChanged(null, null);

            }
        }
        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = true;

            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent eventPFL-000020660
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void GetYearMonth()
        {
            string comcod = this.GetComCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");

            //this.ddlyearmon.DataBind();
            //string txtdate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMMM-yyyy");
            ds1.Dispose();
        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            DataTable dt = new DataTable();
            string comcod = this.GetComCode();
            this.Save_Value();
            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            int rowcount = 0;
            int gvname = 0;
            switch (rbtnindex)
            {
                case "0":
                    dt = (DataTable)ViewState["tblMissInPunch"];
                    rowcount = this.gvMissInPunch.Rows.Count;
                    gvname = (this.gvMissInPunch.PageSize) * (this.gvMissInPunch.PageIndex);
                    break;
                case "1":
                    dt = (DataTable)ViewState["tblMissOutPunch"];
                    rowcount = this.gvMissOutPunch.Rows.Count;
                    gvname = (this.gvMissOutPunch.PageSize) * (this.gvMissOutPunch.PageIndex);
                    break;
                case "2":
                    dt = (DataTable)ViewState["tblDoubtfulPunch"];
                    rowcount = this.gvDoubtfulPunch.Rows.Count;
                    gvname = (this.gvDoubtfulPunch.PageSize) * (this.gvDoubtfulPunch.PageIndex);
                    break;
            }
            for (int j = 0; j < rowcount; j++)
            {
                int index = gvname + j;
                string empid = dt.Rows[index]["empid"].ToString();
                string dayid = dt.Rows[index]["dayid"].ToString();
                string atstatus = dt.Rows[index]["atstatus"].ToString();
                bool result = false;
                string actualintime = dt.Rows[index]["intime"].ToString();
                string remarks = dt.Rows[index]["remarks"].ToString();
                bool check = false;
                bool adjust = true;
                string punchst = "";
                switch (rbtnindex)
                {
                    case "0":

                        check = ((CheckBox)this.gvMissInPunch.Rows[j].FindControl("UpdateChk")).Checked;
                        punchst = "In Punch";
                        break;
                    case "1":
                        check = ((CheckBox)this.gvMissOutPunch.Rows[j].FindControl("UpdategvMissOutPunchChk")).Checked;
                        punchst = "Out Punch";
                        break;
                    case "2":
                        // adjust = true;
                        punchst = "Doubtful Punch";
                        check = ((CheckBox)this.gvDoubtfulPunch.Rows[j].FindControl("UpdategvDoubtfulPunchChk")).Checked;
                        break;
                }

                string outtime = dt.Rows[index]["outtime"].ToString();

                if (check)
                {

                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "UPDATE_MISSED_PUNCH", empid, dayid, actualintime, outtime, atstatus, adjust.ToString(), punchst, userid, postDat, trmid, sessionid, remarks);

                    if (!result)
                    {

                        string msg = HRData.ErrorObject["msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                    }

                }

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update Successfully');", true);

        }
        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            Data_Bind();



        }
        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["tblMissInPunch"];
            DataTable dt1 = (DataTable)ViewState["tblMissOutPunch"];
            DataTable dt2 = (DataTable)ViewState["tblDoubtfulPunch"];
            string intimedp, outimedp;
            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":
                    int rindex;
                    for (int j = 0; j < this.gvMissInPunch.Rows.Count; j++)
                    {

                        rindex = (this.gvMissInPunch.PageSize) * (this.gvMissInPunch.PageIndex) + j;
                        intimedp = Convert.ToDateTime(dt.Rows[rindex]["intime"]).ToString("dd-MMM-yyyy");
                        outimedp = Convert.ToDateTime(((TextBox)this.gvMissInPunch.Rows[j].FindControl("txtgvinDate")).Text).ToString("dd-MMM-yyyy");

                        string actualintime = ((TextBox)this.gvMissInPunch.Rows[j].FindControl("gvmipoffintime")).Text.Trim();
                        string outtime = ((Label)this.gvMissInPunch.Rows[j].FindControl("gvmipoffouttime")).Text.Trim();
                        string atstatus = ((DropDownList)this.gvMissInPunch.Rows[j].FindControl("DdlAtStatus")).SelectedValue.Trim().ToString();
                        string remarks = ((TextBox)this.gvMissInPunch.Rows[j].FindControl("gvtxtmipremarks")).Text.Trim().ToString();

                        dt.Rows[rindex]["intime"] = intimedp + " " + actualintime;
                        dt.Rows[rindex]["outtime"] = outimedp + " " + outtime;
                        dt.Rows[rindex]["atstatus"] = atstatus;
                        dt.Rows[rindex]["remarks"] = remarks;
                    }
                    ViewState["tblMissInPunch"] = dt;
                    break;
                case "1":
                    int aindex;
                    for (int j = 0; j < this.gvMissOutPunch.Rows.Count; j++)
                    {
                        aindex = (this.gvMissOutPunch.PageSize) * (this.gvMissOutPunch.PageIndex) + j;

                        // intimedp = Convert.ToDateTime(dt.Rows[aindex]["intime"]).ToString("dd-MMM-yyyy");
                        outimedp = Convert.ToDateTime(((TextBox)this.gvMissOutPunch.Rows[j].FindControl("txtgvDate")).Text).ToString("dd-MMM-yyyy");

                        string actualouttime = ((TextBox)this.gvMissOutPunch.Rows[j].FindControl("gvmopoffouttime")).Text.Trim();
                        string atstatus = ((DropDownList)this.gvMissOutPunch.Rows[j].FindControl("DdlAtStatus")).SelectedValue.Trim().ToString();
                        string remarks = ((TextBox)this.gvMissOutPunch.Rows[j].FindControl("gvtxtmopremarks")).Text.Trim().ToString();

                        dt1.Rows[aindex]["outtime"] = outimedp + " " + actualouttime;
                        dt1.Rows[aindex]["atstatus"] = atstatus;
                        dt1.Rows[aindex]["remarks"] = remarks;
                    }
                    ViewState["tblMissOutPunch"] = dt1;

                    break;
                case "2":
                    int bindex;
                    for (int j = 0; j < this.gvDoubtfulPunch.Rows.Count; j++)
                    {

                        bindex = (this.gvDoubtfulPunch.PageSize) * (this.gvDoubtfulPunch.PageIndex) + j;
                        intimedp = Convert.ToDateTime(dt2.Rows[bindex]["intime"]).ToString("dd-MMM-yyyy");
                        outimedp = Convert.ToDateTime(((TextBox)this.gvDoubtfulPunch.Rows[j].FindControl("txtgvdoubtDate")).Text).ToString("dd-MMM-yyyy");

                        string doubtouttime = ((TextBox)this.gvDoubtfulPunch.Rows[j].FindControl("gvdbtfpoffouttime")).Text.Trim();
                        string doubtintime = ((TextBox)this.gvDoubtfulPunch.Rows[j].FindControl("gvdbtfpoffintime")).Text.Trim();
                        string atstatus = ((DropDownList)this.gvDoubtfulPunch.Rows[j].FindControl("DdlAtStatus")).SelectedValue.Trim().ToString();
                        string remarks = ((TextBox)this.gvDoubtfulPunch.Rows[j].FindControl("gvtxtdbtfremarks")).Text.Trim().ToString();

                        dt2.Rows[bindex]["intime"] = intimedp + " " + doubtintime;
                        dt2.Rows[bindex]["outtime"] = outimedp + " " + doubtouttime;
                        dt2.Rows[bindex]["atstatus"] = atstatus;
                        dt2.Rows[bindex]["remarks"] = remarks;

                    }
                    ViewState["tblDoubtfulPunch"] = dt2;
                    break;
            }
        }
        private void Save_Same_Value()
        {
            DataTable dt = (DataTable)ViewState["tblMissInPunch"];
            DataTable dt1 = (DataTable)ViewState["tblMissOutPunch"];
            DataTable dt2 = (DataTable)ViewState["tblDoubtfulPunch"];
            string intimedp, outimedp;
            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":


                    int rindex;
                    for (int j = 0; j < this.gvMissInPunch.Rows.Count; j++)
                    {
                        if (((CheckBox)this.gvMissInPunch.Rows[j].FindControl("UpdategvMissInPunchChk")).Checked)
                        {
                            rindex = (this.gvMissInPunch.PageSize) * (this.gvMissInPunch.PageIndex) + j;

                            intimedp = Convert.ToDateTime(dt2.Rows[j]["intime"]).ToString("dd-MMM-yyyy");
                            outimedp = Convert.ToDateTime(((TextBox)this.gvMissInPunch.Rows[0].FindControl("txtgvDate")).Text).ToString("dd-MMM-yyyy");

                            string actualouttime = ((TextBox)this.gvMissInPunch.Rows[0].FindControl("gvmopoffintime")).Text.Trim();
                            //string atstatus = ((DropDownList)this.gvMissInPunch.Rows[j].FindControl("DdlAtStatus")).SelectedValue.Trim().ToString();

                            dt.Rows[rindex]["outtime"] = outimedp + " " + actualouttime;
                            dt.Rows[rindex]["adate"] = outimedp + " " + actualouttime;
                            //dt.Rows[aindex]["atstatus"] = atstatus;
                        }


                    }
                    ViewState["tblMissInPunch"] = dt;
                    this.Data_Bind();
                    break;
                case "1":
                    int aindex;
                    for (int j = 0; j < this.gvMissOutPunch.Rows.Count; j++)
                    {
                        if (((CheckBox)this.gvMissOutPunch.Rows[j].FindControl("UpdategvMissOutPunchChk")).Checked)
                        {
                            aindex = (this.gvMissOutPunch.PageSize) * (this.gvMissOutPunch.PageIndex) + j;

                            //intimedp = Convert.ToDateTime(((TextBox)this.gvMissOutPunch.Rows[0].FindControl("gvmopoffouttime")).Text).ToString("dd-MMM-yyyy");
                            outimedp = Convert.ToDateTime(((TextBox)this.gvMissOutPunch.Rows[0].FindControl("txtgvDate")).Text).ToString("dd-MMM-yyyy");

                            string actualouttime = ((TextBox)this.gvMissOutPunch.Rows[0].FindControl("gvmopoffouttime")).Text.Trim();
                            //string atstatus = ((DropDownList)this.gvMissOutPunch.Rows[j].FindControl("DdlAtStatus")).SelectedValue.Trim().ToString();

                            dt1.Rows[aindex]["outtime"] = outimedp + " " + actualouttime;
                            dt1.Rows[aindex]["adate"] = outimedp + " " + actualouttime;
                            //dt1.Rows[aindex]["atstatus"] = atstatus;
                        }


                    }
                    ViewState["tblMissOutPunch"] = dt1;
                    this.Data_Bind();
                    break;
                case "2":

                    int bindex;
                    for (int j = 0; j < this.gvDoubtfulPunch.Rows.Count; j++)
                    {
                        if (((CheckBox)this.gvDoubtfulPunch.Rows[j].FindControl("UpdategvDoubtfulPunchChk")).Checked)
                        {
                            bindex = (this.gvDoubtfulPunch.PageSize) * (this.gvDoubtfulPunch.PageIndex) + j;

                            intimedp = Convert.ToDateTime(dt2.Rows[j]["intime"]).ToString("dd-MMM-yyyy");
                            string actualintime = ((TextBox)this.gvDoubtfulPunch.Rows[0].FindControl("gvdbtfpoffintime")).Text.Trim();
                            outimedp = Convert.ToDateTime(((TextBox)this.gvDoubtfulPunch.Rows[0].FindControl("txtgvdoubtDate")).Text).ToString("dd-MMM-yyyy");

                            string actualouttime = ((TextBox)this.gvDoubtfulPunch.Rows[0].FindControl("gvdbtfpoffouttime")).Text.Trim();
                            //string atstatus = ((DropDownList)this.gvDoubtfulPunch.Rows[j].FindControl("DdlAtStatus")).SelectedValue.Trim().ToString();

                            dt2.Rows[bindex]["outtime"] = outimedp + " " + actualouttime;
                            dt2.Rows[bindex]["intime"] = intimedp + " " + actualintime;
                            dt2.Rows[bindex]["adate"] = outimedp + " " + actualouttime;
                            //dt2.Rows[aindex]["atstatus"] = atstatus;
                        }


                    }
                    ViewState["tblDoubtfulPunch"] = dt2;
                    this.Data_Bind();
                    break;
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void rbtnMissPunch_SelectedIndexChanged(object sender, EventArgs e)
        {

            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":
                    MultiView1.ActiveViewIndex = 0;
                    break;

                case "1":
                    MultiView1.ActiveViewIndex = 1;
                    break;

                case "2":
                    MultiView1.ActiveViewIndex = 2;
                    break;
            }
            ddlyearmon_SelectedIndexChanged(null, null);
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":
                    MultiView1.ActiveViewIndex = 0;
                    this.GetMissedInPunch();
                    break;

                case "1":
                    MultiView1.ActiveViewIndex = 1;
                    this.GetMissedOutPunch();
                    break;

                case "2":
                    MultiView1.ActiveViewIndex = 2;
                    this.GetDoubtfulPunch();
                    break;
            }
        }

        private void GetMissedInPunch()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usrid = hst["usrid"].ToString();
                string comcod = GetComCode();
                string ddlEmpName = "%";
                string txtfromdate = Convert.ToDateTime(this.ddlDayLst.SelectedValue.Trim()).ToString("dd-MMM-yyyy");
                string txttodate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%";
                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                string AllBranch = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "AllGroup" : "";
                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GET_MISSED_IN_PUNCH", null, null, null, txtfromdate, txttodate, ddlEmpName, Company, deptCode, division, section, joblocation, line, AllBranch, usrid);
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    return;
                }

                ViewState["tblMissInPunch"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = ex.ToString();
            }
        }
        private void GetMissedOutPunch()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usrid = hst["usrid"].ToString();
                string comcod = GetComCode();
                string ddlEmpName = "%";
                string fromdate = Convert.ToDateTime(this.ddlDayLst.SelectedValue.Trim()).ToString("dd-MMM-yyyy");
                string txttodate = Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy");
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%";
                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                string AllBranch = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "AllGroup" : "";
                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GET_MISSED_OUT_PUNCH", null, null, null, fromdate, txttodate, ddlEmpName, Company, deptCode, division, section, joblocation, line, AllBranch, usrid);
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    return;
                }

                ViewState["tblMissOutPunch"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = ex.ToString();
            }
        }
        private void GetDoubtfulPunch()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComCode();
                string usrid = hst["usrid"].ToString();
                string ddlEmpName = "%";
                string fromdate = Convert.ToDateTime(this.ddlDayLst.SelectedValue.Trim()).ToString("dd-MMM-yyyy");
                string txttodate = Convert.ToDateTime(fromdate).ToString("dd-MMM-yyyy");
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%";
                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                string AllBranch = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "AllGroup" : "";
                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GET_DOUBTFUL_PUNCH", null, null, null, fromdate, txttodate, ddlEmpName, Company, deptCode, division, section, joblocation, line, AllBranch, usrid);
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    return;
                }

                ViewState["tblDoubtfulPunch"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = ex.ToString();
            }
        }
        private void GetLineDDL()
        {
            string comcod = GetComCode();
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
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string jobLocCode = "87";
            var lst = getlist.GetCommonHRgcod(comcod, jobLocCode);

            this.ddlJobLocation.DataTextField = "hrgdesc";
            this.ddlJobLocation.DataValueField = "hrgcod";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
            this.ddlJobLocation.SelectedValue = "87002";

        }
        private void GetWorkStation()
        {

            string comcod = GetComCode();
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
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetDivision()
        {
            try
            {
                string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
                string comcod = GetComCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

                if (lst == null)
                    return;
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
            catch (Exception ex)
            {

            }
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetComCode();
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
            this.ddlDept.SelectedValue = "000000000000";
            this.ddlDept_SelectedIndexChanged(null, null);
        }
       
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);

            this.listProject.DataTextField = "actdesc";
            this.listProject.DataValueField = "actcode";
            this.listProject.DataSource = lst1;
            this.listProject.DataBind();
            this.listProject.SelectedValue = "000000000000";

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblMissInPunch"];
            DataTable dt1 = (DataTable)ViewState["tblMissOutPunch"];
            DataTable dt2 = (DataTable)ViewState["tblDoubtfulPunch"];
            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":
                    this.gvMissInPunch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMissInPunch.DataSource = dt;
                    this.gvMissInPunch.DataBind();
                    break;

                case "1":
                    this.gvMissOutPunch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMissOutPunch.DataSource = dt1;
                    this.gvMissOutPunch.DataBind();
                    break;

                case "2":
                    this.gvDoubtfulPunch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvDoubtfulPunch.DataSource = dt2;
                    this.gvDoubtfulPunch.DataBind();
                    break;
            }
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {

                case "0":
                    this.PrintMissInPunch();
                    break;

                case "1":
                    this.PrintMissOutPunch();
                    break;

                case "2":
                    this.PrintDoubtfulPunch();
                    break;
            }
        }

        private void PrintMissInPunch()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rptDt = " For The " + txtfromdate;
            //Get HR Company Name and Address
            string wrkstattion = this.ddlWstation.SelectedValue.ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> list1 = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>)Session["hrcompnameadd"];
            string comnam = list1.FindAll(l => l.actcode == wrkstattion)[0].hrcomname;
            string comadd = list1.FindAll(l => l.actcode == wrkstattion)[0].hrcomadd;
            DataTable dt = (DataTable)ViewState["tblMissInPunch"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptEmpMissPunch>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMissInPunch", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", rptDt));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Missed In Punch"));
            Rpt1.SetParameters(new ReportParameter("txtFooter", ASTUtility.Concat(comnam, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintMissOutPunch()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rptDt = " For The " + txtfromdate;
            //Get HR Company Name and Address
            string wrkstattion = this.ddlWstation.SelectedValue.ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> list1 = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>)Session["hrcompnameadd"];
            string comnam = list1.FindAll(l => l.actcode == wrkstattion)[0].hrcomname;
            string comadd = list1.FindAll(l => l.actcode == wrkstattion)[0].hrcomadd;
            DataTable dt = (DataTable)ViewState["tblMissOutPunch"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptEmpMissPunch>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMissOutPunch", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", rptDt));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Missed Out Punch"));
            Rpt1.SetParameters(new ReportParameter("txtFooter", ASTUtility.Concat(comnam, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintDoubtfulPunch()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rptDt = " For The " + txtfromdate;
            //Get HR Company Name and Address
            string wrkstattion = this.ddlWstation.SelectedValue.ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> list1 = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>)Session["hrcompnameadd"];
            string comnam = list1.FindAll(l => l.actcode == wrkstattion)[0].hrcomname;
            string comadd = list1.FindAll(l => l.actcode == wrkstattion)[0].hrcomadd;
            DataTable dt = (DataTable)ViewState["tblDoubtfulPunch"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptEmpMissPunch>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDoubtfulPunch", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", rptDt));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Doubtful Punch"));
            Rpt1.SetParameters(new ReportParameter("txtFooter", ASTUtility.Concat(comnam, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvMissOutPunch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMissOutPunch.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void ddlJobLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvMissInPunch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMissInPunch.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvDoubtfulPunch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDoubtfulPunch.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvMissInPunch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string atstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "atstatus"));

                DropDownList DDLStatus = (DropDownList)e.Row.FindControl("DdlAtStatus");
                DDLStatus.SelectedValue = atstatus;
            }
        }

        protected void gvMissOutPunch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string atstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "atstatus"));

                DropDownList DDLStatus = (DropDownList)e.Row.FindControl("DdlAtStatus");
                DDLStatus.SelectedValue = atstatus;
            }
        }

        protected void gvDoubtfulPunch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string atstatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "atstatus"));

                DropDownList DDLStatus = (DropDownList)e.Row.FindControl("DdlAtStatus");
                DDLStatus.SelectedValue = atstatus;
            }
        }

        protected void UpdateChkHead_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvMissInPunch.Rows.Count; i++)
            {
                if (((CheckBox)this.gvMissInPunch.HeaderRow.FindControl("UpdateChkHead")).Checked)
                {
                    ((CheckBox)this.gvMissInPunch.Rows[i].FindControl("UpdateChk")).Checked = true;
                }
                else
                {
                    ((CheckBox)this.gvMissInPunch.Rows[i].FindControl("UpdateChk")).Checked = false;
                }
            }
        }

        protected void UpdategvMissOutPunchChkHead_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvMissOutPunch.Rows.Count; i++)
            {
                if (((CheckBox)this.gvMissOutPunch.HeaderRow.FindControl("UpdategvMissOutPunchChkHead")).Checked)
                {
                    ((CheckBox)this.gvMissOutPunch.Rows[i].FindControl("UpdategvMissOutPunchChk")).Checked = true;
                }
                else
                {
                    ((CheckBox)this.gvMissOutPunch.Rows[i].FindControl("UpdategvMissOutPunchChk")).Checked = false;
                }
            }
        }

        protected void UpdategvDoubtfulPunchChkHead_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvDoubtfulPunch.Rows.Count; i++)
            {
                if (((CheckBox)this.gvDoubtfulPunch.HeaderRow.FindControl("UpdategvDoubtfulPunchChkHead")).Checked)
                {
                    ((CheckBox)this.gvDoubtfulPunch.Rows[i].FindControl("UpdategvDoubtfulPunchChk")).Checked = true;
                }
                else
                {
                    ((CheckBox)this.gvDoubtfulPunch.Rows[i].FindControl("UpdategvDoubtfulPunchChk")).Checked = false;
                }
            }
        }

        protected void lnkbtnUpdateAll_Click(object sender, EventArgs e)
        {
            this.Save_Same_Value();
        }

        protected void ddlyearmon_SelectedIndexChanged(object sender, EventArgs e)
        {

            string rbtnindex = this.rbtnMissPunch.SelectedIndex.ToString();
            switch (rbtnindex)
            {
                case "0":
                    MultiView1.ActiveViewIndex = 0;
                    this.GetMissedInPunchDays();
                    break;

                case "1":
                    MultiView1.ActiveViewIndex = 1;
                    this.GetMissedOutPunchDays();
                    break;

                case "2":
                    MultiView1.ActiveViewIndex = 2;
                    this.GetDoubtfulPunchDays();
                    break;
            }
        }
        private void GetMissedInPunchDays()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usrid = hst["usrid"].ToString();
                string comcod = GetComCode();
                string ddlEmpName = "%";
                string txtfromdate = this.ddlyearmon.SelectedValue.Trim().ToString();
                string txttodate = "";
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%";
                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                string AllBranch = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "AllGroup" : "";
                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GET_MISSED_IN_PUNCH_DAYS", null, null, null, txtfromdate, txttodate, ddlEmpName, Company, deptCode, division, section, joblocation, line, AllBranch, usrid);
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    return;
                }

                this.ddlDayLst.DataTextField = "missdate";
                this.ddlDayLst.DataValueField = "missdate";
                this.ddlDayLst.DataSource = ds1.Tables[0];
                this.ddlDayLst.DataBind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }
        private void GetMissedOutPunchDays()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string usrid = hst["usrid"].ToString();
                string comcod = GetComCode();
                string ddlEmpName = "%";
                string txtfromdate = this.ddlyearmon.SelectedValue.Trim().ToString();
                string txttodate = "";
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%";
                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                string AllBranch = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "AllGroup" : "";
                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GET_MISSED_OUT_PUNCH_DAYS", null, null, null, txtfromdate, txttodate, ddlEmpName, Company, deptCode, division, section, joblocation, line, AllBranch, usrid);
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    return;
                }

                this.ddlDayLst.DataTextField = "missdate";
                this.ddlDayLst.DataValueField = "missdate";
                this.ddlDayLst.DataSource = ds1.Tables[0];
                this.ddlDayLst.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }
        private void GetDoubtfulPunchDays()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetComCode();
                string usrid = hst["usrid"].ToString();
                string ddlEmpName = "%";
                string txtfromdate = this.ddlyearmon.SelectedValue.Trim().ToString();
                string txttodate = "";
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string deptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.listProject.SelectedValue.ToString() == "000000000000") ? "%" : this.listProject.SelectedValue.ToString() + "%";
                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                string AllBranch = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "AllGroup" : "";
                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GET_DOUBTFUL_PUNCH_DAYS", null, null, null, txtfromdate, txttodate, ddlEmpName, Company, deptCode, division, section,
                    joblocation, line, AllBranch, usrid);
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    return;
                }

                this.ddlDayLst.DataTextField = "missdate";
                this.ddlDayLst.DataValueField = "missdate";
                this.ddlDayLst.DataSource = ds1.Tables[0];
                this.ddlDayLst.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }
    }
}