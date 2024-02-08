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
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_84_Lea
{
    public partial class EmpLeaveInfo : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
               
                this.ShowView();
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetJobLocation();
                this.GetLineDDL();
                this.GetLeaveType();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Over all Leave Status";

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            PrintLeaveinfo();
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

        }
        private void GetLineDDL()
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
            ViewState["tbllineddl"] = ds3.Tables[0];
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
        private void GetJobLocation()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);

            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
            this.ddlJobLocation.SelectedValue = "00000";

        }

        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Leave":
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = "01" + date.Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.MultiView1.ActiveViewIndex = 0;
                    break;
            }

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }




        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        private void ShowData()
        {
            Session.Remove("tblover");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string emptype = ((this.ddlWstation.SelectedValue.ToString().Substring(0, 4) == "0000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
            string leaveType = (this.ddlLeaveType.SelectedValue.ToString() == "00000" ? "51" : this.ddlLeaveType.SelectedValue.ToString()) + "%";
            DataSet ds2 = HRData.GetTransInfoNew01(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "EMPLEAVESTATUS", emptype, division, dept, section, frmdate, todate, Empcode, line, joblocation, userid, leaveType);
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                this.gvMonEmpLeave.DataSource = null;
                this.gvMonEmpLeave.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            Session["tblover"] = ds2.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblover"];
            this.gvMonEmpLeave.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMonEmpLeave.DataSource = dt;
            this.gvMonEmpLeave.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblover"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvMonEmpLeave.FooterRow.FindControl("lblgvFCL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cleave)", "")) ? 0 : dt.Compute("sum(cleave)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonEmpLeave.FooterRow.FindControl("lblgvFEL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(enleave)", "")) ? 0 : dt.Compute("sum(enleave)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonEmpLeave.FooterRow.FindControl("lblgvFSL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sleave)", "")) ? 0 : dt.Compute("sum(sleave)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonEmpLeave.FooterRow.FindControl("lblgvFML")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(matlev)", "")) ? 0 : dt.Compute("sum(matlev)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonEmpLeave.FooterRow.FindControl("lblgvFWPL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(wpaylev)", "")) ? 0 : dt.Compute("sum(wpaylev)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonEmpLeave.FooterRow.FindControl("lblgvFTL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trainlev)", "")) ? 0 : dt.Compute("sum(trainlev)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonEmpLeave.FooterRow.FindControl("lblgvFPL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(patelev)", "")) ? 0 : dt.Compute("sum(patelev)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonEmpLeave.FooterRow.FindControl("lblgvFHL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hajjlev)", "")) ? 0 : dt.Compute("sum(hajjlev)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonEmpLeave.FooterRow.FindControl("lblgvFTotL")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(total)", "")) ? 0 : dt.Compute("sum(total)", ""))).ToString("#,##0;(#,##0); ");
        }

        protected void gvMonEmpLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonEmpLeave.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void PrintLeaveinfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comadd = hst["comadd1"].ToString();

            DataTable ds3 = (DataTable)Session["tblover"];

            var lstitem = ds3.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveInfo>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.EmpLeaveInfo", lstitem, null, null);
            rpt1.EnableExternalImages = true;
            string foot = ASTUtility.Concat(compname, username, printdate);

            rpt1.SetParameters(new ReportParameter("comadd", comadd));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Employee Leave Info"));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("footer", foot));



            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}