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
using System.IO;
using System.Data.OleDb;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_86_All
{
    public partial class RptEmpEOTSignature : System.Web.UI.Page
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
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "EOTSign") ? "WORKERS DAILY SIGNATURE EOT REPORT "
                     : "";

                this.GetWorkStation();
                this.GetAllOrganogramList();

                //this.GetCompName();           
                this.GetGradeList();
                this.lblmsg.Visible = false;
                this.CommonButton();
                GetLineddl();
            }
        }



        public void CommonButton()
        {
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);


            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetLineddl()
        {
            string comcod = GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "70999";

            ViewState["tbllineddl"] = ds3.Tables[0];
        }
        private void GetWorkStation()

        {
            string qtype = this.Request.QueryString["Type"].ToString().Trim().Trim();

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            if (qtype == "EOTSign")
            {
                this.ddlWstation.SelectedValue = "940300000000";
                this.ddlWstation.Enabled = false;
            }



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
            string comcod = GetComeCode();
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

            string comcod = GetComeCode();
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
            string comcod = GetComeCode();
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

        private void GetGradeList()
        {

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string qtype = this.Request.QueryString["Type"].ToString();

            string emptype = ASTUtility.Left(this.ddlWstation.SelectedValue.ToString(), 4);
            var lst = getlist.GetEmpGradelist(comcod);

            if (emptype == "9401")
            {
                lst = lst.FindAll(x => Convert.ToInt32(x.hrgcod) < 0336000);

            }
            else if (emptype == "9402")
            {
                lst = lst.FindAll(x => Convert.ToInt32(x.hrgcod) >= 0350000 && Convert.ToInt32(x.hrgcod) < 0360000);
                //var result = x.Select(int.Parse)
                //  .Count(r => r >= start && r < end);
            }
            else
            {
                lst = lst.FindAll(x => Convert.ToInt32(x.hrgcod) >= 0360000 && Convert.ToInt32(x.hrgcod) < 0370000);


            }


            this.ddlGrade.DataTextField = "hrgdesc";
            this.ddlGrade.DataValueField = "hrgcod";
            this.ddlGrade.DataSource = lst;
            this.ddlGrade.DataBind();

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EOTSign":
                    this.ShowWorkerEOT();
                    break;


            }
        }

        protected void gvEmpOverTime_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvEmpOverTime.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }


        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string qtype = this.Request.QueryString["Type"].ToString();

            if (this.lnkbtnShow.Text == "Ok")
            {

                this.ddlWstation.Enabled = false;
                this.ddlDivision.Enabled = false;
                this.ddlDept.Enabled = false;
                //this.lblDeptDesc.Visible = true;
                this.ddlSection.Enabled = false;
                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.lnkbtnShow.Text = "New";
                //this.lblCompanyName.Text = this.ddlCompanyName.SelectedItem.Text;
                //this.lblDeptDesc.Text = this.ddlDept.SelectedItem.Text;
                this.SectionView();
                return;
            }
            this.MultiView1.ActiveViewIndex = -1;
            this.ddlDivision.Enabled = true;
            this.ddlDept.Enabled = true;
            this.ddlSection.Enabled = true;
            //this.lblDeptDesc.Visible = false;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.gvEmpOverTime.DataSource = null;
            this.gvEmpOverTime.DataBind();
            this.lnkbtnShow.Text = "Ok";
            //this.lblCompanyName.Text = "";
            this.lblmsg.Text = "";


        }


        private void SectionView()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EOTSign":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowWorkerEOT();
                    break;

            }

        }

        private void ShowWorkerEOT()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string dayid = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");

            string txtdate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string line = (this.ddlempline.SelectedValue.ToString() == "") ? "%" : this.ddlempline.SelectedValue.ToString();

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL03", "RPT_EMPLOYEE_EOT", comnam, dayid, txtdate, divison, deptname, section, line, "");
            if (ds2 == null)
            {
                this.gvEmpOverTime.DataSource = null;
                this.gvEmpOverTime.DataBind();
                return;
            }
            Session["tblover"] = HiddenSameData(ds2.Tables[0]);
            /// ViewState["tblOtDetails"] = ds2.Tables[1];

            this.Data_Bind();
        }

        //txtDate

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblover"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EOTSign":
                    this.gvEmpOverTime.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpOverTime.DataSource = dt;
                    this.gvEmpOverTime.DataBind();
                    // this.FooterCalculation();
                    break;




            }



        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblover"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EOTSign":
                    ((Label)this.gvEmpOverTime.FooterRow.FindControl("lgvFhour")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tohour)", "")) ? 0.00
                        : dt.Compute("sum(tohour)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    break;


            }


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EOTSign":
                    this.RptCarSubAllownce();
                    break;


            }
        }


        private void RptCarSubAllownce()
        {
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            //string hostname = hst["hostname"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.EclassEmpEOT>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptWorkerDailyEot", lst, null, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Workers Daily Signature EOT"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            //BDAccSession.Current.RdlcReport1 = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            //if (dt1.Rows.Count == 0)
            //    return dt1;

            //string secid;
            //string type = this.Request.QueryString["Type"].ToString().Trim();
            //int j;
            //switch (type)
            //{

            //    case "EOTSign":

            //        secid = dt1.Rows[0]["secid"].ToString();
            //        for (j = 1; j < dt1.Rows.Count; j++)
            //        {
            //            if (dt1.Rows[j]["secid"].ToString() == secid)
            //            {
            //                secid = dt1.Rows[j]["secid"].ToString();
            //                dt1.Rows[j]["section"] = "";
            //            }

            //            else
            //            {
            //                secid = dt1.Rows[j]["secid"].ToString();
            //            }

            //        }

            //        break;
            //}        
            return dt1;

        }

    }
}