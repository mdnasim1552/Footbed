using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class RptEmpMonthlyAbscent : System.Web.UI.Page
    {

        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Absent Summary";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01"+date.Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.GetAllOrganogramList();
                this.GetCompany();
                this.GetWorkStation();
                this.GetProjectName();
                this.GetJobLocation();
                this.GetLineddl();
                this.GetEmpName();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.PrintMonthwiseAbscent();
        }


        private void PrintMonthwiseAbscent()
        {

            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComeCode();


                //string frmdate = this.txtfromdate.Text.ToString();
                //string todate = this.txttodate.Text.ToString();

                //string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string comnam = (comcod == "5306") ? (hst["comnam"] + " (Unit-2)").ToString() : (hst["comnam"]).ToString();
                string username = hst["username"].ToString();
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

                DataTable dt = (DataTable)Session["monthwiseabscent"];


                //var listBN = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformationBn>();

                var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmpMonthwiseAbscent>();

                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptMonthWiseAbscent", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("printdate", printdate));


                //Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                //Rpt1.SetParameters(new ReportParameter("rpttitle", "Applicant's Part"));
                //Rpt1.SetParameters(new ReportParameter("printFooter", ""));
                Session["Report1"] = Rpt1;



                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception e)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + e.Message + "');", true);
            }


        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ShowValue();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                //this.gvMonthWiseRpt.DataSource = null;
                //this.gvMonthWiseRpt.DataBind();
                return;
            }
        }


        private void ShowValue()
        {
            Session.Remove("monthwiseabscent");
            string comcod = this.GetComeCode();
            string empType = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string Dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "00000" ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
            string fDate = this.txtfromdate.Text;
            string toDate = this.txttodate.Text;
            string empidmulti = "";
            foreach (ListItem item in ddlEmpNameAllInfo.Items)
            {
                if (item.Selected)
                {
                    empidmulti += item.Value;
                }
            }
            DataSet ds = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "LINEWISEABSENT", empType, fDate, toDate, Dept, division, section, joblocation, line, empidmulti);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvMonthWiseRpt.DataSource = null;
                this.gvMonthWiseRpt.DataBind();
                return;
            }
            DataTable dt = ds.Tables[0];
            Session["monthwiseabscent"] = dt;
            this.LoadGrid();
        }




        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
             GetEmpName();
        }



        private void GetEmpName()
        {

            string comcod = this.GetComeCode();
            string emptype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dpt = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string empStatus = this.ddlEmpStatus.SelectedValue.ToString();
            string linecode = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
            DataSet ds3 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPNAME", emptype, dpt, section, "%%", empStatus, linecode, "", "", "");

            if (ds3 == null)
            {
                return;
            }
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds3.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();

            ViewState["empname"] = ds3.Tables[0];
        }



        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            Session["lstOrganoData"] = lst;
        }


        private void GetWorkStation()
        {
            string comcod = this.GetComeCode();
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
            this.GetDivision();
        }

        private void GetDivision()
        {
            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];
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
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];
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
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];
            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);
            //lst1.Add()
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "000000000000";
        }


        private void LoadGrid()
        {
            try
            {

                DataTable dt = (DataTable)Session["monthwiseabscent"];
                this.gvMonthWiseRpt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvMonthWiseRpt.DataSource = dt;
                this.gvMonthWiseRpt.DataBind();

                Session["Report1"] = gvMonthWiseRpt;
                if (dt.Rows.Count > 0)
                    ((HyperLink)this.gvMonthWiseRpt.HeaderRow.FindControl("hlbtnbnkpdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + ex.Message + "');", true);

            }
        }


        protected void gvMonthWiseRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvMonthWiseRpt.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }


        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["monthwiseabscent"];
            int TblRowIndex;
            for (int i = 0; i < this.gvMonthWiseRpt.Rows.Count; i++)
            {
                string lblgvidcard = ((Label)this.gvMonthWiseRpt.Rows[i].FindControl("lblgvidcard")).Text.Trim().ToString();
                string lblgvEmpName = ((Label)this.gvMonthWiseRpt.Rows[i].FindControl("lblgvEmpName")).Text.Trim().ToString();
                string lblgvDesig = ((Label)this.gvMonthWiseRpt.Rows[i].FindControl("lblgvDesig")).Text.Trim().ToString();
                string lblgvworkingday = ((Label)this.gvMonthWiseRpt.Rows[i].FindControl("lblgvworkingday")).Text.Trim().ToString();

                TblRowIndex = (gvMonthWiseRpt.PageIndex) * gvMonthWiseRpt.PageSize + i;

                dt.Rows[TblRowIndex]["idcard"] = lblgvidcard;
                dt.Rows[TblRowIndex]["empname"] = lblgvEmpName;
                dt.Rows[TblRowIndex]["desig"] = lblgvDesig;
                dt.Rows[TblRowIndex]["workingday"] = lblgvworkingday;
            }
            Session["monthwiseabscent"] = dt;
        }








        //private void FooterCalculation(DataTable dt)
        //{
        //    //((Label)this.gvMonthWiseRpt.FooterRow.FindControl("lgvFDr")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dr)", "")) ? 0.00 :
        //    //        dt.Compute("sum(dr)", ""))).ToString("#,##0;(#,##0); ");
        //    //((Label)this.gvMonthWiseRpt.FooterRow.FindControl("lgvFCr")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cr)", "")) ? 0.00 :
        //    //        dt.Compute("sum(cr)", ""))).ToString("#,##0;(#,##0); ");
        //    Session["Report1"] = gvMonthWiseRpt;


        //    ((Label)this.gvMonthWiseRpt.FooterRow.FindControl("lgvOpeningAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opening)", "")) ?
        //                                0 : dt.Compute("sum(opening)", ""))).ToString("#,##0.00;(#,##0.00); ");
        //    ((HyperLink)this.gvMonthWiseRpt.HeaderRow.FindControl("hlbtnbnkpdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        //    //((HyperLink)this.gvMonthWiseRpt.HeaderRow.FindControl("hlbtnbnkpdataExel")).Enabled = true;

        //    //((HyperLink)this.gvMonthWiseRpt.HeaderRow.FindControl("hlbtnbnkpdataExel")).NavigateUrl = "../../RptViewer?PrintOpt=GRIDTOEXCEL";
        //}






        private void ClearScreen()
        {
            //this.txtRefNum.Text = "";
            //this.txtSrinfo.Text = "";
            //this.txtRefNum.Text = "";
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetCompany()
        {
            //Hashtable hst = BDAccSession.Current.tblLogin;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            string UserID = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");

            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);
        }

        private void GetLineddl()
        {
            string comcod = GetComeCode();
            DataSet ds3 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";
            ds3.Dispose();
            // ViewState["tbllineddl"] = ds3.Tables[0];
        }

        private void GetJobLocation()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%%";
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAMEFL", txtSProject, company, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "deptname";
            this.ddlProjectName.DataValueField = "deptid";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }

       

        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

    }
}