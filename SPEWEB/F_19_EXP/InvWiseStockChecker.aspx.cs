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
using System.Data.OleDb;
using System.IO;

namespace SPEWEB.F_19_EXP
{
    public partial class InvWiseStockChecker : System.Web.UI.Page
    {
        ProcessAccess ProData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                //    (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text ="Invoice Wise Stock Checker";
                this.CommonButton();
                if (this.Request.QueryString["genno"].Length > 0)
                {
                    this.lbtnOk_Click(null,null);
                }

            }
            

        }

        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
           

            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
          //  ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
           
            this.Data_Bind();
        }

       
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
                   
        }

      

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


       

     
        private void GetInvWiseStock()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string txtFdate = this.txtfromdate.Text.ToString();
            string invno = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = ProData.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_INVOICE_WISE_STOCK_CHECK", txtFdate, invno, "");
            if (ds1 == null)
            {
                return;
            }

            ViewState["tblStockCheck"] = ds1.Tables[0];
            this.Totalinvqty.Text="Total Invoice Qty: "+ Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("Sum(invqty)", "")) ?
               0.00 : ds1.Tables[0].Compute("Sum(invqty)", ""))).ToString("#,##0;(#,##0); ");

           
            this.MissingQty.Text = "Total Deficit Qty: " + Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("Sum(unavqty)", "")) ?
               0.00 : ds1.Tables[0].Compute("Sum(unavqty)", ""))).ToString("#,##0;(#,##0); ");

            this.Data_Bind();
           
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            
         this.GetInvWiseStock();
             
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblStockCheck"];
           
            if (this.CheckBoxStockNill.Checked)
            {
                DataView dv = dt.Copy().DefaultView;
                dv.RowFilter = "invqty>stockqty";
                dt = dv.ToTable();

            }



            this.gvInvStocks.DataSource = dt;
                        this.gvInvStocks.DataBind();
                        if (dt.Rows.Count == 0)
                            return;
                        Session["Report1"] = gvInvStocks;
              ((HyperLink)this.gvInvStocks.HeaderRow.FindControl("hlbtntbCdataExelE")).NavigateUrl =
                "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            
           


        }

        


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



        }

        protected void CheckBoxStockNill_CheckedChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}