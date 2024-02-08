using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPEENTITY.C_81_Hrm.C_89_Pay
{
    public class EmpArrearSalaryList
    {
        public string comcod { get; set; }
        public string deptid { get; set; }
        public string secid { get; set; }
        public string empid { get; set; }
        public string desigid { get; set; }
        public string desig { get; set; }
        public string empname { get; set; }
        public string idcardno { get; set; }
        public double aramt { get; set; }
        public string deptname { get; set; }
        public string section { get; set; }
        public double percnt { get; set; }
        public double pfamt { get; set; }
        public double itax { get; set; }
        public double tapfamt { get; set; }
        public string remarks { get; set; }
    }

    [Serializable]
    public class BankStatement
    {
        public string idcard { get; set; }
        public string empname { get; set; }
        public string banksl { get; set; }
        public string bankaddr { get; set; }
        public string bankname { get; set; }
        public string acno { get; set; }
        public double amt { get; set; }
        public string desig { get; set; }

    }

    [Serializable]
    public class RptSalRequisition
    {
        public string saltype { get; set; }
        public string saldesc { get; set; }
        public string secid { get; set; }
        public string section { get; set; }
        public double noofemp { get; set; }
        public double workday { get; set; }
        public double salam { get; set; }
        public double incam { get; set; }
        public double othour { get; set; }
        public double otamt { get; set; }
        public double pfund { get; set; }
        public double netamt { get; set; }
        public RptSalRequisition()
        {

        }

    }
    [Serializable]
    public class RptSalRequisitionSummary
    {
        public string comcod { get; set; }
        public string saltype { get; set; }
        public string saldesc { get; set; }
        public string secid { get; set; }
        public string secid2 { get; set; }
        public string linecode { get; set; }
        public string hrgdesc { get; set; }
        public string dept { get; set; }
        public string section { get; set; }
        public double noofemp { get; set; }
        public double workday { get; set; }
        public double salam { get; set; }
        public double incam { get; set; }
        public double othour { get; set; }
        public double otamt { get; set; }
        public double pfund { get; set; }
        public double netamt { get; set; }
        public double netamt2 { get; set; }
        public double ot2hour { get; set; }
        public double ot1hour { get; set; }
        public double otNot2orNot1hour { get; set; }
        public double offdayOThour { get; set; }
        public double ot2houramt { get; set; }
        public double ot1houramt { get; set; }
        public double otNot2orNot1houramt { get; set; }
        public double offdayOThouramt { get; set; }
        public double attnbonusamt { get; set; }
        public RptSalRequisitionSummary()
        {

        }

    }
    [Serializable]
    public class RptEarnLvPayRequisition
    {
        public string saltype { get; set; }
        public string saldesc { get; set; }
        public string secid { get; set; }
        public string section { get; set; }
        public double noofemp { get; set; }
        public double elvencashday { get; set; }
        public double elvpayamt { get; set; }
        public RptEarnLvPayRequisition()
        {

        }

    }

    [Serializable]
    public class RptFoodAllowance
    {
        public string empid { get; set; }
        public string refno { get; set; }
        public string desigid { get; set; }
        public string linecode { get; set; }
        public string section { get; set; }
        public string idcardno { get; set; }
        public string empname { get; set; }
        public string desig { get; set; }
        public string linedesc { get; set; }
        public double bsal { get; set; }
        public double gssal { get; set; }
        public double wrkday { get; set; }
        public double perday { get; set; }
        public double payamt { get; set; }
        public RptFoodAllowance()
        {
                
        }
    }
   
    [Serializable]
    public class RptIncrement
    {
        public string empid { get; set; }
        public string empname { get; set; }
        public string idcardno { get; set; }
        public string incrno { get; set; }
        public string incrno1 { get; set; }
        public string incrdate1 { get; set; }
        public string companycod { get; set; }
        public string divid { get; set; }
        public string deptcode { get; set; }
        public string seccode { get; set; }
        public string mdivid { get; set; }
        public string mdeptid { get; set; }
        public string msecid { get; set; }
        public string desigid { get; set; }
        public DateTime joindate { get; set; }
        public string companyname { get; set; }
        public string deptname { get; set; }
        public string section { get; set; }
        public string desig { get; set; }
        public string linecode { get; set; }
        public string linedesc { get; set; }
        public double grossal { get; set; }
        public string lincrdate { get; set; }
        public double lincamt { get; set; }
        public double inpercnt { get; set; }
        public double incamt { get; set; }
        public double finincamt { get; set; }
        public string remarks { get; set; }
        public RptIncrement()
        {

        }
    }

    [Serializable]
    public class RptMonthlyOTSumSectionWise
    {
        public string comcod { get; set; }
        public string daysname { get; set; }
        public double s1 { get; set; }
        public double s2 { get; set; }
        public double s3 { get; set; }
        public double s4 { get; set; }
        public double s5 { get; set; }
        public double s6 { get; set; }
        public double s7 { get; set; }
        public double s8 { get; set; }
        public double s9 { get; set; }
        public double s10 { get; set; }
        public double s11 { get; set; }
        public double s12 { get; set; }
        public double s13 { get; set; }
        public double s14 { get; set; }
        public double s15 { get; set; }
        public double s16 { get; set; }
        public double s17 { get; set; }
        public double s18 { get; set; }
        public double s19 { get; set; }
        public double s20 { get; set; }
        public double s21 { get; set; }
        public double s22 { get; set; }
        public double s23 { get; set; }
        public double s24 { get; set; }
        public double s25 { get; set; }
        public double s26 { get; set; }
        public double s27 { get; set; }
        public double s28 { get; set; }
        public double s29 { get; set; }
        public double s30 { get; set; }
        public double s31 { get; set; }
        public double s32 { get; set; }
        public double s33 { get; set; }
        public double s34 { get; set; }
        public double s35 { get; set; }
        public double s36 { get; set; }
        public double s37 { get; set; }
        public double s38 { get; set; }
        public double s39 { get; set; }
        public double s40 { get; set; }
        public double s41 { get; set; }
        public double s42 { get; set; }
        public double s43 { get; set; }
        public double s44 { get; set; }
        public double s45 { get; set; }
        public double s46 { get; set; }
        public double totalovt { get; set; }
        public double m1 { get; set; }
        public double m2 { get; set; }
        public double m3 { get; set; }
        public double m4 { get; set; }
        public double m5 { get; set; }
        public double m6 { get; set; }
        public double m7 { get; set; }
        public double m8 { get; set; }
        public double m9 { get; set; }
        public double m10 { get; set; }
        public double m11 { get; set; }
        public double m12 { get; set; }
        public double m13 { get; set; }
        public double m14 { get; set; }
        public double m15 { get; set; }
        public double m16 { get; set; }
        public double m17 { get; set; }
        public double m18 { get; set; }
        public double m19 { get; set; }
        public double m20 { get; set; }
        public double m21 { get; set; }
        public double m22 { get; set; }
        public double m23 { get; set; }
        public double m24 { get; set; }
        public double m25 { get; set; }
        public double m26 { get; set; }
        public double m27 { get; set; }
        public double m28 { get; set; }
        public double m29 { get; set; }
        public double m30 { get; set; }
        public double m31 { get; set; }
        public double m32 { get; set; }
        public double m33 { get; set; }
        public double m34 { get; set; }
        public double m35 { get; set; }
        public double m36 { get; set; }
        public double m37 { get; set; }
        public double m38 { get; set; }
        public double m39 { get; set; }
        public double m40 { get; set; }
        public double m41 { get; set; }
        public double m42 { get; set; }
        public double m43 { get; set; }
        public double m44 { get; set; }
        public double m45 { get; set; }
        public double m46 { get; set; }
        public double totalmanpower { get; set; }
        public RptMonthlyOTSumSectionWise()
        {

        }
    }

    [Serializable]
    public class RptOTReqSummary
    {
        public string comcod { get; set; }
        public string rowid { get; set; }
        public string sectionid { get; set; }
        public string empsection { get; set; }
        public string linecode { get; set; }
        public string fline { get; set; }
        public double actotcount { get; set; }
        public double prsntotcount { get; set; }
        public double aprvothrs { get; set; }
        public double actaprvothrs { get; set; }
        public double actothrs { get; set; }
        public double otamt { get; set; }
        public double uauthotcount { get; set; }
        public double uauthothrs { get; set; }
        public double uauthotamt { get; set; }
        public double tototamt { get; set; }
        public string remarks { get; set; }

        public RptOTReqSummary()
        {

        }

    }

    [Serializable]
    public class RptFinalSettPaySheet
    {
        public string empid { get; set; }
        public string idcardno { get; set; }
        public string empname { get; set; }
        public string bankcode { get; set; }
        public string acno { get; set; }
        public double payableamt { get; set; }
        public RptFinalSettPaySheet()
        {

        }

    }

    [Serializable]
    public class RptTransAllowance
    {
        public string empid { get; set; }
        public string refno { get; set; }
        public string desigid { get; set; }
        public string linecode { get; set; }
        public string section { get; set; }
        public string idcardno { get; set; }
        public string empname { get; set; }
        public string desig { get; set; }
        public string linedesc { get; set; }
        public string busloccode { get; set; }
        public string buslocation { get; set; }
        public DateTime actintime { get; set; }
        public DateTime actouttime { get; set; }
        public double payamt { get; set; }
        public RptTransAllowance()
        {

        }
    }
}
