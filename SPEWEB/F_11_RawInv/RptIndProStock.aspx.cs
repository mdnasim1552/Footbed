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
using Microsoft.Reporting.WinForms;
using SPERDLC;

namespace SPEWEB.F_11_RawInv
{
    public partial class RptIndProStock : System.Web.UI.Page
    {
        public double balamt = 0.000000, balqty = 0;
        ProcessAccess CustData = new ProcessAccess();
        Common objComm = new Common();
        static string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                 string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //((Label)this.Master.FindControl("lblTitle")).Text = "";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.SelectView();
                this.CommonButton();
                this.Visibility();
                this.GetCodeBookList();
                this.lbtnFindStore_Click(null, null);
                string type = this.Request.QueryString["Type"].ToString().Trim();
                

                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "ProHis") ? "Products History"
                     : (type == "MatHis") ? "Materials History" : "";
                
                if (Request.QueryString.AllKeys.Contains("sircode") && this.Request.QueryString["sircode"].ToString().Trim() != "")
                {
                    string sircode = this.Request.QueryString["sircode"].ToString().Trim();
                    this.ddlProduct.SelectedValue = sircode;
                    this.txtfrmdate.Text = this.Request.QueryString["date"].ToString().Trim() == "" ?
                        Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy") :
                        Convert.ToDateTime(this.Request.QueryString["date"].ToString().Trim()).ToString("dd-MMM-yyyy");

                    this.txttodate.Text = this.Request.QueryString["dayid"].ToString().Trim() == "" ?
                        System.DateTime.Today.ToString("dd-MMM-yyyy") :
                        Convert.ToDateTime(this.Request.QueryString["dayid"].ToString().Trim()).ToString("dd-MMM-yyyy");
                    this.lbtnOk_Click(null,null);
                }
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private void Visibility()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (type == "ProHis")
            {
                this.pnlcenter.Visible = true;
            }
        }
        protected void GetCodeBookList()
        {
            try
            {
                string Querytype = this.Request.QueryString["Type"];
                string coderange = "04%";

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataSet dsone = CustData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GET_MATERIAL_HEAD", coderange, "", "", "", "", "", "", "");
                Session["tblmatsubhead"] = dsone.Tables[1];
                this.ddlCodeBook.DataTextField = "sircode";
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
            this.ddlGroup.DataTextField = "sircode";
            this.ddlGroup.DataValueField = "sircode1";
            this.ddlGroup.DataSource = dv.ToTable();
            this.ddlGroup.DataBind();
            this.ddlGroup_SelectedIndexChanged(null, null);
        }
        private void CommonButton()
        {


        }
        protected void lbtnFindStore_Click(object sender, EventArgs e)
        {
            this.GetFGStoreName();
        }
        protected void GetFGStoreName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string HeaderCode = (type == "ProHis") ? "37%" : "15%";
            string filter = this.txtSrcStore.Text.Trim() + "%";

            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_FG_INV_02", "GETFGSTORE", HeaderCode, filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlFGInvStore.DataSource = dt1;
            this.ddlFGInvStore.DataTextField = "actdesc1";
            this.ddlFGInvStore.DataValueField = "actcode";
            this.ddlFGInvStore.DataBind();

        }
        private void SelectView()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ProHis":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "MatHis":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

        }
        private void GetProduct()
        {
            string comcod = objComm.GetCompCode();
            string txtSPoduct =  "%";
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string Type = (type == "ProHis") ? "41%" : this.ddlGroup.SelectedValue.ToString() == "040000000000" ? "04%" : this.ddlGroup.SelectedValue.ToString().Substring(0, 7) + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_FG_INV_02", "GETPRODUCT", txtSPoduct, Type, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");
                return;
            }


            this.ddlProduct.DataTextField = "proddesc";
            this.ddlProduct.DataValueField = "prodcode";
            this.ddlProduct.DataSource = ds1.Tables[0];
            this.ddlProduct.DataBind();
            ds1.Dispose();
        }

        protected void lbtnFindProduct_Click(object sender, EventArgs e)
        {
            this.GetProduct();
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }




        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ProHis":
                case "MatHis":

                    this.ProdHistory();
                    break;
            }


        }
        private void ProdHistory()
        {
            try
            {
                string type = this.Request.QueryString["Type"].ToString().Trim();
                string comcod = objComm.GetCompCode();
                string product = (type == "ProHis") ? ASTUtility.Left(this.ddlProduct.SelectedValue.ToString(), 12) : this.ddlProduct.SelectedValue.ToString();

                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string Center = (this.ddlFGInvStore.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlFGInvStore.SelectedValue.ToString();
                string CallType = (type == "ProHis") ? "GETSTOCKDET" : "GETSTOCKDETMAT";
                DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_FG_INV_02", CallType, product, frmdate, todate, Center, "", "", "", "", "");


                if (ds2.Tables[0].Rows.Count == 0 || ds2 == null)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "No Data Found";
                    ((Label)this.Master.FindControl("lblmsg")).Style.Add("Background", "red");

                    this.gvmatstk.DataSource = null;
                    this.gvmatstk.DataBind();
                    this.gvMatHis.DataSource = null;
                    this.gvMatHis.DataBind();
                    return;
                }

                DataView dv = ds2.Tables[0].DefaultView;

                if (CbForPlus.Checked)
                {
                    dv.RowFilter = "qty >= 0";

                    this.BalCalculation(dv.ToTable());
                    Session["tblmatstk"] = dv.ToTable();

                    this.Data_Bind();
                }
                else if (CbForMinus.Checked)
                {
                    dv.RowFilter = "qty < 0";

                    this.BalCalculation(dv.ToTable());
                    Session["tblmatstk"] = dv.ToTable();

                    this.Data_Bind();
                }
                else
                {
                    this.BalCalculation(ds2.Tables[0]);
                    Session["tblmatstk"] = ds2.Tables[0];
                    this.Data_Bind();
                }

            }
            catch (Exception ex)
            { }


        }
        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double amt, qty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                amt = Convert.ToDouble(dt.Rows[i]["amt"]);
                balamt = balamt + (amt);

                qty = Convert.ToDouble(dt.Rows[i]["qty"]);
                balqty = balqty + qty;

                dt.Rows[i]["balamt"] = balamt;
                dt.Rows[i]["balqty"] = balqty;

                // }
            }
            return dt;


        }
        private void Data_Bind()
        {
            try
            {

                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {
                    case "ProHis":
                        this.gvmatstk.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvmatstk.DataSource = (DataTable)Session["tblmatstk"];
                        this.gvmatstk.DataBind();
                        this.FooterCalculation();
                        break;
                    case "MatHis":
                        this.gvMatHis.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvMatHis.DataSource = (DataTable)Session["tblmatstk"];
                        this.gvMatHis.DataBind();
                        this.FooterCalculation();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblmatstk"];
            double qty = 0.00, amt = 0.00, rate = 0.00;
            try
            {
                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {
                    case "ProHis":

                        if (dt.Rows.Count == 0)
                            return;

                        //double qty = 0.00, amt = 0.00, rate = 0.00;
                        qty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty)", "")) ? 0.00 : dt.Compute("Sum(qty)", "")));
                        amt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
                             0.00 : dt.Compute("Sum(amt)", "")));


                        rate = (qty == 0 ? 0.00 : amt / qty);


                        ((Label)this.gvmatstk.FooterRow.FindControl("lblgvFqty")).Text = qty.ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvmatstk.FooterRow.FindControl("lblgvFrate")).Text = rate.ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvmatstk.FooterRow.FindControl("lblgvFamt")).Text = amt.ToString("#,##0.00;(#,##0.00); ");
                        break;
                    case "MatHis":
                        if (dt.Rows.Count == 0)
                            return;


                        qty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(qty)", "")) ? 0.00 : dt.Compute("Sum(qty)", "")));
                        amt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
                             0.00 : dt.Compute("Sum(amt)", "")));


                        rate = (qty == 0 ? 0.00 : amt / qty);


                        ((Label)this.gvMatHis.FooterRow.FindControl("lblgvFqty")).Text = qty.ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvMatHis.FooterRow.FindControl("lblgvFrate")).Text = rate.ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvMatHis.FooterRow.FindControl("lblgvFamt")).Text = amt.ToString("#,##0.00;(#,##0.00); ");
                        break;
                }
            }
            catch (Exception ex)
            { }


        }
        protected void gvMatHis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatHis.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvmatstk_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvmatstk.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string formDate = txtfrmdate.Text;
            string toDate = txttodate.Text;
            string totalDate = "From: " + formDate + "  To:" + toDate;
            string materialHistory = this.Request.QueryString["Type"] == "MatHis" ? "Material: " + ddlProduct.SelectedItem.ToString() : "Product: " + ddlProduct.SelectedItem.ToString();
            //ddlProduct.SelectedItem.ToString()
            string Type = this.Request.QueryString["Type"] == "MatHis" ? "Materials History" : "Product History";

            DataTable dt = (DataTable)Session["tblmatstk"];

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string userinf = ASTUtility.Concat(compname, username, printdate);


            var lst = dt.DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.RptIndProStockModel>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("RD_23_SaM.RptIndProStock", lst, null, null);
            rpt1.SetParameters(new ReportParameter("comname", comnam));
            rpt1.SetParameters(new ReportParameter("title", Type));
            rpt1.SetParameters(new ReportParameter("ddlItem", materialHistory));
            rpt1.SetParameters(new ReportParameter("dateFtoT", totalDate));


            //rpt1.SetParameters(new ReportParameter("ComName", comnam));
            //rpt1.SetParameters(new ReportParameter("ChlnNo", chlnno));
            //rpt1.SetParameters(new ReportParameter("RptTitle", this.ddlToChlnNo.SelectedItem.Text.Trim().Substring(13)));
            //rpt1.SetParameters(new ReportParameter("Date1", chlndate));
            //rpt1.SetParameters(new ReportParameter("userinf", userinf));

            Session["Report1"] = rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }




        protected void gvmatstk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();

                string Memono = ((HyperLink)e.Row.FindControl("lgvvounum")).Text.ToString();
                string Centrid = ((Label)e.Row.FindControl("lblcenterid")).Text.ToString();


                string Custcode = ((Label)e.Row.FindControl("lblcustid")).Text.ToString();
                string SDate = ((Label)e.Row.FindControl("lgvvoudate")).Text.ToString();

                HyperLink InvNo = (HyperLink)e.Row.FindControl("lgvvounum");
                if (ASTUtility.Left(Memono, 3) == "INV")
                {
                    InvNo.Style.Add("color", "blue");
                    InvNo.NavigateUrl = "~/F_23_SaM/LinkInvoice.aspx?Type=InvDetails&comcod=" + comcod + "&Centid=" + Centrid + "&Memo=" + Memono + "&Custid=" + Custcode + "&Date=" + SDate;
                }


            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProduct();

        }

        protected void CbForPlus_CheckedChanged(object sender, EventArgs e)
        {
            if (CbForPlus.Checked)
            {
                CbForMinus.Checked = false;
            }
        }

        protected void CbForMinus_CheckedChanged(object sender, EventArgs e)
        {
            if (CbForMinus.Checked)
            {
                CbForPlus.Checked = false;
            }
        }

        protected void gvMatHis_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}