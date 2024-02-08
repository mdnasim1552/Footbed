using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_15_Pro
{
    public partial class DateWiseMatIssue : System.Web.UI.Page
    {

        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtFDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Date Wise Material Issue";
                this.GetSesson();
                this.GetGroupList();
            }

        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";

            }
            DdlSeason_SelectedIndexChanged(null, null);
        }

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMasterLc();
        }

        private void GetMasterLc()
        {

            string comcod = GetCompCode();
            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", "1601%", "%", findseason, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataRow dataRow = ds1.Tables[1].NewRow();
            dataRow["mlcdesc"] = "---All---";
            dataRow["mlccod"] = "000000000000";
            ds1.Tables[1].Rows.Add(dataRow);

            this.ddlmlccod.DataTextField = "mlcdesc";
            this.ddlmlccod.DataValueField = "mlccod";
            this.ddlmlccod.DataSource = ds1.Tables[1];
            this.ddlmlccod.DataBind();
            this.ddlmlccod.SelectedValue = "000000000000";
            ViewState["tblordstyle"] = ds1.Tables[0];

            ddlmlccod_SelectedIndexChanged(null, null);
        }

        protected void ddlmlccod_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetOrderName();
        }

        private void GetOrderName()
        {
            string mlccode1 = ddlmlccod.SelectedValue.ToString() == "000000000000" ? "%" : ddlmlccod.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            if (mlccode1 != "%")
            {
                DataView dv1;
                dv1 = dt1.DefaultView;
                dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
                dt1 = dv1.ToTable(true, "styledesc2", "stylecode1");
            }
            this.ddlOrderList.DataTextField = "styledesc2";
            this.ddlOrderList.DataValueField = "stylecode1";
            this.ddlOrderList.DataSource = dt1;
            this.ddlOrderList.DataBind();

        }

        protected void ddlOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stylecode = this.ddlOrderList.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("stylecode1='" + stylecode + "'");
            dt1 = dv1.ToTable(true, "styledesc2", "stylecode1", "mlccod");
            this.ddlmlccod.SelectedValue = dt1.Rows[0]["mlccod"].ToString();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comaddress = hst["comadd1"].ToString();
            string comcod = hst["comcod"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string FDate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string TDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string date = " From " + FDate + " To " + TDate;
            DataTable dt = (DataTable)ViewState["tblMatIssueSumm"];
            DataTable dt1 = (DataTable)ViewState["tblMatissuDetails"];
            string RptTitle = "Date Wise Material Issue Details";
            //string itmname = Convert.ToString(dt.Rows[0]["itmname"]).ToString();
            //string itmqty = Convert.ToString(dt.Rows[0]["itmqty"]).ToString();
            var list = dt1.DataTableToList<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matdetails>();
            var list1 = dt.DataTableToList<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matsummary>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptGetDateWiseMatIssueInfo", list, list1, null);
            Rpt1.SetParameters(new ReportParameter("comname", comname));
            Rpt1.SetParameters(new ReportParameter("comaddress", comaddress));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Rpt1.SetParameters(new ReportParameter("RptTitle", RptTitle));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return hst["comcod"].ToString();
        }

        private void GetDateWiseMatIssu()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string FDate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string TDate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string ordercode = (this.ddlmlccod.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlmlccod.SelectedValue.ToString() + "%";
            string group = this.ddlGroup.SelectedValue == "040000000000" ? "%%" : this.ddlGroup.SelectedValue.ToString().Substring(0, 4) + "%";
            string subGroup = this.ddlSubGroup.SelectedValue == "040000000000" ? "%%" : this.ddlSubGroup.SelectedValue.ToString().Substring(0, 7) + "%";
            subGroup = (subGroup == "0000000%") ? "%%" : subGroup;
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "GETDATEWISEMATISSUEINFO", FDate, TDate, ordercode, group, subGroup, "", "", "", "");

            if (ds1 == null)
                return;


            ViewState["tblMatIssueSumm"] = ds1.Tables[0];
            ViewState["tblMatissuDetails"] = ds1.Tables[1];
            ViewState["tblMatIssu"] = ds1.Tables[2];

            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dtmatdetails = (DataTable)ViewState["tblMatissuDetails"];
            DataTable dtmatsumm = (DataTable)ViewState["tblMatIssueSumm"];
            DataTable dtmatissu = (DataTable)ViewState["tblMatIssu"];

            this.gvDateMatlist.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDateMatlist.DataSource = dtmatdetails;
            this.gvDateMatlist.DataBind();

            this.gvDateMatlistSummary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDateMatlistSummary.DataSource = dtmatsumm;
            this.gvDateMatlistSummary.DataBind();

            this.gvDateMatIssuDetail.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDateMatIssuDetail.DataSource = dtmatissu;
            this.gvDateMatIssuDetail.DataBind();

            this.FooterCalculation("gvDateMatlist", dtmatdetails);
            this.FooterCalculation("gvDateMatlistSummary", dtmatsumm);
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        private void FooterCalculation(string GrView, DataTable dt)
        {
            //this footer total calculation
            if (dt.Rows.Count == 0)
                return;
            switch (GrView)
            {
                case "gvDateMatlist":
                    ((Label)this.gvDateMatlist.FooterRow.FindControl("lblQtyTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(itmqty)", "")) ?
             0.00 : dt.Compute("Sum(itmqty)", ""))).ToString("#,##0;(#,##0);  ");

                    ((Label)this.gvDateMatlist.FooterRow.FindControl("lblAmtTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(totalAmount)", "")) ?
                     0.00 : dt.Compute("Sum(totalAmount)", ""))).ToString("#,##0;(#,##0);  ");
                    break;
                case "gvDateMatlistSummary":

                    ((Label)this.gvDateMatlistSummary.FooterRow.FindControl("lblQTYTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(itmqty)", "")) ?
                    0.00 : dt.Compute("Sum(itmqty)", ""))).ToString("#,##0;(#,##0);  ");

                    break;
            }
        }

        protected void gvDateMatlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //}
        }
        protected void gvDateMatlistSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //
        }
        protected void lbtnmeOk_Click(object sender, EventArgs e)
        {

            this.GetDateWiseMatIssu();
        }



        protected void lnkOrderDetails_Click(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string isunum = ((DataTable)ViewState["tblMatissuDetails"]).Rows[index]["isueno"].ToString();
            string Ordername = ((DataTable)ViewState["tblMatissuDetails"]).Rows[index]["OrderName"].ToString();
            string issuedate = ((DataTable)ViewState["tblMatissuDetails"]).Rows[index]["ISUEDATE"].ToString();
            string date = Convert.ToDateTime(issuedate).ToString("dd-MMM-yyyy");

            string comcod = this.GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "GET_DATA_MAT_ISSUE_NO_WISE", isunum, "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
                return;

            this.gvIssueDetails.DataSource = ds1.Tables[0];
            this.gvIssueDetails.DataBind();
            this.lblIssno.Text = "Issue No: " + isunum;
            this.lblissuedate.Text = "Issue Date: " + date;
            ViewState["tblMatissuDetailssingle"] = ds1.Tables[0];
            ViewState["tblMatissusummarysingle"] = ds1.Tables[1];

            string OrderNo = ds1.Tables[1].Rows[0]["orderno1"].ToString();             
            this.lblordernomodal.Text = "Order No: " + OrderNo;
            this.lblordernamemodal.Text = "Order Name: " + Ordername;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

        }

        protected void gvDateMatlistSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDateMatlistSummary.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvDateMatlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDateMatlist.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void gvDateMatIssuDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDateMatIssuDetail.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        //protected void btnprintmodal_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    DataTable dt = (DataTable)ViewState["tblMatissuDetails"];
        //    DataTable dt1 = (DataTable)ViewState["tblMatissusummary"];
        //    string title = "Issue No Wise Material Details";

        //    string comcod = this.GetCompCode();
        //  //  DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "GET_DATA_MAT_ISSUE_NO_WISE", isunum, "", "", "", "", "");
        //   // if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
        //     //   return;

        //   // DataTable dt1 = (DataTable)ds1.Tables[0];
        //   // DataTable dt2 = (DataTable)ds1.Tables[1];
        //    var list = dt.DataTableToList<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matsummary>();
        //    var list2 = dt1.DataTableToList<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matdetails>();
        //    LocalReport Rpt1 = new LocalReport();
        //    Rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptDateWiseMaterialShowIssuewise", list, list2, null);
        //    Rpt1.SetParameters(new ReportParameter("title", title));
        //    //Rpt1.SetParameters(new ReportParameter("Ordername", Ordername));
        //    //Rpt1.SetParameters(new ReportParameter("issuedate", issuedate));
        //    //Session["Report1"] = Rpt1;
        //    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
        //    //   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //}

        protected void DetailsPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comaddress = hst["comadd1"].ToString();
            string comcod = hst["comcod"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblMatissuDetailssingle"];
            DataTable dt1 = (DataTable)ViewState["tblMatissusummarysingle"];
            //Convert.ToInt32(dt.Rows[0]["columnname"].ToString());
            string issueno = Convert.ToString(dt1.Rows[0]["isueno"]).ToString();
            string orderno = Convert.ToString(dt1.Rows[0]["orderno1"]).ToString();
            string orderdesc = Convert.ToString(dt1.Rows[0]["orderdesc"]).ToString();
            string isuedate = Convert.ToDateTime(dt1.Rows[0]["isuedate"]).ToString("dd-MMM-yyyy");
            string title = "Issue No Wise Material Details";
            var list = dt.DataTableToList<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matsummaryreport>();
            // var list2 = dt1.DataTableToList<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.Matdetailsreport>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptDateWiseMaterialShowIssuewise", list, null, null);
            Rpt1.SetParameters(new ReportParameter("comname", comname));
            Rpt1.SetParameters(new ReportParameter("comaddress", comaddress));
            Rpt1.SetParameters(new ReportParameter("isuedate", isuedate));
            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("issueno", issueno));
            Rpt1.SetParameters(new ReportParameter("orderno", orderno));
            Rpt1.SetParameters(new ReportParameter("orderdesc", orderdesc));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            string type = "PDF";
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
            //((Label)this.Master.FindControl("DetailsPrint")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
            //   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvDateMatIssuDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnPrintDetails_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string comaddress = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["tblMatIssu"];
            string title = "Material Issue Details";
            var list = dt.DataTableToList<SPEENTITY.C_15_Pro.GetDateWiseMatIssueInfoClass.MatIssueDetailsReport>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptMatIssueDetails", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comname", comname));
            Rpt1.SetParameters(new ReportParameter("comaddress", comaddress));
            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Rpt1.SetParameters(new ReportParameter("comlogo", comLogo));

            Session["Report1"] = Rpt1;

            string type = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue;

            ScriptManager.RegisterStartupScript(this, GetType(), "target", "SetTarget('" + type + "');", true);
        }


        protected void GetGroupList()
        {
            try
            {
                string Querytype = this.Request.QueryString["Type"];
                string coderange = "04%";

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                Session["tblGroup"] = ds1.Tables[1];
                this.ddlGroup.DataTextField = "sircode";
                this.ddlGroup.DataValueField = "sircode1";
                this.ddlGroup.DataSource = ds1.Tables[0];
                this.ddlGroup.DataBind();
                this.ddlGroup_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string group = this.ddlGroup.SelectedValue.ToString().Substring(0, 4) + "%";
            DataTable dt = (DataTable)Session["tblGroup"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode1 like '" + group + "'";

            DataTable dt1 = dv.ToTable();

            DataRow dr = dt1.NewRow();
            dr["sircode"] = "All";
            dr["sircode1"] = "000000000000";
            dt1.Rows.Add(dr);

            this.ddlSubGroup.DataTextField = "sircode";
            this.ddlSubGroup.DataValueField = "sircode1";
            this.ddlSubGroup.DataSource = dt1;
            this.ddlSubGroup.DataBind();
        }
    }
}

//string issueno = Convert.ToString(dt1.Rows[0]["isueno"]).ToString();
//string orderno = Convert.ToString(dt1.Rows[0]["orderno"]).ToString();
//string orderdesc = Convert.ToString(dt1.Rows[0]["orderdesc"]).ToString();
//string isuedate = Convert.ToDateTime(dt1.Rows[0]["isuedate"]).ToString("dd-MMM-yyyy");


//Rpt1.SetParameters(new ReportParameter("isuedate", isuedate));
//Rpt1.SetParameters(new ReportParameter("issueno", issueno));
//Rpt1.SetParameters(new ReportParameter("orderno", orderno));
//Rpt1.SetParameters(new ReportParameter("orderdesc", orderdesc));