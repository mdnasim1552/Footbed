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

namespace SPEWEB.F_81_Hrm.F_93_AnnInc
{
    public partial class lnkpagecarsuballow : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetWorkStation();
                GetAllOrganogramList();
                GetCarSubdata();
                CommonButton();
            }
        }
        private void CommonButton()
        {


            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            // ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFinalUpdate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);

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

            this.ddlDivision_SelectedIndexChanged(null, null);

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

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

        private void GetCarSubdata()
        {
            string comcod = this.GetCompCode();

            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETCARSUBALLOWSETUPDATA", Company, divison, deptname, section);
            if (ds1 == null)
            {
                this.gvcarsubsetup.DataSource = null;
                this.gvcarsubsetup.DataBind();
                return;

            }

            ViewState["tblcarsuballowsetup"] = ds1.Tables[0];
            ViewState["tblbacklog"] = ds1.Tables[0];
            this.Data_Bind();



        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblcarsuballowsetup"];

            gvcarsubsetup.DataSource = dt;
            gvcarsubsetup.DataBind();
        }

        private void saveData()
        {
            DataTable dt = (DataTable)ViewState["tblcarsuballowsetup"];

            for (int i = 0; i < this.gvcarsubsetup.Rows.Count; i++)
            {
                string empid = ((Label)this.gvcarsubsetup.Rows[i].FindControl("lgempid")).Text;
                double caramt = Math.Round(Convert.ToDouble("0" + ((TextBox)this.gvcarsubsetup.Rows[i].FindControl("txtCaramt")).Text.Trim()), 0);
                double subamt = Math.Round(Convert.ToDouble("0" + ((TextBox)this.gvcarsubsetup.Rows[i].FindControl("txtsubamt")).Text.Trim()), 0);
                double gross = Math.Round(Convert.ToDouble("0" + ((Label)this.gvcarsubsetup.Rows[i].FindControl("lblincgross")).Text.Trim()), 0);
                double tsubamt = (gross - caramt) * 25 / 100;

                dt.Rows[i]["empid"] = empid;
                dt.Rows[i]["caramt"] = caramt;
                dt.Rows[i]["subamt"] = Math.Round(tsubamt, 0);
            }

            ViewState["tblcarsuballowsetup"] = dt;
        }


        protected void lnkbtnShow_OnClick(object sender, EventArgs e)
        {
            GetCarSubdata();
        }

        protected void lnkFinalUpdate_Click(object sender, EventArgs e)
        {


            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();
                string comcod = this.GetCompCode();
                saveData();
                DataTable dt = (DataTable)ViewState["tblcarsuballowsetup"];
                //  DataTable dtbacklog = (DataTable)ViewState["tblbacklog"];

                //DataSet ds1 = new DataSet("ds1");
                //ds1.Tables.Add(dt);
                //ds1.Tables[0].TableName = "tbl1";
                int i;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string carallow = dt.Rows[i]["caramt"].ToString();
                    string suballow = dt.Rows[i]["subamt"].ToString();

                    bool result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTCARSUBALLOWEMP", empid, carallow, suballow);
                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                }



            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
            }
        }

        protected void txtCaramt_OnTextChanged(object sender, EventArgs e)
        {
            this.saveData();
            Data_Bind();
        }
        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.saveData();
            Data_Bind();
        }

    }
}