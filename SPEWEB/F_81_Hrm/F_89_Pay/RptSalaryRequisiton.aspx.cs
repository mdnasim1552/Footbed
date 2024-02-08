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
    public partial class RptSalaryRequisiton : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.GetYearMonth();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Salary Requisition";
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
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.lnkbtnShow.Text = "New";
                this.CheckBoxswo.Enabled = false;

                string comcod = this.GetComeCode();
                string monthId = this.ddlyearmon.SelectedValue.ToString().Trim();
                DataSet ds1 = new DataSet();
                Session.Remove("listSalRequisition");
                if (this.CheckBoxswo.Checked)
                {

                    ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_SALARYSUMMEY", "GETMONTHLYSALARYREQISITION", monthId);
                    if (ds1 == null)
                        return;
                    Session["listSalRequisition"] = this.HiddenSameData(ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisitionSummary>());
                }
                else
                {
                    ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_SALREQUISTION", "GETMONTHLYSALARYREQISITION", monthId);
                    if (ds1 == null)
                        return;
                    Session["listSalRequisition"] = this.HiddenSameData(ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisition>());

                }

                this.Data_Bind();
            }
            else
            {
                this.lnkbtnShow.Text = "Ok";
                this.CheckBoxswo.Enabled = true;


                if (this.CheckBoxswo.Checked)
                {
                    this.gvSalRequisitionSummary.DataSource = null;
                    this.gvSalRequisitionSummary.DataBind();
                    return;
                }
                else
                {
                    this.gvSalRequisition.DataSource = null;
                    this.gvSalRequisition.DataBind();
                    return;
                }
            }
            

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
        private List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisition> HiddenSameData(List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisition> lst)
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



        private void Data_Bind()
        {
            try
            {
                if (this.CheckBoxswo.Checked)
                {
                    this.MultiView1.ActiveViewIndex = 1;
                    var list = (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisitionSummary>)Session["listSalRequisition"];
                    this.gvSalRequisitionSummary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSalRequisitionSummary.DataSource = list;
                    this.gvSalRequisitionSummary.DataBind();
                    this.FooterCalculation();
                }
                else
                {
                    this.MultiView1.ActiveViewIndex = 0;
                    var list = (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisition>)Session["listSalRequisition"];
                    this.gvSalRequisition.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSalRequisition.DataSource = list;
                    this.gvSalRequisition.DataBind();
                    this.FooterCalculation();
                }
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }

        }
        private void FooterCalculation()
        {
            if (this.CheckBoxswo.Checked)
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
            else
            {
                var list = (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisition>)Session["listSalRequisition"];
                if (list.Count == 0)
                    return;

                ((Label)this.gvSalRequisition.FooterRow.FindControl("lgvFNoOfEmp")).Text = ((list.Sum(l => l.noofemp) == 0) ? 0 : list.Sum(l => l.noofemp)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalRequisition.FooterRow.FindControl("lgvFTotDays")).Text = ((list.Sum(l => l.workday) == 0) ? 0 : list.Sum(l => l.workday)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalRequisition.FooterRow.FindControl("lgvFSalAmt")).Text = ((list.Sum(l => l.salam) == 0) ? 0 : list.Sum(l => l.salam)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalRequisition.FooterRow.FindControl("lgvFIncrmt")).Text = ((list.Sum(l => l.incam) == 0) ? 0 : list.Sum(l => l.incam)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalRequisition.FooterRow.FindControl("lgvFOvtHour")).Text = ((list.Sum(l => l.othour) == 0) ? 0 : list.Sum(l => l.othour)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalRequisition.FooterRow.FindControl("lgvFOvtAmt")).Text = ((list.Sum(l => l.otamt) == 0) ? 0 : list.Sum(l => l.otamt)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalRequisition.FooterRow.FindControl("lgvFpfundAmt")).Text = ((list.Sum(l => l.pfund) == 0) ? 0 : list.Sum(l => l.pfund)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalRequisition.FooterRow.FindControl("lgvFTotAmt")).Text = ((list.Sum(l => l.netamt) == 0) ? 0 : list.Sum(l => l.netamt)).ToString("#,##0;(#,##0); ");
            }
            
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvSalRequisition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalRequisition.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvSalRequisitionSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalRequisitionSummary.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvSalRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            string salType = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "saltype")).Trim();
            Label lblSalDesc = (Label)e.Row.FindControl("lblgvSalDesc");
            if (salType == "01")
            {
                lblSalDesc.Attributes["style"] = "color:blue;";
            }
            else if (salType == "02")
            {
                lblSalDesc.Attributes["style"] = "color:green;";
            }
           
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
            if (this.lnkbtnShow.Text == "Ok")
            {
                return;
            }
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
            if (this.CheckBoxswo.Checked)
            {
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
            else
            {
                var list1 = (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptSalRequisition>)Session["listSalRequisition"];
                double netAmt = list1.Select(l => l.netamt).Sum();
                string tkInWord = ASTUtility.Trans(netAmt, 2);
                switch (comcod)
                {
                    case "5305":
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSalRequisition", list1, null, null);
                        Rpt1.EnableExternalImages = true;
                        break;

                    default:
                        Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptSalRequisitionFootbed", list1, null, null);
                        Rpt1.EnableExternalImages = true;
                        break;
                }

                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary, Wages & Over Time : " + month + " " + year));
                Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("tkInWord", tkInWord));
                Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
           
        }
    }
}