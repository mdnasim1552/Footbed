using Microsoft.Reporting.WinForms;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptEmpFoodAllowance : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.GetWorkStation();
                this.GetDivision();
                this.GetDeptList();
                this.GetSectionList();
                this.GetJobLocation();
                this.GetEmpLine();
                this.GetMonth();
                this.SelectView();

                string Type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "BreakFast") ? "Breakfast Payment Sheet" : (Type == "NightBill") ? "Night Bill Payment Sheet" :
                   (Type == "TransAllow") ? "Transport Allowance Payment Sheet" : "";

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
        private void GetWorkStation()
        {
            Session.Remove("lstwrkstation");
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation.SelectedValue = "000000000000";

        }
        private void GetDivision()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            var lst = getlist.GetDivision(comcod, wstation);
            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "00000";

        }

        private void GetDeptList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            var lst = getlist.GetDept(comcod, wstation);
            this.ddlDep.DataTextField = "actdesc";
            this.ddlDep.DataValueField = "actcode";
            this.ddlDep.DataSource = lst;
            this.ddlDep.DataBind();
            this.ddlDep.SelectedValue = "00000";

        }

        private void GetSectionList()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            var lst = getlist.GetSection(comcod, wstation);
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "00000";

        }
        private void GetJobLocation()
        {

            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
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
            this.GetDivision();
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }

        //protected void GetEmployee()
        //{
        //    string comcod = this.GetCompCode();
        //    string company = (this.ddlWstation.SelectedValue.Substring(0, 4).ToString() == "0000") ? "%" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString() + "%";
        //    string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7).ToString() + "%";
        //    string deptid = (this.ddlDep.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDep.SelectedValue.ToString().Substring(0, 9).ToString() + "%";
        //    string sectioncode = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
        //    string line = "%";
        //    string txtEmpname = "%";
        //    string chkwithouddat = "without";

        //    DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLISTIDCARD", sectioncode, txtEmpname, company, div, deptid, line, "", "", chkwithouddat);
        //    this.ddlEmployee.DataTextField = "empname";
        //    this.ddlEmployee.DataValueField = "empid";
        //    this.ddlEmployee.DataSource = ds1.Tables[0];
        //    this.ddlEmployee.DataBind();

        //}
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "BreakFast":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "NightBill":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "TransAllow":
                    this.divMonth.Visible = false;
                    this.divDate.Visible = true;
                    this.MultiView1.ActiveViewIndex = 2;
                    this.txtDate.Text = Convert.ToDateTime(System.DateTime.Today).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

            }
        }
        private void GetEmpLine()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlEmpLine.DataTextField = "hrgdesc";
            this.ddlEmpLine.DataValueField = "hrgcod";
            this.ddlEmpLine.DataSource = ds3;
            this.ddlEmpLine.DataBind();
            this.ddlEmpLine.SelectedValue = "00000";
        }
        private void GetMonth()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlMonth.DataTextField = "yearmon";
            this.ddlMonth.DataValueField = "ymon";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "BreakFast":
                    if (this.lbtnOk.Text == "New")
                    {
                        this.lbtnOk.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.divAddEmp.Visible = false;
                        this.chkAddEmp.Checked = false;
                        this.gvEmpBreakFast.DataSource = null;
                        this.gvEmpBreakFast.DataBind();
                        return;

                    }

                    this.lbtnOk.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.GetBreakFastData();
                    break;

                case "NightBill":
                    if (this.lbtnOk.Text == "New")
                    {
                        this.lbtnOk.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.divAddEmp.Visible = false;
                        this.chkAddEmp.Checked = false;
                        this.gvEmpNightBill.DataSource = null;
                        this.gvEmpNightBill.DataBind();
                        return;
                    }

                    this.lbtnOk.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.GetNightBillData();
                    break;

                case "TransAllow":
                    if (this.lbtnOk.Text == "New")
                    {
                        this.lbtnOk.Text = "Ok";
                        this.divChkEmp.Visible = false;
                        this.divAddEmp.Visible = false;
                        this.chkAddEmp.Checked = false;
                        this.gvTransAllow.DataSource = null;
                        this.gvTransAllow.DataBind();
                        return;
                    }

                    this.lbtnOk.Text = "New";
                    this.divChkEmp.Visible = true;
                    this.GetTransAllowanceData();
                    break;
            }

        }

        private void GetBreakFastData()
        {
            Session.Remove("tblallowance");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string monthId = this.ddlMonth.SelectedValue.ToString();
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string line = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string jobLocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string empStatus = this.ddlEmpStatus.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "RPT_EMP_BREAKFAST", null, null, null, monthId, emptype, divison, deptid, section, line, jobLocation, userid, empStatus, "", "", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpBreakFast.DataSource = null;
                this.gvEmpBreakFast.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            Session["tblallowance"] = ds2.Tables[0];
            this.Data_Bind();
        }

        private void GetNightBillData()
        {
            Session.Remove("tblallowance");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string monthId = this.ddlMonth.SelectedValue.ToString();
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string line = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string jobLocation = this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87%" : this.ddlJobLocation.SelectedValue.ToString() + "%";
            string empStatus = this.ddlEmpStatus.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "RPT_EMP_NIGHT_BILL", null, null, null, monthId, emptype, divison, deptid, section, line, jobLocation, userid, empStatus, "", "", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpBreakFast.DataSource = null;
                this.gvEmpBreakFast.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            Session["tblallowance"] = ds2.Tables[0];
            this.Data_Bind();
        }
        private void GetTransAllowanceData()
        {
            Session.Remove("tblallowance");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string date = this.txtDate.Text;
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
            string deptid = ((this.ddlDep.SelectedValue.ToString() == "00000") ? "" : this.ddlDep.SelectedValue.ToString()) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string line = (this.ddlEmpLine.SelectedValue.ToString() == "00000" ? "70" : this.ddlEmpLine.SelectedValue.ToString()) + "%";
            string jobLocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string empStatus = this.ddlEmpStatus.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "RPT_EMP_TRANSALLOW", null, null, null, date, emptype, divison, deptid, section, line, jobLocation, userid, empStatus, "", "", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpBreakFast.DataSource = null;
                this.gvEmpBreakFast.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            Session["tblallowance"] = ds2.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            try
            {

                string type = this.Request.QueryString["Type"].ToString().Trim();
                DataTable dt = (DataTable)Session["tblallowance"];

                switch (type)
                {
                    case "BreakFast":
                        this.gvEmpBreakFast.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpBreakFast.DataSource = dt;
                        this.gvEmpBreakFast.DataBind();

                        Session["Report1"] = gvEmpBreakFast;
                        if (dt.Rows.Count > 0)
                        {
                            ((HyperLink)this.gvEmpBreakFast.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }
                        break;

                    case "NightBill":
                        this.gvEmpNightBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvEmpNightBill.DataSource = dt;
                        this.gvEmpNightBill.DataBind();

                        Session["Report1"] = gvEmpNightBill;
                        if (dt.Rows.Count > 0)
                        {
                            ((HyperLink)this.gvEmpNightBill.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }
                        break;

                    case "TransAllow":
                        this.gvTransAllow.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvTransAllow.DataSource = dt;
                        this.gvTransAllow.DataBind();

                        Session["Report1"] = gvTransAllow;
                        if (dt.Rows.Count > 0)
                        {
                            ((HyperLink)this.gvTransAllow.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string type = this.Request.QueryString["Type"].ToString().Trim();
                if (chkAddEmp.Checked)
                {
                    this.divAddEmp.Visible = true;
                    Session.Remove("tbladdallowance");
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string userid = hst["usrid"].ToString();
                    string comcod = this.GetCompCode();
                    string monthId = type == "TransAllow" ? this.txtDate.Text : this.ddlMonth.SelectedValue.ToString();
                    string emptype = "%";
                    string divison = "%";
                    string deptid = "%";
                    string section = "%";
                    string line = "%";
                    string jobLocation = this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87%" : this.ddlJobLocation.SelectedValue.ToString() + "%";
                    string CallType = this.GetCallType();
                    DataSet ds2 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", CallType, null, null, null, monthId, emptype, divison, deptid, section, line, jobLocation, userid, "", "", "", "", "", "", "", "", "", "", "", "");
                    if (ds2 == null)
                    {
                        this.gvEmpBreakFast.DataSource = null;
                        this.gvEmpBreakFast.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject.ToString() + "');", true);
                        return;
                    }

                    Session["tbladdallowance"] = ds2.Tables[0];
                    DataTable dt1 = ds2.Tables[0];
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
                            dra["idcard"] = dr1["idcardno"].ToString();
                            dra["empname"] = dr1["idcardno"].ToString() + "-" + dr1["empname"].ToString();
                            dt.Rows.Add(dra);
                        }
                    }

                    this.ddlEmployee.DataTextField = "empname";
                    this.ddlEmployee.DataValueField = "empid";
                    this.ddlEmployee.DataSource = dt;
                    this.DataBind();

                    //Data Bind Type Wise
                    switch (type)
                    {
                        //Gridview null for Transport Allow. report. 
                        case "TransAllow":
                            Session.Remove("tblallowance");
                            DataTable dt3 = dt1.Clone();
                            Session["tblallowance"] = dt3;
                            this.Data_Bind();
                            break;

                        default:
                            //Gridview null for all Line Selected.
                            string linecode = this.ddlEmpLine.SelectedValue;
                            switch (linecode)
                            {
                                case "00000":
                                    Session.Remove("tblallowance");
                                    DataTable dt4 = dt1.Clone();
                                    Session["tblallowance"] = dt4;
                                    this.Data_Bind();
                                    break;

                                default:
                                    this.Data_Bind();
                                    break;
                            }
                            break;
                    }
                    
                }
                else
                {
                    this.divAddEmp.Visible = false;
                    switch (type)
                    {
                        case "BreakFast":
                            this.gvEmpBreakFast.DataSource = null;
                            this.gvEmpBreakFast.DataBind();
                            break;

                        case "NightBill":
                            this.gvEmpNightBill.DataSource = null;
                            this.gvEmpNightBill.DataBind();
                            break;

                        case "TransAllow":
                            this.gvTransAllow.DataSource = null;
                            this.gvTransAllow.DataBind();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }

        private string GetCallType()
        {
            string callType = "";
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "BreakFast":
                    callType = "RPT_EMP_BREAKFAST";
                    break;

                case "NightBill":
                    callType = "RPT_EMP_NIGHT_BILL";
                    break;

                case "TransAllow":
                    callType = "RPT_EMP_TRANSALLOW";
                    break;
            }

            return callType;
        }

        protected void lnkbtnAddEmp_Click(object sender, EventArgs e)
        {
            try
            {
                string type = this.Request.QueryString["Type"].ToString().Trim();
                DataTable dt = (DataTable)Session["tblallowance"];
                DataTable dtadd = (DataTable)Session["tbladdallowance"];
                string empid = this.ddlEmployee.SelectedValue.ToString();
                DataRow[] dr1 = dt.Select("empid='" + empid + "'");
                if (dr1.Length == 0)
                {
                    DataRow[] dra = dtadd.Select("empid='" + empid + "'");
                    dt.ImportRow(dra[0]);
                }
                else
                {
                    string idcard = dt.Select("empid='" + empid + "'")[0]["idcardno"].ToString();
                    string empname = dt.Select("empid='" + empid + "'")[0]["empname"].ToString();

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Employee: " + idcard + " - " + empname + " already existed!" + "');", true);
                }

                DataView dv = dt.DefaultView;
                //dv.Sort = ("idcardno");
                Session["tblallowance"] = dv.ToTable();
                this.SaveValue();
                this.Data_Bind();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
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
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "BreakFast":
                    this.PrintBreakFast();
                    break;

                case "NightBill":
                    this.PrintNightBill();
                    break;

                case "TransAllow":
                    this.PrintTransAllowance();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Food Allowance Roport";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void PrintBreakFast()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string monthid = this.ddlMonth.SelectedValue.ToString();
            string year = monthid.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(monthid.Substring(4));

            DataTable dt = (DataTable)Session["tblallowance"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptFoodAllowance>();
            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                //FB
                case "5305":
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptBreakFastBillFB", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    break;

                //Footbed
                case "5306":
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptBreakFastBillFootbed", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    break;

                default:
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptBreakFastBill", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    break;
            }
          
            Rpt1.SetParameters(new ReportParameter("comcod", comcod));
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Breakfast Payment Sheet Month of " + month + " " + year));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintNightBill()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string username = hst["username"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string monthid = this.ddlMonth.SelectedValue.ToString();
            string year = monthid.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(monthid.Substring(4));

            DataTable dt = (DataTable)Session["tblallowance"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptFoodAllowance>();
            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                //FB
                case "5305":
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptNightBillFB", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    break;

                //Footbed
                case "5306":
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptNightBillFootbed", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    break;

                default:
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptNightBill", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    break;
            }
           
            Rpt1.SetParameters(new ReportParameter("comcod", comcod));
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Food Allowance Payment Sheet Month of " + month + " " + year));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintTransAllowance()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string paydate = this.txtDate.Text;

            DataTable dt = (DataTable)Session["tblallowance"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptTransAllowance>();
            double netAmt = list.Sum(l => l.payamt);
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptTransAllowance", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comcod", comcod));
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "TRANSPORT BILL - " + paydate));
            Rpt1.SetParameters(new ReportParameter("InWord", "In Word: " + ASTUtility.Trans(netAmt,2)));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void gvEmpBreakFast_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpBreakFast.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvEmpNightBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpNightBill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvTransAllow_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvTransAllow.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lnkbtnPutSameValue_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblallowance"];
            int TblRowIndex;
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "BreakFast":
                    for (int i = 0; i < this.gvEmpBreakFast.Rows.Count; i++)
                    {
                        double workDay = Convert.ToDouble("0" + ((TextBox)this.gvEmpBreakFast.Rows[0].FindControl("txtgvBFWrkDay")).Text.Trim());
                        double perDayAmt = Convert.ToDouble("0" + ((TextBox)this.gvEmpBreakFast.Rows[0].FindControl("txtgvBFPerDay")).Text.Trim());
                        TblRowIndex = (gvEmpBreakFast.PageIndex * gvEmpBreakFast.PageSize) + i;
                        dt.Rows[TblRowIndex]["wrkday"] = workDay;
                        dt.Rows[TblRowIndex]["perday"] = perDayAmt;
                    }
                    break;

                case "NightBill":
                    for (int i = 0; i < this.gvEmpNightBill.Rows.Count; i++)
                    {
                        double workDay = Convert.ToDouble("0" + ((TextBox)this.gvEmpNightBill.Rows[0].FindControl("txtgvNBWrkDay")).Text.Trim());
                        double perDayAmt = Convert.ToDouble("0" + ((TextBox)this.gvEmpNightBill.Rows[0].FindControl("txtgvNBPerDay")).Text.Trim());
                        TblRowIndex = (gvEmpNightBill.PageIndex * gvEmpNightBill.PageSize) + i;
                        dt.Rows[TblRowIndex]["wrkday"] = workDay;
                        dt.Rows[TblRowIndex]["perday"] = perDayAmt;
                    }
                    break;

                case "TransAllow":
                    for (int i = 0; i < this.gvTransAllow.Rows.Count; i++)
                    {
                        double payamt = Convert.ToDouble("0" + ((TextBox)this.gvTransAllow.Rows[0].FindControl("txtgvTAPayAmt")).Text.Trim());
                        TblRowIndex = (gvTransAllow.PageIndex * gvTransAllow.PageSize) + i;
                        dt.Rows[TblRowIndex]["payamt"] = payamt;
                    }
                    break;

                default:
                    break;
            }

            Session["tblallowance"] = dt;
            this.Data_Bind();
        }

        protected void lnkbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }
        private void SaveValue()
        {
            string type = this.Request.QueryString["Type"].ToString();
            DataTable dt = (DataTable)Session["tblallowance"];
            int TblRowIndex;
            switch (type)
            {
                case "BreakFast":
                    for (int i = 0; i < this.gvEmpBreakFast.Rows.Count; i++)
                    {
                        double wrkDay = Convert.ToDouble("0" + ((TextBox)this.gvEmpBreakFast.Rows[i].FindControl("txtgvBFWrkDay")).Text.Trim());
                        double perDayAmt = Convert.ToDouble("0" + ((TextBox)this.gvEmpBreakFast.Rows[i].FindControl("txtgvBFPerDay")).Text.Trim());
                        TblRowIndex = (gvEmpBreakFast.PageIndex * gvEmpBreakFast.PageSize) + i;

                        double payAmt = wrkDay > 0 ? (wrkDay * perDayAmt) : 0;
                        dt.Rows[TblRowIndex]["wrkday"] = wrkDay;
                        dt.Rows[TblRowIndex]["perday"] = perDayAmt;
                        dt.Rows[TblRowIndex]["payamt"] = payAmt;
                    }
                    break;

                case "NightBill":
                    for (int i = 0; i < this.gvEmpNightBill.Rows.Count; i++)
                    {
                        double wrkDay = Convert.ToDouble("0" + ((TextBox)this.gvEmpNightBill.Rows[i].FindControl("txtgvNBWrkDay")).Text.Trim());
                        double perDayAmt = Convert.ToDouble("0" + ((TextBox)this.gvEmpNightBill.Rows[i].FindControl("txtgvNBPerDay")).Text.Trim());
                        TblRowIndex = (gvEmpNightBill.PageIndex * gvEmpNightBill.PageSize) + i;

                        double payAmt = wrkDay > 0 ? (wrkDay * perDayAmt) : 0;
                        dt.Rows[TblRowIndex]["wrkday"] = wrkDay;
                        dt.Rows[TblRowIndex]["perday"] = perDayAmt;
                        dt.Rows[TblRowIndex]["payamt"] = payAmt;
                    }
                    break;

                case "TransAllow":
                    for (int i = 0; i < this.gvTransAllow.Rows.Count; i++)
                    {
                        double payAmt = Convert.ToDouble("0" + ((TextBox)this.gvTransAllow.Rows[i].FindControl("txtgvTAPayAmt")).Text.Trim());
                        TblRowIndex = (gvTransAllow.PageIndex * gvTransAllow.PageSize) + i;

                        dt.Rows[TblRowIndex]["payamt"] = payAmt;
                    }
                    break;

                default:
                    break;
            }
            Session["tblallowance"] = dt;
            this.Data_Bind();

        }
        protected void lnkbtnFiUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblallowance"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string type = this.Request.QueryString["type"].ToString();
            string monthid = this.ddlMonth.SelectedValue.ToString();
            string dayid = Convert.ToDateTime(this.txtDate.Text).ToString("yyyyMMdd");
            switch (type)
            {
                case "BreakFast":
                    try
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string empid = dt.Rows[i]["empid"].ToString();
                            string secid = dt.Rows[i]["refno"].ToString();
                            string wrkday = dt.Rows[i]["wrkday"].ToString();
                            string perdrate = dt.Rows[i]["perday"].ToString();
                            string amt = dt.Rows[i]["payamt"].ToString();
                            if (wrkday != "0")
                            {
                                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "INSERT_UPDATE_BREAKFAST", monthid, empid, secid, wrkday, perdrate, amt);
                            }

                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Breakfast Updated Successfully');", true);

                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);


                    }
                    break;

                case "NightBill":
                    try
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string empid = dt.Rows[i]["empid"].ToString();
                            string secid = dt.Rows[i]["refno"].ToString();
                            string wrkday = dt.Rows[i]["wrkday"].ToString();
                            string perdrate = dt.Rows[i]["perday"].ToString();
                            string amt = dt.Rows[i]["payamt"].ToString();
                            if (wrkday != "0")
                            {
                                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "INSERT_UPDATE_NIGHTBILL", monthid, empid, secid, wrkday, perdrate, amt);
                            }
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Night Bill Updated Successfully');", true);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);


                    }
                    break;

                case "TransAllow":
                    try
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string empid = dt.Rows[i]["empid"].ToString();
                            string secid = dt.Rows[i]["refno"].ToString();
                            string busloccode = dt.Rows[i]["busloccode"].ToString();
                            string actintime = dt.Rows[i]["actintime"].ToString();
                            string actouttime = dt.Rows[i]["actouttime"].ToString();
                            string amt = dt.Rows[i]["payamt"].ToString();
                            if (amt != "0")
                            {
                                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "INSERT_UPDATE_TRANSALLOW", dayid, empid, secid, busloccode, actintime, actouttime, amt);
                                if(!result)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                                    return;
                                }
                            }
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Transport Bill Updated Successfully');", true);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);


                    }
                    break;

                default:
                    break;
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Food & Trans Allowance Roport";
                string eventdesc = "Update Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString();
            DataTable dt = (DataTable)Session["tblallowance"];
            
            switch (type)
            {
                case "BreakFast":
                    int grdindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                    int rowIndex = (this.gvEmpBreakFast.PageSize) * this.gvEmpBreakFast.PageIndex + grdindex;     
                    var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptFoodAllowance>();
                    list.RemoveAt(rowIndex);
                    Session["tblallowance"] = ASITUtility03.ListToDataTable(list);
                    this.Data_Bind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('BreakFast Emp. Deleted Successfully');", true);                    
                    break;

                case "NightBill":
                    grdindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                    rowIndex = (this.gvEmpBreakFast.PageSize) * this.gvEmpBreakFast.PageIndex + grdindex;
                    list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptFoodAllowance>();
                    list.RemoveAt(rowIndex);
                    Session["tblallowance"] = ASITUtility03.ListToDataTable(list);
                    this.Data_Bind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Night Bill Emp. Deleted Successfully');", true);
                    break;

                case "TransAllow":
                    grdindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                    rowIndex = (this.gvTransAllow.PageSize) * this.gvTransAllow.PageIndex + grdindex;
                    var listtrnsallow = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptTransAllowance>();
                    listtrnsallow.RemoveAt(rowIndex);
                    Session["tblallowance"] = ASITUtility03.ListToDataTable(listtrnsallow);
                    this.Data_Bind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Transport Allow. Emp. Deleted Successfully');", true);
                    break;

                default:
                    break;
            }          

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Food Allowance Roport";
                string eventdesc = "Delete Allowance";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


    }
}