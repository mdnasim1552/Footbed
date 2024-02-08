using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_21_Acc
{
    public class EClassAccVoucher
    {


        [Serializable]
        public class EClassGenReq
        {
            public string pactdesc { set; get; }
            public string reqno1 { set; get; }
            public DateTime reqdat { set; get; }
            public string mrfno { set; get; }
            public double proamt { set; get; }
            public double appamt { set; get; }

            public string aprname { set; get; }
            public string aprfname { set; get; }
            public string reqname { set; get; }
            public decimal payment { set; get; }


            public EClassGenReq() { }
        }

        [Serializable]
        public class EClassResHead
        {
            public string rescode { set; get; }
            public string resdesc { set; get; }
            public string resdesc1 { set; get; }
            public string resunit { set; get; }
            
            public EClassResHead(string rescode, string resdesc, string resdesc1, string resunit)
            {
                this.rescode = rescode;
                this.resdesc = resdesc;
                this.resdesc1 = resdesc1;
                this.resunit = resunit;

            }
        }




        [Serializable]
        public class EClassAccHead
        {
            public string actcode { set; get; }
            public string actdesc { set; get; }
            public string actdesc1 { set; get; }
            public string actelev { set; get; }
            public string acttype { set; get; }
            public EClassAccHead() { }

            public EClassAccHead(string actcode, string actdesc, string actdesc1, string actelev, string acttype)
            {
                this.actcode = actcode;
                this.actdesc = actdesc;
                this.actdesc1 = actdesc1;
                this.actelev = actelev;
                this.acttype = acttype;

            }
        }




        [Serializable]
        public class EClassVoucher
        {
            public string vounum { set; get; }
            public string vounum1 { set; get; }


            public EClassVoucher(string vounum, string vounum1)
            {
                this.vounum = vounum;
                this.vounum1 = vounum1;


            }
        }

        [Serializable]
        public class Shequity
        {
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public double opnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closam { get; set; }
            public Shequity() { }
        }

        [Serializable]
        public class EclassAccControlSchdule // by Safiul alam
        {
            public string comcod { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public double opndram { get; set; }
            public double opncram { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closdram { get; set; }
            public double closcram { get; set; }
            public double closam { get; set; }
            public string clremks { get; set; }
        }

        [Serializable]
        public class AccTrialBl1
        {
            //        comcod,  actcode1, actdesc1, actcode2, actdesc2, actcode3, actdesc3, actcode4, actdesc4,  opnam,  
            //opndram=(case when opnam>0 then opnam else 0.00 end), opncram=(case when opnam<0 then opnam*-1 else 0.00 end), dram, cram, closdram, closcram,  
            //curam = trnam, closam=abs(closam), netdram=(case when closam>0 then  closam else 0.00 end), netcram=(case when closam>0 then 0.00   else closam*-1 end), 
            //mainhead, leb2, drcr

            public string comcod { get; set; }
            public string actcode1 { get; set; }
            public string actdesc1 { get; set; }
            public string actcode2 { get; set; }
            public string actdesc2 { get; set; }
            public string actcode3 { get; set; }
            public string actdesc3 { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public Decimal opnam { get; set; }
            public Decimal opndram { get; set; }
            public Decimal opncram { get; set; }
            public Decimal dram { get; set; }
            public Decimal cram { get; set; }
            public Decimal closdram { get; set; }
            public Decimal closcram { get; set; }
            public Decimal curam { get; set; }
            public Decimal closam { get; set; }
            public Decimal netdram { get; set; }
            public Decimal netcram { get; set; }
            public string mainhead { get; set; }
            public string leb2 { get; set; }
            public string drcr { get; set; }
            public AccTrialBl1() { }
        }

        [Serializable]
        public class DtlOfBalanceSheet1
        {
            public string comcod { get; set; }
            public string actcode4 { get; set; }
            public string rescode4 { get; set; }
            public string rescode { get; set; }
            public Decimal opnam { get; set; }
            public Decimal trnam { get; set; }
            public Decimal curam { get; set; }
            public Decimal dram { get; set; }
            public Decimal cram { get; set; }
            public Decimal closam { get; set; }
            public string actdesc4 { get; set; }
            public string resdesc { get; set; }
            public DtlOfBalanceSheet1() { }
        }

        [Serializable]
        public class MLCCost
        {
            public string comcod { get; set; }
            public string mgrp { get; set; }
            public string mgrpdesc { get; set; }
            public string grp { get; set; }
            public string actcode { get; set; }
            public string rescode { get; set; }
            public string grpdesc { get; set; }
            public double itmamt { get; set; }
            public double toamt { get; set; }
            public string actdesc { get; set; }
            public string resdesc { get; set; }
            public string resunit { get; set; }
            public double balamt { get; set; }
            public double variance { get; set; }
            public MLCCost() { }
        }

         [Serializable]
        public class EclassPrintVoucherBr
        {
            public string vounum { get; set; }
            public string mactcode { get; set; }
            public string grp1 { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string trnrmrk { get; set; }
            public string unit { get; set; }
            public string isunum { get; set; }
            public double trnqty { get; set; }
            public double trnrat { get; set; }
            public double Dr { get; set; }
            public double Cr { get; set; }
            public string billno { get; set; }
            public EclassPrintVoucherBr()
            {

            }
        }

         [Serializable]

         public class EClassCashFlowD
         {

             public string comcod { get; set; }
             public string actcode { get; set; }
             public string grpdesc { get; set; }
             public string grp { get; set; }
             public string actdesc { get; set; }
             public string acgcode { get; set; }
             public double bopnam { get; set; }
             public double opnam { get; set; }
             public double dram { get; set; }
             public double cram { get; set; }
             public double curam { get; set; }
             public double closam { get; set; }
             public double changeam { get; set; }

             public EClassCashFlowD() { }
             public EClassCashFlowD(string comcod, string actcode, string grpdesc, string grp, string actdesc, string acgcode, double bopnam,
                 double opnam, double dram, double cram, double curam, double closam, double changeam)
             {
                 this.comcod = comcod;
                 this.actcode = actcode;
                 this.acgcode = acgcode;
                 this.bopnam = bopnam;
                 this.opnam = opnam;
                 this.dram = dram;
                 this.cram = cram;
                 this.curam = curam;
                 this.closam = closam;
                 this.changeam = changeam;
                 this.grpdesc = grpdesc;
                 this.grp = grp;
                 this.actdesc = actdesc;

             }

         }


         [Serializable]


         public class AccLeadger
         {
             public string grp { get; set; }
             public string grpdesc { get; set; }
             public string comcod { get; set; }
             public string actcode { get; set; }
             public string head1 { get; set; }
             public DateTime voudat { get; set; }
             public string voudat1 { get; set; }
             public string vounum { get; set; }
             public string vounum1 { get; set; }
             public string cactcode { get; set; }
             public string cactdesc { get; set; }
             public string rescode { get; set; }
             public string resdesc { get; set; }
             public double trnam { get; set; }
             public double trnqty { get; set; }
             public double trnrate { get; set; }
             public double dram { get; set; }
             public double cram { get; set; }
             public double balamt { get; set; }
             public string trnrmrk { get; set; }
             public string refnum { get; set; }
             public string srinfo { get; set; }
             public string venar1 { get; set; }
             public string venar2 { get; set; }
             public string username { get; set; }
             public AccLeadger() { }
             public AccLeadger(string grp, string grpdesc, string comcod, string actcode, string head1, DateTime voudat,
                 string voudat1, string vounum, string vounum1, string cactcode, string cactdesc, string rescode,
                 double trnam, double trnqty, double trnrate, double dram, double cram, double balamt, string trnrmrk,
                 string refnum, string srinfo, string venar1, string venar2, string username)
             {
                 this.grp = grp;
                 this.grpdesc = grpdesc;
                 this.comcod = comcod;
                 this.actcode = actcode;
                 this.head1 = head1;
                 this.voudat = voudat;
                 this.voudat1 = voudat1;
                 this.vounum = vounum;
                 this.vounum1 = vounum1;
                 this.cactcode = cactcode;
                 this.cactdesc = cactdesc;
                 this.rescode = rescode;
                 this.trnam = trnam;
                 this.trnqty = trnqty;
                 this.trnrate = trnrate;
                 this.dram = dram;
                 this.cram = cram;
                 this.balamt = balamt;
                 this.trnrmrk = trnrmrk;
                 this.refnum = refnum;
                 this.srinfo = srinfo;
                 this.venar1 = venar1;
                 this.venar2 = venar2;
                 this.username = username;






             }




         }




         [Serializable]

         public class AccDTransaction
         {
             public string comcod { get; set; }
             public string vounum { get; set; }
             public string vounum1 { get; set; }
             public string rescode { get; set; }
             public DateTime voudat { get; set; }
             public string voudat1 { get; set; }
             public double dram { get; set; }
             public double cram { get; set; }
             public string refnum { get; set; }
             public string srinfo { get; set; }
             public string voutype { get; set; }
             public string payto { get; set; }
             public string resdesc { get; set; }

             public AccDTransaction() { }
         }

         [Serializable]

         public class AccDTransactrtionAll
         {
             public int rowid { get; set; }
             public string comcod { get; set; }
             public DateTime voudat { get; set; }
             public string voudat1 { get; set; }
             public string vounum1 { get; set; }
             public string vounum { get; set; }
             public string acrescode { get; set; }
             public string acresdesc { get; set; }
             public decimal trnqty { get; set; }
             public decimal inneram { get; set; }
             public decimal dram { get; set; }
             public decimal cram { get; set; }
             public string trnrmrk { get; set; }
             public string refnum { get; set; }
             public string srinfo { get; set; }
             public string venarr { get; set; }
             public string voutyp { get; set; }
             public string spcldesc { get; set; }
             public string refvno { get; set; }
             public string comsnam { get; set; }
             public string postedbydesc { get; set; }
             public string drcr { get; set; }
             public string actcode { get; set; }
             public string rescode { get; set; }
             public string spclcode { get; set; }
             public string payto { get; set; }

             public AccDTransactrtionAll() { }
         }
    }
}