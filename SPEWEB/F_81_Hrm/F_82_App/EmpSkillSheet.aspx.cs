using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class EmpSkillSheet : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE SKILL GRADE";
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                this.CommonButton();
                GetWorkStation();
                GetAllOrganogramList();
                this.GetSkillddl();
            }
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }

        private void GetSkillddl()
        {
            string comcod = GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSKILLDDLVALUE", "", "", "", "", "", "", "", "", "");
            this.ddlempSkill.DataTextField = "hrgdesc";
            this.ddlempSkill.DataValueField = "hrgcod";
            this.ddlempSkill.DataSource = ds3;
            this.ddlempSkill.DataBind();
            this.ddlempSkill.SelectedValue = "";

            ViewState["tblSkillddl"] = ds3.Tables[0];
        }

        private void GetWorkStation()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000" && x.actcode != "940100000000");
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation.SelectedIndex = 1;
            this.ddlWstation_SelectedIndexChanged(null, null);
        }

        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
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
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
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

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();
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

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            ViewState.Remove("GetAllEmpInf");
            string comcod = this.GetCompCode();
            string company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string depart = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLINEINFO", company, divison, depart, section, "", "", "", "");
            if (ds == null)
            {
                return;
            }

            ViewState["GetAllEmpInf"] = ds.Tables[0];
            this.pnlSkill.Visible = true;
            this.Data_bind();
        }

        private void Data_bind()
        {
            DataTable dt = (DataTable)ViewState["GetAllEmpInf"];
            DataTable dt1 = (DataTable)ViewState["tblSkillddl"];

            this.grvspecday.DataSource = dt;
            this.grvspecday.DataBind();

            DataView dv1;
            DropDownList ddlgval;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                dv1 = dt1.DefaultView;
                ddlgval = ((DropDownList)this.grvspecday.Rows[i].FindControl("ddlempSkill"));
                ddlgval.DataTextField = "hrgdesc";
                ddlgval.DataValueField = "hrgcod";
                ddlgval.DataSource = dv1.ToTable();
                ddlgval.DataBind();

                ddlgval.SelectedValue = dt.Rows[i]["skillcode"].ToString() == "" ? "" : (dt.Rows[i]["skillcode"].ToString());
            }

        }
        public void CommonButton()
        {
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkSaveData_Click);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkbtnGenSkill_Click(object sender, EventArgs e)
        {
            string ddSkill = this.ddlempSkill.SelectedValue.ToString();
            string ddSkilldesc = this.ddlempSkill.SelectedItem.ToString();
            DataTable dt = (DataTable)ViewState["GetAllEmpInf"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["skillcode"] = ddSkill;
                dt.Rows[i]["skilldesc"] = ddSkilldesc;
            }
            ViewState["GetAllEmpInf"] = dt;
            this.Data_bind();
        }

        protected void lnkSaveData_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            bool result;
            for (int i = 0; i < this.grvspecday.Rows.Count; i++)
            {
                string Gvalue = ((DropDownList)this.grvspecday.Rows[i].FindControl("ddlempSkill")).SelectedValue.ToString();
                string empid = ((Label)this.grvspecday.Rows[i].FindControl("lblempid")).Text.Trim();
                result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATESKILLVAL", empid, Gvalue);
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successful";
                }
            }
        }
    }
}