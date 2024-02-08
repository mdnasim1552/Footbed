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
using SPEENTITY;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.Script.Serialization;

namespace SPEWEB.F_03_CostABgd
{
    public partial class RptLCStuatus : System.Web.UI.Page
    {
        ProcessAccess Data = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtFrmDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFrmDate.Text = Convert.ToDateTime("01" + this.txtFrmDate.Text.Trim().Substring(2)).ToString("dd-MMM-yyyy");
                this.txtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "PeriodicOrderSt") ? "Periodic Order Status" : (this.Request.QueryString["Type"].ToString() == "OrdrArchive") ? "Periodic Order Archive Status" : "Periodic Production Status";
                this.GetBuyer();
                this.SelectView();
                this.GetSesson();
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = Data.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
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
            //DdlSeason_SelectedIndexChanged(null, null);
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void SelectView()
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "OrdrArchive":
                    this.FieldIsDetails.Visible = false;
                    this.RptType.Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "PeriodicOrderSt":
                    this.FieldIsDetails.Visible = false;
                    this.RptType.Visible = true;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "PeriodicProdSt":
                    this.FieldIsDetails.Visible = true;
                    this.RptType.Visible = false;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
            }

        }
        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            DataSet ds2 = Data.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "", "", "", "");
            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds2.Tables[0];
            this.ddlBuyer.DataBind();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "OrdrArchive":
                case "PeriodicOrderSt":
                    this.ShowOrderStatus();
                    string value = this.DdlRptType.SelectedValue.ToString();
                    switch (value)
                    {
                        case "0":
                            this.PnlDetailsStat.Visible = true;
                            this.PnlOrdSummary.Visible = false; 
                            this.PnlShipValue.Visible = false;
                            this.PnlCatQuantity.Visible = false;
                            this.PnlAgentOrder.Visible = false;
                            break;
                        case "1":
                            this.PnlDetailsStat.Visible = false;
                            this.PnlOrdSummary.Visible = true;
                            this.PnlShipValue.Visible = false;
                            this.PnlCatQuantity.Visible = false;
                            this.PnlAgentOrder.Visible = false;
                            break;
                        case "2":
                            this.PnlDetailsStat.Visible = false;
                            this.PnlOrdSummary.Visible = false;
                            this.PnlShipValue.Visible = true;
                            this.PnlCatQuantity.Visible = false;
                            this.PnlAgentOrder.Visible = false;
                            break;
                        case "3":
                            this.PnlDetailsStat.Visible = false;
                            this.PnlOrdSummary.Visible = false;
                            this.PnlShipValue.Visible = false;
                            this.PnlCatQuantity.Visible = true;
                            this.PnlAgentOrder.Visible = false;
                            break;
                        case "4":
                        case "5":
                            this.PnlDetailsStat.Visible = false;
                            this.PnlOrdSummary.Visible = false;
                            this.PnlShipValue.Visible = false;
                            this.PnlCatQuantity.Visible = false;
                            this.PnlAgentOrder.Visible = true;
                            break;

                    }
                    break;                
                    
                case "PeriodicProdSt":
                    this.ShowProdStatus();
                    break;

            }


        }

        private void ShowOrderStatus()
        {
            ViewState.Remove("tblLCorder");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtFrmDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtToDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string season = this.DdlSeason.SelectedValue == "00000" ? "%" : this.DdlSeason.SelectedValue + "%";
            string buyerid = ""; // this.ddlAccProject.SelectedValue.ToString();
            string type = this.DdlRptType.SelectedValue;
            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    buyerid += item.Value;
                }
            }

            DataSet ds2 = Data.GetTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "RPTPERIODICORSTATUS", frmdate, todate, buyerid, season, type, "", "", "", "");
            if (ds2 == null)
            {
                this.gvOrderStatus.DataSource = null;
                this.gvOrderStatus.DataBind();
                return;
            }
            
            ViewState["tblLCorder"] = ds2.Tables[0];
            if(type == "4" || type == "5")
            {
                ViewState["tblAgentName"] = ds2.Tables[1];
            }
            this.Data_Bind();
            this.PieOrderSummary();

        }

        private void ShowProdStatus()
        {
            ViewState.Remove("tblLCorder");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtFrmDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtToDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //this.lnkbtnExclDnld.Visible = true;
            string buyerid = ""; // this.ddlAccProject.SelectedValue.ToString();
            foreach (ListItem item in ddlBuyer.Items)
            {
                if (item.Selected)
                {
                    buyerid += item.Value;
                }
            }

            DataSet ds2 = Data.GetTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "RPTPERIODICPDSTATUS", frmdate, todate, buyerid, "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvOrderStatus.DataSource = null;
                this.gvOrderStatus.DataBind();
                return;
            }

            ViewState["tblLCorder"] = HiddenSameData(ds2.Tables[0]);
            ViewState["tbllproddetails"] = ds2.Tables[1];

            this.Data_Bind();

            //GridView gvTemp = new GridView();
            //gvTemp.AllowPaging = false;
            //gvTemp.DataSource = ds2.Tables[1];
            //gvTemp.DataBind();
            //Session["Report1"] = gvTemp;
            //lnkbtnExclDnld.NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string Type = this.Request.QueryString["Type"].ToString();
            string mlccod, ordrcod;
            switch (Type)
            {
                case "PeriodicOrderSt":
                    mlccod = dt1.Rows[0]["mlccod"].ToString();
                    ordrcod = dt1.Rows[0]["ordrcod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["mlccod"].ToString() == mlccod && dt1.Rows[j]["ordrcod"].ToString() == ordrcod)
                        {
                            mlccod = dt1.Rows[j]["mlccod"].ToString();
                            ordrcod = dt1.Rows[j]["ordrcod"].ToString();
                            dt1.Rows[j]["mlcdesc"] = "";
                            dt1.Rows[j]["ordrdesc"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["mlccod"].ToString() == mlccod)
                                dt1.Rows[j]["mlcdesc"] = "";

                            if (dt1.Rows[j]["ordrcod"].ToString() == ordrcod)
                            {
                                dt1.Rows[j]["ordrdesc"] = "";
                            }
                            mlccod = dt1.Rows[j]["mlccod"].ToString();
                            ordrcod = dt1.Rows[j]["ordrcod"].ToString();
                        }

                    }

                    break;
                case "PeriodicProdSt":
                    string linecode = dt1.Rows[0]["linecode"].ToString();
                    // mlccod = dt1.Rows[0]["mlccod"].ToString();
                    ordrcod = dt1.Rows[0]["ordrcod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["linecode"].ToString() == linecode && dt1.Rows[j]["ordrcod"].ToString() == ordrcod)
                        {
                            //dt1.Rows[j]["mlcdesc"] = "";
                            dt1.Rows[j]["ordrdesc"] = "";
                            dt1.Rows[j]["linedesc"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["linecode"].ToString() == linecode)
                                dt1.Rows[j]["linedesc"] = "";

                            //if (dt1.Rows[j]["mlccod"].ToString() == mlccod)
                            //    dt1.Rows[j]["mlcdesc"] = "";
                            //if (dt1.Rows[j]["ordrcod"].ToString() == ordrcod)
                            //    dt1.Rows[j]["ordrdesc"] = "";

                        }

                        linecode = dt1.Rows[j]["linecode"].ToString();
                        //mlccod = dt1.Rows[j]["mlccod"].ToString();
                        ordrcod = dt1.Rows[j]["ordrcod"].ToString();

                    }

                    break;

            }

            return dt1;

        }

        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            DataTable dt = (DataTable)ViewState["tblLCorder"];
            string RptType = this.DdlRptType.SelectedValue;

            switch (Type)
            {
                case "PeriodicOrderSt":

                    switch(RptType)
                    {
                        case "0":
                            this.gvOrderStatus.Columns[15].Visible = false;
                            this.gvOrderStatus.DataSource = HiddenSameData(dt);
                            this.gvOrderStatus.DataBind();
                            this.FooterCalCualtion();
                            break;
                        case "1":
                            this.gvOrderStSummary.DataSource = dt;
                            this.gvOrderStSummary.DataBind();
                            this.FooterCalCualtion2();
                            this.PieOrderSummary();
                            break;
                        case "2":
                            this.gvShipVal.DataSource = dt;
                            this.gvShipVal.DataBind();
                            if (dt.Rows.Count == 0)
                                return;
                            ((Label)(this.gvShipVal.FooterRow.FindControl("lgvsum6"))).Text = Convert.ToDouble(dt.Compute("SUM(ordrqty)", string.Empty)).ToString("#,##0;(#,##0); ");
                            ((Label)(this.gvShipVal.FooterRow.FindControl("lgvsum7"))).Text = Convert.ToDouble(dt.Compute("SUM(fcamtusd)", string.Empty)).ToString("#,##0.00;(#,##0.00); ") + "  $";
                            ((Label)(this.gvShipVal.FooterRow.FindControl("lgvsum8"))).Text = Convert.ToDouble(dt.Compute("SUM(fcamteuro)", string.Empty)).ToString("#,##0.00;(#,##0.00); ") + "  €";
                            this.BarShipmentValue();
                            break;
                        case "3":
                            this.gvCatQuantity.DataSource = dt;
                            this.gvCatQuantity.DataBind();
                            if (dt.Rows.Count == 0)
                                return;
                            ((Label)(this.gvCatQuantity.FooterRow.FindControl("lgvsum2"))).Text = Convert.ToDouble(dt.Compute("SUM(gents)", string.Empty)).ToString("#,##0;(#,##0); ");
                            ((Label)(this.gvCatQuantity.FooterRow.FindControl("lgvsum3"))).Text = Convert.ToDouble(dt.Compute("SUM(ladies)", string.Empty)).ToString("#,##0;(#,##0); ");
                            ((Label)(this.gvCatQuantity.FooterRow.FindControl("lgvsum4"))).Text = Convert.ToDouble(dt.Compute("SUM(kids)", string.Empty)).ToString("#,##0;(#,##0); ");
                            ((Label)(this.gvCatQuantity.FooterRow.FindControl("lgvsum1"))).Text = Convert.ToDouble(dt.Compute("SUM(ordrqty)", string.Empty)).ToString("#,##0;(#,##0); ");
                            this.PieCategoryQuantity();
                            break;
                        case "4":
                        case "5":
                            DataTable dtAgnt = (DataTable)ViewState["tblAgentName"];
                            //DataTable dtOrdr = (DataTable)ViewState["tblLCorder"];

                            int i = 2;
                            foreach (DataRow item in dtAgnt.Rows)
                            {
                                this.gvAgentOrder.Columns[i].Visible = true;
                                this.gvAgentOrder.Columns[i].HeaderText = item["agentdesc"].ToString();
                                i++;
                            }

                            this.gvAgentOrder.DataSource = dt;
                            this.gvAgentOrder.DataBind();

                            int j = 1;
                            foreach (DataRow x in dtAgnt.Rows)
                            {
                                ((Label)(this.gvAgentOrder.FooterRow.FindControl("lbTtls" + j))).Text = Convert.ToDouble(dt.Compute("SUM(s" +j.ToString() +")", string.Empty)).ToString("#,##0;(#,##0); ");
                                j++;
                            }

                            if (dt.Rows.Count == 0)
                                return;
                            ((Label)(this.gvAgentOrder.FooterRow.FindControl("lgvTtlQty1"))).Text = Convert.ToDouble(dt.Compute("SUM(totalqty)", string.Empty)).ToString("#,##0;(#,##0); ");
                            
                            this.BarExFactoryOrder();

                            break;

                    }
                    break;
                case "OrdrArchive":
                    this.gvOrderStatus.Columns[12].Visible = true;
                    this.gvOrderStatus.DataSource = dt;
                    this.gvOrderStatus.DataBind();
                    this.FooterCalCualtion();
                    break;
                case "PeriodicProdSt":
                    this.gvProdStatus.DataSource = dt;
                    this.gvProdStatus.DataBind();
                    this.FooterCalCualtion();
                    break;
            }

        }

        private void PieOrderSummary()
        {
            DataTable dt2 = (DataTable)ViewState["tblLCorder"];
            var buyerWise = dt2.DataTableToList<BuyerWise>();
            var jsonSerialiser = new JavaScriptSerializer();
            var buyerWise_json = jsonSerialiser.Serialize(buyerWise);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OrderSummaryGraph('" + buyerWise_json + "')", true);
        }

        private void PieCategoryQuantity()
        {
            DataTable dt2 = (DataTable)ViewState["tblLCorder"];
            double gentsRatio = (Convert.ToDouble(dt2.Compute("SUM(gents)", string.Empty)) * 100) / Convert.ToDouble(dt2.Compute("SUM(ordrqty)", string.Empty));
            double LadiesRatio = (Convert.ToDouble(dt2.Compute("SUM(ladies)", string.Empty)) * 100) / Convert.ToDouble(dt2.Compute("SUM(ordrqty)", string.Empty));
            double KidsRatio = (Convert.ToDouble(dt2.Compute("SUM(kids)", string.Empty)) * 100) / Convert.ToDouble(dt2.Compute("SUM(ordrqty)", string.Empty));
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CategoryQtyGraph('" + gentsRatio.ToString("#,##0.00;(#,##0.00);") + "','" + LadiesRatio.ToString("#,##0.00;(#,##0.00);") + "','" + KidsRatio.ToString("#,##0.00;(#,##0.00);") + "')", true);

        }

        private void BarShipmentValue()
        {
            DataTable dt2 = (DataTable)ViewState["tblLCorder"]; 
            var monthwise = dt2.DataTableToList<MonthShipValue>();

            monthwise = monthwise.GroupBy(x => x.exfactordate).Select(y => new MonthShipValue
            {
                exfactordate=y.Key,
                fcamtusd = Convert.ToDouble(y.Sum(a => a.fcamtusd).ToString("#,##0.00;(#,##0.00);")),
                fcamteuro = Convert.ToDouble(y.Sum(a => a.fcamteuro).ToString("#,##0.00;(#,##0.00);")),

            }).ToList();

            var jsonSerialiser = new JavaScriptSerializer();
            var monthWise_json = jsonSerialiser.Serialize(monthwise);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ShipmentValGraph('" + monthWise_json + "')", true);
        }

        private void BarExFactoryOrder()
        {
            DataTable dt = (DataTable)ViewState["tblLCorder"];
            var expagentwise = dt.DataTableToList<MonthShipValue>();

            var jsonSerialiser = new JavaScriptSerializer();
            var monthWise_json = jsonSerialiser.Serialize(expagentwise);

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExFactOrderGraph('" + monthWise_json + "')", true);
        }

        protected void DdlRptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string value = this.DdlRptType.SelectedValue.ToString();
            //switch (value)
            //{
            //    case "DT":
            //        this.gvOrderStatus.Visible = true;
            //        this.gvOrderStSummary.Visible = false;
            //        break;
            //    case "SM":
            //        this.gvOrderStSummary.Visible = true;
            //        this.gvOrderStatus.Visible = false;
            //        break;
            //}
        }

        private void FooterCalCualtion()
        {
            DataTable dt = (DataTable)ViewState["tblLCorder"];
            if (dt.Rows.Count == 0)
                return;
            string Type = this.Request.QueryString["Type"].ToString();


            switch (Type)
            {
                case "PeriodicOrderSt":
                case "OrdrArchive":
                    ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvFAmtfc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(fcamt)", "")) ?
                        0.00 : dt.Compute("Sum(fcamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
                      0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    
                    ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvqtyftr")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ordrqty)", "")) ?
                      0.00 : dt.Compute("Sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvOrderStatus.FooterRow.FindControl("lgvpackSum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(packqty)", "")) ?
                      0.00 : dt.Compute("Sum(packqty)", ""))).ToString("#,##0;(#,##0); ");

                    break;
                case "PeriodicProdSt":

                    ((Label)this.gvProdStatus.FooterRow.FindControl("lgvproFqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(proqty)", "")) ?
                        0.00 : dt.Compute("Sum(proqty)", ""))).ToString("#,##0;(#,##0.00); ");
                    ((Label)this.gvProdStatus.FooterRow.FindControl("lgvFAmtfcPd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amount)", "")) ?
                        0.00 : dt.Compute("Sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvProdStatus.FooterRow.FindControl("lgvFAmtPd")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
                    //  0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }


        }

        private void FooterCalCualtion2()
        {
            DataTable dt1 = (DataTable)ViewState["tblLCorder"];

            if (dt1.Rows.Count > 0)
            {
                double Ordrsum = Convert.ToDouble(dt1.Compute("SUM(ordrqty)", string.Empty));
                double ratioSum = Convert.ToDouble(dt1.Compute("SUM(ratio)", string.Empty));

                ((Label)(this.gvOrderStSummary.FooterRow.FindControl("lgvOrdersum"))).Text = Ordrsum.ToString("#,##0;(#,##0); ");
                ((Label)(this.gvOrderStSummary.FooterRow.FindControl("lgvRatioSum"))).Text = ratioSum.ToString("#,##0.00;(#,##0.00); ") + " %";
            }

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "PeriodicOrderSt":
                    this.PrintPreOrdStatus();
                    break;

                case "PeriodicProdSt":
                    this.PrintPreProdStatus();
                    break;
            }
        }

        private void PrintPreOrdStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Header = (this.Request.QueryString["Type"].ToString() == "PeriodicOrderSt") ? "Periodic Order Status" : "Periodic Production Status";
            string fromtodate = "From: " + this.txtFrmDate.Text + " To: " + this.txtToDate.Text;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            DataTable dt = ((DataTable)ViewState["tblLCorder"]);
            var lst = dt.DataTableToList<SPEENTITY.C_03_CostABgd.PerOrderStatus.PerOrdrStat>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptPreOrderStatus", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("fromtodate", fromtodate));
            Rpt1.SetParameters(new ReportParameter("footer", userinfo));


            ReportDocument rrs1 = new RMGiRPT.R_03_CostABgd.RptPerOrderStatus();
            TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptHeader = rrs1.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            rptHeader.Text = Header;
            TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "From: " + this.txtFrmDate.Text + " To: " + this.txtToDate.Text;
            TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rrs1.SetDataSource((DataTable)ViewState["tblLCorder"]);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rrs1.SetParameterValue("ComLogo", ComLogo);
            var x = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintPreProdStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Header = (this.Request.QueryString["Type"].ToString() == "PeriodicOrderSt") ? "Periodic Order Status" : "Periodic Production Status";
            string fromtodate = "From: " + this.txtFrmDate.Text + " To: " + this.txtToDate.Text;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            DataTable dt = new DataTable();

            LocalReport Rpt1 = new LocalReport();

            if (!chkDtls.Checked)
            {
                dt = ((DataTable)ViewState["tblLCorder"]);

                if (dt == null) return;
                if (dt.Rows.Count == 0) return;

                var lst = dt.DataTableToList<SPEENTITY.C_03_CostABgd.PerOrderStatus.PerProdStat>();
                Rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptPreProdStatus", lst, null, null);
            }
            else
            {
                dt = (DataTable)ViewState["tbllproddetails"];

                if (dt == null) return;
                if (dt.Rows.Count == 0) return;

                var lst = dt.DataTableToList<SPEENTITY.C_03_CostABgd.PerOrderStatus.PerProdStatDetails>();
                Rpt1 = RptSetupClass.GetLocalReport("R_03_CostABgd.RptPreProdStatusDetails", lst, null, null);
            }

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("fromtodate", fromtodate));
            Rpt1.SetParameters(new ReportParameter("footer", userinfo));


            //ReportDocument rrs1 = new RMGiRPT.R_03_CostABgd.RptPerOrderStatus();
            //TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptHeader = rrs1.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //rptHeader.Text = Header;
            //TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From: " + this.txtFrmDate.Text + " To: " + this.txtToDate.Text;
            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource((DataTable)ViewState["tblLCorder"]);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rrs1.SetParameterValue("ComLogo", ComLogo);
            var x = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk1")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvOrderStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                LinkButton archivebtn = (LinkButton)e.Row.FindControl("LbtnArchive");
                bool archive = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "archive"));
                double OrdQty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ordrqty"));
                double PackQty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "packqty"));
                double mlccod = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ordrcod"));

                HyperLink lblgvlcOrder = (HyperLink)e.Row.FindControl("lblgvlcOrder");
                lblgvlcOrder.NavigateUrl = "~/F_01_Mer/OrderDetails?Type=Entry&actcode=" + mlccod;

                if (archive == true)
                {
                    archivebtn.ToolTip = "Active From Archive";                  
                    archivebtn.Text = "<span class=\"fa fa-lock-open\"></span>";
                    archivebtn.CssClass="btn btn-sm btn-success";
                }
                else
                {
                    archivebtn.ToolTip = "Make it Archive";
                    archivebtn.Text = "<span class=\"fa fa-lock\"></span>";
                    archivebtn.CssClass="btn btn-sm btn-danger";                  
                }

                if(OrdQty != PackQty)
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                }

            }
        }

        protected void LbtnArchive_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string mlccod = ((Label)this.gvOrderStatus.Rows[index].FindControl("LblgvOrdNo")).Text.ToString();           
            string dayid = ((Label)this.gvOrderStatus.Rows[index].FindControl("LblgvDayid")).Text.ToString();
            string styleid = ((Label)this.gvOrderStatus.Rows[index].FindControl("LblgvStyle")).Text.ToString();
            bool archive =Convert.ToBoolean(((Label)this.gvOrderStatus.Rows[index].FindControl("Lblgvarchive")).Text.ToString());
            if (archive == true)
            {
                archive = false;
            }
            else
            {
                archive = true;
            }
            bool result = Data.UpdateTransInfo(comcod, "SP_REPORT_MLCORDERSTATUS", "ORDER_ARCHIVE_STATUS_UPDATE", mlccod, dayid, styleid, archive.ToString(), "", "", "", "", "");
            if (result == true)
            {
                this.ShowOrderStatus();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Archive Status Change Successfully');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Something went wrong');", true);

            }

        }

        protected void LbtngvProdNo_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbllproddetails"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string prodno = ((LinkButton)this.gvProdStatus.Rows[index].FindControl("LbtngvProdNo")).Text.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "prdno='"+prodno+"'";
            this.gvProdDetails.DataSource = dv.ToTable();
            this.gvProdDetails.DataBind();
            ((Label)(this.gvProdDetails.FooterRow.FindControl("DlgvproFqty"))).Text = Convert.ToDouble(dv.ToTable().Compute("SUM(proqty)", string.Empty)).ToString("#,##0;(#,##0); ");

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        public System.Web.UI.WebControls.SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (System.Web.UI.WebControls.SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }

        public class BuyerWise
        {
            public string buyerid { get; set; }
            public string buyername { get; set; }
            public double ordrqty { get; set; }
            public double ratio { get; set; }
        }

        public class MonthShipValue
        {
            public string exfactordate { get; set; }
            public double fcamtusd { get; set; }
            public double totalqty { get; set; }
            public double fcamteuro { get; set; }
        }

        protected void gvOrderStSummary_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortDirection = "Asc";

            }

            DataTable dt = (DataTable)ViewState["tblLCorder"];
            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortDirection;
            gvOrderStSummary.DataSource = sortedView;
            gvOrderStSummary.DataBind();

            ViewState["tblLCorder"] = sortedView.ToTable();
            this.FooterCalCualtion2();
            this.PieOrderSummary();
        }
    }
}