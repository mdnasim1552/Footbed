using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_03_CostABgd
{
    public class PerOrderStatus
    {
        [Serializable]
        public class PerOrdrStat
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

            public PerOrdrStat() { }
        }


        [Serializable]
        public class PerProdStat
        {
            public string comcod { get; set; }
            public string prdno { get; set; }
            public string linecode { get; set; }
            public string linedesc { get; set; }
            public string mlccod { get; set; }
            public string ordrcod { get; set; }
            public string ordrdesc { get; set; }
            public string styleid { get; set; }
            public string styledesc { get; set; }
            public string buyerid { get; set; }
            public string buyername { get; set; }
            public string storeid { get; set; }
            public string storename { get; set; }
            public string prddate { get; set; }
            public string inqno { get; set; }
            public double proqty { get; set; }
            public double amount { get; set; }
            public double rate { get; set; }

            public PerProdStat() { }
        }
        
        [Serializable]
        public class PerProdStatDetails
        {
            public string comcod { get; set; }
            public string unit { get; set; }
            public string prdno { get; set; }
            public string linecode { get; set; }
            public string linedesc { get; set; }
            public string mlccod { get; set; }
            public string ordrdesc { get; set; }
            public string storeid { get; set; }
            public string styleid { get; set; }
            public string styledesc { get; set; }
            public string colorid { get; set; }
            public string colordesc { get; set; }
            public string sizeid { get; set; }
            public string sizedesc { get; set; }
            public double proqty { get; set; }
            public string prddate { get; set; }
            public string inqno { get; set; }
            public string buyerid { get; set; }
            public string buyername { get; set; }
            public string preqno { get; set; }
            public string dayid { get; set; }
            public string location { get; set; }
            public string locdesc { get; set; }
            public string artno { get; set; }
            public string flrordrnum { get; set; }
            

            public PerProdStatDetails() { }
        }

        public class ExportPlanVsAchiv
        {
            public string comcod { get; set; }
            public string orderno { get; set; }
            public string orderdesc { get; set; }
            public string slnum { get; set; }
            public DateTime shimentdate { get; set; }
            public string prodline { get; set; }
            public string linedesc { get; set; }
            public double shimentqty { get; set; }
            public double proplanqty { get; set; }
            public string trialordr { get; set; }
            public string country { get; set; }
            public string custorder { get; set; }
            public DateTime startdate { get; set; }
            public DateTime enddate { get; set; }
            public string ordsheet { get; set; }
            
            public ExportPlanVsAchiv() { }
        }
    }
}
