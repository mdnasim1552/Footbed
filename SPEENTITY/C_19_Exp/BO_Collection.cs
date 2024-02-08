using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_19_Exp
{
    [Serializable]
    public class BO_Collection
    {
        public string centrid { set; get; }
        public string custid { set; get; }
        public string centrdesc { set; get; }
        public string memono { set; get; }
        public string name { set; get; }
        public string chqno { set; get; }
        public double fcamt { set; get; }
        public double amount { set; get; }
        public double fcbnkcharge { set; get; }
        public double vatamt { set; get; }
        public double cglamt { set; get; }
        public string vounum { set; get; }
        public string mrdat { set; get; }
        public string bnknam { set; get; }
        public string memono1 { set; get; }
        public string paydat { set; get; }
        public string username { set; get; }
        public string bbranch { set; get; }
        public string paytype { set; get; }
        public string voudat { set; get; }
        public string dbcode { set; get; }
        public string dbpoint { set; get; }
        public string remarks { set; get; }

        public BO_Collection() { }

        public BO_Collection(string centrid, string custid, string centrdesc, string memono, string name, string chqno, double fcamt, double amount, double fcbnkcharge, 
            double vatamt, double cglamt, string vounum, string mrdat, string bnknam, string memono1, string paydat, string username, string bbranch, string paytype,
            string voudat, string remarks)
        {
            this.centrid = centrid;
            this.custid = custid;
            this.centrdesc = centrdesc;
            this.memono = memono;
            this.name = name;
            this.chqno = chqno;
            this.fcamt = fcamt;
            this.amount = amount;
            this.fcbnkcharge = fcbnkcharge;
            this.vatamt = vatamt;
            this.cglamt = cglamt;
            this.vounum = vounum;
            this.mrdat = mrdat;
            this.bnknam = bnknam;
            this.memono1 = memono1;
            this.paydat = paydat;
            this.username = username;
            this.bbranch = bbranch;
            this.paytype = paytype;
            this.voudat = voudat;
            this.remarks = remarks;
        }

       
    }

    [Serializable]
    public class PlanVSAch
    {

        public string comcod { get; set; }
        public string grpcode { get; set; }
        public string grpdesc { get; set; }
        public string actcode { get; set; }
        public double bgdamt { get; set; }
        public double acamt { get; set; }
        public double perAch { get; set; }
        public double uptaramt { get; set; }
        public double upachamt { get; set; }
        public double perAch2 { get; set; }
        public string actdesc { get; set; }


    }
    [Serializable]

    public class DelvIMEI
    {
        public string comcod { get; set; }
        public string centrid { get; set; }

        public string orderno { get; set; }
        public string orderno1 { get; set; }
        public string custid { get; set; }
        public string sirdesc { get; set; }
        public string gdatat { get; set; }
        public string gdesc { get; set; }
        public string orderdat { get; set; }
        public string rsircode { get; set; }
        public string model { get; set; }
        public string mimei { get; set; }
        public string simei { get; set; }


    }
    [Serializable]

    public class PaymentHistory
    {
        //grp, a.sgp
        public string grp { get; set; }
        public string sgp { get; set; }
        public string comcod { get; set; }
        public string centrid { get; set; }
        public string actdesc { get; set; }
        public string custid { get; set; }
        public string sirdesc { get; set; }
        public string orderno { get; set; }
        public string orderdat { get; set; }
        public string orderdat1 { get; set; }
        public double orderamt { get; set; }
        public double paidamt { get; set; }
        public double balance { get; set; }
        public string orderno1 { get; set; }

        public double qty { get; set; }
        public double balqty { get; set; }


    }
    [Serializable]

    public class ProIssuance
    {

        public string comcod { get; set; }
        public string centrid { get; set; }
        public string actdesc { get; set; }
        public string custid { get; set; }
        public string sirdesc { get; set; }
        public string gdatat { get; set; }
        public string gdesc { get; set; }
        public string model { get; set; }
        public string prcod { get; set; }
        public double salqty { get; set; }
        public double salamt { get; set; }
    }
    [Serializable]
    public class CompWiseCollection
    {
        public string comcod { get; set; }
        public string comname { get; set; }
        public string mrslno { get; set; }
        public string centrid { get; set; }
        public string centrdesc { get; set; }
        public string custid { get; set; }
        public string custdesc { get; set; }
        public DateTime recvdate { get; set; }
        public DateTime paydate { get; set; }
        public string paytype { get; set; }
        public string paytype1 { get; set; }
        public string repchckno { get; set; }
        public string chqno { get; set; }
        public string custbnk { get; set; }
        public string custbnkbrnch { get; set; }
        public string refid { get; set; }
        public string remarks { get; set; }
        public string recvtype { get; set; }
        public string recvtype1 { get; set; }
        public string compbnkid { get; set; }
        public string compbnkdesc { get; set; }
        public double recamount { get; set; }
        public string collfrm { get; set; }

        public CompWiseCollection() { }
        public CompWiseCollection(string comcod, string comname, string centrid, string centrdesc, string custid, string custdesc, DateTime recvdate, DateTime paydate, string paytype, string paytype1,
           string repchckno, string chqno, string custbnk, string custbnkbrnch, string refid,string remarks, string recvtype, string recvtype1, string compbnkid, string compbnkdesc, double recamount, string collfrm) {
            this.comcod = comcod;
            this.comname = comname;
            this.centrid = centrid;
            this.centrdesc = centrdesc;
            this.custid = custid;
            this.custdesc = custdesc;
            this.recvdate = recvdate;
            this.paydate = paydate;
            this.paytype = paytype;
            this.paytype1 = paytype1;
            this.repchckno = repchckno;
            this.chqno = chqno;
            this.custbnk = custbnk;
            this.custbnkbrnch = custbnkbrnch;
            this.refid = refid;
            this.remarks = remarks;
            this.recvtype = recvtype;
            this.recvtype1 = recvtype1;
            this.compbnkid = compbnkid;
            this.compbnkdesc = compbnkdesc;
            this.recamount = recamount;
            this.collfrm = collfrm;
    }

    }
    [Serializable]
    public class SalReplaceRecvLIst
    {
        public string teamcode { get; set; }
        public string teamname { get; set; }
        public string spocod { get; set; }
         public string territory { get; set; }
        
        public string memono { get; set; }

        public string memono1 { get; set; }
        public string centrid { get; set; }
        public string centrdesc { get; set; }
        public string repmemo { get; set; }
        public string custid { get; set; }
        public string custdesc { get; set; }
        public string repdat { get; set; }
        public string prcod { get; set; }
        public string prodesc { get; set; }
        public double itmqty { get; set; }
        public double itmamt { get; set; }
        public string aprvstatus {get;set;}



    }
    [Serializable]
    public class ComWiseCollectionMR
    {
        public string comcod { get; set; }
        public string comname { get; set; }
        public string centrid { get; set; }
        public string centrdesc { get; set; }
        public string custid { get; set; }
        public string custdesc { get; set; }
        public string custadd { get; set; }
        public DateTime mrdat { get; set; }
        public double amount { get; set; }
        public string baltype { get; set; }
        public double payamt { get; set; }
        public double invamt { get; set; }
        public double balance { get; set; }
        public string acno { get; set; }
        public string bank { get; set; }
        public string dbcode { get; set; }
        public string dbpoint { get; set; }

    }

    [Serializable]
    public class SalReturnRecvLIst
    {
        public string retmemo { get; set; }
        public string retmemo1 { get; set; }
        public string centrid { get; set; }
        public string centrdesc { get; set; }      
        public string custid { get; set; }
        public string custdesc { get; set; }
        public string retdat { get; set; }
        public string prcod { get; set; }
        public string prodesc { get; set; }
        public double itmqty { get; set; }
        public double itmamt { get; set; }
        public string aprvstatus { get; set; }


    }

}
