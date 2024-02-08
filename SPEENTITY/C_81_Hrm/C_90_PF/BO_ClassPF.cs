using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_81_Hrm.C_90_PF
{
    [Serializable]
    public class BO_ClassPF
    {
        public string comcod { get; set; }
        public string emptype { get; set; }
        public string refno { get; set; }
        public string section { get; set; }
        public string idcard { get; set; }
        public string empid { get; set; }
        public string empname { get; set; }
        public string desigdesc { get; set; }
        public DateTime joinindat { get; set; }
        public DateTime resigndate { get; set; }
        public string servlength { get; set; }
        public string deptmentid { get; set; }
        public string emptypedesc { get; set; }
        public string sectiondesc { get; set; }
        public string deptment { get; set; }
        public string grade { get; set; }
        public string Joblocation { get; set; }
        public double bsal { get; set; }
        public double gspay { get; set; }
        public double pfund { get; set; }
        public double opnamt { get; set; }
        public double netpfamt { get; set; }
        public double comconpfamt { get; set; }
        public double payablepfamt { get; set; }
        public BO_ClassPF() { }
    }
}
