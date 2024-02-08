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

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class RptEmpAttenSummary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetWorkStation();
                GetDivision();
                GetDeptList();
                GetSectionList();
                this.GetJobLocation();
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = "01" + date.Substring(2);
                this.txttoDate.Text = Convert.ToDateTime(txtDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                string Type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "AttnMon") ? "Attendance Summary Report(Month Wise)" : "";
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            rptEmpMonAttn();
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
            string userid = hst["usrid"].ToString();
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
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetDept(comcod, wstation);
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst;
            this.ddlDept.DataBind();
            this.ddlDept.SelectedValue = "00000";

        }

        private void GetSectionList()
        {
            string comcod = GetCompCode();
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
        
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }

        private void rptEmpMonAttn()
        {
            DataTable dt = (DataTable)ViewState["tblattenmon"];
            if (dt == null)
            {
                Response.Write("<script>alert('Please Click OK of the Page and Press Print to continue!');</script>");
            }
            else
            {
                string comcod = this.GetCompCode();

                string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
                string empType = this.ddlWstation.SelectedItem.ToString();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();

                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                ///string cominfo = ASTUtility.Cominformation();

                var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_83_Att.BO_ClassAttn.EMonAtten>().ToList();

                string rptTitle = "Attendance Summary Report for " + date;
                LocalReport rpt1 = new LocalReport();
                rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_83_Att.RptMonAttenSummary", lst, null, null);
                rpt1.EnableExternalImages = true;
                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("rptTitle", rptTitle));                
                rpt1.SetParameters(new ReportParameter("empType", empType));                
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
                rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (lnkbtnShow.Text == "Ok")
            {
                lnkbtnShow.Text = "New";
                ShowData();
            }
            else
            {
                lnkbtnShow.Text = "Ok";
                this.gvAttnSum.DataSource = null;
                this.gvAttnSum.DataBind();
            }
        }

        public void ShowData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
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
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string empstatus = this.ddlEmpStatus.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE_SUMMARY", "MONTHLY_ATTEN_SUMMARY", emptypemulti, section, "", joblocation, date, todate, divison, deptid, userid, empstatus);
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }

            ViewState["tblattenmon"] = ds2.Tables[0];
            this.Data_Bind();
            
        }

        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblattenmon"];
                this.gvAttnSum.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                this.gvAttnSum.DataSource = dt;
                this.gvAttnSum.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ex.Message +"');", true);
            }
          
        }

        protected void gvAttnSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAttnSum.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}