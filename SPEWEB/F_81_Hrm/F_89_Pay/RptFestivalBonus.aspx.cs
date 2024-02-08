using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptFestivalBonus : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Festival Bonus Report";
                this.GetWorkStation();
                this.GetDivision();
                this.GetDeptList();
                this.GetSectionList();
                this.GetLineDDL();
                this.GetJobLocation();
                this.GetYear();
            }
        }
       
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent eventPFL-000020660
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void GetYear()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETYEAR", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlYear.DataTextField = "year1";
            this.ddlYear.DataValueField = "year1";
            this.ddlYear.DataSource = ds1.Tables[0];
            this.ddlYear.DataBind();
            this.ddlYear.SelectedValue = System.DateTime.Today.Year.ToString();

            //Get Bonus Date
            this.GetBonusDateList();
            ds1.Dispose();
        }
        private void GetBonusDateList ()
        {
            string comcod = this.GetComCode();
            string year = this.ddlYear.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "GET_BONUS_DATE_YEARLY", year, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlBonusDayList.DataTextField = "bonusname";
            this.ddlBonusDayList.DataValueField = "bonusdate";
            this.ddlBonusDayList.DataSource = ds1.Tables[0];
            this.ddlBonusDayList.DataBind();
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowFestivalBonus();
        }
        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBonusDateList();
        }
        private void ShowFestivalBonus()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usrid = hst["usrid"].ToString();
                string comcod = GetComCode();
                string bonusDate = Convert.ToDateTime(this.ddlBonusDayList.SelectedValue.Trim()).ToString("dd-MMM-yyyy");
                string emptypemulti = "";
                foreach (ListItem items in ddlWstation.Items)
                {
                    if (items.Selected)
                    {
                        emptypemulti += items.Value;
                    }
                }
                string divison = ((this.ddlDivision.SelectedValue.ToString() == "00000") ? "" : this.ddlDivision.SelectedValue.ToString()) + "%";
                string deptid = ((this.ddlDept.SelectedValue.ToString() == "00000") ? "" : this.ddlDept.SelectedValue.ToString()) + "%";
                string section = ((this.ddlSection.SelectedValue.ToString() == "00000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
                string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
                string line = ((this.ddlempline.SelectedValue.ToString() == "00000") ? "70" : this.ddlempline.SelectedValue.ToString()) + "%";
                DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_PAYROLL04", "RPT_FESTIVAL_BONUS", null, null, null, bonusDate, emptypemulti, divison, deptid, section, line, joblocation, usrid);
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    this.gvFestBonus.DataSource = null;
                    this.gvFestBonus.DataBind();
                    return;
                }

                Session["tblfestbonus"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = ex.ToString();
            }
        }
        private void GetLineDDL()
        {
            string comcod = GetComCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLINEDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempline.DataTextField = "hrgdesc";
            this.ddlempline.DataValueField = "hrgcod";
            this.ddlempline.DataSource = ds3;
            this.ddlempline.DataBind();
            this.ddlempline.SelectedValue = "00000";
            ViewState["tbllineddl"] = ds3.Tables[0];
        }
        private void GetJobLocation()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);

            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
            this.ddlJobLocation.SelectedValue = "00000";

        }
        private void GetWorkStation()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation.SelectedValue = "000000000000";
        }      
        private void GetDivision()
        {
            string comcod = GetComCode();
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
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
            string comcod = GetComCode();
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            var lst = getlist.GetDept(comcod, wstation);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "00000";
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
        private void GetSectionList()
        {
            string comcod = GetComCode();
            string wstation = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            var lst = getlist.GetSection(comcod, wstation);
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst;
            this.ddlSection.DataBind();
            this.ddlSection.SelectedValue = "00000";
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblfestbonus"];
            if (dt.Rows.Count == 0)
                return;

            this.gvFestBonus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvFestBonus.DataSource = dt;
            this.gvFestBonus.DataBind();

            Session["Report1"] = gvFestBonus;
            if (dt.Rows.Count > 0)
                ((HyperLink)this.gvFestBonus.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintFestivalBonus();
        }

        private void PrintFestivalBonus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compName = hst["comnam"].ToString();
            string compAdd = hst["comaddf"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string txtEidName = this.ddlBonusDayList.SelectedItem.Text;

            DataTable dt = (DataTable)Session["tblfestbonus"];
            var list = dt.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptFestBonus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptFestivalBonus", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", compName));
            Rpt1.SetParameters(new ReportParameter("compAdd", compAdd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", txtEidName));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvFestBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvFestBonus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }       
    }
}