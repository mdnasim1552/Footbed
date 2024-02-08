using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_81_Hrm.F_82_App
{
    public partial class EmpAppPart : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE APPLICATION'S PART";
            }



        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
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
          
            // this Part Only For Empty Dataset in RDLC 
            string emptype = "9403%";
            string div =  "%" ;
            string deptname =  "%";
            string section ="%" ;
            string empidN = "%";
            string frmdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string todate =System.DateTime.Now.ToString("dd-MMM-yyyy");

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "RPTALLEMPLISDATEWISE", emptype, div, empidN, deptname, section, frmdate, todate, "", "");
         
            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];

            var lst1 = dt.DataTableToList<SPEENTITY.C_81_Hrm.F_92_Mgt.BO_EmpDashboard.AllEmpInformationGrpwise>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_82_App.RptAppsPart", lst1, null, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Applicant's Part"));
            rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
          


            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}