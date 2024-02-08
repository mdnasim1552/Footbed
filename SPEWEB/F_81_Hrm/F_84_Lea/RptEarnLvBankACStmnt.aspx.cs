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

namespace SPEWEB.F_81_Hrm.F_84_Lea
{
    public partial class RptEarnLvBankACStmnt : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text ="Earn Leave Bank A/C Statement";
                this.GetWorkStation();
                this.GetDivision();
                this.GetDeptList();
                this.GetSectionList();
                this.GetYearMonth();
                this.GetJobLocation();
                this.GetBankName();
                this.GetLineddl();
            }

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }       

        private void GetWorkStation()
        {
            Session.Remove("lstwrkstation");
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
            lst.Add(new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1("000000000000", "ALL", "", "", "", ""));
            Session["lstwrkstation"] = lst;
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation.SelectedValue = "000000000000";
        }       
        private void GetDivision()
        {

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetDivision(comcod, wstation);
            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "00000";

        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        private void GetDeptList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetDept(comcod, wstation);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "00000";
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
        }
        private void GetSectionList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetSection(comcod, wstation);
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "00000";

        }
        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();
            string type = "Y";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", type, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.AddYears(-1).ToString("yyyy") + "12";
            ds1.Dispose();
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
        private void GetBankName()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETBANKNAME", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1.Tables[0];
            this.ddlBankName.DataBind();
        }
        private void GetLineddl()
        {
            string comcod = GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;

            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if(lnkbtnShow.Text =="Ok")
            {
                this.lnkbtnShow.Text = "New";
                this.divPageSize.Visible = true;
                this.ShowEnLvAccStmnt();
                this.Employee_List();
                return;
            }

            this.lnkbtnShow.Text = "Ok";
            this.divPageSize.Visible = false;
            this.gvEarnLvAccStatmnt.DataSource = null;
            this.gvEarnLvAccStatmnt.DataBind();
            this.ddlEmployee.Items.Clear();
        }

        private void ShowEnLvAccStmnt()
        {
            Session.Remove("tblenlvacstmnt");
            Session.Remove("tblenlvacstmnt2");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string emptypemulti = "";
            foreach (ListItem items in ddlWstation.Items)
            {
                if (items.Selected)
                {
                    emptypemulti += items.Value;
                }
            }
            string divison = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string JobLocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string empStatus = this.ddlEmpType.SelectedValue.ToString().Trim();
            string bankname = this.ddlBankName.SelectedValue.ToString();
            string line = (this.ddlempline.SelectedValue.ToString() == "00000") ? "%" : this.ddlempline.SelectedValue.ToString() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "RPT_EARNLEAVE_BANKSTMNT", emptypemulti, divison, deptid, section, MonthId, date, JobLocation, userid, empStatus, bankname, line);
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvEarnLvAccStatmnt.DataSource = null;
                this.gvEarnLvAccStatmnt.DataBind();
                return;
            }

            Session["tblenlvacstmnt"] = ds1.Tables[0];
            Session["tblenlvacstmnt2"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblenlvacstmnt"];
            if (dt.Rows.Count == 0)
                return;

            List<string> employeeList = new List<string>();
            foreach (ListItem litem in ddlEmployee.Items)
            {

                if (litem.Selected)
                {
                    employeeList.Add(litem.Value);

                }

            }
            if(employeeList.Count != 0)
            {
                EnumerableRowCollection<DataRow> filteredRows = from DataRow row in dt.AsEnumerable()
                               where employeeList.Contains(row.Field<string>("empid"))
                               select row;
                dt= filteredRows.CopyToDataTable();
            }

            //string empid = this.ddlEmployee.SelectedValue;
            //DataView dv = dt.DefaultView;
            //empid= empid == "000000000000" ? "" : empid;
            //dv.RowFilter = "empid like '%" + empid + "%'";
            //dt = dv.ToTable();

            

            this.gvEarnLvAccStatmnt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEarnLvAccStatmnt.DataSource = dt;
            this.gvEarnLvAccStatmnt.DataBind();

            if (dt.Rows.Count > 0)
            {
                this.FooterCalculation();
                Session["Report1"] = gvEarnLvAccStatmnt;
                ((HyperLink)this.gvEarnLvAccStatmnt.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }
            Session["tblenlvacstmnt"] = dt;
        }
        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {   
            DataTable dt = (DataTable)Session["tblenlvacstmnt2"];
            Session["tblenlvacstmnt"] = dt;
            if (dt == null) return;
            this.Data_Bind();
            
        }
        private void Employee_List()
        {
            DataTable dt = (DataTable)Session["tblenlvacstmnt2"];
            DataView dv = new DataView(dt);
            DataTable dt2 = dv.ToTable(false, "empname", "empid","idcardno");
            dt2.Columns.Add("nameidcardno", typeof(string));
            foreach(DataRow row in dt2.Rows)
            {
                row["nameidcardno"] = row["idcardno"]+ "-" + row["empname"];
            }
            dt2.Rows.Add("All Employee", "000000000000", "000000000000", "All");
            dt2.DefaultView.Sort = "empid asc";
            this.ddlEmployee.DataTextField = "nameidcardno";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = dt2;
            this.ddlEmployee.DataBind();
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblenlvacstmnt"];
            ((Label)this.gvEarnLvAccStatmnt.FooterRow.FindControl("lblgvFPayAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(leavenamt)", "")) ? 0.00 :
                               dt.Compute("Sum(leavenamt)", ""))).ToString("#,##0;(#,##0); ");
        }

        protected void gvEarnLvAccStatmnt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEarnLvAccStatmnt.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compName = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string year = this.ddlyearmon.SelectedItem.Text.Trim();

            DataTable dt = (DataTable)Session["tblenlvacstmnt"];
            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_86_All.EmployeeInformation.RptEarnLeaveEnCashment>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptEnLvBankAccStmnt", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", compName));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Earn Leave Bank A/C Statemnet " + year));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}