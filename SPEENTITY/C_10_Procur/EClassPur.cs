using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_10_Procur
{
    public class EClassPur
    {
        //------------BO---

        #region purchase Dashboard
        [Serializable]
        public class EClassYear
        {
            public string yearid { set; get;}
            public double ttlamt { set; get; }

            public double purpay { set; get; }

            public EClassYear(string yearid, double ttlamt, double purpay)
            {
                this.yearid = yearid;
                this.ttlamt = ttlamt;
                this.purpay = purpay;
            }
        }
        [Serializable]
        public class EClassMonthly
        {
            public string yearmon { set; get; }
            public string yearmon1 { set; get; }
            public double ttlsalamt { set; get; }
            public double tpayamt { set; get; }
            public double ttlsalamtcore { set; get; }
            public double tpayamtcore { set; get; }
            public EClassMonthly() { }
            public EClassMonthly(string yearmon, string yearmon1, double ttlsalamt, double tpayamt, double ttlsalamtcore, double tpayamtcore)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.ttlsalamt = ttlsalamt;
                this.tpayamt = tpayamt;
                this.ttlsalamtcore = ttlsalamtcore;
                this.tpayamtcore = tpayamtcore;

            }

        }
        [Serializable]
        public class EClassWeekly
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

            public EClassWeekly(string wcode1, double wpamt1, double wpayamt1, string wcode2, double wpamt2, double wpayamt2, string wcode3, double wpamt3, double wpayamt3,
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

        [Serializable]
        public class EClassDayWisePur
        {
            public string pactcode { set; get; }
            public string pactdesc { set; get; }
            public string rsircode { set; get; }
            public string rsirdesc { set; get; }

            public string ssircode { set; get; }
            public string ssirdesc { set; get; }
            public string billno1 { set; get; }

            public string billno { set; get; }
            public string billdate1 { set; get; }
            public string vounum1 { set; get; }
            public string vounum { set; get; }
            public double billamt { set; get; }

            public EClassDayWisePur(string pactcode, string pactdesc, string rsircode, string rsirdesc, string ssircode, string ssirdesc, string billno1, string billno,
                    string billdate1, string vounum1, string vounum, double billamt)
            {
                this.pactcode = pactcode;
                this.pactdesc = pactdesc;
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.ssircode = ssircode;
                this.ssirdesc = ssirdesc;
                this.billno1 = billno1;
                this.billno = billno;
                this.billdate1 = billdate1;
                this.vounum1 = vounum1;
                this.vounum = vounum;
                this.billamt = billamt;
            }
        }

        [Serializable]
        public class EClassDayWisePay
        {
            public string pactcode { set; get; }
            public string pactdesc { set; get; }
            public string cactcode { set; get; }
            public string cactdesc { set; get; }

            public string ssircode { set; get; }
            public string ssirdesc { set; get; }
            public string billno1 { set; get; }

            public string billno { set; get; }
            public string voudat { set; get; }
            public string vounum1 { set; get; }
            public string vounum { set; get; }
            public double payamt { set; get; }

            public EClassDayWisePay(string pactcode, string pactdesc, string cactcode, string cactdesc, string ssircode, string ssirdesc, string billno1, string billno,
                    string voudat, string vounum1, string vounum, double payamt)
            {
                this.pactcode = pactcode;
                this.pactdesc = pactdesc;
                this.cactcode = cactcode;
                this.cactdesc = cactdesc;
                this.ssircode = ssircode;
                this.ssirdesc = ssirdesc;
                this.billno1 = billno1;
                this.billno = billno;
                this.voudat = voudat;
                this.vounum1 = vounum1;
                this.vounum = vounum;
                this.payamt = payamt;
            }
        }
        [Serializable]
        public class ComparativeStatementCreate
        {
            public string comcod { get; set; }
            public string ssircode { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public decimal rate { get; set; }
            public decimal stkqty { get; set; }
            public decimal areqty { get; set; }
            public decimal propqty { get; set; }
            public decimal csreqqty { get; set; }
            public decimal propqty1 { get; set; }
            public decimal amount { get; set; }
            public string supdesc { get; set; }
            public string rsirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public decimal istpurate { get; set; }
            public string payment { get; set; }
            public string cperson { get; set; }
            public string mobile { get; set; }
            public decimal leadtime { get; set; }
            public decimal auditrate { get; set; }
            public string paytype { get; set; }
            public decimal advamt { get; set; }
          
        }

        [Serializable]
        public class reqadjstmntlist
        {
            public string comcod { get; set; }
            public string reqno { get; set; }
            public string reqdat { get; set; }
            public string mrfno { get; set; }
            public double preqty { get; set; }
            public double billqty { get; set; }
            public double balqty { get; set; }

            public string ack { get; set; }
        }







        #endregion

        [Serializable]
        public class SuppOutStndStatmnt
        {
            public string comcod { get; set; }
            public string rescode { get; set; }
            public double opnam { get; set; }
            public double trnam { get; set; }
            public double trnqty { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double purret { get; set; }
            public double netpur { get; set; }
            public double balamt { get; set; }
            public string resdesc { get; set; }

          
        }
        [Serializable]
        public class SisterCompList
        {
            public string  comcod { get; set; }
            public string comname { get; set; }
            public string company { get; set; }
            public string mcomcod { get; set; }
            public string comadd { get; set; }
            public string scomcod { get; set; }

        }
        [Serializable]
        public class EClassPurchaseOrdr
        {
            
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string reqno { get; set; }
            public string mrfno { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string rsirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string spdesc { get; set; }
            public string rsirunit { get; set; }
            public double ordqty { get; set; }
            public double ordrqty { get; set; }
            public double aprovrate { get; set; }
            public double ordramt { get; set; }
            public double ordamt { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string proadd { get; set; }



        }
        [Serializable]
        public class PurchaseOrderTerms
        {
            public string comcod { get; set; }
            public string orderno { get; set; }
            public string termssubj { get; set; }
            public string termsdesc { get; set; }
            public string termsmrk { get; set; }
        }

        [Serializable]
        public class PromMatHistory
        {
            public string comcod { get; set; }
            public string issueno { get; set; }
            public string actcode { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string unit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string deptcode { get; set; }//
            public string deptname { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string idcard { get; set; }
            public string terricode { get; set; }
            public string territory { get; set; }
            public DateTime issuedat { get; set; }
            public double issueqty { get; set; }
            public double issueamt { get; set; }
            public double rate { get; set; }
            public string vounum { get; set; }
            public string narr { get; set; }
            public string apstatus { get; set; }

        }
        [Serializable]
        public class EclassProwisePurchase
        {
           
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string actcode { get; set; }
            public string r1 { get; set; }
            public string r2 { get; set; }
            public string r3 { get; set; }
            public string r4 { get; set; }
            public string r5 { get; set; }
            public string r6 { get; set; }
            public string r7 { get; set; }
            public string r8 { get; set; }
            public string r9 { get; set; }
            public string r10 { get; set; }
            public double tqty { get; set; }
            public double trate { get; set; }
            public double tamt { get; set; }
            public double q1 { get; set; }
            public double q2 { get; set; }
            public double q3 { get; set; }
            public double q4 { get; set; }
            public double q5 { get; set; }
            public double q6 { get; set; }
            public double q7 { get; set; }
            public double q8 { get; set; }
            public double q9 { get; set; }
            public double q10 { get; set; }
            public double a1 { get; set; }
            public double a2 { get; set; }
            public double a3 { get; set; }
            public double a4 { get; set; }
            public double a5 { get; set; }
            public double a6 { get; set; }
            public double a7 { get; set; }
            public double a8 { get; set; }
            public double a9 { get; set; }
            public double a10 { get; set; }
            public double u1 { get; set; }
            public double u2 { get; set; }
            public double u3 { get; set; }
            public double u4 { get; set; }
            public double u5 { get; set; }
            public double u6 { get; set; }
            public double u7 { get; set; }
            public double u8 { get; set; }
            public double u9 { get; set; }
            public double u10 { get; set; }
            
             }

        public class RptBillAproved01
        {
            public string comcod { set; get; }
            public string actcode { set; get; }
            public string slnum { set; get; }
            public string billno { set; get; }
            public string reqno { set; get; }
            public string valdate { get; set; }
            public string mslnum1 { set; get; }
            public string usrdesig { set; get; }
            public string actdesc { set; get; }
            public string actdesc2 { set; get; }
            public string rescount { set; get; }
            public string apdate { set; get; }
            public string billno2 { set; get; }
            public string useridapp { set; get; }
            public double apamt { set; get; }
            public double amt { set; get; }
            public double advamt { set; get; }
            public double netamt { get; set; }
            public string refno { set; get; }
            public string revdate { set; get; }
            public string apppaydate { set; get; }
            public string rec { set; get; }
            public string approved { set; get; }
            public string forward { set; get; }
            public string nochq { set; get; }
            public string issued { set; get; }
            public double issuedamt { set; get; }
            public string issn { get; set; }
            public string appisedate { set; get; }
            public string confirm { set; get; }
            public string checqno { set; get; }
            public string bankinf { set; get; }
            public string checqdt { set; get; }
            public string preparedid { set; get; }
            public string recomid { set; get; }
            public string appovedid { set; get; }
            public RptBillAproved01() { }
        }

        [Serializable]
        public class EclassPurReturn
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

            public EclassPurReturn() { }

            public EclassPurReturn(string rsircode, string spcfcod, double itmqty, double rate, double itmamt, string rsirdesc,
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
        public class EClassShowInvoice
        {

            public string memono { set; get; }
            public string memono1 { set; get; }

            public string sdelno { set; get; }
            public string sdelno1 { set; get; }
            public string centrid { set; get; }
            public string rsircode { set; get; }
            public string rsirdesc { set; get; }
            public string batchcode { set; get; }
            public string batchdesc { set; get; }
            public string rsirunit { set; get; }
            public string custcode { set; get; }
            public double invqty { set; get; }
            public double retqty { set; get; }
            public double invrate { set; get; }
            public double invamt { set; get; }
            public double retamt { set; get; }
            public double invdis { set; get; }
            public double retindis { set; get; }
            public double invvat { set; get; }
            public double retvat { set; get; }
            public double ovdis { set; get; }
            public double retovdis { set; get; }
            public string remarks { set; get; }
            public string teamcode { set; get; }
            public string taxcode { set; get; }
            public double freeqty { set; get; }
            public double retfreeqty { set; get; }
            public double repackamt { set; get; }
            public double caramt { set; get; }

            public double finaldis { set; get; }
            public EClassShowInvoice()
            { }

            public EClassShowInvoice(string memono, string memono1, string sdelno, string sdelno1, string centrid, string rsircode, string rsirdesc, string batchcode, string batchdesc, string rsirunit,
                    string custcode, double invqty, double retqty, double invrate, double invamt, double retamt, double invdis, double retindis,
                    double invvat, double retvat, double ovdis, double retovdis, string remarks, string teamcode, string taxcode, double freeqty, double retfreeqty, double repackamt, double caramt)
            {
                this.memono = memono;
                this.memono1 = memono1;
                this.sdelno = sdelno;
                this.sdelno1 = sdelno1;
                this.centrid = centrid;
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.rsirunit = rsirunit;
                this.custcode = custcode;
                this.invqty = invqty;
                this.retqty = retqty;
                this.invrate = invrate;
                this.invamt = invamt;
                this.retamt = retamt;
                this.invdis = invdis;
                this.retindis = retindis;
                this.invvat = invvat;
                this.retvat = retvat;
                this.ovdis = ovdis;
                this.retovdis = retovdis;
                this.remarks = remarks;
                this.teamcode = teamcode;
                this.taxcode = taxcode;
                this.freeqty = freeqty;
                this.retfreeqty = retfreeqty;
                this.repackamt = repackamt;
                this.caramt = caramt;

            }
        }

        [Serializable]
        public class PendingProdutionList
        {
            public string comcod { get; set; }
            public string pbno { get; set; }
            public DateTime pbdate { get; set; }
            public string compstatus { get; set; }
            public string batchcode { get; set; }
            public string batchdesc { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string sirunit { get; set; }
            public double rsqty { get; set; }
            public double proqty { get; set; }
            public double bal { get; set; }
            public double uresqty { get; set; }
            public double fresqty { get; set; }
            public double ttlisuqty { get; set; }
            public double donqty { get; set; }
            public double retissue { get; set; }
        }

        [Serializable]
        public class EclassProdManList
        {
            public string mgrrno { set; get; }
            public string storid { set; get; }
            public string stordesc { set; get; }
            public string ssircode { set; get; }
            public string ssirdesc { set; get; }
            public string batchcode { get; set; }
            public string batchdesc { get; set; }
            public DateTime mgrrdate { set; get; }
            public string appstatus { get; set; }
            public double qty { set; get; }
            public double amt { set; get; }

            public string mgrrno1 { set; get; }
            public string suplname { set; get; }
            public string refno { set; get; }
            public string remarks { set; get; }

        }

        [Serializable]
        public class EclassMttLst
        {
            public string mgrrno { get; set; }
            public string actdesc { get; set; }
            public string itmdesc { get; set; }
            public string spcfdesc { get; set; }
            public string itmunit { get; set; }
            public double conqty { get; set; }
            public double conamt { get; set; }
            public EclassMttLst() { }

        }

        [Serializable]
        public class EClassSalProInfo
        {

            public string procode { set; get; }
            public string batchcode { set; get; }
            public string prodesc { set; get; }
            public string batchdesc { set; get; }
            public string rsirunit { set; get; }
            public double qty { set; get; }
            public double prorate { set; get; }
            public double proamt { set; get; }
            public DateTime expdate { get; set; }
            public EClassSalProInfo()
            {

            }


            public EClassSalProInfo(string procode, string batchcode, string prodesc, string batchdesc, string rsirunit,
                double qty, double prorate, double proamt, DateTime expdate)
            {
                this.procode = procode;
                this.batchcode = batchcode;
                this.prodesc = prodesc;
                this.batchdesc = batchdesc;
                this.rsirunit = rsirunit;
                this.qty = qty;
                this.prorate = prorate;
                this.proamt = proamt;
                this.expdate = expdate;
            }
        }

        [Serializable]
        public class ReqAdjstmntList
        {
            public string comcod { get; set; }
            public string reqno { get; set; }
            public string reqdat { get; set; }
            public string mrfno { get; set; }
            public double preqty { get; set; }
            public double billqty { get; set; }
            public double balqty { get; set; }

            public string ack { get; set; }
        }

    }   
}