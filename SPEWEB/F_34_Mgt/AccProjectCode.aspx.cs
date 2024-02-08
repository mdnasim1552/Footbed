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

namespace SPEWEB.F_34_Mgt
{
    public partial class AccProjectCode : System.Web.UI.Page
    {
        ProcessAccess mgtData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GeProjectMainCode();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Create Master L/C Code";
                this.CommonButton();
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(btnForward_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);

            // ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }



        private void CommonButton()
        {
           

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;


            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Save";

        }

        private void GeProjectMainCode()
        {


            string comcod = this.GetComeCode();
            string filter =  "%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROMAINCODE", filter, "", "", "", "", "", "", "", "");
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
            string filter =  "%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROSUBCODE1", ProMainCode, filter, "", "", "", "", "", "", "");
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
            string filter =  "%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROSUBCODE2", ProSubCode1, filter, "", "", "", "", "", "", "");
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
            string filter =  "%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPRODETAILSCODE", ProSubCode2, filter, "", "", "", "", "", "", "");
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
                this.txtProjectName.Text = "";
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
            
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string SubCode2 = this.ddlSub2.SelectedValue.ToString().Trim().Substring(0, 8);
            string ProjectName = this.txtProjectName.Text;
            string ShortName = this.txtShortName.Text.Trim();
            
            string article = this.TextBoxDesc.Text.ToString();
            string inqno = this.LbInqno.Text.ToString();
            string mlccod = this.LbMlccod.Text.ToString();
            string sdino = this.LbSdino.Text.ToString();
            bool result = true;

            string articleChk = this.CbForArticle.Checked.ToString();



            if (this.ddlProjectList.Items.Count > 0)
            {
                string projectcode = this.ddlProjectList.SelectedValue.ToString();
                result = mgtData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "UPDATEPROJECT", projectcode, ProjectName, ShortName, userid, articleChk, article, inqno, mlccod, sdino, "", "", "", "", "", "", "");
            }
            else
            {
                result = mgtData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTPROJECT", SubCode2, ProjectName, ShortName, userid, articleChk, article, inqno, mlccod, sdino, "", "", "", "", "", "", "", "");
            }
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void ddlProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlProjectList.Items.Count == 0)
                return;

            string comcod = this.GetComeCode();
            string procode = this.ddlProjectList.SelectedValue.ToString();
            this.txtProjectName.Text = this.ddlProjectList.SelectedItem.Text.Trim().ToString().Substring(13);
            this.txtShortName.Text = (((DataTable)ViewState["tblprolist"]).Select("actcode='" + procode + "'"))[0]["acttdesc"].ToString();
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GET_WIP_RELETED_ARTICLENAME", procode, "", "", "", "", "", "", "");

            if (ds1 == null)
                return;


            if (comcod == "5305" || comcod == "5306")
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.LbInqno.Text = ds1.Tables[0].Rows[0]["inqno"].ToString();
                    this.LbMlccod.Text = ds1.Tables[0].Rows[0]["mlccod"].ToString();
                    this.LbSdino.Text = ds1.Tables[0].Rows[0]["sdino"].ToString();
                    this.TextBoxDesc.Text = ds1.Tables[0].Rows[0]["article"].ToString();
                }
                else
                {
                    this.LbInqno.Text = "";
                    this.LbMlccod.Text = "";
                    this.LbSdino.Text = "";
                    this.TextBoxDesc.Text = "";
                }
            }

            else
            {
                this.LbInqno.Text = "";
                this.LbMlccod.Text = "";
                this.LbSdino.Text = "";
                this.TextBoxDesc.Text = "";

                this.TextBoxDesc.Visible = false;
                this.CbForArticle.Visible = false;
                this.Label2.Visible = false;
            }

        }
    }
}