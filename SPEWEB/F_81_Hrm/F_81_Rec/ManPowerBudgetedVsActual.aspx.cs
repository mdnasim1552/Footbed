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
using SPEENTITY.C_81_Hrm.C_81_Rec;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_81_Hrm.F_81_Rec
{
    public partial class ManPowerBudgetedVsActual : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        BL_ClassManPower getlist = new BL_ClassManPower();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Manpower Budget Vs. Actual";
                string date = Convert.ToDateTime(System.DateTime.Today).ToString("dd-MMM-yyyy");
                this.txtFrmDate.Text = Convert.ToDateTime("01-" + date.Substring(3)).ToString("dd-MMM-yyyy");
                this.txtToDate.Text = Convert.ToDateTime(System.DateTime.Today).ToString("dd-MMM-yyyy");
                this.CommonButton();
                this.GetWorkStation();

            }

        }
        private void CommonButton()
        {
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).ToolTip = "Calculation";

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkReCalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void GetWorkStation()
        {
            Session.Remove("lstwrkstation");
            string comcod = GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            var lst = getlist.GetWstation(comcod, userid);
            lst = lst.FindAll(x => x.actcode.Substring(4) == "00000000");
            this.ddlWstation.DataTextField = "actdesc";
            this.ddlWstation.DataValueField = "actcode";
            this.ddlWstation.DataSource = lst;
            this.ddlWstation.DataBind();
            this.ddlWstation.SelectedValue = "000000000000";
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void lnkBtnOk_Click(object sender, EventArgs e)
        {
            this.GetBgdtData();
        }
        private void GetBgdtData()
        {
            string comcod = this.GetCompCode();
            string empTypeCode = this.ddlWstation.SelectedValue.Substring(0, 4) + "%";
            string frmDate = this.txtFrmDate.Text;
            string toDate = this.txtToDate.Text;
            DataSet ds = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETMANPOWERBUDGET", null, null, null, empTypeCode, frmDate, toDate);
            //DataSet ds = HRData.GetTransInfoNew(comcod, "dbo_hrm.SP_ENTRY_MANPOWER_BUDGETED", "GETEMPRECRUCOMPARISON", null, null, null, empTypeCode, frmDate, toDate);
            if (ds == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                this.gvBudgeted.DataSource = null;
                this.gvBudgeted.DataBind();
                return;
            }

            Session["tbladdbgdt"] = this.HiddenSameData(ds.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt)
        {
            try
            {
                string secid = dt.Rows[0]["secid"].ToString();
                string linecode = dt.Rows[0]["linecode"].ToString();

                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["secid"].ToString() == secid)
                    {
                        dt.Rows[i]["section"] = "";
                    }

                    if (dt.Rows[i]["secid"].ToString() == secid && dt.Rows[i]["linecode"].ToString() == linecode)
                    {
                        dt.Rows[i]["bgdamt"] = 0;
                        dt.Rows[i]["totalrplce"] = 0;
                        dt.Rows[i]["asperbgd"] = 0;
                    }

                    secid = dt.Rows[i]["secid"].ToString();
                    linecode = dt.Rows[i]["linecode"].ToString();
                }

                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        protected void gvBudgeted_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.SaveValue();
            this.gvBudgeted.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void Data_Bind()
        {
            DataTable tbl1 = (DataTable)Session["tbladdbgdt"];
            this.gvBudgeted.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.gvBudgeted.DataSource = tbl1;
            this.gvBudgeted.DataBind();

            if(tbl1.Rows.Count>0)
             Session["Report1"] = gvBudgeted;
            ((HyperLink)this.gvBudgeted.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }
        private void lnkReCalculate_Click(object sender, EventArgs e)
        {
            //this.SaveValue();
            this.Data_Bind();
        }

        //private void SaveValue()
        //{
        //    try
        //    {
        //        DataTable dt = (DataTable)Session["tbladdbgdt"];
        //        for (int i = 0; i < gvBudgeted.Rows.Count; i++)
        //        {
        //            int row = (this.gvBudgeted.PageSize * this.gvBudgeted.PageIndex) + i;
        //            double bgdAmt = Convert.ToDouble("0" + ((TextBox)this.gvBudgeted.Rows[i].FindControl("txtBudget")).Text);
        //            dt.Rows[row]["bgdamt"] = bgdAmt;

        //        }

        //        Session["tbladdbgdt"] = dt;
        //    }
        //    catch (Exception ex)
        //    {

        //        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
        //    }
        //}
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comaddf"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string date = "(" + this.txtFrmDate.Text + " To " + this.txtToDate.Text + " )";

            DataTable dt = (DataTable)Session["tbladdbgdt"];
            var lstbgd = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.ManPowerBgdState>();
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptManPowerBgdState", lstbgd, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("compName", comnam));
            rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Manpower Budget Vs. Actual"));
            rpt1.SetParameters(new ReportParameter("date", "Date: " + date));
            rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region Previous Print
            //string comcod = this.GetCompCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tbladdbgdt"];
            //if (dt == null)
            //    return;
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //var lstsum = dt.DataTableToList<SPEENTITY.C_81_Hrm.C_81_Rec.BO_ClassManPower.ManPowerBudgtActual>();
            //string year = lstsum[0].bgdyear.ToString();
            //LocalReport rpt1 = new LocalReport();
            //rpt1 = RptSetupClass.GetLocalReport("RD_81_HRM.RD_81_Rec.RptManPowerBudgtActual", lstsum, null, null);
            //rpt1.EnableExternalImages = true;
            //rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("rpttitle", "Budgeted Manpower Vs.Actual-" + year));
            //rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.SaveValue();
            this.Data_Bind();
        }
    }
}