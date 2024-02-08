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

namespace SPEWEB.F_81_Hrm.F_85_Lon
{
    public partial class EmpLoanStatus : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE LOAN STATUS";
                this.GetAllOrganogramList();
                this.GetWorkStation();

                this.GetCompName();
                this.lblmsg.Visible = false;
                this.GetDepartment();


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
        private void GetCompName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string userid = hst["usrid"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string txtDepartment ="%"+ this.txtSrcDept.Text.Trim() + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME", txtDepartment, userid, "", "", "", "", "", "", "");
            //this.ddlDeptName.DataTextField = "actdesc";
            //this.ddlDeptName.DataValueField = "actcode";
            //this.ddlDeptName.DataSource = ds1.Tables[0];
            //this.ddlDeptName.DataBind();
        }
        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        private void GetDepartment()
        {
            if (this.ddlCompany.Items.Count == 0)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = this.txtDept.Text + "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, userid, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = ds1.Tables[0];
            this.ddlDept.DataBind();
            this.ddlDept_SelectedIndexChanged(null, null);

            //string comcod = this.GetComeCode();
            //string txtDepartment = this.ddlDeptName.SelectedValue.ToString().Substring(0, 2) + "%";
            //string dept = "%" + this.txtSrcDepartment.Text + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", txtDepartment, dept, "", "", "", "", "", "", "");
            //this.ddlDepartment.DataTextField = "actdesc";
            //this.ddlDepartment.DataValueField = "actcode";
            //this.ddlDepartment.DataSource = ds1.Tables[0];
            //this.ddlDepartment.DataBind();

        }

        private void GetSectionName()
        {
            //string comcod = this.GetCompCode();
            //string txtSProject = "%" + this.txtsrcsection.Text.Trim() + "%";
            //string Dept = this.ddlDept.SelectedValue.ToString();
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", Dept, txtSProject, "", "", "", "", "", "", "");
            //this.ddlSection.DataTextField = "sectionname";
            //this.ddlSection.DataValueField = "section";
            //this.ddlSection.DataSource = ds1.Tables[0];
            //this.ddlSection.DataBind();

        }

        protected void ddlDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionName();
        }

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void ibtnFindDepartment_Click(object sender, EventArgs e)
        {
            //this.GetDepartment();
            this.GetCompName();

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.lblmsg.Visible = false;
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.empLoanStatus();
            //this.lblmsg.Text = "";
        }

        private void empLoanStatus()
        {
            Session.Remove("tbloan");
            string comcod = this.GetCompCode();
            string comnam = this.ddlCompany.SelectedValue.Substring(0, 2).ToString();
            string deptname = this.ddlDept.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "EMPLOANSTATUS", date, deptname, comnam, "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");

                this.gvEmpLoanStatus.DataSource = null;
                this.gvEmpLoanStatus.DataBind();
                return;
            }
            Session["tbloan"] = this.HiddenSameData(ds2.Tables[0]);
            var dtln = ds2.Tables[1];



            this.gvEmpLoanStatus.Columns[9].Visible = false;
            this.gvEmpLoanStatus.Columns[10].Visible = false;
            this.gvEmpLoanStatus.Columns[11].Visible = false;
            this.gvEmpLoanStatus.Columns[12].Visible = false;
            this.gvEmpLoanStatus.Columns[13].Visible = false;

            int ro = 9;
            for (int i = 0; i < dtln.Rows.Count; i++)
            {
                this.gvEmpLoanStatus.Columns[ro].Visible = true;
                this.gvEmpLoanStatus.Columns[ro].HeaderText = (string)dtln.Rows[i]["hrgdesc"];
                ro++;
            }

            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbloan"];
            this.gvEmpLoanStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpLoanStatus.DataSource = dt;
            this.gvEmpLoanStatus.DataBind();
            this.FooterCalculation();
        }

        private void FooterCalculation()
        {
            //DataTable tbl = (DataTable)Session["tbloanName"];

            //for (int i = 1; i <= tbl.Rows.Count; i++)
            //{
            //    var lname = "lbh" + i.ToString();
            //    ((Label)this.gvEmpLoanStatus.HeaderRow.FindControl(lname)).Text = (string)tbl.Rows[i - 1]["hrgdesc"];
            //}

            DataTable dt = (DataTable)Session["tbloan"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFLoanamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tloan)", "")) ? 0.00
                    : dt.Compute("sum(tloan)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFPaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ? 0.00
                   : dt.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lblgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ? 0.00
                    : dt.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lbh1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r1)", "")) ? 0.00
                    : dt.Compute("sum(r1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lbh2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r2)", "")) ? 0.00
                    : dt.Compute("sum(r2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lbh3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r3)", "")) ? 0.00
                    : dt.Compute("sum(r3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lbh4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r4)", "")) ? 0.00
                    : dt.Compute("sum(r4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpLoanStatus.FooterRow.FindControl("lbh5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(r5)", "")) ? 0.00
                : dt.Compute("sum(r5)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbloan"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string companyname = ddlCompany.SelectedItem.Text.Trim().Substring(13);
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");


            ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_85_Lon.rptEmpLoanStatus();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = companyname;
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["Asdate"] as TextObject;
            txtccaret.Text = "Date: " + frmdate;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;

            int j;

            secid = dt1.Rows[0]["section"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["section"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["section"].ToString();
                    dt1.Rows[j]["secdesc"] = "";
                }

                else
                {
                    secid = dt1.Rows[j]["section"].ToString();
                }

            }


            return dt1;

        }
        protected void gvEmpLoanStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var hyperLink = e.Row.FindControl("lblgvEmpName") as HyperLink;
                var empid = e.Row.FindControl("lgvEmpId") as Label;
                if (hyperLink != null)
                    hyperLink.NavigateUrl = "~/F_81_Hrm/F_85_Lon/EmpLoanDetails.aspx?Empid=" + empid.Text.ToString();
            }
        }


    }
}