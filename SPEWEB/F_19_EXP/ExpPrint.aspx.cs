using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using SPEENTITY;
using SPEENTITY.C_22_Sal;
using Microsoft.Reporting.WinForms;
using SPERDLC;
using SPELIB;

namespace SPEWEB.F_19_EXP
{
    public partial class ExpPrint : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ExpInvoice":
                    this.CommercialInvoice();
                    break;

                case "DelChallan":
                    this.PrintDeliveryChallan();
                    break;

                case "MoneyReceipt":
                    this.PrintMRInfo();
                    break;

                case "ShipMark":
                    this.PrintShipMark();
                    break;
                case "ShipMarkV2":
                    this.PrintShipMarkV2();
                    break;
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void CommercialInvoice()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string invno = this.Request.QueryString["genno"].ToString();


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_EXPORT", "GET_EXPORT_INFO", invno, "", "", "", "", "", "", "", "");

           DataTable dtprocess = ds1.Tables[0].AsEnumerable()
                                      .GroupBy(r => new {
                                          invno = r.Field<string>("invno"),
                                          mlccod = r.Field<string>("mlccod"),
                                          mlcdesc = r.Field<string>("mlcdesc"),
                                          styleid = r.Field<string>("styleid"),
                                          styledesc = r.Field<string>("styledesc"),
                                          colorid = r.Field<string>("colorid"),
                                          colordesc = r.Field<string>("colordesc"),
                                          artno = r.Field<string>("artno"),
                                          ordrno = r.Field<string>("ordrno"),
                                          ordrref = r.Field<string>("ordrref"),
                                          formaname = r.Field<string>("formaname"), 
                                          po = r.Field<string>("po")

                                      })
                                      .Select(g =>
                                      {
                                          var row = ds1.Tables[0].NewRow();
                                          
                                          row["invno"] = g.Key.invno;
                                          row["mlccod"] = g.Key.mlccod;
                                          row["mlcdesc"] = g.Key.mlcdesc;
                                          row["styleid"] = g.Key.styleid;
                                          row["styledesc"] = g.Key.styledesc; 
                                          row["colorid"] = g.Key.colorid;
                                          row["colordesc"] = g.Key.colordesc;
                                          row["ordrno"] = g.Key.ordrno;
                                          row["ordrref"] = g.Key.ordrref;
                                          row["formaname"] = g.Key.formaname;
                                          row["artno"] = g.Key.artno;
                                          row["po"] = g.Key.po;
                                          row["ordrqty"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("ordrqty")));
                                          row["itmamt"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("itmamt")));
                                          row["rate"] = Convert.ToDouble(g.Average(r => r.Field<decimal>("rate")));
                                          row["totlprs"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("totlprs")));
                                          row["stylamt"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("stylamt")));
                                          row["pperctnqty"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("pperctnqty"))); 
                                          row["totlctn"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("totlctn")));
                                          row["gwperctn"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("gwperctn")));
                                          row["nwperctn"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("nwperctn")));
                                          row["cbm"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("cbm")));
                                          row["totalgw"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("totalgw")));
                                          row["qty"] = Convert.ToDouble(g.Sum(r => r.Field<decimal>("qty")));

                                          return row;
                                      }).CopyToDataTable();




            var lst1 =new List<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>();
            if (comcod == "5305" || comcod == "5306")
            {
                lst1 = dtprocess.DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>();
            }
            else
            {
               lst1 = ds1.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassExportDoc>();

            }
            var lst2 = ds1.Tables[2].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.RptEclassFrdletter>();

            string InvoiceNo = ds1.Tables[0].Rows[0]["invno"].ToString();
            string InvoiceDate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["invdate"]).ToString("dd-MMM-yyyy");

            string LcNo = ds1.Tables[1].Rows[0]["lcno"].ToString();
            string LcDate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["lcisudat"]).ToString("dd-MMM-yyyy");
            string BlNo = ds1.Tables[1].Rows[0]["blno"].ToString();
            string BlDate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["bldate"]).ToString("dd-MMM-yyyy");

            string LcIssuBnk = ds1.Tables[1].Rows[0]["isubankname"].ToString();
            string LcIssuBnkAdd = ds1.Tables[1].Rows[0]["isubankadd"].ToString();
            string BeniBank = ds1.Tables[1].Rows[0]["benibank"].ToString();
            string benibankcust = ds1.Tables[1].Rows[0]["benibankcust"].ToString();
            string BeBankAdd = ds1.Tables[1].Rows[0]["benibankAdd"].ToString();
            string BeniBankSw = ds1.Tables[1].Rows[0]["benibanksw"].ToString();
            string RcvBankName = ds1.Tables[1].Rows[0]["rcvbankname"].ToString();
            string RcvBankAdd = ds1.Tables[1].Rows[0]["rcvbankadd"].ToString();
            string comaddf = ds1.Tables[1].Rows[0]["comaddf"].ToString();

            string expno = ds1.Tables[1].Rows[0]["expno"].ToString();
            string ExpRegDt = Convert.ToDateTime(ds1.Tables[1].Rows[0]["expdate"]).ToString("dd-MMM-yyyy");
            string Orgin = "Bangladesh";
            string ModeofShip = ds1.Tables[1].Rows[0]["shiptypedesc"].ToString();
            string Shipfrom = ds1.Tables[1].Rows[0]["exloaddesc"].ToString();
            string Shipto = ds1.Tables[1].Rows[0]["exdisdesc"].ToString();
            string HscCod = ds1.Tables[1].Rows[0]["hscode"].ToString();
            string unit = ds1.Tables[1].Rows[0]["stylunit"].ToString();
            string styledesc = ds1.Tables[0].Rows[0]["styledesc"].ToString();

            string shipmentdat = Convert.ToDateTime(ds1.Tables[1].Rows[0]["invdate"]).ToString("dd-MMM-yyyy");
            string Shipper = comnam;
            string shippadd = ds1.Tables[1].Rows[0]["comaddf"].ToString();
            string ExFDate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["exfacdt"]).ToString("dd-MMM-yyyy");
            string ShipMarks = ds1.Tables[1].Rows[0]["shipmarks"].ToString();
            string Invrefno = ds1.Tables[1].Rows[0]["invrefno"].ToString();
            string PaymentTerms = ds1.Tables[1].Rows[0]["payterm"].ToString();
            string PaymentTermsCust = ds1.Tables[1].Rows[0]["paytermcust"].ToString();
            string PortLoad = ds1.Tables[1].Rows[0]["exloaddesc"].ToString();
            string Mode = ds1.Tables[1].Rows[0]["delvdesc"].ToString();
            string cutomBank = ds1.Tables[1].Rows[0]["custbank"].ToString();
            double txtDis = Convert.ToDouble(ds1.Tables[1].Rows[0]["disper"]);

            string notifyName = ds1.Tables[1].Rows[0]["notpartydesc"].ToString();
            string notifyadd = ds1.Tables[1].Rows[0]["notpadd"].ToString();
            string notifyName1 = ds1.Tables[1].Rows[0]["notpartydesc1"].ToString();
            string notifyadd1 = ds1.Tables[1].Rows[0]["notpadd1"].ToString();

            string buyername = ds1.Tables[1].Rows[0]["custdesc"].ToString();
            string buyeradd = ds1.Tables[1].Rows[0]["custadd"].ToString();
            string currency = ds1.Tables[1].Rows[0]["currency"].ToString();

            string remarks = ds1.Tables[1].Rows[0]["remarks"].ToString();
            string delvCode = ds1.Tables[1].Rows[0]["delvtrm"].ToString();
            string delvDesc = ds1.Tables[1].Rows[0]["delvdesc"].ToString();
            string printFormat = ds1.Tables[1].Rows[0]["printformat"].ToString();
            string crtnRemarks = ds1.Tables[1].Rows[0]["cartonremarks"].ToString();
            string blno = ds1.Tables[1].Rows[0]["blno"].ToString();
            string bldate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["bldate"]).ToString("dd-MMM-yyyy");
            string exporterrefno = ds1.Tables[1].Rows[0]["exporterrefno"].ToString();
            string exporterrefdate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["exporterrefdate"]).ToString("dd-MMM-yyyy");
            string cartonno = Convert.ToDouble(ds1.Tables[1].Rows[0]["cartonno"]).ToString("#,##0;(#,##0); ");
            string gweight = Convert.ToDouble(ds1.Tables[1].Rows[0]["gweight"]).ToString("#,##0.0;(#,##0.0); ");
            string nweight = Convert.ToDouble(ds1.Tables[1].Rows[0]["nweight"]).ToString("#,##0.0;(#,##0.0); ");

            string proformaInv = ds1.Tables[1].Rows[0]["proforminv"].ToString();
            string notify2 = ds1.Tables[1].Rows[0]["notify2"].ToString();


            //string consignee = notifyName;
            string consignee = ds1.Tables[1].Rows[0]["consigne"].ToString();
            string typeofgoods = ds1.Tables[1].Rows[0]["typeofgoodsdesc"].ToString();
            string consigneeAdd = notifyadd;

            double NetAmt = Convert.ToDouble(lst1.Select(p => p.stylamt).Sum());
            double tctn = Convert.ToDouble(lst1.Select(p => p.totlctn).Sum());
            double twgt = Convert.ToDouble(lst1.Select(p => p.tnetwth).Sum());
            double tqty = Convert.ToDouble(lst1.Select(p => p.totlprs).Sum());

            double lcvalue = Convert.ToDouble(lst1.Select(p => p.stylamt).Sum());
            double disvalue = Convert.ToDouble(ds1.Tables[1].Rows[0]["disamt"]);

            string inwords = ASTUtility.Trans(Math.Round(NetAmt - disvalue), 4);
            string ctninwords = ASTUtility.Trans(Math.Round(Convert.ToDouble(cartonno)), 4);
            string CartoninWord = ctninwords.Replace("Dollar Only", "");

            double totalCartons = lst1.Sum(p => p.totlctn);
            string destCountry = ds1.Tables[1].Rows[0]["countrydesc"].ToString();


            string salecondition = ds1.Tables[1].Rows[0]["salecondition"].ToString();
            string declaration = ds1.Tables[1].Rows[0]["declaration"].ToString();
            string statement = ds1.Tables[1].Rows[0]["statement"].ToString();
            string ERCNo = "RA-0083933";
            string ERDate ="08-Jan-2016";
            string supcontact = ds1.Tables[1].Rows[0]["supcontact"].ToString();

            LocalReport rpt1 = new LocalReport();


            if (comcod == "5305" || comcod == "5306")
            {
                if (printFormat == "1") //F - CCC
                {
                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptCommInvoiceFBFrmt1", lst1.FindAll(p => p.totlprs > 0), null, null);

                    rpt1.SetParameters(new ReportParameter("salecondition", salecondition));
                    rpt1.SetParameters(new ReportParameter("declaration", declaration));
                    rpt1.SetParameters(new ReportParameter("statement", statement));
                    rpt1.SetParameters(new ReportParameter("ERCNo", ERCNo));
                    rpt1.SetParameters(new ReportParameter("ERDate", ERDate));
                    rpt1.SetParameters(new ReportParameter("supcontact", supcontact));
                    rpt1.SetParameters(new ReportParameter("consignee", consignee));
                    rpt1.SetParameters(new ReportParameter("blno", blno));
                    rpt1.SetParameters(new ReportParameter("bldate", bldate));
                    rpt1.SetParameters(new ReportParameter("exporterrefno", exporterrefno));
                    rpt1.SetParameters(new ReportParameter("exporterrefdate", exporterrefdate));
                    rpt1.SetParameters(new ReportParameter("benibankcust", benibankcust));
                    rpt1.SetParameters(new ReportParameter("PaymentTermsCust", PaymentTermsCust));
                    rpt1.SetParameters(new ReportParameter("typeofgoods", typeofgoods));

                }
                else if (printFormat == "2") //F - COMPAR SPA.
                {
                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptCommInvoiceFBFrmt2", lst1.FindAll(p => p.totlprs > 0), null, null);
                }
                else if (printFormat == "3") //F - EUROPE. 
                {
                    string inWord = inwords.Replace("Dollar", "Euro");
                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptCommInvoiceFBFrmt3", lst1.FindAll(p => p.totlprs > 0), null, null);
                    rpt1.SetParameters(new ReportParameter("benibankcust", benibankcust));
                    rpt1.SetParameters(new ReportParameter("LcIssueDate", LcDate));
                    rpt1.SetParameters(new ReportParameter("BlNo", BlNo));
                    rpt1.SetParameters(new ReportParameter("BlDate", BlDate));
                    rpt1.SetParameters(new ReportParameter("typeofgoods", typeofgoods));
                    rpt1.SetParameters(new ReportParameter("cartonno", cartonno));
                    rpt1.SetParameters(new ReportParameter("gweight", gweight));
                    rpt1.SetParameters(new ReportParameter("nweight", nweight));
                    rpt1.SetParameters(new ReportParameter("proformaInv", proformaInv));
                    rpt1.SetParameters(new ReportParameter("notify2", notify2));
                    rpt1.SetParameters(new ReportParameter("inWord", "In Words: " + inWord));
                    rpt1.SetParameters(new ReportParameter("txtDis", txtDis.ToString("#,##0.0;(#,##0.0); ")));
                    //rpt1.SetParameters(new ReportParameter("DisAmt", Convert.ToDouble(ds1.Tables[1].Rows[0]["disamt"]).ToString("#,##0.00;(#,##0.00); ")));

                }
                else
                {
                    rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptCommInvoiceFBFrmt1", lst1.FindAll(p => p.totlprs > 0), null, null);

                    rpt1.SetParameters(new ReportParameter("salecondition", salecondition));
                    rpt1.SetParameters(new ReportParameter("declaration", declaration));
                    rpt1.SetParameters(new ReportParameter("statement", statement));
                    rpt1.SetParameters(new ReportParameter("ERCNo", ERCNo));
                    rpt1.SetParameters(new ReportParameter("ERDate", ERDate));
                    rpt1.SetParameters(new ReportParameter("supcontact", supcontact));
                    rpt1.SetParameters(new ReportParameter("consignee", consignee));

                }

            }
            else
            {
              
                rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptCommInvoice", lst1.FindAll(p => p.totlprs > 0), null, null);
            }

            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("comaddf", comaddf.Replace("Factory :", "")));
            rpt1.SetParameters(new ReportParameter("InvoiceNo", InvoiceNo));
            rpt1.SetParameters(new ReportParameter("LcNo", LcNo));
            rpt1.SetParameters(new ReportParameter("invrefno", Invrefno));
            rpt1.SetParameters(new ReportParameter("InvoiceDate", InvoiceDate));
            rpt1.SetParameters(new ReportParameter("LcDate", "DT: " + LcDate));

            rpt1.SetParameters(new ReportParameter("BeniBank", BeniBank));
            rpt1.SetParameters(new ReportParameter("BeBankAdd", BeBankAdd));
            rpt1.SetParameters(new ReportParameter("BeniBankSw", BeniBankSw));

            rpt1.SetParameters(new ReportParameter("IssueBankNm", LcIssuBnk));
            rpt1.SetParameters(new ReportParameter("IssueBankAdd", LcIssuBnkAdd));

            rpt1.SetParameters(new ReportParameter("RcvBankNm", RcvBankName));
            rpt1.SetParameters(new ReportParameter("RcvBankAdd", RcvBankAdd));

            rpt1.SetParameters(new ReportParameter("ExpNo", expno));
            rpt1.SetParameters(new ReportParameter("ExpRegNo", expno));
            rpt1.SetParameters(new ReportParameter("ExpRegDt", ExpRegDt));


            rpt1.SetParameters(new ReportParameter("destCountry", destCountry));
            rpt1.SetParameters(new ReportParameter("originCountry", "Bangladesh"));

            rpt1.SetParameters(new ReportParameter("Orgin", Orgin));
            rpt1.SetParameters(new ReportParameter("ModeofShip", ModeofShip));
            rpt1.SetParameters(new ReportParameter("Shipfrom", Shipfrom));
            rpt1.SetParameters(new ReportParameter("Shipto", Shipto));
            rpt1.SetParameters(new ReportParameter("HscCod", HscCod));
            rpt1.SetParameters(new ReportParameter("shipmentdat", shipmentdat));

            rpt1.SetParameters(new ReportParameter("Shipper", Shipper));
            rpt1.SetParameters(new ReportParameter("shippadd", shippadd));

            rpt1.SetParameters(new ReportParameter("styledesc", styledesc));
            rpt1.SetParameters(new ReportParameter("remarks", remarks));
            rpt1.SetParameters(new ReportParameter("crtnRemarks", crtnRemarks));

            rpt1.SetParameters(new ReportParameter("notifyName", notifyName));
            rpt1.SetParameters(new ReportParameter("notifyadd", notifyadd));
            rpt1.SetParameters(new ReportParameter("notifyName1", notifyName1));
            rpt1.SetParameters(new ReportParameter("notifyadd1", notifyadd1));
            rpt1.SetParameters(new ReportParameter("buyername", buyername));
            rpt1.SetParameters(new ReportParameter("buyeradd", buyeradd));

            rpt1.SetParameters(new ReportParameter("Fterms", ""));
            rpt1.SetParameters(new ReportParameter("paymntTerms", PaymentTerms));
            rpt1.SetParameters(new ReportParameter("PaymentTermsCust", PaymentTermsCust));
            rpt1.SetParameters(new ReportParameter("portloading", PortLoad));
            rpt1.SetParameters(new ReportParameter("mode", Mode));
            rpt1.SetParameters(new ReportParameter("cutomBank", cutomBank));
            rpt1.SetParameters(new ReportParameter("exporterrefno", exporterrefno));
            rpt1.SetParameters(new ReportParameter("exporterrefdate", exporterrefdate));

            rpt1.SetParameters(new ReportParameter("ShipMarks", ShipMarks));
            rpt1.SetParameters(new ReportParameter("ExFDate", ExFDate));
            rpt1.SetParameters(new ReportParameter("delvCode", delvCode));
            rpt1.SetParameters(new ReportParameter("delvDesc", delvDesc));

            rpt1.SetParameters(new ReportParameter("txtDis", txtDis.ToString("#,##0.0;(#,##0.0); ")));
            rpt1.SetParameters(new ReportParameter("DisAmt", Convert.ToDouble(ds1.Tables[1].Rows[0]["disamt"]).ToString("#,##0.00;(#,##0.00); ")));
            rpt1.SetParameters(new ReportParameter("txttAmt", "Grand Total:"));
            rpt1.SetParameters(new ReportParameter("TAmt", (NetAmt - disvalue).ToString("#,##0.00;(#,##0.00); ")));

            rpt1.SetParameters(new ReportParameter("RptTitle", "COMMERCIAL INVOICE"));
            rpt1.SetParameters(new ReportParameter("InWrd", "IN WORDS: " + inwords));
            rpt1.SetParameters(new ReportParameter("ttlCrtns", "TOTAL CARTONS: "+ CartoninWord + "(" + cartonno + ")" + " Cartons Only")); 

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }


        private void PrintShipMark()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string mlccod = this.Request.QueryString["mlccod"];
            string dayid = this.Request.QueryString["dayid"];
            string printFormat = this.Request.QueryString["format"];

            LocalReport rpt1 = new LocalReport();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "GET_SHIPPING_MARK", mlccod, dayid, "", "", "", "", "", "", "");
            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.ShippingMark>();
            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptShippingMark", list, null, null);     
            rpt1.EnableExternalImages = true;

            //string size = ds1.Tables[0].Rows[0]["sizes"].ToString();
            //string qty = ds1.Tables[0].Rows[0]["sizesqty"].ToString();

            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + printFormat + "', target='_self');</script>";
        }

        private void PrintShipMarkV2()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string compname = hst["comnam"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string mlccod = this.Request.QueryString["mlccod"];
            string dayid = this.Request.QueryString["dayid"];
            string printFormat = this.Request.QueryString["format"];

            LocalReport rpt1 = new LocalReport();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_EXPORT_02", "GET_SHIPPING_MARK", mlccod, dayid, "", "", "", "", "", "", "");
            var list = ds1.Tables[0].DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.ShippingMark>();
            rpt1 = RptSetupClass.GetLocalReport("R_01_Mer.RptShippingMarkV2", list, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("compName", compname));
            rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + printFormat + "', target='_self');</script>";
        }

        private void PrintDeliveryChallan()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");

            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address:" + compname + " , Session: " + session + " , User: " + username + " , Time: " + printdate;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printType = string.IsNullOrEmpty(Request.QueryString["PrintFormat"]) ? "" : Request.QueryString["PrintFormat"].ToString();


            //string comcod = this.GetCompCode();
            string invno = this.Request.QueryString["genno"].ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CHALLAN", "GET_CHALLAN_PRINT_INFO", invno, "", "", "", "", "", "", "", "");

            DataTable dt1 = ds1.Tables[0];
            var lst = dt1.DataTableToList<SPEENTITY.C_19_Exp.EClassExpBO.EclassExport>();

            DataTable dt2 = ds1.Tables[1];

            double tAmt = lst.Select(p => p.totlprs).Sum();

            LocalReport rpt1 = new LocalReport();

            if (comcod == "5305" || comcod == "5306")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptDeliveryChallanFBFrmt2", lst, null, null);

                var summary = lst.GroupBy(x => x.invno).Select(x => new SPEENTITY.C_19_Exp.EClassExpBO.ChallanSummary
                    {
                        ttlctn = Convert.ToDouble(x.Sum(i => i.totlctn)),
                        ttlpairs = Convert.ToDouble(x.Sum(i => i.totlprs)),
                        invno = x.FirstOrDefault().invno,
                        invrefno = x.FirstOrDefault().invrefno,
                        invdate = x.FirstOrDefault().invdat
                    }
                );

                string chlnDesc = "";

                int counter = 0;
                foreach (var item in summary)
                {
                    counter++;
                    if (counter == 1)
                    {
                        string ttlCtnInWrd = "(" + ASTUtility.Trans(Math.Round(item.ttlctn), 2).Replace("Taka", "") + ") Cartons";
                        string ttlPrsInWrd = "(" + ASTUtility.Trans(Math.Round(item.ttlpairs), 2).Replace("Taka", "") + ") Pairs";
                        string matdesc = "Complete Leather Footwear By Cow Finished Leather";
                        string dispatchAdd = dt2.Rows[0]["exdisdesc"].ToString() + ", " + dt2.Rows[0]["custcountry"].ToString();

                        chlnDesc += "Received " + Convert.ToInt32(item.ttlctn).ToString() + " " + ttlCtnInWrd + " "
                        + Convert.ToInt32(item.ttlpairs).ToString() + " " + ttlPrsInWrd + " "
                        + matdesc
                        + " Export to " + dispatchAdd
                        + ", Invoice: " + item.invrefno
                        + ", Dated: " + item.invdate.ToString("dd.MM.yyyy");
                    }
                    else
                    {
                        chlnDesc += ", Cartons = " + item.ttlctn + " Pcs (" + item.ttlpairs + "), " + item.invrefno + ", Dated: " + item.invdate.ToString("dd.MM.yyyy");
                    }
                }


                double ttlctn = Convert.ToDouble(lst.Select(p => p.totlctn).Sum());
                double ttlpairs = Convert.ToDouble(lst.Select(p => p.totlprs).Sum());
                string invrefno = ds1.Tables[0].Rows[0]["invrefno"].ToString();
                string invdate = ds1.Tables[0].Rows[0]["invdat"].ToString();

                rpt1.SetParameters(new ReportParameter("chlndesc", chlnDesc));
                rpt1.SetParameters(new ReportParameter("deldate", Convert.ToDateTime(dt2.Rows[0]["deldate"]).ToString("dd-MMM-yyyy")));
                rpt1.SetParameters(new ReportParameter("contactPrsn", dt2.Rows[0]["cntctperson"].ToString()));
                rpt1.SetParameters(new ReportParameter("contactNum", dt2.Rows[0]["contact"].ToString()));
                rpt1.SetParameters(new ReportParameter("containerno", dt2.Rows[0]["containerno"].ToString()));
                rpt1.SetParameters(new ReportParameter("licenseno", dt2.Rows[0]["licenseno"].ToString()));
            }
            else if (comcod == "5301")
            {
                rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptDeliveryChallanEdison2", lst, null, null);
                rpt1.EnableExternalImages = true;

                rpt1.SetParameters(new ReportParameter("invrefno", ds1.Tables[0].Rows[0]["invrefno"].ToString()));
            }


            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            // rpt1.SetParameters(new ReportParameter("RptTitle", "Order Sheet"));   ,  , 
            rpt1.SetParameters(new ReportParameter("custName", dt2.Rows[0]["custdesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("custadd", dt2.Rows[0]["custadd"].ToString()));
            rpt1.SetParameters(new ReportParameter("custCuntry", dt2.Rows[0]["custcountry"].ToString()));
            rpt1.SetParameters(new ReportParameter("RefNo", dt2.Rows[0]["refno"].ToString()));
            rpt1.SetParameters(new ReportParameter("dadd", dt2.Rows[0]["exdisdesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("Discharge", dt2.Rows[0]["exdisdesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("MofPay", dt2.Rows[0]["paymode"].ToString()));
            rpt1.SetParameters(new ReportParameter("contact", dt2.Rows[0]["cntctperson"].ToString() + " -" + dt2.Rows[0]["contact"].ToString()));
            rpt1.SetParameters(new ReportParameter("totlctn", Convert.ToDouble(lst.Select(p => p.totlctn).Sum()).ToString()));
            rpt1.SetParameters(new ReportParameter("ordrno", ds1.Tables[0].Rows[0]["orderno"].ToString()));
            rpt1.SetParameters(new ReportParameter("artclno", dt2.Rows[0]["artno"].ToString()));
            rpt1.SetParameters(new ReportParameter("colordesc", ds1.Tables[0].Rows[0]["colordesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("styledesc", ds1.Tables[0].Rows[0]["styledesc"].ToString()));
            rpt1.SetParameters(new ReportParameter("invno", dt2.Rows[0]["dchno1"].ToString()));
            rpt1.SetParameters(new ReportParameter("deport", dt2.Rows[0]["deport"].ToString()));
            rpt1.SetParameters(new ReportParameter("transport", dt2.Rows[0]["transport"].ToString()));
            rpt1.SetParameters(new ReportParameter("tranadd", dt2.Rows[0]["tranadd"].ToString()));
            rpt1.SetParameters(new ReportParameter("invdate", Convert.ToDateTime(dt2.Rows[0]["deldate"]).ToString("dd-MMM-yyyy")));

            rpt1.SetParameters(new ReportParameter("VechNo", dt2.Rows[0]["vehclno"].ToString()));
            rpt1.SetParameters(new ReportParameter("sealno", dt2.Rows[0]["sealno"].ToString()));
            rpt1.SetParameters(new ReportParameter("DechBy", dt2.Rows[0]["despatchtype"].ToString()));
            rpt1.SetParameters(new ReportParameter("comaddf", dt2.Rows[0]["comaddf"].ToString()));

            rpt1.SetParameters(new ReportParameter("txtPreparedBy", dt2.Rows[0]["postedname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["posteddat"]));
            rpt1.SetParameters(new ReportParameter("txtReviewBy", dt2.Rows[0]["reviwname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["revidat"]));
            rpt1.SetParameters(new ReportParameter("txtAuthBy", dt2.Rows[0]["authname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["authdat"]));
            rpt1.SetParameters(new ReportParameter("txtApprovedBy", dt2.Rows[0]["appname"].ToString() + "\n" + ds1.Tables[1].Rows[0]["aprvdat"]));

            rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(tAmt), 2).Replace("Taka", "Pair")));



            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }

        private void PrintMRInfo()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string mrno = this.Request.QueryString["mrno"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_EXPORT", "GETPAYMENTINFO", mrno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            DataTable dt12 = (DataTable)ds1.Tables[1];
            var lst12 = dt12.DataTableToList<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>();

            //ViewState["tblacinfo"] = ds1.Tables[0];
            ViewState["tblcollection"] = lst12;
            ViewState["dtbankdata"] = ds1.Tables[2];

            string buyername = ds1.Tables[0].Rows[0]["resdesc"].ToString();
            string refno = mrno;//this.ddltype.SelectedItem.Text.ToString() + txtissueno.Text.ToString();
            string date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["paydat"]).ToString("dd-MMM-yyyy");
            string bankname = ds1.Tables[0].Rows[0]["cactdesc"].ToString();
            string currency = ds1.Tables[1].Rows[0]["curdesc"].ToString();
            string currate = Convert.ToDouble(ds1.Tables[1].Rows[0]["colconvrate"]).ToString("#,##0.00 ;-#,##0.00; ");

            DataTable dt = (DataTable)ViewState["dtbankdata"];

            var lst = (List<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassDebtorBill>)ViewState["tblcollection"];

            var lst1 = dt.DataTableToList<SPEENTITY.C_19_Exp.EClassReceiptaPayment.EClassBankData>();
            ///   EClassBankData
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass.GetLocalReport("R_19_Exp.RptExportRealization", lst, lst1, null);


            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            rpt1.SetParameters(new ReportParameter("buyername", "Buyer Name: " + buyername));
            rpt1.SetParameters(new ReportParameter("refno", "Ref. No: " + refno));

            rpt1.SetParameters(new ReportParameter("date", "Date: " + date));
            rpt1.SetParameters(new ReportParameter("bankname", "Bank Name: " + bankname));
            rpt1.SetParameters(new ReportParameter("currency", "Currency Name: " + currency));
            rpt1.SetParameters(new ReportParameter("currate", "Currency Rate: " + currate));


            rpt1.SetParameters(new ReportParameter("RptTitle", "Export Realization"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string type = Request.QueryString["Type"].ToString();

            switch (type)
            {
                case "DelChallan":
                    string mlccod = dt1.Rows[0]["mlccod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["mlccod"].ToString() == mlccod)
                        {
                            mlccod = dt1.Rows[j]["mlccod"].ToString();
                            dt1.Rows[j]["artno"] = "";
                        }

                        else
                            mlccod = dt1.Rows[j]["mlccod"].ToString();
                    }
                    break;
            }

            return dt1;
        }

    }
}