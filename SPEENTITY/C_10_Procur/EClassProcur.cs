using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_10_Procur
{
    public class EClassProcur
    {
        #region CS Class 

        [Serializable]
        public class ComparativeStatementCreate
        {
            public string comcod { get; set; }
            public string ssircode { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public decimal rate { get; set; }
            public decimal stkqty { get; set; }
            public decimal areqty { get; set; }
            public decimal propqty { get; set; }
            public decimal csreqqty { get; set; }
            public decimal propqty1 { get; set; }
            public decimal amount { get; set; }
            public string supdesc { get; set; }
            public string rsirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public decimal istpurate { get; set; }
            public string payment { get; set; }
            public string cperson { get; set; }
            public string mobile { get; set; }
            public decimal leadtime { get; set; }
            public decimal auditrate { get; set; }
            public string paytype { get; set; }
            public decimal advamt { get; set; }

        }
        #endregion

        [Serializable]
        public class MaterialWiseStock
        {
            public string gp { get; set; }
            public string grp { get; set; }
            public string isuno { get; set; }
            public string isuno1 { get; set; }
            public string exdate { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string subcode { get; set; }
            public string subdesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string unit { get; set; }
            public double inqty { get; set; }
            public double outqty { get; set; }
            public double stock { get; set; }


        }

        [Serializable]
        public class MkrServay02
        {
            public string comcod { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc1 { get; set; }
            public string rsirunit { get; set; }
            public string spcfcod { get; set; }
            public double qty { get; set; }
            public double resrate1 { get; set; }
            public double resrate2 { get; set; }
            public double resrate3 { get; set; }
            public double resrate4 { get; set; }
            public double amt1 { get; set; }
            public double amt2 { get; set; }
            public double amt3 { get; set; }
            public double amt4 { get; set; }
            public string camt1 { get; set; }
            public string camt2 { get; set; }
            public string camt3 { get; set; }
            public string camt4 { get; set; }
            public string msrrmrk { get; set; }
            public double aprovrate { get; set; }
            public string spcfdesc { get; set; }
            public MkrServay02() { }
        }

        [Serializable]
        public class MkrServay03
        {
            //  a.comcod, a.ssircode,  a.discount, a.ccharge, a.payterm, ssirdesc=isnull(b.sirdesc, '')
            public string comcod { get; set; }
            public string ssircode { get; set; }
            public double discount { get; set; }
            public string ccharge { get; set; }
            public string payterm { get; set; }
            public string ssirdesc { get; set; }
            public MkrServay03() { }

        }

        [Serializable]
        public class SurveyInfo
        {
            public string comcod { get; set; }
            public string msrno { get; set; }
            public string ssircode { get; set; }
            public string rsircode { get; set; }
            public string matcode { get; set; }
            public string rsirdesc { get; set; }
            public string unit { get; set; }
            public string color { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string spcfdesc1 { get; set; }
            public string spcfdesc2 { get; set; }
            public string spcfcolordesc { get; set; }
            public double spcfdescl { get; set; }
            public double spcfdescw { get; set; }
            public double spcfdesch { get; set; }
            public double spcfdescsqm { get; set; }
            public string spcfsizedesc { get; set; }
            public double reqqty { get; set; }
            public double resrate { get; set; }
            public double reqamt { get; set; }
            public string msrrmrk { get; set; }
            public string curdesc { get; set; }
            public double amount { get; set; }
            public string selection { get; set; }
            public string mattype { get; set; }
            public string conunit { get; set; }
            public string conunitdesc { get; set; }
            public double conunitqty { get; set; }
            public string bomid { get; set; }
            public string article { get; set; }


        }

        [Serializable]
        public class VendorInfo
        {
            public string comcod { get; set; }
            public string ssircode { get; set; }
            public string msrno { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string paymod { get; set; }
            public string deltrm { get; set; }
            public string delmod { get; set; }
            public string paytype { get; set; }
            public string paylimit { get; set; }
            public string paymoddesc { get; set; }
            public string deltrmdesc { get; set; }
            public string delmoddesc { get; set; }
            public string paytypedesc { get; set; }
            public string paylimitdesc { get; set; }
            public string sirdesc { get; set; }
            public string supaddress { get; set; }
            public string vendtype { get; set; }
            public string portload { get; set; }
            public string portdisc { get; set; }
            public string yincoterms { get; set; }
            public string delleadt { get; set; }
            public string expdatedel { get; set; }
            public string expdatarri { get; set; }
            public string lcopbnk { get; set; }
            public string swiftcod { get; set; }
            public string reqno { get; set; }
            public string pactcode { get; set; }
            public string namecust { get; set; }
            public string prodstdate { get; set; }
            public string refno { get; set; }
            public DateTime cdate { get; set; }
            public string portloaddesc { get; set; }
            public string portdisdesc { get; set; }
            public string bankname { get; set; }
            public string swiftdesc { get; set; }

            public string email { get; set; }
            public string mobile { get; set; }
            public string cnperson { get; set; }
            public string supbin { get; set; }
            public string suploc { get; set; }
            public string supcurrency { get; set; }
            public string supcurdesc { get; set; }
            public string supcursubdesc { get; set; }
            public string cursymbol { get; set; }
            public string custadd { get; set; }
            public string custmob { get; set; }
            public string custemail { get; set; }
            public string incoterms { get; set; }
            public string incotermsdesc { get; set; }
            public string paymode { get; set; }
            public string paymodedesc { get; set; }
            public string pono { get; set; }
            public string poref { get; set; }
            public string shipmode { get; set; }
            public string shipmodedesc { get; set; }
            public string spnote { get; set; }
            public string vendor { get; set; }
            public string venadd { get; set; }
            public string venemail { get; set; }
            public string venconperson { get; set; }
            public string venphn { get; set; }
            public string remarks { get; set; }

        }

        [Serializable]
        public class ReqApproval
        {
            public string sigheadcom { get; set; }
            public string sigfincon { get; set; }
            public string sigheadbuz { get; set; }
            public string sigextmgt { get; set; }
        }

        [Serializable]
        public class EclassProwisePurchase
        {

            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string actcode { get; set; }
            public string r1 { get; set; }
            public string r2 { get; set; }
            public string r3 { get; set; }
            public string r4 { get; set; }
            public string r5 { get; set; }
            public string r6 { get; set; }
            public string r7 { get; set; }
            public string r8 { get; set; }
            public string r9 { get; set; }
            public string r10 { get; set; }
            public double tqty { get; set; }
            public double trate { get; set; }
            public double tamt { get; set; }
            public double q1 { get; set; }
            public double q2 { get; set; }
            public double q3 { get; set; }
            public double q4 { get; set; }
            public double q5 { get; set; }
            public double q6 { get; set; }
            public double q7 { get; set; }
            public double q8 { get; set; }
            public double q9 { get; set; }
            public double q10 { get; set; }
            public double a1 { get; set; }
            public double a2 { get; set; }
            public double a3 { get; set; }
            public double a4 { get; set; }
            public double a5 { get; set; }
            public double a6 { get; set; }
            public double a7 { get; set; }
            public double a8 { get; set; }
            public double a9 { get; set; }
            public double a10 { get; set; }
            public double u1 { get; set; }
            public double u2 { get; set; }
            public double u3 { get; set; }
            public double u4 { get; set; }
            public double u5 { get; set; }
            public double u6 { get; set; }
            public double u7 { get; set; }
            public double u8 { get; set; }
            public double u9 { get; set; }
            public double u10 { get; set; }

        }

        [Serializable]
        public class PromMatHistory
        {
            public string comcod { get; set; }
            public string issueno { get; set; }
            public string actcode { get; set; }
            public string rsircode { get; set; }
            public string rsirdesc { get; set; }
            public string unit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string deptcode { get; set; }
            public string deptname { get; set; }
            public string empid { get; set; }
            public string empname { get; set; }
            public string idcard { get; set; }
            public string terricode { get; set; }
            public string territory { get; set; }
            public DateTime issuedat { get; set; }
            public double issueqty { get; set; }
            public double issueamt { get; set; }
            public double rate { get; set; }
            public string vounum { get; set; }
            public string narr { get; set; }
            public string authstatus { get; set; }
            public string apstatus { get; set; }
            public string isAudited { get; set; }

        }

        [Serializable]
        public class SuppOutStndStatmnt
        {
            public string comcod { get; set; }
            public string rescode { get; set; }
            public double opnam { get; set; }
            public double trnam { get; set; }
            public double trnqty { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double purret { get; set; }
            public double netpur { get; set; }
            public double balamt { get; set; }
            public string resdesc { get; set; }
            public double payble { get; set; }
            public double payment { get; set; }
            public double curdue { get; set; }
            public double payment1 { get; set; }
            public double payment2 { get; set; }
            public double payment3 { get; set; }


        }

        [Serializable]
        public class IQCInspectionReport
        {
            public string comcod { get; set; }
            public string grrno { get; set; }
            public string rescod { get; set; }
            public string resdesc { get; set; }
            public string spcfdesc { get; set; }
            public string color { get; set; }
            public double rcvqty { get; set; }
            public double passqty { get; set; }
            public double qcqty { get; set; }
            public double rejqty { get; set; }
            public double rejprcnt { get; set; }
            public string unit { get; set; }
            public string qcstatus { get; set; }
            public string finding { get; set; }
            public string remarks { get; set; }
            public string chckdetails { get; set; }
            public string chalanno { get; set; }
            public string ssirdesc { get; set; }


        }

        [Serializable]
        public class PrjSummary
        {
            public string mrptcode { get; set; }
            public string rptcode { get; set; }
            public string rptunit { get; set; }
            public double qty { get; set; }
            public double amt { get; set; }
            public double rate { get; set; }
            public string rptdesc { get; set; }
            public string colst { get; set; }
            //public double  peramt { get; set; }

            public PrjSummary()
            {

            }
        }

        [Serializable]
        public class TermsInfo
        {
            public string termstitle { get; set; }
            public string termsdetails { get; set; }

            public TermsInfo()
            {

            }
        }
        public class HisMaterial
        {
            public string comcod { get; set; }
            public string mrrno { get; set; }
            public string mrrno1 { get; set; }

            public string mrrdat1 { get; set; }


            public string ssircode { get; set; }
            public string reqno { get; set; }
            public string reqno1 { get; set; }

            public double mrrqty { get; set; }
            public double mrrrat { get; set; }
            public double mrramt { get; set; }
            public string ssirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string pactdesc { get; set; }
            //public double  peramt { get; set; }

            public HisMaterial()
            {

            }
        }


        [Serializable]
        public class DayWisePurchase
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string genno { get; set; }
            public string genno1 { get; set; }
            public string refno { get; set; }
            public DateTime date01 { get; set; }
            public DateTime gdate { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string ssircode { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double amt { get; set; }
            public string usrid { get; set; }
            public string usrname { get; set; }
            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public string ssirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string reqtype { get; set; }
            public string actcode { get; set; }
        }

        [Serializable]
        public class MasterOReport
        {
            public string comcod { get; set; }
            public string orderno { get; set; }
            public string pono { get; set; }
            public string otype { get; set; }
            public DateTime orderdat { get; set; }
            public double ordrqty { get; set; }
            public double ordamt { get; set; }
            public DateTime expdeldat { get; set; }
            public string lcname { get; set; }
            public double lcvalue { get; set; }
            public string lcdate { get; set; }
            public string storename { get; set; }
            public string lcpaymentrms { get; set; }
            public string ssirdesc { get; set; }
            public string shiperdesc { get; set; }
            public string lastchln { get; set; }
            public DateTime lastchlndate { get; set; }
            public double ttlshipqty { get; set; }
            public DateTime lclastpay { get; set; }
            public double lcpayamt { get; set; }
            public double lclinkqty { get; set; }
            public double rcvqty { get; set; }
            public double qcqty { get; set; }
            public double costbalqty { get; set; }
            public string paymntstatus { get; set; }
            public string deltrms { get; set; }
            public string remarks { get; set; }
        }
    }
}