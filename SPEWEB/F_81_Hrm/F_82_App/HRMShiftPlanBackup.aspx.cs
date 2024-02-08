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
    public partial class HRMShiftPlanBackup : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Shift Plan Backup";
                this.txtfromdate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
               // this.GenInfo();
                ViewState["shiftid"] = "";
                ViewState["locationid"] = "";
                this.GetJobLocation();
               // GetAllShiftData();
            }
        }

        protected void lnkBtnAdd_Click(object sender, EventArgs e)
        {
        }
        private void ClearData()
        {
            this.txtShiftName.Text = "";
            this.TxtMachineStime.Text = "";
            this.txtLateMargin.Text = "0";
            this.txtabsTime.Text = "";
            this.txtOutGraceTime.Text = "0";
            //this.ddlLanintime.SelectedIndex = 0;
            //this.ddlLanouttime.SelectedIndex = 0;
            this.lblshiftPLNid.Text = "";
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GenInfo()
        {

            //string comcod = this.GetCompCode();
            ////string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
            //DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETGENINFO", "", "", "", "", "", "", "", "", "");
            ////Session["UserLog"] = ds5.Tables[7];

            //if (ds5 == null)
            //    return;


            ////this.ddlOffintime.DataTextField = "offintime";
            ////this.ddlOffintime.DataValueField = "offinid";
            ////this.ddlOffintime.DataSource = ds5.Tables[1];
            ////this.ddlOffintime.DataBind();

            ////this.ddlOffouttime.DataTextField = "offouttime";
            ////this.ddlOffouttime.DataValueField = "offoutid";
            ////this.ddlOffouttime.DataSource = ds5.Tables[2];
            ////this.ddlOffouttime.DataBind();

            //this.ddlLanintime.DataTextField = "lanintime";
            //this.ddlLanintime.DataValueField = "laninid";
            //this.ddlLanintime.DataSource = ds5.Tables[3];
            //this.ddlLanintime.DataBind();

            ////this.ddlLanintime.SelectedItem.Text = "None";


            //this.ddlLanouttime.DataTextField = "lanouttime";
            //this.ddlLanouttime.DataValueField = "lanoutid";
            //this.ddlLanouttime.DataSource = ds5.Tables[4];
            //this.ddlLanouttime.DataBind();
            ////this.ddlLanouttime.SelectedValue = "";
            ////this.ddlLanouttime.SelectedItem.Text = "None";




            //ds5.Dispose();


        }
        private void GetJobLocation()
        {
            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }
        protected void lnkbtnShowData_Click(object sender, EventArgs e)
        {
            this.GetAllShiftData();
        }
        private void GetAllShiftData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string date = this.txtfromdate.Text.Trim(); 
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETSHIFTBACKUP", date, joblocation, userid, "", "", "", "", "", "");            
            ViewState["AllShiftData"] = ds4.Tables[0];
            this.Data_Bind();


        }

        private void Data_Bind()
        {
            this.grvshift.DataSource =(DataTable) ViewState["AllShiftData"];
            this.grvshift.DataBind();


        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            string comcod = this.GetCompCode();
            int index = row.RowIndex;           
            
            string shiftid = ((Label)this.grvshift.Rows[index].FindControl("lblshiftid")).Text.ToString();
            string locationid = ((Label)this.grvshift.Rows[index].FindControl("lbllocationid")).Text.ToString();
            ViewState["shiftid"] = shiftid;
            ViewState["locationid"] = locationid;
            string date = this.txtfromdate.Text.Trim();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETSHIFTINFO", shiftid, date, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                return;
            }

            this.txtShiftName.Text = ds4.Tables[0].Rows[0]["shiftname"].ToString();
            this.TxtMachineStime.Text = ds4.Tables[0].Rows[0]["macstarttime"].ToString();
            this.txtLateMargin.Text = ds4.Tables[0].Rows[0]["latemarg"].ToString();
            this.txtabsTime.Text = ds4.Tables[0].Rows[0]["abstime"].ToString();
            this.txtoffintime.Text = Convert.ToDateTime(ds4.Tables[0].Rows[0]["offintime"]).ToString("HH:mm");
            this.txtOffouttime.Text = Convert.ToDateTime(ds4.Tables[0].Rows[0]["offouttime"]).ToString("HH:mm");
            this.txtLanintime.Text = Convert.ToDateTime(ds4.Tables[0].Rows[0]["lnchintime"]).ToString("HH:mm");
            this.txtLanouttime.Text = Convert.ToDateTime(ds4.Tables[0].Rows[0]["lnchouttime"]).ToString("HH:mm");
            this.lblshiftPLNid.Text = ds4.Tables[0].Rows[0]["shiftid"].ToString();
            this.ddloffdaytype.SelectedValue= ds4.Tables[0].Rows[0]["happlicable"].ToString();
            this.ddlJobLocation.SelectedValue = locationid;
            this.txtOutGraceTime.Text = ds4.Tables[0].Rows[0]["outtgrace"].ToString();

            this.Data_Bind();
        }

        protected void grvshift_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            string sshiftid = ViewState["shiftid"].ToString();
            string slocationid = ViewState["locationid"].ToString();
            string rshiftid = ((Label)e.Row.FindControl("lblshiftid")).Text.Trim();              
            string rlocationid = ((Label)e.Row.FindControl("lbllocationid")).Text.Trim();
            if (sshiftid == rshiftid && slocationid == rlocationid)
               e.Row.Attributes["style"]= "background:blue; color:white;";
           

        }

        protected void lbtnUpdateshiftback_Click(object sender, EventArgs e)
        {

            DateTime frmdate, todate, offintime, today, addday, offouttime, lanintime, lanouttime, systime;
            string dayid;
            frmdate = Convert.ToDateTime(this.txtfromdate.Text);
            dayid = frmdate.ToString("yyyyMMdd");
            string comcod = this.GetCompCode();

            string shiftid = this.lblshiftPLNid.Text;
            string macstart = this.TxtMachineStime.Text.Trim();
            string latemarg = this.txtLateMargin.Text.Trim();
            string abstime = this.txtabsTime.Text.Trim();
            offintime = Convert.ToDateTime(frmdate.ToString("dd-MMM-yyyy") + " " + this.txtoffintime.Text.Trim());
            systime = Convert.ToDateTime(frmdate.ToString("dd-MMM-yyyy") + " " + "08:00 PM");
            today = frmdate;
            addday = today.AddDays(1);
            offouttime = (systime <= offintime) ? Convert.ToDateTime(addday.ToString("dd-MMM-yyyy") + " " + this.txtOffouttime.Text.Trim()) : Convert.ToDateTime(today.ToString("dd-MMM-yyyy") + " " + this.txtOffouttime.Text.Trim());
            lanintime = (systime <= offintime) ? Convert.ToDateTime(addday.ToString("dd-MMM-yyyy") + " " + this.txtLanintime.Text.Trim()) : Convert.ToDateTime(today.ToString("dd-MMM-yyyy") + " " + this.txtLanintime.Text.Trim());
            lanouttime = (systime <= offintime) ? Convert.ToDateTime(addday.ToString("dd-MMM-yyyy") + " " + this.txtLanouttime.Text.Trim()) : Convert.ToDateTime(today.ToString("dd-MMM-yyyy") + " " + this.txtLanouttime.Text.Trim());
            frmdate = frmdate.AddDays(1);

            string happlicable = this.ddloffdaytype.SelectedValue.ToString();
            string jobLocation = this.ddlJobLocation.SelectedValue.ToString();
            string outgracetime = this.txtOutGraceTime.Text.Trim();

            DataSet ds4 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "UPDATESHIPTPLANBACKUP", null,null,null, dayid, shiftid, offintime.ToString(), offouttime.ToString(),
                lanintime.ToString(), lanouttime.ToString(), macstart, latemarg, abstime, happlicable, jobLocation, outgracetime);
            if (ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Shift Backup Plan Change Failed!');", true);
                return;
            }
            else
            {
                string msg = "Employee found and updated " + ds4.Tables[0].Rows[0]["ttlemp"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                this.GetAllShiftData();
                this.ClearData();
            }


        }
    }
}