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

namespace SPEWEB.F_05_ProShip
{
    public partial class RptProcessBasePlan : System.Web.UI.Page
    {
        ProcessAccess _processAccess = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Process Base Production Plan";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.CommonButton();
                this.GetProcessAndLine();
                this.GetSesson();
                this.HideAndShowFields();
            }
        }

        private void HideAndShowFields()
        {
            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {

                case "Daywise":
                    this.FieldSeason.Visible = false;
                    break;

                case "ArtclLrnCurv":
                    this.FieldDate.Visible = false;
                    this.FieldCalendar.Visible = false;
                    break;
            }
        }

        public void CommonButton()
        {
            //   ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            
            if (ds1 == null)
                return;

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "---All---");
            
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

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "Daywise":
                    this.PrintProcessBaseProdPlan();
                    break;

                case "ArtclLrnCurv":
                    break;
            }

        }

        private void PrintProcessBaseProdPlan()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string date = this.txtDatefrom.Text;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string process = this.DdlProcess.SelectedItem.Text;

            DataTable dt = (DataTable)ViewState["tblprodplan"];
            DataTable tblDays = (DataTable)ViewState["tblDays"];

            var list = dt.DataTableToList<SPEENTITY.C_05_ProShip.EClassPlanning.RptProcessBaseProdPlan>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_05_ProShip.RptProcessBaseProdPlan", list, null, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("RptTitle", "Process Base Production Plan"));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", compadd));
            rpt1.SetParameters(new ReportParameter("date", date));
            rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt1.SetParameters(new ReportParameter("process", process));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            int i = 0;
            foreach (var row in tblDays.Rows)
            {
                rpt1.SetParameters(new ReportParameter("d" + (i + 1).ToString(), tblDays.Rows[i]["pdayname"].ToString() + "\n" + Convert.ToDateTime(tblDays.Rows[i]["plandate"]).ToString("dd-MMM")));
                rpt1.SetParameters(new ReportParameter("wk" + (i + 1).ToString(), tblDays.Rows[i]["wknum"].ToString()));

                i++;
            }

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void GetProcessAndLine()
        {
            string comcod = GetCompCode();

            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GETPROCESS_WISE_LINE", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0].DefaultView.ToTable(true, "prodprocess", "prodprocessdesc");
            this.DdlProcess.DataTextField = "prodprocessdesc";
            this.DdlProcess.DataValueField = "prodprocess";
            this.DdlProcess.DataSource = dt;
            this.DdlProcess.DataBind();
            ViewState["tblprocesslist"] = ds1.Tables[0];
            this.DdlProcess_SelectedIndexChanged(null, null);
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {




        }





        private void lnkbtnSave_Click(object sender, EventArgs e)
        {

        }

        private void GetPlanInformation()
        {
            string comcod = GetCompCode();
            ViewState.Remove("tblprodplan");
            string process = this.DdlProcess.SelectedValue.ToString();
            string fromdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_REPORT_PLANNING_INFO", "GET_DAY_WISE_PROCESS_PLAN", process, fromdate, "", "", "");

            if (ds1 == null)
            {
                this.gvPlan.DataSource = null;
                this.gvPlan.DataBind();
                this.FooterCalculation();
                return;
            }
            DataTable dt = ds1.Tables[0];
            //  dt.DefaultView.Sort = "startdate desc,enddate desc";

            ViewState["tblprodplan"] = dt;
            ViewState["tblDays"] = ds1.Tables[1];
            this.Bind_Plan_Allocation();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {

                case "Daywise":
                    this.MVProcBasePlan.ActiveViewIndex = 0;
                    if (this.lbtnOk.Text == "Ok")
                    {
                        this.DdlProcess.Enabled = false;
                        this.txtDatefrom.Enabled = false;
                        this.lbtnOk.Text = "New";
                        GetPlanInformation();
                    }
                    else
                    {
                        this.DdlProcess.Enabled = true;
                        this.txtDatefrom.Enabled = true;
                        this.gvPlan.DataSource = null;
                        this.gvPlan.DataBind();
                        this.FooterCalculation();
                        this.lbtnOk.Text = "Ok";
                    }

                    break;
                    
                case "ArtclLrnCurv":
                    this.MVProcBasePlan.ActiveViewIndex = 1;

                    this.GetArtclLrningCurvData();
                    this.ShowArtclLrningCurvData();
                    break;

            }
        }

        private void GetArtclLrningCurvData()
        {
            string comcod = this.GetCompCode();
            string season = this.DdlSeason.SelectedValue == "00000" ? "%%" : this.DdlSeason.SelectedValue + "%";
            string process = this.DdlProcess.SelectedValue;
            DataSet ds = _processAccess.GetTransInfo(comcod, "SP_REPORT_PLANNING_INFO", "GET_EFICIENCY_LEARING_CURVE_REPORT", season, process, "", "", "", "", "");

            if(ds != null)
            {
                ViewState["dsArtclLrnCurv"] = ds;
            }

        }

        private void ShowArtclLrningCurvData()
        {
            var ds = (DataSet) ViewState["dsArtclLrnCurv"];

            if (ds == null) return;

            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 10; i < 30; i++)
                    this.gvArtclLrnCurv.Columns[i].Visible = false;

                //for (int rowIndex = 0; rowIndex < ds.Tables[0].Rows.Count; rowIndex++)
                //{
                //    for (int colIndex = 1; colIndex < 20; colIndex++)
                //    {
                //        if (Convert.ToDouble(ds.Tables[0].Rows[rowIndex]["d" + colIndex.ToString()]) > 0)
                //        {
                //            this.gvArtclLrnCurv.Columns[colIndex + 9].Visible = true;
                //        }
                //    }
                //}

                int indexx = 1;
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.gvArtclLrnCurv.Columns[indexx + 9].Visible = true;
                    this.gvArtclLrnCurv.Columns[indexx + 9].HeaderText = ds.Tables[1].Rows[i]["gdesc"].ToString().Trim();
                    indexx++;
                }
                //this.gvArtclLrnCurv.EditIndex = -1;
                this.gvArtclLrnCurv.DataSource = ds.Tables[0];
                this.gvArtclLrnCurv.DataBind();

                Session["Report1"] = gvArtclLrnCurv;
                lnkbtnExcl.NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
            else
            {
                this.gvArtclLrnCurv.DataSource = ds.Tables[0];
                this.gvArtclLrnCurv.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
            }



        }

        protected void LbtnSizes_Click(object sender, EventArgs e)
        {
            Session.Remove("SizePlan");
            this.gvsizes.DataSource = null;
            this.gvsizes.DataBind();

            this.ModalHead.Text = "Line Plan Color Wise Size Breakdown";
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string toddat = "";
            string process = this.DdlProcess.SelectedValue.ToString();
            string mlccod = ((Label)this.gvPlan.Rows[index].FindControl("lblmlccod")).Text.ToString();
            string style = ((Label)this.gvPlan.Rows[index].FindControl("lblgvStyleID")).Text.ToString();
            string colorid = ((Label)this.gvPlan.Rows[index].FindControl("lblgvColorID")).Text.ToString();
            string dayid = ((Label)this.gvPlan.Rows[index].FindControl("lblDayid")).Text.ToString();
            string slnum = ((Label)this.gvPlan.Rows[index].FindControl("lblSlnum")).Text.ToString();
            this.LblCodes.Text = mlccod + style + colorid + dayid + slnum;
            this.lblTodDate.Text = toddat;
            DataSet result = _processAccess.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERWISE_SIZE_INFORMATION", mlccod, style, colorid, dayid, toddat, slnum, process);
            if (result == null)
            {
                return;
            }
            ViewState["tblsizesdetails"] = result.Tables[0];
            ViewState["tblsizes"] = result.Tables[1];
            ViewState["tblPlanSummary"] = result.Tables[2].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            int j = 1;
            for (int i = 0; i < result.Tables[1].Rows.Count; i++)
            {

                int columid = j++;// Convert.ToInt32(ASTUtility.Right(result.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gvsizes.Columns[columid + 3].Visible = true;
                this.gvsizes.Columns[columid + 3].HeaderText = result.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();

                this.gvPlanSummary.Columns[columid + 2].Visible = true;
                this.gvPlanSummary.Columns[columid + 2].HeaderText = result.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }

            this.gvsizes.EditIndex = -1;
            this.gvsizes.DataSource = result.Tables[0];
            this.gvsizes.DataBind();



            this.gvPlanSummary.DataSource = result.Tables[2];
            this.gvPlanSummary.DataBind();

            /// show main order balance

            string type = (dayid != "00000000") ? "Reorder" : "";
            string date = (dayid == "00000000") ? "01-Jan-1900" : Convert.ToDateTime(dayid.Substring(4, 2) + "/" + dayid.Substring(6, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ORDER_DETAILS", mlccod, type, date, style, "", "", "", ""); ;

            for (int i = 5; i < 45; i++)
                this.gv1.Columns[i].Visible = false;

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gv1.Columns[columid + 4].Visible = true;
                this.gv1.Columns[columid + 4].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }
            List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            ViewState["tblOrderQty"] = lst;
            this.gv1.DataSource = lst;
            this.gv1.DataBind();
            this.FooterCal();



            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);
        }


        private void Bind_Plan_Allocation()
        {
            DataTable dt = (DataTable)ViewState["tblprodplan"];
            DataTable tblDays = (DataTable)ViewState["tblDays"];


            int j = 1;
            for (int i = 0; i < tblDays.Rows.Count; i++)
            {

                int columid = j++;// Convert.ToInt32(ASTUtility.Right(result.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                //this.gvPlan.Columns[columid + 6].Visible = true;
                this.gvPlan.Columns[columid + 9].HeaderText = tblDays.Rows[i]["pdayname"].ToString() + "<br>" + Convert.ToDateTime(tblDays.Rows[i]["plandate"]).ToString("dd-MMM");

                if (tblDays.Rows[i]["daystatus"].ToString() != "WRK")
                {
                    this.gvPlan.Columns[columid + 9].ItemStyle.BackColor = System.Drawing.Color.LightSalmon;
                }
                else
                {
                    this.gvPlan.Columns[columid + 9].ItemStyle.BackColor = System.Drawing.Color.Transparent;
                }

            }

            this.gvPlan.DataSource = dt;
            this.gvPlan.DataBind();
            this.FooterCalculation();
        }

        private void FooterCal()
        {
            //   List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> list = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];

            //   if (list == null || list.Count == 0)
            //   {
            //       return;
            //   }

            //((Label)this.gvPlan.FooterRow.FindControl("FLblgvTotal")).Text = ((list.Sum(p => p.totalqty) == 0) ? 0 : list.Sum(p => p.totalqty)).ToString("#,##0;(#,##0); ");
            //   ((Label)this.gvPlan.FooterRow.FindControl("FLblgvColTotal")).Text = ((list.Sum(p => p.colqty) == 0) ? 0 : list.Sum(p => p.colqty)).ToString("#,##0;(#,##0); ");
            var list = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblOrderQty"];
            if (list != null && list.Count != 0)
            {

                ((Label)this.gv1.FooterRow.FindControl("FLblgvTotal")).Text = ((list.Sum(p => p.totalqty) == 0) ? 0 : list.Sum(p => p.totalqty)).ToString("#,##0;(#,##0); ");

            }
            var plansum = (List<SPEENTITY.C_01_Mer.GetOrderWithCat>)ViewState["tblPlanSummary"];
            if (plansum != null && plansum.Count != 0)
            {


                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS1")).Text = ((plansum.Sum(p => p.s1) == 0) ? 0 : plansum.Sum(p => p.s1)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS2")).Text = ((plansum.Sum(p => p.s2) == 0) ? 0 : plansum.Sum(p => p.s2)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS3")).Text = ((plansum.Sum(p => p.s3) == 0) ? 0 : plansum.Sum(p => p.s3)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS4")).Text = ((plansum.Sum(p => p.s4) == 0) ? 0 : plansum.Sum(p => p.s4)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS5")).Text = ((plansum.Sum(p => p.s5) == 0) ? 0 : plansum.Sum(p => p.s5)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS6")).Text = ((plansum.Sum(p => p.s6) == 0) ? 0 : plansum.Sum(p => p.s6)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS7")).Text = ((plansum.Sum(p => p.s7) == 0) ? 0 : plansum.Sum(p => p.s7)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS8")).Text = ((plansum.Sum(p => p.s8) == 0) ? 0 : plansum.Sum(p => p.s8)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS9")).Text = ((plansum.Sum(p => p.s9) == 0) ? 0 : plansum.Sum(p => p.s9)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS10")).Text = ((plansum.Sum(p => p.s10) == 0) ? 0 : plansum.Sum(p => p.s10)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS11")).Text = ((plansum.Sum(p => p.s11) == 0) ? 0 : plansum.Sum(p => p.s11)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS12")).Text = ((plansum.Sum(p => p.s12) == 0) ? 0 : plansum.Sum(p => p.s12)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS13")).Text = ((plansum.Sum(p => p.s13) == 0) ? 0 : plansum.Sum(p => p.s13)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS14")).Text = ((plansum.Sum(p => p.s14) == 0) ? 0 : plansum.Sum(p => p.s14)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS15")).Text = ((plansum.Sum(p => p.s15) == 0) ? 0 : plansum.Sum(p => p.s15)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS16")).Text = ((plansum.Sum(p => p.s16) == 0) ? 0 : plansum.Sum(p => p.s16)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS17")).Text = ((plansum.Sum(p => p.s17) == 0) ? 0 : plansum.Sum(p => p.s17)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS18")).Text = ((plansum.Sum(p => p.s18) == 0) ? 0 : plansum.Sum(p => p.s18)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS19")).Text = ((plansum.Sum(p => p.s19) == 0) ? 0 : plansum.Sum(p => p.s19)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS20")).Text = ((plansum.Sum(p => p.s20) == 0) ? 0 : plansum.Sum(p => p.s20)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS21")).Text = ((plansum.Sum(p => p.s21) == 0) ? 0 : plansum.Sum(p => p.s21)).ToString("#,##0;(#,##0); ");
                ((Label)this.gvPlanSummary.FooterRow.FindControl("PlanGvS22")).Text = ((plansum.Sum(p => p.s22) == 0) ? 0 : plansum.Sum(p => p.s22)).ToString("#,##0;(#,##0); ");

            }

            DataTable dt = (DataTable)ViewState["tblsizesdetails"];
            if (dt != null && dt.Rows.Count > 0)
            {
                ((Label)this.gvsizes.FooterRow.FindControl("GvS1")).Text = Convert.ToDouble(dt.Rows[0]["b1"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS2")).Text = Convert.ToDouble(dt.Rows[0]["b2"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS3")).Text = Convert.ToDouble(dt.Rows[0]["b3"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS4")).Text = Convert.ToDouble(dt.Rows[0]["b4"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS5")).Text = Convert.ToDouble(dt.Rows[0]["b5"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS6")).Text = Convert.ToDouble(dt.Rows[0]["b6"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS7")).Text = Convert.ToDouble(dt.Rows[0]["b7"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS8")).Text = Convert.ToDouble(dt.Rows[0]["b8"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS9")).Text = Convert.ToDouble(dt.Rows[0]["b9"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS10")).Text = Convert.ToDouble(dt.Rows[0]["b10"]).ToString("#,##0;(#,##0); ");

                ((Label)this.gvsizes.FooterRow.FindControl("GvS11")).Text = Convert.ToDouble(dt.Rows[0]["b11"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS12")).Text = Convert.ToDouble(dt.Rows[0]["b12"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS13")).Text = Convert.ToDouble(dt.Rows[0]["b13"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS14")).Text = Convert.ToDouble(dt.Rows[0]["b14"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS15")).Text = Convert.ToDouble(dt.Rows[0]["b15"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS16")).Text = Convert.ToDouble(dt.Rows[0]["b16"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS17")).Text = Convert.ToDouble(dt.Rows[0]["b17"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS18")).Text = Convert.ToDouble(dt.Rows[0]["b18"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS19")).Text = Convert.ToDouble(dt.Rows[0]["b19"]).ToString("#,##0;(#,##0); ");
                ((Label)this.gvsizes.FooterRow.FindControl("GvS20")).Text = Convert.ToDouble(dt.Rows[0]["b20"]).ToString("#,##0;(#,##0); ");

            }

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string linecod = dt1.Rows[0]["linecode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["linecode"].ToString() == linecod)
                {
                    linecod = dt1.Rows[j]["linecode"].ToString();
                    //   dt1.Rows[j]["linedesc"] = "";
                }
                else
                {
                    linecod = dt1.Rows[j]["linecode"].ToString();
                }

            }
            return dt1;
        }






        protected void lbtnPush_Click(object sender, EventArgs e)
        {

        }

        protected void gvPlan_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridView gv = sender as GridView;
            DataTable dtdays = (DataTable)ViewState["tblDays"];
            DataView dv = dtdays.DefaultView;
            dtdays = dv.ToTable(true, "wknum", "wekdays");

            //check if the row is the header row
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //create the first row
                GridViewRow extraHeader1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                extraHeader1.BackColor = System.Drawing.Color.LightSalmon;

                TableCell cell1 = new TableCell();
                cell1.ColumnSpan = 9;
                cell1.Text = "#Week Number >>";
                cell1.BackColor = System.Drawing.Color.LightSkyBlue;
                extraHeader1.Cells.Add(cell1);


                foreach (DataRow item in dtdays.Rows)
                {
                    TableCell cell2 = new TableCell();
                    cell2.ColumnSpan = Convert.ToInt32(item["wekdays"]);
                    cell2.Text = Convert.ToString(item["wknum"]);
                    cell2.BorderColor = System.Drawing.Color.LightSkyBlue;
                    cell2.BackColor = System.Drawing.Color.LightYellow;
                    extraHeader1.Cells.Add(cell2);

                }

                gv.Controls[0].Controls.AddAt(0, extraHeader1);
            }
        }

        protected void FooterCalculation()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblprodplan"];

                var list1 = dt.DataTableToList<SPEENTITY.C_05_ProShip.EClassPlanning.RptProcessBaseProdPlan>();

                if (list1.Count == 0)
                    return;
                ((Label)this.gvPlan.FooterRow.FindControl("LblOrdQtyF")).Text = (list1.Select(p => p.ordrqty).Sum() == 0.00) ? "" : list1.Select(p => p.ordrqty).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("LblTPlanQtyF")).Text = (list1.Select(p => p.totalqty).Sum() == 0.00) ? "" : list1.Select(p => p.totalqty).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay1")).Text = (list1.Select(p => p.d1).Sum() == 0.00) ? "" : list1.Select(p => p.d1).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay2")).Text = (list1.Select(p => p.d2).Sum() == 0.00) ? "" : list1.Select(p => p.d2).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay3")).Text = (list1.Select(p => p.d3).Sum() == 0.00) ? "" : list1.Select(p => p.d3).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay4")).Text = (list1.Select(p => p.d4).Sum() == 0.00) ? "" : list1.Select(p => p.d4).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay5")).Text = (list1.Select(p => p.d5).Sum() == 0.00) ? "" : list1.Select(p => p.d5).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay6")).Text = (list1.Select(p => p.d6).Sum() == 0.00) ? "" : list1.Select(p => p.d6).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay7")).Text = (list1.Select(p => p.d7).Sum() == 0.00) ? "" : list1.Select(p => p.d7).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay8")).Text = (list1.Select(p => p.d8).Sum() == 0.00) ? "" : list1.Select(p => p.d8).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay9")).Text = (list1.Select(p => p.d9).Sum() == 0.00) ? "" : list1.Select(p => p.d9).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay10")).Text = (list1.Select(p => p.d10).Sum() == 0.00) ? "" : list1.Select(p => p.d10).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay11")).Text = (list1.Select(p => p.d11).Sum() == 0.00) ? "" : list1.Select(p => p.d11).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay12")).Text = (list1.Select(p => p.d12).Sum() == 0.00) ? "" : list1.Select(p => p.d12).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay13")).Text = (list1.Select(p => p.d13).Sum() == 0.00) ? "" : list1.Select(p => p.d13).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay14")).Text = (list1.Select(p => p.d14).Sum() == 0.00) ? "" : list1.Select(p => p.d14).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay15")).Text = (list1.Select(p => p.d15).Sum() == 0.00) ? "" : list1.Select(p => p.d15).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay16")).Text = (list1.Select(p => p.d16).Sum() == 0.00) ? "" : list1.Select(p => p.d16).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay17")).Text = (list1.Select(p => p.d17).Sum() == 0.00) ? "" : list1.Select(p => p.d17).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay18")).Text = (list1.Select(p => p.d18).Sum() == 0.00) ? "" : list1.Select(p => p.d18).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay19")).Text = (list1.Select(p => p.d19).Sum() == 0.00) ? "" : list1.Select(p => p.d19).Sum().ToString();
                ((Label)this.gvPlan.FooterRow.FindControl("GvDay20")).Text = (list1.Select(p => p.d20).Sum() == 0.00) ? "" : list1.Select(p => p.d20).Sum().ToString();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + ex.Message + "');", true);
            }
        }


        protected void DdlProcess_SelectedIndexChanged(object sender, EventArgs e)
        {

            string process = DdlProcess.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblprocesslist"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("prodprocess='" + process + "'");
            dt1 = dv1.ToTable(true, "sircode", "sirdesc1");
            this.DdlLines.DataTextField = "sirdesc1";
            this.DdlLines.DataValueField = "sircode";
            this.DdlLines.DataSource = dt1;
            this.DdlLines.DataBind();
           
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {

            string lineCode = this.DdlLines.SelectedValue;
            DataTable dt = ((DataTable)ViewState["tblprodplan"]).Copy();
            DataView dv = dt.DefaultView;

            dv.RowFilter = ("lineCode='" + lineCode + "'");

            gvPlan.DataSource = dv;
            gvPlan.DataBind();
            lnkbtnSearch.Visible = false;
            lnkbtnCross.Visible = true;

        }

        protected void lnkbtnCross_Click(object sender, EventArgs e)
        {
            Bind_Plan_Allocation();
            lnkbtnSearch.Visible = true;
            lnkbtnCross.Visible = false;
        }
    }
}