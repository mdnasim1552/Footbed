using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_15_Pro
{
 public   class GetDateWiseMatIssueInfoClass
    {
        [Serializable]
        public class Matsummary
        {
            public string comcod { get; set; }
            public string itmcode { get; set; }
            public string itmname { get; set; }
            public string spcfdesc { get; set; }
            public string color { get; set; }
            public string size { get; set; }
            public double itmqty { get; set; }
            public double itmamt { get; set; }
            public Matsummary()
            {

            }

        }

        [Serializable]
        public class Matsummaryreport
        {
            public string comcod { get; set; }
            public string isueno { get; set; }
            public string orderno { get; set; }
            public string orderno1 { get; set; }
            public string itmcode { get; set; }
            public string itmdesc { get; set; }
            public string sirunit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public double itmqty { get; set; }
            public double itmamt { get; set; }
            public Matsummaryreport()
            {

            }

        }

        [Serializable]
        public class MatIssueDetailsReport
        {
            public string preqno1 { get; set; }
            public string isueno1 { get; set; }
            public DateTime isuedate { get; set; }
            public string article { get; set; }
            public string orderno1 { get; set; }
            public string itemdesc { get; set; }
            public string spcfdesc { get; set; }
            public string color { get; set; }
            public string sizes { get; set; }
            public double reqqty { get; set; }
            public double itmqty { get; set; }
            public double remissuqty { get; set; }
            public double conqty { get; set; }
            public string conuntdesc { get; set; }
            public MatIssueDetailsReport()
            {

            }

        }

        [Serializable]
        public class Matdetails
        {
            public string comcod { get; set; }
            public string isueno1 { get; set; }
            public string isueno { get; set; }
            public DateTime isuedate { get; set; }
            public double itemcount { get; set; }
            public string orderno { get; set; }
            public string ordername { get; set; }
            public double itmqty { get; set; }
            public double totalamount { get; set; }
           
            public Matdetails()
            {

            }

        }



        [Serializable]
        public class Matdetailsreport
        {
            public string comcod { get; set; }
            public string isueno { get; set; }
            public DateTime isuedate { get; set; }
            public string orderno { get; set; }
            public string orderdesc { get; set; }
            public Matdetailsreport()
            {

            }

        }




    }
}
