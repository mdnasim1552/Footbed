using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SPEENTITY.C_32_Mis
{
    public class EClassSales_02
    {
     

        #region SalesDash_Board
      
     
     
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

        [Serializable]
        public class IncomStatment12
        {
            public string comcod { get; set; }
            public string grpcode { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string expensesdesc { get; set; }
            public double toamt { get; set; }
            public double amt1 { get; set; }
            public double amt2 { get; set; }
            public double amt3 { get; set; }
            public double amt4 { get; set; }
            public double amt5 { get; set; }
            public double amt6 { get; set; }
            public double amt7 { get; set; }
            public double amt8 { get; set; }
            public double amt9 { get; set; }
            public double amt10 { get; set; }
            public double amt11 { get; set; }
            public double amt12 { get; set; }
            public IncomStatment12() { }
        }

#endregion

    }
}
