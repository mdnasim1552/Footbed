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

namespace SPEWEB.F_47_Kpi
{
    public partial class KpiTargetEntry : System.Web.UI.Page
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
                //((Label)this.Master.FindControl("lblTitle")).Text = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = " KPI Budget Entry";
                GetWorkStation();
                GetAllOrganogramList();
                CommonButton();
                this.ConfirmMessage.Visible = false;
                txtfromdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
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
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }

        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
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
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
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
            this.ibtnEmpList_Click(null, null);
        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();

        }
        private void GetEmployeeName()
        {

            string comcod = this.GetCompCode();
            //string ProjectCode = (this.txtEmpSrc.Text.Trim().Length > 0) ? "%" : (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
         

            string CompanyName = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";

            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string projectcode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string txtSProject = "%" + this.txtEmpSrc.Text + "%";

            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", CompanyName, txtSProject, division, projectcode, section, "", "", "", "");

            if (ds5 == null)
            {

                return;
            }

            DataTable dt1 = ds5.Tables[0].Copy();
            DataView dv1 = dt1.DefaultView;
            dt1 = dv1.ToTable().DefaultView.ToTable(true, "empid", "empname");
            dt1.Rows.Add("000000000000", "ALL Employee");

            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = dt1;
            this.ddlEmpName.DataBind();
            this.ddlEmpName.SelectedValue = "000000000000";
            Session["getemplist"] = dt1;

        }
        private void GetUserName()
        {

            string comcod = this.GetCompCode();
            string EmpName = this.ddlEmpName.SelectedValue.ToString() + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPRUSRNAME", EmpName, "", "", "", "", "", "", "", "");

            if (ds5 == null)
            {

                return;
            }

            DataTable dt1 = ds5.Tables[0].Copy();
            DataView dv1 = dt1.DefaultView;
            dt1 = dv1.ToTable().DefaultView.ToTable();
            

            this.ddluserName.DataTextField = "username";
            this.ddluserName.DataValueField = "usrid";
            this.ddluserName.DataSource = dt1;
            this.ddluserName.DataBind();
            

        }
        protected void searchbutton_Click(object sender, EventArgs e)
        {
            
            DataTable dt = (DataTable)Session["tblpay"];
            DataView view = new DataView();
            view.Table = dt;


            //this.gvovsal02.DataSource = view.ToTable();
            //this.gvovsal02.DataBind();

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "New")
            {
               Session.Remove("tblkpibgd");
                this.ddlworkGroup.Visible = false;
                this.ddlworklst.Visible = false;
                this.lblworkGroup.Visible = false;
                this.lblworklst.Visible = false;
                this.lnkbtnAdd.Visible = false;
                
                this.lnkbtnShow.Text = "Ok";
                this.ddlWstation.Enabled = true;
                this.ddlDivision.Enabled = true;
                this.ddlDept.Enabled = true;
                this.ddlSection.Enabled = true;
                this.ddlEmpName.Enabled = true;
                
                this.gvkpibgd.DataSource=null;
                this.gvkpibgd.DataBind();
            }
            else
            {
                this.ddlworkGroup.Visible = true;
                this.ddlworklst.Visible = true;
                this.lblworkGroup.Visible = true;
                this.lblworklst.Visible = true;
                this.lnkbtnAdd.Visible = true;

                this.lnkbtnShow.Text = "New";
                this.ddlWstation.Enabled = false;
                this.ddlDivision.Enabled = false;
                this.ddlDept.Enabled = false;
                this.ddlSection.Enabled = false;
                this.ddlEmpName.Enabled = false;
                this.kpibgdlst();
                this.GetWorkGroup();
            }
           
        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUserName();
        }
        private void GetWorkGroup()
        {

            string comcod = this.GetCompCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_MGT", "GETWORKGRP", "", "", "", "", "", "", "", "", "");
            if (ds5 == null)
            {
                return;
            }
            DataTable dt1 = ds5.Tables[0].Copy();
            DataView dv1 = dt1.DefaultView;
            dt1 = dv1.ToTable().DefaultView.ToTable();
            this.ddlworkGroup.DataTextField = "kpigdesc";
            this.ddlworkGroup.DataValueField = "kpigcod";
            this.ddlworkGroup.DataSource = dt1;
            this.ddlworkGroup.DataBind();
            ddlworkGroup_SelectedIndexChanged(null, null);
        }
        protected void ddlworkGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string workGroup =  this.ddlworkGroup.SelectedValue.Substring(0, 4).ToString() + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_MGT", "GETWORKLST", workGroup, "", "", "", "", "", "", "", "");
            if (ds5 == null)
            {
                return;
            }
            DataTable dt1 = ds5.Tables[0].Copy();
            DataView dv1 = dt1.DefaultView;
            dt1 = dv1.ToTable().DefaultView.ToTable();
            this.ddlworklst.DataTextField = "kpigdesc";
            this.ddlworklst.DataValueField = "kpigcod";
            this.ddlworklst.DataSource = dt1;
            this.ddlworklst.DataBind();
            Session["getworklist"] = dt1;
        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            this.AddRow();
            this.gvdata_load();
        }
        protected void gvdata_load()
        {
            DataSet ds1=(DataSet)Session["tblkpibgd"];
            this.gvkpibgd.DataSource = ds1.Tables[0];
            this.gvkpibgd.DataBind();
        }
        protected void kpibgdlst()
        {
            string comcod = this.GetCompCode();
            string monthid = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMM") +"%";
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string userid = this.ddluserName.SelectedValue.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_MGT", "KPIBGDLST",  empid, userid, monthid, "", "", "", "", "");
            if (ds == null)
            {
                return;
            }
            Session["tblkpibgd"] = ds;
            this.gvdata_load();
        }
        public void CommonButton()
        {
           
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("yyyyMM");
            
            string ddluserName = this.ddluserName.Text == ""? "0000000": this.ddluserName.Text;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string empid = hst["empid"].ToString();
            bool result = false;
            DataSet ds1 = (DataSet)Session["tblkpibgd"];
            int i = 0;
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
               
                string txtwrkqty = Convert.ToString(((TextBox)this.gvkpibgd.Rows[i].FindControl("txtkpiqty")).Text.Trim());
                string txtgvEmpcode = Convert.ToString(dr1["empid"]);
                string txtgvkpicode = Convert.ToString(dr1["kpicode"]);
                result = HRData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_KPI_MGT", "KPIENTRY", txtfromdate, txtgvEmpcode, txtgvkpicode, txtwrkqty, ddluserName, userid, "", "", "");
                i++;
            }
           
            if (result)
            {

                ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
            else
            {
                ((Panel)this.Master.FindControl("AlertArea")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);

        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            DataSet ds = (DataSet)Session["tblkpibgd"];

            DataRow[] dr = ds.Tables[0].Select();
            for (int i = 0; gvkpibgd.Rows.Count > i; i++)
            {

                string bdgqty= ((TextBox)this.gvkpibgd.Rows[i].FindControl("txtkpiqty")).Text.Trim();
                dr[i]["bgdqty"] = bdgqty;
                dr[i]["mark"] = Convert.ToDouble(bdgqty) * Convert.ToDouble(dr[i]["stdrate"]);
            }
            Session["tblkpibgd"] = ds;
            this.gvdata_load();
        }
        protected void AddRow()
        {
            DataSet ds = (DataSet)Session["tblkpibgd"];
            DataRow[] dr = ds.Tables[0].Select();
            foreach (DataRow drl1 in ds.Tables[0].Rows)
            {
                string kpicode = drl1["kpicode"].ToString();
                string skpicode = this.ddlworklst.SelectedValue.ToString();
                if (skpicode == kpicode)
                {
                    return;
                }
            }
            DataTable dtwrk = (DataTable)Session["getworklist"];
            string kpicode1= this.ddlworklst.SelectedValue.ToString();
            DataRow[] dr2 = dtwrk.Select("kpigcod = '"+ kpicode1 + "'");
            var emps = this.ddlEmpName.SelectedItem.ToString().Split('-');
            DataRow dr1 = ds.Tables[0].NewRow();
            dr1["empid"] = this.ddlEmpName.SelectedValue.ToString();
            dr1["empdesc"] = emps[1];
            dr1["kpicode"] = kpicode1;
            dr1["kpidesc"] = this.ddlworklst.SelectedItem.ToString();
            dr1["bgdqty"] = 0;
            dr1["mark"] = 0;
            dr1["stdrate"] = dr2[0]["stdrate"];
            ds.Tables[0].Rows.Add(dr1);
            Session["tblkpibgd"] = ds;
        }

        protected void gvkpibgd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataSet ds = (DataSet)Session["tblkpibgd"];
            if (ds.Tables[0].Rows.Count!=0)
            {
                ds.Tables[0].Rows[e.RowIndex].Delete();
            }
            Session["tblkpibgd"] = ds;
            this.gvdata_load();
        }
    }
}