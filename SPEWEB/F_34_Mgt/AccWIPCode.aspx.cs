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
    public partial class AccWIPCode : System.Web.UI.Page
    {
        ProcessAccess mgtData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                string url = HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp);
                int indexOfExt = 0;
                if (url.Contains(".aspx"))
                {
                    indexOfExt = url.IndexOf(".aspx");
                    url = url.Substring(0, 41) + url.Substring(41 + 5);
                }
                if (!ASTUtility.PagePermission(url, (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                this.GetProjectSubCode1();

                ((Label)this.Master.FindControl("lblTitle")).Text = "WIP Code Opening";

            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetProjectSubCode1()
        {
            string comcod = this.GetComeCode();
            //string ProMainCode = this.ddlMainCode.SelectedValue.ToString().Substring(0,2);
            string type = this.Request.QueryString["Type"].ToString();
            string filter = (type == "FgWIP") ? "1601%" : (type == "MPurWIP") ? "1698%" : (type == "Adjlc") ? "1498%" : (type == "Adjprd") ? "1697%" : "1651%";


            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROSUBCODE1", filter.Substring(0, 2), filter, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
                return;
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
            string filter = "%" + this.txtSrcSub1.Text + "%";
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
            string filter = "%" + this.txtSrcSub2.Text + "%";
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
                // this.txtProjectName.Text = "";
                this.txtShortName.Text = "";
            }

        }
        //protected void imgbtnMainCode_Click(object sender, EventArgs e)
        //{
        //    this.GeProjectMainCode();
        //}
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
            bool result = true;



            if (this.ddlProjectList.Items.Count > 0)
            {
                string projectcode = this.ddlProjectList.SelectedValue.ToString();
                result = mgtData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "UPDATEPROJECT", projectcode, ProjectName, ShortName, userid, "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                result = mgtData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTPROJECT", SubCode2, ProjectName, ShortName, userid, "", "", "", "", "", "", "", "", "", "", "");
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
            this.lblmsg.Text = "";
        }
    }
}