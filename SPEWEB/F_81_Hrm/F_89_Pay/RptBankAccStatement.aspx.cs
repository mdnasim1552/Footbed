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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptBankAccStatement : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.RdoBtnAct.SelectedIndex = 0;
                this.GetMonth();
                this.GetBankName();
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetJobLocation();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Bank A/C Statement";
            }
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
        private void GetWorkStation()
        {

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
            Session["lstwrkstation"] = lst;
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

            this.ddlWstation_SelectedIndexChanged(null, null);

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
            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
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
        private void GetMonth()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "4301":
                case "4305":
                    this.txtDate.Text = System.DateTime.Today.ToString("yyyyMM");
                    break;

                default:
                    this.txtDate.Text = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
                    break;

            }

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetBankName()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETBANKNAME", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1.Tables[0];
            this.ddlBankName.DataBind();
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "New")
            {
                this.lnkbtnShow.Text = "Ok";
                this.divChkEmp.Visible = false;
                this.divAddEmp.Visible = false;
                this.chkAddEmp.Checked = false;
                this.gvBankAccStatmnt.DataSource = null;
                this.gvBankAccStatmnt.DataBind();
                return;

            }

            this.lnkbtnShow.Text = "New";
            this.divChkEmp.Visible = true;
            this.ShowBankAccStatement();
        }

        private void ShowBankAccStatement()
        {
            Session.Remove("tblover");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string bankCode = this.ddlBankName.SelectedValue.ToString();
            string date = this.txtDate.Text.Trim();
            string comnam = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string DepCode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string empStatus = this.RdoBtnAct.SelectedValue.ToString();
            string jobLocation = this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87%" : this.ddlJobLocation.SelectedValue.ToString() + "%";
            DataSet ds2 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "EMP_BANK_ACC_STATEMENT", null, null, null, date, bankCode, comnam, divison, DepCode, section, empStatus, jobLocation, userid, "", "", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvBankAccStatmnt.DataSource = null;
                this.gvBankAccStatmnt.DataBind();
                return;
            }

            DataTable dt = this.HiddenSameData(ds2.Tables[0]);
            Session["tblover"] = dt;

            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt)
        {
            string deptid = dt.Rows[0]["deptid"].ToString();
            string secid = dt.Rows[0]["secid"].ToString();

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["deptid"].ToString() == deptid && dt.Rows[i]["secid"].ToString() == secid)
                {
                    dt.Rows[i]["deptname"] = "";
                    dt.Rows[i]["section"] = "";

                }
                else
                {
                    if (dt.Rows[i]["deptid"].ToString() == deptid)
                        dt.Rows[i]["deptname"] = "";

                    if (dt.Rows[i]["secid"].ToString() == secid)
                        dt.Rows[i]["section"] = "";
                }

                deptid = dt.Rows[i]["deptid"].ToString();
                secid = dt.Rows[i]["secid"].ToString();
            }

            return dt;
        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblover"];
            this.gvBankAccStatmnt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvBankAccStatmnt.DataSource = dt;
            this.gvBankAccStatmnt.DataBind();
            //For Details Report
            if (this.chkRptDetails.Checked)
            {
                this.gvBankAccStatmnt.Columns[1].Visible = true;
                this.gvBankAccStatmnt.Columns[2].Visible = true;
                this.gvBankAccStatmnt.Columns[6].Visible = true;
                this.gvBankAccStatmnt.Columns[8].Visible = true;
            }
            else
            {
                this.gvBankAccStatmnt.Columns[1].Visible = false;
                this.gvBankAccStatmnt.Columns[2].Visible = false;
                this.gvBankAccStatmnt.Columns[6].Visible = false;
                this.gvBankAccStatmnt.Columns[8].Visible = false;
            }

            if (dt.Rows.Count != 0)
            {
                Session["Report1"] = gvBankAccStatmnt;
                ((HyperLink)this.gvBankAccStatmnt.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintBankAccStatement();
        }

        private void PrintBankAccStatement()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string year = this.txtDate.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));
            string title = "";
            //Filter Only With A/C No 
            DataTable dt = (DataTable)Session["tblover"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "acno <> ''";
            var list = dv.ToTable().DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.BankStatement>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptBankAccStatement", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Lab Test for the month of " + month + " " + year));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("txtFooter", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvBankAccStatmnt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBankAccStatmnt.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {

            if (chkAddEmp.Checked)
            {
                this.divAddEmp.Visible = true;
                DataTable dt1 = (DataTable)Session["tblover"];
                Session.Remove("tblemp");
                this.CreateDataTable();
                DataTable dt = (DataTable)Session["tblemp"];
                this.ddlEmployee.Items.Clear();
                foreach (DataRow dr1 in dt1.Rows)

                {

                    string empid = dr1["empid"].ToString();
                    if (dt.Select("empid='" + empid + "'").Length == 0)
                    {

                        DataRow dra = dt.NewRow();
                        dra["empid"] = dr1["empid"].ToString();
                        dra["idcard"] = dr1["idcard"].ToString();
                        dra["empname"] = dr1["idcard"].ToString() + "-" + dr1["empname"].ToString();
                        dt.Rows.Add(dra);
                    }

                }

                Session.Remove("tblover");
                Session.Remove("tbladdEmpstatus");
                DataTable dt2 = dt1.Copy();
                Session["tbladdEmpstatus"] = dt2;
                DataTable dt3 = dt1.Clone();
                Session["tblover"] = dt3;

                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = dt;
                this.DataBind();

                //GriedView Bind
                this.Data_Bind();

            }
            else
            {
                this.divAddEmp.Visible = false;
                this.gvBankAccStatmnt.DataSource = null;
                this.gvBankAccStatmnt.DataBind();
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

                DataTable dt = (DataTable)Session["tblover"];
                DataTable dtadd = (DataTable)Session["tbladdEmpstatus"];
                string empid = this.ddlEmployee.SelectedValue.ToString();
                DataRow[] dr1 = dt.Select("empid='" + empid + "'");
                if (dr1.Length == 0)
                {
                    DataRow[] dra = dtadd.Select("empid='" + empid + "'");
                    dt.ImportRow(dra[0]);
                }
                else
                {
                    string empdetails = "Card: " + dr1[0]["idcard"].ToString() + " - " + dr1[0]["empname"].ToString() + " - already Existed!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + empdetails + "');", true);
                }

                DataView dv = dt.DefaultView;
                dv.Sort = ("secid,idcard");
                Session["tblover"] = dv.ToTable();
                this.Data_Bind();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
    }
}