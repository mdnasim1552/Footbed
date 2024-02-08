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


namespace SPEWEB.F_35_GrAcc
{
    public partial class LinkRptGrpIncomeSt : System.Web.UI.Page
    {
        ProcessAccess feaData = new ProcessAccess();
        public static double ToCost, OrdrVal, toqty, ToCostPer, ToCostPerM, totalcmPer;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "FORCASTED INCOME STATEMENT";

                this.ProjectName();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComCode()
        {
            return (this.Request.QueryString["comcod"].ToString());

        }
        private void ProjectName()
        {
            string comcod = this.GetComCode();
            string Filter1 = this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GETORDRLIST", Filter1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "infdesc";
            this.ddlProjectName.DataValueField = "infcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.ProjectName();



        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {
            this.lblHeadStyle.Visible = true;
            this.lblHeadCost.Visible = true;
            //this.lblBal.Visible = true;
            this.ShowValue();


        }
        private void ShowValue()
        {
            Session.Remove("tbllcana");
            string comcod = this.GetComCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = feaData.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "ORDRINFORMATION", pactcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFeaPrj.DataSource = null;
                this.gvFeaPrj.DataBind();
                this.gvFeaPrjC.DataSource = null;
                this.gvFeaPrjC.DataBind();
                return;
            }
            Session["tbllcana"] = ds2.Tables[0];
            Session["tblCtRate"] = ds2;
            this.lblSymbol.Text = "Currency: " + ds2.Tables[1].Rows[0]["curncy"].ToString() + ",  Rate: " + Convert.ToDouble(ds2.Tables[2].Rows[0]["rate"]).ToString("#,##0.00;(#,##0.00); ") + " Taka";
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbllcana"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "infcod like('03%')";
            this.gvFeaPrj.DataSource = dv.ToTable();
            this.gvFeaPrj.DataBind();
            dv = dt.DefaultView;
            dv.RowFilter = "infcod not like ('03%')";
            this.gvFeaPrjC.DataSource = dv.ToTable();
            this.gvFeaPrjC.DataBind();
            this.FooterCal();



        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tbllcana"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "infcod like('03%')";
            DataTable dts = dv.ToTable();

            dv.RowFilter = "infcod  not like('03%')";
            DataTable dtc = dv.ToTable();
            ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFQty")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(qty)", "")) ?
               0.00 : dts.Compute("Sum(qty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFPerM")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(percntge)", "")) ?
               0.00 : dts.Compute("Sum(percntge)", ""))).ToString("#,##0;(#,##0); ");

            toqty = Convert.ToDouble("0" + ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFQty")).Text);

            ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dts.Compute("Sum(amt)", "")) ?
              0.00 : dts.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");

            OrdrVal = Convert.ToDouble("0" + ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFAmt")).Text);

            //((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFQtyc")).Text = Convert.ToDouble((Convert.IsDBNull(dtc.Compute("Sum(qty)", "")) ?
            //  0.00 : dtc.Compute("Sum(qty)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc")).Text = Convert.ToDouble((Convert.IsDBNull(dtc.Compute("Sum(amt)", "")) ?
              0.00 : dtc.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFPer")).Text = Convert.ToDouble((Convert.IsDBNull(dtc.Compute("Sum(percntge)", "")) ?
              0.00 : dtc.Compute("Sum(percntge)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ToCost = Convert.ToDouble("0" + ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFAmtc")).Text);
            ToCostPer = Convert.ToDouble("0" + ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFPer")).Text);
            ToCostPerM = Convert.ToDouble("0" + ((Label)this.gvFeaPrj.FooterRow.FindControl("lgvFPerM")).Text);
            ////
            totalcmPer = (ToCostPerM - ToCostPer);
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFTotalCMPer")).Text = totalcmPer.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("Label1")).Text = "0.00";
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("Label2")).Text = "0.00";

            /////
            double totalcm = (OrdrVal - ToCost);
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFTotalCM")).Text = totalcm.ToString("#,##0;(#,##0); ");

            double cmdz = ((OrdrVal - ToCost) / toqty);
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFCMDZ")).Text = cmdz.ToString("#,##0.00;(#,##0.00); ");

            double cmpcs = (((OrdrVal - ToCost) / toqty) / 12);
            ((Label)this.gvFeaPrjC.FooterRow.FindControl("lgvFCMPCS")).Text = cmpcs.ToString("#,##0.00;(#,##0.00); ");

            //this.lblBalVal.Text = (OrdrVal - ToCost).ToString("#,##0;(#,##0); ");
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string LCname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            //// RptLCAnalysis rpcp = new RptLCAnalysis();
            //DataSet ds = (DataSet)Session["tblCtRate"];

            //ReportDocument rpcp = new RMGiRPT.R_01_Mer.RptLCAnalysis();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comname;
            //TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //txtPrjName.Text = "Description: " + LCname;
            ////TextObject txtCurrency = rpcp.ReportDefinition.ReportObjects["txtCurrency"] as TextObject;
            ////txtCurrency.Text = lblSymbol.Text;
            //TextObject rpttxttoCost = rpcp.ReportDefinition.ReportObjects["txttocsot"] as TextObject;
            //rpttxttoCost.Text = ToCost.ToString("#,##0.00;(#,##0.00); ");
            //TextObject txtBalance = rpcp.ReportDefinition.ReportObjects["txtBalance"] as TextObject;
            //txtBalance.Text = (OrdrVal - ToCost).ToString("#,##0.00;(#,##0.00); ");
            //TextObject rpttxtcmperdz = rpcp.ReportDefinition.ReportObjects["txtcmperdz"] as TextObject;
            //rpttxtcmperdz.Text = ((OrdrVal - ToCost) / toqty).ToString("#,##0.00;(#,##0.00); ");
            //TextObject txtcmperpcs = rpcp.ReportDefinition.ReportObjects["txtcmperpcs"] as TextObject;
            //txtcmperpcs.Text = (((OrdrVal - ToCost) / toqty) / 12).ToString("#,##0.00;(#,##0.00); ");

            //TextObject rpttxttoCostPer = rpcp.ReportDefinition.ReportObjects["txtGper"] as TextObject;
            //rpttxttoCostPer.Text = ToCostPer.ToString("#,##0.00;(#,##0.00); ") + "%";
            //TextObject txtBalancePer = rpcp.ReportDefinition.ReportObjects["txtBper"] as TextObject;
            //txtBalancePer.Text = totalcmPer.ToString("#,##0.00;(#,##0.00); ") + "%";
            //TextObject rpttxtRate = rpcp.ReportDefinition.ReportObjects["txtCurrency"] as TextObject;
            //rpttxtRate.Text = "1 " + ds.Tables[1].Rows[0]["curncy"].ToString() + " = " + Convert.ToDouble(ds.Tables[2].Rows[0]["rate"]).ToString("#,##0.00;(#,##0.00); ") + " Taka";

            //TextObject txttqty = rpcp.ReportDefinition.ReportObjects["txttqty"] as TextObject;
            //txttqty.Text = toqty.ToString("#,##0;(#,##0); ");

            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rpcp.SetDataSource((DataTable)Session["tbllcana"]);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}