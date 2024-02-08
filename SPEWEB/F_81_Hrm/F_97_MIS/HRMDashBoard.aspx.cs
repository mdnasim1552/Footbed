using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPELIB;
using SPEENTITY.C_81_Hrm.C_92_Mgt;
using System.Web.Script.Serialization;

namespace SPEWEB.F_81_Hrm.F_97_MIS
{
    public partial class HRMDashBoard : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        HRM_BL HRMBL = new HRM_BL();
        ProcessAccess _DataEntry = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // prevPage = Request.UrlReferrer.ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "HRM SMARTBOARD";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                this.lbtnOk_Click(null, null);

                ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).ToolTip = "Please Exit from Dashboard";

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        public string GetMonth ()
        {
            string month = Convert.ToDateTime(System.DateTime.Today).ToString("MMM-yyyy");
            return (month);
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblEmpStatus.Visible = true;
            this.lblYestAttn.Visible = true;
            this.GetEmployeeStatus();
            this.GetYestAttnStatus();
            this.GetTodayAttnStatus();
            this.GetAttnSalaryStatusGraph();
        }

        private void GetAttnSalaryStatusGraph()
        {
            var jsonSerialiser = new JavaScriptSerializer();

            string comcod = GetCompCode();
            string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds2 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO", "HRM_ATTN_SALARY_STATUS", CurDate1, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;

            var lstattn = ds2.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassAttnStatus>();
            var lstsal = ds2.Tables[1].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassCurYearSalary>();

            var attnstatus = jsonSerialiser.Serialize(lstattn);
            var curyrsal = jsonSerialiser.Serialize(lstsal);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + attnstatus + "','" + curyrsal + "')", true);

            ds2.Dispose();
        }

        private void GetEmployeeStatus()
        {
            try
            {
                string comcod = GetCompCode();
                string CurDate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus> list = HRMBL.ShowEmployeeStatus(comcod, CurDate);
                if (list == null)
                    return;

                this.gvEmpStatus.DataSource = list;
                this.gvEmpStatus.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        private void GetYestAttnStatus()
        {
            try
            {
                string comcod = GetCompCode();
                string CurDate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus> list1 = HRMBL.ShowYestAttnStatus(comcod, CurDate);
                if (list1 == null)
                    return;

                this.gvYestAttn.DataSource = list1;
                this.gvYestAttn.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
        private void GetTodayAttnStatus()
        {
            try
            {
                string comcod = GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassEmpStatus> lst1 = HRMBL.ShowTodayAttnStatus(comcod, CurDate1);
                if (lst1 == null)
                    return;

                this.gvTodayAttn.DataSource = lst1;
                this.gvTodayAttn.DataBind();

               DataSet ds1 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_DASH_BOARD_INFO", "HRM_TODAY_ATTN_STATUS", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassTodayAttnStatus> listc = ds1.Tables[1].DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassTodayAttnStatus>();
                ViewState["tbltdayattn"] = listc;
                this.BindChartTodayAttn();
            }
            catch (Exception ex)
            {

            }
        }
        private void BindChartTodayAttn()
        {
            List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassTodayAttnStatus> lst = (List<SPEENTITY.C_81_Hrm.C_92_Mgt.EClass_HRM.EClassTodayAttnStatus>)ViewState["tbltdayattn"];

            this.yprsnt.Text = Convert.ToDouble(lst[0].prsntempprcnt).ToString();
            this.yabsnt.Text = Convert.ToDouble(lst[0].absntempprcnt).ToString();
            this.yleave.Text = Convert.ToDouble(lst[0].leavempprcnt).ToString();
            this.yhlday.Text = Convert.ToDouble(lst[0].hldayempprcnt).ToString();

        }

    }
}