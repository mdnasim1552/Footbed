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
    public partial class RetiredEmployee : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtSepDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtefrmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                GetWorkStation();
                GetAllOrganogramList();

                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE SEPARATION INFORMATION ";

                this.CommonButton();
                this.GetSingEmpName();
                this.GetSepType();
            }

        }

        private void CommonButton()
        {

            
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(lnkbtnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;  ;;//

        }
        protected void lnkbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

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
            this.ddlWstation.SelectedValue= "000000000000";
            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        private void GetSingEmpName()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPSIGNAME", "81", "%", "%", "%", "", "", "", "", "");
            this.ddlsign.DataTextField = "signame";
            this.ddlsign.DataValueField = "idcard";
            this.ddlsign.DataSource = ds3.Tables[0];
            this.ddlsign.DataBind();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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
            this.ddlSection_SelectedIndexChanged(null,null);
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




        private void GetSepType()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GET_SEAPRATION_TYPE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + HRData.ErrorObject.ToString() + "');", true);
                return;
            }

            ViewState["tblseptype"] = ds1.Tables[0];
        }

        protected void resign_CheckedChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }

        private void GetEmployeeName()
        {
            if (this.lnkbtnShow.Text == "New")
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string compcode = (this.ddlWstation.SelectedValue.ToString().Substring(0, 4) == "0000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string deptcode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string isResignEmp = resign.Checked ? "True" : "False";
            string txtSProject = "%" + this.txtSrcEmployee.Text + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETEMPTNAME", compcode, deptcode, Section, txtSProject, usrid, isResignEmp, "", "", "");
            if(ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + HRData.ErrorObject.ToString() + "');", true);
                return;
            }

            Session["tblempdsg"] = ds3.Tables[0];
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds3.Tables[0];
            this.ddlEmployee.DataBind();

        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }

        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.ddlWstation.Enabled = false;
                this.ddlDivision.Enabled = false;
                this.ddlDept.Enabled = false;
                this.ddlSection.Enabled = false;
                this.ddlEmployee.Enabled = false;
                this.lnkbtnShow.Text = "New";
                this.PnlSepType.Visible = true;
                this.ShowSeparationDetails();
                return;
            }

            this.ddlWstation.Enabled = true;
            this.ddlDivision.Enabled = true;
            this.ddlDept.Enabled = true;
            this.ddlSection.Enabled = true;
            this.ddlEmployee.Enabled = true;
            this.lnkbtnShow.Text = "Ok";
            this.PnlSepType.Visible = false;
            this.ddlSepType.Items.Clear();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblover"];
                string comcod = this.GetCompCode();
                string date = Convert.ToDateTime(this.txtSepDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string entryDate = Convert.ToDateTime(this.txtefrmDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string empid = this.ddlEmployee.SelectedValue.ToString();
                string sptype = (this.ddlSepType.SelectedValue.ToString() == "00000") ? "" : this.ddlSepType.SelectedValue.ToString();
                string sempid = (this.ddlsign.SelectedValue.ToString() == "000000000000") ? "" : this.ddlsign.SelectedValue.ToString();
                string dayId = Convert.ToDateTime(this.txtSepDate.Text.Trim()).ToString("yyyyMMdd");

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "RESIGNEMP", empid, dayId, "", "", "", "", "", "");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Employee separation must be after last present day!');", true);
                    return;
                }

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string PostedByid = hst["usrid"].ToString();
                string Posttrmid = hst["compname"].ToString();
                string PostSession = hst["session"].ToString();
                string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string notes = this.TxtNotes.Text.ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "INSERTORUPDATESEPARATION", empid, date, sptype, Posteddat, PostedByid, Posttrmid, PostSession, notes, entryDate, sempid, "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject.ToString() + "');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Separated Successfully');", true);
                this.GetEmployeeName();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }          
            
        }

        private void ShowSeparationDetails()
        {
            string empid = this.ddlEmployee.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["tblempdsg"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.lblDesig.Text = ((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["desig"].ToString();
                this.LblJoiningDate.Text = Convert.ToDateTime(((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["joiningdat"]).ToString("dd-MMM-yyyy");
                this.LblConfirmationDate.Text = Convert.ToDateTime(((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["confirmdat"]).ToString("dd-MMM-yyyy");

                DataTable dt1 = (DataTable)ViewState["tblseptype"];
                this.ddlSepType.DataTextField = "hrgdesc";
                this.ddlSepType.DataValueField = "hrgcod";
                this.ddlSepType.DataSource = dt1;
                this.ddlSepType.DataBind();

                string resignEmp = resign.Checked ? "True" : "False";
                if (resignEmp == "True")
                {
                    string comcod = this.GetCompCode();
                    DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.sp_report_hr_empstatus2", "GETEMPSEPDATE", empid, "", "", "", "", "", "");            
                    DataTable dt2 = ds1.Tables[0];
                    if (dt2.Rows.Count > 0)
                    {
                        this.txtSepDate.Text = Convert.ToDateTime((dt2).Select("empid='" + empid + "'")[0]["dayid"]).ToString("dd-MMM-yyyy");
                        this.txtefrmDate.Text = Convert.ToDateTime((dt2).Select("empid='" + empid + "'")[0]["entrydate"]).ToString("dd-MMM-yyyy");
                        this.ddlSepType.SelectedValue = dt2.Select("empid='" + empid + "'")[0]["septype"].ToString();
                        this.TxtNotes.Text = dt2.Select("empid='" + empid + "'")[0]["notes"].ToString();
                    }
                    else
                    {
                        this.txtSepDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                        this.txtefrmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    }
                }
                else
                {
                    this.txtefrmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtSepDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                }
            }
        }

        
    }
}