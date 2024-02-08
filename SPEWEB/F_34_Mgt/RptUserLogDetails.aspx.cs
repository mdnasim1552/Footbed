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
using Microsoft.Reporting.WinForms;


namespace SPEWEB.F_34_Mgt
{
    public partial class RptUserLogDetails : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                string today = System.DateTime.Today.ToString("yyyy-MM-dd");
                this.txtfromdate.Attributes.Add("data-default-date", today); //System.DateTime.Today.AddDays(-1).ToString("dd-MMM-yyyy");

                this.txttodate.Attributes.Add("data-default-date", today);
                this.GetUserName();
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "USER LOG DETAILS LIST";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetUserName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETUSERNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlUserName.DataTextField = "usrsname";
            this.ddlUserName.DataValueField = "usrid";
            this.ddlUserName.DataSource = ds1.Tables[0];
            this.ddlUserName.DataBind();

        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetUserName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.UserLogStatus();
        }

        private void UserLogStatus()
        {
            Session.Remove("UserLog");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usercode = this.ddlUserName.SelectedValue.ToString();
            string fdate = this.txtfromdate.Text;
            string tdate = this.txttodate.Text;
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "GETALLLOGINF", usercode, fdate, tdate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLogType.DataSource = null;
                this.gvLogType.DataBind();

                this.gvstatus.DataSource = null;
                this.gvstatus.DataBind();
                return;
            }
            Session["UserLog"] = HiddenSameData(ds1.Tables[0]);
            Session["UserLogDetails"] = (ds1.Tables[1]);
            this.Data_Bind();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "User Input List";
                string eventdesc = "Show Report";
                string eventdesc2 = this.ddlUserName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string grp = dt1.Rows[0]["grp"].ToString();
            string vonum = dt1.Rows[0]["number"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }
                if (dt1.Rows[j]["number"].ToString() == vonum)
                {

                    vonum = dt1.Rows[j]["number"].ToString();
                    dt1.Rows[j]["number"] = "";
                    dt1.Rows[j]["valdat"] = "";
                    dt1.Rows[j]["entrydat"] = "";


                }

                else
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    vonum = dt1.Rows[j]["number"].ToString();
                }
            }



            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["UserLog"];
            DataView dv = dt.Copy().DefaultView;
            dv.RowFilter = ("rgp='A'");
            this.gvLogType.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvLogType.DataSource = dv.ToTable();
            this.gvLogType.DataBind();

            DataView dv1 = dt.Copy().DefaultView;
            dv1.RowFilter = ("rgp='B'");
            this.gvLogType2.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvLogType2.DataSource = dv1.ToTable();
            this.gvLogType2.DataBind();

            if (dt.Rows.Count == 0)
                return;
            Session["Report1"] = gvLogType;
            ((HyperLink)this.gvLogType.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            //

            DataTable dt1 = (DataTable)Session["UserLogDetails"];

            this.gvstatus.DataSource = dt1;
            this.gvstatus.DataBind();




        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintLogAll();
        }


        private void PrintLogAll()
        {
            //string rptDt = "Date: From: " + this.txtfromdate.Text.ToString() + " To " + this.txttodate.Text.ToString();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string username = hst["username"].ToString();
            //string username1 = "Name: " + this.ddlUserName.SelectedItem.ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //// string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //DataTable dt = (DataTable)Session["UserLog"];

            //var list = dt.DataTableToList<MFGOBJ.C_34_Mgt.UserLogDetails>();
            //LocalReport rpt1 = new LocalReport();

            //rpt1 = RptSetupClass1.GetLocalReport("RD_33_Mgt.RptUserLogDetails", list, null, null);
            //rpt1.EnableExternalImages = true;
            //rpt1.SetParameters(new ReportParameter("comnam", comnam));

            //rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //rpt1.SetParameters(new ReportParameter("rptTitle", "User Log Details"));
            //rpt1.SetParameters(new ReportParameter("FromToDate", rptDt));
            //rpt1.SetParameters(new ReportParameter("username", username1));
            //rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            ////rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            //Session["Report1"] = rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvLogType.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvLogType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLogType.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvLogType2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLogType2.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}