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
namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class HREmpShiftSetup : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Shift Plan Register";
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                //this.lblmsg.Visible = false;
                this.GenInfo();
                
                this.GetAllOrganogramList();
                this.CommonButton();
                this.GetAllShiftData();

                //Out time grace
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
       
        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lTotal_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkSaveData_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GenInfo()
        {

            string comcod = this.GetCompCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETGENINFO", "", "", "", "", "", "", "", "", "");
            if (ds5 == null)
                return;

            this.ddlOffintime.DataTextField = "offintime";
            this.ddlOffintime.DataValueField = "offinid";
            this.ddlOffintime.DataSource = ds5.Tables[1];
            this.ddlOffintime.DataBind();

            this.ddlOffouttime.DataTextField = "offouttime";
            this.ddlOffouttime.DataValueField = "offoutid";
            this.ddlOffouttime.DataSource = ds5.Tables[2];
            this.ddlOffouttime.DataBind();

            this.ddlLanintime.DataTextField = "lanintime";
            this.ddlLanintime.DataValueField = "laninid";
            this.ddlLanintime.DataSource = ds5.Tables[3];
            this.ddlLanintime.DataBind();

            this.ddlLanouttime.DataTextField = "lanouttime";
            this.ddlLanouttime.DataValueField = "lanoutid";
            this.ddlLanouttime.DataSource = ds5.Tables[4];
            this.ddlLanouttime.DataBind();

            ds5.Dispose();


        }





        private void GetAllShiftData()
        {
            string comcod = this.GetCompCode();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETSHIFTINFOR", "", "", "", "", "", "", "", "", "");
            if(ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }

            this.grvshift.DataSource = ds4.Tables[0];
            this.grvshift.DataBind();
            ViewState["AllShiftData"] = ds4.Tables[0];

        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {

            try
            {
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                string comcod = this.GetCompCode();
                int index = row.RowIndex;
                string shiftid = ((Label)this.grvshift.Rows[index].FindControl("lblshiftid")).Text.ToString();
                DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETINDIVISUALSHIFT", shiftid, "", "", "", "", "", "", "", "");
                if (ds4 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    return;
                }

                this.txtShiftName.Text = ds4.Tables[0].Rows[0]["shifname"].ToString();
                this.TxtMachineStime.Text = ds4.Tables[0].Rows[0]["macstarttime"].ToString();
                this.txtMacEndTime.Text = ds4.Tables[0].Rows[0]["macendtime"].ToString() == "00:00:00" ? "" : ds4.Tables[0].Rows[0]["macendtime"].ToString();
                this.txtLateMargin.Text = ds4.Tables[0].Rows[0]["latemarg"].ToString();
                this.txtabsTime.Text = ds4.Tables[0].Rows[0]["abstime"].ToString();
                this.ddlOffintime.SelectedValue = ds4.Tables[0].Rows[0]["stdintime"].ToString();
                this.ddlOffouttime.SelectedValue = ds4.Tables[0].Rows[0]["stouttime"].ToString();
                this.ddlLanintime.SelectedValue = ds4.Tables[0].Rows[0]["shiftlintime"].ToString();
                this.ddlLanouttime.SelectedValue = ds4.Tables[0].Rows[0]["shiftlouttime"].ToString();
                this.lblshiftPLNid.Text = ds4.Tables[0].Rows[0]["shiftid"].ToString();
                this.txtouttimegrace.Text = Convert.ToInt32(ds4.Tables[0].Rows[0]["outtgrace"].ToString()).ToString("#,##0;(#,##0);");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }
        }

        protected void Lbtnremov_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["deleteCk"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no permission');", true);

                
                return;
            }
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            string comcod = this.GetCompCode();
            int index = row.RowIndex;
            string shiftid = ((Label)this.grvshift.Rows[index].FindControl("lblshiftid")).Text.ToString();
            DataTable dt = (DataTable)ViewState["AllShiftData"];
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETINDDELSHIFT", shiftid, "", "", "", "", "", "", "", "");
            if (result)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Data Deleted Successfully')", true);
                int ins = this.grvshift.PageSize * this.grvshift.PageIndex + index;
                dt.Rows[ins].Delete();
                ViewState["AllShiftData"] = dt;

            }
            this.GetAllShiftData();
        }

        protected void lnkBtnAdd_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string comcod = hst["comcod"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string emptype = this.ddlWstation.SelectedValue.ToString();

            string ID = this.lblshiftPLNid.Text.Trim();
            string shiftname = this.txtShiftName.Text.Trim();
            string macstarttime = this.TxtMachineStime.Text.Trim();
            string macEndTime = this.txtMacEndTime.Text.Trim();
            string shiftin = this.ddlOffintime.SelectedValue.ToString();
            string shiftout = this.ddlOffouttime.SelectedValue.ToString();

            string latemarg = this.txtLateMargin.Text.Trim();
            string abstime = this.txtabsTime.Text.Trim();
            string lnchintime = this.ddlLanintime.SelectedValue.ToString();
            string lnchouttime = this.ddlLanouttime.SelectedValue.ToString();
            string outtimegrace =Convert.ToInt32("0"+this.txtouttimegrace.Text.Trim()).ToString();


            string offintimepam = ASTUtility.Right(this.ddlOffintime.SelectedItem.Text.Trim(), 2).ToUpper();
            string offouttimepam = ASTUtility.Right(this.ddlOffouttime.SelectedItem.Text.Trim(), 2).ToUpper();           
            string addday = (offintimepam == "PM" && offouttimepam == "AM") ? "1" : "0";         


            if (shiftname == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Missing Shift Name');", true);

                return;
            }

            bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "INSERTSHIFTPLAN", shiftname, macstarttime, shiftin, shiftout, latemarg, abstime, lnchintime, lnchouttime, Posteddat, Terminal, Sessionid, userid,
                    ID, outtimegrace, addday, macEndTime, "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Shift Plan Created Fail Please Try Again!');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Shift plan Created Successfully');", true);
                this.lblshiftPLNid.Text = "0";

                this.GetAllShiftData();
                this.ClearData();
            }

        }


        private void ClearData()
        {
            this.txtShiftName.Text = "";
            this.TxtMachineStime.Text = "";
            this.txtMacEndTime.Text = "";
            this.txtLateMargin.Text = "0";
            this.txtouttimegrace.Text = "0";
            this.txtabsTime.Text = "";
            this.ddlOffintime.SelectedIndex = 0;
            this.ddlOffouttime.SelectedIndex = 0;
            this.ddlLanintime.SelectedIndex = 0;
            this.ddlLanouttime.SelectedIndex = 0;
            this.lblshiftPLNid.Text = "";
        }

        protected void lnkApply_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            string comcod = this.GetCompCode();
            int index = row.RowIndex;
            string shiftid = ((Label)this.grvshift.Rows[index].FindControl("lblshiftid")).Text.ToString();
            string workgrp = ((Label)this.grvshift.Rows[index].FindControl("lblEmpType")).Text.ToString().Substring(0,4);
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "UPDATEEMPOFFICETIME", shiftid, workgrp, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Employee Not Found!');", true);
                return;
            }
            else
            {
                string msg = "Employee found and updated " + ds4.Tables[0].Rows[0]["ttlemp"].ToString();            
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('"+ msg + "');", true);

            }

        }

        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetAllShiftData();
        }
    }
}