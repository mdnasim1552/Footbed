using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_81_Hrm.C_92_Mgt
{
    public class BO_EmpSep
    {
        [Serializable]
        public class EmpSep01
        {
            public string empname { get; set; }
            public string refno { get; set; }
            public string idcard { get; set; }
            public string desig { get; set; }
            public string dept { get; set; }
            public string presadd { get; set; }
            public string pastoff { get; set; }
            public string thana { get; set; }
            public string dist { get; set; }
            public string permadd { get; set; }
            public string sdate { get; set; }
            public string sepdate { get; set; }
            public string empnameeng { get; set; }
            public string prsentaddeng { get; set; }
            public string permaddeng { get; set; }
            public string pseng { get; set; }
            public string disteng { get; set; }
            public string duration { get; set; }
            public string joiningdate { get; set; }
            public string signatory { get; set; }
            public string signadesig { get; set; }
            
        }
    }
}
