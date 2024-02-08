using Microsoft.Reporting.WinForms;
using SPELIB;
using SPERDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPEWEB.F_05_ProShip
{
    public partial class RptLotWiseArticle : System.Web.UI.Page
    {
        ProcessAccess Merdata = new ProcessAccess();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Lot Wise Article Report";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CommonButton();

                this.GetLotList();

            
                if (this.ddlmlccod.Items.Count == 0)
                {
                    this.GetSesson();
                }

            }
           
        }
        public void CommonButton()
        {
            //   ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void GetSesson()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            
            string season = Request.QueryString["genno"].ToString().Length==0 ? hst["season"].ToString() : Request.QueryString["genno"].ToString();
           
            this.DdlSeason.SelectedValue = season;

            DdlSeason_SelectedIndexChanged(null, null);
        }
        protected void DdlSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMasterLc();
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> list = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];
            DataTable dt1 = (DataTable)ViewState["tblOrderSize"];
            DataTable dt2 = (DataTable)ViewState["tblratio"];

            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> listratio = dt2.DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCatLot>();


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compadd = hst["comadd1"].ToString();
            string masterlc = "Article Name: " + ddlmlccod.SelectedItem.Text;
            string style = "Style: " + ddlStyle.SelectedItem.Text;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //var list = dt.DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCatLot>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_05_ProShip.RptArticleWiseLot", list, listratio, null);
            rpt1.EnableExternalImages = true;
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("compAddress", compadd));
            rpt1.SetParameters(new ReportParameter("masterlc", masterlc));
            rpt1.SetParameters(new ReportParameter("style", style));
            rpt1.SetParameters(new ReportParameter("rptTitle", "Article Wise Lot"));
            rpt1.SetParameters(new ReportParameter("rptTitle2", "Size Wise Ratio"));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(comnam, username, printdate)));

            //int i = 1;
            //foreach (var size in listratio)
            //{

            //    rpt1.SetParameters(new ReportParameter(i.ToString(), size.s1));
            //    i++;
            //    if (i > 25) break;
            //}

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                rpt1.SetParameters(new ReportParameter("size" + (i + 1).ToString(), dt1.Rows[i]["SizeDesc"].ToString()));
            }

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void GetMasterLc()
        {

            string comcod = GetCompCode();
            string findseason = (this.DdlSeason.SelectedValue.ToString() == "00000") ? "%" : this.DdlSeason.SelectedValue.ToString() + "%";

            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERNO", "1601%", "%", findseason, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblordstyle"] = ds1.Tables[0];

            this.ddlmlccod.DataTextField = "mlcdesc";
            this.ddlmlccod.DataValueField = "mlccod";
            this.ddlmlccod.DataSource = ds1.Tables[1];
            this.ddlmlccod.DataBind();

            if(ds1.Tables[0].Rows.Count > 0)
            {
                if (Request.QueryString["actcode"].ToString().Length > 0)
                {
                    this.ddlmlccod.SelectedValue = Request.QueryString["actcode"].ToString();
                }
       
            }

            this.ddlmlccod_SelectedIndexChanged(null, null);
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value_Lot_allocation();
            this.Bind_Order_Allocation();
        }


        protected void ddlmlccod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mlccode1 = ddlmlccod.SelectedValue.ToString();
            DataTable dt1 = ((DataTable)ViewState["tblordstyle"]).Copy();
            DataView dv1;
            dv1 = dt1.DefaultView;
            dv1.RowFilter = ("mlccod='" + mlccode1 + "'");
            dt1 = dv1.ToTable(true, "styledesc2", "stylecode1");
            this.ddlStyle.DataTextField = "styledesc2";
            this.ddlStyle.DataValueField = "stylecode1";
            this.ddlStyle.DataSource = dt1;
            this.ddlStyle.DataBind();

            if(dt1.Rows.Count > 0)
            {
               
                if (Request.QueryString["dayid"].ToString().Length > 0)
                {
                    this.ddlStyle.SelectedValue = Request.QueryString["dayid"].ToString();

                }
                if (Request.QueryString["actcode"].ToString().Length>0 && Request.QueryString["dayid"].ToString().Length > 0)
                {
                    this.lbtnOk_Click(null, null);

                }
            }
        }


        private void Save_Value_Lot_allocation()
        {
            //////////// packing /////////////////
            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lstlot = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];
            if (lstlot == null)
                return;
            for (int i = 0; i < this.gv1.Rows.Count; i++)
            {
                string lotno = ((DropDownList)gv1.Rows[i].FindControl("DdlLotlist")).SelectedValue.ToString();
                double s1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF1")).Text.Trim()));
                double s2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF2")).Text.Trim()));
                double s3 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF3")).Text.Trim()));
                double s4 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF4")).Text.Trim()));
                double s5 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF5")).Text.Trim()));
                double s6 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF6")).Text.Trim()));
                double s7 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF7")).Text.Trim()));
                double s8 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF8")).Text.Trim()));
                double s9 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF9")).Text.Trim()));
                double s10 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF10")).Text.Trim()));
                double s11 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF11")).Text.Trim()));
                double s12 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF12")).Text.Trim()));
                double s13 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF13")).Text.Trim()));
                double s14 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF14")).Text.Trim()));
                double s15 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF15")).Text.Trim()));
                double s16 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF16")).Text.Trim()));
                double s17 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF17")).Text.Trim()));
                double s18 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF18")).Text.Trim()));
                double s19 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF19")).Text.Trim()));
                double s20 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF20")).Text.Trim()));
                double s21 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF21")).Text.Trim()));
                double s22 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF22")).Text.Trim()));
                double s23 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF23")).Text.Trim()));
                double s24 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF24")).Text.Trim()));
                double s25 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF25")).Text.Trim()));
                double s26 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF26")).Text.Trim()));
                double s27 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF27")).Text.Trim()));
                double s28 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF28")).Text.Trim()));
                double s29 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF29")).Text.Trim()));
                double s30 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF30")).Text.Trim()));
                double s31 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF31")).Text.Trim()));
                double s32 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF32")).Text.Trim()));
                double s33 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF33")).Text.Trim()));
                double s34 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF34")).Text.Trim()));
                double s35 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF35")).Text.Trim()));
                double s36 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF36")).Text.Trim()));
                double s37 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF37")).Text.Trim()));
                double s38 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF38")).Text.Trim()));
                double s39 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF39")).Text.Trim()));
                double s40 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1.Rows[i].FindControl("txtgvF40")).Text.Trim()));


                lstlot[i].lotno = lotno;
                lstlot[i].s1 = s1;
                lstlot[i].s2 = s2;
                lstlot[i].s3 = s3;
                lstlot[i].s4 = s4;
                lstlot[i].s5 = s5;
                lstlot[i].s6 = s6;
                lstlot[i].s7 = s7;
                lstlot[i].s8 = s8;
                lstlot[i].s9 = s9;
                lstlot[i].s10 = s10;
                lstlot[i].s11 = s11;
                lstlot[i].s12 = s12;
                lstlot[i].s13 = s13;
                lstlot[i].s14 = s14;
                lstlot[i].s15 = s15;
                lstlot[i].s16 = s16;
                lstlot[i].s17 = s17;
                lstlot[i].s18 = s18;
                lstlot[i].s19 = s19;
                lstlot[i].s20 = s20;
                lstlot[i].s21 = s21;
                lstlot[i].s22 = s22;
                lstlot[i].s23 = s23;
                lstlot[i].s24 = s24;
                lstlot[i].s25 = s25;
                lstlot[i].s26 = s26;
                lstlot[i].s27 = s27;
                lstlot[i].s28 = s28;
                lstlot[i].s29 = s29;
                lstlot[i].s30 = s30;
                lstlot[i].s31 = s31;
                lstlot[i].s32 = s32;
                lstlot[i].s33 = s33;
                lstlot[i].s34 = s34;
                lstlot[i].s35 = s35;
                lstlot[i].s36 = s36;
                lstlot[i].s37 = s37;
                lstlot[i].s38 = s38;
                lstlot[i].s39 = s39;
                lstlot[i].s40 = s10;
                lstlot[i].totalqty = (s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12 + s13 + s14 + s15 + s16 + s17 + s18 + s19 + s20 +
                    s21 + s22 + s23 + s24 + s25 + s26 + s27 + s28 + s29 + s30 + s31 + s32 + s33 + s34 + s35 + s36 + s37 + s38 + s39 + s40);


            }
            ViewState["tblOrderQty"] = lstlot;
        }


        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Save_Value_Lot_allocation();
                List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst1 = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];
                //List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst2= (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)lst1;


                //lst2.RemoveAt(lst1.FindIndex(p => p.colordesc != ""));
                List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst2 = lst1.FindAll(p => p.colordesc == "").ToList();

                if (lst2 == null)
                    return;
                string comcod = GetCompCode();
                string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
                string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
                string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
                string mLCCode = this.ddlmlccod.SelectedValue.ToString();

                bool result = false;

                DataTable dtsizes = (DataTable)ViewState["tblOrderSize"];
                String[] SizeID = new String[dtsizes.Rows.Count];
                int sizeindex = 0;
                foreach (DataRow item in dtsizes.Rows)
                {
                    SizeID[sizeindex] = item["sizeid"].ToString();
                    sizeindex++;
                }

                for (int i = 0; i < lst2.Count; i++)
                {
                    string slnum = lst2[i].slnum;
                    string lotno = lst2[i].lotno;

                    //String[] SizeID = {"720100101001", "720100101002", "720100101003", "720100101004", "720100101005", "720100101006",
                    //           "720100101007", "720100101008", "720100101009", "720100101010", "720100101011", "720100101012",
                    //           "720100101013", "720100101014", "720100101015", "720100101016", "720100101017", "720100101018", "720100101019", "720100101020",
                    //              "720100101021", "720100101022", "720100101023", "720100101024", "720100101025", "720100101026", "720100101027", "720100101028",
                    //              "720100101029", "720100101030", "720100101031", "720100101032", "720100101033", "720100101034", "720100101035", "720100101036", "720100101037",
                    //              "720100101038", "720100101039", "720100101040"};
                    String[] OrderQty = {lst2[i].s1.ToString(),
          lst2[i].s2.ToString(),lst2[i].s3.ToString(),
        lst2[i].s4.ToString(),lst2[i].s5.ToString(),  lst2[i].s6.ToString(),lst2[i].s7.ToString(),
            lst2[i].s8.ToString(),lst2[i].s9.ToString(),
           lst2[i].s10.ToString(), lst2[i].s11.ToString(),
           lst2[i].s12.ToString(),lst2[i].s13.ToString(),
           lst2[i].s14.ToString(),            lst2[i].s15.ToString(),
            lst2[i].s16.ToString(),           lst2[i].s17.ToString(),
           lst2[i].s18.ToString(),            lst2[i].s19.ToString(),
            lst2[i].s20.ToString(),            lst2[i].s21.ToString(),
           lst2[i].s22.ToString(),            lst2[i].s23.ToString(),
           lst2[i].s24.ToString(),            lst2[i].s25.ToString(),
            lst2[i].s26.ToString(),            lst2[i].s27.ToString(),
            lst2[i].s28.ToString(),          lst2[i].s29.ToString(),
            lst2[i].s30.ToString(),            lst2[i].s31.ToString(),
           lst2[i].s32.ToString(),           lst2[i].s33.ToString(),
            lst2[i].s34.ToString(),            lst2[i].s35.ToString(),
            lst2[i].s36.ToString(),            lst2[i].s37.ToString(),
          lst2[i].s38.ToString(),            lst2[i].s39.ToString(),
          lst2[i].s40.ToString(),

            };

                    if (lst2[i].slnum != "" && lst2[i].slnum != "000" && lst2[i].lotno != "00000")
                    {
                        for (int j = 0; j < SizeID.Length; j++)
                        {
                            if (Convert.ToDouble(OrderQty[j]) > 0)
                            {

                                result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "UPDATE_ORDER_LOT_DETAILS", mLCCode, dayid, styleid, colorid, lotno,
                                           SizeID[j], OrderQty[j], slnum, "", "", "", "");

                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Lot Selection Error');", true);

                    }

                }

                if (result)
                {
                    //   this.OrderDetailsInformation();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


                }

                //////////////update ratio////////////////
                DataTable tblratio = (DataTable)ViewState["tblratio"];
                double lottotalqty = Convert.ToDouble(tblratio.Rows[0]["totalqty"]);
                String[] lotqty = {tblratio.Rows[0]["s1"].ToString(),
          tblratio.Rows[0]["s2"].ToString(),tblratio.Rows[0]["s3"].ToString(),
        tblratio.Rows[0]["s4"].ToString(),tblratio.Rows[0]["s5"].ToString(),  tblratio.Rows[0]["s6"].ToString(),
        tblratio.Rows[0]["s7"].ToString(),
           tblratio.Rows[0]["s8"].ToString(),tblratio.Rows[0]["s9"].ToString(),
           tblratio.Rows[0]["s10"].ToString(), tblratio.Rows[0]["s11"].ToString(),
           tblratio.Rows[0]["s12"].ToString(),tblratio.Rows[0]["s13"].ToString(),
           tblratio.Rows[0]["s14"].ToString(),           tblratio.Rows[0]["s15"].ToString(),
            tblratio.Rows[0]["s16"].ToString(),           tblratio.Rows[0]["s17"].ToString(),
          tblratio.Rows[0]["s18"].ToString(),            tblratio.Rows[0]["s19"].ToString(),
           tblratio.Rows[0]["s20"].ToString(),            tblratio.Rows[0]["s21"].ToString(),
          tblratio.Rows[0]["s22"].ToString(),            tblratio.Rows[0]["s23"].ToString(),
          tblratio.Rows[0]["s24"].ToString(),            tblratio.Rows[0]["s25"].ToString(),
            tblratio.Rows[0]["s26"].ToString(),            tblratio.Rows[0]["s27"].ToString(),
           tblratio.Rows[0]["s28"].ToString(),          tblratio.Rows[0]["s29"].ToString(),
            tblratio.Rows[0]["s30"].ToString(),            tblratio.Rows[0]["s31"].ToString(),
           tblratio.Rows[0]["s32"].ToString(),           tblratio.Rows[0]["s33"].ToString(),
            tblratio.Rows[0]["s34"].ToString(),            tblratio.Rows[0]["s35"].ToString(),
           tblratio.Rows[0]["s36"].ToString(),            tblratio.Rows[0]["s37"].ToString(),
          tblratio.Rows[0]["s38"].ToString(),            tblratio.Rows[0]["s39"].ToString(),
         tblratio.Rows[0]["s40"].ToString(),

            };

                for (int i = 0; i < dtsizes.Rows.Count; i++)
                {
                    dtsizes.Rows[i]["lotqty"] = lottotalqty;
                    dtsizes.Rows[i]["itmqty"] = lotqty[i];
                }
                DataSet ds1 = new DataSet("ds1");
                ds1.Tables.Add(dtsizes);
                ds1.Tables[0].TableName = "tblratio";
                result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "UPDATE_ORDER_LOT_RATIO", ds1, null, null, mLCCode, dayid, styleid, colorid);

            }


            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error:" + ex.Message + "');", true);


            }
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";

                this.DdlSeason.Enabled = false;
                this.ddlmlccod.Enabled = false;
                this.ddlStyle.Enabled = false;
                this.LinkButtonSeePlan.Visible = true;
                this.LbtnOrderDetails.Visible = true;

                string comcod = GetCompCode();
                this.LblMessage.Visible = true;
                this.LblMessage.Text = "Note: LOT = Total Qty / Sum of Size Ratio";
                ViewState.Remove("tblOrderQty");
                string mMLCCOD = this.ddlmlccod.SelectedValue.ToString();
                string styleid = this.ddlStyle.SelectedValue.ToString().Substring(0, 12);
                string colorid = this.ddlStyle.SelectedValue.ToString().Substring(12, 12);
                string dayid = this.ddlStyle.SelectedValue.ToString().Substring(24, 8);
                this.OrderDeatilslink.Visible = true;
                this.seePlanlink.Visible = true;
                this.LbtnOrderDetails.NavigateUrl = "~/F_01_Mer/OrderDetails?Type=Entry&actcode=" + mMLCCOD;
                this.LinkButtonSeePlan.NavigateUrl = "~/F_05_ProShip/ExportPlanVsAchiv?Type=Entry&actcode="+ mMLCCOD + "&sircode="+ styleid+colorid+dayid;

                DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_ARTICLE_LOT_INFORMATION", mMLCCOD, styleid, colorid, dayid, "", "", "");

                if (ds1 == null)
                    return;
                ViewState["tblsizedesc"] = ds1.Tables[1];
                string mStyleID = "xxxxxxx";
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (ds1.Tables[0].Rows[i]["styleid"].ToString() == mStyleID)
                        ds1.Tables[0].Rows[i]["StyleDesc"] = " >> ";
                    mStyleID = ds1.Tables[0].Rows[i]["styleid"].ToString();
                }
                ViewState["tblOrderQty"] = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCatLot>().OrderBy(p => p.styleid).OrderBy(p => p.colorid).ToList();
                ViewState["tblOrderSize"] = ds1.Tables[1];
                ViewState["tblratio"] = ds1.Tables[2];

                for (int i = 5; i < 45; i++)
                    this.gv1.Columns[i].Visible = false;

                int indexx = 1;
                for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
                {

                    //  int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                    this.gv1.Columns[indexx + 4].Visible = true;
                    this.gv1.Columns[indexx + 4].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
                    indexx++;
                }
                this.gv1.EditIndex = -1;


                for (int i = 1; i < 41; i++)
                    this.gv1ratio.Columns[i].Visible = false;

                int indexy = 1;
                for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
                {

                    // int columid1 = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                    this.gv1ratio.Columns[indexy].Visible = true;
                    this.gv1ratio.Columns[indexy].HeaderText = ds1.Tables[1].Rows[i]["SizeDesc"].ToString().Trim();
                    indexy++;
                }
                this.gv1ratio.EditIndex = -1;

                this.Bind_Order_Allocation();

            }
            else
            {
                this.lbtnOk.Text = "Ok";

                this.DdlSeason.Enabled = true;
                this.ddlmlccod.Enabled = true;
                this.ddlStyle.Enabled = true;
                this.LinkButtonSeePlan.Visible = false;
                this.LbtnOrderDetails.Visible = false;

                this.ClearScreen();
            }

        }

        private void ClearScreen()
        {

            this.LblMessage.Visible = false;
            this.LblMessage.Text = "";

            this.gv1.DataSource = null;
            this.gv1.DataBind();

            this.gv1ratio.DataSource = null;
            this.gv1ratio.DataBind();
        }


        private void Bind_Order_Allocation()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> list = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];
            this.gv1.DataSource = HiddenSameData(list);
            this.gv1.DataBind();
            FooterCalLotList();
            this.OrderINput_Selection();

            DataTable dt = (DataTable)ViewState["tblratio"];
            this.gv1ratio.DataSource = dt;
            this.gv1ratio.DataBind();
        }
        private void OrderINput_Selection()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];

            if (lst2 == null || lst2.Count == 0)
                return;

            DataTable dt = ASITUtility03.ListToDataTable(lst2);

            for (int i = 0; i < this.gv1.Rows.Count; i++)
            {
                for (int j = 1; j <= 15; j++)
                {

                    if (dt.Rows[i]["ColorDesc"].ToString() != "")
                    {
                        ((TextBox)gv1.Rows[i].FindControl("txtgvF" + j + "")).Enabled = false;
                        ((TextBox)gv1.Rows[i].FindControl("lblgvColorDesc0")).Enabled = false;
                        ((LinkButton)gv1.Rows[i].FindControl("lbAddMore")).Visible = true;
                        ((DropDownList)gv1.Rows[i].FindControl("DdlLotlist")).Visible = false;

                    }
                    else
                    {
                        ((TextBox)gv1.Rows[i].FindControl("lblgvColorDesc0")).Visible = false;

                    }
                }
            }
        }
        private void FooterCal()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> list = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];

            if (list == null || list.Count == 0)
            {
                return;
            }

         ((Label)this.gv1.FooterRow.FindControl("FLblgvTotal")).Text = ((list.Sum(p => p.totalqty) == 0) ? 0 : list.Sum(p => p.totalqty)).ToString("#,##0;(#,##0); ");
            ((Label)this.gv1.FooterRow.FindControl("FLblgvColTotal")).Text = ((list.Sum(p => p.colqty) == 0) ? 0 : list.Sum(p => p.colqty)).ToString("#,##0;(#,##0); ");


        }
        private List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> HiddenSameData(List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst)
        {

            string styleid = "";
            string colorid = "";
            if (lst == null)
                return new List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>();

            foreach (SPEENTITY.C_01_Mer.GetOrderWithCatLot c1 in lst)
            {
                if (styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString())
                {
                    c1.description = "";
                    c1.buyerdesc = "";
                }

                styleid = c1.styleid.ToString();
                colorid = c1.colorid.ToString();

            }

            return lst;

        }
        private void GetLotList()
        {
            string comcod = GetCompCode();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "GET_SALGINF_INFORMATION", "28%", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            ViewState["tbllotlist"] = ds1.Tables[0];


        }
        protected void lbAddMore_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            string mlccod = this.ddlmlccod.SelectedValue.Trim().ToString();
            string mlcdesc = this.ddlmlccod.SelectedItem.ToString();
            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];
            string slnum = "000";
            //List<SPEENTITY.C_01_Mer.GetOrderWithCat> finallst =
            if (lst2.Count > 0)
            {
                slnum = (Convert.ToInt32(lst2.Max(p => p.slnum)) == 0) ? "001" : Convert.ToInt32(lst2.Max(p => p.slnum)).ToString();

            }
            string slnum1 = ASTUtility.Right("000" + (Convert.ToInt16(slnum) + 1), 3).ToString();

            DataTable dt = ASITUtility03.ListToDataTable(lst2);

            DataRow dr1 = dt.NewRow();
            dr1["buyerdesc"] = "";
            dr1["comcod"] = comcod;
            dr1["mlccod"] = mlccod;
            dr1["mlcdesc"] = mlcdesc;
            dr1["styleid"] = "000000000000";
            dr1["styledesc"] = "";
            dr1["colorid"] = "000000000000";
            dr1["description"] = "";
            dr1["colorDesc"] = "";
            dr1["styleunit"] = "";
            dr1["lotno"] = "00000";
            dr1["colqty"] = 0.00;
            dr1["slnum"] = slnum1;
            dr1["s1"] = 0.00;
            dr1["s2"] = 0.00;
            dr1["s3"] = 0.00;
            dr1["s4"] = 0.00;
            dr1["s5"] = 0.00;
            dr1["s6"] = 0.00;
            dr1["s7"] = 0.00;
            dr1["s8"] = 0.00;
            dr1["s9"] = 0.00;
            dr1["s10"] = 0.00;
            dr1["s11"] = 0.00;
            dr1["s12"] = 0.00;
            dr1["s13"] = 0.00;
            dr1["s14"] = 0.00;
            dr1["s15"] = 0.00;
            dr1["s16"] = 0.00;
            dr1["s17"] = 0.00;
            dr1["s18"] = 0.00;
            dr1["s19"] = 0.00;
            dr1["s20"] = 0.00;
            dr1["s21"] = 0.00;
            dr1["s22"] = 0.00;
            dr1["s23"] = 0.00;
            dr1["s24"] = 0.00;
            dr1["s25"] = 0.00;
            dr1["s26"] = 0.00;
            dr1["s27"] = 0.00;
            dr1["s28"] = 0.00;
            dr1["s29"] = 0.00;
            dr1["s30"] = 0.00;
            dr1["s31"] = 0.00;
            dr1["s32"] = 0.00;
            dr1["s33"] = 0.00;
            dr1["s34"] = 0.00;
            dr1["s35"] = 0.00;
            dr1["s36"] = 0.00;
            dr1["s37"] = 0.00;
            dr1["s38"] = 0.00;
            dr1["s39"] = 0.00;
            dr1["s40"] = 0.00;
            dr1["totalqty"] = 0.00;
            dt.Rows.Add(dr1);

            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst1 = dt.DataTableToList<SPEENTITY.C_01_Mer.GetOrderWithCatLot>();
            ViewState["tblOrderQty"] = lst1;
            this.Bind_Order_Allocation();
        }
        private void FooterCalLotList()
        {
            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];

            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> list = lst.FindAll(p => p.colordesc == "").ToList();

            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst2 = lst.FindAll(p => p.colordesc != "").ToList();

            if (lst == null || lst.Count == 0)
            {
                return;
            }
            ((Label)this.gv1.FooterRow.FindControl("flblgvF1")).Text = ((lst2.Sum(p => p.s1)) - (list.Sum(p => p.s1))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF2")).Text = ((lst2.Sum(p => p.s2)) - (list.Sum(p => p.s2))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF3")).Text = ((lst2.Sum(p => p.s3)) - (list.Sum(p => p.s3))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF4")).Text = ((lst2.Sum(p => p.s4)) - (list.Sum(p => p.s4))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF5")).Text = ((lst2.Sum(p => p.s5)) - (list.Sum(p => p.s5))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF6")).Text = ((lst2.Sum(p => p.s6)) - (list.Sum(p => p.s6))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF7")).Text = ((lst2.Sum(p => p.s7)) - (list.Sum(p => p.s7))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF8")).Text = ((lst2.Sum(p => p.s8)) - (list.Sum(p => p.s8))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF9")).Text = ((lst2.Sum(p => p.s9)) - (list.Sum(p => p.s9))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF10")).Text = ((lst2.Sum(p => p.s10)) - (list.Sum(p => p.s10))).ToString("#,##0;(#,##0);");

            ((Label)this.gv1.FooterRow.FindControl("flblgvF11")).Text = ((lst2.Sum(p => p.s11)) - (list.Sum(p => p.s11))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF12")).Text = ((lst2.Sum(p => p.s12)) - (list.Sum(p => p.s12))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF13")).Text = ((lst2.Sum(p => p.s13)) - (list.Sum(p => p.s13))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF14")).Text = ((lst2.Sum(p => p.s14)) - (list.Sum(p => p.s14))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF15")).Text = ((lst2.Sum(p => p.s15)) - (list.Sum(p => p.s15))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF16")).Text = ((lst2.Sum(p => p.s16)) - (list.Sum(p => p.s16))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF17")).Text = ((lst2.Sum(p => p.s17)) - (list.Sum(p => p.s17))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF18")).Text = ((lst2.Sum(p => p.s18)) - (list.Sum(p => p.s18))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF19")).Text = ((lst2.Sum(p => p.s19)) - (list.Sum(p => p.s19))).ToString("#,##0;(#,##0);");
            ((Label)this.gv1.FooterRow.FindControl("flblgvF20")).Text = ((lst2.Sum(p => p.s20)) - (list.Sum(p => p.s20))).ToString("#,##0;(#,##0);");



        }
        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable tbllot = (DataTable)ViewState["tbllotlist"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowIndex = e.Row.RowIndex;

                if (rowIndex == 0)
                {
                    LinkButton btnDel = (LinkButton)e.Row.FindControl("btnDel");
                    if (btnDel != null)
                    {
                        btnDel.Visible = false;
                    }
                }

                DropDownList DdlLotlist = (DropDownList)e.Row.FindControl("DdlLotlist");
                DdlLotlist.DataTextField = "gdesc";
                DdlLotlist.DataValueField = "gcod";
                DdlLotlist.DataSource = tbllot;
                DdlLotlist.DataBind();
                DdlLotlist.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lotno"));

            }
        }


        protected void lbtnPush_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            double lotqty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("TxtgvTotal1")).Text.Trim()));

            double s1 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF1")).Text.Trim()));
            double s2 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF2")).Text.Trim()));
            double s3 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF3")).Text.Trim()));
            double s4 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF4")).Text.Trim()));
            double s5 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF5")).Text.Trim()));
            double s6 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF6")).Text.Trim()));
            double s7 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF7")).Text.Trim()));
            double s8 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF8")).Text.Trim()));
            double s9 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF9")).Text.Trim()));
            double s10 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF10")).Text.Trim()));
            double s11 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF11")).Text.Trim()));
            double s12 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF12")).Text.Trim()));
            double s13 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF13")).Text.Trim()));
            double s14 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF14")).Text.Trim()));
            double s15 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF15")).Text.Trim()));
            double s16 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF16")).Text.Trim()));
            double s17 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF17")).Text.Trim()));
            double s18 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF18")).Text.Trim()));
            double s19 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF19")).Text.Trim()));
            double s20 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF20")).Text.Trim()));
            double s21 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF21")).Text.Trim()));
            double s22 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF22")).Text.Trim()));
            double s23 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF23")).Text.Trim()));
            double s24 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF24")).Text.Trim()));
            double s25 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF25")).Text.Trim()));
            double s26 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF26")).Text.Trim()));
            double s27 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF27")).Text.Trim()));
            double s28 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF28")).Text.Trim()));
            double s29 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF29")).Text.Trim()));
            double s30 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF30")).Text.Trim()));
            double s31 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF31")).Text.Trim()));
            double s32 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF32")).Text.Trim()));
            double s33 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF33")).Text.Trim()));
            double s34 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF34")).Text.Trim()));
            double s35 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF35")).Text.Trim()));
            double s36 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF36")).Text.Trim()));
            double s37 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF37")).Text.Trim()));
            double s38 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF38")).Text.Trim()));
            double s39 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF39")).Text.Trim()));
            double s40 = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)gv1ratio.Rows[index].FindControl("PtxtgvF40")).Text.Trim()));

            double totalratio = (s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12 + s13 + s14 + s15 + s16 + s17 + s18 + s19 + s20 +
                    s21 + s22 + s23 + s24 + s25 + s26 + s27 + s28 + s29 + s30 + s31 + s32 + s33 + s34 + s35 + s36 + s37 + s38 + s39 + s40);

            List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst2 = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];
            //List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> mainlist = lst2.FindAll(p=>p.colordesc!="").ToList();

            foreach (var item in lst2)
            {
                if (item.colordesc == "")
                {
                    item.s1 = (totalratio == 0) ? item.s1 : (s1 / totalratio) * lotqty;
                    item.s2 = (totalratio == 0) ? item.s2 : (s2 / totalratio) * lotqty;
                    item.s3 = (totalratio == 0) ? item.s3 : (s3 / totalratio) * lotqty;
                    item.s4 = (totalratio == 0) ? item.s4 : (s4 / totalratio) * lotqty;
                    item.s5 = (totalratio == 0) ? item.s5 : (s5 / totalratio) * lotqty;
                    item.s6 = (totalratio == 0) ? item.s6 : (s6 / totalratio) * lotqty;
                    item.s7 = (totalratio == 0) ? item.s7 : (s7 / totalratio) * lotqty;
                    item.s8 = (totalratio == 0) ? item.s8 : (s8 / totalratio) * lotqty;
                    item.s9 = (totalratio == 0) ? item.s9 : (s9 / totalratio) * lotqty;
                    item.s10 = (totalratio == 0) ? item.s10 : (s10 / totalratio) * lotqty;
                    item.s11 = (totalratio == 0) ? item.s11 : (s11 / totalratio) * lotqty;
                    item.s12 = (totalratio == 0) ? item.s12 : (s12 / totalratio) * lotqty;
                    item.s13 = (totalratio == 0) ? item.s13 : (s13 / totalratio) * lotqty;
                    item.s14 = (totalratio == 0) ? item.s14 : (s14 / totalratio) * lotqty;
                    item.s15 = (totalratio == 0) ? item.s15 : (s15 / totalratio) * lotqty;
                    item.s16 = (totalratio == 0) ? item.s16 : (s16 / totalratio) * lotqty;
                    item.s17 = (totalratio == 0) ? item.s17 : (s17 / totalratio) * lotqty;
                    item.s18 = (totalratio == 0) ? item.s18 : (s18 / totalratio) * lotqty;
                    item.s19 = (totalratio == 0) ? item.s19 : (s19 / totalratio) * lotqty;
                    item.s20 = (totalratio == 0) ? item.s20 : (s20 / totalratio) * lotqty;

                }
            }

            ViewState["tblOrderQty"] = lst2;

            DataTable tblratio = (DataTable)ViewState["tblratio"];
            tblratio.Rows[index]["s1"] = s1;
            tblratio.Rows[index]["s2"] = s2;
            tblratio.Rows[index]["s3"] = s3;
            tblratio.Rows[index]["s4"] = s4;
            tblratio.Rows[index]["s5"] = s5;
            tblratio.Rows[index]["s6"] = s6;
            tblratio.Rows[index]["s7"] = s7;
            tblratio.Rows[index]["s8"] = s8;
            tblratio.Rows[index]["s9"] = s9;
            tblratio.Rows[index]["s10"] = s10;
            tblratio.Rows[index]["s11"] = s11;
            tblratio.Rows[index]["s12"] = s12;
            tblratio.Rows[index]["s13"] = s13;
            tblratio.Rows[index]["s14"] = s14;
            tblratio.Rows[index]["s15"] = s15;
            tblratio.Rows[index]["s16"] = s16;
            tblratio.Rows[index]["s17"] = s17;
            tblratio.Rows[index]["s18"] = s18;
            tblratio.Rows[index]["s19"] = s19;
            tblratio.Rows[index]["s20"] = s20;
            tblratio.Rows[index]["s21"] = s21;
            tblratio.Rows[index]["s22"] = s22;
            tblratio.Rows[index]["s23"] = s23;
            tblratio.Rows[index]["s24"] = s24;
            tblratio.Rows[index]["s25"] = s25;
            tblratio.Rows[index]["s26"] = s26;
            tblratio.Rows[index]["s27"] = s27;
            tblratio.Rows[index]["s28"] = s28;
            tblratio.Rows[index]["s29"] = s29;
            tblratio.Rows[index]["s30"] = s30;
            tblratio.Rows[index]["totalqty"] = lotqty;
            ViewState["tblratio"] = tblratio;
            this.Bind_Order_Allocation();
        }



        protected void gv1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string comcod = GetCompCode();
                List<SPEENTITY.C_01_Mer.GetOrderWithCatLot> lst = (List<SPEENTITY.C_01_Mer.GetOrderWithCatLot>)ViewState["tblOrderQty"];

                string mMLCCOD = ddlmlccod.SelectedValue.ToString();
                string styleid = ddlStyle.SelectedValue.ToString().Substring(0, 12);
                string colorid = ddlStyle.SelectedValue.ToString().Substring(12, 12);
                string dayid = ddlStyle.SelectedValue.ToString().Substring(24, 8);

                string slnum = ((Label)this.gv1.Rows[e.RowIndex].FindControl("lblgvslnum")).Text.Trim();
                string lotno = ((DropDownList)this.gv1.Rows[e.RowIndex].FindControl("DdlLotlist")).Text.Trim();

                bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_PLANNING_INFO", "DELETE_ARTICLE_LOT_INFORMATION", mMLCCOD, styleid, colorid, dayid, lotno+slnum, "", "", "","");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Sorry!! This Lot already in Plan!');", true);
                    return;
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                }

                lst.RemoveAt(e.RowIndex);
                ViewState["tblOrderQty"] = lst;

                this.Bind_Order_Allocation();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "deleteError", "alert('Error: " + ex.Message + "');", true);
            }
        }

       
        
    }
}