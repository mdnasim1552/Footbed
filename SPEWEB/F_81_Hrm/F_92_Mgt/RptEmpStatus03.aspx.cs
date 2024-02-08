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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class RptEmpStatus03 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)

        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "GradeWiseEmp") ? "Grade Wise Employee Details" : "";
                GetWorkStation();
                GetAllOrganogramList();

                this.SelectType();
                this.GetGrade();

                //EMPLOYEE SALARY INFORMATION
            }

        }



        private void SelectType()
        {
            //string type = this.Request.QueryString["Type"].ToString().Trim();

            //switch (type)
            //{
            //    case "GradeWiseEmp":
            //        this.MultiView1.ActiveViewIndex = 0;
            //        break;




            //}



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

            this.ddlSection_SelectedIndexChanged(null, null);
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


        private void GetGrade()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            string emptype = ASTUtility.Left(this.ddlWstation.SelectedValue.ToString(), 4);

            var lst = getlist.GetEmpGradelist(comcod);
            //if (emptype == "9401")
            //{
            //    lst = lst.FindAll(x => Convert.ToInt32(x.hrgcod) < 0336000);

            //}
            //else if (emptype == "9402")
            //{
            //    lst = lst.FindAll(x => Convert.ToInt32(x.hrgcod) >= 0350000 && Convert.ToInt32(x.hrgcod) < 0360000);
            //    //var result = x.Select(int.Parse)
            //    //  .Count(r => r >= start && r < end);
            //}
            //else
            //{
            //    lst = lst.FindAll(x => Convert.ToInt32(x.hrgcod) >= 0360000 && Convert.ToInt32(x.hrgcod) < 0370000);


            //}

            this.ddlgrade.DataTextField = "hrgdesc";
            this.ddlgrade.DataValueField = "hrgcod";
            this.ddlgrade.DataSource = lst;
            this.ddlgrade.DataBind();
            this.ddlgrade_SelectedIndexChanged(null, null);


        }

        protected void ddlgrade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "GradeWiseEmp":
                    this.ShowGradeWiseEmp();
                    break;



            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }


        private void ShowGradeWiseEmp()
        {
            ViewState.Remove("tblempstatus");
            string comcod = this.GetCompCode();
            string EmpType = this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string div = this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string dept = this.ddlDept.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString();
            string grade = ((this.ddlgrade.SelectedValue.ToString() == "0000") ? "03" : this.ddlgrade.SelectedValue.ToString()) + "%";

            //string CompanyName = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";      
            //string projectcode = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8)) + "%";        
            //string section="";
            //if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000")) 
            //{
            //string [] sec=this.DropCheck1.Text.Trim().Split(',');

            //if (sec[0].Substring(0, 4) == "0000")
            //    section = "";
            //else
            //    foreach (string s1 in sec) 
            //        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + s1.Substring(0, 4);

            //}



            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "RPTGRADEWISEMPSAL", EmpType, div, dept, section, grade, "", "", "", "");

            if (ds3 == null)
            {
                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            ViewState["tblempstatus"] = this.HiddenSameData(dt);
            this.Data_Bind();

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "GradeWiseEmp":

                    string company = dt1.Rows[0]["company"].ToString();
                    string grade = dt1.Rows[0]["grade"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["grade"].ToString() == grade)
                        {
                            //company = dt1.Rows[j]["company"].ToString();
                            //grade = dt1.Rows[j]["grade"].ToString();
                            dt1.Rows[j]["companyname"] = "";
                            dt1.Rows[j]["gradedesc"] = "";
                        }

                        else
                        {


                            if (dt1.Rows[j]["company"].ToString() == company)
                                dt1.Rows[j]["companyname"] = "";

                            //if (dt1.Rows[j]["grade"].ToString() == grade)
                            //    dt1.Rows[j]["gradedesc"] = "";                    
                        }

                        company = dt1.Rows[j]["company"].ToString();
                        grade = dt1.Rows[j]["grade"].ToString();



                    }
                    break;


            }



            return dt1;

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblempstatus"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "GradeWiseEmp":
                    this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpList.DataSource = dt;
                    this.gvEmpList.DataBind();
                    this.FooterCalculation();
                    break;
            }

        }

        private void FooterCalculation()
        {

            DataTable dt = (DataTable)ViewState["tblempstatus"];
            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {


                case "GradeWiseEmp":
                    ((Label)this.gvEmpList.FooterRow.FindControl("lgvFoallows")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptGradeWiseEmp();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;


            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)ViewState["tblempstatus"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintOvertimeSalary()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)ViewState["tblpay"];
            //double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", "")));
            //string date = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            //ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_89_Pay.RptOvertimeSalary();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = this.ddlCompany.SelectedItem.Text;
            //TextObject txtTitle = rptstate.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Allowance for Holiday/Friday Duties (H/O) - Month Of " + date;
            //TextObject txttk = rptstate.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            //txttk.Text = "Amount In Word: " + ASTUtility.Trans(Math.Round(netpay), 2);
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //this.lblprint.Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvgwemp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.gvgwemp.PageIndex = e.NewPageIndex;
            //this.Data_Bind();

        }
        protected void gvEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvEmpList.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }


        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGrade();
        }
    }
}