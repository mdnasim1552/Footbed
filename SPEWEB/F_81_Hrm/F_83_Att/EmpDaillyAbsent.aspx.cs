using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_81_Hrm.C_81_Rec;


namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class EmpDaillyAbsent : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFromDate.Text = Convert.ToDateTime("01"+txtDate.Text.Substring(3)).ToString("dd-MMM-yyyy");
                this.txtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string Type = "";

                if (Request.QueryString.AllKeys.Contains("Type"))
                {

                    Type = this.Request.QueryString["Type"].ToString();
                }
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "AttnAftrAbs") ? "Daily Present After Absent" :
                                                                    (Type == "AttnAftrLeave") ? "Daily Present After Leave" :
                                                                    "EMPLOYEE DAILY ABSENT INFORMATION ";

                this.CommonButton();
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.selected();
                this.GetLineddl();
                this.GetJobLocation();
                this.GetLeaveType();
            }
        }
        public void selected()
        {
            string Type = "";

            if (Request.QueryString.AllKeys.Contains("Type"))
            {
                Type = this.Request.QueryString["Type"].ToString();
            }
            
            if (Type == "AttnAftrAbs")
            {
                this.divDate.Visible = true;
                this.divFromdate.Visible = false;
                this.divToDate.Visible = false;
                this.divLine.Visible = true;
                this.MultiView1.ActiveViewIndex = 1;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            }
            else if(Type == "AttnAftrLeave")
            {
                this.divLine.Visible = true;
                this.divDate.Visible = false;
                this.divFromdate.Visible = true;
                this.divToDate.Visible = true;
                this.divLeaveType.Visible = true;
                this.MultiView1.ActiveViewIndex = 2;
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            }
            else
            {               
                this.MultiView1.ActiveViewIndex = 0;
                this.divDate.Visible = true;
                this.divFromdate.Visible = false;
                this.divToDate.Visible = false;
            }
            
        }
        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;


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
        }
        private void GetJobLocation()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();

        }
        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetLeaveType()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GET_LEAVE_TYPE");
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                return;
            }

            this.ddlLeaveType.DataTextField = "gdesc";
            this.ddlLeaveType.DataValueField = "gcod";
            this.ddlLeaveType.DataSource = ds2.Tables[0];
            this.ddlLeaveType.DataBind();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnFiUpdate_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
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
            this.ddlSection.SelectedValue = "000000000000";
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            string qType = this.Request.QueryString["Type"] ?? "";
            switch (qType)
            {
                case "AttnAftrAbs":
                    this.GetEmpPresntAbsent();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "AttnAftrLeave":
                    this.ShowEmpPrsntAftrLeave();
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                default:
                    this.GetEmpAbsent();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
            }

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string Type = "";

                if (Request.QueryString.AllKeys.Contains("Type"))
                {

                    Type = this.Request.QueryString["Type"].ToString();
                }

                if (Type == "AttnAftrAbs")
                {
                    this.GetEmpPresntAbsent();
                    this.MultiView1.ActiveViewIndex = 1;
                }
                else if (Type == "AttnAftrLeave")
                {
                    if (this.lnkbtnShow.Text == "Ok")
                    {
                        this.lnkbtnShow.Text = "New";
                        this.ShowEmpPrsntAftrLeave();
                        this.MultiView1.ActiveViewIndex = 2;
                        return;
                    }

                    this.gvPrsntAftrLeave.DataSource = null;
                    this.gvPrsntAftrLeave.DataBind();
                    this.lnkbtnShow.Text = "Ok";
                }
                else
                {
                    this.MultiView1.ActiveViewIndex = 0;
                    if (this.lnkbtnShow.Text == "Ok")
                    {
                        this.txtDate.Enabled = false;
                        this.ddlWstation.Visible = false;
                        this.ddlWstation.Visible = true;
                        this.divPageSize.Visible = true;
                        this.lnkbtnShow.Text = "New";
                        this.GetEmpAbsent();
                        return;
                    }

                    this.txtDate.Enabled = true;
                    this.divPageSize.Visible = false;
                    this.gvempabsent.DataSource = null;
                    this.gvempabsent.DataBind();
                    this.lnkbtnShow.Text = "Ok";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }          

        }

        private void ShowEmpPrsntAftrLeave()
        {
            Session.Remove("tblempabsent");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string fromDate = this.txtFromDate.Text.Trim();
            string toDate = this.txtToDate.Text.Trim();
            string company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string depart = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string empline = (this.ddlempline.SelectedValue.ToString() == "00000" ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string leaveType = (this.ddlLeaveType.SelectedValue.ToString() == "00000" ? "" : this.ddlLeaveType.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GET_EMP_PRESENT_AFTER_LEAVE", null, null, null, fromDate, toDate, company, divison, depart, section, empline, Empcode, joblocation, userid, leaveType);
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                this.gvPrsntAftrLeave.DataSource = null;
                this.gvPrsntAftrLeave.DataBind();
                return;
            }

            Session["tblempabsent"] = ds3.Tables[0];
            this.Data_Bind();
            ds3.Dispose();
        }

        private void GetEmpAbsent()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            Session.Remove("tblempabsent");
            string comcod = this.GetCompCode();
            string date = this.txtDate.Text.Trim();
            string company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string depart = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString().Substring(0, 9)) + "%";
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string jobLocation = ((this.ddlJobLocation.SelectedValue.ToString() == "00000") ? "" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "GETEMPABSENT", date, company, Empcode, divison, depart, section, jobLocation, userid, "");
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvempabsent.DataSource = null;
                this.gvempabsent.DataBind();
                return;
            }
            Session["tblempabsent"] = ds2.Tables[0];
            this.Data_Bind();
            ds2.Dispose();


        }
        private void GetEmpPresntAbsent()
        {
            Session.Remove("tblempabsent");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string date = this.txtDate.Text.Trim();
            string company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string depart = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString())+ "%" ;
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string empline = (this.ddlempline.SelectedValue.ToString() == "00000" ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "GETEMPPRESENTABSENT", date, company, Empcode, divison, depart, section, empline, joblocation, userid);
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Daata Found!')", true);
                this.gvPrsAftrAbs.DataSource = null;
                this.gvPrsAftrAbs.DataBind();
                return;
            }
            Session["tblempabsent"] = ds2.Tables[0];
            this.Data_Bind();
            ds2.Dispose();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string deptid = dt1.Rows[0]["deptid"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptid"].ToString() == deptid && dt1.Rows[j]["secid"].ToString() == secid)
                {
                    deptid = dt1.Rows[j]["deptid"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["deptid"] = "";
                    dt1.Rows[j]["secid"] = "";
                    dt1.Rows[j]["deptdesc"] = "";
                    dt1.Rows[j]["section"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["deptid"].ToString() == deptid)
                        dt1.Rows[j]["deptdesc"] = "";
                    if (dt1.Rows[j]["secid"].ToString() == secid)
                        dt1.Rows[j]["section"] = "";


                    deptid = dt1.Rows[j]["deptid"].ToString();
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }
            return dt1;
        }

        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblempabsent"];
                string Type = "";
                if (Request.QueryString.AllKeys.Contains("Type"))
                {
                    Type = this.Request.QueryString["Type"].ToString();
                }
                switch (Type)
                {
                    case "AttnAftrAbs":
                        this.gvPrsAftrAbs.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPrsAftrAbs.DataSource = dt;
                        this.gvPrsAftrAbs.DataBind();
                        break;

                    case "AttnAftrLeave":
                        this.gvPrsntAftrLeave.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvPrsntAftrLeave.DataSource = dt;
                        this.gvPrsntAftrLeave.DataBind();
                        break;

                    default:
                        this.gvempabsent.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvempabsent.DataSource = dt;
                        this.gvempabsent.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
          

        }
       
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void lbtnFiUpdate_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblempabsent"];
            string comcod = this.GetCompCode();
            string Monthid = Convert.ToDateTime(this.txtDate.Text).ToString("MMyyyy"); //year
            string date = this.txtDate.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString(); //empid
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, date, "1", Monthid, "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Is Not Updated');", true);
                    return;
                }
            }


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


        }
        protected void gvempabsent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvempabsent.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Company = this.ddlWstation.SelectedItem.Text.Trim().ToString();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Rpttitle = "EMPLOYEE DAILY ABSENT INFORMATION";
            string type = "";
            if (Request.QueryString.AllKeys.Contains("Type"))
            {
                type = this.Request.QueryString["Type"].ToString();
            }
            DataTable dt = (DataTable)Session["tblempabsent"];
            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpAbsentInf>();
            LocalReport rpt1 = new LocalReport();         

            if (type == "AttnAftrAbs")
            {
                Rpttitle = "Present Report for : " + frmdate;
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpDailyPresentAbdent", lst, null, null);
                rpt1.SetParameters(new ReportParameter("dateft", frmdate));
                rpt1.SetParameters(new ReportParameter("Rpttitle", Rpttitle));
                rpt1.SetParameters(new ReportParameter("Company", Company));
            }
            else if(type == "AttnAftrLeave")
            {
                var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptAttnAftrLeave>();
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDailyAttnAftrLeave", list, null, null);
                rpt1.EnableExternalImages = true;
                rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
                rpt1.SetParameters(new ReportParameter("date", "Date: " + this.txtToDate.Text.Trim()));
                rpt1.SetParameters(new ReportParameter("rptTitle", "After Leave Attendance Status"));
            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptEmpDailyAbdent", lst, null, null);
                rpt1.SetParameters(new ReportParameter("dateft", "Date: " + frmdate));
                rpt1.SetParameters(new ReportParameter("Rpttitle", Rpttitle));
                rpt1.SetParameters(new ReportParameter("Company", Company));
            }

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvPrsntAftrLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPrsntAftrLeave.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvPrsAftrAbs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPrsAftrAbs.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}