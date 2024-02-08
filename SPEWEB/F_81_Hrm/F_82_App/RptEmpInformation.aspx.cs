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
using Microsoft.Reporting.WinForms;
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;


namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class RptEmpInformation : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = 
                    (this.Request.QueryString["Type"].ToString().Trim() == "Services") ? "EMPLOYEE  SERVICES INFORMATION" : "EMPLOYEE INFORMATION";

                this.GetWorkStation();
                this.GetAllOrganogramList();
                this.GetJobLocation();
                this.ChckAppint.Visible = false;
                this.ChckPerInf.Visible = false;
                this.ChckApplic.Visible = false;
                this.ChckIdCard.Visible = false;
                this.ChckNomeni.Visible = false;
                this.chkRef.Visible = false;
                this.SelectView();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void SelectView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Services":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.divEmp.Visible = true;
                    break;

                case "EmpAllInfo":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.divEmp.Visible = true;
                    break;

                case "AllDoc":
                    this.divFilter.Visible = true;                    
                    this.divEmp.Visible = true;
                    break;

                case "EmpDyInfo":
                case "EmpDyInfo02":
                    this.GetCompany();
                    this.GetDynamcifield();
                    this.GetDesignation();
                    this.FieldVisible();
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ddlSrch1_SelectedIndexChanged(null, null);
                    this.ddlSrch2_SelectedIndexChanged(null, null);
                    this.ddlSrch3_SelectedIndexChanged(null, null);
                    this.divEmp.Visible = false;
                    break;
            }

        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }





        private void GetEmpName()
        {

            string comcod = this.GetComeCode();

            string emptype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dpt = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";

            string txtSProject = (this.Request.QueryString["Type"].ToString().Trim() == "Services") ? ("%") : ( "%");
            string date = this.txtDate.Text.Trim();
            // DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETEMPTNAME", txtSProject, date, emptype, division, dpt, section, "", "", "");

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPNAME", emptype, dpt, section, txtSProject, "1", "%", "", "", "");

            if (this.Request.QueryString["Type"].ToString().Trim() == "Services")
            {
                this.ibtnEmpListAllinfo.Visible = false;
                this.ddlEmpNameAllInfo.Visible = false;
                this.ddlEmpName.DataTextField = "empname";
                this.ddlEmpName.DataValueField = "empid";
                this.ddlEmpName.DataSource = ds3.Tables[0];
                this.ddlEmpName.DataBind();
            }
            else
            {
                this.ddlEmpNameAllInfo.DataTextField = "empname";
                this.ddlEmpNameAllInfo.DataValueField = "empid";
                this.ddlEmpNameAllInfo.DataSource = ds3.Tables[0];
                this.ddlEmpNameAllInfo.DataBind();

            }

        }

        private void GetDynamcifield()
        {
            ViewState.Remove("tbldyfield");
            string comcod = this.GetComeCode();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTDYNAMICFIELD", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.cblEmployee.Items.Clear();
                return;
            }

            this.cblEmployee.DataTextField = "descrip";
            this.cblEmployee.DataValueField = "code";
            this.cblEmployee.DataSource = ds4.Tables[0];
            this.cblEmployee.DataBind();
            ViewState["tbldyfield"] = ds4.Tables[0];


        }

        private void GetDesignation()
        {

            // ViewState.Remove("tbldesignation");
            string comcod = this.GetComeCode();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETDESIGNATION", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
                return;

            this.ddldesig01.DataTextField = "desig";
            this.ddldesig01.DataValueField = "desigid";
            this.ddldesig01.DataSource = ds4.Tables[0];
            this.ddldesig01.DataBind();

            this.ddldesig02.DataTextField = "desig";
            this.ddldesig02.DataValueField = "desigid";
            this.ddldesig02.DataSource = ds4.Tables[0];
            this.ddldesig02.DataBind();

            this.ddldesig03.DataTextField = "desig";
            this.ddldesig03.DataValueField = "desigid";
            this.ddldesig03.DataSource = ds4.Tables[0];
            this.ddldesig03.DataBind();

            this.ddltodesig1.DataTextField = "desig";
            this.ddltodesig1.DataValueField = "desigid";
            this.ddltodesig1.DataSource = ds4.Tables[0];
            this.ddltodesig1.DataBind();

            this.ddltodesig2.DataTextField = "desig";
            this.ddltodesig2.DataValueField = "desigid";
            this.ddltodesig2.DataSource = ds4.Tables[0];
            this.ddltodesig2.DataBind();

            this.ddltodesig3.DataTextField = "desig";
            this.ddltodesig3.DataValueField = "desigid";
            this.ddltodesig3.DataSource = ds4.Tables[0];
            this.ddltodesig3.DataBind();


            //ViewState["tbldesignation"] = ds4.Tables[0];
            ds4.Dispose();

        }

        private void FieldVisible()
        {
            this.lblSearchlist.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlFieldList1.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlSrch1.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.txtSearch1.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlOperator1.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlFieldList2.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlSrch2.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.txtSearch2.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlOperator2.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlFieldList3.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.ddlSrch3.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
            this.txtSearch3.Visible = (this.Request.QueryString["Type"].ToString().Trim() == "EmpDyInfo") ? false : true;
        }

        private void GetCompany()
        {
            string comcod = this.GetComeCode();
            string txtCompany = this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            ds1.Dispose();

        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetComeCode();
            switch (Type)
            {
                case "Services":
                    this.PrintServices();
                    break;
                case "EmpAllInfo":
                    switch (comcod)
                    {
                        case "5305":
                        case "5306":
                            this.PrintEmpAllInfoFB();
                            break;


                        default:
                            this.PrintEmpAllInfo();
                            break;




                    }
                    
                   
                    break;
                case "AllDoc":
                    switch (GetComeCode())
                    {
                        case "5305":
                        case "5306":
                            this.PrintAllDocFB();
                            break;
                        default:
                            this.PrintAllDoc();
                            break;
                    }
                   
                    break;
                case "EmpDyInfo":
                case "EmpDyInfo02":
                    this.PrintDyInfo();
                    break;


            }
        }
        private void PrintServices()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblservices"];
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            ReportDocument rptempservices = new RMGiRPT.R_81_Hrm.R_82_App.RptEmpServices();
            TextObject txtempname = rptempservices.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + this.ddlEmpName.SelectedItem.Text.Trim();
            TextObject rptdate = rptempservices.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Date: " + date;
            TextObject txtuserinfo = rptempservices.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempservices.SetDataSource(dt);
            //string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempservices.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempservices;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
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
            string stringNum = "";

            char[] dtae = num.ToCharArray();
            foreach (var item in dtae)
            {

                switch (item)
                {
                    case '0': stringNum += "০"; break;
                    case '1': stringNum += "১"; break;
                    case '2': stringNum += "২"; break;
                    case '3': stringNum += "৩"; break;
                    case '4': stringNum += "৪"; break;
                    case '5': stringNum += "৫"; break;
                    case '6': stringNum += "৬"; break;
                    case '7': stringNum += "৭"; break;
                    case '8': stringNum += "৮"; break;
                    case '9': stringNum += "৯"; break;
                }



            }
            return stringNum;
        }
        private void PrintEmpAllInfo()
        {
          
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string empType = this.ddlWstation.SelectedValue.ToString();
            var rptTitle = "Employee Information";
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformation>();
            var listBN = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformationBn>();
            var empNames = this.ddlEmpNameAllInfo.SelectedItem.Text.Trim().Split('-');
            var empName = empNames[1];
            var empSl = empNames[0];
            string deptName = (ds1.Tables[2].Rows.Count == 0) ? "" : ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            string netSal = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");
            string deptprestring = "";
            LocalReport report = new LocalReport();
            if (empType.Substring(0, 4) == "9401" || empType.Substring(0, 4) == "9402" || empType.Substring(0, 4) == "9404" || empType.Substring(0, 4) == "9409")
            {
                deptprestring = "DEPARTMENT NAME: ";
                report = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpAllInformationENG", list, null, null);
            }
            else if (empType.Substring(0, 4) == "9403")
            {
                foreach (var item in listBN)
                {

                    if (item.grpid == "04000")
                    {
                        var valInt = item.amt.Split('.');
                        var val = valInt[0].ToString();
                        string num = this.NumBn(val);
                        item.amt = num;
                    }
                    if (item.gcod == "01003" || item.gcod == "01007" || item.gcod == "01008")
                    {

                        var valdate = item.gdesc.Split('-');
                        var val1 = this.NumBn(valdate[0]);
                        var val3 = this.NumBn(valdate[2]);
                        var val2 = GetMonthName(valdate[1]);
                        item.gdescbn = val1 + "-" + val2 + "-" + val3;
                    }
                    if (item.gcod == "01001")
                    {
                        item.gdescbn = NumBn(item.gdesc);
                    }
                    if (item.gcod == "01990")
                    {
                        item.gdescbn = NumBn(item.gdesc);
                    }
                }

                report = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpAllInformationBN", listBN, null, null);

                comnam = ds1.Tables[2].Rows[0]["empcomdescb"].ToString();// "এডিসন ফুটওয়্যার লিঃ ";
                empName = ds1.Tables[0].Rows[1]["gdescbn"].ToString();
                comadd = ds1.Tables[2].Rows[0]["comadd"].ToString();
                rptTitle = "কর্মচারীর তথ্য";
                deptName = (ds1.Tables[2].Rows.Count == 0) ? "" : ds1.Tables[2].Rows[0]["empdeptdescbn"].ToString();
            }

            report.EnableExternalImages = true;
            report.SetParameters(new ReportParameter("compName", comnam));
            report.SetParameters(new ReportParameter("compAdd", comadd));
            report.SetParameters(new ReportParameter("empSl", empSl));
            report.SetParameters(new ReportParameter("empName", empName));
            report.SetParameters(new ReportParameter("deptName", deptprestring + deptName));
            report.SetParameters(new ReportParameter("comLogo", comLogo));
            report.SetParameters(new ReportParameter("netSal", netSal));
            report.SetParameters(new ReportParameter("rptTitle", rptTitle));
            report.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = report;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

           
        }

        private void PrintEmpAllInfoFB()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string empType = this.ddlWstation.SelectedValue.ToString();
            var rptTitle = "Employee Information";
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformation>();
            var listBN = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformationBn>();
            var empNames = this.ddlEmpNameAllInfo.SelectedItem.Text.Trim().Split('-');
            var empName = empNames[1];
            var empSl = empNames[0];
            string deptName = (ds1.Tables[2].Rows.Count == 0) ? "" : ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            string netSal = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");
            string deptprestring = "";
            LocalReport report = new LocalReport();

            switch (comcod)
            {
                case "9403": // Workder
                case "9404": // Workder
                case "9405": // Workder
                case "9406": // Workder
                case "9407": // Workder
                case "9408": // Workder
                case "9409": // Workder
                case "9410": // Workder
                    report = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpAllInformationBN", listBN, null, null);
                    foreach (var item in listBN)
                    {

                        if (item.grpid == "04000")
                        {
                            var valInt = item.amt.Split('.');
                            var val = valInt[0].ToString();
                            string num = this.NumBn(val);
                            item.amt = num;
                        }
                        if (item.gcod == "01003" || item.gcod == "01007" || item.gcod == "01008")
                        {

                            var valdate = item.gdesc.Split('-');
                            var val1 = this.NumBn(valdate[0]);
                            var val3 = this.NumBn(valdate[2]);
                            var val2 = GetMonthName(valdate[1]);
                            item.gdescbn = val1 + "-" + val2 + "-" + val3;
                        }
                        if (item.gcod == "01001")
                        {
                            item.gdescbn = NumBn(item.gdesc);
                        }
                        if (item.gcod == "01990")
                        {
                            item.gdescbn = NumBn(item.gdesc);
                        }
                    }

                    comnam = ds1.Tables[2].Rows[0]["empcomdescb"].ToString();// "এডিসন ফুটওয়্যার লিঃ ";
                    empName = ds1.Tables[0].Rows[1]["gdescbn"].ToString();
                    comadd = ds1.Tables[2].Rows[0]["comadd"].ToString();
                    rptTitle = "কর্মচারীর তথ্য";
                    deptName = (ds1.Tables[2].Rows.Count == 0) ? "" : ds1.Tables[2].Rows[0]["empdeptdescbn"].ToString();


                    break;


                default:
                    deptprestring = "DEPARTMENT NAME: ";
                    report = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpAllInformationENG", list, null, null);
                    break;



            }




          

            report.EnableExternalImages = true;
            report.SetParameters(new ReportParameter("compName", comnam));
            report.SetParameters(new ReportParameter("compAdd", comadd));
            report.SetParameters(new ReportParameter("empSl", empSl));
            report.SetParameters(new ReportParameter("empName", empName));
            report.SetParameters(new ReportParameter("deptName", deptprestring + deptName));
            report.SetParameters(new ReportParameter("comLogo", comLogo));
            report.SetParameters(new ReportParameter("netSal", netSal));
            report.SetParameters(new ReportParameter("rptTitle", rptTitle));
            report.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = report;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

         



        }
        private void PrintAllDocFB()
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string langtype = "0";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPNOMINEELIST", empid, langtype, "", "", "", "", "", "", "");

            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string empType = this.ddlWstation.SelectedValue.ToString();
            var rptTitle = "Employee Information";
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformation>();
            var listBN = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformationBn>();
            var rptlist = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.Empnomineelist>();
            if (ds3 != null)
            {
                rptlist = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.Empnomineelist>();
            }

            var empNames = this.ddlEmpNameAllInfo.SelectedItem.Text.Trim().Split('-');
            var empName = empNames[1];
            var empSl = empNames[0];
            string deptName = (ds1.Tables[2].Rows.Count == 0) ? "" : ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            string netSal = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");
            string deptprestring = "";

            bool applic = this.ChckApplic.Checked;
            bool empInfo = this.ChckPerInf.Checked;
            bool nomeni = this.ChckNomeni.Checked;
            bool idcard = this.ChckIdCard.Checked;
            bool niyog = this.ChckAppint.Checked;
            bool refer = this.chkRef.Checked;
            double Bsal = 0.00;
            double HRent = 0.00;
            double MedAllow = 0.00;
            double Conv = 0.00;
            double foodall = 0.00;
            foreach (var item in listBN)
            {
                if (item.gcod == "04001")
                {
                    Bsal = Convert.ToDouble(item.amt);
                }
                else if (item.gcod == "04002")
                {
                    HRent = Convert.ToDouble(item.amt);
                }
                else if (item.gcod == "04003")
                {
                    MedAllow = Convert.ToDouble(item.amt);
                }
                else if (item.gcod == "04004")
                {
                    Conv = Convert.ToDouble(item.amt);
                }
                else if (item.gcod == "04020")
                {
                    foodall = Convert.ToDouble(item.amt);
                }




            }

            string TotalAllow = Convert.ToDouble(Convert.ToDouble(Bsal) + Convert.ToDouble(HRent) + Convert.ToDouble(MedAllow) + Convert.ToDouble(Conv) + Convert.ToDouble(foodall)).ToString("#,##0; (#,##0); ");
            var joDate = ds3.Tables[1].Rows[0]["jointdat"];
            var jDate = Convert.ToDateTime(joDate).ToString("dd-MMM-yyyy");
            var pdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            var dob = Convert.ToDateTime(ds3.Tables[1].Rows[0]["dob"]).ToString("dd-MMM-yyyy");

            var dates = jDate.Split('-');
            string firstnum = NumBn(dates[0]);
            string lastnum = NumBn(dates[2]);
            string vllage = ds3.Tables[0].Rows[6]["gdatatbn"].ToString();
            string distr = ds3.Tables[0].Rows[7]["gdatatbn"].ToString();
            string uppo = ds3.Tables[0].Rows[8]["gdatatbn"].ToString();
            string post = ds3.Tables[0].Rows[9]["gdatatbn"].ToString();
            var bdates = dob.Split('-');
            string firstbdates = NumBn(bdates[0]);
            string lastbdates = NumBn(bdates[2]);
            var pdates = pdate.Split('-');
            string firstpdate = NumBn(dates[0]);
            string lastpdate = NumBn(dates[2]);
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%"; ;
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString();
            string empidN = (empid == "000000000000") ? "%" : empid + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "RPTALLEMPLISTFACTORYBANGAL", emptype, div, empidN, deptname, section, "", "", "", "");
            if(ds4.Tables[0].Rows.Count==0 || ds4.Tables==null)
            {
                return;
            }
            var lst1 = ds4.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfoBangla>();
            string laimg = "";
            string Auimg = "";

            laimg = lst1[0].empsign.ToString();
            Auimg = lst1[0].mangerempsign.ToString();

            LocalReport report = new LocalReport();
            if (empType.Substring(0, 4) == "9401" || empType.Substring(0, 4) == "9402" || empType.Substring(0, 4) == "9404" || empType.Substring(0, 4) == "9409")
            {
                //deptprestring = "DEPARTMENT NAME: ";
                //report = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpAllInformationENG", list, null, null);
            }
            else if (empType.Substring(0, 4) == "9403")
            {
                foreach (var item in listBN)
                {

                    if (item.grpid == "04000")
                    {
                        var valInt = item.amt.Split('.');
                        var val = valInt[0].ToString();
                        string num = this.NumBn(val);
                        item.amt = num;
                    }
                    if (item.gcod == "01003" || item.gcod == "01007" || item.gcod == "01008")
                    {

                        var valdate = item.gdesc.Split('-');
                        var val1 = this.NumBn(valdate[0]);
                        var val3 = this.NumBn(valdate[2]);
                        var val2 = GetMonthName(valdate[1]);
                        item.gdescbn = val1 + "-" + val2 + "-" + val3;
                    }
                    if (item.gcod == "01001")
                    {
                        item.gdescbn = NumBn(item.gdesc);
                    }
                    if (item.gcod == "01990")
                    {
                        item.gdescbn = NumBn(item.gdesc);
                    }
                }
                report = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAllDocBNFB", listBN, rptlist, lst1);

                comnam = "এফবি ফুটওয়্যার লিঃ";
                empName = ds1.Tables[0].Rows[1]["gdescbn"].ToString();
                comadd = "উলুসারা, কালিয়াকৈর ,গাজীপুর। ";
                rptTitle = "কর্মচারীর তথ্য";
                deptName = (ds1.Tables[2].Rows.Count == 0) ? "" : ds1.Tables[2].Rows[0]["empdeptdescbn"].ToString();
            }
            else
            {
                report = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAllDocBN", listBN, rptlist, lst1);

            }
            string empMob = lst1[0].mangmobile.ToString();
            string HTMLFrmt = "<p>তারিখ:</p><p></p><p>বরাবর</p><p></p><p>মহাব্যবস্থাপক</p><p></p>মানব সম্পদ,প্রশাসন কমপ্লায়েন্স<p></p><p><strong>বিষয়: চাকুরীর জন্য আবেদন  </strong></p><p></p><p>জনাব,</p><p></p>যথাবিহীত সম্মান পূর্বক বিনীত নিবেদন এই যে, বিশ্বস্ত সূত্রে জানতে পারলাম যে আপনার প্রতিষ্ঠানে পদে কিছু সংখ্যক লোক নিয়োগ করা হবে আমি উক্ত পদের জন্য একজন প্রার্থী ।</p><p></p><p>অতএব, বিনীত নিবেদন এই যে ,আমাকে উল্লেখিত পদে নিয়োগ প্রদান করতে মহাশয়ের মর্জি হয় |</p><p></p><p></p><p></p><p></p><p></p><p><strong>নিবেদক/নিবেদিকা</strong></p><p></p><p>নাম :" + empName + " </p><p></p><p>স্বাক্ষর :---------------------------------</p>";
            string fathernam = ds1.Tables[0].Rows[3]["gdescbn"].ToString();
            string mothernam = ds1.Tables[0].Rows[4]["gdescbn"].ToString();
            string bdate = (firstbdates + "-" + GetMonthName(bdates[1]) + "-" + lastbdates).ToString();
            string jnDate = (firstnum + "-" + GetMonthName(dates[1]) + "-" + lastnum).ToString();
            report.EnableExternalImages = true;
            string laimg1 = new Uri(Server.MapPath(laimg)).AbsoluteUri;
            string Auimg1 = new Uri(Server.MapPath(Auimg)).AbsoluteUri;
            report.SetParameters(new ReportParameter("compName", comnam));
            report.SetParameters(new ReportParameter("compAdd", comadd));
            report.SetParameters(new ReportParameter("empSl", empSl));
            report.SetParameters(new ReportParameter("empName", empName));
            report.SetParameters(new ReportParameter("empMob", empMob));
            report.SetParameters(new ReportParameter("deptName", deptprestring + deptName));
            report.SetParameters(new ReportParameter("comLogo", comLogo));
            report.SetParameters(new ReportParameter("netSal", netSal));
            report.SetParameters(new ReportParameter("applic", applic.ToString()));
            report.SetParameters(new ReportParameter("empInfo", empInfo.ToString()));
            report.SetParameters(new ReportParameter("vllage", vllage.ToString()));
            report.SetParameters(new ReportParameter("distr", distr.ToString()));
            report.SetParameters(new ReportParameter("uppo", uppo.ToString()));
            report.SetParameters(new ReportParameter("post", post.ToString()));
            report.SetParameters(new ReportParameter("nomeni", nomeni.ToString()));
            report.SetParameters(new ReportParameter("idcard", idcard.ToString()));
            report.SetParameters(new ReportParameter("niyog", niyog.ToString()));
            report.SetParameters(new ReportParameter("HTMLFrmt", HTMLFrmt.ToString()));
            report.SetParameters(new ReportParameter("refer", refer.ToString()));
            report.SetParameters(new ReportParameter("foodall", foodall.ToString()));
            report.SetParameters(new ReportParameter("Bsal", Bsal.ToString()));
            report.SetParameters(new ReportParameter("HRent", HRent.ToString()));
            report.SetParameters(new ReportParameter("MedAllow", MedAllow.ToString()));
            report.SetParameters(new ReportParameter("Conv", Conv.ToString()));
            report.SetParameters(new ReportParameter("TotalAllow", TotalAllow.ToString()));
            report.SetParameters(new ReportParameter("HRent", HRent.ToString()));
            report.SetParameters(new ReportParameter("MedAllow", MedAllow.ToString()));
            report.SetParameters(new ReportParameter("Conv", Conv.ToString()));
            report.SetParameters(new ReportParameter("TotalAllow", TotalAllow.ToString()));
            //--------------Nomeni---
            report.SetParameters(new ReportParameter("nRptTitle", "মনোনয়ন ফরম"));
            report.SetParameters(new ReportParameter("pRptTitle", "পরিচয় পত্র"));
            report.SetParameters(new ReportParameter("degnam", ds3.Tables[1].Rows[0]["desigdesc"].ToString()));
            report.SetParameters(new ReportParameter("joindate", jnDate));
            report.SetParameters(new ReportParameter("bdate", bdate));
            report.SetParameters(new ReportParameter("fnam", fathernam));
            report.SetParameters(new ReportParameter("mname", mothernam));
            report.SetParameters(new ReportParameter("deptnam", ds3.Tables[1].Rows[0]["deptdesc"].ToString()));
            report.SetParameters(new ReportParameter("sesonnam", ds3.Tables[1].Rows[0]["sectiondesc"].ToString()));
            report.SetParameters(new ReportParameter("gender", ds3.Tables[1].Rows[0]["genderdesc"].ToString()));
            report.SetParameters(new ReportParameter("paddess", ds3.Tables[1].Rows[0]["peraddress"].ToString()));
            report.SetParameters(new ReportParameter("idno", ds3.Tables[1].Rows[0]["idcard"].ToString()));
            report.SetParameters(new ReportParameter("printdate", firstpdate + "-" + GetMonthName(pdates[1]) + "-" + lastpdate));
            //========ID=========
            report.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            report.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));
            report.SetParameters(new ReportParameter("laimg1", laimg1));
            report.SetParameters(new ReportParameter("Auimg1", Auimg1));
            report.SetParameters(new ReportParameter("rptTitle", rptTitle));
            report.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = report;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintAllDoc()
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string langtype = "0";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPNOMINEELIST", empid, langtype, "", "", "", "", "", "", "");
         
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string empType = this.ddlWstation.SelectedValue.ToString();
            var rptTitle = "Employee Information";
            var list = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformation>();
            var listBN = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInformationBn>();
            var rptlist = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.Empnomineelist>();
            if (ds3!=null)
            {
                rptlist = ds3.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.Empnomineelist>();
            }
            
            var empNames = this.ddlEmpNameAllInfo.SelectedItem.Text.Trim().Split('-');
            var empName = empNames[1];
            var empSl = empNames[0];
            string deptName = (ds1.Tables[2].Rows.Count == 0) ? "" : ds1.Tables[2].Rows[0]["empdeptdesc"].ToString();
            string netSal = Convert.ToDouble(ds1.Tables[1].Rows[0]["netsal"]).ToString("#,##0; (#,##0); ");
            string deptprestring = "";
             
            bool applic = this.ChckApplic.Checked;
            bool empInfo = this.ChckPerInf.Checked; 
            bool nomeni = this.ChckNomeni.Checked;
            bool idcard = this.ChckIdCard.Checked;
            bool niyog = this.ChckAppint.Checked;
            bool refer= this.chkRef.Checked;
            double Bsal=0.00 ;
            double HRent = 0.00;
            double MedAllow = 0.00;
            double Conv = 0.00;
            double foodall = 0.00;
            foreach (var item in listBN)
            {
                if (item.gcod=="04001")
                {
                    Bsal = Convert.ToDouble(item.amt);
                }
                else if (item.gcod == "04002")
                {
                    HRent = Convert.ToDouble(item.amt);
                }
                else if (item.gcod == "04003")
                {
                    MedAllow = Convert.ToDouble(item.amt);
                }
                else if (item.gcod == "04004")
                {
                    Conv = Convert.ToDouble(item.amt);
                }
                else if (item.gcod == "04020")
                {
                    foodall = Convert.ToDouble(item.amt);
                }
               
                 
                 
                 
            }
            
            string TotalAllow = Convert.ToDouble(Convert.ToDouble(Bsal) + Convert.ToDouble(HRent) + Convert.ToDouble(MedAllow) + Convert.ToDouble(Conv) + Convert.ToDouble(foodall)).ToString("#,##0; (#,##0); ");
            var joDate = ds3.Tables[1].Rows[0]["jointdat"];
            var jDate = Convert.ToDateTime(joDate).ToString("dd-MMM-yyyy");
            var pdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            var dob = Convert.ToDateTime(ds3.Tables[1].Rows[0]["dob"]).ToString("dd-MMM-yyyy");

            var dates = jDate.Split('-');
            string firstnum = NumBn(dates[0]);
            string lastnum = NumBn(dates[2]);
            string vllage = ds3.Tables[0].Rows[6]["gdatatbn"].ToString();
            string distr = ds3.Tables[0].Rows[7]["gdatatbn"].ToString();
            string uppo = ds3.Tables[0].Rows[8]["gdatatbn"].ToString();
            string post = ds3.Tables[0].Rows[9]["gdatatbn"].ToString();
            var bdates = dob.Split('-');
            string firstbdates = NumBn(bdates[0]);
            string lastbdates = NumBn(bdates[2]);
            var pdates = pdate.Split('-');
            string firstpdate = NumBn(dates[0]);
            string lastpdate = NumBn(dates[2]);
            string emptype = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%"; ;
            string div = (this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7) + "%";
            string deptname = (this.ddlDept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString();
            string empidN = (empid == "000000000000") ? "%" : empid + "%";
            DataSet ds4=HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "RPTALLEMPLISTFACTORYBANGAL", emptype, div, empidN, deptname, section, "", "", "", "");
            var lst1 = ds4.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.empInfoBangla>();
            string laimg = "";
            string Auimg = "";
            
            laimg = lst1[0].empsign.ToString();
            Auimg = lst1[0].mangerempsign.ToString();

           LocalReport report = new LocalReport();
            if (empType.Substring(0, 4) == "9401" || empType.Substring(0, 4) == "9402" || empType.Substring(0, 4) == "9404" || empType.Substring(0, 4) == "9409")
            {
                //deptprestring = "DEPARTMENT NAME: ";
                //report = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptEmpAllInformationENG", list, null, null);
            }
            else if (empType.Substring(0, 4) == "9403")
            {
                foreach (var item in listBN)
                {

                    if (item.grpid == "04000")
                    {
                        var valInt = item.amt.Split('.');
                        var val = valInt[0].ToString();
                        string num = this.NumBn(val);
                        item.amt = num;
                    }
                    if (item.gcod == "01003" || item.gcod == "01007" || item.gcod == "01008")
                    {

                        var valdate = item.gdesc.Split('-');
                        var val1 = this.NumBn(valdate[0]);
                        var val3 = this.NumBn(valdate[2]);
                        var val2 = GetMonthName(valdate[1]);
                        item.gdescbn = val1 + "-" + val2 + "-" + val3;
                    }
                    if (item.gcod == "01001")
                    {
                        item.gdescbn = NumBn(item.gdesc);
                    }
                    if (item.gcod == "01990")
                    {
                        item.gdescbn = NumBn(item.gdesc);
                    }
                }
                report = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAllDocBN", listBN, rptlist, lst1);
              
                comnam = "এডিসন ফুটওয়্যার লিঃ";
                empName = ds1.Tables[0].Rows[1]["gdescbn"].ToString();
                comadd = "তালতলী , মির্জাপুর ,হোতাপাড়া, গাজীপুর। ";
                rptTitle = "কর্মচারীর তথ্য";
                deptName = (ds1.Tables[2].Rows.Count == 0) ? "" : ds1.Tables[2].Rows[0]["empdeptdescbn"].ToString();
            }
            else
            {
                report = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptAllDocBN", listBN, rptlist, lst1);

            }
            string empMob = lst1[0].mangmobile.ToString();
            string HTMLFrmt = "<p>তারিখ:</p><p></p><p>বরাবর</p><p></p><p>মহাব্যবস্থাপক</p><p></p>মানব সম্পদ,প্রশাসন কমপ্লায়েন্স<p></p><p><strong>বিষয়: চাকুরীর জন্য আবেদন  </strong></p><p></p><p>জনাব,</p><p></p>যথাবিহীত সম্মান পূর্বক বিনীত নিবেদন এই যে, বিশ্বস্ত সূত্রে জানতে পারলাম যে আপনার প্রতিষ্ঠানে পদে কিছু সংখ্যক লোক নিয়োগ করা হবে আমি উক্ত পদের জন্য একজন প্রার্থী ।</p><p></p><p>অতএব, বিনীত নিবেদন এই যে ,আমাকে উল্লেখিত পদে নিয়োগ প্রদান করতে মহাশয়ের মর্জি হয় |</p><p></p><p></p><p></p><p></p><p></p><p><strong>নিবেদক/নিবেদিকা</strong></p><p></p><p>নাম :" + empName + " </p><p></p><p>স্বাক্ষর :---------------------------------</p>";
            string fathernam = ds1.Tables[0].Rows[3]["gdescbn"].ToString();
            string mothernam = ds1.Tables[0].Rows[4]["gdescbn"].ToString();
            string bdate = (firstbdates + "-" + GetMonthName(bdates[1]) + "-" + lastbdates).ToString();
            string jnDate = (firstnum + "-" + GetMonthName(dates[1]) + "-" + lastnum).ToString();
            report.EnableExternalImages = true;
            string laimg1 = new Uri(Server.MapPath(laimg)).AbsoluteUri;
            string Auimg1 = new Uri(Server.MapPath(Auimg)).AbsoluteUri;
            report.SetParameters(new ReportParameter("compName", comnam));
            report.SetParameters(new ReportParameter("compAdd", comadd));
            report.SetParameters(new ReportParameter("empSl", empSl));
            report.SetParameters(new ReportParameter("empName", empName));
            report.SetParameters(new ReportParameter("empMob", empMob));
            report.SetParameters(new ReportParameter("deptName", deptprestring + deptName));
            report.SetParameters(new ReportParameter("comLogo", comLogo));
            report.SetParameters(new ReportParameter("netSal", netSal));
            report.SetParameters(new ReportParameter("applic", applic.ToString()));
            report.SetParameters(new ReportParameter("empInfo", empInfo.ToString()));
            report.SetParameters(new ReportParameter("vllage", vllage.ToString()));
            report.SetParameters(new ReportParameter("distr", distr.ToString()));
            report.SetParameters(new ReportParameter("uppo", uppo.ToString()));
            report.SetParameters(new ReportParameter("post", post.ToString()));
            report.SetParameters(new ReportParameter("nomeni", nomeni.ToString()));
            report.SetParameters(new ReportParameter("idcard", idcard.ToString()));
            report.SetParameters(new ReportParameter("niyog", niyog.ToString()));
            report.SetParameters(new ReportParameter("HTMLFrmt", HTMLFrmt.ToString()));
            report.SetParameters(new ReportParameter("refer", refer.ToString()));
            report.SetParameters(new ReportParameter("foodall", foodall.ToString()));
            report.SetParameters(new ReportParameter("Bsal", Bsal.ToString()));
            report.SetParameters(new ReportParameter("HRent", HRent.ToString()));
            report.SetParameters(new ReportParameter("MedAllow", MedAllow.ToString()));
            report.SetParameters(new ReportParameter("Conv", Conv.ToString()));
            report.SetParameters(new ReportParameter("TotalAllow", TotalAllow.ToString()));
            report.SetParameters(new ReportParameter("HRent", HRent.ToString()));
            report.SetParameters(new ReportParameter("MedAllow", MedAllow.ToString()));
            report.SetParameters(new ReportParameter("Conv", Conv.ToString()));
            report.SetParameters(new ReportParameter("TotalAllow", TotalAllow.ToString()));
            //--------------Nomeni---
            report.SetParameters(new ReportParameter("nRptTitle", "মনোনয়ন ফরম"));
            report.SetParameters(new ReportParameter("pRptTitle", "পরিচয় পত্র"));
            report.SetParameters(new ReportParameter("degnam", ds3.Tables[1].Rows[0]["desigdesc"].ToString()));
            report.SetParameters(new ReportParameter("joindate", jnDate));
            report.SetParameters(new ReportParameter("bdate", bdate));
            report.SetParameters(new ReportParameter("fnam",fathernam));
            report.SetParameters(new ReportParameter("mname", mothernam));
            report.SetParameters(new ReportParameter("deptnam", ds3.Tables[1].Rows[0]["deptdesc"].ToString()));
            report.SetParameters(new ReportParameter("sesonnam", ds3.Tables[1].Rows[0]["sectiondesc"].ToString()));
            report.SetParameters(new ReportParameter("gender", ds3.Tables[1].Rows[0]["genderdesc"].ToString()));
            report.SetParameters(new ReportParameter("paddess", ds3.Tables[1].Rows[0]["peraddress"].ToString()));
            report.SetParameters(new ReportParameter("idno", ds3.Tables[1].Rows[0]["idcard"].ToString()));
            report.SetParameters(new ReportParameter("printdate", firstpdate + "-" + GetMonthName(pdates[1]) + "-" + lastpdate));
            //========ID=========
            report.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            report.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));
            report.SetParameters(new ReportParameter("laimg1", laimg1));
            report.SetParameters(new ReportParameter("Auimg1", Auimg1));
            report.SetParameters(new ReportParameter("rptTitle", rptTitle));
            report.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = report;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintDyInfo()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            this.printSearch();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblRptservices"];
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("MMMM dd, yyyy");
            ReportDocument rptempdyinfo = new RMGiRPT.R_81_Hrm.R_82_App.RptDynamicInfo();
            TextObject txtComName = rptempdyinfo.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            txtComName.Text = this.ddlCompany.SelectedItem.Text.Trim();

            //int j=0;
            for (int i = 0; i < this.cblEmployee.Items.Count; i++)
            {


                if (((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "comname")) || ((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "section")) || ((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "desigid")) || ((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "brcode")))
                {
                    //string header = this.cblEmployee.Items[i].Text.Trim().Replace(" ", "_");
                    //TextObject rpttxth = rptempdyinfo.ReportDefinition.ReportObjects[header] as TextObject;
                    //rpttxth.Text = header.Replace("_", " ");
                    //continue;
                }
                else if (cblEmployee.Items[i].Selected)
                {
                    string header = this.cblEmployee.Items[i].Value;
                    string title = this.cblEmployee.Items[i].Text.Trim();
                    TextObject rpttxth = rptempdyinfo.ReportDefinition.ReportObjects[header] as TextObject;
                    rpttxth.Text = title;
                }

                else if ((cblEmployee.Items[i].Value == "comname") || (cblEmployee.Items[i].Value == "section") || (cblEmployee.Items[i].Value == "desigid") || (cblEmployee.Items[i].Value == "brcode")) ;

                else
                {

                    string header = this.cblEmployee.Items[i].Value;
                    TextObject rpttxth = rptempdyinfo.ReportDefinition.ReportObjects[header] as TextObject;
                    rpttxth.Text = "";

                }
                //j++;
            }


            TextObject txtuserinfo = rptempdyinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempdyinfo.SetDataSource(dt);
            // string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempdyinfo.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempdyinfo;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            Session.Remove("tblservices");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string Date = this.txtDate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPSERVICES", empid, Date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvempservices.DataSource = null;
                this.gvempservices.DataBind();
                return;
            }
            Session["tblservices"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblservices"];

                string Type = this.Request.QueryString["Type"].ToString().Trim();
                switch (Type)
                {
                    case "Services":
                        this.gvempservices.DataSource = dt;
                        this.gvempservices.DataBind();
                        break;

                    case "EmpDyInfo":
                    case "EmpDyInfo02":

                        int i;


                        for (i = 1; i < this.gvempDyInfo.Columns.Count; i++)
                            this.gvempDyInfo.Columns[i].Visible = false;


                        for (i = 0; i < this.cblEmployee.Items.Count; i++)
                        {

                            string name = cblEmployee.Items[i].Value;
                            if (this.cblEmployee.Items[i].Selected)
                            {
                                this.gvempDyInfo.Columns[i+1].Visible = true;

                                this.gvempDyInfo.Columns[i+1].HeaderText = this.cblEmployee.Items[i].Text.Trim();
                                //(cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "comname"))
                                if (((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "brcode")) || ((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "section"))
                                    || ((cblEmployee.Items[i].Selected) && (cblEmployee.Items[i].Value == "desigid")))
                                {
                                    this.gvempDyInfo.Columns[i+1].Visible = false;
                                }
                            }

                        }
                        if(dt !=null || dt.Rows.Count>0)
                        this.gvempDyInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvempDyInfo.DataSource = dt;
                        this.gvempDyInfo.DataBind();
                        Session["Report1"] = gvempDyInfo;
                        ((HyperLink)this.gvempDyInfo.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;
                }


            }
            catch (Exception ex)
            { 
            }

        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
            // this.GetEmpName();
        }
        protected void lbtnEmpDyInfo_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            //this.lblPage.Visible = true;
            //this.ddlpagesize.Visible = true;
            string txtjorbirdate = "";
            string txtlike = "";
            string SearchInfo = "";
            string orderinfo = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid= hst["usrid"].ToString();


            if (this.ddlFieldList1.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch1.SelectedValue == "like") ? "%'" : "";

                string srch1 = "";
                if (this.ddlFieldList1.SelectedValue.ToString() == "desigid" && this.ddlSrch1.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig1.SelectedValue.ToString() + " and " + this.ddldesig01.SelectedValue.ToString();


                else if (this.ddlSrch1.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch1.Text.Trim() + "'" + " and '" + this.txttoSearch1.Text.Trim();
                else
                    srch1 = this.txtSearch1.Text.Trim();



                SearchInfo = SearchInfo + "" + this.ddlFieldList1.SelectedValue.ToString() + " " + this.ddlSrch1.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch1.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }


            if (this.ddlFieldList2.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList2.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList2.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch2.SelectedValue == "like") ? "%'" : "";


                string srch1 = "";
                if (this.ddlFieldList2.SelectedValue.ToString() == "desigid" && this.ddlSrch2.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig2.SelectedValue.ToString() + " and " + this.ddldesig02.SelectedValue.ToString();


                else if (this.ddlSrch2.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch2.Text.Trim() + "'" + " and '" + this.txttoSearch2.Text.Trim();
                else
                    srch1 = this.txtSearch2.Text.Trim();



                SearchInfo = SearchInfo + " " + this.ddlOperator1.SelectedValue.ToString() + " " + this.ddlFieldList2.SelectedValue.ToString() + " " + this.ddlSrch2.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch2.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }

            if (this.ddlFieldList3.SelectedValue != "00000")
            {
                txtjorbirdate = ((this.ddlFieldList3.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList2.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch3.SelectedValue == "like") ? "%'" : "";

                string srch1 = "";
                if (this.ddlFieldList3.SelectedValue.ToString() == "desigid" && this.ddlSrch3.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig3.SelectedValue.ToString() + " and " + this.ddldesig03.SelectedValue.ToString();
                else if (this.ddlSrch3.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch3.Text.Trim() + "'" + " and '" + this.txttoSearch3.Text.Trim();
                else
                    srch1 = this.txtSearch3.Text.Trim();
                SearchInfo = SearchInfo + " " + this.ddlOperator2.SelectedValue.ToString() + " " + this.ddlFieldList3.SelectedValue.ToString() + " " + this.ddlSrch3.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch3.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;
            }
            if (this.ddlOrder1.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder1.SelectedValue.ToString() + " " + this.ddlOrderad1.SelectedValue.ToString() + ", ";
            }
            if (this.ddlOrder2.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder2.SelectedValue.ToString() + " " + this.ddlOrderad2.SelectedValue.ToString() + ", ";
            }
            if (this.ddlOrder3.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder3.SelectedValue.ToString() + " " + this.ddlOrderad3.SelectedValue.ToString() + ",";
            }
            SearchInfo = SearchInfo.Trim();
            if (orderinfo.Length > 0)
                orderinfo = ASTUtility.Left(orderinfo.Trim(), orderinfo.Trim().Length - 1);
            string date1 = DateTime.Today.ToString("dd-MMM-yyyy");
            string CompanyName = (this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dpt = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString()) + "%";
            string empStatus=this.ddlEmpStatus.SelectedValue.ToString();
            string joblocation = (this.ddlJobLocation.SelectedValue.ToString() == "00000" ? "87" : this.ddlJobLocation.SelectedValue.ToString()) + "%";
            DataSet ds1 = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "HREMPDYINFORMATION", null, null, null, SearchInfo, orderinfo, joblocation, date1, CompanyName, division, dpt, section, usrid, empStatus);
            if (ds1 == null || ds1.Tables[0].Rows.Count==0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvempDyInfo.DataSource = null;
                this.gvempDyInfo.DataBind();
                return;
            }
            Session["tblservices"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string comname = dt1.Rows[0]["comname"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comname"].ToString() == comname)
                {
                    comname = dt1.Rows[j]["comname"].ToString();
                    dt1.Rows[j]["comdesc"] = "";
                }
                else
                    comname = dt1.Rows[j]["comname"].ToString();
            }
            return dt1;
        }
        private void printSearch()
        {
            string comcod = this.GetComeCode();
            //this.lblPage.Visible = true;
            //this.ddlpagesize.Visible = true;
            string fieldinfo = "";
            string txtjorbirdate = "";
            string txtlike = "";
            string SearchInfo = "";
            string orderinfo = "";
            for (int i = 0; i < this.cblEmployee.Items.Count; i++)
            {
                if (cblEmployee.Items[i].Selected)
                {
                    fieldinfo = fieldinfo + "" + cblEmployee.Items[i].Value.ToString() + ", ";
                }


            }

            if (this.ddlFieldList1.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch1.SelectedValue == "like") ? "%'" : "";

                string srch1 = "";
                if (this.ddlFieldList1.SelectedValue.ToString() == "desigid" && this.ddlSrch1.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig1.SelectedValue.ToString() + " and " + this.ddldesig01.SelectedValue.ToString();


                else if (this.ddlSrch1.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch1.Text.Trim() + " and " + this.txttoSearch1.Text.Trim();
                else
                    srch1 = this.txtSearch1.Text.Trim();
                SearchInfo = SearchInfo + "" + this.ddlFieldList1.SelectedValue.ToString() + " " + this.ddlSrch1.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch1.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }


            if (this.ddlFieldList2.SelectedValue != "00000")
            {

                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch2.SelectedValue == "like") ? "%'" : "";

                string srch1 = "";
                if (this.ddlFieldList2.SelectedValue.ToString() == "desigid" && this.ddlSrch2.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig2.SelectedValue.ToString() + " and " + this.ddldesig02.SelectedValue.ToString();


                else if (this.ddlSrch2.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch2.Text.Trim() + " and " + this.txttoSearch2.Text.Trim();
                else
                    srch1 = this.txtSearch2.Text.Trim();

                SearchInfo = SearchInfo + " " + this.ddlOperator1.SelectedValue.ToString() + " " + this.ddlFieldList2.SelectedValue.ToString() + " " + this.ddlSrch2.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch2.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }

            if (this.ddlFieldList3.SelectedValue != "00000")
            {
                txtjorbirdate = ((this.ddlFieldList1.SelectedValue.ToString() == "joindate") ? "'" : (this.ddlFieldList1.SelectedValue.ToString() == "birthdate") ? "'" : "");
                txtlike = (this.ddlSrch3.SelectedValue == "like") ? "%'" : "";

                string srch1 = "";
                if (this.ddlFieldList3.SelectedValue.ToString() == "desigid" && this.ddlSrch3.SelectedValue.ToString() == "between")
                    srch1 = this.ddltodesig3.SelectedValue.ToString() + " and " + this.ddldesig03.SelectedValue.ToString();


                else if (this.ddlSrch3.SelectedValue.ToString() == "between")

                    srch1 = this.txtSearch3.Text.Trim() + " and " + this.txttoSearch3.Text.Trim();
                else
                    srch1 = this.txtSearch3.Text.Trim();
                SearchInfo = SearchInfo + " " + this.ddlOperator2.SelectedValue.ToString() + " " + this.ddlFieldList3.SelectedValue.ToString() + " " + this.ddlSrch3.SelectedValue.ToString() + " " + txtjorbirdate + ((this.ddlSrch3.SelectedValue == "like") ? "'" : "") + srch1 + txtjorbirdate + txtlike;

            }

            if (this.ddlOrder1.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder1.SelectedValue.ToString() + " " + this.ddlOrderad1.SelectedValue.ToString() + ", ";

            }
            if (this.ddlOrder2.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder2.SelectedValue.ToString() + " " + this.ddlOrderad2.SelectedValue.ToString() + ", ";

            }
            if (this.ddlOrder3.SelectedValue != "00000")
            {
                orderinfo = orderinfo + " " + this.ddlOrder3.SelectedValue.ToString() + " " + this.ddlOrderad3.SelectedValue.ToString() + ",";

            }
            fieldinfo = ASTUtility.Left(fieldinfo.Trim(), fieldinfo.Trim().Length - 1);
            SearchInfo = SearchInfo.Trim();
            if (orderinfo.Length > 0)
                orderinfo = ASTUtility.Left(orderinfo.Trim(), orderinfo.Trim().Length - 1);
            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTHREMPDYINFORMATION", fieldinfo, SearchInfo, orderinfo, company, "", "", "", "", "");
            if (ds1 == null)
            {
                //this.gvempDyInfo.DataSource = null;
                //this.gvempDyInfo.DataBind();
                //return;
            }
            ViewState["tblRptservices"] = this.HiddenSameData(ds1.Tables[0]);
            //this.Data_Bind();
        }
        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall.Checked)
            {
                for (int i = 0; i < this.cblEmployee.Items.Count; i++)
                {
                    cblEmployee.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i < this.cblEmployee.Items.Count; i++)
                {
                    cblEmployee.Items[i].Selected = false;
                }
            }
            this.cblEmployee_SelectedIndexChanged(null, null);
        }
        protected void cblEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbldyfield"];


            for (int i = 0; i < this.cblEmployee.Items.Count; i++)
            {
                dt.Rows[i]["ffalse"] = (this.cblEmployee.Items[i].Selected) ? "True" : "False";
            }


            DataRow dr1 = dt.NewRow();
            dr1["code"] = "00000";
            dr1["descrip"] = "----selecttion---";
            dr1["ffalse"] = "True";
            dt.Rows.Add(dr1);
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("ffalse like 'True%'");

            //Search Field Option

            this.ddlFieldList1.DataTextField = "descrip";
            this.ddlFieldList1.DataValueField = "code";
            this.ddlFieldList1.DataSource = dv.ToTable();
            this.ddlFieldList1.DataBind();

            this.ddlFieldList2.DataTextField = "descrip";
            this.ddlFieldList2.DataValueField = "code";
            this.ddlFieldList2.DataSource = dv.ToTable();
            this.ddlFieldList2.DataBind();

            this.ddlFieldList3.DataTextField = "descrip";
            this.ddlFieldList3.DataValueField = "code";
            this.ddlFieldList3.DataSource = dv.ToTable();
            this.ddlFieldList3.DataBind();

            this.ddlFieldList1.SelectedValue = "00000";
            this.ddlFieldList2.SelectedValue = "00000";
            this.ddlFieldList3.SelectedValue = "00000";

            // dv.Sort="code";

            this.ddlOrder1.DataTextField = "descrip";
            this.ddlOrder1.DataValueField = "code";
            this.ddlOrder1.DataSource = dv.ToTable();
            this.ddlOrder1.DataBind();

            this.ddlOrder2.DataTextField = "descrip";
            this.ddlOrder2.DataValueField = "code";
            this.ddlOrder2.DataSource = dv.ToTable();
            this.ddlOrder2.DataBind();

            this.ddlOrder3.DataTextField = "descrip";
            this.ddlOrder3.DataValueField = "code";
            this.ddlOrder3.DataSource = dv.ToTable();
            this.ddlOrder3.DataBind();




            this.ddlOrder1.SelectedValue = "00000";
            this.ddlOrder2.SelectedValue = "00000";
            this.ddlOrder3.SelectedValue = "00000";


            dv.RowFilter = ("code not in ('00000')");
            ViewState["tbldyfield"] = dv.ToTable();






        }


        //protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Data_Bind();
        //}

        protected void ddlSrch1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fieldlist1 = (this.ddlFieldList1.Items.Count == 0) ? "AAAAA" : this.ddlFieldList1.SelectedValue.ToString();
            string srchlist1 = this.ddlSrch1.SelectedValue.ToString();

            if (fieldlist1 == "desigid" && srchlist1 == "between")
            {
                this.ddldesig01.Visible = true;
                this.txtSearch1.Visible = false;
                this.lbland1.Visible = true;
                this.ddltodesig1.Visible = true;
                this.txttoSearch1.Visible = false;
            }
            else if (srchlist1 == "between")
            {
                this.ddldesig01.Visible = false;
                this.txtSearch1.Visible = true;
                this.lbland1.Visible = true;
                this.ddltodesig1.Visible = false;
                this.txttoSearch1.Visible = true;
            }


            else
            {
                this.ddldesig01.Visible = false;
                this.txtSearch1.Visible = true;
                this.lbland1.Visible = false;
                this.ddltodesig1.Visible = false;
                this.txttoSearch1.Visible = false;
            }

        }
        protected void ddlSrch2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fieldlist2 = (this.ddlFieldList2.Items.Count == 0) ? "AAAAA" : this.ddlFieldList2.SelectedValue.ToString();
            string srchlist2 = this.ddlSrch2.SelectedValue.ToString();

            if (fieldlist2 == "desigid" && srchlist2 == "between")
            {
                this.ddldesig02.Visible = true;
                this.txtSearch2.Visible = false;
                this.lbland2.Visible = true;
                this.ddltodesig2.Visible = true;
                this.txttoSearch2.Visible = false;
            }
            else if (srchlist2 == "between")
            {
                this.ddldesig02.Visible = false;
                this.txtSearch2.Visible = true;
                this.lbland2.Visible = true;
                this.ddltodesig2.Visible = false;
                this.txttoSearch2.Visible = true;
            }


            else
            {
                this.ddldesig02.Visible = false;
                this.txtSearch2.Visible = true;
                this.lbland2.Visible = false;
                this.ddltodesig2.Visible = false;
                this.txttoSearch2.Visible = false;
            }
        }
        protected void ddlSrch3_SelectedIndexChanged(object sender, EventArgs e)
        {

            string fieldlist3 = (this.ddlFieldList3.Items.Count == 0) ? "AAAAA" : this.ddlFieldList3.SelectedValue.ToString();
            string srchlist3 = this.ddlSrch2.SelectedValue.ToString();

            if (fieldlist3 == "desigid" && srchlist3 == "between")
            {
                this.ddldesig03.Visible = true;
                this.txtSearch3.Visible = false;
                this.lbland3.Visible = true;
                this.ddltodesig3.Visible = true;
                this.txttoSearch3.Visible = false;
            }
            else if (srchlist3 == "between")
            {
                this.ddldesig03.Visible = false;
                this.txtSearch3.Visible = true;
                this.lbland3.Visible = true;
                this.ddltodesig3.Visible = false;
                this.txttoSearch3.Visible = true;
            }


            else
            {
                this.ddldesig03.Visible = false;
                this.txtSearch3.Visible = true;
                this.lbland3.Visible = false;
                this.ddltodesig3.Visible = false;
                this.txttoSearch3.Visible = false;
            }

        }

        protected void ddlFieldList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSrch1_SelectedIndexChanged(null, null);
        }
        protected void ddlFieldList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSrch2_SelectedIndexChanged(null, null);
        }
        protected void ddlFieldList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSrch2_SelectedIndexChanged(null, null);
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
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            if(this.Request.QueryString["Type"]== "AllDoc")
            {
                this.ddlWstation.SelectedValue = "940300000000";
                this.ddlWstation.Enabled = false;
            }
          
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

            this.ddlSection_SelectedIndexChanged(null, null);
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


        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmpName();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblRptEmpInfo"] = ds1.Tables[0];
            DataTable dt = (DataTable)ViewState["tblRptEmpInfo"];
            this.gvempinfo.DataSource = dt;
            this.gvempinfo.DataBind();
        }
        protected void gvempinfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();


                if (ASTUtility.Left(code, 2) == "03")
                {
                    ((Label)e.Row.FindControl("lgcResDesc1")).Text = "Designation";

                    return;
                }
                if (code == "04001" || code == "04002" || code == "04003" || code == "04004" || code == "04101" || code == "04102")
                {
                    ((Label)e.Row.FindControl("lblgvgph")).Text = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ");

                    return;
                }
                if (code == "19001")
                {
                    ((Label)e.Row.FindControl("lgcResDesc1")).Text = "Account No.";

                    return;
                }

            }
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvempDyInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvempDyInfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}