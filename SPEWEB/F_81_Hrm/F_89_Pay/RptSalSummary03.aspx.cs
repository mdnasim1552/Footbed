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

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptSalSummary03 : System.Web.UI.Page
    {
        BL_ClassManPower getlist = new BL_ClassManPower();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.GetWorkStation();
                this.GetYearMonth();
                this.GetAllOrganogramList();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Salary Summary (Salary, Wages & Over Time)";
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
        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string monthId = this.ddlyearmon.SelectedValue.ToString().Trim();

            List<string> ddlWstationList = new List<string>();
            List<string> ddlWstationList2 = new List<string>();
            foreach (ListItem litem in ddlWstation.Items)
            {

                if (litem.Selected)
                {
                    ddlWstationList.Add(litem.Value);

                }
                if (litem.Value != "000000000000")
                {
                    ddlWstationList2.Add(litem.Value);
                }

            }
            string Wstation = "";
            if (ddlWstationList.Count != 0)
            {
                Wstation = string.Join(", ", ddlWstationList);
            }
            Wstation = Wstation.Contains("000000000000") == true ? string.Join(", ", ddlWstationList2) : Wstation;

            List<string> ddlEmpStatus = new List<string>();
            foreach (ListItem litem in ListEmpStatus.Items)
            {
                if (litem.Selected)
                {
                    ddlEmpStatus.Add(litem.Value);

                }
            }
            string EmpStatus = "M";
            if (ddlEmpStatus.Count != 0)
            {
                EmpStatus = string.Join(", ", ddlEmpStatus);
            }

            DataSet ds1 = new DataSet();
            Session.Remove("listSalRequisition");
            ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_SALARYSUMMEY", "GETMONTHLYSALARYREQISITION", monthId, Wstation, EmpStatus);
            if (ds1 == null)
                return;
            Session["listSalRequisition"] = this.HiddenSameData(ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisitionSummary>());

            this.Data_Bind();

        }


        private List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisitionSummary> HiddenSameData(List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisitionSummary> lst)
        {
            if (lst.Count == 0)
                return lst;
            int j = 0;
            string saltype = lst[0].saltype;
            foreach (var lst1 in lst)
            {
                if (j == 0)
                {
                    j++;

                }

                else if (lst1.saltype.ToString() == saltype)
                {
                    lst1.saldesc = "";

                }



                saltype = lst1.saltype;




            }

            return lst;

        }
        private void GetWorkStation()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
            //lst = lst.Where(item => item.actcode != "000000000000").ToList();


            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            Session.Remove("hrcompnameadd");
            Session["hrcompnameadd"] = lst;
            this.ddlWstation_SelectedIndexChanged(null, null);
        }
        public void GetAllOrganogramList()
        {
            string comcod = GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            var lst = getlist.GETORGANOGRAMALLLIST(comcod, userid);
            ViewState["lstOrganoData"] = lst;
        }
        protected void ddlWstation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetAllOrganogramList();
            this.GetDivision();
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetDeptList();
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
                foreach (var item in wstation)
                {
                    var lst1 = lst.FindAll(x =>
                    x.actcode.Substring(0, 4) == item.Substring(0, 4) &&
                    x.actcode.Substring(7) == "00000" &&
                    x.actcode != item);

                    allData.AddRange(lst1);
                }
            }
            allData.Add(new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf { actcode = "000000000000", actdesc = "All Division" });


            this.ddlDivision.DataTextField = "actdesc";
            this.ddlDivision.DataValueField = "actcode";
            this.ddlDivision.DataSource = allData;
            this.ddlDivision.DataBind();
            this.ddlDivision.SelectedValue = "000000000000";

            //this.ddlDivision_SelectedIndexChanged(null, null);

        }
        private void Data_Bind()
        {
            try
            {
                this.MultiView1.ActiveViewIndex = 0;
                var list = (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisitionSummary>)Session["listSalRequisition"];
                this.gvSalRequisitionSummary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvSalRequisitionSummary.DataSource = list;
                this.gvSalRequisitionSummary.DataBind();
                this.FooterCalculation();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }

        }
        private void FooterCalculation()
        {
            var list = (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisitionSummary>)Session["listSalRequisition"];
            if (list.Count == 0)
                return;

            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFNoOfEmpsm")).Text = ((list.Sum(l => l.noofemp) == 0) ? 0 : list.Sum(l => l.noofemp)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFTotDayssm")).Text = ((list.Sum(l => l.workday) == 0) ? 0 : list.Sum(l => l.workday)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFSalAmtsm")).Text = ((list.Sum(l => l.salam) == 0) ? 0 : list.Sum(l => l.salam)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFIncrmtsm")).Text = ((list.Sum(l => l.incam) == 0) ? 0 : list.Sum(l => l.incam)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFOvtHoursm")).Text = ((list.Sum(l => l.othour) == 0) ? 0 : list.Sum(l => l.othour)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFOvtAmtsm")).Text = ((list.Sum(l => l.otamt) == 0) ? 0 : list.Sum(l => l.otamt)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFpfundAmtsm")).Text = ((list.Sum(l => l.pfund) == 0) ? 0 : list.Sum(l => l.pfund)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFTotAmtsm")).Text = ((list.Sum(l => l.netamt) == 0) ? 0 : list.Sum(l => l.netamt)).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFTot2hoursm")).Text = ((list.Sum(l => l.ot2hour) == 0) ? 0 : list.Sum(l => l.ot2hour)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFTot1hoursm")).Text = ((list.Sum(l => l.ot1hour) == 0) ? 0 : list.Sum(l => l.ot1hour)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFTot3hoursm")).Text = ((list.Sum(l => l.otNot2orNot1hour) == 0) ? 0 : list.Sum(l => l.otNot2orNot1hour)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFToffdayOThoursm")).Text = ((list.Sum(l => l.offdayOThour) == 0) ? 0 : list.Sum(l => l.offdayOThour)).ToString("#,##0;(#,##0); ");

            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFattnbonusamtsm")).Text = ((list.Sum(l => l.attnbonusamt) == 0) ? 0 : list.Sum(l => l.attnbonusamt)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFot2houramtsm")).Text = ((list.Sum(l => l.ot2houramt) == 0) ? 0 : list.Sum(l => l.ot2houramt)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFot1houramtsm")).Text = ((list.Sum(l => l.ot1houramt) == 0) ? 0 : list.Sum(l => l.ot1houramt)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFotNot2orNot1houramtsm")).Text = ((list.Sum(l => l.otNot2orNot1houramt) == 0) ? 0 : list.Sum(l => l.otNot2orNot1houramt)).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSalRequisitionSummary.FooterRow.FindControl("lgvFoffdayOThouramtsm")).Text = ((list.Sum(l => l.offdayOThouramt) == 0) ? 0 : list.Sum(l => l.offdayOThouramt)).ToString("#,##0;(#,##0); ");

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvSalRequisitionSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalRequisitionSummary.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvSalRequisitionSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            string salType = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "saltype")).Trim();
            Label lblSalDesc = (Label)e.Row.FindControl("lblgvSalDescsm");
            if (salType == "01")
            {
                lblSalDesc.Attributes["style"] = "color:blue;";
            }
            else if (salType == "02")
            {
                lblSalDesc.Attributes["style"] = "color:green;";
            }

        }
        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string comadd = hst["comadd"].ToString();
            string comnam = hst["comnam"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string month = ASITUtility03.GetFullMonthName(this.ddlyearmon.SelectedValue.ToString().Substring(4));
            string year = this.ddlyearmon.SelectedValue.ToString().Substring(0, 4);
            LocalReport Rpt1 = new LocalReport();

            var list1 = (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisitionSummary>)Session["listSalRequisition"];
            Rpt1.LoadReportDefinition(RPTPathClass.GetReportFilePath("RD_81_HRM.RD_89_Pay.RptSalRequisitionSummary"));
            Rpt1 = Rpt1.SetRDLCReportDatasets(new Dictionary<string, object> { { "DataSet1", list1 } });
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle1", "Monthly Salary Summary"));
            Rpt1.SetParameters(new ReportParameter("rptTitle2", "Salary, Wages & Over Time : " + month + " " + year));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}