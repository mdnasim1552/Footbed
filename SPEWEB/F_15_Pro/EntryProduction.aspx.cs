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
    public partial class EntryProduction : System.Web.UI.Page
    {

        ProcessAccess Production = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Production Information";
                this.LoadDataLC_Order();
                this.GetLine();
                CommonButton();
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrintReport_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);

            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void CommonButton()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ////((LinkButton)this.Master.FindControl("btnClose")).Visible = false;

            //  ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text= "Calculation";


        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;

        }


        protected void lbtnPrevList_Click(object sender, EventArgs e)
        {
            string comcod = GetComCode();
            string date1 = this.txtDate.Text.Substring(0, 11);
            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "ActualProductionIdList", "", date1, "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.DDLPrevIDList.DataTextField = "IdDatNo";
            this.DDLPrevIDList.DataValueField = "prodno";
            this.DDLPrevIDList.DataSource = ds1.Tables[0];
            this.DDLPrevIDList.DataBind();
            if (this.DDLMasterLC.Items.Count == 0)
                this.LoadDataLC_Order();
        }
        private void LoadDataLC_Order()
        {

            string comcod = GetComCode();
            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "EXISTLCORDER", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.DDLMasterLC.DataTextField = "ACTDESC1";
            this.DDLMasterLC.DataValueField = "ACTCODE";
            this.DDLMasterLC.DataSource = ds1.Tables[0];
            this.DDLMasterLC.DataBind();
            ViewState["TblOrder"] = ds1.Tables[1];
            DDLMasterLC_SelectedIndexChanged(null, null);
        }
        private void GetLine()
        {
            string comcod = GetComCode();
            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GETLINE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlLine.DataTextField = "linedesc";
            this.ddlLine.DataValueField = "linecode";
            this.ddlLine.DataSource = ds1.Tables[0];
            this.ddlLine.DataBind();
            ds1.Dispose();



        }

        protected void lbtnProOk_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.lbtnProOk.Text == "Ok")
                {

                    this.lbtnProOk.Text = "New";
                    this.txtPrevSearch.Visible = false;
                    this.lbtnPrevList.Visible = false;
                    this.DDLPrevIDList.Visible = false;
                    this.lblline.Text = this.ddlLine.SelectedItem.Text;
                    this.ddlLine.Visible = false;
                    this.lblline.Visible = true;


                    if (this.DDLPrevIDList.Items.Count > 0)
                    {
                        this.lblProdID.Text = this.DDLPrevIDList.SelectedValue.ToString().Trim();
                        show_ProductionInformation(true);
                        this.lblMLC.Visible = true;
                        this.lblOrder.Visible = true;
                        this.DDLMasterLC.Visible = false;
                        this.DDLOrder.Visible = false;


                        this.ProBal.Visible = false;
                        this.lblPage.Visible = false;
                        this.lblbalinformation.Visible = false;
                        return;

                    }
                    this.lblPage.Visible = true;
                    this.ProBal.Visible = true;
                    this.lblbalinformation.Visible = true;

                    //show_ProductionInformation(true);
                    string comcod = GetComCode();
                    DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "LastProdNo", "", "", "", "", "", "", "", "", "");
                    if (ds1 == null)
                        return;
                    this.lblProdID.Text = ds1.Tables[0].Rows[0]["NewProdNo"].ToString();
                    this.lblMLC.Text = this.DDLMasterLC.SelectedItem.ToString().Trim();
                    this.lblOrder.Text = this.DDLOrder.SelectedItem.ToString().Trim();
                    show_ProductionInformation(true);
                    this.lblMLC.Visible = true;
                    this.lblOrder.Visible = true;
                    this.DDLMasterLC.Visible = false;
                    this.DDLOrder.Visible = false;
                    this.ShowBalanceQty();
                }
                else
                {
                    this.lblMessage.Text = "";
                    this.DDLPrevIDList.Items.Clear();
                    this.lbtnProOk.Text = "Ok";
                    this.lblProdID.Text = "";
                    this.txtRefNo.Text = "";
                    this.txtPrevSearch.Visible = true;
                    this.lbtnPrevList.Visible = true;
                    this.DDLPrevIDList.Visible = true;
                    this.DDLMasterLC.Visible = true;
                    this.DDLOrder.Visible = true;
                    this.lblMLC.Visible = false;
                    this.lblOrder.Visible = false;
                    this.ddlLine.Visible = true;
                    this.lblline.Visible = false;
                    this.lblPage.Visible = false;
                    this.lblbalinformation.Visible = false;

                    gv1.DataSource = null;
                    gv1.DataBind();
                    gvBalPro.DataSource = null;
                    gvBalPro.DataBind();
                    this.gvBalpro02.DataSource = null;
                    this.gvBalpro02.DataBind();

                }
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = "Error" + ex.Message;
            }
        }


        private void ShowBalanceQty()
        {


            string comcod = GetComCode();
            string orderid = this.DDLOrder.SelectedValue.ToString().Trim().Substring(12);
            string date = this.txtDate.Text;
            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "SHOWBALANCEQTY", orderid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBalpro02.DataSource = null;
                this.gvBalpro02.DataBind();
                return;

            }


            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    this.gvBalpro02.Columns[i + 4].HeaderText = "";
                    this.gvBalpro02.Columns[i + 4].Visible = false;
                }

                for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
                {
                    string ColHeadDesc = ds1.Tables[1].Rows[i]["GenData"].ToString();
                    this.gvBalpro02.Columns[i + 4].HeaderText = ColHeadDesc;
                    this.gvBalpro02.Columns[i + 4].Visible = true;
                }
                this.gvBalpro02.DataSource = ds1.Tables[0];
                this.gvBalpro02.DataBind();


                ((Label)this.gvBalpro02.FooterRow.FindControl("lblgvFProqty1")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(f720100101001)", "")) ?
                                    0 : ds1.Tables[0].Compute("sum(f720100101001)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvBalpro02.FooterRow.FindControl("lblgvFProqty2")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(f720100101002)", "")) ?
                                        0 : ds1.Tables[0].Compute("sum(f720100101002)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvBalpro02.FooterRow.FindControl("lblgvFProqty3")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(f720100101003)", "")) ?
                                        0 : ds1.Tables[0].Compute("sum(f720100101003)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvBalpro02.FooterRow.FindControl("lblgvFProqty4")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(f720100101004)", "")) ?
                                        0 : ds1.Tables[0].Compute("sum(f720100101004)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvBalpro02.FooterRow.FindControl("lblgvFProqty5")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(f720100101005)", "")) ?
                                        0 : ds1.Tables[0].Compute("sum(f720100101005)", ""))).ToString("#,##0;(#,##0); ");


                ((Label)this.gvBalpro02.FooterRow.FindControl("lblgvFbalqty")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(balqty)", "")) ?
                                        0 : ds1.Tables[0].Compute("sum(balqty)", ""))).ToString("#,##0;(#,##0); ");
            }







        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {

            //this.PanelPrev.Visible = false;



            //this.lblProdID.Text = this.DDLPrevIDList.SelectedValue.ToString().Trim();


            ////this.lbtnFUpdate.Enabled = this.chkEdit.Checked;

            //show_ProductionInformation(true);

        }
        protected void DDLMasterLC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccod = this.DDLMasterLC.SelectedValue.ToString().Trim();
            DataView dv1 = ((DataTable)ViewState["TblOrder"]).DefaultView;
            dv1.RowFilter = "mlccod = '" + mlccod + "'";
            this.DDLOrder.DataTextField = "GenData";
            this.DDLOrder.DataValueField = "gencod1";
            this.DDLOrder.DataSource = dv1;
            this.DDLOrder.DataBind();
        }


        private void show_ProductionInformation(bool mEntryMode)
        {
            if (this.DDLMasterLC.SelectedValue == null || this.DDLOrder.SelectedValue == null)
            {
                return;
            }
            string comcod = GetComCode();
            string mlccod = this.DDLMasterLC.SelectedValue.ToString().Trim();
            string orderid = this.DDLOrder.SelectedValue.ToString().Trim().Substring(12);
            string Prodid = this.lblProdID.Text.Trim();
            string date = this.txtDate.Text;

            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GetActualProductionDetails", mlccod, orderid, Prodid, date, "", "", "", "", "");

            ViewState["Size"] = (ds1.Tables[1]);
            ViewState["ProBalance"] = HiddenSameData(ds1.Tables[3]);
            if (ds1 == null)
                return;

            if (ds1.Tables[2].Rows.Count > 0)
            {
                this.DDLMasterLC.SelectedValue = ds1.Tables[2].Rows[0]["mlccod"].ToString();
                DDLMasterLC_SelectedIndexChanged(null, null);
                this.DDLOrder.SelectedValue = ds1.Tables[2].Rows[0]["mlccod"].ToString() + ds1.Tables[2].Rows[0]["ordrid"].ToString();
                this.txtDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["producdat"]).ToString("dd-MMM-yyyy");
                this.txtRefNo.Text = ds1.Tables[2].Rows[0]["prodno2"].ToString();
                this.lblMLC.Text = this.DDLMasterLC.SelectedItem.ToString();
                this.lblOrder.Text = this.DDLOrder.SelectedItem.ToString();
                this.ddlLine.SelectedValue = ds1.Tables[2].Rows[0]["linecode"].ToString();
                this.lblline.Text = this.ddlLine.SelectedItem.Text;


            }
            for (int i = 0; i < 15; i++)
            {
                this.gv1.Columns[i + 5].HeaderText = "";
                this.gv1.Columns[i + 5].Visible = false;
            }

            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {
                string ColHeadDesc = ds1.Tables[1].Rows[i]["GenData"].ToString();
                this.gv1.Columns[i + 5].HeaderText = ColHeadDesc;
                this.gv1.Columns[i + 5].Visible = true;
            }



            string mStyleID2 = "xxxxxxx";
            string mColorID2 = "xxxxxxx";
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                if (ds1.Tables[0].Rows[i]["styleid"].ToString() == mStyleID2)
                    ds1.Tables[0].Rows[i]["StyleDes"] = " >> ";
                if (ds1.Tables[0].Rows[i]["styleid"].ToString() +
                    ds1.Tables[0].Rows[i]["colorid"].ToString() == mStyleID2 + mColorID2)
                    ds1.Tables[0].Rows[i]["Desc1"] = " >";

                mStyleID2 = ds1.Tables[0].Rows[i]["styleid"].ToString();
                mColorID2 = ds1.Tables[0].Rows[i]["colorid"].ToString();
            }
            DataTable tbl01 = ds1.Tables[0];
            DataView ddv1 = tbl01.DefaultView;
            ddv1.Sort = "styleid, colorid";
            ViewState["tblProd"] = ddv1.ToTable();
            this.gv1.EditIndex = -1;
            this.gv1_CalculateFootertotal(gv1, ((DataTable)ViewState["tblProd"]));
            this.gv1.DataSource = ((DataTable)ViewState["tblProd"]);
            this.gv1.DataBind();
            this.Data_Bind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string styleid = dt1.Rows[0]["styleid"].ToString();
            string colorid = dt1.Rows[0]["colorid"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["styleid"].ToString() == styleid && dt1.Rows[j]["colorid"].ToString() == colorid)
                {
                    styleid = dt1.Rows[j]["styleid"].ToString();
                    colorid = dt1.Rows[j]["colorid"].ToString();
                    dt1.Rows[j]["styledesc"] = "";
                    dt1.Rows[j]["colordesc"] = "";


                }

                else
                {
                    if (dt1.Rows[j]["styleid"].ToString() == styleid)
                        dt1.Rows[j]["styledesc"] = "";
                    if (dt1.Rows[j]["colorid"].ToString() == colorid)
                        dt1.Rows[j]["colordesc"] = "";

                    styleid = dt1.Rows[j]["styleid"].ToString();
                    colorid = dt1.Rows[j]["colorid"].ToString();

                }

            }


            return dt1;


        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["ProBalance"];
            DataTable dt1 = (DataTable)ViewState["Size"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    this.gvBalPro.Columns[i + 4].HeaderText = "";
                    this.gvBalPro.Columns[i + 4].Visible = false;
                }

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string ColHeadDesc = dt1.Rows[i]["GenData"].ToString();
                    this.gvBalPro.Columns[i + 4].HeaderText = ColHeadDesc;
                    this.gvBalPro.Columns[i + 4].Visible = true;
                }
                this.gvBalPro.DataSource = dt;
                this.gvBalPro.DataBind();
                this.FooterCal();
            }
        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)ViewState["ProBalance"];

            ((Label)this.gvBalPro.FooterRow.FindControl("lblgvFProqty1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(f720100101001)", "")) ?
                                    0 : dt.Compute("sum(f720100101001)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvBalPro.FooterRow.FindControl("lblgvFProqty2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(f720100101002)", "")) ?
                                    0 : dt.Compute("sum(f720100101002)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvBalPro.FooterRow.FindControl("lblgvFProqty3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(f720100101003)", "")) ?
                                    0 : dt.Compute("sum(f720100101003)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvBalPro.FooterRow.FindControl("lblgvFProqty4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(f720100101004)", "")) ?
                                    0 : dt.Compute("sum(f720100101004)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvBalPro.FooterRow.FindControl("lblgvFProqty5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(f720100101005)", "")) ?
                                    0 : dt.Compute("sum(f720100101005)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvBalPro.FooterRow.FindControl("lblgvFTotal1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Total1)", "")) ?
                                    0 : dt.Compute("sum(Total1)", ""))).ToString("#,##0;(#,##0); ");



        }
        protected void gv1_CalculateFootertotal(GridView gv1a, DataTable tbl01a)
        {
            gv1a.Columns[5].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201001)", "")) ?
                0 : tbl01a.Compute("sum(F7201001)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[6].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201002)", "")) ?
                0 : tbl01a.Compute("sum(F7201002)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[7].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201003)", "")) ?
                0 : tbl01a.Compute("sum(F7201003)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[8].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201004)", "")) ?
                0 : tbl01a.Compute("sum(F7201004)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[9].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201005)", "")) ?
                0 : tbl01a.Compute("sum(F7201005)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[10].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201006)", "")) ?
                0 : tbl01a.Compute("sum(F7201006)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[11].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201007)", "")) ?
                0 : tbl01a.Compute("sum(F7201007)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[12].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201008)", "")) ?
                0 : tbl01a.Compute("sum(F7201008)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[13].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201009)", "")) ?
                0 : tbl01a.Compute("sum(F7201009)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[14].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201010)", "")) ?
                0 : tbl01a.Compute("sum(F7201010)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[15].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201011)", "")) ?
                0 : tbl01a.Compute("sum(F7201011)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[16].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201012)", "")) ?
                0 : tbl01a.Compute("sum(F7201012)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[17].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201013)", "")) ?
                0 : tbl01a.Compute("sum(F7201013)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[18].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201014)", "")) ?
                0 : tbl01a.Compute("sum(F7201014)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[19].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(F7201015)", "")) ?
                0 : tbl01a.Compute("sum(F7201015)", ""))).ToString("#,##0;(#,##0); ");

            gv1a.Columns[20].FooterText = Convert.ToDouble((Convert.IsDBNull(tbl01a.Compute("sum(Total1)", "")) ?
                0 : tbl01a.Compute("sum(Total1)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void lbtnPrintReport_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument _reportDoc = new RMGiRPT.R_15_Pro.rptProductionInfo();

            TextObject txtCompName = _reportDoc.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = comnam;

            TextObject txtCompAdd = _reportDoc.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            txtCompAdd.Text = comadd;

            TextObject txtheadings = _reportDoc.ReportDefinition.ReportObjects["txtheadings"] as TextObject;
            txtheadings.Text = "PRODUCTION INFORMATION DETAILS";

            TextObject txtmlcdesc = _reportDoc.ReportDefinition.ReportObjects["txtmlcdesc"] as TextObject;
            txtmlcdesc.Text = this.DDLMasterLC.SelectedItem.Text.Substring(10);

            TextObject txtOrdrDesc = _reportDoc.ReportDefinition.ReportObjects["txtOrdrDesc"] as TextObject;
            txtOrdrDesc.Text = this.DDLOrder.SelectedItem.Text.Substring(10);

            TextObject txtrefno = _reportDoc.ReportDefinition.ReportObjects["txtrefno"] as TextObject;
            txtrefno.Text = this.txtRefNo.Text;

            TextObject txtProdno = _reportDoc.ReportDefinition.ReportObjects["txtProdno"] as TextObject;
            txtProdno.Text = this.lblProdID.Text;

            TextObject txtproddat = _reportDoc.ReportDefinition.ReportObjects["txtproddat"] as TextObject;
            txtproddat.Text = this.txtDate.Text.Substring(0, 11);// this.dtpProducDat.Value.ToString("dd-MMM-yyyy");

            TextObject txtF7201001 = _reportDoc.ReportDefinition.ReportObjects["txtF7201001"] as TextObject;
            txtF7201001.Text = (this.gv1.Columns[6].Visible ? this.gv1.Columns[6].HeaderText.ToString() : "");

            TextObject txtF7201002 = _reportDoc.ReportDefinition.ReportObjects["txtF7201002"] as TextObject;
            txtF7201002.Text = (this.gv1.Columns[7].Visible ? this.gv1.Columns[7].HeaderText.ToString() : "");

            TextObject txtF7201003 = _reportDoc.ReportDefinition.ReportObjects["txtF7201003"] as TextObject;
            txtF7201003.Text = (this.gv1.Columns[8].Visible ? this.gv1.Columns[8].HeaderText.ToString() : "");

            TextObject txtF7201004 = _reportDoc.ReportDefinition.ReportObjects["txtF7201004"] as TextObject;
            txtF7201004.Text = (this.gv1.Columns[9].Visible ? this.gv1.Columns[9].HeaderText.ToString() : "");

            TextObject txtF7201005 = _reportDoc.ReportDefinition.ReportObjects["txtF7201005"] as TextObject;
            txtF7201005.Text = (this.gv1.Columns[10].Visible ? this.gv1.Columns[10].HeaderText.ToString() : "");

            TextObject txtF7201006 = _reportDoc.ReportDefinition.ReportObjects["txtF7201006"] as TextObject;
            txtF7201006.Text = (this.gv1.Columns[11].Visible ? this.gv1.Columns[11].HeaderText.ToString() : "");

            TextObject txtF7201007 = _reportDoc.ReportDefinition.ReportObjects["txtF7201007"] as TextObject;
            txtF7201007.Text = (this.gv1.Columns[12].Visible ? this.gv1.Columns[12].HeaderText.ToString() : "");

            TextObject txtF7201008 = _reportDoc.ReportDefinition.ReportObjects["txtF7201008"] as TextObject;
            txtF7201008.Text = (this.gv1.Columns[13].Visible ? this.gv1.Columns[13].HeaderText.ToString() : "");

            TextObject txtF7201009 = _reportDoc.ReportDefinition.ReportObjects["txtF7201009"] as TextObject;
            txtF7201009.Text = (this.gv1.Columns[14].Visible ? this.gv1.Columns[14].HeaderText.ToString() : "");
            TextObject txtuserinfo = _reportDoc.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            DataView dvRptData = ((DataTable)ViewState["tblProd"]).DefaultView;
            dvRptData.RowFilter = "Total1 > 0 or colorid='000000000000'";
            _reportDoc.SetDataSource(dvRptData);

            Session["Report1"] = _reportDoc;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void chkEdit_CheckedChanged(object sender, EventArgs e)
        {

        }


        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {

            DataTable tbl01 = ((DataTable)ViewState["tblProd"]);
            for (int i = 0; i < this.gv1.Rows.Count; i++)
            {
                double mF7201001 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201001")).Text.Trim());
                double mF7201002 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201002")).Text.Trim());
                double mF7201003 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201003")).Text.Trim());
                double mF7201004 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201004")).Text.Trim());
                double mF7201005 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201005")).Text.Trim());
                double mF7201006 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201006")).Text.Trim());
                double mF7201007 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201007")).Text.Trim());
                double mF7201008 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201008")).Text.Trim());
                double mF7201009 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201009")).Text.Trim());
                double mF7201010 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201010")).Text.Trim());
                double mF7201011 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201011")).Text.Trim());
                double mF7201012 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201012")).Text.Trim());
                double mF7201013 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201013")).Text.Trim());
                double mF7201014 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201014")).Text.Trim());
                double mF7201015 = Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7201015")).Text.Trim());


                tbl01.Rows[i]["F7201001"] = mF7201001;
                tbl01.Rows[i]["F7201002"] = mF7201002;
                tbl01.Rows[i]["F7201003"] = mF7201003;
                tbl01.Rows[i]["F7201004"] = mF7201004;
                tbl01.Rows[i]["F7201005"] = mF7201005;
                tbl01.Rows[i]["F7201006"] = mF7201006;
                tbl01.Rows[i]["F7201007"] = mF7201007;
                tbl01.Rows[i]["F7201008"] = mF7201008;
                tbl01.Rows[i]["F7201009"] = mF7201009;
                tbl01.Rows[i]["F7201010"] = mF7201010;
                tbl01.Rows[i]["F7201011"] = mF7201011;
                tbl01.Rows[i]["F7201012"] = mF7201012;
                tbl01.Rows[i]["F7201013"] = mF7201013;
                tbl01.Rows[i]["F7201014"] = mF7201014;
                tbl01.Rows[i]["F7201015"] = mF7201015;
                tbl01.Rows[i]["Total1"] = mF7201001 + mF7201002 + mF7201003 + mF7201004 + mF7201005 +
                                                    mF7201006 + mF7201007 + mF7201008 + mF7201009 + mF7201010 +
                                                    mF7201011 + mF7201012 + mF7201013 + mF7201014 + mF7201015;
            }
            this.gv1_CalculateFootertotal(gv1, tbl01);
            DataView ddv1 = tbl01.DefaultView;
            ddv1.Sort = "styleid, colorid";
            ViewState["tblProd"] = ddv1.ToTable();
            this.gv1.EditIndex = -1;
            this.gv1.DataSource = ((DataTable)ViewState["tblProd"]);
            this.gv1.DataBind();

        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mLccod = this.DDLOrder.SelectedValue.ToString().Trim().Substring(12);
            string linecode = this.ddlLine.SelectedValue.ToString();
            string mProdid = this.lblProdID.Text.Trim();
            string mProdid2 = this.txtRefNo.Text.Trim();
            string mProddat = this.txtDate.Text.Trim().Substring(0, 11);
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string entrydat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            string comcod = GetComCode();
            bool result = Production.UpdateTransInfo(comcod, "SP_ENTRY_PRODUCTION", "SaveActualProductionInfo", mLccod, linecode, mProdid, mProdid2,
                mProddat, userid, Terminal, Sessionid, entrydat, "", "", "", "", "", "");


            if (!result)
            {
                this.lblMessage.Text = Production.ErrorObject["Msg"].ToString();
                return;
            }

            DataTable tbl01 = ((DataTable)ViewState["tblProd"]);
            for (int i = 0; i < tbl01.Rows.Count; i++)
            {
                string mStyleID = tbl01.Rows[i]["styleid"].ToString();
                string mColorID = tbl01.Rows[i]["colorid"].ToString();
                string mF7201001 = tbl01.Rows[i]["F7201001"].ToString();
                string mF7201002 = tbl01.Rows[i]["F7201002"].ToString();
                string mF7201003 = tbl01.Rows[i]["F7201003"].ToString();
                string mF7201004 = tbl01.Rows[i]["F7201004"].ToString();
                string mF7201005 = tbl01.Rows[i]["F7201005"].ToString();
                string mF7201006 = tbl01.Rows[i]["F7201006"].ToString();
                string mF7201007 = tbl01.Rows[i]["F7201007"].ToString();
                string mF7201008 = tbl01.Rows[i]["F7201008"].ToString();
                string mF7201009 = tbl01.Rows[i]["F7201009"].ToString();
                string mF7201010 = tbl01.Rows[i]["F7201010"].ToString();
                string mF7201011 = tbl01.Rows[i]["F7201011"].ToString();
                string mF7201012 = tbl01.Rows[i]["F7201012"].ToString();
                string mF7201013 = tbl01.Rows[i]["F7201013"].ToString();
                string mF7201014 = tbl01.Rows[i]["F7201014"].ToString();
                string mF7201015 = tbl01.Rows[i]["F7201015"].ToString();

                result = Production.UpdateTransInfo1(comcod, "SP_ENTRY_PRODUCTION", "SaveActualProductionDetails",
                           mLccod, mProdid, mStyleID, mColorID, mF7201001, mF7201002, mF7201003, mF7201004,
                           mF7201005, mF7201006, mF7201007, mF7201008, mF7201009, mF7201010, mF7201011,
                           mF7201012, mF7201013, mF7201014, mF7201015, mProddat, "", "", "", "", "", "");
                if (!result)
                {
                    this.lblMessage.Text = Production.ErrorObject["Msg"].ToString();
                    return;
                }
            }
            this.lblMessage.Text = "Updated Successfully";
        }


        protected void gvBalPro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBalPro.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }

}