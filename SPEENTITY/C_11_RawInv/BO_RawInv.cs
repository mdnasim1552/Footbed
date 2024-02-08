using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_11_RawInv
{
    [Serializable]
    public class CentralStore
    {
        public string comcod { get; set; }
        public string actcode { get; set; }
        public string subcode { get; set; }
        public string actgrp { get; set; }
        public string actgrpdesc { get; set; }
        public string subgrp { get; set; }
        public string subgrpdesc { get; set; }
        public double opnqty { get; set; }
        public double opnrate { get; set; }
        public double opnam { get; set; }
        public double recqty { get; set; }
        public double recam { get; set; }
        public double recrate { get; set; }
        public double trnqty { get; set; }
        public double trnrate { get; set; }
        public double trnam { get; set; }
        public double matisqty { get; set; }
        public double matisamt {get; set;}
        public double stqty { get; set; }
        public double stcamt { get; set; }
        public double strate { get; set; }
        public string actdesc { get; set; }
        public string subdesc1 { get; set; }
        public string sirunit { get; set; }
        public string matcode { get; set; }
        public string spcfdesc { get; set; }
        public CentralStore(){}
        }
    [Serializable]
    public class MtrReqDetails
    {
        public string comcod { get; set; }
        public string mtreqno { get; set; }
        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public double qty { get; set; }
        public double rate { get; set; }
        public double amt { get; set; }
        public string resdesc { get; set; }
        public string sirunit { get; set; }
        public string spcfdesc { get; set; }
        public double balqty { get; set; }
        public string fmlc { get; set; }
        public string tmlc { get; set; }
        public string actcode { get; set; }
        public string forder { get; set; }
        public string torder { get; set; }
       
        public MtrReqDetails() { }
    }

    [Serializable]
    public class EclassMaterialIssue
    {
        public string preqno { get; set; }
        public string preqno1 { get; set; }
        public string bactcode { get; set; }
        public string bactdesc { get; set; }
        public string procode { get; set; }
        public string prodesc { get; set; }
        public string rsircode { get; set; }

        public string rsirdesc { get; set; }
        public string rsirunit { get; set; }
        public double isuqty { get; set; }
        public double balqty { get; set; }
        public double isurate { get; set; }
        public double stockqty { get; set; }
        public double balstkqty { get; set; }
        public double conqty { get; set; }
        public string conunt { get; set; }
        public string conuntdesc { get; set; }
        public string untcod { get; set; }
        public string spcfcod { get; set; }
        public string spcfdesc { get; set; }
        public double conspair { get; set; }


        // MatIssueStatusProps

        public string comcod { get; set; }
        public string deptcode { get; set; }
        public double stkqty { get; set; }
        public double stkrate { get; set; }
        public double issueqty { get; set; }
        public double issueamt { get; set; }
        public string scode { get; set; }
        public string deptname { get; set; }
        public string empid { get; set; }
        public string tdesc { get; set; }
        public string remarks { get; set; }

    }

    [Serializable]
    public class EclassResources
    {
        public string rsircode { set; get; }
        public string rsirdesc { set; get; }
        public string spcfcode { set; get; }
        public string spcfdesc { set; get; }

        public double resqty { set; get; }
        public double rate { set; get; }
        public string centrid { get; set; }
        public double amt { get; set; }
        public string batchcod { set; get; }
        public string batchdesc { set; get; }
        public string dayid { set; get; }


        public EclassResources()
        {
        }
        public EclassResources(string rsircode, string rsirdesc, string spcfcode, string spcfdesc, string centrid, double resqty, double rate, double amt)
        {
            this.rsircode = rsircode;
            this.rsirdesc = rsirdesc;
            this.spcfcode = spcfcode;
            this.spcfdesc = spcfdesc;
            this.centrid = centrid;
            this.resqty = resqty;
            this.rate = rate;
            this.amt = amt;
        }
        public EclassResources(string batchcod, string batchdesc, string dayid, string rsircode, string rsirdesc, string spcfcode, string spcfdesc, double resqty, double rate, double amt)
        {
            this.batchcod = batchcod;
            this.batchdesc = batchdesc;
            this.dayid = dayid;
            this.rsircode = rsircode;
            this.rsirdesc = rsirdesc;
            this.spcfcode = spcfcode;
            this.spcfdesc = spcfdesc;
            this.centrid = centrid;
            this.resqty = resqty;
            this.rate = rate;
            this.amt = amt;
        }
    }
    
    
    [Serializable]
    public class StockAdjList
    {
        public string adjno { get; set; }
        public string adjno1 { get; set; }
        public string monthid { get; set; }
        public string monthdesc { get; set; }
        public DateTime adjdate { get; set; }
        public string centrid { get; set; }
        public string centrdesc { get; set; }
        public string batchcod { get; set; }
        public string batchdesc { get; set; }
        public double adjqty { get; set; }
        public double amt { get; set; }
        public double itmcount { get; set; }
        public string status { get; set; }

    }
    

    [Serializable]
    public class RptInvQtyBasis
    {
        public string comcod { get; set; }
        public string actcode { get; set; }
        public string subcode { get; set; }
        public string spcfcode { get; set; }
        public double opnqty { get; set; }
        public double opnrate { get; set; }
        public double opnam { get; set; }
        public double recqty { get; set; }
        public double recrate { get; set; }
        public double recam { get; set; }
        public double trninqty { get; set; }
        public double trninrat { get; set; }
        public double trninam { get; set; }
        public double trnqty { get; set; }
        public double trnrate { get; set; }
        public double trnam { get; set; }
        public double matisqty { get; set; }
        public double matisam { get; set; }
        public double lqty { get; set; }
        public double lrate { get; set; }
        public double lamt { get; set; }
        public double sqty { get; set; }
        public double srate { get; set; }
        public double samt { get; set; }
        public double dqty { get; set; }
        public double drate { get; set; }
        public double damt { get; set; }
        public double stqty { get; set; }
        public double strate { get; set; }
        public double stcamt { get; set; }
        public double pnedqty { get; set; }
        public double pendwithstkqty { get; set; }
        public string actdesc { get; set; }
        public string subdesc1 { get; set; }
        public string sirunit { get; set; }
        public string spcfdesc { get; set; }
        public string matcode { get; set; }
    }


    [Serializable]
    public class RptMatTransfer
    {
        public string comcod { get; set; }
        public string mtreqno { get; set; }
        public string mtreqno1 { get; set; }
        public string orderno { get; set; }
        public string mtrref { get; set; }
        public string mtrdat { get; set; }
        public string tfpactcode { get; set; }
        public string ttpactcode { get; set; }
        public string rsircode { get; set; }
        public string spcfcod { get; set; }
        public double mtrfqty { get; set; }
        public double balqty { get; set; }
        public string tfactdesc { get; set; }
        public string ttactdesc { get; set; }
        public double qty { get; set; }
        public double gqty { get; set; }
        public double tqty { get; set; }
        public double trbalqty { get; set; }
        public double itmcount { get; set; }
        public double mtrfrat { get; set; }
        public double mtrfamt { get; set; }
        public double getpqty { get; set; }
        public double getpamt { get; set; }
        public string tfpactdesc { get; set; }
        public string ttpactdesc { get; set; }
        public string rsirdesc { get; set; }
        public string rsirunit { get; set; }
        public string spcfdesc { get; set; }
        public string textfield { get; set; }
        public string color { get; set; }
        public string valuefiled { get; set; }
        public string forder { get; set; }
        public string torder { get; set; }
        public string supdesc { get; set; }
        public string actcode { get; set; }
        public string fmlc { get; set; }
        public string tmlc { get; set; }
    }
}
