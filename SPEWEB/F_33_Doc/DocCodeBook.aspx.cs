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

namespace SPEWEB.F_33_Doc
{
    public partial class DocCodeBook : System.Web.UI.Page
    {

        ProcessRAccess Rprss = new ProcessRAccess();
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "DOCUMENT INFORMATION FIELD VIEW/EDIT";
                GetUserName();
            }
            if (this.ddlOthersBook.Items.Count == 0)
                this.Load_CodeBooList();

           
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void Load_CodeBooList()
        {

            try
            {
                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = hst["comcod"].ToString();
                string comcod = this.GetCompCode();
                string Type = this.Request.QueryString["Type"].ToString().Trim();

                DataSet dsone = this.da.GetTransInfo(comcod, "SP_ENTRY_DOC", "GET_DOC_GINF_HEAD", Type,
                                "", "", "", "", "", "", "", "");
                if (dsone == null)
                    return;

                this.ddlOthersBook.DataTextField = "gdesc";
                this.ddlOthersBook.DataValueField = "gcod";
                this.ddlOthersBook.DataSource = dsone.Tables[0];
                this.ddlOthersBook.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ex.Message +"');", true);

                
            }



        }

        private void GetUserName()
        {


            string comcod = this.GetCompCode();
            string txtUser = "%";
            DataSet ds3 = da.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERNAME", txtUser, "", "", "", "", "", "", "", "");
            ViewState["UserList"] = ds3.Tables[0];

        }
        //private void LoadGrid()
        //{
        //    //Hashtable hst = (Hashtable)Session["tblLogin"];
        //    //string comcod = hst["comcod"].ToString();
        //    string comcod = this.GetCompCode();
        //    string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
        //    string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();

        //    DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTINFO", tempddl1,
        //                    tempddl2, "", "", "", "", "", "", "");
        //    if (ds1.Tables[0].Rows.Count == 0)
        //    {
        //        this.lnknewentry.Visible = true;

        //    }
        //    Session["storedata"] = ds1.Tables[0];
        //    this.grvacc_DataBind();

        //}


        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {


            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();

        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();

            DataTable dt = (DataTable)ViewState["UserList"];
            DataTable dt2 = (DataTable)Session["storedata"];
            string usrcode = dt2.Rows[e.NewEditIndex]["usrcode"].ToString();
            DropDownList userlist = (DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("DdlUser");
            userlist.DataTextField = "usrname";
            userlist.DataValueField = "usrid";
            userlist.DataSource = dt;
            userlist.DataBind();
            userlist.SelectedValue=usrcode;

            string timeflag = dt2.Rows[e.NewEditIndex]["timeflag"].ToString();

            DropDownList ddltimeflag = (DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlStatus");
            ddltimeflag.SelectedValue = timeflag;

            string time = dt2.Rows[e.NewEditIndex]["incmtime"].ToString()??"10:00 AM";

            DropDownList ddlIncTime = (DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("ddlIncTime");
            ddlIncTime.SelectedValue = time.Substring(0,5);

            DropDownList TimeDaylight = (DropDownList)grvacc.Rows[e.NewEditIndex].FindControl("TimeDaylight");
            TimeDaylight.SelectedValue = ASTUtility.Right(time, 2);



        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
           
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();
            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2.Substring(0,2)+gcode2.Substring(3,3);
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string slnum = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvSlnum")).Text.Trim();
            string usrcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("DdlUser")).Text.Trim();
            string time = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlIncTime")).Text.Trim();
            string daylight = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("TimeDaylight")).Text.Trim();
            string status = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlStatus")).Text.Trim();
            string color = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvColor")).Text.Trim();

            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_DOC", "INSERT_DOC_GCOD_INF", tgcod,
                           gdesc, slnum, usrcode, time+' '+daylight, status, color, "", "", "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something Went Wrong!!');", true);

            }
            this.grvacc.EditIndex = -1;
            this.ShowInformation();
            this.grvacc_DataBind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Doc Code Book";
                string eventdesc = "Update Document CodeBook";
                string eventdesc2 = tgcod;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];
                if(this.ddlOthersBook.SelectedValue.ToString().Substring(0,2) != "99")
                {
                    this.grvacc.Columns[5].Visible = false;
                    this.grvacc.Columns[6].Visible = false;
                    this.grvacc.Columns[7].Visible = false;
                    this.grvacc.Columns[8].Visible = false;
                    this.grvacc.Columns[9].Visible = false;
                }
                else
                {
                    this.grvacc.Columns[5].Visible = true;
                    this.grvacc.Columns[6].Visible = true;
                    this.grvacc.Columns[7].Visible = true;
                    this.grvacc.Columns[8].Visible = true;
                    this.grvacc.Columns[9].Visible = true;
                }
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

            }
            catch (Exception ex)
            {
            }

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            



        }

        protected void lnkok_Click(object sender, EventArgs e)
        {
            if (this.lnkok.Text == "Ok")
            {
                try
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    Session.Remove("storedata");
                    this.lnkok.Text = "New";
                    this.ddlOthersBook.Enabled = false;
                    this.ddlOthersBookSegment.Enabled = false;
                    string Code = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
                    this.ShowInformation();
                    this.grvacc.EditIndex = -1;
                    this.grvacc_DataBind();

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Information Not Found!');", true);

                }
            }
            else
            {
                this.lnkok.Text = "Ok";
                this.ddlOthersBook.Enabled = true;
                this.ddlOthersBookSegment.Enabled = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
            }
        }
        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlOthersBook.SelectedValue.ToString()).Substring(0, 2);
            string tempddl2 = this.ddlOthersBookSegment.SelectedValue.ToString().Trim();
            string qtype = this.Request.QueryString["Type"].ToString();

            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_DOC", "GET_DOC_GINF_DETAILS", tempddl1,
                            tempddl2, qtype, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                //this.lnknewentry.Visible = true;

            }
            Session["storedata"] = ds1.Tables[0];
        }



    }
}