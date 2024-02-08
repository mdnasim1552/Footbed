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


namespace SPEWEB.F_21_GAcc
{

    public partial class RptAccBudget : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01-" + ASTUtility.Right(date, 8)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.ViewSection();

            }

        }

        private void ViewSection()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "WbgdVsAc":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "WbgdVsAcDetials":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "WbgdVsAc":
                    this.ShowWbggVsEx();
                    break;
                case "WbgdVsAcDetials":
                    this.ShowWbggVsAcDetails();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Report Budget";
                string eventdesc = "Show Report: " + type; ;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



        private void ShowWbggVsEx()
        {

            Session.Remove("tblbgdvsex");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BGD", "RPTWRVSACAMT", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblbgdvsex"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private void ShowWbggVsAcDetails()
        {
            Session.Remove("tblbgdvsex");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BGD", "RPTWRVSACHDETAILS", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblbgdvsex"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string grpcode;
            string type = this.Request.QueryString["Type"].ToString().Trim();


            switch (type)
            {
                case "WbgdVsAc":
                    grpcode = dt1.Rows[0]["grpcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";


                        }

                        else
                        {

                            grpcode = dt1.Rows[j]["grpcode"].ToString();


                        }

                    }

                    break;

                case "WbgdVsAcDetials":
                    grpcode = dt1.Rows[0]["grpcode"].ToString();
                    string actcode = dt1.Rows[0]["actcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpcode"].ToString() == grpcode && dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                            dt1.Rows[j]["actdesc"] = "";

                        }

                        else
                        {


                            if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
                            {
                                dt1.Rows[j]["grpdesc"] = "";
                            }
                            if (dt1.Rows[j]["actcode"].ToString() == actcode)
                            {
                                dt1.Rows[j]["actdesc"] = "";
                            }

                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                            actcode = dt1.Rows[j]["actcode"].ToString();

                        }

                    }
                    break;

            }


            return dt1;

        }


        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblbgdvsex"];

            switch (type)
            {
                case "WbgdVsAc":
                    this.gvbgdvse.DataSource = dt;
                    this.gvbgdvse.DataBind();
                    break;

                case "WbgdVsAcDetials":
                    this.gvbgdvsAcd.DataSource = dt;
                    this.gvbgdvsAcd.DataBind();
                    break;

            }


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "WbgdVsAc":
                    this.PrintBudgetdVsAc();
                    break;

                case "WbgdVsAcDetials":
                    this.PrintBudgetdVsAchDet();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Report Budget";
                string eventdesc = "Print Report: " + type; ;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void PrintBudgetdVsAc()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.rptAccBudVsExpen();
            //DataTable dt = (DataTable)Session["tblbgdvsex"];
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["tblbgdvsex"]);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBudgetdVsAchDet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RMGiRPT.R_21_GAcc.rptBudVsAchivDet();
            //DataTable dt = (DataTable)Session["tblbgdvsex"];
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)Session["tblbgdvsex"]);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void gvbgdvse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblgvAcDesc");
                Label bgdamt = (Label)e.Row.FindControl("lgvbgdamt");
                Label acamt = (Label)e.Row.FindControl("lgvacamt");
                Label diffamt = (Label)e.Row.FindControl("txtgvdiffamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    bgdamt.Font.Bold = true;
                    acamt.Font.Bold = true;
                    diffamt.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");


                }

            }

        }

        protected void gvbgdvsAcd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblgvAcDescDet");
                Label bgdamt = (Label)e.Row.FindControl("lgvbgdamtDet");
                Label acamt = (Label)e.Row.FindControl("lgvacamtDet");
                Label diffamt = (Label)e.Row.FindControl("txtgvdiffamtDet");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if ((ASTUtility.Right(code, 4) == "AAAA") || (ASTUtility.Right(code, 4) == "BBBB"))
                {

                    actdesc.Font.Bold = true;
                    bgdamt.Font.Bold = true;
                    acamt.Font.Bold = true;
                    diffamt.Font.Bold = true;
                    // actdesc.Style.Add("text-align", "right");


                }

            }
        }
    }
}