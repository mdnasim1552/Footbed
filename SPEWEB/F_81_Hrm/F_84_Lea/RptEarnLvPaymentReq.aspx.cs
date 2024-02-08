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

namespace SPEWEB.F_81_Hrm.F_84_Lea
{
    public partial class RptEarnLvPaymentReq : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.GetYearMonth();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Earn Leave Payment Requisition";
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
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "Y", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();

            this.ddlyearmon.SelectedValue = System.DateTime.Today.AddYears(-1).ToString("yyyy") + "12";
            ds1.Dispose();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string monthId = this.ddlyearmon.SelectedValue.ToString().Trim();
            string empStatus = this.ddlEmpType.SelectedValue.ToString().Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_SALREQUISTION", "GET_YEARLY_EARNLEAVE_REQ", monthId, empStatus);
            if (ds1 == null)
                return;

            Session["listEarnLvPayReq"] = this.HiddenSameData(ds1.Tables[0].DataTableToList<SPEENTITY.C_81_Hrm.C_89_Pay.RptEarnLvPayRequisition>());
            this.Data_Bind();

        }

        private List<SPEENTITY.C_81_Hrm.C_89_Pay.RptEarnLvPayRequisition> HiddenSameData(List<SPEENTITY.C_81_Hrm.C_89_Pay.RptEarnLvPayRequisition> lst)
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
                var list = (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptEarnLvPayRequisition>)Session["listEarnLvPayReq"];
                this.gvEarnLvPayReq.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvEarnLvPayReq.DataSource = list;
                this.gvEarnLvPayReq.DataBind();
                this.FooterCalculation();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }

        }
        private void FooterCalculation()
        {
            var list = (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptEarnLvPayRequisition>)Session["listEarnLvPayReq"];
            if(list.Count>0)
            ((Label)this.gvEarnLvPayReq.FooterRow.FindControl("lgvFPayableAmt")).Text = list.Sum(p => p.elvpayamt).ToString("#,##0.00;(#,##0.00); ");
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvEarnLvPayReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEarnLvPayReq.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvEarnLvPayReq_RowDataBound(object sender, GridViewRowEventArgs e)
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
        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string username = hst["username"].ToString();
            string comnam = hst["comnam"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string month = ASITUtility03.GetFullMonthName(this.ddlyearmon.SelectedValue.ToString().Substring(4));
            string year = this.ddlyearmon.SelectedValue.ToString().Substring(0, 4);

            var list1 = (List<SPEENTITY.C_81_Hrm.C_89_Pay.RptEarnLvPayRequisition>)Session["listEarnLvPayReq"];
            string netAmt = list1.Select(l => l.elvpayamt).Sum().ToString("#,##0;(#,##0); ");
            string tkInWord = ASTUtility.Trans(Convert.ToDouble(netAmt), 2);

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_84_Lea.RptEarnLvPayReq", list1, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Earn Leave " + year));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("tkInWord", tkInWord));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}