using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SPEENTITY.C_19_Exp
{
   public class EClassReceiptaPayment
    {

        [Serializable]
        public class batchGrp
        {
            public string actcode { set; get; }
            public string actdesc { set; get; }

            public batchGrp(string actcode, string actdesc)
            {

                this.actcode = actcode;
                this.actdesc = actdesc;
            }
        }

        [Serializable]
        public class EClassBankaCash
        {
            public string bankcode { set; get; }
            public string bankdesc { set; get; }

            public EClassBankaCash(string bankcode, string bankdesc)
            {

                this.bankcode = bankcode;
                this.bankdesc = bankdesc;
            }
        }


        [Serializable]
        public class EClassDeborCreditor
        {
            public string rescode { set; get; }
            public string resdesc { set; get; }
            public string curcode { set; get; }

        
            public EClassDeborCreditor(string rescode, string resdesc, string curcode)
            {
                this.rescode = rescode;
                this.resdesc = resdesc;
                this.curcode = curcode;
                
            }
        }


        [Serializable]
        public class EClassDebtorBill
        {
            public string  actcode { set; get; }
            public string rescode { set; get; }
            public DateTime voudat { set; get; }
            public string isunum { set; get; }
            public string isunum1 { set; get; }
            public double billam { set; get; }
            public double hbalam { set; get; }
            public double balam { set; get; }
            public double receiptam { set; get; }
            public string chk { set; get; }
            public double allocamt { set; get; }
            public double vatamt { set; get; }
            public string curcod { set; get; }
            public double convrate { set; get; }
            public string curdesc { set; get; }
            public double bdtamount { set; get; }
            public double fcbnkcharge { set; get; }
            public double invbdtamt { set; get; }
            public double cglamt { set; get; }
            public string invrefno { set; get; }
            public double colconvrate { set; get; }
            public string adjflag { set; get; }
            public double fcadjamt { set; get; }
            public double adjamt { set; get; }
            public double aitamt { set; get; }
            public double commamt { set; get; }
            public double othcharge { set; get; }
            public double shortfallfc { set; get; }
            public double shortfallbdt { set; get; }
            public double ovrdueintrst { set; get; }
            public EClassDebtorBill() { }
            public EClassDebtorBill(string actcode, string rescode, DateTime voudat, string isunum, string isunum1, double billam, double hbalam,
                double balam, double receiptam, string chk, double allocamt, double vatamt, string curcod, double convrate, string curdesc,
                double bdtamount, double fcbnkcharge, double invbdtamt, double cglamt, string invrefno, double colconvrate, string adjflag,
                double fcadjamt, double adjamt, double aitamt, double commamt, double othcharge)
            {
                this.actcode = actcode;
                this.rescode = rescode;
                this.voudat = voudat;
                this.isunum = isunum;
                this.isunum1 = isunum1;
                this.billam = billam;
                this.hbalam = hbalam;
                this.balam = balam;
                this.receiptam = receiptam;
                this.chk = chk;
                this.allocamt = allocamt;
                this.vatamt = vatamt;
                this.curcod = curcod;
                this.convrate = convrate;
                this.curdesc = curdesc;
                this.bdtamount = bdtamount;
                this.fcbnkcharge = fcbnkcharge;
                this.invbdtamt = invbdtamt;
                this.cglamt = cglamt;
                this.invrefno = invrefno;
                this.colconvrate = colconvrate;
                this.adjflag = adjflag;
                this.fcadjamt = fcadjamt;
                this.adjamt = adjamt;
                this.aitamt = aitamt;
                this.commamt = commamt;
                this.othcharge = othcharge;
            
        }
        }



        [Serializable]
        public class EClassCreditorBill
        {
            public string actcode { set; get; }
            public string rescode { set; get; }
            public DateTime voudat { set; get; }
            public string gsttype { set; get; }
            public string isunum { set; get; }

            public string pactdesc { set; get; }
            public double billam { set; get; }
            public double hbalam { set; get; }
            public double balam { set; get; }
            public double receiptam { set; get; }
            public double sgd { set; get; }
            public string chk { set; get; }
            public string curdesc { set; get; }

            public EClassCreditorBill(string actcode, string rescode, DateTime voudat, string gsttype, string isunum, string pactdesc, double billam,
                double hbalam, double balam, double receiptam, double sgd, string chk, string curdesc)
            {
                this.actcode = actcode;
                this.rescode = rescode;
                this.voudat = voudat;
                this.gsttype = gsttype;
                this.isunum = isunum;
                this.pactdesc = pactdesc;
                this.billam = billam;
                this.hbalam = hbalam;
                this.balam = balam;
                this.receiptam = receiptam;
                this.sgd = sgd;
                this.chk = chk;
                this.curdesc = curdesc;
            }
        }

       [Serializable]

        //dr2 ["actcode"] = actcode;
        //    dr2 ["rescode"] = rescode;
        //    dr2 ["actdesc"] = this.ddlacccode.SelectedItem.ToString();
        //    dr2 ["resdesc1"] = resdesc1;
        //    dr2 ["fcamt"] = 0.00;
        //    dr2 ["bdtamt"] = 0.00;

       public class EClassBankData
       {
           public string actcode { get; set; }
           public string rescode { get; set; }
           public string actdesc { get; set; }
           public string resdesc1 { get; set; }
           public double fcamt { get; set; }
           public double bdtamt { get; set; }
           public double lgamt { get; set; }
           public double tamt { get; set; }

           public EClassBankData()
           {

           }

           public EClassBankData(string actcode, string rescode, string actdesc, string resdesc1, double fcamt, double dbtamt, double lgamt, double tamt)
           {
               this.actcode = actcode;
               this.rescode = rescode;
               this.actdesc = actdesc;
               this.resdesc1 = resdesc1;
               this.fcamt = fcamt;
               this.bdtamt = bdtamt;
               this.lgamt = lgamt;
               this.tamt = tamt;

           }

       }

    }
}
