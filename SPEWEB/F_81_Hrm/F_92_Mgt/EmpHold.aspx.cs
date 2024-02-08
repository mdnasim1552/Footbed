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

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class EmpHold : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.GetMonth();
                this.txtfrmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //string type = this.Request.QueryString["InputType"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (type == "Dept") ? "Department Code" : (type == "Appointment") ? "Employees Code" : "";

                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "EmpSalHold") ? "Employee Salary Hold" : "Employee Bonus Hold";


                this.CommonButton();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Employee Salary Hold";

                GetWorkStation();
                GetAllOrganogramList();


            }

        }
        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkupdate_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetCompCode();
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
        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
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

            string comcod = GetCompCode();
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
            string comcod = GetCompCode();
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





        protected void createtable()
        {

            DataTable dttemp = new DataTable();
            dttemp.Columns.Add("comcod", Type.GetType("System.String"));
            dttemp.Columns.Add("empid", Type.GetType("System.String"));
            dttemp.Columns.Add("empname", Type.GetType("System.String"));
            dttemp.Columns.Add("idcard", Type.GetType("System.String"));
            dttemp.Columns.Add("desig", Type.GetType("System.String"));
            dttemp.Columns.Add("frmdate", Type.GetType("System.DateTime"));
            dttemp.Columns.Add("todate", Type.GetType("System.DateTime"));
            Session["tblemphold"] = dttemp;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void GetEmployeeName()
        {
            Session.Remove("tblempdsg");
            string comcod = this.GetCompCode();
            string compcode = (this.ddlWstation.SelectedValue.ToString().Substring(0, 4) == "0000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string deptcode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string txtSProject = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPNAME", compcode, deptcode, Section, txtSProject, "1", "%", "", "", "");
            Session["tblempdsg"] = ds3.Tables[0];
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds3.Tables[0];
            this.ddlEmployee.DataBind();
        }

        private void GetMonth()
        {

            string comcod = this.GetCompCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETMONTH", "", "", "", "", "", "", "", "", "");
            this.ddlMonth.DataTextField = "mnam";
            this.ddlMonth.DataValueField = "yearmon";
            this.ddlMonth.DataSource = ds2.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM");


        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.ddlMonth.Enabled = false;
                this.lbtnOk.Text = "New";
                this.PnlSub.Visible = true;
                this.GetFrmToDate();
                ddlSection_SelectedIndexChanged(null, null);
                return;
            }

            this.ddlMonth.Enabled = true;
            this.lbtnOk.Text = "Ok";
            this.PnlSub.Visible = false;
            this.ddlEmployee.Items.Clear();
            this.gvemphold.DataSource = null;
            this.gvemphold.DataBind();
            Session.Remove("tblemphold");
        }

        private void GetFrmToDate()
        {
            string yearmon = this.ddlMonth.SelectedValue.ToString(); //202305
            string frmdate = yearmon.Substring(4) + "-01-" + yearmon.Substring(0, 4);
            this.txtfrmDate.Text = Convert.ToDateTime(frmdate).ToString("dd-MMM-yyyy");
            this.txttoDate.Text = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }

        private void SaveValue()
        {
            if (Session["tblemphold"] == null)
                this.createtable();

            DataTable dt = (DataTable)Session["tblemphold"];
            DataTable dtemp = (DataTable)(DataTable)Session["tblempdsg"];
            string empid = this.ddlEmployee.SelectedValue.ToString();
            DataRow[] dr = dt.Select("empid='" + empid + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = dt.NewRow();

                dr1["empid"] = empid;
                dr1["empname"] = this.ddlEmployee.SelectedItem.Text.Trim();
                dr1["idcard"] = (dtemp.Select("empid='" + empid + "'"))[0]["idcard"];
                dr1["desig"] = (dtemp.Select("empid='" + empid + "'"))[0]["desig"];
                dr1["frmdate"] = this.txtfrmDate.Text.Trim();
                dr1["todate"] = this.txttoDate.Text.Trim();
                dt.Rows.Add(dr1);
            }
            else
            {
                string existempdet = "Employee : " + dr[0]["empname"].ToString() + " already existed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + existempdet + "');", true);

                dr[0]["frmdate"] = this.txtfrmDate.Text.Trim();
                dr[0]["todate"] = this.txttoDate.Text.Trim();
                Session["tblemphold"] = dt;
            }
            this.Data_Bind();


        }

        private void Data_Bind()
        {

            this.gvemphold.DataSource = (DataTable)Session["tblemphold"];
            this.gvemphold.DataBind();

        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            this.SaveValue();

        }

        protected void lnkupdate_Click(object sender, EventArgs e)
        {
            try
            {


                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["tblemphold"];
                string Month = this.ddlMonth.SelectedValue.ToString();



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string frmdate = Convert.ToDateTime(dt.Rows[i]["frmdate"].ToString()).ToString("dd-MMMM-yyyy");
                    string todate = Convert.ToDateTime(dt.Rows[i]["todate"].ToString()).ToString("dd-MMMM-yyyy");
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSORUPEMPHOLD", Month, empid, frmdate, todate, "", "", "", "", "", "", "", "", "", "", "");

                    if (!result)
                        return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Salary Hold Updated Successfully');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
               

            }


        }


        protected void gvemphold_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            imgbtnEmployee_Click(null, null);
        }
    }
}