using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using SPELIB;
using SPERDLC;

using Microsoft.Reporting.WinForms;


namespace SPEWEB.F_19_EXP
{
    public partial class ExportMgt : System.Web.UI.Page
    {
        public string prevsize { get; set; }
        public int noRowSpan { get; set; }
        public double Stockqty { get; set; }

        ProcessAccess proc1 = new ProcessAccess();
        public static double ToCost, OrdrVal, toqty, ToCostPer, ToCostPerM, totalcmPer;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetBuyer();

                string comcod = this.GetComeCode();

                this.ShowShiplineType();
                this.Get_Shipment();
                this.ShowOtherInfo();
                this.GetDescriptionGds();
                this.ShowDelMode();
                this.GetSeason();
                ((Label)this.Master.FindControl("lblTitle")).Text = " EXPORT";
                this.txtExFact.Text = "";
                //  this.txtExpDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CommonButton();

                if (this.Request.QueryString["actcode"].Length != 0)
                {
                    this.PreviousList();

                    this.lbtnOk_Click(null, null);
                }


            }
        }

        private void GetBuyer()
        {
            string comcod = this.GetComeCode();
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_MER_PROANALYSIS", "GET_BUYER_LIST", "", "", "", "", "", "",
                "", "", "");
            this.ddlBuyer.DataTextField = "sirdesc";
            this.ddlBuyer.DataValueField = "sircode";
            this.ddlBuyer.DataSource = ds2.Tables[0];
            this.ddlBuyer.DataBind();
            this.ddlBuyer_SelectedIndexChanged(null, null);
        }

        protected void ddlBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLCCode();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkbtnSave_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Text = "Delete Selected";
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Click += new EventHandler(lnkbtnTranList_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lnkbtnTranList_Click(object sender, EventArgs e)
        {


            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];
            List<int> lstRemove = new List<int>();
            for (int i = 0; i < gvSalCon.Rows.Count; i++)
            {
                if (((CheckBox)this.gvSalCon.Rows[i].FindControl("chkCol")).Checked)
                {
                    lstRemove.Add(i);
                }
            }
            lst.RemoveAll(x => lstRemove.Contains(lst.IndexOf(x)));
            ViewState["tblexport"] = lst;
            this.Data_Bind();
        }

        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;

            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).OnClientClick = "return confirm('Do You Want To Delete Selected Item?')";

            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = true;
            //((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;


            //((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = true;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

            //  ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Text= "Calculation";


        }
        private void ShowDelMode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds6 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "GETDELMODE", "", "", "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
            this.ddlDelMode.DataTextField = "gdesc";
            this.ddlDelMode.DataValueField = "gcod";
            this.ddlDelMode.DataSource = ds6.Tables[0];
            this.ddlDelMode.DataBind();

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetLCCode()
        {

            string comcod = this.GetComeCode();
            string filter = "%";
            string Buyer = this.ddlBuyer.SelectedValue.ToString();

            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_INV_STDANA", "GETORDERMLCCOD", filter, Buyer, "", "", "", "", "", "");
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();
            ViewState["tbllcorder"] = lst;
            this.ddlmlccode.DataSource = lst.Select(m => new { m.mlccod, m.mlcdesc }).Distinct().ToList();
            this.ddlmlccode.DataTextField = "mlcdesc";
            this.ddlmlccode.DataValueField = "mlccod";
            this.ddlmlccode.DataBind();

            //if (Request.QueryString["actcode"].ToString() != "")
            //{
            //    this.ddlmlccode.SelectedValue = this.Request.QueryString["actcode"].ToString();
            // this.ddlmlccode.Enabled = false;
            //   }
            ds2.Dispose();
            this.ddlmlccode_SelectedIndexChanged(null, null);
        }
        protected void ddlmlccode_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tbllcorder"];
            string mlccode = this.ddlmlccode.SelectedValue.ToString();
            this.dllorderType.DataSource = tbl1.FindAll(x => x.mlccod == mlccode);
            this.dllorderType.DataTextField = "rdaydesc";
            this.dllorderType.DataValueField = "rdayid";
            this.dllorderType.DataBind();

            this.dllorderType_SelectedIndexChanged(null, null);


        }

        protected void dllorderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string mlccode = this.ddlmlccode.SelectedValue.ToString();
            string rDayid = this.dllorderType.SelectedValue.ToString();
            string buyer = (this.ddlBuyer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBuyer.SelectedValue.ToString() + "%";
            string date = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_EXPORT_STYLE_INFO", mlccode, rDayid, buyer, date, "", "", "", "");
            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> lst = ds2.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();

            if (lst.Count == 0)
                return;

            this.ddlprocode.DataSource = lst.Select(m => new { m.style1, m.styledesc1 }).Distinct().ToList();
            this.ddlprocode.DataTextField = "styledesc1";
            this.ddlprocode.DataValueField = "style1";
            this.ddlprocode.DataBind();

            ViewState["tblmlcorder"] = lst;
            ddlprocode_SelectedIndexChanged(null, null);

        }

        private void ShowShiplineType()
        {
            string comcod = GetComeCode();
            DataSet ds6 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "GETSHIPLINETYPE", "", "", "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
            this.DDLShipmentType.DataTextField = "sirdesc";
            this.DDLShipmentType.DataValueField = "sircode";
            this.DDLShipmentType.DataSource = ds6.Tables[0];
            this.DDLShipmentType.DataBind();

        }

        private void Get_Shipment()
        {
            string comcod = GetComeCode();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "ExisLCOrderShip", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.DDLShipment.DataTextField = "ShipDesc";
            this.DDLShipment.DataValueField = "shipmid";
            this.DDLShipment.DataSource = ds1.Tables[2];
            this.DDLShipment.DataBind();
        }

        private void ShowOtherInfo()
        {
            string comcod = GetComeCode();
            DataSet ds6 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_DOCS", "GETCOUNTRYPORT", "", "", "", "", "", "", "", "", "");
            if (ds6 == null)
                return;
            this.ddlCountry.DataTextField = "name";
            this.ddlCountry.DataValueField = "id";
            this.ddlCountry.DataSource = ds6.Tables[0];
            this.ddlCountry.DataBind();

            this.ddlPortLoad.DataTextField = "sirdesc";
            this.ddlPortLoad.DataValueField = "sircode";
            this.ddlPortLoad.DataSource = ds6.Tables[1];
            this.ddlPortLoad.DataBind();

            this.ddlPortDis.DataTextField = "sirdesc";
            this.ddlPortDis.DataValueField = "sircode";
            this.ddlPortDis.DataSource = ds6.Tables[2];
            this.ddlPortDis.DataBind();

        }

        private void GetDescriptionGds()
        {
            string comcod = GetComeCode();
            DataSet ds7 = proc1.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GET_LC_GEN_INFO", "59%", "", "", "", "", "", "", "", "", "");

            if (ds7 == null)
                return;

            this.ddlDesGrp.DataTextField = "gdesc";
            this.ddlDesGrp.DataValueField = "gcod";
            this.ddlDesGrp.DataSource = ds7.Tables[0];
            this.ddlDesGrp.DataBind();

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.CommercialInvoice();
        }

        private void CommercialInvoice()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Date = this.txtdate.Text;


            string Expno = this.lblCurNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtdate.Text.Trim(), 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            //DataSet ds1 = proc1.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "RPTEXPORTDOC", Expno, "", "", "", "", "", "", "", "");
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_EXPORT_INFO", Expno, "", "", "", "", "", "", "", "");

            var lst1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>();
            var lst2 = ds1.Tables[2].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassFrdletter>();

            string InvoiceNo = ds1.Tables[0].Rows[0]["invno"].ToString();
            string InvoiceDate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["invdate"]).ToString("dd-MMM-yyyy");

            string LcNo = ds1.Tables[1].Rows[0]["lcno"].ToString();
            string LcDate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["lcisudat"]).ToString("dd-MMM-yyyy");

            string proformaInvNo = "";
            string proformdate = "";

            //string ExpNo = ds1.Tables[0].Rows[0]["invno"].ToString();
            //string ExpDate = Convert.ToDateTime(ds1.Tables[0].Rows[0]["invdate"]).ToString("dd-MMM-yyyy");
            string LcIssuBnk = ds1.Tables[1].Rows[0]["isubankname"].ToString();
            string LcIssuBnkAdd = ds1.Tables[1].Rows[0]["isubankadd"].ToString();
            string BeniBank = ds1.Tables[1].Rows[0]["benibank"].ToString();
            string BeBankAdd = ds1.Tables[1].Rows[0]["benibankAdd"].ToString();
            string BeniBankSw = ds1.Tables[1].Rows[0]["benibanksw"].ToString();
            string untoOrder = ds1.Tables[1].Rows[0]["rcvbankname"].ToString();
            string untoOrderAdd = ds1.Tables[1].Rows[0]["rcvbankadd"].ToString();
            string comaddf = ds1.Tables[1].Rows[0]["comaddf"].ToString();

            string ExpRegNo = ds1.Tables[1].Rows[0]["expno"].ToString(); ;
            string ExpRegDt = Convert.ToDateTime(ds1.Tables[1].Rows[0]["expdate"]).ToString("dd-MMM-yyyy");
            string Orgin = "Bangladesh";
            string ModeofShip = ds1.Tables[1].Rows[0]["shiptypedesc"].ToString();
            string Shipfrom = ds1.Tables[1].Rows[0]["exloaddesc"].ToString();
            string Shipto = ds1.Tables[1].Rows[0]["exdisdesc"].ToString();
            string HscCod = ds1.Tables[1].Rows[0]["hscode"].ToString();
            string unit = ds1.Tables[1].Rows[0]["stylunit"].ToString();

            string shipmentdat = Convert.ToDateTime(ds1.Tables[1].Rows[0]["invdate"]).ToString("dd-MMM-yyyy");
            string Shipper = comnam;
            string shippadd = ds1.Tables[1].Rows[0]["comaddf"].ToString();
            //string comsname = lst1[0].comsnam.ToString();
            //var genlist = lst1.FindAll(p => p.gencod == "010100102003");  
            string ExFDate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["exfacdt"]).ToString("dd-MMM-yyyy");
            string ShipMarks = ds1.Tables[1].Rows[0]["shipmarks"].ToString();
            string Invrefno = ds1.Tables[1].Rows[0]["invrefno"].ToString();
            string PaymentTerms = ds1.Tables[1].Rows[0]["payterm"].ToString();


            string notifyName = ds1.Tables[1].Rows[0]["notpartydesc"].ToString();
            string notifyadd = ds1.Tables[1].Rows[0]["notpadd"].ToString();
            string notifyName1 = ds1.Tables[1].Rows[0]["notpartydesc1"].ToString();
            string notifyadd1 = ds1.Tables[1].Rows[0]["notpadd1"].ToString();

            string buyername = ds1.Tables[1].Rows[0]["custdesc"].ToString();
            string buyeradd = ds1.Tables[1].Rows[0]["custadd"].ToString();
            //string lcvalue = Convert.ToDouble(ds1.Tables[1].Rows[0]["lcval"]).ToString("#,##0.00;(#,##0.00); ");
            string currency = ds1.Tables[1].Rows[0]["currency"].ToString();

            string remarks = ds1.Tables[1].Rows[0]["remarks"].ToString();
            string crtnRemarks = ds1.Tables[1].Rows[0]["cartonremarks"].ToString();

            string consignee = notifyName;
            string consigneeAdd = notifyadd;
            double NetAmt = Convert.ToDouble(lst1.Select(p => p.stylamt).Sum());
            double tctn = Convert.ToDouble(lst1.Select(p => p.totlctn).Sum());
            double twgt = Convert.ToDouble(lst1.Select(p => p.tnetwth).Sum());
            double tqty = Convert.ToDouble(lst1.Select(p => p.totlprs).Sum());

            double lcvalue = Convert.ToDouble(lst1.Select(p => p.stylamt).Sum());
            double disvalue = Convert.ToDouble(ds1.Tables[1].Rows[0]["disamt"]);


            LocalReport rpt1 = new LocalReport();
            string RptType = this.RadioButtonList1.SelectedIndex.ToString();
            string printFormat = ds1.Tables[1].Rows[0]["printformat"].ToString();

            switch (RptType)
            {
                case "0":

                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('ExpPrint.aspx?Type=ExpInvoice&genno=" + Expno + "', target='_blank');</script>";

                    //rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptCommInvoice", lst1.FindAll(p => p.totlprs > 0), null, null);

                    //rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    //rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    //rpt1.SetParameters(new ReportParameter("comaddf", comaddf));
                    //rpt1.SetParameters(new ReportParameter("InvoiceNo",  InvoiceNo));
                    //rpt1.SetParameters(new ReportParameter("LcNo",  LcNo));
                    //rpt1.SetParameters(new ReportParameter("invrefno", Invrefno));
                    //rpt1.SetParameters(new ReportParameter("InvoiceDate",  InvoiceDate));
                    //rpt1.SetParameters(new ReportParameter("LcDate", "DT: " + LcDate));


                    ////rpt1.SetParameters(new ReportParameter("proformaInvNo",  proformaInvNo));
                    ////rpt1.SetParameters(new ReportParameter("proformdate", "DT: " + proformdate));
                    ////rpt1.SetParameters(new ReportParameter("ExpNo",  ExpNo));
                    ////rpt1.SetParameters(new ReportParameter("ExpDate", "DT: " + ExpDate));
                    //rpt1.SetParameters(new ReportParameter("BeniBank", BeniBank));
                    //rpt1.SetParameters(new ReportParameter("BeBankAdd", BeBankAdd));
                    //rpt1.SetParameters(new ReportParameter("BeniBankSw", BeniBankSw));
                    ////rpt1.SetParameters(new ReportParameter("untoOrder",  untoOrder));
                    ////rpt1.SetParameters(new ReportParameter("untoOrderAdd", untoOrderAdd));
                    //rpt1.SetParameters(new ReportParameter("ExpRegNo",  ExpRegNo));
                    //rpt1.SetParameters(new ReportParameter("ExpRegDt", "DT: " + ExpRegDt));

                    //rpt1.SetParameters(new ReportParameter("Orgin",  Orgin));
                    //rpt1.SetParameters(new ReportParameter("ModeofShip",  ModeofShip));
                    //rpt1.SetParameters(new ReportParameter("Shipfrom",  Shipfrom));
                    //rpt1.SetParameters(new ReportParameter("Shipto",  Shipto));
                    //rpt1.SetParameters(new ReportParameter("HscCod",  HscCod));
                    //rpt1.SetParameters(new ReportParameter("shipmentdat",  shipmentdat));

                    //rpt1.SetParameters(new ReportParameter("Shipper", Shipper));
                    //rpt1.SetParameters(new ReportParameter("shippadd", shippadd));


                    //rpt1.SetParameters(new ReportParameter("notifyName", notifyName));
                    //rpt1.SetParameters(new ReportParameter("notifyadd", notifyadd));
                    //rpt1.SetParameters(new ReportParameter("notifyName1", notifyName1));
                    //rpt1.SetParameters(new ReportParameter("notifyadd1", notifyadd1));
                    ////rpt1.SetParameters(new ReportParameter("consignee", consignee));
                    ////rpt1.SetParameters(new ReportParameter("consigneeAdd", consigneeAdd));
                    //rpt1.SetParameters(new ReportParameter("buyername", buyername));
                    //rpt1.SetParameters(new ReportParameter("buyeradd", buyeradd));

                    ////rpt1.SetParameters(new ReportParameter("remarks", remarks));

                    //rpt1.SetParameters(new ReportParameter("Fterms", ""));
                    //rpt1.SetParameters(new ReportParameter("ShipMarks", ShipMarks));
                    //rpt1.SetParameters(new ReportParameter("ExFDate", ExFDate));

                    //rpt1.SetParameters(new ReportParameter("txtDis","Less Discount : " + Convert.ToDouble(ds1.Tables[1].Rows[0]["disper"]).ToString("#,##0;(#,##0); ")+ " %"));
                    //rpt1.SetParameters(new ReportParameter("DisAmt", Convert.ToDouble(ds1.Tables[1].Rows[0]["disamt"]).ToString("#,##0.00;(#,##0.00); ")));
                    //rpt1.SetParameters(new ReportParameter("txttAmt", "Grand Total :"));
                    //rpt1.SetParameters(new ReportParameter("TAmt", (NetAmt - disvalue).ToString("#,##0.00;(#,##0.00); ")));


                    //rpt1.SetParameters(new ReportParameter("RptTitle", "COMMERCIAL INVOICE"));
                    ////rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
                    //rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(NetAmt - disvalue), 4)));

                    break;
                case "1": ///Frd Letter

                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptFrdLetter", lst2, null, null);

                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("comaddf", comaddf));
                    rpt1.SetParameters(new ReportParameter("date", "Date :" + Date));
                    rpt1.SetParameters(new ReportParameter("untoOrder", untoOrder));
                    rpt1.SetParameters(new ReportParameter("untoOrderAdd", untoOrderAdd));
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Negotiation of Export Documents of " + currency + " " + NetAmt.ToString() + " under L/C No. " + LcNo));

                    rpt1.SetParameters(new ReportParameter("body", "Enclosed please findherewith the following doccuments duly completed, sealed and signed by us. Please negotiate the documents at the earliest. (Send by DHL)"));

                    rpt1.SetParameters(new ReportParameter("fbody", "We would appreciate if you would kindly purchase our above mentioned Bill and credir all proceeds to our following accounts maintained with you"));


                    rpt1.SetParameters(new ReportParameter("forcomnam", "For " + comnam));


                    break;
                case "2":
                    if (comcod == "5305" || comcod == "5306")
                    {
                        if (printFormat == "0")
                        {
                            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptPackingList", lst1, null, null);
                        }
                        else if (printFormat == "1")
                        {
                            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptPackingListFBFrmt1", lst1, null, null);
                        }
                        else if (printFormat == "2")
                        {
                            //var lst3 = new List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>();
                            int i = 0;
                            string[] sizes = new string[lst1.Count];
                            foreach (var item in lst1)
                            {
                                sizes[i] = item.sizedesc.ToString();
                                i++;
                                switch (i)
                                {
                                    case 1:
                                        lst1[0].s1 = item.totlprs;
                                        break;

                                    case 2:
                                        lst1[0].s2 = item.totlprs;
                                        break;

                                    case 3:
                                        lst1[0].s3 = item.totlprs;
                                        break;

                                    case 4:
                                        lst1[0].s4 = item.totlprs;
                                        break;

                                    case 5:
                                        lst1[0].s5 = item.totlprs;
                                        break;

                                    case 6:
                                        lst1[0].s6 = item.totlprs;
                                        break;

                                    case 7:
                                        lst1[0].s7 = item.totlprs;
                                        break;

                                    case 8:
                                        lst1[0].s8 = item.totlprs;
                                        break;

                                    case 9:
                                        lst1[0].s9 = item.totlprs;
                                        break;

                                    case 10:
                                        lst1[0].s10 = item.totlprs;
                                        break;

                                    case 11:
                                        lst1[0].s11 = item.totlprs;
                                        break;

                                    case 12:
                                        lst1[0].s12 = item.totlprs;
                                        break;

                                    case 13:
                                        lst1[0].s13 = item.totlprs;
                                        break;

                                    case 14:
                                        lst1[0].s14 = item.totlprs;
                                        break;

                                    case 15:
                                        lst1[0].s15 = item.totlprs;
                                        break;

                                    case 16:
                                        lst1[0].s16 = item.totlprs;
                                        break;

                                    case 17:
                                        lst1[0].s17 = item.totlprs;
                                        break;

                                    case 18:
                                        lst1[0].s18 = item.totlprs;
                                        break;

                                    case 19:
                                        lst1[0].s19 = item.totlprs;
                                        break;

                                    case 20:
                                        lst1[0].s20 = item.totlprs;
                                        break;

                                    default:
                                        break;
                                }
                            }

                            lst1 = lst1
                                .GroupBy(pl => new { pl.artno, pl.mlccod, pl.rdayid, pl.ordrno, pl.po, pl.styleid, pl.colorid })
                                .Select(x => new SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc
                                {
                                    mlccod = lst1[0].mlccod,
                                    mlcdesc = lst1[0].mlcdesc,
                                    styleid = lst1[0].styleid,
                                    styledesc = lst1[0].styledesc,
                                    colorid = lst1[0].colorid,
                                    colordesc = lst1[0].colordesc,
                                    stylunit = lst1[0].stylunit,
                                    sizeid = lst1[0].sizeid,
                                    sizedesc = lst1[0].sizedesc,
                                    ordrno = lst1[0].ordrno,
                                    artno = lst1[0].artno,
                                    itmamt = lst1[0].itmamt,
                                    hscode = lst1[0].hscode,
                                    ordrqty = lst1[0].ordrqty,
                                    rate = lst1[0].rate,
                                    stylamt = lst1[0].stylamt,
                                    pperctnqty = lst1.Sum(c => c.totlprs) / lst1.Sum(c => c.totlctn),
                                    totlprs = lst1.Sum(c => c.totlprs),
                                    totlctn = lst1.Sum(c => c.totlctn),
                                    gwperctn = lst1[0].gwperctn,
                                    tgwwth = lst1[0].tgwwth,
                                    nwperctn = lst1[0].nwperctn,
                                    tnetwth = lst1[0].tnetwth,
                                    dimenctn = lst1[0].dimenctn,
                                    customercod = lst1[0].customercod,
                                    customer = lst1[0].customer,
                                    forma = lst1[0].forma,
                                    upper = lst1[0].upper,
                                    smpltyp = lst1[0].smpltyp,
                                    cbm = lst1[0].cbm,
                                    qty = lst1[0].qty,
                                    po = lst1[0].po,
                                    totalgw = lst1[0].totalgw,
                                    s1 = lst1[0].s1,
                                    s2 = lst1[0].s2,
                                    s3 = lst1[0].s3,
                                    s4 = lst1[0].s4,
                                    s5 = lst1[0].s5,
                                    s6 = lst1[0].s6,
                                    s7 = lst1[0].s7,
                                    s8 = lst1[0].s8,
                                    s10 = lst1[0].s10,
                                    s11 = lst1[0].s11,
                                    s12 = lst1[0].s12,
                                    s13 = lst1[0].s13,
                                    s14 = lst1[0].s14,
                                    s15 = lst1[0].s15,
                                    s16 = lst1[0].s16,
                                    s17 = lst1[0].s17,
                                    s18 = lst1[0].s18,
                                    s19 = lst1[0].s19,
                                    s20 = lst1[0].s20,
                                }).ToList();

                            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptPackingListFBFrmt2", lst1, null, null);

                            i = 0;
                            foreach (var size in sizes)
                            {
                                i++;
                                rpt1.SetParameters(new ReportParameter("s" + (i).ToString(), size));
                            }

                            double totalPairs = Convert.ToDouble(lst1.Select(p => p.totlprs).Sum());
                            string crtnSmry = tctn.ToString() + " CARTONS = " + totalPairs.ToString() + " PAIRS";

                            rpt1.SetParameters(new ReportParameter("crtnSmry", crtnSmry));
                            rpt1.SetParameters(new ReportParameter("shipmarks", ShipMarks));
                            rpt1.SetParameters(new ReportParameter("crtnRemarks", crtnRemarks));


                        }
                        else
                        {
                            //rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptPackingListFBFrmt1", lst1, null, null);
                            return;
                        }

                    }
                    else
                    {
                        rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptPackingList", lst1, null, null);
                    }

                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("InvoiceNo", InvoiceNo));
                    rpt1.SetParameters(new ReportParameter("LcNo", LcNo));

                    rpt1.SetParameters(new ReportParameter("InvoiceDate", "DT: " + InvoiceDate));
                    rpt1.SetParameters(new ReportParameter("LcDate", "DT: " + LcDate));
                    rpt1.SetParameters(new ReportParameter("proformaInvNo", proformaInvNo));
                    rpt1.SetParameters(new ReportParameter("proformdate", "DT: " + proformdate));
                    rpt1.SetParameters(new ReportParameter("ExpNo", ExpRegNo));
                    rpt1.SetParameters(new ReportParameter("ExpDate", "DT: " + ExpRegDt));
                    rpt1.SetParameters(new ReportParameter("LcIssuBnk", LcIssuBnk));
                    rpt1.SetParameters(new ReportParameter("LcIssuBnkAdd", LcIssuBnkAdd));
                    rpt1.SetParameters(new ReportParameter("untoOrder", untoOrder));
                    rpt1.SetParameters(new ReportParameter("untoOrderAdd", untoOrderAdd));
                    rpt1.SetParameters(new ReportParameter("ExpRegNo", ExpRegNo));
                    rpt1.SetParameters(new ReportParameter("ExpRegDt", "DT: " + ExpRegDt));

                    rpt1.SetParameters(new ReportParameter("Orgin", Orgin));
                    rpt1.SetParameters(new ReportParameter("ModeofShip", ModeofShip));
                    rpt1.SetParameters(new ReportParameter("Shipfrom", Shipfrom));
                    rpt1.SetParameters(new ReportParameter("Shipto", Shipto));
                    rpt1.SetParameters(new ReportParameter("HscCod", HscCod));
                    rpt1.SetParameters(new ReportParameter("shipmentdat", shipmentdat));
                    rpt1.SetParameters(new ReportParameter("Shipper", Shipper));
                    rpt1.SetParameters(new ReportParameter("shippadd", shippadd));

                    rpt1.SetParameters(new ReportParameter("notifyName", notifyName));
                    rpt1.SetParameters(new ReportParameter("notifyadd", notifyadd));
                    rpt1.SetParameters(new ReportParameter("notifyName1", notifyName));
                    rpt1.SetParameters(new ReportParameter("notifyadd1", notifyadd));
                    rpt1.SetParameters(new ReportParameter("consignee", consignee));
                    rpt1.SetParameters(new ReportParameter("consigneeAdd", consigneeAdd));
                    rpt1.SetParameters(new ReportParameter("buyername", buyername));
                    rpt1.SetParameters(new ReportParameter("buyeradd", buyeradd));

                    rpt1.SetParameters(new ReportParameter("remarks", remarks));

                    rpt1.SetParameters(new ReportParameter("RptTitle", "PACKING LIST"));
                    rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
                    rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(NetAmt), 2)));

                    break;
                case "3": ///Bill of Exchange
                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptBillOfExchange", lst1, null, null);

                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("RptTitle", "BILL OF EXCHANGE"));
                    rpt1.SetParameters(new ReportParameter("InvoiceNo", InvoiceNo));
                    rpt1.SetParameters(new ReportParameter("date", "Date :" + Date));
                    rpt1.SetParameters(new ReportParameter("LcNo", LcNo));
                    rpt1.SetParameters(new ReportParameter("LcDate", "DT: " + LcDate));
                    rpt1.SetParameters(new ReportParameter("Lcval", "For " + currency + " " + lcvalue));
                    rpt1.SetParameters(new ReportParameter("invamt", currency + " " + NetAmt.ToString()));

                    string billbody = "First of Exchange ( Second of the same tenor and date being unpaid ) pay to the order of the " + untoOrder + untoOrderAdd + ", the sum  " + ASTUtility.Trans(Math.Round(NetAmt), 2) + " value received and place same of the account of invoice no ." + InvoiceNo + " DT: " + InvoiceDate + " for " + tctn.ToString() + " Cartons Containing " + tqty.ToString() + unit + " Shoes";

                    rpt1.SetParameters(new ReportParameter("body", billbody));

                    rpt1.SetParameters(new ReportParameter("LcIssuBnk", LcIssuBnk));
                    rpt1.SetParameters(new ReportParameter("LcIssuBnkAdd", LcIssuBnkAdd));



                    break;
                case "4":
                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptGSP", lst1, null, null);

                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("epbreg", ""));

                    rpt1.SetParameters(new ReportParameter("notifyName", notifyName));
                    rpt1.SetParameters(new ReportParameter("notifyadd", notifyadd));

                    rpt1.SetParameters(new ReportParameter("ModeofShip", "By " + ModeofShip));
                    rpt1.SetParameters(new ReportParameter("Shipfrom", "From: " + Shipfrom));
                    rpt1.SetParameters(new ReportParameter("Shipto", "To: " + Shipto));
                    rpt1.SetParameters(new ReportParameter("blno", "Bl No: "));
                    rpt1.SetParameters(new ReportParameter("blDate", "Dated : "));
                    rpt1.SetParameters(new ReportParameter("contno", "Container No : "));

                    rpt1.SetParameters(new ReportParameter("tctn", tctn.ToString() + " Cartons"));
                    rpt1.SetParameters(new ReportParameter("twgt", twgt.ToString()));
                    rpt1.SetParameters(new ReportParameter("tqty", tqty.ToString()));
                    rpt1.SetParameters(new ReportParameter("ctnno", remarks));
                    rpt1.SetParameters(new ReportParameter("unit", unit));
                    rpt1.SetParameters(new ReportParameter("prod", "Shoes"));

                    rpt1.SetParameters(new ReportParameter("InvoiceNo", InvoiceNo));
                    rpt1.SetParameters(new ReportParameter("InvoiceDate", "DT: " + InvoiceDate));

                    string orderno = "Order No: " + ds1.Tables[0].Rows[0]["ordno"].ToString();
                    string artno = "Article No: " + ds1.Tables[0].Rows[0]["artno"].ToString();


                    rpt1.SetParameters(new ReportParameter("orderno", orderno));
                    rpt1.SetParameters(new ReportParameter("artno", artno));
                    rpt1.SetParameters(new ReportParameter("ExpNo", "Exp No: " + ExpRegNo));
                    rpt1.SetParameters(new ReportParameter("ExpDate", "DT: " + ExpRegDt));
                    rpt1.SetParameters(new ReportParameter("LcNo", "L/C No: " + LcNo));
                    rpt1.SetParameters(new ReportParameter("LcDate", "DT: " + LcDate));
                    rpt1.SetParameters(new ReportParameter("binno", "BIN No: "));
                    rpt1.SetParameters(new ReportParameter("shipbillno", "Shipping Bill No: "));
                    rpt1.SetParameters(new ReportParameter("shipbilldate", "Dated: "));

                    break;
                case "5":
                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptBenefiDecla", lst1, null, null);

                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("date", "Date :" + Date));
                    rpt1.SetParameters(new ReportParameter("header", "Beneficiary's Declaration"));


                    string Pino = ""; string pidate = " Dated. ";

                    string body = "We, " + comnam + ", " + comadd + " do hereby declare that goods shipped and invoiced are confirm to those described in the relative proforma invoice no. :" + Pino + pidate + " whice we shipped under commercial invoice no. " + InvoiceNo + " DT: " + InvoiceDate + " L/C No: " + LcNo + " DT: " + LcDate;

                    rpt1.SetParameters(new ReportParameter("body", body));

                    rpt1.SetParameters(new ReportParameter("forcomnam", "For " + comnam));


                    break;
                case "6":
                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptBenefiDecla", lst1, null, null);

                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("date", "Date :" + Date));
                    rpt1.SetParameters(new ReportParameter("header", "Beneficiary's Certificate"));


                    string body1 = "We, " + comnam + ", " + comadd + " do hereby Certify that Issuing Bank Confirming their acceptance and/or no-acceptance of amendment made under this credit quoting amendment number # anainst" + " L/C No: " + LcNo + " DT: " + LcDate;

                    rpt1.SetParameters(new ReportParameter("body", body1));

                    rpt1.SetParameters(new ReportParameter("forcomnam", "For " + comnam));
                    break;
            }



            if (RptType != "0")
            {
                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
        }

        private void Data_Bind()
        {
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            //IEnumerable<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> Query = (from Export in lst
            //                                                                   orderby Export.mlccod ascending
            //                                                                   select Export);


            if (lst.Count == 0)
            {
                this.gvSalCon.DataSource = null;
                this.gvSalCon.DataBind();
                return;
            }
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst1 = lst.OrderBy(c => c.po).ToList();
            ViewState["tblexport"] = lst1;
            this.gvSalCon.DataSource = (lst1);
            this.gvSalCon.DataBind();
            this.Footcalculation();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblmlcorder"];
            string rdayid = this.dllorderType.SelectedValue.ToString();
            string style = this.ddlprocode.SelectedValue.ToString().Substring(0, 12);
            string color = this.ddlprocode.SelectedValue.ToString().Substring(12, 12);
            string sdino = this.ddlprocode.SelectedValue.ToString().Substring(24, 14);

            DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            var newlist = tbl1.FindAll(x => x.styleid == style && x.colorid == color && x.rdayid == rdayid && x.sdino == sdino);
            foreach (SPEENTITY.C_03_CostABgd.EclassSalesContact c1 in newlist)
            {

                string mlccode = c1.mlccod.ToString();
                string mlcdesc = c1.mlcdesc.ToString();
                string styleid = c1.styleid.ToString();
                string styledesc = c1.styledesc.ToString();
                string colorid = c1.colorid.ToString();
                string colordesc = c1.colordesc.ToString();
                string sizeid = c1.sizeid.ToString();
                string sizedesc = c1.sizedesc.ToString();
                double ordrqty = Convert.ToDouble(c1.ordrqty);
                double prdqty = Convert.ToDouble(c1.ordrqty1);
                double rate = Convert.ToDouble(c1.rate);
                string ordrno = c1.ordrno.ToString();
                string hscode = c1.hscode.ToString();
                double balqty = Convert.ToDouble(c1.balqty);
                string redayid = c1.rdayid.ToString();
                double itmamt = 0.00;
                string inqno = c1.sdino.ToString();
                double inprocqty = Convert.ToDouble(c1.inprocqty);

                var checklist = lst.FindAll(p => p.mlccod == mlccode && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.ordrno == ordrno && p.rdayid == redayid && p.ordno == inqno);
                if (checklist.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist');", true);
                    continue;
                }

                lst.Add(new SPEENTITY.C_19_Exp.EClassExpBO.EclassExport(mlccode, mlcdesc, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, ordrqty, rate, ordrno, hscode,
                    0.00, 0.00, 0.00, 0.00, 0.00, "", 0.00, 0.00, prdqty, balqty, redayid, itmamt, inqno, inprocqty));
            }
            ViewState["tblexport"] = lst;
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string comcod = this.GetComeCode();
            string dayid = this.dllorderType.SelectedValue.ToString();
            DataTable Tempdt;
            DataView Tempdv;
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "GETLCDETINFO", mlccod, dayid, "", "", "", "", "", "", "");
            Tempdt = ds2.Tables[0].Copy();
            Tempdv = Tempdt.DefaultView;
            Tempdv.RowFilter = ("gcod ='010100101009' or gcod ='010100101010'");
            Tempdt = Tempdv.ToTable();
            if ((Tempdt.Rows[0]["gdesc1"].ToString() != "") && (Tempdt.Rows[1]["gdesc1"].ToString() != ""))
            {
                this.Data_Bind();
                this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + dayid;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Add Conversion Rate');", true);
                this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + dayid;
            }
        }

        protected void AddAll_Click(object sender, EventArgs e)
        {
            DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> list3 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblmlcorder"];

            foreach (SPEENTITY.C_03_CostABgd.EclassSalesContact c1 in list3)
            {
                string mlccode = c1.mlccod.ToString();
                string mlcdesc = c1.mlcdesc.ToString();
                string styleid = c1.styleid.ToString();
                string styledesc = c1.styledesc.ToString();
                string colorid = c1.colorid.ToString();
                string colordesc = c1.colordesc.ToString();
                string sizeid = c1.sizeid.ToString();
                string sizedesc = c1.sizedesc.ToString();
                double ordrqty = Convert.ToDouble(c1.ordrqty);
                double prdqty = Convert.ToDouble(c1.ordrqty1);
                double rate = Convert.ToDouble(c1.rate);
                string ordrno = c1.ordrno.ToString();
                string hscode = c1.hscode.ToString();
                double balqty = Convert.ToDouble(c1.balqty);
                string rdayid = c1.rdayid.ToString();
                double itmamt = 0.00;
                double inprocqty = c1.inprocqty;
                string sdino = c1.sdino.ToString();
                var checklist = lst.FindAll(p => p.mlccod == mlccode && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.ordrno == ordrno && p.rdayid == rdayid && p.ordno == sdino);
                if (checklist.Count > 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Already Exist";
                    continue;
                }

                lst.Add(new SPEENTITY.C_19_Exp.EClassExpBO.EclassExport(mlccode, mlcdesc, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, ordrqty, rate, ordrno, hscode,
                 0.00, 0.00, 0.00, 0.00, 0.00, "", 0.00, 0.00, prdqty, balqty, rdayid, itmamt, sdino, inprocqty));

            }

            ViewState["tblexport"] = lst;
            this.Data_Bind();
        }

        protected void gvSalCon_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];
            int rowindex = (this.gvSalCon.PageSize) * (this.gvSalCon.PageIndex) + e.RowIndex;
            lst.RemoveAt(rowindex);
            ViewState["tblexport"] = lst;
            this.Data_Bind();
        }

        private void SaveValue()
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            for (int i = 0; i < this.gvSalCon.Rows.Count; i++)
            {
                string po = ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvpono")).Text.ToString();
                string wearhouse = ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvwearhouseno")).Text.ToString();




                double pperctnqty = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvpperctnqty")).Text.Trim());
                double totlprs = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvtotlprs")).Text.Trim());
                // double totlctn = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvtotlctn")).Text.Trim());
                double gwperctn = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvgwperctn")).Text.Trim());
                // double totalgw = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("txtgvtotalgw")).Text.Trim());
                double nwperctn = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("txtnwperctn")).Text.Trim());
                string dimenctn = Convert.ToString(((TextBox)this.gvSalCon.Rows[i].FindControl("txtdimenctn")).Text.Trim());
                double cbm = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("txtcbm")).Text.Trim());
                double rate = Convert.ToDouble("0" + ((TextBox)this.gvSalCon.Rows[i].FindControl("lblRate")).Text.Trim());
                lst[i].pperctnqty = pperctnqty;
                lst[i].totlprs = totlprs;
                lst[i].totlctn = (pperctnqty == 0) ? 0 : totlprs / pperctnqty;
                lst[i].gwperctn = gwperctn;
                lst[i].totalgw = (pperctnqty == 0) ? 0 : (totlprs / pperctnqty) * gwperctn;
                lst[i].nwperctn = nwperctn;
                lst[i].dimenctn = dimenctn;
                lst[i].cbm = cbm;
                lst[i].rate = rate;
                lst[i].itmamt = totlprs * rate;
                lst[i].po = po;
                lst[i].wearhouse = wearhouse;
            }
            ViewState["tblexport"] = lst;
        }
        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            SaveValue();
            this.Data_Bind();
        }

        protected void RefBtn_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblexport");
            this.gvSalCon.DataSource = null;
            this.gvSalCon.DataBind();


        }
        private List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> HiddenSameData(List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst)
        {

            //string slnum = dt.Rows[0]["slnum"].ToString();
            string styleid = "";
            string colorid = "";
            string sizeid = "";
            //var list22 = lst.OrderBy(m => m.styleid).ThenBy(m => m.colorid).ThenBy(m => m.sizeid).ToList();
            var list22 = lst.OrderBy(m => m.mlccod).ThenBy(m => m.styleid).ThenBy(m => m.colorid).ThenBy(m => m.sizeid).ToList();
            foreach (SPEENTITY.C_19_Exp.EClassExpBO.EclassExport c1 in list22)
            {
                if (styleid == c1.styleid.ToString())
                {
                    c1.styledesc = "";
                }
                if (styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString())
                {
                    c1.colordesc = "";
                }
                if (styleid == c1.styleid.ToString() && colorid == c1.colorid.ToString() && sizeid == c1.sizeid.ToString())
                {
                    c1.sizedesc = "";
                }

                styleid = c1.styleid.ToString();
                colorid = c1.colorid.ToString();
                sizeid = c1.sizeid.ToString();

            }
            ViewState["tblexport"] = list22;
            return list22;

        }
        protected void Get_INVNO()
        {

            string CurDate1 = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string mCPRNo = "NEWINV";
            if (this.ddlPrevList.Items.Count > 0)
                mCPRNo = this.ddlPrevList.SelectedValue.ToString();
            if (mCPRNo == "NEWINV")
            {
                string comcod = this.GetComeCode();
                DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_LAST_INVNO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);

                this.ddlPrevList.DataTextField = "maxno1";
                this.ddlPrevList.DataValueField = "maxno";
                this.ddlPrevList.DataSource = ds2.Tables[0];
                this.ddlPrevList.DataBind();
            }
        }

        private void lnkbtnSave_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('You have no permission');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string PostedDat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            //string rdayid = this.dllorderType.SelectedValue.ToString();
            string shipment = this.DDLShipment.SelectedValue.ToString();
            string shptype = this.DDLShipmentType.SelectedValue.ToString();
            string Country = this.ddlCountry.SelectedValue.ToString();
            string PortLoad = this.ddlPortLoad.SelectedValue.ToString();
            string PortDis = this.ddlPortDis.SelectedValue.ToString();
            string PackPln = this.ddlPackPlnList.SelectedValue.ToString();
            this.SaveValue();
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            if (this.ddlPrevList.Items.Count == 0)
                this.Get_INVNO();
            string invno = this.lblCurNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtdate.Text.Trim(), 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string invdate = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string exfactdate = (this.txtExFact.Text.Trim().Length > 0) ? Convert.ToDateTime(this.txtExFact.Text.Trim()).ToString("dd-MMM-yyyy") : "01-Jan-1900";
            string refno = this.txtRefNo.Text.Trim();
            string mNAR = this.txtBillNarr.Text.Trim();
            string descripGd = this.ddlDesGrp.SelectedValue;

            string blno = this.txtBlNo.Text.Trim();
            string bldate = this.txtBlDate.Text.Trim();

            string exprtrRefNo = this.txtExpRefNo.Text.Trim();
            string exprtrRefDate = this.txtExpRefDt.Text.Trim();

            string expno = this.txtExpNo.Text;
            string expDate = this.txtExpDate.Text;
            double Codpar = Convert.ToDouble("0" + this.txtDisPer.Text.Trim());
            double CodAmt = Convert.ToDouble("0" + this.txtDis.Text.Trim());

            string DelMode = this.ddlDelMode.SelectedValue.ToString();
            string DelDate = this.txtMDate.Text;

            string format = ddlFormat.SelectedValue;
            string crtnRmrks = "";

            double cartonno = Convert.ToDouble("0" + this.txtCrtnNo.Text.Trim());
            double gweight = Convert.ToDouble("0" + this.txtGweight.Text.Trim());
            double nweight = Convert.ToDouble("0" + this.txtNweight.Text.Trim());

            string proformaInv = this.txtProforma.Text.Trim();
            string notify2 = this.txtnotify2.Text.Trim();

            string type = this.Request.QueryString["Type"].ToString();
            DataTable dt1 = ASITUtility03.ListToDataTable(lst);
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";

            if (this.Request.QueryString["genno"].Length == 0)
            {
                if (refno.Length == 0)
                {
                    this.txtRefNo.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Invoice No. Should Not Be Empty');", true);
                    return;
                }
                DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "CHECK_DUPLICATE_REFNO", refno, "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    if (ds2.Tables[0].Rows[0]["invno"].ToString() != invno)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Invoice No. Already Exist');", true);
                        this.txtRefNo.Focus();
                        return;
                    }
                }
            }
            var temp = ds1.GetXml();
            bool result = proc1.UpdateXmlTransInfo(comcod, "SP_ENTRY_EXPORT", "UPDATE_EXPORT_INFO", ds1, null, null, invno, mlccod, invdate, refno, shipment, shptype, mNAR, 
                PostedDat, usrid, sessionid, trmid, type, exfactdate, Country, PortLoad, PortDis, expno, expDate, Codpar.ToString(), CodAmt.ToString(), DelMode, DelDate, format, 
                crtnRmrks, PackPln, blno, bldate, exprtrRefNo, exprtrRefDate, descripGd, cartonno.ToString(), gweight.ToString(), nweight.ToString(), proformaInv, notify2);
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Some Duplicate Or Invalid Format');", true);
                return;
            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {
                ViewState.Remove("tblexport");
                this.imgPreVious.Visible = true;
                this.ddlPrevList.Visible = true;
                this.ddlPrevList.Items.Clear();
                this.lblCurNo1.Text = "";
                this.txtCurNo2.Text = "";
                this.txtdate.Enabled = true;
                this.txtRefNo.Text = "";
                this.ddlmlccode.Enabled = true;
                this.txtBillNarr.Text = "";
                this.gvSalCon.DataSource = null;
                this.gvSalCon.DataBind();
                this.Panel2.Visible = false;
                this.DDLShipment.Enabled = true;
                this.DDLShipmentType.Enabled = true;
                this.export.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.txtExFact.Text = ""; //System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtExpNo.Text = "";
                this.txtExpDate.Text = "";
                this.SelectionPanel.Visible = false;
                this.Btnpanel.Visible = false;
                this.ddlBuyer.Enabled = true;//System.DateTime.Today.ToString("dd-MMM-yyyy");
                return;
            }
            this.imgPreVious.Visible = false;
            this.ddlPrevList.Visible = false;
            this.lblCurNo1.Enabled = true;
            this.txtCurNo2.Enabled = true;
            this.Panel2.Visible = true;
            //this.ddlmlccode.Enabled = false;
            this.DDLShipment.Enabled = false;
            this.DDLShipmentType.Enabled = false;
            this.export.Visible = true;
            this.lbtnOk.Text = "New";
            this.ddlBuyer.Enabled = false;
            this.SelectionPanel.Visible = true;
            this.Btnpanel.Visible = true;
            this.Get_Info();

        }

        protected void Get_Info()
        {

            try
            {
                string comcod = this.GetComeCode();
                string CurDate1 = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string mCPRNo = "NEWINV";
                if (this.ddlPrevList.Items.Count > 0)
                {
                    this.txtdate.Enabled = false;
                    mCPRNo = this.ddlPrevList.SelectedValue.ToString();

                }
                //else if (this.ddlPackPlnList.Items.Count > 0)
                //{
                //    mCPRNo = this.ddlPackPlnList.SelectedValue.ToString();

                //}
                this.HypStockCheck.NavigateUrl = "~/F_19_EXP/InvWiseStockChecker?genno=" + mCPRNo;
                DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_EXPORT_INFO", mCPRNo, "", "", "", "", "", "", "", "");
                var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>();

                if (lst == null)
                    return;

                if (mCPRNo == "NEWINV")
                {
                    DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_LAST_INVNO", CurDate1, "", "", "", "", "", "", "", "");
                    if (ds2 == null)
                        return;
                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);
                }

                ViewState["tblexport"] = lst;

                this.txtDisPer.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["disper"]).ToString("#,##0.0;(#,##0.0); ");
                this.txtDis.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["disamt"]).ToString("#,##0.00;(#,##0.00); ");

                //this.txtGTotal.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["invno1"]).ToString("#,##0.00;(#,##0.00); ");

                this.txtRefNo.Text = ds1.Tables[1].Rows[0]["invrefno"].ToString();

                this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["invno1"].ToString().Substring(0, 6); ; //lstord[0].promno1.ToString().Substring(0, 6);
                this.txtCurNo2.Text = ds1.Tables[1].Rows[0]["invno1"].ToString().Substring(6, 5); //lstord[0].promno1.ToString().Substring(6, 5);
                this.txtdate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["invdate"]).ToString("dd-MMM-yyyy");
                this.txtExFact.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["exfacdt"]).ToString("dd-MMM-yyyy");
                this.txtBillNarr.Text = ds1.Tables[1].Rows[0]["remarks"].ToString();

                this.ddlBuyer.SelectedValue = ds1.Tables[1].Rows[0]["custid"].ToString();

                //this.dllorderType.SelectedValue = ds1.Tables[1].Rows[0]["rdayid"].ToString();
                this.ddlFormat.SelectedValue = ds1.Tables[1].Rows[0]["printformat"].ToString();
                this.ddlDesGrp.SelectedValue = ds1.Tables[1].Rows[0]["typeofgoodsdesc"].ToString();
                this.txtExpNo.Text = ds1.Tables[1].Rows[0]["expno"].ToString();
                this.txtExpDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["expdate"]).ToString("dd-MMM-yyyy");
                this.txtBlNo.Text = ds1.Tables[1].Rows[0]["blno"].ToString();
                this.txtBlDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["bldate"]).ToString("dd-MMM-yyyy");
                this.txtExpRefNo.Text = ds1.Tables[1].Rows[0]["exporterrefno"].ToString();
                this.txtCrtnNo.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cartonno"]).ToString("#,##0;(#,##0); ");
                this.txtGweight.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["gweight"]).ToString("#,##0.0;(#,##0.0); ");
                this.txtNweight.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["nweight"]).ToString("#,##0.0;(#,##0.0); ");

                this.txtProforma.Text = ds1.Tables[1].Rows[0]["proforminv"].ToString();
                this.txtnotify2.Text = ds1.Tables[1].Rows[0]["notify2"].ToString();

                this.txtExpRefDt.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["exporterrefdate"]).ToString("dd-MMM-yyyy");
                this.ddlCountry.SelectedValue = ds1.Tables[1].Rows[0]["excountry"].ToString();
                this.ddlPortLoad.SelectedValue = ds1.Tables[1].Rows[0]["exloading"].ToString();
                this.ddlPortDis.SelectedValue = ds1.Tables[1].Rows[0]["exdischare"].ToString();

                this.ddlDelMode.SelectedValue = ds1.Tables[1].Rows[0]["delvtrm"].ToString();
                this.txtMDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["delvdate"]).ToString("dd-MMM-yyyy");
                this.ddlBuyer_SelectedIndexChanged(null, null);
                // this.ddlmlccode.SelectedValue = ds1.Tables[1].Rows[0]["mlccod"].ToString();
                this.ddlmlccode_SelectedIndexChanged(null, null);
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Ensure Selected Desire Buyer";
            }


        }

        protected void AddMore_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = (List<SPEENTITY.C_03_CostABgd.EclassSalesContact>)ViewState["tblmlcorder"];
            string rdayid = this.dllorderType.SelectedValue.ToString();
            string style = this.ddlprocode.SelectedValue.ToString().Substring(0, 12);
            string color = this.ddlprocode.SelectedValue.ToString().Substring(12, 12);
            string sdino = this.ddlprocode.SelectedValue.ToString().Substring(24, 14);

            DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            var newlist = tbl1.FindAll(x => x.styleid == style && x.colorid == color && x.rdayid == rdayid && x.sdino == sdino);

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            string mlccode = lst[RowIndex].mlccod.ToString();
            string mlcdesc = lst[RowIndex].mlcdesc.ToString();
            string styleid = lst[RowIndex].styleid.ToString();
            string styledesc = lst[RowIndex].styledesc.ToString();
            string colorid = lst[RowIndex].colorid.ToString();
            string colordesc = lst[RowIndex].colordesc.ToString();
            string sizeid = lst[RowIndex].sizeid.ToString();
            string sizedesc = lst[RowIndex].sizedesc.ToString();
            double ordrqty = Convert.ToDouble(lst[RowIndex].ordrqty);
            double rate = Convert.ToDouble(lst[RowIndex].rate);
            string ordrno = lst[RowIndex].ordrno.ToString();
            string ordno = lst[RowIndex].ordno.ToString();
            string hscode = lst[RowIndex].hscode.ToString();
            double balqty = Convert.ToDouble(lst[RowIndex].balqty);
            string redayid = lst[RowIndex].rdayid.ToString();
            double itmamt = 0.00;
            double inprocqty = lst[RowIndex].inprocqty;


            lst.Add(new SPEENTITY.C_19_Exp.EClassExpBO.EclassExport(mlccode, mlcdesc, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, ordrqty, rate, ordrno, hscode,
                    0.00, 0.00, 0.00, 0.00, 0.00, "", 0.00, 0.00, 0.00, balqty, redayid, itmamt, ordno, inprocqty));

            ViewState["tblexport"] = lst;
            this.Data_Bind();

        }

        protected void chkheadl_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvSalCon.Rows.Count; i++)
            {
                if (((CheckBox)this.gvSalCon.HeaderRow.FindControl("chkhead")).Checked)
                {
                    ((CheckBox)this.gvSalCon.Rows[i].FindControl("chkCol")).Checked = true;
                }
                else
                {
                    ((CheckBox)this.gvSalCon.Rows[i].FindControl("chkCol")).Checked = false;
                }
            }

        }

        protected void LbtnPushGrid_Click(object sender, EventArgs e)
        {
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            string rdayid = this.dllorderType.SelectedValue.ToString();
            string style = this.ddlprocode.SelectedValue.ToString().Substring(0, 12);
            string color = this.ddlprocode.SelectedValue.ToString().Substring(12, 12);
            string sdino = this.ddlprocode.SelectedValue.ToString().Substring(24, 14);

            DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
            foreach (GridViewRow row in this.gvStockDetails.Rows)
            {
                int index = row.RowIndex;
                if (((CheckBox)this.gvStockDetails.Rows[index].FindControl("chkColItem")).Checked)
                {
                    DataTable stocktbl = (DataTable)Session["tblstock"];

                    DataRow dr = stocktbl.Rows[index];

                    string mlccode = dr["mlccod"].ToString();
                    string mlcdesc = dr["actdesc"].ToString();
                    string styleid = dr["styleid"].ToString();
                    string styledesc = dr["styldesc"].ToString();
                    string colorid = dr["colorid"].ToString();
                    string colordesc = dr["colordesc"].ToString();
                    string sizeid = dr["sizeid"].ToString();
                    string sizedesc = dr["sizedesc"].ToString();
                    double ordrqty = 0.00;
                    double prdqty = 0.00;
                    double rate = Convert.ToDouble(dr["rate"]);
                    string ordrno = dr["ordrno"].ToString();
                    string hscode = "";
                    double balqty = Convert.ToDouble(dr["stockqty"]);
                    string redayid = dr["odayid"].ToString();
                    double itmamt = 0.00;
                    string inqno = "00000000000000";
                    double inprocqty = 0.0;



                    var checklist = lst.FindAll(p => p.mlccod == mlccode && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.ordrno == ordrno && p.rdayid == redayid && p.ordno == inqno);
                    if (checklist.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist');", true);
                        continue;
                    }

                    lst.Add(new SPEENTITY.C_19_Exp.EClassExpBO.EclassExport(mlccode, mlcdesc, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, ordrqty, rate, ordrno, hscode,
                        0.00, 0.00, 0.00, 0.00, 0.00, "", 0.00, 0.00, prdqty, balqty, redayid, itmamt, inqno, inprocqty));
                    ViewState["tblexport"] = lst;
                }
            }

            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string comcod = this.GetComeCode();

            DataTable Tempdt;
            DataView Tempdv;
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "GETLCDETINFO", mlccod, rdayid, "", "", "", "", "", "", "");
            Tempdt = ds2.Tables[0].Copy();
            Tempdv = Tempdt.DefaultView;
            Tempdv.RowFilter = ("gcod ='010100101009' or gcod ='010100101010'");
            Tempdt = Tempdv.ToTable();
            if ((Tempdt.Rows[0]["gdesc1"].ToString() != "") && (Tempdt.Rows[1]["gdesc1"].ToString() != ""))
            {
                this.Data_Bind();
                this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + rdayid;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Add Conversion Rate');", true);
                this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + rdayid;
            }
        }

        protected void gvStockDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gv = (GridView)sender;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Stockqty = Stockqty + Convert.ToDouble("0" + ((Label)e.Row.Cells[5].FindControl("DlgvqtyStock")).Text);
                double inprototal = Convert.ToDouble("0" + ((Label)e.Row.Cells[6].FindControl("DlgvInProcess")).Text);

                if (prevsize != ((Label)e.Row.Cells[2].FindControl("LblRescode")).Text)
                {
                    Stockqty = 0;
                    Stockqty = Convert.ToDouble("0" + ((Label)e.Row.Cells[5].FindControl("DlgvqtyStock")).Text);

                    prevsize = ((Label)e.Row.Cells[2].FindControl("LblRescode")).Text;
                    noRowSpan = e.Row.RowIndex;
                    ((Label)e.Row.Cells[6].FindControl("DlgvTotalStock")).Text = (prevsize == "000000000000") ? "" : "<center><b style='color:blue'>" + Stockqty.ToString() + "</b></center>";
                    ((Label)e.Row.Cells[8].FindControl("DlgvAvailabe")).Text = (prevsize == "000000000000") ? "" : " <center><b style='color:red'>" + (Stockqty - inprototal).ToString() + "</b></center>";

                }
                else
                {
                    //Increase the rowspan row count by one\

                    gv.Rows[noRowSpan].Cells[7].RowSpan = (gv.Rows[noRowSpan].Cells[7].RowSpan >= 2) ? gv.Rows[noRowSpan].Cells[7].RowSpan + 1 : gv.Rows[noRowSpan].Cells[7].RowSpan + 2;
                    gv.Rows[noRowSpan].Cells[6].RowSpan = (gv.Rows[noRowSpan].Cells[6].RowSpan >= 2) ? gv.Rows[noRowSpan].Cells[6].RowSpan + 1 : gv.Rows[noRowSpan].Cells[6].RowSpan + 2;
                    gv.Rows[noRowSpan].Cells[8].RowSpan = (gv.Rows[noRowSpan].Cells[8].RowSpan >= 2) ? gv.Rows[noRowSpan].Cells[8].RowSpan + 1 : gv.Rows[noRowSpan].Cells[8].RowSpan + 2;


                    e.Row.Controls[6].Visible = false;
                    e.Row.Controls[7].Visible = false;
                    e.Row.Controls[8].Visible = false;
                    ((Label)gv.Rows[noRowSpan].Cells[6].FindControl("DlgvTotalStock")).Text = "<center><b style='color:blue'>" + Stockqty.ToString() + "</b></center>";
                    ((Label)gv.Rows[noRowSpan].Cells[8].FindControl("DlgvAvailabe")).Text = " <center><b style='color:red'>" + (Stockqty - inprototal).ToString() + "</b></center>";

                }

            }
        }

        protected void chkPackPlan_CheckedChanged(object sender, EventArgs e)
        {
            this.divPackPln.Visible = this.chkPackPlan.Checked;
            string buyer = this.ddlBuyer.SelectedValue.Trim() == "000000000000" ? "%" : this.ddlBuyer.SelectedValue.Trim() + "%";
            string comcod = this.GetComeCode();

            if (this.chkPackPlan.Checked)
            {
                DataSet ds = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_PACK_PLAN", buyer, "", "", "", "", "");
                this.ddlPackPlnList.DataValueField = "packpln";
                this.ddlPackPlnList.DataTextField = "packplndesc";
                this.ddlPackPlnList.DataSource = ds.Tables[0];
                this.ddlPackPlnList.DataBind();

                this.ddlmlccode.Enabled = false;
                this.dllorderType.Enabled = false;
                this.ddlprocode.Enabled = false;
            }
            else
            {
                this.ddlmlccode.Enabled = true;
                this.dllorderType.Enabled = true;
                this.ddlprocode.Enabled = true;
            }

        }

        protected void btnAddPck_Click(object sender, EventArgs e)
        {

            try
            {
                string comcod = this.GetComeCode();
                List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

                string pktPln = this.ddlPackPlnList.SelectedValue.ToString();
                DataSet ds = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_EXPORT_PACK_PLN_INFO", pktPln, "", "", "", "", "");

                List<SPEENTITY.C_03_CostABgd.EclassSalesContact> tbl1 = ds.Tables[0].DataTableToList<SPEENTITY.C_03_CostABgd.EclassSalesContact>();



                DateTime deldate = Convert.ToDateTime(this.txtdate.Text.Trim());
                foreach (SPEENTITY.C_03_CostABgd.EclassSalesContact c1 in tbl1)
                {

                    string mlccode = c1.mlccod.ToString();
                    string mlcdesc = c1.mlcdesc.ToString();
                    string styleid = c1.styleid.ToString();
                    string styledesc = c1.styledesc.ToString();
                    string colorid = c1.colorid.ToString();
                    string colordesc = c1.colordesc.ToString();
                    string sizeid = c1.sizeid.ToString();
                    string sizedesc = c1.sizedesc.ToString();
                    double ordrqty = Convert.ToDouble(c1.ordrqty);
                    double prdqty = Convert.ToDouble(c1.ordrqty1);
                    double totlctn = Convert.ToDouble(c1.totlctn);
                    double rate = Convert.ToDouble(c1.rate);
                    string ordrno = c1.ordrno.ToString();
                    string po = c1.po.ToString();
                    string hscode = c1.hscode.ToString();
                    double balqty = Convert.ToDouble(c1.balqty);
                    string redayid = c1.rdayid.ToString();
                    double itmamt = 0.00;
                    string inqno = c1.sdino.ToString();
                    double inprocqty = Convert.ToDouble(c1.inprocqty);
                    double totlprs = Convert.ToDouble(c1.totlprs);
                    double ttlctn = Convert.ToDouble(c1.totlctn);


                    var checklist = lst.FindAll(p => p.mlccod == mlccode && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid
                    && p.ordrno == ordrno && p.rdayid == redayid && p.ordno == inqno && p.po== po);
                    if (checklist.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Already Exist');", true);
                        continue;
                    }

                    lst.Add(new SPEENTITY.C_19_Exp.EClassExpBO.EclassExport(mlccode, mlcdesc, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, ordrqty, rate, ordrno, po, hscode,
                        0.00, totlprs, ttlctn, 0.00, 0.00, "", 0.00, 0.00, prdqty, balqty, redayid, itmamt, inqno, inprocqty));
                }



                ViewState["tblexport"] = lst;
                Hashtable hst = (Hashtable)Session["tblLogin"];

                string mlccod = this.ddlmlccode.SelectedValue.ToString();
                string dayid = this.dllorderType.SelectedValue.ToString();
                DataTable Tempdt;
                DataView Tempdv;
                DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_MASTERLC_02", "GETLCDETINFO", mlccod, dayid, "", "", "", "", "", "", "");
                Tempdt = ds2.Tables[0].Copy();
                Tempdv = Tempdt.DefaultView;
                Tempdv.RowFilter = ("gcod ='010100101009' or gcod ='010100101010'");
                Tempdt = Tempdv.ToTable();
                if ((Tempdt.Rows[0]["gdesc1"].ToString() != "") && (Tempdt.Rows[1]["gdesc1"].ToString() != ""))
                {
                    this.Data_Bind();
                    this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + dayid;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Please Add Conversion Rate');", true);
                    this.hypbtnMatReq1.NavigateUrl = "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=" + mlccod + "&dayid=" + dayid;
                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Ensure Selected Desire Buyer";
            }

        }

        protected void imgPreVious_Click(object sender, EventArgs e)
        {
            this.PreviousList();
        }
        private void PreviousList()
        {
            string txtdate = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string comcod = this.GetComeCode();
            string Invno = (this.Request.QueryString["genno"].Length == 0) ? "%" : this.Request.QueryString["genno"].ToString();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_PREV_INVNO", txtdate, Invno, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPrevList.DataTextField = "invno1";
            this.ddlPrevList.DataValueField = "invno";
            this.ddlPrevList.DataSource = ds1.Tables[0];
            this.ddlPrevList.DataBind();

        }
        private void Footcalculation()
        {
            try
            {

                var list1 = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];
                if (list1.Count == 0)
                    return;
                ((Label)this.gvSalCon.FooterRow.FindControl("txtFNetTotal")).Text = (list1.Select(p => p.ordrqty).Sum() == 0.00) ? "0" : list1.Select(p => p.ordrqty).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalCon.FooterRow.FindControl("txtPrdTotal")).Text = (list1.Select(p => p.prdqty).Sum() == 0.00) ? "0" : list1.Select(p => p.prdqty).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalCon.FooterRow.FindControl("txtbalqtyTotal")).Text = (list1.Select(p => p.balqty).Sum() == 0.00) ? "0" : list1.Select(p => p.balqty).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalCon.FooterRow.FindControl("txtFtotlprs")).Text = (list1.Select(p => p.totlprs).Sum() == 0.00) ? "0" : list1.Select(p => p.totlprs).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.gvSalCon.FooterRow.FindControl("txtFtotlctn")).Text = (list1.Select(p => p.totlctn).Sum() == 0.00) ? "0" : list1.Select(p => p.totlctn).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.gvSalCon.FooterRow.FindControl("txtFtotalgw")).Text = (list1.Select(p => p.totalgw).Sum() == 0.00) ? "0" : list1.Select(p => p.totalgw).Sum().ToString("#,##0.00;(#,##0.00); ");

                ((Label)this.gvSalCon.FooterRow.FindControl("txtFtotalNw")).Text = ((list1.Select(p => p.totlctn).Sum() * list1.Select(p => p.nwperctn).Sum()) == 0.00) ? "0" : (list1.Select(p => p.totlctn).Sum() * list1.Select(p => p.nwperctn).Sum()).ToString("#,##0;(#,##0); ");

                ((Label)this.gvSalCon.FooterRow.FindControl("txtFAmt")).Text = ((list1.Select(p => p.itmamt).Sum()) == 0.00) ? "0" :
                    list1.Select(p => p.itmamt).Sum().ToString("#,##0.00;(#,##0.00); ");

                //((Label)this.gvSalCon.FooterRow.FindControl("txtFAmt")).Text = ((list1.Select(p => p.totlprs).Sum()) == 0.00) ? "0" :
                //    (list1[0].rate * list1.Select(p => p.totlprs).Sum()).ToString("#,##0.00;(#,##0.00); ");


                ((Label)this.gvSalCon.FooterRow.FindControl("txtFcbm")).Text = (list1.Select(p => p.cbm).Sum() == 0.00) ? "0" : list1.Select(p => p.cbm).Sum().ToString("#,##0.00;(#,##0.00); ");

                double Codpar = Convert.ToDouble("0" + this.txtDisPer.Text.Trim()) / 100;

                double DisAmt = (list1.Select(p => p.itmamt).Sum()) * Codpar;
                this.txtDis.Text = DisAmt.ToString("#,##0.00;(#,##0.00); ");
                this.txtGTotal.Text = ((list1.Select(p => p.itmamt).Sum()) - DisAmt).ToString("#,##0.00;(#,##0.00); ");

                //double Codpar = Convert.ToDouble("0" + this.txtDis.Text.Trim());


            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;

            }

        }

        protected void LbtnShowAll_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string ordtype = this.dllorderType.SelectedValue.ToString();
            string style = this.ddlprocode.SelectedValue.ToString().Substring(0, 12);
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "STYLEWISE_ORDER_DETAILS", mlccod, ordtype, style, "", "", "", "", "", "");
            if (ds1 == null)
                return;


            for (int i = 0; i < ds1.Tables[1].Rows.Count; i++)
            {

                int columid = Convert.ToInt32(ASTUtility.Right(ds1.Tables[1].Rows[i]["sizeid"].ToString(), 2));

                this.gvsizes.Columns[columid + 1].Visible = true;
                this.gvsizes.Columns[columid + 1].HeaderText = ds1.Tables[1].Rows[i]["sizedesc"].ToString().Trim();
            }
            this.gvsizes.EditIndex = -1;

            this.gvsizes.DataSource = ds1.Tables[0];
            this.gvsizes.DataBind();
            string Buyer = this.ddlBuyer.SelectedValue.ToString() + "%";
            string date2 = System.DateTime.Today.ToString("dd-MMM-yyyy");
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_REPORT_FG_INV_02", "RPT_LOCATION_WISE_STOCK", "%", date2, date2, Buyer, mlccod + "%", ordtype + "%", "Location", "", "");
            Session["tblstock"] = ds2.Tables[0];
            this.gvStockDetails.DataSource = ds2.Tables[0];
            this.gvStockDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void ddlprocode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string mlccod = this.ddlmlccode.SelectedValue.ToString();
            string styleid = this.ddlprocode.SelectedValue.ToString().Substring(0, 12);
            string colorid = this.ddlprocode.SelectedValue.ToString().Substring(12, 12);
            string dayid = this.dllorderType.SelectedValue.ToString();
            DataSet result = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSTYLE_WISE_INFO", mlccod, styleid, colorid, dayid, "", "", "", "", "");

            if (result == null)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Not Found";
                return;
            }
            if (result.Tables[0].Rows.Count == 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Not Found";
                return;
            }

            this.txtordqty.Text = Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]).ToString("#,##0.00;(#,##0.00); ");
            DataSet result1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT_PLAN", "GET_ORDERSTYLE_EXPORT_INFO", mlccod, styleid, colorid, dayid, "", "", "", "", "");
            double exportqty = 0;
            if (result1 == null)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Data Not Found";
                return;
            }
            if (result1.Tables[0].Rows.Count > 0)
            {
                exportqty = Convert.ToDouble(result1.Tables[0].Rows[0]["totlprs"]);
            }
            this.txtexpqty.Text = (Convert.ToDouble(result.Tables[0].Rows[0]["ordrqty"]) - exportqty).ToString("#,##0.00;(#,##0.00); ");
        }

        private void GetSeason()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "SEASONLIST", "33%", "", "", "", "", "", "", "", "");
            ds1.Tables[0].Rows.Add(comcod, "00000", "All");

            ds1.Tables[0].DefaultView.Sort = "gcod DESC";
            if (ds1 == null)
                return;

            DdlSeason.DataTextField = "gdesc";
            DdlSeason.DataValueField = "gcod";
            DdlSeason.DataSource = ds1.Tables[0];
            DdlSeason.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string season = hst["season"].ToString();
            if (season != null && season != "00000")
            {
                this.DdlSeason.SelectedValue = season;
            }
            else
            {
                this.DdlSeason.SelectedValue = "00000";
            }
        }

    }


}