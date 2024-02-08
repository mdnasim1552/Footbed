using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class HRPunchEntryManually : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                GetWorkStation();
                GetAllOrganogramList();
                GetMinute();
                CreateDataTable();
                GetJobLocation();
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
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
            lst.Add(new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1("000000000000", "ALL", "", "", "", ""));
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

            this.ddlSection_SelectedIndexChanged(null, null);
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

            this.ddlJobLocation_SelectedIndexChanged(null, null);
        }
        private void GetEmpName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetComeCode();
            string emptype = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string div = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString())+"%";
            string txtSProject ="%";
            string jobLoc = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", section, txtSProject, emptype, div, department, jobLoc, userid, "", "");
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds5.Tables[0];
            this.ddlEmpName.DataBind();

            ViewState["tblempdet"] = ds5.Tables[0];
            ds5.Dispose();
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
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetJobLocation();
        }
        protected void ddlJobLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        private void GetMinute()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GET_MINUTE", "", "", "", "");
            if (ds1==null)
                return;

            this.ddlMinute.DataTextField = "mmin";
            this.ddlMinute.DataValueField = "mmin";
            this.ddlMinute.DataSource = ds1.Tables[0];
            this.ddlMinute.DataBind();
            ds1.Dispose();
        }
      
        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable tbl1 = (DataTable)ViewState["tblempinfo"];
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("empid = '" + empid + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                DataTable tbl2 = (DataTable)ViewState["tblempdet"];
                dr1["department"] = (tbl2.Select("empid='"+empid+"'"))[0]["deptname"].ToString();
                dr1["section"] = (tbl2.Select("empid='"+empid+"'"))[0]["section"].ToString();
                dr1["empid"] = (tbl2.Select("empid='"+empid+"'"))[0]["empid"].ToString();
                dr1["idcard"] = (tbl2.Select("empid='"+empid+"'"))[0]["idcardno"].ToString();
                dr1["empname"] = (tbl2.Select("empid='"+empid+"'"))[0]["empnameo"].ToString();
                dr1["desig"] = (tbl2.Select("empid='"+empid+"'"))[0]["desig"].ToString();
                dr1["intime"] = this.ddlHour.SelectedValue.ToString() +" : " + this.ddlMinute.SelectedValue.ToString() +" " + this.ddlTT.SelectedValue.ToString();
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }
            else
            {
                dr2[0]["intime"] = this.ddlHour.SelectedValue.ToString() +" : " + this.ddlMinute.SelectedValue.ToString() +" " + this.ddlTT.SelectedValue.ToString();              

            }

            ViewState["tblempinfo"] = tbl1;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblempinfo"];
            this.gvPunchEntry.DataSource=dt;
            this.gvPunchEntry.DataBind();
        }

        private void CreateDataTable()
        {
            ViewState.Remove("tblempinfo");
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("department", Type.GetType("System.String"));
            tblt01.Columns.Add("section", Type.GetType("System.String"));
            tblt01.Columns.Add("empid", Type.GetType("System.String"));
            tblt01.Columns.Add("idcard", Type.GetType("System.String"));
            tblt01.Columns.Add("empname", Type.GetType("System.String"));
            tblt01.Columns.Add("desig", Type.GetType("System.String"));
            tblt01.Columns.Add("intime", Type.GetType("System.String"));
            tblt01.Columns.Add("remarks", Type.GetType("System.String"));
            ViewState["tblempinfo"] = tblt01;
        }

        protected void lFinalUpdate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string postDat = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            DataTable dt = (DataTable)ViewState["tblempinfo"];
            string comcod = this.GetComeCode();
            string Date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string idcardno = dt.Rows[i]["idcard"].ToString();
                string intime = Date+" "+ dt.Rows[i]["intime"].ToString();
                string machid = "0";
                string remarks = dt.Rows[i]["remarks"].ToString();

                bool result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPPUNCH", idcardno, Date, intime, machid, remarks, usrid, postDat, trmid, sessionid);                
                if (!result)
                {
                    string msg = HRData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ msg + "');", true);
                    return;
                }
               

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Manual Punch Updated Successfully');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string empcount = dt.Rows.Count.ToString();
                string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string eventtype = "Punch Entry Manually";
                string eventdesc = "Month ID: " + todate + ", Employee Type: " + this.ddlWstation.SelectedValue.ToString(); ;
                string eventdesc2 = "Employe Status- Active Employee, Total Employe " + empcount;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblempinfo"];

            for (int i = 0; i < this.gvPunchEntry.Rows.Count; i++)
            {
                string intime = ((TextBox)this.gvPunchEntry.Rows[i].FindControl("txtgvIntime")).Text.Trim();
                string remarks = ((TextBox)this.gvPunchEntry.Rows[i].FindControl("txtgvRemarks")).Text.Trim();
                dt.Rows[i]["intime"] = intime;
                dt.Rows[i]["remarks"] = remarks;
            }
            ViewState["tblempinfo"] = dt;


        }
    }
}