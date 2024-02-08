using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using SPELIB;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using System.IO;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.Linq;
using DocumentFormat.OpenXml;

namespace SPEWEB.F_05_ProShip
{
    public partial class RptSkilMatrix : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Skill Matrix Report";

                GetWorkStation();
                GetAllOrganogramList();
                GetJobLocation();
                this.CommonButton();
                this.GetLineddl();
                this.getSkillList();
                this.UiModification();
                this.GetSesson();
                this.GeLCName();
                this.GetProcessAndLine();
            }
            var CompInfoBn = ASTUtility.CompInfoBn();

            if(this.Request.QueryString["Type"].ToString().Trim() != "SkilMatrix")
            {
                LinkButtonExportToExcel.Visible = false;
                cellLvlColorTbl.Visible = false;
            }
        }

        public void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = this.Request.QueryString["Type"].ToString() == "WrkWisCapMon" ? true : false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).ToolTip = "Final Update";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).ToolTip = "Total Calculation";

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
        }

        void UiModification()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            if (type == "WrkWisCapMon")
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Work Wise Capacity Monitoring";

                cellGvSkilMat.Visible = false;
                cellListEmpGrd.Visible = false;
                cellListDesig.Visible = false;
                cellListSkil.Visible = false;
                cellListSkilLvl.Visible = false;
                cellSeason.Visible = true;
                cellArtNo.Visible = true;
                cellDdlProcess.Visible = true;
            }

            if (type == "WrkWisCapMon")
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Work Wise Capacity Monitoring";

                cellGvSkilMat.Visible = false;
                cellListEmpGrd.Visible = false;
                cellListDesig.Visible = false;
                cellListSkil.Visible = false;
                cellListSkilLvl.Visible = false;
                cellSeason.Visible = true;
                cellArtNo.Visible = true;
                cellDdlProcess.Visible = true;
            }

            if(type == "RptWrkWisCapMon")
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Work Wise Capacity Monitoring Report";

                cellGvSkilMat.Visible = false;
                cellListEmpGrd.Visible = false;
                cellListDesig.Visible = false;
                cellListSkil.Visible = false;
                cellListSkilLvl.Visible = false;
                cellSeason.Visible = true;
                cellArtNo.Visible = true;
                cellDdlProcess.Visible = true;
                cellEmpType.Visible = false;
                cellDivi.Visible = false;
                cellDept.Visible = false;
                cellSec.Visible = false;
                cellLine.Visible = false;
                cellJobLoc.Visible = false;
            }
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (type == "WrkWisCapMon")
            {
                this.gvWrkWisCapMon.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
                this.LoadGridView(true);
            }
            if (type == "RptWrkWisCapMon")
            {
                this.GVRptWrkWisCapMon.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
                this.LoadGridView(true);
            }
        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";
            }
        }

        private void GeLCName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%1601%";
            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETORDERNO", txtSProject, findseason, "", "", "", "", "", "", "");
            this.ddlLCName.DataTextField = "actdesc1";
            this.ddlLCName.DataValueField = "actcode";
            this.ddlLCName.DataSource = ds1.Tables[0];
            this.ddlLCName.DataBind();
        }

        private void GetProcessAndLine()
        {
            string comcod = GetCompCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GETPROCESS_WISE_LINE", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0].DefaultView.ToTable(true, "prodprocess", "prodprocessdesc");
            this.DdlProcess.DataTextField = "prodprocessdesc";
            this.DdlProcess.DataValueField = "prodprocess";
            this.DdlProcess.DataSource = dt;
            this.DdlProcess.DataBind();
            ViewState["tblprocesslist"] = ds1.Tables[0];
        }

        void getSkillList()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_PLANNING_INFO", "GET_EMP_SKILL_MATRIX", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            this.ddlSkill.DataTextField = "skilldesc";
            this.ddlSkill.DataValueField = "skilcod";
            this.ddlSkill.DataSource = ds1.Tables[1];
            this.ddlSkill.DataBind();
        }

        private void GetEmployeeGrade()
        {
            string comcod = this.GetCompCode();
            string emptype = ddlWstation.SelectedValue;
            emptype = emptype == "000000000000" ? "%" : emptype.Substring(0, 4);
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPT_EMPLOYEE_GRADE", emptype, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            DataTable dt = ds1.Tables[0];
            if (emptype == "9403")
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "[dhrgcod] not in ('0333','0338','0340','0350')";
                dt = dv.ToTable();
            }

            this.empGradeDropDownList.DataTextField = "hrgdesc";
            this.empGradeDropDownList.DataValueField = "dhrgcod";
            this.empGradeDropDownList.DataSource = dt;
            this.empGradeDropDownList.DataBind();
            Session["EmployeeGrade"] = dt;
        }
        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string emptype = ddlWstation.SelectedValue;
            emptype = emptype == "000000000000" ? "%" : emptype.Substring(0, 4);

            List<string> dhrgcodList = new List<string>();
            foreach (ListItem litem in empGradeDropDownList.Items)
            {

                if (litem.Selected)
                {
                    dhrgcodList.Add(litem.Value);

                }

            }
            string dhrgcod = "03%";
            if (dhrgcodList.Count != 0)
            {
                dhrgcod = string.Join(", ", dhrgcodList);
            }


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPT_EMPLOYEE_DESIGNATION", dhrgcod, emptype, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            this.empDesignationDropDownList.DataTextField = "hrgdesc";
            this.empDesignationDropDownList.DataValueField = "hrgcod";
            this.empDesignationDropDownList.DataSource = ds1.Tables[0];
            this.empDesignationDropDownList.DataBind();
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
            if (ds3 == null)
                return;

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
            this.ddlJob.DataTextField = "location";
            this.ddlJob.DataValueField = "loccode";
            this.ddlJob.DataSource = lst;
            this.ddlJob.DataBind();

        }

        protected void LoadGridView(bool isPageSize = false)
        {
             string type = this.Request.QueryString["Type"].ToString().Trim();

            if(type == "RptWrkWisCapMon")
            {
                DataTable ds3 = (DataTable)ViewState["RptWrkWiseMon"];
                GVRptWrkWisCapMon.DataSource = ds3;
                GVRptWrkWisCapMon.DataBind();
                CellRptWrkWisCapMon.Visible = true;
                if (!isPageSize)
                {
                    GVRptWrkMonth.DataSource = null;
                    GVRptWrkMonth.DataBind();
                }
            }
            else
            {
                DataTable ds4 = (DataTable)ViewState["WrkeiseCapMon"];
                gvWrkWisCapMon.DataSource = ds4;
                gvWrkWisCapMon.DataBind();
                cellGvWrkWisCapMon.Visible = true;
                if (!isPageSize)
                {
                    gvrWrkCapDate.DataSource = null;
                    gvrWrkCapDate.DataBind();
                }    
            }
        }

        bool UpdateFlag = false;

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            try
            {

                if (lnkbtnShow.Text == "Ok" || UpdateFlag)
                {
                    UpdateFlag = false;
                    ddlWstation.Enabled = false;
                    ddlDivision.Enabled = false;
                    ddlDept.Enabled = false;
                    ddlSection.Enabled = false;
                    ddlempline.Enabled = false;
                    ddlJob.Enabled = false;
                    DdlSeason.Enabled = false;
                    ddlLCName.Enabled = false;
                    DdlProcess.Enabled = false;
                    lnkbtnShow.Text = "New";

                    this.GetDataInfo();
                }
                else
                {
                    this.gvWrkWisCapMon.EditIndex = -1;

                    ddlWstation.Enabled = true;
                    ddlDivision.Enabled = true;
                    ddlDept.Enabled = true;
                    ddlSection.Enabled = true;
                    ddlempline.Enabled = true;
                    ddlJob.Enabled = true;
                    DdlSeason.Enabled = true;
                    ddlLCName.Enabled = true;
                    DdlProcess.Enabled = true;

                    lblSelectDate.Visible = false;

                    lnkbtnShow.Text = "Ok";

                    ViewState["WrkeiseCapMon"] = null;

                    ViewState["RptWrkWiseMon"] = null;

                    this.LoadGridView();

                    cellddlType.Visible = false;
                }

            }
            catch (Exception ex)
            {

            }

        }
        private void GetDataInfo()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (type == "WrkWisCapMon")
            {
                capacityMonitoring();
                cellddlType.Visible = true;
                ddlType.SelectedIndex = 0;
            }
            else if (type == "RptWrkWisCapMon")
            {
                RptCapacityMonitoring();
            }
            else
            {
                GetSillMatrixdata();
            }
        }

        private void GetSillMatrixdata()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_PLANNING_INFO", "GET_EMP_SKILL_MATRIX", "", "", "", "", "", "", "", "", "");

            for (int i = 0; i < 50; i++)
            {
                this.gvSkilMatrix.Columns[i + 3].Visible = false;
            }

            int indexx = 0;
            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {
                this.gvSkilMatrix.Columns[indexx + 3].Visible = true;
                this.gvSkilMatrix.Columns[indexx + 3].HeaderText = ds1.Tables[1].Rows[i]["skilldesc"].ToString().Trim();
                indexx++;
            }


            ViewState["ExportToExcelFile"] = ds1.Tables[0];
            ViewState["headerList"] = ds1.Tables[1];

            this.gvSkilMatrix.EditIndex = -1;
            gvSkilMatrix.DataSource = ds1.Tables[0];
            gvSkilMatrix.DataBind();
        }

        private void capacityMonitoring(string dayid = "")
        {
            string comcod = this.GetCompCode();
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string Deptid = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string JobLocation = (this.ddlJob.SelectedValue.ToString() == "00000") ? "%" : this.ddlJob.SelectedValue.ToString() + "%";
            string Line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            string ArticleNo = this.ddlLCName.SelectedValue.ToString();
            string Process = this.DdlProcess.SelectedValue.ToString();

            string Dayid = (dayid.Length == 0) ? DateTime.Now.ToString("yyyyMMdd") : dayid;

            DataSet ds4 = HRData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_EMP_WRK_WIS_MON", wstation, division, Deptid, Section, JobLocation, Line, ArticleNo, Process, Dayid);
            if (ds4 == null)
            {
                return;
            }
            else
            {
                ViewState["WrkeiseCapMon"] = ds4.Tables[0];
                gvWrkWisCapMon.DataSource = ds4.Tables[0];
                gvWrkWisCapMon.DataBind();

                DataTable dt1 = ds4.Tables[0].Copy();
                DataRow dr1 = dt1.NewRow();

                dr1["empname"] = "---Select Operator---";
                dr1["empid"] = "000000000000";

                dt1.Rows.InsertAt(dr1, 0);

                ViewState["WrkeiseCapMon2"] = dt1;

                gvrWrkCapDate.DataSource = ds4.Tables[1];
                gvrWrkCapDate.DataBind();

                cellGvWrkWisCapMon.Visible = true;
            }

        }

        private void RptCapacityMonitoring(string dayid = "")
        {
            try
            {
                string comcod = this.GetCompCode();

                string ArticleNo = this.ddlLCName.SelectedValue.ToString();
                string Process = this.DdlProcess.SelectedValue.ToString();

                string Dayid = (dayid.Length == 0) ? "" : dayid;

                DataSet ds4 = HRData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "EMP_WRK_WIS_MON_RPRT", ArticleNo, Process, Dayid);

                if (ds4 == null)
                {
                    return;
                }
                else
                {
                    cellGvSkilMat.Visible = false;
                    cellGvWrkWisCapMon.Visible = false;

                    ViewState["RptWrkWiseMon"] = ds4.Tables[0];

                    GVRptWrkWisCapMon.DataSource = ds4.Tables[0];
                    GVRptWrkWisCapMon.DataBind();

                    if (dayid == "")
                    {
                        string selectedDayID = Convert.ToDateTime(ds4.Tables[1].Rows[0]["dayid"]).ToString("dd-MMM-yyyy");
                        ViewState["CapacityDate"] = selectedDayID;
                    }

                    GVRptWrkMonth.DataSource = ds4.Tables[1];
                    GVRptWrkMonth.DataBind();

                    CellRptWrkWisCapMon.Visible = true;
                }
            }
            catch (Exception ex)
            {

            } 
        }

        public int Datediffday1(DateTime dtto, DateTime dtfrm)
        {

            int year, mon, day;
            year = dtto.Year - dtfrm.Year;
            mon = dtto.Month - dtfrm.Month;
            day = dtto.Day - dtfrm.Day;
            if (day < 0)
            {

                day = day + 30;
                mon = mon - 1;
                if (mon < 0)
                {
                    mon = mon + 12;
                    year = year - 1;
                }
            }

            if (mon < 0)
            {

                mon = mon + 12;
                year = year - 1;
            }

            return mon;
        }
        private string Companygross()
        {
            string ComGross = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                default:
                    ComGross = ""; ;
                    break;



            }

            return ComGross;
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "RptWrkWisCapMon":

                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    string comnam = hst["comnam"].ToString();
                    string article = ddlLCName.SelectedItem.Text;
                    string section = DdlProcess.SelectedItem.Text;
                    string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                    string capacityDate = ViewState["CapacityDate"].ToString();
                    string footer = "Printed Date: " + DateTime.Now.ToString("dd-MMM-yyyy");
                    DataTable dt = (DataTable)ViewState["RptWrkWiseMon"];
                    var rptlist = dt.DataTableToList<SPEENTITY.C_05_ProShip.EClassPlanning.EWrkCapMon>();
                    LocalReport Rpt1a = new LocalReport();

                    Rpt1a = RptSetupClass.GetLocalReport("R_05_ProShip.RptWrkCapMon", rptlist, null, null);
                    Rpt1a.EnableExternalImages = true;
                    Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
                    Rpt1a.SetParameters(new ReportParameter("title", "Capacity Sheet"));
                    Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));
                    Rpt1a.SetParameters(new ReportParameter("article", "Article - " + article));
                    Rpt1a.SetParameters(new ReportParameter("section", section));
                    Rpt1a.SetParameters(new ReportParameter("capacityDate", capacityDate));
                    Rpt1a.SetParameters(new ReportParameter("footer", footer));
                    Session["Report1"] = Rpt1a;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



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
            Session["hrcompnameadd"] = lst;
            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();
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

        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();

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

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();
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
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
            this.VisibilitiesLineorJob();
            this.GetEmployeeGrade();
            this.ddlGrade_SelectedIndexChanged(null, null);
        }
        private void VisibilitiesLineorJob()

        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "5305":
                case "5306":
                    this.lblline.Visible = true;
                    this.ddlempline.Visible = true;
                    this.lblJob.Visible = true;
                    this.ddlJob.Visible = true;
                    break;

                default:
                    this.lblline.Visible = true;
                    this.ddlempline.Visible = true;
                    break;

            }


        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {

                case "WrkWisCapMon":

                    if(ddlType.SelectedIndex == 0)
                    {
                        for (int i = 0; i < gvWrkWisCapMon.Rows.Count; i++)
                        {
                            TextBox txtCycleTime = (TextBox)gvWrkWisCapMon.Rows[i].FindControl("txtCycleTime");
                            double smv = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvWrkWisCapMon.Rows[i].FindControl("txtCycleTime")).Text.Trim()));

                            double a = smv;
                            double x = Math.Floor(a);

                            if ((a - x) >= 0.60)
                            {
                                smv = ASTUtility.MinuteConversion(a);
                            }

                            txtCycleTime.Text = smv.ToString("#,##0.00");
                        }
                    }
                    else
                    {
                        for (int i = 0; i < grvWWCMOprW.Rows.Count; i++)
                        {
                            TextBox txtCycleTime = (TextBox)grvWWCMOprW.Rows[i].FindControl("txtCycleTimeByOpNam");
                            double smv = Convert.ToDouble(ASTUtility.ExprToValue("0" + (txtCycleTime).Text.Trim()));

                            double a = smv;
                            double x = Math.Floor(a);

                            if ((a - x) >= 0.60)
                            {
                                smv = ASTUtility.MinuteConversion(a);
                            }

                            txtCycleTime.Text = smv.ToString("#,##0.00");
                        }
                    }

                    break;
                default:
                    break;

            }
        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "WrkWisCapMon":
                    this.UpdateWrkWiseMon();
                    break;
                default:
                    break;
            }
        }

        protected void UpdateWrkWiseMon()
        {
            try
            {
                if(ddlType.SelectedIndex == 0)
                {
                    bool result = false;
                    DataTable dt = (DataTable)ViewState["WrkeiseCapMon"];

                    int totalRow = dt.Rows.Count - 1;

                    int pageindex = gvWrkWisCapMon.PageIndex;

                    int pageSize = gvWrkWisCapMon.PageSize;

                    int start = pageSize > totalRow ? 0 : (pageSize * pageindex);

                    int end = pageSize > totalRow + 1 ? totalRow + 1 : (pageSize * pageindex) + pageSize;

                    end = end > totalRow + 1 ? totalRow + 1 : end;

                    for (int i = start; i < end; i++)
                    {
                        string comcod = this.GetCompCode();
                        string mlccod = ddlLCName.SelectedValue;
                        string process = DdlProcess.SelectedValue;
                        string empid = ((Label)gvWrkWisCapMon.Rows[i % pageSize].FindControl("lblEmpId")).Text;
                        string dayid = DateTime.Now.ToString("yyyyMMdd");
                        string procod = ((DropDownList)gvWrkWisCapMon.Rows[i % pageSize].FindControl("ddlPProces")).SelectedValue;

                        TextBox txtCycleTime = (TextBox)gvWrkWisCapMon.Rows[i % pageSize].FindControl("txtCycleTime");
                        double smv = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvWrkWisCapMon.Rows[i % pageSize].FindControl("txtCycleTime")).Text.Trim()));

                        double a = smv;
                        double x = Math.Floor(a);

                        if ((a - x) >= 0.60)
                        {
                            smv = ASTUtility.MinuteConversion(a);
                        }

                        string cycletime = smv.ToString();

                        result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "UPDATE_EMP_WRK_WIS_MON", mlccod, process, empid, dayid, procod, cycletime);
                    }

                    if (result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Updated');", true);
                        UpdateFlag = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);
                        UpdateFlag = true;
                    }
                    this.lnkbtnShow_Click(null, null);
                }
                else
                {
                    bool result = false;
                    DataTable dt = (DataTable)ViewState["ddlProcess"];

                    int totalRow = dt.Rows.Count - 1;

                    int pageindex = grvWWCMOprW.PageIndex;

                    int pageSize = grvWWCMOprW.PageSize;

                    int start = pageSize > totalRow ? 0 : (pageSize * pageindex);

                    int end = pageSize > totalRow + 1 ? totalRow + 1 : (pageSize * pageindex) + pageSize;

                    end = end > totalRow + 1 ? totalRow + 1 : end;

                    for (int i = start; i < end; i++)
                    {
                        string comcod = this.GetCompCode();
                        string mlccod = ddlLCName.SelectedValue;
                        string process = DdlProcess.SelectedValue;

                        string empid = ((DropDownList)grvWWCMOprW.Rows[i % pageSize].FindControl("ddlPProcesByOpNam")).SelectedValue;
                        string dayid = DateTime.Now.ToString("yyyyMMdd");
                        string procod = ((Label)grvWWCMOprW.Rows[i % pageSize].FindControl("lblProcodOprWis")).Text;

                        TextBox txtCycleTime = (TextBox)grvWWCMOprW.Rows[i % pageSize].FindControl("txtCycleTime");
                        double smv = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)grvWWCMOprW.Rows[i % pageSize].FindControl("txtCycleTimeByOpNam")).Text.Trim()));

                        double a = smv;
                        double x = Math.Floor(a);

                        if ((a - x) >= 0.60)
                        {
                            smv = ASTUtility.MinuteConversion(a);
                        }

                        string cycletime = smv.ToString();

                        result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "UPDATE_EMP_WRK_WIS_MON", mlccod, process, empid, dayid, procod, cycletime);
                    }

                    if (result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Updated');", true);
                        UpdateFlag = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);
                        UpdateFlag = true;
                    }
                    this.lnkbtnShow_Click(null, null);
                }
            }
            catch (Exception ex)
            {

            }
        }


        public System.Web.UI.WebControls.SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (System.Web.UI.WebControls.SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }



        private void CreateDataTable()
        {
            if (Session["tblemp"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("empid", Type.GetType("System.String"));
                dt.Columns.Add("empname", Type.GetType("System.String"));
                dt.Columns.Add("idcard", Type.GetType("System.String"));
                Session["tblemp"] = dt;

            }
        }



        protected void gvSkilMatrix_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = "<div class=\"VerticalHeaderText\">" + e.Row.Cells[i].Text + "</div>";
                }
            }
        }

        protected void gvWrkWisCapMon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvWrkWisCapMon.PageIndex = e.NewPageIndex;
            this.LoadGridView(true);
        }

        protected void gvWrkWisCapMon_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                this.gvWrkWisCapMon.EditIndex = e.NewEditIndex;
                this.LoadGridView(true);

                string comcod = this.GetCompCode();

                DropDownList DdlPProcess = ((DropDownList)gvWrkWisCapMon.Rows[e.NewEditIndex].FindControl("ddlPProces"));

                string mlccod = ddlLCName.SelectedValue;
                string proccod = DdlProcess.SelectedValue;

                DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ARTICLE_SMV_INFO", mlccod, proccod, "", "", "", "", "", "", "");

                DdlPProcess.DataTextField = "gdesc";
                DdlPProcess.DataValueField = "gcod";
                DdlPProcess.DataSource = ds1.Tables[0];
                DdlPProcess.DataBind();
                DdlPProcess.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvWrkWisCapMon_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvWrkWisCapMon.EditIndex = -1;
            this.LoadGridView(true);
        }

        protected void gvWrkWisCapMon_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                string mlccod = ddlLCName.SelectedValue;
                string process = DdlProcess.SelectedValue;
                string empid = ((Label)gvWrkWisCapMon.Rows[e.RowIndex].FindControl("lblEmpId")).Text;
                string dayid = DateTime.Now.ToString("yyyyMMdd");
                string procod = ((DropDownList)gvWrkWisCapMon.Rows[e.RowIndex].FindControl("ddlPProces")).SelectedValue;
                string cycletime = "2.6";

                bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "UPDATE_EMP_WRK_WIS_MON", mlccod, process, empid, dayid, procod, cycletime);

                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully Updated');", true);
                    UpdateFlag = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);
                }
                this.gvWrkWisCapMon.EditIndex = -1;
                this.lnkbtnShow_Click(null, null);
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvWrkWisCapMon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();

                string mlccod = ddlLCName.SelectedValue;
                string proccod = DdlProcess.SelectedValue;

                DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ARTICLE_SMV_INFO", mlccod, proccod, "", "", "", "", "", "", "");

                ViewState["ddlProcess"] = ds1.Tables[0];

                DropDownList DdlPProcess = ((DropDownList)e.Row.FindControl("ddlPProces"));

                Label lblProcod = (Label)e.Row.FindControl("lblProcod");

                DdlPProcess.DataTextField = "gdesc";
                DdlPProcess.DataValueField = "gcod";
                DdlPProcess.DataSource = ds1.Tables[0];
                DdlPProcess.DataBind();

                DdlPProcess.SelectedValue = lblProcod.Text;

                DdlPProcess.Visible = true;

            }
            catch (Exception ex)
            {

            }

        }

        protected void lblWrkCapDate_Click(object sender, EventArgs e)
        {
            string dayid = Convert.ToDateTime(((LinkButton)sender).Text).ToString("yyyyMMdd");
            this.capacityMonitoring(dayid);
        }

        protected void GVRptWrkWisCapMon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GVRptWrkWisCapMon.PageIndex = e.NewPageIndex;
            this.LoadGridView(true);
        }

        protected void lblWrkCapDateR_Click(object sender, EventArgs e)
        {
            string dayid = Convert.ToDateTime(((LinkButton)sender).Text).ToString("yyyyMMdd");
            ViewState["CapacityDate"] = ((LinkButton)sender).Text;
            this.RptCapacityMonitoring(dayid);
        }

        protected void LinkButtonExportToExcel_Click(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)ViewState["ExportToExcelFile"];
            DataTable headerList = (DataTable)ViewState["headerList"];

            if (dataTable.Rows.Count == 0 || headerList.Rows.Count == 0)
            {
                return;
            }

            string templateFilePath = Server.MapPath("~/Excel_Files/Template_SKILL_MATRIX.xlsx"); // Path to the template Excel file
            string newFilePath = Server.MapPath("~/Excel_Files/DataTableExport_SKILL_MATRIX.xlsx");  // Change the file name as needed

            File.Copy(templateFilePath, newFilePath, true);


            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(newFilePath, true))
            {
                SheetData sheetData = spreadsheetDocument.WorkbookPart.WorksheetParts.First().Worksheet.GetFirstChild<SheetData>();

                var rowsToDelete = sheetData.Elements<Row>().Skip(3).ToList();

                foreach (var row in rowsToDelete)
                {
                    row.Remove();
                }

                Cell cell = new Cell();
                Row row3 = new Row();

                List<string> columnHeadersList = new List<string>();

                columnHeadersList.Insert(0, "Sl".ToString());
                columnHeadersList.Insert(1, "ID CARD".ToString());
                columnHeadersList.Insert(2, "Name".ToString());

                int index = 3;
                foreach (DataRow row in headerList.Rows)
                {
                    columnHeadersList.Insert(index, row["skilldesc"].ToString());
                    index++;
                }

                foreach (var item in columnHeadersList)
                {
                    cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(item) };
                    row3.Append(cell);
                }

                sheetData.Append(row3);


                List<List<string>> arrayList = new List<List<string>>();

                int k = 1;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    List<string> list1 = new List<string>();

                    list1.Insert(0, k.ToString());
                    list1.Insert(1, dataRow["idcard"].ToString());
                    list1.Insert(2, dataRow["empname"].ToString());

                    int listIndexx = 3;
                    int indexx = 1;

                    for (int i = 0; i < headerList.Rows.Count; i++)
                    {
                        string lvlColor = dataRow["s" + indexx].ToString();

                        
                            if (lvlColor == "bg-green")
                            {
                                list1.Insert(listIndexx, "High");
                            }
                            else if (lvlColor == "bg-yellow")
                            {
                                list1.Insert(listIndexx, "Medium");
                            }
                            else if (lvlColor == "bg-red")
                            {
                                list1.Insert(listIndexx, "Low");
                            }
                            else
                            {
                                list1.Insert(listIndexx, "");
                            }
                        
                        listIndexx++;
                        indexx++;
                    }

                    arrayList.Add(list1);
                    k++;
                }

                foreach (var arrayItem in arrayList)
                {
                    Row row4 = new Row();

                    int nn = 0;

                    foreach (string modifiedItem in arrayItem)
                    {
                        if (int.TryParse(modifiedItem, out int intValue) & nn > 2)
                        {
                            cell = new Cell(new CellValue(intValue.ToString())) { DataType = CellValues.Number };
                        }

                        else if (double.TryParse(modifiedItem, out double doubleValue) & nn > 2)
                        {
                            cell = new Cell(new CellValue(doubleValue.ToString())) { DataType = CellValues.Number };
                        }

                        else
                        {
                            cell = new Cell(new InlineString(new Text(modifiedItem))) { DataType = CellValues.InlineString, };
                        }

                        cell.StyleIndex = (UInt32Value)1U;

                        row4.Append(cell);
                        nn++;
                    }
                    sheetData.Append(row4);
                }
                spreadsheetDocument.WorkbookPart.WorksheetParts.First().Worksheet.Save();
            }


            string script = "window.open('" + ResolveUrl("~/Excel_Files/DataTableExport_SKILL_MATRIX.xlsx") + "','_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "OpenWindow", script, true);
        }

        protected void GVRptWrkMonth_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton btnDate = ((LinkButton)e.Row.FindControl("lblWrkCapDateR"));

                    btnDate.BackColor = System.Drawing.Color.FromArgb(43, 90, 146);

                    string date = ViewState["CapacityDate"].ToString();

                    if (btnDate.Text == ViewState["CapacityDate"].ToString())
                    {
                        lblSelectDate.Visible = true;
                        btnDate.BackColor = System.Drawing.Color.FromArgb(0,46,99);
                        lblSelectDate.Text = "Your Have Selected " + btnDate.Text;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlType.SelectedValue == "1")
            {
                cellGvWrkWisCapMon.Visible = true;
            }
            if (ddlType.SelectedValue == "2")
            {
                DataTable dt1 = (DataTable)ViewState["ddlProcess"];
                grvWWCMOprW.DataSource = dt1;
                grvWWCMOprW.DataBind();

                cellGvWrkWisCapMon.Visible = false;
                cellGrvWWCMOprW.Visible = true;
            }
        }

        protected void grvWWCMOprW_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DataTable dt1 = (DataTable)ViewState["WrkeiseCapMon2"];

                DropDownList DdlPProcessByOpNam = ((DropDownList)e.Row.FindControl("ddlPProcesByOpNam"));

                Label lblProcod = (Label)e.Row.FindControl("lblProcod");

                DdlPProcessByOpNam.DataTextField = "empname";
                DdlPProcessByOpNam.DataValueField = "empid";
                DdlPProcessByOpNam.DataSource = dt1;
                DdlPProcessByOpNam.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}