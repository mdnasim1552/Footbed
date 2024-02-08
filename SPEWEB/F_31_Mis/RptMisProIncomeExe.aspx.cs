using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;

namespace SPEWEB.F_31_Mis
{
    public partial class RptMisProIncomeExe : System.Web.UI.Page
    {
        ProcessAccess MISData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "PrjIncome") ? 
                    "BUDGETED INCOME - ALL PROJECT(SUMMARY)" : (this.Request.QueryString["Type"].ToString().Trim() == "BgdIncomeOrderWise") ? "Budgeted Income - Order Wise" : "PROJECT ISSUE STATEMENT";
                this.SectionView();
            }
        }


        private void SectionView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "BgdIncomeLCWise":
                case "BgdIncomeOrderWise":
                    this.lblfrmDate.Visible = false;
                    this.txtDatefrom.Visible = false;
                    this.lbltoDate.Visible = false;
                    this.txtDateto.Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;

                    break;


                case "PrjExecution":
                    this.txtDatefrom.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "ConBgdVsExe":
                    this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    //this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lblfrmDate.Text = "Date :";
                    this.lbltoDate.Visible = false;
                    this.txtDateto.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetComeCode();
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report: " + Type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "BgdIncomeLCWise":
                case "BgdIncomeOrderWise":
                    this.ShowProjectIncomeSt();
                    break;

                case "PrjExecution":
                    this.ShowProExecution();
                    break;
                case "ConBgdVsExe":
                    this.ShowConBgdVsExe();
                    break;
            }
        }

        private void ShowProjectIncomeSt()
        {

            Session.Remove("tbldata");
            string comcod = this.GetComeCode();
            string rptType = (this.Request.QueryString["Type"].ToString() == "BgdIncomeLCWise") ? "Summery" : "Orders";
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTPROINCOMEST", rptType, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProIncome.DataSource = null;
                this.gvProIncome.DataBind();
                return;
            }
            Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();

        }


        private void ShowProExecution()
        {
            Session.Remove("tbldata");
            string comcod = this.GetComeCode();
            string frmdate = this.txtDatefrom.Text.Trim();
            string todate = this.txtDateto.Text.Trim();
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTPROWORKISSUE", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProExecution.DataSource = null;
                this.gvProExecution.DataBind();
                return;
            }
            Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();
        }
        private void ShowConBgdVsExe()
        {
            Session.Remove("tbldata");
            string comcod = this.GetComeCode();
            string frmdate = this.txtDatefrom.Text.Trim();
            string todate = this.txtDateto.Text.Trim();
            DataSet ds1 = MISData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTCONBGDVSEXPENSES", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvConBgdVsExe.DataSource = null;
                this.gvConBgdVsExe.DataBind();
                return;
            }
            Session["tbldata"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();
        }

        private void Data_Bind()
        {

            DataTable dt = ((DataTable)Session["tbldata"]);
            if (dt.Rows.Count == 0)
                return;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "BgdIncomeLCWise":
                case "BgdIncomeOrderWise":
                    this.gvProIncome.DataSource = dt;
                    this.gvProIncome.DataBind();
                    ((Label)this.gvProIncome.FooterRow.FindControl("lgvFinamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(inam)", "")) ? 0.00 : dt.Compute("sum(inam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProIncome.FooterRow.FindControl("lgvFcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdam)", "")) ? 0.00 : dt.Compute("sum(bgdam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProIncome.FooterRow.FindControl("lgvFmargin")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(maram)", "")) ? 0.00 : dt.Compute("sum(maram)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "PrjExecution":
                    this.gvProExecution.DataSource = dt;
                    this.gvProExecution.DataBind();
                    ((Label)this.gvProExecution.FooterRow.FindControl("lgvFpreamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pream)", "")) ? 0.00 : dt.Compute("sum(pream)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProExecution.FooterRow.FindControl("lgvFcuramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curam)", "")) ? 0.00 : dt.Compute("sum(curam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProExecution.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "ConBgdVsExe":
                    this.gvConBgdVsExe.DataSource = dt;
                    this.gvConBgdVsExe.DataBind();
                    ((Label)this.gvConBgdVsExe.FooterRow.FindControl("lgvFBgdCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0.00 : dt.Compute("sum(bgdamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvConBgdVsExe.FooterRow.FindControl("lgvFEexcution")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(exeam)", "")) ? 0.00 : dt.Compute("sum(exeam)", ""))).ToString("#,##0;(#,##0); ");

                    //((Label)this.gvProExecution.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toam)", "")) ? 0.00 : dt.Compute("sum(toam)", ""))).ToString("#,##0;(#,##0); ");
                    break;


            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "BgdIncomeLCWise":
                case "BgdIncomeOrderWise":
                    this.PrintProjectIncomeSt();
                    break;
                case "PrjExecution":
                    this.PrintProjectPrjExecution();
                    break;
                case "ConBgdVsExe":
                    this.PrintConBgdVsExe();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetComeCode();
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report: " + Type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void PrintProjectIncomeSt()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string HeaderTitle = (this.Request.QueryString["Type"] == "BgdIncomeLCWise") ? "Budgeted Income - LC Wise" : "Budgeted Income - Order Wise";
            //ReportDocument rptstk = new RMGiRPT.R_31_Mis.rptBgdInSt();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtHeaderTitle = rptstk.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //txtHeaderTitle.Text = HeaderTitle;

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource((DataTable)Session["tbldata"]);
            ////string comcod = this.GetComeCode();
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintProjectPrjExecution()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            this.txtDatefrom.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            //ReportDocument rptstk = new RMGiRPT.R_31_Mis.RptWorkExecution();//new RMGiRPT.R_10_Mis.RptWorkExecution();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["compName"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "Date :" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To :" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource((DataTable)Session["tbldata"]);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintConBgdVsExe()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //this.txtDatefrom.Text = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            //this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            //ReportDocument rptstk = new RMGiRPT.R_31_Mis.RptConBgdVsExe();//RptWorkExecution();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "Date :" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource((DataTable)Session["tbldata"]);
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


    }
}