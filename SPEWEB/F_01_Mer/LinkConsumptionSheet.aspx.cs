using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPELIB;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPEENTITY.C_22_Sal;

namespace SPEWEB.F_01_Mer
{
    public partial class LinkConsumptionSheet : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Merdata = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (prevPage.Length == 0)
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "PreCosting") ? "Pre-Costing Sheet- Common Materials" : "Consumption Sheet- Common Materials";

                CommonButton();
                this.GetInqnumber();
                this.GetProcess();
                //this.getCurList();
                //this.GetComponentList();
                //Select_View();
                if (type == "Entry" || type == "ConApp")
                {

                    // ((Label)this.gvCost.FooterRow.FindControl("lbltoalf")).Visible = false;

                }



            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).ValidationGroup = "gvSave";
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);


        }
        //private void Select_View()
        //{
        //    if (this.ChckVIew.Checked == true)
        //    {
        //        this.ProcessPanel.Visible = false;
        //        MultiView1.ActiveViewIndex = 1;
        //        this.ShowConsump();
        //    }
        //    else
        //    {
        //        this.ProcessPanel.Visible = true;
        //        MultiView1.ActiveViewIndex = 0;
        //    }

        //}
        public void CommonButton()
        {
            ((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
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
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetInqnumber()
        {
            string comcod = this.GetCompCode();
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");

            string season = "%%";
            string agent = "%%";
            string customer = "%%";
            string category = "%%";
            string samptype = "%%";

            //DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ_LIST", "01-Jan-1900", todate);
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ_LIST", season, agent, customer, category, samptype);

            DataView dv = ds1.Tables[0].DefaultView;
            //   dv.RowFilter = "inqno<>'" + this.Request.QueryString["actcode"].ToString() + "'";
            this.ddlPreList.DataTextField = "edarticle";
            this.ddlPreList.DataValueField = "inqnostyleid";
            this.ddlPreList.DataSource = ds1.Tables[0];// dv.ToTable(true, "inqno2", "inqno");
            this.ddlPreList.DataBind();


        }

        //private void getCurList()
        //{

        //    DataSet ds = lst.Curreny();
        //    var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
        //    ViewState["tblcur"] = lstConv;

        //    var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
        //    ViewState["tblcurdesc"] = lstCurryDesc;
        //    this.ddlCurList.DataValueField = "curcode";
        //    this.ddlCurList.DataTextField = "curdesc";
        //    this.ddlCurList.DataSource = lstCurryDesc;
        //    this.ddlCurList.DataBind();
        //    ddlCurrency_SelectedIndexChanged(null, null);
        //}
        //protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string fcode = "001";
        //    string tcode = this.ddlCurList.SelectedValue.ToString();
        //    List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

        //    double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;


        //    this.txtExchngerate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
        //    //double txtpeople = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBudgeted.Rows[j].FindControl("txtpeople")).Text.Trim()));

        //}
        private void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        private void lnkPrint_Click(object sender, EventArgs e)
        {
            this.ChckVIew.Checked = true;
            this.ChckVIew_CheckedChanged(null, null);

            string type = this.Request.QueryString["Type"].ToString();

            if (type == "PreCosting")
            {

                this.CostingComonMeterial();
            }
            else
            {
                this.ConsumptionMeterial();
            }



        }



        private void CostingComonMeterial()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string inqno = this.Request.QueryString["actcode"].ToString(); //this.ddlStyle.SelectedValue.ToString();




            string artno = "";// this.txtArtno.Text.Trim().ToString();
            string color = "";// this.txtcolor.Text.Trim().ToString();
            string range = "";// this.txtsizernge.Text.Trim().ToString();
            string size = "";// this.txtconsize.Text.Trim().ToString();
            string qty = "";// this.txtordqty.Text.Trim().ToString();
            string date = "";//this.txtDatefrom.Text.Trim().ToString();
            string buyer = "";// this.txtbuyer.Text.Trim().ToString();
            string mrsendizer = "";// this.txtmerchand.Text.Trim().ToString();
            string catgory = "";// this.txtCategory.Text.Trim().ToString();
            string lstref = "";// this.txtlastrefno.Text.Trim().ToString();
            string construction = "";// this.txtconstruction.Text.Trim().ToString();
            string smploptionno = "";// this.txtsampleno.Text.Trim().ToString();
            string season = "";//this.txtseason.Text.Trim().ToString();
            string brndname = "";// this.txtbrand.Text.Trim().ToString();
            string estdate = "";// this.txtestproduction.Text.Trim().ToString();


            string tarrate = "";// this.txttarprice.Text.Trim().ToString();
            string offer = "";// this.txtoffprice.Text.Trim().ToString();
            string confirm = "";// this.txtconfrmprice.Text.Trim().ToString();
            string currency = "";//this.ddlCurList.SelectedItem.Text.Trim().ToString();


            DataTable dt = (DataTable)ViewState["tblcomItm"];


            DataTable dtcommon = (DataTable)ViewState["CommonCost"];


            if (dt == null)
            {
                return;
            }

            var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.CommonMterailsCal>();
            //var lst1 = dtcommon.DataTableToList<SPEENTITY.C_01_Mer.EclassCommonCost>();


            double tAmt = lst.Select(p => p.amt).Sum();
            //double tAmtc = lst1.Select(p => p.amt).Sum();

            string artamount = Convert.ToDouble(tAmt).ToString("#,##0.00;(#,##0.00); ");


            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptCommonCostingSheet", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));

            rpt1.SetParameters(new ReportParameter("artno", artno));
            rpt1.SetParameters(new ReportParameter("color", color));
            rpt1.SetParameters(new ReportParameter("range", range));
            rpt1.SetParameters(new ReportParameter("size", size));
            rpt1.SetParameters(new ReportParameter("qty", qty));
            rpt1.SetParameters(new ReportParameter("date", "DATE " + date));
            rpt1.SetParameters(new ReportParameter("buyer", buyer));
            rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
            rpt1.SetParameters(new ReportParameter("catgory", catgory));
            rpt1.SetParameters(new ReportParameter("lstref", lstref));
            rpt1.SetParameters(new ReportParameter("construction", construction));
            rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
            rpt1.SetParameters(new ReportParameter("season", season));
            rpt1.SetParameters(new ReportParameter("brndname", brndname));
            rpt1.SetParameters(new ReportParameter("estdate", estdate));

            //rpt1.SetParameters(new ReportParameter("tarrate", tarrate));
            //rpt1.SetParameters(new ReportParameter("offer", offer));
            //rpt1.SetParameters(new ReportParameter("confirm", confirm));
            //rpt1.SetParameters(new ReportParameter("currency", currency));
            //rpt1.SetParameters(new ReportParameter("artamount", artamount));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Common Material Pre-Costing"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void ConsumptionMeterial()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string inqno = this.Request.QueryString["actcode"].ToString(); //this.ddlStyle.SelectedValue.ToString();
            string artno = "";// this.txtArtno.Text.Trim().ToString();
            string color = "";// this.txtcolor.Text.Trim().ToString();
            string range = "";// this.txtsizernge.Text.Trim().ToString();
            string size = "";// this.txtconsize.Text.Trim().ToString();
            string qty = "";// this.txtordqty.Text.Trim().ToString();
            string date = "";//this.txtDatefrom.Text.Trim().ToString();
            string buyer = "";// this.txtbuyer.Text.Trim().ToString();
            string mrsendizer = "";// this.txtmerchand.Text.Trim().ToString();
            string catgory = "";// this.txtCategory.Text.Trim().ToString();
            string lstref = "";// this.txtlastrefno.Text.Trim().ToString();
            string construction = "";// this.txtconstruction.Text.Trim().ToString();
            string smploptionno = "";// this.txtsampleno.Text.Trim().ToString();
            string season = "";//this.txtseason.Text.Trim().ToString();
            string brndname = "";// this.txtbrand.Text.Trim().ToString();
            string estdate = "";// this.txtestproduction.Text.Trim().ToString();

            string exrate = "";//this.txtExchngerate.Text.Trim().ToString();
            string tarrate = "";// this.txttarprice.Text.Trim().ToString();
            string offer = "";// this.txtoffprice.Text.Trim().ToString();
            string confirm = "";// this.txtconfrmprice.Text.Trim().ToString();
            string currency = "";//this.ddlCurList.SelectedItem.Text.Trim().ToString();


            DataTable dt = (DataTable)ViewState["tblcomItm"];


            //DataTable dtcommon = (DataTable)ViewState["CommonCost"];


            if (dt == null)
            {
                return;
            }

            var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.CommonMterailsCal>();
            //var lst1 = dtcommon.DataTableToList<SPEENTITY.C_01_Mer.EclassCommonCost>();


            double tAmt = lst.Select(p => p.amt).Sum();
            //double tAmtc = lst1.Select(p => p.amt).Sum();

            string artamount = Convert.ToDouble(tAmt).ToString("#,##0.00;(#,##0.00); ");


            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptCommonConsSheet", lst, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));

            rpt1.SetParameters(new ReportParameter("artno", artno));
            rpt1.SetParameters(new ReportParameter("color", color));
            rpt1.SetParameters(new ReportParameter("range", range));
            rpt1.SetParameters(new ReportParameter("size", size));
            rpt1.SetParameters(new ReportParameter("qty", qty));
            rpt1.SetParameters(new ReportParameter("date", "DATE " + date));
            rpt1.SetParameters(new ReportParameter("buyer", buyer));
            rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
            rpt1.SetParameters(new ReportParameter("catgory", catgory));
            rpt1.SetParameters(new ReportParameter("lstref", lstref));
            rpt1.SetParameters(new ReportParameter("construction", construction));
            rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
            rpt1.SetParameters(new ReportParameter("season", season));
            rpt1.SetParameters(new ReportParameter("brndname", brndname));
            rpt1.SetParameters(new ReportParameter("estdate", estdate));

            //rpt1.SetParameters(new ReportParameter("tarrate", tarrate));
            //rpt1.SetParameters(new ReportParameter("offer", offer));
            //rpt1.SetParameters(new ReportParameter("confirm", confirm));
            //rpt1.SetParameters(new ReportParameter("currency", currency));
            //rpt1.SetParameters(new ReportParameter("artamount", artamount));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Common Material  Consumption"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void lnkbtnSave_Click(object sender, EventArgs e)
        {

            Page.Validate();
            if (!Page.IsValid)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Validation Error!!!";
                return;
            }
            this.UpdateCost();
        }





        protected void GetProcess()
        {

            string comcod = this.GetCompCode();
            string filter = "%";
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_INV_STDANA", "GETPROCESSCODE", filter, "", "", "", "", "", "", "", "");
            this.ddlProcess.DataSource = ds3.Tables[0];
            this.ddlProcess.DataTextField = "resdesc";
            this.ddlProcess.DataValueField = "rescode";
            this.ddlProcess.DataBind();



            ViewState["tblcodeType"] = ds3.Tables[0];
            ds3.Dispose();
            this.ddlProcess_SelectedIndexChanged(null, null);
            //this.imgbtnResourceCost_Click(null, null);
        }

        protected void ddlProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgbtnResourceCost_Click(null, null);
            //this.Image2.Visible = false;
            //this.Image3.Visible = false;

            this.ShowConsump();

        }
        protected void imgbtnResourceCost_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblres");
            string comcod = this.GetCompCode();
            string filter = "%";

            string rescode = this.ddlProcess.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblcodeType"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rescode='" + rescode + "'");
            dt = dv.ToTable();
            string Codetype = dt.Rows[0]["acttype"].ToString();
            string SearchInfo = "";
            if (Codetype.Length > 0)
            {

                int len;
                string[] ar = Codetype.Split('/');
                foreach (string ar1 in ar)
                {


                    if (ar1.Contains("-"))
                    {
                        len = ar1.IndexOf("-");
                        SearchInfo = SearchInfo + "left(sircode,'" + len + "') between " + ar1.Trim().Replace("-", " and ") + " ";
                    }
                    else
                    {
                        len = ar1.Length;

                        SearchInfo = SearchInfo + "left(sircode,'" + len + "')" + " = " + ar1 + " ";
                    }
                    SearchInfo = SearchInfo + " or ";

                }
                if (SearchInfo.Length > 0)
                    SearchInfo = "(" + SearchInfo.Substring(0, SearchInfo.Length - 3) + ")";
            }


            SearchInfo = (SearchInfo.Length == 0) ? "left(sircode, 2) between 01 and 06 " : SearchInfo;




            // DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_INV_STDANA", "GETRESCODE_02", "(left(sircode,'4') = 0416)", "", "", "", "", "", "", "", "");
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_FOR_CONSUMPTION", "%", "COMMON", "", "", "", "", "", "", "");

            this.ddlResourcesCost.DataSource = ds3.Tables[0];
            this.ddlResourcesCost.DataTextField = "sirdesc1";
            this.ddlResourcesCost.DataValueField = "sircode1";
            this.ddlResourcesCost.DataBind();




            ViewState["tblresRes"] = ds3.Tables[0];
            // ViewState["tblSpcf"] = ds3.Tables[1];
            ds3.Dispose();
            // this.ddlResourcesCost_SelectedIndexChanged(null, null);

        }
        protected void ddlResourcesCost_SelectedIndexChanged(object sender, EventArgs e)
        {

            string mResCode = this.ddlResourcesCost.SelectedValue.ToString();
            this.ddlSpcfcode.Items.Clear();
            DataTable tbl1 = (DataTable)ViewState["tblSpcf"];
            DataView dv1 = tbl1.DefaultView;
            dv1.RowFilter = "rsircode = '" + mResCode + "' or spcfcod = '000000000000'";
            DataTable dt = dv1.ToTable();

            if (dt.Rows.Count > 1)
            {
                dt.Rows[0].Delete();
            }


            this.ddlSpcfcode.DataTextField = "spcfdesc";
            this.ddlSpcfcode.DataValueField = "spcfcod";
            this.ddlSpcfcode.DataSource = dt;
            this.ddlSpcfcode.DataBind();



        }



        private void ShowConsump()
        {
            ViewState.Remove("tblcomItm");
            string comcod = this.GetCompCode();
            //string lccode = this.ddlinqno.SelectedValue.ToString();
            string processcode = "%";
            if (this.ChckVIew.Checked == false)
            {
                processcode = this.ddlProcess.SelectedValue.ToString() + "%";
            }
            string inqno = this.Request.QueryString["actcode"].ToString();
            string prodcode = this.Request.QueryString["genno"].ToString();

            //DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_COMM_MATERIALS", prodcode, processcode, lccode, "", "", "", "");
            // DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_COMM_MATERIALS", inqno, styleid, "", "", "", "", "", "", "");
            // ViewState["tblcomItm"] = ds1.Tables[0];

            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_COMM_MATERIALS", inqno, prodcode, processcode, "", "", "", "");
            if (ds4 == null)
            {

                return;
            }
            ViewState["tblcomItm"] = ds4.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblcomItm"];
            if (dt == null || dt.Rows.Count == 0)
            {
                this.gvCost.DataSource = null;
                this.gvCost.DataBind();
                return;
            }

            //DataTable commoncost = (DataTable)ViewState["CommonCost"];
            if (this.ChckVIew.Checked == false)
            {
                if (this.Request.QueryString["Type"].ToString() == "Entry")
                {

                    this.gvCost.Columns[11].Visible = false;
                    this.gvCost.Columns[12].Visible = false;
                    this.gvCost.Columns[13].Visible = false;
                    this.gvCost.Columns[14].Visible = false;
                }


                this.gvCost.DataSource = HiddenSameValue(dt);
                this.gvCost.DataBind();
                Session["Report1"] = gvCost;
                ((HyperLink)this.gvCost.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                this.FooterCalculation(dt);
            }
            else
            {
                if (this.Request.QueryString["Type"].ToString() == "Entry")
                {
                    this.gvRtpcon.Columns[10].Visible = false;
                    this.gvRtpcon.Columns[11].Visible = false;
                    this.gvRtpcon.Columns[12].Visible = false;
                    this.gvRtpcon.Columns[13].Visible = false;
                }

                this.gvRtpcon.DataSource = HiddenSameValue(dt);
                this.gvRtpcon.DataBind();
                Session["Report1"] = gvRtpcon;
                ((HyperLink)this.gvRtpcon.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                this.FooterCalculation(dt);
            }

        }
        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            if (this.ChckVIew.Checked == false)
            {
                ((Label)this.gvCost.FooterRow.FindControl("lblgvBdamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(convrate)", "")) ? 0.00 : dt.Compute("sum(convrate)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvCost.FooterRow.FindControl("lblgvfamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvCost.FooterRow.FindControl("lblgvfper")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.00;(#,##0.00); ") + (Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))) > 0 ? "%" : "");
                //((Label)this.gvCost.FooterRow.FindControl("lblgvttlqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }
            else
            {


                ((Label)this.gvRtpcon.FooterRow.FindControl("lblgvBdamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(convrate)", "")) ? 0.00 : dt.Compute("sum(convrate)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvRtpcon.FooterRow.FindControl("lblgvfamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                ((Label)this.gvRtpcon.FooterRow.FindControl("lblgvfper")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.00;(#,##0.00); ") + (Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))) > 0 ? "%" : "");
                //((Label)this.gvRtpcon.FooterRow.FindControl("lblgvttlqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }
        }



        protected void lnkAddResouctCost_Click(object sender, EventArgs e)
        {
            DataTable tbl2 = (DataTable)ViewState["tblcomItm"];
            string processcode = this.ddlProcess.SelectedValue.ToString();
            string processdesc = this.ddlProcess.SelectedItem.ToString();
            string rescod = this.ddlResourcesCost.SelectedValue.ToString().Substring(0, 12);
            string colorid = "000000000000";
            string sizeid = "000000000000";
            string Specification = this.ddlResourcesCost.SelectedValue.ToString().Substring(12, 12);
            string compcode = "000000000000";
            string compname = "";



            DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and spcfcode='" + Specification + "' and colorid='" + colorid + "' and sizeid='" + sizeid + "' and compcode='" + compcode + "'");
            // DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and  compcode='" + compcode + "'" + "' and spcfcode='" + Specification + "'");
            if (dr.Length > 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + "Selected Component Already Added";

                return;
            }
            else
            {
                DataRow dr1 = tbl2.NewRow();
                dr1["procode"] = processcode;
                dr1["prodesc"] = processdesc;
                dr1["rescode"] = rescod;
                dr1["resdesc"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["sirtdes"].ToString();
                dr1["resunit"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["sirunit"].ToString();
                dr1["conqty"] = 0;
                dr1["westpc"] = 0;
                dr1["qty"] = 0;
                dr1["rate"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["rate"].ToString();
                dr1["amt"] = 0;
                dr1["convrate"] = 0;
                dr1["percnt"] = 0;
                dr1["colorid"] = colorid;
                dr1["sizeid"] = sizeid;
                // dr1["rawmatname"] = this.ddlResourcesCost.SelectedItem.ToString().Substring(13);
                dr1["spcfcode"] = Specification;
                dr1["spcfdesc"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["sirdesc"].ToString();
                dr1["compname"] = compname;
                dr1["compcode"] = compcode;
                tbl2.Rows.Add(dr1);
            }
            //    }
            //}




            ViewState["tblcomItm"] = tbl2;
            this.Data_Bind();
        }
        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["tblcomItm"];
            double qty, rate, amt, convRate;


            double convrate1 = 0.00; //Convert.ToDouble("0" + ((TextBox)txtExchngerate).Text.Trim());

            for (int i = 0; i < this.gvCost.Rows.Count; i++)
            {
                string resdesc = ((Label)this.gvCost.Rows[i].FindControl("lblgvDesc")).Text.Trim();
                string resunit = ((Label)this.gvCost.Rows[i].FindControl("txtgvunit0")).Text.Trim();

                double conqty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvconqty")).Text.Trim());
                double wper = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvwestpc")).Text.Trim());

                double netQty = conqty + (conqty * (wper / 100));

                ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text = netQty.ToString("#,##0.000000;(#,##0.000000); ");

                qty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text.Trim());
                rate = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqrateCost")).Text.Trim());
                double amtgrid = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvamtCost")).Text.Trim());
                amt = qty * rate;
                convRate = qty * convrate1;
                dt.Rows[i]["resdesc"] = resdesc;
                dt.Rows[i]["resunit"] = resunit;
                dt.Rows[i]["conqty"] = conqty;
                dt.Rows[i]["westpc"] = wper;
                dt.Rows[i]["qty"] = qty;
                dt.Rows[i]["rate"] = rate;
                dt.Rows[i]["convrate"] = convRate;


                dt.Rows[i]["amt"] = (qty * rate > 0) ? amt : amtgrid;
            }

            double netqty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", "")));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["percnt"] = (netqty == 0) ? 0.00 : (Convert.ToDouble(dt.Rows[i]["qty"].ToString()) * 100) / netqty;

            }



            ViewState["tblcomItm"] = dt;



        }
        private void UpdateCost()
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblcomItm"];




            string prodcode = this.Request.QueryString["genno"];
            string proscode = this.ddlProcess.SelectedValue.ToString();
            string inqno = this.Request.QueryString["actcode"];
            double convrate1 = 0.00;// Convert.ToDouble("0" + ((TextBox)txtExchngerate).Text.Trim());




            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string procode = dt.Rows[i]["procode"].ToString();
                string rescod = dt.Rows[i]["rescode"].ToString();
                string resdesc = dt.Rows[i]["resdesc"].ToString();
                string runit = dt.Rows[i]["resunit"].ToString();
                string resqty = dt.Rows[i]["qty"].ToString();
                string resamt = dt.Rows[i]["amt"].ToString();
                string percnt = dt.Rows[i]["percnt"].ToString();
                string rate = dt.Rows[i]["rate"].ToString();
                string conqty = dt.Rows[i]["conqty"].ToString();
                string westpc = dt.Rows[i]["westpc"].ToString();
                string comptCode = dt.Rows[i]["compcode"].ToString();
                string spcfcode = dt.Rows[i]["spcfcode"].ToString();
                string colorid = dt.Rows[i]["colorid"].ToString();
                string sizeid = dt.Rows[i]["sizeid"].ToString();

                if (percnt == "")
                    percnt = "0.0";
                bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "CONSUMPTION_UPDATE", "STDCINF2C", prodcode, procode, rescod, resqty, resamt, percnt, inqno, "", colorid, sizeid, "", conqty, westpc, spcfcode, rate, "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + Merdata.ErrorObject["Msg"].ToString();

                    return;
                }

                ((Label)this.Master.FindControl("lblmsg")).Text = " Update Successfully.";

            }



        }
        private string XmlDataInsert(string Inqno, string Styleid, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");

            ds.Tables[0].Columns.Add("delbyid", typeof(string));
            ds.Tables[0].Columns.Add("delseson", typeof(string));
            ds.Tables[0].Columns.Add("deldate", typeof(DateTime));

            ds.Tables[0].Rows[0]["delbyid"] = usrid;
            ds.Tables[0].Rows[0]["delseson"] = session;
            ds.Tables[0].Rows[0]["deldate"] = Date;


            ds1.Merge(ds.Tables[0]);
            //s      ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            //  ds1.Tables[1].TableName = "tbl2";

            bool resulta = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Inqno, Styleid);

            if (!resulta)
            {

                //return;
            }
            else
            {
                //   ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                //   ((Label)this.Master.FindControl("lblANMgsBox")).Text = "Successfully Deleted";
                //   ((Label)this.Master.FindControl("lblANMgsBox")).Attributes["style"] = "background:Green;";

                //   ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

            return "";
        }
        protected void gvCost_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblcomItm"];
            string Inqno = this.Request.QueryString["actcode"];
            string prodcode = this.Request.QueryString["genno"];
            string proscode = this.ddlProcess.SelectedValue.ToString();

            string rescod = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcodeCost")).Text.Trim();
            string spcfcode = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvspcfcode")).Text.Trim();
            string compcode = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcompcode")).Text.Trim();
            string colorid = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcolor")).Text.Trim();
            string sizeid = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvsize")).Text.Trim();

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);

            this.XmlDataInsert(Inqno, prodcode, ds1);

            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "DELETE_RESOURCE2", prodcode, proscode, rescod, colorid, sizeid, compcode, spcfcode, Inqno, "", "", "", "", "", "", "");
            if (!result)
                return;
            int index = (this.gvCost.PageIndex) * this.gvCost.PageSize + e.RowIndex;
            dt.Rows[index].Delete();
            ViewState["tblcomItm"] = dt;
            this.Data_Bind();
        }

        protected void ChckVIew_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChckVIew.Checked == true)
            {
                this.ProcessPanel.Visible = false;
                this.pnlReport.Visible = true;

                this.ShowConsump();
            }
            else
            {
                this.ProcessPanel.Visible = true;
                this.pnlReport.Visible = false;

            }
        }
        protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (this.Request.QueryString["Type"].ToString() == "PreCosting")
                {
                    ((TextBox)e.Row.FindControl("txtgvconqty")).Enabled = false;
                    //  ((TextBox)e.Row.FindControl("txtgvwestpc")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtgvqtyCost")).Enabled = false;
                }
                else
                {
                    ((TextBox)e.Row.FindControl("txtgvconqty")).Enabled = true;
                    //   ((TextBox)e.Row.FindControl("txtgvwestpc")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtgvqtyCost")).Enabled = true;
                }
            }
        }
        private DataTable HiddenSameValue(DataTable dt)
        {

            string procode = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (procode == dt.Rows[i]["procode"].ToString())
                {
                    dt.Rows[i]["prodesc"] = "";
                }
                procode = dt.Rows[i]["procode"].ToString();
            }
            // ViewState["tblcomItm"] = dt;
            return dt;
        }


        protected void ChckCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChckCopy.Checked == true)
            {
                this.ddlPreList.Visible = true;
                //  this.ddlArticle.Visible = true;
                this.LbtnCopy.Visible = true;
                ddlPreList_SelectedIndexChanged(null, null);
            }
            else
            {
                this.ddlPreList.Visible = false;
                //  this.ddlArticle.Visible = false;
                this.LbtnCopy.Visible = false;
            }

        }

        protected void ddlPreList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string comcod = this.GetCompCode();
            //string inqno = this.ddlPreList.SelectedValue.ToString();
            //DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqno, "", "", "", "", "", "", "");
            //if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            //{
            //    return;
            //}

            //this.ddlArticle.DataTextField = "styledesc1";
            //this.ddlArticle.DataValueField = "styleid";
            //this.ddlArticle.DataSource = ds1.Tables[0];
            //this.ddlArticle.DataBind();
        }
        protected void LbtnCopy_Click(object sender, EventArgs e)
        {
            string frinqno = this.ddlPreList.SelectedValue.ToString().Substring(0, 14);
            string frstyleid = this.ddlPreList.SelectedValue.ToString().Substring(14, 12);
            string toinqno = this.Request.QueryString["actcode"].ToString();
            string tostyleid = this.Request.QueryString["genno"].ToString();
            string comcod = this.GetCompCode();
            if (frinqno == toinqno && frstyleid == tostyleid)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You Select Same Style";
                return;
            }

            bool result = Merdata.UpdateTransInfo1(comcod, "SP_ENTRY_CONSUMPTION", "COPY_COMMON_MATERIAL_CONSUMPTION", frinqno, frstyleid, toinqno, tostyleid);
            if (result == true)
            {
                this.ShowConsump();
                ((Label)this.Master.FindControl("lblmsg")).Text = "Copy Common Material Successfully";
            }


        }

    }
}