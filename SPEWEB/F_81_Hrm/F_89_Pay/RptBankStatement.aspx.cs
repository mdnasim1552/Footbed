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
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptBankStatement : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.GetMonth();
                this.SelectType();
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Bank Statement - Department Wise";
                // this.lblmsg.Visible = false;
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Bnkstmntcwise") ? "Bank Statement - Department Wise" : "Bank Statement - Bank Wise";

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetMonth()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "4301":
                case "4305":
                    this.txtfMonth.Text = System.DateTime.Today.ToString("yyyyMM");
                    break;

                default:
                    this.txtfMonth.Text = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
                    break;


            }


        }



        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Bnkstmntcwise":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "Bnkstmtbnkwise":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
            }
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Bnkstmntcwise":
                    this.ShowBnkStmntCwise();
                    break;
                case "Bnkstmtbnkwise":
                    this.ShowBnkStmntBwise();
                    break;
            }
        }

        private void ShowBnkStmntCwise()
        {

            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();
            string CallType = (this.chkBonus.Checked) ? "RPTBONBANKSTCOMWISIE" : "RPTBANKSTCOMPANYWISIE";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", CallType, month, "", "", "", "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvbnkst.DataSource = null;
                this.gvbnkst.DataBind();
                return;
            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();
        }
        private void ShowBnkStmntBwise()
        {

            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();
            string CallType = (this.chkBonus.Checked) ? "RPTBONBANKSTBANKWISIE" : "RPTBANKSTBANKWISIE";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", CallType, month, "", "", "", "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvbsbwise.DataSource = null;
                this.gvbsbwise.DataBind();
                return;
            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;

            string company = dt1.Rows[0]["company"].ToString();
            string bnkcode = dt1.Rows[0]["bnkcode"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["bnkcode"].ToString() == bnkcode)
                {
                    company = dt1.Rows[j]["company"].ToString();
                    bnkcode = dt1.Rows[j]["bnkcode"].ToString();
                    dt1.Rows[j]["companyname"] = "";
                    dt1.Rows[j]["bnkname"] = "";

                }

                else
                {


                    if (dt1.Rows[j]["company"].ToString() == company)
                    {
                        dt1.Rows[j]["companyname"] = "";
                    }
                    if (dt1.Rows[j]["bnkcode"].ToString() == bnkcode)
                    {
                        dt1.Rows[j]["bnkname"] = "";
                    }

                    company = dt1.Rows[j]["company"].ToString();
                    bnkcode = dt1.Rows[j]["bnkcode"].ToString();

                }

            }



            return dt1;

        }



        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Bnkstmntcwise":
                    this.gvbnkst.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvbnkst.DataSource = (DataTable)Session["tblSalSum"];
                    this.gvbnkst.DataBind();
                    break;
                case "Bnkstmtbnkwise":
                    this.gvbsbwise.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvbsbwise.DataSource = (DataTable)Session["tblSalSum"];
                    this.gvbsbwise.DataBind();
                    break;

            }
        }


        protected void gvbnkst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvbnkst.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Bnkstmntcwise":
                    this.printBnkStmntCwis();
                    break;
                case "Bnkstmtbnkwise":
                    this.printBnkStmntBwis();
                    break;
            }

        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private void printBnkStmntCwis()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");

            //Sanmar
            ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_89_Pay.RptBankStmentComWise();
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Month: " + date; ;

            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void printBnkStmntBwis()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");

           // Sanmar
            ReportDocument rpcp = new RMGiRPT.R_81_Hrm.R_89_Pay.RptBankStmentBankWise();
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Month: " + date; ;

            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvbnkst_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label Description = (Label)e.Row.FindControl("lblgvDescription");
                Label salamt = (Label)e.Row.FindControl("lgvnetsal");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "section")).ToString();
                //string company = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "section")).ToString();

                if (code == "")
                {
                    return;
                }
                if ((ASTUtility.Right(code, 10) == "AAAAAAAAAA") || (ASTUtility.Right(code, 10) == "0000000000"))
                {

                    Description.Font.Bold = true;
                    salamt.Font.Bold = true;
                    salamt.Style.Add("text-align", "left");


                }

            }

        }
        protected void gvbsbwise_PageIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvbsbwise_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label Description = (Label)e.Row.FindControl("lblgvbsbwiseDesc");
                Label salamt = (Label)e.Row.FindControl("lgvbsbamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "section")).ToString();

                if (code == "")
                {
                    return;
                }
                if ((ASTUtility.Right(code, 10) == "AAAAAAAAAA") || (ASTUtility.Right(code, 10) == "0000000000"))
                {

                    Description.Font.Bold = true;
                    salamt.Font.Bold = true;
                    salamt.Style.Add("text-align", "left");


                }

            }

        }
    }
}