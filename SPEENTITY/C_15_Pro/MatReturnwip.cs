using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_15_Pro
{
    public class MatReturnwip
    {
        [Serializable]
        public class wipinfolist
        {
            public string comcod { get; set; }
            public string misuno1 { get; set; }
            public string misuno { get; set; }
            public string bactcode { get; set; }
            public string actcode { get; set; }
            public string bactdesc { get; set; }
            public string actdesc { get; set; }
            public wipinfolist() { }
        }
        [Serializable]
        public class meteriallist
        {
            public string comcod { get; set; }
            public string bactcode { get; set; }
            public string bactdesc { get; set; }
            public string actcode { get; set; }
            public string storedesc { get; set; }
            public string misuno { get; set; }
            public string rsircode { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }
            public string sirdesc { get; set; }
            public double rqty { get; set; }
            public double ramt { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }

            public meteriallist() { }
        }
        [Serializable]
        public class RetmetList
        {
            public string comcod { get; set; }
            public string retissuno { get; set; }
            public string retissdat { get; set; }
            public string bactcode { get; set; }
            public string bactdesc { get; set; }
            public string actdesc { get; set; }
            public string actcode { get; set; }
            public string misuno1 { get; set; }
            public string misuno { get; set; }

        }
        [Serializable]
        public class MatReIssueMatList
        {
            public string comcod { get; set; }
            public string preqno { get; set; }
            public string preqno1 { get; set; }
            public string bactcode { get; set; }
            public string bactdesc { get; set; }
            public string procode { get; set; }
            public string prodesc { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public double balqty { get; set; }
            public double isuqty { get; set; }
            public double isurate { get; set; }
            public string pbdate { get; set; }
            public double stockqty { get; set; }
            public double balstkqty { get; set; }

        }
        [Serializable]

        public class DaywiseMatisselist
        {
            public string comcod { get; set; }
            public string misuno { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string bactcode { get; set; }
            public string bactdesc { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string unit { get; set; }
            public double tarqty { get; set; }
            public double taramt { get; set; }
            public double isuqty { get; set; }
            public double issuamt { get; set; }
            public double rate { get; set; }
            public double norqty { get; set; }
            public double noramt { get; set; }
            public double reqisuqty { get; set; }
            public double reissamt { get; set; }
            public double retqty { get; set; }
            public double retamt { get; set; }
            public string matexper { get; set; }
            public string balqty1 { get; set; }

            public double matexper1 { get; set; }
            public double balqty { get; set; }
            public double balamt { get; set; }



        }
        [Serializable]
        public class IssuedMatlist
        {
            public string comcod { get; set; }
            public string misuno { get; set; }
            public string misudate { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string bactcode { get; set; }
            public string bactdesc { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public double isuqty { get; set; }
            public double issuamt { get; set; }
            public double isurate { get; set; }
        }
        [Serializable]
        public class ReqProlist
        {
            public string comcod { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public double isuqty { get; set; }
            public string unit { get; set; }
        }

    }
}