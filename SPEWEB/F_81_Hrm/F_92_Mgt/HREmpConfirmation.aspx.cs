using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WinForms;
using System.Drawing;
using SPELIB;
using SPERDLC;
using SPEENTITY;
using SPEENTITY.C_81_Hrm.C_81_Rec;


namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class HREmpConfirmation : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                
                this.SelectView();
                GetWorkStation();
                GetAllOrganogramList();
                this.GetSignEmp();
                this.GetJobLocation();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Confirmation";
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString();
            string date = "";
            switch (type)
            {
               
                case "Entry":
                     date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = date;
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                case "Rpt":
                    this.divToDate.Visible = true;
                    this.lblfrmdate.Text = "From";
                    date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = Convert.ToDateTime("01-" + date.Substring(3)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
            }          

            
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

            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetComeCode();
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

        private void GetDeptList()
        {
            string wstation = this.ddlDivision.SelectedValue.ToString();//940100000000

            string comcod = GetComeCode();
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
            this.ddlDept.SelectedValue = "000000000000";

            this.ddlDept_SelectedIndexChanged(null, null);

        }

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString();//940100000000
            string comcod = GetComeCode();
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
            this.ddlSection.SelectedValue = "000000000000";
        }


        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string footer = ASTUtility.Concat(compname, username, printdate).ToString();
            DataTable dt = (DataTable)Session["tblMrr"];
            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.BO_EmpConfirm>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptEmpConfirm", lst, null, null);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Employee Confirm Information"));
            rpt1.SetParameters(new ReportParameter("footer", footer));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            try
            {
                Session.Remove("tblMrr");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string company = (this.ddlWstation.SelectedValue.Substring(0, 4).ToString() == "0000") ? "%" : this.ddlWstation.SelectedValue.Substring(0, 4).ToString() + "%";
                string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
                string DeptCode = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string qtype = this.Request.QueryString["Type"].ToString();
                string jobLocation = this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "%" : this.ddlJobLocation.SelectedValue.ToString() + "%";
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPCONFIRM", company, DeptCode, frmdate, division, section, todate, qtype, jobLocation, "");
                if (ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                    this.dgvEmpCon.DataSource = null;
                    this.dgvEmpCon.DataBind();
                    return;
                }
                Session["tblMrr"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }

        }
        private void Data_Bind()
        {
            this.dgvEmpCon.DataSource = (DataTable)Session["tblMrr"];
            this.dgvEmpCon.DataBind();
        }
        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.dgvEmpCon.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.dgvEmpCon.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";
                string remarks = ((TextBox)this.dgvEmpCon.Rows[i].FindControl("txtRem")).Text.ToString();
                string signatory = ((DropDownList)this.dgvEmpCon.Rows[i].FindControl("DdlSignatory")).SelectedValue.ToString();
                string confirmdat = ((TextBox)this.dgvEmpCon.Rows[i].FindControl("lgvCondat")).Text.Trim();

                dt.Rows[i]["chkmv"] = chkmr;
                dt.Rows[i]["remarks"] = remarks;
                dt.Rows[i]["signatory"] = signatory;
                dt.Rows[i]["condat"] = confirmdat;
                
                ((CheckBox)this.dgvEmpCon.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.dgvEmpCon.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.dgvEmpCon.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.dgvEmpCon.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblMrr"] = dt;

        }



        protected void lbok_Click(object sender, EventArgs e)
        {
            this.CheckValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblMrr"];           
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");          
           
            try
            {
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string Remarks = dt.Rows[i]["remarks"].ToString();
                    string Chk = dt.Rows[i]["chkmv"].ToString();
                    string confirmdat = dt.Rows[i]["condat"].ToString();
                    string signatory = dt.Rows[i]["signatory"].ToString();
                    if (Chk == "True")
                    {
                        bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTEMPCONFIRM", empid, Remarks, userid, posteddat, Terminal, Sessionid,
                              confirmdat, signatory, "", "", "", "", "", "", "");
                        if (!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                            return;
                        }
                    }     
                  
                }               

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Confirmation Updated Successfully');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }

        protected void lnkPrintInd_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string footer = ASTUtility.Concat(compname, username, printdate).ToString();
            DataTable dt = (DataTable)Session["tblMrr"];     
            DataView dv2 = new DataView(dt);
            dv2.RowFilter = "chkmv <> 'False'";
            dt = dv2.ToTable();
            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.BO_EmpConfirm>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptConfirmletter", lst, null, null);
            rpt1.EnableExternalImages = true;
            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("rpttitle", "Employee Confirm Information"));
            //rpt1.SetParameters(new ReportParameter("footer", footer));
            Session["Report1"] = rpt1;
            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }
        private void GetSignEmp()
        {            
            string comcod = this.GetComeCode();
            DataSet tblSignatory = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPSIGNAME", "80", "%", "%", "%", "", "", "", "", "");
            ViewState["tblSignatory"] = tblSignatory.Tables[0];
        }
        private void GetJobLocation()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string jobLocCode = "87";
            var lst = getlist.GetCommonHRgcod(comcod, jobLocCode);

            this.ddlJobLocation.DataTextField = "hrgdesc";
            this.ddlJobLocation.DataValueField = "hrgcod";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }
        protected void dgvEmpCon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable tblSignatory = ((DataTable)ViewState["tblSignatory"]).Copy();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string signatory = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "signatory"));

                DropDownList DdlSignatory = (DropDownList)e.Row.FindControl("DdlSignatory");
                DdlSignatory.DataTextField = "signame";
                DdlSignatory.DataValueField = "idcard";
                DdlSignatory.DataSource = tblSignatory;
                DdlSignatory.DataBind();
                DdlSignatory.SelectedValue = signatory;
            }
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblMrr"];

            int i;
            int row;
            if (((CheckBox)this.dgvEmpCon.HeaderRow.FindControl("chkAll")).Checked)
            {

                for (i = 0; i < this.dgvEmpCon.Rows.Count; i++)
                {
                    ((CheckBox)this.dgvEmpCon.Rows[i].FindControl("chkvmrno")).Checked = true;
                    row = (this.dgvEmpCon.PageSize * this.dgvEmpCon.PageIndex) + i;
                    dt.Rows[row]["chkmv"] = "True";
                }

            }

            else
            {
                for (i = 0; i < this.dgvEmpCon.Rows.Count; i++)
                {
                    ((CheckBox)this.dgvEmpCon.Rows[i].FindControl("chkvmrno")).Checked = false;
                    row = (this.dgvEmpCon.PageSize * this.dgvEmpCon.PageIndex) + i;
                    dt.Rows[row]["chkmv"] = "False";

                }

            }


            Session["tblMrr"] = dt;
        }
    }
}