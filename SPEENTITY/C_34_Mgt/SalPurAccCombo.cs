using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_34_Mgt
{
    public class  SalPurAccCombo
    {
        public string yearmon {set;get;}
        public string yearmon1 { set; get; }
        public double ttlsalamt { set; get; }
        public double tpayamt { set; get; }
        public double tllpuramt { set; get; }
        public double collamt { set; get; }
        public double dram { set; get; }
        public double cram { set; get; }

        public double taramt { set; get; }
        public double examt { set; get; }



    }

    // select pactcode, pactdesc, rsircode, sirdesc, bgdamt, trnamt, balamt, proamt, appamt, qty, rate=iif(qty=0.00,0.00,proamt/qty), ppdamt from #tblotreq1  order by  pactcode, rsircode

    [Serializable]
    public class OthReqStatus
    {
        public string pactcode { get; set; }
        public string pactdesc { get; set; }
        public string rsircode { get; set; }
        public string sirdesc { get; set; }
        public double bgdamt { get; set; }
        public double trnamt { get; set; }
        public double proamt { get; set; }
        public double appamt { get; set; }
        public double qty { get; set; }
        public double rate { get; set; }
        public double ppdamt { get; set; }
        public string billno { get; set; }

        public OthReqStatus() { }
    }
}
