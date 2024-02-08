using Microsoft.Reporting.WinForms;
using SPELIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_01_Mer
{
    public partial class RptPfiInvList : System.Web.UI.Page
    {
        ProcessAccess _processAccess = new ProcessAccess();
        Common _common = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //  this.CreateList();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Proforma Invoice List";
                
                this.txtFromDate.Text = "01-"+System.DateTime.Today.ToString("MMM-yyyy");
                this.txtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.hyplnkCrtNew.NavigateUrl = "~/F_03_CostABgd/SalesContact?Type=Entry&genno=&actcode=&dayid=&sircode=";

                this.GetSesson();
                this.GetBuyer();

            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }


        private void GetSesson()
        {
            string comcod = _common.GetCompCode();
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
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

        }

        private void GetBuyer()
        {
            string comcod = _common.GetCompCode();
            string agent = "%";
            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_AGENTWISE_BUYER_LIST", agent, "", "", "", "", "", "", "", "");

            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds1.Tables[0];
            this.ddlBuyer.DataBind();
            this.ddlBuyer.Items.Add(new ListItem { Value = "000000000000", Text = "----All----" });
            this.ddlBuyer.SelectedValue = "000000000000";
        }

        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            string comcod = _common.GetCompCode();
            string txtFromDate = this.txtFromDate.Text.ToString();
            string txtToDate = this.txtToDate.Text.ToString();
            string season = this.ddlSeason.SelectedValue == "00000" ? "%" : this.ddlSeason.SelectedValue+"%";
            string buyer = this.ddlBuyer.SelectedValue == "000000000000" ? "%" : this.ddlBuyer.SelectedValue+"%";

            DataSet ds1 = _processAccess.GetTransInfo(comcod, "SP_INV_STDANA", "GET_DATE_WISE_PFI_REPORT", txtFromDate, txtToDate, buyer, season, "", "");

            if (ds1 != null)
            {
                if(ds1.Tables[0].Rows.Count == 0)
                {
                    return;
                }
            }

            ViewState["tblPfi"] = ds1.Tables[0];

            this.gvPfiList.DataSource = ds1.Tables[0];
            this.gvPfiList.DataBind();
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "PfiRpt":
                    this.PrintPfiRpt();
                    break;
            }
        }

        private void PrintPfiRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = _common.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFromDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtToDate.Text.Trim()).ToString("dd-MMM-yyyy");
            
            DataTable dt = (DataTable)ViewState["tblPfi"];
            var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.RptPfiList>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = SPERDLC.RptSetupClass.GetLocalReport("R_01_Mer.RptPfiList", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Proforma Invoice List"));
            Rpt1.SetParameters(new ReportParameter("date", fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("comlogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvPfiList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                var pfiNo = ((Label)(e.Row.FindControl("gvplLblPfiNo"))).Text;
                //var actcode = ((Label)(e.Row.FindControl("gvplLblArtclNo"))).Text.Trim();
                var buyerid = ((Label)(e.Row.FindControl("gvplLblBuyerId"))).Text.Trim();
                var pfiEditLink = (HyperLink)(e.Row.FindControl("gvplHypLnkEdit"));
                var pfiPfiPrint = (HyperLink)(e.Row.FindControl("gvplHypLnkPrint"));
                var PfiPrintCCC = (HyperLink)(e.Row.FindControl("gvplHypLnkPrintCCC"));

                //string buyer = ((Label)(e.Row.FindControl("gvplLblBuyerDesc"))).Text;
                //if(buyer == "CCC")
                //{
                //    PfiPrintCCC.Enabled = true;
                //    pfiPfiPrint.Enabled = false;
                //}
                //else
                //{
                //    PfiPrintCCC.Enabled = false;
                //    pfiPfiPrint.Enabled = true;
                //}


                pfiEditLink.NavigateUrl = "~/F_03_CostABgd/SalesContact?Type=Edit&genno="+pfiNo+"&actcode=&dayid=&sircode="+buyerid;
                pfiPfiPrint.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PFIWISEORDR&genno=" + pfiNo;
                PfiPrintCCC.NavigateUrl = "~/F_01_Mer/MerChanPrint?Type=PFIWISEORDRv2&genno=" + pfiNo;
            }
        }

        protected void gvplLnkBtnDelt_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)sender).NamingContainer);

            string pfino = ((Label)row.FindControl("gvplLblPfiNo")).Text.Trim();

            string comcod = _common.GetCompCode();
            bool result = _processAccess.UpdateTransInfo(comcod, "SP_INV_STDANA", "DELETE_PROFORMA_INV", pfino, "", "");

            if (result)
            {
                DataTable dt = (DataTable) ViewState["tblPfi"];
                DataView dv = dt.AsDataView();

                dv.RowFilter = "pfino <> '"+pfino+"'";

                DataTable dt2 = dv.ToTable();
                this.gvPfiList.DataSource = dt2;
                this.gvPfiList.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Proforma invoice deleted successfully');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Delete failed');", true);
                return;
            }

        }
    }
}