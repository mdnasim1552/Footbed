using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_09_Commer
{
    public class BO_AllLCInfo
    {
        [Serializable]
        public class AllLCInfolist
        {
            public string actcode { set; get; }
            public string actdesc1 { set; get; }
            public string lcdate { set; get; }
            public string shipdate { set; get; }
            public string shipardate { set; get; }
            public string deldate { set; get; }
            public string expdate { set; get; }
            public double cnvrsion { set; get; }
            public double fcamt { set; get; }
            public double bdamt { set; get; }
            public string bankname { set; get; }
            public string actdesc { set; get; }
            public string currency { set; get; }
            public string curdesc { set; get; }
            public string csplname { set; get; }
            public string cspldesc { set; get; }
            public AllLCInfolist(string actdesc1, string actcode, string lcdate, string shipdate, string shipardate, string deldate, string expdate, double cnvrsion, double fcamt, double bdamt, string bankname, string actdesc,
                string currency, string curdesc, string csplname, string cspldesc)
            {
                this.actcode = actcode;
                this.actdesc1 = actdesc1;
                this.lcdate = lcdate;
                this.shipdate = shipdate;
                this.shipardate = shipardate;
                this.deldate = deldate;
                this.expdate = expdate;
                this.cnvrsion = cnvrsion;
                this.fcamt = fcamt;
                this.bdamt = bdamt;
                this.bankname = bankname;
                this.actdesc = actdesc;
                this.currency = currency;
                this.curdesc = curdesc;
                this.csplname = csplname;
                this.cspldesc = cspldesc;
            }

        }
        
        [Serializable]
        public class LCQCPrint
        {
            public string comcod { set; get; }
            public string rcvno { set; get; }
            public string actcode { set; get; }
            public string actdesc { set; get; }
            public string storid { set; get; }
            public string stordesc { set; get; }
            public DateTime rcvdate { set; get; }
            public DateTime expdate { set; get; }
            public string lotno { set; get; }
            public string chalanno { set; get; }
            public DateTime chalandate { set; get; }
            public string rescod { set; get; }
            public string resdesc { set; get; }
            public string spcfcod { set; get; }
            public string spcfdesc { set; get; }
            public string bomid { set; get; }
            public double rcvqty { set; get; }
            public double revamt { set; get; }
            public double rcvuptolast { set; get; }
            public double remainordr { set; get; }
            public double freeqty { set; get; }
            public double ordrqty { set; get; }
            public double trate { set; get; }
            public string unit { set; get; }
            public string ssircode { set; get; }
            public string ssirdesc { set; get; }
        }

        [Serializable]
        public class LCCostingPrint
        {
            public string comcod { set; get; }
            public string grrno { set; get; }
            public string rcvno { set; get; }
            public string actcode { set; get; }
            public string actdesc { set; get; }

            public string storid { set; get; }
            public string unit { set; get; }
            public string stordesc { set; get; }
            public DateTime rcvdate { set; get; }
            public DateTime expdate { set; get; }
            public string lotno { set; get; }
            public string chalanno { set; get; }
            public DateTime chalandate { set; get; }
            public string rescod { set; get; }
            public string resdesc { set; get; }
            public string spcfcod { set; get; }
            public string spcfdesc { set; get; }
            public string bomid { set; get; }
            public double rcvqty { set; get; }
            public double revamt { set; get; }
            public double qcqty { set; get; }
            public double preqcqty { set; get; }
            public double remqty { set; get; }
            public double trate { set; get; }
            public string remarks { set; get; }
            public string rackno { set; get; }
            public string location { set; get; }

        }

        [Serializable]
        public class RptMatInspctReport
        {
            public string comcod { get; set; }
            public string rcvno { get; set; }
            public double rcvqty { get; set; }
            public DateTime rcvdate { get; set; }
            public string nsupcode { get; set; }
            public string ssirdesc { get; set; }
            public string actcode { get; set; }
            public string storid { get; set; }
            public string lotno { get; set; }
            public DateTime expdate { get; set; }
            public DateTime posteddat { get; set; }
            public string chalanno { get; set; }
            public string reporttype { get; set; }
            public string rescod { get; set; }
            public string resdesc { get; set; }
            public string artcldesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfcolordesc { get; set; }
            public string spcfsizedesc { get; set; }
            public string bomid { get; set; }
            public string reqno { get; set; }
            public string syspon { get; set; }
            public string location { get; set; }
            public string status { get; set; }
            public double trate { get; set; }
            public double shipqty { get; set; }
            public double qcqty { get; set; }
            public double mrrqty { get; set; }
            public double passqty { get; set; }
            public double failqty { get; set; }
            public double rejectprcnt { get; set; }
            public string unit { get; set; }
            public string chckmethod { get; set; }
            public string fborderno { get; set; }
            public string remarks { get; set; }
            public string chckby { get; set; }
            public string passfail { get; set; }
            public string finding { get; set; }
            public string chckdetails { get; set; }
            public string brndcustmr { get; set; }


            //public string invno { get; set; }
            //public string itmname { get; set; }
            //public string articlename { get; set; }
            //public string size { get; set; }
            //public string color { get; set; }
            //public double qapass { get; set; }
            //public double rejctprcnt { get; set; }
            //public string problematic { get; set; }
            //public string rejection { get; set; }
            //public string passfail { get; set; }
            //public string failcause { get; set; }
            //public string rejctcause { get; set; }
            //public string comment { get; set; }
        }

        [Serializable]
        public class POShipLog
        {
            public string logno { set; get; }
            public string challanno { set; get; }
            public string sirdesc { set; get; }
            public string rsircode { set; get; }
            public string spcfdesc { set; get; }
            public string spcfcod { set; get; }
            public string sirunit { set; get; }
            public double shipqty { set; get; }
            public DateTime expecteddeldate { set; get; }
            public DateTime challandate { set; get; }

        }
    }
}

