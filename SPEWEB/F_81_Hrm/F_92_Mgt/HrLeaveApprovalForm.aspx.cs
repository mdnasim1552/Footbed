using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SPEENTITY;
using System.Collections;
using SPELIB;
using System.Data.SqlClient;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class HrLeaveApprovalForm : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "HR Approval Setup";

                
                this.GetDepartment();
                //this.GetEmployeeName();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        public void LoadOrderDapp()
        {
            string comcod = GetCompCode();
            string dptName = this.ddlWstation.SelectedValue.ToString();
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "SELECTLEAVEAPP", dptName, "", "", "", "", "", "", "", "");

            DataTable UserInfoTable = ds.Tables[0];
            DataTable usermgt = ds.Tables[0];


            DataView dvguser = UserInfoTable.DefaultView;

            dvguser.RowFilter = ("userrole='gen' ");

            UserInfoTable = dvguser.ToTable();

            DataView dvmguser = usermgt.DefaultView;
            dvmguser.RowFilter = ("userrole='mgt' ");



            usermgt = dvmguser.ToTable();



            ViewState["UserInfoTable"] = UserInfoTable;
            ViewState["UserInfoTablemgt"] = usermgt;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetDepartment()
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


        }

        private void Getuser()
        {

            string comcod = GetCompCode();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETUSERNAMELIST", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();

            this.ddlUserListmgt.DataTextField = "usrsname";
            this.ddlUserListmgt.DataValueField = "usrid";
            this.ddlUserListmgt.DataSource = ds1.Tables[0];
            this.ddlUserListmgt.DataBind();


            ds1.Dispose();
        }






        protected void Select_Click(object sender, EventArgs e)
        {
            string userName = ddlUserList.SelectedItem.Text;
            string usrid = ddlUserList.SelectedItem.Value;
            string comcod = GetCompCode();

            string centrid = this.ddlWstation.SelectedValue.ToString();
            string actdesc = this.ddlWstation.SelectedItem.Text;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];

            DataRow[] dr = UserInfoTable.Select("usrid='" + usrid + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = UserInfoTable.NewRow();
                dr1["usrid"] = usrid;
                dr1["usrsname"] = userName;
                dr1["centrid"] = centrid;
                dr1["actdesc"] = actdesc;
                dr1["comcod"] = comcod;
                dr1["userrole"] = "gen";
                dr1["slno"] = gvProLinkInfo.Rows.Count + 1;
                UserInfoTable.Rows.Add(dr1);
            }

            ViewState["UserInfoTable"] = UserInfoTable;
            gvProLinkInfo_DataBind();
        }
        protected void SelectAll_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode(); ;
            string centrid = this.ddlWstation.SelectedValue.ToString();
            string actdesc = this.ddlWstation.SelectedItem.Text;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];
            for (int i = 0; i < this.ddlUserList.Items.Count; i++)
            {
                string usrid = this.ddlUserList.Items[i].Value;
                string userName = this.ddlUserList.Items[i].Text;
                DataRow[] dr = UserInfoTable.Select("usrid='" + usrid + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = UserInfoTable.NewRow();
                    dr1["usrid"] = usrid;
                    dr1["usrsname"] = userName;
                    dr1["centrid"] = centrid;
                    dr1["actdesc"] = actdesc;
                    dr1["comcod"] = comcod;
                    dr1["userrole"] = "gen";
                    dr1["slno"] = gvProLinkInfo.Rows.Count + 1;
                    UserInfoTable.Rows.Add(dr1);
                }
            }


            ViewState["UserInfoTable"] = UserInfoTable;
            gvProLinkInfo_DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnSelectmgt_Click(object sender, EventArgs e)
        {
            string userName = ddlUserListmgt.SelectedItem.Text;
            string usrid = ddlUserListmgt.SelectedItem.Value;
            string comcod = GetCompCode();

            string centrid = this.ddlWstation.SelectedValue.ToString();
            string actdesc = this.ddlWstation.SelectedItem.Text;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTablemgt"];

            DataRow[] dr = UserInfoTable.Select("usrid='" + usrid + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = UserInfoTable.NewRow();
                dr1["usrid"] = usrid;
                dr1["usrsname"] = userName;
                dr1["centrid"] = centrid;
                dr1["actdesc"] = actdesc;
                dr1["comcod"] = comcod;
                dr1["userrole"] = "mgt";
                dr1["slno"] = gvProLinkInfo.Rows.Count + 1;
                UserInfoTable.Rows.Add(dr1);
            }

            ViewState["UserInfoTablemgt"] = UserInfoTable;
            gvProLinkInfo_DataBind();
        }


        protected void alllbtnSelectmgt_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode(); ;
            string centrid = this.ddlWstation.SelectedValue.ToString();
            string actdesc = this.ddlWstation.SelectedItem.Text;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTablemgt"];
            for (int i = 0; i < this.ddlUserListmgt.Items.Count; i++)
            {
                string usrid = this.ddlUserListmgt.Items[i].Value;
                string userName = this.ddlUserListmgt.Items[i].Text;
                DataRow[] dr = UserInfoTable.Select("usrid='" + usrid + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = UserInfoTable.NewRow();
                    dr1["usrid"] = usrid;
                    dr1["usrsname"] = userName;
                    dr1["centrid"] = centrid;
                    dr1["actdesc"] = actdesc;
                    dr1["comcod"] = comcod;
                    dr1["userrole"] = "mgt";
                    dr1["slno"] = gvProLinkInfo.Rows.Count + 1;
                    UserInfoTable.Rows.Add(dr1);
                }
            }


            ViewState["UserInfoTablemgt"] = UserInfoTable;
            gvProLinkInfo_DataBind();
        }


        protected void gvProLinkInfo_DataBind()
        {
            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];
            DataTable UserInfoTablemgt = (DataTable)ViewState["UserInfoTablemgt"];

            this.gvProLinkInfo.DataSource = UserInfoTable;
            this.gvProLinkInfo.DataBind();

            this.gvMgtUser.DataSource = UserInfoTablemgt;
            this.gvMgtUser.DataBind();

        }



        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            
            string dpt = this.ddlWstation.SelectedValue.ToString();
            string comcod = GetCompCode(); ;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];
            if (!IsValid(UserInfoTable))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid Serial No');", true);
                return;
            }

            if (HasDup(UserInfoTable))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Duplicate Serial No');", true);
                return;
            }


            bool result = false;
            //below DELETEUSR calltype why use i don't know (@@nahid-20190429) 
            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELETEUSR", dpt, "gen", "", "", "", "", "", "", "", "", "", "", "");

            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {

                //string userid = UserInfoTable.Rows[i]["usrid"].ToString();
                string userid = ((Label)gvProLinkInfo.Rows[i].FindControl("userid")).Text;
                string slNum = ((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text;
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTLEAVEAPP", slNum, userid, dpt, "gen", "", "", "", "", "", "", "", "", "", "", "");


            }
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed!');", true);
                
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);



        }
        public bool IsValid(DataTable UserInfoTable)
        {
            int slNum = 0;
            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {
                try
                {
                    slNum = Convert.ToInt32(((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text);
                }
                catch (Exception)
                {
                    return false;
                }


            }

            return true;
        }
        public bool IsUnique(DataTable UserInfoTable, string str, int index)
        {

            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {
                string temp = ((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text;
                if (str == temp && index != i)
                    return false;
            }

            return true;
        }

        public bool HasDup(DataTable UserInfoTable)
        {
            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {
                string temp = ((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text;
                if (!IsUnique(UserInfoTable, temp, i))
                    return true;
            }
            return false;
        }


        protected void gvProLinkInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            string centrid = this.ddlWstation.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["UserInfoTable"];
            string userid = ((Label)this.gvProLinkInfo.Rows[e.RowIndex].FindControl("userid")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELLEAVEAPP", userid, centrid, "gen", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProLinkInfo.PageSize) * (this.gvProLinkInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                ViewState["UserInfoTable"] = dt;
                gvProLinkInfo_DataBind();
            }

        }

        protected void gvMgtUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            string centrid = this.ddlWstation.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["UserInfoTablemgt"];
            string userid = ((Label)this.gvMgtUser.Rows[e.RowIndex].FindControl("useridMgt")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELLEAVEAPP", userid, centrid, "mgt", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProLinkInfo.PageSize) * (this.gvProLinkInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                ViewState["UserInfoTablemgt"] = dt;
                gvProLinkInfo_DataBind();
            }

        }
        protected void MgtUserDeleteAll_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["UserInfoTablemgt"];
            string centrid = this.ddlWstation.SelectedValue.ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELALLLEAVEAPP", centrid, "mgt", "", "", "", "", "", "", "", "", "", "", "", "", "");
            BindGrid();
        }

        protected void lbtnDeleteAll_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["UserInfoTable"];
            string centrid = this.ddlWstation.SelectedValue.ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELALLLEAVEAPP", centrid, "gen", "", "", "", "", "", "", "", "", "", "", "", "", "");
            BindGrid();
        }


        //LinkButton4

        protected void GetcntresByNam(object sender, EventArgs e)
        {
            //GetCentres();
            lbtnOkOrNew.Visible = true;


        }
        protected void GetUserInfo(object sender, EventArgs e)
        {
            if (lbtnOkOrNew.Text == "Ok")
            {
                if (ddlWstation.Items.Count > 0)
                {
                    this.Panel2.Visible = true;
                    this.Panel1.Visible = true;
                    lbtnOkOrNew.Text = "New";
                    this.LoadOrderDapp();
                    ddlWstation.Enabled = false;
                    //ddlEmploye.Enabled = false;

                    BindGrid();
                    this.Getuser();

                }

                else
                {
                    ddlWstation.Enabled = true;
                    //ddlEmploye.Enabled = true;
                    this.Panel2.Visible = false;
                    this.Panel1.Visible = false;

                }

                return;
            }

            if (lbtnOkOrNew.Text == "New")
            {
                ddlWstation.Enabled = true;
                //ddlEmploye.Enabled = true;

                lbtnOkOrNew.Text = "Ok";
                this.Panel2.Visible = false;
                this.Panel1.Visible = false;


            }

        }
        protected void BindGrid()
        {
            LoadOrderDapp();
            gvProLinkInfo_DataBind();
        }


        protected void lbtnUpdateMgt_OnClick(object sender, EventArgs e)
        {
            
            string dpt = this.ddlWstation.SelectedValue.ToString();
            string comcod = GetCompCode(); ;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTablemgt"];
            if (!IsValid(UserInfoTable))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Invalid Serial No');", true);
                return;
            }

            if (HasDup(UserInfoTable))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Duplicate Serial No');", true);
                return;
            }

            bool result = false;
            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELETEUSR", dpt, "mgt", "", "", "", "", "", "", "", "", "", "", "");

            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {

                //string userid = UserInfoTable.Rows[i]["usrid"].ToString();
                string userid = ((Label)gvMgtUser.Rows[i].FindControl("useridMgt")).Text;
                string slNum = ((TextBox)gvMgtUser.Rows[i].FindControl("slnoMgt")).Text;
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTLEAVEAPP", slNum, userid, dpt, "mgt", "", "", "", "", "", "", "", "", "", "", "");


            }
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Failed!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


        }
    }
}