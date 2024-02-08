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
using SPEENTITY;

namespace SPEWEB.F_19_EXP
{
    public partial class ExportSample : System.Web.UI.Page
    {
        UserManagerSampling objUserMan = new UserManagerSampling();
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetGenCode();
              
                this.ShowShiplineType();
                this.Get_Shipment();
                this.ShowOtherInfo();
                this.ShowDelMode();
                ((Label)this.Master.FindControl("lblTitle")).Text = " EXPORT SAMPLE ENTRY";
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtExFact.Text = "";
                //  this.txtExpDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.CommonButton();
                this.Getuser();
                if (this.Request.QueryString["actcode"].Length != 0)
                {
                    this.PreviousList();
                    //this.lbtnOk_Click(null, null);
                }
                
            }
        }

        private void GetBuyer()
        {
            string comcod = this.GetComeCode();
            string agent=(this.DdlAgent.SelectedValue.ToString()=="00000")?"%": this.DdlAgent.SelectedValue.ToString()+"%";
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_AGENTWISE_BUYER_LIST", agent, "", "", "", "", "",
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

            //((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void CommonButton()
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = false;
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;


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
            DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_EXPORT_STYLE_INFO", mlccode, rDayid, buyer, "", "", "", "", "");
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
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            this.CommercialInvoice();


        }
        private void Getuser()
        {
            if (this.lbtnOk.Text == "New")
                return;

            string comcod = this.GetComeCode();
           
            DataSet ds1 = proc1.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETUSERNAME", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
            ds1.Dispose();
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
            string agent = ds1.Tables[1].Rows[0]["agentdesc"].ToString();
            string contactpersonmail = ds1.Tables[1].Rows[0]["contactpersonmail"].ToString();
            
            string buyername = ds1.Tables[1].Rows[0]["custdesc"].ToString();
            string buyeradd = ds1.Tables[1].Rows[0]["custadd"].ToString();
            //string lcvalue = Convert.ToDouble(ds1.Tables[1].Rows[0]["lcval"]).ToString("#,##0.00;(#,##0.00); ");
            string currency = ds1.Tables[1].Rows[0]["currency"].ToString();

            string remarks = "CARTON NO : " + ds1.Tables[1].Rows[0]["remarks"].ToString();

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
                    rpt1.SetParameters(new ReportParameter("date", "Date :" + Date));
                    rpt1.SetParameters(new ReportParameter("untoOrder", untoOrder));
                    rpt1.SetParameters(new ReportParameter("untoOrderAdd", untoOrderAdd));
                    rpt1.SetParameters(new ReportParameter("RptTitle", "Negotiation of Export Documents of " + currency + " " + NetAmt.ToString() + " under L/C No. " + LcNo));

                    rpt1.SetParameters(new ReportParameter("body", "Enclosed please findherewith the following doccuments duly completed, sealed and signed by us. Please negotiate the documents at the earliest. (Send by DHL)"));




                    rpt1.SetParameters(new ReportParameter("fbody", "We would appreciate if you would kindly purchase our above mentioned Bill and credir all proceeds to our following accounts maintained with you"));


                    rpt1.SetParameters(new ReportParameter("forcomnam", "For " + comnam));


                    break;
                case "2":
                    switch (comcod)
                    {
                        case "5305":
                        case "5306":
                            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptPackingListFb", lst1, null, null);
                            break;
                        default:
                            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptPackingList", lst1, null, null);
                            break;
                    }

                    rpt1.SetParameters(new ReportParameter("comnam", comnam));
                    rpt1.SetParameters(new ReportParameter("comadd", comadd));
                    rpt1.SetParameters(new ReportParameter("InvoiceNo", InvoiceNo));
                    rpt1.SetParameters(new ReportParameter("LcNo", LcNo));
                    rpt1.SetParameters(new ReportParameter("agent", agent));
                    rpt1.SetParameters(new ReportParameter("contactpersonmail", contactpersonmail));
                    
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


                    //


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

            this.gvSalCon.DataSource = HiddenSameData(lst);
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
            var newlist = tbl1.FindAll(x => x.styleid == style && x.colorid == color && x.rdayid == rdayid  && x.sdino== sdino);
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

                var checklist = lst.FindAll(p => p.mlccod == mlccode && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.ordrno == ordrno && p.rdayid == redayid && p.ordno== inqno);
                if (checklist.Count > 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Already Exist";
                    return;
                }

                lst.Add(new SPEENTITY.C_19_Exp.EClassExpBO.EclassExport(mlccode, mlcdesc, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, ordrqty, rate, ordrno, hscode,
                    0.00, 0.00, 0.00, 0.00, 0.00, "", 0.00, 0.00, prdqty, balqty, redayid, itmamt, inqno,0.00));
            }
            ViewState["tblexport"] = lst;
            this.Data_Bind();
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
                string sdino = c1.sdino.ToString();
                var checklist = lst.FindAll(p => p.mlccod == mlccode && p.styleid == styleid && p.colorid == colorid && p.sizeid == sizeid && p.ordrno == ordrno && p.rdayid == rdayid && p.ordno== sdino);
                if (checklist.Count > 0)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Already Exist";
                    return;
                }

                lst.Add(new SPEENTITY.C_19_Exp.EClassExpBO.EclassExport(mlccode, mlcdesc, styleid, styledesc, colorid, colordesc, sizeid, sizedesc, ordrqty, rate, ordrno, hscode,
                 0.00, 0.00, 0.00, 0.00, 0.00, "", 0.00, 0.00, prdqty, balqty, rdayid, itmamt, sdino,0.00));

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
            string Agent = this.DdlAgent.SelectedValue.ToString();
            string ContactPerson = this.ddlUserList.SelectedValue.ToString();
            
            this.SaveValue();
            List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport> lst = (List<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>)ViewState["tblexport"];

            if (this.ddlPrevList.Items.Count == 0)
                this.Get_INVNO();
            string invno = this.lblCurNo1.Text.Trim().Substring(0, 3) + ASTUtility.Right(this.txtdate.Text.Trim(), 4) + this.lblCurNo1.Text.Trim().Substring(3, 2) + this.txtCurNo2.Text.Trim();
            string invdate = Convert.ToDateTime(this.txtdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string exfactdate = (this.txtExFact.Text.Trim().Length > 0) ? Convert.ToDateTime(this.txtExFact.Text.Trim()).ToString("dd-MMM-yyyy") : "01-Jan-1900";
            string refno = this.txtRefNo.Text.Trim();
            string mNAR = this.txtBillNarr.Text.Trim();
            string expno = this.txtExpNo.Text;
            string expDate = this.txtExpDate.Text;
            double Codpar = Convert.ToDouble("0" + this.txtDisPer.Text.Trim());
            double CodAmt = Convert.ToDouble("0" + this.txtDis.Text.Trim());

            string DelMode = this.ddlDelMode.SelectedValue.ToString();
            string DelDate = this.txtMDate.Text;


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
            bool result = proc1.UpdateXmlTransInfo(comcod, "SP_ENTRY_EXPORT", "UPDATE_EXPORT_INFO", ds1, null, null, invno, mlccod, invdate, refno, shipment, shptype, mNAR, PostedDat, usrid, sessionid, trmid, type, exfactdate, Country, PortLoad, PortDis, expno, expDate, Codpar.ToString(), CodAmt.ToString(), DelMode, DelDate, Agent,ContactPerson);
            if (result == true)
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Updated Successfully');", true);
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
                this.DdlAgent.Enabled = true;//System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.addsec.Visible = false;
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
            this.DdlAgent.Enabled = false;
            this.Get_Info();
            this.addsec.Visible = true;

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

                DataSet ds1 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_EXPORT_INFO", mCPRNo, "", "", "", "", "", "", "", "");
                var lst = ds1.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>();

                if (lst == null)
                    return;
                ViewState["tblexport"] = lst;




                if (mCPRNo == "NEWINV")
                {
                    DataSet ds2 = proc1.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_LAST_INVNO", CurDate1, "", "", "", "", "", "", "", "");
                    if (ds2 == null)
                        return;
                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(0, 6);
                    this.txtCurNo2.Text = ds2.Tables[0].Rows[0]["maxno1"].ToString().Substring(6, 5);

                    return;
                }


                this.txtDisPer.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["disper"]).ToString("#,##0;(#,##0); ");
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
                this.txtExpNo.Text = ds1.Tables[1].Rows[0]["expno"].ToString();
                this.txtExpDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["expdate"]).ToString("dd-MMM-yyyy");
                this.ddlCountry.SelectedValue = ds1.Tables[1].Rows[0]["excountry"].ToString();
                this.ddlPortLoad.SelectedValue = ds1.Tables[1].Rows[0]["exloading"].ToString();
                this.ddlPortDis.SelectedValue = ds1.Tables[1].Rows[0]["exdischare"].ToString();

                this.ddlDelMode.SelectedValue = ds1.Tables[1].Rows[0]["delvtrm"].ToString();
                this.txtMDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["delvdate"]).ToString("dd-MMM-yyyy");
                this.ddlBuyer_SelectedIndexChanged(null,null);
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
        private void GetGenCode()
        {
            Session.Remove("lstgencode");
            string comcod = this.GetComeCode();
            var lst = objUserMan.GetGenCode(comcod);
            Session["lstgencode"] = lst;
            var lstagent = lst.FindAll(l => l.gcod.ToString().Substring(0, 2) == "32");
            lstagent.Add(new SPEENTITY.C_04_Sampling.EClassPrototypeSampling.EClassGenCode("00000", "None"));

            DdlAgent.DataTextField = "gdesc";
            DdlAgent.DataValueField = "gcod";
            DdlAgent.DataSource = lstagent;
            DdlAgent.DataBind();
            
            DdlAgent_SelectedIndexChanged(null,null);


        }

        protected void DdlAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetBuyer();
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
            if (result.Tables[0].Rows.Count==0)
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
    }
}