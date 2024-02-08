using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_31_Mis
{
    public class BO_Production
    {
        [Serializable]
        public class EClassYear
        {
            public string yearid { set; get; }

            public double protar { set; get; }
            public double proact { set; get; }

            public EClassYear(string yearid, double protar, double proact)
            {
                this.yearid = yearid;
                this.protar = protar;
                this.proact = proact;
            }
        }
        [Serializable]
        public class EClassMonthly
        {
            public string yearmon { set; get; }

            public string yearmon1 { set; get; }

            public double protar { set; get; }
            public double proact { set; get; }

            public EClassMonthly(string yearmon, string yearmon1, double protar, double proact)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.protar = protar;
                this.proact = proact;
            }
        }
        [Serializable]
        public class EClassWeekly
        {
            public string wcode1 { set; get; }
            public double wtamt1 { set; get; }
            public double waamt1 { set; get; }

            public string wcode2 { set; get; }
            public double wtamt2 { set; get; }
            public double waamt2 { set; get; }

            public string wcode3 { set; get; }
            public double wtamt3 { set; get; }
            public double waamt3 { set; get; }

            public string wcode4 { set; get; }
            public double wtamt4 { set; get; }
            public double waamt4 { set; get; }

            public EClassWeekly(string wcode1, double wtamt1, double waamt1, string wcode2, double wtamt2, double waamt2, string wcode3, double wtamt3, double waamt3,
                    string wcode4, double wtamt4, double waamt4)
            {
                this.wcode1 = wcode1;
                this.wtamt1 = wtamt1;
                this.waamt1 = waamt1;
                this.wcode2 = wcode2;
                this.wtamt2 = wtamt2;
                this.waamt2 = waamt2;
                this.wcode3 = wcode3;
                this.wtamt3 = wtamt3;
                this.waamt3 = waamt3;
                this.wcode4 = wcode4;
                this.wtamt4 = wtamt4;
                this.waamt4 = waamt4;
            }
        }
         [Serializable]
        public class EClassDayWise
        {
             //comcod, mlccod,  orderno, protdate,  protar, orderdesc, curcode, conrate, curdesc, mlcdesc
             public DateTime protdate { set; get; }

             public double protar { set; get; }
             public string mlcdesc { set; get; }
             public string orderdesc { set; get; }

             public string curdesc { set; get; }


             public EClassDayWise(DateTime protdate, double protar, string mlcdesc, string orderdesc, string curdesc)
            {
                this.protdate = protdate;
                this.protar = protar;
                this.mlcdesc = mlcdesc;
                this.orderdesc = orderdesc;
                this.curdesc = curdesc;
                
            }
        }

        [Serializable]

         public class EClassDayWiseProAct
         {
             //comcod, mlccod,  orderno, protdate,  protar, orderdesc, curcode, conrate, curdesc, mlcdesc
            public DateTime producdat { set; get; }

             public double proact { set; get; }
             public string mlcdesc { set; get; }
             public string orderdesc { set; get; }

             public string curdesc { set; get; }


             public EClassDayWiseProAct(DateTime producdat, double proact, string mlcdesc, string orderdesc, string curdesc)
             {
                 this.producdat = producdat;
                 this.proact = proact;
                 this.mlcdesc = mlcdesc;
                 this.orderdesc = orderdesc;
                 this.curdesc = curdesc;

             }
         }
       

    }
}
