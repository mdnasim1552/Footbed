using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SPELIB;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_15_Pro
{
    public partial class RptProductionConsumption : System.Web.UI.Page
    {
        ProcessAccess ProData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "Daywise") ? " Day Wise Material Consumption Report" : "Production Vs Consumption";

                this.GetSeason();
                this.DdlSeason_SelectedIndexChanged(null, null);


                if (this.Request.QueryString["Type"].ToString() == "Daywise")
                {
                    this.lblArticle.Visible = true;
                    this.ddlOrderList.Visible = true;
                }
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetSeason()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");
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

            this.DdlSeason_SelectedIndexChanged(null, null);
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblopvss");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.ShowView();

        }

        private void ShowView()
        {
            Session.Remove("tblprodcon");
            string comcod = this.GetComCode();
           
            string fdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string tdate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string season = this.DdlSeason.SelectedValue.ToString();
            string rtype = this.ddlRType.SelectedValue.ToString();
            string bomid = "";
            foreach (ListItem item in ddlOrderList.Items)
            {
                if (item.Selected)
                {
                    bomid += item.Value;
                }
            }

            string search = "%" + this.Searchbox.Text.ToString() + "%";
            DataSet ds2 = new DataSet();
            if (this.Request.QueryString["Type"].ToString() == "Daywise")
            {
                this.MultiView1.ActiveViewIndex = 0;
                ds2 = ProData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "PRODUCTION_MATERIAL_CONSUMPTION", fdate, tdate, rtype, search, season, bomid, "", "");
                if (ds2 == null)
                    return;
                //Session["tblprodcon"] = HiddenSameData(ds2.Tables[0]);
                Session["tblprodcon"] = ds2.Tables[0];
            }    
            this.LoadGrid();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string preqno = dt1.Rows[0]["preqno"].ToString();
            string batchcode = dt1.Rows[0]["batchcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["preqno"].ToString() == preqno && dt1.Rows[j]["batchcode"].ToString() == batchcode)
                {
                    preqno = dt1.Rows[j]["preqno"].ToString();
                    batchcode = dt1.Rows[j]["batchcode"].ToString();
                    
                    dt1.Rows[j]["batchdesc"] = "";
                    dt1.Rows[j]["bomid"] = "";
                    dt1.Rows[j]["rproqty"] = 0.00;
                    dt1.Rows[j]["recfgqty"] = 0.00;
                    dt1.Rows[j]["balfgqty"] = 0.00;

                }

                else
                {
                   
                    preqno = dt1.Rows[j]["preqno"].ToString();
                    batchcode = dt1.Rows[j]["batchcode"].ToString();

                }

            }


            return dt1;


        }

        private void LoadGrid()
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Daywise":
                    string rtype = this.ddlRType.SelectedValue.ToString();
                    switch (rtype)
                    {
                        case "DPR":
                            this.gvProdCons.Columns[1].Visible = true;
                            this.gvProdCons.Columns[2].Visible = true;
                            this.gvProdCons.Columns[3].Visible = true;
                            this.gvProdCons.Columns[4].Visible = true;
                            this.gvProdCons.Columns[6].Visible = true;
                            this.gvProdCons.Columns[7].Visible = true;
                            this.gvProdCons.Columns[8].Visible = true;
                            break;
                        case "BOM":
                            this.gvProdCons.Columns[1].Visible = false;
                            this.gvProdCons.Columns[2].Visible = false;
                            this.gvProdCons.Columns[3].Visible = false;
                            this.gvProdCons.Columns[4].Visible = true;
                            this.gvProdCons.Columns[6].Visible = true;
                            this.gvProdCons.Columns[7].Visible = true;
                            this.gvProdCons.Columns[8].Visible = true;
                            break;
                        case "DAILY":
                            this.gvProdCons.Columns[1].Visible = false;
                            this.gvProdCons.Columns[2].Visible = true;
                            this.gvProdCons.Columns[3].Visible = false;
                            this.gvProdCons.Columns[4].Visible = false;
                            this.gvProdCons.Columns[6].Visible = true;
                            this.gvProdCons.Columns[7].Visible = true;
                            this.gvProdCons.Columns[8].Visible = true;
                            break;
                        case "SUMMARY":
                            this.gvProdCons.Columns[1].Visible = true;
                            this.gvProdCons.Columns[2].Visible = true;
                            this.gvProdCons.Columns[3].Visible = true;
                            this.gvProdCons.Columns[4].Visible = true;
                            this.gvProdCons.Columns[6].Visible = false;
                            this.gvProdCons.Columns[7].Visible = false;
                            this.gvProdCons.Columns[8].Visible = false;
                            break;
                    }

                    this.gvProdCons.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvProdCons.DataSource = (DataTable)Session["tblprodcon"];
                    this.gvProdCons.DataBind();
                    this.FooterCal();
                    break;

              

            }


        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblprodcon"];
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "OrdProVsShip":
                    ((Label)this.gvProdCons.FooterRow.FindControl("lblgvFOrdrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ?
                               0 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProdCons.FooterRow.FindControl("lblgvFProqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proqty)", "")) ?
                                       0 : dt.Compute("sum(proqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProdCons.FooterRow.FindControl("lblgvFShpqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shipqty)", "")) ?
                                       0 : dt.Compute("sum(shipqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProdCons.FooterRow.FindControl("lblgvFBProQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balpro)", "")) ?
                                       0 : dt.Compute("sum(balpro)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProdCons.FooterRow.FindControl("lblgvFBShpqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balship)", "")) ?
                                       0 : dt.Compute("sum(balship)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            RptEmpAllStatus();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void gvProdCons_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProdCons.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        private void RptEmpAllStatus()
        {

            //Fahad
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string Type = this.ddlRType.SelectedValue.Trim().ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblprodcon"];
            var lst = dt.DataTableToList<SPEENTITY.C_15_Pro.DayWiseMatCons.DPRWiseMattCons>();
            LocalReport Rpt1 = new LocalReport();
            string Rpttitle = "";

            //case "R_15_Pro.RptDPRWiseMatCons": Rpt1a = SetRptDayWiseMatCons(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
            //case "R_15_Pro.RptBOMWiseMatCons": Rpt1a = SetRptDayWiseMatCons(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
            //case "R_15_Pro.RptDayWiseMatCons": Rpt1a = SetRptDayWiseMatCons(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
            //case "R_15_Pro.RptSummerWiseMatCons": Rpt1a = SetRptDayWiseMatCons(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
            switch (Type)
            {
                case "BOM": Rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptBOMWiseMatCons", lst, null, null);
                    Rpttitle = "BOM Wise Material Consumption Report";
                    break;
                case "DAILY": Rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptDayWiseMatCons", lst, null, null);
                    Rpttitle = "Day Wise Material Consumption Report";
                    break;
                case "SUMMARY": Rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptSummerWiseMatCons", lst, null, null);
                    Rpttitle = "Material Consumption Summary Report";
                    break;
                default: Rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptDPRWiseMatCons", lst, null, null);
                    Rpttitle = "DPR Wise Material Consumption Report";
                    break;
            }
           
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            // Rpt1.SetParameters(new ReportParameter("Date", "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", Rpttitle));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", "1601%", "%", findseason, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblordstyle"] = ds1.Tables[0];
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();

            this.ddlOrderList.DataTextField = "styledesc2";
            this.ddlOrderList.DataValueField = "bomid";
            this.ddlOrderList.DataSource = dt1;
            this.ddlOrderList.DataBind();
        }
    }
}