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

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class SubsistanceBonus : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                // this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Subsistance Bonus Allowance";
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetYearMonth();
                this.CommonButton();
            }
        }
        public void CommonButton()
        {
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Text = "Save";


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lUpdate_Click);
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            // Iqbal Nayan
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
            string Todate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string section = "";

            string month = this.ddlyearmon.SelectedItem.Text.Trim();
            var lst = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.ECurSubAllowance>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSubsisBonusAllowance", lst, null, null);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("Todate", Todate));
            rpt1.SetParameters(new ReportParameter("section", section));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Executive Subsistance Bonus For The Month of " + month));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
            lst = lst.FindAll(x => x.actcode == "940100000000");

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

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%");
            string div = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%");
            string deptmane = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%");
            string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "EMPSUBSISTANCEBONUSALLOWANCE", Company, MonthId, date, div, deptmane, Section, "Bonus", "", "");
            if (ds2 == null)
            {
                this.gvCarSub.DataSource = null;
                this.gvCarSub.DataBind();
                return;
            }
            Session["tblover"] = ds2.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblover"];
            //this.gvCarSub.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvCarSub.DataSource = dt;
            this.gvCarSub.DataBind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            int j;
            secid = dt1.Rows[0]["secid"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }

            return dt1;

        }
        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];

            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlyearmon.DataBind();
            //this.ddlyearmon.DataBind();
            //string txtdate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMMM-yyyy");
            ds1.Dispose();
        }

        protected void gvCarSub_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCarSub.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        private void SaveValue()
        {
            int rowindex;
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblover"];
            for (int i = 0; i < this.gvCarSub.Rows.Count; i++)
            {

                double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvCarSub.Rows[i].FindControl("gvpersubbons")).Text.Replace("%", "").Trim());
                double bsal = Convert.ToDouble("0" + ((Label)this.gvCarSub.Rows[i].FindControl("txtgvsubbonus")).Text.Trim());
                double bonamt = bsal * 0.01 * perbonus;
                rowindex = (this.gvCarSub.PageSize) * (this.gvCarSub.PageIndex) + i;
                dt.Rows[rowindex]["perbon"] = perbonus;
                dt.Rows[rowindex]["bonamt"] = bonamt;
            }
            Session["tblover"] = dt;
        }
        protected void lTotal_Click(object sender, EventArgs e)
        {
            SaveValue();
            this.Data_Bind();
        }


        protected void lUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string MonthId = this.ddlyearmon.Text.Trim();
            string bondate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string refno = dt.Rows[i]["secid"].ToString();
                string desid = dt.Rows[i]["desigid"].ToString();
                string bsal = Convert.ToDouble(dt.Rows[i]["bsal"]).ToString();
                string perbon = Convert.ToDouble(dt.Rows[i]["perbon"]).ToString();
                string gsalary = Convert.ToDouble(dt.Rows[i]["gsalary"]).ToString();
                string bonamt = Convert.ToDouble(dt.Rows[i]["bonamt"]).ToString();
                string duration = Convert.ToDouble(dt.Rows[i]["duration"]).ToString();
                string cash = Convert.ToDouble(dt.Rows[i]["bonamt"]).ToString();
                string carallow = Convert.ToDouble(dt.Rows[i]["carallow"]).ToString();
                string suballowance = Convert.ToDouble(dt.Rows[i]["suballowance"]).ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTSUBSISTANCEBONUSALLOWANCE", MonthId, empid, refno, desid, bsal, gsalary, perbon, duration, bonamt, carallow, suballowance, cash, bondate);
                if (!result)
                    return;
            }
            //this.lblmsg.Visible = true;
            //this.lblmsg.Text = "Updated Successfully";
        }
    }
}