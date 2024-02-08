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
using SPERDLC;
using Microsoft.Reporting.WinForms;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_05_ProShip
{
    public partial class ExportPlanVsAchiv : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                this.txtCurStartDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurEndDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.txtCurShMentDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                this.Load_Project_Combo();
                this.GetFloorSetupInfo();
                this.CommonButton();
                if (this.Request.QueryString["sircode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);
                }
                //For Sanmar
              
                ((Label)this.Master.FindControl("lblTitle")).Text = "Production And Shipment Plan";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFiUpdate_Click);

            // ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Session_tbExPlan_Update();
            this.gvIExPlanInfo_DataBind();
        }

        private void CommonButton()
        {
            string comcod = this.GetCompCode();
            
            if(comcod != "5305" && comcod != "5306")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            }
            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            }
            

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void Load_Project_Combo()
        {


            this.ddlOrderList.Items.Clear();
            string comcod = this.GetCompCode();
            string FindProject =  "1601%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", FindProject, "%", "%", "", "", "", "", "", "");
            //   DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETORDERNO", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlOrderList.DataTextField = "mlcdesc";
            this.ddlOrderList.DataValueField = "mlccod";
            this.ddlOrderList.DataSource = ds1.Tables[1];
            this.ddlOrderList.DataBind();
            ViewState["tblordstyle"] = ds1.Tables[0];
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.ddlOrderList.SelectedValue = this.Request.QueryString["actcode"].ToString();
            }
            this.ddlOrderList_SelectedIndexChanged(null, null);

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            var odrName = this.ddlOrderList.SelectedItem.Text;
            var artNames = this.ddlStyle.SelectedItem.Text.Split(',').ToList();
            var artName = artNames[0].Substring(8);
            var DueTod = this.lblDueTod.Text;
            var ordqty = this.ordqty.Text;
            var Byername = this.BuyerName.Text;
            var stardDate = this.txtCurStartDate.Text.Trim();
            var endDate = this.txtCurEndDate.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tbExPlan"];
            var rptlist = dt.DataTableToList<SPEENTITY.C_03_CostABgd.PerOrderStatus.ExportPlanVsAchiv>();
            LocalReport Rpt1a = new LocalReport();
            Rpt1a = RptSetupClass.GetLocalReport("R_05_ProShip.RptExportPlanVsAchiv", rptlist, null, null);
            Rpt1a.EnableExternalImages = true;
            Rpt1a.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1a.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1a.SetParameters(new ReportParameter("odrName", odrName));
            Rpt1a.SetParameters(new ReportParameter("DueTod", DueTod));
            Rpt1a.SetParameters(new ReportParameter("ordqty", ordqty));
            Rpt1a.SetParameters(new ReportParameter("Byername", Byername));
            Rpt1a.SetParameters(new ReportParameter("stardDate", stardDate));
            Rpt1a.SetParameters(new ReportParameter("endDate", endDate));
            Rpt1a.SetParameters(new ReportParameter("artName", artName));
            Rpt1a.SetParameters(new ReportParameter("rptitle", "Production & Shipment Plan"));
            Rpt1a.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            Rpt1a.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #region old
            ReportDocument rptstk = new RMGiRPT.R_03_CostABgd.RptProShipPlan();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject rptProjectName = rptstk.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
            rptProjectName.Text = "Order Name: " + this.ddlOrderList.SelectedItem.Text.Substring(14);

            TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rpttxtdate.Text = "Starting Date: " + this.txtCurStartDate.Text.Trim() + " Ending Date: " + this.txtCurEndDate.Text.Trim();

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string comcod = this.GetCompCode();
            //    string eventtype = "Materials Receive Information";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.lblCurMRRNo1.Text.Trim() + this.txtCurMRRNo2.Text.Trim();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                //this.ddlOrderList.Visible = true;
                //this.lblddlOrder.Visible = false;
                this.ddlStyle.Enabled = true;
                this.txtCurEndDate.Text = this.txtCurEndDate.Text = DateTime.Today.ToString("dd.MM.yyyy"); ;
                
                this.txtProQty.Text = "";
                this.txtProQty.Text = "";
                this.ddlOrderList.Enabled = true;
                this.gvShiMentInfo.DataSource = null;
                this.gvShiMentInfo.DataBind();
                this.Panel1.Visible = false;
                this.lbtnOk.Text = "Ok";
                return;
            }


            //this.lblddlOrder.Text = (this.ddlOrderList.Items.Count == 0 ? "XXX" : this.ddlOrderList.SelectedItem.Text.Trim());
            this.ddlOrderList.Enabled = false;
            //this.lblddlOrder.Visible = true;
            //this.ddlOrderList.Enabled = false;
            this.Panel1.Visible = true;
            this.lbtnOk.Text = "New";
            this.ddlStyle.Enabled = false;
            this.Get_Receive_Info();
        }

        private void GetFloorSetupInfo()
        {
            //
            string comcod = GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GETPRODSETUPINFO", "", "", "", "", "", "", "", "", "");

            this.ddlLine.DataTextField = "sirdesc";
            this.ddlLine.DataValueField = "sircode";
            this.ddlLine.DataSource = ds1.Tables[1];
            this.ddlLine.DataBind();
        }

        protected void Session_tbExPlan_Update()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["tbExPlan"];
                int TblRowIndex2;
                for (int j = 0; j < this.gvShiMentInfo.Rows.Count; j++)
                {
                    string dgvSubCod = ((Label)this.gvShiMentInfo.Rows[j].FindControl("lblgvShMentDat")).Text.Trim();
                    string startdate = ((TextBox)this.gvShiMentInfo.Rows[j].FindControl("TxtStrdate")).Text.Trim();
                    string enddate = ((TextBox)this.gvShiMentInfo.Rows[j].FindControl("TxtEnddate")).Text.Trim();
                    double dgvShiQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvShiMentInfo.Rows[j].FindControl("lblgvShimentQty")).Text.Trim()));
                    double dgvProQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvShiMentInfo.Rows[j].FindControl("lblgvProQty")).Text.Trim()));
                    string trialordr = ((TextBox)this.gvShiMentInfo.Rows[j].FindControl("TxtTrialOrder")).Text.Trim();
                    string country = ((TextBox)this.gvShiMentInfo.Rows[j].FindControl("TxtCountry")).Text.Trim();
                    string custorder = ((TextBox)gvShiMentInfo.Rows[j].FindControl("TxtCustOrder")).Text.ToString();

                    ((TextBox)this.gvShiMentInfo.Rows[j].FindControl("lblgvShimentQty")).Text = dgvShiQty.ToString("#,##0.00;(#,##0.00); ");
                    TblRowIndex2 = (this.gvShiMentInfo.PageIndex) * this.gvShiMentInfo.PageSize + j;

                    tbl1.Rows[TblRowIndex2]["shimentdate"] = dgvSubCod;
                    tbl1.Rows[TblRowIndex2]["shimentqty"] = dgvShiQty;
                    tbl1.Rows[TblRowIndex2]["proplanqty"] = dgvProQty;
                    tbl1.Rows[TblRowIndex2]["trialordr"] = trialordr;
                    tbl1.Rows[TblRowIndex2]["country"] = country;
                    tbl1.Rows[TblRowIndex2]["custorder"] = custorder;
                    tbl1.Rows[TblRowIndex2]["startdate"] = startdate;
                    tbl1.Rows[TblRowIndex2]["enddate"] = enddate;

                }
                
                Session["tbExPlan"] = tbl1;
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvIExPlanInfo_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["tbExPlan"];
                DataTable tbl2 = (DataTable)Session["tbExPlan2"];
                DataTable tbl3 = (DataTable)Session["tbProcessStat"];

                this.gvShiMentInfo.DataSource = tbl1;
                this.gvShiMentInfo.DataBind();
                
                this.gvShiMentInfo2.DataSource = tbl2;
                this.gvShiMentInfo2.DataBind();

                this.gvProcessStat.DataSource = tbl3;
                this.gvProcessStat.DataBind();

                ((Label)this.gvShiMentInfo2.FooterRow.FindControl("gvsilftrTtlFgQty")).Text = Convert.ToDouble(tbl2.Compute("Sum(rsqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvShiMentInfo2.FooterRow.FindControl("gvsilftrTtlMatQty")).Text = Convert.ToDouble( tbl2.Compute("Sum(matqty)", string.Empty)).ToString("#,##0.00;(#,##0.00); ");

                if (tbl1.Rows.Count == 0)
                    return;
                       this.lbtnResFooterTotal_Click(null, null);
            }
            catch (Exception ex)
            {

            }
        }


        protected void Get_Receive_Info()
        {
            Session.Remove("tblorderno");
            string comcod = this.GetCompCode();

            string OrderNo = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Trim().Substring(24, 8);
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GETPRESHIMENT", OrderNo, styleid, colorid, dayid, "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tbExPlan"] = ds1.Tables[0];
            Session["tblPOsum"] = ds1.Tables[2];
           
            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.ddlOrderList.SelectedValue = ds1.Tables[1].Rows[0]["orderno"].ToString();
               
                //this.txtCurStartDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["startdate"]).ToString("dd.MM.yyyy");
                //this.txtCurEndDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["enddate"]).ToString("dd.MM.yyyy");
                //this.lblddlOrder.Text = (this.ddlOrderList.Items.Count == 0 ? "XXX" : this.ddlOrderList.SelectedItem.Text.Trim());
            }
            if (ds1.Tables[2].Rows.Count > 0)
            {
                this.DdlCustorder.DataTextField = "custordno";
                this.DdlCustorder.DataValueField = "custordno";
                this.DdlCustorder.DataSource = ds1.Tables[2];
                this.DdlCustorder.DataBind();
                DdlCustorder_SelectedIndexChanged(null,null);
            }
            DataTable Tempdt;
            DataView Tempdv;
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "GETLCDETINFO", OrderNo, dayid, "", "", "", "", "", "", "");
            Tempdt = ds2.Tables[0].Copy();
            Tempdv = Tempdt.DefaultView;
            Tempdv.RowFilter = ("gcod ='010100101009' or gcod ='010100101010'");
            Tempdt = Tempdv.ToTable();
            if ((Tempdt.Rows[0]["gdesc1"].ToString() != "") && (Tempdt.Rows[1]["gdesc1"].ToString() != ""))
            {
                this.GetRequisitionList();
                this.gvIExPlanInfo_DataBind();
                this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + OrderNo + "&dayid=" + dayid;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Add Conversion Rate');", true);
                this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + OrderNo + "&dayid=" + dayid;
            }


        }

        private void GetRequisitionList()
        {
            string comcod = this.GetCompCode();
            string OrderNo = this.ddlOrderList.SelectedValue.ToString();
            string dayid = this.ddlStyle.SelectedValue.ToString().Trim().Substring(24, 8);
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDER_WISE_PRODUCTION_REQ_LIST", OrderNo, dayid);

            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                Session["tbExPlan2"] = null;
                gvShiMentInfo2.DataSource = null;
                gvShiMentInfo2.DataBind();
                return;
            }
            Session["tbExPlan2"] = ds1.Tables[0];
            Session["tbProcessStat"] = ds1.Tables[1];
        }

        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.Session_tbExPlan_Update();
            DataTable tbl1 = (DataTable)Session["tbExPlan"];
            //DataView dv = tbl1.DefaultView;
            //dv.RowFilter = "slnum desc";
            //DataTable sortedDT = dv.ToTable();
            string Mlccod = this.ddlOrderList.SelectedValue.ToString();
            string custorderno = this.DdlCustorder.SelectedValue.ToString();

            string line = this.ddlLine.SelectedValue.ToString();
            string linedesc = this.ddlLine.SelectedItem.ToString();
            string ShimentDate = GetStdDate(this.txtCurShMentDate.Text.Trim());
            string startdate = GetStdDate(this.txtCurStartDate.Text.Trim());
            string enddate = GetStdDate(this.txtCurEndDate.Text.Trim());
            string ShimentQty = "0" + this.txtShQty.Text;
            string ProdPlanQty = "0" + this.txtProQty.Text;
            string slnum = "000";
            if (tbl1.Rows.Count > 0)
            {
                 slnum = (Convert.ToInt32(tbl1.Compute("max([slnum])", string.Empty)) == 0) ? "000" : Convert.ToInt32(tbl1.Compute("max([slnum])", string.Empty)).ToString();

            }
            DataRow[] dr = tbl1.Select("custorder = '" + custorderno + "' and trialordr = ''");
            if (dr.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Customer and Floor Order Already Added');", true);

                return;
            }
                string slnum1 = ASTUtility.Right("000" + (Convert.ToInt16(slnum) + 1), 3).ToString();
            DataRow[] dr2 = tbl1.Select("slnum='" + slnum1 + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["orderno"] = Mlccod;
                dr1["linedesc"] = linedesc;
                dr1["prodline"] = line;
                dr1["shimentdate"] = ShimentDate;
                dr1["shimentqty"] = ShimentQty;
                dr1["proplanqty"] = ProdPlanQty;
                dr1["custorder"] = custorderno;
                dr1["country"] = "";
                dr1["trialordr"] = "";
                dr1["startdate"] = startdate;
                dr1["enddate"] = enddate;
                dr1["slnum"] = slnum1;
                dr1["ordsheet"] = "false";
                dr1["tarqty"] = "0.00";
                tbl1.Rows.Add(dr1);
            }

            int RowNo = 1;
            Session["tbExPlan"] = tbl1;
            double PageNo = Math.Ceiling(RowNo * 1.00 / this.gvShiMentInfo.PageSize);
            this.gvShiMentInfo.PageIndex = Convert.ToInt32(PageNo - 1);
            this.gvIExPlanInfo_DataBind();

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tbExPlan_Update();
            this.gvShiMentInfo.PageIndex = ((DropDownList)this.gvShiMentInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvIExPlanInfo_DataBind();
        }

        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            this.Session_tbExPlan_Update();
            DataTable tbl1 = (DataTable)Session["tbExPlan"];
            ((Label)this.gvShiMentInfo.FooterRow.FindControl("lgvFSQty")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(shimentqty)", "")) ?
                    0.00 : tbl1.Compute("Sum(shimentqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvShiMentInfo.FooterRow.FindControl("lgvFProQty")).Text =
                Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(proplanqty)", "")) ?
                    0.00 : tbl1.Compute("Sum(proplanqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            double ordrqty = Convert.ToDouble("0" + this.ordqty.Text.Trim());
            this.lblDueTod.Text = (ordrqty - Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(proplanqty)", "")) ?
                    0.00 : tbl1.Compute("Sum(proplanqty)", "")))).ToString("#,##0.00;(#,##0.00); ");
        }

        protected void ImgbtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            this.Load_Project_Combo();
        }

        protected void ddlOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlOrderList.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable(true, "styledesc2", "stylecode1");
            this.ddlStyle.DataTextField = "styledesc2";
            this.ddlStyle.DataValueField = "stylecode1";
            this.ddlStyle.DataSource = dt1;
            this.ddlStyle.DataBind();
            if (this.Request.QueryString["sircode"].Length > 0)
            {
                this.ddlStyle.SelectedValue = this.Request.QueryString["sircode"].ToString();
            }
            this.ddlStyle_SelectedIndexChanged(null, null);
        }
        protected void ImgbtnFindOrder_Click(object sender, EventArgs e)
        {
            Load_Project_Combo();
        }
        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            this.Session_tbExPlan_Update();
            DataTable tbl1 = (DataTable)Session["tbExPlan"];

            string mlccod = this.ddlOrderList.SelectedValue.ToString().Trim();
            string styleid = this.ddlStyle.SelectedValue.ToString().Trim().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Trim().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Trim().Substring(24, 8);
            string StartDate = GetStdDate(this.txtCurStartDate.Text.Trim());
            string EndDate = GetStdDate(this.txtCurEndDate.Text.Trim());

            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string posteddat = DateTime.Today.ToString("dd-MMM-yyyy hh:mm:ss");

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATEEXPLANINFO", "EXPLANST",
                             mlccod, styleid, colorid, dayid, usrid, sessionid, trmid, posteddat, "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                string slnum = tbl1.Rows[i]["slnum"].ToString();
                string prodline = tbl1.Rows[i]["prodline"].ToString();
                string Shimentdate = tbl1.Rows[i]["shimentdate"].ToString();
                double shimentqty = Convert.ToDouble(tbl1.Rows[i]["shimentqty"].ToString());
                double proplanqty = Convert.ToDouble(tbl1.Rows[i]["proplanqty"].ToString());
                string trialorder = tbl1.Rows[i]["trialordr"].ToString();
                string country = tbl1.Rows[i]["country"].ToString();
                string custorder = tbl1.Rows[i]["custorder"].ToString();
                string startdate = tbl1.Rows[i]["startdate"].ToString();
                string enddate = tbl1.Rows[i]["enddate"].ToString();
                //if (shimentqty > 0)
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATEEXPLANINFO", "EXPLANDET",
                                mlccod, Shimentdate, shimentqty.ToString(), proplanqty.ToString(), styleid, prodline, colorid, trialorder, country, dayid, custorder, startdate, enddate, slnum, "");
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + purData.ErrorObject["Msg"].ToString() + "');", true);

                    return;
                }


            }
            this.txtCurStartDate.Enabled = false;
            this.txtCurEndDate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

        }
        protected void gvShiMentInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tbExPlan"];
            string sORDERNO = this.ddlOrderList.SelectedValue.ToString().Trim();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string line = this.ddlStyle.SelectedValue.ToString().Trim();
            string prodline = ((Label)this.gvShiMentInfo.Rows[e.RowIndex].FindControl("lblgvLine")).Text.Trim();
            string date = Convert.ToDateTime(((Label)this.gvShiMentInfo.Rows[e.RowIndex].FindControl("lblgvShMentDat")).Text).ToString("dd-MMM-yyyy");
            string slnum = ((Label)this.gvShiMentInfo.Rows[e.RowIndex].FindControl("lblgvSlnum")).Text.ToString();
           
            //lblgvLine
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "DELETEORDER", sORDERNO, styleid, prodline, date, colorid, dayid, slnum, "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvShiMentInfo.PageSize) * (this.gvShiMentInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Unable to Delete due to Plan";
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            }

            DataView dv = dt.DefaultView;
            Session["tbExPlan"] = dv.ToTable();
            this.gvIExPlanInfo_DataBind();
        }

        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            DataSet result = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSTYLE_WISE_INFO", mlccod, styleid, colorid, dayid, "", "", "", "", "");

            if (result == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Data Not Found');", true);
                return;
            }
            this.BuyerName.Text = result.Tables[0].Rows[0]["buyername"].ToString();
            this.ordqty.Text = Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0;(#,##0); ");

        }

        protected void LbtnGenerate_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string sORDERNO = this.ddlOrderList.SelectedValue.ToString().Trim();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string prodline = this.ddlLine.SelectedValue.ToString().Trim();
            string ffromtDate = GetStdDate(this.txtCurStartDate.Text.Trim());
            string toDate = GetStdDate(this.txtCurEndDate.Text.Trim());
            string sdate = GetStdDate(this.txtCurShMentDate.Text.Trim());
            string proqty = this.txtProQty.Text.Trim().ToString();
            string ordqty = Convert.ToDouble("0" + this.ordqty.Text.Trim()).ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATE_ORDER_STYLE_WISE_PLAN", sORDERNO, styleid, ffromtDate, toDate, proqty, sdate, ordqty, prodline, colorid, dayid, "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + purData.ErrorObject["Msg"].ToString() + "');", true);
                return;
            }

            this.txtCurStartDate.Enabled = false;
            this.txtCurEndDate.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            this.Get_Receive_Info();
        }

        protected void LbtnSizes_Click(object sender, EventArgs e)
        {
            Session.Remove("SizePlan");
            this.gvsizes.DataSource = null;
            this.gvsizes.DataBind();
            this.LbtnSizesUpdate.Visible = true;
            this.LbtnMoldUpdaet.Visible = false;
            this.ModalHead.Text = "Trial Order Color Wise Size Breakdown";
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string toddat = Convert.ToDateTime(((Label)this.gvShiMentInfo.Rows[index].FindControl("lblgvShMentDat")).Text).ToString("dd-MMM-yyyy");
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string style = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string slnum = ((Label)this.gvShiMentInfo.Rows[index].FindControl("lblgvSlnum")).Text.ToString();

            DataSet result = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERWISE_SIZE_INFORMATION", mlccod, style, colorid, dayid, toddat, slnum);
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
            if(this.GetCompCode() != "5301")
            {
                SizeINput_Selection();

            }
            /// show main order balance

            string type = (dayid != "00000000") ? "Reorder" : "";
            string date = (dayid == "00000000") ? "01-Jan-1900" :  Convert.ToDateTime(dayid.Substring(4,2)+"/"+dayid.Substring(6,2)+"/"+dayid.Substring(0,4)).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_ORDER_DETAILS", mlccod, type, date, style, "", "", "", ""); ;

            for (int i = 5; i < 45; i++)
                this.gv1.Columns[i].Visible = false;

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gv1.Columns[columid + 4].Visible = true;
                this.gv1.Columns[columid + 4].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }
         List<SPEENTITY.C_01_Mer.GetOrderWithCat> lst= ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCat>();
            ViewState["tblOrderQty"] = lst;
            this.gv1.DataSource = lst;
            this.gv1.DataBind();
            this.FooterCal();
         


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);
        }
        private void SizeINput_Selection()
        {
            DataTable dt =(DataTable)ViewState["tblsizesdetails"];       

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
        private void FooterCal()
        {
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
            if(dt!=null && dt.Rows.Count > 0)
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
        protected void LbtnSizesUpdate_Click(object sender, EventArgs e)
        {
            try
            {


                string comcod = this.GetCompCode();
                string mlccod = this.ddlOrderList.SelectedValue.ToString();
                string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
                string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
                string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
                DataTable dt = (DataTable)ViewState["tblsizes"];
                string shipmntdat = "";
                string slnum = "";
                for (int i = 0; i < gvsizes.Rows.Count; i++)
                {
                    string mStyleID = ((Label)gvsizes.Rows[i].FindControl("lblgvStyleID")).Text.Trim();
                    string mColorID = ((Label)gvsizes.Rows[i].FindControl("lblgvColorID")).Text.Trim();
                    shipmntdat = Convert.ToDateTime(((Label)gvsizes.Rows[i].FindControl("lblgvSipmentdate")).Text).ToString("dd-MMM-yyyy");
                    slnum = ((Label)gvsizes.Rows[i].FindControl("mlblgvSlnum")).Text.Trim();

                    String[] SizeID = {"720100101001", "720100101002", "720100101003", "720100101004", "720100101005", "720100101006",
                               "720100101007", "720100101008", "720100101009", "720100101010", "720100101011", "720100101012",
                               "720100101013", "720100101014", "720100101015", "720100101016", "720100101017", "720100101018", "720100101019", "720100101020",
                                  "720100101021", "720100101022", "720100101023", "720100101024", "720100101025", "720100101026", "720100101027", "720100101028",
                                  "720100101029", "720100101030", "720100101031", "720100101032", "720100101033", "720100101034", "720100101035", "720100101036", "720100101037",
                                  "720100101038", "720100101039", "720100101040"};
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
                bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "UPDATE_PLAN_SIZEINFORMATION", ds, null, null, mlccod, styleid, colorid, dayid, shipmntdat, slnum, "");
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

        protected void gvShiMentInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink checkmaterial = (HyperLink)e.Row.FindControl("HypCheckMat");
                HyperLink ordersheet = (HyperLink)e.Row.FindControl("lbtnOrdersheet");
                HyperLink HypPlaning = (HyperLink)e.Row.FindControl("HypPlaning");

                string ordstatus = ((Label)e.Row.FindControl("lblOrderStatus")).Text.Trim().ToString();
                double tarqty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "tarqty"));
               
               
                if (tarqty > 0)
                {
                  
                    e.Row.ToolTip = "Size Wise Plan Assortment Saved";
                    ((TextBox)e.Row.FindControl("lblgvProQty")).Enabled = false;
                    ((TextBox)e.Row.FindControl("TxtStrdate")).Enabled = false;
                    ((TextBox)e.Row.FindControl("TxtEnddate")).Enabled = false;
                    ((TextBox)e.Row.FindControl("TxtEnddate")).Enabled = false;
                    ((TextBox)e.Row.FindControl("TxtTrialOrder")).Enabled = false;
                    e.Row.BackColor = System.Drawing.Color.LightSkyBlue;
                }
                string mlccod = this.ddlOrderList.SelectedValue.ToString();
                string stylecolor = this.ddlStyle.SelectedValue.ToString();
                string trialqty = Convert.ToDouble("0" + ((TextBox)e.Row.FindControl("lblgvProQty")).Text.Trim()).ToString();
                string toddate = Convert.ToDateTime(((Label)e.Row.FindControl("lblgvShMentDat")).Text.Trim()).ToString("dd-MMM-yyyy");
                string slnum = ((Label)e.Row.FindControl("lblgvSlnum")).Text.Trim().ToString();
                string linecode = ((Label)e.Row.FindControl("lblgvLine")).Text.Trim().ToString();


                if (ordstatus == "True")
                {
                    ordersheet.ToolTip = "Saved";
                    ordersheet.Attributes.Add("class", "text-red");
                }
                else
                {
                    ordersheet.ToolTip = "Not Saved";
                    ordersheet.Attributes.Add("class", "text-green");
                }
                checkmaterial.NavigateUrl = "~/F_15_Pro/MatAvailabilityFG?Type=FG&actcode=" + mlccod + "&sircode=" + stylecolor + "&genno=" + trialqty;

                ordersheet.NavigateUrl = "~/F_05_ProShip/OrderSheetPlan?Type=Entry&actcode=" + mlccod + "&sircode=" + stylecolor + "&date=" + toddate + "&genno=" + slnum;
                HypPlaning.NavigateUrl = "~/F_05_ProShip/ProductionPlan?Type=Entry&actcode=" + mlccod + "&sircode=" + stylecolor + "&date=" + toddate + "&genno=" + slnum+ "&centrid=" + linecode;


            }
        }

        protected void LbtnModalBreakDown_Click(object sender, EventArgs e)
        {


            this.gvsizes.DataSource = null;
            this.gvsizes.DataBind();
            this.LbtnSizesUpdate.Visible = false;
            this.LbtnMoldUpdaet.Visible = true;
            string comcod = this.GetCompCode();
            this.ModalHead.Text = "Trial Order Color Wise Mold Size Stock Update";
            string mlccod = this.ddlOrderList.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            DataSet result = purData.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_ORDERWISE_MOLDSIZE_INFORMATION", mlccod, styleid, dayid);
            if (result == null)
            {
                return;
            }
            ViewState["tblmold"] = result.Tables[1];
            for (int i = 0; i < result.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(result.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gvsizes.Columns[columid + 3].Visible = true;
                this.gvsizes.Columns[columid + 3].HeaderText = result.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }
            this.gvsizes.Columns[20].Visible = false;
            this.gvsizes.Columns[2].Visible = false;
            this.gvsizes.EditIndex = -1;

            this.gvsizes.DataSource = result.Tables[0];
            this.gvsizes.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModal();", true);
        }

        protected void LbtnMoldUpdaet_Click(object sender, EventArgs e)
        {
            try
            {


                string comcod = this.GetCompCode();
                string mlccod = this.ddlOrderList.SelectedValue.ToString();
                string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);

                DataTable dt = (DataTable)ViewState["tblmold"];
                string shipmntdat = "";
                for (int i = 0; i < gvsizes.Rows.Count; i++)
                {
                    string mStyleID = ((Label)gvsizes.Rows[i].FindControl("lblgvStyleID")).Text.Trim();
                    shipmntdat = Convert.ToDateTime(((Label)gvsizes.Rows[i].FindControl("lblgvSipmentdate")).Text).ToString("dd-MMM-yyyy");
                    String[] SizeID = {"720100101001", "720100101002", "720100101003", "720100101004", "720100101005", "720100101006",
                               "720100101007", "720100101008", "720100101009", "720100101010", "720100101011", "720100101012",
                               "720100101013", "720100101014", "720100101015" , "720100101016", "720100101017", "720100101018", "720100101019", "720100101020",
                                  "720100101021", "720100101022", "720100101023", "720100101024", "720100101025", "720100101026", "720100101027", "720100101028",
                                  "720100101029", "720100101030", "720100101031", "720100101032", "720100101033", "720100101034", "720100101035", "720100101036", "720100101037",
                                  "720100101038", "720100101039", "720100101040"};
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


                    for (int j = 0; j < SizeID.Length; j++) ///SizeID.Length - 1  change by 26-Feb-21
                    {

                        if (Convert.ToDouble(OrderQty[j]) > 0)
                        {
                            dt.Rows[j]["moldqty"] = Convert.ToDouble(OrderQty[j]).ToString();
                        }


                    }


                }
                DataSet ds = new DataSet("ds1");
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "tblmold";
                bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_EXPORT", "UPDATE_MOLDQTY", ds, null, null, mlccod, styleid, "");
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

        protected void DdlCustorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt =(DataTable) Session["tblPOsum"];
            string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
            string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
            string custorderno = this.DdlCustorder.SelectedValue.ToString();

            DataView dv = dt.Copy().DefaultView;
            dv.RowFilter = "custordno='"+ custorderno + "' and dayid='" + dayid + "' and styleid='" + styleid + "'";
            if (dv.ToTable().Rows.Count > 0)
            {
                this.txtProQty.Text = Convert.ToDouble(dv.ToTable().Rows[0]["ordrqty"]).ToString("#,##0");
                this.txtShQty.Text = Convert.ToDouble(dv.ToTable().Rows[0]["ordrqty"]).ToString("#,##0");

            }

        }


        protected void lnkbtnPrintCombined_Click(object sender, EventArgs e)
        {
            string order = "";
            string reqno = "";
            string reqdat = "";

            for (int i = 0; i < this.gvShiMentInfo2.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.gvShiMentInfo2.Rows[i].FindControl("chkPrntCombined");

                if (chkbox.Checked)
                {
                    string mlccod = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblBatchCode")).Text;
                    string dayid = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblDayId")).Text;
                    string order2 = mlccod + dayid;
                    reqdat = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblReqDate")).Text;

                    if (order != order2)
                    {
                        order += order2;
                    }

                    if (order.Length > 20)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have selected more than 1 order');", true);
                        return;
                    }

                    reqno += ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvlblsiReqNo")).Text;

                }
            }

            string printFormat = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue;
            string url = ResolveClientUrl("~//F_15_Pro/Print.aspx?Type=PrintReqMulti&mlccod=" + order + "&reqno=" + reqno + "&pbdate=" + reqdat + "&printfrmt=" + printFormat);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);
        }

        protected void LbtnIssueMulti_Click(object sender, EventArgs e)
        {
            string order = "";
            string reqno = "";
            string reqdat = "";

            for (int i = 0; i < this.gvShiMentInfo2.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.gvShiMentInfo2.Rows[i].FindControl("chkPrntCombined");

                if (chkbox.Checked)
                {
                    string mlccod = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblBatchCode")).Text;
                    string dayid = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblDayId")).Text;
                    string order2 = mlccod + dayid;
                    reqdat = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblReqDate")).Text;

                    if (order != order2)
                    {
                        order += order2;
                    }

                    if (order.Length > 20)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have selected more than 1 order');", true);
                        return;
                    }

                    reqno += ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvlblsiReqNo")).Text;

                }
            }

            string url = ResolveClientUrl("~/F_11_RawInv/PBMatIssueSingle?Type=Entry&genno=" + reqno + "&actcode=" + order.Substring(0, 12) + "&reptype=NORMAL");
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);
        }

        protected void LbtnprodEntry_Click(object sender, EventArgs e)
        {
            string OrderNo = this.ddlOrderList.SelectedValue.ToString();
            string dayid = this.ddlStyle.SelectedValue.ToString().Trim().Substring(24, 8);
            string reqno = "";
           
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string sircode = ((Label)this.gvProcessStat.Rows[index].FindControl("LblProcess")).Text.ToString();
            string reqdate = "01-Jan-2023";
           
            for (int i = 0; i < this.gvShiMentInfo2.Rows.Count; i++)
            {
                var chkbox = (CheckBox)this.gvShiMentInfo2.Rows[i].FindControl("chkPrntCombined");

                if (chkbox.Checked)
                {
                    reqno += ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvlblsiReqNo")).Text;
                    reqdate = ((Label)this.gvShiMentInfo2.Rows[i].FindControl("gvsilblReqDate")).Text; 
                }
            }

            if (reqno.Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You have not selected any requisition.');", true);
                return;
            }

            string url = ResolveClientUrl("~/F_15_Pro/ProductionProcess?Type=ProTransfer&actcode=" + OrderNo + "&genno=" + reqno + "&sircode=" 
                + sircode + "&date=" + reqdate + "&dayid=" + dayid);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "ShowWindow('" + url + "')", true);
        }

        protected void gvProcessStat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string sircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "prostep"));
                var prostep = ASTUtility.Right(sircode, 2);

                double balance = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "balqty"));

                if (balance <= 0 || prostep == "99")
                {
                    ((LinkButton)e.Row.FindControl("LbtnprodEntry")).Visible = false;
                }
            }
        }
    }
}