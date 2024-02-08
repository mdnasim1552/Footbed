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
using System.Web.Services;
using System.Web.Script.Services;

namespace SPEWEB.F_81_Hrm.F_90_PF
{
    

    public partial class RptAccProFund : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static ProcessAccess accDataajax = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"] ?? "";
                ((Label)this.Master.FindControl("lblTitle")).Text = type == "PFAcc" ? "PF Account" : "PF Account (Yearly)";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-Jan-" + date.Substring(7);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy");

                GetAllOrganogramList();
                GetWorkStation();
                this.GetJobLocation();
                this.GetLineDDL();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        //private void lnkbtnSave_Click(object sender, EventArgs e)
        //{
        //    DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
        //    DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
        //    for (int i = 6; i < 18; i++)
        //    {
        //        if (datefrm > dateto)
        //            break;

        //        this.gvProFund.Columns[i].HeaderText = datefrm.ToString("MMM yy");
        //        datefrm = datefrm.AddMonths(1);

        //    }
        //}
        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"] ?? "";
            switch (type)
            {
                case "PFAcc":
                    this.ShowPFAccData();
                    break;

                default:
                    this.ShowPFYearlyData();
                    break;
            }
            
            this.Data_Bind();
            this.SaveValue();
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetComeCode();
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

        private void GetJobLocation()
        {

            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
       
        protected void lnkOk_Click(object sender, EventArgs e)
        {
            string okbtn = this.LinkButton1.Text;
            if (okbtn == "Ok")
            {
                this.LinkButton1.Text = "New";
                this.txtfrmdate.Enabled = false;
                this.txttodate.Enabled = false;
                string type = this.Request.QueryString["Type"] ?? "";
                switch (type)
                {
                    case "PFAcc":
                        this.ShowPFAccData();
                        break;

                    default:
                        this.ShowPFYearlyData();
                        break;
                }

            }
            else
            {
                this.LinkButton1.Text = "Ok";
                this.txtfrmdate.Enabled = true;
                this.txttodate.Enabled = true;
                this.gvProFund.DataSource = null;
                this.gvProFund.DataBind();
                this.gvProFundAcc.DataSource = null;
                this.gvProFundAcc.DataBind();
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            }
           
        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
            this.GetEmpDatewise();
        }
        private void GetLineDDL()
        {
            string comcod = GetComeCode();
            DataSet ds3 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";
            ViewState["tbllineddl"] = ds3.Tables[0];
        }
        private void GetEmpDatewise()
        {
            string comcod = this.GetComeCode();
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dept = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = this.txtfrmdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();
            //string empStatus = this.ddlEmpStatus.SelectedValue.ToString();
            string empStatus = this.RdoBtnAct.SelectedValue.ToString()=="0"?"1":(this.RdoBtnAct.SelectedValue.ToString() == "1"?"2":"0");
            string linecode = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "" : this.ddlempline.SelectedValue.ToString()) + "%";
            DataSet ds5 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPNAMEDATEWISE", frmdate, todate, emptype, division, division, dept, section, empStatus, linecode);
            if (ds5 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            DataTable dt1 = ds5.Tables[0].Copy();
            DataView dv1 = dt1.DefaultView;
            dt1 = dv1.ToTable().DefaultView.ToTable(true, "empid", "empname");
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = dt1;
            this.ddlEmpNameAllInfo.DataBind();
            //this.ddlEmpNameAllInfo.SelectedValue = "000000000000";
        }
        private void ShowPFAccData()
        {
            Session.Remove("tblprofund");
            Session.Remove("tblprofund2");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string actype = this.rbtList.SelectedIndex.ToString();
            string CompanyName = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string projectcode = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptname = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string empStatus = this.RdoBtnAct.SelectedValue.ToString();
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "SHOWEMPPFFUNDACC", CompanyName, projectcode, deptname, section, 
                            frmdate, todate, actype, empStatus, joblocation, userid);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                this.gvProFund.DataSource = null;
                this.gvProFund.DataBind();
                return;
            }

            Session["tblprofund"] = ds1.Tables[0];
            Session["tblprofund2"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void SaveValue()
        {
            int TblRowIndex;
            DataTable dt = (DataTable)Session["tblprofund"];
            for (int i = 0; i < this.gvProFund.Rows.Count; i++)
            {
                double amt1 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt1")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt1")).Text.Trim());
                double amt2 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt2")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt2")).Text.Trim());
                double amt3 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt3")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt3")).Text.Trim());
                double amt4 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt4")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt4")).Text.Trim());
                double amt5 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt5")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt5")).Text.Trim());
                double amt6 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt6")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt6")).Text.Trim());
                double amt7 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt7")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt7")).Text.Trim());
                double amt8 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt8")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt8")).Text.Trim());
                double amt9 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt9")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt9")).Text.Trim());
                double amt10 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt10")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt10")).Text.Trim());
                double amt11 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt11")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt11")).Text.Trim());
                double amt12 = Convert.ToDouble(((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt12")).Text.Trim()==""?"0": ((TextBox)this.gvProFund.Rows[i].FindControl("lgvamt12")).Text.Trim());
                TblRowIndex = (gvProFund.PageIndex) * gvProFund.PageSize + i;
                dt.Rows[TblRowIndex]["amt1"] = amt1;
                dt.Rows[TblRowIndex]["amt2"] = amt2;
                dt.Rows[TblRowIndex]["amt3"] = amt3;
                dt.Rows[TblRowIndex]["amt4"] = amt4;
                dt.Rows[TblRowIndex]["amt5"] = amt5;
                dt.Rows[TblRowIndex]["amt6"] = amt6;
                dt.Rows[TblRowIndex]["amt7"] = amt7;
                dt.Rows[TblRowIndex]["amt8"] = amt8;
                dt.Rows[TblRowIndex]["amt9"] = amt9;
                dt.Rows[TblRowIndex]["amt10"] = amt10;
                dt.Rows[TblRowIndex]["amt11"] = amt11;
                dt.Rows[TblRowIndex]["amt12"] = amt12;
            }
            Session["tblprofund"] = dt;
        }
        private void ShowPFYearlyData()
        {
            Session.Remove("tblprofund");
            Session.Remove("tblprofund2");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfrmdate.Text.Trim()));
            if (mon!= 11)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Month Should Equal Twelve');", true);
                return;
            }

            string actype = this.rbtList.SelectedIndex.ToString();
            string CompanyName = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string projectcode = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptname = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string empStatus = this.RdoBtnAct.SelectedValue.ToString();
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "SHOWEMPPFFUNDDP", CompanyName, projectcode, deptname, section, frmdate, todate, actype, empStatus, joblocation, userid);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + accData.ErrorObject["Msg"].ToString() + "');", true);
                this.gvProFund.DataSource = null;
                this.gvProFund.DataBind();
                return;
            }

            Session["tblprofund"] = ds1.Tables[0];
            Session["tblprofund2"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            try
            {
                string searchcard = this.txtEmpSearch.Text.Trim();
                DataTable dt =(DataTable)Session["tblprofund"];
                DataView dv = dt.DefaultView;
                dv.RowFilter = "idcard like '%" + searchcard + "%'";
                dt = dv.ToTable();

                DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
                DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());

                string type = this.Request.QueryString["Type"] ?? "";
                switch (type)
                {
                    case "PFAcc":
                        for (int i = 6; i < 66; i++)
                        {
                            this.gvProFundAcc.Columns[i].Visible = false;
                        }
                        for (int i = 6; i < 66; i++)
                        {                           
                            if (datefrm > dateto)
                                break;
                            this.gvProFundAcc.Columns[i].Visible = true;
                            this.gvProFundAcc.Columns[66].Visible = true;
                            this.gvProFundAcc.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                            datefrm = datefrm.AddMonths(1);

                        }

                        this.gvProFundAcc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
                        this.gvProFundAcc.DataSource = dt;
                        this.gvProFundAcc.DataBind();
                        this.FooterCalCulation(dt);
                        Session["tblprofund"] = dt;
                        break;

                    default:
                        for (int i = 6; i < 18; i++)
                        {
                            if (datefrm > dateto)
                                break;

                            this.gvProFund.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                            datefrm = datefrm.AddMonths(1);

                        }

                        this.gvProFund.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
                        this.gvProFund.DataSource = dt;
                        this.gvProFund.DataBind();
                        this.FooterCalCulation(dt);
                        Session["tblprofund"] = dt;
                        if (dt.Rows.Count != 0)
                        {
                            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
                        }
                        break;
                }
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }          

        }

        private void FooterCalCulation(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0)
                    return;

                string type = this.Request.QueryString["Type"] ?? "";
                switch (type)
                {
                    case "PFAcc":
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFOpnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ? 0.00 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt13")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt13)", "")) ? 0.00 : dt.Compute("sum(amt13)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt14")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt14)", "")) ? 0.00 : dt.Compute("sum(amt14)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt15")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt15)", "")) ? 0.00 : dt.Compute("sum(amt15)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt16")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt16)", "")) ? 0.00 : dt.Compute("sum(amt16)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt17")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt17)", "")) ? 0.00 : dt.Compute("sum(amt17)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt18")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt18)", "")) ? 0.00 : dt.Compute("sum(amt18)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt19")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt19)", "")) ? 0.00 : dt.Compute("sum(amt19)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt20")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt20)", "")) ? 0.00 : dt.Compute("sum(amt20)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt21")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt21)", "")) ? 0.00 : dt.Compute("sum(amt21)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt22")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt22)", "")) ? 0.00 : dt.Compute("sum(amt22)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt23")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt23)", "")) ? 0.00 : dt.Compute("sum(amt23)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt24")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt24)", "")) ? 0.00 : dt.Compute("sum(amt24)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt25")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt25)", "")) ? 0.00 : dt.Compute("sum(amt25)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt26")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt26)", "")) ? 0.00 : dt.Compute("sum(amt26)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt27")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt27)", "")) ? 0.00 : dt.Compute("sum(amt27)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt28")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt28)", "")) ? 0.00 : dt.Compute("sum(amt28)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt29")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt29)", "")) ? 0.00 : dt.Compute("sum(amt29)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt30")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt30)", "")) ? 0.00 : dt.Compute("sum(amt30)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt31)", "")) ? 0.00 : dt.Compute("sum(amt31)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt32")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt32)", "")) ? 0.00 : dt.Compute("sum(amt32)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt33")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt33)", "")) ? 0.00 : dt.Compute("sum(amt33)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt34")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt34)", "")) ? 0.00 : dt.Compute("sum(amt34)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt35")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt35)", "")) ? 0.00 : dt.Compute("sum(amt35)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt36")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt36)", "")) ? 0.00 : dt.Compute("sum(amt36)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt37")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt37)", "")) ? 0.00 : dt.Compute("sum(amt37)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt38")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt38)", "")) ? 0.00 : dt.Compute("sum(amt38)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt39")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt39)", "")) ? 0.00 : dt.Compute("sum(amt39)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt40")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt40)", "")) ? 0.00 : dt.Compute("sum(amt40)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt41")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt41)", "")) ? 0.00 : dt.Compute("sum(amt41)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt42")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt42)", "")) ? 0.00 : dt.Compute("sum(amt42)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt43")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt43)", "")) ? 0.00 : dt.Compute("sum(amt43)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt44")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt44)", "")) ? 0.00 : dt.Compute("sum(amt44)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt45")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt45)", "")) ? 0.00 : dt.Compute("sum(amt45)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt46")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt46)", "")) ? 0.00 : dt.Compute("sum(amt46)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt47")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt47)", "")) ? 0.00 : dt.Compute("sum(amt47)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt48")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt48)", "")) ? 0.00 : dt.Compute("sum(amt48)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt49")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt49)", "")) ? 0.00 : dt.Compute("sum(amt49)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt50")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt50)", "")) ? 0.00 : dt.Compute("sum(amt50)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt51")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt51)", "")) ? 0.00 : dt.Compute("sum(amt51)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt52")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt52)", "")) ? 0.00 : dt.Compute("sum(amt52)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt53")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt53)", "")) ? 0.00 : dt.Compute("sum(amt53)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt54")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt54)", "")) ? 0.00 : dt.Compute("sum(amt54)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt55")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt55)", "")) ? 0.00 : dt.Compute("sum(amt55)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt56")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt56)", "")) ? 0.00 : dt.Compute("sum(amt56)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt57")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt57)", "")) ? 0.00 : dt.Compute("sum(amt57)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt58")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt58)", "")) ? 0.00 : dt.Compute("sum(amt58)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt59")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt59)", "")) ? 0.00 : dt.Compute("sum(amt59)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFamt60")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt60)", "")) ? 0.00 : dt.Compute("sum(amt60)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFundAcc.FooterRow.FindControl("lgvFtoam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");

                        Session["Report1"] = gvProFundAcc;
                        ((HyperLink)this.gvProFundAcc.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;

                    default:
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFOpnam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ? 0.00 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvProFund.FooterRow.FindControl("lgvFtoam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");
                        
                        Session["Report1"] = gvProFund;
                        ((HyperLink)this.gvProFund.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }              

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //DataTable dt = (DataTable)Session["tblprofund"];

            //ReportDocument rptpf = new RMGiRPT.R_81_Hrm.R_90_PF.RptMonthWisePF();


            //TextObject rptCname = rptpf.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject rptTxtHead = rptpf.ReportDefinition.ReportObjects["txtHead"] as TextObject;
            //rptTxtHead.Text = (this.rbtList.SelectedIndex == 0) ? "PF Fund Statement" : "AIT Statement";


            //TextObject rptdate = rptpf.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rptdate.Text = "(From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

            //DateTime datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptpf.ReportDefinition.ReportObjects["txtMonth" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}



            //TextObject txtuserinfo = rptpf.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptpf.SetDataSource(dt);
            //Session["Report1"] = rptpf;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        protected void rbtList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.lnkOk_Click(null, null);
        }

        protected void gvProFund_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvProFund.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvProFundAcc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProFundAcc.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"] ?? "";
            switch (type)
            {
                case "PFAcc":
                    this.Data_Bind();
                    break;

                default:
                    this.SaveValue();
                    this.Data_Bind();
                    break;
            }
            
        }

        protected void txtEmpSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblprofund2"];
            Session["tblprofund"] = dt;
            this.Data_Bind();
            //string searchcard = this.txtEmpSearch.Text.Trim();
            //if (searchcard == "")
            //{
            //    //this.lnkOk_Click(null, null);
            //    string type = this.Request.QueryString["Type"] ?? "";
            //    switch (type)
            //    {
            //        case "PFAcc":
            //            this.ShowPFAccData();
            //            break;

            //        default:
            //            this.ShowPFYearlyData();
            //            break;
            //    }
            //}
            //else
            //{
            //    this.Data_Bind();
            //}

        }

        protected void lnkEditUser_Click(object sender, EventArgs e)
        {
            string datefrm = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("yyyyMM");
            string dateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("yyyyMM");
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empid = "";

            string type = this.Request.QueryString["Type"] ?? "";
            switch (type)
            {
                case "PFAcc":
                    empid = ((Label)this.gvProFundAcc.Rows[RowIndex].FindControl("lgvEmpid")).Text.Trim();
                    break;

                default:
                    empid = ((Label)this.gvProFund.Rows[RowIndex].FindControl("lgvEmpid")).Text.Trim();
                    break;
            }

            string comcod = this.GetComeCode();
            DataSet ds = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_SALARYSUMMEY", "VIEWPFUND", datefrm, dateto, empid);
            DataTable dt = ds.Tables[0];
            this.GridEmployeePF.DataSource = dt;
            this.GridEmployeePF.DataBind();
            ((Label)this.GridEmployeePF.FooterRow.FindControl("lgvFPF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openUserModal();", true);
        }
        protected void lbtnSaveUser_Click(object sender, EventArgs e)
        {
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = ((Label)this.GridEmployeePF.Rows[RowIndex].FindControl("lgvcomcod")).Text.Trim();
            string monthid = ((Label)this.GridEmployeePF.Rows[RowIndex].FindControl("lgvmonthid")).Text.Trim();
            string refno = ((Label)this.GridEmployeePF.Rows[RowIndex].FindControl("lgvRefnopf")).Text.Trim();
            string empid = ((Label)this.GridEmployeePF.Rows[RowIndex].FindControl("lgvEmpidpf")).Text.Trim();
            string pf = ((TextBox)this.GridEmployeePF.Rows[RowIndex].FindControl("lgvPF")).Text.Trim();
            pf = pf == "" ? "0" : pf;
            pf = Convert.ToDouble(pf).ToString();         
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openUserModal();", true);
        }

        protected void closemodal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "closeUserModal();", true);
        }

        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SaveData(string comcod, string monthid, string refno, string empid, string pf)//data: JSON.stringify({ comcod: comcod ,monthid: monthid, refno: refno,empid:empid,pf:pf}),
        {
            pf = pf == "" ? "0" : pf;
            pf = Convert.ToDouble(pf).ToString();
            bool result = accDataajax.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_SALARYSUMMEY", "EDITPFFUND", monthid, refno, empid, pf);
            return result.ToString();
        }
    }
}