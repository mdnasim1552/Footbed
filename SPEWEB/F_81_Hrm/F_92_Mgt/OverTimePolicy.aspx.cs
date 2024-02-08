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
    public partial class OverTimePolicy : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Over Time Policy";
                GetWorkStation();
                GetAllOrganogramList();
                this.ShowPolicy();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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

            this.GetDesigAPolicy();
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


        private void GetDesigAPolicy()
        {


            string comcod = this.GetCompCode();

            string company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string depart = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString().Substring(0, 9)) + "%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDESIGAPOLICY", company, divison, depart, section, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddldesig.DataTextField = "desig";
            this.ddldesig.DataValueField = "desigid";
            this.ddldesig.DataSource = ds1.Tables[0];
            this.ddldesig.DataBind();



            // Policy
            this.ddlpolicy.DataTextField = "gdesc";
            this.ddlpolicy.DataValueField = "gcod";
            this.ddlpolicy.DataSource = ds1.Tables[1];
            this.ddlpolicy.DataBind();
            ds1.Dispose();
        }




        private void ShowPolicy()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "SHOWDESIGPOLICY", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblPolicy"] = ds1.Tables[0];

            this.Data_Bind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }




        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblPolicy"];
            this.gvovpolicy.DataSource = tbl1;
            this.gvovpolicy.DataBind();

        }

        private void Session_tbltbPreLink_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblPolicy"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvovpolicy.Rows.Count; j++)
            {
                string rate = Convert.ToDouble("0" + ((TextBox)this.gvovpolicy.Rows[j].FindControl("txtgvrate")).Text.Trim()).ToString();

                TblRowIndex2 = (this.gvovpolicy.PageIndex) * this.gvovpolicy.PageSize + j;
                tbl1.Rows[TblRowIndex2]["rate"] = rate;
            }
            ViewState["tblPolicy"] = tbl1;
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tblPolicy"];
            string secid = this.ddlSection.SelectedValue.ToString();
            string desigid = this.ddldesig.SelectedValue.ToString();
            string gcod = this.ddlpolicy.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("secid = '" + secid + "' and desigid='" + desigid + "' and gcod='" + gcod + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["secid"] = this.ddlSection.SelectedValue.ToString();
                dr1["section"] = this.ddlSection.SelectedItem.Text;
                dr1["desigid"] = this.ddldesig.SelectedValue.ToString();
                dr1["desigid"] = this.ddldesig.SelectedValue.ToString();
                dr1["desig"] = this.ddldesig.SelectedItem.Text;
                dr1["gcod"] = this.ddlpolicy.SelectedValue.ToString();
                dr1["gdesc"] = this.ddlpolicy.SelectedItem.Text.Trim();
                dr1["rate"] = 0.00;
                tbl1.Rows.Add(dr1);
            }
            ViewState["tblPolicy"] = tbl1;
            this.Data_Bind();
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lblmsg1.Text = "You have no permission";
            //    return;
            //}


            string comcod = this.GetCompCode();
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tblPolicy"];
            foreach (DataRow dr in tbl1.Rows)
            {
                string secid = dr["secid"].ToString();
                string desigid = dr["desigid"].ToString();
                string gcod = dr["gcod"].ToString();
                string rate = dr["rate"].ToString();

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSORUPDATEEMPOVERTRATE", secid, desigid, gcod, rate, "", "", "", "", "", "", "", "", "", "", "", "");

                //INSERTUPPAYROLLINK 
                if (!result)
                {
                    this.lblmsg1.Text = HRData.ErrorObject["Msg"].ToString();
                    return;
                }
            }
            this.lblmsg1.Text = "Data Updated successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Salary Sheet user Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        protected void Imgbtndesig_Click(object sender, EventArgs e)
        {
            this.GetDesigAPolicy();
        }



        protected void gvovpolicy_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //DataTable dt = (DataTable)Session["tblPolicy"];
            //string actcode = ((Label)this.gvovpolicy.Rows[e.RowIndex].FindControl("lblgvCompCod")).Text.Trim();
            //bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "DELETEPAYLINK", actcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //    return;

            //this.lblmsg1.Text = "Updated Successfully";
            this.ShowPolicy();
        }
    }
}