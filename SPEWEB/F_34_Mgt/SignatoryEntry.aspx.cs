using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_34_Mgt
{
    public partial class SignatoryEntry : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.getListModulename();
                this.getListEpm();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Signatory Entry";
            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void getListModulename()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            ProcessAccess ulogin = new ProcessAccess();
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE", usrid, "", "", "", "", "", "", "", "");

            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = ("flag='True'");//usrper

            this.ddlModuleName.DataTextField = "modulename";
            this.ddlModuleName.DataValueField = "moduleid";
            this.ddlModuleName.DataSource = dv.ToTable();// ds1.Tables[0];
            this.ddlModuleName.DataBind();

        }
        private void getListEpm()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", "%", "%%", "%%", "", "9401%", "%%", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            //this.ddlEmpName.SelectedValue = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblempname"];
            this.grvsign.DataSource = dt;
            this.grvsign.DataBind();

        }
        private void GetSingEmpName()
        {
            string comcod = this.GetComeCode();
            string Moduleid = this.ddlModuleName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPSIGLST", Moduleid, "%", "%", "%", "", "", "", "", "");
            Session["tblempname"] = ds3.Tables[0];
            Data_Bind();
        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.GetSingEmpName();
            this.ddlEmpName.Visible = true;
            this.lnkadd.Visible = true;
            this.lblemp.Visible = true;
        }
        protected void lnkadd_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblempname"];
            string[] emps = this.ddlEmpName.SelectedItem.ToString().Trim().Split(',');
            string Moduleid = this.ddlModuleName.SelectedValue.ToString();
            DataRow[] dr = dt.Select("comcod='" + this.GetComeCode() + "' and idcard='"+ emps[1].ToString().Trim() + "' and signtype='" + Moduleid + "'");

            if (dr.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Selected Employee Already Added');", true);
                return;
            }
            else
            {
                DataRow dr1 = dt.NewRow();
                dr1["comcod"] = this.GetComeCode();
                dr1["signtype"] = Moduleid;
                dr1["idcard"] = emps[1].ToString().Trim();
                dr1["signame"] = emps[0].ToString();
                dr1["signdesig"] = emps[3].ToString();

                dt.Rows.Add(dr1);
            }
            Session["tblempname"] = dt;
            Data_Bind();
        }

        protected void lbtnUpPer_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblempname"];
            string comcod = this.GetComeCode();
            for (int i=0; i<dt.Rows.Count; i++)
            {
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSUPDATEEMPSIG", dt.Rows[i]["idcard"].ToString(), dt.Rows[i]["signame"].ToString(), dt.Rows[i]["signdesig"].ToString(), dt.Rows[i]["signtype"].ToString(), "",  "", "", "", "", "", "");
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                }
            }
        }
        protected void grvsign_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblempname"];
            string comcod = this.GetComeCode();
            string idcard = ((Label)this.grvsign.Rows[e.RowIndex].FindControl("lblgridcard")).Text.Trim();
            string signtype = ((Label)this.grvsign.Rows[e.RowIndex].FindControl("lblgrsigntype")).Text.Trim();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEEMPSIG", idcard, signtype, "", "", "", "", "", "", "");
            Session["tblempname"] = ds.Tables[0];
            Data_Bind();
        }
    }
}