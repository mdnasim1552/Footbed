using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPEENTITY.C_05_ProShip
{

    public class EClassPlanning
    {
        [Serializable]
        public class EclassFullYearEvents
        {
            public string sl { get; set; }
            public string comcod { get; set; }
            public string dayid { get; set; }
            public string dstatus { get; set; }
            public string remarks { get; set; }
            public string dstatusdet { get; set; }

        }

        [Serializable]
        public class RptMaterialMaster
        {
            public string comcod { get; set; }
            public string bomid { get; set; }
            public string itmno { get; set; }
            public string itmdesc { get; set; }
            public string itmunit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public double itmqty { get; set; }
            public string color { get; set; }
            public string orderno { get; set; }
            public string matgrp { get; set; }
            public string matgrpdesc { get; set; }
            public double stockqty { get; set; }
            public double bomstock { get; set; }
            public double bomisuqty { get; set; }
            public double bomisubal { get; set; }
            public double avilbqty { get; set; }
        }

        [Serializable]
        public class BOM
        {
            public int comcod { get; set; }
            public string bomid { get; set; }
            public string bomdesc { get; set; }
            public string mlccod { get; set; }
            public string mlcdesc { get; set; }
            public string dayid { get; set; }
            public bool pipeline { get; set; }
        }

        [Serializable]
        public class OrderStatusRpt
        {
            public int comcod { get; set; }
            public string mlccod { get; set; }
            public string dayid { get; set; }
            public string colorid { get; set; }
            public string colordesc { get; set; }
            public string buyerid { get; set; }
            public string constuction { get; set; }
            public string constdesc { get; set; }
            public string agent { get; set; }
            public string agentdesc { get; set; }
            public string shoetype { get; set; }
            public string shoetypdesc { get; set; }
            public string article { get; set; }
            public string styleid { get; set; }
            public double ordrqty { get; set; }
            public string odrdate { get; set; }
            public string customorder { get; set; }
            public string exfacdat { get; set; }
            public string weeknumber { get; set; }
            public string buyerdesc { get; set; }
            public string images { get; set; }
            public string leadtime { get; set; }
            public string lformacod { get; set; }
            public string lformadesc { get; set; }
            public string bomid { get; set; }
            public string outsolesource { get; set; }
            public double bookqty { get; set; }
            public double shipedqty { get; set; }
            public double bookbal { get; set; }
            public double leatherpct { get; set; }
            public double synthtpc { get; set; }
            public double ornament { get; set; }
            public double threadpct { get; set; }
            public double outsole { get; set; }
            public bool pipeline { get; set; }
            public bool uppknif { get; set; }
            public bool linknif { get; set; }
            public bool botknif { get; set; }
            public string knif { get; set; }
            public string pptdate { get; set; }
            public string cutstart { get; set; }
            public string cutend { get; set; }
            public string sewstart { get; set; }
            public string sewend { get; set; }
            public string fitstart { get; set; }
            public string fitend { get; set; }
            public string lasstart { get; set; }
            public string lasend { get; set; }
            public double cutdone { get; set; }
            public double sewdone { get; set; }
            public double fitdone { get; set; }
            public double lastdone { get; set; }
            public string confirmdate { get; set; }
        }

        [Serializable]
        public class RptProcessBaseProdPlan
        {
            public string comcod { get; set; }
            public string mlccod { get; set; }
            public string mlcdesc { get; set; }
            public string styleid { get; set; }
            public string styledesc { get; set; }
            public string colorid { get; set; }
            public string colordesc { get; set; }
            public string orderno { get; set; }
            public double ordrqty { get; set; }
            public string slnum { get; set; }
            public string linecode { get; set; }
            public string linedesc { get; set; }
            public string odayid { get; set; }
            public double totalqty { get; set; }
            public double d1 { get; set; }
            public double d2 { get; set; }
            public double d3 { get; set; }
            public double d4 { get; set; }
            public double d5 { get; set; }
            public double d6 { get; set; }
            public double d7 { get; set; }
            public double d8 { get; set; }
            public double d9 { get; set; }
            public double d10 { get; set; }
            public double d11 { get; set; }
            public double d12 { get; set; }
            public double d13 { get; set; }
            public double d14 { get; set; }
            public double d15 { get; set; }
            public double d16 { get; set; }
            public double d17 { get; set; }
            public double d18 { get; set; }
            public double d19 { get; set; }
            public double d20 { get; set; }
            public double whours { get; set; }
            public DateTime endate { get; set; }
        }

        [Serializable]
        public class PlanDates
        {
            public DateTime plandate { get; set; }
            public string pdayname { get; set; }
            public string wknum { get; set; }
            public string daystatus { get; set; }
            public string remarks { get; set; }
            public int wekdays { get; set; }

        }

        [Serializable]
        public class ArticleLayoutClass
        {
            public string comcod { get; set; }
            public string mlccod { get; set; }
            public string proccod { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public double smv { get; set; }
            public double oparator { get; set; }
            public double helper { get; set; }
            public string oprname { get; set; }
            public string helpername { get; set; }
            public double eftarget { get; set; }
            public string linkcode { get; set; }
            public string mctype { get; set; }
            public double eftrgt60 { get; set; }

        }

        [Serializable]
        public class EWrkCapMon
        {
            public string comcod { get; set; }
            public string empname { get; set; }
            public string idcardno { get; set; }
            public string oprname { get; set; }
            public string mlccod { get; set; }
            public string process { get; set; }
            public double cycletime { get; set; }
            public string procod { get; set; }
            public string dayid { get; set; }
            public string machtyp { get; set; }
            public string frequency { get; set; }
            public int manpower { get; set; }
            public string pmc { get; set; }
            public string totalcap { get; set; }

        }
    }
}
