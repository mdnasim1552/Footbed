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
    public partial class RptMonthWiseOTSheet : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFrmDate.Text = "01" + date.Substring(2);
                this.txtToDate.Text = Convert.ToDateTime(this.txtFrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.GetWorkStation();
                ((Label)this.Master.FindControl("lblTitle")).Text =  "Employee Month Wise OT Sheet";

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetWorkStation()
        {
            Session.Remove("lstwrkstation");
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
            lst.Add(new SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.HrSirInf1("000000000000", "ALL", "", "", "", ""));
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation.SelectedValue = "000000000000";

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            string empType = this.ddlWstation.SelectedValue.ToString() =="000000000000" ? "94%" : this.ddlWstation.SelectedValue.ToString().Substring(0, 4)+"%";
            string frmDate = this.txtFrmDate.Text;
            string toDate = this.txtToDate.Text;
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_OTSHEET", "RPTMONTHLYOTSHEET", frmDate, toDate, empType, "", "", "", "", "", "");
            if (ds1==null)
                return;

            var lstMonWiseOTSheet = ds1.Tables[0].DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptMonWiseOTSheet>();
            var lstMonWiseOTDesc = ds1.Tables[1].DataTableToList<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptMonWiseOTDesc>();
            ViewState["lstMonWiseOTSheet"] = lstMonWiseOTSheet;
            ViewState["lstMonWiseOTDesc"] = lstMonWiseOTDesc;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            try
            {
                var list1 = (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptMonWiseOTSheet>)ViewState["lstMonWiseOTSheet"];
                var list2 = (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptMonWiseOTDesc>)ViewState["lstMonWiseOTDesc"];
                int i = 0;
                int j = 2;
                foreach (var lisitem in list2)
                {
                    this.gvMonWiseOTSheet.Columns[j].HeaderText =lisitem.section.ToString();
                    i++;
                    j++;
                    if (i>9)
                        break;
                }

                this.gvMonWiseOTSheet.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue.ToString());
                this.gvMonWiseOTSheet.DataSource = list1;
                this.gvMonWiseOTSheet.DataBind();

                if (list1.Count>0)
                {
                    this.FooterCalCulation();

                    Session["Report1"] = gvMonWiseOTSheet;
                   ((HyperLink)this.gvMonWiseOTSheet.HeaderRow.FindControl("hlbtnexportexcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('"+ ex.Message+"');", true);
            }

        }

        private void FooterCalCulation()
        {
            var list1 = (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptMonWiseOTSheet>)ViewState["lstMonWiseOTSheet"];

            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFS1")).Text = list1.Select(l => l.s1).Sum().ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFS2")).Text = list1.Select(l => l.s2).Sum().ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFS3")).Text = list1.Select(l => l.s3).Sum().ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFS4")).Text = list1.Select(l => l.s4).Sum().ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFS5")).Text = list1.Select(l => l.s5).Sum().ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFS6")).Text = list1.Select(l => l.s6).Sum().ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFS7")).Text = list1.Select(l => l.s7).Sum().ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFS8")).Text = list1.Select(l => l.s8).Sum().ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFS9")).Text = list1.Select(l => l.s9).Sum().ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFS10")).Text = list1.Select(l => l.s10).Sum().ToString("#,#0.00;(#,##0.00); ");
            ((Label)this.gvMonWiseOTSheet.FooterRow.FindControl("lblgvFTotOT")).Text = list1.Select(l => l.totalot).Sum().ToString("#,#0.00;(#,##0.00); ");
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvMonWiseOTSheet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonWiseOTSheet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string comnam = hst["comnam"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string month = Convert.ToDateTime(this.txtFrmDate.Text).ToString("MMMM,yyyy");

            var list1 = (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptMonWiseOTSheet>)ViewState["lstMonWiseOTSheet"];
            var list2 = (List<SPEENTITY.RD_81_Hrm.RD_89_Pay.RpHRtPayroll.RptMonWiseOTDesc>)ViewState["lstMonWiseOTDesc"];

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_89_Pay.RptMonthWiseOTSheet", list1, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee OT Sheet for the month of " +month));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            int i = 1;
            int j = 2;
            foreach (var lisitem in list2)
            {
                Rpt1.SetParameters(new ReportParameter("section"+i, lisitem.section.ToString()));
                i++;
                j++;
                if (i>10)
                    break;
            }
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}