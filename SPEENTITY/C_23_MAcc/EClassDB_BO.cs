using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_23_MAcc
{
    public class EClassDB_BO
    {
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
            public EClassAccMonthly(string yearmon, string yearmon1, double dram, double cram)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.dram = dram;
                this.cram = cram;
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
        public class EClassLPROYearly
        {
            public string yearid { set; get; }
            public double puram { set; get; }
            public double paymntam { set; get; }
            public EClassLPROYearly(string yearid, double puram, double paymntam)
            {
                this.yearid = yearid;
                this.puram = puram;
                this.paymntam = paymntam;
            }
        }
         [Serializable]
        public class EClassPROMonthly
        {
            public string yearmon { set; get; }
            public string yearmon1 { set; get; }
            public double puram { set; get; }
            public double paymntam { set; get; }
            public EClassPROMonthly(string yearmon, string yearmon1, double puram, double paymntam)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.puram = puram;
                this.paymntam = paymntam;
            }
        }

         [Serializable]
        public class EClassPROWeekly
        {
            public string wcode1 { set; get; }
            public double wpamt1 { set; get; }
            public double wpayamt1 { set; get; }

            public string wcode2 { set; get; }
            public double wpamt2 { set; get; }
            public double wpayamt2 { set; get; }

            public string wcode3 { set; get; }
            public double wpamt3 { set; get; }
            public double wpayamt3 { set; get; }

            public string wcode4 { set; get; }
            public double wpamt4 { set; get; }
            public double wpayamt4 { set; get; }


            public EClassPROWeekly(string wcode1, double wpamt1, double wpayamt1, string wcode2, double wpamt2, double wpayamt2, string wcode3, double wpamt3, double wpayamt3,
                    string wcode4, double wpamt4, double wpayamt4)
            {
                this.wcode1 = wcode1;
                this.wpamt1 = wpamt1;
                this.wpayamt1 = wpayamt1;
                this.wcode2 = wcode2;
                this.wpamt2 = wpamt2;
                this.wpayamt2 = wpayamt2;
                this.wcode3 = wcode3;
                this.wpamt3 = wpamt3;
                this.wpayamt3 = wpayamt3;
                this.wcode4 = wcode4;
                this.wpamt4 = wpamt4;
                this.wpayamt4 = wpayamt4;
                
            }
        }
    }
}
