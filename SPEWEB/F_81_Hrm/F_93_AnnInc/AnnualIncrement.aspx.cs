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
using System.Data.OleDb;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_81_Hrm.F_93_AnnInc
{
    public partial class AnnualIncrement : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.txtdate.Text = System.DateTime.Today.ToString("dd.MM.yyyy");
                // this.GetCompany();
                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetIncreNo();
                this.GetJobLocation();
                this.GetSignatory();
                this.GetDesignation();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE INCREMENT INFORMATION";


            }
            if (fileuploadExcel.HasFile)
            {
                try
                {
                    Session.Remove("XcelData");
                    //  this.lmsg.Visible = true;
                    string connString = "";
                    string StrFileName = string.Empty;
                    if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                    {
                        StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                        string StrFileType = fileuploadExcel.PostedFile.ContentType;
                        int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                        if (IntFileSize <= 0)
                        {
                            //  this.lmsg.Text = "Uploading Fail";
                            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert(' file Uploading failed');", true);
                            return;
                        }
                        else
                        {
                            string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
                            string[] filePaths = Directory.GetFiles(savelocation);
                            foreach (string filePath in filePaths)
                                File.Delete(filePath);
                            fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);
                            //   this.lmsg.Text = "Uploading Successfully";
                        }
                    }

                    string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                    //string path = apppath + "ExcelFile\\" + StrFileName;
                    string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);

                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {

                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    }

                    //string query = "SELECT [Product],[Category],[Qty(Pcs)],[Value],[Unit Price],[ERP CODE] FROM [Sheet1$]";
                    string query = "";

                    query = "SELECT * FROM [Sheet1$]";


                    OleDbConnection conn = new OleDbConnection(connString);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    Session["XcelData"] = ds.Tables[0];
                    // this.DataInsert();
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();
                    //this.GetExelData();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
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
            this.GetSectionList();
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

            //this.lnkbtnShow_Click(null,null);
            //this.ShowInc();
        }

        private void GetCompany()
        {
            if (this.lnkbtnShow.Text == "New")
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");

            this.ddlCompany1.DataTextField = "actdesc";
            this.ddlCompany1.DataValueField = "actcode";
            this.ddlCompany1.DataSource = ds1.Tables[0];
            this.ddlCompany1.DataBind();
            this.ddlCompany1_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        private void GetDeptName()
        {
            if (this.lnkbtnShow.Text == "New")
                return;
            string comcod = this.GetComeCode();
            string Company = this.ddlCompany1.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = this.txtSrcDept.Text + "%%";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, userid, "", "", "", "", "", "");

            this.ddlDept1.DataTextField = "actdesc";
            this.ddlDept1.DataValueField = "actcode";
            this.ddlDept1.DataSource = ds1.Tables[0];
            this.ddlDept1.DataBind();
            this.ddlDept_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        private void GetSection()
        {

            if (this.lnkbtnShow.Text == "New")
                return;
            string comcod = this.GetComeCode();
            string company = this.ddlDept1.SelectedValue.ToString();
            string txtSrcSection = this.txtSrcSection.Text.Trim() + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAMEFL", txtSrcSection, company, "", "", "", "", "", "", "");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", company, txtSrcSection, "", "", "", "", "", "", "");

            this.ddlSection1.DataTextField = "sectionname";
            this.ddlSection1.DataValueField = "section";
            this.ddlSection1.DataSource = ds2.Tables[0];
            this.ddlSection1.DataBind();
            ds2.Dispose();

        }
        private void GetJobLocation()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);

            this.ddlJobLoc.DataTextField = "location";
            this.ddlJobLoc.DataValueField = "loccode";
            this.ddlJobLoc.DataSource = lst;
            this.ddlJobLoc.DataBind();
           
        }

        private void GetSignatory()
        {
            string comcod = this.GetComeCode();
            DataSet tblSignatory = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPSIGNAME", "80", "%", "%", "%", "", "", "", "", "");
            if (tblSignatory == null)
                return;

            Session["tblsignatory"] = tblSignatory.Tables[0];
        }

        private void GetDesignation()
        {
            string comcod = this.GetComeCode();
            DataSet dsdesig = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETEMPGRADE", "", "", "", "", "", "", "", "", "");
            if (dsdesig == null)
                return;

            Session["tbldesignation"] = dsdesig.Tables[0];
        }

        protected void ddlCompany1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetAllOrganogramList();
            this.GetDeptName();
        }


        private void GetPreviousList()
        {

            string comcod = GetComeCode();
            string mREQDAT = this.GetStdDate(this.txtdate.Text);
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENTNO", mREQDAT, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.ddlPrevIncList.Items.Clear();
                return;

            }

            this.ddlPrevIncList.DataTextField = "incrno1";
            this.ddlPrevIncList.DataValueField = "incrno";
            this.ddlPrevIncList.DataSource = ds2.Tables[0];
            this.ddlPrevIncList.DataBind();

        }

        private void GetIncreNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.GetStdDate(this.txtdate.Text);
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_ANNUAL_INCREMENT", "GETINCREMENTNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataTable dt1 = ds3.Tables[0];
            this.txtdate.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["maxincdt"].ToString().Trim()).ToString("dd.MM.yyyy");
            this.lblCurIncrNo.Text = ds3.Tables[0].Rows[0]["maxincno1"].ToString().Substring(0, 5);
            this.txtCurIncrNo.Text = ds3.Tables[0].Rows[0]["maxincno1"].ToString().Substring(6);
        }


        protected void GetIncrementNo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.GetStdDate(this.txtdate.Text.Trim());
            string mIncNo = "NEWINC";
            if (this.ddlPrevIncList.Items.Count > 0)
                mIncNo = this.ddlPrevIncList.SelectedValue.ToString();

            if (mIncNo == "NEWINC")
            {
                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETINCREMENTNO", date, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurIncrNo.Text = ds1.Tables[0].Rows[0]["maxincno1"].ToString().Substring(0, 6);
                    this.txtCurIncrNo.Text = ds1.Tables[0].Rows[0]["maxincno1"].ToString().Substring(6, 5);
                    this.ddlPrevIncList.DataTextField = "maxincno1";
                    this.ddlPrevIncList.DataValueField = "maxincno1";
                    this.ddlPrevIncList.DataSource = ds1.Tables[0];
                    this.ddlPrevIncList.DataBind();
                }

            }

        }


        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }



        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "New")
            {

                this.divPrevList.Visible = true;
                this.ddlPrevIncList.Items.Clear();
                this.lnkbtnShow.Text = "Ok";
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                this.lblCompany.Visible = false;
                this.lblDept.Visible = false;
                this.lblSection.Visible = false;
                this.ddlCompany1.Visible = true;
                this.ddlDept1.Visible = true;
                this.ddlSection1.Visible = true;
                this.txtdate.Enabled = true;
                this.divChkEmp.Visible = false;
                this.divAddEmp.Visible = false;
                this.chkAddEmp.Checked = false;
                return;
            }


            this.divPrevList.Visible = false;
            this.lnkbtnShow.Text = "New";
            this.lblCompany.Visible = true;
            this.lblDept.Visible = true;
            this.lblSection.Visible = true;
            this.ddlCompany1.Visible = false;
            this.ddlDept1.Visible = false;
            this.ddlSection1.Visible = false;
            this.divChkEmp.Visible = true;
            this.ShowInc();
        }
        private void ShowInc()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string DeptCode = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string SecCode = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string JobLocation = (this.ddlJobLoc.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLoc.SelectedValue.ToString()) + "%";
            string txtDate = this.GetStdDate(this.txtdate.Text);
            DataSet ds2;
            if (ddlPrevIncList.Items.Count == 0)
            {
                this.GetIncreNo();
                ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETINCREMENT", Company, divison, DeptCode, SecCode, JobLocation, txtDate, userid, "", "");
            }
            else
            {
                string preincreno = this.ddlPrevIncList.SelectedValue.ToString();
                ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENT", preincreno, "", "", "", "", "", "", "", "");
                this.lblCurIncrNo.Text = ds2.Tables[1].Rows[0]["incrno1"].ToString().Substring(0, 6);
                this.txtCurIncrNo.Text = ds2.Tables[1].Rows[0]["incrno1"].ToString().Substring(6, 5);
                this.txtdate.Text = Convert.ToDateTime(ds2.Tables[1].Rows[0]["incrdate"].ToString()).ToString("dd.MM.yyyy");

            }
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblAnnInc"] = dt;
            this.LoadGrid();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string deptcode = dt1.Rows[0]["deptcode"].ToString();
            string seccode = dt1.Rows[0]["seccode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode && dt1.Rows[j]["seccode"].ToString() == seccode)
                {

                    dt1.Rows[j]["deptname"] = "";
                    dt1.Rows[j]["section"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                        dt1.Rows[j]["deptname"] = "";
                    if (dt1.Rows[j]["seccode"].ToString() == seccode)
                        dt1.Rows[j]["section"] = "";
                }

                deptcode = dt1.Rows[j]["deptcode"].ToString();
                seccode = dt1.Rows[j]["seccode"].ToString();

            }
            return dt1;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comadd = hst["comaddf"].ToString();
                string comnam = hst["comnam"].ToString();
                string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                DataTable dt1 = new DataTable();
                if (chkallemptype.Checked)
                {
                    string Company = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
                    string year = Convert.ToDateTime(this.GetStdDate(this.txtdate.Text)).ToString("yyyy");
                    DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPREINCREMENTALL", Company, year, "", "", "", "", "", "", "");

                    dt1 = ds2.Tables[0];
                }
                else
                {
                    dt1 = (DataTable)Session["tblAnnInc"];
                }

                string txtDate = GetStdDate(this.txtdate.Text);
                string prevYear = Convert.ToDateTime(txtDate).AddYears(-1).ToString("yyyy");
                string curYear = ASTUtility.Right(this.txtdate.Text, 4);
                string empType = this.GetEmpType();

                var lst = dt1.DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptAnnualIncrement>();
                LocalReport Rpt1 = new LocalReport();
                //Final Increment
                if (chkOnlyFInc.Checked)
                {
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_93_AnnInc.RptAnnualIncrement", lst, null, null);
                }
                //Proposed Increment
                else
                {
                    Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_93_AnnInc.RptPropAnnualIncrement", lst, null, null);
                }
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
                Rpt1.SetParameters(new ReportParameter("prevYear", prevYear));
                Rpt1.SetParameters(new ReportParameter("curYear", curYear));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Proposed Increment for " + empType + " , " + curYear));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private string GetEmpType()
        {
            string empType = "";
            string wrkStation = this.ddlWstation.SelectedValue.Substring(0,4);
            switch (wrkStation)
            {
                case "9401":
                case "9402":
                    empType = "Executive";
                    break;

                case "9411":
                case "9412":
                    empType = "Supporting Staff-(HO)";
                    break;

                case "9413":
                case "9415":
                    empType = "Factory Staff Grade-02(Non OT Based)";
                    break;

                case "9414":
                    empType = "Factory Staff(OT Based)";
                    break;
                
                case "9416":
                    empType = "Supporting Staff(OT Based)";
                    break;

                case "9420":
                case "9422":
                    empType = "Factory Staff Grade-01";
                    break;

                case "9403":
                case "9408":               
                    empType = "Worker";
                    break;

                default:
                    empType = "All";
                    break;
            }
            return empType;
        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDeptName();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        private void SaveValue()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAnnInc"];
                int TblRowIndex;
                for (int i = 0; i < this.gvAnnIncre.Rows.Count; i++)
                {
                    double carallow = Convert.ToDouble("0" + ((Label)this.gvAnnIncre.Rows[i].FindControl("lgvprecarsubamt")).Text.Trim());
                    double suballow = Convert.ToDouble("0" + ((Label)this.gvAnnIncre.Rows[i].FindControl("lgvprsubamt")).Text.Trim());
                    double presal = Convert.ToDouble("0" + ((Label)this.gvAnnIncre.Rows[i].FindControl("lgvpreamt")).Text.Trim());
                    double incpercnt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("lgvincpercnt")).Text.Trim());
                    double incamt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("txtgvincamt")).Text.Trim());
                    double hrpromincamt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("txtgvhrpromincamt")).Text.Trim());
                    double fincamt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("txtgvfinamount")).Text.Trim());
                    string remarks = ((TextBox)this.gvAnnIncre.Rows[i].FindControl("TxtRemarks")).Text.Trim();
                    string Signatory = ((DropDownList)this.gvAnnIncre.Rows[i].FindControl("DdlSignatory")).SelectedValue.Trim().ToString();
                    string grade = ((DropDownList)this.gvAnnIncre.Rows[i].FindControl("ddlGrade")).SelectedValue.Trim().ToString();
                    string promdesigid = ((DropDownList)this.gvAnnIncre.Rows[i].FindControl("ddlDesignation")).SelectedValue.Trim().ToString();
                    string hrpromdesigid = ((DropDownList)this.gvAnnIncre.Rows[i].FindControl("ddlDesignation1")).SelectedValue.Trim().ToString();


                    TblRowIndex = (gvAnnIncre.PageIndex) * gvAnnIncre.PageSize + i;

                    incpercnt = presal == 0 ? presal : incpercnt > 0 ? incpercnt : Math.Round(((incamt * 100) / presal),2);
                    incamt = incamt > 0 ? incamt : incpercnt > 0 ? (presal * 0.01 * incpercnt) : 0.00;
                    dt.Rows[TblRowIndex]["inpercnt"] = incpercnt;
                    dt.Rows[TblRowIndex]["incamt"] = incamt;
                    dt.Rows[TblRowIndex]["pinincamt"] = incamt;
                    dt.Rows[TblRowIndex]["finincamt"] = fincamt;
                    dt.Rows[TblRowIndex]["remarks"] = remarks;
                    dt.Rows[TblRowIndex]["carsubamt"] = carallow;
                    dt.Rows[TblRowIndex]["subamt"] = suballow;
                    dt.Rows[TblRowIndex]["hrpromincamt"] = hrpromincamt;
                    dt.Rows[TblRowIndex]["signatory"] = Signatory;
                    dt.Rows[TblRowIndex]["grade"] = grade;
                    dt.Rows[TblRowIndex]["prodesigid"] = promdesigid;
                    dt.Rows[TblRowIndex]["hrprodesigid"] = hrpromdesigid;
                }
                Session["tblAnnInc"] = dt;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
           

        }
        private void LoadGrid()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAnnInc"];
                this.gvAnnIncre.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvAnnIncre.DataSource = dt;
                this.gvAnnIncre.DataBind();
                this.FooterCal();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }
        protected void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFpresal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(grossal)", "")) ? 0.00 : dt.Compute("sum(grossal)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(incamt)", "")) ? 0.00 : dt.Compute("sum(incamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFPinincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pinincamt)", "")) ? 0.00 : dt.Compute("sum(pinincamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFhrpromincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrpromincamt)", "")) ? 0.00 : dt.Compute("sum(hrpromincamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvAnnIncre.FooterRow.FindControl("lgvFfinincamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(finincamt)", "")) ? 0.00 : dt.Compute("sum(finincamt)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            this.LoadGrid();

        }

        protected void gvAnnIncre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvAnnIncre.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {

            //this.SaveValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string cutdate = this.GetStdDate(this.txtdate.Text);
            try
            {
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)Session["tblAnnInc"];
                if (ddlPrevIncList.Items.Count == 0)
                    this.GetIncrementNo();
                string incno = this.lblCurIncrNo.Text.ToString().Trim().Substring(0, 3) + cutdate.Substring(7, 4) + this.lblCurIncrNo.Text.ToString().Trim().Substring(3, 2) + this.txtCurIncrNo.Text.ToString().Trim();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    string grossal = dt.Rows[i]["grossal"].ToString();
                    string incpercnt = dt.Rows[i]["inpercnt"].ToString();
                    string incamt = dt.Rows[i]["incamt"].ToString();
                    double pinincamt = Convert.ToDouble(dt.Rows[i]["pinincamt"].ToString());
                    double finincamt = Convert.ToDouble(dt.Rows[i]["finincamt"].ToString());
                    string remarks = dt.Rows[i]["remarks"].ToString();
                    string suballow = dt.Rows[i]["subamt"].ToString();
                    string carallow = dt.Rows[i]["carsubamt"].ToString();
                    string hrpromincamt = dt.Rows[i]["hrpromincamt"].ToString();
                    string signatory = dt.Rows[i]["signatory"].ToString();
                    string incamtprevyr = dt.Rows[i]["incamtprevyr"].ToString();
                    string grade = dt.Rows[i]["grade"].ToString();
                    string prodesigid = dt.Rows[i]["prodesigid"].ToString();
                    string hrprodesigid = dt.Rows[i]["hrprodesigid"].ToString();

                    if (pinincamt != 0)
                    {
                        bool result = HRData.UpdateTransInfo3(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "INSUPDATE_INCREMENT", incno, empid,
                                                        cutdate, grossal, incpercnt, incamt, finincamt.ToString(), postDat, userid, trmid, 
                                                        sessionid, remarks, carallow, suballow, signatory, incamtprevyr, grade, prodesigid, pinincamt.ToString(), hrpromincamt, hrprodesigid,"","");

                        if(!result)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                        }

                    }
                }
                
                
                
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message+"');", true);


            }
        }


        protected void imgbtnSectionSrch_Click(object sender, EventArgs e)
        {
            this.GetSection();
        }
        protected void imgbtnPreList_Click(object sender, EventArgs e)
        {
            this.GetPreviousList();
        }
        protected void ddlDept1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();
        }
        protected void lbtnPutSameValue_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            double incpercnt = Convert.ToDouble(dt.Rows[0]["inpercnt"]);
            for (int i = 1; i < dt.Rows.Count; i++)
            {

                double grossal = Convert.ToDouble(dt.Rows[i]["grossal"]);
                dt.Rows[i]["inpercnt"] = incpercnt;
                dt.Rows[i]["incamt"] = grossal * 0.01 * incpercnt;
                dt.Rows[i]["pinincamt"] = grossal * 0.01 * incpercnt;
            }
            Session["tblAnnInc"] = dt;
            this.LoadGrid();

        }

        protected void lbtnRound_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblAnnInc"];
            int TblRowIndex;
            for (int i = 0; i < this.gvAnnIncre.Rows.Count; i++)
            {

                double Finincamt = Convert.ToDouble("0" + ((TextBox)this.gvAnnIncre.Rows[i].FindControl("lgvfinamount")).Text.Trim());
                TblRowIndex = (gvAnnIncre.PageIndex) * gvAnnIncre.PageSize + i;
                dt.Rows[TblRowIndex]["pinincamt"] = Finincamt;
            }
            Session["tblAnnInc"] = dt;
            this.LoadGrid();


        }
        protected void btnexcuplosd_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "5305"://Increment Data FB
                case "5306"://Increment Data FB
                    this.ShowIncrementFB();
                    break;

                default:
                    this.ShowIncrementGen();
                    break;
            }

        }

        private void ShowIncrementGen()
        {
            DataTable dt = (DataTable)Session["XcelData"];
            DataTable dtEmp = (DataTable)Session["tblAnnInc"];
            if (dtEmp.Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < dtEmp.Rows.Count; i++)
            {
                DataRow[] rows = dt.Select("Card_no ='" + dtEmp.Rows[i]["idcardno"] + "'");

                if (rows.Length > 0)
                {
                    double presal = Convert.ToDouble(dtEmp.Rows[i]["grossal"]);
                    double incpercnt = Convert.ToDouble(dtEmp.Rows[i]["inpercnt"]);
                    double incamt = Convert.ToDouble(rows[0]["increment"]);

                    incpercnt = incpercnt > 0 ? incpercnt : (incamt * 100) / presal;

                    dtEmp.Rows[i]["incamt"] = Convert.ToDouble("0" + rows[0]["increment"]);
                    dtEmp.Rows[i]["finincamt"] = Convert.ToDouble("0" + rows[0]["increment"]);
                    dtEmp.Rows[i]["inpercnt"] = Convert.ToDouble("0" + incpercnt);



                }
            }
            Session["tblAnnInc"] = dtEmp;
            this.LoadGrid();
        }

        private void ShowIncrementFB()
        {
            try
            {
                DataTable dt = (DataTable)Session["XcelData"];
                DataTable dtEmp = (DataTable)Session["tblAnnInc"];
                if (dtEmp.Rows.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < dtEmp.Rows.Count; i++)
                {
                    DataRow[] rows = dt.Select("Card_no ='" + dtEmp.Rows[i]["idcardno"] + "'");

                    if (rows.Length > 0)
                    {
                        dtEmp.Rows[i]["incamt"] = Convert.ToDouble("0" + rows[0]["Increment"]);
                        dtEmp.Rows[i]["pinincamt"] = Convert.ToDouble("0" + rows[0]["Increment"]);
                        dtEmp.Rows[i]["finincamt"] = Convert.ToDouble("0" + rows[0]["Increment"]);
                        dtEmp.Rows[i]["remarks"] = Convert.ToString(rows[0]["Remarks"]);
                        //dtEmp.Rows[i]["signatory"] = Convert.ToDouble(rows[0]["Signatory"]);

                    }

                }

                DataView dv1 = dtEmp.DefaultView;
                dv1.RowFilter = ("incamt>0");
                Session["tblAnnInc"] = dv1.ToTable();
                this.LoadGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
        }
      
        protected void gvAnnIncre_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable dtsign = (DataTable)Session["tblsignatory"];
            DataTable dtdesig = (DataTable)Session["tbldesignation"];


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string signatory = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "signatory"));

                DropDownList DdlSignatory = (DropDownList)e.Row.FindControl("DdlSignatory");
                DdlSignatory.DataTextField = "signame";
                DdlSignatory.DataValueField = "idcard";
                DdlSignatory.DataSource = dtsign;
                DdlSignatory.DataBind();
                DdlSignatory.SelectedValue = signatory;


                string prodesigid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prodesigid"));
                DropDownList ddlDesignation = (DropDownList)e.Row.FindControl("ddlDesignation");
                ddlDesignation.DataTextField = "hrgdesc";
                ddlDesignation.DataValueField = "hrgcod";
                ddlDesignation.DataSource = dtdesig;
                ddlDesignation.DataBind();
                ddlDesignation.SelectedValue = prodesigid;

                string hrprodesigid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "hrprodesigid"));
                DropDownList ddlDesignation1 = (DropDownList)e.Row.FindControl("ddlDesignation1");
                ddlDesignation1.DataTextField = "hrgdesc";
                ddlDesignation1.DataValueField = "hrgcod";
                ddlDesignation1.DataSource = dtdesig;
                ddlDesignation1.DataBind();
                ddlDesignation1.SelectedValue = hrprodesigid;

                string grade = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grade"));
                DropDownList ddlGrade = (DropDownList)e.Row.FindControl("ddlGrade");
                ddlGrade.SelectedValue = grade;
            }
        }

        protected void chkAddEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddEmp.Checked)
            {
                this.divAddEmp.Visible = true;
                DataTable dt1 = (DataTable)Session["tblAnnInc"];
                DataTable dt2 = new DataTable(); 
                //Previous List New Emp. Add
                if (this.ddlPrevIncList.Items.Count>0)
                {
                    this.ShowIncrEmpList();
                    dt2 = (DataTable)Session["tbladdAnnInc"];
                }                

                Session.Remove("tblemp");
                this.CreateDataTable();
                DataTable dt = (DataTable)Session["tblemp"];
                this.ddlEmployee.Items.Clear();
                //Previous List New Emp. Add
                if (this.ddlPrevIncList.Items.Count>0)
                {
                    foreach (DataRow dr1 in dt2.Rows)
                    {
                        string empid = dr1["empid"].ToString();
                        if (dt.Select("empid='" + empid + "'").Length == 0)
                        {

                            DataRow dra = dt.NewRow();
                            dra["empid"] = dr1["empid"].ToString();
                            dra["idcard"] = dr1["idcardno"].ToString();
                            dra["empname"] = dr1["idcardno"].ToString() + "-" + dr1["empname"].ToString();
                            dt.Rows.Add(dra);
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        string empid = dr1["empid"].ToString();
                        if (dt.Select("empid='" + empid + "'").Length == 0)
                        {

                            DataRow dra = dt.NewRow();
                            dra["empid"] = dr1["empid"].ToString();
                            dra["idcard"] = dr1["idcardno"].ToString();
                            dra["empname"] = dr1["idcardno"].ToString() + "-" + dr1["empname"].ToString();
                            dt.Rows.Add(dra);
                        }
                    }
                }

                //Previous List New Emp. Add
                if (this.ddlPrevIncList.Items.Count > 0)
                {
                    Session.Remove("tblAnnInc");
                    Session.Remove("tbladdAnnInc");
                    Session["tbladdAnnInc"] = dt2;
                    Session["tblAnnInc"] = dt1.Copy();
                }
               else
                {
                    Session.Remove("tblAnnInc");
                    Session.Remove("tbladdAnnInc");
                    dt2 = dt1.Copy();
                    Session["tbladdAnnInc"] = dt2;
                    DataTable dt3 = dt1.Clone();
                    Session["tblAnnInc"] = dt3;
                }

                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = dt;
                this.DataBind();

                //GridView DataBind
                this.LoadGrid();
            }
            else
            {
                this.divAddEmp.Visible = false;
            }
        }

        private void ShowIncrEmpList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string Company = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string divison = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string DeptCode = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string SecCode = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string JobLocation = (this.ddlJobLoc.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLoc.SelectedValue.ToString()) + "%";
            string txtDate = this.GetStdDate(this.txtdate.Text);
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETINCREMENT", Company, divison, DeptCode, SecCode, JobLocation, txtDate, userid, "", "");
            if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvAnnIncre.DataSource = null;
                this.gvAnnIncre.DataBind();
                return;
            }
            
            Session["tbladdAnnInc"] = ds2.Tables[0];
        }

        private void CreateDataTable()
        {
            if (Session["tblemp"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("empid", Type.GetType("System.String"));
                dt.Columns.Add("empname", Type.GetType("System.String"));
                dt.Columns.Add("idcard", Type.GetType("System.String"));
                Session["tblemp"] = dt;

            }
        }

        protected void lnkbtnAddEmp_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = (DataTable)Session["tblAnnInc"];
                DataTable dtadd = (DataTable)Session["tbladdAnnInc"];

                string empid = this.ddlEmployee.SelectedValue.ToString();

                DataRow[] dr = dt.Select("empid='" + empid + "'");
                if (dr.Length == 0)
                {
                    DataRow[] dra = dtadd.Select("empid='" + empid + "'");
                    dt.ImportRow(dra[0]);
                }               
                else
                {
                    string existempdet = "Employee : " + dr[0]["idcardno"].ToString() + " - " + dr[0]["empname"].ToString() + " already existed!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + existempdet + "');", true);
                }

                Session["tblAnnInc"] = dt;
                this.SaveValue();
                this.LoadGrid();
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message+"');", true);
            }
           
        }

        protected void lnkBtnDelIncrmnt_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAnnInc"];
                string comcod = this.GetComeCode();
                int rowIndex = ((GridViewRow)((LinkButton)(sender)).NamingContainer).RowIndex;
                int index = (this.gvAnnIncre.PageSize * this.gvAnnIncre.PageIndex) + rowIndex;
                string empId = dt.Rows[index]["empid"].ToString();
                string incrmentNO = this.ddlPrevIncList.SelectedValue.ToString();
                bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "DELETE_ANNUAL_INCREMENT", incrmentNO, empId, "", "", "", "", "", "");
              
                dt.Rows.RemoveAt(index);
                Session["tblAnnInc"] = dt;
                this.LoadGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('"+"Annual Increment Deleted Successfully" + "');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
            }
          
        }
    }
}