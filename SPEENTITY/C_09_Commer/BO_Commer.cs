using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_09_Commer
{
    [Serializable]
    public class BBLCPayStatus
    {
        public string comcod { get; set; }
        public string mlccod { get; set; }
        public string mlcdesc { get; set; }
        public string blccod { get; set; }
        public string blcdesc { get; set; }
        public string suplrid { get; set; }
        public string supdesc { get; set; }
        public DateTime suppldat { get; set; }
        public DateTime settldat { get; set; }
        public DateTime paydat { get; set; }
        public double amt { get; set; }
        public double grossamt { get; set; }
        public double dueamt { get; set; }
        public double paidamt { get; set; }
        public string vounum { get; set; }
        public double diffday { get; set; }
        public BBLCPayStatus() { }
    }

    [Serializable]
    public class EClassBBLCInfo
    {
        public string comcod { get; set; }
        public string blccod { get; set; }
        public string blcdesc { get; set; }
        public string bankid { get; set; }
        public string BankName { get; set; }
        public string suplrid { get; set; }
        public string supdesc { get; set; }
        public string bblcordrid { get; set; }
        public DateTime ordrdat { get; set; }
        public DateTime suppldat { get; set; }
        public DateTime settldat { get; set; }

        public EClassBBLCInfo() { }
        public EClassBBLCInfo(string comcod, string blccod, string blcdesc, string bankid, string BankName, string suplrid, string supdesc,
            string bblcordrid, DateTime ordrdat, DateTime suppldat, DateTime settldat)
        {
            this.comcod = comcod;
            this.blccod = blccod;
            this.blcdesc = blcdesc;
            this.bankid = bankid;
            this.BankName = BankName;
            this.suplrid = suplrid;
            this.supdesc = supdesc;
            this.bblcordrid = bblcordrid;
            this.ordrdat = ordrdat;
            this.suppldat = suppldat;
            this.settldat = settldat;

        }
    }

    [Serializable]
    public class LocalPORCV
    {
        public string reqno { get; set; }
        public string reqno1 { get; set; }
        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public string bomid { get; set; }
        public string rsirdesc1 { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public string spcfdesc { get; set; }
        public string rsirunit { get; set; }
        public double orderqty { get; set; }
        public double mrrqty { get; set; }
        public double orderbal { get; set; }
        public double recup { get; set; }
        public double mrrrate { get; set; }
        public double mrramt { get; set; }
        public string mrrnote { get; set; }
        public double chlnqty { get; set; }
        public string rackno { get; set; }
        public string location { get; set; }

    }

    [Serializable]
    public class EClassLCMGT
    {
        public string comcod { get; set; }
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public string storid { get; set; }
        public string stordesc { get; set; }
        public string rcvno { get; set; }
        public string grrno { get; set; }
        public string lotno { get; set; }
        public string chalanno { get; set; }
        public DateTime chalandate { get; set; }
        public DateTime expdate { get; set; }
        public DateTime rcvdate { get; set; }
        public string rescod { get; set; }
        public string resdesc { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public string unit { get; set; }
        public double rcvqty { get; set; }
        public double balqty { get; set; }
        public double qcqty { get; set; }
        public double ordrqty { get; set; }
        public double freeqty { get; set; }
        public double rcvuptolast { get; set; }
        public double remainordr { get; set; }
        public double trate { get; set; }
        public double revamt { get; set; }
        public double preqcqty { get; set; }
        public double remqty { get; set; }
        public string remarks { get; set; }
        public string rackno { get; set; }
        public string location { get; set; }
        public string bomid { get; set; }
        public double shipqty { get; set; }
        public double reporttype { get; set; }
        public string reqno { get; set; }
        public string syspon { get; set; }
        public string pono { get; set; }

    }

    public class BO_LCOpening
    {
        public string rescod { get; set; }
        public string resdesc { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public string scode { get; set; }
        public string unit { get; set; }
        public double ordrqty { get; set; }
        public double freeqty { get; set; }
        public double rate { get; set; }
        public double conver { get; set; }
        public double amount { get; set; }
        public double bdamount { get; set; }
    }
  
    public class EClassBBLMatInfo
    {
        public string comcod { get; set; }
        public string reqno { get; set; }
        public string reqno1 { get; set; }
        public string orderno { get; set; }
        public string orderno1 { get; set; }
        public string mrrno{ get; set; }
        public string mrrno1{ get; set; }
        public string pactcode { get; set; }
        public string rsircode { get; set; }
        public string rsirdesc1 { get; set; }
        public string spcfcod { get; set; }
        public double mrrqty { get; set; }
        public double mrrrate { get; set; }
        public double mrramt { get; set; }
        public string rsirunit { get; set; }
        public DateTime mrrdate { get; set; }
        public string chlnno { get; set; }
        public double chlnqty { get; set; }
        public string mrrnote { get; set; }
        public string var1 { get; set; }
        
       

    }

    [Serializable]
    public class BO_LCGenInfo
    {
        public string comcod { get; set; }
        public string gcod { get; set; }
        public string ssircode { get; set; }
        public string gdesc { get; set; }
        public string gdesc1 { get; set; }
        public string gph { get; set; }
    }

    [Serializable]
    public class BO_Meterails
    {
        public string mlcdesc { get; set; }
        public string bomid { get; set; }
        public string reqno { get; set; }
        public string reqno1 { get; set; }
        public string reqdat { get; set; }
        public string rsirdesc { get; set; }
        public string spcfdesc { get; set; }
        public string unit { get; set; }
        public double reqqty { get; set; }
        public double mrrqty { get; set; }
        public double remainqty { get; set; }
        public string reqtype { get; set; }
        public string buyername { get; set; }
        public string deptname { get; set; }
    }


    [Serializable]
    public class BomWiseMatSummary
    {
        public string comcod { get; set; }
        public string matcod { get; set; }
        public string matdesc { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public double itmqty { get; set; }
        public double stockqty { get; set; }
        public double ordqty { get; set; }
        public double prjectionord { get; set; }
        public double ttlorder { get; set; }
        public double shipqty { get; set; }
        public double shipbalqty { get; set; }
        public double isuqty { get; set; }
        public double rcvqty { get; set; }
        public double bombalrcv { get; set; }
        public double bombalpo { get; set; }
        public double bombalissue { get; set; }
    }
    
    [Serializable]
    public class BO_Matwisepo
    {
        public string comcod { get; set; }
        public string orderno { get; set; }
        public string syspon { get; set; }
        public string custompon { get; set; }
        public string orderdat { get; set; }
        public string bblccode { get; set; }
        public string ssirdesc { get; set; }
        public string rsircode { get; set; }
        public string itemdesc { get; set; }
        public string spcfcod { get; set; }
        public string spcdesc { get; set; }
        public string sizedesc { get; set; }
        public string colordesc { get; set; }
        public double ordrqty { get; set; }
        public double ordrate { get; set; }
        public double ordamt { get; set; }
        public string pactcode { get; set; }
        public string pactdesc { get; set; }
        public string season { get; set; }
        public string curdesc { get; set; }

    }

    [Serializable]
    public class BO_WeeklyWiseMat
    {
        public string comcod { get; set; }
        public string rsirdesc { get; set; }
        public string rsircode { get; set; }
        public string spcfcode { get; set; }
        public string spcfdesc { get; set; }
        public double conqty { get; set; }
        public double stockqty { get; set; }
        public double onrcvqty { get; set; }
        public double shipqty { get; set; }

    }


    [Serializable]
    public class BOMvsReceidved
    {
        public string comcod { get; set; }
        public string bomid { get; set; }
        public string rsircode { get; set; }
        public string rsirdesc { get; set; }
        public string unit { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public double reqqty { get; set; }
        public double reqamt { get; set; }
        public double projorder { get; set; }
        public double mrrqty { get; set; }
        public double mrramt { get; set; }
        public double remainqty { get; set; }
        public DateTime reqdat { get; set; }
        public string mlccod { get; set; }
        public string mlcdesc { get; set; }
        public string dayid { get; set; }
        public string buyerid { get; set; }
        public string buyername { get; set; }
        public double bomamt { get; set; }
        public double bomqty { get; set; }
        public double purordqty { get; set; }
        public string defaultsup { get; set; }
        public string defaultsupname { get; set; }
        public double rate { get; set; }
        public double stkqty { get; set; }
    }


    [Serializable]
    public class WorkOrderVsSupply
    {
        public string comcod { get; set; }
        public string orderno { get; set; }
        public string orderno1 { get; set; }
        public string pactcode { get; set; }
        public string orderdat { get; set; }
        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public double ordrqty { get; set; }
        public double shippedqty { get; set; }
        public double shipbalqty { get; set; }
        public double price { get; set; }
        public double mrrqty { get; set; }
        public double billqty { get; set; }
        public double balqty { get; set; }
        public string resdesc { get; set; }
        public string resunit { get; set; }
        public string pactdesc { get; set; }
        public string spcfdesc { get; set; }
        public string ssircode { get; set; }
        public string ssirdesc { get; set; }
        public double ordamt { get; set; }
        public string curcodedesc { get; set; }
        public string cursymbol { get; set; }
        public string selection { get; set; }
        public string shipperdesc { get; set; }
        public string expdeldat { get; set; }
    }

    [Serializable]
    public class IndSupPurchase
    {
        public string comcod { get; set; }
        public string mrrno { get; set; }
        public string mrrref { get; set; }
        public string mrrno1 { get; set; }
        public DateTime mrrdat { get; set; }
        public string pactcode { get; set; }
        public string ssircode { get; set; }
        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public double qty { get; set; }
        public double amt { get; set; }
        public double rate { get; set; }
        public string orderno { get; set; }
        public string orderno1 { get; set; }
        public string ordrref { get; set; }
        public string reqno { get; set; }
        public string reqno1 { get; set; }
        public string billno { get; set; }
        public string billno1 { get; set; }
        public string mrfno { get; set; }
        public string resdesc { get; set; }
        public string resunit { get; set; }
        public string supdesc { get; set; }
        public string pactdesc { get; set; }
        public string usrsname { get; set; }
        public string spcfdesc { get; set; }
        public string color { get; set; }
    }


    [Serializable]
    public class RptPurchaseSummary
    {
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string rptcode { get; set; }
        public double qty { get; set; }
        public double amt { get; set; }
        public double rate { get; set; }
        public string rptdesc { get; set; }
        public string rptunit { get; set; }
        public string pactdesc { get; set; }
    }


    [Serializable]
    public class RptSeasonSummary
    {
        public string comcod { get; set; }
        public string ssircode { get; set; }
        public string ssirdesc { get; set; }
        public double ordrqty { get; set; }
        public double ordamt { get; set; }
        public double ordamt1 { get; set; }
        public string curcode { get; set; }
        public string curcodedesc { get; set; }
        public string cursymbol { get; set; }
        public string countrycode { get; set; }
        public string country { get; set; }
        public double ordamtbdt { get; set; }
        public double oramteuro { get; set; }
    }
    

    [Serializable]
    public class RptCountrySummary
    {
        public string comcod { get; set; }
        public string countrycode { get; set; }
        public string country { get; set; }
        public double ordrqty { get; set; }
        public double ordamt { get; set; }
        public double ordamtbdt { get; set; }
        public double oramteuro { get; set; }
    }


    [Serializable]
    public class RptSeasonBySeasonSupplierSummary
    {
        public string comcod { get; set; }
        public string ssircode { get; set; }
        public string ssirdesc { get; set; }
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

    }


    [Serializable]
    public class SeasonSum
    {
        public string season { get; set; }
        public string seasondesc { get; set; }
        public double ordamt { get; set; }
    }


    [Serializable]
    public class RptRawMatSupLeadTime
    {
        public string comcod { get; set; }
        public string orderno { get; set; }
        public string pono { get; set; }
        public string orderdat { get; set; }
        public string season { get; set; }
        public string ssircode { get; set; }
        public string ssirdesc { get; set; }
        public string rcvdate { get; set; }
        public double leadtime { get; set; }
        public double delcount { get; set; }
        public string catagories { get; set; }
    }

    [Serializable]
    public class RptSupLeadTimeSummary
    {
        public string comcod { get; set; }
        public double delcount { get; set; }
        public string catagories { get; set; }
        public double percnt { get; set; }
        public string remarks { get; set; }
    }

    public class RptSeasonOverviewOfMaterials
    {
        public string comcod { get; set; }
        public string itmno { get; set; }
        public string itmdesc { get; set; }
        public string unit { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public string colordesc { get; set; }
        public string ssircode { get; set; }
        public string suplier { get; set; }
        public double reqandallw { get; set; }
        public double itmqty { get; set; }
        public double stqty { get; set; }
        public double reqallw { get; set; }
        public double ordrqty { get; set; }
        public double rcvqty { get; set; }
        public double shipqty { get; set; }
        public double ndtobuy { get; set; }
        public double ndtobuypo { get; set; }
        public double poship { get; set; }
        public double poinhouseqty { get; set; }
    }

    [Serializable]
    public class RptMatPriceVariance
    {
        public string comcod { get; set; }
        public string rsircode { get; set; }
        public string rsirdesc { get; set; }
        public string rsirunit { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public string ssircode { get; set; }
        public double o1 { get; set; }
        public double o2 { get; set; }
        public double o3 { get; set; }
        public double o4 { get; set; }
        public double o5 { get; set; }
        public double r1 { get; set; }
        public double r2 { get; set; }
        public double r3 { get; set; }
        public double r4 { get; set; }
        public double r5 { get; set; }
        public double amt1 { get; set; }
        public double amt2 { get; set; }
        public double amt3 { get; set; }
        public double amt4 { get; set; }
        public double amt5 { get; set; }

    }



    [Serializable]
    public class RptDayWisPrchse
    {
        public string comcod { get; set; }
        public string mrrno { get; set; }
        public string mrrno1 { get; set; }
        public string mrrref { get; set; }
        public string chlno { get; set; }
        public string mrrdat { get; set; }
        public string pactcode { get; set; }
        public string ssircode { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public string color { get; set; }
        public double amt { get; set; }
        public double qty { get; set; }
        public double rate { get; set; }
        public string orderno { get; set; }
        public string orderno1 { get; set; }
        public string pono { get; set; }
        public string ordrref { get; set; }
        public string reqno { get; set; }
        public string reqno1 { get; set; }
        public string billno { get; set; }
        public string billno1 { get; set; }
        public string mrfno { get; set; }
        public string resdesc { get; set; }
        public string resunit { get; set; }
        public string supdesc { get; set; }
        public string pactdesc { get; set; }
        public string usrsname { get; set; }
        public string season { get; set; }
    }


    [Serializable]

    public class RptPurMktSurvey1
    {
        public string comcod { get; set; }
        public string rsircode { get; set; }
        public string rsirdesc { get; set; }
        public string rsirunit { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public string color { get; set; }
        public string s1 { get; set; }
        public string s2 { get; set; }
        public string s3 { get; set; }
        public string s4 { get; set; }
        public string s5 { get; set; }
    }

    public class RptPurMktSurvey2
    {
        public string ssircode { get; set; }
        public string ssirdesc { get; set; }
        public string rate { get; set; }
        public string curdesc { get; set; }
    }

}
