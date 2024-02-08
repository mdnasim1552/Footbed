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
    public partial class HRSpecialDaySetup : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "SPECIAL DAY SETUP ";
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CommonButton();
                this.GetSpecialDay();
                GetWorkStation();
                GetAllOrganogramList();
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

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            ViewState.Remove("GetAllSpecialDay");
            string comcod = this.GetCompCode();
            string company = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string depart = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string monthid = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("yyyyMMdd");

            string sdate = this.txtDate.Text.ToString();


            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETSPECIALDATEINFO", company, divison, depart, section, monthid, "", "", "", "");
            if (ds == null)
            {
                return;
            }
            this.grvspecday.DataSource = ds.Tables[0];
            this.grvspecday.DataBind();
            ViewState["GetAllSpecialDay"] = ds.Tables[0];

        }
        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lTotal_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkSaveData_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetSpecialDay()
        {
            string comcod = this.GetCompCode();

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETSPECIALDAY", "", "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            this.ddlSpecialDay.DataTextField = "hrgdesc";
            this.ddlSpecialDay.DataValueField = "hrgcod";
            this.ddlSpecialDay.DataSource = ds.Tables[0];
            this.ddlSpecialDay.DataBind();

            ViewState["GetSpecial"] = ds.Tables[0];

        }

        protected void lnkSaveData_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();


            string monthid = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("yyyyMMdd");
            string date = this.txtDate.Text.ToString();
            string SpecialDay = this.ddlSpecialDay.SelectedValue.Trim().ToString();

            DataTable dt = (DataTable)ViewState["GetAllSpecialDay"];

            DataSet ds1 = new DataSet("ds1");
            DataView dv1 = new DataView(dt);
            ds1.Tables.Add(dv1.ToTable());
            ds1.Tables[0].TableName = "tbl1";

            bool result;
            string tempdate = ds1.GetXml();
            result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "INSERTSPECIALDATE", ds1, null, null, monthid, date, SpecialDay, "", "", "");


            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            }

        }

        protected void chkalladjus_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["GetAllSpecialDay"];
            int i, index;
            if (((CheckBox)this.grvspecday.HeaderRow.FindControl("chkalladjus")).Checked)
            {

                for (i = 0; i < this.grvspecday.Rows.Count; i++)
                {
                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkadj")).Checked = true;


                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkeot")).Checked = false;
                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkPermit")).Checked = false;

                    ((CheckBox)this.grvspecday.HeaderRow.FindControl("checkAllEot")).Checked = false;
                    ((CheckBox)this.grvspecday.HeaderRow.FindControl("chkallView")).Checked = false;




                    index = (this.grvspecday.PageSize) * (this.grvspecday.PageIndex) + i;
                    dt.Rows[index]["adjleav"] = "True";
                    dt.Rows[index]["advleave"] = "False";
                    dt.Rows[index]["eot"] = "False";


                }

            }

            else
            {
                for (i = 0; i < this.grvspecday.Rows.Count; i++)
                {
                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkadj")).Checked = false;
                    index = (this.grvspecday.PageSize) * (this.grvspecday.PageIndex) + i;
                    dt.Rows[index]["adjleav"] = "False";

                }

            }

            ViewState["GetAllSpecialDay"] = dt;
        }

        protected void checkAllEot_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["GetAllSpecialDay"];
            int i, index;
            if (((CheckBox)this.grvspecday.HeaderRow.FindControl("checkAllEot")).Checked)
            {

                for (i = 0; i < this.grvspecday.Rows.Count; i++)
                {
                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkeot")).Checked = true;
                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkPermit")).Checked = false;
                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkadj")).Checked = false;


                    ((CheckBox)this.grvspecday.HeaderRow.FindControl("chkalladjus")).Checked = false;
                    ((CheckBox)this.grvspecday.HeaderRow.FindControl("chkallView")).Checked = false;



                    index = (this.grvspecday.PageSize) * (this.grvspecday.PageIndex) + i;
                    dt.Rows[index]["eot"] = "True";
                    dt.Rows[index]["advleave"] = "False";
                    dt.Rows[index]["adjleav"] = "False";
                }


            }

            else
            {
                for (i = 0; i < this.grvspecday.Rows.Count; i++)
                {
                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkeot")).Checked = false;
                    index = (this.grvspecday.PageSize) * (this.grvspecday.PageIndex) + i;
                    dt.Rows[index]["eot"] = "False";


                }

            }

            ViewState["GetAllSpecialDay"] = dt;

        }

        protected void chkallView_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["GetAllSpecialDay"];
            int i, index;
            if (((CheckBox)this.grvspecday.HeaderRow.FindControl("chkallView")).Checked)
            {

                for (i = 0; i < this.grvspecday.Rows.Count; i++)
                {
                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkPermit")).Checked = true;


                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkeot")).Checked = false;
                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkadj")).Checked = false;


                    ((CheckBox)this.grvspecday.HeaderRow.FindControl("chkalladjus")).Checked = false;
                    ((CheckBox)this.grvspecday.HeaderRow.FindControl("checkAllEot")).Checked = false;




                    index = (this.grvspecday.PageSize) * (this.grvspecday.PageIndex) + i;
                    dt.Rows[index]["advleave"] = "True";
                    dt.Rows[index]["eot"] = "False";
                    dt.Rows[index]["adjleav"] = "False";

                }


            }

            else
            {
                for (i = 0; i < this.grvspecday.Rows.Count; i++)
                {
                    ((CheckBox)this.grvspecday.Rows[i].FindControl("chkPermit")).Checked = false;
                    index = (this.grvspecday.PageSize) * (this.grvspecday.PageIndex) + i;
                    dt.Rows[index]["advleave"] = "False";


                }

            }

            ViewState["GetAllSpecialDay"] = dt;
        }

        protected void chkeot_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["GetAllSpecialDay"];


            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.Parent.Parent;
            int rowid = gr.RowIndex;
            if (chk.Checked == true)
            {
                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkeot")).Checked = true;
                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkPermit")).Checked = false;
                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkadj")).Checked = false;
                dt.Rows[rowid]["eot"] = (chk.Checked == true ? "True" : "False");
                dt.Rows[rowid]["advleave"] = "False";
                dt.Rows[rowid]["adjleav"] = "False";
            }
            else
            {
                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkeot")).Checked = false;
                dt.Rows[rowid]["eot"] = (chk.Checked == true ? "True" : "False");
            }
            ViewState["GetAllSpecialDay"] = dt;

        }

        protected void chkPermit_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["GetAllSpecialDay"];


            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.Parent.Parent;
            int rowid = gr.RowIndex;
            if (chk.Checked == true)
            {
                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkeot")).Checked = false;
                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkPermit")).Checked = true;
                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkadj")).Checked = false;
                dt.Rows[rowid]["eot"] = "False";
                dt.Rows[rowid]["advleave"] = (chk.Checked == true ? "True" : "False");
                dt.Rows[rowid]["adjleav"] = "False";

            }

            else
            {

                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkPermit")).Checked = false;
                dt.Rows[rowid]["advleave"] = (chk.Checked == true ? "True" : "False");

            }

            ViewState["GetAllSpecialDay"] = dt;
        }

        protected void chkadj_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["GetAllSpecialDay"];


            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.Parent.Parent;
            int rowid = gr.RowIndex;
            if (chk.Checked == true)
            {
                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkeot")).Checked = false;
                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkPermit")).Checked = false;
                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkadj")).Checked = true;
                dt.Rows[rowid]["eot"] = "False";
                dt.Rows[rowid]["advleave"] = "False";
                dt.Rows[rowid]["adjleav"] = (chk.Checked == true ? "True" : "False");
            }
            else
            {

                ((CheckBox)this.grvspecday.Rows[rowid].FindControl("chkadj")).Checked = false;
                dt.Rows[rowid]["adjleav"] = (chk.Checked == true ? "True" : "False");

            }

            ViewState["GetAllSpecialDay"] = dt;
        }
    }
}