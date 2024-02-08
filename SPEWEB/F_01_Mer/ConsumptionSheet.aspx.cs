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
using AjaxControlToolkit;
using System.IO;

namespace SPEWEB.F_01_Mer
{
    public partial class ConsumptionSheet : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess Merdata = new ProcessAccess();
        SalesInvoice_BL lst = new SalesInvoice_BL();
        Common CommonClass = new Common();

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
                string type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "PreCosting") ? "CBD Sheet" : "Consumption Sheet";
                this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtestproduction.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                CommonButton();
                this.GetInqnumber();
                this.getCurList();
                this.GetComponentList();

                Select_View();
                if (type == "Entry" || type == "ConApp")
                {
                    //this.DirectCost.Visible = false;
                    //((Label)this.gvCost.FooterRow.FindControl("lbltoalf")).Visible = false;

                    this.panelCosting.Visible = false;
                }
                else
                {
                    //this.DirectCost.Visible = true;
                    this.panelCosting.Visible = true;
                }


            }
            
            this.lnkbtnAdd_Click(null, null);
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Click += new EventHandler(Con_Cost_Approved);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Click += new EventHandler(lnkbtnAdd_Click);
        }



        private void Select_View()
        {

            if (this.ChckVIew.Checked == true)
            {

                this.ProcessPanel.Visible = false;
                MultiView1.ActiveViewIndex = 1;
                
                this.ShowConsump();
            }
            else
            {
                this.ShowConsump();
                this.ProcessPanel.Visible = true;
                MultiView1.ActiveViewIndex = 0;
            }
            if (GetCompCode()=="5305")
            {
                this.pnlSmpleSizes.Visible = true;
            }
        }
        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            if (this.Request.QueryString["Type"] == "ConApp" || this.Request.QueryString["Type"] == "PreCostingApp")
            {

                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = true;
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Text = "Approve";
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).OnClientClick = "return confirm('Do You want to Approve?')";
            }
            else
            {
                ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;

            }


            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            if (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp")
            {
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Text = "Refresh";
                ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = true;
            }


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
            this.ddlinqno.DataTextField = "inqno2";
            this.ddlinqno.DataValueField = "inqno";
            this.ddlinqno.DataSource = ds1.Tables[0];
            this.ddlinqno.DataBind();
            DataView dv = ds1.Tables[0].DefaultView;
            //dv.RowFilter = "inqno<>'" + this.Request.QueryString["actcode"].ToString() + "'";
            this.ddlPreList.DataTextField = "edarticle";
            this.ddlPreList.DataValueField = "inqnostyleid";
            this.ddlPreList.DataSource = ds1.Tables[0];// dv.ToTable(true, "inqno2", "inqno");
            this.ddlPreList.DataBind();
            if (this.Request.QueryString["actcode"].Length > 0)
            {
                this.ddlinqno.SelectedValue = this.Request.QueryString["actcode"].ToString();
            }
            ddlinqno_SelectedIndexChanged(null, null);


        }
        private void getCurList()
        {

            DataSet ds = lst.Curreny();
            var lstConv = ds.Tables[0].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>();
            ViewState["tblcur"] = lstConv;

            var lstCurryDesc = ds.Tables[1].DataTableToList<SPEENTITY.C_22_Sal.Sales_BO.Currencyinf>();
            ViewState["tblcurdesc"] = lstCurryDesc;
            this.ddlCurList.DataValueField = "curcode";
            this.ddlCurList.DataTextField = "curdesc";
            this.ddlCurList.DataSource = lstCurryDesc;
            this.ddlCurList.DataBind();
            ddlCurrency_SelectedIndexChanged(null, null);
        }
        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fcode = "001";
            string tcode = this.ddlCurList.SelectedValue.ToString();
            List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf> lst1 = (List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"];

            double method = (((List<SPEENTITY.C_22_Sal.Sales_BO.ConvInf>)ViewState["tblcur"]).FindAll(p => p.fcode == fcode && p.tcode == tcode))[0].conrate;


            this.txtExchngerate.Text = Convert.ToDouble("0" + method).ToString("#,##0.000000 ;-#,##0.000000; ");
            double target = Convert.ToDouble("0" + this.txttarprice.Text.Trim());
            double offer = Convert.ToDouble("0" + this.txtoffprice.Text.Trim());
            double confirm = Convert.ToDouble("0" + this.txtconfrmprice.Text.Trim());
            this.txttarprice.Text = Convert.ToDouble("0" + target).ToString("#,##0.000000 ;-#,##0.000000; ");
            this.txtoffprice.Text = Convert.ToDouble("0" + offer).ToString("#,##0.000000 ;-#,##0.000000; ");
            this.txtconfrmprice.Text = Convert.ToDouble("0" + confirm).ToString("#,##0.000000 ;-#,##0.000000; ");
            //double txtpeople = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBudgeted.Rows[j].FindControl("txtpeople")).Text.Trim()));

        }
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
                switch (GetCompCode())
                {
                    case "5306":
                    case "5305":
                        PrintConsumptionSheetFB();
                        break;
                    default:
                        this.PrintPreCostingSheet();
                        break;
                }
                
            }
            else
            {
                this.PrintConsumptionSheet();
            }
           
        }
        private void PrintConsumptionSheetFB()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string todate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string qinqno = this.Request.QueryString["sdino"].ToString();
            string srchinqno = (qinqno.Length > 0 ? qinqno : "") + "%";
            DataSet ds = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETSAMPLEINQNO", todate, srchinqno);

            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETSAMPLEINQINFO", ds.Tables[0].Rows[0]["sampleid"].ToString(), "",
                          "", "", "", "", "", "", "");

            var lst1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>();
            string url = lst1[0].images.ToString();
            string image = "";

            if (url.Length > 0)
            {
                image = new Uri(Server.MapPath(url)).AbsoluteUri;
            }
            else
            {
                image = new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
            }

            string artno = lst1[0].article.Trim(); ;
            string color = lst1[0].colordesc;
            string range = lst1[0].sizerange;
            string size = lst1[0].samsize.ToString();
            string qty = "";

            string buyer = lst1[0].buyerdesc;
            string mrsendizer = "";
            string catgory = lst1[0].catagorydesc;
            string lstref = "";
            string construction = lst1[0].construction;
            string smploptionno = "";
            string season = lst1[0].seasondesc;
            string brndname = "";
            string estdate = "";
            string costrang = lst1[0].comsize;
            string ordrqty = "";
            string technician = "";
            string lastmold = lst1[0].lformadesc;
            string samptype = lst1[0].samtypedesc;
            string forma = lst1[0].lformadesc;


            string style = lst1[0].catagorydesc;
            string Type = this.Request.QueryString["Type"].ToString();

            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_PROTO_SAMPLING", "GETCONSUMPTIONINFO", ds.Tables[0].Rows[0]["sampleid"].ToString(), "", "", "", "", "", "");

            if (ds4 == null)
            {
                return;
            }

            string date = (ds4.Tables[0].Rows.Count == 0) ? System.DateTime.Today.ToString("dd-MMM-yyyy") : Convert.ToDateTime(ds4.Tables[0].Rows[0]["csumpdat"]).ToString("dd-MMM-yyyy");
            var lst = ds4.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionFB>();
            var lstCost = ds4.Tables[2].DataTableToList<SPEENTITY.C_01_Mer.EclassCommonCostSam>();
            var curinfo = ds4.Tables[1].DataTableToList<SPEENTITY.C_01_Mer.EclassCurinfo>();
            DataView dv = ds4.Tables[2].Copy().DefaultView;
            dv.RowFilter = "sircode not like '049800102999%' and sircode not like '049800104999%'";
            double totlcst = Convert.ToDouble((Convert.IsDBNull(ds4.Tables[0].Compute("sum(amt)", "")) ? 0.00 : ds4.Tables[0].Compute("sum(amt)", "")));
            double tltcommncost = Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("sum(stdamt)", "")) ? 0.00 : dv.ToTable().Compute("sum(stdamt)", "")));
            string totalamt = Convert.ToDouble(totlcst + tltcommncost).ToString("#,##0.0000;(#,##0.0000);");
            string exchnga = curinfo.Select(x => x.exratea).SingleOrDefault().ToString();
            string exchngb = curinfo.Select(a => a.exrateb).SingleOrDefault().ToString();
            string conrate = curinfo.Select(a => a.conrate).SingleOrDefault().ToString();
            string curdesc = curinfo.Select(a => a.codedesc).SingleOrDefault().ToString();
            //var lstother = (List<SPEENTITY.C_04_Sampling.EClassPrototypeSampling.PrototypeSampling>)(ViewState["tblinquery"]);
            //string samptype = lstother[0].samtypedesc;
            //string forma = lstother[0].lformadesc;
            //season = lstother[0].season;
            LocalReport rpt1 = new LocalReport();
            if (Type == "PGEntry")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptConsumptionSheetFB", lst, null, null);

                rpt1.EnableExternalImages = true;
                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("artno", artno));
                rpt1.SetParameters(new ReportParameter("color", color));
                rpt1.SetParameters(new ReportParameter("range", range));
                rpt1.SetParameters(new ReportParameter("size", size));
                rpt1.SetParameters(new ReportParameter("qty", qty));
                rpt1.SetParameters(new ReportParameter("date", date));
                rpt1.SetParameters(new ReportParameter("buyer", buyer));
                rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
                rpt1.SetParameters(new ReportParameter("catgory", catgory));
                rpt1.SetParameters(new ReportParameter("lstref", lstref));
                rpt1.SetParameters(new ReportParameter("construction", construction));
                rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
                rpt1.SetParameters(new ReportParameter("season", season));
                rpt1.SetParameters(new ReportParameter("brndname", brndname));
                rpt1.SetParameters(new ReportParameter("image", image));
                rpt1.SetParameters(new ReportParameter("estdate", estdate));
                rpt1.SetParameters(new ReportParameter("costrang", costrang));
                rpt1.SetParameters(new ReportParameter("ordrqty", ordrqty));
                rpt1.SetParameters(new ReportParameter("technician", technician));
                rpt1.SetParameters(new ReportParameter("lastmold", lastmold));
                rpt1.SetParameters(new ReportParameter("style", style));


                rpt1.SetParameters(new ReportParameter("RptTitle", "PD Guide"));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
                rpt1.SetParameters(new ReportParameter("notes", ds4.Tables[1].Rows[0]["notes"].ToString()));
                rpt1.SetParameters(new ReportParameter("createdby", ds4.Tables[1].Rows[0]["createdby"].ToString()));
                rpt1.SetParameters(new ReportParameter("createdbydesig", ds4.Tables[1].Rows[0]["createdbydesig"].ToString()));
            }
            //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));
            else if (Type == "PreCosting")
            {
                switch (comcod)
                {
                    case "5305":
                        rpt1 = RptSetupClass.GetLocalReport("R_04_Samp.RptPreSampCostingSheetFB", lst, lstCost, curinfo);
                        rpt1.EnableExternalImages = true;

                        rpt1.SetParameters(new ReportParameter("PlAnalysis", (Convert.ToDouble(lst[0].offprice) - Convert.ToDouble(Convert.ToDouble(totalamt) * Convert.ToDouble(exchnga))).ToString("#,##0.00;(#,##0.00); ")));
                        rpt1.SetParameters(new ReportParameter("con2rate", (Convert.ToDouble(exchngb)).ToString("#,##0.0000;(#,##0.0000); ")));
                        rpt1.SetParameters(new ReportParameter("totalamt", (Convert.ToDouble(totalamt)).ToString("#,##0.0000;(#,##0.0000); ")));
                        rpt1.SetParameters(new ReportParameter("conrate", conrate));
                        rpt1.SetParameters(new ReportParameter("curdesc", curdesc));
                        rpt1.SetParameters(new ReportParameter("con1rate", (Convert.ToDouble(exchnga)).ToString("#,##0.0000;(#,##0.0000); ")));

                        break;
                    default:
                        rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptPreCostingSheet", lst, lst1, null);
                        break;
                }

                rpt1.EnableExternalImages = true;
                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("artno", artno));
                rpt1.SetParameters(new ReportParameter("color", color));
                rpt1.SetParameters(new ReportParameter("range", range));
                rpt1.SetParameters(new ReportParameter("size", size));
                rpt1.SetParameters(new ReportParameter("qty", qty));
                rpt1.SetParameters(new ReportParameter("date", date));
                rpt1.SetParameters(new ReportParameter("buyer", buyer));
                rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
                rpt1.SetParameters(new ReportParameter("catgory", catgory));
                rpt1.SetParameters(new ReportParameter("lstref", lstref));
                rpt1.SetParameters(new ReportParameter("construction", construction));
                rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
                rpt1.SetParameters(new ReportParameter("season", season));
                rpt1.SetParameters(new ReportParameter("brndname", brndname));
                rpt1.SetParameters(new ReportParameter("image", image));
                //rpt1.SetParameters(new ReportParameter("estdate", estdate));
                //rpt1.SetParameters(new ReportParameter("exrate", exrate));
                //rpt1.SetParameters(new ReportParameter("tarrate", tarrate));
                //rpt1.SetParameters(new ReportParameter("offer", offer));
                //rpt1.SetParameters(new ReportParameter("confirm", confirm));
                //rpt1.SetParameters(new ReportParameter("currency", currency));
                //rpt1.SetParameters(new ReportParameter("artamount", artamount));
                rpt1.SetParameters(new ReportParameter("season", season));

                rpt1.SetParameters(new ReportParameter("costrang", costrang));
                rpt1.SetParameters(new ReportParameter("ordrqty", ordrqty));
                rpt1.SetParameters(new ReportParameter("technician", technician));
                rpt1.SetParameters(new ReportParameter("lastmold", lastmold));
                rpt1.SetParameters(new ReportParameter("style", style));

                rpt1.SetParameters(new ReportParameter("offprice", lst[0].offprice.ToString("#,##0.00; (#,##0.00); ")));
                rpt1.SetParameters(new ReportParameter("tarprice", lst[0].tarprice.ToString("#,##0.00; (#,##0.00); ")));
                rpt1.SetParameters(new ReportParameter("cnfrmprice", Convert.ToDouble(lstCost.Where(x => x.sircode == "049800102999").Select(c => c.stdamt).FirstOrDefault()).ToString("#,##0.00; (#,##0.00); ")));
                rpt1.SetParameters(new ReportParameter("con1", (Convert.ToDouble(totalamt) * Convert.ToDouble(exchnga)).ToString("#,##0.0000;(#,##0.0000); ")));
                rpt1.SetParameters(new ReportParameter("con2", (Convert.ToDouble(totalamt) * Convert.ToDouble(exchngb)).ToString("#,##0.0000;(#,##0.0000); ")));
                rpt1.SetParameters(new ReportParameter("samptype", samptype));
                rpt1.SetParameters(new ReportParameter("forma", forma));
                rpt1.SetParameters(new ReportParameter("RptTitle", "CBD Sheet"));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            }
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }


        private void PrintPreCostingSheet()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string styleid = this.ddlStyle.SelectedValue.ToString();
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = list1.FindAll(s => s.styleid == styleid);
            string url = list2[0].images.ToString();
            string image = "";

            if (url.Length > 0)
            {
                image = new Uri(Server.MapPath(url)).AbsoluteUri;
            }
            else
            {
                image = new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
            }



            string artno = this.txtArtno.Text.Trim().ToString();
            string color = this.txtcolor.Text.Trim().ToString();
            string range = this.txtsizernge.Text.Trim().ToString();
            string size = this.txtconsize.Text.Trim().ToString();
            string qty = this.txtordqty.Text.Trim().ToString();
            string date = this.txtDatefrom.Text.Trim().ToString();
            string buyer = this.txtbuyer.Text.Trim().ToString();
            string mrsendizer = this.RefMarName.Text.Trim().ToString();
            string catgory = this.txtCategory.Text.Trim().ToString();
            string lstref = this.txtlastrefno.Text.Trim().ToString();
            string construction = this.txtconstruction.Text.Trim().ToString();
            string smploptionno = this.txtsampleno.Text.Trim().ToString();
            string season = this.txtseason.Text.Trim().ToString();
            string brndname = this.txtbrand.Text.Trim().ToString();
            string estdate = this.txtestproduction.Text.Trim().ToString();
            string costrang = size;
            string ordrqty = this.txtordqty.Text.Trim().ToString(); ;
            string technician = this.txtsampleno.Text.Trim().ToString();
            string lastmold = this.txtlastrefno.Text.Trim().ToString();
            string style = this.txtCategory.Text.Trim().ToString();




            string exrate = this.txtExchngerate.Text.Trim().ToString();
            string tarrate = this.txttarprice.Text.Trim().ToString();
            string offer = this.txtoffprice.Text.Trim().ToString();
            string confirm = this.txtconfrmprice.Text.Trim().ToString();
            string currency = this.ddlCurList.SelectedItem.Text.Trim().ToString();


            DataTable dt = (DataTable)ViewState["tblstdcost"];


            DataTable dtcommon = (DataTable)ViewState["CommonCost"];
           
            DataTable tbl3 = (DataTable)ViewState["tblSmpleSizes"];
            if (dt == null)
            {
                return;
            }

            if (tbl3 == null)
            {
                return;
            }
            var lst2 = tbl3.DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>();

           

            var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.EclassConsumption>();
            var lst1 = dtcommon.DataTableToList<SPEENTITY.C_01_Mer.EclassCommonCost>();


            double tAmt = lst.Select(p => p.amt).Sum();
            double tAmtc = lst1.Select(p => p.amt).Sum();

            string artamount = Convert.ToDouble(tAmt + tAmtc).ToString("#,##0.00;(#,##0.00); ");


            LocalReport rpt1 = new LocalReport();
            

            switch (comcod)
            {
                case "5305":
                    rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptPreCostingSheetFB", lst, lst2, null);
                    break;
                default:
                    rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptPreCostingSheet", lst, lst1, null);
                    break;
            }

            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));

            rpt1.SetParameters(new ReportParameter("artno", artno));
            rpt1.SetParameters(new ReportParameter("color", color));
            rpt1.SetParameters(new ReportParameter("range", range));
            rpt1.SetParameters(new ReportParameter("size", size));
            rpt1.SetParameters(new ReportParameter("qty", qty));
            rpt1.SetParameters(new ReportParameter("date", "DATE: " + date));
            rpt1.SetParameters(new ReportParameter("buyer", buyer));
            rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
            rpt1.SetParameters(new ReportParameter("catgory", catgory));
            rpt1.SetParameters(new ReportParameter("lstref", lstref));
            rpt1.SetParameters(new ReportParameter("construction", construction));
            rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
            rpt1.SetParameters(new ReportParameter("season", season));
            rpt1.SetParameters(new ReportParameter("brndname", brndname));
            rpt1.SetParameters(new ReportParameter("image", image));
            rpt1.SetParameters(new ReportParameter("estdate", estdate));
            rpt1.SetParameters(new ReportParameter("exrate", exrate));
            rpt1.SetParameters(new ReportParameter("tarrate", tarrate));
            rpt1.SetParameters(new ReportParameter("offer", offer));
            rpt1.SetParameters(new ReportParameter("confirm", confirm));
            rpt1.SetParameters(new ReportParameter("currency", currency));
            rpt1.SetParameters(new ReportParameter("artamount", artamount));

            rpt1.SetParameters(new ReportParameter("costrang", costrang));
            rpt1.SetParameters(new ReportParameter("ordrqty", ordrqty));
            rpt1.SetParameters(new ReportParameter("technician", technician));
            rpt1.SetParameters(new ReportParameter("lastmold", lastmold));
            rpt1.SetParameters(new ReportParameter("style", style));


            rpt1.SetParameters(new ReportParameter("RptTitle", "CBD Sheet"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            //rpt1.SetParameters(new ReportParameter("issuedat", DateTime.Today.ToString("MMMM-yyyy")));
            //rpt1.SetParameters(new ReportParameter("validity", DateTime.Today.AddYears(3).ToString("MMMM-yyyy")));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintConsumptionSheet()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string styleid = this.ddlStyle.SelectedValue.ToString();
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = list1.FindAll(s => s.styleid == styleid);
            string url = list2[0].images.ToString();
            string image = "";

            if (url.Length > 0)
            {
                image = new Uri(Server.MapPath(url)).AbsoluteUri;
            }
            else
            {
                image = new Uri(Server.MapPath("~/images/no_img_preview.jpg")).AbsoluteUri;
            }



            string artno = this.txtArtno.Text.Trim().ToString();
            string color = this.txtcolor.Text.Trim().ToString();
            string range = this.txtsizernge.Text.Trim().ToString();
            string size = this.txtconsize.Text.Trim().ToString();
            string qty = this.txtordqty.Text.Trim().ToString();
            string date = this.txtDatefrom.Text.Trim().ToString();
            string buyer = this.txtbuyer.Text.Trim().ToString();
            string mrsendizer = this.txtmerchand.Text.Trim().ToString();
            string catgory = this.txtCategory.Text.Trim().ToString();
            string lstref = this.txtlastrefno.Text.Trim().ToString();
            string construction = this.txtconstruction.Text.Trim().ToString();
            string smploptionno = this.txtsampleno.Text.Trim().ToString();
            string season = this.txtseason.Text.Trim().ToString();
            string brndname = this.txtbrand.Text.Trim().ToString();
            string estdate = this.txtestproduction.Text.Trim().ToString();
            string costrang = size;
            string ordrqty = this.txtordqty.Text.Trim().ToString(); ;
            string technician = this.txtsampleno.Text.Trim().ToString();
            string lastmold = this.txtlastrefno.Text.Trim().ToString();
            string style = this.txtCategory.Text.Trim().ToString();


            DataTable dt = (DataTable)ViewState["tblstdcost"];
            DataTable tbl3 = (DataTable)ViewState["tblSmpleSizes"];
            if (dt == null)
            {
                return;
            }

            var lst = dt.DataTableToList<SPEENTITY.C_01_Mer.EclassConsumption>();
            var lst2 = tbl3.DataTableToList<SPEENTITY.C_01_Mer.EclassConsumptionSamSize>();


            LocalReport rpt1 = new LocalReport();
            switch (comcod)
            {
                case "5305":
                    rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptConsumptionSheetFB", lst, lst2, null);
                    break;
                default:
                    rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptConsumptionSheet", lst, null, null);
                    break;
            }
            
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));

            rpt1.SetParameters(new ReportParameter("artno", artno));
            rpt1.SetParameters(new ReportParameter("color", color));
            rpt1.SetParameters(new ReportParameter("range", range));
            rpt1.SetParameters(new ReportParameter("size", size));
            rpt1.SetParameters(new ReportParameter("qty", qty));
            rpt1.SetParameters(new ReportParameter("date", "DATE: " + date));
            rpt1.SetParameters(new ReportParameter("buyer", buyer));
            rpt1.SetParameters(new ReportParameter("mrsendizer", mrsendizer));
            rpt1.SetParameters(new ReportParameter("catgory", catgory));
            rpt1.SetParameters(new ReportParameter("lstref", lstref));
            rpt1.SetParameters(new ReportParameter("construction", construction));
            rpt1.SetParameters(new ReportParameter("smploptionno", smploptionno));
            rpt1.SetParameters(new ReportParameter("season", season));
            rpt1.SetParameters(new ReportParameter("brndname", brndname));
            rpt1.SetParameters(new ReportParameter("image", image));
            rpt1.SetParameters(new ReportParameter("estdate", estdate));

            rpt1.SetParameters(new ReportParameter("costrang", costrang));
            rpt1.SetParameters(new ReportParameter("ordrqty", ordrqty));
            rpt1.SetParameters(new ReportParameter("technician", technician));
            rpt1.SetParameters(new ReportParameter("lastmold", lastmold));
            rpt1.SetParameters(new ReportParameter("style", style));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Consumption Sheet"));
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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Validation Error!!!');", true);

               
                return;
            }
            this.lnkbtnRecalculate_Click(null, null);
            this.UpdateCost();
            this.lnkbtnAdd_Click(null, null);
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.ddlStyle.Enabled = false;
                this.ddlinqno.Enabled = false;
                this.GetProcess();
                this.Get_ColorSize();
                
            }

            this.lbtnOk.Text = "Ok";
            this.ddlStyle.Enabled = true;
            this.ddlinqno.Enabled = true;
        }
        private void Get_ColorSize()
        {
            string comcod = this.GetCompCode();
            string styleid = this.ddlStyle.SelectedValue.ToString();
            string inqno = this.ddlinqno.SelectedValue.ToString();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_ACTUAL_COLOR_SIZE", inqno, styleid, "", "", "", "", "", "", "");
            List<SPEENTITY.C_01_Mer.EclassOrderDetails> color = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            color.Add(new SPEENTITY.C_01_Mer.EclassOrderDetails("000000000000", "Common", "Y", "", "", ""));
            List<SPEENTITY.C_01_Mer.EclassOrderDetails> consize = ds1.Tables[3].DataTableToList<SPEENTITY.C_01_Mer.EclassOrderDetails>();
            consize.Add(new SPEENTITY.C_01_Mer.EclassOrderDetails("", "", "", "000000000000", "Common", "Y"));
            ViewState["tblcolor"] = color;
            ViewState["tblconsize"] = consize;
            this.Bind_color_size();
        }
        private void Bind_color_size()
        {
            List<SPEENTITY.C_01_Mer.EclassOrderDetails> color = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblcolor"];
            this.ddlcolor.DataTextField = "colordesc";
            this.ddlcolor.DataValueField = "colorid";
            this.ddlcolor.DataSource = color.FindAll(p => p.colorselect == "Y");
            this.ddlcolor.DataBind();
            this.ddlcolor.SelectedValue = "000000000000";

            List<SPEENTITY.C_01_Mer.EclassOrderDetails> consize = (List<SPEENTITY.C_01_Mer.EclassOrderDetails>)ViewState["tblconsize"];
            this.ddlconsize.DataTextField = "sizedesc";
            this.ddlconsize.DataValueField = "sizeid";
            this.ddlconsize.DataSource = consize.FindAll(p => p.sizeselect == "Y");
            this.ddlconsize.DataBind();
            this.ddlconsize.SelectedValue = "000000000000";


        }
        protected void ddlinqno_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string inqno = this.ddlinqno.SelectedValue.ToString();
            DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_SMPLE_INQ", inqno, "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0 || ds1 == null)
            {
                return;
            }
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_01_Mer.EclassSampleInquiry>();
            ViewState["tblinquery"] = list1;
            this.ddlStyle.DataTextField = "styledesc";
            this.ddlStyle.DataValueField = "styleid";
            this.ddlStyle.DataSource = list1;
            this.ddlStyle.DataBind();



            this.txtbuyer.Text = ds1.Tables[1].Rows[0]["buyername"].ToString();
            this.txtbuyerid.Text = ds1.Tables[1].Rows[0]["buyerid"].ToString();

            this.txtmerchand.Text = ds1.Tables[1].Rows[0]["postedbyname"].ToString();
            if (this.Request.QueryString["genno"].Length > 0)
            {
                this.ddlStyle.SelectedValue = this.Request.QueryString["genno"].ToString();
                this.lbtnOk_Click(null, null);
            }
            this.ddlStyle_SelectedIndexChanged(null, null);

            //this.ddlStyle_SelectedIndexChanged(null,null);
        }
        protected void GetProcess()
        {

            string comcod = this.GetCompCode();
            string inqno = this.Request.QueryString["actcode"].ToString();
            string styleid = this.Request.QueryString["genno"].ToString();
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "PreCosting" || type == "PreCostingApp")
            {
                type = "Cost";
            }
            string filter = "%";
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GETPROCESSCODE", filter, inqno, styleid, type, "", "", "", "", "");
            this.ddlProcess.DataSource = ds3.Tables[0];
            this.ddlProcess.DataTextField = "resdesc";
            this.ddlProcess.DataValueField = "rescode";
            this.ddlProcess.DataBind();
            ViewState["tblcodeType"] = ds3.Tables[0];
            ds3.Dispose();
            this.ddlProcess_SelectedIndexChanged(null, null);
            //this.imgbtnResourceCost_Click(null, null);
        }
        protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string inqno = this.ddlinqno.SelectedValue.ToString();
            string styleid = this.ddlStyle.SelectedValue.ToString();
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = list1.FindAll(s => s.styleid == styleid);
            this.txtCategory.Text = list2[0].catedesc.ToString();
            this.txtArtno.Text = list2[0].artno.ToString();
            this.txtcolor.Text = list2[0].color.ToString();
            this.txtordqty.Text = Convert.ToDouble(list2[0].ordqty).ToString("#,##0.00;(#,##0.00); ");
            this.txtconqty.Text = "1.00";
            this.txtsirunit.Text = list2[0].sirunit.ToString();
            this.txtsizernge.Text = list2[0].sizernge.ToString();
            this.txtconsize.Text = list2[0].consize.ToString();
            this.txtseason.Text = list2[0].seasondesc.ToString();
            this.txtsamplesize.Text = list2[0].samsize.ToString();
            this.txtbrand.Text = list2[0].brandesc.ToString();
            this.Uploadedimg.ImageUrl = (list2[0].images.ToString().Length > 0) ? list2[0].images.ToString() : "~/images/no_img_preview.png";
            this.hypLnkbtn.NavigateUrl = list2[0].images.ToString();

            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_INFO_FOR_CONSUMPTION", inqno, styleid, "", "", "", "", "", "", "");


            DataTable dt;
            string type = this.Request.QueryString["Type"].ToString();
            if (type == "Entry" || type == "ConApp")
            {
                DataView dv = ds3.Tables[1].DefaultView;
                dv.RowFilter = ("rescode like '049800101%'");
                dt = dv.ToTable();

                this.gvdircost.Columns[2].Visible = false;
            }

            else
            {
                dt = ds3.Tables[1];
            }

            ViewState["CommonCost"] = dt;


            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                return;
            this.RefMarName.Text = ds3.Tables[0].Rows[0]["marchand"].ToString();
            this.txtlastrefno.Text = ds3.Tables[0].Rows[0]["lstrefno"].ToString();
            this.txtNotes.Text = ds3.Tables[0].Rows[0]["notes"].ToString();
            this.txtconstruction.Text = ds3.Tables[0].Rows[0]["constrction"].ToString();
            this.txtsampleno.Text = ds3.Tables[0].Rows[0]["smplno"].ToString();
            this.txtordqty.Text = Convert.ToDouble(ds3.Tables[0].Rows[0]["estqty"]).ToString("#,##0.00;(#,##0.00); ");

            this.txtestproduction.Text = Convert.ToDateTime(ds3.Tables[0].Rows[0]["estprddat"]).ToString("dd-MMM-yyyy");
            this.ddlCurList.SelectedValue = ds3.Tables[0].Rows[0]["curcod"].ToString();
            this.txtExchngerate.Text = Convert.ToDouble(ds3.Tables[0].Rows[0]["exrate"]).ToString("#,##0.00;(#,##0.00); ");
            this.txttarprice.Text = Convert.ToDouble(ds3.Tables[0].Rows[0]["tarprice"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtoffprice.Text = Convert.ToDouble(ds3.Tables[0].Rows[0]["offprice"]).ToString("#,##0.00;(#,##0.00); ");
            this.txtconfrmprice.Text = Convert.ToDouble(ds3.Tables[0].Rows[0]["cnfrmprice"]).ToString("#,##0.00;(#,##0.00); ");

            //this.Data_Bind();
        }
        protected void ddlProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.imgbtnResourceCost_Click(null, null);
            //this.Image2.Visible = false;
            //this.Image3.Visible = false;
            string process = ddlProcess.SelectedValue.ToString();
            if (process == "490100101007" || process == "490100101004")
            {
                this.LbtnImport.Visible = true;
            }
            else
            {
                this.LbtnImport.Visible = false;
            }
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
            if (dt.Rows.Count == 0)
                return;

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
            //    DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_INV_STDANA", "GETRESCODE_02", SearchInfo, "", "", "", "", "", "", "", "");
            DataSet ds3 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_FOR_CONSUMPTION", "04%", "", "", "", "", "", "", "", "");
            this.ddlResourcesCost.DataSource = ds3.Tables[0];
            this.ddlResourcesCost.DataTextField = "sirdesc1";
            this.ddlResourcesCost.DataValueField = "sircode1";
            this.ddlResourcesCost.DataBind();
            //this.ddlrescode.DataSource = ds3.Tables[0];
            //this.ddlrescode.DataTextField = "resdesc";
            //this.ddlrescode.DataValueField = "rescode";
            //this.ddlrescode.DataBind();

            ViewState["tblresRes"] = ds3.Tables[0];
            //   ViewState["tblSpcf"] = ds3.Tables[1];
            ds3.Dispose();
            //  this.ddlResourcesCost_SelectedIndexChanged(null, null);

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

        private void GetComponentList()
        {
            string comcod = this.GetCompCode();
            string rawMaterials = this.ddlResourcesCost.SelectedValue.ToString();
            DataSet ds2 = Merdata.GetTransInfo(comcod, "SP_INV_STDANA", "GETCOMPONENTLIST", "", "", "", "", "", "", "", "", "");

            if (ds2 == null)
                return;

            ddlComponent.DataTextField = "resdesc";
            ddlComponent.DataValueField = "rescode";
            ddlComponent.DataSource = ds2.Tables[0];
            ddlComponent.DataBind();
            //   string curencyName = this.ddlCurList.SelectedItem.ToString();
            // this.gvCost.Columns[10].HeaderText = "Std. Rate (" + curencyName + ")";
        }
        private void ShowConsump()
        {
            ViewState.Remove("tblstdcost");
            string comcod = this.GetCompCode();
            string lccode = this.ddlinqno.SelectedValue.ToString();
            string processcode = "%";
            if (this.ChckVIew.Checked == false)
            {
                processcode = this.ddlProcess.SelectedValue.ToString() + "%";
            }
            string prodcode = this.ddlStyle.SelectedValue.ToString();
          

            DataSet ds4 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "SHOW_CONSUMPTION_INFO", prodcode, processcode, lccode, "", "", "", "");
            if (ds4 == null)
            {
                this.gvRtpcon.DataSource = null;
                this.gvRtpcon.DataBind();
                return;
            }
            ViewState["tblstdcost"] = ds4.Tables[0];
            ViewState["tblSmpleSizes"] = ds4.Tables[1];
            this.Data_Bind();
            this.SmpSize();

        }
        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblstdcost"];
            DataTable dt1 = (DataTable)ViewState["tblSmpleSizes"];
            DataTable commoncost = (DataTable)ViewState["CommonCost"];
            if (this.ChckVIew.Checked == false)
            {
                if (this.Request.QueryString["Type"].ToString() == "Entry" || this.Request.QueryString["Type"].ToString() == "ConApp")
                {

                    this.gvCost.Columns[11].Visible = false;
                    this.gvCost.Columns[12].Visible = false;
                    this.gvCost.Columns[13].Visible = false;
                    this.gvCost.Columns[14].Visible = false;
                    this.gvCost.Columns[15].Visible = false;
                    this.gvCost.Columns[16].Visible = false;
                }
                switch (this.GetCompCode())
                {
                    case "5301":
                        this.gvCost.Columns[12].Visible = false;
                        this.gvCost.Columns[13].Visible = false;
                        break;
                    default:
                        
                    break;
                }


                this.gvCost.DataSource = dt;
                this.gvCost.DataBind();
                this.gvdircost.DataSource = null;
                this.gvdircost.DataBind();
                if (dt.Rows.Count == 0)
                    return;
                Session["Report1"] = gvCost;
                ((HyperLink)this.gvCost.HeaderRow.FindControl("hlbtnRdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                this.FooterCalculation(dt);
            }
            else
            {
                if (this.Request.QueryString["Type"].ToString() == "Entry" || this.Request.QueryString["Type"].ToString() == "ConApp")
                {
                    //dt = (DataTable)ViewState["tblstdcostView"];

                    this.gvRtpcon.Columns[10].Visible = false;
                    this.gvRtpcon.Columns[11].Visible = false;
                    this.gvRtpcon.Columns[12].Visible = false;
                    this.gvRtpcon.Columns[13].Visible = false;
                    this.gvRtpcon.Columns[14].Visible = false;
                    this.gvRtpcon.Columns[15].Visible = false;
                }
                switch (this.GetCompCode())
                {
                    case "5301":
                        this.gvRtpcon.Columns[11].Visible = false;
                        this.gvRtpcon.Columns[12].Visible = false;
                        break;
                    default:
                       
                        break;
                }

                this.gvRtpcon.DataSource = HiddenSameValue(dt);
                this.gvRtpcon.DataBind();
                Session["Report1"] = gvRtpcon;
                ((HyperLink)this.gvRtpcon.HeaderRow.FindControl("hlbtnRdataExel2")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                this.FooterCalculation(dt);
                this.gvdircost.DataSource = commoncost;
                this.gvdircost.DataBind();
                this.othFooterCalculation(commoncost);
            }

        }
        protected void SmpSize()
        {
            
            DataTable tbl3 = (DataTable)ViewState["tblSmpleSizes"];
            if (tbl3.Rows.Count == 0)
                return;
            
            string processcode = this.ddlProcess.SelectedValue.ToString();



            for (int i = 0; i < tbl3.Columns.Count; i++)
            {
                if (tbl3.Rows[0].ItemArray[i].ToString() == "")
                {
                    grvSmpleSizes.Columns[i].Visible = false;

                }
                
            }
            
            this.grvSmpleSizes.ShowHeader = false;
           this.grvSmpleSizes.ShowFooter = false;
            this.grvSmpleSizes.DataSource = tbl3;
            this.grvSmpleSizes.DataBind();
            for (int i = 0; i < tbl3.Rows.Count; i++)
            {

                if (i != 0)
                {
                    grvSmpleSizes.Rows[i].FindControl("lnkAddSmpSiz").Visible = false;

                }
                else
                {
                    grvSmpleSizes.Rows[i].BackColor = System.Drawing.Color.FromArgb(200, 200, 200);
                }

            }
        }
        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            if (this.ChckVIew.Checked == false)
            {
                ((Label)this.gvCost.FooterRow.FindControl("lblgvBdamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(convrate)", "")) ? 0.00 : dt.Compute("sum(convrate)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
                ((Label)this.gvCost.FooterRow.FindControl("lblgvfamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
                ((Label)this.gvCost.FooterRow.FindControl("lblgvfper")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.0000;(#,##0.0000); ") + (Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))) > 0 ? "%" : "");
                //((Label)this.gvCost.FooterRow.FindControl("lblgvttlqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }
            else
            {


                ((Label)this.gvRtpcon.FooterRow.FindControl("lblgvBdamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(convrate)", "")) ? 0.00 : dt.Compute("sum(convrate)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
                ((Label)this.gvRtpcon.FooterRow.FindControl("lblgvfamtCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
                ((Label)this.gvRtpcon.FooterRow.FindControl("lblgvfper")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.0000;(#,##0.0000); ") + (Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ? 0.00 : dt.Compute("sum(percnt)", ""))) > 0 ? "%" : "");
                //((Label)this.gvRtpcon.FooterRow.FindControl("lblgvttlqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");
            }
        }


        private void othFooterCalculation(DataTable dt)
        {

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataTable dt2 = (DataTable)ViewState["tblstdcost"];
            double conAmt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(amt)", "")) ? 0.00 : dt2.Compute("sum(amt)", "")));

            double otAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 : dt.Compute("sum(amt)", "")));

            ((Label)this.gvdircost.FooterRow.FindControl("lblgvfamtCost")).Text = (conAmt + otAmt).ToString("#,##0.0000;(#,##0.0000); ");
            DataView dv = dt.DefaultView;
            dv.RowFilter = "rescode not like '049800108%'";
            DataTable dt3 = dv.ToTable();

            double otAm2 = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("sum(amt)", "")) ? 0.00 : dt3.Compute("sum(amt)", "")));
            ViewState["prevvalue"] = (conAmt + otAm2).ToString("#,##0.0000;(#,##0.0000); ");
            ((Label)this.gvdircost.FooterRow.FindControl("lblgvfamtPrevCost")).Text = (conAmt + otAm2).ToString("#,##0.0000;(#,##0.0000); ");


        }
        protected void lnkAddResouctCost_Click(object sender, EventArgs e)
        {
            this.Save_Value();
            DataTable tbl2 = (DataTable)ViewState["tblstdcost"];
            string processcode = this.ddlProcess.SelectedValue.ToString();
            string processdesc = this.ddlProcess.SelectedItem.ToString();
            string rescod = this.ddlResourcesCost.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlcolor.SelectedValue.ToString();
            string sizeid = this.ddlconsize.SelectedValue.ToString();
            string Specification = this.ddlResourcesCost.SelectedValue.ToString().Substring(12, 12); ;
            string compcode = this.ddlComponent.SelectedValue.ToString();
            string compname = this.ddlComponent.SelectedItem.ToString();


            //foreach (ListItem item in ddlComponent.Items)
            //{
            //    if (item.Selected)
            //    {

            //compcode = item.Value;
            //compname = item.Text;

            DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and spcfcode='" + Specification + "' and colorid='" + colorid + "' and sizeid='" + sizeid + "' and compcode='" + compcode + "'");
            // DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and  compcode='" + compcode + "'" + "' and spcfcode='" + Specification + "'");
            if (dr.Length > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Selected Component Already Added');", true);



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
                dr1["cfrate"] = 0.00;
                dr1["fcfrate"] = (((DataTable)ViewState["tblresRes"]).Select("sircode='" + rescod + "' and spcfcod='" + Specification + "'"))[0]["rate"].ToString(); ;
                tbl2.Rows.Add(dr1);
            }
            //    }
            //}




            ViewState["tblstdcost"] = tbl2;
            this.Data_Bind();
        }
        private void Save_Value()
        {
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            double qty, rate, amt, convRate, cfrate;

            if (ChckVIew.Checked == false)
            {
                double convrate1 = Convert.ToDouble("0" + ((TextBox)txtExchngerate).Text.Trim());

                for (int i = 0; i < this.gvCost.Rows.Count; i++)
                {
                    string resdesc = ((Label)this.gvCost.Rows[i].FindControl("lblgvDesc")).Text.Trim();
                    string resunit = ((Label)this.gvCost.Rows[i].FindControl("txtgvunit0")).Text.Trim();
                    string deptcode = ((DropDownList)this.gvCost.Rows[i].FindControl("DdlDepartmnet")).SelectedValue.ToString().Trim();
                    string deptname = ((DropDownList)this.gvCost.Rows[i].FindControl("DdlDepartmnet")).SelectedItem.ToString();

                    double conqty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvconqty")).Text.Trim());
                    double wper = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvwestpc")).Text.Trim());

                    double netQty = conqty + (conqty * (wper / 100));

                    ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text = netQty.ToString("#,##0.000000;(#,##0.000000); ");

                    qty = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqtyCost")).Text.Trim());
                    rate = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqrateCost")).Text.Trim());
                    cfrate = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvqcfrate")).Text.Trim());

                    double amtgrid = Convert.ToDouble("0" + ((TextBox)this.gvCost.Rows[i].FindControl("txtgvamtCost")).Text.Trim());
                    double fcfrate = (cfrate * rate / 100) + rate;
                    amt = qty * fcfrate;
                    convRate = qty * convrate1;

                    dt.Rows[i]["procode"] = deptcode;
                    dt.Rows[i]["prodesc"] = deptname;
                    dt.Rows[i]["resdesc"] = resdesc;
                    dt.Rows[i]["resunit"] = resunit;
                    dt.Rows[i]["conqty"] = conqty;
                    dt.Rows[i]["westpc"] = wper;
                    dt.Rows[i]["qty"] = qty;
                    dt.Rows[i]["rate"] = rate;
                    dt.Rows[i]["convrate"] = convRate;
                    dt.Rows[i]["cfrate"] = cfrate;
                    dt.Rows[i]["fcfrate"] = fcfrate;

                    dt.Rows[i]["amt"] = (qty * fcfrate > 0) ? amt : amtgrid;
                }

                double netqty = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ? 0.00 : dt.Compute("sum(qty)", "")));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["percnt"] = (netqty == 0) ? 0.00 : (Convert.ToDouble(dt.Rows[i]["qty"].ToString()) * 100) / netqty;

                }
            }
            DataTable commoncost = (DataTable)ViewState["CommonCost"];
            this.lnkbtnAdd_Click(null, null);
            //-------------------------------------- for percent------------------------------
            DataTable ttlcost = (DataTable)ViewState["tblttlcost"];
            if (ChckVIew.Checked == true)
            {
                if (ttlcost != null)
                {
                    DataView dv = ttlcost.DefaultView;
                    dv.RowFilter = "rsircode not like '049800108%' OR rsircode not like '049800105006%'";
                    ttlcost = dv.ToTable();
                    double totlfor_percent = Convert.ToDouble((Convert.IsDBNull(ttlcost.Compute("sum(stdamt)", "")) ? 0.00 : ttlcost.Compute("sum(stdamt)", "")));
                    for (int i = 0; i < this.gvdircost.Rows.Count; i++)
                    {

                        double com_amt = Convert.ToDouble("0" + ((TextBox)this.gvdircost.Rows[i].FindControl("txtgvamtCost")).Text.Trim());
                        double percnt = Convert.ToDouble("0" + ((TextBox)this.gvdircost.Rows[i].FindControl("txtpercnt")).Text.Trim());
                        if (percnt > 0)
                        {
                            com_amt = (totlfor_percent * percnt) / 100;
                            com_amt = com_amt + (com_amt * percnt) / 100;
                        }


                        commoncost.Rows[i]["amt"] = com_amt;
                        commoncost.Rows[i]["percnt"] = percnt;
                    }
                }
            }


            ViewState["tblstdcost"] = dt;
            ViewState["CommonCost"] = commoncost;


        }
        private void UpdateCost()
        {

            string comcod = this.GetCompCode();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string PostDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            string pcsessionid = hst["session"].ToString();
            string pctrmid = hst["compname"].ToString();
            string pcuserid = hst["usrid"].ToString();
            string pcDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");




            DataTable dt = (DataTable)ViewState["tblstdcost"];
            DataTable dtComm = (DataTable)ViewState["CommonCost"];


            string prodcode = this.ddlStyle.SelectedValue.ToString();
            string proscode = this.ddlProcess.SelectedValue.ToString();
            string inqno = this.ddlinqno.SelectedValue.ToString();
            double convrate1 = Convert.ToDouble("0" + ((TextBox)txtExchngerate).Text.Trim());
            string marchand = this.RefMarName.Text.Trim().ToString();
            string lastrefno = this.txtlastrefno.Text.Trim().ToString();
            string construction = this.txtconstruction.Text.Trim().ToString();
            string smpleno = this.txtsampleno.Text.Trim().ToString();
            string brandname = this.txtbrand.Text.Trim().ToString();
            string Notes = this.txtNotes.Text.Trim().ToString();
            string estprddat = Convert.ToDateTime(this.txtestproduction.Text.Trim()).ToString("dd-MMM-yyyy");
            string curcode = this.ddlCurList.SelectedValue.ToString();
            double estqty = Convert.ToDouble("0" + ((TextBox)txtordqty).Text.Trim());
            double tarprice = Convert.ToDouble("0" + ((TextBox)txttarprice).Text.Trim());
            double offprice = Convert.ToDouble("0" + ((TextBox)txtoffprice).Text.Trim());
            double confrmprice = Convert.ToDouble("0" + ((TextBox)txtconfrmprice).Text.Trim());
            this.AddSmpSizSaveValue();
            DataTable tbl2 = (DataTable)ViewState["tblSmpleSizes"];
            DataSet ds= new DataSet("ds1");
            ds.Tables.Add(tbl2);
            ds.Tables[0].TableName = "tblsizes";
            string xmlss = ds.GetXml();

            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "CONSUMPTION_UPDATE",  ds, null,null, "STDCINF2B", inqno, prodcode, marchand, lastrefno,
                construction, smpleno, estqty.ToString(), brandname, estprddat, curcode, convrate1.ToString(), tarprice.ToString(), offprice.ToString(), confrmprice.ToString(),
               userid, sessionid, trmid, PostDat, pcuserid, pcsessionid, pctrmid, pcDat, Notes);
           //
            for (int i = 0; i < dt.Rows.Count; i++)
            { //
                string id = dt.Rows[i]["id"].ToString();

                string procode = dt.Rows[i]["procode"].ToString();
                string rescod = dt.Rows[i]["rescode"].ToString();
                string resdesc = dt.Rows[i]["resdesc"].ToString();
                string runit = dt.Rows[i]["resunit"].ToString();
                string resqty = dt.Rows[i]["qty"].ToString();
                string resamt = dt.Rows[i]["amt"].ToString();
                string rate = dt.Rows[i]["rate"].ToString();
                string percnt = dt.Rows[i]["percnt"].ToString();
                string conqty = dt.Rows[i]["conqty"].ToString();
                string westpc = dt.Rows[i]["westpc"].ToString();
                string comptCode = dt.Rows[i]["compcode"].ToString();
                string spcfcode = dt.Rows[i]["spcfcode"].ToString();
                string colorid = dt.Rows[i]["colorid"].ToString();
                string sizeid = dt.Rows[i]["sizeid"].ToString();
                string cfrate = dt.Rows[i]["cfrate"].ToString();
                if (percnt == "")
                    percnt = "0.0";
                if (Convert.ToDouble("0" + resqty) > 0)
                {
                    result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "CONSUMPTION_UPDATE", prodcode, procode, rescod, resqty, resamt, percnt, inqno, resdesc, colorid, sizeid, runit, conqty, westpc, spcfcode, comptCode, convrate1.ToString(), rate, cfrate, id);
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: "+ Merdata.ErrorObject["Msg"].ToString() + "');", true);



                        return;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);


            }
            if ( dt.Rows.Count == 0)
                return;
            for (int i = 0; i < dtComm.Rows.Count; i++)
            {
                string id = dtComm.Rows[i]["id"].ToString();

                string rescod = dtComm.Rows[i]["rescode"].ToString();
                string resdesc = dtComm.Rows[i]["resdesc"].ToString();
                string runit = dtComm.Rows[i]["resunit"].ToString();
                string resqty = "0.00";
                string resamt = dtComm.Rows[i]["amt"].ToString();
                string percnt = dtComm.Rows[i]["percnt"].ToString(); ;
                string conqty = "0.00";
                string westpc = "0.00";
                string comptCode = "000000000000";
                string spcfcode = "000000000000";
                string colorid = "000000000000";
                string sizeid = "000000000000";
                
                result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "CONSUMPTION_UPDATE", prodcode, "000000000000", rescod, resqty, resamt, percnt, inqno, resdesc, colorid, sizeid, runit, conqty, westpc, spcfcode, comptCode, convrate1.ToString(), "0.00","0.00", id);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Error: " + Merdata.ErrorObject["Msg"].ToString() + "');", true);

                
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

              
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
            DataTable dt = new DataTable();
            ds1.Merge(dt);
            ds1.Tables[0].Columns.Add("delbyid", typeof(string));
            ds1.Tables[0].Columns.Add("delseson", typeof(string));
            ds1.Tables[0].Columns.Add("deldate", typeof(DateTime));

            //ds1.Tables[0].Rows[0]["delbyid"] = usrid;
            //ds1.Tables[0].Rows[0]["delseson"] = session;
            //ds1.Tables[0].Rows[0]["deldate"] = Date;


            ds1.Merge(ds.Tables[0]);
            //ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            //ds1.Tables[1].TableName = "tbl2";

            bool resulta = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Inqno, Styleid);

            if (!resulta)
            {

                //return;
            }
            else
            {
                //    ((Label)this.Master.FindControl("lblANMgsBox")).Visible = true;
                //    ((Label)this.Master.FindControl("lblANMgsBox")).Text = "Successfully Deleted";
                //       ((Label)this.Master.FindControl("lblANMgsBox")).Attributes["style"] = "background:Green;";

                //  ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

            return "";
        }
        protected void gvCost_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblstdcost"];
            string Inqno = this.ddlinqno.SelectedValue.ToString();

            string prodcode = this.ddlStyle.SelectedValue.ToString();
            string proscode = this.ddlProcess.SelectedValue.ToString();

            string rescod = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcodeCost")).Text.Trim();
            string spcfcode = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvspcfcode")).Text.Trim();
            string compcode = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcompcode")).Text.Trim();
            string colorid = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvcolor")).Text.Trim();
            string sizeid = ((Label)this.gvCost.Rows[e.RowIndex].FindControl("lblgvsize")).Text.Trim();

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);

            this.XmlDataInsert(Inqno, prodcode, ds1);


            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "DELETE_RESOURCE", prodcode, proscode, rescod, colorid, sizeid, compcode, spcfcode, Inqno, "", "", "", "", "", "", "");
            if (!result)
                return;
            int index = (this.gvCost.PageIndex) * this.gvCost.PageSize + e.RowIndex;
            dt.Rows[index].Delete();
            DataView dv = dt.DefaultView;
            
            //dt.AsEnumerable()
            //      .Where(r => r.Field<string>("col1") != "ali")
            //      .CopyToDataTable();


            ViewState["tblstdcost"] = dv.ToTable();
            this.Data_Bind();
            this.SmpSize();
        }

        protected void ChckVIew_CheckedChanged(object sender, EventArgs e)
        {
            ViewState.Remove("tblstdcost");
            this.gvdircost.DataSource = null;
            this.gvdircost.DataBind();
            this.Select_View();
        }
        protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataTable tblcodeType = ((DataTable)ViewState["tblcodeType"]).Copy();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (this.Request.QueryString["Type"].ToString() == "PreCosting")
                {
                    ((TextBox)e.Row.FindControl("txtgvconqty")).Enabled = false;
                    //    ((TextBox)e.Row.FindControl("txtgvwestpc")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtgvqtyCost")).Enabled = false;
                    //((Label)this.gvCost.FooterRow.FindControl("lbltoalf")).Visible = tr;
                }
                else
                {
                    ((TextBox)e.Row.FindControl("txtgvconqty")).Enabled = true;
                    //   ((TextBox)e.Row.FindControl("txtgvwestpc")).Enabled = true;
                    ((TextBox)e.Row.FindControl("txtgvqtyCost")).Enabled = true;

                }
                string prcode= Convert.ToString(DataBinder.Eval(e.Row.DataItem, "procode"));

                DropDownList dept = (DropDownList)e.Row.FindControl("DdlDepartmnet");
                dept.DataTextField = "resdesc";
                dept.DataValueField = "rescode";
                dept.DataSource = tblcodeType;
                dept.DataBind();
                dept.SelectedValue = prcode;
               
                
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp")
                {
                    ((Label)e.Row.FindControl("lbltoalf")).Visible = true;
                }
                else
                {
                    ((Label)e.Row.FindControl("lbltoalf")).Visible = false;
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
            ViewState["tblstdcost"] = dt;
            return dt;
        }


        //protected void lblgvDesc_Click(object sender, EventArgs e)
        //{
        //    string comcod = this.GetCompCode();
        //    GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
        //    int index = row.RowIndex;
        //    string curdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");

        //    string Itemcode = ((Label)this.gvdircost.Rows[index].FindControl("lblgvcodeCost")).Text.ToString();

        //    string inqno = this.ddlinqno.SelectedValue.ToString();
        //    string styleid = this.ddlStyle.SelectedValue.ToString();

        //    DataSet ds1 = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_COMM_MATERIALS", inqno, styleid, "", "", "", "", "", "", "");
        //    ViewState["tblcomItm"] = ds1.Tables[0];


        //    this.ModalDataBind();

        //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        //}
        //private void ModalDataBind()
        //{
        //   DataTable dt = (DataTable)ViewState["tblcomItm"];
        //   this.grvComm.DataSource = dt;
        //   this.grvComm.DataBind();


        //}

        //protected void lnkbtnAdd_Click(object sender, EventArgs e)
        //{
        //    DataTable tbl2 = (DataTable)ViewState["tblcomItm"];
        //    string processcode = this.ddlProcess2.SelectedValue.ToString();
        //    string processdesc = this.ddlProcess2.SelectedItem.ToString().Substring(14);
        //    string rescod = this.ddlrescode.SelectedValue.ToString();
        //    string colorid = this.ddlcolor.SelectedValue.ToString();
        //    string sizeid = this.ddlconsize.SelectedValue.ToString();
        //    string Specification = this.ddlSpcfcode2.SelectedValue.ToString();
        //    string compcode = "000000000000";
        //    string compname = "";




        //    DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and spcfcode='" + Specification + "' and colorid='" + colorid + "' and sizeid='" + sizeid + "' and compcode='" + compcode + "'");
        //    // DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and  compcode='" + compcode + "'" + "' and spcfcode='" + Specification + "'");
        //    if (dr.Length > 0)
        //    {
        //        ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + "Selected Component Already Added";

        //        return;
        //    }
        //    else
        //    {
        //        //comcod, inqno, prodcode, colorid, sizeid, procode, rsircode, spcfcode, conqty, westpc, rstdqty, stdamt

        //        DataRow dr1 = tbl2.NewRow();
        //        dr1["procode"] = processcode;
        //        dr1["prodesc"] = processdesc;
        //        dr1["rescode"] = rescod;
        //        dr1["resdesc"] = this.ddlrescode.SelectedItem.ToString().Substring(13);
        //        dr1["resunit"] = (((DataTable)ViewState["tblresRes"]).Select("rescode='" + rescod + "'"))[0]["resunit"].ToString();
        //        dr1["conqty"] = 0;
        //        dr1["westpc"] = 0;
        //        dr1["qty"] = 0;
        //        dr1["rate"] = (((DataTable)ViewState["tblresRes"]).Select("rescode='" + rescod + "'"))[0]["rate"].ToString();
        //        dr1["amt"] = 0;
        //        dr1["convrate"] = 0;
        //        dr1["percnt"] = 0;
        //        dr1["colorid"] = colorid;
        //        dr1["sizeid"] = sizeid;
        //        // dr1["rawmatname"] = this.ddlResourcesCost.SelectedItem.ToString().Substring(13);
        //        dr1["spcfcode"] = Specification;
        //        dr1["spcfdesc"] = this.ddlSpcfcode2.SelectedItem.Text.Trim();
        //        dr1["compname"] = compname;
        //        dr1["compcode"] = compcode;
        //        tbl2.Rows.Add(dr1);
        //    }
        //    //    }
        //    //}




        //    ViewState["tblcomItm"] = tbl2;
        //    this.ModalDataBind();
        //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        //}
        protected void gvdircost_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lblgvDesc");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();



                string inqno = this.ddlinqno.SelectedValue.ToString();
                string styleid = this.ddlStyle.SelectedValue.ToString();

                string Type = this.Request.QueryString["Type"].ToString();

                string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (ASTUtility.Left(rescode, 9) == "049800101")
                {
                    if (Type == "Entry" || Type == "ConApp")
                    {
                        hlink1.Text = "Commmon Material Consumption";
                    }
                    hlink1.NavigateUrl = "~/F_01_Mer/LinkConsumptionSheet?Type=" + Type + "&actcode=" + inqno + "&genno=" + styleid;
                    //hlink1.Attributes()
                }
                ((TextBox)e.Row.FindControl("txtpercnt")).Enabled = false;

                if (rescode.Substring(0, 9) == "049800108" || rescode == "049800105006")//049800105006 factoring charge for FB Footwear
                {
                    ((TextBox)e.Row.FindControl("txtpercnt")).Enabled = true;
                }
                //else
                //{
                //    HypApproval.NavigateUrl = "~/F_13_CWare/PurReqEntry02.aspx?InputType=FxtAstApproval&actcode=" + prjCode + "&genno=" + reqno;
                //}






            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp")
                {
                    double conAmt = 0;
                    double otAmt = 0;
                    int RowIndex = e.Row.RowIndex;
                    int DataItemIndex = e.Row.DataItemIndex;
                    int Columnscount = gvdircost.Columns.Count;
                    GridViewRow row = new GridViewRow(RowIndex, DataItemIndex, DataControlRowType.Footer, DataControlRowState.Normal);
                    for (int i = 0; i < Columnscount; i++)
                    {
                        string mtext = String.Empty;
                        switch (i.ToString())
                        {
                            case "1":
                                mtext = "Profit/Loss with Percent";
                                break;
                            case "3":
                                DataTable dt2 = (DataTable)ViewState["tblstdcost"];
                                DataTable commoncost = (DataTable)ViewState["CommonCost"];
                                conAmt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(amt)", "")) ? 0.00 : dt2.Compute("sum(amt)", "")));
                                otAmt = Convert.ToDouble((Convert.IsDBNull(commoncost.Compute("sum(amt)", "")) ? 0.00 : commoncost.Compute("sum(amt)", "")));
                                double confirmprice = Convert.ToDouble("0" + this.txtconfrmprice.Text.Trim());
                                mtext = (((confirmprice-(conAmt + otAmt))*100)/ (conAmt + otAmt)).ToString("#,##0.0000;(#,##0.0000); ")+" %";
                                break;
                            default:
                                mtext = "";
                                break;
                        }
                        TableCell tablecell = new TableCell();
                        tablecell.Text = mtext;
                        tablecell.Attributes.CssStyle.Add("text-align", "right");
                        tablecell.Attributes.CssStyle.Add("color", "blue");
                        row.Cells.Add(tablecell);
                    }
                    this.gvdircost.Controls[0].Controls.Add(row);



                    ((Label)e.Row.FindControl("lbltoalf")).Visible = true;
                }
                else
                {
                    ((Label)e.Row.FindControl("lbltoalf")).Visible = false;
                }
            }
        }

        protected void Con_Cost_Approved(object sender, EventArgs e)
        {

            this.Consumtion_Approved();
        }

        private void Consumtion_Approved()
        {
            
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Enabled = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string userid = hst["usrid"].ToString();
            string AppDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string InqNO = this.Request.QueryString["actcode"].ToString();
            string Styleid = this.Request.QueryString["genno"].ToString();
            string stylename = this.txtCategory.Text.ToString();
            string artno = this.txtArtno.Text.ToString();

            string updateType = (this.Request.QueryString["Type"] == "ConApp") ? "Cons" : "PreCos";

            bool result = Merdata.UpdateXmlTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "APPROVED_COMSUMTION", null, null, null, InqNO, Styleid, userid, AppDat, trmid, sessionid, updateType);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentfail('Consumption Not Approved');", true);


                return;

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Consumption Approved Successfully');", true);

        

            if (this.Request.QueryString["Type"].ToString() == "PreCostingApp")
            {
                string esubject = "Costing Complete! Request to Order Generate";
                string url = "http://202.0.94.49/F_34_Mgt/RptAdminInterface";
                string bodyContent = "Dear Sir, </br>A New Order Generate, Please click  <b> <a href='" + url +
                                "' target='_blank'>" + stylename + "-" + artno + " </a> </b> on the link to Accept or Reject";

                if (CommonClass.ConfimMail("0101010", esubject, url, bodyContent) == true)
                {

                }
            }



            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

            //Common.LogStatus("Diagnosis Complite", "QC Qualified", "Recived No: ", centrid + " - " + wrRecvno);
        }
        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string styleid = this.Request.QueryString["genno"].ToString();
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list1 = (List<SPEENTITY.C_01_Mer.EclassSampleInquiry>)ViewState["tblinquery"];
            List<SPEENTITY.C_01_Mer.EclassSampleInquiry> list2 = list1.FindAll(s => s.styleid == styleid);
            string images = (list2[0].images.ToString().Length > 0) ? list2[0].images.ToString() : "";
            string comcod = this.GetCompCode();
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string Url = "";

            string inqno = this.Request.QueryString["actcode"].ToString();
            var filePath = Server.MapPath(images);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            if (AsyncFileUpload1.HasFile)
            {
                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/SAMPLE/") + random + extension);
                Url = "~/Upload/SAMPLE/" + random + extension;
            }
            bool result = Merdata.UpdateTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "REPLACE_SAMPLE_IMAGE", inqno, styleid, Url);
            if (result)
            {
                this.Uploadedimg.ImageUrl = Url;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);

            }
        }
        private void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string inqno = this.Request.QueryString["actcode"].ToString();
            string styleid = this.Request.QueryString["genno"].ToString();
            DataSet result = Merdata.GetTransInfo(comcod, "SP_ENTRY_COST_AND_BUDGET", "GET_INQUIRYWISE_TOTAL_COST", inqno, styleid);
            if (result == null || result.Tables[0].Rows.Count == 0)
                return;
            ViewState["tblttlcost"] = result.Tables[0];
            this.txtEstimated.Text = Convert.ToDouble((Convert.IsDBNull(result.Tables[0].Compute("sum(stdamt)", "")) ? 0.00 : result.Tables[0].Compute("sum(stdamt)", ""))).ToString("#,##0.0000;(#,##0.0000); ");
        }

        protected void LbtnComponent_Click(object sender, EventArgs e)
        {
            this.GetComponentList();
        }

        protected void txtgvqrateCost_TextChanged(object sender, EventArgs e)
        {
            this.Save_Value();
            this.Data_Bind();
        }

        protected void ChckCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChckCopy.Checked == true)
            {
                this.ddlPreList.Visible = true;
                 this.LblSelArticle.Visible = true;
                this.LbtnCopy.Visible = true;
                ddlPreList_SelectedIndexChanged(null, null);
            }
            else
            {
                this.ddlPreList.Visible = false;
                this.LblSelArticle.Visible = false;
                this.LbtnCopy.Visible = false;
            }
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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('You Select Same Style');", true);
                return;
            }
            string type = (this.Request.QueryString["Type"].ToString() == "PreCosting" || this.Request.QueryString["Type"].ToString() == "PreCostingApp") ? "Rate" : "All";
            bool result = Merdata.UpdateTransInfo1(comcod, "SP_ENTRY_CONSUMPTION", "COPY_CONSUMPTION", frinqno, frstyleid, toinqno, tostyleid, type);
            if (result == true)
            {
                this.ShowConsump();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Copy Successfully');", true);


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

        protected void LbtnImport_Click(object sender, EventArgs e)
        {

            string process = this.ddlProcess.SelectedValue.ToString();
            string toinqno = this.Request.QueryString["actcode"].ToString();
            string tostyleid = this.Request.QueryString["genno"].ToString();
            string comcod = this.GetCompCode();
            string buyerid = this.txtbuyerid.Text.Trim().ToString();
            bool result = Merdata.UpdateTransInfo1(comcod, "SP_ENTRY_CONSUMPTION", "COPY_CONSUMPTION_ANALYSIS", process, toinqno, tostyleid, buyerid);
            if (result == true)
            {
                this.ShowConsump();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Import Successfully');", true);

            }
        }

        protected void lblgvspcfdesc_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string material = ((Label)this.gvCost.Rows[index].FindControl("lblgvcodeCost")).Text.ToString();
            string spcfcode = ((Label)this.gvCost.Rows[index].FindControl("lblgvspcfcode")).Text.ToString();
            string compcode = ((Label)this.gvCost.Rows[index].FindControl("lblgvcompcode")).Text.ToString();
            string deptcode = ((Label)this.gvCost.Rows[index].FindControl("lblgvDeptcode")).Text.ToString();
            this.lblHelper.Text = deptcode + compcode + material + spcfcode;
            DataSet result = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "GET_MATERIAL_WISE_SPECIFICATION", material);
            if (result.Tables[0].Rows.Count == 0 || result == null)
            {
                return;
            }

            this.ddlSpecification.DataTextField = "spcfdesc";
            this.ddlSpecification.DataValueField = "spcfcod";
            this.ddlSpecification.DataSource = result.Tables[0];
            this.ddlSpecification.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string lccode = this.ddlinqno.SelectedValue.ToString();
            string prodcode = this.ddlStyle.SelectedValue.ToString();
            string deptcode = this.lblHelper.Text.ToString().Substring(0, 12);
            string compcode = this.lblHelper.Text.ToString().Substring(12, 12);
            string material = this.lblHelper.Text.ToString().Substring(24, 12);
            string spcfcode = this.lblHelper.Text.ToString().Substring(36, 12);
            string tospcfcod = this.ddlSpecification.SelectedValue.ToString();
            bool result = Merdata.UpdateTransInfo1(comcod, "SP_ENTRY_CONSUMPTION", "CHANGE_SPECIFICATION_FOR_CONSUMPTION", lccode, prodcode, deptcode, compcode, material, spcfcode, tospcfcod);
            if (result == true)
            {
                this.ShowConsump();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Specification Change Successfully');", true);

      
            }
        }
        protected void lnkAddSmpSiz_Click(object sender, EventArgs e)
        {
            
            DataTable tbl2 = (DataTable)ViewState["tblSmpleSizes"];

            this.AddSmpSizSaveValue();

            DataRow dr1 = tbl2.NewRow();
            dr1["typedesc"] = "";
            dr1["s1"] = "";
            dr1["s2"] = "";
            dr1["s3"] = "";
            dr1["s4"] = "";
            dr1["s5"] = "";
            dr1["s6"] = "";
            dr1["s7"] = "";
            dr1["s8"] = "";
            dr1["s9"] = "";
            dr1["s10"] = "";

            tbl2.Rows.Add(dr1);
            
            ViewState["tblSmpleSizes"] = tbl2;
            this.SmpSize();
        }
        protected void AddSmpSizSaveValue()
        {
            DataTable tbl2 = (DataTable)ViewState["tblSmpleSizes"];
            string comcod = this.GetCompCode();
            foreach (GridViewRow row in grvSmpleSizes.Rows)
            {

                tbl2.Rows[row.RowIndex]["typedesc"] = ((TextBox)row.FindControl("lblgvtypedesc")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s1"] = ((TextBox)row.FindControl("lblgvs1")).Text.ToString(); 
                tbl2.Rows[row.RowIndex]["s2"] = ((TextBox)row.FindControl("lblgvs2")).Text.ToString(); 
                tbl2.Rows[row.RowIndex]["s3"] = ((TextBox)row.FindControl("lblgvs3")).Text.ToString(); 
                tbl2.Rows[row.RowIndex]["s4"] = ((TextBox)row.FindControl("lblgvs4")).Text.ToString(); 
                tbl2.Rows[row.RowIndex]["s5"] = ((TextBox)row.FindControl("lblgvs5")).Text.ToString(); 
                tbl2.Rows[row.RowIndex]["s6"] = ((TextBox)row.FindControl("lblgvs6")).Text.ToString();
                tbl2.Rows[row.RowIndex]["s7"] = ((TextBox)row.FindControl("lblgvs7")).Text.ToString(); 
                tbl2.Rows[row.RowIndex]["s8"] = ((TextBox)row.FindControl("lblgvs8")).Text.ToString(); 
                tbl2.Rows[row.RowIndex]["s9"] = ((TextBox)row.FindControl("lblgvs9")).Text.ToString(); 
                tbl2.Rows[row.RowIndex]["s10"] = ((TextBox)row.FindControl("lblgvs10")).Text.ToString(); 

            }
            ViewState["tblSmpleSizes"] = tbl2;
            this.SmpSize();
        }

        protected void LbtnSampleImport_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string inqno = this.ddlinqno.SelectedValue.ToString();
            DataSet result = Merdata.GetTransInfo(comcod, "SP_ENTRY_CONSUMPTION", "IMPORT_MATERIALS_FROM_SAMPLING", inqno);
            if (result ==null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Nothing to Import');", true);


            }


            DataTable tbl2 = (DataTable)ViewState["tblstdcost"];
          
           string colorid = this.ddlcolor.SelectedValue.ToString();
            string sizeid = this.ddlconsize.SelectedValue.ToString();
           


            foreach (DataRow item in result.Tables[0].Rows)
            {
                //    if (item.Selected)
                //    {

                //compcode = item.Value;
                //compname = item.Text;

                DataRow[] dr = tbl2.Select("rescode='" + item["rsircode"] + "' and spcfcode='" + item["spcfcode"] + "' and colorid='" + colorid + "' and sizeid='" + sizeid + "' and compcode='" + item["compcode"] + "'");
            // DataRow[] dr = tbl2.Select("rescode='" + rescod + "' and  compcode='" + compcode + "'" + "' and spcfcode='" + Specification + "'");
            if (dr.Length > 0)
            {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Selected Component Already Added');", true);


                return;
            }
            else
            {
                DataRow dr1 = tbl2.NewRow();
                dr1["procode"] = "000000000000";
                dr1["prodesc"] = "None";
                dr1["rescode"] = item["rsircode"];
                dr1["resdesc"] = item["rsirdesc"];
                dr1["resunit"] = item["rsirunit"];
                dr1["conqty"] = item["conqty"];
                dr1["westpc"] = 0;
                dr1["qty"] = item["conqty"];
                dr1["rate"] = item["rate"];
                dr1["amt"] = item["stdamt"];
                dr1["convrate"] = 0;
                dr1["percnt"] = item["percnt"];
                dr1["colorid"] = colorid;
                dr1["sizeid"] = sizeid;
                // dr1["rawmatname"] = this.ddlResourcesCost.SelectedItem.ToString().Substring(13);
                dr1["spcfcode"] = item["spcfcode"]; 
                dr1["spcfdesc"] = item["spcfdesc"];
                dr1["compname"] = item["compdesc"];
                dr1["compcode"] = item["compcode"]; ;
                dr1["cfrate"] = item["cfrate"];
                    dr1["fcfrate"] = item["rate"];
                    tbl2.Rows.Add(dr1);
            }
            //    }
            }




            ViewState["tblstdcost"] = tbl2;
            this.Data_Bind();
        }
    }
}
