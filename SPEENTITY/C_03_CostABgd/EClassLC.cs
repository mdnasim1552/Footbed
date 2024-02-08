using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_03_CostABgd
{
    public class EClassLC
    {


        [Serializable]
        public class LcDetailsInfo
        {           
            public string comcod { get; set; }
            public string mlccod { get; set; }
            public string mlcdesc { get; set; }
            public string lcdate { get; set; }
            public string ordtype { get; set; }
            public string buyercode { get; set; }
            public string buyerdesc { get; set; }
            public double lcvalfc { get; set; }
            public string currency { get; set; }
            public string currencydesc { get; set; }
            public double curramt { get; set; }
            public double amtbdt { get; set; }
            public double ordrqty { get; set; }
            public string piinfo { get; set; }
            public double piqty { get; set; }

            public double costamt { get; set; }
            public string bominfo { get; set; }
            public double bomqty { get; set; }


            public LcDetailsInfo() { }
        }

        [Serializable]
        public class PerOrderStatus
        {
            public string comcod { get; set; }
            public string dayid { get; set; }
            public string mlccod { get; set; }
            public string mlcdesc { get; set; }
            public string ordrcod { get; set; }
            public string ordrdesc { get; set; }
            public string orderdat { get; set; }
            public double ordrqty { get; set; }
            public string styleid { get; set; }
            public string styledesc { get; set; }
            public string buyerid { get; set; }
            public string buyername { get; set; }
            public double fcrate { get; set; }
            public double rate { get; set; }
            public double fcamt { get; set; }
            public double amt { get; set; }
            public string unit { get; set; }

            public PerOrderStatus() { }
        }


        [Serializable]
        public class EclassOrderDetails
        {
            public string preqno { get; set; }
            public string mlccod { get; set; }
            public string mlcdesc { get; set; }
            public string styleid { get; set; }
            public string styleunit { get; set; }
            public string artno { get; set; }
            public string hscode { get; set; }
            public string styledesc { get; set; }
            public double stylqty { get; set; }
            public double stylerate { get; set; }
            public double styleamt { get; set; }
            public string styleselect { get; set; }
            public string imgpath { get; set; }
            public string colorid { get; set; }
            public string colordesc { get; set; }
            public string colorselect { get; set; }
            public string sizeid { get; set; }
            public string sizedesc { get; set; }
            public string sizeselect { get; set; }
            public string location { get; set; }
            public string locationdesc { get; set; }
            public double amount { get; set; }
            public double perqtyamt { get; set; }
            public double s1 { get; set; }
            public double s2 { get; set; }
            public double s3 { get; set; }
            public double s4 { get; set; }
            public double s5 { get; set; }
            public double s6 { get; set; }
            public double s7 { get; set; }
            public double s8 { get; set; }
            public double s9 { get; set; }
            public double s10 { get; set; }
            public double s11 { get; set; }
            public double s12 { get; set; }
            public double s13 { get; set; }
            public double s14 { get; set; }
            public double s15 { get; set; }
            public double s16 { get; set; }
            public double s17 { get; set; }
            public double s18 { get; set; }
            public double s19 { get; set; }
            public double s20 { get; set; }
            public double s21 { get; set; }
            public double s22 { get; set; }
            public double s23 { get; set; }
            public double s24 { get; set; }
            public double s25 { get; set; }
            public double s26 { get; set; }
            public double s27 { get; set; }
            public double s28 { get; set; }
            public double s29 { get; set; }
            public double s30 { get; set; }
            public double s31 { get; set; }
            public double s32 { get; set; }
            public double s33 { get; set; }
            public double s34 { get; set; }
            public double s35 { get; set; }
            public double s36 { get; set; }
            public double s37 { get; set; }
            public double s38 { get; set; }
            public double s39 { get; set; }
            public double s40 { get; set; }
            public double p1 { get; set; }
            public double p2 { get; set; }
            public double p3 { get; set; }
            public double p4 { get; set; }
            public double p5 { get; set; }
            public double p6 { get; set; }
            public double p7 { get; set; }
            public double p8 { get; set; }
            public double p9 { get; set; }
            public double p10 { get; set; }
            public double p11 { get; set; }
            public double p12 { get; set; }
            public double p13 { get; set; }
            public double p14 { get; set; }
            public double p15 { get; set; }
            public double p16 { get; set; }
            public double p17 { get; set; }
            public double p18 { get; set; }
            public double p19 { get; set; }
            public double p20 { get; set; }
            public double p21 { get; set; }
            public double p22 { get; set; }
            public double p23 { get; set; }
            public double p24 { get; set; }
            public double p25 { get; set; }
            public double p26 { get; set; }
            public double p27 { get; set; }
            public double p28 { get; set; }
            public double p29 { get; set; }
            public double p30 { get; set; }
            public double p31 { get; set; }
            public double p32 { get; set; }
            public double p33 { get; set; }
            public double p34 { get; set; }
            public double p35 { get; set; }
            public double p36 { get; set; }
            public double p37 { get; set; }
            public double p38 { get; set; }
            public double p39 { get; set; }
            public double p40 { get; set; }
            public double totalqty { get; set; }
            public double cutqty { get; set; }

        }
       
        [Serializable]
        public class BudgetResource
        {
            public string itmno { get; set; }
            public string itmdesc { get; set; }
            public string spcfcode { get; set; }
            public string spcfdesc { get; set; }
            public string itmunit { get; set; }
            public double itmqty { get; set; }
            public double itmrat { get; set; }
            public double itmamt { get; set; }
        }


        [Serializable]
        public class StdCostSheetRPT
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string resdesc { get; set; }
            public string spcfcode { get; set; }
            public string procode { get; set; }
            public string rescode { get; set; }
            public string prodesc { get; set; }
            public string spcfdesc { get; set; }
            public string resunit { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }
            public double avgrate { get; set; }
            public double avgamt { get; set; }
            public double maxrate { get; set; }
            public double conqty { get; set; }
            public double maxamt { get; set; }
            public double westpc { get; set; }
            public double percnt { get; set; }
            public double bdtamt { get; set; }
            public string compcode { get; set; }
            public string compname { get; set; }
            public string imgpath { get; set; }


        }


        [Serializable]
        public class EClassMatReqAgainsOrder
        {
            public string inqno { get; set; }
            public string procode { get; set; }
            public string procname { get; set; }
            public string rsircode { get; set; }
            public string mlccode { get; set; }
            public string dayid { get; set; }
            public string rsirdesc { get; set; }
            public string rsirdesc1 { get; set; }
            public string spcfcode { get; set; }
            public string spcfdesc { get; set; }
            public string spcfdesc1 { get; set; }
            public string compcode { get; set; }
            public string compdesc { get; set; }
            public string color { get; set; }
            public string size { get; set; }
            public double rstdqty { get; set; }
            public double srstdqty { get; set; }
            public double rate { get; set; }
            public string runit { get; set; }
            public double stdamt { get; set; }
            public string ssircode { get; set; }
            public string ssirdesc { get; set; }
            public string suparticle { get; set; }
            public string origin { get; set; }
            public string country { get; set; }
            public string cmposition { get; set; }
            public double addicons { get; set; }
            public double addiwestpc { get; set; }
            public string remarks { get; set; }
            public string reqtype { get; set; }
            public string nrcod { get; set; }
            public string nrdesc { get; set; }
            public string sizedetails { get; set; }
            public string purtype { get; set; }
            public double prcnt { get; set; }
            public string fgsize { get; set; }
            public string imgurl { get; set; }
            public double conqty { get; set; }
            public double conqtyold { get; set; }
            public double subtotal { get; set; }
            public double westpc { get; set; }
            public double westamt { get; set; }
            public EClassMatReqAgainsOrder() { }
            public EClassMatReqAgainsOrder( string inqno, string procode, string procname, string rsircode, string rsirdesc, string spcfcode,
                string spcfdesc, string compcode, string compdesc, string color, string size, double rstdqty, double rate, string runit, double stdamt,
                string ssircode, string ssirdesc, string suparticle, string origin, string country, string cmposition, string remarks, string reqtype,
                string nrcod, string nrdesc, string purtype, string fgsize, string mlccode, string dayid) {
                this.inqno = inqno;
                this.procode = procode;
                this.procname = procname;
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.spcfcode = spcfcode;
                this.spcfdesc = spcfdesc;
                this.compcode = compcode;
                this.compdesc = compdesc;
                this.color = color;
                this.size = size;
                this.rstdqty = rstdqty;
                this.rate = rate;
                this.runit = runit;
                this.stdamt = stdamt;
                this.ssircode = ssircode;
                this.ssirdesc = ssirdesc;
                this.suparticle = suparticle;
                this.origin = origin;
                this.country = country;
                this.cmposition = cmposition;
                this.remarks = remarks;
                this.reqtype = reqtype;
                this.nrcod = nrcod;
                this.nrdesc = nrdesc;
                this.purtype = purtype;
                this.fgsize = fgsize;
                this.mlccode = mlccode;
                this.dayid = dayid;
            }
        }


  
      //  comcod, mlccod, gencod, gendata, style, styledesc, confrmprice, ordrqty, consprice, bomprice=(bomcost+dircost)/ordrqty, bomcost=bomcost+dircost,
        //dircost, revenue=ordrqty*confrmprice, profloss=((ordrqty*confrmprice)-(bomcost+dircost)) from #tbltopshet

        [Serializable]
       public class BomApproval01
       {
            public string comcod { get; set; }
            public string mlccod { get; set; }
            public string gencod { get; set; }
            public string gendata { get; set; }
            public string style { get; set; }
            public string styledesc { get; set; }
            public double confrmprice { get; set; }
            public double ordrqty { get; set; }
            public double consprice { get; set; }
            public double bomprice { get; set; }
            public double bomcost { get; set; }
            public double dircost { get; set; }
            public double revenue { get; set; }
            public double profloss { get; set; }
            public BomApproval01() { }
       }
        // select comcod, grp, mlccod, rsircode, spcfcod, itmqty, itmamt, reqtype, rsirdesc, reqsl, percnt from #tblfinal1 order by reqsl, grp
        [Serializable]
        public class BomApproval02
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string mlccod { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public double itmqty { get; set; }
            public double itmamt { get; set; }
            public string reqtype { get; set; }
            public string rsirdesc { get; set; }
            public string reqsl { get; set; }
            public double percnt { get; set; }
            public BomApproval02() { }
        }


        //select a.comcod, a.rsircode,rsirdesc=isnull(b.sirdesc,''), stdamt=a.stdamt*isnull(c.ordrqty,0.00), percnt=((a.stdamt*isnull(c.ordrqty,0.00)*100)/@totalcost) from #tblcomcostsum a 
        [Serializable]
       public class BomApproval03
       {
            public string comcod { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public double stdamt { get; set; }
            public double percnt { get; set; }
            public BomApproval03() { }
       }
    }
}
