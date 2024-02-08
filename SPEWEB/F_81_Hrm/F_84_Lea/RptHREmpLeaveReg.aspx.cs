using Microsoft.Reporting.WinForms;
using SPEENTITY.C_81_Hrm.C_81_Rec;
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

namespace SPEWEB.F_81_Hrm.F_84_Lea
{
    public partial class RptHREmpLeaveReg : System.Web.UI.Page
    {
        Common Common = new Common();
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Leave Register Report";
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFrmDate.Text = "01-"+date.Substring(3);
                this.txtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                GetWorkStation();
               
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
     
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
           
         
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string printType = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedItem.Text;
            string comnam = hst["comnambn"].ToString();
            string comadd = "উলুসারা,কালিয়াকৈর,গাজীপুর";
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string empid = this.ddlPEmpName.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["tblregdtl"];
            DataTable dt1 = (DataTable)Session["tblemp"];

            DataView dv = dt1.DefaultView;
            dv.RowFilter = "empid='" + empid +"'";
            dt1 = dv.ToTable();
            if ((dt.Rows.Count == 0 || dt == null) || (dt1.Rows.Count == 0 || dt1 == null))
            {
               
                return;
            }



            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptMonWiseEmpLeaveReg>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptHREmpLeaveReg", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("date", dt1.Rows[0]["joindateb"].ToString()));
            rpt1.SetParameters(new ReportParameter("card", dt1.Rows[0]["idcardno"].ToString()));
            rpt1.SetParameters(new ReportParameter("name", dt1.Rows[0]["employeenamb"].ToString()));
            rpt1.SetParameters(new ReportParameter("desi", dt1.Rows[0]["desigb"].ToString()));
            rpt1.SetParameters(new ReportParameter("dept", dt1.Rows[0]["deptnameb"].ToString()));
            rpt1.SetParameters(new ReportParameter("section", dt1.Rows[0]["sectionb"].ToString()));
            rpt1.SetParameters(new ReportParameter("line", dt1.Rows[0]["lineb"].ToString()));
            rpt1.SetParameters(new ReportParameter("rpttitle", "ছুটির রেজিস্টার ও ছুটির বহি"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
            string joblocation = "87%";
            string txtSProject = (qempid.Length > 0 ? qempid : "%" + this.txtNSrcEmp.Text) + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETNEWPNAME", "", txtSProject, joblocation, userid, "", "", "", "", "");
            this.ddlNPEmpName.DataTextField = "empname";
            this.ddlNPEmpName.DataValueField = "empid";
            this.ddlNPEmpName.DataSource = ds5.Tables[0];
            this.ddlNPEmpName.DataBind();
        }
        private void GetDivision()
        {



            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
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
        protected void ddlPEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComASecSelected();
        }
        private void GetComASecSelected()
        {

            string empid = this.ddlPEmpName.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            
                this.ddlWstation.SelectedValue = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                this.ddlDivision.SelectedValue = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["divcode"].ToString();
                this.ddlDept.SelectedValue = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
                this.ddlSection.SelectedValue = ((DataTable)Session["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
           

        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetAllOrganogramList();
            this.GetDivision();
        }
        protected void ibtnFindEmp_Click(object sender, EventArgs e)
        {
            this.ddlSection_SelectedIndexChanged(null, null);
        }
        private void GetWorkStation()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

           


            var lst = getlist.GetWstation(comcod, userid);

          
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();

          

            
                    this.ddlWstation.SelectedValue = "940100000000";
              
                this.ddlWstation_SelectedIndexChanged(null, null);
           
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
        }

        protected void ddlWstation1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetAllOrganogramList();
            this.GetDivision();
        }
        private void GetDeptList()
        {
            
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            
            
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
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionList();
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string empid = this.ddlPEmpName.SelectedValue.ToString().Trim();
            string FrmDate = this.txtFrmDate.Text.ToString().Trim();
            string ToDate = this.txtToDate.Text.ToString().Trim();
            
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETLEAVREGISTERLEAVEBOOK", empid, FrmDate, ToDate, "", "", "", "");
            this.gvLeaveRegStatus.DataSource = ds4.Tables[0];
            this.gvLeaveRegStatus.DataBind();
            Session["tblregdtl"] = ds4.Tables[0];
        }

        private void GetSectionList()
        {
            
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            
                //string wstation = this.ddlDept.SelectedValue.ToString();//940100000000

                //var lst1 = lst.FindAll(x => x.actcode.Substring(0, 9) == wstation.Substring(0, 9) && x.actcode != wstation);



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

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {

            Session.Remove("tblemp");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();

            string emptype = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4))+ "%";
            string div = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string department = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%");

            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";

            //string ProjectCode = (this.txtSrcEmp.Text.Trim().Length > 0) ? "%" : this.ddlSection.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcEmp.Text + "%";
            string joblocation = "87%";

            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", section, txtSProject, emptype, div, department, joblocation, userid, "", "");
           // DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", section, txtSProject, emptype, div, department, "", "", "", "");
            this.ddlPEmpName.DataTextField = "empname";
            this.ddlPEmpName.DataValueField = "empid";
            this.ddlPEmpName.DataSource = ds5.Tables[0];
            this.ddlPEmpName.DataBind();
            


            Session["tblemp"] = ds5.Tables[0];
            //    this.GetComASecSelected();
        }


    }


}