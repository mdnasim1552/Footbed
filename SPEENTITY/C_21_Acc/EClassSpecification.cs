using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPEENTITY.C_21_Acc
{
    [Serializable]
    public class EClassSpecification
    {

        public string sircode { get; set; }
        public string spcfcod { get; set; }
        public string spcfcod2 { get; set; }
        public string spcfcod3 { get; set; } 
        public string spcfcod4 { get; set; }
        public string sirtdes { get; set; }
        public string spcfdesc { get; set; }
        public double sirval { get; set; }
        public string desc1 { get; set; }
        public string desc2 { get; set; }
        public string desc3 { get; set; }
        public string desc4 { get; set; }
        public string desc5 { get; set; }
        public string unit { get; set; }
        public string incoterms { get; set; }
        public string incotermsdesc { get; set; }
        public double mark { get; set; }
        public double allowance { get; set; }
        public bool sizeble { get; set; }
        public bool convertible { get; set; }
        public double stdrate { get; set; }
        public string photo { get; set; }

        public EClassSpecification() { }
        public EClassSpecification(string spcfcod, string spcfcod2, string spcfcod3, string spcfcod4, string sirtdes, string spcfdesc, string desc1, string desc2, 
            string desc3, string desc4, string desc5, string unit, string incoterms, string incotermsdesc, double mark, double allowance, bool sizeble,
            bool convertible, double stdrate, string photo)
        {
            this.spcfcod = spcfcod;
            this.spcfcod2 = spcfcod2;
            this.spcfcod3 = spcfcod3;
            this.spcfcod4 = spcfcod4;
            this.sirtdes = sirtdes;
            this.spcfdesc = spcfdesc;
            this.desc1 = desc1;
            this.desc2 = desc2;
            this.desc3 = desc3;
            this.desc4 = desc4;
            this.desc5 = desc5;
            this.unit = unit;
            this.incoterms = incoterms;
            this.incotermsdesc = incotermsdesc;
            this.mark = mark;
            this.allowance = allowance;
            this.sizeble = sizeble;
            this.convertible = convertible;
            this.stdrate = stdrate;
            this.photo = photo;
        }

        public EClassSpecification( string sircode, string spcfcod, string spcfdesc, string desc1, string desc2,
           string desc3, string desc4, string desc5)
        {
            this.sircode = sircode;
            this.spcfcod = spcfcod;
            this.spcfdesc = spcfdesc;
            this.desc1 = desc1;
            this.desc2 = desc2;
            this.desc3 = desc3;
            this.desc4 = desc4;
            this.desc5 = desc5;
   
          
        }
        
        public EClassSpecification( string sircode, string spcfcod, string spcfdesc, double sirval, string desc1, string desc2,
           string desc3, string desc4, string desc5)
        {
            this.sircode = sircode;
            this.spcfcod = spcfcod;
            this.spcfdesc = spcfdesc;
            this.sirval = sirval;
            this.desc1 = desc1;
            this.desc2 = desc2;
            this.desc3 = desc3;
            this.desc4 = desc4;
            this.desc5 = desc5;
 
          
        }

    }
}
