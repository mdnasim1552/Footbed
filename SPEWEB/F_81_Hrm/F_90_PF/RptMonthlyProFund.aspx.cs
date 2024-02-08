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
using System.Drawing;
using SPEENTITY.C_81_Hrm.C_81_Rec;

namespace SPEWEB.F_81_Hrm.F_90_PF
{
    public partial class RptMonthlyProFund : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                GetAllOrganogramList();
                GetWorkStation();
                this.GetMonth();
                this.SelectView();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SelectView()
        {
            string quType = this.Request.QueryString["Type"] ?? "";
            switch (quType)
            {
                case "MonthlyPF":
                    this.Multiview1.ActiveViewIndex = 0;
                    break;

                default:
                    this.Multiview1.ActiveViewIndex = 1;
                    break;
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
        private void GetWorkStation()
        {

            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
            lst = lst.Where(item => item.actcode != "000000000000").ToList();


            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            Session["hrcompnameadd"] = lst;

            this.ddlWstation_SelectedIndexChanged(null, null);

        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllOrganogramList();
            this.GetDivision();
        }
        private void GetDivision()
        {
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];
            List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> allData = new List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>();
            List<string> wstation = new List<string>();
            foreach (ListItem litem in ddlWstation.Items)
            {

                if (litem.Selected)
                {
                    wstation.Add(litem.Value);

                }

            }

            if (wstation.Count != 0)
            {
                foreach(var item in wstation)
                {
                    var lst1 = lst.FindAll(x =>
                    x.actcode.Substring(0, 4) == item.Substring(0, 4) &&
                    x.actcode.Substring(7) == "00000" &&
                    x.actcode != item);

                    allData.AddRange(lst1);
                }
            }
            allData.Add(new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" });


            //string wstation = this.ddlWstation.SelectedValue.ToString();//940100000000
            //string comcod = GetCompCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string userid = hst["usrid"].ToString();

            //List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf> lst = (List<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf>)ViewState["lstOrganoData"];

            //var lst1 = lst.FindAll(x => x.actcode.Substring(0, 4) == wstation.Substring(0, 4) && x.actcode.Substring(7) == "00000" && x.actcode != wstation);
            //SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf all = new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" };
            //lst1.Add(all);


            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = allData;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "000000000000";

            this.ddlDivision_SelectedIndexChanged(null, null);

        }

        private void GetMonth()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlMonth.DataTextField = "yearmon";
            this.ddlMonth.DataValueField = "ymon";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
        }
       

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptList();
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
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionList();
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

        }
      
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string quType = this.Request.QueryString["Type"] ?? "";
            switch (quType)
            {
                case "MonthlyPF":
                    this.ShowMonthlyPF();
                    break;

                default:
                    this.ShowMonthlyPFPaySheet();
                    break;
            }
           
        }

        private void ShowMonthlyPFPaySheet()
        {
            List<string> wstationList = new List<string>();
            foreach (ListItem litem in ddlWstation.Items)
            {

                if (litem.Selected)
                {
                    wstationList.Add(litem.Value);

                }

            }
            string wstationcode = "94%";
            if (wstationList.Count != 0)
            {
                wstationcode = string.Join(", ", wstationList);
            }

            Session.Remove("tblprofund");
            Session.Remove("tblprofund2");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            //string CompanyName = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string CompanyName = wstationcode;
            string division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptname = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string manthid = this.ddlMonth.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "SHOWMONTHLYPFPAYSHEET", CompanyName, division, deptname, section, manthid, usrid);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvProFund.DataSource = null;
                this.gvProFund.DataBind();
                return;
            }

            Session["tblprofund"] = ds1.Tables[0];
            Session["tblprofund2"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void ShowMonthlyPF()
        {
            List<string> wstationList = new List<string>();
            foreach (ListItem litem in ddlWstation.Items)
            {

                if (litem.Selected)
                {
                    wstationList.Add(litem.Value);

                }

            }
            string wstationcode = "94%";
            if (wstationList.Count != 0)
            {
                wstationcode = string.Join(", ", wstationList);
            }

            Session.Remove("tblprofund");
            Session.Remove("tblprofund2");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            //string CompanyName = ((this.ddlWstation.SelectedValue.ToString() == "000000000000") ? "94" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)) + "%";
            string CompanyName = wstationcode;
            string division = ((this.ddlDivision.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDivision.SelectedValue.ToString().Substring(0, 7)) + "%";
            string deptname = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string manthid = this.ddlMonth.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PFACCOUNTS", "SHOWMONTHLYPF", CompanyName, division, deptname, section, manthid, usrid);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found!');", true);
                this.gvMonthlyPF.DataSource = null;
                this.gvMonthlyPF.DataBind();
                return;
            }

            Session["tblprofund"] = ds1.Tables[0];
            Session["tblprofund2"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            string quType = this.Request.QueryString["Type"] ?? "";

            string searchcard = this.txtEmpSearch.Text.Trim();
            switch (quType)
            {
                case "MonthlyPF":
                    try
                    {
                        DataTable dt = (DataTable)Session["tblprofund"];
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = "idcard like '%" + searchcard + "%'";
                        dt = dv.ToTable();

                        this.gvMonthlyPF.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
                        this.gvMonthlyPF.DataSource = dt;
                        this.gvMonthlyPF.DataBind();
                        this.FooterCalCulation(dt);
                        Session["tblprofund"] = dt;
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                    }
                    break;

                default:
                    try
                    {
                        DataTable dt = (DataTable)Session["tblprofund"];
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = "idcard like '%" + searchcard + "%'";
                        dt = dv.ToTable();
                        this.gvProFund.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
                        this.gvProFund.DataSource = dt;
                        this.gvProFund.DataBind();
                        this.FooterCalCulation(dt);
                        Session["tblprofund"] = dt;
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                    }
                    break;
            }           
           
        }

        private void FooterCalCulation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            string quType = this.Request.QueryString["Type"] ?? "";
            switch (quType)
            {
                case "MonthlyPF":
                    ((Label)this.gvMonthlyPF.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gspay)", "")) ? 0.00 : dt.Compute("sum(gspay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonthlyPF.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonthlyPF.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");

                    Session["Report1"] = gvMonthlyPF;
                    ((HyperLink)this.gvMonthlyPF.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                default:
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnamt)", "")) ? 0.00 : dt.Compute("sum(opnamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(comconpfamt)", "")) ? 0.00 : dt.Compute("sum(comconpfamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProFund.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payablepfamt)", "")) ? 0.00 : dt.Compute("sum(payablepfamt)", ""))).ToString("#,##0;(#,##0); ");

                    Session["Report1"] = gvProFund;
                    ((HyperLink)this.gvProFund.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
            }
           
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string quType = this.Request.QueryString["Type"] ?? "";
            switch (quType)
            {
                case "MonthlyPF":
                    this.PrintMonthlyPF();
                    break;

                default:
                    switch (comcod)
                    {
                        case "5305":
                        case "5306":
                        case "5301":
                            this.PrintPFPaySheetFB();
                            break;

                        default:
                            this.PrintPfFund();
                            break;

                    }
                    break;
            }

            

        }
        private void PrintMonthlyPF()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comaddf"].ToString();
            string comnam = hst["comnam"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string month = this.ddlMonth.SelectedItem.Text;
            DataTable dt1 = (DataTable)Session["tblprofund"];

            var lst = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_90_PF.BO_ClassPF>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_90_PF.RptMonthlyPFFB", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            rpt1.SetParameters(new ReportParameter("rptTitle", "C.P.F Sheet For the Month of - " + month));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintPFPaySheetFB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comaddf"].ToString();
            string comnam = hst["comnam"].ToString();
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string month = this.ddlMonth.SelectedItem.Text;
            DataTable dt1 = (DataTable)Session["tblprofund"];

            var lst = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_90_PF.BO_ClassPF>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_90_PF.RptMonthlyPFPaySheetFB", lst, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Provident Fund Payment Sheet Month of " + month));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintPfFund()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string month = this.ddlMonth.SelectedItem.Text;
            DataTable dt1 = (DataTable)Session["tblprofund"];
            var lst = dt1.DataTableToList<SPEENTITY.C_81_Hrm.C_90_PF.BO_ClassPF>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_90_PF.RptMonthlyPF", lst, null, null);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "PF Sheet For the Month of - " + month));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvProFund_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProFund.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvMonthlyPF_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonthlyPF.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void txtEmpSearch_TextChanged(object sender, EventArgs e)
        {
            string searchcard = this.txtEmpSearch.Text.Trim();
            DataTable dt = (DataTable)Session["tblprofund2"];
            Session["tblprofund"] = dt;
            if (dt == null) return;
            this.Data_Bind();

            //if (searchcard == "")
            //{
            //    this.lnkbtnShow_Click(null, null);
            //}
            //else
            //{
            //    this.Data_Bind();
            //}
            
        }
        protected void lnkEditUser_Click(object sender, EventArgs e){
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod= ((Label)this.gvMonthlyPF.Rows[RowIndex].FindControl("lgvcomcodid")).Text.Trim();
            string empid = ((Label)this.gvMonthlyPF.Rows[RowIndex].FindControl("lgvempid")).Text.Trim();
            string refno = ((Label)this.gvMonthlyPF.Rows[RowIndex].FindControl("lgvrefnoid")).Text.Trim();
            string monthid = ((Label)this.gvMonthlyPF.Rows[RowIndex].FindControl("lgvmonthidid")).Text.Trim();
            string empname = ((Label)this.gvMonthlyPF.Rows[RowIndex].FindControl("lgvempname")).Text.Trim();
            string idcard = ((Label)this.gvMonthlyPF.Rows[RowIndex].FindControl("lgvcardno")).Text.Trim();
            string designation = ((Label)this.gvMonthlyPF.Rows[RowIndex].FindControl("lgvDesignation")).Text.Trim();
            string pf= ((LinkButton)this.gvMonthlyPF.Rows[RowIndex].FindControl("lgvamt3")).Text.Trim();
            this.lblcomcodId.Text = comcod;
            this.lblEmployeeId.Text = empid;
            this.lblMonthId.Text = monthid;
            this.lblRefno.Text = refno;
            this.txtEmpIdcard.Text = idcard;
            this.txtmShortName.Text = empname;
            this.txtmDesignation.Text = designation;
            this.txtmPF.Text = pf;
            this.lblRowIndex.Text = RowIndex.ToString();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openUserModal();", true);
        }
        protected void lbtnSaveUser_Click(object sender, EventArgs e)
        {
            string comcod = this.lblcomcodId.Text;
            string monthid = this.lblMonthId.Text;
            string refno = this.lblRefno.Text;
            string empid = this.lblEmployeeId.Text;
            int RowIndex = Convert.ToInt32(this.lblRowIndex.Text);
            //string pf = Convert.ToDouble(this.txtmPF.Text).ToString();
            string pf = this.txtmPF.Text.Trim();
            pf = pf == "" ? "0" : pf;
            pf = Convert.ToDouble(pf).ToString();
            bool result = accData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_SALARYSUMMEY", "EDITPFFUND", monthid, refno, empid,pf);
            if (result)
            {
                ((LinkButton)this.gvMonthlyPF.Rows[RowIndex].FindControl("lgvamt3")).Text = pf;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Updated successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Updated Fail');", true);
            }
        }

    }
}