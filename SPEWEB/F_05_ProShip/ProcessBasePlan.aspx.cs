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
    public partial class ProcessBasePlan : System.Web.UI.Page
    {
        ProcessAccess Merdata = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Process Base Production Plan";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                this.txtDatefrom.Text = System.DateTime.Today.AddDays(-15).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(30).ToString("dd-MMM-yyyy");
                //this.TxtCopyStartDate.Text= System.DateTime.Today.AddDays(-19).ToString("dd-MMM-yyyy");
                this.CommonButton();
                this.GetProcessAndLine();
                this.GetSesson();
                GetLotList();
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
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
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


        private void lnkPrint_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> list = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];
            DataTable dt1 = (DataTable)ViewState["tblOrderSize"];
            DataTable dt2 = (DataTable)ViewState["tblratio"];

            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> listratio = dt2.DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCatLot>();


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string masterlc = "Article Name: " + ddlmlccod.SelectedItem.Text;
            string style = "Style: " + ddlStyle.SelectedItem.Text;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //var list = dt.DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCatLot>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_05_ProShip.RptArticleWiseLot", list, listratio, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("masterlc", masterlc));
            rpt1.SetParameters(new ReportParameter("style", style));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Article Wise Lot"));
            rpt1.SetParameters(new ReportParameter("rptTitle2", "Size Wise Ratio"));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(comnam, username, printdate)));

            //int i = 1;
            //foreach (var size in listratio)
            //{

            //    rpt1.SetParameters(new ReportParameter(i.ToString(), size.s1));
            //    i++;
            //    if (i > 25) break;
            //}

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                rpt1.SetParameters(new ReportParameter("size" + (i + 1).ToString(), dt1.Rows[i]["SizeDesc"].ToString()));
            }

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            //this.DdlSeason.SelectedValue = "00000";
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
            string season = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";
            string lotbalance = (this.ChkLotBalance.Checked == true) ? "LOTBALNCE" : "";
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ORDERNO", "1601%", season, lotbalance, "", "", "", "", "");

            if (ds1 != null)
            {
                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.ddlmlccod.DataSource = ds1.Tables[1];
                    this.ddlmlccod.DataBind();

                    this.ddlStyle.DataSource = ds1.Tables[0];
                    this.ddlStyle.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Article Found For This Season');", true);

                    return;
                }

                this.ddlmlccod.DataTextField = "mlcdesc";
                this.ddlmlccod.DataValueField = "mlccod";
                this.ddlmlccod.DataSource = ds1.Tables[1];
                this.ddlmlccod.DataBind();
                ViewState["tblordstyle"] = ds1.Tables[0];

                ddlmlccod_SelectedIndexChanged(null, null);
            }

        }

        private void GetProcessAndLine()
        {

            string comcod = GetCompCode();

            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GETPROCESS_WISE_LINE", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0].DefaultView.ToTable(true, "prodprocess", "prodprocessdesc");
            this.DdlProcess.DataTextField = "prodprocessdesc";
            this.DdlProcess.DataValueField = "prodprocess";
            this.DdlProcess.DataSource = dt;
            this.DdlProcess.DataBind();
            ViewState["tblprocesslist"] = ds1.Tables[0];

            DdlProcess_SelectedIndexChanged(null, null);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            SaveValue();
            Bind_Plan_Allocation();


        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblprodplan"];

            for (int i = 0; i < this.gvPlan.Rows.Count; i++)
            {
                double hours = ASTUtility.StrPosOrNagative("0" + ((TextBox)this.gvPlan.Rows[i].FindControl("LblHours")).Text.Trim());
                // double capacity = ASTUtility.StrPosOrNagative("0" + ((TextBox)this.gvPlan.Rows[i].FindControl("LblCapacity")).Text.Trim());
                string strdate = Convert.ToDateTime(((TextBox)this.gvPlan.Rows[i].FindControl("TxtgvStrdate")).Text.Trim()).ToString("dd-MMM-yyyy");
                string enddate = Convert.ToDateTime(((TextBox)this.gvPlan.Rows[i].FindControl("TxtgvEnddate")).Text.Trim()).ToString("dd-MMM-yyyy");

                double capacity = hours * Convert.ToDouble(dt.Rows[i]["cpperhour"]);

                double daysreq = (capacity == 0) ? 0 : Convert.ToDouble(dt.Rows[i]["tqty"]) / capacity;


                dt.Rows[i]["whours"] = hours;
                dt.Rows[i]["capacity"] = capacity;
                dt.Rows[i]["mandayselect"] = (((CheckBox)this.gvPlan.Rows[i].FindControl("chkack")).Checked == true) ? "true" : "false";
                dt.Rows[i]["trgteffcncy"] = ((TextBox)gvPlan.Rows[i].FindControl("LblTargEfcincy")).Text;
                dt.Rows[i]["fincapacity"] = ( Convert.ToDouble(dt.Rows[i]["capacity"].ToString()) * Convert.ToDouble(dt.Rows[i]["trgteffcncy"].ToString()) ) / 100;

                if (((CheckBox)this.gvPlan.Rows[i].FindControl("chkack")).Checked)
                {
                    dt.Rows[i]["daysreq"] = (Convert.ToDateTime(enddate) - Convert.ToDateTime(strdate)).Days;
                    dt.Rows[i]["startdate"] = strdate;
                    dt.Rows[i]["enddate"] = enddate;
                }
                else
                {
                    dt.Rows[i]["daysreq"] = daysreq;
                    dt.Rows[i]["startdate"] = strdate;
                    dt.Rows[i]["enddate"] = Convert.ToDateTime(strdate).AddDays((daysreq < 1) ? 0 : Convert.ToInt32(daysreq)).ToString("dd-MMM-yyyy");
                }
                //dt.Rows[i]["daysreq"] = daysreq;
                //dt.Rows[i]["startdate"] = strdate;
                //dt.Rows[i]["enddate"] = Convert.ToDateTime(strdate).AddDays((daysreq < 1) ? 0 : Convert.ToInt32(daysreq)).ToString("dd-MMM-yyyy");

                string finalCap = dt.Rows[i]["fincapacity"].ToString();

                if (Convert.ToDouble(finalCap) > 0)
                {
                    dt.Rows[i]["daysreq"] = Convert.ToDouble(dt.Rows[i]["tqty"]) / Convert.ToDouble(finalCap);
                }
            }
            //dt.DefaultView.Sort = "startdate desc,enddate desc";
            ViewState["tblprodplan"] = dt;

        }

        protected void ddlmlccod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlmlccod.SelectedValue.ToString();

            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;

            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable(true, "styledesc2", "stylecode1");
            this.ddlStyle.DataTextField = "styledesc2";
            this.ddlStyle.DataValueField = "stylecode1";
            this.ddlStyle.DataSource = dt1;
            this.ddlStyle.DataBind();
            ddlStyle_SelectedIndexChanged(null, null);

        }





        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usrid = hst["usrid"].ToString();
                string sessionid = hst["session"].ToString();
                string trmid = hst["compname"].ToString();
                string posteddat = DateTime.Today.ToString("dd-MMM-yyyy hh:mm:ss");
                DataTable dt = (DataTable)ViewState["tblprodplan"];
                bool result = false;
                foreach (DataRow dr in dt.Rows)
                {
                    string mlccod = dr["mlccod"].ToString();
                    string styleid = dr["styleid"].ToString();
                    string colorid = dr["colorid"].ToString();
                    string odayid = dr["odayid"].ToString();
                    string shipmntdate = dr["shipmntdat"].ToString();
                    string startdate = dr["startdate"].ToString();
                    string enddate = dr["enddate"].ToString();
                    string linecode = dr["linecode"].ToString();
                    string slnum = dr["slnum"].ToString();
                    string whours = dr["whours"].ToString();
                    string capacity = dr["capacity"].ToString();
                    string tqty = dr["tqty"].ToString();
                    string daysreq = dr["daysreq"].ToString();
                    string lotno = dr["lotno"].ToString();
                    string mandayselect = dr["mandayselect"].ToString();
                    string trgteffcncy = dr["trgteffcncy"].ToString();

                    result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "SAVE_PROCESS_BASE_PLAN",
                        mlccod, styleid, colorid, odayid, linecode, shipmntdate, startdate, enddate, slnum, usrid, sessionid,
                        trmid, posteddat, whours, capacity, tqty, daysreq, lotno, mandayselect, trgteffcncy);

                }


                if (result)
                {
                    GetPlanInformation();
                    this.ddlStyle_SelectedIndexChanged(null, null);
                    this.DdlLines_SelectedIndexChanged(null, null);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


                }


            }


            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);


            }
        }

        private void GetPlanInformation()
        {
            string comcod = GetCompCode();
            ViewState.Remove("tblprodplan");
            string process = this.DdlProcess.SelectedValue.ToString();
            string fromdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            //string fromdate = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            //string todate = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            //string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GETPROCESS_LINE_WISE_PRODUCTION_PLAN", process, fromdate, todate, "", "", ""); ;

            if (ds1 == null)
            {
                this.gvPlan.DataSource = null;
                this.gvPlan.DataBind();
                return;
            }
            DataTable dt = ds1.Tables[0];
            //  dt.DefaultView.Sort = "startdate desc,enddate desc";

            ViewState["tblprodplan"] = dt;
            this.Bind_Plan_Allocation();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.DdlProcess.Enabled = false;
                this.txtDatefrom.Enabled = false;
                this.txtDateto.Enabled = false;
                this.lbtnOk.Text = "New";
                DataTable dtprocess = (DataTable)ViewState["tblprocesslist"];
                DataView dv = dtprocess.DefaultView;
                dv.RowFilter = "prodprocess <>" + this.DdlProcess.SelectedValue.ToString() + " and prodprocess<>'000000000000' and prodprocessdesc not like '%lasting%'";
                DataTable dt = dv.ToTable(true, "prodprocess", "prodprocessdesc");
                this.DdlCopytoProcess.DataTextField = "prodprocessdesc";
                this.DdlCopytoProcess.DataValueField = "prodprocess";
                this.DdlCopytoProcess.DataSource = dt;
                this.DdlCopytoProcess.DataBind();
                this.Copybtn.Visible = true;
                this.LblInformation.Text = "Hey! You are copying Process From " + this.DdlProcess.SelectedItem.ToString() + " with Date Ranges : " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
                GetPlanInformation();
            }
            else
            {
                this.DdlProcess.Enabled = true;
                this.txtDatefrom.Enabled = true;
                this.txtDateto.Enabled = true;
                this.gvPlan.DataSource = null;
                this.gvPlan.DataBind();
                this.lbtnOk.Text = "Ok";
                this.Copybtn.Visible = false;
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
            //string toddat = Convert.ToDateTime(((Label)this.gvPlan.Rows[index].FindControl("lblgvShipment")).Text).ToString("dd-MMM-yyyy");
            string toddat = Convert.ToDateTime(((Label)this.gvPlan.Rows[index].FindControl("lblgvExplndate")).Text).ToString("dd-MMM-yyyy");

            string mlccod = ((Label)this.gvPlan.Rows[index].FindControl("lblmlccod")).Text.ToString();
            string style = ((Label)this.gvPlan.Rows[index].FindControl("lblgvStyleID")).Text.ToString();
            string colorid = ((Label)this.gvPlan.Rows[index].FindControl("lblgvColorID")).Text.ToString();
            string dayid = ((Label)this.gvPlan.Rows[index].FindControl("lblDayid")).Text.ToString();
            string slnum = ((Label)this.gvPlan.Rows[index].FindControl("lblSlnum")).Text.ToString();
            string lotno = ((Label)this.gvPlan.Rows[index].FindControl("lblLotno")).Text.ToString();
            this.LblCodes.Text = mlccod + style + colorid + dayid + slnum;
            this.lblTodDate.Text = toddat;
            DataSet result = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ORDERWISE_SIZE_INFORMATION", mlccod, style, colorid, dayid, toddat, slnum, lotno);
            if (result == null)
            {
                return;
            }
            ViewState["tblsizesdetails"] = result.Tables[0];
            ViewState["tblsizes"] = result.Tables[1];
            ViewState["tblPlanSummary"] = result.Tables[2].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            for (int i = 3; i < 45; i++)
                this.gvsizes.Columns[i].Visible = false;
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
            if (this.GetCompCode() != "5301")
            {
                SizeINput_Selection();

            }
            /// show main order balance

            string type = (dayid != "00000000") ? "Reorder" : "";
            string date = (dayid == "00000000") ? "01-Jan-1900" : Convert.ToDateTime(dayid.Substring(4, 2) + "/" + dayid.Substring(6, 2) + "/" + dayid.Substring(0, 4)).ToString("dd-MMM-yyyy");
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ORDER_DETAILS", mlccod, type, date, style, "", "", "", ""); ;

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

        protected void LbtnSizesUpdate_Click(object sender, EventArgs e)
        {
            try
            {


                string comcod = this.GetCompCode();

                string mlccod = this.LblCodes.Text.Substring(0, 12).ToString();
                string styleid = this.LblCodes.Text.ToString().Substring(12, 12);
                string colorid = this.LblCodes.Text.ToString().Substring(24, 12);
                string dayid = this.LblCodes.Text.ToString().Substring(36, 8);
                DataTable dt = (DataTable)ViewState["tblsizes"];
                string shipmntdat = Convert.ToDateTime(lblTodDate.Text).ToString("dd-MMM-yyyy");
                string slnum = "";
                String[] SizeID = new String[dt.Rows.Count];
                int sizeindex = 0;
                foreach (DataRow item in dt.Rows)
                {
                    SizeID[sizeindex] = item["sizeid"].ToString();
                    sizeindex++;
                }
                for (int i = 0; i < gvsizes.Rows.Count; i++)
                {
                    string mStyleID = ((Label)gvsizes.Rows[i].FindControl("lblgvStyleID")).Text.Trim();
                    string mColorID = ((Label)gvsizes.Rows[i].FindControl("lblgvColorID")).Text.Trim();
                    //  shipmntdat = Convert.ToDateTime(((Label)gvsizes.Rows[i].FindControl("lblgvSipmentdate")).Text).ToString("dd-MMM-yyyy");
                    slnum = ((Label)gvsizes.Rows[i].FindControl("mlblgvSlnum")).Text.Trim();

                    //String[] SizeID = {"720100101001", "720100101002", "720100101003", "720100101004", "720100101005", "720100101006",
                    //           "720100101007", "720100101008", "720100101009", "720100101010", "720100101011", "720100101012",
                    //           "720100101013", "720100101014", "720100101015", "720100101016", "720100101017", "720100101018", "720100101019", "720100101020",
                    //              "720100101021", "720100101022", "720100101023", "720100101024", "720100101025", "720100101026", "720100101027", "720100101028",
                    //              "720100101029", "720100101030", "720100101031", "720100101032", "720100101033", "720100101034", "720100101035", "720100101036", "720100101037",
                    //              "720100101038", "720100101039", "720100101040"};
                    String[] OrderQty = {
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF1")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF2")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF3")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF4")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF5")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF6")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF7")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF8")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF9")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF10")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF11")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF12")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF13")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF14")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF15")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF16")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF17")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF18")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF19")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF20")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF21")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF22")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF23")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF24")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF25")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF26")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF27")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF28")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF29")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF30")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF31")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF32")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF33")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF34")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF35")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF36")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF37")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF38")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF39")).Text.Trim(),
            "0"+((TextBox)gvsizes.Rows[i].FindControl("txtgvF40")).Text.Trim(),

            };


                    for (int j = 0; j < SizeID.Length; j++) ///SizeID.Length-1 change by shovon 26-Feb-2021
                    {

                        if (Convert.ToDouble(OrderQty[j]) > 0)
                        {
                            dt.Rows[j]["trialordrqty"] = Convert.ToDouble(OrderQty[j]).ToString();
                        }


                    }


                }
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "tblsizes";
                bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATE_PLAN_SIZEINFORMATION", ds, null, null, mlccod, styleid, colorid, dayid, shipmntdat, slnum, "");
                if (result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        private void SizeINput_Selection()
        {
            DataTable dt = (DataTable)ViewState["tblsizesdetails"];

            for (int i = 0; i < this.gvsizes.Rows.Count; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    double value = Convert.ToDouble(dt.Rows[i]["p" + j + ""]);
                    //   double value=Convert.ToDouble("0" + ((Label)this.gv1.Rows[i].FindControl("lblP"+j+"")).Text.Trim());
                    if (value == 0)
                    {
                        ((TextBox)gvsizes.Rows[i].FindControl("txtgvF" + j + "")).Enabled = false;
                        ((TextBox)gvsizes.Rows[i].FindControl("txtgvF" + j + "")).BackColor = System.Drawing.Color.Red;
                        ((TextBox)gvsizes.Rows[i].FindControl("txtgvF" + j + "")).ToolTip = "Size Already Planned";
                    }
                }
            }
        }
        private void Bind_Plan_Allocation()
        {
            DataTable dt = (DataTable)ViewState["tblprodplan"];

            this.gvPlan.DataSource = dt;
            this.gvPlan.DataBind();
            //FooterCalLotList();
            //this.OrderINput_Selection();


        }
        private void OrderINput_Selection()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];

            if (lst2 == null || lst2.Count == 0)
                return;

            DataTable dt = ASITUtility03.ListToDataTable(lst2);

            for (int i = 0; i < this.gvPlan.Rows.Count; i++)
            {
                for (int j = 1; j <= 15; j++)
                {

                    if (dt.Rows[i]["ColorDesc"].ToString() != "")
                    {
                        ((TextBox)gvPlan.Rows[i].FindControl("txtgvF" + j + "")).Enabled = false;
                        ((TextBox)gvPlan.Rows[i].FindControl("lblgvColorDesc0")).Enabled = false;
                        ((LinkButton)gvPlan.Rows[i].FindControl("lbAddMore")).Visible = true;
                        ((DropDownList)gvPlan.Rows[i].FindControl("DdlLotlist")).Visible = false;

                    }
                    else
                    {
                        ((TextBox)gvPlan.Rows[i].FindControl("lblgvColorDesc0")).Visible = false;

                    }
                }
            }
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
                    dt1.Rows[j]["linedesc"] = "";
                }
                else
                {
                    linecod = dt1.Rows[j]["linecode"].ToString();
                }

            }
            return dt1;
        }

        private void GetLotList()
        {
            string comcod = GetCompCode();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_SALGINF_INFORMATION", "28%", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            ViewState["tbllotlist"] = ds1.Tables[0];


        }
        protected void lbAddMore_Click(object sender, EventArgs e)
        {

        }
        private void FooterCalLotList()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];

            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> list = lst.FindAll(p => p.colordesc == "").ToList();

            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst2 = lst.FindAll(p => p.colordesc != "").ToList();

            if (lst == null || lst.Count == 0)
            {
                return;
            }
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF1")).Text = ((lst2.Sum(p => p.s1)) - (list.Sum(p => p.s1))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF2")).Text = ((lst2.Sum(p => p.s2)) - (list.Sum(p => p.s2))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF3")).Text = ((lst2.Sum(p => p.s3)) - (list.Sum(p => p.s3))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF4")).Text = ((lst2.Sum(p => p.s4)) - (list.Sum(p => p.s4))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF5")).Text = ((lst2.Sum(p => p.s5)) - (list.Sum(p => p.s5))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF6")).Text = ((lst2.Sum(p => p.s6)) - (list.Sum(p => p.s6))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF7")).Text = ((lst2.Sum(p => p.s7)) - (list.Sum(p => p.s7))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF8")).Text = ((lst2.Sum(p => p.s8)) - (list.Sum(p => p.s8))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF9")).Text = ((lst2.Sum(p => p.s9)) - (list.Sum(p => p.s9))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF10")).Text = ((lst2.Sum(p => p.s10)) - (list.Sum(p => p.s10))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF11")).Text = ((lst2.Sum(p => p.s11)) - (list.Sum(p => p.s11))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF12")).Text = ((lst2.Sum(p => p.s12)) - (list.Sum(p => p.s12))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF13")).Text = ((lst2.Sum(p => p.s13)) - (list.Sum(p => p.s13))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF14")).Text = ((lst2.Sum(p => p.s14)) - (list.Sum(p => p.s14))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF15")).Text = ((lst2.Sum(p => p.s15)) - (list.Sum(p => p.s15))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF16")).Text = ((lst2.Sum(p => p.s16)) - (list.Sum(p => p.s16))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF17")).Text = ((lst2.Sum(p => p.s17)) - (list.Sum(p => p.s17))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF18")).Text = ((lst2.Sum(p => p.s18)) - (list.Sum(p => p.s18))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF19")).Text = ((lst2.Sum(p => p.s19)) - (list.Sum(p => p.s19))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvPlan.FooterRow.FindControl("flblgvF20")).Text = ((lst2.Sum(p => p.s20)) - (list.Sum(p => p.s20))).ToString("#,##0;(#,##0); ");



        }
        protected void gvPlan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable tbllot = (DataTable)ViewState["tbllotlist"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DropDownList DdlLotlist = (DropDownList)e.Row.FindControl("DdlLotlist");
                //DdlLotlist.DataTextField = "gdesc";
                //DdlLotlist.DataValueField = "gcod";
                //DdlLotlist.DataSource = tbllot;
                //DdlLotlist.DataBind();
                //DdlLotlist.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lotno"));

                if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "dailyplan")) > 0)
                {

                    HyperLink HyperLink1 = (HyperLink)e.Row.FindControl("HypDetails");

                    HyperLink1.CssClass = "btn btn-xs btn-success text-white";
                    HyperLink1.ToolTip = "Daily Plan Saved";
                    if (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "dailyplan")) == Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "dailyplanaprv")))
                    {
                        HyperLink1.CssClass = "btn btn-xs btn-primary text-white";
                        HyperLink1.ToolTip = "Daily Plan Approrved All";
                    }
                    HyperLink1.Text = "<i class='fa fa-thumbs-up'></i>";
                    ((TextBox)e.Row.FindControl("TxtgvStrdate")).Enabled = false;
                    ((TextBox)e.Row.FindControl("TxtgvEnddate")).Enabled = false;
                    ((TextBox)e.Row.FindControl("LblCapacity")).Enabled = false;
                    ((TextBox)e.Row.FindControl("LblHours")).Enabled = false;
                    e.Row.Cells[0].Controls.Clear();

                }

            }
        }

        protected void lbtnPush_Click(object sender, EventArgs e)
        {

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
            this.DdlLines_SelectedIndexChanged(null, null);
        }

        protected void LblSelect_Click(object sender, EventArgs e)
        {
            DataTable tblplan = (DataTable)ViewState["tblprodplan"];
            if (tblplan == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Press Ok Before Selection');", true);
                return;

            }

            double remainplanlot = ASTUtility.StrPosOrNagative("0" + this.LblLotPlanRem.Text);

            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            string linecode = this.DdlLines.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string lotno = this.DdlLotNo.SelectedValue.ToString();
            string plandept = this.DdlProcess.SelectedValue.ToString();
            double qty = ASTUtility.StrPosOrNagative("0" + this.TxtQty.Text);

            if (remainplanlot < qty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You are not eligible to Add QTY more than Remaining QTY');", true);
                return;

            }

            if (qty == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please mention qty and then Select');", true);
                return;

            }
            if (lotno == "" || lotno == "00000")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('There has no Article Lot to Plan');", true);
                return;

            }

            DataRow[] dr3 = tblplan.Select("mlccod = '" + mlccod + "'and odayid = '" + dayid + "' and colorid = '" + colorid + "' and styleid = '" + styleid + "' and linecode='" + linecode + "' and slnum='000'");
            if (dr3.Length == 0)
            {

                DataTable tblord = (DataTable)ViewState["tblordstyle"];
                DataRow[] dr4 = tblord.Select("mlccod = '" + mlccod + "' and dayid='"+ dayid + "' ");

                //dr1["spcfdesc"] = this.ddlResSpcf.SelectedItem.Text.Trim();
                double capacity = (plandept == "800100101001") ? Convert.ToDouble(dr4[0]["cutcapacity"]) : (plandept == "800100101018") ? Convert.ToDouble(dr4[0]["lastcapacity"]) : Convert.ToDouble(dr4[0]["sewcapacity"]);
                double targetefficieny = (plandept == "800100101001") ? Convert.ToDouble(dr4[0]["cutargeteff"]) : (plandept == "800100101018") ? Convert.ToDouble(dr4[0]["lstargeteff"]) : Convert.ToDouble(dr4[0]["swtargeteff"]);

                double stdwkhours = Convert.ToDouble(this.DaysetupWkHours.InnerHtml);
                double ttlcapacity = (dr4.Count() == 0) ? 0.00 : (capacity * stdwkhours);
                if (ttlcapacity == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Sorry! Article Capacity is 0');", true);
                    return;
                }
                double dayreq = Math.Round((dr4.Count() == 0) ? 0.00 : (ttlcapacity == 0) ? 0.00 : (qty / ttlcapacity), 1);

                double orderqty = (dr4.Count() == 0) ? 0.00 : Convert.ToDouble(dr4[0]["ordrqty"]);
                DateTime lstbokdate = Convert.ToDateTime(this.LastBookdate.InnerHtml);

                var mergedata = this.CheckEligibleDate(linecode, lstbokdate, dayreq);

                DateTime Strdate = mergedata.Item1;
                DateTime Enddate = mergedata.Item2;
                double dayreqfinal = mergedata.Item3;
                DataRow dr1 = tblplan.NewRow();
                dr1["linecode"] = linecode;
                dr1["linedesc"] = this.DdlLines.SelectedItem.ToString();
                dr1["mlccod"] = mlccod;
                dr1["mlcdesc"] = this.ddlmlccod.SelectedItem.ToString();
                dr1["styleid"] = styleid;
                dr1["colorid"] = colorid;
                dr1["colordesc"] = (dr4.Count() == 0) ? "" : dr4[0]["colordesc"];
                dr1["odayid"] = dayid;
                dr1["tqty"] = qty;
                dr1["ttlplnqty"] = (dr4.Count() == 0) ? "0.00" : dr4[0]["ttlplanqty"];
                dr1["orderno"] = (dr4.Count() == 0) ? "" : dr4[0]["orderno"];
                dr1["artno"] = (dr4.Count() == 0) ? "" : dr4[0]["artno"];
                dr1["shipmntdat"] = (dr4.Count() == 0) ? "01-Jan-1900" : dr4[0]["shipmntdat"];
                dr1["startdate"] = Strdate;
                dr1["enddate"] = Enddate;// Convert.ToDateTime(Strdate).AddDays((dayreq<1)?0:Convert.ToInt32(dayreq)).ToString("dd-MMM-yyyy"); 
                dr1["slnum"] = "000";
                dr1["ordrqty"] = orderqty;
                dr1["prdqty"] = (dr4.Count() == 0) ? "0.00" : dr4[0]["prdqty"];
                dr1["balqty"] = (dr4.Count() == 0) ? "0.00" : (Convert.ToDouble(dr4[0]["ttlplanqty"]) - qty).ToString();
                dr1["whours"] = stdwkhours;
                dr1["cpperhour"] = capacity;
                dr1["capacity"] = ttlcapacity;
                dr1["progress"] = (orderqty == 0) ? 0 : ((dr4.Count() == 0) ? 0.00 : Convert.ToDouble(dr4[0]["prdqty"]) / orderqty) * 100;
                dr1["daysreq"] = dayreqfinal;
                dr1["lotno"] = lotno;
                dr1["mandayselect"] = false;
                dr1["dailyplan"] = 0.00;
                dr1["lotdesc"] = this.DdlLotNo.SelectedItem.ToString();
                dr1["explndate"] = (dr4.Count() == 0) ? "01-Jan-1900" : dr4[0]["shipmntdat"];
                dr1["trgteffcncy"] = targetefficieny;
                dr1["fincapacity"] =(targetefficieny==0)?0: (ttlcapacity*targetefficieny)/100;

                tblplan.Rows.Add(dr1);
            }
            //  tblplan.DefaultView.Sort = "startdate desc,enddate desc";
            ViewState["tblprodplan"] = tblplan;
            this.Bind_Plan_Allocation();
        }

        public Tuple<DateTime, DateTime, double> CheckEligibleDate(string line, DateTime strdate, double dayreq)
        {
            string comcod = this.GetCompCode();
            string endate = Convert.ToDateTime(strdate).AddDays((dayreq < 1) ? 0 : Convert.ToInt32(dayreq)).ToString("dd-MMM-yyyy");
            DataSet result = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "CHECK_DATE_ELIGIBILITY", line, strdate.ToString("dd-MMM-yyyy"), dayreq.ToString(), "", "", "");
            if (result != null)
            {
                return Tuple.Create(Convert.ToDateTime(result.Tables[0].Rows[0]["date1"]), Convert.ToDateTime(result.Tables[0].Rows[0]["enddate"]),
                    (dayreq < Convert.ToDouble(result.Tables[0].Rows[0]["daysreq"])) ? Convert.ToDouble(result.Tables[0].Rows[0]["daysreq"]) : dayreq);
            }

            return Tuple.Create(strdate, Convert.ToDateTime(endate), dayreq); ;


        }
        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlmlccod.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string process = this.DdlProcess.SelectedValue.ToString();

            DataSet result = Merdata.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSTYLE_WISE_INFO", mlccod, styleid, colorid, dayid, process, "", "", "", "");

            if (result == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Not Found');", true);
                return;
            }
            this.BuyerName.Text = result.Tables[0].Rows[0]["buyername"].ToString();
            this.ordqty.Text = Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0;(#,##0); ");
            this.lblDueTod.Text = (Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]) - Convert.ToDouble(result.Tables[0].Rows[0]["PROPLANQTY"])).ToString("#,##0;(#,##0); ");

            DataSet result1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ARTICLE_WISE_LOT_SUMMARY", mlccod, styleid, colorid, dayid, process, "", "", "", "");

            this.lblCrtLotLnk.NavigateUrl = "~/F_05_ProShip/ArticleWiseLot?Type=Entry&genno=" + this.DdlSeason.SelectedValue.Trim()
                                                                                    + "&actcode=" + mlccod
                                                                                    + "&dayid=" + this.ddlStyle.SelectedValue.Trim();

            this.DdlLotNo.DataTextField = "lotdesc";
            this.DdlLotNo.DataValueField = "lotno1";
            this.DdlLotNo.DataSource = result1.Tables[0];
            this.DdlLotNo.DataBind();

            ViewState["tblLotSummary"] = result1.Tables[0];
            DdlLotNo_SelectedIndexChanged(null, null);
        }

        protected void DdlLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            string linecod = this.DdlLines.SelectedValue.ToString();
            string comcod = this.GetCompCode();
            DataSet result = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_LINE_BOOKED_LASDATE", linecod, "", "", "", "", "");
            if (result != null)
            {
                LastBookdate.InnerHtml = Convert.ToDateTime(result.Tables[0].Rows[0]["lastbokdate"]).ToString("dd-MMM-yyyy");
                DaysetupWkHours.InnerHtml = Convert.ToDouble(result.Tables[0].Rows[0]["wrkhours"]).ToString("#,##0");

            }
        }

        protected void DdlLotNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lotno = this.DdlLotNo.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblLotSummary"];
            if (dt == null || dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('LOT Assortment not found with this article');", true);

                return;
            }

            DataRow[] dr4 = dt.Select("lotno1 = '" + lotno + "' ");

            this.TxtQty.Text = Convert.ToDouble(dr4[0]["itmqty"]).ToString("#,##0;(#,##0); ");
            this.LblLotQty.Text = Convert.ToDouble(dr4[0]["itmqty"]).ToString("#,##0;(#,##0); ");
            this.LblLotPlanRem.Text = Convert.ToDouble(dr4[0]["lotplanremqty"]).ToString("#,##0;(#,##0); ");

        }

        protected void LbtnProcessCopy_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string fromprocess = this.DdlProcess.SelectedValue.ToString();
            string toprocess = this.DdlCopytoProcess.SelectedValue.ToString();
            string fromdate = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string startday = DdlStarDay.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string posteddat = DateTime.Today.ToString("dd-MMM-yyyy hh:mm:ss");

            DataSet result = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "PROCESS_LINE_PLAN_COPY", fromprocess, toprocess,
                fromdate, todate, startday, usrid, trmid, sessionid, posteddat);
            if (result != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Copy Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + Merdata.ErrorObject["Msg"].ToString() + "');", true);
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CLoseMOdal();", true);

        }

        protected void lnkbtnOrder_Click(object sender, EventArgs e)
        {
            this.GetMasterLc();
        }

        protected void gvPlan_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            int rowindex = (this.gvPlan.PageSize) * (this.gvPlan.PageIndex) + e.RowIndex;

            DataTable dt = (DataTable)ViewState["tblprodplan"];
            string mlccod = dt.Rows[rowindex]["mlccod"].ToString();
            string styleid = dt.Rows[rowindex]["styleid"].ToString();
            string colorid = dt.Rows[rowindex]["colorid"].ToString();
            string slnum = dt.Rows[rowindex]["slnum"].ToString();
            string lotno = dt.Rows[rowindex]["lotno"].ToString();
            string linecode = dt.Rows[rowindex]["linecode"].ToString();
            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "PROCESS_BASE_PRODUCTION_PLAN_DEL", null, null, null, mlccod, styleid, colorid, slnum, lotno, linecode);
            if (result)
            {
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = "comcod <> '' and mlccod <> ''";
                ViewState["tblprodplan"] = dv.ToTable();

                this.Bind_Plan_Allocation();
            }


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