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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class RptHREmpStatus : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.rbtnlst.SelectedIndex = 0;
                this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                GetWorkStation();
                GetAllOrganogramList();

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "All") ? "Employee Information Report" : "EMPLOYEE STATUS INFORMATION";
                if (type == "All")
                {
                    this.MultiView.ActiveViewIndex = 1;
                    this.rbtnlst.Visible = false;
                }
                else
                {
                    this.MultiView.ActiveViewIndex = 0;
                    this.rbtnlst.Visible = true;
                }
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        public void GetAllOrganogramList()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
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

            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetComCode();
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






        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.Request.QueryString["Type"].ToString() == "All")
            {
                EmployeeInfo();
                return;
            }
            Session.Remove("tblEmpst");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = this.GetComCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            int index = Convert.ToInt32(this.rbtnlst.SelectedIndex.ToString());
            string calltype = "";
            switch (index)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 5:
                case 6:
                case 8:
                    calltype = "RPTEMPSTATUS";
                    break;
                case 4:
                    calltype = "RPTTEREMPSTATUS";
                    break;
                case 7:
                    calltype = "RPTCONFIRMATIONDUE";
                    break;
            }
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", calltype, Company, Department, section, frmdate, todate, division, "", "", "");

            if (ds3 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                this.gvEmpStatus.DataSource = null;
                this.gvEmpStatus.DataBind();
                return;
            }
            Session["tblEmpst"] = ds3.Tables[0];
            this.LoadGrid();
        }

        public void EmployeeInfo()
        {
            Session.Remove("tblEmpinfo");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = this.GetComCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";


            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPLOYEE_ALL_INFORMATION", Company, Department, section, division, "", "", "");

            if (ds3 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                this.GridView1.DataSource = null;
                this.GridView1.DataBind();
                return;
            }
            Session["tblEmpinfo"] = ds3.Tables[0];
            this.GridView1.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.GridView1.DataSource = ds3.Tables[0];
            this.GridView1.DataBind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string company = dt1.Rows[0]["company"].ToString();
            string department = dt1.Rows[0]["department"].ToString();
            string section = dt1.Rows[0]["section"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["department"].ToString() == department && dt1.Rows[j]["section"].ToString() == section)
                {
                    company = dt1.Rows[j]["company"].ToString();
                    department = dt1.Rows[j]["department"].ToString();
                    section = dt1.Rows[j]["section"].ToString();
                    dt1.Rows[j]["companyname"] = "";
                    dt1.Rows[j]["departmentname"] = "";
                    dt1.Rows[j]["sectionname"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["company"].ToString() == company)
                    {
                        dt1.Rows[j]["companyname"] = "";
                    }

                    if (dt1.Rows[j]["department"].ToString() == department)
                    {
                        dt1.Rows[j]["departmentname"] = "";
                    }

                    if (dt1.Rows[j]["section"].ToString() == section)
                    {
                        dt1.Rows[j]["sectionname"] = "";
                    }
                    company = dt1.Rows[j]["company"].ToString();
                    department = dt1.Rows[j]["department"].ToString();
                    section = dt1.Rows[j]["section"].ToString();
                }
            }
            return dt1;


        }


        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblEmpst"];
            DataView dv;
            int index = Convert.ToInt32(this.rbtnlst.SelectedIndex.ToString());
            switch (index)
            {
                case 0:
                    break;

                case 1:
                    dv = dt.DefaultView;
                    dv.RowFilter = ("tecst='yes'");
                    dt = dv.ToTable();
                    break;
                case 2:
                    dv = dt.DefaultView;
                    dv.RowFilter = ("tecst='no' or tecst=''");
                    dt = dv.ToTable();
                    break;

                case 3:
                    string txtdeg = this.txtDegree.Text.Trim() + "%";
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acadeg like '" + txtdeg + "'");
                    dt = dv.ToTable();
                    break;

                case 5:
                    DateTime frmdate = Convert.ToDateTime(this.txtfromdate.Text);
                    DateTime todate = Convert.ToDateTime(this.txttodate.Text);
                    dv = dt.DefaultView;
                    dv.RowFilter = ("joindate >= '" + frmdate + "' and joindate<= '" + todate + "'");
                    dt = dv.ToTable();
                    break;

                case 6:
                    string txtdesig = this.txtDesig.Text.Trim() + "%";
                    dv = dt.DefaultView;
                    dv.RowFilter = ("desig like '" + txtdesig + "'");
                    dt = dv.ToTable();
                    break;
                case 7:
                    break;

                case 8:
                    DateTime frmdate1 = Convert.ToDateTime(this.txtfromdate.Text);
                    DateTime todate1 = Convert.ToDateTime(this.txttodate.Text);
                    dv = dt.DefaultView;
                    dv.RowFilter = ("dobdat >= '" + frmdate1 + "' and dobdat<= '" + todate1 + "'");
                    dt = dv.ToTable();
                    break;
            }

            dt = this.HiddenSameData(dt);
            Session["tblEmpst"] = dt;
            this.gvEmpStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpStatus.DataSource = dt;
            this.gvEmpStatus.DataBind();
            this.gvEmpStatus.Columns[1].Visible = (this.ddlDept.SelectedValue == "000000000000") ? true : false;
            this.gvEmpStatus.Columns[10].Visible = (this.rbtnlst.SelectedIndex == 4) ? true : false;
            this.gvEmpStatus.Columns[11].Visible = (this.rbtnlst.SelectedIndex == 7) ? true : false;
            this.FooterCalculation();

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblEmpst"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvEmpStatus.FooterRow.FindControl("lgvFNetSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //string Type = this.Request.QueryString["Type"].ToString();
            int index = Convert.ToInt32(this.rbtnlst.SelectedIndex.ToString());
            switch (index)
            {

                case 0:
                case 8:
                    this.RptEmpAllStatus();
                    break;

                default:
                    this.Allprints();
                    break;
            }

        }



        private void RptEmpAllStatus()
        {
            if (this.Request.QueryString["Type"].ToString() == "All")
            {
                RptEmployeeInfo();
                return;
            }
            //IQBAL NAYAN
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string Company = this.ddlWstation.SelectedItem.Text.Trim().ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblEmpst"];

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.Empstatus>();

            LocalReport rpt1 = new LocalReport();
           
            switch (comcod)
            {
                case "5305":
                case "5306":
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptHRAllEmpStatusFB", lst, null, null);
                    break;

                default:
                    rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptHRAllEmpStatus", lst, null, null);
                    break;

            }
           
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("Date", "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("Company", Company));
            rpt1.SetParameters(new ReportParameter("Rpttitle", "All Employee list with academic Qualification"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptEmployeeInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string Company = this.ddlWstation.SelectedItem.Text.Trim().ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblEmpinfo"];

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EmpInfo>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptHRAllEmpInfo", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("Date", "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
            rpt1.SetParameters(new ReportParameter("Company", Company));
            rpt1.SetParameters(new ReportParameter("Rpttitle", "All Employee list with academic Qualification"));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void Allprints()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // string prjname = this.ddlDept.SelectedItem.Text.Trim().Substring(13);


            if (this.rbtnlst.SelectedIndex == 0)
            {
                //ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                //TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //CompName.Text = comname;
                //TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                //txtTitle.Text = "All Employee list with academic Qualification";
                //TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                //txtdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
                //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                ////string comcod = this.GetComeCode();
                //string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rpcp.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rpcp; 

                // this.RptEmpAllStatus();
            }
            if (this.rbtnlst.SelectedIndex == 1)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Employee List-Technical Person";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }
            else if (this.rbtnlst.SelectedIndex == 2)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Employee List-Non Technical Person";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }
            else if (this.rbtnlst.SelectedIndex == 3)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Employee List Academic Degree Wise";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }




            else if (this.rbtnlst.SelectedIndex == 4)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptRetiredEmployee();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Retired Employee List";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }

            else if (this.rbtnlst.SelectedIndex == 5)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Joining Date Wise Employee List";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"; ;
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }

            else if (this.rbtnlst.SelectedIndex == 6)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptHRAllEmpStatus();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtTitle = rpcp.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
                txtTitle.Text = "Designation Wise Employee List";
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }

            else if (this.rbtnlst.SelectedIndex == 7)
            {
                ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_83_Att.RptEmpConfirmation();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                CompName.Text = comname;
                TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                txtdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy") + ")";
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource((DataTable)Session["tblEmpst"]);
                string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;

            }

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvEmpStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpStatus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void rbtnlst_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.lblfrmdate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7) || (this.rbtnlst.SelectedIndex == 8);
            this.txtfromdate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7) || (this.rbtnlst.SelectedIndex == 8);
            this.lbltodate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7) || (this.rbtnlst.SelectedIndex == 8);
            this.txttodate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7) || (this.rbtnlst.SelectedIndex == 8);
            this.txtDegree.Visible = (this.rbtnlst.SelectedIndex == 3);
            this.txtDesig.Visible = (this.rbtnlst.SelectedIndex == 6);
            this.lblimg.Visible = (this.rbtnlst.SelectedIndex == 3) || (this.rbtnlst.SelectedIndex == 6);
        }


    }

}