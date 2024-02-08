using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_19_Exp
{
    public class EClassExpBO
    {
        [Serializable]

        public class EclassExpInvoic01
        {
            public string comcod { get; set; }
            public string mlccod { get; set; }
            public string mlcdes { get; set; }
            public string gencod { get; set; }
            public string grndes { get; set; }
            public string refcod { get; set; }
            public string gendata { get; set; }
            public double refval { get; set; }
            public string data1 { get; set; }
            public DateTime dateval { get; set; }
            public string comsnam { get; set; }
            public string comnam { get; set; }
            public string comadd1 { get; set; }
            public string comadd2 { get; set; }
            public string comadd3 { get; set; }
            public string comadd4 { get; set; }
        }

        [Serializable]

        public class EclassEcpInvoice2
        {
            public string comcod { get; set; }
            public string invno { get; set; }
            public string invno2 { get; set; }
            public string artno { get; set; }
            public string orderid { get; set; }
            public string color { get; set; }
            public string mlccod { get; set; }
            public string ordrid { get; set; }
            public string styleid { get; set; }
            public string shipmid { get; set; }
            public DateTime shipmdat { get; set; }
            public string exportno { get; set; }
            public DateTime exportdt { get; set; }
            public string blawbno { get; set; }
            public DateTime blawbdt { get; set; }
            public string cntnrno { get; set; }
            public string invrmrks { get; set; }
            public string shipmark { get; set; }
            public string tqtydes { get; set; }
            public string tnwtdes { get; set; }
            public string tgwtdes { get; set; }
            public string tcbmdes { get; set; }
            public string msurmnt { get; set; }
            public string orderdes { get; set; }
            public double totqty { get; set; }
            public double tcrtnqty { get; set; }
            public string styldes { get; set; }
            public string stylunit { get; set; }
            public double stylrate { get; set; }
            public double stylamt { get; set; }
        }

        [Serializable]
        public class EclassExport
        {
            public string artno { get; set; }
            public string mlccod { get; set; }
            public string mlcdesc { get; set; }
            public string styleid { get; set; }
            public string styledesc { get; set; }
            public string colorid { get; set; }
            public string colordesc { get; set; }
            public string sizeid { get; set; }
            public string sizedesc { get; set; }
            public double ordrqty { get; set; }
            public double prdqty { get; set; }
            public double rate { get; set; }
            public string ordrno { get; set; }
            public string hscode { get; set; }
            public double pperctnqty { get; set; }
            public double totlprs { get; set; }
            public double totlctn { get; set; }
            public double gwperctn { get; set; }
            public double nwperctn { get; set; }
            public string dimenctn { get; set; }
            public double cbm { get; set; }
            public double totalgw { get; set; }
            public double balqty { get; set; }
            public string invno { get; set; }
            public string invrefno { get; set; }
            public DateTime invdat { get; set; }
            public string rdayid { get; set; }
            public double invqty { get; set; }
            public double itmamt { get; set; }
            public string ordno { get; set; }
            public string orderno { get; set; }
            public string location { get; set; }
            public string locdesc { get; set; }
            public string remarks { get; set; }
            public string refno { get; set; }
            public string wearhouse { get; set; }
            public string po { get; set; }
            public double stockqty { get; set; }
            public double invqty1 { get; set; }
            public double chlntotal { get; set; }
            public bool stockstatus {get;set;}
            public double inprocqty {get;set;}
            public double slrowid { get; set; }

            public EclassExport() { }
            public EclassExport(string mlccod, string mlcdesc, string styleid, string styledesc, string colorid, string colordesc,
                string sizeid, string sizedesc, double ordrqty, double rate, string ordrno, string hscode, double pperctnqty,
                double totlprs, double totlctn, double gwperctn, double nwperctn, string dimenctn, double cbm, double totalgw, double prdqty,
                double balqty, string rdayid, double itmamt, string ordno, double inprocqty)
            {
                this.mlccod = mlccod;
                this.mlcdesc = mlcdesc;
                this.styleid = styleid;
                this.styledesc = styledesc;
                this.colorid = colorid;
                this.colordesc = colordesc;
                this.sizeid = sizeid;
                this.sizedesc = sizedesc;
                this.ordrqty = ordrqty;
                this.rate = rate;
                this.ordrno = ordrno;
                this.hscode = hscode;
                this.pperctnqty = pperctnqty;
                this.totlprs = totlprs;
                this.totlctn = totlctn;
                this.gwperctn = gwperctn;
                this.nwperctn = nwperctn;
                this.dimenctn = dimenctn;
                this.cbm = cbm;
                this.totalgw = totalgw;
                this.prdqty = prdqty;
                this.balqty = balqty;
                this.rdayid = rdayid;
                this.itmamt = itmamt;
                this.ordno = ordno;
                this.inprocqty = inprocqty;
            }
            public EclassExport(string mlccod, string mlcdesc, string styleid, string styledesc, string colorid, string colordesc,
                string sizeid, string sizedesc, double ordrqty, double rate, string ordrno, string po, string hscode, double pperctnqty,
                double totlprs, double totlctn, double gwperctn, double nwperctn, string dimenctn, double cbm, double totalgw, double prdqty,
                double balqty, string rdayid, double itmamt, string ordno, double inprocqty)
            {
                this.mlccod = mlccod;
                this.mlcdesc = mlcdesc;
                this.styleid = styleid;
                this.styledesc = styledesc;
                this.colorid = colorid;
                this.colordesc = colordesc;
                this.sizeid = sizeid;
                this.sizedesc = sizedesc;
                this.ordrqty = ordrqty;
                this.rate = rate;
                this.ordrno = ordrno;
                this.hscode = hscode;
                this.pperctnqty = pperctnqty;
                this.totlprs = totlprs;
                this.totlctn = totlctn;
                this.gwperctn = gwperctn;
                this.nwperctn = nwperctn;
                this.dimenctn = dimenctn;
                this.cbm = cbm;
                this.totalgw = totalgw;
                this.prdqty = prdqty;
                this.balqty = balqty;
                this.rdayid = rdayid;
                this.itmamt = itmamt;
                this.ordno = ordno;
                this.inprocqty = inprocqty;
                this.po = po;
            }
            public EclassExport(string invno, string mlccod, string mlcdesc, string styleid, string styledesc, string colorid, string colordesc,
                string sizeid, string sizedesc, double totlprs, double totlctn, double invqty, string rdayid, string location,
                string locdesc, string wearhouse, string po, double stockqty, double invqty1, double chlntotal, double slrowid)
            {
                this.invno = invno;
                this.mlccod = mlccod;
                this.mlcdesc = mlcdesc;
                this.styleid = styleid;
                this.styledesc = styledesc;
                this.colorid = colorid;
                this.colordesc = colordesc;
                this.sizeid = sizeid;
                this.sizedesc = sizedesc;            
                this.totlprs = totlprs;
                this.totlctn = totlctn;
                this.invqty = invqty;
                this.rdayid = rdayid;
                this.location = location;
                this.locdesc = locdesc;
                this.stockqty = stockqty;
                this.wearhouse = wearhouse;
                this.po = po;
                this.invqty1 = invqty1;
                this.chlntotal = chlntotal;
                this.slrowid = slrowid;

            }
        }
        [Serializable]
        public class RptEclassExportSummery
        {
            public string comcod { get; set; }
            public string invno { get; set; }
            public DateTime invdate { get; set; }
            public string mlccod { get; set; }
            public string lcctno { get; set; }
            public string lcctdate { get; set; }
            public string rdayid { get; set; }
            public string styleid { get; set; }
            public double totlprs { get; set; }
            public double ttlamt { get; set; }
            public string remarks { get; set; }
            public string invrefno { get; set; }
            public string buyerid { get; set; }
            public string curcode { get; set; }
            public string buyername { get; set; }
            public string expno { get; set; }
            public DateTime exfacdt { get; set; }
            public string curdesc { get; set; }
            public string cursymbol { get; set; }
            public string itmname { get; set; }
            public string billno { get; set; }
            public string realizationno { get; set; }
            public string billdate { get; set; }
            public string custid { get; set; }
            public string custdesc { get; set; }
            public double collamt { get; set; }
            public double unrealamt { get; set; }
            public double usdunrealamt { get; set; }
            public double eurounrealamt { get; set; }
            public double poununrealamt { get; set; }
            
            public double real1 { get; set; }
            public string realdat1 { get; set; }
            public double realcon1 { get; set; }
            
            public double real2 { get; set; }
            public string realdat2 { get; set; }
            public double realcon2 { get; set; }
            
            public double real3 { get; set; }
            public string realdat3 { get; set; }
            public double realcon3 { get; set; }

            public double shortamt { get; set; }

            public double aitamt { get; set; }
            public double commamt { get; set; }
            public double othcharge { get; set; }
            public double fundbillup { get; set; }
            public double bactobacfc { get; set; }
            public double bactobacbdt { get; set; }
            public double remainamt { get; set; }
            public double ttlrealizbdt { get; set; }


            public RptEclassExportSummery() { }

        }
        [Serializable]
        public class RptEclassExportDoc
        {
            public string mlccod { get; set; }
            public string mlcdesc { get; set; }
            public string styleid { get; set; }
            public string styledesc { get; set; }
            public string colorid { get; set; }
            public string colordesc { get; set; }
            public string stylunit { get; set; }
            public string sizeid { get; set; }
            public string sizedesc { get; set; }
            public string ordrno { get; set; }
            public string ordrref { get; set; }
            public string formaname { get; set; }
            public string artno { get; set; }
            public double itmamt { get; set; }
            public string hscode { get; set; }
            public double ordrqty { get; set; }
            public double rate { get; set; }
            public double stylamt { get; set; }
            public double pperctnqty { get; set; }
            public double totlprs { get; set; }
            public double totlctn { get; set; }
            public double gwperctn { get; set; }
            public double tgwwth { get; set; }
            public double nwperctn { get; set; }
            public double tnetwth { get; set; }
            public string dimenctn { get; set; }
            public string customercod { get; set; }
            public string customer { get; set; }
            public string forma { get; set; }
            public string upper { get; set; }
            public string smpltyp { get; set; }
            public double cbm { get; set; }
            public double qty { get; set; }
            public string po { get; set; }
            public string rdayid { get; set; }
            public double totalgw { get; set; }
            public double s1 { get; set; }
            public double s2 { get; set; }
            public double s3 { get; set; }
            public double s4 { get; set; }
            public double s5 { get; set; }
            public double s6 { get; set; }
            public double s7 { get; set; }
            public double s8 { get; set; }
            public double s9 { get; set; }
            public double s10 { get; set; }
            public double s11 { get; set; }
            public double s12 { get; set; }
            public double s13 { get; set; }
            public double s14 { get; set; }
            public double s15 { get; set; }
            public double s16 { get; set; }
            public double s17 { get; set; }
            public double s18 { get; set; }
            public double s19 { get; set; }
            public double s20 { get; set; }


            public RptEclassExportDoc() { }

        }

        [Serializable]
        public class RptEclassFrdletter
        {
            public string itmcode { get; set; }
            public string itmdesc { get; set; }
            public string refno { get; set; }
            public string doctype { get; set; }
            public string noofcopy { get; set; }

            public RptEclassFrdletter() { }

        }
        [Serializable]
        public class RptExportPlan
        {
            public string comcod { get; set; }
            public string preqno { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public double conspair { get; set; }
            public string mlccod { get; set; }
            public string inqno { get; set; }
            public string article { get; set; }
            public string brand { get; set; }
            public string colordesc { get; set; }
            public string sizerange { get; set; }
            public string buyerid { get; set; }
            public string buyername { get; set; }
            public string styleid { get; set; }
            public string colorid { get; set; }
            public double colororderqty { get; set; }
            public string images { get; set; }
            public double ordrqty { get; set; }

            public RptExportPlan()
            {
            }
        }
        [Serializable]
        public class RptExportPlan1
        {
            public string comcod { get; set; }
            public string trialordr { get; set; }
            public string mlccod { get; set; }
            public string styleid { get; set; }
            public string styledesc { get; set; }
            public string colorid { get; set; }
            public string colordesc { get; set; }
            public double s1 { get; set; }
            public double s2 { get; set; }
            public double s3 { get; set; }
            public double s4 { get; set; }
            public double s5 { get; set; }
            public double s6 { get; set; }
            public double s7 { get; set; }
            public double s8 { get; set; }
            public double s9 { get; set; }
            public double s10 { get; set; }
            public double s11 { get; set; }
            public double s12 { get; set; }
            public double s13 { get; set; }
            public double s14 { get; set; }
            public double s15 { get; set; }
            public double s16 { get; set; }
            public double s17 { get; set; }
            public double s18 { get; set; }
            public double s19 { get; set; }
            public double s20 { get; set; }
            public double s21 { get; set; }
            public double s22 { get; set; }
            public double s23 { get; set; }
            public double s24 { get; set; }
            public double s25 { get; set; }
            public double s26 { get; set; }
            public double s27 { get; set; }
            public double s28 { get; set; }
            public double s29 { get; set; }
            public double s30 { get; set; }
            public double s31 { get; set; }
            public double s32 { get; set; }
            public double s33 { get; set; }
            public double s34 { get; set; }
            public double s35 { get; set; }
            public double s36 { get; set; }
            public double s37 { get; set; }
            public double s38 { get; set; }
            public double s39 { get; set; }
            public double s40 { get; set; }
            public double totalqty { get; set; }
            public RptExportPlan1()
            {
            }
        }
        [Serializable]
        public class RptExportPlan2
        {
            public string comcod { get; set; }
            public string mlccod { get; set; }
            public string styleid { get; set; }
            public string colorid { get; set; }
            public string sizeid { get; set; }
            public string sizedesc { get; set; }
            public RptExportPlan2()
            {
            }
        }
        [Serializable]
        public class RptExportPlan3
        {
            public string comcod { get; set; }
            public string mlccod { get; set; }
            public string ordrid { get; set; }
            public string compcode { get; set; }
            public string compname { get; set; }
            public string procname { get; set; }
            public string procode { get; set; }
            public double rsqty { get; set; }
            public string dayid { get; set; }
            public string styleid { get; set; }
            public string colorid { get; set; }
            public string itmno { get; set; }
            public string itmdesc { get; set; }
            public string itmunit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string ssircode { get; set; }
            public double consppair { get; set; }
            public double itmqty { get; set; }
            public double itmrat { get; set; }
            public double prwestpc { get; set; }
            public double westpc { get; set; }
            public double ttlreqrd { get; set; }
            
            public RptExportPlan3()
            {
            }
        }

        public class ChallanSummary
        {
            public double ttlctn { get; set; }
            public double ttlpairs { get; set; }
            public string invno { get; set; }
            public string invrefno { get; set; }
            public DateTime invdate { get; set; }
        }
        
        [Serializable]
        public class RptIncntvDeclaration
        {
            public string comcod { get; set; }
            public string invno { get; set; }
            public string rdayid { get; set; }
            public string invrefno { get; set; }
            public DateTime invdate { get; set; }
            public string amount { get; set; }
        }


        [Serializable]
        public class ShippingMark
        {
            public string packid { set; get; }
            public string itmno { set; get; }
            public string article { set; get; }
            public string packdesc { set; get; }
            public string colorname { set; get; }
            public string spcfcod { set; get; }
            public string cartoon { set; get; }
            public double perctn { set; get; }
            public string categorydesc { set; get; }
            public string desc1 { set; get; }
            public string buyerid { set; get; }
            public string buyername { set; get; }
            public string orderno { set; get; }
            public string buyeraddress { set; get; }
            public string sizes { set; get; }    
            public string sizesqty { set; get; }
            public string lformacod { set; get; }
            public string formadesc { set; get; }

        }


        [Serializable]
        public class EclassExportReturn
        {
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public double itmqty { get; set; }
            public double rate { get; set; }
            public double itmamt { get; set; }
            public string model { get; set; }
            public string rsirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string centrid { get; set; }
            public string centrdesc { get; set; }
            public string ssircode { get; set; }
            public string rsirunit { get; set; }
            public string batchcode { get; set; }
            public string batchdesc { get; set; }
            public string remarks { get; set; }

            public EclassExportReturn() { }

            public EclassExportReturn(string rsircode, string spcfcod, double itmqty, double rate, double itmamt, string rsirdesc,
                string spcfdesc, string centrid, string centrdesc, string ssircode, string rsirunit, string batchcode, string batchdesc)
            {


                this.rsircode = rsircode;
                this.spcfcod = spcfcod;
                this.itmqty = itmqty;
                this.rate = rate;
                this.itmamt = itmamt;
                this.rsirdesc = rsirdesc;
                this.spcfdesc = spcfdesc;
                this.centrid = centrid;
                this.centrdesc = centrdesc;
                this.ssircode = ssircode;
                this.rsirunit = rsirunit;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
            }
        }

        [Serializable]
        public class EclassExportList
        {
            public string mgrrno { get; set; }
            public string actdesc { get; set; }
            public string itmdesc { get; set; }
            public string spcfdesc { get; set; }
            public string itmunit { get; set; }
            public double conqty { get; set; }
            public double conamt { get; set; }
            public EclassExportList() { }

        }

    }
}

  