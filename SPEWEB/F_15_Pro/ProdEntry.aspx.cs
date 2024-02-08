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
using Microsoft.Reporting.WinForms;

namespace SPEWEB.F_15_Pro
{
    public partial class ProdEntry : System.Web.UI.Page
    {

        ProcessAccess Production = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Goods Received Entry";
                this.Load_Location();
                this.GetLCCode();

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
           
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;

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
            this.DDLPrevIDList.DataValueField = "prdno";
            this.DDLPrevIDList.DataSource = ds1.Tables[0];
            this.DDLPrevIDList.DataBind();
            ViewState["TblPreOrder"] = ds1.Tables[0];
            DDLPrevIDList_SelectedIndexChanged(null, null);
            //if (this.DDLOrder.Items.Count == 0)
            //    this.GetLCCode();
        }
        //private void LoadDataLC_Order()
        //{

        //    string comcod = GetComCode();
        //    DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "EXISTLCORDER", "", "", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    this.DDLMasterLC.DataTextField = "ACTDESC1";
        //    this.DDLMasterLC.DataValueField = "ACTCODE";
        //    this.DDLMasterLC.DataSource = ds1.Tables[0];
        //    this.DDLMasterLC.DataBind();
        //    ViewState["TblOrder"] = ds1.Tables[1];
        //    //DDLMasterLC_SelectedIndexChanged(null, null);
        //}
        private void GetLCCode()
        {
            string comcod = GetComCode();
            string txtsrch = "%";
            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_MASTERLC", "DTLLCLIST", "", txtsrch, "", "", "", "", "", "", ""); ;
            if (ds1 == null)
                return;
            this.DDLOrder.DataTextField = "actdesc1";
            this.DDLOrder.DataValueField = "actcode";
            this.DDLOrder.DataSource = ds1.Tables[0];
            this.DDLOrder.DataBind();
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.DDLOrder.SelectedValue = this.Request.QueryString["actcode"].ToString();
                this.DDLOrder_SelectedIndexChanged(null, null);
                this.lbtnProOk_Click(null, null);

            }

        }

        protected void lbtnProOk_Click(object sender, EventArgs e)
        {
            try
            {

                
                if (this.lbtnProOk.Text == "Ok")
                {

                    this.lbtnProOk.Text = "New";
                    prodinf.Visible = true;
                    this.lbtnPrevList.Visible = false;
                    this.DDLPrevIDList.Visible = false;

                    if (this.DDLPrevIDList.Items.Count > 0)
                    {
                        this.lblProdID.Text = this.DDLPrevIDList.SelectedValue.ToString().Trim();
                        show_ProductionInformation(true);
                        //this.lblMLC.Visible = true;
                      
                        //this.DDLMasterLC.Visible = false;
                        this.DDLOrder.Enabled = false;


                        this.ProBal.Visible = false;
                        this.lblPage.Visible = false;

                        return;

                    }
                    this.lblPage.Visible = true;
                    this.ProBal.Visible = true;


                    //show_ProductionInformation(true);
                    if (this.Request.QueryString["Type"].ToString() == "Entry")
                    {
                        string comcod = GetComCode();
                        DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "LastProdNo", "", "", "", "", "", "", "", "", "");
                        if (ds1 == null)
                            return;
                        this.lblProdID.Text = ds1.Tables[0].Rows[0]["NewProdNo"].ToString();
                    }
                    else
                    {
                        this.lblProdID.Text = this.Request.QueryString["sircode"].ToString();
                    }
                   
                    //this.lblMLC.Text = this.DDLMasterLC.SelectedItem.ToString().Trim();
                    
                    show_ProductionInformation(true);
                    //this.lblMLC.Visible = true;
                   
                    this.ddlPreq.Enabled = false;
                    this.DDLOrder.Enabled = false;
                    // this.ShowBalanceQty();
                }
                else
                {
                   
                    this.DDLPrevIDList.Items.Clear();
                    this.lbtnProOk.Text = "Ok";
                    this.lblProdID.Text = "";
                    this.txtRefNo.Text = "";
                    prodinf.Visible = false;
                    this.lbtnPrevList.Visible = true;
                    this.DDLPrevIDList.Visible = true;
                    //this.DDLMasterLC.Visible = true;
                    this.DDLOrder.Enabled = true;
                    //this.lblMLC.Visible = false;
                    
                    this.lblPage.Visible = false;



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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + ex.Message + "');", true);
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
        protected void Load_Location()
        {
            string comcod = this.GetComCode();
            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETMATLOCATION", "", "%", "", "", "", "", "", "", "");

            ViewState["tblLocation"] = ds1.Tables[0];
            if (ds1 == null)
                return;
        }

        //protected void DDLMasterLC_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string mlccod = this.DDLMasterLC.SelectedValue.ToString().Trim();
        //    DataView dv1 = ((DataTable)ViewState["TblOrder"]).DefaultView;
        //    dv1.RowFilter = "mlccod = '" + mlccod + "'";
        //    this.DDLOrder.DataTextField = "GenData";
        //    this.DDLOrder.DataValueField = "gencod1";
        //    this.DDLOrder.DataSource = dv1;
        //    this.DDLOrder.DataBind();
        //}


        private void show_ProductionInformation(bool mEntryMode)
        {
            if (this.DDLOrder.SelectedValue == null)
            {
                return;
            }
            string comcod = GetComCode();
            //string mlccod = this.DDLMasterLC.SelectedValue.ToString().Trim();
            string orderid = this.DDLOrder.SelectedValue.ToString().Trim();
            string preqno = this.ddlPreq.SelectedValue.ToString().Trim();
            if (this.Request.QueryString["genno"].Length > 14)
            {
                preqno = this.Request.QueryString["genno"].ToString();
            }
            string Prodid = this.lblProdID.Text.Trim();
            string date = this.txtDate.Text;

            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODUCTION", "GET_PROD_INFO", orderid, Prodid, preqno, "", "", "", "");

            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> prodinf = ds1.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>();
            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> sizedesc = ds1.Tables[1].DataTableToList<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>();

            ViewState["Size"] = sizedesc;
            ViewState["tblProd"] = prodinf;// HiddenSameData(ds1.Tables[3]);
            ViewState["tbl_actualprod"] = prodinf;
            if (ds1 == null)
                return;

            if (ds1.Tables[2].Rows.Count > 0)
            {
                //this.DDLMasterLC.SelectedValue = ds1.Tables[2].Rows[0]["mlccod"].ToString().Substring(0, 8) + "0000";
                //DDLMasterLC_SelectedIndexChanged(null, null);
                //this.DDLOrder.SelectedValue = ds1.Tables[2].Rows[0]["mlccod"].ToString().Substring(0, 8) + "0000" + ds1.Tables[2].Rows[0]["mlccod"].ToString();
                this.txtDate.Text = Convert.ToDateTime(ds1.Tables[2].Rows[0]["prddate"]).ToString("dd-MMM-yyyy");
                this.txtRefNo.Text = ds1.Tables[2].Rows[0]["refno"].ToString();
                //this.lblMLC.Text = this.DDLMasterLC.SelectedItem.ToString();
               

            }
            if (ds1.Tables[3].Rows.Count == 0)
                return;
            this.TotalOrder.Text = Convert.ToDouble(ds1.Tables[3].Rows[0]["colororderqty"]).ToString("#,##0.00;(#,##0.00); ");
            this.BuyerName.Text = ds1.Tables[3].Rows[0]["buyername"].ToString();
            this.lblbrand.Text = ds1.Tables[3].Rows[0]["brand"].ToString();
            this.lblcolor.Text = ds1.Tables[3].Rows[0]["colordesc"].ToString();
            this.lblTrialOrderNo.Text = ds1.Tables[3].Rows[0]["trialordr"].ToString();
            this.lblarticle.Text = ds1.Tables[3].Rows[0]["article"].ToString();
            this.lblsizernge.Text = ds1.Tables[3].Rows[0]["sizerange"].ToString();
            this.SmpleIMg.ImageUrl = (ds1.Tables[3].Rows[0]["images"].ToString() == "") ? "~/images/no_img_preview.png" : ds1.Tables[3].Rows[0]["images"].ToString();
            this.ordqty.Text = Convert.ToDouble(ds1.Tables[3].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");
            this.lblCurrency.Text = ds1.Tables[3].Rows[0]["currency"].ToString();
            this.lblCurcode.Text = ds1.Tables[3].Rows[0]["curcode"].ToString();
            this.lblCRate.Text = ds1.Tables[3].Rows[0]["conrate"].ToString();
            this.Data_Bind();

            string toddat = Convert.ToDateTime(ds1.Tables[3].Rows[0]["todate"]).ToString("dd-MMM-yyyy");

            string style = ds1.Tables[3].Rows[0]["styleid"].ToString();
            string colorid = ds1.Tables[3].Rows[0]["colorid"].ToString();
            string dayid = ds1.Tables[3].Rows[0]["dayid"].ToString();
            string slnum = ds1.Tables[3].Rows[0]["slnum"].ToString();

            DataSet result = Production.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERWISE_SIZE_INFORMATION", orderid, style, colorid, dayid, toddat, slnum);
            if (result == null)
            {
                return;
            }
            ViewState["tblsizes"] = result.Tables[1];
            int j = 1;
            for (int i = 0; i < result.Tables[1].Rows.Count; i++)
            {

                int columid = j++;// Convert.ToInt32(ASTUtility.Right(result.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gvsizes.Columns[columid + 3].Visible = true;
                this.gvsizes.Columns[columid + 3].HeaderText = result.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
            }
            this.gvsizes.EditIndex = -1;

            this.gvsizes.DataSource = result.Tables[0];
            this.gvsizes.DataBind();
            this.adjustment();
        }
        private List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> HiddenSameData(List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> prodinf)
        {

            if (prodinf.Count == 0)
                return prodinf;

            string styleid = prodinf[0].styleid.ToString();
            string colorid = prodinf[0].colorid.ToString();

            for (int j = 1; j < prodinf.Count; j++)
            {
                if (prodinf[j].styleid.ToString() == styleid && prodinf[j].colorid.ToString() == colorid)
                {
                    styleid = prodinf[j].styleid.ToString();
                    colorid = prodinf[j].colorid.ToString();
                    prodinf[j].styledesc = "";
                    prodinf[j].colordesc = "";


                }

                else
                {
                    if (prodinf[j].styleid.ToString() == styleid)
                        prodinf[j].styledesc = "";
                    if (prodinf[j].colorid.ToString() == colorid)
                        prodinf[j].colordesc = "";

                    styleid = prodinf[j].styleid.ToString();
                    colorid = prodinf[j].colorid.ToString();

                }

            }


            return prodinf;


        }
        private void Data_Bind()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            if (hst["usrdesig"].ToString() != "Administrator")
            {
                this.gv1.Columns[48].Visible = false;
                this.gv1.Columns[49].Visible = false;

            }
            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> prodlist = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["tblProd"];
            if (prodlist.Count == 0)
            {
                this.gv1.DataSource = null;
                this.gv1.DataBind();

                return;
            }




            for (int i = 6; i < 20; i++)
                this.gv1.Columns[i].Visible = false;
            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> sizelst = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["Size"];
            int columid = 1;
            foreach (SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails c1 in sizelst)
            {

                // Convert.ToInt32(ASTUtility.Right(c1.sizeid.ToString(), 2));

                this.gv1.Columns[columid + 5].Visible = true;
                this.gv1.Columns[columid + 5].HeaderText = c1.sizedesc.ToString().Trim();
                columid = columid + 1;

            }
            this.gv1.EditIndex = -1;
            this.gv1_CalculateFootertotal(this.gv1, prodlist);
            this.gv1.DataSource = HiddenSameData(prodlist);
            this.gv1.DataBind();
            this.Production_Selection();


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
        protected void gv1_CalculateFootertotal(GridView gv1a, List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> ordrlist)
        {
            gv1a.Columns[6].FooterText = (ordrlist.Select(p => p.s1).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s1).Sum().ToString("#,##0;(#,##0); ");
            gv1a.Columns[7].FooterText = (ordrlist.Select(p => p.s2).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s2).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[8].FooterText = (ordrlist.Select(p => p.s3).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s3).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[9].FooterText = (ordrlist.Select(p => p.s4).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s4).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[10].FooterText = (ordrlist.Select(p => p.s5).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s5).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[11].FooterText = (ordrlist.Select(p => p.s6).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s6).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[12].FooterText = (ordrlist.Select(p => p.s7).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s7).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[13].FooterText = (ordrlist.Select(p => p.s8).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s8).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[14].FooterText = (ordrlist.Select(p => p.s9).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s9).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[15].FooterText = (ordrlist.Select(p => p.s10).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s10).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[16].FooterText = (ordrlist.Select(p => p.s11).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s11).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[17].FooterText = (ordrlist.Select(p => p.s12).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s12).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[18].FooterText = (ordrlist.Select(p => p.s13).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s13).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[19].FooterText = (ordrlist.Select(p => p.s14).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s14).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[20].FooterText = (ordrlist.Select(p => p.s15).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s15).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[21].FooterText = (ordrlist.Select(p => p.s16).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s16).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[22].FooterText = (ordrlist.Select(p => p.s17).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s17).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[23].FooterText = (ordrlist.Select(p => p.s18).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s18).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[24].FooterText = (ordrlist.Select(p => p.s19).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s19).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[25].FooterText = (ordrlist.Select(p => p.s20).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s20).Sum().ToString("#,##0;(#,##0); ");
            gv1a.Columns[26].FooterText = (ordrlist.Select(p => p.s21).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s21).Sum().ToString("#,##0;(#,##0); ");
            gv1a.Columns[27].FooterText = (ordrlist.Select(p => p.s22).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s22).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[28].FooterText = (ordrlist.Select(p => p.s23).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s23).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[29].FooterText = (ordrlist.Select(p => p.s24).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s24).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[30].FooterText = (ordrlist.Select(p => p.s25).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s25).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[31].FooterText = (ordrlist.Select(p => p.s26).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s26).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[32].FooterText = (ordrlist.Select(p => p.s27).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s27).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[33].FooterText = (ordrlist.Select(p => p.s28).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s28).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[34].FooterText = (ordrlist.Select(p => p.s29).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s29).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[35].FooterText = (ordrlist.Select(p => p.s30).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s30).Sum().ToString("#,##0;(#,##0); ");
            gv1a.Columns[36].FooterText = (ordrlist.Select(p => p.s31).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s31).Sum().ToString("#,##0;(#,##0); ");
            gv1a.Columns[37].FooterText = (ordrlist.Select(p => p.s32).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s32).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[38].FooterText = (ordrlist.Select(p => p.s33).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s33).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[39].FooterText = (ordrlist.Select(p => p.s34).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s34).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[40].FooterText = (ordrlist.Select(p => p.s35).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s35).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[41].FooterText = (ordrlist.Select(p => p.s36).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s36).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[42].FooterText = (ordrlist.Select(p => p.s37).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s37).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[43].FooterText = (ordrlist.Select(p => p.s38).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s38).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[44].FooterText = (ordrlist.Select(p => p.s39).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s39).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[45].FooterText = (ordrlist.Select(p => p.s40).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.s40).Sum().ToString("#,##0;(#,##0); ");

            gv1a.Columns[46].FooterText = (ordrlist.Select(p => p.totalqty).Sum() == 0.00) ? "0" : ordrlist.Select(p => p.totalqty).Sum().ToString("#,##0;(#,##0); ");


        }
        private void adjustment()
        {
            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> ordrlist = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["tblProd"];
            DataTable dt = ASITUtility03.ListToDataTable(ordrlist);
            if (dt.Rows.Count > 0)
            {
                var newDt = dt.AsEnumerable()
                                      .GroupBy(r => new {
                                          mlccod = r.Field<string>("mlccod"),
                                          styleid = r.Field<string>("styleid"),
                                          colorid = r.Field<string>("colorid"),
                                       
                                      })
                                      .Select(g =>
                                      {
                                          var row = dt.NewRow();
                                          row["mlccod"] = g.Key.mlccod;
                                          row["styleid"] = g.Key.styleid;
                                          row["colorid"] = g.Key.colorid;                                          
                                          row["mlcdesc"] = g.First()["mlcdesc"];
                                          row["styledesc"] = g.First()["styledesc"];
                                          row["colordesc"] = g.First()["colordesc"]; 
                                          row["StyleUnit"] = g.First()["StyleUnit"];
                                          row["s1"] = g.Sum(r => r.Field<double>("s1"));
                                          row["s2"] = g.Sum(r => r.Field<double>("s2"));
                                          row["s3"] = g.Sum(r => r.Field<double>("s3"));
                                          row["s4"] = g.Sum(r => r.Field<double>("s4"));
                                          row["s5"] = g.Sum(r => r.Field<double>("s5"));
                                          row["s6"] = g.Sum(r => r.Field<double>("s6"));
                                          row["s7"] = g.Sum(r => r.Field<double>("s7"));
                                          row["s8"] = g.Sum(r => r.Field<double>("s8"));
                                          row["s9"] = g.Sum(r => r.Field<double>("s9"));
                                          row["s10"] = g.Sum(r => r.Field<double>("s10"));
                                          row["s11"] = g.Sum(r => r.Field<double>("s11"));
                                          row["s12"] = g.Sum(r => r.Field<double>("s12"));
                                          row["s13"] = g.Sum(r => r.Field<double>("s13"));
                                          row["s14"] = g.Sum(r => r.Field<double>("s14"));
                                          row["s15"] = g.Sum(r => r.Field<double>("s15"));
                                          row["s16"] = g.Sum(r => r.Field<double>("s16"));
                                          row["s17"] = g.Sum(r => r.Field<double>("s17"));
                                          row["s18"] = g.Sum(r => r.Field<double>("s18"));
                                          row["s19"] = g.Sum(r => r.Field<double>("s19"));
                                          row["s20"] = g.Sum(r => r.Field<double>("s20"));
                                          row["s21"] = g.Sum(r => r.Field<double>("s21"));
                                          row["s22"] = g.Sum(r => r.Field<double>("s22"));
                                          row["s23"] = g.Sum(r => r.Field<double>("s23"));
                                          row["s24"] = g.Sum(r => r.Field<double>("s24"));
                                          row["s25"] = g.Sum(r => r.Field<double>("s25"));
                                          row["s26"] = g.Sum(r => r.Field<double>("s26"));
                                          row["s27"] = g.Sum(r => r.Field<double>("s27"));
                                          row["s28"] = g.Sum(r => r.Field<double>("s28"));
                                          row["s29"] = g.Sum(r => r.Field<double>("s29"));
                                          row["s30"] = g.Sum(r => r.Field<double>("s30"));
                                          row["s31"] = g.Sum(r => r.Field<double>("s31"));
                                          row["s32"] = g.Sum(r => r.Field<double>("s32"));
                                          row["s33"] = g.Sum(r => r.Field<double>("s33"));
                                          row["s34"] = g.Sum(r => r.Field<double>("s34"));
                                          row["s35"] = g.Sum(r => r.Field<double>("s35"));
                                          row["s36"] = g.Sum(r => r.Field<double>("s36"));
                                          row["s37"] = g.Sum(r => r.Field<double>("s37"));
                                          row["s38"] = g.Sum(r => r.Field<double>("s38"));
                                          row["s39"] = g.Sum(r => r.Field<double>("s39"));
                                          row["s40"] = g.Sum(r => r.Field<double>("s40"));
                                          row["totalqty"] = g.Sum(r => r.Field<double>("totalqty"));


                                          return row;
                                      }).CopyToDataTable();

                Session["tblProdHelper"] = newDt;

                this.gvtblProductionHelper_DataBind();
            }
        }
        private void gvtblProductionHelper_DataBind()
        {
            DataTable dtisue = (DataTable)Session["tblProdHelper"];

            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> sizelst = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["Size"];
            int columid = 1;
            foreach (SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails c1 in sizelst)
            {              

                this.gvProdItem.Columns[columid + 4].Visible = true;
                this.gvProdItem.Columns[columid + 4].HeaderText = c1.sizedesc.ToString().Trim();
                columid = columid + 1;

            }

            this.gvProdItem.DataSource = dtisue;
            this.gvProdItem.DataBind();

            //((Label)this.gvIsuItem.FooterRow.FindControl("lgvRSumSMRRQty")).Text = (listsum.Select(p => p.mrrqty).Sum() == 0.00) ? "0" : listsum.Select(p => p.mrrqty).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalBalQty")).Text = (listsum.Select(p => p.orderbal).Sum() == 0.00) ? "0" : listsum.Select(p => p.orderbal).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalReceived")).Text = (listsum.Select(p => p.recup).Sum() == 0.00) ? "0" : listsum.Select(p => p.recup).Sum().ToString("#,##0.00;(#,##0.00); ");
            //((Label)this.gvIsuItem.FooterRow.FindControl("lblTotalOrdQty")).Text = (listsum.Select(p => p.ordrqty).Sum() == 0.00) ? "0" : listsum.Select(p => p.ordrqty).Sum().ToString("#,##0.00;(#,##0.00); ");

        }
        protected void LbtnReqItemShow_Click(object sender, EventArgs e)
        {
            if (this.LbtnReqItemShow.Text == "Expand")
            {
                this.gvProdItem.Visible = true;
                this.LbtnReqItemShow.Text = "Collapse";
            }
            else
            {
                  this.gvProdItem.Visible = false;
                this.LbtnReqItemShow.Text = "Expand";
            }
        }

        protected void lbtnPrintReport_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = GetComCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string orderno = this.DDLOrder.SelectedItem.Text;

            string mlcno = "";//this.lblMLC.Text.ToString().Substring(14);
            string refno = this.txtRefNo.Text.ToString();
           
            string date = this.txtDate.Text.ToString();

            string prodid = this.lblProdID.Text.Trim().ToString();

            DataSet ds1 = Production.GetTransInfo(comcod, "SP_REPORT_PRODUCTION", "RPTPRODDETAILS", prodid, "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];
            DataTable dt1 = HiddenSameData(dt);

            var lst = dt.DataTableToList<SPEENTITY.C_15_Pro.BO_Production.EclassProdDetails>();

            LocalReport rpt1 = new LocalReport();

            rpt1 = RptSetupClass.GetLocalReport("R_15_Pro.RptProdinfo", lst, null, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Goods Received Information"));
            rpt1.SetParameters(new ReportParameter("prodid", prodid));
            rpt1.SetParameters(new ReportParameter("refno", "Ref No:" + refno));
            //rpt1.SetParameters(new ReportParameter("mlcno", mlcno));
            rpt1.SetParameters(new ReportParameter("orderno", orderno));
            rpt1.SetParameters(new ReportParameter("date", "Date: " + date));
            rpt1.SetParameters(new ReportParameter("client", this.BuyerName.Text.ToString()));
            rpt1.SetParameters(new ReportParameter("brand", this.lblbrand.Text.ToString()));
            rpt1.SetParameters(new ReportParameter("color", this.lblcolor.Text.ToString()));
            rpt1.SetParameters(new ReportParameter("article", this.lblarticle.Text.ToString()));
            rpt1.SetParameters(new ReportParameter("sizernge", this.lblsizernge.Text.ToString()));
            rpt1.SetParameters(new ReportParameter("trialordr", this.lblTrialOrderNo.Text.ToString()));
            rpt1.SetParameters(new ReportParameter("trialordrqty", this.ordqty.Text.ToString()));
            rpt1.SetParameters(new ReportParameter("tordrqty", this.TotalOrder.Text.ToString()));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat("", username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        private DataTable HiddenSameData(DataTable dt)     // HiddenSameData by  DataTable 
        {
            if (dt.Rows.Count == 0)
                return dt;


            string styldedesc = dt.Rows[0]["styldedesc"].ToString();
            string colordesc = dt.Rows[0]["colordesc"].ToString();

            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["styldedesc"].ToString() == styldedesc && dt.Rows[j]["colordesc"].ToString() == colordesc)
                {
                    styldedesc = dt.Rows[j]["styldedesc"].ToString();
                    colordesc = dt.Rows[j]["colordesc"].ToString();

                    dt.Rows[j]["styldedesc"] = "";
                    dt.Rows[j]["colordesc"] = "";


                }

                else
                {
                    styldedesc = dt.Rows[j]["styldedesc"].ToString();
                    colordesc = dt.Rows[j]["colordesc"].ToString();


                }
            }
            return dt;
        }

        //protected void OldlbtnPrintReport_Click(object sender, EventArgs e)
        //{

        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    ReportDocument _reportDoc = new RMGiRPT.R_15_Pro.rptProductionInfo();

        //    TextObject txtCompName = _reportDoc.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
        //    txtCompName.Text = comnam;

        //    TextObject txtCompAdd = _reportDoc.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
        //    txtCompAdd.Text = comadd;

        //    TextObject txtheadings = _reportDoc.ReportDefinition.ReportObjects["txtheadings"] as TextObject;
        //    txtheadings.Text = "PRODUCTION INFORMATION DETAILS";

        //    TextObject txtmlcdesc = _reportDoc.ReportDefinition.ReportObjects["txtmlcdesc"] as TextObject;
        //    txtmlcdesc.Text = "";//this.DDLMasterLC.SelectedItem.Text.Substring(10);

        //    TextObject txtOrdrDesc = _reportDoc.ReportDefinition.ReportObjects["txtOrdrDesc"] as TextObject;
        //    txtOrdrDesc.Text = this.DDLOrder.SelectedItem.Text.Substring(10);

        //    TextObject txtrefno = _reportDoc.ReportDefinition.ReportObjects["txtrefno"] as TextObject;
        //    txtrefno.Text = this.txtRefNo.Text;

        //    TextObject txtProdno = _reportDoc.ReportDefinition.ReportObjects["txtProdno"] as TextObject;
        //    txtProdno.Text = this.lblProdID.Text;

        //    TextObject txtproddat = _reportDoc.ReportDefinition.ReportObjects["txtproddat"] as TextObject;
        //    txtproddat.Text = this.txtDate.Text.Substring(0, 11);// this.dtpProducDat.Value.ToString("dd-MMM-yyyy");

        //    TextObject txtF7201001 = _reportDoc.ReportDefinition.ReportObjects["txtF7201001"] as TextObject;
        //    txtF7201001.Text = (this.gv1.Columns[6].Visible ? this.gv1.Columns[6].HeaderText.ToString() : "");

        //    TextObject txtF7201002 = _reportDoc.ReportDefinition.ReportObjects["txtF7201002"] as TextObject;
        //    txtF7201002.Text = (this.gv1.Columns[7].Visible ? this.gv1.Columns[7].HeaderText.ToString() : "");

        //    TextObject txtF7201003 = _reportDoc.ReportDefinition.ReportObjects["txtF7201003"] as TextObject;
        //    txtF7201003.Text = (this.gv1.Columns[8].Visible ? this.gv1.Columns[8].HeaderText.ToString() : "");

        //    TextObject txtF7201004 = _reportDoc.ReportDefinition.ReportObjects["txtF7201004"] as TextObject;
        //    txtF7201004.Text = (this.gv1.Columns[9].Visible ? this.gv1.Columns[9].HeaderText.ToString() : "");

        //    TextObject txtF7201005 = _reportDoc.ReportDefinition.ReportObjects["txtF7201005"] as TextObject;
        //    txtF7201005.Text = (this.gv1.Columns[10].Visible ? this.gv1.Columns[10].HeaderText.ToString() : "");

        //    TextObject txtF7201006 = _reportDoc.ReportDefinition.ReportObjects["txtF7201006"] as TextObject;
        //    txtF7201006.Text = (this.gv1.Columns[11].Visible ? this.gv1.Columns[11].HeaderText.ToString() : "");

        //    TextObject txtF7201007 = _reportDoc.ReportDefinition.ReportObjects["txtF7201007"] as TextObject;
        //    txtF7201007.Text = (this.gv1.Columns[12].Visible ? this.gv1.Columns[12].HeaderText.ToString() : "");

        //    TextObject txtF7201008 = _reportDoc.ReportDefinition.ReportObjects["txtF7201008"] as TextObject;
        //    txtF7201008.Text = (this.gv1.Columns[13].Visible ? this.gv1.Columns[13].HeaderText.ToString() : "");

        //    TextObject txtF7201009 = _reportDoc.ReportDefinition.ReportObjects["txtF7201009"] as TextObject;
        //    txtF7201009.Text = (this.gv1.Columns[14].Visible ? this.gv1.Columns[14].HeaderText.ToString() : "");
        //    TextObject txtuserinfo = _reportDoc.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    DataView dvRptData = ((DataTable)ViewState["tblProd"]).DefaultView;
        //    dvRptData.RowFilter = "Total1 > 0 or colorid='000000000000'";
        //    _reportDoc.SetDataSource(dvRptData);

        //    Session["Report1"] = _reportDoc;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //}

        protected void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {

            this.Save_Value();
            this.Data_Bind();

        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mLccod = this.DDLOrder.SelectedValue.ToString();
            string preqno = this.ddlPreq.SelectedValue.ToString();
            string mProdid = this.lblProdID.Text.Trim();
            string mProdid2 = this.txtRefNo.Text.Trim();
            string mProddat = this.txtDate.Text.Trim().Substring(0, 11);
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string entrydat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = GetComCode();
            string curcode = this.lblCurcode.Text.ToString();
            string conrate = this.lblCRate.Text.ToString();
            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> ordrlist = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["tblProd"];
            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> Size = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["Size"];

            DataTable dt1 = ASITUtility03.ListToDataTable(ordrlist);
            DataTable dt2 = ASITUtility03.ListToDataTable(Size);
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt1);
            ds1.Tables.Add(dt2);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            var temp = ds1.GetXml();
            bool result = Production.UpdateXmlTransInfo(comcod, "SP_ENTRY_PRODUCTION", "UPDATE_PROD_INFO", ds1, null, null, mLccod, mProdid, mProdid2,
                mProddat, userid, Terminal, Sessionid, entrydat, preqno, curcode, conrate, "", "", "");


            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

                return;
            }

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

        private void Save_Value()
        {

            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> ordrlist = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["tblProd"];
            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> tbl_actualprod = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["tbl_actualprod"];


            for (int i = 0; i < gv1.Rows.Count; i++)
            {
                string mStyleID = ((Label)gv1.Rows[i].FindControl("lblgvStyleID")).Text.Trim();
                string mColorID = ((Label)gv1.Rows[i].FindControl("lblgvColorID")).Text.Trim();
                string location = ((DropDownList)gv1.Rows[i].FindControl("ddlval")).SelectedValue.ToString();
                ordrlist[i].s1 = (tbl_actualprod[i].s1 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF1")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF1")).Text.Trim()) : tbl_actualprod[i].s1;
                ordrlist[i].s2 = (tbl_actualprod[i].s2 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF2")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF2")).Text.Trim()) : tbl_actualprod[i].s2;
                ordrlist[i].s3 = (tbl_actualprod[i].s3 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF3")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF3")).Text.Trim()) : tbl_actualprod[i].s3;
                ordrlist[i].s4 = (tbl_actualprod[i].s4 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF4")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF4")).Text.Trim()) : tbl_actualprod[i].s4;
                ordrlist[i].s5 = (tbl_actualprod[i].s5 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF5")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF5")).Text.Trim()) : tbl_actualprod[i].s5;
                ordrlist[i].s6 = (tbl_actualprod[i].s6 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF6")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF6")).Text.Trim()) : tbl_actualprod[i].s6;
                ordrlist[i].s7 = (tbl_actualprod[i].s7 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7")).Text.Trim()) : tbl_actualprod[i].s7;
                ordrlist[i].s8 = (tbl_actualprod[i].s8 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF8")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF8")).Text.Trim()) : tbl_actualprod[i].s8;
                ordrlist[i].s9 = (tbl_actualprod[i].s9 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF9")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF9")).Text.Trim()) : tbl_actualprod[i].s9;
                ordrlist[i].s10 = (tbl_actualprod[i].s10 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF10")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF10")).Text.Trim()) : tbl_actualprod[i].s10;
                ordrlist[i].s11 = (tbl_actualprod[i].s11 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF11")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF11")).Text.Trim()) : tbl_actualprod[i].s11;
                ordrlist[i].s12 = (tbl_actualprod[i].s12 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF12")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF12")).Text.Trim()) : tbl_actualprod[i].s12;
                ordrlist[i].s13 = (tbl_actualprod[i].s13 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF13")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF13")).Text.Trim()) : tbl_actualprod[i].s13;
                ordrlist[i].s14 = (tbl_actualprod[i].s14 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF14")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF14")).Text.Trim()) : tbl_actualprod[i].s14;
                ordrlist[i].s15 = (tbl_actualprod[i].s15 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF15")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF15")).Text.Trim()) : tbl_actualprod[i].s15;
                ordrlist[i].s16 = (tbl_actualprod[i].s16 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF16")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF16")).Text.Trim()) : tbl_actualprod[i].s16;
                ordrlist[i].s17 = (tbl_actualprod[i].s17 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF17")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF17")).Text.Trim()) : tbl_actualprod[i].s17;
                ordrlist[i].s18 = (tbl_actualprod[i].s18 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF18")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF18")).Text.Trim()) : tbl_actualprod[i].s18;
                ordrlist[i].s19 = (tbl_actualprod[i].s19 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF19")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF19")).Text.Trim()) : tbl_actualprod[i].s19;
                ordrlist[i].s20 = (tbl_actualprod[i].s20 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF20")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF20")).Text.Trim()) : tbl_actualprod[i].s20;
                ordrlist[i].s21 = (tbl_actualprod[i].s21 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF21")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF21")).Text.Trim()) : tbl_actualprod[i].s21;
                ordrlist[i].s22 = (tbl_actualprod[i].s22 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF22")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF22")).Text.Trim()) : tbl_actualprod[i].s22;
                ordrlist[i].s23 = (tbl_actualprod[i].s23 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF23")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF23")).Text.Trim()) : tbl_actualprod[i].s23;
                ordrlist[i].s24 = (tbl_actualprod[i].s24 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF24")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF24")).Text.Trim()) : tbl_actualprod[i].s24;
                ordrlist[i].s25 = (tbl_actualprod[i].s25 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF25")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF25")).Text.Trim()) : tbl_actualprod[i].s25;
                ordrlist[i].s26 = (tbl_actualprod[i].s26 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF26")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF26")).Text.Trim()) : tbl_actualprod[i].s26;
                ordrlist[i].s27 = (tbl_actualprod[i].s27 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF27")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF27")).Text.Trim()) : tbl_actualprod[i].s27;
                ordrlist[i].s28 = (tbl_actualprod[i].s28 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF28")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF28")).Text.Trim()) : tbl_actualprod[i].s28;
                ordrlist[i].s29 = (tbl_actualprod[i].s29 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF29")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF29")).Text.Trim()) : tbl_actualprod[i].s29;
                ordrlist[i].s30 = (tbl_actualprod[i].s30 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF30")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF30")).Text.Trim()) : tbl_actualprod[i].s30;
                ordrlist[i].s31 = (tbl_actualprod[i].s31 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF31")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF31")).Text.Trim()) : tbl_actualprod[i].s31;
                ordrlist[i].s32 = (tbl_actualprod[i].s32 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF32")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF32")).Text.Trim()) : tbl_actualprod[i].s32;
                ordrlist[i].s33 = (tbl_actualprod[i].s33 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF33")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF33")).Text.Trim()) : tbl_actualprod[i].s33;
                ordrlist[i].s34 = (tbl_actualprod[i].s34 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF34")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF34")).Text.Trim()) : tbl_actualprod[i].s34;
                ordrlist[i].s35 = (tbl_actualprod[i].s35 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF35")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF35")).Text.Trim()) : tbl_actualprod[i].s35;
                ordrlist[i].s36 = (tbl_actualprod[i].s36 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF36")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF36")).Text.Trim()) : tbl_actualprod[i].s36;
                ordrlist[i].s37 = (tbl_actualprod[i].s37 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF37")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF37")).Text.Trim()) : tbl_actualprod[i].s37;
                ordrlist[i].s38 = (tbl_actualprod[i].s38 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF38")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF38")).Text.Trim()) : tbl_actualprod[i].s38;
                ordrlist[i].s39 = (tbl_actualprod[i].s39 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF39")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF39")).Text.Trim()) : tbl_actualprod[i].s39;
                ordrlist[i].s40 = (tbl_actualprod[i].s40 >= Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF40")).Text.Trim())) ? Convert.ToDouble("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF40")).Text.Trim()) : tbl_actualprod[i].s40;


                ordrlist[i].totalqty = (ordrlist[i].s1 + ordrlist[i].s2 + ordrlist[i].s3 + ordrlist[i].s4 + ordrlist[i].s5 +
                           ordrlist[i].s6 + ordrlist[i].s7 + ordrlist[i].s8 + ordrlist[i].s9 + ordrlist[i].s10 + ordrlist[i].s11 +
                            ordrlist[i].s12 + ordrlist[i].s13 + ordrlist[i].s14 + ordrlist[i].s15 + ordrlist[i].s16 + ordrlist[i].s17 + ordrlist[i].s18 +
                            ordrlist[i].s19 + ordrlist[i].s20 + ordrlist[i].s21 + ordrlist[i].s22 + ordrlist[i].s23 + ordrlist[i].s24 + ordrlist[i].s25 +
                           ordrlist[i].s26 + ordrlist[i].s27 + ordrlist[i].s28 + ordrlist[i].s29 + ordrlist[i].s30 + ordrlist[i].s31 + ordrlist[i].s32 +
                           ordrlist[i].s33 + ordrlist[i].s34 + ordrlist[i].s35 +
                           ordrlist[i].s36 + ordrlist[i].s37 + ordrlist[i].s38 + ordrlist[i].s39 + ordrlist[i].s40);
                ordrlist[i].location = location;
            }
            ViewState["tblProd"] = ordrlist;

        }

        protected void DDLPrevIDList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["TblPreOrder"];
            string prodid = this.DDLPrevIDList.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "prdno='" + prodid + "'";

            DDLOrder.SelectedValue = dv.ToTable().Rows[0]["actcode"].ToString();

            //this.DDLOrder.DataTextField = "actdesc1";
            //this.DDLOrder.DataValueField = "actcode";
            //this.DDLOrder.DataSource = dv.ToTable();
            //this.DDLOrder.DataBind();

        }
        private void Production_Selection()
        {
            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> prodlist = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["tblProd"];
            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> sizelst = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["Size"];

            DataTable dt = ASITUtility03.ListToDataTable(prodlist);

            for (int i = 0; i < this.gv1.Rows.Count; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    double value = Convert.ToDouble(dt.Rows[i]["p" + j + ""]);
                    //   double value=Convert.ToDouble("0" + ((Label)this.gv1.Rows[i].FindControl("lblP"+j+"")).Text.Trim());
                    if (value == 0)
                    {
                        ((TextBox)gv1.Rows[i].FindControl("txtgvF" + j + "")).Enabled = false;
                        ((TextBox)gv1.Rows[i].FindControl("txtgvF" + j + "")).BackColor = System.Drawing.Color.DarkRed;
                        ((TextBox)gv1.Rows[i].FindControl("txtgvF" + j + "")).ToolTip = "Can't Production";
                    }
                }
            }
        }



        protected void DDLOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            string mlccod = this.DDLOrder.SelectedValue.ToString();
            DataSet ds1 = Production.GetTransInfo(comcod, "SP_ENTRY_PRODPROCESS", "GET_ORDER_WISE_REQNO", mlccod, "", "", "", "", "", "", ""); ;
            if (ds1 == null)
                return;
            this.ddlPreq.DataTextField = "preqno";
            this.ddlPreq.DataValueField = "preqno";
            this.ddlPreq.DataSource = ds1.Tables[0];
            this.ddlPreq.DataBind();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlPreq.SelectedValue = this.Request.QueryString["genno"].ToString();
            }
        }

        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddllocation = (DropDownList)e.Row.FindControl("ddlval");
                DataTable dt = (DataTable)ViewState["tblLocation"];
                string location = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "location")).ToString();
                ddllocation.DataTextField = "gdesc";
                ddllocation.DataValueField = "gcod";
                ddllocation.DataSource = dt;
                ddllocation.DataBind();
                ddllocation.SelectedValue = location;

            }
        }

        protected void LbtnAdjust_Click(object sender, EventArgs e)
        {
            DataTable tblProdHelper = (DataTable)Session["tblProdHelper"];
            List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails> prodlist = (List<SPEENTITY.C_03_CostABgd.EClassLC.EclassOrderDetails>)ViewState["tblProd"];

            for (int i = 0; i < this.gvProdItem.Rows.Count; i++)
            {

            }

        }
    }

}