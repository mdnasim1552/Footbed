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
using System.IO;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using System.Drawing.Imaging;
using System.Drawing;

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class HREmpEntry : System.Web.UI.Page
    {
        Common Common = new Common();
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        System.IO.Stream image_file = null;
        string Upload = "";
        int size = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");


                txtImgUrl.Text = "D:\\asit-website-main\\asit-website-main\\src\\images";

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Personal") ? "EMPLOYEE PERSONAL INFORMATION "
                    : (this.Request.QueryString["Type"].ToString().Trim() == "Aggrement") ? "EMPLOYMENT AGREEMENT INFORMATION"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "Officetime") ? "OFFICE TIME SETUP"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "shifttime") ? "SHIFT/ROSTER SETUP ENTRY"
                    : "EMPLOYMENT OFFICE TIME INFORMATION ";
                this.SelectView();
                GetAllOrganogramList();
                GetWorkStation();
                GetJobLocation();
                this.ShowOffTime();

                string emptype = this.Request.QueryString["emptype"].ToString().Trim();
                if (emptype == "New")
                {
                    chknewEmp.Checked = true;
                    this.chknewEmp_CheckedChanged(null, null);
                }
                else
                {
                    chknewEmp.Checked = false;

                }
                this.rbtPaymentType.SelectedIndex = 1;

                this.CommonButton();
                //this.ddlSection_SelectedIndexChanged(null, null);

            }

        }


        private void GetJobLocation()
        {

            string Type = this.Request.QueryString["Type"];
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            string jobLoc = this.Request.QueryString["jobloc"] ?? "";

            switch (Type)
            {
                case "Aggrement":
                    this.ddlJob.DataTextField = "location";
                    this.ddlJob.DataValueField = "loccode";
                    this.ddlJob.DataSource = lst;
                    this.ddlJob.DataBind();
                    if (jobLoc.Length > 0)
                        this.ddlJob.SelectedValue = jobLoc;
                    break;

                default:
                    this.ddlJobLocation.DataTextField = "location";
                    this.ddlJobLocation.DataValueField = "loccode";
                    this.ddlJobLocation.DataSource = lst;
                    this.ddlJobLocation.DataBind();
                    break;
            }




        }

        private void CommonButton()
        {

            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Update Employee Wekend/Holiday";
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string wrkstation = this.ddlWstation.SelectedValue.ToString();
            string empid = this.ddlPEmpName.SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "SETUP_EMPLOYEE_WEEKEND", wrkstation, empid, "", "", "", "", "", "", "");
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Weekend/Holidays Updated');", true);


                return;
            }

        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetWorkStation()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            string qtype = this.Request.QueryString["Type"].ToString().Trim();


            var lst = getlist.GetWstation(comcod, userid);

            if (qtype == "shifttime")
            {
                if (this.GetCompCode() == "5301")
                {
                    lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000" && x.actcode != "940100000000" && x.actcode != "940200000000");

                }
                else
                {
                    lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

                }

            }
            else
            {
                lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            }

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

            this.ddlWstation1.DataTextField = "actdesc";
            this.ddlWstation1.DataValueField = "actcode";
            this.ddlWstation1.DataSource = lst;
            this.ddlWstation1.DataBind();

            if (qtype == "Aggrement")
            {

                if (Request.QueryString.AllKeys.Contains("section"))
                {
                    this.ddlWstation.SelectedValue = (this.Request.QueryString["section"] == "") ? "940100000000" : ASTUtility.Left(this.Request.QueryString["section"].ToString(), 4) + "00000000";
                }
                this.ddlWstation_SelectedIndexChanged(null, null);
            }
            else
            {
                this.ddlWstation1_SelectedIndexChanged(null, null);
            }

        }
        private void GetDivision()
        {

            string Type = this.Request.QueryString["Type"].ToString();
            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];
            if (Type == "Aggrement")
            {

                var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
                lst1.Add(all);
                var lst2 = lst1.OrderBy(l => l.actcode);
                this.ddlDivision.DataTextField = "actdesc";
                this.ddlDivision.DataValueField = "actcode";
                this.ddlDivision.DataSource = lst2;
                this.ddlDivision.DataBind();

                this.ddlDivision_SelectedIndexChanged(null, null);
            }
            //var lst2 = lst1;
            else
            {
                string wstation1 = this.ddlWstation1.SelectedValue.ToString();//940100000000
                var lst3 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation1.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation1);
                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all1 = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
                lst3.Add(all1);
                this.ddlDivision1.DataTextField = "actdesc";
                this.ddlDivision1.DataValueField = "actcode";
                this.ddlDivision1.DataSource = lst3;
                this.ddlDivision1.DataBind();
                this.ddlDivision1.SelectedValue = "000000000000";

                this.ddlDivision1_SelectedIndexChanged(null, null);
            }


        }

        private void GetDeptList()
        {
            string Type = this.Request.QueryString["Type"].ToString();

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];

            if (Type == "Aggrement")
            {
                string wstation = this.ddlWstation.SelectedValue.ToString();
                string division = this.ddlDivision.SelectedValue.ToString();//940100000000
                List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst1 = new List<BO_ClassManPower.HrSirInf>();

                if (division == "000000000000")
                    lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(9) == "000" && x.actcode.Substring(7) != "00000");

                else
                    lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == division.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode.Substring(7) != "00000");

                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
                lst1.Add(all);
                var lst2 = lst1.OrderBy(l => l.actcode);
                this.ddlDept.DataTextField = "actdesc";
                this.ddlDept.DataValueField = "actcode";
                this.ddlDept.DataSource = lst2;
                this.ddlDept.DataBind();
                this.ddlDept_SelectedIndexChanged(null, null);

            }
            //var lst2 = lst1;
            else
            {
                string wstation1 = this.ddlDivision1.SelectedValue.ToString();//940100000000
                var lst3 = lst.FindAll(x => x.actcode.Substring(0, 7) == wstation1.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode != wstation1);

                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all1 = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Department" };
                lst3.Add(all1);


                this.ddlDept1.DataTextField = "actdesc";
                this.ddlDept1.DataValueField = "actcode";
                this.ddlDept1.DataSource = lst3;
                this.ddlDept1.DataBind();
                this.ddlDept1.SelectedValue = "000000000000";
                this.ddlDept.SelectedValue = "000000000000";

                this.ddlDept1_SelectedIndexChanged(null, null);
            }


        }

        private void GetSectionList()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];

            if (Type == "Aggrement")
            {

                string wstation = this.ddlWstation.SelectedValue.ToString();
                string deptname = this.ddlDept.SelectedValue.ToString();//940100101000
                List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst1 = new List<BO_ClassManPower.HrSirInf>();

                if (deptname == "000000000000")
                    lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(9) != "000");

                else
                    lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == deptname.Substring(0, 9) && x.actcode.Substring(9) != "000");


                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
                lst1.Add(all);
                var lst2 = lst1.OrderBy(l => l.actcode);

                this.ddlSection.DataTextField = "actdesc";
                this.ddlSection.DataValueField = "actcode";
                this.ddlSection.DataSource = lst2;
                this.ddlSection.DataBind();
                this.ddlSection.SelectedValue = "000000000000";
                this.ddlSection_SelectedIndexChanged(null, null);


            }
            else
            {
                string wstation1 = this.ddlDept1.SelectedValue.ToString();//940100000000

                var lst12 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation1.Substring(0, 9) && x.actcode != wstation1);

                SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all1 = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Section" };
                lst12.Add(all1);

                this.ddlSection1.DataTextField = "actdesc";
                this.ddlSection1.DataValueField = "actcode";
                this.ddlSection1.DataSource = lst12;
                this.ddlSection1.DataBind();
                this.ddlSection1.SelectedValue = "000000000000";
            }



        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetAllOrganogramList();
            this.GetDivision();
        }
        protected void ddlWstation1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetAllOrganogramList();
            this.GetDivision();
            this.GetShipList();
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            Session["lstOrganoData"] = lst;
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        protected void ddlDivision1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }
        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {


                case "Aggrement":

                    this.MultiView1.ActiveViewIndex = 0;


                    this.GenInfo();
                    this.GetGrossType();
                    this.GetBldMeReFes();
                    this.GetLeavinfo();
                    this.ShowLocation();



                    break;

                case "Officetime":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;


                case "shifttime":

                    this.lblfrmdate.Visible = true;
                    this.txtfromdate.Visible = true;
                    this.lbltodate.Visible = true;
                    this.txttodate.Visible = true;
                    DateTime curdate = System.DateTime.Today;
                    this.txtfromdate.Text = curdate.ToString("dd-MMM-yyyy");
                    this.txttodate.Text = curdate.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;






            }

        }


        protected void ddlJobLocation_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeOff();
        }
        protected void ibtnFindCompanyAgg_Click(object sender, EventArgs e)
        {

        }

        private void GetShipList()
        {
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();

            string emptype = this.ddlWstation1.SelectedValue.ToString() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETSHIFTINFOR", emptype, "", "", "", "", "", "", "", "");


            this.ddlShift.DataTextField = "shifname";
            this.ddlShift.DataValueField = "shiftid";
            this.ddlShift.DataSource = ds4.Tables[0];
            this.ddlShift.DataBind();
            DataTable dt = ds4.Tables[0];
            Session["tblshiftinfo"] = dt;
            this.ddlShift_SelectedIndexChanged(null, null);



        }

        protected void ibtnNFindEmp_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string qempid = this.Request.QueryString["empid"] ?? "";
            if (qempid.Length > 0)
            {
                string emptype = "94" + qempid.Substring(2, 2) + string.Concat(Enumerable.Repeat("0", 8));
                this.ddlWstation.SelectedValue = emptype;
                this.ddlWstation_SelectedIndexChanged(null, null);

            }

            string txtSProject = (qempid.Length > 0 ? qempid : "%") + "%";
            string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETNEWPNAME", "", txtSProject, joblocation, userid, "", "", "", "", "");
            this.ddlNPEmpName.DataTextField = "empname";
            this.ddlNPEmpName.DataValueField = "empid";
            this.ddlNPEmpName.DataSource = ds5.Tables[0];
            this.ddlNPEmpName.DataBind();

            Session["tblnemp"] = ds5.Tables[0];
        }

        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                if (this.lnkbtnSerOk.Text == "Ok")
                {
                    this.lnkbtnSerOk.Text = "New";


                    if (ddlPEmpName.Items.Count > 0)
                        this.GetComASecSelected();

                    if (this.ddlNPEmpName.Items.Count > 0)
                    {
                        // this.lblPEmpName.Text = this.ddlNPEmpName.SelectedItem.Text.Substring(7);
                        this.ddlNPEmpName.Enabled = false;
                        this.chknewEmp.Checked = true;
                    }
                    else
                    {
                        this.ddlPEmpName.Enabled = false;
                        // this.lblPEmpName.Text = this.ddlPEmpName.SelectedItem.Text.Substring(7);
                    }

                    //if (this.ddlPEmpName.Items.Count == 0)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please Select Employee !!')", true);
                    //    return;
                    //}

                    string ws = ASTUtility.Left(this.ddlWstation.SelectedValue.ToString(), 4);
                    this.ShowImage();
                    //this.chknewEmp_CheckedChanged(null, null);
                    this.lbtnDeletelink.Visible = false;
                    this.ddlWstation.Enabled = false;
                    this.ddlDivision.Enabled = false;
                    this.ddlDept.Enabled = false;
                    this.ddlSection.Enabled = false;

                    //this.ddlPEmpName.Enabled = false; 
                    //this.lblPEmpName.Visible = true;
                    this.pnlGenInfo.Visible = true;
                    this.lblhSalary.Visible = true;

                    this.lbltxtTotalSal.Visible = true;
                    this.lbltotalsal.Visible = true;



                    this.lblhAllowAdd.Visible = false;
                    this.lblhAllowDed.Visible = false;
                    this.lnkbtnFinalSWUpdate.Visible = true;
                    this.chknewEmp.Visible = false;
                    this.chkEdit.Visible = true;
                    this.txtgrossal.Visible = true;
                    this.rbtGross.Visible = false;
                    //this.rbtGross.SelectedIndex =0;   
                    this.rbtholiday.SelectedIndex = 0;
                    // Bank And OverTime

                    switch (comcod)
                    {
                        case "5305":
                            switch (ws)
                            {

                                case "9403":
                                case "9414": // Worker

                                    this.rbtnOverTime.SelectedIndex = 2;
                                    this.ddlBankName1.SelectedValue = "190300010001";
                                    //  rbtPaymentType_SelectedIndexChanged(null, null);
                                    break;

                                default:
                                    this.rbtnOverTime.SelectedIndex = 0;
                                    break;



                            }
                            break;


                        case "5306":
                            switch (ws)
                            {


                                case "9408":
                                case "9416": // Worker
                                    this.rbtnOverTime.SelectedIndex = 2;
                                    this.ddlBankName1.SelectedValue = "190300020001";
                                    //   rbtPaymentType_SelectedIndexChanged(null, null);
                                    break;

                                default:
                                    this.rbtnOverTime.SelectedIndex = 0;
                                    break;



                            }
                            break;


                        default:
                            this.rbtnOverTime.SelectedIndex = 0;
                            break;



                    }
                    rbtPaymentType_SelectedIndexChanged(null, null);
                    this.rbtnOverTime_SelectedIndexChanged(null, null);
                    this.EmpSerRule();
                    this.TSandAllow();

                    this.GenLogInfo();
                    this.pnlImage.Visible = true;


                    string qtpe = this.Request.QueryString["Type"].ToString().Trim();
                    string saledpt = this.ddlDept.SelectedValue.ToString();

                    if (qtpe == "Aggrement" && saledpt == "940200100000")
                    {
                        this.TeamSetup.Visible = true;
                        this.ShowLocation();

                    }


                    if (qtpe == "Aggrement" && ws == "9401")
                    {
                        switch (comcod)
                        {
                            case "5305": //FB
                            case "5306": //FB
                                this.pnlcarsub.Visible = false;
                                this.pnlcarsub2.Visible = false;
                                break;
                            case "5301":
                                this.pnlcarsub.Visible = true;
                                this.pnlcarsub2.Visible = true;
                                txtgrossal.Enabled = false;
                                txtgrossal.ToolTip = "Please Use Gross Salary";

                                break;
                            default:
                                this.pnlcarsub.Visible = true;
                                this.pnlcarsub2.Visible = true;
                                break;


                        }





                    }
                    else
                    {
                        this.pnlcarsub.Visible = false;
                        this.pnlcarsub2.Visible = false;
                    }

                }
                else
                {
                    this.lnkbtnSerOk.Text = "Ok";
                    this.lbtnDeletelink.Visible = true;
                    this.ddlWstation.Enabled = true;
                    this.ddlDept.Enabled = true;
                    this.ddlSection.Enabled = true;
                    this.ddlPEmpName.Enabled = true;
                    this.ddlDivision.Enabled = true;

                    //this.lblPEmpName.Visible = false;
                    this.pnlGenInfo.Visible = false;
                    this.lblhSalary.Visible = false;

                    this.lbltxtTotalSal.Visible = false;
                    this.lbltotalsal.Visible = false;



                    this.lblhAllowAdd.Visible = false;
                    this.lblhAllowDed.Visible = false;
                    this.lnkbtnFinalSWUpdate.Visible = false;
                    this.chknewEmp.Checked = false;
                    this.chknewEmp.Visible = true;
                    this.chkEdit.Checked = false;
                    this.chkEdit.Visible = false;
                    this.lblholidayallowance.Visible = false;
                    this.txtholidayallowance.Visible = false;
                    this.txtgrossal.Visible = false;
                    this.rbtGross.Visible = false;
                    this.pnlPaymenttype.Visible = false;
                    this.pnlPaymenttypeA.Visible = false;
                    this.pnlPaymenttypeB.Visible = false;
                    this.txtgsallary.Text = "0.00";
                    this.txtcar.Text = "0.00";
                    this.txtSuballow.Text = "0.00";
                    this.TeamSetup.Visible = false;
                    this.pnlImage.Visible = false;

                    this.lblEmp.Visible = true;
                    this.ddlPEmpName.Visible = true;


                    this.lblnewEmp.Visible = false;
                    this.ddlNPEmpName.Visible = false;
                    this.ddlNPEmpName.Items.Clear();
                    this.gvSalAdd.DataSource = null;
                    this.gvSalAdd.DataBind();
                    this.gvSalSub.DataSource = null;
                    this.gvSalSub.DataBind();
                    this.gvAllowAdd.DataSource = null;
                    this.gvAllowAdd.DataBind();
                    this.gvAllowSub.DataSource = null;
                    this.gvAllowSub.DataBind();


                    //this.lblPEmpName.Text = "";
                    this.lblDesgination.Text = "";
                    this.lbloffintime.Text = "";
                    this.lbloffouttime.Text = "";
                    this.lbllanintime.Text = "";
                    this.lbllanouttime.Text = "";
                    this.lblEduQua.Text = "";
                    this.lblAtype.Text = "";
                    this.txtholidayallowance.Text = "";
                    this.txtfixedRate.Text = "";
                    this.txthourlyRate.Text = "";
                    this.txtceilingRate1.Text = "";
                    this.txtceilingRate2.Text = "";
                    this.txtceilingRate3.Text = "";
                    this.txtgrossal.Text = "";

                    this.txtAcNo1.Text = "";
                    this.txtAcNo2.Text = "";
                    this.ddlWstation.SelectedValue = "000000000000";
                    this.ddlWstation_SelectedIndexChanged(null, null);


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }

        private void GenLogInfo()
        {
            string comcod = this.GetCompCode();
            string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETAGGLOG", empid, "", "", "", "", "", "", "", "");
            Session["UserLog"] = ds5.Tables[0];

            if (ds5 == null)
                return;
        }

        private void GenInfo()
        {
            string comcod = this.GetCompCode();
            string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETGENINFO", empid, "", "", "", "", "", "", "", "");
            //Session["UserLog"] = ds5.Tables[7];

            if (ds5 == null)
                return;


            this.ddlDesignation.DataTextField = "designame";
            this.ddlDesignation.DataValueField = "desigid";
            this.ddlDesignation.DataSource = ds5.Tables[0];
            this.ddlDesignation.DataBind();
            this.ddlDesignation.SelectedValue = "0399999";

            this.ddlOffintime.DataTextField = "offintime";
            this.ddlOffintime.DataValueField = "offinid";
            this.ddlOffintime.DataSource = ds5.Tables[1];
            this.ddlOffintime.DataBind();

            if (this.GetCompCode() == "5301")
            {
                this.ddlOffintime.SelectedValue = "22007";
            }
            this.ddlOffouttime.DataTextField = "offouttime";
            this.ddlOffouttime.DataValueField = "offoutid";
            this.ddlOffouttime.DataSource = ds5.Tables[2];
            this.ddlOffouttime.DataBind();

            this.ddlLanintime.DataTextField = "lanintime";
            this.ddlLanintime.DataValueField = "laninid";
            this.ddlLanintime.DataSource = ds5.Tables[3];
            this.ddlLanintime.DataBind();

            if (this.GetCompCode() == "5301")
            {
                this.ddlLanintime.SelectedValue = "24003";
            }
            this.ddlLanouttime.DataTextField = "lanouttime";
            this.ddlLanouttime.DataValueField = "lanoutid";
            this.ddlLanouttime.DataSource = ds5.Tables[4];
            this.ddlLanouttime.DataBind();


            this.ddlEduQua.DataTextField = "eduqua";
            this.ddlEduQua.DataValueField = "eduid";
            this.ddlEduQua.DataSource = ds5.Tables[5];
            this.ddlEduQua.DataBind();

            this.ddlAggrement.DataTextField = "agtype";
            this.ddlAggrement.DataValueField = "agtypeid";
            this.ddlAggrement.DataSource = ds5.Tables[6];
            this.ddlAggrement.DataBind();



            this.ddlBankName1.DataTextField = "actdesc";
            this.ddlBankName1.DataValueField = "actcode";
            this.ddlBankName1.DataSource = ds5.Tables[8];
            this.ddlBankName1.DataBind();

            this.ddlBankName2.DataTextField = "actdesc";
            this.ddlBankName2.DataValueField = "actcode";
            this.ddlBankName2.DataSource = ds5.Tables[9];
            this.ddlBankName2.DataBind();
            this.ddlBankName2.SelectedValue = "";
            ds5.Dispose();
        }
        private void GetGrossType()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "5305":
                case "5306":
                    this.rbtGross.SelectedIndex = 5;
                    break;

                default:
                    this.rbtGross.SelectedIndex = 4;
                    this.rbtGross.Items[0].Enabled = false;
                    this.rbtGross.Items[1].Enabled = false;
                    this.rbtGross.Items[2].Enabled = false;
                    this.rbtGross.Items[3].Enabled = false;
                    break;

            }

        }
        private void EmpSerRule()
        {
            Session.Remove("tblData");

            string comcod = this.GetCompCode();
            string projectcode = this.ddlSection.SelectedValue.ToString();
            string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
            DataSet ds6 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEEINFO", projectcode, empid, "", "", "", "", "", "", "");
            if (ds6 == null)
                return;

            DataTable dtcarsub = ds6.Tables[6];

            if (ds6.Tables[0].Rows.Count > 0)
            {
                DataRow[] dr1 = (ds6.Tables[0]).Select("gcod like '03%'");

                if (dr1.Length > 0)
                {
                    this.lblDesgination.Text = dr1[0]["gdesc"].ToString().Trim();
                    this.ddlDesignation.SelectedValue = dr1[0]["gcod"].ToString().Trim();
                }

                //Over Time 
                dr1 = (ds6.Tables[0]).Select("gcod like '07004%'");
                if (dr1.Length > 0)
                {

                    this.rbtnOverTime.SelectedIndex = (dr1[0]["gdatat1"].ToString().Trim() == "0") ? 0 : (dr1[0]["gdatat1"].ToString().Trim() == "1") ? 1
                                                : (dr1[0]["gdatat1"].ToString().Trim() == "2") ? 2 : 3;
                    if (this.rbtnOverTime.SelectedIndex == 2)
                    {
                        double h = Convert.ToDouble(dr1[0]["hrate"].ToString().Trim());
                        this.txtdevided.Text =h==0?"0": Math.Round((((Convert.ToDouble((ds6.Tables[2].Select("gcod='04001'"))[0]["gval"])) * 2) / Convert.ToDouble(dr1[0]["hrate"].ToString().Trim())), 0).ToString();

                    }

                    this.txtfixedRate.Text = Convert.ToDouble(dr1[0]["rate"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.txthourlyRate.Text = Convert.ToDouble(dr1[0]["hrate"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.txtceilingRate1.Text = Convert.ToDouble(dr1[0]["crate1"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.txtceilingRate2.Text = Convert.ToDouble(dr1[0]["crate2"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.txtceilingRate3.Text = Convert.ToDouble(dr1[0]["crate3"].ToString().Trim()).ToString("#,##0;(#,##0); ");

                    this.rbtnOverTime_SelectedIndexChanged(null, null);
                }

                dr1 = (ds6.Tables[0]).Select("gcod like '07005%'");
                if (dr1.Length > 0)
                {
                    this.rbtholiday.SelectedIndex = (dr1[0]["gdatat1"].ToString().Trim() == "0") ? 0 : (dr1[0]["gdatat1"].ToString().Trim() == "1") ? 1 : 2;
                    this.txtholidayallowance.Text = Convert.ToDouble(dr1[0]["rate"].ToString().Trim()).ToString("#,##0;(#,##0); ");
                    this.rbtholiday_SelectedIndexChanged(null, null);

                }



                dr1 = (ds6.Tables[0]).Select("gcod like '11%'");

                if (dr1.Length > 0)
                {
                    this.lblEduQua.Text = dr1[0]["gdesc"].ToString().Trim();
                    this.ddlEduQua.SelectedValue = dr1[0]["gcod"].ToString().Trim();
                    this.txtEduPass.Text = dr1[0]["gdatat2"].ToString().Trim();
                }

                dr1 = (ds6.Tables[0]).Select("gcod like '12%'");
                if (dr1.Length > 0)
                {
                    this.lblAtype.Text = dr1[0]["gdesc"].ToString().Trim();
                    this.ddlAggrement.SelectedValue = dr1[0]["gcod"].ToString().Trim();


                }

            }

            //Shift setup
            if (ds6.Tables[1].Rows.Count > 0)
            {
                this.lbloffintime.Text = Convert.ToDateTime(ds6.Tables[1].Rows[0]["gdesc"]).ToString("hh:mm tt");
                this.lbloffouttime.Text = Convert.ToDateTime(ds6.Tables[1].Rows[1]["gdesc"]).ToString("hh:mm tt");


                string offintime = this.ddlOffintime.SelectedValue.ToString();
                string offouttime = this.ddlOffouttime.SelectedValue.ToString();
                string lanintime = this.ddlLanintime.SelectedValue.ToString();
                string lanouttime = this.ddlLanouttime.SelectedValue.ToString();

                this.ddlOffintime.SelectedValue = (ds6.Tables[8].Rows[0]["offincode"].ToString().Trim().Length == 0) ? offintime : ds6.Tables[1].Rows[0]["gcod"].ToString().Trim();
                this.ddlOffouttime.SelectedValue = (ds6.Tables[8].Rows[0]["offoutcode"].ToString().Trim().Length == 0) ? offouttime : ds6.Tables[1].Rows[1]["gcod"].ToString().Trim();
                this.ddlLanintime.SelectedValue = (ds6.Tables[8].Rows[0]["lnincode"].ToString().Trim().Length == 0) ? lanintime : ds6.Tables[1].Rows[2]["gcod"].ToString().Trim();
                this.ddlLanouttime.SelectedValue = (ds6.Tables[8].Rows[0]["lnoutcod"].ToString().Trim().Length == 0) ? lanouttime : ds6.Tables[1].Rows[3]["gcod"].ToString().Trim();


            }
            else
            {


                string offintime = this.ddlOffintime.SelectedValue.ToString();
                string offouttime = this.ddlOffouttime.SelectedValue.ToString();
                string lanintime = this.ddlLanintime.SelectedValue.ToString();
                string lanouttime = this.ddlLanouttime.SelectedValue.ToString();

                this.ddlOffintime.SelectedValue = (ds6.Tables[8].Rows.Count == 0 || ds6.Tables[8].Rows[0]["offincode"].ToString().Trim().Length == 0) ? offintime : ds6.Tables[8].Rows[0]["offincode"].ToString().Trim();
                this.ddlOffouttime.SelectedValue = (ds6.Tables[8].Rows.Count == 0 || ds6.Tables[8].Rows[0]["offoutcode"].ToString().Trim().Length == 0) ? offouttime : ds6.Tables[8].Rows[0]["offoutcode"].ToString().Trim();
                this.ddlLanintime.SelectedValue = (ds6.Tables[8].Rows.Count == 0 || ds6.Tables[8].Rows[0]["lnincode"].ToString().Trim().Length == 0) ? lanintime : ds6.Tables[8].Rows[0]["lnincode"].ToString().Trim();
                this.ddlLanouttime.SelectedValue = (ds6.Tables[8].Rows.Count == 0 || ds6.Tables[8].Rows[0]["lnoutcod"].ToString().Trim().Length == 0) ? lanouttime : ds6.Tables[8].Rows[0]["lnoutcod"].ToString().Trim();


            }

            //Salary & Deduction
            if (ds6.Tables[2].Rows.Count > 0)
            {
                Session["tblData"] = ds6.Tables[2];
                this.ShowSalAllow();

            }
            if (ds6.Tables[0].Rows.Count == 0 && ds6.Tables[1].Rows.Count == 0)
            {

                this.rbtholiday.SelectedIndex = 0;
                this.ddlDesignation_SelectedIndexChanged(null, null);
                this.ddlOffintime_SelectedIndexChanged(null, null);
                this.ddlOffouttime_SelectedIndexChanged(null, null);
                this.ddlLanintime_SelectedIndexChanged(null, null);
                this.ddlLanouttime_SelectedIndexChanged(null, null);
                this.ddlEduQua_SelectedIndexChanged(null, null);
                this.ddlProQua_SelectedIndexChanged(null, null);


            }
            if (ds6.Tables[3].Rows.Count > 0)
            {
                string Bankname = ds6.Tables[3].Rows[0]["gdatat"].ToString();
                if (Bankname.Length != 0)
                {
                    this.pnlPaymenttype.Visible = true;
                    this.pnlPaymenttypeA.Visible = true;
                    this.pnlPaymenttypeB.Visible = true;
                    this.rbtPaymentType.SelectedIndex = 1;
                    this.ddlBankName1.SelectedValue = (ds6.Tables[3].Rows[0]["gdatat"]).ToString();
                    this.txtAcNo1.Text = (ds6.Tables[3].Rows[0]["acno"]).ToString();
                    this.txtCashAmt.Text = Convert.ToDouble(ds6.Tables[3].Rows[0]["cashamt"]).ToString("#,##0;(#,##0);");
                }
                //Bank 02 
                string Bankname2 = ds6.Tables[3].Rows[0]["bankcode"].ToString();
                if (Bankname2.Trim().Length != 0)
                {
                    this.pnlPaymenttype.Visible = true;
                    this.pnlPaymenttypeA.Visible = true;
                    this.pnlPaymenttypeB.Visible = true;
                    this.rbtPaymentType.SelectedIndex = 1;
                    this.ddlBankName2.SelectedValue = (ds6.Tables[3].Rows[0]["bankcode"]).ToString().Trim();
                    this.txtAcNo2.Text = (ds6.Tables[3].Rows[0]["acno2"]).ToString();
                }
                else
                {
                    //(Default None)
                    this.ddlBankName2.SelectedItem.Text = "NONE";
                }
            }

            //PF Start Date
            if (ds6.Tables[4].Rows.Count > 0)
            {

                this.txtPf.Text = (Convert.ToDateTime(ds6.Tables[4].Rows[0]["pfdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds6.Tables[4].Rows[0]["pfdate"]).ToString("dd-MMM-yyyy");

            }
            else
            {
                this.txtPf.Text = Convert.ToDateTime(ds6.Tables[7].Rows[0]["confdate"]).ToString("dd-MMM-yyyy");
            }

            if (ds6.Tables[5].Rows.Count > 0)
            {
                this.txtpfend.Text = (Convert.ToDateTime(ds6.Tables[5].Rows[0]["pfenddat"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds6.Tables[5].Rows[0]["pfenddat"]).ToString("dd-MMM-yyyy");
            }



            //}
            if ((ASTUtility.Left(projectcode, 4) == "9401"))
            {
                if (dtcarsub.Rows.Count > 0)
                {

                    this.txtcar.Text = (Convert.ToDouble(dtcarsub.Rows[0]["caramt"]).ToString() == "0.00") ? "0.00" : Convert.ToDouble(dtcarsub.Rows[0]["caramt"]).ToString("#,##0;(#,##0);");
                    this.txtSuballow.Text = (Convert.ToDouble(dtcarsub.Rows[0]["subamt"]).ToString() == "0.00") ? "0.00" : Convert.ToDouble(dtcarsub.Rows[0]["subamt"]).ToString("#,##0;(#,##0);");



                    double caramt = Convert.ToDouble("0" + this.txtcar.Text.Trim().ToString());
                    double sumamt = Convert.ToDouble("0" + this.txtSuballow.Text.Trim().ToString());
                    double tgrosamt = Convert.ToDouble("0" + this.txtgrossal.Text.Trim());

                    double incramt = Convert.ToDouble("0" + this.txtgrossal.Text.Trim());
                    double finalamt = caramt + sumamt + incramt;
                    this.txtgsallary.Text = finalamt.ToString();
                }
            }





        }
        private void ShowSalAllow()
        {
            Session.Remove("tblsaladd");
            Session.Remove("tblsalsub");
            Session.Remove("tblallowadd");
            Session.Remove("tblallowsub");
            DataTable dtr = (DataTable)Session["tblData"];
            DataView dvr = new DataView();
            DataTable dtr1 = new DataTable();

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '040%'");
            dtr1 = dvr.ToTable();
            Session["tblsaladd"] = dtr1;
            this.gvSalAdd.DataSource = dtr1;
            this.gvSalAdd.DataBind();
            this.FooterCalculation(dtr1, "gvSalAdd");

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '041%'");
            dtr1 = dvr.ToTable();
            Session["tblsalsub"] = dtr1;
            this.gvSalSub.DataSource = dtr1;
            this.gvSalSub.DataBind();
            this.FooterCalculation(dtr1, "gvSalSub");

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '070%'");
            dtr1 = dvr.ToTable();
            Session["tblallowadd"] = dtr1;
            this.gvAllowAdd.DataSource = dtr1;
            this.gvAllowAdd.DataBind();
            this.FooterCalculation(dtr1, "gvAllowAdd");

            dtr1 = dtr;
            dvr = dtr1.DefaultView;
            dvr.RowFilter = ("gcod like '071%'");
            dtr1 = dvr.ToTable();
            Session["tblallowsub"] = dtr1;
            this.gvAllowSub.DataSource = dtr1;
            this.gvAllowSub.DataBind();
            this.FooterCalculation(dtr1, "gvAllowSub");




        }
        private void TSandAllow()
        {
            double SalAdd = 0, SallSub = 0;
            DataTable dtsaladd = (DataTable)Session["tblsaladd"];
            DataTable dtsalsub = (DataTable)Session["tblsalsub"];
            DataTable dtweagadd = (DataTable)Session["tblallowadd"];
            DataTable dtweafsub = (DataTable)Session["tblallowsub"];
            SalAdd = Convert.ToDouble((Convert.IsDBNull(dtsaladd.Compute("sum(gval)", "")) ? 0.00 : dtsaladd.Compute("sum(gval)", "")));
            SallSub = Convert.ToDouble((Convert.IsDBNull(dtsalsub.Compute("sum(gval)", "")) ? 0.00 : dtsalsub.Compute("sum(gval)", "")));
            this.lbltotalsal.Text = (SalAdd - SallSub).ToString("#,##0;(#,##0); ");
            //this.txtgrossal.Text = (Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["percnt"]) > 0) ?(((Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 100) /Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["percnt"]))).ToString("#,##0; (##0); ") : "";


        }

        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataTable dt1 = dt.Copy();
            string deptcode = this.ddlDept.SelectedValue.ToString().Substring(0, 4);
            switch (GvName)
            {


                case "gvSalAdd":

                    double toaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                    ((Label)this.gvSalAdd.FooterRow.FindControl("lgvFSalAdd")).Text = toaddamt.ToString("#,##0;(#,##0); ");

                    string comcod = this.GetCompCode();
                    DataView dv = dt1.DefaultView;
                    double topaddamt = 0.00;
                    switch (comcod)
                    {

                        case "5301":

                            switch (deptcode)
                            {

                                case "9403":
                                    this.txtgrossal.Text = toaddamt.ToString("#,##0;(#,##0); ").Trim();
                                    break;

                                default:
                                    dv.RowFilter = ("percnt>0");
                                    dt1 = dv.ToTable();
                                    topaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                                    this.txtgrossal.Text = topaddamt.ToString("#,##0;(#,##0); ").Trim();

                                    break;


                            }

                            break;



                        case "5305"://FB
                        case "5306"://FB

                            switch (deptcode)
                            {
                                //Salary Worker
                                case "9403":
                                case "9404":
                                case "9405":
                                case "9406":
                                case "9407":
                                case "9408":
                                case "9409":
                                case "9410":
                                case "9413": // Worker
                                case "9414": // Worker
                                case "9415": // Worker
                                case "9416": // Worker
                                    this.txtgrossal.Text = toaddamt.ToString("#,##0;(#,##0); ").Trim();
                                    break;

                                //Salary Executive
                                default:
                                    dv.RowFilter = ("percnt>0");
                                    dt1 = dv.ToTable();
                                    topaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                                    this.txtgrossal.Text = topaddamt.ToString("#,##0;(#,##0); ").Trim();

                                    break;


                            }

                            break;


                        default:
                            dv.RowFilter = ("percnt>0");
                            dt1 = dv.ToTable();
                            topaddamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(gval)", "")) ? 0 : dt1.Compute("sum(gval)", "")));
                            this.txtgrossal.Text = topaddamt.ToString("#,##0;(#,##0); ").Trim();
                            break;
                    }









                    break;


                case "gvSalSub":
                    ((Label)this.gvSalSub.FooterRow.FindControl("lgvFSalSub")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gval)", "")) ?
                             0 : dt.Compute("sum(gval)", ""))).ToString("#,##0;(#,##0); ");

                    break;

            }




        }

        protected void ddlPEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComASecSelected();
        }
        private void GetComASecSelected()
        {

            string empid = this.ddlPEmpName.SelectedValue.ToString().Trim();
            DataTable dt = ((DataTable)Session["tblemp"]).Copy();
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0 && this.chknewEmp.Checked == false)
            {
                this.ddlWstation.SelectedValue = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();

                DataView dv;
                dv = dt.DefaultView;
                dv.RowFilter = ("empid='" + empid + "'");

                string wstation = this.ddlWstation.SelectedValue.ToString();

                //Division
                List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)Session["lstOrganoData"];
                string division = dr[0]["divcode"].ToString();
                string deptname = dr[0]["deptcode"].ToString();
                string section = dr[0]["refno"].ToString();
                var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);

                this.ddlDivision.DataTextField = "actdesc";
                this.ddlDivision.DataValueField = "actcode";
                this.ddlDivision.DataSource = lst1;
                this.ddlDivision.DataBind();
                this.ddlDivision.SelectedValue = division;


                //Department
                lst1 = lst.FindAll(x => x.actcode.Substring(0, 7) == division.Substring(0, 7) && x.actcode.Substring(9) == "000" && x.actcode.Substring(7) != "00000");
                this.ddlDept.DataTextField = "actdesc";
                this.ddlDept.DataValueField = "actcode";
                this.ddlDept.DataSource = lst1;
                this.ddlDept.DataBind();
                this.ddlDept.SelectedValue = deptname;


                //Section 
                lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == deptname.Substring(0, 9) && x.actcode.Substring(9) != "000");
                this.ddlSection.DataTextField = "actdesc";
                this.ddlSection.DataValueField = "actcode";
                this.ddlSection.DataSource = lst1;
                this.ddlSection.DataBind();
                this.ddlSection.SelectedValue = section;

            }


        }
        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblDesgination.Text = this.ddlDesignation.SelectedItem.Text;
        }
        protected void ddlOffintime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbloffintime.Text = this.ddlOffintime.SelectedItem.Text;
        }
        protected void ddlOffouttime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbloffouttime.Text = this.ddlOffouttime.SelectedItem.Text;

        }
        protected void ddlLanintime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbllanintime.Text = this.ddlLanintime.SelectedItem.Text;

        }
        protected void ddlLanouttime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbllanouttime.Text = this.ddlLanouttime.SelectedItem.Text;
        }
        protected void ddlEduQua_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblEduQua.Text = (this.ddlEduQua.Items.Count == 0) ? "" : this.ddlEduQua.SelectedItem.Text;
        }
        protected void ddlProQua_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblAtype.Text = this.ddlAggrement.SelectedItem.Text;
        }
        protected void lbtnTSalAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsaladd"];

            for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
            {
                double txtsal = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                double percnt = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                dt.Rows[i]["percnt"] = percnt;
                dt.Rows[i]["gval"] = txtsal;

            }
            Session["tblsaladd"] = dt;
            this.FooterCalculation(dt, "gvSalAdd");
            this.TSandAllow();
        }
        protected void lbtnTSalSub_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsalsub"];

            for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
            {
                double txtsal = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                double percnt = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());

                //  txtgvgpersub
                dt.Rows[i]["percnt"] = percnt;
                dt.Rows[i]["gval"] = txtsal;
            }
            Session["tblsalsub"] = dt;
            this.FooterCalculation(dt, "gvSalSub");
            this.TSandAllow();

        }
        protected void lbtnTAllowAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblallowadd"];

            for (int i = 0; i < this.gvAllowAdd.Rows.Count; i++)
            {

                double txtrate = Convert.ToDouble("0" + ((TextBox)this.gvAllowAdd.Rows[i].FindControl("txtgvAllowAdd")).Text.Trim());
                dt.Rows[i]["rate"] = txtrate;
            }
            Session["tblallowadd"] = dt;
            this.gvAllowAdd.DataSource = dt;
            this.gvAllowAdd.DataBind();
            //this.FooterCalculation(dt, "gvAllowAdd");
            //this.TSandAllow();
        }
        protected void lbtnTAllowSub_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblallowsub"];

            for (int i = 0; i < this.gvAllowSub.Rows.Count; i++)
            {

                double txtallrate = Convert.ToDouble("0" + ((TextBox)this.gvAllowSub.Rows[i].FindControl("txtgvAllowSub")).Text.Trim());
                dt.Rows[i]["rate"] = txtallrate;
            }


            Session["tblallowsub"] = dt;
            this.gvAllowSub.DataSource = dt;
            this.gvAllowSub.DataBind();

        }
        protected void lnkbtnFinalSWUpdate_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataTable dtuser = (DataTable)Session["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
            //string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");
            //string tblEdittrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editrmid"].ToString();

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (dtuser.Rows.Count == 0) ? "" : userid;
            string Editdat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string Editrmid = (dtuser.Rows.Count == 0) ? "" : Terminal;

            //-------------------------////

            string projectcode = this.ddlSection.SelectedValue.ToString();
            string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
            string desigid = this.ddlDesignation.SelectedValue.ToString();
            if (desigid == "0399999")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Designation!');", true);
                return;
            }
            if (this.txtgrossal.Text.Trim().ToString() == "" || this.txtgrossal.Text.Trim().ToString() == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Salary must be inserted!');", true);
                return;
            }
            if (projectcode == "000000000000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Section!');", true);
                return;
            }
            string designame = this.ddlDesignation.SelectedItem.Text;
            string offinid = this.ddlOffintime.SelectedValue.ToString();
            string offintime = this.ddlOffintime.SelectedItem.Text;
            string offoutid = this.ddlOffouttime.SelectedValue.ToString();
            string offouttime = this.ddlOffouttime.SelectedItem.Text;
            string laninid = this.ddlLanintime.SelectedValue.ToString();
            string lanintime = this.ddlLanintime.SelectedItem.Text;
            string lanoutid = this.ddlLanouttime.SelectedValue.ToString();
            string lanouttime = this.ddlLanouttime.SelectedItem.Text;
            string eduid = (this.ddlEduQua.Items.Count == 0) ? "" : this.ddlEduQua.SelectedValue.ToString();
            string education = (this.ddlEduQua.Items.Count == 0) ? "" : this.ddlEduQua.SelectedItem.Text;
            string agtypeid = this.ddlAggrement.SelectedValue.ToString();
            string agtype = this.ddlAggrement.SelectedItem.Text;
            string paytype = this.rbtPaymentType.SelectedIndex.ToString().Trim();
            string bankname1 = this.ddlBankName1.SelectedValue.ToString().Trim();
            string bankname2 = this.ddlBankName2.SelectedValue.ToString().Trim();
            string bankacno1 = this.txtAcNo1.Text;
            string bankacno2 = this.txtAcNo2.Text;
            string type = (paytype == "0") ? "" : bankname1;
            string acno1 = (paytype == "0") ? "" : bankacno1;
            string bank2 = (paytype == "0") ? "" : bankname2;
            string acno2 = (paytype == "0") ? "" : bankacno2;
            string cashamt = Convert.ToDouble("0" + this.txtCashAmt.Text.Trim()).ToString();
            string txtedupass = "Passing Year";
            string edupass = this.txtEduPass.Text.Trim();
            string holidaytype = (this.rbtholiday.SelectedIndex).ToString();
            string overtimetype = (this.rbtnOverTime.SelectedIndex).ToString();

            string carallow = Convert.ToDouble("0" + this.txtcar.Text.Trim()).ToString();
            string suballow = Convert.ToDouble("0" + this.txtSuballow.Text.Trim()).ToString();


            //---------------------------for bank account duplicate Check------------------------------>
            //DataSet  ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "CHECKDUPLICATEBANKACC", acno1, "", "", "", "", "", "", "", "");
            //  if (ds.Tables[0].Rows.Count > 0)
            //  {
            //      return;
            //  }

            string pfdate = (this.txtPf.Text.Trim() == "") ? "01-Jan-1900" : Convert.ToDateTime(txtPf.Text.Trim()).ToString("dd-MMM-yyyy");
            string pfenddat = (txtpfend.Text.Trim() == "") ? "01-Jan-1900" : Convert.ToDateTime(txtpfend.Text.Trim()).ToString("dd-MMM-yyyy");
            string wlstattion = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            if (wlstattion == "9403" && pfdate == "01-Jan-1900")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Provide PF Starting Date');", true);
                return;
            }

            bool result;

            /////////-------------------Log----------------------///////

            result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTUPDATEAGG", empid, PostedByid, PostSession, Posttrmid, Posteddat,
                    EditByid, Editdat, Editrmid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
                return;


            //////---------------------------------------------////////////




            result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMPIDANDREFNOADESIG", empid, "94%", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);
                return;
            }


            //Update Holiday & Casual(10), Sick(14)
            result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTHOLIDAYALEAVEDAY", empid, "", "", "", "", "", "", "", "",
             "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }


            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, desigid, "T", designame, projectcode, "", "", "", "",
                "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", "", "", userid, Posttrmid);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }


            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, offinid, "D", "01-Jan-1900 " + offintime, projectcode,
                "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", "", "", userid, Posttrmid);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);
                return;
            }
            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, offoutid, "D", "01-Jan-1900 " + offouttime, projectcode,
                "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", "", "", userid, Posttrmid);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }
            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, laninid, "D", "01-Jan-1900 " + lanintime, projectcode,
                "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", "", "", userid, Posttrmid);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }
            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, lanoutid, "D", "01-Jan-1900 " + lanouttime, projectcode,
                "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", "", "", userid, Posttrmid);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }
            if (comcod != "4330")

                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, eduid, "T", education, projectcode, txtedupass,
                    edupass, "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", "", "", userid, Posttrmid);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }
            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, agtypeid, "T", agtype, projectcode, "", "", "", "",
                "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", "", "", userid, Posttrmid);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }

            // Bank COde
            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "19001", "T", type, projectcode, "", "", "", "", "0",
                "", "0", "0", "0", "0", "0", "0", acno1, bank2, acno2, "0", "0", "1", "", "", "", cashamt, "", "", userid, Posttrmid);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }


            // PF Start Date
            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "27001", "D", pfdate, projectcode, "", "", "", "", "0",
                "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", "", "", userid, Posttrmid);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }

            //PF END DATE
            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "27002", "D", pfenddat, projectcode, "", "", "", "", "0",
                "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", "", "", userid, Posttrmid);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }



            //Car and Subsitance Allow
            result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTCARSUBALLOWEMP", empid, carallow, suballow);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

                return;
            }





            DataTable dtsaladd = (DataTable)Session["tblsaladd"];
            DataTable dtsalsub = (DataTable)Session["tblsalsub"];
            DataTable dtallowadd = (DataTable)Session["tblallowadd"];
            DataTable dtallowsub = (DataTable)Session["tblallowsub"];
            string holidayrate = (this.rbtholiday.SelectedIndex == 0) ? "0" : (this.rbtholiday.SelectedIndex == 2) ? Convert.ToDouble("0" + this.txtholidayallowance.Text.Trim()).ToString() : (Math.Round((Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) / 31), 0)).ToString();

            string fixedrate = Convert.ToDouble("0" + this.txtfixedRate.Text.Trim()).ToString();

            double dhourlyrate = Convert.ToDouble("0" + this.txtdevided.Text.Trim()) > 0 ? Math.Round(((Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"])) * 2) / Convert.ToDouble("0" + this.txtdevided.Text.Trim()), 2) : 0;

            string hourlyrate = (this.rbtnOverTime.SelectedIndex == 2) ? Math.Round(dhourlyrate, 2).ToString() : (this.rbtnOverTime.SelectedIndex == 1) ? Convert.ToDouble("0" + this.txthourlyRate.Text.Trim()).ToString() : "0";
            string ceilingrate1 = Convert.ToDouble("0" + this.txtceilingRate1.Text.Trim()).ToString();
            string ceilingrate2 = Convert.ToDouble("0" + this.txtceilingRate2.Text.Trim()).ToString();
            string ceilingrate3 = Convert.ToDouble("0" + this.txtceilingRate3.Text.Trim()).ToString();


            int i;
            string gcode, gtype, gval, percnt, unit, qty, rate; ;

            for (i = 0; i < dtsaladd.Rows.Count; i++)
            {
                gcode = dtsaladd.Rows[i]["gcod"].ToString();
                gtype = dtsaladd.Rows[i]["gtype"].ToString();
                gval = dtsaladd.Rows[i]["gval"].ToString();
                percnt = dtsaladd.Rows[i]["percnt"].ToString();
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, gcode, gtype, gval, projectcode, "", "", "", "",
                    percnt, "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "", "", "", userid, Posttrmid);
            }

            for (i = 0; i < dtsalsub.Rows.Count; i++)
            {
                gcode = dtsalsub.Rows[i]["gcod"].ToString();
                gtype = dtsalsub.Rows[i]["gtype"].ToString();
                gval = dtsalsub.Rows[i]["gval"].ToString();
                percnt = dtsalsub.Rows[i]["percnt"].ToString();
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, gcode, gtype, gval, projectcode, "", "", "", "",
                    percnt, "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "", "", "", userid, Posttrmid);
            }
            for (i = 0; i < dtallowadd.Rows.Count; i++)
            {
                gcode = dtallowadd.Rows[i]["gcod"].ToString();
                gtype = dtallowadd.Rows[i]["gtype"].ToString();
                gval = dtallowadd.Rows[i]["gval"].ToString();
                percnt = dtallowadd.Rows[i]["percnt"].ToString();
                unit = dtallowadd.Rows[i]["unit"].ToString();
                qty = dtallowadd.Rows[i]["qty"].ToString();
                rate = dtallowadd.Rows[i]["rate"].ToString();

                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, gcode, gtype, gval, projectcode, "", "", "", "",
                    percnt, unit, qty, rate, "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "", "", "", userid, Posttrmid);
            }

            // Overtime
            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "07004", "N", "0", projectcode, overtimetype, "", "", "", "0", "", "0", fixedrate,
           hourlyrate, ceilingrate1, ceilingrate2, ceilingrate3, "", "", "", "0", "0", "1", "", "", "", "", "", "", userid, Posttrmid);

            //holiday rate
            result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "07005", "N", "0", projectcode, holidaytype, "", "", "", "0", "", "0", holidayrate
                , "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "", "", "", userid, Posttrmid);



            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Is Not Updated');", true);

                return;
            }



            for (i = 0; i < dtallowsub.Rows.Count; i++)
            {
                gcode = dtallowsub.Rows[i]["gcod"].ToString();
                gtype = dtallowsub.Rows[i]["gtype"].ToString();
                gval = dtallowsub.Rows[i]["gval"].ToString();
                percnt = dtallowsub.Rows[i]["percnt"].ToString();
                unit = dtallowsub.Rows[i]["unit"].ToString();
                qty = dtallowsub.Rows[i]["qty"].ToString();
                rate = dtallowsub.Rows[i]["rate"].ToString();
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, gcode, gtype, gval, projectcode, "", "", "", "",
                    percnt, unit, qty, rate, "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "", "", "", userid, Posttrmid);
            }


            //this is sales team setup location update (nahid)


            for (int k = 0; k < this.gvLocation.Rows.Count; k++)
            {
                string Circle = ((DropDownList)this.gvLocation.Rows[k].FindControl("ddlcircle")).SelectedValue.ToString();
                string gtypea = ((Label)this.gvLocation.Rows[k].FindControl("lgvgval")).Text.Trim();
                string Region = ((DropDownList)this.gvLocation.Rows[k].FindControl("ddlregion")).SelectedValue.ToString().Trim();
                string Area = ((DropDownList)this.gvLocation.Rows[k].FindControl("ddlarea")).SelectedValue.ToString();
                string Territory = ((DropDownList)this.gvLocation.Rows[k].FindControl("ddlterritory")).SelectedValue.ToString();
                string poseccode = ((DropDownList)this.gvLocation.Rows[k].FindControl("ddlposeccode")).SelectedValue.ToString() == "00000" ? "" : ((DropDownList)this.gvLocation.Rows[k].FindControl("ddlposeccode")).SelectedValue.ToString();
                string Seq = ((Label)this.gvLocation.Rows[k].FindControl("lgvseq")).Text.Trim();

                // if (Gcode != "99999" && Group != "999999999")
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, "32001", gtypea, Circle, "", Region, Area, Territory,
                    "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", Seq, "0", "0", poseccode, "0", "", "", userid, Posttrmid);
                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);


                }


            }


            this.lbtnUpdateImg_Click();


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Agreement Updated Successfully');", true);

            string empname = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedItem.Text.Substring(7) : this.ddlPEmpName.SelectedItem.Text.Substring(7);

            Common.LogStatus("Employee Agreement", "Department: " + this.ddlDept.SelectedItem.Text + " Section: " + this.ddlSection.SelectedItem.Text, this.ddlPEmpName.SelectedValue, empname);
        }
        protected void chknewEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chknewEmp.Checked)
            {
                this.lblEmp.Visible = false;
                this.ddlPEmpName.Visible = false;

                this.ibtnNFindEmp_Click(null, null);
                this.lblnewEmp.Visible = true;
                this.ddlNPEmpName.Visible = true;
                this.ddlNPEmpName.Enabled = true;


            }
            else
            {

                this.lblEmp.Visible = true;
                this.ddlPEmpName.Visible = true;
                this.lblnewEmp.Visible = false;
                this.ddlNPEmpName.Visible = false;
                this.ddlPEmpName.Enabled = false;
            }


        }

        protected void lbtnCalculation_Click(object sender, EventArgs e)
        {
            DataTable dtsaladd = (DataTable)Session["tblsaladd"];
            DataTable dtsalsub = (DataTable)Session["tblsalsub"];
            double basic, gross, percent;
            string code;
            string deptcode = this.ddlDept.SelectedValue.ToString().Substring(0, 4);
            gross = Convert.ToDouble("0" + this.txtgrossal.Text.Trim());

            //Gross Salary Sanmar  62.50
            basic = Convert.ToDouble("0");
            if (this.rbtGross.SelectedIndex == 0)
            {
                for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                    percent = (code == "04001") ? 62.50 : (code == "04002") ? 40.00 : (code == "04003") ? 10.00 : (code == "04004") ? 10.00 : 0.00;

                    if (i == 0)
                    {
                        dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                        dtsaladd.Rows[i]["percnt"] = percent;
                        continue;
                    }

                    dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 0.01), 0)
                                                : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                    dtsaladd.Rows[i]["percnt"] = percent;
                }


                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.Trim();
                    percent = (code == "04101") ? 5.00 : 0.00;
                    dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * 0.01 * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"])), 0)
                            : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                    dtsalsub.Rows[i]["percnt"] = percent;
                }
            }
            //sanmar 55.55
            else if (this.rbtGross.SelectedIndex == 1)
            {
                for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                    percent = (code == "04001") ? 55.55 : (code == "04002") ? 40.00 : (code == "04003") ? 20.00 : (code == "04004") ? 20.00 : 0.00;

                    if (i == 0)
                    {
                        dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                        dtsaladd.Rows[i]["percnt"] = percent;
                        continue;
                    }

                    dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 0.01), 0)
                            : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                    dtsaladd.Rows[i]["percnt"] = percent;
                }


                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    code = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.Trim();
                    percent = (code == "04101") ? 5.00 : 0.00;
                    dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * 0.01 * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"])), 0)
                            : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                    dtsalsub.Rows[i]["percnt"] = percent;
                }


            }
            else if (this.rbtGross.SelectedIndex == 2)
            {

                for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                {
                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                    dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());

                    //dtsaladd.Rows[i]["gval"] = Math.Round((gross * 0.01 * percent), 0);
                    dtsaladd.Rows[i]["percnt"] = percent;

                }


                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                    dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim()); ;
                    //  dtsalsub.Rows[i]["gval"] = Math.Round((percent * 0.01 * gross), 0);
                    dtsalsub.Rows[i]["percnt"] = percent;

                }



            }
            //Edison Properties
            else if (this.rbtGross.SelectedIndex == 4)
            {


                if (deptcode == "9401")
                {
                    double caramt = Math.Round(Convert.ToDouble("0" + this.txtcar.Text.Trim().ToString()), 0);
                    double sumamt = Math.Round(Convert.ToDouble("0" + this.txtSuballow.Text.Trim().ToString()), 0);
                    double tgrosamt = Math.Round(Convert.ToDouble("0" + this.txtgsallary.Text.Trim()), 0);

                    gross = tgrosamt - caramt - sumamt;

                    double incramt = gross;//Convert.ToDouble("0" + this.txtgrossal.Text.Trim());
                    double finalamt = caramt + sumamt + incramt;
                    this.txtgrossal.Text = gross.ToString();
                    this.txtgsallary.Text = finalamt.ToString();
                }
                else
                {
                    gross = Convert.ToDouble("0" + this.txtgrossal.Text.Trim());

                }


                switch (deptcode)
                {
                    case "9404": // STAFF

                        for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                        {
                            code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                            percent = (code == "04001") ? 60 : (code == "04002") ? 30 : (code == "04003") ? 6 : (code == "04004") ? 4 : 0.00;
                            dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                            dtsaladd.Rows[i]["percnt"] = percent;

                        }


                        for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                        {
                            code = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.Trim();
                            percent = (code == "04101") ? 8.00 : 0.00;
                            dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * 0.01 * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"])), 0)
                                    : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                            dtsalsub.Rows[i]["percnt"] = percent;

                        }

                        break;


                    case "9403": // Worker

                        // Medical Allowance,Conveyance,

                        (dtsaladd.Select("gcod='04003'"))[0]["gval"] = 600;
                        double convey = Convert.ToDouble((dtsaladd.Select("gcod='04003'"))[0]["gval"]); ;
                        (dtsaladd.Select("gcod='04004'"))[0]["gval"] = 350;
                        double medical = Convert.ToDouble((dtsaladd.Select("gcod='04004'"))[0]["gval"]);
                        (dtsaladd.Select("gcod='04020'"))[0]["gval"] = 900;
                        double foodalw = Convert.ToDouble((dtsaladd.Select("gcod='04020'"))[0]["gval"]);


                        for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                        {
                            code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                            percent = (code == "04002") ? 50 : 0.00;

                            if (i == 0)
                            {
                                dtsaladd.Rows[i]["gval"] = Math.Round((gross - convey - medical - foodalw) / 1.5, 0);
                                dtsaladd.Rows[i]["percnt"] = percent;
                                continue;
                            }

                            dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * ((gross - convey - medical - foodalw) / 1.5) * 0.01), 0)
                                    : Convert.ToDouble(dtsaladd.Rows[i]["gval"]);

                            //dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"]) * 0.01), 0)
                            //        : Convert.ToDouble(dtsaladd.Rows[i]["gval"]);
                            dtsaladd.Rows[i]["percnt"] = percent;
                        }
                        for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                        {
                            code = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.Trim();
                            percent = (code == "04101") ? 8.00 : 0.00;
                            dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * 0.01 * ((gross - convey - medical - foodalw) / 1.5)), 0)
                                    : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                            dtsalsub.Rows[i]["percnt"] = percent;

                        }

                        break;



                    default:


                        for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                        {
                            string gcode = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                            percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                            dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                            dtsaladd.Rows[i]["percnt"] = percent;
                            if (gcode == "04001")
                            {
                                basic = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                            }


                        }


                        for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                        {
                            percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                            //dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim()); ;
                            dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((basic * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                            //  dtsalsub.Rows[i]["gval"] = Math.Round((percent * 0.01 * gross), 0);
                            dtsalsub.Rows[i]["percnt"] = percent;

                        }

                        break;



                }




            }

            //FB 
            else if (this.rbtGross.SelectedIndex == 5)
            {

                switch (deptcode)
                {

                    case "9403": // Worker
                    case "9404": // Worker
                    case "9405": // Worker
                    case "9406": // Worker
                    case "9407": // Worker
                    case "9408": // Worker
                    case "9409": // Worker
                    case "9410": // Worker
                    case "9413": // NON OT
                    case "9414": // NON OT
                    case "9415": // OT Based
                    case "9416": // OT Based
                        (dtsaladd.Select("gcod='04003'"))[0]["gval"] = 600;
                        double medical = Convert.ToDouble((dtsaladd.Select("gcod='04003'"))[0]["gval"]); ;
                        (dtsaladd.Select("gcod='04004'"))[0]["gval"] = 350;
                        double convey = Convert.ToDouble((dtsaladd.Select("gcod='04004'"))[0]["gval"]);
                        (dtsaladd.Select("gcod='04020'"))[0]["gval"] = 900;
                        double foodalw = Convert.ToDouble((dtsaladd.Select("gcod='04020'"))[0]["gval"]);
                        for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                        {
                            code = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                            percent = (code == "04002") ? 50 : 0.00;

                            if (i == 0)
                            {
                                dtsaladd.Rows[i]["gval"] = Math.Round((gross - medical - convey - foodalw) / 1.5, 0);
                                dtsaladd.Rows[i]["percnt"] = percent;
                                continue;
                            }

                            dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * ((gross - medical - convey - foodalw) / 1.5) * 0.01), 0)
                                    : Convert.ToDouble(dtsaladd.Rows[i]["gval"]);
                            dtsaladd.Rows[i]["percnt"] = percent;
                        }
                        for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                        {
                            code = ((Label)this.gvSalSub.Rows[i].FindControl("lblgvItmCodesalsub")).Text.Trim();
                            percent = (code == "04101") ? 7.5 : 0.00;

                            dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((percent * 0.01 * ((gross - medical - convey - foodalw) / 1.5)), 0)
                                    : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                            dtsalsub.Rows[i]["percnt"] = percent;

                        }

                        break;


                        //Executive,Grade-01,S.Staff
                        default:
                        for (int i = 0; i < this.gvSalAdd.Rows.Count; i++)
                        {
                            string gcode = ((Label)this.gvSalAdd.Rows[i].FindControl("lblgvItmCodesaladd")).Text.Trim();
                            percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                            dtsaladd.Rows[i]["gval"] = percent > 0 ? Math.Round((gross * 0.01 * percent), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvSaladd")).Text.Trim());
                            dtsaladd.Rows[i]["percnt"] = percent;

                        }
                        for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                        {

                            percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                            dtsalsub.Rows[i]["gval"] = percent > 0 ? Math.Round((0.01 * percent * Convert.ToDouble((dtsaladd.Select("gcod='04001'"))[0]["gval"])), 0) : Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvSalSub")).Text.Trim());
                            dtsalsub.Rows[i]["percnt"] = percent;

                        }
                        break;

                }

            }

            //Default
            else
            {
                basic = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[0].FindControl("txtgvSaladd")).Text.Trim());

                for (int i = 1; i < this.gvSalAdd.Rows.Count; i++)
                {
                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalAdd.Rows[i].FindControl("txtgvgperadd")).Text.Trim());
                    dtsaladd.Rows[i]["gval"] = Math.Round((percent * basic * 0.01), 0);
                    dtsaladd.Rows[i]["percnt"] = percent;

                }


                for (int i = 0; i < this.gvSalSub.Rows.Count; i++)
                {
                    percent = Convert.ToDouble("0" + ((TextBox)this.gvSalSub.Rows[i].FindControl("txtgvgpersub")).Text.Trim());
                    dtsalsub.Rows[i]["gval"] = Math.Round((percent * 0.01 * basic), 0);
                    dtsalsub.Rows[i]["percnt"] = percent;


                }


            }

            //////////////////////////////
            Session["tblsaladd"] = dtsaladd;
            Session["tblsalsub"] = dtsalsub;
            this.gvSalAdd.DataSource = dtsaladd;
            this.gvSalAdd.DataBind();
            this.gvSalSub.DataSource = dtsalsub;
            this.gvSalSub.DataBind();
            this.FooterCalculation(dtsaladd, "gvSalAdd");
            this.FooterCalculation(dtsalsub, "gvSalSub");
            this.TSandAllow();

        }
        protected void lbtnDeletelink_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string Section = this.ddlSection.SelectedValue.ToString();
            if (this.ddlPEmpName.Items.Count == 0)
                return;
            string EmpName = this.ddlPEmpName.SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMPIDANDREFNO", EmpName, Section, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Unlink Failed');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Unlink Successfully');", true);
            this.ddlSection_SelectedIndexChanged(null, null);
        }
        protected void rbtholiday_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtholidayallowance.Visible = (this.rbtholiday.SelectedIndex == 2);
            this.lblholidayallowance.Visible = (this.rbtholiday.SelectedIndex == 2);
        }

        protected void rbtnOverTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblfiexedRate.Visible = (this.rbtnOverTime.SelectedIndex == 0);
            this.txtfixedRate.Visible = (this.rbtnOverTime.SelectedIndex == 0);
            this.lblhourlyRate.Visible = (this.rbtnOverTime.SelectedIndex == 1);
            this.txthourlyRate.Visible = (this.rbtnOverTime.SelectedIndex == 1);
            this.PnlMultiply.Visible = (this.rbtnOverTime.SelectedIndex == 2);

            //this.txtMultiply.Visible = (this.rbtnOverTime.SelectedIndex == 2);
            this.txtdevided.Visible = (this.rbtnOverTime.SelectedIndex == 2);
            this.lblCeilingRate1.Visible = (this.rbtnOverTime.SelectedIndex == 3);
            this.lblCeilingRate2.Visible = (this.rbtnOverTime.SelectedIndex == 3);
            this.lblCeilingRate3.Visible = (this.rbtnOverTime.SelectedIndex == 3);
            this.txtceilingRate1.Visible = (this.rbtnOverTime.SelectedIndex == 3);
            this.txtceilingRate2.Visible = (this.rbtnOverTime.SelectedIndex == 3);
            this.txtceilingRate3.Visible = (this.rbtnOverTime.SelectedIndex == 3);

            if (this.rbtnOverTime.SelectedIndex == 0)
            {
                this.txthourlyRate.Text = "";
                this.txtceilingRate1.Text = "";
                this.txtceilingRate2.Text = "";
                this.txtceilingRate3.Text = "";

            }





            else if (this.rbtnOverTime.SelectedIndex == 1 || this.rbtnOverTime.SelectedIndex == 2)
            {
                this.txtfixedRate.Text = "";
                this.txtceilingRate1.Text = "";
                this.txtceilingRate2.Text = "";
                this.txtceilingRate3.Text = "";

            }

            else
            {
                this.txtfixedRate.Text = "";
                this.txthourlyRate.Text = "";


            }

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            if (this.lnkbtnShow.Text == "Ok")
            {
                this.lnkbtnShow.Text = "New";
                //this.lblDeptDesc.Text = this.ddlSection1.SelectedItem.Text.Trim(); 
                this.ddlWstation1.Enabled = false;
                this.ddlDivision1.Enabled = false;
                this.ddlDept1.Enabled = false;
                this.ddlSection1.Enabled = false;
                // this.ddlSection1.Visible = true; 
                this.pnlOfftime.Visible = true;

                string qtype = this.Request.QueryString["Type"].ToString();


                this.GetEmployeeOff();


                if (qtype == "shifttime")
                {
                    this.GetShipList();

                    this.pnlShift.Visible = true;
                }

                return;
            }

            this.lnkbtnShow.Text = "Ok";
            this.ddlWstation1.Enabled = true;
            this.ddlDivision1.Enabled = true;
            this.ddlSection1.Enabled = true;
            this.ddlDept1.Enabled = true;
            //this.ddlSection1.Visible = false;
            this.txtgrossal.Text = "0.00";
            this.txtcar.Text = "0.00";
            this.txtSuballow.Text = "0.00";
            this.pnlOfftime.Visible = false;
            this.chkEdit.Checked = false;

        }

        private void ShowOffTime()
        {

            string comcod = this.GetCompCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETOFFTIME", "", "", "", "", "", "", "", "", "");
            if (ds5 == null)
                return;

            this.ddlOffintimedw.DataTextField = "offintime";
            this.ddlOffintimedw.DataValueField = "offinid";
            this.ddlOffintimedw.DataSource = ds5.Tables[0];
            this.ddlOffintimedw.DataBind();



            this.ddlOffouttimedw.DataTextField = "offouttime";
            this.ddlOffouttimedw.DataValueField = "offoutid";
            this.ddlOffouttimedw.DataSource = ds5.Tables[1];
            this.ddlOffouttimedw.DataBind();

            this.ddlLanintimedw.DataTextField = "lanintime";
            this.ddlLanintimedw.DataValueField = "laninid";
            this.ddlLanintimedw.DataSource = ds5.Tables[2];
            this.ddlLanintimedw.DataBind();

            this.ddlLanouttimedw.DataTextField = "lanouttime";
            this.ddlLanouttimedw.DataValueField = "lanoutid";
            this.ddlLanouttimedw.DataSource = ds5.Tables[3];
            this.ddlLanouttimedw.DataBind();
            ds5.Dispose();


        }
        protected void lnkbtnUpdateOfftime_Click(object sender, EventArgs e)
        {




            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {



                case "Officetime":
                    this.UpdateOffTime();

                    break;


                case "shifttime":
                    this.UpdateShiftTime();

                    break;






            }





        }
        class EmpidList
        {

            public string empid { get; set; }
            public EmpidList() { }
            public EmpidList(string empid)
            {
                this.empid = empid;
            }


        }

        private void UpdateOffTime()
        {

            string comcod = this.GetCompCode();
            string company = this.ddlWstation1.SelectedValue.ToString().Substring(0, 4) + "%";

            string div = (this.ddlDivision1.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDivision1.SelectedValue.ToString().Substring(0, 7)) + "%";
            string Dept1 = (this.ddlDept1.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept1.SelectedValue.ToString().Substring(0, 9)) + "%";
            string projectcode = (this.ddlSection1.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection1.SelectedValue.ToString()) + "%";

            string offinid = this.ddlOffintimedw.SelectedValue.ToString();
            string offintime = this.ddlOffintimedw.SelectedItem.Text;
            string offoutid = this.ddlOffouttimedw.SelectedValue.ToString();
            string offouttime = this.ddlOffouttimedw.SelectedItem.Text;
            string laninid = this.ddlLanintimedw.SelectedValue.ToString();
            string lanintime = this.ddlLanintimedw.SelectedItem.Text;
            string lanoutid = this.ddlLanouttimedw.SelectedValue.ToString();
            string lanouttime = this.ddlLanouttimedw.SelectedItem.Text;


            string empid = "";
            List<EmpidList> lst = new List<EmpidList>();

            foreach (ListItem item in DropCheck1.Items)
            {
                if (item.Selected)
                {
                    //empid += item.Value;
                    lst.Add(new EmpidList(item.Value));
                }
            }

            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = ASITUtility03.ListToDataTable(lst);

            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";



            //string[] aempid = this.DropCheck1.Text.Trim().Split(',');

            //if (aempid[0].Substring(0, 12) == "000000000000")
            //    empid = "";
            //else
            //    foreach (string aempid1 in aempid)
            //        empid = empid + aempid1.Substring(0, 12);


            bool result;

            result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMPOFFTIME", ds1, null, null, projectcode, company, Dept1, div);
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Is Not Updated');", true);

                return;
            }

            result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", ds1, null, null, projectcode, offinid, "01-Jan-1900 " + offintime, company, Dept1, div, "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Is Not Updated');", true);
                return;
            }
            result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", ds1, null, null, projectcode, offoutid, "01-Jan-1900 " + offouttime, company, Dept1, div, "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Is Not Updated');", true);
                return;
            }
            result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", ds1, null, null, projectcode, laninid, "01-Jan-1900 " + lanintime, company, Dept1, div, "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Is Not Updated');", true);
                return;
            }
            result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", ds1, null, null, projectcode, lanoutid, "01-Jan-1900 " + lanouttime, company, Dept1, div, "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Is Not Updated');", true);
                return;
            }



            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);



        }

        private void UpdateShiftTime()
        {


            DateTime frmdate, todate, offintime, today, addday, offouttime, lanintime, lanouttime, systime;

            string dayid;
            frmdate = Convert.ToDateTime(this.txtfromdate.Text);
            todate = Convert.ToDateTime(this.txttodate.Text);

            string comcod = this.GetCompCode();
            string empid = "";
            DataTable dt = new DataTable();
            //dt.clear();
            dt.Columns.Add("empid");
            //string[] aempid = this.DropCheck1.Text.Trim().Split(',');

            //if (aempid[0].Substring(0, 12) == "000000000000")
            //    empid = "";
            //else
            //    foreach (string aempid1 in aempid)
            //        empid = empid + aempid1.Substring(0, 12);




            foreach (ListItem item in DropCheck1.Items)
            {
                if (item.Selected)
                {
                    //  empid += item.Value;
                    DataRow _newrow = dt.NewRow();
                    _newrow["empid"] = item.Value;

                    dt.Rows.Add(_newrow);
                }
            }

            DataSet ds = new DataSet("ds1");
            ds.Tables.Add(dt);
            ds.Tables[0].TableName = "tblempid";

            while (frmdate <= todate)
            {

                dayid = frmdate.ToString("yyyyMMdd");
                string ws = ((this.ddlWstation1.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation1.SelectedValue.ToString().Substring(0, 4)) + "%";
                string divison = ((this.ddlDivision1.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision1.SelectedValue.ToString().Substring(0, 7) + "%");
                string Dept1 = (this.ddlDept1.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDept1.SelectedValue.ToString().Substring(0, 9)) + "%";
                string sec = (this.ddlSection1.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection1.SelectedValue.ToString()) + "%";

                string shiftid = this.ddlShift.SelectedValue.ToString();
                string abstime = this.Hiddabstime.Value;
                string latemarg = this.Hiddlatemarg.Value;
                string macstart = this.hiddmacstarttime.Value;




                offintime = Convert.ToDateTime(frmdate.ToString("dd-MMM-yyyy") + " " + this.ddlOffintimedw.SelectedItem.Text);
                systime = Convert.ToDateTime(frmdate.ToString("dd-MMM-yyyy") + " " + "08:00 PM");
                today = frmdate;
                addday = today.AddDays(1);
                offouttime = (systime <= offintime) ? Convert.ToDateTime(addday.ToString("dd-MMM-yyyy") + " " + this.ddlOffouttimedw.SelectedItem.Text) : Convert.ToDateTime(today.ToString("dd-MMM-yyyy") + " " + this.ddlOffouttimedw.SelectedItem.Text);
                lanintime = (systime <= offintime) ? Convert.ToDateTime(addday.ToString("dd-MMM-yyyy") + " " + this.ddlLanintimedw.SelectedItem.Text) : Convert.ToDateTime(today.ToString("dd-MMM-yyyy") + " " + this.ddlLanintimedw.SelectedItem.Text);
                lanouttime = (systime <= offintime) ? Convert.ToDateTime(addday.ToString("dd-MMM-yyyy") + " " + this.ddlLanouttimedw.SelectedItem.Text) : Convert.ToDateTime(today.ToString("dd-MMM-yyyy") + " " + this.ddlLanouttimedw.SelectedItem.Text);
                frmdate = frmdate.AddDays(1);

                bool result;

                result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATESHIFTTIME", ds, null, null, dayid, ws, Dept1, sec, empid, offintime.ToString(),
                    offouttime.ToString(), lanintime.ToString(), lanouttime.ToString(), divison, shiftid, macstart, latemarg, abstime, "", "", "", "", "");
                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data Is Not Updated');", true);
                    return;
                }
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {

        }

        protected void ddlSection1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void rbtAgreementType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void rbtPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rbtPaymentType.SelectedIndex == 1)
            {
                this.pnlPaymenttype.Visible = true;
                this.pnlPaymenttypeA.Visible = true;
                this.pnlPaymenttypeB.Visible = true;

            }
            else
            {
                this.pnlPaymenttype.Visible = false;

                this.pnlPaymenttypeA.Visible = false;
                this.pnlPaymenttypeB.Visible = false;
            }
        }
        protected void lnkNextbtn_Click(object sender, EventArgs e)
        {
            //bool tf = (this.chknewEmp.Checked == true);
            //string empid = (this.chknewEmp.Checked == true) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
            string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('ImgUpload.aspx?Type=Entry&empid=" + empid + "', target='_self');</script>";

        }
        protected void chkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEdit.Checked)
            {

                this.ddlWstation.Enabled = true;
                this.ddlDivision.Enabled = true;
                this.ddlDept.Enabled = true;
                this.ddlSection.Enabled = true;
                this.ddlPEmpName.Enabled = true;

            }

            else
            {


                this.ddlWstation.Enabled = false;
                this.ddlDivision.Enabled = false;
                this.ddlSection.Enabled = false;
                this.ddlPEmpName.Enabled = false;
                this.ddlDept.Enabled = false;

            }
        }
        protected void lbtnsrchEmployeeoff_Click(object sender, EventArgs e)
        {
            this.GetEmployeeOff();
        }



        private void GetEmployeeOff()
        {
            string comcod = this.GetCompCode();
            string company = (this.ddlWstation1.SelectedValue == "000000000000" ? "94" : this.ddlWstation1.SelectedValue.ToString().Substring(0, 4)) + "%";
            string dept = (this.ddlDept1.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept1.SelectedValue.ToString().Substring(0, 9)) + "%";
            string secname = (this.ddlSection1.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection1.SelectedValue.ToString()) + "%";
            string txtempsrch = "%" + this.txtsrchempoff.Text.Trim() + "%";
            string jobLocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEENAMEOT", company, dept, secname, txtempsrch, jobLocation, "", "", "", "");

            this.DropCheck1.DataTextField = "empname";
            this.DropCheck1.DataValueField = "empid";
            this.DropCheck1.DataSource = ds3.Tables[0];
            this.DropCheck1.DataBind();


        }

        /// <summary>
        /// Sales Tema Location Setup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void GetBldMeReFes()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            Session.Remove("tblbmrf");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETBLDMEREFES", userid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblbmrf"] = ds2.Tables[0];



        }
        private void ShowLocation()
        {
            string comcod = this.GetCompCode();
            string empid = this.ddlPEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOCATION", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvLocation.DataSource = null;
                this.gvLocation.DataBind();
                return;
            }

            Session["tblempLoc"] = ds3.Tables[0];
            this.Bind_Location();
            // DataTable dt = ds3.Tables[0];

            // DataSet ds1 = (DataSet)Session["tblacadeg"];

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string slno1 = "";
            DataTable dt = (DataTable)Session["tblempLoc"];
            if (dt.Rows.Count == 0)
            {
                slno1 = "001";
            }
            else
            {
                int slno = Convert.ToInt16(dt.Rows[dt.Rows.Count - 1]["seq"]) + 1;//
                slno1 = slno.ToString();// ASTUtility.Right(("000" + slno), 3).ToString();
            }

            DataRow row = dt.NewRow();
            row["comcod"] = comcod;
            row["gcod"] = "32001";
            row["seq"] = slno1;
            row["gval"] = "T";
            row["circle"] = "";
            row["region"] = "";
            row["area"] = "";
            row["territory"] = "";
            dt.Rows.Add(row);
            Session["tblempLoc"] = dt;
            this.Bind_Location();
        }
        private void Bind_Location()
        {
            DataTable dt = (DataTable)Session["tblempLoc"];
            this.gvLocation.DataSource = dt;
            this.gvLocation.DataBind();
            DataTable dt1 = (DataTable)Session["tblbmrf"];
            DataView dv1, dv;
            DropDownList ddlcircle, ddlregion, ddlarea, ddlterritory;
            DataTable dt3;
            DataRow dr1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Circle
                string circle = dt.Rows[i]["circle"].ToString();
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("gcod like '41%'");
                dt3 = dv1.ToTable();


                dr1 = dt3.NewRow();
                dr1["gcod"] = "00000";
                dr1["gdesc"] = "NONE";
                dt3.Rows.Add(dr1);
                dv = dt3.DefaultView;
                dv.Sort = "gcod";

                ddlcircle = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlcircle"));
                ddlcircle.DataTextField = "gdesc";
                ddlcircle.DataValueField = "gcod";
                ddlcircle.DataSource = dv.ToTable();
                ddlcircle.DataBind();
                ddlcircle.SelectedValue = circle;

                //region
                string region = dt.Rows[i]["region"].ToString();
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("gcod like '43%'");
                dt3 = dv1.ToTable();

                dr1 = dt3.NewRow();
                dr1["gcod"] = "00000";
                dr1["gdesc"] = "NONE";
                dt3.Rows.Add(dr1);
                dv = dt3.DefaultView;
                dv.Sort = "gcod";

                ddlregion = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlregion"));
                ddlregion.DataTextField = "gdesc";
                ddlregion.DataValueField = "gcod";
                ddlregion.DataSource = dv.ToTable();
                ddlregion.DataBind();
                ddlregion.SelectedValue = region;
                //ddlarea
                string area = dt.Rows[i]["area"].ToString();
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("gcod like '45%'");
                dt3 = dv1.ToTable();

                dr1 = dt3.NewRow();
                dr1["gcod"] = "00000";
                dr1["gdesc"] = "NONE";
                dt3.Rows.Add(dr1);
                dv = dt3.DefaultView;
                dv.Sort = "gcod";

                ddlarea = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlarea"));
                ddlarea.DataTextField = "gdesc";
                ddlarea.DataValueField = "gcod";
                ddlarea.DataSource = dv.ToTable();
                ddlarea.DataBind();
                ddlarea.SelectedValue = area;

                //ddlterritory
                string territory = dt.Rows[i]["territory"].ToString();
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("gcod like '46%'");
                dt3 = dv1.ToTable();

                dr1 = dt3.NewRow();
                dr1["gcod"] = "00000";
                dr1["gdesc"] = "NONE";
                dt3.Rows.Add(dr1);
                dv = dt3.DefaultView;
                dv.Sort = "gcod";

                ddlterritory = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlterritory"));
                ddlterritory.DataTextField = "gdesc";
                ddlterritory.DataValueField = "gcod";
                ddlterritory.DataSource = dv.ToTable();
                ddlterritory.DataBind();
                ddlterritory.SelectedValue = territory;

                //ddlEmployee Catagory
                string poseccode = dt.Rows[i]["poseccode"].ToString();
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("gcod like '50%'");
                dt3 = dv1.ToTable();

                dr1 = dt3.NewRow();
                dr1["gcod"] = "00000";
                dr1["gdesc"] = "NONE";
                dt3.Rows.Add(dr1);
                dv = dt3.DefaultView;
                dv.Sort = "gcod";

                ddlterritory = ((DropDownList)this.gvLocation.Rows[i].FindControl("ddlposeccode"));
                ddlterritory.DataTextField = "gdesc";
                ddlterritory.DataValueField = "gcod";
                ddlterritory.DataSource = dv.ToTable();
                ddlterritory.DataBind();
                ddlterritory.SelectedValue = poseccode;


                //Session["tblempLoc"] = dt.ToTable();
                //tblempLoc
            }

        }

        protected void gvLocation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblempLoc"];
            string comcod = this.GetCompCode();

            string empid = this.ddlPEmpName.SelectedValue.ToString();
            string seq = ((Label)this.gvLocation.Rows[e.RowIndex].FindControl("lgvseq")).Text.ToString();



            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLOCATIONDELETE", empid, "32001", seq, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                int rowindex = (this.gvLocation.PageSize) * (this.gvLocation.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();

            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblempLoc");
            Session["tblempLoc"] = dv.ToTable();

            this.Bind_Location();

        }

        //////
        //// Get Leave Information (nahid)
        //////
        private void GetLeavinfo()
        {
            string comcod = this.GetCompCode();
            string yearid = System.DateTime.Today.ToString("yyyy");

            string empid = (this.ddlNPEmpName.Items.Count > 0) ? this.ddlNPEmpName.SelectedValue.ToString() : this.ddlPEmpName.SelectedValue.ToString();
            DataSet ds6 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETLEAVEIFNOINDV", empid, yearid, "", "", "", "", "", "", "");
            if (ds6 == null || ds6.Tables[0].Rows.Count == 0)
            {
                this.txternleave.Text = "0";
                this.txtcsleave.Text = "0";
                this.txtskleave.Text = "0";
                return;
            }
            else
            {
                this.txternleave.Text = ds6.Tables[0].Rows[0]["leave"].ToString();
                this.txtcsleave.Text = ds6.Tables[0].Rows[1]["leave"].ToString();
                this.txtskleave.Text = ds6.Tables[0].Rows[2]["leave"].ToString();
            }
        }

        private void lbtnUpdateImg_Click()
        {
            try
            {
                ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");
                string comcod = this.GetCompCode();
                string empid = "";
                string idcard = "";
                if (this.chknewEmp.Checked == true)
                {
                    DataTable dt = (DataTable)Session["tblnemp"];
                    if (dt.Rows.Count > 0)
                        empid = this.ddlNPEmpName.SelectedValue.ToString();
                    idcard = dt.Select("empid='" + empid + "'")[0]["idcardno"].ToString();
                }
                else
                {
                    DataTable dt1 = (DataTable)Session["tblemp"];
                    if (dt1.Rows.Count > 0)
                        empid = this.ddlPEmpName.SelectedValue.ToString();
                    idcard = dt1.Select("empid='" + empid + "'")[0]["idcardno"].ToString();
                }

                string filePath = "";
                string filePath2 = "";
                string msg = "";
                string fileExtention = "";
                int fileLenght = 0;


                //check image selected or not
                if ((imgFileUpload.PostedFile != null) && (imgFileUpload.PostedFile.ContentLength > 0) || (imgSigFileUpload.PostedFile != null) && (imgSigFileUpload.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName).ToString() ?? "";
                    string fn2 = System.IO.Path.GetFileName(imgSigFileUpload.PostedFile.FileName).ToString() ?? "";
                    //check image
                    if ((imgFileUpload.PostedFile != null) && (imgFileUpload.PostedFile.ContentLength > 0) && (fn != null || fn != ""))
                    {
                        Guid uid = Guid.NewGuid();
                        fileExtention = "jpg";
                        //fileExtention = imgFileUpload.PostedFile.ContentType;
                        fileLenght = imgFileUpload.PostedFile.ContentLength;
                        fn = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName).ToString() ?? "";
                        switch (comcod)
                        {
                            case "5305"://FB
                                filePath = "~/Upload/HRM/EmpImgFB/" + idcard + ".jpg";
                                break;

                            case "5306"://Footbed
                                filePath = "~/Upload/HRM/EmpImgFBF/" + idcard + ".jpg";
                                break;

                            default:
                                filePath = "~/Upload/HRM/EmpImg/" + idcard + ".jpg";
                                break;
                        }
                        //filePath = "~/Upload/HRM/EmpImg/" + idcard + "." + fileExtention.ToString().Remove(0, 6);
                        if (fileExtention == "jpg")
                        {
                            if (fileLenght <= 5048576)
                            {
                                DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");

                                if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                                {
                                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgFileUpload.PostedFile.InputStream);
                                    System.Drawing.Image objImage = ScaleImage(bmpPostedImage);
                                    //Convert to JPG
                                    //System.Drawing.Image imgJpg = ImagetoJPG(objImage);
                                    objImage.Save(Server.MapPath(filePath));



                                }
                                else
                                {
                                    DataTable dt2 = ds2.Tables[0];
                                    string file1 = dt2.Rows[0]["imgurl"].ToString();
                                    if (fn2 == null || fn2 == "")
                                    {
                                        filePath2 = dt2.Rows[0]["signurl"].ToString();
                                    }

                                    FileInfo getFile = new FileInfo(Server.MapPath(file1));

                                    if (getFile.Exists)
                                    {
                                        getFile.Delete();
                                    }

                                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgFileUpload.PostedFile.InputStream);
                                    System.Drawing.Image objImage = ScaleImage(bmpPostedImage);
                                    // Saving image in jpeg format
                                    objImage.Save(Server.MapPath(filePath));

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Image must be less than 5MB!');", true);
                            }
                        }

                    }

                    //check signature
                    if ((imgSigFileUpload.PostedFile != null) && (imgSigFileUpload.PostedFile.ContentLength > 0) && (fn2 != null || fn2 != ""))
                    {
                        Guid uid = Guid.NewGuid();
                        fileExtention = "jpg";
                        //fileExtention = imgSigFileUpload.PostedFile.ContentType;
                        switch (comcod)
                        {
                            case "5305"://FB
                                filePath = "~/Upload/HRM/EmpSignFB/" + idcard + ".jpg";
                                break;

                            case "5306"://Footbed
                                filePath = "~/Upload/HRM/EmpSignFBF/" + idcard + ".jpg";
                                break;

                            default:
                                filePath = "~/Upload/HRM/EmpSign/" + idcard + ".jpg";
                                break;
                        }
                        //filePath2 = "~/Upload/HRM/EmpSign/" + idcard + "." + fileExtention.ToString().Remove(0, 6);
                        fileLenght = imgSigFileUpload.PostedFile.ContentLength;
                        if (fileExtention == "jpg")
                        {
                            if (fileLenght <= 1048576)
                            {
                                DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");

                                if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                                {
                                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgSigFileUpload.PostedFile.InputStream);
                                    System.Drawing.Image objImage1 = ScaleImage2(bmpPostedImage);
                                    // Saving sign in jpeg format
                                    objImage1.Save(Server.MapPath(filePath2));


                                }
                                else
                                {
                                    DataTable dt2 = ds2.Tables[0];
                                    string file1 = dt2.Rows[0]["signurl"].ToString();
                                    if (fn == null || fn == "")
                                    {
                                        filePath = dt2.Rows[0]["imgurl"].ToString();
                                    }

                                    FileInfo getFile = new FileInfo(Server.MapPath(file1));
                                    if (getFile.Exists)
                                    {
                                        getFile.Delete();
                                    }

                                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgSigFileUpload.PostedFile.InputStream);
                                    System.Drawing.Image objImage1 = ScaleImage2(bmpPostedImage);
                                    // Saving sign in jpeg format
                                    objImage1.Save(Server.MapPath(filePath2));

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Signature must be less than 1MB!');", true);
                            }
                        }

                    }

                    bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "INSERTUPDATEIMAGENEW", empid, "", "", filePath, filePath2, "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Error: " + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Agreement Updated Successfully');", true);

                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Agreement Updated Successfully but no Image selected!!!');", true);
                //}
                #region Previous ImageUpload
                //if (imgFileUpload.HasFile)
                //{
                //    Upload = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName);
                //    string filepath = Server.MapPath("~") + "\\Upload\\HRM\\EmpImg" + "\\" + Upload;
                //    imgFileUpload.PostedFile.SaveAs(filepath);
                //    EmpImg.ImageUrl = "~/Upload/HRM/EmpImg/" + Upload;
                //    image_file = imgFileUpload.PostedFile.InputStream;
                //    size = imgFileUpload.PostedFile.ContentLength;
                //    Session["i"] = image_file;
                //    Session["s"] = size;

                //}
                //if (imgSigFileUpload.HasFile)
                //{
                //    Upload = System.IO.Path.GetFileName(imgSigFileUpload.PostedFile.FileName);
                //    string filepath = Server.MapPath("~") + "\\Upload\\HRM\\EmpSign" + "\\" + Upload;
                //    imgSigFileUpload.PostedFile.SaveAs(filepath);
                //    EmpSig.ImageUrl = "~/Upload/HRM/EmpSign/" + Upload;
                //    image_file = imgSigFileUpload.PostedFile.InputStream;
                //    size = imgSigFileUpload.PostedFile.ContentLength;
                //    Session["i1"] = image_file;
                //    Session["s1"] = size;
                //}

                //string comcod = this.GetCompCode();
                //string savelocation = Server.MapPath("~") + "\\Upload\\HRM\\EmpImg";
                //string[] filePaths = Directory.GetFiles(savelocation);
                //foreach (string filePath in filePaths)
                //    File.Delete(filePath);

                //string empid = this.ddlPEmpName.SelectedValue.ToString();
                ////long[] photo = new long[0];
                ////long[] signature = new long[0];

                //byte[] photo = new byte[0];
                //byte[] signature = new byte[0];


                //image_file = (Stream)Session["i"];
                //size = Convert.ToInt32(Session["s"]);

                //BinaryReader br = new BinaryReader(image_file);
                //photo = br.ReadBytes(size);


                ////Signature
                //if (Session["i1"] != null)
                //{
                //    image_file = (Stream)Session["i1"];
                //    size = Convert.ToInt32(Session["s1"]);
                //    BinaryReader br1 = new BinaryReader(image_file);
                //    signature = br1.ReadBytes(size);
                //}

                //ProcessAccess HRData = new ProcessAccess();
                //DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "EMPID", empid, "", "", "", "", "", "", "", "");
                //bool updatPhoto;
                //if (ds3.Tables[0].Rows.Count == 0)
                //{
                //    updatPhoto = HRData.InsertClientPhoto(comcod, empid, photo, signature);
                //    //bool updatPhoto = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "INSERTUPDATEIMAGE", empid, photo.ToString(), signature.ToString(), "", "", "", "", "", "", "", "", "", "", "", "");
                //    if (updatPhoto)
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);



                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Image Upload Failed');", true);


                //    }
                //}

                //else
                //{
                //    if (photo.Length > 0 && signature.Length > 0)
                //    {
                //        updatPhoto = HRData.UpdateClientPhoto(comcod, empid, photo, signature);
                //    }


                //    else if (photo.Length > 0)
                //    {
                //        updatPhoto = HRData.UpdateClientOnlyPhoto(comcod, empid, photo);
                //    }


                //    else if (signature.Length > 0)
                //    {
                //        updatPhoto = HRData.UpdateClientOnlySign(comcod, empid, signature);
                //    }
                //}
                #endregion
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }

        }
        private System.Drawing.Image ImagetoJPG(System.Drawing.Image img)
        {
            // Get a bitmap.
            Bitmap bmp1 = new Bitmap(img);
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder,50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"c:\TestPhotoQualityFifty.jpg", jpgEncoder, myEncoderParameters);

            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"c:\TestPhotoQualityHundred.jpg", jpgEncoder, myEncoderParameters);

            // Save the bitmap as a JPG file with zero quality level compression.
            myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"c:\TestPhotoQualityZero.jpg", jpgEncoder, myEncoderParameters);

            return img;

        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        public static System.Drawing.Image ScaleImage(System.Drawing.Image image)
        {
            //var ratio = (double)maxHeight / image.Height;
            var newWidth = 300;
            var newHeight = 300;
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
        public static System.Drawing.Image ScaleImage2(System.Drawing.Image image)
        {

            var newWidth = 300;
            var newHeight = 80;
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
        private void ShowImage()
        {
            string comcod = this.GetCompCode();
            string empid = "";
            string idcard = "";
            if (this.chknewEmp.Checked == true)
            {
                DataTable dt = (DataTable)Session["tblnemp"];
                if (dt.Rows.Count > 0)
                    empid = this.ddlNPEmpName.SelectedValue.ToString();
                idcard = dt.Select("empid='" + empid + "'")[0]["idcardno"].ToString();
            }
            else
            {
                DataTable dt1 = (DataTable)Session["tblemp"];
                if (dt1.Rows.Count > 0)
                    empid = this.ddlPEmpName.SelectedValue.ToString();
                idcard = dt1.Select("empid='" + empid + "'")[0]["idcardno"].ToString();
            }

            string empImg = "";
            string empSign = "";
            switch (comcod)
            {
                case "5305"://FB
                    empImg = "~/Upload/HRM/EmpImgFB/" + idcard + ".jpg";
                    empSign = "~/Upload/HRM/EmpSignFB/" + idcard + ".jpg";
                    break;

                case "5306"://Footbed
                    empImg = "~/Upload/HRM/EmpImgFBF/" + idcard + ".jpg";
                    empSign = "~/Upload/HRM/EmpSignFBF/" + idcard + ".jpg";
                    break;

                default:
                    empImg = "~/Upload/HRM/EmpImg/" + idcard + ".jpg";
                    empSign = "~/Upload/HRM/EmpSign/" + idcard + ".jpg";
                    break;
            }
            this.EmpImg.ImageUrl = empImg;
            this.EmpSign.ImageUrl = empSign;

            #region Image Retrieve from DB
            //ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");
            //string empid = this.ddlPEmpName.SelectedValue.ToString();
            //if (this.chknewEmp.Checked == true)
            //{
            //    empid = this.ddlNPEmpName.SelectedValue.ToString();
            //}

            //DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
            //    return;
            //}

            //DataTable dt2 = ds1.Tables[0];
            //if(dt2.Rows.Count>0)
            //{
            //    string imgUrl = dt2.Rows[0]["imgurl"].ToString();
            //    string signUrl = dt2.Rows[0]["signurl"].ToString();

            //    this.EmpImg.ImageUrl = imgUrl;
            //    this.EmpSign.ImageUrl = signUrl;
            //}
            //else
            //{
            //    this.EmpImg.ImageUrl = "";
            //    this.EmpSign.ImageUrl = "";
            //}
            #endregion
        }



        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            ProcessAccess HRData = new ProcessAccess();
            string empid = this.ddlPEmpName.SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "DELETEUSEIMG", empid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete Failed!');", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
        protected void ddlDept1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chkEdit.Checked)
                    return;
                else if (this.chknewEmp.Checked)
                    return;
                string typeemp = this.Request.QueryString["emptype"].ToString().Trim();
                string qempid = this.Request.QueryString["empid"].ToString().Trim();
                string qemptype = this.Request.QueryString["section"] ?? "";
                if (qemptype.Length > 0)
                {
                    qemptype = qemptype.Substring(0, 4) + "%";
                }

                Session.Remove("tblemp");
                string comcod = this.GetCompCode();

                string emptype = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
                string div = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
                string department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";

                string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
                string joblocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
                string txtSProject = "%";


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();

                DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", section, txtSProject, emptype, div, department, joblocation, userid, "", "");
                this.ddlPEmpName.DataTextField = "empname";
                this.ddlPEmpName.DataValueField = "empid";
                this.ddlPEmpName.DataSource = ds5.Tables[0];
                this.ddlPEmpName.DataBind();

                //Selected Employee
                if (typeemp == "Old" && qemptype == emptype)
                {
                    if (qempid.Length > 0)
                    {
                        this.ddlPEmpName.SelectedValue = qempid;

                    }

                }

                Session["tblemp"] = ds5.Tables[0];
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
            }
        }

        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblshiftinfo"];
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Not Found any Shift Information');", true);
                return;
            }
            string shiftid = this.ddlShift.SelectedValue.ToString().Trim();

            if (shiftid == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Shift');", true);
                return;
            }

            DataView dvr = new DataView();

            dvr = dt.DefaultView;
            dvr.RowFilter = ("shiftid=" + shiftid);
            dt = dvr.ToTable();


            this.ddlOffintimedw.SelectedValue = dt.Rows[0]["stdintime"].ToString().Trim();
            this.ddlOffouttimedw.SelectedValue = dt.Rows[0]["stouttime"].ToString().Trim();
            //this.ddlOffouttimedw.Enabled = false;
            //this.ddlOffintimedw.Enabled = false;
            this.Hiddshiftid.Value = dt.Rows[0]["shiftid"].ToString().Trim();
            this.Hiddabstime.Value = dt.Rows[0]["abstime"].ToString().Trim();
            this.Hiddlatemarg.Value = dt.Rows[0]["latemarg"].ToString().Trim();
            this.hiddmacstarttime.Value = dt.Rows[0]["macstarttime"].ToString().Trim();

        }

        protected void ddlJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chknewEmp.Checked)
            {
                this.ibtnNFindEmp_Click(null, null);
            }

            else
            {

                this.ddlSection_SelectedIndexChanged(null, null);


            }
        }

    }
}