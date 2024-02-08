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
using SPELIB;
using System.IO;
using System.Data.OleDb;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class HRGenOffdaySetup : System.Web.UI.Page
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


                ((Label)this.Master.FindControl("lblTitle")).Text = "GENERAL OFF DAY SETUP";

                this.GetAllOrganogramList();
                this.GetWorkStation();
                this.SetYear();

                this.GetMonth();

            }
        }
        private void SetYear()
        {
            var items = new List<string> { };
            
            for (int curyear = Convert.ToInt32(System.DateTime.Today.AddYears(-3).ToString("yyyy")); curyear < Convert.ToInt32(System.DateTime.Today.AddYears(-3).ToString("yyyy")) + 10; curyear++)
            {
                items.Add(curyear.ToString());
            }
            this.ddlyear.DataSource = items;
            this.ddlyear.DataBind();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

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



        }


        private void GetMonth()
        {
            string comcod = this.GetComeCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONTHFOROFFDAY", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }

            this.ddlMonth.DataTextField = "mnam";
            this.ddlMonth.DataValueField = "mno1";
            this.ddlMonth.DataSource = ds2.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.Month.ToString("yyyyMM").Trim();

        }

        [WebMethod(EnableSession = false)]
        public static string GetMonCalenderStatic(string emptype, string monthid)
        {
            try
            {
                Common ObjCommon = new Common();
                string comcod = ObjCommon.GetCompCode();
                string date = Convert.ToDateTime(monthid.Substring(4, 2) + "-" + "01-" + monthid.Substring(0, 4)).ToString("dd-MMM-yyyy");
                ProcessAccess HRData = new ProcessAccess();
                DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONDATESETUP", date, emptype, "", "", "", "", "", "", "");

                if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
                {

                    return "false";
                }
                List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.EmpOffDaySetup> lst = ds4.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.EmpOffDaySetup>();
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lst);
                //var json = jsonSerialiser.Serialize(list2);
                return json;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }


        [WebMethod(EnableSession = false)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SaveEmpOffDays(List<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.EmpOffDaySetup> offdata, string emptype, string monthid, string remarks)
        {
            try
            {
                HRGenOffdaySetup obj = new HRGenOffdaySetup();

                var listWithoutCol = offdata.Select(x => new { x.sdate, x.sdate1, x.wekend, x.holiday, x.reason }).ToList();
                Common ObjCommon = new Common();
                DataTable dt = ASITUtility03.ListToDataTable(listWithoutCol);
                DataSet ds = new DataSet("ds");
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "tblempoffday";
                string comcod = ObjCommon.GetCompCode();
                ProcessAccess HRData = new ProcessAccess();
                bool result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "UPDATE_EMPOFFDAYS", ds, null, null, emptype, monthid, remarks, "", "", "", "", "", "");

                if (result == true)
                {
                    return "Data Update Successfully";
                }
                return "Invalid";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }


        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}