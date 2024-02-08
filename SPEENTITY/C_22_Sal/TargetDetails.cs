using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_22_Sal
{
   public class TargetDetails
    {
       public string id { get; set; }
       public string dbcode { get; set; }
       public string proid { get; set; }
       public string proqty { get; set; }
       public string tericode { get; set; }
    }
    [Serializable]
    public class EClassDashboardList
    {
        public string divnme { get; set; }
        public string srtname { get; set; }
        public string wdays { get; set; }
        public double msalttlamt { get; set; }
        public double monthcoll { get; set; }
        public double tsaleamt { get; set; }
        public double tcollamt { get; set; }
        public double totalcust { get; set; }
        public double totalret { get; set; }
    }
    [Serializable]
    public class EclassCustomerProductSumary
    {
        public string sircode { get; set; }
        public string sirdesc { get; set; }
        public double itmqty { get; set; }
        public double itmamt { get; set; }

    }
}
