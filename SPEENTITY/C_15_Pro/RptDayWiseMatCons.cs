using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_15_Pro
{
    public class DayWiseMatCons
    {
        [Serializable]
        public class DPRWiseMattCons
        {
            public string comcod { get; set; }
            public string preqno { get; set; }
            public DateTime pbdate { get; set; }
            public string batchdesc { get; set; }
            public string bomid { get; set; }
            public double rproqty { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string spcfdesc { get; set; }
            public double rsqty { get; set; }
            public double isuqty { get; set; }
            public double fisuqty { get; set; }
            public double isubalqty { get; set; }
            public double fisubalqty { get; set; }
            public double recfgqty { get; set; }
            public double balfgqty { get; set; }
            public DPRWiseMattCons() { }
        }
    }
}
