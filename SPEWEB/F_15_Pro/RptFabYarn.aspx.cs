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
using SPERDLC;
using CrystalDecisions.CrystalReports.Engine;

namespace SPEWEB.F_15_Pro
{
    public partial class RptFabYarn : System.Web.UI.Page
    {
        ProcessAccess FabricYarn = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                string type = this.Request.QueryString["Type"].ToString().Trim();
                this.lblTitle.Text = ((type == "Fabric") ? "Fabric Requisition Information" : ((type == "Yarn") ? "Yarn Requisition Information" : ""));

                this.txtfrmDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetMasterLC();
                this.GetOrder();



            }
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;

        }

        private void GetMasterLC()
        {
            string comcod = GetComCode();
            string txtsrch = "%" + this.txtpmlcsrch.Text.Trim() + "%";
            DataSet ds1 = FabricYarn.GetTransInfo(comcod, "SP_REPORT_FABYARN", "GETMASTERLC", txtsrch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlMLc.DataTextField = "actdesc";
            this.ddlMLc.DataValueField = "actcode";
            this.ddlMLc.DataSource = ds1.Tables[0];
            this.ddlMLc.DataBind();


        }

        private void GetOrder()
        {
            string comcod = GetComCode();
            string mlccod = this.ddlMLc.SelectedValue.ToString();
            string txtsrch = "%" + this.txtordercsrch.Text.Trim() + "%";

            DataSet ds2 = FabricYarn.GetTransInfo(comcod, "SP_REPORT_FABYARN", "GETORDER", mlccod, txtsrch, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            this.ddlOrder.DataTextField = "ordername";
            this.ddlOrder.DataValueField = "orderid";
            this.ddlOrder.DataSource = ds2.Tables[0];
            this.ddlOrder.DataBind();

        }
        protected void imgbtnFindPMlc_Click(object sender, ImageClickEventArgs e)
        {
            this.GetMasterLC();
        }
        protected void imgbtnFindOrder_Click(object sender, ImageClickEventArgs e)
        {
            this.GetOrder();
        }

        protected void ddlMLc_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetOrder();
        }
        protected void lbtnShow_Click(object sender, EventArgs e)
        {

            if (this.Request.QueryString["Type"].ToString().Trim() == "Fabric")
                this.ShowFabric();
            else
                this.ShowYarn();






        }
        private void ShowFabric()
        {
            Session.Remove("tblfab");
            this.MultiView1.ActiveViewIndex = 0;
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = GetComCode();
            string mlccod = this.ddlMLc.SelectedValue.ToString();
            string orderid = this.ddlOrder.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = FabricYarn.GetTransInfo(comcod, "SP_REPORT_FABYARN", "SHOWFABFORSPCORD", mlccod, orderid, frmdate, todate, "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvfab.DataSource = null;
                this.gvfab.DataBind();
                return;
            }

            Session["tblfab"] = HiddenSameData(ds3.Tables[0]);
            this.LoadGrid();

        }

        private void ShowYarn()
        {
            Session.Remove("tblfab");
            this.MultiView1.ActiveViewIndex = 1;
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = GetComCode();
            string mlccod = this.ddlMLc.SelectedValue.ToString();
            string orderid = this.ddlOrder.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = FabricYarn.GetTransInfo(comcod, "SP_REPORT_FABYARN", "SHOWFABFORSPCORD", mlccod, orderid, frmdate, todate, "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvYarn.DataSource = null;
                this.gvYarn.DataBind();
                return;
            }

            Session["tblfab"] = HiddenSameData(ds3.Tables[0]);
            this.LoadGrid();




        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;



            string orderno = dt1.Rows[0]["orderid"].ToString();
            string fabno = dt1.Rows[0]["fabno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["orderid"].ToString() == orderno && dt1.Rows[j]["fabno"].ToString() == fabno)
                {
                    orderno = dt1.Rows[j]["orderid"].ToString();
                    fabno = dt1.Rows[j]["fabno"].ToString();
                    dt1.Rows[j]["orderdesc"] = "";
                    dt1.Rows[j]["fabno1"] = "";

                }

                else
                {



                    if (dt1.Rows[j]["orderid"].ToString() == orderno)
                    {
                        dt1.Rows[j]["orderdesc"] = "";
                    }

                    if (dt1.Rows[j]["fabno"].ToString() == fabno)
                    {
                        dt1.Rows[j]["fabno1"] = "";

                    }
                    orderno = dt1.Rows[j]["orderid"].ToString();
                    fabno = dt1.Rows[j]["fabno"].ToString();

                }

            }


            return dt1;


        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblfab"];
            if (dt.Rows.Count == 0)
                return;
            if (this.Request.QueryString["Type"].ToString().Trim() == "Fabric")
            {
                this.gvfab.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvfab.DataSource = dt;
                this.gvfab.DataBind();
                ((Label)this.gvfab.FooterRow.FindControl("lblgvFfQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ?
                0 : dt.Compute("sum(qty)", ""))).ToString("#,##0;(#,##0); ");
            }
            else
            {
                this.gvYarn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.gvYarn.DataSource = dt;
                this.gvYarn.DataBind();
                ((Label)this.gvYarn.FooterRow.FindControl("lblgvFfabQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ?
                0 : dt.Compute("sum(qty)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvYarn.FooterRow.FindControl("lblgvFyQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(yrnqty)", "")) ?
                0 : dt.Compute("sum(yrnqty)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvYarn.FooterRow.FindControl("lblgvFiyQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(iyrnqty)", "")) ?
                0 : dt.Compute("sum(iyrnqty)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvYarn.FooterRow.FindControl("lblgvFdyQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dyrnqty)", "")) ?
                0 : dt.Compute("sum(dyrnqty)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvYarn.FooterRow.FindControl("lblgvFtyQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tyrnqty)", "")) ?
                0 : dt.Compute("sum(tyrnqty)", ""))).ToString("#,##0;(#,##0); ");

            }

        }

        protected void gvfab_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.LoadGrid();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            if (this.Request.QueryString["Type"].ToString().Trim() == "Fabric")
                this.PrintFabric();
            else
                this.PrintYarn();



        }

        private void PrintFabric()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblfab"];
            ReportDocument rptFab = new RMGiRPT.R_15_Pro.rptFabIOrdOrrAll();
            string title = (this.ddlOrder.SelectedValue.ToString() == "000000000000") ? "Fabric Requisition Status( All Order )" : "Fabric Requisition Status( Specific Order )";
            TextObject rpttxtcname = rptFab.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            rpttxtcname.Text = comnam;
            TextObject rpttitle = rptFab.ReportDefinition.ReportObjects["headtitle"] as TextObject;
            rpttitle.Text = title;

            TextObject txtuserinfo = rptFab.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptFab.SetDataSource(dt);
            Session["Report1"] = rptFab;
            lbljavascript.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                  this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintYarn()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblfab"];
            //ReportDocument rptFab = new RMGiRPT.R_15_Pro.rptYrnIOrdOrAll();
            //string title = (this.ddlOrder.SelectedValue.ToString() == "000000000000") ? "Yarn Requisition Status( All Order )" : "Yarn Requisition Status( Specific Order )";
            //TextObject rpttxtcname = rptFab.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            //rpttxtcname.Text = comnam;
            //TextObject rpttitle = rptFab.ReportDefinition.ReportObjects["headtitle"] as TextObject;
            //rpttitle.Text = title;
            //TextObject txtuserinfo = rptFab.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptFab.SetDataSource(dt);
            //Session["Report1"] = rptFab;
            //lbljavascript.Text = "<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                      this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void gvYarn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.LoadGrid();
        }
    }


}