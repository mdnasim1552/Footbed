using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using Microsoft.Reporting.WinForms;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using SPELIB;
using SPERDLC;
using System.Net;
using System.Net.Mail;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptDayWiseEmploySalary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../../AcceessError.aspx");
                this.GetDate();
                // this.GetCompany();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "MONTH WISE EMPLOYEE SALARY";
                // this.lblmsg.Visible = false;

                this.GetWorkStation();
                this.GetAllOrganogramList();
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


            this.ddlWstation_SelectedIndexChanged(null, null);
        }

        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }

        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString(); //940100000000
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst =
                (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x =>
                x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" &&
                x.actcode != wstation);

            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all =
                new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf
                { actcode = "000000000000", actdesc = "All Division" };
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
            string wstation = this.ddlDivision.SelectedValue.ToString(); //940100000000

            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst =
                (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x =>
                x.actcode.Substring(0, 7) == wstation.Substring(0, 7) && x.actcode.Substring(9) == "000" &&
                x.actcode != wstation);

            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all =
                new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf
                { actcode = "000000000000", actdesc = "All Department" };
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

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetSectionList()
        {
            string wstation = this.ddlDept.SelectedValue.ToString(); //940100000000
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst =
                (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);


            SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all =
                new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf
                { actcode = "000000000000", actdesc = "All Section" };
            lst1.Add(all);


            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = lst1;
            this.ddlSection.DataBind();

            this.ddlSection.SelectedValue = "000000000000";

            GetEmployeeName();



        }





        private void GetEmployeeName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            //string ProjectCode = (this.txtEmpSrc.Text.Trim().Length > 0) ? "%" : (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string txtSProject = "%%";

            string CompanyName = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";

            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string projectcode = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string joblocation = "87%";


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", CompanyName, txtSProject, division, projectcode, section, "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowSal();
        }


        private void ShowSal()
        {


            //  Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string projectcode = this.ddlSection.SelectedValue.ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            //   string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            //string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
            // mon = this.Datediffday1(Convert.ToDateTime(curdate), Convert.ToDateTime(dt1));


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_SALARYSUMMEY", "GETDATEWISEEMPSALARYINF",
                frmdate, todate, empid, "", "", "", "", "", "");
            Session["gvData"] = ds1.Tables[0];

            //this.gvSalSumDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvpayroll.DataSource = ds1.Tables[0];
            this.gvpayroll.DataBind();

        }




        private void GetDate()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "4301":
                    //case "4305":
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "26" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1)
                        .ToString("dd-MMM-yyyy");
                    break;

                default:
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1)
                        .ToString("dd-MMM-yyyy");
                    break;


            }


        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }






        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string refno = dt1.Rows[0]["refno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["refno"].ToString() == refno)
                {
                    refno = dt1.Rows[j]["refno"].ToString();
                    dt1.Rows[j]["refdesc"] = "";
                }
                else
                {
                    refno = dt1.Rows[j]["refno"].ToString();
                }
            }

            return dt1;

        }

        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tblSalSum"];
            //this.gvSalSumDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvpayroll.DataSource = dt;
            this.gvpayroll.DataBind();
            //this.FooterCalculation();


        }


        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvpayroll.FooterRow.FindControl("lgvFTCurEmp")).Text = Convert
                .ToDouble((Convert.IsDBNull(dt.Compute("sum(curempno)", "")) ? 0.00 : dt.Compute("sum(curempno)", "")))
                .ToString("#,##0;(#,##0); ");
            ((Label)this.gvpayroll.FooterRow.FindControl("lgvFTCurMamt")).Text = Convert
                .ToDouble((Convert.IsDBNull(dt.Compute("sum(curpay)", "")) ? 0.00 : dt.Compute("sum(curpay)", "")))
                .ToString("#,##0;(#,##0); ");
            ((Label)this.gvpayroll.FooterRow.FindControl("lgvFTPreEmp")).Text = Convert
                .ToDouble((Convert.IsDBNull(dt.Compute("sum(preempno)", "")) ? 0.00 : dt.Compute("sum(preempno)", "")))
                .ToString("#,##0;(#,##0); ");
            ((Label)this.gvpayroll.FooterRow.FindControl("lgvFTPreMamt")).Text = Convert
                .ToDouble((Convert.IsDBNull(dt.Compute("sum(prepay)", "")) ? 0.00 : dt.Compute("sum(prepay)", "")))
                .ToString("#,##0;(#,##0); ");


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            // Hashtable hst = (Hashtable)Session["tblLogin"];
            // string comcod = this.GetCompCode();
            // string comnam = hst["comnam"].ToString();
            // string comadd = hst["comadd1"].ToString();  //address
            // string compname = hst["compname"].ToString();
            // string username = hst["username"].ToString();
            // DataTable dt = (DataTable)Session["gvData"];
            // if (dt.Rows.Count == 0)
            // {
            //     return;

            // }
            // string deapartment = this.ddlProjectName.SelectedItem.Text.Trim().ToString();

            // string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            // string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            // string empid = (this.ddlEmpName.SelectedValue.ToString());
            // string EmpId = dt.Rows[0]["cardno"].ToString();
            // string designation = dt.Rows[0]["desig"].ToString();
            // string acno= dt.Rows[0]["acno"].ToString(); 
            // string joiningdat = Convert.ToDateTime(dt.Rows[0]["joining"]).ToString("dd-MMM-yyyy");
            // string separation = (Convert.ToDateTime(dt.Rows[0]["separation"]).ToString("dd-MMM-yyyy")=="01-Jan-1900")?"": Convert.ToDateTime(dt.Rows[0]["separation"]).ToString("dd-MMM-yyyy");
            // string empname = this.ddlEmpName.SelectedItem.Text.Substring(7).Trim().ToString();
            // string servlen = dt.Rows[0]["servlen"].ToString(); 
            // string section =dt.Rows[0]["zone"].ToString(); ;
            // string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // string DateFT = "(From " + txtfromdate + " To " + todate + ")";
            // string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;



            // var lst = dt.DataTableToList<MFGOBJ.C_81_Hrm.C_89_Pay.BO_EmpSalary.DaywiseSalary>();


            // LocalReport rpt1 = new LocalReport();

            // rpt1 = RptSetupClass1.GetLocalReport("RD_81_Hrm.RD_89_Pay.RptDayWiseSalary", lst, null, null);

            //// rpt1.EnableExternalImages = true;
            // rpt1.SetParameters(new ReportParameter("comnam", comnam)); 
            // rpt1.SetParameters(new ReportParameter("comadd", comadd));
            // rpt1.SetParameters(new ReportParameter("DateFT", DateFT));
            // rpt1.SetParameters(new ReportParameter("RptTitle", "Individual Salary Statement"));
            // rpt1.SetParameters(new ReportParameter("DateFT", DateFT));
            // rpt1.SetParameters(new ReportParameter("empname", empname));
            // rpt1.SetParameters(new ReportParameter("joiningdat", joiningdat)); 
            // rpt1.SetParameters(new ReportParameter("separation", separation)); 
            // rpt1.SetParameters(new ReportParameter("servlen", servlen)); 
            // rpt1.SetParameters(new ReportParameter("empid", EmpId));
            // rpt1.SetParameters(new ReportParameter("designation", designation));
            // rpt1.SetParameters(new ReportParameter("acno", acno));
            // rpt1.SetParameters(new ReportParameter("zone", section));
            // rpt1.SetParameters(new ReportParameter("deapartment", deapartment));


            // rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //// rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            // Session["Report1"] = rpt1;
            // ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
            //     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }



        protected void gvSalSumDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvpayroll.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }


        protected void imgbtnEmpName_OnClick(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
    }
}