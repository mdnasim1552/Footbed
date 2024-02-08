using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SPEENTITY.C_22_Sal
{
    public class EClassSales_02
    {
        [Serializable]
        public class EClassVaT
        {
            public string centrid { set; get; }
            public string centrdesc { set; get; }

            public string custid { set; get; }
            public string custdesc { set; get; }

            public string memono1 { set; get; }
            public string vounum1 { set; get; }

            public string memodat { set; get; }
            public double itmamt { set; get; }
            public double vatper { set; get; }
            public double invdisper { set; get; }
            public double vat { set; get; }

            public double invdis { set; get; }
            public EClassVaT(string centrid, string centrdesc, string custid, string custdesc, string memono1, string vounum1, string memodat, double itmamt, double vatper, double vat, double invdisper, double invdis)
            {
                this.centrid = centrid;
                this.centrdesc = centrdesc;
                this.custid = custid;
                this.custdesc = custdesc;
                this.memono1 = memono1;
                this.vounum1 = vounum1;
                this.memodat = memodat;
                this.itmamt = itmamt;
                this.vat = vat;
                this.vatper = vatper;
               
                this.invdis = invdis;
                this.invdisper = invdisper;
            }
        }

        #region SalesDash_Board
        [Serializable]
        public class EClassYear
        {
            public string yearid { set; get; }
            public double samt { set; get; }
            public double collamt { set; get; }
            public double balance { set; get; }
           
            public EClassYear(string yearid, double samt, double collamt,double balance)
            {
                this.yearid = yearid;
                this.samt = samt;
                this.collamt = collamt;
                this.balance = balance;
            }
        }
        [Serializable]
        public class EClassMonthly
        {
            public string yearmon { set; get; }

            public string yearmon1 { set; get; }

            public double ttlsalamt { set; get; }
            public double collamt { set; get; }
            public double bal { set; get; }
            
            
            public double targtsaleamt { set; get; }
            public double tarcollamt { set; get; }

            public double targtsaleamtcore { set; get; }
            public double tarcollamtcore { set; get; }


            public double ttlsalamtcore { set; get; }
            public double collamtcrore { set; get; }





            public EClassMonthly(string yearmon, string yearmon1, double ttlsalamt, double collamt, double bal, double targtsaleamt, double tarcollamt, double targtsaleamtcore, double tarcollamtcore, double ttlsalamtcore, double collamtcrore)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.ttlsalamt = ttlsalamt;
                this.collamt = collamt;
                this.bal = bal;

                this.targtsaleamt = targtsaleamt;
                this.tarcollamt = tarcollamt;
                this.targtsaleamtcore = targtsaleamtcore;
                this.tarcollamtcore = tarcollamtcore;


                this.ttlsalamtcore = ttlsalamtcore;
                this.collamtcrore = collamtcrore;
            }
        }
        [Serializable]
        public class EClassWeekly
        {
            public string wcode1 { set; get; }
            public double wsamt1 { set; get; }
            public double wcamt1 { set; get; }

            public string wcode2 { set; get; }
            public double wsamt2 { set; get; }
            public double wcamt2 { set; get; }

            public string wcode3 { set; get; }
            public double wsamt3 { set; get; }
            public double wcamt3 { set; get; }

            public string wcode4 { set; get; }
            public double wsamt4 { set; get; }
            public double wcamt4 { set; get; }
            public double wbal1 { set; get; }
            public double wbal2 { set; get; }
            public double wbal3 { set; get; }
            public double wbal4 { set; get; }
           
            public EClassWeekly(string wcode1, double wsamt1, double wcamt1, double wbal1, string wcode2, double wsamt2, double wcamt2, double wbal2, string wcode3, double wsamt3, double wcamt3,
                   double wbal3, string wcode4, double wsamt4, double wcamt4,double wbal4)
            {
                this.wcode1 = wcode1;
                this.wsamt1 = wsamt1;
                this.wcamt1 = wcamt1;
                this.wcode2 = wcode2;
                this.wsamt2 = wsamt2;
                this.wcamt2 = wcamt2;
                this.wcode3 = wcode3;
                this.wsamt3 = wsamt3;
                this.wcamt3 = wcamt3;
                this.wcode4 = wcode4;
                this.wsamt4 = wsamt4;
                this.wcamt4 = wcamt4;
                this.wbal1 = wbal1;
                this.wbal2 = wbal2;
                this.wbal3 = wbal3;
                this.wbal4 = wbal4;
            }
        }

        [Serializable]
        public class EClassDayWise
        {
            public string centrid { set; get; }
            public string centrdesc { set; get; }
            public string custid { set; get; }
            public string custdesc { set; get; }
            public string memono1 { set; get; }
            
            public string memono { set; get; }
            public string memodat { set; get; }
            public string vounum1 { set; get; }
            public string vounum { set; get; }
            public double itmamt { set; get; }
            public double vat { set; get; }
            public double invdis { set; get; }
            public double collamt { set; get; }
            public double netamt { set; get; }


            public EClassDayWise(string centrid, string centrdesc, string custid, string custdesc, string memono1, string memono, string memodat, string vounum1, string vounum,
                    double itmamt, double vat, double invdis, double collamt, double netamt)
            {
                this.centrid = centrid;
                this.centrdesc = centrdesc;
                this.custid = custid;
                this.custdesc = custdesc;
                this.memono1 = memono1;
                this.memono = memono;
                this.memodat = memodat;
                this.vounum1 = vounum1;
                this.vounum = vounum;
                this.itmamt = itmamt;
                this.vat = vat;
                this.invdis = invdis;
                this.collamt = collamt;
                this.netamt = netamt;
            }
        }

        [Serializable]
        public class EClassDayWiseColl
        {
            public string centrid { set; get; }
            public string centrdesc { set; get; }
            public string custid { set; get; }
            public string custdesc { set; get; }
            public string mrslno1 { set; get; }

            public string mrslno { set; get; }
            public string mrdat { set; get; }
            public string vounum1 { set; get; }
            public string vounum { set; get; }
            public double amount { set; get; }

            public EClassDayWiseColl(string centrid, string centrdesc, string custid, string custdesc, string mrslno1, string mrslno, string mrdat, string vounum1,
                string vounum, double amount)
            {
                this.centrid = centrid;
                this.centrdesc = centrdesc;
                this.custid = custid;
                this.custdesc = custdesc;
                this.mrslno1 = mrslno1;
                this.mrslno = mrslno;
                this.mrdat = mrdat;
                this.vounum1 = vounum1;
                this.vounum = vounum;
                this.amount = amount;
            }
        }

        [Serializable]
        public class Rptsalescollection
        {
            public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassYear> LstEClassYear { get; set; }
            public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassMonthly> LstEClassMonthly { get; set; }
            public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassWeekly> LstEClassWeekly { get; set; }
            public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWise> LstEClassDayWise { get; set; }
            public List<SPEENTITY.C_22_Sal.EClassSales_02.EClassDayWiseColl> LstEClassDayWiseColl { get; set; }
        }

#endregion



    }
}
