using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_17_GFInv
{
    [Serializable]
    public class FGCenterStorec
    {
        //comcod, a.ordrno, a.actdesc, rate=isnull(b.rate,0.00), a.opproqty, opproamt=(a.opproqty*isnull(b.rate,0.00)), a.proqty, proamt=(a.proqty*isnull(b.rate,0.00)), a.shipqty, shipamt=(a.shipqty*isnull(b.rate,0.00)),
        //a.stockqty, stockamt=(a.stockqty*isnull(b.rate,0.00)), trnqty=0.00, trnamt=0.00
        public string grp { get; set; }
        public string comcod { get; set; }
        public string ordrno { get; set; }
        public string actdesc { get; set; }
        public double rate { get; set; }
        public double opproqty { get; set; }
        public double opproamt { get; set; }
        public double proqty { get; set; }
        public double proamt { get; set; }
        public double shipqty { get; set; }
        public double shipamt { get; set; }
        public double stockqty { get; set; }
        public double stockamt { get; set; }
        public double trnqty { get; set; }
        public double trnamt { get; set; }
        public double inprqty { get; set; }
        public string custid { get; set; }
        public string custdesc { get; set; }
        public string styleid { get; set; }
        public string styldesc { get; set; }
        public string colorid { get; set; }
        public string colordesc { get; set; }
        public string odayid { get; set; }
        public string resdesc { get; set; }
        public string sizeid { get; set; }
        public string sizedesc { get; set; }
        public string location { get; set; }
        public string locdesc { get; set; }
        public double aging { get; set; }
        public DateTime lastprduction { get; set; }

        public FGCenterStorec() { }
    }

    [Serializable]
    public class FGInspection
    {
        public string comcod { get; set; }
        public string mlccod { get; set; }
        public string dayid { get; set; }
        public string styleid { get; set; }
        public string styledesc { get; set; }
        public double price { get; set; }
        public string orderno { get; set; }
        public string unit { get; set; }
        public DateTime orderdat { get; set; }
        public string colorid { get; set; }
        public string colordesc { get; set; }
        public double ordrqty { get; set; }
        public double insqty { get; set; }
        public string linecode { get; set; }
        public double amajor { get; set; }
        public double aminor { get; set; }
        public double failqty { get; set; }
        public double passqty { get; set; }
        public string insbyname { get; set; }
        public DateTime insbydate { get; set; }
        public string remarks { get; set; }
        public string buyerid { get; set; }
        public string buyername { get; set; }
        public string result { get; set; }
        public double fcritical { get; set; }
        public double fmajor { get; set; }
        public double fminor { get; set; }


        //public FGInspection() { }
    }
}
