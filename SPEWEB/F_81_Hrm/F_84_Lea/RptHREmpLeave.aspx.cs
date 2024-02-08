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
using System.Drawing;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_84_Lea
{
    public partial class RptHREmpLeave : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = date;
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.ShowView();

                ((Label)this.Master.FindControl("lblTitle")).Text = "LEAVE STATUS";
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetEmployee();
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


            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();

            this.ddlSection.SelectedValue = "000000000000";


            this.GetEmployee();


        }

        private void GetEmployee()
        {


            string comcod = this.GetCompCode();
            string emptype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string department = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";


            string txtSProject = "%" + this.txtSrcEmployee.Text + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEMPNAME", emptype, divison, department, section, "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds3.Tables[0];
            this.ddlEmployee.DataBind();
            ds3.Dispose();


        }


        private void ShowView()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EmpLeaveSt":
                    this.txtfromdate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "EmpLeaveSt02":
                    this.txtfromdate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.SelectIndex();
        }
        private void SelectIndex()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EmpLeaveSt":
                    this.ShowLeaveStatus();
                    break;

                case "EmpLeaveSt02"://Leave Report FB
                    this.ShowLeaveStatus02();
                    break;

            }


        }

        private void ShowLeaveStatus()
        {
            this.lblleavesummary.Visible = true;
            this.lblleavesDetails.Visible = true;
            ViewState.Remove("tblleave");
            string comcod = this.GetCompCode();
            string Empid = this.ddlEmployee.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "LEAVESTATUS02", Empid, frmdate, todate, "", "", "", "", "", "");

            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {

                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                this.gvLeavedetails.DataSource = null;
                this.gvLeavedetails.DataBind();
                return;
            }


            ViewState["tblleave"] = ds3.Tables[0];
            this.Data_Bind();

        }

        private void ShowLeaveStatus02()
        {
            string comcod = this.GetCompCode();
            string Empid = this.ddlEmployee.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "LEAVESTATUSFB", Empid, frmdate, todate, "", "", "", "", "", "");

            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {
                this.gvLeaveStatus02.DataSource = null;
                this.gvLeaveStatus02.DataBind();
                return;
            }

            ViewState["tblleave"] = ds3.Tables[0];
            this.Data_Bind();
        }


        private void Data_Bind()
        {
            DataTable dt = ((DataTable)ViewState["tblleave"]).Copy();
            DataTable dt1 = new DataTable();
            DataView dvr = new DataView();
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "EmpLeaveSt":

                    //A. Sales
                    dvr = dt.DefaultView;
                    dvr.RowFilter = ("grp = 'A'");
                    dt1 = dvr.ToTable();
                    this.gvLeaveStatus.DataSource = dt1;
                    this.gvLeaveStatus.DataBind();
                    //this.FooterCalculation(dt1, "gvLeaveStatus");   

                    //B. Collection Summary
                    dvr = dt.DefaultView;
                    dvr.RowFilter = ("grp = 'B'");
                    dt1 = dvr.ToTable();
                    this.gvLeavedetails.DataSource = dt1;
                    this.gvLeavedetails.DataBind();
                    //  this.FooterCalculation(dt1, "gvLeavedetails"); 
                    //C. Cheque In Hand
                    break;

                case "EmpLeaveSt02":
                    this.gvLeaveStatus02.DataSource = dt;
                    this.gvLeaveStatus02.DataBind();
                    break;
            }

        }


        private void FooterCalculation(DataTable dt, string grview)
        {

            if (dt.Rows.Count == 0)
                return;

            switch (grview)
            {
                case "gvLeaveStatus":
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opleave)", "")) ? 0.00
                    : dt.Compute("sum(opleave)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFleaveentitled")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(enleave)", "")) ? 0.00
                          : dt.Compute("sum(enleave)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFleaveenjoy")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(enjleave)", "")) ? 0.00
                          : dt.Compute("sum(enjleave)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLeaveStatus.FooterRow.FindControl("lblgvFleavebal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balleave)", "")) ? 0.00
                          : dt.Compute("sum(balleave)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "gvLeavedetails":
                    ((Label)this.gvLeavedetails.FooterRow.FindControl("lblgvFleavedays")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lvday)", "")) ? 0.00
                         : dt.Compute("sum(lvday)", ""))).ToString("#,##0;(#,##0); ");
                    break;



            }


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EmpLeaveSt":
                    this.PrintEmpLvStatus();
                    break;

                case "EmpLeaveSt02":
                    this.PrintEmpLeavStatus02();
                    break;

            }


        }

        private void PrintEmpLvStatus()
        {
            DataTable dt = (DataTable)ViewState["tblleave"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmployee.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string subtitle = "Period: " + this.txtfromdate.Text + " To " + this.txttodate.Text;
            string userinf = ASTUtility.Concat(comname, username, printdate);
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEMPDETAILSINFO", empid, todate, "", "", "", "", "", "", "");

            if (ds.Tables[0].Rows.Count == 0 || ds == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            DataTable dt1 = ds.Tables[0];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpLeavStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptEmpLeavStatus", list, null, null);
            Rpt1.SetParameters(new ReportParameter("comnam", comname));
            Rpt1.SetParameters(new ReportParameter("subtitle", subtitle));
            Rpt1.SetParameters(new ReportParameter("userinf", userinf));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            string jondate = Convert.ToDateTime(dt1.Rows[0]["joindate"]).ToString("dd-MMM-yyyy");
            string lstrtdat = Convert.ToDateTime(dt1.Rows[0]["lstrtdat"]).ToString("dd-MMM-yyyy");
            Rpt1.SetParameters(new ReportParameter("idcardno", dt1.Rows[0]["idcardno"].ToString()));
            Rpt1.SetParameters(new ReportParameter("companyname", dt1.Rows[0]["companyname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empname", dt1.Rows[0]["empname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("desig", dt1.Rows[0]["desig"].ToString()));
            Rpt1.SetParameters(new ReportParameter("section", dt1.Rows[0]["section"].ToString()));
            Rpt1.SetParameters(new ReportParameter("joindate", jondate));
            Rpt1.SetParameters(new ReportParameter("serlength", dt1.Rows[0]["serlength"].ToString()));
            Rpt1.SetParameters(new ReportParameter("lstrtdat", lstrtdat));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintEmpLeavStatus02()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string empid = this.ddlEmployee.SelectedValue.ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOYEEALLADDRESS", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            string empId = ds1.Tables[0].Rows[0]["idcard"].ToString();
            string empNameEng = ds1.Tables[0].Rows[0]["empname"].ToString();
            string empNameBn = ds1.Tables[0].Rows[0]["empnamebn"].ToString();
            string empJoinDate = ds1.Tables[0].Rows[0]["jointdat"].ToString();
            string empDept = ds1.Tables[0].Rows[0]["deptdesceng"].ToString();
            string empDesig = ds1.Tables[0].Rows[0]["desigeng"].ToString();
            string empLine = ds1.Tables[0].Rows[0]["linedesc"].ToString();
            string leaveYear = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyy");
            //For Emp Type wise company name and address
            string wrkstattion = this.ddlWstation.SelectedValue.ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1> list1 = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1>)Session["hrcompnameadd"];
            string compName = list1.FindAll(l => l.actcode == wrkstattion)[0].hrcomname;
            string compAdd = list1.FindAll(l => l.actcode == wrkstattion)[0].hrcomadd;
            DataTable dt = (DataTable)ViewState["tblleave"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpLeavStatus02>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptEmpLeavStatus02", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", compName));
            Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Leave Status Report(Individual)"));
            Rpt1.SetParameters(new ReportParameter("empCode", ""));
            Rpt1.SetParameters(new ReportParameter("empId", empId));
            Rpt1.SetParameters(new ReportParameter("empNameEng", empNameEng));
            Rpt1.SetParameters(new ReportParameter("empNameBn", "( " + empNameBn + " )"));
            Rpt1.SetParameters(new ReportParameter("empJoinDate", Convert.ToDateTime(empJoinDate).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("empDept", empDept));
            Rpt1.SetParameters(new ReportParameter("empDesig", empDesig));
            Rpt1.SetParameters(new ReportParameter("empLine", empLine));
            Rpt1.SetParameters(new ReportParameter("leaveYear", leaveYear));
            Rpt1.SetParameters(new ReportParameter("txtFooter", printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvLeaveStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                Label Description = (Label)e.Row.FindControl("lblgvDescription");
                Label opnleave = (Label)e.Row.FindControl("lblgvopnleave");
                Label leaveentitled = (Label)e.Row.FindControl("lblgvleaveentitled");
                Label leaveenjoy = (Label)e.Row.FindControl("lblgvleaveenjoy");
                Label leavebal = (Label)e.Row.FindControl("lblgvleavebal");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "51AAA")
                {


                    Description.Font.Bold = true;
                    opnleave.Font.Bold = true;
                    leaveentitled.Font.Bold = true;
                    leaveenjoy.Font.Bold = true;
                    leavebal.Font.Bold = true;
                    Description.Style.Add("text-align", "right");
                }

            }

        }
        protected void gvLeavedetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                Label Description = (Label)e.Row.FindControl("lblgvDescriptionld");

                Label leavedays = (Label)e.Row.FindControl("lblgvleavedays");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "51AAA")
                {


                    Description.Font.Bold = true;
                    leavedays.Font.Bold = true;
                    Description.Style.Add("text-align", "right");
                }

            }

        }
        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployee();
        }

        protected void gvLeaveStatus02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                if (grp == "")
                {
                    return;
                }
                if (grp == "00000")
                {
                    e.Row.Font.Bold = true;
                }

            }
        }
    }

}