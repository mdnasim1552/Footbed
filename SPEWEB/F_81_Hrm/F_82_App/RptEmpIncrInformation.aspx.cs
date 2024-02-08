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

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class RptEmpIncrInformation : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE INCREMENT LETTER";
                GetWorkStation();
                GetAllOrganogramList();
                string Type = this.Request.QueryString["Type"]??"";
                if(Type== "AllDoc")
                {
                    this.Okbtn.Visible = true;
                    //this.Daterange.Visible = true;
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + date.Substring(2);
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
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

        private void GetEmpName()
        {

            string comcod = this.GetComeCode();

        
            string emptype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dpt = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = this.txtfromdate.Text;
            string todate = this.txttodate.Text;
            //dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT '5305','INCREMENT_APPROVED_EMP','9403%','%','%', '01-Jan-2021', '31-Dec-2021'
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "INCREMENT_APPROVED_EMP", emptype, dpt, section, frmdate, todate, "", "", "", "");

            if (ds3 == null)
            {
                return;
            }
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds3.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();


            ViewState["empname"] = ds3.Tables[0];


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string frmdate = this.txtfromdate.Text;
            string todate = this.txttodate.Text;
            string emptype = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dpt = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string refno = this.txtrefno.Text;
            string empidN = "";
            string[] empid = this.ddlEmpNameAllInfo.Text.Trim().Split(',');
           
            if (empid[0].Substring(0, 3) == "000")
                empidN = "";
            else
                foreach (ListItem item in ddlEmpNameAllInfo.Items)
                {
                    if (item.Selected)
                    {
                        if (item.Selected)
                        {
                            empidN += item.Value;
                        }
                    }
                }
            DataSet ds= HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "INCREMENT_APPROVED_EMP_BYID", empidN, emptype, dpt, section, frmdate,todate, "", "","");

            var lst1 = ds.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_82_App.EmployeeInfo.EmployeeIncrInfo>();
            string currntdate = System.DateTime.Now.ToString("dd/MM/yyyy");
            string currntyear = System.DateTime.Now.ToString("yyyy");

            for (var item =0;lst1.Count> item; item++)
            {
                double grossAm = lst1[item].grossal + lst1[item].incamt;
                lst1[item].Gross = ASTUtility.Trans(lst1[item].grossal, 2);

                lst1[item].strGross = ASTUtility.Trans(grossAm, 2);
                lst1[item].strIncAm = ASTUtility.Trans(lst1[item].incamt, 2);
                
            }
            

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptEmpIncrletter", lst1, null, null);
            Rpt1.EnableExternalImages = true;
   
            Rpt1.SetParameters(new ReportParameter("currntdate", currntdate));
            Rpt1.SetParameters(new ReportParameter("currntyear", currntyear));
            Rpt1.SetParameters(new ReportParameter("refno", refno));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }





        private void ShowName()
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTEMPINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        }

        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            //this.GetEmpName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        private void Data_Bind()
        {
 
        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
             this.GetEmpName();
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
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_Bind();
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
            lst.Add(new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1("000000000000", "ALL","","","",""));
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            Session["hrcompnameadd"] = lst;
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
            string type = this.Request.QueryString["Type"] ?? "";
            if (type == "Datewise")
            {
                this.GetEmpDatewise();
            }
            else
            {
                GetEmpName();
            }
            


        }

        private void GetEmpDatewise()
        {
            string comcod = this.GetComeCode();
            //string ProjectCode = (this.txtEmpSrc.Text.Trim().Length > 0) ? "%" : (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
           

            string CompanyName = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";

            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dept = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = this.txtfromdate.Text.ToString();
            string todate = this.txttodate.Text.ToString();

            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETEMPNAMEDATEWISE", frmdate, todate, CompanyName, division, division, dept, section, "1");

            if (ds5 == null)
            {

                return;
            }

            DataTable dt1 = ds5.Tables[0].Copy();
            DataView dv1 = dt1.DefaultView;
            dt1 = dv1.ToTable().DefaultView.ToTable(true, "empid", "empname");
            dt1.Rows.Add("000000000000", "ALL Employee");

            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = dt1;
            this.ddlEmpNameAllInfo.DataBind();
            this.ddlEmpNameAllInfo.SelectedValue = "000000000000";

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
    }
}