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

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptDayWiseOTSheet : System.Web.UI.Page
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
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Day Wise OT Sheet";
                GetJobLocation();
                GetWorkStation();
                GetAllOrganogramList();
                this.GetLineDDL();

                //Test

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent eventPFL-000020660
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "New")
            {
                this.lnkbtnShow.Text = "Ok";
                this.divChkEmp.Visible = false;
                this.divAddEmp.Visible = false;
                this.chkAddEmp.Checked = false;
                this.gvDayWiseOT.DataSource = null;
                this.gvDayWiseOT.DataBind();
                return;

            }

            this.lnkbtnShow.Text = "New";
            this.divChkEmp.Visible = true;
            this.ShowData();

           
        }
        private void ShowData()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string userid= hst["usrid"].ToString();
                string ddlEmpName = this.ddlEmpName.SelectedValue.ToString() == "" ? "%" : this.ddlEmpName.SelectedValue.ToString() + "%";
                string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
                string Company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
                string deptCode = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
                string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
                string joblocation = ((this.ddlJobLocation.SelectedValue.ToString() == "00000") ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = (this.ddlempline.SelectedValue== "00000"?"": this.ddlempline.SelectedValue) + "%";
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPT_DAYWISE_OT_SHEET", txtfromdate, txttodate, ddlEmpName, Company, deptCode, division, section, joblocation, userid,line);
                if (ds1 == null || ds1.Tables[0].Rows.Count==0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No OT Found!');", true);
                    return;
                }

                Session["tblDayWiseOT"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
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

            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }
        private void GetWorkStation()
        {

            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            switch (comcod)
            {
                //FB & Footbed Footwear OT Emp. Only
                case "5305":
                    lst = lst.FindAll(x => (x.actcode.Substring(0,4) == "0000") || (x.actcode.Substring(0,4) == "9403") || (x.actcode.Substring(0,4) == "9414"));
                    break;
                case "5306":
                    lst = lst.FindAll(x => x.actcode.Substring(0,4) == "0000" || x.actcode.Substring(0,4) == "9408" || x.actcode.Substring(0,4) == "9416");
                    break;

                default:
                    lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
                    break;
            }

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
        private void GetEmpName()
        {
            string comcod = this.GetComCode();
            string company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string deptcode = (ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string dtcode = (ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : ddlSection.SelectedValue.ToString() + "%";
            string txtSEmployee = "%" + "%";
            string resignstatus = (this.BtnChckResign.Checked == true) ? "RESIGN" : "ALL";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPNAME", company, dtcode, txtSEmployee, section, deptcode, resignstatus, "", "", "");
            if (ds3 == null)
            {
                string msg = "No Data Found";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + msg + "');", true);
                return;
            }
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
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

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblDayWiseOT"];
            this.gvDayWiseOT.PageSize =Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDayWiseOT.DataSource = dt;
            this.gvDayWiseOT.DataBind();
            this.FooterCalculation();
        }
        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblDayWiseOT"];
            if (dt.Rows.Count == 0)
                return;
            

            Int32 Minute = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(totalmin)", "")) ?
                               0 : dt.Compute("sum(totalmin)", "")));
            int txtHrsFrac = Convert.ToInt32(Minute / 60);
            double txtMinFrac = Minute % 60;
            double totalHrs = txtHrsFrac + txtMinFrac * 0.01;
            ((Label)this.gvDayWiseOT.FooterRow.FindControl("lgvFTotalot")).Text = totalHrs.ToString("#,##0.00;(#,##0.00); ");



    }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintDayWiseOTSheet();
        }

        private void PrintDayWiseOTSheet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string rptDt = " For The " + txtfromdate;
            DataTable dt = (DataTable)Session["tblDayWiseOT"];
            //Int32 Minute = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(totalmin)", "")) ?
            //                   0 : dt.Compute("sum(totalmin)", "")));
            //int txtHrsFrac = Convert.ToInt32(Minute / 60);
            //double txtMinFrac = Minute % 60;
           // double totalHrs = txtHrsFrac + txtMinFrac * 0.01;

            double totalHrs = Convert.ToDouble("0"+((Label)this.gvDayWiseOT.FooterRow.FindControl("lgvFTotalot")).Text.Trim());

            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.RptDayWiseOTSheet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptDayWiseOTSheet", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", rptDt));
            Rpt1.SetParameters(new ReportParameter("totalHrs", totalHrs.ToString("#,##0.00;(#,##0.00); ")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Over Time Report"));
            Rpt1.SetParameters(new ReportParameter("txtFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
       
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void ddlJobLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void imgbtnEmpName_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnChckResign_CheckedChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        protected void gvDayWiseOT_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDayWiseOT.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddEmp.Checked)
            {
                this.divAddEmp.Visible = true;
                DataTable dt = (DataTable)Session["tblDayWiseOT"];
                //Concat ID Card & Name
                this.CreateDataTable();
                DataTable dt1 = (DataTable)Session["tblemp"];
                foreach (DataRow dr1 in dt.Rows)
                {
                    string empid = dr1["empid"].ToString();
                    if (dt1.Select("empid='" + empid + "'").Length == 0)
                    {
                        DataRow dra = dt1.NewRow();
                        dra["empid"] = dr1["empid"].ToString();
                        dra["idcard"] = dr1["idcardno"].ToString();
                        dra["empname"] = dr1["idcardno"].ToString() + "-" + dr1["empname"].ToString();
                        dt1.Rows.Add(dra);
                    }

                }

                Session.Remove("tblDayWiseOT");
                Session.Remove("tbladdemppay");
                DataTable dt2 = dt.Clone();
                DataTable dt3 = dt.Copy();
                Session["tblDayWiseOT"] = dt2;
                Session["tbladdemppay"] = dt3;

                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = dt1;
                this.DataBind();

                this.Data_Bind();
            }
            else
            {
                this.divAddEmp.Visible = false;
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

        protected void lnkbtnAddEmp_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = (DataTable)Session["tbladdemppay"];
                DataTable dt1 = (DataTable)Session["tblDayWiseOT"];
                string empId = this.ddlEmployee.SelectedValue.ToString();
                DataRow[] dr = dt1.Select("empid='" + empId + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt1.NewRow();
                    dr1["empid"] = this.ddlEmployee.SelectedValue.ToString();
                    dr1["rowid"] = dt.Select("empid='" + empId + "'")[0]["rowid"];
                    dr1["idcardno"] = dt.Select("empid='" + empId + "'")[0]["idcardno"];
                    dr1["empname"] = dt.Select("empid='" + empId + "'")[0]["empname"];
                    dr1["desigid"] = dt.Select("empid='" + empId + "'")[0]["desigid"];
                    dr1["empdesig"] = dt.Select("empid='" + empId + "'")[0]["empdesig"];
                    dr1["deptid"] = dt.Select("empid='" + empId + "'")[0]["deptid"];
                    dr1["empdept"] = dt.Select("empid='" + empId + "'")[0]["empdept"];
                    dr1["sectionid"] = dt.Select("empid='" + empId + "'")[0]["sectionid"];
                    dr1["empsection"] = dt.Select("empid='" + empId + "'")[0]["empsection"];
                    dr1["linecode"] = dt.Select("empid='" + empId + "'")[0]["linecode"];
                    dr1["fline"] = dt.Select("empid='" + empId + "'")[0]["fline"];
                    dr1["offouttime"] = dt.Select("empid='" + empId + "'")[0]["offouttime"];
                    dr1["intime"] = dt.Select("empid='" + empId + "'")[0]["intime"];
                    dr1["outtime"] = dt.Select("empid='" + empId + "'")[0]["outtime"];
                    dr1["cardot"] = dt.Select("empid='" + empId + "'")[0]["cardot"];
                    dr1["extraot"] = dt.Select("empid='" + empId + "'")[0]["extraot"];
                    dr1["totalot"] = dt.Select("empid='" + empId + "'")[0]["totalot"];
                    dr1["totalmin"] = dt.Select("empid='" + empId + "'")[0]["totalmin"];
                    dr1["totalfot"] = dt.Select("empid='" + empId + "'")[0]["totalfot"];

                    dt1.Rows.Add(dr1);

                }

                DataView dv = dt1.DefaultView;
                dv.Sort = "deptid, sectionid";
                Session["tblDayWiseOT"] = dv.ToTable();
                this.Data_Bind();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }
    }
}