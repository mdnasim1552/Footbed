using SPELIB;
using SPEENTITY.C_81_Hrm.C_81_Rec;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_81_Hrm.F_92_Mgt
{
    public partial class RpEmpIncPro : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Promotion") ? "Periodic Promotion List" : "EMPLOYEE INCREMENT SHEET";


                GetWorkStation();
                GetAllOrganogramList();
                GetJobLocation();
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.AddMonths(1).ToString("dd-MMM-yyyy");
                this.SelectType();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "Increment":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "Promotion":
                    this.MultiView1.ActiveViewIndex =1;
                    break;



            }



        }
        private void GetJobLocation()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GetJobLocation(comcod, userid);
            this.ddlJob.DataTextField = "location";
            this.ddlJob.DataValueField = "loccode";
            this.ddlJob.DataSource = lst;
            this.ddlJob.DataBind();
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
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

            this.ddlSection1.DataTextField = "actdesc";
            this.ddlSection1.DataValueField = "actcode";
            this.ddlSection1.DataSource = lst1;
            this.ddlSection1.DataBind();
            this.ddlSection1.SelectedValue = "000000000000";
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
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "Increment":
                    this.ShowIncrement();
                    break;

                case "Promotion":
                    this.ShowPromotion();
                    break;

            }
        }

        private void ShowPromotion()
        {
            ViewState.Remove("tblempstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string CompanyName = (this.ddlWstation.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dpt = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection1.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection1.SelectedValue.ToString()) + "%";
            string JobLocation = (this.ddlJob.SelectedValue.ToString() == "00000" ? "87" : this.ddlJob.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "RPTEMPLOYEE_PROMOTIONLIST", CompanyName, dpt, section, frmdate, todate, division, JobLocation, userid, "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvPromolist.DataSource = null;
                this.gvPromolist.DataBind();
                return;

            }
            DataTable dt = ds3.Tables[0];
            ViewState["tblempstatus"] = this.HiddenSameData(dt);
            this.Data_Bind();

        }
        private void ShowIncrement()
        {
            ViewState.Remove("tblempstatus");
            string comcod = this.GetCompCode();
            string CompanyName = this.ddlWstation.SelectedValue.ToString().Substring(0, 4) + "%";
            string division = (this.ddlDivision.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string dpt = (this.ddlDept.SelectedValue.ToString() == "000000000000" ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = (this.ddlSection1.SelectedValue.ToString() == "000000000000" ? "" : this.ddlSection1.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "RPTEMPLOYEEINCREMENT", CompanyName, dpt, section, frmdate, todate, division, "", "", "");
            if (ds3 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvEmpinc.DataSource = null;
                this.gvEmpinc.DataBind();
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


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "Increment":

                    string deptid = dt1.Rows[0]["deptid"].ToString();
                    string secid = dt1.Rows[0]["secid"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {

                        if (dt1.Rows[j]["deptid"].ToString() == deptid && dt1.Rows[j]["secid"].ToString() == secid)
                        {

                            dt1.Rows[j]["deptname"] = "";
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["deptid"].ToString() == deptid)
                                dt1.Rows[j]["deptname"] = "";

                            if (dt1.Rows[j]["secid"].ToString() == secid)
                                dt1.Rows[j]["section"] = "";



                        }

                        deptid = dt1.Rows[j]["deptid"].ToString();
                        secid = dt1.Rows[j]["secid"].ToString();
                    }
                    break;

                case "Promotion":

                    string deptid1 = dt1.Rows[0]["deptid"].ToString();
                    string secid1 = dt1.Rows[0]["secid"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {

                        if (dt1.Rows[j]["deptid"].ToString() == deptid1 && dt1.Rows[j]["secid"].ToString() == secid1)
                        {

                            dt1.Rows[j]["deptname"] = "";
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["deptid"].ToString() == deptid1)
                                dt1.Rows[j]["deptname"] = "";

                            if (dt1.Rows[j]["secid"].ToString() == secid1)
                                dt1.Rows[j]["section"] = "";



                        }

                        deptid1 = dt1.Rows[j]["deptid"].ToString();
                        secid1 = dt1.Rows[j]["secid"].ToString();
                    }
                    break;


            }



            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblempstatus"];
            if (dt.Rows.Count == 0)
            {
                this.gvEmpinc.DataSource = null;
                this.gvEmpinc.DataBind();
                this.gvPromolist.DataSource = null;
                this.gvPromolist.DataBind();
                return;
            }
               
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Increment":
                   

                    this.gvEmpinc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpinc.DataSource = dt;
                    this.gvEmpinc.DataBind();

                    Session["Report1"] = gvEmpinc;
                    ((HyperLink)this.gvEmpinc.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl =
                        "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    this.FooterCalculation();
                    break;
                case "Promotion":
                 

                    this.gvPromolist.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPromolist.DataSource = dt;
                    this.gvPromolist.DataBind();

                    Session["Report1"] = gvPromolist;
                    ((HyperLink)this.gvPromolist.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl =
                        "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    this.FooterCalculation();
                    break;
            }



        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblempstatus"];

            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {


                case "Increment":
                    ((Label)this.gvEmpinc.FooterRow.FindControl("lblgvFpresalary")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(presal)", "")) ? 0.00 : dt.Compute("sum(presal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpinc.FooterRow.FindControl("lblgvFgvincam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(incam)", "")) ? 0.00 : dt.Compute("sum(incam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpinc.FooterRow.FindControl("lblgvFsalaincam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salaincmnt)", "")) ? 0.00 : dt.Compute("sum(salaincmnt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Increment":
                    this.PrintIncrement();
                    break;
                case "Promotion":
                    this.PrintPromotion();
                    break;
            }

        }
        private void PrintIncrement()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstate = new RMGiRPT.R_81_Hrm.R_92_Mgt.RptEmpIncrement();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtDateRange = rptstate.ReportDefinition.ReportObjects["txtDateRange"] as TextObject;
            txtDateRange.Text = ""; /*"(From "+this.txtfromdate.Text+" To "+this.txttodate.Text+")";*/

            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)ViewState["tblempstatus"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            //this.lbljavascript.Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintPromotion()
        {
            DataTable dt = (DataTable)ViewState["tblempstatus"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compName = hst["comnam"].ToString();
            string comAdd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string fromdate = Convert.ToDateTime( this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy"); 
            
            var lst = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_92_Mgt.EClassHrInterface.EmpPromotion>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_92_MGT.RptPromotion", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", compName));
            rpt1.SetParameters(new ReportParameter("comadd", comAdd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("date", "Date: "+fromdate + " To "+ todate));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Periodic Promotion List"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }        
        protected void gvEmpinc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpinc.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvPromolist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPromolist.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}