using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_15_Pro
{
    public class BO_Production
    {
        [Serializable]
        public class EClassYear
        {
            public string yearid { set; get; }

            public double bgdamt { set; get; }
            public double proamt { set; get; }

            public EClassYear(string yearid, double bgdamt, double proamt)
            {
                this.yearid = yearid;
                this.bgdamt = bgdamt;
                this.proamt = proamt;
            }
        }

        [Serializable]
        public class EClassMonthly
        {
            public string yearmon { set; get; }

            public string yearmon1 { set; get; }

            public double bgdamt { set; get; }
            public double proamt { set; get; }

            public EClassMonthly(string yearmon, string yearmon1, double bgdamt, double proamt)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.bgdamt = bgdamt;
                this.proamt = proamt;
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

            public EClassWeekly(string wcode1, double wsamt1, double wcamt1, string wcode2, double wsamt2, double wcamt2, string wcode3, double wsamt3, double wcamt3,
                    string wcode4, double wsamt4, double wcamt4)
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
            }
        }
         [Serializable]
        public class EClassDayWise
        {
             public string pbdate { set; get; }
             public string pbno { set; get; }
             public string preqno { set; get; }
             public string batchcode { set; get; }
             public string batchdesc { set; get; }

             public string rsircode { set; get; }
             public string rsirdesc { set; get; }
            public double rsqty { set; get; }
            public double preqamt { set; get; }
            public double bgdrat { set; get; }

            public EClassDayWise(string pbdate, string pbno, string preqno, string batchcode, string batchdesc, string rsircode, string rsirdesc, 
                    double rsqty, double preqamt, double bgdrat)
            {
                this.pbdate = pbdate;
                this.pbno = pbno;
                this.preqno = preqno;
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.rsircode = rsircode;
                this.rsirdesc = rsirdesc;
                this.rsqty = rsqty;
                this.preqamt = preqamt;
                this.bgdrat = bgdrat;
            }
        }

        [Serializable]
        public class EClassDayWiseExe
        {
            public string batchcode { set; get; }
            public string batchdesc { set; get; }
            public string storid { set; get; }
            public string centrdesc { set; get; }
            public string itmcod { set; get; }

            public string rsirdesc { set; get; }
            public string prodate { set; get; }
            public string vounum1 { set; get; }
            public double proqty { set; get; }
            public double proamt { set; get; }
            public double rate { set; get; }

            public EClassDayWiseExe(string batchcode, string batchdesc, string storid, string centrdesc, string itmcod, string rsirdesc, string prodate, string vounum1,
                 double proqty, double proamt, double rate)
            {
                this.batchcode = batchcode;
                this.batchdesc = batchdesc;
                this.storid = storid;
                this.centrdesc = centrdesc;
                this.itmcod = itmcod;
                this.rsirdesc = rsirdesc;
                this.prodate = prodate;
                this.vounum1 = vounum1;
                this.proqty = proqty;
                this.proamt = proamt;
                this.rate = rate;
            }
        }
        [Serializable]

        public class ProductionLOssLIst
        {
            public string comcod { get; set; }
            public string bactcode { get; set; }
            public string bactdesc { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public double targetqty { get; set; }
            public double targetamt { get; set; }
            public double proqty { get; set; }
            public double proamt { get; set; }
            public double qcrecqty { get; set; }
            public double qcamt { get; set; }
            public double totloss { get; set; }
            public double prloss { get; set; }
            public double qcloss { get; set; }
            public double totlosamt { get; set; }
            public double prlosamt { get; set; }
            public double qclosamt { get; set; }
        }


        [Serializable]
        public class OrdProdShipment
        {
            public string comcod { get; set; }
            public string mlccod { get; set; }
            public string mlcdesc { get; set; }
            public string curreny { get; set; }
            public double conrate { get; set; }
            public double orval { get; set; }
            public double proval { get; set; }
            public double shipval { get; set; }
            public double ordqty { get; set; }
            public double proqty { get; set; }
            public double shipqty { get; set; }
            public double ordvspro { get; set; }
            public double ordvsship { get; set; }
            public double provsship { get; set; }
            public string brand { get; set; }
            public string brandesc { get; set; }
            public OrdProdShipment() { }
        }

        // comcod, flrcode, linecode, mlccod, stylecode, protqty , proacqty, whour, capacity, macno, soexqty, hproqty, flrdesc, linedesc, mlcdesc, buyer, styledesc 
        [Serializable]
        public class WorkVsAchievment 
        {
            public string comcod { get; set; }
            public string flrcode { get; set; }
            public string linecode { get; set; }
            public string mlccod { get; set; }
            public string stylecode { get; set; }
            public double protqty { get; set; }
            public double proacqty { get; set; }
            public double whour { get; set; }
            public double capacity { get; set; }
            public double macno { get; set; }
            public double soexqty { get; set; }
            public double hproqty { get; set; }
            public string flrdesc { get; set; }
            public string linedesc { get; set; }
            public string mlcdesc { get; set; }
            public string buyer { get; set; }
            public string styledesc { get; set; }
            public WorkVsAchievment() { }
        }

    
        [Serializable]
        public class EclassProdDetails
        {
            public string comcod { get; set; }
            public string prdno { get; set; }
            public string mlccod { get; set; }
            public string styleid { get; set; }
            public string styldedesc { get; set; }
            public string styleunit { get; set; }
            public string colorid { get; set; }
            public string colordesc { get; set; }
            public string sizeid { get; set; }
            public string sizedesc { get; set; }
            public double prdqty { get; set; }
        }

        [Serializable]
        public class EclassDefectOrder
        {
            public string comcod { get; set; }
            public string mlccod { get; set; }
            public string styleid { get; set; }
            public string qcapplieddate { get; set; }
            public string styledesc { get; set; }
            public string sizedesc { get; set; }
            public string compdesc { get; set; }
            public string defectname { get; set; }
            public string orderno { get; set; }
            public double qty { get; set; }
            public string remarks { get; set; }
            public string linedesc { get; set; }
            public string processdesc { get; set; }
        }

        [Serializable]
        public class EclassDefectParChart
        {
            public string comcod { get; set; }
            public string defectcode { get; set; }
            public string defectname { get; set; }
            public double qty { get; set; }
            public double cumqty { get; set; }
            public double cumpercnt { get; set; }
        }

        [Serializable]
        public class EclassProdProcess
        {
            public int pid { get; set; }
            public string ppnno { get; set; }
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string rt { get; set; }
            public string orderno { get; set; }
            public string step { get; set; }
            public string styleid { get; set; }
            public string styledesc { get; set; }
            public string colorid { get; set; }
            public string colordesc { get; set; }
            public string sizeid { get; set; }
            public string sizedesc { get; set; }
            public string rdate { get; set; }
            public double recvqty { get; set; }
            public string tdate { get; set; }
            public double trnsqty { get; set; }
            public double balqty { get; set; }
            public double rejectionqty { get; set; }
            public double manpower { get; set; }
            public double repairqty { get; set; }
            public string floorline { get; set; }
            public string machine { get; set; }
            public string prodtime { get; set; }
            public string styleunit { get; set; }
            public string flordesc { get; set; }
            public string machdesc { get; set; }
            public string timedesc { get; set; }
            public string tprostep { get; set; }
            public string tprostepdesc { get; set; }
            public string fprostep { get; set; }
            public string fprostepdesc { get; set; }
            public double ordrqty { get; set; }
            public double proqty { get; set; }
            public  string qcstatus { get; set; }
            public string preqno { get; set; }
            public EclassProdProcess() {}
            public EclassProdProcess(string comcod, string grp, string grpdesc, string rt, string orderno, string step, string styleid,
                string styledesc, string colorid, string colordesc, string sizeid, string sizedesc, string rdate, double recvqty,
                string tdate, double trnsqty, double balqty, double rejectionqty, double manpower, double repairqty, string floorline,
                string machine, string prodtime, string styleunit, string flordesc, string machdesc, string timedesc, string tprostep,
                string tprostepdesc, string fprostep, string fprostepdesc, string preqno)
            {
                this.comcod = comcod;
                this.grp = grp;
                this.grpdesc = grpdesc;
                this.rt = rt;
                this.orderno = orderno;
                this.step = step;
                this.styleid = styleid;
                this.styledesc = styledesc;
                this.colorid = colorid;
                this.colordesc = colordesc;
                this.sizeid = sizeid;
                this.sizedesc = sizedesc;
                this.rdate = rdate;
                this.recvqty = recvqty;
                this.tdate = tdate;
                this.trnsqty = trnsqty;
                this.balqty = balqty;
                this.rejectionqty = rejectionqty;
                this.manpower = manpower;
                this.repairqty = repairqty;
                this.floorline = floorline;
                this.machine = machine;
                this.prodtime = prodtime;
                this.styleunit = styleunit;
                this.flordesc = flordesc;
                this.machdesc = machdesc;
                this.timedesc = timedesc;
                this.tprostep = tprostep;
                this.tprostepdesc = tprostepdesc;
                this.fprostep = fprostep;
                this.fprostepdesc = fprostepdesc;
                this.preqno = preqno;
            }

            [Serializable]
            public class RptProdReqPrint
            {
                public string comcod { get; set; }
                public string mlccod { get; set; }
                public string procode { get; set; }
                public string procname{ get; set; }
                public string itmno { get; set; }
                public string itmdesc { get; set; }
                public string itmunit { get; set; }
                public string spcfcod { get; set; }
                public string spcfdesc { get; set; }
                public string itmqty { get; set; }
                public double itmqty1 { get; set; }
                public double tqty { get; set; }
                public double stockqty { get; set; }
                public string actdesc { get; set; }

            }
            [Serializable]
            public class RptProditmPrint
            {
                public string comcod { get; set; }
                public string procode { get; set; }
                public string rsirdesc { get; set; }
                public string rsirunit { get; set; }
                public double tqty { get; set; }
                public double cresqty { get; set; }

            }

        }

        [Serializable]
        public class EclassManualProduction
        {
            public string comcod { get; set; }
            public string dayid { get; set; }
            public string styleid { get; set; }
            public string colorid { get; set; }
            public string sizeid { get; set; }
            public string mlccod { get; set; }
            public string styledesc { get; set; }
            public string mlcdesc { get; set; }
            public string rsirunit { get; set; }
            public string orderno { get; set; }
            public double qty { get; set; }
            public double prorate { get; set; }
            public double proamt { get; set; }
            public double fcamt { get; set; }
            public string location { get; set; }
            public string locedsc { set; get; }

            public EclassManualProduction() { }
            public EclassManualProduction(string comcod, string styleid, string colorid, string sizeid, string mlccod, string styledesc, string mlcdesc, string rsirunit,
                    string orderno,double qty, double prorate, double proamt, string 
                dayid, double fcamt, string location)
            {
                this.comcod = comcod;
                this.styleid = styleid;
                this.colorid = colorid;
                this.sizeid = sizeid;
                this.styledesc = styledesc;
                this.mlccod = mlccod;
                this.mlcdesc = mlcdesc;
                this.rsirunit = rsirunit;
                this.orderno = orderno;
                this.qty = qty;
                this.prorate = prorate;
                this.proamt = proamt;
                this.dayid = dayid;
                this.fcamt = fcamt;
                this.location = location;
            }

        }

        public class EclassManualiProCost
        {
            public string comcode { set; get; }
            public string mgrrno { set; get; }
            public string mlccod { set; get; }
            public string itmcode { set; get; }
            public string itmdesc { set; get; }
            public string itmunit { set; get; }
            public string spcfcod { set; get; }
            public string spcfdesc { set; get; }
            public string itmqty { set; get; }
            public string itmamt { set; get; }
            
            public EclassManualiProCost() { }

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
            public string rmentrystatus { get; set; }
            public string rmappstatus { get; set; }


        }


        [Serializable]
        public class Day_wise_Production
        {
            public string codes { get; set; }

            public string itmdesc { get; set; }

            public double proqty { get; set; }

            public double targetqty { get; set; }

            public double variance { get; set; }

            public double efficiency { get; set; }
            public double qcqty { get; set; }
            public double rejqty { get; set; }

        }

        [Serializable]
        public class EclassProdGainLoss
        {
            public double proqty { get; set; }
            public double proamt { get; set; }
            public double qcqty { get; set; }
            public double qcamt { get; set; }
            public double rejqty { get; set; }
            public double rejamt { get; set; }
            public double ttlqcqty { get; set; }
            public double ttlqcamt { get; set; }

        }

        [Serializable]
        public class RptDailyProdBalanceSheet
        {
            public string comcod { get; set; }
            public string rwtype { get; set; }
            public string dtype { get; set; }
            public string qcdate { get; set; }
            public string mlccod { get; set; }
            public string styleid { get; set; }
            public string colorid { get; set; }
            public double totalqty { get; set; }
            public double rejqty { get; set; }
            public double repairqty { get; set; }
            public double b1 { get; set; }
            public double b2 { get; set; }
            public double b3 { get; set; }
            public double b4 { get; set; }
            public double b5 { get; set; }
            public double b6 { get; set; }
            public double b7 { get; set; }
            public double b8 { get; set; }
            public double b9 { get; set; }
            public double b10 { get; set; }
            public double b11 { get; set; }
            public double b12 { get; set; }
            public double b13 { get; set; }
            public double b14 { get; set; }
            public double b15 { get; set; }
            public double b16 { get; set; }
            public double b17 { get; set; }
            public double b18 { get; set; }
            public double b19 { get; set; }
            public double b20 { get; set; }
            public double b21 { get; set; }
            public double b22 { get; set; }
            public double b23 { get; set; }
            public double b24 { get; set; }
            public double b25 { get; set; }
            public double b26 { get; set; }
            public double b27 { get; set; }
            public double b28 { get; set; }
            public double b29 { get; set; }
            public double b30 { get; set; }
            public double b31 { get; set; }
            public double b32 { get; set; }
            public double b33 { get; set; }
            public double b34 { get; set; }
            public double b35 { get; set; }
            public double b36 { get; set; }
            public double b37 { get; set; }
            public double b38 { get; set; }
            public double b39 { get; set; }
            public double b40 { get; set; }
        }

        [Serializable]
        public class RptSizeProdBalanceSheet
        {
            public string comcod { get; set; }
            public string rwtype { get; set; }
            public string dtype { get; set; }
            public string fprostep { get; set; }
            public string fprostepdesc { get; set; }
            public string mlccod { get; set; }
            public string styleid { get; set; }
            public string colorid { get; set; }
            public double rejqty { get; set; }
            public double repairqty { get; set; }
            public double totalqty { get; set; }
            public double b1 { get; set; }
            public double b2 { get; set; }
            public double b3 { get; set; }
            public double b4 { get; set; }
            public double b5 { get; set; }
            public double b6 { get; set; }
            public double b7 { get; set; }
            public double b8 { get; set; }
            public double b9 { get; set; }
            public double b10 { get; set; }
            public double b11 { get; set; }
            public double b12 { get; set; }
            public double b13 { get; set; }
            public double b14 { get; set; }
            public double b15 { get; set; }
            public double b16 { get; set; }
            public double b17 { get; set; }
            public double b18 { get; set; }
            public double b19 { get; set; }
            public double b20 { get; set; }
            public double b21 { get; set; }
            public double b22 { get; set; }
            public double b23 { get; set; }
            public double b24 { get; set; }
            public double b25 { get; set; }
            public double b26 { get; set; }
            public double b27 { get; set; }
            public double b28 { get; set; }
            public double b29 { get; set; }
            public double b30 { get; set; }
            public double b31 { get; set; }
            public double b32 { get; set; }
            public double b33 { get; set; }
            public double b34 { get; set; }
            public double b35 { get; set; }
            public double b36 { get; set; }
            public double b37 { get; set; }
            public double b38 { get; set; }
            public double b39 { get; set; }
            public double b40 { get; set; }
        }


        [Serializable]
        public class RptQltyNdProd
        {
            public string comcod { get; set; }
            public string mlccod { get; set; }
            public string rdate { get; set; }
            public string styleid { get; set; }
            public string colorid { get; set; }
            public double ordrqty { get; set; }
            public double qty { get; set; }
            public double repairqty { get; set; }
            public double rejectionqty { get; set; }
            public double manpower { get; set; }
            public string odayid { get; set; }
            public double uptoqty { get; set; }
            public double ordrbal { get; set; }
            public string causeofrej { get; set; }
            public string styledesc { get; set; }
            public string colordesc { get; set; }
            public string orderno { get; set; }
            public string buyer { get; set; }
            public string buyerdesc { get; set; }
            public double machine { get; set; }
            public double whours { get; set; }
        }


        [Serializable]
        public class RptQltyNdProd2
        {
            public string comcod { get; set; }
            public string reasons { get; set; }
            public string reasondesc { get; set; }
            public string pcode { get; set; }
        }

        [Serializable]
        public class RptMonthlyProdAnalyticalReport
        {
            public string section { get; set; }
            public double manpower { get; set; }
            public double macqty { get; set; }
            public double ttlqty { get; set; }
            public double wrkhours { get; set; }
            public double rejectionqty { get; set; }
            public double repairqty { get; set; }
            public double perhourprod { get; set; }
            public double permanprod { get; set; }
            public double rewrkprcnt { get; set; }
            public double rejprcnt { get; set; }
            public string majorproblem { get; set; }
        }

        [Serializable]
        public class DefectPareto
        {
            public string defectcode { get; set; }
            public string defectname { get; set; }
            public double qty { get; set; }
            public double cumqty { get; set; }
            public double cumpercnt { get; set; }
        }
    }
}
