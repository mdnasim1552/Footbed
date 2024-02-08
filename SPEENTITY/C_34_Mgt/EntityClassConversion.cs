using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPEENTITY.C_34_Mgt
{


    [Serializable]
     public class EntityClassConversion
    {
        public string fcode { get; set; }
        public string tcode { get; set; }
        public string fcodedesc { get; set; }
        public string tcodedesc { get; set; }
        public double conrate { get; set; }
        public double conrate1 { get; set; }

        public EntityClassConversion(string fcode, string tcode, string fcodedesc, string tcodedesc, double conrate, double conrate1) 
        {
            this.fcode = fcode;
            this.tcode = tcode;
            this.fcodedesc = fcodedesc;
            this.tcodedesc = tcodedesc;
            this.conrate = conrate;
            this.conrate1 = conrate1;
        
        }

       

    }

    [Serializable]
    public class EclassDocinformation
    {
        public string docno { get; set; }
        public string ssircode { get; set; }
        public string ssirdesc { get; set; }
        public string curcode { get; set; }
        public string currency { get; set; }
        public string refno { get; set; }
        public DateTime ordrdat { get; set; }
        public DateTime deldat { get; set; }
        public DateTime docdat { get; set; }
        public string remarks { get; set; }
        public string supcod { get; set; }
        public string saddress { get; set; }

        public double ordval { get; set; }

    }
    [Serializable]
    public class EclassDocFilesInformation
    {
        public string docno { get; set; }
        public string gcod { get; set; }
        public string gdesc { get; set; }
        public DateTime rowdat { get; set; }
        public string postedbyid { get; set; }

        public string usrname { get; set; }

    }
    [Serializable]
    public class EclassDocNotes
    {
        public string gcod { get; set; }
        public string slnum { get; set; }
        public string usrcode { get; set; }
        public string aprvdat  { get; set; }
        public string username { get; set; }
        public string comments { get; set; }
        public DateTime rowdat { get; set; }
        public string digsig { get; set; }
        public string designation { get; set; }
    }

    [Serializable]
    public class GetCompany
    {
        public string comcod { get; set; }
        public string comname { get; set; }
        public string usrid { get; set; }
        public string usrsname { get; set; }
        public string usrname { get; set; }
        public string usrdesig { get; set; }
        public string usrpass { get; set; }
        public string usrrmrk { get; set; }
        public string empid { get; set; }
        public string urole { get; set; }
        public string usractive { get; set; }

        public GetCompany()
        {

        }
        public GetCompany(string comcod, string comname, string usrid, string usrsname, string usrname, string usrdesig, string usrpass, string usrrmrk, string empid, string urole, string usractive)
        {
            this.comcod = comcod;
            this.comname = comname;
            this.usrid = usrid;
            this.usrsname = usrsname;
            this.usrname = usrname;
            this.usrdesig = usrdesig;
            this.usrpass = usrpass;
            this.usrrmrk = usrrmrk;
            this.empid = empid;
            this.urole = urole;
            this.usractive = usractive;

        }
    }
}


