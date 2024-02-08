using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_09_Commer
{
    public partial class RptMataWisePO : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string type = Request.QueryString["Type"];
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "WeeklyPlanWiseMat") ? "Weekly Plan Wise Material Report" : "Materials Wise PO Report";

                this.txtFDate.Text = DateTime.Today.AddDays(-15).ToString("dd-MMM-yyyy");
                this.txtdate.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                this.Visiblity();
                this.GetCodeBookList();
            }

        }

        private void Visiblity()
        {
            string type = Request.QueryString["Type"];
            switch (type)
            {
                case "MatWisePO":
                    this.GetSesson();
                    this.divddlYear.Visible = false;
                    this.lblWeek.Visible = false;
                    this.ddlWeek.Visible = false;
                    break;
                case "WeeklyPlanWiseMat":
                    this.GetYear();
                    this.GetWeek();
                    this.GetSesson();
                    this.divtxtFDate.Visible = false;
                    this.divtxtdate.Visible = false;
                    this.divddlType.Visible = false;
                    break;
            }
        }

        protected void GetCodeBookList()
        {
            try
            {
                string Querytype = this.Request.QueryString["Type"];
                string coderange = "04%";
                string comcod = GetCompCode();
                DataSet dsone = MktData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                Session["tblmatsubhead"] = dsone.Tables[1];
                this.ddlCodeBook.DataTextField = "sirdesc";
                this.ddlCodeBook.DataValueField = "sircode1";
                this.ddlCodeBook.DataSource = dsone.Tables[0];
                this.ddlCodeBook.DataBind();
                this.ddlCodeBook_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);
            }
        }

        protected void ddlCodeBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mathead = this.ddlCodeBook.SelectedValue.ToString().Substring(0, 4) + "%";
            DataTable dt = (DataTable)Session["tblmatsubhead"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "sircode1 like '" + mathead + "'";
            this.ddlGroup.DataTextField = "sirdesc";
            this.ddlGroup.DataValueField = "sircode1";
            this.ddlGroup.DataSource = dv.ToTable();
            this.ddlGroup.DataBind();
        }

        private void GetWeek()
        {
            CultureInfo CI = new CultureInfo("en-US");
            System.Globalization.Calendar Cal = CI.Calendar;
            // first week of year
            CalendarWeekRule CWR = CI.DateTimeFormat.CalendarWeekRule;
            // first day of week
            DayOfWeek FirstDOW = CI.DateTimeFormat.FirstDayOfWeek;
            // to get the current week number
            int week = Cal.GetWeekOfYear(DateTime.Now, CWR, FirstDOW);



            for (int i = 1; i <= 52; i++)
            {
                this.ddlWeek.Items.Insert(i - 1, new ListItem($"Week {i}", $"{i}"));
              //  this.lblWeek.Items.Insert(i - 1, new ListItem($"Week {i}", $"{i}"));
            }
            this.ddlWeek.DataBind();
            this.ddlWeek.SelectedValue = week.ToString();
        }

        private void GetYear()
        {
            string year = DateTime.Now.Year.ToString().Substring(2, 2) == "00" ? "" : DateTime.Now.Year.ToString().Substring(0, 2);
            int start = DateTime.Now.Year.ToString().Substring(2, 2) == "00" ? (DateTime.Now.Year - 5) : (Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2)) - 5);
            int end = DateTime.Now.Year.ToString().Substring(2, 2) == "00" ? (DateTime.Now.Year + 5) : (Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2)) + 5);

            int index = 0;
            for (int i = start; i <= end; i++)
            {
                this.ddlYear.Items.Insert(index++, new ListItem($"{year}{i}", $"{year}{i}"));
                string r = $"{year}{i}";
            }
            this.ddlYear.DataBind();
            this.ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");


            ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");


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
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string stype = Request.QueryString["Type"];

            switch (stype)
            {
                case "MatWisePO":
                    this.PrintMatWisePO();
                    break;
                case "WeeklyPlanWiseMat":
                    this.PrintWeeklyPlanWiseMat();
                    break;
            }
        }


        private void PrintMatWisePO()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Fdate = this.txtFDate.Text;
            string Tdate = this.txtdate.Text;


            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = Convert.ToString((Fdate) + " To " + (Tdate));
            string dType = this.ddlType.SelectedValue;

            DataTable dt = (DataTable)Session["tblstatus"];
            var list = dt.DataTableToList<SPEENTITY.C_09_Commer.BO_Matwisepo>();

            LocalReport rpt1 = new LocalReport();

            var type = this.ddlType.SelectedValue;
            if (type == "details")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptMatWisePO", list, null, null);
                rpt1.SetParameters(new ReportParameter("RptTitle", "Materials Wise PO Report (Details)"));
            }
            else
            {
                rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptMatWisePOSummery", list, null, null);
                rpt1.SetParameters(new ReportParameter("RptTitle", "Materials Wise PO Report (Summary)"));
            }

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", compadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("type", dType));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintWeeklyPlanWiseMat()
        {
            DataTable dt = (DataTable)Session["tblWeekWiseMat"];
            var list = dt.DataTableToList<SPEENTITY.C_09_Commer.BO_WeeklyWiseMat>();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
            string year = this.ddlYear.Text.ToString().Trim();

            string selectedWeek = "";

            foreach (ListItem item in ddlWeek.Items)
            {
                if (item.Selected)
                {
                    selectedWeek += item.Value.Length == 1 ? "" + item.Value + ". " : item.Value;
                }
            }

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_09_Commer.RptWeekPlanWiseMat", list, "", "");
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Weekly Plan Wise Material Report"));
            rpt1.SetParameters(new ReportParameter("year", "Year: " + year));
            rpt1.SetParameters(new ReportParameter("selectedWeek", "Weeks: " + selectedWeek ));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

           
        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];
            switch (type)
            {
                case "MatWisePO":
                    this.GetMatWisePO();
                    break;
                case "WeeklyPlanWiseMat":
                    this.GetWeeklyPlanWiseMat();
                    break;
            }
        }

        private void GetWeeklyPlanWiseMat()
        {
            string comcode = this.GetCompCode();
            string year = this.ddlYear.SelectedValue.Trim();
            //string week = this.ddlWeek.SelectedValue.Trim();
            string Season = this.DdlSeason.SelectedValue == "00000" ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            string matGrp = (this.ddlCodeBook.SelectedValue == "040000000000") ? "04%" : this.ddlCodeBook.SelectedValue.Substring(0, 4) + "%";
            string subMatGrp = "";

            foreach (ListItem item in ddlGroup.Items)
            {
                if (item.Selected)
                {
                    subMatGrp += item.Value;
                }
            }


            string weeks = "";

            foreach (ListItem item in ddlWeek.Items)
            {
                if (item.Selected)
                {
                    weeks += item.Value.Length == 1 ? "0" + item.Value : item.Value;
                }
            }

            DataSet ds = this.MktData.GetTransInfo(comcode, "SP_REPORT_ORDER_STATUS", "WEEK_WISE_MATERIAL_STATUS", year, weeks, matGrp, subMatGrp, Season);
            
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                Session["tblWeekWiseMat"] = null;
                this.gvWeekWiseMat.DataSource = null;
                this.gvWeekWiseMat.DataBind();
                return;
            }

            Session["tblWeekWiseMat"] = ds.Tables[0];
            this.LoadGv();
        }

        private void GetMatWisePO()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string srcmat = this.txtSrcMat.Text.Trim().Length > 0 ? "%" + this.txtSrcMat.Text.Trim() + "%" : "%";
            string Season = this.DdlSeason.SelectedValue == "00000" ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string Project = "%";
            string dType = this.ddlType.SelectedValue;
            string matGrp = (this.ddlCodeBook.SelectedValue == "040000000000") ? "04%" : this.ddlCodeBook.SelectedValue.Substring(0, 4) + "%";
            string subMatGrp = "";
            foreach (ListItem item in ddlGroup.Items)
            {
                if (item.Selected)
                {
                    subMatGrp += item.Value;
                }
            }

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "MATWISEPO", fromdate, todate, Season, Project, dType, matGrp, subMatGrp, srcmat);
            if (ds1 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Data not found');", true);
                return;
            }
            Session["tblstatus"] = ds1.Tables[0];
            this.lnkbtnExptExcel.Visible = true;
            this.LoadGv();
            this.txtSrcMat.Text = "";
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void LoadGv()
        {
            string type = Request.QueryString["Type"];
            switch (type)
            {
                case "MatWisePO":
                    DataTable dt = (DataTable)Session["tblstatus"];
                    string dType = this.ddlType.SelectedValue;
                    if (dType == "summary")
                    {
                        this.gvPOReport.Columns[1].Visible = false;
                        this.gvPOReport.Columns[8].Visible = false;
                        this.gvPOReport.Columns[9].Visible = false;
                        this.gvPOReport.Columns[12].Visible = false;
                    }
                    else
                    {
                        this.gvPOReport.Columns[1].Visible = true;
                        this.gvPOReport.Columns[8].Visible = true;
                        this.gvPOReport.Columns[9].Visible = true;
                        this.gvPOReport.Columns[12].Visible = true;
                    }

                    this.gvPOReport.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());

                    if (dType == "summary")
                    {
                        this.gvPOReport.DataSource = dt;
                    }
                    else
                    {
                        this.gvPOReport.DataSource = dt;
                    }
                    this.gvPOReport.DataBind();
                    this.CalculateTotalInFooter();
                    this.PrepareExcelDownload(dt);

                    break;
                case "WeeklyPlanWiseMat":
                    this.gvWeekWiseMat.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvWeekWiseMat.DataSource = (DataTable)Session["tblWeekWiseMat"];
                    this.gvWeekWiseMat.DataBind();
                    this.WeeklyTotalInFooter();
                    break;
            }

        }

        private void CalculateTotalInFooter()
        {
            DataTable dt = (DataTable)Session["tblstatus"];
            string dType = this.ddlType.SelectedValue;

            if (dt.Rows.Count > 0)
            {
                double totalQty = Convert.ToDouble(dt.Compute("SUM(ordrqty)", string.Empty));
                double totalAmt = Convert.ToDouble(dt.Compute("SUM(ordamt)", string.Empty));

                ((Label)(this.gvPOReport.FooterRow.FindControl("gvLblTtlOrdQty"))).Text = totalQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvPOReport.FooterRow.FindControl("gvLblTtlOrdAmt"))).Text = totalAmt.ToString("#,##0.0000;(#,##0.0000); ");
            }
        }

        private void WeeklyTotalInFooter()
        {
            DataTable dt = (DataTable)Session["tblWeekWiseMat"];
            string dType = this.ddlType.SelectedValue;

            if (dt.Rows.Count > 0)
            {
                double totalConQty = Convert.ToDouble(dt.Compute("SUM(conqty)", string.Empty));
                double totalStkQty = Convert.ToDouble(dt.Compute("SUM(stockqty)", string.Empty));
                double totalShipQty = Convert.ToDouble(dt.Compute("SUM(shipqty)", string.Empty));
                double totalRcvQty = Convert.ToDouble(dt.Compute("SUM(onrcvqty)", string.Empty));

                ((Label)(this.gvWeekWiseMat.FooterRow.FindControl("gvLblTtlConsQty"))).Text = totalConQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvWeekWiseMat.FooterRow.FindControl("gvLblTtlStckQty"))).Text = totalStkQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvWeekWiseMat.FooterRow.FindControl("gvLblShipQty"))).Text = totalShipQty.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.gvWeekWiseMat.FooterRow.FindControl("gvLblTtlRcvQty"))).Text = totalRcvQty.ToString("#,##0.00;(#,##0.00); ");
            }
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            string supcode = dt1.Rows[0]["bblccode"].ToString();
            string syspon = dt1.Rows[0]["syspon"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["syspon"].ToString() == syspon)
                {
                    syspon = dt1.Rows[j]["syspon"].ToString();
                    dt1.Rows[j]["custompon"] = "";
                }
                else
                {
                    syspon = dt1.Rows[j]["syspon"].ToString();
                }

                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    supcode = dt1.Rows[j]["bblccode"].ToString();
                    dt1.Rows[j]["itemdesc"] = "";
                    if (dt1.Rows[j]["bblccode"].ToString() == supcode)
                    {
                        dt1.Rows[j]["ssirdesc"] = "";
                        supcode = dt1.Rows[j]["bblccode"].ToString();
                    }
                }
                else
                {
                    if (dt1.Rows[j]["bblccode"].ToString() == supcode)
                    {
                        dt1.Rows[j]["ssirdesc"] = "";
                        supcode = dt1.Rows[j]["bblccode"].ToString();

                    }
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    supcode = dt1.Rows[j]["bblccode"].ToString();
                }
            }
            return dt1;

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGv();
        }

        protected void gvPOReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPOReport.PageIndex = e.NewPageIndex;
            this.LoadGv();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lnkbtnok_Click(null, null);
        }

        protected void gvPOReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var type = this.ddlType.SelectedValue;

            if (type == "summary")
            {
                this.gvPOReport.Columns[2].Visible = false;
                this.gvPOReport.Columns[7].Visible = false;
            }
            else
            {
                this.gvPOReport.Columns[2].Visible = true;
                this.gvPOReport.Columns[7].Visible = true;
            }
        }

        protected void PrepareExcelDownload(DataTable dt)
        {
            try
            {
                if (dt == null) return;

                GridView gvExcel = new GridView();
                gvExcel.AllowPaging = false;
                gvExcel.DataSource = dt;
                gvExcel.DataBind();

                Session["Report1"] = gvExcel;
                lnkbtnExptExcel.NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            catch (Exception Ex)
            {
            }
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            this.GetMatWisePO();
        }

        protected void gvWeekWiseMat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvWeekWiseMat.PageIndex = e.NewPageIndex;
            this.LoadGv();
        }
    }
}