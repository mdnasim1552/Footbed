using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_21_Acc
{
    public class EClassDB_BO
    {
        [Serializable]
        public class OnlineBillReg
        {
            public string comcod { get; set; }
            public string slnum { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string billno { get; set; }
            public string billno1 { get; set; }
            public DateTime chqdate { get; set; }
            public double amount { get; set; }
            public string bankcode { get; set; }
            public DateTime payapdat { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public DateTime fdate { get; set; }
            public string resdesc { get; set; }
            public string actdesc { get; set; }
            public string bankname { get; set; }
            public string checqno { get; set; }
            public OnlineBillReg() { }
        }

        [Serializable]
        public class EClassAccYearly
        {
            public string yearid { set; get; }
            public double dram { set; get; }
            public double cram { set; get; }
            public EClassAccYearly(string yearid, double dram, double cram)
            {
                this.yearid = yearid;
                this.dram = dram;
                this.cram = cram;
            }
        }
        [Serializable]
        public class EClassAccMonthly
        {
            public string yearmon { set; get; }
            public string yearmon1 { set; get; }
            public double dram { set; get; }
            public double cram { set; get; }
            public double dramcore { set; get; }
            public double cramcore { set; get; }
            public EClassAccMonthly(string yearmon, string yearmon1, double dram, double cram, double dramcore, double cramcore)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.dram = dram;
                this.cram = cram;
                this.dramcore = dramcore;
                this.cramcore = cramcore;

            }
        }
        [Serializable]
        public class EClassAccWeekly
        {
            public string wcode1 { set; get; }
            public double wpamt1 { set; get; }
            public double wramt1 { set; get; }

            public string wcode2 { set; get; }
            public double wpamt2 { set; get; }
            public double wramt2 { set; get; }

            public string wcode3 { set; get; }
            public double wpamt3 { set; get; }
            public double wramt3 { set; get; }

            public string wcode4 { set; get; }
            public double wpamt4 { set; get; }
            public double wramt4 { set; get; }

            public double brec { set; get; }
            public double bpay { set; get; }

            public EClassAccWeekly(string wcode1, double wpamt1, double wramt1, string wcode2, double wpamt2, double wramt2, string wcode3, double wpamt3, double wramt3,
                    string wcode4, double wpamt4, double wramt4, double brec, double bpay)
            {
                this.wcode1 = wcode1;
                this.wpamt1 = wpamt1;
                this.wramt1 = wramt1;
                this.wcode2 = wcode2;
                this.wpamt2 = wpamt2;
                this.wramt2 = wramt2;
                this.wcode3 = wcode3;
                this.wpamt3 = wpamt3;
                this.wramt3 = wramt3;
                this.wcode4 = wcode4;
                this.wpamt4 = wpamt4;
                this.wramt4 = wramt4;
                this.brec = brec;
                this.bpay = bpay;
            }
        }
        [Serializable]
        public class AccRatioAnalysis ///grp, grpdesc , rcode, rdesc, rfourm, ratio, rstd, inter
        {
            public string comcode { get; set; }
            public string rcode { get; set; }
            public string rcode2 { get; set; }
            public string rcode3 { get; set; }
            public string rdesc { get; set; }
            public string rfourm { get; set; }
            public decimal rstd { get; set; }
            public decimal ratio { get; set; }
            public string grpdesc { get; set; }
            public string inter { get; set; }
            public string grp { get; set; }
        }

        [Serializable]
        public class CashBankStatement
        {
            public string grp { get; set; }
            public string grp1 { get; set; }
            public string recndt1 { get; set; }
            public string vounum1 { get; set; }
            public string voudat1 { get; set; }
            public string refnum { get; set; }
            public string chequedat1 { get; set; }
            public string actdesc { get; set; }
            public string resdesc { get; set; }
            public string payto { get; set; }
            public string vounar { get; set; }
            public double depam { get; set; }
            public double payam { get; set; }
            public double balamt { get; set; }
        }

       
    }
}
