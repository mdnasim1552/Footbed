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
    public partial class HREmpLeave : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        public static string ComLogo = "";
        public static string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.ShowView();
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetJobLocation();
                //this.GetCompany();
                //this.GetDeptName();
                string type = Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "FLeaveApp") ? "Leave Application Form (Manual)" : (type == "LeaveRule") ? "Company Leave Rule" : "Leave Application (Online)";

                this.txtaplydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.GetProjectName();
                //this.GetEmployeeName();

                Hashtable hst = (Hashtable)Session["tblLogin"];
                userid = hst["usrid"].ToString();
                string comcod = this.GetComeCode();
                ComLogo = userid == "5305139" ? new Uri(Server.MapPath(@"~\Image\LOGO" + "5306" + ".jpg")).AbsoluteUri : new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
            Session["hrcompnameadd"] = lst;

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
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
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
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
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
        private void GetJobLocation()
        {

            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJobLocation.DataTextField = "location";
            this.ddlJobLocation.DataValueField = "loccode";
            this.ddlJobLocation.DataSource = lst;
            this.ddlJobLocation.DataBind();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void imgbtnEmpSeach_Click(object sender, EventArgs e)
        {
            this.ShowCompLeaveRule();
        }

        private void ShowView()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetComeCode();
            switch (type)
            {
                case "LeaveRule":
                    this.divLvRule.Visible = true;
                    this.divCardNo.Visible = true;
                    this.txtdate.Text = System.DateTime.Today.ToString("yyyy");
                    string date = "01-Jan-" + System.DateTime.Today.ToString("yyyy");
                    this.txtyearstrtdate.Text = date;
                    this.txtyearenddate.Text = Convert.ToDateTime(date).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "LeaveApp":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "LEAVE APPLICATION Approval";
                    this.pnlReplacer.Visible = true;
                    this.divLvRule.Visible = false;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "FLeaveApp":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "LEAVE APPLICATION FORM VIEW/EDIT";
                    this.divLvRule.Visible = false;
                    this.txtformdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    switch (comcod)
                    {
                        case "4301":
                        case "4305":
                            this.rblstFormType.SelectedIndex = 2;
                            break;
                        default:
                            this.rblstFormType.SelectedIndex = 0;
                            break;
                    }
                    break;
            }

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ViewState["Uflag"] = 0;
            this.SelectIndex();
        }
        private void SelectIndex()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LeaveRule":
                    this.ShowLeaveRule();
                    break;

                case "LeaveApp":
                    this.ShowLeaveApp();

                    break;

                case "FLeaveApp":
                    this.ShowFLeaveApp();
                    break;
            }


        }
        private void ShowLeaveRule()
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.txtdate.ReadOnly = true;
                this.chkLeave.Visible = true;
                this.LbtnAutoGen.Visible = true;
                this.ShowCompLeaveRule();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.txtdate.ReadOnly = false;
                this.chkLeave.Visible = false;
                this.LbtnAutoGen.Visible = false;
                this.gvLeaveRule.DataSource = null;
                this.gvLeaveRule.DataBind();
                this.pnlleave.Visible = false;
                this.chkLeave.Checked = false;
                this.txtEmpSearch.Text = "";
            }
        }

        private void ShowLeaveApp()
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.chkPreLeave.Checked = false;
                this.lblleaveApp.Visible = true;
                this.lblleaveStatus.Visible = true;
                this.lblleaveInformation.Visible = true;
                this.PnlEmp.Visible = true;
                this.Pnlapply.Visible = true;
                this.PnlRmrks.Visible = true;
                this.pnlAttnStatus.Visible = true;
                this.lbtnOk.Text = "New";
                this.imgbtnlAppEmpSeaarch_Click(null, null);

            }
            else
            {
                this.chkPreLeave.Checked = true;
                this.lbtnOk.Text = "Ok";
                this.PnlEmp.Visible = false;
                this.Pnlapply.Visible = false;
                this.PnlRmrks.Visible = false;
                this.pnlAttnStatus.Visible = false;
                this.lblleaveApp.Visible = false;
                this.lblleaveStatus.Visible = false;
                this.lblleaveInformation.Visible = false;
                this.ddlEmpName.Items.Clear();
                this.ddlPreLeave.Items.Clear();
                this.lblDesignation.Text = "";
                this.lblJoiningDate.Text = "";
                this.txtLeavLreasons.Text = "";
                this.txtaddofenjoytime.Text = "";
                this.txtLeavRemarks.Text = "";
                this.gvLeaveApp.DataSource = null;
                this.gvLeaveApp.DataBind();
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                this.gvleaveInfo.DataSource = null;
                this.gvleaveInfo.DataBind();
                this.gvAttnStatus.DataSource = null;
                this.gvAttnStatus.DataBind();
            }
        }
        private void ShowFLeaveApp()
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.PnlEmplApp.Visible = true;
                this.imgbtnlFEmpSeaarch_Click(null, null);

            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.PnlEmplApp.Visible = false;
                this.ddlEmpNamelApp.Items.Clear();
                this.lblComPanylApp.Text = "";
                this.lblSectionlApp.Text = "";
                this.lblDesignationlApp.Text = "";
                this.lblJoiningDatelApp.Text = "";
                this.gvLeaveStatus01.DataSource = null;
                this.gvLeaveStatus01.DataBind();

            }
        }
        private void ShowCompLeaveRule()
        {
            ViewState.Remove("YearLeav");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string yearid = this.txtdate.Text;
            string company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string div = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string Dept = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string empcode = "%" + this.txtEmpSearch.Text.Trim() + "%";
            string jobLocation = this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87%" : this.ddlJobLocation.SelectedValue.ToString() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLEAVE", yearid, company, section, empcode, Dept, div, jobLocation, usrid, "");
            if (ds4.Tables[0].Rows.Count == 0 || ds4 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvLeaveRule.DataSource = null;
                this.gvLeaveRule.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds4.Tables[0]);
            ViewState["YearLeav"] = dt;
            this.LoadGrid();

        }

        private void LoadGrid()
        {

            DataTable dt = (DataTable)ViewState["YearLeav"];
            this.gvLeaveRule.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvLeaveRule.DataSource = dt;
            this.gvLeaveRule.DataBind();

            for (int i = 0; i < gvLeaveRule.Rows.Count; i++)
            {
                string monstatus = ((Label)gvLeaveRule.Rows[i].FindControl("lblmonstatus")).Text.Trim();

                if (monstatus == "A")
                {
                    this.gvLeaveRule.Rows[i].BackColor = Color.Violet;
                    this.gvLeaveRule.Rows[i].ForeColor = Color.Black;
                }
                else if (monstatus == "B")
                {
                    this.gvLeaveRule.Rows[i].BackColor = Color.SkyBlue;
                    this.gvLeaveRule.Rows[i].ForeColor = Color.Black;
                }

            }

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

        private void SaveValue()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["YearLeav"];
                int TblRowIndex;
                for (int i = 0; i < this.gvLeaveRule.Rows.Count; i++)
                {

                    string ernleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvel")).Text.Trim()).ToString();
                    string csleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvcl")).Text.Trim()).ToString();
                    string skleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvsl")).Text.Trim()).ToString();
                    string mtleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvml")).Text.Trim()).ToString();
                    string wpleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvWPl")).Text.Trim()).ToString();
                    string trpleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvTrL")).Text.Trim()).ToString();

                    string paternity = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtpaternity")).Text.Trim()).ToString();
                    string abrleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtAbrleav")).Text.Trim()).ToString();
                    string spcleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtSpcLeave")).Text.Trim()).ToString();
                    string compleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtCompleave")).Text.Trim()).ToString();

                    TblRowIndex = (gvLeaveRule.PageIndex) * gvLeaveRule.PageSize + i;

                    dt.Rows[TblRowIndex]["ernleave"] = ernleave;
                    dt.Rows[TblRowIndex]["csleave"] = csleave;
                    dt.Rows[TblRowIndex]["skleave"] = skleave;
                    dt.Rows[TblRowIndex]["mtleave"] = mtleave;
                    dt.Rows[TblRowIndex]["wpleave"] = wpleave;
                    dt.Rows[TblRowIndex]["trpleave"] = trpleave;
                    dt.Rows[TblRowIndex]["paternity"] = paternity;
                    dt.Rows[TblRowIndex]["abrleave"] = abrleave;
                    dt.Rows[TblRowIndex]["spcleave"] = spcleave;
                    dt.Rows[TblRowIndex]["compleave"] = compleave;

                }
                ViewState["YearLeav"] = dt;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }


        protected void gvLeaveRule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvLeaveRule.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void lnkbtnFUpLeave_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)ViewState["YearLeav"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string yearid = this.txtdate.Text;
            string ystrtdate = Convert.ToDateTime(this.txtyearstrtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string yenddate = Convert.ToDateTime(this.txtyearenddate.Text.Trim()).ToString("dd-MMM-yyyy");

            if (ystrtdate == "01-Jan-1900" || yenddate == "01-Jan-1900")
                return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string ernid = dt.Rows[i]["ernid"].ToString();
                string ernleave = dt.Rows[i]["ernleave"].ToString();
                string csid = dt.Rows[i]["csid"].ToString();
                string csleave = dt.Rows[i]["csleave"].ToString();
                string skid = dt.Rows[i]["skid"].ToString();
                string skleave = dt.Rows[i]["skleave"].ToString();
                string mtid = dt.Rows[i]["mtid"].ToString();
                string mtleave = dt.Rows[i]["mtleave"].ToString();
                string wpid = dt.Rows[i]["wpid"].ToString();
                string wpleave = dt.Rows[i]["wpleave"].ToString();
                string trpid = dt.Rows[i]["trpid"].ToString();
                string trpleave = dt.Rows[i]["trpleave"].ToString();

                //string hjid = dt.Rows[i]["hjid"].ToString();
                //string hajj = dt.Rows[i]["hajj"].ToString();
                string abrlvid = dt.Rows[i]["abrlvid"].ToString();
                string abrleave = dt.Rows[i]["abrleave"].ToString();

                string patid = dt.Rows[i]["patid"].ToString();
                string paternity = dt.Rows[i]["paternity"].ToString();
                string spclevcod = dt.Rows[i]["spclevcod"].ToString();
                string specialeave = dt.Rows[i]["spcleave"].ToString();
                string complvid = dt.Rows[i]["complvid"].ToString();
                string compleave = dt.Rows[i]["compleave"].ToString();

                bool result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAV", yearid, empid, ernid, ernleave, csid, csleave, skid, skleave, mtid,
                    mtleave, wpid, wpleave, trpid, trpleave, abrlvid, abrleave, patid, paternity, ystrtdate, yenddate, spclevcod, specialeave, complvid, compleave);

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Employee Leave is Not Updated!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Employee Leave Information Updated Successfully');", true);
                }
            }


            if (ConstantInfo.LogStatus == true)
            {
                string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string eventtype = "Company Leave rule Updated";
                string eventdesc = "Date: " + todate + ", Employee Type: " + this.ddlWstation.SelectedValue.ToString(); ;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);

            }
        }
        protected void lnkbtnGenLeave_Click(object sender, EventArgs e)
        {
            //string yearid =  this.txtyearstrtdate.Text;
            string yearid = System.DateTime.Today.ToString("dd-MMM-yyyy");

            string ernleave = Convert.ToDouble("0" + this.txternleave.Text).ToString();
            string csleave = Convert.ToDouble("0" + this.txtcsleave.Text).ToString();
            string skleave = Convert.ToDouble("0" + this.txtskleave.Text).ToString();
            string mtleave = Convert.ToDouble("0" + this.txtmtleave.Text).ToString();
            string wpleave = Convert.ToDouble("0" + this.txtWPayleave.Text).ToString();
            string trpleave = Convert.ToDouble("0" + this.txtTrainleave.Text).ToString();

            string paternity = Convert.ToDouble("0" + this.txtpaternity.Text).ToString();
            string aborleave = Convert.ToDouble("0" + this.txtAborLeave.Text).ToString();
            string specialleave = Convert.ToDouble("0" + this.TxtSpecialLeave.Text).ToString();

            DataTable dt = (DataTable)ViewState["YearLeav"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Date = dt.Rows[i]["joindate"].ToString();

                int mon = ASTUtility.Datediffday(Convert.ToDateTime(yearid), Convert.ToDateTime(Date));
                int maxday = (mon > 365) ? 365 : mon;
                string monstatus = dt.Rows[i]["monstatus"].ToString();
                string gender = dt.Rows[i]["gender"].ToString();


                dt.Rows[i]["ernleave"] = (mon <= 365) ? 0 : Convert.ToDouble(ernleave);

                dt.Rows[i]["csleave"] = (monstatus == "B") ? 5 : Convert.ToDouble(csleave);
                dt.Rows[i]["skleave"] = (monstatus == "B") ? 5 : Convert.ToDouble(skleave);
                dt.Rows[i]["mtleave"] = (gender == "M") ? 0 : Convert.ToDouble(mtleave);
                dt.Rows[i]["wpleave"] = wpleave;
                dt.Rows[i]["trpleave"] = trpleave;
                dt.Rows[i]["paternity"] = paternity;
                dt.Rows[i]["abrleave"] = aborleave;
                dt.Rows[i]["spcleave"] = specialleave;

            }
            ViewState["YearLeav"] = dt;
            this.chkLeave.Checked = false;
            this.chkLeave_CheckedChanged(null, null);
            this.LoadGrid();



        }
        protected void lnkbtnGenLeave1_Click(object sender, EventArgs e)
        {
            //string yearid =  this.txtyearstrtdate.Text;
            string yearid = Convert.ToDateTime(this.txtyearenddate.Text).ToString("dd-MMM-yyyy");

            string ernleave = Convert.ToDouble("0" + this.txternleave.Text).ToString();
            string csleave = Convert.ToDouble("0" + this.txtcsleave.Text).ToString();
            string skleave = Convert.ToDouble("0" + this.txtskleave.Text).ToString();
            string mtleave = Convert.ToDouble("0" + this.txtmtleave.Text).ToString();
            string wpleave = Convert.ToDouble("0" + this.txtWPayleave.Text).ToString();
            string trpleave = Convert.ToDouble("0" + this.txtTrainleave.Text).ToString();

            string paternity = Convert.ToDouble("0" + this.txtpaternity.Text).ToString();
            string abrleave = Convert.ToDouble("0" + this.txtAborLeave.Text).ToString();
            string spcialeave = Convert.ToDouble("0" + this.TxtSpecialLeave.Text).ToString();

            DataTable dt = (DataTable)ViewState["YearLeav"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Date = dt.Rows[i]["joindate"].ToString();

                int mon = ASTUtility.Datediffday(Convert.ToDateTime(yearid), Convert.ToDateTime(Date));
                int maxday = (mon > 365) ? 365 : mon;
                string monstatus = dt.Rows[i]["monstatus"].ToString();


                dt.Rows[i]["ernleave"] = (mon <= 365) ? 0 : Convert.ToDouble(ernleave);
                dt.Rows[i]["csleave"] = maxday * 0.02739726; ///(monstatus == "B") ? 5 : Convert.ToDouble(csleave);
                dt.Rows[i]["skleave"] = maxday * 0.038356164; // (monstatus == "B") ? 5 : Convert.ToDouble(skleave);
                dt.Rows[i]["mtleave"] = mtleave;
                dt.Rows[i]["wpleave"] = wpleave;
                dt.Rows[i]["trpleave"] = trpleave;
                dt.Rows[i]["paternity"] = paternity;
                dt.Rows[i]["abrleave"] = abrleave;
                dt.Rows[i]["spcleave"] = spcialeave;

            }
            ViewState["YearLeav"] = dt;
            this.chkLeave.Checked = false;
            this.chkLeave_CheckedChanged(null, null);
            this.LoadGrid();



        }
        protected void chkLeave_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkLeave.Checked == true)
            {
                this.pnlleave.Visible = true;
                this.txtcsleave.Text = "";
                this.txternleave.Text = "";
                this.txtskleave.Text = "";
                this.txtmtleave.Text = "";
                this.txtWPayleave.Text = "";
                this.txtpaternity.Text = "";
                this.txtAborLeave.Text = "";
            }
            else
            {
                this.pnlleave.Visible = false;
            }
        }




        private void GetEmployeeName()
        {
            ViewState.Remove("tblEmpDesc");
            ViewState.Remove("tblleave");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string pactcode = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string dept = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString()) + "%";
            string IdCardNo = "%%";
            string jobLocation = this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87%" : this.ddlJobLocation.SelectedValue.ToString() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", company, pactcode, IdCardNo, dept, jobLocation, usrid, "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject.ToString() + "');", true);
                return;
            }

            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();

            ViewState["tblEmpDesc"] = ds1.Tables[0];
            this.gvLeaveApp.DataSource = ds1.Tables[1];
            this.gvLeaveApp.DataBind();
            ViewState["tblleave"] = ds1.Tables[1];

            this.gvAttnStatus.DataSource = null;
            this.gvAttnStatus.DataBind();
            // this.ddlEmpName_SelectedIndexChanged(null, null);
        }


        private void GetLeaveid()
        {
            string comcod = this.GetComeCode();
            string mTRNNo = "NEWTRR";
            if (this.ddlPreLeave.Items.Count > 0)
                mTRNNo = this.ddlPreLeave.SelectedValue.ToString();

            if (mTRNNo == "NEWTRR")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLEAVEID", "", "", "", "", "", "", "", "", "");
                if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
                {

                    return;
                }
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lbltrnleaveid.Text = ds1.Tables[0].Rows[0]["ltrnid"].ToString().Trim();

                    this.ddlPreLeave.DataTextField = "ltrnid";
                    this.ddlPreLeave.DataValueField = "ltrnid";
                    this.ddlPreLeave.DataSource = ds1.Tables[0];
                    this.ddlPreLeave.DataBind();
                }

            }




        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblEmpDesc"];
            DataRow dr1 = dt.NewRow();
            dr1["empname"] = "None";
            dr1["empid"] = "000000000000";
            dt.Rows.Add(dr1);
            DataView dv = dt.Copy().DefaultView;
            dv.RowFilter = ("empid <>" + empid);
            this.ddlReplaceremp.DataTextField = "empname";
            this.ddlReplaceremp.DataValueField = "empid";
            this.ddlReplaceremp.DataSource = dv.ToTable();
            this.ddlReplaceremp.DataBind();
            this.ddlReplaceremp.SelectedValue = "000000000000";

            this.ShowEmppLeave();
            this.EmpLeaveInfo();
            this.ShowDailyAttnStatus();

        }
        private void ShowDailyAttnStatus()
        {

            try
            {
                string comcod = this.GetComeCode();
                string empid = this.ddlEmpName.SelectedValue.ToString();
                string monthid = System.DateTime.Today.ToString("yyyyMM");
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "DAILY_ATTN_STATUS", monthid, empid, "", "", "");
                if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject.ToString() + "');", true);
                    return;
                }

                string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                DateTime fmondate = Convert.ToDateTime("01-" + todate.Substring(3, 3) + todate.Substring(6));
                DateTime tmondate = Convert.ToDateTime(fmondate).AddMonths(1).AddDays(-1);
                int i = 0;
                for (i = 0; i <= 31; i++)
                {
                    if (fmondate > tmondate)
                        break;
                    this.gvAttnStatus.Columns[i].Visible = true;
                    fmondate = fmondate.AddDays(1);

                }

                this.gvAttnStatus.DataSource = ds1.Tables[0];
                this.gvAttnStatus.DataBind();

                int j = 0;
                for (i = 1; i <= 31; i++)
                {
                    string sl = ASTUtility.Right("0" + i.ToString(), 2);

                    string lblname = "lblgvday" + sl;
                    string estatus = ((Label)this.gvAttnStatus.Rows[0].FindControl(lblname)).Text.Trim();
                    Label lblgvcell = ((Label)this.gvAttnStatus.Rows[0].FindControl(lblname));

                    if (estatus == "AB")
                    {
                        lblgvcell.Attributes["style"] = "background-color:red; color:white;";
                        this.gvAttnStatus.HeaderRow.Cells[j].Attributes["style"] = "background-color:red; color:white;";

                    }
                    else if (estatus == "WH" || estatus == "FH")
                    {
                        lblgvcell.Attributes["style"] = "background-color:yellow;";
                        this.gvAttnStatus.HeaderRow.Cells[j].Attributes["style"] = "background-color:yellow;";
                    }

                    else if (estatus == "EL" || estatus == "CL" || estatus == "SL" || estatus == "ML" || estatus == "LWP" || estatus == "LFT" || estatus == "PL" || estatus == "ABL" || estatus == "AL" || estatus == "LV")
                    {
                        lblgvcell.Attributes["style"] = "background-color:blue; color:white;";
                        this.gvAttnStatus.HeaderRow.Cells[j].Attributes["style"] = "background-color:blue; color:white;";
                    }
                    j++;
                }
            }

            catch (Exception ex)
            {


            }

        }
        private void ShowEmppLeave()
        {

            ViewState.Remove("tblleavest");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string aplydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)ViewState["tblEmpDesc"];
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");

            if (dr1.Length > 0)
            {
                this.lblDesignation.Text = dr1[0]["desig"].ToString();
                this.lblJoiningDate.Text = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");
            }
            string year = Convert.ToDateTime(this.txtaplydate.Text).ToString("yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, aplydat, year, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject.ToString() + "');", true);
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
            this.ddlPreLeave.Items.Clear();

            this.gvLeaveApp.DataSource = null;
            this.gvLeaveApp.DataBind();

            this.gvLeaveStatus.DataSource = null;
            this.gvLeaveStatus.DataBind();

            this.gvleaveInfo.DataSource = null;
            this.gvleaveInfo.DataBind();

            this.gvAttnStatus.DataSource = null;
            this.gvAttnStatus.DataBind();

            this.txtLeavLreasons.Text = "";
            this.txtaddofenjoytime.Text = "";
            this.txtLeavRemarks.Text = "";
            this.txtdutiesnameandDesig.Text = "";
            this.lblDesignation.Text = "";
            this.lblJoiningDate.Text = "";
            this.txtApprdate.Text = "";
            this.GetEmployeeName();
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
            string code = "";
            for (int i = 0; i < this.gvLeaveApp.Rows.Count; i++)
            {
                code = ((Label)this.gvLeaveApp.Rows[i].FindControl("lblgvgcodapply")).Text.Trim();


                // code=
                //TimeSpan ts = (this.CalExt3.SelectedDate.Value - this.CalExt2.SelectedDate.Value);
                int leaveday = Convert.ToInt32("0" + ((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvlapplied")).Text.Trim());

                if (leaveday > 0)
                {
                    string stdat = Convert.ToDateTime(((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvenjoydt1")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string endat = Convert.ToDateTime(stdat).AddDays(leaveday - 1).ToString("dd-MMM-yyyy");
                    dt.Rows[i]["lapplied"] = leaveday;
                    dt.Rows[i]["lenjoydt1"] = stdat;
                    dt.Rows[i]["lenjoydt2"] = endat;
                    // double enjleave = Convert.ToDouble(dt1.Rows[i]["ltaken"]);




                    DataRow[] dr1 = dt1.Select("gcod='" + code + "'");
                    if (dr1.Length == 0)
                        continue;
                    double Clsleave = dt1.Select("gcod='" + code + "'").Length == 0 ? 0 : Convert.ToDouble(dt1.Select("gcod='" + code + "'")[0]["pbal"]);
                    dr1[0]["applyday"] = leaveday;
                    dr1[0]["appday"] = leaveday;
                    dr1[0]["applydate"] = stdat;
                    dr1[0]["appdate"] = stdat;
                    dr1[0]["balleave"] = Clsleave - leaveday;
                    dr1[0]["tltakreq"] = leaveday;
                }

            }

            this.gvLeaveApp.DataSource = dt;
            this.gvLeaveApp.DataBind();
            ViewState["tblleave"] = dt;


            //---------For Status table --------------------------------------

            ViewState["tblleavest"] = dt1;
            this.Data_Bind();
        }

        private void GetLveAppEmployeeName()
        {
            ViewState.Remove("tblEmpDesc");
            string comcod = this.GetComeCode();
            string company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string IdCardNo = "%" + "%";
            string dept = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString() + "%";
            //string date = Convert.ToDateTime(this.txtformdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", company, section, IdCardNo, dept, "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject.ToString() + "');", true);
                return;
            }

            this.ddlEmpNamelApp.DataTextField = "empname";
            this.ddlEmpNamelApp.DataValueField = "empid";
            this.ddlEmpNamelApp.DataSource = ds1.Tables[0];
            this.ddlEmpNamelApp.DataBind();
            ViewState["tblEmpDesc"] = ds1.Tables[0];
            //this.ddlEmpNamelApp_SelectedIndexChanged(null, null);

        }


        protected void ddlEmpNamelApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState.Remove("tblleavest");

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNamelApp.SelectedValue.ToString();
            string aplydat = Convert.ToDateTime(this.txtformdate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)ViewState["tblEmpDesc"];
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");

            if (dr1.Length > 0)
            {

                this.lblComPanylApp.Text = dr1[0]["companyname"].ToString();
                this.lblSectionlApp.Text = dr1[0]["section"].ToString();
                this.lblDesignationlApp.Text = dr1[0]["desig"].ToString();
                this.lblJoiningDatelApp.Text = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");


            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, aplydat, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject.ToString() + "');", true);
                this.gvLeaveStatus01.DataSource = null;
                this.gvLeaveStatus01.DataBind();
                return;
            }


            this.gvLeaveStatus01.DataSource = ds1.Tables[0];
            this.gvLeaveStatus01.DataBind();
            ViewState["tblleavest"] = ds1.Tables[0];


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
            try
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
                string year = System.DateTime.Today.ToString("yyyy");
                //string calltype = ((this.rblstapptype.SelectedIndex == 0) ? "LEAVESTATUS" : (this.rblstapptype.SelectedIndex == 1) ? "LEAVESTATUS01" : "LEAVESTATUS02");
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, date, year, "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject.ToString() + "');", true);
                    this.gvLeaveStatus.DataSource = null;
                    this.gvLeaveStatus.DataBind();
                    return;
                }


                // Session["tblleavest"] = ds1.Tables[0];
                DataTable dt1 = (DataTable)ViewState["tblleave"];
                DataTable dt2 = ds1.Tables[0];
                foreach (DataRow dr in dt1.Rows)
                {
                    dr["lapplied"] = "0";
                    dr["lenjoydt1"] = "01-jan -1900";
                    dr["lenjoydt2"] = "01-jan -1900";
                }

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
                drls[0]["appday"] = (Convert.ToDateTime(drp[0]["aprdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "0" : drp[0]["lapplied"];
                drls[0]["applydate"] = drp[0]["strtdat"];
                drls[0]["appdate"] = drp[0]["aprdat"];
                // drls[0]["todate"] = drp[0]["strtdat"];

                drls[0]["balleave"] = (Convert.ToDateTime(drp[0]["aprdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? Clsleave : Clsleave - leaveday;
                drls[0]["tltakreq"] = (Convert.ToDateTime(drp[0]["aprdat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? leaveday : 0;
                //drls[0]["balleave"] = Clsleave - (leaveday + enjleave);
                //drls[0]["tltakreq"] = (leaveday + enjleave);

                ViewState["tblleave"] = dt1;
                ViewState["tblleavest"] = dt2;
                //Genral info
                this.lbltrnleaveid.Text = this.ddlPreLeave.SelectedValue.ToString();
                this.txtaplydate.Text = Convert.ToDateTime(drp[0]["aplydat"]).ToString("dd-MMM-yyyy");
                this.txtApprdate.Text = Convert.ToDateTime(drp[0]["aprdat"]).ToString("dd-MMM-yyyy");
                this.txtLeavLreasons.Text = drp[0]["lreason"].ToString();
                this.txtaddofenjoytime.Text = drp[0]["addlentime"].ToString();
                this.txtLeavRemarks.Text = drp[0]["lrmarks"].ToString();
                this.txtdutiesnameandDesig.Text = drp[0]["denameadesig"].ToString();
                //this.ddlReplaceremp.SelectedValue = drp[0]["rempid"].ToString();

                //gvbind
                this.gvLeaveApp.DataSource = dt1;
                this.gvLeaveApp.DataBind();
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
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
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
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
                case "LeaveRule":
                    break;

                case "LeaveApp":

                    this.PrintLeaveApprove();
                    break;


                case "FLeaveApp":

                    string emptype = this.ddlWstation.SelectedValue.Substring(0, 4).ToString();


                    if ((emptype == "9401") || (emptype == "9402"))
                    {
                        this.PrintRptLeavApp();

                    }
                    else if (emptype == "9403")
                    {
                        this.PrintRptLeavBangla();
                    }
                    else
                    {
                        //  this.PrintRDLCAppform();
                    }
                    break;
            }
        }



        private void PrintRptLeavApp()
        {
            // IQBAL NAYAN
            string empid1 = this.ddlEmpNamelApp.SelectedValue.ToString();

            DataTable dt1 = (DataTable)ViewState["tblEmpDesc"];
            DataRow[] dr1 = dt1.Select("empid='" + empid1 + "'");
            string empname = dr1[0]["empname1"].ToString();
            string empid = dr1[0]["idcardno"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string empname = this.ddlEmpNamelApp.SelectedItem.Text.Substring(8);
            string department = this.ddlDept.SelectedItem.Text.ToString();
            //string empid = this.ddlEmpNamelApp.SelectedItem.Text.Substring(0, 6);
            string desg = this.lblDesignationlApp.Text;
            string joindate = this.lblJoiningDatelApp.Text.ToString();
            string section = this.ddlSection.SelectedItem.Text.ToString();
            //  DataView dv = ((DataTable)Session["tblleavest"]).DefaultView; 
            // dv.RowFilter = ("appday>0");
            DataTable dt = (DataTable)ViewState["tblleavest"];    // dv.ToTable();

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.LeaveApp>();


            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptLeaveFormEng", lst, null, null);



            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("empname", empname));
            rpt1.SetParameters(new ReportParameter("department", department));
            rpt1.SetParameters(new ReportParameter("empid", empid));
            rpt1.SetParameters(new ReportParameter("desg", desg));
            rpt1.SetParameters(new ReportParameter("section", section));
            rpt1.SetParameters(new ReportParameter("joindate", joindate));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Leave Application Form"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintRptLeavBangla()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = this.GetComeCode();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpNamelApp.SelectedValue.ToString();
            
            DataTable dtemp = (DataTable)ViewState["tblEmpDesc"];
            DataRow[] dr1 = dtemp.Select("empid='" + empid + "'");
            string empname = "";
            string cardno = "";
            string department = "";
            string desg = "";
            string joindate = "";
            string section = "";
            if (dr1.Length > 0)
            {
                cardno = dr1[0]["idcardno"].ToString();
                empname = dr1[0]["empnamebn"].ToString();
                department = dr1[0]["deptbn"].ToString();
                desg = dr1[0]["desig"].ToString();
                joindate = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");
                section = dr1[0]["sectionbn"].ToString();

            }
            //  DataView dv = ((DataTable)Session["tblleavest"]).DefaultView; 
            // dv.RowFilter = ("appday>0");
            DataTable dt = (DataTable)ViewState["tblleavest"];    // dv.ToTable();

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.LeaveApp>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptLeaveFormBang", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", "এডিসন ফুটওয়্যার লিঃ "));
            rpt1.SetParameters(new ReportParameter("comadd", "তালতলী , মির্জাপুর ,হোতাপাড়া, গাজীপুর।"));
            rpt1.SetParameters(new ReportParameter("empname", empname));
            rpt1.SetParameters(new ReportParameter("department", department));
            rpt1.SetParameters(new ReportParameter("empid", cardno));
            rpt1.SetParameters(new ReportParameter("desg", desg));
            rpt1.SetParameters(new ReportParameter("section", section));
            rpt1.SetParameters(new ReportParameter("joindate", joindate));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintRDLCAppform()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empname = this.ddlEmpNamelApp.SelectedItem.Text.Substring(7);
            string department = this.lblSectionlApp.Text;
            string empid = this.ddlEmpNamelApp.SelectedItem.Text.Substring(0, 6);
            string desg = this.lblDesignationlApp.Text;

            string location = "";
            //  DataView dv = ((DataTable)Session["tblleavest"]).DefaultView; 
            // dv.RowFilter = ("appday>0");
            DataTable dt = (DataTable)ViewState["tblleavest"];    // dv.ToTable();

            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.LeaveApp>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptLeaveApp", lst, null, null);

            //Hashtable reportParm = new Hashtable();
            //reportParm["comnam"] = comnam;
            //reportParm["comadd"] = comadd;
            //reportParm["headertxt"] = "Leave Form";

            //reportParm["Empname"] = this.ddlEmpNamelApp.SelectedItem.Text.Substring(7);
            //reportParm["depertment"] = this.lblSectionlApp.Text;
            //reportParm["empId"] = this.ddlEmpNamelApp.SelectedItem.Text.Substring(0, 6);
            //reportParm["Designation"] = this.lblDesignationlApp.Text;
            //reportParm["Locaion"] = "";// this.txtLeavLreasons.Text;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("empname", empname));
            rpt1.SetParameters(new ReportParameter("department", department));
            rpt1.SetParameters(new ReportParameter("empid", empid));
            rpt1.SetParameters(new ReportParameter("desg", desg));
            rpt1.SetParameters(new ReportParameter("location", location));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Leave Application Form(Manually)"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintLeaveApprove()
        {

            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "5301":
                case "5305":
                case "5306":
                    this.PrintLeaveAppForm();
                    break;

                default:
                    this.PrintLeaveAppForm();
                    break;
            }

        }

        private void PrintLeaveAppForm()
        {
            int index = this.ddlReportType.SelectedIndex;

            switch (index)
            {
                case 0:
                    this.PrintLeaveAppFormBangla();
                    break;

                default:
                    this.PrintLeaveAppFormEng();
                    break;

            }

        }

        private void PrintLeaveAppFormBangla()
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
            string repempname = "";
            string emptypeDesc = "";
            if (this.ddlEmpName.SelectedValue.ToString() == "000000000000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Plese Select Employee First!');", true);
                return;
            }
            else
            {
                repempname = (this.ddlReplaceremp.SelectedValue.ToString() == "000000000000") ? "" : this.ddlReplaceremp.SelectedItem.ToString();
            }

            //string applydat = repempname.Length == 0 ? "" : Convert.ToDateTime(this.txtaplydate.Text).ToString("dd/MM/yyyy");
            string applydat =  Convert.ToDateTime(this.txtaplydate.Text).ToString("dd/MM/yyyy");
            //string issueDateSuspension = ASITUtility02.NumBn(issuDate.Substring(0, 2)) + "/" + ASITUtility02.NumBn(issuDate.Substring(3, 2)) + "/" + ASITUtility02.NumBn(issuDate.Substring(6, 4)) + " ইং";
            string applydatBn =  ASITUtility02.NumBn(applydat.Substring(0, 2)) + "/" + ASITUtility02.NumBn(applydat.Substring(3, 2)) + "/" + ASITUtility02.NumBn(applydat.Substring(6, 4)) + " ইং";
            string reason = this.txtLeavLreasons.Text.Trim();
            string addentime = this.txtaddofenjoytime.Text.Trim();
            string remarks = this.txtLeavRemarks.Text.Trim();
            string dnameadesig = this.txtdutiesnameandDesig.Text.Trim();
            if (dnameadesig.Length == 0)
            {
                dnameadesig = repempname;

            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            string emptype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string empid = this.ddlEmpName.SelectedValue.ToString();
           
            DataTable dtemp = (DataTable)ViewState["tblEmpDesc"];
            DataRow[] dr1 = dtemp.Select("empid='" + empid + "'");

            if (dr1.Length > 0)
            {
                cardno = dr1[0]["idcardno"].ToString();
                empname = dr1[0]["empname1"].ToString();
                department = dr1[0]["deptbn"].ToString();
                desg = dr1[0]["desigb"].ToString();
                joindate = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd/MM/yyyy");
                section = dr1[0]["sectionbn"].ToString();
                line = dr1[0]["lineb"].ToString();
                emptypeDesc = dr1[0]["emptype"].ToString();

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
                joinafter = Convert.ToDateTime(dv.ToTable().Rows[0]["enddat"]).AddDays(1).ToString("dd/MM/yyyy");
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
                leavetype = dv.ToTable().Rows[0]["gcod"].ToString();
                if (reason.Length == 0)
                {
                    reason = dv.ToTable().Rows[0]["lreason"].ToString();
                }
            }


            LocalReport Rpt1 = new LocalReport();


            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptLeavAppFormBn", list, null, null);
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
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("todate", todate));
            Rpt1.SetParameters(new ReportParameter("joinafter", joinafter));
            Rpt1.SetParameters(new ReportParameter("applydat", applydatBn));
            Rpt1.SetParameters(new ReportParameter("reason", reason));
            Rpt1.SetParameters(new ReportParameter("addentime", addentime));
            Rpt1.SetParameters(new ReportParameter("remarks", remarks));
            Rpt1.SetParameters(new ReportParameter("emptype", emptype));
            Rpt1.SetParameters(new ReportParameter("emptype", emptype));
            Rpt1.SetParameters(new ReportParameter("emptypeDesc", emptypeDesc));
            Rpt1.SetParameters(new ReportParameter("dnameadesig", dnameadesig));
            Rpt1.SetParameters(new ReportParameter("totaldays", totaldays));
            Rpt1.SetParameters(new ReportParameter("leavetype", leavetype));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintLeaveAppFormEng()
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
            string repempname = "";
            string emptypeDesc = "";
            if (this.ddlEmpName.SelectedValue.ToString() == "000000000000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Plese Select Employee First!');", true);
                return;
            }
            else
            {
                repempname = (this.ddlReplaceremp.SelectedValue.ToString() == "000000000000") ? "" : this.ddlReplaceremp.SelectedItem.ToString();
            }

            string applydat = "";
            string reason = this.txtLeavLreasons.Text.Trim(); ;
            string addentime = this.txtaddofenjoytime.Text.Trim();
            string remarks = this.txtLeavRemarks.Text.Trim();
            string dnameadesig = this.txtdutiesnameandDesig.Text.Trim();
            if (dnameadesig.Length == 0)
            {
                dnameadesig = repempname;

            }
            string emptype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            string empid = this.ddlEmpName.SelectedValue.ToString();
            
            DataTable dtemp = (DataTable)ViewState["tblEmpDesc"];
            DataRow[] dr1 = dtemp.Select("empid='" + empid + "'");

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
                emptypeDesc = dr1[0]["emptype"].ToString();
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

                applydat = Convert.ToDateTime(dv.ToTable().Rows[0]["aplydat"]).ToString("dd-MMM-yyyy");

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

                applydat = Convert.ToDateTime(dv.ToTable().Rows[0]["aplydat"]).ToString("dd-MMM-yyyy");
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
            applydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd/MM/yyyy");
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptLeavAppFormEng", list, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("line", line));
            Rpt1.SetParameters(new ReportParameter("companyname", comname));
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comname));
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
            Rpt1.SetParameters(new ReportParameter("emptypeDesc", emptypeDesc));
            Rpt1.SetParameters(new ReportParameter("dnameadesig", dnameadesig));
            Rpt1.SetParameters(new ReportParameter("totaldays", totaldays));
            Rpt1.SetParameters(new ReportParameter("leavetype", leavetype));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        public string GetBanglaNumber(int number)
        {
            return string.Concat(number.ToString().Select(c => (char)('\u09E6' + c - '0')));
        }
        protected void lnkbtnUpdateLeave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string PostSession = Sessionid;
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            this.SaveLeave();
            DataTable dt = ((DataTable)ViewState["tblleave"]).Copy();
            string comcod = this.GetComeCode();
            if (this.ddlPreLeave.Items.Count == 0)
                this.GetLeaveid();

            string trnid = this.lbltrnleaveid.Text;
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string repemp = this.ddlReplaceremp.SelectedValue.ToString();

            //Leave Balance Exceed
            DataTable dt1 = (DataTable)ViewState["tblleavest"];
            string code = "";
            for (int i = 0; i < this.gvLeaveApp.Rows.Count; i++)
            {
                code = ((Label)this.gvLeaveApp.Rows[i].FindControl("lblgvgcodapply")).Text.Trim();
                int leavapplyday = Convert.ToInt32("0" + ((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvlapplied")).Text.Trim());

                if (leavapplyday > 0)
                {
                    DataRow[] dr1 = dt1.Select("gcod='" + code + "'");
                    if (dr1.Length == 0)
                        continue;

                    double pballeave = dt1.Select("gcod='" + code + "'").Length == 0 ? 0 : Convert.ToDouble(dt1.Select("gcod='" + code + "'")[0]["pbal"]);
                    if (leavapplyday == pballeave)
                    {
                        ;
                    }
                    else if (pballeave == 0 || leavapplyday > pballeave)
                    {
                        this.ShowEmppLeave();
                        this.EmpLeaveInfo();
                        this.lnkbtnRef_Click(null, null);
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Leave Balance Exceed or No Balance!');", true);
                        return;
                    }

                }

            }

            // Duplicate Entry
            DataRow[] dr = dt.Select("lapplied>0");
            string gcod = dr[0]["gcod"].ToString();
            string frmdate = Convert.ToDateTime(dr[0]["lenjoydt1"]).ToString("dd-MMM-yyyy");
            // string todate = Convert.ToDateTime(dr[0]["lenjoydt2"]).ToString("dd-MMM-yyyy");

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVEAPPONSAMEDATE", empid, gcod, frmdate, "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                ;

            }

            else
            {
                DataView dv1 = ds1.Tables[0].DefaultView;
                dv1.RowFilter = ("ltrnid <>'" + trnid + "'");
                DataTable dtc = dv1.ToTable();
                if (dtc.Rows.Count == 0)
                    ;
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Existed!. Not Possible Same Date Leave.');", true);
                    return;
                }
            }


            //Already Present

            string dayid = Convert.ToDateTime(dr[0]["lenjoydt1"]).ToString("yyyyMMdd");
            DataSet dsp = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "ISEMPLOYEEPRESENT", empid, dayid, "", "", "", "", "", "");
            if (dsp.Tables[0].Rows.Count == 0)
            {
                ;

            }

            else
            {


                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Presented! Not Possible Leave in Present Day');", true);
                return;

            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double lapplied = Convert.ToDouble(dt.Rows[i]["lapplied"]);
                if (lapplied > 0)
                {

                    gcod = dt.Rows[i]["gcod"].ToString();
                    frmdate = Convert.ToDateTime(dt.Rows[i]["lenjoydt1"]).ToString("dd-MMM-yyyy");
                    string todate = Convert.ToDateTime(dt.Rows[i]["lenjoydt2"]).ToString("dd-MMM-yyyy");
                    string applydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
                    string reason = this.txtLeavLreasons.Text.Trim(); ;
                    string addentime = this.txtaddofenjoytime.Text.Trim();
                    string remarks = this.txtLeavRemarks.Text.Trim();
                    string dnameadesig = this.txtdutiesnameandDesig.Text.Trim();

                    string APRdate = ((this.txtApprdate.Text).Length == 0) ? "01-Jan-1900" : Convert.ToDateTime(this.txtApprdate.Text).ToString("dd-MMM-yyyy");
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAVAPP", trnid, empid, gcod, frmdate, todate, applydat, reason, remarks, APRdate, addentime, dnameadesig, repemp,
                        PostedByid, Posttrmid, PostSession, Posteddat);
                }
            }

            this.ShowEmppLeave();
            this.EmpLeaveInfo();
            this.lnkbtnRef_Click(null, null);
            //this.RefreshData();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Leave Updated Successfully');", true);

        }
        private void RefreshData()
        {
            this.ddlPreLeave.Items.Clear();
            this.txtLeavLreasons.Text = "";
            this.txtaddofenjoytime.Text = "";
            this.txtLeavRemarks.Text = "";
            this.txtdutiesnameandDesig.Text = "";
            this.lblDesignation.Text = "";
            this.lblJoiningDate.Text = "";
            this.txtApprdate.Text = "";
            this.ddlReplaceremp.Items.Clear();
            //this.GetEmployeeName();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {


            string comcod = this.GetComeCode();
            string trnid = this.lbltrnleaveid.Text;
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Leave Delete Failed!');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Leave Deleted Sucessfully');", true);



        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "LeaveRule":
                    this.SaveValue();
                    this.LoadGrid();
                    break;

                case "LeaveApp":
                    break;

                case "FLeaveApp":
                    break;
            }
        }


        protected void imgbtnlAppEmpSeaarch_Click(object sender, EventArgs e)
        {

            this.GetEmployeeName();

        }
        protected void imgbtnlFEmpSeaarch_Click(object sender, EventArgs e)
        {
            this.GetLveAppEmployeeName();
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
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Leave Delete Failed!');", true);
                return;
            }

            int rowindex = (this.gvleaveInfo.PageSize) * (this.gvleaveInfo.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            ViewState.Remove("tblempleaveinfo");
            ViewState["tblempleaveinfo"] = dv.ToTable();
            this.gvleaveInfo.DataSource = dv.ToTable();
            this.gvleaveInfo.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Leave Deleted Sucessfully');", true);
        }




        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.txtyearenddate.Text = "31-Dec-" + this.txtdate.Text.Trim();
            this.txtyearstrtdate.Text = Convert.ToDateTime(this.txtyearenddate.Text).AddYears(-1).AddDays(1).ToString("dd-MMM-yyyy");
        }


    }
}