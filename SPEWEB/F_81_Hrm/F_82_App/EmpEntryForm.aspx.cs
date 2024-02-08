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

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class EmpEntryForm : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        CommonHelperClass helpercl = new CommonHelperClass();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Entry";
                this.MultiView1.ActiveViewIndex = 0;

                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetCompany();
                this.chkNewEmp.Checked = true;
                this.chkNewEmp_CheckedChanged(null, null);
                // this.getLastEmpid();
                this.helpercl.GetSisterConcernInf();
                this.CommonButton();

            }
        }


        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Personal Entry Form";

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event

            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(LnkPersonal_Information);


            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void LnkPersonal_Information(object sender, EventArgs e)
        {
            string empid = "";
            if (this.chkNewEmp.Checked == true)
            {
                empid = this.hidempid.Text.ToString();
            }
            else
            {
                empid = this.ddlEmpList.SelectedValue.ToString();

            }

            Response.Redirect("~/F_81_Hrm/F_82_App/EmpEntry01?Type=Entry&empid="+ empid);

        }

        public void GetAllOrganogramList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation.SelectedValue = comcod == "5305" ? "940300000000" : "940800000000";


        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }





        private void GetCompany()
        {
            string comcod = this.GetComeCode();
            string txtCompany = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANY", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompName.DataTextField = "sirdesc";
            this.ddlCompName.DataValueField = "sircode";
            this.ddlCompName.DataSource = ds1.Tables[0];
            this.ddlCompName.DataBind();
            ds1.Dispose();
            this.ingbtnLoc_Click(null, null);

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            //ViewState.Remove("tblservices");
            //string comcod = this.GetComeCode();
            //string empid = this.ddlEmpName.SelectedValue.ToString();
            //string Date = this.txtDate.Text.Trim();
            //DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_HR_EMPSTATUS", "RPTEMPSERVICES", empid, Date, "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvempservices.DataSource = null;
            //    this.gvempservices.DataBind();
            //    return;
            //}
            //ViewState["tblservices"]=ds1.Tables[0];
            //    this.Data_Bind();
        }
        // img btn click 
        protected void imgbtnComp_Click(object sender, EventArgs e)
        {
            this.GetCompany();

        }
        protected void ingbtnLoc_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string txtLocSrc = "%%";
            string company = this.ddlCompName.SelectedValue.ToString().Trim().Substring(0, 2);
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETLOCATION", txtLocSrc, company, "", "", "", "", "", "", "");
            this.ddlLocation.DataTextField = "sirdesc";
            this.ddlLocation.DataValueField = "sircode";
            this.ddlLocation.DataSource = ds2.Tables[0];
            this.ddlLocation.DataBind();
            ds2.Dispose();
            this.imgbtnBranch_Click(null, null);
        }
        protected void imgbtnBranch_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string txtBranch = "%%";
            string location = this.ddlLocation.SelectedValue.ToString().Trim().Substring(0, 4);
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETBRANCH", txtBranch, location, "", "", "", "", "", "", "");
            this.ddlBranch.DataTextField = "sirdesc";
            this.ddlBranch.DataValueField = "sircode";
            this.ddlBranch.DataSource = ds3.Tables[0];
            this.ddlBranch.DataBind();
            ds3.Dispose();
            this.imgbtnDept_Click(null, null);
        }
        protected void imgbtnDept_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string txtDept = "%%";
            string Branch = this.ddlBranch.SelectedValue.ToString().Trim().Substring(0, 7);
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPT", txtDept, Branch, "", "", "", "", "", "", "");
            this.ddlDept.DataTextField = "sirdesc";
            this.ddlDept.DataValueField = "sircode";
            this.ddlDept.DataSource = ds4.Tables[0];
            this.ddlDept.DataBind();
            ds4.Dispose();
            this.ddlEmpList.Items.Clear();
            this.ddlEmployee.Items.Clear();
            this.txtEmpName.Text = "";
            this.txtEmpNameB.Text = "";
        }
        protected void imgbtnPreEMP_Click(object sender, EventArgs e)
        {

            if (this.chkNewEmp.Checked)
                return;
            string comcod = this.GetComeCode();
            string txtDept = "%%";

            string empdept = "93"+ this.ddlWstation.SelectedValue.ToString().Substring(2, 2);

            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPLIST", txtDept, empdept + "%", "", "", "", "", "", "", "");
            this.ddlEmpList.DataTextField = "sirdesc";
            this.ddlEmpList.DataValueField = "sircode";
            this.ddlEmpList.DataSource = ds5.Tables[0];
            this.ddlEmpList.DataBind();
            ViewState["empName"] = ds5.Tables[0];
            ds5.Dispose();
            this.ddlEmpList_SelectedIndexChanged(null, null);

        }
        //  selected index change 
        protected void ddlCompName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ingbtnLoc_Click(null, null);
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgbtnBranch_Click(null, null);
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgbtnDept_Click(null, null);
            //this.ddlEmpList.Items.Clear();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlEmpList.Items.Clear();
            this.txtEmpName.Text = "";
            this.txtEmpNameB.Text = "";
        }
        protected void ddlEmpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpList.Items.Count == 0)
                return;

            DataTable dt = (DataTable)ViewState["empName"];
            if (dt.Rows.Count == 0)
                return;

            string empcode = this.ddlEmpList.SelectedValue.ToString();
            DataView dvr = dt.DefaultView;
            dvr.RowFilter = ("sircode=" + empcode);
            dt = dvr.ToTable();

            string empname = dt.Rows[0]["sirdesc"].ToString();
            string[] empArray = empname.Split('-');
            this.txtEmpName.Text = empArray[1];
            this.txtEmpNameB.Text = dt.Rows[0]["sirdescb"].ToString();
            this.lblEmpnameB.Text = dt.Rows[0]["sirdescb"].ToString();
            this.ddlEmployee.Visible = false;
        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string comcod = this.GetComeCode();
            if (this.ddlWstation.SelectedValue.ToString() == "000000000000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Employee Type');", true);
                return;
            }
          
            string empdept = "93" + this.ddlWstation.SelectedValue.ToString().Substring(2, 2);

            string empname = (chkApplist.Checked == true ? ddlEmployee.SelectedItem.Text : this.txtEmpName.Text);//this.txtEmpName.Text;
            string empNameB = (chkApplist.Checked == true ? this.lblEmpnameB.Text : this.txtEmpNameB.Text);
            bool result = true;

            DataSet ds1;

            string advempcode = chkApplist.Checked == true ? ddlEmployee.SelectedValue.ToString() : "";
            //930100101
            if (chkApplist.Checked == false)
            {
                if (this.txtEmpName.Text.Length < 1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Employee name can't be empty!');", true);
                    return;
                }
            }

            if (this.ddlEmpList.Items.Count > 0)
            {
                string empcode = this.ddlEmpList.SelectedValue.ToString();

                if (userrole == "01" || userrole == "97" || userrole == "99")
                {
                    ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "UPDATEEMPNAME", empcode, empname, empNameB);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have no authorize to Update Employee!');", true);
                    return;
                }

            }
            else
            {

                ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTEMPNAME", empdept, empname, "", "", advempcode, empNameB, "", "", "");


            }

            if (ds1 != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Data Updated Successfully');", true);
                this.txtEmpName.Text = "";
                this.txtEmpNameB.Text = "";
                string empid = ds1.Tables[0].Rows[0]["empid"].ToString();
                this.hidempid.Text = empid;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Sorry, Data Updated Fail');", true);
            }

        }

        private void getLastEmpid()
        {
            string comcod = this.GetComeCode();
            string compny = ASTUtility.Left(ddlCompName.SelectedValue, 2);
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "LASTEMPID", compny, "", "", "", "", "", "", "", "");
            this.lblEmplastId.Text = ds1.Tables[0].Rows[0]["lastempid"].ToString();
        }

        protected void chkNewEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkNewEmp.Checked)
            {
                this.ddlEmpList.Items.Clear();
                this.txtEmpName.Text = "";
                this.txtEmpNameB.Text = "";
            }

        }


        protected void lnkNextbtn_Click(object sender, EventArgs e)
        {
            this.getLastEmpid();
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('EmpEntry01.aspx?Type=Entry&empid=" + this.lblEmplastId.Text + "', target='_self');</script>";
        }

        protected void chkApplist_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApplist.Checked == true)
            {
                this.ddlEmployee.Visible = true;
                this.lblempdata.Visible = true;
                this.txtEmpName.Visible = false;
                this.GetSelected();
            }
            else
            {

                this.ddlEmployee.Visible = false;
                this.txtEmpName.Visible = true;
                this.lblempdata.Visible = false;


            }


        }

        private void GetSelected()
        {
            string comcod = this.GetComeCode();


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETJOINEMPLIST", "10002", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlEmployee.Items.Clear();
            this.ddlEmployee.DataTextField = "empname1";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds1.Tables[0];
            this.ddlEmployee.DataBind();
            ViewState["empinfo"] = ds1.Tables[0];

            this.ddlEmployee_SelectedIndexChanged(null, null);
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["empinfo"];
            string empcode = this.ddlEmployee.SelectedValue.ToString();


            DataView dvr = new DataView();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("empid=" + empcode);
            dt = dvr.ToTable();

            this.lblempdata.Text = dt.Rows[0]["empdata"].ToString();

        }

        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            imgbtnPreEMP_Click(null, null);
        }
    }
}