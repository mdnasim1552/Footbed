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
using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using AjaxControlToolkit;
using System.Drawing;

namespace SPEWEB.F_05_ProShip
{
    public partial class LCPlanInformation : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "ArtCapacity") ? "Article Capacity Plan" :
                                                                    (type == "ArtEffiency") ? "Article Efficiency Setup" :
                                                                    (type == "SmvCalculation") ? "Article SMV Calculation" :
                                                                                                 "Article Wise Layout Paper" ;
                this.CommonButton();
                if (this.ddlLCName.Items.Count == 0)
                {
                    this.GetSesson();
                }

                if (type == "SmvCalculation" || type == "ArtLayout")
                {
                    GetProcessAndLine();
                }
            }

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lUpdatInfo_Click);
            // ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Bind_Effieciency();
            this.FooterCalculation();
        }

        private void GetProcessAndLine()
        {

            string comcod = GetComCode();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GETPROCESS_WISE_LINE", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0].DefaultView.ToTable(true, "prodprocess", "prodprocessdesc");
            this.DdlProcess.DataTextField = "prodprocessdesc";
            this.DdlProcess.DataValueField = "prodprocess";
            this.DdlProcess.DataSource = dt;
            this.DdlProcess.DataBind();
            ViewState["tblprocesslist"] = ds1.Tables[0];

            cellDdlProcess.Visible = true;

        }

        public void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["Efficiency"];
            DataTable dt1 = (DataTable)ViewState["SmvCal"];
            string type = this.Request.QueryString["Type"].ToString();

            if (type == "ArtEffiency")
            {
                if (dt == null || dt.Rows.Count == 0)
                    return;

                for (int i = 0; i < this.gvEficiency.Rows.Count; i++)
                {
                    double percnt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvEficiency.Rows[i].FindControl("txtgvPercnt")).Text.Trim()));
                    double smv = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvEficiency.Rows[i].FindControl("txtgvSMV")).Text.Trim()));
                    double whours = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvEficiency.Rows[i].FindControl("txtgvWhours")).Text.Trim()));
                    double manpower = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvEficiency.Rows[i].FindControl("txtgvManpower")).Text.Trim()));
                    // double qty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gvEficiency.Rows[i].FindControl("txtgvQty")).Text.Trim()));
                    percnt = ((percnt == 0) ? 1 : percnt / 100);
                    double qty = (smv == 0) ? 0 : (percnt * (whours * 60) * manpower) / smv;

                    double smvcal = 0;

                    dt.Rows[i]["percnt"] = percnt;
                    dt.Rows[i]["smv"] = smvcal;
                    dt.Rows[i]["whours"] = whours;
                    dt.Rows[i]["manpower"] = manpower;
                    dt.Rows[i]["qty"] = qty;
                }

                ViewState["tblOrderQty"] = dt;
            }

            else if (type == "SmvCalculation")
            {

                for (int i = 0; i < this.GridViewSmvCal.Rows.Count; i++)
                {
                    double smv = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)GridViewSmvCal.Rows[i].FindControl("txtgvSMV1")).Text.Trim()));    
                    double target = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)GridViewSmvCal.Rows[i].FindControl("txtTarget")).Text.Trim()));
                    //double manpower = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)GridViewSmvCal.Rows[i].FindControl("txtOperator")) + ASTUtility.ExprToValue("0" + ((TextBox)GridViewSmvCal.Rows[i].FindControl("txtHelper")).Text.Trim())));
                    double oprator = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)GridViewSmvCal.Rows[i].FindControl("txtOperator")).Text.Trim()));
                    double helper = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)GridViewSmvCal.Rows[i].FindControl("txtHelper")).Text.Trim()));
                    double manpower = oprator + helper;

                    
                    double a = smv;
                    double x = Math.Floor(a);
                    if ((a - x) >= 0.60)
                    {
                        smv = ASTUtility.MinuteConversion(a);
                    }


                    string oprname = ((TextBox)GridViewSmvCal.Rows[i].FindControl("txtOprName")).Text.Trim();
                    string helpername = ((TextBox)GridViewSmvCal.Rows[i].FindControl("txtHelperName")).Text.Trim();


                    dt1.Rows[i]["smv"] = smv;
                    dt1.Rows[i]["oparator"] = oprator;
                    dt1.Rows[i]["helper"] = helper;
                    dt1.Rows[i]["eftarget"] = target;
                    ((Label)this.GridViewSmvCal.Rows[i].FindControl("lblmpower")).Text = manpower.ToString("#,##0;(#,##0); ");

                    dt1.Rows[i]["oprname"] = oprname;
                    dt1.Rows[i]["helpername"] = helpername;
                }

                ViewState["SmvCal"] = dt1;

            }

            
        }
        public void Bind_Effieciency()
        {
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "ArtEffiency")
            {
                DataTable dt = (DataTable)ViewState["Efficiency"];
                this.gvEficiency.DataSource = HiddenSameDataEfficiency(dt);
                this.gvEficiency.DataBind();
            }
            else if (type == "SmvCalculation")
            {
                DataTable dt = (DataTable)ViewState["SmvCal"];
                this.GridViewSmvCal.DataSource = dt;
                this.GridViewSmvCal.DataBind();
            }
            else if (type == "ArtLayout")
            {
                DataTable dt = (DataTable)ViewState["SmvCal"];
                this.GridViewSmvCal.DataSource = dt;
                this.GridViewSmvCal.DataBind();
            }

        }
        private void CommonButton()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;

            string type = this.Request.QueryString["Type"].ToString();
            if (type == "ArtLayout")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            }
            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;

                if(type != "ArtCapacity")
                {
                    ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
                }
            }

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetSesson()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
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
            this.GeLCName();
        }
        private void GeLCName()
        {

            string comcod = this.GetComCode();
            string txtSProject = "%1601%";
            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString()+"%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETORDERNO", txtSProject, findseason, "", "", "", "", "", "", "");
            this.ddlLCName.DataTextField = "actdesc1";
            this.ddlLCName.DataValueField = "actcode";
            this.ddlLCName.DataSource = ds1.Tables[0];
            this.ddlLCName.DataBind();

        }
        protected void ibtnFindLC_Click(object sender, EventArgs e)
        {
            this.GeLCName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblLCdesc.Text = this.ddlLCName.SelectedItem.Text;
                //this.lblLCmDesc.Text = this.ddlLCName.SelectedItem.Text.Substring(13);
                this.ddlLCName.Visible = false;
                //this.lblLCmDesc.Visible = true;
                this.lblLCdesc.Visible = true;
                this.LblMessage.Visible = true;
                this.LoadGrid();
                this.DdlProcess.Enabled = false;
                this.DdlSeason.Enabled = false;
                this.impfrmsmvcell.Visible = true;
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.DdlProcess.Enabled = true;
                this.DdlSeason.Enabled = true;
                this.impfrmsmvcell.Visible = false;
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlLCName.Visible = true;
          
            this.lblLCdesc.Text = "";
            
            this.lblLCdesc.Visible = false;
            this.gvLcpjInfo.DataSource = null;
            this.gvLcpjInfo.DataBind();
            this.gvordanacost.DataSource = null;
            this.gvordanacost.DataBind();
            this.LblMessage.Visible=false;
            this.LblMessage.Text = "";
            this.gvEficiency.DataSource = null;
            this.gvEficiency.DataBind();

            this.GridViewSmvCal.DataSource = null;
            this.GridViewSmvCal.DataBind();
        }

        private void LoadGrid()
        {
            string type = this.Request.QueryString["Type"].ToString();
            if(type== "ArtEffiency")
            {
                this.Multiview1.ActiveViewIndex = 1;
                ArticleEfficiencyPlan();
            }
            else if (type == "SmvCalculation" || type == "ArtLayout")
            {
                this.Multiview1.ActiveViewIndex = 2;
                SmvCalculation();
            }
            else
            {
                this.Multiview1.ActiveViewIndex = 0;
                ArticleCapacityPlan();
            }
        }

        private void SmvCalculation()
        {
            try
            {
                string comcod = this.GetComCode();
                string LCCode = this.ddlLCName.SelectedValue.ToString();

                string mlccod = ddlLCName.SelectedValue;
                string proccod = DdlProcess.SelectedValue;

                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ARTICLE_SMV_INFO", mlccod, proccod, "", "", "", "", "", "", "");

                ViewState["SmvCal"] = ds1.Tables[0];

                DataTable dt = ds1.Tables[0];

                this.GridViewSmvCal.DataSource = dt;
                this.GridViewSmvCal.DataBind();
                this.FooterCalculation();
            }
            catch(Exception ex)
            {

            }
        }

        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)ViewState["SmvCal"];

            if (dt1.Rows.Count > 0)
            {
                double smv = Convert.ToDouble(dt1.Compute("SUM(smv)", string.Empty));
                double ttl2 = Convert.ToDouble(dt1.Compute("SUM(eftarget)", string.Empty))/dt1.Rows.Count;
                double ttl3 = Convert.ToDouble(dt1.Compute("SUM(oparator)", string.Empty)) + Convert.ToDouble(dt1.Compute("SUM(helper)", string.Empty));
                double ttl4 = Convert.ToDouble(dt1.Compute("SUM(oparator)", string.Empty));
                double ttl5 = Convert.ToDouble(dt1.Compute("SUM(helper)", string.Empty));

                double a = smv;
                double x = Math.Floor(a);
                if ((a - x) >= 0.60)
                {
                    smv = ASTUtility.MinuteConversion(a);
                }

                double ttl1 = smv;


                ((Label)(this.GridViewSmvCal.FooterRow.FindControl("gvttlSMV"))).Text = ttl1.ToString("#,##0.00;(#,##0.00); ");
                ((Label)(this.GridViewSmvCal.FooterRow.FindControl("gvttlTarget"))).Text = ttl2.ToString("#,##0;(#,##0); ");
                ((Label)(this.GridViewSmvCal.FooterRow.FindControl("gvttlMP"))).Text = ttl3.ToString("#,##0;(#,##0); ");
                ((Label)(this.GridViewSmvCal.FooterRow.FindControl("gvttlOprtr"))).Text = ttl4.ToString("#,##0;(#,##0); ");
                ((Label)(this.GridViewSmvCal.FooterRow.FindControl("gvttlHlpr"))).Text = ttl5.ToString("#,##0;(#,##0); ");
            }
        }

        public void ArticleCapacityPlan()
        {
            string comcod = this.GetComCode();
            string LCCode = this.ddlLCName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETLCPLANGINFO", LCCode, "", "", "", "", "", "", "", "");
            this.gvLcpjInfo.DataSource = ds1.Tables[0];
            this.gvLcpjInfo.DataBind();

            ((Label)this.gvLcpjInfo.FooterRow.FindControl("lblSewHourlyCapacity")).Text = ds1.Tables[2].Rows[0]["sewingpairs"].ToString();
            ((Label)this.gvLcpjInfo.FooterRow.FindControl("lblCutHourlyCapacity")).Text = ds1.Tables[2].Rows[0]["cuttingpairs"].ToString();
            ((Label)this.gvLcpjInfo.FooterRow.FindControl("lblLastHourlyCapacity")).Text = ds1.Tables[2].Rows[0]["lastingpairs"].ToString();

            this.gvordanacost.DataSource = HiddenSameData1(ds1.Tables[1]);
            this.gvordanacost.DataBind();

            this.LblMessage.Text = ds1.Tables[2].Rows[0]["notes"].ToString();

        }

        public void ArticleEfficiencyPlan()
        {
            string comcod = this.GetComCode();
            string LCCode = this.ddlLCName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ARTICLE_WISE_EFICIENCY", LCCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["Efficiency"] = ds1.Tables[0];

            this.gvEficiency.DataSource = HiddenSameDataEfficiency(ds1.Tables[0]);
            this.gvEficiency.DataBind();

            //((Label)this.gvLcpjInfo.FooterRow.FindControl("lblSewHourlyCapacity")).Text = ds1.Tables[2].Rows[0]["sewingpairs"].ToString();
            //((Label)this.gvLcpjInfo.FooterRow.FindControl("lblCutHourlyCapacity")).Text = ds1.Tables[2].Rows[0]["cuttingpairs"].ToString();
            //((Label)this.gvLcpjInfo.FooterRow.FindControl("lblLastHourlyCapacity")).Text = ds1.Tables[2].Rows[0]["lastingpairs"].ToString();

            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ARTICLE_BASIC_INFORMATION", LCCode, "", "", "", "", "", "", "", "");
            if (ds2 != null)
            {
                if (ds2.Tables[0].Rows.Count == 0)
                    return;
                 this.BuyerName.Text = ds2.Tables[0].Rows[0]["buyername"].ToString();
                this.lblbrand.Text = ds2.Tables[0].Rows[0]["brand"].ToString();
                this.lblcolor.Text = ds2.Tables[0].Rows[0]["colordesc"].ToString();
                //this.lblTrialOrderNo.Text = ds1.Tables[3].Rows[0]["trialordr"].ToString();
                this.lblarticle.Text = ds2.Tables[0].Rows[0]["article"].ToString();
                this.lblsizernge.Text = ds2.Tables[0].Rows[0]["sizerange"].ToString();
                this.SmpleIMg.ImageUrl = (ds2.Tables[0].Rows[0]["images"].ToString() == "") ? "~/images/no_img_preview.png" : ds2.Tables[0].Rows[0]["images"].ToString(); 
                this.TotalOrder.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");
                this.lblCurrency.Text = ds2.Tables[0].Rows[0]["currency"].ToString();
                this.lblCurcode.Text = ds2.Tables[0].Rows[0]["curcode"].ToString();
                this.lblCRate.Text = ds2.Tables[0].Rows[0]["conrate"].ToString();
            }

        }

        private DataTable HiddenSameData1(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string gp = dt1.Rows[0]["gp"].ToString();
            string itmcomd = dt1.Rows[0]["itmcomd"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gp"].ToString() == gp && dt1.Rows[j]["itmcomd"].ToString() == itmcomd)
                {

                    dt1.Rows[j]["gpdesc"] = "";
                    dt1.Rows[j]["itmdesc"] = "";


                }

                else
                {

                    if (dt1.Rows[j]["gp"].ToString() == gp)
                        dt1.Rows[j]["gpdesc"] = "";

                    if (dt1.Rows[j]["itmcomd"].ToString() == itmcomd)
                        dt1.Rows[j]["itmdesc"] = "";
                }


                gp = dt1.Rows[j]["gp"].ToString();
                itmcomd = dt1.Rows[j]["itmcomd"].ToString();


            }



            return dt1;

        }
        private DataTable HiddenSameDataEfficiency(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int j;
            string process = dt1.Rows[0]["process"].ToString();           
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["process"].ToString() == process)
                {

                    dt1.Rows[j]["processdesc"] = "";                

                }

                process = dt1.Rows[j]["process"].ToString();               

            }



            return dt1;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "ArtLayout":
                    this.ArticleLayoutPrint();
                    break;
                case "SmvCalculation":
                    this.ArticleSMVPrint();
                    break;
            }
        }

        private void ArticleSMVPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["SmvCal"];
            var list = dt.DataTableToList<SPEENTITY.C_05_ProShip.EClassPlanning.ArticleLayoutClass>();
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_05_ProShip.RptArticleSMVPrint", list, null, null);
            string rpttitle = "SMV Bulletin";

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", rpttitle));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("comlogo", comlogo));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void ArticleLayoutPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)ViewState["SmvCal"];
            var list = dt.DataTableToList<SPEENTITY.C_05_ProShip.EClassPlanning.ArticleLayoutClass>();
            string comlogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_05_ProShip.RptArticleLayout", list, null, null);
            string rpttitle = "Article Wise Layout Paper";

            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", rpttitle));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("comlogo", comlogo));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lUpdatInfo_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString();
            if (type == "ArtEffiency")
            {
                this.Multiview1.ActiveViewIndex = 1;
                SaveEfficiency();
            }
            if (type == "SmvCalculation")
            {
                this.Multiview1.ActiveViewIndex = 2;
                SaveSmvCalculation();
            }
            else
            {
                this.Multiview1.ActiveViewIndex = 0;
                SaveArticleCapacity();
            }

        }


        public void SaveSmvCalculation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlLCName.SelectedValue.ToString();
            bool result = false;
            for (int i = 0; i < this.GridViewSmvCal.Rows.Count; i++)
            {
                string mlccod = ddlLCName.SelectedValue;
                string proccod = DdlProcess.SelectedValue;
                string gcod = ((Label)this.GridViewSmvCal.Rows[i].FindControl("lblGcod")).Text.Trim();
                string smv = ((TextBox)this.GridViewSmvCal.Rows[i].FindControl("txtgvSMV1")).Text.Trim();
                string oparator = ((TextBox)this.GridViewSmvCal.Rows[i].FindControl("txtOperator")).Text.Trim();
                string helper = ((TextBox)this.GridViewSmvCal.Rows[i].FindControl("txtHelper")).Text.Trim();
                string oprtrname = ((TextBox)this.GridViewSmvCal.Rows[i].FindControl("txtOprName")).Text.Trim();
                string helpername = ((TextBox)this.GridViewSmvCal.Rows[i].FindControl("txtHelperName")).Text.Trim();

                double a = Convert.ToDouble(smv);
                double x = Math.Floor(a);
                if ((a - x) >= 0.60)
                {
                    smv = ASTUtility.MinuteConversion(a).ToString();
                }

                string originalString = ((TextBox)this.GridViewSmvCal.Rows[i].FindControl("txtTarget")).Text.Trim();
                char charToRemove = ',';
                string modifiedString = originalString.Replace(charToRemove.ToString(), "");

                string efTarget = modifiedString;
                
                result = MktData.UpdateTransInfo(comcod, "[SP_ENTRY_PLANNING_INFO]", "UPDATE_ARTICLE_SMV_INFO", mlccod, proccod, gcod, smv, oparator, helper, oprtrname, helpername, efTarget);
            }

            if(result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated successfully');", true);

            }

            this.LoadGrid();
        }


        public void SaveEfficiency()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlLCName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["Efficiency"];
            this.Save_Value();
            foreach (DataRow dr in dt.Rows)
            {
                string Gcode = dr["gcod"].ToString();
                string process = dr["process"].ToString();
                string percnt = dr["percnt"].ToString();
                string smv = dr["smv"].ToString();
                string whours = dr["whours"].ToString();
                string manpower = dr["manpower"].ToString();
                string qty = dr["qty"].ToString();
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "UPDATE_EFFICIENCY_PLAN", pactcode, process,Gcode, percnt, whours, manpower, qty, smv, "", "", "", "");
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated successfully');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Efficiency Plan Information";
                string eventdesc = "Update Article Efficiency Plan Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            this.LoadGrid();
        }
        public void SaveArticleCapacity()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlLCName.SelectedValue.ToString();

            for (int i = 0; i < this.gvLcpjInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvLcpjInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvLcpjInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvLcpjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() == "" ? "0" : ((TextBox)this.gvLcpjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                string Gvalue1 = ((TextBox)this.gvLcpjInfo.Rows[i].FindControl("txtgvVal1")).Text.Trim()==""?"0": ((TextBox)this.gvLcpjInfo.Rows[i].FindControl("txtgvVal1")).Text.Trim();
                string Gvalue2 = ((TextBox)this.gvLcpjInfo.Rows[i].FindControl("txtgvVal2")).Text.Trim()==""?"0": ((TextBox)this.gvLcpjInfo.Rows[i].FindControl("txtgvVal2")).Text.Trim();
                var rslt = MktData.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "INSORUPDATELCPJINF", pactcode, Gcode, gtype, Gvalue, Gvalue1, Gvalue2, "", "", "", "", "", "");

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated successfully');", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Master Plan Information";
                string eventdesc = "Update Master Plan Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            this.LoadGrid();
        }


        protected void gvordanacost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label itemdesc = (Label)e.Row.FindControl("lgcResDesc1");
                Label lblgvQty = (Label)e.Row.FindControl("lblgvQty");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "itmcomd")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "AAA")
                {

                    //itemdesc.ReadOnly = true;
                    //txtgvItmUnit01.ReadOnly = true;
                    //txtgvItmQty01.ReadOnly = true;
                    //txtgvItmRat01.ReadOnly = true;
                    //itemdesc.Style.Add("background-color", "blue");
                    itemdesc.Style.Add("text-align", "right");
                    itemdesc.Style.Add("color", "blue");
                    lblgvQty.Style.Add("color", "blue");

                }

            }
        }


        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {

            //string misudate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //string misuno = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + misudate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();

            //string comcod = this.GetComeCode();
            //string Url = "";
            //string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);


            //if (AsyncFileUpload1.HasFile)
            //{
            //    string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
            //    AsyncFileUpload1.SaveAs(Server.MapPath("~/Uploads/") + comcod + misuno + extension);

            //    Url = comcod + misuno + extension;
            //}


            //var result = PurData.UpdateTransInfo(comcod, "SP_REPORT_FIXEDASSET_INFO", "UPLOADIMG", misuno, Url, "", "", "", "", "", "", "", "", "", "", "", "");


        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                string Mlccod = ddlLCName.SelectedValue;
                string comcod = GetComCode();

                DataSet ds = MktData.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_SMV_BY_ARTICLE", Mlccod);
                DataTable dt = ds.Tables[0];

                string cuttingsmv = "";
                string sewingsmv = "";
                string lastingsmv = "";

                for(int i=0; i<dt.Rows.Count; i++)
                {
                    double smv = Convert.ToDouble(dt.Rows[i]["totalsmv"]);
                    double a = smv;
                    double x = a>0.5?Math.Floor(a):a;
                    if ((a - x) >= 0.60)
                    {
                        smv = ASTUtility.MinuteConversion(a);
                    }
                    dt.Rows[i]["totalsmv"] = smv;
                }

                for(int i=0; i< dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["proccod"].ToString() == "800100101001") // Cutting
                    {
                        cuttingsmv = Convert.ToDouble(dt.Rows[i]["totalsmv"]).ToString("#,##0.00;(#,##0.00); ");
                    }
                    if (dt.Rows[i]["proccod"].ToString() == "800100101007") // Stitching
                    {
                        sewingsmv = Convert.ToDouble(dt.Rows[i]["totalsmv"]).ToString("#,##0.00;(#,##0.00); ");
                    }
                    if (dt.Rows[i]["proccod"].ToString() == "800100101013") // Lasting
                    {
                        lastingsmv = Convert.ToDouble(dt.Rows[i]["totalsmv"]).ToString("#,##0.00;(#,##0.00); ");
                    }
                }

                TextBox Cutting = (TextBox)this.gvLcpjInfo.Rows[1].FindControl("txtgvVal1");
                TextBox Sewing = (TextBox)this.gvLcpjInfo.Rows[1].FindControl("txtgvVal");
                TextBox Lasting = (TextBox)this.gvLcpjInfo.Rows[1].FindControl("txtgvVal2");

                Cutting.Text = cuttingsmv;
                Sewing.Text = sewingsmv;
                Lasting.Text = lastingsmv;
            }
            catch(Exception ex)
            {

            }
        }

        protected void gvLcpjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if(e.Row.RowIndex >= 0)
                {
                    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                    if (code == "01099")
                    {
                        Label lblDesc = (Label)e.Row.FindControl("lgcResDesc1");
                        lblDesc.Visible = false;

                        LinkButton btnDesc = (LinkButton)e.Row.FindControl("btnDesc1");
                        btnDesc.Visible = true;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        protected void btnDesc1_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();

            string mlccod = ddlLCName.SelectedValue;
            string proccod = DdlProcess.SelectedValue;

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPROCESS", "", "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0].DefaultView.ToTable(true, "prodesc", "procode");

            if (ds1 == null)
                return;

            ViewState["ddlProcess"] = dt;

            DataView dv = dt.DefaultView;

            dv.RowFilter = "procode<>'800100101099'";

            grvProcess.DataSource = dv;
            grvProcess.DataBind();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void lblbtnSave_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string mlccod = ddlLCName.SelectedValue;
            string gcod = "01099";
            string procod = "";

            for (int i=0; i< grvProcess.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)grvProcess.Rows[i].FindControl("chkProcess");

                if (cb.Checked)
                {
                    Label lblProcode = (Label)grvProcess.Rows[i].FindControl("lblProcode");
                    procod += lblProcode.Text;
                }
            }

            bool rslt = MktData.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "UPDATE_ART_CAP_PLAN", mlccod, gcod, procod);
            
            if (rslt)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated successfully');", true);
            }

        }
    }
}