using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SPELIB;
using SPERDLC;
using Microsoft.Reporting.WinForms;


namespace SPEWEB.F_15_Pro
{
    public partial class RptOrderProShipAll : System.Web.UI.Page
    {
        public static string flag = "";
        ProcessAccess ComData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = "01" + (this.txtFDate.Text.Trim().Substring(2));
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Order, Production & Shipment - All Orders";

                this.GetSesson();
                this.GetBuyer();
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetBuyer()
        {
            string comcod = this.GetCompCode();
            string agent = "%";
            DataSet ds1 = ComData.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_AGENTWISE_BUYER_LIST", agent, "", "", "", "", "", "", "", "");

            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds1.Tables[0];
            this.ddlBuyer.DataBind();
            this.ddlBuyer.Items.Add(new ListItem { Value = "000000000000", Text = "----All----" });
            this.ddlBuyer.SelectedValue = "000000000000";
        }


        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = ComData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            ddlSeason.DataTextField = "gdesc";
            ddlSeason.DataValueField = "gcod";
            ddlSeason.DataSource = ds1.Tables[0];
            ddlSeason.DataBind();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();

            this.ddlSeason.SelectedValue = season;
            
            //DdlSeason_SelectedIndexChanged(null, null);
        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tbOrProShipAll");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string season = this.ddlSeason.SelectedValue == "00000" ? "" : this.ddlSeason.SelectedValue;
            string buyer = this.ddlBuyer.SelectedValue == "000000000000" ? "" : this.ddlBuyer.SelectedValue;

            DataSet ds1 = ComData.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "RPTORDPROSHIALL", fromdate, todate, season, buyer, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvOrdProShip.DataSource = null;
                this.gvOrdProShip.DataBind();
                return;
            }

            ViewState["tbOrProShipAll"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {

            this.gvOrdProShip.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvOrdProShip.DataSource = (DataTable)ViewState["tbOrProShipAll"];
            this.gvOrdProShip.DataBind();
            this.FooterCalculation();
        }
        private void FooterCalculation()
        {


            DataTable dt = (DataTable)ViewState["tbOrProShipAll"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvOrdProShip.FooterRow.FindControl("lgvFOrdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(orval)", "")) ?
                            0 : dt.Compute("sum(orval)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOrdProShip.FooterRow.FindControl("lgvFProamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proval)", "")) ?
                                    0 : dt.Compute("sum(proval)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOrdProShip.FooterRow.FindControl("lgvFShipdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shipval)", "")) ?
                                    0 : dt.Compute("sum(shipval)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvOrdProShip.FooterRow.FindControl("lgvFOrdqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordqty)", "")) ?
                0 : dt.Compute("sum(ordqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOrdProShip.FooterRow.FindControl("lgvFProqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proqty)", "")) ?
                0 : dt.Compute("sum(proqty)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvOrdProShip.FooterRow.FindControl("lgvFShipqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shipqty)", "")) ?
                0 : dt.Compute("sum(shipqty)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            string ToFrDate = "(From :" + fromdate + " To " + todate + ")";

            DataTable dt = (DataTable)ViewState["tbOrProShipAll"];

            var lst = dt.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.OrdProdShipment>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptOrdProShipAll", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("ToFrDate", ToFrDate));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Order, Production & Shipment - All Orders"));
            rpt1.SetParameters(new ReportParameter("Logo", ComLogo));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            // rpt1.SetParameters(new ReportParameter("todate", DateTime.Today.ToString("dd-MMM-yyyy")));



            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lbtnPrint_ClickOLD(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //ReportDocument OrderAll = new RMGiRPT.R_15_Pro.RptOrdProShipAll();
            //TextObject rptCname = OrderAll.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;
            ////TextObject rpttxtHeaderTitle = OrderAll.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            ////rpttxtHeaderTitle.Text = HeaderTitle;
            //TextObject txtFDate1 = OrderAll.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;

            //TextObject txtuserinfo = OrderAll.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //OrderAll.SetDataSource((DataTable)ViewState["tbOrProShipAll"]);
            //Session["Report1"] = OrderAll;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvOrdProShip_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOrdProShip.PageIndex = e.NewPageIndex;
            this.Data_Bind();
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
        protected void gvOrdProShip_OnSorting(object sender, GridViewSortEventArgs e)
        {


            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }

            DataTable dt = ((DataTable)ViewState["tbOrProShipAll"]).Copy();

            //DataTable dt = ds1.Tables[0].Copy();

            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            //ViewState["tbldelorder"] = sortedView.ToTable();
            ViewState["tbOrProShipAll"] = sortedView.ToTable();

            this.Data_Bind();
        }
    }
}