using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB
{
    public partial class LetterDefault : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CommonButton();
                var type = this.Request.QueryString["Entry"].ToString().Trim();
                string type1 = this.Request.QueryString["Type"].ToString().Trim();
                ((Panel)this.Master.FindControl("pnlPrint")).Visible = false;

                this.GetWorkStation();
                this.GetAllOrganogramList();


                //if (type == "Apprv" || type == "HR")
                //{
                //    this.panl1.Visible = false;
                //    this.btnsave.Visible = false;
                //    this.btnapprv.Visible = false;
                //    this.ShowLetter();
                //}
               // this.ShowLetter();
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = date;
                if (type1 == "10003" || type1 == "10004" || type1 == "10005" || type1 == "10020" || type1 == "10002" || type1 == "94023" || type1 == "10013")
                {
                    this.GetSelected();
                    this.ddlCat.Visible = true;
                    this.lblcat.Visible = true;
                }

                else
                {
                    this.GetEmployee();
                    this.ddlCat.Visible = false;
                    this.lblcat.Visible = false;
                }

                this.GetLettPattern();

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Apprv") ? "Appiontment Letter Create"
                 : (this.Request.QueryString["Type"].ToString().Trim() == "10005&Entry") ? "Appiontment Letter Approved"
                 : "Joining Letter Create";


                //string titale = this.Request.QueryString["Entry"].ToString().Trim();
                //((Label)this.Master.FindControl("lblTitle")).Text = titale;
                ddlEmployee_SelectedIndexChanged(null, null);

                ((Label)this.Master.FindControl("lblTitle")).Text = "Appiontment Letter Create";


            }
        }



        private void CommonButton()
        {

            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;



            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;


            string Type = this.Request.QueryString["Entry"].ToString();
            if (Type == "Apprv")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text = "Approve";
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = true;

            }


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(btnsave_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Click += new EventHandler(btnapprv_Click);

            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnSave_Click);


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
            string qtype = this.Request.QueryString["Type"].ToString().Trim().Trim();

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");

            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            if (qtype == "Overtime")
            {
                this.ddlWstation.SelectedValue = "940300000000";
                this.ddlWstation.Enabled = false;

            }

            else if (qtype == "CarSub" || qtype == "Mobile")
            {
                this.ddlWstation.SelectedValue = "940100000000";
                this.ddlWstation.Enabled = false;
            }

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

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Entry"].ToString();

            switch (Type)
            {
                case "Apprv":
                    this.mgtprint();
                    break;

                default:
                    this.hrPrint();
                    break;
            }




        }

        private void mgtprint()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string comcod = GetCompCode();

            //string msg = this.txtml.Text;



            //DataTable dt1 = (DataTable)ViewState["letter"];



            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //LocalReport Rpt1 = new LocalReport();
            //string img = string.Empty;
            //try
            //{
            //    if (!(dt1.Rows[0]["EMPSIGN"] is DBNull))
            //        img = Convert.
            //        ((byte[])dt1.Rows[0]["EMPSIGN"]);
            //}
            //catch (Exception)
            //{
            //    img = string.Empty;

            //}

            //Rpt1 = RptHRSetup.GetLocalReport("RD_81_Hrm.LetterDefault01", null, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("rpetext", msg));
            //Rpt1.SetParameters(new ReportParameter("ComImg", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("comName", comnam));
            //Rpt1.SetParameters(new ReportParameter("Comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("ApprvDesig", (string)dt1.Rows[0]["apprvdesig"]));
            //Rpt1.SetParameters(new ReportParameter("ApprvName", (string)dt1.Rows[0]["apprvname"]));
            //Rpt1.SetParameters(new ReportParameter("Apprasign", img));
            //Session["Report1"] = Rpt1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('RDLCViewerWin.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void hrPrint()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string usrid = hst["usrid"].ToString();
            //string username = hst["username"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string comcod = GetCompCode();

            //string msg = this.txtml.Text;

            //DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "SHOWUSERSIGN", usrid, "", "", "", "");

            //DataTable dt1 = ds3.Tables[0];
            //// DataTable dt1 = (DataTable)ViewState["letter"];



            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //LocalReport Rpt1 = new LocalReport();
            //string img = string.Empty;
            //try
            //{
            //    if (!(dt1.Rows[0]["empsign"] is DBNull))
            //        img = Convert.ToBase64String((byte[])dt1.Rows[0]["empsign"]);
            //    //img = string.Empty;
            //}
            //catch (Exception)
            //{
            //    img = string.Empty;

            //}

            //Rpt1 = RptHRSetup.GetLocalReport("RD_81_Hrm.LetterDefault01", null, null, null);
            //Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("rpetext", msg));
            //Rpt1.SetParameters(new ReportParameter("ComImg", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("comName", comnam));
            //Rpt1.SetParameters(new ReportParameter("Comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("ApprvDesig", (string)dt1.Rows[0]["desig"]));
            //Rpt1.SetParameters(new ReportParameter("ApprvName", (string)dt1.Rows[0]["empname"]));
            //Rpt1.SetParameters(new ReportParameter("Apprasign", img));
            //Session["Report1"] = Rpt1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('RDLCViewerWin.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string user = hst["usrid"].ToString();
            string cat1 = "";
            string type1 = this.Request.QueryString["Type"].ToString().Trim();
            if (type1 == "10003" || type1 == "10004" || type1 == "10005" || type1 == "10020")
            {
                cat1 = this.ddlCat.SelectedValue;
            }
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            var empid = this.ddlEmployee.SelectedValue.ToString();
            string TxtRef = this.TxtRef.Text;
            var strval = this.txtml.Text;
            //DataTable dt = new DataTable();
            //dt.Columns.Add("lettdesc");
            //dt.Rows.Add(strval);

            //DataSet ds1 = new DataSet("ds1");
            //DataView dv1 = new DataView(dt);
            //ds1.Tables.Add(dv1.ToTable());
            //ds1.Tables[0].TableName = "tbl1";



            // var strval3 = Encoding.ASCII.GetBytes(strval);
            var type = this.Request.QueryString["Type"].ToString().Trim();
            var date = this.txttodate.Text;
            string comcod = this.GetCompCode();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPLOADLETTER", empid, type, strval, date, cat1, user, TxtRef, "", "");
            if (ds3.Tables[0].Rows.Count > 0 || ds3 != null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Save Successfully";

            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Fail";
            }


            if (type1 == "10020")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Send Data Bank";
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;
                //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false; 
            }



        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.txtml.Text = "";
            if (chkpre.Checked)
            {
                this.PreviousD();
            }
            else
            {
                this.ShowView();
            }
        }

        protected void btnapprv_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string user = hst["usrid"].ToString();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            var strval = this.txtml.Text;
            // var strval3 = Encoding.ASCII.GetBytes(strval);
            var type = this.Request.QueryString["Type"].ToString().Trim();
            var empid = this.Request.QueryString["ID"].ToString().Trim();
            var date = this.txttodate.Text;
            string comcod = this.GetCompCode();
            DataSet result = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "APPROVELETTER", empid, type, user, "", "", "", "", "", "");
            ((Label)this.Master.FindControl("lblmsg")).Text = "Approve Successfully";
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

        }

        private void ShowLetter()
        {



            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetCompCode();
            string empid = this.Request.QueryString["ID"].ToString().Trim();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTER", empid, type, "", "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            string lett = (string)ds3.Tables[0].Rows[0]["LETTDESC"];
            this.txtml.Text = lett;

            ViewState["letter"] = ds3.Tables[0];
        }

        private void GetEmployee()
        {
            //Session.Remove("empinfo");
            //string comcod = this.GetComeCode();
            //string txtSProject = (this.Request.QueryString["empid"] != "") ? "%" + this.Request.QueryString["empid"].ToString() + "%" : "%";
            //string qempid = this.Request.QueryString["empid"] ?? "";


            //if (qempid.Length > 0)
            //{
            //    string emptype = "94" + qempid.Substring(2, 2) + string.Concat(Enumerable.Repeat("0", 8));

            //    this.ddlWstation.SelectedValue = emptype;
            //    //  this.ddlWstation_SelectedIndexChanged(null, null);



            //}


            //string empType = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            //string divison = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            //string deptcode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            //string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";



            //string NewEmp = "New" ;
            //string InactiveEmp = "0";


            //DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", txtSProject, deptcode, section, NewEmp, empType, divison, InactiveEmp, "", "");
            //this.ddlEmployee.DataTextField = "empname";
            ////this.ddlEmpName.SelectedValue = "empname";
            //this.ddlEmployee.DataValueField = "empid";
            //this.ddlEmployee.DataSource = ds3.Tables[0];
            //this.ddlEmployee.DataBind();
            //Session["empinfo"] = ds3.Tables[0];
            //ds3.Dispose();

            //this.ddlEmployee.SelectedValue = (this.Request.QueryString["empid"] == "") ? this.ddlEmployee.Items[0].Value : this.Request.QueryString["empid"].ToString(); ;



            string comcod = this.GetComeCode();
            string company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string deptcode = (ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string dtcode = (ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : ddlSection.SelectedValue.ToString() + "%";
            string txtSEmployee = "%%";
            //DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEMPNAME", txtSProject, "", "", "", "", "", "", "", "");

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEMPNAME", company, dtcode, section, deptcode, txtSEmployee, "", "", "");
            if (ds3 == null)
            {

                return;
            }


            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds3.Tables[0];
            this.ddlEmployee.DataBind();
            ds3.Dispose();
            ViewState["empinfo"] = ds3;
        }

        private void GetSelected()
        {
            string comcod = this.GetCompCode();

            string qtype = this.Request.QueryString["Type"].ToString();
            string callType = "";
            if (qtype == "10002" || qtype == "94023")
            {
                callType = "GETCANENAME"; ///Appoinment Latter

            }
            else if (qtype == "10005" || qtype == "94123")
            {
                callType = "GETJOINEMPLIST";
                this.pnlref.Visible = false;
            }
            else if (qtype == "10013" || qtype == "10020")
            {
                callType = "GETCONFIRMEMP";
            }


            else
            {
                callType = "GETCANENAME";

            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", callType, qtype, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlEmployee.Items.Clear();
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds1.Tables[0];
            this.ddlEmployee.DataBind();



            this.ddlCat.DataTextField = "section";
            this.ddlCat.DataValueField = "pactcode";
            this.ddlCat.DataSource = ds1.Tables[0];
            // this.ddlCat.Enabled = false;
            this.ddlCat.DataBind();


            ViewState["empinfo"] = ds1;
        }

        protected void GetLettPattern()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            this.data(type);

        }

        protected void imgbtnEmployee_Click(object sender, EventArgs e)
        {
            string type1 = this.Request.QueryString["Type"].ToString().Trim();
            if (type1 == "10003" || type1 == "10004" || type1 == "10005" || type1 == "10020" || type1 == "10002" || type1 == "94123")
            {
                this.GetSelected();
            }

            else
            {
                this.GetEmployee();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            this.txtml.Text = this.data(type);


        }

        private void PreviousD()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetCompCode();
            string empid = this.ddlPrevious.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTER", empid, type, "", "", "", "", "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
                return;
            this.ddlEmployee.Items.Clear();
            this.ddlEmployee.DataTextField = "empname";
            this.ddlEmployee.DataValueField = "empid";
            this.ddlEmployee.DataSource = ds3.Tables[0];
            this.ddlEmployee.DataBind();

            string lett = (string)ds3.Tables[0].Rows[0]["LETTDESC"];
            this.txtml.Text = lett;
            ViewState["letter"] = ds3.Tables[0];
        }

        protected string data(string type01)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string cdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string compname = hst["compname"].ToString();
            string usrid = hst["usrid"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = GetCompCode();

            string refissueno = "Ref.: " + this.refTxt.Text.Trim() + this.TxtRef.Text.Trim();
            string tdate = this.txttodate.Text.ToString();

            //created by nahid for dynamic html value
            //this.LblGrpCompany.Text = ((DataTable)Session["tbllog1"]).Rows[0]["comnam"].ToString();
            string comaddress = (((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(0, 6) == "<br />") ? ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString().Substring(6) : ((DataTable)Session["tbllog1"]).Rows[0]["comadd"].ToString();

            string imgpge = "~/Image/" + "LOGO" + ((DataTable)Session["tbllog1"]).Rows[0]["comcod"].ToString() + ".PNG";
            byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath(imgpge));
            string complogo = Convert.ToBase64String(imageArray);



            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "SHOWUSERSIGN", usrid, "", "", "", "");
            DataTable dt1 = ds3.Tables[0];
            string empid = this.ddlEmployee.SelectedValue.ToString();

            string type1 = this.Request.QueryString["Type"].ToString().Trim();

            string calltype = (Request.QueryString["Type"].ToString() == "10002") ? "GETEMPSALINFOAPP" :
                Request.QueryString["Type"].ToString() == "10015" ? "GETEDISIONEXTSALINFO" :
                Request.QueryString["Type"].ToString() == "94023" ? "GETEMPSALINFOAPP"
                : "GETEMPSALINFO";

            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", calltype, empid, tdate, "", "", "", "", "", "", "");

            if (ds5 == null)
            {

                return "";
            }

            DataTable dtsalery = ds5.Tables[0];
            //DataView dvr = new DataView();
            //DataTable dtr1 = new DataTable();
            //dtr1 = dtsalery;
            //dvr = dtr1.DefaultView;
            //dvr.RowFilter = ("gcod like '040%'");
            //dtr1 = dvr.ToTable();
            //DataTable dtsaldtsal = dtr1;

            double SalAdd = 0;
            string ttlsalary = "";
            string inwords = "";

            ////Building an HTML string.
            StringBuilder html = new StringBuilder();
            ////Table start.
            //html.Append("<table style='width:400px' border = '1'>");
            ////Building the Header row.
            //html.Append("<tr><th style='width:100px;font-weight:bold'>Desctiption</th><th style='width:100px;font-weight:bold'>Value</th></tr>");
            ////Building the Data rows.
            //foreach (DataRow dr1 in dtsal.Rows)
            //{
            //    html.Append("<tr>");
            //    html.Append("<td style='width:100px;'>" + dr1["gdesc"].ToString() + "</td>" + "<td style='width:80px; text-align:right'>" + Convert.ToDouble(dr1["gval"]).ToString("#,##0.00;(#,##0.00); ") + "</td>");
            //    html.Append("</tr>");
            //}
            //html.Append("<tr><th style='width:100px;font-weight:bold'>Total</th><th style='width:100px;font-weight:bold; text-align:right'>" + ttlsalary + "</th></tr>");
            ////Table end.
            //html.Append("</table>");
            string tablesale = html.ToString();

            DataSet dtempinf = (DataSet)ViewState["empinfo"];
            DataView dv = dtempinf.Tables[0].DefaultView;
            dv.RowFilter = ("empid='" + empid + "'");
            DataTable dtempinf_ = dv.ToTable();
            string jdate = "", idCard = "";


            if (type1 == "10015" || type1 == "10012")
            {
                jdate = dtempinf_.Rows.Count > 0 ? Convert.ToDateTime(dtempinf_.Rows[0]["jdate"]).ToString("dd-MMM-yyy") : "01-Jan-1900";
                idCard = dtempinf_.Rows.Count > 0 ? dtempinf_.Rows[0]["idcard"].ToString() : "";


            }

            string lbody = string.Empty;
            // string empid=hst["empid"].ToString();
            string name = (string)ViewState["name"];
            string Desig = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["desig"].ToString();//(string)ViewState["desig"];
            string depart = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["dptdesc"].ToString();//(string)ViewState["section"];
            string dptdesc = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["section"].ToString();//(string)ViewState["section"];        
            string grddesc = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["desig"].ToString();//(string)ViewState["desig"];
            string usrdesig = (dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["desig"].ToString();
            string usersign = (dt1.Rows.Count == 0) ? "" : Convert.ToBase64String((byte[])dt1.Rows[0]["empsign"]);
            string uname = (dt1.Rows.Count == 0) ? "" : dt1.Rows[0]["empname"].ToString();
            //string refno = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["refno"].ToString();//(string)ViewState["desig"];
            string refno = (dtempinf_.Rows.Count == 0) ? "" : dtempinf_.Rows[0]["secid"].ToString();//(string)ViewState["desig"];

            string todate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string appdate = (dtempinf_.Rows.Count == 0) ? "" : Convert.ToDateTime(dtempinf_.Rows[0]["appdate"]).ToString("dd-MMM-yyyy");//(string)ViewState["desig"];
            string TxtRef = (Request.QueryString["Type"].ToString() == "10005") ? refno : this.TxtRef.Text; ;



            switch (type01)
            {
                //<img class='Companylogo' src='Image/LOGO8701.PNG' />";
                case "10001":
                    lbody = "<p><strong>Ref: SPL/HR/Appt/16/524</strong></p><p><strong>Nov 20, 2016</strong></p><p><strong>Abdullah Al Noman</strong></p><p><strong>C/O: Abdul Bashar&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>Vill: Boro Hossainput, PO: Banglabazar-3822,</strong></p><p><strong>PS: Begumganj, Dist: Noakhali&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p>&nbsp;</p><p><strong>Subject: <u>Letter of Appointment.</u></strong></p><p>&nbsp;</p><p>Dear <strong>Mr. Abdullah</strong>,</p><p>&nbsp;With reference to your application and subsequent interview with us, we have the pleasure to inform you that the Management of Star Paradise Ltd is pleased to appoint you as <strong>&ldquo;Field Officer&rdquo;</strong> in our Company <strong>Star Paradise Ltd </strong>on the following terms and conditions:</p><p>&nbsp;</p><p><strong>Commencement and nature of Appointment:</strong></p><ol><li>Your appointment is effective from <strong>Nov 20, 2016</strong>.</li><li>Your appointment is initially on probation basis for 6 (Six) months from the date of your joining. On satisfactory completion of your probationary period, your service may be confirmed. Otherwise, your probationary period may be extended for such a period as may be decided upon by the Management.</li><li>Your place of posting shall be at <strong>'Comilla &ndash; Chandpur&rdquo;</strong> and you shall work under the supervision of <strong>Divisional Head</strong>.</li></ol><p>&nbsp;</p><ol start='2'><li><strong>Placement, Compensation and other benefits:</strong></li><li>You will be entitled to Festival Bonuses as per Company policy, which are two (02) Eid Bonus, 100% of your Gross Salary each.</li><li>Your monthly salary as in &ldquo;Annexure-A&rdquo; only, which includes all your perquisites and allowances. Compensation will be governed by the rules of the Company on the subject, as applicable and / or amended hereafter.</li><li>Personal Income Tax, if any will be on your account and will be deducted each month by the company at source at the time of monthly salary disbursement for onward submission to the relevant Income Tax authorities.</li><li>You are not to disclose / discuss your salary with anyone related to this organization and keep it strictly confidential.</li><li>This is a position of full time and continuous responsibilities and will not engage yourself any Part-time work, profession or employment without written permission from the management.</li><li>You will be entitled with other benefits of the organization time to time as per the Company policy.</li></ol><p>&nbsp;</p><ol start='3'><li><strong>Duties and responsibilities:</strong></li><li>You will carry on with the duties and responsibilities entrusted to you and also the duties and responsibilities that may be entrusted to you by the Management from time to time. You will require working late hours whenever necessary for the greater interest of the organization.</li><li>You have to abide by all instructions and orders issued by the management in good spirit.</li><li>You will retire attaining the age limit fixed by the Bangladesh Govt. through Bangladesh Labor Act.</li></ol><p>&nbsp;</p><ol start='4'><li><strong>Transfer:</strong></li><li>Your Service is transferable from one project to another project of the Company for the greater interest of the Organization.</li><li>The Management may change your designation, duties and responsibilities from time to time as they think fit and proper without disturbing salary and allowances.</li></ol><p>&nbsp;</p><ol start='5'><li><strong>Termination of Service:</strong></li><li>The Management reserves the right to terminate your service at any time without assigning any reason, if your work, attitude or behavior not found satisfactory.</li><li>Either party may however, terminate the contract of employment by giving a notice period of 60 (Sixty) days in writing or in lieu thereof an equivalent of two months&rsquo; basic salary, will have to be paid by the company / surrendered by you in case of failure in giving two months&rsquo; prior notice after confirmation of service.</li><li>During the probation period, either party may terminate the contract of employment with 30 (Thirty) days prior notice.</li><li>When you intend to resign you will have to handover official charges to the nominated person of the Company.</li></ol><p>&nbsp;</p><ol start='6'><li><strong>Confidentiality:</strong></li></ol><p>You shall not, at any time, during the continuance or even after the cessation of your employment hereunder, disclose or divulge or make public, except on legal obligations, either directly to any person, firm or company or use for yourself any trade secret or confidential, technical knowledge, formula, process, compositions, ideas or documents, concerning the business and affairs of the company or any of its dealings, transactions or affairs which you may have acquired from the company or have to your knowledge during the course of and incidental to your employment. If you disclose any such information to any other person(s) or organization, the Company shall prosecute against you for such breach of code of conducts, as it considers necessary to protect its interest and enforce its rights.</p><p>&nbsp;</p><h2>Annexure &ndash; A</h2><p>&nbsp;</p><p><strong>Dear Mr. Abdullah,</strong></p><p>&nbsp;</p><p>You shall be placed at <strong>Grade-2</strong>, the monthly Gross salary of <strong>Tk. 6,000.00 (Six Thousand)</strong> only which is broken down as follows:</p><p>&nbsp;</p><p>&nbsp;<strong><u>Particulars &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; In Taka</u></strong></p><p><strong><u>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 3,600.00&nbsp;&nbsp;</u></strong></p><p><strong><u>House Rent &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;1,800.00</u></strong></p><p><strong><u>Conveyance Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;330.00</u></strong></p><p><strong><u>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;270.00</u></strong></p><p><strong><u>Total Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;6,000.00 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;(Six Thousand)</u></strong></p><p>&nbsp;</p><p>If you are agreeable to the aforesaid offer, please acknowledge this letter by way of formal acceptance and return immediately for further action.</p><p>&nbsp;We have the pleasure in welcoming you and sincerely hope that our company will get benefited from your service.</p><p>&nbsp;</p><p>For <strong>Star Paradise Ltd</strong>,</p><p>&nbsp;</p><p>&nbsp;<strong>(Moshiur Hossain Uday)</strong></p><p><strong>Managing Director</strong></p><p>&nbsp;</p><p>I<strong>, Abdullah Al Noman</strong> have fully understood the contents of the letter of appointment and willingly agree to abide by the terms and conditions as stipulated herein above.</p><p>&nbsp;</p><p>&nbsp;</p><p>______________________</p><p>Signature of the Employee</p><p>&nbsp;</p><p>Date: __________________</p><p><strong>&nbsp;</strong></p><p><strong>Copy to:</strong></p><ol><li>HRIS</li><li>Personal File</li></ol>";
                    break;
                //appoinment letter for factory    
                case "10002":
                    lbody = "<div class='letbody'><div class='pntHeadJon'><p><p><p>Ref: " + TxtRef + "</p><p><strong> " + todate + " </strong></p><p><strong>" + name + "</strong></p><p><strong>C/O: Md. Abdul Muttalib&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>Vill: Gibanda ( Sharkar bar), PO: Gibanda</strong></p><p><strong>PS: Islampur, Dist: Jamalpur&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p><strong>&nbsp;</strong></p></div><p><strong>Subject: <u>Letter of appointment as <strong> " + Desig + "</strong>.</u></strong></p><p>&nbsp;</p><p>Dear <strong>" + name + "</strong>,</p><p>With reference to your application and subsequent interview with us, the Management is pleased to appoint you as <strong>  " + Desig + "</strong> in <strong>" + grddesc + "</strong> in <strong> " + comnam + " </strong>. from 01 Sep 2018 on the following terms and conditions:- </p><p>&nbsp;</p><ol><li>You will be paid a salary of <strong> " + ttlsalary + "/-" + inwords + "</strong> per month. Your salary and allowances breakdown will be as follows:<p>" + tablesale + "</p></li><li> On satisfactory completion of the probation period of 06 (Six) months, your service will be confirmed based on performance evaluation by your supervisor and/or Division Head and your probable confirmation date would be 01 March, 2016. </li><li>Your service will be governed by the service rules and regulations of the company which may be changed or modified time to time and you will be abide by the rules and regulations.</li><li>Your service may be transferred at any place within our business territory at any time or any other unit of the group or anywhere in Bangladesh.</li><li>You will report to the Head of Footwear of the company and your job assignments will be related with all functions as assigned to you time to time by your supervisor or any other person designated by the authority.</li><li>You must maintain secrecy in respect of all company matters and policies that you may come to learn during your employment in the company.</li><li>You are not allowed to work/involve in personal business or in any other organization without written approval from the authority. </li><li>The authority will not be responsible for any of your illegal works and activities in and outside of the office/Company. The authority will not also be responsible for any objection and / or dues arise by any of your previous company.<p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p></p><p></p><p></p><p></p><p></p></p><p></p><p></p><p></p><p></p></li><li>If it is found/proved your involvement in any illegal works or activities against the law of the country, termination will become effective without making further communication. Such termination will also become effective if it is found continuous absenteeism for more than 10 days without any approval from respective supervisor or HR Division.</li><li>The authority may require offering job separation if you found no longer suitable for your position or any other position in the company considering the business competence & employee’s competence and adaption or if the job responsibility performed by the employee does not match anymore with the Company’s adapted system/technology/function/strategy to achieve the long term strategic objective. The notice period of such job separation for the permanent employees may be 02 (two) months.</li><li>The authority reserves the right to terminate your job at any time without showing any reason if you are found with gross violation of the rules and regulations of the organization, violation of Business Conduct Guideline, misconduct and other undisciplined activities. For such job separation, you will be entitled to a 7 days’ notice period during your probation period, or you have to give 7 days’ notice to the company if you want to resign from the service. After confirmation, this notice period will be two (02) months for both the parties.</li><li>Candidate has to contact with HR Division if he/she wants to postpone the date of joining as mention in the letter and submit an application for the approval mentioning the causes for delay joining. This appointment letter will be valid for maximum 7 (Seven) days upon from the given date of joining. </li></ol><p>&nbsp;</p>if the offer as per above terms and conditions are acceptable to you, please sign the copy of this letter as a token of your acceptance and accordingly report for your duty to the HR at Corporate Office. Welcome to the EDISON Footwear and wishing you a successful career with us.</p><p>&nbsp;</p><p>Your truly,</p><p>&nbsp;</p><p>FOR EDISON FOOTWEAR Ltd.</p><table border='0' style='border:none;'><tr><td><p></p><p></p><p></p><p>------------------------</p>  <p class='pUname'><strong>Nafiz Alam</strong></p> <p class='pUname'><strong>Deputy General Manager, HR</strong></p></td><td> <p></p><p></p><p></p><p>------------------------</p><p class='pUname'><strong> Md. Nuruzzaman</p> <p class='pUname'><strong> Head Of Business</strong></p></td></tr></table><table><tr><td style='width:50%'><p><strong>Acceptance:</strong></p><p>(I have read and fully agreed with all the terms and conditions of the employment. I will be bound to join the company on the mentioned date failure of what may cause the company to go for legal action.) <p></td><td style='width:50%; text-align:center'><p><strong>Signature and Date:</strong></p><p></p><p></p><p></p></td></tr></table><p><strong>Copy to:</strong></p><ol><li>Personal File</li></ol></div>";
                    break;
                //Joining Letter for factory    
                case "10005":
                    lbody = "<div class='printHeader pntHeadJon'><p><br><br><br>Date: " + cdate + "</p><p><strong> Mr.Md.Nafiz Alam </strong></p><p><strong>Deputy General Manager</p><p>Human Resources Department</p><p>Edison Footwear Ltd.</p><p>Rangs Babylonia (level 6)</p><p>246, Bir Uttam Mir Shawkat Road, </p><p>Tejgaon, Dhaka 1208.</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></p><p>&nbsp;</p><p><strong class='subj'>Subject: <u>Joining Letter</u></strong></p><p>&nbsp;</p> <p></br></br></br><br></br></br></br></p><div class='letbody'><p>Dear <strong>Sir</strong>,</p><p>I have honour to submit my joining report for the post of " + Desig + " in <stron> " + dptdesc + " </strong> department today the <stron>" + todate + " </strong> in respect to your appointment letter dated " + appdate + " Ref# " + TxtRef + ".</p>Yours sincerely,<p><br><br><br><br><br><br><p class='pImage'></p>  <p class='pUname'><strong>" + name + "</p> <p class='pUname'><strong>" + Desig + "</strong></p><p><br><br><br><br><br><br> Full Name in Capital Letters</p></div>";
                    break;



                //////offer later for sales department;
                ////case "10003":
                ////    lbody = "<p><strong>SPL/HR/Ofr/16/586</strong></p><p><strong>" + name + "</strong></p><p><strong>Vill: Nalua, PO: Afalkait</strong></p><p><strong>PS: Bekergang, Dist: Barisal</strong></p><p><strong>Sub: Offer for Employment</strong></p><p>Dear <strong>" + name + "</strong></p><p>With reference to discussions with you and your willingness to join our company, we are pleased to offer you appointment as <strong> " + Desig + "</strong> in <strong>" + comnam + "</strong> at <strong>" + dptdesc + ", " + depart + " </strong> with effect from <strong> " + cdate +" </strong>.</p><p>&nbsp;The Letter of Appointment will be issued soon.</p><p>&nbsp;</p><p>&nbsp;Please bring the following papers on the date of joining:</p><ol><li>Photocopy of all academic certificates</li><li>Release letter / Letter of acceptance of resignation in the Company Letter Head from the previous employer</li><li>Passport size photograph -3</li><li>Photocopy of passport /Photocopy of National ID Card</li><li>Salary Certificate/ Bank Statement</li><li>300 (100*3) Tk Stamp Paper</li></ol><p>&nbsp;</p><p>&nbsp;</p><p>Yours Sincerely,</p><p class='pImage'><strong><img src='data:Image/png;base64," + usersign + "' width='200px' height='80px' ></img></p>  <p class='pUname'><strong>" + "Moshiur Hossain Uday" + "</p> <p class='pUname'><strong>" + "Managing Director" + "</strong></p>";

                ////    break;
                //////offer Later For general
                ////case "10004":
                ////    lbody = "<p><strong>SPL/HR/Ofr/16/559</strong></p><p><strong>" + name + "</strong></p><p><strong>Vill: West Vashanchar, PO: Ambikapur</strong></p><p><strong>PS: Faridpur, Dist: Faridpur</strong></p><p>&nbsp;</p><p>&nbsp;Dear <strong>" + name + "</strong></p><p>&nbsp;</p><p>&nbsp;<span style='text-decoration: underline;'><strong>Offer for Employment</strong></span></p><p>&nbsp;With reference to discussions with you and your willingness to join our company, we are pleased to offer you appointment as <strong>&ldquo;Plant Manager-Pole Project&rdquo; </strong>in <strong>" + comnam + "</strong>at <strong>Factory </strong>with effect from <strong>1 January,</strong><strong> 2017</strong></p><p>The Letter of Appointment will be issued soon.</p><p>&nbsp;</p><p>Please bring the following papers on the date of joining:</p><ol><li>Photocopy of all academic certificates</li><li>Release letter / Letter of acceptance of resignation in the Company Letter Head from the previous employer</li><li>Passport size photograph -3</li><li>Photocopy of passport /Photocopy of National ID Card</li><li>Salary Certificate/ Bank Statement</li></ol><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp; &nbsp; Yours Sincerely,</p>"; //<p>&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;</p><p>&nbsp;<strong>Moshiur Hossain Uday</strong></p><p><strong>&nbsp;Managing Director</strong></p>";
                ////    break;

                ////// extension letter
                ////case "10006":
                ////    lbody = "<p><strong>Ref. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; : SPL/HR/Exten/511/15</strong></p><p>Date &nbsp;&nbsp;&nbsp;&nbsp; : December 1, 2016</p><p>&nbsp;<strong>" + name + "</strong></p><p><strong>Staff ID # 124</strong></p><p>" + Desig + "</p><p>" + depart + "</p><p>&nbsp;Subject: <strong>Unsatisfactory Performance during Probationary Period.</strong></p><p>&nbsp;</p><p>Dear Mr. <strong>" + name + "</strong>,</p><p>&nbsp;Please be informed that after a careful review of your performance during probation period, it is found that you have failed to show the satisfactory performance, which is required for your position. Due to your unsatisfactory performance, we are unable to confirm you after six months&rsquo; probation period.</p><p>Your supervisor has suggested areas for improvement in your probationary assessment sheet, and it is expected that you will seek every opportunity to improve the highlighted areas.Therefore, through this letter you are placed under observation for <strong>3 months</strong> beginning from, <strong>01.11.2016.</strong> After completion of the observation period, your performance will re-evaluated by your supervisor. If you fail to show satisfactory performance during that period, your service may be no longer required for the organization.</p><p>&nbsp;</p><p>Thank you and hope you will make every efforts to improve yourself within <strong>next three months</strong>.</p><p>&nbsp;</p><p><strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p>&nbsp;CC:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; HRIS</p><p>Personal File&nbsp;</p>";
                ////    break;
                //////promotion letter
                ////case "10007":
                ////    lbody = "<p style='text-align: center;'>&nbsp;</p><h3 style='text-align: center;'><span style='text-decoration: underline;'><strong>Private &amp; Confidential</strong></span></h3><p>&nbsp;<strong>Ref: SPL/HR/Prom/489/16&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong></p><p><strong>16 November, 2016</strong></p><p><strong>&nbsp;</strong><strong>" + name + ", ID: 278</strong></p><p>" + Desig + ",</p><p>" + depart + "</p><p>&nbsp;<strong>Subject: Promotion</strong></p><p>&nbsp;Dear Mr. <strong>" + name + "</strong>,</p><p>&nbsp;We are pleased to inform you that, the company have decided to promote you to the position of <strong><u>Junior Territory Sales Manager</u></strong> recognition of your performance, effective December 1, 2016.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 360px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 360px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 360px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 360px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 360px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>We acknowledge your excellent performance and congratulate you on your well-deserved promotion. We hope you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;<strong>Moshiur Hossain</strong></p><p><strong>Managing Director.</strong></p><p><strong><u>Copy to:</u></strong></p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HRIS</p><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Personal File</p>";
                ////    break;
                //////increment letter
                ////case "10008":
                ////    lbody = "<p style='text-align: center;'>&nbsp;</p><p style='padding-left: 360px;'>&nbsp;</p><h3 style='text-align: center;'><span style='text-decoration: underline;'><strong>&nbsp;</strong><strong>Private &amp; Confidential</strong></span></h3><p>&nbsp;</p><p><strong>REF: SPL/HR/INCREMENT/16</strong></p><p><strong>Date: July 12, 2016</strong></p><p>&nbsp;<strong>" + name + ", ID: 101</strong></p><p><strong>" + Desig + ",</strong></p><p>" + depart + "</p><p><strong>Factory.</strong></p><p>&nbsp;</p><p><strong>Subject: Increment of Salary</strong></p><p>&nbsp;</p><p><strong>Dear " + name + ",</strong></p><p>&nbsp;We are pleased to inform you that the management has decided to review your monthly gross salary in recognition of your performance during the year 2015-2016, effective from <strong>July 01, 2016</strong>.</p><p>&nbsp;In view of the decision the breakdown of your revised monthly salary stands as follows:</p><p style='padding-left: 330px;'>Basic Salary &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;7,2000.00&nbsp; &nbsp; &nbsp;</p><p style='padding-left: 330px;'>House Rent Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;3,600.00</p><p style='padding-left: 330px;'>Transport Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 660.00</p><p style='padding-left: 330px;'>Medical Allowance &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 540.00</p><p style='padding-left: 330px;'><strong>Total: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;TK &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;12,000.00</strong></p><p>&nbsp;</p><p>&nbsp;We acknowledge your good performance and hope that you will continue to contribute to the growth and success of the organization in future.</p><p>&nbsp;We wish you all the best and look forward to better performance in future.</p><p>&nbsp;</p><p>With best regards</p>";//<p>&nbsp;<strong>Moshiur Hossain Uday</strong></p><p>Managing Director</p><p>&nbsp;CC: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>Personal File</p><p>HRIS</p><p>&nbsp;</p>";
                ////    break;
                //////transfer lettre
                ////case "10009":
                ////    lbody = "<p>&nbsp;<strong>Ref: SPL/HR/TL/558/16</strong></p><p><strong>December 10, 2016</strong></p><p><strong>Mr.</strong> <strong>" + name + "</strong> <strong>(ID # 202)</strong></p><p><strong>TSM,</strong><strong>Dhaka South</strong></p><p>&nbsp;</p><p>&nbsp;Subject: <strong><u>Transfer of Service </u></strong></p><p><br /> <strong>Dear Mr. </strong><strong>" + name + "</strong>,</p><p>&nbsp;In consideration of the exigencies of the Company, it has been decided to transfer you from <strong>Dhaka South (Munshigonj) to Faridpur (Barishal) </strong>effective from <strong>20 Dec, 2016</strong>.</p><p>&nbsp;Please note that the terms and conditions of your service shall not be changed due to this transfer.</p><p>&nbsp;It is expected that you will continue to provide your best services in achieving business goals and objectives of the Company in the days to come.</p><p>&nbsp;Wish you a happy career in Star Paradise Limited.</p><p>&nbsp;</p><p>Yours sincerely<br /> &nbsp;</p>"; //<p>&nbsp;_______________________</p><p><strong>Ridwan Rouf Khan</strong></p><p>Assistant Manager-Human Resources</p><p>&nbsp;&nbsp; C.C</p><ul><li>HRIS</li><li>Personal File</li></ul><p>&nbsp;</p>";
                ////    break;
                //////acceptance of resignation
                ////case "10010":
                ////    lbody = "<p>July 13, 2016</p><p>&nbsp;<strong>" + name + ", ID: 106</strong></p><p>CMO,</p><p>" + Desig + "</p><p>" + depart + "</p><p>&nbsp;Subject:<strong><u> Acceptance of Resignation</u></strong></p><p>&nbsp;</p><p>Dear Mr. <strong>" + name + "</strong>,</p><p>This with reference to your letter dated <strong>July 1, 2016</strong> in which you have expressed your inability to continue your service with the organization. We would like to inform you that the management has accepted your resignation with effect from <strong>July 11, 2016.</strong></p><p>Accordingly, you will be released from your work at the close of business of <strong>July 10, 2016</strong> subject to a clearance certificate being issued to you by the concerned departments to the effect that you do not owe to <strong>Fidelity Holdings Ltd</strong> any outstanding dues and or any liabilities thereof.</p><p>You are, also, requested to submit the Identity Card and others official things to HR Department to facilitate your quick clearance from the service.</p><p>We take this opportunity to wish you well and success in all your future endeavors.</p><p>&nbsp;</p><p>&nbsp;</p><p>Sincerely,</p>"; //<p>&nbsp;<strong>Moshiur Hossain Uday</strong></p><p>&nbsp;Managing Director</p><p>&nbsp;</p><p>Copy:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; MD</p><p>HRIS</p><p>Personal File</p>";
                ////    break;
                //////release letter
                ////case "10011":
                ////    lbody = "<p><strong>June 04, 2015</strong></p><p><strong>" + name + "</strong></p><p>" + Desig + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>Accounts&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p><p>" + comnam + "</p><p><strong>&nbsp;</strong></p><p>&nbsp;</p><h3 style=" + "text-align: center;" + "><span style=" + "text-decoration: underline;" + "><strong>RELEASE LETTER</strong></span></h3><p>&nbsp;</p><p>&nbsp;<strong>Dear Mr. </strong><strong>" + name + "</strong><strong>,</strong></p><p><strong>&nbsp;</strong>With reference to, your letter dated <strong>March 05, 2015;</strong> you are hereby released from the service of <strong>" + comnam + "</strong> as at close of business on <strong>April 30, 2015</strong>.</p><p>&nbsp;We wish you all success in life.</p><p>&nbsp;</p><p>&nbsp;</p><p>Yours Sincerely,</p>";//<p>&nbsp;</p><p><strong>Ridwan Rouf Khan</strong></p><p><strong>Assistant Manager-Human Resources.</strong></p>";
                ////    break;


                //experence certificate for current employee
                case "10012":
                    lbody =
                        "<style>@page { size:8.5in 11in; margin: 2cm 4cm } @media print {.page {page -break-after: always;}}</style>" +
                        "<div>" +
                        "<header>" +
                        "<p><img  src='Image/LOGO5104.PNG' /> </p>" + "<div class='printHeader'><p><br /><br />" +
                        "<p><strong>" + refissueno + "</strong></p><p><strong></p>Date: " + cdate +
                        "</p> <br /><br /><br /><br /><br />" +
                        "</header>" +
                        "<br /><br /><br /><br /><br /><p class='cfHead'>To Whom it May Concern</p><br /><br />" +

                        "<br />" +
                        "</br></br></br><br></br></br></br>" +

                        "<p> This is to certify that <strong>" + name + "</strong>, <strong> " + Desig + " " +
                        "</strong> -  <strong>" + dptdesc + ", " + " <strong> Employee ID No. " + idCard + "" +
                        " </strong> working in " + comnam + " from <strong>" + jdate + " to till date</strong>" +
                        ".</p><p>The organization has never found any serious misconduct from his end and has no objection " +
                        "recommending him.</p>" +
                        "<br /><br /><br /><br /> <p>&nbsp;</p><p>I wish him all round success.</p>" +


                        "<br>" + "<br>" + "<br>" + "<br>" + "<br></br><br></br></br></br>" +
                        "<hr width='20%'><p class=''><strong><b>    HR & Administrator</b></p>  <p class='pUname'><strong>" +
                        uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>" +
                        "<br>" +

                        "<div style='margin:10px 0; position:fixed; bottom:0; righ:30px'>" +
                        "<p style='font-size:10px; text-align:center !important;'>This is system generated Salary Certificate </p> " +

                        "<hr border-top:1px solid black;>" +

                        "<div style='width:61%; float:left'" +
                        "<p><strong> Corporate Office:</strong></p>" +
                        "<p>Rangs Babylonia, Level 6, 246 Bir Uttam Mir Shawkat Road, (Tejgaon Link Road)</p>" +
                        "<p>Tejgaon I/A, Dhaka-1208, Bangladesh.</p>" +
                        "<p>Phone: +88 02 8878057, +88 02 8878058, www.edison-bd.com, www.edisonfootwear.com</p>" +
                        "</div>" +
                        "<div style='width:38%; float:right'" +
                        "<p><strong> Factory:</strong></p>" +
                        "<p>JL No-05 Mirzapur</p>" +
                        "<p>Gazipur Sadar, Gazipur, Bangladesh.</p>" +
                        "</div>" +

                        "</div></div>";

                    break;
                ///////Salary certificate
                case "10015":
                    if (comcod == "5301")
                    {
                        SalAdd = Convert.ToDouble((Convert.IsDBNull(dtsalery.Compute("sum(tsalary)", "")) ? 0.00 : dtsalery.Compute("sum(tsalary)", "")));
                        ttlsalary = SalAdd.ToString("#,##0.00;(#,##0.00); ");
                        inwords = ASTUtility.Trans(SalAdd, 2);
                        // Iqbal Nayan

                        double gval = Convert.ToDouble((Convert.IsDBNull(dtsalery.Compute("sum(gval)", "")) ? 0.00 : dtsalery.Compute("sum(gval)", "")));
                        double carallow = Convert.ToDouble((Convert.IsDBNull(dtsalery.Compute("sum(carallow)", "")) ? 0.00 : dtsalery.Compute("sum(carallow)", "")));
                        double suballow = Convert.ToDouble((Convert.IsDBNull(dtsalery.Compute("sum(suballow)", "")) ? 0.00 : dtsalery.Compute("sum(suballow)", "")));
                        double tdeduction = Convert.ToDouble((Convert.IsDBNull(dtsalery.Compute("sum(tdeduction)", "")) ? 0.00 : dtsalery.Compute("sum(tdeduction)", "")));
                        double pfund = Convert.ToDouble((Convert.IsDBNull(dtsalery.Compute("sum(pfund)", "")) ? 0.00 : dtsalery.Compute("sum(pfund)", "")));
                        double itax = Convert.ToDouble((Convert.IsDBNull(dtsalery.Compute("sum(itax)", "")) ? 0.00 : dtsalery.Compute("sum(itax)", "")));
                        double bankamt = Convert.ToDouble((Convert.IsDBNull(dtsalery.Compute("sum(bankamt)", "")) ? 0.00 : dtsalery.Compute("sum(bankamt)", "")));


                        // Iqbal Nayan
                        //string suballow = Convert.ToDouble(dtsalery.Rows[0]["suballow"]).ToString("#,##0.00;(#,##0.00); ");
                        //string tdeduction = Convert.ToDouble(dtsalery.Rows[0]["tdeduction"]).ToString("#,##0.00;(#,##0.00); ");
                        //string pfund = Convert.ToDouble(dtsalery.Rows[0]["pfund"]).ToString("#,##0.00;(#,##0.00); ");
                        //string itax = Convert.ToDouble(dtsalery.Rows[0]["itax"]).ToString("#,##0.00;(#,##0.00); ");

                        lbody = "<style>@page { size:8.5in 11in; margin: 2cm 4cm } @media print {.page {page -break-after: always;}}</style>" +
                                "<div>" +
                                "<header>" +
                                "<p><img  src='Image/LOGO5104.PNG' /> </p>" + "<div class='printHeader'><p><br /><br />" +
                               "<p><strong>" + refissueno + "</strong></p><p><strong></p>Date: " + cdate + "</p> <br /><br /><br /><br /><br />" +
                                "</header>" +
                                "<br /><br /><br /><br /><br /><p class='cfHead'>TO WHOM IT MAY CONCERN</p><br /><br />" +

                                "<br />" +
                                "</br></br></br><br></br></br></br>" +
                                "<p></p><p>This is to certify that Mr./Ms  <strong>" + name + "</strong>  has been working with us as Permanent  employee. His/Her basic employment and monthly remuneration details are as follows:</p>" +
                               "<div  style='width:600px; margin:20px auto; margin:20px auto;'>" +
                               "<table class='tbodystyle'><tr><td>Designation</td><td>:" + Desig + "</td></tr>" +
                               "<tr><td>ID</td><td>: " + idCard + "</td></tr>" +
                               "<tr><td>Department</td><td>: " + dptdesc + "</td></tr>" +
                               "<tr><td>Joining Date</td><td>: " + jdate + "</td></tr>" +
                               "<tr><td>Salary</td><td>: " + gval + "</td></tr>" +
                               "<tr><td>Allowance</td><td>: " + suballow + "</td></tr>" +
                               "<tr><td>Car Allowance</td><td>: " + carallow + "</td></tr>" +
                               "<tr><td>Total</td><td>: " + ttlsalary + "</td></tr>" +
                               "<tr><td>Inword (BDT)</td><td>: " + inwords + "</td></tr>" +
                               "<tr><td>Deduction</td><td>: " + tdeduction + "</td></tr>" +
                               "<tr><td>Provident Fund</td><td>: " + pfund + "</td></tr>" +
                               "<tr><td>Income Tax</td><td>: " + itax + "</td></tr>" +
                                "<tr><td>Bank Transfer</td><td>: " + bankamt + "</td></tr>" +
                               "</table></div>" +
                                "<br>" + "<br>" + "<br>" + "<br>" + "<br>" +
                               "<p class='pFottext'>We are issuing this letter on the specific request of our employee without accepting any liability on behalf of this letter or part of this letter on our company.</p> <br><br><br><br><br><br><br><br><hr width='20%'><p class=''><strong><b>    HR & Administrator</b></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>" +
                               "<br>" +

                               "<div style='margin:10px 0; position:fixed; bottom:0; righ:30px'>" +
                                "<p style='font-size:10px; text-align:center !important;'>This is system generated Salary Certificate </p> " +

                                "<hr border-top:1px solid black;>" +

                                "<div style='width:61%; float:left'" +
                                "<p><strong> Corporate Office:</strong></p>" +
                                "<p>Rangs Babylonia, Level 6, 246 Bir Uttam Mir Shawkat Road, (Tejgaon Link Road)</p>" +
                                "<p>Tejgaon I/A, Dhaka-1208, Bangladesh.</p>" +
                                "<p>Phone: +88 02 8878057, +88 02 8878058, www.edison-bd.com, www.edisonfootwear.com</p>" +
                                "</div>" +
                                "<div style='width:38%; float:right'" +
                                "<p><strong> Factory:</strong></p>" +
                                "<p>JL No-05 Mirzapur</p>" +
                                "<p>Gazipur Sadar, Gazipur, Bangladesh.</p>" +
                                "</div>" +

                                "</div></div>";

                    }
                    else
                    {
                        lbody = "<p><img  src='Image/LOGO5104.PNG' /> </p>" + "<div class='printHeader'>" + "<p><strong>SPL/HR/Ofr/16/559</strong></p><p><strong></p>" +
                                " <p style='text-align:right'><br /><br /><br /><br /><br /><br /><br />Date: " + cdate + "</p><p class='cfHead'>To Whom it May Concern</p><p></br></br></br><br></br></br></br></p><p>This is to certify that <strong>" + name + "</strong>, <strong> " + Desig + " </strong> -  <strong>" + dptdesc + ", " + "</strong></br> has been working as a regular employee in our company since  <strong>" + jdate + "</strong>.</p><p class='pMargin'><strong>Breakdown of his monthly salary stands as follow:</strong></p><div style='width:400px; margin:0 auto;'>" + tablesale + "</div><p>&nbsp;</p><p><strong>" + inwords + "</strong></p><p>&nbsp;</p> <p class='pFottext'>I hereby certify that the above mentioned information is correct and accurate to the best of my knowledge it also noticeable that company will not be responsible for any sort of loan or personal transection of the above mentioned employee </p>  <br><br><hr width='20%'><p class=''><strong><u><b>HR & Administrator</b></u></p>  <p class='pUname'><strong>" + uname + "</p> <p class='pUname'><strong>" + usrdesig + "</strong></p>";
                    }

                    //<img src="Image/LoginImg/edisonidcard.jpg" />
                    //"<p> src='Image/LOGO8701.PNG/> </p>" +
                    break;




                default:
                    break;
            }
            return lbody;
        }


        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            //this.lblmsg.Visible = true;
            string comcod = GetCompCode();


            string empname = (string)ViewState["name"];


            bool result = true;
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTEMPNAMENEW", empname, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Sorry, Data Updated Fail";
                //this.lblmsg.ForeColor
            }





        }





        protected void chkpre_CheckedChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            if (!chkpre.Checked)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetCompCode();
            string empid = this.ddlEmployee.SelectedValue.ToString();

            string forJobCand = "";
            if (type == "10003" || type == "10004" || type == "10005")
            {
                forJobCand = "GETLETTERJOBCAND";
            }


            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLETTER", "%", type, forJobCand, "", "", "", "", "", "");
            this.ddlPrevious.DataTextField = "empname";
            this.ddlPrevious.DataValueField = "EMPID";
            this.ddlPrevious.DataSource = ds3.Tables[0];
            this.ddlPrevious.DataBind();
            ds3.Dispose();
        }
        protected void ddlPrevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cod = this.ddlPrevious.SelectedValue.ToString().Trim();

            this.ddlEmployee.ClearSelection();
            this.ddlEmployee.Items.FindByValue(cod).Selected = true;
        }
        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {

            string type1 = this.Request.QueryString["Type"].ToString().Trim();
            if (type1 == "10003" || type1 == "10004" || type1 == "10005" || type1 == "10020" || type1 == "10002")
            {
                var empid = this.ddlEmployee.SelectedValue.ToString();

                var ds = (DataSet)ViewState["empinfo"];
                if (ds == null || ds.Tables[0].Rows.Count < 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                    ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                    return;
                }

                DataTable dt1 = ds.Tables[0].Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("empid='" + empid + "'");
                dt1 = dv.ToTable();

                ViewState["name"] = (dt1.Rows.Count > 0 ? dt1.Rows[0]["empname1"] : "");
                ViewState["section"] = (dt1.Rows.Count > 0 ? dt1.Rows[0]["section"] : "");
                ViewState["desig"] = (dt1.Rows.Count > 0 ? dt1.Rows[0]["desig"] : "");


                this.ddlCat.DataTextField = "dptdesc";
                this.ddlCat.DataValueField = "empid";
                this.ddlCat.DataSource = dt1;
                // this.ddlCat.Enabled = false;
                this.ddlCat.DataBind();



            }
            else
            {
                var empid = this.ddlEmployee.SelectedValue.ToString();

                var ds = (DataSet)ViewState["empinfo"];
                if (ds == null)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                    ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                    return;
                }
                DataTable dt1 = ds.Tables[0].Copy();
                DataView dv = dt1.DefaultView;
                dv.RowFilter = ("empid='" + empid + "'");
                dt1 = dv.ToTable();

                ViewState["name"] = (dt1.Rows.Count > 0 ? dt1.Rows[0]["empname1"] : "");
                ViewState["section"] = (dt1.Rows.Count > 0 ? dt1.Rows[0]["section"] : "");
                ViewState["desig"] = (dt1.Rows.Count > 0 ? dt1.Rows[0]["desig"] : "");
            }


        }

        protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployee();
        }
    }
}