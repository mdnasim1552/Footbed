using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;

namespace SPEWEB.F_21_GAcc
{
    public partial class LcCodeBook : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GeProjectMainCode();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Code Information";
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GeProjectMainCode()
        {


            string comcod = this.GetComeCode();
            string filter = "%" + this.txtsrchMainCode.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROMAINCODE", filter, "", "", "", "", "", "", "", "");
            this.ddlMainCode.DataSource = ds1.Tables[0];
            this.ddlMainCode.DataTextField = "actdesc";
            this.ddlMainCode.DataValueField = "actcode";
            this.ddlMainCode.DataBind();
            this.GetProjectSubCode1();
            ds1.Dispose();

        }

        private void GetProjectSubCode1()
        {
            string comcod = this.GetComeCode();
            string ProMainCode = this.ddlMainCode.SelectedValue.ToString().Substring(0, 2);
            string filter = "%" + this.txtsrchMainCode.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROSUBCODE1", ProMainCode, filter, "", "", "", "", "", "", "");
            this.ddlSub1.DataSource = ds1.Tables[0];
            this.ddlSub1.DataTextField = "actdesc";
            this.ddlSub1.DataValueField = "actcode";
            this.ddlSub1.DataBind();
            this.GetProjectSubCode2();
            ds1.Dispose();

        }

        private void GetProjectSubCode2()
        {
            string comcod = this.GetComeCode();
            string ProSubCode1 = this.ddlSub1.SelectedValue.ToString().Substring(0, 4);
            string filter = "%" + this.txtsrchMainCode.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROSUBCODE2", ProSubCode1, filter, "", "", "", "", "", "", "");
            this.ddlSub2.DataSource = ds1.Tables[0];
            this.ddlSub2.DataTextField = "actdesc";
            this.ddlSub2.DataValueField = "actcode";
            this.ddlSub2.DataBind();
            this.GetProjectDetailsCode();
            ds1.Dispose();

        }

        private void GetProjectDetailsCode()
        {
            ViewState.Remove("tblprolist");
            string comcod = this.GetComeCode();
            string ProSubCode2 = this.ddlSub2.SelectedValue.ToString().Substring(0, 8);
            string filter = "%" + this.txtsrchMainCode.Text + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPRODETAILSCODE", ProSubCode2, filter, "", "", "", "", "", "", "");
            this.ddlProjectList.DataSource = ds1.Tables[0];
            this.ddlProjectList.DataTextField = "actdesc";
            this.ddlProjectList.DataValueField = "actcode";
            this.ddlProjectList.DataBind();
            ViewState["tblprolist"] = ds1.Tables[0];
            ds1.Dispose();
            this.ddlProjectList_SelectedIndexChanged(null, null);


        }

        protected void chkNewProject_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkNewProject.Checked)
            {
                this.ddlProjectList.Items.Clear();
                // this.txtProjectName.Text = "";
                this.txtShortName.Text = "";
            }

        }
        protected void imgbtnMainCode_Click(object sender, EventArgs e)
        {
            this.GeProjectMainCode();
        }
        protected void ingbtnSub1_Click(object sender, EventArgs e)
        {
            this.GetProjectSubCode1();

        }
        protected void imgbtnSub2_Click(object sender, EventArgs e)
        {
            this.GetProjectSubCode2();

        }
        protected void mgbtnPreDetails_Click(object sender, EventArgs e)
        {
            if (!(this.chkNewProject.Checked))
                this.GetProjectDetailsCode();

        }
        protected void ddlMainCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectSubCode1();
        }
        protected void ddlSub1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectSubCode2();
        }

        protected void ddlSub2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectDetailsCode();

        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            this.lblmsg.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string SubCode2 = this.ddlSub2.SelectedValue.ToString().Trim().Substring(0, 8);
            string ProjectName = this.txtProjectName.Text;
            string ShortName = this.txtShortName.Text.Trim();
            string type = this.txtType.Text.Trim();
            bool result = true;



            if (this.ddlProjectList.Items.Count > 0)
            {
                string projectcode = this.ddlProjectList.SelectedValue.ToString();
                result = accData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "UPDATEPROJECT", projectcode, ProjectName, ShortName, userid, "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                result = accData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTPROJECT", SubCode2, ProjectName, ShortName, userid, "", "", "", "", "", "", "", "", "", "", "");
            }
            if (result)
            {
                this.lblmsg.Text = "Updated Successfully";
                //this.txtProjectName.Text = "";
                //this.txtShortName.Text = "";
            }
            else
            {
                this.lblmsg.Text = "Sorry, Data Updated Fail";
                //this.lblmsg.ForeColor
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void ddlProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlProjectList.Items.Count == 0)
                return;
            string procode = this.ddlProjectList.SelectedValue.ToString();
            this.txtProjectName.Text = this.ddlProjectList.SelectedItem.Text.Trim().ToString().Substring(13);
            this.txtShortName.Text = (((DataTable)ViewState["tblprolist"]).Select("actcode='" + procode + "'"))[0]["acttdesc"].ToString();
            this.txtType.Text = (((DataTable)ViewState["tblprolist"]).Select("actcode='" + procode + "'"))[0]["acttype"].ToString();
            this.lblmsg.Text = "";
        }

    }
}