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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_83_Att
{
    public partial class HREmpOffDays : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetJobLocation();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE OFF DAY'S INFORMATION";
                //this.txtDate.Text = Convert.ToDateTime(System.DateTime.Today).ToString("dd-MMM-yyyy");
                
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }       
        public void GetAllOrganogramList()
        {
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        private void GetWorkStation()
        {

            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }
        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
            lst1.Add(all);


            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = lst1;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "000000000000";
            this.ddlDivision_SelectedIndexChanged(null, null);

        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation);
            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
            lst1.Add(all);

            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = lst1;
            this.ddlDept.DataBind();
            this.ddlDept_SelectedIndexChanged(null, null);

        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
        }
        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);


            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();

        }
        private void GetMonth()
        {

            string comcod = this.GetComCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONTHFOROFFDAY", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
              
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }

            this.ddlMonth.DataTextField = "mnam";
            this.ddlMonth.DataValueField = "mno";
            this.ddlMonth.DataSource = ds2.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.Month.ToString().Trim();


        }
        private void GetEmployee()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4))) + "%";
            string division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string Department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string secname = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string joblocation = ((this.ddlJobLocation.SelectedValue.ToString() == "00000") ? "" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETEMPLOYEENAME", Company, division, Department, secname, joblocation, userid, "", "", "");
            if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                return;
            }
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();

        }


        protected void lnkbtnoffShow_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            Session.Remove("tbloffday");
            string comcod = this.GetComCode();
            string employee = (this.ddlEmpName.SelectedValue.ToString() == "000000000000" ? "" : this.ddlEmpName.SelectedValue.ToString()) + "%";
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string div = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptmane = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string Section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string jLocation = ((this.ddlJobLocation.SelectedValue.ToString()=="00000")? "": this.ddlJobLocation.SelectedValue.ToString()) + "%";
            string date = Convert.ToDateTime("01-" + this.ddlMonth.SelectedItem.Text.Trim()).ToString("dd-MMM-yyyy");
            string onDate = this.txtDate.Text;
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "SHOWEMPOFFDAY", Section, date, employee, Company, deptmane, div, jLocation, userid, onDate);
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvoffday.DataSource = null;
                this.gvoffday.DataBind();
                return;
            }

            Session["tbloffday"] = this.HiddenSameData(ds4.Tables[0]);
            Session["tblprevoffday"] = ds4.Tables[1];
            this.LoadGrid();

            //Previous Offdays 
            this.PnloffDays.Visible = true;
            this.GetMonCalender();
            this.SelectedPrevOffdays();
        }

        private void SelectedPrevOffdays()
        {
            DataTable dt = (DataTable)Session["tblprevoffday"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime offday = Convert.ToDateTime(dt.Rows[i]["sdate"]);

                for (int j = 0; j < this.chkDate.Items.Count; j++)
                {              
                    if (Convert.ToDateTime(this.chkDate.Items[j].Value)== offday)
                    {
                        this.chkDate.Items[j].Selected = true;
                        break;
                    }
                }
            }

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string company = dt1.Rows[0]["company"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["secid"].ToString() == secid)
                {

                    dt1.Rows[j]["companyname"] = "";
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["company"].ToString() == company)
                        dt1.Rows[j]["companyname"] = "";

                    if (dt1.Rows[j]["secid"].ToString() == secid)
                        dt1.Rows[j]["secton"] = "";
                }


                company = dt1.Rows[j]["company"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();
            }
            return dt1;

        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void chkoffDays_CheckedChanged(object sender, EventArgs e)
        {
            this.PnloffDays.Visible = this.chkoffDays.Checked;
            if (this.chkoffDays.Checked)
            {
                this.GetMonCalender();
                this.Chkgov.Checked = false;
                this.ChkSPH.Checked = false;
                this.txtReason.Text = "";
            }
        }


        private void GetMonCalender()
        {
            this.chkDate.Items.Clear();
            string comcod = this.GetComCode();
            string Month = this.ddlMonth.SelectedItem.Text.Substring(0, 3);
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string date = "01-" + Month + "-" + year;
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONDATE", date, "", "", "", "", "", "", "", "");

            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {

                return;
            }
            this.chkDate.DataTextField = "sdate1";
            this.chkDate.DataValueField = "sdate";
            this.chkDate.DataSource = ds4.Tables[0];
            this.chkDate.DataBind();
        }
        private void GetJobLocation()
        {
            string comcod = this.GetComCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();

        }
        protected void lnkbtnAllUpdate_Click(object sender, EventArgs e)
        {      
            string comcod = this.GetComCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string div = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%");
            string deptmane = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%");
            string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string employee = (this.ddlEmpName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlEmpName.SelectedValue.ToString());
            string reason = this.txtReason.Text.Trim();
            //<asp:CheckBox ID="ChkWeekend" runat="server" CssClass="checkbox" Text="Weekend" /><br />
            //<asp:CheckBox ID="Chkgov" runat="server" CssClass="checkbox" Text="Govt. Holiday" /><br />
            //<asp:CheckBox ID="ChkSPH" runat="server" CssClass="checkbox" Text="Secial Holiday" /><br />
            //<asp:CheckBox ID="ChkAdjust" runat="server" CssClass="checkbox" Text="Adjust Holiday" />   
            string dStatus =this.ChkWeekend.Checked==true?"1": (this.Chkgov.Checked == true ? "2" : (this.ChkSPH.Checked == true ? "3" : (this.ChkAdjust.Checked==true?"4":"0")));
            //string dStatus = "0";
            //string dStatus = (this.Chkgov.Checked == true) ? "1" : (this.ChkSPH.Checked == true) ? "2" : "0";
            string jLocation = ((this.ddlJobLocation.SelectedValue.ToString() == "00000") ? "" : this.ddlJobLocation.SelectedValue.ToString()) + "%";

            for (int i = 0; i < this.chkDate.Items.Count; i++)
            {
                if (this.chkDate.Items[i].Selected)
                {

                    string offdate = Convert.ToDateTime(this.chkDate.Items[i].Value).ToString("dd-MMM-yyyy");
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "INSERTORUPOFFDAY", Company, Section, employee, offdate, reason, dStatus, div, deptmane, jLocation, "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject.ToString() + "');", true);
                        return;

                    }

                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Offday Setup Successfully');", true);
            this.chkoffDays.Checked = false;
            this.chkoffDays_CheckedChanged(null, null);
            this.gvoffday.DataSource = null;
            this.gvoffday.DataBind();

        }
        private void LoadGrid()
        {
            this.gvoffday.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvoffday.DataSource = (DataTable)Session["tbloffday"];
            this.gvoffday.DataBind();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tbloffday"];
            int rowindex;
            for (int i = 0; i < this.gvoffday.Rows.Count; i++)
            {
                string date = ((TextBox)this.gvoffday.Rows[i].FindControl("txtgvOffdate")).Text;
                string reason = ((TextBox)this.gvoffday.Rows[i].FindControl("txtgvReason")).Text;
                rowindex = (this.gvoffday.PageIndex) * (this.gvoffday.PageSize) + i;
                dt.Rows[rowindex]["wkdate"] = date;
                dt.Rows[rowindex]["reason"] = reason;
            }
            Session["tbloffday"] = dt;

        }
        protected void gvoffday_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvoffday.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }

        protected void lnkbtnFUpOff_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tbloffday"];
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string div = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%");
            string deptmane = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%");          
            string comcod = this.GetComCode();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string jobloca = dt.Rows[i]["jobloc"].ToString();
                string empid = dt.Rows[i]["empid"].ToString() + "%";
                string wkdate = dt.Rows[i]["wkdate"].ToString();
                string reason = dt.Rows[i]["reason"].ToString();
                string dstatus = dt.Rows[i]["dstatus"].ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "INSERTORUPOFFDAY", Company, Section, empid, wkdate, reason, dstatus, div, deptmane, jobloca, "", "", "", "", "", "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Offday Info Updated Successfully');", true);
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        
        protected void lnkBtnDelOffday_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tbloffday"];
                string comcod = this.GetComCode();
                int rowIndex = ((GridViewRow)((LinkButton)(sender)).NamingContainer).RowIndex;
                int index = (this.gvoffday.PageSize) * (this.gvoffday.PageIndex) + rowIndex;
                string empid = dt.Rows[index]["empid"].ToString();
                string date = Convert.ToDateTime(dt.Rows[index]["wkdate"]).ToString("dd-MMMM-yyyy");
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "DELETEEMPOFFDAY", empid, date, "", "", "", "", "", "", "", "", "", "", "", "", "");
                if (result)
                {
                    dt.Rows[index].Delete();
                }

                DataView dv = dt.DefaultView;
                Session["tbloffday"] = dv.ToTable();
                this.LoadGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Offday Deleted Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
        protected void lnkBtnDelAllOffday_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tbloffday"];
                string comcod = this.GetComCode();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string date = Convert.ToDateTime(dt.Rows[i]["wkdate"]).ToString("dd-MMMM-yyyy");
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "DELETEEMPOFFDAY", empid, date, "", "", "", "", "", "", "", "", "", "", "", "", "");
                    if(result)
                    {
                        dt.Rows[i].Delete();
                    }
                    DataView dv = dt.DefaultView;
                    Session["tbloffday"] = dv.ToTable();
                    this.LoadGrid();
                }
                
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Offday Deleted Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.lnkbtnShow.Text = "New";
                this.ddlDept.Enabled = false;
                this.ddlSection.Enabled = false;
                this.ddlWstation.Enabled = false;
                this.ddlDivision.Enabled = false;
                this.PnlEmp.Visible = true;
                this.chkoffDays.Visible = true;
                this.GetMonth();
                this.GetEmployee();
            }
            else
            {
              
                this.lnkbtnShow.Text = "Ok";
                this.chkoffDays.Checked = false;
                this.ddlDept.Enabled = true;
                this.ddlSection.Enabled = true;
                this.ddlWstation.Enabled = true;
                this.ddlDivision.Enabled = true;
                this.PnlEmp.Visible = false;
                this.PnloffDays.Visible = false;
                this.chkoffDays.Visible = false;
                this.lblPage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.Chkgov.Checked = false;
                this.ChkSPH.Checked = false;
                this.gvoffday.DataSource = null;
                this.gvoffday.DataBind();

            }
        }       
       
    }
}