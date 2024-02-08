using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SPEENTITY.C_22_Sal
{
    public class EClassDelivery
    {
        [Serializable]
        public class EClassSalesOrder
        {


            public string sorderno { set; get; }
            public string sorderno1 { set; get; }
            public string pactcode { set; get; }
            public string custcode { set; get; }
            public string custdesc { set; get; }
            public string teamdesc { set; get; }
            public string ssircode { set; get; }
            public string ssirdesc { set; get; }
            public string sorddate { set; get; }
            public string astatus { set; get; }
            public string cusaddr { set; get; }
            public string phone { set; get; }

            public string narration { set; get; }

            public EClassSalesOrder(string sorderno, string sorderno1, string pactcode, string custcode, string custdesc, string teamdesc, string ssircode, string ssirdesc,
                string sorddate, string astatus, string cusaddr, string phone, string narration)
            {
                this.sorderno = sorderno;
                this.sorderno1 = sorderno1;
                this.pactcode = pactcode;
                this.custcode = custcode;
                this.custdesc = custdesc;
                this.teamdesc = teamdesc;
                this.ssircode = ssircode;
                this.ssirdesc = ssirdesc;
                this.sorddate = sorddate;
                this.astatus = astatus;
                this.cusaddr = cusaddr;
                this.phone = phone;
                this.narration = narration;
            }



        }

        [Serializable]
        public class EClassLastDel
        {

            public string maxsdelno { set; get; }
            public string maxsdelno1 { set; get; }

            public EClassLastDel(string maxsdelno, string maxsdelno1)
            {
                this.maxsdelno = maxsdelno;
                this.maxsdelno1 = maxsdelno1;

            }
        }





        [Serializable]
        public class EClassDelDetails
        {

            public string rsircode { set; get; }
            public string rsirdesc { set; get; }

            public string batchcode { set; get; }
            public string batchdesc { set; get; }
            public string rsirunit { set; get; }
            public double sorderqty { set; get; }
            public double utodelqty { set; get; }
            public double balqty { set; get; }
            public double delqty { set; get; }
            public double avlablqty { set; get; }
            public string wastatus { set; get; }
            public double promqty { set; get; }
            public double dpromqty { set; get; }
            public double tqty { set; get; }
            public double stockqty { set; get; }

            public EClassDelDetails()
            {

            }
            public EClassDelDetails(string rsircode, string rsirdesc, string batchcode, string batchdesc, string rsirunit, double sorderqty, double utodelqty,
                double balqty, double delqty, double avlablqty, string wastatus, double promqty, double dpromqty, double tqty, double stockqty)
            {
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.rsirunit = rsirunit;
                this.sorderqty = sorderqty;
                this.utodelqty = utodelqty;
                this.balqty = balqty;
                this.delqty = delqty;
                this.avlablqty = avlablqty;
                this.wastatus = wastatus;
                this.promqty = promqty;
                this.dpromqty = dpromqty;
                this.tqty = tqty;
                this.stockqty = stockqty;
            }
        }



        [Serializable]
        public class EClassOrdAppDetails
        {

            public string pactcode { set; get; }
            public string orderno1 { set; get; }
            public string orderno { set; get; }
            public string rsircode { set; get; }
            public string rsirdesc { set; get; }
            public string rsirunit { set; get; }
            public double ordrqty { set; get; }
            public double ordraqty { set; get; }
            public double balty { set; get; }
            public double aprovqty { set; get; }
            public double ordamt { set; get; }
            public double orddis { set; get; }
            public double freeqty { set; get; }


            public EClassOrdAppDetails()
            {

            }
            public EClassOrdAppDetails(string pactcode, string orderno, string orderno1, string rsircode, string rsirdesc, string rsirunit, double ordrqty, double ordraqty,
                double balty, double aprovqty, double ordamt, double orddis, double freeqty)
            {
                this.pactcode = pactcode;
                this.orderno1 = orderno1;
                this.orderno = orderno;
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;

                this.rsirunit = rsirunit;
                this.ordrqty = ordrqty;
                this.ordraqty = ordraqty;
                this.balty = balty;
                this.aprovqty = aprovqty;
                this.ordamt = ordamt;
                this.orddis = orddis;
                this.freeqty = freeqty;

            }
        }

        [Serializable]
        public class EClassGetDelivery
        {

            public string sdelno1 { set; get; }
            public DateTime sdeldate { set; get; }
            public string sorderno1 { set; get; }
            public string refo { set; get; }
            public string narration { set; get; }
            public string sorderno { set; get; }

            public string pactdesc { set; get; }
            public string sactcode { set; get; }

            public string sircode { set; get; }
            public string sirdesc { set; get; }

            public string transdet1 { set; get; }
            public string transdet2 { set; get; }
            public string transdet3 { set; get; }
            public string teamdesc { set; get; }
            public EClassGetDelivery(string sdelno1, DateTime sdeldate, string sorderno1, string refo, string narration, string sorderno, string pactdesc, string sactcode,
                    string sircode, string sirdesc, string transdet1, string transdet2, string transdet3, string teamdesc)
            {

                this.sdelno1 = sdelno1;
                this.sdeldate = sdeldate;
                this.sorderno1 = sorderno1;
                this.refo = refo;
                this.narration = narration;
                this.sorderno = sorderno;
                this.pactdesc = pactdesc;
                this.sactcode = sactcode;

                this.sircode = sircode;
                this.sirdesc = sirdesc;

                this.transdet1 = transdet1;
                this.transdet2 = transdet2;
                this.transdet3 = transdet3;
                this.teamdesc = teamdesc;

            }
        }


        [Serializable]
        public class EClassOrderApproval
        {

            public string sdelno1 { set; get; }
            public DateTime sdeldate { set; get; }
            public string sorderno1 { set; get; }
            public string refo { set; get; }
            public string narration { set; get; }
            public string sorderno { set; get; }

            public string pactdesc { set; get; }
            public string sactcode { set; get; }

            public string sircode { set; get; }
            public string sirdesc { set; get; }

            public string transdet1 { set; get; }
            public string transdet2 { set; get; }
            public string transdet3 { set; get; }
            public string teamdesc { set; get; }
            public EClassOrderApproval(string sdelno1, DateTime sdeldate, string sorderno1, string refo, string narration, string sorderno, string pactdesc, string sactcode,
                    string sircode, string sirdesc, string transdet1, string transdet2, string transdet3, string teamdesc)
            {

                this.sdelno1 = sdelno1;
                this.sdeldate = sdeldate;
                this.sorderno1 = sorderno1;
                this.refo = refo;
                this.narration = narration;
                this.sorderno = sorderno;
                this.pactdesc = pactdesc;
                this.sactcode = sactcode;

                this.sircode = sircode;
                this.sirdesc = sirdesc;

                this.transdet1 = transdet1;
                this.transdet2 = transdet2;
                this.transdet3 = transdet3;
                this.teamdesc = teamdesc;

            }
        }


        [Serializable]
        public class EClassPreDelivery
        {

            public string sdelno { set; get; }
            public string sdelno1 { set; get; }

            public EClassPreDelivery(string sdelno, string sdelno1)
            {
                this.sdelno = sdelno;
                this.sdelno1 = sdelno1;

            }
        }


        [Serializable]
        public class EClassProIMEI
        {
            public string rsircode { get; set; }
            public string batchcode { get; set; }
            public string batchdesc { get; set; }
            public string packno { set; get; }
            public string mimei { set; get; }
            public string simei { set; get; }
            public string rsirdesc { set; get; }
            public int seq { set; get; }
            public EClassProIMEI()
            {

            }
            public EClassProIMEI(string rsircode, string batchcode, string batchdesc, string packno, string mimei, string simei, string rsirdesc, int seq)
            {

                this.rsircode = rsircode;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.packno = packno;
                this.mimei = mimei;
                this.simei = simei;
                this.rsirdesc = rsirdesc;
                this.seq = seq;
            }
        }
        [Serializable]
        public class EClassProIMEIPOS
        {
            public string rsircode { get; set; }
            public string batchcode { get; set; }
            public string batchdesc { get; set; }
            public string packno { set; get; }
            public string mimei { set; get; }
            public string simei { set; get; }
            public string rsirdesc { set; get; }
            public int seq { set; get; }
            public double stprice { set; get; }
            public double tax { set; get; }
            public double disamt { set; get; }
            public EClassProIMEIPOS()
            {

            }
            public EClassProIMEIPOS(string rsircode, string batchcode, string batchdesc, string packno, string mimei, string simei, string rsirdesc, int seq, double stprice,
                double tax, double disamt)
            {

                this.rsircode = rsircode;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.packno = packno;
                this.mimei = mimei;
                this.simei = simei;
                this.rsirdesc = rsirdesc;
                this.seq = seq;
                this.stprice = stprice;
                this.tax = tax;
                this.disamt = disamt;

            }
        }

        [Serializable]
        public class EClassProIMEITrans
        {
            public string rsircode { get; set; }
            public string batchcode { get; set; }
            public string batchdesc { get; set; }
            public string packno { set; get; }
            public string mimei { set; get; }
            public string simei { set; get; }
            public string rsirdesc { set; get; }
            public int seq { set; get; }
            public double stprice { set; get; }
            public double wsale { set; get; }


            public EClassProIMEITrans()
            {

            }
            public EClassProIMEITrans(string rsircode, string batchcode, string batchdesc, string packno, string mimei, string simei, string rsirdesc, int seq, double stprice, double wsale)
            {

                this.rsircode = rsircode;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.packno = packno;
                this.mimei = mimei;
                this.simei = simei;
                this.rsirdesc = rsirdesc;
                this.seq = seq;
                this.stprice = stprice;
                this.wsale = wsale;
            }
        }

        [Serializable]
        public class EClassRetIMEI
        {
            public string sdelno { set; get; }
            public string rsircode { get; set; }
            public string packno { set; get; }
            public string mimei { set; get; }
            public string simei { set; get; }
            public string rsirdesc { set; get; }
            public int seq { set; get; }
            public string batchcode { set; get; }

            public string batchdesc { set; get; }
            public EClassRetIMEI()
            {

            }
            public EClassRetIMEI(string sdelno, string rsircode, string packno, string mimei, string simei, string rsirdesc, int seq, string batchcode, string batchdesc)
            {
                this.sdelno = sdelno;
                this.rsircode = rsircode;
                this.packno = packno;
                this.mimei = mimei;
                this.simei = simei;
                this.rsirdesc = rsirdesc;
                this.seq = seq;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;

            }
        }
        [Serializable]
        public class EClassOrderInfo
        {


            public string rsircode { set; get; }
            public string rsirdesc { set; get; }

            public string batchcode { set; get; }
            public string batchdesc { set; get; }
            public string rsirunit { set; get; }
            public double sorderqty { set; get; }
            public double utodelqty { set; get; }
            public double balqty { set; get; }
            public double delqty { set; get; }
            public double avlablqty { set; get; }

            public string wastatus { set; get; }
            public double promqty { set; get; }
            public double dpromqty { set; get; }
            public double tqty { set; get; }
            public double stockqty { set; get; }

            public EClassOrderInfo()
            { }
            public EClassOrderInfo(string rsircode, string rsirdesc, string batchcode, string batchdesc, string rsirunit, double sorderqty, double utodelqty,
                double balqty, double delqty, double avlablqty, string wastatus, double promqty, double dpromqty, double tqty, double stockqty)
            {
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.rsirunit = rsirunit;
                this.sorderqty = sorderqty;
                this.utodelqty = utodelqty;
                this.balqty = balqty;
                this.delqty = delqty;
                this.avlablqty = avlablqty;
                this.wastatus = wastatus;
                this.promqty = promqty;
                this.dpromqty = dpromqty;
                this.tqty = tqty;
                this.stockqty = stockqty;
            }


        }

        [Serializable]
        public class EClassStore
        {

            public string actcode { set; get; }
            public string actdesc { set; get; }

            public EClassStore(string actcode, string actdesc)
            {
                this.actcode = actcode;
                this.actdesc = actdesc;

            }
        }


        [Serializable]
        public class EClassFStoreInfo
        {

            public string sactcode2 { set; get; }
            public string sactdesc2 { set; get; }
            public string rsircode { set; get; }
            public string rsirdesc { set; get; }
            public string rsirunit { set; get; }
            public double delqty { set; get; }

            public EClassFStoreInfo()
            {

            }


            public EClassFStoreInfo(string sactcode2, string sactdesc2, string rsircode, string rsirdesc, string rsirunit, double delqty)
            {
                this.sactcode2 = sactcode2;
                this.sactdesc2 = sactdesc2;
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.rsirunit = rsirunit;
                this.delqty = delqty;

            }
        }

        [Serializable]
        public class EClassOrderInfo2
        {


            public string rsircode { set; get; }
            public string rsirdesc { set; get; }

            public string batchcode { set; get; }
            public string batchdesc { set; get; }
            public string rsirunit { set; get; }
            public double sorderqty { set; get; }
            public double utodelqty { set; get; }
            public double balqty { set; get; }
            public double delqty { set; get; }
            public double avlablqty { set; get; }

            public string wastatus { set; get; }
            public double promqty { set; get; }
            public double dpromqty { set; get; }
            public double stockqty { set; get; }
            public EClassOrderInfo2(string rsircode, string rsirdesc, string batchcode, string batchdesc, string rsirunit, double sorderqty, double utodelqty,
                double balqty, double delqty, double avlablqty, string wastatus, double promqty, double dpromqty, double stockqty)
            {
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.rsirunit = rsirunit;
                this.sorderqty = sorderqty;
                this.utodelqty = utodelqty;
                this.balqty = balqty;
                this.delqty = delqty;
                this.avlablqty = avlablqty;
                this.wastatus = wastatus;
                this.promqty = promqty;
                this.dpromqty = dpromqty;
                this.stockqty = stockqty;
            }


        }

        [Serializable]
        public class EClassAccCode
        {
            public string rescode { set; get; }
            public string resdesc { set; get; }
            public string rsirunit { set; get; }
            public double stkqty { set; get; }
            public double rate { set; get; }
            public EClassAccCode()
            { }
            public EClassAccCode(string rescode, string resdesc, string rsirunit, double stkqty, double rate)
            {
                this.rescode = rescode;
                this.resdesc = resdesc;
                this.rsirunit = rsirunit;
                this.stkqty = stkqty;
                this.rate = rate;
            }


        }

        [Serializable]
        public class EClassAccAdd
        {
            public string actcode { set; get; }
            public string usircode { set; get; }
            public string rescode { set; get; }
            public string resdesc { set; get; }
            public string rsirunit { set; get; }
            public double qty { set; get; }
            public double rate { set; get; }
            public double amount { set; get; }
            public EClassAccAdd()
            { }
            public EClassAccAdd(string actcode, string usircode, string rescode, string resdesc, string rsirunit, double qty, double rate, double amount)
            {
                this.actcode = actcode;
                this.usircode = usircode;
                this.rescode = rescode;
                this.resdesc = resdesc;
                this.rsirunit = rsirunit;
                this.qty = qty;
                this.rate = rate;
                this.amount = amount;
            }


        }

    }
}
