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
    public partial class HREmpJoblocation : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Job Location Permission";
                this.Getuser();
               
                
              

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
        private void Getuser()
        {
            if (this.lbtnOk.Text == "New")
                return;

            string comcod = this.GetCompCode();
            string mSrchTxt = this.txtUserSearch1.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETUSERNAME", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
            ds1.Dispose();
        }

        protected void GetJoblocation()
        {

            string comcod = this.GetCompCode();       
          
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "GETLOCATION", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;

            this.ddlLocation.DataTextField = "location";
            this.ddlLocation.DataValueField = "loccode";
            this.ddlLocation.DataSource = ds1.Tables[0];
            this.ddlLocation.DataBind();
            ds1.Dispose();



        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.ddlUserList.Enabled = true;
                this.ddlLocation.Enabled = true; 
                this.ddlLocation.Items.Clear();
                this.gvPayrollLinkInfo.DataSource = null;
                this.gvPayrollLinkInfo.DataBind();
                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                return;
            }

            this.ddlUserList.Enabled = false;
            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.ShowEmpJobLocation();

        }
        private void ShowEmpJobLocation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UserCode = this.ddlUserList.SelectedValue.ToString();
            //string type = "94%";
            //if (this.Request.QueryString["Type"].ToString() == "Production")
            //{
            //    type = "49%";
            //}
            //else if (this.Request.QueryString["Type"].ToString() == "Process")
            //{
            //    type = "80%";
            //}
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "GETUSERJOBLOCATION", UserCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblPayPer"] = ds1.Tables[0];

            this.Data_Bind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }


        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["tblPayPer"];
            this.gvPayrollLinkInfo.DataSource = tbl1;
            this.gvPayrollLinkInfo.DataBind();

        }

        private void Session_tbltbPreLink_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tblPayPer"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvPayrollLinkInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvPayrollLinkInfo.Rows[j].FindControl("txtgvRemarks")).Text.Trim();

                TblRowIndex2 = (this.gvPayrollLinkInfo.PageIndex) * this.gvPayrollLinkInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["remarks"] = dgvRemarks;
            }
            ViewState["tblPayPer"] = tbl1;
        }

        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tblPayPer"];
            string loccode = this.ddlLocation.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("loccode = '" + loccode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["userid"] = this.ddlUserList.SelectedValue.ToString();
                dr1["loccode"] = this.ddlLocation.SelectedValue.ToString();
                dr1["location"] = this.ddlLocation.SelectedItem.Text.Trim();
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }
            ViewState["tblPayPer"] = tbl1;
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
            DataTable tbl1 = (DataTable)ViewState["tblPayPer"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string userid = tbl1.Rows[i]["userid"].ToString();
                string loccode = tbl1.Rows[i]["loccode"].ToString();
                string mRMRKS = tbl1.Rows[i]["remarks"].ToString();

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "INSERTUPJOBLOCATION", userid, loccode, mRMRKS, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {

                    string msg = HRData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ msg + "');", true);

                    return;
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
          

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update User Depertment Permission Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        protected void ImgbtnFindUser1_Click(object sender, EventArgs e)
        {
            this.Getuser();
        }


        protected void ImgbtnFindComp_Click(object sender, EventArgs e)
        {
            this.GetJoblocation();
        }       

        protected void lnkbtnDelLoc_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataTable dt = (DataTable)Session["tblPayPer"];
                string usrid = this.ddlUserList.SelectedValue.ToString();
                int rowIndex = ((GridViewRow)((LinkButton)(sender)).NamingContainer).RowIndex;
                string loccode = ((Label)this.gvPayrollLinkInfo.Rows[rowIndex].FindControl("lblgvLocCode")).Text.Trim();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "DELETEJOBLOCATION", loccode, usrid, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Location Deleted Successfully');", true);
                this.ShowEmpJobLocation();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
    }
}