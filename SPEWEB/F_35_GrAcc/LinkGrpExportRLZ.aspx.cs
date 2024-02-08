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

namespace SPEWEB.F_35_GrAcc
{
    public partial class LinkGrpExportRLZ : System.Web.UI.Page
    {
        ProcessAccess FabricYarn = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "EXPORT REALIZATION & INCENTIVE";


                this.GetBankMasterLC();

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
            return (this.Request.QueryString["comcod"].ToString());

        }

        private void GetBankMasterLC()
        {
            string comcod = GetComCode();
            string txtsrch = "%" + this.txtBank.Text.Trim() + "%";
            DataSet ds1 = FabricYarn.GetTransInfo(comcod, "SP_ENTRY_REALIZATION", "GETBANKNAME", txtsrch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlBank.DataTextField = "bankname";
            this.ddlBank.DataValueField = "bankcod";
            this.ddlBank.DataSource = ds1.Tables[0];
            this.ddlBank.DataBind();



        }


        protected void lbtnPrint_Click(object sender, EventArgs e) //Print report
        {

            this.RptExpRLZ();

        }
        private void RptExpRLZ()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tbExPlz"];

            //ReportDocument rptmlcordrcost = new RMGiRPT.R_19_EXP.rptExportRLZ();
            //TextObject rpttxtcname = rptmlcordrcost.ReportDefinition.ReportObjects["txtCname"] as TextObject;
            //rpttxtcname.Text = comnam;
            //TextObject rpttxtBank = rptmlcordrcost.ReportDefinition.ReportObjects["txtBankName"] as TextObject;
            //rpttxtBank.Text = this.ddlBank.SelectedItem.Text.ToString();

            //TextObject txtuserinfo = rptmlcordrcost.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptmlcordrcost.SetDataSource(dt);
            //Session["Report1"] = rptmlcordrcost;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tbExPlz");
            string comcod = this.GetComCode();
            string bankcod = (this.ddlBank.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBank.SelectedValue.ToString();
            DataSet ds1 = FabricYarn.GetTransInfo(comcod, "SP_ENTRY_REALIZATION", "EXREZINCENTIVE", bankcod, "", "", "", "", "", "", "", "");


            if (ds1.Tables[0] == null)
            {
                this.gvExRezInc.DataSource = null;
                this.gvExRezInc.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tbExPlz"] = dt1;

            this.LoadGrid();
            this.FooterCalculation();
        }
        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["tbExPlz"];

            this.gvExRezInc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvExRezInc.DataSource = dt;
            this.gvExRezInc.DataBind();



        }
        private DataTable HiddenSameDate(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string mlcmcod = dt1.Rows[0]["mlcmcod"].ToString();
            string mlccod = dt1.Rows[0]["mlccod"].ToString();
            string invno = dt1.Rows[0]["invno"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mlcmcod"].ToString() == mlcmcod)
                {
                    mlcmcod = dt1.Rows[j]["mlcmcod"].ToString();
                    dt1.Rows[j]["mlcmdesc"] = "";
                    dt1.Rows[j]["lcval"] = 0.00;
                }

                else
                {
                    mlcmcod = dt1.Rows[j]["mlcmcod"].ToString();
                }
                //////--------------------------------///////////////////

                if (dt1.Rows[j]["mlccod"].ToString() == mlccod)
                {
                    mlccod = dt1.Rows[j]["mlccod"].ToString();
                    dt1.Rows[j]["mlcdesc"] = "";
                }

                else
                {
                    mlccod = dt1.Rows[j]["mlccod"].ToString();
                }
                //////--------------------------------///////////////////
                if (dt1.Rows[j]["invno"].ToString() == invno)
                {
                    invno = dt1.Rows[j]["invno"].ToString();
                    dt1.Rows[j]["invno"] = "";
                }

                else
                {
                    invno = dt1.Rows[j]["invno"].ToString();
                }
            }

            return dt1;
        }

        private void FooterCalculation()
        {


            DataTable dt = (DataTable)Session["tbExPlz"];
            if (dt.Rows.Count == 0)
                return;

            //double shPlnQty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shipplnqty)", "")) ?
            //                   0 : dt.Compute("sum(shipplnqty)", "")));
            //double proQty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(propqty)", "")) ?
            //                0 : dt.Compute("sum(propqty)", "")));
            //double shiQty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(shipqty)", "")) ?
            //                0 : dt.Compute("sum(shipqty)", "")));
            //double proPar = ((proQty * 100) / shPlnQty);
            //double shiPar = ((shiQty * 100) / shPlnQty);

            //((Label)this.gvExRezInc.FooterRow.FindControl("lgvFShiPlqty")).Text = shPlnQty.ToString("#,##0;(#,##0); ");
            //((Label)this.gvExRezInc.FooterRow.FindControl("lgvFProqty")).Text = proQty.ToString("#,##0;(#,##0); ");
            //((Label)this.gvExRezInc.FooterRow.FindControl("lgvFShiQty")).Text = shiQty.ToString("#,##0;(#,##0); ");

            //((Label)this.gvExRezInc.FooterRow.FindControl("lgvFShiPlPar")).Text = "100%";
            //((Label)this.gvExRezInc.FooterRow.FindControl("lgvFProPar")).Text = proPar.ToString("#,##0.00;(#,##0.00); ") + "%";
            //((Label)this.gvExRezInc.FooterRow.FindControl("lgvFShiPar")).Text = shiPar.ToString("#,##0.00;(#,##0.00); ") + "%";


        }
        protected void imgbtnFindBank_Click(object sender, EventArgs e)
        {
            this.GetBankMasterLC();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvExRezInc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            gvExRezInc.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tbExPlz"];
            int TblRowIndex;
            for (int i = 0; i < this.gvExRezInc.Rows.Count; i++)
            {
                double dgvRezval = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvExRezInc.Rows[i].FindControl("txtgvRezVal")).Text.Trim()));
                string Billdat = (((TextBox)this.gvExRezInc.Rows[i].FindControl("txtgvdBillDat")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvExRezInc.Rows[i].FindControl("txtgvdBillDat")).Text.Trim();
                string IncDat = (((TextBox)this.gvExRezInc.Rows[i].FindControl("txtgvdIncDat")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvExRezInc.Rows[i].FindControl("txtgvdIncDat")).Text.Trim();
                double dgvIncVal = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvExRezInc.Rows[i].FindControl("txtgvIncVal")).Text.Trim()));
                double dgvRecVal = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvExRezInc.Rows[i].FindControl("txtgvRecVal")).Text.Trim()));

                double BalVal = (dgvIncVal - dgvRecVal);

                ((TextBox)this.gvExRezInc.Rows[i].FindControl("txtgvRezVal")).Text = dgvRezval.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvExRezInc.Rows[i].FindControl("txtgvIncVal")).Text = dgvIncVal.ToString("#,##0.00;(#,##0.00); ");
                ((TextBox)this.gvExRezInc.Rows[i].FindControl("txtgvRecVal")).Text = dgvRecVal.ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvExRezInc.Rows[i].FindControl("lblgvBalAmt")).Text = BalVal.ToString("#,##0.00;(#,##0.00); ");


                TblRowIndex = (gvExRezInc.PageIndex) * gvExRezInc.PageSize + i;

                dt.Rows[TblRowIndex]["rlzval"] = dgvRezval;
                dt.Rows[TblRowIndex]["billdat"] = Billdat;
                dt.Rows[TblRowIndex]["incentivedat"] = IncDat;
                dt.Rows[TblRowIndex]["incentiveval"] = dgvIncVal;
                dt.Rows[TblRowIndex]["receival"] = dgvRecVal;
                dt.Rows[TblRowIndex]["balval"] = BalVal;
            }
            Session["tbExPlz"] = dt;

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveValue();
                string comcod = GetComCode();
                DataTable dt = (DataTable)Session["tbExPlz"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string mlccod = dt.Rows[i]["mlccod"].ToString();
                    string invno = dt.Rows[i]["invno"].ToString();
                    double RlzVal = Convert.ToDouble(dt.Rows[i]["rlzval"].ToString());
                    string billdat = Convert.ToDateTime(dt.Rows[i]["billdat"]).ToString("dd-MMM-yyyy");
                    string IncDat = Convert.ToDateTime(dt.Rows[i]["incentivedat"]).ToString("dd-MMM-yyyy");
                    double IncVal = Convert.ToDouble(dt.Rows[i]["incentiveval"].ToString());
                    double RecVal = Convert.ToDouble(dt.Rows[i]["receival"].ToString());
                    if (RlzVal > 0)
                    {
                        DataSet ds1 = FabricYarn.GetTransInfo(comcod, "SP_ENTRY_REALIZATION", "INSERTUPDATEXRLZ", mlccod, invno, RlzVal.ToString(), IncDat,
                            IncVal.ToString(), RecVal.ToString(), billdat, "", "");
                        if (ds1 == null)
                        {
                            this.lblmsg.Text = "Error:" + FabricYarn.ErrorObject["Msg"].ToString();
                            this.lblmsg.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }
                }


                this.lblmsg.Text = "Record Update Successfully.";

            }
            catch (Exception ex)
            {
                this.lblmsg.Text = "Error:" + ex.Message;
            }
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }
        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblmsg.Text = "";
        }
    }

}