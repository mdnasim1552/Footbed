using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_34_Mgt
{
    [Serializable]
    public class  EClassSalPurAcc
    {
        public string yearmon {set;get;}
        public string yearmon1 { set; get; }
        public double ttlsalamt { set; get; }
        public double collamt { set; get; }
        public double ttlpuramt { set; get; }
        public double tpayamt { set; get; }
   
   
        public double dram { set; get; }
        public double cram { set; get; }

        public EClassSalPurAcc()
        {
        }


        public EClassSalPurAcc(string yearmon, string yearmon1, double ttlsalamt, double collamt, double ttlpuramt, double tpayamt, double dram, double cram)
            {
                this.yearmon = yearmon;
                this.yearmon1 = yearmon1;
                this.ttlsalamt = ttlsalamt;
                this.collamt = collamt;
                this.collamt = collamt;
                this.ttlpuramt = ttlpuramt;
                this.tpayamt = tpayamt;
                this.dram = dram;
                this.cram = cram;
              
            }



    }

   [Serializable]
   public class EclassUnitConversion
    {
        public string bcod { get; set; }
        public string bcodesc { get; set; }
        public string ccod { get; set; }
        public string ccodesc { get; set; }
        public double conrat { get; set; }
        public string remarks { get; set; }
        public EclassUnitConversion() { }
        public EclassUnitConversion(string bcod, string bcodesc, string ccod, string ccodesc, double conrat, string remarks)
        {
            this.bcod = bcod;
            this.bcodesc = bcodesc;
            this.ccod = ccod;
            this.ccodesc = ccodesc;
            this.conrat = conrat;
            this.remarks = remarks;
        }

    }
    [Serializable]
    public class EclassGroupChat
    {
        public string chatno { get; set; }
        public string chtname { get; set; }
        public string concern { get; set; }
        public string postedname { get; set; }
        public string postedbyid { get; set; }
        public string posteddat { get; set; }
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public string probledat { get; set; }
        public string taskcod { get; set; }
        public string taskname { get; set; }
        public string asinuser { get; set; }
    }
    [Serializable]
    public class EclassChatMSG
    {
        public string chatno { get; set; }
        public string message { get; set; }
        public string postedname { get; set; }
        public string postedbyid { get; set; }
        public string posteddat { get; set; }
        public string mestatus { get; set; }
        public string pday { get; set; }
        public string ptime { get; set; }
        public Boolean files { get; set; }
    }

    [Serializable]
    public class Grouptaskchat
    {
        public string comcod { get; set; }
        public string chatno { get; set; }
        public string userid { get; set; }
        public string postedname { get; set; }
        public string userimg { get; set; }
        public string usermail { get; set; }
        public string umobile { get; set; }
        public string userjoinind { get; set; }
        public string udegn { get; set; }
        public string empid { get; set; }
        public string taskname { get; set; }
        public string actcode { get; set; }
        public string actdesc { get; set; }
        public string taskcod { get; set; }
        public string fmessage { get; set; }
        public string cstatus { get; set; }
        public DateTime probledat { get; set; }
        public string clstatus { get; set; }
        public string chtname { get; set; }
        public string postedbyid { get; set; }
        public string asinuser { get; set; }
        public string asinnamne { get; set; }
        public Grouptaskchat() { }

    //    comcod, chatno, userid, postedname, userimg, usermail, empid, umobile, userjoinind, udegn, actcode, actdesc, taskcod, 
    //fmessage,cstatus, taskname,probledat, clstatus, asinuser, asinnamne, chtname, postedbyid, posteddat 
    }

    [Serializable]
    public class Grouptaskuser
    {
     //   comcod, userid,  postedname, usermail, umobile, userjoinind, udegn,  userimg
        public string comcod { get; set; }
        public string userid { get; set; }
        public string postedname { get; set; }
        public string usermail { get; set; }
        public string umobile { get; set; }
        public string userjoinind { get; set; }
        public string udegn { get; set; }
        public string userimg { get; set; }
        public Grouptaskuser() { }
    }
}
