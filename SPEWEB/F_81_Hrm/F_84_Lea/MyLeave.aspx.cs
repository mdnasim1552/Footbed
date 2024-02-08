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
using System.Drawing;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_84_Lea
{
    public partial class MyLeave : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.GetEmpid();

               
                ((Label)this.Master.FindControl("lblTitle")).Text = "ONLINE LEAVE APPLICATION";
               
                //this.GetEmpid();
                this.lbtnOk_Click(null, null);
                ///
                /// 
                this.rblstapptype.SelectedIndex = 0;
                this.rblstapptype.SelectedIndex = 0;

                this.ShowLeaveApp();
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
       
     
        

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            
        }



        private void ShowLeaveApp()
        {
           
                this.lblleaveApp.Visible = true;
                this.lblleaveStatus.Visible = true;
                this.lblleaveInformation.Visible = true;
                //this.PnlEmp.Visible = true;
                //this.Pnlapply.Visible = true;
                this.PnlRmrks.Visible = true;
                this.txtaplydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.txtApprdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetLeaveid();
                this.imgbtnlAppEmpSeaarch_Click(null, null);
              
           
        }

        



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["secid"] = "";
                    dt1.Rows[j]["secname"] = "";
                }
                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }
            return dt1;
        }






        private void GetEmployeeName()
        {
            ViewState.Remove("tblEmpDesc");
            ViewState.Remove("tblleave");
            string comcod = this.GetComeCode();
            //string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            //string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            //string IdCardNo = "%" + this.txtlAppEmpSearch.Text.Trim() + "%";
            string empid = this.Request.QueryString["Type"].ToString() == "User" ? this.lblempid.Text : "%%";
            //string deptcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : ASTUtility.Left(this.ddlProjectName.SelectedValue.ToString(), 7) + "00000%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", "%", "94%", empid, "%", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();

            this.ddlEmpName.Enabled = false;
            ViewState["tblEmpDesc"] = ds1.Tables[0];

            this.gvLeaveApp.DataSource = ds1.Tables[1];
            this.gvLeaveApp.DataBind();
            ViewState["tblleave"] = ds1.Tables[1];
            this.ddlEmpName_SelectedIndexChanged(null, null);
        }

        private void GetLeaveid()
        {

            string comcod = this.GetComeCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLEAVEID", "", "", "", "", "", "", "", "", "");
            this.lbltrnleaveid.Text = ds5.Tables[0].Rows[0]["ltrnid"].ToString().Trim();
        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowEmppLeave();
            this.EmpLeaveInfo();

        }

        private void ShowEmppLeave()
        {

            //this.txtLeavLreasons.Text = "";
            //this.txtLeavRemarks.Text = "";
            ViewState.Remove("tblleavest");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string aplydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)ViewState["tblEmpDesc"];
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");

            if (dr1.Length > 0)
            {
                this.lblComPany.Text = dr1[0]["companyname"].ToString();
                this.lblSection.Text = dr1[0]["section"].ToString();
                this.lblDesignation.Text = dr1[0]["desig"].ToString();
                this.lblJoiningDate.Text = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");


            }

            //string calltype = ((this.rblstapptype.SelectedIndex == 0) ? "LEAVESTATUS" : (this.rblstapptype.SelectedIndex == 1) ? "LEAVESTATUS01" : "LEAVESTATUS02");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, aplydat, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return;
            }

            ViewState["tblleavest"] = ds1.Tables[0];
            this.Data_Bind();

        }


        private void EmpLeaveInfo()
        {
            ViewState.Remove("tblempleaveinfo");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string aplydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVEINFORMATION", empid, aplydat, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvleaveInfo.DataSource = null;
                this.gvleaveInfo.DataBind();
                return;
            }
            DataTable dt1 = ds1.Tables[0];
            if (dt1.Rows.Count == 0)
            {
                this.gvleaveInfo.DataSource = null;
                this.gvleaveInfo.DataBind();
                return;
            }
            string gcod = dt1.Rows[0]["gcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                    dt1.Rows[j]["gdesc"] = "";
                gcod = dt1.Rows[j]["gcod"].ToString();
            }
            ViewState["tblempleaveinfo"] = dt1;
            this.gvleaveInfo.DataSource = dt1;
            this.gvleaveInfo.DataBind();
            ds1.Dispose();
        }

        protected void lnkbtnRef_Click(object sender, EventArgs e)
        {

            //this.PnlEmp.Visible = false;
            //this.Pnlapply.Visible = false;
            this.PnlRmrks.Visible = false;
            this.lblleaveApp.Visible = false;
            this.lblleaveStatus.Visible = false;
            this.lblleaveInformation.Visible = false;
            this.ddlEmpName.Items.Clear();

            this.lblComPany.Text = "";
            this.lblSection.Text = "";
            this.lblDesignation.Text = "";
            this.lblJoiningDate.Text = "";
            this.txtaplydate.Text = "";
            this.txtLeavLreasons.Text = "";
            this.txtaddofenjoytime.Text = "";
            this.txtLeavRemarks.Text = "";
            //this.lmsg11.Text = "";
            this.gvLeaveApp.DataSource = null;
            this.gvLeaveApp.DataBind();
            this.gvLeaveStatus.DataSource = null;
            this.gvLeaveStatus.DataBind();
            this.gvleaveInfo.DataSource = null;
            this.gvleaveInfo.DataBind();


            this.ShowEmppLeave();
        }
        private void Data_Bind()
        {
            this.gvLeaveStatus.DataSource = (DataTable)ViewState["tblleavest"];
            this.gvLeaveStatus.DataBind();
        }


        private void SaveLeave()
        {
            this.lblleaveApp.Visible = true;
            DataTable dt = (DataTable)ViewState["tblleave"];
            DataTable dt1 = (DataTable)ViewState["tblleavest"];
            for (int i = 0; i < this.gvLeaveApp.Rows.Count; i++)
            {
                //TimeSpan ts = (this.CalExt3.SelectedDate.Value - this.CalExt2.SelectedDate.Value);
                int leaveday = Convert.ToInt32("0" + ((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvlapplied")).Text.Trim());

                if (leaveday > 0)
                {
                    string stdat = Convert.ToDateTime(((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvenjoydt1")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string endat = Convert.ToDateTime(stdat).AddDays(leaveday - 1).ToString("dd-MMM-yyyy");
                    dt.Rows[i]["lapplied"] = leaveday;
                    dt.Rows[i]["lenjoydt1"] = stdat;
                    dt.Rows[i]["lenjoydt2"] = endat;
                    double enjleave = Convert.ToDouble(dt1.Rows[i]["ltaken"]);
                    double Clsleave = Convert.ToDouble(dt1.Rows[i]["pbal"]);
                    dt1.Rows[i]["applyday"] = leaveday;
                    dt1.Rows[i]["appday"] = leaveday;
                    dt1.Rows[i]["applydate"] = stdat;
                    dt1.Rows[i]["appdate"] = stdat;
                    //dt1.Rows[i]["todate"] = endat;
                    //dt1.Rows[i]["pbal"] = Convert.ToInt32(dt1.Rows[i]["pbal"]) - leaveday;
                    //dt1.Rows[i]["ltaken"] = Convert.ToInt32(dt1.Rows[i]["ltaken"]) + leaveday;
                    dt1.Rows[i]["balleave"] = Clsleave - leaveday;
                    dt1.Rows[i]["tltakreq"] = leaveday;


                }
            }

            this.gvLeaveApp.DataSource = dt;
            this.gvLeaveApp.DataBind();
            ViewState["tblleave"] = dt;


            //---------For Status table --------------------------------------

            ViewState["tblleavest"] = dt1;
            this.Data_Bind();
        }



        protected void lnkbtnPreLeave_Click(object sender, EventArgs e)
        {
            this.PnlPreLeave.Visible = false;
            this.chkPreLeave.Checked = false;
            this.PreLeaveInfo();
            this.chkPreLeave_CheckedChanged(null, null);
        }
        private void PreLeaveInfo()
        {
            ViewState.Remove("tblleavest");
            DataTable dt = (DataTable)ViewState["tblprelinf"];
            string ltrnid = this.ddlPreLeave.SelectedValue.ToString();
            DataRow[] drp = dt.Select("ltrnid='" + ltrnid + "'");
            if (dt.Rows.Count == 0)
                return;

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string date = Convert.ToDateTime(drp[0]["strtdat"]).ToString("dd-MMM-yyyy");
            //string calltype = ((this.rblstapptype.SelectedIndex == 0) ? "LEAVESTATUS" : (this.rblstapptype.SelectedIndex == 1) ? "LEAVESTATUS01" : "LEAVESTATUS02");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return;
            }
            // Session["tblleavest"] = ds1.Tables[0];
            DataTable dt1 = (DataTable)ViewState["tblleave"];
            DataTable dt2 = ds1.Tables[0];


            string gcod = drp[0]["gcod"].ToString();
            DataRow[] drl = dt1.Select("gcod='" + gcod + "'");
            DataRow[] drls = dt2.Select("gcod='" + gcod + "'");

            //leave-------
            drl[0]["lapplied"] = drp[0]["lapplied"];
            drl[0]["lenjoydt1"] = drp[0]["strtdat"];
            drl[0]["lenjoydt2"] = drp[0]["enddat"];
            //leave status-------
            double leaveday = Convert.ToDouble(drp[0]["lapplied"].ToString());
            double enjleave = Convert.ToDouble(drls[0]["ltaken"]);
            double Clsleave = Convert.ToDouble(drls[0]["pbal"]);
            drls[0]["applyday"] = drp[0]["lapplied"];
            drls[0]["appday"] = drp[0]["lapplied"];
            drls[0]["applydate"] = drp[0]["strtdat"];
            drls[0]["appdate"] = drp[0]["strtdat"];
            // drls[0]["todate"] = drp[0]["strtdat"];

            drls[0]["balleave"] = Clsleave - leaveday;
            drls[0]["tltakreq"] = leaveday;
            //drls[0]["balleave"] = Clsleave - (leaveday + enjleave);
            //drls[0]["tltakreq"] = (leaveday + enjleave);

            ViewState["tblleave"] = dt1;
            ViewState["tblleavest"] = dt2;
            //Genral info
            this.lbltrnleaveid.Text = this.ddlPreLeave.SelectedValue.ToString();
            this.txtaplydate.Text = Convert.ToDateTime(drp[0]["aplydat"]).ToString("dd-MMM-yyyy");
            // this.txtApprdate.Text = Convert.ToDateTime(drp[0]["aprdat"]).ToString("dd-MMM-yyyy");
            this.txtLeavLreasons.Text = drp[0]["lreason"].ToString();
            this.txtaddofenjoytime.Text = drp[0]["addlentime"].ToString();
            this.txtLeavRemarks.Text = drp[0]["lrmarks"].ToString();
            this.txtdutiesnameandDesig.Text = drp[0]["denameadesig"].ToString();

            //gvbind
            this.gvLeaveApp.DataSource = dt1;
            this.gvLeaveApp.DataBind();
            this.Data_Bind();
        }


        protected void chkPreLeave_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPreLeave.Checked)
            {
                this.PnlPreLeave.Visible = true;
                this.PreLeaveno();
            }
            else
            {
                this.PnlPreLeave.Visible = false;
            }

        }
        private void PreLeaveno()
        {

            ViewState.Remove("tblprelinf");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "PREVIOUSLEAVENO", empid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlPreLeave.Items.Clear();
                return;
            }
            ViewState["tblprelinf"] = ds1.Tables[0];
            this.ddlPreLeave.DataTextField = "ltrndesc";
            this.ddlPreLeave.DataValueField = "ltrnid";
            this.ddlPreLeave.DataSource = ds1.Tables[0];
            this.ddlPreLeave.DataBind();

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "User":
                    this.PrintLeaveApprove();
                    break;



            }


        }
        private void PrintLeaveApprove()
        {


            string comcod = this.GetComeCode();

            switch (comcod)
            {
                
                default:

                    this.PrintExecutiveLeavForm();
                    break;




            }



        }

        private void PrintExecutiveLeavForm()
        {
            string empname = "";
            string cardno = "";
            string department = "";
            string desg = "";
            string joindate = "";
            string section = "";
            string line = "";


            string frmdate = "";
            string todate = "";
            string joinafter = "";
            string totaldays = "";
            string leavetype = "";

            string applydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            string reason = this.txtLeavLreasons.Text.Trim(); ;
            string addentime = this.txtaddofenjoytime.Text.Trim();
            string remarks = this.txtLeavRemarks.Text.Trim();
            string dnameadesig = this.txtdutiesnameandDesig.Text.Trim();
            if (dnameadesig.Length == 0)
            {
                dnameadesig = "";//this.ddlReplaceremp.SelectedItem.ToString();

            }
            


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            string empid = this.ddlEmpName.SelectedValue.ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dtemp = (DataTable)ViewState["tblEmpDesc"];
            DataRow[] dr1 = dtemp.Select("empid='" + empid + "'");

            string emptype = dr1[0]["emptype"].ToString();

            string totalleav = "Total Days" + "2";
            if (dr1.Length > 0)
            {
                cardno = dr1[0]["idcardno"].ToString();
                empname = dr1[0]["empname1"].ToString();
                department = dr1[0]["dept"].ToString();
                desg = dr1[0]["desig"].ToString();
                joindate = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");
                section = dr1[0]["section"].ToString();
                line = dr1[0]["line"].ToString();
            }
            DataTable dt = (DataTable)ViewState["tblleavest"];
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpLeavStatusInfoEng>();
            DataTable dt2 = (DataTable)ViewState["tblprelinf"];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                DataView dv = dt2.DefaultView;
                dv.RowFilter = "ltrnid='" + this.ddlPreLeave.SelectedValue.ToString() + "'";

                frmdate = Convert.ToDateTime(dv.ToTable().Rows[0]["strtdat"]).ToString("dd/MM/yyyy");
                todate = Convert.ToDateTime(dv.ToTable().Rows[0]["enddat"]).ToString("dd/MM/yyyy");
                joinafter = Convert.ToDateTime(dv.ToTable().Rows[0]["afterjoin"]).ToString("dd/MM/yyyy");
                double total = (Convert.ToDateTime(dv.ToTable().Rows[0]["enddat"]) - Convert.ToDateTime(dv.ToTable().Rows[0]["strtdat"])).TotalDays + 1;
                totaldays = total.ToString();
                leavetype = dv.ToTable().Rows[0]["gcod"].ToString();
                if (reason.Length == 0)
                {
                    reason = dv.ToTable().Rows[0]["lreason"].ToString();
                }
            }
            else if (this.lbltrnleaveid.Text.ToString().Length > 0)
            {
                PreLeaveno();
                DataTable dt3 = (DataTable)ViewState["tblprelinf"];
                DataView dv = dt3.DefaultView;
                dv.RowFilter = "ltrnid='" + this.lbltrnleaveid.Text.ToString() + "'";

                frmdate = Convert.ToDateTime(dv.ToTable().Rows[0]["strtdat"]).ToString("dd/MM/yyyy");
                todate = Convert.ToDateTime(dv.ToTable().Rows[0]["enddat"]).ToString("dd/MM/yyyy");
                joinafter = Convert.ToDateTime(dv.ToTable().Rows[0]["afterjoin"]).ToString("dd/MM/yyyy");
                double total = (Convert.ToDateTime(dv.ToTable().Rows[0]["enddat"]) - Convert.ToDateTime(dv.ToTable().Rows[0]["strtdat"])).TotalDays + 1;
                totaldays = total.ToString();
                leavetype = dt3.Rows[0]["gcod"].ToString();
                if (reason.Length == 0)
                {
                    reason = dv.ToTable().Rows[0]["lreason"].ToString();
                }
            }
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptLeavAppFormEng", list, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("line", line));
            Rpt1.SetParameters(new ReportParameter("companyname", comname));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " LEAVE APPLICATION FORM"));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("empname", empname));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));

            Rpt1.SetParameters(new ReportParameter("idcard", cardno));
            Rpt1.SetParameters(new ReportParameter("datofjoin", joindate));
            Rpt1.SetParameters(new ReportParameter("desig", desg));
            Rpt1.SetParameters(new ReportParameter("department", department));
            Rpt1.SetParameters(new ReportParameter("sect", section));
            Rpt1.SetParameters(new ReportParameter("totaldays", totalleav));

            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("todate", todate));
            Rpt1.SetParameters(new ReportParameter("joinafter", joinafter));

            Rpt1.SetParameters(new ReportParameter("applydat", applydat));
            Rpt1.SetParameters(new ReportParameter("reason", reason));
            Rpt1.SetParameters(new ReportParameter("addentime", addentime));
            Rpt1.SetParameters(new ReportParameter("remarks", remarks));
            Rpt1.SetParameters(new ReportParameter("emptype", emptype));
            Rpt1.SetParameters(new ReportParameter("dnameadesig", dnameadesig));
            Rpt1.SetParameters(new ReportParameter("totaldays", totaldays));
            Rpt1.SetParameters(new ReportParameter("leavetype", leavetype));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void lnkbtnUpdateLeave_Click(object sender, EventArgs e)
        {
            this.SaveLeave();
            DataTable dt = (DataTable)ViewState["tblleave"];
            string comcod = this.GetComeCode();
            string trnid = this.lbltrnleaveid.Text;
            string empid = this.ddlEmpName.SelectedValue.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double lapplied = Convert.ToDouble(dt.Rows[i]["lapplied"]);
                if (lapplied > 0)
                {

                    string gcod = dt.Rows[i]["gcod"].ToString();
                    string frmdate = Convert.ToDateTime(dt.Rows[i]["lenjoydt1"]).ToString("dd-MMM-yyyy");
                    string todate = Convert.ToDateTime(dt.Rows[i]["lenjoydt2"]).ToString("dd-MMM-yyyy");
                    string applydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
                    string reason = this.txtLeavLreasons.Text.Trim();

                    if (reason.Length > 0)
                    {
                        //
                    }

                    string addentime = this.txtaddofenjoytime.Text.Trim();
                    string remarks = this.txtLeavRemarks.Text.Trim();
                    string dnameadesig = this.txtdutiesnameandDesig.Text.Trim();

                    string APRdate = "";
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAVAPP", trnid, empid, gcod, frmdate, todate, applydat, reason, remarks, APRdate, addentime, dnameadesig, "", "", "", "");


                    if (result == true && chkPreLeave.Checked == false)
                    {
                        this.SendSms(frmdate, todate);
                    }
                }

            }
            this.EmpLeaveInfo();
            this.ShowEmppLeave();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


        }

        protected void SendSms(string frmdate, string todate)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSUPERVISER", empid, "", "", "", "", "", "", "", "");

            if (ds == null)
                return;
            DataTable dt = (DataTable)ViewState["tblleave"];
            //DataRow[] dr = dt.Select("lapplied>0"); 
            double lapplied = Convert.ToDouble(dt.Select("lapplied>0")[0]["lapplied"]);
            string leavedesc = dt.Select("lapplied>0")[0]["gdesc"].ToString();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string phone = (string)ds.Tables[0].Rows[i]["phone"];
                string empname = (string)ds.Tables[1].Rows[0]["name"];
                string empdesig = (string)ds.Tables[1].Rows[0]["desig"];
                string appdate = "";
                if (hst["compsms"].ToString() == "True")
                {
                    SendSmsProcess sms = new SendSmsProcess();
                    string comnam = hst["comnam"].ToString();
                    string compname = hst["compname"].ToString();
                    // string frmname = "PurReqApproval.aspx?Type=RateInput";
                    // string SMSHead = "Leave Applied From : ";
                    string SMSText = leavedesc + " applied from : " + frmdate + " To " + todate + "\n" + "Name: " + empname + " Designation : " + empdesig;
                    bool resultsms = sms.SendSmmsPwd(SMSText, SMSText, phone);
                }
            }
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {


            string comcod = this.GetComeCode();
            string trnid = this.lbltrnleaveid.Text;
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted failed');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Sucessfully Deleted');", true);



        }

        
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "User":
                    break;


            }
        }


        protected void imgbtnlAppEmpSeaarch_Click(object sender, EventArgs e)
        {

            this.GetEmployeeName();

        }
        protected void imgbtnlFEmpSeaarch_Click(object sender, EventArgs e)
        {
            // this.GetLveAppEmployeeName();
        }


        protected void gvleaveInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label description = (Label)e.Row.FindControl("lgvledescription");
                Label lgvleavedays = (Label)e.Row.FindControl("lgvleavedays");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpsl")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "BBBB" || ASTUtility.Right(code, 4) == "CCCC")
                {
                    description.Font.Bold = true;
                    lgvleavedays.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }

            }
        }
        protected void gvleaveInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblempleaveinfo"];
            string trnid = ((Label)this.gvleaveInfo.Rows[e.RowIndex].FindControl("lgvltrnleaveid")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted failed');", true);
                return;
            }
            int rowindex = (this.gvleaveInfo.PageSize) * (this.gvleaveInfo.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            ViewState.Remove("tblempleaveinfo");
            ViewState["tblempleaveinfo"] = dv.ToTable();
            this.gvleaveInfo.DataSource = dv.ToTable();
            this.gvleaveInfo.DataBind();
        }
        private string GetUserCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["usrid"].ToString());

        }
        private void GetEmpid()
        {
            ViewState.Remove("tblUsrinfo");
            string comcod = GetComeCode();
            string usrid = GetUserCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERIND", "%%", usrid, "", "", "", "", "", "", "");


            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }
            ViewState["tblEmpid"] = ds1.Tables[0];

            this.lblempid.Text = ds1.Tables[0].Rows[0]["empid"].ToString();
        }


    }
}