using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_81_Hrm.C_92_Mgt
{
    public class EClass_HRM
    {
        [Serializable]
        public class EClassEmpStatus
        {
            public string statusdesc { set; get; }
            public int noofemp { set; get; }
            public EClassEmpStatus(string statusdesc, int noofemp)
            {
                this.statusdesc = statusdesc;
                this.noofemp = noofemp;
            }
        }

        [Serializable]
        public class EClassTodayAttnStatus
        {
            public double actemp { set; get; }
            public double prsntempprcnt { set; get; }
            public double absntempprcnt { set; get; }
            public double leavempprcnt { set; get; }
            public double hldayempprcnt { set; get; }
            public EClassTodayAttnStatus(){ }
        }

        [Serializable]
        public class EClassAttnStatus
        {
            public string comcod { get; set; }
            public string ymonday { get; set; }
            public string ymonddesc { get; set; }
            public decimal actemp { get; set; }
            public decimal present { get; set; }
            public decimal absent { get; set; }
            public decimal leave { get; set; }
            public EClassAttnStatus()
            {

            }
        }

        [Serializable]
        public class EClassCurYearSalary
        {
            public string comcod { get; set; }
            public string ymonday { get; set; }
            public string ymonddesc { get; set; }
            public decimal salamt { get; set; }
            public EClassCurYearSalary()
            {

            }
        }

        [Serializable]
        public class IPSetupInf
        {
            public string machno { get; set; }
            public string ipaddress { get; set; }
            public string machinealias { get; set; }
            public string port { get; set; }
            public IPSetupInf() { }
        }
    }
}
