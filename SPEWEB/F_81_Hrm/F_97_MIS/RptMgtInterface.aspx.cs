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

using SPEENTITY.C_81_Hrm.C_81_Rec;
using SPEENTITY.C_81_Hrm.C_84_Lea;

namespace SPEWEB.F_81_Hrm.F_97_MIS
{
    public partial class RptMgtInterface : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFdate.Text = "01" + date.Substring(2);
                this.txtTdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                GetWorkStation();
                GetAllOrganogramList();

                this.GetDynamcifield();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE SALARY INFORMATION";
                this.ddlReportLevel.SelectedIndex = 0;
                this.lnkbtnShow_Click(null, null);
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void GetWorkStation()
        {

            string comcod = GetCompCode();
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



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowMgtInterface();
        }

        private void GetDynamcifield()
        {
            //ViewState.Remove("tbldyfield");
            string comcod = this.GetCompCode();

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "RPTMGTDYNAMICFIELD", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.ddlOrder.Items.Clear();
                return;
            }

            this.ddlOrder.DataTextField = "descrip";
            this.ddlOrder.DataValueField = "code";
            this.ddlOrder.DataSource = ds4.Tables[0];
            this.ddlOrder.DataBind();

        }


        private void ShowMgtInterface()
        {
            ViewState.Remove("tblempstatus");
            string comcod = this.GetCompCode();
            string fdate = Convert.ToDateTime(this.txtFdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string tdate = Convert.ToDateTime(this.txtTdate.Text.Trim()).ToString("dd-MMM-yyyy");

            string empType = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";

            string order = "";
            if (this.ddlOrder.SelectedValue != "00000")
            {
                order = this.ddlOrder.SelectedValue.ToString() + " " + this.ddlOrderad1.SelectedValue.ToString();
            }

            string label = this.ddlReportLevel.SelectedValue.ToString();

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MGT_INTERFACE", "RPTMGTINTERFACE", fdate, tdate, label, order, empType, "", "", "", "");

            if (ds3 == null)
            {


                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            ViewState["tblempstatus"] = this.HiddenSameData(dt);
            this.Data_Bind();

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string company = dt1.Rows[0]["company"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            string deptid = dt1.Rows[0]["deptid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    dt1.Rows[j]["section"] = "";
                }
                if (dt1.Rows[j]["deptid"].ToString() == deptid)
                {
                    dt1.Rows[j]["department"] = "";
                }

                if (dt1.Rows[j]["company"].ToString() == company)
                {
                    dt1.Rows[j]["companyname"] = "";
                }

                company = dt1.Rows[j]["company"].ToString();
                deptid = dt1.Rows[j]["deptid"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();



            }

            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblempstatus"];


            this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpList.DataSource = dt;
            this.gvEmpList.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblempstatus"];

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvEmpList.FooterRow.FindControl("lgvFNoEmp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(noemp)", "")) ? 0.00 : dt.Compute("sum(noemp)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpList.FooterRow.FindControl("lgvFsalary")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");





        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string daterange = "(From  " + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyy") + " To " + Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyy") + ")";
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;



            DataTable dt = (DataTable)ViewState["tblempstatus"];
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var rptlist = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpSalInf>();

            LocalReport rpt1 = new LocalReport();

            //rpt1 = RMGiRDLC.RptSetupClass.GetLocalReport("RD_81_HRM.RD_97_MIS.RptEmpSalInform", rptlist, null, null);
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_97_MIS.RptEmpSalInform", rptlist, null, null);

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "EMPLOYEE SALARY INFORMATION"));
            rpt1.SetParameters(new ReportParameter("FDate", daterange));

            Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        //    ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptGradeWiseEmp();
        //    TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
        //    rptCname.Text = comnam;


        //    TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
        //    rptstate.SetDataSource((DataTable)ViewState["tblempstatus"]);
        //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    //rptstate.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptstate;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}






        //private void PrintEmpSalInf()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string daterange = "(From  " + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyy") + " To " + Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyy") + ")";
        //    string session = hst["session"].ToString();
        //    string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;



        //    DataTable dt = (DataTable)ViewState["tblempstatus"];
        //    string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
        //    var rptlist = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpSalInf>();

        //    LocalReport rpt1 = new LocalReport();

        //    rpt1 = RMGiRDLC.RptSetupClass.GetLocalReport("RD_97_MIS.RptEmpSalInform", rptlist, null, null);

        //    rpt1.EnableExternalImages = true;
        //    rpt1.SetParameters(new ReportParameter("comnam", comnam));
        //    rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
        //    rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
        //    rpt1.SetParameters(new ReportParameter("comadd", comadd));
        //    rpt1.SetParameters(new ReportParameter("RptTitle", "EMPLOYEE SALARY INFORMATION"));
        //    rpt1.SetParameters(new ReportParameter("FDate", daterange));

        //    Session["Report1"] = rpt1;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
        //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //}


        protected void gvEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvEmpList.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        protected void ddlReportLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlReportLevel.SelectedValue.ToString() == "1")
            {
                this.gvEmpList.Columns[3].Visible = false;
                this.gvEmpList.Columns[4].Visible = false;
                this.gvEmpList.Columns[5].Visible = false;
                this.gvEmpList.Columns[6].Visible = false;
                this.gvEmpList.Columns[9].Visible = false;
                this.gvEmpList.Columns[13].Visible = false;
            }
            else if (this.ddlReportLevel.SelectedValue.ToString() == "2")
            {
                this.gvEmpList.Columns[3].Visible = true;
                this.gvEmpList.Columns[4].Visible = false;
                this.gvEmpList.Columns[5].Visible = false;
                this.gvEmpList.Columns[6].Visible = false;
                this.gvEmpList.Columns[9].Visible = false;
                this.gvEmpList.Columns[13].Visible = false;

            }
            else if (this.ddlReportLevel.SelectedValue.ToString() == "3")
            {
                this.gvEmpList.Columns[3].Visible = true;
                this.gvEmpList.Columns[4].Visible = true;
                this.gvEmpList.Columns[5].Visible = false;
                this.gvEmpList.Columns[6].Visible = false;
                this.gvEmpList.Columns[9].Visible = false;
                this.gvEmpList.Columns[13].Visible = false;

            }
            else if (this.ddlReportLevel.SelectedValue.ToString() == "4")
            {
                this.gvEmpList.Columns[3].Visible = true;
                this.gvEmpList.Columns[4].Visible = true;
                this.gvEmpList.Columns[5].Visible = false;
                this.gvEmpList.Columns[6].Visible = false;
                this.gvEmpList.Columns[9].Visible = false;
                this.gvEmpList.Columns[13].Visible = false;
            }
            else
            {
                //
            }

        }
        protected void gvEmpList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink serperiod = (HyperLink)e.Row.FindControl("hlnkgvserperiod");
                HyperLink hlnksalary = (HyperLink)e.Row.FindControl("hlnksalary");
                HyperLink Late = (HyperLink)e.Row.FindControl("hlnkgvLate");
                Label joinning = (Label)e.Row.FindControl("lblgvjoindateemp");

                string comcod = this.GetCompCode();
                string Empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();

                string jdat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "jdat")).ToString();

                if (jdat == "1")
                {
                    joinning.Style.Add("color", "red");
                }

                //---------------------Salary-----------------------//
                if (Empid != "000000000000")
                {
                    hlnksalary.Font.Bold = true;
                    hlnksalary.NavigateUrl = "LinkMgtInterface.aspx?Type=EmpSal&comcod=" + comcod + "&empid=" + Empid;
                }
                //---------------------Late-----------------------//

                Late.Font.Bold = true;
                Late.NavigateUrl = "LinkMgtInterface.aspx?Type=LateStatus&comcod=" + comcod + "&empid=" + Empid + "&Date1=" + Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy") +
                        "&Date2=" + Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");

                //---------------------Service period-----------------------//

                serperiod.Style.Add("color", "blue");
                serperiod.NavigateUrl = "LinkMgtInterface.aspx?Type=Services&comcod=" + comcod + "&empid=" + Empid + "&Date=" + Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");

            }

        }



        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }

        private void GetDivision()
        {

            string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            string comcod = GetCompCode();
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

            string comcod = GetCompCode();
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
            string comcod = GetCompCode();
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
            // GetEmpName();
        }
    }
}