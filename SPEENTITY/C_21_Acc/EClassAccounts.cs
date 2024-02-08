using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_21_Acc
{
    public class EClassAccounts
    {
        #region Accounts Code (Ontime)

        [Serializable]
        public class EClassAccCode
        {


            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string actelev { get; set; }
            public string acttype { get; set; }
            public string acttdesc { get; set; }
            public string userdesc { get; set; }






            public EClassAccCode()
            {

            }

            public EClassAccCode(string actcode, string actdesc, string actelev, string acttype, string acttdesc, string userdesc)
            {
                this.actcode = actcode;
                this.actdesc = actdesc;
                this.actelev = actelev;
                this.acttype = acttype;
                this.acttdesc = acttdesc;
                this.userdesc = userdesc;

            }
        }




        [Serializable]
        public class EClassSupplierBill
        {


            public string actcode { get; set; }
            public string subcode { get; set; }
            public string actdesc { get; set; }
            public string subdesc { get; set; }
            public double trnqty { get; set; }
            public double trnrate { get; set; }
            public double trndram { get; set; }
            public double trncram { get; set; }







            public EClassSupplierBill()
            {

            }

            public EClassSupplierBill(string actcode, string subcode, string actdesc, string subdesc, double trnqty, double trnrate, double trndram, double trncram)
            {
                this.actcode = actcode;
                this.subcode = subcode;
                this.actdesc = actdesc;
                this.subdesc = subdesc;
                this.trnqty = trnqty;
                this.trnrate = trnrate;
                this.trndram = trndram;
                this.trncram = trncram;

            }
        }

        [Serializable]
        public class GeneralAdminOverH
        {
            public string comcod { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public double curam { get; set; }
            public double opnam { get; set; }
            public double closam { get; set; }
            public double percentcu { get; set; }
            public double cpercent { get; set; }
            public GeneralAdminOverH() { }
        }

        [Serializable]
        public class Rptspbalancesheet
        {
            public string comcod { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string comnam { get; set; }
            public string actcode4 { get; set; }
            public string actdesc4 { get; set; }
            public double opnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double curam { get; set; }
            public double closam { get; set; }
            public string mainhead { get; set; }


            public Rptspbalancesheet() { }

        }

        [Serializable]
        public class ListOfIssuedCheque
        {
            public DateTime voudat { get; set; }
            public string cheqeno { get; set; }
            public string vounum1 { get; set; }
            public double trnamt { get; set; }
            public string bankname { get; set; }
            public string partyname { get; set; }
            public string varnar { get; set; }
            public string pmode { get; set; }
            public string recndt { get; set; }

        }

        [Serializable]
        public class NoteIncoStatement
        {
            public string comcod { get; set; }
            public string actcod4 { get; set; }
            public string actdesc4 { get; set; }
            public double curam { get; set; }
            public double opnam { get; set; }
            public double closam { get; set; }
            public double percentcu { get; set; }
            public double cpercent { get; set; }
            public NoteIncoStatement() { }

        }

        #endregion
        public class accLedger
        {
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public DateTime voudat { get; set; }
            public string voudat1 { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }

            public string trnrmrk { get; set; }
            public string refnum { get; set; }
            public string srinfo { get; set; }
            public string venar { get; set; }
            public string voutype { get; set; }

            public double opam { get; set; }
            public double trnam { get; set; }
            public double trnqty { get; set; }
            public double trnrate { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double clsam { get; set; }


        }


        [Serializable]
        public class ResCodeBook
        {
            public string sircode { get; set; }
            public string sircode2 { get; set; }
            public string sircode3 { get; set; }
            public string sircode4 { get; set; }
            public string sirdesc { get; set; }
            public string sirtype { get; set; }
            public string sirtdes { get; set; }
            public string sirunit { get; set; }
            public double sirval { get; set; }
            public string usercode { get; set; }
            public string userdesc { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string spcfcod { get; set; }
            public string size { get; set; }
            public string thickness { get; set; }
            public string color { get; set; }
            public string brand { get; set; }
            public string other { get; set; }
            public double Allowance { get; set; }
            public string fullname { get; set; }
            public double mark { get; set; }
            public string sizeble { get; set; }

        }


        [Serializable]
        public class Unit
        {
            public string comcod { get; set; }
            public string gcod { get; set; }
            public string gdesc { get; set; }
            public string orderby { get; set; }
        }


        [Serializable]
        public class EclassOverallBalance
        {
            public string cactcode { get; set; }
            public string cactdesc { get; set; }
            public double trnam { get; set; }

        }


        [Serializable]
        public class AccOpening
        {
            public string comcod { get; set; }
            public string actcode { get; set; }
            public string actdesc { get; set; }
            public string actelev { get; set; }
            public double Dr { get; set; }
            public double Cr { get; set; }
            //public string acttype { get; set; }
            public AccOpening() { }
        }

        [Serializable]
        public class AccOpLevel2
        {
            public string comcod { get; set; }
            public string rescode { get; set; }
            public string resdesc { get; set; }
            public string spcfcod { get; set; }
            public string spcfdesc { get; set; }
            public string resunit { get; set; }
            public double qty { get; set; }
            public double rate { get; set; }
            public double Dr { get; set; }
            public double Cr { get; set; }
            public AccOpLevel2() { }
        }
        [Serializable]
        public class AccSpLedger
        {
            public string actcode { get; set; }
            public string grp { get; set; }
            public string grpdesc { get; set; }
            public string actdesc { get; set; }
            public string voudat1 { get; set; }
            public string vounum1 { get; set; }
            public double opam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double clsam { get; set; }
            public string refnum { get; set; }
            public string trnrmrk { get; set; }
            public string venar { get; set; }
            public AccSpLedger() { }
        }
        [Serializable]
        public class SupCustTxVt
        {
            public string actcode { get; set; }
            public string vounum1 { get; set; }
            public DateTime voudat { get; set; }
            public double opamt { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double clsamt { get; set; }
            public string venar1 { get; set; }
            public string refnum { get; set; }
            public SupCustTxVt() { }
        }


        [Serializable]
        public class AccDetailsSchedule
        {

            public string subcode1 { get; set; }
            public string subdesc1 { get; set; }

            public string unit { get; set; }
            public double trnqty { get; set; }
            public double drqty { get; set; }
            public double crqty { get; set; }
            public double closqty { get; set; }

            public double opnam { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double closam { get; set; }

            public AccDetailsSchedule() { }
        }

        [Serializable]
        public class EClassReciptvspayment
        {
            public string mpaycode { get; set; }
            public string paycode { get; set; }
            public double payam { get; set; }
            public string actdesc { get; set; }
            public string colst { get; set; }


            public EClassReciptvspayment()
            {

            }


            public EClassReciptvspayment(string mpaycode, string paycode, double payam, string actdesc, string colst)
            {

                this.mpaycode = mpaycode;
                this.paycode = paycode;
                this.payam = payam;
                this.actdesc = actdesc;
                this.colst = colst;


            }

        }

        [Serializable]
        public class RptBankReconciliation
        {
            public string grp1 { get; set; }
            public DateTime voudat { get; set; }
            public string vounum { get; set; }
            public string vounum1 { get; set; }
            public string note1 { get; set; }
            public double dram { get; set; }
            public double cram { get; set; }
            public double balam { get; set; }
            public double trnam { get; set; }
            public double vousum { get; set; }
            public string actdesc { get; set; }
            public string subdesc { get; set; }
            public string refnum { get; set; }
            public string comcod { get; set; }
            public string comsnam { get; set; }
            
        }


        [Serializable]
        public class RptMatPriceSummary
        {
            public string comcod { get; set; }
            public string sircode { get; set; }
            public string sirdesc { get; set; }
            public string sirtdes { get; set; }
            public string incoterms { get; set; }
            public string sirunit { get; set; }
            public double sirval { get; set; }
            public string other { get; set; }
            public string size { get; set; }
            public string thickness { get; set; }
            public double Allowance { get; set; }
            public string untcod { get; set; }
            public double mark { get; set; }
        }
    }
}
