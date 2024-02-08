using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_04_Sampling
{
   public class EClassPrototypeSampling
    {
        [Serializable]

        public class PrototypeSampling
        {
            public string lformacod { get; set; }
            public string article { get; set; }
            public string catagory { get; set; }
            public string shoetype { get; set; }
            public string season { get; set; }
            public string images { get; set; }
            public string agentcod { get; set; }
            public string samsize { get; set; }
            public string comsize { get; set; }
            public double samqty { get; set; }
            public string sizerange { get; set; }
            public string remarks { get; set; }
            public string upmaterial { get; set; }
            public string limaterial { get; set; }
            public string skmaterial { get; set; }
            public string osmaterial { get; set; }
            public string accessories { get; set; }
            public string lformadesc { get; set; }
            public string catagorydesc { get; set; }
            public string shoetdesc { get; set; }
            public string seasondesc { get; set; }
            public string agent { get; set; }
            public string buyer { get; set; }
            public string buyerdesc { get; set; }
            public string conscode { get; set; }
            public string construction { get; set; }

            public string samptype { get; set; }
            public string samtypedesc { get; set; }

            public DateTime deldate { get; set; }

            public string brand { get; set; }
            public string branddesc { get; set; }

            public string unit { get; set; }
            public string unitdesc { get; set; }

            public string batchcode { get; set; }
            public string batchdesc { get; set; }
            public string color { get; set; }
            public string colordesc { get; set; }
            public string conordno { get; set; }
            public string sversion { get; set; }
            public PrototypeSampling()
            { 
            
            
            }


            public PrototypeSampling(string  lformacod, string article, string catagory, string shoetype, string season, string images, string agentcod, string samsize, string comsize,
                         string sizerange, double samqty, string remarks, string upmaterial, string limaterial, string skmaterial, string osmaterial, string accessories, string lformadesc, 
                         string catagorydesc, string shoetdesc, string seasondesc, string agent, string conordno, string sversion)
            {


                this.lformacod = lformacod;
                this.article = article;
                this.catagory = catagory;
                this.shoetype = shoetype;
                this.season = season;
                this.images = images;
                this.agentcod = agentcod;
                this.samsize = samsize;
                this.comsize = comsize;
                this.sizerange = sizerange;
                this.samqty = samqty;
                this.remarks = remarks;
                this.upmaterial = upmaterial;
                this.limaterial = limaterial;
                this.skmaterial = skmaterial;
                this.osmaterial = osmaterial;
                this.accessories = accessories;
                this.lformadesc = lformadesc;
                this.catagorydesc = catagorydesc;
                this.shoetdesc = shoetdesc;
                this.seasondesc = seasondesc;
                this.agent = agent;
                this.conordno = conordno;
                this.sversion = sversion;
            }




        }

        [Serializable]
        public class PrototypeSamProduct
        {
            public int sampleid { get; set; }
            public string grpid { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string msircode { get; set; }
            public string spcfcod { get; set; }
            public string msirdesc { get; set; }
            public string msirunit { get; set; }

            public PrototypeSamProduct()
            {


            }
            public PrototypeSamProduct(int sampleid, string grpid, string grp, string grpdesc, string msircode, string spcfcod, string msirdesc, string msirunit)
            {

                this.sampleid = sampleid;
                this.grpid = grpid;
                this.grp = grp;
                this.grpdesc = grpdesc;
                this.msircode = msircode;
                this.spcfcod = spcfcod;
                this.msirdesc = msirdesc;
                this.msirunit = msirunit;
            }

        }




        [Serializable]
        public class EClassGenCode
        {
            public string gcod { get; set; }
            public string gdesc { get; set; }
            
            public EClassGenCode()
            {


            }
            public EClassGenCode(string gcod, string gdesc)
            {

                this.gcod = gcod;
                this.gdesc = gdesc;
               
            }

        }

        [Serializable]
        public class EClassGroup
        {
            public string Id { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
           

            public EClassGroup()
            {


            }
            public EClassGroup(string Id,  string grp, string grpdesc)
            {
                this.Id = Id;
                this.grp = grp;
                this.grpdesc = grpdesc;
                

            }

        }



        [Serializable]
        public class EClassMaterial
        {
            public string sircode { get; set; }
            public string sirdesc { get; set; }
            public string sirunit { get; set; }

            public EClassMaterial()
            {


            }
            public EClassMaterial(string sircode, string sirdesc, string sirunit)
            {

                this.sircode = sircode;
                this.sirdesc = sirdesc;
                this.sirunit = sirunit;

            }

        }
        
        [Serializable]
        public class EClassPdBook
        {
            public string comcod { get; set; }
            public string season { get; set; }
            public string seasonnam { get; set; }
            public string gdesc { get; set; }
            public string article { get; set; }
            public string lformacod { get; set; }
            public string lformadesc { get; set; }
            public string buyerdesc { get; set; }
            public string buyer { get; set; }
            public string sizerange { get; set; }
            public string samsize { get; set; }
            public string comsize { get; set; }
            public string designer { get; set; }
            public string dsgnrnam { get; set; }
            public string pattloc { get; set; }
            public string pattgrad { get; set; }
            public string uppknif { get; set; }
            public string linknif { get; set; }
            public string botknif { get; set; }
            public string outsole { get; set; }
            public string remarks { get; set; }
           

        }

        [Serializable]
        public class EClassKnifEntry
        {
            public string comcod { get; set; }
            public string article { get; set; }
            public string compcode { get; set; }
            public string compname { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string sizes { get; set; }
            public double qty { get; set; }
            public double conqty { get; set; }
            public double inch { get; set; }
            public string remarks { get; set; }
        }

        [Serializable]
        public class EClassSamReport
        {
            public string comcod { get; set; }
            public string inqno { get; set; }
            public string samptype { get; set; }
            public string samptypdesc { get; set; }
            public string images { get; set; }
            public DateTime inqdat { get; set; }
            public DateTime deldate { get; set; }
            public string article { get; set; }
            public string catagory { get; set; }
            public string categorydesc { get; set; }
            public string agentcod { get; set; }
            public string agentdesc { get; set; }
            public string buyer { get; set; }
            public string buyerdesc { get; set; }
            public double samqty { get; set; }
            public string shoetype { get; set; }
            public string shoetypdesc { get; set; }
            public string brand { get; set; }
            public string brandesc { get; set; }
            public string color { get; set; }
            public string colordesc { get; set; }
            public string season { get; set; }
            public string seasondesc { get; set; }
            public string constuction { get; set; }
            public string constuctiondesc { get; set; }
            public string samsize { get; set; }
            public string sizerange { get; set; }
            public double fgrcvqty { get; set; }
            public double shipqty { get; set; }

        }

        [Serializable]
        public class SamTagPrint
        {
            public string comcod { get; set; }
            public string inqno1 { get; set; }
            public string inqno { get; set; }
            public string article { get; set; }
            public string cust { get; set; }
            public string color { get; set; }
            public string ordno { get; set; }
            public string samlast { get; set; }
            public string lining { get; set; }
            public string samupper { get; set; }
            public string socks { get; set; }
            public string finishing { get; set; }
            public string stitch { get; set; }
            public string lace { get; set; }
            public string sole { get; set; }
            public string samsize { get; set; }
            public DateTime inqdat { get; set; }
        }

        [Serializable]
        public class PackList
        {
            public string comcod { get; set; }
            public string inqno1 { get; set; }
            public string inqno { get; set; }
            public string article { get; set; }
            public string cust { get; set; }
            public string color { get; set; }
            public string ordno { get; set; }
            public string samlast { get; set; }
            public string lining { get; set; }
            public string samupper { get; set; }
            public string socks { get; set; }
            public string finishing { get; set; }
            public string stitch { get; set; }
            public string lace { get; set; }
            public string sole { get; set; }
            public string samsize { get; set; }
            public DateTime inqdat { get; set; }
            public double samqty { get; set; }
            public string gdesc { get; set; }
        }


        [Serializable]
        public class RptSamProdReq1
        {
            public string comcod { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string untcod { get; set; }
            public string rsirunit { get; set; }
            public double samqty { get; set; }
            public double ttlconqty { get; set; }
            public double conamt { get; set; }
            public double rate { get; set; }
            public double stockqty { get; set; }
        }
        

        [Serializable]
        public class RptSamProdReq2
        {
            public string inqno { get; set; }
            public string inqno1 { get; set; }
            public double samqty { get; set; }
            public string samsize { get; set; }
            public double fgrcvqty { get; set; }
            public string unit { get; set; }
            public string unitdesc { get; set; }
            public string colordesc { get; set; }
            public string buyer { get; set; }
            public string buyerdesc { get; set; }
            public string samptype { get; set; }
            public string samptypedesc { get; set; }
        }

        [Serializable]
        public class EclassSizeDetails
        {
            public string sizeid { get; set; }
            public string sizedesc { get; set; }
            public string sizeselect { get; set; }
        }

    }
}
