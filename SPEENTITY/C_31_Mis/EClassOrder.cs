using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_31_Mis
{
   public class EClassOrder
    {

        #region OrderDash_Board
        [Serializable]
        public class EClassYear
        {
            public string yearid { set; get; }

            public double ordramt { set; get; }
            public double shipamt { set; get; }

            public EClassYear(string yearid, double ordramt, double shipamt)
            {
                this.yearid = yearid;
                this.ordramt = ordramt;
                this.shipamt = shipamt;
            }
        }
        [Serializable]
        public class EClassMonthly
        {
            public string yearmon { set; get; }

            public string yearmon1 { set; get; }

            public double ordramt { set; get; }
            public double shipamt { set; get; }

            public double balshipamt { set; get; }
            public double collamt { set; get; }
            public double balcollamt { set; get; }

            public EClassMonthly(string yearmon, string yearmon1, double ordramt, double shipamt, double balshipamt, double collamt, double balcollamt)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.ordramt = ordramt;
                this.shipamt = shipamt;
                this.balshipamt = balshipamt;
                this.collamt = collamt;
                this.balcollamt = balcollamt;
            }
        }
        [Serializable]
        public class EClassWeekly
        {
            public string wcode1 { set; get; }
            public double woamt1 { set; get; }
            public double wsamt1 { set; get; }

            public string wcode2 { set; get; }
            public double woamt2 { set; get; }
            public double wsamt2 { set; get; }

            public string wcode3 { set; get; }
            public double woamt3 { set; get; }
            public double wsamt3 { set; get; }

            public string wcode4 { set; get; }
            public double woamt4 { set; get; }
            public double wsamt4 { set; get; }

            public EClassWeekly(string wcode1, double woamt1, double wsamt1, string wcode2, double woamt2, double wsamt2, string wcode3, double woamt3, double wsamt3,
                    string wcode4, double woamt4, double wsamt4)
            {
                this.wcode1 = wcode1;
                this.woamt1 = woamt1;
                this.wsamt1 = wsamt1;
                this.wcode2 = wcode2;
                this.woamt2 = woamt2;
                this.wsamt2 = wsamt2;
               
                this.wcode3 = wcode3;
                this.woamt3 = woamt3;
                this.wsamt3 = wsamt3;
               
                this.wcode4 = wcode4;
                this.woamt4 = wsamt4;
                this.wsamt4 = wsamt4;
            
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

            public EClassDayWise(string centrid, string centrdesc, string custid, string custdesc, string memono1, string memono, string memodat, string vounum1, string vounum,
                    double itmamt, double vat, double invdis)
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

        #endregion

        [Serializable]
        public class EclassBalSheetSum
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public double noncuram { get; set; }
            public double curam { get; set; }
            public double equityam { get; set; }
            public double noncurlia { get; set; }
            public double curlia { get; set; }
            public double toasset { get; set; }
            public double tolib { get; set; }
        }
    }
}
