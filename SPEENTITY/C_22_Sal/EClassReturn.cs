using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_22_Sal
{
    public class EClassReturn
    {
        [Serializable]

        public class EClassLastRet
        {

            public string maxno { set; get; }
            public string maxno1 { set; get; }
            public EClassLastRet()
            {
            }
            public EClassLastRet(string maxno, string maxno1)
            {
                this.maxno = maxno;
                this.maxno1 = maxno1;

            }
        }

        [Serializable]

        public class EClassPreRet
        {

            public string retmemo { set; get; }
            public string retmemo1 { set; get; }

            public string custcode { set; get; }
            public string custdesc { set; get; }

            public string invno { set; get; }
            public string invno1 { set; get; }
            public string orderno { set; get; }
            public string teamcode { set; get; }
            public string teamdesc { set; get; }
            public EClassPreRet()
            {
            }
            public EClassPreRet(string retmemo, string retmemo1, string custcode, string custdesc, string invno, string invno1, string orderno, string teamcode, string teamdesc)
            {
                this.retmemo = retmemo;
                this.retmemo1 = retmemo1;
                this.custcode = custcode;
                this.custdesc = custdesc;
                this.invno = invno;
                this.invno1 = invno1;
                this.orderno = orderno;
                this.teamcode = teamcode;
                this.teamdesc = teamdesc;
            }
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
        public class EClassGetInvoice
        {

            public string invno { set; get; }
            public string invno1 { set; get; }
            public string custcode { set; get; }
            public string custdesc { set; get; }
            public string orderno { set; get; }
            public string teamcode { set; get; }
            public string teamdesc { set; get; }

            public double itmamt { set; get; }
            public double paidamt { set; get; }
            public double balamt { set; get; }


            public EClassGetInvoice(string invno, string invno1, string custcode, string custdesc, string orderno, string teamcode, string teamdesc)
            {
                this.invno = invno;
                this.invno1 = invno1;
                this.custcode = custcode;
                this.custdesc = custdesc;
                this.orderno = orderno;
                this.teamcode = teamcode;
                this.teamdesc = teamdesc;
            }
        }



    }
}
