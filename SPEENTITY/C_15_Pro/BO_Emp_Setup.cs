using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_15_Pro
{
    public class BO_Emp_Setup
    {
        [Serializable]
        public class EClassEmpProc
        {
            public string sircode { set; get; }

            public string sirdesc { set; get; }

            public EClassEmpProc(string sircode, string sirdesc)
            {
                this.sircode = sircode;
                this.sirdesc = sirdesc;
            }
        }
    }
}
