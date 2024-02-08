using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using System.IO;
using System.Web.Services;
using System.Globalization;

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class EmpEntry01 : System.Web.UI.Page
    {
        Common Common = new Common();
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        static string distid = "";
        static string upzid = "";
        string postid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");


                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE PERSONAL INFORMATION";
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetInformation();
                this.GetSingEmpName();
                this.GetJobLocation();     
                this.getLastCardNo();
                this.lblLastCardNo.Visible = false;
                this.ddlEmpName_SelectedIndexChanged(null, null);
                this.CommonButton();
            }




        }
        private void CommonButton()
        {
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Text = "Final Update";
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).ToolTip = "Final Update Information";
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Agreement Form";
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Style.Remove("color");
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Style.Add("background-color", "blue");
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnFinalUpdate);
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(LnkAgreement_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void lnkbtnFinalUpdate (object sender, EventArgs e)
        {
            string type = ASTUtility.Left(this.ddlInformation.SelectedValue.ToString(), 2);
            switch (type)
            {
                case "01"://Basic Info.
                    lUpdatPerInfo_Click(null,null);
                    break;
                case "10"://Academic Record
                    lUpdateDegree_Click(null, null);
                    break;
                case "13"://Employment Record
                    lUpdateEmprecord_Click(null, null);
                    break;
                case "14"://Assoc. To 
                    lUpdateEmpAssocia_Click(null, null);
                    break;
                case "15"://Job Res.
                    lUpdateJobRes_Click(null, null);
                    break;
                case "20"://Nominee Form
                    lUpdateEmpNominee_Click(null, null);
                    break;
                case "37"://Reference
                    lUpdateRef_Click(null, null);
                    break;
                case "39"://Special Skill
                    lUpdateSpecialSkill_Click(null, null);
                    break;
            }
            
        }
        protected void LnkAgreement_Click(object sender, EventArgs e)
        {
            string section = "";
            string emptype = "";
            if (this.chkNewEmp.Checked == true)
            {
                section = "";
                emptype = "New";
            }
            else
            {
                section = Session["refno"].ToString()??"";
                emptype = "Old";

            }

            string empid = this.ddlEmpName.SelectedValue.ToString();
            string joblocation = Session["joblocation"].ToString();
            if (empid == "000000000000")
            {
                this.Label1.BackColor = System.Drawing.Color.Red;
                Label1.BorderWidth = 1;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "alert('Please select Employee')", true);
                return;
            }
            else
            {
                Response.Redirect("~/F_81_Hrm/F_82_App/HREmpEntry?Type=Aggrement&section=" + section + "&empid=" + empid + "&emptype=" + emptype + "&jobloc=" + joblocation);

            }


        }
        private void GetSingEmpName()
        {
            string comcod = this.GetComeCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPNAME", "940[1-2]%", "%", "%", "%", "0", "%", "", "", "");
            this.ddlsign.DataTextField = "empname1";
            this.ddlsign.DataValueField = "empid";
            this.ddlsign.DataSource = ds3.Tables[0];
            this.ddlsign.DataBind(); 
            if(comcod == "5305")
            {
                this.ddlsign.SelectedValue = "930100101063";
            }
           
        }
        private void GetJobLocation()
        {
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
            //lst1.Add()

            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();




            this.ddlSection_SelectedIndexChanged(null, null);

        }


        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }

        public void GetAllOrganogramList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }

        private void getLastCardNo()
        {

            string comcod = this.GetComeCode();
            string section = this.ddlSection.SelectedValue.ToString();//.Substring(0,4)!="9401"?:"";
                                                                      //(this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";


            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLASTCARDNO", section, "", "", "", "", "", "", "", "");
            if (ds5==null)
            {
                return;
            }
            this.lblLastCardNo.Text = ds5.Tables[0].Rows[0]["lastCard"].ToString().Trim();


        }
        protected void chkNewEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNewEmp.Checked == true)
            {
                InactiveEmp.Checked = false;

                //  chkNewEmp.Text = "New Emp";
            }
            else
            {
                // chkNewEmp.Text = "Current Emp";

                //  InactiveEmp.Checked = true;

            }
            this.GetEmployeeName();
            getLastCardNo();


        }
        private void GetEmployeeName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            Session.Remove("tblempname");
            string comcod = this.GetComeCode();
            string txtSProject = (this.Request.QueryString["empid"] != "") ? "%" + this.Request.QueryString["empid"].ToString() + "%" : "%";
            string qempid = this.Request.QueryString["empid"] ?? "";


            if (qempid.Length > 0)
            {
                string emptype = "94" + qempid.Substring(2, 2) + string.Concat(Enumerable.Repeat("0", 8));
                this.ddlWstation.SelectedValue = emptype;
              //  this.ddlWstation_SelectedIndexChanged(null, null);

            }

            string empType = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptcode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string NewEmp = (this.chkNewEmp.Checked == true) ? "New" : "";
            string InactiveEmp = (this.InactiveEmp.Checked == true) ? "0" : "";
            string jobLoc = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLISTPERINFO", txtSProject, deptcode, section, NewEmp, empType, divison, InactiveEmp, jobLoc, userid);
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblempname"] = ds3.Tables[0];
            ds3.Dispose();

            this.ddlEmpName.SelectedValue = (this.Request.QueryString["empid"] == "") ? this.ddlEmpName.Items[0].Value : this.Request.QueryString["empid"].ToString(); 

        }

        private void GetInformation()
        {

            string comcod = this.GetComeCode();
            string txtinformation = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETINFORMATION", txtinformation, "", "", "", "", "", "", "", "");
            this.ddlInformation.DataTextField = "infodesc";
            this.ddlInformation.DataValueField = "infoid";
            this.ddlInformation.DataSource = ds3.Tables[1];
            this.ddlInformation.DataBind();

        }


        protected void ibtnInformation_Click(object sender, EventArgs e)
        {
            this.GetInformation();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = ASTUtility.Left(this.ddlInformation.SelectedValue.ToString(), 2);
            switch (type)
            {
                case "20":
                    this.NominineeFormPrint();
                    break;
            }

        }


        private void NominineeFormPrint()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");

            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //ViewState ["nominiparlist"] = ds3.Tables[0];
            // ViewState["nominiempdata"] = ds3.Tables[1];


            DataTable dt = (DataTable)ViewState["nominiempdata"];
            DataTable dt1 = (DataTable)ViewState["nominiparlist"];

            DataView dv1 = new DataView();
            dv1 = dt1.DefaultView;
            //dv1.RowFilter = ("nomname <> ''");
            dv1.ToTable();

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var rptlist = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.Empnomineelist>();
            var joDate = dt.Rows[0]["jointdat"];
            var jDate = Convert.ToDateTime(joDate).ToString("dd-MMM-yyyy");
            var pdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            var dob = Convert.ToDateTime(dt.Rows[0]["dob"]).ToString("dd-MMM-yyyy");

            var dates = jDate.Split('-');
            string firstnum = NumBn(dates[0]);
            string lastnum = NumBn(dates[2]);

            var bdates = dob.Split('-');
            string firstbdates = NumBn(dates[0]);
            string lastbdates = NumBn(dates[2]);
            var pdates = pdate.Split('-');
            string firstpdate = NumBn(dates[0]);
            string lastpdate = NumBn(dates[2]);
            var CompInfoBn = ASTUtility.CompInfoBn();
            
            LocalReport rpt1 = new LocalReport();


            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpNomList", rptlist, null, null);




            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", CompInfoBn.Item1));
            rpt1.SetParameters(new ReportParameter("comadd", CompInfoBn.Item2));
            rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "মনোনয়ন ফরম"));
            rpt1.SetParameters(new ReportParameter("printdate", firstpdate + "-" + GetMonthName(pdates[1]) + "-" + lastpdate));
            rpt1.SetParameters(new ReportParameter("empnam", dt.Rows[0]["empnam"].ToString()));
            rpt1.SetParameters(new ReportParameter("degnam", dt.Rows[0]["desigdesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("joindate", firstbdates + "-" + GetMonthName(bdates[1]) + "-" + lastbdates));
            rpt1.SetParameters(new ReportParameter("bdate", firstnum + "-" + GetMonthName(dates[1]) + "-" + lastnum));
            rpt1.SetParameters(new ReportParameter("fnam", dt.Rows[0]["fathname"].ToString()));
            rpt1.SetParameters(new ReportParameter("mname", dt.Rows[0]["mothname"].ToString()));
            rpt1.SetParameters(new ReportParameter("deptnam", dt.Rows[0]["deptdesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("sesonnam", dt.Rows[0]["sectiondesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("gender", dt.Rows[0]["gender"].ToString()));
            rpt1.SetParameters(new ReportParameter("paddess", dt.Rows[0]["peraddress"].ToString()));
            rpt1.SetParameters(new ReportParameter("idno", dt.Rows[0]["idcard"].ToString()));

            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        public string GetMonthName(string name)
        {
            return name.Replace("Jan", "জানুয়ারী").Replace("Feb", "ফেব্রুয়ারী").Replace("Mar", "মার্চ").
                Replace("Apr", "এপ্রিল").Replace("May", "মে").Replace("Jun", "জুন").Replace("Jul", "জুলাই").
                Replace("Aug", "আগস্ট").Replace("Sep", "সেপ্টেম্বর").Replace("Oct", "অক্টোবর").Replace("Nov", "নভেম্বর").
                Replace("Dec", "ডিসেম্বর");

        }
        public string NumBn(string num)
        {
            string stringDate = "";
            char[] dtae = num.ToCharArray();
            foreach (var item in dtae)
            {

                switch (item)
                {
                    case '0': stringDate += "০"; break;
                    case '1': stringDate += "১"; break;
                    case '2': stringDate += "২"; break;
                    case '3': stringDate += "৩"; break;
                    case '4': stringDate += "৪"; break;
                    case '5': stringDate += "৫"; break;
                    case '6': stringDate += "৬"; break;
                    case '7': stringDate += "৭"; break;
                    case '8': stringDate += "৮"; break;
                    case '9': stringDate += "৯"; break;
                }



            }
            return stringDate;
        }
        //if (this.lbtnOk.Text == "Ok")
        //{

        //    this.ddlEmpName.Visible = false;
        //    this.lblEmpName.Visible = true;
        //    this.ddlInformation.Visible = false;
        //    this.lblInformation.Visible = true;
        //    this.lbtnOk.Text = "New";
        //    this.lblEmpName.Text = this.ddlEmpName.SelectedItem.Text;
        //    this.lblInformation.Text = this.ddlInformation.SelectedItem.Text;
        //    this.SelectView();
        //    return;
        //}

        //this.ddlEmpName.Visible = true;
        //this.lblEmpName.Visible = false;
        //this.ddlInformation.Visible = true;
        //this.lblInformation.Visible = false;
        //this.lbtnOk.Text = "Ok";
        //this.MultiView1.ActiveViewIndex = -1;
        //this.lblEmpName.Text = "";
        //this.lblInformation.Text = "";
        //this.lblmsg.Text = "";



        private void SelectView()
        {
            string infoid = this.ddlInformation.SelectedValue.ToString();
            this.lblLastCardNo.Visible = false;

            switch (infoid)
            {
                case "01":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetBldMeReFes();
                    this.GetSupervisorName();
                    this.ShowPersonalInformation();
                    break;

                case "10":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetAcaDemicDegree();
                    this.ShowDegree();

                    break;

                case "13":

                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowEmpRecord();

                    break;

                case "14":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.ShowEmpPosition();
                    break;

                case "15":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.ShowJobRespon();
                    break;

                case "16":
                    this.MultiView1.ActiveViewIndex = 6;
                    break;

                case "20":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.ShowNominee();
                    break;

                case "37":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.ShowReferecne();
                    break;

                case "39":
                    this.MultiView1.ActiveViewIndex = 7;
                    this.ShowSpecialInfo();
                    break;

            }

        }

        private void GetBldMeReFes()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            Session.Remove("tblempdetinfo");
            Session.Remove("tbldistrict");
            Session.Remove("tblupzilla");
            Session.Remove("tblpostoffice");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETBLDMEREFES", userid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblempdetinfo"] = ds2.Tables[0];


            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOUNTRYDIVDISTUPZPO", "", "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            Session["tbldistrict"] = ds3.Tables[0];
            Session["tblupzilla"] = ds3.Tables[1];
            Session["tblpostoffice"] = ds3.Tables[2];
            Session["tblvill"] = ds3.Tables[3];
            Session["tblshift"] = ds3.Tables[4];



        }
        private void GetSupervisorName()
        {
            Session.Remove("tblsppname");
            string comcod = this.GetComeCode();
            string txtSProject = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", txtSProject, "%", "%", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            Session["tblsppname"] = ds3.Tables[0];
            ds3.Dispose();
        }

        private void GetAcaDemicDegree()
        {
            string comcod = this.GetComeCode();
            Session.Remove("tblacadeg");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETACADEMICDEGREE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblacadeg"] = ds2;
            ds2.Dispose();

        }
        private void ShowPersonalInformation()
        {
                        //name
            string comcod = this.GetComeCode();
            string empType = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPPERSONALINFO", empid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            DataTable emptalbe = (DataTable)Session["tblempname"];
            DataTable dt = ds2.Tables[0];
            DataTable personaldt = ds2.Tables[0].Copy();
            Session["UserLog"] = ds2.Tables[1];
            if(ds2.Tables[2]!=null && ds2.Tables[2].Rows.Count>0)
            {
                Session["refno"] = ds2.Tables[2].Rows[0]["refno"].ToString();
            }
           
            DataRow[] dr = dt.Select("gcod='01002'");
            dr[0]["gdesc1"] = (((DataTable)Session["tblempname"]).Select("empid='" + empid + "'"))[0]["empname1"];
            dr[0]["gdatatbn"] = ((((DataTable)Session["tblempname"]).Select("empid='" + empid + "'"))[0]["bname"].ToString() != "") ?
                (((DataTable)Session["tblempname"]).Select("empid='" + empid + "'"))[0]["bname"].ToString() : dr[0]["gdatatbn"];
            DataTable dt1 = (DataTable)Session["tblempdetinfo"];
            DataTable dtdist = (DataTable)Session["tbldistrict"];
            DataTable dtupzila = (DataTable)Session["tblupzilla"];
            DataTable dtpostoff = (DataTable)Session["tblpostoffice"];
            DataTable dtvill = (DataTable)Session["tblvill"];
            DataTable dtshift = (DataTable)Session["tblshift"];


            DataView dv1;
            this.gvPersonalInfo.DataSource = ds2.Tables[0];// ds2.Tables[0];
            this.gvPersonalInfo.DataBind();
            string postid = "";
            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();


                switch (Gcode)
                {

                    //bangla lang disable for few codes
                    case "01001":

                        switch (comcod)
                        {
                            case "5301":
                                ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = (chkNewEmp.Checked == true ? (dt.Rows[i]["gdesc1"].ToString().Trim().Length == 0 ? this.lblLastCardNo.Text.Trim() : dt.Rows[i]["gdesc1"].ToString().Trim()) : (dt.Rows[i]["gdesc1"].ToString()));
                                break;

                            default:
                                break;
                        
                        
                        }
                    

                        
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                    case "01006": //Probation Period
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '85%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        //Default
                        if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim().Length == 0)
                        {

                            switch (comcod)
                            {
                                case "5305":
                                case "5306":
                                    ddlgval.SelectedValue = "85003";
                                    string Joindate = "";
                                    for (int j = 0; j < this.gvPersonalInfo.Rows.Count; j++)
                                    {
                                        string Gcodei = ((Label)this.gvPersonalInfo.Rows[j].FindControl("lblgvItmCode")).Text.Trim();

                                        switch (Gcodei)
                                        {
                                            case "01003": //Join Date

                                                Joindate = (((TextBox)this.gvPersonalInfo.Rows[j].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd.MM.yyyy")
                                                    : ((TextBox)this.gvPersonalInfo.Rows[j].FindControl("txtgvdVal")).Text.Trim();
                                                //Joindate = ASTUtility.DateFormat(Joindate) ;
                                                // ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = Joindate;
                                                break;


                                            case "01006": //Confirmation Date
                                                string value = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedItem.Text.Trim();
                                                int monyear = (value.Contains("Month")) ? Convert.ToInt32(ASTUtility.Left(value, 2)) : (12 * Convert.ToInt32(ASTUtility.Left(value, 2)));
                                                string ConDate = Convert.ToDateTime(ASTUtility.DateFormat(Joindate)).AddMonths(monyear).ToString("dd.MM.yyyy");
                                                ((TextBox)this.gvPersonalInfo.Rows[j + 1].FindControl("txtgvdVal")).Text = ConDate;
                                                break;

                                        }


                                    }
                                    break;

                                default:
                                    break;


                            }
                        }
                        else
                        {
                            
                            ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        }
                        break;



                    case "01009"://Blood Group
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '90%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01010"://Relationship Status 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '92%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "01011"://Religion
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '95%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01012"://Festival
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '97%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01013"://Nationality
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '98%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01023"://Gender
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '99%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    //DataTable dtdist = (DataTable)Session["tbldistrict"];
                    //DataTable dtupzila = (DataTable)Session["tblupzilla"];

                    case "01025"://Permanent District
                        dv1 = dtdist.DefaultView;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "distname";
                        ddlgval.DataValueField = "distid";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01026"://Permanent Upazila
                        dv1 = dtupzila.DefaultView;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "upzname";
                        ddlgval.DataValueField = "upzid";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01031"://Present District
                        dv1 = dtdist.DefaultView;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "distname";
                        ddlgval.DataValueField = "distid";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim().Length == 0) //New Emp.
                        {
                            ddlgval.SelectedValue = "3";
                        }
                        else
                        {
                            ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim(); //Existing Emp.
                        }
                        break;                   

                    case "01032"://Present Upazila
                        dv1 = dtupzila.DefaultView;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "upzname";
                        ddlgval.DataValueField = "upzid";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim().Length == 0) //New Emp.
                        {
                            ddlgval.SelectedValue = "160";
                        }
                        else
                        {
                            ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim(); //Existing Emp.
                        }
                        break;

                    case "01033"://Present Post Office
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim().Length == 0) //New Emp.
                        {
                            ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = "বাড়ইপাড়া";
                        }
                        else
                        {
                            ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim(); //Existing Emp.
                        }                        
                        break;

                    case "01035"://Present Village
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim().Length == 0) //New Emp.
                        {
                            ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = "উলুসাড়া";
                        }
                        else
                        {
                            ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim(); //Existing Emp.
                        }                       
                        break;

                    //case "01027": // Post Office	
                    //case "01033": //Present post Office
                    //    dv1 = dtpostoff.DefaultView;
                    //    //  dv1.RowFilter = ("gcod like '96%'");
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    //    ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                    //    ddlgval.DataTextField = "postoffnam";
                    //    ddlgval.DataValueField = "postid";
                    //    ddlgval.DataSource = dv1.ToTable();
                    //    ddlgval.DataBind();
                    //    ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    //    postid = ddlgval.SelectedValue.ToString();
                    //    break;


                    case "01030": // Shift                                          
                        //  dv1.RowFilter = ("gcod like '96%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "shiftname";
                        ddlgval.DataValueField = "shiftid";
                        ddlgval.DataSource = dtshift;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                        

                    //case "01037": //Village (Parmanent)
                    //case "01035": //Village (Present)                        
                    //    dv1 = dtvill.DefaultView;
                    //    dv1.RowFilter = ("postid='" + postid + "' or postid='0'");
                    //    //  dv1.RowFilter = ("gcod like '96%'");
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                    //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                    //    ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                    //    ddlgval.DataTextField = "villagenam";
                    //    ddlgval.DataValueField = "villid";
                    //    ddlgval.DataSource = dv1.ToTable();
                    //    ddlgval.DataBind();
                    //    ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                    //    break;


                    case "01038": //Unit
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '875%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "01995": // Service Location
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '28%' or gcod like '29%'");
                        dv1.Sort = "gcod asc";
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01996": // Supper Location


                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "empname";
                        ddlgval.DataValueField = "empid";
                        ddlgval.DataSource = (DataTable)Session["tblsppname"];
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01997": // Grade
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '86%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01007": //Confirmation Date
                    case "01999":
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        break;

                    case "01008":
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        string date = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                        if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim().Length > 0)
                        {
                            ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = Convert.ToDateTime(date).ToString("dd.MM.yyyy");
                        }
                        break;

                    case "01003": // Joining Date
                 
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;

                        date = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                        //if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim().Length == 0)
                        //{
                        //    ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = System.DateTime.Today.ToString("dd.MM.yyyy");

                        //}
                        if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim().Length > 0)
                        {
                            ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = Convert.ToDateTime(date).ToString("dd.MM.yyyy");
                        }
                        break;


                  


                    case "01028": //Job location
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '870%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();

                        if (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim().Length == 0)
                        {

                            switch (comcod)
                            {
                                case "5305":
                                case "5306"://Footbed Footwear Ltd
                                    string emptype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4);
                                    switch (emptype)
                                    {
                                        case "9403": // Worker
                                        case "9404": // Worker
                                        case "9405": // Worker
                                        case "9406": // Worker
                                        case "9407": // Worker
                                        case "9408": // Worker(Footbed)
                                        case "9409": // Worker
                                        case "9410": // Worker
                                        case "9413": // NON OT Based
                                        case "9414": // OT Based
                                        case "9415": // NON OT Based(Footbed)
                                        case "9416": // OT Based(Footbed)
                                        case "9420": // Grade-01
                                        case "9422": // Grade-01(Footbed)
                                            ddlgval.SelectedValue = "87002";
                                            break;

                                        default:
                                            break;

                                    }
                                    break;


                                default:
                                    break;

                            }
                        }
                        else
                        {

                            ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        }
                        Session["joblocation"] = ddlgval.SelectedValue;
                       // ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01050": // Budgeted

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));

                        var lst = getlist.GetBgdTypelist(comcod);


                        ddlgval.DataTextField = "hrgdesc";
                        ddlgval.DataValueField = "hrgcod";
                        ddlgval.DataSource = lst;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();



                        break;
                    case "01070": // Floor Line

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '70%'");
                        dv1.Sort = "gcod desc";
                        DataTable dtFloor = dv1.ToTable();
                        //dtFloor.Rows.Add("","00000", "None");
                        //DataRowView newRow = dv1.AddNew();
                        //newRow["gcod"] = "";
                        //newRow["gdesc"] = "None";                       
                        //newRow.EndEdit();

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dtFloor;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01074": // Skill

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '740%'");

                        DataTable dtskill = dv1.ToTable();
                        DataRow dr3 = dtskill.NewRow();
                        dr3["gcod"] = "";
                        dr3["gdesc"] = "None";
                        dtskill.Rows.Add(dr3);
                        //DataRowView newRow1 = dv1.AddNew();
                        //newRow1["gcod"] = "";
                        //newRow1["gdesc"] = "None";
                        //newRow1.EndEdit();



                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dtskill;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                        break;
                    case "01082": // Manpower Type

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '745%'");

                        DataTable mantype = dv1.ToTable();
                        DataRow dr4 = mantype.NewRow();
                        dr4["gcod"] = "";
                        dr4["gdesc"] = "None";
                        mantype.Rows.Add(dr4);
                        //DataRowView newRow1 = dv1.AddNew();
                        //newRow1["gcod"] = "";
                        //newRow1["gdesc"] = "None";
                        //newRow1.EndEdit();



                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = mantype;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                        break;

                    case "01090": //Bus Location

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '80%'");

                        DataTable busloc = dv1.ToTable();
                        DataRow dr5 = busloc.NewRow();
                        dr5["gcod"] = "";
                        dr5["gdesc"] = "None";
                        busloc.Rows.Add(dr5);

                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = busloc;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        break;

                }

            }



        }
        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {            
            this.ddlInformation_SelectedIndexChanged(null, null);
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string signatory = ((DataTable)Session["tblempname"]).Select("empid = '" + empid + "'")[0]["signatory"].ToString();
            if (signatory != "000000000000")
            {
                this.ddlsign.SelectedValue = signatory;
            }
        }

        protected void ddlInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectView();
        }
        private void ShowDegree()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPACADEGREE", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvDegree.DataSource = null;
                this.gvDegree.DataBind();
                return;

            }
            Session["tblempAcaRecord"] = ds3.Tables[0];

            DataTable dt = ds3.Tables[0];
            this.gvDegree.DataSource = ds3.Tables[0];
            this.gvDegree.DataBind();
            DataSet ds1 = (DataSet)Session["tblacadeg"];

            DropDownList ddldegree, ddlAcadegree, ddlMajorSubj, ddlresult, ddlpyear, ddlBoard, ddlInst;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Degree
                string Gcode = dt.Rows[i]["gcod"].ToString();
                ddldegree = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlDegree"));
                ddldegree.DataTextField = "gdesc";
                ddldegree.DataValueField = "gcod";
                ddldegree.DataSource = ds1.Tables[0];
                ddldegree.DataBind();
                ddldegree.SelectedValue = Gcode;

                //Academic Degree
                DataTable dt1 = ds1.Tables[1].Copy();
                DataView dv1 = dt1.DefaultView;
                dv1.RowFilter = ("maincode='99999' or maincode='" + Gcode + "'");

                string subcode = dt.Rows[i]["subcode"].ToString();
                ddlAcadegree = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                ddlAcadegree.DataTextField = "subdesc";
                ddlAcadegree.DataValueField = "subcode";
                ddlAcadegree.DataSource = dv1.ToTable();
                ddlAcadegree.DataBind();
                ddlAcadegree.SelectedValue = subcode;


                //Major Subject
                string majsubcode = dt.Rows[i]["majsubcode"].ToString();
                ddlMajorSubj = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlMajorSubj"));

                ddlMajorSubj.Items.Clear();
                ddlMajorSubj.DataTextField = "gdesc";
                ddlMajorSubj.DataValueField = "gcod";
                ddlMajorSubj.DataSource = ds1.Tables[3];
                ddlMajorSubj.DataBind();
                ddlMajorSubj.SelectedValue = majsubcode;



                //Result

                string resultcode = dt.Rows[i]["resultcode"].ToString();
                ddlresult = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlResult"));
                ddlresult.DataTextField = "gdesc";
                ddlresult.DataValueField = "gcod";
                ddlresult.DataSource = ds1.Tables[2];
                ddlresult.DataBind();
                ddlresult.SelectedValue = resultcode;


                //Passing Year
                int year = (dt.Rows[i]["pyear"].ToString() == "") ? Convert.ToInt32(System.DateTime.Today.ToString("yyyy")) : Convert.ToInt32("0" + dt.Rows[i]["pyear"].ToString());

                List<string> passyear = ASITUtility02.pasyear(year - 54, year + 5);
                ddlpyear = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlPassingYear"));
                foreach (string pass in passyear)
                    ddlpyear.Items.Add(pass);
                ddlpyear.SelectedValue = year.ToString();

                //Board

                string BoardCode = dt.Rows[i]["brcode"].ToString();
                ddlBoard = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlBoard"));
                ddlBoard.DataTextField = "gdesc";
                ddlBoard.DataValueField = "gcod";
                ddlBoard.DataSource = ds1.Tables[4];
                ddlBoard.DataBind();
                ddlBoard.SelectedValue = BoardCode;

                //Institution
                ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvVal")).Visible = false;
                //string InsCode = dt.Rows[i]["instcode"].ToString();
                ddlInst = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlInst"));
                ddlInst.DataTextField = "gdesc";
                ddlInst.DataValueField = "gcod";
                ddlInst.DataSource = ds1.Tables[5];
                ddlInst.DataBind();
                ddlInst.SelectedValue = "99999";

                if (((TextBox)this.gvDegree.Rows[i].FindControl("txtgvVal")).Text.Trim().Length > 0)
                {
                    ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlInst")).Items.Clear();
                    ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlInst")).Visible = false;
                    ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvVal")).Visible = true;
                }



            }
            this.ddlResult_SelectedIndexChanged(null, null);

        }
        private void ShowMajorSub()
        {

        }

        private void ShowEmpRecord()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPRECORD", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvEmpRec.DataSource = null;
                this.gvEmpRec.DataBind();
                return;
            }
            this.gvEmpRec.DataSource = ds3.Tables[0];
            this.gvEmpRec.DataBind();
        }

        private void ShowEmpPosition()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPPOSITION", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvAssocia.DataSource = null;
                this.gvAssocia.DataBind();
                return;

            }
            this.gvAssocia.DataSource = ds3.Tables[0];
            this.gvAssocia.DataBind();



        }

        private void ShowReferecne()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPPREF", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvRef.DataSource = null;
                this.gvRef.DataBind();
                return;

            }
            this.gvRef.DataSource = ds3.Tables[0];
            this.gvRef.DataBind();
        }


        private void ShowNominee()
        {
            try
            {
                DataTable dtdist = (DataTable)Session["tbldistrict"];
                DataTable dtupzila = (DataTable)Session["tblupzilla"];
                DataTable dtpostoff = (DataTable)Session["tblpostoffice"];
                DataTable dtempdet = (DataTable)Session["tblempdetinfo"];
                string comcod = this.GetComeCode();
                string empid = this.ddlEmpName.SelectedValue.ToString();
                string langtype = "0";
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPNOMINEELIST", empid, langtype, "", "", "", "", "", "", "");
                if (ds3 == null)
                {
                    this.gvNomineeForm.DataSource = null;
                    this.gvNomineeForm.DataBind();
                    return;

                }
                this.gvNomineeForm.DataSource = ds3.Tables[0];
                this.gvNomineeForm.DataBind();

                DropDownList ddlgval;
                for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                {
                    string Gcode = ds3.Tables[0].Rows[i]["gcod"].ToString();

                    switch (Gcode)
                    {
                        case "20002"://Relation
                            DataView dv = dtempdet.DefaultView;
                            dv.RowFilter = "gcod like '26%'";

                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ddlgval = ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval"));
                            ddlgval.DataTextField = "gdescbn";
                            ddlgval.DataValueField = "gcod";
                            ddlgval.DataSource = dv.ToTable();
                            ddlgval.DataBind();

                            if (((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Text.Trim().Length > 0)
                            {
                                ((Panel)this.gvNomineeForm.Rows[i].FindControl("Panegrd")).Visible = false;
                                ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).Items.Clear();
                                ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).Visible = false;
                                ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Visible = true;
                            }
                            break;

                        case "20003"://Age
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((Panel)this.gvNomineeForm.Rows[i].FindControl("Panegrd")).Visible = false;
                            break;

                        case "20015"://District
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ddlgval = ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval"));
                            ddlgval.DataTextField = "distname";
                            ddlgval.DataValueField = "distid";
                            ddlgval.DataSource = dtdist;
                            ddlgval.DataBind();
                            //ddlgval.SelectedValue = ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Text.Trim();
                            ddlgval.SelectedValue = ds3.Tables[1].Rows.Count == 0 ? "" : ds3.Tables[1].Rows[0]["distid"].ToString();
                            break;

                        case "20020"://Upazila    
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Visible = false;
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ddlgval = ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval"));
                            ddlgval.DataTextField = "upzname";
                            ddlgval.DataValueField = "upzid";
                            ddlgval.DataSource = dtupzila;
                            ddlgval.DataBind();
                            //ddlgval.SelectedValue = ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Text.Trim();
                            ddlgval.SelectedValue = ds3.Tables[1].Rows.Count == 0 ? "" : ds3.Tables[1].Rows[0]["upzid"].ToString();
                            break;

                        case "20025"://Post Office
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Text = ds3.Tables[1].Rows.Count == 0 ? "" : ds3.Tables[1].Rows[0]["perpost"].ToString();
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((Panel)this.gvNomineeForm.Rows[i].FindControl("Panegrd")).Visible = false;
                            break;

                        case "20010"://Village
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Text = ds3.Tables[1].Rows.Count == 0 ? "" : ds3.Tables[1].Rows[0]["pervill"].ToString();
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((Panel)this.gvNomineeForm.Rows[i].FindControl("Panegrd")).Visible = false;
                            break;

                        default:
                            ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvdVal")).Visible = false;
                            ((Panel)this.gvNomineeForm.Rows[i].FindControl("Panegrd")).Visible = false;
                            ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).Items.Clear();
                            ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).Visible = false;
                            break;
                    }
                }

                ViewState["nominiparlist"] = ds3.Tables[0];
                ViewState["nominiempdata"] = ds3.Tables[1];
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }    
        }


        private void ShowJobRespon()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPJOBRESPONSIBILITES", empid, "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.grvJobRespo.DataSource = null;
                this.grvJobRespo.DataBind();

                return;

            }
            this.grvJobRespo.DataSource = ds4.Tables[0];
            this.grvJobRespo.DataBind();

        }
        private void ShowSpecialInfo()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPSPECIALSKIL", empid, "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvSpecialInfo.DataSource = null;
                this.gvSpecialInfo.DataBind();

                return;

            }
            this.gvSpecialInfo.DataSource = ds4.Tables[0];
            this.gvSpecialInfo.DataBind();

            DropDownList ddlSpecialVal;
            for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
            {
                string Gcode = ds4.Tables[0].Rows[i]["gdata"].ToString();

                ddlSpecialVal = ((DropDownList)this.gvSpecialInfo.Rows[i].FindControl("ddlSpecialVal"));

                ddlSpecialVal.SelectedValue = Gcode;


            }
        


    }
        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            try
            {
                this.UpdateSignatory();
                //---------------Validation Check---------------------//
                int value = 0;
                string joindate = "";
                for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                    if (Gcode == "01001" || Gcode == "01003" || Gcode == "01008")
                    {
                        string Gvalue = (Gcode == "01001") ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() :
                                        (Gcode == "01003") ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() :
                                        (Gcode == "01008") ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() :
                                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                        if (Gvalue.Length == 0)
                        {
                            int x = 1;
                            ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.Color.Red;

                            ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Attributes["style"] = "border:1px solid red; width:100px";
                            ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Attributes["style"] = "border:1px solid red; width:100px";


                            value = value + x;
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('ID CARD/DOJ/DOB is not empty!');", true);
                            return;
                        }
                        else
                        {
                            ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#000");
                        }

                    }
                    if (Gcode == "01003")
                    {

                        joindate = ASTUtility.DateFormat(((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim());
                    }
                    //else if (Gcode == "01023" || Gcode == "01010" || Gcode == "01011")
                    //{
                    //    string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

                    //    if (Gvalue == "99001" || Gvalue == "92001" || Gvalue == "95001")
                    //    {
                    //        int y = 1;
                    //        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.Color.Red;
                    //        value = value + y;
                    //    }
                    //    else
                    //    {
                    //        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#000");
                    //    }

                    //}


                }
                //if (value != 0)
                //{
                //    this.lblmsg.Text = "Updated Not Successfully";
                //    return;
                //}
                //---------------Validation Check---------------------//

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComeCode();
                //string Gvalue="";
                string empid = this.ddlEmpName.SelectedValue.ToString();
                if (empid == "000000000000")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Valid Emp Name');", true);


                }
                string empname = ((TextBox)this.gvPersonalInfo.Rows[1].FindControl("txtgvVal")).Text.Trim();
                //Log Entry

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

                bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPLINF", empid, empname, PostedByid, PostSession, Posttrmid, Posteddat,
                        EditByid, Editdat, Editrmid, joindate, "", "", "", "", "", "", "", "", "", "", "");



                if (result == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);


                    return;
                }

                for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                    string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                    string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                    string Gdatatbn = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtGdatatbn")).Text.Trim();

                    //Check Duplicate ID Card
                    if (Gcode == "01001")
                    {
                        Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();

                        ///////////////----------------------------------------
                        DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETIDCARDNO", Gvalue, "", "", "", "", "", "", "", "");
                        if (ds2.Tables[0].Rows.Count == 0)
                            ;
                        else
                        {
                            DataView dv1 = ds2.Tables[0].DefaultView;
                            dv1.RowFilter = ("empid <>'" + empid + "'");
                            DataTable dt = dv1.ToTable();
                            if (dt.Rows.Count == 0)
                                ;
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Found Duplicate ID CARD No');", true);


                                return;
                            }
                        }

                    }
                    //Check Duplicate Machine ID
                    else if (Gcode == "01990")
                    {
                        Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETMACHINEIDNO", Gvalue, "", "", "", "", "", "", "", "");
                        if (ds3.Tables[0].Rows.Count == 0 || ds3 == null)
                            ;
                        else
                        {
                            DataView dv2 = ds3.Tables[0].DefaultView;
                            dv2.RowFilter = ("empid <>'" + empid + "'and machineid <>''");
                            DataTable dt = dv2.ToTable();
                            if (dt.Rows.Count == 0)
                                ;
                            else
                            {
                                string dupInfo = "Found Duplicate Machine ID- ID Card: " + dt.Rows[0]["idcardno"].ToString() + " Machine ID: " + dt.Rows[0]["machineid"].ToString();
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + dupInfo + "');", true);
                                return;
                            }
                        }
                    }

                    if (Gcode == "01003" || Gcode == "01007" || Gcode == "01008")
                    {

                        Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    }

                    //Age Validation
                    if (Gcode == "01008")
                    {

                        Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                        string fdate = ASTUtility.DateFormat(Gvalue);
                        string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                        int year = ASTUtility.DatediffYear(Convert.ToDateTime(todate), Convert.ToDateTime(fdate));
                        if (year < 18)
                        {
                            string msg = "Age: " + year + " ***Employee Age Must be 18 or Greater than***!";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                            return;
                        }
                    }

                    if (Gcode == "01999")
                    {
                        Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    }

                    Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue)
                        : (gtype == "N") ? ASTUtility.StrPosOrNagative(Gvalue).ToString()
                        : Gvalue;
                    result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Gvalue, "", "", "", "", "", "0", "",
                        "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", Gdatatbn, "", userid, Posttrmid);

                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail');", true);


                    }
                }
                this.getLastCardNo();

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);



                Common.LogStatus("Personal Information", this.ddlInformation.SelectedItem.Text + " Department: " + this.ddlDept.SelectedItem.Text + " Section: " + this.ddlSection.SelectedItem.Text, empid, " " + empname);

            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }       
        }
        protected void lUpdateDegree_Click(object sender, EventArgs e)
        {
            

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvDegree.Rows.Count; i++)
            {
                string Gcode = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlDegree")).SelectedValue.ToString();
                string gtype = ((Label)this.gvDegree.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Degree = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlDegree")).SelectedItem.Text.Trim();
                string Group = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree")).SelectedValue.ToString();
                string MajorSubj = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlMajorSubj")).SelectedValue.ToString();
                //Updated Jan 2023
                string Institute = (((DropDownList)this.gvDegree.Rows[i].FindControl("ddlInst")).SelectedValue == "") ?
                              ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlInst")).SelectedItem.Text.Trim();

                string Result = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlResult")).SelectedValue.ToString();
                string PassisnYr = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlPassingYear")).SelectedValue.ToString();
                string marksorgrade = Convert.ToDouble("0" + ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Text.Trim()).ToString();
                string Scale = Convert.ToDouble("0" + ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Text.Trim()).ToString();
                string ddlBoard = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlBoard")).SelectedValue.ToString();
                if (Gcode != "99999" && Group != "999999999")
                {
                    bool result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Degree, "", Group, Institute, Result,
                       PassisnYr, "0", "", "0", "0", "0", "0", "0", "0", "", "", "", marksorgrade, Scale, "1", MajorSubj, ddlBoard, "", "0", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        return;
                    }
                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Academic Record Updated Successfully');", true);

        }
        protected void lUpdateEmprecord_Click(object sender, EventArgs e)
        {
           

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvEmpRec.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvEmpRec.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvEmpRec.Rows[i].FindControl("lgvgval")).Text.Trim();
                string ComName = ((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgcvCompany")).Text.Trim();
                string Desig = ((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgvDesig")).Text.Trim();
                string txtgvesDuration = ((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgvesDuration")).Text.Trim();

                if (ComName.Length > 0)
                    HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, ComName, "", Desig, txtgvesDuration,
                        "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0");

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

           

        }
        protected void lUpdateEmpAssocia_Click(object sender, EventArgs e)
        {
          

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvAssocia.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvAssocia.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvAssocia.Rows[i].FindControl("lgvgval")).Text.Trim();
                string OrgName = ((TextBox)this.gvAssocia.Rows[i].FindControl("txtgcvOrgName")).Text.Trim();
                string Position = ((TextBox)this.gvAssocia.Rows[i].FindControl("txtgvPostion")).Text.Trim();


                if (OrgName.Length > 0)
                    HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, OrgName, "", Position, "", "", "",
                        "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0");

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

          

        }
        protected void lUpdateRef_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            bool result;
            for (int i = 0; i < this.gvRef.Rows.Count; i++)
            {

                string Gcode = ((Label)this.gvRef.Rows[i].FindControl("lblgvItmCodeRef")).Text.Trim();
                string gtype = ((Label)this.gvRef.Rows[i].FindControl("lblgvRefgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvRef.Rows[i].FindControl("txtgvRefVal")).Text.Trim(); 
                string Gdatatbn = ((TextBox)this.gvRef.Rows[i].FindControl("txtgvRefGdatabn")).Text.Trim();

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue)
                       : (gtype == "N") ? ASTUtility.StrPosOrNagative(Gvalue).ToString()
                       : Gvalue;

                result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Gvalue, "", "", "", "", "", "0", "",
                    "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", Gdatatbn, "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Reference Updated Failed');", true);


                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Reference Updated Successfully');", true);

        }
        protected void lUpdateSpecialSkill_Click(object sender, EventArgs e)
        {
            

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            bool result;
            for (int i = 0; i < this.gvSpecialInfo.Rows.Count; i++)
            {

                string Gcode = ((Label)this.gvSpecialInfo.Rows[i].FindControl("gvItmCodeNomi")).Text.Trim();
                //string gtype = ((Label)this.gvSpecialInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((DropDownList)this.gvSpecialInfo.Rows[i].FindControl("ddlSpecialVal")).SelectedValue.ToString();
                //string Gdatatbn = ((TextBox)this.gvSpecialInfo.Rows[i].FindControl("txtGdatatbn")).Text.Trim();

                result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, "", Gvalue, "", "", "", "", "", "0", "",
                    "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

                  
                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            
        }
        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "01002") //if (code == "01002" || code == "01999")
                {

                    txtgvname.ReadOnly = true;

                }

            }


        }

        protected void lbtnUpdateImg_Click(object sender, EventArgs e)
        {

        }

        protected void ddlval_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
            DataTable dtupzila = (DataTable)Session["tblupzilla"];
            DataTable dtpostoff = (DataTable)Session["tblpostoffice"];
            DataTable dtvill = (DataTable)Session["tblvill"];
            
            int rowIndex = row.RowIndex;
            DataView dv1;
            DropDownList ddlgval;
            string codeItem= ((Label)this.gvPersonalInfo.Rows[rowIndex].FindControl("lblgvItmCode")).Text.ToString();
            if (codeItem == "01025" || codeItem == "01031")
            {
                string districtid= ((DropDownList)this.gvPersonalInfo.Rows[rowIndex].FindControl("ddlval")).SelectedValue.ToString(); ;
                dv1 = dtupzila.DefaultView;
                dv1.RowFilter = ("district_id =" + districtid);
                ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[rowIndex+1].FindControl("ddlval"));
                ddlgval.DataTextField = "upzname";
                ddlgval.DataValueField = "upzid";
                ddlgval.DataSource = dv1.ToTable();
                ddlgval.DataBind();
                //string upazilaid = ((DropDownList)this.gvPersonalInfo.Rows[rowIndex+1].FindControl("ddlval")).SelectedValue.ToString(); ;
                //dv1 = dtpostoff.DefaultView;
                //dv1.RowFilter = ("upzid =" + upazilaid);
                //ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[rowIndex + 2].FindControl("ddlval"));
                //ddlgval.DataTextField = "postoffnam";
                //ddlgval.DataValueField = "postid";
                //ddlgval.DataSource = dv1.ToTable();
                //ddlgval.DataBind();
            }
            //if (codeItem == "01026" || codeItem=="01032")
            //{
            //    string upazilaid = ((DropDownList)this.gvPersonalInfo.Rows[rowIndex].FindControl("ddlval")).SelectedValue.ToString(); ;
            //    dv1 = dtpostoff.DefaultView;
            //    dv1.RowFilter = ("upzid =" + upazilaid);
            //    ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[rowIndex + 1].FindControl("ddlval"));
            //    ddlgval.DataTextField = "postoffnam";
            //    ddlgval.DataValueField = "postid";
            //    ddlgval.DataSource = dv1.ToTable();
            //    ddlgval.DataBind();
            //}



            //if (codeItem == "01027" || codeItem == "01033")
            //{
            //    string postid = ((DropDownList)this.gvPersonalInfo.Rows[rowIndex].FindControl("ddlval")).SelectedValue.ToString(); ;
            //    dv1 = dtvill.DefaultView;
            //    dv1.RowFilter = ("postid =" + postid);
            //    ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[rowIndex+1].FindControl("ddlval"));
            //    ddlgval.DataTextField = "villagenam";
            //    ddlgval.DataValueField = "villid";
            //    ddlgval.DataSource = dv1.ToTable();
            //    ddlgval.DataBind();
            //}




            string Joindate = "";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                switch (Gcode)
                {
                    case "01003": //Join Date
                    case "01999": //Join Date
                        Joindate = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd.MM.yyyy")
                            :Convert.ToDateTime(ASTUtility.DateFormat(((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim())).ToString("dd.MM.yyyy");
                        //Joindate = ASTUtility.DateFormat(Joindate) ;
                        // ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = Joindate;
                        break;


                    case "01006": //Confirmation Date
                        string value = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedItem.Text.Trim();
                        int monyear = (value.Contains("Months") || value.Contains("Month") || value.Contains("months")) ? Convert.ToInt32(ASTUtility.Left(value, 2)) : (12 * Convert.ToInt32(ASTUtility.Left(value, 2)));
                        string ConDate = Convert.ToDateTime(ASTUtility.DateFormat(Joindate)).AddMonths(monyear).ToString("dd.MM.yyyy");
                        ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvdVal")).Text = ConDate;
                        break;
                    
                }


            }


        }

        protected void txtgvdVal_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;          
            int rowIndex = row.RowIndex;
            
            string codeItem = ((Label)this.gvPersonalInfo.Rows[rowIndex].FindControl("lblgvItmCode")).Text.ToString();
            if (codeItem == "01003")
            {
                string Joindate = "";
                for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
                {
                    string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                    switch (Gcode)
                    {
                        case "01003": //Join Date
                        case "01999": //Join Date
                            Joindate = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd.MM.yyyy")
                                : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                            //Joindate = ASTUtility.DateFormat(Joindate) ;
                            // ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = Joindate;
                            break;


                        case "01006": //Confirmation Date
                            string value = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedItem.Text.Trim();
                            int monyear = (value.Contains("Months") || value.Contains("Month") || value.Contains("months")) ? Convert.ToInt32(ASTUtility.Left(value, 2)) : (12 * Convert.ToInt32(ASTUtility.Left(value, 2)));
                            string ConDate = Convert.ToDateTime(ASTUtility.DateFormat(Joindate)).AddMonths(monyear).ToString("dd.MM.yyyy");
                            ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvdVal")).Text = ConDate;
                            return;
                            break;

                    }


                }
            }      

        }
        protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlgval;
            DataSet ds1 = (DataSet)Session["tblacadeg"];
            DataTable dt1 = ds1.Tables[1].Copy();
            DataView dv1;
            DataRow[] dr1;
            for (int i = 0; i < this.gvDegree.Rows.Count; i++)
            {
                string Maincode = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlDegree")).Text.Trim();
                string subcode = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree")).Text.Trim();



                switch (Maincode)
                {

                    case "10001": //SSC
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        dr1 = dv1.ToTable().Select("subcode='" + subcode + "'");
                        if (dr1.Length > 0)
                            ddlgval.SelectedValue = subcode;
                        break;



                    case "10002": //HSC
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        dr1 = dv1.ToTable().Select("subcode='" + subcode + "'");
                        if (dr1.Length > 0)
                            ddlgval.SelectedValue = subcode;
                        break;


                    case "10003": //Diploma
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        dr1 = dv1.ToTable().Select("subcode='" + subcode + "'");
                        if (dr1.Length > 0)
                            ddlgval.SelectedValue = subcode;
                        break;


                    case "10004": //BSC
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        dr1 = dv1.ToTable().Select("subcode='" + subcode + "'");
                        if (dr1.Length > 0)
                            ddlgval.SelectedValue = subcode;
                        break;

                    case "10005": //MSC
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        dr1 = dv1.ToTable().Select("subcode='" + subcode + "'");
                        if (dr1.Length > 0)
                            ddlgval.SelectedValue = subcode;
                        break;

                    case "10006": //Doctoral
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        dr1 = dv1.ToTable().Select("subcode='" + subcode + "'");
                        if (dr1.Length > 0)
                            ddlgval.SelectedValue = subcode;
                        break;

                    case "10007": //V
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        dr1 = dv1.ToTable().Select("subcode='" + subcode + "'");
                        if (dr1.Length > 0)
                            ddlgval.SelectedValue = subcode;
                        break;

                    case "10008": //VIII
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        dr1 = dv1.ToTable().Select("subcode='" + subcode + "'");
                        if (dr1.Length > 0)
                            ddlgval.SelectedValue = subcode;
                        break;

                    case "10009":
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        dr1 = dv1.ToTable().Select("subcode='" + subcode + "'");
                        if (dr1.Length > 0)
                            ddlgval.SelectedValue = subcode;
                        break;

                    case "99999": 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        break;
                }


            }
        }


        protected void ddlResult_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < this.gvDegree.Rows.Count; i++)
            {
                string resultcode = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlResult")).SelectedValue.ToString();



                switch (resultcode)
                {
                    case "17001":  //Marks System
                    case "17002":
                    case "17003":
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Text = "Marks(%) :";
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Text = "";
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Visible = true;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Visible = true;
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvScale")).Visible = false;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Visible = false;
                        break;


                    case "17004":   //Grade System
                                    //((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Text = "";
                                    //((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Text = "";
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Text = "CGPA :";
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Visible = true;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Visible = true;
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvScale")).Visible = true;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Visible = true;
                        break;

                    case "17005": //Apeared
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Text = "";
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Text = "";
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Visible = false;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Visible = false;
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvScale")).Visible = false;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Visible = false;
                        break;


                    case "99999": //Apeared
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Text = "";
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Text = "";
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Visible = false;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Visible = false;
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvScale")).Visible = false;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Visible = false;
                        break;






                }


            }
        }

        //protected void ibtngrdEmpList_Click(object sender, EventArgs e)
        //{

        //    for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
        //    {
        //        string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

        //        switch (Gcode)
        //        {
        //            case "01996": //Supper Visor


        //                string comcod = this.GetComeCode();
        //                DropDownList ddl2 = (DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval");
        //                string Searchemp = "%%";
        //                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", Searchemp, "", "", "", "", "", "", "", "");
        //                ddl2.DataTextField = "empname";
        //                ddl2.DataValueField = "empid";
        //                ddl2.DataSource = ds3.Tables[0];
        //                ddl2.DataBind();
        //                ds3.Dispose();
        //                break;



        //        }


        //    }
        //}


        protected void lnkNextbtn_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('HREmpEntry.aspx?Type=Aggrement', target='_self');</script>";
        }

        protected void gvDegree_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblempAcaRecord"];
            string comcod = this.GetComeCode();

            string empid = this.ddlEmpName.SelectedValue.ToString();
            string Gcode = ((DropDownList)this.gvDegree.Rows[e.RowIndex].FindControl("ddlDegree")).SelectedValue.ToString();



            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPDEGREEDELETE", empid, Gcode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                int rowindex = (this.gvDegree.PageSize) * (this.gvDegree.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();

            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblempAcaRecord");
            ViewState["tblempAcaRecord"] = dv.ToTable();

            this.gvDegree.DataBind();
            this.ShowDegree();

        }


        protected void lUpdateJobRes_Click(object sender, EventArgs e)
        {
            

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.grvJobRespo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.grvJobRespo.Rows[i].FindControl("lblgvItmCode1")).Text.Trim();
                // string gtype = ((Label)this.grvJobRespo.Rows[i].FindControl("lgvgval1")).Text.Trim();
                string jobRespons = ((TextBox)this.grvJobRespo.Rows[i].FindControl("txtgcvJobRes")).Text.Trim();

                if (jobRespons.Length > 0)
                    HRData.UpdateTransInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPJOBRESPONINSUPDATE", empid, Gcode, jobRespons, "", "", "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0");

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

          

        }
   
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
        }


        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            getLastCardNo();
            GetEmployeeName();
            this.SelectView();
        }

        protected void lUpdateEmpNominee_Click(object sender, EventArgs e)
        {
          

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            bool result;
            string Gvalue = "";
            for (int i = 0; i < this.gvNomineeForm.Rows.Count; i++)
            {

                string Gcode = ((Label)this.gvNomineeForm.Rows[i].FindControl("gvItmCodeNomi")).Text.Trim();
                string gtype = ((Label)this.gvNomineeForm.Rows[i].FindControl("lgvgval")).Text.Trim();
                switch (Gcode)
                {
                    case "20002"://Relation
                        Gvalue = (((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).Items.Count == 0) ?
                               ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).SelectedItem.Text.Trim();
                        break;

                    default:
                        Gvalue = (((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).Items.Count == 0) ?
                               ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                        break;
                }
                string Gdatatbn = ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtGdatatbn")).Text.Trim();

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue)
                       : (gtype == "N") ? ASTUtility.StrPosOrNagative(Gvalue).ToString()
                       : Gvalue;

                result = HRData.UpdateTransInfo1(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Gvalue, "", "", "", "", "", "0", "",
                    "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "1", "", "", "", "0", Gdatatbn, "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Failed');", true);

               
                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

         

        }

        protected void InactiveEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (InactiveEmp.Checked == true)
            {
                chkNewEmp.Checked = false;
                this.GetEmployeeName();
            }
            else
            {
                chkNewEmp.Checked = true;

            }
        }
        protected void ddlval_SelectedIndexChanged1(object sender, EventArgs e)
        {

            try
            {
                string tempdist1 = "";
                string tempupz1 = "";
                DataView dv1;
                DropDownList ddlgval;

                for (int i = 0; i < this.gvNomineeForm.Rows.Count; i++)
                {
                    DataTable dtupzila = (DataTable)Session["tblupzilla"];

                    DataTable dtpostoff = (DataTable)Session["tblpostoffice"];

                    string Gcode = ((Label)this.gvNomineeForm.Rows[i].FindControl("gvItmCodeNomi")).Text.Trim();

                    switch (Gcode)
                    {
                        case "20002"://Relation
                            string relcode = ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                            switch (relcode)
                            {
                                case "26999":
                                    ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvdVal")).Visible = false;
                                    ((Panel)this.gvNomineeForm.Rows[i].FindControl("Panegrd")).Visible = false;
                                    ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).Items.Clear();
                                    ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).Visible = false;
                                    ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Visible = true;
                                    ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Text = "";
                                    break;
                            }
                            break;

                        case "20015": //District
                            tempdist1 = ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                            break;

                        case "20020": //Upazila
                            if (distid != tempdist1)
                            {
                                distid = tempdist1;
                                dv1 = dtupzila.DefaultView;
                                dv1.RowFilter = ("district_id =" + distid);
                                ddlgval = ((DropDownList)this.gvNomineeForm.Rows[i].FindControl("ddlval"));
                                ddlgval.DataTextField = "upzname";
                                ddlgval.DataValueField = "upzid";
                                ddlgval.DataSource = dv1.ToTable();
                                ddlgval.DataBind();
                                //ddlgval.SelectedValue = ((TextBox)this.gvNomineeForm.Rows[i].FindControl("txtgvVal")).Text.Trim();
                            }
                            break;
                    }

                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }   

        }

       private void UpdateSignatory()
        {
            string comcod = this.GetComeCode().ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            if (empid == "000000000000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Select Valid Emp Name');", true);


            }
            string signatory = this.ddlsign.SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATE_EMPLOYEE_AGG_SIGNATORY", empid, signatory, "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

            }

        }

        [WebMethod(EnableSession = false)]
        public static bool SaveCapturedImage(string data, string empid)
        {
            string fileName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");
            byte[] imageBytes = new byte[0];
            byte[] signature = new byte[0];
            //Convert Base64 Encoded string to Byte Array.
            imageBytes = Convert.FromBase64String(data.Split(',')[1]);
            Common Common1 = new Common();
            string comcod = Common1.GetCompCode();
            ProcessAccess HRData1 = new ProcessAccess();

            //Save the Byte Array as Image File.
            //string filePath = HttpContext.Current.Server.MapPath(string.Format("~/Upload/HRM/{0}.jpg", fileName));
            //File.WriteAllBytes(filePath, imageBytes);
            DataSet ds3 = HRData1.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "EMPID", empid, "", "", "", "", "", "", "", "");
            bool updatPhoto;
            if (ds3.Tables[0].Rows.Count == 0)
            {
                updatPhoto = HRData1.InsertClientPhoto(comcod, empid, imageBytes, signature);
                //bool updatPhoto = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "INSERTUPDATEIMAGE", empid, photo.ToString(), signature.ToString(), "", "", "", "", "", "", "", "", "", "", "", "");
                if (updatPhoto)
                {
                   
                    return true;


                }
                else
                {
                   
                    return false;
                }
            }
            else
            {
                return false;
            }
           
        }

        protected void txtgvdVal_TextChanged1(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
            int rowIndex = row.RowIndex;

            ((TextBox)this.gvNomineeForm.Rows[rowIndex].FindControl("txtgvdVal")).Visible = false;
            DateTime birthDate = Convert.ToDateTime(((TextBox)this.gvNomineeForm.Rows[rowIndex].FindControl("txtgvdVal")).Text.ToString());
            DateTime todDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));
            int age = ASTUtility.DatediffYear(todDate, birthDate);
            ((TextBox)this.gvNomineeForm.Rows[rowIndex].FindControl("txtgvVal")).Visible = true;
            ((TextBox)this.gvNomineeForm.Rows[rowIndex].FindControl("txtgvVal")).Text = Convert.ToString(age);           
        }

        protected void ddlInst_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Not Updated
            int rowIndex = ((GridViewRow)((DropDownList)sender).NamingContainer).RowIndex;

            string instcode = ((DropDownList)this.gvDegree.Rows[rowIndex].FindControl("ddlInst")).SelectedValue.ToString();
            switch (instcode)
            {
                case "31999":
                    ((DropDownList)this.gvDegree.Rows[rowIndex].FindControl("ddlInst")).Items.Clear();
                    ((DropDownList)this.gvDegree.Rows[rowIndex].FindControl("ddlInst")).Visible = false;
                    ((TextBox)this.gvDegree.Rows[rowIndex].FindControl("txtgvVal")).Visible = true;
                    ((TextBox)this.gvDegree.Rows[rowIndex].FindControl("txtgvVal")).Text = "";
                    break;
            }
        }
    }
}