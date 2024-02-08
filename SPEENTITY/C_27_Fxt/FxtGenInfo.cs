using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_27_Fxt
{
     public class FxtGenInfo
    {
         // comcod,gcod,gdesc, gph, fxtgcod, fxtgval,  fxtsircod, fxtgdesc, gdesc1

         public string comcod { get; set; }
         public string gcod { get; set; }
         public string gdesc { get; set; }
         public string gph { get; set; }
         public string fxtgcod { get; set; }
         public string fxtgval { get; set; }
         public string fxtsircod { get; set; }
         public string fxtgdesc { get; set; }
         public string gdesc1 { get; set; }
    }
    [Serializable]
    public class FxtAstDetailsinf
    {
        public string comcod { get; set; }
        public string pactcode { get; set; }
        public string dept { get; set; }
        public string floorno { get; set; }
        public string modelno { get; set; }
        public double qty { get; set; }
        public string assetid { get; set; }
        public string rsircode { get; set; }
        public string slno { get; set; }
        public string empid { get; set; }
        public string dgcod { get; set; }
        public double prate { get; set; }
        public DateTime purdat { get; set; }
        public double pvalu { get; set; }
        public double bookval { get; set; }
        public double depreamt { get; set; }
        public double salval { get; set; }
        public string assetnam { get; set; }
        public string punit { get; set; }
        public string empname { get; set; }
        public string desig { get; set; }
        public string brlastcode { get; set; }
        public string astdesc { get; set; }
        public string mfgserial { get; set; }
        public string capacity { get; set; }
        public string suppname { get; set; }
        public string supcountry { get; set; }
        public string majcat { get; set; }
        public string mincat { get; set; }
        public string location { get; set; }
        public string section { get; set; }
        public string asstlife { get; set; }
        public string room { get; set; }
        public double addvalue { get; set; }
        public double totalcst { get; set; }
        public double accdep { get; set; }
        public double depytd { get; set; }
        public double downvalue { get; set; }
        

    }
    [Serializable]
    public class ACCWISECURDEP
    {
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public DateTime voudat { get; set; }
        public double opndram { get; set; }
        public double opncram { get; set; }
        public double dram { get; set; }
        public double cram { get; set; }
        public double curam { get; set; }
        public double clsam { get; set; }
        public string today { get; set; }
        public double curpdep { get; set; }
        public double rate { get; set; }
        public DateTime asdate { get; set; }
    }

    [Serializable]
    public class RtpeFixAssetsSchu
    {
        public string actcode { get; set; }
        public string actdesc { get; set; }        
        public double opnam { get; set; }
        public double curam { get; set; }
        public double saleam { get; set; }
        public double disam { get; set; }
        public double toam { get; set; }
        public double dcharge { get; set; }
        public double opndep { get; set; }
        public double adjam { get; set; }
        public double curdepop { get; set; }
        public double curpdepcur { get; set; }
        public double todep { get; set; }
        public double clsam { get; set; }
    }

    [Serializable]
    public class FixedAssetRpt
    {
        public string comcod { get; set; }
        public string dept { get; set; }
        public string sirdesc1 { get; set; }
        public string assetnam { get; set; }
        public string assetid { get; set; }
        public string modelno { get; set; }
        public string punit { get; set; }
        public DateTime purdat { get; set; }
        public double qty { get; set; }
        public double prate { get; set; }
        public double pvalu { get; set; }
        public double depreamt { get; set; }
        public double salval { get; set; }
        public double bookval { get; set; }
        public string empname { get; set; }
        public string desig { get; set; }
        public string remarks { get; set; }

    }

}
