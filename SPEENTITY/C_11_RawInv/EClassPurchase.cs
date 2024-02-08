using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_11_RawInv
{
    public class EClassPurchase
    {
        [Serializable]
        public class RptRequisition
        {
            public string scode { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string rsirdesc1 { get; set; }
            public string rsirdesc2 { get; set; }
            public string rsirdesc3 { get; set; }
            public string spcfdesc { get; set; }
            public string rsirunit { get; set; }
            public string reqnote { get; set; }
            public double stkqty { get; set; }
            public double preqty { get; set; }
            public double areqty { get; set; }
            public double reqrat { get; set; }
            public double lpurrate { get; set; }
            public double areqamt { get; set; }
            public double preqamt { get; set; }
            public string ptype { get; set; }

            public string ptyped { get; set; }
            public string budget { get; set; }
            public string pkgsize { get; set; }
            public string budgetdesc { get; set; }
            public string desc1 { get; set; }
            public string desc2 { get; set; }
            public string desc3 { get; set; }
            public string desc4 { get; set; }
            public string bomid { get; set; }
            public string mlcdesc { get; set; }

        }
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
            public string ssircode { get; set; }
            public string ssirdesc { get; set; }



        }
         [Serializable]
        public class PurchaseMRR
        {
             public string rsircode { get; set; }
             public string spcfcod { get; set; }
             public string rsirdesc1 { get; set; }
             public string spcfdesc { get; set; }
             public string rsirunit { get; set; }
             public string bomid { get; set; }
             public double mrrqty { get; set; }
             public double mrrrate { get; set; }
             public double mrramt { get; set; }
             public string mrrnote { get; set; }
             public double chlnqty { get; set; }
             public string reqno1 { get; set; }
             public string size { get; set; }
             public string color { get; set; }
             public double orderqty { get; set; }
             public PurchaseMRR() { }
             public PurchaseMRR(string rsircode, string spcfcod, string rsirdesc1, string spcfdesc, string rsirunit, string bomid, double mrrrate, 
                                double mrramt, string mrrnote, string reqno1, string size, string color, double mrrqty, double chlnqty, double orderqty) 
             {
                this.rsircode = rsircode;
                this.spcfcod = spcfcod;
                this.rsirdesc1 = rsirdesc1;
                this.spcfdesc = spcfdesc;
                this.rsirunit = rsirunit;
                this.bomid = bomid;
                this.mrrrate = mrrrate;
                this.mrramt = mrramt;
                this.mrrnote = mrrnote;
                this.reqno1 = reqno1;
                this.size = size;
                this.color = color;
                this.mrrqty = mrrqty;
                this.chlnqty = chlnqty;
                this.orderqty = orderqty;
            }

        }
        
        [Serializable]
        public class MaterialTransferReq
        {


            public string sirdesc { get; set; }
            public string sirunit { get; set; }
            public double qty { get; set; }
            public string resdesc { get; set; }
            public string spcfdesc { get; set; }


        }
        [Serializable]
        public class Gatepass
        {


            public string rsirdesc { get; set; }
            public string rsirunit { get; set; }
            public double getpqty { get; set; }
            public string resdesc { get; set; }
            public string spcfdesc { get; set; }


        }
        [Serializable]
        public class EClassPurchaseOrdr
        {

            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comcod { get; set; }
            public string reqno { get; set; }
            public string mrfno { get; set; }
            public string rsircode { get; set; }
            public string spcfcod { get; set; }
            public string rsirdesc { get; set; }
            public string spcfdesc { get; set; }
            public string spcfcolordesc { get; set; }
            public string spcfsizedesc { get; set; }
            public string spcfdesc1 { get; set; }
            public string spcfdesc2 { get; set; }
            public string spdesc { get; set; }
            public string rsirunit { get; set; }
            public double ordqty { get; set; }
            public double ordrqty { get; set; }
            public double aprovrate { get; set; }
            public double ordramt { get; set; }
            public double ordamt { get; set; }
            public string pactcode { get; set; }
            public string pactdesc { get; set; }
            public string proadd { get; set; }
            public string selection { get; set; }
            public string mattype { get; set; }
            
        }

        [Serializable]
        public class PurchaseOrderTerms
        {
            public string comcod { get; set; }
            public string orderno { get; set; }
            public string termssubj { get; set; }
            public string termsdesc { get; set; }
            public string termsmrk { get; set; }
        }

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
        
        public class MatUnused
        {
            public string comcod { get; set; }
            public string sircode { get; set; }
            public string sirdesc { get; set; }
            public string sirunit { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public double stqty { get; set; }
            public double stcamt { get; set; }
            public double strate { get; set; }
            public DateTime lastuse { get; set; }
            public DateTime lastpurchase { get; set; }
            public string nuseday { get; set; }
            

        }
    }


    







}
